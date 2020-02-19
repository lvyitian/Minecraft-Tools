using Microsoft.Win32;
using Minecraft_Tools.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Diseñador_Estandartes_Escudos : Form
    {
        public Ventana_Diseñador_Estandartes_Escudos()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Enumeration used to identify the multiple banner presets. The extra "_" are used to properly translate the creators of the banners.
        /// </summary>
        internal enum Preconfiguraciones_Estandartes : int
        {
            Jupisoft_logo___by_Jupisoft__ = 0,
            Wither___by_Jupisoft__,
            Nether_portal___by_Jupisoft__,
            Flower___by_Jupisoft__,
            Spain___by_Jupisoft__,
            Spain_2___by_Jupisoft__,
            Angry_bunny___by_Jupisoft__,
            Landscape___by_Jupisoft__,

            End_city___by_Mojang__,

            Allium_alliance___by_Stressmonster101__,
            Heart___by_Stressmonster101__,

            Skull_and_bones_on_fire___by_Xisumavoid__,
            Sun_of_Fire___by_Xisumavoid__,
            Apple_in_hand___by_Xisumavoid__,
            Union_jack___by_Xisumavoid__,
            Symmetrical_pattern___by_Xisumavoid__,
            Mojang_logo___by_Xisumavoid__,
            Bug___by_Xisumavoid__,
            Eye___by_Xisumavoid__,
            Mouth___by_Xisumavoid__,
            Number_0___by_Xisumavoid__,
            Number_1___by_Xisumavoid__,
            Number_2___by_Xisumavoid__,
            Number_3___by_Xisumavoid__,
            Number_4___by_Xisumavoid__,
            Number_5___by_Xisumavoid__,
            Number_6___by_Xisumavoid__,
            Number_7___by_Xisumavoid__,
            Number_8___by_Xisumavoid__,
            Number_9___by_Xisumavoid__,
            United_states_of_america___by_Xisumavoid__,
            Brazil___by_Xisumavoid__,
            England___by_Xisumavoid__,
            Jamaica___by_Xisumavoid__,
            Japan___by_Xisumavoid__,
            Germany___by_Xisumavoid__,

            // Chess by_Xisumavoid (soon)...
            Curtain___by_Xisumavoid__,

            Blue_bunny___by_LD_Shadowlady__,
            Pink_bunny___by_LD_Shadowlady__,
            Ice_cream___by_LD_Shadowlady__,
            Heart_message___by_LD_Shadowlady__
        }

        /// <summary>
        /// Enumeration used to identify the multiple banner designs. The extra "_" are used to properly translate the alternative name of the designs.
        /// </summary>
        internal enum Diseños_Estandartes : int
        {
            /// <summary>
            /// Transparent.
            /// </summary>
            None = 0,
            /// <summary>
            /// Bordure.
            /// </summary>
            Border___Bordure__,
            /// <summary>
            /// Field masoned.
            /// </summary>
            Bricks___Field_masoned__,
            /// <summary>
            /// Roundel.
            /// </summary>
            Circle___Roundel__,
            /// <summary>
            /// Creeper charge.
            /// </summary>
            Creeper___Creeper_charge__,
            /// <summary>
            /// Saltire.
            /// </summary>
            Cross___Saltire__,
            /// <summary>
            /// Bordure indented.
            /// </summary>
            Curly_border___Bordure_indented__,
            /// <summary>
            /// Per bend sinister.
            /// </summary>
            Diagonal_left___Per_bend_sinister__,
            /// <summary>
            /// Per bend.
            /// </summary>
            Diagonal_right___Per_bend__,
            /// <summary>
            /// Per bend inverted.
            /// </summary>
            Diagonal_up_left___Per_bend_inverted__,
            /// <summary>
            /// Per bend sinister inverted.
            /// </summary>
            Diagonal_up_right___Per_bend_sinister_inverted__,
            /// <summary>
            /// Flower charge.
            /// </summary>
            Flower___Flower_charge__,
            /// <summary>
            /// Gradient.
            /// </summary>
            Gradient___Gradient__,
            /// <summary>
            /// Base gradient.
            /// </summary>
            Gradient_up___Base_gradient__,
            /// <summary>
            /// Per fess.
            /// </summary>
            Half_horizontal___Per_fess__,
            /// <summary>
            /// Per fess inverted.
            /// </summary>
            Half_horizontal_bottom___Per_fess_inverted__,
            /// <summary>
            /// Per pale.
            /// </summary>
            Half_vertical___Per_pale__,
            /// <summary>
            /// Per pale inverted.
            /// </summary>
            Half_vertical_right___Per_pale_inverted__,
            /// <summary>
            /// Thing.
            /// </summary>
            Mojang___Thing__,
            /// <summary>
            /// Lozenge.
            /// </summary>
            Rhombus___Lozenge__,
            /// <summary>
            /// Skull charge.
            /// </summary>
            Skull___Skull_charge__,
            /// <summary>
            /// Paly.
            /// </summary>
            Small_stripes___Paly__,
            /// <summary>
            /// Base dexter canton.
            /// </summary>
            Square_bottom_left___Base_dexter_canton__,
            /// <summary>
            /// Base sinister canton.
            /// </summary>
            Square_bottom_right___Base_sinister_canton__,
            /// <summary>
            /// Chief dexter canton.
            /// </summary>
            Square_top_left___Chief_dexter_canton__,
            /// <summary>
            /// Chief sinister canton.
            /// </summary>
            Square_top_right___Chief_sinister_canton__,
            /// <summary>
            /// Cross.
            /// </summary>
            Straight_cross___Cross__,
            /// <summary>
            /// Base.
            /// </summary>
            Stripe_bottom___Base__,
            /// <summary>
            /// Pale.
            /// </summary>
            Stripe_center___Pale__,
            /// <summary>
            /// Bend sinister.
            /// </summary>
            Stripe_downleft___Bend_sinister__,
            /// <summary>
            /// Bend.
            /// </summary>
            Stripe_downright___Bend__,
            /// <summary>
            /// Pale dexter.
            /// </summary>
            Stripe_left___Pale_dexter__,
            /// <summary>
            /// Fess.
            /// </summary>
            Stripe_middle___Fess__,
            /// <summary>
            /// Pale sinister.
            /// </summary>
            Stripe_right___Pale_sinister__,
            /// <summary>
            /// Chief.
            /// </summary>
            Stripe_top___Chief__,
            /// <summary>
            /// Chevron.
            /// </summary>
            Triangle_bottom___Chevron__,
            /// <summary>
            /// Inverted chevron.
            /// </summary>
            Triangle_top___Inverted_chevron__,
            /// <summary>
            /// Base indented.
            /// </summary>
            Triangles_bottom___Base_indented__,
            /// <summary>
            /// Chief indented.
            /// </summary>
            Triangles_top___Chief_indented__,

            Total // Don't use
        }

        /// <summary>
        /// Enumeration used to identify the 16 dyes in Minecraft.
        /// </summary>
        internal enum Colores : int
        {
            Red = 0,
            Brown,
            Orange,
            Yellow,
            Lime,
            Green,
            Cyan,
            Light_blue,
            Blue,
            Purple,
            Magenta,
            Pink,
            Black,
            Gray,
            Light_gray,
            White
        }

        /// <summary>
        /// Enumeration used to identify the materials used to craft banners.
        /// </summary>
        internal enum Materiales : int
        {
            Rose_red = 0,
            Cocoa_beans,
            Orange_dye,
            Dandelion_yellow,
            Lime_dye,
            Cactus_green,
            Cyan_dye,
            Light_blue_dye,
            Lapis_lazuli,
            Purple_dye,
            Magenta_dye,
            Pink_dye,
            Ink_sac,
            Gray_dye,
            Light_gray_dye,
            Bone_meal,

            Red_wool,
            Brown_wool,
            Orange_wool,
            Yellow_wool,
            Lime_wool,
            Green_wool,
            Cyan_wool,
            Light_blue_wool,
            Blue_wool,
            Purple_wool,
            Magenta_wool,
            Pink_wool,
            Black_wool,
            Gray_wool,
            Light_gray_wool,
            White_wool,

            Stick,

            Banner,
            Vine,
            Bricks,
            Creeper_head,
            Wither_skeleton_skull,
            Oxeye_daisy,
            Golden_apple,

            Unknown
        }

        /// <summary>
        /// Array used to quickly search and compare the average RGB color of a Minecraft texture.
        /// </summary>
        internal static readonly Color[] Matriz_Colores_Materiales = new Color[]
        {
            Color.FromArgb(255, 160, 33, 33), // Rojo
            Color.FromArgb(255, 96, 58, 32), // Marrón
            Color.FromArgb(255, 198, 124, 36), // Naranja
            Color.FromArgb(255, 187, 163, 33), // Amarillo
            Color.FromArgb(255, 96, 162, 11), // Lima
            Color.FromArgb(255, 59, 86, 19), // Verde
            Color.FromArgb(255, 33, 103, 133), // Aguamarina
            Color.FromArgb(255, 101, 140, 197), // Celeste
            Color.FromArgb(255, 37, 75, 166), // Azul
            Color.FromArgb(255, 133, 62, 173), // Púrpura
            Color.FromArgb(255, 170, 81, 164), // Fucsia
            Color.FromArgb(255, 197, 123, 161), // Rosa
            Color.FromArgb(255, 49, 43, 48), // Negro
            Color.FromArgb(255, 108, 108, 108), // Gris
            Color.FromArgb(255, 144, 144, 155), // Plateado
            Color.FromArgb(255, 155, 155, 168), // Blanco

            Color.FromArgb(255, 161, 39, 35), // Red wool
            Color.FromArgb(255, 114, 72, 41), // Brown wool
            Color.FromArgb(255, 241, 118, 20), // Orange wool
            Color.FromArgb(255, 249, 198, 40), // Yellow wool
            Color.FromArgb(255, 112, 185, 26), // Lime wool
            Color.FromArgb(255, 85, 110, 28), // Green wool
            Color.FromArgb(255, 21, 138, 145), // Cyan wool
            Color.FromArgb(255, 58, 175, 217), // Light blue wool
            Color.FromArgb(255, 53, 57, 157), // Blue wool
            Color.FromArgb(255, 122, 42, 173), // Purple wool
            Color.FromArgb(255, 190, 69, 180), // Magenta wool
            Color.FromArgb(255, 238, 141, 172), // Pink wool
            Color.FromArgb(255, 21, 21, 26), // Black wool
            Color.FromArgb(255, 63, 68, 72), // Gray wool
            Color.FromArgb(255, 142, 142, 135), // Light gray wool
            Color.FromArgb(255, 234, 236, 237), // White wool

            Color.FromArgb(255, 76, 57, 22), // Stick

            Color.FromArgb(255, 232, 231, 230), // Banner
            Color.FromArgb(255, 58, 136, 65), // Vine
            Color.FromArgb(255, 147, 100, 87), // Bricks
            Color.FromArgb(255, 87, 147, 79), // Creeper head
            Color.FromArgb(255, 46, 47, 47), // Wither skeleton head
            Color.FromArgb(255, 176, 198, 139), // Oxeye daisy
            Color.FromArgb(255, 164, 150, 57), // Golden apple
            
            Color.FromArgb(0, 0, 0, 0) // Transparent
        };

        internal static List<Bitmap> Lista_Miniaturas_Diseños_Estandartes = null;

        /// <summary>
        /// Array that holds up the 16 basic minecraft colors, based on the average RGB colors from the concrete block textures.
        /// </summary>
        internal static readonly Color[] Matriz_16_Colores_ = new Color[16]
        {
            Color.FromArgb(255, 142, 33, 33), // Red
            Color.FromArgb(255, 96, 60, 32), // Brown
            Color.FromArgb(255, 224, 97, 1), // Orange
            Color.FromArgb(255, 241, 175, 21), // Yellow
            Color.FromArgb(255, 94, 169, 24), // Lime
            Color.FromArgb(255, 73, 91, 36), // Green
            Color.FromArgb(255, 21, 119, 136), // Cyan
            Color.FromArgb(255, 36, 137, 199), // Light blue
            Color.FromArgb(255, 45, 47, 143), // Blue
            Color.FromArgb(255, 100, 32, 156), // Purple
            Color.FromArgb(255, 169, 48, 159), // Magenta
            Color.FromArgb(255, 214, 101, 143), // Pink
            Color.FromArgb(255, 8, 10, 15), // Black
            Color.FromArgb(255, 55, 58, 62), // Gray
            Color.FromArgb(255, 125, 125, 115), // Light gray
            Color.FromArgb(255, 207, 213, 214) // White
        };

        /// <summary>
        /// Array that holds up the 16 basic colors, designed by Jupisoft and not related to Minecraft.
        /// </summary>
        internal static readonly Color[] Matriz_16_Colores = new Color[16]
        {
            Color.FromArgb(255, 255, 0, 0), // Rojo
            Color.FromArgb(255, 128, 0, 0), // Marrón
            Color.FromArgb(255, 255, 160, 0), // Naranja
            Color.FromArgb(255, 255, 255, 0), // Amarillo
            Color.FromArgb(255, 160, 255, 0), // Lima
            Color.FromArgb(255, 0, 160, 0), // Verde
            Color.FromArgb(255, 0, 255, 224), // Aguamarina
            Color.FromArgb(255, 0, 160, 255), // Azul claro
            Color.FromArgb(255, 0, 0, 255), // Azul
            Color.FromArgb(255, 160, 0, 255), // Púrpura
            Color.FromArgb(255, 255, 0, 255), // Magenta
            Color.FromArgb(255, 255, 0, 160), // Rosa
            Color.FromArgb(255, 0, 0, 0), // Negro
            Color.FromArgb(255, 128, 128, 128), // Gris
            Color.FromArgb(255, 192, 192, 192), // Gris claro
            Color.FromArgb(255, 255, 255, 255), // Blanco
        };

        internal Bitmap Obtener_Imagen_Diseño_Escudo(string Nombre_Textura)
        {
            try
            {
                if (!string.IsNullOrEmpty(Nombre_Textura) && string.Compare(Nombre_Textura, "None", true) != 0)
                {
                    string[] Matriz_Líneas = Nombre_Textura.Split(new string[] { "___" }, StringSplitOptions.RemoveEmptyEntries);
                    Nombre_Textura = Matriz_Líneas[0];
                    Matriz_Líneas = null;
                    Bitmap Imagen = null;
                    try { Imagen = (Bitmap)Resources.ResourceManager.GetObject("Shield_" + Nombre_Textura.ToLowerInvariant()); }
                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Imagen = null; }
                    if (Imagen != null) return Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        internal Bitmap Obtener_Imagen_Diseño_Estandarte(string Nombre_Textura)
        {
            try
            {
                if (!string.IsNullOrEmpty(Nombre_Textura) && string.Compare(Nombre_Textura, "None", true) != 0)
                {
                    string[] Matriz_Líneas = Nombre_Textura.Split(new string[] { "___" }, StringSplitOptions.RemoveEmptyEntries);
                    Nombre_Textura = Matriz_Líneas[0];
                    Matriz_Líneas = null;
                    Bitmap Imagen = null;
                    try { Imagen = (Bitmap)Resources.ResourceManager.GetObject("Banner_" + Nombre_Textura.ToLowerInvariant()); }
                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Imagen = null; }
                    if (Imagen != null) return Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        internal Bitmap Obtener_Imagen_Tinte(Materiales Material)
        {
            try
            {
                Bitmap Imagen = null;
                try { Imagen = (Bitmap)Resources.ResourceManager.GetObject("minecraft_" + Material.ToString().ToLowerInvariant()); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Imagen = null; }
                if (Imagen != null) return Imagen;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }
        internal static Point[,] Matriz_Posiciones_Recetas = null;

        internal Bitmap Obtener_Imagen_Receta(Diseños_Estandartes Diseño, Materiales Color_Material)
        {
            try
            {
                Bitmap Imagen = (Bitmap)Resources.Cuadrícula_Creación.Clone();
                Graphics Pintar = Graphics.FromImage(Imagen);
                Pintar.CompositingMode = CompositingMode.SourceOver;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.None;
                if (Diseño != Diseños_Estandartes.None)
                {
                    for (int Índice_Y = 0; Índice_Y < 3; Índice_Y++)
                    {
                        for (int Índice_X = 0; Índice_X < 3; Índice_X++)
                        {
                            if (Matriz_Materiales_Recetas[(int)Diseño, Índice_X, Índice_Y] == Materiales.Rose_red) // Replace with the current dye.
                            {
                                Pintar.DrawImage(Program.Obtener_Imagen_Recursos("minecraft_" + Color_Material.ToString().ToLowerInvariant()), new Rectangle(Matriz_Posiciones_Recetas[Índice_X, Índice_Y], new Size(16, 16)), new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                            }
                            else if (Matriz_Materiales_Recetas[(int)Diseño, Índice_X, Índice_Y] != Materiales.Unknown)
                            {
                                Pintar.DrawImage(Program.Obtener_Imagen_Recursos("minecraft_" + Matriz_Materiales_Recetas[(int)Diseño, Índice_X, Índice_Y].ToString().ToLowerInvariant()), new Rectangle(Matriz_Posiciones_Recetas[Índice_X, Índice_Y], new Size(16, 16)), new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                            }
                        }
                    }
                }
                else
                {
                    Bitmap Imagen_Lana = Program.Obtener_Imagen_Recursos("minecraft_" + ((Materiales)((int)Color_Material + 16)).ToString().ToLowerInvariant());
                    Pintar.DrawImage(Imagen_Lana, new Rectangle(Matriz_Posiciones_Recetas[0, 0], new Size(16, 16)), new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Imagen_Lana, new Rectangle(Matriz_Posiciones_Recetas[1, 0], new Size(16, 16)), new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Imagen_Lana, new Rectangle(Matriz_Posiciones_Recetas[2, 0], new Size(16, 16)), new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Imagen_Lana, new Rectangle(Matriz_Posiciones_Recetas[0, 1], new Size(16, 16)), new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Imagen_Lana, new Rectangle(Matriz_Posiciones_Recetas[1, 1], new Size(16, 16)), new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Imagen_Lana, new Rectangle(Matriz_Posiciones_Recetas[2, 1], new Size(16, 16)), new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Resources.Palo, new Rectangle(Matriz_Posiciones_Recetas[1, 2], new Size(16, 16)), new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                    Imagen_Lana.Dispose();
                    Imagen_Lana = null;
                }
                Pintar.Dispose();
                Pintar = null;
                return Imagen;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        internal ComboBox[] Matriz_ComboBox_Diseño = null;
        internal ComboBox[] Matriz_ComboBox_Colores = null;
        internal PictureBox[] Matriz_PictureBox_Diseño = null;
        internal PictureBox[] Matriz_PictureBox_Colores = null;
        internal Button[] Matriz_Botones_Subir = null;
        internal Button[] Matriz_Botones_Bajar = null;

        internal static List<byte[]> Lista_Matrices_Bytes_Materiales = null;
        internal static Materiales[,,] Matriz_Materiales_Recetas = null;

        internal static bool Añadir_Bordes_Estandartes_Escudos = true;
        internal static bool Autozoom_Imágenes_Estandarte_Escudo = true;
        internal static bool Usar_Nombres_Diseño_Minecraft = false;

        internal readonly string Texto_Título = "Banner and Shield Designer by Jupisoft for " + Program.Texto_Usuario;
        internal bool Variable_Siempre_Visible = false;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;
        internal bool Ocupado = false;

        internal List<Diseños_Estandartes> Lista_Diseños = new List<Diseños_Estandartes>(new Diseños_Estandartes[17] { Diseños_Estandartes.None, Diseños_Estandartes.None, Diseños_Estandartes.None, Diseños_Estandartes.None, Diseños_Estandartes.None, Diseños_Estandartes.None, Diseños_Estandartes.None, Diseños_Estandartes.None, Diseños_Estandartes.None, Diseños_Estandartes.None, Diseños_Estandartes.None, Diseños_Estandartes.None, Diseños_Estandartes.None, Diseños_Estandartes.None, Diseños_Estandartes.None, Diseños_Estandartes.None, Diseños_Estandartes.None });
        internal List<int> Lista_Colores = new List<int>(new int[17] { 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 });

        private void Ventana_Diseñador_Estandartes_Escudos_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                /*for (int Índice = 0; Índice < Minecraft.Bloques.Matriz_Bloques.Length; Índice++)
                {
                    Minecraft.Bloques.Matriz_Bloques[Índice].Imagen_Textura.Clone(new Rectangle(0, 0, Minecraft.Bloques.Matriz_Bloques[Índice].Imagen_Textura.Width, Minecraft.Bloques.Matriz_Bloques[Índice].Imagen_Textura.Height), PixelFormat.Format32bppArgb).Save(Application.StartupPath + "\\" + Minecraft.Bloques.Matriz_Bloques[Índice].Nombre.Replace(':', '_') + ".png", ImageFormat.Png);
                }*/
                string[] Matriz_Nombres = null;
                if (Lista_Matrices_Bytes_Materiales == null || Lista_Matrices_Bytes_Materiales.Count <= 0)
                {
                    Lista_Matrices_Bytes_Materiales = new List<byte[]>();
                    Matriz_Nombres = Enum.GetNames(typeof(Materiales));
                    if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                    {
                        foreach (string Nombre in Matriz_Nombres)
                        {
                            Bitmap Imagen = Program.Obtener_Imagen_Sobre_Fondo(string.Compare(Nombre, "Unknown", true) != 0 ? Program.Obtener_Imagen_Recursos("minecraft_" + Nombre.ToLowerInvariant()) : new Bitmap(16, 16, PixelFormat.Format32bppArgb), Color.FromArgb(255, 139, 139, 139));
                            Lista_Matrices_Bytes_Materiales.Add(Program.Obtener_Matriz_Bytes_Imagen(Imagen));
                            Imagen.Dispose();
                            Imagen = null;
                        }
                        Matriz_Nombres = null;
                    }
                }
                if (Matriz_Posiciones_Recetas == null || Matriz_Posiciones_Recetas.Length <= 0)
                {
                    Matriz_Posiciones_Recetas = new Point[3, 3];
                    Matriz_Posiciones_Recetas[0, 0] = new Point(4, 4);
                    Matriz_Posiciones_Recetas[1, 0] = new Point(22, 4);
                    Matriz_Posiciones_Recetas[2, 0] = new Point(40, 4);
                    Matriz_Posiciones_Recetas[0, 1] = new Point(4, 22);
                    Matriz_Posiciones_Recetas[1, 1] = new Point(22, 22);
                    Matriz_Posiciones_Recetas[2, 1] = new Point(40, 22);
                    Matriz_Posiciones_Recetas[0, 2] = new Point(4, 40);
                    Matriz_Posiciones_Recetas[1, 2] = new Point(22, 40);
                    Matriz_Posiciones_Recetas[2, 2] = new Point(40, 40);
                }
                if (Matriz_Materiales_Recetas == null || Matriz_Materiales_Recetas.Length <= 0)
                {
                    Matriz_Materiales_Recetas = new Materiales[(int)Diseños_Estandartes.Total, 3, 3];

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.None, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.None, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.None, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.None, 0, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.None, 1, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.None, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.None, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.None, 1, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.None, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Border___Bordure__, 0, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Border___Bordure__, 1, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Border___Bordure__, 2, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Border___Bordure__, 0, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Border___Bordure__, 1, 1] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Border___Bordure__, 2, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Border___Bordure__, 0, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Border___Bordure__, 1, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Border___Bordure__, 2, 2] = Materiales.Rose_red;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Bricks___Field_masoned__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Bricks___Field_masoned__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Bricks___Field_masoned__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Bricks___Field_masoned__, 0, 1] = Materiales.Bricks;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Bricks___Field_masoned__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Bricks___Field_masoned__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Bricks___Field_masoned__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Bricks___Field_masoned__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Bricks___Field_masoned__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Circle___Roundel__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Circle___Roundel__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Circle___Roundel__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Circle___Roundel__, 0, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Circle___Roundel__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Circle___Roundel__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Circle___Roundel__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Circle___Roundel__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Circle___Roundel__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Creeper___Creeper_charge__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Creeper___Creeper_charge__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Creeper___Creeper_charge__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Creeper___Creeper_charge__, 0, 1] = Materiales.Creeper_head;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Creeper___Creeper_charge__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Creeper___Creeper_charge__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Creeper___Creeper_charge__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Creeper___Creeper_charge__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Creeper___Creeper_charge__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Cross___Saltire__, 0, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Cross___Saltire__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Cross___Saltire__, 2, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Cross___Saltire__, 0, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Cross___Saltire__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Cross___Saltire__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Cross___Saltire__, 0, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Cross___Saltire__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Cross___Saltire__, 2, 2] = Materiales.Rose_red;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Curly_border___Bordure_indented__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Curly_border___Bordure_indented__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Curly_border___Bordure_indented__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Curly_border___Bordure_indented__, 0, 1] = Materiales.Vine;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Curly_border___Bordure_indented__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Curly_border___Bordure_indented__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Curly_border___Bordure_indented__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Curly_border___Bordure_indented__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Curly_border___Bordure_indented__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_left___Per_bend_sinister__, 0, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_left___Per_bend_sinister__, 1, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_left___Per_bend_sinister__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_left___Per_bend_sinister__, 0, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_left___Per_bend_sinister__, 1, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_left___Per_bend_sinister__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_left___Per_bend_sinister__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_left___Per_bend_sinister__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_left___Per_bend_sinister__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_right___Per_bend__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_right___Per_bend__, 1, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_right___Per_bend__, 2, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_right___Per_bend__, 0, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_right___Per_bend__, 1, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_right___Per_bend__, 2, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_right___Per_bend__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_right___Per_bend__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_right___Per_bend__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_up_left___Per_bend_inverted__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_up_left___Per_bend_inverted__, 1, 0] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_up_left___Per_bend_inverted__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_up_left___Per_bend_inverted__, 0, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_up_left___Per_bend_inverted__, 1, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_up_left___Per_bend_inverted__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_up_left___Per_bend_inverted__, 0, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_up_left___Per_bend_inverted__, 1, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_up_left___Per_bend_inverted__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_up_right___Per_bend_sinister_inverted__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_up_right___Per_bend_sinister_inverted__, 1, 0] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_up_right___Per_bend_sinister_inverted__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_up_right___Per_bend_sinister_inverted__, 0, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_up_right___Per_bend_sinister_inverted__, 1, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_up_right___Per_bend_sinister_inverted__, 2, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_up_right___Per_bend_sinister_inverted__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_up_right___Per_bend_sinister_inverted__, 1, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Diagonal_up_right___Per_bend_sinister_inverted__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Flower___Flower_charge__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Flower___Flower_charge__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Flower___Flower_charge__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Flower___Flower_charge__, 0, 1] = Materiales.Oxeye_daisy;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Flower___Flower_charge__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Flower___Flower_charge__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Flower___Flower_charge__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Flower___Flower_charge__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Flower___Flower_charge__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Gradient___Gradient__, 0, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Gradient___Gradient__, 1, 0] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Gradient___Gradient__, 2, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Gradient___Gradient__, 0, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Gradient___Gradient__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Gradient___Gradient__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Gradient___Gradient__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Gradient___Gradient__, 1, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Gradient___Gradient__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Gradient_up___Base_gradient__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Gradient_up___Base_gradient__, 1, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Gradient_up___Base_gradient__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Gradient_up___Base_gradient__, 0, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Gradient_up___Base_gradient__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Gradient_up___Base_gradient__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Gradient_up___Base_gradient__, 0, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Gradient_up___Base_gradient__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Gradient_up___Base_gradient__, 2, 2] = Materiales.Rose_red;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_horizontal___Per_fess__, 0, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_horizontal___Per_fess__, 1, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_horizontal___Per_fess__, 2, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_horizontal___Per_fess__, 0, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_horizontal___Per_fess__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_horizontal___Per_fess__, 2, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_horizontal___Per_fess__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_horizontal___Per_fess__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_horizontal___Per_fess__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_horizontal_bottom___Per_fess_inverted__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_horizontal_bottom___Per_fess_inverted__, 1, 0] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_horizontal_bottom___Per_fess_inverted__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_horizontal_bottom___Per_fess_inverted__, 0, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_horizontal_bottom___Per_fess_inverted__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_horizontal_bottom___Per_fess_inverted__, 2, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_horizontal_bottom___Per_fess_inverted__, 0, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_horizontal_bottom___Per_fess_inverted__, 1, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_horizontal_bottom___Per_fess_inverted__, 2, 2] = Materiales.Rose_red;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_vertical___Per_pale__, 0, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_vertical___Per_pale__, 1, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_vertical___Per_pale__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_vertical___Per_pale__, 0, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_vertical___Per_pale__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_vertical___Per_pale__, 2, 1] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_vertical___Per_pale__, 0, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_vertical___Per_pale__, 1, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_vertical___Per_pale__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_vertical_right___Per_pale_inverted__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_vertical_right___Per_pale_inverted__, 1, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_vertical_right___Per_pale_inverted__, 2, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_vertical_right___Per_pale_inverted__, 0, 1] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_vertical_right___Per_pale_inverted__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_vertical_right___Per_pale_inverted__, 2, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_vertical_right___Per_pale_inverted__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_vertical_right___Per_pale_inverted__, 1, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Half_vertical_right___Per_pale_inverted__, 2, 2] = Materiales.Rose_red;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Mojang___Thing__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Mojang___Thing__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Mojang___Thing__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Mojang___Thing__, 0, 1] = Materiales.Golden_apple;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Mojang___Thing__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Mojang___Thing__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Mojang___Thing__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Mojang___Thing__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Mojang___Thing__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Rhombus___Lozenge__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Rhombus___Lozenge__, 1, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Rhombus___Lozenge__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Rhombus___Lozenge__, 0, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Rhombus___Lozenge__, 1, 1] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Rhombus___Lozenge__, 2, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Rhombus___Lozenge__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Rhombus___Lozenge__, 1, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Rhombus___Lozenge__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Skull___Skull_charge__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Skull___Skull_charge__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Skull___Skull_charge__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Skull___Skull_charge__, 0, 1] = Materiales.Wither_skeleton_skull;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Skull___Skull_charge__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Skull___Skull_charge__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Skull___Skull_charge__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Skull___Skull_charge__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Skull___Skull_charge__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Small_stripes___Paly__, 0, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Small_stripes___Paly__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Small_stripes___Paly__, 2, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Small_stripes___Paly__, 0, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Small_stripes___Paly__, 1, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Small_stripes___Paly__, 2, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Small_stripes___Paly__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Small_stripes___Paly__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Small_stripes___Paly__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_bottom_left___Base_dexter_canton__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_bottom_left___Base_dexter_canton__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_bottom_left___Base_dexter_canton__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_bottom_left___Base_dexter_canton__, 0, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_bottom_left___Base_dexter_canton__, 1, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_bottom_left___Base_dexter_canton__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_bottom_left___Base_dexter_canton__, 0, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_bottom_left___Base_dexter_canton__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_bottom_left___Base_dexter_canton__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_bottom_right___Base_sinister_canton__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_bottom_right___Base_sinister_canton__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_bottom_right___Base_sinister_canton__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_bottom_right___Base_sinister_canton__, 0, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_bottom_right___Base_sinister_canton__, 1, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_bottom_right___Base_sinister_canton__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_bottom_right___Base_sinister_canton__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_bottom_right___Base_sinister_canton__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_bottom_right___Base_sinister_canton__, 2, 2] = Materiales.Rose_red;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_top_left___Chief_dexter_canton__, 0, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_top_left___Chief_dexter_canton__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_top_left___Chief_dexter_canton__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_top_left___Chief_dexter_canton__, 0, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_top_left___Chief_dexter_canton__, 1, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_top_left___Chief_dexter_canton__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_top_left___Chief_dexter_canton__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_top_left___Chief_dexter_canton__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_top_left___Chief_dexter_canton__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_top_right___Chief_sinister_canton__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_top_right___Chief_sinister_canton__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_top_right___Chief_sinister_canton__, 2, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_top_right___Chief_sinister_canton__, 0, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_top_right___Chief_sinister_canton__, 1, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_top_right___Chief_sinister_canton__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_top_right___Chief_sinister_canton__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_top_right___Chief_sinister_canton__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Square_top_right___Chief_sinister_canton__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Straight_cross___Cross__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Straight_cross___Cross__, 1, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Straight_cross___Cross__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Straight_cross___Cross__, 0, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Straight_cross___Cross__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Straight_cross___Cross__, 2, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Straight_cross___Cross__, 0, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Straight_cross___Cross__, 1, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Straight_cross___Cross__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_bottom___Base__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_bottom___Base__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_bottom___Base__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_bottom___Base__, 0, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_bottom___Base__, 1, 1] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_bottom___Base__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_bottom___Base__, 0, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_bottom___Base__, 1, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_bottom___Base__, 2, 2] = Materiales.Rose_red;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_center___Pale__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_center___Pale__, 1, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_center___Pale__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_center___Pale__, 0, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_center___Pale__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_center___Pale__, 2, 1] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_center___Pale__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_center___Pale__, 1, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_center___Pale__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_downleft___Bend_sinister__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_downleft___Bend_sinister__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_downleft___Bend_sinister__, 2, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_downleft___Bend_sinister__, 0, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_downleft___Bend_sinister__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_downleft___Bend_sinister__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_downleft___Bend_sinister__, 0, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_downleft___Bend_sinister__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_downleft___Bend_sinister__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_downright___Bend__, 0, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_downright___Bend__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_downright___Bend__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_downright___Bend__, 0, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_downright___Bend__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_downright___Bend__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_downright___Bend__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_downright___Bend__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_downright___Bend__, 2, 2] = Materiales.Rose_red;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_left___Pale_dexter__, 0, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_left___Pale_dexter__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_left___Pale_dexter__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_left___Pale_dexter__, 0, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_left___Pale_dexter__, 1, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_left___Pale_dexter__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_left___Pale_dexter__, 0, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_left___Pale_dexter__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_left___Pale_dexter__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_middle___Fess__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_middle___Fess__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_middle___Fess__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_middle___Fess__, 0, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_middle___Fess__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_middle___Fess__, 2, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_middle___Fess__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_middle___Fess__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_middle___Fess__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_right___Pale_sinister__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_right___Pale_sinister__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_right___Pale_sinister__, 2, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_right___Pale_sinister__, 0, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_right___Pale_sinister__, 1, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_right___Pale_sinister__, 2, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_right___Pale_sinister__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_right___Pale_sinister__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_right___Pale_sinister__, 2, 2] = Materiales.Rose_red;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_top___Chief__, 0, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_top___Chief__, 1, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_top___Chief__, 2, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_top___Chief__, 0, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_top___Chief__, 1, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_top___Chief__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_top___Chief__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_top___Chief__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Stripe_top___Chief__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangle_bottom___Chevron__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangle_bottom___Chevron__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangle_bottom___Chevron__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangle_bottom___Chevron__, 0, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangle_bottom___Chevron__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangle_bottom___Chevron__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangle_bottom___Chevron__, 0, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangle_bottom___Chevron__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangle_bottom___Chevron__, 2, 2] = Materiales.Rose_red;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangle_top___Inverted_chevron__, 0, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangle_top___Inverted_chevron__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangle_top___Inverted_chevron__, 2, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangle_top___Inverted_chevron__, 0, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangle_top___Inverted_chevron__, 1, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangle_top___Inverted_chevron__, 2, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangle_top___Inverted_chevron__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangle_top___Inverted_chevron__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangle_top___Inverted_chevron__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangles_bottom___Base_indented__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangles_bottom___Base_indented__, 1, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangles_bottom___Base_indented__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangles_bottom___Base_indented__, 0, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangles_bottom___Base_indented__, 1, 1] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangles_bottom___Base_indented__, 2, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangles_bottom___Base_indented__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangles_bottom___Base_indented__, 1, 2] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangles_bottom___Base_indented__, 2, 2] = Materiales.Unknown;

                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangles_top___Chief_indented__, 0, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangles_top___Chief_indented__, 1, 0] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangles_top___Chief_indented__, 2, 0] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangles_top___Chief_indented__, 0, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangles_top___Chief_indented__, 1, 1] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangles_top___Chief_indented__, 2, 1] = Materiales.Rose_red;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangles_top___Chief_indented__, 0, 2] = Materiales.Unknown;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangles_top___Chief_indented__, 1, 2] = Materiales.Banner;
                    Matriz_Materiales_Recetas[(int)Diseños_Estandartes.Triangles_top___Chief_indented__, 2, 2] = Materiales.Unknown;
                }
                Matriz_ComboBox_Diseño = new ComboBox[17]
                {
                    ComboBox_Diseño_1,
                    ComboBox_Diseño_2,
                    ComboBox_Diseño_3,
                    ComboBox_Diseño_4,
                    ComboBox_Diseño_5,
                    ComboBox_Diseño_6,
                    ComboBox_Diseño_7,
                    ComboBox_Diseño_8,
                    ComboBox_Diseño_9,
                    ComboBox_Diseño_10,
                    ComboBox_Diseño_11,
                    ComboBox_Diseño_12,
                    ComboBox_Diseño_13,
                    ComboBox_Diseño_14,
                    ComboBox_Diseño_15,
                    ComboBox_Diseño_16,
                    ComboBox_Diseño_17
                };
                Matriz_ComboBox_Colores = new ComboBox[17]
                {
                    ComboBox_Color_1,
                    ComboBox_Color_2,
                    ComboBox_Color_3,
                    ComboBox_Color_4,
                    ComboBox_Color_5,
                    ComboBox_Color_6,
                    ComboBox_Color_7,
                    ComboBox_Color_8,
                    ComboBox_Color_9,
                    ComboBox_Color_10,
                    ComboBox_Color_11,
                    ComboBox_Color_12,
                    ComboBox_Color_13,
                    ComboBox_Color_14,
                    ComboBox_Color_15,
                    ComboBox_Color_16,
                    ComboBox_Color_17
                };
                Matriz_PictureBox_Diseño = new PictureBox[17]
                {
                    Picture_Diseño_1,
                    Picture_Diseño_2,
                    Picture_Diseño_3,
                    Picture_Diseño_4,
                    Picture_Diseño_5,
                    Picture_Diseño_6,
                    Picture_Diseño_7,
                    Picture_Diseño_8,
                    Picture_Diseño_9,
                    Picture_Diseño_10,
                    Picture_Diseño_11,
                    Picture_Diseño_12,
                    Picture_Diseño_13,
                    Picture_Diseño_14,
                    Picture_Diseño_15,
                    Picture_Diseño_16,
                    Picture_Diseño_17
                };
                Matriz_PictureBox_Colores = new PictureBox[17]
                {
                    Picture_Color_1,
                    Picture_Color_2,
                    Picture_Color_3,
                    Picture_Color_4,
                    Picture_Color_5,
                    Picture_Color_6,
                    Picture_Color_7,
                    Picture_Color_8,
                    Picture_Color_9,
                    Picture_Color_10,
                    Picture_Color_11,
                    Picture_Color_12,
                    Picture_Color_13,
                    Picture_Color_14,
                    Picture_Color_15,
                    Picture_Color_16,
                    Picture_Color_17
                };
                Matriz_Botones_Subir = new Button[16]
                {
                    Botón_Subir_2,
                    Botón_Subir_3,
                    Botón_Subir_4,
                    Botón_Subir_5,
                    Botón_Subir_6,
                    Botón_Subir_7,
                    Botón_Subir_8,
                    Botón_Subir_9,
                    Botón_Subir_10,
                    Botón_Subir_11,
                    Botón_Subir_12,
                    Botón_Subir_13,
                    Botón_Subir_14,
                    Botón_Subir_15,
                    Botón_Subir_16,
                    Botón_Subir_17
                };
                Matriz_Botones_Bajar = new Button[16]
                {
                    Botón_Bajar_2,
                    Botón_Bajar_3,
                    Botón_Bajar_4,
                    Botón_Bajar_5,
                    Botón_Bajar_6,
                    Botón_Bajar_7,
                    Botón_Bajar_8,
                    Botón_Bajar_9,
                    Botón_Bajar_10,
                    Botón_Bajar_11,
                    Botón_Bajar_12,
                    Botón_Bajar_13,
                    Botón_Bajar_14,
                    Botón_Bajar_15,
                    Botón_Bajar_16,
                    Botón_Bajar_17
                };
                Ocupado = true;
                Registro_Cargar_Opciones();
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                Matriz_Nombres = Enum.GetNames(typeof(Preconfiguraciones_Estandartes));
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    foreach (string Nombre in Matriz_Nombres)
                    {
                        ComboBox_Preconfiguraciones.Items.Add(Nombre.Replace("___", " (").Replace("__", ")").Replace('_', ' '));
                    }
                    Matriz_Nombres = null;
                }
                for (int Índice_Capa = 0; Índice_Capa < 17; Índice_Capa++)
                {
                    Matriz_ComboBox_Diseño[Índice_Capa].SelectedIndex = Índice_Capa > 0 ? (int)Lista_Diseños[Índice_Capa] : 0;
                    Matriz_ComboBox_Colores[Índice_Capa].SelectedIndex = (int)Lista_Colores[Índice_Capa];
                }
                Ocupado = false;
                if (ComboBox_Preconfiguraciones.Items.Count > 0) ComboBox_Preconfiguraciones.SelectedIndex = 0;
                /*for (int Índice = 1; Índice <= 39; Índice++)
                {
                    Obtener_Imagen_Diseño_Estandarte(((Diseños_Estandartes)Índice).ToString()).Clone(new Rectangle(1, 0, 20, 40), PixelFormat.Format32bppArgb).Save(Application.StartupPath + "\\Banner_" + ((Diseños_Estandartes)Índice).ToString().ToLowerInvariant() + ".png", ImageFormat.Png);
                }
                if (Lista_Miniaturas_Diseños_Estandartes == null || Lista_Miniaturas_Diseños_Estandartes.Count <= 0)
                {*//*
                    Lista_Miniaturas_Diseños_Estandartes = new List<Bitmap>();
                    Matriz_Nombres = Enum.GetNames(typeof(Diseños_Estandartes));
                    if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                    {
                        foreach (string Nombre in Matriz_Nombres)
                        {
                        // Temporary function (now disabled) to cut and convert to an alpha mask the original Minecraft banner textures. Note that the top left and right pixels are repainted by hand before this function.
                        Bitmap Imagen = (Bitmap)Resources.ResourceManager.GetObject("Shield_" + Nombre.ToLowerInvariant().Replace("none", "base")); //Obtener_Imagen_Diseño_Estandarte(Nombre);
                            if (Imagen != null)
                            {
                                int Ancho = 10;
                                int Alto = 20;
                                Imagen = Imagen.Clone(new Rectangle(2, 2, Ancho, Alto), Imagen.PixelFormat);
                                BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                                byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                                Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                                int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                                int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                                for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                                {
                                    for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                                    {
                                        Matriz_Bytes[Índice + 3] = (byte)((Matriz_Bytes[Índice + 2] + Matriz_Bytes[Índice + 1] + Matriz_Bytes[Índice]) / 3);
                                    }
                                }
                                Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                                Imagen.UnlockBits(Bitmap_Data);
                                Bitmap_Data = null;
                                Matriz_Bytes = null;
                                Imagen.Save(Application.StartupPath + "\\Shield_" + Nombre.ToLowerInvariant().Replace("none", "base") + ".png", ImageFormat.Png);
                                Imagen.Dispose();
                                Imagen = null;
                            }
                            /*for (int Índice = 0; Índice < 39; Índice++)
                            {
                                Obtener_Imagen_Diseño_Estandarte(((Diseños_Estandartes)Índice).ToString()).Clone(new Rectangle(0, 1, 20, 40), PixelFormat.Format32bppArgb).Save(Application.StartupPath + "\\Banner_" + ((Diseños_Estandartes)Índice).ToString().ToLowerInvariant() + ".png", ImageFormat.Png);
                            }
                            Lista_Miniaturas_Diseños_Estandartes.Add(Obtener_Miniatura_Imagen(Obtener_Imagen_Diseño_Estandarte(Nombre), 16, 16, true, false));*//*
                        }
                    }
                    Matriz_Nombres = null;
                /*}*/
                /*for (int Índice_Fila = 0; Índice_Fila < 25; Índice_Fila++)
                {
                    //DataGridViewRow Fila = new DataGridViewRow();
                    //Fila.CreateCells(DataGridView_Principal);
                    DataGridView_Principal.Rows.Add(new object[] { true, Lista_Miniaturas_Diseños_Estandartes[0], "Base", Program.Obtener_Imagen_Color(Matriz_16_Colores[15]), "White" });
                }
                DataGridView_Principal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                DataGridView_Principal.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Diseñador_Estandartes_Escudos_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Diseñador_Estandartes_Escudos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Diseñador_Estandartes_Escudos_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Matriz_ComboBox_Diseño = null;
                Matriz_ComboBox_Colores = null;
                Matriz_PictureBox_Diseño = null;
                Matriz_PictureBox_Colores = null;
                Matriz_Botones_Subir = null;
                Matriz_Botones_Bajar = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Diseñador_Estandartes_Escudos_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                Generar_Estandarte();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Diseñador_Estandartes_Escudos_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Diseñador_Estandartes_Escudos_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
                {
                    string[] Matriz_Rutas = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                    if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                    {
                        foreach (string Ruta in Matriz_Rutas)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                                {
                                    FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                                    Image Imagen_Original = null;
                                    try { Imagen_Original = Image.FromStream(Lector, false, false); }
                                    catch { Imagen_Original = null; }
                                    if (Imagen_Original != null)
                                    {
                                        int Ancho = Imagen_Original.Width;
                                        int Alto = Imagen_Original.Height;
                                        int Total_Capas = Ancho / 60;
                                        if (Total_Capas > 17) Total_Capas = 17;
                                        if (Ancho >= 60 && Alto >= 60) // Ignore the rest of the image and cut only the upper left part for a faster decoding.
                                        {
                                            Ancho = 60 * Total_Capas;
                                            Alto = 60;
                                            Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                                            Graphics Pintar = Graphics.FromImage(Imagen);
                                            Pintar.CompositingMode = CompositingMode.SourceCopy;
                                            Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                            Pintar.Dispose();
                                            Pintar = null;
                                            Imagen_Original.Dispose();
                                            Imagen_Original = null;
                                            BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                                            byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                                            Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                                            Imagen.UnlockBits(Bitmap_Data);
                                            Bitmap_Data = null;
                                            Imagen.Dispose();
                                            Imagen = null;
                                            Materiales[,,] Matriz_Materiales = new Materiales[Total_Capas, 3, 3];
                                            for (int Índice_Capa = 0; Índice_Capa < Total_Capas; Índice_Capa++) // Read each crafting grid.
                                            {
                                                for (int Índice_Y = 0; Índice_Y < 3; Índice_Y++) // Read each vertical material.
                                                {
                                                    for (int Índice_X = 0; Índice_X < 3; Índice_X++) // Read each horizontal material.
                                                    {
                                                        Matriz_Materiales[Índice_Capa, Índice_X, Índice_Y] = Materiales.Unknown; // Default for not found.
                                                    }
                                                }
                                            }
                                            for (int Índice_Capa = 0, Índice = 0, Recetas_X = 0; Índice_Capa < Total_Capas; Índice_Capa++, Recetas_X += 60) // Read each crafting grid.
                                            {
                                                for (int Índice_Y = 0; Índice_Y < 3; Índice_Y++) // Read each vertical material.
                                                {
                                                    for (int Índice_X = 0; Índice_X < 3; Índice_X++) // Read each horizontal material.
                                                    {
                                                        int Índice_Mínimo = -1;
                                                        int Diferencia_Mínima = int.MaxValue;
                                                        for (int Índice_Material = 0; Índice_Material < Lista_Matrices_Bytes_Materiales.Count; Índice_Material++) // Read each valid material.
                                                        {
                                                            if ((Índice_Capa > 0 && (Índice_Material < (int)Materiales.Red_wool || Índice_Material > (int)Materiales.Stick)) || (Índice_Capa == 0 && Índice_Material >= (int)Materiales.Red_wool && Índice_Material <= (int)Materiales.Stick)) // Only allow wool and sticks in the first crafting grid, and the rest of materials only in the other crafting grids, or in other words, decode it quicker.
                                                            {
                                                                int Diferencia = 0;
                                                                for (int Y = 0, Índice_Lista = 0; Y < 16; Y++) // Read each column of pixels.
                                                                {
                                                                    Índice = ((Ancho * (Matriz_Posiciones_Recetas[Índice_X, Índice_Y].Y + Y)) + (Recetas_X + Matriz_Posiciones_Recetas[Índice_X, Índice_Y].X)) * 4; // Move the index to the start of the next row of pixels (it's a fast way to avoid cutting the image in hundreds of smaller images).
                                                                    for (int X = 0; X < 16; X++, Índice_Lista += 4, Índice += 4) // Read each row of pixels.
                                                                    {
                                                                        Diferencia += Math.Abs(Lista_Matrices_Bytes_Materiales[Índice_Material][Índice_Lista + 3] - Matriz_Bytes[Índice + 3]) + Math.Abs(Lista_Matrices_Bytes_Materiales[Índice_Material][Índice_Lista + 2] - Matriz_Bytes[Índice + 2]) + Math.Abs(Lista_Matrices_Bytes_Materiales[Índice_Material][Índice_Lista + 1] - Matriz_Bytes[Índice + 1]) + Math.Abs(Lista_Matrices_Bytes_Materiales[Índice_Material][Índice_Lista] - Matriz_Bytes[Índice]);
                                                                    }
                                                                }
                                                                if (Diferencia < Diferencia_Mínima)
                                                                {
                                                                    Índice_Mínimo = Índice_Material;
                                                                    Diferencia_Mínima = Diferencia;
                                                                }
                                                            }
                                                        }
                                                        if (Índice_Mínimo > -1) // A valid material has been found.
                                                        {
                                                            Matriz_Materiales[Índice_Capa, Índice_X, Índice_Y] = (Materiales)Índice_Mínimo;
                                                        }
                                                    }
                                                }
                                            }
                                            Ocupado = true;
                                            for (int Índice = 0; Índice < 17; Índice++)
                                            {
                                                if (Índice > 0) Matriz_ComboBox_Diseño[Índice].SelectedIndex = 0;
                                                Matriz_ComboBox_Colores[Índice].SelectedIndex = 15;
                                            }
                                            // Identify and select all the found materials inside the crafting grid.
                                            // Note: this function could be very useful for auto-loading recipes in...
                                            // other projects, so feel free to re-use this code if it can help you.
                                            for (int Índice_Capa = 0; Índice_Capa < Total_Capas; Índice_Capa++)
                                            {
                                                if (Índice_Capa > 0)
                                                {
                                                    int Índice_Diseño = 0;
                                                    for (int Índice_Material = 0; Índice_Material < (int)Diseños_Estandartes.Total; Índice_Material++) // Read each valid material.
                                                    {
                                                        int Materiales_Iguales = 0;
                                                        for (int Índice_Y = 0; Índice_Y < 3; Índice_Y++) // Read each vertical material.
                                                        {
                                                            for (int Índice_X = 0; Índice_X < 3; Índice_X++) // Read each horizontal material.
                                                            {
                                                                if (((int)Matriz_Materiales[Índice_Capa, Índice_X, Índice_Y] >= (int)Materiales.Rose_red && (int)Matriz_Materiales[Índice_Capa, Índice_X, Índice_Y] <= (int)Materiales.Bone_meal && Matriz_Materiales_Recetas[Índice_Material, Índice_X, Índice_Y] == Materiales.Rose_red) || (Matriz_Materiales[Índice_Capa, Índice_X, Índice_Y] == Matriz_Materiales_Recetas[Índice_Material, Índice_X, Índice_Y])) // If the image has a dye, and the recipe for the current design has it too, or if the 2 materials are the same.
                                                                {
                                                                    Materiales_Iguales++;
                                                                }
                                                            }
                                                        }
                                                        if (Materiales_Iguales >= 9) // Found a valid design.
                                                        {
                                                            Índice_Diseño = Índice_Material;
                                                            break;
                                                        }
                                                    }
                                                    if (Índice_Diseño > 0)
                                                    {
                                                        Establecer_Diseño(Índice_Capa, (Diseños_Estandartes)Índice_Diseño); // Set the found design.
                                                    }
                                                    bool Coloreado = false; // Search the color to use with the design.
                                                    for (int Índice_Y = 0; Índice_Y < 3; Índice_Y++) // Read each vertical material.
                                                    {
                                                        for (int Índice_X = 0; Índice_X < 3; Índice_X++) // Read each horizontal material.
                                                        {
                                                            if ((int)Matriz_Materiales[Índice_Capa, Índice_X, Índice_Y] >= (int)Materiales.Rose_red && (int)Matriz_Materiales[Índice_Capa, Índice_X, Índice_Y] <= (int)Materiales.Bone_meal)
                                                            {
                                                                Matriz_ComboBox_Colores[Índice_Capa].SelectedIndex = (int)Matriz_Materiales[Índice_Capa, Índice_X, Índice_Y]; // Set the designs dye colors.
                                                                Coloreado = true;
                                                                break;
                                                            }
                                                        }
                                                        if (Coloreado) break;
                                                    }
                                                }
                                                else
                                                {
                                                    if (Matriz_Materiales[Índice_Capa, 1, 1] >= Materiales.Red_wool && Matriz_Materiales[Índice_Capa, 1, 1] <= Materiales.White_wool)
                                                    {
                                                        ComboBox_Color_1.SelectedIndex = (int)Matriz_Materiales[Índice_Capa, 1, 1] - 16; // Set the base dye color.
                                                    }
                                                    else ComboBox_Color_1.SelectedIndex = 15; // White wool on missing dye.
                                                }
                                            }
                                            Ocupado = false;
                                            Generar_Estandarte();
                                        }
                                    }
                                    Lector.Close();
                                    Lector.Dispose();
                                    Lector = null;
                                }
                            }
                            catch (Exception Excepción)
                            {
                                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                continue;
                            }
                        }
                        Matriz_Rutas = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Ventana_Diseñador_Estandartes_Escudos_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Alt && !e.Control && !e.Shift)
                {
                    if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Delete)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        this.Close();
                    }
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

        private void Temporizador_Principal_Tick(object sender, EventArgs e)
        {
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
                        if ((Environment.TickCount / 500) % 2 == 0)
                        {
                            if (!Variable_Memoria)
                            {
                                Variable_Memoria = true;
                                Barra_Estado_Etiqueta_Memoria.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            if (Variable_Memoria)
                            {
                                Variable_Memoria = false;
                                Barra_Estado_Etiqueta_Memoria.ForeColor = Color.Black;
                            }
                        }
                    }
                }
                catch { Barra_Estado_Etiqueta_Memoria.Text = "RAM: ? MB (? GB)"; }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Diseños_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int Índice = int.Parse(((Control)sender).Name.Replace("ComboBox_Diseño_", null)) - 1;
                if (Índice > 0)
                {
                    Diseños_Estandartes Diseño = Diseños_Estandartes.None;
                    if (string.Compare(Matriz_ComboBox_Diseño[Índice].Text, "None", true) != 0)
                    {
                        if (!Usar_Nombres_Diseño_Minecraft)
                        {
                            Diseño = (Diseños_Estandartes)Enum.Parse(typeof(Diseños_Estandartes), Matriz_ComboBox_Diseño[Índice].Text.Replace(" (", "___").Replace(")", "__").Replace(' ', '_'));
                        }
                        else
                        {
                            string[] Matriz_Líneas = Matriz_ComboBox_Diseño[Índice].Text.Replace(" (", "___").Replace(")", null).Replace(' ', '_').Split(new string[] { "___" }, StringSplitOptions.RemoveEmptyEntries);
                            Diseño = (Diseños_Estandartes)Enum.Parse(typeof(Diseños_Estandartes), Matriz_Líneas[1] + "___" + Matriz_Líneas[0] + "__");
                            Matriz_Líneas = null;
                        }
                    }
                    Lista_Diseños[Índice] = Diseño;
                }
                Matriz_PictureBox_Diseño[Índice].Image = Program.Recolorear_Imagen(Índice > 0 ? Obtener_Imagen_Diseño_Estandarte(Lista_Diseños[Índice].ToString()) : Resources.Banner_base, Matriz_16_Colores[Lista_Colores[Índice]]);
                Matriz_PictureBox_Diseño[Índice].Refresh();
                Generar_Estandarte();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Colores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int Índice = int.Parse(((Control)sender).Name.Replace("ComboBox_Color_", null)) - 1;
                Lista_Colores[Índice] = ((ComboBox)Panel_Derecho.Controls["ComboBox_Color_" + (Índice + 1).ToString()]).SelectedIndex;
                Matriz_PictureBox_Diseño[Índice].Image = Program.Recolorear_Imagen(Índice > 0 ? Obtener_Imagen_Diseño_Estandarte(Lista_Diseños[Índice].ToString()) : Resources.Banner_base, Matriz_16_Colores[Lista_Colores[Índice]]);
                Matriz_PictureBox_Colores[Índice].BackColor = Matriz_16_Colores[Lista_Colores[Índice]];
                Matriz_PictureBox_Colores[Índice].Image = Obtener_Imagen_Tinte((Materiales)Lista_Colores[Índice]);
                Matriz_PictureBox_Diseño[Índice].Refresh();
                Matriz_PictureBox_Colores[Índice].Refresh();
                Generar_Estandarte();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botones_Restablecer_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                int Índice = int.Parse(((Control)sender).Name.Replace("Botón_Restablecer_", null)) - 1;
                if (Índice > 0) Matriz_ComboBox_Diseño[Índice].SelectedIndex = 0;
                Matriz_ComboBox_Colores[Índice].SelectedIndex = 15;
                Ocupado = false;
                Generar_Estandarte();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botones_Aleatorio_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                int Índice = int.Parse(((Control)sender).Name.Replace("Botón_Aleatorio_", null)) - 1;
                if (Índice > 0) Matriz_ComboBox_Diseño[Índice].SelectedIndex = Program.Rand.Next(0, Matriz_ComboBox_Diseño[Índice].Items.Count);
                Matriz_ComboBox_Colores[Índice].SelectedIndex = Program.Rand.Next(0, Matriz_ComboBox_Colores[Índice].Items.Count);
                Ocupado = false;
                Generar_Estandarte();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Subir_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                int Índice_Capa = int.Parse(((Control)sender).Name.Replace("Botón_Subir_", null)) - 1;
                if (Índice_Capa - 2 > -1)
                {
                    Matriz_Botones_Subir[Índice_Capa - 2].Select();
                    Matriz_Botones_Subir[Índice_Capa - 2].Focus();
                }
                else
                {
                    Matriz_Botones_Subir[Matriz_Botones_Subir.Length - 1].Select();
                    Matriz_Botones_Subir[Matriz_Botones_Subir.Length - 1].Focus();
                }
                Diseños_Estandartes Diseño_Estandarte = Lista_Diseños[Índice_Capa];
                int Índice_Color = Lista_Colores[Índice_Capa];
                Lista_Diseños.RemoveAt(Índice_Capa);
                Lista_Colores.RemoveAt(Índice_Capa);
                Índice_Capa--;
                if (Índice_Capa < 1) Índice_Capa = 16;
                Lista_Diseños.Insert(Índice_Capa, Diseño_Estandarte);
                Lista_Colores.Insert(Índice_Capa, Índice_Color);
                for (Índice_Capa = 1; Índice_Capa < 17; Índice_Capa++)
                {
                    Establecer_Diseño(Índice_Capa, Lista_Diseños[Índice_Capa]);
                    Matriz_ComboBox_Colores[Índice_Capa].SelectedIndex = (int)Lista_Colores[Índice_Capa];
                }
                Ocupado = false;
                Generar_Estandarte();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Bajar_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                int Índice_Capa = int.Parse(((Control)sender).Name.Replace("Botón_Bajar_", null)) - 1;
                if (Índice_Capa < Matriz_Botones_Bajar.Length)
                {
                    Matriz_Botones_Bajar[Índice_Capa].Select();
                    Matriz_Botones_Bajar[Índice_Capa].Focus();
                }
                else
                {
                    Matriz_Botones_Bajar[0].Select();
                    Matriz_Botones_Bajar[0].Focus();
                }
                Diseños_Estandartes Diseño_Estandarte = Lista_Diseños[Índice_Capa];
                int Índice_Color = Lista_Colores[Índice_Capa];
                Lista_Diseños.RemoveAt(Índice_Capa);
                Lista_Colores.RemoveAt(Índice_Capa);
                Índice_Capa++;
                if (Índice_Capa >= 17) Índice_Capa = 1;
                Lista_Diseños.Insert(Índice_Capa, Diseño_Estandarte);
                Lista_Colores.Insert(Índice_Capa, Índice_Color);
                for (Índice_Capa = 1; Índice_Capa < 17; Índice_Capa++)
                {
                    Establecer_Diseño(Índice_Capa, Lista_Diseños[Índice_Capa]);
                    Matriz_ComboBox_Colores[Índice_Capa].SelectedIndex = (int)Lista_Colores[Índice_Capa];
                }
                Ocupado = false;
                Generar_Estandarte();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Preconfiguraciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Preconfiguraciones.SelectedIndex > -1)
                {
                    Ocupado = true;
                    for (int Índice_Capa = 0; Índice_Capa < 17; Índice_Capa++) // Reset first all the layers.
                    {
                        Matriz_ComboBox_Diseño[Índice_Capa].SelectedIndex = 0;
                        Matriz_ComboBox_Colores[Índice_Capa].SelectedIndex = 15;
                    }
                    Preconfiguraciones_Estandartes Preconfiguración = (Preconfiguraciones_Estandartes)ComboBox_Preconfiguraciones.SelectedIndex;
                    Diseños_Estandartes[] Matriz_Diseños = null;
                    Colores[] Matriz_Colores = null;
                    if (Preconfiguración == Preconfiguraciones_Estandartes.Jupisoft_logo___by_Jupisoft__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[5]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Rhombus___Lozenge__,
                            Diseños_Estandartes.Rhombus___Lozenge__,
                            Diseños_Estandartes.Border___Bordure__,
                            Diseños_Estandartes.Border___Bordure__
                        };
                        Matriz_Colores = new Colores[5]
                        {
                            Colores.Red,
                            Colores.Black,
                            Colores.White,
                            Colores.Black,
                            Colores.White
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Wither___by_Jupisoft__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[7]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Rhombus___Lozenge__,
                            Diseños_Estandartes.Gradient___Gradient__,
                            Diseños_Estandartes.Half_horizontal___Per_fess__,
                            Diseños_Estandartes.Triangle_top___Inverted_chevron__,
                            Diseños_Estandartes.Stripe_top___Chief__,
                            Diseños_Estandartes.Skull___Skull_charge__
                        };
                        Matriz_Colores = new Colores[7]
                        {
                            Colores.White,
                            Colores.Black,
                            Colores.Black,
                            Colores.Light_gray,
                            Colores.White,
                            Colores.Light_gray,
                            Colores.Black
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Nether_portal___by_Jupisoft__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[7]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Gradient___Gradient__,
                            Diseños_Estandartes.Gradient_up___Base_gradient__,
                            Diseños_Estandartes.Gradient___Gradient__,
                            Diseños_Estandartes.Border___Bordure__,
                            Diseños_Estandartes.Border___Bordure__,
                            Diseños_Estandartes.Border___Bordure__
                        };
                        Matriz_Colores = new Colores[7]
                        {
                            Colores.Purple,
                            Colores.White,
                            Colores.Black,
                            Colores.Purple,
                            Colores.Black,
                            Colores.Black,
                            Colores.Black
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Flower___by_Jupisoft__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[7]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Gradient_up___Base_gradient__,
                            Diseños_Estandartes.Stripe_bottom___Base__,
                            Diseños_Estandartes.Straight_cross___Cross__,
                            Diseños_Estandartes.Circle___Roundel__,
                            Diseños_Estandartes.Triangle_top___Inverted_chevron__,
                            Diseños_Estandartes.Triangles_top___Chief_indented__
                        };
                        Matriz_Colores = new Colores[7]
                        {
                            Colores.Light_blue,
                            Colores.Red,
                            Colores.Green,
                            Colores.Lime,
                            Colores.Lime,
                            Colores.Red,
                            Colores.Orange
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Spain___by_Jupisoft__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[3]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Stripe_left___Pale_dexter__,
                            Diseños_Estandartes.Stripe_right___Pale_sinister__
                        };
                        Matriz_Colores = new Colores[3]
                        {
                            Colores.Yellow,
                            Colores.Red,
                            Colores.Red
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Spain_2___by_Jupisoft__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[3]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Stripe_top___Chief__,
                            Diseños_Estandartes.Stripe_bottom___Base__
                        };
                        Matriz_Colores = new Colores[3]
                        {
                            Colores.Yellow,
                            Colores.Red,
                            Colores.Red
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Angry_bunny___by_Jupisoft__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[7]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Circle___Roundel__,
                            Diseños_Estandartes.Gradient_up___Base_gradient__,
                            Diseños_Estandartes.Flower___Flower_charge__,
                            Diseños_Estandartes.Skull___Skull_charge__,
                            Diseños_Estandartes.Cross___Saltire__,
                            Diseños_Estandartes.Triangles_bottom___Base_indented__
                        };
                        Matriz_Colores = new Colores[7]
                        {
                            Colores.Green,
                            Colores.Red,
                            Colores.Black,
                            Colores.White,
                            Colores.Gray,
                            Colores.White,
                            Colores.White
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Landscape___by_Jupisoft__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[7]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Flower___Flower_charge__,
                            Diseños_Estandartes.Stripe_center___Pale__,
                            Diseños_Estandartes.Square_bottom_left___Base_dexter_canton__,
                            Diseños_Estandartes.Stripe_right___Pale_sinister__,
                            Diseños_Estandartes.Square_top_right___Chief_sinister_canton__,
                            Diseños_Estandartes.Square_bottom_right___Base_sinister_canton__
                        };
                        Matriz_Colores = new Colores[7]
                        {
                            Colores.Light_blue,
                            Colores.Green,
                            Colores.Light_blue,
                            Colores.Brown,
                            Colores.Light_blue,
                            Colores.Yellow,
                            Colores.Blue
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.End_city___by_Mojang__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[3]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Triangle_top___Inverted_chevron__,
                            Diseños_Estandartes.Triangle_bottom___Chevron__
                        };
                        Matriz_Colores = new Colores[3]
                        {
                            Colores.Magenta,
                            Colores.Black,
                            Colores.Black
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Allium_alliance___by_Stressmonster101__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[6]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Stripe_center___Pale__,
                            Diseños_Estandartes.Stripe_top___Chief__,
                            Diseños_Estandartes.Flower___Flower_charge__,
                            Diseños_Estandartes.Circle___Roundel__,
                            Diseños_Estandartes.Circle___Roundel__,
                        };
                        Matriz_Colores = new Colores[6]
                        {
                            Colores.Pink,
                            Colores.Lime,
                            Colores.Pink,
                            Colores.Purple,
                            Colores.Magenta,
                            Colores.Purple
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Heart___by_Stressmonster101__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[5]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Half_horizontal_bottom___Per_fess_inverted__,
                            Diseños_Estandartes.Half_horizontal___Per_fess__,
                            Diseños_Estandartes.Rhombus___Lozenge__,
                            Diseños_Estandartes.Triangle_top___Inverted_chevron__
                        };
                        Matriz_Colores = new Colores[5]
                        {
                            Colores.White,
                            Colores.Magenta,
                            Colores.Magenta,
                            Colores.Pink,
                            Colores.Magenta
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Skull_and_bones_on_fire___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[7]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Mojang___Thing__,
                            Diseños_Estandartes.Flower___Flower_charge__,
                            Diseños_Estandartes.Circle___Roundel__,
                            Diseños_Estandartes.Stripe_top___Chief__,
                            Diseños_Estandartes.Stripe_bottom___Base__,
                            Diseños_Estandartes.Skull___Skull_charge__
                        };
                        Matriz_Colores = new Colores[7]
                        {
                            Colores.Black,
                            Colores.Yellow,
                            Colores.Orange,
                            Colores.Red,
                            Colores.Black,
                            Colores.Black,
                            Colores.White
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Sun_of_Fire___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[6]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Gradient___Gradient__,
                            Diseños_Estandartes.Gradient___Gradient__,
                            Diseños_Estandartes.Flower___Flower_charge__,
                            Diseños_Estandartes.Circle___Roundel__,
                            Diseños_Estandartes.Circle___Roundel__
                        };
                        Matriz_Colores = new Colores[6]
                        {
                            Colores.Yellow,
                            Colores.Red,
                            Colores.Yellow,
                            Colores.Orange,
                            Colores.Orange,
                            Colores.Red
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Apple_in_hand___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[7]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Gradient___Gradient__,
                            Diseños_Estandartes.Stripe_bottom___Base__,
                            Diseños_Estandartes.Half_horizontal___Per_fess__,
                            Diseños_Estandartes.Circle___Roundel__,
                            Diseños_Estandartes.Mojang___Thing__,
                            Diseños_Estandartes.Border___Bordure__
                        };
                        Matriz_Colores = new Colores[7]
                        {
                            Colores.White,
                            Colores.Orange,
                            Colores.Brown,
                            Colores.Light_blue,
                            Colores.Red,
                            Colores.Light_blue,
                            Colores.Yellow
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Union_jack___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[7]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Stripe_downleft___Bend_sinister__,
                            Diseños_Estandartes.Stripe_downright___Bend__,
                            Diseños_Estandartes.Cross___Saltire__,
                            Diseños_Estandartes.Stripe_center___Pale__,
                            Diseños_Estandartes.Stripe_middle___Fess__,
                            Diseños_Estandartes.Straight_cross___Cross__
                        };
                        Matriz_Colores = new Colores[7]
                        {
                            Colores.Blue,
                            Colores.White,
                            Colores.White,
                            Colores.Red,
                            Colores.White,
                            Colores.White,
                            Colores.Red
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Symmetrical_pattern___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[6]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Cross___Saltire__,
                            Diseños_Estandartes.Rhombus___Lozenge__,
                            Diseños_Estandartes.Flower___Flower_charge__,
                            Diseños_Estandartes.Border___Bordure__,
                            Diseños_Estandartes.Circle___Roundel__
                        };
                        Matriz_Colores = new Colores[6]
                        {
                            Colores.White,
                            Colores.Red,
                            Colores.Red,
                            Colores.Red,
                            Colores.White,
                            Colores.White
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Mojang_logo___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[3]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Gradient___Gradient__,
                            Diseños_Estandartes.Mojang___Thing__
                        };
                        Matriz_Colores = new Colores[3]
                        {
                            Colores.Yellow,
                            Colores.Orange,
                            Colores.White
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Bug___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[7]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Rhombus___Lozenge__,
                            Diseños_Estandartes.Creeper___Creeper_charge__,
                            Diseños_Estandartes.Diagonal_left___Per_bend_sinister__,
                            Diseños_Estandartes.Diagonal_up_right___Per_bend_sinister_inverted__,
                            Diseños_Estandartes.Skull___Skull_charge__,
                            Diseños_Estandartes.Half_horizontal___Per_fess__
                        };
                        Matriz_Colores = new Colores[7]
                        {
                            Colores.White,
                            Colores.Black,
                            Colores.Black,
                            Colores.White,
                            Colores.White,
                            Colores.Black,
                            Colores.White
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Eye___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[3]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Rhombus___Lozenge__,
                            Diseños_Estandartes.Circle___Roundel__
                        };
                        Matriz_Colores = new Colores[3]
                        {
                            Colores.White,
                            Colores.White,
                            Colores.Magenta
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Mouth___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[4]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Triangles_top___Chief_indented__,
                            Diseños_Estandartes.Stripe_bottom___Base__,
                            Diseños_Estandartes.Triangles_bottom___Base_indented__
                        };
                        Matriz_Colores = new Colores[4]
                        {
                            Colores.Black,
                            Colores.White,
                            Colores.Red,
                            Colores.White
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Number_0___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[6]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Stripe_right___Pale_sinister__,
                            Diseños_Estandartes.Stripe_left___Pale_dexter__,
                            Diseños_Estandartes.Stripe_bottom___Base__,
                            Diseños_Estandartes.Stripe_top___Chief__,
                            Diseños_Estandartes.Border___Bordure__
                        };
                        Matriz_Colores = new Colores[6]
                        {
                            Colores.White,
                            Colores.Red,
                            Colores.Red,
                            Colores.Red,
                            Colores.Red,
                            Colores.White
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Number_1___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[6]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Stripe_top___Chief__,
                            Diseños_Estandartes.Stripe_right___Pale_sinister__,
                            Diseños_Estandartes.Stripe_center___Pale__,
                            Diseños_Estandartes.Stripe_bottom___Base__,
                            Diseños_Estandartes.Border___Bordure__
                        };
                        Matriz_Colores = new Colores[6]
                        {
                            Colores.White,
                            Colores.Red,
                            Colores.White,
                            Colores.Red,
                            Colores.Red,
                            Colores.White
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Number_2___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[7]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Stripe_bottom___Base__,
                            Diseños_Estandartes.Stripe_left___Pale_dexter__,
                            Diseños_Estandartes.Half_horizontal___Per_fess__,
                            Diseños_Estandartes.Stripe_middle___Fess__,
                            Diseños_Estandartes.Stripe_top___Chief__,
                            Diseños_Estandartes.Border___Bordure__
                        };
                        Matriz_Colores = new Colores[7]
                        {
                            Colores.White,
                            Colores.Red,
                            Colores.Red,
                            Colores.White,
                            Colores.Red,
                            Colores.Red,
                            Colores.White
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Number_3___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[6]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Stripe_top___Chief__,
                            Diseños_Estandartes.Stripe_middle___Fess__,
                            Diseños_Estandartes.Stripe_bottom___Base__,
                            Diseños_Estandartes.Stripe_right___Pale_sinister__,
                            Diseños_Estandartes.Border___Bordure__
                        };
                        Matriz_Colores = new Colores[6]
                        {
                            Colores.White,
                            Colores.Red,
                            Colores.Red,
                            Colores.Red,
                            Colores.Red,
                            Colores.White
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Number_4___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[6]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Half_horizontal___Per_fess__,
                            Diseños_Estandartes.Stripe_center___Pale__,
                            Diseños_Estandartes.Stripe_middle___Fess__,
                            Diseños_Estandartes.Stripe_right___Pale_sinister__,
                            Diseños_Estandartes.Border___Bordure__
                        };
                        Matriz_Colores = new Colores[6]
                        {
                            Colores.White,
                            Colores.Red,
                            Colores.White,
                            Colores.Red,
                            Colores.Red,
                            Colores.White
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Number_5___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[7]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Half_horizontal___Per_fess__,
                            Diseños_Estandartes.Half_vertical___Per_pale__,
                            Diseños_Estandartes.Stripe_bottom___Base__,
                            Diseños_Estandartes.Stripe_middle___Fess__,
                            Diseños_Estandartes.Stripe_top___Chief__,
                            Diseños_Estandartes.Border___Bordure__
                        };
                        Matriz_Colores = new Colores[7]
                        {
                            Colores.Red,
                            Colores.White,
                            Colores.White,
                            Colores.Red,
                            Colores.Red,
                            Colores.Red,
                            Colores.White
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Number_6___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[7]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Stripe_right___Pale_sinister__,
                            Diseños_Estandartes.Half_horizontal___Per_fess__,
                            Diseños_Estandartes.Stripe_middle___Fess__,
                            Diseños_Estandartes.Stripe_left___Pale_dexter__,
                            Diseños_Estandartes.Stripe_bottom___Base__,
                            Diseños_Estandartes.Border___Bordure__
                        };
                        Matriz_Colores = new Colores[7]
                        {
                            Colores.White,
                            Colores.Red,
                            Colores.White,
                            Colores.Red,
                            Colores.Red,
                            Colores.Red,
                            Colores.White
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Number_7___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[4]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Stripe_top___Chief__,
                            Diseños_Estandartes.Stripe_right___Pale_sinister__,
                            Diseños_Estandartes.Border___Bordure__
                        };
                        Matriz_Colores = new Colores[4]
                        {
                            Colores.White,
                            Colores.Red,
                            Colores.Red,
                            Colores.White
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Number_8___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[7]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Stripe_bottom___Base__,
                            Diseños_Estandartes.Stripe_middle___Fess__,
                            Diseños_Estandartes.Stripe_top___Chief__,
                            Diseños_Estandartes.Stripe_right___Pale_sinister__,
                            Diseños_Estandartes.Stripe_left___Pale_dexter__,
                            Diseños_Estandartes.Border___Bordure__
                        };
                        Matriz_Colores = new Colores[7]
                        {
                            Colores.White,
                            Colores.Red,
                            Colores.Red,
                            Colores.Red,
                            Colores.Red,
                            Colores.Red,
                            Colores.White
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Number_9___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[7]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Half_horizontal___Per_fess__,
                            Diseños_Estandartes.Stripe_center___Pale__,
                            Diseños_Estandartes.Stripe_middle___Fess__,
                            Diseños_Estandartes.Stripe_top___Chief__,
                            Diseños_Estandartes.Stripe_right___Pale_sinister__,
                            Diseños_Estandartes.Border___Bordure__
                        };
                        Matriz_Colores = new Colores[7]
                        {
                            Colores.White,
                            Colores.Red,
                            Colores.White,
                            Colores.Red,
                            Colores.Red,
                            Colores.Red,
                            Colores.White
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.United_states_of_america___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[3]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Small_stripes___Paly__,
                            Diseños_Estandartes.Square_top_left___Chief_dexter_canton__
                        };
                        Matriz_Colores = new Colores[3]
                        {
                            Colores.White,
                            Colores.Red,
                            Colores.Blue
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Brazil___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[3]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Rhombus___Lozenge__,
                            Diseños_Estandartes.Circle___Roundel__
                        };
                        Matriz_Colores = new Colores[3]
                        {
                            Colores.Lime,
                            Colores.Yellow,
                            Colores.Blue
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.England___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[3]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Stripe_middle___Fess__,
                            Diseños_Estandartes.Stripe_center___Pale__
                        };
                        Matriz_Colores = new Colores[3]
                        {
                            Colores.White,
                            Colores.Red,
                            Colores.Red
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Jamaica___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[4]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Triangle_top___Inverted_chevron__,
                            Diseños_Estandartes.Triangle_bottom___Chevron__,
                            Diseños_Estandartes.Cross___Saltire__
                        };
                        Matriz_Colores = new Colores[4]
                        {
                            Colores.Lime,
                            Colores.Black,
                            Colores.Black,
                            Colores.Yellow
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Japan___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[2]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Circle___Roundel__
                        };
                        Matriz_Colores = new Colores[2]
                        {
                            Colores.White,
                            Colores.Red
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Germany___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[3]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Stripe_bottom___Base__,
                            Diseños_Estandartes.Stripe_top___Chief__
                        };
                        Matriz_Colores = new Colores[3]
                        {
                            Colores.Red,
                            Colores.Yellow,
                            Colores.Black
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Curtain___by_Xisumavoid__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[2]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Triangles_top___Chief_indented__
                        };
                        Matriz_Colores = new Colores[2]
                        {
                            Colores.Light_blue,
                            Colores.Black
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Blue_bunny___by_LD_Shadowlady__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[7]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Rhombus___Lozenge__,
                            Diseños_Estandartes.Flower___Flower_charge__,
                            Diseños_Estandartes.Triangle_top___Inverted_chevron__,
                            Diseños_Estandartes.Cross___Saltire__,
                            Diseños_Estandartes.Curly_border___Bordure_indented__,
                            Diseños_Estandartes.Triangles_bottom___Base_indented__
                        };
                        Matriz_Colores = new Colores[7]
                        {
                            Colores.White,
                            Colores.Pink,
                            Colores.White,
                            Colores.Light_blue,
                            Colores.White,
                            Colores.Light_blue,
                            Colores.White
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Pink_bunny___by_LD_Shadowlady__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[7]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Circle___Roundel__,
                            Diseños_Estandartes.Flower___Flower_charge__,
                            Diseños_Estandartes.Triangle_top___Inverted_chevron__,
                            Diseños_Estandartes.Cross___Saltire__,
                            Diseños_Estandartes.Curly_border___Bordure_indented__,
                            Diseños_Estandartes.Triangles_bottom___Base_indented__
                        };
                        Matriz_Colores = new Colores[7]
                        {
                            Colores.White,
                            Colores.Pink,
                            Colores.White,
                            Colores.Pink,
                            Colores.White,
                            Colores.Pink,
                            Colores.White
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Ice_cream___by_LD_Shadowlady__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[7]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Rhombus___Lozenge__,
                            Diseños_Estandartes.Rhombus___Lozenge__,
                            Diseños_Estandartes.Stripe_middle___Fess__,
                            Diseños_Estandartes.Half_horizontal___Per_fess__,
                            Diseños_Estandartes.Circle___Roundel__,
                            Diseños_Estandartes.Border___Bordure__
                        };
                        Matriz_Colores = new Colores[7]
                        {
                            Colores.Pink,
                            Colores.Brown,
                            Colores.Orange,
                            Colores.White,
                            Colores.Pink,
                            Colores.White,
                            Colores.Pink
                        };
                    }
                    else if (Preconfiguración == Preconfiguraciones_Estandartes.Heart_message___by_LD_Shadowlady__)
                    {
                        Matriz_Diseños = new Diseños_Estandartes[7]
                        {
                            Diseños_Estandartes.None,
                            Diseños_Estandartes.Stripe_bottom___Base__,
                            Diseños_Estandartes.Border___Bordure__,
                            Diseños_Estandartes.Rhombus___Lozenge__,
                            Diseños_Estandartes.Circle___Roundel__,
                            Diseños_Estandartes.Triangle_top___Inverted_chevron__,
                            Diseños_Estandartes.Stripe_top___Chief__
                        };
                        Matriz_Colores = new Colores[7]
                        {
                            Colores.White,
                            Colores.Light_blue,
                            Colores.Light_blue,
                            Colores.White,
                            Colores.Red,
                            Colores.White,
                            Colores.Light_blue
                        };
                    }
                    if (Matriz_Diseños != null && Matriz_Colores != null && Matriz_Diseños.Length == Matriz_Colores.Length)
                    {
                        for (int Índice_Capa = 0; Índice_Capa < Matriz_Diseños.Length && Índice_Capa < 17; Índice_Capa++)
                        {
                            if (Índice_Capa > 0) Establecer_Diseño(Índice_Capa, Matriz_Diseños[Índice_Capa]);
                            Matriz_ComboBox_Colores[Índice_Capa].SelectedIndex = (int)Matriz_Colores[Índice_Capa];
                        }
                    }
                    Ocupado = false;
                    Generar_Estandarte();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Restablecer_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                for (int Índice_Capa = 0; Índice_Capa < 17; Índice_Capa++)
                {
                    if (Índice_Capa > 0) Matriz_ComboBox_Diseño[Índice_Capa].SelectedIndex = 0;
                    Matriz_ComboBox_Colores[Índice_Capa].SelectedIndex = 15;
                }
                Ocupado = false;
                Generar_Estandarte();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Aleatorio_Creativo_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                for (int Índice_Capa = 0; Índice_Capa < 17; Índice_Capa++)
                {
                    if (Índice_Capa > 0) Matriz_ComboBox_Diseño[Índice_Capa].SelectedIndex = Program.Rand.Next(0, Matriz_ComboBox_Diseño[Índice_Capa].Items.Count);
                    Matriz_ComboBox_Colores[Índice_Capa].SelectedIndex = Program.Rand.Next(0, 16);
                }
                Ocupado = false;
                Generar_Estandarte();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Aleatorio_Supervivencia_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                for (int Índice_Capa = 0; Índice_Capa < 17; Índice_Capa++)
                {
                    if (Índice_Capa < 7)
                    {
                        if (Índice_Capa > 0) Matriz_ComboBox_Diseño[Índice_Capa].SelectedIndex = Program.Rand.Next(0, Matriz_ComboBox_Diseño[Índice_Capa].Items.Count);
                        Matriz_ComboBox_Colores[Índice_Capa].SelectedIndex = Program.Rand.Next(0, 16);
                    }
                    else
                    {
                        Matriz_ComboBox_Diseño[Índice_Capa].SelectedIndex = 0;
                        Matriz_ComboBox_Colores[Índice_Capa].SelectedIndex = 15;
                    }
                }
                Ocupado = false;
                Generar_Estandarte();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    ComboBox Combo = sender as ComboBox;
                    if (Combo != null && Combo.Items.Count > 0)
                    {
                        Combo.SelectedIndex = Program.Rand.Next(0, Combo.Items.Count);
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Function that generates a new banner based on it's 17 possible layers.
        /// </summary>
        internal void Generar_Estandarte()
        {
            try
            {
                if (!Ocupado)
                {
                    int Ancho = 20;
                    int Alto = 40;
                    int Ancho_Escudo = 10;
                    int Alto_Escudo = 20;
                    int Ancho_Recetas = 0;
                    for (int Índice_Capa = 0; Índice_Capa < 17; Índice_Capa++)
                    {
                        if (Lista_Diseños[Índice_Capa] != Diseños_Estandartes.None || Índice_Capa == 0)
                        {
                            Ancho_Recetas += 60;
                        }
                    }
                    Bitmap Imagen_Recetas = new Bitmap(Ancho_Recetas, 120, PixelFormat.Format32bppArgb);
                    Graphics Pintar_Recetas = Graphics.FromImage(Imagen_Recetas);
                    Pintar_Recetas.Clear(Color.FromArgb(255, 198, 198, 198)); // Minecraft crafting grid color.
                    Pintar_Recetas.CompositingMode = CompositingMode.SourceOver;
                    Pintar_Recetas.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar_Recetas.InterpolationMode = InterpolationMode.NearestNeighbor;
                    Pintar_Recetas.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar_Recetas.SmoothingMode = SmoothingMode.None;
                    Bitmap Imagen_Escudo = new Bitmap(Ancho_Escudo, Alto_Escudo, PixelFormat.Format32bppArgb);
                    Graphics Pintar_Escudo = Graphics.FromImage(Imagen_Escudo);
                    Pintar_Escudo.Clear(Matriz_16_Colores[Lista_Colores[0]]);
                    Pintar_Escudo.CompositingMode = CompositingMode.SourceOver;
                    Pintar_Escudo.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar_Escudo.InterpolationMode = InterpolationMode.NearestNeighbor;
                    Pintar_Escudo.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar_Escudo.SmoothingMode = SmoothingMode.None;
                    Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.Clear(Matriz_16_Colores[Lista_Colores[0]]);
                    Pintar.CompositingMode = CompositingMode.SourceOver;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.None;
                    for (int Índice_Capa = 0, Recetas_X = 0; Índice_Capa < 17; Índice_Capa++)
                    {
                        if (Lista_Diseños[Índice_Capa] != Diseños_Estandartes.None || Índice_Capa == 0)
                        {
                            Pintar_Recetas.DrawImage(Resources.Cuadrícula_Creación_Resultado_Estandarte, new Rectangle(Recetas_X, 60, 60, 60), new Rectangle(0, 0, 60, 60), GraphicsUnit.Pixel);
                            Bitmap Imagen_Receta = Obtener_Imagen_Receta(Índice_Capa > 0 ? Lista_Diseños[Índice_Capa] : Diseños_Estandartes.None, (Materiales)Lista_Colores[Índice_Capa]);
                            if (Imagen_Receta != null)
                            {
                                Pintar_Recetas.DrawImage(Imagen_Receta, new Rectangle(Recetas_X, 0, 60, 60), new Rectangle(0, 0, 60, 60), GraphicsUnit.Pixel);
                                Imagen_Receta.Dispose();
                                Imagen_Receta = null;
                            }
                            Bitmap Imagen_Capa_Escudo = Índice_Capa > 0 ? Obtener_Imagen_Diseño_Escudo(Lista_Diseños[Índice_Capa].ToString()) : Resources.Shield_base;
                            if (Imagen_Capa_Escudo != null)
                            {
                                Imagen_Capa_Escudo = Program.Recolorear_Imagen(Imagen_Capa_Escudo, Matriz_16_Colores[Lista_Colores[Índice_Capa]]);
                                Pintar_Escudo.DrawImage(Imagen_Capa_Escudo, new Rectangle(0, 0, Ancho_Escudo, Alto_Escudo), new Rectangle(0, 0, Ancho_Escudo, Alto_Escudo), GraphicsUnit.Pixel);
                                Imagen_Capa_Escudo.Dispose();
                                Imagen_Capa_Escudo = null;
                            }
                            Bitmap Imagen_Capa = Índice_Capa > 0 ? Obtener_Imagen_Diseño_Estandarte(Lista_Diseños[Índice_Capa].ToString()) : Resources.Banner_base;
                            if (Imagen_Capa != null)
                            {
                                Imagen_Capa = Program.Recolorear_Imagen(Imagen_Capa, Matriz_16_Colores[Lista_Colores[Índice_Capa]]);
                                Pintar.DrawImage(Imagen_Capa, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                Imagen_Capa.Dispose();
                                Imagen_Capa = null;
                            }
                            Pintar_Recetas.DrawImage(Imagen_Escudo, new Rectangle(Recetas_X + 10, 90, 10, 20), new Rectangle(0, 0, 10, 20), GraphicsUnit.Pixel);
                            Pintar_Recetas.DrawImage(Imagen, new Rectangle(Recetas_X + 35, 70, 20, 40), new Rectangle(0, 0, 20, 40), GraphicsUnit.Pixel);
                            Recetas_X += 60;
                        }
                    }
                    Pintar_Recetas.Dispose();
                    Pintar_Recetas = null;
                    Pintar_Escudo.Dispose();
                    Pintar_Escudo = null;
                    Pintar.Dispose();
                    Pintar = null;
                    if (Añadir_Bordes_Estandartes_Escudos)
                    {
                        Bitmap Imagen_Fondo_Escudo = Program.Obtener_Imagen_Sobre_Fondo(new Bitmap(12, 22, PixelFormat.Format32bppArgb), Color.FromArgb(255, 145, 147, 155)); // The only color in the front border of a shield.
                        Pintar_Escudo = Graphics.FromImage(Imagen_Fondo_Escudo);
                        Pintar_Escudo.CompositingMode = CompositingMode.SourceCopy;
                        Pintar_Escudo.DrawImage(Imagen_Escudo, new Rectangle(1, 1, 10, 20), new Rectangle(0, 0, 10, 20), GraphicsUnit.Pixel);
                        Pintar_Escudo.Dispose();
                        Pintar_Escudo = null;
                        Imagen_Escudo = Imagen_Fondo_Escudo;

                        Bitmap Imagen_Fondo_Estandarte = new Bitmap(22, 48, PixelFormat.Format32bppArgb);
                        Pintar = Graphics.FromImage(Imagen_Fondo_Estandarte);
                        Pintar.CompositingMode = CompositingMode.SourceCopy;
                        Pintar.DrawImage(Imagen, new Rectangle(1, 4, 20, 40), new Rectangle(0, 0, 20, 40), GraphicsUnit.Pixel);
                        using (SolidBrush Pincel = new SolidBrush(Color.FromArgb(255, 113, 92, 61))) Pintar.FillRectangle(Pincel, 10, 44, 1, 1); // Draw the holder of a banner below it.
                        using (SolidBrush Pincel = new SolidBrush(Color.FromArgb(255, 94, 75, 47))) Pintar.FillRectangle(Pincel, 11, 44, 1, 2);
                        using (SolidBrush Pincel = new SolidBrush(Color.FromArgb(255, 121, 99, 67))) Pintar.FillRectangle(Pincel, 10, 45, 1, 1);
                        using (SolidBrush Pincel = new SolidBrush(Color.FromArgb(255, 121, 100, 68))) Pintar.FillRectangle(Pincel, 10, 46, 1, 1);
                        using (SolidBrush Pincel = new SolidBrush(Color.FromArgb(255, 114, 93, 62))) Pintar.FillRectangle(Pincel, 11, 46, 1, 1);
                        using (SolidBrush Pincel = new SolidBrush(Color.FromArgb(255, 113, 92, 61))) Pintar.FillRectangle(Pincel, 10, 47, 2, 1);
                        Pintar.Dispose();
                        Pintar = null;
                        Imagen = Imagen_Fondo_Estandarte;

                        //Picture_Escudo.BackgroundImage = Imagen_Escudo.Clone() as Bitmap;
                        //Picture.BackgroundImage = Imagen.Clone(new Rectangle(1, 4, 20, 44), Imagen_Escudo.PixelFormat);
                    }
                    else
                    {
                        //Picture_Escudo.BackgroundImage = Imagen_Escudo.Clone() as Bitmap;
                        //Picture.BackgroundImage = Imagen.Clone() as Bitmap;
                    }
                    int Zoom = 1;
                    if (Autozoom_Imágenes_Estandarte_Escudo)
                    {
                        Imagen = Program.Obtener_Imagen_Autozoom(Imagen, Picture.ClientSize.Width, Picture.ClientSize.Height, true, CheckState.Unchecked, out Zoom);
                        Imagen_Escudo = Program.Obtener_Imagen_Zoom(Imagen_Escudo, Zoom * 2, CheckState.Unchecked);
                    }
                    this.Text = Texto_Título + " - [Recipe dimensions: " + Program.Traducir_Número(Ancho_Recetas) + " x 120" + (Ancho + Alto != 1 ? " pixels" : " pixel") + ", Autozooms: " + Program.Traducir_Número(Zoom * 2) + "x, " + Program.Traducir_Número(Zoom) + "x]";
                    Picture_Recetas.Image = Imagen_Recetas;
                    Picture_Escudo.Image = Imagen_Escudo;
                    Picture.Image = Imagen;
                    Picture_Recetas.Refresh();
                    Picture_Escudo.Refresh();
                    Picture.Refresh();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.Banner_and_shield_designer;
                Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Acerca_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Acerca Ventana = new Ventana_Acerca();
                Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Depurador_Excepciones_Click(object sender, EventArgs e)
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
                Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Abrir_Carpeta_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Diseñador_Estandartes);
                Program.Ejecutar_Ruta(Program.Ruta_Guardado_Imágenes_Diseñador_Estandartes, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture_Recetas.Image != null)
                {
                    Clipboard.SetImage(Picture_Recetas.Image);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Estandarte_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.Image != null)
                {
                    Clipboard.SetImage(Picture.Image);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Escudo_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture_Escudo.Image != null)
                {
                    Clipboard.SetImage(Picture_Escudo.Image);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture_Recetas.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Diseñador_Estandartes);
                    Picture_Recetas.Image.Save(Program.Ruta_Guardado_Imágenes_Diseñador_Estandartes + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " Banner crafting recipe.png", ImageFormat.Png);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Estandarte_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Diseñador_Estandartes);
                    Picture.Image.Save(Program.Ruta_Guardado_Imágenes_Diseñador_Estandartes + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " Banner design.png", ImageFormat.Png);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Escudo_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture_Escudo.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Diseñador_Estandartes);
                    Picture_Escudo.Image.Save(Program.Ruta_Guardado_Imágenes_Diseñador_Estandartes + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " Shield design.png", ImageFormat.Png);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Establecer_Diseño(int Índice_Capa, Diseños_Estandartes Diseño)
        {
            try
            {
                if (Índice_Capa > 0 && Índice_Capa < 17)
                {
                    string Nombre = Diseño.ToString();
                    if (Diseño != Diseños_Estandartes.None)
                    {
                        if (!Usar_Nombres_Diseño_Minecraft) // Use the Minecraft texture naming.
                        {
                            Nombre = Nombre.Replace("___", " (").Replace("__", ")").Replace('_', ' ');
                        }
                        else
                        {
                            string[] Matriz_Líneas = Nombre.Replace("___", " (").Replace("__", null).Replace('_', ' ').Split(new string[] { " (" }, StringSplitOptions.RemoveEmptyEntries);
                            Nombre = Matriz_Líneas[1] + " (" + Matriz_Líneas[0] + ")";
                            Matriz_Líneas = null;
                        }
                    }
                    Matriz_ComboBox_Diseño[Índice_Capa].Text = Nombre;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Generar_Estandarte();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Añadir_Bordes_Estandartes_Escudos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Añadir_Bordes_Estandartes_Escudos = Menú_Contextual_Añadir_Bordes_Estandartes_Escudos.Checked;
                Registro_Guardar_Opciones();
                Generar_Estandarte();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Autozoom_Imágenes_Estandarte_Escudo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Autozoom_Imágenes_Estandarte_Escudo = Menú_Contextual_Autozoom_Imágenes_Estandarte_Escudo.Checked;
                Registro_Guardar_Opciones();
                Generar_Estandarte();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Usar_Nombres_Diseño_Minecraft_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Usar_Nombres_Diseño_Minecraft = Menú_Contextual_Usar_Nombres_Diseño_Minecraft.Checked;
                Registro_Guardar_Opciones();
                Ocupado = true; // Disable any drawing of images while changing the values.
                for (int Índice_Capa = 1; Índice_Capa < 17; Índice_Capa++)
                {
                    Matriz_ComboBox_Diseño[Índice_Capa].Items.Clear(); // Clear all the previous names for the 16 layers.
                }
                if (!Usar_Nombres_Diseño_Minecraft) // Use the Minecraft texture naming.
                {
                    for (int Índice_Capa = 1; Índice_Capa < 17; Índice_Capa++)
                    {
                        Matriz_ComboBox_Diseño[Índice_Capa].Items.Add(Diseños_Estandartes.None.ToString()); // Be sure the first entry will always be "None" for the 16 layers.
                    }
                    string[] Matriz_Nombres = Enum.GetNames(typeof(Diseños_Estandartes));
                    if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                    {
                        Matriz_Nombres = new List<string>(Matriz_Nombres).GetRange(1, Matriz_Nombres.Length - 2).ToArray(); // Remove the first and last entries in the array.
                        for (int Índice_Nombre = 0; Índice_Nombre < Matriz_Nombres.Length; Índice_Nombre++)
                        {
                            Matriz_Nombres[Índice_Nombre] = Matriz_Nombres[Índice_Nombre].Replace("___", " (").Replace("__", ")").Replace('_', ' '); // There's no need for sorting since this names already are.
                            for (int Índice_Capa = 1; Índice_Capa < 17; Índice_Capa++)
                            {
                                Matriz_ComboBox_Diseño[Índice_Capa].Items.Add(Matriz_Nombres[Índice_Nombre]); // Set the new names.
                            }
                        }
                        Matriz_Nombres = null;
                    }
                }
                else // Use the Minecraft in-game naming.
                {
                    for (int Índice_Capa = 1; Índice_Capa < 17; Índice_Capa++)
                    {
                        Matriz_ComboBox_Diseño[Índice_Capa].Items.Add(Diseños_Estandartes.None.ToString()); // Be sure the first entry will always be "None" for the 16 layers.
                    }
                    string[] Matriz_Nombres = Enum.GetNames(typeof(Diseños_Estandartes));
                    if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                    {
                        Matriz_Nombres = new List<string>(Matriz_Nombres).GetRange(1, Matriz_Nombres.Length - 2).ToArray(); // Remove the first and last entries in the array.
                        for (int Índice_Nombre = 0; Índice_Nombre < Matriz_Nombres.Length; Índice_Nombre++)
                        {
                            string[] Matriz_Líneas = Matriz_Nombres[Índice_Nombre].Replace("___", " (").Replace("__", null).Replace('_', ' ').Split(new string[] { " (" }, StringSplitOptions.RemoveEmptyEntries);
                            Matriz_Nombres[Índice_Nombre] = Matriz_Líneas[1] + " (" + Matriz_Líneas[0] + ")";
                            Matriz_Líneas = null;
                        }
                        Array.Sort(Matriz_Nombres); // Sort the new names in the array.
                        for (int Índice_Nombre = 0; Índice_Nombre < Matriz_Nombres.Length; Índice_Nombre++)
                        {
                            for (int Índice_Capa = 1; Índice_Capa < 17; Índice_Capa++)
                            {
                                Matriz_ComboBox_Diseño[Índice_Capa].Items.Add(Matriz_Nombres[Índice_Nombre]); // Set the new names.
                            }
                        }
                        Matriz_Nombres = null;
                    }
                }
                for (int Índice_Capa = 1; Índice_Capa < 17; Índice_Capa++) // Set once again the selected designs, this will require to reconvert the selected designs.
                {
                    Establecer_Diseño(Índice_Capa, Lista_Diseños[Índice_Capa]);
                }
                Ocupado = false; // Allow once again for the drawing of images.
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Copiar_Código_Fuente_Click(object sender, EventArgs e)
        {
            try
            {
                string Texto = "else if (Preconfiguración == Preconfiguraciones_Estandartes.Custom_" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + "___by_" + Program.Texto_Usuario + "__)\r\n{\r\n";
                int Total_Capas = 1;
                for (int Índice_Capa = 1; Índice_Capa < 17; Índice_Capa++)
                {
                    if (Lista_Diseños[Índice_Capa] != Diseños_Estandartes.None) Total_Capas++;
                }
                Texto += "    Matriz_Diseños = new Diseños_Estandartes[" + Total_Capas + "]\r\n    {\r\n";
                for (int Índice_Capa = 0; Índice_Capa < Total_Capas; Índice_Capa++)
                {
                    Texto += "        Diseños_Estandartes." + Lista_Diseños[Índice_Capa].ToString() + ",\r\n";
                }
                Texto = Texto.TrimEnd(",\r\n".ToCharArray()) + "\r\n    };\r\n";
                Texto += "    Matriz_Colores = new Colores[" + Total_Capas + "]\r\n    {\r\n";
                for (int Índice_Capa = 0; Índice_Capa < Total_Capas; Índice_Capa++)
                {
                    Texto += "        Colores." + ((Colores)Lista_Colores[Índice_Capa]).ToString() + ",\r\n";
                }
                Texto = Texto.TrimEnd(",\r\n".ToCharArray()) + "\r\n    };\r\n}";
                Clipboard.SetText(Texto);
                Texto = null;
                SystemSounds.Asterisk.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Registro_Cargar_Opciones()
        {
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Banner and Shield Designer");
                try { Añadir_Bordes_Estandartes_Escudos = bool.Parse((string)Clave.GetValue("Add_Banner_Shield_Borders", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Añadir_Bordes_Estandartes_Escudos = true; }
                try { Autozoom_Imágenes_Estandarte_Escudo = bool.Parse((string)Clave.GetValue("Autozoom_Banner_Shield_Images", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Autozoom_Imágenes_Estandarte_Escudo = true; }
                try { Usar_Nombres_Diseño_Minecraft = bool.Parse((string)Clave.GetValue("Use_Minecraft_Design_Names", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Usar_Nombres_Diseño_Minecraft = true; }

                // Correct any bad value after loading:
                // ...

                // Apply all the loaded values:
                Menú_Contextual_Añadir_Bordes_Estandartes_Escudos.Checked = Añadir_Bordes_Estandartes_Escudos;
                Menú_Contextual_Autozoom_Imágenes_Estandarte_Escudo.Checked = Autozoom_Imágenes_Estandarte_Escudo;
                Menú_Contextual_Usar_Nombres_Diseño_Minecraft.Checked = Usar_Nombres_Diseño_Minecraft;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal void Registro_Guardar_Opciones()
        {
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Banner and Shield Designer");
                string[] Matriz_Nombres = Clave.GetValueNames();
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                    {
                        Clave.DeleteValue(Matriz_Nombres[Índice]);
                    }
                }
                Matriz_Nombres = null;
                try { Clave.SetValue("Add_Banner_Shield_Borders", Añadir_Bordes_Estandartes_Escudos, RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                try { Clave.SetValue("Autozoom_Banner_Shield_Images", Autozoom_Imágenes_Estandarte_Escudo, RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                try { Clave.SetValue("Use_Minecraft_Design_Names", Usar_Nombres_Diseño_Minecraft.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal void Registro_Restablecer_Opciones()
        {
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Banner and Shield Designer");
                string[] Matriz_Nombres = Clave.GetValueNames();
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                    {
                        try { Clave.DeleteValue(Matriz_Nombres[Índice]); }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    Matriz_Nombres = null;
                }
                Clave.Close();
                Clave = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Banners_Xisumavoid_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://xisumavoid.com/banners/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Subir_Banners_Xisumavoid_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://xisumavoid.com/server/banner", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Subir_Mapa_Banners_Xisumavoid_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://xisumavoid.com/downloads/Banners.zip", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }
    }
}
