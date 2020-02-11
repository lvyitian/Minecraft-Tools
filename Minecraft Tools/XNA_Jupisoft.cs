using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    internal static class XNA_Jupisoft
    {
        internal static bool Convertir_XNB_a_WAV(string Ruta_Entrada, string Ruta_Salida)
        {
            //Microsoft.Xna.Framework.Content.ContentManager cm = new Microsoft.Xna.Framework.Content.ContentManager(IServiceProvider

            ushort wFormatTag;
            ushort nChannels;
            uint nSamplesPerSec;
            uint nAvgBytesPerSec;
            ushort nBlockAlign;
            ushort wBitsPerSample;
            int dataChunkSize;
            byte[] waveData;

            FileStream Lector_Entrada = new FileStream(Ruta_Entrada, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);

            int q;
            XNA_Jupisoft.PrepareStream(Lector_Entrada, null, out q);
            return false;

            BinaryReader Lector_Entrada_Binario = new BinaryReader(Lector_Entrada, Encoding.ASCII);
            string format = new string(Lector_Entrada_Binario.ReadChars(3));
            if (format != "XNB") return false; // "Invalid file format: " + format;
            char platform = Lector_Entrada_Binario.ReadChar();
            if (platform != 'w') return false; // "Invalid platform: " + platform;
            int xnaVersion = Lector_Entrada_Binario.ReadByte();
            if (xnaVersion != 5)
            {
                MessageBox.Show("Unimplemented XNA version: " + xnaVersion);
                return false; // "Unimplemented XNA version: " + xnaVersion;
            }
            byte profile = Lector_Entrada_Binario.ReadByte();
            if (profile != 0)
            {
                MessageBox.Show("Unimplemented profile: " + profile);
                //return false;
            }
            uint fileLength = Lector_Entrada_Binario.ReadUInt32();
            if (fileLength != Lector_Entrada.Length)
            {
                MessageBox.Show("File length mismatch: " + fileLength + " - should be " + Lector_Entrada.Length);
                //return false;
            }
            uint typeCount = (uint)Read7BitEncodedInt(Lector_Entrada_Binario);
            if (typeCount != 1)
            {
                MessageBox.Show("Too many types: " + typeCount);
                //return false;
            }
            // Should be in a for loop but I'm too lazy to add support for multiple types.
            string type = Lector_Entrada_Binario.ReadString();
            if (type != "Microsoft.Xna.Framework.Content.SoundEffectReader")
            {
                MessageBox.Show("Wrong type reader name: " + type);
                //return false;
            }
            int typeReaderVersion = Lector_Entrada_Binario.ReadInt32();
            if (typeReaderVersion != 0)
            {
                MessageBox.Show("Wrong type reader version: " + typeReaderVersion);
                //return false;
            }
            uint sharedResourcesCount = (uint)Read7BitEncodedInt(Lector_Entrada_Binario);
            if (sharedResourcesCount != 0)
            {
                MessageBox.Show("Too many shared resources: " + sharedResourcesCount);
                //return false;
            }
            if (Read7BitEncodedInt(Lector_Entrada_Binario) != 1)
            {
                MessageBox.Show("???");
                //return false;
            }
            // WAVE format
            uint formatChunkSize = Lector_Entrada_Binario.ReadUInt32();
            if (formatChunkSize != 18)
            {
                MessageBox.Show("Wrong format chunk size: " + formatChunkSize);
                //return false;
            }
            if ((wFormatTag = Lector_Entrada_Binario.ReadUInt16()) != 1)
            {
                MessageBox.Show("Unimplemented wav codec (must be PCM)");
                //return false;
            }
            nChannels = Lector_Entrada_Binario.ReadUInt16();
            nSamplesPerSec = Lector_Entrada_Binario.ReadUInt32();
            nAvgBytesPerSec = Lector_Entrada_Binario.ReadUInt32();
            nBlockAlign = Lector_Entrada_Binario.ReadUInt16();
            wBitsPerSample = Lector_Entrada_Binario.ReadUInt16();
            if (nAvgBytesPerSec != (nSamplesPerSec * nChannels * (wBitsPerSample / 8)))
            {
                MessageBox.Show("Average bytes per second number incorrect");
                return false;
            }
            if (nBlockAlign != (nChannels * (wBitsPerSample / 8)))
            {
                MessageBox.Show("Block align number incorrect");
                return false;
            }
            Lector_Entrada_Binario.ReadUInt16();
            waveData = Lector_Entrada_Binario.ReadBytes(dataChunkSize = Lector_Entrada_Binario.ReadInt32());

            MessageBox.Show("Done!");
            return false;

            Stream Lector_Salida = File.Create(Ruta_Salida);
            BinaryWriter Lector_Salida_Binario = new BinaryWriter(Lector_Salida);
            Lector_Salida_Binario.Write("RIFF".ToCharArray());
            Lector_Salida_Binario.Write(dataChunkSize + 36);
            Lector_Salida_Binario.Write("WAVE".ToCharArray());
            Lector_Salida_Binario.Write("fmt ".ToCharArray());
            Lector_Salida_Binario.Write(16);
            Lector_Salida_Binario.Write(wFormatTag);
            Lector_Salida_Binario.Write(nChannels);
            Lector_Salida_Binario.Write(nSamplesPerSec);
            Lector_Salida_Binario.Write(nAvgBytesPerSec);
            Lector_Salida_Binario.Write(nBlockAlign);
            Lector_Salida_Binario.Write(wBitsPerSample);
            Lector_Salida_Binario.Write("data".ToCharArray());
            Lector_Salida_Binario.Write(dataChunkSize);
            Lector_Salida_Binario.Write(waveData);

            Lector_Salida_Binario.Close();
            Lector_Salida_Binario.Dispose();
            Lector_Salida_Binario = null;
            Lector_Salida.Close();
            Lector_Salida.Dispose();
            Lector_Salida = null;
            Lector_Entrada_Binario.Close();
            Lector_Entrada_Binario.Dispose();
            Lector_Entrada_Binario = null;
            Lector_Entrada.Close();
            Lector_Entrada.Dispose();
            Lector_Entrada = null;
            return true; // "Successfully converted " + Path.GetFileNameWithoutExtension(Ruta_Entrada);
        }

        internal static int Read7BitEncodedInt(BinaryReader br)
        {
            int num = 0;
            int num2 = 0;
            while (num2 != 35)
            {
                byte b = br.ReadByte();
                num |= (int)(b & 127) << num2;
                num2 += 7;
                if ((b & 128) == 0)
                {
                    return num;
                }
            }
            throw new FormatException("Failed to read a Microsoft 7-bit encoded integer");
        }

        internal static Stream PrepareStream(Stream input, string assetName, out int graphicsProfile)
        {
            Stream result;
            try
            {
                BinaryReader binaryReader = new BinaryReader(input);
                if (binaryReader.ReadByte() != 88 ||
                    binaryReader.ReadByte() != 78 ||
                    binaryReader.ReadByte() != 66) // "XNB".
                {
                    //throw ContentReader.CreateContentLoadException(assetName, null, FrameworkResources.BadXnbMagic, new object[0]);
                    MessageBox.Show("BadXnbMagic");
                    throw new Exception("BadXnbMagic");
                }
                if (binaryReader.ReadByte() != 119) // "w".
                {
                    //throw ContentReader.CreateContentLoadException(assetName, null, FrameworkResources.BadXnbPlatform, new object[0]);
                    MessageBox.Show("BadXnbPlatform");
                    throw new Exception("BadXnbPlatform");
                }
                int num = (int)binaryReader.ReadUInt16();
                graphicsProfile = (num & 32512) >> 8;
                num &= -32513;
                bool flag;
                if (num == 5) // Not compressed?
                {
                    flag = false;
                }
                else // Compressed?
                {
                    if (num != 32773)
                    {
                        //throw ContentReader.CreateContentLoadException(assetName, null, FrameworkResources.BadXnbVersion, new object[0]);
                        MessageBox.Show("BadXnbVersion");
                        throw new Exception("BadXnbVersion");
                    }
                    flag = true;
                }
                MessageBox.Show("Flag: " + flag);
                int num2 = binaryReader.ReadInt32();
                if (input.CanSeek && (long)(num2 - 10) > input.Length - input.Position)
                {
                    //throw ContentReader.CreateContentLoadException(assetName, null, FrameworkResources.BadXnbSize, new object[0]);
                    MessageBox.Show("BadXnbSize");
                    throw new Exception("BadXnbSize");
                }
                if (flag)
                {
                    int compressedTodo = num2 - 14;
                    int decompressedTodo = binaryReader.ReadInt32();
                    //result = new XNA_Jupisoft.DecompressStream(input, compressedTodo, decompressedTodo);
                    //result = input;
                    result = null;
                }
                else
                {
                    result = input;
                }
            }
            catch (IOException innerException)
            {
                //throw ContentReader.CreateContentLoadException(assetName, innerException, FrameworkResources.BadXnb, new object[0]);
                MessageBox.Show("BadXnb");
                throw new Exception("BadXnb");
            }
            return result;
        }

        /*internal class DecompressStream : Stream
        {
            private const int CompressedBufferSize = 65536;

            private const int DecompressedBufferSize = 65536;

            private Stream baseStream;

            private int compressedTodo;

            private int compressedSize;

            private int compressedPosition;

            private byte[] compressedBuffer;

            private int decompressedTodo;

            private int decompressedSize;

            private int decompressedPosition;

            private byte[] decompressedBuffer;

            private IntPtr decompressionContext;

            public override bool CanRead
            {
                get
                {
                    return true;
                }
            }

            public override bool CanSeek
            {
                get
                {
                    return false;
                }
            }

            public override bool CanWrite
            {
                get
                {
                    return false;
                }
            }

            public override long Length
            {
                get
                {
                    throw new NotSupportedException();
                }
            }

            public override long Position
            {
                get
                {
                    throw new NotSupportedException();
                }
                set
                {
                    throw new NotSupportedException();
                }
            }

            public DecompressStream(Stream baseStream, int compressedTodo, int decompressedTodo)
            {
                this.baseStream = baseStream;
                this.compressedTodo = compressedTodo;
                this.decompressedTodo = decompressedTodo;
                this.compressedBuffer = new byte[65536];
                this.decompressedBuffer = new byte[65536];
                this.decompressionContext = NativeMethods.CreateDecompressionContext();
                if (this.decompressionContext == IntPtr.Zero)
                {
                    //throw new InvalidOperationException(FrameworkResources.DecompressionError);
                    MessageBox.Show("DecompressionError");
                    throw new Exception("DecompressionError");
                }
            }

            protected override void Dispose(bool disposing)
            {
                if (this.decompressionContext != IntPtr.Zero)
                {
                    NativeMethods.DestroyDecompressionContext(this.decompressionContext);
                    this.decompressionContext = IntPtr.Zero;
                }
                base.Dispose(disposing);
            }

            public override int ReadByte()
            {
                if (this.decompressedPosition >= this.decompressedSize && !this.DecompressNextBuffer())
                {
                    return -1;
                }
                return (int)this.decompressedBuffer[this.decompressedPosition++];
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                if (this.decompressedPosition >= this.decompressedSize && !this.DecompressNextBuffer())
                {
                    return 0;
                }
                int num = this.decompressedSize - this.decompressedPosition;
                if (count > num)
                {
                    count = num;
                }
                Array.Copy(this.decompressedBuffer, this.decompressedPosition, buffer, offset, count);
                this.decompressedPosition += count;
                return count;
            }

            private unsafe bool DecompressNextBuffer()
            {
                if (this.decompressedTodo <= 0)
                {
                    return false;
                }
                while (true)
                {
                    if (this.compressedPosition >= this.compressedSize)
                    {
                        this.ReadNextBufferFromDisk();
                    }
                    int num = this.compressedSize - this.compressedPosition;
                    int num2 = 65536;
                    fixed (byte* ptr = this.compressedBuffer)
                    {
                        fixed (byte* ptr2 = this.decompressedBuffer)
                        {
                            if (NativeMethods.Decompress(this.decompressionContext, (void*)ptr2, ref num2, (void*)((byte*)ptr + this.compressedPosition), ref num) != null)
                            {
                                break;
                            }
                        }
                    }
                    if (num2 == 0 && num == 0)
                    {
                        goto Block_7;
                    }
                    this.compressedPosition += num;
                    this.decompressedTodo -= num2;
                    this.decompressedSize = num2;
                    this.decompressedPosition = 0;
                    if (this.decompressedSize != 0)
                    {
                        return true;
                    }
                }
                throw new InvalidOperationException(FrameworkResources.DecompressionError);
                Block_7:
                throw new InvalidOperationException(FrameworkResources.DecompressionError);
            }

            private void ReadNextBufferFromDisk()
            {
                if (this.compressedTodo <= 0)
                {
                    return;
                }
                this.ReadBufferFromDisk(this.compressedBuffer, ref this.compressedTodo, out this.compressedSize);
                this.compressedPosition = 0;
            }

            private void ReadBufferFromDisk(byte[] buffer, ref int bufferTodo, out int bufferSize)
            {
                int num = 65536;
                if (num > bufferTodo)
                {
                    num = bufferTodo;
                }
                int num2;
                for (int i = 0; i < num; i += num2)
                {
                    num2 = this.baseStream.Read(buffer, i, num - i);
                    if (num2 == 0)
                    {
                        throw new InvalidOperationException(FrameworkResources.DecompressionError);
                    }
                }
                bufferTodo -= num;
                bufferSize = num;
            }

            public override void Flush()
            {
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotSupportedException();
            }

            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException();
            }
        }*/

        internal static class NativeMethods
        {
            /*public unsafe static IntPtr CreateDecompressionContext()
            {
                if (!< Module >.? A0xb221fc8f.gImportedDecompressionMethods)
                {

                < Module >.ImportXnaNativeMethod((method*)(&< Module >.? A0xb221fc8f.CreateDecompressionContext), (sbyte*)(&< Module >.?? _C@_0BL@BCLAJPDJ@CreateDecompressionContext ?$AA@));

                < Module >.ImportXnaNativeMethod((method*)(&< Module >.? A0xb221fc8f.DestroyDecompressionContext), (sbyte*)(&< Module >.?? _C@_0BM@OFGNMGMO@DestroyDecompressionContext ?$AA@));

                < Module >.ImportXnaNativeMethod((method*)(&< Module >.? A0xb221fc8f.Decompress), (sbyte*)(&< Module >.?? _C@_0L@JFAOIMHK@Decompress ?$AA@));

                < Module >.? A0xb221fc8f.gImportedDecompressionMethods = true;
                }
                void* value = calli(System.Void * modopt(System.Runtime.CompilerServices.CallConvCdecl)(), < Module >.? A0xb221fc8f.CreateDecompressionContext);
                IntPtr result = new IntPtr(value);
                return result;
            }

            public static void DestroyDecompressionContext(IntPtr context)
            {
                calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.Void *), context.ToPointer(), < Module >.? A0xb221fc8f.DestroyDecompressionContext);
            }

            public unsafe static int Decompress(IntPtr context, void* outputData, ref int outputSize, void* sourceData, ref int sourceSize)
            {
                if (context == IntPtr.Zero)
                {
                    return -2147024809;
                }
                uint num = outputSize;
                uint num2 = sourceSize;
                int num3 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.Void *, System.Void *, System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) *, System.Void *, System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) *), context.ToPointer(), outputData, ref num, sourceData, ref num2, < Module >.? A0xb221fc8f.Decompress);
                if (num3 < 0)
                {
                    return num3;
                }
                outputSize = num;
                sourceSize = num2;
                return 0;
            }*/
        }
    }
}
