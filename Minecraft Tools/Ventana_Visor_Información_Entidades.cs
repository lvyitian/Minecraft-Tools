using Minecraft_Tools.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Visor_Información_Entidades : Form
    {
        public Ventana_Visor_Información_Entidades()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Structure that holds up all the information about an entity. Note that the damage dealt shown is the maximum possible for each entity.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Entidades
        {
            internal string Nombre;
            internal string Recurso;
            /// <summary>
            /// Each value means half heart, so 2 means a full heart.
            /// </summary>
            internal int Corazones;
            internal int Daño_Fácil;
            internal int Daño_Normal;
            internal int Daño_Difícil;
            internal string Actitud;
            internal string Debilidad;
            //internal string Armas;

            internal Entidades(string Nombre, int Corazones, int Daño_Fácil, int Daño_Normal, int Daño_Difícil, string Actitud, string Debilidad)
            {
                this.Nombre = Nombre;
                this.Recurso = Nombre.Replace(' ', '_').Replace('\\', '_').Replace('/', '_').Replace(':', '_').Replace('*', '_').Replace('?', '_').Replace('\"', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_').Replace('.', '_');
                this.Corazones = Corazones;
                this.Daño_Fácil = Daño_Fácil;
                this.Daño_Normal = Daño_Normal;
                this.Daño_Difícil = Daño_Difícil;
                this.Actitud = Actitud;
                this.Debilidad = Debilidad;
            }

            /// <summary>
            /// TODO: Add drops, spawn info, experience drops, etc.
            /// </summary>
            internal static readonly Entidades[] Matriz_Entidades = new Entidades[]
            {
                //new Entidades("Arrow", 0, 4, 4, 5, "Passive", "?"),
                new Entidades("Bat", 6, 0, 0, 0, "Passive", "?"),
                new Entidades("Blaze", 20, 4, 6, 9, "?", ""),
                new Entidades("Blaze_fireball", 20, 5, 5, 5, "?", "?"),
                new Entidades("Cat_siamese", 10, 0, 0, 0, "?", "?"),
                new Entidades("Chicken", 4, 0, 0, 0, "?", "?"),
                new Entidades("Chicken_jockey", 4, 0, 0, 0, "?", "?"),
                new Entidades("Cow", 10, 0, 0, 0, "Passive", "?"),
                new Entidades("Cow_mooshroom", 10, 0, 0, 0, "Passive", "?"),
                new Entidades("Creeper", 20, 25, 49, 73, "?", "Scared of cats"),
                new Entidades("Creeper_charged", 20, 49, 97, 145, "?", "Scared of cats?"),
                new Entidades("Dolphin", 10, 2, 3, 4, "?", "?"),
                new Entidades("Drowned", 20, 2, 3, 4, "?", "?"),
                new Entidades("Drowned_trident", 20, 8, 8, 8, "?", "?"),
                new Entidades("Elder Guardian", 80, 5, 8, 12, "?", "?"),
                new Entidades("Ender Dragon", 200, 6, 10, 15, "?", "?"),
                new Entidades("Enderman", 40, 4, 7, 10, "?", "Scared of sunlight"),
                new Entidades("Endermite", 8, 2, 2, 2, "?", "?"),
                new Entidades("Evoker", 24, 6, 6, 6, "?", "?"),
                new Entidades("Fish", 3, 6, 6, 6, "?", "?"),
                new Entidades("Giant", 100, 26, 50, 75, "?", "?"),
                new Entidades("Ghast", 10, 6, 6, 6, "?", "?"),
                new Entidades("Ghast_explosion", 10, 9, 17, 25, "?", "?"),
                new Entidades("Guardian", 30, 4, 6, 9, "?", "?"),
                new Entidades("Horse", 30, 2, 3, 4, "?", "?"),
                new Entidades("Husk", 20, 2, 3, 4, "?", "?"),
                new Entidades("Illusioner", 32, 4, 4, 5, "?", "?"),
                new Entidades("Iron Golem", 100, 11, 21, 31, "?", "?"),
                new Entidades("Llama", 30, 1, 1, 1, "?", "?"),
                new Entidades("Magma_Cube_small", 1, 2, 3, 4, "?", "?"),
                new Entidades("Magma_Cube_medium", 4, 3, 4, 6, "?", "?"),
                new Entidades("Magma_Cube_big", 16, 4, 6, 9, "?", "?"),
                new Entidades("Ocelot", 10, 0, 0, 0, "Passive", "?"),
                new Entidades("Parrot", 6, 0, 0, 0, "Passive", "?"),
                new Entidades("Pig", 10, 0, 0, 0, "Passive", "?"),
                new Entidades("Player_alex", 20, 1, 1, 1, "?", "?"),
                new Entidades("Player_steve", 20, 1, 1, 1, "?", "?"),
                new Entidades("Phantom", 20, 4, 6, 9, "?", "?"),
                new Entidades("Polarbear", 30, 4, 6, 9, "Attacks players if it has a baby", "?"),
                new Entidades("Pufferfish", 3, 2, 3, 4, "Gives the player Poison for 7 seconds", "?"),
                new Entidades("Pufferfish_poison", 3, 5, 5, 5, "Gives the player Poison for 7 seconds", "?"),
                new Entidades("Rabbit", 3, 0, 0, 0, "Passive", "?"),
                new Entidades("Sheep", 8, 0, 0, 0, "Passive", "?"),
                new Entidades("Shulker", 30, 4, 4, 4, "?", "?"),
                new Entidades("Silverfish", 8, 1, 1, 1, "?", "?"),
                new Entidades("Skeleton", 20, 4, 4, 5, "?", "Burns under sunlight and is scared of dogs or wolves"),
                new Entidades("Slime_small", 1, 0, 0, 0, "?", "?"),
                new Entidades("Slime_medium", 4, 2, 2, 3, "?", "?"),
                new Entidades("Slime_big", 16, 3, 4, 6, "?", "?"),
                new Entidades("Snowman", 4, 3, 3, 3, "Passive", "?"),
                new Entidades("Spider", 16, 2, 2, 3, "?", "Passive under sunlight"),
                new Entidades("Spider_cave", 12, 2, 2, 3, "?", "Passive under sunlight"),
                new Entidades("Spider_jockey", 20, 2, 2, 3, "?", ""),
                new Entidades("Squid", 10, 0, 0, 0, "Passive", "?"),
                new Entidades("Stray", 20, 4, 4, 5, "?", "Burns under sunlight and is scared of dogs or wolves"),
                new Entidades("The Killer Bunny", 3, 5, 8, 12, "?", "?"),
                new Entidades("Turtle", 30, 5, 8, 12, "?", "?"),
                new Entidades("Vex", 14, 5, 9, 13, "?", "?"),
                new Entidades("Villager", 20, 0, 0, 0, "Passive", "?"),
                new Entidades("Villager_butcher", 20, 0, 0, 0, "Passive", "?"),
                new Entidades("Villager_farmer", 20, 0, 0, 0, "Harvests nearby crops", "?"),
                new Entidades("Villager_librarian", 20, 0, 0, 0, "Passive", "?"),
                new Entidades("Villager_priest", 20, 0, 0, 0, "Passive", "?"),
                new Entidades("Villager_smith", 20, 0, 0, 0, "Passive", "?"),
                new Entidades("Vindicator", 24, 7, 13, 19, "?", "?"),
                new Entidades("Witch", 26, 4, 7, 10, "?", "?"),
                new Entidades("Wither", 300, 5, 8, 12, "Kills any other entity", "?"),
                new Entidades("Wither_skeleton", 20, 4, 7, 10, "?", "?"),
                //new Entidades("Wither_skull", 0, 5, 8, 12, "?", "?"),
                new Entidades("Wolf_hostime", 8, 2, 2, 3, "?", "?"),
                new Entidades("Wolf_tamed", 20, 3, 4, 3, "?", "?"),
                new Entidades("Zombie", 20, 2, 3, 4, "?", "Burns under sunlight"),
                new Entidades("Zombie_pigman", 20, 5, 9, 13, "?", "?"),
                new Entidades("Zombie_villager", 20, 2, 3, 4, "?", "Burns under sunlight"),
                new Entidades("Zombie_villager_butcher", 20, 2, 3, 4, "?", "Burns under sunlight"),
                new Entidades("Zombie_villager_farmer", 20, 2, 3, 4, "?", "Burns under sunlight"),
                new Entidades("Zombie_villager_librarian", 20, 2, 3, 4, "?", "Burns under sunlight"),
                new Entidades("Zombie_villager_priest", 20, 2, 3, 4, "?", "Burns under sunlight"),
                new Entidades("Zombie_villager_smith", 20, 2, 3, 4, "?", "Burns under sunlight")
            };
        }

        internal Bitmap Obtener_Imagen_Corazones(int Corazones)
        {
            try
            {
                int Ancho = Corazones % 2 == 0 ? Corazones / 2 : (Corazones + 1) / 2;
                if (Ancho > 10) Ancho = 10;
                Ancho *= 9;
                int Alto = Corazones / 20;
                if (Alto * 20 < Corazones) Alto++;
                Alto *= 9;
                if (Ancho > 0 && Alto > 0)
                {
                    Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.None;
                    int X = 0;
                    int Y = 0;
                    for (; X < Corazones; X += 2)
                    {
                        if (X > 0 && X % 20 == 0) Y += 9;
                        Pintar.DrawImage(Resources.Corazón, new Rectangle(((X % 20) / 2) * 9, Y, 9, 9), new Rectangle(0, 0, 9, 9), GraphicsUnit.Pixel);
                    }
                    if (Corazones % 2 != 0) Pintar.DrawImage(Resources.Corazón_Mitad, new Rectangle(((((X - 2) % 20) / 2) * 9) + 5, Y, 4, 9), new Rectangle(5, 0, 4, 9), GraphicsUnit.Pixel);
                    Pintar.Dispose();
                    Pintar = null;
                    return Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return Resources.Corazón_Mitad; //new Bitmap(9, 9, PixelFormat.Format32bppArgb);
        }

        internal readonly string Texto_Título = "Entities Information Viewer by Jupisoft for " + Program.Texto_Usuario;
        internal bool Variable_Siempre_Visible = false;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;
        internal static Stopwatch Cronómetro_FPS = Stopwatch.StartNew();
        internal long Segundo_FPS_Anterior = 0L;
        internal long FPS_Temporal = 0L;
        internal long FPS_Real = 0L;

        private void Ventana_Visor_Información_Entidades_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                if (Entidades.Matriz_Entidades != null && Entidades.Matriz_Entidades.Length > 0)
                {
                    for (int Índice_Entidad = 0; Índice_Entidad < Entidades.Matriz_Entidades.Length; Índice_Entidad++)
                    {
                        Bitmap Imagen = Program.Obtener_Imagen_Recursos("Entity_" + Entidades.Matriz_Entidades[Índice_Entidad].Recurso);
                        Imagen = Program.Obtener_Imagen_Miniatura(Imagen, 32, 32, true, false, CheckState.Checked);
                        DataGridView_Principal.Rows.Add(new object[]
                        {
                            Imagen,
                            Entidades.Matriz_Entidades[Índice_Entidad].Nombre.Replace('_', ' '),
                            Obtener_Imagen_Corazones(Entidades.Matriz_Entidades[Índice_Entidad].Corazones),
                            (double)Entidades.Matriz_Entidades[Índice_Entidad].Corazones / 2d,
                            Obtener_Imagen_Corazones(Entidades.Matriz_Entidades[Índice_Entidad].Daño_Fácil),
                            (double)Entidades.Matriz_Entidades[Índice_Entidad].Daño_Fácil / 2d,
                            Obtener_Imagen_Corazones(Entidades.Matriz_Entidades[Índice_Entidad].Daño_Normal),
                            (double)Entidades.Matriz_Entidades[Índice_Entidad].Daño_Normal / 2d,
                            Obtener_Imagen_Corazones(Entidades.Matriz_Entidades[Índice_Entidad].Daño_Difícil),
                            (double)Entidades.Matriz_Entidades[Índice_Entidad].Daño_Difícil / 2d, Entidades.Matriz_Entidades[Índice_Entidad].Actitud,
                            Entidades.Matriz_Entidades[Índice_Entidad].Debilidad
                        });
                    }
                    DataGridView_Principal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    DataGridView_Principal.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    DataGridView_Principal.Sort(Columna_Nombre, ListSortDirection.Ascending);
                    if (DataGridView_Principal.Rows.Count > 0)
                    {
                        this.Text = Texto_Título + " - [Minecraft entities known: " + Program.Traducir_Número(DataGridView_Principal.Rows.Count) + "]";
                        DataGridView_Principal.CurrentCell = DataGridView_Principal[0, 0];
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Información_Entidades_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Información_Entidades_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Información_Entidades_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Información_Entidades_KeyDown(object sender, KeyEventArgs e)
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
                long Milisegundo_FPS = Cronómetro_FPS.ElapsedMilliseconds;
                long Segundo_FPS = Milisegundo_FPS / 1000L;
                if (Segundo_FPS != Segundo_FPS_Anterior)
                {
                    Segundo_FPS_Anterior = Segundo_FPS;
                    FPS_Real = FPS_Temporal;
                    Barra_Estado_Etiqueta_FPS.Text = FPS_Real.ToString() + " FPS";
                    FPS_Temporal = 0L;
                }
                FPS_Temporal++;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void DataGridView_Principal_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                Depurador.Escribir_Excepción(e.Exception != null ? e.Exception.ToString() : null);
                e.ThrowException = false;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
