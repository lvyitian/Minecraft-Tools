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
    /// Xoroshiro128p is a concrete generator class derived from SeedableRng which implements
    /// the xoroshiro128+ algorithm. It has a RandMax value of 2^64-1.
    /// <para>This class also inherits the IClonableRng and IConcordantRng interfaces, as well
    /// as IJumpableRng. Its jump ahead increment is 2^64.</para>
    /// </summary>
    /// <remarks>
    /// <para>The xoroshiro128+ algorithm is an extremely fast pseudo random generator utilizing a
    /// shift/rotate-based linear transformation and 128 bits of internal state.</para>
    /// <para>This implementation supports "fast jumping" but due to its relatively short
    /// period, jumping is acceptable only for applications with a mild amount of parallelism;
    /// otherwise the authors recommend the xorshift1024* generator.</para>
    /// <para>The initial state (without randomization) is set to: 0x0123456789ABCDEF,
    /// 0x0123456789ABCDF0. From this state, the first three calls to Next() generate:
    /// 0x02468ACF13579BDF, 0xF7809392B3C315F9, 0x6BA15C8A1FEDB99E.</para>
    /// <para>xoroshiro128+ was created by David Blackman and Sebastiano Vigna as a
    /// successor to xorshift128+. The algorithm is in the public domain.
    /// For further information, see: http://xorshift.di.unimi.it</para>
    /// <seealso cref="Xorshift1024s"/>
    /// <seealso cref="Rng"/>
    /// <seealso cref="SeedableRng&lt;TInteg&gt;"/>
    /// <seealso cref="IClonableRng"/>
    /// <seealso cref="IConcordantRng"/>
    /// <seealso cref="IJumpableRng"/>
    /// </remarks>
    public class Xoroshiro128p : SeedableRng<ulong>, IClonableRng, IConcordantRng, IJumpableRng
    {
        static readonly ulong[] JumpConstants = { 0xBEAC0467EBA5FACB, 0xD86B048B86AA9922 };

        private ulong state0;
        private ulong state1;

        /// <summary>
        /// Default constructor. The generator is initialized in its default,
        /// non-randomized, starting state.
        /// </summary>
        public Xoroshiro128p()
            : base(UInt64.MaxValue)
        {
            SetStartingState();
        }

        /// <summary>
        /// Constructor with an option to randomize the generator.
        /// </summary>
        /// <param name="randomize">Randomize the generator</param>
        public Xoroshiro128p(bool randomize)
            : base(UInt64.MaxValue)
        {
            if (randomize) Randomize();
            else SetStartingState();
        }

        public override string AlgorithmName
        {
            get { return "xoroshiro128+"; }
        }

        public override ulong Next()
        {
            return NextImpl();
        }

        public override int SeedLength
        {
            get { return 2; }
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
                if (seed.Length == 1)
                {
                    SetStartingState();
                    state0 = seed[0];
                }
                else
                {
                    state0 = seed[0];
                    state1 = seed[1];
                }

                ResetCache();
            }
        }

        public Rng CloneInstance()
        {
            var clone = (Xoroshiro128p)CloneBaseRng();
            clone.state0 = state0;
            clone.state1 = state1;
            return clone;
        }

        public bool IsConcordant(Rng other)
        {
            if (IsBaseConcordant(other))
            {
                Xoroshiro128p temp = (Xoroshiro128p)other;
                return (temp.state0 == state0 && temp.state1 == state1);
            }

            return false;
        }

        public ulong JumpSig
        {
            get { return 2; }
        }

        public int JumpExp
        {
            get { return 64; }
        }

        public void Jump()
        {
            ulong s0 = 0;
            ulong s1 = 0;

            for (int i = 0; i < JumpConstants.Length; ++i)
            {
                for (int b = 0; b < 64; ++b)
                {
                    if ((JumpConstants[i] & (0x01UL << b)) != 0)
                    {
                        s0 ^= state0;
                        s1 ^= state1;
                    }

                    Next();
                }
            }

            state0 = s0;
            state1 = s1;
		}

        // Override for performance. Disable to test base methods.
#if !DISABLE_SUBCLASS_OVERRIDES
        public override Rng NewInstance()
        {
            // Faster than reflection technique in base class.
            return new Xoroshiro128p();
        }

        public override uint Next32()
        {
            // Faster than the "caching" solution in the base class.
            return (uint)NextImpl();
        }

        public override ulong Next64()
        {
            // Faster than base class.
            return NextImpl();
        }

        public override void Discard(long count)
        {
            // Cut out additional call. Marginally faster.
            if (count > 0)
            {
                ulong s0 = state0;
                ulong s1 = state1;

                while (--count > -1)
                {
                    s1 ^= s0;
                    s0 = ((s0 << 55) | (s0 >> 9)) ^ s1 ^ (s1 << 14);
                    s1 = (s1 << 36) | (s1 >> 28);
                }

                state0 = s0;
                state1 = s1;
            }
        }
#endif

        private void SetStartingState()
        {
            // Define an initial seed of:
            // s[0] = 0x0123456789ABCDEF
            // s[n] = s[n-1] + 1;
            state0 = 0x0123456789ABCDEFUL;
            state1 = state0 + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ulong NextImpl()
        {
            ulong rslt = state0 + state1;

            state1 ^= state0;
            state0 = ((state0 << 55) | (state0 >> 9)) ^ state1 ^ (state1 << 14);
            state1 = (state1 << 36) | (state1 >> 28);

            return rslt;
        }

    }
}
