//---------------------------------------------------------------------------
// PROJECT      : CLEROM.RNG
// COPYRIGHT    : Kuiper 2016
// WEBSITE      : https://kuiper.zone/clerom-rng
// LICENSE      : https://opensource.org/licenses/MIT
//---------------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Kuiper.Clerom
{
    /// <summary>
    /// SystemCrypto does not implement a random number generator itself, rather it encapsulates
    /// an instance of the C# RNGCryptoServiceProvider class. It has a RandMax value of 2^64-1.
    /// <para>SystemCrypto is a concrete class which derives directly from Rng. Note that this
    /// class cannot be seeded or cloned.</para>
    /// </summary>
    /// <remarks>
    /// <para>Any cryptographic or randomness qualities of SystemCrypto are those
    /// provided by the underlying C# RNGCryptoServiceProvider class.</para>
    /// <seealso cref="Rng"/>
    /// <seealso cref="SystemRng"/>
    /// </remarks>
    public class SystemCrypto : Rng
    {
        private const int Buf8Size =  384; // <- should divide by 8
        private const int Buf32Size = Buf8Size / sizeof(uint);
        private const int Buf64Size = Buf8Size / sizeof(ulong);

        private int counter32 = 0;
        private int counter64 = 0;
        private byte[] buf8 = new byte[Buf8Size];
        private uint[] buf32 = new uint[Buf32Size];
        private ulong[] buf64 = new ulong[Buf64Size];

        private RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SystemCrypto()
            : base(UInt64.MaxValue)
        {
        }
        
        public override string AlgorithmName
        {
            get { return "OS Cryptographic Generator"; }
        }

        public override ulong Next()
        {
            return NextImpl64();
        }

        // Override for performance. Disable to test base methods.
#if !DISABLE_SUBCLASS_OVERRIDES
        public override Rng NewInstance()
        {
            return new SystemCrypto();
        }

        public override uint Next32()
        {
            if (counter32 == 0)
            {
                // Generate new bytes using 32 bit block
                // We do this only for performance. The base class Next32() would
                // otherwise take care of things, though will be a tad slower.
                crypto.GetBytes(buf8);
                Buffer.BlockCopy(buf8, 0, buf32, 0, Buf8Size);
                counter32 = Buf32Size;
            }

            return buf32[--counter32];
        }

        public override ulong Next64()
        {
            return NextImpl64();
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong NextImpl64()
        {
            if (counter64 == 0)
            {
                // Generate new bytes
                crypto.GetBytes(buf8);
                Buffer.BlockCopy(buf8, 0, buf64, 0, Buf8Size);
                counter64 = Buf64Size;
            }

            return buf64[--counter64];
        }

    }
}
