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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Contador_Densidades_Bloques : Form
    {
        public Ventana_Contador_Densidades_Bloques()
        {
            InitializeComponent();
        }

        internal static readonly short ID_Aire = Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:air"];
        internal static readonly short ID_Aire_Cuevas = Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:cave_air"];
        internal static readonly short ID_Aire_Vacío = Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:void_air"];

        internal readonly string Texto_Título = "Block Densities Counter by Jupisoft for " + Program.Texto_Usuario;
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

        internal int Variable_Y_Mínimo = 0;
        internal int Variable_Y_Máximo = 255;

        internal static Dictionary<short, object> Diccionario_Cuevas = null;

        internal Brush Pincel_Blanco_Negro = Brushes.White;
        internal ImageList Lista_Imágenes_16 = null;
        internal ImageList Lista_Imágenes_32 = null;
        internal List<string> Lista_Rutas_Mundos = new List<string>();
        internal Dictionary<string, string> Diccionario_Dimensiones_Rutas = new Dictionary<string, string>();
        internal List<string> Lista_Rutas_Regiones = new List<string>();
        internal Minecraft.Regiones Región = new Minecraft.Regiones();
        internal SortedDictionary<short, int[]> Diccionario_Bloques_Densidades = new SortedDictionary<short, int[]>();
        internal int Total_Chunks = 0;
        internal int Total_Bloques = 0;
        internal Bitmap Imagen_Cuadrícula = new Bitmap(512, 512, PixelFormat.Format32bppArgb);
        internal Bitmap Imagen_Chunks = new Bitmap(512, 512, PixelFormat.Format32bppArgb);
        internal Bitmap Imagen_Chunks_Arco_Iris = new Bitmap(512, 512, PixelFormat.Format32bppArgb);
        internal Bitmap Imagen_Biomas = new Bitmap(512, 512, PixelFormat.Format32bppArgb);
        internal Bitmap Imagen_Cuevas = new Bitmap(512, 512, PixelFormat.Format32bppArgb);
        internal Bitmap Imagen_Estructuras = new Bitmap(512, 512, PixelFormat.Format32bppArgb);
        internal Bitmap Imagen_Superficie = new Bitmap(512, 512, PixelFormat.Format32bppArgb);
        internal Bitmap Imagen_Superficie_Seca = new Bitmap(512, 512, PixelFormat.Format32bppArgb);
        internal Bitmap Imagen_Distribución = new Bitmap(512, 512, PixelFormat.Format32bppArgb);
        internal Bitmap Imagen_Densidad = new Bitmap(512, 512, PixelFormat.Format32bppArgb);
        internal Bitmap Imagen_Densidad_Arco_Iris = new Bitmap(512, 512, PixelFormat.Format32bppArgb);

        private void Ventana_Contador_Densidades_Bloques_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título + " - [Drag and drop any valid region file to load it's contents individually]";
                Menú_Contextual_Acerca.Text = "About " + Program.Texto_Programa + " " + Program.Texto_Versión + "...";
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = Variable_Siempre_Visible;

                // Start the dictionary used to find caves, whose blocks usually can have air below.
                if (Diccionario_Cuevas == null || Diccionario_Cuevas.Count <= 0)
                {
                    Diccionario_Cuevas = new Dictionary<short, object>();
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:air"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:cave_air"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:void_air"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:acacia_leaves"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:birch_leaves"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:dark_oak_leaves"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:jungle_leaves"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:oak_leaves"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:spruce_leaves"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:acacia_log"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:birch_log"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:dark_oak_log"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:jungle_log"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:oak_log"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:spruce_log"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:brown_mushroom_block"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:red_mushroom_block"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:mushroom_stem"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:vine"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:cocoa"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:snow"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:bee_nest"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:nether_wart_block"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:warped_wart_block"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:shroomlight"], null);
                    Diccionario_Cuevas.Add(Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:weeping_vines"], null);
                }

                Graphics Pintar_Cuadrícula = Graphics.FromImage(Imagen_Cuadrícula);
                Pintar_Cuadrícula.CompositingMode = CompositingMode.SourceCopy;
                Pintar_Cuadrícula.CompositingQuality = CompositingQuality.HighQuality;
                Pintar_Cuadrícula.InterpolationMode = InterpolationMode.NearestNeighbor;
                Pintar_Cuadrícula.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar_Cuadrícula.SmoothingMode = SmoothingMode.None;
                Pintar_Cuadrícula.TextRenderingHint = TextRenderingHint.AntiAlias;
                HatchBrush Pincel = new HatchBrush(HatchStyle.Percent50, Color.FromArgb(192, 128, 128, 128), Color.Transparent);
                for (int Índice_Y = 0; Índice_Y < 512; Índice_Y += 16)
                {
                    for (int Índice_X = 0; Índice_X < 512; Índice_X += 16)
                    {
                        Pintar_Cuadrícula.FillRectangle(Pincel, Índice_X, Índice_Y, 16, 16);
                        Pintar_Cuadrícula.FillRectangle(Brushes.Transparent, Índice_X + 1, Índice_Y + 1, 14, 14);
                    }
                }
                Pincel.Dispose();
                Pincel = null;
                Pintar_Cuadrícula.Dispose();
                Pintar_Cuadrícula = null;

                for (int Índice = 0, Índice_32 = 0; Índice < 256; Índice += 8, Índice_32++)
                {
                    Color Color_ARGB = Program.Obtener_Color_Puro_1530((Índice_32 * 1529) / 32);
                    if (Panel_Regla.Controls.ContainsKey("Panel_Regla_" + Índice.ToString()))
                    {
                        Panel Panel_Temporal = Panel_Regla.Controls["Panel_Regla_" + Índice.ToString()] as Panel;
                        if (Panel_Temporal != null)
                        {
                            Panel_Temporal.BackColor = Color_ARGB;
                        }
                    }
                    /*if (Panel_Regla.Controls.ContainsKey("Etiqueta_Regla_" + Índice.ToString()))
                    {
                        Label Etiqueta_Temporal = Panel_Regla.Controls["Etiqueta_Regla_" + Índice.ToString()] as Label;
                        if (Etiqueta_Temporal != null)
                        {
                            //Etiqueta_Temporal.BackColor = Color.Black;
                            //Etiqueta_Temporal.BackColor = Color_ARGB;
                            //Etiqueta_Temporal.ForeColor = Color_ARGB;
                        }
                    }*/
                }

                Lista_Imágenes_16 = new ImageList();
                Lista_Imágenes_16.ColorDepth = ColorDepth.Depth32Bit;
                Lista_Imágenes_16.ImageSize = new Size(16, 16);
                Lista_Imágenes_16.TransparentColor = Color.Empty;

                Lista_Imágenes_32 = new ImageList();
                Lista_Imágenes_32.ColorDepth = ColorDepth.Depth32Bit;
                Lista_Imágenes_32.ImageSize = new Size(32, 32);
                Lista_Imágenes_32.TransparentColor = Color.Empty;

                foreach (Minecraft.Bloques Bloque in Minecraft.Bloques.Matriz_Bloques)
                {
                    Lista_Imágenes_16.Images.Add(Bloque.Índice.ToString(), Bloque.Imagen_Textura);
                    Lista_Imágenes_32.Images.Add(Bloque.Índice.ToString(), Program.Obtener_Imagen_Miniatura(Bloque.Imagen_Textura, 32, 32, true, false, CheckState.Checked));
                }

                ListView_Bloques.SmallImageList = Lista_Imágenes_32;
                ListView_Bloques.LargeImageList = Lista_Imágenes_32;

                Cargar_Mundos();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Contador_Densidades_Bloques_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Contador_Densidades_Bloques_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Contador_Densidades_Bloques_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                // Always reset the last region and try to free some memory.
                Región = new Minecraft.Regiones();
                GC.Collect();
                GC.GetTotalMemory(true);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Contador_Densidades_Bloques_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
                int Ancho = (ListView_Bloques.ClientSize.Width - SystemInformation.VerticalScrollBarWidth) / 2;
                Columna_Bloque.Width = Ancho;
                Columna_Cantidad.Width = Ancho;
                Columna_ID.Width = 0;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Contador_Densidades_Bloques_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Contador_Densidades_Bloques_DragDrop(object sender, DragEventArgs e)
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

                                break;
                            }
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                        }
                        Matriz_Rutas = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Contador_Densidades_Bloques_KeyDown(object sender, KeyEventArgs e)
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

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Dimensión_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Región_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Mundo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Alt && !e.Control && !e.Shift)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Cargar_Dimensiones();
                        ComboBox_Dimensión.Select();
                        ComboBox_Dimensión.Focus();
                    }
                    else Ventana_Contador_Densidades_Bloques_KeyDown(sender, e);
                }
                else Ventana_Contador_Densidades_Bloques_KeyDown(sender, e);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Dimensión_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Alt && !e.Control && !e.Shift)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Cargar_Regiones();
                        ComboBox_Región.Select();
                        ComboBox_Región.Focus();
                    }
                    else Ventana_Contador_Densidades_Bloques_KeyDown(sender, e);
                }
                else Ventana_Contador_Densidades_Bloques_KeyDown(sender, e);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Región_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Alt && !e.Control && !e.Shift)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Cargar_Chunks();
                        ComboBox_Región.Select();
                        ComboBox_Región.Focus();
                    }
                    else Ventana_Contador_Densidades_Bloques_KeyDown(sender, e);
                }
                else Ventana_Contador_Densidades_Bloques_KeyDown(sender, e);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Cargar_Mundo_Click(object sender, EventArgs e)
        {
            try
            {
                Cargar_Dimensiones();
                ComboBox_Dimensión.Select();
                ComboBox_Dimensión.Focus();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Cargar_Dimensión_Click(object sender, EventArgs e)
        {
            try
            {
                Cargar_Regiones();
                ComboBox_Región.Select();
                ComboBox_Región.Focus();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Cargar_Región_Click(object sender, EventArgs e)
        {
            try
            {
                Cargar_Chunks();
                ComboBox_Región.Select();
                ComboBox_Región.Focus();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Y_Mínimo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Y_Mínimo = (int)NumericUpDown_Y_Mínimo.Value;
                Botón_Cargar_Región.PerformClick();
                NumericUpDown_Y_Mínimo.Select();
                NumericUpDown_Y_Mínimo.Focus();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Y_Máximo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Y_Máximo = (int)NumericUpDown_Y_Máximo.Value;
                Botón_Cargar_Región.PerformClick();
                NumericUpDown_Y_Máximo.Select();
                NumericUpDown_Y_Máximo.Focus();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ListView_Bloques_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Menú_Contextual_Mostrar_Mapa_Distribución.Checked)
                {
                    Cargar_Distribución();
                }
                else if (Menú_Contextual_Mostrar_Mapa_Densidad.Checked)
                {
                    Cargar_Densidad();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_Región_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.X > -1 && e.X < Picture_Región.ClientSize.Width &&
                    e.Y > -1 && e.Y < Picture_Región.ClientSize.Height)
                {
                    Barra_Estado_Etiqueta_Cursor.Text = "Cursor: Y = " + (255 - (e.Y / 2)).ToString();
                }
                else Barra_Estado_Etiqueta_Cursor.Text = "Cursor: Y = ?";
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_Región_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                Barra_Estado_Etiqueta_Cursor.Text = "Cursor: Y = ?";
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

        private void Menú_Contextual_Mostrar_Mapa_Chunks_Click(object sender, EventArgs e)
        {
            try
            {
                Menú_Contextual_Mostrar_Mapa_Biomas.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Cuevas.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Estructuras.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Superficie.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Superficie_Seca.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Distribución.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Densidad.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Chunks.Checked = true;

                Picture_Región.BackgroundImage = !Menú_Contextual_Dibujar_Mapas_Arco_Iris.Checked ? Imagen_Chunks : Imagen_Chunks_Arco_Iris;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Mostrar_Mapa_Biomas_Click(object sender, EventArgs e)
        {
            try
            {
                Menú_Contextual_Mostrar_Mapa_Chunks.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Cuevas.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Estructuras.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Superficie.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Superficie_Seca.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Distribución.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Densidad.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Biomas.Checked = true;

                Picture_Región.BackgroundImage = Imagen_Biomas;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Mostrar_Mapa_Cuevas_Click(object sender, EventArgs e)
        {
            try
            {
                Menú_Contextual_Mostrar_Mapa_Chunks.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Biomas.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Estructuras.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Superficie.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Superficie_Seca.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Distribución.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Densidad.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Cuevas.Checked = true;

                Picture_Región.BackgroundImage = Imagen_Cuevas;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Mostrar_Mapa_Estructuras_Click(object sender, EventArgs e)
        {
            try
            {
                Menú_Contextual_Mostrar_Mapa_Chunks.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Biomas.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Cuevas.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Superficie.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Superficie_Seca.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Distribución.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Densidad.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Estructuras.Checked = true;

                Picture_Región.BackgroundImage = Imagen_Estructuras;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Mostrar_Mapa_Superficie_Click(object sender, EventArgs e)
        {
            try
            {
                Menú_Contextual_Mostrar_Mapa_Chunks.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Biomas.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Cuevas.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Estructuras.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Superficie_Seca.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Distribución.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Densidad.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Superficie.Checked = true;

                Picture_Región.BackgroundImage = Imagen_Superficie;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Mostrar_Mapa_Superficie_Seca_Click(object sender, EventArgs e)
        {
            try
            {
                Menú_Contextual_Mostrar_Mapa_Chunks.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Biomas.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Cuevas.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Estructuras.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Superficie.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Distribución.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Densidad.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Superficie_Seca.Checked = true;

                Picture_Región.BackgroundImage = Imagen_Superficie_Seca;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Mostrar_Mapa_Distribución_Click(object sender, EventArgs e)
        {
            try
            {
                Menú_Contextual_Mostrar_Mapa_Chunks.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Biomas.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Cuevas.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Estructuras.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Superficie.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Superficie_Seca.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Densidad.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Distribución.Checked = true;

                ListView_Bloques_SelectedIndexChanged(ListView_Bloques, EventArgs.Empty);
                Picture_Región.BackgroundImage = Imagen_Distribución;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Mostrar_Mapa_Densidad_Click(object sender, EventArgs e)
        {
            try
            {
                Menú_Contextual_Mostrar_Mapa_Chunks.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Biomas.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Cuevas.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Estructuras.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Superficie.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Superficie_Seca.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Distribución.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Densidad.Checked = true;

                ListView_Bloques_SelectedIndexChanged(ListView_Bloques, EventArgs.Empty);
                Picture_Región.BackgroundImage = !Menú_Contextual_Dibujar_Mapas_Arco_Iris.Checked ? Imagen_Densidad : Imagen_Densidad_Arco_Iris;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Dibujar_Cuadrícula_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Picture_Región.Image = !Menú_Contextual_Dibujar_Cuadrícula.Checked ? null : Imagen_Cuadrícula;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Dibujar_Mapas_Arco_Iris_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Picture_Región.BackgroundImage = !Menú_Contextual_Dibujar_Mapas_Arco_Iris.Checked ? Imagen_Chunks : Imagen_Chunks_Arco_Iris;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Mostrar_Bloques_32_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Menú_Contextual_Mostrar_Bloques_32.Checked)
                {
                    ListView_Bloques.SmallImageList = Lista_Imágenes_16;
                    ListView_Bloques.LargeImageList = Lista_Imágenes_16;
                }
                else
                {
                    ListView_Bloques.SmallImageList = Lista_Imágenes_32;
                    ListView_Bloques.LargeImageList = Lista_Imágenes_32;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Fondo_Blanco_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Picture_Región.BackColor = !Menú_Contextual_Fondo_Blanco.Checked ? Color.Black : Color.White;
                Pincel_Blanco_Negro = !Menú_Contextual_Fondo_Blanco.Checked ? Brushes.White : Brushes.Black;
                if (ListView_Bloques.Items.Count > 0)
                {
                    ListView_Bloques_SelectedIndexChanged(ListView_Bloques, EventArgs.Empty);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (Picture_Región.BackgroundImage != null)
                {
                    Clipboard.SetImage(Picture_Región.BackgroundImage);
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
                if (Picture_Región.BackgroundImage != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado);
                    Picture_Región.BackgroundImage.Save(Program.Ruta_Guardado + "\\Density " + Program.Obtener_Nombre_Temporal() + ".png", ImageFormat.Png);
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

        internal Point Obtener_Posición_Región(string Ruta_Región)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta_Región))
                {
                    string Nombre = Path.GetFileNameWithoutExtension(Ruta_Región);
                    if (!string.IsNullOrEmpty(Nombre) && Nombre.StartsWith("r.", StringComparison.InvariantCultureIgnoreCase) && Nombre.Length >= 5)
                    {
                        string Texto_X = null;
                        string Texto_Z = null;
                        bool Coordenada_Z = false;
                        for (int Índice_Caracter = 2; Índice_Caracter < Nombre.Length; Índice_Caracter++)
                        {
                            if (Nombre[Índice_Caracter] == '-')
                            {
                                if (!Coordenada_Z)
                                {
                                    if (string.IsNullOrEmpty(Texto_X)) Texto_X += Nombre[Índice_Caracter];
                                }
                                else if (string.IsNullOrEmpty(Texto_Z)) Texto_Z += Nombre[Índice_Caracter];
                            }
                            else if (char.IsDigit(Nombre[Índice_Caracter]))
                            {
                                if (!Coordenada_Z) Texto_X += Nombre[Índice_Caracter];
                                else Texto_Z += Nombre[Índice_Caracter];
                            }
                            else if (Nombre[Índice_Caracter] == '.' && !Coordenada_Z) Coordenada_Z = true;
                        }
                        return new Point(int.Parse(Texto_X), int.Parse(Texto_Z));
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return Point.Empty;
        }

        internal string Obtener_Nombre_ID(short ID)
        {
            try
            {
                if (Minecraft.Bloques.Diccionario_Índice_Nombre.ContainsKey(ID))
                {
                    string Nombre = Minecraft.Bloques.Diccionario_Índice_Nombre[ID].Replace(' ', '_').Replace('~', '_').Replace('=', '_').Replace('+', '_').Replace('-', '_').Replace('\\', '_').Replace('/', '_').Replace(':', '_').Replace('*', '_').Replace('?', '_').Replace('\"', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_').Replace('.', '_').ToLowerInvariant().Replace("minecraft_", null).Replace('_', ' ');
                    return Nombre.Substring(0, 1).ToUpperInvariant() + Nombre.Substring(1);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        internal void Cargar_Mundos()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Always reset some controls.
                this.Text = Texto_Título + " - [Loading the existing worlds...]";
                ComboBox_Mundo.Items.Clear();
                ComboBox_Dimensión.Items.Clear();
                ComboBox_Región.Items.Clear();
                ListView_Bloques.Items.Clear();
                Picture_Región.BackgroundImage = null;
                TextBox_Densidad.Text = null;
                TextBox_Mejores_Alturas.Text = null;
                TextBox_Todas_Alturas.Text = null;

                // Force a form refresh.
                this.Refresh();

                // Always reset some variables.
                Lista_Rutas_Mundos.Clear();
                Diccionario_Dimensiones_Rutas.Clear();
                Lista_Rutas_Regiones.Clear();
                Total_Chunks = 0;
                Total_Bloques = 0;
                Diccionario_Bloques_Densidades.Clear();
                Región = new Minecraft.Regiones();

                // Try to collect some RAM memory.
                GC.Collect();
                GC.GetTotalMemory(true);

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
                        ComboBox_Mundo.SelectedIndex = 0;
                        Matriz_Rutas = null;
                    }
                }
                /*if (Directory.Exists(Program.Ruta_Guardado_Twitch))
                {
                    string[] Matriz_Rutas_Packs_Mods = Directory.GetDirectories(Program.Ruta_Guardado_Twitch, "*", SearchOption.TopDirectoryOnly);
                    if (Matriz_Rutas_Packs_Mods != null && Matriz_Rutas_Packs_Mods.Length > 0)
                    {
                        foreach (string Ruta_Pack_Mods in Matriz_Rutas_Packs_Mods)
                        {
                            if (Directory.Exists(Ruta_Pack_Mods + "\\saves"))
                            {
                                string[] Matriz_Rutas = Directory.GetDirectories(Ruta_Pack_Mods + "\\saves", "*", SearchOption.TopDirectoryOnly);
                                if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                                {
                                    if (Matriz_Rutas.Length > 1) Array.Sort(Matriz_Rutas);
                                    foreach (string Ruta in Matriz_Rutas)
                                    {
                                        ComboBox_Mundos_Curse.Items.Add("[" + Path.GetFileName(Ruta_Pack_Mods) + "]: " + Path.GetFileName(Ruta));
                                    }
                                    Lista_Rutas_Mundos_Twitch.AddRange(Matriz_Rutas);
                                    Matriz_Rutas = null;
                                }
                            }
                        }
                        if (ComboBox_Mundos_Curse.Items.Count > 0) ComboBox_Mundos_Curse.SelectedIndex = 0;
                    }
                }*/
                this.Text = Texto_Título + " - [Found worlds: " + Program.Traducir_Número(ComboBox_Mundo.Items.Count) + "]";
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        internal void Cargar_Dimensiones()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Always reset some controls.
                this.Text = Texto_Título + " - [Loading the existing dimensions...]";
                ComboBox_Dimensión.Items.Clear();
                ComboBox_Región.Items.Clear();
                ListView_Bloques.Items.Clear();
                Picture_Región.BackgroundImage = null;
                TextBox_Densidad.Text = null;
                TextBox_Mejores_Alturas.Text = null;
                TextBox_Todas_Alturas.Text = null;

                // Force a form refresh.
                this.Refresh();

                // Always reset some variables.
                Diccionario_Dimensiones_Rutas.Clear();
                Lista_Rutas_Regiones.Clear();
                Total_Chunks = 0;
                Total_Bloques = 0;
                Diccionario_Bloques_Densidades.Clear();
                Región = new Minecraft.Regiones();

                // Try to collect some RAM memory.
                GC.Collect();
                GC.GetTotalMemory(true);

                int Índice_Mundo = ComboBox_Mundo.SelectedIndex;
                if (Índice_Mundo > -1 && Índice_Mundo < Lista_Rutas_Mundos.Count)
                {
                    string Ruta_Mundo = Lista_Rutas_Mundos[Índice_Mundo];
                    Diccionario_Dimensiones_Rutas = Minecraft.Obtener_Diccionario_Rutas_Dimensiones(Ruta_Mundo);
                    if (Diccionario_Dimensiones_Rutas != null && Diccionario_Dimensiones_Rutas.Count > 0)
                    {
                        foreach (KeyValuePair<string, string> Entrada in Diccionario_Dimensiones_Rutas)
                        {
                            ComboBox_Dimensión.Items.Add(Entrada.Key);
                        }
                        if (ComboBox_Dimensión.Items.Count > 0) ComboBox_Dimensión.SelectedIndex = 0;
                    }
                    Ruta_Mundo = null;
                }
                this.Text = Texto_Título + " - [Found dimensions: " + Program.Traducir_Número(ComboBox_Dimensión.Items.Count) + "]";
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        internal void Cargar_Regiones()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Always reset some controls.
                this.Text = Texto_Título + " - [Loading the existing regions...]";
                ComboBox_Región.Items.Clear();
                ListView_Bloques.Items.Clear();
                Picture_Región.BackgroundImage = null;
                TextBox_Densidad.Text = null;
                TextBox_Mejores_Alturas.Text = null;
                TextBox_Todas_Alturas.Text = null;

                // Force a form refresh.
                this.Refresh();

                // Always reset some variables.
                Lista_Rutas_Regiones.Clear();
                Total_Chunks = 0;
                Total_Bloques = 0;
                Diccionario_Bloques_Densidades.Clear();
                Región = new Minecraft.Regiones();

                // Try to collect some RAM memory.
                GC.Collect();
                GC.GetTotalMemory(true);

                string Dimensión = ComboBox_Dimensión.Text;
                if (!string.IsNullOrEmpty(Dimensión) && Diccionario_Dimensiones_Rutas.ContainsKey(Dimensión))
                {
                    string Ruta_Dimensión = Diccionario_Dimensiones_Rutas[Dimensión];
                    if (!string.IsNullOrEmpty(Ruta_Dimensión) && Directory.Exists(Ruta_Dimensión))
                    {

                    }
                    string[] Matriz_Rutas_Regiones = Directory.GetFiles(Ruta_Dimensión, "*.mc*", SearchOption.TopDirectoryOnly);
                    if (Matriz_Rutas_Regiones != null && Matriz_Rutas_Regiones.Length > 0)
                    {
                        if (Matriz_Rutas_Regiones.Length > 1) Array.Sort(Matriz_Rutas_Regiones);
                        Lista_Rutas_Regiones.AddRange(Matriz_Rutas_Regiones);
                        foreach (string Ruta_Región in Matriz_Rutas_Regiones)
                        {
                            ComboBox_Región.Items.Add(Path.GetFileName(Ruta_Región));
                        }
                        if (ComboBox_Región.Items.Count > 0) ComboBox_Región.SelectedIndex = 0;
                        Matriz_Rutas_Regiones = null;
                    }
                    Ruta_Dimensión = null;
                }
                this.Text = Texto_Título + " - [Found regions: " + Program.Traducir_Número(ComboBox_Región.Items.Count) + "]";

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        internal void Cargar_Chunks()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Always reset some controls.
                this.Text = Texto_Título + " - [Loading the existing chunks...]";
                ListView_Bloques.Items.Clear();
                Picture_Región.BackgroundImage = null;
                TextBox_Densidad.Text = null;
                TextBox_Mejores_Alturas.Text = null;
                TextBox_Todas_Alturas.Text = null;

                // Force a form refresh.
                this.Refresh();

                // Always reset some variables.
                Total_Chunks = 0;
                Total_Bloques = 0;
                Diccionario_Bloques_Densidades.Clear();
                Región = new Minecraft.Regiones();

                // Try to collect some RAM memory.
                GC.Collect();
                GC.GetTotalMemory(true);

                string Dimensión = ComboBox_Dimensión.Text;
                if (!string.IsNullOrEmpty(Dimensión) && Diccionario_Dimensiones_Rutas.ContainsKey(Dimensión))
                {
                    string Ruta_Dimensión = Diccionario_Dimensiones_Rutas[Dimensión];
                    if (!string.IsNullOrEmpty(Ruta_Dimensión) && Directory.Exists(Ruta_Dimensión))
                    {
                        int Índice_Región = ComboBox_Región.SelectedIndex;
                        if (Índice_Región > -1 && Índice_Región < Lista_Rutas_Regiones.Count)
                        {
                            string Ruta_Región = Lista_Rutas_Regiones[Índice_Región];
                            Región = Minecraft.Cargar_Región(Ruta_Dimensión, Obtener_Posición_Región(Ruta_Región), true, false, false, Menú_Contextual_Habilitar_Mapa_Biomas.Checked, false);
                            if (Región.Iniciada && Región.Matriz_Chunks != null && Región.Matriz_Chunks.Length > 0)
                            {
                                Graphics Pintar_Chunks = Graphics.FromImage(Imagen_Chunks);
                                Pintar_Chunks.CompositingMode = CompositingMode.SourceCopy;
                                Pintar_Chunks.CompositingQuality = CompositingQuality.HighQuality;
                                Pintar_Chunks.InterpolationMode = InterpolationMode.NearestNeighbor;
                                Pintar_Chunks.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                Pintar_Chunks.SmoothingMode = SmoothingMode.None;
                                Pintar_Chunks.TextRenderingHint = TextRenderingHint.AntiAlias;
                                Pintar_Chunks.Clear(Color.Transparent);

                                Graphics Pintar_Chunks_Arco_Iris = Graphics.FromImage(Imagen_Chunks_Arco_Iris);
                                Pintar_Chunks_Arco_Iris.CompositingMode = CompositingMode.SourceCopy;
                                Pintar_Chunks_Arco_Iris.CompositingQuality = CompositingQuality.HighQuality;
                                Pintar_Chunks_Arco_Iris.InterpolationMode = InterpolationMode.NearestNeighbor;
                                Pintar_Chunks_Arco_Iris.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                Pintar_Chunks_Arco_Iris.SmoothingMode = SmoothingMode.None;
                                Pintar_Chunks_Arco_Iris.TextRenderingHint = TextRenderingHint.AntiAlias;
                                Pintar_Chunks_Arco_Iris.Clear(Color.Transparent);

                                Graphics Pintar_Biomas = Graphics.FromImage(Imagen_Biomas);
                                Pintar_Biomas.CompositingMode = CompositingMode.SourceCopy;
                                Pintar_Biomas.CompositingQuality = CompositingQuality.HighQuality;
                                Pintar_Biomas.InterpolationMode = InterpolationMode.NearestNeighbor;
                                Pintar_Biomas.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                Pintar_Biomas.SmoothingMode = SmoothingMode.None;
                                Pintar_Biomas.TextRenderingHint = TextRenderingHint.AntiAlias;
                                Pintar_Biomas.Clear(Color.Transparent);

                                Graphics Pintar_Cuevas = Graphics.FromImage(Imagen_Cuevas);
                                Pintar_Cuevas.CompositingMode = CompositingMode.SourceCopy;
                                Pintar_Cuevas.CompositingQuality = CompositingQuality.HighQuality;
                                Pintar_Cuevas.InterpolationMode = InterpolationMode.NearestNeighbor;
                                Pintar_Cuevas.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                Pintar_Cuevas.SmoothingMode = SmoothingMode.None;
                                Pintar_Cuevas.TextRenderingHint = TextRenderingHint.AntiAlias;
                                Pintar_Cuevas.Clear(Color.Transparent);

                                Graphics Pintar_Estructuras = Graphics.FromImage(Imagen_Estructuras);
                                Pintar_Estructuras.CompositingMode = CompositingMode.SourceCopy;
                                Pintar_Estructuras.CompositingQuality = CompositingQuality.HighQuality;
                                Pintar_Estructuras.InterpolationMode = InterpolationMode.NearestNeighbor;
                                Pintar_Estructuras.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                Pintar_Estructuras.SmoothingMode = SmoothingMode.None;
                                Pintar_Estructuras.TextRenderingHint = TextRenderingHint.AntiAlias;
                                Pintar_Estructuras.Clear(Color.Transparent);

                                Graphics Pintar_Superficie = Graphics.FromImage(Imagen_Superficie);
                                Pintar_Superficie.CompositingMode = CompositingMode.SourceCopy;
                                Pintar_Superficie.CompositingQuality = CompositingQuality.HighQuality;
                                Pintar_Superficie.InterpolationMode = InterpolationMode.NearestNeighbor;
                                Pintar_Superficie.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                Pintar_Superficie.SmoothingMode = SmoothingMode.None;
                                Pintar_Superficie.TextRenderingHint = TextRenderingHint.AntiAlias;
                                Pintar_Superficie.Clear(Color.Transparent);

                                Graphics Pintar_Superficie_Seca = Graphics.FromImage(Imagen_Superficie_Seca);
                                Pintar_Superficie_Seca.CompositingMode = CompositingMode.SourceCopy;
                                Pintar_Superficie_Seca.CompositingQuality = CompositingQuality.HighQuality;
                                Pintar_Superficie_Seca.InterpolationMode = InterpolationMode.NearestNeighbor;
                                Pintar_Superficie_Seca.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                Pintar_Superficie_Seca.SmoothingMode = SmoothingMode.None;
                                Pintar_Superficie_Seca.TextRenderingHint = TextRenderingHint.AntiAlias;
                                Pintar_Superficie_Seca.Clear(Color.Transparent);

                                short[,] Matriz_Cuevas = !Menú_Contextual_Habilitar_Mapa_Cuevas.Checked ? null : new short[512, 512];

                                Total_Chunks = 0;
                                for (int Índice_Chunk_Z = 0; Índice_Chunk_Z < 32; Índice_Chunk_Z++)
                                {
                                    for (int Índice_Chunk_X = 0; Índice_Chunk_X < 32; Índice_Chunk_X++)
                                    {
                                        if (Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Matriz_Bytes_IDs != null &&
                                            Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Matriz_Bytes_IDs.Length > 0)
                                        {
                                            Pintar_Chunks.FillRectangle(Pincel_Blanco_Negro, Índice_Chunk_X * 16, Índice_Chunk_Z * 16, 16, 16);

                                            SolidBrush Pincel = new SolidBrush(Program.Obtener_Color_Puro_1530(Program.Rand.Next(0, 1530)));
                                            Pintar_Chunks_Arco_Iris.FillRectangle(Pincel, Índice_Chunk_X * 16, Índice_Chunk_Z * 16, 16, 16);
                                            Pincel.Dispose();
                                            Pincel = null;

                                            Total_Chunks++;

                                            for (int Índice_Bloque_Z = 0; Índice_Bloque_Z < 16; Índice_Bloque_Z++)
                                            {
                                                for (int Índice_Bloque_X = 0; Índice_Bloque_X < 16; Índice_Bloque_X++)
                                                {
                                                    for (int Índice_Bloque_Y = Math.Min(Variable_Y_Máximo, Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Y_Máximo); Índice_Bloque_Y >= Variable_Y_Mínimo; Índice_Bloque_Y--)
                                                    {
                                                        short ID = Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Matriz_Bytes_IDs[Índice_Bloque_X, Índice_Bloque_Y, Índice_Bloque_Z];
                                                        if (!Diccionario_Bloques_Densidades.ContainsKey(ID)) // Add the new block.
                                                        {
                                                            Diccionario_Bloques_Densidades.Add(ID, new int[256]);
                                                        }
                                                        Diccionario_Bloques_Densidades[ID][Índice_Bloque_Y]++; // Always count 1.
                                                    }
                                                }
                                            }

                                            if (Menú_Contextual_Habilitar_Mapa_Biomas.Checked)
                                            {
                                                if (Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Matriz_Bytes_Biomas != null &&
                                                    Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Matriz_Bytes_Biomas.Length > 0)
                                                {
                                                    for (int Índice_Bloque_Z = 0; Índice_Bloque_Z < 16; Índice_Bloque_Z++)
                                                    {
                                                        for (int Índice_Bloque_X = 0; Índice_Bloque_X < 16; Índice_Bloque_X++)
                                                        {
                                                            short ID_Bioma = Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Matriz_Bytes_Biomas[Índice_Bloque_X, Índice_Bloque_Z];
                                                            // It's a known biome color.
                                                            if (Minecraft.Diccionario_Biomas_Colores.ContainsKey(ID_Bioma))
                                                            {
                                                                SolidBrush Pincel_Bioma = new SolidBrush(Minecraft.Diccionario_Biomas_Colores[ID_Bioma]);
                                                                Pintar_Biomas.FillRectangle(Pincel_Bioma, (Índice_Chunk_X * 16) + Índice_Bloque_X, (Índice_Chunk_Z * 16) + Índice_Bloque_Z, 1, 1);
                                                                Pincel_Bioma.Dispose();
                                                                Pincel_Bioma = null;
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            if (Menú_Contextual_Habilitar_Mapa_Cuevas.Checked)
                                            {
                                                for (int Índice_Bloque_Z = 0; Índice_Bloque_Z < 16; Índice_Bloque_Z++)
                                                {
                                                    for (int Índice_Bloque_X = 0; Índice_Bloque_X < 16; Índice_Bloque_X++)
                                                    {
                                                        bool Aire_Encontrado = true;
                                                        short Cuevas_Subterráneas = 0;
                                                        for (int Índice_Bloque_Y = Math.Min(Variable_Y_Máximo, Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Y_Máximo); Índice_Bloque_Y >= Variable_Y_Mínimo; Índice_Bloque_Y--)
                                                        {
                                                            short ID = Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Matriz_Bytes_IDs[Índice_Bloque_X, Índice_Bloque_Y, Índice_Bloque_Z];

                                                            if (!Diccionario_Cuevas.ContainsKey(ID))
                                                            {
                                                                if (Aire_Encontrado)
                                                                {
                                                                    Cuevas_Subterráneas++;
                                                                    Aire_Encontrado = false;
                                                                }
                                                            }
                                                            else Aire_Encontrado = true;
                                                        }
                                                        Matriz_Cuevas[(Índice_Chunk_X * 16) + Índice_Bloque_X, (Índice_Chunk_Z * 16) + Índice_Bloque_Z] = Cuevas_Subterráneas;
                                                    }
                                                }
                                            }

                                            if (Menú_Contextual_Habilitar_Mapa_Estructuras.Checked)
                                            {
                                                for (int Índice_Bloque_Z = 0; Índice_Bloque_Z < 16; Índice_Bloque_Z++)
                                                {
                                                    for (int Índice_Bloque_X = 0; Índice_Bloque_X < 16; Índice_Bloque_X++)
                                                    {
                                                        for (int Índice_Bloque_Y = Math.Min(Variable_Y_Máximo, Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Y_Máximo); Índice_Bloque_Y >= Variable_Y_Mínimo; Índice_Bloque_Y--)
                                                        {
                                                            short ID = Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Matriz_Bytes_IDs[Índice_Bloque_X, Índice_Bloque_Y, Índice_Bloque_Z];

                                                            // It's a known hidden structure block.
                                                            if (Minecraft.Bloques.Diccionario_Índice_Estructuras_Ocultas.ContainsKey(ID))
                                                            {
                                                                // It's a known block color.
                                                                if (Minecraft.Bloques.Diccionario_Índice_Color_ARGB.ContainsKey(ID))
                                                                {
                                                                    SolidBrush Pincel_Estructuras = new SolidBrush(Program.Obtener_Color_3D_Sólido(Minecraft.Bloques.Diccionario_Índice_Color_ARGB[ID], Índice_Bloque_Y));
                                                                    Pintar_Estructuras.FillRectangle(Pincel_Estructuras, (Índice_Chunk_X * 16) + Índice_Bloque_X, (Índice_Chunk_Z * 16) + Índice_Bloque_Z, 1, 1);
                                                                    Pincel_Estructuras.Dispose();
                                                                    Pincel_Estructuras = null;
                                                                }
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            if (Menú_Contextual_Habilitar_Mapas_Superficie.Checked)
                                            {
                                                for (int Índice_Bloque_Z = 0; Índice_Bloque_Z < 16; Índice_Bloque_Z++)
                                                {
                                                    for (int Índice_Bloque_X = 0; Índice_Bloque_X < 16; Índice_Bloque_X++)
                                                    {
                                                        bool Dibujar_Superficie = true;
                                                        bool Dibujar_Superficie_Seca = true;
                                                        for (int Índice_Bloque_Y = Math.Min(Variable_Y_Máximo, Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Y_Máximo); Índice_Bloque_Y >= Variable_Y_Mínimo; Índice_Bloque_Y--)
                                                        {
                                                            short ID = Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Matriz_Bytes_IDs[Índice_Bloque_X, Índice_Bloque_Y, Índice_Bloque_Z];

                                                            if (Dibujar_Superficie && // Still needs to draw this pixel.
                                                                ID != ID_Aire && // It's not air a type of air.
                                                                ID != ID_Aire_Cuevas &&
                                                                ID != ID_Aire_Vacío)
                                                            {
                                                                // It's a known block color.
                                                                if (Minecraft.Bloques.Diccionario_Índice_Color_ARGB.ContainsKey(ID))
                                                                {
                                                                    SolidBrush Pincel_Superficie = new SolidBrush(Program.Obtener_Color_3D_Sólido(Minecraft.Bloques.Diccionario_Índice_Color_ARGB[ID], Índice_Bloque_Y));
                                                                    Pintar_Superficie.FillRectangle(Pincel_Superficie, (Índice_Chunk_X * 16) + Índice_Bloque_X, (Índice_Chunk_Z * 16) + Índice_Bloque_Z, 1, 1);
                                                                    Pincel_Superficie.Dispose();
                                                                    Pincel_Superficie = null;
                                                                    Dibujar_Superficie = false; // Don't draw again this pixel.
                                                                }
                                                            }

                                                            if (Dibujar_Superficie_Seca && // Still needs to draw this pixel.
                                                                !Minecraft.Diccionario_Bloques_Transparentes.ContainsKey(ID)) // Not transparent.
                                                            {
                                                                // It's a known block color.
                                                                if (Minecraft.Bloques.Diccionario_Índice_Color_ARGB.ContainsKey(ID))
                                                                {
                                                                    SolidBrush Pincel_Superficie_Seca = new SolidBrush(Program.Obtener_Color_3D_Sólido(Minecraft.Bloques.Diccionario_Índice_Color_ARGB[ID], Índice_Bloque_Y));
                                                                    Pintar_Superficie_Seca.FillRectangle(Pincel_Superficie_Seca, (Índice_Chunk_X * 16) + Índice_Bloque_X, (Índice_Chunk_Z * 16) + Índice_Bloque_Z, 1, 1);
                                                                    Pincel_Superficie_Seca.Dispose();
                                                                    Pincel_Superficie_Seca = null;
                                                                    Dibujar_Superficie_Seca = false; // Don't draw again this pixel.
                                                                }
                                                            }

                                                            // We are done with this pixel.
                                                            if (!Dibujar_Superficie && !Dibujar_Superficie_Seca)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            // Get all the block densities.
                                            for (int Índice_Bloque_Z = 0; Índice_Bloque_Z < 16; Índice_Bloque_Z++)
                                            {
                                                for (int Índice_Bloque_X = 0; Índice_Bloque_X < 16; Índice_Bloque_X++)
                                                {
                                                    for (int Índice_Bloque_Y = Math.Min(Variable_Y_Máximo, Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Y_Máximo); Índice_Bloque_Y >= Variable_Y_Mínimo; Índice_Bloque_Y--)
                                                    {
                                                        short ID = Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Matriz_Bytes_IDs[Índice_Bloque_X, Índice_Bloque_Y, Índice_Bloque_Z];
                                                        if (!Diccionario_Bloques_Densidades.ContainsKey(ID)) // Add the new block.
                                                        {
                                                            Diccionario_Bloques_Densidades.Add(ID, new int[256]);
                                                        }
                                                        Diccionario_Bloques_Densidades[ID][Índice_Bloque_Y]++; // Always count 1.
                                                    }
                                                }
                                            }
                                        }
                                        /*else // This chunk doesn't exist.
                                        {
                                            //Pintar_Chunks.FillRectangle(Brushes.Red, Índice_Chunk_X, Índice_Chunk_Z, 1, 1);
                                        }*/
                                    }
                                }
                                Total_Bloques = Total_Chunks * 65536;

                                // This needs a post drawing to find the maximum cave height at once.
                                if (Menú_Contextual_Habilitar_Mapa_Cuevas.Checked)
                                {
                                    // Find the maximum cave height.
                                    short Máximo_Cuevas = 0;
                                    for (int Índice_Chunk_Z = 0; Índice_Chunk_Z < 32; Índice_Chunk_Z++)
                                    {
                                        for (int Índice_Chunk_X = 0; Índice_Chunk_X < 32; Índice_Chunk_X++)
                                        {
                                            if (Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Matriz_Bytes_IDs != null &&
                                                Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Matriz_Bytes_IDs.Length > 0)
                                            {
                                                for (int Índice_Bloque_Z = 0; Índice_Bloque_Z < 16; Índice_Bloque_Z++)
                                                {
                                                    for (int Índice_Bloque_X = 0; Índice_Bloque_X < 16; Índice_Bloque_X++)
                                                    {
                                                        if (Matriz_Cuevas[(Índice_Chunk_X * 16) + Índice_Bloque_X, (Índice_Chunk_Z * 16) + Índice_Bloque_Z] > Máximo_Cuevas) Máximo_Cuevas = Matriz_Cuevas[(Índice_Chunk_X * 16) + Índice_Bloque_X, (Índice_Chunk_Z * 16) + Índice_Bloque_Z];
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    // Now draw all the found caves.
                                    for (int Índice_Chunk_Z = 0; Índice_Chunk_Z < 32; Índice_Chunk_Z++)
                                    {
                                        for (int Índice_Chunk_X = 0; Índice_Chunk_X < 32; Índice_Chunk_X++)
                                        {
                                            if (Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Matriz_Bytes_IDs != null &&
                                                Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Matriz_Bytes_IDs.Length > 0)
                                            {
                                                for (int Índice_Bloque_Z = 0; Índice_Bloque_Z < 16; Índice_Bloque_Z++)
                                                {
                                                    for (int Índice_Bloque_X = 0; Índice_Bloque_X < 16; Índice_Bloque_X++)
                                                    {
                                                        // TODO: improve a bit the gray scale gradient?
                                                        int Valor = Máximo_Cuevas > 0 ? 255 - ((Math.Min((int)Matriz_Cuevas[(Índice_Chunk_X * 16) + Índice_Bloque_X, (Índice_Chunk_Z * 16) + Índice_Bloque_Z], 255) * 255) / Máximo_Cuevas) : 255;
                                                        SolidBrush Pincel_Cuevas = new SolidBrush(Color.FromArgb(255, Valor, Valor, Valor));
                                                        Pintar_Cuevas.FillRectangle(Pincel_Cuevas, (Índice_Chunk_X * 16) + Índice_Bloque_X, (Índice_Chunk_Z * 16) + Índice_Bloque_Z, 1, 1);
                                                        Pincel_Cuevas.Dispose();
                                                        Pincel_Cuevas = null;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    Matriz_Cuevas = null;
                                }

                                Pintar_Chunks.Dispose();
                                Pintar_Chunks = null;
                                Pintar_Chunks_Arco_Iris.Dispose();
                                Pintar_Chunks_Arco_Iris = null;
                                Pintar_Biomas.Dispose();
                                Pintar_Biomas = null;
                                Pintar_Cuevas.Dispose();
                                Pintar_Cuevas = null;
                                Pintar_Estructuras.Dispose();
                                Pintar_Estructuras = null;
                                Pintar_Superficie.Dispose();
                                Pintar_Superficie = null;
                                Pintar_Superficie_Seca.Dispose();
                                Pintar_Superficie_Seca = null;

                                Picture_Región.BackgroundImage = !Menú_Contextual_Dibujar_Mapas_Arco_Iris.Checked ? Imagen_Chunks : Imagen_Chunks_Arco_Iris;

                                if (Diccionario_Bloques_Densidades.Count > 0)
                                {
                                    foreach (KeyValuePair<short, int[]> Entrada in Diccionario_Bloques_Densidades)
                                    {
                                        int Total = 0;
                                        for (int Índice_Y = Variable_Y_Mínimo; Índice_Y <= Variable_Y_Máximo; Índice_Y++)
                                        {
                                            Total += Entrada.Value[Índice_Y];
                                        }
                                        ListViewItem Bloque = new ListViewItem(new string[] { Obtener_Nombre_ID(Entrada.Key), Program.Traducir_Número(Total), Entrada.Key.ToString() }, Entrada.Key.ToString());
                                        ListView_Bloques.Items.Add(Bloque);
                                    }
                                }
                            }
                        }
                    }
                }
                this.Text = Texto_Título + " - [Existing region: " + Program.Traducir_Número_Decimales_Redondear(((double)Total_Chunks * 100d) / 1024d, 4) + " %, Chunks: " + Program.Traducir_Número(Total_Chunks) + " of 1.024, Blocks: " + Program.Traducir_Número(Total_Bloques) + " of 67.108.864]";
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        internal void Cargar_Densidad()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Always reset some controls.
                this.Text = Texto_Título + " - [Loading the existing chunks...]";
                Picture_Región.BackgroundImage = null;
                TextBox_Densidad.Text = null;
                TextBox_Mejores_Alturas.Text = null;
                TextBox_Todas_Alturas.Text = null;

                // Force a form refresh.
                this.Refresh();

                if (ListView_Bloques.SelectedIndices != null && ListView_Bloques.SelectedIndices.Count > 0 && Diccionario_Bloques_Densidades.Count > 0)
                {
                    Graphics Pintar_Densidad = Graphics.FromImage(Imagen_Densidad);
                    Pintar_Densidad.CompositingMode = CompositingMode.SourceCopy;
                    Pintar_Densidad.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar_Densidad.InterpolationMode = InterpolationMode.NearestNeighbor;
                    Pintar_Densidad.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar_Densidad.SmoothingMode = SmoothingMode.None;
                    Pintar_Densidad.TextRenderingHint = TextRenderingHint.AntiAlias;
                    Pintar_Densidad.Clear(Color.Transparent);

                    Graphics Pintar_Densidad_Arco_Iris = Graphics.FromImage(Imagen_Densidad_Arco_Iris);
                    Pintar_Densidad_Arco_Iris.CompositingMode = CompositingMode.SourceCopy;
                    Pintar_Densidad_Arco_Iris.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar_Densidad_Arco_Iris.InterpolationMode = InterpolationMode.NearestNeighbor;
                    Pintar_Densidad_Arco_Iris.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar_Densidad_Arco_Iris.SmoothingMode = SmoothingMode.None;
                    Pintar_Densidad_Arco_Iris.TextRenderingHint = TextRenderingHint.AntiAlias;
                    Pintar_Densidad_Arco_Iris.Clear(Color.Transparent);

                    int Índice_Bloque = ListView_Bloques.SelectedIndices[0];
                    int Índice = 0;
                    foreach (KeyValuePair<short, int[]> Entrada in Diccionario_Bloques_Densidades)
                    {
                        if (Índice == Índice_Bloque)
                        {
                            int Máximo = 0;
                            for (int Índice_Y = Variable_Y_Mínimo; Índice_Y <= Variable_Y_Máximo; Índice_Y++)
                            {
                                if (Entrada.Value[Índice_Y] > Máximo) Máximo = Entrada.Value[Índice_Y];
                            }
                            if (Máximo > 0)
                            {
                                int Total = 0;
                                int Niveles_Y = 0;
                                string Texto_Mejores_Alturas = null;
                                string Texto_Todas_Alturas = null;
                                for (int Índice_Y = Variable_Y_Mínimo; Índice_Y <= Variable_Y_Máximo; Índice_Y++)
                                {
                                    Total += Entrada.Value[Índice_Y];
                                    if (Entrada.Value[Índice_Y] >= Máximo)
                                    {
                                        Texto_Mejores_Alturas += Índice_Y.ToString() + ", ";
                                    }
                                    if (Entrada.Value[Índice_Y] > 0)
                                    {
                                        Niveles_Y++;
                                        Texto_Todas_Alturas += Índice_Y.ToString() + ", ";
                                    }

                                    int Ancho = (Entrada.Value[Índice_Y] * 512) / Máximo;
                                    if (Entrada.Value[Índice_Y] > 0 && Ancho <= 0) Ancho = 1; // Minimum of 1.

                                    Pintar_Densidad.FillRectangle(Pincel_Blanco_Negro, 0, 510 - (Índice_Y * 2), Ancho, 2);

                                    //SolidBrush Pincel = new SolidBrush(Program.Obtener_Color_Puro_1530((Índice_Y * 1529) / 255));
                                    SolidBrush Pincel = new SolidBrush(Program.Obtener_Color_Puro_1530((Ancho * 1529) / 512));
                                    Pintar_Densidad_Arco_Iris.FillRectangle(Pincel, 0, 510 - (Índice_Y * 2), Ancho, 2);
                                    Pincel.Dispose();
                                    Pincel = null;
                                }

                                TextBox_Densidad.Text = Program.Traducir_Número_Decimales_Redondear(((double)Total * 100d) / (double)Total_Bloques, 4) + " %, 1 each " + Program.Traducir_Número_Decimales_Redondear((double)Total_Bloques / (double)Total, 4) + " blocks, found at " + Program.Traducir_Número(Niveles_Y) + " of the 256 height levels";
                                if (!string.IsNullOrEmpty(Texto_Mejores_Alturas))
                                {
                                    if (Texto_Mejores_Alturas.EndsWith(", "))
                                    {
                                        Texto_Mejores_Alturas = Texto_Mejores_Alturas.TrimEnd(", ".ToCharArray());
                                    }
                                    TextBox_Mejores_Alturas.Text = Texto_Mejores_Alturas;
                                }
                                if (!string.IsNullOrEmpty(Texto_Todas_Alturas))
                                {
                                    if (Texto_Todas_Alturas.EndsWith(", "))
                                    {
                                        Texto_Todas_Alturas = Texto_Todas_Alturas.TrimEnd(", ".ToCharArray());
                                    }
                                    TextBox_Todas_Alturas.Text = Texto_Todas_Alturas;
                                }
                            }
                            break;
                        }
                        Índice++; // Always increase the block counter.
                    }
                    Pintar_Densidad.Dispose();
                    Pintar_Densidad = null;
                    Pintar_Densidad_Arco_Iris.Dispose();
                    Pintar_Densidad_Arco_Iris = null;
                    Picture_Región.BackgroundImage = !Menú_Contextual_Dibujar_Mapas_Arco_Iris.Checked ? Imagen_Densidad : Imagen_Densidad_Arco_Iris;
                }
                this.Text = Texto_Título + " - [Existing region: " + Program.Traducir_Número_Decimales_Redondear(((double)Total_Chunks * 100d) / 1024d, 4) + " %, Chunks: " + Program.Traducir_Número(Total_Chunks) + " of 1.024, Blocks: " + Program.Traducir_Número(Total_Bloques) + " of 67.108.864]";
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        internal void Cargar_Distribución()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Always reset some controls.
                this.Text = Texto_Título + " - [Loading the existing chunks...]";
                Picture_Región.BackgroundImage = null;

                // Force a form refresh.
                this.Refresh();

                if (ListView_Bloques.SelectedIndices != null && ListView_Bloques.SelectedIndices.Count > 0 && Diccionario_Bloques_Densidades.Count > 0)
                {
                    int Índice_Bloque = short.Parse(ListView_Bloques.SelectedItems[0].SubItems[2].Text);
                    if (Región.Iniciada && Región.Matriz_Chunks != null && Región.Matriz_Chunks.Length > 0)
                    {
                        Graphics Pintar_Distribución = Graphics.FromImage(Imagen_Distribución);
                        Pintar_Distribución.CompositingMode = CompositingMode.SourceCopy;
                        Pintar_Distribución.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar_Distribución.InterpolationMode = InterpolationMode.NearestNeighbor;
                        Pintar_Distribución.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar_Distribución.SmoothingMode = SmoothingMode.None;
                        Pintar_Distribución.TextRenderingHint = TextRenderingHint.AntiAlias;
                        Pintar_Distribución.Clear(Color.Transparent);

                        for (int Índice_Chunk_Z = 0; Índice_Chunk_Z < 32; Índice_Chunk_Z++)
                        {
                            for (int Índice_Chunk_X = 0; Índice_Chunk_X < 32; Índice_Chunk_X++)
                            {
                                if (Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Matriz_Bytes_IDs != null &&
                                    Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Matriz_Bytes_IDs.Length > 0)
                                {
                                    for (int Índice_Bloque_Z = 0; Índice_Bloque_Z < 16; Índice_Bloque_Z++)
                                    {
                                        for (int Índice_Bloque_X = 0; Índice_Bloque_X < 16; Índice_Bloque_X++)
                                        {
                                            for (int Índice_Bloque_Y = Variable_Y_Mínimo; Índice_Bloque_Y <= Math.Min(Variable_Y_Máximo, Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Y_Máximo); Índice_Bloque_Y++)
                                            {
                                                short ID = Región.Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Matriz_Bytes_IDs[Índice_Bloque_X, Índice_Bloque_Y, Índice_Bloque_Z];
                                                if (ID == Índice_Bloque)
                                                {
                                                    /*if (Minecraft.Bloques.Diccionario_Índice_Color_ARGB.ContainsKey(ID))
                                                    {
                                                        SolidBrush Pincel = new SolidBrush(Minecraft.Bloques.Diccionario_Índice_Color_ARGB[ID]);
                                                        Pintar_Posiciones.FillRectangle(Pincel, (Índice_Chunk_X * 16) + Índice_Bloque_X, (Índice_Chunk_Z * 16) + Índice_Bloque_Z, 1, 1);
                                                        Pincel.Dispose();
                                                        Pincel = null;
                                                    }*/
                                                    Pintar_Distribución.FillRectangle(Pincel_Blanco_Negro, (Índice_Chunk_X * 16) + Índice_Bloque_X, (Índice_Chunk_Z * 16) + Índice_Bloque_Z, 1, 1);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        Pintar_Distribución.Dispose();
                        Pintar_Distribución = null;
                        Picture_Región.BackgroundImage = Imagen_Distribución;
                    }
                }
                this.Text = Texto_Título + " - [Existing region: " + Program.Traducir_Número_Decimales_Redondear(((double)Total_Chunks * 100d) / 1024d, 4) + " %, Chunks: " + Program.Traducir_Número(Total_Chunks) + " of 1.024, Blocks: " + Program.Traducir_Número(Total_Bloques) + " of 67.108.864]";
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
