using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    /// <summary>
    /// Custom class to load WAV sounds, modify them and load them in a new SoundPlayer ready to play.
    /// </summary>
    internal static class Reproductor_Sonidos
    {
        internal static readonly string Ruta_Sonidos = Application.StartupPath + "\\Sounds";

        /// <summary>
        /// Loads the selected sound inside a SoundPlayer ready to play with the selected stereo angle.
        /// </summary>
        /// <param name="Nombre">Any valid and existing file name (without the extension) inside the Sounds folder, near the application.</param>
        /// <param name="Porcentaje_Volumen">Any valid percent number between 0 and 100. Useful to decrease the volume of louder sounds.</param>
        /// <param name="Ángulo_Estéreo">Any valid angle between 0 and 360 degrees, where 180 means equal stereo separation, 0 means only the left channel will have sound and 360 means only the right channel will have sound (note that the angle is counter clockwise).</param>
        /// <returns></returns>
        internal static SoundPlayer Cargar_Sonido(string Nombre, double Porcentaje_Frecuencia, double Porcentaje_Volumen, double Ángulo_Estéreo)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta_Sonidos) && Directory.Exists(Ruta_Sonidos) && !string.IsNullOrEmpty(Nombre))
                {
                    string Ruta = Ruta_Sonidos + "\\" + Nombre + ".wav";
                    if (File.Exists(Ruta))
                    {
                        FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                        if (Lector.Length > 44L) // At least has a RIFF WAVE header
                        {
                            double Porcentaje_L = 100d; // Default volume at 100 %.
                            double Porcentaje_R = 100d;
                            Ángulo_Estéreo = (((360d - Ángulo_Estéreo) - 180d) * 100d) / 180d; // Invert the angle, make it in the range of -180 to +180 and finally convert it to a percentage between -100 and +100.
                            if (Ángulo_Estéreo != 0d) // Hopefully this simple stereo trick will work.
                            {
                                //if (Ángulo_Estéreo < 0d) Porcentaje_R -= 100d - Math.Abs(Ángulo_Estéreo);
                                //else Porcentaje_L -= 100d - Ángulo_Estéreo;
                                if (Ángulo_Estéreo < 0d) Porcentaje_R = (Porcentaje_R - (100d - Math.Abs(Ángulo_Estéreo))) / 2d;
                                else Porcentaje_L = (Porcentaje_L - (100d - Ángulo_Estéreo)) / 2d;
                            }
                            bool Modificar = Porcentaje_Volumen < 100d || Porcentaje_L < 100d || Porcentaje_R < 100d;
                            //Ángulo_Estéreo -= 180d;
                            //if (Ángulo_Estéreo < 0d) Ángulo_Estéreo = 180d - (Ángulo_Estéreo + 360d);
                            Lector.Seek(0L, SeekOrigin.Begin);
                            // Warning: this code assumes the WAV file is 16 bits stereo at 44100 Hz.
                            BinaryReader Lector_Binario = new BinaryReader(Lector, Encoding.ASCII);
                            if (new string(Lector_Binario.ReadChars(4)) != "RIFF") throw new Exception("Invalid file format 1");
                            Lector_Binario.ReadInt32(); // File length minus first 8 bytes of RIFF description, we don't use it
                            if (new string(Lector_Binario.ReadChars(4)) != "WAVE") throw new Exception("Invalid file format 2");
                            if (new string(Lector_Binario.ReadChars(4)) != "fmt ") throw new Exception("Invalid file format 3");
                            int Longitud_Formato = Lector_Binario.ReadInt32();
                            if (Longitud_Formato < 16) throw new Exception("Invalid file format 4"); // bad format chunk length
                                                                                                     //Lector_WAV.Seek(Lector_WAV.Position + Longitud_Formato, SeekOrigin.Begin);
                                                                                                     //Byte[] Matriz_Bytes_Formato = new Byte[Longitud_Formato];
                                                                                                     //Lector.Read(Matriz_Bytes_Formato, 0, Matriz_Bytes_Formato.Length);
                                                                                                     //Matriz_Bytes_Formato = null;
                            Lector_Binario.ReadInt16(); // wFormatTag
                            Lector_Binario.ReadInt16(); // nChannels
                            long Reproductor_Índice_Frecuencia = Lector.Position;
                            int Frecuencia_Original = Lector_Binario.ReadInt32(); // nSamplesPerSec
                            long Reproductor_Índice_Bytes_Segundo = Lector.Position;
                            int Abc = Lector_Binario.ReadInt32(); // nAvgBytesPerSec
                            Lector_Binario.ReadInt16(); // nBlockAlign
                            Lector_Binario.ReadInt16(); // wBitsPerSample
                                                        //Lector_Binario.Read(null, 0, 128);
                                                        //Lector_Binario.ReadInt64();
                                                        //Lector_Binario.ReadInt64();
                                                        // advance in the stream to skip the wave format block 
                            Longitud_Formato -= 16; // minimum format size

                            //MessageBox.Show(Reproductor_Índice_Frecuencia.ToString(), Reproductor_Índice_Bytes_Segundo.ToString());
                            //MessageBox.Show(Frecuencia_Original.ToString(), Abc.ToString());

                            while (Longitud_Formato > 0)
                            {
                                Lector_Binario.ReadByte();
                                Longitud_Formato--;
                            }
                            //MessageBox.Show(Lector.Position.ToString());
                            // assume the data chunk is aligned
                            //Lector.Seek(36L, SeekOrigin.Begin);
                            //MessageBox.Show(Lector.Position.ToString());
                            int Byte_Anterior_1 = Lector.ReadByte();
                            int Byte_Anterior_2 = Lector.ReadByte();
                            int Byte_Anterior_3 = Lector.ReadByte();
                            while (Lector.Position < Lector.Length)
                            {
                                int Byte_Actual = Lector.ReadByte();
                                if (Byte_Anterior_1 == 100 && Byte_Anterior_2 == 97 && Byte_Anterior_3 == 116 && Byte_Actual == 97) break; // data
                                else
                                {
                                    Byte_Anterior_1 = Byte_Anterior_2;
                                    Byte_Anterior_2 = Byte_Anterior_3;
                                    Byte_Anterior_3 = Byte_Actual;
                                }
                            }
                            if (Lector.Position >= Lector.Length) throw new Exception("Invalid file format 5");

                            long Reproductor_Longitud_Samples = Lector_Binario.ReadUInt32();
                            long Reproductor_Índice_Samples = Lector.Position;

                            List<byte> Lista_Bytes = new List<byte>(); // Buffer for the modified sound.
                            byte[] Matriz_Bytes_Cabecera = new byte[Reproductor_Índice_Samples];
                            Lector.Seek(0L, SeekOrigin.Begin);
                            Lector.Read(Matriz_Bytes_Cabecera, 0, Matriz_Bytes_Cabecera.Length);
                            // Modify the frequency of the sound (pitch):
                            //if (Frecuencia >= 100 && Frecuencia <= 200000)
                            if (Porcentaje_Frecuencia != 100d)
                            {
                                int Frecuencia = (int)Math.Round(((double)Frecuencia_Original * Porcentaje_Frecuencia) / 100d, MidpointRounding.AwayFromZero);
                                if (Frecuencia < 100) Frecuencia = 100;
                                else if (Frecuencia > 200000) Frecuencia = 200000;
                                byte[] Matriz_Bytes_Frecuencia = BitConverter.GetBytes(Frecuencia);
                                //Array.Reverse(Matriz_Bytes_Frecuencia);
                                Matriz_Bytes_Frecuencia.CopyTo(Matriz_Bytes_Cabecera, Reproductor_Índice_Frecuencia);
                                Matriz_Bytes_Frecuencia = null;
                            }
                            Lista_Bytes.AddRange(Matriz_Bytes_Cabecera);

                            // Modifiy the volume of the channels to simulate the selected stereo angle (so for example the sound will seem to come from the same place where a lightning strikes on the screen).
                            byte[] Matriz_Bytes = new byte[4096];
                            for (long Índice_Bloque = 0L; Índice_Bloque <= Reproductor_Longitud_Samples; Índice_Bloque += 4096L)
                            {
                                Lector.Seek(Reproductor_Índice_Samples + Índice_Bloque, SeekOrigin.Begin);
                                int Longitud = Lector.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                if (Modificar) // Avoid modifying the sound if nothing in it should change.
                                {
                                    for (int Índice_L = 0, Índice_R = 2; Índice_L < Longitud; Índice_L += 4, Índice_R += 4) // Add 4 bytes to skip a left and right 16 bits sample pair.
                                    {
                                        //long Sample_L = (long)BitConverter.ToInt16(Matriz_Bytes, Índice_L); // Convert the 2 left channel bytes to a 16 bits sound sample between -32768 and +32767.
                                        //long Sample_R = (long)BitConverter.ToInt16(Matriz_Bytes, Índice_R);
                                        if (Porcentaje_L < 100d)
                                        {
                                            int Sample_L = (int)Math.Round((((double)BitConverter.ToInt16(Matriz_Bytes, Índice_L) * Porcentaje_Volumen) * Porcentaje_L) / 10000d, MidpointRounding.AwayFromZero); // Load and convert the sample to the specified percentage.
                                            if (Sample_L < -32768) Sample_L = -32768; // Check for out of boundaries values.
                                            else if (Sample_L > 32767) Sample_L = 32767;
                                            BitConverter.GetBytes((short)Sample_L).CopyTo(Matriz_Bytes, Índice_L); // Return the sample with the modified volume to the byte array.
                                        }
                                        if (Porcentaje_R < 100d)
                                        {
                                            int Sample_R = (int)Math.Round((((double)BitConverter.ToInt16(Matriz_Bytes, Índice_R) * Porcentaje_Volumen) * Porcentaje_R) / 10000d, MidpointRounding.AwayFromZero);
                                            if (Sample_R < -32768) Sample_R = -32768;
                                            else if (Sample_R > 32767) Sample_R = 32767;
                                            BitConverter.GetBytes((short)Sample_R).CopyTo(Matriz_Bytes, Índice_R);
                                        }
                                        //long Sample_L_0_65535 = Sample_L + 32768L; // Add +32768 to the sample to avoid working with negative numbers.
                                        //long Sample_R_0_65535 = Sample_R + 32768L;
                                        //long Volumen_L = Math.Abs(Sample_L < 0 ? Sample_L + 1L : Sample_L); // Convert the signed sample to volume.
                                        //long Volumen_R = Math.Abs(Sample_R < 0 ? Sample_R + 1L : Sample_R);
                                    }
                                }
                                Lista_Bytes.AddRange(Matriz_Bytes); // Write the modified samples to the bytes buffer.
                                                                    //Lector.Seek(Reproductor_Índice_Samples + Índice_Bloque, SeekOrigin.Begin);
                                                                    //Lector.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                            }
                            Lector_Binario.Close();
                            Lector_Binario.Dispose();
                            Lector_Binario = null;
                            Lector.Close();
                            Lector.Dispose();
                            Lector = null;
                            //File.WriteAllBytes(Application.StartupPath + "\\jhgfd.wav", Lector_Memoria.ToArray());
                            //SoundPlayer Reproductor = new SoundPlayer(new FileStream(Application.StartupPath + "\\jhgfd.wav", FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                            SoundPlayer Reproductor = new SoundPlayer(new MemoryStream(Lista_Bytes.ToArray()));
                            Reproductor.Load();
                            Lista_Bytes = null;
                            return Reproductor;
                        }
                        Lector.Close();
                        Lector.Dispose();
                        Lector = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Loads the selected sound inside a SoundPlayer ready to play with the selected stereo angle.
        /// </summary>
        /// <param name="Matriz_Bytes">Any byte array containing a valid WAV file with it's header inside.</param>
        /// <param name="Porcentaje_Frecuencia">Any valid percentage to play the sound at different pitches or notes.</param>
        /// <returns></returns>
        internal static SoundPlayer Cargar_Sonido(byte[] Matriz_Bytes, double Porcentaje_Frecuencia)
        {
            try
            {
                if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                {
                    long Índice_Frecuencia = 24L;
                    int Frecuencia_Original = 44100;
                    long Índice_Bytes_Segundo = 28L;
                    int Bytes_Segundo_Original = 176400;
                    int Frecuencia = (int)Math.Round(((double)Frecuencia_Original * Porcentaje_Frecuencia) / 100d, MidpointRounding.AwayFromZero);
                    if (Frecuencia < 100) Frecuencia = 100;
                    else if (Frecuencia > 200000) Frecuencia = 200000;
                    int Bytes_Segundo = Frecuencia * (Bytes_Segundo_Original / Frecuencia_Original);
                    BitConverter.GetBytes(Frecuencia).CopyTo(Matriz_Bytes, Índice_Frecuencia);
                    BitConverter.GetBytes(Bytes_Segundo).CopyTo(Matriz_Bytes, Índice_Bytes_Segundo);
                    SoundPlayer Reproductor = new SoundPlayer(new MemoryStream(Matriz_Bytes));
                    Reproductor.Load();
                    return Reproductor;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Loads the selected sound inside a SoundPlayer ready to play with the selected stereo angle.
        /// </summary>
        /// <param name="Índice_Frecuencia">The start index inside the byte array of the 4 bytes used to store the frequency of the sound. The default index is 24.</param>
        /// <param name="Frecuencia_Original">The 4 bytes value used to store the frequency of the sound. The default value is 44100.</param>
        /// <param name="Índice_Bytes_Segundo">The start index inside the byte array of the 4 bytes used to store the bytes per second of the sound. The default index is 28.</param>
        /// <param name="Bytes_Segundo_Original">The 4 bytes value used to store the bytes per second of the sound. The default value is 176400.</param>
        /// <param name="Matriz_Bytes">Any byte array containing a valid WAV file with it's header inside.</param>
        /// <param name="Porcentaje_Frecuencia">Any valid percentage to play the sound at different pitches or notes.</param>
        /// <returns></returns>
        internal static SoundPlayer Cargar_Sonido(byte[] Matriz_Bytes, long Índice_Frecuencia, int Frecuencia_Original, long Índice_Bytes_Segundo, int Bytes_Segundo_Original, double Porcentaje_Frecuencia)
        {
            try
            {
                if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                {
                    //if (Porcentaje_Frecuencia != 100d) // Adjust the frequency to play the desired note or pitch.
                    {
                        if (Índice_Frecuencia <= 0L) Índice_Frecuencia = 24L;
                        if (Frecuencia_Original <= 0L) Frecuencia_Original = 44100;
                        if (Índice_Bytes_Segundo <= 0L) Índice_Bytes_Segundo = 28L;
                        if (Bytes_Segundo_Original <= 0L) Bytes_Segundo_Original = 176400;
                        int Frecuencia = (int)Math.Round(((double)Frecuencia_Original * Porcentaje_Frecuencia) / 100d, MidpointRounding.AwayFromZero);
                        if (Frecuencia < 100) Frecuencia = 100;
                        else if (Frecuencia > 200000) Frecuencia = 200000;
                        int Bytes_Segundo = Frecuencia * (Bytes_Segundo_Original / Frecuencia_Original);
                        BitConverter.GetBytes(Frecuencia).CopyTo(Matriz_Bytes, Índice_Frecuencia);
                        BitConverter.GetBytes(Bytes_Segundo).CopyTo(Matriz_Bytes, Índice_Bytes_Segundo);
                    }
                    SoundPlayer Reproductor = new SoundPlayer(new MemoryStream(Matriz_Bytes));
                    Reproductor.Load();
                    return Reproductor;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }
    }
}
