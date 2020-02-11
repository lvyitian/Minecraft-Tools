//---------------------------------------------------------------------------
// PROJECT      : CLEROM.RNG
// COPYRIGHT    : Kuiper 2016
// WEBSITE      : https://kuiper.zone/clerom-rng
// LICENSE      : https://opensource.org/licenses/MIT
//---------------------------------------------------------------------------

namespace Kuiper.Clerom
{
    /// <summary>
    /// MinStdLcg is a concrete generator class derived from LcgRng which implements
    /// the "Minimum standard" linear congruential algorithm,  as recommended by Park, Miller
    /// and Stockmeyer in 1993. It has a RandMax value of 2^31-2.
    /// <para>This class also inherits the IClonableRng and IConcordantRng interfaces.</para>
    /// </summary>
    /// <remarks>
    /// <para>The initial state (without randomization) is set to 1. From this
    /// state, the first three calls to Next() generate: 0x0000BC8F, 0x0AE257E2,
    /// 0x4CF91F46.</para>
    /// <seealso cref="LcgRng&lt;TInteg&gt;"/>
    /// <seealso cref="Rng"/>
    /// <seealso cref="SeedableRng&lt;TInteg&gt;"/>
    /// <seealso cref="IClonableRng"/>
    /// <seealso cref="IConcordantRng"/>
    /// </remarks>
    public class MinStdLcg : LcgRng<uint>
    {
        public const ulong Multiplier = 48271UL;
        public const ulong Increment =  0UL;
        public const ulong Modulus =    2147483647UL; // 2^31-1

        /// <summary>
        /// Default constructor. The generator is initialized in its default,
        /// non-randomized, starting state.
        /// </summary>
        public MinStdLcg()
            : base(2147483646UL, Multiplier, Increment, Modulus)
        {
        }

        /// <summary>
        /// Constructor with an option to randomize the generator.
        /// </summary>
        /// <param name="randomize">Randomize the generator</param>
        public MinStdLcg(bool randomize)
            : base(2147483646UL, Multiplier, Increment, Modulus)
        {
            if (randomize) Randomize();
        }

        public override string AlgorithmName
        {
            get { return "MINSTD (LCG)"; }
        }

        // Override for performance. Disable to test base methods.
#if !DISABLE_SUBCLASS_OVERRIDES
        public override Rng NewInstance()
        {
            return new MinStdLcg();
        }
#endif
    }
}
