//---------------------------------------------------------------------------
// PROJECT      : CLEROM.RNG
// COPYRIGHT    : Kuiper 2016
// WEBSITE      : https://kuiper.zone/clerom-rng
// LICENSE      : https://opensource.org/licenses/MIT
//---------------------------------------------------------------------------

namespace Kuiper.Clerom
{
    /// <summary>
    /// An interface for cloning CLEROM.RNG generators, where clones have a "deep" copy of
    /// the original's internal state.
    /// </summary>
    /// <remarks>
    /// <para>Normally, only generators which derive from SeedableRng&lt;TInteg&gt; would
    /// inherit this interface. See also IConcordantRng.</para>
    /// <seealso cref="Rng"/>
    /// <seealso cref="SeedableRng&lt;TInteg&gt;"/>
    /// <seealso cref="IConcordantRng"/>
    /// <seealso cref="IJumpableRng"/>
    /// </remarks>
    public interface IClonableRng
    {
        /// <summary>
        /// Creates a clone of the pseudo random generator with a "deep" copy of its internal
        /// state.
        /// <para>A newly cloned generator will produce the same results as the original,
        /// provided they are called in the same way.</para>
        /// <para>When implementing IClonableRng.CloneInstance(), call Rng.CloneBaseRng()
        /// to create the initial instance with a "deep" copy all field values in the
        /// Rng base class.</para>
        /// </summary>
        Rng CloneInstance();
    }
}