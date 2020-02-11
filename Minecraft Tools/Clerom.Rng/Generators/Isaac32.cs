//---------------------------------------------------------------------------
// PROJECT      : CLEROM.RNG
// COPYRIGHT    : Kuiper 2016
// WEBSITE      : https://kuiper.zone/clerom-rng
// LICENSE      : https://opensource.org/licenses/MIT
//---------------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;

namespace Kuiper.Clerom
{
    /// <summary>
    /// Isaac32 is a concrete generator class derived from SeedableRng which implements
    /// the ISAAC algorithm. It has a RandMax value of 2^32-1.
    /// <para>This class also inherits the IClonableRng and IConcordantRng interfaces.</para>
    /// </summary>
    /// <remarks>
    /// <para>ISAAC is a fast pseudo random number generator algorithm widely held to be
    /// cryptographically secure.</para>
    /// <para>The internal state comprises 256 32-bit integers, with results written to another
    /// 256 integer results array. The initial state (without randomization) is an array of zero
    /// values. From this state, the first three calls to Next() generate: 0x182600F3, 0x300B4A8D,
    /// 0x301B6622.</para>
    /// <para>The ISAAC algorithm was created by Bob Jenkins and is in the public domain. For more
    /// information, see: http://burtleburtle.net/bob/rand/isaacafa.html</para>
    /// <seealso cref="Isaac64"/>
    /// <seealso cref="Rng"/>
    /// <seealso cref="SeedableRng&lt;TInteg&gt;"/>
    /// <seealso cref="IClonableRng"/>
    /// <seealso cref="IConcordantRng"/>
    /// </remarks>
    public class Isaac32 : SeedableRng<uint>, IClonableRng, IConcordantRng
    {
        private const int  StateSize = 256;
        private const uint StateMask = 0xFF;
        private const uint GoldenRatio = 0x9E3779B9U;

        private int stateIdx;
        private uint aa, bb, cc;
        private uint[] state = new uint[StateSize];
        private uint[] ready = new uint[StateSize];

        /// <summary>
        /// Default constructor. The generator is initialized in its default,
        /// non-randomized, starting state.
        /// </summary>
        public Isaac32()
            : base(UInt32.MaxValue)
        {
            SetSeed(null);
        }

        /// <summary>
        /// Constructor with an option to randomize the generator.
        /// </summary>
        /// <param name="randomize">Randomize the generator</param>
        public Isaac32(bool randomize)
            : base(UInt32.MaxValue)
        {
            if (randomize) Randomize();
            else SetSeed(null);
        }

        public override string AlgorithmName
        {
            get { return "ISAAC-32"; }
        }

        public override ulong Next()
        {
            return NextImpl();
        }

        public override int SeedLength
        {
            get { return StateSize; }

        }
       
        public override void SetSeed(uint[] seed)
        {
            if (seed == null || seed.Length > 0)
            {
                // Zero result block
                for (int n = 0; n < StateSize; ++n)
                {
                    ready[n] = 0;
                }

                if (seed != null)
                {
                    // Copy seed to result block
                    if (seed.Length < StateSize)
                    {
                        Buffer.BlockCopy(seed, 0, ready, 0, seed.Length * sizeof(uint));
                    }
                    else
                    {
                        Buffer.BlockCopy(seed, 0, ready, 0, StateSize * sizeof(uint));
                    }
                }

                aa = 0;
                bb = 0;
                cc = 0;

                uint a = GoldenRatio;
                uint b = a, c = a, d = a, e = a, f = a, g = a, h = a;

                // Scramble
                for (int n = 0; n < 4; ++n)
                {
                    Mixer(ref a, ref b, ref c, ref d, ref e, ref f, ref g, ref h);
                }

                for (int n = 0; n < StateSize; n += 8)
                {
                    // Populated with seed
                    a += ready[n];
                    b += ready[n + 1];
                    c += ready[n + 2];
                    d += ready[n + 3];
                    e += ready[n + 4];
                    f += ready[n + 5];
                    g += ready[n + 6];
                    h += ready[n + 7];

                    Mixer(ref a, ref b, ref c, ref d, ref e, ref f, ref g, ref h);

                    // Initialize state
                    state[n] = a;
                    state[n + 1] = b;
                    state[n + 2] = c;
                    state[n + 3] = d;
                    state[n + 4] = e;
                    state[n + 5] = f;
                    state[n + 6] = g;
                    state[n + 7] = h;
                }

                // Second pass
                for (int n = 0; n < StateSize; n += 8)
                {
                    a += state[n];
                    b += state[n + 1];
                    c += state[n + 2];
                    d += state[n + 3];
                    e += state[n + 4];
                    f += state[n + 5];
                    g += state[n + 6];
                    h += state[n + 7];

                    Mixer(ref a, ref b, ref c, ref d, ref e, ref f, ref g, ref h);

                    state[n] = a;
                    state[n + 1] = b;
                    state[n + 2] = c;
                    state[n + 3] = d;
                    state[n + 4] = e;
                    state[n + 5] = f;
                    state[n + 6] = g;
                    state[n + 7] = h;
                }

                stateIdx = 0;

                // Important to reset base cache
                ResetCache();
            }

        }

