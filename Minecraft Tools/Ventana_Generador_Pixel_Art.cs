using ImageMagick;
using Microsoft.Win32;
using Substrate;
using Substrate.Core;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Generador_Pixel_Art : Form
    {
        public Ventana_Generador_Pixel_Art()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Pixel Art Generator by Jupisoft for " + Program.Texto_Usuario;

        internal static List<short> Lista_Paleta = null;
        internal static bool Limitar_Altura_Forzar_Altura = false;
        internal static int Limitar_Altura = 256;
        internal static bool Construir_Paredes_Cristal = true;
        internal static DitherMethod Interpolación = DitherMethod.FloydSteinberg;

        internal Stopwatch Cronómetro_Memoria = new Stopwatch(); // Turn the text red when over 4 GB

        internal string Ruta_Original = null;
        internal Bitmap Imagen_Original = null;
        internal bool Variable_Siempre_Visible = false;
        internal bool Tamaño_16 = true;
        internal int Ancho = 0;
        internal int Alto = 0;
        internal int Ancho_Original = 0;
        internal int Alto_Original = 0;

        private void Ventana_Generador_Pixel_Art_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título + " - [Current Block Palette: " + Program.Traducir_Número(Lista_Paleta != null ? Lista_Paleta.Count : 0) + ((Lista_Paleta != null ? Lista_Paleta.Count : 0) != 1 ? " Blocks" : " Block") + "]";
                this.WindowState = FormWindowState.Maximized;
                Barra_Estado_Etiqueta_Imagen_Paleta.Image = Obtener_Imagen_Paleta();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Generador_Pixel_Art_Shown(object sender, EventArgs e)
        {
            try
            {
                Registro_Cargar_Opciones();
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Generador_Pixel_Art_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
                Cronómetro_Memoria.Reset();
                Cronómetro_Memoria = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Generador_Pixel_Art_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Generador_Pixel_Art_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal bool Abrir_Ruta_Imagen(string Ruta)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                    Image Imagen_Original = null;
                    try { Imagen_Original = Image.FromStream(Lector, false, false); }
                    catch { Imagen_Original = null; }
                    if (Imagen_Original != null)
                    {
                        Ruta_Original = Ruta;
                        Picture_1.BackgroundImage = null;
                        Picture_2.BackgroundImage = null;
                        this.Imagen_Original = null;
                        Ancho_Original = Imagen_Original.Width;
                        Alto_Original = Imagen_Original.Height;
                        if (Limitar_Altura != 0 && (Limitar_Altura_Forzar_Altura || Alto_Original > Limitar_Altura))
                        {
                            Ancho = (int)Math.Round(((double)Ancho_Original * (double)Limitar_Altura) / (double)Alto_Original, MidpointRounding.AwayFromZero);
                            Alto = Limitar_Altura;
                        }
                        else
                        {
                            Ancho = Ancho_Original;
                            Alto = Alto_Original;
                        }
                        Barra_Estado_Etiqueta_Dimensiones_Imagen_Original.Text = "Dimensions of the image: " + Program.Traducir_Número(Ancho_Original) + " x " + Program.Traducir_Número(Alto_Original);
                        Barra_Estado_Etiqueta_Dimensiones_Imagen_Bloques.Text = "Dimensions of the image of blocks: " + Program.Traducir_Número(Ancho * 16) + " x " + Program.Traducir_Número(Alto * 16);
                        Barra_Estado_Etiqueta_Dimensiones_Pixel_Art.Text = "Dimensions of the pixel art: " + Program.Traducir_Número(Ancho) + " x " + Program.Traducir_Número(Alto);
                        Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                        Graphics Pintar = Graphics.FromImage(Imagen);
                        Pintar.CompositingMode = CompositingMode.SourceCopy;
                        /*Pintar.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar.SmoothingMode = SmoothingMode.None;
                        Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;*/
                        Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho_Original, Alto_Original), GraphicsUnit.Pixel);
                        Pintar.Dispose();
                        Pintar = null;
                        this.Imagen_Original = Imagen;
                        Lector.Close();
                        Lector.Dispose();
                        Lector = null;
                        Generar_Imagen_Pixel_Art_Cuantizada();
                        return true;
                    }
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                }
            }
            catch (Exception Excepción)
            {
                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                Picture_1.BackgroundImage = null;
                Picture_2.BackgroundImage = null;
                Barra_Estado_Etiqueta_Dimensiones_Imagen_Original.Text = "Dimensions of the original image: 0 x 0";
                Barra_Estado_Etiqueta_Dimensiones_Imagen_Bloques.Text = "Dimensions of the image made of blocks: 0 x 0";
                Barra_Estado_Etiqueta_Dimensiones_Pixel_Art.Text = "Dimensions of the pixel art: 0 x 0";
            }
            finally { this.Cursor = Cursors.Default; }
            return false;
        }

        private void Ventana_Generador_Pixel_Art_DragDrop(object sender, DragEventArgs e)
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
                            try { if (Abrir_Ruta_Imagen(Ruta)) break; }
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

        private void Ventana_Generador_Pixel_Art_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Alt && !e.Control && !e.Shift)
                {
                    if (e.KeyCode == Keys.Escape) this.Close();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Generador_Pixel_Art_SizeChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal Bitmap Generar_Imagen_Pixel_Art_Cuantizada()
        {
            try
            {
                if (this.Imagen_Original != null)
                {
                    this.Text = Texto_Título + " - [Current Block Palette: " + Program.Traducir_Número(Lista_Paleta != null ? Lista_Paleta.Count : 0) + ((Lista_Paleta != null ? Lista_Paleta.Count : 0) != 1 ? " Blocks" : " Block") + ", please wait up to a few minutes...]";
                    Picture_1.BackgroundImage = Imagen_Original;
                    Picture_2.BackgroundImage = null;
                    Dictionary<short, Color> Diccionario_Paleta = new Dictionary<short, Color>();
                    Dictionary<int, short> Diccionario_Códigos_Hash = new Dictionary<int, short>();
                    bool Paleta_Vacía = Lista_Paleta == null || Lista_Paleta.Count <= 0;
                    if (Paleta_Vacía) Lista_Paleta = new List<short>();
                    foreach (KeyValuePair<short, string> Entrada in Minecraft.Diccionario_Bloques_Índices_Nombres)
                    {
                        if (Paleta_Vacía || Lista_Paleta.Contains(Entrada.Key))
                        {
                            int Código_Hash = Minecraft.Diccionario_Bloques_Índices_Colores[Entrada.Key].GetHashCode();
                            if (!Diccionario_Códigos_Hash.ContainsKey(Código_Hash))
                            {
                                //if (Minecraft.Diccionario_Texturas[Entrada.Key] == null) MessageBox.Show(Entrada.Value);
                                Diccionario_Paleta.Add(Entrada.Key, Minecraft.Diccionario_Bloques_Índices_Colores[Entrada.Key]);
                                Diccionario_Códigos_Hash.Add(Código_Hash, Entrada.Key);
                                if (Paleta_Vacía) Lista_Paleta.Add(Entrada.Key);
                            }
                        }
                    }
                    if (Diccionario_Paleta.Count > 0)
                    {
                        //MessageBox.Show("Colores de la paleta: " + Diccionario_Paleta.Count.ToString());
                        Bitmap Imagen_Paleta = new Bitmap(Diccionario_Paleta.Count, 1, PixelFormat.Format24bppRgb);
                        Graphics Pintar_Paleta = Graphics.FromImage(Imagen_Paleta);
                        Pintar_Paleta.CompositingMode = CompositingMode.SourceCopy;
                        int Índice_X = 0;
                        foreach (KeyValuePair<short, Color> Entrada in Diccionario_Paleta)
                        {
                            SolidBrush Pincel = new SolidBrush(Entrada.Value);
                            Pintar_Paleta.FillRectangle(Pincel, Índice_X, 0, 1, 1);
                            Pincel.Dispose();
                            Pincel = null;
                            Índice_X++;
                        }
                        Pintar_Paleta.Dispose();
                        Pintar_Paleta = null;

                        MagickImage Imagen_Mapa = new MagickImage(Imagen_Paleta);
                        MagickImage Imagen_Mapeada = new MagickImage(this.Imagen_Original.Clone() as Bitmap);

                        QuantizeSettings Ajustes_Cuantización = new QuantizeSettings();
                        //Ajustes_Cuantización.Colors = Diccionario_Paleta.Count; // 2
                        //Ajustes_Cuantización.ColorSpace = ColorSpace.RGB;
                        Ajustes_Cuantización.DitherMethod = Interpolación;
                        //Ajustes_Cuantización.MeasureErrors = false;
                        //Ajustes_Cuantización.TreeDepth = Diccionario_Paleta.Count; // 8

                        Imagen_Mapeada.Map(Imagen_Mapa, Ajustes_Cuantización);

                        Bitmap Imagen_Cuantizada = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                        Graphics Pintar_Cuantizada = Graphics.FromImage(Imagen_Cuantizada);
                        Pintar_Cuantizada.CompositingMode = CompositingMode.SourceCopy;
                        /*Pintar_Cuantizada.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar_Cuantizada.InterpolationMode = InterpolationMode.NearestNeighbor;
                        Pintar_Cuantizada.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar_Cuantizada.SmoothingMode = SmoothingMode.None;*/
                        Pintar_Cuantizada.DrawImage(Imagen_Mapeada.ToBitmap(ImageFormat.Png), new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                        Pintar_Cuantizada.Dispose();
                        Pintar_Cuantizada = null;
                        Picture_2.BackgroundImage = Imagen_Cuantizada;
                        Imagen_Paleta.Dispose();
                        Imagen_Paleta = null;
                        Imagen_Mapa.Dispose();
                        Imagen_Mapa = null;
                        Imagen_Mapeada.Dispose();
                        Imagen_Mapeada = null;
                    }
                    Diccionario_Paleta = null;
                    Diccionario_Códigos_Hash = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            finally { this.Text = Texto_Título + " - [Current Block Palette: " + Program.Traducir_Número(Lista_Paleta != null ? Lista_Paleta.Count : 0) + ((Lista_Paleta != null ? Lista_Paleta.Count : 0) != 1 ? " Blocks" : " Block") + "]"; }
            return null;
        }

        internal void Generar_Imagen_Pixel_Art_Bloques(bool Copiar, bool Guardar)
        {
            try
            {
                if (Picture_2.BackgroundImage != null && Lista_Paleta != null && Lista_Paleta.Count > 0 && (Copiar || Guardar))
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.Text = Texto_Título + " - [Current Block Palette: " + Program.Traducir_Número(Lista_Paleta != null ? Lista_Paleta.Count : 0) + ((Lista_Paleta != null ? Lista_Paleta.Count : 0) != 1 ? " Blocks" : " Block") + ", please wait up to a few minutes...]";
                    Dictionary<short, Color> Diccionario_Paleta = new Dictionary<short, Color>();
                    Dictionary<int, short> Diccionario_Códigos_Hash = new Dictionary<int, short>();
                    foreach (KeyValuePair<short, string> Entrada in Minecraft.Diccionario_Bloques_Índices_Nombres)
                    {
                        if (Lista_Paleta.Contains(Entrada.Key))
                        {
                            int Código_Hash = Minecraft.Diccionario_Bloques_Índices_Colores[Entrada.Key].GetHashCode();
                            if (!Diccionario_Códigos_Hash.ContainsKey(Código_Hash))
                            {
                                //if (Minecraft.Diccionario_Texturas[Entrada.Key] == null) MessageBox.Show(Entrada.Value);
                                Diccionario_Paleta.Add(Entrada.Key, Minecraft.Diccionario_Bloques_Índices_Colores[Entrada.Key]);
                                Diccionario_Códigos_Hash.Add(Código_Hash, Entrada.Key);
                            }
                        }
                    }
                    BitmapData Bitmap_Data = ((Bitmap)Picture_2.BackgroundImage).LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadOnly, Picture_2.BackgroundImage.PixelFormat);
                    byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                    int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Picture_2.BackgroundImage.PixelFormat)) / 8);
                    ((Bitmap)Picture_2.BackgroundImage).UnlockBits(Bitmap_Data);
                    Bitmap_Data = null;

                    int Zoom = 16;
                    int Ancho_Zoom = Ancho * Zoom;
                    int Alto_Zoom = Alto * Zoom;
                    Bitmap Imagen = null;
                    try { Imagen = new Bitmap(Ancho_Zoom, Alto_Zoom, PixelFormat.Format24bppRgb); }
                    catch { Imagen = null; }
                    if (Imagen == null)
                    {
                        MessageBox.Show(this, "The image made of blocks couldn't be created, it was too big.\r\nIntented dimensions: " + Program.Traducir_Número(Ancho_Zoom) + " x " + Program.Traducir_Número(Alto_Zoom) + (Ancho_Zoom * Alto_Zoom != 1 ? " pixels." : " pixel."), Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Matriz_Bytes = null;
                        return;
                    }
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    for (int Y = 0, Pintar_Y = 0, Índice = 0; Y < Alto; Y++, Pintar_Y += Zoom)
                    {
                        for (int X = 0, Pintar_X = 0; X < Ancho; X++, Pintar_X += Zoom, Índice += 4)
                        {
                            Color Color_ARGB = Color.FromArgb(255/*Matriz_Bytes[Índice + 3]*/, Matriz_Bytes[Índice + 2], Matriz_Bytes[Índice + 1], Matriz_Bytes[Índice]);
                            int Código_Hash = Color_ARGB.GetHashCode();
                            if (Diccionario_Códigos_Hash.ContainsKey(Código_Hash))
                            {
                                if (Minecraft.Diccionario_Texturas[Diccionario_Códigos_Hash[Código_Hash]] != null) Pintar.DrawImage(Minecraft.Diccionario_Texturas[Diccionario_Códigos_Hash[Código_Hash]], new Rectangle(Pintar_X, Pintar_Y, Zoom, Zoom), new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                                else
                                {
                                    SolidBrush Pincel = new SolidBrush(Color_ARGB);
                                    Pintar.FillRectangle(Pincel, Pintar_X, Pintar_Y, Zoom, Zoom);
                                    Pincel.Dispose();
                                    Pincel = null;
                                }
                            }
                            //else MessageBox.Show(Matriz_Bytes[Índice + 3].ToString() + ", " + Matriz_Bytes[Índice + 2].ToString() + ", " + Matriz_Bytes[Índice + 1].ToString() + ", " + Matriz_Bytes[Índice].ToString());
                        }
                    }
                    Pintar.Dispose();
                    Pintar = null;
                    if (Copiar)
                    {
                        Clipboard.SetImage(Imagen);
                        SystemSounds.Asterisk.Play();
                    }
                    if (Guardar)
                    {
                        Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Pixel_Art);
                        Imagen.Save(Program.Ruta_Guardado_Imágenes_Pixel_Art + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " Pixel art blocks (" + Program.Traducir_Número(Lista_Paleta.Count) + (Lista_Paleta.Count != 1 ? " blocks" : " block") + ").png", ImageFormat.Png);
                        SystemSounds.Asterisk.Play();
                    }
                    Imagen.Dispose();
                    Imagen = null;
                    Diccionario_Paleta = null;
                    Diccionario_Códigos_Hash = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            finally
            {
                GC.Collect();
                GC.GetTotalMemory(true);
                this.Text = Texto_Título + " - [Current Block Palette: " + Program.Traducir_Número(Lista_Paleta != null ? Lista_Paleta.Count : 0) + ((Lista_Paleta != null ? Lista_Paleta.Count : 0) != 1 ? " Blocks" : " Block") + "]";
                this.Cursor = Cursors.Default;
            }
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

        private void Menú_Contextual_Editar_Paleta_Actual_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Selector_Bloques Ventana = new Ventana_Selector_Bloques();
                if (Lista_Paleta != null)
                {
                    Ventana.Lista_Paleta_Original = new List<short>();
                    if (Lista_Paleta.Count > 0) Ventana.Lista_Paleta_Original.AddRange(Lista_Paleta.GetRange(0, Lista_Paleta.Count));
                }
                Ventana.Variable_Siempre_Visible = this.Variable_Siempre_Visible;
                if (Ventana.ShowDialog(this) == DialogResult.OK)
                {
                    if (Ventana.Lista_Paleta != null && Ventana.Lista_Paleta.Count > 0)
                    {
                        Lista_Paleta = new List<short>();
                        Lista_Paleta.AddRange(Ventana.Lista_Paleta.GetRange(0, Ventana.Lista_Paleta.Count));
                        Barra_Estado_Etiqueta_Imagen_Paleta.Image = Obtener_Imagen_Paleta();
                    }
                }
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Exportar_Mundo_Horizontal_Click(object sender, EventArgs e)
        {
            try
            {
                Exportar_Mundo_Minecraft(false);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Exportar_Mundo_Vertical_Click(object sender, EventArgs e)
        {
            try
            {
                Exportar_Mundo_Minecraft(true);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Copiar_Paleta_Click(object sender, EventArgs e)
        {
            try
            {
                if (Barra_Estado_Etiqueta_Imagen_Paleta.Image != null)
                {
                    Clipboard.SetImage(Barra_Estado_Etiqueta_Imagen_Paleta.Image);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture_2.BackgroundImage != null)
                {
                    Clipboard.SetImage(Picture_2.BackgroundImage);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Copiar_Bloques_Click(object sender, EventArgs e)
        {
            try
            {
                Generar_Imagen_Pixel_Art_Bloques(true, false);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Guardar_Paleta_Click(object sender, EventArgs e)
        {
            try
            {
                if (Barra_Estado_Etiqueta_Imagen_Paleta.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Pixel_Art);
                    Barra_Estado_Etiqueta_Imagen_Paleta.Image.Save(Program.Ruta_Guardado_Imágenes_Pixel_Art + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " Pixel art palette (" + Program.Traducir_Número(Lista_Paleta.Count) + (Lista_Paleta.Count != 1 ? " blocks" : " block") + ").png", ImageFormat.Png);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture_2.BackgroundImage != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Pixel_Art);
                    Picture_2.BackgroundImage.Save(Program.Ruta_Guardado_Imágenes_Pixel_Art + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " Pixel art quantized (" + Program.Traducir_Número(Lista_Paleta.Count) + (Lista_Paleta.Count != 1 ? " blocks" : " block") + ").png", ImageFormat.Png);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Guardar_Bloques_Click(object sender, EventArgs e)
        {
            try
            {
                Generar_Imagen_Pixel_Art_Bloques(false, true);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Preiniciar_Lista_Paleta()
        {
            try
            {
                Lista_Paleta = new List<short>();
                foreach (KeyValuePair<short, string> Entrada in Minecraft.Diccionario_Bloques_Índices_Nombres)
                {
                    if (!Minecraft.Diccionario_Texturas_Transparentes.ContainsKey(Entrada.Key) && !Minecraft.Diccionario_Bloques_Altura_Diferente.ContainsKey(Entrada.Key) && !Minecraft.Diccionario_Bloques_Minecraft_1_13.ContainsKey(Entrada.Key) && !Entrada.Value.Contains("mushroom") && !Entrada.Value.Contains("shulker") && !Entrada.Value.Contains("smooth") && string.Compare(Entrada.Value, "minecraft:dirt", true) != 0 && string.Compare(Entrada.Value, "minecraft:furnace", true) != 0)
                    {
                        Lista_Paleta.Add(Entrada.Key);
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static Bitmap Obtener_Imagen_Paleta()
        {
            try
            {
                if (Lista_Paleta != null && Lista_Paleta.Count > 0)
                {
                    Bitmap Imagen = new Bitmap(Lista_Paleta.Count, 20, PixelFormat.Format24bppRgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.FillRectangle(Program.Pincel_Fondo, 0, 0, Lista_Paleta.Count, 20);
                    Pintar.CompositingMode = CompositingMode.SourceOver;
                    for (int Índice_Paleta = 0; Índice_Paleta < Lista_Paleta.Count; Índice_Paleta++)
                    {
                        if (Minecraft.Diccionario_Bloques_Índices_Colores.ContainsKey(Lista_Paleta[Índice_Paleta]))
                        {
                            SolidBrush Pincel = new SolidBrush(Minecraft.Diccionario_Bloques_Índices_Colores[Lista_Paleta[Índice_Paleta]]);
                            Pintar.FillRectangle(Pincel, Índice_Paleta, 0, 1, 20);
                            Pincel.Dispose();
                            Pincel = null;
                        }
                    }
                    Pintar.Dispose();
                    Pintar = null;
                    return Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        private void Menú_Contextual_Limitar_Altura_Forzar_Altura_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Limitar_Altura_Forzar_Altura = Menú_Contextual_Limitar_Altura_Forzar_Altura.Checked;
                Registro_Guardar_Opciones();
                Abrir_Ruta_Imagen(Ruta_Original);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Limitar_Altura_Click(object sender, EventArgs e)
        {
            try
            {
                for (int Índice = 0; Índice < Menú_Contextual_Limitar_Altura.DropDownItems.Count; Índice++) if (Menú_Contextual_Limitar_Altura.DropDownItems[Índice].GetType() == typeof(ToolStripMenuItem) && Menú_Contextual_Limitar_Altura.DropDownItems[Índice] != Menú_Contextual_Limitar_Altura_Forzar_Altura) ((ToolStripMenuItem)Menú_Contextual_Limitar_Altura.DropDownItems[Índice]).Checked = false;
                ToolStripMenuItem Menú = (ToolStripMenuItem)sender;
                Menú.Checked = true;
                Limitar_Altura = int.Parse(Menú.Name.Replace("Menú_Contextual_Limitar_Altura_", null));
                Registro_Guardar_Opciones();
                Abrir_Ruta_Imagen(Ruta_Original);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Construir_Paredes_Cristal_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Construir_Paredes_Cristal = Menú_Contextual_Construir_Paredes_Cristal.Checked;
                Registro_Guardar_Opciones();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal void Exportar_Mundo_Minecraft(bool Pixel_Art_Vertical)
        {
            try
            {
                if (Picture_2.BackgroundImage != null && Lista_Paleta != null && Lista_Paleta.Count > 0)
                {
                    if (!Pixel_Art_Vertical || (Pixel_Art_Vertical && Alto <= 256))
                    {
                        this.Cursor = Cursors.WaitCursor;
                        this.Text = Texto_Título + " - [Current Block Palette: " + Program.Traducir_Número(Lista_Paleta != null ? Lista_Paleta.Count : 0) + ((Lista_Paleta != null ? Lista_Paleta.Count : 0) != 1 ? " Blocks" : " Block") + ", please wait up to a few minutes...]";
                        string Ruta = Program.Ruta_Guardado_Minecraft + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " Pixel art " + (!Pixel_Art_Vertical ? "horizontal" : "vertical");
                        if (Directory.Exists(Ruta))
                        {
                            MessageBox.Show(this, "Somehow the directory name for the new Minecraft map already exists.\r\nPlease try it again if the system clock is running properly.\r\nPath: \"" + Ruta + "\".", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Ruta = null;
                            return;
                        }
                        Program.Crear_Carpetas(Ruta);
                        AnvilWorld Mundo = AnvilWorld.Create(Ruta);
                        IChunkManager Chunks = Mundo.GetChunkManager(0);
                        BlockManager Bloques = Mundo.GetBlockManager(0);
                        Mundo.Level.LevelName = Path.GetFileName(Ruta);
                        Mundo.Level.UseMapFeatures = false;
                        //Mundo.Level.GeneratorOptions = "1;minecraft:bedrock";
                        Mundo.Level.GameType = GameType.CREATIVE;
                        Mundo.Level.Spawn = !Pixel_Art_Vertical ? new SpawnPoint(0, 64, 0) : new SpawnPoint(0, 64, 15);
                        Mundo.Level.AllowCommands = true;
                        Mundo.Level.GameRules.DoMobSpawning = false;
                        Mundo.Level.GameRules.DoFireTick = false;
                        Mundo.Level.GameRules.MobGriefing = false;
                        Mundo.Level.GameRules.KeepInventory = true;
                        Mundo.Level.RainTime = 55555;
                        Mundo.Level.IsRaining = false;
                        Mundo.Level.Player = new Player();
                        Mundo.Level.Player.Dimension = 0;
                        Mundo.Level.Player.Position = new Vector3();
                        if (!Pixel_Art_Vertical)
                        {
                            Mundo.Level.Player.Position.X = 0;
                            Mundo.Level.Player.Position.Y = 64;
                            Mundo.Level.Player.Position.Z = 0;
                        }
                        else
                        {
                            Mundo.Level.Player.Position.X = 0;
                            Mundo.Level.Player.Position.Y = 64;
                            Mundo.Level.Player.Position.Z = 15;
                        }
                        Substrate.Orientation Orientación = new Substrate.Orientation();
                        Orientación.Pitch = 45d; // -90º a +90º // 25 = hacia abajo
                        Orientación.Yaw = !Pixel_Art_Vertical ? -45d : -135d; // -180º a +180º // 45 = Sureste
                        Mundo.Level.Player.Rotation = Orientación;
                        Mundo.Level.Player.Spawn = !Pixel_Art_Vertical ? new SpawnPoint(0, 64, 0) : new SpawnPoint(0, 64, 15);
                        Mundo.Level.Player.Abilities.Flying = true;
                        Mundo.Level.RandomSeed = 4;
                        Dictionary<short, Color> Diccionario_Paleta = new Dictionary<short, Color>();
                        Dictionary<int, short> Diccionario_Códigos_Hash = new Dictionary<int, short>();
                        foreach (KeyValuePair<short, string> Entrada in Minecraft.Diccionario_Bloques_Índices_Nombres)
                        {
                            if (Lista_Paleta.Contains(Entrada.Key))
                            {
                                int Código_Hash = Minecraft.Diccionario_Bloques_Índices_Colores[Entrada.Key].GetHashCode();
                                if (!Diccionario_Códigos_Hash.ContainsKey(Código_Hash))
                                {
                                    //if (Minecraft.Diccionario_Texturas[Entrada.Key] == null) MessageBox.Show(Entrada.Value);
                                    Diccionario_Paleta.Add(Entrada.Key, Minecraft.Diccionario_Bloques_Índices_Colores[Entrada.Key]);
                                    Diccionario_Códigos_Hash.Add(Código_Hash, Entrada.Key);
                                }
                            }
                        }
                        BitmapData Bitmap_Data = ((Bitmap)Picture_2.BackgroundImage).LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadOnly, Picture_2.BackgroundImage.PixelFormat);
                        byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                        Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                        int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Picture_2.BackgroundImage.PixelFormat)) / 8);
                        ((Bitmap)Picture_2.BackgroundImage).UnlockBits(Bitmap_Data);
                        Bitmap_Data = null;
                        bool Capa_Lecho_Roca = !Pixel_Art_Vertical || Limitar_Altura != 256;
                        if (!Pixel_Art_Vertical)
                        {
                            int Bloques_Ancho = 0;
                            int Bloques_Alto = 0;
                            for (int Y = -16, Chunk_Z = -1, Índice = 0; Y < Alto + 16; Y += 16, Chunk_Z++, Bloques_Alto += 16)
                            {
                                Bloques_Ancho = 0;
                                for (int X = -16, Chunk_X = -1; X < Ancho + 16; X += 16, Chunk_X++, Bloques_Ancho += 16)
                                {
                                    ChunkRef Chunk = Chunks.CreateChunk(Chunk_X, Chunk_Z);
                                    Chunk.IsLightPopulated = true; // For 1.13+ conversion support.
                                    Chunk.IsTerrainPopulated = true;
                                    Chunk.Blocks.AutoLight = false;
                                    if (Chunk_X > -1 && Chunk_Z > -1)
                                    {
                                        for (int Índice_Chunk_Z = 0; Índice_Chunk_Z < 16; Índice_Chunk_Z++)
                                        {
                                            for (int Índice_Chunk_X = 0; Índice_Chunk_X < 16; Índice_Chunk_X++)
                                            {
                                                if (X + Índice_Chunk_X < Ancho && Y + Índice_Chunk_Z < Alto)
                                                {
                                                    Índice = ((Ancho * (Y + Índice_Chunk_Z)) + (X + Índice_Chunk_X)) * 4;
                                                    Chunk.Blocks.SetID(Índice_Chunk_X, 0, Índice_Chunk_Z, (int)BlockType.BEDROCK);
                                                    Color Color_ARGB = Color.FromArgb(255, Matriz_Bytes[Índice + 2], Matriz_Bytes[Índice + 1], Matriz_Bytes[Índice]);
                                                    int Código_Hash = Color_ARGB.GetHashCode();
                                                    if (Diccionario_Códigos_Hash.ContainsKey(Código_Hash))
                                                    {
                                                        byte Data;
                                                        byte ID = Minecraft.Buscar_ID_Data_Minecraft_1_12_2(Diccionario_Códigos_Hash[Código_Hash], out Data);
                                                        string Nombre = Minecraft.Diccionario_Bloques_Índices_Nombres[Diccionario_Códigos_Hash[Código_Hash]];
                                                        if (Nombre.Contains("_log")) Data |= (byte)12;
                                                        if (Nombre.Contains("dispenser") || Nombre.Contains("dropper") || Nombre.Contains("observer") || Nombre.Contains("piston")) Data |= (byte)1;
                                                        Chunk.Blocks.SetID(Índice_Chunk_X, 1, Índice_Chunk_Z, (int)ID);
                                                        Chunk.Blocks.SetData(Índice_Chunk_X, 1, Índice_Chunk_Z, (int)Data);
                                                    }
                                                    else // If a block finding is missing assume it's black
                                                    {
                                                        Chunk.Blocks.SetID(Índice_Chunk_X, 1, Índice_Chunk_Z, (int)251); // Concrete
                                                        Chunk.Blocks.SetData(Índice_Chunk_X, 1, Índice_Chunk_Z, (int)15); // Black
                                                    }
                                                }
                                                //else if (Construir_Paredes_Cristal) Chunk.Blocks.SetID(Índice_Chunk_X, 0, Índice_Chunk_Z, (int)BlockType.BEDROCK);
                                            }
                                        }
                                    }
                                    Chunk.Blocks.RebuildHeightMap();
                                    Chunk.Blocks.RebuildBlockLight();
                                    Chunk.Blocks.RebuildSkyLight();
                                }
                            }
                            if (Construir_Paredes_Cristal)
                            {
                                Bloques_Ancho -= 16;
                                Bloques_Alto -= 16;
                                for (int Índice_X = -16; Índice_X < Bloques_Ancho - 1; Índice_X++) // North glass wall
                                {
                                    for (int Índice_Y = 0; Índice_Y < 63; Índice_Y++)
                                    {
                                        Bloques.SetID(Índice_X, Índice_Y, -16, Índice_Y != 0 ? (int)BlockType.GLASS : (int)BlockType.BEDROCK);
                                    }
                                }
                                for (int Índice_Z = -16; Índice_Z < Bloques_Alto - 1; Índice_Z++) // East glass wall
                                {
                                    for (int Índice_Y = 0; Índice_Y < 63; Índice_Y++)
                                    {
                                        Bloques.SetID(Bloques_Ancho - 1, Índice_Y, Índice_Z, Índice_Y != 0 ? (int)BlockType.GLASS : (int)BlockType.BEDROCK);
                                    }
                                }
                                for (int Índice_X = Bloques_Ancho - 1; Índice_X >= -16; Índice_X--) // South glass wall
                                {
                                    for (int Índice_Y = 0; Índice_Y < 63; Índice_Y++)
                                    {
                                        Bloques.SetID(Índice_X, Índice_Y, Bloques_Alto - 1, Índice_Y != 0 ? (int)BlockType.GLASS : (int)BlockType.BEDROCK);
                                    }
                                }
                                for (int Índice_Z = Bloques_Alto - 1; Índice_Z >= -16; Índice_Z--) // West glass wall
                                {
                                    for (int Índice_Y = 0; Índice_Y < 63; Índice_Y++)
                                    {
                                        Bloques.SetID(-16, Índice_Y, Índice_Z, Índice_Y != 0 ? (int)BlockType.GLASS : (int)BlockType.BEDROCK);
                                    }
                                }
                            }
                        }
                        else
                        {
                            int Bloques_Ancho = 0;
                            int Bloques_Alto = 48;
                            for (int Y = -16, Chunk_Z = -1, Índice = 0; Y < 32; Y += 16, Chunk_Z++)
                            {
                                Bloques_Ancho = 0;
                                for (int X = -16, Chunk_X = -1; X < Ancho + 16; X += 16, Chunk_X++, Bloques_Ancho += 16)
                                {
                                    ChunkRef Chunk = Chunks.CreateChunk(Chunk_X, Chunk_Z);
                                    Chunk.IsLightPopulated = true; // For 1.13+ conversion support.
                                    Chunk.IsTerrainPopulated = true;
                                    Chunk.Blocks.AutoLight = false;
                                    if (Chunk_X >= 0 && Chunk_Z == 0)
                                    {
                                        for (int Índice_Chunk_X = 0; Índice_Chunk_X < 16; Índice_Chunk_X++)
                                        {
                                            if (X + Índice_Chunk_X < Ancho)
                                            {
                                                if (Capa_Lecho_Roca) Chunk.Blocks.SetID(Índice_Chunk_X, 0, 0, (int)BlockType.BEDROCK);
                                                for (int Índice_Chunk_Y = !Capa_Lecho_Roca ? 0 : 1; Índice_Chunk_Y < (!Capa_Lecho_Roca ? Alto : Alto + 1); Índice_Chunk_Y++)
                                                {
                                                    Índice = ((Ancho * ((Alto - 1) - (Índice_Chunk_Y - (!Capa_Lecho_Roca ? 0 : 1)))) + (X + Índice_Chunk_X)) * 4;
                                                    //Chunk.Blocks.SetID(Índice_Chunk_X, 0, Índice_Chunk_Z, (int)BlockType.BEDROCK);
                                                    Color Color_ARGB = Color.FromArgb(255, Matriz_Bytes[Índice + 2], Matriz_Bytes[Índice + 1], Matriz_Bytes[Índice]);
                                                    int Código_Hash = Color_ARGB.GetHashCode();
                                                    if (Diccionario_Códigos_Hash.ContainsKey(Código_Hash))
                                                    {
                                                        byte Data;
                                                        byte ID = Minecraft.Buscar_ID_Data_Minecraft_1_12_2(Diccionario_Códigos_Hash[Código_Hash], out Data);
                                                        string Nombre = Minecraft.Diccionario_Bloques_Índices_Nombres[Diccionario_Códigos_Hash[Código_Hash]];
                                                        //if (Nombre.Contains("_log")) Data |= (byte)0;
                                                        if (Nombre.Contains("dispenser") || Nombre.Contains("dropper") || Nombre.Contains("observer") || Nombre.Contains("piston")) Data |= (byte)3;
                                                        Chunk.Blocks.SetID(Índice_Chunk_X, Índice_Chunk_Y, 0, (int)ID);
                                                        Chunk.Blocks.SetData(Índice_Chunk_X, Índice_Chunk_Y, 0, (int)Data);
                                                    }
                                                    else // If a block finding is missing assume it's black
                                                    {
                                                        Chunk.Blocks.SetID(Índice_Chunk_X, Índice_Chunk_Y, 0, (int)251); // Concrete
                                                        Chunk.Blocks.SetData(Índice_Chunk_X, Índice_Chunk_Y, 0, (int)15); // Black
                                                    }
                                                }
                                            }
                                            //else if (Construir_Paredes_Cristal) Chunk.Blocks.SetID(Índice_Chunk_X, 0, 0, (int)BlockType.BEDROCK);
                                        }
                                    }
                                    Chunk.Blocks.RebuildHeightMap();
                                    Chunk.Blocks.RebuildBlockLight();
                                    Chunk.Blocks.RebuildSkyLight();
                                }
                            }
                            if (Construir_Paredes_Cristal)
                            {
                                Bloques_Ancho -= 16;
                                Bloques_Alto -= 16;
                                for (int Índice_X = -16; Índice_X < Bloques_Ancho - 1; Índice_X++) // North glass wall
                                {
                                    for (int Índice_Y = 0; Índice_Y < 63; Índice_Y++)
                                    {
                                        Bloques.SetID(Índice_X, Índice_Y, -16, Índice_Y != 0 ? (int)BlockType.GLASS : (int)BlockType.BEDROCK);
                                    }
                                }
                                for (int Índice_Z = -16; Índice_Z < Bloques_Alto - 1; Índice_Z++) // East glass wall
                                {
                                    for (int Índice_Y = 0; Índice_Y < 63; Índice_Y++)
                                    {
                                        Bloques.SetID(Bloques_Ancho - 1, Índice_Y, Índice_Z, Índice_Y != 0 ? (int)BlockType.GLASS : (int)BlockType.BEDROCK);
                                    }
                                }
                                for (int Índice_X = Bloques_Ancho - 1; Índice_X >= -16; Índice_X--) // South glass wall
                                {
                                    for (int Índice_Y = 0; Índice_Y < 63; Índice_Y++)
                                    {
                                        Bloques.SetID(Índice_X, Índice_Y, Bloques_Alto - 1, Índice_Y != 0 ? (int)BlockType.GLASS : (int)BlockType.BEDROCK);
                                    }
                                }
                                for (int Índice_Z = Bloques_Alto - 1; Índice_Z >= -16; Índice_Z--) // West glass wall
                                {
                                    for (int Índice_Y = 0; Índice_Y < 63; Índice_Y++)
                                    {
                                        Bloques.SetID(-16, Índice_Y, Índice_Z, Índice_Y != 0 ? (int)BlockType.GLASS : (int)BlockType.BEDROCK);
                                    }
                                }
                            }
                        }
                        Matriz_Bytes = null;
                        Diccionario_Paleta = null;
                        Diccionario_Códigos_Hash = null;
                        Chunks.Save();
                        Mundo.Save();
                        Chunks = null;
                        Bloques = null;
                        Mundo = null;
                        SystemSounds.Asterisk.Play();
                    }
                    else MessageBox.Show(this, "The height of the image is larger than 256 (current height: " + Program.Traducir_Número(Alto) + ").\r\nPlease limit the height from the context menu and it try again.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            finally
            {
                this.Text = Texto_Título + " - [Current Block Palette: " + Program.Traducir_Número(Lista_Paleta != null ? Lista_Paleta.Count : 0) + ((Lista_Paleta != null ? Lista_Paleta.Count : 0) != 1 ? " Blocks" : " Block") + "]";
                this.Cursor = Cursors.Default;
            }
        }

        private void Menú_Contextual_Interpolación_Click(object sender, EventArgs e)
        {
            try
            {
                for (int Índice = 0; Índice < Menú_Contextual_Interpolación.DropDownItems.Count; Índice++) if (Menú_Contextual_Interpolación.DropDownItems[Índice].GetType() == typeof(ToolStripMenuItem)) ((ToolStripMenuItem)Menú_Contextual_Interpolación.DropDownItems[Índice]).Checked = false;
                ToolStripMenuItem Menú = (ToolStripMenuItem)sender;
                Menú.Checked = true;
                Interpolación = (DitherMethod)int.Parse(Menú.Name.Replace("Menú_Contextual_Interpolación_", null));
                //Registro_Guardar_Opciones();
                Abrir_Ruta_Imagen(Ruta_Original);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Abrir_Ruta_Imagen(Ruta_Original);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal void Registro_Cargar_Opciones()
        {
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Pixel Art Generator");
                try { Interpolación = (DitherMethod)Clave.GetValue("Dither_Method", (int)DitherMethod.FloydSteinberg); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Interpolación = DitherMethod.FloydSteinberg; }
                try { Limitar_Altura_Forzar_Altura = bool.Parse((string)Clave.GetValue("Limit_Height_Force_Height", bool.FalseString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Limitar_Altura_Forzar_Altura = false; }
                try { Limitar_Altura = (int)Clave.GetValue("Limit_Height", 256); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Limitar_Altura = 256; }
                try { Construir_Paredes_Cristal = bool.Parse((string)Clave.GetValue("Build_Glass_Walls", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Construir_Paredes_Cristal = true; }

                // Correct any bad value after loading:
                if ((int)Interpolación <= 0 || (int)Interpolación > (int)DitherMethod.FloydSteinberg) Interpolación = DitherMethod.FloydSteinberg;
                if (Limitar_Altura != 0 && Limitar_Altura != 16 && Limitar_Altura != 32 && Limitar_Altura != 48 && Limitar_Altura != 64 && Limitar_Altura != 80 && Limitar_Altura != 96 && Limitar_Altura != 112 && Limitar_Altura != 128 && Limitar_Altura != 144 && Limitar_Altura != 160 && Limitar_Altura != 176 && Limitar_Altura != 192 && Limitar_Altura != 208 && Limitar_Altura != 224 && Limitar_Altura != 240 && Limitar_Altura != 256 && Limitar_Altura != 127 && Limitar_Altura != 255) Limitar_Altura = 256;

                // Apply all the loaded values:
                Menú_Contextual_Interpolación.DropDownItems["Menú_Contextual_Interpolación_" + ((int)Interpolación).ToString()].PerformClick();
                Menú_Contextual_Limitar_Altura_Forzar_Altura.Checked = Limitar_Altura_Forzar_Altura;
                Menú_Contextual_Limitar_Altura.DropDownItems["Menú_Contextual_Limitar_Altura_" + Limitar_Altura.ToString()].PerformClick();
                Menú_Contextual_Construir_Paredes_Cristal.Checked = Construir_Paredes_Cristal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal void Registro_Guardar_Opciones()
        {
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Pixel Art Generator");
                string[] Matriz_Nombres = Clave.GetValueNames();
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                    {
                        Clave.DeleteValue(Matriz_Nombres[Índice]);
                    }
                }
                Matriz_Nombres = null;
                try { Clave.SetValue("Dither_Method", Interpolación, RegistryValueKind.DWord); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                try { Clave.SetValue("Limit_Height_Force_Height", Limitar_Altura_Forzar_Altura.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                try { Clave.SetValue("Limit_Height", Limitar_Altura, RegistryValueKind.DWord); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                try { Clave.SetValue("Build_Glass_Walls", Construir_Paredes_Cristal.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal void Registro_Restablecer_Opciones()
        {
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Pixel Art Generator");
                string[] Matriz_Nombres = Clave.GetValueNames();
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                    {
                        try { Clave.DeleteValue(Matriz_Nombres[Índice]); }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    Matriz_Nombres = null;
                }
                Clave.Close();
                Clave = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.Pixel_art_generator_with_world_exporter;
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

        private void Menú_Contextual_Abrir_Carpeta_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Pixel_Art);
                Program.Ejecutar_Ruta(Program.Ruta_Guardado_Imágenes_Pixel_Art, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }
    }
}
