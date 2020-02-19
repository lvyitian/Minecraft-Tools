using Minecraft_Tools.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Exportador_Estructuras_Pintadas : Form
    {
        public Ventana_Exportador_Estructuras_Pintadas()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Painted Structures Exporter by Jupisoft for " + Program.Texto_Usuario;

        internal bool Variable_Siempre_Visible = false;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;
        
        internal string Variable_Dimensión = string.Empty;
        internal static int Variable_X_De = 0;
        internal static int Variable_Y_De = 0;
        internal static int Variable_Z_De = 0;
        internal static int Variable_X_A = 15; // By default select the whole center chunk
        internal static int Variable_Y_A = 255;
        internal static int Variable_Z_A = 15;

        internal bool Ocupado = false;

        internal Dictionary<string, string> Diccionario_Dimensiones_Rutas = new Dictionary<string, string>();
        internal Dictionary<string, Rectangle> Diccionario_Dimensiones_Límites = new Dictionary<string, Rectangle>();
        internal Dictionary<string, List<Point>> Diccionario_Dimensiones_Lista_Posiciones_Regiones = new Dictionary<string, List<Point>>();
        internal Dictionary<string, List<Minecraft.Regiones>> Diccionario_Dimensiones_Caché_Regiones = new Dictionary<string, List<Minecraft.Regiones>>();

        internal bool Pendiente_Copiar_Portapapeles = false;
        internal bool Pendiente_Copiar_Ventana_Portapapeles = false;
        internal bool Pendiente_Vaciar_Toda_Caché = false;
        internal bool Pendiente_Guardar_Imagen_PNG = false;
        internal bool Pendiente_Invertir_Mapa = false;
        internal bool Pendiente_Dibujar_Mapa = false;
        internal bool Pendiente_Redimensionar_Mapa = false;
        internal bool Pendiente_Subproceso_Abortar = false;
        internal bool Pendiente_Subproceso_Abortar_Global = false;
        internal bool Subproceso_Activo = false;

        internal string Ruta_Pendiente_Abrir = null;
        internal string Ruta_Nivel = null;

        internal int Ancho_Cliente = 0;
        internal int Alto_Cliente = 0;

        internal Thread Subproceso = null;

        private void Ventana_Exportador_Estructuras_Pintadas_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                Ocupado = true;
                Minecraft.Pendiente_Subproceso_Abortar = false;
                this.WindowState = FormWindowState.Maximized;
                Ocupado = false;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Exportador_Estructuras_Pintadas_Shown(object sender, EventArgs e)
        {
            try
            {
                Temporizador_Principal.Start();
                this.Activate();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Exportador_Estructuras_Pintadas_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Pendiente_Dibujar_Mapa = false;
                Pendiente_Subproceso_Abortar_Global = true;
                Pendiente_Dibujar_Mapa = true;
                if (Subproceso_Activo) e.Cancel = true;
                else
                {
                    //Registro_Guardar_Opciones();
                    foreach (KeyValuePair<string, string> Entrada in Diccionario_Dimensiones_Rutas)
                    {
                        Diccionario_Dimensiones_Lista_Posiciones_Regiones[Entrada.Key].Clear();
                        Diccionario_Dimensiones_Caché_Regiones[Entrada.Key].Clear();
                    }
                    Diccionario_Dimensiones_Rutas.Clear();
                    Diccionario_Dimensiones_Límites.Clear();
                    Diccionario_Dimensiones_Lista_Posiciones_Regiones.Clear();
                    Diccionario_Dimensiones_Caché_Regiones.Clear();
                    GC.Collect();
                    GC.GetTotalMemory(true);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Exportador_Estructuras_Pintadas_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Exportador_Estructuras_Pintadas_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Exportador_Estructuras_Pintadas_DragDrop(object sender, DragEventArgs e)
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
                                if (!string.IsNullOrEmpty(Ruta))
                                {
                                    string Ruta_Carpeta = Directory.Exists(Ruta) ? Ruta : Path.GetDirectoryName(Ruta);
                                    if (Directory.Exists(Ruta_Carpeta + "\\region") || Directory.Exists(Ruta_Carpeta + "\\DIM-1") || Directory.Exists(Ruta_Carpeta + "\\DIM1"))
                                    {
                                        Ruta_Pendiente_Abrir = Ruta_Carpeta;
                                        //this.Cursor = Cursors.WaitCursor; // En espera para abrir la ruta
                                    }
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

        private void Ventana_Exportador_Estructuras_Pintadas_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Alt && !e.Control && !e.Shift)
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
                catch { Barra_Estado_Etiqueta_Memoria.Text = "RAM: ? MB (? GB)"; }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            try
            {
                if (!string.IsNullOrEmpty(Ruta_Pendiente_Abrir))
                {
                    if (!Subproceso_Activo)
                    {
                        Ruta_Nivel = Ruta_Pendiente_Abrir;
                        Ruta_Pendiente_Abrir = null;
                        Abrir_Ruta_Nivel();
                        return;
                    }
                    else
                    {
                        Pendiente_Subproceso_Abortar = true;
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(Ruta_Nivel))
                {
                    if (!Subproceso_Activo)
                    {
                        //if (Picture.BackColor.GetHashCode() != Color_Fondo.GetHashCode()) Picture.BackColor = Color_Fondo;
                        if (Pendiente_Invertir_Mapa)
                        {
                            /*Pendiente_Invertir_Mapa = false;
                            BitmapData Bitmap_Data = ((Bitmap)Picture.BackgroundImage).LockBits(new Rectangle(0, 0, Ancho_Cliente, Alto_Cliente), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                            byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto_Cliente];
                            Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                            for (int Y = 0, Índice = 0; Y < Alto_Cliente; Y++)
                            {
                                for (int X = 0; X < Ancho_Cliente; X++, Índice += 4)
                                {
                                    if (Matriz_Bytes[Índice + 3] > 0) // Negativizar los píxeles no transparentes
                                    {
                                        Matriz_Bytes[Índice + 2] = (byte)(255 - Matriz_Bytes[Índice + 2]);
                                        Matriz_Bytes[Índice + 1] = (byte)(255 - Matriz_Bytes[Índice + 1]);
                                        Matriz_Bytes[Índice] = (byte)(255 - Matriz_Bytes[Índice]);
                                    }
                                }
                            }
                            Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                            ((Bitmap)Picture.BackgroundImage).UnlockBits(Bitmap_Data);
                            Bitmap_Data = null;
                            Matriz_Bytes = null;
                            Picture.Invalidate();
                            Picture.Update();*/
                        }
                        if (Pendiente_Vaciar_Toda_Caché)
                        {
                            Pendiente_Vaciar_Toda_Caché = false;
                            foreach (KeyValuePair<string, string> Entrada in Diccionario_Dimensiones_Rutas)
                            {
                                Diccionario_Dimensiones_Caché_Regiones[Entrada.Key].Clear();
                            }
                            GC.Collect();
                            GC.GetTotalMemory(true);
                        }
                        if (Pendiente_Guardar_Imagen_PNG)
                        {
                            /*Pendiente_Guardar_Imagen_PNG = false;
                            Bitmap Imagen = new Bitmap(Ancho_Cliente, Alto_Cliente, PixelFormat.Format24bppRgb);
                            Pintar = Graphics.FromImage(Imagen);
                            Pintar.Clear(Variable_Color_Fondo);
                            Pintar.CompositingMode = CompositingMode.SourceOver;
                            Pintar.DrawImage(Picture.BackgroundImage, new Rectangle(0, 0, Ancho_Cliente, Alto_Cliente), new Rectangle(0, 0, Ancho_Cliente, Alto_Cliente), GraphicsUnit.Pixel);
                            Pintar.Dispose();
                            Pintar = null;
                            Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Realistic_World_Viewer_2D);
                            if (Directory.Exists(Program.Ruta_Guardado_Imágenes_Realistic_World_Viewer_2D))
                            {
                                string Ruta = Program.Ruta_Guardado_Imágenes_Realistic_World_Viewer_2D + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " Map (" + ComboBox_Mapa.Text + (Variable_Mapa_3D ? " 3D " : " 2D ") + Variable_Dimensión.ToString().Replace('_', ' ') + ") [XYZ " + Program.Traducir_Número(Variable_X) + ", " + Program.Traducir_Número(Variable_Y) + ", " + Program.Traducir_Número(Variable_Z) + "].png";
                                try
                                {
                                    Imagen.Save(Ruta, ImageFormat.Png);
                                    try { Process.Start(Ruta); }
                                    catch { }
                                    SystemSounds.Asterisk.Play();
                                }
                                catch { MessageBox.Show(this, "The program couldn't save the map to:\r\n" + Ruta + ".\r\nPlease try it again later and make sure you have the right privileges.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                                Ruta = null;
                            }
                            else MessageBox.Show(this, "The program couldn't create the save folder for the map at:\r\n" + Program.Ruta_Guardado_Imágenes_Realistic_World_Viewer_2D + ".\r\nPlease try it again later and make sure you have the right privileges.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Imagen.Dispose();
                            Imagen = null;*/
                        }
                        if (Pendiente_Copiar_Portapapeles)
                        {
                            /*Pendiente_Copiar_Portapapeles = false;
                            Bitmap Imagen = new Bitmap(Ancho_Cliente, Alto_Cliente, PixelFormat.Format24bppRgb);
                            Pintar = Graphics.FromImage(Imagen);
                            Pintar.Clear(Variable_Color_Fondo);
                            Pintar.CompositingMode = CompositingMode.SourceOver;
                            Pintar.DrawImage(Picture.BackgroundImage, new Rectangle(0, 0, Ancho_Cliente, Alto_Cliente), new Rectangle(0, 0, Ancho_Cliente, Alto_Cliente), GraphicsUnit.Pixel);
                            Pintar.Dispose();
                            Pintar = null;
                            try
                            {
                                Clipboard.SetImage(Imagen);
                                SystemSounds.Asterisk.Play();
                            }
                            catch { MessageBox.Show(this, "The program couldn't copy the map to the clipboard.\r\nPlease try it again in a while.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                            Imagen.Dispose();
                            Imagen = null;*/
                        }
                        if (Pendiente_Copiar_Ventana_Portapapeles)
                        {
                            /*Pendiente_Copiar_Ventana_Portapapeles = false;
                            Bitmap Imagen = new Bitmap(this.Width, this.Height, PixelFormat.Format24bppRgb);
                            this.DrawToBitmap(Imagen, new Rectangle(0, 0, this.Width, this.Height));
                            try
                            {
                                Clipboard.SetImage(Imagen);
                                SystemSounds.Asterisk.Play();
                            }
                            catch { MessageBox.Show(this, "The program couldn't copy the map to the clipboard.\r\nPlease try it again in a while.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                            Imagen.Dispose();
                            Imagen = null;*/
                        }
                        if (Pendiente_Redimensionar_Mapa)
                        {
                            /*Pendiente_Redimensionar_Mapa = false;
                            Picture.BackgroundImage = new Bitmap(Ancho_Cliente, Alto_Cliente, PixelFormat.Format32bppArgb);
                        */}
                    }
                    if (Pendiente_Dibujar_Mapa && !Ocupado)
                    {
                        if (!Subproceso_Activo)
                        {
                            if (Pendiente_Subproceso_Abortar)
                            {
                                Minecraft.Pendiente_Subproceso_Abortar = false;
                                Pendiente_Subproceso_Abortar = false;
                            }
                            /*if (Pendiente_Vaciar_Caché_Overworld)
                            {
                                Diccionario_Caché_Regiones_Overworld.Clear();
                                Pendiente_Vaciar_Caché_Overworld = false;
                            }
                            if (Pendiente_Vaciar_Caché_Nether)
                            {
                                Diccionario_Caché_Regiones_Nether.Clear();
                                Pendiente_Vaciar_Caché_Nether = false;
                            }
                            if (Pendiente_Vaciar_Caché_The_End)
                            {
                                Diccionario_Caché_Regiones_The_End.Clear();
                                Pendiente_Vaciar_Caché_The_End = false;
                            }*/
                            if (!Pendiente_Subproceso_Abortar_Global)
                            {
                                Pendiente_Dibujar_Mapa = false;
                                Subproceso_Activo = true;
                                Picture.Image = null;
                                Picture.Cursor = Cursors.WaitCursor;
                                Subproceso = new Thread(new ParameterizedThreadStart(Subproceso_DoWork));
                                Subproceso.IsBackground = true;
                                Subproceso.Priority = ThreadPriority.Normal;
                                Subproceso.Start();
                            }
                            else
                            {
                                Temporizador_Principal.Stop();
                                this.Close();
                            }
                        }
                        else if (!Pendiente_Subproceso_Abortar)
                        {
                            Minecraft.Pendiente_Subproceso_Abortar = true;
                            Pendiente_Subproceso_Abortar = true;
                        }
                    }
                }
                else if (Pendiente_Dibujar_Mapa) Pendiente_Dibujar_Mapa = false; // Si no hay un mapa cargado cancelar el dibujo cada vez
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Abrir_Ruta_Nivel()
        {
            try
            {
                Ocupado = true;
                if (!string.IsNullOrEmpty(Ruta_Nivel))
                {
                    string Dimensión = Variable_Dimensión;
                    ComboBox_Dimensión.Items.Clear();
                    /*Numérico_X_De.Minimum = 0;
                    Numérico_Z_De.Minimum = 0;
                    Numérico_X_De.Maximum = 0;
                    Numérico_Z_De.Maximum = 0;
                    Numérico_X_De.Value = 0;
                    Numérico_Z_De.Value = 0;*/
                    Barra_Estado_Etiqueta_Regiones.Text = "Regions: 0";
                    Barra_Estado_Etiqueta_Coordenadas_XZ_Mínimas.Text = "Min. XZ: 0, 0";
                    Barra_Estado_Etiqueta_Coordenadas_XZ_Máximas.Text = "Max. XZ: 0, 0";
                    Barra_Estado_Etiqueta_Dimensiones.Text = "Dimensions: 0 x 0";
                    Barra_Estado_Etiqueta_Bloques_Visibles.Text = "Visible blocks: 0";
                    //Pendiente_Vaciar_Caché_Overworld = true;
                    //Pendiente_Vaciar_Caché_Nether = true;
                    //Pendiente_Vaciar_Caché_The_End = true;
                    //Ruta_Nivel = Ruta_Carpeta;
                    //Ruta_Regiones_Overworld = Ruta_Nivel + "\\region";
                    //Ruta_Regiones_Nether = Ruta_Nivel + "\\DIM-1\\region";
                    //Ruta_Regiones_The_End = Ruta_Nivel + "\\DIM1\\region";
                    //Diccionario_Rutas_Regiones_Overworld = Minecraft.Obtener_Rutas_Regiones(Ruta_Regiones_Overworld);
                    //Diccionario_Rutas_Regiones_Nether = Minecraft.Obtener_Rutas_Regiones(Ruta_Regiones_Nether);
                    //Diccionario_Rutas_Regiones_The_End = Minecraft.Obtener_Rutas_Regiones(Ruta_Regiones_The_End);

                    // Reiniciar todas las variables:
                    foreach (KeyValuePair<string, string> Entrada in Diccionario_Dimensiones_Rutas)
                    {
                        Diccionario_Dimensiones_Lista_Posiciones_Regiones[Entrada.Key].Clear();
                        Diccionario_Dimensiones_Caché_Regiones[Entrada.Key].Clear();
                    }
                    Diccionario_Dimensiones_Rutas.Clear();
                    Diccionario_Dimensiones_Límites.Clear();
                    Diccionario_Dimensiones_Lista_Posiciones_Regiones.Clear();
                    Diccionario_Dimensiones_Caché_Regiones.Clear();
                    GC.Collect();
                    GC.GetTotalMemory(true);

                    // Intentar cargar el mundo:
                    Diccionario_Dimensiones_Rutas = Minecraft.Obtener_Diccionario_Rutas_Dimensiones(Ruta_Nivel);
                    if (Diccionario_Dimensiones_Rutas.Count > 0)
                    {
                        foreach (KeyValuePair<string, string> Entrada in Diccionario_Dimensiones_Rutas)
                        {
                            Rectangle Rectángulo;
                            Diccionario_Dimensiones_Lista_Posiciones_Regiones.Add(Entrada.Key, Minecraft.Obtener_Rutas_Regiones(Entrada.Value, out Rectángulo));
                            Diccionario_Dimensiones_Límites.Add(Entrada.Key, Rectángulo);
                            Diccionario_Dimensiones_Caché_Regiones.Add(Entrada.Key, new List<Minecraft.Regiones>());
                            ComboBox_Dimensión.Items.Add(Entrada.Key);
                        }
                        if (ComboBox_Dimensión.Items.Count > 0)
                        {
                            if (ComboBox_Dimensión.Items.Contains(Dimensión)) ComboBox_Dimensión.Text = Dimensión;
                            else ComboBox_Dimensión.SelectedIndex = 0;
                        }
                        Variable_Dimensión = "Overworld"; // 2018_04_09_10_14_28_067
                        Ocupado = false;
                        Pendiente_Dibujar_Mapa = true;
                        /*Mundo = AnvilWorld.Open(Ruta_Carpeta);
                        if (Mundo != null)
                        {
                            Chunks = Mundo.GetChunkManager(ComboBox_Dimensión.SelectedIndex == 0 ? 0 : ComboBox_Dimensión.SelectedIndex == 1 ? -1 : 1);
                            if (Chunks != null)
                            {
                                int Total_Chunks = 0;
                                foreach (IChunk Chunk in Chunks) Total_Chunks++;
                                Texto_Chunks = Program.Traducir_Número(Total_Chunks);
                                Texto_Semilla = Program.Traducir_Número(Mundo.Level.RandomSeed);
                                Ocupado = true;
                                Numérico_X.Value = Mundo.Level.Spawn.X;
                                //Numérico_Y.Value = Mundo.Level.Spawn.Y;
                                Numérico_Y.Value = ComboBox_Dimensión.SelectedIndex == 0 || ComboBox_Dimensión.SelectedIndex == 2 ? 255 : 64;
                                Numérico_Z.Value = Mundo.Level.Spawn.Z;
                                Ocupado = false;
                                Dibujar_Pendiente = true;
                                break;
                            }
                            else
                            {
                                Mundo = null;
                                Chunks = null;
                            }
                        }
                        else
                        {
                            Mundo = null;
                            Chunks = null;
                        }*/
                    }
                    else // Cancelar del todo la carga
                    {
                        Ruta_Nivel = null;
                    }
                    Dimensión = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            finally
            {
                //this.Cursor = Cursors.Default;
                Ocupado = false;
            }
        }

        internal void Subproceso_DoWork(object Objeto)
        {
            try
            {
                Stopwatch Cronómetro = Stopwatch.StartNew();
                string Variable_Temporal_Dimensión = Variable_Dimensión;
                bool Habilitar_Cargar_IDs = true;
                bool Habilitar_Cargar_Luz_Bloques = true;
                bool Habilitar_Cargar_Luz_Cielo = true;
                bool Habilitar_Cargar_Biomas = true;
                bool Habilitar_Cargar_Mapa_Altura = true;

                // Make the XYZ selection valid, even if it's inverted
                int X_Noroeste = Math.Min(Variable_X_De, Variable_X_A);
                int Y_Noroeste = Math.Min(Variable_Y_De, Variable_Y_A);
                int Z_Noroeste = Math.Min(Variable_Z_De, Variable_Z_A);
                int X_Sureste = Math.Max(Variable_X_De, Variable_X_A);
                int Y_Sureste = Math.Max(Variable_Y_De, Variable_Y_A);
                int Z_Sureste = Math.Max(Variable_Z_De, Variable_Z_A);

                Rectangle Rectángulo = new Rectangle(0, 0, 1, 1);
                Dictionary<Point, Point> Diccionario_Posiciones_Regiones_Bloques = new Dictionary<Point, Point>();
                List<Point> Lista_Posiciones_Regiones = new List<Point>();
                List<List<Point>> Lista_Posiciones_Chunks = new List<List<Point>>();
                List<List<Point>> Lista_Posiciones_Pintar = new List<List<Point>>();

                // Align the selected start coordinates to a chunk start, and take it's difference
                int Pintar_X = -(X_Noroeste % 16);
                int Pintar_Z = -(Z_Noroeste % 16);
                //int Pintar_X = -(16 - (X_Noroeste % 16));
                //int Pintar_Z = -(16 - (Z_Noroeste % 16));

                int Chunks_Dibujados = 0;
                int Total_Chunks = 0;

                int Ancho = (X_Sureste - X_Noroeste) + 1;
                int Alto = (Z_Sureste - Z_Noroeste) + 1;
                int Repeticiones = ((Y_Sureste - Y_Noroeste) + 1) * Alto;

                // Add the regions and chunks that need to be loaded later
                for (int Índice_Z = 0; Índice_Z < Alto; Índice_Z += 16)
                {
                    for (int Índice_X = 0; Índice_X < Ancho; Índice_X += 16)
                    {
                        int Región_X = (X_Noroeste + Índice_X) / 512;
                        int Región_Z = (Z_Noroeste + Índice_Z) / 512;
                        if (Región_X * 512 > (X_Noroeste + Índice_X)) Región_X--;
                        if (Región_Z * 512 > (Z_Noroeste + Índice_Z)) Región_Z--;
                        int Chunk_X = ((X_Noroeste + Índice_X) - (Región_X * 512)) / 16;
                        int Chunk_Z = ((Z_Noroeste + Índice_Z) - (Región_Z * 512)) / 16;
                        Point Posición_Región = new Point(Región_X, Región_Z);
                        Point Posición_Chunk = new Point(Chunk_X, Chunk_Z);
                        Point Posición_Pintar = new Point(Pintar_X + Índice_X, Pintar_Z + Índice_Z);
                        if (!Lista_Posiciones_Regiones.Contains(Posición_Región))
                        {
                            Lista_Posiciones_Regiones.Add(Posición_Región);
                            Lista_Posiciones_Chunks.Add(new List<Point>(new Point[] { Posición_Chunk }));
                            Lista_Posiciones_Pintar.Add(new List<Point>(new Point[] { Posición_Pintar }));
                            Total_Chunks++;
                        }
                        else
                        {
                            int Índice_Región = Lista_Posiciones_Regiones.IndexOf(Posición_Región);
                            if (!Lista_Posiciones_Chunks[Índice_Región].Contains(Posición_Chunk))
                            {
                                Lista_Posiciones_Chunks[Índice_Región].Add(Posición_Chunk);
                                Lista_Posiciones_Pintar[Índice_Región].Add(Posición_Pintar);
                                Total_Chunks++;
                            }
                        }
                    }
                }
                Bitmap Imagen = new Bitmap(Ancho, Repeticiones, PixelFormat.Format24bppRgb);
                Graphics Pintar = Graphics.FromImage(Imagen);
                Pintar.CompositingMode = CompositingMode.SourceCopy;
                for (int Índice_Región = 0; Índice_Región < Lista_Posiciones_Regiones.Count; Índice_Región++)
                {
                    if (Pendiente_Subproceso_Abortar) return;
                    Minecraft.Regiones Región = new Minecraft.Regiones(Point.Empty);
                    /*if (Diccionario_Dimensiones_Caché_Regiones[Variable_Temporal_Dimensión].Count > 0)
                    {
                        foreach (Minecraft.Regiones Región_Temporal in Diccionario_Dimensiones_Caché_Regiones[Variable_Temporal_Dimensión])
                        {
                            if (Región_Temporal.Posición == Lista_Posiciones_Regiones[Índice_Región] && Región_Temporal.Iniciada)
                            {
                                Región = Región_Temporal;
                                break;
                            }
                        }
                    }*/
                    if (!Región.Iniciada)
                    {
                        this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [Loading the region " + Program.Traducir_Número(Índice_Región + 1) + " of " + Program.Traducir_Número(Lista_Posiciones_Regiones.Count) + ", please wait...]" });
                        Región = Minecraft.Cargar_Región(Diccionario_Dimensiones_Rutas[Variable_Temporal_Dimensión], Lista_Posiciones_Regiones[Índice_Región], Habilitar_Cargar_IDs, Habilitar_Cargar_Luz_Bloques, Habilitar_Cargar_Luz_Cielo, Habilitar_Cargar_Biomas, Habilitar_Cargar_Mapa_Altura);
                        //if (Región.Iniciada) Diccionario_Dimensiones_Caché_Regiones[Variable_Temporal_Dimensión].Add(Región);
                    }
                    if (Pendiente_Subproceso_Abortar) return;
                    if (Región.Iniciada && Región.Matriz_Chunks != null && Región.Matriz_Chunks.Length > 0)
                    {
                        Barra_Estado.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Versión_Minecraft, "Minecraft: " + (!Región.Minecraft_1_13 ? "1.12.2-" : "1.13+") });
                        this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [Drawing the blocks, please wait...]" });
                        for (int Índice_Chunk = 0; Índice_Chunk < Lista_Posiciones_Chunks[Índice_Región].Count; Índice_Chunk++)
                        {
                            if (Pendiente_Subproceso_Abortar) return;
                            Point Posición_Chunk = Lista_Posiciones_Chunks[Índice_Región][Índice_Chunk];
                            Point Posición_Dibujar = Lista_Posiciones_Pintar[Índice_Región][Índice_Chunk];
                            Rectángulo.Location = Posición_Dibujar;
                            if (Región.Matriz_Chunks[Posición_Chunk.X, Posición_Chunk.Y].Matriz_Bytes_IDs != null)
                            {
                                for (int Índice_Bloque_Z = 0; Índice_Bloque_Z < 16; Índice_Bloque_Z++, Rectángulo.Y++)
                                {
                                    Rectángulo.X = Posición_Dibujar.X;
                                    for (int Índice_Bloque_X = 0; Índice_Bloque_X < 16; Índice_Bloque_X++, Rectángulo.X++)
                                    {
                                        int Rectángulo_Y = Rectángulo.Y;
                                        for (int Índice_Bloque_Y = Y_Noroeste; Índice_Bloque_Y <= Y_Sureste; Índice_Bloque_Y++, Rectángulo.Y += Alto)
                                        {
                                            if (Pendiente_Subproceso_Abortar) return;
                                            short ID = Región.Matriz_Chunks[Posición_Chunk.X, Posición_Chunk.Y].Matriz_Bytes_IDs[Índice_Bloque_X, Índice_Bloque_Y, Índice_Bloque_Z];
                                            if (Minecraft.Diccionario_Bloques_Índices_Colores.ContainsKey(ID))
                                            {
                                                SolidBrush Pincel = new SolidBrush(Minecraft.Diccionario_Bloques_Índices_Colores[ID]);
                                                Pintar.FillRectangle(Pincel, Rectángulo);
                                                Pincel.Dispose();
                                                Pincel = null;
                                            }
                                        }
                                        Rectángulo.Y = Rectángulo_Y;
                                    }
                                }
                                //Picture.Invoke(new Invocación.Delegado_Control_Invalidate_Rectangle(Invocación.Ejecutar_Delegado_Control_Invalidate_Rectangle), new object[] { Picture, new Rectangle(Posición_Dibujar, new Size(16, 16)) });
                            }
                            this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [Drawing the chunk " + Program.Traducir_Número(Chunks_Dibujados) + " of " + Program.Traducir_Número(Total_Chunks) + "...]" });
                        }
                    }
                }
                Pintar.Dispose();
                Pintar = null;
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Exportador_Estructuras_Pintadas);
                Imagen.Save(Program.Ruta_Guardado_Imágenes_Exportador_Estructuras_Pintadas + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " Painted structure (" + Program.Traducir_Número(Ancho) + " x " + Program.Traducir_Número((Y_Sureste - Y_Noroeste) + 1) + " x " + Program.Traducir_Número(Alto) + ").png", ImageFormat.Png);
                Picture.Invoke(new Invocación.Delegado_PictureBox_Image(Invocación.Ejecutar_Delegado_PictureBox_Image), new object[] { Picture, Imagen });
                //Picture.Invoke(new Invocación.Delegado_Control_Invalidate(Invocación.Ejecutar_Delegado_Control_Invalidate), new object[] { Picture });
                this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [Done in " + Program.Traducir_Intervalo_Minutos_Segundos(Cronómetro.Elapsed) + ", visible chunks: " + Program.Traducir_Número(Chunks_Dibujados) + "]" });
                Cronómetro.Stop();
                Cronómetro = null;
                //Picture.Update();
            }
            catch (ThreadAbortException) { } // Cancelado, no registrar el error
            catch (OutOfMemoryException Excepción)
            {
                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                this.Text = Texto_Título + " - [The program ran out of memory... Sorry]";
            }
            catch (Exception Excepción)
            {
                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                try { this.Text = Texto_Título + " - [An exception has been registered...]"; }
                catch { }
            }
            finally
            {
                Picture.Invoke(new Invocación.Delegado_Control_Cursor(Invocación.Ejecutar_Delegado_Control_Cursor), new object[] { Picture, Cursors.Hand });
                Minecraft.Pendiente_Subproceso_Abortar = false;
                Pendiente_Subproceso_Abortar = false;
                Subproceso_Activo = false;
                Subproceso = null;
                GC.Collect();
                GC.GetTotalMemory(true);
            }
        }

        private void ComboBox_Dimensión_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Dimensión = ComboBox_Dimensión.Text;
                //Registro_Guardar_Opciones();
                Pendiente_Dibujar_Mapa = true;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_X_De_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Numérico_X_De.Refresh();
                Variable_X_De = (int)Numérico_X_De.Value;
                //Registro_Guardar_Opciones();
                Pendiente_Dibujar_Mapa = true;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_Y_De_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Numérico_Y_De.Refresh();
                Variable_Y_De = (int)Numérico_Y_De.Value;
                //Registro_Guardar_Opciones();
                Pendiente_Dibujar_Mapa = true;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_Z_De_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Numérico_Z_De.Refresh();
                Variable_Z_De = (int)Numérico_Z_De.Value;
                //Registro_Guardar_Opciones();
                Pendiente_Dibujar_Mapa = true;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_X_A_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Numérico_X_A.Refresh();
                Variable_X_A = (int)Numérico_X_A.Value;
                //Registro_Guardar_Opciones();
                Pendiente_Dibujar_Mapa = true;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_Y_A_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Numérico_Y_A.Refresh();
                Variable_Y_A = (int)Numérico_Y_A.Value;
                //Registro_Guardar_Opciones();
                Pendiente_Dibujar_Mapa = true;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_Z_A_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Numérico_Z_A.Refresh();
                Variable_Z_A = (int)Numérico_Z_A.Value;
                //Registro_Guardar_Opciones();
                Pendiente_Dibujar_Mapa = true;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }
    }
}