        public Rng CloneInstance()
        {
            // MemberwiseClone is faster here, as it
            // avoids state initialization in constructor.
            // IMPORTANT. There must be no reference types in the
            // base classes, otherwise we can't use this method.
            var clone = (Isaac32)MemberwiseClone();

            // No need to copy value fields,
            // but we need new array instances.
            clone.state = new uint[StateSize];
            Buffer.BlockCopy(state, 0, clone.state, 0, StateSize * sizeof(uint));

            clone.ready = new uint[StateSize];
            Buffer.BlockCopy(ready, 0, clone.ready, 0, StateSize * sizeof(uint));

            //clone.state = (uint[])state.Clone();
            //clone.ready = (uint[])ready.Clone();

            return clone;
        }

        public bool IsConcordant(Rng other)
        {
            if (IsBaseConcordant(other))
            {
                var temp = (Isaac32)other;

                if (temp.stateIdx == stateIdx &&
                    temp.aa == aa &&
                    temp.bb == bb &&
                    temp.cc == cc)
                {
                    for (int n = 0; n < StateSize; ++n)
                    {
                        if (temp.state[n] != state[n] || temp.ready[n] != ready[n])
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        // Override for performance. Disable to test base methods.
#if !DISABLE_SUBCLASS_OVERRIDES
        public override Rng NewInstance()
        {
            return new Isaac32();
        }

        public override uint Next32()
        {
            return NextImpl();
        }

        public override ulong Next64()
        {
            return ((ulong)NextImpl() << 32) | NextImpl();
        }

        public override void Discard(long count)
        {
            int idx = stateIdx;

            while (--count > -1)
            {
                if (--idx == -1)
                {
                    Reload();
                    idx = StateSize - 1;
                }
            }

            stateIdx = idx;
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private uint NextImpl()
        {
            if (stateIdx == 0)
            {
                Reload();
                stateIdx = StateSize;
            }

            return ready[--stateIdx];
        }

        private void Reload()
        {
            uint x, y;
            uint a = aa;
            uint b = bb + (++cc);

            int idx;
            int nrot = StateSize / 2 - 1;

            // Local copies
            var sloc = state;
            var rloc = ready;

            for (int n = 0; n < StateSize; ++n)
            {
                x = sloc[n];
                idx = n & 0x03;

                if (idx == 0) a ^= (a << 13);
                else if (idx == 1) a ^= (a >> 6);
                else if (idx == 2) a ^= (a << 2);
                else a ^= (a >> 16);

                a += sloc[++nrot & StateMask];

                sloc[n] = y = sloc[(x >> 2) & StateMask] + a + b;
                rloc[n] = b = sloc[(y >> 10) & StateMask] + x;
            }

            aa = a;
            bb = b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Mixer(ref uint a, ref uint b, ref uint c, ref uint d,
            ref uint e, ref uint f, ref uint g, ref uint h)
        {
            a ^= b << 11; d += a; b += c;
            b ^= c >> 2; e += b; c += d;
            c ^= d << 8; f += c; d += e;
            d ^= e >> 16; g += d; e += f;
            e ^= f << 10; h += e; f += g;
            f ^= g >> 4; a += f; g += h;
            g ^= h << 8; b += g; h += a;
            h ^= a >> 9; c += h; a += b;
        }
    }
}
