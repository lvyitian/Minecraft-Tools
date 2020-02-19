using ICSharpCode.SharpZipLib.Zip;
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
    public partial class Ventana_Generador_Packs_Recursos_Mapas_Luz: Form
    {
        public Ventana_Generador_Packs_Recursos_Mapas_Luz()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Custom Light Maps Resource Packs Generator by Jupisoft for " + Program.Texto_Usuario;
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

        internal static int Variable_Plantilla = 0;
        internal static int Variable_Formato_Pack = 3;
        internal static bool Variable_Cielo_Nublado = false;

        internal Bitmap Variable_Imagen_Pack = null;
        internal Bitmap Variable_Imagen_Mapa_Luz = null;

        private void Ventana_Generador_Packs_Recursos_Mapas_Luz_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título + " - [The packs need Optifine with \"Custom Colors: On\"]";
                this.WindowState = FormWindowState.Maximized;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
                ComboBox_Plantilla.SelectedIndex = Variable_Plantilla;
                ComboBox_Formato_Pack.SelectedIndex = Variable_Formato_Pack;
                CheckBox_Cielo_Nublado.Checked = Variable_Cielo_Nublado;
                //Actualizar_Nivel_Luz();
                // This will generate a resource pack with logarithm or square root filters to see
                // a lot better where mobs will be able to spawn, this is a modular resource pack.
                /*if (string.Compare(Environment.UserName, "Jupisoft", true) == 0) // Overwrite warning!
                {
                    string Ruta_Minecraft = Application.StartupPath + "\\1.13.2";
                    string[] Matriz_Archivos = Directory.GetFiles(Ruta_Minecraft, "*", SearchOption.AllDirectories);
                    if (Matriz_Archivos != null && Matriz_Archivos.Length > 0)
                    {
                        if (Matriz_Archivos.Length > 1) Array.Sort(Matriz_Archivos);
                        foreach (string Ruta in Matriz_Archivos)
                        {
                            try
                            {
                                Image Imagen_Original = null;
                                Program.Quitar_Atributo_Sólo_Lectura(Ruta);
                                FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                if (Lector != null && Lector.Length > 0L)
                                {
                                    Lector.Seek(0L, SeekOrigin.Begin);
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
                                        Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                                        Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                        Pintar.Dispose();
                                        Pintar = null;
                                        Imagen_Original.Dispose();
                                        Imagen_Original = null;
                                        Lector.Close();
                                        Lector.Dispose();
                                        Lector = null;
                                        BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                                        byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                                        System.Runtime.InteropServices.Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                                        int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                                        int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                                        int Rojo, Verde, Azul;
                                        for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                                        {
                                            for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                                            {
                                                //Rojo = Matriz_Bytes[Índice + 2] + 128; // Brightness.
                                                //Verde = Matriz_Bytes[Índice + 1] + 128;
                                                //Azul = Matriz_Bytes[Índice] + 128;

                                                Rojo = Matriz_Bytes[Índice + 2] * 2; // Intensity.
                                                Verde = Matriz_Bytes[Índice + 1] * 2;
                                                Azul = Matriz_Bytes[Índice] * 2;

                                                if (Rojo < 0) Rojo = 0;
                                                else if (Rojo > 255) Rojo = 255;
                                                if (Verde < 0) Verde = 0;
                                                else if (Verde > 255) Verde = 255;
                                                if (Azul < 0) Azul = 0;
                                                else if (Azul > 255) Azul = 255;

                                                Matriz_Bytes[Índice + 2] = (byte)Rojo;
                                                Matriz_Bytes[Índice + 1] = (byte)Verde;
                                                Matriz_Bytes[Índice] = (byte)Azul;

                                                //Matriz_Bytes[Índice + 2] = Program.Matriz_Bytes_Filtro_Raíz_Cuadrada[Matriz_Bytes[Índice + 2]]; // Square root.
                                                //Matriz_Bytes[Índice + 1] = Program.Matriz_Bytes_Filtro_Raíz_Cuadrada[Matriz_Bytes[Índice + 1]];
                                                //Matriz_Bytes[Índice] = Program.Matriz_Bytes_Filtro_Raíz_Cuadrada[Matriz_Bytes[Índice]];

                                                //Matriz_Bytes[Índice + 2] = Program.Matriz_Bytes_Filtro_Logaritmo[Matriz_Bytes[Índice + 2]]; // Logarithm.
                                                //Matriz_Bytes[Índice + 1] = Program.Matriz_Bytes_Filtro_Logaritmo[Matriz_Bytes[Índice + 1]];
                                                //Matriz_Bytes[Índice] = Program.Matriz_Bytes_Filtro_Logaritmo[Matriz_Bytes[Índice]];
                                            }
                                        }
                                        System.Runtime.InteropServices.Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                                        Imagen.UnlockBits(Bitmap_Data);
                                        Bitmap_Data = null;
                                        Matriz_Bytes = null;
                                        Imagen.Save(Ruta, ImageFormat.Png);
                                        Imagen.Dispose();
                                        Imagen = null;
                                    }
                                    else
                                    {
                                        Lector.Close();
                                        Lector.Dispose();
                                        Lector = null;
                                    }
                                }
                            }
                            catch (Exception Excepción)
                            {
                                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                                continue;
                            }
                        }
                    }
                }*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Packs_Recursos_Mapas_Luz_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
                /*ColorDialog Diálogo_Color = new ColorDialog();
                Diálogo_Color.AllowFullOpen = true;
                Diálogo_Color.AnyColor = true;
                Diálogo_Color.Color = Variable_Color_Fondo;
                Diálogo_Color.CustomColors = new int[16] { 255, 65535, 65280, 16776960, 16711680, 16711935, 0, 16777215, 128, 32896, 32768, 8421376, 8388608, 8388736, 8421504, 12632256 };
                Diálogo_Color.FullOpen = true;
                Diálogo_Color.SolidColorOnly = false;
                if (Diálogo_Color.ShowDialog(this) == DialogResult.OK)
                {
                    Variable_Color_Fondo = Color.FromArgb(255, Diálogo_Color.Color.R, Diálogo_Color.Color.G, Diálogo_Color.Color.B);
                    Registro_Guardar_Opciones();
                    Picture.BackColor = Variable_Color_Fondo;
                    Picture.Refresh();
                }
                Diálogo_Color.Dispose();
                Diálogo_Color = null;*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Packs_Recursos_Mapas_Luz_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Packs_Recursos_Mapas_Luz_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Packs_Recursos_Mapas_Luz_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
                //Actualizar_Mapa_Luz();
                if (Variable_Imagen_Mapa_Luz != null)
                {
                    int Zoom;
                    Picture_Mapa_Luz.Image = Program.Obtener_Imagen_Autozoom(Variable_Imagen_Mapa_Luz, Picture_Mapa_Luz.ClientSize.Width, Picture_Mapa_Luz.ClientSize.Height, true, CheckState.Unchecked, out Zoom);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Packs_Recursos_Mapas_Luz_KeyDown(object sender, KeyEventArgs e)
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
                Actualizar_Mapa_Luz();
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

        private void ComboBox_Plantilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Ocupado && ComboBox_Plantilla.SelectedIndex > -1)
                {
                    Ocupado = true;
                    Variable_Plantilla = ComboBox_Plantilla.SelectedIndex;
                    if (Variable_Plantilla == 0)
                    {
                        CheckBox_Cielo_Nublado.Enabled = true; // Manual mode.
                        NumericUpDown_Parpadeo_Antorcha.Enabled = true;
                        TrackBar_Parpadeo_Antorcha.Enabled = true;
                        NumericUpDown_Alfa.Enabled = true;
                        TrackBar_Alfa.Enabled = true;
                        NumericUpDown_Rojo.Enabled = true;
                        TrackBar_Rojo.Enabled = true;
                        NumericUpDown_Verde.Enabled = true;
                        TrackBar_Verde.Enabled = true;
                        NumericUpDown_Azul.Enabled = true;
                        TrackBar_Azul.Enabled = true;
                        Variable_Imagen_Pack = null; //Resources.Pack_Custom_Color_Lighting;
                        Picture_Pack.Image = null;
                        NumericUpDown_Dimensión_Mínima.Value = -1;
                        NumericUpDown_Dimensión_Máxima.Value = 1;
                        Variable_Imagen_Mapa_Luz = null;
                        Picture_Mapa_Luz.Image = null;
                    }
                    else if (Variable_Plantilla == 1)
                    {
                        CheckBox_Cielo_Nublado.Enabled = false;
                        NumericUpDown_Parpadeo_Antorcha.Enabled = false;
                        TrackBar_Parpadeo_Antorcha.Enabled = false;
                        NumericUpDown_Alfa.Enabled = false;
                        TrackBar_Alfa.Enabled = false;
                        NumericUpDown_Rojo.Enabled = false;
                        TrackBar_Rojo.Enabled = false;
                        NumericUpDown_Verde.Enabled = false;
                        TrackBar_Verde.Enabled = false;
                        NumericUpDown_Azul.Enabled = false;
                        TrackBar_Azul.Enabled = false;
                        Variable_Imagen_Pack = Resources.Pack_Indev_Scary_Lighting;
                        Picture_Pack.Image = Variable_Imagen_Pack;
                        NumericUpDown_Dimensión_Mínima.Value = -1;
                        NumericUpDown_Dimensión_Máxima.Value = 1;
                        Variable_Imagen_Mapa_Luz = Resources.Light_Map_Indev_Scary_Lighting;
                        int Zoom;
                        Picture_Mapa_Luz.Image = Program.Obtener_Imagen_Autozoom(Variable_Imagen_Mapa_Luz, Picture_Mapa_Luz.ClientSize.Width, Picture_Mapa_Luz.ClientSize.Height, true, CheckState.Unchecked, out Zoom);
                    }
                    else if (Variable_Plantilla == 2)
                    {
                        CheckBox_Cielo_Nublado.Enabled = false;
                        NumericUpDown_Parpadeo_Antorcha.Enabled = false;
                        TrackBar_Parpadeo_Antorcha.Enabled = false;
                        NumericUpDown_Alfa.Enabled = false;
                        TrackBar_Alfa.Enabled = false;
                        NumericUpDown_Rojo.Enabled = false;
                        TrackBar_Rojo.Enabled = false;
                        NumericUpDown_Verde.Enabled = false;
                        TrackBar_Verde.Enabled = false;
                        NumericUpDown_Azul.Enabled = false;
                        TrackBar_Azul.Enabled = false;
                        Variable_Imagen_Pack = Resources.Pack_Infinite_Blind_Vision;
                        Picture_Pack.Image = Variable_Imagen_Pack;
                        NumericUpDown_Dimensión_Mínima.Value = -1;
                        NumericUpDown_Dimensión_Máxima.Value = 1;
                        Variable_Imagen_Mapa_Luz = Resources.Light_Map_Infinite_Blind_Vision;
                        int Zoom;
                        Picture_Mapa_Luz.Image = Program.Obtener_Imagen_Autozoom(Variable_Imagen_Mapa_Luz, Picture_Mapa_Luz.ClientSize.Width, Picture_Mapa_Luz.ClientSize.Height, true, CheckState.Unchecked, out Zoom);
                    }
                    else if (Variable_Plantilla == 3)
                    {
                        CheckBox_Cielo_Nublado.Enabled = false;
                        NumericUpDown_Parpadeo_Antorcha.Enabled = false;
                        TrackBar_Parpadeo_Antorcha.Enabled = false;
                        NumericUpDown_Alfa.Enabled = false;
                        TrackBar_Alfa.Enabled = false;
                        NumericUpDown_Rojo.Enabled = false;
                        TrackBar_Rojo.Enabled = false;
                        NumericUpDown_Verde.Enabled = false;
                        TrackBar_Verde.Enabled = false;
                        NumericUpDown_Azul.Enabled = false;
                        TrackBar_Azul.Enabled = false;
                        Variable_Imagen_Pack = Resources.Pack_Infinite_Night_Vision;
                        Picture_Pack.Image = Variable_Imagen_Pack;
                        NumericUpDown_Dimensión_Mínima.Value = -1;
                        NumericUpDown_Dimensión_Máxima.Value = 1;
                        Variable_Imagen_Mapa_Luz = Resources.Light_Map_Infinite_Night_Vision;
                        int Zoom;
                        Picture_Mapa_Luz.Image = Program.Obtener_Imagen_Autozoom(Variable_Imagen_Mapa_Luz, Picture_Mapa_Luz.ClientSize.Width, Picture_Mapa_Luz.ClientSize.Height, true, CheckState.Unchecked, out Zoom);
                    }
                    else if (Variable_Plantilla == 4)
                    {
                        CheckBox_Cielo_Nublado.Enabled = false;
                        NumericUpDown_Parpadeo_Antorcha.Enabled = false;
                        TrackBar_Parpadeo_Antorcha.Enabled = false;
                        NumericUpDown_Alfa.Enabled = false;
                        TrackBar_Alfa.Enabled = false;
                        NumericUpDown_Rojo.Enabled = false;
                        TrackBar_Rojo.Enabled = false;
                        NumericUpDown_Verde.Enabled = false;
                        TrackBar_Verde.Enabled = false;
                        NumericUpDown_Azul.Enabled = false;
                        TrackBar_Azul.Enabled = false;
                        Variable_Imagen_Pack = Resources.Pack_Inverted_Torch_Lighting;
                        Picture_Pack.Image = Variable_Imagen_Pack;
                        NumericUpDown_Dimensión_Mínima.Value = -1;
                        NumericUpDown_Dimensión_Máxima.Value = 1;
                        Variable_Imagen_Mapa_Luz = Resources.Light_Map_Inverted_Torch_Lighting;
                        int Zoom;
                        Picture_Mapa_Luz.Image = Program.Obtener_Imagen_Autozoom(Variable_Imagen_Mapa_Luz, Picture_Mapa_Luz.ClientSize.Width, Picture_Mapa_Luz.ClientSize.Height, true, CheckState.Unchecked, out Zoom);
                    }
                    else if (Variable_Plantilla == 5)
                    {
                        CheckBox_Cielo_Nublado.Enabled = false;
                        NumericUpDown_Parpadeo_Antorcha.Enabled = false;
                        TrackBar_Parpadeo_Antorcha.Enabled = false;
                        NumericUpDown_Alfa.Enabled = false;
                        TrackBar_Alfa.Enabled = false;
                        NumericUpDown_Rojo.Enabled = false;
                        TrackBar_Rojo.Enabled = false;
                        NumericUpDown_Verde.Enabled = false;
                        TrackBar_Verde.Enabled = false;
                        NumericUpDown_Azul.Enabled = false;
                        TrackBar_Azul.Enabled = false;
                        Variable_Imagen_Pack = Resources.Pack_Mob_Spawning_Detector;
                        Picture_Pack.Image = Variable_Imagen_Pack;
                        NumericUpDown_Dimensión_Mínima.Value = -1;
                        NumericUpDown_Dimensión_Máxima.Value = 1;
                        Variable_Imagen_Mapa_Luz = Resources.Light_Map_Mob_Spawning_Detector;
                        int Zoom;
                        Picture_Mapa_Luz.Image = Program.Obtener_Imagen_Autozoom(Variable_Imagen_Mapa_Luz, Picture_Mapa_Luz.ClientSize.Width, Picture_Mapa_Luz.ClientSize.Height, true, CheckState.Unchecked, out Zoom);
                    }
                    else// if (Variable_Plantilla == 6)
                    {
                        CheckBox_Cielo_Nublado.Enabled = false;
                        NumericUpDown_Parpadeo_Antorcha.Enabled = false;
                        TrackBar_Parpadeo_Antorcha.Enabled = false;
                        NumericUpDown_Alfa.Enabled = false;
                        TrackBar_Alfa.Enabled = false;
                        NumericUpDown_Rojo.Enabled = false;
                        TrackBar_Rojo.Enabled = false;
                        NumericUpDown_Verde.Enabled = false;
                        TrackBar_Verde.Enabled = false;
                        NumericUpDown_Azul.Enabled = false;
                        TrackBar_Azul.Enabled = false;
                        Variable_Imagen_Pack = Resources.Pack_Rainbow_Torch_Lighting;
                        Picture_Pack.Image = Variable_Imagen_Pack;
                        NumericUpDown_Dimensión_Mínima.Value = -1;
                        NumericUpDown_Dimensión_Máxima.Value = 1;
                        Variable_Imagen_Mapa_Luz = Resources.Light_Map_Rainbow_Torch_Lighting;
                        int Zoom;
                        Picture_Mapa_Luz.Image = Program.Obtener_Imagen_Autozoom(Variable_Imagen_Mapa_Luz, Picture_Mapa_Luz.ClientSize.Width, Picture_Mapa_Luz.ClientSize.Height, true, CheckState.Unchecked, out Zoom);
                    }
                    Ocupado = false;
                    Actualizar_Mapa_Luz();
                }
                else Picture_Pack.Image = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Formato_Pack_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Formato_Pack.SelectedIndex > -1)
                {
                    Variable_Formato_Pack = ComboBox_Formato_Pack.SelectedIndex;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Cielo_Nublado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Cielo_Nublado = CheckBox_Cielo_Nublado.Checked;
                Actualizar_Mapa_Luz();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Generar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (Variable_Imagen_Pack != null && Variable_Imagen_Mapa_Luz != null)
                {
                    int Alfa = (int)NumericUpDown_Alfa.Value;
                    int Rojo = (int)NumericUpDown_Rojo.Value;
                    int Verde = (int)NumericUpDown_Verde.Value;
                    int Azul = (int)NumericUpDown_Azul.Value;
                    int Dimensión_Mínima = Math.Min((int)NumericUpDown_Dimensión_Mínima.Value, (int)NumericUpDown_Dimensión_Máxima.Value); // Avoid user input errors.
                    int Dimensión_Máxima = Math.Max((int)NumericUpDown_Dimensión_Mínima.Value, (int)NumericUpDown_Dimensión_Máxima.Value); // Get the true minimum and maximum.

                    MemoryStream Lector_Memoria = new MemoryStream(); // Save the image as PNG in the memory.
                    Variable_Imagen_Mapa_Luz.Save(Lector_Memoria, ImageFormat.Png);
                    byte[] Matriz_Bytes = Lector_Memoria.ToArray(); // Get the bytes of the saved image.
                    Lector_Memoria.Close();
                    Lector_Memoria.Dispose();
                    Lector_Memoria = null;

                    int Formato_Pack = ComboBox_Formato_Pack.SelectedIndex + 1;
                    string Ruta_ZIP = Program.Obtener_Ruta_Temporal_Escritorio();
                    if (Variable_Plantilla == 0) Ruta_ZIP += " Color ARGB " + Alfa.ToString() + ", " + Rojo.ToString() + ", " + Verde.ToString() + ", " + Azul.ToString() + " [Optifine] [1.6 to 1.14+].zip";
                    else if (Variable_Plantilla == 1) Ruta_ZIP += " Indev Scary Lighting [Optifine] [1.6 to 1.14+].zip";
                    else if (Variable_Plantilla == 2) Ruta_ZIP += " Infinite Blind Vision [Optifine] [1.6 to 1.14+].zip";
                    else if (Variable_Plantilla == 3) Ruta_ZIP += " Infinite Night Vision [Optifine] [1.6 to 1.14+].zip";
                    else if (Variable_Plantilla == 4) Ruta_ZIP += " Inverted Torch Lighting [Optifine] [1.6 to 1.14+].zip";
                    else if (Variable_Plantilla == 5) Ruta_ZIP += " Mob Spawning Detector [Optifine] [1.6 to 1.14+].zip";
                    else Ruta_ZIP += " Rainbow Torch Lighting [Optifine] [1.6 to 1.14+].zip";
                    FileStream Lector = new FileStream(Ruta_ZIP, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector.SetLength(0L);
                    Lector.Seek(0L, SeekOrigin.Begin);
                    ZipOutputStream Archivo_ZIP = new ZipOutputStream(Lector); // Start a new zip file.
                    for (int Índice = Dimensión_Mínima; Índice <= Dimensión_Máxima; Índice++)
                    {
                        Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/mcpatcher/lightmap/world" + Índice.ToString() + ".png"));
                        Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length); // Add copies of the image in the zip.
                        Archivo_ZIP.CloseEntry(); // Add support for Minecraft 1.12.2-.
                        Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/optifine/lightmap/world" + Índice.ToString() + ".png"));
                        Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length); // Add copies of the image in the zip.
                        Archivo_ZIP.CloseEntry(); // Add support for Minecraft 1.13+.
                    } // This even supports modpacks dimensions, just search it's ID numbers and include them. Optifine supports them?
                    Matriz_Bytes = null;

                    // package net.minecraft.util.text:
                    /*BLACK("BLACK", '0', 0),
                    DARK_BLUE("DARK_BLUE", '1', 1),
                    DARK_GREEN("DARK_GREEN", '2', 2),
                    DARK_AQUA("DARK_AQUA", '3', 3),
                    DARK_RED("DARK_RED", '4', 4),
                    DARK_PURPLE("DARK_PURPLE", '5', 5),
                    GOLD("GOLD", '6', 6),
                    GRAY("GRAY", '7', 7),
                    DARK_GRAY("DARK_GRAY", '8', 8),
                    BLUE("BLUE", '9', 9),
                    GREEN("GREEN", 'a', 10),
                    AQUA("AQUA", 'b', 11),
                    RED("RED", 'c', 12),
                    LIGHT_PURPLE("LIGHT_PURPLE", 'd', 13),
                    YELLOW("YELLOW", 'e', 14),
                    WHITE("WHITE", 'f', 15),
                    OBFUSCATED("OBFUSCATED", 'k', true),
                    BOLD("BOLD", 'l', true),
                    STRIKETHROUGH("STRIKETHROUGH", 'm', true),
                    UNDERLINE("UNDERLINE", 'n', true),
                    ITALIC("ITALIC", 'o', true),
                    RESET("RESET", 'r', -1);*/

                    Archivo_ZIP.PutNextEntry(new ZipEntry("pack.mcmeta"));
                    string Texto_Pack = "{\r\n  \"pack\": {\r\n    \"pack_format\": " + Formato_Pack.ToString() + ",\r\n    \"description\": \"";
                    if (Variable_Plantilla == 0) Texto_Pack += "§fColor§r §7A§cR§aG§9B:§r §7" + Alfa.ToString() + "§r, §c" + Rojo.ToString() + "§r, §a" + Verde.ToString() + "§r, §9" + Azul.ToString();
                    else if (Variable_Plantilla == 1) Texto_Pack += "§7Indev§r §8Scary§r §fLighting§r";
                    else if (Variable_Plantilla == 2) Texto_Pack += "§fInfinite§r §8Blind Vision§r";
                    else if (Variable_Plantilla == 3) Texto_Pack += "§fInfinite§r §aNight Vision§r";
                    else if (Variable_Plantilla == 4) Texto_Pack += "§7Inverted§r §eTorch§r §fLighting§r";
                    else if (Variable_Plantilla == 5) Texto_Pack += "§cMob Spawning§r §fDetector§r";
                    else Texto_Pack += "§cR§6a§ei§an§bb§9o§dw§r §eTorch§r §fLighting§r";
                    Texto_Pack += "\\n§fBy:§r §c" + Program.Texto_Usuario + "§r §f[Use Optifine]§r\"\r\n  }\r\n}\r\n";
                    Matriz_Bytes = Encoding.UTF8.GetBytes(Texto_Pack);
                    Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Archivo_ZIP.CloseEntry();
                    Matriz_Bytes = null;
                    Texto_Pack = null;

                    Lector_Memoria = new MemoryStream(); // Save the image as PNG in the memory.
                    Variable_Imagen_Pack.Save(Lector_Memoria, ImageFormat.Png);
                    Matriz_Bytes = Lector_Memoria.ToArray(); // Get the bytes of the saved image.
                    Lector_Memoria.Close();
                    Lector_Memoria.Dispose();
                    Lector_Memoria = null;

                    Archivo_ZIP.PutNextEntry(new ZipEntry("pack.png"));
                    Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Archivo_ZIP.CloseEntry();
                    Matriz_Bytes = null;

                    Archivo_ZIP.Finish();
                    Archivo_ZIP.Close();
                    Archivo_ZIP.Dispose();
                    Archivo_ZIP = null;
                    Matriz_Bytes = null;
                    SystemSounds.Asterisk.Play();
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void NumericUpDown_Parpadeo_Antorcha_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (TrackBar_Parpadeo_Antorcha.Value != (int)NumericUpDown_Parpadeo_Antorcha.Value)
                {
                    TrackBar_Parpadeo_Antorcha.Value = (int)NumericUpDown_Parpadeo_Antorcha.Value;
                }
                Actualizar_Mapa_Luz();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TrackBar_Parpadeo_Antorcha_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (NumericUpDown_Parpadeo_Antorcha.Value != TrackBar_Parpadeo_Antorcha.Value)
                {
                    NumericUpDown_Parpadeo_Antorcha.Value = TrackBar_Parpadeo_Antorcha.Value;
                } // Don't update, just sync it.
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Alfa_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (TrackBar_Alfa.Value != (int)NumericUpDown_Alfa.Value)
                {
                    TrackBar_Alfa.Value = (int)NumericUpDown_Alfa.Value;
                }
                Actualizar_Mapa_Luz();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TrackBar_Alfa_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (NumericUpDown_Alfa.Value != TrackBar_Alfa.Value)
                {
                    NumericUpDown_Alfa.Value = TrackBar_Alfa.Value;
                } // Don't update, just sync it.
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Rojo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (TrackBar_Rojo.Value != (int)NumericUpDown_Rojo.Value)
                {
                    TrackBar_Rojo.Value = (int)NumericUpDown_Rojo.Value;
                }
                Actualizar_Mapa_Luz();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TrackBar_Rojo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (NumericUpDown_Rojo.Value != TrackBar_Rojo.Value)
                {
                    NumericUpDown_Rojo.Value = TrackBar_Rojo.Value;
                } // Don't update, just sync it.
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Verde_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (TrackBar_Verde.Value != (int)NumericUpDown_Verde.Value)
                {
                    TrackBar_Verde.Value = (int)NumericUpDown_Verde.Value;
                }
                Actualizar_Mapa_Luz();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TrackBar_Verde_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (NumericUpDown_Verde.Value != TrackBar_Verde.Value)
                {
                    NumericUpDown_Verde.Value = TrackBar_Verde.Value;
                } // Don't update, just sync it.
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Azul_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (TrackBar_Azul.Value != (int)NumericUpDown_Azul.Value)
                {
                    TrackBar_Azul.Value = (int)NumericUpDown_Azul.Value;
                }
                Actualizar_Mapa_Luz();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TrackBar_Azul_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (NumericUpDown_Azul.Value != TrackBar_Azul.Value)
                {
                    NumericUpDown_Azul.Value = TrackBar_Azul.Value;
                } // Don't update, just sync it.
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Dimensión_Mínima_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (TrackBar_Dimensión_Mínima.Value != (int)NumericUpDown_Dimensión_Mínima.Value)
                {
                    TrackBar_Dimensión_Mínima.Value = (int)NumericUpDown_Dimensión_Mínima.Value;
                }
                Actualizar_Mapa_Luz();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TrackBar_Dimensión_Mínima_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (NumericUpDown_Dimensión_Mínima.Value != TrackBar_Dimensión_Mínima.Value)
                {
                    NumericUpDown_Dimensión_Mínima.Value = TrackBar_Dimensión_Mínima.Value;
                } // Don't update, just sync it.

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Dimensión_Máxima_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (TrackBar_Dimensión_Máxima.Value != (int)NumericUpDown_Dimensión_Máxima.Value)
                {
                    TrackBar_Dimensión_Máxima.Value = (int)NumericUpDown_Dimensión_Máxima.Value;
                }
                Actualizar_Mapa_Luz();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TrackBar_Dimensión_Máxima_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (NumericUpDown_Dimensión_Máxima.Value != TrackBar_Dimensión_Máxima.Value)
                {
                    NumericUpDown_Dimensión_Máxima.Value = TrackBar_Dimensión_Máxima.Value;
                } // Don't update, just sync it.
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Actualizar_Mapa_Luz()
        {
            try
            {
                if (!Ocupado && Variable_Plantilla == 0) // Only for custom color lighting.
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        Ocupado = true;
                        //int Parpadeo = (int)NumericUpDown_Parpadeo_Antorcha.Value; // Finish this.
                        int Alfa = (int)NumericUpDown_Alfa.Value;
                        int Rojo = (int)NumericUpDown_Rojo.Value;
                        int Verde = (int)NumericUpDown_Verde.Value;
                        int Azul = (int)NumericUpDown_Azul.Value;
                        Bitmap Imagen_Alfa = new Bitmap(16, 16, PixelFormat.Format32bppArgb);
                        Graphics Pintar_Alfa = Graphics.FromImage(Imagen_Alfa);
                        Pintar_Alfa.CompositingMode = CompositingMode.SourceCopy;
                        Pintar_Alfa.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar_Alfa.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Pintar_Alfa.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar_Alfa.SmoothingMode = SmoothingMode.HighQuality;
                        Pintar_Alfa.TextRenderingHint = TextRenderingHint.AntiAlias;
                        SolidBrush Pincel_Alfa = new SolidBrush(Color.FromArgb(Alfa, Rojo, Verde, Azul));
                        Pintar_Alfa.FillRectangle(Pincel_Alfa, 0, 0, 16, 16);
                        Pincel_Alfa.Dispose();
                        Pincel_Alfa = null;
                        Pintar_Alfa.Dispose();
                        Pintar_Alfa = null;

                        Bitmap Imagen_Rojo = new Bitmap(16, 16, PixelFormat.Format32bppArgb);
                        Graphics Pintar_Rojo = Graphics.FromImage(Imagen_Rojo);
                        Pintar_Rojo.CompositingMode = CompositingMode.SourceCopy;
                        Pintar_Rojo.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar_Rojo.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Pintar_Rojo.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar_Rojo.SmoothingMode = SmoothingMode.HighQuality;
                        Pintar_Rojo.TextRenderingHint = TextRenderingHint.AntiAlias;
                        SolidBrush Pincel_Rojo = new SolidBrush(Color.FromArgb(Alfa, Rojo, 0, 0));
                        Pintar_Rojo.FillRectangle(Pincel_Rojo, 0, 0, 16, 16);
                        Pincel_Rojo.Dispose();
                        Pincel_Rojo = null;
                        Pintar_Rojo.Dispose();
                        Pintar_Rojo = null;

                        Bitmap Imagen_Verde = new Bitmap(16, 16, PixelFormat.Format32bppArgb);
                        Graphics Pintar_Verde = Graphics.FromImage(Imagen_Verde);
                        Pintar_Verde.CompositingMode = CompositingMode.SourceCopy;
                        Pintar_Verde.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar_Verde.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Pintar_Verde.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar_Verde.SmoothingMode = SmoothingMode.HighQuality;
                        Pintar_Verde.TextRenderingHint = TextRenderingHint.AntiAlias;
                        SolidBrush Pincel_Verde = new SolidBrush(Color.FromArgb(Alfa, 0, Verde, 0));
                        Pintar_Verde.FillRectangle(Pincel_Verde, 0, 0, 16, 16);
                        Pincel_Verde.Dispose();
                        Pincel_Verde = null;
                        Pintar_Verde.Dispose();
                        Pintar_Verde = null;

                        Bitmap Imagen_Azul = new Bitmap(16, 16, PixelFormat.Format32bppArgb);
                        Graphics Pintar_Azul = Graphics.FromImage(Imagen_Azul);
                        Pintar_Azul.CompositingMode = CompositingMode.SourceCopy;
                        Pintar_Azul.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar_Azul.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Pintar_Azul.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar_Azul.SmoothingMode = SmoothingMode.HighQuality;
                        Pintar_Azul.TextRenderingHint = TextRenderingHint.AntiAlias;
                        SolidBrush Pincel_Azul = new SolidBrush(Color.FromArgb(Alfa, 0, 0, Azul));
                        Pintar_Azul.FillRectangle(Pincel_Azul, 0, 0, 16, 16);
                        Pincel_Azul.Dispose();
                        Pincel_Azul = null;
                        Pintar_Azul.Dispose();
                        Pintar_Azul = null;

                        Variable_Imagen_Pack = new Bitmap(16, 16, PixelFormat.Format32bppArgb); // Preview of the pack.
                        Graphics Pintar_Pack = Graphics.FromImage(Variable_Imagen_Pack);
                        Pintar_Pack.CompositingMode = CompositingMode.SourceCopy;
                        Pintar_Pack.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar_Pack.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Pintar_Pack.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar_Pack.SmoothingMode = SmoothingMode.HighQuality;
                        Pintar_Pack.TextRenderingHint = TextRenderingHint.AntiAlias;
                        for (int Y = 0; Y < 16; Y += 2) // Draw the background to have a 8 x 8 pattern even with zoom.
                        {
                            for (int X = 0; X < 16; X += 2)
                            {
                                Pintar_Pack.FillRectangle(Y % 4 == 0 ? (X % 4 == 0 ? Brushes.Silver : Brushes.White) : (X % 4 == 0 ? Brushes.White : Brushes.Silver), X, Y, 2, 2);
                            }
                        }
                        Pintar_Pack.CompositingMode = CompositingMode.SourceOver;
                        SolidBrush Pincel = new SolidBrush(Color.FromArgb(Alfa, Rojo, Verde, Azul));
                        Pintar_Pack.FillRectangle(Pincel, 0, 0, 16, 16);
                        Pincel.Dispose();
                        Pincel = null;
                        Pintar_Pack.Dispose();
                        Pintar_Pack = null;
                        Variable_Imagen_Pack = Program.Obtener_Imagen_Miniatura(Variable_Imagen_Pack, 64, 64, false, false, CheckState.Checked);

                        Variable_Imagen_Mapa_Luz = new Bitmap(64, 64, PixelFormat.Format32bppArgb); // Light map for Optifine.
                        Graphics Pintar_Mapa_Luz = Graphics.FromImage(Variable_Imagen_Mapa_Luz);
                        Pintar_Mapa_Luz.CompositingMode = CompositingMode.SourceCopy;
                        Pintar_Mapa_Luz.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar_Mapa_Luz.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Pintar_Mapa_Luz.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar_Mapa_Luz.SmoothingMode = SmoothingMode.HighQuality;
                        Pintar_Mapa_Luz.TextRenderingHint = TextRenderingHint.AntiAlias;
                        for (int Y = 1; Y <= 16; Y++) // Draw each sky and torch light level.
                        {
                            //Pincel = new SolidBrush(Color.FromArgb(Alfa, (Rojo * Y) / 32, (Verde * Y) / 32, (Azul * Y) / 32));
                            Pincel = new SolidBrush(Color.FromArgb(Alfa, (Rojo * Y) / 256, (Verde * Y) / 256, (Azul * Y) / 256)); // Darker sky light at night.
                            Pintar_Mapa_Luz.FillRectangle(Pincel, 0, Y - 1, 16, 1);
                            //Pintar_Mapa_Luz.FillRectangle(Pincel, 32, Y + 15, 16, 1);
                            Pincel.Dispose();
                            Pincel = null;

                            if (!Variable_Cielo_Nublado) Pincel = new SolidBrush(Color.FromArgb(Alfa, (Rojo * Y) / 32, (Verde * Y) / 32, (Azul * Y) / 32)); // Medium sky light.
                            else Pincel = new SolidBrush(Color.FromArgb(Alfa, (Rojo * Y) / 64, (Verde * Y) / 64, (Azul * Y) / 64)); // Dark sky light.
                            Pintar_Mapa_Luz.FillRectangle(Pincel, 16, Y - 1, 48, 1);
                            Pincel.Dispose();
                            Pincel = null;

                            Pincel = new SolidBrush(Color.FromArgb(Alfa, (Rojo * Y) / 16, (Verde * Y) / 16, (Azul * Y) / 16)); // Torch light.
                            Pintar_Mapa_Luz.FillRectangle(Pincel, 0, Y + 15, 64, 1);
                            Pincel.Dispose();
                            Pincel = null;
                        }
                        Pincel = new SolidBrush(Color.FromArgb(Alfa, Rojo, Verde, Azul));
                        Pintar_Mapa_Luz.FillRectangle(Pincel, 0, 32, 64, 32); // Draw the night vision sky and torch levels.
                        Pincel.Dispose();
                        Pincel = null;
                        Pintar_Mapa_Luz.Dispose();
                        Pintar_Mapa_Luz = null;

                        Picture_Pack.Image = Variable_Imagen_Pack;
                        Picture_Alfa.Image = Imagen_Alfa;
                        Picture_Rojo.Image = Imagen_Rojo;
                        Picture_Verde.Image = Imagen_Verde;
                        Picture_Azul.Image = Imagen_Azul;
                        int Zoom;
                        Picture_Mapa_Luz.Image = Program.Obtener_Imagen_Autozoom(Variable_Imagen_Mapa_Luz, Picture_Mapa_Luz.ClientSize.Width, Picture_Mapa_Luz.ClientSize.Height, true, CheckState.Unchecked, out Zoom);
                    }
                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                        Ocupado = false; // Always reset at the end.
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
