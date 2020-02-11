using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Tools
{
    /// <summary>
    /// Class designed to store a copy of the Java UUID class. Ported to C# by Jupisoft.
    /// "Jupisoft" = "7af45d88-e129-4e09-b1f6-9eee3e636325".
    /// "ISpectre23" = "04ac603c-fc4c-47fb-b1e8-e559f2c65176".
    /// </summary>
    internal class UUID
    {
        public static UUID getOfflineUUID(string username)
        {
            return UUID.NameUUIDFromBytes(Encoding.UTF8.GetBytes("OfflinePlayer:" + username));
        }

        private static readonly long serialVersionUID = -4856846361193249489L;

        private readonly long mostSigBits;

        private readonly long leastSigBits;

        public/*private*/ UUID(byte[] paramArrayOfByte)
        {
            long l1 = 0L;
            long l2 = 0L;
            //assert paramArrayOfByte.length == 16 : "data must be 16 bytes in length";
            if (paramArrayOfByte.Length < 16)
            {
                Array.Resize(ref paramArrayOfByte, 16);
            }
            byte b;
            for (b = 0; b < 8; b++)
            {
                l1 = l1 << 8 | (paramArrayOfByte[b] & 0xFF);
            }
            for (b = 8; b < 16; b++)
            {
                l2 = l2 << 8 | (paramArrayOfByte[b] & 0xFF);
            }
            this.mostSigBits = l1;
            this.leastSigBits = l2;
        }

        public UUID(long paramLong1, long paramLong2)
        {
            this.mostSigBits = paramLong1;
            this.leastSigBits = paramLong2;
        }

        public static UUID randomUUID()
        {
            //SecureRandom secureRandom = Holder.numberGenerator;
            Random secureRandom = Holder.numberGenerator;
            byte[] arrayOfByte = new byte[16];
            secureRandom.NextBytes(arrayOfByte);
            arrayOfByte[6] = (byte)(arrayOfByte[6] & 0xF);
            arrayOfByte[6] = (byte)(arrayOfByte[6] | 0x40);
            arrayOfByte[8] = (byte)(arrayOfByte[8] & 0x3F);
            arrayOfByte[8] = (byte)(arrayOfByte[8] | 0x80);
            return new UUID(arrayOfByte);
        }

        public static UUID NameUUIDFromBytes(byte[] paramArrayOfByte)
        {
            MD5 messageDigest;
            try
            {
                messageDigest = MD5.Create("MD5");
            }
            catch (Exception noSuchAlgorithmException)
            {
                throw new Exception("MD5 not supported", noSuchAlgorithmException);
            }
            byte[] arrayOfByte = messageDigest.ComputeHash(paramArrayOfByte);
            arrayOfByte[6] = (byte)(arrayOfByte[6] & 0xF);
            arrayOfByte[6] = (byte)(arrayOfByte[6] | 0x30);
            arrayOfByte[8] = (byte)(arrayOfByte[8] & 0x3F);
            arrayOfByte[8] = (byte)(arrayOfByte[8] | 0x80);
            return new UUID(arrayOfByte);
        }

        public static UUID FromString(string paramString)
        {
            string[] arrayOfString = paramString.Split("-".ToCharArray());
            if (arrayOfString.Length != 5)
            {
                throw new ArgumentException("Invalid UUID string: " + paramString);
            }
            for (byte b = 0; b < 5; b++)
            {
                arrayOfString[b] = "0x" + arrayOfString[b];
            }
            long l1 = Convert.ToInt64(arrayOfString[0], 16);
            l1 <<= 16;
            l1 |= Convert.ToInt64(arrayOfString[1], 16);
            l1 <<= 16;
            l1 |= Convert.ToInt64(arrayOfString[2], 16);
            long l2 = Convert.ToInt64(arrayOfString[3], 16);
            l2 <<= 48;
            l2 |= Convert.ToInt64(arrayOfString[4], 16);
            return new UUID(l1, l2);
        }

        public long GetLeastSignificantBits()
        {
            return this.leastSigBits;
        }

        public long GetMostSignificantBits()
        {
            return this.mostSigBits;
        }

        public int Version()
        {
            return (int)(this.mostSigBits >> 12 & 0xFL);
        }

        //public int Variant() { return (int)(this.leastSigBits >>> (int)(64L - (this.leastSigBits >>> 62)) & this.leastSigBits >> 63); }

        /*public long Timestamp()
        {
            if (Version() != 1)
                throw new InvalidOperationException("Not a time-based UUID");
            return (this.mostSigBits & 0xFFFL) << 48 | (this.mostSigBits >> 16 & 0xFFFFL) << 32 | this.mostSigBits >>> 32;
        }*/

        /*public int clockSequence()
        {
            if (Version() != 1)
                throw new InvalidOperationException("Not a time-based UUID");
            return (int)((this.leastSigBits & 0x3FFF000000000000L) >>> 48);
        }*/

        public long Node()
        {
            if (Version() != 1)
                throw new /*Unsupported*/InvalidOperationException("Not a time-based UUID");
            return this.leastSigBits & 0xFFFFFFFFFFFFL;
        }

        public override string ToString()
        {
            return Digits(this.mostSigBits >> 32, 8) + "-" + Digits(this.mostSigBits >> 16, 4) + "-" + Digits(this.mostSigBits, 4) + "-" + Digits(this.leastSigBits >> 48, 4) + "-" + Digits(this.leastSigBits, 12);
        }

        private static string Digits(long paramLong, int paramInt)
        {
            long l = 1L << paramInt * 4;
            return Convert.ToString(l | paramLong & l - 1L, 16).Substring(1);
        }

        public override int GetHashCode()
        {
            long l = this.mostSigBits ^ this.leastSigBits;
            return (int)(l >> 32) ^ (int)l;
        }

        public override bool Equals(object paramObject)
        {
            if (paramObject == null || !(paramObject is UUID))
            {
                return false;
            }
            UUID uUID = (UUID)paramObject;
            return (this.mostSigBits == uUID.mostSigBits && this.leastSigBits == uUID.leastSigBits);
        }

        public int CompareTo(UUID paramUUID)
        {
            return (this.mostSigBits < paramUUID.mostSigBits) ? -1 : ((this.mostSigBits > paramUUID.mostSigBits) ? 1 : ((this.leastSigBits < paramUUID.leastSigBits) ? -1 : ((this.leastSigBits > paramUUID.leastSigBits) ? 1 : 0)));
        }

        private static class Holder
        {
            //static final SecureRandom numberGenerator = new SecureRandom();
            internal static readonly Random numberGenerator = new Random();
        }

        /*internal byte[] ToByte()
        {
            byte[] Matriz_Bytes = new byte[16];
            BitConverter.GetBytes(mostSigBits).CopyTo(Matriz_Bytes, 0);
            BitConverter.GetBytes(leastSigBits).CopyTo(Matriz_Bytes, 0);
            return Matriz_Bytes;
        }*/
    }
}
