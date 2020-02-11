//---------------------------------------------------------------------------
// PROJECT      : CLEROM.RNG
// COPYRIGHT    : Kuiper 2016
// WEBSITE      : https://kuiper.zone/clerom-rng
// LICENSE      : https://opensource.org/licenses/MIT
//---------------------------------------------------------------------------

namespace Kuiper.Clerom
{
    /// <summary>
    /// Rand48Lcg is a concrete generator class derived from LcgRng which implements
    /// the RAND48 linear congruential algorithm. It has a RandMax value of 2^31-1.
    /// <para>This class also inherits the IClonableRng and IConcordantRng interfaces.</para>
    /// </summary>
    /// <remarks>
    /// <para>The initial state (without randomization) is set to 0x1234ABCD. From this
    /// state, the first three calls to Next() generate: 0x32BF5B92, 0x6B95064B,
    /// 0x2D3A1E03.</para>
    /// <seealso cref="LcgRng&lt;TInteg&gt;"/>
    /// <seealso cref="Rng"/>
    /// <seealso cref="SeedableRng&lt;TInteg&gt;"/>
    /// <seealso cref="IClonableRng"/>
    /// <seealso cref="IConcordantRng"/>
    /// </remarks>
    public class Rand48Lcg : LcgRng<ulong>
    {
        public const ulong Multiplier = 25214903917UL;
        public const ulong Increment =  11UL;
        public const ulong Modulus =    281474976710656UL; // 2^48
        
        private const ulong NativeMax = 2147483647UL;
        private const ulong DefaultSeed = 0x1234ABCDUL;

        /// <summary>
        /// Default constructor. The generator is initialized in its default,
        /// non-randomized, starting state.
        /// </summary>
        public Rand48Lcg()
            : base(NativeMax, Multiplier, Increment, Modulus)
        {
            SetSeed(DefaultSeed);
        }

        /// <summary>
        /// Constructor with an option to randomize the generator.
        /// </summary>
        /// <param name="randomize">Randomize the generator</param>
        public Rand48Lcg(bool randomize)
            : base(NativeMax, Multiplier, Increment, Modulus)
        {
            if (randomize) Randomize();
            else SetSeed(DefaultSeed);
        }

        public override string AlgorithmName
        {
            get { return "RAND48 (LCG)"; }
        }

        public override ulong Next()
        {
            return (base.Next() >> 17);
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
                var temp = new ulong[1] { (seed[0] << 16)  | 0x0000330EU };
                base.SetSeed(temp);
            }
        }

        // Override for performance. Disable to test base methods.
#if !DISABLE_SUBCLASS_OVERRIDES
        public override Rng NewInstance()
        {
            return new Rand48Lcg();
        }
#endif
    }
}
