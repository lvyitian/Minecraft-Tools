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
    /// Isaac64 is a concrete generator class derived from SeedableRng which implements
    /// the ISAAC-64 algorithm. It has a RandMax value of 2^64-1.
    /// <para>This class also inherits the IClonableRng and IConcordantRng interfaces.</para>
    /// </summary>
    /// <remarks>
    /// <para>ISAAC is a fast pseudo random number generator algorithm widely held to be
    /// cryptographically secure. This is a 64-bit variant of the algorithm.</para>
    /// <para>The internal state comprises 256 64-bit integers, with results written to another
    /// 256 integer results array. The initial state (without randomization) is an array of zero
    /// values. From this state, the first three calls to Next() generate:
    /// 0x9D39247E33776D41, 0x2AF7398005AAA5C7, 0x44DB015024623547.</para>
    /// <para>The ISAAC algorithm was created by Bob Jenkins and is in the public domain. For more
    /// information, see: http://burtleburtle.net/bob/rand/isaacafa.html</para>
    /// <seealso cref="Isaac32"/>
    /// <seealso cref="Rng"/>
    /// <seealso cref="SeedableRng&lt;TInteg&gt;"/>
    /// <seealso cref="IClonableRng"/>
    /// <seealso cref="IConcordantRng"/>
    /// </remarks>
    public class Isaac64 :SeedableRng<ulong>, IClonableRng, IConcordantRng
    {
        private const int   StateSize = 256;
        private const uint  StateMask = 0xFF;
        private const ulong GoldenRatio = 0x9E3779B97F4A7C13UL;

        private int stateIdx;
        private ulong aa, bb, cc;
        private ulong[] state = new ulong[StateSize];
        private ulong[] ready = new ulong[StateSize];

        /// <summary>
        /// Default constructor. The generator is initialized in its default,
        /// non-randomized, starting state.
        /// </summary>
        public Isaac64()
            : base(UInt64.MaxValue)
        {
            SetSeed(null);
        }

        /// <summary>
        /// Constructor with an option to randomize the generator.
        /// </summary>
        /// <param name="randomize">Randomize the generator</param>
        public Isaac64(bool randomize)
            : base(UInt64.MaxValue)
        {
            if (randomize) Randomize();
            else SetSeed(null);
        }

        public override string AlgorithmName
        {
            get { return "ISAAC-64"; }
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
                        Buffer.BlockCopy(seed, 0, ready, 0, seed.Length * sizeof(ulong));
                    }
                    else
                    {
                        Buffer.BlockCopy(seed, 0, ready, 0, StateSize * sizeof(ulong));
                    }

                }

                aa = 0;
                bb = 0;
                cc = 0;

                ulong a = GoldenRatio;
                ulong b = a, c = a, d = a, e = a, f = a, g = a, h = a;

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
            var clone = (Isaac64)MemberwiseClone();

            // No need to copy value fields,
            // but we need new array instances.
            clone.state = new ulong[StateSize];
            Buffer.BlockCopy(state, 0, clone.state, 0, StateSize * sizeof(ulong));

            clone.ready = new ulong[StateSize];
            Buffer.BlockCopy(ready, 0, clone.ready, 0, StateSize * sizeof(ulong));

            //clone.state = (ulong[])state.Clone();
            //clone.ready = (ulong[])ready.Clone();

            return clone;
        }

        public bool IsConcordant(Rng other)
        {
            if (IsBaseConcordant(other))
            {
                var temp = (Isaac64)other;

                if (temp.stateIdx == stateIdx && temp.aa == aa &&
                    temp.bb == bb && temp.cc == cc)
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
            return new Isaac64();
        }
        
        public override ulong Next64()
        {
            return NextImpl();
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
        private ulong NextImpl()
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
            ulong x, y;
            ulong a = aa;
            ulong b = bb + (++cc);

            int idx;
            int nrot = StateSize / 2 - 1;

            // Local copies
            var sloc = state;
            var rloc = ready;

            for (int n = 0; n < StateSize; ++n)
            {
                x = sloc[n];
                idx = n & 0x03;

                if (idx == 0) a = ~(a ^ (a << 21));
                else if (idx == 1) a ^= (a >> 5);
                else if (idx == 2) a ^= (a << 12);
                else a ^= (a >> 33);

                a += sloc[++nrot & StateMask];

                sloc[n] = y = sloc[(x >> 3) & StateMask] + a + b;
                rloc[n] = b = sloc[(y >> 11) & StateMask] + x;
            }

            aa = a;
            bb = b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Mixer(ref ulong a, ref ulong b, ref ulong c, ref ulong d,
            ref ulong e, ref ulong f, ref ulong g, ref ulong h)
        {
           a -= e; f ^= h >> 9;  h += a;
           b -= f; g ^= a << 9;  a += b;
           c -= g; h ^= b >> 23; b += c;
           d -= h; a ^= c << 15; c += d;
           e -= a; b ^= d >> 14; d += e;
           f -= b; c ^= e << 20; e += f;
           g -= c; d ^= f >> 17; f += g;
           h -= d; e ^= g << 14; g += h;
        }
    }
}
