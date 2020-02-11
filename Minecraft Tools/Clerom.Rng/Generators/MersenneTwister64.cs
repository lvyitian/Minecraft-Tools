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
    /// MersenneTwister64 is a concrete generator class derived from SeedableRng which implements
    /// the MT19937-64 algorithm. It has a RandMax value of 2^64-1.
    /// <para>This class also inherits the IClonableRng and IConcordantRng interfaces.</para>
    /// </summary>
    /// <remarks>
    /// <para>Mersenne Twister, or MT19937, is a widely used pseudo random number generator
    /// (MT19937-64 is the 64-bit variant). It is not suited to cryptographic purposes.</para>
    /// <para>The initial state (without randomization) is seeded with the integer value 5489
    /// according to the MT seeding routine*. From this state, the first three calls to Next()
    /// generate: 0xC96D191CF6F6AEA6, 0x401F7AC78BC80F1C, 0xB5EE8CB6ABE457F8.</para>
    /// <para>Mersenne Twister (MT) was originally developed by Makoto Matsumoto and Takuji
    /// Nishimura in 1996/1997.</para>
    /// <para>(*) The SeedableRng.SetSeed() method is overridden in this class in order to
    /// implement the MT seeding routine.</para>
    /// <seealso cref="MersenneTwister64"/>
    /// <seealso cref="Rng"/>
    /// <seealso cref="SeedableRng&lt;TInteg&gt;"/>
    /// <seealso cref="IClonableRng"/>
    /// <seealso cref="IConcordantRng"/>
    /// </remarks>
    public class MersenneTwister64 : SeedableRng<ulong>, IClonableRng, IConcordantRng
    {
        private const ulong DefaultSeed = 5489UL;
        private const int   BitWidth =    64;
        private const int   BitWidthM2 =  62;

        private const ulong InitSeedA = 19650218UL;
        private const ulong InitSeedB = 3935559000370003845UL;
        private const ulong InitSeedC = 2862933555777941757UL;

        private const int  StateSize =  312;
        private const ulong UMask =  0xFFFFFFFF80000000UL;
        private const ulong LMask =  0x000000007FFFFFFFUL;
        private const ulong Matrix = 0xB5026F5AA96619E9UL;
        private const int   ShiftU = 29;
        private const int   ShiftS = 17;
        private const ulong MaskB =  0x71D67FFFEDA60000UL;
        private const int   ShiftT = 37;
        private const ulong MaskC =  0xFFF7EEE000000000UL;
        private const ulong MaskD =  0x5555555555555555U;
        private const int   ShiftL = 43;
        private const ulong InitF =  0x5851F42D4C957F2DUL;
        private const int   Offset = 156;

        private int stateIdx;
        private ulong[] state = new ulong[StateSize];

        /// <summary>
        /// Default constructor. The generator is initialized in its default,
        /// non-randomized, starting state.
        /// </summary>
        public MersenneTwister64()
            : base(UInt64.MaxValue)
        {
            SetSeed(DefaultSeed);
        }

        /// <summary>
        /// Constructor with an option to randomize the generator.
        /// </summary>
        /// <param name="randomize">Randomize the generator</param>
        public MersenneTwister64(bool randomize)
            : base(UInt64.MaxValue)
        {
            if (randomize) Randomize();
            else SetSeed(DefaultSeed);
        }

        public override string AlgorithmName
        {
            get { return "MT19937-64"; }
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
            if (seed == null)
            {
                SetSeed(DefaultSeed);
            }
            else
            if (seed.Length > 0)
            {
                // Pre-initialize state
                // This will call CopyCacheFrom()
                SetSeed(InitSeedA);

                int i = 1;
                uint j = 0;
                int jlen = seed.Length;

                int klen = jlen;
                if (klen < StateSize) klen = StateSize;

                // Use as local
                var sloc = state;

                for (int k = 0; k < klen; ++k)  
                {
                    ulong a = sloc[i - 1];
                    sloc[i] = (sloc[i] ^ ((a ^ (a >> BitWidthM2)) * InitSeedB)) + seed[j] + j;

                    if (++i == StateSize)
                    {
                        sloc[0] = sloc[StateSize - 1];
                        i = 1;
                    }

                    if (++j == jlen) j = 0;
                }

                for (int k = 1; k < StateSize; ++k)
                {
                    ulong a = sloc[i - 1];
                    sloc[i] = (sloc[i] ^ ((a ^ (a >> BitWidthM2)) * InitSeedC)) - (uint)i;

                    if (++i == StateSize)
                    {
                        sloc[0] = sloc[StateSize - 1];
                        i = 1;
                    }
                }

                sloc[0] = 0x01UL << (BitWidth - 1);
            }
        }

        /// <summary>
        /// Overrides SeedableRng.SetSeed(ulong) to implement the MT19937 integer
        /// seeding routine.
        /// </summary>
        public override void SetSeed(ulong seed)
        {
            ulong a;
            var sloc = state;

            sloc[0] = seed;

            for (int n = 1; n < StateSize; ++n)
            {
                a = sloc[n - 1];
                sloc[n] = InitF * (a ^ (a >> BitWidthM2)) + (ulong)n;
            }

            stateIdx = StateSize;

            // Important to reset base cache
            ResetCache();
        }

        public virtual Rng CloneInstance()
        {
            // MemberwiseClone is faster here, as it
            // avoid state initialization in constructor.
            // IMPORTANT. There must be no reference types in the
            // base classes, otherwise we can't use this method.
            var clone = (MersenneTwister64)MemberwiseClone();

            // No need to copy value fields,
            // but we need new array instance.
            clone.state = new ulong[StateSize];
            Buffer.BlockCopy(state, 0, clone.state, 0, StateSize * sizeof(uint));
            
            //clone.state = (ulong[])state.Clone();

            return clone;
        }

        public virtual bool IsConcordant(Rng other)
        {
            if (IsBaseConcordant(other))
            {
                var temp = (MersenneTwister64)other;

                if (temp.stateIdx == stateIdx)
                {
                    var sloc = state;
                    var tloc = temp.state;

                    for (int n = 0; n < StateSize; ++n)
                    {
                        if (tloc[n] != sloc[n])
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
            return new MersenneTwister64();
        }

        public override ulong Next64()
        {
            return NextImpl();
        }

        public override void Discard(long count)
        {
            int idx = stateIdx;
            int max = StateSize + 1;

            while (--count > -1)
            {
                if (++idx == max)
                {
                    Reload();
                    idx = 1;
                }
            }

            stateIdx = idx;
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected ulong NextImpl()
        {
            // Protected for access by subclass
            if (stateIdx == StateSize)
            {
                Reload();
                stateIdx = 0;
            }

            ulong y = state[stateIdx++];

            y ^= (y >> ShiftU) & MaskD;
            y ^= (y << ShiftS) & MaskB;
            y ^= (y << ShiftT) & MaskC;
            return y ^ (y >> ShiftL);
        }

        private void Reload()
        {
            int nrot = 1;
            int mrot = Offset;

            var sloc = state;

            for (int n = 0; n < StateSize; ++n)
            {
                ulong x = ((sloc[n] & UMask) | (sloc[nrot] & LMask));
                ulong xa = x >> 1;

                if ((x & 0x01U) == 1) xa ^= Matrix;

                sloc[n] = sloc[mrot] ^ xa;

                if (++nrot == StateSize) nrot = 0;
                if (++mrot == StateSize) mrot = 0;
            }
            
        }

    }

}