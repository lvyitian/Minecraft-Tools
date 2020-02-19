using ImageMagick;
using Ionic.Zlib;
using Microsoft.Win32;
using Minecraft_Tools.Properties;
using Substrate;
using Substrate.Core;
using Substrate.Nbt;
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
    public partial class Ventana_Conversor_Mundos_1_13_a_1_12_2 : Form
    {
        public Ventana_Conversor_Mundos_1_13_a_1_12_2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Alphabetical and numerical sorter designed to sort block properties.
        /// </summary>
        internal class Comparador_String : IComparer<string>
        {
            public int Compare(string X, string Y)
            {
                if (!string.IsNullOrEmpty(X) && !string.IsNullOrEmpty(Y))
                {
                    int Índice_X = X.IndexOf(':');
                    int Índice_Y = Y.IndexOf(':');
                    if (Índice_X > -1 && Índice_Y > -1)
                    {
                        int Longitud_Máxima = Math.Max(X.Length, Y.Length);
                        return string.Compare(X.Substring(0, Índice_X + 1) + new string(char.IsNumber(X[Índice_X + 2]) ? '0' : X[Índice_X + 2], Longitud_Máxima - X.Length) + X.Substring(Índice_X + 2), Y.Substring(0, Índice_Y + 1) + new string(char.IsNumber(Y[Índice_Y + 2]) ? '0' : Y[Índice_Y + 2], Longitud_Máxima - Y.Length) + Y.Substring(Índice_Y + 2));
                    }
                    else return string.Compare(X, Y);
                }
                else return string.Compare(X, Y);
            }
        }

        /// <summary>
        /// Array used to quickly get the palette block indexes by combining some of it's values.
        /// </summary>
        internal static readonly int[] Matriz_Máscaras_Bits = new int[]
        {
            1, // 1 bit
            3,
            7,
            15,
            31,
            63,
            127,
            255,
            511,
            1023,
            2047,
            4095 // 12 bits
        };

        internal static readonly short Índice_Aire = Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:air"];
        internal static readonly short Índice_Agua = Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:water"];
        internal static readonly short Índice_Lava = Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:lava"];
        internal static readonly short Índice_Agua_Fluyendo = Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:flowing_water"];
        internal static readonly short Índice_Lava_Fluyendo = Minecraft.Bloques.Diccionario_Nombre_Índice["minecraft:flowing_lava"];

        // General window variables.
        internal readonly string Texto_Título = "Minecraft 1.13+ to 1.12.2- World Converter by Jupisoft for " + Program.Texto_Usuario;
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
        /// Any text stored in this variable will be copid to the clipboard in a safe way.
        /// </summary>
        internal string Texto_Portapapeles = null;

        // Variables used to save and reload the window settings each time it starts.
        internal static int Variable_Dimensión_Overworld = 0;
        internal static int Variable_Dimensión_Nether = 1;
        internal static int Variable_Dimensión_The_End = 2;
        internal static int Variable_Biomas = 0;
        internal static int Variable_Bioma_Vacío = 1;
        internal static int Variable_Luz = 16;
        internal static bool Variable_Mundo_Invertido = false;
        internal static bool Variable_Mundo_Invertido_Suelo = false;
        internal static bool Variable_Auto_Destrucción = false;
        internal static bool Variable_Mundo_Agua = false;
        internal static bool Variable_Mundo_Lava = false;

        // Temporal variables used in each world conversion.
        internal string Ruta_Mundo = null;
        internal string Ruta_Regiones_Overworld = null;
        internal string Ruta_Regiones_Nether = null;
        internal string Ruta_Regiones_The_End = null;
        Rectangle Rectángulo_Dimensiones_Overworld;
        Rectangle Rectángulo_Dimensiones_Nether;
        Rectangle Rectángulo_Dimensiones_The_End;
        List<Point> Lista_Posiciones_Regiones_Overworld = null;
        List<Point> Lista_Posiciones_Regiones_Nether = null;
        List<Point> Lista_Posiciones_Regiones_The_End = null;
        internal List<string> Lista_Rutas_Mundos_Minecraft = new List<string>();
        internal Thread Subproceso = null;
        internal bool Pendiente_Subproceso_Abortar = false;
        internal bool Subproceso_Activo = false;

        /// <summary>
        /// Dictionary used to change block types into another block types (like lava into water or netherrack to diamond ore). Any block conversion is valid with this, even converting different block types to the same one, like all the ores to stone.
        /// </summary>
        internal static SortedDictionary<string, string> Diccionario_1_13_a_1_12_2 = null;
        /// <summary>
        /// Dictionary used to change block types into another block types (like lava into water or netherrack to diamond ore). Any block conversion is valid with this, even converting different block types to the same one, like all the ores to stone.
        /// </summary>
        internal static SortedDictionary<string, string> Diccionario_Transmutación = new SortedDictionary<string, string>();
        /// <summary>
        /// Dictionary used to change most blocks into only a few ones (like turning any block to wool, but based on the average texture color of the original block, so it will fit to the most similar wool color). There are 5 types of blocks that will never be converted with this: air, water, lava, flowing water and flowing lava.
        /// </summary>
        internal static SortedDictionary<string, string> Diccionario_Cuantización = new SortedDictionary<string, string>();
        /// <summary>
        /// Temporary dictionary to really quantize all the Minecraft blocks.
        /// </summary>
        Dictionary<string, string> Diccionario_Cuantización_Final = new Dictionary<string, string>();

        /// <summary>
        /// Useful funtion designed to search all the block colors of the same type, and draw a part of all of it's textures in only one, as seen on the quantization context menu of this window, so it's programming itself.
        /// </summary>
        internal void Crear_Imágenes_16_Texturas()
        {
            try
            {
                string[] Matriz_Nombres_Colores = new string[]
                {
                    "white_",
                    "orange_",
                    "magenta_",
                    "light_blue_",
                    "yellow_",
                    "lime_",
                    "pink_",
                    "gray_",
                    "light_gray_",
                    "cyan_",
                    "purple_",
                    "blue_",
                    "brown_",
                    "green_",
                    "red_",
                    "black_"
                };
                Point[] Matriz_Posiciones = new Point[]
                {
                    new Point(0, 0),
                    new Point(4, 0),
                    new Point(8, 0),
                    new Point(12, 0),
                    new Point(0, 4),
                    new Point(4, 4),
                    new Point(8, 4),
                    new Point(12, 4),
                    new Point(0, 8),
                    new Point(4, 8),
                    new Point(8, 8),
                    new Point(12, 8),
                    new Point(0, 12),
                    new Point(4, 12),
                    new Point(8, 12),
                    new Point(12, 12)
                };
                SortedDictionary<string, Bitmap> Diccionario_Imágenes = new SortedDictionary<string, Bitmap>();
                SortedDictionary<string, Graphics> Diccionario_Pintar = new SortedDictionary<string, Graphics>();
                foreach (Minecraft.Bloques Bloque in Minecraft.Bloques.Matriz_Bloques)
                {
                    for (int Índice = 0; Índice < 16; Índice++)
                    {
                        if (Bloque.Nombre_1_13.StartsWith("minecraft:" + Matriz_Nombres_Colores[Índice]))
                        {
                            string Nombre = Bloque.Nombre_1_13.Replace("minecraft:" + Matriz_Nombres_Colores[Índice], null);
                            if (!Diccionario_Imágenes.ContainsKey(Nombre))
                            {
                                Bitmap Imagen = new Bitmap(16, 16, PixelFormat.Format32bppArgb);
                                Graphics Pintar = Graphics.FromImage(Imagen);
                                Pintar.CompositingMode = CompositingMode.SourceCopy;
                                Diccionario_Imágenes.Add(Nombre, Imagen);
                                Diccionario_Pintar.Add(Nombre, Pintar);
                            }
                            Diccionario_Pintar[Nombre].DrawImage(Bloque.Imagen_Textura, new Rectangle(Matriz_Posiciones[Índice], new Size(4, 4)), new Rectangle(Matriz_Posiciones[Índice], new Size(4, 4)), GraphicsUnit.Pixel);
                            break;
                        }
                    }
                }
                foreach (KeyValuePair<string, Graphics> Entrada in Diccionario_Pintar)
                {
                    Entrada.Value.Dispose();
                }
                foreach (KeyValuePair<string, Bitmap> Entrada in Diccionario_Imágenes)
                {
                    Program.Guardar_Imagen_Temporal(Entrada.Value, Entrada.Key);
                    Entrada.Value.Dispose();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Mundos_1_13_a_1_12_2_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                Reiniciar_Diccionario_1_13_a_1_12_2();
                this.Text = Texto_Título + " - [The original world files will never be modified]";
                this.WindowState = FormWindowState.Maximized;
                if (Lista_Biomas != null && Lista_Biomas.Count > 0) // Add the 1.12.2- biomes.
                {
                    for (int Índice = 0; Índice < Lista_Biomas.Count; Índice++)
                    {
                        ComboBox_Biomas.Items.Add(Lista_Biomas[Índice].Value);
                        ComboBox_Bioma_Vacío.Items.Add(Lista_Biomas[Índice].Value);
                    }
                }
                ComboBox_Overworld.SelectedIndex = Variable_Dimensión_Overworld;
                ComboBox_Nether.SelectedIndex = Variable_Dimensión_Nether;
                ComboBox_The_End.SelectedIndex = Variable_Dimensión_The_End;
                ComboBox_Biomas.SelectedIndex = Variable_Biomas;
                ComboBox_Bioma_Vacío.SelectedIndex = Variable_Bioma_Vacío;
                ComboBox_Luz.SelectedIndex = Variable_Luz;
                CheckBox_Mundo_Invertido.CheckState = Variable_Mundo_Invertido_Suelo ? CheckState.Indeterminate : Variable_Mundo_Invertido ? CheckState.Checked : CheckState.Unchecked;
                CheckBox_Auto_Destrucción.Checked = Variable_Auto_Destrucción;
                Ocupado = true;
                Registro_Cargar_Opciones(); // Not working yet, sorry.
                Ocupado = false;
                // Load the existing Minecraft worlds from the default save folder:
                if (Directory.Exists(Program.Ruta_Guardado_Minecraft))
                {
                    Lista_Rutas_Mundos_Minecraft.AddRange(Directory.GetDirectories(Program.Ruta_Guardado_Minecraft, "*", SearchOption.TopDirectoryOnly));
                    if (Lista_Rutas_Mundos_Minecraft != null && Lista_Rutas_Mundos_Minecraft.Count > 0)
                    {
                        if (Lista_Rutas_Mundos_Minecraft.Count > 1) Lista_Rutas_Mundos_Minecraft.Sort();
                        foreach (string Ruta in Lista_Rutas_Mundos_Minecraft)
                        {
                            ComboBox_Ruta_Mundo_1_13.Items.Add(Ruta);
                        }
                        ComboBox_Ruta_Mundo_1_13.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Mundos_1_13_a_1_12_2_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                ComboBox_Ruta_Mundo_1_13.SelectAll(); // For fast editing.
                DataGridView_Transmutación.Sort(Columna_Entrada, ListSortDirection.Ascending);
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Mundos_1_13_a_1_12_2_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Subproceso_Activo)
                {
                    if (MessageBox.Show(this, "Currently there is a world conversion in progress.\r\nDo you want to cancel it, but saving what has been done?", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes && Subproceso_Activo) // Since a message can stay on top for infinite time, double check if it's still converting.
                    {
                        Pendiente_Subproceso_Abortar = true;
                    }
                    e.Cancel = true;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Mundos_1_13_a_1_12_2_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Mundos_1_13_a_1_12_2_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Conversor_Mundos_1_13_a_1_12_2_DragDrop(object sender, DragEventArgs e)
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
                                    ComboBox_Ruta_Mundo_1_13.Text = Ruta;
                                    //ComboBox_Ruta_Mundo_1_13.Text = Directory.Exists(Ruta) ? Ruta : Path.GetDirectoryName(Ruta);
                                    //Minecraft.Información_Niveles Información_Nivel = Minecraft.Información_Niveles.Obtener_Información_Nivel(Ruta);
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

        private void Ventana_Conversor_Mundos_1_13_a_1_12_2_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Normal) this.WindowState = FormWindowState.Maximized;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Mundos_1_13_a_1_12_2_KeyDown(object sender, KeyEventArgs e)
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
                        Botón_Convertir.PerformClick();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Overworld_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Dimensión_Overworld = ComboBox_Overworld.SelectedIndex;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Nether_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Dimensión_Nether = ComboBox_Nether.SelectedIndex;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_The_End_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Dimensión_The_End = ComboBox_The_End.SelectedIndex;
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
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Conversor_Mundos_1_13_1_12_2);
                Program.Ejecutar_Ruta(Program.Ruta_Guardado_Imágenes_Conversor_Mundos_1_13_1_12_2, ProcessWindowStyle.Maximized);
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
                try
                {
                    if (!string.IsNullOrEmpty(Texto_Portapapeles))
                    {
                        Clipboard.SetText(Texto_Portapapeles);
                        Texto_Portapapeles = null;
                    }
                }
                catch (Exception Excepción)
                {
                    Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                    Texto_Portapapeles = null;
                }
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

        private void Botón_Convertir_Click(object sender, EventArgs e)
        {
            try
            {
                //Verificar_Texturas_32_Bits_Alfa(null);
                //return;
                Ruta_Mundo = ComboBox_Ruta_Mundo_1_13.Text;
                if (!string.IsNullOrEmpty(Ruta_Mundo) && Directory.Exists(Ruta_Mundo))
                {
                    Ruta_Regiones_Overworld = Ruta_Mundo + "\\region";
                    Ruta_Regiones_Nether = Ruta_Mundo + "\\DIM-1\\region";
                    Ruta_Regiones_The_End = Ruta_Mundo + "\\DIM1\\region";
                    if (Directory.Exists(Ruta_Regiones_Overworld) || Directory.Exists(Ruta_Regiones_Nether) || Directory.Exists(Ruta_Regiones_The_End))
                    {
                        Lista_Posiciones_Regiones_Overworld = Minecraft.Obtener_Rutas_Regiones(Ruta_Regiones_Overworld, out Rectángulo_Dimensiones_Overworld);
                        Lista_Posiciones_Regiones_Nether = Minecraft.Obtener_Rutas_Regiones(Ruta_Regiones_Nether, out Rectángulo_Dimensiones_Nether);
                        Lista_Posiciones_Regiones_The_End = Minecraft.Obtener_Rutas_Regiones(Ruta_Regiones_The_End, out Rectángulo_Dimensiones_The_End);
                        if ((Lista_Posiciones_Regiones_Overworld != null && Lista_Posiciones_Regiones_Overworld.Count > 0) ||
                            (Lista_Posiciones_Regiones_Nether != null && Lista_Posiciones_Regiones_Nether.Count > 0) ||
                            (Lista_Posiciones_Regiones_The_End != null && Lista_Posiciones_Regiones_The_End.Count > 0))
                        {
                            long Total_Regiones = Lista_Posiciones_Regiones_Overworld.Count + Lista_Posiciones_Regiones_Nether.Count + Lista_Posiciones_Regiones_The_End.Count;
                            if (MessageBox.Show(this, "The selected world is ready to be converted.\r\nThe original world files will never be modified.\r\n\r\n" +
                                "Overworld region files: " + Program.Traducir_Número(Lista_Posiciones_Regiones_Overworld.Count) + " (chunks: " + Program.Traducir_Número((long)Lista_Posiciones_Regiones_Overworld.Count * 1024L) + ", blocks: " + Program.Traducir_Número((long)Lista_Posiciones_Regiones_Overworld.Count * 1024L * 65536L) + ").\r\n" +
                                "Nether region files: " + Program.Traducir_Número(Lista_Posiciones_Regiones_Nether.Count) + " (chunks: " + Program.Traducir_Número((long)Lista_Posiciones_Regiones_Nether.Count * 1024L) + ", blocks: " + Program.Traducir_Número((long)Lista_Posiciones_Regiones_Nether.Count * 1024L * 65536L) + ").\r\n" +
                                "The End region files: " + Program.Traducir_Número(Lista_Posiciones_Regiones_The_End.Count) + " (chunks: " + Program.Traducir_Número((long)Lista_Posiciones_Regiones_The_End.Count * 1024L) + ", blocks: " + Program.Traducir_Número((long)Lista_Posiciones_Regiones_The_End.Count * 1024L * 65536L) + ").\r\n\r\n" +
                                "Total region files: " + Program.Traducir_Número(Total_Regiones) + " (chunks: " + Program.Traducir_Número(Total_Regiones * 1024L) + ", blocks: " + Program.Traducir_Número((long)Total_Regiones * 1024L * 65536L) + ").\r\n" +
                                "\r\nDo you want to start the conversion as a new 1.12.2- world?", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                Grupo_Ajustes.Enabled = false;
                                Tabla_Bloques.Enabled = false;
                                Menú_Contextual.Enabled = false;
                                Subproceso = new Thread(new ThreadStart(Subproceso_DoWork));
                                Subproceso.IsBackground = true;
                                Subproceso.Priority = ThreadPriority.Normal;
                                Subproceso.Start();
                            }
                        }
                        else MessageBox.Show(this, "The selected world doesn't have any region file.\r\nPlease select a different world.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else // Try to load the world as an old InfDev world to convert it to 1.12.2-.
                    {
                        Grupo_Ajustes.Enabled = false;
                        Tabla_Bloques.Enabled = false;
                        Menú_Contextual.Enabled = false;
                        Subproceso = new Thread(new ThreadStart(Subproceso_NBT_DoWork));
                        Subproceso.IsBackground = true;
                        Subproceso.Priority = ThreadPriority.Normal;
                        Subproceso.Start();
                    }
                    //else MessageBox.Show(this, "The select world doesn't have any region folder.\r\nPlease select a different world.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (!string.IsNullOrEmpty(Ruta_Mundo) && File.Exists(Ruta_Mundo)) // Indev world?
                {
                    Grupo_Ajustes.Enabled = false;
                    Tabla_Bloques.Enabled = false;
                    Menú_Contextual.Enabled = false;
                    Subproceso = new Thread(new ThreadStart(Subproceso_Indev_DoWork));
                    Subproceso.IsBackground = true;
                    Subproceso.Priority = ThreadPriority.Normal;
                    Subproceso.Start();
                }
                //else MessageBox.Show(this, "The select world path is not valid.\r\nPlease select a different world.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        //List<string> Lista_Propiedades_Ejemplos = new List<string>(); // For tests.

        /// <summary>
        /// Converts the new 1.13+ block names to other similar blocks from 1.12.2-. Suggestions are welocme to use better old blocks as replacements.
        /// </summary>
        /// <param name="Nombre">Any valid Minecraft block name, starting with "minecraft:".</param>
        /// <returns>The replaced block name or the original if it was present at 1.12.2-.</returns>
        internal string Reemplazar_Bloques_Minecraft_1_13(string Nombre)
        {
            try
            {
                // First replace some block types by another ones.
                if (Diccionario_Transmutación.ContainsKey(Nombre))
                {
                    Nombre = Diccionario_Transmutación[Nombre];
                }

                // Then replace to reduce the block types to allow just the desired ones.
                // Note that here the second dictionary is used, since the first only stores
                // the user desired block palette.
                if (Diccionario_Cuantización_Final.ContainsKey(Nombre))
                {
                    Nombre = Diccionario_Cuantización_Final[Nombre];
                }

                // Finally convert the 1.13+ blocks to 1.12.2-, if they haven't been converted yet.
                if (Diccionario_1_13_a_1_12_2.ContainsKey(Nombre))
                {
                    Nombre = Diccionario_1_13_a_1_12_2[Nombre];
                }

                /*// Tests with block deletion, this could usually never be done before, so enjoy.
                if (CheckBox_Mundo_Acuático.Checked)
                {
                    if (string.Compare(Nombre, "minecraft:air", true) == 0) Nombre = "minecraft:water";
                }
                if (CheckBox_Eliminar_Agua.Checked) // Update a column of water for a chain reaction! [WARNING]
                {
                    if (string.Compare(Nombre, "minecraft:water", true) == 0) Nombre = "minecraft:air"; // Cool test to dry the oceans, etc.
                }
                if (CheckBox_Eliminar_Lava.Checked)
                {
                    if (string.Compare(Nombre, "minecraft:lava", true) == 0) Nombre = "minecraft:air";
                }
                if (CheckBox_Eliminar_Piedras.Checked)
                {
                    if (string.Compare(Nombre, "minecraft:stone", true) == 0) Nombre = "minecraft:air";
                    if (string.Compare(Nombre, "minecraft:cobblestone", true) == 0) Nombre = "minecraft:air"; // Should this be deleted?
                    if (string.Compare(Nombre, "minecraft:mossy_cobblestone", true) == 0) Nombre = "minecraft:air"; // Should this be deleted?
                    if (string.Compare(Nombre, "minecraft:andesite", true) == 0) Nombre = "minecraft:air";
                    if (string.Compare(Nombre, "minecraft:diorite", true) == 0) Nombre = "minecraft:air";
                    if (string.Compare(Nombre, "minecraft:granite", true) == 0) Nombre = "minecraft:air";
                    if (string.Compare(Nombre, "minecraft:sandstone", true) == 0) Nombre = "minecraft:air";
                    if (string.Compare(Nombre, "minecraft:red_sandstone", true) == 0) Nombre = "minecraft:air";
                    if (string.Compare(Nombre, "minecraft:prismarine", true) == 0) Nombre = "minecraft:air"; // Should this be deleted?
                    //if (string.Compare(Nombre, "", true) == 0) Nombre = "minecraft:air"; // Add more blocks here?
                }
                if (CheckBox_Eliminar_Tierra.Checked)
                {
                    if (string.Compare(Nombre, "minecraft:dirt", true) == 0) Nombre = "minecraft:air";
                }
                if (CheckBox_Eliminar_Netherrack.Checked)
                {
                    if (string.Compare(Nombre, "minecraft:netherrack", true) == 0) Nombre = "minecraft:air";
                }
                if (CheckBox_Eliminar_End_Stone.Checked)
                {
                    if (string.Compare(Nombre, "minecraft:end_stone", true) == 0) Nombre = "minecraft:air";
                }
                if (CheckBox_Eliminar_Lecho_Roca.Checked)
                {
                    if (string.Compare(Nombre, "minecraft:bedrock", true) == 0) Nombre = "minecraft:air";
                }
                if (CheckBox_Eliminar_Minerales.Checked)
                {
                    if (string.Compare(Nombre, "minecraft:coal_ore", true) == 0) Nombre = "minecraft:air";
                    if (string.Compare(Nombre, "minecraft:diamond_ore", true) == 0) Nombre = "minecraft:air";
                    if (string.Compare(Nombre, "minecraft:emerald_ore", true) == 0) Nombre = "minecraft:air";
                    if (string.Compare(Nombre, "minecraft:gold_ore", true) == 0) Nombre = "minecraft:air";
                    if (string.Compare(Nombre, "minecraft:iron_ore", true) == 0) Nombre = "minecraft:air";
                    if (string.Compare(Nombre, "minecraft:lapis_ore", true) == 0) Nombre = "minecraft:air";
                    if (string.Compare(Nombre, "minecraft:nether_quartz_ore", true) == 0) Nombre = "minecraft:air";
                    if (string.Compare(Nombre, "minecraft:redstone_ore", true) == 0) Nombre = "minecraft:air";
                }
                if (CheckBox_Intercambiar_Agua_Lava.Checked)
                {
                    if (string.Compare(Nombre, "minecraft:water", true) == 0) Nombre = "minecraft:lava";
                    if (string.Compare(Nombre, "minecraft:flowing_water", true) == 0) Nombre = "minecraft:flowing_lava";
                }
                if (CheckBox_Intercambiar_Lava_Agua.Checked) // Try this on the Nether and swim in water!
                {
                    if (string.Compare(Nombre, "minecraft:lava", true) == 0) Nombre = "minecraft:water";
                    if (string.Compare(Nombre, "minecraft:flowing_lava", true) == 0) Nombre = "minecraft:flowing_water";
                }*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return Nombre;
        }

        internal SortedDictionary<string, List<string>> Diccionario_Bloques_Desconocidos = new SortedDictionary<string, List<string>>();
        internal SortedDictionary<string, List<string>> Diccionario_Bloques_Obsoletos = new SortedDictionary<string, List<string>>();
        internal SortedDictionary<string, List<string>> Diccionario_Bloques_Únicos = new SortedDictionary<string, List<string>>();
        internal string Texto_Bloques_Desconocidos = null;
        internal string Texto_Bloques_Obsoletos = null;
        internal string Texto_Bloques_Únicos = null;

        /// <summary>
        /// Obtains an ID and Data value for Minecraft 1.12.2- based on a Minecraft 1.13+ block name and it's list of properties (which is optional and basically used to properly rotate the blocks, etc).
        /// </summary>
        /// <param name="Nombre_1_13">A valid Minecraft 1.13+ block name, which should start with "minecraft:".</param>
        /// <param name="Lista_Propiedades">A list of strings that define block properties in NBT format. It can null or empty, which will result in a default Data value.</param>
        /// <param name="Data">A value between 0 and 15 defining the metadata value of a block, like it's rotation.</param>
        /// <returns>Returns the Minecraft 1.12.2- block ID, between 0 and 255, also returns a Data value, between 0 and 15. So combining those 2 values gives a value between 0 and 4.095, or with 12 bits.</returns>
        internal byte Obtener_ID_Data_Minecraft_1_12_2(string Nombre_1_13, List<string> Lista_Propiedades, out string Nombre_1_12_2, out byte Data)
        {
            Nombre_1_12_2 = Nombre_1_13;
            Data = 0; // Default block state (ranges from 0 to 15).
            try
            {
                byte ID = 0; // Defaults to an Air block.

                // First replace the 1.13+ missing blocks on 1.12.2- with relatively similar blocks.
                Nombre_1_12_2 = Reemplazar_Bloques_Minecraft_1_13(Nombre_1_13);

                if (string.Compare(Nombre_1_13, Nombre_1_12_2, true) != 0)
                {
                    string Nombre_Minúsculas = Nombre_1_12_2.ToLowerInvariant();
                    if (!Nombre_Minúsculas.Contains("_slab") && !Nombre_Minúsculas.Contains("_stairs"))
                    {
                        Lista_Propiedades.Clear(); // Avoid setting wrong properties if the block has changed.
                    }
                    Nombre_Minúsculas = null;
                }

                // Now convert the adapted 1.13+ Minecraft name to an internal index of this program.
                short ID_Minecraft_1_13 = Minecraft.Bloques.Diccionario_Nombre_Índice[Nombre_1_12_2];

                // Then search if that index can be converted to 1.12.2- ID and Data values.
                foreach (KeyValuePair<short, short> Entrada in Minecraft.Diccionario_Bloques_Índices_1_12_2_a_Índices_1_13)
                {
                    if (Entrada.Value == ID_Minecraft_1_13)
                    {
                        ID = Minecraft.Obtener_Valores_ID_Data(Entrada.Key, out Data);
                        //if (ID != 43 && ID != 44 && ID != 125 && ID != 126 && ID != 181 && ID != 182) // Ignore all the slabs.
                        {
                            break;
                        }
                    }
                }

                //if (Lista_Propiedades != null && Lista_Propiedades.Count > 0) // Always check it.
                {
                    // Finally adapt the Data value with it's found properties, so it can rotated, etc.
                    ID = Obtener_ID_Data_Bloque_Ajustados(Nombre_1_12_2, Lista_Propiedades, ID, Data, out Data);
                }

                return ID; //Return the ID and Data values generated after being fully adapted.

                // If we are here, something went wrong and Air will replace a full type of block.
                // So if you expected a block and see Air, the conversion might have failed in here.
            }
            catch (Exception Excepción)
            {
                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                Data = 0; // On any error, try the Data 0, which will work on most cases.
            }
            //MessageBox.Show("????");
            return 0; // Return 0 or Air block, if not found, which also needs a Data value of 0.
        }

        /// <summary>
        /// Searches for a property value inside the list of properties, ignoring the letter case.
        /// </summary>
        /// <param name="Propiedad">A text string with the property to search for.</param>
        /// <param name="Lista_Propiedades">A list with the NBT properties of a Minecraft 1.13+ block in the form of text strings.</param>
        /// <returns>Returns true if the list contains the selected property. Returns false toherwise.</returns>
        internal bool Buscar_Propiedad(string Propiedad, List<string> Lista_Propiedades)
        {
            try
            {
                //if (Lista_Propiedades != null && Lista_Propiedades.Count > 0) // Checked before.
                {
                    foreach (string Texto_Propiedad in Lista_Propiedades)
                    {
                        if (string.Compare(Texto_Propiedad, Propiedad, true) == 0)
                        {
                            return true; // Found the wanted property.
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return false; // Wanted property not found.
        }

        /// <summary>
        /// The list with all the possible properties of the blocks in Minecraft 1.13.1, extracted directly from a debug world (see the secrets on the help viewer by pressing F1 to know how to create one), which should've created all the possible values at once. Here the values have been sorted and all include at least one block name that uses every property for a better understanding. This is also very useful to know which properties and what values can be expected, and also for converting between 1.12.2- and 1.13+. But note that maybe not all the blocks that share a common value like "age" might have the same value range between them, so be aware of that.
        /// </summary>
        internal static readonly List<string> Lista_Propiedades_Únicas = new List<string>(new string[]
        {
            "age: 0", // sugar_cane.
            "age: 1", // sugar_cane.
            "age: 2", // fire.
            "age: 3", // sugar_cane.
            "age: 4", // sugar_cane.
            "age: 5", // fire.
            "age: 6", // potatoes.
            "age: 7", // potatoes.
            "age: 8", // fire.
            "age: 9", // sugar_cane.
            "age: 10", // sugar_cane.
            "age: 11", // fire.
            "age: 12", // sugar_cane.
            "age: 13", // fire.
            "age: 14", // fire.
            "age: 15", // cactus.
            "age: 16", // kelp.
            "age: 17", // kelp.
            "age: 18", // kelp.
            "age: 19", // kelp.
            "age: 20", // kelp.
            "age: 21", // kelp.
            "age: 22", // kelp.
            "age: 23", // kelp.
            "age: 24", // kelp.
            "age: 25", // kelp.
            "attached: false", // tripwire.
            "attached: true", // tripwire_hook.
            "axis: x", // stripped_birch_log.
            "axis: y", // stripped_birch_log.
            "axis: z", // stripped_birch_log.
            "bites: 0", // cake.
            "bites: 1", // cake.
            "bites: 2", // cake.
            "bites: 3", // cake.
            "bites: 4", // cake.
            "bites: 5", // cake.
            "bites: 6", // cake.
            "conditional: false", // chain_command_block.
            "conditional: true", // command_block.
            "delay: 1", // repeater.
            "delay: 2", // repeater.
            "delay: 3", // repeater.
            "delay: 4", // repeater.
            "disarmed: false", // tripwire.
            "disarmed: true", // tripwire.
            "distance: 1", // jungle_leaves.
            "distance: 2", // jungle_leaves.
            "distance: 3", // jungle_leaves.
            "distance: 4", // jungle_leaves.
            "distance: 5", // jungle_leaves.
            "distance: 6", // jungle_leaves.
            "distance: 7", // jungle_leaves.
            "down: false", // red_mushroom_block.
            "down: true", // brown_mushroom_block.
            "drag: false", // bubble_column.
            "drag: true", // bubble_column.
            "east: false", // fire.
            "east: none", // redstone_wire.
            "east: side", // redstone_wire.
            "east: true", // fire.
            "east: up", // redstone_wire.
            "eggs: 1", // turtle_egg.
            "eggs: 2", // turtle_egg.
            "eggs: 3", // turtle_egg.
            "eggs: 4", // turtle_egg.
            "enabled: false", // hopper.
            "enabled: true", // hopper.
            "extended: false", // sticky_piston.
            "extended: true", // sticky_piston.
            "eye: false", // end_portal_frame.
            "eye: true", // end_portal_frame.
            "face: ceiling", // jungle_button.
            "face: floor", // oak_button.
            "face: wall", // oak_button.
            "facing: down", // end_rod.
            "facing: east", // lime_bed.
            "facing: north", // white_bed.
            "facing: south", // blue_bed.
            "facing: up", // end_rod.
            "facing: west", // lime_bed.
            "half: bottom", // oak_stairs.
            "half: lower", // iron_door.
            "half: top", // oak_stairs.
            "half: upper", // oak_door.
            "has_bottle_0: false", // brewing_stand.
            "has_bottle_0: true", // brewing_stand.
            "has_bottle_1: false", // brewing_stand.
            "has_bottle_1: true", // brewing_stand.
            "has_bottle_2: false", // brewing_stand.
            "has_bottle_2: true", // brewing_stand.
            "has_record: false", // jukebox.
            "has_record: true", // jukebox.
            "hatch: 0", // turtle_egg.
            "hatch: 1", // turtle_egg.
            "hatch: 2", // turtle_egg.
            "hinge: left", // oak_door.
            "hinge: right", // oak_door.
            "in_wall: false", // jungle_fence_gate.
            "in_wall: true", // jungle_fence_gate.
            "instrument: basedrum", // note_block.
            "instrument: bass", // note_block.
            "instrument: bell", // note_block.
            "instrument: chime", // note_block.
            "instrument: flute", // note_block.
            "instrument: guitar", // note_block.
            "instrument: harp", // note_block.
            "instrument: hat", // note_block.
            "instrument: snare", // note_block.
            "instrument: xylophone", // note_block.
            "inverted: false", // daylight_detector.
            "inverted: true", // daylight_detector.
            "layers: 1", // snow.
            "layers: 2", // snow.
            "layers: 3", // snow.
            "layers: 4", // snow.
            "layers: 5", // snow.
            "layers: 6", // snow.
            "layers: 7", // snow.
            "layers: 8", // snow.
            "level: 0", // water.
            "level: 1", // water.
            "level: 2", // water.
            "level: 3", // water.
            "level: 4", // water.
            "level: 5", // water.
            "level: 6", // water.
            "level: 7", // water.
            "level: 8", // water.
            "level: 9", // water.
            "level: 10", // water.
            "level: 11", // water.
            "level: 12", // water.
            "level: 13", // water.
            "level: 14", // water.
            "level: 15", // water.
            "lit: false", // furnace.
            "lit: true", // furnace.
            "locked: false", // repeater.
            "locked: true", // repeater.
            "mode: compare", // comparator.
            "mode: corner", // structure_block.
            "mode: data", // structure_block.
            "mode: load", // structure_block.
            "mode: save", // structure_block.
            "mode: subtract", // comparator.
            "moisture: 0", // farmland.
            "moisture: 1", // farmland.
            "moisture: 2", // farmland.
            "moisture: 3", // farmland.
            "moisture: 4", // farmland.
            "moisture: 5", // farmland.
            "moisture: 6", // farmland.
            "moisture: 7", // farmland.
            "north: false", // fire.
            "north: none", // redstone_wire.
            "north: side", // redstone_wire.
            "north: true", // fire.
            "north: up", // redstone_wire.
            "note: 0", // note_block.
            "note: 1", // note_block.
            "note: 2", // note_block.
            "note: 3", // note_block.
            "note: 4", // note_block.
            "note: 5", // note_block.
            "note: 6", // note_block.
            "note: 7", // note_block.
            "note: 8", // note_block.
            "note: 9", // note_block.
            "note: 10", // note_block.
            "note: 11", // note_block.
            "note: 12", // note_block.
            "note: 13", // note_block.
            "note: 14", // note_block.
            "note: 15", // note_block.
            "note: 16", // note_block.
            "note: 17", // note_block.
            "note: 18", // note_block.
            "note: 19", // note_block.
            "note: 20", // note_block.
            "note: 21", // note_block.
            "note: 22", // note_block.
            "note: 23", // note_block.
            "note: 24", // note_block.
            "occupied: false", // blue_bed.
            "occupied: true", // lime_bed.
            "open: false", // oak_door.
            "open: true", // iron_door.
            "part: foot", // lime_bed.
            "part: head", // blue_bed.
            "persistent: false", // jungle_leaves.
            "persistent: true", // jungle_leaves.
            "pickles: 1", // sea_pickle.
            "pickles: 2", // sea_pickle.
            "pickles: 3", // sea_pickle.
            "pickles: 4", // sea_pickle.
            "power: 0", // redstone_wire.
            "power: 1", // redstone_wire.
            "power: 2", // redstone_wire.
            "power: 3", // redstone_wire.
            "power: 4", // redstone_wire.
            "power: 5", // redstone_wire.
            "power: 6", // redstone_wire.
            "power: 7", // redstone_wire.
            "power: 8", // redstone_wire.
            "power: 9", // redstone_wire.
            "power: 10", // redstone_wire.
            "power: 11", // redstone_wire.
            "power: 12", // redstone_wire.
            "power: 13", // redstone_wire.
            "power: 14", // redstone_wire.
            "power: 15", // redstone_wire.
            "powered: false", // note_block.
            "powered: true", // note_block.
            "rotation: 0", // sign.
            "rotation: 1", // zombie_head.
            "rotation: 2", // zombie_head.
            "rotation: 3", // sign.
            "rotation: 4", // sign.
            "rotation: 5", // green_banner.
            "rotation: 6", // green_banner.
            "rotation: 7", // green_banner.
            "rotation: 8", // gray_banner.
            "rotation: 9", // gray_banner.
            "rotation: 10", // gray_banner.
            "rotation: 11", // orange_banner.
            "rotation: 12", // orange_banner.
            "rotation: 13", // orange_banner.
            "rotation: 14", // orange_banner.
            "rotation: 15", // wither_skeleton_skull.
            "shape: ascending_east", // detector_rail.
            "shape: ascending_north", // detector_rail.
            "shape: ascending_south", // detector_rail.
            "shape: ascending_west", // detector_rail.
            "shape: east_west", // detector_rail.
            "shape: inner_left", // oak_stairs.
            "shape: inner_right", // oak_stairs.
            "shape: north_east", // rail.
            "shape: north_south", // activator_rail.
            "shape: north_west", // rail.
            "shape: outer_left", // oak_stairs.
            "shape: outer_right", // oak_stairs.
            "shape: south_east", // rail.
            "shape: south_west", // rail.
            "shape: straight", // oak_stairs.
            "short: false", // piston_head.
            "short: true", // piston_head.
            "signal_fire: false",
            "signal_fire: true",
            "snowy: false", // grass_block.
            "snowy: true", // grass_block.
            "south: false", // fire.
            "south: none", // redstone_wire.
            "south: side", // redstone_wire.
            "south: true", // fire.
            "south: up", // redstone_wire.
            "stage: 0", // oak_sapling.
            "stage: 1", // oak_sapling.
            "triggered: false", // dropper.
            "triggered: true", // dropper.
            "type: bottom", // purpur_slab.
            "type: double", // red_sandstone_slab.
            "type: left", // trapped_chest.
            "type: normal", // piston_head.
            "type: right", // trapped_chest.
            "type: single", // trapped_chest.
            "type: sticky", // piston_head.
            "type: top", // purpur_slab.
            "unstable: false", // tnt.
            "unstable: true", // tnt.
            "up: false", // fire.
            "up: true", // fire.
            "waterlogged: false", // oak_stairs.
            "waterlogged: true", // oak_stairs.
            "west: false", // fire.
            "west: none", // redstone_wire.
            "west: side", // redstone_wire.
            "west: true", // fire.
            "west: up" // redstone_wire.
        });

        /// <summary>
        /// Obtains a custom Data value for 1.12.2- based on the list of properties of a block. The page "Java Edition data values" on the official Minecraft wiki was key to help to program it correctly, so please check it out also if you're interested in this topic.
        /// </summary>
        /// <param name="Nombre">A valid Minecraft 1.13+ block name.</param>
        /// <param name="Lista_Propiedades">A list with the NBT properties of a block. It can be null or empty, which will give a default Data value.</param>
        /// <param name="ID_Original">The Minecraft 1.12.2- block ID based on the block name.</param>
        /// <param name="Data_Original">The Minecraft 1.12.2- block Data based on the block name.</param>
        /// <param name="Data">A value between 0 and 15. It's default is 0.</param>
        /// <returns>Returns an adapted Data value between 0 and 15. If it doesn't need to be adapted it will return the original Data value passed.</returns>
        internal byte Obtener_ID_Data_Bloque_Ajustados(string Nombre, List<string> Lista_Propiedades, byte ID_Original, byte Data_Original, out byte Data)
        {
            Data = Data_Original; // Default of the original Data value passed.
            try
            {
                if (Lista_Propiedades == null) Lista_Propiedades = new List<string>(); // This can be empty, but never null.
                byte ID = ID_Original; // Default of the original ID value passed.

                // Change some IDs based on the properties, since my program ignores
                // for example if a repeater is on or off, because on it's name that
                // value never appears, so now analyze it's properties to find it out
                // and properly convert the blocks and in some cases it's ID values.

                /*if (string.Compare(Nombre, "minecraft:stone_slab", true) == 0 ||
                    string.Compare(Nombre, "minecraft:sandstone_slab", true) == 0 ||
                    string.Compare(Nombre, "minecraft:petrified_oak_slab", true) == 0 ||
                    string.Compare(Nombre, "minecraft:cobblestone_slab", true) == 0 ||
                    string.Compare(Nombre, "minecraft:brick_slab", true) == 0 ||
                    string.Compare(Nombre, "minecraft:stone_brick_slab", true) == 0 ||
                    string.Compare(Nombre, "minecraft:quartz_slab", true) == 0 ||
                    string.Compare(Nombre, "minecraft:nether_brick_slab", true) == 0 ||
                    string.Compare(Nombre, "", true) == 0 ||
                    string.Compare(Nombre, "", true) == 0 ||
                    string.Compare(Nombre, "", true) == 0 ||
                    string.Compare(Nombre, "", true) == 0 ||
                    string.Compare(Nombre, "", true) == 0 ||
                    string.Compare(Nombre, "", true) == 0 ||
                    string.Compare(Nombre, "", true) == 0) // ID: 44.
                {
                    if (!Buscar_Propiedad("type: double", Lista_Propiedades)) // Regular slab.
                    {
                        if (Buscar_Propiedad("type: top", Lista_Propiedades)) Data |= !Variable_Mundo_Invertido ? (byte)8 : (byte)0; // Slab is upside-down, occupying the top half of its voxel.
                        else Data |= !Variable_Mundo_Invertido ? (byte)0 : (byte)8;




                    }
                    else // Double slab.
                    {
                        //if (Buscar_Propiedad("type: top", Lista_Propiedades)) Data |= 8; // Slab is upside-down, occupying the top half of its voxel.
                    }
                }*/
                if (string.Compare(Nombre, "minecraft:stone_slab", true) == 0 ||
                    string.Compare(Nombre, "minecraft:sandstone_slab", true) == 0 ||
                    string.Compare(Nombre, "minecraft:petrified_oak_slab", true) == 0 ||
                    string.Compare(Nombre, "minecraft:cobblestone_slab", true) == 0 ||
                    string.Compare(Nombre, "minecraft:brick_slab", true) == 0 ||
                    string.Compare(Nombre, "minecraft:stone_brick_slab", true) == 0 ||
                    string.Compare(Nombre, "minecraft:quartz_slab", true) == 0 ||
                    string.Compare(Nombre, "minecraft:nether_brick_slab", true) == 0 ||
                    string.Compare(Nombre, "minecraft:oak_slab", true) == 0 ||
                    string.Compare(Nombre, "minecraft:spruce_slab", true) == 0 ||
                    string.Compare(Nombre, "minecraft:birch_slab", true) == 0 ||
                    string.Compare(Nombre, "minecraft:jungle_slab", true) == 0 ||
                    string.Compare(Nombre, "minecraft:acacia_slab", true) == 0 ||
                    string.Compare(Nombre, "minecraft:dark_oak_slab", true) == 0) // ID: 44, 126.
                { // Using also "half: X" because Mojang uses it on it's NBT structure files (it's a bug).
                    if (!Buscar_Propiedad("type: double", Lista_Propiedades) && !Buscar_Propiedad("half: double", Lista_Propiedades)) // Regular slab.
                    {
                        if (Buscar_Propiedad("type: top", Lista_Propiedades) || Buscar_Propiedad("half: top", Lista_Propiedades)) Data |= !Variable_Mundo_Invertido ? (byte)8 : (byte)0; // Slab is upside-down, occupying the top half of its voxel.
                        else Data |= !Variable_Mundo_Invertido ? (byte)0 : (byte)8;
                    }
                    else // Double slab.
                    {
                        if (string.Compare(Nombre, "minecraft:stone_slab", true) == 0)
                        {
                            ID = 1;
                            Data = 0;
                        }
                        else if (string.Compare(Nombre, "minecraft:sandstone_slab", true) == 0)
                        {
                            ID = 24;
                            Data = 0;
                        }
                        else if (string.Compare(Nombre, "minecraft:petrified_oak_slab", true) == 0)
                        {
                            ID = 5;
                            Data = 0;
                        }
                        else if (string.Compare(Nombre, "minecraft:cobblestone_slab", true) == 0)
                        {
                            ID = 4;
                            Data = 0;
                        }
                        else if (string.Compare(Nombre, "minecraft:brick_slab", true) == 0)
                        {
                            ID = 45;
                            Data = 0;
                        }
                        else if (string.Compare(Nombre, "minecraft:stone_brick_slab", true) == 0)
                        {
                            ID = 98;
                            Data = 0;
                        }
                        else if (string.Compare(Nombre, "minecraft:quartz_slab", true) == 0)
                        {
                            ID = 155;
                            Data = 0;
                        }
                        else if (string.Compare(Nombre, "minecraft:nether_brick_slab", true) == 0)
                        {
                            ID = 112;
                            Data = 0;
                        }
                        else if (string.Compare(Nombre, "minecraft:oak_slab", true) == 0)
                        {
                            ID = 5;
                            Data = 0;
                        }
                        else if (string.Compare(Nombre, "minecraft:spruce_slab", true) == 0)
                        {
                            ID = 5;
                            Data = 1;
                        }
                        else if (string.Compare(Nombre, "minecraft:birch_slab", true) == 0)
                        {
                            ID = 5;
                            Data = 2;
                        }
                        else if (string.Compare(Nombre, "minecraft:jungle_slab", true) == 0)
                        {
                            ID = 5;
                            Data = 3;
                        }
                        else if (string.Compare(Nombre, "minecraft:acacia_slab", true) == 0)
                        {
                            ID = 5;
                            Data = 4;
                        }
                        else if (string.Compare(Nombre, "minecraft:dark_oak_slab", true) == 0)
                        {
                            ID = 5;
                            Data = 5;
                        }
                        //Data = 15; // ?
                        //if (Buscar_Propiedad("type: top", Lista_Propiedades)) Data |= 8; // Slab is upside-down, occupying the top half of its voxel.
                    }
                }
                else if (string.Compare(Nombre, "minecraft:water", true) == 0 ||
                    string.Compare(Nombre, "minecraft:flowing_water", true) == 0 ||
                    string.Compare(Nombre, "minecraft:lava", true) == 0 ||
                    string.Compare(Nombre, "minecraft:flowing_lava", true) == 0) // ID: 8 ~ 11.
                {
                    if (Buscar_Propiedad("level: 8", Lista_Propiedades)) Data = 8; // Level 8.
                    else if (Buscar_Propiedad("level: 7", Lista_Propiedades)) Data = 7; // Level 7.
                    else if (Buscar_Propiedad("level: 6", Lista_Propiedades)) Data = 6; // Level 6.
                    else if (Buscar_Propiedad("level: 5", Lista_Propiedades)) Data = 5; // Level 5.
                    else if (Buscar_Propiedad("level: 4", Lista_Propiedades)) Data = 4; // Level 4.
                    else if (Buscar_Propiedad("level: 3", Lista_Propiedades)) Data = 3; // Level 3.
                    else if (Buscar_Propiedad("level: 2", Lista_Propiedades)) Data = 2; // Level 2.
                    else if (Buscar_Propiedad("level: 1", Lista_Propiedades)) Data = 1; // Level 1.
                    else if (Buscar_Propiedad("level: 0", Lista_Propiedades)) Data = 0; // Level 0.
                }
                else if (string.Compare(Nombre, "minecraft:oak_log", true) == 0 ||
                    string.Compare(Nombre, "minecraft:stripped_oak_log", true) == 0 ||
                    string.Compare(Nombre, "minecraft:stripped_oak_wood", true) == 0) // ID: 17, 0.
                {
                    Data = 0; // Oak Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    ID = 17; // change the ID to log.
                }
                else if (string.Compare(Nombre, "minecraft:oak_bark", true) == 0 ||
                    string.Compare(Nombre, "minecraft:oak_wood", true) == 0) // ID: 17, 0.
                {
                    Data = 0; // Oak Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    Data |= 12; // Only bark.

                    ID = 17; // change the ID to log.
                }
                else if (string.Compare(Nombre, "minecraft:spruce_log", true) == 0 ||
                    string.Compare(Nombre, "minecraft:stripped_spruce_log", true) == 0 ||
                    string.Compare(Nombre, "minecraft:stripped_spruce_wood", true) == 0) // ID: 17, 1.
                {
                    Data = 1; // Spruce Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    ID = 17; // change the ID to log.
                }
                else if (string.Compare(Nombre, "minecraft:spruce_bark", true) == 0 ||
                    string.Compare(Nombre, "minecraft:spruce_wood", true) == 0) // ID: 17, 1.
                {
                    Data = 1; // Spruce Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    Data |= 12; // Only bark.

                    ID = 17; // change the ID to log.
                }
                else if (string.Compare(Nombre, "minecraft:birch_log", true) == 0 ||
                    string.Compare(Nombre, "minecraft:stripped_birch_log", true) == 0 ||
                    string.Compare(Nombre, "minecraft:stripped_birch_wood", true) == 0) // ID: 17, 2.
                {
                    Data = 2; // Birch Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    ID = 17; // change the ID to log.
                }
                else if (string.Compare(Nombre, "minecraft:birch_bark", true) == 0 ||
                    string.Compare(Nombre, "minecraft:birch_wood", true) == 0) // ID: 17, 2.
                {
                    Data = 2; // Birch Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    Data |= 12; // Only bark.

                    ID = 17; // change the ID to log.
                }
                else if (string.Compare(Nombre, "minecraft:jungle_log", true) == 0 ||
                    string.Compare(Nombre, "minecraft:stripped_jungle_log", true) == 0 ||
                    string.Compare(Nombre, "minecraft:stripped_jungle_wood", true) == 0) // ID: 17, 3.
                {
                    Data = 3; // Jungle Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    ID = 17; // change the ID to log.
                }
                else if (string.Compare(Nombre, "minecraft:jungle_bark", true) == 0 ||
                    string.Compare(Nombre, "minecraft:jungle_wood", true) == 0) // ID: 17, 3.
                {
                    Data = 3; // Jungle Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    Data |= 12; // Only bark.

                    ID = 17; // change the ID to log.
                }
                else if (string.Compare(Nombre, "minecraft:oak_leaves", true) == 0) // ID: 18, 0.
                {
                    /*if (Buscar_Propiedad("persistent: true", Lista_Propiedades)) */Data = 12; // Oak Leaves (no decay and check decay).
                    //else Data = 0; // Oak Leaves.
                }
                else if (string.Compare(Nombre, "minecraft:spruce_leaves", true) == 0) // ID: 18, 1.
                {
                    /*if (Buscar_Propiedad("persistent: true", Lista_Propiedades)) */Data = 13; // Spruce Leaves (no decay and check decay).
                    //else Data = 1; // Spruce Leaves.
                }
                else if (string.Compare(Nombre, "minecraft:birch_leaves", true) == 0) // ID: 18, 2.
                {
                    /*if (Buscar_Propiedad("persistent: true", Lista_Propiedades)) */Data = 14; // Birch Leaves (no decay and check decay).
                    //else Data = 2; // Birch Leaves.
                }
                else if (string.Compare(Nombre, "minecraft:jungle_leaves", true) == 0) // ID: 18, 3.
                {
                    /*if (Buscar_Propiedad("persistent: true", Lista_Propiedades)) */Data = 15; // Jungle Leaves (no decay and check decay).
                    //else Data = 3; // Jungle Leaves.
                }
                else if (string.Compare(Nombre, "minecraft:dispenser", true) == 0 ||
                    string.Compare(Nombre, "minecraft:dropper", true) == 0) // ID: 23 - 158.
                {
                    if (Buscar_Propiedad("facing: down", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)0 : (byte)1; // Dropper facing down.
                    else if (Buscar_Propiedad("facing: up", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)1 : (byte)0; // Dropper facing up.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Dropper facing north.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // Dropper facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // Dropper facing west.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // Dropper facing east.

                    if (Buscar_Propiedad("triggered: true", Lista_Propiedades)) Data |= 8; // Set if it's activated.
                }
                else if (string.Compare(Nombre, "minecraft:bed", true) == 0 ||
                    string.Compare(Nombre, "minecraft:black_bed", true) == 0 ||
                    string.Compare(Nombre, "minecraft:blue_bed", true) == 0 ||
                    string.Compare(Nombre, "minecraft:brown_bed", true) == 0 ||
                    string.Compare(Nombre, "minecraft:cyan_bed", true) == 0 ||
                    string.Compare(Nombre, "minecraft:gray_bed", true) == 0 ||
                    string.Compare(Nombre, "minecraft:green_bed", true) == 0 ||
                    string.Compare(Nombre, "minecraft:light_blue_bed", true) == 0 ||
                    string.Compare(Nombre, "minecraft:light_gray_bed", true) == 0 ||
                    string.Compare(Nombre, "minecraft:lime_bed", true) == 0 ||
                    string.Compare(Nombre, "minecraft:magenta_bed", true) == 0 ||
                    string.Compare(Nombre, "minecraft:orange_bed", true) == 0 ||
                    string.Compare(Nombre, "minecraft:pink_bed", true) == 0 ||
                    string.Compare(Nombre, "minecraft:purple_bed", true) == 0 ||
                    string.Compare(Nombre, "minecraft:red_bed", true) == 0 ||
                    string.Compare(Nombre, "minecraft:white_bed", true) == 0 ||
                    string.Compare(Nombre, "minecraft:yellow_bed", true) == 0) // ID: 26.
                {
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // Head facing South.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // Head facing West.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Head facing North.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Head facing East.

                    if (Buscar_Propiedad("occupied: true", Lista_Propiedades)) Data |= 4; // The bed is occupied.

                    if (Buscar_Propiedad("part: head", Lista_Propiedades)) Data |= 8; // The head of the bed.

                    ID = 26; // Needs a change of ID.
                }
                else if (string.Compare(Nombre, "minecraft:activator_rail", true) == 0 ||
                    string.Compare(Nombre, "minecraft:detector_rail", true) == 0 ||
                    string.Compare(Nombre, "minecraft:powered_rail", true) == 0) // ID: 27 ~ 28 - 157.
                {
                    if (Buscar_Propiedad("shape: north_south", Lista_Propiedades)) Data = 0; // flat track going north-south.
                    else if (Buscar_Propiedad("shape: east_west", Lista_Propiedades)) Data = 1; // flat track going west-east.
                    else if (Buscar_Propiedad("shape: ascending_east", Lista_Propiedades)) Data = 2; // sloped track ascending to the east.
                    else if (Buscar_Propiedad("shape: ascending_west", Lista_Propiedades)) Data = 3; // sloped track ascending to the west.
                    else if (Buscar_Propiedad("shape: ascending_north", Lista_Propiedades)) Data = 4; // sloped track ascending to the north.
                    else if (Buscar_Propiedad("shape: ascending_south", Lista_Propiedades)) Data = 5; // sloped track ascending to the south.

                    if (Buscar_Propiedad("powered: true", Lista_Propiedades)) Data |= 8; // Set if rail is active.
                }
                else if (string.Compare(Nombre, "minecraft:piston", true) == 0 ||
                    string.Compare(Nombre, "minecraft:sticky_piston", true) == 0) // ID: 29 ~ 33.
                {
                    if (Buscar_Propiedad("facing: down", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)0 : (byte)1; // Down.
                    else if (Buscar_Propiedad("facing: up", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)1 : (byte)0; // Up.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // north.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // west.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // east.

                    if (Buscar_Propiedad("extended: true", Lista_Propiedades)) Data |= 8; // 1 for pushed out.
                }
                else if (string.Compare(Nombre, "minecraft:piston_head", true) == 0) // ID: 34.
                {
                    // What is the "short: true" property in the piston heads?...
                    // Here will be ignored until I find what it is, so please tell me.

                    if (Buscar_Propiedad("facing: down", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)0 : (byte)1; // Down.
                    else if (Buscar_Propiedad("facing: up", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)1 : (byte)0; // Up.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // north.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // west.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // east.

                    if (Buscar_Propiedad("type: sticky", Lista_Propiedades)) Data |= 8; // 1 is sticky.
                }
                else if (string.Compare(Nombre, "minecraft:torch", true) == 0 ||
                    string.Compare(Nombre, "minecraft:wall_torch", true) == 0 || // ID: 50.
                    string.Compare(Nombre, "minecraft:soul_fire_torch", true) == 0 || // MC 1.16+ fix.
                    string.Compare(Nombre, "minecraft:soul_fire_wall_torch", true) == 0)
                {
                    if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 1; // Facing east (attached to a block to its west).
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 2; // Facing west (attached to a block to its east).
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // Facing south (attached to a block to its north).
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 4; // Facing north (attached to a block to its south).
                    else Data = 5; // Facing up (attached to a block beneath it).

                    ID = 50; // Needs a change of ID.
                }
                else if (string.Compare(Nombre, "minecraft:fire", true) == 0) // ID: 51.
                {
                    if (Buscar_Propiedad("age: 0", Lista_Propiedades)) Data = 0; // Age 0.
                    else if (Buscar_Propiedad("age: 1", Lista_Propiedades)) Data = 1; // Age 1.
                    else if (Buscar_Propiedad("age: 2", Lista_Propiedades)) Data = 2; // Age 2.
                    else if (Buscar_Propiedad("age: 3", Lista_Propiedades)) Data = 3; // Age 3.
                    else if (Buscar_Propiedad("age: 4", Lista_Propiedades)) Data = 4; // Age 4.
                    else if (Buscar_Propiedad("age: 5", Lista_Propiedades)) Data = 5; // Age 5.
                    else if (Buscar_Propiedad("age: 6", Lista_Propiedades)) Data = 6; // Age 6.
                    else if (Buscar_Propiedad("age: 7", Lista_Propiedades)) Data = 7; // Age 7.
                    else if (Buscar_Propiedad("age: 8", Lista_Propiedades)) Data = 8; // Age 8.
                    else if (Buscar_Propiedad("age: 9", Lista_Propiedades)) Data = 9; // Age 9.
                    else if (Buscar_Propiedad("age: 10", Lista_Propiedades)) Data = 10; // Age 10.
                    else if (Buscar_Propiedad("age: 11", Lista_Propiedades)) Data = 11; // Age 11.
                    else if (Buscar_Propiedad("age: 12", Lista_Propiedades)) Data = 12; // Age 12.
                    else if (Buscar_Propiedad("age: 13", Lista_Propiedades)) Data = 13; // Age 13.
                    else if (Buscar_Propiedad("age: 14", Lista_Propiedades)) Data = 14; // Age 14.
                    else if (Buscar_Propiedad("age: 15", Lista_Propiedades)) Data = 15; // Age 15.
                    // 0x0 is a placed or spread fire. Once it reaches 0xF the eternal fire-trick will work since there will be no further updates of the block.
                }
                else if (string.Compare(Nombre, "minecraft:acacia_stairs", true) == 0 ||
                    string.Compare(Nombre, "minecraft:birch_stairs", true) == 0 ||
                    string.Compare(Nombre, "minecraft:brick_stairs", true) == 0 ||
                    string.Compare(Nombre, "minecraft:cobblestone_stairs", true) == 0 ||
                    string.Compare(Nombre, "minecraft:dark_oak_stairs", true) == 0 ||
                    string.Compare(Nombre, "minecraft:jungle_stairs", true) == 0 ||
                    string.Compare(Nombre, "minecraft:nether_brick_stairs", true) == 0 ||
                    string.Compare(Nombre, "minecraft:oak_stairs", true) == 0 ||
                    string.Compare(Nombre, "minecraft:purpur_stairs", true) == 0 ||
                    string.Compare(Nombre, "minecraft:quartz_stairs", true) == 0 ||
                    string.Compare(Nombre, "minecraft:red_sandstone_stairs", true) == 0 ||
                    string.Compare(Nombre, "minecraft:sandstone_stairs", true) == 0 ||
                    string.Compare(Nombre, "minecraft:spruce_stairs", true) == 0 ||
                    string.Compare(Nombre, "minecraft:stone_brick_stairs", true) == 0) // ID: 53 ~ 67 ~ 108 ~ 109 ~ 114 ~ 128 ~ 134 ~ 135 ~ 136 ~ 156 ~ 163 ~ 164 ~ 180 ~ 203.
                {
                    if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 0; // East.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // West.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 2; // South.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 3; // North.

                    if (Buscar_Propiedad("half: top", Lista_Propiedades)) Data |= !Variable_Mundo_Invertido ? (byte)4 : (byte)0; // Set if stairs are upside-down.
                    else Data |= !Variable_Mundo_Invertido ? (byte)0 : (byte)4;
                }
                else if (string.Compare(Nombre, "minecraft:chest", true) == 0 ||
                    string.Compare(Nombre, "minecraft:ladder", true) == 0 ||
                    string.Compare(Nombre, "minecraft:trapped_chest", true) == 0) // ID: 54 ~ 65 ~ 146.
                {
                    if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // facing north.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // facing west.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // facing east.

                    if (Data < 2 || Data > 5) Data = 2; // Invalid values default to 2.
                }
                else if (string.Compare(Nombre, "minecraft:redstone_wire", true) == 0 ||
                    string.Compare(Nombre, "minecraft:light_weighted_pressure_plate", true) == 0 ||
                    string.Compare(Nombre, "minecraft:heavy_weighted_pressure_plate", true) == 0) // ID: 55 ~ 147-148.
                {
                    if (Buscar_Propiedad("power: 0", Lista_Propiedades)) Data = 0; // Power 0.
                    else if (Buscar_Propiedad("power: 1", Lista_Propiedades)) Data = 1; // Power 1.
                    else if (Buscar_Propiedad("power: 2", Lista_Propiedades)) Data = 2; // Power 2.
                    else if (Buscar_Propiedad("power: 3", Lista_Propiedades)) Data = 3; // Power 3.
                    else if (Buscar_Propiedad("power: 4", Lista_Propiedades)) Data = 4; // Power 4.
                    else if (Buscar_Propiedad("power: 5", Lista_Propiedades)) Data = 5; // Power 5.
                    else if (Buscar_Propiedad("power: 6", Lista_Propiedades)) Data = 6; // Power 6.
                    else if (Buscar_Propiedad("power: 7", Lista_Propiedades)) Data = 7; // Power 7.
                    else if (Buscar_Propiedad("power: 8", Lista_Propiedades)) Data = 8; // Power 8.
                    else if (Buscar_Propiedad("power: 9", Lista_Propiedades)) Data = 9; // Power 9.
                    else if (Buscar_Propiedad("power: 10", Lista_Propiedades)) Data = 10; // Power 10.
                    else if (Buscar_Propiedad("power: 11", Lista_Propiedades)) Data = 11; // Power 11.
                    else if (Buscar_Propiedad("power: 12", Lista_Propiedades)) Data = 12; // Power 12.
                    else if (Buscar_Propiedad("power: 13", Lista_Propiedades)) Data = 13; // Power 13.
                    else if (Buscar_Propiedad("power: 14", Lista_Propiedades)) Data = 14; // Power 14.
                    else if (Buscar_Propiedad("power: 15", Lista_Propiedades)) Data = 15; // Power 15.
                }
                else if (string.Compare(Nombre, "minecraft:wheat", true) == 0 ||
                    string.Compare(Nombre, "minecraft:carrots", true) == 0 ||
                    string.Compare(Nombre, "minecraft:potatoes", true) == 0) // ID: 59 ~ 141-142.
                {
                    if (Buscar_Propiedad("age: 0", Lista_Propiedades)) Data = 0; // Age 0.
                    else if (Buscar_Propiedad("age: 1", Lista_Propiedades)) Data = 1; // Age 1.
                    else if (Buscar_Propiedad("age: 2", Lista_Propiedades)) Data = 2; // Age 2.
                    else if (Buscar_Propiedad("age: 3", Lista_Propiedades)) Data = 3; // Age 3.
                    else if (Buscar_Propiedad("age: 4", Lista_Propiedades)) Data = 4; // Age 4.
                    else if (Buscar_Propiedad("age: 5", Lista_Propiedades)) Data = 5; // Age 5.
                    else if (Buscar_Propiedad("age: 6", Lista_Propiedades)) Data = 6; // Age 6.
                    else if (Buscar_Propiedad("age: 7", Lista_Propiedades)) Data = 7; // Age 7.
                }
                else if (string.Compare(Nombre, "minecraft:farmland", true) == 0) // ID: 60.
                {
                    if (Buscar_Propiedad("moisture: 0", Lista_Propiedades)) Data = 0; // Moisture 0.
                    else if (Buscar_Propiedad("moisture: 1", Lista_Propiedades)) Data = 1; // Moisture 1.
                    else if (Buscar_Propiedad("moisture: 2", Lista_Propiedades)) Data = 2; // Moisture 2.
                    else if (Buscar_Propiedad("moisture: 3", Lista_Propiedades)) Data = 3; // Moisture 3.
                    else if (Buscar_Propiedad("moisture: 4", Lista_Propiedades)) Data = 4; // Moisture 4.
                    else if (Buscar_Propiedad("moisture: 5", Lista_Propiedades)) Data = 5; // Moisture 5.
                    else if (Buscar_Propiedad("moisture: 6", Lista_Propiedades)) Data = 6; // Moisture 6.
                    else if (Buscar_Propiedad("moisture: 7", Lista_Propiedades)) Data = 7; // Moisture 7.
                }
                else if (string.Compare(Nombre, "minecraft:furnace", true) == 0) // ID: 61-62.
                {
                    if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // facing north.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // facing west.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // facing east.

                    if (Data < 2 || Data > 5) Data = 2; // Invalid values default to 2.

                    if (Buscar_Propiedad("lit: true", Lista_Propiedades)) ID = 62; // Lit needs a change of ID.
                }
                else if (string.Compare(Nombre, "minecraft:sign", true) == 0) // ID: 63.
                {
                    if (Buscar_Propiedad("rotation: 0", Lista_Propiedades)) Data = 0; // south.
                    else if (Buscar_Propiedad("rotation: 1", Lista_Propiedades)) Data = 1; // south-southwest.
                    else if (Buscar_Propiedad("rotation: 2", Lista_Propiedades)) Data = 2; // southwest.
                    else if (Buscar_Propiedad("rotation: 3", Lista_Propiedades)) Data = 3; // west-southwest.
                    else if (Buscar_Propiedad("rotation: 4", Lista_Propiedades)) Data = 4; // west.
                    else if (Buscar_Propiedad("rotation: 5", Lista_Propiedades)) Data = 5; // west-northwest.
                    else if (Buscar_Propiedad("rotation: 6", Lista_Propiedades)) Data = 6; // northwest.
                    else if (Buscar_Propiedad("rotation: 7", Lista_Propiedades)) Data = 7; // north-northwest.
                    else if (Buscar_Propiedad("rotation: 8", Lista_Propiedades)) Data = 8; // north.
                    else if (Buscar_Propiedad("rotation: 9", Lista_Propiedades)) Data = 9; // north-northeast.
                    else if (Buscar_Propiedad("rotation: 10", Lista_Propiedades)) Data = 10; // northeast.
                    else if (Buscar_Propiedad("rotation: 11", Lista_Propiedades)) Data = 11; // east-northeast.
                    else if (Buscar_Propiedad("rotation: 12", Lista_Propiedades)) Data = 12; // east.
                    else if (Buscar_Propiedad("rotation: 13", Lista_Propiedades)) Data = 13; // east-southeast.
                    else if (Buscar_Propiedad("rotation: 14", Lista_Propiedades)) Data = 14; // southeast.
                    else if (Buscar_Propiedad("rotation: 15", Lista_Propiedades)) Data = 15; // south-southeast.
                }
                else if (string.Compare(Nombre, "minecraft:acacia_door", true) == 0 ||
                    string.Compare(Nombre, "minecraft:birch_door", true) == 0 ||
                    string.Compare(Nombre, "minecraft:dark_oak_door", true) == 0 ||
                    string.Compare(Nombre, "minecraft:iron_door", true) == 0 ||
                    string.Compare(Nombre, "minecraft:jungle_door", true) == 0 ||
                    string.Compare(Nombre, "minecraft:oak_door", true) == 0 ||
                    string.Compare(Nombre, "minecraft:spruce_door", true) == 0) // ID: 64 ~ 193-197.
                {
                    if (Buscar_Propiedad("half: upper", Lista_Propiedades))
                    {
                        Data = 8; // This is the top half of a door.

                        if (Buscar_Propiedad("hinge: right", Lista_Propiedades)) Data |= 1; // Hinge is on the left.

                        if (Buscar_Propiedad("powered: true", Lista_Propiedades)) Data |= 2; // Door is Powered.
                    }
                    else // half: lower
                    {
                        if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 0; // Facing east.
                        else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 1; // Facing south.
                        else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 2; // Facing west.
                        else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 3; // Facing north.

                        if (Buscar_Propiedad("open: true", Lista_Propiedades)) Data |= 4; // Open.
                    }
                }
                else if (string.Compare(Nombre, "minecraft:rail", true) == 0) // ID: 66.
                {
                    if (Buscar_Propiedad("shape: north_south", Lista_Propiedades)) Data = 0; // Straight rail connecting to the north and south.
                    else if (Buscar_Propiedad("shape: east_west", Lista_Propiedades)) Data = 1; // Straight rail connecting to the east and west.
                    else if (Buscar_Propiedad("shape: ascending_east", Lista_Propiedades)) Data = 2; // Sloped rail ascending to the east.
                    else if (Buscar_Propiedad("shape: ascending_west", Lista_Propiedades)) Data = 3; // Sloped rail ascending to the west.
                    else if (Buscar_Propiedad("shape: ascending_north", Lista_Propiedades)) Data = 4; // Sloped rail ascending to the north.
                    else if (Buscar_Propiedad("shape: ascending_south", Lista_Propiedades)) Data = 5; // Sloped rail ascending to the south.
                    else if (Buscar_Propiedad("shape: south_east", Lista_Propiedades)) Data = 6; // Curved rail connecting to the south and east.
                    else if (Buscar_Propiedad("shape: south_west", Lista_Propiedades)) Data = 7; // Curved rail connecting to the south and west.
                    else if (Buscar_Propiedad("shape: north_west", Lista_Propiedades)) Data = 8; // Curved rail connecting to the north and west.
                    else if (Buscar_Propiedad("shape: north_east", Lista_Propiedades)) Data = 9; // Curved rail connecting to the north and east.
                }
                else if (string.Compare(Nombre, "minecraft:prismarine_stairs", true) == 0) // ID: 67.
                {
                    if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 0; // East.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // West.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 2; // South.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 3; // North.

                    if (Buscar_Propiedad("half: top", Lista_Propiedades)) Data |= !Variable_Mundo_Invertido ? (byte)4 : (byte)0; // Set if stairs are upside-down.
                    else Data |= !Variable_Mundo_Invertido ? (byte)0 : (byte)4;

                    ID = 67; // change the ID to cobblestone stairs.
                }
                else if (string.Compare(Nombre, "minecraft:wall_sign", true) == 0) // ID: 68.
                {
                    if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // north.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // west.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // east.
                }
                else if (string.Compare(Nombre, "minecraft:lever", true) == 0) // ID: 69.
                {
                    if (Buscar_Propiedad("face: wall", Lista_Propiedades)) // Lever on block side.
                    {
                        if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 1; // Lever on block side facing east.
                        else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 2; // Lever on block side facing west.
                        else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // Lever on block side facing south.
                        else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 4; // Lever on block side facing north.
                    }
                    else if (Buscar_Propiedad("face: floor", Lista_Propiedades)) // Lever on block top.
                    {
                        if (Buscar_Propiedad("facing: south", Lista_Propiedades) || Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)5 : (byte)0; // Lever on block top points south when off.
                        else if (Buscar_Propiedad("facing: east", Lista_Propiedades) || Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)6 : (byte)7; // Lever on block top points east when off.
                    }
                    else if (Buscar_Propiedad("face: ceiling", Lista_Propiedades)) // Lever on block bottom.
                    {
                        if (Buscar_Propiedad("facing: east", Lista_Propiedades) || Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)0 : (byte)5; // Lever on block bottom points east when off.
                        else if (Buscar_Propiedad("facing: south", Lista_Propiedades) || Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)7 : (byte)6; // Lever on block bottom points south when off.
                    }

                    if (Buscar_Propiedad("powered: true", Lista_Propiedades)) Data |= 8; // Set if activated/disabled.
                }
                else if (string.Compare(Nombre, "minecraft:acacia_pressure_plate", true) == 0 ||
                    string.Compare(Nombre, "minecraft:birch_pressure_plate", true) == 0 ||
                    string.Compare(Nombre, "minecraft:dark_oak_pressure_plate", true) == 0 ||
                    string.Compare(Nombre, "minecraft:jungle_pressure_plate", true) == 0 ||
                    string.Compare(Nombre, "minecraft:oak_pressure_plate", true) == 0 ||
                    string.Compare(Nombre, "minecraft:spruce_pressure_plate", true) == 0 ||
                    string.Compare(Nombre, "minecraft:stone_pressure_plate", true) == 0) // ID: 70 ~ 72.
                {
                    if (Buscar_Propiedad("powered: true", Lista_Propiedades)) Data = 1; // If this bit is set, the pressure plate is active.
                }
                else if (string.Compare(Nombre, "minecraft:redstone_ore", true) == 0) // ID: 73 ~ 74.
                {
                    if (Buscar_Propiedad("lit: true", Lista_Propiedades)) ID = 74; // Lit.
                }
                else if (string.Compare(Nombre, "minecraft:redstone_torch", true) == 0 ||
                    string.Compare(Nombre, "minecraft:redstone_wall_torch", true) == 0) // ID: 75 ~ 76.
                {
                    if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 1; // Facing east (attached to a block to its west).
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 2; // Facing west (attached to a block to its east).
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // Facing south (attached to a block to its north).
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 4; // Facing north (attached to a block to its south).
                    else Data = 5; // Facing up (attached to a block beneath it).

                    if (Buscar_Propiedad("lit: true", Lista_Propiedades)) ID = 76; // Lit.
                    else ID = 75; // Unlit. Needs a change of ID.

                    // Bug fix thanks to iaraUM. I forgot to add the ID 75 when the block was redstone_wall_torch
                    // and since the dictionaries used only use any ID one time, then the redstone_torch had the
                    // correct ID 75 but the other didn't had any ID assigned to it, which will convert the block
                    // as Air, so it was always getting lost during the conversion. That made me think that
                    // possibly other blocks that can attach to a wall might be failing on it's conversions.
                }
                else if (string.Compare(Nombre, "minecraft:acacia_button", true) == 0 ||
                    string.Compare(Nombre, "minecraft:birch_button", true) == 0 ||
                    string.Compare(Nombre, "minecraft:dark_oak_button", true) == 0 ||
                    string.Compare(Nombre, "minecraft:jungle_button", true) == 0 ||
                    string.Compare(Nombre, "minecraft:oak_button", true) == 0 ||
                    string.Compare(Nombre, "minecraft:spruce_button", true) == 0 ||
                    string.Compare(Nombre, "minecraft:stone_button", true) == 0) // ID: 77 ~ 143.
                {
                    if (Buscar_Propiedad("face: ceiling", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)0 : (byte)5; // Button on block bottom facing down.
                    else if (Buscar_Propiedad("face: wall", Lista_Propiedades)) // Button on block side.
                    {
                        if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 1; // Button on block side facing east.
                        else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 2; // Button on block side facing west.
                        else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // Button on block side facing south.
                        else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 4; // Button on block side facing north.
                    }
                    else if (Buscar_Propiedad("face: floor", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)5 : (byte)0; // Button on block top facing up.

                    if (Buscar_Propiedad("powered: true", Lista_Propiedades)) Data |= 8; // If this bit is set, the button is currently active.

                    ID = 143; // Change the ID to oak button.
                }
                else if (string.Compare(Nombre, "minecraft:snow", true) == 0) // ID: 78.
                {
                    if (Buscar_Propiedad("layers: 1", Lista_Propiedades)) Data = 0; // One layer, 2 pixels thick.
                    else if (Buscar_Propiedad("layers: 2", Lista_Propiedades)) Data = 1; // Two layers, 4 pixels thick.
                    else if (Buscar_Propiedad("layers: 3", Lista_Propiedades)) Data = 2; // Three layers, 6 pixels thick.
                    else if (Buscar_Propiedad("layers: 4", Lista_Propiedades)) Data = 3; // Four layers, 8 pixels thick.
                    else if (Buscar_Propiedad("layers: 5", Lista_Propiedades)) Data = 4; // Five layers, 10 pixels thick.
                    else if (Buscar_Propiedad("layers: 6", Lista_Propiedades)) Data = 5; // Six layers, 12 pixels thick.
                    else if (Buscar_Propiedad("layers: 7", Lista_Propiedades)) Data = 6; // Seven layers, 14 pixels thick.
                    else if (Buscar_Propiedad("layers: 8", Lista_Propiedades)) Data = 7; // Eight layers, 16 pixels thick.
                }
                else if (string.Compare(Nombre, "minecraft:cactus", true) == 0 ||
                    string.Compare(Nombre, "minecraft:sugar_cane", true) == 0) // ID: 81 ~ 83.
                {
                    if (Buscar_Propiedad("age: 0", Lista_Propiedades)) Data = 0; // Age 0.
                    else if (Buscar_Propiedad("age: 1", Lista_Propiedades)) Data = 1; // Age 1.
                    else if (Buscar_Propiedad("age: 2", Lista_Propiedades)) Data = 2; // Age 2.
                    else if (Buscar_Propiedad("age: 3", Lista_Propiedades)) Data = 3; // Age 3.
                    else if (Buscar_Propiedad("age: 4", Lista_Propiedades)) Data = 4; // Age 4.
                    else if (Buscar_Propiedad("age: 5", Lista_Propiedades)) Data = 5; // Age 5.
                    else if (Buscar_Propiedad("age: 6", Lista_Propiedades)) Data = 6; // Age 6.
                    else if (Buscar_Propiedad("age: 7", Lista_Propiedades)) Data = 7; // Age 7.
                    else if (Buscar_Propiedad("age: 8", Lista_Propiedades)) Data = 8; // Age 8.
                    else if (Buscar_Propiedad("age: 9", Lista_Propiedades)) Data = 9; // Age 9.
                    else if (Buscar_Propiedad("age: 10", Lista_Propiedades)) Data = 10; // Age 10.
                    else if (Buscar_Propiedad("age: 11", Lista_Propiedades)) Data = 11; // Age 11.
                    else if (Buscar_Propiedad("age: 12", Lista_Propiedades)) Data = 12; // Age 12.
                    else if (Buscar_Propiedad("age: 13", Lista_Propiedades)) Data = 13; // Age 13.
                    else if (Buscar_Propiedad("age: 14", Lista_Propiedades)) Data = 14; // Age 14.
                    else if (Buscar_Propiedad("age: 15", Lista_Propiedades)) Data = 15; // Age 15.
                }
                else if (string.Compare(Nombre, "minecraft:jukebox", true) == 0) // ID: 84.
                {
                    if (Buscar_Propiedad("has_record: true", Lista_Propiedades)) Data = 1; // Contains a disc.
                    // The associated block entity is used to identify which record has been inserted.
                }
                else if (string.Compare(Nombre, "minecraft:carved_pumpkin", true) == 0 ||
                    string.Compare(Nombre, "minecraft:jack_o_lantern", true) == 0) // ID: 86 ~ 91.
                {
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // Pumpkin facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // Pumpkin facing west.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Pumpkin facing north.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Pumpkin facing east.

                    //Data = 4; // Jack o'lantern without face. Will this work?
                }
                else if (string.Compare(Nombre, "minecraft:pumpkin", true) == 0) // ID: 86.
                {
                    ID = 86; // Convert the ID to a pumpkin with face.

                    Data = (byte)Program.Rand.Next(0, 4); // Randomize the facing direction  if the pumpkin.

                    //Data = 4; // Pumpkin without face according to the wiki, but on my tests it dissapeared.
                }
                else if (string.Compare(Nombre, "minecraft:cake", true) == 0) // ID: 92.
                {
                    if (Buscar_Propiedad("bites: 0", Lista_Propiedades)) Data = 0; // 0 pieces eaten.
                    else if (Buscar_Propiedad("bites: 1", Lista_Propiedades)) Data = 1; // 1 piece eaten.
                    else if (Buscar_Propiedad("bites: 2", Lista_Propiedades)) Data = 2; // 2 pieces eaten.
                    else if (Buscar_Propiedad("bites: 3", Lista_Propiedades)) Data = 3; // 3 pieces eaten.
                    else if (Buscar_Propiedad("bites: 4", Lista_Propiedades)) Data = 4; // 4 pieces eaten.
                    else if (Buscar_Propiedad("bites: 5", Lista_Propiedades)) Data = 5; // 5 pieces eaten.
                    else if (Buscar_Propiedad("bites: 6", Lista_Propiedades)) Data = 6; // 6 pieces eaten.
                }
                else if (string.Compare(Nombre, "minecraft:repeater", true) == 0) // ID: 93 ~ 94.
                {
                    // Note that the arrow or triangle points in the opposite direction, which is confusing.
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // Facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // Facing west.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Facing north.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Facing east.

                    if (Buscar_Propiedad("delay: 1", Lista_Propiedades)) Data |= 0; // Delay of 1 redstone tick.
                    else if (Buscar_Propiedad("delay: 2", Lista_Propiedades)) Data |= 4; // Delay of 2 redstone ticks.
                    else if (Buscar_Propiedad("delay: 3", Lista_Propiedades)) Data |= 8; // Delay of 3 redstone ticks.
                    else if (Buscar_Propiedad("delay: 4", Lista_Propiedades)) Data |= 12; // Delay of 4 redstone ticks.

                    if (Buscar_Propiedad("powered: true", Lista_Propiedades)) ID = 94; // Powered needs a change of ID.
                }
                else if (string.Compare(Nombre, "minecraft:acacia_trapdoor", true) == 0 ||
                    string.Compare(Nombre, "minecraft:birch_trapdoor", true) == 0 ||
                    string.Compare(Nombre, "minecraft:dark_oak_trapdoor", true) == 0 ||
                    string.Compare(Nombre, "minecraft:jungle_trapdoor", true) == 0 ||
                    string.Compare(Nombre, "minecraft:oak_trapdoor", true) == 0 ||
                    string.Compare(Nombre, "minecraft:spruce_trapdoor", true) == 0) // ID: 96.
                {
                    // The directions are inverted, which is confusing.
                    if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 0; // Trapdoor on the south side of a block.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 1; // Trapdoor on the north side of a block.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 2; // Trapdoor on the east side of a block.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Trapdoor on the west side of a block.

                    if (Buscar_Propiedad("open: true", Lista_Propiedades)) Data |= 4; // If this bit is set, the trapdoor is open.

                    if (Buscar_Propiedad("half: top", Lista_Propiedades)) Data |= !Variable_Mundo_Invertido ? (byte)8 : (byte)0; // If this bit is set, the trapdoor is on the top half of a block. Otherwise, it is on the bottom half.
                    else Data |= !Variable_Mundo_Invertido ? (byte)0 : (byte)8;

                    ID = 96; // Change the ID to oak trapdoor.
                }
                else if (string.Compare(Nombre, "minecraft:iron_trapdoor", true) == 0) // ID: 167.
                {
                    // The directions are inverted, which is confusing.
                    if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 0; // Trapdoor on the south side of a block.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 1; // Trapdoor on the north side of a block.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 2; // Trapdoor on the east side of a block.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Trapdoor on the west side of a block.

                    if (Buscar_Propiedad("open: true", Lista_Propiedades)) Data |= 4; // If this bit is set, the trapdoor is open.

                    if (Buscar_Propiedad("half: top", Lista_Propiedades)) Data |= !Variable_Mundo_Invertido ? (byte)8 : (byte)0; // If this bit is set, the trapdoor is on the top half of a block. Otherwise, it is on the bottom half.
                    else Data |= !Variable_Mundo_Invertido ? (byte)0 : (byte)8;
                }
                else if (string.Compare(Nombre, "minecraft:mushroom_stem", true) == 0) // ID: 99.
                {
                    // Ignore the top part or the conversion will fail sometimes.
                    if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: false", Lista_Propiedades) &&
                        Buscar_Propiedad("north: false", Lista_Propiedades) &&
                        Buscar_Propiedad("south: false", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: false", Lista_Propiedades) &&
                        Buscar_Propiedad("west: false", Lista_Propiedades)) Data = 0; // Pores on all sides.
                    else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: true", Lista_Propiedades) &&
                        Buscar_Propiedad("north: true", Lista_Propiedades) &&
                        Buscar_Propiedad("south: true", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: false", Lista_Propiedades) &&
                        Buscar_Propiedad("west: true", Lista_Propiedades)) Data = 10; // Stem texture on all four sides, pores on top and bottom.
                    else if (Buscar_Propiedad("down: true", Lista_Propiedades) &&
                        Buscar_Propiedad("east: true", Lista_Propiedades) &&
                        Buscar_Propiedad("north: true", Lista_Propiedades) &&
                        Buscar_Propiedad("south: true", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: true", Lista_Propiedades) &&
                        Buscar_Propiedad("west: true", Lista_Propiedades)) Data = 15; // Stem texture on all six sides.
                    else Data = 0;

                    ID = 99; // Change the block ID to "minecraft:brown_mushroom_block".
                }
                else if (string.Compare(Nombre, "minecraft:brown_mushroom_block", true) == 0 ||
                    string.Compare(Nombre, "minecraft:red_mushroom_block", true) == 0) // ID: 99 ~ 100.
                {
                    // Ignore the top part or the conversion will fail sometimes.
                    if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: false", Lista_Propiedades) &&
                        Buscar_Propiedad("north: false", Lista_Propiedades) &&
                        Buscar_Propiedad("south: false", Lista_Propiedades) &&
                        Buscar_Propiedad("up: false", Lista_Propiedades) &&
                        Buscar_Propiedad("west: false", Lista_Propiedades)) Data = 0; // Pores on all sides.
                    else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: false", Lista_Propiedades) &&
                        Buscar_Propiedad("north: true", Lista_Propiedades) &&
                        Buscar_Propiedad("south: false", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: true", Lista_Propiedades) &&
                        Buscar_Propiedad("west: true", Lista_Propiedades)) Data = 1; // Cap texture on top, west and north.
                    else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: false", Lista_Propiedades) &&
                        Buscar_Propiedad("north: true", Lista_Propiedades) &&
                        Buscar_Propiedad("south: false", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: true", Lista_Propiedades) &&
                        Buscar_Propiedad("west: false", Lista_Propiedades)) Data = 2; // Cap texture on top and north.
                    else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: true", Lista_Propiedades) &&
                        Buscar_Propiedad("north: true", Lista_Propiedades) &&
                        Buscar_Propiedad("south: false", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: true", Lista_Propiedades) &&
                        Buscar_Propiedad("west: false", Lista_Propiedades)) Data = 3; // Cap texture on top, north and east.
                    else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: false", Lista_Propiedades) &&
                        Buscar_Propiedad("north: false", Lista_Propiedades) &&
                        Buscar_Propiedad("south: false", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: true", Lista_Propiedades) &&
                        Buscar_Propiedad("west: true", Lista_Propiedades)) Data = 4; // Cap texture on top and west.
                    else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: false", Lista_Propiedades) &&
                        Buscar_Propiedad("north: false", Lista_Propiedades) &&
                        Buscar_Propiedad("south: false", Lista_Propiedades) &&
                        Buscar_Propiedad("up: true", Lista_Propiedades) && // Don't ignore it here.
                        Buscar_Propiedad("west: false", Lista_Propiedades)) Data = 5; // Cap texture on top.
                    else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: true", Lista_Propiedades) &&
                        Buscar_Propiedad("north: false", Lista_Propiedades) &&
                        Buscar_Propiedad("south: false", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: true", Lista_Propiedades) &&
                        Buscar_Propiedad("west: false", Lista_Propiedades)) Data = 6; // Cap texture on top and east.
                    else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: false", Lista_Propiedades) &&
                        Buscar_Propiedad("north: false", Lista_Propiedades) &&
                        Buscar_Propiedad("south: true", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: true", Lista_Propiedades) &&
                        Buscar_Propiedad("west: true", Lista_Propiedades)) Data = 7; // Cap texture on top, south and west.
                    else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: false", Lista_Propiedades) &&
                        Buscar_Propiedad("north: false", Lista_Propiedades) &&
                        Buscar_Propiedad("south: true", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: true", Lista_Propiedades) &&
                        Buscar_Propiedad("west: false", Lista_Propiedades)) Data = 8; // Cap texture on top and south.
                    else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: true", Lista_Propiedades) &&
                        Buscar_Propiedad("north: false", Lista_Propiedades) &&
                        Buscar_Propiedad("south: true", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: true", Lista_Propiedades) &&
                        Buscar_Propiedad("west: false", Lista_Propiedades)) Data = 9; // Cap texture on top, east and south.
                                                                                      //else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                                                                                      //Buscar_Propiedad("east: false", Lista_Propiedades) &&
                                                                                      //Buscar_Propiedad("north: false", Lista_Propiedades) &&
                                                                                      //Buscar_Propiedad("south: false", Lista_Propiedades) &&
                                                                                      //Buscar_Propiedad("up: false", Lista_Propiedades) &&
                                                                                      //Buscar_Propiedad("west: false", Lista_Propiedades)) Data = 10; // Stem texture on all four sides, pores on top and bottom.
                    else if (Buscar_Propiedad("down: true", Lista_Propiedades) &&
                        Buscar_Propiedad("east: true", Lista_Propiedades) &&
                        Buscar_Propiedad("north: true", Lista_Propiedades) &&
                        Buscar_Propiedad("south: true", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: true", Lista_Propiedades) &&
                        Buscar_Propiedad("west: true", Lista_Propiedades)) Data = 14; // Cap texture on all six sides.
                                                                                      //else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                                                                                      //Buscar_Propiedad("east: false", Lista_Propiedades) &&
                                                                                      //Buscar_Propiedad("north: false", Lista_Propiedades) &&
                                                                                      //Buscar_Propiedad("south: false", Lista_Propiedades) &&
                                                                                      //Buscar_Propiedad("up: false", Lista_Propiedades) &&
                                                                                      //Buscar_Propiedad("west: false", Lista_Propiedades)) Data = 15; // Stem texture on all six sides.
                    else Data = 0;
                }
                else if (string.Compare(Nombre, "minecraft:pumpkin_stem", true) == 0) // ID: 104.
                {
                    if (Buscar_Propiedad("age: 0", Lista_Propiedades)) Data = 0; // Freshly planted stem.
                    else if (Buscar_Propiedad("age: 1", Lista_Propiedades)) Data = 1; // First stage of growth.
                    else if (Buscar_Propiedad("age: 2", Lista_Propiedades)) Data = 2; // Second stage of growth.
                    else if (Buscar_Propiedad("age: 3", Lista_Propiedades)) Data = 3; // Third stage of growth.
                    else if (Buscar_Propiedad("age: 4", Lista_Propiedades)) Data = 4; // Fourth stage of growth.
                    else if (Buscar_Propiedad("age: 5", Lista_Propiedades)) Data = 5; // Fifth stage of growth.
                    else if (Buscar_Propiedad("age: 6", Lista_Propiedades)) Data = 6; // Sixth stage of growth.
                    else if (Buscar_Propiedad("age: 7", Lista_Propiedades)) Data = 7; // Seventh stage of growth.
                }
                else if (string.Compare(Nombre, "minecraft:attached_pumpkin_stem", true) == 0) // ID: 104.
                {
                    Data = 7; // Seventh stage of growth.

                    ID = 104; // Change the ID to pumpkin stem.
                }
                else if (string.Compare(Nombre, "minecraft:melon_stem", true) == 0) // ID: 105.
                {
                    if (Buscar_Propiedad("age: 0", Lista_Propiedades)) Data = 0; // Freshly planted stem.
                    else if (Buscar_Propiedad("age: 1", Lista_Propiedades)) Data = 1; // First stage of growth.
                    else if (Buscar_Propiedad("age: 2", Lista_Propiedades)) Data = 2; // Second stage of growth.
                    else if (Buscar_Propiedad("age: 3", Lista_Propiedades)) Data = 3; // Third stage of growth.
                    else if (Buscar_Propiedad("age: 4", Lista_Propiedades)) Data = 4; // Fourth stage of growth.
                    else if (Buscar_Propiedad("age: 5", Lista_Propiedades)) Data = 5; // Fifth stage of growth.
                    else if (Buscar_Propiedad("age: 6", Lista_Propiedades)) Data = 6; // Sixth stage of growth.
                    else if (Buscar_Propiedad("age: 7", Lista_Propiedades)) Data = 7; // Seventh stage of growth.
                }
                else if (string.Compare(Nombre, "minecraft:attached_melon_stem", true) == 0) // ID: 105.
                {
                    Data = 7; // Seventh stage of growth.

                    ID = 105; // Change the ID to melon stem.
                }
                else if (string.Compare(Nombre, "minecraft:vine", true) == 0) // ID: 106.
                {
                    if (Buscar_Propiedad("south: true", Lista_Propiedades)) Data |= 1; // south.
                    if (Buscar_Propiedad("west: true", Lista_Propiedades)) Data |= 2; // west.
                    if (Buscar_Propiedad("north: true", Lista_Propiedades)) Data |= 4; // north.
                    if (Buscar_Propiedad("east: true", Lista_Propiedades)) Data |= 8; // east.
                }
                else if (string.Compare(Nombre, "minecraft:acacia_fence_gate", true) == 0 ||
                    string.Compare(Nombre, "minecraft:birch_fence_gate", true) == 0 ||
                    string.Compare(Nombre, "minecraft:dark_oak_fence_gate", true) == 0 ||
                    string.Compare(Nombre, "minecraft:jungle_fence_gate", true) == 0 ||
                    string.Compare(Nombre, "minecraft:oak_fence_gate", true) == 0 ||
                    string.Compare(Nombre, "minecraft:spruce_fence_gate", true) == 0) // ID: 107.
                {
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // Facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // Facing west.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Facing north.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Facing east.

                    if (Buscar_Propiedad("open: true", Lista_Propiedades)) Data |= 4; // 0 if the gate is closed, 1 if open.
                }
                else if (string.Compare(Nombre, "minecraft:prismarine_brick_stairs", true) == 0 ||
                    string.Compare(Nombre, "minecraft:prismarine_bricks_stairs", true) == 0) // ID: 109.
                {
                    if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 0; // East.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // West.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 2; // South.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 3; // North.

                    if (Buscar_Propiedad("half: top", Lista_Propiedades)) Data |= !Variable_Mundo_Invertido ? (byte)4 : (byte)0; // Set if stairs are upside-down.
                    else Data |= !Variable_Mundo_Invertido ? (byte)0 : (byte)4;

                    ID = 109; // Change the ID to stone brick stairs.
                }
                else if (string.Compare(Nombre, "minecraft:dark_prismarine_stairs", true) == 0) // ID: 114.
                {
                    if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 0; // East.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // West.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 2; // South.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 3; // North.

                    if (Buscar_Propiedad("half: top", Lista_Propiedades)) Data |= !Variable_Mundo_Invertido ? (byte)4 : (byte)0; // Set if stairs are upside-down.
                    else Data |= !Variable_Mundo_Invertido ? (byte)0 : (byte)4;

                    ID = 114; // Change the ID to nether brick stairs.
                }
                else if (string.Compare(Nombre, "minecraft:nether_wart", true) == 0) // ID: 115.
                {
                    if (Buscar_Propiedad("age: 0", Lista_Propiedades)) Data = 0; // Age 0.
                    else if (Buscar_Propiedad("age: 1", Lista_Propiedades)) Data = 1; // Age 1.
                    else if (Buscar_Propiedad("age: 2", Lista_Propiedades)) Data = 2; // Age 2.
                    else if (Buscar_Propiedad("age: 3", Lista_Propiedades)) Data = 3; // Age 3.
                }
                else if (string.Compare(Nombre, "minecraft:brewing_stand", true) == 0) // ID: 117.
                {
                    if (Buscar_Propiedad("has_bottle_0: true", Lista_Propiedades)) Data |= 1; // The slot pointing east.
                    if (Buscar_Propiedad("has_bottle_2: true", Lista_Propiedades)) Data |= 2; // The slot pointing southwest.
                    if (Buscar_Propiedad("has_bottle_1: true", Lista_Propiedades)) Data |= 4; // The slot pointing northwest.
                }
                else if (string.Compare(Nombre, "minecraft:cauldron", true) == 0) // ID: 118.
                {
                    if (Buscar_Propiedad("level: 0", Lista_Propiedades)) Data = 0; // Empty.
                    else if (Buscar_Propiedad("level: 1", Lista_Propiedades)) Data = 1; // ⅓ filled.
                    else if (Buscar_Propiedad("level: 2", Lista_Propiedades)) Data = 2; // ⅔ filled.
                    else if (Buscar_Propiedad("level: 3", Lista_Propiedades)) Data = 3; // Fully filled.
                }
                else if (string.Compare(Nombre, "minecraft:end_portal_frame", true) == 0) // ID: 120.
                {
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // To the south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // To the west.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // To the north.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // To the east.

                    if (Buscar_Propiedad("eye: true", Lista_Propiedades)) Data |= 4; // 0x4 is a bit flag: 0 is an "empty" frame block, 1 is a block with an Eye of Ender inserted.
                }
                else if (string.Compare(Nombre, "minecraft:redstone_lamp", true) == 0) // ID: 123 ~ 124.
                {
                    if (Buscar_Propiedad("lit: true", Lista_Propiedades)) ID = 124; // Lit.
                }
                else if (string.Compare(Nombre, "minecraft:cocoa", true) == 0) // ID: 127.
                {
                    // The directions are inverted, which is confusing.
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // Attached to the north.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // Attached to the east.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Attached to the south.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Attached to the west.

                    if (Buscar_Propiedad("age: 0", Lista_Propiedades)) Data |= 0; // First stage.
                    else if (Buscar_Propiedad("age: 1", Lista_Propiedades)) Data |= 4; // Second stage.
                    else if (Buscar_Propiedad("age: 2", Lista_Propiedades)) Data |= 8; // Final stage.
                }
                else if (string.Compare(Nombre, "minecraft:tripwire_hook", true) == 0) // ID: 131.
                {
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // Tripwire hook on block side facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // Tripwire hook on block side facing west.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Tripwire hook on block side facing north.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Tripwire hook on block side facing east.

                    if (Buscar_Propiedad("attached: true", Lista_Propiedades)) Data |= 4; // If set, the tripwire hook is connected and ready to trip ("middle" position).

                    if (Buscar_Propiedad("powered: true", Lista_Propiedades)) Data |= 8; // If set, the tripwire hook is currently activated ("down" position).
                }
                else if (string.Compare(Nombre, "minecraft:tripwire", true) == 0) // ID: 132.
                {
                    if (Buscar_Propiedad("powered: true", Lista_Propiedades)) Data |= 1; // Set if tripwire is activated (an entity is intersecting its collision mask).
                    //if (Buscar_Propiedad("", Lista_Propiedades)) Data |= 2; // Unused.
                    if (Buscar_Propiedad("attached: true", Lista_Propiedades)) Data |= 4; // Set if tripwire is attached to a valid tripwire circuit.
                    if (Buscar_Propiedad("disarmed: true", Lista_Propiedades)) Data |= 8; // Set if tripwire is disarmed.
                }
                else if (string.Compare(Nombre, "minecraft:creeper_head", true) == 0 ||
                    string.Compare(Nombre, "minecraft:dragon_head", true) == 0 ||
                    string.Compare(Nombre, "minecraft:player_head", true) == 0 ||
                    string.Compare(Nombre, "minecraft:skeleton_skull", true) == 0 ||
                    string.Compare(Nombre, "minecraft:wither_skeleton_skull", true) == 0 ||
                    string.Compare(Nombre, "minecraft:zombie_head", true) == 0) // ID: 144.
                {
                    Data = 1; // On the floor (rotation is stored in the tile entity).
                }
                else if (string.Compare(Nombre, "minecraft:creeper_wall_head", true) == 0 ||
                    string.Compare(Nombre, "minecraft:dragon_wall_head", true) == 0 ||
                    string.Compare(Nombre, "minecraft:player_wall_head", true) == 0 ||
                    string.Compare(Nombre, "minecraft:skeleton_wall_skull", true) == 0 ||
                    string.Compare(Nombre, "minecraft:wither_skeleton_wall_skull", true) == 0 ||
                    string.Compare(Nombre, "minecraft:zombie_wall_head", true) == 0) // ID: 144.
                {
                    if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // On a wall, facing north.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // On a wall, facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // On a wall, facing east.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // On a wall, facing west.

                    ID = 144; // Needs a change of ID.
                }
                else if (string.Compare(Nombre, "minecraft:anvil", true) == 0) // ID: 145.
                {
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // Anvil facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // Anvil facing west.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Anvil facing north.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Anvil facing east.
                }
                else if (string.Compare(Nombre, "minecraft:chipped_anvil", true) == 0) // ID: 145.
                {
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // Anvil facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // Anvil facing west.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Anvil facing north.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Anvil facing east.

                    Data |= 4; // Slightly Damaged Anvil.
                }
                else if (string.Compare(Nombre, "minecraft:damaged_anvil", true) == 0) // ID: 145.
                {
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // Anvil facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // Anvil facing west.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Anvil facing north.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Anvil facing east.

                    Data |= 8; // Very Damaged Anvil.
                }
                else if (string.Compare(Nombre, "minecraft:comparator", true) == 0) // ID: 149 ~ 150.
                {
                    // If it was an item in a hopper, after the conversion it will be gone,
                    // and then the comparator will still be on, and after putting back a
                    // new item in the hopper, it will be off forever, and finally needing
                    // to remove it and place it back for it work again as expected...
                    // I'm not sure how I could fix this bug, so any suggestion is welcome.
                    /*if (Buscar_Propiedad("powered: true", Lista_Propiedades))
                    {
                        // Note that the arrow or triangle points in the opposite direction, which is confusing.
                        if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 0; // Facing north.
                        else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 1; // Facing east.
                        else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 2; // Facing south.
                        else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 3; // Facing west.

                        if (Buscar_Propiedad("mode: subtract", Lista_Propiedades)) Data |= 4; // Set if in subtraction mode (front torch up and powered).

                        Data |= 8; // Set if powered (at any power level).

                        ID = 150; // Powered needs a change of ID.
                    }
                    else*/
                    {
                        if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // Facing south.
                        else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // Facing west.
                        else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Facing north.
                        else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Facing east.

                        if (Buscar_Propiedad("mode: subtract", Lista_Propiedades)) Data |= 4; // Set if in subtraction mode (front torch up and powered).
                    }
                }
                else if (string.Compare(Nombre, "minecraft:daylight_detector", true) == 0) // ID: 151 ~ 178.
                {
                    if (Buscar_Propiedad("power: 0", Lista_Propiedades)) Data = 0; // Power 0.
                    else if (Buscar_Propiedad("power: 1", Lista_Propiedades)) Data = 1; // Power 1.
                    else if (Buscar_Propiedad("power: 2", Lista_Propiedades)) Data = 2; // Power 2.
                    else if (Buscar_Propiedad("power: 3", Lista_Propiedades)) Data = 3; // Power 3.
                    else if (Buscar_Propiedad("power: 4", Lista_Propiedades)) Data = 4; // Power 4.
                    else if (Buscar_Propiedad("power: 5", Lista_Propiedades)) Data = 5; // Power 5.
                    else if (Buscar_Propiedad("power: 6", Lista_Propiedades)) Data = 6; // Power 6.
                    else if (Buscar_Propiedad("power: 7", Lista_Propiedades)) Data = 7; // Power 7.
                    else if (Buscar_Propiedad("power: 8", Lista_Propiedades)) Data = 8; // Power 8.
                    else if (Buscar_Propiedad("power: 9", Lista_Propiedades)) Data = 9; // Power 9.
                    else if (Buscar_Propiedad("power: 10", Lista_Propiedades)) Data = 10; // Power 10.
                    else if (Buscar_Propiedad("power: 11", Lista_Propiedades)) Data = 11; // Power 11.
                    else if (Buscar_Propiedad("power: 12", Lista_Propiedades)) Data = 12; // Power 12.
                    else if (Buscar_Propiedad("power: 13", Lista_Propiedades)) Data = 13; // Power 13.
                    else if (Buscar_Propiedad("power: 14", Lista_Propiedades)) Data = 14; // Power 14.
                    else if (Buscar_Propiedad("power: 15", Lista_Propiedades)) Data = 15; // Power 15.

                    if (Buscar_Propiedad("inverted: true", Lista_Propiedades)) ID = 178; // Inverted needs a change of ID.
                }
                else if (string.Compare(Nombre, "minecraft:hopper", true) == 0) // ID: 154.
                {
                    if (Buscar_Propiedad("facing: down", Lista_Propiedades)) Data = 0; // Output facing down.
                                                                                       //else if (Buscar_Propiedad("facing: up", Lista_Propiedades)) Data = 1; // (unused). But, why Mojang didn't add hoppers going up?
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Output facing north.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // Output facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // Output facing west.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // Output facing east.

                    if (Buscar_Propiedad("enabled: false", Lista_Propiedades)) Data |= 8; // Set if activated/disabled.
                }
                else if (string.Compare(Nombre, "minecraft:acacia_leaves", true) == 0) // ID: 161, 0.
                {
                    /*if (Buscar_Propiedad("persistent: true", Lista_Propiedades)) */Data = 12; // Acacia Leaves (no decay and check decay).
                    //else Data = 0; // Acacia Leaves.
                }
                else if (string.Compare(Nombre, "minecraft:dark_oak_leaves", true) == 0) // ID: 161, 1.
                {
                    /*if (Buscar_Propiedad("persistent: true", Lista_Propiedades)) */Data = 13; // Dark Oak Leaves (no decay and check decay).
                    //else Data = 1; // Dark Oak Leaves.
                }
                else if (string.Compare(Nombre, "minecraft:acacia_log", true) == 0 ||
                    string.Compare(Nombre, "minecraft:stripped_acacia_log", true) == 0 ||
                    string.Compare(Nombre, "minecraft:stripped_acacia_wood", true) == 0) // ID: 162, 0.
                {
                    Data = 0; // Acacia Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    ID = 162; // change the ID to log.
                }
                else if (string.Compare(Nombre, "minecraft:acacia_bark", true) == 0 ||
                    string.Compare(Nombre, "minecraft:acacia_wood", true) == 0) // ID: 162, 0.
                {
                    Data = 0; // Acacia Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    Data |= 12; // Only bark.

                    ID = 162; // change the ID to log.
                }
                else if (string.Compare(Nombre, "minecraft:dark_oak_log", true) == 0 ||
                    string.Compare(Nombre, "minecraft:stripped_dark_oak_log", true) == 0 ||
                    string.Compare(Nombre, "minecraft:stripped_dark_oak_wood", true) == 0) // ID: 162, 1.
                {
                    Data = 1; // Dark Oak Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    ID = 162; // change the ID to log.
                }
                else if (string.Compare(Nombre, "minecraft:dark_oak_bark", true) == 0 ||
                    string.Compare(Nombre, "minecraft:dark_oak_wood", true) == 0) // ID: 162, 1.
                {
                    Data = 1; // Dark Oak Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    Data |= 12; // Only bark.

                    ID = 162; // change the ID to log.
                }
                else if (string.Compare(Nombre, "minecraft:large_fern", true) == 0 ||
                    string.Compare(Nombre, "minecraft:lilac", true) == 0 ||
                    string.Compare(Nombre, "minecraft:peony", true) == 0 ||
                    string.Compare(Nombre, "minecraft:rose_bush", true) == 0 ||
                    string.Compare(Nombre, "minecraft:sunflower", true) == 0 ||
                    string.Compare(Nombre, "minecraft:tall_grass", true) == 0) // ID: 175.
                {
                    if (Buscar_Propiedad("half: upper", Lista_Propiedades)) Data = 8; // Top Half of any Large Plant; low three bits 0x7 are derived from the block below.
                }
                else if (string.Compare(Nombre, "minecraft:banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:black_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:blue_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:brown_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:cyan_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:gray_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:green_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:light_blue_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:light_gray_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:lime_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:magenta_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:orange_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:pink_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:purple_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:red_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:white_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:yellow_banner", true) == 0) // ID: 176.
                {
                    if (Buscar_Propiedad("rotation: 0", Lista_Propiedades)) Data = 0; // Rotation 0.
                    else if (Buscar_Propiedad("rotation: 1", Lista_Propiedades)) Data = 1; // Rotation 1.
                    else if (Buscar_Propiedad("rotation: 2", Lista_Propiedades)) Data = 2; // Rotation 2.
                    else if (Buscar_Propiedad("rotation: 3", Lista_Propiedades)) Data = 3; // Rotation 3.
                    else if (Buscar_Propiedad("rotation: 4", Lista_Propiedades)) Data = 4; // Rotation 4.
                    else if (Buscar_Propiedad("rotation: 5", Lista_Propiedades)) Data = 5; // Rotation 5.
                    else if (Buscar_Propiedad("rotation: 6", Lista_Propiedades)) Data = 6; // Rotation 6.
                    else if (Buscar_Propiedad("rotation: 7", Lista_Propiedades)) Data = 7; // Rotation 7.
                    else if (Buscar_Propiedad("rotation: 8", Lista_Propiedades)) Data = 8; // Rotation 8.
                    else if (Buscar_Propiedad("rotation: 9", Lista_Propiedades)) Data = 9; // Rotation 9.
                    else if (Buscar_Propiedad("rotation: 10", Lista_Propiedades)) Data = 10; // Rotation 10.
                    else if (Buscar_Propiedad("rotation: 11", Lista_Propiedades)) Data = 11; // Rotation 11.
                    else if (Buscar_Propiedad("rotation: 12", Lista_Propiedades)) Data = 12; // Rotation 12.
                    else if (Buscar_Propiedad("rotation: 13", Lista_Propiedades)) Data = 13; // Rotation 13.
                    else if (Buscar_Propiedad("rotation: 14", Lista_Propiedades)) Data = 14; // Rotation 14.
                    else if (Buscar_Propiedad("rotation: 15", Lista_Propiedades)) Data = 15; // Rotation 15.
                }
                else if (string.Compare(Nombre, "minecraft:black_wall_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:blue_wall_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:brown_wall_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:cyan_wall_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:gray_wall_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:green_wall_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:light_blue_wall_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:light_gray_wall_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:lime_wall_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:magenta_wall_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:orange_wall_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:pink_wall_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:purple_wall_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:red_wall_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:wall_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:white_wall_banner", true) == 0 ||
                    string.Compare(Nombre, "minecraft:yellow_wall_banner", true) == 0) // ID: 177.
                {
                    if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // north.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // west.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // east.

                    ID = 177; // Needs a change of ID.
                }
                else if (string.Compare(Nombre, "minecraft:end_rod", true) == 0) // ID: 198.
                {
                    if (Buscar_Propiedad("facing: down", Lista_Propiedades)) Data = 0; // Facing down.
                    else if (Buscar_Propiedad("facing: up", Lista_Propiedades)) Data = 1; // Facing up.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Facing north.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // Facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // Facing west.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // Facing east.
                }
                else if (string.Compare(Nombre, "minecraft:chorus_flower", true) == 0) // ID: 200.
                {
                    if (Buscar_Propiedad("age: 0", Lista_Propiedades)) Data = 0000; // Age 0.
                    else if (Buscar_Propiedad("age: 1", Lista_Propiedades)) Data = 0000; // Age 1.
                    else if (Buscar_Propiedad("age: 2", Lista_Propiedades)) Data = 0000; // Age 2.
                    else if (Buscar_Propiedad("age: 3", Lista_Propiedades)) Data = 0000; // Age 3.
                    else if (Buscar_Propiedad("age: 4", Lista_Propiedades)) Data = 0000; // Age 4.
                    else if (Buscar_Propiedad("age: 5", Lista_Propiedades)) Data = 0000; // Age 5, the data value denotes its age, it will not grow anymore when data value is 0x5.
                }
                else if (string.Compare(Nombre, "minecraft:beetroots", true) == 0) // ID: 207.
                {
                    if (Buscar_Propiedad("age: 0", Lista_Propiedades)) Data = 0; // Age 0.
                    else if (Buscar_Propiedad("age: 1", Lista_Propiedades)) Data = 1; // Age 1.
                    else if (Buscar_Propiedad("age: 2", Lista_Propiedades)) Data = 2; // Age 2.
                    else if (Buscar_Propiedad("age: 3", Lista_Propiedades)) Data = 3; // Age 3.
                }
                else if (string.Compare(Nombre, "minecraft:observer", true) == 0) // ID: 218.
                {
                    // The directions seem to be inverted, which is confusing.
                    if (Buscar_Propiedad("facing: down", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)0 : (byte)1; // Facing down.
                    else if (Buscar_Propiedad("facing: up", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)1 : (byte)0; // Facing up.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Facing south.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // Facing north.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // Facing east.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // Facing west.
                }
                else if (string.Compare(Nombre, "minecraft:black_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre, "minecraft:blue_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre, "minecraft:brown_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre, "minecraft:cyan_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre, "minecraft:gray_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre, "minecraft:green_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre, "minecraft:light_blue_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre, "minecraft:light_gray_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre, "minecraft:lime_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre, "minecraft:magenta_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre, "minecraft:orange_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre, "minecraft:pink_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre, "minecraft:purple_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre, "minecraft:red_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre, "minecraft:white_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre, "minecraft:yellow_glazed_terracotta", true) == 0) // ID: 235 ~ 250.
                {
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // south (the player was facing north when this block was placed).
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // west.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // north.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // east.
                }
                else if (string.Compare(Nombre, "minecraft:structure_block", true) == 0) // ID: 255.
                {
                    if (Buscar_Propiedad("mode: save", Lista_Propiedades)) Data = 0; // Save.
                    else if (Buscar_Propiedad("mode: load", Lista_Propiedades)) Data = 1; // Load.
                    else if (Buscar_Propiedad("mode: corner", Lista_Propiedades)) Data = 2; // Corner.
                    else if (Buscar_Propiedad("mode: data", Lista_Propiedades)) Data = 3; // Data.
                }
                return ID;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return ID_Original;
        }

        #region Lista_Biomas // Minecraft 1.12.2- Biome list.
        /// <summary>
        /// List used to force a unique biome in a existing dimension.
        /// </summary>
        internal static readonly List<KeyValuePair<byte, string>> Lista_Biomas = new List<KeyValuePair<byte, string>>(new KeyValuePair<byte, string>[]
        {
            new KeyValuePair<byte, string>(0, "Ocean"),
            new KeyValuePair<byte, string>(1, "Plains"),
            new KeyValuePair<byte, string>(2, "Desert"),
            new KeyValuePair<byte, string>(3, "Extreme hills"),
            new KeyValuePair<byte, string>(4, "Forest"),
            new KeyValuePair<byte, string>(5, "Taiga"),
            new KeyValuePair<byte, string>(6, "Swampland"),
            new KeyValuePair<byte, string>(7, "River"),
            new KeyValuePair<byte, string>(8, "Hell"),
            new KeyValuePair<byte, string>(9, "Sky"),
            new KeyValuePair<byte, string>(10, "Frozen ocean"),
            new KeyValuePair<byte, string>(11, "Frozen river"),
            new KeyValuePair<byte, string>(12, "Ice flats"),
            new KeyValuePair<byte, string>(13, "Ice mountains"),
            new KeyValuePair<byte, string>(14, "Mushroom island"),
            new KeyValuePair<byte, string>(15, "Mushroom island shore"),
            new KeyValuePair<byte, string>(16, "Beaches"),
            new KeyValuePair<byte, string>(17, "Desert hills"),
            new KeyValuePair<byte, string>(18, "Forest hills"),
            new KeyValuePair<byte, string>(19, "Taiga hills"),
            new KeyValuePair<byte, string>(20, "Smaller extreme hills"),
            new KeyValuePair<byte, string>(21, "Jungle"),
            new KeyValuePair<byte, string>(22, "Jungle hills"),
            new KeyValuePair<byte, string>(23, "Jungle edge"),
            new KeyValuePair<byte, string>(24, "Deep ocean"),
            new KeyValuePair<byte, string>(25, "Stone beach"),
            new KeyValuePair<byte, string>(26, "Cold beach"),
            new KeyValuePair<byte, string>(27, "Birch forest"),
            new KeyValuePair<byte, string>(28, "Birch forest hills"),
            new KeyValuePair<byte, string>(29, "Roofed forest"),
            new KeyValuePair<byte, string>(30, "Taiga cold"),
            new KeyValuePair<byte, string>(31, "Taiga cold hills"),
            new KeyValuePair<byte, string>(32, "Redwood taiga"),
            new KeyValuePair<byte, string>(33, "Redwood taiga hills"),
            new KeyValuePair<byte, string>(34, "Extreme hills with trees"),
            new KeyValuePair<byte, string>(35, "Savanna"),
            new KeyValuePair<byte, string>(36, "Savanna rock"),
            new KeyValuePair<byte, string>(37, "Mesa"),
            new KeyValuePair<byte, string>(38, "Mesa rock"),
            new KeyValuePair<byte, string>(39, "Mesa clear rock"),
            new KeyValuePair<byte, string>(127, "The void"),
            new KeyValuePair<byte, string>(129, "Mutated plains"),
            new KeyValuePair<byte, string>(130, "Mutated desert"),
            new KeyValuePair<byte, string>(131, "Mutated extreme hills"),
            new KeyValuePair<byte, string>(132, "Mutated forest"),
            new KeyValuePair<byte, string>(133, "Mutated taiga"),
            new KeyValuePair<byte, string>(134, "Mutated swampland"),
            new KeyValuePair<byte, string>(140, "Mutated ice flats"),
            new KeyValuePair<byte, string>(149, "Mutated jungle"),
            new KeyValuePair<byte, string>(151, "Mutated jungle edge"),
            new KeyValuePair<byte, string>(155, "Mutated birch forest"),
            new KeyValuePair<byte, string>(156, "Mutated birch forest hills"),
            new KeyValuePair<byte, string>(157, "Mutated roofed forest"),
            new KeyValuePair<byte, string>(158, "Mutated taiga cold"),
            new KeyValuePair<byte, string>(160, "Mutated redwood taiga"),
            new KeyValuePair<byte, string>(161, "Mutated redwood taiga hills"),
            new KeyValuePair<byte, string>(162, "Mutated extreme hills with trees"),
            new KeyValuePair<byte, string>(163, "Mutated savanna"),
            new KeyValuePair<byte, string>(164, "Mutated savanna rock"),
            new KeyValuePair<byte, string>(165, "Mutated mesa"),
            new KeyValuePair<byte, string>(166, "Mutated mesa rock"),
            new KeyValuePair<byte, string>(167, "Mutated mesa clear rock")
        });
        #endregion

        /// <summary>
        /// Thread function that converts the available dimensions of any 1.13+ Minecraft world.
        /// </summary>
        internal void Subproceso_DoWork()
        {
            bool Subproceso_Abortado = false; // Used to know if the window must be closed.
            try
            {
                Subproceso_Activo = true;
                Stopwatch Cronómetro_Total = Stopwatch.StartNew();
                Texto_Bloques_Desconocidos = null;
                Diccionario_Bloques_Desconocidos.Clear();
                Diccionario_Bloques_Obsoletos.Clear();
                Diccionario_Bloques_Únicos.Clear();
                ////Lista_Propiedades_Únicas.Clear(); // 2018_10_08_12_25_04_362
                //Lista_Propiedades_Ejemplos.Clear();
                this.Invoke(new Invocación.Delegado_Control_Cursor(Invocación.Ejecutar_Delegado_Control_Cursor), new object[] { this, Cursors.WaitCursor });
                //Stopwatch Cronómetro_Región = new Stopwatch();
                //Stopwatch Cronómetro_Chunk = new Stopwatch();

                // Start the progress bars.
                int Progreso_Región = 0;
                int Progreso_Total = 0;
                int Total_Regiones = Lista_Posiciones_Regiones_Overworld.Count + Lista_Posiciones_Regiones_Nether.Count + Lista_Posiciones_Regiones_The_End.Count;
                this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Región, "Region progress: " + Program.Traducir_Número_Decimales_Redondear(((double)Progreso_Región * 100d) / 1024d, 4) + " % (" + Program.Traducir_Número(Progreso_Región) + " of 1.024 chunks)" });
                this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Total, "Total progress: " + Program.Traducir_Número_Decimales_Redondear(Total_Regiones != 0 ? (((double)Progreso_Total * 100d) / (double)Total_Regiones) : 0d, 4) + " % (" + Program.Traducir_Número(Progreso_Total) + " of " + Program.Traducir_Número(Total_Regiones) + (Total_Regiones != 1L ? " regions)" : " region)") });
                this.Invoke(new Invocación.Delegado_ProgressBar_Maximum(Invocación.Ejecutar_Delegado_ProgressBar_Maximum), new object[] { Barra_Progreso_Chunk, 1024 });
                this.Invoke(new Invocación.Delegado_ProgressBar_Maximum(Invocación.Ejecutar_Delegado_ProgressBar_Maximum), new object[] { Barra_Progreso_Región, Total_Regiones });

                // Load all the available information for the level and player to copy those values later.
                Minecraft.Información_Niveles Información_Nivel = Minecraft.Información_Niveles.Obtener_Información_Nivel(Ruta_Mundo);
                Dictionary<string, Minecraft.Posiciones_Jugadores> Diccionario_Posiciones_Jugadores = Minecraft.Posiciones_Jugadores.Obtener_Posiciones_Jugadores(Ruta_Mundo);
                Minecraft.Posiciones_Jugadores Posición_Jugador = new Minecraft.Posiciones_Jugadores(Información_Nivel.SpawnX, Math.Min((Información_Nivel.SpawnY + 1L), 255L), Información_Nivel.SpawnZ); // Set the spawn point as the default player position, but 1 block higher (just in case).
                if (Diccionario_Posiciones_Jugadores != null && Diccionario_Posiciones_Jugadores.Count > 0)
                {
                    foreach (KeyValuePair<string, Minecraft.Posiciones_Jugadores> Entrada in Diccionario_Posiciones_Jugadores)
                    {
                        Posición_Jugador = Entrada.Value; // Use the position for the first player found.
                        break;
                    }
                }

                string Ruta = Program.Ruta_Guardado_Minecraft + "\\" + Program.Obtener_Nombre_Temporal() + " 1_13_to_1_12";
                if (Directory.Exists(Ruta))
                {
                    this.Invoke(new Invocación.Delegado_IWin32Window_MessageBox(Invocación.Ejecutar_Delegado_IWin32Window_MessageBox), new object[] { this, "Somehow the directory name for the new Minecraft map already exists.\r\nPlease try it again if the system clock is running properly.\r\nPath: \"" + Ruta + "\".", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning });
                    Ruta = null;
                    return;
                }
                Program.Crear_Carpetas(Ruta);
                AnvilWorld Mundo = AnvilWorld.Create(Ruta);
                Mundo.Level.LevelName = Path.GetFileName(Ruta);
                Mundo.Level.UseMapFeatures = true; // ?
                //Mundo.Level.GeneratorOptions = "1;minecraft:bedrock"; // Not used for now.
                Mundo.Level.GameType = GameType.CREATIVE;
                Mundo.Level.Spawn = new SpawnPoint((int)Información_Nivel.SpawnX, (int)Math.Min((Información_Nivel.SpawnY + 1L), 255L), (int)Información_Nivel.SpawnZ);
                Mundo.Level.AllowCommands = true; // Allow cheats.
                Mundo.Level.GameRules.DoMobSpawning = true; // Spawn mobs.
                Mundo.Level.GameRules.DoFireTick = false; // Prevent the new level to burn out.
                Mundo.Level.GameRules.MobGriefing = false; // Prevent the mobs to destroy anything.
                Mundo.Level.GameRules.KeepInventory = true; // Keep the player inventory.
                Mundo.Level.RainTime = (int)Información_Nivel.RainTime;
                Mundo.Level.IsRaining = Información_Nivel.Raining != 0L;
                Mundo.Level.Player = new Player();
                Mundo.Level.Player.Dimension = Posición_Jugador.Dimesión; // 0 = Overworld, -1 = Nether, +1 = The End.
                Mundo.Level.Player.Position = new Vector3();
                Mundo.Level.Player.Position.X = (double)Posición_Jugador.X; // Try to spawn where the player was.
                Mundo.Level.Player.Position.Y = (double)Posición_Jugador.Y;
                Mundo.Level.Player.Position.Z = (double)Posición_Jugador.Z;
                Substrate.Orientation Orientación = new Substrate.Orientation();
                Orientación.Pitch = 45d; // -90º a +90º // 45º = Camera centered (looking into the horizon).
                Orientación.Yaw = -45d; // -180º a +180º // -45º = Camera rotation (looking at the southeast).
                Mundo.Level.Player.Rotation = Orientación;
                Mundo.Level.Player.Spawn = new SpawnPoint((int)Información_Nivel.SpawnX, (int)Math.Min((Información_Nivel.SpawnY + 1L), 255L), (int)Información_Nivel.SpawnZ);
                Mundo.Level.Player.Abilities.Flying = true; // Start with creative flight enabled.
                Mundo.Level.RandomSeed = Información_Nivel.RandomSeed; // Copy the original seed.
                Mundo.Level.ThunderTime = (int)Información_Nivel.ThunderTime;
                Mundo.Level.IsThundering = Información_Nivel.Thundering != 0L;

                // Start the multiple dimensions at once to work with them.
                IChunkManager Chunks_Overworld = null;
                BlockManager Bloques_Overworld = null;
                IChunkManager Chunks_Nether = null;
                BlockManager Bloques_Nether = null;
                IChunkManager Chunks_The_End = null;
                BlockManager Bloques_The_End = null;
                //if (Lista_Posiciones_Regiones_Overworld != null && Lista_Posiciones_Regiones_Overworld.Count > 0)
                {
                    Chunks_Overworld = Mundo.GetChunkManager(0);
                    Bloques_Overworld = Mundo.GetBlockManager(0);
                }
                //if (Lista_Posiciones_Regiones_Nether != null && Lista_Posiciones_Regiones_Nether.Count > 0)
                {
                    Chunks_Nether = Mundo.GetChunkManager(-1);
                    Bloques_Nether = Mundo.GetBlockManager(-1);
                }
                //if (Lista_Posiciones_Regiones_The_End != null && Lista_Posiciones_Regiones_The_End.Count > 0)
                {
                    Chunks_The_End = Mundo.GetChunkManager(1);
                    Bloques_The_End = Mundo.GetBlockManager(1);
                }
                List<string> Listas_Rutas_Regiones = new List<string>();
                List<List<Point>> Listas_Posiciones_Regiones = new List<List<Point>>();

                // Allow cross-dimensions in the Overworld.
                if (Variable_Dimensión_Overworld == 0)
                {
                    Listas_Rutas_Regiones.Add(Ruta_Regiones_Overworld);
                    Listas_Posiciones_Regiones.Add(Lista_Posiciones_Regiones_Overworld);
                }
                else if (Variable_Dimensión_Overworld == 1)
                {
                    Listas_Rutas_Regiones.Add(Ruta_Regiones_Nether);
                    Listas_Posiciones_Regiones.Add(Lista_Posiciones_Regiones_Nether);
                }
                else if (Variable_Dimensión_Overworld == 2)
                {
                    Listas_Rutas_Regiones.Add(Ruta_Regiones_The_End);
                    Listas_Posiciones_Regiones.Add(Lista_Posiciones_Regiones_The_End);
                }

                // Allow cross-dimensions in the Nether.
                if (Variable_Dimensión_Nether == 0)
                {
                    Listas_Rutas_Regiones.Add(Ruta_Regiones_Overworld);
                    Listas_Posiciones_Regiones.Add(Lista_Posiciones_Regiones_Overworld);
                }
                else if (Variable_Dimensión_Nether == 1)
                {
                    Listas_Rutas_Regiones.Add(Ruta_Regiones_Nether);
                    Listas_Posiciones_Regiones.Add(Lista_Posiciones_Regiones_Nether);
                }
                else if (Variable_Dimensión_Nether == 2)
                {
                    Listas_Rutas_Regiones.Add(Ruta_Regiones_The_End);
                    Listas_Posiciones_Regiones.Add(Lista_Posiciones_Regiones_The_End);
                }

                // Allow cross-dimensions in The End.
                if (Variable_Dimensión_The_End == 0)
                {
                    Listas_Rutas_Regiones.Add(Ruta_Regiones_Overworld);
                    Listas_Posiciones_Regiones.Add(Lista_Posiciones_Regiones_Overworld);
                }
                else if (Variable_Dimensión_The_End == 1)
                {
                    Listas_Rutas_Regiones.Add(Ruta_Regiones_Nether);
                    Listas_Posiciones_Regiones.Add(Lista_Posiciones_Regiones_Nether);
                }
                else if (Variable_Dimensión_The_End == 2)
                {
                    Listas_Rutas_Regiones.Add(Ruta_Regiones_The_End);
                    Listas_Posiciones_Regiones.Add(Lista_Posiciones_Regiones_The_End);
                }

                // This will allow to mix or repeat blocks from all the Minecraft dimensions.
                List<IChunkManager> Lista_Chunks = new List<IChunkManager>(new IChunkManager[] { Chunks_Overworld, Chunks_Nether, Chunks_The_End });
                List<BlockManager> Lista_Bloques = new List<BlockManager>(new BlockManager[] { Bloques_Overworld, Bloques_Nether, Bloques_The_End });

                // This will quickly convert the worlds to "pixel art" made of wool, concrete, etc.
                List<string> Lista_Cuantización = new List<string>();
                Diccionario_Cuantización_Final.Clear(); // Reset each time.
                if (Diccionario_Cuantización.Count > 0)
                {
                    foreach (KeyValuePair<string, string> Entrada in Diccionario_Cuantización)
                    {
                        if (!Lista_Cuantización.Contains(Entrada.Key))
                        {
                            Lista_Cuantización.Add(Entrada.Key);
                        }
                    }
                }
                if (Lista_Cuantización.Count > 0) // This code is almost like the pixel art tool.
                {
                    // Generate an image with unique block colors for the later conversion.
                    Bitmap Imagen_Paleta = new Bitmap(Lista_Cuantización.Count, 1, PixelFormat.Format24bppRgb);
                    Graphics Pintar_Paleta = Graphics.FromImage(Imagen_Paleta);
                    Pintar_Paleta.CompositingMode = CompositingMode.SourceCopy;
                    for (int Índice_Paleta = 0; Índice_Paleta < Lista_Cuantización.Count; Índice_Paleta++)
                    {
                        if (Pendiente_Subproceso_Abortar)
                        {
                            Pendiente_Subproceso_Abortar = false;
                            Mundo.Save(); // Save the part of the world already generated.
                            Mundo = null;
                            Subproceso_Abortado = true;
                            return; // Cancel safely before time.
                        }
                        // Remove any trasparency in the color or it will fail.
                        SolidBrush Pincel = new SolidBrush(Minecraft.Bloques.Diccionario_Índice_Color_ARGB_Sólido[Minecraft.Bloques.Diccionario_Nombre_Índice[Lista_Cuantización[Índice_Paleta]]]);
                        Pintar_Paleta.FillRectangle(Pincel, Índice_Paleta, 0, 1, 1);
                        Pincel.Dispose();
                        Pincel = null;
                    }
                    Pintar_Paleta.Dispose();
                    Pintar_Paleta = null;
                    MagickImage Imagen_Mapa = new MagickImage(Imagen_Paleta); // Used to map colors to those.
                    Lista_Cuantización = null;

                    int Ancho = Minecraft.Bloques.Diccionario_Índice_Color_ARGB.Count - 5; // Ignore the previous 5 blocks.
                    int Alto = 1;
                    Bitmap Imagen_Bloques = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                    Graphics Pintar_Bloques = Graphics.FromImage(Imagen_Bloques);
                    Pintar_Bloques.CompositingMode = CompositingMode.SourceCopy;
                    //Pintar_Bloques.CompositingQuality = CompositingQuality.HighQuality;
                    //Pintar_Bloques.InterpolationMode = InterpolationMode.NearestNeighbor;
                    //Pintar_Bloques.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    //Pintar_Bloques.SmoothingMode = SmoothingMode.None;
                    //Pintar_Bloques.TextRenderingHint = TextRenderingHint.AntiAlias;
                    int Bloque_X = 0;
                    // Draw 1 pixel per known block horizontally with the average block colors.
                    foreach (KeyValuePair<short, Color> Entrada in Minecraft.Bloques.Diccionario_Índice_Color_ARGB_Sólido)
                    {
                        if (!Bloque_Índice_Aire_Agua_Lava(Entrada.Key)) // Ignore those blocks.
                        {
                            // Remove any trasparency in the color or it will fail.
                            SolidBrush Pincel = new SolidBrush(Entrada.Value);
                            Pintar_Bloques.FillRectangle(Pincel, Bloque_X, 0, 1, 1);
                            Pincel.Dispose();
                            Pincel = null;
                            Bloque_X++;
                        }
                    }
                    Pintar_Bloques.Dispose();
                    Pintar_Bloques = null;

                    // Map or filter all the block colors and replace them with the allowed ones.
                    MagickImage Imagen_Mapeada = new MagickImage(Imagen_Bloques); // This library is needed for mapping colors.
                    QuantizeSettings Ajustes_Cuantización = new QuantizeSettings();
                    Ajustes_Cuantización.DitherMethod = DitherMethod.No; // It needs this.
                    Imagen_Mapeada.Map(Imagen_Mapa, Ajustes_Cuantización); // Map the colors.

                    // Convert the quantized image to a regular bitmap in PNG format.
                    Bitmap Imagen_Cuantización = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                    Graphics Pintar_Cuantización = Graphics.FromImage(Imagen_Cuantización);
                    Pintar_Cuantización.CompositingMode = CompositingMode.SourceCopy;
                    //Pintar_Cuantización.CompositingQuality = CompositingQuality.HighQuality;
                    //Pintar_Cuantización.InterpolationMode = InterpolationMode.NearestNeighbor;
                    //Pintar_Cuantización.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    //Pintar_Cuantización.SmoothingMode = SmoothingMode.None;
                    //Pintar_Cuantización.TextRenderingHint = TextRenderingHint.AntiAlias;
                    Pintar_Cuantización.DrawImage(Imagen_Mapeada.ToBitmap(ImageFormat.Png), new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                    Pintar_Cuantización.Dispose();
                    Pintar_Cuantización = null;

                    // Read all the pixels of the quantized image inside a byte array.
                    BitmapData Bitmap_Data = Imagen_Cuantización.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadOnly, Imagen_Cuantización.PixelFormat);
                    byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                    // If the image doesn't has 32 bits, this value will be added after each horizontal row.
                    //int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen_Cuantizada.PixelFormat)) / 8);
                    Imagen_Cuantización.UnlockBits(Bitmap_Data);
                    Bitmap_Data = null;
                    Imagen_Mapeada.Dispose();
                    Imagen_Mapeada = null;
                    Imagen_Bloques.Dispose();
                    Imagen_Bloques = null;

                    int Índice_BGRA = 0; // Index for the ARGB pixel array (with inverted colors).
                    // Write back the converted colors only as blocks between wool or concrete.
                    foreach (KeyValuePair<short, Color> Entrada in Minecraft.Bloques.Diccionario_Índice_Color_ARGB_Sólido)
                    {
                        if (Pendiente_Subproceso_Abortar)
                        {
                            Pendiente_Subproceso_Abortar = false;
                            //Archivo_Región = null; // Dispose the current region file.
                            //Chunks.Save(); // Save the part of the chunks already generated.
                            //Chunks = null;
                            //Bloques = null;
                            Mundo.Save(); // Save the part of the world already generated.
                            Mundo = null;
                            Subproceso_Abortado = true;
                            return; // Cancel safely before time.
                        }
                        if (!Bloque_Índice_Aire_Agua_Lava(Entrada.Key)) // Ignore those blocks.
                        {
                            // Remove any trasparency in the color or it will fail.
                            Diccionario_Cuantización_Final.Add(Minecraft.Bloques.Diccionario_Índice_Nombre[Entrada.Key], Minecraft.Bloques.Diccionario_Índice_Nombre[Minecraft.Bloques.Diccionario_Código_Hash_Color_Sólido_Índice[Color.FromArgb(255/*Matriz_Bytes[Índice + 3]*/, Matriz_Bytes[Índice_BGRA + 2], Matriz_Bytes[Índice_BGRA + 1], Matriz_Bytes[Índice_BGRA]).GetHashCode()]]); // The colors are stored in the array in BGRA order (inverted).
                            Índice_BGRA += 4; // Add 1 for each byte per pixel (32 bits per pixel / 8 bits per byte = 4 bytes of increase).
                        }
                    }
                    Matriz_Bytes = null;
                    Imagen_Mapa.Dispose();
                    Imagen_Mapa = null;
                    Imagen_Paleta.Dispose();
                    Imagen_Paleta = null;
                }

                long Bloques_Convertidos = 0L; // Used to know the real converted blocks.
                // Iterate through the available dimensions in the original world and convert them.
                for (int Índice_Dimensión = 0; Índice_Dimensión < 3; Índice_Dimensión++)
                {
                    if (Pendiente_Subproceso_Abortar)
                    {
                        Pendiente_Subproceso_Abortar = false;
                        Mundo.Save(); // Save the part of the world already generated.
                        Mundo = null;
                        Subproceso_Abortado = true;
                        return; // Cancel safely before time.
                    }
                    if (Listas_Posiciones_Regiones[Índice_Dimensión] != null && Listas_Posiciones_Regiones[Índice_Dimensión].Count > 0)
                    {
                        // Use this trick to avoid repeating the whole code for each dimension.
                        List<Point> Lista_Posiciones_Regiones = Listas_Posiciones_Regiones[Índice_Dimensión];
                        IChunkManager Chunks = Lista_Chunks[Índice_Dimensión];
                        BlockManager Bloques = Lista_Bloques[Índice_Dimensión];
                        foreach (Point Posición_Región in Lista_Posiciones_Regiones) // Region coordinates.
                        {
                            if (Pendiente_Subproceso_Abortar)
                            {
                                Pendiente_Subproceso_Abortar = false;
                                Chunks.Save(); // Save the part of the chunks already generated.
                                Chunks = null;
                                Bloques = null;
                                Mundo.Save(); // Save the part of the world already generated.
                                Mundo = null;
                                Subproceso_Abortado = true;
                                return; // Cancel safely before time.
                            }
                            string Ruta_Región = Listas_Rutas_Regiones[Índice_Dimensión] + "\\r." + Posición_Región.X.ToString() + "." + Posición_Región.Y.ToString();
                            bool Mundo_Anterior_1_6 = false;
                            if (File.Exists(Ruta_Región + ".mca")) Ruta_Región += ".mca";
                            else if (File.Exists(Ruta_Región + ".mcr"))
                            {
                                Ruta_Región += ".mcr";
                                Mundo_Anterior_1_6 = true; // It's an old world format (< MC 1.6).
                            }
                            else Ruta_Región = null; // Not found? A minute ago it was here.
                            if (!string.IsNullOrEmpty(Ruta_Región) && File.Exists(Ruta_Región))
                            {
                                Substrate_Jupisoft.Core.RegionFile Archivo_Región = new Substrate_Jupisoft.Core.RegionFile(Ruta_Región);
                                if (Archivo_Región != null)
                                {
                                    for (int Chunk_Z = 0; Chunk_Z < 32; Chunk_Z++)
                                    {
                                        for (int Chunk_X = 0; Chunk_X < 32; Chunk_X++)
                                        {
                                            if (Pendiente_Subproceso_Abortar)
                                            {
                                                Pendiente_Subproceso_Abortar = false;
                                                Archivo_Región = null; // Dispose the current region file.
                                                Chunks.Save(); // Save the part of the chunks already generated.
                                                Chunks = null;
                                                Bloques = null;
                                                Mundo.Save(); // Save the part of the world already generated.
                                                Mundo = null;
                                                Subproceso_Abortado = true;
                                                return; // Cancel safely before time.
                                            }
                                            AnvilChunk.Biomes_Jupisoft = null; // Reset this for the next use.
                                            byte Bioma_Chunk = 0;
                                            // Changing The end biome crashed my game, so avoid it.
                                            if (Variable_Biomas == 3 && Índice_Dimensión != Variable_Dimensión_The_End)
                                            {
                                                // Set a random biome in a full chunk (16 x 16 blocks).
                                                Bioma_Chunk = Lista_Biomas[Program.Rand.Next(0, Lista_Biomas.Count)].Key; // Random biomes.
                                            }

                                            // Update the progress bars in real time after every converted chunk.
                                            this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [Conversion time: " + Program.Traducir_Intervalo_Horas_Minutos_Segundos(Cronómetro_Total.Elapsed) + "]" });
                                            this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Región, "Region progress: " + Program.Traducir_Número_Decimales_Redondear(((double)Progreso_Región * 100d) / 1024d, 4) + " % (" + Program.Traducir_Número(Progreso_Región) + " of 1.024 chunks)" });
                                            this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Total, "Total progress: " + Program.Traducir_Número_Decimales_Redondear(Total_Regiones != 0 ? ((((double)Progreso_Total + (Progreso_Región / 1024d)) * 100d) / (double)Total_Regiones) : 0d, 4) + " % (" + Program.Traducir_Número(Progreso_Total) + " of " + Program.Traducir_Número(Total_Regiones) + (Total_Regiones != 1L ? " regions)" : " region)") });
                                            this.Invoke(new Invocación.Delegado_ProgressBar_Value(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Chunk, Progreso_Región });
                                            Progreso_Región++;

                                            int Chunk_X_Global = (Posición_Región.X * 32) + Chunk_X; // Chunk coordinates.
                                            int Chunk_Z_Global = (Posición_Región.Y * 32) + Chunk_Z;

                                            int Bloque_X_Global = Chunk_X_Global * 16; // Block coordinates.
                                            int Bloque_Z_Global = Chunk_Z_Global * 16;

                                            string Compresión; // Finds the compression used in the chunks.
                                            Stream Lector_Chunk = Archivo_Región.GetChunkDataInputStream(Chunk_X, Chunk_Z, out Compresión);
                                            if (Lector_Chunk != null)
                                            {
                                                Substrate_Jupisoft.Nbt.NbtTree Árbol = new Substrate_Jupisoft.Nbt.NbtTree(Lector_Chunk);
                                                if (Árbol != null && Árbol.Root != null)
                                                {
                                                    Substrate_Jupisoft.Nbt.TagNodeCompound Árbol_Compuesto = (Árbol.Root as Substrate_Jupisoft.Nbt.TagNode) as Substrate_Jupisoft.Nbt.TagNodeCompound;
                                                    if (Árbol_Compuesto != null)
                                                    {
                                                        Substrate_Jupisoft.Nbt.NbtTree _tree = new Substrate_Jupisoft.Nbt.NbtTree(Árbol_Compuesto);
                                                        Substrate_Jupisoft.Nbt.TagNodeCompound Nodo_Level = _tree.Root["Level"] as Substrate_Jupisoft.Nbt.TagNodeCompound;
                                                        if (!Mundo_Anterior_1_6)
                                                        {
                                                            if (Nodo_Level.ContainsKey("Status")) // Minecraft 1.13+
                                                            { // TODO: support 1.12.2- to 1.12.2- directly as a world editor with filters.
                                                                string Texto_Estado = Nodo_Level["Status"] as Substrate_Jupisoft.Nbt.TagNodeString;
                                                                if (!string.IsNullOrEmpty(Texto_Estado) && string.Compare(Texto_Estado, "empty", true) != 0) // The chunk is not empty. Needs to be expanded...
                                                                {
                                                                    // Changing The end biome crashed my game, so avoid it.
                                                                    if (Variable_Biomas != 1 && Índice_Dimensión != Variable_Dimensión_The_End)
                                                                    {
                                                                        // Try to save the original biomes for each chunk.
                                                                        int[] Matriz_Biomas = null;
                                                                        if (Nodo_Level.ContainsKey("Biomes")) Matriz_Biomas = Nodo_Level["Biomes"].ToTagIntArray();
                                                                        if (Matriz_Biomas == null || Matriz_Biomas.Length < 256) // On error use the unknown biome.
                                                                        {
                                                                            Matriz_Biomas = new int[256];
                                                                            for (int Índice_Bioma = 0; Índice_Bioma < 256; Índice_Bioma++)
                                                                            {
                                                                                Matriz_Biomas[Índice_Bioma] = Variable_Bioma_Vacío;
                                                                            }
                                                                        }
                                                                        AnvilChunk.Biomes_Jupisoft = new ZXByteArray(16, 16);
                                                                        for (int Índice_Z = 0, Índice = 0; Índice_Z < 16; Índice_Z++)
                                                                        {
                                                                            for (int Índice_X = 0; Índice_X < 16; Índice_X++, Índice++)
                                                                            {
                                                                                if (Pendiente_Subproceso_Abortar)
                                                                                {
                                                                                    Pendiente_Subproceso_Abortar = false;
                                                                                    Archivo_Región = null; // Dispose the current region file.
                                                                                    Chunks.Save(); // Save the part of the chunks already generated.
                                                                                    Chunks = null;
                                                                                    Bloques = null;
                                                                                    Mundo.Save(); // Save the part of the world already generated.
                                                                                    Mundo = null;
                                                                                    Subproceso_Abortado = true;
                                                                                    return; // Cancel safely before time.
                                                                                }
                                                                                if (Variable_Biomas == 2) Matriz_Biomas[Índice] = Lista_Biomas[Program.Rand.Next(0, Lista_Biomas.Count)].Key; // Random biomes.
                                                                                else if (Variable_Biomas == 3) Matriz_Biomas[Índice] = Bioma_Chunk;
                                                                                else if (Variable_Biomas > 3) Matriz_Biomas[Índice] = Lista_Biomas[Variable_Biomas - 4].Key; // Forced biomes.

                                                                                // Filter and change the Minecraft 1.13+ biomes.
                                                                                if (Matriz_Biomas[Índice] == 40) Matriz_Biomas[Índice] = 127; // minecraft:sky_island_low
                                                                                else if (Matriz_Biomas[Índice] == 41) Matriz_Biomas[Índice] = 127; // minecraft:sky_island_medium
                                                                                else if (Matriz_Biomas[Índice] == 42) Matriz_Biomas[Índice] = 127; // minecraft:sky_island_high
                                                                                else if (Matriz_Biomas[Índice] == 43) Matriz_Biomas[Índice] = 127; // minecraft:sky_island_barren
                                                                                else if (Matriz_Biomas[Índice] == 44) Matriz_Biomas[Índice] = 0; // minecraft:warm_ocean[
                                                                                else if (Matriz_Biomas[Índice] == 45) Matriz_Biomas[Índice] = 0; // minecraft:lukewarm_ocean
                                                                                else if (Matriz_Biomas[Índice] == 46) Matriz_Biomas[Índice] = 0; // minecraft:cold_ocean
                                                                                else if (Matriz_Biomas[Índice] == 47) Matriz_Biomas[Índice] = 24; // minecraft:warm_deep_ocean
                                                                                else if (Matriz_Biomas[Índice] == 48) Matriz_Biomas[Índice] = 24; // minecraft:lukewarm_deep_ocean
                                                                                else if (Matriz_Biomas[Índice] == 49) Matriz_Biomas[Índice] = 24; // minecraft:cold_deep_ocean
                                                                                else if (Matriz_Biomas[Índice] == 50) Matriz_Biomas[Índice] = 24; // minecraft:frozen_deep_ocean

                                                                                // Convert the 1.13+ biomes to 1.12.2-, replacing some of the new ones with the old ones.
                                                                                AnvilChunk.Biomes_Jupisoft[Índice_X, Índice_Z] = (byte)Matriz_Biomas[Índice];
                                                                            }
                                                                        }
                                                                        Matriz_Biomas = null;
                                                                    }

                                                                    // Load the 16 possible sections (16 x 16 x 16, or 4.096 blocks) for each chunk:
                                                                    Substrate_Jupisoft.Nbt.TagNodeList Nodo_Secciones = Nodo_Level["Sections"] as Substrate_Jupisoft.Nbt.TagNodeList;
                                                                    if (Nodo_Secciones != null)
                                                                    {
                                                                        Dictionary<SpawnPoint, string[]> Diccionario_Entidades = new Dictionary<SpawnPoint, string[]>();
                                                                        Substrate_Jupisoft.Nbt.TagNodeList Nodo_Entidades = Nodo_Level["TileEntities"] as Substrate_Jupisoft.Nbt.TagNodeList;
                                                                        if (Nodo_Entidades != null)
                                                                        {
                                                                            foreach (Substrate_Jupisoft.Nbt.TagNodeCompound Nodo_Entidad in Nodo_Entidades)
                                                                            {
                                                                                if (Pendiente_Subproceso_Abortar)
                                                                                {
                                                                                    Pendiente_Subproceso_Abortar = false;
                                                                                    Archivo_Región = null; // Dispose the current region file.
                                                                                    Chunks.Save(); // Save the part of the chunks already generated.
                                                                                    Chunks = null;
                                                                                    Bloques = null;
                                                                                    Mundo.Save(); // Save the part of the world already generated.
                                                                                    Mundo = null;
                                                                                    Subproceso_Abortado = true;
                                                                                    return; // Cancel safely before time.
                                                                                }
                                                                                if (Nodo_Entidad.ContainsKey("id"))
                                                                                {
                                                                                    string ID = Nodo_Entidad["id"].ToTagString();
                                                                                    if (!string.IsNullOrEmpty(ID) && Nodo_Entidad.ContainsKey("x") && Nodo_Entidad.ContainsKey("y") && Nodo_Entidad.ContainsKey("z"))
                                                                                    {
                                                                                        if (string.Compare(ID, "minecraft:sign", true) == 0 || string.Compare(ID, "minecraft:wall_sign", true) == 0)
                                                                                        {
                                                                                            string[] Matriz_Líneas = new string[4];
                                                                                            if (Nodo_Entidad.ContainsKey("Text1")) Matriz_Líneas[0] = Nodo_Entidad["Text1"].ToTagString();
                                                                                            if (Nodo_Entidad.ContainsKey("Text2")) Matriz_Líneas[1] = Nodo_Entidad["Text2"].ToTagString();
                                                                                            if (Nodo_Entidad.ContainsKey("Text3")) Matriz_Líneas[2] = Nodo_Entidad["Text3"].ToTagString();
                                                                                            if (Nodo_Entidad.ContainsKey("Text4")) Matriz_Líneas[3] = Nodo_Entidad["Text4"].ToTagString();
                                                                                            if (!string.IsNullOrEmpty(Matriz_Líneas[0])) // {"text":"9"}.
                                                                                            {
                                                                                                Matriz_Líneas[0] = Matriz_Líneas[0].Replace("{\"text\":\"", null);
                                                                                                Matriz_Líneas[0] = Matriz_Líneas[0].Substring(0, Matriz_Líneas[0].Length - 2);
                                                                                            }
                                                                                            if (!string.IsNullOrEmpty(Matriz_Líneas[1]))
                                                                                            {
                                                                                                Matriz_Líneas[1] = Matriz_Líneas[1].Replace("{\"text\":\"", null);
                                                                                                Matriz_Líneas[1] = Matriz_Líneas[1].Substring(0, Matriz_Líneas[1].Length - 2);
                                                                                            }
                                                                                            if (!string.IsNullOrEmpty(Matriz_Líneas[2]))
                                                                                            {
                                                                                                Matriz_Líneas[2] = Matriz_Líneas[2].Replace("{\"text\":\"", null);
                                                                                                Matriz_Líneas[2] = Matriz_Líneas[2].Substring(0, Matriz_Líneas[2].Length - 2);
                                                                                            }
                                                                                            if (!string.IsNullOrEmpty(Matriz_Líneas[3]))
                                                                                            {
                                                                                                Matriz_Líneas[3] = Matriz_Líneas[3].Replace("{\"text\":\"", null);
                                                                                                Matriz_Líneas[3] = Matriz_Líneas[3].Substring(0, Matriz_Líneas[3].Length - 2);
                                                                                            }
                                                                                            for (int Índice_Línea = 0; Índice_Línea < 4; Índice_Línea++) // Avoid null strings (just in case).
                                                                                            {
                                                                                                if (Pendiente_Subproceso_Abortar)
                                                                                                {
                                                                                                    Pendiente_Subproceso_Abortar = false;
                                                                                                    Archivo_Región = null; // Dispose the current region file.
                                                                                                    Chunks.Save(); // Save the part of the chunks already generated.
                                                                                                    Chunks = null;
                                                                                                    Bloques = null;
                                                                                                    Mundo.Save(); // Save the part of the world already generated.
                                                                                                    Mundo = null;
                                                                                                    Subproceso_Abortado = true;
                                                                                                    return; // Cancel safely before time.
                                                                                                }
                                                                                                if (string.IsNullOrEmpty(Matriz_Líneas[Índice_Línea])) Matriz_Líneas[Índice_Línea] = string.Empty;
                                                                                            }
                                                                                            SpawnPoint Vector = new SpawnPoint(Nodo_Entidad["x"].ToTagInt(), Nodo_Entidad["y"].ToTagInt(), Nodo_Entidad["z"].ToTagInt());
                                                                                            if (!Diccionario_Entidades.ContainsKey(Vector))
                                                                                            {
                                                                                                if (!Variable_Auto_Destrucción || ((Vector.X % 16 != 0 && (Vector.X + 1) % 16 != 0) || (Vector.Z % 16 != 0 && (Vector.Z + 1) % 16 != 0))) Diccionario_Entidades.Add(Vector, Matriz_Líneas);
                                                                                            }
                                                                                            //else MessageBox.Show(this, Vector.ToString(), "Vector XYZ repeated?");
                                                                                        }
                                                                                        //else if (string.Compare(ID, "minecraft:hopper", true) == 0)
                                                                                        {
                                                                                            // TODO: Add support so the items in the chests, etc won't be lost...
                                                                                        } // This might take a few weeks... or months! (but hopefully not years).
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        ChunkRef Chunk = Chunks.CreateChunk(Chunk_X_Global, Chunk_Z_Global);
                                                                        Chunk.IsLightPopulated = true; // For 1.13+ conversion support.
                                                                        Chunk.IsTerrainPopulated = true;
                                                                        Chunk.Blocks.AutoLight = false;
                                                                        List<int> Lista_Secciones_Existentes = new List<int>(); // The list with the existing sections.
                                                                        foreach (Substrate_Jupisoft.Nbt.TagNodeCompound Nodo_Sección in Nodo_Secciones)
                                                                        {
                                                                            if (Pendiente_Subproceso_Abortar)
                                                                            {
                                                                                Pendiente_Subproceso_Abortar = false;
                                                                                Archivo_Región = null; // Dispose the current region file.
                                                                                Chunks.Save(); // Save the part of the chunks already generated.
                                                                                Chunks = null;
                                                                                Bloques = null;
                                                                                Mundo.Save(); // Save the part of the world already generated.
                                                                                Mundo = null;
                                                                                Subproceso_Abortado = true;
                                                                                return; // Cancel safely before time.
                                                                            }
                                                                            Substrate_Jupisoft.Nbt.TagNodeCompound Árbol_Sección = Nodo_Sección as Substrate_Jupisoft.Nbt.TagNodeCompound;
                                                                            if (Árbol_Sección == null) continue; // Ignore empty sections.
                                                                            byte Sección_Y = Árbol_Sección["Y"] as Substrate_Jupisoft.Nbt.TagNodeByte;
                                                                            //if (Sección_Y < 0 || Sección_Y >= 16) MessageBox.Show(Sección_Y.ToString(), "Sección_Y");
                                                                            int Sección_Y_16 = Sección_Y * 16;
                                                                            Lista_Secciones_Existentes.Add(Sección_Y_16);
                                                                            //if (Sección_Y_16 > Chunk.Y_Máximo) Chunk.Y_Máximo = (short)Sección_Y_16;

                                                                            //short[] Matriz_Índices_Paleta = null;
                                                                            //int Índice_Paleta_Aire = -1;

                                                                            if (Árbol_Sección.ContainsKey("Palette") && Árbol_Sección.ContainsKey("BlockStates"))
                                                                            {
                                                                                List<AlphaBlock> Lista_Bloques_Paleta = new List<AlphaBlock>(); // Add up to 4.096 blocks per section.
                                                                                Substrate_Jupisoft.Nbt.TagNodeList Nodo_Paleta = Árbol_Sección["Palette"] as Substrate_Jupisoft.Nbt.TagNodeList;
                                                                                foreach (Substrate_Jupisoft.Nbt.TagNodeCompound Nodo_Nombre in Nodo_Paleta)
                                                                                {
                                                                                    if (Pendiente_Subproceso_Abortar)
                                                                                    {
                                                                                        Pendiente_Subproceso_Abortar = false;
                                                                                        Archivo_Región = null; // Dispose the current region file.
                                                                                        Chunks.Save(); // Save the part of the chunks already generated.
                                                                                        Chunks = null;
                                                                                        Bloques = null;
                                                                                        Mundo.Save(); // Save the part of the world already generated.
                                                                                        Mundo = null;
                                                                                        Subproceso_Abortado = true;
                                                                                        return; // Cancel safely before time.
                                                                                    }
                                                                                    if (Nodo_Nombre != null && Nodo_Nombre.ContainsKey("Name"))
                                                                                    {
                                                                                        string Nombre = Nodo_Nombre["Name"].ToTagString();
                                                                                        //if (string.IsNullOrEmpty(Nombre)) Nombre = "?";
                                                                                        //if (!Lista_Propiedades_Ejemplos.Contains(Nombre)) Lista_Propiedades_Ejemplos.Add(Nombre);
                                                                                        if (!string.IsNullOrEmpty(Nombre))
                                                                                        {
                                                                                            //Matriz_Índices_Paleta[Índice_Paleta] = (short)Índice_Paleta;
                                                                                            //Matriz_Índices_Paleta[Índice_Paleta] = Minecraft.Diccionario_Bloques_Nombres_Índices[Nombre];
                                                                                            //if (string.Compare(Nombre, "minecraft:air", true) == 0) Índice_Paleta_Aire = Índice_Paleta;
                                                                                            byte ID = 0; // Air.
                                                                                            byte Data = 0; // Default air.

                                                                                            // Check if the block properties exist or not.
                                                                                            if (!Nodo_Nombre.ContainsKey("Properties")) // Block without properties.
                                                                                            {
                                                                                                if (Minecraft.Diccionario_Bloques_Nombres_Índices.ContainsKey(Nombre))
                                                                                                {
                                                                                                    string Nombre_1_12_2;
                                                                                                    ID = Obtener_ID_Data_Minecraft_1_12_2(Nombre, new List<string>(), out Nombre_1_12_2, out Data);
                                                                                                    if (!Diccionario_Bloques_Obsoletos.ContainsKey(Nombre))
                                                                                                    {
                                                                                                        Diccionario_Bloques_Obsoletos.Add(Nombre, new List<string>());
                                                                                                    }
                                                                                                }
                                                                                                else // Unknown block found, so set it to air.
                                                                                                {
                                                                                                    ID = 0;
                                                                                                    Data = 0;
                                                                                                    // Warn the user later about any future unsupported block type.
                                                                                                    if (!Diccionario_Bloques_Desconocidos.ContainsKey(Nombre))
                                                                                                    {
                                                                                                        Diccionario_Bloques_Desconocidos.Add(Nombre, new List<string>());
                                                                                                    }
                                                                                                }
                                                                                                if (!Diccionario_Bloques_Únicos.ContainsKey(Nombre)) // Always add if not present.
                                                                                                {
                                                                                                    Diccionario_Bloques_Únicos.Add(Nombre, new List<string>());
                                                                                                }
                                                                                            }
                                                                                            else // Block with properties.
                                                                                            {
                                                                                                List<string> Lista_Propiedades = new List<string>();
                                                                                                Substrate_Jupisoft.Nbt.TagNodeCompound Nodo_Propiedades = Nodo_Nombre["Properties"].ToTagCompound();
                                                                                                foreach (string Clave in Nodo_Propiedades.Keys)
                                                                                                {
                                                                                                    if (Pendiente_Subproceso_Abortar)
                                                                                                    {
                                                                                                        Pendiente_Subproceso_Abortar = false;
                                                                                                        Archivo_Región = null; // Dispose the current region file.
                                                                                                        Chunks.Save(); // Save the part of the chunks already generated.
                                                                                                        Chunks = null;
                                                                                                        Bloques = null;
                                                                                                        Mundo.Save(); // Save the part of the world already generated.
                                                                                                        Mundo = null;
                                                                                                        Subproceso_Abortado = true;
                                                                                                        return; // Cancel safely before time.
                                                                                                    }
                                                                                                    string Propiedad = Clave + ": " + Nodo_Propiedades[Clave].ToTagString();
                                                                                                    Lista_Propiedades.Add(Propiedad);
                                                                                                    /*if (!Lista_Propiedades_Ejemplos.Contains("\"" + Propiedad + "\","))
                                                                                                    {
                                                                                                        Lista_Propiedades_Únicas.Add("\"" + Propiedad + "\", // " + Nombre + "."); // 2018_10_08_12_25_58_913
                                                                                                        Lista_Propiedades_Ejemplos.Add("\"" + Propiedad + "\",");
                                                                                                    }*/
                                                                                                }
                                                                                                if (Minecraft.Diccionario_Bloques_Nombres_Índices.ContainsKey(Nombre))
                                                                                                {
                                                                                                    string Nombre_1_12_2;
                                                                                                    ID = Obtener_ID_Data_Minecraft_1_12_2(Nombre, Lista_Propiedades, out Nombre_1_12_2, out Data);
                                                                                                    if (!Diccionario_Bloques_Obsoletos.ContainsKey(Nombre))
                                                                                                    {
                                                                                                        Diccionario_Bloques_Obsoletos.Add(Nombre, new List<string>());
                                                                                                    }
                                                                                                }
                                                                                                else // Unknown block found, so set it to air.
                                                                                                {
                                                                                                    ID = 0;
                                                                                                    Data = 0;
                                                                                                    // Warn the user later about any future unsupported block type.
                                                                                                    if (!Diccionario_Bloques_Desconocidos.ContainsKey(Nombre))
                                                                                                    {
                                                                                                        Diccionario_Bloques_Desconocidos.Add(Nombre, Lista_Propiedades);
                                                                                                    }
                                                                                                    else if (Lista_Propiedades.Count > 0)
                                                                                                    {
                                                                                                        // Learn all the properties for the new block types.
                                                                                                        foreach (string Propiedad in Lista_Propiedades)
                                                                                                        {
                                                                                                            if (!Diccionario_Bloques_Desconocidos[Nombre].Contains(Propiedad))
                                                                                                            {
                                                                                                                Diccionario_Bloques_Desconocidos[Nombre].Add(Propiedad);
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                                if (!Diccionario_Bloques_Únicos.ContainsKey(Nombre)) Diccionario_Bloques_Únicos.Add(Nombre, new List<string>());
                                                                                                foreach (string Propiedad in Lista_Propiedades)
                                                                                                {
                                                                                                    if (!Diccionario_Bloques_Únicos[Nombre].Contains(Propiedad))
                                                                                                    {
                                                                                                        Diccionario_Bloques_Únicos[Nombre].Add(Propiedad);
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            try
                                                                                            {
                                                                                                // Start the converted block type.
                                                                                                AlphaBlock Bloque = new AlphaBlock(ID, Data);

                                                                                                // Check if this block needs an entity with it or not.
                                                                                                // Substrate is missing all the new entities (and some old)!
                                                                                                // Sadly when setting with it a block that needs a TileEntity
                                                                                                // it throw exceptions that made the conversion impossible.
                                                                                                // So now this application has a custom Substrate library
                                                                                                // without the initial definitions of the blocks that might
                                                                                                // a TileEntity but still didn't have one on Substrate, and
                                                                                                // maybe that "temporary" fix will allow it to add those
                                                                                                // blocks on the world and "maybe" Minecraft will fix them (hopefully!).
                                                                                                if (ID == 23) // TileEntityTrap
                                                                                                {
                                                                                                    Substrate.TileEntities.TileEntityTrap Entidad = new Substrate.TileEntities.TileEntityTrap();
                                                                                                    Bloque.SetTileEntity(Entidad);
                                                                                                }
                                                                                                /*else if (ID == 34) // TileEntityPiston
                                                                                                {
                                                                                                    Substrate.TileEntities.TileEntityPiston Entidad = new Substrate.TileEntities.TileEntityPiston();
                                                                                                    Bloque.SetTileEntity(Entidad);
                                                                                                }*/ // Too old? Ignored for now also.
                                                                                                else if (ID == 25) // TileEntityMusic
                                                                                                {
                                                                                                    Substrate.TileEntities.TileEntityMusic Entidad = new Substrate.TileEntities.TileEntityMusic();
                                                                                                    Bloque.SetTileEntity(Entidad);
                                                                                                }
                                                                                                else if (ID == 52) // TileEntityMobSpawner
                                                                                                {
                                                                                                    Substrate.TileEntities.TileEntityMobSpawner Entidad = new Substrate.TileEntities.TileEntityMobSpawner();
                                                                                                    Bloque.SetTileEntity(Entidad);
                                                                                                }
                                                                                                else if (ID == 54) // TileEntityChest
                                                                                                {
                                                                                                    Substrate.TileEntities.TileEntityChest Entidad = new Substrate.TileEntities.TileEntityChest();
                                                                                                    Bloque.SetTileEntity(Entidad);
                                                                                                }
                                                                                                else if (ID == 61 || ID == 62) // TileEntityFurnace
                                                                                                {
                                                                                                    Substrate.TileEntities.TileEntityFurnace Entidad = new Substrate.TileEntities.TileEntityFurnace();
                                                                                                    Bloque.SetTileEntity(Entidad);
                                                                                                }
                                                                                                /*else if (ID == 63 || ID == 68) // TileEntitySign // This will be set later on.
                                                                                                {
                                                                                                    Substrate.TileEntities.TileEntitySign Entidad = new Substrate.TileEntities.TileEntitySign();
                                                                                                    Bloque.SetTileEntity(Entidad);
                                                                                                }*/
                                                                                                /*else if (ID == 84) // TileEntityRecordPlayer
                                                                                                {
                                                                                                    Substrate.TileEntities.TileEntityRecordPlayer Entidad = new Substrate.TileEntities.TileEntityRecordPlayer();
                                                                                                    Bloque.SetTileEntity(Entidad);
                                                                                                }*/
                                                                                                else if (ID == 116) // TileEntityEnchantmentTable
                                                                                                {
                                                                                                    Substrate.TileEntities.TileEntityEnchantmentTable Entidad = new Substrate.TileEntities.TileEntityEnchantmentTable();
                                                                                                    Bloque.SetTileEntity(Entidad);
                                                                                                }
                                                                                                else if (ID == 117) // TileEntityBrewingStand
                                                                                                {
                                                                                                    Substrate.TileEntities.TileEntityBrewingStand Entidad = new Substrate.TileEntities.TileEntityBrewingStand();
                                                                                                    Bloque.SetTileEntity(Entidad);
                                                                                                }
                                                                                                else if (ID == 119) // TileEntityEndPortal
                                                                                                {
                                                                                                    Substrate.TileEntities.TileEntityEndPortal Entidad = new Substrate.TileEntities.TileEntityEndPortal();
                                                                                                    Bloque.SetTileEntity(Entidad);
                                                                                                }
                                                                                                else if (ID == 137) // TileEntityControl
                                                                                                {
                                                                                                    Substrate.TileEntities.TileEntityControl Entidad = new Substrate.TileEntities.TileEntityControl();
                                                                                                    Bloque.SetTileEntity(Entidad);
                                                                                                }
                                                                                                else if (ID == 138) // TileEntityBeacon
                                                                                                {
                                                                                                    Substrate.TileEntities.TileEntityBeacon Entidad = new Substrate.TileEntities.TileEntityBeacon();
                                                                                                    Bloque.SetTileEntity(Entidad);
                                                                                                }
                                                                                                // Add the converted block to the custom list.
                                                                                                Lista_Bloques_Paleta.Add(Bloque);
                                                                                            }
                                                                                            catch (Exception Excepción)
                                                                                            {
                                                                                                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                                                                                                try { Lista_Bloques_Paleta.Add(new AlphaBlock(0, 0)); } // Air.
                                                                                                catch { }
                                                                                            }
                                                                                        }
                                                                                        else // Null block found, so set it to air.
                                                                                        {
                                                                                            // Adding it even if it's invalid won't make the block palette fully disordered.
                                                                                            Lista_Bloques_Paleta.Add(new AlphaBlock(0, 0)); // Add an Air block to the custom list.
                                                                                        }
                                                                                    }
                                                                                }
                                                                                if (Lista_Bloques_Paleta.Count == Nodo_Paleta.Count)
                                                                                {
                                                                                    int Bits_Bloque = 0; //Lista_Bits.Count / 4096; // Bits per block.

                                                                                    List<bool> Lista_Bits = new List<bool>((Árbol_Sección["BlockStates"] as Substrate_Jupisoft.Nbt.TagNodeLongArray).Matriz_Bits);
                                                                                    if (Lista_Bits != null && Lista_Bits.Count > 0)
                                                                                    {
                                                                                        Bits_Bloque = Math.Max(Lista_Bits.Count / 4096, 4); // Minimum of 4 bits.

                                                                                        bool[] Matriz_Bits = new bool[32];
                                                                                        int[] Matriz_Valor = new int[1];
                                                                                        int Índice_Bit = 0;

                                                                                        // Read the whole 16 x 16 x 16 blocks section (it's a 1/16 chunk).
                                                                                        for (int Índice_Y = 0; Índice_Y < 16; Índice_Y++)
                                                                                        {
                                                                                            // Read the 16 x 16 (256) rectangle of blocks.
                                                                                            for (int Índice_Z = 0; Índice_Z < 16; Índice_Z++)
                                                                                            {
                                                                                                // Read each row with 16 horizontal blocks.
                                                                                                for (int Índice_X = 0; Índice_X < 16; Índice_X++)
                                                                                                {
                                                                                                    if (Pendiente_Subproceso_Abortar)
                                                                                                    {
                                                                                                        Pendiente_Subproceso_Abortar = false;
                                                                                                        Archivo_Región = null; // Dispose the current region file.
                                                                                                        Chunks.Save(); // Save the part of the chunks already generated.
                                                                                                        Chunks = null;
                                                                                                        Bloques = null;
                                                                                                        Mundo.Save(); // Save the part of the world already generated.
                                                                                                        Mundo = null;
                                                                                                        Subproceso_Abortado = true;
                                                                                                        return; // Cancel safely before time.
                                                                                                    }
                                                                                                    Array.Clear(Matriz_Bits, 0, Matriz_Bits.Length);
                                                                                                    Lista_Bits.GetRange(Índice_Bit, Bits_Bloque).CopyTo(Matriz_Bits, 0); // 32 - Índice_Máscara);
                                                                                                                                                                         //Lista_Bits_Máscara.CopyTo(Matriz_Bits, 0); // 32 - Índice_Máscara);
                                                                                                    Matriz_Valor[0] = 0; // Reset... is this really necessary?
                                                                                                    new BitArray(Matriz_Bits).CopyTo(Matriz_Valor, 0);
                                                                                                    int Índice = Matriz_Valor[0];

                                                                                                    // Use this variable with 3 coordinates (in 3D) to quickly compare block positions.
                                                                                                    SpawnPoint Vector = new SpawnPoint(Bloque_X_Global + Índice_X, Sección_Y_16 + Índice_Y, Bloque_Z_Global + Índice_Z);
                                                                                                    if (!Diccionario_Entidades.ContainsKey(Vector)) // Save a regular block without entity.
                                                                                                    {
                                                                                                        try
                                                                                                        {
                                                                                                            if ((!Variable_Auto_Destrucción || ((Vector.X % 16 != 0 && (Vector.X + 1) % 16 != 0) || (Vector.Z % 16 != 0 && (Vector.Z + 1) % 16 != 0)) || Lista_Bloques_Paleta[Índice].ID == 0) && ((!Variable_Mundo_Agua && !Variable_Mundo_Lava) || Lista_Bloques_Paleta[Índice].ID != 0)) Chunk.Blocks.SetBlock(Índice_X, !Variable_Mundo_Invertido ? Sección_Y_16 + Índice_Y : 255 - (Sección_Y_16 + Índice_Y), Índice_Z, Lista_Bloques_Paleta[Índice]);
                                                                                                            else if (Lista_Bloques_Paleta[Índice].ID == 0) Chunk.Blocks.SetBlock(Índice_X, !Variable_Mundo_Invertido ? Sección_Y_16 + Índice_Y : 255 - (Sección_Y_16 + Índice_Y), Índice_Z, new AlphaBlock(Variable_Mundo_Agua ? 9 : 11, 0)); // Water or lava.
                                                                                                            else Chunk.Blocks.SetBlock(Índice_X, !Variable_Mundo_Invertido ? Sección_Y_16 + Índice_Y : 255 - (Sección_Y_16 + Índice_Y), Índice_Z, new AlphaBlock(46, 0)); // TNT.
                                                                                                        }
                                                                                                        catch (Exception Excepción)
                                                                                                        {
                                                                                                            Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                                                                                                            try { Chunk.Blocks.SetBlock(Índice_X, !Variable_Mundo_Invertido ? Sección_Y_16 + Índice_Y : 255 - (Sección_Y_16 + Índice_Y), Índice_Z, new AlphaBlock(0, 0)); } // Air.
                                                                                                            catch { }
                                                                                                        }
                                                                                                    }
                                                                                                    else // Save the text in all the signs and wall signs. The rest of entities will be lost.
                                                                                                    {
                                                                                                        string[] Matriz_Líneas = Diccionario_Entidades[Vector];
                                                                                                        if (Lista_Bloques_Paleta[Índice].ID == 63 || Lista_Bloques_Paleta[Índice].ID == 68) // "minecraft:sign" or "minecraft:wall_sign".
                                                                                                        {
                                                                                                            try
                                                                                                            {
                                                                                                                AlphaBlock Bloque = new AlphaBlock(Lista_Bloques_Paleta[Índice].ID, Lista_Bloques_Paleta[Índice].Data);
                                                                                                                Substrate.TileEntities.TileEntitySign Entidad = new Substrate.TileEntities.TileEntitySign();
                                                                                                                Entidad.Text1 = Matriz_Líneas[0];
                                                                                                                Entidad.Text2 = Matriz_Líneas[1];
                                                                                                                Entidad.Text3 = Matriz_Líneas[2];
                                                                                                                Entidad.Text4 = Matriz_Líneas[3];
                                                                                                                Entidad.X = Vector.X;
                                                                                                                Entidad.Y = Vector.Y;
                                                                                                                Entidad.Z = Vector.Z;
                                                                                                                Bloque.SetTileEntity(Entidad);
                                                                                                                Chunk.Blocks.SetBlock(Índice_X, !Variable_Mundo_Invertido ? Sección_Y_16 + Índice_Y : 255 - (Sección_Y_16 + Índice_Y), Índice_Z, Bloque);
                                                                                                            }
                                                                                                            catch (Exception Excepción)
                                                                                                            {
                                                                                                                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                                                                                                                try { Chunk.Blocks.SetBlock(Índice_X, !Variable_Mundo_Invertido ? Sección_Y_16 + Índice_Y : 255 - (Sección_Y_16 + Índice_Y), Índice_Z, new AlphaBlock(0, 0)); } // Air.
                                                                                                                catch { }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                    if (Variable_Luz != 16) // Set a custom light level in the whole world.
                                                                                                    {
                                                                                                        Chunk.Blocks.SetBlockLight(Índice_X, !Variable_Mundo_Invertido ? Sección_Y_16 + Índice_Y : 255 - (Sección_Y_16 + Índice_Y), Índice_Z, Variable_Luz != 17 ? Variable_Luz : Program.Rand.Next(0, 16));
                                                                                                        //Chunk.Blocks.SetSkyLight(Índice_X, Sección_Y_16 + Índice_Y, Índice_Z, 15);
                                                                                                    }
                                                                                                    Índice_Bit += Bits_Bloque; // Prepare the next block index for the next iteration.
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                                //else MessageBox.Show(""); // Several blocks are missing after the conversion?...
                                                                                Bloques_Convertidos += 4096L; // Add a full block section.
                                                                            }
                                                                        }
                                                                        if (Variable_Mundo_Agua || Variable_Mundo_Lava) // Force water or lava up to Y = 127.
                                                                        {
                                                                            AlphaBlock Bloque = new AlphaBlock(Variable_Mundo_Agua ? 9 : 11, 0);
                                                                            for (int Y = 0; Y < 128; Y += 16)
                                                                            {
                                                                                if (!Lista_Secciones_Existentes.Contains(Y)) // Write the missing sections.
                                                                                {
                                                                                    for (int Índice_Y = 0; Índice_Y < 16; Índice_Y++)
                                                                                    {
                                                                                        for (int Índice_Z = 0; Índice_Z < 16; Índice_Z++)
                                                                                        {
                                                                                            for (int Índice_X = 0; Índice_X < 16; Índice_X++)
                                                                                            {
                                                                                                Chunk.Blocks.SetBlock(Índice_X, !Variable_Mundo_Invertido ? Y + Índice_Y : 255 - (Y + Índice_Y), Índice_Z, Bloque);
                                                                                                if (Variable_Luz != 16) // Set a custom light level in the whole world.
                                                                                                {
                                                                                                    Chunk.Blocks.SetBlockLight(Índice_X, !Variable_Mundo_Invertido ? Y + Índice_Y : 255 - (Y + Índice_Y), Índice_Z, Variable_Luz != 17 ? Variable_Luz : Program.Rand.Next(0, 16));
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        if (Variable_Mundo_Invertido_Suelo)
                                                                        {
                                                                            for (int Índice_Y = 0; Índice_Y < 2; Índice_Y++)
                                                                            {
                                                                                for (int Índice_Z = 0; Índice_Z < 16; Índice_Z++)
                                                                                {
                                                                                    for (int Índice_X = 0; Índice_X < 16; Índice_X++)
                                                                                    {
                                                                                        Chunk.Blocks.SetBlock(Índice_X, Índice_Y, Índice_Z, new AlphaBlock(17, 12)); // Oak log (only bark).
                                                                                        if (Variable_Luz != 16) // Set a custom light level in the whole world.
                                                                                        {
                                                                                            Chunk.Blocks.SetBlockLight(Índice_X, Índice_Y, Índice_Z, Variable_Luz != 17 ? Variable_Luz : Program.Rand.Next(0, 16));
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                            for (int Índice_Z = 0; Índice_Z < 16; Índice_Z++)
                                                                            {
                                                                                for (int Índice_X = 0; Índice_X < 16; Índice_X++)
                                                                                {
                                                                                    Chunk.Blocks.SetBlock(Índice_X, 2, Índice_Z, new AlphaBlock(8, 0)); // Water.
                                                                                    if (Variable_Luz != 16) // Set a custom light level in the whole world.
                                                                                    {
                                                                                        Chunk.Blocks.SetBlockLight(Índice_X, 2, Índice_Z, Variable_Luz != 17 ? Variable_Luz : Program.Rand.Next(0, 16));
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        Chunk.Blocks.RebuildHeightMap(); // Automatic height map.
                                                                        if (Variable_Luz == 16) Chunk.Blocks.RebuildBlockLight(); // Automatic block light.
                                                                        Chunk.Blocks.RebuildSkyLight(); // Automatic sky light.
                                                                    }
                                                                }
                                                                //Chunk.Matriz_Bytes_ID = Matriz_Índices_Bloques_XYZ.Clone() as int[,,];
                                                                //Matriz_Chunks[Chunk_X, Chunk_Z] = Chunk;
                                                            }
                                                            else // Convert to it's same format.
                                                            {
                                                                // TODO: add this feature (1.12.2- to 1.12.2-).
                                                            }
                                                        }
                                                        else// if (Mundo_Anterior_1_6) // It's an old world format (< MC 1.6).
                                                        {
                                                            // Changing The end biome crashed my game, so avoid it.
                                                            if (Variable_Biomas != 1 && Índice_Dimensión != Variable_Dimensión_The_End)
                                                            {
                                                                // Try to save the original biomes for each chunk.
                                                                byte[] Matriz_Biomas = null;
                                                                if (Nodo_Level.ContainsKey("Biomes")) Matriz_Biomas = Nodo_Level["Biomes"].ToTagByteArray();
                                                                if (Matriz_Biomas == null || Matriz_Biomas.Length < 256) // On error use the unknown biome.
                                                                {
                                                                    Matriz_Biomas = new byte[256];
                                                                    for (int Índice_Bioma = 0; Índice_Bioma < 256; Índice_Bioma++)
                                                                    {
                                                                        Matriz_Biomas[Índice_Bioma] = (byte)Variable_Bioma_Vacío;
                                                                    }
                                                                }
                                                                AnvilChunk.Biomes_Jupisoft = new ZXByteArray(16, 16);
                                                                for (int Índice_Z = 0, Índice = 0; Índice_Z < 16; Índice_Z++)
                                                                {
                                                                    for (int Índice_X = 0; Índice_X < 16; Índice_X++, Índice++)
                                                                    {
                                                                        if (Pendiente_Subproceso_Abortar)
                                                                        {
                                                                            Pendiente_Subproceso_Abortar = false;
                                                                            Archivo_Región = null; // Dispose the current region file.
                                                                            Chunks.Save(); // Save the part of the chunks already generated.
                                                                            Chunks = null;
                                                                            Bloques = null;
                                                                            Mundo.Save(); // Save the part of the world already generated.
                                                                            Mundo = null;
                                                                            Subproceso_Abortado = true;
                                                                            return; // Cancel safely before time.
                                                                        }
                                                                        if (Variable_Biomas == 2) Matriz_Biomas[Índice] = Lista_Biomas[Program.Rand.Next(0, Lista_Biomas.Count)].Key; // Random biomes.
                                                                        else if (Variable_Biomas == 3) Matriz_Biomas[Índice] = Bioma_Chunk;
                                                                        else if (Variable_Biomas > 3) Matriz_Biomas[Índice] = Lista_Biomas[Variable_Biomas - 4].Key; // Forced biomes.

                                                                        // Filter and change the Minecraft 1.13+ biomes.
                                                                        if (Matriz_Biomas[Índice] == 40) Matriz_Biomas[Índice] = 127; // minecraft:sky_island_low
                                                                        else if (Matriz_Biomas[Índice] == 41) Matriz_Biomas[Índice] = 127; // minecraft:sky_island_medium
                                                                        else if (Matriz_Biomas[Índice] == 42) Matriz_Biomas[Índice] = 127; // minecraft:sky_island_high
                                                                        else if (Matriz_Biomas[Índice] == 43) Matriz_Biomas[Índice] = 127; // minecraft:sky_island_barren
                                                                        else if (Matriz_Biomas[Índice] == 44) Matriz_Biomas[Índice] = 0; // minecraft:warm_ocean[
                                                                        else if (Matriz_Biomas[Índice] == 45) Matriz_Biomas[Índice] = 0; // minecraft:lukewarm_ocean
                                                                        else if (Matriz_Biomas[Índice] == 46) Matriz_Biomas[Índice] = 0; // minecraft:cold_ocean
                                                                        else if (Matriz_Biomas[Índice] == 47) Matriz_Biomas[Índice] = 24; // minecraft:warm_deep_ocean
                                                                        else if (Matriz_Biomas[Índice] == 48) Matriz_Biomas[Índice] = 24; // minecraft:lukewarm_deep_ocean
                                                                        else if (Matriz_Biomas[Índice] == 49) Matriz_Biomas[Índice] = 24; // minecraft:cold_deep_ocean
                                                                        else if (Matriz_Biomas[Índice] == 50) Matriz_Biomas[Índice] = 24; // minecraft:frozen_deep_ocean

                                                                        // Convert the 1.6- possible biomes to 1.12.2-. // NOT TESTED YET!
                                                                        AnvilChunk.Biomes_Jupisoft[Índice_X, Índice_Z] = Matriz_Biomas[Índice];
                                                                    }
                                                                }
                                                                Matriz_Biomas = null;
                                                            }

                                                            // Assume the old world height limit of 128.

                                                            // Try to get the existing block IDs or get the default one.
                                                            byte[] Matriz_IDs = null;
                                                            if (Nodo_Level.ContainsKey("Blocks")) Matriz_IDs = Nodo_Level["Blocks"].ToTagByteArray();
                                                            if (Matriz_IDs == null || Matriz_IDs.Length < 32768) // On error use the unknown biome.
                                                            {
                                                                Matriz_IDs = new byte[32768];
                                                                for (int Índice_Y = 0, Índice = 0; Índice_Y < 128; Índice_Y++)
                                                                {
                                                                    for (int Índice_Z = 0; Índice_Z < 16; Índice_Z++)
                                                                    {
                                                                        for (int Índice_X = 0; Índice_X < 16; Índice_X++, Índice++)
                                                                        {
                                                                            if (Índice_Y != 0) Matriz_IDs[Índice] = 0; // Air.
                                                                            else Matriz_IDs[Índice] = 7; // Bedrock.
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            // Try to get the existing block Data or get the default one.
                                                            byte[] Matriz_Data = null;
                                                            if (Nodo_Level.ContainsKey("Data")) Matriz_Data = Nodo_Level["Data"].ToTagByteArray();
                                                            if (Matriz_Data == null || Matriz_Data.Length < 16384) // On error use the unknown biome.
                                                            {
                                                                Matriz_Data = new byte[16384];
                                                                for (int Índice = 0; Índice < 16384; Índice++)
                                                                {
                                                                    Matriz_Data[Índice] = 0; // Default block state.
                                                                }
                                                            }

                                                            // WARNING: YZX old chunk order.

                                                            ChunkRef Chunk = Chunks.CreateChunk(Chunk_X_Global, Chunk_Z_Global);
                                                            Chunk.IsLightPopulated = true; // For 1.13+ conversion support.
                                                            Chunk.IsTerrainPopulated = true;
                                                            Chunk.Blocks.AutoLight = false;
                                                            for (int Índice_X = 0, Índice_ID = 0, Índice_Data = 0; Índice_X < 16; Índice_X++)
                                                            {
                                                                for (int Índice_Z = 0; Índice_Z < 16; Índice_Z++)
                                                                {
                                                                    for (int Índice_Y = 0; Índice_Y < 128; Índice_Y++, Índice_ID++)
                                                                    {
                                                                        if (Pendiente_Subproceso_Abortar)
                                                                        {
                                                                            Pendiente_Subproceso_Abortar = false;
                                                                            Archivo_Región = null; // Dispose the current region file.
                                                                            Chunks.Save(); // Save the part of the chunks already generated.
                                                                            Chunks = null;
                                                                            Bloques = null;
                                                                            Mundo.Save(); // Save the part of the world already generated.
                                                                            Mundo = null;
                                                                            Subproceso_Abortado = true;
                                                                            return; // Cancel safely before time.
                                                                        }
                                                                        // Start the converted block type.
                                                                        byte ID = Matriz_IDs[Índice_ID];
                                                                        byte Data = Índice_ID % 2 != 0 ? (byte)((Matriz_Data[Índice_Data] >> 4) & 0xF) : (byte)(Matriz_Data[Índice_Data] & 0xF);
                                                                        AlphaBlock Bloque = new AlphaBlock(ID, Data);

                                                                        // Check if this block needs an entity with it or not.
                                                                        // Substrate is missing all the new entities (and some old)!
                                                                        // Sadly when setting with it a block that needs a TileEntity
                                                                        // it throw exceptions that made the conversion impossible.
                                                                        // So now this application has a custom Substrate library
                                                                        // without the initial definitions of the blocks that might
                                                                        // a TileEntity but still didn't have one on Substrate, and
                                                                        // maybe that "temporary" fix will allow it to add those
                                                                        // blocks on the world and "maybe" Minecraft will fix them (hopefully!).
                                                                        if (ID == 23) // TileEntityTrap
                                                                        {
                                                                            Substrate.TileEntities.TileEntityTrap Entidad = new Substrate.TileEntities.TileEntityTrap();
                                                                            Bloque.SetTileEntity(Entidad);
                                                                        }
                                                                        /*else if (ID == 34) // TileEntityPiston
                                                                        {
                                                                            Substrate.TileEntities.TileEntityPiston Entidad = new Substrate.TileEntities.TileEntityPiston();
                                                                            Bloque.SetTileEntity(Entidad);
                                                                        }*/ // Too old? Ignored for now also.
                                                                        else if (ID == 25) // TileEntityMusic
                                                                        {
                                                                            Substrate.TileEntities.TileEntityMusic Entidad = new Substrate.TileEntities.TileEntityMusic();
                                                                            Bloque.SetTileEntity(Entidad);
                                                                        }
                                                                        else if (ID == 52) // TileEntityMobSpawner
                                                                        {
                                                                            Substrate.TileEntities.TileEntityMobSpawner Entidad = new Substrate.TileEntities.TileEntityMobSpawner();
                                                                            Bloque.SetTileEntity(Entidad);
                                                                        }
                                                                        else if (ID == 54) // TileEntityChest
                                                                        {
                                                                            Substrate.TileEntities.TileEntityChest Entidad = new Substrate.TileEntities.TileEntityChest();
                                                                            Bloque.SetTileEntity(Entidad);
                                                                        }
                                                                        else if (ID == 61 || ID == 62) // TileEntityFurnace
                                                                        {
                                                                            Substrate.TileEntities.TileEntityFurnace Entidad = new Substrate.TileEntities.TileEntityFurnace();
                                                                            Bloque.SetTileEntity(Entidad);
                                                                        }
                                                                        /*else if (ID == 63 || ID == 68) // TileEntitySign // This will be set later on?
                                                                        {
                                                                            Substrate.TileEntities.TileEntitySign Entidad = new Substrate.TileEntities.TileEntitySign();
                                                                            Bloque.SetTileEntity(Entidad);
                                                                        }*/
                                                                        /*else if (ID == 84) // TileEntityRecordPlayer
                                                                        {
                                                                            Substrate.TileEntities.TileEntityRecordPlayer Entidad = new Substrate.TileEntities.TileEntityRecordPlayer();
                                                                            Bloque.SetTileEntity(Entidad);
                                                                        }*/
                                                                        else if (ID == 116) // TileEntityEnchantmentTable
                                                                        {
                                                                            Substrate.TileEntities.TileEntityEnchantmentTable Entidad = new Substrate.TileEntities.TileEntityEnchantmentTable();
                                                                            Bloque.SetTileEntity(Entidad);
                                                                        }
                                                                        else if (ID == 117) // TileEntityBrewingStand
                                                                        {
                                                                            Substrate.TileEntities.TileEntityBrewingStand Entidad = new Substrate.TileEntities.TileEntityBrewingStand();
                                                                            Bloque.SetTileEntity(Entidad);
                                                                        }
                                                                        else if (ID == 119) // TileEntityEndPortal
                                                                        {
                                                                            Substrate.TileEntities.TileEntityEndPortal Entidad = new Substrate.TileEntities.TileEntityEndPortal();
                                                                            Bloque.SetTileEntity(Entidad);
                                                                        }
                                                                        else if (ID == 137) // TileEntityControl
                                                                        {
                                                                            Substrate.TileEntities.TileEntityControl Entidad = new Substrate.TileEntities.TileEntityControl();
                                                                            Bloque.SetTileEntity(Entidad);
                                                                        }
                                                                        else if (ID == 138) // TileEntityBeacon
                                                                        {
                                                                            Substrate.TileEntities.TileEntityBeacon Entidad = new Substrate.TileEntities.TileEntityBeacon();
                                                                            Bloque.SetTileEntity(Entidad);
                                                                        }
                                                                        try
                                                                        {
                                                                            Chunk.Blocks.SetBlock(Índice_X, !Variable_Mundo_Invertido ? Índice_Y : 255 - Índice_Y, Índice_Z, Bloque);
                                                                        }
                                                                        catch (Exception Excepción)
                                                                        {
                                                                            Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                                                                            try { Chunk.Blocks.SetBlock(Índice_X, !Variable_Mundo_Invertido ? Índice_Y : 255 - Índice_Y, Índice_Z, new AlphaBlock(0, 0)); } // Air.
                                                                            catch { }
                                                                        }
                                                                        Bloques_Convertidos++;
                                                                        if (Índice_ID % 2 != 0) Índice_Data++;
                                                                    }
                                                                }
                                                            }
                                                            Chunk.Blocks.RebuildHeightMap(); // Automatic height map.
                                                            if (Variable_Luz == 16) Chunk.Blocks.RebuildBlockLight(); // Automatic block light.
                                                            Chunk.Blocks.RebuildSkyLight(); // Automatic sky light.
                                                            Matriz_IDs = null;
                                                            Matriz_Data = null;
                                                        }
                                                    }
                                                    //else MessageBox.Show("Fallo al descodificar...");
                                                }
                                            }
                                            /*else // A chunk is null on a region, add an empty (air) chunk instead? // NOT TESTED YET!
                                            {
                                                ChunkRef Chunk = Chunks.CreateChunk(Chunk_X_Global, Chunk_Z_Global);
                                                Chunk.IsLightPopulated = true; // For 1.13+ conversion support.
                                                Chunk.IsTerrainPopulated = true;
                                                Chunk.Blocks.AutoLight = false;
                                                Chunk.Blocks.RebuildHeightMap(); // Automatic height map.
                                                Chunk.Blocks.RebuildBlockLight(); // Automatic block light.
                                                Chunk.Blocks.RebuildSkyLight(); // Automatic sky light.
                                                // Hopefully this will avoid Minecraft hunging up if some chunk was missing.
                                                // Note: this will leave huge gaps between region borders if the chunks are null.
                                            }*/
                                        }
                                    }
                                    Archivo_Región = null;
                                    Progreso_Región = 0;
                                    Progreso_Total++;
                                }
                                else // This should never happen.
                                {
                                    Progreso_Región = 0;
                                    Progreso_Total++;
                                }
                            }
                            else // This should never happen.
                            {
                                Progreso_Región = 0;
                                Progreso_Total++;
                            }
                            this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Región, "Region progress: " + Program.Traducir_Número_Decimales_Redondear(((double)Progreso_Región * 100d) / 1024d, 4) + " % (" + Program.Traducir_Número(Progreso_Región) + " of 1.024 chunks)" });
                            this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Total, "Total progress: " + Program.Traducir_Número_Decimales_Redondear(Total_Regiones != 0 ? ((((double)Progreso_Total + (Progreso_Región / 1024d)) * 100d) / (double)Total_Regiones) : 0d, 4) + " % (" + Program.Traducir_Número(Progreso_Total) + " of " + Program.Traducir_Número(Total_Regiones) + (Total_Regiones != 1L ? " regions)" : " region)") });
                            this.Invoke(new Invocación.Delegado_ProgressBar_Value(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Chunk, Progreso_Región });
                            this.Invoke(new Invocación.Delegado_ProgressBar_Value(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Región, Progreso_Total });
                            Chunks.Save(); // Save the chunks of the new region to save RAM memory.
                            GC.Collect(); // Recover RAM memory after every region file.
                            GC.GetTotalMemory(true);
                        }
                        Chunks.Save();
                        Chunks = null;
                        Bloques = null;
                    }
                }
                Mundo.Save();
                Mundo = null;
                Progreso_Región = 1024; // Force all the progress bars to it's maximum values.
                Progreso_Total = Total_Regiones;
                this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Región, "Region progress: " + Program.Traducir_Número_Decimales_Redondear(((double)Progreso_Región * 100d) / 1024d, 4) + " % (" + Program.Traducir_Número(Progreso_Región) + " of 1.024 chunks)" });
                this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Total, "Total progress: " + Program.Traducir_Número_Decimales_Redondear(Total_Regiones != 0 ? (((double)Progreso_Total * 100d) / (double)Total_Regiones) : 0d, 4) + " % (" + Program.Traducir_Número(Progreso_Total) + " of " + Program.Traducir_Número(Total_Regiones) + (Total_Regiones != 1L ? " regions)" : " region)") });
                this.Invoke(new Invocación.Delegado_ProgressBar_Value(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Chunk, Barra_Progreso_Chunk.Maximum });
                this.Invoke(new Invocación.Delegado_ProgressBar_Value(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Región, Barra_Progreso_Región.Maximum });
                //this.Activate(); // This need to be invoked in a thread like the rest.
                //double Chunks_Convertidos = Bloques_Convertidos / 65536d; // 1 chunk in blocks.
                double Regiones_Convertidas = Bloques_Convertidos / 67108864d; // 1 region in blocks.
                if ((DialogResult)this.Invoke(new Invocación.Delegado_IWin32Window_MessageBox(Invocación.Ejecutar_Delegado_IWin32Window_MessageBox), new object[] { this, "The new world has been successfully generated. You'll find it here:\r\n\r\n\"" + Ruta + "\".\r\n\r\nBlocks converted: " + Program.Traducir_Número(Bloques_Convertidos) + (Bloques_Convertidos != 1L ? " blocks" : " block") + " (" + Program.Traducir_Número_Decimales_Redondear(Regiones_Convertidas, 4) + (Regiones_Convertidas != 1d ? " regions" : " region") + ").\r\n\r\nAverage time per chunk (65.536 blocks): " + Program.Traducir_Intervalo_Horas_Minutos_Segundos(TimeSpan.FromTicks(Bloques_Convertidos != 0 ? ((Cronómetro_Total.Elapsed.Ticks * 65536L) / Bloques_Convertidos) : 0L)) + ".\r\nAverage time per region (67.108.864 blocks): " + Program.Traducir_Intervalo_Horas_Minutos_Segundos(TimeSpan.FromTicks(Bloques_Convertidos != 0 ? ((Cronómetro_Total.Elapsed.Ticks * 67108864L) / Bloques_Convertidos) : 0L)) + ".\r\n\r\nYou'll probably see it at the bottom of your Minecraft world list.\r\nDo you want to open now the folder with your converted world?", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, Bloques_Convertidos > 0L ? MessageBoxIcon.Information : MessageBoxIcon.Warning }) == DialogResult.Yes)
                {
                    Program.Ejecutar_Ruta(Ruta, ProcessWindowStyle.Maximized);
                }
                //Lista_Propiedades_Ejemplos.Sort(); // Used to extract all the properties from a debug world.
                //File.WriteAllLines(Program.Obtener_Ruta_Temporal_Escritorio() + ".txt", Lista_Propiedades_Ejemplos.ToArray(), Encoding.Unicode);
                if (Diccionario_Bloques_Desconocidos.Count > 0) // Warn of possible future unknown blocks.
                {
                    foreach (KeyValuePair<string, List<string>> Entrada in Diccionario_Bloques_Desconocidos)
                    {
                        Texto_Bloques_Desconocidos += Entrada.Key + "\r\n{\r\n";
                        if (Entrada.Value != null && Entrada.Value.Count > 0)
                        {
                            if (Entrada.Value.Count > 1) Diccionario_Bloques_Desconocidos[Entrada.Key].Sort(new Comparador_String());
                            foreach (string Propiedad in Entrada.Value)
                            {
                                Texto_Bloques_Desconocidos += "    " + Propiedad + "\r\n";
                            }
                        }
                        else Texto_Bloques_Desconocidos += "    \r\n";
                        Texto_Bloques_Desconocidos += "}\r\n";
                    }
                    Texto_Bloques_Desconocidos += "\r\n\r\n";
                    // Add support even to help with the implementation of the new block types.
                    if (string.Compare(Environment.UserName, "Jupisoft", true) == 0)
                    {
                        // Support for the 1.13+ to 1.12.2- block converter.
                        foreach (KeyValuePair<string, List<string>> Entrada in Diccionario_Bloques_Desconocidos)
                        {
                            Texto_Bloques_Desconocidos += "Diccionario_1_13_a_1_12_2.Add(\"" + Entrada.Key + "\", \"\");\r\n";
                        }
                        Texto_Bloques_Desconocidos += "\r\n\r\n";
                        // Support for the knowledge of the new block types.
                        foreach (KeyValuePair<string, List<string>> Entrada in Diccionario_Bloques_Desconocidos)
                        {
                            Texto_Bloques_Desconocidos += "new Bloques(\"" + Entrada.Key + "\", null, null, Color.FromArgb(), false, Obtenciones.Survival),\r\n";
                        }
                    }
                    Texto_Bloques_Desconocidos = Texto_Bloques_Desconocidos.TrimEnd("\r\n".ToCharArray());
                    this.Invoke(new Invocación.Delegado_IWin32Window_MessageBox(Invocación.Ejecutar_Delegado_IWin32Window_MessageBox), new object[] { this, "Unknown blocks found replaced by air: " + Program.Traducir_Número(Diccionario_Bloques_Desconocidos.Count) + ".\r\nThe unknown block list will be copied to the clipboard.\r\n\r\n" + Texto_Bloques_Desconocidos, Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning });
                    Texto_Portapapeles = Texto_Bloques_Desconocidos;
                    Diccionario_Bloques_Desconocidos.Clear();
                    Texto_Bloques_Desconocidos = null;
                }
                if (Diccionario_Bloques_Obsoletos.Count > 0 && string.Compare(Environment.UserName, "Jupisoft", true) == 0) // Warn of possible obsolete or unsupported blocks.
                {
                    int Bloques_Obsoletos = 0;
                    foreach (Minecraft.Bloques Bloque in Minecraft.Bloques.Matriz_Bloques)
                    {
                        if (!Bloque.Obsoleto && !Diccionario_Bloques_Obsoletos.ContainsKey(Bloque.Nombre_1_13))
                        {
                            Texto_Bloques_Obsoletos += Bloque.Nombre_1_13 + "\r\n";
                            Bloques_Obsoletos++;
                        }
                    }
                    if (Bloques_Obsoletos > 0)
                    {
                        Texto_Bloques_Obsoletos = Texto_Bloques_Obsoletos.TrimEnd("\r\n".ToCharArray());
                        this.Invoke(new Invocación.Delegado_IWin32Window_MessageBox(Invocación.Ejecutar_Delegado_IWin32Window_MessageBox), new object[] { this, "Obsolete or unused blocks found: " + Program.Traducir_Número(Bloques_Obsoletos) + ".\r\nThe obsolete block list will be copied to the clipboard.\r\n\r\n" + Texto_Bloques_Obsoletos, Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning });
                        Texto_Portapapeles = Texto_Bloques_Obsoletos;
                    }
                    Diccionario_Bloques_Obsoletos.Clear();
                    Texto_Bloques_Obsoletos = null;
                }
                if (Diccionario_Bloques_Únicos.Count > 0 && string.Compare(Environment.UserName, "Jupisoft", true) == 0)
                {
                    string Texto_Bloques_Únicos = null;
                    foreach (KeyValuePair<string, List<string>> Entrada in Diccionario_Bloques_Únicos)
                    {
                        Texto_Bloques_Únicos += "Diccionario_Bloques.Add(\"" + Entrada.Key + "\", new List<string>(new string[] { ";
                        if (Entrada.Value != null && Entrada.Value.Count > 0)
                        {
                            if (Entrada.Value.Count > 1) Diccionario_Bloques_Únicos[Entrada.Key].Sort(new Comparador_String());
                            foreach (string Propiedad in Entrada.Value)
                            {
                                Texto_Bloques_Únicos += "\"" + Propiedad + "\", ";
                            }
                            Texto_Bloques_Únicos = Texto_Bloques_Únicos.TrimEnd(", ".ToCharArray()) + " ";
                        }
                        Texto_Bloques_Únicos += "}));\r\n";
                    }
                    this.Invoke(new Invocación.Delegado_IWin32Window_MessageBox(Invocación.Ejecutar_Delegado_IWin32Window_MessageBox), new object[] { this, "Unique blocks with it's found properties: " + Program.Traducir_Número(Diccionario_Bloques_Únicos.Count) + ".\r\nThe unique block list will be copied to the clipboard.\r\n\r\n" + Texto_Bloques_Únicos, Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning });
                    Texto_Portapapeles = Texto_Bloques_Únicos;
                    Diccionario_Bloques_Únicos.Clear();
                    Texto_Bloques_Únicos = null;
                }
                Cronómetro_Total.Reset();
                Cronómetro_Total = null;
            }
            catch (ThreadAbortException) { Subproceso_Abortado = true; } // Aborted, ignore this exception.
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally
            {
                try
                {
                    AnvilChunk.Biomes_Jupisoft = null; // Always reset the temporary biome array.
                    Pendiente_Subproceso_Abortar = false;
                    Subproceso_Activo = false;
                    Subproceso = null;
                    GC.Collect(); // Recover RAM memory at the end.
                    GC.GetTotalMemory(true);
                    if (!Subproceso_Abortado)
                    {
                        this.Invoke(new Invocación.Delegado_Control_Cursor(Invocación.Ejecutar_Delegado_Control_Cursor), new object[] { this, Cursors.Default });
                        // Reset all the progress bars.
                        this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [The original world files will never be modified]" });
                        this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Región, "Region progress: 0,0000 % (0 of 1.024 chunks)" });
                        this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Total, "Total progress: 0,0000 % (0 of 0 regions)" });
                        this.Invoke(new Invocación.Delegado_ProgressBar_Value(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Chunk, 0 });
                        this.Invoke(new Invocación.Delegado_ProgressBar_Value(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Región, 0 });
                        this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { Grupo_Ajustes, true });
                        this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { Tabla_Bloques, true });
                        this.Invoke(new Invocación.Delegado_ContextMenuStrip_Enabled(Invocación.Ejecutar_Delegado_ContextMenuStrip_Enabled), new object[] { Menú_Contextual, true });
                        this.Invoke(new Invocación.Delegado_Control_Select(Invocación.Ejecutar_Delegado_Control_Select), new object[] { Botón_Convertir });
                        this.Invoke(new Invocación.Delegado_Control_Focus(Invocación.Ejecutar_Delegado_Control_Focus), new object[] { Botón_Convertir });
                    }
                    else this.Invoke(new Invocación.Delegado_Form_Close(Invocación.Ejecutar_Delegado_Form_Close), new object[] { this }); // Close the window.
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            }
        }

        /// <summary>
        /// Tries to add, change and remove some block transmutations from the dictionary.
        /// </summary>
        /// <param name="Matriz_Transmutaciones">Array with all the new transmutations to add, change or remove from the dictionary. When any value from a transmutation is null or empty, it will try to be removed from the dictionary.</param>
        internal void Agregar_Transmutaciones(KeyValuePair<string, string>[] Matriz_Transmutaciones)
        {
            try
            {
                if (Matriz_Transmutaciones != null && Matriz_Transmutaciones.Length > 0)
                {
                    // Try to remember the selected transmutation or it's index (if there is one selected).
                    int Índice_Selección = 0;
                    string Selección = null;
                    if (DataGridView_Transmutación.Rows != null && DataGridView_Transmutación.Rows.Count > 0 && DataGridView_Transmutación.SelectedRows != null && DataGridView_Transmutación.SelectedRows.Count > 0)
                    {
                        Índice_Selección = DataGridView_Transmutación.SelectedRows[0].Index;
                        Selección = DataGridView_Transmutación.SelectedRows[0].Cells[Columna_Entrada.Index].Value.ToString();
                    }

                    // Add, edit or remove some transmutations from the dictionary and the list in the window.
                    DataGridView_Transmutación.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    DataGridView_Transmutación.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                    foreach (KeyValuePair<string, string> Entrada in Matriz_Transmutaciones)
                    {
                        if (!string.IsNullOrEmpty(Entrada.Key))
                        {
                            if (!string.IsNullOrEmpty(Entrada.Value)) // Add or change a transmutation.
                            {
                                if (!Diccionario_Transmutación.ContainsKey(Entrada.Key)) // Add a transmutation.
                                {
                                    Diccionario_Transmutación.Add(Entrada.Key, Entrada.Value);
                                    DataGridView_Transmutación.Rows.Add(new object[] { Program.Obtener_Imagen_Recursos(Entrada.Key), Entrada.Key, Program.Obtener_Imagen_Recursos(Entrada.Value), Entrada.Value });
                                }
                                else // Change a transmutation.
                                {
                                    Diccionario_Transmutación[Entrada.Key] = Entrada.Value;
                                    if (DataGridView_Transmutación.Rows != null && DataGridView_Transmutación.Rows.Count > 0)
                                    {
                                        for (int Índice_Fila = DataGridView_Transmutación.Rows.Count - 1; Índice_Fila >= 0; Índice_Fila--)
                                        {
                                            if (string.Compare(DataGridView_Transmutación.Rows[Índice_Fila].Cells[Columna_Entrada.Index].Value.ToString(), Entrada.Key, true) == 0)
                                            {
                                                DataGridView_Transmutación.Rows[Índice_Fila].Cells[Columna_Imagen_Salida.Index].Value = Program.Obtener_Imagen_Recursos(Entrada.Value);
                                                DataGridView_Transmutación.Rows[Índice_Fila].Cells[Columna_Salida.Index].Value = Entrada.Value;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            else // Remove a transmutation.
                            {
                                if (Diccionario_Transmutación.ContainsKey(Entrada.Key))
                                {
                                    Diccionario_Transmutación.Remove(Entrada.Key);
                                    if (DataGridView_Transmutación.Rows != null && DataGridView_Transmutación.Rows.Count > 0)
                                    {
                                        for (int Índice_Fila = DataGridView_Transmutación.Rows.Count - 1; Índice_Fila >= 0; Índice_Fila--)
                                        {
                                            if (string.Compare(DataGridView_Transmutación.Rows[Índice_Fila].Cells[Columna_Entrada.Index].Value.ToString(), Entrada.Key, true) == 0)
                                            {
                                                DataGridView_Transmutación.Rows.RemoveAt(Índice_Fila);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    DataGridView_Transmutación.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    DataGridView_Transmutación.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                    // Make sure the list is properly ordered.
                    DataGridView_Transmutación.Sort(DataGridView_Transmutación.SortedColumn != Columna_Salida ? Columna_Entrada : Columna_Salida, DataGridView_Transmutación.SortOrder != SortOrder.Descending ? ListSortDirection.Ascending : ListSortDirection.Descending);

                    // Update the current number of transmutations.
                    Grupo_Transmutación.Text = "Block transmutation settings - [turns one block type into another, " + Program.Traducir_Número(Diccionario_Transmutación.Count) + (Diccionario_Transmutación.Count != 1 ? " transmutations]" : " transmutation]");

                    // Try to set back the selected transmutation or it's index (if there was one selected).
                    if (DataGridView_Transmutación.Rows != null && DataGridView_Transmutación.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(Selección))
                        {
                            foreach (DataGridViewRow Fila in DataGridView_Transmutación.Rows)
                            {
                                if (string.Compare(Fila.Cells[Columna_Entrada.Index].Value.ToString(), Selección, true) == 0)
                                {
                                    DataGridView_Transmutación.CurrentCell = Fila.Cells[0];
                                    break;
                                }
                            }
                        }
                        else if (Índice_Selección > -1)
                        {
                            if (Índice_Selección >= DataGridView_Transmutación.Rows.Count) Índice_Selección = DataGridView_Transmutación.Rows.Count - 1;
                            DataGridView_Transmutación.CurrentCell = DataGridView_Transmutación.Rows[Índice_Selección].Cells[0];
                        }
                    }

                    Grupo_Transmutación.Invalidate();
                    Grupo_Transmutación.Update();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Tries to add or remove some block quantizations from the dictionary.
        /// </summary>
        /// <param name="Matriz_Cuantizaciones">Array with all the new quantizations to add, change or remove from the dictionary. When any value from a quantization is null or empty, it will try to be removed from the dictionary.</param>
        internal void Agregar_Cuantizaciones(KeyValuePair<string, string>[] Matriz_Cuantizaciones)
        {
            try
            {
                if (Matriz_Cuantizaciones != null && Matriz_Cuantizaciones.Length > 0)
                {
                    // Try to remember the selected quantization or it's index (if there is one selected).
                    int Índice_Selección = 0;
                    string Selección = null;
                    if (DataGridView_Cuantización.Rows != null && DataGridView_Cuantización.Rows.Count > 0 && DataGridView_Cuantización.SelectedRows != null && DataGridView_Cuantización.SelectedRows.Count > 0)
                    {
                        Índice_Selección = DataGridView_Cuantización.SelectedRows[0].Index;
                        Selección = DataGridView_Cuantización.SelectedRows[0].Cells[Columna_Bloques.Index].Value.ToString();
                    }

                    // Add or remove some quantizations from the dictionary and the list in the window.
                    DataGridView_Cuantización.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    DataGridView_Cuantización.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                    foreach (KeyValuePair<string, string> Entrada in Matriz_Cuantizaciones)
                    {
                        if (!string.IsNullOrEmpty(Entrada.Key))
                        {
                            if (!string.IsNullOrEmpty(Entrada.Value)) // Add or change a quantization.
                            {
                                if (!Diccionario_Cuantización.ContainsKey(Entrada.Key)) // Add a quantization.
                                {
                                    Diccionario_Cuantización.Add(Entrada.Key, null);
                                    DataGridView_Cuantización.Rows.Add(new object[] { Program.Obtener_Imagen_Recursos(Entrada.Key), Entrada.Key });
                                }
                            }
                            else // Remove a quantization.
                            {
                                if (Diccionario_Cuantización.ContainsKey(Entrada.Key))
                                {
                                    Diccionario_Cuantización.Remove(Entrada.Key);
                                    if (DataGridView_Cuantización.Rows != null && DataGridView_Cuantización.Rows.Count > 0)
                                    {
                                        for (int Índice_Fila = DataGridView_Cuantización.Rows.Count - 1; Índice_Fila >= 0; Índice_Fila--)
                                        {
                                            if (string.Compare(DataGridView_Cuantización.Rows[Índice_Fila].Cells[Columna_Bloques.Index].Value.ToString(), Entrada.Key, true) == 0)
                                            {
                                                DataGridView_Cuantización.Rows.RemoveAt(Índice_Fila);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    DataGridView_Cuantización.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    DataGridView_Cuantización.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                    // Make sure the list is properly ordered.
                    DataGridView_Cuantización.Sort(Columna_Bloques, DataGridView_Cuantización.SortOrder != SortOrder.Descending ? ListSortDirection.Ascending : ListSortDirection.Descending);

                    // Update the current number of quantizations.
                    Grupo_Cuantización.Text = "Block quantization settings - [reduces the block types to the selected ones, " + Program.Traducir_Número(Diccionario_Cuantización.Count) + (Diccionario_Cuantización.Count != 1 ? " quantizations]" : " quantization]");

                    // Try to set back the selected quantization or it's index (if there was one selected).
                    if (DataGridView_Cuantización.Rows != null && DataGridView_Cuantización.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(Selección))
                        {
                            foreach (DataGridViewRow Fila in DataGridView_Cuantización.Rows)
                            {
                                if (string.Compare(Fila.Cells[Columna_Bloques.Index].Value.ToString(), Selección, true) == 0)
                                {
                                    DataGridView_Cuantización.CurrentCell = Fila.Cells[0];
                                    break;
                                }
                            }
                        }
                        else if (Índice_Selección > -1)
                        {
                            if (Índice_Selección >= DataGridView_Cuantización.Rows.Count) Índice_Selección = DataGridView_Cuantización.Rows.Count - 1;
                            DataGridView_Cuantización.CurrentCell = DataGridView_Cuantización.Rows[Índice_Selección].Cells[0];
                        }
                    }

                    Grupo_Cuantización.Invalidate();
                    Grupo_Cuantización.Update();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Reemplazar_Lecho_Roca_Aire_Click(object sender, EventArgs e)
        {
            try
            {
                Agregar_Transmutaciones(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("minecraft:bedrock", "minecraft:air")
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Reemplazar_Minerales_Aire_Click(object sender, EventArgs e)
        {
            try
            {
                Agregar_Transmutaciones(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("minecraft:coal_ore", "minecraft:air"),
                    new KeyValuePair<string, string>("minecraft:diamond_ore", "minecraft:air"),
                    new KeyValuePair<string, string>("minecraft:emerald_ore", "minecraft:air"),
                    new KeyValuePair<string, string>("minecraft:gold_ore", "minecraft:air"),
                    new KeyValuePair<string, string>("minecraft:iron_ore", "minecraft:air"),
                    new KeyValuePair<string, string>("minecraft:lapis_ore", "minecraft:air"),
                    new KeyValuePair<string, string>("minecraft:nether_quartz_ore", "minecraft:air"),
                    new KeyValuePair<string, string>("minecraft:redstone_ore", "minecraft:air")
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Reemplazar_Piedras_Aire_Click(object sender, EventArgs e)
        {
            try
            {
                Agregar_Transmutaciones(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("minecraft:stone", "minecraft:air"),
                    //new KeyValuePair<string, string>("minecraft:cobblestone", "minecraft:air"),
                    new KeyValuePair<string, string>("minecraft:andesite", "minecraft:air"),
                    new KeyValuePair<string, string>("minecraft:diorite", "minecraft:air"),
                    new KeyValuePair<string, string>("minecraft:granite", "minecraft:air"),
                    //new KeyValuePair<string, string>("minecraft:polished_andesite", "minecraft:air"),
                    //new KeyValuePair<string, string>("minecraft:polished_diorite", "minecraft:air"),
                    //new KeyValuePair<string, string>("minecraft:polished_granite", "minecraft:air"),
                    new KeyValuePair<string, string>("minecraft:sandstone", "minecraft:air"),
                    //new KeyValuePair<string, string>("minecraft:chiseled_sandstone", "minecraft:air"),
                    //new KeyValuePair<string, string>("minecraft:cut_sandstone", "minecraft:air"),
                    //new KeyValuePair<string, string>("minecraft:smooth_sandstone", "minecraft:air"),
                    new KeyValuePair<string, string>("minecraft:red_sandstone", "minecraft:air"),
                    //new KeyValuePair<string, string>("minecraft:chiseled_red_sandstone", "minecraft:air"),
                    //new KeyValuePair<string, string>("minecraft:cut_red_sandstone", "minecraft:air"),
                    //new KeyValuePair<string, string>("minecraft:smooth_red_sandstone", "minecraft:air"),
                    //new KeyValuePair<string, string>("minecraft:prismarine", "minecraft:air"),
                    //new KeyValuePair<string, string>("minecraft:prismarine_bricks", "minecraft:air"),
                    //new KeyValuePair<string, string>("minecraft:dark_prismarine", "minecraft:air")
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Reemplazar_Tierra_Aire_Click(object sender, EventArgs e)
        {
            try
            {
                Agregar_Transmutaciones(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("minecraft:dirt", "minecraft:air")
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Reemplazar_Grava_Aire_Click(object sender, EventArgs e)
        {
            try
            {
                Agregar_Transmutaciones(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("minecraft:gravel", "minecraft:air")
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Reemplazar_Arena_Aire_Click(object sender, EventArgs e)
        {
            try
            {
                Agregar_Transmutaciones(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("minecraft:sand", "minecraft:air")
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Reemplazar_Netherrack_Aire_Click(object sender, EventArgs e)
        {
            try
            {
                Agregar_Transmutaciones(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("minecraft:netherrack", "minecraft:air")
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Reemplazar_Piedra_Fin_Aire_Click(object sender, EventArgs e)
        {
            try
            {
                Agregar_Transmutaciones(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("minecraft:end_stone", "minecraft:air")
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Reemplazar_Agua_Aire_Click(object sender, EventArgs e)
        {
            try
            {
                Agregar_Transmutaciones(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("minecraft:water", "minecraft:air"),
                    new KeyValuePair<string, string>("minecraft:flowing_water", "minecraft:air")
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Reemplazar_Lava_Aire_Click(object sender, EventArgs e)
        {
            try
            {
                Agregar_Transmutaciones(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("minecraft:lava", "minecraft:air"),
                    new KeyValuePair<string, string>("minecraft:flowing_lava", "minecraft:air")
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Reemplazar_Agua_Lava_Click(object sender, EventArgs e)
        {
            try
            {
                Agregar_Transmutaciones(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("minecraft:water", "minecraft:lava"),
                    new KeyValuePair<string, string>("minecraft:flowing_water", "minecraft:flowing_lava")
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Reemplazar_Lava_Agua_Click(object sender, EventArgs e)
        {
            try
            {
                Agregar_Transmutaciones(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("minecraft:lava", "minecraft:water"),
                    new KeyValuePair<string, string>("minecraft:flowing_lava", "minecraft:flowing_water")
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Forzar_Mundo_Agua_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Mundo_Agua = Menú_Contextual_Forzar_Mundo_Agua.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Forzar_Mundo_Lava_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Mundo_Lava = Menú_Contextual_Forzar_Mundo_Lava.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void DataGridView_Transmutación_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
                Depurador.Escribir_Excepción(e.Exception != null ? e.Exception.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Luz_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Luz = ComboBox_Luz.SelectedIndex;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void DataGridView_Transmutación_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Right)
                {
                    DataGridView.HitTestInfo Info = DataGridView_Transmutación.HitTest(e.X, e.Y);
                    if (Info.Type == DataGridViewHitTestType.None)
                    {
                        DataGridView_Transmutación.ClearSelection();
                        DataGridView_Transmutación.CurrentCell = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Transmutación_Restablecer_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridView_Transmutación.Rows != null && DataGridView_Transmutación.Rows.Count > 0)
                {
                    if (MessageBox.Show(this, "Do you want to delete all the transmutations in the list?\r\nThis operation can't be undone later.", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        List<KeyValuePair<string, string>> Lista_Transmutaciones = new List<KeyValuePair<string, string>>();
                        foreach (DataGridViewRow Fila in DataGridView_Transmutación.Rows)
                        {
                            Lista_Transmutaciones.Add(new KeyValuePair<string, string>(Fila.Cells[Columna_Entrada.Index].Value.ToString(), null));
                        }
                        Agregar_Transmutaciones(Lista_Transmutaciones.ToArray());
                    }
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Transmutación_Aleatorizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(this, "Do you want to randomize all the transmutations?\r\nThis operation can't be undone later.", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (DataGridView_Transmutación.Rows != null && DataGridView_Transmutación.Rows.Count > 0)
                    {
                        List<KeyValuePair<string, string>> Lista_Transmutaciones_Quitar = new List<KeyValuePair<string, string>>();
                        foreach (DataGridViewRow Fila in DataGridView_Transmutación.Rows)
                        {
                            Lista_Transmutaciones_Quitar.Add(new KeyValuePair<string, string>(Fila.Cells[Columna_Entrada.Index].Value.ToString(), null));
                        }
                        Agregar_Transmutaciones(Lista_Transmutaciones_Quitar.ToArray());
                    } // Remove all.

                    // Add the name of all the known blocks inside a list.
                    List<string> Lista_Nombres_Válidos = new List<string>();
                    List<string> Lista_Nombres_Temporal = new List<string>();
                    List<string> Lista_Nombres_Entrada = new List<string>();
                    List<string> Lista_Nombres_Salida = new List<string>();

                    for (int Índice = 0; Índice < Minecraft.Bloques.Matriz_Bloques.Length; Índice++)
                    {
                        // Ignore blocks from old snapshots, partial size blocks, air, water and lava.
                        if (!Minecraft.Bloques.Matriz_Bloques[Índice].Obsoleto &&
                            !Minecraft.Bloques.Matriz_Bloques[Índice].Altura_Diferente &&
                            string.Compare(Minecraft.Bloques.Matriz_Bloques[Índice].Nombre_1_13, "minecraft:air", true) != 0 &&
                            string.Compare(Minecraft.Bloques.Matriz_Bloques[Índice].Nombre_1_13, "minecraft:water", true) != 0 &&
                            string.Compare(Minecraft.Bloques.Matriz_Bloques[Índice].Nombre_1_13, "minecraft:lava", true) != 0 &&
                            string.Compare(Minecraft.Bloques.Matriz_Bloques[Índice].Nombre_1_13, "minecraft:flowing_water", true) != 0 &&
                            string.Compare(Minecraft.Bloques.Matriz_Bloques[Índice].Nombre_1_13, "minecraft:flowing_lava", true) != 0)
                        {
                            Lista_Nombres_Válidos.Add(Minecraft.Bloques.Matriz_Bloques[Índice].Nombre_1_13);
                        }
                    }

                    Lista_Nombres_Temporal.AddRange(Lista_Nombres_Válidos.GetRange(0, Lista_Nombres_Válidos.Count));
                    for (int Índice = Lista_Nombres_Temporal.Count - 1; Índice >= 0; Índice--)
                    {
                        int Índice_Aleatorio = Program.Rand.Next(0, Lista_Nombres_Temporal.Count);
                        Lista_Nombres_Entrada.Add(Lista_Nombres_Temporal[Índice_Aleatorio]);
                        Lista_Nombres_Temporal.RemoveAt(Índice_Aleatorio);
                    }
                    Lista_Nombres_Temporal.Clear(); // Just in case.

                    Lista_Nombres_Temporal.AddRange(Lista_Nombres_Válidos.GetRange(0, Lista_Nombres_Válidos.Count));
                    for (int Índice = Lista_Nombres_Temporal.Count - 1; Índice >= 0; Índice--)
                    {
                        int Índice_Aleatorio = Program.Rand.Next(0, Lista_Nombres_Temporal.Count);
                        Lista_Nombres_Salida.Add(Lista_Nombres_Temporal[Índice_Aleatorio]);
                        Lista_Nombres_Temporal.RemoveAt(Índice_Aleatorio);
                    }
                    Lista_Nombres_Temporal = null;

                    List<KeyValuePair<string, string>> Lista_Transmutaciones = new List<KeyValuePair<string, string>>();
                    for (int Índice = 0; Índice < Lista_Nombres_Válidos.Count; Índice++)
                    {
                        Lista_Transmutaciones.Add(new KeyValuePair<string, string>(Lista_Nombres_Entrada[Índice], Lista_Nombres_Salida[Índice]));
                    }
                    Agregar_Transmutaciones(Lista_Transmutaciones.ToArray());

                    Lista_Transmutaciones = null;
                    Lista_Nombres_Entrada = null;
                    Lista_Nombres_Salida = null;
                    Lista_Nombres_Válidos = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Transmutación_Quitar_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridView_Transmutación.SelectedRows != null && DataGridView_Transmutación.SelectedRows.Count > 0)
                {
                    List<KeyValuePair<string, string>> Lista_Transmutaciones = new List<KeyValuePair<string, string>>();
                    foreach (DataGridViewRow Fila in DataGridView_Transmutación.SelectedRows)
                    {
                        Lista_Transmutaciones.Add(new KeyValuePair<string, string>(Fila.Cells[Columna_Entrada.Index].Value.ToString(), null));
                    }
                    Agregar_Transmutaciones(Lista_Transmutaciones.ToArray());
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Transmutación_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Transmutación_Cuantización Ventana = new Ventana_Transmutación_Cuantización();
                Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                Ventana.Diccionario_Transmutación_Cuantización = Diccionario_Transmutación;
                Ventana.Cuantización = false;
                if (Ventana.ShowDialog(this) == DialogResult.OK)
                {
                    Agregar_Transmutaciones(new KeyValuePair<string, string>[] { Ventana.Transmutación_Cuantización });
                }
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Reiniciar_Diccionario_1_13_a_1_12_2()
        {
            try
            {
                /*string pp = null;
                foreach (Minecraft.Bloques Bloque in Minecraft.Bloques.Matriz_Bloques)
                {
                    if (Bloque.Lista_ID == null || Bloque.Lista_ID.Count <= 0)
                    {
                        pp += "Diccionario_1_13_a_1_12_2.Add(\"" + Bloque.Nombre_1_13 + "\", \"\");\r\n";
                    }
                }
                Clipboard.SetText(pp);
                return;*/

                if (Diccionario_1_13_a_1_12_2 == null || Diccionario_1_13_a_1_12_2.Count <= 0)
                {
                    /**/

                    Diccionario_1_13_a_1_12_2 = new SortedDictionary<string, string>();
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_bark", "");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_button", "minecraft:oak_button");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_pressure_plate", "minecraft:oak_pressure_plate");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_trapdoor", "minecraft:oak_trapdoor");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_wood", "minecraft:acacia_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:attached_melon_stem", "minecraft:melon_stem");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:attached_pumpkin_stem", "minecraft:pumpkin_stem");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:banner", "minecraft:white_banner");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:birch_bark", "");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:birch_button", "minecraft:oak_button");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:birch_pressure_plate", "minecraft:oak_pressure_plate");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:birch_trapdoor", "minecraft:oak_trapdoor");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:birch_wood", "minecraft:birch_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:black_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:blue_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:blue_coral", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:blue_coral_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:blue_coral_plant", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:blue_dead_coral", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:blue_ice", "minecraft:light_blue_concrete");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:brain_coral", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:brain_coral_block", "minecraft:pink_glazed_terracotta");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:brain_coral_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:brain_coral_wall_fan", "minecraft:water");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:brown_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bubble_column", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bubble_coral", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bubble_coral_block", "minecraft:magenta_glazed_terracotta");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bubble_coral_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bubble_coral_wall_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:cave_air", "minecraft:air");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:conduit", "minecraft:beacon");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:creeper_wall_head", "minecraft:creeper_head");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:cyan_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_bark", "");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_button", "minecraft:oak_button");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_pressure_plate", "minecraft:oak_pressure_plate");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_trapdoor", "minecraft:oak_trapdoor");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_wood", "minecraft:dark_oak_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_prismarine_slab", "minecraft:nether_brick_slab");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_prismarine_stairs", "minecraft:nether_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_brain_coral", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_brain_coral_block", "minecraft:light_gray_glazed_terracotta");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_brain_coral_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_brain_coral_wall_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_bubble_coral", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_bubble_coral_block", "minecraft:light_gray_glazed_terracotta");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_bubble_coral_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_bubble_coral_wall_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_fire_coral", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_fire_coral_block", "minecraft:light_gray_glazed_terracotta");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_fire_coral_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_fire_coral_wall_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_horn_coral", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_horn_coral_block", "minecraft:light_gray_glazed_terracotta");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_horn_coral_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_horn_coral_wall_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_tube_coral", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_tube_coral_block", "minecraft:light_gray_glazed_terracotta");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_tube_coral_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_tube_coral_wall_fan", "minecraft:water");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dragon_wall_head", "minecraft:dragon_head");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dried_kelp_block", "minecraft:green_concrete");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:fire_coral", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:fire_coral_block", "minecraft:red_glazed_terracotta");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:fire_coral_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:fire_coral_wall_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:four_turtle_eggs", "minecraft:air");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:gray_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:green_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:horn_coral", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:horn_coral_block", "minecraft:yellow_glazed_terracotta");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:horn_coral_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:horn_coral_wall_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:item_frame", "minecraft:air");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_bark", "");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_button", "minecraft:oak_button");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_pressure_plate", "minecraft:oak_pressure_plate");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_trapdoor", "minecraft:oak_trapdoor");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_wood", "minecraft:jungle_planks");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:kelp", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:kelp_plant", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:kelp_top", "minecraft:water");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:light_blue_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:light_gray_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:lime_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:magenta_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:melon_block", "minecraft:melon");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:mob_spawner", "minecraft:spawner");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:mushroom_stem", "minecraft:brown_mushroom_block");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:oak_bark", "");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:oak_wood", "minecraft:oak_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:orange_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:pink_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:pink_coral", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:pink_coral_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:pink_coral_plant", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:pink_dead_coral", "minecraft:water");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:player_wall_head", "minecraft:player_head");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:portal", "minecraft:nether_portal");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_brick_slab", "minecraft:stone_brick_slab");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_brick_stairs", "minecraft:stone_brick_stairs");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_bricks_slab", "minecraft:stone_brick_slab");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_bricks_stairs", "minecraft:stone_brick_stairs");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_slab", "minecraft:cobblestone_slab");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_stairs", "minecraft:cobblestone_stairs");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:pumpkin", "minecraft:carved_pumpkin");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:purple_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:purple_coral", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:purple_coral_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:purple_coral_plant", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:purple_dead_coral", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_coral", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_coral_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_coral_plant", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_dead_coral", "minecraft:water");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:redstone_wall_torch", "minecraft:redstone_torch");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:sea_grass", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:sea_pickle", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:seagrass", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:shulker_box", "minecraft:purple_shulker_box");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:skeleton_wall_skull", "minecraft:skeleton_skull");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_bark", "");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_button", "minecraft:oak_button");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_pressure_plate", "minecraft:oak_pressure_plate");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_trapdoor", "minecraft:oak_trapdoor");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_wood", "minecraft:spruce_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_acacia_log", "minecraft:acacia_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_acacia_wood", "minecraft:acacia_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_birch_log", "minecraft:birch_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_birch_wood", "minecraft:birch_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_dark_oak_log", "minecraft:dark_oak_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_dark_oak_wood", "minecraft:dark_oak_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_jungle_log", "minecraft:jungle_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_jungle_wood", "minecraft:jungle_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_oak_log", "minecraft:oak_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_oak_wood", "minecraft:oak_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_spruce_log", "minecraft:spruce_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_spruce_wood", "minecraft:spruce_planks");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:tall_sea_grass", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:tall_seagrass", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:three_turtle_eggs", "minecraft:air");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:tube_coral", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:tube_coral_block", "minecraft:blue_glazed_terracotta");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:tube_coral_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:tube_coral_wall_fan", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:turtle_egg", "minecraft:air");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:two_turtle_eggs", "minecraft:air");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:void_air", "minecraft:air");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:wall_banner", "minecraft:white_banner");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:wall_torch", "minecraft:torch");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:white_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:wither_skeleton_wall_skull", "minecraft:wither_skeleton_skull");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:yellow_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:yellow_coral", "minecraft:yellow_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:yellow_coral_fan", "minecraft:yellow_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:yellow_coral_plant", "minecraft:yellow_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:yellow_dead_coral", "minecraft:light_gray_stained_glass");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:zombie_wall_head", "minecraft:zombie_head");

                    // Add support for all the new Minecraft 1.14 (Snapshot 18w43c) new block types:
                    Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_sign", "minecraft:sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_wall_sign", "minecraft:wall_sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:andesite_slab", "minecraft:cobblestone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:andesite_stairs", "minecraft:cobblestone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:andesite_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bamboo", "minecraft:lime_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bamboo_sapling", "minecraft:green_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:birch_sign", "minecraft:sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:birch_wall_sign", "minecraft:wall_sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:brick_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:cornflower", "minecraft:blue_orchid");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_sign", "minecraft:sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_wall_sign", "minecraft:wall_sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:diorite_slab", "minecraft:cobblestone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:diorite_stairs", "minecraft:cobblestone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:diorite_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:end_stone_brick_slab", "minecraft:cobblestone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:end_stone_brick_stairs", "minecraft:cobblestone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:end_stone_brick_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:granite_slab", "minecraft:cobblestone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:granite_stairs", "minecraft:cobblestone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:granite_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_sign", "minecraft:sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_wall_sign", "minecraft:wall_sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:lily_of_the_valley", "minecraft:oxeye_daisy");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:loom", "minecraft:crafting_table");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:mossy_cobblestone_slab", "minecraft:cobblestone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:mossy_cobblestone_stairs", "minecraft:cobblestone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:mossy_stone_brick_slab", "minecraft:stone_brick_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:mossy_stone_brick_stairs", "minecraft:stone_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:mossy_stone_brick_wall", "minecraft:mossy_cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:nether_brick_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:oak_sign", "minecraft:sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:oak_wall_sign", "minecraft:wall_sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:polished_andesite_slab", "minecraft:stone_brick_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:polished_andesite_stairs", "minecraft:stone_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:polished_diorite_slab", "minecraft:stone_brick_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:polished_diorite_stairs", "minecraft:stone_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:polished_granite_slab", "minecraft:stone_brick_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:polished_granite_stairs", "minecraft:stone_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_acacia_sapling", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_allium", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_azure_bluet", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_bamboo", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_birch_sapling", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_blue_orchid", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_brown_mushroom", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_cactus", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_cornflower", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_dandelion", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_dark_oak_sapling", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_dead_bush", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_fern", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_jungle_sapling", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_lily_of_the_valley", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_oak_sapling", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_orange_tulip", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_oxeye_daisy", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_pink_tulip", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_poppy", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_red_mushroom", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_red_tulip", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_spruce_sapling", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_white_tulip", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_wither_rose", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_nether_brick_slab", "minecraft:nether_brick_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_nether_brick_stairs", "minecraft:nether_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_nether_brick_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_sandstone_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:sandstone_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_quartz_slab", "minecraft:quartz_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_quartz_stairs", "minecraft:quartz_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_red_sandstone_slab", "minecraft:red_sandstone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_red_sandstone_stairs", "minecraft:red_sandstone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_sandstone_slab", "minecraft:sandstone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_sandstone_stairs", "minecraft:sandstone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_stone_slab", "minecraft:stone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_sign", "minecraft:sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_wall_sign", "minecraft:wall_sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:stone_brick_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:stone_stairs", "minecraft:stone_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:wither_rose", "minecraft:allium");

                    // Minecraft 1.14 (Snapshot 18w44a).
                    Diccionario_1_13_a_1_12_2.Add("minecraft:barrel", "minecraft:cauldron");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bell", "minecraft:note_block");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:blast_furnace", "minecraft:furnace");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:cartography_table", "minecraft:crafting_table");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:fletching_table", "minecraft:crafting_table");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:grindstone", "minecraft:anvil");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:lectern", "minecraft:bookshelf");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smithing_table", "minecraft:crafting_table");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smoker", "minecraft:furnace");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:stonecutter", "minecraft:anvil");

                    // Minecraft 1.14 (Snapshot 18w49a).
                    Diccionario_1_13_a_1_12_2.Add("minecraft:jigsaw", "minecraft:coal_block");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:lantern", "minecraft:sea_lantern");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:scaffolding", "minecraft:dirt");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:sweet_berry_bush", "minecraft:lime_stained_glass_pane");

                    // Minecraft 1.14 (Snapshot 19w06a).
                    Diccionario_1_13_a_1_12_2.Add("minecraft:campfire", "minecraft:magma_block");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:composter", "minecraft:cauldron");

                    // Minecraft 1.14.
                    Diccionario_1_13_a_1_12_2.Add("minecraft:cut_red_sandstone_slab", "minecraft:red_sandstone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:cut_sandstone_slab", "minecraft:sandstone_slab");

                    // New blocks from the Minecraft 1.15 snapshot 19w35a:
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:bee_hive", "minecraft:cauldron");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bee_nest", "minecraft:cauldron");

                    // New blocks from Minecraft 1.15.2:
                    Diccionario_1_13_a_1_12_2.Add("minecraft:beehive", "minecraft:cauldron");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:honey_block", "minecraft:slime_block");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:honeycomb_block", "minecraft:yellow_concrete");

                    // New blocks from the Minecraft 1.16 snapshot 20w06a:
                    Diccionario_1_13_a_1_12_2.Add("minecraft:ancient_debris", "minecraft:diamond_ore");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:basalt", "minecraft:cobblestone");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:crimson_button", "minecraft:oak_button");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:crimson_door", "minecraft:acacia_door");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:crimson_fence", "minecraft:acacia_fence");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:crimson_fence_gate", "minecraft:acacia_fence_gate");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:crimson_fungi", "minecraft:red_mushroom");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:crimson_nylium", "minecraft:red_concrete");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:crimson_planks", "minecraft:acacia_planks");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:crimson_pressure_plate", "minecraft:oak_pressure_plate");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:crimson_roots", "minecraft:red_carpet");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:crimson_sign", "minecraft:sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:crimson_slab", "minecraft:acacia_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:crimson_stairs", "minecraft:acacia_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:crimson_stem", "minecraft:acacia_log");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:crimson_trapdoor", "minecraft:oak_trapdoor");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:crimson_wall_sign", "minecraft:wall_sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:nether_sprouts", "minecraft:green_carpet");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:netherite_block", "minecraft:diamond_block");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:shroomlight", "minecraft:glowstone");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:soul_fire", "minecraft:fire");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:soul_fire_lantern", "minecraft:sea_lantern");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:soul_fire_torch", "minecraft:torch");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:soul_fire_wall_torch", "minecraft:torch");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:soul_soil", "minecraft:brown_concrete");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_crimson_stem", "minecraft:acacia_log");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_warped_stem", "minecraft:dark_oak_log");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:warped_button", "minecraft:stone_button");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:warped_door", "minecraft:dark_oak_door");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:warped_fence", "minecraft:dark_oak_fence");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:warped_fence_gate", "minecraft:dark_oak_fence_gate");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:warped_fungi", "minecraft:crimson_fungi");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:warped_nylium", "minecraft:cyan_concrete");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:warped_planks", "minecraft:dark_oak_planks");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:warped_pressure_plate", "minecraft:stone_pressure_plate");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:warped_roots", "minecraft:cyan_carpet");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:warped_sign", "minecraft:sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:warped_slab", "minecraft:dark_oak_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:warped_stairs", "minecraft:dark_oak_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:warped_stem", "minecraft:dark_oak_log");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:warped_trapdoor", "minecraft:iron_trapdoor");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:warped_wall_sign", "minecraft:wall_sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:warped_wart_block", "minecraft:dark_oak_leaves");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:weeping_vines", "minecraft:dark_oak_leaves");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:weeping_vines_plant", "minecraft:acacia_leaves");

                    // ...

                    /*Diccionario_1_13_a_1_12_2 = new SortedDictionary<string, string>();
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_bark", "");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_button", "minecraft:oak_button");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_pressure_plate", "minecraft:oak_pressure_plate");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_trapdoor", "minecraft:oak_trapdoor");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_wood", "minecraft:acacia_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:attached_melon_stem", "minecraft:melon_stem");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:attached_pumpkin_stem", "minecraft:pumpkin_stem");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:banner", "minecraft:white_banner");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:birch_bark", "");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:birch_button", "minecraft:oak_button");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:birch_pressure_plate", "minecraft:oak_pressure_plate");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:birch_trapdoor", "minecraft:oak_trapdoor");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:birch_wood", "minecraft:birch_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:black_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:blue_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:blue_coral", "minecraft:blue_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:blue_coral_fan", "minecraft:blue_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:blue_coral_plant", "minecraft:blue_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:blue_dead_coral", "minecraft:light_gray_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:blue_ice", "minecraft:light_blue_concrete");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:brain_coral", "minecraft:pink_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:brain_coral_block", "minecraft:pink_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:brain_coral_fan", "minecraft:pink_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:brain_coral_wall_fan", "minecraft:pink_stained_glass_pane");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:brown_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bubble_column", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bubble_coral", "minecraft:purple_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bubble_coral_block", "minecraft:purple_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bubble_coral_fan", "minecraft:purple_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bubble_coral_wall_fan", "minecraft:purple_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:cave_air", "minecraft:air");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:conduit", "minecraft:beacon");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:creeper_wall_head", "minecraft:creeper_head");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:cyan_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_bark", "");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_button", "minecraft:oak_button");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_pressure_plate", "minecraft:oak_pressure_plate");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_trapdoor", "minecraft:oak_trapdoor");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_wood", "minecraft:dark_oak_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_prismarine_slab", "minecraft:nether_brick_slab");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_prismarine_stairs", "minecraft:nether_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_brain_coral", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_brain_coral_block", "minecraft:light_gray_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_brain_coral_fan", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_brain_coral_wall_fan", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_bubble_coral", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_bubble_coral_block", "minecraft:light_gray_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_bubble_coral_fan", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_bubble_coral_wall_fan", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_fire_coral", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_fire_coral_block", "minecraft:light_gray_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_fire_coral_fan", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_fire_coral_wall_fan", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_horn_coral", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_horn_coral_block", "minecraft:light_gray_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_horn_coral_fan", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_horn_coral_wall_fan", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_tube_coral", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_tube_coral_block", "minecraft:light_gray_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_tube_coral_fan", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_tube_coral_wall_fan", "minecraft:light_gray_stained_glass_pane");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dragon_wall_head", "minecraft:dragon_head");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dried_kelp_block", "minecraft:green_concrete");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:fire_coral", "minecraft:red_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:fire_coral_block", "minecraft:red_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:fire_coral_fan", "minecraft:red_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:fire_coral_wall_fan", "minecraft:red_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:four_turtle_eggs", "minecraft:white_stained_glass_pane");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:gray_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:green_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:horn_coral", "minecraft:yellow_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:horn_coral_block", "minecraft:yellow_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:horn_coral_fan", "minecraft:yellow_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:horn_coral_wall_fan", "minecraft:yellow_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:item_frame", "minecraft:brown_stained_glass_pane");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_bark", "");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_button", "minecraft:oak_button");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_pressure_plate", "minecraft:oak_pressure_plate");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_trapdoor", "minecraft:oak_trapdoor");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_wood", "minecraft:jungle_planks");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:kelp", "minecraft:lime_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:kelp_plant", "minecraft:lime_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:kelp_top", "minecraft:lime_stained_glass_pane");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:light_blue_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:light_gray_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:lime_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:magenta_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:melon_block", "minecraft:melon");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:mob_spawner", "minecraft:spawner");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:mushroom_stem", "minecraft:brown_mushroom_block");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:oak_bark", "");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:oak_wood", "minecraft:oak_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:orange_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:pink_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:pink_coral", "minecraft:pink_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:pink_coral_fan", "minecraft:pink_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:pink_coral_plant", "minecraft:pink_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:pink_dead_coral", "minecraft:light_gray_stained_glass");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:player_wall_head", "minecraft:player_head");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:portal", "minecraft:nether_portal");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_brick_slab", "minecraft:stone_brick_slab");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_brick_stairs", "minecraft:stone_brick_stairs");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_bricks_slab", "minecraft:stone_brick_slab");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_bricks_stairs", "minecraft:stone_brick_stairs");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_slab", "minecraft:cobblestone_slab");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_stairs", "minecraft:cobblestone_stairs");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:pumpkin", "minecraft:carved_pumpkin");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:purple_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:purple_coral", "minecraft:purple_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:purple_coral_fan", "minecraft:purple_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:purple_coral_plant", "minecraft:purple_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:purple_dead_coral", "minecraft:light_gray_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_coral", "minecraft:red_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_coral_fan", "minecraft:red_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_coral_plant", "minecraft:red_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_dead_coral", "minecraft:light_gray_stained_glass");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:redstone_wall_torch", "minecraft:redstone_torch");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:sea_grass", "minecraft:green_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:sea_pickle", "minecraft:sea_lantern");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:seagrass", "minecraft:green_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:shulker_box", "minecraft:purple_shulker_box");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:skeleton_wall_skull", "minecraft:skeleton_skull");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_bark", "");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_button", "minecraft:oak_button");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_pressure_plate", "minecraft:oak_pressure_plate");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_trapdoor", "minecraft:oak_trapdoor");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_wood", "minecraft:spruce_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_acacia_log", "minecraft:acacia_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_acacia_wood", "minecraft:acacia_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_birch_log", "minecraft:birch_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_birch_wood", "minecraft:birch_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_dark_oak_log", "minecraft:dark_oak_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_dark_oak_wood", "minecraft:dark_oak_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_jungle_log", "minecraft:jungle_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_jungle_wood", "minecraft:jungle_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_oak_log", "minecraft:oak_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_oak_wood", "minecraft:oak_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_spruce_log", "minecraft:spruce_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_spruce_wood", "minecraft:spruce_planks");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:tall_sea_grass", "minecraft:green_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:tall_seagrass", "minecraft:green_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:three_turtle_eggs", "minecraft:white_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:tube_coral", "minecraft:blue_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:tube_coral_block", "minecraft:blue_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:tube_coral_fan", "minecraft:blue_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:tube_coral_wall_fan", "minecraft:blue_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:turtle_egg", "minecraft:white_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:two_turtle_eggs", "minecraft:white_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:void_air", "minecraft:air");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:wall_banner", "minecraft:white_banner");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:wall_torch", "minecraft:torch");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:white_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:wither_skeleton_wall_skull", "minecraft:wither_skeleton_skull");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:yellow_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:yellow_coral", "minecraft:yellow_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:yellow_coral_fan", "minecraft:yellow_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:yellow_coral_plant", "minecraft:yellow_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:yellow_dead_coral", "minecraft:light_gray_stained_glass");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:zombie_wall_head", "minecraft:zombie_head");

                    // Add support for all the new Minecraft 1.14 (Snapshot 18w43c) new block types:
                    Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_sign", "minecraft:sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_wall_sign", "minecraft:wall_sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:andesite_slab", "minecraft:cobblestone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:andesite_stairs", "minecraft:cobblestone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:andesite_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bamboo", "minecraft:lime_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bamboo_sapling", "minecraft:green_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:birch_sign", "minecraft:sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:birch_wall_sign", "minecraft:wall_sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:brick_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:cornflower", "minecraft:blue_orchid");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_sign", "minecraft:sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_wall_sign", "minecraft:wall_sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:diorite_slab", "minecraft:cobblestone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:diorite_stairs", "minecraft:cobblestone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:diorite_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:end_stone_brick_slab", "minecraft:cobblestone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:end_stone_brick_stairs", "minecraft:cobblestone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:end_stone_brick_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:granite_slab", "minecraft:cobblestone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:granite_stairs", "minecraft:cobblestone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:granite_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_sign", "minecraft:sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_wall_sign", "minecraft:wall_sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:lily_of_the_valley", "minecraft:oxeye_daisy");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:loom", "minecraft:crafting_table");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:mossy_cobblestone_slab", "minecraft:cobblestone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:mossy_cobblestone_stairs", "minecraft:cobblestone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:mossy_stone_brick_slab", "minecraft:stone_brick_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:mossy_stone_brick_stairs", "minecraft:stone_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:mossy_stone_brick_wall", "minecraft:mossy_cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:nether_brick_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:oak_sign", "minecraft:sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:oak_wall_sign", "minecraft:wall_sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:polished_andesite_slab", "minecraft:stone_brick_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:polished_andesite_stairs", "minecraft:stone_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:polished_diorite_slab", "minecraft:stone_brick_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:polished_diorite_stairs", "minecraft:stone_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:polished_granite_slab", "minecraft:stone_brick_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:polished_granite_stairs", "minecraft:stone_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_acacia_sapling", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_allium", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_azure_bluet", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_bamboo", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_birch_sapling", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_blue_orchid", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_brown_mushroom", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_cactus", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_cornflower", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_dandelion", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_dark_oak_sapling", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_dead_bush", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_fern", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_jungle_sapling", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_lily_of_the_valley", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_oak_sapling", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_orange_tulip", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_oxeye_daisy", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_pink_tulip", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_poppy", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_red_mushroom", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_red_tulip", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_spruce_sapling", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_white_tulip", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_wither_rose", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_nether_brick_slab", "minecraft:nether_brick_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_nether_brick_stairs", "minecraft:nether_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_nether_brick_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_sandstone_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:sandstone_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_quartz_slab", "minecraft:quartz_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_quartz_stairs", "minecraft:quartz_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_red_sandstone_slab", "minecraft:red_sandstone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_red_sandstone_stairs", "minecraft:red_sandstone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_sandstone_slab", "minecraft:sandstone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_sandstone_stairs", "minecraft:sandstone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_stone_slab", "minecraft:stone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_sign", "minecraft:sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_wall_sign", "minecraft:wall_sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:stone_brick_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:stone_stairs", "minecraft:stone_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:wither_rose", "minecraft:allium");

                    // Minecraft 1.14 (Snapshot 18w44a).
                    Diccionario_1_13_a_1_12_2.Add("minecraft:barrel", "minecraft:cauldron");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bell", "minecraft:note_block");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:blast_furnace", "minecraft:furnace");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:cartography_table", "minecraft:crafting_table");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:fletching_table", "minecraft:crafting_table");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:grindstone", "minecraft:anvil");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:lectern", "minecraft:bookshelf");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smithing_table", "minecraft:crafting_table");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smoker", "minecraft:furnace");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:stonecutter", "minecraft:anvil");

                    // Minecraft 1.14 (Snapshot 18w49a).
                    Diccionario_1_13_a_1_12_2.Add("minecraft:jigsaw", "minecraft:coal_block");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:lantern", "minecraft:glowstone");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:scaffolding", "minecraft:dirt");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:sweet_berry_bush", "minecraft:lime_stained_glass_pane");

                    // Minecraft 1.14 (Snapshot 19w06a).
                    Diccionario_1_13_a_1_12_2.Add("minecraft:campfire", "minecraft:magma_block");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:composter", "minecraft:cauldron");*/
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void DataGridView_Transmutación_KeyDown(object sender, KeyEventArgs e)
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
                        Botón_Convertir.PerformClick();
                    }
                    else if (e.KeyCode == Keys.Divide)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Botón_Transmutación_Restablecer.PerformClick();
                    }
                    else if (e.KeyCode == Keys.Multiply)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Botón_Transmutación_Aleatorizar.PerformClick();
                    }
                    else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Botón_Transmutación_Quitar.PerformClick();
                    }
                    else if (e.KeyCode == Keys.Insert || e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.Add)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Botón_Transmutación_Agregar.PerformClick();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Transmutación_Menú_Contextual_Click(object sender, EventArgs e)
        {
            try
            {
                Menú_Contextual.Show(Grupo_Transmutación, 0, 0);
                Menú_Contextual_Reemplazar_Lecho_Roca_Aire.Select();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Biomas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Biomas = ComboBox_Biomas.SelectedIndex;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Bioma_Vacío_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Bioma_Vacío = ComboBox_Bioma_Vacío.SelectedIndex;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Auto_Destrucción_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Auto_Destrucción = CheckBox_Auto_Destrucción.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Checks if a block ID represents Air, Water, Lava, Flowing water or Flowing lava.
        /// </summary>
        /// <param name="ID">Any valid block ID between 0 and 255.</param>
        /// <returns>Returns true if the ID is any of the searched block types. Returns false otherwise.</returns>
        internal bool ID_Bloque_Aire_Agua_Lava(int ID)
        {
            try
            {
                return ID == 0 || ID == 8 || ID == 9 || ID == 10 || ID == 11;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return false;
        }

        /// <summary>
        /// Checks if a block index represents Air, Water, Lava, Flowing water or Flowing lava.
        /// </summary>
        /// <param name="ID">Any valid block index. Note that this index has nothing to do with Minecraft and it's created internally by this application to quickly recognize block types.</param>
        /// <returns>Returns true if the index is any of the searched block types. Returns false otherwise.</returns>
        internal bool Bloque_Índice_Aire_Agua_Lava(int Índice)
        {
            try
            {
                return Índice == Índice_Aire || Índice == Índice_Agua || Índice == Índice_Lava || Índice == Índice_Agua_Fluyendo || Índice == Índice_Lava_Fluyendo;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return false;
        }

        private void Menú_Contextual_Cuantización_Concrete_Click(object sender, EventArgs e)
        {
            try
            {
                Agregar_Cuantizaciones(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("minecraft:white_concrete", " "),
                    new KeyValuePair<string, string>("minecraft:orange_concrete", " "),
                    new KeyValuePair<string, string>("minecraft:magenta_concrete", " "),
                    new KeyValuePair<string, string>("minecraft:light_blue_concrete", " "),
                    new KeyValuePair<string, string>("minecraft:yellow_concrete", " "),
                    new KeyValuePair<string, string>("minecraft:lime_concrete", " "),
                    new KeyValuePair<string, string>("minecraft:pink_concrete", " "),
                    new KeyValuePair<string, string>("minecraft:gray_concrete", " "),
                    new KeyValuePair<string, string>("minecraft:light_gray_concrete", " "),
                    new KeyValuePair<string, string>("minecraft:cyan_concrete", " "),
                    new KeyValuePair<string, string>("minecraft:purple_concrete", " "),
                    new KeyValuePair<string, string>("minecraft:blue_concrete", " "),
                    new KeyValuePair<string, string>("minecraft:brown_concrete", " "),
                    new KeyValuePair<string, string>("minecraft:green_concrete", " "),
                    new KeyValuePair<string, string>("minecraft:red_concrete", " "),
                    new KeyValuePair<string, string>("minecraft:black_concrete", " ")
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Cuantización_Concrete_Powder_Click(object sender, EventArgs e)
        {
            try
            {
                Agregar_Cuantizaciones(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("minecraft:white_concrete_powder", " "),
                    new KeyValuePair<string, string>("minecraft:orange_concrete_powder", " "),
                    new KeyValuePair<string, string>("minecraft:magenta_concrete_powder", " "),
                    new KeyValuePair<string, string>("minecraft:light_blue_concrete_powder", " "),
                    new KeyValuePair<string, string>("minecraft:yellow_concrete_powder", " "),
                    new KeyValuePair<string, string>("minecraft:lime_concrete_powder", " "),
                    new KeyValuePair<string, string>("minecraft:pink_concrete_powder", " "),
                    new KeyValuePair<string, string>("minecraft:gray_concrete_powder", " "),
                    new KeyValuePair<string, string>("minecraft:light_gray_concrete_powder", " "),
                    new KeyValuePair<string, string>("minecraft:cyan_concrete_powder", " "),
                    new KeyValuePair<string, string>("minecraft:purple_concrete_powder", " "),
                    new KeyValuePair<string, string>("minecraft:blue_concrete_powder", " "),
                    new KeyValuePair<string, string>("minecraft:brown_concrete_powder", " "),
                    new KeyValuePair<string, string>("minecraft:green_concrete_powder", " "),
                    new KeyValuePair<string, string>("minecraft:red_concrete_powder", " "),
                    new KeyValuePair<string, string>("minecraft:black_concrete_powder", " ")
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Cuantización_Glazed_Terracotta_Click(object sender, EventArgs e)
        {
            try
            {
                Agregar_Cuantizaciones(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("minecraft:white_glazed_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:orange_glazed_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:magenta_glazed_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:light_blue_glazed_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:yellow_glazed_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:lime_glazed_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:pink_glazed_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:gray_glazed_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:light_gray_glazed_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:cyan_glazed_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:purple_glazed_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:blue_glazed_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:brown_glazed_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:green_glazed_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:red_glazed_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:black_glazed_terracotta", " ")
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Cuantización_Stained_Glass_Click(object sender, EventArgs e)
        {
            try
            {
                Agregar_Cuantizaciones(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("minecraft:white_stained_glass", " "),
                    new KeyValuePair<string, string>("minecraft:orange_stained_glass", " "),
                    new KeyValuePair<string, string>("minecraft:magenta_stained_glass", " "),
                    new KeyValuePair<string, string>("minecraft:light_blue_stained_glass", " "),
                    new KeyValuePair<string, string>("minecraft:yellow_stained_glass", " "),
                    new KeyValuePair<string, string>("minecraft:lime_stained_glass", " "),
                    new KeyValuePair<string, string>("minecraft:pink_stained_glass", " "),
                    new KeyValuePair<string, string>("minecraft:gray_stained_glass", " "),
                    new KeyValuePair<string, string>("minecraft:light_gray_stained_glass", " "),
                    new KeyValuePair<string, string>("minecraft:cyan_stained_glass", " "),
                    new KeyValuePair<string, string>("minecraft:purple_stained_glass", " "),
                    new KeyValuePair<string, string>("minecraft:blue_stained_glass", " "),
                    new KeyValuePair<string, string>("minecraft:brown_stained_glass", " "),
                    new KeyValuePair<string, string>("minecraft:green_stained_glass", " "),
                    new KeyValuePair<string, string>("minecraft:red_stained_glass", " "),
                    new KeyValuePair<string, string>("minecraft:black_stained_glass", " ")
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Cuantización_Terracotta_Click(object sender, EventArgs e)
        {
            try
            {
                Agregar_Cuantizaciones(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("minecraft:white_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:orange_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:magenta_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:light_blue_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:yellow_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:lime_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:pink_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:gray_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:light_gray_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:cyan_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:purple_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:blue_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:brown_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:green_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:red_terracotta", " "),
                    new KeyValuePair<string, string>("minecraft:black_terracotta", " ")
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Cuantización_Wool_Click(object sender, EventArgs e)
        {
            try
            {
                Agregar_Cuantizaciones(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("minecraft:white_wool", " "),
                    new KeyValuePair<string, string>("minecraft:orange_wool", " "),
                    new KeyValuePair<string, string>("minecraft:magenta_wool", " "),
                    new KeyValuePair<string, string>("minecraft:light_blue_wool", " "),
                    new KeyValuePair<string, string>("minecraft:yellow_wool", " "),
                    new KeyValuePair<string, string>("minecraft:lime_wool", " "),
                    new KeyValuePair<string, string>("minecraft:pink_wool", " "),
                    new KeyValuePair<string, string>("minecraft:gray_wool", " "),
                    new KeyValuePair<string, string>("minecraft:light_gray_wool", " "),
                    new KeyValuePair<string, string>("minecraft:cyan_wool", " "),
                    new KeyValuePair<string, string>("minecraft:purple_wool", " "),
                    new KeyValuePair<string, string>("minecraft:blue_wool", " "),
                    new KeyValuePair<string, string>("minecraft:brown_wool", " "),
                    new KeyValuePair<string, string>("minecraft:green_wool", " "),
                    new KeyValuePair<string, string>("minecraft:red_wool", " "),
                    new KeyValuePair<string, string>("minecraft:black_wool", " ")
                });
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Cuantización_Restablecer_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridView_Cuantización.Rows != null && DataGridView_Cuantización.Rows.Count > 0)
                {
                    if (MessageBox.Show(this, "Do you want to delete all the quantizations in the list?\r\nThis operation can't be undone later.", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        List<KeyValuePair<string, string>> Lista_Cuantizaciones = new List<KeyValuePair<string, string>>();
                        foreach (DataGridViewRow Fila in DataGridView_Cuantización.Rows)
                        {
                            Lista_Cuantizaciones.Add(new KeyValuePair<string, string>(Fila.Cells[Columna_Entrada.Index].Value.ToString(), null));
                        }
                        Agregar_Cuantizaciones(Lista_Cuantizaciones.ToArray());
                    }
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Cuantización_Aleatorizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(this, "Do you want to randomize all the quantizations?\r\nThis operation can't be undone later.", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (DataGridView_Cuantización.Rows != null && DataGridView_Cuantización.Rows.Count > 0)
                    {
                        List<KeyValuePair<string, string>> Lista_Cuantizaciones_Quitar = new List<KeyValuePair<string, string>>();
                        foreach (DataGridViewRow Fila in DataGridView_Cuantización.Rows)
                        {
                            Lista_Cuantizaciones_Quitar.Add(new KeyValuePair<string, string>(Fila.Cells[Columna_Bloques.Index].Value.ToString(), null));
                        }
                        Agregar_Cuantizaciones(Lista_Cuantizaciones_Quitar.ToArray());
                    } // Remove all.

                    // Add the name of all the known blocks inside a list.
                    List<string> Lista_Nombres_Válidos = new List<string>();
                    List<string> Lista_Nombres_Temporal = new List<string>();
                    List<string> Lista_Nombres_Entrada = new List<string>();

                    for (int Índice = 0; Índice < Minecraft.Bloques.Matriz_Bloques.Length; Índice++)
                    {
                        // Ignore blocks from old snapshots, partial size blocks, air, water and lava.
                        if (!Minecraft.Bloques.Matriz_Bloques[Índice].Obsoleto &&
                            !Minecraft.Bloques.Matriz_Bloques[Índice].Altura_Diferente &&
                            string.Compare(Minecraft.Bloques.Matriz_Bloques[Índice].Nombre_1_13, "minecraft:air", true) != 0 &&
                            string.Compare(Minecraft.Bloques.Matriz_Bloques[Índice].Nombre_1_13, "minecraft:water", true) != 0 &&
                            string.Compare(Minecraft.Bloques.Matriz_Bloques[Índice].Nombre_1_13, "minecraft:lava", true) != 0 &&
                            string.Compare(Minecraft.Bloques.Matriz_Bloques[Índice].Nombre_1_13, "minecraft:flowing_water", true) != 0 &&
                            string.Compare(Minecraft.Bloques.Matriz_Bloques[Índice].Nombre_1_13, "minecraft:flowing_lava", true) != 0 &&
                            Program.Rand.Next(1, 101) <= 10) // Only allow 10 % of the valid blocks.
                        {
                            Lista_Nombres_Válidos.Add(Minecraft.Bloques.Matriz_Bloques[Índice].Nombre_1_13);
                        }
                    }

                    Lista_Nombres_Temporal.AddRange(Lista_Nombres_Válidos.GetRange(0, Lista_Nombres_Válidos.Count));
                    for (int Índice = Lista_Nombres_Temporal.Count - 1; Índice >= 0; Índice--)
                    {
                        int Índice_Aleatorio = Program.Rand.Next(0, Lista_Nombres_Temporal.Count);
                        Lista_Nombres_Entrada.Add(Lista_Nombres_Temporal[Índice_Aleatorio]);
                        Lista_Nombres_Temporal.RemoveAt(Índice_Aleatorio);
                    }
                    Lista_Nombres_Temporal = null;

                    List<KeyValuePair<string, string>> Lista_Cuantizaciones = new List<KeyValuePair<string, string>>();
                    for (int Índice = 0; Índice < Lista_Nombres_Válidos.Count; Índice++)
                    {
                        Lista_Cuantizaciones.Add(new KeyValuePair<string, string>(Lista_Nombres_Entrada[Índice], " "));
                    }
                    Agregar_Cuantizaciones(Lista_Cuantizaciones.ToArray());

                    Lista_Cuantizaciones = null;
                    Lista_Nombres_Entrada = null;
                    Lista_Nombres_Válidos = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Cuantización_Menú_Contextual_Click(object sender, EventArgs e)
        {
            try
            {
                Menú_Contextual.Show(Grupo_Cuantización, 0, 0);
                Menú_Contextual_Cuantización_Concrete.Select();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Cuantización_Quitar_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridView_Cuantización.SelectedRows != null && DataGridView_Cuantización.SelectedRows.Count > 0)
                {
                    List<KeyValuePair<string, string>> Lista_Cuantizaciones = new List<KeyValuePair<string, string>>();
                    foreach (DataGridViewRow Fila in DataGridView_Cuantización.SelectedRows)
                    {
                        Lista_Cuantizaciones.Add(new KeyValuePair<string, string>(Fila.Cells[Columna_Bloques.Index].Value.ToString(), null));
                    }
                    Agregar_Cuantizaciones(Lista_Cuantizaciones.ToArray());
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Cuantización_Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Transmutación_Cuantización Ventana = new Ventana_Transmutación_Cuantización();
                Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                Ventana.Diccionario_Transmutación_Cuantización = Diccionario_Cuantización;
                Ventana.Cuantización = true;
                if (Ventana.ShowDialog(this) == DialogResult.OK)
                {
                    Agregar_Cuantizaciones(new KeyValuePair<string, string>[] { Ventana.Transmutación_Cuantización });
                }
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void DataGridView_Cuantización_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
                Depurador.Escribir_Excepción(e.Exception != null ? e.Exception.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void DataGridView_Cuantización_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Right)
                {
                    DataGridView.HitTestInfo Info = DataGridView_Cuantización.HitTest(e.X, e.Y);
                    if (Info.Type == DataGridViewHitTestType.None)
                    {
                        DataGridView_Cuantización.ClearSelection();
                        DataGridView_Cuantización.CurrentCell = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Importar_Transmutaciones_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Conversor_Mundos_1_13_1_12_2);
                OpenFileDialog Diálogo_Abrir = new OpenFileDialog();
                Diálogo_Abrir.Filter = "Plain text files (*.txt)|*.txt|All files (*.*)|*.*";
                Diálogo_Abrir.InitialDirectory = Program.Ruta_Guardado_Imágenes_Conversor_Mundos_1_13_1_12_2;
                Diálogo_Abrir.Title = "Import the transmutations...";
                if (Diálogo_Abrir.ShowDialog(this) == DialogResult.OK)
                {
                    string Ruta = Diálogo_Abrir.FileName;
                    if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                    {
                        FileStream Lector = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        if (Lector.Length > 0L)
                        {
                            Lector.Seek(0L, SeekOrigin.Begin);
                            StreamReader Lector_Texto = new StreamReader(Lector, true);
                            List<KeyValuePair<string, string>> Lista_Transmutaciones = new List<KeyValuePair<string, string>>();
                            while (!Lector_Texto.EndOfStream)
                            {
                                string Línea_Entrada = Lector_Texto.ReadLine();
                                string Línea_Salida = Lector_Texto.ReadLine();
                                if (!string.IsNullOrEmpty(Línea_Entrada) && !string.IsNullOrEmpty(Línea_Salida))
                                {
                                    if (Minecraft.Bloques.Diccionario_Nombre_Índice.ContainsKey(Línea_Entrada) &&
                                        Minecraft.Bloques.Diccionario_Nombre_Índice.ContainsKey(Línea_Salida))
                                    {
                                        Lista_Transmutaciones.Add(new KeyValuePair<string, string>(Línea_Entrada, Línea_Salida));
                                    }
                                }
                            }
                            if (Lista_Transmutaciones.Count > 0)
                            {
                                if (DataGridView_Transmutación.Rows != null && DataGridView_Transmutación.Rows.Count > 0)
                                {
                                    List<KeyValuePair<string, string>> Lista_Transmutaciones_Quitar = new List<KeyValuePair<string, string>>();
                                    foreach (DataGridViewRow Fila in DataGridView_Transmutación.Rows)
                                    {
                                        Lista_Transmutaciones_Quitar.Add(new KeyValuePair<string, string>(Fila.Cells[Columna_Entrada.Index].Value.ToString(), null));
                                    }
                                    Agregar_Transmutaciones(Lista_Transmutaciones_Quitar.ToArray());
                                    Lista_Transmutaciones_Quitar = null;
                                }
                                Agregar_Transmutaciones(Lista_Transmutaciones.ToArray());
                            }
                            else SystemSounds.Beep.Play();
                            Lista_Transmutaciones = null;
                            Lector_Texto.Close();
                            Lector_Texto.Dispose();
                            Lector_Texto = null;
                        }
                        else SystemSounds.Beep.Play();
                        Lector.Close();
                        Lector.Dispose();
                        Lector = null;
                    }
                    else SystemSounds.Beep.Play();
                }
                Diálogo_Abrir.Dispose();
                Diálogo_Abrir = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Exportar_Transmutaciones_Click(object sender, EventArgs e)
        {
            try
            {
                if (Diccionario_Transmutación.Count > 0)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Conversor_Mundos_1_13_1_12_2);
                    string Ruta = Program.Ruta_Guardado_Imágenes_Conversor_Mundos_1_13_1_12_2 + "\\" + Program.Obtener_Nombre_Temporal() + " Transmutations " + Diccionario_Transmutación.Count.ToString() + ".txt";
                    FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector.SetLength(0L);
                    Lector.Seek(0L, SeekOrigin.Begin);
                    StreamWriter Lector_Texto = new StreamWriter(Lector, Encoding.Default);
                    foreach (KeyValuePair<string, string> Entrada in Diccionario_Transmutación)
                    {
                        Lector_Texto.WriteLine(Entrada.Key);
                        Lector_Texto.WriteLine(Entrada.Value);
                    }
                    Lector_Texto.Close();
                    Lector_Texto.Dispose();
                    Lector_Texto = null;
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                    SystemSounds.Asterisk.Play();
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Importar_Cuantizaciones_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Conversor_Mundos_1_13_1_12_2);
                OpenFileDialog Diálogo_Abrir = new OpenFileDialog();
                Diálogo_Abrir.Filter = "Plain text files (*.txt)|*.txt|All files (*.*)|*.*";
                Diálogo_Abrir.InitialDirectory = Program.Ruta_Guardado_Imágenes_Conversor_Mundos_1_13_1_12_2;
                Diálogo_Abrir.Title = "Import the quantizations...";
                if (Diálogo_Abrir.ShowDialog(this) == DialogResult.OK)
                {
                    string Ruta = Diálogo_Abrir.FileName;
                    if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                    {
                        FileStream Lector = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        if (Lector.Length > 0L)
                        {
                            Lector.Seek(0L, SeekOrigin.Begin);
                            StreamReader Lector_Texto = new StreamReader(Lector, true);
                            List<KeyValuePair<string, string>> Lista_Cuantizaciones = new List<KeyValuePair<string, string>>();
                            while (!Lector_Texto.EndOfStream)
                            {
                                string Línea_Entrada = Lector_Texto.ReadLine();
                                if (!string.IsNullOrEmpty(Línea_Entrada))
                                {
                                    if (Minecraft.Bloques.Diccionario_Nombre_Índice.ContainsKey(Línea_Entrada))
                                    {
                                        Lista_Cuantizaciones.Add(new KeyValuePair<string, string>(Línea_Entrada, " "));
                                    }
                                }
                            }
                            if (Lista_Cuantizaciones.Count > 0)
                            {
                                if (DataGridView_Cuantización.Rows != null && DataGridView_Cuantización.Rows.Count > 0)
                                {
                                    List<KeyValuePair<string, string>> Lista_Cuantizaciones_Quitar = new List<KeyValuePair<string, string>>();
                                    foreach (DataGridViewRow Fila in DataGridView_Cuantización.Rows)
                                    {
                                        Lista_Cuantizaciones_Quitar.Add(new KeyValuePair<string, string>(Fila.Cells[Columna_Bloques.Index].Value.ToString(), null));
                                    }
                                    Agregar_Cuantizaciones(Lista_Cuantizaciones_Quitar.ToArray());
                                    Lista_Cuantizaciones_Quitar = null;
                                }
                                Agregar_Cuantizaciones(Lista_Cuantizaciones.ToArray());
                            }
                            else SystemSounds.Beep.Play();
                            Lista_Cuantizaciones = null;
                            Lector_Texto.Close();
                            Lector_Texto.Dispose();
                            Lector_Texto = null;
                        }
                        else SystemSounds.Beep.Play();
                        Lector.Close();
                        Lector.Dispose();
                        Lector = null;
                    }
                    else SystemSounds.Beep.Play();
                }
                Diálogo_Abrir.Dispose();
                Diálogo_Abrir = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Exportar_Cuantizaciones_Click(object sender, EventArgs e)
        {
            try
            {
                if (Diccionario_Cuantización.Count > 0)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Conversor_Mundos_1_13_1_12_2);
                    string Ruta = Program.Ruta_Guardado_Imágenes_Conversor_Mundos_1_13_1_12_2 + "\\" + Program.Obtener_Nombre_Temporal() + " Quantizations " + Diccionario_Cuantización.Count.ToString() + ".txt";
                    FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector.SetLength(0L);
                    Lector.Seek(0L, SeekOrigin.Begin);
                    StreamWriter Lector_Texto = new StreamWriter(Lector, Encoding.Default);
                    foreach (KeyValuePair<string, string> Entrada in Diccionario_Cuantización)
                    {
                        Lector_Texto.WriteLine(Entrada.Key);
                    }
                    Lector_Texto.Close();
                    Lector_Texto.Dispose();
                    Lector_Texto = null;
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                    SystemSounds.Asterisk.Play();
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Mundo_Invertido_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Mundo_Invertido = CheckBox_Mundo_Invertido.CheckState != CheckState.Unchecked;
                Variable_Mundo_Invertido_Suelo = CheckBox_Mundo_Invertido.CheckState == CheckState.Indeterminate;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Thread function that converts the overworld dimension from any InfDev world.
        /// </summary>
        internal void Subproceso_NBT_DoWork()
        {
            bool Subproceso_Abortado = false; // Used to know if the window must be closed.
            try
            {
                Subproceso_Activo = true;
                Stopwatch Cronómetro_Total = Stopwatch.StartNew();
                Texto_Bloques_Desconocidos = null;
                Diccionario_Bloques_Desconocidos.Clear();
                Diccionario_Bloques_Obsoletos.Clear();
                ////Lista_Propiedades_Únicas.Clear(); // 2018_10_08_12_25_04_362
                //Lista_Propiedades_Ejemplos.Clear();
                this.Invoke(new Invocación.Delegado_Control_Cursor(Invocación.Ejecutar_Delegado_Control_Cursor), new object[] { this, Cursors.WaitCursor });
                //Stopwatch Cronómetro_Región = new Stopwatch();
                //Stopwatch Cronómetro_Chunk = new Stopwatch();
                // Start the progress bars.
                int Progreso_Chunk = 32768;
                int Progreso_Total = 0;
                int Total_Chunks = 0;
                this.Invoke(new Invocación.Delegado_ProgressBar_Maximum(Invocación.Ejecutar_Delegado_ProgressBar_Maximum), new object[] { Barra_Progreso_Chunk, Progreso_Chunk });
                this.Invoke(new Invocación.Delegado_ProgressBar_Maximum(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Chunk, Progreso_Chunk });
                this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Región, "Chunk progress: " + Program.Traducir_Número_Decimales_Redondear(((double)Progreso_Chunk * 100d) / 32768d, 4) + " % (" + Program.Traducir_Número(Progreso_Chunk) + " of 32.768 blocks)" });
                this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Total, "Total progress: " + Program.Traducir_Número_Decimales_Redondear(Total_Chunks != 0 ? (((double)Progreso_Total * 100d) / (double)Total_Chunks) : 0d, 4) + " % (" + Program.Traducir_Número(Progreso_Total) + " of " + Program.Traducir_Número(Total_Chunks) + (Total_Chunks != 1L ? " chunks)" : " chunk)") });

                string[] Matriz_Archivos = Directory.GetFiles(Ruta_Mundo, "*.dat", SearchOption.AllDirectories);
                List<string> Lista_Archivos_DAT = new List<string>();
                if (Matriz_Archivos != null && Matriz_Archivos.Length > 0)
                {
                    foreach (string Archivo in Matriz_Archivos)
                    {
                        if (string.Compare(Archivo, Ruta_Mundo + "\\level.dat", true) != 0) // Ignore the level file.
                        {
                            Lista_Archivos_DAT.Add(Archivo);
                        }
                    }
                    Matriz_Archivos = null;
                    if (Lista_Archivos_DAT != null && Lista_Archivos_DAT.Count > 0)
                    {
                        if (Lista_Archivos_DAT.Count > 1) Lista_Archivos_DAT.Sort();
                        Total_Chunks = Lista_Archivos_DAT.Count;
                        this.Invoke(new Invocación.Delegado_ProgressBar_Maximum(Invocación.Ejecutar_Delegado_ProgressBar_Maximum), new object[] { Barra_Progreso_Región, Total_Chunks });
                        // Load all the available information for the level and player to copy those values later.
                        Minecraft.Información_Niveles Información_Nivel = Minecraft.Información_Niveles.Obtener_Información_Nivel(Ruta_Mundo);
                        Dictionary<string, Minecraft.Posiciones_Jugadores> Diccionario_Posiciones_Jugadores = Minecraft.Posiciones_Jugadores.Obtener_Posiciones_Jugadores(Ruta_Mundo);
                        Minecraft.Posiciones_Jugadores Posición_Jugador = new Minecraft.Posiciones_Jugadores(Información_Nivel.SpawnX, Math.Min((Información_Nivel.SpawnY + 1L), 255L), Información_Nivel.SpawnZ); // Set the spawn point as the default player position, but 1 block higher (just in case).
                        if (Diccionario_Posiciones_Jugadores != null && Diccionario_Posiciones_Jugadores.Count > 0)
                        {
                            foreach (KeyValuePair<string, Minecraft.Posiciones_Jugadores> Entrada in Diccionario_Posiciones_Jugadores)
                            {
                                Posición_Jugador = Entrada.Value; // Use the position for the first player found.
                                break;
                            }
                        }

                        string Ruta = Program.Ruta_Guardado_Minecraft + "\\" + Program.Obtener_Nombre_Temporal() + " 1_13_to_1_12";
                        if (Directory.Exists(Ruta))
                        {
                            this.Invoke(new Invocación.Delegado_IWin32Window_MessageBox(Invocación.Ejecutar_Delegado_IWin32Window_MessageBox), new object[] { this, "Somehow the directory name for the new Minecraft map already exists.\r\nPlease try it again if the system clock is running properly.\r\nPath: \"" + Ruta + "\".", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning });
                            Ruta = null;
                            return;
                        }
                        Program.Crear_Carpetas(Ruta);
                        AnvilWorld Mundo = AnvilWorld.Create(Ruta);
                        Mundo.Level.LevelName = Path.GetFileName(Ruta);
                        Mundo.Level.UseMapFeatures = true; // ?
                        //Mundo.Level.GeneratorOptions = "1;minecraft:bedrock"; // Not used for now.
                        Mundo.Level.GameType = GameType.CREATIVE;
                        Mundo.Level.Spawn = new SpawnPoint((int)Información_Nivel.SpawnX, (int)Math.Min((Información_Nivel.SpawnY + 1L), 255L), (int)Información_Nivel.SpawnZ);
                        Mundo.Level.AllowCommands = true; // Allow cheats.
                        Mundo.Level.GameRules.DoMobSpawning = true; // Spawn mobs.
                        Mundo.Level.GameRules.DoFireTick = false; // Prevent the new level to burn out.
                        Mundo.Level.GameRules.MobGriefing = false; // Prevent the mobs to destroy anything.
                        Mundo.Level.GameRules.KeepInventory = true; // Keep the player inventory.
                        //Mundo.Level.RainTime = (int)Información_Nivel.RainTime;
                        //Mundo.Level.IsRaining = Información_Nivel.Raining != 0L;
                        Mundo.Level.Player = new Player();
                        Mundo.Level.Player.Dimension = 0; //Posición_Jugador.Dimesión; // 0 = Overworld, -1 = Nether, +1 = The End.
                        Mundo.Level.Player.Position = new Vector3();
                        Mundo.Level.Player.Position.X = (double)Posición_Jugador.X; // Try to spawn where the player was.
                        Mundo.Level.Player.Position.Y = (double)Posición_Jugador.Y;
                        Mundo.Level.Player.Position.Z = (double)Posición_Jugador.Z;
                        Substrate.Orientation Orientación = new Substrate.Orientation();
                        Orientación.Pitch = 45d; // -90º a +90º // 45º = Camera centered (looking into the horizon).
                        Orientación.Yaw = -45d; // -180º a +180º // -45º = Camera rotation (looking at the southeast).
                        Mundo.Level.Player.Rotation = Orientación;
                        Mundo.Level.Player.Spawn = new SpawnPoint((int)Información_Nivel.SpawnX, (int)Math.Min((Información_Nivel.SpawnY + 1L), 255L), (int)Información_Nivel.SpawnZ);
                        Mundo.Level.Player.Abilities.Flying = true; // Start with creative flight enabled.
                        Mundo.Level.RandomSeed = Información_Nivel.RandomSeed; // Copy the original seed.
                        //Mundo.Level.ThunderTime = (int)Información_Nivel.ThunderTime;
                        //Mundo.Level.IsThundering = Información_Nivel.Thundering != 0L;

                        IChunkManager Chunks_Overworld = Mundo.GetChunkManager(0);
                        //BlockManager Bloques_Overworld = Mundo.GetBlockManager(0);

                        foreach (string Archivo in Lista_Archivos_DAT) // Load each chunk DAt file as read-only.
                        {
                            Progreso_Total++;
                            this.Invoke(new Invocación.Delegado_ProgressBar_Maximum(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Región, Progreso_Total });
                            this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Total, "Total progress: " + Program.Traducir_Número_Decimales_Redondear(Total_Chunks != 0 ? (((double)Progreso_Total * 100d) / (double)Total_Chunks) : 0d, 4) + " % (" + Program.Traducir_Número(Progreso_Total) + " of " + Program.Traducir_Número(Total_Chunks) + (Total_Chunks != 1L ? " chunks)" : " chunk)") });
                            FileStream Lector = new FileStream(Archivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            if (Lector != null && Lector.Length > 0L)
                            {
                                NbtTree Árbol = null;
                                try
                                {
                                    Lector.Seek(0L, SeekOrigin.Begin);
                                    Árbol = new NbtTree();
                                    Árbol.ReadFrom(Lector);
                                }
                                catch { Árbol = null; }
                                if (Árbol == null || Árbol.Root == null)
                                {
                                    try
                                    {
                                        Lector.Seek(0L, SeekOrigin.Begin);
                                        Árbol = new NbtTree();
                                        Árbol.ReadFrom(new GZipStream(Lector, CompressionMode.Decompress));
                                    }
                                    catch { Árbol = null; }
                                }
                                if (Árbol == null || Árbol.Root == null)
                                {
                                    try
                                    {
                                        Lector.Seek(0L, SeekOrigin.Begin);
                                        Árbol = new NbtTree();
                                        Árbol.ReadFrom(new ZlibStream(Lector, CompressionMode.Decompress));
                                    }
                                    catch { Árbol = null; }
                                }
                                if (Árbol == null || Árbol.Root == null)
                                {
                                    try
                                    {
                                        Lector.Seek(0L, SeekOrigin.Begin);
                                        Árbol = new NbtTree();
                                        Árbol.ReadFrom(new DeflateStream(Lector, CompressionMode.Decompress));
                                    }
                                    catch { Árbol = null; }
                                }
                                if (Árbol != null && Árbol.Root != null && Árbol.Root.Keys != null && Árbol.Root.Keys.Count > 0)
                                {
                                    foreach (string Clave in Árbol.Root.Keys)
                                    {
                                        try
                                        {
                                            if (Pendiente_Subproceso_Abortar)
                                            {
                                                Pendiente_Subproceso_Abortar = false;
                                                Chunks_Overworld.Save(); // Save the part of the chunks already generated.
                                                Chunks_Overworld = null;
                                                //Bloques_Overworld = null;
                                                Mundo.Save(); // Save the part of the world already generated.
                                                Mundo = null;
                                                Subproceso_Abortado = true;
                                                return; // Cancel safely before time.
                                            }
                                            if (string.Compare(Clave, "Level", true) == 0)
                                            {
                                                TagNodeCompound Nodo_Compuesto = Árbol.Root[Clave] as TagNodeCompound;
                                                if (Nodo_Compuesto != null && Nodo_Compuesto.Keys != null && Nodo_Compuesto.Keys.Count > 0)
                                                {
                                                    long Chunk_X = long.MinValue;
                                                    long Chunk_Z = long.MinValue;
                                                    byte[] Matriz_Bytes_Blocks = null;
                                                    byte[] Matriz_Bytes_Data = null;
                                                    foreach (string Subclave in Nodo_Compuesto.Keys)
                                                    {
                                                        if (string.Compare(Subclave, "Blocks", true) == 0) // 32.768 bytes (maximum height = 127).
                                                        {
                                                            try { Matriz_Bytes_Blocks = (byte[])Nodo_Compuesto[Subclave].ToTagByteArray(); }
                                                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Matriz_Bytes_Blocks = null; }
                                                        }
                                                        else if (string.Compare(Subclave, "Data", true) == 0) // 32.768 bytes (maximum height = 127).
                                                        {
                                                            try
                                                            {
                                                                byte[] Matriz_Bytes_Temporal = (byte[])Nodo_Compuesto[Subclave].ToTagByteArray();
                                                                if (Matriz_Bytes_Temporal != null && Matriz_Bytes_Temporal.Length >= 16384)
                                                                {
                                                                    Matriz_Bytes_Data = new byte[32768];
                                                                    for (int Índice_Byte = 0, Índice_Data = 0; Índice_Byte < Matriz_Bytes_Temporal.Length; Índice_Byte++, Índice_Data += 2)
                                                                    {
                                                                        Matriz_Bytes_Data[Índice_Data + 1] = (byte)((Matriz_Bytes_Temporal[Índice_Byte] >> 4) & 0xF);
                                                                        Matriz_Bytes_Data[Índice_Data] = (byte)(Matriz_Bytes_Temporal[Índice_Byte] & 0xF);
                                                                    }
                                                                }
                                                                Matriz_Bytes_Temporal = null;
                                                            }
                                                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Matriz_Bytes_Data = null; }
                                                        }
                                                        else if (string.Compare(Subclave, "xPos", true) == 0)
                                                        {
                                                            try { Chunk_X = (int)Nodo_Compuesto[Subclave].ToTagInt(); }
                                                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Chunk_X = long.MinValue; }
                                                        }
                                                        else if (string.Compare(Subclave, "zPos", true) == 0)
                                                        {
                                                            try { Chunk_Z = (int)Nodo_Compuesto[Subclave].ToTagInt(); }
                                                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Chunk_Z = long.MinValue; }
                                                        }
                                                    }
                                                    if (Matriz_Bytes_Data == null || Matriz_Bytes_Data.Length < 32768) Matriz_Bytes_Data = new byte[32768];
                                                    if (Chunk_X > long.MinValue && Chunk_Z > long.MinValue && Matriz_Bytes_Blocks != null && Matriz_Bytes_Blocks.Length >= 32768)
                                                    {
                                                        ChunkRef Chunk = Chunks_Overworld.CreateChunk((int)Chunk_X, (int)Chunk_Z);
                                                        Chunk.IsLightPopulated = true; // For 1.13+ conversion support.
                                                        Chunk.IsTerrainPopulated = true;
                                                        Chunk.Blocks.AutoLight = false;
                                                        for (int Bloque_X = 0, Índice_Bloque = 0; Bloque_X < 16; Bloque_X++)
                                                        {
                                                            for (int Bloque_Z = 0; Bloque_Z < 16; Bloque_Z++)
                                                            {
                                                                for (int Bloque_Y = 0; Bloque_Y < 128; Bloque_Y++, Índice_Bloque++)
                                                                {
                                                                    Chunk.Blocks.SetBlock(Bloque_X, Bloque_Y, Bloque_Z, new AlphaBlock(Matriz_Bytes_Blocks[Índice_Bloque], Matriz_Bytes_Data[Índice_Bloque]));
                                                                }
                                                            }
                                                        }
                                                        AnvilChunk.Biomes_Jupisoft = new ZXByteArray(16, 16);
                                                        for (int Bloque_Z = 0; Bloque_Z < 16; Bloque_Z++)
                                                        {
                                                            for (int Bloque_X = 0; Bloque_X < 16; Bloque_X++)
                                                            {
                                                                AnvilChunk.Biomes_Jupisoft[Bloque_X, Bloque_Z] = 1; // Plains.
                                                            }
                                                        }
                                                        Chunk.Blocks.RebuildHeightMap();
                                                        Chunk.Blocks.RebuildSkyLight();
                                                        Chunk.Blocks.RebuildBlockLight();
                                                        Chunks_Overworld.Save();
                                                    }
                                                    Nodo_Compuesto = null;
                                                }
                                                break;
                                            }
                                        }
                                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                    }
                                    Árbol = null;
                                    Lector.Close();
                                    Lector.Dispose();
                                    Lector = null;
                                    GC.Collect(); // Recover RAM memory after every saved chunk file.
                                    GC.GetTotalMemory(true);
                                }
                            }
                        }
                        this.Invoke(new Invocación.Delegado_ProgressBar_Maximum(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Región, Total_Chunks });
                        Mundo.Save();
                        Mundo = null;
                        Program.Ejecutar_Ruta(Ruta, ProcessWindowStyle.Maximized);
                        SystemSounds.Asterisk.Play();
                    }
                }
            }
            catch (ThreadAbortException) { Subproceso_Abortado = true; } // Aborted, ignore this exception.
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally
            {
                try
                {
                    AnvilChunk.Biomes_Jupisoft = null; // Always reset the temporary biome array.
                    Pendiente_Subproceso_Abortar = false;
                    Subproceso_Activo = false;
                    Subproceso = null;
                    GC.Collect(); // Recover RAM memory at the end.
                    GC.GetTotalMemory(true);
                    if (!Subproceso_Abortado)
                    {
                        this.Invoke(new Invocación.Delegado_Control_Cursor(Invocación.Ejecutar_Delegado_Control_Cursor), new object[] { this, Cursors.Default });
                        // Reset all the progress bars.
                        this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [The original world files will never be modified]" });
                        this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Región, "Region progress: 0,0000 % (0 of 1.024 chunks)" });
                        this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Total, "Total progress: 0,0000 % (0 of 0 regions)" });
                        this.Invoke(new Invocación.Delegado_ProgressBar_Value(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Chunk, 0 });
                        this.Invoke(new Invocación.Delegado_ProgressBar_Value(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Región, 0 });
                        this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { Grupo_Ajustes, true });
                        this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { Tabla_Bloques, true });
                        this.Invoke(new Invocación.Delegado_ContextMenuStrip_Enabled(Invocación.Ejecutar_Delegado_ContextMenuStrip_Enabled), new object[] { Menú_Contextual, true });
                        this.Invoke(new Invocación.Delegado_Control_Select(Invocación.Ejecutar_Delegado_Control_Select), new object[] { Botón_Convertir });
                        this.Invoke(new Invocación.Delegado_Control_Focus(Invocación.Ejecutar_Delegado_Control_Focus), new object[] { Botón_Convertir });
                    }
                    else this.Invoke(new Invocación.Delegado_Form_Close(Invocación.Ejecutar_Delegado_Form_Close), new object[] { this }); // Close the window.
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            }
        }

        /// <summary>
        /// Thread function that converts the overworld dimension from any Indev world.
        /// </summary>
        internal void Subproceso_Indev_DoWork()
        {
            bool Subproceso_Abortado = false; // Used to know if the window must be closed.
            try
            {
                Subproceso_Activo = true;
                Stopwatch Cronómetro_Total = Stopwatch.StartNew();
                Texto_Bloques_Desconocidos = null;
                Diccionario_Bloques_Desconocidos.Clear();
                Diccionario_Bloques_Obsoletos.Clear();
                ////Lista_Propiedades_Únicas.Clear(); // 2018_10_08_12_25_04_362
                //Lista_Propiedades_Ejemplos.Clear();
                this.Invoke(new Invocación.Delegado_Control_Cursor(Invocación.Ejecutar_Delegado_Control_Cursor), new object[] { this, Cursors.WaitCursor });
                //Stopwatch Cronómetro_Región = new Stopwatch();
                //Stopwatch Cronómetro_Chunk = new Stopwatch();
                // Start the progress bars.
                int Progreso_Chunk = 32768;
                int Progreso_Total = 0;
                int Total_Chunks = 0;
                this.Invoke(new Invocación.Delegado_ProgressBar_Maximum(Invocación.Ejecutar_Delegado_ProgressBar_Maximum), new object[] { Barra_Progreso_Chunk, Progreso_Chunk });
                this.Invoke(new Invocación.Delegado_ProgressBar_Maximum(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Chunk, Progreso_Chunk });
                this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Región, "Chunk progress: " + Program.Traducir_Número_Decimales_Redondear(((double)Progreso_Chunk * 100d) / 32768d, 4) + " % (" + Program.Traducir_Número(Progreso_Chunk) + " of 32.768 blocks)" });
                this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Total, "Total progress: " + Program.Traducir_Número_Decimales_Redondear(Total_Chunks != 0 ? (((double)Progreso_Total * 100d) / (double)Total_Chunks) : 0d, 4) + " % (" + Program.Traducir_Número(Progreso_Total) + " of " + Program.Traducir_Número(Total_Chunks) + (Total_Chunks != 1L ? " chunks)" : " chunk)") });

                string Ruta = Program.Ruta_Guardado_Minecraft + "\\" + Program.Obtener_Nombre_Temporal() + " 1_13_to_1_12";
                if (Directory.Exists(Ruta))
                {
                    this.Invoke(new Invocación.Delegado_IWin32Window_MessageBox(Invocación.Ejecutar_Delegado_IWin32Window_MessageBox), new object[] { this, "Somehow the directory name for the new Minecraft map already exists.\r\nPlease try it again if the system clock is running properly.\r\nPath: \"" + Ruta + "\".", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning });
                    Ruta = null;
                    return;
                }
                Program.Crear_Carpetas(Ruta);
                AnvilWorld Mundo = AnvilWorld.Create(Ruta);
                Mundo.Level.LevelName = Path.GetFileName(Ruta);
                Mundo.Level.UseMapFeatures = true; // ?
                //Mundo.Level.GeneratorOptions = "1;minecraft:bedrock"; // Not used for now.
                Mundo.Level.GameType = GameType.CREATIVE;
                //Mundo.Level.Spawn = new SpawnPoint((int)Información_Nivel.SpawnX, (int)Math.Min((Información_Nivel.SpawnY + 1L), 255L), (int)Información_Nivel.SpawnZ);
                Mundo.Level.Spawn = new SpawnPoint(0, 66, 0);
                Mundo.Level.AllowCommands = true; // Allow cheats.
                Mundo.Level.GameRules.DoMobSpawning = true; // Spawn mobs.
                Mundo.Level.GameRules.DoFireTick = false; // Prevent the new level to burn out.
                Mundo.Level.GameRules.MobGriefing = false; // Prevent the mobs to destroy anything.
                Mundo.Level.GameRules.KeepInventory = true; // Keep the player inventory.
                //Mundo.Level.RainTime = (int)Información_Nivel.RainTime;
                //Mundo.Level.IsRaining = Información_Nivel.Raining != 0L;
                Mundo.Level.Player = new Player();
                Mundo.Level.Player.Dimension = 0; //Posición_Jugador.Dimesión; // 0 = Overworld, -1 = Nether, +1 = The End.
                Mundo.Level.Player.Position = new Vector3();
                Mundo.Level.Player.Position.X = 0d;
                Mundo.Level.Player.Position.Y = 66d;
                Mundo.Level.Player.Position.Z = 0d;
                //Mundo.Level.Player.Position.X = (double)Posición_Jugador.X; // Try to spawn where the player was.
                //Mundo.Level.Player.Position.Y = (double)Posición_Jugador.Y;
                //Mundo.Level.Player.Position.Z = (double)Posición_Jugador.Z;
                Substrate.Orientation Orientación = new Substrate.Orientation();
                Orientación.Pitch = 45d; // -90º a +90º // 45º = Camera centered (looking into the horizon).
                Orientación.Yaw = -45d; // -180º a +180º // -45º = Camera rotation (looking at the southeast).
                Mundo.Level.Player.Rotation = Orientación;
                //Mundo.Level.Player.Spawn = new SpawnPoint((int)Información_Nivel.SpawnX, (int)Math.Min((Información_Nivel.SpawnY + 1L), 255L), (int)Información_Nivel.SpawnZ);
                Mundo.Level.Player.Spawn = new SpawnPoint(0, 66, 0);
                Mundo.Level.Player.Abilities.Flying = true; // Start with creative flight enabled.
                //Mundo.Level.RandomSeed = Información_Nivel.RandomSeed; // Copy the original seed.
                Mundo.Level.RandomSeed = 4L; //-1994576438L;
                //Mundo.Level.ThunderTime = (int)Información_Nivel.ThunderTime;
                //Mundo.Level.IsThundering = Información_Nivel.Thundering != 0L;

                IChunkManager Chunks_Overworld = Mundo.GetChunkManager(0);
                //BlockManager Bloques_Overworld = Mundo.GetBlockManager(0);

                FileStream Lector = new FileStream(Ruta_Mundo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                if (Lector != null && Lector.Length > 0L)
                {
                    NbtTree Árbol = null;
                    try
                    {
                        Lector.Seek(0L, SeekOrigin.Begin);
                        Árbol = new NbtTree();
                        Árbol.ReadFrom(Lector);
                    }
                    catch { Árbol = null; }
                    if (Árbol == null || Árbol.Root == null)
                    {
                        try
                        {
                            Lector.Seek(0L, SeekOrigin.Begin);
                            Árbol = new NbtTree();
                            Árbol.ReadFrom(new GZipStream(Lector, CompressionMode.Decompress));
                        }
                        catch { Árbol = null; }
                    }
                    if (Árbol == null || Árbol.Root == null)
                    {
                        try
                        {
                            Lector.Seek(0L, SeekOrigin.Begin);
                            Árbol = new NbtTree();
                            Árbol.ReadFrom(new ZlibStream(Lector, CompressionMode.Decompress));
                        }
                        catch { Árbol = null; }
                    }
                    if (Árbol == null || Árbol.Root == null)
                    {
                        try
                        {
                            Lector.Seek(0L, SeekOrigin.Begin);
                            Árbol = new NbtTree();
                            Árbol.ReadFrom(new DeflateStream(Lector, CompressionMode.Decompress));
                        }
                        catch { Árbol = null; }
                    }
                    if (Árbol != null && Árbol.Root != null && Árbol.Root.Keys != null && Árbol.Root.Keys.Count > 0)
                    {
                        foreach (string Clave in Árbol.Root.Keys)
                        {
                            try
                            {
                                if (Pendiente_Subproceso_Abortar)
                                {
                                    Pendiente_Subproceso_Abortar = false;
                                    Chunks_Overworld.Save(); // Save the part of the chunks already generated.
                                    Chunks_Overworld = null;
                                    //Bloques_Overworld = null;
                                    Mundo.Save(); // Save the part of the world already generated.
                                    Mundo = null;
                                    Subproceso_Abortado = true;
                                    return; // Cancel safely before time.
                                }
                                if (string.Compare(Clave, "Map", true) == 0)
                                {
                                    TagNodeCompound Nodo_Compuesto = Árbol.Root[Clave] as TagNodeCompound;
                                    if (Nodo_Compuesto != null && Nodo_Compuesto.Keys != null && Nodo_Compuesto.Keys.Count > 0)
                                    {
                                        int Dimensiones_X = int.MinValue;
                                        int Dimensiones_Y = int.MinValue;
                                        int Dimensiones_Z = int.MinValue;
                                        byte[] Matriz_Bytes_Blocks = null;
                                        byte[] Matriz_Bytes_Data = null;
                                        foreach (string Subclave in Nodo_Compuesto.Keys)
                                        {
                                            if (string.Compare(Subclave, "Width", true) == 0)
                                            {
                                                try { Dimensiones_X = (short)Nodo_Compuesto[Subclave].ToTagShort(); }
                                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Dimensiones_X = int.MinValue; }
                                            }
                                            else if (string.Compare(Subclave, "Height", true) == 0)
                                            {
                                                try { Dimensiones_Y = (short)Nodo_Compuesto[Subclave].ToTagShort(); }
                                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Dimensiones_Y = int.MinValue; }
                                            }
                                            else if (string.Compare(Subclave, "Length", true) == 0)
                                            {
                                                try { Dimensiones_Z = (short)Nodo_Compuesto[Subclave].ToTagShort(); }
                                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Dimensiones_Z = int.MinValue; }
                                            }
                                            else if (string.Compare(Subclave, "Blocks", true) == 0) // 32.768 bytes (maximum height = 127).
                                            {
                                                try { Matriz_Bytes_Blocks = (byte[])Nodo_Compuesto[Subclave].ToTagByteArray(); }
                                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Matriz_Bytes_Blocks = null; }
                                            }
                                            else if (string.Compare(Subclave, "Data", true) == 0) // 32.768 bytes (maximum height = 127).
                                            {
                                                try { Matriz_Bytes_Data = (byte[])Nodo_Compuesto[Subclave].ToTagByteArray(); }
                                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Matriz_Bytes_Data = null; }
                                            }
                                        }
                                        if (Matriz_Bytes_Data == null || Matriz_Bytes_Data.Length <= 0) Matriz_Bytes_Data = new byte[Matriz_Bytes_Blocks.Length];
                                        if (Dimensiones_X > int.MinValue && Dimensiones_Y > int.MinValue && Dimensiones_Z > int.MinValue && Matriz_Bytes_Blocks != null && Matriz_Bytes_Blocks.Length > 0 && Matriz_Bytes_Data != null && Matriz_Bytes_Data.Length > 0)
                                        {
                                            for (int Índice_Byte = 0; Índice_Byte < Matriz_Bytes_Blocks.Length; Índice_Byte++)
                                            {
                                                if (Matriz_Bytes_Blocks[Índice_Byte] == 10) Matriz_Bytes_Blocks[Índice_Byte] = 11; // Change flowing lava with lava.
                                            }
                                            Total_Chunks = Dimensiones_X * Dimensiones_Y * Dimensiones_Z;
                                            ChunkRef[,] Matriz_Chunks = new ChunkRef[(Dimensiones_X / 16) + 2, (Dimensiones_Z / 16) + 2];
                                            for (int Chunk_Z = 0, Índice_Chunk_Z = 0; Chunk_Z < Dimensiones_Z + 32; Chunk_Z += 16, Índice_Chunk_Z++)
                                            {
                                                for (int Chunk_X = 0, Índice_Chunk_X = 0; Chunk_X < Dimensiones_X + 32; Chunk_X += 16, Índice_Chunk_X++)
                                                {
                                                    if (Pendiente_Subproceso_Abortar)
                                                    {
                                                        Pendiente_Subproceso_Abortar = false;
                                                        Chunks_Overworld.Save(); // Save the part of the chunks already generated.
                                                        Chunks_Overworld = null;
                                                        //Bloques_Overworld = null;
                                                        Mundo.Save(); // Save the part of the world already generated.
                                                        Mundo = null;
                                                        Subproceso_Abortado = true;
                                                        return; // Cancel safely before time.
                                                    }
                                                    Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z] = Chunks_Overworld.CreateChunk(Índice_Chunk_X, Índice_Chunk_Z);
                                                    Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].IsLightPopulated = true; // For 1.13+ conversion support.
                                                    Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].IsTerrainPopulated = true;
                                                    Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Blocks.AutoLight = false;
                                                }
                                            }
                                            for (int Chunk_Y = 0, Índice_Bloque = 0; Chunk_Y < Dimensiones_Y; Chunk_Y++)
                                            {
                                                for (int Chunk_Z = 0, Índice_Chunk_Z = 0; Chunk_Z < Dimensiones_Z; Chunk_Z++)
                                                {
                                                    if (Chunk_Z % 16 == 0) Índice_Chunk_Z++;
                                                    for (int Chunk_X = 0, Índice_Chunk_X = 0; Chunk_X < Dimensiones_X; Chunk_X++, Índice_Bloque++)
                                                    {
                                                        if (Pendiente_Subproceso_Abortar)
                                                        {
                                                            Pendiente_Subproceso_Abortar = false;
                                                            Chunks_Overworld.Save(); // Save the part of the chunks already generated.
                                                            Chunks_Overworld = null;
                                                            //Bloques_Overworld = null;
                                                            Mundo.Save(); // Save the part of the world already generated.
                                                            Mundo = null;
                                                            Subproceso_Abortado = true;
                                                            return; // Cancel safely before time.
                                                        }
                                                        if (Chunk_X % 16 == 0) Índice_Chunk_X++;
                                                        Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Blocks.SetBlock(Chunk_X % 16, Chunk_Y, Chunk_Z % 16, new AlphaBlock(Matriz_Bytes_Blocks[Índice_Bloque], Matriz_Bytes_Data[Índice_Bloque]));
                                                        /*for (int Bloque_Z = 0; Bloque_Z < 16; Bloque_Z++)
                                                        {
                                                            for (int Bloque_X = 0; Bloque_X < 16; Bloque_X++, Índice_Bloque++)
                                                            {
                                                                Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Blocks.SetBlock(Bloque_X, Chunk_Y, Bloque_Z, new AlphaBlock(Matriz_Bytes_Blocks[Índice_Bloque], Matriz_Bytes_Data[Índice_Bloque]));
                                                            }
                                                        }*/
                                                    }
                                                }
                                                //Progreso_Total += Dimensiones_X * Dimensiones_Z;
                                                //this.Invoke(new Invocación.Delegado_ProgressBar_Maximum(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Región, Progreso_Total });
                                                //this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Total, "Total progress: " + Program.Traducir_Número_Decimales_Redondear(((double)Progreso_Total * 100d) / (double)Total_Chunks, 4) + " % (" + Program.Traducir_Número(Progreso_Total) + " of " + Program.Traducir_Número(Total_Chunks) + (Total_Chunks != 1L ? " chunks)" : " chunk)") });
                                            }
                                            for (int Chunk_Z = 0, Índice_Chunk_Z = 0; Chunk_Z < Dimensiones_Z + 32; Chunk_Z += 16, Índice_Chunk_Z++)
                                            {
                                                for (int Chunk_X = 0, Índice_Chunk_X = 0; Chunk_X < Dimensiones_X + 32; Chunk_X += 16, Índice_Chunk_X++)
                                                {
                                                    if (Pendiente_Subproceso_Abortar)
                                                    {
                                                        Pendiente_Subproceso_Abortar = false;
                                                        Chunks_Overworld.Save(); // Save the part of the chunks already generated.
                                                        Chunks_Overworld = null;
                                                        //Bloques_Overworld = null;
                                                        Mundo.Save(); // Save the part of the world already generated.
                                                        Mundo = null;
                                                        Subproceso_Abortado = true;
                                                        return; // Cancel safely before time.
                                                    }
                                                    AnvilChunk.Biomes_Jupisoft = new ZXByteArray(16, 16);
                                                    for (int Bloque_Z = 0; Bloque_Z < 16; Bloque_Z++)
                                                    {
                                                        for (int Bloque_X = 0; Bloque_X < 16; Bloque_X++)
                                                        {
                                                            AnvilChunk.Biomes_Jupisoft[Bloque_X, Bloque_Z] = 1; // Plains.
                                                        }
                                                    }
                                                    Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Blocks.RebuildHeightMap();
                                                    Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Blocks.RebuildSkyLight();
                                                    Matriz_Chunks[Índice_Chunk_X, Índice_Chunk_Z].Blocks.RebuildBlockLight();
                                                    Chunks_Overworld.Save();
                                                }
                                            }
                                            Matriz_Chunks = null;
                                        }
                                        Nodo_Compuesto = null;
                                    }
                                    break;
                                }
                            }
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                        }
                        Árbol = null;
                        Lector.Close();
                        Lector.Dispose();
                        Lector = null;
                        GC.Collect(); // Recover RAM memory after every saved chunk file.
                        GC.GetTotalMemory(true);
                    }
                }
                this.Invoke(new Invocación.Delegado_ProgressBar_Maximum(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Región, Total_Chunks });
                Mundo.Save();
                Mundo = null;
                Program.Ejecutar_Ruta(Ruta, ProcessWindowStyle.Maximized);
                SystemSounds.Asterisk.Play();
            }
            catch (ThreadAbortException) { Subproceso_Abortado = true; } // Aborted, ignore this exception.
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally
            {
                try
                {
                    AnvilChunk.Biomes_Jupisoft = null; // Always reset the temporary biome array.
                    Pendiente_Subproceso_Abortar = false;
                    Subproceso_Activo = false;
                    Subproceso = null;
                    GC.Collect(); // Recover RAM memory at the end.
                    GC.GetTotalMemory(true);
                    if (!Subproceso_Abortado)
                    {
                        this.Invoke(new Invocación.Delegado_Control_Cursor(Invocación.Ejecutar_Delegado_Control_Cursor), new object[] { this, Cursors.Default });
                        // Reset all the progress bars.
                        this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [The original world files will never be modified]" });
                        this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Región, "Region progress: 0,0000 % (0 of 1.024 chunks)" });
                        this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Total, "Total progress: 0,0000 % (0 of 0 regions)" });
                        this.Invoke(new Invocación.Delegado_ProgressBar_Value(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Chunk, 0 });
                        this.Invoke(new Invocación.Delegado_ProgressBar_Value(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Región, 0 });
                        this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { Grupo_Ajustes, true });
                        this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { Tabla_Bloques, true });
                        this.Invoke(new Invocación.Delegado_ContextMenuStrip_Enabled(Invocación.Ejecutar_Delegado_ContextMenuStrip_Enabled), new object[] { Menú_Contextual, true });
                        this.Invoke(new Invocación.Delegado_Control_Select(Invocación.Ejecutar_Delegado_Control_Select), new object[] { Botón_Convertir });
                        this.Invoke(new Invocación.Delegado_Control_Focus(Invocación.Ejecutar_Delegado_Control_Focus), new object[] { Botón_Convertir });
                    }
                    else this.Invoke(new Invocación.Delegado_Form_Close(Invocación.Ejecutar_Delegado_Form_Close), new object[] { this }); // Close the window.
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            }
        }
    }
}
