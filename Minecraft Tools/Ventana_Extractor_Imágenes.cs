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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Extractor_Imágenes : Form
    {
        public Ventana_Extractor_Imágenes()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Image Extractor by Jupisoft for " + Program.Texto_Usuario;
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

        /// <summary>
        /// The maximum size any loaded image can have. Default is 100 MB. Higher values will need more RAM.
        /// </summary>
        internal static readonly long Tamaño_100_MB = 100L * 1024L * 1024L; // 100 bytes to KB and to MB.
        internal static readonly string Ruta_Extracción = Application.StartupPath + "\\Extracted Images";
        internal static bool Variable_Mostrar_Imágenes = false;
        internal static bool Variable_Archivos_Completos = true;
        internal static bool Variable_Extraer_Subcarpetas = false;
        internal static bool Variable_Mantener_Estructura = false;
        internal static string Variable_Ruta = null;
        internal bool Pendiente_Subproceso_Abortar = false;
        internal bool Subproceso_Activo = false;
        internal Thread Subproceso = null;

        private void Ventana_Extractor_Imágenes_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título + " - [The extracted images will be saved as PNG format]";
                this.WindowState = FormWindowState.Maximized;
                CheckBox_Mostrar_Imágenes.Checked = Variable_Mostrar_Imágenes;
                CheckBox_Archivos_Completos.Checked = Variable_Archivos_Completos;
                CheckBox_Extraer_Subcarpetas.Checked = Variable_Extraer_Subcarpetas;
                CheckBox_Mantener_Estructura.Checked = Variable_Mantener_Estructura;
                TextBox_Ruta.Text = Variable_Ruta;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Extractor_Imágenes_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Extractor_Imágenes_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Subproceso_Activo)
                {
                    e.Cancel = true;
                    Pendiente_Subproceso_Abortar = true; // First abort the thread.
                }
                else Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Extractor_Imágenes_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Extractor_Imágenes_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Extractor_Imágenes_DragDrop(object sender, DragEventArgs e)
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
                                    TextBox_Ruta.Text = Ruta;
                                    break;
                                }
                            }
                            catch (Exception Excepción)
                            {
                                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                                continue;
                            }
                        }
                        Matriz_Rutas = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Extractor_Imágenes_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Extractor_Imágenes_KeyDown(object sender, KeyEventArgs e)
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
                Program.Crear_Carpetas(Ruta_Extracción);
                Program.Ejecutar_Ruta(Ruta_Extracción, ProcessWindowStyle.Maximized);
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

        private void TextBox_Ruta_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Ruta = TextBox_Ruta.Text;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Mostrar_Imágenes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Mostrar_Imágenes = CheckBox_Mostrar_Imágenes.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Archivos_Completos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Archivos_Completos = CheckBox_Archivos_Completos.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Extraer_Subcarpetas_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Extraer_Subcarpetas = CheckBox_Extraer_Subcarpetas.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Mantener_Estructura_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Mantener_Estructura = CheckBox_Mantener_Estructura.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Extraer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Variable_Ruta) && (File.Exists(Variable_Ruta) || Directory.Exists(Variable_Ruta)))
                {
                    TextBox_Ruta.Enabled = false;
                    CheckBox_Mostrar_Imágenes.Enabled = false;
                    CheckBox_Archivos_Completos.Enabled = false;
                    CheckBox_Extraer_Subcarpetas.Enabled = false;
                    CheckBox_Mantener_Estructura.Enabled = false;
                    Botón_Extraer.Enabled = false;
                    Picture.Image = null;
                    Subproceso = new Thread(new ThreadStart(Subproceso_DoWork));
                    Subproceso.IsBackground = true;
                    Subproceso.Priority = ThreadPriority.Normal;
                    Subproceso.Start();
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Subproceso_DoWork()
        {
            Subproceso_Activo = true; // Thread started.
            int Total_Cabeceras = 0; // The total number of found headers.
            int Total_Imágenes = 0; // The total number of extracted images.
            try
            {
                // This tried to extract the temp iles made by Minecraft InfDev to get the chunks.
                // Result: failed, the changes on the map are lost when the chunks are unloaded.
                /*string Ruta_Save = Application.StartupPath + "\\Java";
                Program.Crear_Carpetas(Ruta_Save);
                string Ruta_Temp = @"C:\Users\Jupisoft\AppData\Local\Temp\imageio5730493780140380272.tmp";
                Dictionary<uint, byte[]> Diccionario_CRC_32 = new Dictionary<uint, byte[]>();
                for(; ; )
                {
                    if (Pendiente_Subproceso_Abortar) return;
                    this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, ((char)Program.Rand.Next((int)'A', (int)'Z' + 1)).ToString() });
                    if (File.Exists(Ruta_Temp))
                    {
                        FileStream Lector_Temp = new FileStream(Ruta_Temp, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                        if (Lector_Temp != null)
                        {
                            if (Lector_Temp.Length > 0L)
                            {
                                Lector_Temp.Seek(0L, SeekOrigin.Begin);
                                byte[] Matriz_Bytes_Temp = new byte[Lector_Temp.Length];
                                int Longitud = Lector_Temp.Read(Matriz_Bytes_Temp, 0, Matriz_Bytes_Temp.Length);
                                if (Matriz_Bytes_Temp.Length != Longitud) Array.Resize(ref Matriz_Bytes_Temp, Longitud);
                                uint CRC32 = Program.Calcular_CRC32(Matriz_Bytes_Temp);
                                if (!Diccionario_CRC_32.ContainsKey(CRC32))
                                {
                                    Diccionario_CRC_32.Add(CRC32, null);
                                    File.WriteAllBytes(Ruta_Save + "\\" + CRC32.ToString() + ".txt", Matriz_Bytes_Temp);
                                    SystemSounds.Asterisk.Play();
                                }
                                Matriz_Bytes_Temp = null;
                            }
                            Lector_Temp.Close();
                            Lector_Temp.Dispose();
                            Lector_Temp = null;
                        }
                    }
                }
                return;*/

                //Stopwatch Cronómetro = Stopwatch.StartNew(); // Keep track of the extraction time?
                string Nombre_Temporal = Program.Obtener_Nombre_Temporal();
                this.Invoke(new Invocación.Delegado_Control_Cursor(Invocación.Ejecutar_Delegado_Control_Cursor), new object[] { this, Cursors.WaitCursor });
                List<string> Lista_Rutas = new List<string>();
                this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [Found headers: " + Program.Traducir_Número(Total_Cabeceras) + ", Extracted images: " + Program.Traducir_Número(Total_Imágenes) + ", Indexing files...]" });
                if (File.Exists(Variable_Ruta)) Lista_Rutas.Add(Variable_Ruta); // Single file.
                else // Multiple files.
                {
                    string[] Matriz_Rutas = Directory.GetFiles(Variable_Ruta, "*", !Variable_Extraer_Subcarpetas ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories);
                    if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                    {
                        Lista_Rutas.AddRange(Matriz_Rutas);
                    }
                    Matriz_Rutas = null;
                }
                if (Lista_Rutas.Count > 0)
                {
                    long Tamaño_Actual = 0L;
                    long Tamaño_Total = 0L; // Get the total length of all the files to extract.
                    foreach (string Ruta in Lista_Rutas)
                    {
                        try
                        {
                            if (Pendiente_Subproceso_Abortar) return;
                            Tamaño_Total += new FileInfo(Ruta).Length; // Used to update the progress bar.
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                    }
                    // Now extract all the images from the files.
                    foreach (string Ruta in Lista_Rutas)
                    {
                        try
                        {
                            if (Pendiente_Subproceso_Abortar) return;
                            FileStream Lector = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            if (Lector != null && Lector.Length > 0L)
                            {
                                Lector.Seek(0L, SeekOrigin.Begin);
                                // Lists for the known image headers, up to 4 bytes each.
                                List<long> Lista_Marcadores_BMP_Apertura = new List<long>();
                                List<long> Lista_Marcadores_GIF_Apertura = new List<long>();
                                List<long> Lista_Marcadores_JPG_Apertura = new List<long>();
                                List<long> Lista_Marcadores_PNG_Apertura = new List<long>();
                                // The TGA format doesn't have a "constant" header, so ignore it.
                                List<long> Lista_Marcadores_TIF_Apertura = new List<long>();

                                int Cabeceras = 0;
                                byte[] Matriz_Bytes = new byte[4096];
                                for (long Índice_Posición = 0L; ;) // Read the whole file at 4K intervals.
                                {
                                    if (Pendiente_Subproceso_Abortar) return;
                                    Picture_Progreso.Invoke(new Invocación.Delegado_PictureBox_Image(Invocación.Ejecutar_Delegado_PictureBox_Image), new object[] { Picture_Progreso, Program.Obtener_Imagen_Barra_Progreso(Picture_Progreso.ClientSize, (int)((Tamaño_Actual * (long)Picture_Progreso.ClientSize.Width) / Tamaño_Total)) });
                                    int Longitud = Lector.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                    if (Longitud > 0)
                                    {
                                        // Now find any known image header supported by .NET.
                                        // This might be extended by using external image libraries.
                                        for (int Índice_Byte = 0; Índice_Byte < Longitud; Índice_Byte++)
                                        {
                                            if (Pendiente_Subproceso_Abortar) return;
                                            if (Índice_Byte + 2 < Longitud && // Search for "BM6".
                                                Matriz_Bytes[Índice_Byte] == 66 &&
                                                Matriz_Bytes[Índice_Byte + 1] == 77 &&
                                                Matriz_Bytes[Índice_Byte + 2] == 54 &&
                                                !Lista_Marcadores_GIF_Apertura.Contains(Índice_Posición + Índice_Byte))
                                            {
                                                Lista_Marcadores_BMP_Apertura.Add(Índice_Posición + Índice_Byte); // Found a BMP header.
                                                Cabeceras++;
                                                Total_Cabeceras++;
                                                this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [Found headers: " + Program.Traducir_Número(Total_Cabeceras) + ", Extracted images: " + Program.Traducir_Número(Total_Imágenes) + ", Searching images...]" });
                                            }

                                            if (Índice_Byte + 2 < Longitud && // Search for "GIF".
                                                Matriz_Bytes[Índice_Byte] == 71 &&
                                                Matriz_Bytes[Índice_Byte + 1] == 73 &&
                                                Matriz_Bytes[Índice_Byte + 2] == 70 &&
                                                //Matriz_Bytes[Índice_Byte + 3] == 56 &&
                                                !Lista_Marcadores_GIF_Apertura.Contains(Índice_Posición + Índice_Byte))
                                            {
                                                Lista_Marcadores_GIF_Apertura.Add(Índice_Posición + Índice_Byte); // Found a GIF header.
                                                Cabeceras++;
                                                Total_Cabeceras++;
                                                this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [Found headers: " + Program.Traducir_Número(Total_Cabeceras) + ", Extracted images: " + Program.Traducir_Número(Total_Imágenes) + ", Searching images...]" });
                                            }

                                            if (Índice_Byte + 2 < Longitud && // Search for "ÿØÿ".
                                                Matriz_Bytes[Índice_Byte] == 255 &&
                                                Matriz_Bytes[Índice_Byte + 1] == 216 &&
                                                Matriz_Bytes[Índice_Byte + 2] == 255 &&
                                                !Lista_Marcadores_JPG_Apertura.Contains(Índice_Posición + Índice_Byte))
                                            {
                                                Lista_Marcadores_JPG_Apertura.Add(Índice_Posición + Índice_Byte); // Found a JPG header.
                                                Cabeceras++;
                                                Total_Cabeceras++;
                                                this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [Found headers: " + Program.Traducir_Número(Total_Cabeceras) + ", Extracted images: " + Program.Traducir_Número(Total_Imágenes) + ", Searching images...]" });
                                            }

                                            if (Índice_Byte + 2 < Longitud && // Search for "~PNG".
                                                Matriz_Bytes[Índice_Byte] == 137 &&
                                                Matriz_Bytes[Índice_Byte + 1] == 80 &&
                                                Matriz_Bytes[Índice_Byte + 2] == 78 &&
                                                Matriz_Bytes[Índice_Byte + 3] == 71 &&
                                                !Lista_Marcadores_PNG_Apertura.Contains(Índice_Posición + Índice_Byte))
                                            {
                                                Lista_Marcadores_PNG_Apertura.Add(Índice_Posición + Índice_Byte); // Found a PNG header.
                                                Cabeceras++;
                                                Total_Cabeceras++;
                                                this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [Found headers: " + Program.Traducir_Número(Total_Cabeceras) + ", Extracted images: " + Program.Traducir_Número(Total_Imágenes) + ", Searching images...]" });
                                            }

                                            if (Índice_Byte + 2 < Longitud && // Search for "II*".
                                                Matriz_Bytes[Índice_Byte] == 73 &&
                                                Matriz_Bytes[Índice_Byte + 1] == 73 &&
                                                Matriz_Bytes[Índice_Byte + 2] == 42 &&
                                                !Lista_Marcadores_PNG_Apertura.Contains(Índice_Posición + Índice_Byte))
                                            {
                                                Lista_Marcadores_TIF_Apertura.Add(Índice_Posición + Índice_Byte); // Found a TIFF header.
                                                Cabeceras++;
                                                Total_Cabeceras++;
                                                this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [Found headers: " + Program.Traducir_Número(Total_Cabeceras) + ", Extracted images: " + Program.Traducir_Número(Total_Imágenes) + ", Searching images...]" });
                                            }
                                        }
                                        if (Lector.Position < Lector.Length)
                                        {
                                            // Go back 4 bytes each time to check all positions.
                                            Lector.Seek(Lector.Position - 4L, SeekOrigin.Begin); // For the next use.
                                            Índice_Posición += Longitud - 4;
                                            Tamaño_Actual += Longitud - 4;
                                        }
                                        else // End of file.
                                        {
                                            Índice_Posición += (long)Longitud;
                                            Tamaño_Actual += (long)Longitud;
                                        }
                                    }
                                    else break; // End of file.
                                }
                                if (Cabeceras > 0) // We found at least 1 image header.
                                {
                                    if (Lista_Marcadores_BMP_Apertura.Count > 1) Lista_Marcadores_BMP_Apertura.Sort();
                                    if (Lista_Marcadores_GIF_Apertura.Count > 1) Lista_Marcadores_GIF_Apertura.Sort();
                                    if (Lista_Marcadores_JPG_Apertura.Count > 1) Lista_Marcadores_JPG_Apertura.Sort();
                                    if (Lista_Marcadores_PNG_Apertura.Count > 1) Lista_Marcadores_PNG_Apertura.Sort();
                                    if (Lista_Marcadores_TIF_Apertura.Count > 1) Lista_Marcadores_TIF_Apertura.Sort();
                                    if (!Variable_Archivos_Completos)
                                    {
                                        // This line should avoid copying full files if they are
                                        // already valid images, although can't test total file length,
                                        // and if it starts with an image and has other things after it,
                                        // it will be lost during the extraction, so enable it to get it.
                                        while (Lista_Marcadores_BMP_Apertura.Contains(0L)) Lista_Marcadores_BMP_Apertura.Remove(0L);
                                        while (Lista_Marcadores_GIF_Apertura.Contains(0L)) Lista_Marcadores_GIF_Apertura.Remove(0L);
                                        while (Lista_Marcadores_JPG_Apertura.Contains(0L)) Lista_Marcadores_JPG_Apertura.Remove(0L);
                                        while (Lista_Marcadores_PNG_Apertura.Contains(0L)) Lista_Marcadores_PNG_Apertura.Remove(0L);
                                        while (Lista_Marcadores_TIF_Apertura.Contains(0L)) Lista_Marcadores_TIF_Apertura.Remove(0L);
                                    }
                                    List<List<long>> Lista_Marcadores_Apertura = new List<List<long>>();
                                    Lista_Marcadores_Apertura.Add(Lista_Marcadores_BMP_Apertura);
                                    Lista_Marcadores_Apertura.Add(Lista_Marcadores_GIF_Apertura);
                                    Lista_Marcadores_Apertura.Add(Lista_Marcadores_JPG_Apertura);
                                    Lista_Marcadores_Apertura.Add(Lista_Marcadores_PNG_Apertura);
                                    Lista_Marcadores_Apertura.Add(Lista_Marcadores_TIF_Apertura);
                                    //if (Lista_Marcadores_Apertura.Count > 1) Lista_Marcadores_Apertura.Sort();
                                    // Mixing the formats might not always work for all images if one fails.
                                    for (int Índice_Lista = 0; Índice_Lista < Lista_Marcadores_Apertura.Count; Índice_Lista++)
                                    {
                                        for (int Índice_Apertura = 0; Índice_Apertura < Lista_Marcadores_Apertura[Índice_Lista].Count; Índice_Apertura++)
                                        {
                                            if (Pendiente_Subproceso_Abortar) return;
                                            // Read until the start of the next found image, or if there aren't more,
                                            // read until the end of the file, but keeping the size below 100 MB.
                                            // Test results: some images were missing, so I decided to read up to
                                            // 100 MB for each found header, and it worked, but more surprisingly
                                            // it still was working fast enough, and finally even the JPEG thumbnails
                                            // were exported without errors, so at the end I let this new code below.
                                            //Matriz_Bytes = new byte[Math.Min((Índice_Apertura + 1 < Lista_Marcadores_Apertura[Índice_Lista].Count && !Variable_Archivos_Completos ? Lista_Marcadores_Apertura[Índice_Lista][Índice_Apertura + 1] : Lector.Length) - Lista_Marcadores_Apertura[Índice_Lista][Índice_Apertura], Tamaño_100_MB)];
                                            Matriz_Bytes = new byte[Math.Min(Lector.Length - Lista_Marcadores_Apertura[Índice_Lista][Índice_Apertura], Tamaño_100_MB)];
                                            Lector.Seek(Lista_Marcadores_Apertura[Índice_Lista][Índice_Apertura], SeekOrigin.Begin);
                                            Lector.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                            Image Imagen_Original = null;
                                            try
                                            {
                                                Imagen_Original = Image.FromStream(new MemoryStream(Matriz_Bytes, true), false, false);
                                                // Warning: this commented code will fail since closing the memory stream
                                                // seems to damage the image when drawing it into a new copy. So remember
                                                // this problem and try to avoid it in the future.
                                                /*MemoryStream Lector_Memoria = new MemoryStream(Matriz_Bytes, true);
                                                if (Lector_Memoria != null)
                                                {
                                                    Imagen_Original = Image.FromStream(Lector_Memoria, false, false);
                                                    Lector_Memoria.Close();
                                                    Lector_Memoria.Dispose();
                                                    Lector_Memoria = null;
                                                }*/
                                            }
                                            catch { Imagen_Original = null; }
                                            if (Imagen_Original != null && Imagen_Original.Width > 0 && Imagen_Original.Height > 0)
                                            {
                                                try
                                                {
                                                    if (Pendiente_Subproceso_Abortar) return;
                                                    // We have extracted a real image, now export it.
                                                    int Ancho = Imagen_Original.Width;
                                                    int Alto = Imagen_Original.Height;
                                                    Bitmap Imagen = new Bitmap(Ancho, Alto, Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb);
                                                    Graphics Pintar = Graphics.FromImage(Imagen);
                                                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                                                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                                    Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                                    Pintar.SmoothingMode = SmoothingMode.HighQuality;
                                                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                                                    // This "copy" will remove any excess of bytes at the end of the image.
                                                    Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                                    Pintar.Dispose();
                                                    Pintar = null;
                                                    MemoryStream Lector_Memoria = new MemoryStream();
                                                    Imagen.Save(Lector_Memoria, ImageFormat.Png); // Save the image in the memory.
                                                    // Always use ".ToArray()" to avoid MemoryStream random byte errors.
                                                    // This took me a few years to notice, but this application should be free of that bug.
                                                    Matriz_Bytes = Lector_Memoria.ToArray(); // Get the bytes of the saved image.
                                                    uint CRC_32 = Program.Calcular_CRC32(Matriz_Bytes); // Get the CRC 32 of the bytes.
                                                    string Texto_CRC_32 = Convert.ToString(CRC_32, 16).ToUpperInvariant();
                                                    while (Texto_CRC_32.Length < 8) Texto_CRC_32 = '0' + Texto_CRC_32;
                                                    string Ruta_Salida = Ruta_Extracción + "\\" + Nombre_Temporal + (Variable_Mantener_Estructura ? "\\" + Path.GetFileName(Ruta) : null);
                                                    Program.Crear_Carpetas(Ruta_Salida);
                                                    Ruta_Salida += "\\" + Texto_CRC_32 + ".png";
                                                    if (!File.Exists(Ruta_Salida))
                                                    {
                                                        // Write the actual image as a byte array.
                                                        FileStream Lector_Salida = new FileStream(Ruta_Salida, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                                        Lector_Salida.SetLength(0L);
                                                        Lector_Salida.Seek(0L, SeekOrigin.Begin);
                                                        Lector_Salida.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                                        Lector_Salida.Close();
                                                        Lector_Salida.Dispose();
                                                        Lector_Salida = null;
                                                        Total_Imágenes++;
                                                        this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [Found headers: " + Program.Traducir_Número(Total_Cabeceras) + ", Extracted images: " + Program.Traducir_Número(Total_Imágenes) + ", Searching images...]" });
                                                    }
                                                    if (!Variable_Mostrar_Imágenes)
                                                    {
                                                        Imagen.Dispose();
                                                        Imagen = null;
                                                    }
                                                    else
                                                    {
                                                        // Instead of deleting the image, show it on the main picture box.
                                                        Picture.Invoke(new Invocación.Delegado_PictureBox_SizeMode(Invocación.Ejecutar_Delegado_PictureBox_SizeMode), new object[] { Picture, Ancho > Picture.ClientSize.Width || Alto > Picture.ClientSize.Height ? PictureBoxSizeMode.Zoom : PictureBoxSizeMode.CenterImage });
                                                        Picture.Invoke(new Invocación.Delegado_PictureBox_Image(Invocación.Ejecutar_Delegado_PictureBox_Image), new object[] { Picture, Imagen });
                                                    }
                                                }
                                                catch { }
                                                Imagen_Original.Dispose();
                                                Imagen_Original = null;
                                            }
                                        }
                                    }
                                    Lista_Marcadores_Apertura = null;
                                }
                                Lista_Marcadores_BMP_Apertura = null;
                                Lista_Marcadores_GIF_Apertura = null;
                                Lista_Marcadores_JPG_Apertura = null;
                                Lista_Marcadores_PNG_Apertura = null;
                                Lista_Marcadores_TIF_Apertura = null;
                                Matriz_Bytes = null;
                                Lector.Close();
                                Lector.Dispose();
                                Lector = null;
                            }
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                    }
                    if (Directory.Exists(Ruta_Extracción + "\\" + Nombre_Temporal))
                    {
                        Program.Ejecutar_Ruta(Ruta_Extracción + "\\" + Nombre_Temporal, ProcessWindowStyle.Maximized);
                        SystemSounds.Asterisk.Play(); // We found something.
                    }
                    else SystemSounds.Beep.Play(); // We found nothing.
                }
                else SystemSounds.Beep.Play(); // We found nothing.
                Lista_Rutas = null;
            }
            catch (ThreadAbortException) { }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally
            {
                this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [Found headers: " + Program.Traducir_Número(Total_Cabeceras) + ", Extracted images: " + Program.Traducir_Número(Total_Imágenes) + ", Done]" });
                Picture.Invoke(new Invocación.Delegado_PictureBox_Image(Invocación.Ejecutar_Delegado_PictureBox_Image), new object[] { Picture, null });
                Picture_Progreso.Invoke(new Invocación.Delegado_PictureBox_Image(Invocación.Ejecutar_Delegado_PictureBox_Image), new object[] { Picture_Progreso, null });
                this.Invoke(new Invocación.Delegado_Control_Cursor(Invocación.Ejecutar_Delegado_Control_Cursor), new object[] { this, Cursors.Default });
                if (Pendiente_Subproceso_Abortar)
                {
                    Pendiente_Subproceso_Abortar = false;
                    Subproceso_Activo = false;
                    Subproceso = null;
                    this.Invoke(new Invocación.Delegado_Form_Close(Invocación.Ejecutar_Delegado_Form_Close), new object[] { this });
                }
                else
                {
                    Pendiente_Subproceso_Abortar = false;
                    Subproceso_Activo = false;
                    Subproceso = null;
                    this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { TextBox_Ruta, true });
                    this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { CheckBox_Mostrar_Imágenes, true });
                    this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { CheckBox_Archivos_Completos, true });
                    this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { CheckBox_Extraer_Subcarpetas, true });
                    this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { CheckBox_Mantener_Estructura, true });
                    this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { Botón_Extraer, true });
                    this.Invoke(new Invocación.Delegado_Control_Select(Invocación.Ejecutar_Delegado_Control_Select), new object[] { Botón_Extraer });
                    this.Invoke(new Invocación.Delegado_Control_Focus(Invocación.Ejecutar_Delegado_Control_Focus), new object[] { Botón_Extraer });
                }
            }
        }
    }
}
