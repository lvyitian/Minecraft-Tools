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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Luna_Virtual : Form
    {
        public Ventana_Luna_Virtual()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Virtual Moon by Jupisoft for " + Program.Texto_Usuario;
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

        //internal static Pen Lápiz = new Pen(Color.FromArgb(255, 0, 0, 0));
        internal static SolidBrush Pincel = new SolidBrush(Color.FromArgb(128, 4, 0, 0));
        internal static int Modo = 0;
        internal static DateTime Fecha = DateTime.MinValue;
        internal static int Eclipse = -1;
        internal static int Fase = -1;
        internal Graphics Pintar = null;
        internal int Eclipse_Anterior = -1;
        internal int Fase_Anterior = -1;
        internal int Resto_Anterior = -1;

        internal static string Traducir_Fase(int Índice)
        {
            try
            {
                Índice = Índice % 4;
                if (Índice == 0) return "First quarter";
                else if (Índice == 1) return "Full moon";
                else if (Índice == 2) return "Last quarter";
                else if (Índice == 3) return "New moon";
            }
            catch { }
            return "Unknown";
        }

        private void Ventana_Luna_Virtual_Load(object sender, EventArgs e)
        {
            try
            {
                //Picture.BackColor = Color.Red;
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                //this.WindowState = FormWindowState.Maximized;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
                Picture.Image = new Bitmap(640, 640, PixelFormat.Format32bppArgb);
                Pintar = Graphics.FromImage(Picture.Image);
                Pintar.CompositingMode = CompositingMode.SourceCopy;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.HighQuality;
                Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                //Pintar.Clear(Color.Black);
                for (int Índice = 0; Índice < Luna_Virtual.Matriz_Eclipses_Lunares.Length - 1; Índice++) Combo_Eclipses.Items.Add(Program.Traducir_Fecha(Luna_Virtual.Matriz_Eclipses_Lunares[Índice].Key, true, false, false) + ", " + Luna_Virtual.Matriz_Eclipses_Lunares[Índice].Value + "");
                for (int Índice = 0; Índice < Luna_Virtual.Matriz_Fases_Lunares.Length - 1; Índice++) Combo_Fases.Items.Add(Program.Traducir_Fecha(Luna_Virtual.Matriz_Fases_Lunares[Índice], true, false, false) + ", " + Traducir_Fase(Índice) + "");
                Combo_Modo.Items[2] += " - [" + Program.Traducir_Número((Luna_Virtual.Matriz_Eclipses_Lunares.Length - 1).ToString()) + (Luna_Virtual.Matriz_Eclipses_Lunares.Length == 1 ? " Eclipse]" : " Eclipses]");
                Combo_Modo.Items[3] += " - [" + Program.Traducir_Número((Luna_Virtual.Matriz_Fases_Lunares.Length - 1).ToString()) + (Luna_Virtual.Matriz_Fases_Lunares.Length == 1 ? " Fase]" : " Fases]");
                DateTimePicker_Fecha.Value = Fecha >= DateTimePicker_Fecha.MinDate && Fecha <= DateTimePicker_Fecha.MaxDate ? Fecha : DateTime.Now.Date;
                Combo_Modo.SelectedIndex = Modo;
                Combo_Eclipses.SelectedIndex = Eclipse;
                Combo_Fases.SelectedIndex = Fase;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Luna_Virtual_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Luna_Virtual_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Luna_Virtual_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Luna_Virtual_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Luna_Virtual_KeyDown(object sender, KeyEventArgs e)
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

        private void Picture_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Right)
                {
                    this.Close();
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

                Actualizar_Luna_Virtual();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Actualizar_Luna_Virtual()
        {
            try
            {
                if (!Ocupado)
                {
                    try
                    {
                        Ocupado = true;
                        //DateTime Fecha_Actual = Modo == 0 ? DateTime.Now : Modo == 1 ? DateTimePicker_Fecha.Value : Modo == 2 ? Luna_Virtual.Matriz_Eclipses_Lunares[Combo_Eclipses.SelectedIndex].Key : Luna_Virtual.Matriz_Fases_Lunares[Combo_Fases.SelectedIndex];
                        DateTime Fecha_Actual = DateTime.Now;
                        int Eclipse_Actual = -1;
                        int Fase_Actual = -1;
                        if (Modo != 3)
                        {
                            if (Modo == 0) Text_Fecha.Text = Program.Traducir_Fecha(Fecha_Actual, true, true, true);
                            if (Fase_Anterior > -1 && Luna_Virtual.Matriz_Fases_Lunares[Fase_Anterior] <= Fecha_Actual && Luna_Virtual.Matriz_Fases_Lunares[Fase_Anterior + 1] > Fecha_Actual) Fase_Actual = Fase_Anterior;
                            else
                            {
                                for (int Índice = 0; Índice < Luna_Virtual.Matriz_Fases_Lunares.Length - 1; Índice++)
                                {
                                    if (Luna_Virtual.Matriz_Fases_Lunares[Índice] <= Fecha_Actual && Luna_Virtual.Matriz_Fases_Lunares[Índice + 1] > Fecha_Actual)
                                    {
                                        Fase_Actual = Índice;
                                        Fase_Anterior = Índice;
                                        break;
                                    }
                                }
                            }
                            if (Combo_Fases.SelectedIndex != Fase_Actual) Combo_Fases.SelectedIndex = Fase_Actual;
                        }
                        else if (Modo == 3)
                        {
                            Fase_Anterior = -1;
                            Fase_Actual = Combo_Fases.SelectedIndex;
                        }
                        if (Modo == 2)
                        {
                            Eclipse_Anterior = -1;
                            Eclipse_Actual = Combo_Eclipses.SelectedIndex;
                        }
                        if (Fase_Actual > -1)
                        {
                            Pintar.Clear(Color.Transparent); // Reset.
                            double X = 0d;
                            int Resto = Fase_Actual % 4;
                            Barra_Progreso.Value = (int)Math.Round(((double)Fecha_Actual.Subtract(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual]).Ticks * 100d) / (double)Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual + 1].Subtract(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual]).Ticks, MidpointRounding.AwayFromZero);
                            if (Resto == 0)
                            {
                                this.Text = Texto_Título + " - [" + Program.Traducir_Fecha(Fecha_Actual, false, true, true) + ", Cuarto Creciente]";
                                if (Resto_Anterior != Resto)
                                {
                                    Resto_Anterior = Resto;
                                    Grupo_Fase_Actual.Text = "Current phase - [First quarter]";
                                    Grupo_Fase_Siguiente.Text = "Next phase - [Full moon]";
                                }
                                X += (320d - (((double)Fecha_Actual.Subtract(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual]).Ticks * 320d) / (double)Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual + 1].Subtract(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual]).Ticks)) - 640d;
                                Barra_Progreso_Total.Value = Barra_Progreso.Value + 100;
                            }
                            else if (Resto == 1)
                            {
                                this.Text = Texto_Título + " - [" + Program.Traducir_Fecha(Fecha_Actual, false, true, true) + ", Luna Llena]";
                                if (Resto_Anterior != Resto)
                                {
                                    Resto_Anterior = Resto;
                                    Grupo_Fase_Actual.Text = "Current phase: Full moon";
                                    Grupo_Fase_Siguiente.Text = "Next phase: Last quarter";
                                }
                                X += (320d - (((double)Fecha_Actual.Subtract(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual]).Ticks * 320d) / (double)Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual + 1].Subtract(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual]).Ticks)) + 320d;
                                Barra_Progreso_Total.Value = Barra_Progreso.Value + 200;
                            }
                            else if (Resto == 2)
                            {
                                this.Text = Texto_Título + " - [" + Program.Traducir_Fecha(Fecha_Actual, false, true, true) + ", Cuarto Menguante]";
                                if (Resto_Anterior != Resto)
                                {
                                    Resto_Anterior = Resto;
                                    Grupo_Fase_Actual.Text = "Current phase: Last quarter";
                                    Grupo_Fase_Siguiente.Text = "Next phase: New moon";
                                }
                                X += 320d - (((double)Fecha_Actual.Subtract(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual]).Ticks * 320d) / (double)Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual + 1].Subtract(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual]).Ticks);
                                Barra_Progreso_Total.Value = Barra_Progreso.Value + 300;
                            }
                            else if (Resto == 3)
                            {
                                this.Text = Texto_Título + " - [" + Program.Traducir_Fecha(Fecha_Actual, false, true, true) + ", Luna Nueva]";
                                if (Resto_Anterior != Resto)
                                {
                                    Resto_Anterior = Resto;
                                    Grupo_Fase_Actual.Text = "Current phase: New moon";
                                    Grupo_Fase_Siguiente.Text = "Next phase: First quarter";
                                }
                                X += (320d - (((double)Fecha_Actual.Subtract(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual]).Ticks * 320d) / (double)Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual + 1].Subtract(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual]).Ticks)) - 320d;
                                Barra_Progreso_Total.Value = Barra_Progreso.Value;
                            }
                            else MessageBox.Show(this, Fase_Actual.ToString());
                            Text_Fase_Actual_Fecha.Text = Program.Traducir_Fecha(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual], true, true, true);
                            Text_Fase_Actual_Intervalo.Text = Program.Traducir_Intervalo(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual].Subtract(Fecha_Actual));
                            Text_Fase_Siguiente_Fecha.Text = Program.Traducir_Fecha(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual + 1], true, true, true);
                            Text_Fase_Siguiente_Intervalo.Text = Program.Traducir_Intervalo(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual + 1].Subtract(Fecha_Actual));
                            Text_Fase_Actual_Porcentaje.Text = (100d - Math.Round(((double)Fecha_Actual.Subtract(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual]).Ticks * 100d) / (double)Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual + 1].Subtract(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual]).Ticks, 10, MidpointRounding.AwayFromZero)).ToString();
                            Text_Fase_Siguiente_Porcentaje.Text = Math.Round(((double)Fecha_Actual.Subtract(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual]).Ticks * 100d) / (double)Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual + 1].Subtract(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual]).Ticks, 10, MidpointRounding.AwayFromZero).ToString();
                            //Pintar.FillEllipse(Pincel, (float)X, 3f, 250f, 250f);

                            // Luna = 250 x 250 Píx.

                            double Xpos, Ypos, Rpos;
                            double Xpos1, Xpos2;
                            double Phase = 0d;

                            if (Resto == 1) Phase = 0.00d;
                            else if (Resto == 2) Phase = 0.25d;
                            else if (Resto == 3) Phase = 0.50d;
                            else if (Resto == 0) Phase = 0.75d;

                            //Phase = Program.Rand.NextDouble();

                            //Phase += (Fecha_Actual.Subtract(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual]).Ticks / Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual + 1].Subtract(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual]).Ticks) / 4d;
                            double P = ((double)Fecha_Actual.Subtract(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual]).Ticks / (double)Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual + 1].Subtract(Luna_Virtual.Matriz_Fases_Lunares[Fase_Actual]).Ticks) / 4d;
                            //MessageBox.Show(P.ToString());
                            Phase += P;

                            //if (P < 0d || P > 0.25d) MessageBox.Show(P.ToString());

                            //this.Text = Phase.ToString() + " / " + P.ToString();

                            List<PointF> Lista_Posiciones_Izquierda = new List<PointF>();
                            List<PointF> Lista_Posiciones_Derecha = new List<PointF>();
                            List<PointF> Lista_Posiciones_Izquierda_Superior = new List<PointF>();
                            List<PointF> Lista_Posiciones_Derecha_Superior = new List<PointF>();

                            for (Ypos = 0; Ypos <= 320; Ypos++)
                            {
                                Xpos = (double)(Math.Sqrt(320d * 320d - Ypos * Ypos));
                                // Draw darkness part of the moon
                                PointF pB1 = new PointF((float)(320 - Xpos), (float)(Ypos + 320));
                                PointF pB2 = new PointF((float)(Xpos + 320), (float)(Ypos + 320));
                                //Point pB3 = new Point(3 + (125 - Xpos), 3 + (125 - Ypos));
                                //Point pB4 = new Point(3 + (Xpos + 125), 3 + (125 - Ypos));
                                Rpos = 2 * Xpos;
                                if (Phase < 0.5d) // Menguante y Nueva, oscura a la derecha
                                {
                                    Xpos1 = Xpos;
                                    Xpos2 = (double)(Rpos - 2d * Phase * Rpos - Xpos);
                                    pB1.X = (float)(Xpos2 + 320d);
                                }
                                else
                                {
                                    Xpos1 = -Xpos;
                                    Xpos2 = (double)(Xpos - 2 * Phase * Rpos + Rpos);
                                    pB2.X = (float)(Xpos2 + 320);
                                }
                                // Draw the lighted part of the moon
                                //PointF pW1 = new PointF((float)(Xpos1 + 320d), (float)(320d - Ypos));
                                //PointF pW2 = new PointF((float)(Xpos2 + 320d), (float)(320d - Ypos));
                                //Point pW3 = new Point(Xpos1 + 125, Ypos + 125);
                                //Point pW4 = new Point(Xpos2 + 125, Ypos + 125);

                                //pB1.X = pW1.X;
                                //pB2.X = pW2.X;

                                Lista_Posiciones_Izquierda.Add(pB1);
                                Lista_Posiciones_Derecha.Add(pB2);
                                //Pintar.DrawLine(Lápiz, pB1, pB2);
                                pB1.Y = (float)(320d - Ypos);
                                pB2.Y = pB1.Y;
                                Lista_Posiciones_Izquierda_Superior.Add(pB1);
                                Lista_Posiciones_Derecha_Superior.Add(pB2);
                                //Picture.Invalidate();
                                //Picture.Update();
                                //System.Threading.Thread.Sleep(15);
                                //Application.DoEvents();
                                //Pintar.DrawLine(Lápiz, pB1, pB2);
                                // TODO: improve the drawing in a single pass as a polygon or curve, too much CPU used.
                                // DONE: but still high use of CPU? Needs a better design...
                            }
                            Lista_Posiciones_Derecha_Superior.Reverse();
                            Lista_Posiciones_Izquierda.InsertRange(0, Lista_Posiciones_Izquierda_Superior);
                            Lista_Posiciones_Derecha.InsertRange(0, Lista_Posiciones_Derecha_Superior);

                            Lista_Posiciones_Derecha.Reverse();
                            Lista_Posiciones_Izquierda.AddRange(Lista_Posiciones_Derecha);
                            Pintar.FillPolygon(Pincel, Lista_Posiciones_Izquierda.ToArray());

                            Lista_Posiciones_Izquierda = null;
                            Lista_Posiciones_Derecha = null;
                            Lista_Posiciones_Izquierda_Superior = null;
                            Lista_Posiciones_Derecha_Superior = null;

                            /*Phase = 0d;
                            if (Resto < 2)
                            {

                            }
                            else
                            {

                            }*/
                        }
                        /*else
                        {
                            this.Text = "Luna Virtual de Jupisoft - [" + Program.Traducir_Fecha(Fecha_Actual, false, true, true) + ", Desconocida]";
                            if (Resto_Anterior > -1)
                            {
                                Resto_Anterior = -1;
                                Grupo_Fase_Actual.Text = "Fase Actual: Desconocida";
                                Grupo_Fase_Siguiente.Text = "Fase Siguiente: Desconocida";
                                Text_Fase_Actual_Fecha.Text = "??-??-????, ??:??";
                                Text_Fase_Actual_Intervalo.Text = "?:??:??:??.???";
                                Text_Fase_Actual_Porcentaje.Text = "?";
                                Text_Fase_Siguiente_Fecha.Text = "??-??-????, ??:??";
                                Text_Fase_Siguiente_Intervalo.Text = "?:??:??:??.???";
                                Text_Fase_Siguiente_Porcentaje.Text = "?";
                                Barra_Progreso.Value = 0;
                                Barra_Progreso_Total.Value = 0;
                                Pintar.Clear(Color.Black);
                            }
                        }*/
                        Picture.Invalidate();
                        Picture.Update();
                        /*if (Eclipse_Actual <= -1)
                        {
                            for (int Índice = 0; Índice < Luna_Virtual.Matriz_Eclipses_Lunares.Length - 1; Índice++)
                            {
                                if (Luna_Virtual.Matriz_Eclipses_Lunares[Índice].Key >= Fecha_Actual)
                                {
                                    Eclipse_Actual = Índice;
                                    break;
                                }
                            }
                        }
                        if (Combo_Eclipses.SelectedIndex != Eclipse_Actual) Combo_Eclipses.SelectedIndex = Eclipse_Actual;
                        if (Eclipse_Actual > -1)
                        {
                            if (Eclipse_Anterior != Eclipse_Actual)
                            {
                                Eclipse_Anterior = Eclipse_Actual;
                                Grupo_Eclipse_Siguiente.Text = "Próximo Eclipse - [Eclipse " + Luna_Virtual.Matriz_Eclipses_Lunares[Eclipse_Actual].Value + "]";
                            }
                            Text_Eclipse_Siguiente_Fecha.Text = Program.Traducir_Fecha(Luna_Virtual.Matriz_Eclipses_Lunares[Eclipse_Actual].Key, true, true, true);
                            Text_Eclipse_Siguiente_Tiempo.Text = Program.Traducir_Intervalo(Luna_Virtual.Matriz_Eclipses_Lunares[Eclipse_Actual].Key.Subtract(Fecha_Actual));
                        }
                        else
                        {
                            if (Eclipse_Anterior > -1)
                            {
                                Eclipse_Anterior = -1;
                                Grupo_Eclipse_Siguiente.Text = "Próximo Eclipse - [Desconocido]";
                            }
                            Text_Eclipse_Siguiente_Fecha.Text = "??-??-????, ??:??";
                            Text_Eclipse_Siguiente_Tiempo.Text = "?:??:??:??.???";
                        }*/
                        //Ocupado = false;
                    }
                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                    finally { Ocupado = false; }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
