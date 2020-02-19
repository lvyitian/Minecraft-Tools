using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    /// <summary>
    /// Class designed to store a copy of the Java UUID class. Ported to C# by Jupisoft.
    /// "Jupisoft" = "7af45d88-e129-4e09-b1f6-9eee3e636325".
    /// "ISpectre23" = "04ac603c-fc4c-47fb-b1e8-e559f2c65176".
    /// It works perfectly, but it doesn't give the same UUID for a player as given directly by Mojang.
    /// </summary>
    internal class UUID
    {
        internal static void Test()
        {
            try
            {
                MessageBox.Show(UUID.NameUUIDFromBytes(Encoding.UTF8.GetBytes("jupisoft")).ToString() + "\r\n" + UUID.NameUUIDFromBytes(Encoding.UTF8.GetBytes("OfflinePlayer:" + "jupisoft")).ToString() + "\r\n" + new UUID(8859809209419124233, -5623132338639379675).ToString());
                //byte[] qq = new byte[4];
                //new System.Collections.BitArray(new int[1] { UUID.hashCode(UUID.NameUUIDFromBytes(Encoding.UTF8.GetBytes("OfflinePlayer:" + "Jupisoft")), "Jupisoft") }).CopyTo(qq, 0);
                //MessageBox.Show(UUID.NameUUIDFromBytes(qq).ToString());
                //MessageBox.Show(UUID.hashCode(UUID.NameUUIDFromBytes(Encoding.UTF8.GetBytes("OfflinePlayer:" + "Jupisoft")), "Jupisoft").ToString());
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        /// <summary>
        /// Emulates the Java function "String..hashCode()".
        /// </summary>
        /// <param name="Texto">Any valid string.</param>
        /// <returns>Returns the emulated Java hash code as a 32 bit integer.</returns>
        internal static int Obtener_Código_Hash_String(string Texto)
        {
            try
            {
                if (!string.IsNullOrEmpty(Texto))
                {
                    List<int> Lista_Base = Calculadora_Infinita.Traducir_Número("31");
                    List<int> Lista_Multiplicador = new List<int>(new int[] { 1 });
                    List<int> Lista_Valor = new List<int>(new int[] { 0 });
                    for (int Índice = Texto.Length - 1, Índice_Potencia = 0; Índice >= 0; Índice--, Índice_Potencia++)
                    {
                        try
                        {
                            int Valor_Caracter = (int)Texto[Índice];
                            List<int> Lista_Multiplicación = Calculadora_Infinita.Operación_Multiplicación(Calculadora_Infinita.Traducir_Número(Valor_Caracter.ToString()), Lista_Multiplicador);
                            Lista_Valor = Calculadora_Infinita.Operación_Suma(Lista_Valor, Lista_Multiplicación);
                            if (Índice > 0) Lista_Multiplicador = Calculadora_Infinita.Operación_Multiplicación(Lista_Multiplicador, Lista_Base);
                        }
                        catch { break; }
                    }
                    List<List<int>> Lista_Valor_Binario = Calculadora_Infinita.Operación_Convertir_a_Base(Lista_Valor, new List<int>(new int[] { 2 }));
                    
                    return int.Parse(Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Calculadora_Infinita.Operación_Resta(Calculadora_Infinita.Operación_Convertir_desde_Base(Lista_Valor_Binario.GetRange(Math.Max(0, Lista_Valor_Binario.Count - 31), Math.Min(31, Lista_Valor_Binario.Count)), new List<int>(new int[] { 2 })), Lista_Valor_Binario.Count > 31 && Lista_Valor_Binario[Lista_Valor_Binario.Count - 32][0] != 0 ? new List<int>(new int[] { 2, 1, 4, 7, 4, 8, 3, 6, 4, 8 }) : new List<int>(new int[] { 0 }))));

                    //Lista_Valor_Binario = null;
                    //Lista_Valor = null;
                    //Lista_Multiplicador = null;
                    //Lista_Base = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return 0;
        }

        /// <summary>
        /// Source from "GameProfile.java" from "Minecraft Server 1.12.jar".
        /// </summary>
        /// <returns></returns>
        internal static int hashCode(UUID id, string name)
        {
            int result = id == null ? 0 : id.GetHashCode();
            result = 31 * result + (name == null ? 0 : Obtener_Código_Hash_String(name));
            return result;
        }

        /// <summary>
        /// Gets a players UUID given their GameProfie.
        /// </summary>
        public static UUID getUUID(string/*GameProfile*/ profile)
        {
            UUID uuid = null; //profile.getId();

            if (uuid == null)
            {
                uuid = getOfflineUUID(profile/*.getName()*/);
            }

            return uuid;
        }

        public static UUID getOfflineUUID(string username)
        {
            return UUID.NameUUIDFromBytes(Encoding.UTF8.GetBytes("OfflinePlayer:" + username));
        }

        private static readonly long serialVersionUID = -4856846361193249489L;

        internal readonly long mostSigBits;

        internal readonly long leastSigBits;

        public UUID(byte[] abyte0)
        {
            long l = 0L;
            long l1 = 0L;
            if (/*!$assertionsDisabled && */abyte0.Length != 16)
            {
                throw new Exception("data must be 16 bytes in length");
                //throw new AssertionError("data must be 16 bytes in length");
            }
            for (int i = 0; i < 8; i++)
            {
                l = l << 8 | (long)(abyte0[i] & 0xff);
            }

            for (int j = 8; j < 16; j++)
            {
                l1 = l1 << 8 | (long)(abyte0[j] & 0xff);
            }

            this.mostSigBits = l;
            this.leastSigBits = l1;
            //return new long[2] { mostSigBits, leastSigBits };
        }

        /*public UUID(byte[] paramArrayOfByte)
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
        }*/

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

        public static UUID NameUUIDFromBytes(Stream Lector)
        {
            HashAlgorithm messageDigest;
            try
            {
                //messageDigest = MD5.Create("MD5");
                messageDigest = MD5.Create();
            }
            catch (Exception noSuchAlgorithmException)
            {
                throw new Exception("MD5 not supported", noSuchAlgorithmException);
            }
            byte[] arrayOfByte = messageDigest.ComputeHash(Lector);
            //Array.Reverse(arrayOfByte);
            arrayOfByte[6] = (byte)(arrayOfByte[6] & 0xF);
            arrayOfByte[6] = (byte)(arrayOfByte[6] | 0x30);
            arrayOfByte[8] = (byte)(arrayOfByte[8] & 0x3F);
            arrayOfByte[8] = (byte)(arrayOfByte[8] | 0x80);
            return new UUID(arrayOfByte);
        }

        public static UUID NameUUIDFromBytes(byte[] paramArrayOfByte)
        {
            HashAlgorithm messageDigest;
            try
            {
                //messageDigest = MD5.Create("MD5");
                messageDigest = MD5.Create();
            }
            catch (Exception noSuchAlgorithmException)
            {
                throw new Exception("MD5 not supported", noSuchAlgorithmException);
            }
            byte[] arrayOfByte = messageDigest.ComputeHash(paramArrayOfByte);
            //Array.Reverse(arrayOfByte);
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
