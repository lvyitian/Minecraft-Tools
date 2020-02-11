//---------------------------------------------------------------------------
// PROJECT      : CLEROM.RNG
// COPYRIGHT    : Kuiper 2016
// WEBSITE      : https://kuiper.zone/clerom-rng
// LICENSE      : https://opensource.org/licenses/MIT
//---------------------------------------------------------------------------

namespace Kuiper.Clerom
{
    /// <summary>
    /// An interface for determining whether two generators are not only of the same
    /// concrete type, but also have the same internal state.
    /// </summary>
    /// <remarks>
    /// <para>Do not confuse with Object.Equals(), which provides reference equality.
    /// Furthermore, overriding Object.Equals() so that it provide "value equality" was
    /// deemed unsuitable for CLEROM.RNG classes because random number generators are
    /// reference types, rather than "value types", with highly mutable "hidden" states.
    /// Therefore, the term "concordant" is used in this context to refer to "deep value
    /// equality", which may not otherwise be apparent from the object's public properties.</para>
    /// <para>Normally, only generators which derive from SeedableRng&lt;TInteg&gt; would
    /// inherit this interface. See also IClonableRng.</para>
    /// <seealso cref="Rng"/>
    /// <seealso cref="SeedableRng&lt;TInteg&gt;"/>
    /// <seealso cref="IConcordantRng"/>
    /// <seealso cref="IJumpableRng"/>
    /// </remarks>
    public interface IConcordantRng
    {
        /// <summary>
        /// Returns true if 'other' is a generator of the same concrete type, and has the same
        /// internal state.
        /// <para>Two generators for which IsConcordant() is true will produce identical
        /// results, provided they are called in the same way.</para>
        /// <para>An implementation of IsConcordant() should initially call
        /// Rng.IsBaseConcordant(), which will return false if 'other' is null, or if other
        /// is an object of a different concrete class type (there is no need to repeat this
        /// check).</para>
        /// <para>Moreover, it will also compare internal field values in the base class, and
        /// return true if they have "deep value equality".</para>
        /// <para>If IsBaseConcordant() returns false, then the IsConcordant() implementation
        /// should return false immediately. If IsBaseConcordant() is true, then the internal
        /// state of the subclass should then be compared for "deep" value equality.</para>
        /// </summary>
        /// <param name="other"></param>
        bool IsConcordant(Rng other);
    }
}