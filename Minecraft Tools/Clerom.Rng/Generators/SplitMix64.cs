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
    /// SplitMix64 is a concrete generator class derived from SeedableRng which implements
    /// the SPLITMIX64 algorithm. It has a RandMax value of 2^64-1.
    /// <para>This class also inherits the IClonableRng and IConcordantRng interfaces.</para>
    /// </summary>
    /// <remarks>
    /// <para>The SPLITMIX64 algorithm is a fixed increment version of Java 8's SplittableRandom
    /// generator. It is a very fast generator which passes "Big Crush" and is useful where
    /// only 64 bits of state is required (otherwise xoroshiro128+ or xorshift1024* are recommended).
    /// </para>
    /// <para>The initial state (without randomization) is set to 0x0123456789ABCDEF. From this
    /// state, the first three calls to Next() generate: 0x157A3807A48FAA9D, 0xD573529B34A1D093,
    /// 0x2F90B72E996DCCBE.</para>
    /// <para>This implementation is derived from code put in the public domain by Sebastiano Vigna.
    /// Refer to: http://xoroshiro.di.unimi.it/splitmix64.c</para>
    /// <seealso cref="Rng"/>
    /// <seealso cref="SeedableRng&lt;TInteg&gt;"/>
    /// <seealso cref="IClonableRng"/>
    /// <seealso cref="IConcordantRng"/>
    /// </remarks>
    public class SplitMix64 : SeedableRng<ulong>, IClonableRng, IConcordantRng
    {
        private const ulong DefaultSeed =  0x0123456789ABCDEFUL;

        private ulong state = DefaultSeed;

        /// <summary>
        /// Default constructor. The generator is initialized in its default,
        /// non-randomized, starting state.
        /// </summary>
        public SplitMix64()
            : base(UInt64.MaxValue)
        {
        }

        /// <summary>
        /// Constructor with an option to randomize the generator.
        /// </summary>
        /// <param name="randomize">Randomize the generator</param>
        public SplitMix64(bool randomize)
            : base(UInt64.MaxValue)
        {
            if (randomize) Randomize();
        }

        public override string AlgorithmName
        {
            get { return "SPLITMIX64"; }
        }

        public override ulong Next()
        {
            return NextImpl();
        }

        public override int SeedLength
        {
            get { return 1; }
        }

        public override void SetSeed(ulong[] seed)
        {
            if (seed == null)
            {
                state = DefaultSeed;

                // Important to reset base cache
                ResetCache();
            }
            else
            if (seed.Length > 0)
            {
                state = seed[0];
                ResetCache();
            }
        }

        public Rng CloneInstance()
        {
            var clone = (SplitMix64)CloneBaseRng();
            clone.state = state;
            return clone;
        }

        public bool IsConcordant(Rng other)
        {
            if (IsBaseConcordant(other))
            {
                return (state == ((SplitMix64)other).state);
            }

            return false;
        }

        // Override for performance. Disable to test base methods.
#if !DISABLE_SUBCLASS_OVERRIDES
        public override Rng NewInstance()
        {
            return new SplitMix64();
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
            while (--count > -1)
            {
                state += 0x9E3779B97F4A7C15UL;
            }
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ulong NextImpl()
        {
            ulong z = (state += 0x9E3779B97F4A7C15UL);
            z = (z ^ (z >> 30)) * 0xBF58476D1CE4E5B9UL;
	        z = (z ^ (z >> 27)) * 0x94D049BB133111EBUL;
	        return z ^ (z >> 31);
        }

    }
}