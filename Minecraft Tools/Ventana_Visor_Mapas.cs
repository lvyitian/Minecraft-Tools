using Ionic.Zlib;
using Minecraft_Tools.Properties;
using Substrate_Jupisoft.Nbt;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Visor_Mapas : Form
    {
        public Ventana_Visor_Mapas()
        {
            InitializeComponent();
        }

        internal class Comparador_Mapas : IComparer<string>
        {
            public int Compare(string X, string Y)
            {
                try
                {
                    int Número_X = int.Parse(Path.GetFileNameWithoutExtension(X).Replace("map_", null));
                    int Número_Y = int.Parse(Path.GetFileNameWithoutExtension(Y).Replace("map_", null));
                    if (Número_X < Número_Y) return -1;
                    else if (Número_X > Número_Y) return 1;
                    else return 0;
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                return string.Compare(X, Y);
            }
        }

        internal readonly string Texto_Título = "Map Viewer by Jupisoft for " + Program.Texto_Usuario;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;
        internal static Stopwatch FPS_Cronómetro = Stopwatch.StartNew();
        internal long FPS_Segundo_Anterior = 0L;
        internal long FPS_Temporal = 0L;
        internal long FPS_Real = 0L;
        /// <summary>
        /// List used to see the actual time spacing between the FPS. It can only store a full second before it resets itself.
        /// </summary>
        internal List<int> Lista_FPS_Milisegundos = new List<int>();
        /// <summary>
        /// Variable that if it's true will always show the main window on top of others.
        /// </summary>
        internal bool Variable_Siempre_Visible = false;
        internal List<string> Lista_Rutas_Mundos = new List<string>();
        internal List<string> Lista_Rutas_Mapas = new List<string>();

        private void Ventana_Visor_Mapas_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                Menú_Contextual_Acerca.Text = "About " + Program.Texto_Programa + " " + Program.Texto_Versión + "...";
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = Variable_Siempre_Visible;
                //MessageBox.Show((255 << 24).ToString()); // Give full alpha test.
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Mapas_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
                Cargar_Mundos();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Mapas_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Mapas_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Mapas_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Mapas_KeyDown(object sender, KeyEventArgs e)
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

        private void ComboBox_Mundo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cargar_Mapas();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Mapa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cargar_Mapa();
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

        private void Menú_Contextual_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                Menú_Contextual_Depurador_Excepciones.Text = "Exception debugger - [" + Program.Traducir_Número(Variable_Excepción_Total) + (Variable_Excepción_Total != 1 ? " exceptions" : " exception") + "]...";
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Donar_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=KSMZ3XNG2R9P6", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(this, "The help file is not available yet... sorry.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Acerca_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Acerca Ventana = new Ventana_Acerca();
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
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Abrir_Carpeta_Guardado_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Crear_Carpetas(Program.Ruta_Guardado);
                Program.Ejecutar_Ruta(Program.Ruta_Guardado, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Cargar_Mundos();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (Picture_Mapa.BackgroundImage != null)
                {
                    Clipboard.SetImage(Picture_Mapa.BackgroundImage);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (Picture_Mapa.BackgroundImage != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado);
                    Picture_Mapa.BackgroundImage.Save(Program.Ruta_Guardado + "\\Map " + Program.Obtener_Nombre_Temporal() + ".png", ImageFormat.Png);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Temporizador_Principal_Tick(object sender, EventArgs e)
        {
            try
            {
                int Tick = Environment.TickCount; // Used in the next calculations.

                try // If there are new exceptions, flash in red text every 500 milliseconds.
                {
                    if (Variable_Excepción)
                    {
                        if ((Tick / 500) % 2 == 0)
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

                try // CPU and RAM use calculations.
                {
                    try
                    {
                        if (Tick % 250 == 0) // Update every 250 milliseconds.
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
                            if (Memoria_Bytes < 4294967296L) // < 4 GB, default black text.
                            {
                                if (Variable_Memoria)
                                {
                                    Variable_Memoria = false;
                                    Barra_Estado_Etiqueta_Memoria.ForeColor = Color.Black;
                                }
                            }
                            else // >= 4 GB, flash in red text every 500 milliseconds.
                            {
                                if ((Tick / 500) % 2 == 0)
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

                try // FPS calculation and drawing.
                {
                    long FPS_Milisegundo = FPS_Cronómetro.ElapsedMilliseconds;
                    long FPS_Segundo = FPS_Milisegundo / 1000L;
                    int Milisegundo_Actual = FPS_Cronómetro.Elapsed.Milliseconds;
                    if (FPS_Segundo != FPS_Segundo_Anterior)
                    {
                        FPS_Segundo_Anterior = FPS_Segundo;
                        FPS_Real = FPS_Temporal;
                        Barra_Estado_Etiqueta_FPS.Text = FPS_Real.ToString() + " FPS";
                        FPS_Temporal = 0L;
                        Lista_FPS_Milisegundos.Clear(); // Reset.
                    }
                    Lista_FPS_Milisegundos.Add(Milisegundo_Actual); // Add the current millisecond.
                    FPS_Temporal++;

                    //if (Variable_Dibujar_Espaciado_FPS)
                    {
                        // Draw the FPS spacing in real time.
                        int Ancho_FPS = Picture_FPS.ClientSize.Width;
                        if (Ancho_FPS > 0) // Don't draw if the window is minimized.
                        {
                            Bitmap Imagen_FPS = new Bitmap(Ancho_FPS, 8, PixelFormat.Format32bppArgb);
                            Graphics Pintar_FPS = Graphics.FromImage(Imagen_FPS);
                            Pintar_FPS.CompositingMode = CompositingMode.SourceOver;
                            Pintar_FPS.CompositingQuality = CompositingQuality.HighQuality;
                            Pintar_FPS.InterpolationMode = InterpolationMode.NearestNeighbor;
                            Pintar_FPS.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Pintar_FPS.SmoothingMode = SmoothingMode.None;
                            Pintar_FPS.TextRenderingHint = TextRenderingHint.AntiAlias;
                            Ancho_FPS -= 8; // Subtract 8 pixels to draw the full FPS icons on the image borders.
                            foreach (int Milisegundo in Lista_FPS_Milisegundos)
                            {
                                SolidBrush Pincel = new SolidBrush(Program.Obtener_Color_Puro_1530((Milisegundo * 1529) / 999));
                                Pintar_FPS.FillEllipse(Pincel, ((Milisegundo * Ancho_FPS) / 999), 0, 8, 8);
                                Pincel.Dispose();
                                Pincel = null;
                            }
                            Pintar_FPS.Dispose();
                            Pintar_FPS = null;
                            Picture_FPS.BackgroundImage = Imagen_FPS;
                        }
                    }
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Cargar_Mundos()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.Text = Texto_Título + " - [Loading the existing worlds...]";
                ComboBox_Mundo.Items.Clear();
                ComboBox_Mapa.Items.Clear();
                Picture_Mapa.BackgroundImage = null;
                Picture_Mapa.Image = null;
                Barra_Estado_Etiqueta_Dimensión.Text = "Dimension: ?";
                Barra_Estado_Etiqueta_Centro.Text = "Map center: ?, ?";
                Barra_Estado_Etiqueta_Escala.Text = "Scale: ?";
                Lista_Rutas_Mundos.Clear();
                Lista_Rutas_Mapas.Clear();
                // Load the existing Minecraft worlds from the default save folder and of any existing modpacks:
                if (Directory.Exists(Program.Ruta_Guardado_Minecraft))
                {
                    string[] Matriz_Rutas = Directory.GetDirectories(Program.Ruta_Guardado_Minecraft, "*", SearchOption.TopDirectoryOnly);
                    if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                    {
                        if (Matriz_Rutas.Length > 1) Array.Sort(Matriz_Rutas);
                        foreach (string Ruta in Matriz_Rutas)
                        {
                            Lista_Rutas_Mundos.Add(Ruta);
                            ComboBox_Mundo.Items.Add(Path.GetFileName(Ruta));
                        }
                        Matriz_Rutas = null;
                    }
                }
                this.Text = Texto_Título + " - [Found worlds: " + Program.Traducir_Número(ComboBox_Mundo.Items.Count) + "]";
                if (ComboBox_Mundo.Items.Count > 0) ComboBox_Mundo.SelectedIndex = 0;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        internal void Cargar_Mapas()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.Text = Texto_Título + " - [Loading the existing maps...]";
                ComboBox_Mapa.Items.Clear();
                Picture_Mapa.BackgroundImage = null;
                Picture_Mapa.Image = null;
                Barra_Estado_Etiqueta_Dimensión.Text = "Dimension: ?";
                Barra_Estado_Etiqueta_Centro.Text = "Map center: ?, ?";
                Barra_Estado_Etiqueta_Escala.Text = "Scale: ?";
                Lista_Rutas_Mapas.Clear();
                int Índice_Mundo = ComboBox_Mundo.SelectedIndex;
                if (Índice_Mundo > -1 && Índice_Mundo < Lista_Rutas_Mundos.Count)
                {
                    string Ruta_Mundo = Lista_Rutas_Mundos[Índice_Mundo] + "\\data";
                    if (Directory.Exists(Ruta_Mundo))
                    {
                        string[] Matriz_Rutas = Directory.GetFiles(Ruta_Mundo, "map_*.dat", SearchOption.TopDirectoryOnly);
                        if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                        {
                            if (Matriz_Rutas.Length > 1) Array.Sort(Matriz_Rutas, new Comparador_Mapas());
                            foreach (string Ruta in Matriz_Rutas)
                            {
                                string Nombre = Path.GetFileName(Ruta);
                                // This should be a valid map if starts with "map_".
                                if (Nombre.StartsWith("map_", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Lista_Rutas_Mapas.Add(Ruta);
                                    ComboBox_Mapa.Items.Add(Path.GetFileName(Ruta));
                                }
                            }
                            Matriz_Rutas = null;
                        }
                    }
                }
                this.Text = Texto_Título + " - [Found maps: " + Program.Traducir_Número(ComboBox_Mapa.Items.Count) + "]";
                if (ComboBox_Mapa.Items.Count > 0) ComboBox_Mapa.SelectedIndex = 0;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        internal void Cargar_Mapa()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Picture_Mapa.BackgroundImage = null;
                Picture_Mapa.Image = null;
                Barra_Estado_Etiqueta_Dimensión.Text = "Dimension: ?";
                Barra_Estado_Etiqueta_Centro.Text = "Map center: ?, ?";
                Barra_Estado_Etiqueta_Escala.Text = "Scale: ?";
                string Ruta_Mapa = null;
                string Compresión = null;
                int Ancho_Alto = 0;
                int Autozoom = 0;
                this.Text = Texto_Título + " - [Loading the selected map...]";
                int Índice_Mapa = ComboBox_Mapa.SelectedIndex;
                if (ComboBox_Mundo.SelectedIndex > -1 && ComboBox_Mundo.SelectedIndex < Lista_Rutas_Mundos.Count && Índice_Mapa > -1 && Índice_Mapa < Lista_Rutas_Mapas.Count)
                {
                    Ruta_Mapa = Lista_Rutas_Mapas[Índice_Mapa];
                    if (File.Exists(Ruta_Mapa))
                    {
                        FileStream Lector = new FileStream(Ruta_Mapa, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        if (Lector != null && Lector.Length > 0L)
                        {
                            NbtTree Árbol = null;
                            try
                            {
                                Lector.Seek(0L, SeekOrigin.Begin);
                                Árbol = new NbtTree();
                                Árbol.ReadFrom(Lector);
                                if (Árbol != null && Árbol.Root != null) Compresión = "Uncompressed";
                            }
                            catch// (Exception Excepción)
                            {
                                //Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                                Árbol = null;
                            }
                            if (Árbol == null || Árbol.Root == null)
                            {
                                try
                                {
                                    Lector.Seek(0L, SeekOrigin.Begin);
                                    Árbol = new NbtTree();
                                    Árbol.ReadFrom(new GZipStream(Lector, CompressionMode.Decompress));
                                    if (Árbol != null && Árbol.Root != null) Compresión = "GZipStream";
                                }
                                catch// (Exception Excepción)
                                {
                                    //Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                                    Árbol = null;
                                }
                            }
                            if (Árbol == null || Árbol.Root == null)
                            {
                                try
                                {
                                    Lector.Seek(0L, SeekOrigin.Begin);
                                    Árbol = new NbtTree();
                                    Árbol.ReadFrom(new ZlibStream(Lector, CompressionMode.Decompress));
                                    if (Árbol != null && Árbol.Root != null) Compresión = "ZlibStream";
                                }
                                catch// (Exception Excepción)
                                {
                                    //Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                                    Árbol = null;
                                }
                            }
                            if (Árbol == null || Árbol.Root == null)
                            {
                                try
                                {
                                    Lector.Seek(0L, SeekOrigin.Begin);
                                    Árbol = new NbtTree();
                                    Árbol.ReadFrom(new DeflateStream(Lector, CompressionMode.Decompress));
                                    if (Árbol != null && Árbol.Root != null) Compresión = "DeflateStream";
                                }
                                catch// (Exception Excepción)
                                {
                                    //Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                                    Árbol = null;
                                }
                            }
                            if (Árbol != null &&
                                Árbol.Root != null &&
                                Árbol.Root.Keys != null &&
                                Árbol.Root.Keys.Count > 0 &&
                                Árbol.Root.Keys.Contains("data"))
                            {
                                TagNodeCompound Nodo_Data = Árbol.Root["data"] as TagNodeCompound;
                                if (Nodo_Data != null &&
                                    Nodo_Data.Keys != null &&
                                    Nodo_Data.Keys.Count > 0)
                                {
                                    if (Nodo_Data.Keys.Contains("colors"))
                                    {
                                        byte[] Matriz_Bytes_Mapa = Nodo_Data["colors"] as TagNodeByteArray;
                                        if (Matriz_Bytes_Mapa != null && Matriz_Bytes_Mapa.Length > 0)
                                        {
                                            // Divide the colors length into 2 dimensions to get the map size. It works.
                                            Ancho_Alto = (int)Math.Sqrt((double)Matriz_Bytes_Mapa.Length); // 128 x 128.
                                            Bitmap Imagen_Mapa = new Bitmap(Ancho_Alto, Ancho_Alto, PixelFormat.Format32bppArgb);
                                            Graphics Pintar_Mapa = Graphics.FromImage(Imagen_Mapa);
                                            Pintar_Mapa.CompositingMode = CompositingMode.SourceCopy;
                                            Pintar_Mapa.CompositingQuality = CompositingQuality.HighQuality;
                                            Pintar_Mapa.InterpolationMode = InterpolationMode.NearestNeighbor;
                                            Pintar_Mapa.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                            Pintar_Mapa.SmoothingMode = SmoothingMode.None;
                                            Pintar_Mapa.TextRenderingHint = TextRenderingHint.AntiAlias;
                                            //Pintar_Mapa.Clear(Color.Transparent);
                                            for (int Índice_Z = 0, Índice_Byte = 0; Índice_Z < Ancho_Alto; Índice_Z++)
                                            {
                                                for (int Índice_X = 0; Índice_X < Ancho_Alto; Índice_X++, Índice_Byte++)
                                                {
                                                    //Pintar_Mapa.FillRectangle(Program.Matriz_Pinceles_Arco_Iris_256[Matriz_Bytes_Mapa[Índice_Byte]], Índice_X, Índice_Z, 1, 1);
                                                    //if (Matriz_Bytes_Mapa[Índice_Byte] < 64)
                                                    {
                                                        int Índice = Matriz_Bytes_Mapa[Índice_Byte] / 4; // This let me decode the map format.
                                                        //Color Color_ARGB = Color.FromArgb(255, Matriz_Colores[Índice].R, Matriz_Colores[Índice].G, Matriz_Colores[Índice].B);
                                                        SolidBrush Pincel = new SolidBrush(Matriz_Colores[Índice]);
                                                        Pintar_Mapa.FillRectangle(Pincel, Índice_X, Índice_Z, 1, 1);
                                                        Pincel.Dispose();
                                                        Pincel = null;
                                                    }
                                                    /*else
                                                    {
                                                        Pintar_Mapa.FillRectangle(Brushes.Black, Índice_X, Índice_Z, 1, 1);
                                                    }*/
                                                }
                                            }
                                            Pintar_Mapa.Dispose();
                                            Pintar_Mapa = null;
                                            Picture_Mapa.BackgroundImage = Imagen_Mapa;
                                            Picture_Mapa.Image = Program.Obtener_Imagen_Autozoom(Imagen_Mapa, Picture_Mapa.ClientSize.Width, Picture_Mapa.ClientSize.Height, true, CheckState.Unchecked, out Autozoom);
                                        }
                                    }
                                    if (Nodo_Data.Keys.Contains("dimension"))
                                    {
                                        try
                                        {
                                            int Dimensión = Nodo_Data["dimension"] as TagNodeInt;
                                            string Texto_Dimensión = "Unknown";
                                            if (Dimensión == 0) Texto_Dimensión = "Overworld";
                                            else if (Dimensión == -1) Texto_Dimensión = "Nether";
                                            else if (Dimensión == 1) Texto_Dimensión = "The end";
                                            Barra_Estado_Etiqueta_Dimensión.Text = "Dimension: " + Texto_Dimensión;
                                        }
                                        catch // Try a second format. It happened on "Minecraft - Mario Hide & Seek".
                                        {
                                            byte Dimensión = Nodo_Data["dimension"] as TagNodeByte;
                                            string Texto_Dimensión = "Unknown";
                                            if (Dimensión == 0) Texto_Dimensión = "Overworld";
                                            else if (Dimensión == -1) Texto_Dimensión = "Nether";
                                            else if (Dimensión == 1) Texto_Dimensión = "The end";
                                            Barra_Estado_Etiqueta_Dimensión.Text = "Dimension: " + Texto_Dimensión;
                                        }
                                    }
                                    if (Nodo_Data.Keys.Contains("xCenter") &&
                                        Nodo_Data.Keys.Contains("zCenter"))
                                    {
                                        try
                                        {
                                            int Centro_X = Nodo_Data["xCenter"] as TagNodeInt;
                                            int Centro_Z = Nodo_Data["zCenter"] as TagNodeInt;
                                            Barra_Estado_Etiqueta_Centro.Text = "Map center: " + Program.Traducir_Número(Centro_X) + ", " + Program.Traducir_Número(Centro_Z);
                                        }
                                        catch // Try a second format.
                                        {
                                            byte Centro_X = Nodo_Data["xCenter"] as TagNodeByte;
                                            byte Centro_Z = Nodo_Data["zCenter"] as TagNodeByte;
                                            Barra_Estado_Etiqueta_Centro.Text = "Map center: " + Program.Traducir_Número(Centro_X) + ", " + Program.Traducir_Número(Centro_Z);
                                        }
                                    }
                                    if (Nodo_Data.Keys.Contains("scale"))
                                    {
                                        try
                                        {
                                            byte Escala = Nodo_Data["scale"] as TagNodeByte;
                                            Barra_Estado_Etiqueta_Escala.Text = "Scale: " + (Escala + 1).ToString() + "x";
                                        }
                                        catch // Try a second format.
                                        {
                                            int Escala = Nodo_Data["scale"] as TagNodeInt;
                                            Barra_Estado_Etiqueta_Escala.Text = "Scale: " + (Escala + 1).ToString() + "x";
                                        }
                                    }
                                    Nodo_Data = null;
                                }
                            }
                            Árbol = null;
                            Lector.Close();
                            Lector.Dispose();
                            Lector = null;
                        }
                    }
                }
                this.Text = Texto_Título + " - [Map " + (!string.IsNullOrEmpty(Ruta_Mapa) ? Path.GetFileNameWithoutExtension(Ruta_Mapa).Replace("map_", null) : "?") + ": " + Program.Traducir_Número(Ancho_Alto) + " x " + Program.Traducir_Número(Ancho_Alto) + " blocks, Compression: " + Compresión + ", Autozoom: " + Program.Traducir_Número(Autozoom) + "x]";
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        /// <summary>
        /// Array used to color maps ported from the Minecraft 1.12 original source code.
        /// </summary>
        internal static readonly Color[] Matriz_Colores = new Color[64]
        {
            Color.FromArgb(-16777216 | 0),
            Color.FromArgb(-16777216 | 8368696),
            Color.FromArgb(-16777216 | 16247203),
            Color.FromArgb(-16777216 | 13092807),
            Color.FromArgb(-16777216 | 16711680),
            Color.FromArgb(-16777216 | 10526975),
            Color.FromArgb(-16777216 | 10987431),
            Color.FromArgb(-16777216 | 31744),

            Color.FromArgb(-16777216 | 16777215),
            Color.FromArgb(-16777216 | 10791096),
            Color.FromArgb(-16777216 | 9923917),
            Color.FromArgb(-16777216 | 7368816),
            Color.FromArgb(-16777216 | 4210943),
            Color.FromArgb(-16777216 | 9402184),
            Color.FromArgb(-16777216 | 16776437),
            Color.FromArgb(-16777216 | 14188339),

            Color.FromArgb(-16777216 | 11685080),
            Color.FromArgb(-16777216 | 6724056),
            Color.FromArgb(-16777216 | 15066419),
            Color.FromArgb(-16777216 | 8375321),
            Color.FromArgb(-16777216 | 15892389),
            Color.FromArgb(-16777216 | 5000268),
            Color.FromArgb(-16777216 | 10066329),
            Color.FromArgb(-16777216 | 5013401),

            Color.FromArgb(-16777216 | 8339378),
            Color.FromArgb(-16777216 | 3361970),
            Color.FromArgb(-16777216 | 6704179),
            Color.FromArgb(-16777216 | 6717235),
            Color.FromArgb(-16777216 | 10040115),
            Color.FromArgb(-16777216 | 1644825),
            Color.FromArgb(-16777216 | 16445005),
            Color.FromArgb(-16777216 | 6085589),

            Color.FromArgb(-16777216 | 4882687),
            Color.FromArgb(-16777216 | 55610),
            Color.FromArgb(-16777216 | 8476209),
            Color.FromArgb(-16777216 | 7340544),
            Color.FromArgb(-16777216 | 13742497),
            Color.FromArgb(-16777216 | 10441252),
            Color.FromArgb(-16777216 | 9787244),
            Color.FromArgb(-16777216 | 7367818),

            Color.FromArgb(-16777216 | 12223780),
            Color.FromArgb(-16777216 | 6780213),
            Color.FromArgb(-16777216 | 10505550),
            Color.FromArgb(-16777216 | 3746083),
            Color.FromArgb(-16777216 | 8874850),
            Color.FromArgb(-16777216 | 5725276),
            Color.FromArgb(-16777216 | 8014168),
            Color.FromArgb(-16777216 | 4996700),

            Color.FromArgb(-16777216 | 4993571),
            Color.FromArgb(-16777216 | 5001770),
            Color.FromArgb(-16777216 | 9321518),
            Color.FromArgb(-16777216 | 2430480),
            Color.FromArgb(-16777216 | 0), // ?.
            Color.FromArgb(-16777216 | 0), // ?.
            Color.FromArgb(-16777216 | 0), // ?.
            Color.FromArgb(-16777216 | 0), // ?.

            Color.FromArgb(-16777216 | 0), // ?.
            Color.FromArgb(-16777216 | 0), // ?.
            Color.FromArgb(-16777216 | 0), // ?.
            Color.FromArgb(-16777216 | 0), // ?.
            Color.FromArgb(-16777216 | 0), // ?.
            Color.FromArgb(-16777216 | 0), // ?.
            Color.FromArgb(-16777216 | 0), // ?.
            Color.FromArgb(-16777216 | 0) // ?.
        };

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

        // Minecraft 1.12 original source code from the file "Client/src/net/minecraft/block/material/MapColor.java":
        /*package net.minecraft.block.material;

        import net.minecraft.item.EnumDyeColor;

        public class MapColor
        {
                // Holds all the 16 colors used on maps, very similar of a pallete system.

                public static final MapColor[] COLORS = new MapColor[64];
                public static final MapColor[] field_193575_b = new MapColor[16];
                public static final MapColor AIR = new MapColor(0, 0);
                public static final MapColor GRASS = new MapColor(1, 8368696);
                public static final MapColor SAND = new MapColor(2, 16247203);
                public static final MapColor CLOTH = new MapColor(3, 13092807);
                public static final MapColor TNT = new MapColor(4, 16711680);
                public static final MapColor ICE = new MapColor(5, 10526975);
                public static final MapColor IRON = new MapColor(6, 10987431);
                public static final MapColor FOLIAGE = new MapColor(7, 31744);
                public static final MapColor SNOW = new MapColor(8, 16777215);
                public static final MapColor CLAY = new MapColor(9, 10791096);
                public static final MapColor DIRT = new MapColor(10, 9923917);
                public static final MapColor STONE = new MapColor(11, 7368816);
                public static final MapColor WATER = new MapColor(12, 4210943);
                public static final MapColor WOOD = new MapColor(13, 9402184);
                public static final MapColor QUARTZ = new MapColor(14, 16776437);
                public static final MapColor ADOBE = new MapColor(15, 14188339);
                public static final MapColor MAGENTA = new MapColor(16, 11685080);
                public static final MapColor LIGHT_BLUE = new MapColor(17, 6724056);
                public static final MapColor YELLOW = new MapColor(18, 15066419);
                public static final MapColor LIME = new MapColor(19, 8375321);
                public static final MapColor PINK = new MapColor(20, 15892389);
                public static final MapColor GRAY = new MapColor(21, 5000268);
                public static final MapColor SILVER = new MapColor(22, 10066329);
                public static final MapColor CYAN = new MapColor(23, 5013401);
                public static final MapColor PURPLE = new MapColor(24, 8339378);
                public static final MapColor BLUE = new MapColor(25, 3361970);
                public static final MapColor BROWN = new MapColor(26, 6704179);
                public static final MapColor GREEN = new MapColor(27, 6717235);
                public static final MapColor RED = new MapColor(28, 10040115);
                public static final MapColor BLACK = new MapColor(29, 1644825);
                public static final MapColor GOLD = new MapColor(30, 16445005);
                public static final MapColor DIAMOND = new MapColor(31, 6085589);
                public static final MapColor LAPIS = new MapColor(32, 4882687);
                public static final MapColor EMERALD = new MapColor(33, 55610);
                public static final MapColor OBSIDIAN = new MapColor(34, 8476209);
                public static final MapColor NETHERRACK = new MapColor(35, 7340544);
                public static final MapColor field_193561_M = new MapColor(36, 13742497);
                public static final MapColor field_193562_N = new MapColor(37, 10441252);
                public static final MapColor field_193563_O = new MapColor(38, 9787244);
                public static final MapColor field_193564_P = new MapColor(39, 7367818);
                public static final MapColor field_193565_Q = new MapColor(40, 12223780);
                public static final MapColor field_193566_R = new MapColor(41, 6780213);
                public static final MapColor field_193567_S = new MapColor(42, 10505550);
                public static final MapColor field_193568_T = new MapColor(43, 3746083);
                public static final MapColor field_193569_U = new MapColor(44, 8874850);
                public static final MapColor field_193570_V = new MapColor(45, 5725276);
                public static final MapColor field_193571_W = new MapColor(46, 8014168);
                public static final MapColor field_193572_X = new MapColor(47, 4996700);
                public static final MapColor field_193573_Y = new MapColor(48, 4993571);
                public static final MapColor field_193574_Z = new MapColor(49, 5001770);
                public static final MapColor field_193559_aa = new MapColor(50, 9321518);
                public static final MapColor field_193560_ab = new MapColor(51, 2430480);

                // Holds the color in RGB value that will be rendered on maps.
                public final int colorValue;

                // Holds the index of the color used on map.
                public final int colorIndex;

                private MapColor(int index, int color)
                {
                    if (index >= 0 && index <= 63)
                    {
                        this.colorIndex = index;
                        this.colorValue = color;
                        COLORS[index] = this;
                    }
                    else
                    {
                        throw new IndexOutOfBoundsException("Map colour ID must be between 0 and 63 (inclusive)");
                    }
                }

                public int getMapColor(int p_151643_1_)
                {
                    int i = 220;

                    if (p_151643_1_ == 3)
                    {
                        i = 135;
                    }

                    if (p_151643_1_ == 2)
                    {
                        i = 255;
                    }

                    if (p_151643_1_ == 1)
                    {
                        i = 220;
                    }

                    if (p_151643_1_ == 0)
                    {
                        i = 180;
                    }

                    int j = (this.colorValue >> 16 & 255) * i / 255;
                    int k = (this.colorValue >> 8 & 255) * i / 255;
                    int l = (this.colorValue & 255) * i / 255;
                    return -16777216 | j << 16 | k << 8 | l;
                }

                public static MapColor func_193558_a(EnumDyeColor p_193558_0_)
                {
                    return field_193575_b[p_193558_0_.getMetadata()];
                }

                static
                {
                    field_193575_b[EnumDyeColor.WHITE.getMetadata()] = SNOW;
                    field_193575_b[EnumDyeColor.ORANGE.getMetadata()] = ADOBE;
                    field_193575_b[EnumDyeColor.MAGENTA.getMetadata()] = MAGENTA;
                    field_193575_b[EnumDyeColor.LIGHT_BLUE.getMetadata()] = LIGHT_BLUE;
                    field_193575_b[EnumDyeColor.YELLOW.getMetadata()] = YELLOW;
                    field_193575_b[EnumDyeColor.LIME.getMetadata()] = LIME;
                    field_193575_b[EnumDyeColor.PINK.getMetadata()] = PINK;
                    field_193575_b[EnumDyeColor.GRAY.getMetadata()] = GRAY;
                    field_193575_b[EnumDyeColor.SILVER.getMetadata()] = SILVER;
                    field_193575_b[EnumDyeColor.CYAN.getMetadata()] = CYAN;
                    field_193575_b[EnumDyeColor.PURPLE.getMetadata()] = PURPLE;
                    field_193575_b[EnumDyeColor.BLUE.getMetadata()] = BLUE;
                    field_193575_b[EnumDyeColor.BROWN.getMetadata()] = BROWN;
                    field_193575_b[EnumDyeColor.GREEN.getMetadata()] = GREEN;
                    field_193575_b[EnumDyeColor.RED.getMetadata()] = RED;
                    field_193575_b[EnumDyeColor.BLACK.getMetadata()] = BLACK;
                }
            }
        */

        // Second function that has a multiplication by 4 of the color index, this let me decode it.
        /*public static void func_190905_a(World p_190905_0_, ItemStack p_190905_1_)
        {
            if (p_190905_1_.getItem() == Items.FILLED_MAP)
            {
                MapData mapdata = Items.FILLED_MAP.getMapData(p_190905_1_, p_190905_0_);

                if (mapdata != null)
                {
                    if (p_190905_0_.provider.getDimensionType().getId() == mapdata.dimension)
                    {
                        int i = 1 << mapdata.scale;
                        int j = mapdata.xCenter;
                        int k = mapdata.zCenter;
                        Biome[] abiome = p_190905_0_.getBiomeProvider().getBiomes((Biome[])null, (j / i - 64) * i, (k / i - 64) * i, 128 * i, 128 * i, false);

                        for (int l = 0; l < 128; ++l)
                        {
                            for (int i1 = 0; i1 < 128; ++i1)
                            {
                                int j1 = l * i;
                                int k1 = i1 * i;
                                Biome biome = abiome[j1 + k1 * 128 * i];
                                MapColor mapcolor = MapColor.AIR;
                                int l1 = 3;
                                int i2 = 8;

                                if (l > 0 && i1 > 0 && l < 127 && i1 < 127)
                                {
                                    if (abiome[(l - 1) * i + (i1 - 1) * i * 128 * i].getBaseHeight() >= 0.0F)
                                    {
                                        --i2;
                                    }

                                    if (abiome[(l - 1) * i + (i1 + 1) * i * 128 * i].getBaseHeight() >= 0.0F)
                                    {
                                        --i2;
                                    }

                                    if (abiome[(l - 1) * i + i1 * i * 128 * i].getBaseHeight() >= 0.0F)
                                    {
                                        --i2;
                                    }

                                    if (abiome[(l + 1) * i + (i1 - 1) * i * 128 * i].getBaseHeight() >= 0.0F)
                                    {
                                        --i2;
                                    }

                                    if (abiome[(l + 1) * i + (i1 + 1) * i * 128 * i].getBaseHeight() >= 0.0F)
                                    {
                                        --i2;
                                    }

                                    if (abiome[(l + 1) * i + i1 * i * 128 * i].getBaseHeight() >= 0.0F)
                                    {
                                        --i2;
                                    }

                                    if (abiome[l * i + (i1 - 1) * i * 128 * i].getBaseHeight() >= 0.0F)
                                    {
                                        --i2;
                                    }

                                    if (abiome[l * i + (i1 + 1) * i * 128 * i].getBaseHeight() >= 0.0F)
                                    {
                                        --i2;
                                    }

                                    if (biome.getBaseHeight() < 0.0F)
                                    {
                                        mapcolor = MapColor.ADOBE;

                                        if (i2 > 7 && i1 % 2 == 0)
                                        {
                                            l1 = (l + (int)(MathHelper.sin((float)i1 + 0.0F) * 7.0F)) / 8 % 5;

                                            if (l1 == 3)
                                            {
                                                l1 = 1;
                                            }
                                            else if (l1 == 4)
                                            {
                                                l1 = 0;
                                            }
                                        }
                                        else if (i2 > 7)
                                        {
                                            mapcolor = MapColor.AIR;
                                        }
                                        else if (i2 > 5)
                                        {
                                            l1 = 1;
                                        }
                                        else if (i2 > 3)
                                        {
                                            l1 = 0;
                                        }
                                        else if (i2 > 1)
                                        {
                                            l1 = 0;
                                        }
                                    }
                                    else if (i2 > 0)
                                    {
                                        mapcolor = MapColor.BROWN;

                                        if (i2 > 3)
                                        {
                                            l1 = 1;
                                        }
                                        else
                                        {
                                            l1 = 3;
                                        }
                                    }
                                }

                                if (mapcolor != MapColor.AIR)
                                {
                                    mapdata.colors[l + i1 * 128] = (byte)(mapcolor.colorIndex * 4 + l1);
                                    mapdata.updateMapData(l, i1);
                                }
                            }
                        }
                    }
                }
            }
        }*/
    }
}
