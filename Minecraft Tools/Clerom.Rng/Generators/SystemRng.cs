//---------------------------------------------------------------------------
// PROJECT      : CLEROM.RNG
// COPYRIGHT    : Kuiper 2016
// WEBSITE      : https://kuiper.zone/clerom-rng
// LICENSE      : https://opensource.org/licenses/MIT
//---------------------------------------------------------------------------

using System;

namespace Kuiper.Clerom
{
    /// <summary>
    /// SystemRng does not implement a pseudo random number generator itself, rather it
    /// encapsulates an instance of the C# System.Random generator in a class derived from
    /// SeedableRng&lt;int&gt;. It has a RandMax value of 2^32-1.
    /// <para>SystemRng does not inherit the IClonableRng or IConcordantRng interfaces.</para>
    /// </summary>
    /// <remarks>
    /// <para>If seeded with the same value, the SystemRng.Next() and Random.Next() calls will both
    /// return the same results. Unlike the System.Random() default constructor, which seeds the
    /// generator with a time value, note that the SystemRng() constructor uses a static value,
    /// 0x1234ABCD, as the seed. Call randomize() to put the generator into a unique state.</para>
    /// <para>According to Microsoft documentation, the System.Random generator uses a subtractive
    /// random number generator algorithm proposed by Donald E. Knuth. However, it is known* that the
    /// implementation is compromised due to an error which Microsoft has concluded cannot be fixed
    /// without adversely affecting existing applications which are reliant on seeded repeatability.</para>
    /// <para>(*) https://connect.microsoft.com/VisualStudio/feedback/details/634761/system-random-serious-bug</para>
    /// <seealso cref="Rng"/>
    /// <seealso cref="SeedableRng&lt;TInteg&gt;"/>
    /// <seealso cref="SystemCrypto"/>
    /// </remarks>
    public class SystemRng : SeedableRng<int>
    {
        private const int DefaultSeed = 0x1234ABCD;

        private Random sysRand;

        /// <summary>
        /// Default constructor. The generator is initialized in its default,
        /// non-randomized, starting state.
        /// </summary>
        public SystemRng()
            : base(Int32.MaxValue - 1)
        {
            SetSeed(null);
        }

        /// <summary>
        /// Constructor with an option to randomize the generator.
        /// </summary>
        /// <param name="randomize">Randomize the generator</param>
        public SystemRng(bool randomize)
            : base(Int32.MaxValue - 1)
        {
            if (randomize) Randomize();
            else SetSeed(null);
        }

        public override string AlgorithmName
        {
            get { return "System.Random"; }
        }

        public override ulong Next()
        {
            return (ulong)sysRand.Next();
        }

        public override int SeedLength
        {
            get { return 1; }
        }

        public override void SetSeed(int[] seed)
        {
            // No other way to set seed. A tad slow.
            if (seed == null)
            {
                sysRand = new Random(DefaultSeed);

                // Important to reset base cache
                ResetCache();
            }
            else
            if (seed.Length > 0)
            {
                sysRand = new Random(seed[0]);
                ResetCache();
            }
        }

        public override uint GetUInt32(uint max, bool maxIncl)
        {
            // Override to ensure same results as Random, where possible.
            if (max > 0 && max < UInt32.MaxValue)
            {
                if (maxIncl) ++max;
                return (uint)(sysRand.Next(
                    Int32.MinValue, (int)max + Int32.MinValue) - Int32.MinValue);
            }

            // This will handle case where max is UInt32.MaxValue inclusive,
            // which Next() doesn't support, and throw our exceptions on range error.
            return base.GetUInt32(max, maxIncl);
        }

        public override double GetDouble()
        {
            // Override to ensure same results as Random.
            return sysRand.NextDouble();
        }

        // Override for performance. Disable to test base methods.
#if !DISABLE_SUBCLASS_OVERRIDES
        public override Rng NewInstance()
        {
            return new SystemRng();
        }
#endif
    }
}
