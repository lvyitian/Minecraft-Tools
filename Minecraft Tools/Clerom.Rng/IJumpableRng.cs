//---------------------------------------------------------------------------
// PROJECT      : CLEROM.RNG
// COPYRIGHT    : Kuiper 2016
// WEBSITE      : https://kuiper.zone/clerom-rng
// LICENSE      : https://opensource.org/licenses/MIT
//---------------------------------------------------------------------------

namespace Kuiper.Clerom
{
    /// <summary>
    /// An interface for generators which implement a "jump ahead" or "fast jump" function.
    /// </summary>
    /// <remarks>
    /// <para>A "jump ahead" function provides an efficient means for a random number generator
    /// to advance its internal state by a large number of equivalent calls to Next() or
    /// Discard(). It is used to ensure that generators, initially seeded with the
    /// same value, do not produce the same or overlapping results. This is useful in
    /// scenarios where parallelism and repeatability are both important.</para>
    /// <para>The jump size is generator dependent, and is described by the JumpSig and JumpExp
    /// properties, such that it equals JumpSig^JumpExp. The actual jump size value, however,
    /// may exceed the maximum range of integer types. For example, the jump size of
    /// Xorshift1024* algorithm is 2^512.</para>
    /// <para>Normally, only generators which derive from SeedableRng&lt;TInteg&gt; would
    /// inherit this interface.</para>
    /// <seealso cref="Rng"/>
    /// <seealso cref="SeedableRng&lt;TInteg&gt;"/>
    /// <seealso cref="IClonableRng"/>
    /// <seealso cref="IConcordantRng"/>
    /// </remarks>
    public interface IJumpableRng
    {
        /// <summary>
        /// The number of equivalent calls to Next() performed by Jump() is described
        /// by JumpSig^JumpExp.
        /// <para>The actual jump size value, however, may exceed the maximum range of integer
        /// types. For example, the jump size of Xorshift1024* algorithm is 2^512.</para>
        /// </summary>
        ulong JumpSig
        {
            get;
        }

        /// <summary>
        /// See JumpSig for information.
        /// </summary>
        int JumpExp
        {
            get;
        }

        /// <summary>
        /// Performs a "fast jump", equivalent to calling Next() JumpSig^JumpExp
        /// times.
        /// </summary>
        void Jump();
    }
}
