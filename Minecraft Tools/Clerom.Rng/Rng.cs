//---------------------------------------------------------------------------
// PROJECT      : CLEROM.RNG
// COPYRIGHT    : Kuiper 2016
// WEBSITE      : https://kuiper.zone/clerom-rng
// LICENSE      : https://opensource.org/licenses/MIT
//---------------------------------------------------------------------------

using Kuiper.Clerom.Internal;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Kuiper.Clerom
{
    /// <summary>
    /// Abstract base class for all random number generators in the CLEROM.RNG library.
    /// </summary>
    /// <remarks>
    /// <para>The Rng base class provides a common implementation for a range of routines
    /// relating to random number generation. All random results ultimately derive from a
    /// single abstract method, Next(), which represents the native integer generation routine
    /// of the underlying random number generator algorithm.</para>
    /// <para>Next() returns an unsigned value between 0, and Rng.RandMax (inclusive),
    /// where both Next() and RandMax are defined by the algorithm implementation. For pseudo
    /// random generators, its output can be expected to match known test vectors from a
    /// defined starting state. Results from all other methods should be considered
    /// implementation dependent, unless otherwise stated.</para>
    /// <para>The Rng base class, by itself, does not support seeding. Therefore, it can
    /// be directly inherited by concrete implementations of hardware based or unseeded
    /// cryptographic generators. Seedable generator implementations should inherit from
    /// SeedableRng&lt;TInteg&gt; instead.</para>
    /// <para>Concrete implementations are required to override Next() and AlgorithmName
    /// property, and provide the value of RandMax to the Rng constructor. Other methods
    /// in the base class have implementations, but a number of them can be overridden if
    /// required.</para>
    /// <seealso cref="SeedableRng&lt;TInteg&gt;"/>
    /// <seealso cref="IClonableRng"/>
    /// <seealso cref="IConcordantRng"/>
    /// <seealso cref="IJumpableRng"/>
    /// </remarks>
    public abstract class Rng
    {
        private const ulong I3E64Const = 0x3FF0000000000000UL;
        private const string CommonRangeError = "Invalid range error.";
        private const string LibUrl = "https://kuiper.zone/clerom-rng";

        // All fields must be copy/compared in both CloneBaseRng() and IsBaseConcordant()
        // methods. Additionally, if reference fields are ever introduced, all CloneInstance()
        // implementations in the Rng classes need to be checked, as I cheated and used
        // MemberwiseClone() on some them for performance reasons. These would need replacing
        // with CloneBaseRng(). Hint: Don't introduce reference fields here, if it can be avoided.
        private ulong  nativeMax;
        private bool   native64;
        private bool   native32;
        private int    shiftBits;
        private ulong  shiftMax;

        // All cache fields must additionally be reset in ResetCache().
        private ulong  flipCache;
        private int    flipCacheSize;
        private uint   next32Cache;
        private bool   next32Flag;
        private double gaussCache;
        private bool   gaussFlag;

        /// <summary>
        /// Subclasses must supply 'randMax', the maximum possible value returned by the
        /// implementation of the Next() method.
        /// </summary>
        /// <param name="randMax">Maximum value returned by Next()</param>
        public Rng(ulong randMax)
        {
            if (randMax == 0)
            {
                throw new ArgumentException("randMax", "RandMax cannot be zero!");
            }

            // Set read-only values
            nativeMax = randMax;
            native64 = (randMax == UInt64.MaxValue);
            native32 = (randMax == UInt32.MaxValue);

            // Get number of bits to use from Next().
            // A RandMax of 2^31-1 gives us 31.
            shiftBits = CleromUtil.BitSize(randMax);
            shiftMax = UInt64.MaxValue >> (64 - shiftBits);

            if (randMax != shiftMax)
            {
                --shiftBits;
                shiftMax >>= 1;
            }

            // Initialize cache values
            ResetCache();

            // Useful for confirming properties when debugging
            /*
            Console.WriteLine();
            Console.WriteLine("NAME       : " + AlgorithmName);
            Console.WriteLine("RandMax    : " + randMax);
            Console.WriteLine("RandMax    : 0x" + randMax.ToString("X16"));
            Console.WriteLine("Native32   : " + native32);
            Console.WriteLine("Native64   : " + native64);
            Console.WriteLine("BitShift  : " + shiftBits);
            Console.WriteLine();
            */
        }

        /// <summary>
        /// Returns the library name. This is the title attribute from the CLEROM.RNG assembly.
        /// </summary>
        public static string LibraryName
        {
            get
            {
                return ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(
                    typeof(Rng).Assembly, typeof(AssemblyTitleAttribute), false)).Title;
            }
        }

        /// <summary>
        /// Returns the CLEROM.RNG library version number from the assembly.
        /// </summary>
        public static string LibraryVersion
        {
            get
            {
                return typeof(Rng).Assembly.GetName().Version.ToString();
            }
        }

        /// <summary>
        /// Returns the CLEROM.RNG library information web page URL.
        /// </summary>
        public static string LibraryWebPage
        {
            get { return LibUrl; }
        }

        /// <summary>
        /// The formal or common name of the generator algorithm, i.e. "MT19937-32".
        /// <para>This property is abstract, and should be implemented by the
        /// generator subclass.</para>
        /// </summary>
        public abstract string AlgorithmName
        {
            get;
        }

        /// <summary>
        /// The maximum possible value of the algorithm's native generation routine,
        /// Next(), inclusive.
        /// </summary>
        public ulong RandMax
        {
            get { return nativeMax; }
        }

        /// <summary>
        /// The value is true if the generator subclass inherits from the SeedableRng class.
        /// <para>This property is provided for convenience, as the C# "is" keyword cannot
        /// be used on generic SeedableRng type unless the seed integer type is known.</para>
        /// </summary>
        public virtual bool IsSeedable
        {
            get { return false; }
        }

        /// <summary>
        /// Next() represents the native integer generation routine of the underlying random
        /// number generator. All other values are derived from this.
        /// <para>The result is an unsigned integer in the range 0 to RandMax, inclusive,
        /// where the value of RandMax is algorithm dependent.</para>
        /// <para>For pseudo random generators, its output can be expected to match known
        /// test vectors from a defined starting state. Results from all other methods
        /// should be considered implementation dependent, unless otherwise stated.</para>
        /// <para>This method is abstract in the Rng base class.</para>
        /// </summary>
        public abstract ulong Next();

        /// <summary>
        /// Generates a random 32-bit unsigned integer uniformly distributed in the range 0 to
        /// 2^32-1, inclusive, irrespective of the output range of Next(), the algorithm's native
        /// routine.
        /// <para>Although the result is derived from Next(), do not expect values to necessarily
        /// match those of Next(). You should always consider results of this method to be
        /// implementation dependent. If you need future-proof repeatability call Next()
        /// instead or, alternatively, override this method in a subclass in order to control
        /// result values.</para>
        /// <para>If you override, ensure all 32 bits are randomly populated without bias
        /// and that no Rng method other than Next() is called.</para>
        /// </summary>
        public virtual uint Next32()
        {
            // Split a 64-bit value into 2, and toggle between a cached value.
            if (!(next32Flag = !next32Flag))
            {
                return next32Cache;
            }

            ulong temp;

            if (native64)
            {
                temp = Next();
            }
            else
            if (native32)
            {
                temp = (Next() << 32) | Next();
            }
            else
            {
                // Build unbiased 64-bit for non 32/64
                // bit types. Relatively slow.
                temp = GetUnbiased64();
            }

            next32Cache = (uint)temp;
            return (uint)(temp >> 32);
        }

        /// <summary>
        /// Generates a random 64-bit unsigned integer uniformly distributed in the range 0 to
        /// 2^64-1, inclusive, irrespective of the output range of Next(), the algorithm's native
        /// routine.
        /// <para>Although the result is derived from Next(), do not expect values to necessarily
        /// match those of Next(). You should always consider results of this method to be
        /// implementation dependent. If you need future-proof repeatability call Next()
        /// instead or, alternatively, override this method in a subclass in order to control
        /// result values.</para>
        /// <para>If you override, ensure all 64 bits are randomly populated without bias
        /// and that no Rng method other than Next() is called.</para>
        /// </summary>
        public virtual ulong Next64()
        {
            if (native64)
            {
                return Next();
            }
            
            if (native32)
            {
                return (Next() << 32) | Next();
            }

            // Build unbiased 64-bit for non 32/64
            // bit types. Relatively slow.
            return GetUnbiased64();
        }

        /// <summary>
        /// Generates a random 32-bit signed integer uniformly distributed in the
        /// range 0 (inclusive) to 'max' (exclusive).
        /// <para>The max value must be greater than 0.</para>
        /// </summary>
        /// <param name="max">Maximum value</param>
        public int GetInt32(int max)
        {
            if (max > 0) return (int)GetUInt32((uint)max, false);
            else throw new ArgumentException("max", CommonRangeError);
        }

        /// <summary>
        /// Generates a random 32-bit signed integer uniformly distributed in the
        /// range 0 (inclusive) to 'max' (inclusive if 'maxIncl' is true).
        /// <para>The max value must be greater than 0. It may also be 0 if
        /// maxIncl is true, but the result will always be 0 in this case.</para>
        /// </summary>
        /// <param name="max">Maximum value</param>
        /// <param name="maxIncl">Maximum is inclusive</param>
        public int GetInt32(int max, bool maxIncl)
        {
            if (max >= 0) return (int)GetUInt32((uint)max, maxIncl);
            else throw new ArgumentException("max", CommonRangeError);
        }

        /// <summary>
        /// Generates a random 32-bit signed integer uniformly distributed in the
        /// range 'min' (inclusive) to 'max' (exclusive).
        /// <para>One or both values may be negative provided min is less than max.</para>
        /// </summary>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        public int GetInt32(int min, int max)
        {
            if (max > min) return min + (int)GetUInt32((uint)(max - min), false);
            else throw new ArgumentException(CommonRangeError);
        }

        /// <summary>
        /// Generates a random 32-bit signed integer uniformly distributed in the
        /// range 'min' (inclusive) to 'max' (inclusive if 'maxIncl' is true).
        /// <para>One or both values may be negative provided min is less than max.</para>
        /// <para>If maxIncl is true, min and max may also be equal, but the result
        /// will always that of min.</para>
        /// </summary>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <param name="maxIncl">Maximum is inclusive</param>
        public int GetInt32(int min, int max, bool maxIncl)
        {
            if (max >= min) return min + (int)GetUInt32((uint)(max - min), maxIncl);
            else throw new ArgumentException(CommonRangeError);
        }

        /// <summary>
        /// Generates a random 32-bit unsigned integer uniformly distributed in the
        /// range 0 (inclusive) to 'max' (exclusive).
        /// <para>The max value must be greater than 0.</para>
        /// </summary>
        /// <param name="max">Maximum value</param>
        public uint GetUInt32(uint max)
        {
            return GetUInt32(max, false);
        }

        /// <summary>
        /// Generates a random 32-bit unsigned integer uniformly distributed in the
        /// range 0 (inclusive) to 'max' (inclusive if 'maxIncl' is true).
        /// <para>The max value must be greater than 0. It may also be 0 if
        /// maxIncl is true, but the result will always be 0 in this case.</para>
        /// <para>This method is virtual and may be overridden for specific requirements
        /// (normally there should be no need to do so). All variants of GetInt32()
        /// and GetUInt32() will call this, and range shift appropriately.</para>
        /// </summary>
        /// <param name="max">Maximum value</param>
        /// <param name="maxIncl">Maximum is inclusive</param>
        public virtual uint GetUInt32(uint max, bool maxIncl)
        {
            // We advance the generator if max is 0 and maxIncl is true. It also
            // spares an additional check in order to return a constant value.
            if (max != 0 || maxIncl)
            {
                if (max < UInt32.MaxValue || !maxIncl)
                {
                    if (maxIncl) ++max;
                    return (uint)(((ulong)max * Next32()) >> 32);
                }

                // Max range
                return Next32();
            }

            throw new ArgumentException(CommonRangeError);
        }

        /// <summary>
        /// Generates a random 32-bit unsigned integer uniformly distributed in the
        /// range 'min' (inclusive) to 'max' (exclusive).
        /// <para>The max value must be greater than min.</para>
        /// </summary>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        public uint GetUInt32(uint min, uint max)
        {
            if (max > min) return min + GetUInt32(max - min, false);
            else throw new ArgumentException(CommonRangeError);
        }

        /// <summary>
        /// Generates a random 32-bit unsigned integer uniformly distributed in the
        /// range 'min' (inclusive) to 'max' (inclusive if 'maxIncl' is true).
        /// <para>If maxIncl is true, min and max may also be equal, but the result
        /// will always that of min. Otherwise, max must be greater than min.</para>
        /// </summary>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <param name="maxIncl">Maximum is inclusive</param>
        public uint GetUInt32(uint min, uint max, bool maxIncl)
        {
            if (max >= min) return min + GetUInt32(max - min, maxIncl);
            else throw new ArgumentException(CommonRangeError);
        }

        /// <summary>
        /// Generates a random 64-bit signed integer uniformly distributed in the
        /// range 0 (inclusive) to 'max' (exclusive).
        /// <para>The max value must be greater than 0.</para>
        /// </summary>
        /// <param name="max">Maximum value</param>
        public long GetInt64(long max)
        {
            if (max > 0) return (long)GetUInt64((ulong)max, false);
            else throw new ArgumentException("max", CommonRangeError);
        }

        /// <summary>
        /// Generates a random 64-bit signed integer uniformly distributed in the
        /// range 0 (inclusive) to 'max' (inclusive if 'maxIncl' is true).
        /// <para>The max value must be greater than 0. It may also be 0 if
        /// maxIncl is true, but the result will always be 0 in this case.</para>
        /// </summary>
        /// <param name="max">Maximum value</param>
        /// <param name="maxIncl">Maximum is inclusive</param>
        public long GetInt64(long max, bool maxIncl)
        {
            if (max >= 0) return (long)GetUInt64((ulong)max, maxIncl);
            else throw new ArgumentException("max", CommonRangeError);
        }

        /// <summary>
        /// Generates a random 64-bit signed integer uniformly distributed in the
        /// range 'min' (inclusive) to 'max' (exclusive).
        /// <para>One or both values may be negative provided min is less than max.</para>
        /// </summary>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        public long GetInt64(long min, long max)
        {
            if (max > min) return min + (long)GetUInt64((ulong)(max - min), false);
            else throw new ArgumentException(CommonRangeError);
        }

        /// <summary>
        /// Generates a random 64-bit signed integer uniformly distributed in the
        /// range 'min' (inclusive) to 'max' (inclusive if 'maxIncl' is true).
        /// <para>One or both values may be negative provided min is less than max.</para>
        /// <para>If maxIncl is true, min and max may also be equal, but the result
        /// will always that of min.</para>
        /// </summary>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <param name="maxIncl">Maximum is inclusive</param>
        public long GetInt64(long min, long max, bool maxIncl)
        {
            if (max >= min) return min + (long)GetUInt64((ulong)(max - min), maxIncl);
            else throw new ArgumentException(CommonRangeError);
        }

        /// <summary>
        /// Generates a random 64-bit unsigned integer uniformly distributed in the
        /// range 0 (inclusive) to 'max' (exclusive).
        /// <para>The max value must be greater than 0.</para>
        /// </summary>
        /// <param name="max">Maximum value</param>
        public ulong GetUInt64(ulong max)
        {
            return GetUInt64(max, false);
        }

        /// <summary>
        /// Generates a random 64-bit unsigned integer uniformly distributed in the
        /// range 0 (inclusive) to 'max' (inclusive if 'maxIncl' is true).
        /// <para>The max value must be greater than 0. It may also be 0 if
        /// maxIncl is true, but the result will always be 0 in this case.</para>
        /// <para>This method is virtual and may be overridden for specific requirements
        /// (normally there should be no need to do so). All variants of GetInt64()
        /// and GetUInt64() will call this, and range shift appropriately.</para>
        /// </summary>
        /// <param name="max">Maximum value</param>
        /// <param name="maxIncl">Maximum is inclusive</param>
        public virtual ulong GetUInt64(ulong max, bool maxIncl)
        {
            if (max <= UInt32.MaxValue)
            {
                return GetUInt32((uint)max, maxIncl);
            }
            
            // We advance the generator if max is 0 and maxIncl is true. It also
            // spares an additional check in order to return a constant value.
            if (max != 0 || maxIncl)
            {
                if (max != UInt64.MaxValue || !maxIncl)
                {
                    if (maxIncl) ++max;
                    ulong rxh = max >> 32;
                    ulong rxl = max & UInt32.MaxValue;

                    ulong rav = Next64();
                    ulong ravh = rav >> 32;
                    ulong ravl = (uint)rav;

                    return ((rxl * ravh) >> 32) + ((rxh * ravl) >> 32 ) + (rxh * ravh);
                
                    // Will leave this for reference. It uses the common
                    // division method but is somewhat slower as division
                    // is expensive.
                    // if (maxIncl) ++range;
                    // ulong div64 = UInt64.MaxValue / range;
                    // ulong rav64 = Next64() / div64;
                    // while (rav64 >= range) { rav64 = Next64() / div64; }
                    // return rav64;
                }
                       
                // Max range
                return Next64();
            }

            throw new ArgumentException(CommonRangeError);
        }

        /// <summary>
        /// Generates a random 64-bit unsigned integer uniformly distributed in the
        /// range 'min' (inclusive) to 'max' (exclusive).
        /// <para>The max value must be greater than min.</para>
        /// </summary>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        public ulong GetUInt64(ulong min, ulong max)
        {
            if (max > min) return min + GetUInt64(max - min, false);
            else throw new ArgumentException(CommonRangeError);
        }

        /// <summary>
        /// Generates a random 64-bit unsigned integer uniformly distributed in the
        /// range 'min' (inclusive) to 'max' (inclusive if 'maxIncl' is true).
        /// <para>If maxIncl is true, min and max may also be equal, but the result will
        /// always that of min. Otherwise, max must be greater than min.</para>
        /// </summary>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <param name="maxIncl">Maximum is inclusive</param>
        public ulong GetUInt64(ulong min, ulong max, bool maxIncl)
        {
            if (max >= min) return min + GetUInt64(max - min, maxIncl);
            else throw new ArgumentException(CommonRangeError);
        }

        /// <summary>
        /// Generates a random double value uniformly distributed in the
        /// range 0 (inclusive) to 1.0 (exclusive).
        /// <para>This method is virtual and may be overridden for specific requirements
        /// (normally there should be no need to do so). All other routines which return a
        /// double type will normally call this method, and range shift appropriately.</para>
        /// </summary>
        public virtual double GetDouble()
        {
            // Double type must be 64-bit IEEE format.
            // Microsoft define double to be this as part of standard.
            // See: http://xorshift.di.unimi.it
            return BitConverter.Int64BitsToDouble((long)(I3E64Const | Next64() >> 12)) - 1;
        }

        /// <summary>
        /// Generates a random double value uniformly distributed in the range
        /// 'min' (inclusive) to 'max' (exclusive).
        /// <para>The max value must be greater than min. The maximum permissible
        /// range between min and max is given by double.MaxValue.</para>
        /// <para>The result is checked to ensure that no range combination gives
        /// a value equal to max.</para>
        /// </summary>
        /// <param name="min">Minimum (inclusive)</param>
        /// <param name="max">Maximum (exclusive)</param>
        public double GetDouble(double min, double max)
        {
            if (max > min)
            {
                double rslt = min + GetDouble() * (max - min);
                
                while(rslt >= max)
                {
                    rslt = min + GetDouble() * (max - min);
                }

                return rslt;
            }

            throw new ArgumentException("max", CommonRangeError);
        }

        /// <summary>
        /// Generates a random double value uniformly distributed in the
        /// open interval 0 (exclusive) to 1.0 (exclusive).
        /// <para>The result is checked to ensure that it never equals 0.</para>
        /// </summary>
        public double GetOpenDouble()
        {
            double rslt = GetDouble();
            while (rslt == 0) rslt = GetDouble();
            return rslt;
        }

        /// <summary>
        /// Generates a random double value uniformly distributed in the open
        /// interval 'min' (exclusive) to 'max' (exclusive).
        /// <para>The max value must be greater than min. The maximum permissible
        /// range between min and max is given by double.MaxValue.</para>
        /// <para>The result is checked to ensure that no range combination gives
        /// a value equal to min or max.</para>
        /// </summary>
        /// <param name="min">Minimum (exclusive)</param>
        /// <param name="max">Maximum (exclusive)</param>
        public double GetOpenDouble(double min, double max)
        {
            if (max > min)
            {
                double rslt = min + GetDouble() * (max - min);
                
                while(rslt >= max || rslt <= min)
                {
                    rslt = min + GetDouble() * (max - min);
                }

                return rslt;
            }

            throw new ArgumentException("max", CommonRangeError);
        }

        /// <summary>
        /// Returns a "normally" distributed random double value, with a mean of 0 and
        /// standard deviation of +1.0.
        /// <para>The Box-Muller transformation is to generate the result.</para>
        /// <para>This method is virtual and may be overridden for specific requirements.</para>
        /// </summary>
        public virtual double GetStdNormal()
        {
            if (!(gaussFlag = !gaussFlag))
            {
                // Two values are generated, with
                // one being cached for the next call.
                return gaussCache;
            }

            // Polar form of Box-Muller.
            // Ref: http://www.design.caltech.edu/erik/Misc/Gaussian.html
            double r0, r1, w;

            do
            {
                r0 = 2.0 * GetDouble() - 1;
                r1 = 2.0 * GetDouble() - 1;
                w = r0 * r0 + r1 * r1;

            } while (w >= 1 || w <= double.Epsilon);

            w = Math.Sqrt((-2.0 * Math.Log(w)) / w);
            gaussCache = r0 * w;

            return r1 * w;
        }

        /// <summary>
        /// Creates a new byte array and populates it with 'count' random values.
        /// <para>GetBytes() will generally be much faster than populating a byte
        /// array with individual calls to GetInt32() or Next().</para>
        /// <para>However, results are implementation and endian dependent; do not expect
        /// them to align with or match the output of Next().</para>
        /// </summary>
        /// <param name="count">Number of values to generate</param>
        public byte[] GetBytes(int count)
        {
            var rslt = new byte[count];
            GetBytes(rslt, 0, count);
            return rslt;
        }

        /// <summary>
        /// Populates the array with 'count' random byte values.
        /// <para>GetBytes() will generally be much faster than populating a byte
        /// array with individual calls to GetInt32() or Next().</para>
        /// <para>However, results are implementation and endian dependent; do not expect
        /// them to align with or match the output of Next().</para>
        /// </summary>
        /// <param name="dst">Destination array</param>
        public int GetBytes(byte[] dst)
        {
            return GetBytes(dst, 0, dst.Length);
        }

        /// <summary>
        /// Populates the array with 'count' random byte values, starting from the
        /// given 'offset'. If count is -1, values are written from offset to the end
        /// of the array.
        /// <para>GetBytes() will generally be much faster than populating a byte
        /// array with individual calls to GetInt32() or Next().</para>
        /// <para>However, results are implementation and endian dependent; do not expect
        /// them to align with or match the output of Next().</para>
        /// <para>This method is virtual and may be overridden for specific requirements
        /// (normally there should be no need to do so). Overloaded variants will call it.
        /// </para>
        /// </summary>
        /// <param name="dst">Destination array</param>
        /// <param name="offset">Start offset</param>
        /// <param name="count">Number of bytes to generate</param>
        public virtual int GetBytes(byte[] dst, int offset, int count)
        {
            // Buffer size in ulongs (i.e. x8 bytes).
            // Changing this size doesn't seem to make a lot
            // difference to performance, unless very small.
            const int MaxBufferSize = 500;

            // This will throw on bounds error
            count = ArrayBoundsCheck(dst, offset, count);

            int i8Count = count;
            int i64Count = i8Count / sizeof(ulong); 
            if (i8Count % sizeof(ulong) != 0) ++i64Count;

            int bufSize = i64Count;
            if (bufSize > MaxBufferSize) bufSize = MaxBufferSize;

            ulong[] buf = new ulong[bufSize];

            int bufIdx;
            int byteSize = bufSize * sizeof(long);
            if (byteSize > i8Count) byteSize = i8Count;

            do
            {
                bufIdx = 0;   
                do { buf[bufIdx] = Next64(); } while (++bufIdx < bufSize);

                Buffer.BlockCopy(buf, 0, dst, offset, byteSize);

                if ((i8Count -= byteSize) == 0)
                {
                    break;
                }

                offset += byteSize;

                if ((i64Count -= bufSize) < bufSize)
                {
                    // Short last block
                    bufSize = i64Count;
                    byteSize = i8Count;
                }

            } while (true);

            return count;
        }

        /// <summary>
        /// Returns true or false, with equal probability.
        /// </summary>
        public virtual bool Flipper()
        {
            if (--flipCacheSize < 0)
            {
                flipCache = Next64();
                flipCacheSize = 63;
            }

            bool rslt = ((flipCache & 0x01UL) == 1UL);
            flipCache >>= 1;
            return rslt;
        }

        /// <summary>
        /// Shuffles the items in the array according to the Fisher-Yates algorithm.
        /// </summary>
        /// <typeparam name="T">Array item type.</typeparam>
        /// <param name="items">The array to shuffle</param>
        public void Shuffle<T>(T[] items)
        {
            Shuffle<T>(items, 0, items.Length);
        }

        /// <summary>
        /// Shuffles 'count' items in the array, starting from the given offset, according
        /// to the Fisher-Yates algorithm.
        /// <para>If count is -1, items are sorted from offset to the end of the array.</para>
        /// <para>This method is virtual and may be overridden for specific requirements
        /// (normally there should be no need to do so). Overloaded variants will call it.</para>
        /// </summary>
        /// <typeparam name="T">Array item type.</typeparam>
        /// <param name="items">The array to shuffle</param>
        /// <param name="offset">Start offset</param>
        /// <param name="count">Number of items to sort</param>
        public virtual void Shuffle<T>(T[] items, int offset, int count)
        {
            // Using unsigned avoids additional cast in the loop.
            uint ucnt = (uint)ArrayBoundsCheck(items, offset, count);

            T temp;
            int j, end = (int)ucnt + offset;

            // Increment deliberate
            ++ucnt;

            // Above +1 deliberate
            while (--ucnt > 1)
            {
                // The maxIncl=false tallies with initial count increment.
                // It avoids a further in-loop increment in GetUInt32().
                // We use GetUInt32() because this is where the implementation
                // lives, and avoids unnecessary re-direction.
                j = offset + (int)GetUInt32(ucnt, false);

                temp = items[j];
                items[j] = items[--end];
                items[end] = temp;
            }
        }

        /// <summary>
        /// Creates a new instance of the generator class. The new instance will be
        /// in its initial state (i.e. not randomized).
        /// <para>This method is implemented in the base class but may be trivially
        /// overridden in the "final" subclass. Because creating an object instance
        /// with "new" is faster than using a reflection technique, overriding will
        /// generally improve performance of this and its overloaded variants in
        /// SeedableRng which call it.</para>
        /// <para>Moreover, this method requires the generator subclass to have a default
        /// constructor. If this is not the case, NewInstance() must be overridden.</para>
        /// </summary>
        public virtual Rng NewInstance()
        {
            var con = GetType().GetConstructor(Type.EmptyTypes);

            if (con != null)
            {
                return (Rng)con.Invoke(Type.EmptyTypes);
            }

            throw new NotSupportedException("This Rng class type has no default constructor.");
        }

        /// <summary>
        /// Resets internal cache values used by the Rng base class to their initial
        /// zero state. 
        /// <para>The Rng base class will cache certain values to improve performance.
        /// For example, GetStdNormal() is a relatively expensive routine, but it actually
        /// generates two results at once. Therefore, one value is cached so that results
        /// need only be derived once every two calls.</para>
        /// <para>Other values may also be cached in a similar way.</para>
        /// <para>This method should be called when implementing SeedableRng.SetSeed() in
        /// a derived subclass to ensure that results from all Rng routines are repeatable
        /// after re-seeding.</para>
        /// </summary>
        public virtual void ResetCache()
        {
            flipCache = 0;
            flipCacheSize = 0;
            next32Cache = 0;
            next32Flag = false;
            gaussCache = 0;
            gaussFlag = false;
        }

        /// <summary>
        /// A protected method that should be called to create the initial clone
        /// instance when implementing IClonableRng.CloneInstance().
        /// <para>CloneBaseRng() will call NewInstance() to create an instance of
        /// the same type as the subclass. It will also "deep" copy all field values
        /// in the Rng base class to the clone.</para>
        /// <para>It will not copy data from the subclass, however. This must be done
        /// by the CloneInstance() implementation.</para>
        /// </summary>
        protected Rng CloneBaseRng()
        {
            Rng clone = NewInstance();

            // Usually this are defined by the class type, but potentially
            // may be different if the subclass provides a constructor with
            // a RandMax parameter (LcgRng does). So we need to copy them.
            nativeMax = clone.nativeMax;
            native64 = clone.native64;
            native32 = clone.native32;
            shiftBits = clone.shiftBits;

            flipCache = clone.flipCache;
            flipCacheSize = clone.flipCacheSize;
            next32Cache = clone.next32Cache;
            next32Flag = clone.next32Flag;
            gaussCache = clone.gaussCache;
            gaussFlag = clone.gaussFlag;

            return clone;
        }

        /// <summary>
        /// A protected method that should be called when implementing
        /// IConcordantRng.IsConcordant() to compare the internal data of the base class.
        /// <para>IsBaseConcordant() will return false if 'other' is null, or if 'other' is
        /// an object of a different concrete class type (there is no need to repeat this check).
        /// </para>
        /// <para>Moreover, it will also compare internal field values in the base class, and
        /// return true if they have "deep value equality". It does not compare data in the
        /// subclass. This must be done by the IsConcordant() implementation.</para>
        /// <para>If IsBaseConcordant() returns false, then the IsConcordant() implementation
        /// should return false immediately. If IsBaseConcordant() is true, then the internal
        /// state of the subclass should then be compared for "deep" value equality.</para>
        /// </summary>
        /// <param name="other">Generator being compared</param>
        protected bool IsBaseConcordant(Rng other)
        {
            if (other == null || GetType() != other.GetType())
            {
                return false;
            }

            return (
                flipCache == other.flipCache && flipCacheSize == other.flipCacheSize &&
                next32Cache == other.next32Cache && next32Flag == other.next32Flag &&
                gaussCache == other.gaussCache && gaussFlag == other.gaussFlag &&
                nativeMax == other.nativeMax &&
                native64 == other.native64 &&
                native32 == other.native32 &&
                shiftBits == other.shiftBits
                );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ulong GetUnbiased64()
        {
            // Build a 64-bit unbiased integer.
            int bits = 0;
            ulong x, rslt = 0;

            do
            {
                do { x = Next(); } while (x > shiftMax);

                rslt <<= shiftBits;
                rslt |= x;
                                
            } while ((bits += shiftBits) < 64);

            return rslt;
        }

        private static int ArrayBoundsCheck<T>(T[] arr, int offset, int count)
        {
            // Common array bounds check.
            // We allow negative count, and return actual count instead.
            int length = arr.Length;

            if (offset >= length || offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset", "Array offset out of range.");
            }
            
            if (count < 0)
            {
                return length - offset;
            }
            
            if ((long)offset + count > length)
            {
                throw new ArgumentException("count", "Array count exceeds length of the array.");
            }

            return count;
        }

    }
}

