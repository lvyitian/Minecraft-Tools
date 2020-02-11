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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Buscador_Números_Primos : Form
    {
        public Ventana_Buscador_Números_Primos()
        {
            InitializeComponent();
        }

        internal static readonly string Ruta_Números_Primos = Application.StartupPath + "\\Numbers";
        internal static readonly string Ruta_Números_Divisibles_Base_Datos = Ruta_Números_Primos + "\\Divisibles";
        internal static readonly string Ruta_Números_Primos_Base_Datos = Ruta_Números_Primos + "\\Primes";
        internal readonly string Texto_Título = "Prime Numbers Finder by Jupisoft for " + Program.Texto_Usuario;
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
        internal int Último_Número_Primo = -1;
        internal int Índice_Número_Primo_Actual = 2;
        Stopwatch Cronómetro_Números_Primos = new Stopwatch();
        internal int Total_Números_Primos_Números = 0;
        internal int Total_Números_Primos_Primos = 0;
        internal bool Pendiente_Subproceso_Abortar = false;
        internal bool Pendiente_Subproceso_Abortar_Ventana = false;
        internal bool Subproceso_Activo = false;
        internal Thread Subproceso = null;
        internal Dictionary<int, object> Diccionario_Números_Primos = new Dictionary<int, object>();
        internal FileStream Lector_Números_Primos = null;
        internal BinaryWriter Lector_Salida_Números_Primos = null;
        internal Dictionary<int, byte> Diccionario_Números_Divisibles = new Dictionary<int, byte>();
        internal FileStream Lector_Números_Divisibles = null;
        internal BinaryWriter Lector_Salida_Números_Divisibles = null;

        private void Ventana_Buscador_Números_Primos_Load(object sender, EventArgs e)
        {
            try
            {
                /*// Test that took only a few seconds!
                List<int> Lista_i = new List<int>();
                int a = 1737747772; //1073741824; // 360 = 24. // 1530 = 24.
                int b = 0;
                for (int i = 1; i < a; i++)
                {
                    if (a % i == 0)
                    {
                        Lista_i.Add(i);
                        b++;
                    }
                }
                MessageBox.Show(Program.Traducir_Lista_Variables(Lista_i.ToArray()), b.ToString());*/
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                Program.Crear_Carpetas(Ruta_Números_Primos);
                Lector_Números_Primos = new FileStream(Ruta_Números_Primos_Base_Datos, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                if (Lector_Números_Primos != null)
                {
                    Lector_Números_Primos.Seek(0L, SeekOrigin.Begin);
                    if (Lector_Números_Primos.Length > 0L)
                    {
                        if (Lector_Números_Primos.Length % 4L != 0)
                        {
                            // The data base is damaged, so just delete the last number to keep in sync.
                            Lector_Números_Primos.SetLength(Lector_Números_Primos.Length - (Lector_Números_Primos.Length % 4L));
                            Lector_Números_Primos.Seek(0L, SeekOrigin.Begin);
                        }
                        if (Lector_Números_Primos.Length > 0L)
                        {
                            int Número_Primo_Máximo = int.MinValue;
                            BinaryReader Lector_Entrada_Números_Primos = new BinaryReader(Lector_Números_Primos, Encoding.ASCII, true);
                            while (Lector_Números_Primos.Position < Lector_Números_Primos.Length)
                            {
                                int Número_Primo = Lector_Entrada_Números_Primos.ReadInt32();
                                Diccionario_Números_Primos.Add(Número_Primo, null);
                                if (Número_Primo > Número_Primo_Máximo)
                                {
                                    Número_Primo_Máximo = Número_Primo;
                                }
                                //TextBox_Números_Primos.Text += Número_Primo.ToString() + ", ";
                            }
                            Índice_Número_Primo_Actual = Número_Primo_Máximo + 1; // Always add one.
                            TextBox_Número_Actual.Text = Program.Traducir_Número(Índice_Número_Primo_Actual);
                            Último_Número_Primo = Número_Primo_Máximo;
                            TextBox_Último_Primo.Text = Program.Traducir_Número(Último_Número_Primo);
                            Lector_Entrada_Números_Primos.Close();
                            Lector_Entrada_Números_Primos.Dispose();
                            Lector_Entrada_Números_Primos = null;
                        }
                    }
                    Lector_Números_Primos.Seek(Lector_Números_Primos.Length, SeekOrigin.Begin);
                    Lector_Salida_Números_Primos = new BinaryWriter(Lector_Números_Primos, Encoding.ASCII, true);
                }
                TextBox_Números_Encontrados.Text = Program.Traducir_Número(Diccionario_Números_Divisibles.Count);
                /*Lector_Números_Divisibles = new FileStream(Ruta_Números_Divisibles_Base_Datos, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                if (Lector_Números_Divisibles != null)
                {
                    Lector_Números_Divisibles.Seek(0L, SeekOrigin.Begin);
                    if (Lector_Números_Divisibles.Length > 0L)
                    {
                        if (Lector_Números_Divisibles.Length > 0L)
                        {
                            long Número_Divisible_Máximo = long.MinValue;
                            BinaryReader Lector_Entrada_Números_Divisibles = new BinaryReader(Lector_Números_Divisibles, Encoding.ASCII, true);
                            while (Lector_Números_Divisibles.Position < Lector_Números_Divisibles.Length)
                            {
                                int Número_Divisible = (int)Lector_Números_Divisibles.Position;
                                byte Número_Divisores = Lector_Entrada_Números_Divisibles.ReadByte();
                                Diccionario_Números_Divisibles.Add(Número_Divisible, Número_Divisores);
                                if (Número_Divisible > Número_Divisible_Máximo)
                                {
                                    Número_Divisible_Máximo = Número_Divisible;
                                }
                                //TextBox_Números_Divisibles.Text += Número_Primo.ToString() + ", ";
                            }
                            NumericUpDown_Número_Actual.Value = Número_Divisible_Máximo + 1; // Always add one.
                            NumericUpDown_Último_Primo.Value = Número_Divisible_Máximo;
                            Lector_Entrada_Números_Divisibles.Close();
                            Lector_Entrada_Números_Divisibles.Dispose();
                            Lector_Entrada_Números_Divisibles = null;
                        }
                    }
                    Lector_Números_Divisibles.Seek(Lector_Números_Divisibles.Length, SeekOrigin.Begin);
                    Lector_Salida_Números_Divisibles = new BinaryWriter(Lector_Números_Divisibles, Encoding.ASCII, true);
                }
                //NumericUpDown_Números_Encontrados.Value = Diccionario_Números_Divisibles.Count;*/
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Buscador_Números_Primos_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Buscador_Números_Primos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Subproceso_Activo)
                {
                    e.Cancel = true;
                    Pendiente_Subproceso_Abortar = true; // First abort the thread.
                    Pendiente_Subproceso_Abortar_Ventana = true; // And also close the window.
                }
                else
                {
                    Temporizador_Principal.Stop();
                    if (Lector_Salida_Números_Primos != null)
                    {
                        Lector_Salida_Números_Primos.Close();
                        Lector_Salida_Números_Primos.Dispose();
                        Lector_Salida_Números_Primos = null;
                    }
                    if (Lector_Números_Primos != null)
                    {
                        Lector_Números_Primos.Close();
                        Lector_Números_Primos.Dispose();
                        Lector_Números_Primos = null;
                    }
                    if (Diccionario_Números_Primos != null) Diccionario_Números_Primos = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Buscador_Números_Primos_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Buscador_Números_Primos_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Buscador_Números_Primos_DragDrop(object sender, DragEventArgs e)
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
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                        }
                        Matriz_Rutas = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Buscador_Números_Primos_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Buscador_Números_Primos_KeyDown(object sender, KeyEventArgs e)
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

        private void Botón_Buscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Subproceso_Activo)
                {
                    Botón_Divisibles_Buscar.Enabled = false;
                    Subproceso = new Thread(new ThreadStart(Subproceso_DoWork));
                    Subproceso.IsBackground = true;
                    Subproceso.Priority = ThreadPriority.Normal;
                    Subproceso.Start();
                    Cronómetro_Números_Primos.Restart();
                    Temporizador_Primos_Tick(Temporizador_Primos, EventArgs.Empty);
                    Temporizador_Primos.Start();
                    Botón_Buscar.Image = Resources.Cancelar;
                    Botón_Buscar.Text = " Cancel ";
                }
                else
                {
                    Pendiente_Subproceso_Abortar = true; // Only abort the thread.
                    Pendiente_Subproceso_Abortar_Ventana = false; // Don't close the window.
                    Botón_Buscar.Image = Resources.Buscar;
                    Botón_Buscar.Text = " Find primes... ";
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
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Números_Primos);
                Program.Ejecutar_Ruta(Program.Ruta_Guardado_Imágenes_Números_Primos, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Dibujar_Imagen_Primos_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (Diccionario_Números_Primos != null && Diccionario_Números_Primos.Count > 0)
                {
                    double Raíz_Cuadrada_Números = Math.Sqrt((double)Índice_Número_Primo_Actual);
                    int Raíz_Cuadrada;
                    if (Raíz_Cuadrada_Números - Math.Truncate(Raíz_Cuadrada_Números) > 0d)
                    {
                        Raíz_Cuadrada = (int)(Math.Truncate(Raíz_Cuadrada_Números) + 1d);
                    }
                    else Raíz_Cuadrada = (int)Math.Truncate(Raíz_Cuadrada_Números);
                    int Ancho_Alto = 24576;
                    if (Raíz_Cuadrada < 24576)
                    {
                        for (int Índice = 16384; Índice >= 16; Índice /= 2)
                        {
                            if (Raíz_Cuadrada >= Índice)
                            {
                                Ancho_Alto = Índice;
                                break;
                            }
                        }
                    }
                    //int Ancho_Alto = 24576; // 16384. // 24.576 x 24.576 = 603.979.776 pixels.
                    //Ancho_Alto = 2048; // 1024.
                    int Ancho = Ancho_Alto;
                    int Alto = Ancho_Alto;
                    long Píxeles = Ancho * Alto;
                    int Longitud = 1;
                    //int Longitud_Máxima = 512;
                    int X = Ancho / 2;
                    int Y = (Alto / 2) - 1;
                    Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format24bppRgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.None;
                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                    Pintar.Clear(Color.White);
                    // Draw 2 lines around the center of the image like the "obsidian walls" of InfDev.
                    SolidBrush Pincel = new SolidBrush(Color.FromArgb(255, 224, 224, 224));
                    Pintar.FillRectangle(Pincel, 0, Alto / 2, Ancho, 1); // X.
                    Pintar.FillRectangle(Pincel, Ancho / 2, 0, 1, Alto); // Y.
                    Pincel.Dispose();
                    Pincel = null;
                    // Draw 2 extra diagonal lines that meet at the center of the image.
                    Pen Lápiz = new Pen(Color.FromArgb(255, 224, 224, 224));
                    Pintar.DrawLine(Lápiz, 0, 0, Alto, Ancho); // X.
                    Pintar.DrawLine(Lápiz, Ancho + 1, 0, 1, Alto); // Y.
                    //Pintar.DrawLine(Lápiz, Ancho, 0, 0, Alto); // Y.
                    Lápiz.Dispose();
                    Lápiz = null;
                    Pintar.Dispose();
                    Pintar = null;
                    BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                    int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                    bool Imagen_Alfa = Image.IsAlphaPixelFormat(Imagen.PixelFormat);
                    int Bytes_Aumento = !Imagen_Alfa ? 3 : 4;
                    int Bytes_Ancho = Math.Abs(Bitmap_Data.Stride);
                    int Bytes_Diferencia = Ancho_Stride - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                    byte[] Matriz_Bytes_Píxeles = new byte[Ancho_Stride * Alto];
                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes_Píxeles, 0, Matriz_Bytes_Píxeles.Length);
                    try
                    {
                        // Use try-catch here to show at least the part correctly done (hopefully everything).
                        int Índice;
                        int Índice_Arco_Iris = 0;
                        Color Color_Arco_Iris = Color.Black;
                        // I started this loop at 1 by default, but after doing several tests now I
                        // believe it should always start at zero since there are 4 "straight lines"
                        // without prime numbers, 2 horizontal and 2 vertical that change it's
                        // positions if the index starts at 1, 2, 3 or even -1, but when it's zero
                        // then the 2 horizontal and the 2 vertical lines meet each with other, which
                        // makes everything look more "complete" and there are also a lot of diagonal
                        // lines that also seem to meet each other when the index is zero, so use it.
                        for (int Índice_Número = 0; Índice_Número < Píxeles; Longitud += 2)
                        {
                            for (int Índice_X = 0; Índice_X < Longitud; Índice_X++, Índice_Número++) // Down.
                            {
                                Y++;
                                if (Diccionario_Números_Primos.ContainsKey(Índice_Número))
                                {
                                    Índice = (Y * Bytes_Ancho) + (X  * Bytes_Aumento);
                                    //Color_Arco_Iris = Program.Obtener_Color_Puro_1530(Índice_Arco_Iris);
                                    Color_Arco_Iris = Program.Matriz_Colores_Arco_Iris_8_Números_Primos[Índice_Arco_Iris];
                                    //Índice_Arco_Iris++;
                                    //if (Índice_Arco_Iris >= 16) Índice_Arco_Iris = 0;
                                    //if (Índice_Arco_Iris >= 1530) Índice_Arco_Iris = 0;
                                    Matriz_Bytes_Píxeles[Índice + 2] = Color_Arco_Iris.R;
                                    Matriz_Bytes_Píxeles[Índice + 1] = Color_Arco_Iris.G;
                                    Matriz_Bytes_Píxeles[Índice] = Color_Arco_Iris.B;
                                    //Matriz_Bytes_Píxeles[Índice + 2] = 0;
                                    //Matriz_Bytes_Píxeles[Índice + 1] = 0;
                                    //Matriz_Bytes_Píxeles[Índice] = 0;
                                    //Matriz_Bytes_Píxeles[Índice + 2] = Program.Matriz_Colores_Arco_Iris_256[Índice_Arco_Iris].R;
                                    //Matriz_Bytes_Píxeles[Índice + 1] = Program.Matriz_Colores_Arco_Iris_256[Índice_Arco_Iris].G;
                                    //Matriz_Bytes_Píxeles[Índice] = Program.Matriz_Colores_Arco_Iris_256[Índice_Arco_Iris].B;
                                }
                                Índice_Arco_Iris++;
                                if (Índice_Arco_Iris >= Program.Matriz_Colores_Arco_Iris_8_Números_Primos.Length) Índice_Arco_Iris = 0;
                            }
                            //Índice_Arco_Iris++;
                            //if (Índice_Arco_Iris >= 256) Índice_Arco_Iris = 0;

                            for (int Índice_Y = 0; Índice_Y < Longitud; Índice_Y++, Índice_Número++) // Left.
                            {
                                X--;
                                if (Diccionario_Números_Primos.ContainsKey(Índice_Número))
                                {
                                    Índice = (Y * Bytes_Ancho) + (X * Bytes_Aumento);
                                    Color_Arco_Iris = Program.Matriz_Colores_Arco_Iris_8_Números_Primos[Índice_Arco_Iris];
                                    Matriz_Bytes_Píxeles[Índice + 2] = Color_Arco_Iris.R;
                                    Matriz_Bytes_Píxeles[Índice + 1] = Color_Arco_Iris.G;
                                    Matriz_Bytes_Píxeles[Índice] = Color_Arco_Iris.B;
                                }
                                Índice_Arco_Iris++;
                                if (Índice_Arco_Iris >= Program.Matriz_Colores_Arco_Iris_8_Números_Primos.Length) Índice_Arco_Iris = 0;
                            }
                            //Índice_Arco_Iris++;
                            //if (Índice_Arco_Iris >= 256) Índice_Arco_Iris = 0;

                            for (int Índice_X = 0; Índice_X < Longitud; Índice_X++, Índice_Número++) // Up.
                            {
                                Y--;
                                if (Diccionario_Números_Primos.ContainsKey(Índice_Número))
                                {
                                    Índice = (Y * Bytes_Ancho) + (X * Bytes_Aumento);
                                    Color_Arco_Iris = Program.Matriz_Colores_Arco_Iris_8_Números_Primos[Índice_Arco_Iris];
                                    Matriz_Bytes_Píxeles[Índice + 2] = Color_Arco_Iris.R;
                                    Matriz_Bytes_Píxeles[Índice + 1] = Color_Arco_Iris.G;
                                    Matriz_Bytes_Píxeles[Índice] = Color_Arco_Iris.B;
                                }
                                Índice_Arco_Iris++;
                                if (Índice_Arco_Iris >= Program.Matriz_Colores_Arco_Iris_8_Números_Primos.Length) Índice_Arco_Iris = 0;
                            }
                            //Índice_Arco_Iris++;
                            //if (Índice_Arco_Iris >= 256) Índice_Arco_Iris = 0;

                            for (int Índice_Y = 0; Índice_Y < Longitud; Índice_Y++, Índice_Número++) // Right.
                            {
                                X++;
                                if (Diccionario_Números_Primos.ContainsKey(Índice_Número))
                                {
                                    Índice = (Y * Bytes_Ancho) + (X * Bytes_Aumento);
                                    Color_Arco_Iris = Program.Matriz_Colores_Arco_Iris_8_Números_Primos[Índice_Arco_Iris];
                                    Matriz_Bytes_Píxeles[Índice + 2] = Color_Arco_Iris.R;
                                    Matriz_Bytes_Píxeles[Índice + 1] = Color_Arco_Iris.G;
                                    Matriz_Bytes_Píxeles[Índice] = Color_Arco_Iris.B;
                                }
                                Índice_Arco_Iris++;
                                if (Índice_Arco_Iris >= Program.Matriz_Colores_Arco_Iris_8_Números_Primos.Length) Índice_Arco_Iris = 0;
                            }
                            //Índice_Arco_Iris++;
                            //if (Índice_Arco_Iris >= 256) Índice_Arco_Iris = 0;

                            // Move 1 pixel to the right and up.
                            X++;
                            Y--;
                        }
                    }
                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                    Marshal.Copy(Matriz_Bytes_Píxeles, 0, Bitmap_Data.Scan0, Matriz_Bytes_Píxeles.Length);
                    Imagen.UnlockBits(Bitmap_Data);
                    Matriz_Bytes_Píxeles = null;
                    Picture.Image = Imagen;
                    GC.Collect();
                    GC.GetTotalMemory(true);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Dibujar_Imagen_Divisibles_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (Diccionario_Números_Divisibles != null && Diccionario_Números_Divisibles.Count > 0)
                {
                    double Raíz_Cuadrada_Números = Math.Sqrt((double)Índice_Número_Primo_Actual);
                    int Raíz_Cuadrada;
                    if (Raíz_Cuadrada_Números - Math.Truncate(Raíz_Cuadrada_Números) > 0d)
                    {
                        Raíz_Cuadrada = (int)(Math.Truncate(Raíz_Cuadrada_Números) + 1d);
                    }
                    else Raíz_Cuadrada = (int)Math.Truncate(Raíz_Cuadrada_Números);
                    int Ancho_Alto = 24576;
                    if (Raíz_Cuadrada < 24576)
                    {
                        for (int Índice = 16384; Índice >= 16; Índice /= 2)
                        {
                            if (Raíz_Cuadrada >= Índice)
                            {
                                Ancho_Alto = Índice;
                                break;
                            }
                        }
                    }
                    //int Ancho_Alto = 24576; // 16384. // 24.576 x 24.576 = 603.979.776 pixels.
                    //Ancho_Alto = 2048; // 1024.
                    int Ancho = Ancho_Alto;
                    int Alto = Ancho_Alto;
                    long Píxeles = Ancho * Alto;
                    int Longitud = 1;
                    //int Longitud_Máxima = 512;
                    int X = Ancho / 2;
                    int Y = (Alto / 2) - 1;
                    Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format24bppRgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.None;
                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                    Pintar.Clear(Color.White);
                    // Draw 2 lines around the center of the image like the "obsidian walls" of InfDev.
                    SolidBrush Pincel = new SolidBrush(Color.FromArgb(255, 224, 224, 224));
                    Pintar.FillRectangle(Pincel, 0, Alto / 2, Ancho, 1); // X.
                    Pintar.FillRectangle(Pincel, Ancho / 2, 0, 1, Alto); // Y.
                    Pincel.Dispose();
                    Pincel = null;
                    // Draw 2 extra diagonal lines that meet at the center of the image.
                    Pen Lápiz = new Pen(Color.FromArgb(255, 224, 224, 224));
                    Pintar.DrawLine(Lápiz, 0, 0, Alto, Ancho); // X.
                    Pintar.DrawLine(Lápiz, Ancho + 1, 0, 1, Alto); // Y.
                    //Pintar.DrawLine(Lápiz, Ancho, 0, 0, Alto); // Y.
                    Lápiz.Dispose();
                    Lápiz = null;
                    Pintar.Dispose();
                    Pintar = null;
                    BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                    int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                    bool Imagen_Alfa = Image.IsAlphaPixelFormat(Imagen.PixelFormat);
                    int Bytes_Aumento = !Imagen_Alfa ? 3 : 4;
                    int Bytes_Ancho = Math.Abs(Bitmap_Data.Stride);
                    int Bytes_Diferencia = Ancho_Stride - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                    byte[] Matriz_Bytes_Píxeles = new byte[Ancho_Stride * Alto];
                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes_Píxeles, 0, Matriz_Bytes_Píxeles.Length);
                    try
                    {
                        // Use try-catch here to show at least the part correctly done (hopefully everything).
                        int Índice;
                        int Índice_Arco_Iris = 0;
                        Color Color_Arco_Iris = Color.Black;
                        // I started this loop at 1 by default, but after doing several tests now I
                        // believe it should always start at zero since there are 4 "straight lines"
                        // without prime numbers, 2 horizontal and 2 vertical that change it's
                        // positions if the index starts at 1, 2, 3 or even -1, but when it's zero
                        // then the 2 horizontal and the 2 vertical lines meet each with other, which
                        // makes everything look more "complete" and there are also a lot of diagonal
                        // lines that also seem to meet each other when the index is zero, so use it.
                        for (int Índice_Número = 0; Índice_Número < Píxeles; Longitud += 2)
                        {
                            for (int Índice_X = 0; Índice_X < Longitud; Índice_X++, Índice_Número++) // Down.
                            {
                                Y++;
                                if (Diccionario_Números_Divisibles.ContainsKey(Índice_Número))
                                {
                                    Índice = (Y * Bytes_Ancho) + (X * Bytes_Aumento);
                                    //Color_Arco_Iris = Program.Obtener_Color_Puro_1530(Índice_Arco_Iris);
                                    Color_Arco_Iris = Program.Matriz_Colores_Arco_Iris_8_Números_Primos[Índice_Arco_Iris];
                                    //Índice_Arco_Iris++;
                                    //if (Índice_Arco_Iris >= 16) Índice_Arco_Iris = 0;
                                    //if (Índice_Arco_Iris >= 1530) Índice_Arco_Iris = 0;
                                    Matriz_Bytes_Píxeles[Índice + 2] = Color_Arco_Iris.R;
                                    Matriz_Bytes_Píxeles[Índice + 1] = Color_Arco_Iris.G;
                                    Matriz_Bytes_Píxeles[Índice] = Color_Arco_Iris.B;
                                    //Matriz_Bytes_Píxeles[Índice + 2] = 0;
                                    //Matriz_Bytes_Píxeles[Índice + 1] = 0;
                                    //Matriz_Bytes_Píxeles[Índice] = 0;
                                    //Matriz_Bytes_Píxeles[Índice + 2] = Program.Matriz_Colores_Arco_Iris_256[Índice_Arco_Iris].R;
                                    //Matriz_Bytes_Píxeles[Índice + 1] = Program.Matriz_Colores_Arco_Iris_256[Índice_Arco_Iris].G;
                                    //Matriz_Bytes_Píxeles[Índice] = Program.Matriz_Colores_Arco_Iris_256[Índice_Arco_Iris].B;
                                }
                                Índice_Arco_Iris++;
                                if (Índice_Arco_Iris >= Program.Matriz_Colores_Arco_Iris_8_Números_Primos.Length) Índice_Arco_Iris = 0;
                            }
                            //Índice_Arco_Iris++;
                            //if (Índice_Arco_Iris >= 256) Índice_Arco_Iris = 0;

                            for (int Índice_Y = 0; Índice_Y < Longitud; Índice_Y++, Índice_Número++) // Left.
                            {
                                X--;
                                if (Diccionario_Números_Divisibles.ContainsKey(Índice_Número))
                                {
                                    Índice = (Y * Bytes_Ancho) + (X * Bytes_Aumento);
                                    Color_Arco_Iris = Program.Matriz_Colores_Arco_Iris_8_Números_Primos[Índice_Arco_Iris];
                                    Matriz_Bytes_Píxeles[Índice + 2] = Color_Arco_Iris.R;
                                    Matriz_Bytes_Píxeles[Índice + 1] = Color_Arco_Iris.G;
                                    Matriz_Bytes_Píxeles[Índice] = Color_Arco_Iris.B;
                                }
                                Índice_Arco_Iris++;
                                if (Índice_Arco_Iris >= Program.Matriz_Colores_Arco_Iris_8_Números_Primos.Length) Índice_Arco_Iris = 0;
                            }
                            //Índice_Arco_Iris++;
                            //if (Índice_Arco_Iris >= 256) Índice_Arco_Iris = 0;

                            for (int Índice_X = 0; Índice_X < Longitud; Índice_X++, Índice_Número++) // Up.
                            {
                                Y--;
                                if (Diccionario_Números_Divisibles.ContainsKey(Índice_Número))
                                {
                                    Índice = (Y * Bytes_Ancho) + (X * Bytes_Aumento);
                                    Color_Arco_Iris = Program.Matriz_Colores_Arco_Iris_8_Números_Primos[Índice_Arco_Iris];
                                    Matriz_Bytes_Píxeles[Índice + 2] = Color_Arco_Iris.R;
                                    Matriz_Bytes_Píxeles[Índice + 1] = Color_Arco_Iris.G;
                                    Matriz_Bytes_Píxeles[Índice] = Color_Arco_Iris.B;
                                }
                                Índice_Arco_Iris++;
                                if (Índice_Arco_Iris >= Program.Matriz_Colores_Arco_Iris_8_Números_Primos.Length) Índice_Arco_Iris = 0;
                            }
                            //Índice_Arco_Iris++;
                            //if (Índice_Arco_Iris >= 256) Índice_Arco_Iris = 0;

                            for (int Índice_Y = 0; Índice_Y < Longitud; Índice_Y++, Índice_Número++) // Right.
                            {
                                X++;
                                if (Diccionario_Números_Divisibles.ContainsKey(Índice_Número))
                                {
                                    Índice = (Y * Bytes_Ancho) + (X * Bytes_Aumento);
                                    Color_Arco_Iris = Program.Matriz_Colores_Arco_Iris_8_Números_Primos[Índice_Arco_Iris];
                                    Matriz_Bytes_Píxeles[Índice + 2] = Color_Arco_Iris.R;
                                    Matriz_Bytes_Píxeles[Índice + 1] = Color_Arco_Iris.G;
                                    Matriz_Bytes_Píxeles[Índice] = Color_Arco_Iris.B;
                                }
                                Índice_Arco_Iris++;
                                if (Índice_Arco_Iris >= Program.Matriz_Colores_Arco_Iris_8_Números_Primos.Length) Índice_Arco_Iris = 0;
                            }
                            //Índice_Arco_Iris++;
                            //if (Índice_Arco_Iris >= 256) Índice_Arco_Iris = 0;

                            // Move 1 pixel to the right and up.
                            X++;
                            Y--;
                        }
                    }
                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                    Marshal.Copy(Matriz_Bytes_Píxeles, 0, Bitmap_Data.Scan0, Matriz_Bytes_Píxeles.Length);
                    Imagen.UnlockBits(Bitmap_Data);
                    Matriz_Bytes_Píxeles = null;
                    Picture.Image = Imagen;
                    GC.Collect();
                    GC.GetTotalMemory(true);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
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
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Números_Primos);
                    Picture.Image.Save(Program.Ruta_Guardado_Imágenes_Números_Primos + "\\" + Program.Obtener_Nombre_Temporal() + " Primes.png", ImageFormat.Png);
                    SystemSounds.Asterisk.Play();
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

        private void Temporizador_Primos_Tick(object sender, EventArgs e)
        {
            try
            {
                //if (Cronómetro_Números_Primos != null && Cronómetro_Números_Primos.IsRunning)
                {
                    //MessageBox.Show(this, "eryrttwetwtwetw");
                    //this.Text = ((char)Program.Rand.Next((int)'A', ((int)'Z') + 1)).ToString();
                    TextBox_Número_Actual.Text = Program.Traducir_Número(Índice_Número_Primo_Actual);
                    int Milisegundos = (int)Cronómetro_Números_Primos.ElapsedMilliseconds;
                    Cronómetro_Números_Primos.Restart();
                    /*int Diferencia = 0;
                    if (Lista_Últimos_Primos.Count > 1)
                    {
                        for (int Índice = Lista_Últimos_Primos.Count - 1, Índice_Anterior = Lista_Últimos_Primos.Count - 2; Índice >= 0; Índice--, Índice_Anterior--)
                        {
                            if (Índice_Anterior < 0) Índice_Anterior = Lista_Últimos_Primos.Count - 1;
                            Diferencia += Math.Abs(Lista_Últimos_Primos[Índice] - Lista_Últimos_Primos[Índice_Anterior]);
                        }
                    }*/
                    if (Milisegundos > 0)
                    {
                        TextBox_Números_Segundo.Text = Program.Traducir_Número((Total_Números_Primos_Números * 1000) / Milisegundos);
                        TextBox_Primos_Segundo.Text = Program.Traducir_Número((Total_Números_Primos_Primos * 1000) / Milisegundos);
                    }
                    else
                    {
                        TextBox_Números_Segundo.Text = "0";
                        TextBox_Primos_Segundo.Text = "0";
                    }
                    TextBox_Espaciado_Primos.Text = Program.Traducir_Número_Decimales_Redondear(((double)Diccionario_Números_Primos.Count * 100d) / (double)Índice_Número_Primo_Actual, 10) + " %";
                    TextBox_Último_Primo.Text = Program.Traducir_Número(Último_Número_Primo);
                    TextBox_Números_Encontrados.Text = Program.Traducir_Número(Diccionario_Números_Primos.Count);
                    //NumericUpDown_Espaciado_Primos.Invoke(new Invocación.Delegado_NumericUpDown_Value(Invocación.Ejecutar_Delegado_NumericUpDown_Value), new object[] { NumericUpDown_Espaciado_Primos, Lista_Últimos_Primos.Count > 1 ? (decimal)Math.Round((double)Diferencia / (double)Lista_Últimos_Primos.Count, 4, MidpointRounding.AwayFromZero) : 0m });
                    //TextBox_Velocidad_Media.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { TextBox_Velocidad_Media, Program.Traducir_Número_Decimales_Redondear(Media_Números, 2) + " numbers checked per second and " + Program.Traducir_Número_Decimales_Redondear(Media_Primos, 2) + " primes found per second with " + Program.Traducir_Número_Decimales_Redondear((double)Diferencia / 251d, 2) + " numbers between each prime" });
                    Total_Números_Primos_Números = 0;
                    Total_Números_Primos_Primos = 0;
                    //Total_Índices = 0;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Subproceso_DoWork()
        {
            Subproceso_Activo = true; // Thread started.
            try
            {
                //List<int> Lista_Últimos_Primos = new List<int>();
                Total_Números_Primos_Números = 0;
                Total_Números_Primos_Primos = 0;
                short Total_Índices = 0;
                //this.Invoke(new Invocación.Delegado_Control_Cursor(Invocación.Ejecutar_Delegado_Control_Cursor), new object[] { this, Cursors.WaitCursor });
                if (Lector_Números_Primos != null && Lector_Salida_Números_Primos != null)
                {
                    // The number we are starting to look for new primes.
                    //int Índice_Número_Primo = Último_Número;
                    bool Continuar = false;
                    bool Número_Primo = false;
                    if (Índice_Número_Primo_Actual <= 255)
                    {
                        //this.Invoke(new Invocación.Delegado_IWin32Window_MessageBox(Invocación.Ejecutar_Delegado_IWin32Window_MessageBox), new object[] { this, "Starting the search for primes at the number " + Program.Traducir_Número(Índice_Número) + ".", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information });
                        // Slow mode for the first primes.
                        for (; Índice_Número_Primo_Actual <= 255; Índice_Número_Primo_Actual++)
                        {
                            // Never cancel at the first "byte" numbers.
                            Total_Números_Primos_Números++;
                            Total_Índices++;
                            // See if the current number is prime or not.
                            Número_Primo = true;
                            //foreach (KeyValuePair<int, object> Entrada in Diccionario_Números_Primos)
                            foreach (int Valor in Diccionario_Números_Primos.Keys)
                            {
                                try
                                {
                                    if (Índice_Número_Primo_Actual % Valor == 0) // It's not a prime number.
                                    {
                                        Número_Primo = false;
                                        break;
                                    }
                                }
                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                            }
                            if (Número_Primo) // We found a new prime number, add it now.
                            {
                                Último_Número_Primo = Índice_Número_Primo_Actual;
                                Total_Números_Primos_Primos++;
                                Diccionario_Números_Primos.Add(Índice_Número_Primo_Actual, null);
                                Lector_Salida_Números_Primos.Write(Índice_Número_Primo_Actual);
                                Lector_Salida_Números_Primos.Flush();
                                //Lista_Últimos_Primos.Add(Índice_Número);
                                //TextBox_Último_Primo.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { TextBox_Último_Primo, Program.Traducir_Número(Índice_Número_Primo_Actual) });
                                //TextBox_Números_Encontrados.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { TextBox_Números_Encontrados, Program.Traducir_Número(Diccionario_Números_Primos.Count) });
                                //TextBox_Números_Primos.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { TextBox_Números_Primos, TextBox_Números_Primos.Text + Índice_Número.ToString() + ", " });
                            }
                        }
                    }
                    //else this.Invoke(new Invocación.Delegado_IWin32Window_MessageBox(Invocación.Ejecutar_Delegado_IWin32Window_MessageBox), new object[] { this, "Starting the search for primes at the number " + Program.Traducir_Número(Índice_Número) + ".", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information });

                    // Always start this part at 256 or higher values or this won't work at all.
                    // Keep in sync with the first 54 primes that fit in a byte to calculate a lot faster.
                    byte[] Matriz_Números_Primos_Byte = new byte[54]
                    {
                        // This order is the same as a 16 x 16 array with 256 byte numbers (0 to 255).
                        2, 3, 5, 7, 11, 13,
                        17, 19, 23, 29, 31,
                        37, 41, 43, 47,
                        53, 59, 61,
                        67, 71, 73, 79,
                        83, 89,
                        97, 101, 103, 107, 109,
                        113, 127,
                        131, 137, 139,
                        149, 151, 157,
                        163, 167, 173,
                        179, 181, 191,
                        193, 197, 199,
                        211, 223,
                        227, 229, 233, 239,
                        241, 251
                    };
                    byte[] Matriz_Índices_Números_Primos_Byte = new byte[54];
                    for (int Índice_Byte = 0; Índice_Byte < 54; Índice_Byte++)
                    {
                        Matriz_Índices_Números_Primos_Byte[Índice_Byte] = (byte)(Índice_Número_Primo_Actual % Matriz_Números_Primos_Byte[Índice_Byte]);
                    }
                    TextBox_Número_Actual.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { TextBox_Número_Actual, Program.Traducir_Número(Índice_Número_Primo_Actual) });

                    // Fastest mode to avoid multiples of the first 54 primes.
                    // Almost infinite loop to find 64 bits prime numbers.
                    for (; Índice_Número_Primo_Actual <= int.MaxValue; )
                    {
                        if (Pendiente_Subproceso_Abortar) // Cancel safely.
                        {
                            TextBox_Número_Actual.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { TextBox_Número_Actual, Program.Traducir_Número(Índice_Número_Primo_Actual) });
                            this.Invoke(new Invocación.Delegado_IWin32Window_MessageBox(Invocación.Ejecutar_Delegado_IWin32Window_MessageBox), new object[] { this, "Cancelling before looking if \"" + Program.Traducir_Número(Índice_Número_Primo_Actual) + "\" is prime.\r\nYou can safely resume the search at any other time.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information });
                            break;
                        }
                        Total_Números_Primos_Números++;
                        Total_Índices++;
                        Continuar = true;
                        for (int Índice_Byte = 0; Índice_Byte < 54; Índice_Byte++)
                        {
                            if (Matriz_Índices_Números_Primos_Byte[Índice_Byte] == 0)
                            {
                                Continuar = false; // It's one of the first 54 primes, so skip it.
                                //break;
                            }
                            // Increase all the indexes at the end to save CPU.
                            Matriz_Índices_Números_Primos_Byte[Índice_Byte]++;
                            if (Matriz_Índices_Números_Primos_Byte[Índice_Byte] >= Matriz_Números_Primos_Byte[Índice_Byte])
                            {
                                Matriz_Índices_Números_Primos_Byte[Índice_Byte] = 0; // Reset when it's maximum is reached.
                            }
                        }
                        if (Continuar)
                        {
                            // See if the current number is prime or not.
                            Número_Primo = true;
                            //foreach (KeyValuePair<int, object> Entrada in Diccionario_Números_Primos)
                            foreach (int Valor in Diccionario_Números_Primos.Keys)
                            {
                                try
                                {
                                    if (Índice_Número_Primo_Actual % Valor == 0) // It's not a prime number.
                                    {
                                        Número_Primo = false;
                                        break;
                                    }
                                }
                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                            }
                            if (Número_Primo) // We found a new prime number, add it now.
                            {
                                Último_Número_Primo = Índice_Número_Primo_Actual;
                                Total_Números_Primos_Primos++;
                                Diccionario_Números_Primos.Add(Índice_Número_Primo_Actual, null);
                                Lector_Salida_Números_Primos.Write(Índice_Número_Primo_Actual);
                                Lector_Salida_Números_Primos.Flush();
                                //Lista_Últimos_Primos.Add(Índice_Número);
                                //TextBox_Último_Primo.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { TextBox_Último_Primo, Program.Traducir_Número(Índice_Número_Primo_Actual) });
                                //TextBox_Números_Encontrados.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { TextBox_Números_Encontrados, Program.Traducir_Número(Diccionario_Números_Primos.Count) });
                                //TextBox_Números_Primos.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { TextBox_Números_Primos, TextBox_Números_Primos.Text + Índice_Número.ToString() + ", " });
                            }
                        }
                        //if (Total_Índices == 1000)
                        //if (Índice_Número % 100000 == 0)
                        /*if (Matriz_Índices_Números_Primos_Byte[53] == 0) // Each 251 numbers show where we are.
                        {
                            TextBox_Número_Actual.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { TextBox_Número_Actual, Program.Traducir_Número(Índice_Número_Primo_Actual) });
                            int Milisegundos = (int)Cronómetro_Números_Primos.ElapsedMilliseconds;
                            Cronómetro_Números_Primos.Restart();
                            /*int Diferencia = 0;
                            if (Lista_Últimos_Primos.Count > 1)
                            {
                                for (int Índice = Lista_Últimos_Primos.Count - 1, Índice_Anterior = Lista_Últimos_Primos.Count - 2; Índice >= 0; Índice--, Índice_Anterior--)
                                {
                                    if (Índice_Anterior < 0) Índice_Anterior = Lista_Últimos_Primos.Count - 1;
                                    Diferencia += Math.Abs(Lista_Últimos_Primos[Índice] - Lista_Últimos_Primos[Índice_Anterior]);
                                }
                            }*//*
                            TextBox_Números_Segundo.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { TextBox_Números_Segundo, Program.Traducir_Número((Total_Números_Primos_Números * 1000) / Milisegundos) });
                            TextBox_Primos_Segundo.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { TextBox_Primos_Segundo, Program.Traducir_Número((Total_Números_Primos_Primos * 1000) / Milisegundos) });
                            //NumericUpDown_Espaciado_Primos.Invoke(new Invocación.Delegado_NumericUpDown_Value(Invocación.Ejecutar_Delegado_NumericUpDown_Value), new object[] { NumericUpDown_Espaciado_Primos, Lista_Últimos_Primos.Count > 1 ? (decimal)Math.Round((double)Diferencia / (double)Lista_Últimos_Primos.Count, 4, MidpointRounding.AwayFromZero) : 0m });
                            //TextBox_Velocidad_Media.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { TextBox_Velocidad_Media, Program.Traducir_Número_Decimales_Redondear(Media_Números, 2) + " numbers checked per second and " + Program.Traducir_Número_Decimales_Redondear(Media_Primos, 2) + " primes found per second with " + Program.Traducir_Número_Decimales_Redondear((double)Diferencia / 251d, 2) + " numbers between each prime" });
                            Total_Números_Primos_Números = 0;
                            Total_Números_Primos_Primos = 0;
                            Total_Índices = 0;
                            //Lista_Últimos_Primos.Clear();
                        }*/
                        if (Índice_Número_Primo_Actual < int.MaxValue) Índice_Número_Primo_Actual++; // Include the last valid number.
                        else break; // Never overflow the int index value.
                    }
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally
            {
                Temporizador_Primos.Stop();
                Cronómetro_Números_Primos.Reset();
                //this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [Found headers: " + Program.Traducir_Número(Total_Cabeceras) + ", Extracted images: " + Program.Traducir_Número(Total_Imágenes) + ", Done]" });
                //Picture.Invoke(new Invocación.Delegado_PictureBox_Image(Invocación.Ejecutar_Delegado_PictureBox_Image), new object[] { Picture, null });
                //Picture_Progreso.Invoke(new Invocación.Delegado_PictureBox_Image(Invocación.Ejecutar_Delegado_PictureBox_Image), new object[] { Picture_Progreso, null });
                //this.Invoke(new Invocación.Delegado_Control_Cursor(Invocación.Ejecutar_Delegado_Control_Cursor), new object[] { this, Cursors.Default });
                if (Pendiente_Subproceso_Abortar)
                {
                    Pendiente_Subproceso_Abortar = false;
                    Subproceso_Activo = false;
                    Subproceso = null;
                    if (Pendiente_Subproceso_Abortar_Ventana)
                    {
                        Pendiente_Subproceso_Abortar_Ventana = false;
                        this.Invoke(new Invocación.Delegado_Form_Close(Invocación.Ejecutar_Delegado_Form_Close), new object[] { this });
                    }
                    else
                    {
                        //this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { Botón_Divisibles_Buscar, true });
                    }
                }
                else // Can't find new numbers with 64 bits...
                {
                    Pendiente_Subproceso_Abortar = false;
                    Subproceso_Activo = false;
                    Subproceso = null;
                    //this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { TextBox_Ruta, true });
                    //this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { CheckBox_Mostrar_Imágenes, true });
                    //this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { CheckBox_Archivos_Completos, true });
                    //this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { CheckBox_Extraer_Subcarpetas, true });
                    //this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { CheckBox_Mantener_Estructura, true });
                    //this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { Botón_Extraer, true });
                    //this.Invoke(new Invocación.Delegado_Control_Select(Invocación.Ejecutar_Delegado_Control_Select), new object[] { Botón_Extraer });
                    //this.Invoke(new Invocación.Delegado_Control_Focus(Invocación.Ejecutar_Delegado_Control_Focus), new object[] { Botón_Extraer });
                }
            }
        }
    }
}
