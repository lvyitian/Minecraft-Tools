using Minecraft_Tools.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Buscador_Estructuras_Dobles : Form
    {
        public Ventana_Buscador_Estructuras_Dobles()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Enumeration that holds up the names of the 1.13+ Minecraft structures.
        /// </summary>
        internal enum Estructuras : int
        {
            Buried_treasure,
            Desert_pyramid,
            End_city,
            Fortress,
            Igloo,
            Jungle_pyramid,
            Mansion,
            Mineshaft,
            Monument,
            Ocean_ruin,
            Shipwreck,
            Slime_chunks,
            Stronghold,
            Swamp_hut,
            Village
        }

        /// <summary>
        /// Enumeration that holds up the names of the 1.13+ Minecraft biomes.
        /// </summary>
        internal enum Biomas : int
        {
            /// <summary>
            /// Used to assume the valid ones for the current structure are selected.
            /// </summary>
            All = -1,
            Ocean = 0,
            Plains = 1,
            Desert = 2,
            Mountains = 3,
            Forest = 4,
            Taiga = 5,
            Swamp = 6,
            River = 7,
            Nether = 8,
            The_end = 9,
            Frozen_ocean = 10,
            Frozen_river = 11,
            Snowy_tundra = 12,
            Snowy_mountains = 13,
            Mushroom_fields = 14,
            Mushroom_field_shore = 15,
            Beach = 16,
            Desert_hills = 17,
            Wooded_hills = 18,
            Taiga_hills = 19,
            Mountain_edge = 20,
            Jungle = 21,
            Jungle_hills = 22,
            Jungle_edge = 23,
            Deep_ocean = 24,
            Stone_shore = 25,
            Snowy_beach = 26,
            Birch_forest = 27,
            Birch_forest_hills = 28,
            Dark_forest = 29,
            Snowy_taiga = 30,
            Snowy_taiga_hills = 31,
            Giant_tree_taiga = 32,
            Giant_tree_taiga_hills = 33,
            Wooded_mountains = 34,
            Savanna = 35,
            Savanna_plateau = 36,
            Badlands = 37,
            Wooded_badlands_plateau = 38,
            Badlands_plateau = 39,
            Small_end_islands = 40,
            End_midlands = 41,
            End_highlands = 42,
            End_barrens = 43,
            Warm_ocean = 44,
            Lukewarm_ocean = 45,
            Cold_ocean = 46,
            Deep_warm_ocean = 47,
            Deep_lukewarm_ocean = 48,
            Deep_cold_ocean = 49,
            Deep_frozen_ocean = 50,
            The_void = 127,
            Sunflower_plains = 129,
            Desert_lakes = 130,
            Gravelly_mountains = 131,
            Flower_forest = 132,
            Taiga_mountains = 133,
            Swamp_hills = 134,
            Ice_spikes = 140,
            Modified_jungle = 149,
            Modified_jungle_edge = 151,
            Tall_birch_forest = 155,
            Tall_birch_hills = 156,
            Dark_forest_hills = 157,
            Snowy_taiga_mountains = 159,
            Giant_spruce_taiga = 160,
            Giant_spruce_taiga_hills = 161,
            Modified_gravelly_mountains = 162,
            Shattered_savanna = 163,
            Shattered_savanna_plateau = 164,
            Eroded_badlands = 165,
            Modified_wooded_badlands_plateau = 166,
            Modified_badlands_plateau = 167
        }

        internal long Variable_Semilla = 0L;
        internal Bitmap Variable_Imagen_Estructura = Resources.Estructura_Monumento.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
        internal Estructuras Variable_Estructura = Estructuras.Monument;
        //internal Biomas Variable_Bioma = Biomas.All;
        internal int Variable_Separación_Máxima = 8; // 5 * 16 = 80 blocks.

        internal readonly string Texto_Título = "Double Structures Finder by Jupisoft for " + Program.Texto_Usuario;
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

        private void Ventana_Buscador_Estructuras_Dobles_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título + " - [Middle click to copy the teleport coordinates of a structure]";
                this.WindowState = FormWindowState.Maximized;
                Ocupado = true;
                Numérico_Semilla.Minimum = long.MinValue;
                Numérico_Semilla.Maximum = long.MaxValue;
                int[] Matriz_Valores = Enum.GetValues(typeof(Estructuras)) as int[];
                if (Matriz_Valores != null && Matriz_Valores.Length > 0)
                {
                    if (Matriz_Valores.Length > 1) Array.Sort(Matriz_Valores);
                    foreach (int Valor in Matriz_Valores)
                    {
                        ComboBox_Estructura.Items.Add(((Estructuras)Valor).ToString().Replace('_', ' '));
                    }
                }
                Matriz_Valores = Enum.GetValues(typeof(Biomas)) as int[];
                if (Matriz_Valores != null && Matriz_Valores.Length > 0)
                {
                    if (Matriz_Valores.Length > 1) Array.Sort(Matriz_Valores);
                    foreach (int Valor in Matriz_Valores)
                    {
                        ComboBox_Bioma.Items.Add(((Biomas)Valor).ToString().Replace('_', ' '));
                    }
                }
                Matriz_Valores = null;
                Picture_Estructura.Image = Variable_Imagen_Estructura;
                if (ComboBox_Estructura.Items.Count > 0) ComboBox_Estructura.Text = Variable_Estructura.ToString().Replace('_', ' ');
                Biomas Bioma = Biomas.Deep_ocean;
                int Índice_Bioma = (int)Bioma;
                if (Minecraft.Diccionario_Biomas_Colores.ContainsKey(Índice_Bioma))
                {
                    Picture_Bioma.BackColor = Minecraft.Diccionario_Biomas_Colores[Índice_Bioma];
                }
                else Picture_Bioma.BackColor = Color.White;
                if (ComboBox_Bioma.Items.Count > 0) ComboBox_Bioma.Text = Bioma.ToString().Replace('_', ' ');
                NumericUpDown_Separación_Máxima.Value = Variable_Separación_Máxima;
                DataGridView_Principal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                DataGridView_Principal.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                DataGridView_Principal.Sort(Columna_Separación_Media_Centro, ListSortDirection.Ascending);
                //DataGridView_Principal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                //DataGridView_Principal.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                Registro_Cargar_Opciones();
                Ocupado = false;
                Buscar_Estructuras_Dobles();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Buscador_Estructuras_Dobles_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Buscador_Estructuras_Dobles_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Buscador_Estructuras_Dobles_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Buscador_Estructuras_Dobles_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Buscador_Estructuras_Dobles_DragDrop(object sender, DragEventArgs e)
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
                                    Minecraft.Información_Niveles Información_Nivel = Minecraft.Información_Niveles.Obtener_Información_Nivel(Ruta);
                                    Numérico_Semilla.Value = Información_Nivel.RandomSeed;
                                    return;
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

        private void Ventana_Buscador_Estructuras_Dobles_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Buscador_Estructuras_Dobles_KeyDown(object sender, KeyEventArgs e)
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

        private void Numérico_Semilla_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Numérico_Semilla.Refresh();
                Variable_Semilla = (long)Numérico_Semilla.Value;
                if (!Ocupado)
                {
                    Buscar_Estructuras_Dobles();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_Semilla_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TextBox_Semilla.Text))
                {
                    try { Numérico_Semilla.Value = Ventana_Calculadora_Infinita_Semillas_Mundos.Calcular_Semilla(TextBox_Semilla.Text); }
                    catch { Numérico_Semilla.Value = 0m; }
                }
                else Numérico_Semilla.Value = 0m;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Estructura_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Estructura.SelectedIndex > -1 && !Ocupado)
                {
                    Ocupado = true;
                    Variable_Estructura = (Estructuras)Enum.Parse(typeof(Estructuras), ComboBox_Estructura.Text.Replace(' ', '_'), true);
                    Biomas Bioma = Biomas.All;
                    if (Variable_Estructura == Estructuras.Buried_treasure)
                    {
                        Variable_Imagen_Estructura = Resources.Estructura_Tesoro.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                        Bioma = Biomas.Beach;
                    }
                    else if (Variable_Estructura == Estructuras.Desert_pyramid)
                    {
                        Variable_Imagen_Estructura = Resources.Estructura_Pirámide.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                        Bioma = Biomas.Desert;
                    }
                    else if (Variable_Estructura == Estructuras.End_city)
                    {
                        Variable_Imagen_Estructura = Resources.Estructura_Ciudad_Elytra.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                        Bioma = Biomas.The_end;
                    }
                    else if (Variable_Estructura == Estructuras.Fortress)
                    {
                        Variable_Imagen_Estructura = Resources.Estructura_Fortaleza.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                        Bioma = Biomas.Nether;
                    }
                    else if (Variable_Estructura == Estructuras.Igloo)
                    {
                        Variable_Imagen_Estructura = Resources.Estructura_Iglú_Sótano.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                        Bioma = Biomas.Snowy_tundra;
                    }
                    else if (Variable_Estructura == Estructuras.Jungle_pyramid)
                    {
                        Variable_Imagen_Estructura = Resources.Estructura_Templo.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                        Bioma = Biomas.Jungle;
                    }
                    else if (Variable_Estructura == Estructuras.Mansion)
                    {
                        Variable_Imagen_Estructura = Resources.Estructura_Mansión.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                        Bioma = Biomas.Dark_forest;
                    }
                    else if (Variable_Estructura == Estructuras.Mineshaft)
                    {
                        Variable_Imagen_Estructura = Resources.Estructura_Mina.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                        Bioma = Biomas.All;
                    }
                    else if (Variable_Estructura == Estructuras.Monument)
                    {
                        Variable_Imagen_Estructura = Resources.Estructura_Monumento.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                        Bioma = Biomas.Deep_ocean;
                    }
                    else if (Variable_Estructura == Estructuras.Ocean_ruin)
                    {
                        Variable_Imagen_Estructura = Resources.Estructura_Ruinas.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                        Bioma = Biomas.Lukewarm_ocean;
                    }
                    else if (Variable_Estructura == Estructuras.Shipwreck)
                    {
                        Variable_Imagen_Estructura = Resources.Estructura_Barco_Mapa.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                        Bioma = Biomas.Ocean;
                    }
                    else if (Variable_Estructura == Estructuras.Stronghold)
                    {
                        Variable_Imagen_Estructura = Resources.Estructura_Fin.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                        Bioma = Biomas.All;
                    }
                    else if (Variable_Estructura == Estructuras.Swamp_hut)
                    {
                        Variable_Imagen_Estructura = Resources.Estructura_Cabaña.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                        Bioma = Biomas.Swamp;
                    }
                    else if (Variable_Estructura == Estructuras.Village)
                    {
                        Variable_Imagen_Estructura = Resources.Estructura_Aldea.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                        Bioma = Biomas.Plains;
                    }
                    else
                    {
                        Variable_Imagen_Estructura = Resources.minecraft_slime_block.Clone() as Bitmap;
                        Bioma = Biomas.Swamp;
                    }
                    Picture_Estructura.Image = Variable_Imagen_Estructura;
                    Picture_Estructura.Refresh();
                    int Índice_Bioma = (int)Bioma;
                    if (Minecraft.Diccionario_Biomas_Colores.ContainsKey(Índice_Bioma))
                    {
                        Picture_Bioma.BackColor = Minecraft.Diccionario_Biomas_Colores[Índice_Bioma];
                    }
                    else Picture_Bioma.BackColor = Color.White;
                    ComboBox_Bioma.Text = Bioma.ToString().Replace('_', ' ');
                    Ocupado = false;
                    Buscar_Estructuras_Dobles();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Separación_Máxima_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown_Separación_Máxima.Refresh();
                Variable_Separación_Máxima = (int)NumericUpDown_Separación_Máxima.Value;
                Buscar_Estructuras_Dobles();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void DataGridView_Principal_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                Depurador.Escribir_Excepción(e.Exception != null ? e.Exception.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                e.ThrowException = false;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void DataGridView_Principal_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    if (e.ColumnIndex > -1 && e.ColumnIndex < DataGridView_Principal.Columns.Count && e.RowIndex > -1 && e.RowIndex < DataGridView_Principal.Rows.Count)
                    {
                        string Nombre = null; // Supports 3 different coordinates to copy to the clipboard.
                        if (e.ColumnIndex == 0 || e.ColumnIndex >= 5) Nombre = "/tp " + DataGridView_Principal[Columna_Separación_Media_X.Index, e.RowIndex].Value.ToString() + " 96 " + DataGridView_Principal[Columna_Separación_Media_Z.Index, e.RowIndex].Value.ToString();
                        else if (e.ColumnIndex <= 2) Nombre = "/tp " + DataGridView_Principal[Columna_Estructura_1_X.Index, e.RowIndex].Value.ToString() + " 96 " + DataGridView_Principal[Columna_Estructura_1_Z.Index, e.RowIndex].Value.ToString();
                        else if (e.ColumnIndex <= 4) Nombre = "/tp " + DataGridView_Principal[Columna_Estructura_2_X.Index, e.RowIndex].Value.ToString() + " 96 " + DataGridView_Principal[Columna_Estructura_2_Z.Index, e.RowIndex].Value.ToString();
                        if (!string.IsNullOrEmpty(Nombre))
                        {
                            Clipboard.SetText(Nombre);
                            DataGridView_Principal.CurrentCell = DataGridView_Principal[Columna_Separación_Media.Index, e.RowIndex];
                            SystemSounds.Asterisk.Play();
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void DataGridView_Principal_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Right)
                {
                    DataGridView.HitTestInfo Info = DataGridView_Principal.HitTest(e.X, e.Y);
                    if (Info.Type == DataGridViewHitTestType.None)
                    {
                        DataGridView_Principal.ClearSelection();
                        DataGridView_Principal.CurrentCell = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Puts the World Random seed to a specific state dependant on the inputs.
        /// </summary>
        /// <param name="p_72843_1_"></param>
        /// <param name="p_72843_2_"></param>
        /// <param name="p_72843_3_"></param>
        /// <returns></returns>
        internal Program.Random_Java setRandomSeed(int p_72843_1_, int p_72843_2_, int p_72843_3_)
        {
            long i = (long)p_72843_1_ * 341873128712L + (long)p_72843_2_ * 132897987541L + Variable_Semilla + (long)p_72843_3_;
            return new Program.Random_Java((ulong)i);
        }

        /// <summary>
        /// Checks given Chunk's Biomes against List of allowed ones.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <param name="radius"></param>
        /// <param name="allowed"></param>
        /// <returns></returns>
        internal bool areBiomesViable(/*int x, int z, int radius, List<Biome> allowed*/)
        {
            return true;
        }

        internal bool canSpawnDesertPyramidAtCoords(int chunkX, int chunkZ)
        {
            int maxDistanceBetweenScatteredFeatures = 32;
            //int minDistanceBetweenScatteredFeatures = 8;
            int i = chunkX;
            int j = chunkZ;
            if (chunkX < 0) chunkX -= maxDistanceBetweenScatteredFeatures - 1;
            if (chunkZ < 0) chunkZ -= maxDistanceBetweenScatteredFeatures - 1;
            int k = chunkX / maxDistanceBetweenScatteredFeatures;
            int l = chunkZ / maxDistanceBetweenScatteredFeatures;
            Program.Random_Java random = setRandomSeed(k, l, 14357617);
            k = k * maxDistanceBetweenScatteredFeatures;
            l = l * maxDistanceBetweenScatteredFeatures;
            k = k + random.nextInt(maxDistanceBetweenScatteredFeatures - 8);
            l = l + random.nextInt(maxDistanceBetweenScatteredFeatures - 8);
            random = null;
            if (i == k && j == l)
            {
                return true;
                //List<Biomas> Lista_Biomas = new List<Biomas>(new Biomas[] { Biomas.Desert, Biomas.Desert_hills });
                //return Variable_Bioma == Biomas.All || Lista_Biomas.Contains(Variable_Bioma);
                /*Biome biome = this.worldObj.getBiomeProvider().getBiome(new BlockPos(i * 16 + 8, 0, j * 16 + 8));
                if (biome == null) return false;
                for (Biome biome1 : BIOMELIST)
                {
                    if (biome == biome1) return true;
                }*/
            }
            return false;
        }

        internal bool canSpawnIglooAtCoords(int chunkX, int chunkZ)
        {
            int maxDistanceBetweenScatteredFeatures = 32;
            //int minDistanceBetweenScatteredFeatures = 8;
            int i = chunkX;
            int j = chunkZ;
            if (chunkX < 0) chunkX -= maxDistanceBetweenScatteredFeatures - 1;
            if (chunkZ < 0) chunkZ -= maxDistanceBetweenScatteredFeatures - 1;
            int k = chunkX / maxDistanceBetweenScatteredFeatures;
            int l = chunkZ / maxDistanceBetweenScatteredFeatures;
            Program.Random_Java random = setRandomSeed(k, l, 14357617);
            k = k * maxDistanceBetweenScatteredFeatures;
            l = l * maxDistanceBetweenScatteredFeatures;
            k = k + random.nextInt(maxDistanceBetweenScatteredFeatures - 8);
            l = l + random.nextInt(maxDistanceBetweenScatteredFeatures - 8);
            random = null;
            if (i == k && j == l)
            {
                return true;
                //List<Biomas> Lista_Biomas = new List<Biomas>(new Biomas[] { Biomas.Snowy_tundra, Biomas.Snowy_taiga });
                //return Variable_Bioma == Biomas.All || Lista_Biomas.Contains(Variable_Bioma);
                /*Biome biome = this.worldObj.getBiomeProvider().getBiome(new BlockPos(i * 16 + 8, 0, j * 16 + 8));
                if (biome == null) return false;
                for (Biome biome1 : BIOMELIST)
                {
                    if (biome == biome1) return true;
                }*/
            }
            return false;
        }

        internal bool canSpawnJunglePyramidAtCoords(int chunkX, int chunkZ)
        {
            int maxDistanceBetweenScatteredFeatures = 32;
            //int minDistanceBetweenScatteredFeatures = 8;
            int i = chunkX;
            int j = chunkZ;
            if (chunkX < 0) chunkX -= maxDistanceBetweenScatteredFeatures - 1;
            if (chunkZ < 0) chunkZ -= maxDistanceBetweenScatteredFeatures - 1;
            int k = chunkX / maxDistanceBetweenScatteredFeatures;
            int l = chunkZ / maxDistanceBetweenScatteredFeatures;
            Program.Random_Java random = setRandomSeed(k, l, 14357617);
            k = k * maxDistanceBetweenScatteredFeatures;
            l = l * maxDistanceBetweenScatteredFeatures;
            k = k + random.nextInt(maxDistanceBetweenScatteredFeatures - 8);
            l = l + random.nextInt(maxDistanceBetweenScatteredFeatures - 8);
            random = null;
            if (i == k && j == l)
            {
                return true;
                //List<Biomas> Lista_Biomas = new List<Biomas>(new Biomas[] { Biomas.Jungle, Biomas.Jungle_hills });
                //return Variable_Bioma == Biomas.All || Lista_Biomas.Contains(Variable_Bioma);
                /*Biome biome = this.worldObj.getBiomeProvider().getBiome(new BlockPos(i * 16 + 8, 0, j * 16 + 8));
                if (biome == null) return false;
                for (Biome biome1 : BIOMELIST)
                {
                    if (biome == biome1) return true;
                }*/
            }
            return false;
        }

        internal bool canSpawnSwampHutAtCoords(int chunkX, int chunkZ)
        {
            int maxDistanceBetweenScatteredFeatures = 32;
            //int minDistanceBetweenScatteredFeatures = 8;
            int i = chunkX;
            int j = chunkZ;
            if (chunkX < 0) chunkX -= maxDistanceBetweenScatteredFeatures - 1;
            if (chunkZ < 0) chunkZ -= maxDistanceBetweenScatteredFeatures - 1;
            int k = chunkX / maxDistanceBetweenScatteredFeatures;
            int l = chunkZ / maxDistanceBetweenScatteredFeatures;
            Program.Random_Java random = setRandomSeed(k, l, 14357617);
            k = k * maxDistanceBetweenScatteredFeatures;
            l = l * maxDistanceBetweenScatteredFeatures;
            k = k + random.nextInt(maxDistanceBetweenScatteredFeatures - 8);
            l = l + random.nextInt(maxDistanceBetweenScatteredFeatures - 8);
            random = null;
            if (i == k && j == l)
            {
                return true;
                //List<Biomas> Lista_Biomas = new List<Biomas>(new Biomas[] { Biomas.Swamp });
                //return Variable_Bioma == Biomas.All || Lista_Biomas.Contains(Variable_Bioma);
                /*Biome biome = this.worldObj.getBiomeProvider().getBiome(new BlockPos(i * 16 + 8, 0, j * 16 + 8));
                if (biome == null) return false;
                for (Biome biome1 : BIOMELIST)
                {
                    if (biome == biome1) return true;
                }*/
            }
            return false;
        }

        /*protected bool canSpawnFortressAtCoords(int chunkX, int chunkZ)
        {
            int i = chunkX >> 4;
            int j = chunkZ >> 4;
            Program.Random_Java random = new Program.Random_Java((long)(i ^ j << 4) ^ (long)Numérico_Semilla.Value);
            this.rand.nextInt();

            if (this.rand.nextInt(3) != 0)
            {
                return false;
            }
            else if (chunkX != (i << 4) + 4 + this.rand.nextInt(8))
            {
                return false;
            }
            else
            {
                return chunkZ == (j << 4) + 4 + this.rand.nextInt(8);
            }
        }*/

        internal bool canSpawnMansionAtCoords(int chunkX, int chunkZ)
        {
            int i = chunkX;
            int j = chunkZ;
            if (chunkX < 0) i = chunkX - 79;
            if (chunkZ < 0) j = chunkZ - 79;
            int k = i / 80;
            int l = j / 80;
            Program.Random_Java random = setRandomSeed(k, l, 10387319);
            k = k * 80;
            l = l * 80;
            k = k + (random.nextInt(60) + random.nextInt(60)) / 2;
            l = l + (random.nextInt(60) + random.nextInt(60)) / 2;
            random = null;
            if (chunkX == k && chunkZ == l)
            {
                return true;
                //List<Biomas> Lista_Biomas = new List<Biomas>(new Biomas[] { Biomas.Dark_forest, Biomas.Dark_forest_hills });
                //return Variable_Bioma == Biomas.All || Lista_Biomas.Contains(Variable_Bioma);
                //bool flag = areBiomesViable(chunkX * 16 + 8, chunkZ * 16 + 8, 32, field_191072_a);
                //if (flag) return true;
            }
            return false;
        }

        /*internal bool canSpawnMineshaftAtCoords(int chunkX, int chunkZ)
        {
            double chance = 0.004D;
            Program.Random_Java rand = new Program.Random_Java((ulong)Numérico_Semilla.Value);
            return rand.nextDouble() < chance && rand.nextInt(80) < Math.Max(Math.Abs(chunkX), Math.Abs(chunkZ));
        }*/

        internal bool canSpawnMonumentAtCoords(int chunkX, int chunkZ)
        {
            int spacing = 32;
            int separation = 5;
            int i = chunkX;
            int j = chunkZ;
            if (chunkX < 0) chunkX -= spacing - 1;
            if (chunkZ < 0) chunkZ -= spacing - 1;
            int k = chunkX / spacing;
            int l = chunkZ / spacing;
            Program.Random_Java random = setRandomSeed(k, l, 10387313);
            k = k * spacing;
            l = l * spacing;
            k = k + (random.nextInt(spacing - separation) + random.nextInt(spacing - separation)) / 2;
            l = l + (random.nextInt(spacing - separation) + random.nextInt(spacing - separation)) / 2;
            random = null;
            if (i == k && j == l)
            {
                return true;
                //List<Biomas> Lista_Biomas = new List<Biomas>(new Biomas[] { Biomas.Deep_ocean });
                //List<Biomas> Lista_Biomas = new List<Biomas>(new Biomas[] { Biomas.Ocean, Biomas.Deep_ocean, Biomas.River, Biomas.Frozen_ocean, Biomas.Frozen_river });
                //return Variable_Bioma == Biomas.All || Lista_Biomas.Contains(Variable_Bioma);
                /*if (!this.worldObj.getBiomeProvider().areBiomesViable(i * 16 + 8, j * 16 + 8, 16, SPAWN_BIOMES))
                {
                    return false;
                }
                boolean flag = this.worldObj.getBiomeProvider().areBiomesViable(i * 16 + 8, j * 16 + 8, 29, WATER_BIOMES);
                if (flag)
                {
                    return true;
                }*/
            }
            return false;
        }

        protected bool canSpawnStrongholdAtCoords(int chunkX, int chunkZ)
        {
            /*if (!this.ranBiomeCheck)
            {
                this.generatePositions();
                this.ranBiomeCheck = true;
            }
            for (ChunkPos chunkpos : this.structureCoords)
            {
                if (chunkX == chunkpos.chunkXPos && chunkZ == chunkpos.chunkZPos)
                {
                    return true;
                }
            }*/
            return false;
        }

        internal bool canSpawnVillageAtCoords(int chunkX, int chunkZ)
        {
            int distance = 32;
            //int minTownSeparation = 8;
            int i = chunkX;
            int j = chunkZ;
            if (chunkX < 0) chunkX -= distance - 1;
            if (chunkZ < 0) chunkZ -= distance - 1;
            int k = chunkX / distance;
            int l = chunkZ / distance;
            Program.Random_Java random = setRandomSeed(k, l, 10387312);
            k = k * distance;
            l = l * distance;
            k = k + random.nextInt(distance - 8);
            l = l + random.nextInt(distance - 8);
            random = null;
            if (i == k && j == l)
            {
                return true;
                //List<Biomas> Lista_Biomas = new List<Biomas>(new Biomas[] { Biomas.Plains, Biomas.Desert, Biomas.Savanna, Biomas.Taiga });
                //return Variable_Bioma == Biomas.All || Lista_Biomas.Contains(Variable_Bioma);
                //bool flag = areBiomesViable(chunkX * 16 + 8, chunkZ * 16 + 8, 32, field_191072_a);
                //if (flag) return true;
            }
            return false;
        }

        /// <summary>
        /// Function that searches for 2 structures of the same type that are located very close, so travelling between them might load the 2 at once (like double guardian farms, etc). This function doesn't predict the final vanilla biomes, so this will only work 100 % of the times with Minecraft 1.13+ using the world buffet feature and the correct biome for each desired structure. Of course in regular vanilla this could happenm, but it will be one of the rarest things ever in Minecraft, so meanwhile a tool that might help this might be "Amidst", since it can predict the biomes, so use it wih the seed and coordinates given by this tool to see if it might happen or not.
        /// </summary>
        internal void Buscar_Estructuras_Dobles()
        {
            try
            {
                if (!Ocupado)
                {
                    this.Cursor = Cursors.WaitCursor;
                    int Mínimo_XZ = -1500; // This will look from -24.000 to +24.000 in the X and Z axes.
                    int Máximo_XZ = 1500; // This will look a total of 9.000.000 chunks each time.
                    List<Point> Lista_Posiciones = new List<Point>();
                    if (Variable_Estructura == Estructuras.Buried_treasure)
                    {
                        /*for (int Chunk_Z = Mínimo_XZ; Chunk_Z <= Máximo_XZ; Chunk_Z++)
                        {
                            for (int Chunk_X = Mínimo_XZ; Chunk_X <= Máximo_XZ; Chunk_X++)
                            {
                                if (canSpawnBuriedTreasureAtCoords(Chunk_X, Chunk_Z))
                                {
                                    Lista_Posiciones.Add(new Point(Chunk_X, Chunk_Z));
                                }
                            }
                        }*/
                    }
                    else if (Variable_Estructura == Estructuras.Desert_pyramid)
                    {
                        for (int Chunk_Z = Mínimo_XZ; Chunk_Z <= Máximo_XZ; Chunk_Z++)
                        {
                            for (int Chunk_X = Mínimo_XZ; Chunk_X <= Máximo_XZ; Chunk_X++)
                            {
                                if (canSpawnDesertPyramidAtCoords(Chunk_X, Chunk_Z))
                                {
                                    Lista_Posiciones.Add(new Point(Chunk_X, Chunk_Z));
                                }
                            }
                        }
                    }
                    else if (Variable_Estructura == Estructuras.End_city)
                    {
                        /*for (int Chunk_Z = Mínimo_XZ; Chunk_Z <= Máximo_XZ; Chunk_Z++)
                        {
                            for (int Chunk_X = Mínimo_XZ; Chunk_X <= Máximo_XZ; Chunk_X++)
                            {
                                if (canSpawnEndCityAtCoords(Chunk_X, Chunk_Z))
                                {
                                    Lista_Posiciones.Add(new Point(Chunk_X, Chunk_Z));
                                }
                            }
                        }*/
                    }
                    else if (Variable_Estructura == Estructuras.Fortress)
                    {
                        /*for (int Chunk_Z = Mínimo_XZ; Chunk_Z <= Máximo_XZ; Chunk_Z++)
                        {
                            for (int Chunk_X = Mínimo_XZ; Chunk_X <= Máximo_XZ; Chunk_X++)
                            {
                                if (canSpawnFortressAtCoords(Chunk_X, Chunk_Z))
                                {
                                    Lista_Posiciones.Add(new Point(Chunk_X, Chunk_Z));
                                }
                            }
                        }*/
                    }
                    else if (Variable_Estructura == Estructuras.Igloo)
                    {
                        for (int Chunk_Z = Mínimo_XZ; Chunk_Z <= Máximo_XZ; Chunk_Z++)
                        {
                            for (int Chunk_X = Mínimo_XZ; Chunk_X <= Máximo_XZ; Chunk_X++)
                            {
                                if (canSpawnIglooAtCoords(Chunk_X, Chunk_Z))
                                {
                                    Lista_Posiciones.Add(new Point(Chunk_X, Chunk_Z));
                                }
                            }
                        }
                    }
                    else if (Variable_Estructura == Estructuras.Jungle_pyramid)
                    {
                        for (int Chunk_Z = Mínimo_XZ; Chunk_Z <= Máximo_XZ; Chunk_Z++)
                        {
                            for (int Chunk_X = Mínimo_XZ; Chunk_X <= Máximo_XZ; Chunk_X++)
                            {
                                if (canSpawnJunglePyramidAtCoords(Chunk_X, Chunk_Z))
                                {
                                    Lista_Posiciones.Add(new Point(Chunk_X, Chunk_Z));
                                }
                            }
                        }
                    }
                    else if (Variable_Estructura == Estructuras.Mansion)
                    {
                        for (int Chunk_Z = Mínimo_XZ; Chunk_Z <= Máximo_XZ; Chunk_Z++)
                        {
                            for (int Chunk_X = Mínimo_XZ; Chunk_X <= Máximo_XZ; Chunk_X++)
                            {
                                if (canSpawnMansionAtCoords(Chunk_X, Chunk_Z))
                                {
                                    Lista_Posiciones.Add(new Point(Chunk_X, Chunk_Z));
                                }
                            }
                        }
                    }
                    else if (Variable_Estructura == Estructuras.Mineshaft)
                    {
                        /*for (int Chunk_Z = Mínimo_XZ; Chunk_Z <= Máximo_XZ; Chunk_Z++)
                        {
                            for (int Chunk_X = Mínimo_XZ; Chunk_X <= Máximo_XZ; Chunk_X++)
                            {
                                if (canSpawnMineshaftAtCoords(Chunk_X, Chunk_Z))
                                {
                                    Lista_Posiciones.Add(new Point(Chunk_X, Chunk_Z));
                                }
                            }
                        }*/
                    }
                    else if (Variable_Estructura == Estructuras.Monument)
                    {
                        for (int Chunk_Z = Mínimo_XZ; Chunk_Z <= Máximo_XZ; Chunk_Z++)
                        {
                            for (int Chunk_X = Mínimo_XZ; Chunk_X <= Máximo_XZ; Chunk_X++)
                            {
                                if (canSpawnMonumentAtCoords(Chunk_X, Chunk_Z))
                                {
                                    Lista_Posiciones.Add(new Point(Chunk_X, Chunk_Z));
                                }
                            }
                        }
                    }
                    else if (Variable_Estructura == Estructuras.Ocean_ruin)
                    {
                        /*for (int Chunk_Z = Mínimo_XZ; Chunk_Z <= Máximo_XZ; Chunk_Z++)
                        {
                            for (int Chunk_X = Mínimo_XZ; Chunk_X <= Máximo_XZ; Chunk_X++)
                            {
                                if (canSpawnOceanRuinAtCoords(Chunk_X, Chunk_Z))
                                {
                                    Lista_Posiciones.Add(new Point(Chunk_X, Chunk_Z));
                                }
                            }
                        }*/
                    }
                    else if (Variable_Estructura == Estructuras.Shipwreck)
                    {
                        /*for (int Chunk_Z = Mínimo_XZ; Chunk_Z <= Máximo_XZ; Chunk_Z++)
                        {
                            for (int Chunk_X = Mínimo_XZ; Chunk_X <= Máximo_XZ; Chunk_X++)
                            {
                                if (canSpawnShipwreckAtCoords(Chunk_X, Chunk_Z))
                                {
                                    Lista_Posiciones.Add(new Point(Chunk_X, Chunk_Z));
                                }
                            }
                        }*/
                    }
                    else if (Variable_Estructura == Estructuras.Stronghold)
                    {
                        for (int Chunk_Z = Mínimo_XZ; Chunk_Z <= Máximo_XZ; Chunk_Z++)
                        {
                            for (int Chunk_X = Mínimo_XZ; Chunk_X <= Máximo_XZ; Chunk_X++)
                            {
                                if (canSpawnStrongholdAtCoords(Chunk_X, Chunk_Z))
                                {
                                    Lista_Posiciones.Add(new Point(Chunk_X, Chunk_Z));
                                }
                            }
                        }
                    }
                    else if (Variable_Estructura == Estructuras.Swamp_hut)
                    {
                        for (int Chunk_Z = Mínimo_XZ; Chunk_Z <= Máximo_XZ; Chunk_Z++)
                        {
                            for (int Chunk_X = Mínimo_XZ; Chunk_X <= Máximo_XZ; Chunk_X++)
                            {
                                if (canSpawnSwampHutAtCoords(Chunk_X, Chunk_Z))
                                {
                                    Lista_Posiciones.Add(new Point(Chunk_X, Chunk_Z));
                                }
                            }
                        }
                    }
                    else if (Variable_Estructura == Estructuras.Village)
                    {
                        for (int Chunk_Z = Mínimo_XZ; Chunk_Z <= Máximo_XZ; Chunk_Z++)
                        {
                            for (int Chunk_X = Mínimo_XZ; Chunk_X <= Máximo_XZ; Chunk_X++)
                            {
                                if (canSpawnVillageAtCoords(Chunk_X, Chunk_Z))
                                {
                                    Lista_Posiciones.Add(new Point(Chunk_X, Chunk_Z));
                                }
                            }
                        }
                    }
                    if (Lista_Posiciones.Count > 1)
                    {
                        //DataGridView_Principal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                        //DataGridView_Principal.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                        DataGridView_Principal.Rows.Clear();
                        // Use a rectangle to store 2 points for 2 different structure coordinates.
                        Dictionary<Rectangle, object> Diccionario_Estructuras = new Dictionary<Rectangle, object>();
                        foreach (Point Posición in Lista_Posiciones)
                        {
                            foreach (Point Posición_2 in Lista_Posiciones)
                            {
                                if (Posición != Posición_2)
                                {
                                    // Sort the coordinates to avoid repeating later the same 2 structures.
                                    Rectangle Rectángulo = (Posición.X + Posición.Y) < (Posición_2.X + Posición_2.Y) ? new Rectangle(Posición.X, Posición.Y, Posición_2.X, Posición_2.Y) : new Rectangle(Posición_2.X, Posición_2.Y, Posición.X, Posición.Y);
                                    if (!Diccionario_Estructuras.ContainsKey(Rectángulo)) // Structures not stored.
                                    {
                                        int Separación_X = Math.Abs(Posición.X - Posición_2.X); // Chunk X spacing.
                                        int Separación_Z = Math.Abs(Posición.Y - Posición_2.Y); // Chunk Z spacing.
                                        //int Separación_Media = (Separación_X + Separación_Z) / 2; // Average of XZ chunk spacing.
                                        int Separación_Máxima = Math.Max(Separación_X, Separación_Z); // Average of XZ chunk spacing.
                                        if (Separación_Máxima <= Variable_Separación_Máxima) // Only store if they are very close.
                                        //if (Separación_Media <= Variable_Separación_Máxima) // Only store if they are very close.
                                        {
                                            //Point Separación = new Point(Separación_X, Separación_Z);
                                            Diccionario_Estructuras.Add(Rectángulo, null);
                                            DataGridView_Principal.Rows.Add(new object[]
                                            {
                                                Variable_Imagen_Estructura,
                                                Rectángulo.X * 16,
                                                Rectángulo.Y * 16,
                                                Rectángulo.Width * 16,
                                                Rectángulo.Height * 16,
                                                (int)Math.Round((double)((Rectángulo.X + Rectángulo.Width) * 16) / 2d, MidpointRounding.AwayFromZero),
                                                (int)Math.Round((double)((Rectángulo.Y + Rectángulo.Height) * 16) / 2d, MidpointRounding.AwayFromZero),
                                                Separación_Máxima * 16,
                                                (int)Math.Round((double)(Separación_Máxima * 16) / 2d, MidpointRounding.AwayFromZero)
                                            }); // Add a new row with the 2 close structures.
                                        }
                                    }
                                    /*Point Posición = new Point((Rectángulo.X + Rectángulo_2.X) / 2, (Rectángulo.Y + Rectángulo_2.Y) / 2);
                                    if (!Lista_Posiciones.Contains(Posición))
                                    {
                                        Lista_Posiciones.Add(Posición);
                                    }*/
                                }
                            }
                        }
                        //DataGridView_Principal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        //DataGridView_Principal.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                        if (DataGridView_Principal.Rows.Count > 0)
                        {
                            this.Text = Texto_Título + " - [Double structures found: " + Program.Traducir_Número(DataGridView_Principal.Rows.Count) + "]";
                            DataGridView_Principal.Refresh();
                            DataGridView_Principal.Sort(DataGridView_Principal.SortedColumn, DataGridView_Principal.SortOrder != SortOrder.Descending ? ListSortDirection.Ascending : ListSortDirection.Descending);
                            DataGridView_Principal.CurrentCell = DataGridView_Principal[Columna_Separación_Media_Centro.Index, 0];
                            SystemSounds.Asterisk.Play();
                        }
                        Diccionario_Estructuras = null;
                        Lista_Posiciones = null;
                        /*if (Lista_Posiciones.Count > 0)
                        {
                            string Texto = null;
                            foreach (Point Posición in Lista_Posiciones)
                            {
                                Texto += Posición.ToString() + "\r\n";
                            }
                            MessageBox.Show(this, Texto, Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        Lista_Posiciones = null;*/
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
