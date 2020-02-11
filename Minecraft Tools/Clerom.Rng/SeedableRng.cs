//---------------------------------------------------------------------------
// PROJECT      : CLEROM.RNG
// COPYRIGHT    : Kuiper 2016
// WEBSITE      : https://kuiper.zone/clerom-rng
// LICENSE      : https://opensource.org/licenses/MIT
//---------------------------------------------------------------------------

using Kuiper.Clerom;
using Kuiper.Clerom.Internal;
using System;
using System.Security.Cryptography;

namespace Kuiper.Clerom
{
    /// <summary>
    /// Abstract base class for all seedable random number generators.
    /// </summary>
    /// <remarks>
    /// <para>SeedableRng&lt;TInteg&gt; is a "generic" abstract class which inherits
    /// the Rng base class. The TInteg type must be one of the standard integer types,
    /// namely int, uint, ulong etc.</para>
    /// <para>SeedableRng provides a common seeding "interface". This class implements both a means
    /// to generate new random seed values and to randomize the generator. Concrete subclasses
    /// need only implement the SetSeed(TInteg[]) method and the SeedLength property.</para>
    /// <para>For consistency and efficiency, it is recommended that the default constructor
    /// of a subclass put the generator into a known starting state, rather than a random
    /// state. An additional constructor with an option to randomize may easily be provided.</para>
    /// <para>Seedable generators may also optionally inherit from one or more of the
    /// following interfaces: IClonableRng, IConcordantRng, IJumpableRng.</para>
    /// <seealso cref="Rng"/>
    /// <seealso cref="IClonableRng"/>
    /// <seealso cref="IConcordantRng"/>
    /// <seealso cref="IJumpableRng"/>
    /// </remarks>
    public abstract class SeedableRng<TInteg> : Rng
        where TInteg : struct
    {
        private static readonly int TypeSize =
            CleromUtil.GenericIntegralSize(typeof(TInteg), true);

        /// <summary>
        /// Subclasses must supply 'randMax', the maximum possible value returned by the
        /// implementation of the Rng.Next() method.
        /// </summary>
        /// <param name="randMax">Maximum value returned by Next()</param>
        public SeedableRng(ulong randMax)
            : base(randMax)
        {
        }

        /// <summary>
        /// See Rng.IsSeedable.
        /// </summary>
        public override bool IsSeedable 
        {
            get { return true; }
        }

        /// <summary>
        /// A convenience method which returns the Type object pertaining to the TInteg
        /// integer type, namely: typeof(TInteg).
        /// </summary>
        public Type SeedIntegType
        {
            get { return typeof(TInteg); }
        }

        /// <summary>
        /// A convenience method which returns the size, in bytes, of the TInteg type.
        /// <para>For SeedableRng&lt;ulong&gt;, the result would be 8, for example.</para>
        /// </summary>
        public int SeedIntegSize
        {
            get { return TypeSize; }
        }

        /// <summary>
        /// The length of the array needed to seed the generator in units of the TInteg type.
        /// <para>This property is abstract and is to be implemented in the algorithm
        /// subclass. The value should be non-zero and remain constant.</para>
        /// </summary>
        public abstract int SeedLength
        {
            get;
        }

        /// <summary>
        /// Sets the generator to a new state using an array of integer seed values.
        /// <para>The type TInteg is an integer type (i.e. uint, ulong etc.), as defined by the
        /// generator subclass.</para>
        /// <para>If 'seed' is null, the generator will be reset to its starting state.
        /// Otherwise, seed should be at least SeedLength items in length.</para>
        /// <para>This method is abstract and is to be implemented in the algorithm subclass.
        /// Whether or not an array shorter than SeedLength may be used to seed the generator
        /// is implementation dependent.</para>
        /// <para>The suggested behavior for a zero length array is to do nothing. For an
        /// array that is short, but not empty, SetSeed(TInteg[]) should throw ArgumentException
        /// if the generator cannot be seeded. Implementations of SetSeed(TInteg[]) must not
        /// throw an exception if seed is null.</para>
        /// <para>When implementing this method, the subclass should call Rng.ResetCache()
        /// to ensure that results from all Rng routines are repeatable after re-seeding.</para>
        /// </summary>
        /// <param name="seed">Array of SeedLength values (null resets)</param>
        public abstract void SetSeed(TInteg[] seed);

        /// <summary>
        /// Sets the generator to a new state, defined by a single 64-bit integer seed value.
        /// <para>This overloaded variant of SetSeed() provides a convenient means to seed
        /// any generator with a single integer value.</para>
        /// <para>Its behavior is as follows:</para>
        /// <para>If SeedLength is 1, the seed value will be cast to the corresponding TInteg
        /// type, copied to an array of length 1, and passed directly to SetSeed(TInteg[]).</para>
        /// <para>If SeedLength is larger than 1, the integer seed will be "expanded" to an array
        /// of SeedLength values, which will then be passed to SetSeed(TInteg[]).</para>
        /// <para>The number of possible seed values using this method may, therefore, be
        /// significantly less than that supported by the SetSeed(TInteg[]) array variant.</para>
        /// <para>Seeding with this method or, indeed, with any single integer value, should
        /// not be considered suitable for cryptographic use.</para>
        /// <para>Furthermore, seed expansions are to be considered implementation dependent,
        /// in the sense that the implementation may potentially change with some future update
        /// to the library.</para>
        /// <para>Currently, expansions are performed using the SPLITMIX64 generator which,
        /// itself, has a 64-bit seed type. If you require future-proof repeatability,
        /// override this method with your own.</para>
        /// </summary>
        /// <param name="seed">Integer seed value</param>
        public virtual void SetSeed(ulong seed)
        {
            if (SeedLength == 1)
            {
                var arr = new TInteg[1];
                arr[0] = (TInteg)(dynamic)seed;
                SetSeed(arr);
            }
            else
            {
                SplitMix64 temp = new SplitMix64();
                temp.SetSeed(seed);
                byte[] bs = temp.GetBytes(TypeSize * SeedLength);
                SetSeed(CleromUtil.ConvertBytesToArray<TInteg>(bs));
            }
        }

        /// <summary>
        /// Same as SetSeed(ulong), but provided for convenience. The value is simply
        /// cast to the ulong type.
        /// </summary>
        /// <param name="seed">Integer seed value</param>
        public void SetSeed(long seed)
        {
            SetSeed((ulong)seed);
        }

        /// <summary>
        /// Randomizes the internal generator state.
        /// <para>It calls SetSeed(TInteg[]) with the result of GenerateSeed().</para>
        /// </summary>
        public void Randomize()
        {
            SetSeed(GenerateSeed());
        }

        /// <summary>                                                                               
        /// Generates a new random seed of SeedLength values for use with seeding the generator,
        /// but does not modify the state of the generator itself. It is called by Randomize().
        /// <para>The seed is generated by the underlying OS via the RNGCryptoServiceProvider
        /// class. It may be overridden to generate seeds according to custom requirements.</para>
        /// </summary>
        public virtual TInteg[] GenerateSeed()
        {
            TInteg[] rslt = null;
            byte[] bytes = new byte[SeedLength * TypeSize];

            if (bytes.Length > 0)
            {
                RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
                crypto.GetBytes(bytes);
                rslt = CleromUtil.ConvertBytesToArray<TInteg>(bytes);
            }

            return rslt;
        }

        /// <summary>
        /// Calls Next() 'count' times to discard output and roll the internal state forward.
        /// <para>It may be overridden if required.</para>
        /// </summary>
        /// <param name="count">Number of outputs to discard</param>
        public virtual void Discard(long count)
        {
            while (--count > -1)
            {
                Next();
            }
        }

        /// <summary>
        /// Creates a new instance of the generator class seeded with the supplied array.
        /// <para>If seed is null, the new generator will be in its default state.</para>
        /// <para>See SetSeed(TInteg[]) for more information.</para>
        /// </summary>
        /// <param name="seed">Array of SeedLength values (null sets default)</param>
        public Rng NewInstance(TInteg[] seed)
        {
            var obj = (SeedableRng<TInteg>)NewInstance();
            if (seed != null) obj.SetSeed(seed);
            return obj;
        }

        /// <summary>
        /// Creates a new instance of the generator class seeded with the supplied
        /// 64-bit integer value.
        /// <para>See SetSeed(ulong) for more information.</para>
        /// </summary>
        /// <param name="seed">Integer seed value</param>
        public Rng NewInstance(ulong seed)
        {
            var obj = (SeedableRng<TInteg>)NewInstance();
            obj.SetSeed(seed);
            return obj;
        }

        /// <summary>
        /// Same as NewInstance(ulong), but provided for convenience. The value is simply
        /// cast to the ulong type.
        /// </summary>
        /// <param name="seed">Integer seed value</param>
        public Rng NewInstance(long seed)
        {
            return NewInstance((ulong)seed);
        }

        /// <summary>
        /// Creates a new instance of the generator class with an option to randomize its state.
        /// <para>If randomize is true, Randomize() will be called on the new instance,
        /// otherwise it will be in its default state.</para>
        /// </summary>
        /// <param name="randomize">Randomize the new generator</param>
        public Rng NewInstance(bool randomize)
        {
            var obj = (SeedableRng<TInteg>)NewInstance();
            if (randomize) obj.Randomize();
            return obj;
        }

        /// <summary>
        /// Creates a new array of type TInteg with SeedLength items. All element values will be 0.
        /// <para>This method is provided for convenience. Do not use it to seed a generator
        /// directly; use GenerateSeed() to create an array of random values.</para>
        /// </summary>
        public TInteg[] NewSeedArray()
        {
            return new TInteg[SeedLength];
        }

        /// <summary>
        /// Creates a new array of type TInteg with SeedLength items. All elements will be
        /// set to 'value'.
        /// </summary>
        /// <param name="value">Element value</param>
        public TInteg[] NewSeedArray(TInteg value)
        {
            var rslt = new TInteg[SeedLength];

            for (int n = 0; n < rslt.Length; ++n)
            {
                rslt[n] = value;
            }

            return rslt;
        }

    }

}

