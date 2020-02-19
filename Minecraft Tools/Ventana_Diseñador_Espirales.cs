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
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Diseñador_Espirales : Form
    {
        public Ventana_Diseñador_Espirales()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Spiral Designer by Jupisoft for " + Program.Texto_Usuario;
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

        private void Ventana_Plantilla_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
                Bitmap Imagen = Generar_Imagen_Espiral((int)NumericUpDown_Ancho.Value, (int)NumericUpDown_Alto.Value, (int)NumericUpDown_Escalones.Value, false, CheckBox_Simetría.CheckState);
                int Zoom;
                Picture.BackgroundImage = Imagen;
                Picture.Image = Program.Obtener_Imagen_Autozoom(Imagen, Picture.ClientSize.Width, Picture.ClientSize.Height, true, CheckState.Unchecked, out Zoom);
                this.Text = Texto_Título + " - [Automatic zoom: " + Program.Traducir_Número(Zoom) + "x]";
                Picture.Refresh();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                Bitmap Imagen = Generar_Imagen_Espiral((int)NumericUpDown_Ancho.Value, (int)NumericUpDown_Alto.Value, (int)NumericUpDown_Escalones.Value, false, CheckBox_Simetría.CheckState);
                int Zoom;
                Picture.BackgroundImage = Imagen;
                Picture.Image = Program.Obtener_Imagen_Autozoom(Imagen, Picture.ClientSize.Width, Picture.ClientSize.Height, true, CheckState.Unchecked, out Zoom);
                this.Text = Texto_Título + " - [Automatic zoom: " + Program.Traducir_Número(Zoom) + "x]";
                Picture.Refresh();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_KeyDown(object sender, KeyEventArgs e)
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
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Espirales);
                Program.Ejecutar_Ruta(Program.Ruta_Guardado_Imágenes_Espirales, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
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
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.BackgroundImage != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Espirales);
                    Picture.BackgroundImage.Save(Program.Ruta_Guardado_Imágenes_Espirales + "\\" + Program.Obtener_Nombre_Temporal() + ".png", ImageFormat.Png);
                    Program.Ejecutar_Ruta(Program.Ruta_Guardado_Imágenes_Espirales, ProcessWindowStyle.Maximized);
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

        /// <summary>
        /// Function designed to generate a spiral image represented as a partial circle with portions of different colors.
        /// Each of these colors should be a floor of slabs placed at different height.
        /// </summary>
        /// <param name="Ancho">The width of the image to generate.</param>
        /// <param name="Alto">The height of the image to generate.</param>
        /// <param name="Escalones">The number of "steps" in a full circle of the spiral.</param>
        /// <param name="Modo_3D">If it's true, use the 3D mode.</param>
        /// <param name="Reflejar">Use this to achieve a perfect 4 sides symmetry. Highly recommended.</param>
        /// <returns>Returns a new image with the desired spiral on it. Returns null on any error.</returns>
        internal Bitmap Generar_Imagen_Espiral(int Ancho, int Alto, int Escalones, bool Modo_3D, CheckState Reflejar)
        {
            try
            {
                //int Ancho = 64;
                //int Alto = 64;
                Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                Graphics Pintar = Graphics.FromImage(Imagen);
                Pintar.CompositingMode = CompositingMode.SourceCopy;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.None;
                Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                Pintar.Clear(Color.White);
                int Divisor = Escalones; // 90. // 120.
                double Ángulo = 360d / (double)Divisor;
                Color[] Matriz_Colores = new Color[4]
                {
                    Color.Black,
                    Color.Red,
                    Color.Lime,
                    Color.Blue
                };
                if (!Modo_3D)
                {
                    for (int Índice = 0, Matiz = 0; Índice < Divisor; Índice++, Matiz++)
                    {
                        SolidBrush Pincel = new SolidBrush(Matriz_Colores[Índice % 4]);
                        //int Valor = ((Índice % 4) * 64) + 63;
                        //SolidBrush Pincel = new SolidBrush(Color.FromArgb(255, Valor, Valor, Valor));
                        //SolidBrush Pincel = new SolidBrush(Program.Matriz_Colores_Arco_Iris_256[(Índice % 8) * 32]); // OK.
                        //SolidBrush Pincel = new SolidBrush(Matriz_Colores[Índice % Matriz_Colores.Length]);
                        //int Valor = (Índice % 8) * 32;
                        //SolidBrush Pincel = new SolidBrush(Color.FromArgb(255, Valor, Valor, Valor));
                        //Brush Pincel = Índice % 2 == 0 ? Brushes.Black : Brushes.Gray;
                        //SolidBrush Pincel = new SolidBrush(Program.Matriz_Colores_Grises_256[Program.Matriz_Bytes_Filtro_Invertir_Bits_Base_16[Matiz % 255]]);
                        //SolidBrush Pincel = new SolidBrush(Program.Matriz_Colores_Arco_Iris_256[Program.Matriz_Bytes_Filtro_Invertir_Bits_Base_2[Matiz % 255]]);
                        //int Valor = Índice + 16;
                        //while (Valor > 255) Valor -= 16;
                        //SolidBrush Pincel = new SolidBrush(Color.FromArgb(255, Valor, Valor, Valor));
                        //SolidBrush Pincel = new SolidBrush(Program.Obtener_Color_Puro_1530(Matiz));
                        //SolidBrush Pincel = new SolidBrush(Program.Matriz_Colores_Arco_Iris_256[Índice % 256]);
                        Pintar.FillPie(Pincel, 0, 0, Ancho, Alto, (float)(-90d + ((Ángulo * (double)Índice) - (Ángulo / 2d))), (float)Ángulo);
                        //Pincel.Dispose();
                        //Pincel = null;
                        //break;
                    }
                }
                else
                {
                    for (int Índice = 0, Matiz = 0; Índice < Divisor; Índice++, Matiz++)
                    {
                        int Valor = Índice % 255;
                        SolidBrush Pincel = new SolidBrush(Color.FromArgb(255, Valor, Valor, Valor));
                        Pintar.FillPie(Pincel, 0, 0, Ancho, Alto, (float)(-90d + ((Ángulo * (double)Índice) - (Ángulo / 2d))), (float)Ángulo);
                    }
                }
                double Porcentaje_Espacio = (int)NumericUpDown_Porcentaje_Espacio.Value;
                if (Porcentaje_Espacio > 0)
                {
                    double X = (Ancho * Porcentaje_Espacio) / 100d;
                    double Y = (Alto * Porcentaje_Espacio) / 100d;
                    double XX = (Ancho - X) / 2d;
                    double YY = (Alto - Y) / 2d;
                    Pintar.FillEllipse(Brushes.White, (float)XX, (float)YY, (float)X, (float)Y);
                }
                if (CheckBox_Borde.Checked)
                {
                    Pintar.DrawEllipse(Pens.Gray, 0, 0, Ancho, Alto);
                }
                Pintar.Dispose();
                Pintar = null;

                if (Reflejar != CheckState.Unchecked)
                {
                    Bitmap Imagen_Esquina = Imagen.Clone(new Rectangle(Ancho / 2, Alto / 2, Ancho / 2, Alto / 2), Imagen.PixelFormat);

                    if (Reflejar == CheckState.Indeterminate)
                    {
                        Imagen.Dispose();
                        Imagen = null;
                        return Imagen_Esquina;
                    }

                    Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.None;
                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                    Pintar.Clear(Color.White);

                    Pintar.DrawImage(Imagen_Esquina, new Rectangle(Ancho / 2, Alto / 2, Ancho / 2, Alto / 2), new Rectangle(0, 0, Ancho / 2, Alto / 2), GraphicsUnit.Pixel);

                    Imagen_Esquina.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    Pintar.DrawImage(Imagen_Esquina, new Rectangle(0, Alto / 2, Ancho / 2, Alto / 2), new Rectangle(0, 0, Ancho / 2, Alto / 2), GraphicsUnit.Pixel);

                    Imagen_Esquina.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    Pintar.DrawImage(Imagen_Esquina, new Rectangle(0, 0, Ancho / 2, Alto / 2), new Rectangle(0, 0, Ancho / 2, Alto / 2), GraphicsUnit.Pixel);

                    Imagen_Esquina.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    Pintar.DrawImage(Imagen_Esquina, new Rectangle(Ancho / 2, 0, Ancho / 2, Alto / 2), new Rectangle(0, 0, Ancho / 2, Alto / 2), GraphicsUnit.Pixel);

                    Pintar.Dispose();
                    Pintar = null;
                    Imagen_Esquina.Dispose();
                    Imagen_Esquina = null;
                }
                return Imagen;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        private void NumericUpDown_Ancho_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown_Ancho.Refresh();
                Bitmap Imagen = Generar_Imagen_Espiral((int)NumericUpDown_Ancho.Value, (int)NumericUpDown_Alto.Value, (int)NumericUpDown_Escalones.Value, false, CheckBox_Simetría.CheckState);
                int Zoom;
                Picture.BackgroundImage = Imagen;
                Picture.Image = Program.Obtener_Imagen_Autozoom(Imagen, Picture.ClientSize.Width, Picture.ClientSize.Height, true, CheckState.Unchecked, out Zoom);
                this.Text = Texto_Título + " - [Automatic zoom: " + Program.Traducir_Número(Zoom) + "x]";
                Picture.Refresh();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Alto_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown_Alto.Refresh();
                Bitmap Imagen = Generar_Imagen_Espiral((int)NumericUpDown_Ancho.Value, (int)NumericUpDown_Alto.Value, (int)NumericUpDown_Escalones.Value, false, CheckBox_Simetría.CheckState);
                int Zoom;
                Picture.BackgroundImage = Imagen;
                Picture.Image = Program.Obtener_Imagen_Autozoom(Imagen, Picture.ClientSize.Width, Picture.ClientSize.Height, true, CheckState.Unchecked, out Zoom);
                this.Text = Texto_Título + " - [Automatic zoom: " + Program.Traducir_Número(Zoom) + "x]";
                Picture.Refresh();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Escalones_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown_Escalones.Refresh();
                Bitmap Imagen = Generar_Imagen_Espiral((int)NumericUpDown_Ancho.Value, (int)NumericUpDown_Alto.Value, (int)NumericUpDown_Escalones.Value, false, CheckBox_Simetría.CheckState);
                int Zoom;
                Picture.BackgroundImage = Imagen;
                Picture.Image = Program.Obtener_Imagen_Autozoom(Imagen, Picture.ClientSize.Width, Picture.ClientSize.Height, true, CheckState.Unchecked, out Zoom);
                this.Text = Texto_Título + " - [Automatic zoom: " + Program.Traducir_Número(Zoom) + "x]";
                Picture.Refresh();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Simetría_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox_Simetría.Refresh();
                Bitmap Imagen = Generar_Imagen_Espiral((int)NumericUpDown_Ancho.Value, (int)NumericUpDown_Alto.Value, (int)NumericUpDown_Escalones.Value, false, CheckBox_Simetría.CheckState);
                int Zoom;
                Picture.BackgroundImage = Imagen;
                Picture.Image = Program.Obtener_Imagen_Autozoom(Imagen, Picture.ClientSize.Width, Picture.ClientSize.Height, true, CheckState.Unchecked, out Zoom);
                this.Text = Texto_Título + " - [Automatic zoom: " + Program.Traducir_Número(Zoom) + "x]";
                Picture.Refresh();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Porcentaje_Espacio_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown_Porcentaje_Espacio.Refresh();
                Bitmap Imagen = Generar_Imagen_Espiral((int)NumericUpDown_Ancho.Value, (int)NumericUpDown_Alto.Value, (int)NumericUpDown_Escalones.Value, false, CheckBox_Simetría.CheckState);
                int Zoom;
                Picture.BackgroundImage = Imagen;
                Picture.Image = Program.Obtener_Imagen_Autozoom(Imagen, Picture.ClientSize.Width, Picture.ClientSize.Height, true, CheckState.Unchecked, out Zoom);
                this.Text = Texto_Título + " - [Automatic zoom: " + Program.Traducir_Número(Zoom) + "x]";
                Picture.Refresh();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Borde_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox_Borde.Refresh();
                Bitmap Imagen = Generar_Imagen_Espiral((int)NumericUpDown_Ancho.Value, (int)NumericUpDown_Alto.Value, (int)NumericUpDown_Escalones.Value, false, CheckBox_Simetría.CheckState);
                int Zoom;
                Picture.BackgroundImage = Imagen;
                Picture.Image = Program.Obtener_Imagen_Autozoom(Imagen, Picture.ClientSize.Width, Picture.ClientSize.Height, true, CheckState.Unchecked, out Zoom);
                this.Text = Texto_Título + " - [Automatic zoom: " + Program.Traducir_Número(Zoom) + "x]";
                Picture.Refresh();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
