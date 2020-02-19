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
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Buscador_Chunks_Limos_Estructuras : Form
    {
        public Ventana_Buscador_Chunks_Limos_Estructuras()
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
        
        internal Bitmap Variable_Imagen_Estructura = Resources.minecraft_slime_block.Clone() as Bitmap;
        internal Estructuras Variable_Estructura = Estructuras.Slime_chunks;
        internal Biomas Variable_Bioma = Biomas.All;

        internal static readonly HatchBrush Pincel_Trama = new HatchBrush(HatchStyle.Percent50, Color.FromArgb(255, 255, 128, 128), Color.Transparent);

        internal static bool Variable_Dibujar_Iconos = false;
        internal static bool Variable_Invertir_Colores = false;
        internal static bool Variable_Mostrar_Reglas = true;

        internal readonly string Texto_Título = "Slime Chunks and Structures Finder by Jupisoft for " + Program.Texto_Usuario;
        internal Stopwatch Cronómetro_Memoria = new Stopwatch(); // Turn the text red when over 4 GB
        internal bool Variable_Siempre_Visible = false;

        internal static int X = 0;
        internal static int Z = 0;
        internal static int Variable_Zoom = 16;

        internal bool Ocupado = false;

        internal int Separación_Regla = -1;

        private void Ventana_Buscador_Chunks_Limos_Estructuras_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                Ocupado = true;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                Picture.KeyDown += Ventana_Buscador_Chunks_Limos_Estructuras_KeyDown;
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
                if (Separación_Regla < 0)
                {
                    Bitmap Imagen = Program.Obtener_Imagen_Texto(int.MinValue.ToString(), Etiqueta_Semilla.Font, Color.White, Color.Black, TextRenderingHint.AntiAlias);
                    Rectangle Rectángulo = Program.Buscar_Zona_Recorte_Imagen(Imagen, Color.White);
                    Imagen.Dispose();
                    Imagen = null;
                    if (Rectángulo.X > -1 && Rectángulo.Y > -1 && Rectángulo.X < int.MaxValue && Rectángulo.Y < int.MaxValue && Rectángulo.Width > 0 && Rectángulo.Height > 0)
                    {
                        Separación_Regla = Rectángulo.Width + 19; // Contar 6 de espacio antes y después del texto y 1 barra de 1 de ancho y 6 más de espacio.
                        for (int Índice = 1; Índice < Separación_Regla * 2; Índice *= 2)
                        {
                            if (Índice >= Separación_Regla)
                            {
                                Separación_Regla = Índice;
                                break;
                            }
                        }

                        /*//Imagen.Save(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\origgg.png", ImageFormat.Png);
                        Imagen = Imagen.Clone(Rectángulo, PixelFormat.Format32bppArgb);
                        //Imagen.Save(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\origgg2.png", ImageFormat.Png);
                        //MessageBox.Show(Rectángulo.ToString());
                        if (Combo_Texto_Superior_Ángulo.SelectedIndex == 1) Imagen.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        if (Combo_Texto_Superior_Ángulo.SelectedIndex == 2) Imagen.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        if (Combo_Texto_Superior_Ángulo.SelectedIndex == 3) Imagen.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        if (CheckBox_Texto_Superior_Voltear_X.Checked) Imagen.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        if (CheckBox_Texto_Superior_Voltear_Y.Checked) Imagen.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        return Imagen;*/
                    }
                    else Separación_Regla = 256; // ¿Forzar si fallase?...

                    /*Bitmap Imagen_1_x_1 = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
                    Graphics Pintar_1_x_1 = Graphics.FromImage();

                    Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.None;
                    //Pintar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho_Original, Alto_Original), GraphicsUnit.Pixel);
                    Pintar.Dispose();
                    Pintar = null;*/
                }
                Numérico_Semilla.Minimum = long.MinValue;
                Numérico_Semilla.Maximum = long.MaxValue;

                Numérico_X_Bloque.Minimum = int.MinValue;
                Numérico_X_Bloque.Maximum = int.MaxValue;
                Numérico_Z_Bloque.Minimum = int.MinValue;
                Numérico_Z_Bloque.Maximum = int.MaxValue;

                Numérico_X_Chunk.Minimum = (int.MinValue / 16) - 1;
                Numérico_X_Chunk.Maximum = int.MaxValue / 16;
                Numérico_Z_Chunk.Minimum = (int.MinValue / 16) - 1;
                Numérico_Z_Chunk.Maximum = int.MaxValue / 16;

                Numérico_X_Región.Minimum = (int.MinValue / 512) - 1;
                Numérico_X_Región.Maximum = int.MaxValue / 512;
                Numérico_Z_Región.Minimum = (int.MinValue / 512) - 1;
                Numérico_Z_Región.Maximum = int.MaxValue / 512;

                Numérico_X_Bloque.Value = X;
                Numérico_Z_Bloque.Value = Z;
                ComboBox_Zoom.Text = Variable_Zoom.ToString() + "x";
                if (ComboBox_Estructura.Items.Count > 0) ComboBox_Estructura.Text = Variable_Estructura.ToString().Replace('_', ' ');
                if (ComboBox_Bioma.Items.Count > 0) ComboBox_Bioma.Text = Variable_Bioma.ToString().Replace('_', ' ');
                TextBox_Semilla.Text = Program.Texto_Usuario;
                Menú_Contextual_Dibujar_Iconos.Checked = Variable_Dibujar_Iconos;
                Menú_Contextual_Invertir_Colores.Checked = Variable_Invertir_Colores;
                Menú_Contextual_Mostrar_Reglas.Checked = Variable_Mostrar_Reglas;
                TextBox_Semilla.Select();
                TextBox_Semilla.Focus();
                TextBox_Semilla.SelectAll();
                Ocupado = false;
                Buscar_Chunks_Limos();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Buscador_Chunks_Limos_Estructuras_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Buscador_Chunks_Limos_Estructuras_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Buscador_Chunks_Limos_Estructuras_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Buscador_Chunks_Limos_Estructuras_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Buscador_Chunks_Limos_Estructuras_DragDrop(object sender, DragEventArgs e)
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

        private void Ventana_Buscador_Chunks_Limos_Estructuras_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                Buscar_Chunks_Limos();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Buscador_Chunks_Limos_Estructuras_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.A || e.KeyCode == Keys.D || e.KeyCode == Keys.W || e.KeyCode == Keys.S)
                {
                    if (!TextBox_Semilla.Focused)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        int Desplazamiento_Zoom = 1;
                        int Desplazamiento_X = e.KeyCode == Keys.A ? -Desplazamiento_Zoom : e.KeyCode == Keys.D ? Desplazamiento_Zoom : 0;
                        int Desplazamiento_Z = e.KeyCode == Keys.W ? -Desplazamiento_Zoom : e.KeyCode == Keys.S ? Desplazamiento_Zoom : 0;
                        if (e.Alt)
                        {
                            Desplazamiento_X *= 4;
                            Desplazamiento_Z *= 4;
                        }
                        if (e.Control)
                        {
                            Desplazamiento_X *= 8;
                            Desplazamiento_Z *= 8;
                        }
                        if (e.Shift)
                        {
                            Desplazamiento_X *= 16;
                            Desplazamiento_Z *= 16;
                        }
                        /*decimal Valor_X = */
                        Numérico_X_Chunk.Value += Desplazamiento_X;
                        /*decimal Valor_Z = */
                        Numérico_Z_Chunk.Value += Desplazamiento_Z;
                    }
                }
                else if (!e.Alt && !e.Control && !e.Shift)
                {
                    if (e.KeyCode == Keys.Escape) this.Close();
                    else if (e.KeyCode == Keys.Enter)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Temporizador_Principal_Tick(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    Program.Proceso.Refresh();
                    long Memoria_Bytes = Program.Proceso.PagedMemorySize64;
                    Barra_Estado_Etiqueta_Memoria.Text = "RAM: " + Program.Traducir_Tamaño_Bytes_Automático(Memoria_Bytes, 2, true);
                    if (Memoria_Bytes >= 4294967296L && !Cronómetro_Memoria.IsRunning) Cronómetro_Memoria.Restart();
                    else if (Memoria_Bytes < 4294967296L && Cronómetro_Memoria.IsRunning)
                    {
                        Cronómetro_Memoria.Reset();
                        Barra_Estado_Etiqueta_Memoria.ForeColor = Color.Black;
                    }
                    if (Cronómetro_Memoria.IsRunning)
                    {
                        Barra_Estado_Etiqueta_Memoria.ForeColor = (Cronómetro_Memoria.ElapsedMilliseconds / 500L) % 2 == 0 ? Color.Black : Color.Red;
                    }
                }
                catch { Barra_Estado_Etiqueta_Memoria.Text = "RAM: ? MB (? GB)"; }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_Semilla_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Numérico_Semilla.Refresh();
                if (!Ocupado)
                {
                    Buscar_Chunks_Limos();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
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
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_X_Bloque_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Numérico_X_Bloque.Refresh();
                if (!Ocupado)
                {
                    Ocupado = true;
                    X = (int)Numérico_X_Bloque.Value;
                    int Valor_X_Chunk = X / 16;
                    int Valor_X_Región = X / 512;
                    if (X < 0)
                    {
                        Valor_X_Chunk--;
                        Valor_X_Región--;
                    }
                    if (Numérico_X_Chunk.Value != Valor_X_Chunk) Numérico_X_Chunk.Value = Valor_X_Chunk;
                    if (Numérico_X_Región.Value != Valor_X_Región) Numérico_X_Región.Value = Valor_X_Región;
                    Ocupado = false;
                    Buscar_Chunks_Limos();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_Z_Bloque_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Numérico_Z_Bloque.Refresh();
                if (!Ocupado)
                {
                    Ocupado = true;
                    Z = (int)Numérico_Z_Bloque.Value;
                    int Valor_Z_Chunk = Z / 16;
                    int Valor_Z_Región = Z / 512;
                    if (Z < 0)
                    {
                        Valor_Z_Chunk--;
                        Valor_Z_Región--;
                    }
                    if (Numérico_Z_Chunk.Value != Valor_Z_Chunk) Numérico_Z_Chunk.Value = Valor_Z_Chunk;
                    if (Numérico_Z_Región.Value != Valor_Z_Región) Numérico_Z_Región.Value = Valor_Z_Región;
                    Ocupado = false;
                    Buscar_Chunks_Limos();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_X_Chunk_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Numérico_X_Chunk.Refresh();
                if (!Ocupado)
                {
                    Ocupado = true;
                    decimal Valor_X_Bloque = Math.Truncate(Numérico_X_Chunk.Value * 16m);
                    //if (Numérico_X_Chunk.Value < 0m) Valor_X_Bloque--;
                    if (Valor_X_Bloque < Numérico_X_Bloque.Minimum) Valor_X_Bloque = Numérico_X_Bloque.Minimum;
                    else if (Valor_X_Bloque > Numérico_X_Bloque.Maximum) Valor_X_Bloque = Numérico_X_Bloque.Maximum;
                    X = (int)Valor_X_Bloque;
                    int Valor_X_Región = X / 512;
                    if (X < 0) Valor_X_Región--;
                    if (Numérico_X_Bloque.Value != Valor_X_Bloque) Numérico_X_Bloque.Value = Valor_X_Bloque;
                    if (Numérico_X_Región.Value != Valor_X_Región) Numérico_X_Región.Value = Valor_X_Región;
                    Ocupado = false;
                    Buscar_Chunks_Limos();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_Z_Chunk_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Numérico_Z_Chunk.Refresh();
                if (!Ocupado)
                {
                    Ocupado = true;
                    decimal Valor_Z_Bloque = Math.Truncate(Numérico_Z_Chunk.Value * 16m);
                    if (Valor_Z_Bloque < Numérico_Z_Bloque.Minimum) Valor_Z_Bloque = Numérico_Z_Bloque.Minimum;
                    else if (Valor_Z_Bloque > Numérico_Z_Bloque.Maximum) Valor_Z_Bloque = Numérico_Z_Bloque.Maximum;
                    Z = (int)Valor_Z_Bloque;
                    int Valor_Z_Región = Z / 512;
                    if (Z < 0) Valor_Z_Región--;
                    if (Numérico_Z_Bloque.Value != Valor_Z_Bloque) Numérico_Z_Bloque.Value = Valor_Z_Bloque;
                    if (Numérico_Z_Región.Value != Valor_Z_Región) Numérico_Z_Región.Value = Valor_Z_Región;
                    Ocupado = false;
                    Buscar_Chunks_Limos();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_X_Región_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Numérico_X_Región.Refresh();
                if (!Ocupado)
                {
                    Ocupado = true;
                    decimal Valor_X_Bloque = Math.Truncate(Numérico_X_Región.Value * 512m);
                    if (Valor_X_Bloque < Numérico_X_Bloque.Minimum) Valor_X_Bloque = Numérico_X_Bloque.Minimum;
                    else if (Valor_X_Bloque > Numérico_X_Bloque.Maximum) Valor_X_Bloque = Numérico_X_Bloque.Maximum;
                    X = (int)Valor_X_Bloque;
                    int Valor_X_Chunk = X / 16;
                    //if (Numérico_X_Región.Value < 0m) Valor_X_Chunk--;
                    if (Numérico_X_Bloque.Value != Valor_X_Bloque) Numérico_X_Bloque.Value = Valor_X_Bloque;
                    if (Numérico_X_Chunk.Value != Valor_X_Chunk) Numérico_X_Chunk.Value = Valor_X_Chunk;
                    Ocupado = false;
                    Buscar_Chunks_Limos();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_Z_Región_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Numérico_Z_Región.Refresh();
                if (!Ocupado)
                {
                    Ocupado = true;
                    decimal Valor_Z_Bloque = Math.Truncate(Numérico_Z_Región.Value * 512m);
                    if (Valor_Z_Bloque < Numérico_Z_Bloque.Minimum) Valor_Z_Bloque = Numérico_Z_Bloque.Minimum;
                    else if (Valor_Z_Bloque > Numérico_Z_Bloque.Maximum) Valor_Z_Bloque = Numérico_Z_Bloque.Maximum;
                    Z = (int)Valor_Z_Bloque;
                    int Valor_Z_Chunk = Z / 16;
                    if (Numérico_Z_Bloque.Value != Valor_Z_Bloque) Numérico_Z_Bloque.Value = Valor_Z_Bloque;
                    if (Numérico_Z_Chunk.Value != Valor_Z_Chunk) Numérico_Z_Chunk.Value = Valor_Z_Chunk;
                    Ocupado = false;
                    Buscar_Chunks_Limos();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void ComboBox_Zoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Zoom.SelectedIndex > -1)
                {
                    Variable_Zoom = int.Parse(ComboBox_Zoom.Text.Replace("x", null));
                    /*Registro_Guardar_Opciones();
                    if (Variable_Cuadrícula_Chunks)
                    {
                        Picture.Image = Obtener_Imagen_Cuadrícula_Chunks(Ancho_Cliente, Alto_Cliente);
                        Picture.Invalidate();
                        Picture.Update();
                    }*/
                    Buscar_Chunks_Limos();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal void Buscar_Chunks_Limos()
        {
            try
            {
                if (!Ocupado)
                {
                    this.Cursor = Cursors.WaitCursor;
                    int Ancho = Picture.ClientSize.Width;
                    int Alto = Picture.ClientSize.Height;
                    if ((Variable_Mostrar_Reglas && Ancho > 40 && Alto > 40) || (!Variable_Mostrar_Reglas && Ancho > 0 && Alto > 0))
                    {
                        Rectangle Rectángulo_Área_Cliente_Sin_Reglas = Variable_Mostrar_Reglas ? new Rectangle(20, 20, Ancho - 40, Alto - 40) : new Rectangle(0, 0, Ancho, Alto);
                        int Dimensiones_X = int.MinValue;
                        int Dimensiones_Z = int.MinValue;
                        int Dimensiones_Ancho = 0;
                        int Dimensiones_Alto = 0;
                        int Chunks_Visibles = 0;
                        int Chunks_Limos = 0;

                        int Ancho_Mitad = Ancho / 2;
                        int Alto_Mitad = Alto / 2;

                        int X = Ancho_Mitad / Variable_Zoom;
                        if (X * Variable_Zoom < Ancho_Mitad) X++;
                        int XX = X * Variable_Zoom;
                        int XXX = Ancho_Mitad - XX;

                        int Z = Alto_Mitad / Variable_Zoom;
                        if (Z * Variable_Zoom < Alto_Mitad) Z++;
                        int ZZ = Z * Variable_Zoom;
                        int ZZZ = Alto_Mitad - ZZ;

                        int XXXX = (int)Numérico_X_Chunk.Value - X;
                        int ZZZZ = (int)Numérico_Z_Chunk.Value - Z;

                        // ...

                        //int Separación_Regla = 32; // Región / 16 // O cada 256 bloques
                        //Separación_Regla = 0;
                        
                        Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                        Graphics Pintar = Graphics.FromImage(Imagen);
                        Pintar.Clear(!Variable_Invertir_Colores ? Color.White : Color.Black);
                        Pintar.CompositingMode = CompositingMode.SourceOver;
                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar.SmoothingMode = SmoothingMode.HighQuality;
                        Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;

                        if (Variable_Mostrar_Reglas) // First pass to draw the rulers.
                        {
                            Pintar.FillRectangle(Pincel_Trama, 0, 0, Ancho, 1);
                            Pintar.FillRectangle(Pincel_Trama, 0, 19, Ancho, 1);
                            Pintar.FillRectangle(Pincel_Trama, 0, Alto - 20, Ancho, 1);
                            Pintar.FillRectangle(Pincel_Trama, 0, Alto - 1, Ancho, 1);
                            Pintar.FillRectangle(Pincel_Trama, 0, 0, 1, Alto);
                            Pintar.FillRectangle(Pincel_Trama, 19, 0, 1, Alto);
                            Pintar.FillRectangle(Pincel_Trama, Ancho - 20, 0, 1, Alto);
                            Pintar.FillRectangle(Pincel_Trama, Ancho - 1, 0, 1, Alto);
                            int Regla_X = Ancho_Mitad;
                            double Regla_XX = (double)Numérico_X_Chunk.Value * 16d;
                            int Regla_XXX = 0;
                            while (Regla_X > 0)
                            {
                                Regla_X -= Separación_Regla;
                                Regla_XXX -= (Separación_Regla * 16) / Variable_Zoom;
                            }
                            int Regla_Z = Alto_Mitad;
                            double Regla_ZZ = (double)Numérico_Z_Chunk.Value * 16d;
                            int Regla_ZZZ = 0;
                            while (Regla_Z > 0)
                            {
                                Regla_Z -= Separación_Regla;
                                Regla_ZZZ -= (Separación_Regla * 16) / Variable_Zoom;
                            }
                            for (int Índice_X = Regla_X, Chunk_X = Regla_XXX; Índice_X < Ancho; Índice_X += Separación_Regla, Chunk_X += (Separación_Regla * 16) / Variable_Zoom)
                            {
                                Pintar.FillRectangle(Pincel_Trama, Índice_X, 0, 1, Alto);
                            }
                            for (int Índice_Z = Regla_Z, Chunk_Z = Regla_ZZZ; Índice_Z < Alto; Índice_Z += Separación_Regla, Chunk_Z += (Separación_Regla * 16) / Variable_Zoom)
                            {
                                Pintar.FillRectangle(Pincel_Trama, 0, Índice_Z, Ancho, 1);
                            }
                        }

                        // Draw the rectangles or structure icons.
                        Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                        for (int Índice_Z = 0, Chunk_Z = ZZZZ, Pintar_Z = ZZZ; Índice_Z < Alto; Índice_Z++, Chunk_Z++, Pintar_Z += Variable_Zoom)
                        {
                            for (int Índice_X = 0, Chunk_X = XXXX, Pintar_X = XXX; Índice_X < Ancho; Índice_X++, Chunk_X++, Pintar_X += Variable_Zoom)
                            {
                                Rectangle Rectángulo = new Rectangle(Pintar_X, Pintar_Z, Variable_Zoom, Variable_Zoom);
                                if (Rectángulo.IntersectsWith(Rectángulo_Área_Cliente_Sin_Reglas))
                                {
                                    bool Chunk_Limos = new Program.Random_Java((ulong)((long)Numérico_Semilla.Value + (long)(Chunk_X * Chunk_X * 4987142) + (long)(Chunk_X * 5947611) + (long)(Chunk_Z * Chunk_Z) * 4392871L + (long)(Chunk_Z * 389711) ^ 987234911L)).nextInt(10) == 0;
                                    //MessageBox.Show((Chunk_Central_X + Chunk_X).ToString() + ", " + (Chunk_Central_Z + Chunk_Z).ToString());
                                    if (Variable_Estructura == Estructuras.Buried_treasure)
                                    {
                                        /*if (canSpawnBuriedTreasureAtCoords(Chunk_X, Chunk_Z))
                                        {
                                            if (!Variable_Dibujar_Iconos) Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.Black : Brushes.White, Rectángulo);
                                            else Pintar.DrawImage(Variable_Imagen_Estructura, Rectángulo, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                                        }*/
                                    }
                                    else if (Variable_Estructura == Estructuras.Desert_pyramid)
                                    {
                                        if (canSpawnDesertPyramidAtCoords(Chunk_X, Chunk_Z))
                                        {
                                            if (!Variable_Dibujar_Iconos) Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.Black : Brushes.White, Rectángulo);
                                            else Pintar.DrawImage(Variable_Imagen_Estructura, Rectángulo, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                                        }
                                    }
                                    else if (Variable_Estructura == Estructuras.End_city)
                                    {
                                        /*if (canSpawnEndCityAtCoords(Chunk_X, Chunk_Z))
                                        {
                                            if (!Variable_Dibujar_Iconos) Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.Black : Brushes.White, Rectángulo);
                                            else Pintar.DrawImage(Variable_Imagen_Estructura, Rectángulo, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                                        }*/
                                    }
                                    else if (Variable_Estructura == Estructuras.Fortress)
                                    {
                                        /*if (canSpawnFortressAtCoords(Chunk_X, Chunk_Z))
                                        {
                                            if (!Variable_Dibujar_Iconos) Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.Black : Brushes.White, Rectángulo);
                                            else Pintar.DrawImage(Variable_Imagen_Estructura, Rectángulo, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                                        }*/
                                    }
                                    else if (Variable_Estructura == Estructuras.Igloo)
                                    {
                                        if (canSpawnIglooAtCoords(Chunk_X, Chunk_Z))
                                        {
                                            if (!Variable_Dibujar_Iconos) Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.Black : Brushes.White, Rectángulo);
                                            else Pintar.DrawImage(Variable_Imagen_Estructura, Rectángulo, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                                        }
                                    }
                                    else if (Variable_Estructura == Estructuras.Jungle_pyramid)
                                    {
                                        if (canSpawnJunglePyramidAtCoords(Chunk_X, Chunk_Z))
                                        {
                                            if (!Variable_Dibujar_Iconos) Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.Black : Brushes.White, Rectángulo);
                                            else Pintar.DrawImage(Variable_Imagen_Estructura, Rectángulo, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                                        }
                                    }
                                    else if (Variable_Estructura == Estructuras.Mansion) // Seed -5773879284548960796.
                                    {
                                        if (canSpawnMansionAtCoords(Chunk_X, Chunk_Z))
                                        {
                                            if (!Variable_Dibujar_Iconos) Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.Black : Brushes.White, Rectángulo);
                                            else Pintar.DrawImage(Variable_Imagen_Estructura, Rectángulo, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                                        }
                                    }
                                    else if (Variable_Estructura == Estructuras.Mineshaft)
                                    {
                                        /*if (canSpawnMineshaftAtCoords(Chunk_X, Chunk_Z))
                                        {
                                            if (!Variable_Dibujar_Iconos) Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.Black : Brushes.White, Rectángulo);
                                            else Pintar.DrawImage(Variable_Imagen_Estructura, Rectángulo, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                                        }*/
                                    }
                                    else if (Variable_Estructura == Estructuras.Monument)
                                    {
                                        if (canSpawnMonumentAtCoords(Chunk_X, Chunk_Z))
                                        {
                                            if (!Variable_Dibujar_Iconos) Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.Black : Brushes.White, Rectángulo);
                                            else Pintar.DrawImage(Variable_Imagen_Estructura, Rectángulo, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                                        }
                                    }
                                    else if (Variable_Estructura == Estructuras.Ocean_ruin)
                                    {
                                        /*if (canSpawnOceanRuinAtCoords(Chunk_X, Chunk_Z))
                                        {
                                            if (!Variable_Dibujar_Iconos) Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.Black : Brushes.White, Rectángulo);
                                            else Pintar.DrawImage(Variable_Imagen_Estructura, Rectángulo, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                                        }*/
                                    }
                                    else if (Variable_Estructura == Estructuras.Shipwreck)
                                    {
                                        /*if (canSpawnShipwreckAtCoords(Chunk_X, Chunk_Z))
                                        {
                                            if (!Variable_Dibujar_Iconos) Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.Black : Brushes.White, Rectángulo);
                                            else Pintar.DrawImage(Variable_Imagen_Estructura, Rectángulo, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                                        }*/
                                    }
                                    else if (Variable_Estructura == Estructuras.Stronghold)
                                    {
                                        /*if (canSpawnStrongholdAtCoords(Chunk_X, Chunk_Z))
                                        {
                                            if (!Variable_Dibujar_Iconos) Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.Black : Brushes.White, Rectángulo);
                                            else Pintar.DrawImage(Variable_Imagen_Estructura, Rectángulo, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                                        }*/
                                    }
                                    else if (Variable_Estructura == Estructuras.Swamp_hut)
                                    {
                                        if (canSpawnSwampHutAtCoords(Chunk_X, Chunk_Z))
                                        {
                                            if (!Variable_Dibujar_Iconos) Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.Black : Brushes.White, Rectángulo);
                                            else Pintar.DrawImage(Variable_Imagen_Estructura, Rectángulo, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                                        }
                                    }
                                    else if (Variable_Estructura == Estructuras.Village)
                                    {
                                        if (canSpawnVillageAtCoords(Chunk_X, Chunk_Z))
                                        {
                                            if (!Variable_Dibujar_Iconos) Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.Black : Brushes.White, Rectángulo);
                                            else Pintar.DrawImage(Variable_Imagen_Estructura, Rectángulo, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                                        }
                                    }
                                    else // Slime_chunks
                                    {
                                        if (Chunk_Limos)
                                        {
                                            if (!Variable_Dibujar_Iconos) Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.Black : Brushes.White, Rectángulo);
                                            else Pintar.DrawImage(Variable_Imagen_Estructura, Rectángulo, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                                        }
                                    }
                                    if (Dimensiones_X <= int.MinValue) Dimensiones_X = Índice_X;
                                    if (Dimensiones_Z <= int.MinValue) Dimensiones_Z = Índice_Z;
                                    if (Índice_Z == Dimensiones_Z) Dimensiones_Ancho++;
                                    if (Índice_X == Dimensiones_X) Dimensiones_Alto++;
                                    Chunks_Visibles++;
                                    if (Chunk_Limos) Chunks_Limos++;
                                }
                            }
                        }
                        Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        if (Variable_Mostrar_Reglas) // Second pass to draw the rulers.
                        {
                            Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.White : Brushes.Black, 0, 0, Ancho, 20);
                            Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.White : Brushes.Black, 0, Alto - 20, Ancho, 20);

                            Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.White : Brushes.Black, 0, 0, 20, Alto);
                            Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.White : Brushes.Black, Ancho - 20, 0, 20, Alto);

                            Pintar.FillRectangle(Pincel_Trama, 0, 0, Ancho, 1);
                            Pintar.FillRectangle(Pincel_Trama, 0, 19, Ancho, 1);
                            Pintar.FillRectangle(Pincel_Trama, 0, Alto - 20, Ancho, 1);
                            Pintar.FillRectangle(Pincel_Trama, 0, Alto - 1, Ancho, 1);

                            Pintar.FillRectangle(Pincel_Trama, 0, 0, 1, Alto);
                            Pintar.FillRectangle(Pincel_Trama, 19, 0, 1, Alto);
                            Pintar.FillRectangle(Pincel_Trama, Ancho - 20, 0, 1, Alto);
                            Pintar.FillRectangle(Pincel_Trama, Ancho - 1, 0, 1, Alto);

                            int Regla_X = Ancho_Mitad; // - ((Separación_Regla * (int)Numérico_X_Chunk.Value) / Variable_Zoom);
                            //double Regla_XX = (double)Numérico_X_Chunk.Value * 16d;
                            int Regla_XXX = 0;
                            while (Regla_X > 0)
                            {
                                Regla_X -= Separación_Regla;
                                Regla_XXX -= (Separación_Regla * 16) / Variable_Zoom;
                            }
                            int Regla_Z = Alto_Mitad;
                            //double Regla_ZZ = (double)Numérico_Z_Chunk.Value * 16d;
                            int Regla_ZZZ = 0;
                            while (Regla_Z > 0)
                            {
                                Regla_Z -= Separación_Regla;
                                Regla_ZZZ -= (Separación_Regla * 16) / Variable_Zoom;
                            }
                            for (int Índice_X = Regla_X, Chunk_X = Regla_XXX; Índice_X < Ancho; Índice_X += Separación_Regla, Chunk_X += (Separación_Regla * 16) / Variable_Zoom)
                            {
                                Pintar.FillRectangle(Pincel_Trama, Índice_X, 1, 1, 18);
                                Pintar.FillRectangle(Pincel_Trama, Índice_X, Alto - 19, 1, 18);
                                Bitmap Imagen_Número = Program.Obtener_Imagen_Texto(Program.Traducir_Número(Chunk_X + (((int)Numérico_X_Chunk.Value * 16) / Variable_Zoom)), Etiqueta_Semilla.Font, !Variable_Invertir_Colores ? Color.White : Color.Black, !Variable_Invertir_Colores ? Color.Black : Color.White, TextRenderingHint.AntiAlias);
                                Rectangle Rectángulo = Program.Buscar_Zona_Recorte_Imagen(Imagen_Número, !Variable_Invertir_Colores ? Color.White : Color.Black);
                                if (Rectángulo.X > -1 && Rectángulo.Y > -1 && Rectángulo.X < int.MaxValue && Rectángulo.Y < int.MaxValue && Rectángulo.Width > 0 && Rectángulo.Height > 0)
                                {
                                    Imagen_Número = Imagen_Número.Clone(Rectángulo, PixelFormat.Format32bppArgb);
                                    Pintar.DrawImage(Imagen_Número, new Rectangle(Índice_X + 3, (20 - Rectángulo.Height) / 2, Rectángulo.Width, Rectángulo.Height), new Rectangle(0, 0, Rectángulo.Width, Rectángulo.Height), GraphicsUnit.Pixel);
                                    Imagen_Número.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                    Pintar.DrawImage(Imagen_Número, new Rectangle(Índice_X + 3, (Alto - 20) + (Rectángulo.Height / 2), Rectángulo.Width, Rectángulo.Height), new Rectangle(0, 0, Rectángulo.Width, Rectángulo.Height), GraphicsUnit.Pixel);
                                }
                                Imagen_Número.Dispose();
                                Imagen_Número = null;
                            }
                            for (int Índice_Z = Regla_Z, Chunk_Z = Regla_ZZZ; Índice_Z < Alto; Índice_Z += Separación_Regla, Chunk_Z += (Separación_Regla * 16) / Variable_Zoom)
                            {
                                Pintar.FillRectangle(Pincel_Trama, 1, Índice_Z, 18, 1);
                                Pintar.FillRectangle(Pincel_Trama, Ancho - 19, Índice_Z, 18, 1);
                                Bitmap Imagen_Número = Program.Obtener_Imagen_Texto(Program.Traducir_Número(Chunk_Z + (((int)Numérico_Z_Chunk.Value * 16) / Variable_Zoom)), Etiqueta_Semilla.Font, !Variable_Invertir_Colores ? Color.White : Color.Black, !Variable_Invertir_Colores ? Color.Black : Color.White, TextRenderingHint.AntiAlias);
                                Rectangle Rectángulo = Program.Buscar_Zona_Recorte_Imagen(Imagen_Número, !Variable_Invertir_Colores ? Color.White : Color.Black);
                                if (Rectángulo.X > -1 && Rectángulo.Y > -1 && Rectángulo.X < int.MaxValue && Rectángulo.Y < int.MaxValue && Rectángulo.Width > 0 && Rectángulo.Height > 0)
                                {
                                    Imagen_Número = Imagen_Número.Clone(Rectángulo, PixelFormat.Format32bppArgb);
                                    Imagen_Número.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                    Rectángulo = new Rectangle(Rectángulo.Y, Rectángulo.X, Rectángulo.Height, Rectángulo.Width);
                                    Pintar.DrawImage(Imagen_Número, new Rectangle((20 - Rectángulo.Width) / 2, Índice_Z + 3, Rectángulo.Width, Rectángulo.Height), new Rectangle(0, 0, Rectángulo.Width, Rectángulo.Height), GraphicsUnit.Pixel);
                                    Imagen_Número.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                    Pintar.DrawImage(Imagen_Número, new Rectangle((Ancho - 20) + (Rectángulo.Width / 2), Índice_Z + 3, Rectángulo.Width, Rectángulo.Height), new Rectangle(0, 0, Rectángulo.Width, Rectángulo.Height), GraphicsUnit.Pixel);
                                }
                                Imagen_Número.Dispose();
                                Imagen_Número = null;
                            }
                            Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.White : Brushes.Black, 1, 1, 18, 18);
                            Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.White : Brushes.Black, Ancho - 19, 1, 18, 18);
                            Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.White : Brushes.Black, 1, Alto - 19, 18, 18);
                            Pintar.FillRectangle(!Variable_Invertir_Colores ? Brushes.White : Brushes.Black, Ancho - 19, Alto - 19, 18, 18);
                        }

                        //Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho_Original, Alto_Original), GraphicsUnit.Pixel);
                        Pintar.Dispose();
                        Pintar = null;

                        Barra_Estado_Etiqueta_Dimensiones.Text = "Dimensions: " + Program.Traducir_Número(Dimensiones_Ancho) + " x " + Program.Traducir_Número(Dimensiones_Alto) + " chunks";
                        Barra_Estado_Etiqueta_Chunks_Visibles.Text = "Visible chunks: " + Program.Traducir_Número(Chunks_Visibles);
                        Barra_Estado_Etiqueta_Chunks_Limos.Text = "Slime chunks: " + Program.Traducir_Número(Chunks_Limos);

                        Picture.BackgroundImage = Imagen;
                        Picture.Refresh();

                        /*Rectangle Rectángulo = Program.Buscar_Zona_Recorte_Imagen(Imagen);
                        if (Rectángulo.X > -1 && Rectángulo.Y > -1 && Rectángulo.X < int.MaxValue && Rectángulo.Y < int.MaxValue && Rectángulo.Width > 0 && Rectángulo.Height > 0)
                        {
                            //Imagen.Save(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\origgg.png", ImageFormat.Png);
                            Imagen = Imagen.Clone(Rectángulo, PixelFormat.Format32bppArgb);
                            //Imagen.Save(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\origgg2.png", ImageFormat.Png);
                            //MessageBox.Show(Rectángulo.ToString());
                            if (Combo_Texto_Superior_Ángulo.SelectedIndex == 1) Imagen.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            if (Combo_Texto_Superior_Ángulo.SelectedIndex == 2) Imagen.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            if (Combo_Texto_Superior_Ángulo.SelectedIndex == 3) Imagen.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            if (CheckBox_Texto_Superior_Voltear_X.Checked) Imagen.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            if (CheckBox_Texto_Superior_Voltear_Y.Checked) Imagen.RotateFlip(RotateFlipType.RotateNoneFlipY);
                            return Imagen;
                        }*/
                    }
                    else Picture.BackgroundImage = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.BackgroundImage != null)
                {
                    Clipboard.SetImage(Picture.BackgroundImage);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.BackgroundImage != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Buscador_Chunks_Limos);
                    if (Directory.Exists(Program.Ruta_Guardado_Imágenes_Buscador_Chunks_Limos))
                    {
                        string Ruta = Program.Ruta_Guardado_Imágenes_Buscador_Chunks_Limos + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " Slime chunks (Seed " + Numérico_Semilla.Value.ToString() + ") [Chunk XZ " + Program.Traducir_Número(Numérico_X_Chunk.Value) + ", " + Program.Traducir_Número(Numérico_Z_Chunk.Value) + "].png";
                        try
                        {
                            Picture.BackgroundImage.Save(Ruta, ImageFormat.Png);
                            try { Program.Ejecutar_Ruta(Ruta, ProcessWindowStyle.Normal); }
                            catch { }
                            SystemSounds.Asterisk.Play();
                        }
                        catch { MessageBox.Show(this, "The program couldn't save the map to:\r\n" + Ruta + ".\r\nPlease try it again later and make sure you have the right privileges.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        Ruta = null;
                    }
                    else MessageBox.Show(this, "The program couldn't create the save folder for the map at:\r\n" + Program.Ruta_Guardado_Imágenes_Buscador_Chunks_Limos + ".\r\nPlease try it again later and make sure you have the right privileges.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Picture_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    Picture.Select();
                    Picture.Focus();
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    int Ancho = Picture.ClientSize.Width;
                    int Alto = Picture.ClientSize.Height;
                    int Ancho_Mitad = Ancho / 2;
                    int Alto_Mitad = Alto / 2;
                    int X = Ancho_Mitad / Variable_Zoom;
                    if (X * Variable_Zoom < Ancho_Mitad) X++;
                    int XX = X * Variable_Zoom;
                    int XXX = Ancho_Mitad - XX;
                    int Z = Alto_Mitad / Variable_Zoom;
                    if (Z * Variable_Zoom < Alto_Mitad) Z++;
                    int ZZ = Z * Variable_Zoom;
                    int ZZZ = Alto_Mitad - ZZ;
                    int XXXX = (int)Numérico_X_Chunk.Value - X;
                    int ZZZZ = (int)Numérico_Z_Chunk.Value - Z;
                    XXXX = (XXXX * 16) + (((e.X + Math.Abs(XXX)) * 16) / Variable_Zoom);
                    ZZZZ = (ZZZZ * 16) + (((e.Y + Math.Abs(ZZZ)) * 16) / Variable_Zoom);
                    Clipboard.SetText("/tp @p " + XXXX.ToString() + " 100 " + ZZZZ.ToString());
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_Semilla_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    byte[] Matriz_Bytes = new byte[8];
                    Program.Rand.NextBytes(Matriz_Bytes); // Workaround to get random 64 bits seeds.
                    Numérico_Semilla.Value = (decimal)BitConverter.ToInt64(Matriz_Bytes, 0);
                    Matriz_Bytes = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void TextBox_Semilla_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    string Texto = TextBox_Semilla.Text;
                    if (!string.IsNullOrEmpty(Texto))
                    {
                        bool Todos_Caracteres_Minúsculas = true;
                        bool Todos_Caracteres_Mayúsculas = true;
                        for (int Índice_Caracter = 0; Índice_Caracter < Texto.Length; Índice_Caracter++)
                        {
                            if (char.IsLetter(Texto[Índice_Caracter]) && (!char.IsLower(Texto[Índice_Caracter]) || !char.IsUpper(Texto[Índice_Caracter])))
                            {
                                if (char.IsLower(Texto[Índice_Caracter])) Todos_Caracteres_Mayúsculas = false;
                                else Todos_Caracteres_Minúsculas = false;
                            }
                        }
                        if (Todos_Caracteres_Minúsculas) // "xisumavoid" to "Xisumavoid"
                        {
                            Texto = Texto.Substring(0, 1).ToUpperInvariant() + (Texto.Length > 1 ? Texto.Substring(1).ToLowerInvariant() : null);
                        }
                        else if (Todos_Caracteres_Mayúsculas) // "XISUMAVOID" to "xisumavoid"
                        {
                            Texto = Texto.ToLowerInvariant();
                        }
                        else // "?" to "XISUMAVOID"
                        {
                            Texto = Texto.ToUpperInvariant();
                        }
                        TextBox_Semilla.Text = Texto;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void ComboBox_Zoom_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    ComboBox_Zoom.Text = "16x";
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_X_Bloque_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    Numérico_X_Bloque.Value = 0m;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_Z_Bloque_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    Numérico_Z_Bloque.Value = 0m;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_X_Chunk_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    Numérico_X_Chunk.Value = 0m;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_Z_Chunk_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    Numérico_Z_Chunk.Value = 0m;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_X_Región_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    Numérico_X_Región.Value = 0m;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_Z_Región_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    Numérico_Z_Región.Value = 0m;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Picture_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                int Ancho = Picture.ClientSize.Width;
                int Alto = Picture.ClientSize.Height;
                if (e.X >= 0 && e.Y >= 0 && e.X < Ancho && e.Y < Alto)
                {
                    int Ancho_Mitad = Ancho / 2;
                    int Alto_Mitad = Alto / 2;
                    int X = Ancho_Mitad / Variable_Zoom;
                    if (X * Variable_Zoom < Ancho_Mitad) X++;
                    int XX = X * Variable_Zoom;
                    int XXX = Ancho_Mitad - XX;
                    int Z = Alto_Mitad / Variable_Zoom;
                    if (Z * Variable_Zoom < Alto_Mitad) Z++;
                    int ZZ = Z * Variable_Zoom;
                    int ZZZ = Alto_Mitad - ZZ;
                    int XXXX = (int)Numérico_X_Chunk.Value - X;
                    int ZZZZ = (int)Numérico_Z_Chunk.Value - Z;
                    XXXX = (XXXX * 16) + (((e.X + Math.Abs(XXX)) * 16) / Variable_Zoom);
                    ZZZZ = (ZZZZ * 16) + (((e.Y + Math.Abs(ZZZ)) * 16) / Variable_Zoom);
                    this.Text = Texto_Título + "- [Looking at Block XZ: " + Program.Traducir_Número(XXXX) + ", " + Program.Traducir_Número(ZZZZ) + "]";
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Dibujar_Iconos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Dibujar_Iconos = Menú_Contextual_Dibujar_Iconos.Checked;
                Buscar_Chunks_Limos();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Invertir_Colores_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Invertir_Colores = Menú_Contextual_Invertir_Colores.Checked;
                Buscar_Chunks_Limos();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Mostrar_Reglas_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Mostrar_Reglas = Menú_Contextual_Mostrar_Reglas.Checked;
                Buscar_Chunks_Limos();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.Slime_chunks_finder;
                Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
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
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Depurador_Excepciones_Click(object sender, EventArgs e)
        {
            try
            {
                /*Variable_Excepción = false;
                Variable_Excepción_Imagen = false;
                Variable_Excepción_Total = 0;
                Barra_Estado_Botón_Excepción.Visible = false;
                Barra_Estado_Separador_1.Visible = false;
                Barra_Estado_Botón_Excepción.Image = Resources.Excepción_Gris;
                Barra_Estado_Botón_Excepción.ForeColor = Color.Black;
                Barra_Estado_Botón_Excepción.Text = "Exceptions: 0";*/
                Ventana_Depurador_Excepciones Ventana = new Ventana_Depurador_Excepciones();
                Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Abrir_Carpeta_Mapas_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Buscador_Chunks_Limos);
                Process Proceso = new Process();
                Proceso.StartInfo.Arguments = null;
                Proceso.StartInfo.ErrorDialog = false;
                Proceso.StartInfo.FileName = Program.Ruta_Guardado_Imágenes_Buscador_Chunks_Limos;
                Proceso.StartInfo.UseShellExecute = true;
                Proceso.StartInfo.Verb = "open";
                Proceso.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                try { Proceso.Start(); }
                catch { SystemSounds.Beep.Play(); }
                Proceso.Close();
                Proceso.Dispose();
                Proceso = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Buscar_Chunks_Limos();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void ComboBox_Estructura_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Estructura.SelectedIndex > -1 && !Ocupado)
                {
                    Ocupado = true;
                    Variable_Estructura = (Estructuras)Enum.Parse(typeof(Estructuras), ComboBox_Estructura.Text.Replace(' ', '_'), true);
                    if (Variable_Estructura == Estructuras.Buried_treasure) Variable_Imagen_Estructura = Resources.Estructura_Tesoro.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                    else if (Variable_Estructura == Estructuras.Desert_pyramid) Variable_Imagen_Estructura = Resources.Estructura_Pirámide.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                    else if (Variable_Estructura == Estructuras.End_city) Variable_Imagen_Estructura = Resources.Estructura_Ciudad_Elytra.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                    else if (Variable_Estructura == Estructuras.Fortress) Variable_Imagen_Estructura = Resources.Estructura_Fortaleza.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                    else if (Variable_Estructura == Estructuras.Igloo) Variable_Imagen_Estructura = Resources.Estructura_Iglú_Sótano.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                    else if (Variable_Estructura == Estructuras.Jungle_pyramid) Variable_Imagen_Estructura = Resources.Estructura_Templo.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                    else if (Variable_Estructura == Estructuras.Mansion) Variable_Imagen_Estructura = Resources.Estructura_Mansión.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                    else if (Variable_Estructura == Estructuras.Mineshaft) Variable_Imagen_Estructura = Resources.Estructura_Mina.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                    else if (Variable_Estructura == Estructuras.Monument) Variable_Imagen_Estructura = Resources.Estructura_Monumento.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                    else if (Variable_Estructura == Estructuras.Ocean_ruin) Variable_Imagen_Estructura = Resources.Estructura_Ruinas.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                    else if (Variable_Estructura == Estructuras.Shipwreck) Variable_Imagen_Estructura = Resources.Estructura_Barco_Mapa.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                    else if (Variable_Estructura == Estructuras.Stronghold) Variable_Imagen_Estructura = Resources.Estructura_Fin.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                    else if (Variable_Estructura == Estructuras.Swamp_hut) Variable_Imagen_Estructura = Resources.Estructura_Cabaña.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                    else if (Variable_Estructura == Estructuras.Village) Variable_Imagen_Estructura = Resources.Estructura_Aldea.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb);
                    else Variable_Imagen_Estructura = Resources.minecraft_slime_block.Clone() as Bitmap;
                    Picture_Estructura.Image = Variable_Imagen_Estructura;
                    Picture_Estructura.Refresh();
                    Ocupado = false;
                    Buscar_Chunks_Limos();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void ComboBox_Bioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Bioma.SelectedIndex > -1 && !Ocupado)
                {
                    Ocupado = true;
                    Variable_Bioma = (Biomas)Enum.Parse(typeof(Biomas), ComboBox_Bioma.Text.Replace(' ', '_'), true);
                    int Índice_Bioma = (int)Variable_Bioma;
                    if (Minecraft.Diccionario_Biomas_Colores.ContainsKey(Índice_Bioma))
                    {
                        Picture_Bioma.BackColor = Minecraft.Diccionario_Biomas_Colores[Índice_Bioma];
                    }
                    else Picture_Bioma.BackColor = Color.White;
                    Picture_Bioma.Refresh();
                    Ocupado = false;
                    Buscar_Chunks_Limos();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
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
            long i = (long)p_72843_1_ * 341873128712L + (long)p_72843_2_ * 132897987541L + (long)Numérico_Semilla.Value + (long)p_72843_3_;
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
            if (i == k && j == l)
            {
                List<Biomas> Lista_Biomas = new List<Biomas>(new Biomas[] { Biomas.Desert, Biomas.Desert_hills });
                return Variable_Bioma == Biomas.All || Lista_Biomas.Contains(Variable_Bioma);
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
            if (i == k && j == l)
            {
                List<Biomas> Lista_Biomas = new List<Biomas>(new Biomas[] { Biomas.Snowy_tundra, Biomas.Snowy_taiga });
                return Variable_Bioma == Biomas.All || Lista_Biomas.Contains(Variable_Bioma);
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
            if (i == k && j == l)
            {
                List<Biomas> Lista_Biomas = new List<Biomas>(new Biomas[] { Biomas.Jungle, Biomas.Jungle_hills });
                return Variable_Bioma == Biomas.All || Lista_Biomas.Contains(Variable_Bioma);
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
            if (i == k && j == l)
            {
                List<Biomas> Lista_Biomas = new List<Biomas>(new Biomas[] { Biomas.Swamp });
                return Variable_Bioma == Biomas.All || Lista_Biomas.Contains(Variable_Bioma);
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
            if (chunkX == k && chunkZ == l)
            {
                List<Biomas> Lista_Biomas = new List<Biomas>(new Biomas[] { Biomas.Dark_forest, Biomas.Dark_forest_hills });
                return Variable_Bioma == Biomas.All || Lista_Biomas.Contains(Variable_Bioma);
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
            if (i == k && j == l)
            {
                List<Biomas> Lista_Biomas = new List<Biomas>(new Biomas[] { Biomas.Deep_ocean });
                //List<Biomas> Lista_Biomas = new List<Biomas>(new Biomas[] { Biomas.Ocean, Biomas.Deep_ocean, Biomas.River, Biomas.Frozen_ocean, Biomas.Frozen_river });
                return Variable_Bioma == Biomas.All || Lista_Biomas.Contains(Variable_Bioma);
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
            if (i == k && j == l)
            {
                List<Biomas> Lista_Biomas = new List<Biomas>(new Biomas[] { Biomas.Plains, Biomas.Desert, Biomas.Savanna, Biomas.Taiga });
                return Variable_Bioma == Biomas.All || Lista_Biomas.Contains(Variable_Bioma);
                //bool flag = areBiomesViable(chunkX * 16 + 8, chunkZ * 16 + 8, 32, field_191072_a);
                //if (flag) return true;
            }
            return false;
        }
    }
}
