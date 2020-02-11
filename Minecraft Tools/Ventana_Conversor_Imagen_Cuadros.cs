using Minecraft_Tools.Properties;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Conversor_Imagen_Cuadros : Form
    {
        public Ventana_Conversor_Imagen_Cuadros()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Paintings Images Converter by Jupisoft for " + Program.Texto_Usuario;
        internal bool Variable_Siempre_Visible = false;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;
        internal static Stopwatch FPS_Cronómetro = Stopwatch.StartNew();
        internal long FPS_Segundo_Anterior = 0L;
        internal long FPS_Temporal = 0L;
        internal long FPS_Real = 0L;
        internal bool Ocupado = false;

        internal static Dictionary<string, Rectangle> Diccionario_Cuadros_Rectángulos = null;
        internal static int Variable_Modo = 0;

        private void Ventana_Conversor_Imagen_Cuadros_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título + " - [Drag and drop any file or folder or set the input path to get started...]";
                this.WindowState = FormWindowState.Maximized;
                if (Diccionario_Cuadros_Rectángulos == null)
                {
                    Diccionario_Cuadros_Rectángulos = new Dictionary<string, Rectangle>();

                    Diccionario_Cuadros_Rectángulos.Add("kebab", new Rectangle(0, 0, 1, 1));
                    Diccionario_Cuadros_Rectángulos.Add("aztec", new Rectangle(1, 0, 1, 1));
                    Diccionario_Cuadros_Rectángulos.Add("alban", new Rectangle(2, 0, 1, 1));
                    Diccionario_Cuadros_Rectángulos.Add("aztec2", new Rectangle(3, 0, 1, 1));
                    Diccionario_Cuadros_Rectángulos.Add("bomb", new Rectangle(4, 0, 1, 1));
                    Diccionario_Cuadros_Rectángulos.Add("plant", new Rectangle(5, 0, 1, 1));
                    Diccionario_Cuadros_Rectángulos.Add("wasteland", new Rectangle(6, 0, 1, 1));

                    Diccionario_Cuadros_Rectángulos.Add("pool", new Rectangle(0, 2, 2, 1));
                    Diccionario_Cuadros_Rectángulos.Add("courbet", new Rectangle(2, 2, 2, 1));
                    Diccionario_Cuadros_Rectángulos.Add("sea", new Rectangle(4, 2, 2, 1));
                    Diccionario_Cuadros_Rectángulos.Add("sunset", new Rectangle(6, 2, 2, 1));
                    Diccionario_Cuadros_Rectángulos.Add("creebet", new Rectangle(8, 2, 2, 1));

                    Diccionario_Cuadros_Rectángulos.Add("wanderer", new Rectangle(0, 4, 1, 2));
                    Diccionario_Cuadros_Rectángulos.Add("graham", new Rectangle(1, 4, 1, 2));

                    Diccionario_Cuadros_Rectángulos.Add("match", new Rectangle(0, 8, 2, 2));
                    Diccionario_Cuadros_Rectángulos.Add("bust", new Rectangle(2, 8, 2, 2));
                    Diccionario_Cuadros_Rectángulos.Add("stage", new Rectangle(4, 8, 2, 2));
                    Diccionario_Cuadros_Rectángulos.Add("void", new Rectangle(6, 8, 2, 2));
                    Diccionario_Cuadros_Rectángulos.Add("skull_and_roses", new Rectangle(8, 8, 2, 2));
                    Diccionario_Cuadros_Rectángulos.Add("wither", new Rectangle(10, 8, 2, 2));

                    Diccionario_Cuadros_Rectángulos.Add("fighters", new Rectangle(0, 6, 4, 2));

                    Diccionario_Cuadros_Rectángulos.Add("skeleton", new Rectangle(12, 4, 4, 3));
                    Diccionario_Cuadros_Rectángulos.Add("donkey_kong", new Rectangle(12, 7, 4, 3));

                    Diccionario_Cuadros_Rectángulos.Add("pointer", new Rectangle(0, 12, 4, 4));
                    Diccionario_Cuadros_Rectángulos.Add("pigscene", new Rectangle(4, 12, 4, 4));
                    Diccionario_Cuadros_Rectángulos.Add("burning_skull", new Rectangle(8, 12, 4, 4));

                    Diccionario_Cuadros_Rectángulos.Add("back", new Rectangle(12, 0, 1, 1)); // MC 1.14+.
                }
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
                ComboBox_Modo.SelectedIndex = Variable_Modo;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Imagen_Cuadros_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Imagen_Cuadros_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Imagen_Cuadros_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Imagen_Cuadros_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Conversor_Imagen_Cuadros_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
                {
                    string[] Matriz_Rutas = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                    if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                    {
                        foreach (string Ruta in Matriz_Rutas)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(Ruta) && (File.Exists(Ruta) || Directory.Exists(Ruta)))
                                {
                                    TextBox_Ruta_Entrada.Text = Ruta;
                                    break;
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
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Conversor_Imagen_Cuadros_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Imagen_Cuadros_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Alt && !e.Control && !e.Shift)
                {
                    if (e.KeyCode == Keys.Escape)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        this.Close();
                    }
                    else if (e.KeyCode == Keys.Enter)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Modo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Modo.SelectedIndex > -1)
                {
                    Variable_Modo = ComboBox_Modo.SelectedIndex;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Convertir_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string Ruta_Entrada = TextBox_Ruta_Entrada.Text;
                string Ruta_Salida = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + Program.Obtener_Nombre_Temporal() + " Paintings";
                if (!string.IsNullOrEmpty(Ruta_Entrada) && !string.IsNullOrEmpty(Ruta_Salida) && (File.Exists(Ruta_Entrada) || Directory.Exists(Ruta_Entrada)) && (!File.Exists(Ruta_Salida) && !Directory.Exists(Ruta_Salida)))
                {
                    if (Variable_Modo == 0)
                    {
                        string Ruta_Base = File.Exists(Ruta_Entrada) ? Ruta_Entrada : Ruta_Entrada + "\\paintings_kristoffer_zetterstrand.png";
                        if (File.Exists(Ruta_Base))
                        {
                            Bitmap Imagen_Original = Program.Cargar_Imagen_Ruta(Ruta_Base, CheckState.Checked);
                            Establecer_Cuadros(Imagen_Original);
                            if (Imagen_Original != null)
                            {
                                TextBox_Ruta_Salida.Text = Ruta_Salida;
                                Program.Crear_Carpetas(Ruta_Salida);
                                int Ancho = Imagen_Original.Width;
                                int Alto = Imagen_Original.Height;
                                int Ancho_Cuadro = Ancho / 16;
                                int Alto_Cuadro = Alto / 16;
                                foreach (KeyValuePair<string, Rectangle> Entrada in Diccionario_Cuadros_Rectángulos)
                                {
                                    try
                                    {
                                        Bitmap Imagen = Imagen_Original.Clone(new Rectangle(Entrada.Value.X * Ancho_Cuadro, Entrada.Value.Y * Alto_Cuadro, Entrada.Value.Width * Ancho_Cuadro, Entrada.Value.Height * Alto_Cuadro), Imagen_Original.PixelFormat);
                                        Imagen.Save(Ruta_Salida + "\\" + Entrada.Key + ".png", ImageFormat.Png);
                                        Imagen.Dispose();
                                        Imagen = null;
                                    }
                                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                }
                                SystemSounds.Asterisk.Play();
                                Program.Ejecutar_Ruta(Ruta_Salida, ProcessWindowStyle.Maximized);
                            }
                        }
                    }
                    else
                    {
                        string Ruta_Base = Directory.Exists(Ruta_Entrada) ? Ruta_Entrada : Path.GetDirectoryName(Ruta_Entrada);
                        if (Directory.Exists(Ruta_Base))
                        {
                            string[] Matriz_Rutas = Directory.GetFiles(Ruta_Base, "*.png", SearchOption.TopDirectoryOnly);
                            if (Matriz_Rutas != null && Matriz_Rutas.Length >= 26)
                            {
                                Dictionary<string, Bitmap> Diccionario_Imágenes_Cuadros = new Dictionary<string, Bitmap>();
                                int Ancho_Cuadro = -1;
                                int Alto_Cuadro = -1;
                                foreach (string Ruta in Matriz_Rutas)
                                {
                                    try
                                    {
                                        Bitmap Imagen_Original = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Checked);
                                        if (Imagen_Original != null)
                                        {
                                            int Ancho = Imagen_Original.Width;
                                            int Alto = Imagen_Original.Height;
                                            string Nombre = Path.GetFileNameWithoutExtension(Ruta).ToLowerInvariant();
                                            if (Diccionario_Cuadros_Rectángulos.ContainsKey(Nombre))
                                            {
                                                if (Ancho_Cuadro < 0 || Alto_Cuadro < 0) // Get at the first match the width and height for all the paintings (hopefully will be constant).
                                                {
                                                    Ancho_Cuadro = Ancho / Diccionario_Cuadros_Rectángulos[Nombre].Width;
                                                    Alto_Cuadro = Alto / Diccionario_Cuadros_Rectángulos[Nombre].Height;
                                                }
                                                if (!Diccionario_Imágenes_Cuadros.ContainsKey(Nombre))
                                                {
                                                    Diccionario_Imágenes_Cuadros.Add(Nombre, Imagen_Original);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                }
                                if (Diccionario_Imágenes_Cuadros.Count > 0)
                                {
                                    TextBox_Ruta_Salida.Text = Ruta_Salida;
                                    Program.Crear_Carpetas(Ruta_Salida);
                                    if (!Diccionario_Imágenes_Cuadros.ContainsKey("back")) // Always have the back of the paintings.
                                    {
                                        Diccionario_Imágenes_Cuadros.Add("back", Resources.paintings_kristoffer_zetterstrand.Clone(new Rectangle(192, 0, 16, 16), Resources.paintings_kristoffer_zetterstrand.PixelFormat));
                                    }

                                    Bitmap Imagen = new Bitmap(Ancho_Cuadro * 16, Alto_Cuadro * 16, PixelFormat.Format32bppArgb);
                                    Graphics Pintar = Graphics.FromImage(Imagen);
                                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                    Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                    Pintar.SmoothingMode = SmoothingMode.HighQuality;
                                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;

                                    // Emulate the background from the old paintings image but with support for other sizes.
                                    SolidBrush Pincel_Fondo = new SolidBrush(Color.FromArgb(255, 214, 127, 255));
                                    SolidBrush Pincel_Línea = new SolidBrush(Color.FromArgb(255, 107, 63, 127));
                                    for (int Índice_Y = 0; Índice_Y < 16; Índice_Y++)
                                    {
                                        for (int Índice_X = 0; Índice_X < 16; Índice_X++)
                                        {
                                            Pintar.FillRectangle(Pincel_Línea, Índice_X * Ancho_Cuadro, Índice_Y * Alto_Cuadro, Ancho_Cuadro, Alto_Cuadro); // Outline.
                                            Pintar.FillRectangle(Pincel_Fondo, (Índice_X * Ancho_Cuadro) + 1, (Índice_Y * Alto_Cuadro) + 1, Ancho_Cuadro - 2, Alto_Cuadro - 2); // Inline.
                                        }
                                    }
                                    Pincel_Fondo.Dispose();
                                    Pincel_Fondo = null;
                                    Pincel_Línea.Dispose();
                                    Pincel_Línea = null;

                                    // Now draw over it the actual paintings.
                                    //Pintar.CompositingMode = CompositingMode.SourceOver; // Mix with the background?
                                    foreach (KeyValuePair<string, Bitmap> Entrada in Diccionario_Imágenes_Cuadros)
                                    {
                                        try
                                        {
                                            if (string.Compare(Entrada.Key, "back") != 0)
                                            {
                                                Pintar.DrawImage(Entrada.Value, new Rectangle(Diccionario_Cuadros_Rectángulos[Entrada.Key].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos[Entrada.Key].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos[Entrada.Key].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos[Entrada.Key].Height * Alto_Cuadro), new Rectangle(0, 0, Entrada.Value.Width, Entrada.Value.Height), GraphicsUnit.Pixel);
                                            }
                                        }
                                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                    }

                                    // Finally draw the back image repeated several times.
                                    TextureBrush Pincel = new TextureBrush(Diccionario_Imágenes_Cuadros["back"], WrapMode.Tile);
                                    Pintar.FillRectangle(Pincel, new Rectangle(Diccionario_Cuadros_Rectángulos["back"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["back"].Y * Alto_Cuadro, (Diccionario_Cuadros_Rectángulos["back"].Width * 4) * Ancho_Cuadro, (Diccionario_Cuadros_Rectángulos["back"].Height * 4) * Alto_Cuadro));
                                    Pincel.Dispose();
                                    Pincel = null;
                                    Pintar.Dispose();
                                    Pintar = null;
                                    Establecer_Cuadros(Imagen, Diccionario_Imágenes_Cuadros);
                                    Imagen.Save(Ruta_Salida + "\\paintings_kristoffer_zetterstrand.png", ImageFormat.Png);
                                    SystemSounds.Asterisk.Play();
                                    Program.Ejecutar_Ruta(Ruta_Salida, ProcessWindowStyle.Maximized);
                                    Diccionario_Imágenes_Cuadros = null;
                                }
                                else
                                {
                                    Establecer_Cuadros(null, null);
                                    SystemSounds.Beep.Play();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Picture_Cuadros_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Right)
                {
                    PictureBox Picture = sender as PictureBox;
                    if (Picture != null && !string.IsNullOrEmpty(Picture.Name) && Picture.Name.Length > "Picture_Cuadro_".Length)
                    {
                        string Nombre = Picture.Name.Substring("Picture_Cuadro_".Length).ToLowerInvariant();
                        Clipboard.SetText(Nombre);
                        SystemSounds.Asterisk.Play();
                        this.Text = Nombre;
                        Nombre = null;
                    }
                    else SystemSounds.Beep.Play();
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
                Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.Main_window;
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
                Program.Crear_Carpetas(Program.Ruta_Minecraft);
                Program.Ejecutar_Ruta(Program.Ruta_Guardado_Minecraft, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                /*if (Picture.Image != null)
                {
                    Clipboard.SetImage(Picture.Image);
                    SystemSounds.Asterisk.Play();
                }*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                /*if (Picture.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Minecraft);
                    Picture.Image.Save(Program.Ruta_Minecraft + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + ".png", ImageFormat.Png);
                    SystemSounds.Asterisk.Play();
                }*/
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

        /// <summary>
        /// Function designed to show a real time preview of the converted paintings.
        /// </summary>
        internal void Establecer_Cuadros(Bitmap Imagen, Dictionary<string, Bitmap> Diccionario_Imágenes_Cuadros)
        {
            try
            {
                if (Imagen != null && Diccionario_Imágenes_Cuadros != null && Diccionario_Imágenes_Cuadros.Count > 0)
                {
                    Picture_Cuadro_Kebab.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("kebab") ? Diccionario_Imágenes_Cuadros["kebab"] : Resources.paintings_kebab;
                    Picture_Cuadro_Aztec.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("aztec") ? Diccionario_Imágenes_Cuadros["aztec"] : Resources.paintings_aztec;
                    Picture_Cuadro_Alban.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("alban") ? Diccionario_Imágenes_Cuadros["alban"] : Resources.paintings_alban;
                    Picture_Cuadro_Aztec2.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("aztec2") ? Diccionario_Imágenes_Cuadros["aztec2"] : Resources.paintings_aztec2;
                    Picture_Cuadro_Bomb.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("bomb") ? Diccionario_Imágenes_Cuadros["bomb"] : Resources.paintings_bomb;
                    Picture_Cuadro_Plant.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("plant") ? Diccionario_Imágenes_Cuadros["plant"] : Resources.paintings_plant;
                    Picture_Cuadro_Wasteland.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("wasteland") ? Diccionario_Imágenes_Cuadros["wasteland"] : Resources.paintings_wasteland;
                    Picture_Cuadro_Back.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("back") ? Diccionario_Imágenes_Cuadros["back"] : Resources.paintings_back;

                    Picture_Cuadro_Pool.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("pool") ? Diccionario_Imágenes_Cuadros["pool"] : Resources.paintings_pool;
                    Picture_Cuadro_Courbet.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("courbet") ? Diccionario_Imágenes_Cuadros["courbet"] : Resources.paintings_courbet;
                    Picture_Cuadro_Sea.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("sea") ? Diccionario_Imágenes_Cuadros["sea"] : Resources.paintings_sea;
                    Picture_Cuadro_Sunset.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("sunset") ? Diccionario_Imágenes_Cuadros["sunset"] : Resources.paintings_sunset;
                    Picture_Cuadro_Creebet.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("creebet") ? Diccionario_Imágenes_Cuadros["creebet"] : Resources.paintings_creebet;

                    Picture_Cuadro_Wanderer.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("wanderer") ? Diccionario_Imágenes_Cuadros["wanderer"] : Resources.paintings_wanderer;
                    Picture_Cuadro_Graham.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("graham") ? Diccionario_Imágenes_Cuadros["graham"] : Resources.paintings_graham;

                    Picture_Cuadro_Match.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("match") ? Diccionario_Imágenes_Cuadros["match"] : Resources.paintings_match;
                    Picture_Cuadro_Bust.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("bust") ? Diccionario_Imágenes_Cuadros["bust"] : Resources.paintings_bust;
                    Picture_Cuadro_Stage.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("stage") ? Diccionario_Imágenes_Cuadros["stage"] : Resources.paintings_stage;
                    Picture_Cuadro_Void.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("void") ? Diccionario_Imágenes_Cuadros["void"] : Resources.paintings_void;
                    Picture_Cuadro_Skull_And_Roses.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("skull_and_roses") ? Diccionario_Imágenes_Cuadros["skull_and_roses"] : Resources.paintings_skull_and_roses;
                    Picture_Cuadro_Wither.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("wither") ? Diccionario_Imágenes_Cuadros["wither"] : Resources.paintings_wither;

                    Picture_Cuadro_Fighters.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("fighters") ? Diccionario_Imágenes_Cuadros["fighters"] : Resources.paintings_fighters;

                    Picture_Cuadro_Skeleton.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("skeleton") ? Diccionario_Imágenes_Cuadros["skeleton"] : Resources.paintings_skeleton;
                    Picture_Cuadro_Donkey_Kong.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("donkey_kong") ? Diccionario_Imágenes_Cuadros["donkey_kong"] : Resources.paintings_donkey_kong;

                    Picture_Cuadro_Pointer.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("pointer") ? Diccionario_Imágenes_Cuadros["pointer"] : Resources.paintings_pointer;
                    Picture_Cuadro_Pigscene.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("pigscene") ? Diccionario_Imágenes_Cuadros["pigscene"] : Resources.paintings_pigscene;
                    Picture_Cuadro_Burning_Skull.BackgroundImage = Diccionario_Imágenes_Cuadros.ContainsKey("burning_skull") ? Diccionario_Imágenes_Cuadros["burning_skull"] : Resources.paintings_burning_skull;

                    Picture_Cuadro_Paintings_Kristoffer_Zetterstrand.BackgroundImage = Imagen;
                }
                else
                {
                    Picture_Cuadro_Kebab.BackgroundImage = Resources.paintings_kebab;
                    Picture_Cuadro_Aztec.BackgroundImage = Resources.paintings_aztec;
                    Picture_Cuadro_Alban.BackgroundImage = Resources.paintings_alban;
                    Picture_Cuadro_Aztec2.BackgroundImage = Resources.paintings_aztec2;
                    Picture_Cuadro_Bomb.BackgroundImage = Resources.paintings_bomb;
                    Picture_Cuadro_Plant.BackgroundImage = Resources.paintings_plant;
                    Picture_Cuadro_Wasteland.BackgroundImage = Resources.paintings_wasteland;
                    Picture_Cuadro_Back.BackgroundImage = Resources.paintings_back;

                    Picture_Cuadro_Pool.BackgroundImage = Resources.paintings_pool;
                    Picture_Cuadro_Courbet.BackgroundImage = Resources.paintings_courbet;
                    Picture_Cuadro_Sea.BackgroundImage = Resources.paintings_sea;
                    Picture_Cuadro_Sunset.BackgroundImage = Resources.paintings_sunset;
                    Picture_Cuadro_Creebet.BackgroundImage = Resources.paintings_creebet;

                    Picture_Cuadro_Wanderer.BackgroundImage = Resources.paintings_wanderer;
                    Picture_Cuadro_Graham.BackgroundImage = Resources.paintings_graham;

                    Picture_Cuadro_Match.BackgroundImage = Resources.paintings_match;
                    Picture_Cuadro_Bust.BackgroundImage = Resources.paintings_bust;
                    Picture_Cuadro_Stage.BackgroundImage = Resources.paintings_stage;
                    Picture_Cuadro_Void.BackgroundImage = Resources.paintings_void;
                    Picture_Cuadro_Skull_And_Roses.BackgroundImage = Resources.paintings_skull_and_roses;
                    Picture_Cuadro_Wither.BackgroundImage = Resources.paintings_wither;

                    Picture_Cuadro_Fighters.BackgroundImage = Resources.paintings_fighters;

                    Picture_Cuadro_Skeleton.BackgroundImage = Resources.paintings_skeleton;
                    Picture_Cuadro_Donkey_Kong.BackgroundImage = Resources.paintings_donkey_kong;

                    Picture_Cuadro_Pointer.BackgroundImage = Resources.paintings_pointer;
                    Picture_Cuadro_Pigscene.BackgroundImage = Resources.paintings_pigscene;
                    Picture_Cuadro_Burning_Skull.BackgroundImage = Resources.paintings_burning_skull;

                    Picture_Cuadro_Paintings_Kristoffer_Zetterstrand.BackgroundImage = Resources.paintings_kristoffer_zetterstrand;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Function designed to show a real time preview of the converted paintings.
        /// </summary>
        internal void Establecer_Cuadros(Bitmap Imagen)
        {
            try
            {
                if (Imagen == null) Imagen = Resources.paintings_kristoffer_zetterstrand;
                int Ancho_Cuadro = Imagen.Width / 16;
                int Alto_Cuadro = Imagen.Height / 16;

                Picture_Cuadro_Kebab.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["kebab"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["kebab"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["kebab"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["kebab"].Height * Alto_Cuadro), Imagen.PixelFormat);
                Picture_Cuadro_Aztec.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["aztec"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["aztec"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["aztec"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["aztec"].Height * Alto_Cuadro), Imagen.PixelFormat);
                Picture_Cuadro_Alban.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["alban"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["alban"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["alban"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["alban"].Height * Alto_Cuadro), Imagen.PixelFormat);
                Picture_Cuadro_Aztec2.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["aztec2"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["aztec2"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["aztec2"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["aztec2"].Height * Alto_Cuadro), Imagen.PixelFormat);
                Picture_Cuadro_Bomb.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["bomb"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["bomb"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["bomb"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["bomb"].Height * Alto_Cuadro), Imagen.PixelFormat);
                Picture_Cuadro_Plant.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["plant"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["plant"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["plant"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["plant"].Height * Alto_Cuadro), Imagen.PixelFormat);
                Picture_Cuadro_Wasteland.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["wasteland"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["wasteland"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["wasteland"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["wasteland"].Height * Alto_Cuadro), Imagen.PixelFormat);
                Picture_Cuadro_Back.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["back"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["back"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["back"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["back"].Height * Alto_Cuadro), Imagen.PixelFormat);

                Picture_Cuadro_Pool.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["pool"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["pool"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["pool"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["pool"].Height * Alto_Cuadro), Imagen.PixelFormat);
                Picture_Cuadro_Courbet.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["courbet"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["courbet"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["courbet"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["courbet"].Height * Alto_Cuadro), Imagen.PixelFormat);
                Picture_Cuadro_Sea.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["sea"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["sea"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["sea"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["sea"].Height * Alto_Cuadro), Imagen.PixelFormat);
                Picture_Cuadro_Sunset.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["sunset"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["sunset"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["sunset"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["sunset"].Height * Alto_Cuadro), Imagen.PixelFormat);
                Picture_Cuadro_Creebet.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["creebet"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["creebet"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["creebet"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["creebet"].Height * Alto_Cuadro), Imagen.PixelFormat);

                Picture_Cuadro_Wanderer.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["wanderer"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["wanderer"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["wanderer"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["wanderer"].Height * Alto_Cuadro), Imagen.PixelFormat);
                Picture_Cuadro_Graham.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["graham"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["graham"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["graham"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["graham"].Height * Alto_Cuadro), Imagen.PixelFormat);

                Picture_Cuadro_Match.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["match"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["match"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["match"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["match"].Height * Alto_Cuadro), Imagen.PixelFormat);
                Picture_Cuadro_Bust.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["bust"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["bust"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["bust"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["bust"].Height * Alto_Cuadro), Imagen.PixelFormat);
                Picture_Cuadro_Stage.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["stage"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["stage"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["stage"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["stage"].Height * Alto_Cuadro), Imagen.PixelFormat);
                Picture_Cuadro_Void.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["void"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["void"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["void"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["void"].Height * Alto_Cuadro), Imagen.PixelFormat);
                Picture_Cuadro_Skull_And_Roses.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["skull_and_roses"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["skull_and_roses"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["skull_and_roses"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["skull_and_roses"].Height * Alto_Cuadro), Imagen.PixelFormat);
                Picture_Cuadro_Wither.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["wither"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["wither"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["wither"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["wither"].Height * Alto_Cuadro), Imagen.PixelFormat);

                Picture_Cuadro_Fighters.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["fighters"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["fighters"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["fighters"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["fighters"].Height * Alto_Cuadro), Imagen.PixelFormat);

                Picture_Cuadro_Skeleton.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["skeleton"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["skeleton"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["skeleton"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["skeleton"].Height * Alto_Cuadro), Imagen.PixelFormat);
                Picture_Cuadro_Donkey_Kong.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["donkey_kong"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["donkey_kong"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["donkey_kong"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["donkey_kong"].Height * Alto_Cuadro), Imagen.PixelFormat);

                Picture_Cuadro_Pointer.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["pointer"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["pointer"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["pointer"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["pointer"].Height * Alto_Cuadro), Imagen.PixelFormat);
                Picture_Cuadro_Pigscene.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["pigscene"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["pigscene"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["pigscene"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["pigscene"].Height * Alto_Cuadro), Imagen.PixelFormat);
                Picture_Cuadro_Burning_Skull.BackgroundImage = Imagen.Clone(new Rectangle(Diccionario_Cuadros_Rectángulos["burning_skull"].X * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["burning_skull"].Y * Alto_Cuadro, Diccionario_Cuadros_Rectángulos["burning_skull"].Width * Ancho_Cuadro, Diccionario_Cuadros_Rectángulos["burning_skull"].Height * Alto_Cuadro), Imagen.PixelFormat);

                Picture_Cuadro_Paintings_Kristoffer_Zetterstrand.BackgroundImage = Imagen;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
