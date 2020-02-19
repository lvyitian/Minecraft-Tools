using Minecraft_Tools.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    /// <summary>
    /// Class designed to copy several sections of the original Minecraft 1.12 source code, to use
    /// them for example to generate the exact leaves color based on a biome like Minecraft does.
    /// </summary>
    internal static class Minecraft_Source_Code_Old
    {
        // Test result: it worked perfectly, used to get the "plants" textures fully colored
        // instead of grayscale (Minecraft default), but using the default biomes for them.
        internal static void Test_Color()
        {
            try
            {
                return;

                string[] Matriz_Rutas = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + "\\Minecraft 3D Textures\\Split", "*.png", SearchOption.TopDirectoryOnly);
                if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                {
                    foreach (string Ruta_ in Matriz_Rutas)
                    {
                        Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta_, CheckState.Checked);
                        if (Imagen != null)
                        {
                            for (int Índice_X = 0, Índice = 0; Índice_X < 96; Índice_X += 16, Índice++)
                            {
                                Bitmap Imagen2 = Imagen.Clone(new Rectangle(Índice_X, 0, 16, 16), PixelFormat.Format32bppArgb);
                                Imagen2.Save(Path.GetDirectoryName(Ruta_) + "\\OK\\" + Path.GetFileNameWithoutExtension(Ruta_) + Índice.ToString() + ".png", ImageFormat.Png);
                                Imagen2.Dispose();
                                Imagen2 = null;
                            }
                            Imagen.Dispose();
                            Imagen = null;
                        }
                    }
                }

                return;

                // Generate stairs and slabs 3D textures from the source blocks.
                Matriz_Rutas = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + "\\Minecraft 3D Textures\\Test", "*.png", SearchOption.TopDirectoryOnly);
                if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                {
                    /*List<string> Lista_Rutas = new List<string>();
                    foreach (string Ruta_ in Matriz_Rutas)
                    {
                        string Ruta_Temporal = null;
                        if (Ruta_.ToLowerInvariant().EndsWith("_stairs.png"))
                        {
                            Ruta_Temporal = Path.GetFileName(Ruta_);
                            Ruta_Temporal = Ruta_Temporal.Substring(0, Ruta_Temporal.Length - "_stairs.png".Length);
                            Ruta_Temporal = Path.GetDirectoryName(Ruta_) + "\\" + Ruta_Temporal + ".png";
                        }
                        else if (Ruta_.ToLowerInvariant().EndsWith("_slab.png"))
                        {
                            Ruta_Temporal = Path.GetFileName(Ruta_);
                            Ruta_Temporal = Ruta_Temporal.Substring(0, Ruta_Temporal.Length - "_slab.png".Length);
                            Ruta_Temporal = Path.GetDirectoryName(Ruta_) + "\\" + Ruta_Temporal + ".png";
                        }

                        if (!string.IsNullOrEmpty(Ruta_Temporal))
                        {
                            if (!Lista_Rutas.Contains(Ruta_.ToLowerInvariant()))
                            {
                                Lista_Rutas.Add(Ruta_.ToLowerInvariant()); // Add the full block names to the list.
                            }
                        }
                    }*/
                    foreach (string Ruta_ in Matriz_Rutas)
                    {
                        Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta_, CheckState.Checked);
                        if (Imagen != null)
                        {
                            Bitmap Imagen_stairs = (Bitmap)Imagen.Clone();
                            Bitmap Imagen_slab = (Bitmap)Imagen.Clone();
                            Imagen = Filtrar_Imagen(Imagen, 75); //  Get a darker image to act like a shadow (75 %).

                            Graphics Pintar_stairs = Graphics.FromImage(Imagen_stairs);
                            Pintar_stairs.CompositingMode = CompositingMode.SourceCopy;
                            Pintar_stairs.CompositingQuality = CompositingQuality.HighQuality;
                            Pintar_stairs.InterpolationMode = InterpolationMode.NearestNeighbor;
                            Pintar_stairs.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Pintar_stairs.SmoothingMode = SmoothingMode.None;
                            Pintar_stairs.TextRenderingHint = TextRenderingHint.AntiAlias;

                            Pintar_stairs.FillRectangle(Brushes.Transparent, 0, 0, 8, 8);
                            Pintar_stairs.FillRectangle(Brushes.Transparent, 24, 0, 8, 8);
                            Pintar_stairs.DrawImage(Imagen, new Rectangle(32, 8, 16, 8), new Rectangle(32, 8, 16, 8), GraphicsUnit.Pixel);
                            Pintar_stairs.DrawImage(Imagen, new Rectangle(64, 0, 16, 8), new Rectangle(64, 0, 16, 8), GraphicsUnit.Pixel);
                            Pintar_stairs.Dispose();
                            Pintar_stairs = null;
                            Imagen_stairs.Save(Path.GetDirectoryName(Ruta_) + "\\" + Path.GetFileNameWithoutExtension(Ruta_) + "_stairs.png", ImageFormat.Png);
                            Imagen_stairs.Dispose();
                            Imagen_stairs = null;

                            Graphics Pintar_slab = Graphics.FromImage(Imagen_slab);
                            Pintar_slab.CompositingMode = CompositingMode.SourceCopy;
                            Pintar_slab.CompositingQuality = CompositingQuality.HighQuality;
                            Pintar_slab.InterpolationMode = InterpolationMode.NearestNeighbor;
                            Pintar_slab.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Pintar_slab.SmoothingMode = SmoothingMode.None;
                            Pintar_slab.TextRenderingHint = TextRenderingHint.AntiAlias;
                            Pintar_slab.FillRectangle(Brushes.Transparent, 0, 0, 32, 8);
                            Pintar_slab.FillRectangle(Brushes.Transparent, 64, 0, 32, 8);
                            Pintar_slab.DrawImage(Imagen, new Rectangle(32, 0, 16, 16), new Rectangle(32, 0, 16, 16), GraphicsUnit.Pixel);
                            Pintar_slab.Dispose();
                            Pintar_slab = null;
                            Imagen_slab.Save(Path.GetDirectoryName(Ruta_) + "\\" + Path.GetFileNameWithoutExtension(Ruta_) + "_slab.png", ImageFormat.Png);

                            Imagen.Dispose();
                            Imagen = null;
                            //Program.Eliminar_Archivo_Carpeta(Ruta_);
                        }
                    }
                    Matriz_Rutas = null;
                }

                return;

                Color Color_ARGB;
                string Ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + "\\Minecraft 3D Textures\\Test";

                Color_ARGB = Dyes.BLACK;
                Colorear_Imagen(Ruta + "\\black_banner.png", Color_ARGB);

                Color_ARGB = Dyes.BLUE;
                Colorear_Imagen(Ruta + "\\blue_banner.png", Color_ARGB);

                Color_ARGB = Dyes.BROWN;
                Colorear_Imagen(Ruta + "\\brown_banner.png", Color_ARGB);

                Color_ARGB = Dyes.CYAN;
                Colorear_Imagen(Ruta + "\\cyan_banner.png", Color_ARGB);

                Color_ARGB = Dyes.GRAY;
                Colorear_Imagen(Ruta + "\\gray_banner.png", Color_ARGB);

                Color_ARGB = Dyes.GREEN;
                Colorear_Imagen(Ruta + "\\green_banner.png", Color_ARGB);

                Color_ARGB = Dyes.BLUE;
                Colorear_Imagen(Ruta + "\\light_blue_banner.png", Color_ARGB);

                Color_ARGB = Dyes.SILVER;
                Colorear_Imagen(Ruta + "\\light_gray_banner.png", Color_ARGB);

                Color_ARGB = Dyes.LIME;
                Colorear_Imagen(Ruta + "\\lime_banner.png", Color_ARGB);

                Color_ARGB = Dyes.MAGENTA;
                Colorear_Imagen(Ruta + "\\magenta_banner.png", Color_ARGB);

                Color_ARGB = Dyes.ORANGE;
                Colorear_Imagen(Ruta + "\\orange_banner.png", Color_ARGB);

                Color_ARGB = Dyes.PINK;
                Colorear_Imagen(Ruta + "\\pink_banner.png", Color_ARGB);

                Color_ARGB = Dyes.PURPLE;
                Colorear_Imagen(Ruta + "\\purple_banner.png", Color_ARGB);

                Color_ARGB = Dyes.RED;
                Colorear_Imagen(Ruta + "\\red_banner.png", Color_ARGB);

                Color_ARGB = Dyes.WHITE;
                Colorear_Imagen(Ruta + "\\white_banner.png", Color_ARGB);

                Color_ARGB = Dyes.YELLOW;
                Colorear_Imagen(Ruta + "\\yellow_banner.png", Color_ARGB);

                Color_ARGB = Dyes.BLACK;
                Colorear_Imagen(Ruta + "\\black_wall_banner.png", Color_ARGB);

                Color_ARGB = Dyes.BLUE;
                Colorear_Imagen(Ruta + "\\blue_wall_banner.png", Color_ARGB);

                Color_ARGB = Dyes.BROWN;
                Colorear_Imagen(Ruta + "\\brown_wall_banner.png", Color_ARGB);

                Color_ARGB = Dyes.CYAN;
                Colorear_Imagen(Ruta + "\\cyan_wall_banner.png", Color_ARGB);

                Color_ARGB = Dyes.GRAY;
                Colorear_Imagen(Ruta + "\\gray_wall_banner.png", Color_ARGB);

                Color_ARGB = Dyes.GREEN;
                Colorear_Imagen(Ruta + "\\green_wall_banner.png", Color_ARGB);

                Color_ARGB = Dyes.BLUE;
                Colorear_Imagen(Ruta + "\\light_blue_wall_banner.png", Color_ARGB);

                Color_ARGB = Dyes.SILVER;
                Colorear_Imagen(Ruta + "\\light_gray_wall_banner.png", Color_ARGB);

                Color_ARGB = Dyes.LIME;
                Colorear_Imagen(Ruta + "\\lime_wall_banner.png", Color_ARGB);

                Color_ARGB = Dyes.MAGENTA;
                Colorear_Imagen(Ruta + "\\magenta_wall_banner.png", Color_ARGB);

                Color_ARGB = Dyes.ORANGE;
                Colorear_Imagen(Ruta + "\\orange_wall_banner.png", Color_ARGB);

                Color_ARGB = Dyes.PINK;
                Colorear_Imagen(Ruta + "\\pink_wall_banner.png", Color_ARGB);

                Color_ARGB = Dyes.PURPLE;
                Colorear_Imagen(Ruta + "\\purple_wall_banner.png", Color_ARGB);

                Color_ARGB = Dyes.RED;
                Colorear_Imagen(Ruta + "\\red_wall_banner.png", Color_ARGB);

                Color_ARGB = Dyes.WHITE;
                Colorear_Imagen(Ruta + "\\white_wall_banner.png", Color_ARGB);

                Color_ARGB = Dyes.YELLOW;
                Colorear_Imagen(Ruta + "\\yellow_wall_banner.png", Color_ARGB);

                return;

                WATER_FLOWING_WATER.colorMultiplier(out Color_ARGB);
                Colorear_Imagen(Ruta + "\\end_gateway.png", Color_ARGB);

                LEAVES2.colorMultiplier(out Color_ARGB);
                Colorear_Imagen(Ruta + "\\acacia_leaves.png", Color_ARGB);

                MELON_STEM_PUMPKIN_STEM.colorMultiplier(7, out Color_ARGB);
                Colorear_Imagen(Ruta + "\\attached_melon_stem.png", Color_ARGB);

                MELON_STEM_PUMPKIN_STEM.colorMultiplier(7, out Color_ARGB);
                Colorear_Imagen(Ruta + "\\attached_pumpkin_stem.png", Color_ARGB);

                ColorizerFoliage.getFoliageColorBirch(out Color_ARGB);
                Colorear_Imagen(Ruta + "\\birch_leaves.png", Color_ARGB);

                LEAVES2.colorMultiplier(out Color_ARGB);
                Colorear_Imagen(Ruta + "\\dark_oak_leaves.png", Color_ARGB);

                TALLGRASS.colorMultiplier(out Color_ARGB);
                Colorear_Imagen(Ruta + "\\fern.png", Color_ARGB);

                GRASS.colorMultiplier(out Color_ARGB);
                Colorear_Imagen(Ruta + "\\grass.png", Color_ARGB);

                GRASS.colorMultiplier(out Color_ARGB);
                Colorear_Imagen(Ruta + "\\grass_block.png", Color_ARGB);

                ColorizerFoliage.getFoliageColorBasic(out Color_ARGB);
                Colorear_Imagen(Ruta + "\\jungle_leaves.png", Color_ARGB);

                TALLGRASS.colorMultiplier(out Color_ARGB);
                Colorear_Imagen(Ruta + "\\large_fern.png", Color_ARGB);

                TALLGRASS.colorMultiplier(out Color_ARGB);
                Colorear_Imagen(Ruta + "\\large_fern_.png", Color_ARGB);

                WATERLILY.colorMultiplier(out Color_ARGB);
                Colorear_Imagen(Ruta + "\\lily_pad.png", Color_ARGB);

                MELON_STEM_PUMPKIN_STEM.colorMultiplier(7, out Color_ARGB);
                Colorear_Imagen(Ruta + "\\melon_stem.png", Color_ARGB);

                ColorizerFoliage.getFoliageColorBasic(out Color_ARGB);
                Colorear_Imagen(Ruta + "\\oak_leaves.png", Color_ARGB);

                TALLGRASS.colorMultiplier(out Color_ARGB);
                Colorear_Imagen(Ruta + "\\potted_fern.png", Color_ARGB);

                MELON_STEM_PUMPKIN_STEM.colorMultiplier(7, out Color_ARGB);
                Colorear_Imagen(Ruta + "\\pumpkin_stem.png", Color_ARGB);

                BlockRedstoneWire.colorMultiplier(15, out Color_ARGB);
                Colorear_Imagen(Ruta + "\\redstone_dust_dot.png", Color_ARGB);

                BlockRedstoneWire.colorMultiplier(15, out Color_ARGB);
                Colorear_Imagen(Ruta + "\\redstone_dust_line0.png", Color_ARGB);

                BlockRedstoneWire.colorMultiplier(15, out Color_ARGB);
                Colorear_Imagen(Ruta + "\\redstone_dust_line1.png", Color_ARGB);

                BlockRedstoneWire.colorMultiplier(15, out Color_ARGB);
                Colorear_Imagen(Ruta + "\\redstone_wire.png", Color_ARGB);

                ColorizerFoliage.getFoliageColorPine(out Color_ARGB);
                Colorear_Imagen(Ruta + "\\spruce_leaves.png", Color_ARGB);

                TALLGRASS.colorMultiplier(out Color_ARGB);
                Colorear_Imagen(Ruta + "\\tall_grass.png", Color_ARGB);

                TALLGRASS.colorMultiplier(out Color_ARGB);
                Colorear_Imagen(Ruta + "\\tall_grass_.png", Color_ARGB);

                VINE.colorMultiplier(out Color_ARGB);
                Colorear_Imagen(Ruta + "\\vine.png", Color_ARGB);

                WATER_FLOWING_WATER.colorMultiplier(out Color_ARGB);
                Colorear_Imagen(Ruta + "\\water.png", Color_ARGB);

                /*BlockRedstoneWire.colorMultiplier(15, out Color_ARGB);
                Colorear_Imagen(Ruta + "\\redstone_wire.png", Color_ARGB);

                BlockRedstoneWire.colorMultiplier(15, out Color_ARGB);
                Colorear_Imagen(Ruta + "\\redstone_wire.png", Color_ARGB);*/

                //return Color_ARGB;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            //return Color.Fuchsia;
        }

        /// <summary>
        /// Loads an existing image, and recolors the non fully transparent but fully gray pixels
        /// with the specified color mixed proportionally with the original gray pixel as a mask.
        /// After that saves back the original image with the new colors, overwriting it.
        /// </summary>
        internal static void Colorear_Imagen(string Ruta, Color Color_ARGB)
        {
            try
            {
                Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Checked);
                if (Imagen != null && Color_ARGB != Color.Empty)
                {
                    int Ancho = Imagen.Width;
                    int Alto = Imagen.Height;
                    BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                    byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                    int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                    int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                    for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                    {
                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                        {
                            if (Matriz_Bytes[Índice + 3] > 0) // Not fully transparent.
                            {
                                int Mínimo = Math.Min(Matriz_Bytes[Índice + 2], Math.Min(Matriz_Bytes[Índice + 1], Matriz_Bytes[Índice]));
                                int Máximo = Math.Max(Matriz_Bytes[Índice + 2], Math.Max(Matriz_Bytes[Índice + 1], Matriz_Bytes[Índice]));
                                int Diferencia_Máxima = Máximo - Mínimo;

                                //if (Matriz_Bytes[Índice] == Matriz_Bytes[Índice + 1] && Matriz_Bytes[Índice] == Matriz_Bytes[Índice + 2]) // It's grayscale.
                                if (Diferencia_Máxima < 32) // Assume grayscale (or else spruce leaves and others will fail).
                                {
                                    // Recolor the pixel based on it's gray value.
                                    int Gris = Matriz_Bytes[Índice]; // Mask color.
                                    int Rojo = (Color_ARGB.R * Gris) / 255;
                                    int Verde = (Color_ARGB.G * Gris) / 255;
                                    int Azul = (Color_ARGB.B * Gris) / 255;

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
                        }
                    }
                    Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                    Imagen.UnlockBits(Bitmap_Data);
                    Bitmap_Data = null;
                    Matriz_Bytes = null;
                    Imagen.Save(Ruta, ImageFormat.Png);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static Bitmap Filtrar_Imagen(Bitmap Imagen, int Porcentaje_Brillo)
        {
            try
            {
                if (Imagen != null)
                {
                    int Ancho = Imagen.Width;
                    int Alto = Imagen.Height;
                    BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                    byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                    int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                    int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                    for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                    {
                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                        {
                            if (Matriz_Bytes[Índice + 3] > 0) // Not fully transparent.
                            {
                                // Change the brightness proportion.
                                int Rojo = (Matriz_Bytes[Índice + 2] * Porcentaje_Brillo) / 100;
                                int Verde = (Matriz_Bytes[Índice + 1] * Porcentaje_Brillo) / 100;
                                int Azul = (Matriz_Bytes[Índice] * Porcentaje_Brillo) / 100;

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

        /*internal enum Bloques : int
        {
            redstone_wire,

        }

        internal static void Colorear_Texturas(string[] Matriz_Rutas, )*/

        internal static class MathHelper
        {
            /// <summary>
            /// Returns the value of the first parameter, clamped to be within the lower and upper
            /// limits given by the second and third parameters.
            /// </summary>
            public static int clamp(int num, int min, int max)
            {
                if (num < min)
                {
                    return min;
                }
                else
                {
                    return num > max ? max : num;
                }
            }
        }

        internal static class BlockRedstoneWire
        {
            /// <summary>
            /// Gets the color of the redstone wire based on the given power level (0 to 15).
            /// </summary>
            public static int colorMultiplier(int p_176337_0_, out Color Color_ARGB)
            {
                float f = (float)p_176337_0_ / 15.0F;
                float f1 = f * 0.6F + 0.4F;

                if (p_176337_0_ == 0)
                {
                    f1 = 0.3F;
                }

                float f2 = f * f * 0.7F - 0.5F;
                float f3 = f * f * 0.6F - 0.7F;

                if (f2 < 0.0F)
                {
                    f2 = 0.0F;
                }

                if (f3 < 0.0F)
                {
                    f3 = 0.0F;
                }

                int i = MathHelper.clamp((int)(f1 * 255.0F), 0, 255);
                int j = MathHelper.clamp((int)(f2 * 255.0F), 0, 255);
                int k = MathHelper.clamp((int)(f3 * 255.0F), 0, 255);
                Color_ARGB = Color.FromArgb(255, i, j, k);
                return -16777216 | i << 16 | j << 8 | k;
            }
        }

        internal static class ColorizerFoliage
        {
            /// <summary>
            /// Color buffer for foliage
            /// </summary>
            private static int[] foliageBuffer = new int[65536];

            public static void setFoliageBiomeColorizer(int[] foliageBufferIn)
            {
                foliageBuffer = foliageBufferIn;
            }

            /// <summary>
            /// Gets the color modifier to use for foliage.
            /// </summary>
            /// <param name="temperature"></param>
            /// <param name="humidity"></param>
            /// <returns></returns>
            public static int getFoliageColor(double temperature, double humidity)
            {
                humidity = humidity * temperature;
                int i = (int)((1.0D - temperature) * 255.0D);
                int j = (int)((1.0D - humidity) * 255.0D);
                return foliageBuffer[j << 8 | i];
            }

            /// <summary>
            /// Gets the foliage color for pine type (metadata 1) trees
            /// </summary>
            public static int getFoliageColorPine(out Color Color_ARGB)
            {
                Color_ARGB = Color.FromArgb(6396257);
                return 6396257;
            }

            /// <summary>
            /// Gets the foliage color for birch type (metadata 2) trees
            /// </summary>
            public static int getFoliageColorBirch(out Color Color_ARGB)
            {
                Color_ARGB = Color.FromArgb(8431445);
                return 8431445;
            }

            public static int getFoliageColorBasic(out Color Color_ARGB)
            {
                Color_ARGB = Color.FromArgb(4764952);
                return 4764952;
            }
        }

        internal static class DOUBLE_PLANT
        {
            internal static int colorMultiplier(out Color Color_ARGB/*IBlockState state, @Nullable IBlockAccess worldIn, @Nullable BlockPos pos, int tintIndex*/)
            {
                //BlockDoublePlant.EnumPlantType blockdoubleplant$enumplanttype = (BlockDoublePlant.EnumPlantType)state.getValue(BlockDoublePlant.VARIANT);
                //return worldIn != null && pos != null && (blockdoubleplant$enumplanttype == BlockDoublePlant.EnumPlantType.GRASS || blockdoubleplant$enumplanttype == BlockDoublePlant.EnumPlantType.FERN) ? BiomeColorHelper.getGrassColorAtPos(worldIn, state.getValue(BlockDoublePlant.HALF) == BlockDoublePlant.EnumBlockHalf.UPPER ? pos.down() : pos) : -1;
                return BiomeColorHelper.getGrassColorAtPos(out Color_ARGB);
            }
        }

        internal static class LEAVES
        {
            internal static int colorMultiplier(out Color Color_ARGB/*IBlockState state, @Nullable IBlockAccess worldIn, @Nullable BlockPos pos, int tintIndex*/)
            {
                /*BlockPlanks.EnumType blockplanks$enumtype = (BlockPlanks.EnumType)state.getValue(BlockOldLeaf.VARIANT);

                if (blockplanks$enumtype == BlockPlanks.EnumType.SPRUCE)
                {
                    return ColorizerFoliage.getFoliageColorPine();
                }
                else if (blockplanks$enumtype == BlockPlanks.EnumType.BIRCH)
                {
                    return ColorizerFoliage.getFoliageColorBirch();
                }
                else
                {
                    return worldIn != null && pos != null ? BiomeColorHelper.getFoliageColorAtPos(worldIn, pos) : ColorizerFoliage.getFoliageColorBasic();
                }*/
                Color_ARGB = Color.Transparent;
                return 0;
            }
        }

        internal static class LEAVES2
        {
            internal static int colorMultiplier(out Color Color_ARGB/*IBlockState state, @Nullable IBlockAccess worldIn, @Nullable BlockPos pos, int tintIndex*/)
            {
                //return worldIn != null && pos != null ? BiomeColorHelper.getFoliageColorAtPos(worldIn, pos) : ColorizerFoliage.getFoliageColorBasic();
                return ColorizerFoliage.getFoliageColorBasic(out Color_ARGB);
            }
        }

        internal static class ColorizerGrass
        {
            /// <summary>
            /// Color buffer for grass
            /// </summary>
            internal static Color[] grassBuffer = null; //new int[65536];

            internal static void setGrassBiomeColorizer(Color[] grassBufferIn)
            {
                grassBuffer = grassBufferIn;
            }

            /// <summary>
            /// Gets the color modifier to use for grass.
            /// </summary>
            internal static int getGrassColor(out Color Color_ARGB, double temperature, double humidity)
            {
                if (grassBuffer == null)
                {
                    grassBuffer = new Color[65536];
                    Bitmap Colormap = Resources.Colormap_Grass;
                    for (int Índice_Y = 0, Índice = 0; Índice_Y < 256; Índice_Y++)
                    {
                        for (int Índice_X = 0; Índice_X < 256; Índice_X++, Índice++)
                        {
                            grassBuffer[Índice] = Colormap.GetPixel(Índice_X, Índice_Y); // Super slow (use LockBits() here).
                        }
                    }
                    Colormap = null;
                }

                humidity = humidity * temperature;
                int i = (int)((1.0D - temperature) * 255.0D);
                int j = (int)((1.0D - humidity) * 255.0D);
                int k = j << 8 | i;
                Color_ARGB = k > grassBuffer.Length ? Color.FromArgb(-65281) : grassBuffer[k];
                return k > grassBuffer.Length ? -65281 : grassBuffer[k].ToArgb();
            }
        }

        internal static class GRASS
        {
            internal static int colorMultiplier(out Color Color_ARGB/*IBlockState state, @Nullable IBlockAccess worldIn, @Nullable BlockPos pos, int tintIndex*/)
            {
                //return worldIn != null && pos != null ? BiomeColorHelper.getGrassColorAtPos(worldIn, pos) : ColorizerGrass.getGrassColor(0.5D, 1.0D);
                return ColorizerGrass.getGrassColor(out Color_ARGB, 0.5D, 1.0D);
            }
        }

        internal static class MELON_STEM_PUMPKIN_STEM
        {
            internal static int colorMultiplier(int BlockStem_AGE, out Color Color_ARGB/*IBlockState state, @Nullable IBlockAccess worldIn, @Nullable BlockPos pos, int tintIndex*/)
            {
                //int i = ((int)state.getValue(BlockStem.AGE)).intValue();
                int i = BlockStem_AGE;
                int j = i * 32;
                int k = 255 - i * 8;
                int l = i * 4;
                Color_ARGB = Color.FromArgb(255, j, k, l);
                return j << 16 | k << 8 | l;
            }
        }

        internal static class TALLGRASS
        {
            internal static int colorMultiplier(out Color Color_ARGB/*IBlockState state, @Nullable IBlockAccess worldIn, @Nullable BlockPos pos, int tintIndex*/)
            {
                //if (worldIn != null && pos != null)
                {
                    //return BiomeColorHelper.getGrassColorAtPos(out Color_ARGB/*worldIn, pos*/);
                    return ColorizerGrass.getGrassColor(out Color_ARGB, 0.5D, 1.0D);
                }
                /*else
                {
                    return state.getValue(BlockTallGrass.TYPE) == BlockTallGrass.EnumType.DEAD_BUSH ? 16777215 : ColorizerGrass.getGrassColor(0.5D, 1.0D);
                }*/
            }
        }

        internal static class VINE
        {
            internal static int colorMultiplier(out Color Color_ARGB/*IBlockState state, @Nullable IBlockAccess worldIn, @Nullable BlockPos pos, int tintIndex*/)
            {
                //return worldIn != null && pos != null ? BiomeColorHelper.getFoliageColorAtPos(worldIn, pos) : ColorizerFoliage.getFoliageColorBasic();
                return ColorizerFoliage.getFoliageColorBasic(out Color_ARGB);
            }
        }

        internal static class WATERLILY
        {
            internal static int colorMultiplier(out Color Color_ARGB/*IBlockState state, @Nullable IBlockAccess worldIn, @Nullable BlockPos pos, int tintIndex*/)
            {
                //return worldIn != null && pos != null ? 2129968 : 7455580;
                Color_ARGB = Color.FromArgb(7455580);
                return 7455580;
            }
        }

        internal static class BiomeColorHelper
        {
            internal static int getColorAtPos(out Color Color_ARGB/*IBlockAccess blockAccess, BlockPos pos, BiomeColorHelper.ColorResolver colorResolver*/)
            {
                int i = 0;
                int j = 0;
                int k = 0;

                /*for (BlockPos.MutableBlockPos blockpos$mutableblockpos : BlockPos.getAllInBoxMutable(pos.add(-1, 0, -1), pos.add(1, 0, 1)))
                {
                    int l = colorResolver.getColorAtPos(blockAccess.getBiome(blockpos$mutableblockpos), blockpos$mutableblockpos);
                    i += (l & 16711680) >> 16;
                    j += (l & 65280) >> 8;
                    k += l & 255;
                }

                return (i / 9 & 255) << 16 | (j / 9 & 255) << 8 | k / 9 & 255;*/
                Color_ARGB = Color.Transparent;
                return 0;
            }

            internal static int getGrassColorAtPos(out Color Color_ARGB/*IBlockAccess blockAccess, BlockPos pos*/)
            {
                //return getColorAtPos(blockAccess, pos, GRASS_COLOR);
                Color_ARGB = Color.Transparent;
                return 0;
            }

            internal static int getFoliageColorAtPos(out Color Color_ARGB/*IBlockAccess blockAccess, BlockPos pos*/)
            {
                //return getColorAtPos(blockAccess, pos, FOLIAGE_COLOR);
                Color_ARGB = Color.Transparent;
                return 0;
            }

            internal static int getWaterColorAtPos(out Color Color_ARGB/*IBlockAccess blockAccess, BlockPos pos*/)
            {
                //return getColorAtPos(blockAccess, pos, WATER_COLOR);
                Color_ARGB = Color.FromArgb(Biome.BiomeProperties.waterColor);
                return Biome.BiomeProperties.waterColor;
            }

            /*interface ColorResolver
            {
                int getColorAtPos(Biome biome, BlockPos blockPosition);
            }*/
        }

        internal static class Biome
        {
            // Registers all of the vanilla biomes. Note: includes the water color, etc for some.
            /*public static void registerBiomes()
            {
            registerBiome(0, "ocean", new BiomeOcean((new Biome.BiomeProperties("Ocean")).setBaseHeight(-1.0F).setHeightVariation(0.1F)));
            registerBiome(1, "plains", new BiomePlains(false, (new Biome.BiomeProperties("Plains")).setBaseHeight(0.125F).setHeightVariation(0.05F).setTemperature(0.8F).setRainfall(0.4F)));
            registerBiome(2, "desert", new BiomeDesert((new Biome.BiomeProperties("Desert")).setBaseHeight(0.125F).setHeightVariation(0.05F).setTemperature(2.0F).setRainfall(0.0F).setRainDisabled()));
            registerBiome(3, "extreme_hills", new BiomeHills(BiomeHills.Type.NORMAL, (new Biome.BiomeProperties("Extreme Hills")).setBaseHeight(1.0F).setHeightVariation(0.5F).setTemperature(0.2F).setRainfall(0.3F)));
            registerBiome(4, "forest", new BiomeForest(BiomeForest.Type.NORMAL, (new Biome.BiomeProperties("Forest")).setTemperature(0.7F).setRainfall(0.8F)));
            registerBiome(5, "taiga", new BiomeTaiga(BiomeTaiga.Type.NORMAL, (new Biome.BiomeProperties("Taiga")).setBaseHeight(0.2F).setHeightVariation(0.2F).setTemperature(0.25F).setRainfall(0.8F)));
            registerBiome(6, "swampland", new BiomeSwamp((new Biome.BiomeProperties("Swampland")).setBaseHeight(-0.2F).setHeightVariation(0.1F).setTemperature(0.8F).setRainfall(0.9F).setWaterColor(14745518)));
            registerBiome(7, "river", new BiomeRiver((new Biome.BiomeProperties("River")).setBaseHeight(-0.5F).setHeightVariation(0.0F)));
            registerBiome(8, "hell", new BiomeHell((new Biome.BiomeProperties("Hell")).setTemperature(2.0F).setRainfall(0.0F).setRainDisabled()));
            registerBiome(9, "sky", new BiomeEnd((new Biome.BiomeProperties("The End")).setRainDisabled()));
            registerBiome(10, "frozen_ocean", new BiomeOcean((new Biome.BiomeProperties("FrozenOcean")).setBaseHeight(-1.0F).setHeightVariation(0.1F).setTemperature(0.0F).setRainfall(0.5F).setSnowEnabled()));
            registerBiome(11, "frozen_river", new BiomeRiver((new Biome.BiomeProperties("FrozenRiver")).setBaseHeight(-0.5F).setHeightVariation(0.0F).setTemperature(0.0F).setRainfall(0.5F).setSnowEnabled()));
            registerBiome(12, "ice_flats", new BiomeSnow(false, (new Biome.BiomeProperties("Ice Plains")).setBaseHeight(0.125F).setHeightVariation(0.05F).setTemperature(0.0F).setRainfall(0.5F).setSnowEnabled()));
            registerBiome(13, "ice_mountains", new BiomeSnow(false, (new Biome.BiomeProperties("Ice Mountains")).setBaseHeight(0.45F).setHeightVariation(0.3F).setTemperature(0.0F).setRainfall(0.5F).setSnowEnabled()));
            registerBiome(14, "mushroom_island", new BiomeMushroomIsland((new Biome.BiomeProperties("MushroomIsland")).setBaseHeight(0.2F).setHeightVariation(0.3F).setTemperature(0.9F).setRainfall(1.0F)));
            registerBiome(15, "mushroom_island_shore", new BiomeMushroomIsland((new Biome.BiomeProperties("MushroomIslandShore")).setBaseHeight(0.0F).setHeightVariation(0.025F).setTemperature(0.9F).setRainfall(1.0F)));
            registerBiome(16, "beaches", new BiomeBeach((new Biome.BiomeProperties("Beach")).setBaseHeight(0.0F).setHeightVariation(0.025F).setTemperature(0.8F).setRainfall(0.4F)));
            registerBiome(17, "desert_hills", new BiomeDesert((new Biome.BiomeProperties("DesertHills")).setBaseHeight(0.45F).setHeightVariation(0.3F).setTemperature(2.0F).setRainfall(0.0F).setRainDisabled()));
            registerBiome(18, "forest_hills", new BiomeForest(BiomeForest.Type.NORMAL, (new Biome.BiomeProperties("ForestHills")).setBaseHeight(0.45F).setHeightVariation(0.3F).setTemperature(0.7F).setRainfall(0.8F)));
            registerBiome(19, "taiga_hills", new BiomeTaiga(BiomeTaiga.Type.NORMAL, (new Biome.BiomeProperties("TaigaHills")).setTemperature(0.25F).setRainfall(0.8F).setBaseHeight(0.45F).setHeightVariation(0.3F)));
            registerBiome(20, "smaller_extreme_hills", new BiomeHills(BiomeHills.Type.EXTRA_TREES, (new Biome.BiomeProperties("Extreme Hills Edge")).setBaseHeight(0.8F).setHeightVariation(0.3F).setTemperature(0.2F).setRainfall(0.3F)));
            registerBiome(21, "jungle", new BiomeJungle(false, (new Biome.BiomeProperties("Jungle")).setTemperature(0.95F).setRainfall(0.9F)));
            registerBiome(22, "jungle_hills", new BiomeJungle(false, (new Biome.BiomeProperties("JungleHills")).setBaseHeight(0.45F).setHeightVariation(0.3F).setTemperature(0.95F).setRainfall(0.9F)));
            registerBiome(23, "jungle_edge", new BiomeJungle(true, (new Biome.BiomeProperties("JungleEdge")).setTemperature(0.95F).setRainfall(0.8F)));
            registerBiome(24, "deep_ocean", new BiomeOcean((new Biome.BiomeProperties("Deep Ocean")).setBaseHeight(-1.8F).setHeightVariation(0.1F)));
            registerBiome(25, "stone_beach", new BiomeStoneBeach((new Biome.BiomeProperties("Stone Beach")).setBaseHeight(0.1F).setHeightVariation(0.8F).setTemperature(0.2F).setRainfall(0.3F)));
            registerBiome(26, "cold_beach", new BiomeBeach((new Biome.BiomeProperties("Cold Beach")).setBaseHeight(0.0F).setHeightVariation(0.025F).setTemperature(0.05F).setRainfall(0.3F).setSnowEnabled()));
            registerBiome(27, "birch_forest", new BiomeForest(BiomeForest.Type.BIRCH, (new Biome.BiomeProperties("Birch Forest")).setTemperature(0.6F).setRainfall(0.6F)));
            registerBiome(28, "birch_forest_hills", new BiomeForest(BiomeForest.Type.BIRCH, (new Biome.BiomeProperties("Birch Forest Hills")).setBaseHeight(0.45F).setHeightVariation(0.3F).setTemperature(0.6F).setRainfall(0.6F)));
            registerBiome(29, "roofed_forest", new BiomeForest(BiomeForest.Type.ROOFED, (new Biome.BiomeProperties("Roofed Forest")).setTemperature(0.7F).setRainfall(0.8F)));
            registerBiome(30, "taiga_cold", new BiomeTaiga(BiomeTaiga.Type.NORMAL, (new Biome.BiomeProperties("Cold Taiga")).setBaseHeight(0.2F).setHeightVariation(0.2F).setTemperature(-0.5F).setRainfall(0.4F).setSnowEnabled()));
            registerBiome(31, "taiga_cold_hills", new BiomeTaiga(BiomeTaiga.Type.NORMAL, (new Biome.BiomeProperties("Cold Taiga Hills")).setBaseHeight(0.45F).setHeightVariation(0.3F).setTemperature(-0.5F).setRainfall(0.4F).setSnowEnabled()));
            registerBiome(32, "redwood_taiga", new BiomeTaiga(BiomeTaiga.Type.MEGA, (new Biome.BiomeProperties("Mega Taiga")).setTemperature(0.3F).setRainfall(0.8F).setBaseHeight(0.2F).setHeightVariation(0.2F)));
            registerBiome(33, "redwood_taiga_hills", new BiomeTaiga(BiomeTaiga.Type.MEGA, (new Biome.BiomeProperties("Mega Taiga Hills")).setBaseHeight(0.45F).setHeightVariation(0.3F).setTemperature(0.3F).setRainfall(0.8F)));
            registerBiome(34, "extreme_hills_with_trees", new BiomeHills(BiomeHills.Type.EXTRA_TREES, (new Biome.BiomeProperties("Extreme Hills+")).setBaseHeight(1.0F).setHeightVariation(0.5F).setTemperature(0.2F).setRainfall(0.3F)));
            registerBiome(35, "savanna", new BiomeSavanna((new Biome.BiomeProperties("Savanna")).setBaseHeight(0.125F).setHeightVariation(0.05F).setTemperature(1.2F).setRainfall(0.0F).setRainDisabled()));
            registerBiome(36, "savanna_rock", new BiomeSavanna((new Biome.BiomeProperties("Savanna Plateau")).setBaseHeight(1.5F).setHeightVariation(0.025F).setTemperature(1.0F).setRainfall(0.0F).setRainDisabled()));
            registerBiome(37, "mesa", new BiomeMesa(false, false, (new Biome.BiomeProperties("Mesa")).setTemperature(2.0F).setRainfall(0.0F).setRainDisabled()));
            registerBiome(38, "mesa_rock", new BiomeMesa(false, true, (new Biome.BiomeProperties("Mesa Plateau F")).setBaseHeight(1.5F).setHeightVariation(0.025F).setTemperature(2.0F).setRainfall(0.0F).setRainDisabled()));
            registerBiome(39, "mesa_clear_rock", new BiomeMesa(false, false, (new Biome.BiomeProperties("Mesa Plateau")).setBaseHeight(1.5F).setHeightVariation(0.025F).setTemperature(2.0F).setRainfall(0.0F).setRainDisabled()));
            registerBiome(127, "void", new BiomeVoid((new Biome.BiomeProperties("The Void")).setRainDisabled()));
            registerBiome(129, "mutated_plains", new BiomePlains(true, (new Biome.BiomeProperties("Sunflower Plains")).setBaseBiome("plains").setBaseHeight(0.125F).setHeightVariation(0.05F).setTemperature(0.8F).setRainfall(0.4F)));
            registerBiome(130, "mutated_desert", new BiomeDesert((new Biome.BiomeProperties("Desert M")).setBaseBiome("desert").setBaseHeight(0.225F).setHeightVariation(0.25F).setTemperature(2.0F).setRainfall(0.0F).setRainDisabled()));
            registerBiome(131, "mutated_extreme_hills", new BiomeHills(BiomeHills.Type.MUTATED, (new Biome.BiomeProperties("Extreme Hills M")).setBaseBiome("extreme_hills").setBaseHeight(1.0F).setHeightVariation(0.5F).setTemperature(0.2F).setRainfall(0.3F)));
            registerBiome(132, "mutated_forest", new BiomeForest(BiomeForest.Type.FLOWER, (new Biome.BiomeProperties("Flower Forest")).setBaseBiome("forest").setHeightVariation(0.4F).setTemperature(0.7F).setRainfall(0.8F)));
            registerBiome(133, "mutated_taiga", new BiomeTaiga(BiomeTaiga.Type.NORMAL, (new Biome.BiomeProperties("Taiga M")).setBaseBiome("taiga").setBaseHeight(0.3F).setHeightVariation(0.4F).setTemperature(0.25F).setRainfall(0.8F)));
            registerBiome(134, "mutated_swampland", new BiomeSwamp((new Biome.BiomeProperties("Swampland M")).setBaseBiome("swampland").setBaseHeight(-0.1F).setHeightVariation(0.3F).setTemperature(0.8F).setRainfall(0.9F).setWaterColor(14745518)));
            registerBiome(140, "mutated_ice_flats", new BiomeSnow(true, (new Biome.BiomeProperties("Ice Plains Spikes")).setBaseBiome("ice_flats").setBaseHeight(0.425F).setHeightVariation(0.45000002F).setTemperature(0.0F).setRainfall(0.5F).setSnowEnabled()));
            registerBiome(149, "mutated_jungle", new BiomeJungle(false, (new Biome.BiomeProperties("Jungle M")).setBaseBiome("jungle").setBaseHeight(0.2F).setHeightVariation(0.4F).setTemperature(0.95F).setRainfall(0.9F)));
            registerBiome(151, "mutated_jungle_edge", new BiomeJungle(true, (new Biome.BiomeProperties("JungleEdge M")).setBaseBiome("jungle_edge").setBaseHeight(0.2F).setHeightVariation(0.4F).setTemperature(0.95F).setRainfall(0.8F)));
            registerBiome(155, "mutated_birch_forest", new BiomeForestMutated((new Biome.BiomeProperties("Birch Forest M")).setBaseBiome("birch_forest").setBaseHeight(0.2F).setHeightVariation(0.4F).setTemperature(0.6F).setRainfall(0.6F)));
            registerBiome(156, "mutated_birch_forest_hills", new BiomeForestMutated((new Biome.BiomeProperties("Birch Forest Hills M")).setBaseBiome("birch_forest_hills").setBaseHeight(0.55F).setHeightVariation(0.5F).setTemperature(0.6F).setRainfall(0.6F)));
            registerBiome(157, "mutated_roofed_forest", new BiomeForest(BiomeForest.Type.ROOFED, (new Biome.BiomeProperties("Roofed Forest M")).setBaseBiome("roofed_forest").setBaseHeight(0.2F).setHeightVariation(0.4F).setTemperature(0.7F).setRainfall(0.8F)));
            registerBiome(158, "mutated_taiga_cold", new BiomeTaiga(BiomeTaiga.Type.NORMAL, (new Biome.BiomeProperties("Cold Taiga M")).setBaseBiome("taiga_cold").setBaseHeight(0.3F).setHeightVariation(0.4F).setTemperature(-0.5F).setRainfall(0.4F).setSnowEnabled()));
            registerBiome(160, "mutated_redwood_taiga", new BiomeTaiga(BiomeTaiga.Type.MEGA_SPRUCE, (new Biome.BiomeProperties("Mega Spruce Taiga")).setBaseBiome("redwood_taiga").setBaseHeight(0.2F).setHeightVariation(0.2F).setTemperature(0.25F).setRainfall(0.8F)));
            registerBiome(161, "mutated_redwood_taiga_hills", new BiomeTaiga(BiomeTaiga.Type.MEGA_SPRUCE, (new Biome.BiomeProperties("Redwood Taiga Hills M")).setBaseBiome("redwood_taiga_hills").setBaseHeight(0.2F).setHeightVariation(0.2F).setTemperature(0.25F).setRainfall(0.8F)));
            registerBiome(162, "mutated_extreme_hills_with_trees", new BiomeHills(BiomeHills.Type.MUTATED, (new Biome.BiomeProperties("Extreme Hills+ M")).setBaseBiome("extreme_hills_with_trees").setBaseHeight(1.0F).setHeightVariation(0.5F).setTemperature(0.2F).setRainfall(0.3F)));
            registerBiome(163, "mutated_savanna", new BiomeSavannaMutated((new Biome.BiomeProperties("Savanna M")).setBaseBiome("savanna").setBaseHeight(0.3625F).setHeightVariation(1.225F).setTemperature(1.1F).setRainfall(0.0F).setRainDisabled()));
            registerBiome(164, "mutated_savanna_rock", new BiomeSavannaMutated((new Biome.BiomeProperties("Savanna Plateau M")).setBaseBiome("savanna_rock").setBaseHeight(1.05F).setHeightVariation(1.2125001F).setTemperature(1.0F).setRainfall(0.0F).setRainDisabled()));
            registerBiome(165, "mutated_mesa", new BiomeMesa(true, false, (new Biome.BiomeProperties("Mesa (Bryce)")).setBaseBiome("mesa").setTemperature(2.0F).setRainfall(0.0F).setRainDisabled()));
            registerBiome(166, "mutated_mesa_rock", new BiomeMesa(false, true, (new Biome.BiomeProperties("Mesa Plateau F M")).setBaseBiome("mesa_rock").setBaseHeight(0.45F).setHeightVariation(0.3F).setTemperature(2.0F).setRainfall(0.0F).setRainDisabled()));
            registerBiome(167, "mutated_mesa_clear_rock", new BiomeMesa(false, false, (new Biome.BiomeProperties("Mesa Plateau M")).setBaseBiome("mesa_clear_rock").setBaseHeight(0.45F).setHeightVariation(0.3F).setTemperature(2.0F).setRainfall(0.0F).setRainDisabled()));
            }*/

            internal static class BiomeProperties
            {
                internal static int waterColor = 16777215; // 16777215 (Default); // 14745518 (Swamp).
            }

            /*public int getGrassColorAtPos(BlockPos pos)
            {
                double d0 = (double)MathHelper.clamp(this.getFloatTemperature(pos), 0.0F, 1.0F);
                double d1 = (double)MathHelper.clamp(this.getRainfall(), 0.0F, 1.0F);
                return ColorizerGrass.getGrassColor(d0, d1);
            }*/

            /*public int getFoliageColorAtPos(BlockPos pos)
            {
                double d0 = (double)MathHelper.clamp(this.getFloatTemperature(pos), 0.0F, 1.0F);
                double d1 = (double)MathHelper.clamp(this.getRainfall(), 0.0F, 1.0F);
                return ColorizerFoliage.getFoliageColor(d0, d1);
            }*/
        }

        internal static class WATER_FLOWING_WATER
        {
            internal static int colorMultiplier(out Color Color_ARGB/*IBlockState state, @Nullable IBlockAccess worldIn, @Nullable BlockPos pos, int tintIndex*/)
            {
                //return worldIn != null && pos != null ? BiomeColorHelper.getWaterColorAtPos(worldIn, pos) : -1;
                Color_ARGB = Color.FromArgb(Biome.BiomeProperties.waterColor); // 1.12 for swamps only.
                Color_ARGB = Color.FromArgb(255, 64, 88, 208); // Unknown real color ... temporary fix.
                //MessageBox.Show(Color_ARGB.ToString());
                return Biome.BiomeProperties.waterColor;
            }
        }

        internal static class BlockColors // Flower pot, derived to fern.
        {
            internal static int colorMultiplier(out Color Color_ARGB/*IBlockState state, @Nullable IBlockAccess blockAccess, @Nullable BlockPos pos, int renderPass*/)
            {
                //IBlockColor iblockcolor = this.mapBlockColors.getByValue(Block.getIdFromBlock(state.getBlock()));
                //return iblockcolor == null ? -1 : iblockcolor.colorMultiplier(state, blockAccess, pos, renderPass);
                Color_ARGB = Color.Transparent;
                return -1;
            }
        }

        internal static class Dyes
        {
            /*WHITE(0, 15, "white", "white", 16383998, TextFormatting.WHITE),
            ORANGE(1, 14, "orange", "orange", 16351261, TextFormatting.GOLD),
            MAGENTA(2, 13, "magenta", "magenta", 13061821, TextFormatting.AQUA),
            LIGHT_BLUE(3, 12, "light_blue", "lightBlue", 3847130, TextFormatting.BLUE),
            YELLOW(4, 11, "yellow", "yellow", 16701501, TextFormatting.YELLOW),
            LIME(5, 10, "lime", "lime", 8439583, TextFormatting.GREEN),
            PINK(6, 9, "pink", "pink", 15961002, TextFormatting.LIGHT_PURPLE),
            GRAY(7, 8, "gray", "gray", 4673362, TextFormatting.DARK_GRAY),
            SILVER(8, 7, "silver", "silver", 10329495, TextFormatting.GRAY),
            CYAN(9, 6, "cyan", "cyan", 1481884, TextFormatting.DARK_AQUA),
            PURPLE(10, 5, "purple", "purple", 8991416, TextFormatting.DARK_PURPLE),
            BLUE(11, 4, "blue", "blue", 3949738, TextFormatting.DARK_BLUE),
            BROWN(12, 3, "brown", "brown", 8606770, TextFormatting.GOLD),
            GREEN(13, 2, "green", "green", 6192150, TextFormatting.DARK_GREEN),
            RED(14, 1, "red", "red", 11546150, TextFormatting.DARK_RED),
            BLACK(15, 0, "black", "black", 1908001, TextFormatting.BLACK);*/

            internal static Color WHITE = Color.FromArgb(16383998);
            internal static Color ORANGE = Color.FromArgb(16351261);
            internal static Color MAGENTA = Color.FromArgb(13061821);
            internal static Color LIGHT_BLUE = Color.FromArgb(3847130);
            internal static Color YELLOW = Color.FromArgb(16701501);
            internal static Color LIME = Color.FromArgb(8439583);
            internal static Color PINK = Color.FromArgb(15961002);
            internal static Color GRAY = Color.FromArgb(4673362);
            internal static Color SILVER = Color.FromArgb(10329495);
            internal static Color CYAN = Color.FromArgb(1481884);
            internal static Color PURPLE = Color.FromArgb(8991416);
            internal static Color BLUE = Color.FromArgb(3949738);
            internal static Color BROWN = Color.FromArgb(8606770);
            internal static Color GREEN = Color.FromArgb(6192150);
            internal static Color RED = Color.FromArgb(11546150);
            internal static Color BLACK = Color.FromArgb(1908001);
        }
    }
}
