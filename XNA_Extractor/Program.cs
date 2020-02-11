using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XNA_Extractor
{
    internal static class Program
    {
        /// <summary>
        /// The Windows registry build date, used to know if the older settings should be deleted.
        /// </summary>
        internal static readonly string Texto_Fecha = "019_04_17_03_22_36_454";
        internal static readonly string Ruta_Aplicación = Application.StartupPath;
        internal static readonly string Ruta_XNA = Application.StartupPath + "\\XNA";

        /// <summary>
        /// The main entry point for the "XNA Extractor" application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Depurador.Iniciar_Depurador();
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Ventana_XNA());
            Ventana_XNA Ventana = new Ventana_XNA();
            GraphicsDeviceManager Dispositivo = new GraphicsDeviceManager(Ventana);
            Dispositivo.GraphicsProfile = GraphicsProfile.HiDef; // HiDef for Stardew Valley.
            Ventana.Run();
        }

        /// <summary>
        /// Executes the specified file, directory or URL, with the specified window style.
        /// </summary>
        /// <param name="Ruta">Any valid file or directory path.</param>
        /// <param name="Estado">Any valid window style.</param>
        /// <returns>Returns true if the process can be executed. Returns false if it can't be executed.</returns>
        internal static Process Ejecutar_Ruta_Proceso(string Ruta, string Argumentos, ProcessWindowStyle Estado)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta))
                {
                    Process Proceso = new Process();
                    Proceso.StartInfo.Arguments = Argumentos;
                    Proceso.StartInfo.ErrorDialog = false;
                    Proceso.StartInfo.FileName = Ruta;
                    Proceso.StartInfo.UseShellExecute = true;
                    Proceso.StartInfo.Verb = "open";
                    Proceso.StartInfo.WindowStyle = Estado;
                    if (File.Exists(Ruta)) Proceso.StartInfo.WorkingDirectory = Ruta;
                    else if (Directory.Exists(Ruta)) Proceso.StartInfo.WorkingDirectory = Ruta;
                    Proceso.Start();
                    return Proceso;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// This function makes sure that the selected file or directory doesn't have a read-only attribute, and if it does, tries to remove it automatically.
        /// </summary>
        /// <param name="Ruta">Any valid and existing file or directory path.</param>
        /// <returns>Returns the original attributes of the file or directory.</returns>
        internal static FileAttributes Quitar_Atributo_Sólo_Lectura(string Ruta)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta) && (File.Exists(Ruta) || Directory.Exists(Ruta)))
                {
                    FileSystemInfo Info = File.Exists(Ruta) ? (FileSystemInfo)new FileInfo(Ruta) : (FileSystemInfo)new DirectoryInfo(Ruta);
                    FileAttributes Atributos_Originales = Info.Attributes;
                    FileAttributes Atributos = Atributos_Originales;
                    if ((Atributos & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        Atributos -= FileAttributes.ReadOnly;
                        if (Atributos <= 0) Atributos = FileAttributes.Normal;
                        Info.Attributes = Atributos;
                    }
                    Info = null;
                    return Atributos_Originales;
                }
            }
            catch { }
            return FileAttributes.Normal;
        }

        /// <summary>
        /// Deletes an existing file or directory and it's subfolders.
        /// </summary>
        /// <param name="Ruta">Any valid file or directory path.</param>
        /// <returns>Returns true if the file or directory doesn't exist anymore. Returns false on any error.</returns>
        internal static bool Eliminar_Archivo_Carpeta(string Ruta)
        {
            try
            {
                return Eliminar_Archivo_Carpeta(Ruta, true);
            }
            catch { }
            return false;
        }

        /// <summary>
        /// Deletes an existing file or directory.
        /// </summary>
        /// <param name="Ruta">Any valid file or directory path.</param>
        /// <param name="Eliminar_Subcarpetas">True to delete all the subfolders.</param>
        /// <returns>Returns true if the file or directory doesn't exist anymore. Returns false on any error.</returns>
        internal static bool Eliminar_Archivo_Carpeta(string Ruta, bool Eliminar_Subcarpetas)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta) && (File.Exists(Ruta) || Directory.Exists(Ruta)))
                {
                    try { Quitar_Atributo_Sólo_Lectura(Ruta); }
                    catch { }
                    try
                    {
                        if (File.Exists(Ruta)) File.Delete(Ruta);
                        else Directory.Delete(Ruta, Eliminar_Subcarpetas);
                    }
                    catch { }
                }
                if (string.IsNullOrEmpty(Ruta) || !File.Exists(Ruta)) return true;
            }
            catch { }
            return false;
        }
    }

    /// <summary>
    /// Class designed to register as Unicode text any possible exception that might happen while running the application.
    /// </summary>
    internal static class Depurador
    {
        internal static FileStream Lector_Depurador = null;
        internal static StreamReader Lector_Depurador_Entrada = null;
        internal static StreamWriter Lector_Depurador_Salida = null;

        /// <summary>
        /// Starts the necessary resources to register and store any exception that might occur while running the application.
        /// </summary>
        internal static void Iniciar_Depurador()
        {
            try
            {
                Lector_Depurador = new FileStream(Program.Ruta_XNA + "\\Debugger.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                Lector_Depurador.Seek(Lector_Depurador.Length, SeekOrigin.Begin); // End of the file.
                Lector_Depurador_Entrada = new StreamReader(Lector_Depurador, Encoding.Unicode); // For reading.
                Lector_Depurador_Salida = new StreamWriter(Lector_Depurador, Encoding.Unicode); // For writing.
            }
            catch { }
        }

        /// <summary>
        /// Writes an exception message inside the "Debugger.txt" file located inside the "XNA" folder as Unicode text if it's not already present. Note: calling "Exception.ToString()" inside this function, while easier, might return a different exception message, so to keep the source code line where the exception happened, it's recommended to use the default calling, which might also fail if the option "Optimize code" is enabled in Microsoft Visual Studio, under the "Project" menu, in "Properties", and then in the tab "Build".
        /// </summary>
        /// <param name="Mensaje">Any text message</param>
        internal static void Escribir_Excepción(string Mensaje)
        {
            try
            {
                if (string.IsNullOrEmpty(Mensaje)) Mensaje = "Unknown exception.";
                Lector_Depurador.Seek(0L, SeekOrigin.Begin); // Start of the file.
                bool Ignorar = false;
                while (!Lector_Depurador_Entrada.EndOfStream)
                {
                    string Línea = Lector_Depurador_Entrada.ReadLine();
                    if (string.Compare(Línea, Mensaje, true) == 0)
                    {
                        Ignorar = true; // Exception already regitered, so ignore it.
                        break;
                    }
                }
                Lector_Depurador.Seek(Lector_Depurador.Length, SeekOrigin.Begin); // End of the file.
                if (!Ignorar) // Register the new exception.
                {
                    Lector_Depurador_Salida.WriteLine(Mensaje);
                    Lector_Depurador_Salida.WriteLine();
                    Lector_Depurador_Salida.Flush();
                }
            }
            catch { }
        }

        /// <summary>
        /// Closes all the previously opened streams and gets ready to close the aplication.
        /// </summary>
        internal static void Detener_Depurador()
        {
            try
            {
                try
                {
                    if (Lector_Depurador_Salida != null)
                    {
                        Lector_Depurador_Salida.Close();
                        Lector_Depurador_Salida.Dispose();
                        Lector_Depurador_Salida = null;
                    }
                }
                catch { }
                try
                {
                    if (Lector_Depurador_Entrada != null)
                    {
                        Lector_Depurador_Entrada.Close();
                        Lector_Depurador_Entrada.Dispose();
                        Lector_Depurador_Entrada = null;
                    }
                }
                catch { }
                try
                {
                    if (Lector_Depurador != null)
                    {
                        Lector_Depurador.Close();
                        Lector_Depurador.Dispose();
                        Lector_Depurador = null;
                    }
                }
                catch { }
            }
            catch { }
        }
    }

    /// <summary>
    /// Class designed by Jupisoft that simulates being a XNA 4.0 game, but instead what it does
    /// is it loads the previously copied ".xnb" resource files in an adjacent folder to this
    /// application called "XNA" and tries to convert one by one to regular formats like PNG
    /// images for the 2D textures or text files for the dictionary of strings.
    /// 
    /// WARNING: the code of this application could be used to obtain all the resources from
    /// any XNA game, which might result in doing something illegal, so be sure to have legit
    /// access to the content of the game you're trying to extract, for example a game you've
    /// created yourself or from someone you have permission to extract it's resources. Jupisoft
    /// will never be responsible for your actions, so please never make anything illegal with
    /// the free source code of this application, thank you.
    /// 
    /// Note: start this application with a Process set to a hidden window style, it'll still work.
    /// </summary>
    public partial class Ventana_XNA : Game
    {
        /// <summary>
        /// Loads the resources from any other XNA game like if they were form this one and extracts them.
        /// Tested with the XNA resources from the games "Terraria" and "Stardew Valley" with success.
        /// With applications like "ILSpy" you can see the source code from XNA games since they are .NET.
        /// The idea of this application is to find secrets hidden in the game textures and resources,
        /// but it should never be used to do anything illegal, so be sure you have the proper
        /// permissions before you start using it on random games. Jupisoft will never be responsible.
        /// </summary>
        protected override void LoadContent()
        {
            try
            {
                try { if (!Directory.Exists(Program.Ruta_XNA)) Directory.CreateDirectory(Program.Ruta_XNA); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }

                List<string> Lista_Rutas = new List<string>();
                List<string> Lista_Rutas_XSB = new List<string>();
                string[] Matriz_Rutas = Directory.GetFiles(Program.Ruta_XNA, "*.xnb", SearchOption.AllDirectories);
                if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                {
                    Lista_Rutas.AddRange(Matriz_Rutas);
                    Matriz_Rutas = null;
                }
                Matriz_Rutas = Directory.GetFiles(Program.Ruta_XNA, "*.xwb", SearchOption.AllDirectories);
                if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                {
                    Lista_Rutas.AddRange(Matriz_Rutas);
                    Matriz_Rutas = null;
                }
                Matriz_Rutas = Directory.GetFiles(Program.Ruta_XNA, "*.xgs", SearchOption.AllDirectories);
                if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                {
                    Lista_Rutas.AddRange(Matriz_Rutas);
                    Matriz_Rutas = null;
                }
                Matriz_Rutas = Directory.GetFiles(Program.Ruta_XNA, "*.xsb", SearchOption.AllDirectories);
                if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                {
                    //Lista_Rutas.AddRange(Matriz_Rutas);
                    Lista_Rutas_XSB.AddRange(Matriz_Rutas);
                    if (Lista_Rutas_XSB.Count > 1) Lista_Rutas.Sort();
                    Matriz_Rutas = null;
                }
                if (Lista_Rutas.Count > 1) Lista_Rutas.Sort();
                Matriz_Rutas = Lista_Rutas.ToArray();
                Lista_Rutas = null;

                if (Matriz_Rutas != null && Matriz_Rutas.Length > 0) // There are files to extract.
                {
                    foreach (string Ruta in Matriz_Rutas)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                            {
                                string Ruta_Relativa = '.' + Ruta.Substring(Program.Ruta_Aplicación.Length).Substring(0, (Ruta.Length - Program.Ruta_Aplicación.Length) - 4);
                                string Ruta_Salida = Ruta.Substring(0, Ruta.Length - 4);

                                try // Try to read as a 2D texture and export as a PNG image.
                                {
                                    Microsoft.Xna.Framework.Graphics.Texture2D Textura = base.Content.Load<Texture2D>(Ruta_Relativa);
                                    if (Textura != null)
                                    {
                                        MemoryStream Lector_Memoria = new MemoryStream();
                                        int Ancho = 16; // Default on error.
                                        int Alto = 16;
                                        try { Ancho = Textura.Width; }
                                        catch { Ancho = 16; }
                                        try { Alto = Textura.Height; }
                                        catch { Alto = 16; }
                                        Textura.SaveAsPng(Lector_Memoria, Ancho, Textura.Height);
                                        byte[] Matriz_Bytes = Lector_Memoria.ToArray();
                                        Lector_Memoria.Close();
                                        Lector_Memoria.Dispose();
                                        Lector_Memoria = null;
                                        Lector_Memoria = new MemoryStream(Matriz_Bytes);
                                        Image Imagen_Original = null;
                                        try { Imagen_Original = Image.FromStream(Lector_Memoria, false, false); }
                                        catch { Imagen_Original = null; }
                                        Lector_Memoria.Close();
                                        Lector_Memoria.Dispose();
                                        Lector_Memoria = null;
                                        Matriz_Bytes = null;
                                        if (Imagen_Original != null) // Reconvert the image to 24 or 32 bits with alpha.
                                        {
                                            //Ancho = Imagen_Original.Width; // Could the width or height change?
                                            //Alto = Imagen_Original.Height;
                                            Bitmap Imagen = new Bitmap(Ancho, Alto, !Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? PixelFormat.Format24bppRgb : PixelFormat.Format32bppArgb);
                                            Graphics Pintar = Graphics.FromImage(Imagen);
                                            Pintar.CompositingMode = CompositingMode.SourceCopy;
                                            Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                            Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                            Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                            Pintar.SmoothingMode = SmoothingMode.None;
                                            Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                                            Pintar.DrawImage(Imagen_Original, new System.Drawing.Rectangle(0, 0, Ancho, Alto), new System.Drawing.Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                            Pintar.Dispose();
                                            Pintar = null;
                                            while (File.Exists(Ruta_Salida + ".png")) Ruta_Salida += '_';
                                            Imagen.Save(Ruta_Salida + ".png", ImageFormat.Png);
                                            Ruta_Salida = null;
                                            Imagen.Dispose();
                                            Imagen = null;
                                            Imagen_Original.Dispose();
                                            Imagen_Original = null;
                                            Ruta_Relativa = null;
                                            Program.Eliminar_Archivo_Carpeta(Ruta); // Delete the copied XNB resource.
                                            continue; // Go to the next XNB resource file.
                                        }
                                    }
                                }
                                catch (Exception Excepción)
                                {
                                    //Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                }

                                try // Try to read as a string and string dictionary and export as Unicode text.
                                {
                                    Dictionary<string, string> Diccionario = base.Content.Load<Dictionary<string, string>>(Ruta_Relativa);
                                    if (Diccionario != null && Diccionario.Count > 0)
                                    {
                                        while (File.Exists(Ruta_Salida + ".txt")) Ruta_Salida += '_';
                                        FileStream Lector = new FileStream(Ruta_Salida + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                        Lector.SetLength(0L);
                                        Lector.Seek(0L, SeekOrigin.Begin);
                                        StreamWriter Lector_Texto = new StreamWriter(Lector, Encoding.Unicode);
                                        foreach (KeyValuePair<string, string> Entrada in Diccionario)
                                        {
                                            try
                                            {
                                                Lector_Texto.WriteLine(Entrada.Key);
                                                Lector_Texto.WriteLine(Entrada.Value);
                                                Lector_Texto.WriteLine();
                                                Lector_Texto.Flush();
                                            }
                                            catch (Exception Excepción)
                                            {
                                                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                                continue;
                                            }
                                        }
                                        Lector_Texto.Close();
                                        Lector_Texto.Dispose();
                                        Lector_Texto = null;
                                        Lector.Close();
                                        Lector.Dispose();
                                        Lector = null;
                                        Diccionario = null;
                                        Program.Eliminar_Archivo_Carpeta(Ruta); // Delete the copied XNB resource.
                                        continue; // Go to the next XNB resource file.
                                    }
                                }
                                catch (Exception Excepción)
                                {
                                    //Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                }
                                
                                try // Try to read as an int and text dictionary and export as Unicode text.
                                {
                                    Dictionary<int, string> Diccionario = base.Content.Load<Dictionary<int, string>>(Ruta_Relativa);
                                    if (Diccionario != null && Diccionario.Count > 0)
                                    {
                                        while (File.Exists(Ruta_Salida + ".txt")) Ruta_Salida += '_';
                                        FileStream Lector = new FileStream(Ruta_Salida + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                        Lector.SetLength(0L);
                                        Lector.Seek(0L, SeekOrigin.Begin);
                                        StreamWriter Lector_Texto = new StreamWriter(Lector, Encoding.Unicode);
                                        foreach (KeyValuePair<int, string> Entrada in Diccionario)
                                        {
                                            try
                                            {
                                                Lector_Texto.WriteLine(Entrada.Key.ToString());
                                                Lector_Texto.WriteLine(Entrada.Value);
                                                Lector_Texto.WriteLine();
                                                Lector_Texto.Flush();
                                            }
                                            catch (Exception Excepción)
                                            {
                                                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                                continue;
                                            }
                                        }
                                        Lector_Texto.Close();
                                        Lector_Texto.Dispose();
                                        Lector_Texto = null;
                                        Lector.Close();
                                        Lector.Dispose();
                                        Lector = null;
                                        Diccionario = null;
                                        Program.Eliminar_Archivo_Carpeta(Ruta); // Delete the copied XNB resource.
                                        continue; // Go to the next XNB resource file.
                                    }
                                }
                                catch (Exception Excepción)
                                {
                                    //Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                }

                                try // Try to read as an int and int array dictionary and export as Unicode text.
                                {
                                    Dictionary<int, int[]> Diccionario = base.Content.Load<Dictionary<int, int[]>>(Ruta_Relativa);
                                    if (Diccionario != null && Diccionario.Count > 0)
                                    {
                                        while (File.Exists(Ruta_Salida + ".txt")) Ruta_Salida += '_';
                                        FileStream Lector = new FileStream(Ruta_Salida + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                        Lector.SetLength(0L);
                                        Lector.Seek(0L, SeekOrigin.Begin);
                                        StreamWriter Lector_Texto = new StreamWriter(Lector, Encoding.Unicode); // .Default?
                                        foreach (KeyValuePair<int, int[]> Entrada in Diccionario)
                                        {
                                            try
                                            {
                                                Lector_Texto.WriteLine(Entrada.Key.ToString());
                                                if (Entrada.Value != null && Entrada.Value.Length > 0)
                                                {
                                                    for (int Índice = 0; Índice < Entrada.Value.Length; Índice++)
                                                    {
                                                        try
                                                        {
                                                            Lector_Texto.Write(Entrada.Value[Índice].ToString() + (Índice + 1 < Entrada.Value.Length ? ", " : null));
                                                        }
                                                        catch (Exception Excepción)
                                                        {
                                                            Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                                            continue;
                                                        }
                                                    }
                                                    Lector_Texto.WriteLine();
                                                }
                                                else Lector_Texto.WriteLine();
                                                Lector_Texto.WriteLine();
                                                Lector_Texto.Flush();
                                            }
                                            catch (Exception Excepción)
                                            {
                                                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                                continue;
                                            }
                                        }
                                        Lector_Texto.Close();
                                        Lector_Texto.Dispose();
                                        Lector_Texto = null;
                                        Lector.Close();
                                        Lector.Dispose();
                                        Lector = null;
                                        Diccionario = null;
                                        Program.Eliminar_Archivo_Carpeta(Ruta); // Delete the copied XNB resource.
                                        continue; // Go to the next XNB resource file.
                                    }
                                }
                                catch (Exception Excepción)
                                {
                                    //Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                }

                                try // Try to read as a sprite font and export as a PNG image.
                                {
                                    Microsoft.Xna.Framework.Graphics.SpriteFont Fuente = base.Content.Load<SpriteFont>(Ruta_Relativa);
                                    if (Fuente != null)
                                    {
                                        Fuente = null;
                                        while (File.Exists(Ruta_Salida + ".png")) Ruta_Salida += '_';
                                        if (XNA_Extractor.Extract.XnbExtractor.Extract(Ruta, Ruta_Salida + ".png", false, false, false, true))
                                        {
                                            Program.Eliminar_Archivo_Carpeta(Ruta); // Delete the copied XNB resource.
                                            continue; // Go to the next XNB resource file.
                                        }
                                    }
                                }
                                catch (Exception Excepción)
                                {
                                    //Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                }

                                try // Try to read as a sound effect and export as WAV audio file.
                                {
                                    Microsoft.Xna.Framework.Audio.SoundEffect Efecto_Sonido = base.Content.Load<Microsoft.Xna.Framework.Audio.SoundEffect>(Ruta_Relativa);
                                    if (Efecto_Sonido != null)
                                    {
                                        Efecto_Sonido.Dispose();
                                        Efecto_Sonido = null;
                                        while (File.Exists(Ruta_Salida + ".wav")) Ruta_Salida += '_';
                                        if (XNA_Extractor.Extract.XnbExtractor.Extract(Ruta, Ruta_Salida + ".wav", false, false, true, false))
                                        {
                                            //Program.Eliminar_Archivo_Carpeta(Ruta); // Delete the copied XNB resource.
                                            continue; // Go to the next XNB resource file.
                                        }
                                    }
                                }
                                catch (Exception Excepción)
                                {
                                    //Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                }

                                /*try // Try to read as an effect and export as ?.
                                {
                                    Microsoft.Xna.Framework.Graphics.Effect Efecto = base.Content.Load<Effect>(Ruta_Relativa);
                                    if (Efecto != null)
                                    {
                                        Program.Eliminar_Archivo_Carpeta(Ruta); // Delete the copied XNB resource.
                                        continue; // Go to the next XNB resource file.
                                    }
                                }
                                catch (Exception Excepción)
                                {
                                    //Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                }*/

                                try // Try to read as a multiple type and export as a PNG image.
                                {
                                    while (File.Exists(Ruta_Salida + ".png")) Ruta_Salida += '_';
                                    if (XNA_Extractor.Extract.XnbExtractor.Extract(Ruta, Ruta_Salida + ".png", false, true, true, true))
                                    {
                                        Program.Eliminar_Archivo_Carpeta(Ruta); // Delete the copied XNB resource.
                                        continue; // Go to the next XNB resource file.
                                    }
                                }
                                catch (Exception Excepción)
                                {
                                    //Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                }

                                try // Try to read as a bank wave file and export as multiple WAV sound files.
                                {
                                    //while (File.Exists(Ruta_Salida + ".wav")) Ruta_Salida += '_';
                                    //MessageBox.Show(Path.GetDirectoryName(Ruta_Salida), Ruta_Salida);
                                    if (/*string.Compare(Path.GetExtension(Ruta), ".xwb", true) == 0 && */XNA_Extractor.Extract.XactExtractor.Extract(Ruta, Path.GetDirectoryName(Ruta)))
                                    {
                                        Program.Eliminar_Archivo_Carpeta(Ruta); // Delete the copied XNB resource.
                                        continue; // Go to the next XNB resource file.
                                    }
                                }
                                catch (Exception Excepción)
                                {
                                    //Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                }

                                // TODO: Add support for more resource types...
                            }
                        }
                        catch (Exception Excepción)
                        {
                            //Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                            continue;
                        }
                    }
                    Matriz_Rutas = null;
                }
                // This works perfectly, but the class from "XactExtractor.cs" seems to export too many files?
                if (Lista_Rutas_XSB != null && Lista_Rutas_XSB.Count > 0) // Post-process the track names if present.
                {
                    List<char> Lista_Caracteres_Inválidos = new List<char>(Path.GetInvalidFileNameChars());
                    foreach (string Ruta in Lista_Rutas_XSB)
                    {
                        try
                        {
                            FileStream Lector_Entrada = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                            if (Lector_Entrada.Length > 0L) // Not empty.
                            {
                                byte[] Matriz_Bytes = new byte[Lector_Entrada.Length]; // Read the whole file at once.
                                int Longitud = Lector_Entrada.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                Lector_Entrada.Close();
                                Lector_Entrada.Dispose();
                                Lector_Entrada = null;
                                if (Longitud < Matriz_Bytes.Length) Array.Resize(ref Matriz_Bytes, Longitud);
                                string Ruta_Actual = Path.GetDirectoryName(Ruta);
                                int Total_Pistas = 0;
                                for (int Índice = 1; Índice < int.MaxValue; Índice++)
                                {
                                    if (File.Exists(Ruta_Actual + "\\" + Índice.ToString() + " Unknown.wav"))
                                    {
                                        Total_Pistas++;
                                    }
                                    else break; // Stop when a track in order is missing.
                                }
                                List<string> Lista_Nombres = new List<string>();
                                int Índice_Anterior = Matriz_Bytes.Length - 1; // This byte should be zero (string ender).
                                for (int Índice = Matriz_Bytes.Length - 2; Índice >= 0; Índice--)
                                {
                                    if (Matriz_Bytes[Índice] == 0)
                                    {
                                        if (Índice + 1 != Índice_Anterior) // Avoid multiple nulls.
                                        {
                                            string Nombre = null;
                                            for (int Índice_Caracter = Índice + 1; Índice_Caracter < Índice_Anterior; Índice_Caracter++)
                                            {
                                                char Caracter = (char)Matriz_Bytes[Índice_Caracter];
                                                if (!char.IsControl(Caracter) && Caracter != 'ÿ' && !Lista_Caracteres_Inválidos.Contains(Caracter))
                                                {
                                                    Nombre += Caracter;
                                                }
                                            }
                                            if (string.IsNullOrEmpty(Nombre)) Nombre = "Unknown";
                                            Lista_Nombres.Add(Nombre);
                                            //if (Lista_Nombres.Count >= Total_Pistas) break;
                                        }
                                        Índice_Anterior = Índice;
                                    }
                                }
                                if (Lista_Nombres != null && Lista_Nombres.Count > 0)
                                {
                                    for (int Índice_Pista = Total_Pistas, Índice_Nombre = 0; Índice_Pista >= 1; Índice_Pista--, Índice_Nombre++)
                                    {
                                        try
                                        {
                                            File.Move(Ruta_Actual + "\\" + Índice_Pista.ToString() + " Unknown.wav", Ruta_Actual + "\\" + Índice_Pista.ToString() + " " + Lista_Nombres[Índice_Nombre] + ".wav");
                                        }
                                        catch (Exception Excepción)
                                        {
                                            //Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                            continue;
                                        }
                                    }
                                    Lista_Nombres.Reverse();
                                    string Ruta_Salida = Ruta.Substring(0, Ruta.Length - 4);
                                    while (File.Exists(Ruta_Salida + ".txt")) Ruta_Salida += '_';
                                    FileStream Lector = new FileStream(Ruta_Salida + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                    Lector.SetLength(0L);
                                    Lector.Seek(0L, SeekOrigin.Begin);
                                    StreamWriter Lector_Texto = new StreamWriter(Lector, Encoding.Unicode);
                                    foreach (string Nombre in Lista_Nombres)
                                    {
                                        try
                                        {
                                            Lector_Texto.WriteLine(Nombre);
                                            Lector_Texto.Flush();
                                        }
                                        catch (Exception Excepción)
                                        {
                                            //Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                            continue;
                                        }
                                    }
                                    Lector_Texto.Close();
                                    Lector_Texto.Dispose();
                                    Lector_Texto = null;
                                    Lector.Close();
                                    Lector.Dispose();
                                    Lector = null;
                                    Lista_Nombres = null;
                                    Program.Eliminar_Archivo_Carpeta(Ruta); // Delete the copied XNB resource.
                                    continue; // Go to the next XNB resource file.
                                }
                            }
                        }
                        catch (Exception Excepción)
                        {
                            Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                            continue;
                        }
                    }
                    Lista_Caracteres_Inválidos = null;
                }
                Depurador.Detener_Depurador();
                this.Exit();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        /*/// <summary>
        /// There's no real need to draw anything on the screen, so just exit at the same start.
        /// </summary>
        /// <param name="gametime"></param>
        protected override void Draw(GameTime gametime)
        {
            this.Exit();
        }*/
    }
}
