using ImageMagick;
using Microsoft.Win32;
using Minecraft_Tools.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    internal static class Program
    {
        /// <summary>
        /// The Windows registry build date, used to know if the older settings should be deleted.
        /// </summary>
        internal static readonly string Texto_Fecha = "2020_02_10_21_32_52_231";
        /// <summary>
        /// The Minecraft version that most tools of this application will support.
        /// </summary>
        internal static readonly string Texto_Minecraft_Versión = "1.15.2";

        /// <summary>
        /// This is only used to give this application to some users as a different edition, so the main tool will always be a pre-selected one.
        /// CheckState.Unchecked for all tools edition (default).
        /// CheckState.Checked for real time screen filters edition.
        /// CheckState.Indeterminate for multidimensional mathematical analyzer.
        /// </summary>
        internal static readonly CheckState Edición_Aplicación = CheckState.Unchecked;

        /// <summary>
        /// Since the application was first designed for "Xisumavoid", this will be the default user name, but can be changed later from the help menu.
        /// </summary>
        internal static string Texto_Usuario = Environment.UserName;
        internal static string Texto_Título = "Minecraft Tools by Jupisoft";
        internal static string Texto_Programa = "Minecraft Tools";
        internal static readonly string Texto_Versión = "1.15"; // Only update for each major version of Minecraft.
        internal static readonly string Texto_Versión_Fecha = Texto_Versión + " (" + Texto_Fecha/*.Replace("_", null)*/ + ")";
        internal static string Texto_Título_Versión = Texto_Título + " " + Texto_Versión;

        /// <summary>
        /// Using this icon instead of adding it to the designer of each form saved almost 11 MB of space in the whole application. Why doesn't .NET make a CRC of the icon and only adds it once to the whole project?
        /// </summary>
        internal static Icon Icono_Jupisoft = null;

        /// <summary>
        /// The biggest square image that can be created with .NET has 26.754 x 26.754 pixels. This variable is just to quickly remember that number if a colossal square image is needed. Discovered by Jupisoft on 2019_02_18_19_53_24_223.
        /// </summary>
        internal static int Ancho_Alto_Máximo_Imagen = 26754;

        /// <summary>
        /// Brush used to emulate the progress bars green color, since those update with a delay are a bit
        /// useless in this application, so it's better to "simulate" them with a graphics object.
        /// </summary>
        internal static readonly SolidBrush Pincel_Progreso = new SolidBrush(Color.FromArgb(255, 6, 176, 37));

        /// <summary>
        /// Function used to obtain a new image with a "simulated" progress bar drawn on it.
        /// </summary>
        /// <param name="Dimensiones">The client size of the desired picture box or the size of the progress bar image.</param>
        /// <param name="Ancho_Progreso">Any value between zero and the width of the image.</param>
        /// <returns>Returns a new image with the "simulated" progress bar. Returns null on any error.</returns>
        internal static Bitmap Obtener_Imagen_Barra_Progreso(Size Dimensiones, int Ancho_Progreso)
        {
            try
            {
                if (Dimensiones.Width > 0 && Dimensiones.Height > 0)
                {
                    if (Ancho_Progreso < 0) Ancho_Progreso = 0;
                    else if (Ancho_Progreso > Dimensiones.Width) Ancho_Progreso = Dimensiones.Width;
                    Bitmap Imagen = new Bitmap(Dimensiones.Width, Dimensiones.Height, PixelFormat.Format32bppArgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.HighQuality;
                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                    Pintar.FillRectangle(Pincel_Progreso, 0, 0, Ancho_Progreso, Dimensiones.Height);
                    Pintar.Dispose();
                    Pintar = null;
                    return Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static readonly string Ruta_Minecraft = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft";
        internal static readonly string Ruta_Guardado_Minecraft = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft\\saves";
        internal static readonly string Ruta_Twitch = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Curse";
        internal static readonly string Ruta_Guardado_Twitch = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Curse\\Minecraft\\Instances";
        internal static readonly string Ruta_Guardado_Imágenes = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools";
        internal static readonly string Ruta_Guardado_Imágenes_Afinador_Bloques_Nota = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\Note Blocks Tuner";
        internal static readonly string Ruta_Guardado_Imágenes_Buscador_Chunks_Limos = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\Slime Chunks Finder";
        internal static readonly string Ruta_Guardado_Imágenes_Buscador_JAR = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\JAR Finder";
        internal static readonly string Ruta_Guardado_Imágenes_Conversor_Mundos_1_13_1_12_2 = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\1.13 to 1.12.2- world converter";
        internal static readonly string Ruta_Guardado_Imágenes_Generador_Miniaturas_Color_Medio = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\Thumbnails and Average Color";
        internal static readonly string Ruta_Guardado_Imágenes_Diseñador_Estandartes = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\Banner Designer";
        internal static readonly string Ruta_Guardado_Imágenes_Espirales = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\Spiral Images";
        internal static readonly string Ruta_Guardado_Imágenes_Exportador_Estructuras_Pintadas = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\Painted Structures";
        internal static readonly string Ruta_Guardado_Imágenes_Estructuras_Personalizadas = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\Custom Structures";
        internal static readonly string Ruta_Guardado_Imágenes_Filtros_Tiempo_Real = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\Real Time Filters";
        internal static readonly string Ruta_Guardado_Imágenes_Información_Miembros_Hermitcraft = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\Hermitcraft Members Information";
        internal static readonly string Ruta_Guardado_Imágenes_Números_Primos = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\Prime Numbers";
        internal static readonly string Ruta_Guardado_Imágenes_Paletas = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\Palettes";
        internal static readonly string Ruta_Guardado_Imágenes_Pixel_Art = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\Pixel Art";
        internal static readonly string Ruta_Guardado_Imágenes_Realistic_World_Viewer_2D = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\Realistic World Viewer in 2D";
        internal static readonly string Ruta_Guardado_Imágenes_Reloj_Minecraft_Tiempo_Real = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\Real Time Minecraft Clock";
        internal static readonly string Ruta_Guardado_Imágenes_Salvapantallas_Bloques = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\Blocks screen saver";
        internal static readonly string Ruta_Guardado_Imágenes_Secretos = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\Secrets";
        internal static readonly string Ruta_Guardado_Imágenes_Visor_Cuadros = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\Paintings Viewer";
        internal static readonly string Ruta_Guardado_Imágenes_Visor_NBT = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\NBT Viewer";
        internal static readonly string Ruta_Guardado_Imágenes_Visor_Nombres_Encantamientos = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\Enchantment Names Viewer";
        internal static readonly string Ruta_Guardado_Imágenes_Visor_Ofertas_Aldeanos = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft\\Minecraft Tools\\Villager Tradings Viewer";

        internal static Random Rand = new Random();
        internal static Kuiper.Clerom.Xoroshiro128p Rand_Xoroshiro128p = new Kuiper.Clerom.Xoroshiro128p(true);
        internal static List<char> Lista_Caracteres_Prohibidos = new List<char>();
        internal static readonly char Caracter_Coma_Decimal = (0.5d).ToString()[1];
        internal static readonly char Caracter_Punto_Decimal = Caracter_Coma_Decimal != '.' ? '.' : ',';
        internal static readonly char Caracter_Signo_Negativo = (-1).ToString()[0];

        internal static readonly Bitmap Imagen_Transparente = new Bitmap(16, 16, PixelFormat.Format32bppArgb);
        internal static HatchBrush Pincel_Trama = new HatchBrush(HatchStyle.Percent50, Color.FromArgb(128, 0, 0, 0), Color.Transparent);
        internal static TextureBrush Pincel_Fondo = new TextureBrush(Resources.Fondo, WrapMode.Tile);

        /// <summary>
        /// Path to the folder near this application that should contain the 79 cards inside, compressed as indexed PNG images with only 256 colors per card to save space.
        /// </summary>
        internal static readonly string Ruta_Cartas_79 = Application.StartupPath + "\\Cards";
        /// <summary>
        /// Array used to store the 79 cards used in this application by the "Magic card guessing" and "Line of life" tools.
        /// </summary>
        internal static Bitmap[] Matriz_Cartas_79 = null;

        internal static class HSL
        {
            /// <summary>
            /// Convierte un color RGB en uno HSL.
            /// </summary>
            /// <param name="Rojo">Valor entre 0 y 255.</param>
            /// <param name="Verde">Valor entre 0 y 255.</param>
            /// <param name="Azul">Valor entre 0 y 255.</param>
            /// <param name="Matiz">Valor entre 0 y 360.</param>
            /// <param name="Saturación">Valor entre 0 y 100.</param>
            /// <param name="Luminosidad">Valor entre 0 y 100.</param>
            internal static void From_RGB(byte Rojo, byte Verde, byte Azul, out double Matiz, out double Saturación, out double Luminosidad)
            {
                Matiz = 0d;
                Saturación = 0d;
                Luminosidad = 0d;
                double Rojo_1 = Rojo / 255d;
                double Verde_1 = Verde / 255d;
                double Azul_1 = Azul / 255d;
                double Máximo, Mínimo, Diferencia;
                Máximo = Math.Max(Rojo_1, Math.Max(Verde_1, Azul_1));
                Mínimo = Math.Min(Rojo_1, Math.Min(Verde_1, Azul_1));
                Luminosidad = (Mínimo + Máximo) / 2d;
                if (Luminosidad <= 0d) return;
                Diferencia = Máximo - Mínimo;
                Saturación = Diferencia;
                if (Saturación > 0d) Saturación /= (Luminosidad <= 0.5d) ? (Máximo + Mínimo) : (2d - Máximo - Mínimo);
                else
                {
                    //Luminosidad = Math.Round(Luminosidad * 100d, 1, MidpointRounding.AwayFromZero);
                    Luminosidad *= Luminosidad * 100d;
                    return;
                }
                double Rojo_2 = (Máximo - Rojo_1) / Diferencia;
                double Verde_2 = (Máximo - Verde_1) / Diferencia;
                double Azul_2 = (Máximo - Azul_1) / Diferencia;
                if (Rojo_1 == Máximo) Matiz = (Verde_1 == Mínimo ? 5d + Azul_2 : 1d - Verde_2);
                else if (Verde_1 == Máximo) Matiz = (Azul_1 == Mínimo ? 1d + Rojo_2 : 3d - Azul_2);
                else Matiz = (Rojo_1 == Mínimo ? 3d + Verde_2 : 5d - Rojo_2);
                Matiz /= 6d;
                if (Matiz >= 1d) Matiz = 0d;
                Matiz *= 360d;
                Saturación *= 100d;
                Luminosidad *= 100d;
                //if (Matiz < 0d || Matiz >= 360d) MessageBox.Show("To Matiz", Matiz.ToString());
                //if (Saturación < 0d || Saturación > 100d) MessageBox.Show("To Saturación");
                //if (Luminosidad < 0d || Luminosidad > 100d) MessageBox.Show("To Luminosidad");
                //Matiz = Math.Round(Matiz * 360d, 1, MidpointRounding.AwayFromZero); // 0.0d ~ 360.0d
                //Saturación = Math.Round(Saturación * 100d, 1, MidpointRounding.AwayFromZero); // 0.0d ~ 100.0d
                //Luminosidad = Math.Round(Luminosidad * 100d, 1, MidpointRounding.AwayFromZero); // 0.0d ~ 100.0d
                //if (Matiz >= 360d) Matiz = 0d;
            }

            /// <summary>
            /// Convierte un color HSL en uno RGB.
            /// </summary>
            /// <param name="Matiz">Valor entre 0 y 360.</param>
            /// <param name="Saturación">Valor entre 0 y 100.</param>
            /// <param name="Luminosidad">Valor entre 0 y 100.</param>
            /// <param name="Rojo">Valor entre 0 y 255.</param>
            /// <param name="Verde">Valor entre 0 y 255.</param>
            /// <param name="Azul">Valor entre 0 y 255.</param>
            internal static void To_RGB(double Matiz, double Saturación, double Luminosidad, out byte Rojo, out byte Verde, out byte Azul)
            {
                if (Matiz >= 360d) Matiz = 0d;
                //Matiz = Math.Round(Matiz, 1, MidpointRounding.AwayFromZero);
                //Saturación = Math.Round(Saturación, 1, MidpointRounding.AwayFromZero);
                //Luminosidad = Math.Round(Luminosidad, 1, MidpointRounding.AwayFromZero);
                Matiz /= 360d; // 0.0d ~ 1.0d
                Saturación /= 100d; // 0.0d ~ 1.0d
                Luminosidad /= 100d; // 0.0d ~ 1.0d
                double Rojo_Temporal = Luminosidad; // Default to Gray
                double Verde_Temporal = Luminosidad;
                double Azul_Temporal = Luminosidad;
                double v = Luminosidad <= 0.5d ? (Luminosidad * (1d + Saturación)) : (Luminosidad + Saturación - Luminosidad * Saturación);
                if (v > 0d)
                {
                    double m, sv, Sextante, fract, vsf, mid1, mid2;
                    m = Luminosidad + Luminosidad - v;
                    sv = (v - m) / v;
                    Matiz *= 6d;
                    Sextante = Math.Floor(Matiz);
                    fract = Matiz - Sextante;
                    vsf = v * sv * fract;
                    mid1 = m + vsf;
                    mid2 = v - vsf;
                    if (Sextante == 0d)
                    {
                        Rojo_Temporal = v;
                        Verde_Temporal = mid1;
                        Azul_Temporal = m;
                    }
                    else if (Sextante == 1d)
                    {
                        Rojo_Temporal = mid2;
                        Verde_Temporal = v;
                        Azul_Temporal = m;
                    }
                    else if (Sextante == 2d)
                    {
                        Rojo_Temporal = m;
                        Verde_Temporal = v;
                        Azul_Temporal = mid1;
                    }
                    else if (Sextante == 3d)
                    {
                        Rojo_Temporal = m;
                        Verde_Temporal = mid2;
                        Azul_Temporal = v;
                    }
                    else if (Sextante == 4d)
                    {
                        Rojo_Temporal = mid1;
                        Verde_Temporal = m;
                        Azul_Temporal = v;
                    }
                    else if (Sextante == 5d)
                    {
                        Rojo_Temporal = v;
                        Verde_Temporal = m;
                        Azul_Temporal = mid2;
                    }
                }
                Rojo = (byte)Math.Round(Rojo_Temporal * 255d, MidpointRounding.AwayFromZero);
                Verde = (byte)Math.Round(Verde_Temporal * 255d, MidpointRounding.AwayFromZero);
                Azul = (byte)Math.Round(Azul_Temporal * 255d, MidpointRounding.AwayFromZero);
            }

            /// <summary>
            /// Obtains a hue value between 0 and 11 for the specified color, or 12 if it's in gray scale.
            /// </summary>
            /// <param name="Rojo">Red value between 0 and 255.</param>
            /// <param name="Verde">Green value between 0 and 255.</param>
            /// <param name="Azul">Blue value between 0 and 255.</param>
            /// <returns>Returns a value between 0 and 11, or 12 if the color it's in gray scale or on any error.</returns>
            internal static int Obtener_Matiz_0_a_11(byte Rojo, byte Verde, byte Azul)
            {
                try
                {
                    if (Rojo != Verde || Rojo != Azul) // Not gray.
                    {
                        double Rojo_1 = Rojo / 255d;
                        double Verde_1 = Verde / 255d;
                        double Azul_1 = Azul / 255d;
                        double Mínimo = Math.Min(Rojo_1, Math.Min(Verde_1, Azul_1));
                        double Máximo = Math.Max(Rojo_1, Math.Max(Verde_1, Azul_1));
                        double Diferencia = Máximo - Mínimo;
                        double Rojo_2 = (Máximo - Rojo_1) / Diferencia;
                        double Verde_2 = (Máximo - Verde_1) / Diferencia;
                        double Azul_2 = (Máximo - Azul_1) / Diferencia;
                        double Matiz_Temporal = 0d;
                        if (Rojo_1 == Máximo) Matiz_Temporal = (Verde_1 == Mínimo ? 5d + Azul_2 : 1d - Verde_2);
                        else if (Verde_1 == Máximo) Matiz_Temporal = (Azul_1 == Mínimo ? 1d + Rojo_2 : 3d - Azul_2);
                        else Matiz_Temporal = (Rojo_1 == Mínimo ? 3d + Verde_2 : 5d - Rojo_2);
                        if (Matiz_Temporal >= 6d) Matiz_Temporal = 0d;
                        int Matiz = (int)(Matiz_Temporal * 510d);
                        //int Matiz = (int)(Matiz_Temporal * 2d);
                        if (Matiz >= 0 || Matiz <= 1529)
                        {
                            if (Matiz > 2933 || Matiz <= 128) return 0;
                            else if (Matiz <= 383) return 1;
                            else if (Matiz <= 638) return 2;
                            else if (Matiz <= 893) return 3;
                            else if (Matiz <= 1148) return 4;
                            else if (Matiz <= 1403) return 5;
                            else if (Matiz <= 1658) return 6;
                            else if (Matiz <= 1913) return 7;
                            else if (Matiz <= 2168) return 8;
                            else if (Matiz <= 2423) return 9;
                            else if (Matiz <= 2678) return 10;
                            else return 11;
                        }
                        else Matiz = 12;
                        return Matiz;
                    }
                    /*int Matiz = Obtener_Matiz_0_a_1529(Rojo, Verde, Azul);
                    if (Matiz != 1530)
                    {
                        if (Matiz > 2933 || Matiz <= 128) return 0;
                        else if (Matiz <= 383) return 1;
                        else if (Matiz <= 638) return 2;
                        else if (Matiz <= 893) return 3;
                        else if (Matiz <= 1148) return 4;
                        else if (Matiz <= 1403) return 5;
                        else if (Matiz <= 1658) return 6;
                        else if (Matiz <= 1913) return 7;
                        else if (Matiz <= 2168) return 8;
                        else if (Matiz <= 2423) return 9;
                        else if (Matiz <= 2678) return 10;
                        else return 11;
                    }*/
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                return 12; // Gray.
            }

            /// <summary>
            /// Obtains a hue value between 0 and 1529 for the specified color, or 1530 if it's in gray scale.
            /// </summary>
            /// <param name="Rojo">Red value between 0 and 255.</param>
            /// <param name="Verde">Green value between 0 and 255.</param>
            /// <param name="Azul">Blue value between 0 and 255.</param>
            /// <returns>Returns a value between 0 and 1529, or 1530 if the color it's in gray scale or on any error.</returns>
            internal static int Obtener_Matiz_0_a_1529(byte Rojo, byte Verde, byte Azul)
            {
                try
                {
                    if (Rojo != Verde || Rojo != Azul) // Not gray.
                    {
                        double Rojo_1 = Rojo / 255d;
                        double Verde_1 = Verde / 255d;
                        double Azul_1 = Azul / 255d;
                        double Mínimo = Math.Min(Rojo_1, Math.Min(Verde_1, Azul_1));
                        double Máximo = Math.Max(Rojo_1, Math.Max(Verde_1, Azul_1));
                        double Diferencia = Máximo - Mínimo;
                        double Rojo_2 = (Máximo - Rojo_1) / Diferencia;
                        double Verde_2 = (Máximo - Verde_1) / Diferencia;
                        double Azul_2 = (Máximo - Azul_1) / Diferencia;
                        double Matiz_Temporal = 0d;
                        if (Rojo_1 == Máximo) Matiz_Temporal = (Verde_1 == Mínimo ? 5d + Azul_2 : 1d - Verde_2);
                        else if (Verde_1 == Máximo) Matiz_Temporal = (Azul_1 == Mínimo ? 1d + Rojo_2 : 3d - Azul_2);
                        else Matiz_Temporal = (Rojo_1 == Mínimo ? 3d + Verde_2 : 5d - Rojo_2);
                        if (Matiz_Temporal >= 6d) Matiz_Temporal = 0d;
                        int Matiz = (int)(Matiz_Temporal * 255d);
                        if (Matiz < 0 || Matiz > 1529) Matiz = 1530;
                        return Matiz;
                    }
                    /*if (Rojo != Verde || Rojo != Azul)
                    {
                        byte Mínimo = Math.Min(Rojo, Math.Min(Verde, Azul));
                        byte Máximo = Math.Max(Rojo, Math.Max(Verde, Azul));
                        double Diferencia = (double)(Máximo - Mínimo);
                        double Matiz_Temporal = 0d;
                        if (Rojo == Máximo)
                        {
                            if (Verde == Mínimo) Matiz_Temporal = 1275d + ((double)(Máximo - Azul) / Diferencia);
                            else Matiz_Temporal = 255d - ((double)(Máximo - Verde) / Diferencia);
                        }
                        else if (Verde == Máximo)
                        {
                            if (Azul == Mínimo) Matiz_Temporal = 255d + ((double)(Máximo - Rojo) / Diferencia);
                            else Matiz_Temporal = 765d - ((double)(Máximo - Azul) / Diferencia);
                        }
                        else
                        {
                            if (Rojo == Mínimo) Matiz_Temporal = 765d + ((double)(Máximo - Verde) / Diferencia);
                            else Matiz_Temporal = 1275d - ((double)(Máximo - Rojo) / Diferencia);
                        }
                        int Matiz = (int)Matiz_Temporal;
                        if (Matiz < 0) Matiz = 0;
                        else if (Matiz > 1529) Matiz = 0;
                        return Matiz;
                    }*/
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                return 1530; // Gray.
            }

            internal static byte Obtener_Matiz_0_a_255(byte Rojo, byte Verde, byte Azul)
            {
                /*int Matiz = 0;
                int Saturación = 0;
                int Luminosidad = 0;
                //double Rojo_1 = Rojo / 255d;
                //double Verde_1 = Verde / 255d;
                //double Azul_1 = Azul / 255d;
                //double Máximo, Mínimo, Diferencia;
                int Máximo = Math.Max(Rojo, Math.Max(Verde, Azul));
                int Mínimo = Math.Min(Rojo, Math.Min(Verde, Azul));
                Luminosidad = (Mínimo + Máximo) / 2;
                if (Luminosidad <= 0) return 0;
                int Diferencia = Máximo - Mínimo;
                Saturación = Diferencia;
                if (Saturación > 0) Saturación /= (Luminosidad <= 128) ? (Máximo + Mínimo) : (510 - Máximo - Mínimo);
                else
                {
                    //Luminosidad = Math.Round(Luminosidad * 100d, 1, MidpointRounding.AwayFromZero);
                    return 0;
                }
                int Rojo_2 = (Máximo - Rojo) / Diferencia;
                int Verde_2 = (Máximo - Verde) / Diferencia;
                int Azul_2 = (Máximo - Azul) / Diferencia;
                if (Rojo == Máximo) Matiz = (Verde == Mínimo ? 1275 + Azul_2 : 255 - Verde_2);
                else if (Verde == Máximo) Matiz = (Azul == Mínimo ? 255 + Rojo_2 : 765 - Azul_2);
                else Matiz = (Rojo == Mínimo ? 765 + Verde_2 : 1275 - Rojo_2);
                if (Matiz >= 1530) Matiz = 0;
                Matiz /= 6;

                //Matiz *= 360d;
                //Saturación *= 100d;
                //Luminosidad *= 100d;



                if (Rojo != Verde || Rojo != Azul)
                {
                    int Matiz = 0;
                    Byte Mínimo = Math.Min(Rojo, Math.Min(Verde, Azul));
                    Byte Máximo = Math.Max(Rojo, Math.Max(Verde, Azul));
                    if (Rojo == Máximo) Matiz = (Verde == Mínimo ? (5 * 255) + (((Máximo - Azul) * 255) / (Máximo - Mínimo)) : (1 * 255) - (((Máximo - Verde) * 255) / (Máximo - Mínimo)));
                    else if (Verde == Máximo) Matiz = (Azul == Mínimo ? (1 * 255) + (((Máximo - Rojo) * 255) / (Máximo - Mínimo)) : (3 * 255) - (((Máximo - Azul) * 255) / (Máximo - Mínimo)));
                    else Matiz = (Rojo == Mínimo ? (3 * 255) + (((Máximo - Verde) * 255) / (Máximo - Mínimo)) : (5 * 255) - (((Máximo - Rojo) * 255) / (Máximo - Mínimo)));
                    Matiz++; // 2013_02_10_09_13_04_593
                    if (Matiz >/*=*//* 1530) Matiz = 0;
                    return (Byte)(Matiz / 6);
                }*/
                return 0;
            }

            internal static byte Obtener_Saturación_0_a_255(byte Rojo, byte Verde, byte Azul)
            {
                if (Rojo != Verde || Rojo != Azul)
                {
                    byte Mínimo = Math.Min(Rojo, Math.Min(Verde, Azul));
                    byte Máximo = Math.Max(Rojo, Math.Max(Verde, Azul));
                    return (byte)(((Máximo - Mínimo) * 255) / ((((Mínimo + Máximo) / 2) <= 128) ? (Máximo + Mínimo) : (510 - Máximo - Mínimo)));
                }
                return 0;
            }

            internal static byte Obtener_Brillo_0_a_255(byte Rojo, byte Verde, byte Azul)
            {
                return (byte)((Math.Min(Rojo, Math.Min(Verde, Azul)) + Math.Max(Rojo, Math.Max(Verde, Azul))) / 2);
            }
        }

        /// <summary>
        /// Function designed to convert any ARGB color into a 3D one based on a height value. Note that the sea level ends at Y = 62.
        /// </summary>
        /// <param name="Color_ARGB">Any valid ARGB value.</param>
        /// <param name="Y">Any desired height value between 0 and 255.</param>
        /// <param name="Color_3D">True to generate a new 3D color. False to return the original color.</param>
        /// <returns>Returns the modified ARGB color based on the specified height. Returns null on any error.</returns>
        internal static Color Obtener_Color_3D(Color Color_ARGB, int Y, bool Color_3D)
        {
            try
            {
                if (Color_3D)
                {
                    if (Y < 62) // Below the sea level (Darker color)
                    {
                        int Diferencia = Y; // Height difference from the sea level.
                        int Rojo = Color_ARGB.R + ((64 * Diferencia) / 61);
                        int Verde = Color_ARGB.G + ((64 * Diferencia) / 61);
                        int Azul = Color_ARGB.B + ((64 * Diferencia) / 61);
                        if (Rojo < 0) Rojo = 0;
                        else if (Rojo > 255) Rojo = 255;
                        if (Verde < 0) Verde = 0;
                        else if (Verde > 255) Verde = 255;
                        if (Azul < 0) Azul = 0;
                        else if (Azul > 255) Azul = 255;
                        return Color.FromArgb(255, Rojo, Verde, Azul);
                    }
                    else if (Y > 62) // Above the sea level (Brighter color)
                    {
                        int Diferencia = 192 - (255 - Y); // Inverted height difference from the sea level.
                        int Rojo = Color_ARGB.R + ((64 * Diferencia) / 192);
                        int Verde = Color_ARGB.G + ((64 * Diferencia) / 192);
                        int Azul = Color_ARGB.B + ((64 * Diferencia) / 192);
                        if (Rojo < 0) Rojo = 0;
                        else if (Rojo > 255) Rojo = 255;
                        if (Verde < 0) Verde = 0;
                        else if (Verde > 255) Verde = 255;
                        if (Azul < 0) Azul = 0;
                        else if (Azul > 255) Azul = 255;
                        return Color.FromArgb(255, Rojo, Verde, Azul);
                    }
                    /*if (Y != 64) // 64 = Original color
                    {
                        if (Y < 64) // From 0 to 63 = Darker color
                        {
                            int Multiplicador = ((Y + 1) / 2) + 32;
                            Color_ARGB = Color.FromArgb(255, (byte)(Math.Min(256, Math.Max(1, ((Color_ARGB.R + 1) * Multiplicador) / 64)) - 1), (byte)(Math.Min(256, Math.Max(1, ((Color_ARGB.G + 1) * Multiplicador) / 64)) - 1), (byte)(Math.Min(256, Math.Max(1, ((Color_ARGB.B + 1) * Multiplicador) / 64)) - 1));
                        }
                        else // From 65 to 128 = Brighter color
                        {
                            int Divisor = (32 - ((Math.Min(128, Y) - 65) / 2)) + 32;
                            Color_ARGB = Color.FromArgb(255, (byte)(Math.Min(256, Math.Max(1, ((Color_ARGB.R + 1) * 64) / Divisor)) - 1), (byte)(Math.Min(256, Math.Max(1, ((Color_ARGB.G + 1) * 64) / Divisor)) - 1), (byte)(Math.Min(256, Math.Max(1, ((Color_ARGB.B + 1) * 64) / Divisor)) - 1));
                        }
                    }*/
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return Color_ARGB;
        }

        internal static Color Obtener_Color_Puro_0_a_11(int Matiz)
        {
            try
            {
                if (Matiz == 0) return Color.FromArgb(255, 0, 0);
                else if (Matiz == 1) return Color.FromArgb(255, 160, 0);
                else if (Matiz == 2) return Color.FromArgb(255, 255, 0);
                else if (Matiz == 3) return Color.FromArgb(160, 255, 0);
                else if (Matiz == 4) return Color.FromArgb(0, 255, 0);
                else if (Matiz == 5) return Color.FromArgb(0, 255, 160);
                else if (Matiz == 6) return Color.FromArgb(0, 255, 255);
                else if (Matiz == 7) return Color.FromArgb(0, 160, 255);
                else if (Matiz == 8) return Color.FromArgb(0, 0, 255);
                else if (Matiz == 9) return Color.FromArgb(160, 0, 255);
                else if (Matiz == 10) return Color.FromArgb(255, 0, 255);
                else if (Matiz == 11) return Color.FromArgb(255, 0, 160);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return Color.FromArgb(255, 255, 255);
        }

        /// <summary>
        /// Function that returns one of the 1.530 possible 24 bits RGB colors with full saturation and middle brightness.
        /// </summary>
        /// <param name="Índice">Any value between 0 and 1529. Red = 0, Yellow = 255, Green = 510, Cyan = 765, blue = 1020, purple = 1275. If the value is below 0 or above 1529, pure white will be returned instead.</param>
        /// <returns>Returns an ARGB color based on the selected index, or white if out of bounds.</returns>
        internal static Color Obtener_Color_Puro_1530(int Índice)
        {
            try
            {
                if (Índice >= 0 && Índice <= 1529)
                {
                    if (Índice < 255) return Color.FromArgb(255, Índice, 0);
                    else if (Índice < 510) return Color.FromArgb(510 - Índice, 255, 0);
                    else if (Índice < 765) return Color.FromArgb(0, 255, 255 - (765 - Índice));
                    else if (Índice < 1020) return Color.FromArgb(0, 1020 - Índice, 255);
                    else if (Índice < 1275) return Color.FromArgb(255 - (1275 - Índice), 0, 255);
                    else return Color.FromArgb(255, 0, 1530 - Índice);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return Color.FromArgb(255, 255, 255);
        }

        /// <summary>
        /// Obtains the negative from any color.
        /// </summary>
        /// <param name="Color_ARGB">Any valid color.</param>
        /// <returns>Returns the negative of the desired color. The alpha value remains unchanged.</returns>
        internal static Color Negativizar_Color(Color Color_ARGB)
        {
            try
            {
                if (Color_ARGB != Color.Empty)
                {
                    return Color.FromArgb(Color_ARGB.A, 255 - Color_ARGB.R, 255 - Color_ARGB.G, 255 - Color_ARGB.B);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return Color_ARGB;
        }

        /// <summary>
        /// Calculates the CRC 32 of any file in the same way as WinRAR or 7-Zip. Very useful to see if 2 files are identical or contain differences between them.
        /// </summary>
        /// <param name="Ruta">Any valid and existing file path.</param>
        /// <returns>A positive number of 32 bits based on the bytes contained in the file.</returns>
        internal static uint Obtener_CRC_32(string Ruta)
        {
            uint CRC_32 = 0xFFFFFFFF; // Start with the bits inverted.
            try
            {
                if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                {
                    FileStream Lector = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    Lector.Seek(0L, SeekOrigin.Begin);
                    byte[] Matriz_Bytes = new byte[4096]; // Use a 4 KB matrix for reading.
                    for (long Índice_Bloque = 0L; Índice_Bloque < Lector.Length; Índice_Bloque += 4096L)
                    {
                        int Longitud = Lector.Read(Matriz_Bytes, 0, 4096); // Read in blocks of 4 KB.
                        for (int Índice_Byte = 0; Índice_Byte < Longitud; Índice_Byte++)
                        {
                            CRC_32 = Matriz_CRC_32[(byte)(CRC_32 ^ Matriz_Bytes[Índice_Byte])] ^ (CRC_32 >> 8); // Add the new value to the previous CRC 32.
                        }
                    }
                    Matriz_Bytes = null;
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return ~CRC_32; // Return the calculated bits inverted (if it's 0xFFFFFFFF will return 0).
        }

        /// <summary>
        /// Reverses the order of the words in any string, and turns the first letter to uppercase.
        /// </summary>
        /// <param name="Nombre">Any valid string with some words.</param>
        /// <returns>The string with it's words in inverted order. Returns null on any error.</returns>
        internal static string Obtener_Nombre_Invertido(string Nombre)
        {
            try
            {
                string Nombre_Invertido = null;
                string[] Matriz_Palabras = Nombre.ToLowerInvariant().Replace(' ', '_').Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
                for (int Índice_Palabra = Matriz_Palabras.Length - 1; Índice_Palabra >= 0; Índice_Palabra--)
                {
                    Nombre_Invertido += Matriz_Palabras[Índice_Palabra] + (Índice_Palabra > 0 ? "_" : null);
                }
                Nombre = Nombre.Substring(0, 1).ToUpperInvariant() + Nombre.Substring(1).Replace('_', ' ');
                Nombre_Invertido = Nombre_Invertido.Substring(0, 1).ToUpperInvariant() + Nombre_Invertido.Substring(1).Replace('_', ' ');
                return Nombre_Invertido;
            }
            catch { }
            return null;
        }

        internal static string Obtener_Nombre_Temporal()
        {
            try
            {
                DateTime Fecha = DateTime.Now;
                string Año = Fecha.Year.ToString();
                string Mes = Fecha.Month.ToString();
                string Día = Fecha.Day.ToString();
                string Hora = Fecha.Hour.ToString();
                string Minuto = Fecha.Minute.ToString();
                string Segundo = Fecha.Second.ToString();
                string Milisegundo = Fecha.Millisecond.ToString();
                while (Año.Length < 4) Año = '0' + Año;
                while (Mes.Length < 2) Mes = '0' + Mes;
                while (Día.Length < 2) Día = '0' + Día;
                while (Hora.Length < 2) Hora = '0' + Hora;
                while (Minuto.Length < 2) Minuto = '0' + Minuto;
                while (Segundo.Length < 2) Segundo = '0' + Segundo;
                while (Milisegundo.Length < 3) Milisegundo = '0' + Milisegundo;
                return Año + "_" + Mes + "_" + Día + "_" + Hora + "_" + Minuto + "_" + Segundo + "_" + Milisegundo;
            }
            catch { }
            return "0000_00_00_00_00_00_000";
        }

        internal static string Obtener_Nombre_Temporal_Sin_Guiones()
        {
            try
            {
                DateTime Fecha = DateTime.Now;
                string Año = Fecha.Year.ToString();
                string Mes = Fecha.Month.ToString();
                string Día = Fecha.Day.ToString();
                string Hora = Fecha.Hour.ToString();
                string Minuto = Fecha.Minute.ToString();
                string Segundo = Fecha.Second.ToString();
                string Milisegundo = Fecha.Millisecond.ToString();
                while (Año.Length < 4) Año = '0' + Año;
                while (Mes.Length < 2) Mes = '0' + Mes;
                while (Día.Length < 2) Día = '0' + Día;
                while (Hora.Length < 2) Hora = '0' + Hora;
                while (Minuto.Length < 2) Minuto = '0' + Minuto;
                while (Segundo.Length < 2) Segundo = '0' + Segundo;
                while (Milisegundo.Length < 3) Milisegundo = '0' + Milisegundo;
                return Año + Mes + Día + Hora + Minuto + Segundo + Milisegundo;
            }
            catch { }
            return "00000000000000000";
        }

        /// <summary>
        /// Returns a full path to the user's desktop with a temporary name for the file. Very useful for quick exporting or testing of several new functions.
        /// </summary>
        /// <returns></returns>
        internal static string Obtener_Ruta_Temporal_Escritorio()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + Obtener_Nombre_Temporal();
        }

        internal static string Traducir_Texto_Mayúsculas_Minúsculas_Automáticamente(string Nombre)
        {
            try
            {
                if (!string.IsNullOrEmpty(Nombre))
                {
                    Nombre = Nombre.ToLowerInvariant();
                    string Texto = null;
                    bool Letra_Anterior = false;
                    for (int Índice_Caracter = 0; Índice_Caracter < Nombre.Length; Índice_Caracter++)
                    {
                        if (char.IsLetter(Nombre[Índice_Caracter]))
                        {
                            if (!Letra_Anterior)
                            {
                                Texto += char.ToUpperInvariant(Nombre[Índice_Caracter]);
                                Letra_Anterior = true;
                            }
                            else
                            {
                                Texto += Nombre[Índice_Caracter];
                                Letra_Anterior = true;
                            }
                        }
                        else
                        {
                            Texto += Nombre[Índice_Caracter];
                            Letra_Anterior = false;
                        }
                    }
                    return Texto;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return Nombre;
        }
        
        /// <summary>
        /// Checks if all the image resources of a folder are in 32 bits with alpha format. Note: windows might say some images are of 8 bits with palette and .NET say they are 32 bits with alpha, so I'm not sure which one should be trusted in this.
        /// </summary>
        /// <param name="Ruta">Any valid directory path that contains valid images.</param>
        internal static void Verificar_Texturas_32_Bits_Alfa(string Ruta)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta) && Directory.Exists(Ruta))
                {
                    string[] Matriz_Rutas = Directory.GetFiles(Ruta, "*", SearchOption.TopDirectoryOnly);
                    if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                    {
                        List<string> Lista_Rutas = new List<string>();
                        foreach (string Ruta_Imagen in Matriz_Rutas)
                        {
                            try
                            {
                                FileStream Lector = new FileStream(Ruta_Imagen, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                                Image Imagen_Original = null;
                                try { Imagen_Original = Image.FromStream(Lector, false, false); }
                                catch { Imagen_Original = null; }
                                if (Imagen_Original != null)
                                {
                                    if (Imagen_Original.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                                    {
                                        Lista_Rutas.Add(Ruta_Imagen);
                                    }
                                    Imagen_Original.Dispose();
                                    Imagen_Original = null;
                                }
                                else Lista_Rutas.Add(Ruta_Imagen);
                                Lector.Close();
                                Lector.Dispose();
                                Lector = null;
                            }
                            catch (Exception Excepción)
                            {
                                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                MessageBox.Show(Ruta_Imagen, "Error");
                                continue;
                            }
                        }
                        if (Lista_Rutas.Count > 0)
                        {
                            if (Lista_Rutas.Count > 1) Lista_Rutas.Sort();
                            File.WriteAllLines(Program.Obtener_Ruta_Temporal_Escritorio() + ".txt", Lista_Rutas.ToArray(), Encoding.Unicode);
                        }
                        MessageBox.Show(Lista_Rutas.Count.ToString(), "Done");
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        /// <summary>
        /// Useful for testing images and save them near the program to see if they end up looking as intented.
        /// </summary>
        internal static void Guardar_Imagen_Temporal(Image Imagen)
        {
            try
            {
                if (Imagen != null)
                {
                    Imagen.Save(Application.StartupPath + "\\" + Obtener_Nombre_Temporal() + ".png", ImageFormat.Png);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        /// <summary>
        /// Useful for testing images and save them near the program with the specified name to see if they end up looking as intented.
        /// </summary>
        internal static void Guardar_Imagen_Temporal(Image Imagen, string Nombre_Sin_Extensión)
        {
            Guardar_Imagen_Temporal(Imagen, Nombre_Sin_Extensión, false);
        }

        /// <summary>
        /// Useful for testing images and save them near the program with the specified name to see if they end up looking as intented.
        /// </summary>
        internal static void Guardar_Imagen_Temporal(Image Imagen, string Nombre_Sin_Extensión, bool Sobrescribir)
        {
            try
            {
                if (Imagen != null)
                {
                    if (!string.IsNullOrEmpty(Nombre_Sin_Extensión))
                    {
                        if (!File.Exists(Application.StartupPath + "\\" + Nombre_Sin_Extensión + ".png") || Sobrescribir)
                        {
                            Imagen.Save(Application.StartupPath + "\\" + Nombre_Sin_Extensión + ".png", ImageFormat.Png);
                        }
                    }
                    else Guardar_Imagen_Temporal(Imagen);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        /// <summary>
        /// Function used to recolor any image or texture using the specified color to replace all of it's pixels.
        /// </summary>
        /// <param name="Imagen">Any valid image in 32 bits ARGB or 24 bits RGB format.</param>
        /// <param name="Color_ARGB">Any valid ARGB color.</param>
        /// <returns>Returns the recolored image. Returns null on any error.</returns>
        internal static Bitmap Recolorear_Imagen(Bitmap Imagen, Color Color_ARGB)
        {
            try
            {
                if (Imagen != null && Color_ARGB != Color.Empty)
                {
                    int Ancho = Imagen.Width;
                    int Alto = Imagen.Height;
                    BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                    byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                    int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                    int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                    byte Rojo = Color_ARGB.R;
                    byte Verde = Color_ARGB.G;
                    byte Azul = Color_ARGB.B;
                    for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                    {
                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                        {
                            Matriz_Bytes[Índice + 2] = Rojo;
                            Matriz_Bytes[Índice + 1] = Verde;
                            Matriz_Bytes[Índice] = Azul;
                        }
                    }
                    Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                    Imagen.UnlockBits(Bitmap_Data);
                    Bitmap_Data = null;
                    Matriz_Bytes = null;
                    return Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Function used to recolor any image or texture using the specified color to replace it's hue and saturation in the original image, and mix it's lightness with the original in the image. Although this function works for now it's just a patch to recolor certain Minecraft textures like grass block or most plants, among others, so they aren't in grayscale anymore.
        /// </summary>
        /// <param name="Imagen">Any valid image in 32 bits ARGB or 24 bits RGB format.</param>
        /// <param name="Color_ARGB">Any valid ARGB color.</param>
        /// <returns>Returns the recolored image. Returns null on any error.</returns>
        internal static Bitmap Recolorear_Imagen_HSL(Bitmap Imagen, Color Color_ARGB)
        {
            try
            {
                if (Imagen != null && Color_ARGB != Color.Empty)
                {
                    int Ancho = Imagen.Width;
                    int Alto = Imagen.Height;
                    BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                    byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                    int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                    int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                    double Matiz, Saturación, Luminosidad;
                    double Matiz_Original, Saturación_Original, Luminosidad_Original;
                    byte Rojo, Verde, Azul;
                    for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                    {
                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                        {
                            Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz, out Saturación, out Luminosidad);
                            Program.HSL.From_RGB(Matriz_Bytes[Índice + 2], Matriz_Bytes[Índice + 1], Matriz_Bytes[Índice], out Matiz_Original, out Saturación_Original, out Luminosidad_Original);
                            Program.HSL.To_RGB(Matiz, Saturación, (Luminosidad + Luminosidad_Original) / 2, out Rojo, out Verde, out Azul);
                            Matriz_Bytes[Índice + 2] = Rojo;
                            Matriz_Bytes[Índice + 1] = Verde;
                            Matriz_Bytes[Índice] = Azul;
                        }
                    }
                    Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                    Imagen.UnlockBits(Bitmap_Data);
                    Bitmap_Data = null;
                    Matriz_Bytes = null;
                    return Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// 2018_03_05_09_02_42_131
        /// </summary>
        internal static DateTime Traducir_Fecha(string Texto_Fecha)
        {
            try
            {
                if (!string.IsNullOrEmpty(Texto_Fecha))
                {
                    string Texto_Números = null;
                    foreach (char Caracter in Texto_Fecha)
                    {
                        if (char.IsDigit(Caracter)) Texto_Números += Caracter;
                    }
                    if (!string.IsNullOrEmpty(Texto_Números) && Texto_Números.Length >= 17)
                    {
                        int Año = int.Parse(Texto_Números.Substring(0, 4));
                        int Mes = int.Parse(Texto_Números.Substring(4, 2));
                        int Día = int.Parse(Texto_Números.Substring(6, 2));
                        int Hora = int.Parse(Texto_Números.Substring(8, 2));
                        int Minuto = int.Parse(Texto_Números.Substring(10, 2));
                        int Segundo = int.Parse(Texto_Números.Substring(12, 2));
                        int Milisegundo = int.Parse(Texto_Números.Substring(14, 3));
                        return new DateTime(Año, Mes, Día, Hora, Minuto, Segundo, Milisegundo);
                    }
                }
            }
            catch { }
            return DateTime.MinValue;
        }

        /// <summary>
        /// Translates a DateTime value into numeric notation with day, month and year.
        /// </summary>
        internal static string Traducir_Fecha(DateTime Fecha)
        {
            try
            {
                if (Fecha != null && Fecha >= DateTime.MinValue && Fecha <= DateTime.MaxValue)
                {
                    string Año = Fecha.Year.ToString();
                    string Mes = Fecha.Month.ToString();
                    string Día = Fecha.Day.ToString();
                    while (Año.Length < 4) Año = "0" + Año;
                    while (Mes.Length < 2) Mes = "0" + Mes;
                    while (Día.Length < 2) Día = "0" + Día;
                    return Día + "-" + Mes + "-" + Año;
                }
            }
            catch (Exception Excepción) { Application.OnThreadException(Excepción); }
            return "??-??-????, ??:??:??.???";
        }

        internal static string Traducir_Día_Semana(DateTime Fecha, bool Habilitar_CurrentInfo)
        {
            if (Habilitar_CurrentInfo == false)
            {
                if (Fecha.DayOfWeek == DayOfWeek.Monday) return "Monday";
                else if (Fecha.DayOfWeek == DayOfWeek.Tuesday) return "Tuesday";
                else if (Fecha.DayOfWeek == DayOfWeek.Wednesday) return "Wednesday";
                else if (Fecha.DayOfWeek == DayOfWeek.Thursday) return "Thursday";
                else if (Fecha.DayOfWeek == DayOfWeek.Friday) return "Friday";
                else if (Fecha.DayOfWeek == DayOfWeek.Saturday) return "Saturday";
                else if (Fecha.DayOfWeek == DayOfWeek.Sunday) return "Sunday";
            }
            else
            {
                string Texto = Fecha.ToString("dddd", DateTimeFormatInfo.CurrentInfo);
                if (string.IsNullOrEmpty(Texto) == false) return Texto.Substring(0, 1).ToUpperInvariant() + (Texto.Length > 1 ? Texto.Substring(1, Texto.Length - 1) : null);
            }
            return "?";
        }

        internal static string Traducir_Fecha(DateTime Fecha, bool Habilitar_Día_Semana, bool Habilitar_Segundos, bool Habilitar_Milisegundos)
        {
            try
            {
                if (Fecha != null && Fecha >= DateTime.MinValue && Fecha <= DateTime.MaxValue)
                {
                    string Año = Fecha.Year.ToString();
                    string Mes = Fecha.Month.ToString();
                    string Día = Fecha.Day.ToString();
                    string Hora = Fecha.Hour.ToString();
                    string Minuto = Fecha.Minute.ToString();
                    string Segundo = Fecha.Second.ToString();
                    string Milisegundo = Fecha.Millisecond.ToString();
                    string Día_Semana = Traducir_Día_Semana(Fecha, false);
                    while (Año.Length < 4) Año = "0" + Año;
                    while (Mes.Length < 2) Mes = "0" + Mes;
                    while (Día.Length < 2) Día = "0" + Día;
                    while (Hora.Length < 2) Hora = "0" + Hora;
                    while (Minuto.Length < 2) Minuto = "0" + Minuto;
                    while (Segundo.Length < 2) Segundo = "0" + Segundo;
                    while (Milisegundo.Length < 3) Milisegundo = "0" + Milisegundo;
                    return (Habilitar_Día_Semana == false ? null : Día_Semana + ", ") + Día + "-" + Mes + "-" + Año + ", " + Hora + ":" + Minuto + (Habilitar_Segundos == false ? null : ":" + Segundo + (Habilitar_Milisegundos == false ? null : "." + Milisegundo));
                }
            }
            catch (Exception Excepción)
            {
                Application.OnThreadException(Excepción);
            }
            return "??-??-????, ??:??" + (Habilitar_Segundos == false ? null : ":??" + (Habilitar_Milisegundos == false ? null : ".???"));
        }

        /// <summary>
        /// Translates a DateTime value into English localization.
        /// </summary>
        internal static string Traducir_Fecha_Inglés(DateTime Fecha)
        {
            try
            {
                if (Fecha != null && Fecha >= DateTime.MinValue && Fecha <= DateTime.MaxValue)
                {

                    string Día = Fecha.Day.ToString();
                    if (Día.EndsWith("1") && Fecha.Day != 11) Día += "st";
                    else if (Día.EndsWith("2") && Fecha.Day != 12) Día += "nd";
                    else if (Día.EndsWith("3") && Fecha.Day != 13) Día += "rd";
                    else Día += "th";

                    string Mes = null;
                    if (Fecha.Month == 1) Mes = "January";
                    else if (Fecha.Month == 2) Mes = "February";
                    else if (Fecha.Month == 3) Mes = "March";
                    else if (Fecha.Month == 4) Mes = "April";
                    else if (Fecha.Month == 5) Mes = "May";
                    else if (Fecha.Month == 6) Mes = "June";
                    else if (Fecha.Month == 7) Mes = "July";
                    else if (Fecha.Month == 8) Mes = "August";
                    else if (Fecha.Month == 9) Mes = "September";
                    else if (Fecha.Month == 10) Mes = "October";
                    else if (Fecha.Month == 11) Mes = "November";
                    else if (Fecha.Month == 12) Mes = "December";

                    string Año = Fecha.Year.ToString();
                    while (Año.Length < 4) Año = "0" + Año;

                    return Mes + ", " + Día + " of " + Año;
                }
            }
            catch (Exception Excepción) { Application.OnThreadException(Excepción); }
            return "?, ?th of ????";
        }

        internal static string Traducir_Fecha_Hora(DateTime Fecha)
        {
            try
            {
                if (Fecha != null && Fecha >= DateTime.MinValue && Fecha <= DateTime.MaxValue)
                {
                    string Año = Fecha.Year.ToString();
                    string Mes = Fecha.Month.ToString();
                    string Día = Fecha.Day.ToString();
                    string Hora = Fecha.Hour.ToString();
                    string Minuto = Fecha.Minute.ToString();
                    string Segundo = Fecha.Second.ToString();
                    string Milisegundo = Fecha.Millisecond.ToString();
                    while (Año.Length < 4) Año = "0" + Año;
                    while (Mes.Length < 2) Mes = "0" + Mes;
                    while (Día.Length < 2) Día = "0" + Día;
                    while (Hora.Length < 2) Hora = "0" + Hora;
                    while (Minuto.Length < 2) Minuto = "0" + Minuto;
                    while (Segundo.Length < 2) Segundo = "0" + Segundo;
                    while (Milisegundo.Length < 3) Milisegundo = "0" + Milisegundo;
                    return Día + "-" + Mes + "-" + Año + ", " + Hora + ":" + Minuto + ":" + Segundo + "." + Milisegundo;
                }
            }
            catch (Exception Excepción) { Application.OnThreadException(Excepción); }
            return "??-??-????, ??:??:??.???";
        }

        internal static string Traducir_Intervalo(TimeSpan Intervalo)
        {
            try
            {
                string Días = Math.Abs(Intervalo.Days).ToString();
                string Horas = Math.Abs(Intervalo.Hours).ToString();
                string Minutos = Math.Abs(Intervalo.Minutes).ToString();
                string Segundos = Math.Abs(Intervalo.Seconds).ToString();
                string Milisegundos = Math.Abs(Intervalo.Milliseconds).ToString();
                while (Horas.Length < 2) Horas = "0" + Horas;
                while (Minutos.Length < 2) Minutos = "0" + Minutos;
                while (Segundos.Length < 2) Segundos = "0" + Segundos;
                while (Milisegundos.Length < 3) Milisegundos = "0" + Milisegundos;
                return (Intervalo.TotalDays >= 0 ? null : "-") + Días + ":" + Horas + ":" + Minutos + ":" + Segundos + "." + Milisegundos;
            }
            catch { }
            return "0:00:00:00.000";
        }

        internal static string Traducir_Intervalo(TimeSpan Intervalo, bool Habilitar_Signo, bool Habilitar_Días, bool Habilitar_Milisegundos)
        {
            try
            {
                if (Intervalo != null)
                {
                    string Días = Intervalo.Days.ToString().Replace("-", "");
                    string Horas = (Habilitar_Días == false ? (Intervalo.Days * 24) + Intervalo.Hours : Intervalo.Hours).ToString().Replace("-", "");
                    string Minutos = Intervalo.Minutes.ToString().Replace("-", "");
                    string Segundos = Intervalo.Seconds.ToString().Replace("-", "");
                    string Milisegundos = Intervalo.Milliseconds.ToString().Replace("-", "");
                    //while (Días.Length < 2) Días = "0" + Días;
                    while (Horas.Length < 2) Horas = "0" + Horas;
                    while (Minutos.Length < 2) Minutos = "0" + Minutos;
                    while (Segundos.Length < 2) Segundos = "0" + Segundos;
                    while (Milisegundos.Length < 3) Milisegundos = "0" + Milisegundos;
                    return (Habilitar_Signo == false ? null : (Intervalo.TotalDays > 0 ? "+" : "-")) + (Habilitar_Días == false ? null : Días + ":") + Horas + ":" + Minutos + ":" + Segundos + (Habilitar_Milisegundos == false ? null : "." + Milisegundos);
                }
            }
            catch (Exception Excepción)
            {
                Application.OnThreadException(Excepción);
            }
            return (Habilitar_Signo == false ? null : "+") + (Habilitar_Días == false ? null : "??:") + "??:??:??" + (Habilitar_Milisegundos == false ? null : ".???");
        }

        internal static string Traducir_Intervalo_Horas_Minutos_Segundos(TimeSpan Intervalo)
        {
            try
            {
                string Horas = Intervalo.Hours.ToString();
                string Minutos = Intervalo.Minutes.ToString();
                string Segundos = Intervalo.Seconds.ToString();
                string Milisegundos = Intervalo.Milliseconds.ToString();
                while (Horas.Length < 2) Horas = "0" + Horas;
                while (Minutos.Length < 2) Minutos = "0" + Minutos;
                while (Segundos.Length < 2) Segundos = "0" + Segundos;
                while (Milisegundos.Length < 3) Milisegundos = "0" + Milisegundos;
                return Horas + ":" + Minutos + ":" + Segundos + "." + Milisegundos;
            }
            catch (Exception Excepción) { Application.OnThreadException(Excepción); }
            return "00:00:00.000";
        }

        internal static string Traducir_Intervalo_Minutos_Segundos(TimeSpan Intervalo)
        {
            try
            {
                string Minutos = Intervalo.Minutes.ToString();
                string Segundos = Intervalo.Seconds.ToString();
                string Milisegundos = Intervalo.Milliseconds.ToString();
                while (Minutos.Length < 2) Minutos = "0" + Minutos;
                while (Segundos.Length < 2) Segundos = "0" + Segundos;
                while (Milisegundos.Length < 3) Milisegundos = "0" + Milisegundos;
                return Minutos + ":" + Segundos + "." + Milisegundos;
            }
            catch (Exception Excepción) { Application.OnThreadException(Excepción); }
            return "00:00.000";
        }

        internal static string Traducir_Número(sbyte Valor)
        {
            return Valor.ToString();
        }

        internal static string Traducir_Número(byte Valor)
        {
            return Valor.ToString();
        }

        internal static string Traducir_Número(short Valor)
        {
            return Valor > -1000 && Valor < 1000 ? Valor.ToString() : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(ushort Valor)
        {
            return Valor < 1000 ? Valor.ToString() : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(int Valor)
        {
            return Valor > -1000 && Valor < 1000 ? Valor.ToString() : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(uint Valor)
        {
            return Valor < 1000 ? Valor.ToString() : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(long Valor)
        {
            return Valor > -1000L && Valor < 1000L ? Valor.ToString() : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(ulong Valor)
        {
            return Valor < 1000UL ? Valor.ToString() : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(float Valor)
        {
            //if (Single.IsNegativeInfinity(Valor)) return "-?";
            //else if (Single.IsPositiveInfinity(Valor)) return "+?";
            //else if (Single.IsNaN(Valor)) return "?";
            if (float.IsInfinity(Valor) || float.IsNaN(Valor)) return "0";
            else return Valor > -1000f && Valor < 1000f ? Valor.ToString().Replace(Caracter_Coma_Decimal, ',') : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(double Valor)
        {
            //if (Double.IsNegativeInfinity(Valor)) return "-?";
            //else if (Double.IsPositiveInfinity(Valor)) return "+?";
            //else if (Double.IsNaN(Valor)) return "?";
            if (double.IsInfinity(Valor) || double.IsNaN(Valor)) return "0";
            else return Valor > -1000d && Valor < 1000d ? Valor.ToString().Replace(Caracter_Coma_Decimal, ',') : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(decimal Valor)
        {
            return Valor > -1000m && Valor < 1000m ? Valor.ToString().Replace(Caracter_Coma_Decimal, ',') : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(string Texto)
        {
            Texto = Texto.Replace(Caracter_Coma_Decimal, ',').Replace(".", null);
            for (int Índice = !Texto.Contains(",") ? Texto.Length - 3 : Texto.IndexOf(',') - 3, Índice_Final = !Texto.StartsWith("-") ? 0 : 1; Índice > Índice_Final; Índice -= 3) Texto = Texto.Insert(Índice, ".");
            return Texto;
            /*Texto = Texto.Replace(Caracter_Coma_Decimal, ',');
            if (Texto.Contains(".")) Texto = Texto.Replace(".", null);
            int Índice = Texto.IndexOf(',');
            for (Índice = Índice < 0 ? Texto.Length - 3 : Índice - 3; Índice > (Texto[0] != '-' ? 0 : 1); Índice -= 3) Texto = Texto.Insert(Índice, ".");
            return Texto;*/
        }

        internal static string Traducir_Número_Decimales_Redondear(double Valor, int Decimales)
        {
            Valor = Math.Round(Valor, Decimales, MidpointRounding.AwayFromZero);
            string Texto = double.IsInfinity(Valor) || double.IsNaN(Valor) ? "0" : Valor > -1000d && Valor < 1000d ? Valor.ToString().Replace(Caracter_Coma_Decimal, ',') : Traducir_Número(Valor.ToString());
            if (Texto.Contains(",") == false) Texto += ',' + new string('0', Decimales);
            else
            {
                Decimales = Decimales - (Texto.Length - (Texto.IndexOf(',') + 1));
                if (Decimales > 0) Texto += new string('0', Decimales);
            }
            return Texto;
        }

        internal static string Traducir_Número_Decimales_Redondear(decimal Valor, int Decimales)
        {
            Valor = Math.Round(Valor, Decimales, MidpointRounding.AwayFromZero);
            string Texto = Valor > -1000m && Valor < 1000m ? Valor.ToString().Replace(Caracter_Coma_Decimal, ',') : Traducir_Número(Valor.ToString());
            if (Texto.Contains(",") == false) Texto += ',' + new string('0', Decimales);
            else
            {
                Decimales -= Texto.Length - (Texto.IndexOf(',') + 1);
                if (Decimales > 0) Texto += new string('0', Decimales);
            }
            return Texto;
        }

        internal static string Traducir_Número_Decimales(double Valor, int Decimales)
        {
            string Texto = double.IsInfinity(Valor) || double.IsNaN(Valor) ? "0" : Valor > -1000d && Valor < 1000d ? Valor.ToString().Replace(Caracter_Coma_Decimal, ',') : Traducir_Número(Valor.ToString());
            if (Texto.Contains(",") == false) Texto += ',' + new string('0', Decimales);
            else
            {
                Decimales = Decimales - (Texto.Length - (Texto.IndexOf(',') + 1));
                if (Decimales > 0) Texto += new string('0', Decimales);
            }
            return Texto;
        }

        internal static readonly byte[] Matriz_Potencias_Base_2 = new byte[8] { 128, 64, 32, 16, 8, 4, 2, 1 };

        /// <summary>
        /// Requiere pretraducir las comas que lo deban ser y sean puntos a comas, o se borrarán...
        /// </summary>
        internal static string Traducir_Número_Puntuación_Miles(string Texto)
        {
            Texto = Texto.Replace(".", null);
            int Índice = Texto.IndexOf(',');
            for (Índice = Índice < 0 ? Texto.Length - 3 : Índice - 3; Índice > (Texto[0] != '-' ? 0 : 1); Índice -= 3) Texto = Texto.Insert(Índice, ".");
            return Texto;
        }

        internal static readonly double[] Matriz_Divisores_Tamaños = new double[8] { 8, 1024, 1024, 1024, 1024, 1024, 1024, 1024 };

        internal static string Traducir_Tamaño_Bits(double Tamaño_Bits, int Decimales, bool Decimales_Cero)
        {
            try
            {
                int Índice_Divisor = 0;
                for (; Índice_Divisor < Matriz_Divisores_Tamaños.Length; Índice_Divisor++)
                {
                    if (Tamaño_Bits >= Matriz_Divisores_Tamaños[Índice_Divisor]) Tamaño_Bits /= Matriz_Divisores_Tamaños[Índice_Divisor];
                    else break;
                }
                string Texto = Traducir_Número_Puntuación_Miles(Math.Round(Tamaño_Bits, Decimales, MidpointRounding.AwayFromZero).ToString());
                if (Decimales_Cero)
                {
                    if (!Texto.Contains(Caracter_Coma_Decimal.ToString())) Texto += Caracter_Coma_Decimal + new string('0', Decimales);
                    else
                    {
                        Decimales = Decimales - (Texto.Length - (Texto.IndexOf(Caracter_Coma_Decimal) + 1));
                        if (Decimales > 0) Texto += new string('0', Decimales);
                    }
                }
                if (Índice_Divisor == 0) return Texto + (Tamaño_Bits == 1d ? " Bit" : " Bits");
                else if (Índice_Divisor == 1) return Texto + (Tamaño_Bits == 1d ? " Byte" : " Bytes");
                else if (Índice_Divisor == 2) return Texto + " KB";
                else if (Índice_Divisor == 3) return Texto + " MB";
                else if (Índice_Divisor == 4) return Texto + " GB";
                else if (Índice_Divisor == 5) return Texto + " TB";
                else if (Índice_Divisor == 6) return Texto + " PB";
                else return Texto + " EB";
            }
            catch { }
            return "? Bits";
        }

        internal static string Traducir_Tamaño_Bits_Segundo(long Tamaño_Bits, double Segundos, int Decimales, bool Decimales_Cero)
        {
            try
            {
                return Traducir_Tamaño_Bits(Tamaño_Bits / Segundos, Decimales, Decimales_Cero) + "/s";
            }
            catch { }
            return "? Bits/s";
        }

        internal static string Traducir_Tamaño_Bytes(long Tamaño_Bytes, int Decimales, bool Decimales_Cero)
        {
            try
            {
                decimal Valor = (decimal)Tamaño_Bytes;
                int Índice = 0;
                for (; Índice < 7; Índice++)
                {
                    if (Valor < 1024m) break;
                    else Valor = Valor / 1024m;
                }
                string Texto = Traducir_Número(Math.Round(Valor, Decimales, MidpointRounding.AwayFromZero));
                if (Decimales_Cero)
                {
                    if (!Texto.Contains(Caracter_Coma_Decimal.ToString())) Texto += ',' + new string('0', Decimales);
                    else
                    {
                        Decimales = Decimales - (Texto.Length - (Texto.IndexOf(Caracter_Coma_Decimal) + 1));
                        if (Decimales > 0) Texto += new string('0', Decimales);
                    }
                }
                if (Índice == 0) Texto += Tamaño_Bytes == 1L ? " Byte" : " Bytes";
                else if (Índice == 1) Texto += " KB";
                else if (Índice == 2) Texto += " MB";
                else if (Índice == 3) Texto += " GB";
                else if (Índice == 4) Texto += " TB";
                else if (Índice == 5) Texto += " PB";
                else if (Índice == 6) Texto += " EB";
                return Texto;
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }
            return "? Bytes";
        }

        internal static string Traducir_Tamaño_Bytes_Automático(long Tamaño_Bytes, int Decimales, bool Decimales_Cero)
        {
            try
            {
                decimal Valor = (decimal)Tamaño_Bytes;
                int Índice = 0;
                for (; Índice < 7; Índice++)
                {
                    if (Valor < 1024m) break;
                    else Valor = Valor / 1024m;
                }
                string Texto = Traducir_Número(Math.Round(Valor, Decimales, MidpointRounding.AwayFromZero));
                if (Decimales_Cero)
                {
                    if (!Texto.Contains(Caracter_Coma_Decimal.ToString())) Texto += ',' + new string('0', Decimales);
                    else
                    {
                        Decimales = Decimales - (Texto.Length - (Texto.IndexOf(Caracter_Coma_Decimal) + 1));
                        if (Decimales > 0) Texto += new string('0', Decimales);
                    }
                }
                if (Índice == 0) Texto += Tamaño_Bytes == 1L ? " Byte" : " Bytes";
                else if (Índice == 1) Texto += " KB";
                else if (Índice == 2) Texto += " MB";
                else if (Índice == 3) Texto += " GB";
                else if (Índice == 4) Texto += " TB";
                else if (Índice == 5) Texto += " PB";
                else if (Índice == 6) Texto += " EB";
                return Texto;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return "? Bytes";
        }

        internal static readonly uint[] Matriz_CRC_32 = new uint[256]
        {
            0x00000000, 0x77073096, 0xEE0E612C, 0x990951BA, 0x076DC419,
            0x706AF48F, 0xE963A535, 0x9E6495A3, 0x0EDB8832, 0x79DCB8A4,
            0xE0D5E91E, 0x97D2D988, 0x09B64C2B, 0x7EB17CBD, 0xE7B82D07,
            0x90BF1D91, 0x1DB71064, 0x6AB020F2, 0xF3B97148, 0x84BE41DE,
            0x1ADAD47D, 0x6DDDE4EB, 0xF4D4B551, 0x83D385C7, 0x136C9856,
            0x646BA8C0, 0xFD62F97A, 0x8A65C9EC, 0x14015C4F, 0x63066CD9,
            0xFA0F3D63, 0x8D080DF5, 0x3B6E20C8, 0x4C69105E, 0xD56041E4,
            0xA2677172, 0x3C03E4D1, 0x4B04D447, 0xD20D85FD, 0xA50AB56B,
            0x35B5A8FA, 0x42B2986C, 0xDBBBC9D6, 0xACBCF940, 0x32D86CE3,
            0x45DF5C75, 0xDCD60DCF, 0xABD13D59, 0x26D930AC, 0x51DE003A,
            0xC8D75180, 0xBFD06116, 0x21B4F4B5, 0x56B3C423, 0xCFBA9599,
            0xB8BDA50F, 0x2802B89E, 0x5F058808, 0xC60CD9B2, 0xB10BE924,
            0x2F6F7C87, 0x58684C11, 0xC1611DAB, 0xB6662D3D, 0x76DC4190,
            0x01DB7106, 0x98D220BC, 0xEFD5102A, 0x71B18589, 0x06B6B51F,
            0x9FBFE4A5, 0xE8B8D433, 0x7807C9A2, 0x0F00F934, 0x9609A88E,
            0xE10E9818, 0x7F6A0DBB, 0x086D3D2D, 0x91646C97, 0xE6635C01,
            0x6B6B51F4, 0x1C6C6162, 0x856530D8, 0xF262004E, 0x6C0695ED,
            0x1B01A57B, 0x8208F4C1, 0xF50FC457, 0x65B0D9C6, 0x12B7E950,
            0x8BBEB8EA, 0xFCB9887C, 0x62DD1DDF, 0x15DA2D49, 0x8CD37CF3,
            0xFBD44C65, 0x4DB26158, 0x3AB551CE, 0xA3BC0074, 0xD4BB30E2,
            0x4ADFA541, 0x3DD895D7, 0xA4D1C46D, 0xD3D6F4FB, 0x4369E96A,
            0x346ED9FC, 0xAD678846, 0xDA60B8D0, 0x44042D73, 0x33031DE5,
            0xAA0A4C5F, 0xDD0D7CC9, 0x5005713C, 0x270241AA, 0xBE0B1010,
            0xC90C2086, 0x5768B525, 0x206F85B3, 0xB966D409, 0xCE61E49F,
            0x5EDEF90E, 0x29D9C998, 0xB0D09822, 0xC7D7A8B4, 0x59B33D17,
            0x2EB40D81, 0xB7BD5C3B, 0xC0BA6CAD, 0xEDB88320, 0x9ABFB3B6,
            0x03B6E20C, 0x74B1D29A, 0xEAD54739, 0x9DD277AF, 0x04DB2615,
            0x73DC1683, 0xE3630B12, 0x94643B84, 0x0D6D6A3E, 0x7A6A5AA8,
            0xE40ECF0B, 0x9309FF9D, 0x0A00AE27, 0x7D079EB1, 0xF00F9344,
            0x8708A3D2, 0x1E01F268, 0x6906C2FE, 0xF762575D, 0x806567CB,
            0x196C3671, 0x6E6B06E7, 0xFED41B76, 0x89D32BE0, 0x10DA7A5A,
            0x67DD4ACC, 0xF9B9DF6F, 0x8EBEEFF9, 0x17B7BE43, 0x60B08ED5,
            0xD6D6A3E8, 0xA1D1937E, 0x38D8C2C4, 0x4FDFF252, 0xD1BB67F1,
            0xA6BC5767, 0x3FB506DD, 0x48B2364B, 0xD80D2BDA, 0xAF0A1B4C,
            0x36034AF6, 0x41047A60, 0xDF60EFC3, 0xA867DF55, 0x316E8EEF,
            0x4669BE79, 0xCB61B38C, 0xBC66831A, 0x256FD2A0, 0x5268E236,
            0xCC0C7795, 0xBB0B4703, 0x220216B9, 0x5505262F, 0xC5BA3BBE,
            0xB2BD0B28, 0x2BB45A92, 0x5CB36A04, 0xC2D7FFA7, 0xB5D0CF31,
            0x2CD99E8B, 0x5BDEAE1D, 0x9B64C2B0, 0xEC63F226, 0x756AA39C,
            0x026D930A, 0x9C0906A9, 0xEB0E363F, 0x72076785, 0x05005713,
            0x95BF4A82, 0xE2B87A14, 0x7BB12BAE, 0x0CB61B38, 0x92D28E9B,
            0xE5D5BE0D, 0x7CDCEFB7, 0x0BDBDF21, 0x86D3D2D4, 0xF1D4E242,
            0x68DDB3F8, 0x1FDA836E, 0x81BE16CD, 0xF6B9265B, 0x6FB077E1,
            0x18B74777, 0x88085AE6, 0xFF0F6A70, 0x66063BCA, 0x11010B5C,
            0x8F659EFF, 0xF862AE69, 0x616BFFD3, 0x166CCF45, 0xA00AE278,
            0xD70DD2EE, 0x4E048354, 0x3903B3C2, 0xA7672661, 0xD06016F7,
            0x4969474D, 0x3E6E77DB, 0xAED16A4A, 0xD9D65ADC, 0x40DF0B66,
            0x37D83BF0, 0xA9BCAE53, 0xDEBB9EC5, 0x47B2CF7F, 0x30B5FFE9,
            0xBDBDF21C, 0xCABAC28A, 0x53B39330, 0x24B4A3A6, 0xBAD03605,
            0xCDD70693, 0x54DE5729, 0x23D967BF, 0xB3667A2E, 0xC4614AB8,
            0x5D681B02, 0x2A6F2B94, 0xB40BBE37, 0xC30C8EA1, 0x5A05DF1B,
            0x2D02EF8D
        };

        /// <summary>
        /// Calcula el CRC de 32 bits de la matriz de bytes indicada.
        /// </summary>
        internal static uint Calcular_CRC32(byte[] Matriz_Bytes)
        {
            if (Matriz_Bytes == null) return 0;
            uint CRC_32_Bits = 0xFFFFFFFF;
            for (int Índice = 0; Índice < Matriz_Bytes.Length; Índice++) CRC_32_Bits = Matriz_CRC_32[(Byte)(CRC_32_Bits ^ Matriz_Bytes[Índice])] ^ (CRC_32_Bits >> 8);
            return ~CRC_32_Bits;
        }

        /// <summary>
        /// Calcula el CRC de 32 bits de la matriz de bytes indicada.
        /// </summary>
        internal static uint Calcular_CRC32(byte[] Matriz_Bytes, int Longitud)
        {
            if (Matriz_Bytes == null || Matriz_Bytes.Length <= 0) return 0;
            else if (Longitud <= 0) Longitud = Matriz_Bytes.Length;
            uint Valor_CRC32 = 0xFFFFFFFF;
            for (int Índice = 0; Índice < Longitud; Índice++) Valor_CRC32 = Matriz_CRC_32[(Byte)(Valor_CRC32 ^ Matriz_Bytes[Índice])] ^ (Valor_CRC32 >> 8);
            return ~Valor_CRC32;
        }

        /// <summary>
        /// Calcula el CRC de 32 bits de la matriz de bytes indicada, continuando desde un valor anterior que debe iniciarse por primera vez en cero.
        /// </summary>
        internal static uint Calcular_CRC32(byte[] Matriz_Bytes, int Longitud, uint Valor_CRC32)
        {
            if (Matriz_Bytes == null || Matriz_Bytes.Length <= 0) return 0;
            else if (Longitud <= 0) Longitud = Matriz_Bytes.Length;
            Valor_CRC32 = ~Valor_CRC32;
            for (int Índice = 0; Índice < Longitud; Índice++) Valor_CRC32 = Matriz_CRC_32[(Byte)(Valor_CRC32 ^ Matriz_Bytes[Índice])] ^ (Valor_CRC32 >> 8);
            return ~Valor_CRC32;
        }

        /// <summary>
        /// Calcula el CRC de 32 bits del archivo indicado, excluyendo el propio CRC 32 ya almacenado.
        /// </summary>
        internal static uint Calcular_CRC32_Sin_CRC_32(string Ruta)
        {
            uint Valor_CRC32 = 0xFFFFFFFF;
            FileStream Lector = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            if (Lector.Length > 4L)
            {
                byte[] Matriz_Bytes = new byte[4096];
                for (long Índice = 0L; Índice < Lector.Length; Índice += 4096L)
                {
                    int Longitud = Lector.Read(Matriz_Bytes, 0, 4096);
                    for (int Subíndice = 0; Subíndice < Longitud; Subíndice++) if (Índice + Subíndice < Lector.Length - 4) Valor_CRC32 = Matriz_CRC_32[(Byte)(Valor_CRC32 ^ Matriz_Bytes[Subíndice])] ^ (Valor_CRC32 >> 8);
                }
                Matriz_Bytes = null;
            }
            Lector.Close();
            Lector.Dispose();
            return ~Valor_CRC32;
        }

        /// <summary>
        /// Calcula el CRC de 32 bits del archivo indicado.
        /// </summary>
        internal static uint Calcular_CRC32(string Ruta)
        {
            uint Valor_CRC32 = 0xFFFFFFFF;
            FileStream Lector = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            byte[] Matriz_Bytes = new byte[4096];
            for (long Índice = 0L; Índice < Lector.Length; Índice += 4096L)
            {
                int Longitud = Lector.Read(Matriz_Bytes, 0, 4096);
                for (int Subíndice = 0; Subíndice < Longitud; Subíndice++) Valor_CRC32 = Matriz_CRC_32[(Byte)(Valor_CRC32 ^ Matriz_Bytes[Subíndice])] ^ (Valor_CRC32 >> 8);
            }
            Matriz_Bytes = null;
            Lector.Close();
            Lector.Dispose();
            return ~Valor_CRC32;
        }

        internal static List<Color> Aleatorizar_Lista(List<Color> Lista)
        {
            List<Color> Lista_Temporal = Lista.GetRange(0, Lista.Count);
            Lista.Clear();
            for (int Índice = Lista_Temporal.Count - 1; Índice >= 0; Índice--)
            {
                int Índice_Aleatorio = Program.Rand.Next(0, Lista_Temporal.Count);
                Lista.Add(Lista_Temporal[Índice_Aleatorio]);
                Lista_Temporal.RemoveAt(Índice_Aleatorio);
            }
            Lista_Temporal = null;
            return Lista;
        }

        /// <summary>
        /// Deletes the background dark color from the Villager Trading Chart from the Minecraft wiki, allowing for further editing later. It may have other custom uses.
        /// </summary>
        /// <param name="Color_ARGB">Any valid ARGB color that will be deleted from the image.</param>
        internal static void Borrar_Color_Fondo_Imagen(string Ruta, Color Color_ARGB)
        {
            try
            {
                Bitmap Imagen = Cargar_Imagen_Ruta(Ruta, CheckState.Checked); // Force loading with alpha.
                if (Imagen != null)
                {
                    int Ancho = Imagen.Width;
                    int Alto = Imagen.Height;
                    BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                    byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                    for (int Y = 0, Índice = 0; Y < Alto; Y++)
                    {
                        for (int X = 0; X < Ancho; X++, Índice += 4)
                        {
                            if (Matriz_Bytes[Índice + 3] == Color_ARGB.A && Matriz_Bytes[Índice + 2] == Color_ARGB.R && Matriz_Bytes[Índice + 1] == Color_ARGB.G && Matriz_Bytes[Índice] == Color_ARGB.B)
                            {
                                Matriz_Bytes[Índice + 3] = 0;
                                Matriz_Bytes[Índice + 2] = 0;
                                Matriz_Bytes[Índice + 1] = 0;
                                Matriz_Bytes[Índice] = 0;
                            }
                        }
                    }
                    Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                    Imagen.UnlockBits(Bitmap_Data);
                    Bitmap_Data = null;
                    Matriz_Bytes = null;
                    Guardar_Imagen_Temporal(Imagen, Program.Obtener_Nombre_Temporal());
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        /// <summary>
        /// Loads any image from disk into memory and redraws it in one of the supported pixel formats, so it will never give any error (in theory).
        /// </summary>
        /// <param name="Lector">Any valid stream like a FileStream or MemoryStream that contains a valid image inside.</param>
        /// <param name="Alfa">If it's Indeterminate the returned image will contain alpha (transparency) only it if had it before. If it's Checked the returned image will always have alpha. Otherwise it will never have alpha.</param>
        /// <returns>The redrawed image in one of the supported pixel formats.</returns>
        internal static Bitmap Cargar_Imagen_Lector(Stream Lector, CheckState Alfa)
        {
            try
            {
                Image Imagen_Original = null;
                try { Imagen_Original = Image.FromStream(Lector, false, false); }
                catch { Imagen_Original = null; }
                if (Imagen_Original != null)
                {
                    int Ancho = Imagen_Original.Width;
                    int Alto = Imagen_Original.Height;
                    Bitmap Imagen = new Bitmap(Ancho, Alto, Alfa == CheckState.Unchecked ? PixelFormat.Format24bppRgb : Alfa == CheckState.Checked ? PixelFormat.Format32bppArgb : (!Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? PixelFormat.Format24bppRgb : PixelFormat.Format32bppArgb));
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.HighQuality;
                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                    Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                    Pintar.Dispose();
                    Pintar = null;
                    Imagen_Original.Dispose();
                    Imagen_Original = null;
                    return Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Loads any image from disk into memory and redraws it in one of the supported pixel formats, so it will never give any error (in theory).
        /// </summary>
        /// <param name="Ruta">Any valid file path that contains an image inside.</param>
        /// <param name="Alfa">If it's Indeterminate the returned image will contain alpha (transparency) only it if had it before. If it's Checked the returned image will always have alpha. Otherwise it will never have alpha.</param>
        /// <returns>The redrawed image in one of the supported pixel formats.</returns>
        internal static Bitmap Cargar_Imagen_Ruta(string Ruta, CheckState Alfa)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                {
                    Image Imagen_Original = null;
                    FileStream Lector = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    try { Imagen_Original = Image.FromStream(Lector, false, false); }
                    catch { Imagen_Original = null; }
                    if (Imagen_Original != null)
                    {
                        int Ancho = Imagen_Original.Width;
                        int Alto = Imagen_Original.Height;
                        Bitmap Imagen = new Bitmap(Ancho, Alto, Alfa == CheckState.Unchecked ? PixelFormat.Format24bppRgb : Alfa == CheckState.Checked ? PixelFormat.Format32bppArgb : (!Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? PixelFormat.Format24bppRgb : PixelFormat.Format32bppArgb));
                        Graphics Pintar = Graphics.FromImage(Imagen);
                        Pintar.CompositingMode = CompositingMode.SourceCopy;
                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar.SmoothingMode = SmoothingMode.HighQuality;
                        Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                        Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                        Pintar.Dispose();
                        Pintar = null;
                        Imagen_Original.Dispose();
                        Imagen_Original = null;
                        return Imagen;
                    }
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Creates all the directories is the specified path if they don't exist yet, without showing any exception.
        /// </summary>
        /// <param name="Ruta">Any valid directory path.</param>
        /// <returns>Returns true if the specified directories in the path now exist. Returns false on any exception, possibly indicating that the directories might not exist.</returns>
        internal static bool Crear_Carpetas(string Ruta)
        {
            try
            {
                if (!Directory.Exists(Ruta))
                {
                    Directory.CreateDirectory(Ruta);
                    return Directory.Exists(Ruta);
                }
                else return true;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return false;
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
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
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
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return false;
        }

        /// <summary>
        /// Exports any image in the selected path as an indexed PNG image with the selected quantity of colors in the palette.
        /// </summary>
        /// <param name="Imagen">Any valid image.</param>
        /// <param name="Colores">Any value between 2 and 256 (both included).</param>
        /// <param name="Ruta">Any valid directory path. The name of the image will be assigned based on the current system time and number of colors selected.</param>
        /// <returns>Returns true if it has exported the image. Returns false otherwise.</returns>
        internal static bool Exportar_Imagen_Indizada(Bitmap Imagen, int Colores, string Ruta)
        {
            try
            {
                if (Imagen != null)
                {
                    if (Colores < 2) Colores = 2;
                    else if (Colores > 256) Colores = 256;
                    MagickImage Imagen_Cuantizada = new MagickImage((Bitmap)Imagen.Clone());
                    QuantizeSettings Ajustes_Cuantización = new QuantizeSettings();
                    Ajustes_Cuantización.Colors = Colores;
                    Ajustes_Cuantización.DitherMethod = DitherMethod.No;
                    Imagen_Cuantizada.Quantize(Ajustes_Cuantización);
                    Bitmap Imagen_Indizada = Imagen_Cuantizada.ToBitmap(ImageFormat.Png);
                    Program.Crear_Carpetas(Ruta);
                    Imagen_Indizada.Save(Ruta + "\\" + Program.Obtener_Nombre_Temporal() + " Palette " + Colores.ToString() + ".png", ImageFormat.Png);
                    Imagen_Indizada.Dispose();
                    Imagen_Indizada = null;
                    Imagen_Cuantizada.Dispose();
                    Imagen_Cuantizada = null;
                    return true;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return false;
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
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return FileAttributes.Normal;
        }

        /// <summary>
        /// Downloads any file form the internet at once in the RAM memory and returns it's contents as a byte array. This code is intended to use for example to download Minecraft skins or the new Mojang's obfuscation maps. WARNING: THIS FUNCTION SHOULDN'T BE USED FOR FILES BIGGER THAN 100 MB.
        /// </summary>
        /// <param name="URL">The desired URL (internet direction) of the file to download.</param>
        /// <param name="Intentos">How many tries should be started to download the desired file if the previous tries fail.</param>
        /// <param name="Segundos_Intento">How many milliseconds should the function wait to get a response from the internet.</param>
        /// <returns>Returns a byte array with the contents of the file. Returns null if the file is empty or on any error.</returns>
        internal static byte[] Descargar_Archivo_Completo(string URL, int Intentos, int Segundos_Intento)
        {
            try
            {
                for (int Índice_Intento = 0; Índice_Intento < Intentos; Índice_Intento++)
                {
                    try
                    {
                        // WebRequest. // HttpWebRequest.
                        // WebResponse. // HttpWebResponse.
                        WebRequest Solicitud = /*(HttpWebRequest)*/WebRequest.Create(URL);
                        //MessageBox.Show(Solicitud.Timeout.ToString()); // 100.000.
                        if (Segundos_Intento > -1) Solicitud.Timeout = Segundos_Intento * 1000; // Give up to 10 seconds to download the file.
                        WebResponse Respuesta = /*(HttpWebResponse)*/Solicitud.GetResponse();
                        BinaryReader Lector_Binario = new BinaryReader(Respuesta.GetResponseStream());
                        MemoryStream Lector_Memoria = new MemoryStream();
                        byte[] Matriz_Búfer = Lector_Binario.ReadBytes(4096); // 1024. // 4096.
                        while (Matriz_Búfer.Length > 0) // Read the file in chunks of 4 KB.
                        {
                            Lector_Memoria.Write(Matriz_Búfer, 0, Matriz_Búfer.Length);
                            Matriz_Búfer = Lector_Binario.ReadBytes(4096);
                        }
                        byte[] Matriz_Bytes = Lector_Memoria.ToArray(); // Get the whole file at once.
                        Matriz_Búfer = null;
                        Lector_Memoria.Close();
                        Lector_Memoria.Dispose();
                        Lector_Memoria = null;
                        Lector_Binario.Close();
                        Lector_Binario.Dispose();
                        Lector_Binario = null;
                        Respuesta.Close();
                        Respuesta.Dispose();
                        Respuesta = null;
                        Solicitud = null;
                        return Matriz_Bytes; // Return the whole file.
                    }
                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Executes the specified file, directory or URL, with the specified window style.
        /// </summary>
        /// <param name="Ruta">Any valid file or directory path.</param>
        /// <param name="Estado">Any valid window style.</param>
        /// <returns>Returns true if the process can be executed. Returns false if it can't be executed.</returns>
        internal static bool Ejecutar_Ruta(string Ruta, ProcessWindowStyle Estado)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta))
                {
                    Process Proceso = new Process();
                    Proceso.StartInfo.Arguments = null;
                    Proceso.StartInfo.ErrorDialog = false;
                    Proceso.StartInfo.FileName = Ruta;
                    Proceso.StartInfo.UseShellExecute = true;
                    Proceso.StartInfo.Verb = "open";
                    Proceso.StartInfo.WindowStyle = Estado;
                    if (File.Exists(Ruta)) Proceso.StartInfo.WorkingDirectory = Ruta;
                    else if (Directory.Exists(Ruta)) Proceso.StartInfo.WorkingDirectory = Ruta;
                    bool Resultado;
                    try { Resultado = Proceso.Start(); }
                    catch { Resultado = false; }
                    Proceso.Close();
                    Proceso.Dispose();
                    Proceso = null;
                    return Resultado;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return false;
        }

        /// <summary>
        /// Executes the specified file, directory or URL, with the specified window style.
        /// </summary>
        /// <param name="Ruta">Any valid file or directory path.</param>
        /// <param name="Estado">Any valid window style.</param>
        /// <returns>Returns true if the process can be executed. Returns false if it can't be executed.</returns>
        internal static Process Ejecutar_Ruta_Proceso(string Ruta, ProcessWindowStyle Estado)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta))
                {
                    Process Proceso = new Process();
                    Proceso.StartInfo.Arguments = null;
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

        internal static Bitmap Obtener_Imagen_Autozoom(Bitmap Imagen_Original, int Ancho_Cliente, int Alto_Cliente, bool Autozoom, CheckState Antialiasing, out int Zoom)
        {
            Zoom = 1;
            try
            {
                if (Imagen_Original != null && Autozoom)
                {
                    int Ancho = Imagen_Original.Width;
                    int Alto = Imagen_Original.Height;
                    if (Ancho_Cliente <= 0) Ancho_Cliente = 1;
                    if (Alto_Cliente <= 0) Alto_Cliente = 1;
                    int Ancho_Zoom = Ancho_Cliente / Ancho;
                    int Alto_Zoom = Alto_Cliente / Alto;
                    Zoom = Math.Max(Math.Min(Ancho_Zoom, Alto_Zoom), 1);
                    Ancho_Zoom = Ancho * Zoom;
                    Alto_Zoom = Alto * Zoom;
                    Bitmap Imagen = new Bitmap(Ancho_Zoom, Alto_Zoom, PixelFormat.Format32bppArgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    //Pintar.Clear(Color.Black);
                    Pintar.CompositingMode = CompositingMode.SourceOver;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = Antialiasing == CheckState.Unchecked ? InterpolationMode.NearestNeighbor : Antialiasing == CheckState.Checked ? InterpolationMode.HighQualityBilinear : InterpolationMode.HighQualityBicubic;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.None;
                    Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho_Zoom, Alto_Zoom), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                    Pintar.Dispose();
                    Pintar = null;
                    return Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return Imagen_Original;
        }

        internal static Bitmap Obtener_Imagen_Zoom(Bitmap Imagen_Original, int Zoom, CheckState Antialiasing)
        {
            try
            {
                if (Zoom < 1) Zoom = 1;
                int Ancho = Imagen_Original.Width;
                int Alto = Imagen_Original.Height;
                int Ancho_Zoom = Ancho * Zoom;
                int Alto_Zoom = Alto * Zoom;
                Bitmap Imagen = new Bitmap(Ancho_Zoom, Alto_Zoom, PixelFormat.Format32bppArgb);
                Graphics Pintar = Graphics.FromImage(Imagen);
                Pintar.Clear(Color.Black);
                Pintar.CompositingMode = CompositingMode.SourceOver;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = Antialiasing == CheckState.Unchecked ? InterpolationMode.NearestNeighbor : Antialiasing == CheckState.Checked ? InterpolationMode.HighQualityBilinear : InterpolationMode.HighQualityBicubic;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.None;
                Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho_Zoom, Alto_Zoom), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                Pintar.Dispose();
                Pintar = null;
                return Imagen;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return Imagen_Original;
        }

        /// <summary>
        /// Used mostly to obtain a JPEG codec to export images with the desired compression.
        /// </summary>
        /// <param name="Identificador">Use ImageFormat.Jpeg.Guid.</param>
        /// <returns>Returns the codec used to export images with the specified format.</returns>
        internal static ImageCodecInfo Obtener_Imagen_Codificador_Guid(Guid Identificador)
        {
            try
            {
                ImageCodecInfo[] Matriz_Codificadores = ImageCodecInfo.GetImageEncoders();
                if (Matriz_Codificadores != null && Matriz_Codificadores.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Codificadores.Length; Índice++)
                    {
                        try
                        {
                            if (Matriz_Codificadores[Índice].FormatID == Identificador)
                            {
                                return Matriz_Codificadores[Índice];
                            }
                        }
                        catch { continue; }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static Bitmap Obtener_Imagen_Color(Color Color_ARGB)
        {
            try
            {
                Bitmap Imagen = new Bitmap(16, 16, PixelFormat.Format24bppRgb);
                Graphics Pintar = Graphics.FromImage(Imagen);
                if (Color_ARGB.A < 255)
                {
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    //Pintar.DrawImage(Resources.Fondo, new Rectangle(0, 0, 16, 16), new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Resources.Fondo, new Rectangle(1, 1, 14, 14), new Rectangle(1, 1, 14, 14), GraphicsUnit.Pixel);
                }
                Pintar.CompositingMode = CompositingMode.SourceOver;
                SolidBrush Pincel = new SolidBrush(Color_ARGB);
                //Pintar.FillRectangle(Pincel, 0, 0, 16, 16);
                Pintar.FillRectangle(Pincel, 1, 1, 14, 14);
                Pincel.Dispose();
                Pincel = null;
                Pintar.Dispose();
                Pintar = null;
                return Imagen;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Returns a square image based on the original one and if that is not squared then it's center will be cut with the same width and height.
        /// </summary>
        /// <param name="Imagen_Original">Any valid bitmap image.</param>
        /// <param name="Centrar">True if the returned image must be cut in the center of the original one. False if it should be cut in the top left corner.</param>
        /// <returns>Returns the original image converted into a squared one. Returns null on any error.</returns>
        internal static Bitmap Obtener_Imagen_Cuadrada(Bitmap Imagen_Original, bool Centrar)
        {
            try
            {
                return Obtener_Imagen_Rectangular(Imagen_Original, 1, 1, Centrar);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Desaturates the selected image.
        /// </summary>
        /// <param name="Imagen_Original">Any valid image.</param>
        /// <param name="Matriz_Bytes_Filtros">A byte array with 256 bytes, used to reassign each of the 256 possible RGB values to another ones. To combine multiple byte arrays at once use the function "Combinar_Matrices_Bytes_Filtros()".</param>
        /// <returns></returns>
        internal static Bitmap Obtener_Imagen_Desaturada(Image Imagen_Original, bool Desaturar_HSV)
        {
            try
            {
                if (Imagen_Original != null)
                {
                    int Ancho = Imagen_Original.Width;
                    int Alto = Imagen_Original.Height;
                    // Apply the desired filters in a copy of the original image.
                    Bitmap Imagen = new Bitmap(Ancho, Alto, Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.HighQuality;
                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                    // Redraw the image as a copy in one of the available pixel formats.
                    Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                    Pintar.Dispose();
                    Pintar = null;

                    BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                    byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                    // Copy all the (A)RGB pixels from the image inside the byte array.
                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                    // Increase 3 bytes if the image is RGB or 4 if it's ARGB.
                    int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                    // After each horizontal (X) row add the possible byte difference between 4 bytes.
                    int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                    if (!Desaturar_HSV)
                    {
                        int Valor;
                        for (int Y = 0, Índice_Byte = 0; Y < Alto; Y++, Índice_Byte += Bytes_Diferencia)
                        {
                            for (int X = 0; X < Ancho; X++, Índice_Byte += Bytes_Aumento)
                            {
                                // Note that the (A)RGB colors are stored as BGR(A) order (inverted).
                                Valor = (int)Math.Round((double)(Matriz_Bytes[Índice_Byte + 2] + Matriz_Bytes[Índice_Byte + 1] + Matriz_Bytes[Índice_Byte]) / 3d, MidpointRounding.AwayFromZero);
                                if (Valor < 0) Valor = 0;
                                else if (Valor > 255) Valor = 255;
                                Matriz_Bytes[Índice_Byte] = (byte)Valor; // Blue.
                                Matriz_Bytes[Índice_Byte + 1] = Matriz_Bytes[Índice_Byte]; // Green.
                                Matriz_Bytes[Índice_Byte + 2] = Matriz_Bytes[Índice_Byte]; // Red.
                            }
                        }
                    }
                    else
                    {
                        double Matiz, Saturación, Luminosidad;
                        int Valor;
                        for (int Y = 0, Índice_Byte = 0; Y < Alto; Y++, Índice_Byte += Bytes_Diferencia)
                        {
                            for (int X = 0; X < Ancho; X++, Índice_Byte += Bytes_Aumento)
                            {
                                // Note that the (A)RGB colors are stored as BGR(A) order (inverted).
                                HSL.From_RGB(Matriz_Bytes[Índice_Byte + 2], Matriz_Bytes[Índice_Byte + 1], Matriz_Bytes[Índice_Byte], out Matiz, out Saturación, out Luminosidad);
                                Valor = (int)Math.Round((Luminosidad * 255d) / 100d, MidpointRounding.AwayFromZero);
                                if (Valor < 0) Valor = 0;
                                else if (Valor > 255) Valor = 255;
                                Matriz_Bytes[Índice_Byte] = (byte)Valor; // Blue.
                                Matriz_Bytes[Índice_Byte + 1] = Matriz_Bytes[Índice_Byte]; // Green.
                                Matriz_Bytes[Índice_Byte + 2] = Matriz_Bytes[Índice_Byte]; // Red.
                            }
                        }
                    }
                    // Copy back the modified byte array with the (A)RGB pixels.
                    Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                    Imagen.UnlockBits(Bitmap_Data);
                    Bitmap_Data = null;
                    return Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static Bitmap Obtener_Imagen_Rectangular(Bitmap Imagen_Original, int Relación_Ancho, int Relación_Alto, bool Centrar)
        {
            try
            {
                if (Imagen_Original != null)
                {
                    int Ancho_Original = Imagen_Original.Width;
                    int Alto_Original = Imagen_Original.Height;
                    int Ancho_Alto = Ancho_Original;
                    /*if (Ancho_Original != Alto_Original)
                    {
                        if (Ancho_Original < Alto_Original)
                        {
                            Ancho_Alto = Ancho_Original;
                            if (Centrar) Y += (Alto_Original - Ancho_Original) / 2;
                        }
                        else
                        {
                            Ancho_Alto = Alto_Original;
                            if (Centrar) X += (Ancho_Original - Alto_Original) / 2;
                        }
                    }*/
                    int Mínimo_Ancho = Ancho_Original / Relación_Ancho;
                    int Mínimo_Alto = Alto_Original / Relación_Alto;
                    int Mínimo_Ancho_Alto = Math.Min(Mínimo_Ancho, Mínimo_Alto);
                    int Ancho = Mínimo_Ancho_Alto * Relación_Ancho;
                    int Alto = Mínimo_Ancho_Alto * Relación_Alto;
                    int X = Centrar ? (Ancho_Original - Ancho) / 2 : 0;
                    int Y = Centrar ? (Alto_Original - Alto) / 2 : 0;

                    Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(X, Y, Ancho, Alto), GraphicsUnit.Pixel);
                    Pintar.Dispose();
                    Pintar = null;
                    return Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Byte array that holds up the 256 byte values modified with the inverted square root function. Useful to give darkness to any image.
        /// </summary>
        internal static readonly byte[] Matriz_Bytes_Filtro_Raíz_Cuadrada_Menos = new byte[256] { 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14, 15, 15, 16, 17, 17, 18, 18, 19, 19, 20, 20, 21, 21, 22, 22, 23, 24, 24, 25, 25, 26, 26, 27, 27, 28, 29, 29, 30, 30, 31, 31, 32, 33, 33, 34, 34, 35, 35, 36, 37, 37, 38, 38, 39, 40, 40, 41, 41, 42, 43, 43, 44, 44, 45, 46, 46, 47, 47, 48, 49, 49, 50, 51, 51, 52, 52, 53, 54, 54, 55, 56, 56, 57, 57, 58, 59, 59, 60, 61, 61, 62, 63, 63, 64, 65, 65, 66, 67, 67, 68, 69, 69, 70, 71, 72, 72, 73, 74, 74, 75, 76, 76, 77, 78, 79, 79, 80, 81, 82, 82, 83, 84, 85, 85, 86, 87, 88, 88, 89, 90, 91, 91, 92, 93, 94, 95, 95, 96, 97, 98, 99, 99, 100, 101, 102, 103, 104, 104, 105, 106, 107, 108, 109, 110, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 136, 137, 138, 139, 140, 141, 142, 143, 144, 146, 147, 148, 149, 150, 152, 153, 154, 155, 157, 158, 159, 161, 162, 163, 165, 166, 168, 169, 171, 172, 174, 175, 177, 178, 180, 182, 184, 185, 187, 189, 191, 193, 195, 197, 200, 202, 205, 207, 210, 213, 216, 219, 223, 227, 232, 239, 255 };
        /// <summary>
        /// Byte array that holds up the 256 byte values modified with the square root function. Useful to give brightness to any image.
        /// </summary>
        internal static readonly byte[] Matriz_Bytes_Filtro_Raíz_Cuadrada = new byte[256] { 0, 16, 23, 28, 32, 36, 39, 42, 45, 48, 50, 53, 55, 58, 60, 62, 64, 66, 68, 70, 71, 73, 75, 77, 78, 80, 81, 83, 84, 86, 87, 89, 90, 92, 93, 94, 96, 97, 98, 100, 101, 102, 103, 105, 106, 107, 108, 109, 111, 112, 113, 114, 115, 116, 117, 118, 119, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 145, 146, 147, 148, 149, 150, 151, 151, 152, 153, 154, 155, 156, 156, 157, 158, 159, 160, 160, 161, 162, 163, 164, 164, 165, 166, 167, 167, 168, 169, 170, 170, 171, 172, 173, 173, 174, 175, 176, 176, 177, 178, 179, 179, 180, 181, 181, 182, 183, 183, 184, 185, 186, 186, 187, 188, 188, 189, 190, 190, 191, 192, 192, 193, 194, 194, 195, 196, 196, 197, 198, 198, 199, 199, 200, 201, 201, 202, 203, 203, 204, 204, 205, 206, 206, 207, 208, 208, 209, 209, 210, 211, 211, 212, 212, 213, 214, 214, 215, 215, 216, 217, 217, 218, 218, 219, 220, 220, 221, 221, 222, 222, 223, 224, 224, 225, 225, 226, 226, 227, 228, 228, 229, 229, 230, 230, 231, 231, 232, 233, 233, 234, 234, 235, 235, 236, 236, 237, 237, 238, 238, 239, 240, 240, 241, 241, 242, 242, 243, 243, 244, 244, 245, 245, 246, 246, 247, 247, 248, 248, 249, 249, 250, 250, 251, 251, 252, 252, 253, 253, 254, 254, 255 };
        /// <summary>
        /// Byte array that holds up the 256 byte values modified with the inverted logarithm function. Useful to give extra darkness to any image.
        /// </summary>
        internal static readonly byte[] Matriz_Bytes_Filtro_Logaritmo_Menos = new byte[256] { 0, 0, 0, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 8, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10, 11, 11, 11, 11, 12, 12, 12, 12, 13, 13, 13, 13, 14, 14, 14, 14, 14, 15, 15, 15, 15, 16, 16, 16, 17, 17, 17, 17, 18, 18, 18, 18, 19, 19, 19, 19, 20, 20, 20, 21, 21, 21, 21, 22, 22, 22, 23, 23, 23, 23, 24, 24, 24, 25, 25, 25, 26, 26, 26, 27, 27, 27, 28, 28, 28, 29, 29, 29, 30, 30, 30, 31, 31, 31, 32, 32, 32, 33, 33, 33, 34, 34, 35, 35, 35, 36, 36, 37, 37, 37, 38, 38, 39, 39, 39, 40, 40, 41, 41, 42, 42, 42, 43, 43, 44, 44, 45, 45, 46, 46, 47, 47, 48, 48, 49, 49, 50, 50, 51, 51, 52, 53, 53, 54, 54, 55, 55, 56, 57, 57, 58, 59, 59, 60, 61, 61, 62, 63, 63, 64, 65, 65, 66, 67, 68, 69, 69, 70, 71, 72, 73, 74, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 85, 86, 87, 88, 89, 91, 92, 93, 95, 96, 97, 99, 101, 102, 104, 106, 107, 109, 111, 113, 115, 118, 120, 123, 125, 128, 131, 134, 138, 141, 145, 150, 155, 160, 166, 173, 182, 192, 205, 224, 255 };
        /// <summary>
        /// Byte array that holds up the 256 byte values modified with the logarithm function. Useful to give extra brightness to any image.
        /// </summary>
        internal static readonly byte[] Matriz_Bytes_Filtro_Logaritmo = new byte[256] { 0, 31, 50, 63, 73, 82, 89, 95, 100, 105, 110, 114, 117, 121, 124, 127, 130, 132, 135, 137, 140, 142, 144, 146, 148, 149, 151, 153, 154, 156, 158, 159, 160, 162, 163, 164, 166, 167, 168, 169, 170, 172, 173, 174, 175, 176, 177, 178, 179, 180, 181, 181, 182, 183, 184, 185, 186, 186, 187, 188, 189, 190, 190, 191, 192, 192, 193, 194, 194, 195, 196, 196, 197, 198, 198, 199, 200, 200, 201, 201, 202, 202, 203, 204, 204, 205, 205, 206, 206, 207, 207, 208, 208, 209, 209, 210, 210, 211, 211, 212, 212, 213, 213, 213, 214, 214, 215, 215, 216, 216, 216, 217, 217, 218, 218, 218, 219, 219, 220, 220, 220, 221, 221, 222, 222, 222, 223, 223, 223, 224, 224, 224, 225, 225, 225, 226, 226, 226, 227, 227, 227, 228, 228, 228, 229, 229, 229, 230, 230, 230, 231, 231, 231, 232, 232, 232, 232, 233, 233, 233, 234, 234, 234, 234, 235, 235, 235, 236, 236, 236, 236, 237, 237, 237, 237, 238, 238, 238, 238, 239, 239, 239, 240, 240, 240, 240, 241, 241, 241, 241, 241, 242, 242, 242, 242, 243, 243, 243, 243, 244, 244, 244, 244, 245, 245, 245, 245, 245, 246, 246, 246, 246, 247, 247, 247, 247, 247, 248, 248, 248, 248, 248, 249, 249, 249, 249, 249, 250, 250, 250, 250, 250, 251, 251, 251, 251, 251, 252, 252, 252, 252, 252, 253, 253, 253, 253, 253, 254, 254, 254, 254, 254, 254, 255, 255, 255 };
        /// <summary>
        /// Byte array that holds up the 256 byte values modified with the base 2 bit inversion function. This filter can be undone by using it a second time.
        /// </summary>
        internal static readonly byte[] Matriz_Bytes_Filtro_Invertir_Bits_Base_2 = new byte[256] { 0, 128, 64, 192, 32, 160, 96, 224, 16, 144, 80, 208, 48, 176, 112, 240, 8, 136, 72, 200, 40, 168, 104, 232, 24, 152, 88, 216, 56, 184, 120, 248, 4, 132, 68, 196, 36, 164, 100, 228, 20, 148, 84, 212, 52, 180, 116, 244, 12, 140, 76, 204, 44, 172, 108, 236, 28, 156, 92, 220, 60, 188, 124, 252, 2, 130, 66, 194, 34, 162, 98, 226, 18, 146, 82, 210, 50, 178, 114, 242, 10, 138, 74, 202, 42, 170, 106, 234, 26, 154, 90, 218, 58, 186, 122, 250, 6, 134, 70, 198, 38, 166, 102, 230, 22, 150, 86, 214, 54, 182, 118, 246, 14, 142, 78, 206, 46, 174, 110, 238, 30, 158, 94, 222, 62, 190, 126, 254, 1, 129, 65, 193, 33, 161, 97, 225, 17, 145, 81, 209, 49, 177, 113, 241, 9, 137, 73, 201, 41, 169, 105, 233, 25, 153, 89, 217, 57, 185, 121, 249, 5, 133, 69, 197, 37, 165, 101, 229, 21, 149, 85, 213, 53, 181, 117, 245, 13, 141, 77, 205, 45, 173, 109, 237, 29, 157, 93, 221, 61, 189, 125, 253, 3, 131, 67, 195, 35, 163, 99, 227, 19, 147, 83, 211, 51, 179, 115, 243, 11, 139, 75, 203, 43, 171, 107, 235, 27, 155, 91, 219, 59, 187, 123, 251, 7, 135, 71, 199, 39, 167, 103, 231, 23, 151, 87, 215, 55, 183, 119, 247, 15, 143, 79, 207, 47, 175, 111, 239, 31, 159, 95, 223, 63, 191, 127, 255 };
        /// <summary>
        /// Byte array that holds up the 256 byte values modified with the base 4 bit inversion function. This filter can be undone by using it a second time.
        /// </summary>
        internal static readonly byte[] Matriz_Bytes_Filtro_Invertir_Bits_Base_4 = new byte[256] { 0, 64, 128, 192, 16, 80, 144, 208, 32, 96, 160, 224, 48, 112, 176, 240, 4, 68, 132, 196, 20, 84, 148, 212, 36, 100, 164, 228, 52, 116, 180, 244, 8, 72, 136, 200, 24, 88, 152, 216, 40, 104, 168, 232, 56, 120, 184, 248, 12, 76, 140, 204, 28, 92, 156, 220, 44, 108, 172, 236, 60, 124, 188, 252, 1, 65, 129, 193, 17, 81, 145, 209, 33, 97, 161, 225, 49, 113, 177, 241, 5, 69, 133, 197, 21, 85, 149, 213, 37, 101, 165, 229, 53, 117, 181, 245, 9, 73, 137, 201, 25, 89, 153, 217, 41, 105, 169, 233, 57, 121, 185, 249, 13, 77, 141, 205, 29, 93, 157, 221, 45, 109, 173, 237, 61, 125, 189, 253, 2, 66, 130, 194, 18, 82, 146, 210, 34, 98, 162, 226, 50, 114, 178, 242, 6, 70, 134, 198, 22, 86, 150, 214, 38, 102, 166, 230, 54, 118, 182, 246, 10, 74, 138, 202, 26, 90, 154, 218, 42, 106, 170, 234, 58, 122, 186, 250, 14, 78, 142, 206, 30, 94, 158, 222, 46, 110, 174, 238, 62, 126, 190, 254, 3, 67, 131, 195, 19, 83, 147, 211, 35, 99, 163, 227, 51, 115, 179, 243, 7, 71, 135, 199, 23, 87, 151, 215, 39, 103, 167, 231, 55, 119, 183, 247, 11, 75, 139, 203, 27, 91, 155, 219, 43, 107, 171, 235, 59, 123, 187, 251, 15, 79, 143, 207, 31, 95, 159, 223, 47, 111, 175, 239, 63, 127, 191, 255 };
        /// <summary>
        /// Byte array that holds up the 256 byte values modified with the base 16 bit inversion function. This filter can be undone by using it a second time.
        /// </summary>
        internal static readonly byte[] Matriz_Bytes_Filtro_Invertir_Bits_Base_16 = new byte[256] { 0, 16, 32, 48, 64, 80, 96, 112, 128, 144, 160, 176, 192, 208, 224, 240, 1, 17, 33, 49, 65, 81, 97, 113, 129, 145, 161, 177, 193, 209, 225, 241, 2, 18, 34, 50, 66, 82, 98, 114, 130, 146, 162, 178, 194, 210, 226, 242, 3, 19, 35, 51, 67, 83, 99, 115, 131, 147, 163, 179, 195, 211, 227, 243, 4, 20, 36, 52, 68, 84, 100, 116, 132, 148, 164, 180, 196, 212, 228, 244, 5, 21, 37, 53, 69, 85, 101, 117, 133, 149, 165, 181, 197, 213, 229, 245, 6, 22, 38, 54, 70, 86, 102, 118, 134, 150, 166, 182, 198, 214, 230, 246, 7, 23, 39, 55, 71, 87, 103, 119, 135, 151, 167, 183, 199, 215, 231, 247, 8, 24, 40, 56, 72, 88, 104, 120, 136, 152, 168, 184, 200, 216, 232, 248, 9, 25, 41, 57, 73, 89, 105, 121, 137, 153, 169, 185, 201, 217, 233, 249, 10, 26, 42, 58, 74, 90, 106, 122, 138, 154, 170, 186, 202, 218, 234, 250, 11, 27, 43, 59, 75, 91, 107, 123, 139, 155, 171, 187, 203, 219, 235, 251, 12, 28, 44, 60, 76, 92, 108, 124, 140, 156, 172, 188, 204, 220, 236, 252, 13, 29, 45, 61, 77, 93, 109, 125, 141, 157, 173, 189, 205, 221, 237, 253, 14, 30, 46, 62, 78, 94, 110, 126, 142, 158, 174, 190, 206, 222, 238, 254, 15, 31, 47, 63, 79, 95, 111, 127, 143, 159, 175, 191, 207, 223, 239, 255 };
        /// <summary>
        /// Byte array that holds up the 256 byte values modified with the negative function. This filter can be undone by using it a second time.
        /// </summary>
        internal static readonly byte[] Matriz_Bytes_Filtro_Negativo = new byte[256] { 255, 254, 253, 252, 251, 250, 249, 248, 247, 246, 245, 244, 243, 242, 241, 240, 239, 238, 237, 236, 235, 234, 233, 232, 231, 230, 229, 228, 227, 226, 225, 224, 223, 222, 221, 220, 219, 218, 217, 216, 215, 214, 213, 212, 211, 210, 209, 208, 207, 206, 205, 204, 203, 202, 201, 200, 199, 198, 197, 196, 195, 194, 193, 192, 191, 190, 189, 188, 187, 186, 185, 184, 183, 182, 181, 180, 179, 178, 177, 176, 175, 174, 173, 172, 171, 170, 169, 168, 167, 166, 165, 164, 163, 162, 161, 160, 159, 158, 157, 156, 155, 154, 153, 152, 151, 150, 149, 148, 147, 146, 145, 144, 143, 142, 141, 140, 139, 138, 137, 136, 135, 134, 133, 132, 131, 130, 129, 128, 127, 126, 125, 124, 123, 122, 121, 120, 119, 118, 117, 116, 115, 114, 113, 112, 111, 110, 109, 108, 107, 106, 105, 104, 103, 102, 101, 100, 99, 98, 97, 96, 95, 94, 93, 92, 91, 90, 89, 88, 87, 86, 85, 84, 83, 82, 81, 80, 79, 78, 77, 76, 75, 74, 73, 72, 71, 70, 69, 68, 67, 66, 65, 64, 63, 62, 61, 60, 59, 58, 57, 56, 55, 54, 53, 52, 51, 50, 49, 48, 47, 46, 45, 44, 43, 42, 41, 40, 39, 38, 37, 36, 35, 34, 33, 32, 31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };

        /// <summary>
        /// Combines the desired 256 bytes arrays into one, used to apply at once multiple filters like logarithm and square root. Note: this 256 bytes array is designed to work with images, but it can also work with sound or any binary value actually, but remember that using other filters than negative or base 2, 4 or 16 bit inversions (and a few other ones), 2 times in a row won't give back the original byte values, so be aware of that.
        /// </summary>
        /// <param name="Matrices_Bytes_Filtros">Any array made up of byte arrays with 256 bytes each.</param>
        /// <returns>Returns a 256 bytes array with all the filters at once. Returns null on any error.</returns>
        internal static byte[] Combinar_Matrices_Bytes_Filtros(byte[][] Matrices_Bytes_Filtros)
        {
            try
            {
                byte[] Matriz_Bytes_Filtros = new byte[256];
                for (int Índice = 0; Índice < 256; Índice++)
                {
                    Matriz_Bytes_Filtros[Índice] = (byte)Índice; // Start with the default values.
                }
                if (Matrices_Bytes_Filtros != null && Matrices_Bytes_Filtros.Length > 0)
                {
                    for (int Índice_Filtro = 0; Índice_Filtro < Matrices_Bytes_Filtros.Length; Índice_Filtro++)
                    {
                        // Ignore the null or empty byte arrays.
                        if (Matrices_Bytes_Filtros[Índice_Filtro] != null && Matrices_Bytes_Filtros[Índice_Filtro].Length >= 256)
                        {
                            for (int Índice = 0; Índice < 256; Índice++)
                            {
                                // Adapt each time the start values to get the finished ones.
                                Matriz_Bytes_Filtros[Índice] = Matrices_Bytes_Filtros[Índice_Filtro][Matriz_Bytes_Filtros[Índice]];
                            }
                        }
                    }
                }
                return Matriz_Bytes_Filtros; // Return the combined byte array with all the filters.
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Compares pixel by pixel all the ARGB channels of 2 images to see in detail if they are the same one or not.
        /// </summary>
        /// <param name="Ruta_1">The file path to the first valid image.</param>
        /// <param name="Ruta_2">The file path to the second valid image.</param>
        /// <returns>Returns the percentage of equal pixels between both images. Returns "double.MinValue" on any error.</returns>
        internal static double Comparar_Píxeles_Imágenes(string Ruta_1, string Ruta_2)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta_1) && File.Exists(Ruta_1) &&
                    !string.IsNullOrEmpty(Ruta_2) && File.Exists(Ruta_2))
                {
                    // Load both images with forced alpha to avoid it missing in only one of them.
                    Bitmap Imagen_1 = Program.Cargar_Imagen_Ruta(Ruta_1, CheckState.Checked);
                    Bitmap Imagen_2 = Program.Cargar_Imagen_Ruta(Ruta_2, CheckState.Checked);
                    if (Imagen_1 != null && Imagen_2 != null)
                    {
                        int Ancho_1 = Imagen_1.Width;
                        int Alto_1 = Imagen_1.Height;
                        int Ancho_2 = Imagen_2.Width;
                        int Alto_2 = Imagen_2.Height;
                        if (Ancho_1 == Ancho_2 &&
                            Alto_1 == Alto_2)
                        {
                            BitmapData Bitmap_Data_1 = Imagen_1.LockBits(new Rectangle(0, 0, Ancho_1, Alto_1), ImageLockMode.ReadOnly, Imagen_1.PixelFormat);
                            int Ancho_Stride_1 = Math.Abs(Bitmap_Data_1.Stride);
                            int Bytes_Aumento_1 = !Image.IsAlphaPixelFormat(Imagen_1.PixelFormat) ? 3 : 4;
                            int Bytes_Diferencia_1 = Ancho_Stride_1 - ((Ancho_1 * Image.GetPixelFormatSize(Imagen_1.PixelFormat)) / 8);
                            byte[] Matriz_Bytes_ARGB_1 = new byte[Ancho_Stride_1 * Alto_1];
                            Marshal.Copy(Bitmap_Data_1.Scan0, Matriz_Bytes_ARGB_1, 0, Matriz_Bytes_ARGB_1.Length);
                            Imagen_1.UnlockBits(Bitmap_Data_1);
                            Bitmap_Data_1 = null;

                            BitmapData Bitmap_Data_2 = Imagen_2.LockBits(new Rectangle(0, 0, Ancho_2, Alto_2), ImageLockMode.ReadOnly, Imagen_2.PixelFormat);
                            int Ancho_Stride_2 = Math.Abs(Bitmap_Data_2.Stride);
                            int Bytes_Aumento_2 = !Image.IsAlphaPixelFormat(Imagen_2.PixelFormat) ? 3 : 4;
                            int Bytes_Diferencia_2 = Ancho_Stride_2 - ((Ancho_2 * Image.GetPixelFormatSize(Imagen_2.PixelFormat)) / 8);
                            byte[] Matriz_Bytes_ARGB_2 = new byte[Ancho_Stride_2 * Alto_2];
                            Marshal.Copy(Bitmap_Data_2.Scan0, Matriz_Bytes_ARGB_2, 0, Matriz_Bytes_ARGB_2.Length);
                            Imagen_2.UnlockBits(Bitmap_Data_2);
                            Bitmap_Data_2 = null;

                            int Píxeles = Ancho_1 * Alto_1;
                            int Píxeles_Iguales = 0;
                            /*int Píxeles_Diferentes = 0;
                            int Píxeles_Diferentes_A = 0;
                            int Píxeles_Diferentes_R = 0;
                            int Píxeles_Diferentes_G = 0;
                            int Píxeles_Diferentes_B = 0;
                            bool Alfa_Igual = false;
                            bool Rojo_Igual = false;
                            bool Verde_Igual = false;
                            bool Azul_Igual = false;*/
                            for (int Y = 0, Índice = 0; Y < Alto_1; Y++, Índice += Bytes_Diferencia_1)
                            {
                                for (int X = 0; X < Ancho_1; X++, Índice += Bytes_Aumento_1)
                                {
                                    if (Matriz_Bytes_ARGB_1[Índice + 3] == Matriz_Bytes_ARGB_2[Índice + 3] &&
                                        Matriz_Bytes_ARGB_1[Índice + 2] == Matriz_Bytes_ARGB_2[Índice + 2] &&
                                        Matriz_Bytes_ARGB_1[Índice + 1] == Matriz_Bytes_ARGB_2[Índice + 1] &&
                                        Matriz_Bytes_ARGB_1[Índice] == Matriz_Bytes_ARGB_2[Índice])
                                    {
                                        Píxeles_Iguales++;
                                    }
                                    /*Alfa_Igual = Matriz_Bytes_ARGB_1[Índice + 3] == Matriz_Bytes_ARGB_2[Índice + 3];
                                    Rojo_Igual = Matriz_Bytes_ARGB_1[Índice + 2] == Matriz_Bytes_ARGB_2[Índice + 2];
                                    Verde_Igual = Matriz_Bytes_ARGB_1[Índice + 1] == Matriz_Bytes_ARGB_2[Índice + 1];
                                    Azul_Igual = Matriz_Bytes_ARGB_1[Índice] == Matriz_Bytes_ARGB_2[Índice];

                                    if (!Alfa_Igual) Píxeles_Diferentes_A++;
                                    if (!Rojo_Igual) Píxeles_Diferentes_R++;
                                    if (!Verde_Igual) Píxeles_Diferentes_G++;
                                    if (!Azul_Igual) Píxeles_Diferentes_B++;

                                    if (Alfa_Igual && Rojo_Igual && Verde_Igual && Azul_Igual) Píxeles_Iguales++;
                                    else Píxeles_Diferentes++;*/
                                }
                            }
                            Matriz_Bytes_ARGB_1 = null;
                            Matriz_Bytes_ARGB_2 = null;
                            return ((double)Píxeles_Iguales * 100d) / (double)Píxeles;
                        }
                        else return 0d;
                    }
                    else return 0d;
                }
                else return 0d;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return double.MinValue;
        }

        /// <summary>
        /// Generates a copy of the specified file and saves it with the specified name.
        /// </summary>
        /// <param name="Ruta_Entrada">The original file to read.</param>
        /// <param name="Ruta_Salida">The new file to generate or overwrite without asking.</param>
        /// <returns>Returns true if the file was copied without errors. Returns false otherwise.</returns>
        internal static bool Copiar_Archivo(string Ruta_Entrada, string Ruta_Salida)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta_Entrada) && !string.IsNullOrEmpty(Ruta_Salida) && File.Exists(Ruta_Entrada))
                {
                    FileStream Lector_Entrada = new FileStream(Ruta_Entrada, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    long Longitud_Total = Lector_Entrada.Length;
                    Lector_Entrada.Seek(0L, SeekOrigin.Begin);
                    if (File.Exists(Ruta_Salida)) Program.Quitar_Atributo_Sólo_Lectura(Ruta_Salida);
                    FileStream Lector_Salida = new FileStream(Ruta_Salida, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector_Salida.SetLength(0L); // Try to overwrite the file.
                    Lector_Salida.Seek(0L, SeekOrigin.Begin);
                    int Longitud_Búfer = 4096; // Buffer length of 4 KB.
                    byte[] Matriz_Bytes_Búfer = new byte[Longitud_Búfer];
                    long Longitud_Leída = 0L;
                    for (long Índice_Bloque = 0L; Índice_Bloque < Lector_Entrada.Length; Índice_Bloque += Longitud_Búfer)
                    {
                        int Longitud = Lector_Entrada.Read(Matriz_Bytes_Búfer, 0, Longitud_Búfer);
                        if (Longitud > 0)
                        {
                            Lector_Salida.Write(Matriz_Bytes_Búfer, 0, Longitud);
                            Lector_Salida.Flush();
                            Longitud_Leída += Longitud;
                        }
                    }
                    Lector_Salida.Close();
                    Lector_Salida.Dispose();
                    Lector_Salida = null;
                    Lector_Entrada.Close();
                    Lector_Entrada.Dispose();
                    Lector_Entrada = null;
                    if (Longitud_Leída == Longitud_Total) return true; // Perfect copy done.
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return false; // Something went wrong.
        }

        /// <summary>
        /// Applies the desired byte filters in a copy of the selected image, like the logarithm filter, which can reveal dark zones of any image.
        /// </summary>
        /// <param name="Imagen_Original">Any valid image.</param>
        /// <param name="Matriz_Bytes_Filtros">A byte array with 256 bytes, used to reassign each of the 256 possible RGB values to another ones. To combine multiple byte arrays at once use the function "Combinar_Matrices_Bytes_Filtros()".</param>
        /// <returns></returns>
        internal static Bitmap Obtener_Imagen_Filtrada(Image Imagen_Original, byte[] Matriz_Bytes_Filtros)
        {
            try
            {
                if (Imagen_Original != null)
                {
                    int Ancho = Imagen_Original.Width;
                    int Alto = Imagen_Original.Height;
                    // Apply the desired filters in a copy of the original image.
                    Bitmap Imagen = new Bitmap(Ancho, Alto, Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.HighQuality;
                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                    // Redraw the image as a copy in one of the available pixel formats.
                    Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                    Pintar.Dispose();
                    Pintar = null;
                    if (Matriz_Bytes_Filtros != null && Matriz_Bytes_Filtros.Length >= 256)
                    {
                        // Apply the desired filters on the whole image in a very efficient way.
                        BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                        byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                        // Copy all the (A)RGB pixels from the image inside the byte array.
                        Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                        // Increase 3 bytes if the image is RGB or 4 if it's ARGB.
                        int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                        // After each horizontal (X) row add the possible byte difference between 4 bytes.
                        int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                        for (int Y = 0, Índice_Byte = 0; Y < Alto; Y++, Índice_Byte += Bytes_Diferencia)
                        {
                            for (int X = 0; X < Ancho; X++, Índice_Byte += Bytes_Aumento)
                            {
                                // Note that the (A)RGB colors are stored as BGR(A) order (inverted).
                                Matriz_Bytes[Índice_Byte + 2] = Matriz_Bytes_Filtros[Matriz_Bytes[Índice_Byte + 2]]; // Red.
                                Matriz_Bytes[Índice_Byte + 1] = Matriz_Bytes_Filtros[Matriz_Bytes[Índice_Byte + 1]]; // Green.
                                Matriz_Bytes[Índice_Byte] = Matriz_Bytes_Filtros[Matriz_Bytes[Índice_Byte]]; // Blue.
                            }
                        }
                        // Copy back the modified byte array with the (A)RGB pixels.
                        Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                        Imagen.UnlockBits(Bitmap_Data);
                        Bitmap_Data = null;
                    }
                    return Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Obtains a miniature (or any size really) from any valid image, keeping it's original aspect ratio if desired.
        /// </summary>
        /// <param name="Imagen_Original">Any valid image.</param>
        /// <param name="Ancho_Miniatura">The desired width of the miniature.</param>
        /// <param name="Alto_Miniatura">The desired height of the miniature.</param>
        /// <param name="Relación_Aspecto">If true the miniature will keep the original aspect ratio.</param>
        /// <param name="Antialiasing">If true the miniature will be drawn with high interpolation, reducing the alias effect, at the cost of getting a bit blurred.</param>
        /// <param name="Alfa">If it's Indeterminate the returned image will contain alpha (transparency) only it if had it before. If it's Checked the returned image will always have alpha. Otherwise it will never have alpha.</param>
        /// <returns>Returns the miniature drawn with the specified options. On any error it will return null.</returns>
        internal static Bitmap Obtener_Imagen_Miniatura(Image Imagen_Original, int Ancho_Miniatura, int Alto_Miniatura, bool Relación_Aspecto, bool Antialiasing, CheckState Alfa)
        {
            try
            {
                if (Imagen_Original != null)
                {
                    int Ancho_Original = Imagen_Original.Width;
                    int Alto_Original = Imagen_Original.Height;
                    int Ancho = Ancho_Miniatura;
                    int Alto = Alto_Miniatura;
                    if (Relación_Aspecto) // Keep the original aspect ratio.
                    {
                        Ancho = (Alto_Miniatura * Ancho_Original) / Alto_Original;
                        Alto = (Ancho_Miniatura * Alto_Original) / Ancho_Original;
                        if (Ancho <= Ancho_Miniatura) Alto = Alto_Miniatura;
                        else if (Alto <= Alto_Miniatura) Ancho = Ancho_Miniatura;
                    }
                    if (Ancho < 1) Ancho = 1;
                    if (Alto < 1) Alto = 1;
                    Bitmap Imagen = new Bitmap(Ancho, Alto, Alfa == CheckState.Indeterminate ? (Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb) : Alfa == CheckState.Checked ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    //Pintar.Clear(Color.Black);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = !Antialiasing ? InterpolationMode.NearestNeighbor : InterpolationMode.HighQualityBicubic;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.None;
                    Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho_Original, Alto_Original), GraphicsUnit.Pixel);
                    Pintar.Dispose();
                    Pintar = null;
                    return Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Cuts a portion of any image.
        /// </summary>
        /// <param name="Imagen_Original">Any valid image.</param>
        /// <param name="X">Horizontal start of the cut.</param>
        /// <param name="Y">Vertical start of the cut.</param>
        /// <param name="Ancho">Width of the cut.</param>
        /// <param name="Alto">Height of the cut.</param>
        /// <param name="Alfa">If it's Indeterminate the returned image will contain alpha (transparency) only it if had it before. If it's Checked the returned image will always have alpha. Otherwise it will never have alpha.</param>
        /// <returns>Returns a cut of the original image. Returns null on any error.</returns>
        internal static Bitmap Obtener_Imagen_Recortada(Image Imagen_Original, int X, int Y, int Ancho, int Alto, CheckState Alfa)
        {
            try
            {
                if (Imagen_Original != null)
                {
                    Bitmap Imagen = new Bitmap(Ancho, Alto, Alfa == CheckState.Indeterminate ? (Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb) : Alfa == CheckState.Checked ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(X, Y, Ancho, Alto), GraphicsUnit.Pixel);
                    Pintar.Dispose();
                    Pintar = null;
                    return Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static Bitmap Obtener_Imagen_Alfa_Brillo(Bitmap Imagen)
        {
            if (Imagen != null && Image.IsAlphaPixelFormat(Imagen.PixelFormat))
            {
                int Ancho = Imagen.Width;
                int Alto = Imagen.Height;
                BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                int Bytes_Aumento = !Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 3 : 4;
                int Bytes_Diferencia = Ancho_Stride - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                byte[] Matriz_Bytes = new byte[Ancho_Stride * Alto];
                Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                {
                    for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                    {
                        int Valor = (Matriz_Bytes[Índice + 2] + Matriz_Bytes[Índice + 1] + Matriz_Bytes[Índice]) / 3;
                        Matriz_Bytes[Índice + 3] = (byte)Valor;
                    }
                }
                Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                Imagen.UnlockBits(Bitmap_Data);
                Matriz_Bytes = null;
            }
            return Imagen;
        }

        internal static Bitmap Obtener_Imagen_Bloque_3D(int Dimensiones, double Rotación, Bitmap Imagen_1, Bitmap Imagen_2, Bitmap Imagen_3, Bitmap Imagen_4, Bitmap Imagen_5, Bitmap Imagen_6, RotateFlipType Rotación_1, RotateFlipType Rotación_2, RotateFlipType Rotación_3, RotateFlipType Rotación_4, RotateFlipType Rotación_5, RotateFlipType Rotación_6, bool Tipo_Sombra_1, bool Tipo_Sombra_2, bool Tipo_Sombra_3, bool Tipo_Sombra_4, bool Tipo_Sombra_5, bool Tipo_Sombra_6, int Sombra_1, int Sombra_2, int Sombra_3, int Sombra_4, int Sombra_5, int Sombra_6, CheckState Suavizado)
        {
            try
            {
                if (Dimensiones <= 0) Dimensiones = 1; // Minimum of 1 x 1 pixel.
                double Radio = (double)Dimensiones / 2d; // Half of the hexagon or 3D cube.
                Bitmap Imagen = new Bitmap(Dimensiones, Dimensiones, PixelFormat.Format32bppArgb);
                if (Imagen_1 != null && Imagen_2 != null && Imagen_3 != null && Imagen_4 != null && Imagen_5 != null && Imagen_6 != null)
                {
                    // Get ready to draw the 3D cube with the proper quality.
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceOver;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = Suavizado == CheckState.Unchecked ? InterpolationMode.NearestNeighbor : Suavizado == CheckState.Checked ? InterpolationMode.Bicubic : InterpolationMode.HighQualityBicubic;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.HighQuality;
                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias; // Not needed.
                    Pintar.TranslateTransform((float)Radio, (float)Radio); // Center of the image.
                    if (Rotación != 0d) Pintar.RotateTransform((float)Rotación); // Custom rotation.

                    double Ángulo_Inicial = -60d; // Start angle for the hexagon.

                    // All the angles used to make an hexagon, which is a cube seen in 2D.
                    double Ángulo_Centro = 0d; // Center.
                    double Ángulo_0 = (((Ángulo_Inicial + 0d) * Math.PI) / 180d); // Top.
                    double Ángulo_60 = (((Ángulo_Inicial + 60d) * Math.PI) / 180d); // Top right.
                    double Ángulo_120 = (((Ángulo_Inicial + 120d) * Math.PI) / 180d); // Bottom right.
                    double Ángulo_180 = (((Ángulo_Inicial + 180d) * Math.PI) / 180d); // Bottom.
                    double Ángulo_240 = (((Ángulo_Inicial + 240d) * Math.PI) / 180d); // Bottom left.
                    double Ángulo_300 = (((Ángulo_Inicial + 300d) * Math.PI) / 180d); // Top left.

                    // The Sine is used to calculate the X (horizontal) position.
                    double Seno_Centro = 0d; // Center.
                    double Seno_0 = Math.Sin(Ángulo_0) * Radio; // Top.
                    double Seno_60 = Math.Sin(Ángulo_60) * Radio; // Top right.
                    double Seno_120 = Math.Sin(Ángulo_120) * Radio; // Bottom right.
                    double Seno_180 = Math.Sin(Ángulo_180) * Radio; // Bottom.
                    double Seno_240 = Math.Sin(Ángulo_240) * Radio; // Bottom left.
                    double Seno_300 = Math.Sin(Ángulo_300) * Radio; // Top left.

                    // The Cosine is used to calculate the Y (Vertical) position.
                    double Coseno_Centro = 0d; // Center.
                    double Coseno_0 = Math.Cos(Ángulo_0) * Radio; // Top.
                    double Coseno_60 = Math.Cos(Ángulo_60) * Radio; // Top right.
                    double Coseno_120 = Math.Cos(Ángulo_120) * Radio; // Bottom right.
                    double Coseno_180 = Math.Cos(Ángulo_180) * Radio; // Bottom.
                    double Coseno_240 = Math.Cos(Ángulo_240) * Radio; // Bottom left.
                    double Coseno_300 = Math.Cos(Ángulo_300) * Radio; // Top left.

                    // Filter the lightness in the images to make the sides more realistic.
                    if (!Tipo_Sombra_1) Imagen_1 = Obtener_Imagen_Brillo(Imagen_1, Sombra_1);
                    else Imagen_1 = Obtener_Imagen_Intensidad(Imagen_1, Sombra_1);
                    if (!Tipo_Sombra_2) Imagen_2 = Obtener_Imagen_Brillo(Imagen_2, Sombra_2);
                    else Imagen_2 = Obtener_Imagen_Intensidad(Imagen_2, Sombra_2);
                    if (!Tipo_Sombra_3) Imagen_3 = Obtener_Imagen_Brillo(Imagen_3, Sombra_3);
                    else Imagen_3 = Obtener_Imagen_Intensidad(Imagen_3, Sombra_3);
                    if (!Tipo_Sombra_4) Imagen_4 = Obtener_Imagen_Brillo(Imagen_4, Sombra_4);
                    else Imagen_4 = Obtener_Imagen_Intensidad(Imagen_4, Sombra_4);
                    if (!Tipo_Sombra_5) Imagen_5 = Obtener_Imagen_Brillo(Imagen_5, Sombra_5);
                    else Imagen_5 = Obtener_Imagen_Intensidad(Imagen_5, Sombra_5);
                    if (!Tipo_Sombra_6) Imagen_6 = Obtener_Imagen_Brillo(Imagen_6, Sombra_6);
                    else Imagen_6 = Obtener_Imagen_Intensidad(Imagen_6, Sombra_6);

                    // Rotate and flip the images like Minecraft does.
                    Imagen_1.RotateFlip(Rotación_1);
                    Imagen_2.RotateFlip(Rotación_2);
                    Imagen_3.RotateFlip(Rotación_3);
                    Imagen_4.RotateFlip(Rotación_4);
                    Imagen_5.RotateFlip(Rotación_5);
                    Imagen_6.RotateFlip(Rotación_6);

                    // Draw the 6 sides of the 3D cube with high quality float.
                    Pintar.DrawImage(Imagen_1, new PointF[] { new PointF((float)Seno_0, (float)Coseno_0), new PointF((float)Seno_300, (float)Coseno_300), new PointF((float)Seno_Centro, (float)Coseno_Centro) });
                    Pintar.DrawImage(Imagen_2, new PointF[] { new PointF((float)Seno_240, (float)Coseno_240), new PointF((float)Seno_180, (float)Coseno_180), new PointF((float)Seno_Centro, (float)Coseno_Centro) });
                    Pintar.DrawImage(Imagen_3, new PointF[] { new PointF((float)Seno_60, (float)Coseno_60), new PointF((float)Seno_0, (float)Coseno_0), new PointF((float)Seno_120, (float)Coseno_120) });
                    Pintar.DrawImage(Imagen_4, new PointF[] { new PointF((float)Seno_300, (float)Coseno_300), new PointF((float)Seno_Centro, (float)Coseno_Centro), new PointF((float)Seno_240, (float)Coseno_240) });
                    Pintar.DrawImage(Imagen_5, new PointF[] { new PointF((float)Seno_0, (float)Coseno_0), new PointF((float)Seno_60, (float)Coseno_60), new PointF((float)Seno_300, (float)Coseno_300) });
                    Pintar.DrawImage(Imagen_6, new PointF[] { new PointF((float)Seno_Centro, (float)Coseno_Centro), new PointF((float)Seno_60, (float)Coseno_60), new PointF((float)Seno_180, (float)Coseno_180) });
                    Pintar.Dispose();
                    Pintar = null;
                }
                return Imagen; // Return the resulting image.
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null; // Return null on any error.
        }

        /// <summary>
        /// A quicker function to generate a block in 3D with a single image.
        /// </summary>
        internal static Bitmap Obtener_Imagen_Bloque_3D(int Dimensiones, double Rotación, Bitmap Imagen, CheckState Suavizado)
        {
            try
            {
                return Obtener_Imagen_Bloque_3D(Dimensiones, Rotación, Imagen.Clone() as Bitmap, Imagen.Clone() as Bitmap, Imagen.Clone() as Bitmap, Imagen.Clone() as Bitmap, Imagen.Clone() as Bitmap, Imagen.Clone() as Bitmap, RotateFlipType.Rotate90FlipY, RotateFlipType.RotateNoneFlipX, RotateFlipType.RotateNoneFlipX, RotateFlipType.RotateNoneFlipY, RotateFlipType.RotateNoneFlipY, RotateFlipType.Rotate90FlipX, true, false, false, false, false, true, 160, 0, 16, 16, 0, 160, Suavizado);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Changes the brightness of an image to the selected percentage.
        /// </summary>
        /// <param name="Imagen">Any valid image.</param>
        /// <param name="Brillo">Any value between -255 and 255 (both included).</param>
        /// <returns>Returns the original image modified. Returns null on any error.</returns>
        internal static Bitmap Obtener_Imagen_Brillo(Bitmap Imagen, int Brillo)
        {
            if (Imagen != null && Brillo != 0)
            {
                if (Brillo < -255) Brillo = -255;
                else if (Brillo > 255) Brillo = 255;
                int Ancho = Imagen.Width;
                int Alto = Imagen.Height;
                BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                int Bytes_Aumento = !Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 3 : 4;
                int Bytes_Diferencia = Ancho_Stride - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                byte[] Matriz_Bytes = new byte[Ancho_Stride * Alto];
                Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                {
                    for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                    {
                        int Rojo = Matriz_Bytes[Índice + 2] + Brillo;
                        int Verde = Matriz_Bytes[Índice + 1] + Brillo;
                        int Azul = Matriz_Bytes[Índice] + Brillo;
                        if (Rojo < 0) Rojo = 0;
                        else if (Rojo > 255) Rojo = 255;
                        if (Verde < 0) Verde = 0;
                        else if (Verde > 255) Verde = 255;
                        if (Azul < 0) Azul = 0;
                        else if (Azul > 255) Azul = 255;
                        Matriz_Bytes[Índice + 2] = (byte)Rojo;
                        Matriz_Bytes[Índice + 1] = (byte)Verde;
                        Matriz_Bytes[Índice] = (byte)Azul;
                    }
                }
                Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                Imagen.UnlockBits(Bitmap_Data);
                Matriz_Bytes = null;
            }
            return Imagen;
        }

        /// <summary>
        /// Changes the brightness of an image to the selected percentage.
        /// </summary>
        /// <param name="Imagen">Any valid image.</param>
        /// <param name="Intensidad">Any value between -255 and 255 (both included).</param>
        /// <returns>Returns the original image modified. Returns null on any error.</returns>
        internal static Bitmap Obtener_Imagen_Intensidad(Bitmap Imagen, int Intensidad)
        {
            if (Imagen != null && Intensidad != 255)
            {
                if (Intensidad < -255) Intensidad = -255;
                else if (Intensidad > 510) Intensidad = 510;
                if (Intensidad < 0) Intensidad = 255 + Math.Abs(Intensidad); // Double.
                int Ancho = Imagen.Width;
                int Alto = Imagen.Height;
                BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                int Bytes_Aumento = !Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 3 : 4;
                int Bytes_Diferencia = Ancho_Stride - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                byte[] Matriz_Bytes = new byte[Ancho_Stride * Alto];
                Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                {
                    for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                    {
                        int Rojo = (Matriz_Bytes[Índice + 2] * Intensidad) / 255;
                        int Verde = (Matriz_Bytes[Índice + 1] * Intensidad) / 255;
                        int Azul = (Matriz_Bytes[Índice] * Intensidad) / 255;
                        if (Rojo < 0) Rojo = 0;
                        else if (Rojo > 255) Rojo = 255;
                        if (Verde < 0) Verde = 0;
                        else if (Verde > 255) Verde = 255;
                        if (Azul < 0) Azul = 0;
                        else if (Azul > 255) Azul = 255;
                        Matriz_Bytes[Índice + 2] = (byte)Rojo;
                        Matriz_Bytes[Índice + 1] = (byte)Verde;
                        Matriz_Bytes[Índice] = (byte)Azul;
                    }
                }
                Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                Imagen.UnlockBits(Bitmap_Data);
                Matriz_Bytes = null;
            }
            return Imagen;
        }

        /// <summary>
        /// Replaces any non transparent ARGB color by the selected one on any valid image.
        /// </summary>
        /// <param name="Imagen">Any valid image.</param>
        /// <param name="Color_ARGB">The color to replace.</param>
        /// <returns>Returns the repainted image. Returns null on any error.</returns>
        internal static Bitmap Obtener_Imagen_Pintada(Bitmap Imagen, Color Color_ARGB)
        {
            if (Imagen != null && Image.IsAlphaPixelFormat(Imagen.PixelFormat))
            {
                int Ancho = Imagen.Width;
                int Alto = Imagen.Height;
                BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Imagen.Width, Imagen.Height), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                int Bytes_Aumento = !Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 3 : 4;
                int Bytes_Diferencia = Ancho_Stride - ((Imagen.Width * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                byte[] Matriz_Bytes = new byte[Ancho_Stride * Imagen.Height];
                Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                {
                    for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                    {
                        if (Matriz_Bytes[Índice + 3] >= 255) // Not a transparent pixel
                        {
                            Matriz_Bytes[Índice + 2] = Color_ARGB.R;
                            Matriz_Bytes[Índice + 1] = Color_ARGB.G;
                            Matriz_Bytes[Índice] = Color_ARGB.B;
                        }
                    }
                }
                Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                Imagen.UnlockBits(Bitmap_Data);
                Matriz_Bytes = null;
            }
            return Imagen;
        }

        /// <summary>
        /// Replaces the desired ARGB color by the selected one on any valid image.
        /// </summary>
        /// <param name="Imagen">Any valid image.</param>
        /// <param name="Color_ARGB_Origen">The color to find.</param>
        /// <param name="Color_ARGB">The color to replace.</param>
        /// <returns>Returns the repainted image. Returns null on any error.</returns>
        internal static Bitmap Obtener_Imagen_Pintada(Bitmap Imagen, Color Color_ARGB_Origen, Color Color_ARGB)
        {
            if (Imagen != null && Image.IsAlphaPixelFormat(Imagen.PixelFormat))
            {
                byte Alfa = Color_ARGB_Origen.A;
                byte Rojo = Color_ARGB_Origen.R;
                byte Verde = Color_ARGB_Origen.G;
                byte Azul = Color_ARGB_Origen.B;
                int Ancho = Imagen.Width;
                int Alto = Imagen.Height;
                BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Imagen.Width, Imagen.Height), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                int Bytes_Aumento = !Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 3 : 4;
                int Bytes_Diferencia = Ancho_Stride - ((Imagen.Width * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                byte[] Matriz_Bytes = new byte[Ancho_Stride * Imagen.Height];
                Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                {
                    for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                    {
                        if (Matriz_Bytes[Índice + 3] == Alfa && Matriz_Bytes[Índice + 2] == Rojo && Matriz_Bytes[Índice + 1] == Verde && Matriz_Bytes[Índice] == Azul) // It's the same color, so replace it.
                        {
                            Matriz_Bytes[Índice + 3] = Color_ARGB.A;
                            Matriz_Bytes[Índice + 2] = Color_ARGB.R;
                            Matriz_Bytes[Índice + 1] = Color_ARGB.G;
                            Matriz_Bytes[Índice] = Color_ARGB.B;
                        }
                    }
                }
                Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                Imagen.UnlockBits(Bitmap_Data);
                Matriz_Bytes = null;
            }
            return Imagen;
        }

        /// <summary>
        /// Obtains a texture from the application internal resources based on the specified name, which might start with "minecraft_" for a block texture.
        /// </summary>
        /// <param name="Nombre_Recurso">The name of an existing texture in the application internal resources.</param>
        /// <returns>Returns an image from the resources. Returns null if the texture doesn't exist and on any error.</returns>
        internal static Bitmap Obtener_Imagen_Recursos(string Nombre_Recurso)
        {
            try
            {
                Bitmap Imagen_Original = null;
                try { Imagen_Original = (Bitmap)Resources.ResourceManager.GetObject(Nombre_Recurso.Replace(' ', '_').Replace('~', '_').Replace('=', '_').Replace('+', '_').Replace('-', '_').Replace('\\', '_').Replace('/', '_').Replace(':', '_').Replace('*', '_').Replace('?', '_').Replace('\"', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_').Replace('.', '_')); }
                catch { Imagen_Original = null; }
                if (Imagen_Original != null)
                {
                    if (Imagen_Original.PixelFormat == PixelFormat.Format32bppArgb) return Imagen_Original;
                    else
                    {
                        int Ancho = Imagen_Original.Width;
                        int Alto = Imagen_Original.Height;
                        Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                        Graphics Pintar = Graphics.FromImage(Imagen);
                        Pintar.CompositingMode = CompositingMode.SourceCopy;
                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar.SmoothingMode = SmoothingMode.None;
                        Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                        Pintar.Dispose();
                        Pintar = null;
                        Imagen_Original.Dispose();
                        Imagen_Original = null;
                        return Imagen;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null/*Resources.Missing_Texture*/;
        }

        /// <summary>
        /// Loads a bitmap from the application external resources based on the specified file path. The file extension might be automatically added if the file is not found and one exists with a known image extension.
        /// </summary>
        /// <param name="Ruta">The name of an existing file, in the application external resources.</param>
        /// <returns>Returns an image loaded from the external resources. Returns null on any error.</returns>
        internal static Bitmap Obtener_Imagen_Recursos_Externos(string Ruta, CheckState Alfa)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta))
                {
                    if (!File.Exists(Ruta))
                    {
                        if (File.Exists(Ruta + ".png")) Ruta += ".png";
                        else if (File.Exists(Ruta + ".jpg")) Ruta += ".jpg";
                        else if (File.Exists(Ruta + ".jpeg")) Ruta += ".jpeg";
                        else if (File.Exists(Ruta + ".gif")) Ruta += ".gif";
                        else if (File.Exists(Ruta + ".bmp")) Ruta += ".bmp";
                        else if (File.Exists(Ruta + ".tif")) Ruta += ".tif";
                        else if (File.Exists(Ruta + ".tiff")) Ruta += ".tiff";
                        else if (File.Exists(Ruta + ".emf")) Ruta += ".emf";
                        else if (File.Exists(Ruta + ".wmf")) Ruta += ".wmf";
                    }
                    if (File.Exists(Ruta))
                    {
                        FileStream Lector = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        if (Lector != null && Lector.Length > 0L)
                        {
                            Lector.Seek(0L, SeekOrigin.Begin);
                            Image Imagen_Original = null;
                            try { Imagen_Original = Image.FromStream(Lector, false, false); }
                            catch { Imagen_Original = null; }
                            if (Imagen_Original != null)
                            {
                                int Ancho = Imagen_Original.Width;
                                int Alto = Imagen_Original.Height;
                                Bitmap Imagen = new Bitmap(Ancho, Alto, Alfa == CheckState.Indeterminate ? (Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb) : Alfa == CheckState.Checked ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb);
                                Graphics Pintar = Graphics.FromImage(Imagen);
                                Pintar.CompositingMode = CompositingMode.SourceCopy;
                                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                Pintar.SmoothingMode = SmoothingMode.HighQuality;
                                Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                                Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                Pintar.Dispose();
                                Pintar = null;
                                Imagen_Original.Dispose();
                                Imagen_Original = null;
                                Lector.Close();
                                Lector.Dispose();
                                Lector = null;
                                return Imagen;
                            }
                            Lector.Close();
                            Lector.Dispose();
                            Lector = null;
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null/*Resources.Missing_Texture*/;
        }

        /// <summary>
        /// Loads an image into memory from any valid existing file.
        /// </summary>
        /// <param name="Ruta">Any valid existing filepath.</param>
        /// <returns>Returns the loaded image converted to a valid format. Returns null on any error.</returns>
        internal static Bitmap Obtener_Imagen_Ruta(string Ruta)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                {
                    FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                    if (Lector != null && Lector.Length > 0L)
                    {
                        Image Imagen_Original = null;
                        try { Imagen_Original = Image.FromStream(Lector, false, false); }
                        catch { Imagen_Original = null; }
                        if (Imagen_Original != null)
                        {
                            int Ancho = Imagen_Original.Width;
                            int Alto = Imagen_Original.Height;
                            Bitmap Imagen = new Bitmap(Ancho, Alto, !Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? PixelFormat.Format24bppRgb : PixelFormat.Format32bppArgb);
                            Graphics Pintar = Graphics.FromImage(Imagen);
                            Pintar.CompositingMode = CompositingMode.SourceCopy;
                            Pintar.CompositingQuality = CompositingQuality.HighQuality;
                            Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Pintar.SmoothingMode = SmoothingMode.HighQuality;
                            Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                            Pintar.Dispose();
                            Pintar = null;
                            Imagen_Original.Dispose();
                            Imagen_Original = null;
                            Lector.Close();
                            Lector.Dispose();
                            Lector = null;
                            return Imagen;
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
        /// Draws a full Minecraft skin viewed from the front. Note that the hair pixels needs to be bigger than the rest, so to draw it properly the skin needs to have a minimum zoom of 4. The skin images without zoom should have about 16 x 32 pixels.
        /// </summary>
        /// <param name="Imagen_Original">Any valid skin image of 32 x 32 or 64 x 64 pixels.</param>
        /// <param name="Dibujar_Pelo">If true, the hair will be drawn.</param>
        /// <param name="Dibujar_Chaqueta">If true, the jacket will be drawn.</param>
        /// <param name="Dibujar_Brazos_Chaqueta">If true, the jacket's arms will be drawn.</param>
        /// <param name="Dibujar_Pantalones">If true, the pants will be drawn.</param>
        /// <returns>Returns a new image containing the skin viewed from the front with a zoom of 4x. Returns null on any error.</returns>
        internal static Bitmap Obtener_Imagen_Skin_2D(Bitmap Imagen_Original, bool Dibujar_Pelo, bool Dibujar_Chaqueta, bool Dibujar_Brazos_Chaqueta, bool Dibujar_Pantalones)
        {
            try
            {
                if (Imagen_Original != null)
                {
                    double Multiplicador = 7.5d; // Tweaked by hand, might not be fully accurate.
                    double Multiplicador_Pelo = 9.25d; // Tweaked by hand, might not be fully accurate.
                    double Diferencia_Pelo = (8d * (Multiplicador_Pelo - Multiplicador)) / 2d; // Tweaked by hand, might not be fully accurate.
                    double Diferencia_Pelo_Superior = (8d * (Multiplicador_Pelo - Multiplicador)) / 1.75d; // Tweaked by hand, might not be fully accurate.
                    Bitmap Imagen = new Bitmap((int)Math.Round(16d * Multiplicador, MidpointRounding.AwayFromZero), (int)Math.Round((32d * Multiplicador) + Diferencia_Pelo_Superior, MidpointRounding.AwayFromZero), PixelFormat.Format32bppArgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.HighQuality;
                    if (Imagen_Original.Height >= 64)
                    {
                        Pintar.DrawImage(Imagen_Original, new RectangleF((float)(4d * Multiplicador), (float)Diferencia_Pelo_Superior, (float)(8d * Multiplicador), (float)(8d * Multiplicador)), new RectangleF(8f, 8f, 8f, 8f), GraphicsUnit.Pixel); // Head
                        Pintar.DrawImage(Imagen_Original, new RectangleF((float)(4d * Multiplicador), (float)((8d * Multiplicador) + Diferencia_Pelo_Superior), (float)(8d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(20f, 20f, 8f, 12f), GraphicsUnit.Pixel); // Body
                        Pintar.DrawImage(Imagen_Original, new RectangleF(0f, (float)((8d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(36f, 52f, 4f, 12f), GraphicsUnit.Pixel); // Left arm
                        Pintar.DrawImage(Imagen_Original, new RectangleF((float)(12d * Multiplicador), (float)((8d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(44f, 20f, 4f, 12f), GraphicsUnit.Pixel); // Right arm
                        Pintar.DrawImage(Imagen_Original, new RectangleF((float)(4d * Multiplicador), (float)((20d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(20f, 52f, 4f, 12f), GraphicsUnit.Pixel); // Left leg
                        Pintar.DrawImage(Imagen_Original, new RectangleF((float)(8d * Multiplicador), (float)((20d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(4f, 20f, 4f, 12f), GraphicsUnit.Pixel); // Right leg

                        Pintar.CompositingMode = CompositingMode.SourceOver; // Overlay
                        if (Dibujar_Pelo) Pintar.DrawImage(Imagen_Original, new RectangleF((float)((4d * Multiplicador) - Diferencia_Pelo), 0f, (float)(8d * Multiplicador_Pelo), (float)(8d * Multiplicador_Pelo)), new RectangleF(40f, 8f, 8f, 8f), GraphicsUnit.Pixel); // Hair
                        if (Dibujar_Chaqueta) Pintar.DrawImage(Imagen_Original, new RectangleF((float)(4d * Multiplicador), (float)((8d * Multiplicador) + Diferencia_Pelo_Superior), (float)(8d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(20f, 36f, 8f, 12f), GraphicsUnit.Pixel); // Jacket
                        if (Dibujar_Brazos_Chaqueta)
                        {
                            Pintar.DrawImage(Imagen_Original, new RectangleF(0f, (float)((8 * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(52f, 52f, 4f, 12f), GraphicsUnit.Pixel); // Left jacket arm
                            Pintar.DrawImage(Imagen_Original, new RectangleF((float)(12d * Multiplicador), (float)((8d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(44f, 36f, 4f, 12f), GraphicsUnit.Pixel); // Right jacket arm
                        }
                        if (Dibujar_Pantalones)
                        {
                            Pintar.DrawImage(Imagen_Original, new RectangleF((float)(4d * Multiplicador), (float)((20d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(4f, 52f, 4f, 12f), GraphicsUnit.Pixel); // Left pants leg
                            Pintar.DrawImage(Imagen_Original, new RectangleF((float)(8d * Multiplicador), (float)((20d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(4f, 36f, 4f, 12f), GraphicsUnit.Pixel); // Right pants leg
                        }
                    }
                    else // 32
                    {
                        Pintar.DrawImage(Imagen_Original, new RectangleF((float)(4d * Multiplicador), (float)Diferencia_Pelo_Superior, (float)(8d * Multiplicador), (float)(8d * Multiplicador)), new RectangleF(8f, 8f, 8f, 8f), GraphicsUnit.Pixel); // Head
                        Pintar.DrawImage(Imagen_Original, new RectangleF((float)(4d * Multiplicador), (float)((8d * Multiplicador) + Diferencia_Pelo_Superior), (float)(8d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(20f, 20f, 8f, 12f), GraphicsUnit.Pixel); // Body
                        Pintar.DrawImage(Imagen_Original, new RectangleF(0f, (float)((8d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(44f, 20f, 4f, 12f), GraphicsUnit.Pixel); // Left arm
                        Pintar.DrawImage(Imagen_Original, new RectangleF((float)(4d * Multiplicador), (float)((20d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(4f, 20f, 4f, 12f), GraphicsUnit.Pixel); // Left leg
                        Imagen_Original.RotateFlip(RotateFlipType.RotateNoneFlipX); // Flip the image.
                        Pintar.DrawImage(Imagen_Original, new RectangleF((float)(12d * Multiplicador), (float)((8d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(16f, 20f, 4f, 12f), GraphicsUnit.Pixel); // Right arm
                        Pintar.DrawImage(Imagen_Original, new RectangleF((float)(8d * Multiplicador), (float)((20d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(56f, 20f, 4f, 12f), GraphicsUnit.Pixel); // Right leg
                        Imagen_Original.RotateFlip(RotateFlipType.RotateNoneFlipX); // Restore the image.

                        Pintar.CompositingMode = CompositingMode.SourceOver; // Overlay
                        if (Dibujar_Pelo) Pintar.DrawImage(Imagen_Original, new RectangleF((float)((4d * Multiplicador) - Diferencia_Pelo), 0f, (float)(8d * Multiplicador_Pelo), (float)(8d * Multiplicador_Pelo)), new RectangleF(40f, 8f, 8f, 8f), GraphicsUnit.Pixel); // Hair
                    }
                    Pintar.Dispose();
                    Pintar = null;
                    return Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Draws a full Minecraft skin viewed from the front. Note that the hair pixels needs to be bigger than the rest, so to draw it properly the skin needs to have a minimum zoom of 4. The skin images without zoom should have 16 x 32 pixels.
        /// </summary>
        /// <param name="Imagen_Original">Any valid skin image of 32 x 32 or 64 x 64 pixels.</param>
        /// <param name="Dibujar_Pelo">If true, the hair will be drawn.</param>
        /// <param name="Dibujar_Chaqueta">If true, the jacket will be drawn.</param>
        /// <param name="Dibujar_Brazos_Chaqueta">If true, the jacket's arms will be drawn.</param>
        /// <param name="Dibujar_Pantalones">If true, the pants will be drawn.</param>
        /// <returns>Returns a new image containing the skin viewed from the front with a zoom of 4x. Returns null on any error.</returns>
        internal static Bitmap Obtener_Imagen_Skin_2D_Dual(Bitmap Imagen_Original, bool Dibujar_Pelo, bool Dibujar_Chaqueta, bool Dibujar_Brazos_Chaqueta, bool Dibujar_Pantalones)
        {
            try // Note: this function is unfinished, use the above one...
            {
                if (Imagen_Original != null)
                {
                    double Multiplicador = 7.5d; // Tweaked by hand, might not be fully accurate.
                    double Multiplicador_Pelo = 9.25d; // Tweaked by hand, might not be fully accurate.
                    double Diferencia_Pelo = (8d * (Multiplicador_Pelo - Multiplicador)) / 2d; // Tweaked by hand, might not be fully accurate.
                    double Diferencia_Pelo_Superior = (8d * (Multiplicador_Pelo - Multiplicador)) / 1.75d; // Tweaked by hand, might not be fully accurate.
                    Bitmap Imagen = new Bitmap((int)Math.Round(16d * Multiplicador, MidpointRounding.AwayFromZero), (int)Math.Round((32d * Multiplicador) + Diferencia_Pelo_Superior, MidpointRounding.AwayFromZero), PixelFormat.Format32bppArgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.HighQuality;
                    if (Imagen_Original.Height >= 64)
                    {
                        Pintar.DrawImage(Imagen_Original, new RectangleF((float)(4d * Multiplicador), (float)Diferencia_Pelo_Superior, (float)(8d * Multiplicador), (float)(8d * Multiplicador)), new RectangleF(8f, 8f, 8f, 8f), GraphicsUnit.Pixel); // Head
                        Pintar.DrawImage(Imagen_Original, new RectangleF((float)(4d * Multiplicador), (float)((8d * Multiplicador) + Diferencia_Pelo_Superior), (float)(8d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(20f, 20f, 8f, 12f), GraphicsUnit.Pixel); // Body
                        Pintar.DrawImage(Imagen_Original, new RectangleF(0f, (float)((8d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(36f, 52f, 4f, 12f), GraphicsUnit.Pixel); // Left arm
                        Pintar.DrawImage(Imagen_Original, new RectangleF((float)(12d * Multiplicador), (float)((8d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(44f, 20f, 4f, 12f), GraphicsUnit.Pixel); // Right arm
                        Pintar.DrawImage(Imagen_Original, new RectangleF((float)(4d * Multiplicador), (float)((20d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(20f, 52f, 4f, 12f), GraphicsUnit.Pixel); // Left leg
                        Pintar.DrawImage(Imagen_Original, new RectangleF((float)(8d * Multiplicador), (float)((20d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(4f, 20f, 4f, 12f), GraphicsUnit.Pixel); // Right leg

                        Pintar.CompositingMode = CompositingMode.SourceOver; // Overlay
                        if (Dibujar_Pelo) Pintar.DrawImage(Imagen_Original, new RectangleF((float)((4d * Multiplicador) - Diferencia_Pelo), 0f, (float)(8d * Multiplicador_Pelo), (float)(8d * Multiplicador_Pelo)), new RectangleF(40f, 8f, 8f, 8f), GraphicsUnit.Pixel); // Hair
                        if (Dibujar_Chaqueta) Pintar.DrawImage(Imagen_Original, new RectangleF((float)(4d * Multiplicador), (float)((8d * Multiplicador) + Diferencia_Pelo_Superior), (float)(8d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(20f, 36f, 8f, 12f), GraphicsUnit.Pixel); // Jacket
                        if (Dibujar_Brazos_Chaqueta)
                        {
                            Pintar.DrawImage(Imagen_Original, new RectangleF(0f, (float)((8 * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(52f, 52f, 4f, 12f), GraphicsUnit.Pixel); // Left jacket arm
                            Pintar.DrawImage(Imagen_Original, new RectangleF((float)(12d * Multiplicador), (float)((8d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(44f, 36f, 4f, 12f), GraphicsUnit.Pixel); // Right jacket arm
                        }
                        if (Dibujar_Pantalones)
                        {
                            Pintar.DrawImage(Imagen_Original, new RectangleF((float)(4d * Multiplicador), (float)((20d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(4f, 52f, 4f, 12f), GraphicsUnit.Pixel); // Left pants leg
                            Pintar.DrawImage(Imagen_Original, new RectangleF((float)(8d * Multiplicador), (float)((20d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(4f, 36f, 4f, 12f), GraphicsUnit.Pixel); // Right pants leg
                        }
                    }
                    else // 32
                    {
                        Pintar.DrawImage(Imagen_Original, new RectangleF((float)(4d * Multiplicador), (float)Diferencia_Pelo_Superior, (float)(8d * Multiplicador), (float)(8d * Multiplicador)), new RectangleF(8f, 8f, 8f, 8f), GraphicsUnit.Pixel); // Head
                        Pintar.DrawImage(Imagen_Original, new RectangleF((float)(4d * Multiplicador), (float)((8d * Multiplicador) + Diferencia_Pelo_Superior), (float)(8d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(20f, 20f, 8f, 12f), GraphicsUnit.Pixel); // Body
                        Pintar.DrawImage(Imagen_Original, new RectangleF(0f, (float)((8d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(44f, 20f, 4f, 12f), GraphicsUnit.Pixel); // Left arm
                        Pintar.DrawImage(Imagen_Original, new RectangleF((float)(4d * Multiplicador), (float)((20d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(4f, 20f, 4f, 12f), GraphicsUnit.Pixel); // Left leg
                        Imagen_Original.RotateFlip(RotateFlipType.RotateNoneFlipX); // Flip the image.
                        Pintar.DrawImage(Imagen_Original, new RectangleF((float)(12d * Multiplicador), (float)((8d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(16f, 20f, 4f, 12f), GraphicsUnit.Pixel); // Right arm
                        Pintar.DrawImage(Imagen_Original, new RectangleF((float)(8d * Multiplicador), (float)((20d * Multiplicador) + Diferencia_Pelo_Superior), (float)(4d * Multiplicador), (float)(12d * Multiplicador)), new RectangleF(56f, 20f, 4f, 12f), GraphicsUnit.Pixel); // Right leg
                        Imagen_Original.RotateFlip(RotateFlipType.RotateNoneFlipX); // Restore the image.

                        Pintar.CompositingMode = CompositingMode.SourceOver; // Overlay
                        if (Dibujar_Pelo) Pintar.DrawImage(Imagen_Original, new RectangleF((float)((4d * Multiplicador) - Diferencia_Pelo), 0f, (float)(8d * Multiplicador_Pelo), (float)(8d * Multiplicador_Pelo)), new RectangleF(40f, 8f, 8f, 8f), GraphicsUnit.Pixel); // Hair
                    }
                    Pintar.Dispose();
                    Pintar = null;
                    return Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static Bitmap Obtener_Imagen_Texto(string Texto, Font Fuente, Color Color_Fondo, Color Color_Fuente, TextRenderingHint Renderizado)
        {
            try
            {
                if (!string.IsNullOrEmpty(Texto) && Fuente != null)
                {
                    Bitmap Imagen = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    SizeF Dimensiones = Pintar.MeasureString(Texto, Fuente);
                    Pintar.Dispose();
                    Pintar = null;
                    Imagen.Dispose();
                    Imagen = null;
                    if (Dimensiones.Width > 0 && Dimensiones.Height > 0)
                    {
                        Imagen = new Bitmap((int)Dimensiones.Width + 8, (int)Dimensiones.Height + 8, PixelFormat.Format32bppArgb);
                        Pintar = Graphics.FromImage(Imagen);
                        if (Color_Fondo != Color.Empty && Color_Fondo != Color.Transparent) Pintar.Clear(Color_Fondo);
                        Pintar.CompositingMode = CompositingMode.SourceOver;
                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Pintar.PageUnit = GraphicsUnit.Pixel;
                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar.SmoothingMode = SmoothingMode.HighQuality;
                        Pintar.TextRenderingHint = Renderizado; // Use "AntiAlias" to Avoid Windows 8.1 bad font drawing.
                        SolidBrush Pincel = new SolidBrush(Color_Fuente);
                        Pintar.DrawString(Texto, Fuente, Pincel, 4f, 4f);
                        Pincel.Dispose();
                        Pincel = null;
                        Pintar.Dispose();
                        Pintar = null;
                        return Imagen;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static string Traducir_Lista_Variables(List<string> Lista_Líneas)
        {
            try
            {
                if (Lista_Líneas != null && Lista_Líneas.Count > 0)
                {
                    string Texto = null;
                    foreach (string Línea in Lista_Líneas)
                    {
                        try { Texto += Línea + ", "; }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    if (!string.IsNullOrEmpty(Texto)) return Texto.TrimEnd(", ".ToCharArray());
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static string Traducir_Lista_Variables(string[] Matriz_Líneas)
        {
            try
            {
                if (Matriz_Líneas != null && Matriz_Líneas.Length > 0)
                {
                    string Texto = null;
                    foreach (string Línea in Matriz_Líneas)
                    {
                        try { Texto += Línea + ", "; }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    if (!string.IsNullOrEmpty(Texto)) return Texto.TrimEnd(", ".ToCharArray());
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static string Traducir_Lista_Variables(List<object> Lista_Objetos)
        {
            try
            {
                if (Lista_Objetos != null && Lista_Objetos.Count > 0)
                {
                    string Texto = null;
                    foreach (object Objeto in Lista_Objetos)
                    {
                        try { Texto += Objeto.ToString() + ", "; }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    if (!string.IsNullOrEmpty(Texto)) return Texto.TrimEnd(", ".ToCharArray());
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static string Traducir_Lista_Variables(object[] Matriz_Objetos)
        {
            try
            {
                if (Matriz_Objetos != null && Matriz_Objetos.Length > 0)
                {
                    string Texto = null;
                    foreach (object Objeto in Matriz_Objetos)
                    {
                        try { Texto += Objeto.ToString() + ", "; }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    if (!string.IsNullOrEmpty(Texto)) return Texto.TrimEnd(", ".ToCharArray());
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static string Traducir_Lista_Variables(List<int> Lista_Objetos)
        {
            try
            {
                if (Lista_Objetos != null && Lista_Objetos.Count > 0)
                {
                    string Texto = null;
                    foreach (int Valor in Lista_Objetos)
                    {
                        try { Texto += Valor.ToString() + ", "; }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    if (!string.IsNullOrEmpty(Texto)) return Texto.TrimEnd(", ".ToCharArray());
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static string Traducir_Lista_Variables(int[] Matriz_Objetos)
        {
            try
            {
                if (Matriz_Objetos != null && Matriz_Objetos.Length > 0)
                {
                    string Texto = null;
                    foreach (int Valor in Matriz_Objetos)
                    {
                        try { Texto += Valor.ToString() + ", "; }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    if (!string.IsNullOrEmpty(Texto)) return Texto.TrimEnd(", ".ToCharArray());
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static string Traducir_Lista_Variables(List<byte> Lista_Objetos)
        {
            try
            {
                if (Lista_Objetos != null && Lista_Objetos.Count > 0)
                {
                    string Texto = null;
                    foreach (byte Valor in Lista_Objetos)
                    {
                        try { Texto += Valor.ToString() + ", "; }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    if (!string.IsNullOrEmpty(Texto)) return Texto.TrimEnd(", ".ToCharArray());
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static string Traducir_Lista_Variables(byte[] Matriz_Objetos)
        {
            try
            {
                if (Matriz_Objetos != null && Matriz_Objetos.Length > 0)
                {
                    string Texto = null;
                    foreach (byte Valor in Matriz_Objetos)
                    {
                        try { Texto += Valor.ToString() + ", "; }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    if (!string.IsNullOrEmpty(Texto)) return Texto.TrimEnd(", ".ToCharArray());
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Reads all the bytes of any file at once and returns them in a new byte array.
        /// </summary>
        /// <param name="Ruta">Any valid and existing file path.</param>
        /// <returns>Returns all the bytes of a file in a byte array. Returns null on any error.</returns>
        internal static byte[] Obtener_Matriz_Bytes_Archivo(string Ruta)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                {
                    FileStream Lector = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    if (Lector.Length > 0L) // Overflow warning: it's not checking for too big files.
                    {
                        Lector.Seek(0L, SeekOrigin.Begin);
                        byte[] Matriz_Bytes = new byte[Lector.Length];
                        int Longitud = Lector.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);
                        if (Longitud > -1)
                        {
                            if (Matriz_Bytes.Length != Longitud) Array.Resize(ref Matriz_Bytes, Longitud);
                        }
                        else Matriz_Bytes = null;
                        Lector.Close();
                        Lector.Dispose();
                        Lector = null;
                        return Matriz_Bytes;
                    }
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Reads all the text lines from any file and returns them as a new list.
        /// </summary>
        /// <param name="Ruta">Any valid file path.</param>
        /// <returns>Returns all the text lines of a file in a list. Returns null on any error.</returns>
        internal static List<string> Obtener_Lista_Líneas_Archivo(string Ruta/*, Encoding Codificación*/)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                {
                    FileStream Lector = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    if (Lector.Length > 0L)
                    {
                        Lector.Seek(0L, SeekOrigin.Begin);
                        StreamReader Lector_Texto = new StreamReader(Lector/*, Codificación*/);
                        List<string> Lista_Líneas = new List<string>();
                        while (!Lector_Texto.EndOfStream)
                        {
                            //string Línea = Lector_Texto.ReadLine();
                            Lista_Líneas.Add(Lector_Texto.ReadLine());
                        }
                        Lector_Texto.Close();
                        Lector_Texto.Dispose();
                        Lector_Texto = null;
                        Lector.Close();
                        Lector.Dispose();
                        Lector = null;
                        return Lista_Líneas;
                    }
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static byte[] Obtener_Matriz_Bytes_Imagen(Bitmap Imagen)
        {
            try
            {
                int Ancho = Imagen.Width;
                int Alto = Imagen.Height;
                BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                Imagen.UnlockBits(Bitmap_Data);
                Bitmap_Data = null;
                return Matriz_Bytes;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static Bitmap Obtener_Imagen_Sobre_Fondo(Bitmap Imagen_Original, Color Color_ARGB)
        {
            try
            {
                if (Imagen_Original != null && Color_ARGB != Color.Empty)
                {
                    int Ancho = Imagen_Original.Width;
                    int Alto = Imagen_Original.Height;
                    Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.Clear(Color_ARGB);
                    Pintar.CompositingMode = CompositingMode.SourceOver;
                    Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                    Pintar.Dispose();
                    Pintar = null;
                    return Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Obtains a rectangle with cut positions from any image trying to exclude from it the background color specified. Note: use "Color.Empty" instead of "Color.Transparent" or it will fail.
        /// </summary>
        /// <param name="Imagen">Any valid image. It should have alpha.</param>
        /// <param name="Color_Fondo">The background color to exclude. Note: use "Color.Empty" instead of "Color.Transparent" or it will fail.</param>
        /// <returns>Returns a rectangle with cut positions for the image, but check if it's out of bounds, which will mean the image needs no changes. Returns null on any error.</returns>
        internal static Rectangle Buscar_Zona_Recorte_Imagen(Bitmap Imagen, Color Color_Fondo)
        {
            if (Imagen != null)
            {
                int Ancho = Imagen.Width;
                int Alto = Imagen.Height;
                int Rectángulo_X = int.MaxValue;
                int Rectángulo_Y = int.MaxValue;
                int Rectángulo_Ancho = int.MinValue;
                int Rectángulo_Alto = int.MinValue;
                BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Imagen.Width, Imagen.Height), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                int Bytes_Aumento = !Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 3 : 4;
                int Bytes_Diferencia = Ancho_Stride - ((Imagen.Width * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                byte[] Matriz_Bytes = new byte[Ancho_Stride * Imagen.Height];
                Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                Imagen.UnlockBits(Bitmap_Data);
                for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                {
                    for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                    {
                        if (((Color_Fondo == Color.Empty ||
                            Color_Fondo == Color.Transparent) &&
                            Matriz_Bytes[Índice + 3] > 0) ||
                            (Color_Fondo != Color.Empty &&
                            Color_Fondo != Color.Transparent &&
                            (Matriz_Bytes[Índice + 3] != Color_Fondo.A ||
                            Matriz_Bytes[Índice + 2] != Color_Fondo.R ||
                            Matriz_Bytes[Índice + 1] != Color_Fondo.G ||
                            Matriz_Bytes[Índice] != Color_Fondo.B)))
                        {
                            if (X < Rectángulo_X) Rectángulo_X = X;
                            if (X + 1 > Rectángulo_Ancho) Rectángulo_Ancho = X + 1;
                            if (Y < Rectángulo_Y) Rectángulo_Y = Y;
                            if (Y + 1 > Rectángulo_Alto) Rectángulo_Alto = Y + 1;
                        }
                    }
                }
                Matriz_Bytes = null;
                //Rectangle Rectángulo = Rectangle.FromLTRB(Rectángulo_X, Rectángulo_Y, Rectángulo_Ancho, Rectángulo_Alto);
                //if (Rectángulo.Width <= 0 || Rectángulo.Height <= 0) Rectángulo = new Rectangle(0, 0, Ancho, Alto);
                return Rectangle.FromLTRB(Rectángulo_X, Rectángulo_Y, Rectángulo_Ancho, Rectángulo_Alto);
            }
            return Rectangle.Empty;
        }

        /// <summary>
        /// java.util.Random.
        /// </summary>
        [Serializable]
        public class Random_Java
        {
            public Random_Java(ulong Semilla)
            {
                this.Semilla = (Semilla ^ 0x5DEECE66DUL) & ((1UL << 48) - 1);
            }

            public int nextInt(int Número)
            {
                if (Número <= 0) throw new ArgumentException("The supplied number must be positive.");

                if ((Número & -Número) == Número)  // i.e., n is a power of 2
                    return (int)((Número * (long)Next(31)) >> 31);

                long bits, val;
                do
                {
                    bits = Next(31);
                    val = bits % (uint)Número;
                }
                while (bits - val + (Número - 1) < 0);

                return (int)val;
            }

            protected uint Next(int Bits)
            {
                Semilla = (Semilla * 0x5DEECE66DL + 0xBL) & ((1L << 48) - 1);

                return (uint)(Semilla >> (48 - Bits));
            }

            private ulong Semilla;
        }

        internal static readonly Color[] Matriz_12_Colores = new Color[12]
        {
            Color.FromArgb(255, 255, 0, 0), // Red.
            Color.FromArgb(255, 255, 160, 0), // Orange.
            Color.FromArgb(255, 255, 255, 0), // Yellow.
            Color.FromArgb(255, 160, 255, 0), // Lime.
            Color.FromArgb(255, 0, 255, 0), // Green.
            Color.FromArgb(255, 0, 255, 160), // Turquoise.
            Color.FromArgb(255, 0, 255, 255), // Cyan.
            Color.FromArgb(255, 0, 160, 255), // Light blue.
            Color.FromArgb(255, 0, 0, 255), // Blue.
            Color.FromArgb(255, 160, 0, 255), // Purple.
            Color.FromArgb(255, 255, 0, 255), // Magenta.
            Color.FromArgb(255, 255, 0, 160), // Pink.
        };
        internal static readonly Color[] Matriz_8_Colores = new Color[8]
        {
            Color.FromArgb(255, 0, 0, 0), // Black.
            Color.FromArgb(255, 255, 0, 0), // Red.
            Color.FromArgb(255, 255, 255, 0), // Yellow.
            Color.FromArgb(255, 0, 255, 0), // Green.
            Color.FromArgb(255, 0, 255, 255), // Cyan.
            Color.FromArgb(255, 0, 0, 255), // Blue.
            Color.FromArgb(255, 255, 0, 255), // Magenta.
            Color.FromArgb(255, 255, 255, 255) // White.
        };
        internal static readonly Color[] Matriz_Colores_12_Notas = new Color[12] { Color.FromArgb(255, 0, 0), Color.FromArgb(255, 144, 0), Color.FromArgb(255, 176, 0), Color.FromArgb(255, 216, 0), Color.FromArgb(255, 255, 0), Color.FromArgb(0, 255, 0), Color.FromArgb(0, 255, 192), Color.FromArgb(0, 96, 255), Color.FromArgb(80, 0, 255), Color.FromArgb(128, 0, 255), Color.FromArgb(160, 0, 255), Color.FromArgb(255, 0, 176) };
        internal static readonly Pen[] Matriz_Lápices_12_Notas = new Pen[12] { new Pen(Color.FromArgb(255, 0, 0)), new Pen(Color.FromArgb(255, 144, 0)), new Pen(Color.FromArgb(255, 176, 0)), new Pen(Color.FromArgb(255, 216, 0)), new Pen(Color.FromArgb(255, 255, 0)), new Pen(Color.FromArgb(0, 255, 0)), new Pen(Color.FromArgb(0, 255, 192)), new Pen(Color.FromArgb(0, 96, 255)), new Pen(Color.FromArgb(80, 0, 255)), new Pen(Color.FromArgb(128, 0, 255)), new Pen(Color.FromArgb(160, 0, 255)), new Pen(Color.FromArgb(255, 0, 176)) };
        internal static readonly SolidBrush[] Matriz_Pinceles_12_Notas = new SolidBrush[12] { new SolidBrush(Color.FromArgb(255, 0, 0)), new SolidBrush(Color.FromArgb(255, 144, 0)), new SolidBrush(Color.FromArgb(255, 176, 0)), new SolidBrush(Color.FromArgb(255, 216, 0)), new SolidBrush(Color.FromArgb(255, 255, 0)), new SolidBrush(Color.FromArgb(0, 255, 0)), new SolidBrush(Color.FromArgb(0, 255, 192)), new SolidBrush(Color.FromArgb(0, 96, 255)), new SolidBrush(Color.FromArgb(80, 0, 255)), new SolidBrush(Color.FromArgb(128, 0, 255)), new SolidBrush(Color.FromArgb(160, 0, 255)), new SolidBrush(Color.FromArgb(255, 0, 176)) };

        internal static Color[] Matriz_Colores_Arco_Iris_8_Números_Primos = new Color[8]
        {
            Color.FromArgb(255, 255, 255, 0), // Unused?
            Color.FromArgb(255, 255, 0, 0), // Very used.
            Color.FromArgb(255, 255, 0, 255), // Only one?
            Color.FromArgb(255, 0, 255, 0), // Very used.
            Color.FromArgb(255, 255, 192, 0), // Unused?
            Color.FromArgb(255, 0, 255, 255), // Very used.
            Color.FromArgb(255, 192, 0, 255), // Unused?
            Color.FromArgb(255, 0, 0, 255) // Very used.
            /*Color.FromArgb(255, 255, 0, 0),
            Color.FromArgb(255, 255, 192, 0),
            Color.FromArgb(255, 255, 255, 0),
            Color.FromArgb(255, 0, 255, 0),
            Color.FromArgb(255, 0, 255, 192),
            Color.FromArgb(255, 0, 255, 255),
            Color.FromArgb(255, 0, 0, 255),
            Color.FromArgb(255, 255, 0, 255)*/
        };
        internal static Color[] Matriz_Colores_Arco_Iris_16 = null;
        //internal static Pen[] Matriz_Lápices_Arco_Iris_256 = null;
        internal static Color[] Matriz_Colores_Arco_Iris_256 = null;
        internal static Color[] Matriz_Colores_Grises_256 = null;
        internal static Color[] Matriz_Colores_Termografía_256 = null;
        internal static Pen[] Matriz_Lápices_Arco_Iris_256 = null;
        internal static Pen[] Matriz_Lápices_Grises_256 = null;
        internal static Pen[] Matriz_Lápices_Termografía_256 = null;
        internal static SolidBrush[] Matriz_Pinceles_Arco_Iris_256 = null;
        internal static SolidBrush[] Matriz_Pinceles_Grises_256 = null;
        internal static SolidBrush[] Matriz_Pinceles_Termografía_256 = null;
        internal static Process Proceso = Process.GetCurrentProcess();
        internal static PerformanceCounter Rendimiento_Procesador = null;

        /// <summary>
        /// The main entry point for the "Minecraft Tools" application.
        /// </summary>
        [STAThread]
        static void Main(string[] Matriz_Argumentos)
        {
            //try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (Matriz_Argumentos != null && Matriz_Argumentos.Length > 0)
                {
                    string Argumento = Matriz_Argumentos[0];
                    if (!string.IsNullOrEmpty(Argumento))
                    {
                        Argumento = Argumento.ToLowerInvariant();
                        if (Argumento.Contains("/c")) // Configure
                        {
                            Ventana_Salvapantallas_Bloques.Argumento_Salvapantallas = CheckState.Indeterminate;
                            Application.Run(new Ventana_Salvapantallas_Bloques());
                            return;
                        }
                        else if (Argumento.Contains("/p")) // Preview
                        {
                            //Ventana_Salvapantallas.Argumento_Salvapantallas = true;
                            //Application.Run(new Ventana_Salvapantallas());
                            return;
                        }
                        else// if (Argumento.Contains("s")) // Screensaver
                        {
                            Ventana_Salvapantallas_Bloques.Argumento_Salvapantallas = CheckState.Checked;
                            Application.Run(new Ventana_Salvapantallas_Bloques());
                            return;
                        }
                    }
                    /*else
                    {
                        Ventana_Salvapantallas.Argumento_Salvapantallas = CheckState.Checked;
                        Application.Run(new Ventana_Salvapantallas());
                        return;
                    }*/
                }
                Depurador.Iniciar_Depurador();
                Minecraft_Splashes.Lista_Líneas.Insert(0, "Now with " + Program.Traducir_Número(Minecraft_Splashes.Lista_Líneas.Count) + " splashes!"); // Add an extra splash that tells how many there are.
                //Copias_Seguridad.Iniciar_Copias_Seguridad(); // Not used yet.
                Lista_Caracteres_Prohibidos.AddRange(Path.GetInvalidPathChars());
                Lista_Caracteres_Prohibidos.AddRange(Path.GetInvalidFileNameChars());
                try { Rendimiento_Procesador = new PerformanceCounter("Processor", "% Processor Time", "_Total", true); }
                catch { Rendimiento_Procesador = null; }
                //try
                {
                    Minecraft.Cargar_Biomas();
                    Minecraft.Bloques.Reiniciar_Diccionarios_Bloques();
                    //Minecraft.Reiniciar_Diccionario_Nombres_Bloques();
                    Ventana_Generador_Pixel_Art.Preiniciar_Lista_Paleta();
                    /*MessageBox.Show(Minecraft.Bloques.Matriz_Bloques.Length.ToString());
                    foreach (Minecraft.Bloques Bloque in Minecraft.Bloques.Matriz_Bloques)
                    {
                        MessageBox.Show(Bloque.Transparencia_Textura.ToString(), Bloque.Nombre);
                    }*/
                    Matriz_Colores_Arco_Iris_16 = new Color[256];
                    Matriz_Colores_Arco_Iris_256 = new Color[256];
                    Matriz_Colores_Grises_256 = new Color[256];
                    Matriz_Colores_Termografía_256 = new Color[256];
                    Matriz_Lápices_Arco_Iris_256 = new Pen[256];
                    Matriz_Lápices_Grises_256 = new Pen[256];
                    Matriz_Lápices_Termografía_256 = new Pen[256];
                    Matriz_Pinceles_Arco_Iris_256 = new SolidBrush[256];
                    Matriz_Pinceles_Grises_256 = new SolidBrush[256];
                    Matriz_Pinceles_Termografía_256 = new SolidBrush[256];
                    for (int Índice = 0; Índice < 256; Índice++)
                    {
                        int Índice_Arco_Iris_16 = ((Índice % 16) * 1529) / 16;
                        int Índice_Arco_Iris = (Índice * 1529) / 255;
                        int Índice_Termografía = 1275 - ((Índice * 1275) / 255);
                        Matriz_Colores_Arco_Iris_16[Índice] = Obtener_Color_Puro_1530(Índice_Arco_Iris_16);
                        Matriz_Colores_Arco_Iris_256[Índice] = Obtener_Color_Puro_1530(Índice_Arco_Iris);
                        Matriz_Colores_Grises_256[Índice] = Color.FromArgb(255, Índice, Índice, Índice);
                        Matriz_Colores_Termografía_256[Índice] = Obtener_Color_Puro_1530(Índice_Termografía);
                        Matriz_Lápices_Arco_Iris_256[Índice] = new Pen(Obtener_Color_Puro_1530(Índice_Arco_Iris));
                        Matriz_Lápices_Grises_256[Índice] = new Pen(Color.FromArgb(255, Índice, Índice, Índice));
                        Matriz_Lápices_Termografía_256[Índice] = new Pen(Obtener_Color_Puro_1530(Índice_Termografía));
                        Matriz_Pinceles_Arco_Iris_256[Índice] = new SolidBrush(Obtener_Color_Puro_1530(Índice_Arco_Iris));
                        Matriz_Pinceles_Grises_256[Índice] = new SolidBrush(Color.FromArgb(255,Índice, Índice, Índice));
                        Matriz_Pinceles_Termografía_256[Índice] = new SolidBrush(Obtener_Color_Puro_1530(Índice_Termografía));
                    }
                }
                //catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                try
                {
                    RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Texto_Versión);
                    string Texto_Fecha_Anterior = null;
                    try { Texto_Fecha_Anterior = Clave.GetValue("Version", null) as string; }
                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Texto_Fecha_Anterior = null; }
                    if (!string.IsNullOrEmpty(Texto_Fecha_Anterior) && string.Compare(Texto_Fecha/*.Replace("_", null)*/, Texto_Fecha_Anterior, true) != 0)
                    {
                        string[] Matriz_Nombres = null;
                        try
                        {
                            Matriz_Nombres = Clave.GetSubKeyNames();
                            if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                            {
                                for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                                {
                                    try { Clave.DeleteSubKey(Matriz_Nombres[Índice]); }
                                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                                }
                            }
                            Matriz_Nombres = null;
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                        try
                        {
                            Matriz_Nombres = Clave.GetValueNames();
                            if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                            {
                                for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                                {
                                    if (string.Compare(Matriz_Nombres[Índice], "User_Name") != 0) // Don't delete the user name
                                    {
                                        try { Clave.DeleteValue(Matriz_Nombres[Índice]); }
                                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                                    }
                                }
                            }
                            Matriz_Nombres = null;
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                        MessageBox.Show("The program's settings were saved from a different version.\r\nTo ensure compatibility, all have been restored to their default values.\r\n\r\nSaved version: " + Texto_Fecha_Anterior + ".\r\nCurrent version: " + Texto_Fecha/*.Replace("_", null)*/ + ".", Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    try { Clave.SetValue("Version", Texto_Fecha/*.Replace("_", null)*/, RegistryValueKind.String); }
                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                    Clave.Close();
                    Clave = null;
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                try { SevenZip.SevenZipBase.SetLibraryPath(Environment.Is64BitProcess ? Application.StartupPath + "\\7z64.dll" : Application.StartupPath + "\\7z.dll"); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                Application.Run(new Ventana_Principal());
            }
            //catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }
    }
}
