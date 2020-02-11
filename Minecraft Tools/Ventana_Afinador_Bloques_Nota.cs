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
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Afinador_Bloques_Nota : Form
    {
        public Ventana_Afinador_Bloques_Nota()
        {
            InitializeComponent();
        }

        internal static bool Variable_Dibujar_Notas = true;
        internal static bool Variable_Desplazar_Notas = true;
        internal static bool Variable_Voltear_Notas_Horizontalmente = false;
        internal static bool Variable_Voltear_Notas_Verticalmente = false;

        internal readonly string Texto_Título = "Note Blocks Tuner by Jupisoft for " + Program.Texto_Usuario;
        internal bool Variable_Siempre_Visible = false;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;
        internal static Stopwatch Cronómetro_FPS = Stopwatch.StartNew();
        internal long Segundo_FPS_Anterior = 0L;
        internal long FPS_Temporal = 0L;
        internal long FPS_Real = 0L;
        internal int Total_Notas = 0;
        internal bool Ocupado = false;
        internal int Milisegundo_Anterior = 0;
        internal SoundPlayer Reproductor = null;

        internal static readonly string Ruta_Sounds_Note = Application.StartupPath + "\\Sounds\\Note";
        internal static readonly string[] Matriz_Nombres_Instrumentos = new string[13]
        {
            "Basedrum",
            "Bass",
            "Bassattack",
            "Bell",
            "Chime",
            "Flute",
            "Guitar",
            "Harp",
            "Harp2",
            "Hat",
            "Pling",
            "Snare",
            "Xylophone"
        };
        internal static readonly byte[][] Matrices_Bytes_WAV = new byte[13][]
        {
            Program.Obtener_Matriz_Bytes_Archivo(Ruta_Sounds_Note + "\\" + Matriz_Nombres_Instrumentos[0] + ".wav"),
            Program.Obtener_Matriz_Bytes_Archivo(Ruta_Sounds_Note + "\\" + Matriz_Nombres_Instrumentos[1] + ".wav"),
            Program.Obtener_Matriz_Bytes_Archivo(Ruta_Sounds_Note + "\\" + Matriz_Nombres_Instrumentos[2] + ".wav"),
            Program.Obtener_Matriz_Bytes_Archivo(Ruta_Sounds_Note + "\\" + Matriz_Nombres_Instrumentos[3] + ".wav"),
            Program.Obtener_Matriz_Bytes_Archivo(Ruta_Sounds_Note + "\\" + Matriz_Nombres_Instrumentos[4] + ".wav"),
            Program.Obtener_Matriz_Bytes_Archivo(Ruta_Sounds_Note + "\\" + Matriz_Nombres_Instrumentos[5] + ".wav"),
            Program.Obtener_Matriz_Bytes_Archivo(Ruta_Sounds_Note + "\\" + Matriz_Nombres_Instrumentos[6] + ".wav"),
            Program.Obtener_Matriz_Bytes_Archivo(Ruta_Sounds_Note + "\\" + Matriz_Nombres_Instrumentos[7] + ".wav"),
            Program.Obtener_Matriz_Bytes_Archivo(Ruta_Sounds_Note + "\\" + Matriz_Nombres_Instrumentos[8] + ".wav"),
            Program.Obtener_Matriz_Bytes_Archivo(Ruta_Sounds_Note + "\\" + Matriz_Nombres_Instrumentos[9] + ".wav"),
            Program.Obtener_Matriz_Bytes_Archivo(Ruta_Sounds_Note + "\\" + Matriz_Nombres_Instrumentos[10] + ".wav"),
            Program.Obtener_Matriz_Bytes_Archivo(Ruta_Sounds_Note + "\\" + Matriz_Nombres_Instrumentos[11] + ".wav"),
            Program.Obtener_Matriz_Bytes_Archivo(Ruta_Sounds_Note + "\\" + Matriz_Nombres_Instrumentos[12] + ".wav")
        };
        internal static readonly Bitmap[] Matriz_Imágenes = new Bitmap[13]
        {
            Resources.Note_Blocks_Basedrum,
            Resources.Note_Blocks_Bass,
            Resources.Note_Blocks_Bassattack,
            Resources.Note_Blocks_Bell,
            Resources.Note_Blocks_Chime,
            Resources.Note_Blocks_Flute,
            Resources.Note_Blocks_Guitar,
            Resources.Note_Blocks_Harp,
            Resources.Note_Blocks_Harp2,
            Resources.Note_Blocks_Hat,
            Resources.Note_Blocks_Pling,
            Resources.Note_Blocks_Snare,
            Resources.Note_Blocks_Xylophone
        };
        internal static readonly Bitmap[] Matriz_Imágenes_Notas = new Bitmap[25]
        {
            Resources.Note_Blocks_Note_0,
            Resources.Note_Blocks_Note_1,
            Resources.Note_Blocks_Note_2,
            Resources.Note_Blocks_Note_3,
            Resources.Note_Blocks_Note_4,
            Resources.Note_Blocks_Note_5,
            Resources.Note_Blocks_Note_6,
            Resources.Note_Blocks_Note_7,
            Resources.Note_Blocks_Note_8,
            Resources.Note_Blocks_Note_9,
            Resources.Note_Blocks_Note_10,
            Resources.Note_Blocks_Note_11,
            Resources.Note_Blocks_Note_12,
            Resources.Note_Blocks_Note_13,
            Resources.Note_Blocks_Note_14,
            Resources.Note_Blocks_Note_15,
            Resources.Note_Blocks_Note_16,
            Resources.Note_Blocks_Note_17,
            Resources.Note_Blocks_Note_18,
            Resources.Note_Blocks_Note_19,
            Resources.Note_Blocks_Note_20,
            Resources.Note_Blocks_Note_21,
            Resources.Note_Blocks_Note_22,
            Resources.Note_Blocks_Note_23,
            Resources.Note_Blocks_Note_24,
        };
        internal static readonly double[] Matriz_Porcentajes_Frecuencias = new double[25]
        {
            0.5d * 100d,
            Math.Pow(2d, -11d / 12d) * 100d,
            Math.Pow(2d, -10d / 12d) * 100d,
            Math.Pow(2d, -9d / 12d) * 100d,
            Math.Pow(2d, -8d / 12d) * 100d,
            Math.Pow(2d, -7d / 12d) * 100d,
            Math.Pow(2d, -6d / 12d) * 100d,
            Math.Pow(2d, -5d / 12d) * 100d,
            Math.Pow(2d, -4d / 12d) * 100d,
            Math.Pow(2d, -3d / 12d) * 100d,
            Math.Pow(2d, -2d / 12d) * 100d,
            Math.Pow(2d, -1d / 12d) * 100d,
            1d * 100d,
            Math.Pow(2d, 1d / 12d) * 100d,
            Math.Pow(2d, 2d / 12d) * 100d,
            Math.Pow(2d, 3d / 12d) * 100d,
            Math.Pow(2d, 4d / 12d) * 100d,
            Math.Pow(2d, 5d / 12d) * 100d,
            Math.Pow(2d, 6d / 12d) * 100d,
            Math.Pow(2d, 7d / 12d) * 100d,
            Math.Pow(2d, 8d / 12d) * 100d,
            Math.Pow(2d, 9d / 12d) * 100d,
            Math.Pow(2d, 10d / 12d) * 100d,
            Math.Pow(2d, 11d / 12d) * 100d,
            2d * 100d
        };
        internal static readonly string[] Matriz_Recursos = new string[13] // For the WAV files
        {
            "Basedrum",
            "Bass",
            "Bassattack",
            "Bell",
            "Chime",
            "Flute",
            "Guitar",
            "Harp",
            "Harp2",
            "Hat",
            "Pling",
            "Snare",
            "Xylophone"
        };
        internal static readonly string[] Matriz_Bloques = new string[13]
        {
            "Stone",
            "Wood",
            "?",
            "Gold block",
            "Packed ice",
            "Clay",
            "Wool",
            "Other block",
            "?",
            "Glass",
            "?",
            "Sand",
            "Bone block"
        };
        internal Label[] Matriz_Etiquetas = null;
        internal ComboBox[] Matriz_ComboBoxes = null;
        internal Label[] Matriz_Etiquetas_Bloques = null;
        internal Graphics Pintar = null;
        internal List<int>[] Matriz_Listas_Milisegundos = new List<int>[13]
        {
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>()
        };
        internal List<int>[] Matriz_Listas_Notas = new List<int>[13]
        {
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>()
        };
        internal List<int>[] Matriz_Listas_Voltear = new List<int>[13]
        {
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>()
        };

        private void Ventana_Afinador_Bloques_Nota_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                Ocupado = true;
                Matriz_Etiquetas = new Label[13]
                {
                    Etiqueta_0,
                    Etiqueta_1,
                    Etiqueta_2,
                    Etiqueta_3,
                    Etiqueta_4,
                    Etiqueta_5,
                    Etiqueta_6,
                    Etiqueta_7,
                    Etiqueta_8,
                    Etiqueta_9,
                    Etiqueta_10,
                    Etiqueta_11,
                    Etiqueta_12
                };
                Matriz_ComboBoxes = new ComboBox[13]
                {
                    ComboBox_0,
                    ComboBox_1,
                    ComboBox_2,
                    ComboBox_3,
                    ComboBox_4,
                    ComboBox_5,
                    ComboBox_6,
                    ComboBox_7,
                    ComboBox_8,
                    ComboBox_9,
                    ComboBox_10,
                    ComboBox_11,
                    ComboBox_12
                };
                Matriz_Etiquetas_Bloques = new Label[13]
                {
                    Etiqueta_Bloque_0,
                    Etiqueta_Bloque_1,
                    Etiqueta_Bloque_2,
                    Etiqueta_Bloque_3,
                    Etiqueta_Bloque_4,
                    Etiqueta_Bloque_5,
                    Etiqueta_Bloque_6,
                    Etiqueta_Bloque_7,
                    Etiqueta_Bloque_8,
                    Etiqueta_Bloque_9,
                    Etiqueta_Bloque_10,
                    Etiqueta_Bloque_11,
                    Etiqueta_Bloque_12
                };
                Picture.BackgroundImage = new Bitmap(832, 384, PixelFormat.Format32bppArgb);
                Picture.Image = new Bitmap(832, 384, PixelFormat.Format32bppArgb);
                Graphics Pintar_Fondo = Graphics.FromImage(Picture.BackgroundImage);
                Pintar_Fondo.CompositingMode = CompositingMode.SourceCopy;
                Pintar_Fondo.Clear(Color.FromArgb(255, 51, 158, 255));
                Pintar = Graphics.FromImage(Picture.Image);
                Pintar.CompositingMode = CompositingMode.SourceOver;
                int Y = 384 - 128;
                for (int Índice_Instrumento = 0; Índice_Instrumento < 13; Índice_Instrumento++)
                {
                    Matriz_Etiquetas[Índice_Instrumento].Text = Matriz_Recursos[Índice_Instrumento] + ':';
                    Matriz_ComboBoxes[Índice_Instrumento].SelectedIndex = 0;
                    Pintar_Fondo.DrawImage(Matriz_Imágenes[Índice_Instrumento], new Rectangle(Índice_Instrumento * 64, Y, 64, 128), new Rectangle(0, 0, 64, 128), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Matriz_Imágenes_Notas[Matriz_ComboBoxes[Índice_Instrumento].SelectedIndex], new Rectangle((Índice_Instrumento * 64) + 22, 272, 20, 32), new Rectangle(0, 0, 20, 32), GraphicsUnit.Pixel);
                    Matriz_Etiquetas_Bloques[Índice_Instrumento].Text = Matriz_Bloques[Índice_Instrumento];
                }
                Pintar_Fondo.Dispose();
                Pintar_Fondo = null;
                Ocupado = false;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Afinador_Bloques_Nota_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Afinador_Bloques_Nota_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
                if (Reproductor != null)
                {
                    Reproductor.Stop();
                    Reproductor.Dispose();
                    Reproductor = null;
                }
                Pintar.Dispose();
                Pintar = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Afinador_Bloques_Nota_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Afinador_Bloques_Nota_KeyDown(object sender, KeyEventArgs e)
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
                    else if (e.KeyCode == Keys.Insert)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        int Milisegundo = Environment.TickCount;
                        if ((Milisegundo - Milisegundo_Anterior) >= 100) // Only allow 10 plays per second in redstone ticks.
                        {
                            Milisegundo_Anterior = Milisegundo;
                            Total_Notas++;
                            this.Text = Texto_Título + " - [Visible notes: " + Program.Traducir_Número(Total_Notas) + "]";
                            int Índice_Instrumento = Program.Rand.Next(0, 13);
                            Matriz_Listas_Milisegundos[Índice_Instrumento].Add(Milisegundo);
                            Matriz_Listas_Notas[Índice_Instrumento].Add(Program.Rand.Next(0, Matriz_ComboBoxes[Índice_Instrumento].Items.Count));
                            Matriz_Listas_Voltear[Índice_Instrumento].Add((Variable_Voltear_Notas_Horizontalmente && Program.Rand.Next(0, 2) == 1 ? 1 : 0) | (Variable_Voltear_Notas_Verticalmente && Program.Rand.Next(0, 2) == 1 ? 2 : 0));
                            Reproductor = Reproductor_Sonidos.Cargar_Sonido(Matrices_Bytes_WAV[Índice_Instrumento].Clone() as byte[], Matriz_Porcentajes_Frecuencias[Matriz_ComboBoxes[Índice_Instrumento].SelectedIndex]);
                            if (Reproductor != null) Reproductor.Play();
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
                {
                    if (!Ocupado)
                    {
                        int Índice_Instrumento = e.X / 64;
                        if (Índice_Instrumento < 0) Índice_Instrumento = 0;
                        else if (Índice_Instrumento > 12) Índice_Instrumento = 12;
                        Matriz_ComboBoxes[Índice_Instrumento].Select();
                        Matriz_ComboBoxes[Índice_Instrumento].Focus();
                        if (e.Button == MouseButtons.Left) // Play
                        {
                            int Milisegundo = Environment.TickCount;
                            if ((Milisegundo - Milisegundo_Anterior) >= 100) // Only allow 10 plays per second in redstone ticks.
                            {
                                Milisegundo_Anterior = Milisegundo;
                                Total_Notas++;
                                this.Text = Texto_Título + " - [Visible notes: " + Program.Traducir_Número(Total_Notas) + "]";
                                Matriz_Listas_Milisegundos[Índice_Instrumento].Add(Milisegundo);
                                Matriz_Listas_Notas[Índice_Instrumento].Add(Matriz_ComboBoxes[Índice_Instrumento].SelectedIndex);
                                Matriz_Listas_Voltear[Índice_Instrumento].Add((Variable_Voltear_Notas_Horizontalmente && Program.Rand.Next(0, 2) == 1 ? 1 : 0) | (Variable_Voltear_Notas_Verticalmente && Program.Rand.Next(0, 2) == 1 ? 2 : 0));
                                Reproductor = Reproductor_Sonidos.Cargar_Sonido(Matrices_Bytes_WAV[Índice_Instrumento].Clone() as byte[], Matriz_Porcentajes_Frecuencias[Matriz_ComboBoxes[Índice_Instrumento].SelectedIndex]);
                                if (Reproductor != null) Reproductor.Play();
                            }
                        }
                        else if (e.Button == MouseButtons.Middle) // Change pitch
                        {
                            if (Matriz_ComboBoxes[Índice_Instrumento].SelectedIndex < 24) Matriz_ComboBoxes[Índice_Instrumento].SelectedIndex++;
                            else Matriz_ComboBoxes[Índice_Instrumento].SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
                {
                    if (!Ocupado)
                    {
                        int Índice_Instrumento = e.X / 64;
                        if (Índice_Instrumento < 0) Índice_Instrumento = 0;
                        else if (Índice_Instrumento > 12) Índice_Instrumento = 12;
                        if (e.Button == MouseButtons.Left) // Play
                        {
                            int Milisegundo = Environment.TickCount;
                            if ((Milisegundo - Milisegundo_Anterior) >= 100) // Only allow 10 plays per second in redstone ticks.
                            {
                                Milisegundo_Anterior = Milisegundo;
                                Total_Notas++;
                                this.Text = Texto_Título + " - [Visible notes: " + Program.Traducir_Número(Total_Notas) + "]";
                                Matriz_Listas_Milisegundos[Índice_Instrumento].Add(Milisegundo);
                                Matriz_Listas_Notas[Índice_Instrumento].Add(Matriz_ComboBoxes[Índice_Instrumento].SelectedIndex);
                                Matriz_Listas_Voltear[Índice_Instrumento].Add((Variable_Voltear_Notas_Horizontalmente && Program.Rand.Next(0, 2) == 1 ? 1 : 0) | (Variable_Voltear_Notas_Verticalmente && Program.Rand.Next(0, 2) == 1 ? 2 : 0));
                                Reproductor = Reproductor_Sonidos.Cargar_Sonido(Matrices_Bytes_WAV[Índice_Instrumento].Clone() as byte[], Matriz_Porcentajes_Frecuencias[Matriz_ComboBoxes[Índice_Instrumento].SelectedIndex]);
                                if (Reproductor != null) Reproductor.Play();
                            }
                        }
                        else if (e.Button == MouseButtons.Middle) // Change pitch
                        {
                            if (Matriz_ComboBoxes[Índice_Instrumento].SelectedIndex < 24) Matriz_ComboBoxes[Índice_Instrumento].SelectedIndex++;
                            else Matriz_ComboBoxes[Índice_Instrumento].SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBoxes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Ocupado)
                {
                    ComboBox Combo = sender as ComboBox;
                    if (Combo != null)
                    {
                        int Índice_Instrumento = int.Parse(Combo.Name.Replace("ComboBox_", null));
                        if (Índice_Instrumento > -1 && Índice_Instrumento < 13)
                        {
                            int Milisegundo = Environment.TickCount;
                            if ((Milisegundo - Milisegundo_Anterior) >= 100) // Only allow 10 plays per second in redstone ticks.
                            {
                                Milisegundo_Anterior = Milisegundo;
                                Total_Notas++;
                                this.Text = Texto_Título + " - [Visible notes: " + Program.Traducir_Número(Total_Notas) + "]";
                                Matriz_Listas_Milisegundos[Índice_Instrumento].Add(Milisegundo);
                                Matriz_Listas_Notas[Índice_Instrumento].Add(Matriz_ComboBoxes[Índice_Instrumento].SelectedIndex);
                                Matriz_Listas_Voltear[Índice_Instrumento].Add((Variable_Voltear_Notas_Horizontalmente && Program.Rand.Next(0, 2) == 1 ? 1 : 0) | (Variable_Voltear_Notas_Verticalmente && Program.Rand.Next(0, 2) == 1 ? 2 : 0));
                                Reproductor = Reproductor_Sonidos.Cargar_Sonido(Matrices_Bytes_WAV[Índice_Instrumento].Clone() as byte[], Matriz_Porcentajes_Frecuencias[Matriz_ComboBoxes[Índice_Instrumento].SelectedIndex]);
                                if (Reproductor != null) Reproductor.Play();
                            }
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.Note_blocks_tuner;
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
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Afinador_Bloques_Nota);
                Program.Ejecutar_Ruta(Program.Ruta_Guardado_Imágenes_Afinador_Bloques_Nota, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Restablecer_Tonos_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Ocupado)
                {
                    Ocupado = true;
                    for (int Índice_Instrumento = 0; Índice_Instrumento < 13; Índice_Instrumento++)
                    {
                        Matriz_ComboBoxes[Índice_Instrumento].SelectedIndex = 0;
                        Pintar.DrawImage(Matriz_Imágenes_Notas[Matriz_ComboBoxes[Índice_Instrumento].SelectedIndex], new Rectangle((Índice_Instrumento * 64) + 22, 272, 20, 32), new Rectangle(0, 0, 20, 32), GraphicsUnit.Pixel);
                    }
                    Ocupado = false;
                    Picture.Refresh();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Aleatorizar_Tonos_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Ocupado)
                {
                    Ocupado = true;
                    for (int Índice_Instrumento = 0; Índice_Instrumento < 13; Índice_Instrumento++)
                    {
                        Matriz_ComboBoxes[Índice_Instrumento].SelectedIndex = Program.Rand.Next(0, Matriz_ComboBoxes[Índice_Instrumento].Items.Count);
                        Pintar.DrawImage(Matriz_Imágenes_Notas[Matriz_ComboBoxes[Índice_Instrumento].SelectedIndex], new Rectangle((Índice_Instrumento * 64) + 22, 272, 20, 32), new Rectangle(0, 0, 20, 32), GraphicsUnit.Pixel);
                    }
                    Ocupado = false;
                    Picture.Refresh();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Dibujar_Notas_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Dibujar_Notas = Menú_Contextual_Dibujar_Notas.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Desplazar_Notas_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Desplazar_Notas = Menú_Contextual_Desplazar_Notas.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Voltear_Notas_Horizontalmente_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Voltear_Notas_Horizontalmente = Menú_Contextual_Voltear_Notas_Horizontalmente.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Voltear_Notas_Verticalmente_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Voltear_Notas_Verticalmente = Menú_Contextual_Voltear_Notas_Verticalmente.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.BackgroundImage != null && Picture.Image != null)
                {
                    Bitmap Imagen = (Bitmap)Picture.BackgroundImage.Clone();
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceOver;
                    Pintar.DrawImage(Picture.Image, new Rectangle(0, 0, 832, 384), new Rectangle(0, 0, 832, 384), GraphicsUnit.Pixel);
                    Pintar.Dispose();
                    Pintar = null;
                    Clipboard.SetImage(Imagen);
                    Imagen.Dispose();
                    Imagen = null;
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.BackgroundImage != null && Picture.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Afinador_Bloques_Nota);
                    Bitmap Imagen = (Bitmap)Picture.BackgroundImage.Clone();
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceOver;
                    Pintar.DrawImage(Picture.Image, new Rectangle(0, 0, 832, 384), new Rectangle(0, 0, 832, 384), GraphicsUnit.Pixel);
                    Pintar.Dispose();
                    Pintar = null;
                    Imagen.Save(Program.Ruta_Guardado_Imágenes_Afinador_Bloques_Nota + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " " + Total_Notas.ToString() + (Total_Notas != 1 ? " notes" : " note") + ".png", ImageFormat.Png);
                    Imagen.Dispose();
                    Imagen = null;
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
                try
                {
                    bool Actualizar = false;
                    for (int Índice_Instrumento = 0; Índice_Instrumento < 13; Índice_Instrumento++)
                    {
                        if (Matriz_Listas_Milisegundos[Índice_Instrumento].Count > 0)
                        {
                            Actualizar = true;
                            Pintar.CompositingMode = CompositingMode.SourceCopy;
                            Pintar.FillRectangle(Brushes.Transparent, Índice_Instrumento * 64, 0, 64, 320);
                            Pintar.CompositingMode = CompositingMode.SourceOver;
                            for (int Índice_Milisegundo = 0; Índice_Milisegundo < Matriz_Listas_Milisegundos[Índice_Instrumento].Count; )
                            {
                                int Índice_Animación = Environment.TickCount - Matriz_Listas_Milisegundos[Índice_Instrumento][Índice_Milisegundo];
                                if (Índice_Animación < 0) Índice_Animación = 0;
                                else if (Índice_Animación > 10000) Índice_Animación = 10000;
                                if (Variable_Dibujar_Notas)
                                {
                                    Bitmap Imagen_Nota = (Bitmap)Matriz_Imágenes_Notas[Matriz_Listas_Notas[Índice_Instrumento][Índice_Milisegundo]].Clone();
                                    if ((Matriz_Listas_Voltear[Índice_Instrumento][Índice_Milisegundo] & 1) != 0) Imagen_Nota.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                    if ((Matriz_Listas_Voltear[Índice_Instrumento][Índice_Milisegundo] & 2) != 0) Imagen_Nota.RotateFlip(RotateFlipType.RotateNoneFlipY);
                                    if (Variable_Desplazar_Notas)
                                    {
                                        int Y = 272 - ((304 * Índice_Animación) / 10000);
                                        Pintar.DrawImage(Imagen_Nota, new Rectangle((Índice_Instrumento * 64) + 22, Y, 20, 32), new Rectangle(0, 0, 20, 32), GraphicsUnit.Pixel);
                                    }
                                    else if (Índice_Animación < 10000 && Índice_Milisegundo >= Matriz_Listas_Milisegundos[Índice_Instrumento].Count - 1) Pintar.DrawImage(Imagen_Nota, new Rectangle((Índice_Instrumento * 64) + 22, 208, 20, 32), new Rectangle(0, 0, 20, 32), GraphicsUnit.Pixel);
                                    Imagen_Nota.Dispose();
                                    Imagen_Nota = null;
                                }
                                if (Índice_Animación >= 10000)
                                {
                                    Total_Notas--;
                                    this.Text = Texto_Título + " - [Visible notes: " + Program.Traducir_Número(Total_Notas) + "]";
                                    Matriz_Listas_Milisegundos[Índice_Instrumento].RemoveAt(Índice_Milisegundo);
                                    Matriz_Listas_Notas[Índice_Instrumento].RemoveAt(Índice_Milisegundo);
                                    Matriz_Listas_Voltear[Índice_Instrumento].RemoveAt(Índice_Milisegundo);
                                }
                                else Índice_Milisegundo++;
                            }
                        }
                        Pintar.DrawImage(Matriz_Imágenes_Notas[Matriz_ComboBoxes[Índice_Instrumento].SelectedIndex], new Rectangle((Índice_Instrumento * 64) + 22, 272, 20, 32), new Rectangle(0, 0, 20, 32), GraphicsUnit.Pixel);
                    }
                    if (Actualizar) Picture.Refresh();
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
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
    }
}
