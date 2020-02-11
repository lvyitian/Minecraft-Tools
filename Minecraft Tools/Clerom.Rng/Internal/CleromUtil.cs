//---------------------------------------------------------------------------
// PROJECT      : CLEROM.RNG
// COPYRIGHT    : Kuiper 2016
// WEBSITE      : https://kuiper.zone/clerom-rng
// LICENSE      : https://opensource.org/licenses/MIT
//---------------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;

namespace Kuiper.Clerom.Internal
{
    static class CleromUtil
    {
        private const string InvalidTypeMessage = "The array must be of integer type " + 
            "(i.e. byte, int, long etc.).";

        public static int GenericIntegralSize(Type t, bool throwException,
            bool allowUnsigned = true, bool allowSigned = true)
        {
            // An internal utility method which gives the size of an integer type
            // (i.e. byte = 1, int = 4, ulong = 8 etc.). If the type t is invalid,
            // ArgumentException will be thrown if throwException is true. Otherwise,
            // the result will be 0.

            // This also possible with Marshal.SizeOf(). However, I want the
            // type checking behavior, and I wonder if this will be faster? 
            if ((allowUnsigned && t.Equals(typeof(ulong))) ||
                (allowSigned && t.Equals(typeof(long))))
            {
                return sizeof(ulong);
            }
            else
            if ((allowUnsigned && t.Equals(typeof(uint))) ||
                (allowSigned && t.Equals(typeof(int))))
            {
                return sizeof(uint);
            }
            else
            if ((allowUnsigned && t.Equals(typeof(byte))) ||
                (allowSigned && t.Equals(typeof(sbyte))))
            {
                return sizeof(byte);
            }
            else
            if ((allowUnsigned && t.Equals(typeof(ushort))) ||
                (allowSigned && t.Equals(typeof(short))))
            {
                return sizeof(ushort);
            }

            if (throwException)
            {
                throw new ArgumentException("T", InvalidTypeMessage);
            }

            return 0;
        }

        public static T[] ConvertBytesToArray<T>(byte[] bytes)
             where T : struct
        {
            // Converts a byte array to integer array of type T.
            // Any extra bytes are discarded. Result is endian dependent.
            if (bytes != null)
            {
                int elemSize = GenericIntegralSize(typeof(T), true);

                int len = bytes.Length / elemSize;
                var rslt = new T[len];
                Buffer.BlockCopy(bytes, 0, rslt, 0, len * elemSize);
                return rslt;
            }

            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsArrayZero<T>(T[] items)
            where T : struct, IEquatable<T>
        {
            // Returns true if all values are 0.
            for (int n = 0; n < items.Length; ++n)
            {
                if (!items.Equals(0)) return false;
            }

            return (items.Length > 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int BitSize(ulong x)
        {
            // Calculate the number of bits needed
            // for x. I.e, for 0x70, it would be 7.

            // Shortcuts to common values.
            if (x == UInt64.MaxValue) return 64;
            if (x == UInt32.MaxValue) return 32;
            if (x == Int32.MaxValue) return 31;

            if (x != 0)
            {
                int bits = 64;
                while ((x >> --bits) == 0);

                return bits + 1;
            }

            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong SwapEndian(ulong x)
        {
            return (x & 0x00000000000000FFUL) << 56 | (x & 0x000000000000FF00UL) << 40 |
                    (x & 0x0000000000FF0000UL) << 24 | (x & 0x00000000FF000000UL) << 8 |
                    (x & 0x000000FF00000000UL) >> 8 | (x & 0x0000FF0000000000UL) >> 24 |
                    (x & 0x00FF000000000000UL) >> 40 | (x & 0xFF00000000000000UL) >> 56;
        }

    }
}
