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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Visor_Nombres_Encantamientos : Form
    {
        public Ventana_Visor_Nombres_Encantamientos()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Enchantment Names Viewer by Jupisoft for " + Program.Texto_Usuario;
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
        internal Random Rand = new Random();

        /// <summary>
        /// If it's not static, every time the window loads, the background will be transparent, but after selecting another color, it will impossible to set a transparent color again (unless starting again this window), due to the lack of alpha in the Windows color selector, so this might be a temporary patch for that.
        /// </summary>
        internal /*static */Color Variable_Color_ARGB_Fondo = Color.Transparent;
        internal static Color Variable_Color_ARGB_Fuente = Color.Black;
        internal static bool Variable_Ajuste_Línea = true;

        internal static readonly string Palabras = "the elder scrolls klaatu berata niktu xyzzy bless curse light darkness fire air earth water hot dry cold wet ignite snuff embiggen twist shorten stretch fiddle destroy imbue galvanize enchant free limited range of towards inside sphere cube self other ball mental physical grow shrink demon elemental spirit animal creature beast humanoid undead fresh stale phnglui mglwnafh cthulhu rlyeh wgahnagl fhtagnbaguette";

        /// <summary>
        /// The list of enchanting words extracted from the Minecraft 1.5.2 source code.
        /// </summary>
        internal static readonly string[] Matriz_Palabras_1_5_2 = "the elder scrolls klaatu berata niktu xyzzy bless curse light darkness fire air earth water hot dry cold wet ignite snuff embiggen twist shorten stretch fiddle destroy imbue galvanize enchant free limited range of towards inside sphere cube self other ball mental physical grow shrink demon elemental spirit animal creature beast humanoid undead fresh stale ".Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

        /// <summary>
        /// The list of enchanting words extracted from the Minecraft 1.13 (snapshot 18w19b) source code. Basically some new (and strange) words were added at the end of the string.
        /// </summary>
        internal static readonly string[] Matriz_Palabras = Palabras.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

        /// <summary>
        /// Generates a random enchant name (extracted from Minecraft 1.5.2). Note that in Minecraft this function uses the Java Random class, while here it's using the .NET Random class.
        /// </summary>
        internal string generateRandomEnchantName()
        {
            try
            {
                int Longitud = Rand.Next(2) + 3;
                string Texto = "";
                for (int Índice = 0; Índice < Longitud; ++Índice)
                {
                    if (Índice > 0) Texto = Texto + " ";
                    Texto = Texto + Matriz_Palabras[Rand.Next(Matriz_Palabras.Length)];
                }
                return Texto;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        /// <summary>
        /// Sets the seed for the enchant name RNG (extracted from Minecraft 1.5.2). Note that the .NET Random class doesn't have the same code as the Java Random class, so the results may be different.
        /// </summary>
        internal void setRandSeed(long Semilla)
        {
            try
            {
                Rand = new Random((int)Semilla);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Nombres_Encantamientos_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título + " - [Standard Galactic Alphabet]";
                this.WindowState = FormWindowState.Maximized;
                Ocupado = true;
                ComboBox_Palabras.Items.Add("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
                ComboBox_Palabras.Items.Add("abcdefghijklmnopqrstuvwxyz");
                ComboBox_Palabras.Items.Add(Palabras);
                ComboBox_Palabras.Items.AddRange(Matriz_Palabras);
                ComboBox_Palabras.Text = generateRandomEnchantName();
                //ComboBox_Palabras.Text = "abcdefghijklmnopqrstuvwxyz";
                Registro_Cargar_Opciones();
                Ocupado = false;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Nombres_Encantamientos_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
                /*Bitmap Imagen_ascii = Program.Obtener_Imagen_Ruta(@"C:\...\ascii.png");
                Bitmap Imagen_ascii_sga = Program.Obtener_Imagen_Ruta(@"C:\...\ascii_sga.png");
                Program.Obtener_Imagen_Pintada(Imagen_ascii, Color.Black);
                Program.Obtener_Imagen_Pintada(Imagen_ascii_sga, Color.Black);
                Program.Guardar_Imagen_Temporal(Imagen_ascii, "ascii");
                Program.Guardar_Imagen_Temporal(Imagen_ascii_sga, "ascii_sga");*/
                /*Bitmap Imagen = (Bitmap)Resources.Fuente_ascii.Clone();
                Bitmap Imagen_SGA = (Bitmap)Resources.Fuente_ascii_sga.Clone();
                Graphics Pintar = Graphics.FromImage(Imagen);
                Pintar.Clear(Color.Transparent);
                Pintar.CompositingMode = CompositingMode.SourceCopy;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.None;
                Graphics Pintar_SGA = Graphics.FromImage(Imagen_SGA);
                Pintar_SGA.Clear(Color.Transparent);
                Pintar_SGA.CompositingMode = CompositingMode.SourceCopy;
                Pintar_SGA.CompositingQuality = CompositingQuality.HighQuality;
                Pintar_SGA.InterpolationMode = InterpolationMode.NearestNeighbor;
                Pintar_SGA.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar_SGA.SmoothingMode = SmoothingMode.None;
                for (int Y = 0; Y < 16; Y++)
                {
                    for (int X = 0; X < 16; X++)
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
                            Pintar.DrawImage(Imagen_Temporal, new Rectangle((X * 8) + ((8 - Rectángulo.Width) / 2), Y * 8, Rectángulo.Width, 8), new Rectangle(0, 0, Rectángulo.Width, Rectángulo.Height), GraphicsUnit.Pixel);
                        }
                        if (Rectángulo_SGA.X > -1 && Rectángulo_SGA.Y > -1 && Rectángulo_SGA.X < int.MaxValue && Rectángulo_SGA.Y < int.MaxValue && Rectángulo_SGA.Width > 0 && Rectángulo_SGA.Height > 0)
                        {
                            Rectángulo_SGA.Y = 0; // Don't move it vertically.
                            Rectángulo_SGA.Height = 8;
                            Imagen_Temporal_SGA = Imagen_Temporal_SGA.Clone(Rectángulo_SGA, PixelFormat.Format32bppArgb);
                            Pintar_SGA.DrawImage(Imagen_Temporal_SGA, new Rectangle((X * 8) + ((8 - Rectángulo_SGA.Width) / 2), Y * 8, Rectángulo_SGA.Width, 8), new Rectangle(0, 0, Rectángulo_SGA.Width, Rectángulo_SGA.Height), GraphicsUnit.Pixel);
                        }
                    }
                }
                Pintar.Dispose();
                Pintar = null;
                Pintar_SGA.Dispose();
                Pintar_SGA = null;
                Program.Guardar_Imagen_Temporal(Imagen, "Fuente_ascii");
                Program.Guardar_Imagen_Temporal(Imagen_SGA, "Fuente_ascii_sga");*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Nombres_Encantamientos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Nombres_Encantamientos_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Nombres_Encantamientos_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Visor_Nombres_Encantamientos_DragDrop(object sender, DragEventArgs e)
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
                                    //Minecraft.Información_Niveles Información_Nivel = Minecraft.Información_Niveles.Obtener_Información_Nivel(Ruta);
                                    SystemSounds.Beep.Play();
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

        private void Ventana_Visor_Nombres_Encantamientos_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                Generar_Imágenes_Nombre(ComboBox_Palabras.Text);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Nombres_Encantamientos_KeyDown(object sender, KeyEventArgs e)
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
                    else if (e.KeyCode == Keys.Insert)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Menú_Contextual_Aleatorizar.PerformClick();
                    }
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
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Visor_Nombres_Encantamientos);
                Program.Ejecutar_Ruta(Program.Ruta_Guardado_Imágenes_Visor_Nombres_Encantamientos, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Aleatorizar_Click(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Palabras.Text = generateRandomEnchantName();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Seleccionar_Color_Fondo_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog Diálogo_Color = new ColorDialog();
                Diálogo_Color.AllowFullOpen = true;
                Diálogo_Color.AnyColor = true;
                Diálogo_Color.Color = Variable_Color_ARGB_Fondo;
                Diálogo_Color.CustomColors = new int[16] { 255, 65535, 65280, 16776960, 16711680, 16711935, 0, 16777215, 128, 32896, 32768, 8421376, 8388608, 8388736, 8421504, 12632256 };
                Diálogo_Color.FullOpen = true;
                Diálogo_Color.SolidColorOnly = false;
                if (Diálogo_Color.ShowDialog(this) == DialogResult.OK)
                {
                    Variable_Color_ARGB_Fondo = Color.FromArgb(255, Diálogo_Color.Color.R, Diálogo_Color.Color.G, Diálogo_Color.Color.B);
                    Registro_Guardar_Opciones();
                    Picture.BackColor = Variable_Color_ARGB_Fondo;
                    Picture_SGA.BackColor = Variable_Color_ARGB_Fondo;
                    Generar_Imágenes_Nombre(ComboBox_Palabras.Text);
                }
                Diálogo_Color.Dispose();
                Diálogo_Color = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Seleccionar_Color_Fuente_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog Diálogo_Color = new ColorDialog();
                Diálogo_Color.AllowFullOpen = true;
                Diálogo_Color.AnyColor = true;
                Diálogo_Color.Color = Variable_Color_ARGB_Fuente;
                Diálogo_Color.CustomColors = new int[16] { 255, 65535, 65280, 16776960, 16711680, 16711935, 0, 16777215, 128, 32896, 32768, 8421376, 8388608, 8388736, 8421504, 12632256 };
                Diálogo_Color.FullOpen = true;
                Diálogo_Color.SolidColorOnly = false;
                if (Diálogo_Color.ShowDialog(this) == DialogResult.OK)
                {
                    Variable_Color_ARGB_Fuente = Color.FromArgb(255, Diálogo_Color.Color.R, Diálogo_Color.Color.G, Diálogo_Color.Color.B);
                    Registro_Guardar_Opciones();
                    Generar_Imágenes_Nombre(ComboBox_Palabras.Text);
                }
                Diálogo_Color.Dispose();
                Diálogo_Color = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Ajuste_Línea_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Ajuste_Línea = Menú_Contextual_Ajuste_Línea.Checked;
                Registro_Guardar_Opciones();
                Generar_Imágenes_Nombre(ComboBox_Palabras.Text);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_SGA_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture_SGA.Image != null)
                {
                    Clipboard.SetImage(Picture.Image);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
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

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Visor_Nombres_Encantamientos);
                    Picture.Image.Save(Program.Ruta_Guardado_Imágenes_Visor_Nombres_Encantamientos + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + ".png", ImageFormat.Png);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_SGA_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture_SGA.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Visor_Nombres_Encantamientos);
                    Picture_SGA.Image.Save(Program.Ruta_Guardado_Imágenes_Visor_Nombres_Encantamientos + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + ".png", ImageFormat.Png);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Palabras_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Generar_Imágenes_Nombre(ComboBox_Palabras.Text);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Numérico_Zoom_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Numérico_Zoom.Refresh();
                Generar_Imágenes_Nombre(ComboBox_Palabras.Text);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Aleatorio_Click(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Palabras.Text = generateRandomEnchantName();
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

        internal void Generar_Imágenes_Nombre(string Texto)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(Texto))
                {
                    int Ancho_Cliente = Picture.ClientSize.Width;
                    int Alto_Cliente = Picture.ClientSize.Height;
                    int Caracteres = Texto.Length;
                    int Ancho_Alto = 8; // The width and height of the Minecraft fonts.
                    int Zoom = (int)Numérico_Zoom.Value;
                    int Ancho_Alto_Zoom = Ancho_Alto * Zoom;

                    int Ancho_Caracteres = Ancho_Cliente / Ancho_Alto_Zoom;
                    if (Ancho_Caracteres < 1) Ancho_Caracteres = 1;
                    else if (Ancho_Caracteres > Caracteres) Ancho_Caracteres = Caracteres;
                    int Alto_Caracteres = Caracteres / Ancho_Caracteres;
                    if (Alto_Caracteres < 1) Alto_Caracteres = 1;
                    if (Alto_Caracteres * Ancho_Caracteres < Caracteres) Alto_Caracteres++;

                    if (!Variable_Ajuste_Línea)
                    {
                        Ancho_Caracteres = Caracteres;
                        Alto_Caracteres = 1;
                    }
                    int Ancho = Ancho_Caracteres * Ancho_Alto_Zoom;
                    int Alto = Alto_Caracteres * Ancho_Alto_Zoom;

                    Bitmap Imagen_SGA = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                    Graphics Pintar_SGA = Graphics.FromImage(Imagen_SGA);
                    Pintar_SGA.Clear(Variable_Color_ARGB_Fondo);
                    Pintar_SGA.CompositingMode = CompositingMode.SourceOver;
                    Pintar_SGA.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar_SGA.InterpolationMode = InterpolationMode.NearestNeighbor;
                    Pintar_SGA.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar_SGA.SmoothingMode = SmoothingMode.None;

                    Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.Clear(Variable_Color_ARGB_Fondo);
                    Pintar.CompositingMode = CompositingMode.SourceOver;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.None;

                    for (int Y = 0, Índice = 0, Resto = Caracteres; Y < Alto_Caracteres; Y++, Resto -= Ancho_Caracteres)
                    {
                        for (int X = 0; X < Ancho_Caracteres; X++, Índice++)
                        {
                            if (Índice < Texto.Length)
                            {
                                int Índice_Caracter = (int)Texto[Índice];
                                if (Índice_Caracter < 0 || Índice_Caracter > 255) Índice_Caracter = 32; // Space
                                int X_Caracter = (int)Índice_Caracter % 16;
                                int Y_Caracter = (int)Índice_Caracter / 16;
                                int X_Diferencia = (Ancho - (Math.Min(Resto, Ancho_Caracteres) * Ancho_Alto_Zoom)) / 2;
                                Pintar_SGA.DrawImage(Program.Obtener_Imagen_Pintada(Resources.Fuente_ascii_sga.Clone(new Rectangle(X_Caracter * Ancho_Alto, Y_Caracter * Ancho_Alto, Ancho_Alto, Ancho_Alto), PixelFormat.Format32bppArgb), Variable_Color_ARGB_Fuente), new Rectangle(X_Diferencia + (X * Ancho_Alto_Zoom), Y * Ancho_Alto_Zoom, Ancho_Alto_Zoom, Ancho_Alto_Zoom), new Rectangle(0, 0, Ancho_Alto, Ancho_Alto), GraphicsUnit.Pixel);
                                Pintar.DrawImage(Program.Obtener_Imagen_Pintada(Resources.Fuente_ascii.Clone(new Rectangle(X_Caracter * Ancho_Alto, Y_Caracter * Ancho_Alto, Ancho_Alto, Ancho_Alto), PixelFormat.Format32bppArgb), Variable_Color_ARGB_Fuente), new Rectangle(X_Diferencia + (X * Ancho_Alto_Zoom), Y * Ancho_Alto_Zoom, Ancho_Alto_Zoom, Ancho_Alto_Zoom), new Rectangle(0, 0, Ancho_Alto, Ancho_Alto), GraphicsUnit.Pixel);
                            }
                        }
                    }
                    Pintar_SGA.Dispose();
                    Pintar_SGA = null;
                    Pintar.Dispose();
                    Pintar = null;
                    Picture_SGA.Image = Imagen_SGA;
                    Picture.Image = Imagen;
                    Picture_SGA.Refresh();
                    Picture.Refresh();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void ComboBox_Palabras_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    ComboBox_Palabras.Text = generateRandomEnchantName();
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
                    ComboBox_Palabras.Text = generateRandomEnchantName();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_SGA_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    ComboBox_Palabras.Text = generateRandomEnchantName();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
