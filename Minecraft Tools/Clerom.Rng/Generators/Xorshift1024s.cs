//---------------------------------------------------------------------------
// PROJECT      : CLEROM.RNG
// COPYRIGHT    : Kuiper 2016
// WEBSITE      : https://kuiper.zone/clerom-rng
// LICENSE      : https://opensource.org/licenses/MIT
//---------------------------------------------------------------------------

using Kuiper.Clerom.Internal;
using System;
using System.Runtime.CompilerServices;

namespace Kuiper.Clerom
{
    /// <summary>
    /// Xorshift1024s is a concrete generator class derived from SeedableRng which implements
    /// the xorshift1024* algorithm. It has a RandMax value of 2^64-1.
    /// <para>This class also inherits the IClonableRng and IConcordantRng interfaces, as well
    /// as IJumpableRng. Its jump ahead increment is 2^512.</para>
    /// </summary>
    /// <remarks>
    /// <para>The xorshift1024* algorithm is a fast non-cryptographic pseudo random generator
    /// utilizing 1024 bits of internal state. It supports fast jumping and is ideal for
    /// applications with a high degree of parallelism.</para>
    /// <para>The initial state (without randomization) is populated as an array of 16 64-bit
    /// integers starting at 0x0123456789ABCDEF and incrementing by 1. From this state, the first
    /// three calls to Next() generate:
    /// 0xB20283EEF9A06758, 0xF7453718340F5D75, 0x5BF14C48B12F2522.</para>
    /// <para>The xorshift1024* algorithm was created by Sebastiano Vigna and is in the public
    /// domain. For further information, see: http://xorshift.di.unimi.it</para>
    /// <seealso cref="Xoroshiro128p"/>
    /// <seealso cref="Rng"/>
    /// <seealso cref="SeedableRng&lt;TInteg&gt;"/>
    /// <seealso cref="IClonableRng"/>
    /// <seealso cref="IConcordantRng"/>
    /// <seealso cref="IJumpableRng"/>
    /// </remarks>
    public class Xorshift1024s :  SeedableRng<ulong>, IClonableRng, IConcordantRng, IJumpableRng
    {
        private const int StateSize = 16;
        private const int IndexMax = StateSize - 1;

        private static readonly ulong[] JumpConstants = { 0x84242F96ECA9C41DUL,
            0xA3C65B8776F96855UL, 0x5B34A39F070B5837UL, 0x4489AFFCE4F31A1EUL,
            0x2FFEEB0A48316F40UL, 0xDC2D9891FE68C022UL, 0x3659132BB12FEA70UL,
            0xAAC17D8EFA43CAB8UL, 0xC4CB815590989B13UL, 0x5EE975283D71C93BUL,
            0x691548C86C1BD540UL, 0x7910C41D10A1E6A5UL, 0x0B5FC64563B3E2A8UL,
            0x047F7684E9FC949DUL, 0xB99181F2D8F685CAUL, 0x284600E3F30E38C3UL };

        private int stateIdx = 0;
        private ulong[] state = new ulong[StateSize];

        /// <summary>
        /// Default constructor. The generator is initialized in its default,
        /// non-randomized, starting state.
        /// </summary>
        public Xorshift1024s()
            : base(UInt64.MaxValue)
        {
            SetStartingState();
        }

        /// <summary>
        /// Constructor with an option to randomize the generator.
        /// </summary>
        /// <param name="randomize">Randomize the generator</param>
        public Xorshift1024s(bool randomize)
            : base(UInt64.MaxValue)
        {
            if (randomize) Randomize();
            else SetStartingState();
        }

        public override string AlgorithmName
        {
            get { return "xorshift1024*"; }
        }

        public override ulong Next()
        {
            return NextImpl();
        }

        public override int SeedLength
        {
            get { return StateSize; }
        }

        public override void SetSeed(ulong[] seed)
        {
            // Don't allow all zero values with this generator.
            if (seed == null || CleromUtil.IsArrayZero(seed))
            {
                SetStartingState();

                // Important to reset base cache
                ResetCache();
            }
            else
            if (seed.Length > 0)
            {
                if (seed.Length < StateSize)
                {
                    SetStartingState();
                    Buffer.BlockCopy(seed, 0, state, 0, seed.Length * sizeof(ulong));
                }
                else
                {
                    Buffer.BlockCopy(seed, 0, state, 0, StateSize * sizeof(ulong));
                }

                stateIdx = 0;
                ResetCache();
            }
        }

        public Rng CloneInstance()
        {
            var clone = (Xorshift1024s)CloneBaseRng();

            clone.stateIdx = stateIdx;
            Buffer.BlockCopy(state, 0, clone.state, 0, StateSize * sizeof(ulong));

            return clone;
        }

        public bool IsConcordant(Rng other)
        {
            if (IsBaseConcordant(other))
            {
                Xorshift1024s temp = (Xorshift1024s)other;

                if (temp.stateIdx == stateIdx)
                {
                    for (int n = 0; n < StateSize; ++n)
                    {
                        if (temp.state[n] != state[n])
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        public ulong JumpSig
        {
            get { return 2; }
        }

        public int JumpExp
        {
            get { return 512; }
        }

        public void Jump()
        {
            ulong[] t = new ulong[StateSize];

            for (int i = 0; i < JumpConstants.Length; ++i)
            {
                for (int b = 0; b < 64; ++b)
                {
                    if ((JumpConstants[i] & (0x01UL) << b) != 0)
                    {
                        for (int j = 0; j < StateSize; ++j)
                        {
                            t[j] ^= state[(j + stateIdx) & IndexMax];
                        }
                    }

                    Next();
                }
            }

            for (int j = 0; j < StateSize; ++j)
            {
                state[(j + stateIdx) & IndexMax] = t[j];
            }
        }

        // Override for performance. Disable to test base methods.
#if !DISABLE_SUBCLASS_OVERRIDES
        public override Rng NewInstance()
        {
            return new Xorshift1024s();
        }

        public override uint Next32()
        {
            return (uint)NextImpl();
        }

        public override ulong Next64()
        {
            return NextImpl();
        }

        public override void Discard(long count)
        {
            if (count > 0)
            {
                int idx = stateIdx;
                ulong s1, s0 = state[idx];

                while (--count > -1)
                {
                    s1 = state[idx = (idx + 1) & IndexMax];
                    s1 ^= s1 << 31;
                    s0 = s1 ^ s0 ^ (s1 >> 11) ^ (s0 >> 30);
                }

                stateIdx = idx;
                state[idx] = s0;
            }
        }
#endif

        private void SetStartingState()
        {
            // Define an initial seed of:
            // s[0] = 0x0123456789ABCDEF
            // s[n] = s[n-1] + 1;
            stateIdx = 0;
            state[0] = 0x0123456789ABCDEFUL;

            for (int n = 1; n < StateSize; ++n)
            {
                state[n] = state[n - 1] + 1;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ulong NextImpl()
        {
            ulong s0 = state[stateIdx];
            ulong s1 = state[stateIdx = (stateIdx + 1) & IndexMax];

            s1 ^= s1 << 31;
            s1 ^= s0 ^ (s1 >> 11) ^ (s0 >> 30);
            state[stateIdx] = s1;

            return s1 * 1181783497276652981UL;
        }
    }
}
