using Microsoft.Win32;
using Minecraft_Tools.Properties;
using SevenZip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Principal : Form
    {
        public Ventana_Principal()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Variable used to quickly start again the last used tool.
        /// </summary>
        internal static int Índice_Herramienta_Anterior = -1;
        internal static bool Variable_Alfabeto_Galáctico = false;
        internal static bool Variable_Siempre_Visible = false;
        internal static int Variable_Herramienta = -1;
        internal readonly string Texto_Título = "Minecraft Tools by Jupisoft for " + Program.Texto_Usuario;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;
        internal Stopwatch Splash_Cronómetro = Stopwatch.StartNew();
        internal long Splash_Milisegundo_Anterior_2000 = -1;
        internal long Splash_Milisegundo_Anterior_100 = -1;
        internal bool Splash_Alfabeto_Galáctico_Anterior = false;
        internal int Índice_Splash = 0;
        internal string Splash_Texto = null;

        internal static Bitmap[] Matriz_Imágenes_Fuente = null;
        internal static Bitmap[] Matriz_Imágenes_Fuente_SGA = null;
        internal static int[] Matriz_Ancho_Fuente = null;
        internal static int[] Matriz_Ancho_Fuente_SGA = null;

        internal void Generar_Barras_Experiencia_Arco_Iris()
        {
            string Ruta_GUI_Icons = Application.StartupPath + "\\GUI Icons";
            string[] Matriz_Rutas = Directory.GetFiles(Ruta_GUI_Icons, "*.png", SearchOption.TopDirectoryOnly);
            if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
            {
                foreach (string Ruta in Matriz_Rutas)
                {
                    Bitmap Imagen = Program.Obtener_Imagen_Ruta(Ruta);
                    int Ancho = Imagen.Width;
                    int Alto = Imagen.Height;
                    Rectangle Rectángulo = Ancho <= 256 ? new Rectangle(0, 69, 182, 5) : new Rectangle(0, 138, 364, 10);
                    BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Imagen.Width, Imagen.Height), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                    int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                    int Bytes_Aumento = !Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 3 : 4;
                    int Bytes_Diferencia = Ancho_Stride - ((Imagen.Width * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                    byte[] Matriz_Bytes_ARGB = new byte[Ancho_Stride * Imagen.Height];
                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes_ARGB, 0, Matriz_Bytes_ARGB.Length);
                    byte Rojo, Verde, Azul;
                    double Matiz, Saturación, Luminosidad;
                    for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                    {
                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                        {
                            if (X >= Rectángulo.X && X < Rectángulo.Right &&
                                Y >= Rectángulo.Y && Y < Rectángulo.Bottom)
                            {
                                if (Matriz_Bytes_ARGB[Índice + 3] > 0)
                                {
                                    Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                    Matiz = (X * 360) / Rectángulo.Width;
                                    if (Matiz < 0d) Matiz = 0d;
                                    else if (Matiz > 360d) Matiz = 0d;
                                    //Saturación = 100d;
                                    if (Saturación < 0d) Saturación = 0d;
                                    else if (Saturación > 100d) Saturación = 100d;
                                    Program.HSL.To_RGB(Matiz, Saturación, Luminosidad, out Rojo, out Verde, out Azul);
                                    Matriz_Bytes_ARGB[Índice + 2] = Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = Verde;
                                    Matriz_Bytes_ARGB[Índice] = Azul;
                                }
                            }
                        }
                    }
                    Marshal.Copy(Matriz_Bytes_ARGB, 0, Bitmap_Data.Scan0, Matriz_Bytes_ARGB.Length);
                    Matriz_Bytes_ARGB = null;
                    Imagen.UnlockBits(Bitmap_Data);
                    Bitmap_Data = null;
                    //Program.Guardar_Imagen_Temporal(Imagen, );
                    Imagen.Save(Path.GetDirectoryName(Ruta) + "\\_" + Path.GetFileName(Ruta), ImageFormat.Png);
                }
            }
        }

        /// <summary>
        /// Finished: 2019_12_28_22_42_07_396.
        /// </summary>
        internal void Generar_Pack_Recursos_Marte()
        {
            try
            {
                /*// Image for redstone circuits.
                Bitmap Img = new Bitmap(16, 1, PixelFormat.Format32bppArgb);
                Graphics Pintar = Graphics.FromImage(Img);
                Pintar.CompositingMode = CompositingMode.SourceCopy;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.HighQuality;
                Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                for (int Índice = 0; Índice < 168; Índice++)
                {
                    Color Color_RGB;
                    Minecraft_Source_Code_Old.BlockRedstoneWire.colorMultiplier(Índice, out Color_RGB);
                    SolidBrush Pincel = new SolidBrush(Color_RGB);
                    Pintar.FillRectangle(Pincel, Índice, 0, 1, 1);
                    Pincel.Dispose();
                    Pincel = null;
                }
                Pintar.Dispose();
                Pintar = null;
                Program.Guardar_Imagen_Temporal(Img);
                return;*/
                /*// Image for melon and pumpkin stems.
                Bitmap Img = new Bitmap(8, 1, PixelFormat.Format32bppArgb);
                Graphics Pintar = Graphics.FromImage(Img);
                Pintar.CompositingMode = CompositingMode.SourceCopy;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.HighQuality;
                Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                for (int Índice = 0; Índice < 8; Índice++)
                {
                    Color Color_RGB;
                    Minecraft_Source_Code_Old.MELON_STEM_PUMPKIN_STEM.colorMultiplier(Índice, out Color_RGB);
                    SolidBrush Pincel = new SolidBrush(Color_RGB);
                    Pintar.FillRectangle(Pincel, Índice, 0, 1, 1);
                    Pincel.Dispose();
                    Pincel = null;
                }
                Pintar.Dispose();
                Pintar = null;
                Program.Guardar_Imagen_Temporal(Img);
                return;*/
                // Image for Durability, XP orbs and Mycelium.
                /*Bitmap Img = new Bitmap(1530, 1, PixelFormat.Format32bppArgb);
                Graphics Pintar = Graphics.FromImage(Img);
                Pintar.CompositingMode = CompositingMode.SourceCopy;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.HighQuality;
                Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                for (int Índice = 0; Índice < 1530; Índice++)
                {
                    SolidBrush Pincel = new SolidBrush(Program.Obtener_Color_Puro_1530(Índice));
                    Pintar.FillRectangle(Pincel, Índice, 0, 1, 1);
                    Pincel.Dispose();
                    Pincel = null;
                }
                Pintar.Dispose();
                Pintar = null;
                Program.Guardar_Imagen_Temporal(Img);
                return;*/
                string Ruta_Base = @"C:\Users\Jupisoft\AppData\Roaming\.minecraft\resourcepacks\Jupisoft Planet Mars [1.13+]\assets\minecraft\textures";
                string[] Matriz_Rutas = Directory.GetFiles(Ruta_Base, "*.png", SearchOption.AllDirectories);
                if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                {
                    if (Matriz_Rutas.Length > 1) Array.Sort(Matriz_Rutas);
                    foreach (string Ruta in Matriz_Rutas)
                    {
                        try
                        {
                            Bitmap Imagen = Program.Obtener_Imagen_Ruta(Ruta);
                            if (Imagen != null)
                            {
                                // First look if it's one of the gray textures like plants, etc and recolor it later.
                                Color Color_ARGB;
                                double Matiz_V = double.MinValue;
                                double Saturación_V = double.MinValue;
                                double Luminosidad_V = double.MinValue;
                                string Nombre = Path.GetFileNameWithoutExtension(Ruta);
                                if (string.Compare(Nombre, "end_gateway", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.WATER_FLOWING_WATER.colorMultiplier(out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "acacia_leaves", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.LEAVES2.colorMultiplier(out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "attached_melon_stem", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.MELON_STEM_PUMPKIN_STEM.colorMultiplier(7, out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "attached_pumpkin_stem", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.MELON_STEM_PUMPKIN_STEM.colorMultiplier(7, out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "birch_leaves", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.ColorizerFoliage.getFoliageColorBirch(out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "dark_oak_leaves", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.LEAVES2.colorMultiplier(out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "fern", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.TALLGRASS.colorMultiplier(out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "grass", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.GRASS.colorMultiplier(out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "grass_block_side_overlay", true) == 0 ||
                                    string.Compare(Nombre, "grass_block_top", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.GRASS.colorMultiplier(out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "jungle_leaves", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.ColorizerFoliage.getFoliageColorBasic(out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "large_fern_bottom", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.TALLGRASS.colorMultiplier(out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "large_fern_top", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.TALLGRASS.colorMultiplier(out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "lily_pad", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.WATERLILY.colorMultiplier(out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "melon_stem", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.MELON_STEM_PUMPKIN_STEM.colorMultiplier(7, out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "oak_leaves", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.ColorizerFoliage.getFoliageColorBasic(out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                /*else if (string.Compare(Nombre, "potted_fern", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.TALLGRASS.colorMultiplier(out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }*/
                                else if (string.Compare(Nombre, "pumpkin_stem", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.MELON_STEM_PUMPKIN_STEM.colorMultiplier(7, out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "redstone_dust_dot", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.BlockRedstoneWire.colorMultiplier(15, out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "redstone_dust_line0", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.BlockRedstoneWire.colorMultiplier(15, out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "redstone_dust_line1", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.BlockRedstoneWire.colorMultiplier(15, out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "redstone_wire", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.BlockRedstoneWire.colorMultiplier(15, out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "spruce_leaves", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.ColorizerFoliage.getFoliageColorPine(out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "tall_grass_bottom", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.TALLGRASS.colorMultiplier(out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "tall_grass_top", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.TALLGRASS.colorMultiplier(out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "vine", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.VINE.colorMultiplier(out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "water_flow", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.WATER_FLOWING_WATER.colorMultiplier(out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "water_overlay", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.WATER_FLOWING_WATER.colorMultiplier(out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else if (string.Compare(Nombre, "water_still", true) == 0)
                                {
                                    Minecraft_Source_Code_Old.WATER_FLOWING_WATER.colorMultiplier(out Color_ARGB);
                                    Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_V, out Saturación_V, out Luminosidad_V);
                                }
                                else // Default (do nothing), but reset each time.
                                {
                                    Matiz_V = double.MinValue;
                                    Saturación_V = double.MinValue;
                                    Luminosidad_V = double.MinValue;
                                }
                                int Ancho = Imagen.Width;
                                int Alto = Imagen.Height;
                                BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Imagen.Width, Imagen.Height), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                                int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                                int Bytes_Aumento = !Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 3 : 4;
                                int Bytes_Diferencia = Ancho_Stride - ((Imagen.Width * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                                byte[] Matriz_Bytes_ARGB = new byte[Ancho_Stride * Imagen.Height];
                                Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes_ARGB, 0, Matriz_Bytes_ARGB.Length);
                                byte Rojo, Verde, Azul;
                                double Matiz, Saturación, Luminosidad;
                                for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                                {
                                    for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                                    {
                                        if (Bytes_Aumento < 4 || Matriz_Bytes_ARGB[Índice + 3] > 0)
                                        {
                                            int Mínimo = Math.Min(Matriz_Bytes_ARGB[Índice + 2], Math.Min(Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]));
                                            int Máximo = Math.Max(Matriz_Bytes_ARGB[Índice + 2], Math.Max(Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]));
                                            int Diferencia_Máxima = Máximo - Mínimo;
                                            //if (Diferencia_Máxima < 32) // if (Matriz_Bytes_ARGB[Índice] != Matriz_Bytes_ARGB[Índice + 1] && Matriz_Bytes_ARGB[Índice] != Matriz_Bytes_ARGB[Índice + 2])
                                            Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                            if (Matiz_V > double.MinValue && Diferencia_Máxima < 32) // Recolor the texture.
                                            {
                                                Matiz = Matiz_V;
                                                Saturación = Saturación_V;
                                            }
                                            //if (Matiz >= 30 && Matiz < 180) // Matiz >= 90 && Matiz < 150)
                                            //if (Saturación <= 0) Saturación = 0.5d;
                                            //if (Luminosidad <= 25) Luminosidad = 25d;
                                            if (Saturación > 0d)
                                            {
                                                // 0 = Red.
                                                // 30 = Orange.
                                                // 60 = Yellow.
                                                // 120 = Green.
                                                // 180 = Cyan.
                                                // 240 = Blue.
                                                // 300 = Fuchsia.
                                                //Matiz -= 220;
                                                Matiz -= 135; // Grass path. // OK
                                                //Matiz += 95; // Green redstone. // OK.
                                                //Matiz += 240; // Blue nether wart outline. // OK.
                                                //Matiz += 60 - 37; // Default.
                                                //Matiz += 190 - 30; // Blue lava. // 215 - 30; // OK.
                                                //Matiz += 205 - 30; // Blue fire, torches, etc. // OK.
                                                //Matiz += 117; // Grass path. // Matiz += 60 - 37; // Matiz += 120 - 37; // Wrong.
                                                //Matiz -= 275 - 60; // Purple to yellow nether portal.
                                                //Matiz -= 220; // Now change the color from green to orange or an equivalent rotation.
                                                //Matiz -= 210; // For Mars red skies.
                                                //Matiz -= 220d; // For red rain.
                                                //Matiz -= 200d; // For stars at day.
                                                if (Matiz < 0d) Matiz += 360d;
                                                else if (Matiz >= 360d) Matiz -= 360d;
                                                //Luminosidad *= 0.75d; // For stars at day.
                                                //Saturación = 100d;
                                                //if (Saturación < 0d) Saturación = 0d;
                                                //else if (Saturación > 100d) Saturación = 100d;
                                                //Luminosidad = 100d;
                                                //if (Luminosidad < 0d) Luminosidad = 0d;
                                                //else if (Luminosidad > 100d) Luminosidad = 100d;
                                                Program.HSL.To_RGB(Matiz, Saturación, Luminosidad, out Rojo, out Verde, out Azul);
                                                //Matriz_Bytes_ARGB[Índice + 3] = 192; // For stars at day.
                                                Matriz_Bytes_ARGB[Índice + 2] = Rojo;
                                                Matriz_Bytes_ARGB[Índice + 1] = Verde;
                                                Matriz_Bytes_ARGB[Índice] = Azul;
                                            }
                                        }
                                    }
                                }
                                Marshal.Copy(Matriz_Bytes_ARGB, 0, Bitmap_Data.Scan0, Matriz_Bytes_ARGB.Length);
                                Matriz_Bytes_ARGB = null;
                                Imagen.UnlockBits(Bitmap_Data);
                                Bitmap_Data = null;
                                //Program.Guardar_Imagen_Temporal(Imagen, );
                                Imagen.Save(Ruta, ImageFormat.Png); // WARNING: Overwriting the original files!
                            }
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Principal_Load(object sender, EventArgs e)
        {
            try
            {
                //Generar_Pack_Recursos_Marte();
                //Generar_Imagen_Espiral(64, 64, 100, false, true);
                //Generar_Barras_Experiencia_Arco_Iris();
                if (Program.Icono_Jupisoft == null) Program.Icono_Jupisoft = this.Icon.Clone() as Icon;
                this.Text = Texto_Título + " - [" + Program.Texto_Minecraft_Versión + ", Known vanilla blocks: " + Program.Traducir_Número(Minecraft.Bloques.Matriz_Bloques.Length) + "]";
                this.WindowState = FormWindowState.Maximized;
                Barra_Estado_Etiqueta_Negro.Image = Program.Obtener_Imagen_Color(Barra_Estado_Etiqueta_Negro.ForeColor); // Set this instead of black, in case some user has another default color.
                Barra_Estado_Etiqueta_Azul.Image = Program.Obtener_Imagen_Color(Barra_Estado_Etiqueta_Azul.ForeColor);
                Barra_Estado_Etiqueta_Rojo.Image = Program.Obtener_Imagen_Color(Barra_Estado_Etiqueta_Rojo.ForeColor);
                Barra_Estado_Etiqueta_Sugerencia.Text = "Welcome dear " + Program.Texto_Usuario + ", I wish you a great day.";
                if (Matriz_Imágenes_Fuente == null || Matriz_Imágenes_Fuente.Length <= 0)
                {
                    Matriz_Imágenes_Fuente = new Bitmap[256];
                    Matriz_Ancho_Fuente = new int[256];
                    for (int Y = 0, Índice = 0; Y < 128; Y += 8)
                    {
                        for (int X = 0; X < 128; X += 8, Índice++)
                        {
                            Bitmap Imagen = Resources.Fuente_ascii.Clone(new Rectangle(X, Y, 8, 8), PixelFormat.Format32bppArgb);
                            Rectangle Rectángulo = Program.Buscar_Zona_Recorte_Imagen(Imagen, Color.Transparent);
                            if (Rectángulo.X > -1 && Rectángulo.Y > -1 && Rectángulo.X < int.MaxValue && Rectángulo.Y < int.MaxValue && Rectángulo.Width > 0 && Rectángulo.Height > 0)
                            {
                                Rectángulo.Y = 0; // Don't move it vertically.
                                Rectángulo.Height = 8;
                                Matriz_Imágenes_Fuente[Índice] = Imagen.Clone(Rectángulo, PixelFormat.Format32bppArgb);
                                Matriz_Ancho_Fuente[Índice] = Rectángulo.Width;
                            }
                            else Matriz_Ancho_Fuente[Índice] = 4;
                        }
                    }
                }
                if (Matriz_Imágenes_Fuente_SGA == null || Matriz_Imágenes_Fuente_SGA.Length <= 0)
                {
                    Matriz_Imágenes_Fuente_SGA = new Bitmap[256];
                    Matriz_Ancho_Fuente_SGA = new int[256];
                    for (int Y = 0, Índice = 0; Y < 128; Y += 8)
                    {
                        for (int X = 0; X < 128; X += 8, Índice++)
                        {
                            Bitmap Imagen = Resources.Fuente_ascii_sga.Clone(new Rectangle(X, Y, 8, 8), PixelFormat.Format32bppArgb);
                            Rectangle Rectángulo = Program.Buscar_Zona_Recorte_Imagen(Imagen, Color.Transparent);
                            if (Rectángulo.X > -1 && Rectángulo.Y > -1 && Rectángulo.X < int.MaxValue && Rectángulo.Y < int.MaxValue && Rectángulo.Width > 0 && Rectángulo.Height > 0)
                            {
                                Rectángulo.Y = 0; // Don't move it vertically.
                                Rectángulo.Height = 8;
                                Matriz_Imágenes_Fuente_SGA[Índice] = Imagen.Clone(Rectángulo, PixelFormat.Format32bppArgb);
                                Matriz_Ancho_Fuente_SGA[Índice] = Rectángulo.Width;
                            }
                            else Matriz_Ancho_Fuente_SGA[Índice] = 4;
                        }
                    }
                }

                // Add a little info about David Maeso's free music albums
                // (if you are reading this, please feel free to listen to the songs).
                if (David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_David_Maeso != null && David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_David_Maeso.Count > 0)
                {
                    for (int Índice_Álbum = 0; Índice_Álbum < David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_David_Maeso.Count; Índice_Álbum++)
                    {
                        Menú_Principal_David_Maeso.DropDownItems.Add(new ToolStripMenuItem(David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_David_Maeso[Índice_Álbum].Título + " (" + David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_David_Maeso[Índice_Álbum].Año.ToString() + ")...", Program.Obtener_Imagen_Recursos("David_Maeso_" + David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_David_Maeso[Índice_Álbum].Recurso + "_16"), new EventHandler(Menú_Principal_David_Maeso_Click), "Menú_Principal_David_Maeso_" + Índice_Álbum.ToString()));
                    }
                }
                // Add a little info about Fratelli Stellari's free music albums
                // (if you are reading this, please feel free to listen to their songs).
                //string Texto = null;
                if (David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Fratelli_Stellari != null && David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Fratelli_Stellari.Count > 0)
                {
                    bool DJoNemesis_Lilly = false;
                    for (int Índice_Álbum = 0; Índice_Álbum < David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Fratelli_Stellari.Count; Índice_Álbum++)
                    {
                        if (!string.IsNullOrEmpty(David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Fratelli_Stellari[Índice_Álbum].Título))
                        {
                            Menú_Principal_Fratelli_Stellari.DropDownItems.Add(new ToolStripMenuItem((!DJoNemesis_Lilly ? null : "DJoNemesis and Lilly - ") + David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Fratelli_Stellari[Índice_Álbum].Título + " (" + David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Fratelli_Stellari[Índice_Álbum].Año.ToString() + ")...", Program.Obtener_Imagen_Recursos("Fratelli_Stellari_" + David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Fratelli_Stellari[Índice_Álbum].Recurso + "_16"), new EventHandler(Menú_Principal_Fratelli_Stellari_Click), "Menú_Principal_Fratelli_Stellari_" + Índice_Álbum.ToString()));
                            //Program.Guardar_Imagen_Temporal(Program.Obtener_Imagen_Miniatura(Minecraft.Obtener_Textura_Recursos("Fratelli_Stellari_" + Lista_Álbumes_Fratelli_Stellari[Índice_Álbum].Recurso + "_16"), 16, 16, true, true), "Fratelli_Stellari_" + Lista_Álbumes_Fratelli_Stellari[Índice_Álbum].Recurso + "_16");
                        }
                        else
                        {
                            DJoNemesis_Lilly = true;
                            Menú_Principal_Fratelli_Stellari.DropDownItems.Add(new ToolStripSeparator());
                        }
                        //Texto += "Fratelli_Stellari_" + Lista_Álbumes_Fratelli_Stellari[Índice_Álbum].Recurso + "_16" + "\r\n";
                    }
                }
                //Clipboard.SetText(Texto);
                // Add a little info about my own free music albums
                // (if you are reading this, please feel free to listen to the songs,
                // since on the website there are full previews, even with the original
                // MIDI scores ready to download, and all for free).
                if (David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Jupisoft != null && David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Jupisoft.Count > 0)
                {
                    for (int Índice_Álbum = 0; Índice_Álbum < David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Jupisoft.Count; Índice_Álbum++)
                    {
                        Menú_Principal_Jupisoft.DropDownItems.Add(new ToolStripMenuItem(David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Jupisoft[Índice_Álbum].Título + " (" + David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Jupisoft[Índice_Álbum].Año.ToString() + ")...", Program.Obtener_Imagen_Recursos("Jupisoft_" + David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Jupisoft[Índice_Álbum].Recurso + "_16"), new EventHandler(Menú_Principal_Jupisoft_Click), "Menú_Principal_Jupisoft_" + Índice_Álbum.ToString()));
                    }
                }
                /*string Ruta_Imágenes = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Covers";
                string[] Matriz_Rutas = Directory.GetFiles(Ruta_Imágenes, "*.png", SearchOption.TopDirectoryOnly);
                if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                {
                    Array.Sort(Matriz_Rutas);
                    int Índice_Álbum = Lista_Álbumes_David_Maeso.Count - 1;
                    foreach (string Ruta in Matriz_Rutas)
                    {
                        FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                        Image Imagen_Original = null;
                        try { Imagen_Original = Image.FromStream(Lector, false, false); }
                        catch { Imagen_Original = null; }
                        if (Imagen_Original != null)
                        {
                            int Ancho = Imagen_Original.Width;
                            int Alto = Imagen_Original.Height;
                            Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                            Graphics Pintar = Graphics.FromImage(Imagen);
                            Pintar.CompositingMode = CompositingMode.SourceCopy;
                            Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                            Pintar.Dispose();
                            Pintar = null;
                            Imagen = Program.Obtener_Imagen_Miniatura(Imagen, 16, 16, true, true);
                            Program.Guardar_Imagen_Temporal(Imagen, Program.Traducir_Texto_Mayúsculas_Minúsculas_Automáticamente("Fratelli_Stellari_" + Lista_Álbumes_Fratelli_Stellari[Índice_Álbum].Título_Recursos/*Path.GetFileNameWithoutExtension(Ruta)*//* + "_16").Replace(" ", "_").Replace(".", "__").Replace(":", "___"));
                            Imagen.Dispose();
                            Imagen = null;
                        }
                        Lector.Close();
                        Lector.Dispose();
                        Lector = null;
                        Índice_Álbum--;
                    }
                }*/
                /*if (Hermitcraft.Hermits.Matriz_Hermits != null && Hermitcraft.Hermits.Matriz_Hermits.Length > 0)
                {
                    foreach (Hermitcraft.Hermits Hermit in Hermitcraft.Hermits.Matriz_Hermits)
                    {
                        Bitmap Imagen = Minecraft.Obtener_Textura_Recursos("Hermitcraft_" + Hermit.Lista_Nombres_Minecraft[0]);
                        if (Imagen != null)
                        {
                            Imagen = Program.Obtener_Imagen_Miniatura(Imagen, 16, 16, true, false);
                            Program.Guardar_Imagen_Temporal(Imagen, "Hermitcraft_" + Hermit.Lista_Nombres_Minecraft[0]);
                        }
                    }
                }*/
                //Program.Guardar_Imagen_Temporal(Program.Obtener_Imagen_Miniatura(Resources.Xisumavoid, 16, 16, true, false));
                this.Select();
                this.Focus();

                /*for (int Y = 0, Índice = 0; Y < 16; Y++)
                {
                    for (int X = 0; X < 16; X++, Índice++)
                    {
                        Bitmap Imagen_Temporal = Resources.Fuente_ascii.Clone(new Rectangle(X * 8, Y * 8, 8, 8), PixelFormat.Format32bppArgb);
                        Bitmap Imagen_Temporal_SGA = Resources.Fuente_ascii_sga.Clone(new Rectangle(X * 8, Y * 8, 8, 8), PixelFormat.Format32bppArgb);
                        Rectangle Rectángulo = Program.Buscar_Zona_Recorte_Imagen(Imagen_Temporal);
                        Rectangle Rectángulo_SGA = Program.Buscar_Zona_Recorte_Imagen(Imagen_Temporal_SGA);
                        if (Rectángulo.X > -1 && Rectángulo.Y > -1 && Rectángulo.X < int.MaxValue && Rectángulo.Y < int.MaxValue && Rectángulo.Width > 0 && Rectángulo.Height > 0)
                        {
                            Rectángulo.Y = 0; // Don't move it vertically.
                            Rectángulo.Height = 8;
                            Imagen_Temporal = Imagen_Temporal.Clone(Rectángulo, PixelFormat.Format32bppArgb);
                            //Pintar.DrawImage(Imagen_Temporal, new Rectangle((X * 8) + ((8 - Rectángulo.Width) / 2), Y * 8, Rectángulo.Width, 8), new Rectangle(0, 0, Rectángulo.Width, Rectángulo.Height), GraphicsUnit.Pixel);
                            Program.Guardar_Imagen_Temporal(Imagen_Temporal, "Fuente_ascii_" + Índice.ToString());
                        }
                        if (Rectángulo_SGA.X > -1 && Rectángulo_SGA.Y > -1 && Rectángulo_SGA.X < int.MaxValue && Rectángulo_SGA.Y < int.MaxValue && Rectángulo_SGA.Width > 0 && Rectángulo_SGA.Height > 0)
                        {
                            Rectángulo_SGA.Y = 0; // Don't move it vertically.
                            Rectángulo_SGA.Height = 8;
                            Imagen_Temporal_SGA = Imagen_Temporal_SGA.Clone(Rectángulo_SGA, PixelFormat.Format32bppArgb);
                            //Pintar_SGA.DrawImage(Imagen_Temporal_SGA, new Rectangle((X * 8) + ((8 - Rectángulo_SGA.Width) / 2), Y * 8, Rectángulo_SGA.Width, 8), new Rectangle(0, 0, Rectángulo_SGA.Width, Rectángulo_SGA.Height), GraphicsUnit.Pixel);
                            Program.Guardar_Imagen_Temporal(Imagen_Temporal_SGA, "Fuente_ascii_sga_" + Índice.ToString());
                        }
                        Imagen_Temporal.Dispose();
                        Imagen_Temporal_SGA.Dispose();
                    }
                }*/
                Minecraft_3D_Textures();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Principal_Shown(object sender, EventArgs e)
        {
            try
            {
                //Minecraft_Splashes.Buscar_Splashes_Repetidos();
                //Borrar_Color_Fondo_Imagen(@"C:\Users\Jupisoft\Videos\__DVDs copiados\VillagerTradeChart.png", Color.FromArgb(255, 56, 56, 56));
                //Crear_Imagen_Mosaico_Fondo();
                //MessageBox.Show((((byte)179 >> 4) & 0xF).ToString()); // Primeros 4 bits de un byte
                //MessageBox.Show(((byte)179 & 0xF).ToString()); // Últimos 4 bits de un byte
                bool Mostrar_Herramientas = Registro_Cargar_Opciones();
                if (Program.Edición_Aplicación == CheckState.Unchecked) Menú_Principal_Herramientas_Abrir_Predeterminada.PerformClick();
                else if (Program.Edición_Aplicación == CheckState.Checked)
                {
                    int Índice_Herramienta = -1;
                    for (int Índice = 0; Índice < Ventana_Selector_Herramientas.Herramientas.Matriz_Herramientas.Length; Índice++)
                    {
                        if (string.Compare(Ventana_Selector_Herramientas.Herramientas.Matriz_Herramientas[Índice].Texto_Tipo, typeof(Ventana_Filtros_Tiempo_Real).FullName, true) == 0)
                        {
                            Índice_Herramienta = Índice;
                            break;
                        }
                    }
                    Ventana_Selector_Herramientas.Herramientas.Ejecutar_Herramienta(Índice_Herramienta, Variable_Siempre_Visible, this);
                }
                else if (Program.Edición_Aplicación == CheckState.Indeterminate)
                {
                    int Índice_Herramienta = -1;
                    for (int Índice = 0; Índice < Ventana_Selector_Herramientas.Herramientas.Matriz_Herramientas.Length; Índice++)
                    {
                        if (string.Compare(Ventana_Selector_Herramientas.Herramientas.Matriz_Herramientas[Índice].Texto_Tipo, typeof(Ventana_Analizador_Matemático_Multidimensional).FullName, true) == 0)
                        {
                            Índice_Herramienta = Índice;
                            break;
                        }
                    }
                    Ventana_Selector_Herramientas.Herramientas.Ejecutar_Herramienta(Índice_Herramienta, Variable_Siempre_Visible, this);
                }
                /*if (!Mostrar_Herramientas) Menú_Principal_Herramientas_Abrir_Predeterminada.PerformClick();
                else // Show the list of tools to any new user.
                {
                    //Menú_Principal_Herramientas.ShowDropDown();
                    //Menú_Principal_Herramientas_Selector_Herramientas.Select();
                    //Menú_Principal_Herramientas_Selector_Herramientas.PerformClick();
                }*/
                Temporizador_Principal_Tick(Temporizador_Principal, EventArgs.Empty);
                this.Activate();
                //Temporizador_Principal.Start();
                Menú_Principal_Herramientas_Selector_Herramientas.PerformClick();
                //this.Text = Program.Texto_Título + " - [Minecraft: " + Program.Texto_Minecraft_Versión + ", Vanilla blocks known: " + Program.Traducir_Número(Minecraft.Bloques.Matriz_Bloques.Length) + "]";

                //MessageBox.Show(Minecraft_Biomas.Obtener_Color_ARGB(2302743).ToString());

                // Reset the average ARGB colors of every Minecraft texture:
                /*Minecraft.Diccionario_Bloques_Índices_Colores.Clear();
                foreach (KeyValuePair<short, string> Entrada in Minecraft.Diccionario_Bloques_Índices_Nombres)
                {
                    Color Color_ARGB = Ventana_Color_Medio_Imagen.Obtener_Color_Medio_Imagen(Minecraft.Obtener_Textura_Recursos(Entrada.Value));
                    Minecraft.Diccionario_Bloques_Índices_Colores.Add(Entrada.Key, Color_ARGB);
                }*/

                // Search for equal colors:
                /*int Repeticiones = 0;
                string Texto = null;
                string Texto_Diccionario = null;
                Dictionary<int, short> Diccionario_Colores = new Dictionary<int, short>();
                foreach (KeyValuePair<short, Color> Entrada in Minecraft.Diccionario_Bloques_Índices_Colores)
                {
                    int Código_Hash = Entrada.Value.GetHashCode();
                    //Texto_Diccionario += "Diccionario_Bloques_Índices_Colores.Add(Diccionario_Bloques_Nombres_Índices[\"" + Minecraft.Diccionario_Bloques_Índices_Nombres[Entrada.Key] + "\"], Color.FromArgb(" + Entrada.Value.A.ToString() + ", " + Entrada.Value.R.ToString() + ", " + Entrada.Value.G.ToString() + ", " + Entrada.Value.B.ToString() + "));\r\n";
                    if (!Diccionario_Colores.ContainsKey(Código_Hash))
                    {
                        Diccionario_Colores.Add(Código_Hash, Entrada.Key);
                    }
                    else
                    {
                        //Texto += Entrada.Value.ToString() + " = " + Minecraft.Diccionario_Bloques_Índices_Nombres[Entrada.Key] + "\r\n";
                        Texto += Minecraft.Diccionario_Bloques_Índices_Nombres[Entrada.Key] + " = " + Minecraft.Diccionario_Bloques_Índices_Nombres[Diccionario_Colores[Código_Hash]] + "\r\n";
                        Repeticiones++;
                    }
                }
                if (!string.IsNullOrEmpty(Texto)) Clipboard.SetText(Repeticiones.ToString() + "\r\n\r\n" + Texto + "\r\n" + Texto_Diccionario);*/

                // Resource pack resolution converter (for lower PCs) [04-08-2019].
                // Result: success, but crashes, it also needs to change other files.
                // Now redone to resave the images as real PNG to save a lot of disk space.
                /*string Ruta_Entrada = @"C:\Users\Jupisoft\AppData\Roaming\.minecraft\resourcepacks\256x Pulchra Revisited 1.13+_";
                string Ruta_Salida = @"C:\Users\Jupisoft\AppData\Roaming\.minecraft\resourcepacks\256x Pulchra Revisited 1.14+";
                string[] Matriz_Rutas = Directory.GetFiles(Ruta_Entrada, "*", SearchOption.AllDirectories);
                if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                {
                    foreach (string Ruta in Matriz_Rutas)
                    {
                        Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Salida + Ruta.Substring(Ruta_Entrada.Length)));
                        //ImageMagick.MagickImage Imagen = null;
                        Image Imagen_Original = null;
                        FileStream Lector = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        if (Lector != null)
                        {
                            if (Lector.Length > 0L)
                            {
                                //try { Imagen = new ImageMagick.MagickImage(Lector); }
                                try { Imagen_Original = Image.FromStream(Lector, false, false); }
                                catch { Imagen_Original = null; }
                            }
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
                                Imagen.Save(Ruta_Salida + Ruta.Substring(Ruta_Entrada.Length), ImageFormat.Png);
                                Imagen.Dispose();
                                Imagen = null;
                                Lector.Close();
                                Lector.Dispose();
                                Lector = null;
                                //Imagen.BackgroundColor = new ImageMagick.MagickColor(Color.Transparent);
                                //Imagen.BorderColor = new ImageMagick.MagickColor(Color.Transparent);
                                //Imagen.Interlace = ImageMagick.Interlace.Png;
                                //Imagen.Interpolate = ImageMagick.PixelInterpolateMethod.Spline;
                                //Imagen.Resize((Imagen.Width * 64) / 512, (Imagen.Height * 64) / 512);
                                //Imagen.ToBitmap(ImageFormat.Png).Save(Ruta_Salida + Ruta.Substring(Ruta_Entrada.Length), ImageFormat.Png);
                            }
                            else
                            {
                                Lector.Close();
                                Lector.Dispose();
                                Lector = null;
                                File.Copy(Ruta, Ruta_Salida + Ruta.Substring(Ruta_Entrada.Length));
                            }
                        }
                    }
                }
                Matriz_Rutas = null;*/

                /*// Draw an "Infiniscope" shape blueprint (very accurate since on the "Floating City"
                // squares or any sharp edges were never used on anything built there) [20-08-2019].
                int Ancho_Alto = 1024; // All sizes are proportional to this global value.
                int Altura_Tubo = (int)Math.Round((double)Ancho_Alto / 10d, MidpointRounding.AwayFromZero); // 64.
                int Altura_Tubo_Doble = Altura_Tubo * 2;
                int Altura_Tubo_2 = Altura_Tubo - 2;
                int Margen = (int)Math.Round((double)Ancho_Alto / 32d, MidpointRounding.AwayFromZero); // 20.
                int Margen_1 = Margen + 1;
                int Margen_Doble = Margen * 2;
                int Margen_Doble_2 = (Margen * 2) + 2;
                //int Diámetro_Tubo = (int)Math.Round((double)Altura_Tubo * 6.5d, MidpointRounding.AwayFromZero); // 640 - 224.
                int Diámetro_Tubo = (int)Math.Round((double)Altura_Tubo * 3.5d, MidpointRounding.AwayFromZero); // 224.
                int Diámetro_Tubo_Mitad = Diámetro_Tubo / 2;
                
                Bitmap Imagen = new Bitmap(Ancho_Alto, Ancho_Alto, PixelFormat.Format32bppArgb);
                Graphics Pintar = Graphics.FromImage(Imagen);
                Pintar.CompositingMode = CompositingMode.SourceOver;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.None;
                Pintar.Clear(Color.White); // White background.

                // Lower rectangle.
                Pintar.FillRectangle(Brushes.Black, Margen, (Ancho_Alto - Margen) - Altura_Tubo, Ancho_Alto - Margen_Doble, Altura_Tubo);
                Pintar.FillRectangle(Brushes.Silver, Margen_1, (Ancho_Alto - Margen_1) - Altura_Tubo_2, Ancho_Alto - Margen_Doble_2, Altura_Tubo_2);

                // Exterior circle.
                Pintar.FillEllipse(Brushes.Silver, Margen + Diámetro_Tubo_Mitad, Margen + Diámetro_Tubo, (Ancho_Alto - Margen_Doble) - Diámetro_Tubo, (Ancho_Alto - Margen_Doble) - Diámetro_Tubo);
                Pintar.DrawEllipse(Pens.Black, Margen + Diámetro_Tubo_Mitad, Margen + Diámetro_Tubo, (Ancho_Alto - Margen_Doble) - Diámetro_Tubo, (Ancho_Alto - Margen_Doble) - Diámetro_Tubo);
                
                // Interior circle.
                Pintar.FillEllipse(Brushes.White, (Margen + Diámetro_Tubo_Mitad) + Altura_Tubo, (Margen + Diámetro_Tubo) + Altura_Tubo, ((Ancho_Alto - Margen_Doble) - Diámetro_Tubo) - Altura_Tubo_Doble, ((Ancho_Alto - Margen_Doble) - Diámetro_Tubo) - Altura_Tubo_Doble);
                Pintar.DrawEllipse(Pens.Black, (Margen + Diámetro_Tubo_Mitad) + Altura_Tubo, (Margen + Diámetro_Tubo) + Altura_Tubo, ((Ancho_Alto - Margen_Doble) - Diámetro_Tubo) - Altura_Tubo_Doble, ((Ancho_Alto - Margen_Doble) - Diámetro_Tubo) - Altura_Tubo_Doble);

                // Lower rectangle end overlay.
                Pintar.FillRectangle(Brushes.Silver, Margen_1, (Ancho_Alto - Margen_1) - Altura_Tubo_2, Ancho_Alto - Margen_Doble_2, Altura_Tubo_2);

                Pintar.Dispose();
                Pintar = null;
                Program.Guardar_Imagen_Temporal(Imagen);
                Imagen.Dispose();
                Imagen = null;*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Principal_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Principal_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Alt && !e.Control && !e.Shift)
                {
                    if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Delete)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Menú_Principal_Archivo_Salir.PerformClick();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Archivo_Salir_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_Siempre_Visible_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Siempre_Visible = Menú_Principal_Ver_Siempre_Visible.Checked;
                this.TopMost = Variable_Siempre_Visible;
                Registro_Guardar_Opciones();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ayuda_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
                Registro_Restablecer_Opciones();
                Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                Ventana.Variable_Siempre_Visible = Menú_Principal_Ver_Siempre_Visible.Checked;
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
                Registro_Guardar_Opciones();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ayuda_Depurador_Excepciones_Click(object sender, EventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
                Registro_Restablecer_Opciones();
                Ventana_Depurador_Excepciones Ventana = new Ventana_Depurador_Excepciones();
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
                Registro_Guardar_Opciones();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ayuda_Acerca_Click(object sender, EventArgs e)
        {
            try
            {
                Registro_Restablecer_Opciones();
                Ventana_Acerca Ventana = new Ventana_Acerca();
                Ventana.Variable_Siempre_Visible = Menú_Principal_Ver_Siempre_Visible.Checked;
                DialogResult Resultado = Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
                Registro_Guardar_Opciones();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Herramientas_Abrir_Predeterminada_Click(object sender, EventArgs e)
        {
            try
            {
                if (Variable_Herramienta > -1)
                {
                    Temporizador_Principal.Stop();
                    Registro_Restablecer_Opciones();
                    Ventana_Selector_Herramientas.Herramientas.Ejecutar_Herramienta(Variable_Herramienta, Variable_Siempre_Visible, this);
                    Registro_Guardar_Opciones();
                    Temporizador_Principal.Start();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Loads the saved (or default if missing) options from the Windows registry.
        /// </summary>
        /// <returns>Returns true if the tools menu should be opened (only after selecting a user name). Returns false otherwise.</returns>
        internal bool Registro_Cargar_Opciones()
        {
            bool Mostrar_Herramientas = false;
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión);
                try { Variable_Siempre_Visible = bool.Parse((string)Clave.GetValue("Always_On_Top", bool.FalseString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Siempre_Visible = false; }
                try
                {
                    Variable_Herramienta = -1;
                    string Texto_Tipo = Clave.GetValue("Default_Tool", null) as string;
                    if (!string.IsNullOrEmpty(Texto_Tipo))
                    {
                        for (int Índice = 0; Índice < Ventana_Selector_Herramientas.Herramientas.Matriz_Herramientas.Length; Índice++)
                        {
                            if (!string.IsNullOrEmpty(Ventana_Selector_Herramientas.Herramientas.Matriz_Herramientas[Índice].Texto_Tipo) &&
                                string.Compare(Texto_Tipo, Ventana_Selector_Herramientas.Herramientas.Matriz_Herramientas[Índice].Texto_Tipo, true) == 0)
                            {
                                Variable_Herramienta = Índice;
                                break;
                            }
                        }
                        Texto_Tipo = null;
                    }
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Herramienta = -1; }
                
                /*Ventana_Gracias.Agradecimientos.Lista_Agradecimientos.Sort(new Ventana_Gracias.Comparador_Agradecimientos());
                Ventana_Acerca.Lista_Gracias.Clear();
                foreach (Ventana_Gracias.Agradecimientos Agradecimiento in Ventana_Gracias.Agradecimientos.Lista_Agradecimientos)
                {
                    Ventana_Acerca.Lista_Gracias.Add(Agradecimiento.Nombre);
                }
                if (!Ventana_Acerca.Lista_Gracias.Contains(Program.Texto_Usuario))
                {
                    Ventana_Gracias.Agradecimientos.Lista_Agradecimientos.Insert(0, new Ventana_Gracias.Agradecimientos(Program.Texto_Usuario, null, DateTime.Now.Date, "Minecraft Tools", "https://www.minecraftforum.net/forums/minecraft-java-edition/recent-updates-and-snapshots/2894808-minecraft-1-13-new-chunk-format-fully-decoded-read"));
                    Ventana_Acerca.Lista_Gracias.Insert(0, Program.Texto_Usuario);
                }*/

                // Apply all the loaded values:
                Menú_Principal_Ver_Siempre_Visible.Checked = Variable_Siempre_Visible;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return Mostrar_Herramientas;
        }

        internal void Registro_Guardar_Opciones()
        {
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión);
                string[] Matriz_Nombres = Clave.GetValueNames();
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                    {
                        if (string.Compare(Matriz_Nombres[Índice], "Version") != 0 && string.Compare(Matriz_Nombres[Índice], "User_Name") != 0) Clave.DeleteValue(Matriz_Nombres[Índice]);
                    }
                }
                Matriz_Nombres = null;
                try { Clave.SetValue("User_Name", Program.Texto_Usuario, RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                try { Clave.SetValue("Always_On_Top", Menú_Principal_Ver_Siempre_Visible.Checked, RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                try { Clave.SetValue("Default_Tool", Variable_Herramienta > -1 ? Ventana_Selector_Herramientas.Herramientas.Matriz_Herramientas[Variable_Herramienta].Texto_Tipo : string.Empty, RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Calling this before loading a (default) tool might save the program from
        /// starting that same tool after it had a crash, thus letting the user delete
        /// all the options from the Windows registry to avoid any bad setting to keep
        /// crashing the program every time it loads a default tool.
        /// If it didn't crash, it saves again the main options as if nothing ever happened.
        /// Note: the always on top option will be lost if it crashes...
        /// </summary>
        internal void Registro_Restablecer_Opciones()
        {
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión);
                string[] Matriz_Nombres = Clave.GetValueNames();
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                    {
                        try { if (string.Compare(Matriz_Nombres[Índice], "Version") != 0 && string.Compare(Matriz_Nombres[Índice], "User_Name") != 0) Clave.DeleteValue(Matriz_Nombres[Índice]); }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    Matriz_Nombres = null;
                }
                Clave.Close();
                Clave = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ayuda_Descargar_Minecraft_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.minecraft.net/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ayuda_Sitio_Hermitcraft_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://hermitcraft.com/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ayuda_Minecraft_Forum_Jupisoft_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.minecraftforum.net/forums/mapping-and-modding-java-edition/minecraft-tools/2947154-minecraft-tools-in-c-for-1-14-with-full-source", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ayuda_Sitio_Jupisoft_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://jupisoft.x10host.com/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ayuda_Enviar_Correo_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("mailto:jupitermauro@gmail.com?subject=" + Program.Texto_Programa + " " + Program.Texto_Versión + ", " + Program.Texto_Fecha.Replace("_", null), ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ayuda_Donar_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=KSMZ3XNG2R9P6", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Archivo_Borrar_Opciones_Registro_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(this, "Do you want to delete all the program options saved in the registry?\r\n\r\nNote: this is useful if you want to \"uninstall\" the program, and want to remove all of it's traces as if it was never executed on this computer.\r\n\r\nAfter this message, the program will exit, then you can delete it if you want. But remember that if you start it again, you will need to delete it's registry options whenever you want to fully uninstall it.\r\n\r\nBut if you want to keep all your settings saved after \"uninstalling\", just delete the program and it's libraries, and the next time you download it, your settings will still be there (if they are from the same version).", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión);
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
                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                    try
                    {
                        Matriz_Nombres = Clave.GetValueNames();
                        if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                        {
                            for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                            {
                                try { Clave.DeleteValue(Matriz_Nombres[Índice]); }
                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                            }
                        }
                        Matriz_Nombres = null;
                    }
                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                    Clave.Close();
                    Clave = null;
                    try
                    {
                        Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools");
                        Clave.DeleteSubKey(Program.Texto_Versión, true);
                        Clave.Close();
                        Clave = null;
                    }
                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                    finally { Environment.Exit(0); }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Barra_Estado_Botón_Excepción_Click(object sender, EventArgs e)
        {
            try
            {
                Variable_Excepción = false;
                Variable_Excepción_Imagen = false;
                Variable_Excepción_Total = 0;
                Barra_Estado_Botón_Excepción.Visible = false;
                Barra_Estado_Separador_1.Visible = false;
                Barra_Estado_Botón_Excepción.Image = Resources.Excepción_Gris;
                Barra_Estado_Botón_Excepción.ForeColor = Color.Black;
                Barra_Estado_Botón_Excepción.Text = "Exceptions: 0";
                Ventana_Depurador_Excepciones Ventana = new Ventana_Depurador_Excepciones();
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Contains the original names of the Minecraft blocks included within the Minecraft launcher, which are actually included in the library file called "launcher.dll", inside the folder called "game", near the executable launcher. Note: to extract it's resources, simply install 7-zip (it's free) and right click the library file and finally select to extract it's files anywhere you like.
        /// </summary>
        internal static readonly string[] Matriz_Nombres_Bloques_Launcher_Minerales = new string[]
        {
            "Coal_Block",
            "Coal_Ore",
            "Diamond_Block",
            "Diamond_Ore",
            "Emerald_Block",
            "Emerald_Ore",
            "Gold_Block",
            "Gold_Ore",
            "Iron_Block",
            "Iron_Ore",
            "Lapis_Ore",
            "Quartz_Ore",
            "Redstone_Block",
            "Redstone_Ore"
        };

        /// <summary>
        /// Contains the original names of the Minecraft blocks included within the Minecraft launcher, which are actually included in the library file called "launcher.dll", inside the folder called "game", near the executable launcher. Note: to extract it's resources, simply install 7-zip (it's free) and right click the library file and finally select to extract it's files anywhere you like.
        /// </summary>
        internal static readonly string[] Matriz_Nombres_Bloques_Launcher = new string[]
        {
            "Bedrock",
            "Bookshelf",
            "Brick",
            "Chest",
            "Clay",
            "Coal_Block",
            "Coal_Ore",
            "Cobblestone",
            "Crafting_Table",
            "Diamond_Block",
            "Diamond_Ore",
            "Dirt",
            "Dirt_Podzol",
            "Dirt_Snow",
            "Emerald_Block",
            "Emerald_Ore",
            "End_Stone",
            "Farmland",
            "Furnace",
            "Furnace_On",
            "Glass",
            "Glowstone",
            "Gold_Block",
            "Gold_Ore",
            "Grass",
            "Gravel",
            "Hardened_Clay",
            "Ice_Packed",
            "Iron_Block",
            "Iron_Ore",
            "Lapis_Ore",
            "Leaves_Birch",
            "Leaves_Jungle",
            "Leaves_Oak",
            "Leaves_Spruce",
            "Log_Acacia",
            "Log_Birch",
            "Log_DarkOak",
            "Log_Jungle",
            "Log_Oak",
            "Log_Spruce",
            "Mycelium",
            "Nether_Brick",
            "Netherrack",
            "Obsidian",
            "Planks_Acacia",
            "Planks_Birch",
            "Planks_DarkOak",
            "Planks_Jungle",
            "Planks_Oak",
            "Planks_Spruce",
            "Quartz_Ore",
            "Red_Sand",
            "Red_Sandstone",
            "Redstone_Block",
            "Redstone_Ore",
            "Sand",
            "Sandstone",
            "Snow",
            "Soul_Sand",
            "Stone",
            "Stone_Andesite",
            "Stone_Diorite",
            "Stone_Granite",
            "TNT",
            "Wool"
        };

        private void Temporizador_Principal_Tick(object sender, EventArgs e)
        {
            try
            {
                /*string[] aa = Directory.GetFiles(@"C:\Users\Jupisoft\Desktop\Miniaturas 2018_09_27_02_23_10_220\launcher\assets\images\icons", "*.png", SearchOption.TopDirectoryOnly);
                Array.Sort(aa);
                List<string> Listaa = new List<string>();
                foreach (string Ruta in aa)
                {
                    Listaa.Add(Path.GetFileNameWithoutExtension(Ruta));
                }
                File.WriteAllLines(Program.Obtener_Ruta_Temporal_Escritorio() + ".txt", Listaa.ToArray(), Encoding.Unicode);
                Application.Exit(); // 2018_09_27_02_57_56_474*/
                // Change the splash text randomly every 2,5 seconds.
                long Milisegundos = Splash_Cronómetro.ElapsedMilliseconds;
                long Milisegundos_2000 = Milisegundos / 2000L;
                long Milisegundos_100 = Milisegundos / 100L;
                if (Milisegundos_2000 != Splash_Milisegundo_Anterior_2000 || (string.Compare(Splash_Texto, "Colormatic", true) == 0 && Milisegundos_100 != Splash_Milisegundo_Anterior_100) || Variable_Alfabeto_Galáctico != Splash_Alfabeto_Galáctico_Anterior)
                {
                    if (Milisegundos_2000 != Splash_Milisegundo_Anterior_2000)
                    {
                        Picture_Mineral_Izquierda.Image = Program.Obtener_Imagen_Recursos(Matriz_Nombres_Bloques_Launcher[Program.Rand.Next(0, Matriz_Nombres_Bloques_Launcher.Length)]);
                        Picture_Mineral_Derecha.Image = Program.Obtener_Imagen_Recursos(Matriz_Nombres_Bloques_Launcher[Program.Rand.Next(0, Matriz_Nombres_Bloques_Launcher.Length)]);

                        /*Bitmap Imagen_Bloque_Izquierda = Program.Obtener_Imagen_Recursos(Matriz_Nombres_Bloques_Launcher[Program.Rand.Next(0, Matriz_Nombres_Bloques_Launcher.Length)]);
                        Bitmap Imagen_Bloque_Derecha = Program.Obtener_Imagen_Recursos(Matriz_Nombres_Bloques_Launcher[Program.Rand.Next(0, Matriz_Nombres_Bloques_Launcher.Length)]);

                        int Girar = Program.Rand.Next(0, 4);
                        if (Girar == 1) Imagen_Bloque_Izquierda.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        else if (Girar == 2) Imagen_Bloque_Izquierda.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        else if (Girar == 3) Imagen_Bloque_Izquierda.RotateFlip(RotateFlipType.Rotate270FlipNone);

                        Girar = Program.Rand.Next(0, 4);
                        if (Girar == 1) Imagen_Bloque_Derecha.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        else if (Girar == 2) Imagen_Bloque_Derecha.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        else if (Girar == 3) Imagen_Bloque_Derecha.RotateFlip(RotateFlipType.Rotate270FlipNone);

                        Picture_Bloque_Izquierda.Image = Imagen_Bloque_Izquierda;
                        Picture_Bloque_Derecha.Image = Imagen_Bloque_Derecha;*/

                        //Picture_Bloque_Izquierda.Image = Program.Obtener_Imagen_Recursos(Matriz_Nombres_Bloques_Launcher[Program.Rand.Next(0, Matriz_Nombres_Bloques_Launcher.Length)]);
                        //Picture_Bloque_Derecha.Image = Program.Obtener_Imagen_Recursos(Matriz_Nombres_Bloques_Launcher[Program.Rand.Next(0, Matriz_Nombres_Bloques_Launcher.Length)]);

                        Bitmap Imagen_Izquierda = Program.Obtener_Imagen_Recursos("mc_char_" + Program.Rand.Next(0, 8).ToString());
                        Imagen_Izquierda.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        Picture_Personaje_Izquierda.Image = Imagen_Izquierda;
                        Picture_Personaje_Derecha.Image = Program.Obtener_Imagen_Recursos("mc_char_" + Program.Rand.Next(0, 8).ToString());

                        if (!string.IsNullOrEmpty(Splash_Texto)) Índice_Splash = Program.Rand.Next(1, Minecraft_Splashes.Lista_Líneas.Count); // Ignore the first splash, that tells how many there are (without itself).
                        else Índice_Splash = 0; // Always show the splash count at the beginning.
                        Splash_Texto = Minecraft_Splashes.Lista_Líneas[Índice_Splash];
                        if (string.IsNullOrEmpty(Splash_Texto)) Splash_Texto = "?";
                        if (Milisegundos_2000 % 4L == 0) // Only show every 4 splashes.
                        {
                            DateTime Fecha = DateTime.Now; // Obtain the current system date.
                            //Fecha = new DateTime(2018, 10, 31); // Used only for testing and debugging.
                            if (Fecha.Month == 12 && Fecha.Day == 24) Splash_Texto = "Merry X-mas!";
                            else if (Fecha.Month == 1 && Fecha.Day == 1) Splash_Texto = "Happy new year!";
                            else if (Fecha.Month == 4 && Fecha.Day == 21) Splash_Texto = "Happy birthday Jupisoft!";
                            else if (Fecha.Month == 10 && Fecha.Day == 31) Splash_Texto = "OOoooOOOoooo! Spooky!";
                        }
                        //Splash_Texto = "Colormatic"; // Debug of random rainbow colors.
                        Splash_Milisegundo_Anterior_2000 = Milisegundos_2000;
                    }
                    Splash_Milisegundo_Anterior_100 = Milisegundos_100;
                    Splash_Alfabeto_Galáctico_Anterior = Variable_Alfabeto_Galáctico;
                    int Ancho = 0;
                    foreach (char Caracter in Splash_Texto)
                    {
                        int Valor_Caracter = (int)Caracter;
                        if (Valor_Caracter < 0 || Valor_Caracter > 255) Valor_Caracter = (int)'?';
                        Ancho += ((!Variable_Alfabeto_Galáctico ? Matriz_Ancho_Fuente[Valor_Caracter] : Matriz_Ancho_Fuente_SGA[Valor_Caracter]) + 1) + 1;
                    }
                    Ancho--;
                    int Ancho_Cliente = Picture_Splash.ClientSize.Width - 12; // 6 pixels of margins x 2.
                    int Alto_Cliente = Picture_Splash.ClientSize.Height - 12; // 6 pixels of margins x 2.
                    int Autozoom = Math.Max(Math.Min(Ancho_Cliente / Ancho, Alto_Cliente / 9), 1); // Minimum of 1x.
                    Bitmap Imagen = new Bitmap((Ancho * Autozoom), Alto_Cliente, PixelFormat.Format32bppArgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceOver;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.None;
                    //Color Color_ARGB_Fondo = Color.Maroon;
                    Color Color_ARGB_Fondo = Color.FromArgb(128, 128, 128);
                    Color Color_ARGB = Color.White;
                    List<Color> Lista_Colores_Aleatorios = null;
                    if (string.Compare(Splash_Texto, "Colormatic", true) == 0)
                    {
                        Lista_Colores_Aleatorios = new List<Color>(new Color[10]
                        {
                            //Color.FromArgb(255, 0, 0), // Avoid red color because of the background.
                            //Color.FromArgb(255, 160, 0), // Also avoid orange color.
                            Color.FromArgb(255, 255, 0), // 1 color for letter available.
                            Color.FromArgb(160, 255, 0),
                            Color.FromArgb(0, 255, 0),
                            Color.FromArgb(0, 255, 160),
                            Color.FromArgb(0, 255, 255),
                            Color.FromArgb(0, 160, 255),
                            Color.FromArgb(0, 0, 255),
                            Color.FromArgb(160, 0, 255),
                            Color.FromArgb(255, 0, 255),
                            Color.FromArgb(255, 0, 160),
                        });
                        Lista_Colores_Aleatorios = Program.Aleatorizar_Lista(Lista_Colores_Aleatorios); // Randomize the color order each 100 milliseconds (25 times per splash).
                    }
                    for (int Índice_Caracter = 0, Índice_X = 0; Índice_Caracter < Splash_Texto.Length; Índice_Caracter++)
                    {
                        if (string.Compare(Splash_Texto, "Colormatic", true) == 0)
                        {
                            Color_ARGB = Lista_Colores_Aleatorios[Índice_Caracter % Lista_Colores_Aleatorios.Count];
                            //Color_ARGB = Program.Obtener_Color_Puro_1530(Program.Rand.Next(160, 1530 - 160)); // Get a pure color that's not red.
                            //Color_ARGB = Color.FromArgb(Program.Rand.Next(128, 256), Program.Rand.Next(128, 256), Program.Rand.Next(128, 256));
                            //Color_ARGB_Fondo = Color.FromArgb(Color_ARGB.R / 2, Color_ARGB.G / 2, Color_ARGB.B / 2);
                            //Color_ARGB_Fondo = Color.FromArgb(Color_ARGB.R / 3, Color_ARGB.G / 3, Color_ARGB.B / 3);
                            //Color_ARGB_Fondo = Color.Gray;
                        }
                        /*else if (Índice_Splash >= Minecraft_Splashes.Índice_Hermitcraft && Índice_Splash < Minecraft_Splashes.Índice_Monster_High)
                        {
                            Color_ARGB_Fondo = Color.FromArgb(128, 128, 0); // Yellow shadow.
                        }
                        else if (Índice_Splash >= Minecraft_Splashes.Índice_Monster_High && Índice_Splash < Minecraft_Splashes.Índice_Jupisoft)
                        {
                            Color_ARGB_Fondo = Color.FromArgb(128, 0, 80); // Pink shadow.
                        }
                        else if (Índice_Splash >= Minecraft_Splashes.Índice_Jupisoft)
                        {
                            Color_ARGB_Fondo = Color.FromArgb(128, 128, 128); // Gray shadow.
                        }*/
                        // Dark red shadow text:
                        int Valor_Caracter = "\u00c0\u00c1\u00c2\u00c8\u00ca\u00cb\u00cd\u00d3\u00d4\u00d5\u00da\u00df\u00e3\u00f5\u011f\u0130\u0131\u0152\u0153\u015e\u015f\u0174\u0175\u017e\u0207\u0000\u0000\u0000\u0000\u0000\u0000\u0000 !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~\u0000\u00c7\u00fc\u00e9\u00e2\u00e4\u00e0\u00e5\u00e7\u00ea\u00eb\u00e8\u00ef\u00ee\u00ec\u00c4\u00c5\u00c9\u00e6\u00c6\u00f4\u00f6\u00f2\u00fb\u00f9\u00ff\u00d6\u00dc\u00f8\u00a3\u00d8\u00d7\u0192\u00e1\u00ed\u00f3\u00fa\u00f1\u00d1\u00aa\u00ba\u00bf\u00ae\u00ac\u00bd\u00bc\u00a1\u00ab\u00bb\u2591\u2592\u2593\u2502\u2524\u2561\u2562\u2556\u2555\u2563\u2551\u2557\u255d\u255c\u255b\u2510\u2514\u2534\u252c\u251c\u2500\u253c\u255e\u255f\u255a\u2554\u2569\u2566\u2560\u2550\u256c\u2567\u2568\u2564\u2565\u2559\u2558\u2552\u2553\u256b\u256a\u2518\u250c\u2588\u2584\u258c\u2590\u2580\u03b1\u03b2\u0393\u03c0\u03a3\u03c3\u03bc\u03c4\u03a6\u0398\u03a9\u03b4\u221e\u2205\u2208\u2229\u2261\u00b1\u2265\u2264\u2320\u2321\u00f7\u2248\u00b0\u2219\u00b7\u221a\u207f\u00b2\u25a0\u0000".IndexOf(Splash_Texto[Índice_Caracter]);
                        if (Valor_Caracter < 0 || Valor_Caracter > 255) Valor_Caracter = (int)'?';
                        if (!Variable_Alfabeto_Galáctico)
                        {
                            if (Matriz_Imágenes_Fuente[Valor_Caracter] != null) Pintar.DrawImage(Program.Obtener_Imagen_Pintada(Matriz_Imágenes_Fuente[Valor_Caracter].Clone() as Bitmap, Color.Black, Color_ARGB_Fondo), new Rectangle(Índice_X + Autozoom, Alto_Cliente - (8 * Autozoom), Matriz_Ancho_Fuente[Valor_Caracter] * Autozoom, 8 * Autozoom), new Rectangle(0, 0, Matriz_Ancho_Fuente[Valor_Caracter], 8), GraphicsUnit.Pixel);
                        }
                        else
                        {
                            if (Matriz_Imágenes_Fuente_SGA[Valor_Caracter] != null) Pintar.DrawImage(Program.Obtener_Imagen_Pintada(Matriz_Imágenes_Fuente_SGA[Valor_Caracter].Clone() as Bitmap, Color.Black, Color_ARGB_Fondo), new Rectangle(Índice_X + Autozoom, Alto_Cliente - (8 * Autozoom), Matriz_Ancho_Fuente_SGA[Valor_Caracter] * Autozoom, 8 * Autozoom), new Rectangle(0, 0, Matriz_Ancho_Fuente_SGA[Valor_Caracter], 8), GraphicsUnit.Pixel);
                        }
                        // White regular text (because the original yellow text kinda looks weird with the red background):
                        if (!Variable_Alfabeto_Galáctico)
                        {
                            if (Matriz_Imágenes_Fuente[Valor_Caracter] != null) Pintar.DrawImage(Program.Obtener_Imagen_Pintada(Matriz_Imágenes_Fuente[Valor_Caracter].Clone() as Bitmap, Color.Black, Color_ARGB), new Rectangle(Índice_X, Alto_Cliente - ((8 * Autozoom) + Autozoom), Matriz_Ancho_Fuente[Valor_Caracter] * Autozoom, 8 * Autozoom), new Rectangle(0, 0, Matriz_Ancho_Fuente[Valor_Caracter], 8), GraphicsUnit.Pixel);
                        }
                        else
                        {
                            if (Matriz_Imágenes_Fuente_SGA[Valor_Caracter] != null) Pintar.DrawImage(Program.Obtener_Imagen_Pintada(Matriz_Imágenes_Fuente_SGA[Valor_Caracter].Clone() as Bitmap, Color.Black, Color_ARGB), new Rectangle(Índice_X, Alto_Cliente - ((8 * Autozoom) + Autozoom), Matriz_Ancho_Fuente_SGA[Valor_Caracter] * Autozoom, 8 * Autozoom), new Rectangle(0, 0, Matriz_Ancho_Fuente_SGA[Valor_Caracter], 8), GraphicsUnit.Pixel);
                        }
                        // Increase the width counter:
                        if (!Variable_Alfabeto_Galáctico) Índice_X += (Matriz_Ancho_Fuente[Valor_Caracter] + 1) * Autozoom;
                        else Índice_X += (Matriz_Ancho_Fuente_SGA[Valor_Caracter] + 1) * Autozoom;
                    }
                    Pintar.Dispose();
                    Pintar = null;
                    Rectangle Rectángulo = Program.Buscar_Zona_Recorte_Imagen(Imagen, Color.Transparent);
                    if (Rectángulo.X > -1 && Rectángulo.Y > -1 && Rectángulo.X < int.MaxValue && Rectángulo.Y < int.MaxValue && Rectángulo.Width > 0 && Rectángulo.Height > 0)
                    {
                        Rectángulo.Y = 0; // Don't move it vertically...
                        Rectángulo.Height = Alto_Cliente; // But center it horizontally.
                        Imagen = Imagen.Clone(Rectángulo, PixelFormat.Format32bppArgb);
                    }
                    //Imagen = Program.Obtener_Imagen_Pintada(Imagen, Color.Black, Color.White);
                    Picture_Splash.Image = Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            int Tick = Environment.TickCount;
            try
            {
                if (Variable_Excepción)
                {
                    if ((Environment.TickCount / 500) % 2 == 0)
                    {
                        if (!Variable_Excepción_Imagen)
                        {
                            Variable_Excepción_Imagen = true;
                            Barra_Estado_Botón_Excepción.Image = Resources.Excepción;
                            Barra_Estado_Botón_Excepción.ForeColor = Color.Red;
                            Barra_Estado_Botón_Excepción.Text = "Exceptions: " + Program.Traducir_Número(Variable_Excepción_Total);
                        }
                    }
                    else
                    {
                        if (Variable_Excepción_Imagen)
                        {
                            Variable_Excepción_Imagen = false;
                            Barra_Estado_Botón_Excepción.Image = Resources.Excepción_Gris;
                            Barra_Estado_Botón_Excepción.ForeColor = Color.Black;
                            Barra_Estado_Botón_Excepción.Text = "Exceptions: " + Program.Traducir_Número(Variable_Excepción_Total);
                        }
                    }
                    if (!Barra_Estado_Botón_Excepción.Visible) Barra_Estado_Botón_Excepción.Visible = true;
                    if (!Barra_Estado_Separador_1.Visible) Barra_Estado_Separador_1.Visible = true;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            try
            {
                try
                {
                    if (Tick % 250 == 0) // Only update every quarter second
                    {
                        if (Program.Rendimiento_Procesador != null)
                        {
                            double CPU = (double)Program.Rendimiento_Procesador.NextValue();
                            if (CPU < 0d) CPU = 0d;
                            else if (CPU > 100d) CPU = 100d;
                            Barra_Estado_Etiqueta_CPU.Text = "CPU: " + Program.Traducir_Número_Decimales_Redondear(CPU, 2) + " %";
                        }
                        Program.Proceso.Refresh();
                        long Memoria_Bytes = Program.Proceso.PagedMemorySize64;
                        Barra_Estado_Etiqueta_Memoria.Text = "RAM: " + Program.Traducir_Tamaño_Bytes_Automático(Memoria_Bytes, 2, true);
                        if (Memoria_Bytes < 4294967296L) // < 4 GB
                        {
                            if (Variable_Memoria)
                            {
                                Variable_Memoria = false;
                                Barra_Estado_Etiqueta_Memoria.ForeColor = Color.Black;
                            }
                        }
                        else // >= 4 GB
                        {
                            if (Tick % 1000 < 500) // Half second on
                            {
                                if (!Variable_Memoria)
                                {
                                    Variable_Memoria = true;
                                    Barra_Estado_Etiqueta_Memoria.ForeColor = Color.Red;
                                }
                            }
                            else
                            {
                                if (Variable_Memoria) // Half second off
                                {
                                    Variable_Memoria = false;
                                    Barra_Estado_Etiqueta_Memoria.ForeColor = Color.Black;
                                }
                            }
                        }
                    }
                }
                catch { Barra_Estado_Etiqueta_Memoria.Text = "RAM: ? MB (? GB)"; }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_Abrir_Carpeta_Minecraft_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(Program.Ruta_Guardado_Minecraft)) Program.Ejecutar_Ruta(Program.Ruta_Guardado_Minecraft, ProcessWindowStyle.Maximized);
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_Abrir_Carpeta_Twitch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(Program.Ruta_Guardado_Twitch)) Program.Ejecutar_Ruta(Program.Ruta_Guardado_Twitch, ProcessWindowStyle.Maximized);
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Herramientas_Aleatoria_Click(object sender, EventArgs e)
        {
            /*try
            {
                Herramientas Herramienta = (Herramientas)Program.Rand.Next(1, (int)Herramientas.Aleatoria);

                if (Herramienta == Herramientas.Visor_Mundos_Realista_2D) Menú_Principal_Herramientas_Visor_Mundos_Realista_2D.PerformClick();
                else if (Herramienta == Herramientas.Buscador_Chunks_Limos) Menú_Principal_Herramientas_Buscador_Chunks_Limos.PerformClick();
                else if (Herramienta == Herramientas.Visor_Skins_Animado_3D) Menú_Principal_Herramientas_Herramienta_Predeterminada_Visor_Skins_Animado_3D.PerformClick();
                else if (Herramienta == Herramientas.Calculadora_Infinita_Semillas_Mundos) Menú_Principal_Herramientas_Calculadora_Infinita_Semillas_Mundos.PerformClick();

                else if (Herramienta == Herramientas.Generador_Pixel_Art_Exportador_Mundos) Menú_Principal_Herramientas_Generador_Pixel_Art_Exportador_Mundos.PerformClick();
                else if (Herramienta == Herramientas.Exportador_Estructuras_Pintadas) Menú_Principal_Herramientas_Exportador_Estructuras_Pintadas.PerformClick();
                else if (Herramienta == Herramientas.Generador_Estructuras_Personalizadas) Menú_Principal_Herramientas_Generador_Estructuras_Personalizadas.PerformClick();
                else if (Herramienta == Herramientas.Generador_Miniaturas_Color_Medio) Menú_Principal_Herramientas_Generador_Miniaturas_Color_Medio.PerformClick();

                else if (Herramienta == Herramientas.Visor_NBT) Menú_Principal_Herramientas_Visor_NBT.PerformClick();
                else if (Herramienta == Herramientas.Buscador_Diferencias_Versiones_JAR) Menú_Principal_Herramientas_Buscador_Diferencias_Versiones_JAR.PerformClick();
                else if (Herramienta == Herramientas.Diseñador_Piedra_Rojiza) Menú_Principal_Herramientas_Diseñador_Piedra_Rojiza.PerformClick();
                else if (Herramienta == Herramientas.Generador_Estructuras_Comandos) Menú_Principal_Herramientas_Generador_Estructuras_Comandos.PerformClick();

                else if (Herramienta == Herramientas.Reloj_Minecraft_Tiempo_Real) Menú_Principal_Herramientas_Reloj_Minecraft_Tiempo_Real.PerformClick();
                else if (Herramienta == Herramientas.Visor_Información_Bloques) Menú_Principal_Herramientas_Visor_Información_Bloques.PerformClick();
                else if (Herramienta == Herramientas.Administrador_Copias_Seguridad) Menú_Principal_Herramientas_Administrador_Copias_Seguridad.PerformClick();
                else if (Herramienta == Herramientas.Descargador_Skins_Automático) Menú_Principal_Herramientas_Descargador_Skins_Automático.PerformClick();

                else if (Herramienta == Herramientas.Visor_Cuadros) Menú_Principal_Herramientas_Visor_Cuadros.PerformClick();
                else if (Herramienta == Herramientas.Afinador_Bloques_Nota) Menú_Principal_Herramientas_Afinador_Bloques_Nota.PerformClick();
                else if (Herramienta == Herramientas.Diseñador_Estandartes_Escudos) Menú_Principal_Herramientas_Diseñador_Estandartes_Escudos.PerformClick();
                else if (Herramienta == Herramientas.Visor_Ofertas_Aldeanos) Menú_Principal_Herramientas_Visor_Ofertas_Aldeanos.PerformClick();

                else if (Herramienta == Herramientas.Visor_Información_Entidades) Menú_Principal_Herramientas_Visor_Información_Entidades.PerformClick();
                else if (Herramienta == Herramientas.Reconstructor_Estructura_Archivos_Recursos) Menú_Principal_Herramientas_Reconstructor_Estructura_Archivos_Recursos.PerformClick();
                // Soon...
                // Soon...

                else if (Herramienta == Herramientas.Visor_Ayuda) Menú_Principal_Ayuda_Visor_Ayuda.PerformClick();
                else if (Herramienta == Herramientas.Depurador_Excepciones) Menú_Principal_Ayuda_Depurador_Excepciones.PerformClick();
                else if (Herramienta == Herramientas.Cambiar_Nombre_Usuario) Menú_Principal_Ayuda_Cambiar_Nombre_Usuario.PerformClick();
                else if (Herramienta == Herramientas.Acerca) Menú_Principal_Ayuda_Acerca.PerformClick();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        */}

        private void Menú_Principal_Ayuda_Reddit_Hermitcraft_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.reddit.com/r/HermitCraft/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ayuda_Sitio_Xisumavoid_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://xisumavoid.com/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_BDubs_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/bdoubleo100", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_Biffa_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/biffaplays", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_Cleo_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/ZombieCleo", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_Cubfan_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/cubfan135", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_Doc_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.youtube.com/docm77", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_Etho_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.youtube.com/ethoslab", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_False_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/FalseSymmetry", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_Hypno_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/hypnotizd", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_iJevin_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/ijevin", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_Impulse_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.youtube.com/impulseSV", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_Iskall_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/Iskall85", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_Jessassin_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/thejessassin", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_JoeHills_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/joehillstsd", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_Keralis_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/keralis", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_Mumbo_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/ThatMumboJumbo", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_PythonGB_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/user/PythonGB", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_ReNDoG_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/rendog", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_Scar_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/goodtimeswithscar", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_Stress_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/stressmonster101", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_TangoTek_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/TangoTekLP", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_Tinfoilchef_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/selif1", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_VintageBeef_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/vintagebeef", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_Welsknight_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/welsknightgaming", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_xBCrafted_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/xbxaxcx", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_Xisuma_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/xisumavoid", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_Zedaph_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://youtube.com/zedaphplays", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Hermitcraft_Información_Completa_Miembros_Click(object sender, EventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
                Registro_Restablecer_Opciones();
                Ventana_Información_Miembros_Hermitcraft Ventana = new Ventana_Información_Miembros_Hermitcraft();
                Ventana.Variable_Siempre_Visible = Menú_Principal_Ver_Siempre_Visible.Checked;
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
                Registro_Guardar_Opciones();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_Amidst_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.minecraftforum.net/forums/mapping-and-modding-java-edition/minecraft-tools/1262200-v3-7-amidst-strongholds-village-biome-etc-finder", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_Chunk_Base_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://chunkbase.com/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_MC_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.mcedit.net/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_MC_Skin_3D_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.planetminecraft.com/mod/mcskin3d/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_Miners_Need_Cool_Shoes_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.needcoolshoes.com/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_Name_MC_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://es.namemc.com/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_NBT_Explorer_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.minecraftforum.net/forums/mapping-and-modding-java-edition/minecraft-tools/1262665-nbtexplorer-nbt-editor-for-windows-and-mac", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_NBT_Explorer_Minecraft_1_13_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://github.com/jaquadro/NBTExplorer/releases", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_Note_Block_Studio_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.stuffbydavid.com/mcnbs", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_Optifine_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://optifine.net/home", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_Skin_History_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://mcskinhistory.com/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_Skin_Viewer_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.minecraftforum.net/forums/mapping-and-modding-java-edition/minecraft-tools/1261408-minecraft-skin-viewer-1-2-supports-1-8-skins", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_Spritecraft_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.diamondpants.com/spritecraft/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_Substrate_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.minecraftforum.net/forums/mapping-and-modding-java-edition/minecraft-tools/1261313-sdk-substrate-map-editing-library-for-c-net-1-3-8", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_Universal_Minecraft_Editor_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.universalminecrafteditor.com/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_David_Maeso_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem Menú = sender as ToolStripMenuItem;
                if (Menú != null)
                {
                    int Índice_Álbum = int.Parse(Menú.Name.Replace("Menú_Principal_David_Maeso_", null));
                    if (David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_David_Maeso != null && David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_David_Maeso.Count > 0 && Índice_Álbum > -1 && Índice_Álbum < David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_David_Maeso.Count && !string.IsNullOrEmpty(David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_David_Maeso[Índice_Álbum].URL_Html))
                    {
                        Program.Ejecutar_Ruta(David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_David_Maeso[Índice_Álbum].URL_Html, ProcessWindowStyle.Normal);
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Fratelli_Stellari_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem Menú = sender as ToolStripMenuItem;
                if (Menú != null)
                {
                    int Índice_Álbum = int.Parse(Menú.Name.Replace("Menú_Principal_Fratelli_Stellari_", null));
                    if (David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Fratelli_Stellari != null && David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Fratelli_Stellari.Count > 0 && Índice_Álbum > -1 && Índice_Álbum < David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Fratelli_Stellari.Count && !string.IsNullOrEmpty(David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Fratelli_Stellari[Índice_Álbum].URL_Html))
                    {
                        Program.Ejecutar_Ruta(David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Fratelli_Stellari[Índice_Álbum].URL_Html, ProcessWindowStyle.Normal);
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_David_Maeso_Visitar_Web_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.davidmaeso.com/index.html", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Jupisoft_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem Menú = sender as ToolStripMenuItem;
                if (Menú != null)
                {
                    int Índice_Álbum = int.Parse(Menú.Name.Replace("Menú_Principal_Jupisoft_", null));
                    if (David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Jupisoft != null && David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Jupisoft.Count > 0 && Índice_Álbum > -1 && Índice_Álbum < David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Jupisoft.Count && !string.IsNullOrEmpty(David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Jupisoft[Índice_Álbum].URL_Html))
                    {
                        Program.Ejecutar_Ruta(David_Maeso_Fratelli_Stellari_Jupisoft.Lista_Álbumes_Jupisoft[Índice_Álbum].URL_Html, ProcessWindowStyle.Normal);
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Jupisoft_Visitar_Web_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://jupisoft.x10host.com/index.html", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ayuda_Gracias_Click(object sender, EventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
                Registro_Restablecer_Opciones();
                Ventana_Gracias Ventana = new Ventana_Gracias();
                Ventana.Variable_Siempre_Visible = Menú_Principal_Ver_Siempre_Visible.Checked;
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
                Registro_Guardar_Opciones();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Fratelli_Stellari_Visitar_Web_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.messaggidallestelle.altervista.org/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_Skin_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //if (e.Button != MouseButtons.Right)
                {
                    Temporizador_Principal.Stop();
                    Registro_Restablecer_Opciones();
                    Ventana_Información_Miembros_Hermitcraft Ventana = new Ventana_Información_Miembros_Hermitcraft();
                    Ventana.Variable_Siempre_Visible = Menú_Principal_Ver_Siempre_Visible.Checked;
                    Ventana.Aleatorizar_Inicio = true;
                    Ventana.ShowDialog(this);
                    Ventana.Dispose();
                    Ventana = null;
                    Registro_Guardar_Opciones();
                    Temporizador_Principal.Start();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_Descargar_26601_Skins_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.mediafire.com/file/rhbf9vd9e002170/26601+Minecraft+Skins.rar", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_Descargar_Packs_Recursos_Edición_Consola_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.mediafire.com/file/1cc56joz4091n44/Minecraft+Console+Edition+Packs.zip", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Herramientas_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem Menú = sender as ToolStripMenuItem;
                if (Menú != null)
                {
                    int Índice_Herramienta = int.Parse(Menú.Name.Replace("Menú_Principal_Herramientas_", null));
                    if (Índice_Herramienta > -1 && Índice_Herramienta < Ventana_Selector_Herramientas.Herramientas.Matriz_Herramientas.Length)
                    {
                        Temporizador_Principal.Stop();
                        Registro_Restablecer_Opciones();
                        Ventana_Selector_Herramientas.Herramientas.Ejecutar_Herramienta(Índice_Herramienta, Variable_Siempre_Visible, this);
                        Registro_Guardar_Opciones();
                        Temporizador_Principal.Start();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Herramienta_Predeterminada_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem Menú = sender as ToolStripMenuItem;
                if (Menú != null)
                {
                    int Índice_Herramienta = int.Parse(Menú.Name.Replace("Menú_Principal_Herramienta_Predeterminada_", null));
                    if (Índice_Herramienta > -1 && Índice_Herramienta < Ventana_Selector_Herramientas.Herramientas.Matriz_Herramientas.Length)
                    {
                        Temporizador_Principal.Stop();
                        Registro_Restablecer_Opciones();
                        Ventana_Selector_Herramientas.Herramientas.Ejecutar_Herramienta(Índice_Herramienta, Variable_Siempre_Visible, this);
                        Registro_Guardar_Opciones();
                        Temporizador_Principal.Start();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Barra_Estado_Botón_Secretos_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control && string.Compare(Environment.UserName, "Jupisoft", true) == 0)
                {
                    // Encrypts any file and cuts it if it's over 20 MB, so it makes secret files.
                    // This function should be hidden to the users, unless they are "Jupisoft".
                    string Ruta_Secretos = Application.StartupPath + "\\Secrets";
                    if (!string.IsNullOrEmpty(Ruta_Secretos) && Directory.Exists(Ruta_Secretos))
                    {
                        string[] Matriz_Rutas = Directory.GetFiles(Ruta_Secretos, "*", SearchOption.AllDirectories);
                        if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                        {
                            int Archivos_Archivos = 0;
                            int Archivos_Imágenes = 0;
                            int Archivos_Encriptados = 0;
                            int Archivos_Cortados = 0;
                            byte[] Matriz_Bytes = new byte[4096];
                            foreach (string Ruta in Matriz_Rutas)
                            {
                                Program.Quitar_Atributo_Sólo_Lectura(Ruta);
                                FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                if (Lector != null && Lector.Length > 0L)
                                {
                                    Lector.Seek(0L, SeekOrigin.Begin);
                                    Image Imagen = null;
                                    try { Imagen = Image.FromStream(Lector, false, false); }
                                    catch { Imagen = null; }
                                    if (Imagen == null) // The file is not a valid image.
                                    {
                                        try
                                        {
                                            Archivos_Archivos++;
                                            // Verify if the zip files can be loaded, meaning they aren't encrypted.
                                            Lector.Seek(0L, SeekOrigin.Begin);
                                            SevenZipExtractor Extractor_7_Zip = new SevenZipExtractor(Lector);
                                            if (Extractor_7_Zip == null || Extractor_7_Zip.FilesCount <= 0 || !Extractor_7_Zip.Check())
                                            {
                                                Extractor_7_Zip = null;
                                                continue; // Skip the files that couldn't be loaded as zip files because they should be already encrypted.
                                            }
                                            else Extractor_7_Zip = null;
                                            Lector.Seek(0L, SeekOrigin.Begin); // The zip reader will change this.
                                            // Encrypt the file now.
                                            for (long Índice_Bloque = 0L; Índice_Bloque < Lector.Length; Índice_Bloque += 4096L)
                                            {
                                                int Longitud = Lector.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                                if (Longitud > 0)
                                                {
                                                    Matriz_Bytes = Jupisoft_Encrypting_Decrypting.Encriptar_Matriz_Bytes(Matriz_Bytes, true, false, true, false);
                                                    Lector.Seek(Índice_Bloque, SeekOrigin.Begin);
                                                    Lector.Write(Matriz_Bytes, 0, Longitud);
                                                }
                                                else break;
                                            }
                                            Archivos_Encriptados++;
                                            continue; // Ignore the file cutting on a first pass, to get the file CRC32.
                                        }
                                        catch { } // The file should already be encrypted.
                                        // Cut any file over 20 MB and already encrypted.
                                        long Tamaño_Máximo_Archivo = 20971520L; // 20 MB.
                                        if (Lector.Length > Tamaño_Máximo_Archivo) // File too big to be uploaded at GitHub, so cut it.
                                        {
                                            string Ruta_Nombre = Path.GetDirectoryName(Ruta) + "\\" + Path.GetFileNameWithoutExtension(Ruta) + '_';
                                            string Extensión = null;
                                            try { Extensión = Path.GetExtension(Ruta); }
                                            catch { Extensión = null; }
                                            // Ignore the first part of the original file.
                                            Lector.Seek(Tamaño_Máximo_Archivo, SeekOrigin.Begin); // The zip reader will change this.
                                            for (;;) // Export the original file as chunks of up to 20 MB.
                                            {
                                                while (File.Exists(Ruta_Nombre + Extensión)) Ruta_Nombre += '_';
                                                FileStream Lector_Salida = new FileStream(Ruta_Nombre + Extensión, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                                Lector_Salida.SetLength(0L);
                                                Lector_Salida.Seek(0L, SeekOrigin.Begin);
                                                int Longitud = 0;
                                                for (long Índice_Bloque = 0L; Índice_Bloque < Tamaño_Máximo_Archivo; Índice_Bloque += 4096L)
                                                {
                                                    Longitud = Lector.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                                    if (Longitud > 0) Lector_Salida.Write(Matriz_Bytes, 0, Longitud);
                                                    else break;
                                                }
                                                Lector_Salida.Close();
                                                Lector_Salida.Dispose();
                                                Lector_Salida = null;
                                                Ruta_Nombre += '_';
                                                if (Longitud <= 0) break; // End of file.
                                            }
                                            // Turn the original file into the first chunk of 20 MB.
                                            Lector.SetLength(Tamaño_Máximo_Archivo);
                                            Lector.Close();
                                            Lector.Dispose();
                                            Lector = null;
                                            Archivos_Cortados++;
                                        }
                                        else
                                        {
                                            Lector.Close();
                                            Lector.Dispose();
                                            Lector = null;
                                        }
                                    }
                                    else // The file is an image, so never encrypt it or cut it.
                                    {
                                        Imagen.Dispose();
                                        Imagen = null;
                                        Lector.Close();
                                        Lector.Dispose();
                                        Lector = null;
                                        Archivos_Imágenes++;
                                    }
                                }
                            }
                            MessageBox.Show(this, "Number of existing secret files: " + Program.Traducir_Número(Archivos_Archivos) + ".\r\nNumber of existing secret images: " + Program.Traducir_Número(Archivos_Imágenes) + ".\r\n\r\nSecret files successfully encrypted: " + Program.Traducir_Número(Archivos_Encriptados) + ".\r\nSecret files successfully cutted: " + Program.Traducir_Número(Archivos_Cortados) + "." + (Archivos_Encriptados > 0 ? "\r\n\r\nThe encrypted files haven't been cutted yet, to access it's CRC32 values.\r\nPlease after copying that CRC32 values make a second pass here." : null), Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else if ((Control.ModifierKeys & Keys.Shift) != Keys.Shift)
                {
                    Temporizador_Principal.Stop();
                    Registro_Restablecer_Opciones();
                    Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                    Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.Secrets;
                    Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                    Ventana.ShowDialog(this);
                    Ventana.Dispose();
                    Ventana = null;
                    Registro_Guardar_Opciones();
                    Temporizador_Principal.Start();
                }
                else
                {
                    Temporizador_Principal.Stop();
                    Registro_Restablecer_Opciones();
                    Barra_Estado_Botón_Secretos.Text = "Secrets: visible";
                    Ventana_Secretos Ventana = new Ventana_Secretos();
                    Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                    Ventana.ShowDialog(this);
                    Ventana.Dispose();
                    Ventana = null;
                    Barra_Estado_Botón_Secretos.Text = "Secrets: hidden";
                    Registro_Guardar_Opciones();
                    Temporizador_Principal.Start();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_Splash_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.None)
                {
                    Variable_Alfabeto_Galáctico = !Variable_Alfabeto_Galáctico;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ayuda_Reddit_Xisumavoid_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.reddit.com/r/Minecraft/comments/852eoc/minecraft_113_chunk_format_fully_decoded/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ayuda_Licencia_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.gnu.org/licenses/gpl-3.0.html", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ayuda_GitHub_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://github.com/Jupisoft111/Minecraft-Tools", ProcessWindowStyle.Normal);

                //SevenZip.SevenZipBase.
                //ICSharpCode.SharpZipLib.Zip.ZipFile.Create(0).TestArchive(0, ICSharpCode.SharpZipLib.Zip.TestStrategy.

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_Jupisoft_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //if (e.Button != MouseButtons.Right)
                {
                    Menú_Principal_Herramientas_Selector_Herramientas.PerformClick();
                    /*Registro_Restablecer_Opciones();
                    Ventana_Acerca Ventana = new Ventana_Acerca();
                    DialogResult Resultado = Ventana.ShowDialog(this);
                    Ventana.Dispose();
                    Ventana = null;
                    Registro_Guardar_Opciones();*/
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ayuda_Registro_Cambios_Click(object sender, EventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
                Registro_Restablecer_Opciones();
                Ventana_Registro_Cambios Ventana = new Ventana_Registro_Cambios();
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
                Registro_Guardar_Opciones();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Herramientas_Selector_Herramientas_Click(object sender, EventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
                Registro_Restablecer_Opciones();
                Ventana_Selector_Herramientas Ventana = new Ventana_Selector_Herramientas();
                Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                if (Ventana.ShowDialog(this) == DialogResult.OK && Ventana.Índice_Herramienta > -1)
                {
                    Índice_Herramienta_Anterior = Ventana.Índice_Herramienta;
                    Ventana.Dispose();
                    Ventana = null;
                    Ventana_Selector_Herramientas.Herramientas.Ejecutar_Herramienta(Índice_Herramienta_Anterior, Variable_Siempre_Visible, this);
                    Registro_Guardar_Opciones();
                    Temporizador_Principal.Start();
                }
                else
                {
                    Ventana.Dispose();
                    Ventana = null;
                    Registro_Guardar_Opciones();
                    Temporizador_Principal.Start();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_Minecraft_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                //if (e.Button != MouseButtons.Right)
                {
                    Program.Ejecutar_Ruta("https://www.minecraft.net/", ProcessWindowStyle.Normal);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_Mojang_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                //if (e.Button != MouseButtons.Right)
                {
                    Program.Ejecutar_Ruta("https://mojang.com", ProcessWindowStyle.Normal);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Barra_Estado_Botón_Secretos_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    Temporizador_Principal.Stop();
                    Registro_Restablecer_Opciones();
                    Barra_Estado_Botón_Secretos.Text = "Secrets: visible";
                    Ventana_Secretos Ventana = new Ventana_Secretos();
                    Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                    Ventana.ShowDialog(this);
                    Ventana.Dispose();
                    Ventana = null;
                    Barra_Estado_Botón_Secretos.Text = "Secrets: hidden";
                    Registro_Guardar_Opciones();
                    Temporizador_Principal.Start();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Herramientas_Herramienta_Predeterminada_Click(object sender, EventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
                Registro_Restablecer_Opciones();
                Ventana_Selector_Herramientas Ventana = new Ventana_Selector_Herramientas();
                Ventana.Seleccionar_Herramienta_Inicio = true;
                Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                if (Ventana.ShowDialog(this) == DialogResult.OK)
                {
                    Variable_Herramienta = Ventana.Índice_Herramienta;
                    Ventana.Dispose();
                    Ventana = null;
                    Registro_Guardar_Opciones();
                    Temporizador_Principal.Start();
                }
                else
                {
                    Ventana.Dispose();
                    Ventana = null;
                    Registro_Guardar_Opciones();
                    Temporizador_Principal.Start();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Herramientas_Abrir_Última_Click(object sender, EventArgs e)
        {
            try
            {
                if (Índice_Herramienta_Anterior > -1)
                {
                    Ventana_Selector_Herramientas.Herramientas.Ejecutar_Herramienta(Índice_Herramienta_Anterior, Variable_Siempre_Visible, this);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ayuda_Minecraft_Forum_Jupisoft_Eliminado_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.minecraftforum.net/forums/minecraft-java-edition/recent-updates-and-snapshots/2894808-minecraft-1-13-new-chunk-format-fully-decoded-read", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal static bool Variable_Modo_Oscuro = false;

        internal void Modo_Oscuro_Invertir_ToolStripItem(ToolStripItem Menú)
        {
            try
            {
                Menú.BackColor = Program.Negativizar_Color(Menú.BackColor);
                Menú.ForeColor = Program.Negativizar_Color(Menú.ForeColor);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Modo_Oscuro_Invertir_ToolStripSeparator(ToolStripSeparator Menú)
        {
            try
            {
                Menú.BackColor = Program.Negativizar_Color(Menú.BackColor);
                //Menú.ForeColor = Program.Negativizar_Color(Menú.ForeColor);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Modo_Oscuro_Invertir_ToolStripMenuItem(ToolStripMenuItem Menú)
        {
            try
            {
                Menú.BackColor = Program.Negativizar_Color(Menú.BackColor);
                Menú.ForeColor = Program.Negativizar_Color(Menú.ForeColor);
                if (Menú.DropDownItems != null && Menú.DropDownItems.Count > 0)
                {
                    foreach (ToolStripItem Submenú in Menú.DropDownItems)
                    {
                        if (Submenú.GetType() == typeof(ToolStripMenuItem))
                        {
                            Modo_Oscuro_Invertir_ToolStripMenuItem((ToolStripMenuItem)Submenú);
                        }
                        else if (Submenú.GetType() == typeof(ToolStripSeparator))
                        {
                            Modo_Oscuro_Invertir_ToolStripSeparator((ToolStripSeparator)Submenú);
                        }
                        else
                        {
                            Modo_Oscuro_Invertir_ToolStripItem(Submenú);
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Modo_Oscuro_Invertir_ToolStripMenuItem_Excluido(ToolStripMenuItem Menú)
        {
            try
            {
                //Menú.BackColor = Program.Negativizar_Color(Menú.BackColor);
                Menú.ForeColor = Program.Negativizar_Color(Menú.ForeColor);
                if (Menú.DropDownItems != null && Menú.DropDownItems.Count > 0)
                {
                    foreach (ToolStripItem Submenú in Menú.DropDownItems)
                    {
                        if (Submenú.GetType() == typeof(ToolStripMenuItem))
                        {
                            Modo_Oscuro_Invertir_ToolStripMenuItem((ToolStripMenuItem)Submenú);
                        }
                        else if (Submenú.GetType() == typeof(ToolStripSeparator))
                        {
                            Modo_Oscuro_Invertir_ToolStripSeparator((ToolStripSeparator)Submenú);
                        }
                        else
                        {
                            Modo_Oscuro_Invertir_ToolStripItem(Submenú);
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Principal_Ver_Modo_Oscuro_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Modo_Oscuro = Menú_Principal_Ver_Modo_Oscuro.Checked;
                if (!Variable_Modo_Oscuro)
                {
                    Menú_Principal.BackColor = default(Color);
                    Barra_Estado.BackColor = default(Color);
                }
                else
                {
                    Menú_Principal.BackColor = SystemColors.MenuBar;
                    Barra_Estado.BackColor = SystemColors.MenuBar;
                    Menú_Principal.BackColor = Program.Negativizar_Color(Menú_Principal.BackColor);
                    Barra_Estado.BackColor = Program.Negativizar_Color(Barra_Estado.BackColor);
                }
                Modo_Oscuro_Invertir_ToolStripMenuItem_Excluido(Menú_Principal_Archivo);
                Modo_Oscuro_Invertir_ToolStripMenuItem_Excluido(Menú_Principal_David_Maeso);
                Modo_Oscuro_Invertir_ToolStripMenuItem_Excluido(Menú_Principal_Fratelli_Stellari);
                Modo_Oscuro_Invertir_ToolStripMenuItem_Excluido(Menú_Principal_Jupisoft);
                Modo_Oscuro_Invertir_ToolStripMenuItem_Excluido(Menú_Principal_Hermitcraft);
                Modo_Oscuro_Invertir_ToolStripMenuItem_Excluido(Menú_Principal_Ver);
                Modo_Oscuro_Invertir_ToolStripMenuItem_Excluido(Menú_Principal_Herramientas);
                Modo_Oscuro_Invertir_ToolStripMenuItem_Excluido(Menú_Principal_Ayuda);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Generates a new background image to be displayed as a mosaic in the main window of the application. The idea is based on the Minecraft launcher which seems to use the stone texture repeated several times with a zoom of 16x, although the original image contains several errors in the square borders, and because of that, this new code was designed to have a technically perfect background image.
        /// </summary>
        internal void Crear_Imagen_Mosaico_Fondo()
        {
            try
            {
                Bitmap Imagen = Resources.minecraft_dirt; //.minecraft_stone;
                BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, 16, 16), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * 16];
                Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                byte Rojo, Verde, Azul;
                double Matiz_Original, Saturación_Original, Luminosidad_Original;
                double Matiz, Saturación, Luminosidad;
                //List<byte> Lista_Azul = new List<byte>(new byte[] { 41, 58, 68, 74, 92, 108, 135 }); // Blue values in a dirt block texture.
                for (int Y = 0, Índice = 0; Y < 16; Y++)
                {
                    for (int X = 0; X < 16; X++, Índice += 4)
                    {
                        /*if (!Lista_Azul.Contains(Matriz_Bytes[Índice]))
                        {
                            Lista_Azul.Add(Matriz_Bytes[Índice]);
                            MessageBox.Show(Matriz_Bytes[Índice].ToString());
                        }*/
                        Color Color_ARGB = Color.FromArgb(255, Matriz_Bytes[Índice + 2], Matriz_Bytes[Índice + 1], Matriz_Bytes[Índice]);
                        Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz_Original, out Saturación_Original, out Luminosidad_Original);
                        /*int Porcentaje = Program.Rand.Next(1, 101);
                        if (Porcentaje <= 35)
                        {
                            Program.HSL.From_RGB(255, 0, 0, out Matiz, out Saturación, out Luminosidad);
                        }
                        else if (Porcentaje <= 70)
                        {
                            Program.HSL.From_RGB(255, 0, 0, out Matiz, out Saturación, out Luminosidad);
                        }
                        else if (Porcentaje <= 90)
                        {
                            Program.HSL.From_RGB(255, 0, 160, out Matiz, out Saturación, out Luminosidad);
                        }
                        else// if (Porcentaje <= 100)
                        {
                            Program.HSL.From_RGB(255, 255, 0, out Matiz, out Saturación, out Luminosidad);
                        }
                        /*if (Matriz_Bytes[Índice] <= 104)
                        {
                            Program.HSL.From_RGB(255, 0, 0, out Matiz, out Saturación, out Luminosidad);
                        }
                        else if (Matriz_Bytes[Índice] <= 116)
                        {
                            Program.HSL.From_RGB(255, 0, 0, out Matiz, out Saturación, out Luminosidad);
                        }
                        else if (Matriz_Bytes[Índice] <= 128)
                        {
                            Program.HSL.From_RGB(255, 0, 160, out Matiz, out Saturación, out Luminosidad);
                        }
                        else// if (Matriz_Bytes[Índice] <= 143)
                        {
                            Program.HSL.From_RGB(255, 255, 0, out Matiz, out Saturación, out Luminosidad);
                        }*/
                        if (Matriz_Bytes[Índice] <= 41)
                        {
                            Program.HSL.From_RGB(255, 255, 0, out Matiz, out Saturación, out Luminosidad);
                        }
                        else if (Matriz_Bytes[Índice] <= 58)
                        {
                            Program.HSL.From_RGB(255, 0, 0, out Matiz, out Saturación, out Luminosidad);
                        }
                        else if (Matriz_Bytes[Índice] <= 68)
                        {
                            Program.HSL.From_RGB(255, 255, 0, out Matiz, out Saturación, out Luminosidad);
                        }
                        else if (Matriz_Bytes[Índice] <= 74)
                        {
                            Program.HSL.From_RGB(255, 0, 160, out Matiz, out Saturación, out Luminosidad);
                        }
                        else if (Matriz_Bytes[Índice] <= 92)
                        {
                            Program.HSL.From_RGB(255, 0, 0, out Matiz, out Saturación, out Luminosidad);
                        }
                        else if (Matriz_Bytes[Índice] <= 108)
                        {
                            Program.HSL.From_RGB(255, 255, 0, out Matiz, out Saturación, out Luminosidad);
                        }
                        else// if (Matriz_Bytes[Índice] <= 135)
                        {
                            Program.HSL.From_RGB(255, 255, 0, out Matiz, out Saturación, out Luminosidad);
                        }
                        Program.HSL.To_RGB(Matiz, Saturación, (Luminosidad + Luminosidad_Original) / 2, out Rojo, out Verde, out Azul);
                        Matriz_Bytes[Índice + 3] = 192;
                        Matriz_Bytes[Índice + 2] = Rojo;
                        Matriz_Bytes[Índice + 1] = Verde;
                        Matriz_Bytes[Índice] = Azul;
                    }
                }
                Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                Imagen.UnlockBits(Bitmap_Data);
                Bitmap_Data = null;
                Matriz_Bytes = null;
                Imagen = Program.Obtener_Imagen_Miniatura(Imagen, 256, 256, true, false, CheckState.Checked);
                Program.Guardar_Imagen_Temporal(Imagen, "bg");
                /*Bitmap Imagen_Mosaico = new Bitmap(512, 512, PixelFormat.Format32bppArgb);
                Graphics Pintar = Graphics.FromImage(Imagen_Mosaico);
                Pintar.CompositingMode = CompositingMode.SourceCopy;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.None;

                Pintar.DrawImage(Imagen, new Rectangle(0, 0, 256, 256), new Rectangle(0, 0, 256, 256), GraphicsUnit.Pixel);

                Imagen.RotateFlip(RotateFlipType.Rotate90FlipNone);
                Pintar.DrawImage(Imagen, new Rectangle(256, 0, 256, 256), new Rectangle(0, 0, 256, 256), GraphicsUnit.Pixel);

                Imagen.RotateFlip(RotateFlipType.Rotate90FlipNone);
                Pintar.DrawImage(Imagen, new Rectangle(256, 256, 256, 256), new Rectangle(0, 0, 256, 256), GraphicsUnit.Pixel);

                Imagen.RotateFlip(RotateFlipType.Rotate90FlipNone);
                Pintar.DrawImage(Imagen, new Rectangle(0, 256, 256, 256), new Rectangle(0, 0, 256, 256), GraphicsUnit.Pixel);

                Pintar.Dispose();
                Pintar = null;
                Program.Guardar_Imagen_Temporal(Imagen_Mosaico, "bg");*/
                /*Bitmap Imagen_Mosaico = new Bitmap(512, 512, PixelFormat.Format32bppArgb);
                Graphics Pintar = Graphics.FromImage(Imagen_Mosaico);
                Pintar.CompositingMode = CompositingMode.SourceCopy;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.None;
                TextureBrush Pincel = new TextureBrush(Imagen, WrapMode.TileFlipXY, new Rectangle(0, 0, 256, 256));
                Pintar.FillRectangle(Pincel, 0, 0, 512, 512);
                Pintar.Dispose();
                Pintar = null;
                Program.Guardar_Imagen_Temporal(Imagen_Mosaico, "bg");*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Function to help with the developing of Minecraft 3D Simulator for Unity.
        /// This generates empty textures for all the known blocks as a 3D blocks (6 sides).
        /// </summary>
        internal void Minecraft_3D_Textures()
        {
            try
            {
                /*Barra_Estado_Etiqueta_Negro.ForeColor = */
                Minecraft_Source_Code_Old.Test_Color();
                return;
                // Start a sorted dictionary with all the Minecraft 1.14.4 known block names.
                SortedDictionary<string, List<string>>  Diccionario_Bloques = new SortedDictionary<string, List<string>>();
                Diccionario_Bloques.Add("acacia_button", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("acacia_door", new List<string>(new string[] { "half_upper" }));
                Diccionario_Bloques.Add("acacia_fence", new List<string>(new string[] { "waterlogged_true" }));
                Diccionario_Bloques.Add("acacia_fence_gate", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("acacia_leaves", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("acacia_log", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("acacia_planks", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("acacia_pressure_plate", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("acacia_sapling", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("acacia_sign", new List<string>(new string[] { })); // ...
                Diccionario_Bloques.Add("acacia_slab", new List<string>(new string[] { "type_double", "type_top", "waterlogged_true" }));
                Diccionario_Bloques.Add("acacia_stairs", new List<string>(new string[] { "half_top", "waterlogged_true" }));
                Diccionario_Bloques.Add("acacia_trapdoor", new List<string>(new string[] { "waterlogged_true" }));
                Diccionario_Bloques.Add("acacia_wall_sign", new List<string>(new string[] { })); // ...
                Diccionario_Bloques.Add("acacia_wood", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("activator_rail", new List<string>(new string[] { "powered_true" }));
                Diccionario_Bloques.Add("air", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("allium", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("andesite", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("andesite_slab", new List<string>(new string[] { "type_double", "type_top", "waterlogged_true" }));
                Diccionario_Bloques.Add("andesite_stairs", new List<string>(new string[] { "half_top", "waterlogged_true" }));
                Diccionario_Bloques.Add("andesite_wall", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("anvil", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("attached_melon_stem", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("attached_pumpkin_stem", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("azure_bluet", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("bamboo", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("bamboo_sapling", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("barrel", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("barrier", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("beacon", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("bedrock", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("beetroots", new List<string>(new string[] { "age_0", "age_1", "age_2", "age_3" }));
                Diccionario_Bloques.Add("bell", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("birch_button", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("birch_door", new List<string>(new string[] { "half_upper" }));
                Diccionario_Bloques.Add("birch_fence", new List<string>(new string[] { "waterlogged_true" }));
                Diccionario_Bloques.Add("birch_fence_gate", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("birch_leaves", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("birch_log", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("birch_planks", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("birch_pressure_plate", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("birch_sapling", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("birch_sign", new List<string>(new string[] { })); // ...
                Diccionario_Bloques.Add("birch_slab", new List<string>(new string[] { "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("birch_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("birch_trapdoor", new List<string>(new string[] { "waterlogged_true" }));
                Diccionario_Bloques.Add("birch_wall_sign", new List<string>(new string[] { })); // ...
                Diccionario_Bloques.Add("birch_wood", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("black_banner", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("black_bed", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "occupied_false", "occupied_true", "part_foot", "part_head" }));
                Diccionario_Bloques.Add("black_carpet", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("black_concrete", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("black_concrete_powder", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("black_glazed_terracotta", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("black_shulker_box", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("black_stained_glass", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("black_stained_glass_pane", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("black_terracotta", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("black_wall_banner", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("black_wool", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("blast_furnace", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("blue_banner", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("blue_bed", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "occupied_false", "occupied_true", "part_foot", "part_head" }));
                Diccionario_Bloques.Add("blue_carpet", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("blue_concrete", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("blue_concrete_powder", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("blue_glazed_terracotta", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("blue_ice", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("blue_orchid", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("blue_shulker_box", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("blue_stained_glass", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("blue_stained_glass_pane", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("blue_terracotta", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("blue_wall_banner", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("blue_wool", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("bone_block", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("bookshelf", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("brain_coral", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("brain_coral_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("brain_coral_fan", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("brain_coral_wall_fan", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("brewing_stand", new List<string>(new string[] { "has_bottle_0_false", "has_bottle_0_true", "has_bottle_1_false", "has_bottle_1_true", "has_bottle_2_false", "has_bottle_2_true" }));
                Diccionario_Bloques.Add("brick_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("brick_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("brick_wall", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("bricks", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("brown_banner", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("brown_bed", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "occupied_false", "occupied_true", "part_foot", "part_head" }));
                Diccionario_Bloques.Add("brown_carpet", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("brown_concrete", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("brown_concrete_powder", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("brown_glazed_terracotta", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("brown_mushroom", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("brown_mushroom_block", new List<string>(new string[] { "down_false", "down_true", "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "up_false", "up_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("brown_shulker_box", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("brown_stained_glass", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("brown_stained_glass_pane", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("brown_terracotta", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("brown_wall_banner", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("brown_wool", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("bubble_column", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("bubble_coral", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("bubble_coral_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("bubble_coral_fan", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("bubble_coral_wall_fan", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("cactus", new List<string>(new string[] { "age_0", "age_1", "age_2", "age_3", "age_4", "age_5", "age_6", "age_7", "age_8", "age_9", "age_10", "age_11", "age_12", "age_13", "age_14", "age_15" }));
                Diccionario_Bloques.Add("cake", new List<string>(new string[] { "bites_0", "bites_1", "bites_2", "bites_3", "bites_4", "bites_5", "bites_6" }));
                Diccionario_Bloques.Add("campfire", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("carrots", new List<string>(new string[] { "age_0", "age_1", "age_2", "age_3", "age_4", "age_5", "age_6", "age_7" }));
                Diccionario_Bloques.Add("cartography_table", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("carved_pumpkin", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("cauldron", new List<string>(new string[] { "level_0", "level_1", "level_2", "level_3" }));
                Diccionario_Bloques.Add("cave_air", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("chain_command_block", new List<string>(new string[] { "conditional_false", "conditional_true", "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("chest", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "type_left", "type_right", "type_single", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("chipped_anvil", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("chiseled_quartz_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("chiseled_red_sandstone", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("chiseled_sandstone", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("chiseled_stone_bricks", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("chorus_flower", new List<string>(new string[] { "age_0", "age_1", "age_2", "age_3", "age_4", "age_5" }));
                Diccionario_Bloques.Add("chorus_plant", new List<string>(new string[] { "down_false", "down_true", "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "up_false", "up_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("clay", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("coal_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("coal_ore", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("coarse_dirt", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("cobblestone", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("cobblestone_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("cobblestone_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("cobblestone_wall", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "up_false", "up_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("cobweb", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("cocoa", new List<string>(new string[] { "age_0", "age_1", "age_2", "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("command_block", new List<string>(new string[] { "conditional_false", "conditional_true", "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("comparator", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "mode_compare", "mode_subtract", "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("composter", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("conduit", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("cornflower", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("cracked_stone_bricks", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("crafting_table", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("creeper_head", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("creeper_wall_head", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("cut_red_sandstone", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("cut_red_sandstone_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("cut_sandstone", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("cut_sandstone_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("cyan_banner", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("cyan_bed", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "occupied_false", "occupied_true", "part_foot", "part_head" }));
                Diccionario_Bloques.Add("cyan_carpet", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("cyan_concrete", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("cyan_concrete_powder", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("cyan_glazed_terracotta", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("cyan_shulker_box", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("cyan_stained_glass", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("cyan_stained_glass_pane", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("cyan_terracotta", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("cyan_wall_banner", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("cyan_wool", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("damaged_anvil", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("dandelion", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dark_oak_button", new List<string>(new string[] { "face_ceiling", "face_floor", "face_wall", "facing_east", "facing_north", "facing_south", "facing_west", "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("dark_oak_door", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_lower", "half_upper", "hinge_left", "hinge_right", "open_false", "open_true", "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("dark_oak_fence", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("dark_oak_fence_gate", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "in_wall_false", "in_wall_true", "open_false", "open_true", "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("dark_oak_leaves", new List<string>(new string[] { "distance_1", "distance_2", "distance_3", "distance_4", "distance_5", "distance_6", "distance_7", "persistent_false", "persistent_true" }));
                Diccionario_Bloques.Add("dark_oak_log", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("dark_oak_planks", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dark_oak_pressure_plate", new List<string>(new string[] { "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("dark_oak_sapling", new List<string>(new string[] { "stage_0", "stage_1" }));
                Diccionario_Bloques.Add("dark_oak_sign", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dark_oak_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("dark_oak_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("dark_oak_trapdoor", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "open_false", "open_true", "powered_false", "powered_true", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("dark_oak_wall_sign", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dark_oak_wood", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("dark_prismarine", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dark_prismarine_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("dark_prismarine_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("daylight_detector", new List<string>(new string[] { "inverted_false", "inverted_true", "power_0", "power_1", "power_2", "power_3", "power_4", "power_5", "power_6", "power_7", "power_8", "power_9", "power_10", "power_11", "power_12", "power_13", "power_14", "power_15" }));
                Diccionario_Bloques.Add("dead_brain_coral", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dead_brain_coral_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dead_brain_coral_fan", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dead_brain_coral_wall_fan", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dead_bubble_coral", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dead_bubble_coral_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dead_bubble_coral_fan", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dead_bubble_coral_wall_fan", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dead_bush", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dead_fire_coral", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dead_fire_coral_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dead_fire_coral_fan", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dead_fire_coral_wall_fan", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dead_horn_coral", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dead_horn_coral_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dead_horn_coral_fan", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dead_horn_coral_wall_fan", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dead_tube_coral", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dead_tube_coral_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dead_tube_coral_fan", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dead_tube_coral_wall_fan", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("detector_rail", new List<string>(new string[] { "powered_false", "powered_true", "shape_ascending_east", "shape_ascending_west", "shape_ascending_north", "shape_ascending_south", "shape_east_west", "shape_north_south" }));
                Diccionario_Bloques.Add("diamond_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("diamond_ore", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("diorite", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("diorite_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("diorite_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("diorite_wall", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dirt", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dispenser", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west", "triggered_false", "triggered_true" }));
                Diccionario_Bloques.Add("dragon_egg", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dragon_head", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("dragon_wall_head", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("dried_kelp_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("dropper", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west", "triggered_false", "triggered_true" }));
                Diccionario_Bloques.Add("emerald_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("emerald_ore", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("enchanting_table", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("end_gateway", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("end_portal", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("end_portal_frame", new List<string>(new string[] { "eye_false", "eye_true", "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("end_rod", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("end_stone", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("end_stone_brick_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("end_stone_brick_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("end_stone_brick_wall", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("end_stone_bricks", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("ender_chest", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("farmland", new List<string>(new string[] { "moisture_0", "moisture_1", "moisture_2", "moisture_3", "moisture_4", "moisture_5", "moisture_6", "moisture_7" }));
                Diccionario_Bloques.Add("fern", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("fire", new List<string>(new string[] { "age_0", "age_1", "age_2", "age_3", "age_4", "age_5", "age_6", "age_7", "age_8", "age_9", "age_10", "age_11", "age_12", "age_13", "age_14", "age_15", "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "up_false", "up_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("fire_coral", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("fire_coral_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("fire_coral_fan", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("fire_coral_wall_fan", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("fletching_table", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("flower_pot", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("frosted_ice", new List<string>(new string[] { "age_0", "age_1", "age_2", "age_3" }));
                Diccionario_Bloques.Add("furnace", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "lit_false", "lit_true" }));
                Diccionario_Bloques.Add("glass", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("glass_pane", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("glowstone", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("gold_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("gold_ore", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("granite", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("granite_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("granite_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("granite_wall", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("grass", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("grass_block", new List<string>(new string[] { "snowy_false", "snowy_true" }));
                Diccionario_Bloques.Add("grass_path", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("gravel", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("gray_banner", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("gray_bed", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "occupied_false", "occupied_true", "part_foot", "part_head" }));
                Diccionario_Bloques.Add("gray_carpet", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("gray_concrete", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("gray_concrete_powder", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("gray_glazed_terracotta", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("gray_shulker_box", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("gray_stained_glass", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("gray_stained_glass_pane", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("gray_terracotta", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("gray_wall_banner", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("gray_wool", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("green_banner", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("green_bed", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "occupied_false", "occupied_true", "part_foot", "part_head" }));
                Diccionario_Bloques.Add("green_carpet", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("green_concrete", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("green_concrete_powder", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("green_glazed_terracotta", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("green_shulker_box", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("green_stained_glass", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("green_stained_glass_pane", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("green_terracotta", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("green_wall_banner", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("green_wool", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("grindstone", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("hay_block", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("heavy_weighted_pressure_plate", new List<string>(new string[] { "power_0", "power_1", "power_2", "power_3", "power_4", "power_5", "power_6", "power_7", "power_8", "power_9", "power_10", "power_11", "power_12", "power_13", "power_14", "power_15" }));
                Diccionario_Bloques.Add("hopper", new List<string>(new string[] { "enabled_false", "enabled_true", "facing_down", "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("horn_coral", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("horn_coral_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("horn_coral_fan", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("horn_coral_wall_fan", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("ice", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("infested_chiseled_stone_bricks", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("infested_cobblestone", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("infested_cracked_stone_bricks", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("infested_mossy_stone_bricks", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("infested_stone", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("infested_stone_bricks", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("iron_bars", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("iron_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("iron_door", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_lower", "half_upper", "hinge_left", "hinge_right", "open_false", "open_true", "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("iron_ore", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("iron_trapdoor", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "open_false", "open_true", "powered_false", "powered_true", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("jack_o_lantern", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("jigsaw", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("jukebox", new List<string>(new string[] { "has_record_false", "has_record_true" }));
                Diccionario_Bloques.Add("jungle_button", new List<string>(new string[] { "face_ceiling", "face_floor", "face_wall", "facing_east", "facing_north", "facing_south", "facing_west", "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("jungle_door", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_lower", "half_upper", "hinge_left", "hinge_right", "open_false", "open_true", "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("jungle_fence", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("jungle_fence_gate", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "in_wall_false", "in_wall_true", "open_false", "open_true", "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("jungle_leaves", new List<string>(new string[] { "distance_1", "distance_2", "distance_3", "distance_4", "distance_5", "distance_6", "distance_7", "persistent_false", "persistent_true" }));
                Diccionario_Bloques.Add("jungle_log", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("jungle_planks", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("jungle_pressure_plate", new List<string>(new string[] { "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("jungle_sapling", new List<string>(new string[] { "stage_0", "stage_1" }));
                Diccionario_Bloques.Add("jungle_sign", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("jungle_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("jungle_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("jungle_trapdoor", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "open_false", "open_true", "powered_false", "powered_true", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("jungle_wall_sign", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("jungle_wood", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("kelp", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("kelp_plant", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("ladder", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("lantern", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("lapis_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("lapis_ore", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("large_fern", new List<string>(new string[] { "half_lower", "half_upper" }));
                Diccionario_Bloques.Add("lava", new List<string>(new string[] { "level_0", "level_1", "level_2", "level_3", "level_4", "level_5", "level_6", "level_7", "level_8", "level_9", "level_10", "level_11", "level_12", "level_13", "level_14", "level_15" }));
                Diccionario_Bloques.Add("lectern", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("lever", new List<string>(new string[] { "face_ceiling", "face_floor", "face_wall", "facing_east", "facing_north", "facing_south", "facing_west", "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("light_blue_banner", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("light_blue_bed", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "occupied_false", "occupied_true", "part_foot", "part_head" }));
                Diccionario_Bloques.Add("light_blue_carpet", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("light_blue_concrete", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("light_blue_concrete_powder", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("light_blue_glazed_terracotta", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("light_blue_shulker_box", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("light_blue_stained_glass", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("light_blue_stained_glass_pane", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("light_blue_terracotta", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("light_blue_wall_banner", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("light_blue_wool", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("light_gray_banner", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("light_gray_bed", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "occupied_false", "occupied_true", "part_foot", "part_head" }));
                Diccionario_Bloques.Add("light_gray_carpet", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("light_gray_concrete", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("light_gray_concrete_powder", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("light_gray_glazed_terracotta", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("light_gray_shulker_box", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("light_gray_stained_glass", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("light_gray_stained_glass_pane", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("light_gray_terracotta", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("light_gray_wall_banner", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("light_gray_wool", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("light_weighted_pressure_plate", new List<string>(new string[] { "power_0", "power_1", "power_2", "power_3", "power_4", "power_5", "power_6", "power_7", "power_8", "power_9", "power_10", "power_11", "power_12", "power_13", "power_14", "power_15" }));
                Diccionario_Bloques.Add("lilac", new List<string>(new string[] { "half_lower", "half_upper" }));
                Diccionario_Bloques.Add("lily_of_the_valley", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("lily_pad", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("lime_banner", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("lime_bed", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "occupied_false", "occupied_true", "part_foot", "part_head" }));
                Diccionario_Bloques.Add("lime_carpet", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("lime_concrete", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("lime_concrete_powder", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("lime_glazed_terracotta", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("lime_shulker_box", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("lime_stained_glass", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("lime_stained_glass_pane", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("lime_terracotta", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("lime_wall_banner", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("lime_wool", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("loom", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("magenta_banner", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("magenta_bed", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "occupied_false", "occupied_true", "part_foot", "part_head" }));
                Diccionario_Bloques.Add("magenta_carpet", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("magenta_concrete", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("magenta_concrete_powder", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("magenta_glazed_terracotta", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("magenta_shulker_box", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("magenta_stained_glass", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("magenta_stained_glass_pane", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("magenta_terracotta", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("magenta_wall_banner", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("magenta_wool", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("magma_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("melon", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("melon_stem", new List<string>(new string[] { "age_0", "age_1", "age_2", "age_3", "age_4", "age_5", "age_6", "age_7" }));
                Diccionario_Bloques.Add("mossy_cobblestone", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("mossy_cobblestone_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("mossy_cobblestone_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("mossy_cobblestone_wall", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "up_false", "up_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("mossy_stone_brick_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("mossy_stone_brick_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("mossy_stone_brick_wall", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("mossy_stone_bricks", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("moving_piston", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west", "type_normal", "type_sticky" }));
                Diccionario_Bloques.Add("mushroom_stem", new List<string>(new string[] { "down_false", "down_true", "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "up_false", "up_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("mycelium", new List<string>(new string[] { "snowy_false", "snowy_true" }));
                Diccionario_Bloques.Add("nether_brick_fence", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("nether_brick_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("nether_brick_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("nether_brick_wall", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("nether_bricks", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("nether_portal", new List<string>(new string[] { "axis_x", "axis_z" }));
                Diccionario_Bloques.Add("nether_quartz_ore", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("nether_wart", new List<string>(new string[] { "age_0", "age_1", "age_2", "age_3" }));
                Diccionario_Bloques.Add("nether_wart_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("netherrack", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("note_block", new List<string>(new string[] { "instrument_basedrum", "instrument_banjo", "instrument_bass", "instrument_bit", "instrument_bell", "instrument_chime", "instrument_cow_bell", "instrument_didgeridoo", "instrument_flute", "instrument_guitar", "instrument_harp", "instrument_hat", "instrument_iron_xylophone", "instrument_pling", "instrument_snare", "instrument_xylophone", "note_0", "note_1", "note_2", "note_3", "note_4", "note_5", "note_6", "note_7", "note_8", "note_9", "note_10", "note_11", "note_12", "note_13", "note_14", "note_15", "note_16", "note_17", "note_18", "note_19", "note_20", "note_21", "note_22", "note_23", "note_24", "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("oak_button", new List<string>(new string[] { "face_ceiling", "face_floor", "face_wall", "facing_east", "facing_north", "facing_south", "facing_west", "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("oak_door", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_lower", "half_upper", "hinge_left", "hinge_right", "open_false", "open_true", "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("oak_fence", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("oak_fence_gate", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "in_wall_false", "in_wall_true", "open_false", "open_true", "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("oak_leaves", new List<string>(new string[] { "distance_1", "distance_2", "distance_3", "distance_4", "distance_5", "distance_6", "distance_7", "persistent_false", "persistent_true" }));
                Diccionario_Bloques.Add("oak_log", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("oak_planks", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("oak_pressure_plate", new List<string>(new string[] { "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("oak_sapling", new List<string>(new string[] { "stage_0", "stage_1" }));
                Diccionario_Bloques.Add("oak_sign", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("oak_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("oak_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("oak_trapdoor", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "open_false", "open_true", "powered_false", "powered_true", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("oak_wall_sign", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("oak_wood", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("observer", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west", "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("obsidian", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("orange_banner", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("orange_bed", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "occupied_false", "occupied_true", "part_foot", "part_head" }));
                Diccionario_Bloques.Add("orange_carpet", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("orange_concrete", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("orange_concrete_powder", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("orange_glazed_terracotta", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("orange_shulker_box", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("orange_stained_glass", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("orange_stained_glass_pane", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("orange_terracotta", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("orange_tulip", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("orange_wall_banner", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("orange_wool", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("oxeye_daisy", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("packed_ice", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("peony", new List<string>(new string[] { "half_lower", "half_upper" }));
                Diccionario_Bloques.Add("petrified_oak_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("pink_banner", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("pink_bed", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "occupied_false", "occupied_true", "part_foot", "part_head" }));
                Diccionario_Bloques.Add("pink_carpet", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("pink_concrete", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("pink_concrete_powder", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("pink_glazed_terracotta", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("pink_shulker_box", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("pink_stained_glass", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("pink_stained_glass_pane", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("pink_terracotta", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("pink_tulip", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("pink_wall_banner", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("pink_wool", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("piston", new List<string>(new string[] { "extended_false", "extended_true", "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("piston_head", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west", "short_false", "short_true", "type_normal", "type_sticky" }));
                Diccionario_Bloques.Add("player_head", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("player_wall_head", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("podzol", new List<string>(new string[] { "snowy_false", "snowy_true" }));
                Diccionario_Bloques.Add("polished_andesite", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("polished_andesite_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("polished_andesite_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("polished_diorite", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("polished_diorite_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("polished_diorite_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("polished_granite", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("polished_granite_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("polished_granite_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("poppy", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potatoes", new List<string>(new string[] { "age_0", "age_1", "age_2", "age_3", "age_4", "age_5", "age_6", "age_7" }));
                Diccionario_Bloques.Add("potted_acacia_sapling", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_allium", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_azure_bluet", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_bamboo", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_birch_sapling", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_blue_orchid", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_brown_mushroom", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_cactus", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_cornflower", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_dandelion", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_dark_oak_sapling", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_dead_bush", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_fern", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_jungle_sapling", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_lily_of_the_valley", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_oak_sapling", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_orange_tulip", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_oxeye_daisy", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_pink_tulip", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_poppy", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_red_mushroom", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_red_tulip", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_spruce_sapling", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_white_tulip", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("potted_wither_rose", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("powered_rail", new List<string>(new string[] { "powered_false", "powered_true", "shape_ascending_east", "shape_ascending_west", "shape_ascending_north", "shape_ascending_south", "shape_east_west", "shape_north_south" }));
                Diccionario_Bloques.Add("prismarine", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("prismarine_brick_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("prismarine_brick_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("prismarine_bricks", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("prismarine_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("prismarine_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("prismarine_wall", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("pumpkin", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("pumpkin_stem", new List<string>(new string[] { "age_0", "age_1", "age_2", "age_3", "age_4", "age_5", "age_6", "age_7" }));
                Diccionario_Bloques.Add("purple_banner", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("purple_bed", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "occupied_false", "occupied_true", "part_foot", "part_head" }));
                Diccionario_Bloques.Add("purple_carpet", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("purple_concrete", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("purple_concrete_powder", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("purple_glazed_terracotta", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("purple_shulker_box", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("purple_stained_glass", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("purple_stained_glass_pane", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("purple_terracotta", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("purple_wall_banner", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("purple_wool", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("purpur_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("purpur_pillar", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("purpur_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("purpur_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("quartz_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("quartz_pillar", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("quartz_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("quartz_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("rail", new List<string>(new string[] { "shape_ascending_east", "shape_ascending_west", "shape_ascending_north", "shape_ascending_south", "shape_east_west", "shape_north_east", "shape_north_west", "shape_north_south", "shape_south_east", "shape_south_west" }));
                Diccionario_Bloques.Add("red_banner", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("red_bed", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "occupied_false", "occupied_true", "part_foot", "part_head" }));
                Diccionario_Bloques.Add("red_carpet", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("red_concrete", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("red_concrete_powder", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("red_glazed_terracotta", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("red_mushroom", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("red_mushroom_block", new List<string>(new string[] { "down_false", "down_true", "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "up_false", "up_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("red_nether_brick_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("red_nether_brick_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("red_nether_brick_wall", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("red_nether_bricks", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("red_sand", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("red_sandstone", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("red_sandstone_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("red_sandstone_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("red_sandstone_wall", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("red_shulker_box", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("red_stained_glass", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("red_stained_glass_pane", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("red_terracotta", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("red_tulip", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("red_wall_banner", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("red_wool", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("redstone_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("redstone_lamp", new List<string>(new string[] { "lit_false", "lit_true" }));
                Diccionario_Bloques.Add("redstone_ore", new List<string>(new string[] { "lit_false", "lit_true" }));
                Diccionario_Bloques.Add("redstone_torch", new List<string>(new string[] { "lit_false", "lit_true" }));
                Diccionario_Bloques.Add("redstone_wall_torch", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "lit_false", "lit_true" }));
                Diccionario_Bloques.Add("redstone_wire", new List<string>(new string[] { "east_none", "east_side", "east_up", "north_none", "north_side", "north_up", "power_0", "power_1", "power_2", "power_3", "power_4", "power_5", "power_6", "power_7", "power_8", "power_9", "power_10", "power_11", "power_12", "power_13", "power_14", "power_15", "south_none", "south_side", "south_up", "west_none", "west_side", "west_up" }));
                Diccionario_Bloques.Add("repeater", new List<string>(new string[] { "delay_1", "delay_2", "delay_3", "delay_4", "facing_east", "facing_north", "facing_south", "facing_west", "locked_false", "locked_true", "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("repeating_command_block", new List<string>(new string[] { "conditional_false", "conditional_true", "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("rose_bush", new List<string>(new string[] { "half_lower", "half_upper" }));
                Diccionario_Bloques.Add("sand", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("sandstone", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("sandstone_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("sandstone_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("sandstone_wall", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("scaffolding", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("sea_lantern", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("sea_pickle", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("seagrass", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("shulker_box", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("skeleton_skull", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("skeleton_wall_skull", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("slime_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("smithing_table", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("smoker", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("smooth_quartz", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("smooth_quartz_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("smooth_quartz_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("smooth_red_sandstone", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("smooth_red_sandstone_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("smooth_red_sandstone_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("smooth_sandstone", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("smooth_sandstone_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("smooth_sandstone_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("smooth_stone", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("smooth_stone_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("snow", new List<string>(new string[] { "layers_1", "layers_2", "layers_3", "layers_4", "layers_5", "layers_6", "layers_7", "layers_8" }));
                Diccionario_Bloques.Add("snow_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("soul_sand", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("spawner", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("sponge", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("spruce_button", new List<string>(new string[] { "face_ceiling", "face_floor", "face_wall", "facing_east", "facing_north", "facing_south", "facing_west", "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("spruce_door", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_lower", "half_upper", "hinge_left", "hinge_right", "open_false", "open_true", "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("spruce_fence", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("spruce_fence_gate", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "in_wall_false", "in_wall_true", "open_false", "open_true", "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("spruce_leaves", new List<string>(new string[] { "distance_1", "distance_2", "distance_3", "distance_4", "distance_5", "distance_6", "distance_7", "persistent_false", "persistent_true" }));
                Diccionario_Bloques.Add("spruce_log", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("spruce_planks", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("spruce_pressure_plate", new List<string>(new string[] { "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("spruce_sapling", new List<string>(new string[] { "stage_0", "stage_1" }));
                Diccionario_Bloques.Add("spruce_sign", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("spruce_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("spruce_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("spruce_trapdoor", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "open_false", "open_true", "powered_false", "powered_true", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("spruce_wall_sign", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("spruce_wood", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("sticky_piston", new List<string>(new string[] { "extended_false", "extended_true", "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("stone", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("stone_brick_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("stone_brick_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("stone_brick_wall", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("stone_bricks", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("stone_button", new List<string>(new string[] { "face_ceiling", "face_floor", "face_wall", "facing_east", "facing_north", "facing_south", "facing_west", "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("stone_pressure_plate", new List<string>(new string[] { "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("stone_slab", new List<string>(new string[] { "type_bottom", "type_double", "type_top", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("stone_stairs", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "half_bottom", "half_top", "shape_inner_left", "shape_inner_right", "shape_outer_left", "shape_outer_right", "shape_straight", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("stonecutter", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("stripped_acacia_log", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("stripped_acacia_wood", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("stripped_birch_log", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("stripped_birch_wood", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("stripped_dark_oak_log", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("stripped_dark_oak_wood", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("stripped_jungle_log", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("stripped_jungle_wood", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("stripped_oak_log", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("stripped_oak_wood", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("stripped_spruce_log", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("stripped_spruce_wood", new List<string>(new string[] { "axis_x", "axis_y", "axis_z" }));
                Diccionario_Bloques.Add("structure_block", new List<string>(new string[] { "mode_corner", "mode_data", "mode_load", "mode_save" }));
                Diccionario_Bloques.Add("structure_void", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("sugar_cane", new List<string>(new string[] { "age_0", "age_1", "age_2", "age_3", "age_4", "age_5", "age_6", "age_7", "age_8", "age_9", "age_10", "age_11", "age_12", "age_13", "age_14", "age_15" }));
                Diccionario_Bloques.Add("sunflower", new List<string>(new string[] { "half_lower", "half_upper" }));
                Diccionario_Bloques.Add("sweet_berry_bush", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("tall_grass", new List<string>(new string[] { "half_lower", "half_upper" }));
                Diccionario_Bloques.Add("tall_seagrass", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("terracotta", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("tnt", new List<string>(new string[] { "unstable_false", "unstable_true" }));
                Diccionario_Bloques.Add("torch", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("trapped_chest", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "type_left", "type_right", "type_single", "waterlogged_false", "waterlogged_true" }));
                Diccionario_Bloques.Add("tripwire", new List<string>(new string[] { "attached_false", "attached_true", "disarmed_false", "disarmed_true", "east_false", "east_true", "north_false", "north_true", "powered_false", "powered_true", "south_false", "south_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("tripwire_hook", new List<string>(new string[] { "attached_false", "attached_true", "facing_east", "facing_north", "facing_south", "facing_west", "powered_false", "powered_true" }));
                Diccionario_Bloques.Add("tube_coral", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("tube_coral_block", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("tube_coral_fan", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("tube_coral_wall_fan", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("turtle_egg", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("vine", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "up_false", "up_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("void_air", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("wall_torch", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("water", new List<string>(new string[] { "level_0", "level_1", "level_2", "level_3", "level_4", "level_5", "level_6", "level_7", "level_8", "level_9", "level_10", "level_11", "level_12", "level_13", "level_14", "level_15" }));
                Diccionario_Bloques.Add("wet_sponge", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("wheat", new List<string>(new string[] { "age_0", "age_1", "age_2", "age_3", "age_4", "age_5", "age_6", "age_7" }));
                Diccionario_Bloques.Add("white_banner", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("white_bed", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "occupied_false", "occupied_true", "part_foot", "part_head" }));
                Diccionario_Bloques.Add("white_carpet", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("white_concrete", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("white_concrete_powder", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("white_glazed_terracotta", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("white_shulker_box", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("white_stained_glass", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("white_stained_glass_pane", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("white_terracotta", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("white_tulip", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("white_wall_banner", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("white_wool", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("wither_rose", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("wither_skeleton_skull", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("wither_skeleton_wall_skull", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("yellow_banner", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("yellow_bed", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west", "occupied_false", "occupied_true", "part_foot", "part_head" }));
                Diccionario_Bloques.Add("yellow_carpet", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("yellow_concrete", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("yellow_concrete_powder", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("yellow_glazed_terracotta", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("yellow_shulker_box", new List<string>(new string[] { "facing_down", "facing_east", "facing_north", "facing_south", "facing_up", "facing_west" }));
                Diccionario_Bloques.Add("yellow_stained_glass", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("yellow_stained_glass_pane", new List<string>(new string[] { "east_false", "east_true", "north_false", "north_true", "south_false", "south_true", "waterlogged_false", "waterlogged_true", "west_false", "west_true" }));
                Diccionario_Bloques.Add("yellow_terracotta", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("yellow_wall_banner", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));
                Diccionario_Bloques.Add("yellow_wool", new List<string>(new string[] { }));
                Diccionario_Bloques.Add("zombie_head", new List<string>(new string[] { "rotation_0", "rotation_1", "rotation_2", "rotation_3", "rotation_4", "rotation_5", "rotation_6", "rotation_7", "rotation_8", "rotation_9", "rotation_10", "rotation_11", "rotation_12", "rotation_13", "rotation_14", "rotation_15" }));
                Diccionario_Bloques.Add("zombie_wall_head", new List<string>(new string[] { "facing_east", "facing_north", "facing_south", "facing_west" }));

                // Create the output folder.
                string Ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + "\\Minecraft 3D Textures";
                Program.Crear_Carpetas(Ruta);

                // Now generate the default image for all the textures.
                Bitmap Imagen = new Bitmap(96, 16, PixelFormat.Format32bppArgb);
                Graphics Pintar = Graphics.FromImage(Imagen);
                Pintar.CompositingMode = CompositingMode.SourceCopy;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.None;
                Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                SolidBrush Pincel_Fondo = new SolidBrush(Color.FromArgb(255, 128, 0, 128)); //new SolidBrush(Color.FromArgb(255, 214, 127, 255));
                SolidBrush Pincel_Línea = new SolidBrush(Color.FromArgb(255, 107, 63, 127));
                for (int Índice_X = 0; Índice_X < 96; Índice_X += 16)
                {
                    Pintar.FillRectangle(Pincel_Línea, Índice_X, 0, 16, 16); // Outline.
                    Pintar.FillRectangle(Pincel_Fondo, Índice_X + 1, 1, 14, 14); // Inline.
                }
                Pincel_Fondo.Dispose();
                Pincel_Fondo = null;
                Pincel_Línea.Dispose();
                Pincel_Línea = null;
                Pintar.Dispose();
                Pintar = null;

                // Now save the same image for all the textures.
                foreach (KeyValuePair<string, List<string>> Entrada in Diccionario_Bloques)
                {
                    Imagen.Save(Ruta + "\\" + Entrada.Key + ".png", ImageFormat.Png);
                }
                Imagen.Dispose();
                Imagen = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
