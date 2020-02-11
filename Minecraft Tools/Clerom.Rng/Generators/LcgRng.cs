//---------------------------------------------------------------------------
// PROJECT      : CLEROM.RNG
// COPYRIGHT    : Kuiper 2016
// WEBSITE      : https://kuiper.zone/clerom-rng
// LICENSE      : https://opensource.org/licenses/MIT
//---------------------------------------------------------------------------

using Kuiper.Clerom.Internal;
using System;

namespace Kuiper.Clerom
{
    /// <summary>
    /// LcgRng&lt;TInteg&gt; is a "generic" base class for linear congruential generators (LCGs).
    /// The TInteg type must be one of the standard integer types, namely int, uint, ulong etc.
    /// <para>The class is concrete and can be instantiated by providing the LCG parameters to
    /// the constructor. Rather than direct instantiation, however, it is recommended that LCG
    /// generators are created by inheriting from LcgRng instead.</para>
    /// <para>LcgRng derives from SeedableRng. It inherits the IClonableRng and IConcordantRng
    /// interfaces.</para>
    /// </summary>
    /// <remarks>
    /// <para>Linear congruential generators are defined by:</para>
    /// <para><i>X(n+1) = ( X(n) * a + c ) % m</i></para>
    /// <para>where "a" is the multiplier, "c" the increment and "m" the modulus. The initial
    /// value for "X" is the seed.</para>
    /// <para>LCGs are typically fast and require minimal memory. However, they suffer from
    /// serial correlation and are unsuitable for applications requiring high quality randomness.
    /// For most purposes, they should be considered obsolete. The xoroshiro128+ algorithm is a
    /// superior alternative offering comparable (or better) speed and memory usage.
    /// </para>
    /// <seealso cref="Rand48Lcg"/>
    /// <seealso cref="MinStdLcg"/>
    /// <seealso cref="Rng"/>
    /// <seealso cref="SeedableRng&lt;TInteg&gt;"/>
    /// <seealso cref="IClonableRng"/>
    /// <seealso cref="IConcordantRng"/>
    /// </remarks>
    public class LcgRng<TInteg> : SeedableRng<TInteg>, IClonableRng, IConcordantRng
        where TInteg : struct
    {
        private const ulong DefaultSeed = 1;

        // Work with ulong internally
        private ulong mult;
        private ulong inc;
        private ulong mod;
        private ulong modMask;

        private ulong state = DefaultSeed;

        /// <summary>
        /// LCG constructor.
        /// <para>It is possible to define a randMax value less than or equal to m-1, as used by
        /// Rand48Lcg. Setting m to 0 negates the modulus.</para>
        /// </summary>
        /// <param name="randMax">Maximum value returned by Next()</param>
        /// <param name="a">Multiplier</param>
        /// <param name="c">Increment</param>
        /// <param name="m">Modulus</param>
        public LcgRng(ulong randMax, ulong a, ulong c, ulong m)
            : base(randMax)
        {
            if (m != 0 && m <= randMax)
            {
                throw new ArgumentException("randMax", "The RandMax value " + randMax +
                    "is invalid (not possible with modulus " + m + ").");
            }

            mult = a;
            inc = c;
            mod = m;

            // Determine a bit mask to use instead of modulus, if possible.
            // This yields as big performance improvement as the C# "%" operator
            // does not appear optimized where the maximum result is a power of 2 value.

            // Default where modulus is 0,
            // which implicitly means 2^64.
            modMask = UInt64.MaxValue;

            if (mod != 0)
            {
                modMask = mod - 1;

                // Number of bits needed by modulus
                int mbits = CleromUtil.BitSize(modMask);

                // If not equal, we can't use a mask.
                if (modMask != UInt64.MaxValue >> (64 - mbits)) modMask = 0;
            }
        }

        public override string AlgorithmName
        {
            // Generic name
            get { return "LCG"; }
        }

        public override ulong Next()
        {
            state = mult * state + inc;
            
            // Use a mask instead of modulus if we can.
            // Applies also where mod is 0 (implicit 2^64).
            if (modMask != 0) return (state &= modMask);

            // Everything else.
            // This is why MINSTD may be slower.
            return (state %= mod);
        }

        public override Rng NewInstance()
        {
            if (GetType() ==  typeof(LcgRng<TInteg>))
            {
                // Not a subclass, create an instance of this with same constants.
                return new LcgRng<TInteg>(RandMax, mult, inc, mod);
            }

            return base.NewInstance();
        }

        public override int SeedLength
        {
            get { return 1; }
        }

        public override void SetSeed(TInteg[] seed)
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
                // Get element 0 as a ulong
                ulong seed64 = Convert.ToUInt64(seed[0]);

                if (mod != 0)
                {
                    seed64 %= mod;

                    if (inc % mod == 0 && seed64 == 0)
                    {
                        // Seed not valid, set default.
                        state = DefaultSeed;
                    }
                    else
                    {
                        state = seed64;
                    }
                }
                else
                {
                    // Implicit mod of 2^64
                    state = seed64;
                }   

                // Important to reset base cache
                ResetCache();
            }
        }

        public Rng CloneInstance()
        {
            var clone = (LcgRng<TInteg>)CloneBaseRng();

            clone.mult = mult;
            clone.inc = inc;
            clone.mod = mod;
            clone.modMask = modMask;

            clone.state = state;

            return clone;
        }

        public bool IsConcordant(Rng other)
        {
            if (IsBaseConcordant(other))
            {
                var otherLcg = (LcgRng<TInteg>)other;

                return (
                    state == otherLcg.state &&
                    mult == otherLcg.mult &&
                    inc == otherLcg.inc &&
                    mod == otherLcg.mod &&
                    modMask == otherLcg.modMask
                    );
            }

            return false;
        }
    }
}
