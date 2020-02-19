using ImageMagick;
using Minecraft_Tools.Properties;
using System;
using System.Collections;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    /// <summary>
    /// WARNING: Using any of the default .NET functions to copy from screen with a Graphics object or
    /// even copy the current cursor to draw it in real time or so will eventually crash any application
    /// since .NET is bugged and doesn't release properly the GDI handles, so please never draw the
    /// current cursor or use the copy from screen methods. The method "BitBlt" used here instead is
    /// safe and it should never crash because of any GDI handles.
    /// </summary>
    public partial class Ventana_Filtros_Tiempo_Real : Form
    {
        public Ventana_Filtros_Tiempo_Real()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Enumeration that defines the image filters supported by this tool.
        /// </summary>
        internal enum Filtros : int
        {
            Original = 0,
            Negative,
            Experimental,
            Auto_level,
            Flip_horizontally,
            Flip_vertically,
            Rotate_180_degrees,
            Brightness_negative,
            Brightness_positive,
            Contrast_negative,
            Contrast_positive,
            Saturation_negative,
            Saturation_positive,
            Lightness_negative,
            Lightness_positive,
            Intensity_negative,
            Intensity_positive,
            Gamma_negative,
            Gamma_positive,
            Square_root_negative,
            Square_root_positive,
            Logarithm_negative,
            Logarithm_positive__night_vision___,
            Normalization,
            Centered_normalization,
            Compression_GIF,
            Compression_JPEG,
            Pixelate,
            Sepia,
            Painting,
            Solarize_minimum,
            Solarize_medium,
            Solarize_maximum,
            Metalize_minimum,
            Metalize_medium,
            Metalize_maximum,
            Reflection_horizontal,
            Reflection_vertical,
            Reflection_diagonal,
            Reflection_quadruple,
            Noise_in_gray_scale,
            Noise_in_color,
            Mono_chrome,
            Mono_chrome_automatic,
            Threshold,
            Threshold_automatic,
            Blur_minimum,
            Blur_median,
            Blur_maximum,
            Blur_mean,
            Focus,
            Focus_maximum,
            Borders,
            Borders_maximum,
            Edges,
            Edges_maximum,
            Posterize_2_tones,
            Posterize_4_tones,
            Posterize_8_tones,
            Posterize_16_tones,
            Posterize_32_tones,
            Posterize_64_tones,
            Posterize_128_tones,
            Differences_over_time__use_on_video___,
            Minimum_difference,
            Minimum_difference_for_JPEG_1,
            Minimum_difference_for_JPEG_2,
            Minimum_difference_for_JPEG_3,
            Minimum_difference_for_JPEG_4,
            Minimum_difference_for_JPEG_5,
            Minimum_difference_for_JPEG_6,
            Minimum_difference_for_JPEG_7,
            Minimum_difference_for_JPEG_8,
            Minimum_difference_for_JPEG_9,
            Minimum_difference_for_JPEG_10,
            Minimum_difference_for_JPEG_11,
            Minimum_difference_for_JPEG_12,
            Minimum_difference_for_JPEG_13,
            Minimum_difference_for_JPEG_14,
            Minimum_difference_for_JPEG_15,
            Minimum_difference_for_JPEG_16,
            Minimum_difference_for_JPEG_17,
            Minimum_difference_for_JPEG_18,
            Minimum_difference_for_JPEG_19,
            Minimum_difference_for_JPEG_20,
            Minimum_difference_for_JPEG_21,
            Minimum_difference_for_JPEG_22,
            Minimum_difference_for_JPEG_23,
            Minimum_difference_for_JPEG_24,
            Minimum_difference_for_JPEG_25,
            Minimum_difference_for_JPEG_26,
            Minimum_difference_for_JPEG_27,
            Minimum_difference_for_JPEG_28,
            Minimum_difference_for_JPEG_29,
            Minimum_difference_for_JPEG_30,
            Minimum_difference_for_JPEG_31,
            Minimum_difference_for_JPEG_32,
            Minimum_difference_for_JPEG_33,
            Minimum_difference_for_JPEG_34,
            Minimum_difference_for_JPEG_35,
            Minimum_difference_for_JPEG_36,
            Minimum_difference_for_JPEG_37,
            Minimum_difference_for_JPEG_38,
            Minimum_difference_for_JPEG_39,
            Minimum_difference_for_JPEG_40,
            Minimum_difference_for_JPEG_41,
            Minimum_difference_for_JPEG_42,
            Minimum_difference_for_JPEG_43,
            Minimum_difference_for_JPEG_44,
            Minimum_difference_for_JPEG_45,
            Minimum_difference_for_JPEG_46,
            Minimum_difference_for_JPEG_47,
            Minimum_difference_for_JPEG_48,
            Minimum_difference_for_JPEG_49,
            Minimum_difference_for_JPEG_50,
            Minimum_difference_for_JPEG_51,
            Minimum_difference_for_JPEG_52,
            Minimum_difference_for_JPEG_53,
            Minimum_difference_for_JPEG_54,
            Minimum_difference_for_JPEG_55,
            Minimum_difference_for_JPEG_56,
            Minimum_difference_for_JPEG_57,
            Minimum_difference_for_JPEG_58,
            Minimum_difference_for_JPEG_59,
            Minimum_difference_for_JPEG_60,
            Minimum_difference_for_JPEG_61,
            Minimum_difference_for_JPEG_62,
            Minimum_difference_for_JPEG_63,
            Minimum_difference_for_JPEG_64,
            Minimum_difference_for_JPEG_72,
            Minimum_difference_for_JPEG_80,
            Minimum_difference_for_JPEG_88,
            Minimum_difference_for_JPEG_96,
            Minimum_difference_for_JPEG_104,
            Minimum_difference_for_JPEG_112,
            Minimum_difference_for_JPEG_120,
            Minimum_difference_for_JPEG_128,
            Minimum_difference_for_JPEG_136,
            Minimum_difference_for_JPEG_144,
            Minimum_difference_for_JPEG_152,
            Minimum_difference_for_JPEG_160,
            Minimum_difference_for_JPEG_168,
            Minimum_difference_for_JPEG_176,
            Minimum_difference_for_JPEG_184,
            Minimum_difference_for_JPEG_192,
            Minimum_difference_for_JPEG_200,
            /*Minimum_difference_for_JPEG_208,
            Minimum_difference_for_JPEG_216,
            Minimum_difference_for_JPEG_224,
            Minimum_difference_for_JPEG_232,
            Minimum_difference_for_JPEG_240,
            Minimum_difference_for_JPEG_248,
            Minimum_difference_for_JPEG_256,*/
            Mean_difference,
            Maximum_difference,
            Rainbow_at_30_degrees,
            Rainbow_at_30_degrees_filled,
            Pixels_in_color,
            Pixels_in_gray_scale,
            Hue_red,
            Hue_orange,
            Hue_yellow,
            Hue_lime,
            Hue_green,
            Hue_turquoise,
            Hue_cyan,
            Hue_light_blue,
            Hue_blue,
            Hue_purple,
            Hue_magenta,
            Hue_pink,
            Swap_RGB_colors_to_RBG,
            Swap_RGB_colors_to_GRB,
            Swap_RGB_colors_to_GBR,
            Swap_RGB_colors_to_BRG,
            Swap_RGB_colors_to_BGR,
            Termography,
            Variable_termography__3D_X____Ray___,
            Sine_horizontal_waves,
            Sine_vertical_waves,
            Sine_horizontal_and_vertical_waves,
            Triangular_horizontal_waves,
            Triangular_vertical_waves,
            Triangular_horizontal_and_vertical_waves,
            Bit_operation_and,
            Bit_operation_or,
            Bit_operation_xor,
            Minimum_RGB,
            Median_RGB,
            Maximum_RGB,
            Red,
            Yellow,
            Green,
            Cyan,
            Blue,
            Magenta,
            Hue,
            Saturation,
            Lightness,
            Negative_Minimum_RGB,
            Negative_Median_RGB,
            Negative_Maximum_RGB,
            Negative_Red,
            Negative_Yellow,
            Negative_Green,
            Negative_Cyan,
            Negative_Blue,
            Negative_Magenta,
            Negative_Hue_inverted,
            Negative_Hue,
            Negative_Saturation,
            Negative_Lightness,
            Desaturate_Minimum_RGB,
            Desaturate_Median_RGB,
            Desaturate_Maximum_RGB,
            Desaturate_Red,
            Desaturate_Yellow,
            Desaturate_Green,
            Desaturate_Cyan,
            Desaturate_Blue,
            Desaturate_Magenta,
            Desaturate_Hue,
            Desaturate_Saturation,
            Desaturate_Lightness,
            HSL_to_RGB,
            HSL_to_RBG,
            HSL_to_GRB,
            HSL_to_GBR,
            HSL_to_BRG,
            HSL_to_BGR,
            RGB_to_HSL,
            RGB_to_HLS,
            RGB_to_SHL,
            RGB_to_SLH,
            RGB_to_LHS,
            RGB_to_LSH,
            Cyan_color_channel,
            Magenta_color_channel,
            Yellow_color_channel,
            Black_color_channel,
            Image_magick_filter,
            Minimum_difference_angle,
        }

        /// <summary>
        /// Enumeration that defines the zoom levels supported by this tool.
        /// </summary>
        internal enum Zooms : int
        {
            Zoom_1x = 0,
            Zoom_2x,
            Zoom_4x,
            Zoom_8x,
            Zoom_16x,
            Zoom_32x,
            Zoom_64x,
            Zoom_128x,
            Zoom_256x
        }
        
        internal static readonly Rectangle Rectángulo_Pantalla = Screen.PrimaryScreen.Bounds;
        internal static bool Variable_Negativo = false;
        internal static bool Variable_Negativo_Posterior = false;
        internal static bool Variable_Desaturado_Anterior = false;
        internal static bool Variable_Desaturado_Posterior = false;
        internal static Filtros Variable_Filtro = Filtros.Original;
        internal static Zooms Variable_Zoom = Zooms.Zoom_1x;
        internal static bool Variable_Zoom_Suave = false;
        internal static bool Variable_Seguir_Cursor = true;
        internal static bool Variable_Mantener_Cursor_Centrado = false;

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        internal readonly string Texto_Título = "Real Time Filters by Jupisoft for " + Program.Texto_Usuario;
        internal bool Variable_Siempre_Visible = true;
        internal bool Variable_Pantalla_Completa = false;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;
        internal static Stopwatch FPS_Cronómetro = Stopwatch.StartNew();
        internal long FPS_Segundo_Anterior = 0L;
        internal long FPS_Temporal = 0L;
        internal long FPS_Real = 0L;
        internal bool Ocupado = false;

        internal int Índice_Externo_Onda_X = 0;
        internal int Índice_Externo_Onda_Y = 0;
        internal int Índice_Termografía = 0;

        /// <summary>
        /// Used to see what pixels change over time.
        /// </summary>
        internal byte[] Matriz_Bytes_Anterior = null;
        internal byte[] Matriz_Bytes_Anterior_Filtrada = null;

        private void Ventana_Filtros_Tiempo_Real_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                /*// 336; 265.
                // 400; 301.
                double Relación_Aspecto_Pantalla = (double)Screen.PrimaryScreen.Bounds.Width / (double)Screen.PrimaryScreen.Bounds.Height;
                if (Math.Abs(Relación_Aspecto_Pantalla - (4d / 3d)) <
                    Math.Abs(Relación_Aspecto_Pantalla - (16d / 9d))) // Using a 4:3 screen.
                {
                    this.Height += 60; // 320 x 240 instead of 320 x 180.
                }*/
                //MessageBox.Show(Environment.OSVersion.Version.ToString());
                this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) + /*4*/(SystemInformation.FixedFrameBorderSize.Width * 2) + 2, 23); // Move the window at the top right of the screen for Windows 8.1.
                //this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) + 8, 23); // Move the window at the top right of the screen for Windows 8.1.
                ////this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) + 8, -1); // Move the window at the top right of the screen for Windows 8.1.
                //this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) - 101, -1); // Move the window at the top right of the screen for Windows 8.1.
                //this.Location = new Point(this.Location.X, 0); // Move the window at the top of the screen.
                //this.Location = new Point(this.Location.X, -this.PointToScreen(new Point(0, 0)).Y); // Move the window at the top of the screen.
                string[] Matriz_Nombres = Enum.GetNames(typeof(Filtros));
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    foreach (string Nombre in Matriz_Nombres)
                    {
                        ComboBox_Filtro.Items.Add(Nombre.Replace("____", "-").Replace("___", ")").Replace("__", " (").Replace("_", " "));
                    }
                }
                else ComboBox_Filtro.Items.Add("Original");
                Menú_Contextual_Negativo.Checked = Variable_Negativo;
                Menú_Contextual_Negativo_Posterior.Checked = Variable_Negativo_Posterior;
                Menú_Contextual_Desaturado_Anterior.Checked = Variable_Desaturado_Anterior;
                Menú_Contextual_Desaturado_Posterior.Checked = Variable_Desaturado_Posterior;
                ComboBox_Filtro.SelectedIndex = (int)Variable_Filtro; // Load the previously used options.
                Menú_Contextual_Zoom_Suave.Checked = Variable_Zoom_Suave;
                ComboBox_Zoom.SelectedIndex = (int)Variable_Zoom; // TODO: load from the registry.
                Menú_Contextual_Seguir_Cursor.Checked = Variable_Seguir_Cursor;
                Menú_Contextual_Siempre_Visible.Checked = Variable_Siempre_Visible;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
                Menú_Contextual_GitHub.Enabled = string.Compare(Environment.UserName, "Jupisoft") == 0;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Filtros_Tiempo_Real_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Filtros_Tiempo_Real_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Variable_Pantalla_Completa) Menú_Contextual_Pantalla_Completa.PerformClick();
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Filtros_Tiempo_Real_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Filtros_Tiempo_Real_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Filtros_Pantalla_Tiempo_Real_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Filtros_Pantalla_Tiempo_Real_DragDrop(object sender, DragEventArgs e)
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
                                        Imagen = Filtrar_Imagen(Imagen, Variable_Filtro);
                                        Imagen.Save(Program.Obtener_Ruta_Temporal_Escritorio() + " " + Path.GetFileNameWithoutExtension(Ruta) + ".png", ImageFormat.Png);
                                        Imagen.Dispose();
                                        Imagen = null;
                                        Imagen_Original.Dispose();
                                        Imagen_Original = null;
                                        Lector.Close();
                                        Lector.Dispose();
                                        Lector = null;
                                        SystemSounds.Asterisk.Play();
                                        return;
                                    }
                                    Lector.Close();
                                    Lector.Dispose();
                                    Lector = null;
                                    //XNA_Jupisoft.Convertir_XNB_a_WAV(Ruta, Program.Obtener_Ruta_Temporal_Escritorio());
                                }
                            }
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                        }
                    }
                    Matriz_Rutas = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }
        
        private void Ventana_Filtros_Tiempo_Real_KeyDown(object sender, KeyEventArgs e)
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
                    else if (e.KeyCode == Keys.Enter)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Menú_Contextual_Pantalla_Completa.PerformClick();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    Menú_Contextual_Pantalla_Completa.PerformClick();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Filtro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Filtro.SelectedIndex > -1)
                {
                    Índice_Termografía = 0;
                    Variable_Filtro = (Filtros)ComboBox_Filtro.SelectedIndex;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Zoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Zoom.SelectedIndex > -1)
                {
                    Variable_Zoom = (Zooms)ComboBox_Zoom.SelectedIndex;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Registro_Cargar_Opciones()
        {
            try
            {
                /*RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Template");

                // bool
                try { Variable_ = bool.Parse((string)Clave.GetValue("Variable_", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_ = true; }

                // int
                try { Variable_ = (int)Clave.GetValue("Variable_", 0); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_ = 0; }
                
                // Correct any bad value after loading:
                if ((int)Variable_ < 0 || (int)Variable_ > (int)Variables.Variable) Variable_ = Variables.Variable;

                // Apply all the loaded values:
                ComboBox_Variable_.SelectedIndex = (int)Variable_;

                Menú_Contextual_Variable_.Checked = Variable_;*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Registro_Guardar_Opciones()
        {
            try
            {
                /*RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Template");
                string[] Matriz_Nombres = Clave.GetValueNames();
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                    {
                        Clave.DeleteValue(Matriz_Nombres[Índice]);
                    }
                }
                Matriz_Nombres = null;
                
                // bool
                try { Clave.SetValue("Variable_", Variable_doDaylightCycle.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                // int
                try { Clave.SetValue("Tickspeed", (int)Variable_, RegistryValueKind.DWord); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Registro_Restablecer_Opciones()
        {
            try
            {
                /*RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Template");
                string[] Matriz_Nombres = Clave.GetValueNames();
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                    {
                        try { Clave.DeleteValue(Matriz_Nombres[Índice]); }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                    }
                    Matriz_Nombres = null;
                }
                Clave.Close();
                Clave = null;*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.Edición_Aplicación != CheckState.Checked)
                {
                    Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                    Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.Main_window;
                    Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                    Ventana.ShowDialog(this);
                    Ventana.Dispose();
                    Ventana = null;
                }
                else MessageBox.Show(this, "Esta ventana contiene decenas de filtros de imagen avanzados.\r\n\r\nSelecciona el que desees utilizar de la lista superior y mueve el cursor sobre la zona de la pantalla que desees filtrar.\r\n\r\nEn teoría debería soportar hasta vídeos que se reproduzcan en otras aplicaciones al igual que cualquier fotografía.\r\n\r\nPero ahora incluye soporte para arrastrar y soltar cualquier imagen, la cual será filtrada y guardada en el escritorio, siempre como una imagen nueva en PNG para no perder calidad. Por lo que tus imágenes originales jamás serán modificadas de ninguna forma.\r\n\r\nEl programa incluye opciones avanzadas como muchos niveles de zoom y suavizado variable.\r\n\r\nAlgunos de los filtros incluidos son variables con el tiempo, o sea que hay que dejar el cursor un rato sobre la misma zona para ir viendo como durante un rato va variando la imagen, permitiendo ver así detalles invisibles a simple vista.\r\n\r\nLa mayoría de los filtros incluidos han sido diseñados por Jupisoft desde el 2004 hasta hoy, así que son fruto de mucho trabajo y cálculos matemáticos complejos, aunque a la vez basados en fórmulas bastante simples de entender.\r\n\r\nSi aún tienes dudas sobre cualquier función del programa, por favor envía un correo a Jupitermauro@gmail.com, muchísimas gracias.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Question);
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

        private void Menú_Contextual_Siempre_Visible_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Siempre_Visible = Menú_Contextual_Siempre_Visible.Checked;
                this.TopMost = Variable_Siempre_Visible;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Mover_Cursor_Centro_Click(object sender, EventArgs e)
        {
            try
            {
                Point Posición = Picture.PointToScreen(new Point(Picture.ClientSize.Width / 2, Picture.ClientSize.Height / 2));
                PInvoke.User32.SetCursorPos(Posición.X, Posición.Y);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Mantener_Cursor_Centrado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Mantener_Cursor_Centrado = Menú_Contextual_Mantener_Cursor_Centrado.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Pantalla_Completa_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Pantalla_Completa = Menú_Contextual_Pantalla_Completa.Checked;
                if (!Variable_Pantalla_Completa)
                {
                    Temporizador_Principal.Stop();
                    Picture.Image = null;
                    if (this.TopMost != Variable_Siempre_Visible) this.TopMost = Variable_Siempre_Visible;
                    this.Opacity = 1d;
                    this.TransparencyKey = Color.Transparent;
                    this.AllowTransparency = false;
                    this.WindowState = FormWindowState.Normal;
                    this.FormBorderStyle = FormBorderStyle.FixedSingle;
                    Cursor.Show();
                    Tabla_Principal.Visible = true;
                    Barra_Estado.Visible = true;
                    Menú_Contextual_Siempre_Visible.Enabled = true;
                    Temporizador_Principal.Start();
                }
                else
                {
                    Temporizador_Principal.Stop();
                    Picture.Image = null;
                    Menú_Contextual_Siempre_Visible.Enabled = false;
                    Barra_Estado.Visible = false;
                    Tabla_Principal.Visible = false;
                    Cursor.Hide();
                    if (!Variable_Siempre_Visible) this.TopMost = true;
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                    this.AllowTransparency = true;
                    this.TransparencyKey = Color.Black;
                    this.Opacity = 0.5d;
                    Temporizador_Principal.Start();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
                if (Picture.Image != null)
                {
                    Clipboard.SetImage(Picture.Image);
                    SystemSounds.Asterisk.Play();
                }
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
                if (Picture.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Filtros_Tiempo_Real);
                    Picture.Image.Save(Program.Ruta_Guardado_Imágenes_Filtros_Tiempo_Real + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + ".png", ImageFormat.Png);
                    SystemSounds.Asterisk.Play();
                }
                Temporizador_Principal.Start();
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
                    }
                    catch { Barra_Estado_Etiqueta_Memoria.Text = "RAM: ? MB (? GB)"; }
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                if (!Variable_GitHub)
                {
                    if ((Control.ModifierKeys & Keys.Alt) != Keys.Alt &&
                                (Control.ModifierKeys & Keys.Control) != Keys.Control)
                    {
                        if (Variable_Mantener_Cursor_Centrado)
                        {
                            Point Posición_Cursor = Control.MousePosition;
                            Point Posición = Picture.PointToScreen(new Point(Picture.ClientSize.Width / 2, Picture.ClientSize.Height / 2));
                            if (Posición_Cursor.X != Posición.X || Posición_Cursor.Y != Posición.Y) PInvoke.User32.SetCursorPos(Posición.X, Posición.Y);
                        }
                        Rectangle Rectángulo = new Rectangle(Variable_Seguir_Cursor ? Control.MousePosition : new Point(Rectángulo_Pantalla.Width / 2, Rectángulo_Pantalla.Height / 2), Picture.ClientSize);
                        int Ancho = Rectángulo.Width;
                        int Alto = Rectángulo.Height;
                        if (Ancho > 0 && Alto > 0)
                        {
                            Rectángulo.X -= (Rectángulo.Width / 2);
                            Rectángulo.Y -= (Rectángulo.Height / 2);

                            if (Rectángulo.X < -128) Rectángulo.X = -128;
                            else if (Rectángulo.X + Rectángulo.Width >= Rectángulo_Pantalla.Width + 128) Rectángulo.X = (Rectángulo_Pantalla.Width + 128) - Rectángulo.Width;
                            if (Rectángulo.Y < -128) Rectángulo.Y = -128;
                            else if (Rectángulo.Y + Rectángulo.Height >= Rectángulo_Pantalla.Height + 128) Rectángulo.Y = (Rectángulo_Pantalla.Height + 128) - Rectángulo.Height;

                            if (Variable_Pantalla_Completa)
                            {
                                //Picture.Visible = false;
                                //Picture.Image = null;
                                //Picture.Refresh();
                            }

                            if (Variable_Filtro != Filtros.Minimum_difference_angle)
                            {
                                Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format24bppRgb);
                                Graphics Pintar = Graphics.FromImage(Imagen);
                                //Pintar.CopyFromScreen(Rectángulo.X, Rectángulo.Y, 0, 0, Imagen.Size, Filtro != Filtros.Negativo ? CopyPixelOperation.SourceCopy : CopyPixelOperation.NotSourceCopy);
                                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero)) // Desktop window?
                                {
                                    IntPtr hSrcDC = gsrc.GetHdc();
                                    IntPtr hDC = Pintar.GetHdc();
                                    int retval = BitBlt(hDC, 0, 0, Ancho, Alto, hSrcDC, Rectángulo.X, Rectángulo.Y, !Variable_Negativo ? (int)CopyPixelOperation.SourceCopy : (int)CopyPixelOperation.NotSourceCopy);
                                    Pintar.ReleaseHdc();
                                    gsrc.ReleaseHdc();
                                }
                                if (Menú_Contextual_Dibujar_Cursor.Checked)
                                {
                                    PInvoke.User32.CursorInfo Info_Cursor = new PInvoke.User32.CursorInfo();
                                    Info_Cursor.cbSize = Marshal.SizeOf(Info_Cursor);
                                    if (PInvoke.User32.GetCursorInfo(ref Info_Cursor))
                                    {
                                        PInvoke.User32.IconInfo Info_Icono = new PInvoke.User32.IconInfo();
                                        if (PInvoke.User32.GetIconInfo(Info_Cursor.Handle_Cursor, ref Info_Icono))
                                        {
                                            Cursor Cursor_Actual = new Cursor(Info_Cursor.Handle_Cursor);
                                            if (Cursor_Actual != null)
                                            {
                                                //Size Dimensiones = Bitmap.FromHbitmap(Info_Icono.Handle_Imagen).Size;
                                                Size Dimensiones = Cursor_Actual.Size;
                                                Bitmap Imagen_Cursor = new Bitmap(Dimensiones.Width, Dimensiones.Height, PixelFormat.Format32bppArgb);
                                                Graphics Pintar_Cursor = Graphics.FromImage(Imagen_Cursor);
                                                Cursor_Actual.DrawStretched(Pintar_Cursor, new Rectangle(0, 0, Imagen_Cursor.Width, Imagen_Cursor.Height));
                                                Pintar_Cursor.Dispose();
                                                //Pintar.DrawImage(Imagen_Cursor, new Rectangle((MousePosition.X - Cursor_Actual.HotSpot.X) + (Imagen.Width / 2), (MousePosition.Y - Cursor_Actual.HotSpot.Y) + (Imagen.Height / 2), Imagen_Cursor.Width, Imagen_Cursor.Height), new Rectangle(0, 0, Imagen_Cursor.Width, Imagen_Cursor.Height), GraphicsUnit.Pixel);
                                                Pintar.DrawImage(Imagen_Cursor, new Rectangle((Imagen.Width / 2) - Cursor_Actual.HotSpot.X, (Imagen.Height / 2) - Cursor_Actual.HotSpot.Y, Imagen_Cursor.Width, Imagen_Cursor.Height), new Rectangle(0, 0, Imagen_Cursor.Width, Imagen_Cursor.Height), GraphicsUnit.Pixel);
                                                Imagen_Cursor.Dispose();
                                                Cursor_Actual.Dispose();
                                            }
                                            PInvoke.Gdi32.DeleteObject(Info_Icono.Handle_Imagen);
                                            PInvoke.Gdi32.DeleteObject(Info_Icono.Handle_Máscara);
                                            //PInvoke.User32.DestroyIcon(Info_Icono.Handle_Imagen);
                                            //PInvoke.User32.DestroyIcon(Info_Icono.Handle_Máscara);
                                        }
                                        PInvoke.User32.DestroyCursor(Info_Cursor.Handle_Cursor);
                                    }

                                    // ...

                                    /*//Tried this method too, but this method results in an error with even fewer loops.
                                    Bitmap newBitmap = new Bitmap(bmp);
                                    // was told to try to make a new bitmap and dispose of the last to ensure that it wasn't locked or being used somewhere. 
                                    bmp.Dispose();
                                    bmp = null;
                                    //error occurs here. 
                                    IntPtr ptr = newBitmap.GetHicon();
                                    ICONINFO tmp = new ICONINFO();
                                    GetIconInfo(ptr, ref tmp);
                                    tmp.xHotspot = xHotSpot;
                                    tmp.yHotspot = yHotSpot;
                                    tmp.fIcon = false;
                                    ptr = CreateIconIndirect(ref tmp);

                                    newBitmap.Dispose();
                                    newBitmap = null;

                                    return new Cursor(ptr);*/

                                    /*[DllImport("user32.dll", CharSet = CharSet.Auto)]
                                     extern static bool DestroyIcon(IntPtr handle);
                                     public static Icon ConvertoToIcon(Bitmap bmp)
                                     {
                                         System.IntPtr icH = bmp.GetHicon();
                                         var toReturn = (Icon)Icon.FromHandle(icH).Clone();
                                         DestroyIcon(icH);
                                         return toReturn;
                                     }*/

                                    /*PInvoke.User32.CursorInfo Info_Cursor = new PInvoke.User32.CursorInfo();
                                    Info_Cursor.cbSize = Marshal.SizeOf(Info_Cursor);
                                    if (PInvoke.User32.GetCursorInfo(ref Info_Cursor))
                                    {
                                        PInvoke.User32.IconInfo Info_Icono = new PInvoke.User32.IconInfo();
                                        if (PInvoke.User32.GetIconInfo(Info_Cursor.Handle_Cursor, ref Info_Icono))
                                        {
                                            Cursor Cursor_Actual = new Cursor(Info_Cursor.Handle_Cursor);
                                            if (Cursor_Actual != null)
                                            {
                                                //Size Dimensiones = Bitmap.FromHbitmap(Info_Icono.Handle_Imagen).Size;
                                                Size Dimensiones = Cursor_Actual.Size;
                                                Bitmap Imagen_Cursor = new Bitmap(Dimensiones.Width, Dimensiones.Height, PixelFormat.Format32bppArgb);
                                                Graphics Pintar_Cursor = Graphics.FromImage(Imagen_Cursor);
                                                Cursor_Actual.DrawStretched(Pintar_Cursor, new Rectangle(0, 0, Imagen_Cursor.Width, Imagen_Cursor.Height));
                                                Pintar_Cursor.Dispose();
                                                //Pintar.DrawImage(Imagen_Cursor, new Rectangle((MousePosition.X - Cursor_Actual.HotSpot.X) + (Imagen.Width / 2), (MousePosition.Y - Cursor_Actual.HotSpot.Y) + (Imagen.Height / 2), Imagen_Cursor.Width, Imagen_Cursor.Height), new Rectangle(0, 0, Imagen_Cursor.Width, Imagen_Cursor.Height), GraphicsUnit.Pixel);
                                                Pintar.DrawImage(Imagen_Cursor, new Rectangle((Imagen.Width / 2) - Cursor_Actual.HotSpot.X, (Imagen.Height / 2) - Cursor_Actual.HotSpot.Y, Imagen_Cursor.Width, Imagen_Cursor.Height), new Rectangle(0, 0, Imagen_Cursor.Width, Imagen_Cursor.Height), GraphicsUnit.Pixel);
                                                Imagen_Cursor.Dispose();
                                                Cursor_Actual.Dispose();
                                            }
                                        }
                                    }*/
                                }
                                Pintar.Dispose();
                                Pintar = null;

                                int Ancho_Zoom = Ancho;
                                int Alto_Zoom = Alto;
                                if (Variable_Zoom != Zooms.Zoom_1x)
                                {
                                    int Zoom = int.Parse(Variable_Zoom.ToString().Replace("Zoom_", null).Replace("x", null));
                                    Ancho /= Zoom;
                                    Alto /= Zoom;
                                    int X_Zoom = (Ancho_Zoom / 2) - (Ancho / 2);
                                    int Y_Zoom = (Alto_Zoom / 2) - (Alto / 2);
                                    /*Bitmap Imagen_Zoom = new Bitmap(Ancho, Alto, PixelFormat.Format24bppRgb);
                                    Pintar = Graphics.FromImage(Imagen_Zoom);
                                    //Pintar.Clear(Color.Gray);
                                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                    Pintar.InterpolationMode = !Variable_Zoom_Suave ? InterpolationMode.NearestNeighbor : InterpolationMode.HighQualityBicubic;
                                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                    Pintar.SmoothingMode = SmoothingMode.HighQuality;
                                    Pintar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                                    Pintar.DrawImage(Imagen, new Rectangle(0, 0, Ancho, Alto), new Rectangle(X_Zoom, Y_Zoom, Ancho_Zoom, Alto_Zoom), GraphicsUnit.Pixel);
                                    Pintar.Dispose();
                                    Pintar = null;
                                    Imagen = Imagen_Zoom;*/
                                    Imagen = Imagen.Clone(new Rectangle(X_Zoom, Y_Zoom, Ancho, Alto), Imagen.PixelFormat);
                                }
                                if (Variable_Desaturado_Anterior)
                                {
                                    BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                                    byte[] Matriz_Bytes_ARGB = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes_ARGB, 0, Matriz_Bytes_ARGB.Length);
                                    int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                                    int Bytes_Ancho = Math.Abs(Bitmap_Data.Stride);
                                    int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                                    for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                                    {
                                        for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                        {
                                            Matriz_Bytes_ARGB[Índice] = (byte)((Matriz_Bytes_ARGB[Índice + 2] + Matriz_Bytes_ARGB[Índice + 1] + Matriz_Bytes_ARGB[Índice]) / 3);
                                            Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                            Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];
                                        }
                                    }
                                    Marshal.Copy(Matriz_Bytes_ARGB, 0, Bitmap_Data.Scan0, Matriz_Bytes_ARGB.Length);
                                    Imagen.UnlockBits(Bitmap_Data);
                                    Bitmap_Data = null;
                                    Matriz_Bytes_ARGB = null;
                                }
                                if (//(Control.ModifierKeys & Keys.Alt) != Keys.Alt &&
                                    //(Control.ModifierKeys & Keys.Control) != Keys.Control &&
                                    (Control.ModifierKeys & Keys.Shift) != Keys.Shift)
                                {
                                    Imagen = Filtrar_Imagen(Imagen, Variable_Filtro);
                                }
                                if (Variable_Negativo_Posterior || Variable_Desaturado_Posterior)
                                {
                                    BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                                    byte[] Matriz_Bytes_ARGB = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes_ARGB, 0, Matriz_Bytes_ARGB.Length);
                                    int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                                    int Bytes_Ancho = Math.Abs(Bitmap_Data.Stride);
                                    int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                                    if (Variable_Negativo_Posterior)
                                    {
                                        for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                                        {
                                            for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                            {
                                                Matriz_Bytes_ARGB[Índice + 2] = (byte)(255 - Matriz_Bytes_ARGB[Índice + 2]);
                                                Matriz_Bytes_ARGB[Índice + 1] = (byte)(255 - Matriz_Bytes_ARGB[Índice + 1]);
                                                Matriz_Bytes_ARGB[Índice] = (byte)(255 - Matriz_Bytes_ARGB[Índice]);
                                            }
                                        }
                                    }
                                    if (Variable_Desaturado_Posterior)
                                    {
                                        for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                                        {
                                            for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                            {
                                                Matriz_Bytes_ARGB[Índice] = (byte)((Matriz_Bytes_ARGB[Índice + 2] + Matriz_Bytes_ARGB[Índice + 1] + Matriz_Bytes_ARGB[Índice]) / 3);
                                                Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                                Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];
                                            }
                                        }
                                    }
                                    Marshal.Copy(Matriz_Bytes_ARGB, 0, Bitmap_Data.Scan0, Matriz_Bytes_ARGB.Length);
                                    Imagen.UnlockBits(Bitmap_Data);
                                    Bitmap_Data = null;
                                    Matriz_Bytes_ARGB = null;
                                }
                                if (Variable_Zoom != Zooms.Zoom_1x) // Zoom after all the filters.
                                {
                                    /*int Zoom = int.Parse(Variable_Zoom.ToString().Replace("Zoom_", null).Replace("x", null));
                                    Ancho_Zoom = Ancho / Zoom;
                                    Alto_Zoom = Alto / Zoom;
                                    X_Zoom = (Ancho / 2) - (Ancho_Zoom / 2);
                                    Y_Zoom = (Alto / 2) - (Alto_Zoom / 2);*/
                                    Bitmap Imagen_Zoom = new Bitmap(Ancho_Zoom, Alto_Zoom, PixelFormat.Format24bppRgb);
                                    Pintar = Graphics.FromImage(Imagen_Zoom);
                                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                    Pintar.InterpolationMode = !Variable_Zoom_Suave ? InterpolationMode.NearestNeighbor : InterpolationMode.HighQualityBicubic;
                                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                    Pintar.SmoothingMode = SmoothingMode.HighQuality;
                                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                                    Pintar.DrawImage(Imagen, new Rectangle(0, 0, Ancho_Zoom, Alto_Zoom), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                    Pintar.Dispose();
                                    Pintar = null;
                                    Imagen = Imagen_Zoom;
                                }

                                Picture.Image = Imagen;
                                Picture.Invalidate();
                                Picture.Update();
                                if (Variable_Pantalla_Completa)
                                {
                                    //Picture.Visible = true;
                                    //Picture.Image = null;
                                    //Picture.Refresh();
                                }
                            }
                            else // 2019_10_09_01_23_08_311 Test.
                            {
                                Ancho /= 2;
                                Alto /= 4;
                                Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format24bppRgb);
                                Graphics Pintar = Graphics.FromImage(Imagen);
                                //Pintar.CopyFromScreen(Rectángulo.X, Rectángulo.Y, 0, 0, Imagen.Size, Filtro != Filtros.Negativo ? CopyPixelOperation.SourceCopy : CopyPixelOperation.NotSourceCopy);
                                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero)) // Desktop window?
                                {
                                    IntPtr hSrcDC = gsrc.GetHdc();
                                    IntPtr hDC = Pintar.GetHdc();
                                    int retval = BitBlt(hDC, 0, 0, Ancho, Alto, hSrcDC, Rectángulo.X + (Ancho / 2), Rectángulo.Y + (Alto * 3), !Variable_Negativo ? (int)CopyPixelOperation.SourceCopy : (int)CopyPixelOperation.NotSourceCopy);
                                    Pintar.ReleaseHdc();
                                    gsrc.ReleaseHdc();
                                }
                                Pintar.Dispose();
                                Pintar = null;

                                List<Point> Lista_Índices_R = new List<Point>();
                                List<Point> Lista_Índices_G = new List<Point>();
                                List<Point> Lista_Índices_B = new List<Point>();

                                BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                                byte[] Matriz_Bytes_ARGB = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                                Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes_ARGB, 0, Matriz_Bytes_ARGB.Length);
                                byte[] Matriz_Bytes_ARGB_Original = (byte[])Matriz_Bytes_ARGB.Clone();
                                int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                                int Bytes_Ancho = Math.Abs(Bitmap_Data.Stride);
                                int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                                int Ruido_JPEG = 0; // Used to avoid JPEG noise. 0 = Disabled. 2 = Default.
                                for (int Índice_Y = 0, Índice = 0, Subíndice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                                {
                                    for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                    {
                                        byte Rojo = Matriz_Bytes_ARGB_Original[Índice + 2];
                                        byte Verde = Matriz_Bytes_ARGB_Original[Índice + 1];
                                        byte Azul = Matriz_Bytes_ARGB_Original[Índice];
                                        int Valor_R = 255, Valor_G = 255, Valor_B = 255;
                                        int Valor_R_Temporal = 0, Valor_G_Temporal = 0, Valor_B_Temporal = 0;
                                        for (int Subíndice_Y = -1, X = 0; Subíndice_Y <= 1; Subíndice_Y++, X++)
                                        {
                                            for (int Subíndice_X = -1, Y = 0; Subíndice_X <= 1; Subíndice_X++, Y++)
                                            {
                                                if ((Subíndice_X != 0 || Subíndice_Y != 0) && Índice_X + Subíndice_X > -1 && Índice_X + Subíndice_X < Ancho && Índice_Y + Subíndice_Y > -1 && Índice_Y + Subíndice_Y < Alto)
                                                {
                                                    Subíndice = (Bytes_Ancho * (Índice_Y + Subíndice_Y)) + ((Índice_X + Subíndice_X) * Bytes_Aumento);
                                                    Valor_R_Temporal = Math.Abs(Rojo - Matriz_Bytes_ARGB_Original[Subíndice + 2]);
                                                    Valor_G_Temporal = Math.Abs(Verde - Matriz_Bytes_ARGB_Original[Subíndice + 1]);
                                                    Valor_B_Temporal = Math.Abs(Azul - Matriz_Bytes_ARGB_Original[Subíndice]);
                                                    if (Valor_R_Temporal < Valor_R && Valor_R_Temporal > Ruido_JPEG && Matriz_Bytes_ARGB_Original[Subíndice + 2] < Rojo) Valor_R = Valor_R_Temporal;
                                                    if (Valor_G_Temporal < Valor_G && Valor_G_Temporal > Ruido_JPEG && Matriz_Bytes_ARGB_Original[Subíndice + 1] < Verde) Valor_G = Valor_G_Temporal;
                                                    if (Valor_B_Temporal < Valor_B && Valor_B_Temporal > Ruido_JPEG && Matriz_Bytes_ARGB_Original[Subíndice] < Azul) Valor_B = Valor_B_Temporal;
                                                }
                                            }
                                        }
                                        Matriz_Bytes_ARGB[Índice + 2] = (byte)(255 - Valor_R);
                                        Matriz_Bytes_ARGB[Índice + 1] = (byte)(255 - Valor_G);
                                        Matriz_Bytes_ARGB[Índice] = (byte)(255 - Valor_B);
                                        if (Matriz_Bytes_ARGB[Índice + 2] >= 240 &&
                                            Matriz_Bytes_ARGB[Índice + 1] < 16 &&
                                            Matriz_Bytes_ARGB[Índice] < 16) Lista_Índices_R.Add(new Point(Índice_X, Índice_Y));
                                        //if (Valor_R == 255 && Valor_G == 0 && Valor_B == 255) Lista_Índices_G.Add(new Point(Índice_X, Índice_Y));
                                        //if (Valor_R == 255 && Valor_G == 255 && Valor_B == 0) Lista_Índices_B.Add(new Point(Índice_X, Índice_Y));
                                    }
                                }
                                Matriz_Bytes_ARGB_Original = null;
                                Marshal.Copy(Matriz_Bytes_ARGB, 0, Bitmap_Data.Scan0, Matriz_Bytes_ARGB.Length);
                                Imagen.UnlockBits(Bitmap_Data);
                                Bitmap_Data = null;

                                //Picture.Image = Imagen;

                                // Now analyze this portion of the image to find angles in it.
                                // First add all the colors that are either pure red, green or blue as indexes in a list.
                                // Then randomly pick some of the indexes, and try to "follow" where goes each color.
                                // Finally get the 2 ends as sin and cos and get their angles.
                                // Do it at once for all the angles and use that average value.
                                // Then draw the rotation found at intervals of 90 degrees.
                                // And it should predict where a player is facing in a world.
                                //Math.Acos()
                                int Iteraciones = 1; // 5.
                                Point Posición = Point.Empty;
                                Point Posición_Final = Point.Empty;
                                List<double> Lista_Ángulos_Seno = new List<double>();
                                List<double> Lista_Ángulos_Coseno = new List<double>();
                                if (Lista_Índices_R.Count >= Iteraciones || Lista_Índices_G.Count >= Iteraciones || Lista_Índices_B.Count > Iteraciones)
                                {
                                    List<Point>[] Matriz_Lista_Posiciones = new List<Point>[] { Lista_Índices_R, Lista_Índices_G, Lista_Índices_B };
                                    for (int Índice_Lista = 0; Índice_Lista < 1/*Matriz_Lista_Posiciones.Length*/; Índice_Lista++)
                                    {
                                        if (Matriz_Lista_Posiciones[Índice_Lista].Count >= Iteraciones)
                                        {
                                            for (int Índice_Iteración = 0; Índice_Iteración < Iteraciones; Índice_Iteración++)
                                            {
                                                int Índice_Aleatorio = Program.Rand.Next(0, Matriz_Lista_Posiciones[Índice_Lista].Count);
                                                Posición = Matriz_Lista_Posiciones[Índice_Lista][Índice_Aleatorio];
                                                int Distancia = 10;
                                                //List<Point> Lista_Índices = new List<Point>();
                                                //Lista_Temporal.Add(Posición); // Start at the center.
                                                for (int Índice_Distancia = Distancia; Índice_Distancia > 1; Índice_Distancia--)
                                                {
                                                    List<Point> Lista_Temporal = new List<Point>();

                                                    // Top.
                                                    for (int Índice_X = Posición.X - Índice_Distancia, Índice_Y = Posición.Y - Índice_Distancia; Índice_X < Posición.X + Índice_Distancia; Índice_X++)
                                                    {
                                                        if (Índice_X > -1 && Índice_X < Ancho && // It's valid.
                                                            Índice_Y > -1 && Índice_Y < Alto &&
                                                            Índice_X != Posición.X && // Avoids itself.
                                                            Índice_Y != Posición.Y)
                                                        {
                                                            int Índice = (Bytes_Ancho * Índice_Y) + (Índice_X * Bytes_Aumento);
                                                            if (Matriz_Bytes_ARGB[Índice + 2] >= 240 &&
                                                                Matriz_Bytes_ARGB[Índice + 1] < 16 &&
                                                                Matriz_Bytes_ARGB[Índice] < 16) // Red.
                                                            {
                                                                Lista_Temporal.Add(new Point(Índice_X, Índice_Y));
                                                            }
                                                        }
                                                    }

                                                    // Right.
                                                    for (int Índice_X = Posición.X + Índice_Distancia, Índice_Y = Posición.Y - Índice_Distancia; Índice_Y < Posición.Y + Índice_Distancia; Índice_Y++)
                                                    {
                                                        if (Índice_X > -1 && Índice_X < Ancho && // It's valid.
                                                            Índice_Y > -1 && Índice_Y < Alto &&
                                                            Índice_X != Posición.X && // Avoids itself.
                                                            Índice_Y != Posición.Y)
                                                        {
                                                            int Índice = (Bytes_Ancho * Índice_Y) + (Índice_X * Bytes_Aumento);
                                                            if (Matriz_Bytes_ARGB[Índice + 2] >= 240 &&
                                                                Matriz_Bytes_ARGB[Índice + 1] < 16 &&
                                                                Matriz_Bytes_ARGB[Índice] < 16) // Red.
                                                            {
                                                                Lista_Temporal.Add(new Point(Índice_X, Índice_Y));
                                                            }
                                                        }
                                                    }

                                                    // Bottom.
                                                    for (int Índice_X = Posición.X - (Índice_Distancia - 1), Índice_Y = Posición.Y + Índice_Distancia; Índice_X <= Posición.X + Índice_Distancia; Índice_X++)
                                                    {
                                                        if (Índice_X > -1 && Índice_X < Ancho && // It's valid.
                                                            Índice_Y > -1 && Índice_Y < Alto &&
                                                            Índice_X != Posición.X && // Avoids itself.
                                                            Índice_Y != Posición.Y)
                                                        {
                                                            int Índice = (Bytes_Ancho * Índice_Y) + (Índice_X * Bytes_Aumento);
                                                            if (Matriz_Bytes_ARGB[Índice + 2] >= 240 &&
                                                                Matriz_Bytes_ARGB[Índice + 1] < 16 &&
                                                                Matriz_Bytes_ARGB[Índice] < 16) // Red.
                                                            {
                                                                Lista_Temporal.Add(new Point(Índice_X, Índice_Y));
                                                            }
                                                        }
                                                    }

                                                    // Left.
                                                    for (int Índice_X = Posición.X - Índice_Distancia, Índice_Y = Posición.Y - (Índice_Distancia - 1); Índice_Y <= Posición.Y + Índice_Distancia; Índice_Y++)
                                                    {
                                                        if (Índice_X > -1 && Índice_X < Ancho && // It's valid.
                                                            Índice_Y > -1 && Índice_Y < Alto &&
                                                            Índice_X != Posición.X && // Avoids itself.
                                                            Índice_Y != Posición.Y)
                                                        {
                                                            int Índice = (Bytes_Ancho * Índice_Y) + (Índice_X * Bytes_Aumento);
                                                            if (Matriz_Bytes_ARGB[Índice + 2] >= 240 &&
                                                                Matriz_Bytes_ARGB[Índice + 1] < 16 &&
                                                                Matriz_Bytes_ARGB[Índice] < 16) // Red.
                                                            {
                                                                Lista_Temporal.Add(new Point(Índice_X, Índice_Y));
                                                            }
                                                        }
                                                    }

                                                    if (Lista_Temporal.Count > 0)
                                                    {
                                                        Posición_Final = Lista_Temporal[0];
                                                        double Ángulo_Seno = Math.Asin(((double)Posición_Final.X - (double)Posición.X) / (double)Distancia);
                                                        double Ángulo_Coseno = Math.Acos(((double)Posición_Final.X - (double)Posición.X) / (double)Distancia);
                                                        Ángulo_Seno = (Ángulo_Seno * 180d) / Math.PI;
                                                        Ángulo_Coseno = (Ángulo_Coseno * 180d) / Math.PI;
                                                        Lista_Ángulos_Seno.Add(Ángulo_Seno);
                                                        Lista_Ángulos_Coseno.Add(Ángulo_Coseno);
                                                        break;
                                                    }
                                                    else this.Text = "No final";
                                                }
                                            }
                                        }
                                        else this.Text = "No red";
                                    }
                                }
                                else this.Text = "No points";
                                if (Lista_Ángulos_Seno.Count > 0 &&
                                    Lista_Ángulos_Coseno.Count > 0)
                                {
                                    double Ángulo_Seno = 0d;
                                    double Ángulo_Coseno = 0d;
                                    foreach (double Ángulo in Lista_Ángulos_Seno)
                                    {
                                        Ángulo_Seno += Ángulo;
                                    }
                                    foreach (double Ángulo in Lista_Ángulos_Coseno)
                                    {
                                        Ángulo_Coseno += Ángulo;
                                    }
                                    Ángulo_Seno /= (double)Lista_Ángulos_Seno.Count;
                                    Ángulo_Coseno /= (double)Lista_Ángulos_Coseno.Count;
                                    //Ángulo_Seno = (Ángulo_Seno + Ángulo_Coseno) / 2d;
                                    //Ángulo_Coseno = Ángulo_Seno;
                                    this.Text = Math.Round(Ángulo_Seno, 2, MidpointRounding.AwayFromZero).ToString() + ", " + Math.Round(Ángulo_Coseno, 2, MidpointRounding.AwayFromZero).ToString();
                                    Ancho *= 2;
                                    Alto *= 4;
                                    Bitmap Imagen_2 = new Bitmap(Ancho, Alto, PixelFormat.Format24bppRgb);
                                    Pintar = Graphics.FromImage(Imagen_2);
                                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                    Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                    Pintar.SmoothingMode = SmoothingMode.HighQuality;
                                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                                    Pintar.Clear(Color.White);
                                    Pintar.DrawImage(Imagen, new Rectangle((Ancho / 2) - (Imagen.Width / 2), (Alto / 2) - (Imagen.Height / 2), Imagen.Width, Imagen.Height), new Rectangle(0, 0, Imagen.Width, Imagen.Height), GraphicsUnit.Pixel);
                                    Pintar.CompositingMode = CompositingMode.SourceOver;
                                    //Pintar.FillPie(Brushes.Red, new Rectangle(0, 0, Ancho, Alto), (float)(Ángulo_Seno - 0.5d), 1f);
                                    //Pintar.FillPie(Brushes.Lime, new Rectangle(0, 0, Ancho, Alto), (float)(Ángulo_Coseno - 0.5d), 1f);
                                    //Pintar.TranslateTransform((float)(Ancho / 2), (float)(Alto / 2));
                                    Pintar.DrawLine(Pens.Black, Posición.X * 1, Posición.Y * 1, Posición_Final.X * 1, Posición_Final.Y * 1);
                                    Pintar.Dispose();
                                    Pintar = null;
                                    Picture.Image = Imagen_2;
                                }
                                Matriz_Bytes_ARGB = null;
                            }
                        }
                    }
                }
                else if ((Control.ModifierKeys & Keys.LShiftKey) != Keys.LShiftKey && (Control.ModifierKeys & Keys.RShiftKey) != Keys.RShiftKey) // Just for Jupisoft (or users of GitHub Desktop)...
                {
                    // This will commit multiple changes "automatically", and it's
                    // designed to check file after file individually and commit
                    // it's changes, but in this case that should always be the
                    // deletion of the files. Screen should have 1.024 x 768 pixels.
                    //Rectangle Rectángulo = new Rectangle(17, 146, 1, 1); // Old VGA.
                    Rectangle Rectángulo = new Rectangle(17, 147, 1, 1); // New VGA.
                    int Ancho = 1;
                    int Alto = 1;

                    Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format24bppRgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    //Pintar.CopyFromScreen(Rectángulo.X, Rectángulo.Y, 0, 0, Imagen.Size, Filtro != Filtros.Negativo ? CopyPixelOperation.SourceCopy : CopyPixelOperation.NotSourceCopy);
                    using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero)) // Desktop window?
                    {
                        IntPtr hSrcDC = gsrc.GetHdc();
                        IntPtr hDC = Pintar.GetHdc();
                        int retval = BitBlt(hDC, 0, 0, Ancho, Alto, hSrcDC, Rectángulo.X, Rectángulo.Y, (int)CopyPixelOperation.SourceCopy);
                        Pintar.ReleaseHdc();
                        gsrc.ReleaseHdc();
                    }
                    Pintar.Dispose();
                    Pintar = null;

                    BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                    byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                    int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                    int Bytes_Ancho = Math.Abs(Bitmap_Data.Stride);
                    int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                    Imagen.UnlockBits(Bitmap_Data);
                    Bitmap_Data = null;
                    Imagen.Dispose();
                    Imagen = null;

                    CheckState Estado = CheckState.Indeterminate;
                    for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                    {
                        for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                        {
                            int Valor = (Matriz_Bytes[Índice + 2] + Matriz_Bytes[Índice + 1] + Matriz_Bytes[Índice]) / 3;
                            if (Valor >= 192 && Valor < 255/*(Matriz_Bytes[Índice + 2] == 236 && Matriz_Bytes[Índice + 1] == 236 && Matriz_Bytes[Índice] == 236) ||
                                (Matriz_Bytes[Índice + 2] == 231 && Matriz_Bytes[Índice + 1] == 231 && Matriz_Bytes[Índice] == 231)*/)
                            {
                                Estado = CheckState.Unchecked;
                            }
                            else if (Valor <= 128) //((Matriz_Bytes[Índice + 2] == 102 && Matriz_Bytes[Índice + 1] == 102 && Matriz_Bytes[Índice] == 102) ||
                                //(Matriz_Bytes[Índice + 2] == 168 && Matriz_Bytes[Índice + 1] == 170 && Matriz_Bytes[Índice] == 171))
                            {
                                Estado = CheckState.Checked;
                            }
                            else// if (Valor == 137)
                            {
                                Estado = CheckState.Indeterminate;
                            }
                        }
                    }
                    Matriz_Bytes = null;

                    this.Text = "GitHub Desktop: " + Estado.ToString();
                    //Temporizador_Principal.Stop();
                    //MessageBox.Show(this, "GitHub Desktop: " + Estado.ToString());
                    //Temporizador_Principal.Start();

                    if (Estado != CheckState.Indeterminate)
                    {
                        Cronómetro_GitHub.Reset();
                        if (Estado == CheckState.Unchecked)
                        {
                            PInvoke.User32.SetCursorPos(17, 147);
                            PInvoke.User32.mouse_event(PInvoke.User32.MouseEventF.LeftDown, 0, 0, 0, 0);
                            Thread.Sleep(100);
                            PInvoke.User32.mouse_event(PInvoke.User32.MouseEventF.LeftUp, 0, 0, 0, 0);
                            Thread.Sleep(100);
                            //Temporizador_Principal.Stop();
                            //MessageBox.Show(this, "Checked?");
                            //Temporizador_Principal.Start();
                        }
                        else
                        {
                            //Rectángulo = new Rectangle(55, 660, 1, 1); // Old VGA.
                            Rectángulo = new Rectangle(55, 660, 1, 1); // New VGA.

                            Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format24bppRgb);
                            Pintar = Graphics.FromImage(Imagen);
                            //Pintar.CopyFromScreen(Rectángulo.X, Rectángulo.Y, 0, 0, Imagen.Size, Filtro != Filtros.Negativo ? CopyPixelOperation.SourceCopy : CopyPixelOperation.NotSourceCopy);
                            using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero)) // Desktop window?
                            {
                                IntPtr hSrcDC = gsrc.GetHdc();
                                IntPtr hDC = Pintar.GetHdc();
                                int retval = BitBlt(hDC, 0, 0, Ancho, Alto, hSrcDC, Rectángulo.X, Rectángulo.Y, (int)CopyPixelOperation.SourceCopy);
                                Pintar.ReleaseHdc();
                                gsrc.ReleaseHdc();
                            }
                            Pintar.Dispose();
                            Pintar = null;

                            Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                            Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                            Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                            Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                            Bytes_Ancho = Math.Abs(Bitmap_Data.Stride);
                            Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                            Imagen.UnlockBits(Bitmap_Data);
                            Bitmap_Data = null;
                            Imagen.Dispose();
                            Imagen = null;

                            GitHub_Estados Estado_GitHub = GitHub_Estados.Desconocido;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    if (Matriz_Bytes[Índice + 2] == 100 && Matriz_Bytes[Índice + 1] == 160 && Matriz_Bytes[Índice] == 228)
                                    {
                                        Estado_GitHub = GitHub_Estados.Deshabilitado;
                                    }
                                    else if (Matriz_Bytes[Índice + 2] == 3 && Matriz_Bytes[Índice + 1] == 102 && Matriz_Bytes[Índice] == 214)
                                    {
                                        Estado_GitHub = GitHub_Estados.Habilitado;
                                    }
                                    else if (Matriz_Bytes[Índice + 2] == 251 && Matriz_Bytes[Índice + 1] == 252 && Matriz_Bytes[Índice] == 253)
                                    {
                                        Estado_GitHub = GitHub_Estados.Esperar;
                                    }
                                    else
                                    {
                                        Estado_GitHub = GitHub_Estados.Desconocido;
                                    }
                                }
                            }
                            Matriz_Bytes = null;
                            //Temporizador_Principal.Stop();
                            //MessageBox.Show(this, "Estado_GitHub 2: " + Estado_GitHub.ToString());
                            //Temporizador_Principal.Start();

                            if (Estado_GitHub != GitHub_Estados.Desconocido)
                            {
                                if (Estado_GitHub == GitHub_Estados.Habilitado)
                                {
                                    PInvoke.User32.SetCursorPos(55, 660);
                                    PInvoke.User32.mouse_event(PInvoke.User32.MouseEventF.LeftDown, 0, 0, 0, 0);
                                    Thread.Sleep(100);
                                    PInvoke.User32.mouse_event(PInvoke.User32.MouseEventF.LeftUp, 0, 0, 0, 0);
                                    Thread.Sleep(100);
                                    //Temporizador_Principal.Stop();
                                    //MessageBox.Show(this, "Clicked?");
                                    //Temporizador_Principal.Start();
                                }
                            }
                        }
                    }
                    else if (!Cronómetro_GitHub.IsRunning) // Countdown to stop.
                    {
                        Cronómetro_GitHub.Restart();
                    }
                    /*else if (Cronómetro_GitHub.ElapsedMilliseconds >= 1000L) // Sound warning.
                    {
                        SystemSounds.Beep.Play();
                    }*/
                    else if (Cronómetro_GitHub.ElapsedMilliseconds >= 15000L) // Stop.
                    {
                        Menú_Contextual_GitHub.Checked = false;
                        Cronómetro_GitHub.Reset();
                        SystemSounds.Hand.Play();
                    }
                    else if (Control.MousePosition.X != 621 || Control.MousePosition.Y != 419)
                    {
                        PInvoke.User32.SetCursorPos(621, 419); // Try to avoid the Close button if an error happens.
                    }
                }
                else Menú_Contextual_GitHub.Checked = false;

                long FPS_Milisegundo = FPS_Cronómetro.ElapsedMilliseconds;
                long FPS_Segundo = FPS_Milisegundo / 1000L;
                if (FPS_Segundo != FPS_Segundo_Anterior)
                {
                    FPS_Segundo_Anterior = FPS_Segundo;
                    FPS_Real = FPS_Temporal;
                    Barra_Estado_Etiqueta_FPS.Text = FPS_Real.ToString() + " FPS";
                    FPS_Temporal = 0L;
                }
                FPS_Temporal++;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal enum GitHub_Estados : byte
        {
            Deshabilitado = 0,
            Habilitado,
            Esperar,
            Desconocido
        }

        internal Stopwatch Cronómetro_GitHub = new Stopwatch();
        internal static bool Variable_GitHub = false;

        private void Menú_Contextual_GitHub_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_GitHub = Menú_Contextual_GitHub.Checked;
                //Temporizador_Principal.Interval = !Variable_GitHub ? 1 : 40;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        public static double Clamp(double value, double min, double max)
        {
            if (value < min)
            {
                return min;
            }
            if (value > max)
            {
                return max;
            }
            return value;
        }

        internal Bitmap Filtrar_Imagen(Bitmap Imagen, Filtros Filtro)
        {
            try
            {
                if (Imagen != null)
                {
                    int Ancho = Imagen.Width;
                    int Alto = Imagen.Height;
                    if (Filtro == Filtros.Flip_horizontally)
                    {
                        Imagen.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    }
                    else if (Filtro == Filtros.Flip_vertically)
                    {
                        Imagen.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    }
                    else if (Filtro == Filtros.Rotate_180_degrees)
                    {
                        Imagen.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                    }
                    else if (Filtro == Filtros.Gamma_negative || Filtro == Filtros.Gamma_positive || Filtro == Filtros.Cyan_color_channel || Filtro == Filtros.Magenta_color_channel || Filtro == Filtros.Yellow_color_channel || Filtro == Filtros.Black_color_channel)
                    {
                        Bitmap Imagen_Atributos = new Bitmap(Ancho, Alto, !Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? PixelFormat.Format24bppRgb : PixelFormat.Format32bppArgb);
                        Graphics Pintar_Atributos = Graphics.FromImage(Imagen_Atributos);
                        Pintar_Atributos.CompositingMode = CompositingMode.SourceCopy;
                        Pintar_Atributos.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar_Atributos.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Pintar_Atributos.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar_Atributos.SmoothingMode = SmoothingMode.HighQuality;
                        ImageAttributes Atributos = new ImageAttributes();
                        if (Filtro == Filtros.Gamma_negative) Atributos.SetGamma(2.0f);
                        else if (Filtro == Filtros.Gamma_positive) Atributos.SetGamma(0.5f);
                        else if (Filtro == Filtros.Cyan_color_channel) Atributos.SetOutputChannel(ColorChannelFlag.ColorChannelC);
                        else if (Filtro == Filtros.Magenta_color_channel) Atributos.SetOutputChannel(ColorChannelFlag.ColorChannelM);
                        else if (Filtro == Filtros.Yellow_color_channel) Atributos.SetOutputChannel(ColorChannelFlag.ColorChannelY);
                        else if (Filtro == Filtros.Black_color_channel) Atributos.SetOutputChannel(ColorChannelFlag.ColorChannelK);
                        Pintar_Atributos.DrawImage(Imagen, new Rectangle(0, 0, Ancho, Alto), 0, 0, Ancho, Alto, GraphicsUnit.Pixel, Atributos);
                        Atributos.Dispose();
                        Atributos = null;
                        Pintar_Atributos.Dispose();
                        Pintar_Atributos = null;
                        Imagen = Imagen_Atributos;
                    }
                    else if (Filtro == Filtros.Compression_GIF)
                    {
                        MemoryStream Lector_Memoria = new MemoryStream();
                        Imagen.Save(Lector_Memoria, ImageFormat.Gif); // Default compression.
                        Imagen = Program.Cargar_Imagen_Lector(Lector_Memoria, CheckState.Indeterminate);
                        Lector_Memoria.Close();
                        Lector_Memoria.Dispose();
                        Lector_Memoria = null;
                    }
                    else if (Filtro == Filtros.Compression_JPEG)
                    {
                        MemoryStream Lector_Memoria = new MemoryStream();
                        ImageCodecInfo Codificador = Program.Obtener_Imagen_Codificador_Guid(ImageFormat.Jpeg.Guid);
                        if (Codificador != null) // We can choose any JPEG compression.
                        {
                            int Calidad_JPEG = Program.Rand.Next(0, 100); // 0 = Minimum, 100 = Maximum quality.
                            EncoderParameters Parámetros = new EncoderParameters(1);
                            Parámetros.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)Calidad_JPEG);
                            Imagen.Save(Lector_Memoria, Codificador, Parámetros);
                            Parámetros.Dispose();
                            Parámetros = null;
                            Codificador = null;
                        }
                        else Picture.Image.Save(Lector_Memoria, ImageFormat.Jpeg); // Default compression.
                        Imagen = Program.Cargar_Imagen_Lector(Lector_Memoria, CheckState.Indeterminate);
                        Lector_Memoria.Close();
                        Lector_Memoria.Dispose();
                        Lector_Memoria = null;
                    }
                    else if (Filtro == Filtros.Image_magick_filter)
                    {
                        MagickImage Imagen_Mágica = new MagickImage(Imagen.Clone() as Bitmap);
                        //Imagen_Mágica.Implode(5d, PixelInterpolateMethod.Average); // Esfera.
                        //Imagen_Mágica.Raise(16); // Marco.
                        //Imagen_Mágica.Swirl(PixelInterpolateMethod.Nearest, 9d); // Giro.
                        //Imagen_Mágica.Normalize();
                        //Imagen_Mágica.SigmoidalContrast(32d);
                        //Imagen_Mágica.Solarize(0.5d);
                        //Imagen_Mágica.Transpose();
                        //Imagen_Mágica.Stereo(Imagen_Mágica);
                        //Imagen_Mágica.AutoLevel(Channels.RGB);
                        //Imagen_Mágica.AutoGamma(Channels.RGB);
                        //Imagen_Mágica.AutoOrient();
                        //Imagen_Mágica.AutoThreshold(AutoThresholdMethod.OTSU);
                        Imagen_Mágica.BlueShift();
                        //Imagen_Mágica.CannyEdge();
                        //Imagen_Mágica.Charcoal(); // Needs a pre negative filter?
                        //Imagen_Mágica.Encipher("Abc");
                        //Imagen_Mágica.Equalize();
                        //Imagen_Mágica.Swirl(360d);
                        Imagen = Imagen_Mágica.ToBitmap();
                        Imagen_Mágica.Dispose();
                        Imagen_Mágica = null;
                    }
                    else if (Filtro != Filtros.Original && (Filtro < Filtros.Flip_horizontally || Filtro > Filtros.Rotate_180_degrees))
                    {
                        bool Cancelar_Marshal_Copy = false; // If it's false, copy at the end the modified pixels.
                        BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                        byte[] Matriz_Bytes_ARGB = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                        Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes_ARGB, 0, Matriz_Bytes_ARGB.Length);
                        int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                        int Bytes_Ancho = Math.Abs(Bitmap_Data.Stride);
                        int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                        Imagen.UnlockBits(Bitmap_Data);
                        Bitmap_Data = null;
                        if (Filtro == Filtros.Negative)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)(255 - Matriz_Bytes_ARGB[Índice + 2]);
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)(255 - Matriz_Bytes_ARGB[Índice + 1]);
                                    Matriz_Bytes_ARGB[Índice] = (byte)(255 - Matriz_Bytes_ARGB[Índice]);
                                }
                            }
                        }
                        else if (Filtro == Filtros.Experimental)
                        {
                            int Matiz;
                            Color Color_ARGB;
                            //Índice_Termografía = Control.MousePosition.X;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    if (Índice_Y < Alto / 2) // Test 1.
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = (byte)(Math.Max(Matriz_Bytes_ARGB[Índice + 2], 255 - Matriz_Bytes_ARGB[Índice + 2]) - Math.Min(Matriz_Bytes_ARGB[Índice + 2], 255 - Matriz_Bytes_ARGB[Índice + 2]));
                                        Matriz_Bytes_ARGB[Índice + 1] = (byte)(Math.Max(Matriz_Bytes_ARGB[Índice + 1], 255 - Matriz_Bytes_ARGB[Índice + 1]) - Math.Min(Matriz_Bytes_ARGB[Índice + 1], 255 - Matriz_Bytes_ARGB[Índice + 1]));
                                        Matriz_Bytes_ARGB[Índice] = (byte)(Math.Max(Matriz_Bytes_ARGB[Índice], 255 - Matriz_Bytes_ARGB[Índice]) - Math.Min(Matriz_Bytes_ARGB[Índice], 255 - Matriz_Bytes_ARGB[Índice]));

                                        //Matriz_Bytes_ARGB[Índice] = (byte)((Matriz_Bytes_ARGB[Índice + 2] + Matriz_Bytes_ARGB[Índice + 1] + Matriz_Bytes_ARGB[Índice]) / 3);
                                        //Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                        //Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];

                                        //Matriz_Bytes_ARGB[Índice + 2] = (byte)(255 - (((Matriz_Bytes_ARGB[Índice + 2] * (255 - Matriz_Bytes_ARGB[Índice + 2])) * 255) / 16256));
                                        //Matriz_Bytes_ARGB[Índice + 1] = (byte)(255 - (((Matriz_Bytes_ARGB[Índice + 1] * (255 - Matriz_Bytes_ARGB[Índice + 1])) * 255) / 16256));
                                        //Matriz_Bytes_ARGB[Índice] = (byte)(255 - (((Matriz_Bytes_ARGB[Índice] * (255 - Matriz_Bytes_ARGB[Índice])) * 255) / 16256));
                                    }
                                    /*Matiz = (Matriz_Bytes_ARGB[Índice + 2] + Matriz_Bytes_ARGB[Índice + 1] + Matriz_Bytes_ARGB[Índice]) + Índice_Termografía;
                                    Matiz *= 2;
                                    if (Matiz >= 1530) Matiz -= 1530;
                                    Color_ARGB = Program.Obtener_Color_Puro_1530(1529 - Matiz);
                                    Matriz_Bytes_ARGB[Índice + 2] = Color_ARGB.R;
                                    Matriz_Bytes_ARGB[Índice + 1] = Color_ARGB.G;
                                    Matriz_Bytes_ARGB[Índice] = Color_ARGB.B;*/
                                    int Gris = (Matriz_Bytes_ARGB[Índice + 2] + Matriz_Bytes_ARGB[Índice + 1] + Matriz_Bytes_ARGB[Índice]) / 1;
                                    //Gris *= 2;
                                    Gris += Índice_Termografía;
                                    //while (Gris > 255) Gris -= 256;
                                    while (Gris >= 1530) Gris -= 1530;
                                    Matriz_Bytes_ARGB[Índice + 2] = Program.Obtener_Color_Puro_1530(Gris).R;
                                    Matriz_Bytes_ARGB[Índice + 1] = Program.Obtener_Color_Puro_1530(Gris).G;
                                    Matriz_Bytes_ARGB[Índice] = Program.Obtener_Color_Puro_1530(Gris).B;
                                    //Matriz_Bytes_ARGB[Índice + 2] = Program.Matriz_Colores_Arco_Iris_256[Gris].R;
                                    //Matriz_Bytes_ARGB[Índice + 1] = Program.Matriz_Colores_Arco_Iris_256[Gris].G;
                                    //Matriz_Bytes_ARGB[Índice] = Program.Matriz_Colores_Arco_Iris_256[Gris].B;
                                }
                            }
                            Índice_Termografía += 3;
                            if (Índice_Termografía >= /*765*/1530) Índice_Termografía = 0;

                            // Solarize V.
                            /*for (int Rojo = 2, Verde = 1, Azul = 0; Rojo < Matriz_Bytes.Length; Rojo += 4, Verde += 4, Azul += 4)
                            {
                                Matriz_Bytes[Rojo] = (Byte)(Math.Max(Matriz_Bytes[Rojo], 255 - Matriz_Bytes[Rojo]) - Math.Min(Matriz_Bytes[Rojo], 255 - Matriz_Bytes[Rojo]));
                                Matriz_Bytes[Verde] = (Byte)(Math.Max(Matriz_Bytes[Verde], 255 - Matriz_Bytes[Verde]) - Math.Min(Matriz_Bytes[Verde], 255 - Matriz_Bytes[Verde]));
                                Matriz_Bytes[Azul] = (Byte)(Math.Max(Matriz_Bytes[Azul], 255 - Matriz_Bytes[Azul]) - Math.Min(Matriz_Bytes[Azul], 255 - Matriz_Bytes[Azul]));
                            }*/
                            // Solarize U.
                            /*for (int Rojo = 2, Verde = 1, Azul = 0; Rojo < Matriz_Bytes.Length; Rojo += 4, Verde += 4, Azul += 4)
                            {
                                Matriz_Bytes[Rojo] = (Byte)(255 - (((Matriz_Bytes[Rojo] * (255 - Matriz_Bytes[Rojo])) * 255) / 16256));
                                Matriz_Bytes[Verde] = (Byte)(255 - (((Matriz_Bytes[Verde] * (255 - Matriz_Bytes[Verde])) * 255) / 16256));
                                Matriz_Bytes[Azul] = (Byte)(255 - (((Matriz_Bytes[Azul] * (255 - Matriz_Bytes[Azul])) * 255) / 16256));
                            }*/
                            // Solarize Y.
                            /*?*/
                            /*byte Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            int Valor;
                            Color Color_ARGB;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    if (Matriz_Bytes_ARGB[Índice] != Matriz_Bytes_ARGB[Índice + 1] || Matriz_Bytes_ARGB[Índice] != Matriz_Bytes_ARGB[Índice + 2]) // Not gray scale.
                                    {
                                        if (Índice_Y < Alto / 2) // Test 1.
                                        {
                                            Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                            //Saturación = 100d;
                                            Saturación /= 2d;
                                            if (Saturación < 0d) Saturación = 0d;
                                            else if (Saturación > 100d) Saturación = 100d;
                                            Program.HSL.To_RGB(Matiz, Saturación, Luminosidad, out Rojo, out Verde, out Azul);

                                            //Rojo = (byte)(Math.Max(Rojo, 255 - Rojo) - Math.Min(Rojo, 255 - Rojo));
                                            //Verde = (byte)(Math.Max(Verde, 255 - Verde) - Math.Min(Verde, 255 - Verde));
                                            //Azul = (byte)(Math.Max(Azul, 255 - Azul) - Math.Min(Azul, 255 - Azul));
                                            
                                            //Rojo = (byte)(255 - (((Rojo * (255 - Rojo)) * 255) / 16256));
                                            //Verde = (byte)(255 - (((Verde * (255 - Verde)) * 255) / 16256));
                                            //Azul = (byte)(255 - (((Azul * (255 - Azul)) * 255) / 16256));
                                            
                                            Matriz_Bytes_ARGB[Índice + 2] = Rojo;
                                            Matriz_Bytes_ARGB[Índice + 1] = Verde;
                                            Matriz_Bytes_ARGB[Índice] = Azul;
                                        }
                                        else // Test 2.
                                        {
                                            //Matriz_Bytes_ARGB[Índice + 2] = (byte)(Math.Max(Matriz_Bytes_ARGB[Índice + 2], 255 - Matriz_Bytes_ARGB[Índice + 2]) - Math.Min(Matriz_Bytes_ARGB[Índice + 2], 255 - Matriz_Bytes_ARGB[Índice + 2]));
                                            //Matriz_Bytes_ARGB[Índice + 1] = (byte)(Math.Max(Matriz_Bytes_ARGB[Índice + 1], 255 - Matriz_Bytes_ARGB[Índice + 1]) - Math.Min(Matriz_Bytes_ARGB[Índice + 1], 255 - Matriz_Bytes_ARGB[Índice + 1]));
                                            //Matriz_Bytes_ARGB[Índice] = (byte)(Math.Max(Matriz_Bytes_ARGB[Índice], 255 - Matriz_Bytes_ARGB[Índice]) - Math.Min(Matriz_Bytes_ARGB[Índice], 255 - Matriz_Bytes_ARGB[Índice]));

                                            //Matriz_Bytes_ARGB[Índice + 2] = (byte)(255 - (((Matriz_Bytes_ARGB[Índice + 2] * (255 - Matriz_Bytes_ARGB[Índice + 2])) * 255) / 16256));
                                            //Matriz_Bytes_ARGB[Índice + 1] = (byte)(255 - (((Matriz_Bytes_ARGB[Índice + 1] * (255 - Matriz_Bytes_ARGB[Índice + 1])) * 255) / 16256));
                                            //Matriz_Bytes_ARGB[Índice] = (byte)(255 - (((Matriz_Bytes_ARGB[Índice] * (255 - Matriz_Bytes_ARGB[Índice])) * 255) / 16256));
                                            
                                            Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                            Saturación = 50;
                                            //Saturación *= 2d;
                                            if (Saturación < 0d) Saturación = 0d;
                                            else if (Saturación > 100d) Saturación = 100d;
                                            Program.HSL.To_RGB(Matiz, Saturación, Luminosidad, out Rojo, out Verde, out Azul);

                                            Matriz_Bytes_ARGB[Índice + 2] = Rojo;
                                            Matriz_Bytes_ARGB[Índice + 1] = Verde;
                                            Matriz_Bytes_ARGB[Índice] = Azul;
                                        }
                                    }
                                }
                            }*/
                        }
                        else if (Filtro == Filtros.Auto_level)
                        {
                            long[] Matriz_Histograma_R = new long[256];
                            long[] Matriz_Histograma_G = new long[256];
                            long[] Matriz_Histograma_B = new long[256];
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Histograma_R[Matriz_Bytes_ARGB[Índice + 2]]++;
                                    Matriz_Histograma_G[Matriz_Bytes_ARGB[Índice + 1]]++;
                                    Matriz_Histograma_B[Matriz_Bytes_ARGB[Índice]]++;
                                    //Total_R += Matriz_Bytes_ARGB[Índice + 2];
                                    //Total_G += Matriz_Bytes_ARGB[Índice + 1];
                                    //Total_B += Matriz_Bytes_ARGB[Índice];
                                    //Total++;
                                }
                            }
                            //double Percentil_0_005 = 5; // 0.005d; // ‰.
                            //double Percentil_0_995 = 995; // 0.995d; // ‰.
                            long Total_R = 0L, Total_G = 0L, Total_B = 0L;
                            for (int Índice = 0; Índice < 256; Índice++)
                            {
                                Total_R += Índice * Matriz_Histograma_R[Índice];
                                Total_G += Índice * Matriz_Histograma_G[Índice];
                                Total_B += Índice * Matriz_Histograma_B[Índice];
                            }
                            int Píxeles = Ancho * Alto;
                            byte Media_R = (byte)(Total_R / Píxeles);
                            byte Media_G = (byte)(Total_G / Píxeles);
                            byte Media_B = (byte)(Total_B / Píxeles);

                            byte Percentil_R_0_005 = 0, Percentil_G_0_005 = 0, Percentil_B_0_005 = 0;
                            byte Percentil_R_0_995 = 0, Percentil_G_0_995 = 0, Percentil_B_0_995 = 0;

                            for (long Índice = 0L, Total = 0L, Percentil = (Total_R * 5L) / 1000L; Índice < 256L; Índice++)
                            {
                                Total += Índice * Matriz_Histograma_R[Índice];
                                if (Total >= Percentil)
                                {
                                    Percentil_R_0_005 = (byte)Índice;
                                    break;
                                }
                            }
                            for (long Índice = 0L, Total = 0L, Percentil = (Total_G * 5L) / 1000L; Índice < 256L; Índice++)
                            {
                                Total += Índice * Matriz_Histograma_G[Índice];
                                if (Total >= Percentil)
                                {
                                    Percentil_G_0_005 = (byte)Índice;
                                    break;
                                }
                            }
                            for (long Índice = 0L, Total = 0L, Percentil = (Total_B * 5L) / 1000L; Índice < 256L; Índice++)
                            {
                                Total += Índice * Matriz_Histograma_B[Índice];
                                if (Total >= Percentil)
                                {
                                    Percentil_B_0_005 = (byte)Índice;
                                    break;
                                }
                            }
                            for (long Índice = 0L, Total = 0L, Percentil = (Total_R * 995L) / 1000L; Índice < 256L; Índice++)
                            {
                                Total += Índice * Matriz_Histograma_R[Índice];
                                if (Total >= Percentil)
                                {
                                    Percentil_R_0_995 = (byte)Índice;
                                    break;
                                }
                            }
                            for (long Índice = 0L, Total = 0L, Percentil = (Total_G * 995L) / 1000L; Índice < 256L; Índice++)
                            {
                                Total += Índice * Matriz_Histograma_G[Índice];
                                if (Total >= Percentil)
                                {
                                    Percentil_G_0_995 = (byte)Índice;
                                    break;
                                }
                            }
                            for (long Índice = 0L, Total = 0L, Percentil = (Total_B * 995L) / 1000L; Índice < 256L; Índice++)
                            {
                                Total += Índice * Matriz_Histograma_B[Índice];
                                if (Total >= Percentil)
                                {
                                    Percentil_B_0_995 = (byte)Índice;
                                    break;
                                }
                            }
                            //Color Color_Mínimo = Color.FromArgb(255, Percentil_R_0_005, Percentil_G_0_005, Percentil_B_0_005);
                            //Color Color_Medio = Color.FromArgb(255, Media_R, Media_G, Media_B);
                            //Color Color_Máximo = Color.FromArgb(255, Percentil_R_0_995, Percentil_G_0_995, Percentil_B_0_995);
                            byte[] Matriz_RGB_Mínimo = new byte[3] { Percentil_R_0_005, Percentil_G_0_005, Percentil_B_0_005 };
                            byte[] Matriz_RGB_Medio = new byte[3] { Media_R, Media_G, Media_B };
                            byte[] Matriz_RGB_Máximo = new byte[3] { Percentil_R_0_005, Percentil_G_0_005, Percentil_B_0_005 };
                            double[] array = new double[3];
                            for (int Índice_RGB = 0; Índice_RGB < 3; Índice_RGB++)
                            {
                                if (Matriz_RGB_Mínimo[Índice_RGB] < Matriz_RGB_Medio[Índice_RGB] && Matriz_RGB_Medio[Índice_RGB] < Matriz_RGB_Máximo[Índice_RGB])
                                {
                                    array[Índice_RGB] = Clamp(Math.Log(0.5, (double)(Matriz_RGB_Medio[Índice_RGB] - Matriz_RGB_Mínimo[Índice_RGB]) / (double)(Matriz_RGB_Máximo[Índice_RGB] - Matriz_RGB_Mínimo[Índice_RGB])), 0.1, 10.0);
                                }
                                else
                                {
                                    array[Índice_RGB] = 1d;
                                }
                            }
                            //return new LevelOp(Matriz_RGB_Mínimo, Matriz_RGB_Máximo, array, ColorBgra.FromColor(Color.Black), ColorBgra.FromColor(Color.White));
                            //UpdateLookupTable();
                            /*int num = 0;
                            while (true)
                            {
                                if (num < 3)
                                {
                                    if (colorOutHigh[num] < colorOutLow[num] || colorInHigh[num] <= colorInLow[num] || gamma[num] < 0f)
                                    {
                                        break;
                                    }
                                    for (int i = 0; i < 256; i++)
                                    {
                                        ColorBgra result = default(ColorBgra);
                                        float[] array = new float[3]
                                        {
                                            b,
                                            g,
                                            r
                                        };
                                        for (int i = 0; i < 3; i++)
                                        {
                                            float num = array[i] - (float)(int)colorInLow[i];
                                            if (num < 0f)
                                            {
                                                result[i] = colorOutLow[i];
                                            }
                                            else if (num + (float)(int)colorInLow[i] >= (float)(int)colorInHigh[i])
                                            {
                                                result[i] = colorOutHigh[i];
                                            }
                                            else
                                            {
                                                result[i] = (byte)((double)(int)colorOutLow[i] + (double)(colorOutHigh[i] - colorOutLow[i]) * Math.Pow(num / (float)(colorInHigh[i] - colorInLow[i]), gamma[i])).Clamp(0.0, 255.0);
                                            }
                                        }
                                        return result;
                                        //ColorBgra colorBgra = Apply(i, i, i);
                                        CurveB[i] = colorBgra.B;
                                        CurveG[i] = colorBgra.G;
                                        CurveR[i] = colorBgra.R;
                                    }
                                    num++;
                                    continue;
                                }
                                //return;
                                break;
                            }
                            isValid = false;*/







                        }
                        else if (Filtro == Filtros.Brightness_negative)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Math.Max(Matriz_Bytes_ARGB[Índice + 2] - 128, 0);
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Math.Max(Matriz_Bytes_ARGB[Índice + 1] - 128, 0);
                                    Matriz_Bytes_ARGB[Índice] = (byte)Math.Max(Matriz_Bytes_ARGB[Índice] - 128, 0);
                                }
                            }
                        }
                        else if (Filtro == Filtros.Brightness_positive)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Math.Min(Matriz_Bytes_ARGB[Índice + 2] + 128, 255);
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Math.Min(Matriz_Bytes_ARGB[Índice + 1] + 128, 255);
                                    Matriz_Bytes_ARGB[Índice] = (byte)Math.Min(Matriz_Bytes_ARGB[Índice] + 128, 255);
                                }
                            }
                        }
                        else if (Filtro == Filtros.Contrast_negative)
                        {
                            /*int Contraste = -128;
                            bool Habilitar_Contraste_Menos = false;
                            bool Habilitar_Contraste_Más = false;
                            if (Contraste < 0)
                            {
                                Habilitar_Contraste_Menos = true;
                                Contraste = (int)(Contraste * -1);
                            }
                            else if (Contraste > 0) Habilitar_Contraste_Más = true;
                            int Resto_Contraste = (int)(255 - Contraste);

                            int Rojo, Verde, Azul;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    //Rojo = ((Matriz_Bytes_ARGB[Índice + 2] / 255) * Resto_Contraste) + ((128 / 255) * Contraste);
                                    //Rojo = ((Matriz_Bytes_ARGB[Índice + 2] * 127) / 255) + ((128 * 128) / 255);
                                    //Verde = ((Matriz_Bytes_ARGB[Índice + 1] * 127) / 255) + ((128 * 128) / 255);
                                    //Azul = ((Matriz_Bytes_ARGB[Índice] * 127) / 255) + ((128 * 128) / 255);

                                    Rojo = (Matriz_Bytes_ARGB[Índice + 2] + 128) / 2;
                                    Verde = (Matriz_Bytes_ARGB[Índice + 1] + 128) / 2;
                                    Azul = (Matriz_Bytes_ARGB[Índice] + 128) / 2;

                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;

                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                }
                            }*/
                        }
                        else if (Filtro == Filtros.Contrast_positive)
                        {
                            /*uint num = (uint)c.ToArgb();
	                        D3DXCOLOR d3DXCOLOR;
	                        *(float*)(&d3DXCOLOR) = (float)(int)(byte)(num >> 16) * 0.003921569f;
	                        *(float*)(ref d3DXCOLOR + 4) = (float)(int)(byte)(num >> 8) * 0.003921569f;
	                        *(float*)(ref d3DXCOLOR + 8) = (float)(int)(byte)num * 0.003921569f;
	                        *(float*)(ref d3DXCOLOR + 12) = (float)(int)(byte)(num >> 24) * 0.003921569f;
	                        D3DXCOLOR d3DXCOLOR2;
	                        global::<Module>.D3DXColorAdjustContrast(&d3DXCOLOR2, &d3DXCOLOR, s);
	                        uint num2 = (*(float*)(&d3DXCOLOR2) >= 1f) ? 255u : ((!(*(float*)(&d3DXCOLOR2) <= 0f)) ? ((uint)(double)(*(float*)(&d3DXCOLOR2) * 255f + 0.5f)) : 0u);
	                        uint num3 = (*(float*)(ref d3DXCOLOR2 + 4) >= 1f) ? 255u : ((!(*(float*)(ref d3DXCOLOR2 + 4) <= 0f)) ? ((uint)(double)(*(float*)(ref d3DXCOLOR2 + 4) * 255f + 0.5f)) : 0u);
	                        uint num4 = (*(float*)(ref d3DXCOLOR2 + 8) >= 1f) ? 255u : ((!(*(float*)(ref d3DXCOLOR2 + 8) <= 0f)) ? ((uint)(double)(*(float*)(ref d3DXCOLOR2 + 8) * 255f + 0.5f)) : 0u);
	                        uint num5 = (*(float*)(ref d3DXCOLOR2 + 12) >= 1f) ? 255u : ((!(*(float*)(ref d3DXCOLOR2 + 12) <= 0f)) ? ((uint)(double)(*(float*)(ref d3DXCOLOR2 + 12) * 255f + 0.5f)) : 0u);
	                        return Color.FromArgb((int)((((((num5 << 8) | num2) << 8) | num3) << 8) | num4));*/

                            /*int Valor_R = 128, Valor_G = 128, Valor_B = 128;
                            bool Modo_Escala_Grises = false;

                            bool Habilitar_Menos_R = false;
                            bool Habilitar_Más_R = false;
                            bool Habilitar_Menos_G = false;
                            bool Habilitar_Más_G = false;
                            bool Habilitar_Menos_B = false;
                            bool Habilitar_Más_B = false;

                            if (Valor_R < 0)
                            {
                                Habilitar_Menos_R = true;
                                Valor_R = (int)(Valor_R * -1);
                            }
                            else if (Valor_R > 0) Habilitar_Más_R = true;

                            if (Valor_G < 0)
                            {
                                Habilitar_Menos_G = true;
                                Valor_G = (int)(Valor_G * -1);
                            }
                            else if (Valor_G > 0) Habilitar_Más_G = true;

                            if (Valor_B < 0)
                            {
                                Habilitar_Menos_B = true;
                                Valor_B = (int)(Valor_B * -1);
                            }
                            else if (Valor_B > 0) Habilitar_Más_B = true;

                            int Resto_Valor_R = (int)(255 - Valor_R);
                            int Resto_Valor_G = (int)(255 - Valor_G);
                            int Resto_Valor_B = (int)(255 - Valor_B);

                            int Rojo = 0;
                            int Verde = 0;
                            int Azul = 0;

                            Double Gris = 0;

                            Double Rojo_Máximo = 0;
                            Double Verde_Máximo = 0;
                            Double Azul_Máximo = 0;

                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Rojo = Matriz_Bytes_ARGB[Índice + 2];
                                    Verde = Matriz_Bytes_ARGB[Índice + 1];
                                    Azul = Matriz_Bytes_ARGB[Índice];

                                    //Gris = (Double)((Double)((Double)Rojo + (Double)Verde + (Double)Azul) / (Double)3);
                                    Gris = 128;

                                    if (Habilitar_Menos_R == true)
                                    {
                                        Rojo = (int)Math.Round((Double)(((Double)((Double)Gris * (Double)Valor_R) + (Double)((Double)Rojo * (Double)Resto_Valor_R)) / (Double)255), 0);
                                    }
                                    else if (Habilitar_Más_R == true)
                                    {
                                        //if (Rojo <= 127) Rojo_Máximo = 0;
                                        //else if (Rojo >= 128) Rojo_Máximo = 255;

                                        Rojo_Máximo = (Double)((Double)((Double)Rojo * (Double)Rojo) / (Double)Gris);
                                        Rojo = (int)Math.Round((Double)(((Double)((Double)Rojo_Máximo * (Double)Valor_R) + (Double)((Double)Rojo * (Double)Resto_Valor_R)) / (Double)255), 0);
                                    }

                                    if (Habilitar_Menos_G == true)
                                    {
                                        Verde = (int)Math.Round((Double)(((Double)((Double)Gris * (Double)Valor_G) + (Double)((Double)Verde * (Double)Resto_Valor_G)) / (Double)255), 0);
                                    }
                                    else if (Habilitar_Más_G == true)
                                    {
                                        //if (Verde <= 127) Verde_Máximo = 0;
                                        //else if (Verde >= 128) Verde_Máximo = 255;

                                        Verde_Máximo = (Double)((Double)((Double)Verde * (Double)Verde) / (Double)Gris);
                                        Verde = (int)Math.Round((Double)(((Double)((Double)Verde_Máximo * (Double)Valor_G) + (Double)((Double)Verde * (Double)Resto_Valor_G)) / (Double)255), 0);
                                    }

                                    if (Habilitar_Menos_B == true)
                                    {
                                        Azul = (int)Math.Round((Double)(((Double)((Double)Gris * (Double)Valor_B) + (Double)((Double)Azul * (Double)Resto_Valor_B)) / (Double)255), 0);
                                    }
                                    else if (Habilitar_Más_B == true)
                                    {
                                        //if (Azul <= 127) Azul_Máximo = 0;
                                        //else if (Azul >= 128) Azul_Máximo = 255;

                                        Azul_Máximo = (Double)((Double)((Double)Azul * (Double)Azul) / (Double)Gris);
                                        Azul = (int)Math.Round((Double)(((Double)((Double)Azul_Máximo * (Double)Valor_B) + (Double)((Double)Azul * (Double)Resto_Valor_B)) / (Double)255), 0);
                                    }

                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;

                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                }
                            }*/

                            /*int Contraste = 128;
                            bool Habilitar_Contraste_Menos = false;
                            bool Habilitar_Contraste_Más = false;
                            if (Contraste < 0)
                            {
                                Habilitar_Contraste_Menos = true;
                                Contraste = -Contraste;
                            }
                            else if (Contraste > 0) Habilitar_Contraste_Más = true;
                            int Resto_Contraste = 255 - Contraste;
                            int Gris = 127;
                            int Rojo_Máximo, Verde_Máximo, Azul_Máximo = 0;
                            int Rojo, Verde, Azul;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Rojo_Máximo = ((Matriz_Bytes_ARGB[Índice + 2] * Matriz_Bytes_ARGB[Índice + 2]) / Gris);
                                    Rojo = ((Rojo_Máximo * Contraste) + (Matriz_Bytes_ARGB[Índice + 2] * Resto_Contraste)) / 255;

                                    Verde_Máximo = ((Matriz_Bytes_ARGB[Índice + 1] * Matriz_Bytes_ARGB[Índice + 1]) / Gris);
                                    Verde = ((Rojo_Máximo * Contraste) + (Matriz_Bytes_ARGB[Índice + 1] * Resto_Contraste)) / 255;

                                    Azul_Máximo = ((Matriz_Bytes_ARGB[Índice] * Matriz_Bytes_ARGB[Índice]) / Gris);
                                    Azul = ((Rojo_Máximo * Contraste) + (Matriz_Bytes_ARGB[Índice] * Resto_Contraste)) / 255;

                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;

                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                }
                            }*/
                        }
                        else if (Filtro == Filtros.Saturation_negative)
                        {
                            byte Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    if (Matriz_Bytes_ARGB[Índice] != Matriz_Bytes_ARGB[Índice + 1] || Matriz_Bytes_ARGB[Índice] != Matriz_Bytes_ARGB[Índice + 2]) // Not gray scale.
                                    {
                                        Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                        Saturación /= 2d;
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
                        else if (Filtro == Filtros.Saturation_positive)
                        {
                            byte Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    if (Matriz_Bytes_ARGB[Índice] != Matriz_Bytes_ARGB[Índice + 1] || Matriz_Bytes_ARGB[Índice] != Matriz_Bytes_ARGB[Índice + 2]) // Not gray scale.
                                    {
                                        Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                        Saturación *= 2d;
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
                        else if (Filtro == Filtros.Lightness_negative)
                        {
                            byte Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                    Luminosidad /= 2d;
                                    if (Luminosidad < 0d) Luminosidad = 0d;
                                    else if (Luminosidad > 100d) Luminosidad = 100d;
                                    Program.HSL.To_RGB(Matiz, Saturación, Luminosidad, out Rojo, out Verde, out Azul);
                                    Matriz_Bytes_ARGB[Índice + 2] = Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = Verde;
                                    Matriz_Bytes_ARGB[Índice] = Azul;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Lightness_positive)
                        {
                            byte Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                    Luminosidad *= 2d;
                                    if (Luminosidad < 0d) Luminosidad = 0d;
                                    else if (Luminosidad > 100d) Luminosidad = 100d;
                                    Program.HSL.To_RGB(Matiz, Saturación, Luminosidad, out Rojo, out Verde, out Azul);
                                    Matriz_Bytes_ARGB[Índice + 2] = Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = Verde;
                                    Matriz_Bytes_ARGB[Índice] = Azul;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Intensity_negative)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)(Matriz_Bytes_ARGB[Índice + 2] / 2);
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)(Matriz_Bytes_ARGB[Índice + 1] / 2);
                                    Matriz_Bytes_ARGB[Índice] = (byte)(Matriz_Bytes_ARGB[Índice] / 2);
                                }
                            }
                        }
                        else if (Filtro == Filtros.Intensity_positive)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Math.Min(Matriz_Bytes_ARGB[Índice + 2] * 2, 255);
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Math.Min(Matriz_Bytes_ARGB[Índice + 1] * 2, 255);
                                    Matriz_Bytes_ARGB[Índice] = (byte)Math.Min(Matriz_Bytes_ARGB[Índice] * 2, 255);
                                }
                            }
                        }
                        else if (Filtro == Filtros.Square_root_negative)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 2] = Program.Matriz_Bytes_Filtro_Raíz_Cuadrada_Menos[Matriz_Bytes_ARGB[Índice + 2]];
                                    Matriz_Bytes_ARGB[Índice + 1] = Program.Matriz_Bytes_Filtro_Raíz_Cuadrada_Menos[Matriz_Bytes_ARGB[Índice + 1]];
                                    Matriz_Bytes_ARGB[Índice] = Program.Matriz_Bytes_Filtro_Raíz_Cuadrada_Menos[Matriz_Bytes_ARGB[Índice]];
                                }
                            }
                        }
                        else if (Filtro == Filtros.Square_root_positive)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 2] = Program.Matriz_Bytes_Filtro_Raíz_Cuadrada[Matriz_Bytes_ARGB[Índice + 2]];
                                    Matriz_Bytes_ARGB[Índice + 1] = Program.Matriz_Bytes_Filtro_Raíz_Cuadrada[Matriz_Bytes_ARGB[Índice + 1]];
                                    Matriz_Bytes_ARGB[Índice] = Program.Matriz_Bytes_Filtro_Raíz_Cuadrada[Matriz_Bytes_ARGB[Índice]];
                                }
                            }
                        }
                        else if (Filtro == Filtros.Logarithm_negative)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 2] = Program.Matriz_Bytes_Filtro_Logaritmo_Menos[Matriz_Bytes_ARGB[Índice + 2]];
                                    Matriz_Bytes_ARGB[Índice + 1] = Program.Matriz_Bytes_Filtro_Logaritmo_Menos[Matriz_Bytes_ARGB[Índice + 1]];
                                    Matriz_Bytes_ARGB[Índice] = Program.Matriz_Bytes_Filtro_Logaritmo_Menos[Matriz_Bytes_ARGB[Índice]];
                                }
                            }
                        }
                        else if (Filtro == Filtros.Logarithm_positive__night_vision___)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 2] = Program.Matriz_Bytes_Filtro_Logaritmo[Matriz_Bytes_ARGB[Índice + 2]];
                                    Matriz_Bytes_ARGB[Índice + 1] = Program.Matriz_Bytes_Filtro_Logaritmo[Matriz_Bytes_ARGB[Índice + 1]];
                                    Matriz_Bytes_ARGB[Índice] = Program.Matriz_Bytes_Filtro_Logaritmo[Matriz_Bytes_ARGB[Índice]];
                                }
                            }
                        }
                        else if (Filtro == Filtros.Normalization)
                        {
                            SortedDictionary<byte, long> Diccionario_Valores_R = new SortedDictionary<byte, long>();
                            SortedDictionary<byte, long> Diccionario_Valores_G = new SortedDictionary<byte, long>();
                            SortedDictionary<byte, long> Diccionario_Valores_B = new SortedDictionary<byte, long>();
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    if (!Diccionario_Valores_R.ContainsKey(Matriz_Bytes_ARGB[Índice + 2]))
                                    {
                                        Diccionario_Valores_R.Add(Matriz_Bytes_ARGB[Índice + 2], 1L);
                                    }
                                    //else Diccionario_Valores_R[Matriz_Bytes[Índice + 2]]++;

                                    if (!Diccionario_Valores_G.ContainsKey(Matriz_Bytes_ARGB[Índice + 1]))
                                    {
                                        Diccionario_Valores_G.Add(Matriz_Bytes_ARGB[Índice + 1], 1L);
                                    }
                                    //else Diccionario_Valores_G[Matriz_Bytes[Índice + 1]]++;

                                    if (!Diccionario_Valores_B.ContainsKey(Matriz_Bytes_ARGB[Índice]))
                                    {
                                        Diccionario_Valores_B.Add(Matriz_Bytes_ARGB[Índice], 1L);
                                    }
                                    //else Diccionario_Valores_B[Matriz_Bytes[Índice]]++;
                                }
                                if (Diccionario_Valores_R.Count >= 256 && Diccionario_Valores_G.Count >= 256 && Diccionario_Valores_B.Count >= 256) break;
                            }
                            if (Diccionario_Valores_R.Count < 256 || Diccionario_Valores_G.Count < 256 || Diccionario_Valores_B.Count < 256)
                            {
                                byte[] Matriz_Bytes_Normalización_R = new byte[256];
                                byte[] Matriz_Bytes_Normalización_G = new byte[256];
                                byte[] Matriz_Bytes_Normalización_B = new byte[256];
                                for (int Índice = 0; Índice < 256; Índice++)
                                {
                                    if (Diccionario_Valores_R.ContainsKey((byte)Índice))
                                    {
                                        int Valor_R = (int)Math.Round(((double)Índice * 256d) / (double)Diccionario_Valores_R.Count, MidpointRounding.AwayFromZero);
                                        if (Valor_R < 0) Valor_R = 0;
                                        else if (Valor_R > 255) Valor_R = 255;
                                        Matriz_Bytes_Normalización_R[Índice] = (byte)Valor_R;
                                    }
                                    if (Diccionario_Valores_G.ContainsKey((byte)Índice))
                                    {
                                        int Valor_G = (int)Math.Round(((double)Índice * 256d) / (double)Diccionario_Valores_G.Count, MidpointRounding.AwayFromZero);
                                        if (Valor_G < 0) Valor_G = 0;
                                        else if (Valor_G > 255) Valor_G = 255;
                                        Matriz_Bytes_Normalización_G[Índice] = (byte)Valor_G;
                                    }
                                    if (Diccionario_Valores_B.ContainsKey((byte)Índice))
                                    {
                                        int Valor_B = (int)Math.Round(((double)Índice * 256d) / (double)Diccionario_Valores_B.Count, MidpointRounding.AwayFromZero);
                                        if (Valor_B < 0) Valor_B = 0;
                                        else if (Valor_B > 255) Valor_B = 255;
                                        Matriz_Bytes_Normalización_B[Índice] = (byte)Valor_B;
                                    }
                                }
                                for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                                {
                                    for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_Normalización_R[Matriz_Bytes_ARGB[Índice + 2]];
                                        Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_Normalización_G[Matriz_Bytes_ARGB[Índice + 1]];
                                        Matriz_Bytes_ARGB[Índice] = Matriz_Bytes_Normalización_B[Matriz_Bytes_ARGB[Índice]];
                                    }
                                }
                            }
                            else Cancelar_Marshal_Copy = true;
                        }
                        else if (Filtro == Filtros.Centered_normalization)
                        {
                            //bool Cancelar = false;
                            byte Mínimo_R = 255, Mínimo_G = 255, Mínimo_B = 255;
                            byte Máximo_R = 0, Máximo_G = 0, Máximo_B = 0;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    if (Matriz_Bytes_ARGB[Índice + 2] < Mínimo_R) Mínimo_R = Matriz_Bytes_ARGB[Índice + 2];
                                    if (Matriz_Bytes_ARGB[Índice + 2] > Máximo_R) Máximo_R = Matriz_Bytes_ARGB[Índice + 2];
                                    if (Matriz_Bytes_ARGB[Índice + 1] < Mínimo_G) Mínimo_G = Matriz_Bytes_ARGB[Índice + 1];
                                    if (Matriz_Bytes_ARGB[Índice + 1] > Máximo_G) Máximo_G = Matriz_Bytes_ARGB[Índice + 1];
                                    if (Matriz_Bytes_ARGB[Índice] < Mínimo_B) Mínimo_B = Matriz_Bytes_ARGB[Índice];
                                    if (Matriz_Bytes_ARGB[Índice] > Máximo_B) Máximo_B = Matriz_Bytes_ARGB[Índice];
                                    if (Mínimo_R <= 0 && Mínimo_G <= 0 && Mínimo_B <= 0 && Máximo_R >= 255 && Máximo_G >= 255 && Máximo_B >= 255) break;
                                }
                            }
                            if (Mínimo_R > 0 || Mínimo_G > 0 || Mínimo_B > 0 || Máximo_R < 255 || Máximo_G < 255 || Máximo_B < 255)
                            {
                                byte[] Matriz_R = new byte[256];
                                byte[] Matriz_G = new byte[256];
                                byte[] Matriz_B = new byte[256];
                                int Media_R = Máximo_R - Mínimo_R;
                                int Media_G = Máximo_G - Mínimo_G;
                                int Media_B = Máximo_B - Mínimo_B;
                                for (int Índice = Mínimo_R, Media = 0; Índice <= Máximo_R; Índice++, Media++) if (Media > 0) Matriz_R[Índice] = (byte)((Media * 255) / Media_R);
                                for (int Índice = Mínimo_G, Media = 0; Índice <= Máximo_G; Índice++, Media++) if (Media > 0) Matriz_G[Índice] = (byte)((Media * 255) / Media_G);
                                for (int Índice = Mínimo_B, Media = 0; Índice <= Máximo_B; Índice++, Media++) if (Media > 0) Matriz_B[Índice] = (byte)((Media * 255) / Media_B);
                                for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                                {
                                    for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = Matriz_R[Matriz_Bytes_ARGB[Índice + 2]];
                                        Matriz_Bytes_ARGB[Índice + 1] = Matriz_G[Matriz_Bytes_ARGB[Índice + 1]];
                                        Matriz_Bytes_ARGB[Índice] = Matriz_B[Matriz_Bytes_ARGB[Índice]];
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Sepia)
                        {
                            int Gris, Rojo, Verde, Azul;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Gris = Matriz_Bytes_ARGB[Índice + 2] + Matriz_Bytes_ARGB[Índice + 1] + Matriz_Bytes_ARGB[Índice];
                                    Rojo = (Gris * 306) / 765; // 1.2 * 255 = 306.
                                    Verde = Gris / 3; // 1.0 = (R + G + B) / 3.
                                    Azul = (Gris * 204) / 765; // 0.8 * 255 = 204.

                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;

                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                }
                            }
                        }

                        else if (Filtro == Filtros.Pixelate)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y += 8)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X += 8)
                                {
                                    int Rojo = 0, Verde = 0, Azul = 0, Divisor = 0;
                                    for (int Subíndice_Y = 0; Subíndice_Y < 8; Subíndice_Y++)
                                    {
                                        for (int Subíndice_X = 0; Subíndice_X < 8; Subíndice_X++)
                                        {
                                            if (Índice_X + Subíndice_X > -1 && Índice_X + Subíndice_X < Ancho && Índice_Y + Subíndice_Y > -1 && Índice_Y + Subíndice_Y < Alto)
                                            {
                                                Índice = (Bytes_Ancho * (Índice_Y + Subíndice_Y)) + ((Índice_X + Subíndice_X) * Bytes_Aumento);
                                                Rojo += Matriz_Bytes_ARGB[Índice + 2];
                                                Verde += Matriz_Bytes_ARGB[Índice + 1];
                                                Azul += Matriz_Bytes_ARGB[Índice];
                                                Divisor++;
                                            }
                                        }
                                    }
                                    Rojo /= Divisor;
                                    Verde /= Divisor;
                                    Azul /= Divisor;
                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;
                                    for (int Subíndice_Y = 0; Subíndice_Y < 8; Subíndice_Y++)
                                    {
                                        for (int Subíndice_X = 0; Subíndice_X < 8; Subíndice_X++)
                                        {
                                            if (Índice_X + Subíndice_X > -1 && Índice_X + Subíndice_X < Ancho && Índice_Y + Subíndice_Y > -1 && Índice_Y + Subíndice_Y < Alto)
                                            {
                                                Índice = (Bytes_Ancho * (Índice_Y + Subíndice_Y)) + ((Índice_X + Subíndice_X) * Bytes_Aumento);
                                                Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                                Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                                Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Painting)
                        {
                            Bitmap Imagen_Painting = new Bitmap(Ancho, Alto, Imagen.PixelFormat);
                            Graphics Pintar_Painting = Graphics.FromImage(Imagen_Painting);
                            Pintar_Painting.CompositingMode = CompositingMode.SourceOver;
                            Pintar_Painting.CompositingQuality = CompositingQuality.HighQuality;
                            Pintar_Painting.InterpolationMode = InterpolationMode.NearestNeighbor;
                            Pintar_Painting.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Pintar_Painting.SmoothingMode = SmoothingMode.None;
                            Pintar_Painting.TextRenderingHint = TextRenderingHint.AntiAlias;
                            Pintar_Painting.Clear(Color.White);

                            //int Filtro_Artístico_Modo = 0; // 0
                            int Filtro_Artístico_Contador_x = 5; // 5
                            int Filtro_Artístico_Contador_y = 5; // 5
                            int Filtro_Artístico_Valor_x_Mínimo = 5; // 5
                            int Filtro_Artístico_Valor_y_Mínimo = 5; // 5
                            int Filtro_Artístico_Valor_x_Máximo = 13; // 13
                            int Filtro_Artístico_Valor_y_Máximo = 13; // 13
                            int Filtro_Artístico_Variación_Matiz = 16; // 16
                            int Filtro_Artístico_Presión_Alfa = 80; // 80
                            Color Filtro_Artístico_Color_Fondo = Color.FromArgb(255, 255, 255, 255);
                            //int Filtro_Artístico_Modo_Interpolación = 8; // 8
                            //int Filtro_Artístico_Modo_Antialiasing = 0; // 5
                            int Filtro_Artístico_Complejidad_Poligonal = 2; // 2
                            int Filtro_Artístico_Intensidad = 216; // 216
                            int Filtro_Artístico_Intensidad_Resto = 255 - Filtro_Artístico_Intensidad; // 216

                            int Contador_ARGB_x = 0;
                            int Contador_ARGB_y = 0;

                            Color Color_ARGB = Color.Empty;

                            int Variación_RGB_Mínima = (int)(Filtro_Artístico_Variación_Matiz * -1);
                            int Variación_RGB_Máxima = (int)(Filtro_Artístico_Variación_Matiz + 1);

                            int Rojo = 0;
                            int Verde = 0;
                            int Azul = 0;

                            int Variación_R = 0;
                            int Variación_G = 0;
                            int Variación_B = 0;

                            int Posición_x_Negativa_Mínima = (int)((Filtro_Artístico_Valor_x_Mínimo * -1) + 1);
                            int Posición_x_Negativa = (int)(Filtro_Artístico_Valor_x_Máximo * -1);
                            int Posición_x_Positiva_Mínima = Filtro_Artístico_Valor_x_Mínimo;
                            int Posición_x_Positiva = (int)(Filtro_Artístico_Valor_x_Máximo + 1);

                            int Posición_y_Negativa_Mínima = (int)((Filtro_Artístico_Valor_y_Mínimo * -1) + 1);
                            int Posición_y_Negativa = (int)(Filtro_Artístico_Valor_y_Máximo * -1);
                            int Posición_y_Positiva_Mínima = Filtro_Artístico_Valor_y_Mínimo;
                            int Posición_y_Positiva = (int)(Filtro_Artístico_Valor_y_Máximo + 1);

                            int Diferencia_x = (int)(Filtro_Artístico_Valor_x_Máximo / 2)/*(int)Math.Round((Double)((Double)Valor_x / (Double)2), 0)*/;
                            int Diferencia_y = (int)(Filtro_Artístico_Valor_y_Máximo / 2)/*(int)Math.Round((Double)((Double)Valor_y / (Double)2), 0)*/;

                            Point[] Puntos = new Point[(int)((Filtro_Artístico_Complejidad_Poligonal * 4) + /*5*/1)];
                            int Contador_Poligonal = 0;

                            int Límite_Poligonal_1 = (int)(((Puntos.Length - /*5*/1) / 4) * 1);
                            int Límite_Poligonal_2 = (int)(((Puntos.Length - /*5*/1) / 4) * 2);
                            int Límite_Poligonal_3 = (int)(((Puntos.Length - /*5*/1) / 4) * 3);
                            int Límite_Poligonal_4 = (int)(((Puntos.Length - /*5*/1) / 4) * 4);

                            for (int Índice_Y = -Filtro_Artístico_Contador_y, Índice = 0, Índice_Reflejo_X = 0; Índice_Y < Alto + Filtro_Artístico_Contador_y; Índice_Y += Filtro_Artístico_Contador_y)
                            {
                                for (int Índice_X = -Filtro_Artístico_Contador_x; Índice_X < Ancho + Filtro_Artístico_Contador_x; Índice_X += Filtro_Artístico_Contador_x)
                                {
                                    Contador_ARGB_x = Índice_X;
                                    Contador_ARGB_y = Índice_Y;
                                    if (Contador_ARGB_x < 0) Contador_ARGB_x = 0;
                                    else if (Contador_ARGB_x >= Imagen.Width) Contador_ARGB_x = Imagen.Width - 1;
                                    if (Contador_ARGB_y < 0) Contador_ARGB_y = 0;
                                    else if (Contador_ARGB_y >= Imagen.Height) Contador_ARGB_y = Imagen.Height - 1;
                                    Índice = (Bytes_Ancho * (Contador_ARGB_y)) + (Contador_ARGB_x * Bytes_Aumento);

                                    Variación_R = Program.Rand.Next(Variación_RGB_Mínima, Variación_RGB_Máxima);
                                    Variación_G = Program.Rand.Next(Variación_RGB_Mínima, Variación_RGB_Máxima);
                                    Variación_B = Program.Rand.Next(Variación_RGB_Mínima, Variación_RGB_Máxima);

                                    Rojo = (int)(Matriz_Bytes_ARGB[Índice + 2] + Variación_R);
                                    Verde = (int)(Matriz_Bytes_ARGB[Índice + 1] + Variación_G);
                                    Azul = (int)(Matriz_Bytes_ARGB[Índice] + Variación_B);

                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;

                                    for (Contador_Poligonal = 0; Contador_Poligonal < Límite_Poligonal_1; Contador_Poligonal++) // (x+, y-)
                                    {
                                        Puntos[Contador_Poligonal] = new Point((int)(((Índice_X - Filtro_Artístico_Contador_x) + Program.Rand.Next(0, Posición_x_Positiva)) + Filtro_Artístico_Valor_x_Mínimo), (int)(((Índice_Y - Filtro_Artístico_Contador_y) - Program.Rand.Next(Posición_y_Negativa, 1)) - Filtro_Artístico_Valor_y_Mínimo));
                                    }
                                    for (; Contador_Poligonal < Límite_Poligonal_2; Contador_Poligonal++) // (x+, y+)
                                    {
                                        Puntos[Contador_Poligonal] = new Point((int)(((Índice_X - Filtro_Artístico_Contador_x) + Program.Rand.Next(0, Posición_x_Positiva)) + Filtro_Artístico_Valor_x_Mínimo), (int)(((Índice_Y - Filtro_Artístico_Contador_y) + Program.Rand.Next(0, Posición_y_Positiva)) + Filtro_Artístico_Valor_y_Mínimo));
                                    }
                                    for (; Contador_Poligonal < Límite_Poligonal_3; Contador_Poligonal++) // (x-, y+)
                                    {
                                        Puntos[Contador_Poligonal] = new Point((int)(((Índice_X - Filtro_Artístico_Contador_x) - Program.Rand.Next(Posición_x_Negativa, 1)) - Filtro_Artístico_Valor_x_Mínimo), (int)(((Índice_Y - Filtro_Artístico_Contador_y) + Program.Rand.Next(0, Posición_y_Positiva)) + Filtro_Artístico_Valor_y_Mínimo));
                                    }
                                    for (; Contador_Poligonal < Límite_Poligonal_4; Contador_Poligonal++) // (x-, y-)
                                    {
                                        Puntos[Contador_Poligonal] = new Point((int)(((Índice_X - Filtro_Artístico_Contador_x) - Program.Rand.Next(Posición_x_Negativa, 1)) - Filtro_Artístico_Valor_x_Mínimo), (int)(((Índice_Y - Filtro_Artístico_Contador_y) - Program.Rand.Next(Posición_y_Negativa, 1)) - Filtro_Artístico_Valor_y_Mínimo));
                                    }

                                    Puntos[(int)(Puntos.Length - 1)] = Puntos[0];

                                    SolidBrush Pincel_Painting = new SolidBrush(Color.FromArgb(Filtro_Artístico_Presión_Alfa, Rojo, Verde, Azul));
                                    Pintar_Painting.FillPolygon(Pincel_Painting, Puntos);
                                    Pincel_Painting.Dispose();
                                    Pincel_Painting = null;
                                }
                            }
                            Pintar_Painting.Dispose();
                            Pintar_Painting = null;

                            BitmapData Bitmap_Data_Painting = Imagen_Painting.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadOnly, Imagen_Painting.PixelFormat);
                            byte[] Matriz_Bytes_ARGB_Painting = new byte[Math.Abs(Bitmap_Data_Painting.Stride) * Alto];
                            Marshal.Copy(Bitmap_Data_Painting.Scan0, Matriz_Bytes_ARGB_Painting, 0, Matriz_Bytes_ARGB_Painting.Length);
                            Imagen_Painting.UnlockBits(Bitmap_Data_Painting);
                            Bitmap_Data_Painting = null;
                            Imagen_Painting.Dispose();
                            Imagen_Painting = null;

                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Rojo = ((Matriz_Bytes_ARGB_Painting[Índice + 2] * Filtro_Artístico_Intensidad) / 255) + ((Matriz_Bytes_ARGB[Índice + 2] * Filtro_Artístico_Intensidad_Resto) / 255);
                                    Verde = ((Matriz_Bytes_ARGB_Painting[Índice + 1] * Filtro_Artístico_Intensidad) / 255) + ((Matriz_Bytes_ARGB[Índice + 1] * Filtro_Artístico_Intensidad_Resto) / 255);
                                    Azul = ((Matriz_Bytes_ARGB_Painting[Índice] * Filtro_Artístico_Intensidad) / 255) + ((Matriz_Bytes_ARGB[Índice] * Filtro_Artístico_Intensidad_Resto) / 255);

                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;

                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                }
                            }
                            Matriz_Bytes_ARGB_Painting = null;
                        }
                        else if (Filtro >= Filtros.Solarize_minimum && Filtro <= Filtros.Metalize_maximum)
                        {
                            byte[] Matriz_Bytes_Solarizar = new byte[256];
                            bool Solarizar = Filtro >= Filtros.Solarize_minimum && Filtro <= Filtros.Solarize_maximum;
                            if (Filtro == Filtros.Solarize_minimum || Filtro == Filtros.Metalize_maximum)
                            {
                                for (int Índice = 0, Valor = 0; Índice < Matriz_Bytes_Solarizar.Length; Índice++)
                                {
                                    Valor = ((Índice * (255 - Índice)) * 255) / (127 * 128);
                                    if (Valor < 0) Valor = 0;
                                    else if (Valor > 255) Valor = 255;
                                    Matriz_Bytes_Solarizar[Índice] = (byte)Valor;
                                }
                                if (Solarizar)
                                {
                                    Array.Reverse(Matriz_Bytes_Solarizar);
                                    for (int Índice = 0; Índice < Matriz_Bytes_Solarizar.Length; Índice++)
                                    {
                                        Matriz_Bytes_Solarizar[Índice] = (byte)(255 - Matriz_Bytes_Solarizar[Índice]);
                                    }
                                }
                            }
                            else if (Filtro == Filtros.Solarize_medium || Filtro == Filtros.Metalize_medium)
                            {
                                for (int Índice = 0; Índice < Matriz_Bytes_Solarizar.Length; Índice++)
                                {
                                    Matriz_Bytes_Solarizar[Índice] = (byte)(Math.Max(Índice, 255 - Índice) - Math.Min(Índice, 255 - Índice));
                                }
                                if (!Solarizar)
                                {
                                    Array.Reverse(Matriz_Bytes_Solarizar);
                                    for (int Índice = 0; Índice < Matriz_Bytes_Solarizar.Length; Índice++)
                                    {
                                        Matriz_Bytes_Solarizar[Índice] = (byte)(255 - Matriz_Bytes_Solarizar[Índice]);
                                    }
                                }
                            }
                            else if (Filtro == Filtros.Solarize_maximum || Filtro == Filtros.Metalize_minimum)
                            {
                                for (int Índice = 0, Valor = 0; Índice < Matriz_Bytes_Solarizar.Length; Índice++)
                                {
                                    Valor = (Math.Min(Índice, 255 - Índice) * 255) / Math.Max(Índice, 255 - Índice);
                                    if (Valor < 0) Valor = 0;
                                    else if (Valor > 255) Valor = 255;
                                    Matriz_Bytes_Solarizar[Índice] = (byte)Valor;
                                }
                                if (Solarizar)
                                {
                                    Array.Reverse(Matriz_Bytes_Solarizar);
                                    for (int Índice = 0; Índice < Matriz_Bytes_Solarizar.Length; Índice++)
                                    {
                                        Matriz_Bytes_Solarizar[Índice] = (byte)(255 - Matriz_Bytes_Solarizar[Índice]);
                                    }
                                }
                            }
                            if (Filtro >= Filtros.Solarize_minimum && Filtro <= Filtros.Solarize_maximum)
                            {
                                for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                                {
                                    for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_Solarizar[Matriz_Bytes_ARGB[Índice + 2]];
                                        Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_Solarizar[Matriz_Bytes_ARGB[Índice + 1]];
                                        Matriz_Bytes_ARGB[Índice] = Matriz_Bytes_Solarizar[Matriz_Bytes_ARGB[Índice]];
                                    }
                                }
                            }
                            else // Metalize.
                            {
                                for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                                {
                                    for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                    {
                                        Matriz_Bytes_ARGB[Índice] = (byte)((Matriz_Bytes_ARGB[Índice + 2] + Matriz_Bytes_ARGB[Índice + 1] + Matriz_Bytes_ARGB[Índice]) / 3);
                                        Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_Solarizar[Matriz_Bytes_ARGB[Índice]];
                                        Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_Solarizar[Matriz_Bytes_ARGB[Índice]];
                                        Matriz_Bytes_ARGB[Índice] = Matriz_Bytes_Solarizar[Matriz_Bytes_ARGB[Índice]];
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Reflection_horizontal)
                        {
                            byte[] Matriz_Bytes_ARGB_Original = (byte[])Matriz_Bytes_ARGB.Clone();
                            int Ancho_1 = Ancho - 1;
                            int Alto_1 = Alto - 1;
                            for (int Índice_Y = 0, Índice = 0, Índice_Reflejo_X = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Índice_Reflejo_X = (Bytes_Ancho * (Índice_Y)) + ((Ancho_1 - Índice_X) * Bytes_Aumento); // X.
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)((Matriz_Bytes_ARGB_Original[Índice + 2] + Matriz_Bytes_ARGB_Original[Índice_Reflejo_X + 2]) / 2);
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)((Matriz_Bytes_ARGB_Original[Índice + 1] + Matriz_Bytes_ARGB_Original[Índice_Reflejo_X + 1]) / 2);
                                    Matriz_Bytes_ARGB[Índice] = (byte)((Matriz_Bytes_ARGB_Original[Índice] + Matriz_Bytes_ARGB_Original[Índice_Reflejo_X]) / 2);
                                }
                            }
                            Matriz_Bytes_ARGB_Original = null;
                        }
                        else if (Filtro == Filtros.Reflection_vertical)
                        {
                            byte[] Matriz_Bytes_ARGB_Original = (byte[])Matriz_Bytes_ARGB.Clone();
                            int Ancho_1 = Ancho - 1;
                            int Alto_1 = Alto - 1;
                            for (int Índice_Y = 0, Índice = 0, Índice_Reflejo_Y = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Índice_Reflejo_Y = (Bytes_Ancho * (Alto_1 - Índice_Y)) + ((Índice_X) * Bytes_Aumento); // Y.

                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)((Matriz_Bytes_ARGB_Original[Índice + 2] + Matriz_Bytes_ARGB_Original[Índice_Reflejo_Y + 2]) / 2);
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)((Matriz_Bytes_ARGB_Original[Índice + 1] + Matriz_Bytes_ARGB_Original[Índice_Reflejo_Y + 1]) / 2);
                                    Matriz_Bytes_ARGB[Índice] = (byte)((Matriz_Bytes_ARGB_Original[Índice] + Matriz_Bytes_ARGB_Original[Índice_Reflejo_Y]) / 2);
                                }
                            }
                            Matriz_Bytes_ARGB_Original = null;
                        }
                        else if (Filtro == Filtros.Reflection_diagonal)
                        {
                            byte[] Matriz_Bytes_ARGB_Original = (byte[])Matriz_Bytes_ARGB.Clone();
                            int Ancho_1 = Ancho - 1;
                            int Alto_1 = Alto - 1;
                            for (int Índice_Y = 0, Índice = 0, Índice_Reflejo_XY = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Índice_Reflejo_XY = (Bytes_Ancho * (Alto_1 - Índice_Y)) + ((Ancho_1 - Índice_X) * Bytes_Aumento); // XY.
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)((Matriz_Bytes_ARGB_Original[Índice + 2] + Matriz_Bytes_ARGB_Original[Índice_Reflejo_XY + 2]) / 2);
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)((Matriz_Bytes_ARGB_Original[Índice + 1] + Matriz_Bytes_ARGB_Original[Índice_Reflejo_XY + 1]) / 2);
                                    Matriz_Bytes_ARGB[Índice] = (byte)((Matriz_Bytes_ARGB_Original[Índice] + Matriz_Bytes_ARGB_Original[Índice_Reflejo_XY]) / 2);
                                }
                            }
                            Matriz_Bytes_ARGB_Original = null;
                        }
                        else if (Filtro == Filtros.Reflection_quadruple)
                        {
                            byte[] Matriz_Bytes_ARGB_Original = (byte[])Matriz_Bytes_ARGB.Clone();
                            int Ancho_1 = Ancho - 1;
                            int Alto_1 = Alto - 1;
                            for (int Índice_Y = 0, Índice = 0, Índice_Reflejo_X = 0, Índice_Reflejo_Y = 0, Índice_Reflejo_XY = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Índice_Reflejo_X = (Bytes_Ancho * (Índice_Y)) + ((Ancho_1 - Índice_X) * Bytes_Aumento); // X.
                                    Índice_Reflejo_Y = (Bytes_Ancho * (Alto_1 - Índice_Y)) + ((Índice_X) * Bytes_Aumento); // Y.
                                    Índice_Reflejo_XY = (Bytes_Ancho * (Alto_1 - Índice_Y)) + ((Ancho_1 - Índice_X) * Bytes_Aumento); // XY.
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)((Matriz_Bytes_ARGB_Original[Índice + 2] + Matriz_Bytes_ARGB_Original[Índice_Reflejo_X + 2] + Matriz_Bytes_ARGB_Original[Índice_Reflejo_Y + 2] + Matriz_Bytes_ARGB_Original[Índice_Reflejo_XY + 2]) / 4);
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)((Matriz_Bytes_ARGB_Original[Índice + 1] + Matriz_Bytes_ARGB_Original[Índice_Reflejo_X + 1] + Matriz_Bytes_ARGB_Original[Índice_Reflejo_Y + 1] + Matriz_Bytes_ARGB_Original[Índice_Reflejo_XY + 1]) / 4);
                                    Matriz_Bytes_ARGB[Índice] = (byte)((Matriz_Bytes_ARGB_Original[Índice] + Matriz_Bytes_ARGB_Original[Índice_Reflejo_X] + Matriz_Bytes_ARGB_Original[Índice_Reflejo_Y] + Matriz_Bytes_ARGB_Original[Índice_Reflejo_XY]) / 4);
                                }
                            }
                            Matriz_Bytes_ARGB_Original = null;
                        }
                        else if (Filtro == Filtros.Noise_in_gray_scale)
                        {
                            int Rojo, Verde, Azul, Intensidad = 192, Resto = 255 - Intensidad;
                            byte[] Matriz_Bytes_ARGB_Ruido = new byte[Ancho * Alto];
                            Program.Rand_Xoroshiro128p.GetBytes(Matriz_Bytes_ARGB_Ruido);
                            for (int Índice_Y = 0, Índice = 0, Índice_Ruido = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento, Índice_Ruido++)
                                {
                                    Rojo = ((Matriz_Bytes_ARGB[Índice + 2] * Resto) / 255) + ((Matriz_Bytes_ARGB_Ruido[Índice_Ruido] * Intensidad) / 255);
                                    Verde = ((Matriz_Bytes_ARGB[Índice + 1] * Resto) / 255) + ((Matriz_Bytes_ARGB_Ruido[Índice_Ruido] * Intensidad) / 255);
                                    Azul = ((Matriz_Bytes_ARGB[Índice] * Resto) / 255) + ((Matriz_Bytes_ARGB_Ruido[Índice_Ruido] * Intensidad) / 255);

                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;

                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Noise_in_color)
                        {
                            int Rojo, Verde, Azul, Intensidad = 160, Resto = 255 - Intensidad;
                            byte[] Matriz_Bytes_ARGB_Ruido = new byte[(Ancho * Alto) * 3];
                            Program.Rand_Xoroshiro128p.GetBytes(Matriz_Bytes_ARGB_Ruido);
                            for (int Índice_Y = 0, Índice = 0, Índice_Ruido = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento, Índice_Ruido += 3)
                                {
                                    Rojo = ((Matriz_Bytes_ARGB[Índice + 2] * Resto) / 255) + ((Matriz_Bytes_ARGB_Ruido[Índice_Ruido + 2] * Intensidad) / 255);
                                    Verde = ((Matriz_Bytes_ARGB[Índice + 1] * Resto) / 255) + ((Matriz_Bytes_ARGB_Ruido[Índice_Ruido + 1] * Intensidad) / 255);
                                    Azul = ((Matriz_Bytes_ARGB[Índice] * Resto) / 255) + ((Matriz_Bytes_ARGB_Ruido[Índice_Ruido] * Intensidad) / 255);
                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Mono_chrome)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice + 2] < 128 ? byte.MinValue : byte.MaxValue;
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice + 1] < 128 ? byte.MinValue : byte.MaxValue;
                                    Matriz_Bytes_ARGB[Índice] = Matriz_Bytes_ARGB[Índice] < 128 ? byte.MinValue : byte.MaxValue;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Mono_chrome_automatic)
                        {
                            int Rojo = 0, Verde = 0, Azul = 0;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Rojo += Matriz_Bytes_ARGB[Índice + 2];
                                    Verde += Matriz_Bytes_ARGB[Índice + 1];
                                    Azul += Matriz_Bytes_ARGB[Índice];
                                }
                            }
                            int Píxeles = Ancho * Alto;
                            Rojo /= Píxeles;
                            Verde /= Píxeles;
                            Azul /= Píxeles;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice + 2] < Rojo ? byte.MinValue : byte.MaxValue;
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice + 1] < Verde ? byte.MinValue : byte.MaxValue;
                                    Matriz_Bytes_ARGB[Índice] = Matriz_Bytes_ARGB[Índice] < Azul ? byte.MinValue : byte.MaxValue;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Threshold)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice] = ((Matriz_Bytes_ARGB[Índice + 2] + Matriz_Bytes_ARGB[Índice + 1] + Matriz_Bytes_ARGB[Índice]) / 3) < 128 ? byte.MinValue : byte.MaxValue;
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                }
                            }
                        }
                        else if (Filtro == Filtros.Threshold_automatic)
                        {
                            int Total = 0;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice] = (byte)((Matriz_Bytes_ARGB[Índice + 2] + Matriz_Bytes_ARGB[Índice + 1] + Matriz_Bytes_ARGB[Índice]) / 3);
                                    Total += Matriz_Bytes_ARGB[Índice];
                                }
                            }
                            Total /= Ancho * Alto;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    if (Matriz_Bytes_ARGB[Índice] < Total)
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = 0;
                                        Matriz_Bytes_ARGB[Índice + 1] = 0;
                                        Matriz_Bytes_ARGB[Índice] = 0;
                                    }
                                    else
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = 255;
                                        Matriz_Bytes_ARGB[Índice + 1] = 255;
                                        Matriz_Bytes_ARGB[Índice] = 255;
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Blur_minimum)
                        {
                            byte[] Matriz_Bytes_ARGB_Original = (byte[])Matriz_Bytes_ARGB.Clone();
                            for (int Índice_Y = 0, Índice = 0, Subíndice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    byte Rojo = 255, Verde = 255, Azul = 255;
                                    for (int Subíndice_Y = -1, X = 0; Subíndice_Y <= 1; Subíndice_Y++, X++)
                                    {
                                        for (int Subíndice_X = -1, Y = 0; Subíndice_X <= 1; Subíndice_X++, Y++)
                                        {
                                            if (Índice_X + Subíndice_X > -1 && Índice_X + Subíndice_X < Ancho && Índice_Y + Subíndice_Y > -1 && Índice_Y + Subíndice_Y < Alto)
                                            {
                                                Subíndice = (Bytes_Ancho * (Índice_Y + Subíndice_Y)) + ((Índice_X + Subíndice_X) * Bytes_Aumento);
                                                if (Matriz_Bytes_ARGB_Original[Subíndice + 2] < Rojo) Rojo = Matriz_Bytes_ARGB_Original[Subíndice + 2];
                                                if (Matriz_Bytes_ARGB_Original[Subíndice + 1] < Verde) Verde = Matriz_Bytes_ARGB_Original[Subíndice + 1];
                                                if (Matriz_Bytes_ARGB_Original[Subíndice] < Azul) Azul = Matriz_Bytes_ARGB_Original[Subíndice];
                                            }
                                        }
                                    }
                                    Matriz_Bytes_ARGB[Índice + 2] = Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = Verde;
                                    Matriz_Bytes_ARGB[Índice] = Azul;
                                }
                            }
                            Matriz_Bytes_ARGB_Original = null;
                        }
                        else if (Filtro == Filtros.Blur_median)
                        {
                            byte[] Matriz_Bytes_ARGB_Original = (byte[])Matriz_Bytes_ARGB.Clone();
                            for (int Índice_Y = 0, Índice = 0, Subíndice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    List<byte> Lista_Rojo = new List<byte>();
                                    List<byte> Lista_Verde = new List<byte>();
                                    List<byte> Lista_Azul = new List<byte>();
                                    for (int Subíndice_Y = -1, X = 0; Subíndice_Y <= 1; Subíndice_Y++, X++)
                                    {
                                        for (int Subíndice_X = -1, Y = 0; Subíndice_X <= 1; Subíndice_X++, Y++)
                                        {
                                            if (Índice_X + Subíndice_X > -1 && Índice_X + Subíndice_X < Ancho && Índice_Y + Subíndice_Y > -1 && Índice_Y + Subíndice_Y < Alto)
                                            {
                                                Subíndice = (Bytes_Ancho * (Índice_Y + Subíndice_Y)) + ((Índice_X + Subíndice_X) * Bytes_Aumento);
                                                Lista_Rojo.Add(Matriz_Bytes_ARGB_Original[Subíndice + 2]);
                                                Lista_Verde.Add(Matriz_Bytes_ARGB_Original[Subíndice + 1]);
                                                Lista_Azul.Add(Matriz_Bytes_ARGB_Original[Subíndice]);
                                            }
                                        }
                                    }
                                    Lista_Rojo.Sort();
                                    Lista_Verde.Sort();
                                    Lista_Azul.Sort();
                                    Matriz_Bytes_ARGB[Índice + 2] = Lista_Rojo[Lista_Rojo.Count / 2];
                                    Matriz_Bytes_ARGB[Índice + 1] = Lista_Verde[Lista_Verde.Count / 2];
                                    Matriz_Bytes_ARGB[Índice] = Lista_Azul[Lista_Azul.Count / 2];
                                    Lista_Rojo = null;
                                    Lista_Verde = null;
                                    Lista_Azul = null;
                                }
                            }
                            Matriz_Bytes_ARGB_Original = null;
                        }
                        else if (Filtro == Filtros.Blur_maximum)
                        {
                            byte[] Matriz_Bytes_ARGB_Original = (byte[])Matriz_Bytes_ARGB.Clone();
                            for (int Índice_Y = 0, Índice = 0, Subíndice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    byte Rojo = 0, Verde = 0, Azul = 0;
                                    for (int Subíndice_Y = -1, X = 0; Subíndice_Y <= 1; Subíndice_Y++, X++)
                                    {
                                        for (int Subíndice_X = -1, Y = 0; Subíndice_X <= 1; Subíndice_X++, Y++)
                                        {
                                            if (Índice_X + Subíndice_X > -1 && Índice_X + Subíndice_X < Ancho && Índice_Y + Subíndice_Y > -1 && Índice_Y + Subíndice_Y < Alto)
                                            {
                                                Subíndice = (Bytes_Ancho * (Índice_Y + Subíndice_Y)) + ((Índice_X + Subíndice_X) * Bytes_Aumento);
                                                if (Matriz_Bytes_ARGB_Original[Subíndice + 2] > Rojo) Rojo = Matriz_Bytes_ARGB_Original[Subíndice + 2];
                                                if (Matriz_Bytes_ARGB_Original[Subíndice + 1] > Verde) Verde = Matriz_Bytes_ARGB_Original[Subíndice + 1];
                                                if (Matriz_Bytes_ARGB_Original[Subíndice] > Azul) Azul = Matriz_Bytes_ARGB_Original[Subíndice];
                                            }
                                        }
                                    }
                                    Matriz_Bytes_ARGB[Índice + 2] = Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = Verde;
                                    Matriz_Bytes_ARGB[Índice] = Azul;
                                }
                            }
                            Matriz_Bytes_ARGB_Original = null;
                        }
                        else if (Filtro == Filtros.Blur_mean)
                        {
                            byte[] Matriz_Bytes_ARGB_Original = (byte[])Matriz_Bytes_ARGB.Clone();
                            for (int Índice_Y = 0, Índice = 0, Subíndice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    int Rojo = 0, Verde = 0, Azul = 0, Total = 0;
                                    for (int Subíndice_Y = -1, X = 0; Subíndice_Y <= 1; Subíndice_Y++, X++)
                                    {
                                        for (int Subíndice_X = -1, Y = 0; Subíndice_X <= 1; Subíndice_X++, Y++)
                                        {
                                            if (Índice_X + Subíndice_X > -1 && Índice_X + Subíndice_X < Ancho && Índice_Y + Subíndice_Y > -1 && Índice_Y + Subíndice_Y < Alto)
                                            {
                                                Subíndice = (Bytes_Ancho * (Índice_Y + Subíndice_Y)) + ((Índice_X + Subíndice_X) * Bytes_Aumento);
                                                Rojo += Matriz_Bytes_ARGB_Original[Subíndice + 2];
                                                Verde += Matriz_Bytes_ARGB_Original[Subíndice + 1];
                                                Azul += Matriz_Bytes_ARGB_Original[Subíndice];
                                                Total++;
                                            }
                                        }
                                    }
                                    Rojo /= Total;
                                    Verde /= Total;
                                    Azul /= Total;
                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                }
                            }
                            Matriz_Bytes_ARGB_Original = null;
                        }
                        else if (Filtro == Filtros.Focus)
                        {
                            int[][] Matriz_Convolución = new int[3][] { new int[3] { 0, -1, 0 }, new int[3] { -1, 5, -1 }, new int[3] { 0, -1, 0 } };
                            //int Desplazamiento = 0;
                            byte[] Matriz_Bytes_ARGB_Original = (byte[])Matriz_Bytes_ARGB.Clone();
                            for (int Índice_Y = 0, Índice = 0, Subíndice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    int Rojo = 0, Verde = 0, Azul = 0;
                                    for (int Subíndice_Y = -1, X = 0; Subíndice_Y <= 1; Subíndice_Y++, X++)
                                    {
                                        for (int Subíndice_X = -1, Y = 0; Subíndice_X <= 1; Subíndice_X++, Y++)
                                        {
                                            if (Matriz_Convolución[X][Y] != 0 && Índice_X + Subíndice_X > -1 && Índice_X + Subíndice_X < Ancho && Índice_Y + Subíndice_Y > -1 && Índice_Y + Subíndice_Y < Alto)
                                            {
                                                Subíndice = (Bytes_Ancho * (Índice_Y + Subíndice_Y)) + ((Índice_X + Subíndice_X) * Bytes_Aumento);
                                                Rojo += Matriz_Bytes_ARGB_Original[Subíndice + 2] * Matriz_Convolución[X][Y];
                                                Verde += Matriz_Bytes_ARGB_Original[Subíndice + 1] * Matriz_Convolución[X][Y];
                                                Azul += Matriz_Bytes_ARGB_Original[Subíndice] * Matriz_Convolución[X][Y];
                                            }
                                        }
                                    }
                                    //Rojo += Desplazamiento;
                                    //Verde += Desplazamiento;
                                    //Azul += Desplazamiento;
                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                }
                            }
                            Matriz_Bytes_ARGB_Original = null;
                            Matriz_Convolución = null;
                        }
                        else if (Filtro == Filtros.Focus_maximum)
                        {
                            int[][] Matriz_Convolución = new int[3][] { new int[3] { -1, -1, -1 }, new int[3] { -1, 9, -1 }, new int[3] { -1, -1, -1 } };
                            //int Desplazamiento = 0;
                            byte[] Matriz_Bytes_ARGB_Original = (byte[])Matriz_Bytes_ARGB.Clone();
                            for (int Índice_Y = 0, Índice = 0, Subíndice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    int Rojo = 0, Verde = 0, Azul = 0;
                                    for (int Subíndice_Y = -1, X = 0; Subíndice_Y <= 1; Subíndice_Y++, X++)
                                    {
                                        for (int Subíndice_X = -1, Y = 0; Subíndice_X <= 1; Subíndice_X++, Y++)
                                        {
                                            if (Matriz_Convolución[X][Y] != 0 && Índice_X + Subíndice_X > -1 && Índice_X + Subíndice_X < Ancho && Índice_Y + Subíndice_Y > -1 && Índice_Y + Subíndice_Y < Alto)
                                            {
                                                Subíndice = (Bytes_Ancho * (Índice_Y + Subíndice_Y)) + ((Índice_X + Subíndice_X) * Bytes_Aumento);
                                                Rojo += Matriz_Bytes_ARGB_Original[Subíndice + 2] * Matriz_Convolución[X][Y];
                                                Verde += Matriz_Bytes_ARGB_Original[Subíndice + 1] * Matriz_Convolución[X][Y];
                                                Azul += Matriz_Bytes_ARGB_Original[Subíndice] * Matriz_Convolución[X][Y];
                                            }
                                        }
                                    }
                                    //Rojo += Desplazamiento;
                                    //Verde += Desplazamiento;
                                    //Azul += Desplazamiento;
                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                }
                            }
                            Matriz_Bytes_ARGB_Original = null;
                            Matriz_Convolución = null;
                        }
                        else if (Filtro == Filtros.Borders)
                        {
                            int[][] Matriz_Convolución = new int[3][] { new int[3] { -1, -1, -1 }, new int[3] { 0, 0, 0 }, new int[3] { 1, 1, 1 } };
                            //int Desplazamiento = 0;
                            byte[] Matriz_Bytes_ARGB_Original = (byte[])Matriz_Bytes_ARGB.Clone();
                            for (int Índice_Y = 0, Índice = 0, Subíndice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    int Rojo = 0, Verde = 0, Azul = 0;
                                    for (int Subíndice_Y = -1, X = 0; Subíndice_Y <= 1; Subíndice_Y++, X++)
                                    {
                                        for (int Subíndice_X = -1, Y = 0; Subíndice_X <= 1; Subíndice_X++, Y++)
                                        {
                                            if (Matriz_Convolución[X][Y] != 0 && Índice_X + Subíndice_X > -1 && Índice_X + Subíndice_X < Ancho && Índice_Y + Subíndice_Y > -1 && Índice_Y + Subíndice_Y < Alto)
                                            {
                                                Subíndice = (Bytes_Ancho * (Índice_Y + Subíndice_Y)) + ((Índice_X + Subíndice_X) * Bytes_Aumento);
                                                Rojo += Matriz_Bytes_ARGB_Original[Subíndice + 2] * Matriz_Convolución[X][Y];
                                                Verde += Matriz_Bytes_ARGB_Original[Subíndice + 1] * Matriz_Convolución[X][Y];
                                                Azul += Matriz_Bytes_ARGB_Original[Subíndice] * Matriz_Convolución[X][Y];
                                            }
                                        }
                                    }
                                    //Rojo += Desplazamiento;
                                    //Verde += Desplazamiento;
                                    //Azul += Desplazamiento;
                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                }
                            }
                            Matriz_Bytes_ARGB_Original = null;
                            Matriz_Convolución = null;
                        }
                        else if (Filtro == Filtros.Borders_maximum)
                        {
                            int[][] Matriz_Convolución = new int[3][] { new int[3] { -5, 0, 0 }, new int[3] { 0, 0, 0 }, new int[3] { 0, 0, 5 } };
                            //int Desplazamiento = 0;
                            byte[] Matriz_Bytes_ARGB_Original = (byte[])Matriz_Bytes_ARGB.Clone();
                            for (int Índice_Y = 0, Índice = 0, Subíndice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    int Rojo = 0, Verde = 0, Azul = 0;
                                    for (int Subíndice_Y = -1, X = 0; Subíndice_Y <= 1; Subíndice_Y++, X++)
                                    {
                                        for (int Subíndice_X = -1, Y = 0; Subíndice_X <= 1; Subíndice_X++, Y++)
                                        {
                                            if (Matriz_Convolución[X][Y] != 0 && Índice_X + Subíndice_X > -1 && Índice_X + Subíndice_X < Ancho && Índice_Y + Subíndice_Y > -1 && Índice_Y + Subíndice_Y < Alto)
                                            {
                                                Subíndice = (Bytes_Ancho * (Índice_Y + Subíndice_Y)) + ((Índice_X + Subíndice_X) * Bytes_Aumento);
                                                Rojo += Matriz_Bytes_ARGB_Original[Subíndice + 2] * Matriz_Convolución[X][Y];
                                                Verde += Matriz_Bytes_ARGB_Original[Subíndice + 1] * Matriz_Convolución[X][Y];
                                                Azul += Matriz_Bytes_ARGB_Original[Subíndice] * Matriz_Convolución[X][Y];
                                            }
                                        }
                                    }
                                    //Rojo += Desplazamiento;
                                    //Verde += Desplazamiento;
                                    //Azul += Desplazamiento;
                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                }
                            }
                            Matriz_Bytes_ARGB_Original = null;
                            Matriz_Convolución = null;
                        }
                        else if (Filtro == Filtros.Edges)
                        {
                            int[][] Matriz_Convolución = new int[3][] { new int[3] { 0, 0, 0 }, new int[3] { 0, 1, 0 }, new int[3] { 0, 0, -1 } };
                            //int Desplazamiento = 0;
                            byte[] Matriz_Bytes_ARGB_Original = (byte[])Matriz_Bytes_ARGB.Clone();
                            for (int Índice_Y = 0, Índice = 0, Subíndice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    int Rojo = 0, Verde = 0, Azul = 0;
                                    for (int Subíndice_Y = -1, X = 0; Subíndice_Y <= 1; Subíndice_Y++, X++)
                                    {
                                        for (int Subíndice_X = -1, Y = 0; Subíndice_X <= 1; Subíndice_X++, Y++)
                                        {
                                            if (Matriz_Convolución[X][Y] != 0 && Índice_X + Subíndice_X > -1 && Índice_X + Subíndice_X < Ancho && Índice_Y + Subíndice_Y > -1 && Índice_Y + Subíndice_Y < Alto)
                                            {
                                                Subíndice = (Bytes_Ancho * (Índice_Y + Subíndice_Y)) + ((Índice_X + Subíndice_X) * Bytes_Aumento);
                                                Rojo += Matriz_Bytes_ARGB_Original[Subíndice + 2] * Matriz_Convolución[X][Y];
                                                Verde += Matriz_Bytes_ARGB_Original[Subíndice + 1] * Matriz_Convolución[X][Y];
                                                Azul += Matriz_Bytes_ARGB_Original[Subíndice] * Matriz_Convolución[X][Y];
                                            }
                                        }
                                    }
                                    //Rojo += Desplazamiento;
                                    //Verde += Desplazamiento;
                                    //Azul += Desplazamiento;
                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                }
                            }
                            Matriz_Bytes_ARGB_Original = null;
                            Matriz_Convolución = null;
                        }
                        else if (Filtro == Filtros.Edges_maximum)
                        {
                            int[][] Matriz_Convolución = new int[3][] { new int[3] { 2, 0, 0 }, new int[3] { 0, -1, 0 }, new int[3] { 0, 0, -1 } };
                            //int[][] Matriz_Convolución = new int[3][] { new int[3] { 2, 0, 1 }, new int[3] { 0, -1, 0 }, new int[3] { -1, 0, -1 } }; // Embossing Propio, 26-06-2012, 07:43, Invertir X-Y a Y-X como XnView...
                            //int[][] Matriz_Convolución = new int[3][] { new int[3] { -2, 0, -1 }, new int[3] { 0, 0, 0 }, new int[3] { 1, 0, 2 } };
                            //int[][] Matriz_Convolución = new int[5][] { new int[5] { 0, 0, -1, 0, 0 }, new int[5] { 0, 0, -1, 0, 0 }, new int[5] { -1, -1, 9, -1, -1 }, new int[5] { 0, 0, -1, 0, 0 }, new int[5] { 0, 0, -1, 0, 0 } }; // Sharpen x5
                            //int[][] Matriz_Convolución = new int[5][] { new int[5] { 1, 1, 1, 1, 1 }, new int[5] { 1, 1, 1, 1, 1 }, new int[5] { 1, 1, 1, 1, 1 }, new int[5] { 1, 1, 1, 1, 1 }, new int[5] { 1, 1, 1, 1, 1 } }; // Blur x5
                            //int[][] Matriz_Convolución = new int[3][] { new int[3] { 0, -1, 0 }, new int[3] { -1, 5, -1 }, new int[3] { 0, -1, 0 } }; // Sharpen x3
                            //int[][] Matriz_Convolución = new int[3][] { new int[3] { 1, 1, 1 }, new int[3] { 1, 1, 1 }, new int[3] { 1, 1, 1 } }; // Blur
                            //int[][] Matriz_Convolución = new int[3][] { new int[3] { 0, 0, 0 }, new int[3] { 1, 1, 0 }, new int[3] { 0, 0, 0 } }; // Edge Enhance
                            //int[][] Matriz_Convolución = new int[3][] { new int[3]{ 0, 1, 0 }, new int[3]{ 1, -4, 1 }, new int[3]{ 0, 1, 0 } }; // Edge Detection
                            //int[][] Matriz_Convolución = new int[3][] { new int[3] { -2, -1, 0 }, new int[3] { -1, 1, 1 }, new int[3] { 0, 1, 2 } }; // Emboss
                            //int Desplazamiento = 0;
                            byte[] Matriz_Bytes_ARGB_Original = (byte[])Matriz_Bytes_ARGB.Clone();
                            for (int Índice_Y = 0, Índice = 0, Subíndice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    int Rojo = 0, Verde = 0, Azul = 0;
                                    for (int Subíndice_Y = -1, X = 0; Subíndice_Y <= 1; Subíndice_Y++, X++)
                                    {
                                        for (int Subíndice_X = -1, Y = 0; Subíndice_X <= 1; Subíndice_X++, Y++)
                                        {
                                            if (Matriz_Convolución[X][Y] != 0 && Índice_X + Subíndice_X > -1 && Índice_X + Subíndice_X < Ancho && Índice_Y + Subíndice_Y > -1 && Índice_Y + Subíndice_Y < Alto)
                                            {
                                                Subíndice = (Bytes_Ancho * (Índice_Y + Subíndice_Y)) + ((Índice_X + Subíndice_X) * Bytes_Aumento);
                                                Rojo += Matriz_Bytes_ARGB_Original[Subíndice + 2] * Matriz_Convolución[X][Y];
                                                Verde += Matriz_Bytes_ARGB_Original[Subíndice + 1] * Matriz_Convolución[X][Y];
                                                Azul += Matriz_Bytes_ARGB_Original[Subíndice] * Matriz_Convolución[X][Y];
                                            }
                                        }
                                    }
                                    //Rojo += Desplazamiento;
                                    //Verde += Desplazamiento;
                                    //Azul += Desplazamiento;
                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                }
                            }
                            Matriz_Bytes_ARGB_Original = null;
                            Matriz_Convolución = null;
                        }
                        else if (Filtro >= Filtros.Posterize_2_tones && Filtro <= Filtros.Posterize_128_tones)
                        {
                            /*int Posterizado = 256; // Original.
                            if (Filtro == Filtros.Posterize_128_tones) Posterizado = 128;
                            else if (Filtro == Filtros.Posterize_64_tones) Posterizado = 64;
                            else if (Filtro == Filtros.Posterize_32_tones) Posterizado = 32;
                            else if (Filtro == Filtros.Posterize_16_tones) Posterizado = 16;
                            else if (Filtro == Filtros.Posterize_8_tones) Posterizado = 8;
                            else if (Filtro == Filtros.Posterize_4_tones) Posterizado = 4;
                            else if (Filtro == Filtros.Posterize_2_tones) Posterizado = 2;
                            byte[] Matriz_Posterizado = new byte[256];
                            for (int Índice = 0; Índice < 256; Índice++)
                            {
                                Matriz_Posterizado[Índice] = (Índice % Posterizado) * ();
                            }
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Posterizado[Matriz_Bytes_ARGB[Índice + 2]];
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Posterizado[Matriz_Bytes_ARGB[Índice + 1]];
                                    Matriz_Bytes_ARGB[Índice] = Matriz_Posterizado[Matriz_Bytes_ARGB[Índice]];
                                }
                            }*/
                        }
                        else if (Filtro == Filtros.Differences_over_time__use_on_video___)
                        {
                            byte[] Matriz_Bytes_ARGB_Original = (byte[])Matriz_Bytes_ARGB.Clone();
                            if (Matriz_Bytes_Anterior == null || Matriz_Bytes_Anterior.Length != Matriz_Bytes_ARGB.Length)
                            {
                                Matriz_Bytes_Anterior = (byte[])Matriz_Bytes_ARGB.Clone();
                                Matriz_Bytes_Anterior_Filtrada = (byte[])Matriz_Bytes_ARGB.Clone();
                            }
                            int Diferencia_Máxima_R = 0;
                            int Diferencia_Máxima_G = 0;
                            int Diferencia_Máxima_B = 0;
                            // Find the maximum RGB differences.
                            int Diferencia_R, Diferencia_G, Diferencia_B;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Diferencia_R = Math.Abs(Matriz_Bytes_ARGB_Original[Índice + 2] - Matriz_Bytes_Anterior[Índice + 2]);
                                    Diferencia_G = Math.Abs(Matriz_Bytes_ARGB_Original[Índice + 1] - Matriz_Bytes_Anterior[Índice + 1]);
                                    Diferencia_B = Math.Abs(Matriz_Bytes_ARGB_Original[Índice] - Matriz_Bytes_Anterior[Índice]);
                                    if (Diferencia_R > Diferencia_Máxima_R) Diferencia_Máxima_R = Diferencia_R;
                                    if (Diferencia_G > Diferencia_Máxima_G) Diferencia_Máxima_G = Diferencia_G;
                                    if (Diferencia_B > Diferencia_Máxima_B) Diferencia_Máxima_B = Diferencia_B;
                                }
                            }
                            // Assign the new values.
                            //Matriz_Bytes = new byte[Matriz_Bytes.Length]; // Clear the whole array.
                            Array.Clear(Matriz_Bytes_ARGB, 0, Matriz_Bytes_ARGB.Length); // Clear the whole array.
                            if (Diferencia_Máxima_R > 0 || Diferencia_Máxima_G > 0 || Diferencia_Máxima_B > 0)
                            {
                                for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                                {
                                    for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                    {
                                        Diferencia_R = Math.Abs(Matriz_Bytes_ARGB_Original[Índice + 2] - Matriz_Bytes_Anterior[Índice + 2]);
                                        Diferencia_G = Math.Abs(Matriz_Bytes_ARGB_Original[Índice + 1] - Matriz_Bytes_Anterior[Índice + 1]);
                                        Diferencia_B = Math.Abs(Matriz_Bytes_ARGB_Original[Índice] - Matriz_Bytes_Anterior[Índice]);
                                        if (Diferencia_R > 0) Matriz_Bytes_ARGB[Índice + 2] = (byte)((Diferencia_R * 255) / Diferencia_Máxima_R);
                                        if (Diferencia_G > 0) Matriz_Bytes_ARGB[Índice + 1] = (byte)((Diferencia_G * 255) / Diferencia_Máxima_G);
                                        if (Diferencia_B > 0) Matriz_Bytes_ARGB[Índice] = (byte)((Diferencia_B * 255) / Diferencia_Máxima_B);
                                    }
                                }
                                Matriz_Bytes_Anterior_Filtrada = (byte[])Matriz_Bytes_ARGB.Clone();
                            }
                            else // Avoid flickering if nothing has changed. The screen won't turn black.
                            {
                                Matriz_Bytes_ARGB = (byte[])Matriz_Bytes_Anterior_Filtrada.Clone();
                            }
                            Matriz_Bytes_Anterior = Matriz_Bytes_ARGB_Original;
                        }
                        else if (Filtro >= Filtros.Minimum_difference && Filtro <= Filtros.Minimum_difference_for_JPEG_200)
                        {
                            byte[] Matriz_Bytes_ARGB_Original = (byte[])Matriz_Bytes_ARGB.Clone();
                            int Ruido_JPEG = 0; // Used to avoid JPEG noise. 0 = Disabled. 2 = Default.
                            try
                            {
                                string Nombre = Filtro.ToString();
                                if (char.IsDigit(Nombre[Nombre.Length - 1]))
                                {
                                    Ruido_JPEG = int.Parse(Nombre.Replace("Minimum_difference_for_JPEG_", null));
                                }
                            }
                            catch { Ruido_JPEG = 0; }
                            for (int Índice_Y = 0, Índice = 0, Subíndice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    byte Rojo = Matriz_Bytes_ARGB_Original[Índice + 2];
                                    byte Verde = Matriz_Bytes_ARGB_Original[Índice + 1];
                                    byte Azul = Matriz_Bytes_ARGB_Original[Índice];
                                    int Valor_R = 255, Valor_G = 255, Valor_B = 255;
                                    int Valor_R_Temporal = 0, Valor_G_Temporal = 0, Valor_B_Temporal = 0;
                                    for (int Subíndice_Y = -1, X = 0; Subíndice_Y <= 1; Subíndice_Y++, X++)
                                    {
                                        for (int Subíndice_X = -1, Y = 0; Subíndice_X <= 1; Subíndice_X++, Y++)
                                        {
                                            if ((Subíndice_X != 0 || Subíndice_Y != 0) && Índice_X + Subíndice_X > -1 && Índice_X + Subíndice_X < Ancho && Índice_Y + Subíndice_Y > -1 && Índice_Y + Subíndice_Y < Alto)
                                            {
                                                Subíndice = (Bytes_Ancho * (Índice_Y + Subíndice_Y)) + ((Índice_X + Subíndice_X) * Bytes_Aumento);
                                                Valor_R_Temporal = Math.Abs(Rojo - Matriz_Bytes_ARGB_Original[Subíndice + 2]);
                                                Valor_G_Temporal = Math.Abs(Verde - Matriz_Bytes_ARGB_Original[Subíndice + 1]);
                                                Valor_B_Temporal = Math.Abs(Azul - Matriz_Bytes_ARGB_Original[Subíndice]);
                                                if (Valor_R_Temporal < Valor_R && Valor_R_Temporal > Ruido_JPEG && Matriz_Bytes_ARGB_Original[Subíndice + 2] < Rojo) Valor_R = Valor_R_Temporal;
                                                if (Valor_G_Temporal < Valor_G && Valor_G_Temporal > Ruido_JPEG && Matriz_Bytes_ARGB_Original[Subíndice + 1] < Verde) Valor_G = Valor_G_Temporal;
                                                if (Valor_B_Temporal < Valor_B && Valor_B_Temporal > Ruido_JPEG && Matriz_Bytes_ARGB_Original[Subíndice] < Azul) Valor_B = Valor_B_Temporal;
                                            }
                                        }
                                    }
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)(255 - Valor_R);
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)(255 - Valor_G);
                                    Matriz_Bytes_ARGB[Índice] = (byte)(255 - Valor_B);
                                }
                            }
                            Matriz_Bytes_ARGB_Original = null;
                        }
                        else if (Filtro == Filtros.Rainbow_at_30_degrees)
                        {
                            byte[] Matriz_Bytes_ARGB_Original = (byte[])Matriz_Bytes_ARGB.Clone();
                            int Porcentaje = 75;
                            int Matiz_Índice = 0;
                            int Matiz_Subíndice = 0;
                            Color Color_ARGB = Color.Empty;
                            for (int Índice_Y = 0, Índice = 0, Subíndice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    bool Cancelar = false;
                                    Matiz_Índice = Program.HSL.Obtener_Matiz_0_a_11(Matriz_Bytes_ARGB_Original[Índice + 2], Matriz_Bytes_ARGB_Original[Índice + 1], Matriz_Bytes_ARGB_Original[Índice]);

                                    byte Rojo = Matriz_Bytes_ARGB_Original[Índice + 2];
                                    byte Verde = Matriz_Bytes_ARGB_Original[Índice + 1];
                                    byte Azul = Matriz_Bytes_ARGB_Original[Índice];
                                    int Valor_R = 255, Valor_G = 255, Valor_B = 255;
                                    int Valor_R_Temporal = 0, Valor_G_Temporal = 0, Valor_B_Temporal = 0;
                                    for (int Subíndice_Y = -1, X = 0; Subíndice_Y <= 1; Subíndice_Y++, X++)
                                    {
                                        for (int Subíndice_X = -1, Y = 0; Subíndice_X <= 1; Subíndice_X++, Y++)
                                        {
                                            if ((Subíndice_X != 0 || Subíndice_Y != 0) && Índice_X + Subíndice_X > -1 && Índice_X + Subíndice_X < Ancho && Índice_Y + Subíndice_Y > -1 && Índice_Y + Subíndice_Y < Alto)
                                            {
                                                Subíndice = (Bytes_Ancho * (Índice_Y + Subíndice_Y)) + ((Índice_X + Subíndice_X) * Bytes_Aumento);
                                                Matiz_Subíndice = Program.HSL.Obtener_Matiz_0_a_11(Matriz_Bytes_ARGB_Original[Subíndice + 2], Matriz_Bytes_ARGB_Original[Subíndice + 1], Matriz_Bytes_ARGB_Original[Subíndice]);
                                                if (Matiz_Índice != Matiz_Subíndice)
                                                {
                                                    Cancelar = true;
                                                    Subíndice_X = 2;
                                                    Subíndice_Y = 2;
                                                    break;
                                                }


                                                Valor_R_Temporal = Math.Abs(Rojo - Matriz_Bytes_ARGB_Original[Subíndice + 2]);
                                                Valor_G_Temporal = Math.Abs(Verde - Matriz_Bytes_ARGB_Original[Subíndice + 1]);
                                                Valor_B_Temporal = Math.Abs(Azul - Matriz_Bytes_ARGB_Original[Subíndice]);
                                                if (Valor_R_Temporal < Valor_R && Valor_R_Temporal > 0 && Matriz_Bytes_ARGB_Original[Subíndice + 2] < Rojo) Valor_R = Valor_R_Temporal;
                                                if (Valor_G_Temporal < Valor_G && Valor_G_Temporal > 0 && Matriz_Bytes_ARGB_Original[Subíndice + 1] < Verde) Valor_G = Valor_G_Temporal;
                                                if (Valor_B_Temporal < Valor_B && Valor_B_Temporal > 0 && Matriz_Bytes_ARGB_Original[Subíndice] < Azul) Valor_B = Valor_B_Temporal;
                                            }
                                            /*else
                                            {
                                                Cancelar = true;
                                                Subíndice_X = 2;
                                                Subíndice_Y = 2;
                                                break;
                                            }*/
                                        }
                                    }

                                    if (!Cancelar)
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = (byte)((Matriz_Bytes_ARGB[Índice + 2] * Porcentaje) / 100);
                                        Matriz_Bytes_ARGB[Índice + 1] = (byte)((Matriz_Bytes_ARGB[Índice + 1] * Porcentaje) / 100);
                                        Matriz_Bytes_ARGB[Índice] = (byte)((Matriz_Bytes_ARGB[Índice] * Porcentaje) / 100);
                                    }
                                    else
                                    {
                                        Color_ARGB = Program.Obtener_Color_Puro_0_a_11(Matiz_Índice);
                                        Matriz_Bytes_ARGB[Índice + 2] = Color_ARGB.R;
                                        Matriz_Bytes_ARGB[Índice + 1] = Color_ARGB.G;
                                        Matriz_Bytes_ARGB[Índice] = Color_ARGB.B;
                                    }
                                    //Matriz_Bytes[Índice + 2] = (byte)(255 - Valor_R);
                                    //Matriz_Bytes[Índice + 1] = (byte)(255 - Valor_G);
                                    //Matriz_Bytes[Índice] = (byte)(255 - Valor_B);
                                }
                            }
                            Matriz_Bytes_ARGB_Original = null;
                        }
                        else if (Filtro == Filtros.Rainbow_at_30_degrees_filled)
                        {
                            int Matiz = 0;
                            Color Color_ARGB = Color.Empty;
                            int Porcentaje = 50;
                            int Porcentaje_Resto = 100 - Porcentaje;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matiz = Program.HSL.Obtener_Matiz_0_a_11(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]);
                                    Color_ARGB = Program.Obtener_Color_Puro_0_a_11(Matiz);
                                    //Matriz_Bytes[Índice + 2] = Color_ARGB.R;
                                    //Matriz_Bytes[Índice + 1] = Color_ARGB.G;
                                    //Matriz_Bytes[Índice] = Color_ARGB.B;
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)(((Color_ARGB.R * Porcentaje) + (Matriz_Bytes_ARGB[Índice + 2] * Porcentaje_Resto)) / 100);
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)(((Color_ARGB.G * Porcentaje) + (Matriz_Bytes_ARGB[Índice + 1] * Porcentaje_Resto)) / 100);
                                    Matriz_Bytes_ARGB[Índice] = (byte)(((Color_ARGB.B * Porcentaje) + (Matriz_Bytes_ARGB[Índice] * Porcentaje_Resto)) / 100);
                                }
                            }
                        }
                        else if (Filtro == Filtros.Pixels_in_color)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    if (Matriz_Bytes_ARGB[Índice] == Matriz_Bytes_ARGB[Índice + 1] && Matriz_Bytes_ARGB[Índice] == Matriz_Bytes_ARGB[Índice + 2])
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = 0;
                                        Matriz_Bytes_ARGB[Índice + 1] = 0;
                                        Matriz_Bytes_ARGB[Índice] = 0;
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Pixels_in_gray_scale)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    if (Matriz_Bytes_ARGB[Índice] != Matriz_Bytes_ARGB[Índice + 1] || Matriz_Bytes_ARGB[Índice] != Matriz_Bytes_ARGB[Índice + 2])
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = 0;
                                        Matriz_Bytes_ARGB[Índice + 1] = 0;
                                        Matriz_Bytes_ARGB[Índice] = 0;
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Hue_red)
                        {
                            int Matiz = 0;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matiz = Program.HSL.Obtener_Matiz_0_a_11(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]);
                                    if (Matiz != 0)
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = 0;
                                        Matriz_Bytes_ARGB[Índice + 1] = 0;
                                        Matriz_Bytes_ARGB[Índice] = 0;
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Hue_orange)
                        {
                            int Matiz = 0;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matiz = Program.HSL.Obtener_Matiz_0_a_11(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]);
                                    if (Matiz != 1)
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = 0;
                                        Matriz_Bytes_ARGB[Índice + 1] = 0;
                                        Matriz_Bytes_ARGB[Índice] = 0;
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Hue_yellow)
                        {
                            int Matiz = 0;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matiz = Program.HSL.Obtener_Matiz_0_a_11(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]);
                                    if (Matiz != 2)
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = 0;
                                        Matriz_Bytes_ARGB[Índice + 1] = 0;
                                        Matriz_Bytes_ARGB[Índice] = 0;
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Hue_lime)
                        {
                            int Matiz = 0;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matiz = Program.HSL.Obtener_Matiz_0_a_11(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]);
                                    if (Matiz != 3)
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = 0;
                                        Matriz_Bytes_ARGB[Índice + 1] = 0;
                                        Matriz_Bytes_ARGB[Índice] = 0;
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Hue_green)
                        {
                            int Matiz = 0;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matiz = Program.HSL.Obtener_Matiz_0_a_11(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]);
                                    if (Matiz != 4)
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = 0;
                                        Matriz_Bytes_ARGB[Índice + 1] = 0;
                                        Matriz_Bytes_ARGB[Índice] = 0;
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Hue_turquoise)
                        {
                            int Matiz = 0;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matiz = Program.HSL.Obtener_Matiz_0_a_11(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]);
                                    if (Matiz != 5)
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = 0;
                                        Matriz_Bytes_ARGB[Índice + 1] = 0;
                                        Matriz_Bytes_ARGB[Índice] = 0;
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Hue_cyan)
                        {
                            int Matiz = 0;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matiz = Program.HSL.Obtener_Matiz_0_a_11(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]);
                                    if (Matiz != 6)
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = 0;
                                        Matriz_Bytes_ARGB[Índice + 1] = 0;
                                        Matriz_Bytes_ARGB[Índice] = 0;
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Hue_light_blue)
                        {
                            int Matiz = 0;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matiz = Program.HSL.Obtener_Matiz_0_a_11(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]);
                                    if (Matiz != 7)
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = 0;
                                        Matriz_Bytes_ARGB[Índice + 1] = 0;
                                        Matriz_Bytes_ARGB[Índice] = 0;
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Hue_blue)
                        {
                            int Matiz = 0;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matiz = Program.HSL.Obtener_Matiz_0_a_11(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]);
                                    if (Matiz != 8)
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = 0;
                                        Matriz_Bytes_ARGB[Índice + 1] = 0;
                                        Matriz_Bytes_ARGB[Índice] = 0;
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Hue_purple)
                        {
                            int Matiz = 0;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matiz = Program.HSL.Obtener_Matiz_0_a_11(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]);
                                    if (Matiz != 9)
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = 0;
                                        Matriz_Bytes_ARGB[Índice + 1] = 0;
                                        Matriz_Bytes_ARGB[Índice] = 0;
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Hue_magenta)
                        {
                            int Matiz = 0;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matiz = Program.HSL.Obtener_Matiz_0_a_11(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]);
                                    if (Matiz != 10)
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = 0;
                                        Matriz_Bytes_ARGB[Índice + 1] = 0;
                                        Matriz_Bytes_ARGB[Índice] = 0;
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Hue_pink)
                        {
                            int Matiz = 0;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matiz = Program.HSL.Obtener_Matiz_0_a_11(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]);
                                    if (Matiz != 11)
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = 0;
                                        Matriz_Bytes_ARGB[Índice + 1] = 0;
                                        Matriz_Bytes_ARGB[Índice] = 0;
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Swap_RGB_colors_to_RBG)
                        {
                            byte Valor_RGB;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Valor_RGB = Matriz_Bytes_ARGB[Índice + 1];
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                    Matriz_Bytes_ARGB[Índice] = Valor_RGB;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Swap_RGB_colors_to_GRB)
                        {
                            byte Valor_RGB;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Valor_RGB = Matriz_Bytes_ARGB[Índice + 2];
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice + 1];
                                    Matriz_Bytes_ARGB[Índice + 1] = Valor_RGB;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Swap_RGB_colors_to_GBR)
                        {
                            byte Valor_R, Valor_G, Valor_B;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Valor_R = Matriz_Bytes_ARGB[Índice + 2];
                                    Valor_G = Matriz_Bytes_ARGB[Índice + 1];
                                    Valor_B = Matriz_Bytes_ARGB[Índice];
                                    Matriz_Bytes_ARGB[Índice + 2] = Valor_G;
                                    Matriz_Bytes_ARGB[Índice + 1] = Valor_B;
                                    Matriz_Bytes_ARGB[Índice] = Valor_R;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Swap_RGB_colors_to_BRG)
                        {
                            byte Valor_R, Valor_G, Valor_B;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Valor_R = Matriz_Bytes_ARGB[Índice + 2];
                                    Valor_G = Matriz_Bytes_ARGB[Índice + 1];
                                    Valor_B = Matriz_Bytes_ARGB[Índice];
                                    Matriz_Bytes_ARGB[Índice + 2] = Valor_B;
                                    Matriz_Bytes_ARGB[Índice + 1] = Valor_R;
                                    Matriz_Bytes_ARGB[Índice] = Valor_G;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Swap_RGB_colors_to_BGR)
                        {
                            byte Valor_RGB;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Valor_RGB = Matriz_Bytes_ARGB[Índice + 2];
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];
                                    Matriz_Bytes_ARGB[Índice] = Valor_RGB;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Termography)
                        {
                            int Matiz;
                            Color Color_ARGB;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    //if (Matriz_Bytes[Índice] != Matriz_Bytes[Índice + 1] || Matriz_Bytes[Índice] != Matriz_Bytes[Índice + 2])
                                    {
                                        Matiz = Matriz_Bytes_ARGB[Índice + 2] + Matriz_Bytes_ARGB[Índice + 1] + Matriz_Bytes_ARGB[Índice];
                                        Color_ARGB = Program.Obtener_Color_Puro_1530(1275 - ((Matiz * 5) / 3));
                                        Matriz_Bytes_ARGB[Índice + 2] = Color_ARGB.R;
                                        Matriz_Bytes_ARGB[Índice + 1] = Color_ARGB.G;
                                        Matriz_Bytes_ARGB[Índice] = Color_ARGB.B;
                                    }
                                    /*else
                                    {
                                        Matriz_Bytes[Índice + 2] = 255;
                                        Matriz_Bytes[Índice + 1] = 255;
                                        Matriz_Bytes[Índice] = 255;
                                    }*/
                                }
                            }
                        }
                        else if (Filtro == Filtros.Variable_termography__3D_X____Ray___)
                        {
                            int Matiz;
                            Color Color_ARGB;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    //if (Matriz_Bytes[Índice] != Matriz_Bytes[Índice + 1] || Matriz_Bytes[Índice] != Matriz_Bytes[Índice + 2])
                                    {
                                        Matiz = (Matriz_Bytes_ARGB[Índice + 2] + Matriz_Bytes_ARGB[Índice + 1] + Matriz_Bytes_ARGB[Índice]) + Índice_Termografía;
                                        Matiz *= 2;
                                        if (Matiz >= 1530) Matiz -= 1530;
                                        Color_ARGB = Program.Obtener_Color_Puro_1530(1529 - Matiz);
                                        Matriz_Bytes_ARGB[Índice + 2] = Color_ARGB.R;
                                        Matriz_Bytes_ARGB[Índice + 1] = Color_ARGB.G;
                                        Matriz_Bytes_ARGB[Índice] = Color_ARGB.B;
                                    }
                                    /*else
                                    {
                                        Matriz_Bytes[Índice + 2] = 255;
                                        Matriz_Bytes[Índice + 1] = 255;
                                        Matriz_Bytes[Índice] = 255;
                                    }*/
                                }
                            }
                            Índice_Termografía++;
                            if (Índice_Termografía >= 765) Índice_Termografía = 0;
                        }
                        else if (Filtro == Filtros.Sine_horizontal_waves ||
                            Filtro == Filtros.Sine_vertical_waves ||
                            Filtro == Filtros.Sine_horizontal_and_vertical_waves)
                        {
                            int Altura_X = 16; // 24. // 32.
                            int Altura_Y = 16;
                            int Amplitud_X = 80; // 128.
                            int Amplitud_Y = 80;
                            int[] Matriz_Seno_X = new int[Amplitud_X];
                            if (Filtro != Filtros.Sine_horizontal_waves)
                            {
                                for (int Índice = 0, Subíndice = 0; Índice < Matriz_Seno_X.Length / 4; Índice++, Subíndice++) Matriz_Seno_X[Índice] = (int)Math.Round(Math.Sin(((90d * (Double)Subíndice) / (Double)((Matriz_Seno_X.Length / 4) - 1)) * (Math.PI / 180d)) * ((Double)Altura_X * 1d), MidpointRounding.AwayFromZero);
                                for (int Índice = Matriz_Seno_X.Length / 4, Subíndice = 0; Índice < Matriz_Seno_X.Length / 2; Índice++, Subíndice++) Matriz_Seno_X[Índice] = (int)Math.Round(Math.Sin((((90d * (Double)Subíndice) / (Double)((Matriz_Seno_X.Length / 4) - 1)) + 90d) * (Math.PI / 180d)) * (Double)Altura_X, MidpointRounding.AwayFromZero);
                                for (int Índice = Matriz_Seno_X.Length / 2, Subíndice = 0; Índice < Matriz_Seno_X.Length; Índice++, Subíndice++) Matriz_Seno_X[Índice] = -Matriz_Seno_X[Índice - (Matriz_Seno_X.Length / 2)];
                            }
                            int[] Matriz_Seno_Y = new int[Amplitud_Y];
                            if (Filtro != Filtros.Sine_vertical_waves)
                            {
                                for (int Índice = 0, Subíndice = 0; Índice < Matriz_Seno_Y.Length / 4; Índice++, Subíndice++) Matriz_Seno_Y[Índice] = (int)-Math.Round(Math.Sin(((90d * (Double)Subíndice) / (Double)((Matriz_Seno_Y.Length / 4) - 1)) * (Math.PI / 180d)) * ((Double)Altura_Y * 1d), MidpointRounding.AwayFromZero);
                                for (int Índice = Matriz_Seno_Y.Length / 4, Subíndice = 0; Índice < Matriz_Seno_Y.Length / 2; Índice++, Subíndice++) Matriz_Seno_Y[Índice] = (int)-Math.Round(Math.Sin((((90d * (Double)Subíndice) / (Double)((Matriz_Seno_Y.Length / 4) - 1)) + 90d) * (Math.PI / 180d)) * (Double)Altura_Y, MidpointRounding.AwayFromZero);
                                for (int Índice = Matriz_Seno_Y.Length / 2, Subíndice = 0; Índice < Matriz_Seno_Y.Length; Índice++, Subíndice++) Matriz_Seno_Y[Índice] = -Matriz_Seno_Y[Índice - (Matriz_Seno_Y.Length / 2)];
                            }
                            byte[] Matriz_Bytes_ARGB_Original = (byte[])Matriz_Bytes_ARGB.Clone();
                            for (int Índice_Y = 0, Índice = 0, Subíndice = 0, Onda_X = 0, Onda_Y = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Onda_X = Índice_X + Matriz_Seno_X[(Rectángulo_Pantalla.Height + Índice_Y) % Matriz_Seno_X.Length];
                                    Onda_Y = Índice_Y + Matriz_Seno_Y[(Rectángulo_Pantalla.Width + Índice_X) % Matriz_Seno_Y.Length];
                                    if (Onda_X < 0) Onda_X += Ancho;
                                    else if (Onda_X >= Ancho) Onda_X -= Ancho;
                                    if (Onda_Y < 0) Onda_Y += Alto;
                                    else if (Onda_Y >= Alto) Onda_Y -= Alto;
                                    Subíndice = (Bytes_Ancho * Onda_Y) + (Onda_X * Bytes_Aumento);
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB_Original[Subíndice + 2];
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB_Original[Subíndice + 1];
                                    Matriz_Bytes_ARGB[Índice] = Matriz_Bytes_ARGB_Original[Subíndice];
                                }
                            }
                            Matriz_Bytes_ARGB_Original = null;
                            Índice_Externo_Onda_X++;
                            Índice_Externo_Onda_Y++;
                            if (Índice_Externo_Onda_X >= Matriz_Seno_X.Length) Índice_Externo_Onda_X -= Matriz_Seno_X.Length;
                            if (Índice_Externo_Onda_Y >= Matriz_Seno_Y.Length) Índice_Externo_Onda_Y -= Matriz_Seno_Y.Length;
                        }
                        else if (Filtro == Filtros.Triangular_horizontal_waves ||
                            Filtro == Filtros.Triangular_vertical_waves ||
                            Filtro == Filtros.Triangular_horizontal_and_vertical_waves)
                        {
                            int Altura_X = 16; // 24. // 32.
                            int Altura_Y = 16;
                            int Amplitud_X = 80; // 128.
                            int Amplitud_Y = 80;
                            int[] Matriz_Triangular_X = new int[Amplitud_X];
                            if (Filtro != Filtros.Triangular_horizontal_waves)
                            {
                                for (int Índice = 0; Índice < Matriz_Triangular_X.Length / 4; Índice++) Matriz_Triangular_X[Índice] = (Altura_X * Índice) / (Matriz_Triangular_X.Length / 4);
                                for (int Índice = Matriz_Triangular_X.Length / 4; Índice < Matriz_Triangular_X.Length / 2; Índice++) Matriz_Triangular_X[Índice] = Matriz_Triangular_X[Índice] = Altura_X - Matriz_Triangular_X[Índice - (Matriz_Triangular_X.Length / 4)];
                                for (int Índice = Matriz_Triangular_X.Length / 2, Subíndice = 0; Índice < Matriz_Triangular_X.Length; Índice++, Subíndice++) Matriz_Triangular_X[Índice] = -Matriz_Triangular_X[Índice - (Matriz_Triangular_X.Length / 2)];
                            }
                            int[] Matriz_Triangular_Y = new int[Amplitud_Y];
                            if (Filtro != Filtros.Triangular_vertical_waves)
                            {
                                for (int Índice = 0; Índice < Matriz_Triangular_Y.Length / 4; Índice++) Matriz_Triangular_Y[Índice] = -((Altura_Y * Índice) / (Matriz_Triangular_Y.Length / 4));
                                for (int Índice = Matriz_Triangular_Y.Length / 4; Índice < Matriz_Triangular_Y.Length / 2; Índice++) Matriz_Triangular_Y[Índice] = Matriz_Triangular_Y[Índice] = -Altura_Y - Matriz_Triangular_Y[Índice - (Matriz_Triangular_Y.Length / 4)];
                                for (int Índice = Matriz_Triangular_Y.Length / 2, Subíndice = 0; Índice < Matriz_Triangular_Y.Length; Índice++, Subíndice++) Matriz_Triangular_Y[Índice] = -Matriz_Triangular_Y[Índice - (Matriz_Triangular_Y.Length / 2)];
                            }
                            byte[] Matriz_Bytes_ARGB_Original = (byte[])Matriz_Bytes_ARGB.Clone();
                            for (int Índice_Y = 0, Índice = 0, Subíndice = 0, Onda_X = 0, Onda_Y = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Onda_X = Índice_X + Matriz_Triangular_X[(Rectángulo_Pantalla.Height + Índice_Y) % Matriz_Triangular_X.Length];
                                    Onda_Y = Índice_Y + Matriz_Triangular_Y[(Rectángulo_Pantalla.Width + Índice_X) % Matriz_Triangular_Y.Length];
                                    if (Onda_X < 0) Onda_X += Ancho;
                                    else if (Onda_X >= Ancho) Onda_X -= Ancho;
                                    if (Onda_Y < 0) Onda_Y += Alto;
                                    else if (Onda_Y >= Alto) Onda_Y -= Alto;
                                    Subíndice = (Bytes_Ancho * Onda_Y) + (Onda_X * Bytes_Aumento);
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB_Original[Subíndice + 2];
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB_Original[Subíndice + 1];
                                    Matriz_Bytes_ARGB[Índice] = Matriz_Bytes_ARGB_Original[Subíndice];
                                }
                            }
                            Matriz_Bytes_ARGB_Original = null;
                            Índice_Externo_Onda_X++;
                            Índice_Externo_Onda_Y++;
                            if (Índice_Externo_Onda_X >= Matriz_Triangular_X.Length) Índice_Externo_Onda_X -= Matriz_Triangular_X.Length;
                            if (Índice_Externo_Onda_Y >= Matriz_Triangular_Y.Length) Índice_Externo_Onda_Y -= Matriz_Triangular_Y.Length;
                        }
                        else if (Filtro == Filtros.Bit_operation_and)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    new BitArray(new byte[] { Math.Min(Matriz_Bytes_ARGB[Índice + 2], Math.Min(Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice])) }).And(new BitArray(new byte[] { Math.Max(Matriz_Bytes_ARGB[Índice + 2], Math.Max(Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice])) })).CopyTo(Matriz_Bytes_ARGB, Índice);
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];
                                }
                            }
                        }
                        else if (Filtro == Filtros.Bit_operation_or)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    new BitArray(new byte[] { Math.Min(Matriz_Bytes_ARGB[Índice + 2], Math.Min(Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice])) }).Or(new BitArray(new byte[] { Math.Max(Matriz_Bytes_ARGB[Índice + 2], Math.Max(Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice])) })).CopyTo(Matriz_Bytes_ARGB, Índice);
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];
                                }
                            }
                        }
                        else if (Filtro == Filtros.Bit_operation_xor)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    new BitArray(new byte[] { Math.Min(Matriz_Bytes_ARGB[Índice + 2], Math.Min(Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice])) }).Xor(new BitArray(new byte[] { Math.Max(Matriz_Bytes_ARGB[Índice + 2], Math.Max(Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice])) })).CopyTo(Matriz_Bytes_ARGB, Índice);
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];
                                }
                            }
                        }
                        else if (Filtro == Filtros.Minimum_RGB)
                        {
                            byte Mínimo;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Mínimo = Math.Min(Matriz_Bytes_ARGB[Índice + 2], Math.Min(Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]));
                                    if (Matriz_Bytes_ARGB[Índice + 2] > Mínimo) Matriz_Bytes_ARGB[Índice + 2] = 0;
                                    if (Matriz_Bytes_ARGB[Índice + 1] > Mínimo) Matriz_Bytes_ARGB[Índice + 1] = 0;
                                    if (Matriz_Bytes_ARGB[Índice] > Mínimo) Matriz_Bytes_ARGB[Índice] = 0;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Median_RGB)
                        {
                            byte Mínimo;
                            byte Máximo;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Mínimo = Math.Min(Matriz_Bytes_ARGB[Índice + 2], Math.Min(Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]));
                                    Máximo = Math.Max(Matriz_Bytes_ARGB[Índice + 2], Math.Max(Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]));
                                    if (Matriz_Bytes_ARGB[Índice + 2] == Mínimo || Matriz_Bytes_ARGB[Índice + 2] == Máximo) Matriz_Bytes_ARGB[Índice + 2] = 0;
                                    if (Matriz_Bytes_ARGB[Índice + 1] == Mínimo || Matriz_Bytes_ARGB[Índice + 1] == Máximo) Matriz_Bytes_ARGB[Índice + 1] = 0;
                                    if (Matriz_Bytes_ARGB[Índice] == Mínimo || Matriz_Bytes_ARGB[Índice] == Máximo) Matriz_Bytes_ARGB[Índice] = 0;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Maximum_RGB)
                        {
                            byte Máximo;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Máximo = Math.Max(Matriz_Bytes_ARGB[Índice + 2], Math.Max(Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]));
                                    if (Matriz_Bytes_ARGB[Índice + 2] < Máximo) Matriz_Bytes_ARGB[Índice + 2] = 0;
                                    if (Matriz_Bytes_ARGB[Índice + 1] < Máximo) Matriz_Bytes_ARGB[Índice + 1] = 0;
                                    if (Matriz_Bytes_ARGB[Índice] < Máximo) Matriz_Bytes_ARGB[Índice] = 0;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Red)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 1] = 0;
                                    Matriz_Bytes_ARGB[Índice] = 0;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Yellow)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice] = 0;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Green)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 2] = 0;
                                    Matriz_Bytes_ARGB[Índice] = 0;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Cyan)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 2] = 0;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Blue)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 2] = 0;
                                    Matriz_Bytes_ARGB[Índice + 1] = 0;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Magenta)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 1] = 0;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Hue)
                        {
                            double Matiz, Saturación, Luminosidad;
                            int Valor;
                            Color Color_ARGB;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    if (Matriz_Bytes_ARGB[Índice] != Matriz_Bytes_ARGB[Índice + 1] || Matriz_Bytes_ARGB[Índice] != Matriz_Bytes_ARGB[Índice + 2]) // Not gray scale.
                                    {
                                        Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                        Valor = (int)(Matiz * 4.25d);
                                        if (Valor < 0) Valor = 0;
                                        else if (Valor > 1529) Valor = 1529;
                                        Color_ARGB = Program.Obtener_Color_Puro_1530(Valor);
                                        Matriz_Bytes_ARGB[Índice + 2] = Color_ARGB.R;
                                        Matriz_Bytes_ARGB[Índice + 1] = Color_ARGB.G;
                                        Matriz_Bytes_ARGB[Índice] = Color_ARGB.B;
                                    }
                                    else
                                    {
                                        Matriz_Bytes_ARGB[Índice + 2] = 255;
                                        Matriz_Bytes_ARGB[Índice + 1] = 255;
                                        Matriz_Bytes_ARGB[Índice] = 255;
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Saturation)
                        {
                            double Matiz, Saturación, Luminosidad;
                            int Valor;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                    Valor = (int)(Saturación * 2.55d);
                                    if (Valor < 0) Valor = 0;
                                    else if (Valor > 255) Valor = 255;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Valor;
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];
                                }
                            }
                        }
                        else if (Filtro == Filtros.Lightness)
                        {
                            double Matiz, Saturación, Luminosidad;
                            int Valor;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                    Valor = (int)(Luminosidad * 2.55d);
                                    if (Valor < 0) Valor = 0;
                                    else if (Valor > 255) Valor = 255;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Valor;
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];
                                }
                            }
                        }
                        else if (Filtro == Filtros.Negative_Minimum_RGB)
                        {
                            byte Mínimo;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Mínimo = Math.Min(Matriz_Bytes_ARGB[Índice + 2], Math.Min(Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]));
                                    if (Matriz_Bytes_ARGB[Índice + 2] <= Mínimo) Matriz_Bytes_ARGB[Índice + 2] = (byte)(255 - Matriz_Bytes_ARGB[Índice + 2]);
                                    if (Matriz_Bytes_ARGB[Índice + 1] <= Mínimo) Matriz_Bytes_ARGB[Índice + 1] = (byte)(255 - Matriz_Bytes_ARGB[Índice + 1]);
                                    if (Matriz_Bytes_ARGB[Índice] <= Mínimo) Matriz_Bytes_ARGB[Índice] = (byte)(255 - Matriz_Bytes_ARGB[Índice]);
                                }
                            }
                        }
                        else if (Filtro == Filtros.Negative_Median_RGB)
                        {
                            byte Mínimo;
                            byte Máximo;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Mínimo = Math.Min(Matriz_Bytes_ARGB[Índice + 2], Math.Min(Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]));
                                    Máximo = Math.Max(Matriz_Bytes_ARGB[Índice + 2], Math.Max(Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]));
                                    if (Matriz_Bytes_ARGB[Índice + 2] > Mínimo && Matriz_Bytes_ARGB[Índice + 2] < Máximo) Matriz_Bytes_ARGB[Índice + 2] = (byte)(255 - Matriz_Bytes_ARGB[Índice + 2]);
                                    if (Matriz_Bytes_ARGB[Índice + 1] > Mínimo && Matriz_Bytes_ARGB[Índice + 1] < Máximo) Matriz_Bytes_ARGB[Índice + 1] = (byte)(255 - Matriz_Bytes_ARGB[Índice + 1]);
                                    if (Matriz_Bytes_ARGB[Índice] > Mínimo && Matriz_Bytes_ARGB[Índice] < Máximo) Matriz_Bytes_ARGB[Índice] = (byte)(255 - Matriz_Bytes_ARGB[Índice]);
                                }
                            }
                        }
                        else if (Filtro == Filtros.Negative_Maximum_RGB)
                        {
                            byte Máximo;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Máximo = Math.Max(Matriz_Bytes_ARGB[Índice + 2], Math.Max(Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]));
                                    if (Matriz_Bytes_ARGB[Índice + 2] >= Máximo) Matriz_Bytes_ARGB[Índice + 2] = (byte)(255 - Matriz_Bytes_ARGB[Índice + 2]);
                                    if (Matriz_Bytes_ARGB[Índice + 1] >= Máximo) Matriz_Bytes_ARGB[Índice + 1] = (byte)(255 - Matriz_Bytes_ARGB[Índice + 1]);
                                    if (Matriz_Bytes_ARGB[Índice] >= Máximo) Matriz_Bytes_ARGB[Índice] = (byte)(255 - Matriz_Bytes_ARGB[Índice]);
                                }
                            }
                        }
                        else if (Filtro == Filtros.Negative_Red)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)(255 - Matriz_Bytes_ARGB[Índice + 2]);
                                }
                            }
                        }
                        else if (Filtro == Filtros.Negative_Yellow)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)(255 - Matriz_Bytes_ARGB[Índice + 2]);
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)(255 - Matriz_Bytes_ARGB[Índice + 1]);
                                }
                            }
                        }
                        else if (Filtro == Filtros.Negative_Green)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)(255 - Matriz_Bytes_ARGB[Índice + 1]);
                                }
                            }
                        }
                        else if (Filtro == Filtros.Negative_Cyan)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)(255 - Matriz_Bytes_ARGB[Índice + 1]);
                                    Matriz_Bytes_ARGB[Índice] = (byte)(255 - Matriz_Bytes_ARGB[Índice]);
                                }
                            }
                        }
                        else if (Filtro == Filtros.Negative_Blue)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice] = (byte)(255 - Matriz_Bytes_ARGB[Índice]);
                                }
                            }
                        }
                        else if (Filtro == Filtros.Negative_Magenta)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)(255 - Matriz_Bytes_ARGB[Índice + 2]);
                                    Matriz_Bytes_ARGB[Índice] = (byte)(255 - Matriz_Bytes_ARGB[Índice]);
                                }
                            }
                        }
                        else if (Filtro == Filtros.Negative_Hue_inverted)
                        {
                            byte Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    if (Matriz_Bytes_ARGB[Índice] != Matriz_Bytes_ARGB[Índice + 1] || Matriz_Bytes_ARGB[Índice] != Matriz_Bytes_ARGB[Índice + 2]) // Not gray scale.
                                    {
                                        Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                        Matiz = 360d - Matiz;
                                        if (Matiz < 0) Matiz += 360;
                                        else if (Matiz > 360) Matiz -= 360;
                                        Program.HSL.To_RGB(Matiz, Saturación, Luminosidad, out Rojo, out Verde, out Azul);
                                        Matriz_Bytes_ARGB[Índice + 2] = Rojo;
                                        Matriz_Bytes_ARGB[Índice + 1] = Verde;
                                        Matriz_Bytes_ARGB[Índice] = Azul;
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Negative_Hue)
                        {
                            byte Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    if (Matriz_Bytes_ARGB[Índice] != Matriz_Bytes_ARGB[Índice + 1] || Matriz_Bytes_ARGB[Índice] != Matriz_Bytes_ARGB[Índice + 2]) // Not gray scale.
                                    {
                                        Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                        Matiz += 180d;
                                        if (Matiz < 0) Matiz += 360;
                                        else if (Matiz > 360) Matiz -= 360;
                                        Program.HSL.To_RGB(Matiz, Saturación, Luminosidad, out Rojo, out Verde, out Azul);
                                        Matriz_Bytes_ARGB[Índice + 2] = Rojo;
                                        Matriz_Bytes_ARGB[Índice + 1] = Verde;
                                        Matriz_Bytes_ARGB[Índice] = Azul;
                                    }
                                }
                            }
                        }
                        else if (Filtro == Filtros.Negative_Saturation)
                        {
                            byte Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                    Saturación = 100d - Saturación;
                                    if (Saturación < 0) Saturación = 0;
                                    else if (Saturación > 100) Saturación = 100;
                                    Program.HSL.To_RGB(Matiz, Saturación, Luminosidad, out Rojo, out Verde, out Azul);
                                    Matriz_Bytes_ARGB[Índice + 2] = Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = Verde;
                                    Matriz_Bytes_ARGB[Índice] = Azul;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Negative_Lightness)
                        {
                            byte Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                    Luminosidad = 100d - Saturación;
                                    if (Luminosidad < 0) Luminosidad = 0;
                                    else if (Luminosidad > 100) Luminosidad = 100;
                                    Program.HSL.To_RGB(Matiz, Saturación, Luminosidad, out Rojo, out Verde, out Azul);
                                    Matriz_Bytes_ARGB[Índice + 2] = Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = Verde;
                                    Matriz_Bytes_ARGB[Índice] = Azul;
                                }
                            }
                        }
                        else if (Filtro == Filtros.Desaturate_Minimum_RGB)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice] = Math.Min(Matriz_Bytes_ARGB[Índice + 2], Math.Min(Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]));
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];
                                }
                            }
                        }
                        else if (Filtro == Filtros.Desaturate_Median_RGB)
                        {
                            byte[] Matriz_Bytes_Mediana = new byte[3];
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Array.Copy(Matriz_Bytes_ARGB, Índice, Matriz_Bytes_Mediana, 0, 3);
                                    Array.Sort(Matriz_Bytes_Mediana);
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_Mediana[1];
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_Mediana[1];
                                    Matriz_Bytes_ARGB[Índice] = Matriz_Bytes_Mediana[1];
                                    /*Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_Mediana[2];
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_Mediana[1];
                                    Matriz_Bytes_ARGB[Índice] = Matriz_Bytes_Mediana[0];*/
                                }
                            }
                        }
                        else if (Filtro == Filtros.Desaturate_Maximum_RGB)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice] = Math.Max(Matriz_Bytes_ARGB[Índice + 2], Math.Max(Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]));
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];
                                }
                            }
                        }
                        else if (Filtro == Filtros.Desaturate_Red)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice + 2];
                                    Matriz_Bytes_ARGB[Índice] = Matriz_Bytes_ARGB[Índice + 2];
                                }
                            }
                        }
                        else if (Filtro == Filtros.Desaturate_Yellow)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice] = (byte)((Matriz_Bytes_ARGB[Índice + 2] + Matriz_Bytes_ARGB[Índice + 1]) / 2);
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];
                                }
                            }
                        }
                        else if (Filtro == Filtros.Desaturate_Green)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice + 1];
                                    Matriz_Bytes_ARGB[Índice] = Matriz_Bytes_ARGB[Índice + 1];
                                }
                            }
                        }
                        else if (Filtro == Filtros.Desaturate_Cyan)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice] = (byte)((Matriz_Bytes_ARGB[Índice + 1] + Matriz_Bytes_ARGB[Índice]) / 2);
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];
                                }
                            }
                        }
                        else if (Filtro == Filtros.Desaturate_Blue)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                }
                            }
                        }
                        else if (Filtro == Filtros.Desaturate_Magenta)
                        {
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matriz_Bytes_ARGB[Índice] = (byte)((Matriz_Bytes_ARGB[Índice + 2] + Matriz_Bytes_ARGB[Índice]) / 2);
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];
                                }
                            }
                        }
                        else if (Filtro == Filtros.Desaturate_Hue)
                        {
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                    Matriz_Bytes_ARGB[Índice] = (byte)((Matiz * 17d) / 24d);
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];
                                }
                            }
                        }
                        else if (Filtro == Filtros.Desaturate_Saturation)
                        {
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                    Matriz_Bytes_ARGB[Índice] = (byte)((Saturación * 51d) / 20d);
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];
                                }
                            }
                        }
                        else if (Filtro == Filtros.Desaturate_Lightness)
                        {
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                    Matriz_Bytes_ARGB[Índice] = (byte)((Luminosidad * 51d) / 20d);
                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];
                                }
                            }
                        }
                        else if (Filtro == Filtros.HSL_to_RGB)
                        {
                            int Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                    Rojo = (int)((Matiz * 255d) / 360d);
                                    Verde = (int)((Saturación * 255d) / 100d);
                                    Azul = (int)((Luminosidad * 255d) / 100d);
                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                }
                            }
                        }
                        else if (Filtro == Filtros.HSL_to_RBG)
                        {
                            int Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                    Rojo = (int)((Matiz * 255d) / 360d);
                                    Azul = (int)((Saturación * 255d) / 100d);
                                    Verde = (int)((Luminosidad * 255d) / 100d);
                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                }
                            }
                        }
                        else if (Filtro == Filtros.HSL_to_GRB)
                        {
                            int Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                    Verde = (int)((Matiz * 255d) / 360d);
                                    Rojo = (int)((Saturación * 255d) / 100d);
                                    Azul = (int)((Luminosidad * 255d) / 100d);
                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                }
                            }
                        }
                        else if (Filtro == Filtros.HSL_to_GBR)
                        {
                            int Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                    Verde = (int)((Matiz * 255d) / 360d);
                                    Azul = (int)((Saturación * 255d) / 100d);
                                    Rojo = (int)((Luminosidad * 255d) / 100d);
                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                }
                            }
                        }
                        else if (Filtro == Filtros.HSL_to_BRG)
                        {
                            int Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                    Azul = (int)((Matiz * 255d) / 360d);
                                    Rojo = (int)((Saturación * 255d) / 100d);
                                    Verde = (int)((Luminosidad * 255d) / 100d);
                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                }
                            }
                        }
                        else if (Filtro == Filtros.HSL_to_BGR)
                        {
                            int Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Program.HSL.From_RGB(Matriz_Bytes_ARGB[Índice + 2], Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice], out Matiz, out Saturación, out Luminosidad);
                                    Azul = (int)((Matiz * 255d) / 360d);
                                    Verde = (int)((Saturación * 255d) / 100d);
                                    Rojo = (int)((Luminosidad * 255d) / 100d);
                                    if (Rojo < 0) Rojo = 0;
                                    else if (Rojo > 255) Rojo = 255;
                                    if (Verde < 0) Verde = 0;
                                    else if (Verde > 255) Verde = 255;
                                    if (Azul < 0) Azul = 0;
                                    else if (Azul > 255) Azul = 255;
                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                }
                            }
                        }
                        else if (Filtro == Filtros.RGB_to_HSL)
                        {
                            byte Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matiz = (int)((Matriz_Bytes_ARGB[Índice + 2] * 360d) / 255d);
                                    Saturación = (int)((Matriz_Bytes_ARGB[Índice + 1] * 100d) / 255d);
                                    Luminosidad = (int)((Matriz_Bytes_ARGB[Índice + 0] * 100d) / 255d);
                                    if (Matiz < 0d) Matiz = 0d;
                                    else if (Matiz >= 360d) Matiz = 0d;
                                    if (Saturación < 0d) Saturación = 0d;
                                    else if (Saturación > 100d) Saturación = 100d;
                                    if (Luminosidad < 0d) Luminosidad = 0d;
                                    else if (Luminosidad > 100d) Luminosidad = 100d;
                                    Program.HSL.To_RGB(Matiz, Saturación, Luminosidad, out Rojo, out Verde, out Azul);
                                    Matriz_Bytes_ARGB[Índice + 2] = Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = Verde;
                                    Matriz_Bytes_ARGB[Índice] = Azul;
                                }
                            }
                        }
                        else if (Filtro == Filtros.RGB_to_HLS)
                        {
                            byte Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Matiz = (int)((Matriz_Bytes_ARGB[Índice + 2] * 360d) / 255d);
                                    Luminosidad = (int)((Matriz_Bytes_ARGB[Índice + 1] * 100d) / 255d);
                                    Saturación = (int)((Matriz_Bytes_ARGB[Índice] * 100d) / 255d);
                                    if (Matiz < 0d) Matiz = 0d;
                                    else if (Matiz >= 360d) Matiz = 0d;
                                    if (Saturación < 0d) Saturación = 0d;
                                    else if (Saturación > 100d) Saturación = 100d;
                                    if (Luminosidad < 0d) Luminosidad = 0d;
                                    else if (Luminosidad > 100d) Luminosidad = 100d;
                                    Program.HSL.To_RGB(Matiz, Saturación, Luminosidad, out Rojo, out Verde, out Azul);
                                    Matriz_Bytes_ARGB[Índice + 2] = Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = Verde;
                                    Matriz_Bytes_ARGB[Índice] = Azul;
                                }
                            }
                        }
                        else if (Filtro == Filtros.RGB_to_SHL)
                        {
                            byte Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Saturación = (int)((Matriz_Bytes_ARGB[Índice + 2] * 100d) / 255d);
                                    Matiz = (int)((Matriz_Bytes_ARGB[Índice + 1] * 360d) / 255d);
                                    Luminosidad = (int)((Matriz_Bytes_ARGB[Índice] * 100d) / 255d);
                                    if (Matiz < 0d) Matiz = 0d;
                                    else if (Matiz >= 360d) Matiz = 0d;
                                    if (Saturación < 0d) Saturación = 0d;
                                    else if (Saturación > 100d) Saturación = 100d;
                                    if (Luminosidad < 0d) Luminosidad = 0d;
                                    else if (Luminosidad > 100d) Luminosidad = 100d;
                                    Program.HSL.To_RGB(Matiz, Saturación, Luminosidad, out Rojo, out Verde, out Azul);
                                    Matriz_Bytes_ARGB[Índice + 2] = Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = Verde;
                                    Matriz_Bytes_ARGB[Índice] = Azul;
                                }
                            }
                        }
                        else if (Filtro == Filtros.RGB_to_SLH)
                        {
                            byte Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Saturación = (int)((Matriz_Bytes_ARGB[Índice + 2] * 100d) / 255d);
                                    Luminosidad = (int)((Matriz_Bytes_ARGB[Índice + 1] * 100d) / 255d);
                                    Matiz = (int)((Matriz_Bytes_ARGB[Índice] * 360d) / 255d);
                                    if (Matiz < 0d) Matiz = 0d;
                                    else if (Matiz >= 360d) Matiz = 0d;
                                    if (Saturación < 0d) Saturación = 0d;
                                    else if (Saturación > 100d) Saturación = 100d;
                                    if (Luminosidad < 0d) Luminosidad = 0d;
                                    else if (Luminosidad > 100d) Luminosidad = 100d;
                                    Program.HSL.To_RGB(Matiz, Saturación, Luminosidad, out Rojo, out Verde, out Azul);
                                    Matriz_Bytes_ARGB[Índice + 2] = Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = Verde;
                                    Matriz_Bytes_ARGB[Índice] = Azul;
                                }
                            }
                        }
                        else if (Filtro == Filtros.RGB_to_LHS)
                        {
                            byte Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Luminosidad = (int)((Matriz_Bytes_ARGB[Índice + 2] * 100d) / 255d);
                                    Matiz = (int)((Matriz_Bytes_ARGB[Índice + 1] * 360d) / 255d);
                                    Saturación = (int)((Matriz_Bytes_ARGB[Índice] * 100d) / 255d);
                                    if (Matiz < 0d) Matiz = 0d;
                                    else if (Matiz >= 360d) Matiz = 0d;
                                    if (Saturación < 0d) Saturación = 0d;
                                    else if (Saturación > 100d) Saturación = 100d;
                                    if (Luminosidad < 0d) Luminosidad = 0d;
                                    else if (Luminosidad > 100d) Luminosidad = 100d;
                                    Program.HSL.To_RGB(Matiz, Saturación, Luminosidad, out Rojo, out Verde, out Azul);
                                    Matriz_Bytes_ARGB[Índice + 2] = Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = Verde;
                                    Matriz_Bytes_ARGB[Índice] = Azul;
                                }
                            }
                        }
                        else if (Filtro == Filtros.RGB_to_LSH)
                        {
                            byte Rojo, Verde, Azul;
                            double Matiz, Saturación, Luminosidad;
                            for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                            {
                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                                {
                                    Luminosidad = (int)((Matriz_Bytes_ARGB[Índice + 2] * 100d) / 255d);
                                    Saturación = (int)((Matriz_Bytes_ARGB[Índice + 1] * 100d) / 255d);
                                    Matiz = (int)((Matriz_Bytes_ARGB[Índice] * 360d) / 255d);
                                    if (Matiz < 0d) Matiz = 0d;
                                    else if (Matiz >= 360d) Matiz = 0d;
                                    if (Saturación < 0d) Saturación = 0d;
                                    else if (Saturación > 100d) Saturación = 100d;
                                    if (Luminosidad < 0d) Luminosidad = 0d;
                                    else if (Luminosidad > 100d) Luminosidad = 100d;
                                    Program.HSL.To_RGB(Matiz, Saturación, Luminosidad, out Rojo, out Verde, out Azul);
                                    Matriz_Bytes_ARGB[Índice + 2] = Rojo;
                                    Matriz_Bytes_ARGB[Índice + 1] = Verde;
                                    Matriz_Bytes_ARGB[Índice] = Azul;
                                }
                            }
                        }
                        if (!Cancelar_Marshal_Copy)
                        {
                            Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                            Marshal.Copy(Matriz_Bytes_ARGB, 0, Bitmap_Data.Scan0, Matriz_Bytes_ARGB.Length);
                            Imagen.UnlockBits(Bitmap_Data);
                            Bitmap_Data = null;
                        }
                        Matriz_Bytes_ARGB = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return Imagen;
        }

        private void Menú_Contextual_Negativo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Negativo = Menú_Contextual_Negativo.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Negativo_Posterior_Click(object sender, EventArgs e)
        {
            try
            {
                Variable_Negativo_Posterior = Menú_Contextual_Negativo_Posterior.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Escala_Grises_Anterior_Click(object sender, EventArgs e)
        {
            try
            {
                Variable_Desaturado_Anterior = Menú_Contextual_Desaturado_Anterior.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Desaturado_Posterior_Click(object sender, EventArgs e)
        {
            try
            {
                Variable_Desaturado_Posterior = Menú_Contextual_Desaturado_Posterior.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Zoom_Suave_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Zoom_Suave = Menú_Contextual_Zoom_Suave.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Seguir_Cursor_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Seguir_Cursor = Menú_Contextual_Seguir_Cursor.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
