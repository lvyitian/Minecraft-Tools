using Microsoft.Win32;
using Substrate;
using Substrate.Core;
using Substrate.Nbt;
using Substrate.TileEntities;
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
    public partial class Ventana_Generador_Estructuras_Personalizadas : Form
    {
        public Ventana_Generador_Estructuras_Personalizadas()
        {
            InitializeComponent();
        }

        internal enum Estructuras : int
        {
            Pyramid = 0,
            Sphere,
            Spiral_stairs,
            Labyrinth,
            Trapped_labyrinth,
            Infdev_pyramid,
            Cilinder,
            Floating_pyramid,
            Desert_pyramid,
            Jungle_pyramid,
            Ocean_monument,
            Player_statue,
            Slime_chunk_farm,
            Text,
            Painted_structure,
            Floating_city,
            Total // Don't use
        }

        internal readonly string Texto_Título = "Custom Structures Generator by Jupisoft for " + Program.Texto_Usuario;
        internal string Texto_Título_Actual = null;

        internal static bool Variable_Estructura_3D = true;
        internal static Estructuras Variable_Estructura = 0;
        internal static bool Variable_Rellenar = true;
        internal static decimal Variable_Lados = 6;
        internal static CheckState Variable_Forzar_Simetría = CheckState.Checked;
        internal static int Variable_Diámetro = 64;
        internal static decimal Variable_Rotación = 0;
        internal static string Variable_Bloque = "minecraft:sea_lantern";
        internal static string Variable_Bloque_Interior = "minecraft:air";
        internal static bool Variable_Autozoom = true;
        internal static bool Construir_Paredes_Cristal = true;
        internal static bool Variable_Gamerule_DoFireTick = true;

        internal Stopwatch Cronómetro_Memoria = new Stopwatch(); // Turn the text red when over 4 GB
        internal Bitmap Imagen_Blanco_Negro = null;
        internal Bitmap Imagen_Estructura_Pintada = null;
        internal bool Ocupado = false;
        internal bool Variable_Siempre_Visible = false;

        // Añadir texto como una estructura gigante con palabras
        // Girar las estructuras pares para que estén sobre su base: Hecho
        // Auto-zoom con checkbox mientras (ancho o alto de polígono * 2) sea menor que el área cliente

        private void Ventana_Generador_Estructuras_Masivas_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.WindowState = FormWindowState.Maximized;
                List<string> Lista_Nombres = new List<string>();
                foreach (KeyValuePair<string, short> Entrada in Minecraft.Diccionario_Bloques_Nombres_Índices)
                {
                    if (!Minecraft.Diccionario_Bloques_Minecraft_1_13.ContainsKey(Entrada.Value))
                    {
                        Lista_Nombres.Add(Entrada.Key.Substring(10, 1).ToUpperInvariant() + Entrada.Key.Substring(11).Replace('_', ' '));
                    }
                }
                Lista_Nombres.Sort(new Minecraft.Comparador_String());
                ComboBox_Bloque.Items.AddRange(Lista_Nombres.ToArray());
                ComboBox_Bloque_Interior.Items.AddRange(Lista_Nombres.ToArray());
                Lista_Nombres = null;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
                Generar_Estructura_Masiva();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal void Dibujar_Iteraciones(Graphics Pintar, double[] Matriz_Ángulos_Seno, double[] Matriz_Ángulos_Coseno, double Radio, double Centro, double X, double Y, int Iteración, int Iteraciones)
        {
            try
            {
                Pintar.ResetTransform();
                Pintar.TranslateTransform((float)X, (float)Y);
                Pintar.DrawEllipse(Pens.Black, (float)(-Radio), (float)(-Radio), (float)(Radio * 2d), (float)(Radio * 2d));
                //Pintar.DrawEllipse(/*Pens.Black*/new Pen(Program.Obtener_Color_Puro_1530(Program.Rand.Next(0, 1530))), (float)(-Radio), (float)(-Radio), (float)(Radio * 2d), (float)(Radio * 2d));
                Pintar.ResetTransform();
                Pintar.TranslateTransform((float)Centro, (float)Centro);
                //Pintar.DrawLine(/*Pens.Red*/new Pen(Program.Obtener_Color_Puro_1530(Program.Rand.Next(0, 1530))), (float)(X - Centro), (float)(Y - Centro), (float)0, (float)0);
                if (Iteración < Iteraciones)
                {
                    for (int Índice_Ángulo = 0; Índice_Ángulo < Matriz_Ángulos_Seno.Length; Índice_Ángulo++)
                    {
                        Dibujar_Iteraciones(Pintar, Matriz_Ángulos_Seno, Matriz_Ángulos_Coseno, Radio, Centro, X - Matriz_Ángulos_Seno[Índice_Ángulo], Y - Matriz_Ángulos_Coseno[Índice_Ángulo], Iteración + 1, Iteraciones);
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Generador_Estructuras_Masivas_Shown(object sender, EventArgs e)
        {
            try
            {
                Temporizador_Principal.Start();
                this.Activate();

                /*// Draw the "Flower of life" and other shapes [2019_06_03_04_56_59_864]...
                double Dimensiones = 2048d;
                Bitmap Imagen = new Bitmap((int)Dimensiones, (int)Dimensiones, PixelFormat.Format32bppArgb);
                Graphics Pintar = Graphics.FromImage(Imagen);
                Pintar.CompositingMode = CompositingMode.SourceOver;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.None; // HighQuality;
                Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                Pintar.Clear(Color.White);

                double Centro = Dimensiones / 2d;
                double Radio = 128d;
                int Ángulos = 6;
                double[] Matriz_Ángulos_Seno = new double[Ángulos];
                double[] Matriz_Ángulos_Coseno = new double[Ángulos];
                for (int Índice_Ángulo = 0; Índice_Ángulo < Ángulos; Índice_Ángulo++)
                {
                    Matriz_Ángulos_Seno[Índice_Ángulo] = Radio * Math.Sin((((360d * (double)Índice_Ángulo) / (double)Ángulos) * Math.PI) / 180d);
                    Matriz_Ángulos_Coseno[Índice_Ángulo] = Radio * Math.Cos((((360d * (double)Índice_Ángulo) / (double)Ángulos) * Math.PI) / 180d);
                }
                Dictionary<PointF, object> Diccionario_Centros = new Dictionary<PointF, object>();
                PointF Posición = new PointF((float)(Dimensiones / 2d), (float)(Dimensiones / 2d));
                int Iteraciones = 5;
                Dibujar_Iteraciones(Pintar, Matriz_Ángulos_Seno, Matriz_Ángulos_Coseno, Radio, Centro, Centro, Centro, 0, Iteraciones);
                Pintar.Dispose();
                Pintar = null;
                Program.Guardar_Imagen_Temporal(Imagen);
                Imagen.Dispose();
                Imagen = null;*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Generador_Estructuras_Masivas_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
                Cronómetro_Memoria.Reset();
                Cronómetro_Memoria = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Generador_Estructuras_Masivas_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Generador_Estructuras_Masivas_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Generador_Estructuras_Masivas_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
                {
                    string[] Matriz_Rutas = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                    if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                    {
                        foreach (string Ruta in Matriz_Rutas)
                        {
                            if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                            {
                                FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                                Image Imagen_Original = null;
                                try { Imagen_Original = Image.FromStream(Lector, false, false); }
                                catch { Imagen_Original = null; }
                                if (Imagen_Original != null)
                                {
                                    int Ancho = Imagen_Original.Width;
                                    int Alto = Imagen_Original.Height;
                                    Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                                    Graphics Pintar = Graphics.FromImage(Imagen);
                                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                                    Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                    Pintar.Dispose();
                                    Pintar = null;
                                    Imagen_Estructura_Pintada = Imagen;
                                    Lector.Close();
                                    Lector.Dispose();
                                    Lector = null;
                                    return;
                                }
                                Lector.Close();
                                Lector.Dispose();
                                Lector = null;
                            }
                        }
                        Matriz_Rutas = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Ventana_Generador_Estructuras_Masivas_KeyDown(object sender, KeyEventArgs e)
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

        private void Ventana_Generador_Estructuras_Masivas_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                Generar_Estructura_Masiva();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void CheckBox_Estructura_3D_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Estructura_3D = CheckBox_Estructura_3D.Checked;
                Registro_Guardar_Opciones();
                Generar_Estructura_Masiva();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void ComboBox_Estructura_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Estructura.SelectedIndex > -1)
                {
                    Variable_Estructura = (Estructuras)ComboBox_Estructura.SelectedIndex;
                    Registro_Guardar_Opciones();
                    Generar_Estructura_Masiva();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void CheckBox_Estructura_Rellenar_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Rellenar = CheckBox_Estructura_Rellenar.Checked;
                Registro_Guardar_Opciones();
                Generar_Estructura_Masiva();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_Lados_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Numérico_Lados.Refresh();
                Variable_Lados = Numérico_Lados.Value;
                Registro_Guardar_Opciones();
                Generar_Estructura_Masiva();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_Diámetro_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Numérico_Diámetro.Refresh();
                Variable_Diámetro = (int)Numérico_Diámetro.Value;
                Registro_Guardar_Opciones();
                Generar_Estructura_Masiva();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_Rotación_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Numérico_Rotación.Refresh();
                Variable_Rotación = Numérico_Rotación.Value;
                Registro_Guardar_Opciones();
                Generar_Estructura_Masiva();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void ComboBox_Bloque_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ComboBox_Bloque.Text))
                {
                    Variable_Bloque = "minecraft:" + ComboBox_Bloque.Text.ToLowerInvariant().Replace(' ', '_');
                    Registro_Guardar_Opciones();
                    Generar_Estructura_Masiva();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void ComboBox_Bloque_Interior_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ComboBox_Bloque_Interior.Text))
                {
                    Variable_Bloque_Interior = "minecraft:" + ComboBox_Bloque_Interior.Text.ToLowerInvariant().Replace(' ', '_');
                    Registro_Guardar_Opciones();
                    Generar_Estructura_Masiva();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void CheckBox_Estructura_Autozoom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Autozoom = CheckBox_Estructura_Autozoom.Checked;
                Registro_Guardar_Opciones();
                Generar_Estructura_Masiva();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal List<int> Obtener_Lista_Diámetros_Esfera(int Diámetro)
        {
            try
            {
                Bitmap Imagen = Obtener_Imagen_Estructura_Masiva(Variable_Rellenar, 1, Diámetro, false, Diámetro);
                if (Imagen != null)
                {
                    int Ancho = Imagen.Width;
                    int Alto = Imagen.Height;
                    BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                    int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                    int Bytes_Diferencia = Ancho_Stride - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                    byte[] Matriz_Bytes = new byte[Ancho_Stride * Alto];
                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                    List<int> Lista_Diámetros_Esfera = new List<int>();
                    for (int Índice_Z = 0, Índice = 0; Índice_Z < Alto; Índice_Z++, Índice += Bytes_Diferencia)
                    {
                        int Diámetro_Mínimo = int.MaxValue;
                        int Diámetro_Máximo = int.MinValue;
                        for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += 4)
                        {
                            if (Matriz_Bytes[Índice + 3] > 0) // Not fully transparent
                            {
                                if (Índice_X < Diámetro_Mínimo) Diámetro_Mínimo = Índice_X;
                                if (Índice_X > Diámetro_Máximo) Diámetro_Máximo = Índice_X;
                            }
                        }
                        Lista_Diámetros_Esfera.Insert(0, (Diámetro_Máximo - Diámetro_Mínimo) + 1);
                    }
                    Imagen.UnlockBits(Bitmap_Data);
                    Bitmap_Data = null;
                    Imagen.Dispose();
                    Imagen = null;
                    return Lista_Diámetros_Esfera;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal Bitmap Obtener_Imagen_Estructura_Masiva(bool Rellenar, decimal Lados, int Diámetro, bool Regenerar_Alfa, int Ancho_Alto)
        {
            try
            {
                int Diámetro_32 = Diámetro + 32;
                int Índice_Y = Math.Min(Diámetro, 127); // For Infdev_pyramid
                if (Variable_Estructura == Estructuras.Labyrinth || Variable_Estructura == Estructuras.Trapped_labyrinth)
                {
                    //if ((Diámetro - 1) % 6 != 0) Diámetro -= (Diámetro - 1) % 6;
                    //if (Diámetro < 7) Diámetro = 7;
                    if ((Diámetro - 1) % 2 != 0) Diámetro -= (Diámetro - 1) % 2;
                    if (Diámetro < 5) Diámetro = 5;
                    Ancho_Alto = 0;
                    Diámetro_32 = Diámetro;
                    //if ((Diámetro - 1) % 6 != 0) MessageBox.Show("Labyrinth size error..."); // OK
                }
                else if (Variable_Estructura == Estructuras.Infdev_pyramid)
                {
                    Diámetro = 253 - ((Índice_Y - 1) * 2); //(125 - (125 - Índice_Y)) * 2; // 253
                    Ancho_Alto = Diámetro;
                    Diámetro_32 = 253 + 32;
                }
                Bitmap Imagen = new Bitmap(Diámetro_32, Diámetro_32, PixelFormat.Format32bppArgb);
                Graphics Pintar = Graphics.FromImage(Imagen);
                Pintar.CompositingMode = CompositingMode.SourceCopy;
                //bool Alta_Calidad = Lados % 2 != 0;
                //Alta_Calidad = true;
                //Alta_Calidad = false;
                //Alta_Calidad = !Alta_Calidad;
                /*Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Pintar.PixelOffsetMode = PixelOffsetMode.Half;
                Pintar.SmoothingMode = SmoothingMode.AntiAlias;
                Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;*/
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Pintar.PixelOffsetMode = PixelOffsetMode.Half;
                Pintar.SmoothingMode = SmoothingMode.None;
                Pintar.TextRenderingHint = TextRenderingHint.AntiAlias; // Avoids color artifacts (at least on Windows 8.1 x64)
                /*if (Alta_Calidad)
                {
                    //Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    //Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.HighQuality;
                    //Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                }
                else
                {
                    //Pintar.CompositingQuality = CompositingQuality.HighSpeed;
                    //Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                    //Pintar.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                    Pintar.SmoothingMode = SmoothingMode.None;
                    //Pintar.TextRenderingHint = TextRenderingHint.SystemDefault;
                }*/
                //Pintar.Clear(Color.White);
                Color Color_ARGB = Minecraft.Diccionario_Bloques_Índices_Colores[Minecraft.Diccionario_Bloques_Nombres_Índices[Variable_Bloque]];
                Color Color_ARGB_Interior = Minecraft.Diccionario_Bloques_Índices_Colores[Minecraft.Diccionario_Bloques_Nombres_Índices[Variable_Bloque_Interior]];
                int Tamaño_Lápiz = 1;
                decimal Tamaño_Lápiz_Mitad = (decimal)Tamaño_Lápiz - 0m; //(int)Math.Round((double)Tamaño_Lápiz / 2d, MidpointRounding.AwayFromZero);
                if (Rellenar) Tamaño_Lápiz_Mitad = 0m;
                //Pen Lápiz = new Pen(Color_ARGB, Tamaño_Lápiz);
                //SolidBrush Pincel = new SolidBrush(Color.Gray);
                //Pen Lápiz_Interior = new Pen(Color_ARGB, Tamaño_Lápiz);
                //SolidBrush Pincel_Interior = new SolidBrush(string.Compare(Variable_Bloque_Interior, "minecraft:air", true) != 0 ? Minecraft.Diccionario_Bloques_Índices_Colores[Minecraft.Diccionario_Bloques_Nombres_Índices[Variable_Bloque_Interior]] : Color.Transparent);
                if (Variable_Estructura != Estructuras.Labyrinth && Variable_Estructura != Estructuras.Trapped_labyrinth && Variable_Estructura != Estructuras.Infdev_pyramid)
                {
                    Pintar.TranslateTransform((float)((decimal)Diámetro_32 / 2m), (float)((decimal)Diámetro_32 / 2m));
                    if (Lados % 2 == 0 || (Variable_Rotación > 0m && Variable_Rotación < 360m)) Pintar.RotateTransform((float)(Variable_Rotación + (Lados % 2 != 0 ? 0m : (360m / (Lados * 2m)))));
                }
                if (Variable_Estructura == Estructuras.Pyramid || Variable_Estructura == Estructuras.Sphere)
                {
                    if (Lados >= 3)
                    {
                        List<PointF> Lista_Posiciones = new List<PointF>();
                        List<PointF> Lista_Posiciones_Interior = new List<PointF>();
                        decimal Ángulo = 360m / Lados;
                        for (int Índice_Lado = 0; Índice_Lado < (int)Lados; Índice_Lado++)
                        {
                            decimal Seno_X = ((Diámetro - Tamaño_Lápiz_Mitad) / 2m) * (decimal)Math.Sin(((double)(0 + (Ángulo * (decimal)Índice_Lado)) * Math.PI) / 180d);
                            decimal Coseno_Y = ((Diámetro - Tamaño_Lápiz_Mitad) / 2m) * (decimal)Math.Cos((((double)(0 + (Ángulo * (decimal)Índice_Lado)) + 180d) * Math.PI) / 180d);
                            Lista_Posiciones.Add(new PointF((float)(Seno_X), (float)(Coseno_Y)));

                            /*if (Seno_X < 0) Seno_X += 0.5m;
                            else if (Seno_X > 0) Seno_X -= 0.5m;
                            if (Coseno_Y < 0) Coseno_Y += 0.5m;
                            else if (Coseno_Y > 0) Coseno_Y -= 0.5m;
                            /*if (!Rellenar)
                            {
                                if (Seno_X < 0) Seno_X += Tamaño_Lápiz_Mitad;
                                else if (Seno_X > 0) Seno_X -= Tamaño_Lápiz_Mitad;
                                if (Coseno_Y < 0) Coseno_Y += Tamaño_Lápiz_Mitad;
                                else if (Coseno_Y > 0) Coseno_Y -= Tamaño_Lápiz_Mitad;
                            }*/
                            Lista_Posiciones_Interior.Add(new PointF((float)(Seno_X), (float)(Coseno_Y)));
                        }
                        if (!Rellenar)
                        {
                            Pintar.FillPolygon(Brushes.Black, Lista_Posiciones.ToArray());
                            Pintar.DrawPolygon(Pens.White, Lista_Posiciones.ToArray());
                            /*Pintar.FillPolygon(Pincel, Lista_Posiciones.ToArray());
                            Pintar.CompositingMode = CompositingMode.SourceCopy;
                            Pintar.FillPolygon(Brushes.Transparent, Lista_Posiciones_Interior.ToArray());
                            Pintar.CompositingMode = CompositingMode.SourceOver;*/
                        }
                        else Pintar.FillPolygon(Brushes.White, Lista_Posiciones.ToArray());
                        Lista_Posiciones = null;
                    }
                    else if (Lados == 1) // Círculo
                    {
                        Pintar.FillEllipse(!Rellenar ? Brushes.Black : Brushes.White, (float)(-(Diámetro - Tamaño_Lápiz_Mitad) / 2m), (float)(-(Diámetro - Tamaño_Lápiz_Mitad) / 2m), (float)((Diámetro - (Tamaño_Lápiz_Mitad * 1))), (float)((Diámetro - (Tamaño_Lápiz_Mitad * 1))));
                        //Pintar.FillEllipse(Brushes.Red, (float)(-Diámetro / 2m), (float)(-Diámetro / 2m), (float)(Diámetro), (float)(Diámetro));
                        //Pintar.DrawEllipse(Lápiz, (float)(-Diámetro / 2m), (float)(-Diámetro / 2m), (float)(Diámetro), (float)(Diámetro));
                        if (!Rellenar) Pintar.DrawEllipse(Pens.White, (float)(-(Diámetro - Tamaño_Lápiz_Mitad) / 2m), (float)(-(Diámetro - Tamaño_Lápiz_Mitad) / 2m), (float)((Diámetro - (Tamaño_Lápiz_Mitad * 1))), (float)((Diámetro - (Tamaño_Lápiz_Mitad * 1))));
                    }
                }
                /*else if (Variable_Estructura == Estructuras.Spiral_stairs)
                {
                    Imagen = Ventana_Principal.Generar_Imagen_Espiral(64, 64, 100, true, true);
                    Picture.Image = Imagen;
                }*/
                /*else if (Estructura == Estructuras.Sphere)
                {

                }*/
                else if (Variable_Estructura == Estructuras.Labyrinth || Variable_Estructura == Estructuras.Trapped_labyrinth)
                {
                    Pintar.FillRectangle(Brushes.White, 0, 0, Diámetro, Diámetro); // Bedrock
                    //Pintar.FillRectangle(Brushes.Black, 1, 1, Diámetro - 2, Diámetro - 2); // Air
                    int Diámetro_1 = Diámetro - 1;
                    int Diámetro_2 = Diámetro - 2;
                    //int Diámetro_3 = Diámetro - 3;
                    int Diámetro_4 = Diámetro - 4;
                    Pintar.FillRectangle(Brushes.Black, 0, 0, 1, 1); // Air for the entrance
                    Pintar.FillRectangle(Brushes.Black, 1, 0, 1, 1); // Air
                    Pintar.FillRectangle(Brushes.Black, 0, 1, 1, 1); // Air
                    Pintar.FillRectangle(Brushes.Black, Diámetro_1, Diámetro_1, 1, 1); // Air for the exit
                    Pintar.FillRectangle(Brushes.Black, Diámetro_2, Diámetro_1, 1, 1); // Air
                    Pintar.FillRectangle(Brushes.Black, Diámetro_1, Diámetro_2, 1, 1); // Air
                    for (int Índice_Z = 1; Índice_Z < Diámetro_1; Índice_Z += 2) // Horizontal walls
                    {
                        Pintar.FillRectangle(Brushes.Black, 1, Índice_Z, Diámetro_2, 1); // Bedrock
                    }
                    for (int Índice_X = 1; Índice_X < Diámetro_1; Índice_X += 2) // Vertical walls
                    {
                        Pintar.FillRectangle(Brushes.Black, Índice_X, 1, 1, Diámetro_2); // Bedrock
                    }
                    /*for (int Índice_Z = 2; Índice_Z < Diámetro_2; Índice_Z += 2) // Vertical spots
                    {
                        for (int Índice_X = 2; Índice_X < Diámetro_2; Índice_X += 2) // Horizontal spots
                        {
                            Pintar.FillRectangle(Brushes.White, Índice_X, Índice_Z, 1, 1); // Bedrock
                        }
                    }*/
                    Point Posición_Entrada = new Point(1, 1);
                    Point Posición_Salida = new Point(Diámetro_2, Diámetro_2);
                    List<Point> Lista_Posiciones_Laberinto = new List<Point>();
                    for (int Índice_Z = 1; Índice_Z < Diámetro_1; Índice_Z += 2) // Vertical spots
                    {
                        for (int Índice_X = 1; Índice_X < Diámetro_1; Índice_X += 2) // Horizontal spots
                        {
                            Point Posición = new Point(Índice_X, Índice_Z);
                            Lista_Posiciones_Laberinto.Add(Posición);
                        }
                    }
                    Point Posición_Actual = Posición_Entrada;
                    List<Point> Lista_Posiciones_Salida_Laberinto = new List<Point>(new Point[] { Posición_Actual });
                    //List<int> Lista_Direcciones = new List<int>(new int[4] { 0, 1, 2, 3 });
                    //List<int> Lista_Direcciones = new List<int>(new int[] { 0, 1, 1, 2, 3, 3 });
                    //List<int> Lista_Direcciones = new List<int>(new int[10] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 3 });
                    //List<int> Lista_Direcciones = new List<int>(new int[] { 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3 });
                    List<int> Lista_Direcciones = new List<int>();
                    int Cantidad_Direcciones = Math.Min((int)Lados, 100);
                    for (int Índice = 0; Índice < Cantidad_Direcciones; Índice++) Lista_Direcciones.Add(0);
                    for (int Índice = 0; Índice < Cantidad_Direcciones + 1; Índice++) Lista_Direcciones.Add(1);
                    for (int Índice = 0; Índice < Cantidad_Direcciones; Índice++) Lista_Direcciones.Add(2);
                    for (int Índice = 0; Índice < Cantidad_Direcciones + 1; Índice++) Lista_Direcciones.Add(3);
                    while (Posición_Actual != Posición_Salida)
                    {
                        List<int> Lista_Direcciones_Temporal = Lista_Direcciones.GetRange(0, Lista_Direcciones.Count);
                        for (;;) // Do an iteration until a valid random direction is found
                        {
                            int Índice = Program.Rand.Next(0, Lista_Direcciones_Temporal.Count);
                            int Dirección = Lista_Direcciones_Temporal[Índice];
                            Lista_Direcciones_Temporal.RemoveAt(Índice); // Don't repeat random invalid directions
                            Point Posición_Temporal = Posición_Actual;
                            if (Dirección == 0) Posición_Temporal.Y -= 2; // North
                            else if (Dirección == 1) Posición_Temporal.Y += 2; // South
                            else if (Dirección == 2) Posición_Temporal.X -= 2; // West
                            else if (Dirección == 3) Posición_Temporal.X += 2; // East
                            if (Posición_Temporal.X >= 1 && Posición_Temporal.Y >= 1 && Posición_Temporal.X < Diámetro_1 && Posición_Temporal.Y < Diámetro_1) // Valid direction
                            {
                                Posición_Actual = Posición_Temporal;
                                if (!Lista_Posiciones_Salida_Laberinto.Contains(Posición_Actual))
                                {
                                    Lista_Posiciones_Salida_Laberinto.Add(Posición_Actual);
                                    break; // A new valid position has been found
                                }
                                if (Lista_Direcciones_Temporal.Count <= 0)
                                {
                                    break; // If it can't go to a new position, go to a previous one
                                }
                            }
                        }
                    }
                    /*foreach (Point Posición in Lista_Posiciones_Salida_Laberinto)
                    {
                        Pintar.FillRectangle(Brushes.Black, Posición.X, Posición.Y, 1, 1); // Open the wall here to be able to reach the exit
                    }*/
                    for (int Índice_Z = 1; Índice_Z < Diámetro_1; Índice_Z += 2) // Vertical spots
                    {
                        for (int Índice_X = 1; Índice_X < Diámetro_1; Índice_X += 2) // Horizontal spots
                        {
                            Point Posición = new Point(Índice_X, Índice_Z);
                            if (!Lista_Posiciones_Salida_Laberinto.Contains(Posición)) Pintar.FillRectangle(Brushes.White, Posición.X, Posición.Y, 1, 1); // Open the wall here to be able to reach the exit
                        }
                    }
                    int Porcentaje_Paredes_Vacías = Math.Min((int)Variable_Rotación, 100); // Change this to have more or less opened walls
                    foreach (Point Posición in Lista_Posiciones_Laberinto)
                    {
                        if (Program.Rand.Next(0, 100) < Porcentaje_Paredes_Vacías) Pintar.FillRectangle(Brushes.Black, Posición.X, Posición.Y, 1, 1); // Open more walls to confuse the players
                    }
                    //MessageBox.Show("Exit found on " + Program.Traducir_Número(Lista_Posiciones_Salida_Laberinto.Count) + " iterations...\r\nPositions: " + Lista_Posiciones_Laberinto.Count.ToString());
                }
                else if (Variable_Estructura == Estructuras.Infdev_pyramid)
                {
                    // Hasta dónde acaba el mar hay 65 bloques hasta el vacío y 63 de altura de la pirámide, por lo que el mar está a Y = 64, comprobado que la pirámide se extiende al menos 17 bloques por debajo del mar y la cima está a Y = 127, el límite del mundo antiguo (128 bloques). No hay lecho de roca, y debajo del vacío hay la luna parada, al caer al vacío el jugador se pega fuego como si hubiera lava invisible y no muere por caída
                    // El mar acaba a Y = 62 y cima + 63 = Y 125
                    //Diámetro = (127 - (127 - Índice_Y)) * 2;
                    Pintar.FillRectangle(Brushes.Black, (253 - Diámetro) / 2, (253 - Diámetro) / 2, Diámetro, Diámetro);
                    if (!Rellenar && Índice_Y > 1) Pintar.FillRectangle(Brushes.White, ((253 - Diámetro) / 2) + 1, ((253 - Diámetro) / 2) + 1, Diámetro - 2, Diámetro - 2);
                }
                else return null;
                Pintar.Dispose();
                Pintar = null;
                Rectangle Rectángulo = Program.Buscar_Zona_Recorte_Imagen(Imagen, Color.Transparent);
                if (Rectángulo.X > -1 && Rectángulo.Y > -1 && Rectángulo.X < int.MaxValue && Rectángulo.Y < int.MaxValue && Rectángulo.Width > 0 && Rectángulo.Height > 0)
                {
                    Imagen = Imagen.Clone(Rectángulo, PixelFormat.Format32bppArgb);
                    Imagen_Blanco_Negro = Imagen.Clone() as Bitmap;
                    int Ancho = Rectángulo.Width;
                    int Alto = Rectángulo.Height;
                    if (Regenerar_Alfa)
                    {
                        BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                        int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                        int Bytes_Diferencia = Ancho_Stride - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                        byte[] Matriz_Bytes = new byte[Ancho_Stride * Alto];
                        Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                        for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                        {
                            for (int X = 0; X < Ancho; X++, Índice += 4)
                            {
                                if (Matriz_Bytes[Índice + 3] > 0) // Not fully transparent
                                {
                                    if (((Matriz_Bytes[Índice + 2] + Matriz_Bytes[Índice + 1] + Matriz_Bytes[Índice]) / 3) < 128)
                                    {
                                        Matriz_Bytes[Índice + 3] = 255;
                                        Matriz_Bytes[Índice + 2] = Color_ARGB_Interior.R;
                                        Matriz_Bytes[Índice + 1] = Color_ARGB_Interior.G;
                                        Matriz_Bytes[Índice] = Color_ARGB_Interior.B;
                                    }
                                    else
                                    {
                                        Matriz_Bytes[Índice + 3] = 255;
                                        Matriz_Bytes[Índice + 2] = Color_ARGB.R;
                                        Matriz_Bytes[Índice + 1] = Color_ARGB.G;
                                        Matriz_Bytes[Índice] = Color_ARGB.B;
                                    }
                                }
                            }
                        }
                        Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                        Imagen.UnlockBits(Bitmap_Data);
                        Bitmap_Data = null;
                    }
                    if (Ancho_Alto > 0)
                    {
                        Bitmap Imagen_Temporal = new Bitmap(Ancho_Alto, Ancho_Alto, PixelFormat.Format32bppArgb);
                        Graphics Pintar_Temporal = Graphics.FromImage(Imagen_Temporal);
                        Pintar_Temporal.CompositingMode = CompositingMode.SourceCopy;
                        Pintar_Temporal.DrawImage(Imagen, new Rectangle((Ancho_Alto / 2) - (Ancho / 2), (Ancho_Alto / 2) - (Alto / 2), Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                        Pintar_Temporal.Dispose();
                        Pintar_Temporal = null;
                        Imagen = Imagen_Temporal;
                        Ancho = Ancho_Alto;
                        Alto = Ancho_Alto;
                    }
                    if ((Variable_Forzar_Simetría == CheckState.Checked || Variable_Forzar_Simetría == CheckState.Indeterminate) && (Variable_Estructura != Estructuras.Labyrinth && Variable_Estructura != Estructuras.Trapped_labyrinth && Variable_Estructura != Estructuras.Infdev_pyramid) && Variable_Rotación == 0m || Variable_Rotación == 360m)
                    {
                        Bitmap Imagen_Simetría = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                        Graphics Pintar_Simetría = Graphics.FromImage(Imagen_Simetría);
                        Pintar_Simetría.CompositingMode = CompositingMode.SourceCopy;
                        if (Variable_Forzar_Simetría == CheckState.Checked) // Horizontal symmetry
                        {
                            int Ancho_Simetría = (Ancho / 2) + (Ancho % 2 != 0 ? 1 : 0);
                            Imagen = Imagen.Clone(new Rectangle(0/*Ancho - Ancho_Simetría*/, 0, Ancho_Simetría, Alto), PixelFormat.Format32bppArgb); // Use only the right half
                            Imagen.RotateFlip(RotateFlipType.RotateNoneFlipX); // Change to the left half

                            Pintar_Simetría.DrawImage(Imagen, new Rectangle(Ancho - Ancho_Simetría, 0, Ancho_Simetría, Alto), new Rectangle(0, 0, Ancho_Simetría, Alto), GraphicsUnit.Pixel); // Right half
                            Imagen.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            Pintar_Simetría.DrawImage(Imagen, new Rectangle(0, 0, Ancho_Simetría, Alto), new Rectangle(0, 0, Ancho_Simetría, Alto), GraphicsUnit.Pixel); // Left half
                        }
                        else // 4 corners symmetry
                        {
                            int Ancho_Simetría = (Ancho / 2) + (Ancho % 2 != 0 ? 1 : 0);
                            int Alto_Simetría = (Alto / 2) + (Alto % 2 != 0 ? 1 : 0);
                            Imagen = Imagen.Clone(new Rectangle(0/*Ancho - Ancho_Simetría*/, 0, Ancho_Simetría, Alto_Simetría), PixelFormat.Format32bppArgb); // Use only the top right corner
                            Imagen.RotateFlip(RotateFlipType.RotateNoneFlipX); // Change to the top left corner

                            Pintar_Simetría.DrawImage(Imagen, new Rectangle(Ancho - Ancho_Simetría, 0, Ancho_Simetría, Alto_Simetría), new Rectangle(0, 0, Ancho_Simetría, Alto_Simetría), GraphicsUnit.Pixel); // top right corner

                            Imagen.RotateFlip(RotateFlipType.RotateNoneFlipY);
                            Pintar_Simetría.DrawImage(Imagen, new Rectangle(Ancho - Ancho_Simetría, Alto - Alto_Simetría, Ancho_Simetría, Alto_Simetría), new Rectangle(0, 0, Ancho_Simetría, Alto_Simetría), GraphicsUnit.Pixel); // bottom right corner

                            Imagen.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            Pintar_Simetría.DrawImage(Imagen, new Rectangle(0, Alto - Alto_Simetría, Ancho_Simetría, Alto_Simetría), new Rectangle(0, 0, Ancho_Simetría, Alto_Simetría), GraphicsUnit.Pixel); // bottom left corner

                            Imagen.RotateFlip(RotateFlipType.RotateNoneFlipY);
                            Pintar_Simetría.DrawImage(Imagen, new Rectangle(0, 0, Ancho_Simetría, Alto_Simetría), new Rectangle(0, 0, Ancho_Simetría, Alto_Simetría), GraphicsUnit.Pixel); // bottom left corner
                        }
                        Pintar_Simetría.Dispose();
                        Pintar_Simetría = null;
                        Imagen = Imagen_Simetría;
                    }
                    return Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal void Generar_Estructura_Masiva()
        {
            try
            {
                if (!Ocupado)
                {
                    Ocupado = true;
                    Bitmap Imagen = Obtener_Imagen_Estructura_Masiva(Variable_Rellenar, Variable_Lados, Variable_Diámetro, true, 0);
                    if (Imagen != null)
                    {
                        int Ancho = Imagen.Width;
                        int Alto = Imagen.Height;
                        int Ancho_Cliente = Picture.ClientSize.Width;
                        int Alto_Cliente = Picture.ClientSize.Height;
                        int Ancho_Alto = Math.Max(Ancho, Alto);
                        int Ancho_Alto_Cliente = Math.Min(Ancho_Cliente, Alto_Cliente);
                        int Multiplicador = Ancho_Alto_Cliente / Ancho_Alto;
                        if (!Variable_Autozoom || Multiplicador <= 0) Multiplicador = 1;
                        Texto_Título_Actual = Texto_Título + " - [Dimensions: " + Program.Traducir_Número(Ancho) + " x " + Program.Traducir_Número(Alto) + (Ancho * Alto != 1 ? " pixels" : " pixel") + ", Autozoom: " + Program.Traducir_Número(Multiplicador) + "x";
                        this.Text = Texto_Título_Actual + "]";
                        if (Variable_Autozoom)
                        {
                            if (Multiplicador > 1)
                            {
                                //MessageBox.Show(Multiplicador.ToString() + "x");
                                int Ancho_Múltiple = Ancho * Multiplicador;
                                int Alto_Múltiple = Alto * Multiplicador;
                                Bitmap Imagen_Zoom = new Bitmap(Ancho_Múltiple, Alto_Múltiple, PixelFormat.Format32bppArgb);
                                Graphics Pintar_Zoom = Graphics.FromImage(Imagen_Zoom);
                                Pintar_Zoom.CompositingMode = CompositingMode.SourceCopy;
                                Pintar_Zoom.CompositingQuality = CompositingQuality.HighQuality;
                                Pintar_Zoom.InterpolationMode = InterpolationMode.NearestNeighbor;
                                Pintar_Zoom.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                Pintar_Zoom.SmoothingMode = SmoothingMode.HighQuality;
                                Pintar_Zoom.TextRenderingHint = TextRenderingHint.AntiAlias;
                                Pintar_Zoom.DrawImage(Imagen, new Rectangle(0, 0, Ancho_Múltiple, Alto_Múltiple), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                Pintar_Zoom.Dispose();
                                Pintar_Zoom = null;
                                Imagen = Imagen_Zoom;
                            }
                        }
                        Picture.Image = Imagen;
                        Picture.Refresh();
                    }
                    Ocupado = false;
                }
            }
            catch (Exception Excepción)
            {
                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                Picture.Image = null;
                Imagen_Blanco_Negro = null;
            }
        }

        internal void Exportar_Mundo_Minecraft()
        {
            try
            {
                if (Picture.Image != null && Imagen_Blanco_Negro != null)
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.Text = Texto_Título_Actual + ", please wait up to a few minutes...]";
                    string Ruta = Program.Ruta_Guardado_Minecraft + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " Custom structures " + Variable_Estructura.ToString().ToLowerInvariant().Replace('_', ' ') + " " + Variable_Lados.ToString();
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
                    Mundo.Level.Spawn = new SpawnPoint(-1, 1, -1);
                    Mundo.Level.AllowCommands = true;
                    Mundo.Level.GameRules.CommandBlockOutput = false; // Hide the command block messages.
                    Mundo.Level.GameRules.DoMobSpawning = false;
                    Mundo.Level.GameRules.DoFireTick = Variable_Gamerule_DoFireTick;
                    Mundo.Level.GameRules.MobGriefing = false;
                    Mundo.Level.GameRules.KeepInventory = true;
                    Mundo.Level.RainTime = 55555;
                    Mundo.Level.IsRaining = false;
                    Mundo.Level.Player = new Player();
                    Mundo.Level.Player.Dimension = 0;
                    Mundo.Level.Player.Position = new Vector3();
                    Mundo.Level.Player.Position.X = -1;
                    Mundo.Level.Player.Position.Y = 1;
                    Mundo.Level.Player.Position.Z = -1;
                    Substrate.Orientation Orientación = new Substrate.Orientation();
                    Orientación.Pitch = 0d; // -90º a +90º // 25 = hacia abajo
                    Orientación.Yaw = -45d; // -180º a +180º // 45 = Sureste
                    Mundo.Level.Player.Rotation = Orientación;
                    Mundo.Level.Player.Spawn = new SpawnPoint(-1, 1, -1);
                    Mundo.Level.Player.Abilities.Flying = true;
                    Mundo.Level.RandomSeed = 4; // Massive ocean.
                    if (Variable_Estructura == Estructuras.Labyrinth || Variable_Estructura == Estructuras.Trapped_labyrinth)
                    {
                        Mundo.Level.DayTime = 18000; // Play at night to hide the exit light
                        Mundo.Level.Time = 18000; // Play at night to hide the exit light
                    }
                    int Diámetro_16 = 253 + 16;
                    int Bloques_Ancho = 0;
                    int Bloques_Alto = 0;
                    byte Data;
                    byte ID = Minecraft.Buscar_ID_Data_Minecraft_1_12_2(Minecraft.Diccionario_Bloques_Nombres_Índices[Variable_Bloque], out Data);
                    byte Data_Interior;
                    byte ID_Interior = Minecraft.Buscar_ID_Data_Minecraft_1_12_2(Minecraft.Diccionario_Bloques_Nombres_Índices[Variable_Bloque_Interior], out Data_Interior);
                    if (!Variable_Estructura_3D)
                    {
                        if (Variable_Estructura == Estructuras.Pyramid || Variable_Estructura == Estructuras.Sphere)
                        {
                            Diámetro_16 = Variable_Diámetro + 16;
                            Bloques_Ancho = 0;
                            Bloques_Alto = 0;
                            for (int Índice_Z = -16, Chunk_Z = -1; Índice_Z < Diámetro_16; Índice_Z += 16, Chunk_Z++, Bloques_Alto += 16)
                            {
                                Bloques_Ancho = 0;
                                for (int Índice_X = -16, Chunk_X = -1; Índice_X < Diámetro_16; Índice_X += 16, Chunk_X++, Bloques_Ancho += 16)
                                {
                                    ChunkRef Chunk = Chunks.CreateChunk(Chunk_X, Chunk_Z);
                                    Chunk.IsLightPopulated = true; // For 1.13+ conversion support.
                                    Chunk.IsTerrainPopulated = true;
                                    Chunk.Blocks.AutoLight = false;
                                    if (Índice_X > -1 && Índice_Z > -1 && Índice_X < Variable_Diámetro && Índice_Z < Variable_Diámetro)
                                    {
                                        for (int Z = 0; Z < 16; Z++)
                                        {
                                            for (int X = 0; X < 16; X++)
                                            {
                                                Chunk.Blocks.SetID(X, 0, Z, (int)BlockType.BEDROCK); // Structure floor
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
                            Bitmap Imagen_Temporal = Obtener_Imagen_Estructura_Masiva(Variable_Rellenar, Variable_Lados, Variable_Diámetro, false, Variable_Diámetro);
                            if (Imagen_Temporal != null)
                            {
                                //Imagen_Temporal.Save(Application.StartupPath + "\\z" + Índice_Y.ToString() + ", " + Diámetro_XZ.ToString() + ".png", ImageFormat.Png);
                                int Ancho = Imagen_Temporal.Width;
                                int Alto = Imagen_Temporal.Height;
                                Bitmap Imagen = new Bitmap(Variable_Diámetro, Variable_Diámetro, PixelFormat.Format32bppArgb);
                                Graphics Pintar = Graphics.FromImage(Imagen);
                                Pintar.CompositingMode = CompositingMode.SourceCopy;
                                Pintar.DrawImage(Imagen_Temporal, new Rectangle((Variable_Diámetro / 2) - (Ancho / 2), (Variable_Diámetro / 2) - (Alto / 2), Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                Pintar.Dispose();
                                Pintar = null;
                                Imagen_Temporal.Dispose();
                                Imagen_Temporal = null;
                                Ancho = Variable_Diámetro;
                                Alto = Variable_Diámetro;
                                BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                                int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                                int Bytes_Diferencia = Ancho_Stride - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                                byte[] Matriz_Bytes = new byte[Ancho_Stride * Alto];
                                Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                                Imagen.UnlockBits(Bitmap_Data);
                                Bitmap_Data = null;
                                for (int Índice_Z = 0, Índice = 0; Índice_Z < Alto; Índice_Z++, Índice += Bytes_Diferencia)
                                {
                                    for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += 4)
                                    {
                                        if (Matriz_Bytes[Índice + 3] > 0) // Not fully transparent
                                        {
                                            if (((Matriz_Bytes[Índice + 2] + Matriz_Bytes[Índice + 1] + Matriz_Bytes[Índice]) / 3) < 128)
                                            {
                                                Bloques.SetID(Índice_X, 62, Índice_Z, (int)ID_Interior);
                                                Bloques.SetData(Índice_X, 62, Índice_Z, (int)Data_Interior);
                                            }
                                            else
                                            {
                                                Bloques.SetID(Índice_X, 62, Índice_Z, (int)ID);
                                                Bloques.SetData(Índice_X, 62, Índice_Z, (int)Data);
                                            }
                                        }
                                    }
                                }
                                Imagen.Dispose();
                                Imagen = null;
                            }
                        }
                    }
                    else
                    {
                        if (Variable_Estructura == Estructuras.Pyramid)
                        {
                            Diámetro_16 = Variable_Diámetro + 16;
                            Bloques_Ancho = 0;
                            Bloques_Alto = 0;
                            for (int Índice_Z = -16, Chunk_Z = -1; Índice_Z < Diámetro_16; Índice_Z += 16, Chunk_Z++, Bloques_Alto += 16)
                            {
                                Bloques_Ancho = 0;
                                for (int Índice_X = -16, Chunk_X = -1; Índice_X < Diámetro_16; Índice_X += 16, Chunk_X++, Bloques_Ancho += 16)
                                {
                                    ChunkRef Chunk = Chunks.CreateChunk(Chunk_X, Chunk_Z);
                                    Chunk.IsLightPopulated = true; // For 1.13+ conversion support.
                                    Chunk.IsTerrainPopulated = true;
                                    Chunk.Blocks.AutoLight = false;
                                    if (Índice_X > -1 && Índice_Z > -1 && Índice_X < Variable_Diámetro && Índice_Z < Variable_Diámetro)
                                    {
                                        for (int Z = 0; Z < 16; Z++)
                                        {
                                            for (int X = 0; X < 16; X++)
                                            {
                                                Chunk.Blocks.SetID(X, 0, Z, (int)BlockType.BEDROCK); // Structure floor
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
                            int Diámetro_3D = ((Variable_Diámetro / 2) * 2) + (Variable_Diámetro % 2 == 0 ? 0 : 1);
                            bool Polaridad_Positiva = true;
                            //bool[,,] Matriz_3D = new bool[Diámetro_3D, Diámetro, Diámetro];
                            for (int Índice_Y = 1, Diámetro_XZ = Variable_Diámetro % 2 != 0 ? 1 : 2; Índice_Y <= Diámetro_3D && Índice_Y < 256; Índice_Y++)
                            {
                                Bitmap Imagen_Temporal = Obtener_Imagen_Estructura_Masiva(Variable_Rellenar, Variable_Lados, Diámetro_XZ, false, Variable_Diámetro);
                                if (Imagen_Temporal != null)
                                {
                                    //Imagen_Temporal.Save(Application.StartupPath + "\\z" + Índice_Y.ToString() + ", " + Diámetro_XZ.ToString() + ".png", ImageFormat.Png);
                                    int Ancho = Imagen_Temporal.Width;
                                    int Alto = Imagen_Temporal.Height;
                                    Bitmap Imagen = new Bitmap(Variable_Diámetro, Variable_Diámetro, PixelFormat.Format32bppArgb);
                                    Graphics Pintar = Graphics.FromImage(Imagen);
                                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                                    Pintar.DrawImage(Imagen_Temporal, new Rectangle((Variable_Diámetro / 2) - (Ancho / 2), (Variable_Diámetro / 2) - (Alto / 2), Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                    Pintar.Dispose();
                                    Pintar = null;
                                    Imagen_Temporal.Dispose();
                                    Imagen_Temporal = null;
                                    Ancho = Variable_Diámetro;
                                    Alto = Variable_Diámetro;
                                    BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                                    int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                                    int Bytes_Diferencia = Ancho_Stride - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                                    byte[] Matriz_Bytes = new byte[Ancho_Stride * Alto];
                                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                                    Imagen.UnlockBits(Bitmap_Data);
                                    Bitmap_Data = null;
                                    for (int Índice_Z = 0, Índice = 0; Índice_Z < Alto; Índice_Z++, Índice += Bytes_Diferencia)
                                    {
                                        for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += 4)
                                        {
                                            if (Matriz_Bytes[Índice + 3] > 0) // Not fully transparent
                                            {
                                                if (((Matriz_Bytes[Índice + 2] + Matriz_Bytes[Índice + 1] + Matriz_Bytes[Índice]) / 3) < 128)
                                                {
                                                    Bloques.SetID(Índice_X, Índice_Y, Índice_Z, (int)ID_Interior);
                                                    Bloques.SetData(Índice_X, Índice_Y, Índice_Z, (int)Data_Interior);
                                                }
                                                else
                                                {
                                                    Bloques.SetID(Índice_X, Índice_Y, Índice_Z, (int)ID);
                                                    Bloques.SetData(Índice_X, Índice_Y, Índice_Z, (int)Data);
                                                }
                                            }
                                        }
                                    }
                                    Imagen.Dispose();
                                    Imagen = null;
                                }
                                //MessageBox.Show(Diámetro_XZ.ToString());
                                if (Polaridad_Positiva) Diámetro_XZ += 2;
                                else Diámetro_XZ -= 2;
                                if (Diámetro_XZ == Variable_Diámetro)
                                {
                                    Polaridad_Positiva = !Polaridad_Positiva;
                                    //MessageBox.Show(Polaridad_Positiva.ToString());
                                }
                            }
                        }
                        else if (Variable_Estructura == Estructuras.Spiral_stairs)
                        {
                            //Bitmap Imagen = Ventana_Principal.Generar_Imagen_Espiral(64, 64, true);
                            Diámetro_16 = Variable_Diámetro + 16;
                            Bloques_Ancho = 0;
                            Bloques_Alto = 0;
                            for (int Índice_Z = -16, Chunk_Z = -1; Índice_Z < Diámetro_16; Índice_Z += 16, Chunk_Z++, Bloques_Alto += 16)
                            {
                                Bloques_Ancho = 0;
                                for (int Índice_X = -16, Chunk_X = -1; Índice_X < Diámetro_16; Índice_X += 16, Chunk_X++, Bloques_Ancho += 16)
                                {
                                    ChunkRef Chunk = Chunks.CreateChunk(Chunk_X, Chunk_Z);
                                    Chunk.IsLightPopulated = true; // For 1.13+ conversion support.
                                    Chunk.IsTerrainPopulated = true;
                                    Chunk.Blocks.AutoLight = false;
                                    if (Índice_X > -1 && Índice_Z > -1 && Índice_X < Variable_Diámetro && Índice_Z < Variable_Diámetro)
                                    {
                                        for (int Z = 0; Z < 16; Z++)
                                        {
                                            for (int X = 0; X < 16; X++)
                                            {
                                                Chunk.Blocks.SetID(X, 0, Z, (int)BlockType.BEDROCK); // Structure floor
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
                            int Diámetro_3D = ((Variable_Diámetro / 2) * 2) + (Variable_Diámetro % 2 == 0 ? 0 : 1);
                            bool Polaridad_Positiva = true;
                            //bool[,,] Matriz_3D = new bool[Diámetro_3D, Diámetro, Diámetro];
                            for (int Índice_Y = 1, Diámetro_XZ = Variable_Diámetro % 2 != 0 ? 1 : 2; Índice_Y <= Diámetro_3D && Índice_Y < 256; Índice_Y++)
                            {
                                Bitmap Imagen_Temporal = Obtener_Imagen_Estructura_Masiva(Variable_Rellenar, Variable_Lados, Diámetro_XZ, false, Variable_Diámetro);
                                if (Imagen_Temporal != null)
                                {
                                    //Imagen_Temporal.Save(Application.StartupPath + "\\z" + Índice_Y.ToString() + ", " + Diámetro_XZ.ToString() + ".png", ImageFormat.Png);
                                    int Ancho = Imagen_Temporal.Width;
                                    int Alto = Imagen_Temporal.Height;
                                    Bitmap Imagen = new Bitmap(Variable_Diámetro, Variable_Diámetro, PixelFormat.Format32bppArgb);
                                    Graphics Pintar = Graphics.FromImage(Imagen);
                                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                                    Pintar.DrawImage(Imagen_Temporal, new Rectangle((Variable_Diámetro / 2) - (Ancho / 2), (Variable_Diámetro / 2) - (Alto / 2), Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                    Pintar.Dispose();
                                    Pintar = null;
                                    Imagen_Temporal.Dispose();
                                    Imagen_Temporal = null;
                                    Ancho = Variable_Diámetro;
                                    Alto = Variable_Diámetro;
                                    BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                                    int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                                    int Bytes_Diferencia = Ancho_Stride - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                                    byte[] Matriz_Bytes = new byte[Ancho_Stride * Alto];
                                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                                    Imagen.UnlockBits(Bitmap_Data);
                                    Bitmap_Data = null;
                                    for (int Índice_Z = 0, Índice = 0; Índice_Z < Alto; Índice_Z++, Índice += Bytes_Diferencia)
                                    {
                                        for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += 4)
                                        {
                                            if (Matriz_Bytes[Índice + 3] > 0) // Not fully transparent
                                            {
                                                if (((Matriz_Bytes[Índice + 2] + Matriz_Bytes[Índice + 1] + Matriz_Bytes[Índice]) / 3) < 128)
                                                {
                                                    Bloques.SetID(Índice_X, Índice_Y, Índice_Z, (int)ID_Interior);
                                                    Bloques.SetData(Índice_X, Índice_Y, Índice_Z, (int)Data_Interior);
                                                }
                                                else
                                                {
                                                    Bloques.SetID(Índice_X, Índice_Y, Índice_Z, (int)ID);
                                                    Bloques.SetData(Índice_X, Índice_Y, Índice_Z, (int)Data);
                                                }
                                            }
                                        }
                                    }
                                    Imagen.Dispose();
                                    Imagen = null;
                                }
                                //MessageBox.Show(Diámetro_XZ.ToString());
                                if (Polaridad_Positiva) Diámetro_XZ += 2;
                                else Diámetro_XZ -= 2;
                                if (Diámetro_XZ == Variable_Diámetro)
                                {
                                    Polaridad_Positiva = !Polaridad_Positiva;
                                    //MessageBox.Show(Polaridad_Positiva.ToString());
                                }
                            }
                        }
                        else if (Variable_Estructura == Estructuras.Sphere)
                        {
                            Diámetro_16 = Variable_Diámetro + 16;
                            Bloques_Ancho = 0;
                            Bloques_Alto = 0;
                            List<int> Lista_Diámetros_Esfera = Obtener_Lista_Diámetros_Esfera(Variable_Diámetro);
                            if (Lista_Diámetros_Esfera != null)
                            {
                                for (int Índice_Z = -16, Chunk_Z = -1; Índice_Z < Diámetro_16; Índice_Z += 16, Chunk_Z++, Bloques_Alto += 16)
                                {
                                    Bloques_Ancho = 0;
                                    for (int Índice_X = -16, Chunk_X = -1; Índice_X < Diámetro_16; Índice_X += 16, Chunk_X++, Bloques_Ancho += 16)
                                    {
                                        ChunkRef Chunk = Chunks.CreateChunk(Chunk_X, Chunk_Z);
                                        Chunk.IsLightPopulated = true; // For 1.13+ conversion support.
                                        Chunk.IsTerrainPopulated = true;
                                        Chunk.Blocks.AutoLight = false;
                                        if (Índice_X > -1 && Índice_Z > -1 && Índice_X < Variable_Diámetro && Índice_Z < Variable_Diámetro)
                                        {
                                            for (int Z = 0; Z < 16; Z++)
                                            {
                                                for (int X = 0; X < 16; X++)
                                                {
                                                    Chunk.Blocks.SetID(X, 0, Z, (int)BlockType.BEDROCK); // Structure floor
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
                                int Diámetro_3D = ((Variable_Diámetro / 2) * 2) + (Variable_Diámetro % 2 == 0 ? 0 : 1);
                                int Diámetro_3D_Mitad = Diámetro_3D / 2;
                                //bool Polaridad_Positiva = true;
                                //bool[,,] Matriz_3D = new bool[Diámetro_3D, Diámetro, Diámetro];
                                Bitmap Imagen = null;
                                int Índice_Anterior = Lista_Diámetros_Esfera[0];
                                for (int Índice_Y = 1; Índice_Y <= Diámetro_3D && Índice_Y < 256; Índice_Y++)
                                {
                                    if (!Variable_Rellenar)
                                    {
                                        //int Índice_Anterior = Math.Max(Índice_Y - 2, Índice_Y - 1);
                                        int Índice_Siguiente = Índice_Y <= Diámetro_3D_Mitad ? Índice_Y - 2 : Índice_Y;
                                        if (Índice_Siguiente < 0) Índice_Siguiente = 0;
                                        else if (Índice_Siguiente >= Lista_Diámetros_Esfera.Count) Índice_Siguiente = Lista_Diámetros_Esfera.Count - 1;
                                        int Diámetro_Menor = Lista_Diámetros_Esfera[Índice_Siguiente]; //Math.Min(Lista_Diámetros_Esfera[Índice_Anterior], Lista_Diámetros_Esfera[Índice_Siguiente]);
                                        if (Diámetro_Menor != Lista_Diámetros_Esfera[Índice_Y - 1])
                                        {
                                            Bitmap Imagen_Temporal = null;
                                            if (Índice_Y <= Diámetro_3D_Mitad)
                                            {
                                                Imagen = Obtener_Imagen_Estructura_Masiva(true, Variable_Lados, Lista_Diámetros_Esfera[Índice_Y - 1], false, Variable_Diámetro);
                                                Imagen_Temporal = Obtener_Imagen_Estructura_Masiva(true, Variable_Lados, Diámetro_Menor, false, Variable_Diámetro);
                                                Índice_Anterior = Diámetro_Menor;
                                            }
                                            else
                                            {
                                                Imagen = Obtener_Imagen_Estructura_Masiva(true, Variable_Lados, Lista_Diámetros_Esfera[Índice_Y - 1], false, Variable_Diámetro);
                                                Imagen_Temporal = Obtener_Imagen_Estructura_Masiva(true, Variable_Lados, Diámetro_Menor, false, Variable_Diámetro);
                                                Índice_Anterior = Diámetro_Menor;
                                            }
                                            //MessageBox.Show(Imagen_Temporal.Size.ToString(), Diámetro.ToString());
                                            BitmapData Bitmap_Data = Imagen_Temporal.LockBits(new Rectangle(0, 0, Variable_Diámetro, Variable_Diámetro), ImageLockMode.ReadWrite, Imagen_Temporal.PixelFormat);
                                            int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                                            int Bytes_Diferencia = Ancho_Stride - ((Variable_Diámetro * Image.GetPixelFormatSize(Imagen_Temporal.PixelFormat)) / 8);
                                            byte[] Matriz_Bytes_Temporal = new byte[Ancho_Stride * Variable_Diámetro];
                                            Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes_Temporal, 0, Matriz_Bytes_Temporal.Length);
                                            Imagen_Temporal.UnlockBits(Bitmap_Data);
                                            Bitmap_Data = null;
                                            /*if (Índice_Y == 56)
                                            {
                                                Imagen_Temporal.Save(Application.StartupPath + "\\aaa.png");
                                                Imagen.Save(Application.StartupPath + "\\bbb.png");
                                            }*/
                                            Imagen_Temporal.Dispose();
                                            Imagen_Temporal = null;

                                            Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Variable_Diámetro, Variable_Diámetro), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                                            byte[] Matriz_Bytes = new byte[Ancho_Stride * Variable_Diámetro];
                                            Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                                            for (int Índice_Z = 0, Índice = 0; Índice_Z < Variable_Diámetro; Índice_Z++, Índice += Bytes_Diferencia)
                                            {
                                                for (int Índice_X = 0; Índice_X < Variable_Diámetro; Índice_X++, Índice += 4)
                                                {
                                                    if (Matriz_Bytes_Temporal[Índice + 3] > 0) // Not fully transparent
                                                    {
                                                        Matriz_Bytes[Índice + 3] = 0; // Fully transparent
                                                        Matriz_Bytes[Índice + 2] = 0;
                                                        Matriz_Bytes[Índice + 1] = 0;
                                                        Matriz_Bytes[Índice] = 0;
                                                    }
                                                }
                                            }
                                            Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                                            Imagen.UnlockBits(Bitmap_Data);
                                            Bitmap_Data = null;
                                            Matriz_Bytes_Temporal = null;
                                            Matriz_Bytes = null;
                                            /*//Imagen_Temporal = new Bitmap(Diámetro, Diámetro, PixelFormat.Format32bppArgb);
                                            Graphics Pintar = Graphics.FromImage(Imagen);
                                            Pintar.CompositingMode = CompositingMode.SourceOver;
                                            Pintar.DrawImage(Imagen_Temporal, new Rectangle((Imagen.Width / 2) - (Diámetro_Menor / 2), (Imagen.Height / 2) - (Diámetro_Menor / 2), Diámetro_Menor, Diámetro_Menor), new Rectangle(0, 0, Diámetro_Menor, Diámetro_Menor), GraphicsUnit.Pixel);
                                            Pintar.Dispose();
                                            Pintar = null;
                                            Imagen_Temporal.Dispose();
                                            Imagen_Temporal = null;*/
                                        }
                                    }
                                    else
                                    {
                                        Imagen = Obtener_Imagen_Estructura_Masiva(Índice_Y != 1 && Índice_Y < Diámetro_3D ? Variable_Rellenar : true, Variable_Lados, Lista_Diámetros_Esfera[Índice_Y - 1], false, Variable_Diámetro);
                                    }
                                    if (Imagen != null)
                                    {
                                        //Imagen_Temporal.Save(Application.StartupPath + "\\z" + Índice_Y.ToString() + ", " + Diámetro_XZ.ToString() + ".png", ImageFormat.Png);
                                        int Ancho = Variable_Diámetro;
                                        int Alto = Variable_Diámetro;
                                        /*Bitmap Imagen = new Bitmap(Diámetro, Diámetro, PixelFormat.Format32bppArgb);
                                        Graphics Pintar = Graphics.FromImage(Imagen);
                                        Pintar.CompositingMode = CompositingMode.SourceCopy;
                                        Pintar.DrawImage(Imagen_Temporal, new Rectangle((Diámetro / 2) - (Ancho / 2), (Diámetro / 2) - (Alto / 2), Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                        Pintar.Dispose();
                                        Pintar = null;
                                        Imagen_Temporal.Dispose();
                                        Imagen_Temporal = null;
                                        Ancho = Diámetro;
                                        Alto = Diámetro;*/
                                        BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                                        int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                                        int Bytes_Diferencia = Ancho_Stride - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                                        byte[] Matriz_Bytes = new byte[Ancho_Stride * Alto];
                                        Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                                        Imagen.UnlockBits(Bitmap_Data);
                                        Bitmap_Data = null;
                                        for (int Índice_Z = 0, Índice = 0; Índice_Z < Alto; Índice_Z++, Índice += Bytes_Diferencia)
                                        {
                                            for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += 4)
                                            {
                                                if (Matriz_Bytes[Índice + 3] > 0) // Not fully transparent
                                                {
                                                    if (((Matriz_Bytes[Índice + 2] + Matriz_Bytes[Índice + 1] + Matriz_Bytes[Índice]) / 3) < 128)
                                                    {
                                                        Bloques.SetID(Índice_X, Índice_Y, Índice_Z, (int)ID_Interior);
                                                        Bloques.SetData(Índice_X, Índice_Y, Índice_Z, (int)Data_Interior);
                                                    }
                                                    else
                                                    {
                                                        Bloques.SetID(Índice_X, Índice_Y, Índice_Z, (int)ID);
                                                        Bloques.SetData(Índice_X, Índice_Y, Índice_Z, (int)Data);
                                                    }
                                                }
                                            }
                                        }
                                        Imagen.Dispose();
                                        Imagen = null;
                                    }
                                }
                            }
                        }
                        else if (Variable_Estructura == Estructuras.Infdev_pyramid)
                        {
                            Diámetro_16 = 253 + 16;
                            Bloques_Ancho = 0;
                            Bloques_Alto = 0;
                            for (int Índice_Z = -16, Chunk_Z = -1; Índice_Z < Diámetro_16; Índice_Z += 16, Chunk_Z++, Bloques_Alto += 16)
                            {
                                Bloques_Ancho = 0;
                                for (int Índice_X = -16, Chunk_X = -1; Índice_X < Diámetro_16; Índice_X += 16, Chunk_X++, Bloques_Ancho += 16)
                                {
                                    ChunkRef Chunk = Chunks.CreateChunk(Chunk_X, Chunk_Z);
                                    Chunk.IsLightPopulated = true; // For 1.13+ conversion support.
                                    Chunk.IsTerrainPopulated = true;
                                    Chunk.Blocks.AutoLight = false;
                                    if (Índice_X > -1 && Índice_Z > -1 && Índice_X < 253 && Índice_Z < 253)
                                    {
                                        for (int Z = 0; Z < 16; Z++)
                                        {
                                            for (int X = 0; X < 16; X++)
                                            {
                                                Chunk.Blocks.SetID(X, 0, Z, (int)BlockType.BEDROCK); // Structure floor
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
                            int Diámetro_3D = 64 + 63;
                            for (int Índice_Y = 1; Índice_Y <= Diámetro_3D && Índice_Y < 256; Índice_Y++)
                            {
                                Bitmap Imagen_Temporal = Obtener_Imagen_Estructura_Masiva(Variable_Rellenar, Variable_Lados, Índice_Y, false, Variable_Diámetro);
                                if (Imagen_Temporal != null)
                                {
                                    //Imagen_Temporal.Save(Application.StartupPath + "\\z" + Índice_Y.ToString() + ", " + Diámetro_XZ.ToString() + ".png", ImageFormat.Png);
                                    int Ancho = Imagen_Temporal.Width;
                                    int Alto = Imagen_Temporal.Height;
                                    Bitmap Imagen = new Bitmap(253, 253, PixelFormat.Format32bppArgb);
                                    Graphics Pintar = Graphics.FromImage(Imagen);
                                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                                    Pintar.DrawImage(Imagen_Temporal, new Rectangle((253 / 2) - (Ancho / 2), (253 / 2) - (Alto / 2), Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                    Pintar.Dispose();
                                    Pintar = null;
                                    Imagen_Temporal.Dispose();
                                    Imagen_Temporal = null;
                                    Ancho = 253;
                                    Alto = 253;
                                    BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                                    int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                                    int Bytes_Diferencia = Ancho_Stride - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                                    byte[] Matriz_Bytes = new byte[Ancho_Stride * Alto];
                                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                                    Imagen.UnlockBits(Bitmap_Data);
                                    Bitmap_Data = null;
                                    for (int Índice_Z = 0, Índice = 0; Índice_Z < Alto; Índice_Z++, Índice += Bytes_Diferencia)
                                    {
                                        for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += 4)
                                        {
                                            if (Matriz_Bytes[Índice + 3] > 0) // Not fully transparent
                                            {
                                                if (((Matriz_Bytes[Índice + 2] + Matriz_Bytes[Índice + 1] + Matriz_Bytes[Índice]) / 3) < 128)
                                                {
                                                    Bloques.SetID(Índice_X, Índice_Y, Índice_Z, (int)ID_Interior);
                                                    Bloques.SetData(Índice_X, Índice_Y, Índice_Z, (int)Data_Interior);
                                                }
                                                else
                                                {
                                                    Bloques.SetID(Índice_X, Índice_Y, Índice_Z, (int)ID);
                                                    Bloques.SetData(Índice_X, Índice_Y, Índice_Z, (int)Data);
                                                }
                                            }
                                        }
                                    }
                                    Imagen.Dispose();
                                    Imagen = null;
                                }
                            }
                        }
                        else if (Variable_Estructura == Estructuras.Labyrinth || Variable_Estructura == Estructuras.Trapped_labyrinth)
                        {
                            Bitmap Imagen = Imagen_Blanco_Negro.Clone() as Bitmap; //Obtener_Imagen_Estructura_Masiva(Variable_Rellenar, Variable_Lados, Variable_Diámetro, false, 0);
                            if (Imagen != null)
                            {
                                //Program.Guardar_Imagen_Temporal(Imagen);
                                int Ancho = Imagen.Width;
                                int Alto = Imagen.Height;
                                Diámetro_16 = Ancho + 16;
                                Bloques_Ancho = 0;
                                Bloques_Alto = 0;
                                for (int Índice_Z = -16, Chunk_Z = -1; Índice_Z < Diámetro_16; Índice_Z += 16, Chunk_Z++, Bloques_Alto += 16)
                                {
                                    Bloques_Ancho = 0;
                                    for (int Índice_X = -16, Chunk_X = -1; Índice_X < Diámetro_16; Índice_X += 16, Chunk_X++, Bloques_Ancho += 16)
                                    {
                                        ChunkRef Chunk = Chunks.CreateChunk(Chunk_X, Chunk_Z);
                                        Chunk.IsLightPopulated = true; // For 1.13+ conversion support.
                                        Chunk.IsTerrainPopulated = true;
                                        Chunk.Blocks.AutoLight = false;
                                        if (Índice_X > -1 && Índice_Z > -1 && Índice_X < Ancho && Índice_Z < Alto)
                                        {
                                            for (int Z = 0; Z < 16; Z++)
                                            {
                                                for (int X = 0; X < 16; X++)
                                                {
                                                    Chunk.Blocks.SetID(X, 0, Z, ID/*(int)BlockType.BEDROCK*/); // Structure floor
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
                                BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                                int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                                int Bytes_Diferencia = Ancho_Stride - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                                byte[] Matriz_Bytes = new byte[Ancho_Stride * Alto];
                                Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                                Imagen.UnlockBits(Bitmap_Data);
                                Bitmap_Data = null;
                                for (int Índice_Y = 0; Índice_Y <= 3; Índice_Y++)
                                {
                                    if (Índice_Y == 0)
                                    {
                                        if (Variable_Estructura == Estructuras.Labyrinth)
                                        {
                                            /*for (int Índice_Z = 2; Índice_Z < Alto - 2; Índice_Z += 2)
                                            {
                                                for (int Índice_X = 2; Índice_X < Ancho - 2; Índice_X += 2)
                                                {
                                                    Bloques.SetID(Índice_X, 0, Índice_Z - 1, 123); // Redstone lamp
                                                    Bloques.SetData(Índice_X, 0, Índice_Z - 1, 0); // North
                                                    Bloques.SetID(Índice_X, 0, Índice_Z + 1, 123); // Redstone lamp
                                                    Bloques.SetData(Índice_X, 0, Índice_Z + 1, 0); // South
                                                    Bloques.SetID(Índice_X - 1, 0, Índice_Z, 123); // Redstone lamp
                                                    Bloques.SetData(Índice_X - 1, 0, Índice_Z, 0); // West
                                                    Bloques.SetID(Índice_X + 1, 0, Índice_Z, 123); // Redstone lamp
                                                    Bloques.SetData(Índice_X + 1, 0, Índice_Z, 0); // East

                                                    //Bloques.SetID(Índice_X, 0, Índice_Z, 46); // Tnt, just to scare the players by it's sound
                                                    //Bloques.SetData(Índice_X, 0, Índice_Z, 0); // It might help the players to know if they went over a path already...
                                                }
                                            }*/
                                        }
                                        else if (Variable_Estructura == Estructuras.Trapped_labyrinth)
                                        {
                                            for (int Índice_Z = 0; Índice_Z < Alto; Índice_Z++)
                                            {
                                                for (int Índice_X = 0; Índice_X < Ancho; Índice_X++)
                                                {
                                                    if ((Índice_X > 1 || Índice_Z > 1) && (Índice_X < Ancho - 2 || Índice_Z < Alto - 2))
                                                    {
                                                        Bloques.SetID(Índice_X, Índice_Y, Índice_Z, 18); // Oak leaves
                                                        Bloques.SetData(Índice_X, Índice_Y, Índice_Z, 0);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (Índice_Y < 3)
                                    {
                                        for (int Índice_Z = 0, Índice = 0; Índice_Z < Alto; Índice_Z++, Índice += Bytes_Diferencia)
                                        {
                                            for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += 4)
                                            {
                                                if (Matriz_Bytes[Índice + 3] > 0) // Not fully transparent
                                                {
                                                    if (((Matriz_Bytes[Índice + 2] + Matriz_Bytes[Índice + 1] + Matriz_Bytes[Índice]) / 3) < 128)
                                                    {
                                                        Bloques.SetID(Índice_X, Índice_Y, Índice_Z, (int)ID_Interior);
                                                        Bloques.SetData(Índice_X, Índice_Y, Índice_Z, (int)Data_Interior);
                                                    }
                                                    else
                                                    {
                                                        Bloques.SetID(Índice_X, Índice_Y, Índice_Z, (int)ID);
                                                        Bloques.SetData(Índice_X, Índice_Y, Índice_Z, (int)Data);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (Variable_Rellenar) // Add a roof
                                    {
                                        for (int Índice_Z = 0; Índice_Z < Alto; Índice_Z++)
                                        {
                                            for (int Índice_X = 0; Índice_X < Ancho; Índice_X++)
                                            {
                                                Bloques.SetID(Índice_X, Índice_Y, Índice_Z, (int)ID);
                                                Bloques.SetData(Índice_X, Índice_Y, Índice_Z, (int)Data);
                                            }
                                        }
                                    }
                                }
                                //new TileEntityControl(); // Command block
                                AlphaBlock Bloque = new AlphaBlock(BlockType.SIGN_POST); // Sign
                                Bloque.Data = 6; // Southeast
                                TileEntitySign Entidad_Señal = Bloque.GetTileEntity() as TileEntitySign;
                                Entidad_Señal.Text1 = "Welcome!";
                                Entidad_Señal.Text2 = "Choose a path";
                                Entidad_Señal.Text3 = "and try to get";
                                Entidad_Señal.Text4 = "to the finish!";
                                Bloques.SetBlock(0, 1, 0, Bloque); // Sign
                                Entidad_Señal.Text1 = "Congrats!!";
                                Entidad_Señal.Text2 = "Your time is";
                                Entidad_Señal.Text3 = "in the chest";
                                Entidad_Señal.Text4 = "<-------";
                                Bloque.Data = 8; // North
                                Bloques.SetBlock(Ancho - 2, 1, Alto - 1, Bloque); // Sign

                                Bloque = new AlphaBlock(BlockType.COMMAND_BLOCK); // Command block
                                TileEntityControl Entidad_Bloque_Comandos = Bloque.GetTileEntity() as TileEntityControl;
                                Entidad_Bloque_Comandos.Command = "/setblock " + (Ancho - 1).ToString() + " 2 " + (Alto - 1).ToString() + " minecraft:hopper"; // Start the timer
                                Bloques.SetBlock(1, 0, 1, Bloque); // Command block
                                Bloques.SetID(1, 1, 1, 70); // Stone pressure plate
                                Bloques.SetData(1, 1, 1, 0);

                                Entidad_Bloque_Comandos.Command = "/setblock " + (Ancho - 1).ToString() + " 2 " + (Alto - 2).ToString() + " minecraft:redstone_block"; // Stop the timer
                                Bloques.SetBlock(Ancho - 2, 0, Alto - 2, Bloque); // Command block
                                Bloques.SetID(Ancho - 2, 1, Alto - 2, 70); // Stone pressure plate
                                Bloques.SetData(Ancho - 2, 1, Alto - 2, 0);

                                //Bloques.SetID(Ancho - 1, 3, Alto - 1, 54); // Chest
                                //Bloques.SetData(Ancho - 1, 3, Alto - 1, 2); // North

                                Item Objeto = new Item(264); // Diamonds
                                Objeto.Count = 64; // Stack of 64
                                Bloque = new AlphaBlock(BlockType.CHEST); // Chest
                                Bloque.Data = 3;
                                TileEntityChest Entidad_Cofre = Bloque.GetTileEntity() as TileEntityChest;
                                for (int Índice = 0; Índice < Entidad_Cofre.Items.Capacity; Índice++)
                                {
                                    Entidad_Cofre.Items[Índice] = Objeto;
                                }
                                Bloques.SetBlock(Ancho - 1, 3, Alto - 1, Bloque); // Chest

                                // Note: It seems that Substrate doesn't support hoppers, so it will be
                                // placed by the starting command block (requires breaking it before a restart)

                                /*Bloque = new AlphaBlock(BlockType.HOPPER); // Hopper // Substrate gives error?
                                TileEntityTrap Entidad_Embudo = Bloque.GetTileEntity() as TileEntityTrap; // Substrate gives error?
                                Bloques.SetBlock(Ancho - 1, 2, Alto - 1, Bloque); // Hopper // Substrate gives error?
                                
                                Bloques.SetID(Ancho - 1, 2, Alto - 1, 154); // Hopper // Substrate gives error?
                                //Bloques.SetData(Ancho - 1, 2, Alto - 1, 0); // Down // Substrate gives error?

                                Bloques.SetID(Ancho - 1, 2, Alto - 2, 152); // Redstone block
                                Bloques.SetData(Ancho - 1, 2, Alto - 2, 0);*/

                                Bloques.SetID(Ancho - 1, 1, Alto - 1, 54); // Chest
                                Bloques.SetData(Ancho - 1, 1, Alto - 1, 2); // North

                                if (Variable_Estructura == Estructuras.Trapped_labyrinth)
                                {
                                    Objeto = new Item(385); // Fire charge
                                    Objeto.Count = 64; // Stack of 64
                                    Bloque = new AlphaBlock(BlockType.DISPENSER); // Dispenser
                                    Bloque.Data = 3;
                                    TileEntityTrap Entidad_Dispensador = Bloque.GetTileEntity() as TileEntityTrap;
                                    for (int Índice = 0; Índice < Entidad_Dispensador.Items.Capacity; Índice++)
                                    {
                                        Entidad_Dispensador.Items[Índice] = Objeto;
                                    }
                                    int Porcentaje_Trampas = 10;
                                    for (int Índice_Z = 2; Índice_Z < Alto - 2; Índice_Z += 2)
                                    {
                                        for (int Índice_X = 2; Índice_X < Ancho - 2; Índice_X += 2)
                                        {
                                            bool Trampa_Norte = Program.Rand.Next(0, 100) < Porcentaje_Trampas;
                                            bool Trampa_Sur = Program.Rand.Next(0, 100) < Porcentaje_Trampas;
                                            bool Trampa_Oeste = Program.Rand.Next(0, 100) < Porcentaje_Trampas;
                                            bool Trampa_Este = Program.Rand.Next(0, 100) < Porcentaje_Trampas;
                                            if (Trampa_Norte)
                                            {
                                                Bloques.SetID(Índice_X, 2, Índice_Z - 1, 50); // Torch
                                                Bloques.SetData(Índice_X, 2, Índice_Z - 1, 4); // North

                                                Bloque.Data = 3; // Facing south
                                                Bloques.SetBlock(Índice_X, 0, Índice_Z - 1, Bloque); // Dispenser

                                                Bloques.SetID(Índice_X, 1, Índice_Z - 1, 70); // Stone pressure plate
                                                Bloques.SetData(Índice_X, 1, Índice_Z - 1, 0);
                                            }
                                            if (Trampa_Sur)
                                            {
                                                Bloques.SetID(Índice_X, 2, Índice_Z + 1, 50); // Torch
                                                Bloques.SetData(Índice_X, 2, Índice_Z + 1, 3); // South

                                                Bloque.Data = 2; // Facing north
                                                Bloques.SetBlock(Índice_X, 0, Índice_Z + 1, Bloque); // Dispenser

                                                Bloques.SetID(Índice_X, 1, Índice_Z + 1, 70); // Stone pressure plate
                                                Bloques.SetData(Índice_X, 1, Índice_Z + 1, 0);
                                            }
                                            if (Trampa_Oeste)
                                            {
                                                Bloques.SetID(Índice_X - 1, 2, Índice_Z, 50); // Torch
                                                Bloques.SetData(Índice_X - 1, 2, Índice_Z, 1); // West

                                                Bloque.Data = 5; // Facing east
                                                Bloques.SetBlock(Índice_X - 1, 0, Índice_Z, Bloque); // Dispenser

                                                Bloques.SetID(Índice_X - 1, 1, Índice_Z, 70); // Stone pressure plate
                                                Bloques.SetData(Índice_X - 1, 1, Índice_Z, 0);
                                            }
                                            if (Trampa_Este)
                                            {
                                                Bloques.SetID(Índice_X + 1, 2, Índice_Z, 50); // Torch
                                                Bloques.SetData(Índice_X + 1, 2, Índice_Z, 2); // East

                                                Bloque.Data = 4; // Facing west
                                                Bloques.SetBlock(Índice_X + 1, 0, Índice_Z, Bloque); // Dispenser

                                                Bloques.SetID(Índice_X + 1, 1, Índice_Z, 70); // Stone pressure plate
                                                Bloques.SetData(Índice_X + 1, 1, Índice_Z, 0);
                                            }
                                            if (Trampa_Norte || Trampa_Sur || Trampa_Oeste || Trampa_Este)
                                            {
                                                Bloques.SetID(Índice_X, 0, Índice_Z, 0); // Air
                                                Bloques.SetData(Índice_X, 0, Índice_Z, 0);
                                            }
                                        }
                                    }
                                    /*Bloques.SetID(2, 0, 1, 23); // Dispenser
                                    Bloques.SetData(2, 0, 1, 3); // South
                                    Bloques.SetID(1, 0, 2, 23); // Dispenser
                                    Bloques.SetData(1, 0, 2, 5); // East*//*
                                    Bloques.SetID(2, 0, 2, 0); // Air
                                    Bloques.SetData(2, 0, 2, 0);
                                    Bloques.SetID(2, 1, 1, 70); // Stone pressure plate
                                    Bloques.SetData(2, 1, 1, 0);
                                    Bloques.SetID(1, 1, 2, 70); // Stone pressure plate
                                    Bloques.SetData(1, 1, 2, 0);
                                    //TileEntityChest Entidad = new TileEntityChest();
                                    Bloques.SetBlock(2, 0, 1, Bloque);
                                    Bloque.Data = 5;
                                    Bloques.SetBlock(1, 0, 2, Bloque);
                                    //Bloques.SetTileEntity(2, 0, 1, Entidad);
                                    //Bloques.SetTileEntity(1, 0, 2, Entidad);

                                    //TileEntity Entidad = new TileEntity("minecraft:dispenser");
                                    //TagNodeList Nodo = (TagNodeList)Entidad.BuildTree();
                                    //Nodo.Add(new TagNodeList(TagType.TAG_LIST, ));

                                    //Bloques.SetTileEntity(2, 0, 1, Entidad);*/
                                }
                                else
                                {
                                    //Viewport3D
                                    int Porcentaje_Antorchas = 20;
                                    for (int Índice_Z = 2; Índice_Z < Alto - 2; Índice_Z += 2)
                                    {
                                        for (int Índice_X = 2; Índice_X < Ancho - 2; Índice_X += 2)
                                        {
                                            int Dirección = Program.Rand.Next(0, 4); // Up to 1 random torch on each column
                                            if (Dirección == 0)
                                            {
                                                if (Program.Rand.Next(0, 100) < Porcentaje_Antorchas)
                                                {
                                                    Bloques.SetID(Índice_X, 2, Índice_Z - 1, 50); // Torch
                                                    Bloques.SetData(Índice_X, 2, Índice_Z - 1, 4); // North
                                                }
                                            }
                                            if (Dirección == 1)
                                            {
                                                if (Program.Rand.Next(0, 100) < Porcentaje_Antorchas)
                                                {
                                                    Bloques.SetID(Índice_X, 2, Índice_Z + 1, 50); // Torch
                                                    Bloques.SetData(Índice_X, 2, Índice_Z + 1, 3); // South
                                                }
                                            }
                                            if (Dirección == 2)
                                            {
                                                if (Program.Rand.Next(0, 100) < Porcentaje_Antorchas)
                                                {
                                                    Bloques.SetID(Índice_X - 1, 2, Índice_Z, 50); // Torch
                                                    Bloques.SetData(Índice_X - 1, 2, Índice_Z, 1); // West
                                                }
                                            }
                                            if (Dirección == 3)
                                            {
                                                if (Program.Rand.Next(0, 100) < Porcentaje_Antorchas)
                                                {
                                                    Bloques.SetID(Índice_X + 1, 2, Índice_Z, 50); // Torch
                                                    Bloques.SetData(Índice_X + 1, 2, Índice_Z, 2); // East
                                                }
                                            }
                                        }
                                    }
                                    /*for (int Índice_Z = 2, Índice = 0; Índice_Z < Alto - 2; Índice_Z += 2)
                                    {
                                        for (int Índice_X = 2; Índice_X < Ancho - 2; Índice_X += 2)
                                        {
                                            Índice = (((Índice_Z - 1) * Ancho) + (Índice_X)) * 4;
                                            if (Matriz_Bytes[Índice + 3] > 0 && ((Matriz_Bytes[Índice + 2] + Matriz_Bytes[Índice + 1] + Matriz_Bytes[Índice]) / 3) < 128) // Not fully transparent
                                            {
                                                Bloques.SetID(Índice_X, 1, Índice_Z - 1, 70); // Stone pressure plate
                                                Bloques.SetData(Índice_X, 1, Índice_Z - 1, 0); // North
                                            }
                                            Índice = (((Índice_Z + 1) * Ancho) + (Índice_X)) * 4;
                                            if (Matriz_Bytes[Índice + 3] > 0 && ((Matriz_Bytes[Índice + 2] + Matriz_Bytes[Índice + 1] + Matriz_Bytes[Índice]) / 3) < 128) // Not fully transparent
                                            {
                                                Bloques.SetID(Índice_X, 1, Índice_Z + 1, 70); // Stone pressure plate
                                                Bloques.SetData(Índice_X, 1, Índice_Z + 1, 0); // South
                                            }
                                            Índice = (((Índice_Z) * Ancho) + (Índice_X - 1)) * 4;
                                            if (Matriz_Bytes[Índice + 3] > 0 && ((Matriz_Bytes[Índice + 2] + Matriz_Bytes[Índice + 1] + Matriz_Bytes[Índice]) / 3) < 128) // Not fully transparent
                                            {
                                                Bloques.SetID(Índice_X - 1, 1, Índice_Z, 70); // Stone pressure plate
                                                Bloques.SetData(Índice_X - 1, 1, Índice_Z, 0); // West
                                            }
                                            Índice = (((Índice_Z) * Ancho) + (Índice_X + 1)) * 4;
                                            if (Matriz_Bytes[Índice + 3] > 0 && ((Matriz_Bytes[Índice + 2] + Matriz_Bytes[Índice + 1] + Matriz_Bytes[Índice]) / 3) < 128) // Not fully transparent
                                            {
                                                Bloques.SetID(Índice_X + 1, 1, Índice_Z, 70); // Stone pressure plate
                                                Bloques.SetData(Índice_X + 1, 1, Índice_Z, 0); // East
                                            }
                                        }
                                    }*/ // Allow this to light the path when walking (experimental)
                                }
                            }
                        }
                        else if (Variable_Estructura == Estructuras.Painted_structure)
                        {
                            if (Imagen_Estructura_Pintada != null)
                            {
                                int Ancho = Imagen_Estructura_Pintada.Width;
                                int Alto = Imagen_Estructura_Pintada.Height; // Can't figure out the real height, so let the user choose it manually first
                                if (Alto % Variable_Diámetro != 0) // Warn the user if the selected height (diameter) might be wrong
                                {
                                    if (MessageBox.Show(this, "The height of the dropped image isn't a multiple of the selected diameter.\r\nDo you want to continue anyway?", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                                    {
                                        Chunks = null; // Cancel
                                        Bloques = null;
                                        Mundo = null;
                                    }
                                }
                                int Ancho_16 = Ancho + 16;
                                Diámetro_16 = Variable_Diámetro + 16;
                                Bloques_Ancho = 0;
                                Bloques_Alto = 0;
                                for (int Índice_Z = -16, Chunk_Z = -1; Índice_Z < Diámetro_16; Índice_Z += 16, Chunk_Z++, Bloques_Alto += 16)
                                {
                                    Bloques_Ancho = 0;
                                    for (int Índice_X = -16, Chunk_X = -1; Índice_X < Ancho_16; Índice_X += 16, Chunk_X++, Bloques_Ancho += 16)
                                    {
                                        ChunkRef Chunk = Chunks.CreateChunk(Chunk_X, Chunk_Z);
                                        Chunk.IsLightPopulated = true; // For 1.13+ conversion support.
                                        Chunk.IsTerrainPopulated = true;
                                        Chunk.Blocks.AutoLight = false;
                                        if (Índice_X > -1 && Índice_Z > -1 && Índice_X < Ancho && Índice_Z < Variable_Diámetro)
                                        {
                                            for (int Z = 0; Z < 16; Z++)
                                            {
                                                for (int X = 0; X < 16; X++)
                                                {
                                                    Chunk.Blocks.SetID(X, 0, Z, (int)BlockType.BEDROCK); // Structure floor
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
                                Rectangle Rectángulo = new Rectangle(0, 0, Ancho, Variable_Diámetro);
                                int Alto_Y = Alto / Variable_Diámetro;
                                for (int Índice_Y = 0; Índice_Y < Alto_Y && Índice_Y < 256; Índice_Y++, Rectángulo.Y += Variable_Diámetro)
                                {
                                    if (Rectángulo.Y + Rectángulo.Height < Alto)
                                    {
                                        Bitmap Imagen = Imagen_Estructura_Pintada.Clone(Rectángulo, Imagen_Estructura_Pintada.PixelFormat);
                                        BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Variable_Diámetro), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                                        int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                                        int Bytes_Diferencia = Ancho_Stride - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                                        byte[] Matriz_Bytes = new byte[Ancho_Stride * Variable_Diámetro];
                                        Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                                        Imagen.UnlockBits(Bitmap_Data);
                                        Bitmap_Data = null;
                                        for (int Índice_Z = 0, Índice = 0; Índice_Z < Variable_Diámetro; Índice_Z++, Índice += Bytes_Diferencia)
                                        {
                                            for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += 4)
                                            {
                                                int Código_Hash = Color.FromArgb(Matriz_Bytes[Índice + 3], Matriz_Bytes[Índice + 2], Matriz_Bytes[Índice + 1], Matriz_Bytes[Índice]).GetHashCode();
                                                foreach (KeyValuePair<short, Color> Entrada in Minecraft.Diccionario_Bloques_Índices_Colores)
                                                {
                                                    if (Entrada.Value.GetHashCode() == Código_Hash) // It's a block with the same color
                                                    {
                                                        short ID_Data = Entrada.Key;
                                                        foreach (KeyValuePair<short, short> Subentrada in Minecraft.Diccionario_Bloques_Índices_1_12_2_a_Índices_1_13)
                                                        {
                                                            if (Subentrada.Value == ID_Data) // found the MC 1.12.2- ID and Data values of the MC 1.13+ block
                                                            {
                                                                ID_Data = Subentrada.Key;
                                                                break;
                                                            }
                                                        }
                                                        ID = Minecraft.Obtener_Valores_ID_Data(ID_Data, out Data); // Try to find the MC 1.12.2- equivalent values of the current block
                                                        Bloques.SetID(Índice_X, Índice_Y, Índice_Z, ID);
                                                        Bloques.SetData(Índice_X, Índice_Y, Índice_Z, Data);
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        Matriz_Bytes = null;
                                        Imagen.Dispose();
                                        Imagen = null;
                                    }
                                }
                            }
                            else // Can't export without a loaded image, cancel everything and warn the user
                            {
                                MessageBox.Show(this, "To generate a custom structure based on a painted one, drop first on the window any image that contains a structure.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Chunks = null;
                                Bloques = null;
                                Mundo = null;
                            }
                        }
                        else if (Variable_Estructura == Estructuras.Floating_city)
                        {
                            // ...

                            /*Diámetro_16 = Variable_Diámetro + 16;
                            Bloques_Ancho = 0;
                            Bloques_Alto = 0;
                            for (int Índice_Z = -16, Chunk_Z = -1; Índice_Z < Diámetro_16; Índice_Z += 16, Chunk_Z++, Bloques_Alto += 16)
                            {
                                Bloques_Ancho = 0;
                                for (int Índice_X = -16, Chunk_X = -1; Índice_X < Diámetro_16; Índice_X += 16, Chunk_X++, Bloques_Ancho += 16)
                                {
                                    ChunkRef Chunk = Chunks.CreateChunk(Chunk_X, Chunk_Z);
                                    Chunk.IsLightPopulated = true; // For 1.13+ conversion support.
                                    Chunk.IsTerrainPopulated = true;
                                    Chunk.Blocks.AutoLight = false;
                                    if (Índice_X > -1 && Índice_Z > -1 && Índice_X < Variable_Diámetro && Índice_Z < Variable_Diámetro)
                                    {
                                        for (int Z = 0; Z < 16; Z++)
                                        {
                                            for (int X = 0; X < 16; X++)
                                            {
                                                Chunk.Blocks.SetID(X, 0, Z, (int)BlockType.BEDROCK); // Structure floor
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
                            }*/
                        }
                    }
                    for (int Índice_Z = -16, Chunk_Z = -1; Índice_Z < Diámetro_16; Índice_Z += 16, Chunk_Z++)
                    {
                        for (int Índice_X = -16, Chunk_X = -1; Índice_X < Diámetro_16; Índice_X += 16, Chunk_X++)
                        {
                            IChunk Chunk = Chunks.GetChunk(Chunk_X, Chunk_Z);
                            if (Chunk != null)
                            {
                                Chunk.Blocks.RebuildHeightMap();
                                Chunk.Blocks.RebuildBlockLight();
                                Chunk.Blocks.RebuildSkyLight();
                            }
                            //else MessageBox.Show("Chunk == null?");
                        }
                    }
                    Chunks.Save();
                    Mundo.Save();
                    Chunks = null;
                    Bloques = null;
                    Mundo = null;
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            finally
            {
                this.Text = Texto_Título_Actual + "]";
                this.Cursor = Cursors.Default;
            }
        }

        internal void Registro_Cargar_Opciones()
        {
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Custom Structures Generator");

                // Toolbar options:
                try { Variable_Estructura_3D = bool.Parse((string)Clave.GetValue("3D_Structure", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Estructura_3D = true; }
                try { Variable_Estructura = (Estructuras)Clave.GetValue("Structure", 0); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Estructura = 0; }
                try { Variable_Rellenar = bool.Parse((string)Clave.GetValue("Fill", bool.FalseString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Rellenar = false; }
                try { Variable_Lados = (int)Clave.GetValue("Sides", 6); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Lados = 6m; }
                try { Variable_Forzar_Simetría = (CheckState)Clave.GetValue("Force_Symmetry", (int)CheckState.Checked); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Forzar_Simetría = CheckState.Checked; }
                try { Variable_Diámetro = (int)Clave.GetValue("Diameter", 64); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Diámetro = 64; }
                try { Variable_Rotación = (int)Clave.GetValue("Rotation", 0); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Rotación = 0m; }
                try { Variable_Bloque = (string)Clave.GetValue("Block", "minecraft:sea_lantern"); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Bloque = "minecraft:sea_lantern"; }
                try { Variable_Bloque_Interior = (string)Clave.GetValue("Interior_Block", "minecraft:air"); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Bloque_Interior = "minecraft:air"; }
                try { Variable_Autozoom = bool.Parse((string)Clave.GetValue("Autozoom", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Autozoom = true; }

                // Context menu options:
                try { Construir_Paredes_Cristal = bool.Parse((string)Clave.GetValue("Build_Glass_Walls", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Construir_Paredes_Cristal = true; }
                try { Variable_Gamerule_DoFireTick = bool.Parse((string)Clave.GetValue("Gamerule_DoFireTick", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Gamerule_DoFireTick = true; }

                // Correct any bad value after loading:
                if ((int)Variable_Estructura <= 0 || (int)Variable_Estructura > (int)Estructuras.Total) Variable_Estructura = 0;
                if (Variable_Lados < 1m || Variable_Lados > 360m) Variable_Lados = 6m;
                if ((int)Variable_Forzar_Simetría < 0 || (int)Variable_Forzar_Simetría > 2) Variable_Forzar_Simetría = CheckState.Checked;
                if (Variable_Diámetro < 1 || Variable_Diámetro > 4096) Variable_Diámetro = 64;
                if (Variable_Rotación < 0m || Variable_Rotación > 360m) Variable_Rotación = 0m;
                if (string.IsNullOrEmpty(Variable_Bloque) || !Variable_Bloque.StartsWith("minecraft:") || !ComboBox_Bloque.Items.Contains(Variable_Bloque.Substring(10, 1).ToUpperInvariant() + Variable_Bloque.Substring(11).Replace('_', ' '))) Variable_Bloque = "minecraft:diamond_ore";
                if (string.IsNullOrEmpty(Variable_Bloque_Interior) || !Variable_Bloque_Interior.StartsWith("minecraft:") || !ComboBox_Bloque_Interior.Items.Contains(Variable_Bloque_Interior.Substring(10, 1).ToUpperInvariant() + Variable_Bloque_Interior.Substring(11).Replace('_', ' '))) Variable_Bloque_Interior = "minecraft:air";

                // Apply all the loaded values:
                CheckBox_Estructura_3D.Checked = Variable_Estructura_3D;
                ComboBox_Estructura.SelectedIndex = (int)Variable_Estructura;
                CheckBox_Estructura_Rellenar.Checked = Variable_Rellenar;
                Numérico_Lados.Value = Variable_Lados;
                CheckBox_Estructura_Forzar_Simetría.CheckState = Variable_Forzar_Simetría;
                Numérico_Diámetro.Value = Variable_Diámetro;
                Numérico_Rotación.Value = Variable_Rotación;
                ComboBox_Bloque.Text = Variable_Bloque.Substring(10, 1).ToUpperInvariant() + Variable_Bloque.Substring(11).Replace('_', ' ');
                ComboBox_Bloque_Interior.Text = Variable_Bloque_Interior.Substring(10, 1).ToUpperInvariant() + Variable_Bloque_Interior.Substring(11).Replace('_', ' ');
                CheckBox_Estructura_Autozoom.Checked = Variable_Autozoom;

                Menú_Contextual_Construir_Paredes_Cristal.Checked = Construir_Paredes_Cristal;
                Menú_Contextual_Gamerule_DoFireTick.Checked = Variable_Gamerule_DoFireTick;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal void Registro_Guardar_Opciones()
        {
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Custom Structures Generator");
                string[] Matriz_Nombres = Clave.GetValueNames();
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                    {
                        Clave.DeleteValue(Matriz_Nombres[Índice]);
                    }
                }
                Matriz_Nombres = null;

                // Toolbar options:
                try { Clave.SetValue("3D_Structure", Variable_Estructura_3D.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                try { Clave.SetValue("Structure", (int)Variable_Estructura, RegistryValueKind.DWord); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                try { Clave.SetValue("Fill", Variable_Rellenar.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                try { Clave.SetValue("Sides", (int)Variable_Lados, RegistryValueKind.DWord); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                try { Clave.SetValue("Force_Symmetry", (int)Variable_Forzar_Simetría, RegistryValueKind.DWord); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                try { Clave.SetValue("Diameter", Variable_Diámetro, RegistryValueKind.DWord); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                try { Clave.SetValue("Rotation", (int)Variable_Rotación, RegistryValueKind.DWord); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                try { Clave.SetValue("Block", Variable_Bloque, RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                try { Clave.SetValue("Interior_Block", Variable_Bloque_Interior, RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                try { Clave.SetValue("Autozoom", Variable_Autozoom.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }

                // Context menu options:
                try { Clave.SetValue("Build_Glass_Walls", Construir_Paredes_Cristal.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                try { Clave.SetValue("Gamerule_DoFireTick", Variable_Gamerule_DoFireTick.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }

                // Context menu options:
                /*try { Clave.SetValue("Dither_Method", Variable_Estructura_3D, RegistryValueKind.DWord); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                try { Clave.SetValue("Limit_Height_Force_Height", Limitar_Altura_Forzar_Altura.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                try { Clave.SetValue("Limit_Height", Limitar_Altura, RegistryValueKind.DWord); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal void Registro_Restablecer_Opciones()
        {
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Custom Structures Generator");
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

        private void Menú_Contextual_Preconfiguraciones_Predeterminado_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                CheckBox_Estructura_3D.Checked = true;
                ComboBox_Estructura.SelectedIndex = (int)Estructuras.Pyramid;
                CheckBox_Estructura_Rellenar.Checked = false;
                CheckBox_Estructura_Forzar_Simetría.Checked = true;
                Numérico_Rotación.Value = 0m;
                ComboBox_Bloque.Text = "minecraft:sea_lantern".Substring(10, 1).ToUpperInvariant() + "minecraft:sea_lantern".Substring(11).Replace('_', ' ');
                ComboBox_Bloque_Interior.Text = "minecraft:air".Substring(10, 1).ToUpperInvariant() + "minecraft:air".Substring(11).Replace('_', ' ');
                Menú_Contextual_Gamerule_DoFireTick.Checked = false;
                Ocupado = false;
                Generar_Estructura_Masiva();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Preconfiguraciones_Bomba_Lava_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                CheckBox_Estructura_3D.Checked = true;
                ComboBox_Estructura.SelectedIndex = (int)Estructuras.Pyramid;
                CheckBox_Estructura_Rellenar.Checked = false;
                CheckBox_Estructura_Forzar_Simetría.Checked = true;
                Numérico_Rotación.Value = 0m;
                ComboBox_Bloque.Text = "minecraft:tnt".Substring(10, 1).ToUpperInvariant() + "minecraft:tnt".Substring(11).Replace('_', ' ');
                ComboBox_Bloque_Interior.Text = "minecraft:lava".Substring(10, 1).ToUpperInvariant() + "minecraft:lava".Substring(11).Replace('_', ' ');
                Menú_Contextual_Gamerule_DoFireTick.Checked = true;
                Ocupado = false;
                Generar_Estructura_Masiva();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Preconfiguraciones_Misil_Nuclear_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                CheckBox_Estructura_3D.Checked = true;
                ComboBox_Estructura.SelectedIndex = (int)Estructuras.Pyramid;
                CheckBox_Estructura_Rellenar.Checked = false;
                CheckBox_Estructura_Forzar_Simetría.Checked = true;
                Numérico_Rotación.Value = 0m;
                ComboBox_Bloque.Text = "minecraft:fire".Substring(10, 1).ToUpperInvariant() + "minecraft:fire".Substring(11).Replace('_', ' ');
                ComboBox_Bloque_Interior.Text = "minecraft:tnt".Substring(10, 1).ToUpperInvariant() + "minecraft:tnt".Substring(11).Replace('_', ' ');
                Menú_Contextual_Gamerule_DoFireTick.Checked = true;
                Ocupado = false;
                Generar_Estructura_Masiva();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Preconfiguraciones_Madera_Ardiente_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                CheckBox_Estructura_3D.Checked = true;
                ComboBox_Estructura.SelectedIndex = (int)Estructuras.Pyramid;
                CheckBox_Estructura_Rellenar.Checked = false;
                CheckBox_Estructura_Forzar_Simetría.Checked = true;
                Numérico_Rotación.Value = 0m;
                ComboBox_Bloque.Text = "minecraft:fire".Substring(10, 1).ToUpperInvariant() + "minecraft:fire".Substring(11).Replace('_', ' ');
                ComboBox_Bloque_Interior.Text = "minecraft:oak_log".Substring(10, 1).ToUpperInvariant() + "minecraft:oak_log".Substring(11).Replace('_', ' ');
                Menú_Contextual_Gamerule_DoFireTick.Checked = false;
                Ocupado = false;
                Generar_Estructura_Masiva();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Preconfiguraciones_Hojas_Ardientes_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                CheckBox_Estructura_3D.Checked = true;
                ComboBox_Estructura.SelectedIndex = (int)Estructuras.Pyramid;
                CheckBox_Estructura_Rellenar.Checked = false;
                CheckBox_Estructura_Forzar_Simetría.Checked = true;
                Numérico_Rotación.Value = 0m;
                ComboBox_Bloque.Text = "minecraft:fire".Substring(10, 1).ToUpperInvariant() + "minecraft:fire".Substring(11).Replace('_', ' ');
                ComboBox_Bloque_Interior.Text = "minecraft:oak_leaves".Substring(10, 1).ToUpperInvariant() + "minecraft:oak_leaves".Substring(11).Replace('_', ' ');
                Menú_Contextual_Gamerule_DoFireTick.Checked = false;
                Ocupado = false;
                Generar_Estructura_Masiva();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Preconfiguraciones_Railes_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                CheckBox_Estructura_3D.Checked = true;
                ComboBox_Estructura.SelectedIndex = (int)Estructuras.Pyramid;
                CheckBox_Estructura_Rellenar.Checked = false;
                CheckBox_Estructura_Forzar_Simetría.Checked = true;
                Numérico_Rotación.Value = 0m;
                ComboBox_Bloque.Text = "minecraft:powered_rail".Substring(10, 1).ToUpperInvariant() + "minecraft:powered_rail".Substring(11).Replace('_', ' ');
                ComboBox_Bloque_Interior.Text = "minecraft:rail".Substring(10, 1).ToUpperInvariant() + "minecraft:rail".Substring(11).Replace('_', ' ');
                Menú_Contextual_Gamerule_DoFireTick.Checked = false;
                Ocupado = false;
                Generar_Estructura_Masiva();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Preconfiguraciones_Yunques_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                CheckBox_Estructura_3D.Checked = true;
                ComboBox_Estructura.SelectedIndex = (int)Estructuras.Pyramid;
                CheckBox_Estructura_Rellenar.Checked = false;
                CheckBox_Estructura_Forzar_Simetría.Checked = true;
                Numérico_Rotación.Value = 0m;
                ComboBox_Bloque.Text = "minecraft:anvil".Substring(10, 1).ToUpperInvariant() + "minecraft:anvil".Substring(11).Replace('_', ' ');
                ComboBox_Bloque_Interior.Text = "minecraft:air".Substring(10, 1).ToUpperInvariant() + "minecraft:air".Substring(11).Replace('_', ' ');
                Menú_Contextual_Gamerule_DoFireTick.Checked = false;
                Ocupado = false;
                Generar_Estructura_Masiva();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Preconfiguraciones_Arena_Flotante_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                CheckBox_Estructura_3D.Checked = true;
                ComboBox_Estructura.SelectedIndex = (int)Estructuras.Pyramid;
                CheckBox_Estructura_Rellenar.Checked = false;
                CheckBox_Estructura_Forzar_Simetría.Checked = true;
                Numérico_Rotación.Value = 0m;
                ComboBox_Bloque.Text = "minecraft:sand".Substring(10, 1).ToUpperInvariant() + "minecraft:sand".Substring(11).Replace('_', ' ');
                ComboBox_Bloque_Interior.Text = "minecraft:air".Substring(10, 1).ToUpperInvariant() + "minecraft:air".Substring(11).Replace('_', ' ');
                Menú_Contextual_Gamerule_DoFireTick.Checked = false;
                Ocupado = false;
                Generar_Estructura_Masiva();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Exportar_Mundo_Click(object sender, EventArgs e)
        {
            try
            {
                Exportar_Mundo_Minecraft();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Gamerule_DoFireTick_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Gamerule_DoFireTick = Menú_Contextual_Gamerule_DoFireTick.Checked;
                Registro_Guardar_Opciones();
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

        private void Menú_Contextual_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Generar_Estructura_Masiva();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Picture_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    Generar_Estructura_Masiva();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Preconfiguraciones_Laberinto_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                CheckBox_Estructura_3D.Checked = true;
                ComboBox_Estructura.SelectedIndex = (int)Estructuras.Labyrinth;
                CheckBox_Estructura_Rellenar.Checked = true;
                Numérico_Lados.Value = 3m;
                Numérico_Rotación.Value = 30m;
                ComboBox_Bloque.Text = "minecraft:bedrock".Substring(10, 1).ToUpperInvariant() + "minecraft:bedrock".Substring(11).Replace('_', ' ');
                ComboBox_Bloque_Interior.Text = "minecraft:air".Substring(10, 1).ToUpperInvariant() + "minecraft:air".Substring(11).Replace('_', ' ');
                Menú_Contextual_Gamerule_DoFireTick.Checked = true;
                Ocupado = false;
                Generar_Estructura_Masiva();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Preconfiguraciones_Laberinto_Trampa_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                CheckBox_Estructura_3D.Checked = true;
                ComboBox_Estructura.SelectedIndex = (int)Estructuras.Trapped_labyrinth;
                CheckBox_Estructura_Rellenar.Checked = true;
                Numérico_Lados.Value = 3m;
                Numérico_Rotación.Value = 30m;
                ComboBox_Bloque.Text = "minecraft:bedrock".Substring(10, 1).ToUpperInvariant() + "minecraft:bedrock".Substring(11).Replace('_', ' ');
                ComboBox_Bloque_Interior.Text = "minecraft:air".Substring(10, 1).ToUpperInvariant() + "minecraft:air".Substring(11).Replace('_', ' ');
                Menú_Contextual_Gamerule_DoFireTick.Checked = true;
                Ocupado = false;
                Generar_Estructura_Masiva();
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
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Estructuras_Personalizadas);
                    Picture.Image.Save(Program.Ruta_Guardado_Imágenes_Estructuras_Personalizadas + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " Custom structures " + Variable_Estructura.ToString().ToLowerInvariant().Replace('_', ' ') + " " + Variable_Lados.ToString() + ".png", ImageFormat.Png);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_Lados_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    Numérico_Lados.Value = 1;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_Diámetro_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    Numérico_Diámetro.Value = 64;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_Rotación_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    Numérico_Rotación.Value = 0;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.Custom_structure_generator;
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
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Estructuras_Personalizadas);
                Program.Ejecutar_Ruta(Program.Ruta_Guardado_Imágenes_Estructuras_Personalizadas, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void CheckBox_Estructura_Forzar_Simetría_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Forzar_Simetría = CheckBox_Estructura_Forzar_Simetría.CheckState;
                Registro_Guardar_Opciones();
                Generar_Estructura_Masiva();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }
    }
}
