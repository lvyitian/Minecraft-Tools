using Microsoft.Win32;
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
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Reloj_Minecraft_Tiempo_Real : Form
    {
        public Ventana_Reloj_Minecraft_Tiempo_Real()
        {
            InitializeComponent();
        }

        internal static bool Variable_doDaylightCycle = true;
        internal static long Variable_Gametick = 0L;
        internal static long Variable_Tickspeed = 1L;
        internal static Climas Variable_Clima = Climas.Clear;
        internal static Climas Variable_Clima_Automático = Climas.Clear;
        internal static int Variable_Clima_Ticks = 0;
        internal static bool Variable_Dibujar_Cielo_Azul = true;
        internal static bool Variable_Dibujar_Tierra_Sol_Luna = true;
        internal static bool Variable_Dibujar_Efectos_Climáticos = true;
        internal static bool Variable_Rotar_Cielo_Azul = false;
        internal static bool Variable_Rotar_Tierra_Sol_Luna = false;
        internal static bool Variable_Rotar_Efectos_Climáticos = false;
        internal static bool Variable_Oscurecer_Cielo = true;
        internal static bool Variable_Reproducir_Sonidos_Efectos_Climáticos = true;

        internal SoundPlayer Reproductor = null;
        internal bool Pendiente_Generar_Rayo = false;
        internal double Pendiente_Generar_Rayo_Ángulo = -1d;

        internal readonly string Texto_Título = "Real Time Minecraft Clock by Jupisoft for " + Program.Texto_Usuario;
        internal bool Variable_Siempre_Visible = false;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;
        internal static Stopwatch Cronómetro_FPS = Stopwatch.StartNew();
        internal long Segundo_FPS_Anterior = 0L;
        internal long FPS_Temporal = 0L;
        internal long FPS_Real = 0L;
        internal bool Ocupado = false;

        internal Stopwatch Cronómetro_Gametick = Stopwatch.StartNew();
        //internal long Milisegundos_Anterior_Cronómetro_Gametick = 0L;
        internal long Milisegundos_Gametick_Anterior = -1;
        internal Color Color_ARGB_Anterior = Color.FromArgb(255, 0, 0, 0); // Use this in the future to avoid drawing again the sky color (unless the window changed it's size).

        private void Ventana_Reloj_Minecraft_Tiempo_Real_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
                if (!Variable_doDaylightCycle || Variable_Tickspeed == 0L) Actualizar_Ciclo_Día_Noche(Variable_Gametick);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Reloj_Minecraft_Tiempo_Real_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
                /*// Try to decipher the BGR(A) values from a Minecraft biome color. (It worked).
                int RGB = 329011;
                //RGB = Color.FromArgb(255, 0, 0, 255).ToArgb(); // OK
                int Rojo = (RGB >> 16) & 0xFF;
                int Verde = (RGB >> 8) & 0xFF;
                int Azul = RGB & 0xFF;
                Color Color_ARGB = Color.FromArgb(255, Rojo, Verde, Azul);
                this.Text = Texto_Título + " - [" + Color_ARGB.ToString() + "]";
                Picture.BackColor = Color_ARGB;*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Reloj_Minecraft_Tiempo_Real_FormClosing(object sender, FormClosingEventArgs e)
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
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Reloj_Minecraft_Tiempo_Real_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Reloj_Minecraft_Tiempo_Real_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Variable_doDaylightCycle || Variable_Tickspeed == 0L) Actualizar_Ciclo_Día_Noche(Variable_Gametick);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Reloj_Minecraft_Tiempo_Real_KeyDown(object sender, KeyEventArgs e)
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
                    else if (e.KeyCode == Keys.Insert) // Generate a new random lightning.
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Pendiente_Generar_Rayo_Ángulo = -1d;
                        Pendiente_Generar_Rayo = true;
                        if (!Variable_doDaylightCycle || Variable_Tickspeed == 0L) Actualizar_Ciclo_Día_Noche(Variable_Gametick);
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Reloj_Minecraft_Tiempo_Real_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Reloj_Minecraft_Tiempo_Real_DragDrop(object sender, DragEventArgs e)
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
                                    if (Información_Nivel.DayTime > long.MinValue) Numérico_Gametick.Value = Math.Abs(Información_Nivel.DayTime);
                                    if (Información_Nivel.Thundering > 0L && Información_Nivel.ThunderTime > 0L)
                                    {
                                        if (ComboBox_Clima.SelectedIndex != (int)Climas.Automatic) ComboBox_Clima.SelectedIndex = (int)Climas.Automatic;
                                        else ComboBox_Clima_SelectedIndexChanged(ComboBox_Clima, EventArgs.Empty);
                                        Variable_Clima_Automático = Climas.Thunder;
                                        Variable_Clima_Ticks = (int)Información_Nivel.ThunderTime;
                                    }
                                    else if (Información_Nivel.Raining > 0L && Información_Nivel.RainTime > 0L)
                                    {
                                        if (ComboBox_Clima.SelectedIndex != (int)Climas.Automatic) ComboBox_Clima.SelectedIndex = (int)Climas.Automatic;
                                        else ComboBox_Clima_SelectedIndexChanged(ComboBox_Clima, EventArgs.Empty);
                                        Variable_Clima_Automático = Climas.Rain;
                                        Variable_Clima_Ticks = (int)Información_Nivel.RainTime;
                                    }
                                    else
                                    {
                                        if (ComboBox_Clima.SelectedIndex != (int)Climas.Automatic) ComboBox_Clima.SelectedIndex = (int)Climas.Automatic;
                                        else ComboBox_Clima_SelectedIndexChanged(ComboBox_Clima, EventArgs.Empty);
                                        Variable_Clima_Automático = Climas.Clear;
                                        if (Información_Nivel.ClearWeatherTime > 0L) Variable_Clima_Ticks = (int)Información_Nivel.ClearWeatherTime;
                                    }
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
                if (Variable_doDaylightCycle) // If the automatic advance of time is enabled
                {
                    long Milisegundos_Gametick = Cronómetro_Gametick.ElapsedMilliseconds; // Obtain the elapsed milliseconds in the timer
                    long Gametick = ((Milisegundos_Gametick - Milisegundos_Gametick_Anterior) / 50L) * Variable_Tickspeed; // Calculate the elapsed milliseconds since the last draw and divide them by 50 to obtain the elapsed Minecraft ticks and multiply them by the tickspeed to control the speed and direction of the time.
                    if (Variable_Clima == Climas.Automatic)
                    {
                        Variable_Clima_Ticks -= Math.Abs((int)Gametick); // Avoid double negatives and subtract the ticks from the current automatic weather.
                        if (Variable_Clima_Ticks < 0) Actualizar_Clima();
                        else Barra_Estado_Etiqueta_Clima.Text = "Weather: " + Variable_Clima_Automático.ToString().ToLowerInvariant() + " (" + Traducir_Tick_Intervalo(Variable_Clima_Ticks) + ")";
                    }
                    Gametick += Variable_Gametick; // Add the tick result to the previous gametick, this will be the new gametick.
                    if (Gametick < 0L) Gametick = (4294944000L - Math.Abs(Gametick)); // Check that the new gametick is inside the valid boundaries, if not, correct it.
                    if (Gametick > 4294944000L) Gametick = Gametick - 4294944000L;
                    //this.Text = Program.Obtener_Matriz_Variables_Traducida_Texto(new object[] { Gametick, Milisegundos_Gametick, Milisegundos_Gametick_Anterior, Variable_Gametick, Variable_Tickspeed });
                    if (Gametick != Variable_Gametick && Gametick >= 0L && Gametick <= 4294944000L && Variable_Tickspeed != 0L) // If the new gametick is not the old and the time is running in any direction.
                    {
                        Milisegundos_Gametick_Anterior = Milisegundos_Gametick; // Save the current milliseconds as the previous ones for the next drawing.
                        Numérico_Gametick.Value = Gametick; // Set the new gametick, this will call the necessary methods to draw the screen.
                    }
                    else if (Variable_Tickspeed == 0L) Milisegundos_Gametick_Anterior = Milisegundos_Gametick; // If the time is stopped, save the current milliseconds as the previous ones for the next drawing.
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

        private void CheckBox_doDaylightCycle_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_doDaylightCycle = CheckBox_doDaylightCycle.Checked;
                Registro_Guardar_Opciones();
                if (!Variable_doDaylightCycle)
                {
                    Cronómetro_Gametick.Reset();
                    Milisegundos_Gametick_Anterior = 0L;
                    Numérico_Gametick.ReadOnly = false;
                    Numérico_Gametick.InterceptArrowKeys = true;
                    Numérico_Gametick.Increment = 1m;
                }
                else
                {
                    Numérico_Gametick.Increment = 0m;
                    Numérico_Gametick.InterceptArrowKeys = false;
                    Numérico_Gametick.ReadOnly = true;
                    Milisegundos_Gametick_Anterior = 0L;
                    Cronómetro_Gametick.Restart();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Numérico_Gametick_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Numérico_Gametick.Refresh();
                Variable_Gametick = (long)Numérico_Gametick.Value;
                Actualizar_Ciclo_Día_Noche(Variable_Gametick);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Tickspeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Tickspeed.SelectedIndex > -1)
                {
                    Variable_Tickspeed = long.Parse(ComboBox_Tickspeed.Text.Replace("x", null));
                    Registro_Guardar_Opciones();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Array that holds up the sky colors of the sunrise and sunset. For the sunrise access to the first 31 colors in the array. For the sunset access to the last 16 colors in the array.
        /// </summary>
        [Obsolete("The sky color now is calculated with a variable double proportion between 2 colors, based on the current day tick value.")]
        internal static readonly Color[] Matriz_Colores_Cielo = new Color[46]
        {
            Color.FromArgb(255, 0, 0, 0),
            Color.FromArgb(255, 3, 2, 0),
            Color.FromArgb(255, 8, 6, 0),
            Color.FromArgb(255, 14, 10, 0),
            Color.FromArgb(255, 21, 15, 0),
            Color.FromArgb(255, 30, 22, 0),
            Color.FromArgb(255, 42, 31, 0),
            Color.FromArgb(255, 56, 42, 0),
            Color.FromArgb(255, 74, 55, 0),
            Color.FromArgb(255, 97, 72, 0),
            Color.FromArgb(255, 124, 93, 0),
            Color.FromArgb(255, 156, 117, 0),
            Color.FromArgb(255, 192, 144, 1),
            Color.FromArgb(255, 223, 171, 16),
            Color.FromArgb(255, 255, 199, 32),
            Color.FromArgb(255, 255, 224, 130),

            //Color.FromArgb(255, 255, 224, 130),
            Color.FromArgb(255, 255, 199, 32),
            Color.FromArgb(255, 223, 172, 19),
            Color.FromArgb(255, 192, 147, 6),
            Color.FromArgb(255, 156, 123, 12),
            Color.FromArgb(255, 124, 103, 19),
            Color.FromArgb(255, 97, 87, 29),
            Color.FromArgb(255, 74, 76, 41),
            Color.FromArgb(255, 56, 72, 58),
            Color.FromArgb(255, 42, 72, 79),
            Color.FromArgb(255, 30, 77, 104),
            Color.FromArgb(255, 21, 84, 132),
            Color.FromArgb(255, 14, 102, 174),
            Color.FromArgb(255, 11, 118, 210),
            Color.FromArgb(255, 21, 138, 241),
            Color.FromArgb(255, 51, 158, 255),

            //Color.FromArgb(255, 51, 158, 255),
            Color.FromArgb(255, 18, 136, 241),
            Color.FromArgb(255, 3, 112, 210),
            Color.FromArgb(255, 0, 92, 174),
            Color.FromArgb(255, 0, 69, 132),
            Color.FromArgb(255, 0, 55, 104),
            Color.FromArgb(255, 0, 41, 79),
            Color.FromArgb(255, 0, 30, 58),
            Color.FromArgb(255, 0, 21, 41),
            Color.FromArgb(255, 0, 15, 29),
            Color.FromArgb(255, 0, 10, 19),
            Color.FromArgb(255, 0, 6, 12),
            Color.FromArgb(255, 0, 3, 5),
            Color.FromArgb(255, 0, 1, 3),
            Color.FromArgb(255, 0, 0, 0),
            Color.FromArgb(255, 0, 0, 0),
        };

        /// <summary>
        /// Array that holds up the Sun colors of the sunrise and sunset. For the sunrise access to the first 31 colors in the array. For the sunset access to the last 16 colors in the array.
        /// </summary>
        [Obsolete("The Sun color now is calculated with a with mask, only varying it's alpha value.")]
        internal static readonly Color[] Matriz_Colores_Sol = new Color[16]
        {
            Color.FromArgb(0, 0, 0, 0),
            Color.FromArgb(1, 3, 2, 0),
            Color.FromArgb(6, 8, 7, 3),
            Color.FromArgb(10, 14, 13, 5),
            Color.FromArgb(18, 21, 21, 12),
            Color.FromArgb(27, 30, 32, 19),
            Color.FromArgb(39, 42, 46, 29),
            Color.FromArgb(53, 56, 63, 41),
            Color.FromArgb(72, 74, 85, 58),
            Color.FromArgb(96, 97, 113, 79),
            Color.FromArgb(125, 124, 148, 104),
            Color.FromArgb(158, 156, 186, 132),
            Color.FromArgb(201, 192, 236, 175),
            Color.FromArgb(235, 226, 255, 226),
            Color.FromArgb(255, 255, 255, 255),
            Color.FromArgb(255, 255, 255, 255),
        };

        internal string Traducir_Tick_Intervalo(long Ticks)
        {
            try
            {
                TimeSpan Intervalo = new TimeSpan(0, 0, 0, 0, (int)(Ticks * 50L));

                string Día = Intervalo.Days.ToString();
                string Hora = Intervalo.Hours.ToString();
                string Minuto = Intervalo.Minutes.ToString();
                string Segundo = Intervalo.Seconds.ToString();

                //while (Hora.Length < 2) Hora = '0' + Hora;
                while (Hora.Length < 2) Hora = '0' + Hora;
                while (Minuto.Length < 2) Minuto = '0' + Minuto;
                while (Segundo.Length < 2) Segundo = '0' + Segundo;
                
                return Día + ":" + Hora + ':' + Minuto + ':' + Segundo;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return "0:00:00:00";
        }

        /// <summary>
        /// Updates the clock and draws the sun, moon and other things based on the specified tick.
        /// </summary>
        /// <param name="Gametick">Any gametick in a 32 bits (int) range.</param>
        internal void Actualizar_Ciclo_Día_Noche(long Gametick)
        {
            try
            {
                if (!Ocupado)
                {
                    // 1 second contains 20 game ticks or 10 redstone ticks.
                    // 1 game tick equals 50 milliseconds.
                    // 1 redstone tick equals 100 milliseconds.
                    // The still water has an animation frametime of 2 or 100 milliseconds.

                    //Gametick_Anterior = Gametick;
                    int Ticks = Environment.TickCount;
                    //Gametick = Math.Abs(Gametick);
                    int Daytick = (int)(Math.Abs(Gametick) % 24000L);
                    int Gameday = (int)(Math.Abs(Gametick) / 24000L);
                    int Fase_Lunar = Gameday % 8;
                    //int Frametime = Gametick % 32; // Animation index for the waters.
                    int Ancho_Cliente = Picture.ClientSize.Width;
                    int Alto_Cliente = Picture.ClientSize.Height;
                    if (Ancho_Cliente <= 0) Ancho_Cliente = 1;
                    if (Alto_Cliente <= 0) Alto_Cliente = 1;
                    int Máximo_Ancho_Alto = Math.Max(Ancho_Cliente, Alto_Cliente);
                    Bitmap Imagen = new Bitmap(Ancho_Cliente, Alto_Cliente, PixelFormat.Format24bppRgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    //Pintar.Clear(Color.Black);
                    Pintar.CompositingMode = CompositingMode.SourceOver;
                    //Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                    //Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.None;

                    double Ángulo_Sol = 360d - (((double)Daytick / (24000d / 360d)) - 90d);
                    if (Ángulo_Sol < 0d) Ángulo_Sol += 360d;
                    if (Ángulo_Sol > 360d) Ángulo_Sol -= 360;
                    //double Ángulo_Sol = 360d - ((Gametime / 50d) - 90d);
                    //Ángulo_Sol = Gametime % 360d;

                    double Ángulo_Luna = Ángulo_Sol - 180d;
                    if (Ángulo_Luna < 0d) Ángulo_Luna += 360d;
                    if (Ángulo_Luna > 360d) Ángulo_Luna -= 360;

                    double Multiplicador_Tierra_Sol_Luna = (double)8;

                    Climas Clima = Variable_Clima == Climas.Automatic ? Variable_Clima_Automático : Variable_Clima;

                    //int Ancho_Cliente_1_5 = (int)Math.Round((double)Ancho_Cliente * 1.5d, MidpointRounding.AwayFromZero);
                    //int Alto_Cliente_1_5 = (int)Math.Round((double)Ancho_Cliente * 1.5d, MidpointRounding.AwayFromZero);
                    //LinearGradientBrush Pincel_Cielo = new LinearGradientBrush(new Rectangle(-(Ancho_Cliente / 2), -(Alto_Cliente / 2), Ancho_Cliente * 2, Alto_Cliente * 2), Color.Black, Color.Blue, LinearGradientMode.Horizontal);
                    /*int Máximo_Ancho_Alto = Math.Max(Ancho_Cliente, Alto_Cliente); // 4096
                    Bitmap Imagen_Gradiente = new Bitmap(Máximo_Ancho_Alto * 2, Máximo_Ancho_Alto * 2, PixelFormat.Format32bppArgb);
                    Graphics Pintar_Gradiente = Graphics.FromImage(Imagen_Gradiente);
                    Pintar_Gradiente.Clear(Color.Black);
                    Pintar_Gradiente.CompositingMode = CompositingMode.SourceOver;
                    Pintar_Gradiente.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar_Gradiente.InterpolationMode = InterpolationMode.NearestNeighbor;
                    Pintar_Gradiente.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar_Gradiente.SmoothingMode = SmoothingMode.None;
                    LinearGradientBrush Pincel_Cielo = new LinearGradientBrush(new Rectangle(0, 0, Máximo_Ancho_Alto * 2, Máximo_Ancho_Alto * 2), Color.Blue, Color.Black, LinearGradientMode.Vertical);
                    Pincel_Cielo.GammaCorrection = true;
                    ColorBlend Mezcla_Colores = new ColorBlend(3);
                    Mezcla_Colores.Colors = new Color[3] { Color.Blue, Color.Black, Color.Black };
                    Mezcla_Colores.Positions = new float[3] { 0f, 0.5f, 1f };
                    Pincel_Cielo.InterpolationColors = Mezcla_Colores;
                    Pintar_Gradiente.FillRectangle(Pincel_Cielo, 0, 0, Máximo_Ancho_Alto * 2, Máximo_Ancho_Alto * 2);
                    Pintar_Gradiente.Dispose();
                    Pintar_Gradiente = null;
                    Pincel_Cielo.Dispose();
                    Pincel_Cielo = null;
                    //Program.Guardar_Imagen_Temporal(Imagen_Gradiente, "Gradiente_Cielo_8K");

                    Pintar.TranslateTransform((float)((double)Ancho_Cliente / 2d), (float)((double)Alto_Cliente / 2d));
                    Pintar.RotateTransform((float)Ángulo_Sol);
                    Pintar.DrawImage(Imagen_Gradiente, new Rectangle(-Máximo_Ancho_Alto, -Máximo_Ancho_Alto, Máximo_Ancho_Alto * 2, Máximo_Ancho_Alto * 2), new Rectangle(0, 0, Máximo_Ancho_Alto * 2, Máximo_Ancho_Alto * 2), GraphicsUnit.Pixel);
                    Pintar.ResetTransform();
                    Imagen_Gradiente.Dispose();
                    Imagen_Gradiente = null;*/

                    Color Color_ARGB_Sol = Color.FromArgb(232, 255, 255, 255);
                    Color Color_ARGB_Luna = Color.FromArgb(0, 255, 255, 255);
                    //int Índice_Color_Sol = 15; // Default white mask.
                    if (Variable_Dibujar_Cielo_Azul)
                    {
                        //if ((Gametick >= 0 && Gametick < 0) || (Gametick >= 0 && Gametick < 0)) // Only draw during Sunrise or sunset
                        //if (Variable_Rotar_Cielo_Azul)
                        {
                            Pintar.TranslateTransform((float)((double)Ancho_Cliente / 2d), (float)((double)Alto_Cliente / 2d));
                            if (Variable_Rotar_Cielo_Azul)
                            {
                                Pintar.SmoothingMode = SmoothingMode.HighQuality;
                                Pintar.RotateTransform((float)Ángulo_Sol);
                            }
                        }

                        Color Color_ARGB_Cielo = Color.FromArgb(255, 51, 158, 255);
                        Color Color_ARGB_Noche = Color.FromArgb(255, 0, 0, 0); // The original Minecraft night is full black, but use this instead the colors below based on the moon phase.
                        Color Color_ARGB_Amanecer = Color.FromArgb(255, 255, 224, 130);
                        Color Color_ARGB_Día = Color.FromArgb(255, 51, 158, 255);
                        //Color Color_ARGB_Anochecer = Color.FromArgb(255, , , );

                        if (Fase_Lunar == 0) Color_ARGB_Noche = Color.FromArgb(255, 64, 64, 64); // Full moon
                        else if (Fase_Lunar == 1) Color_ARGB_Noche = Color.FromArgb(255, 52, 52, 52); // Waning gibbous
                        else if (Fase_Lunar == 2) Color_ARGB_Noche = Color.FromArgb(255, 40, 40, 40); // Last quarter
                        else if (Fase_Lunar == 3) Color_ARGB_Noche = Color.FromArgb(255, 28, 28, 28); // Waning crescent
                        else if (Fase_Lunar == 4) Color_ARGB_Noche = Color.FromArgb(255, 16, 16, 16); // New moon
                        else if (Fase_Lunar == 5) Color_ARGB_Noche = Color.FromArgb(255, 28, 28, 28); // Waxing crescent
                        else if (Fase_Lunar == 6) Color_ARGB_Noche = Color.FromArgb(255, 40, 40, 40); // First quarter
                        else if (Fase_Lunar == 7) Color_ARGB_Noche = Color.FromArgb(255, 52, 52, 52); // Waxing gibbous

                        //int Índice_Color = 31; // Default sky color on daytime.
                        if (Daytick >= 22550 || Daytick < 450) // Sunrise: 1899 ticks.
                        {
                            int Diferencia = Daytick;
                            if (Diferencia < 450) Diferencia += 24000;
                            Diferencia -= 22550;
                            Color_ARGB_Sol = Color.FromArgb((Diferencia * 232) / 1899, 255, 255, 255);
                            Color_ARGB_Luna = Color.FromArgb(72 - ((Diferencia * 72) / 1899), 255, 255, 255);
                            if (Diferencia < 950)
                            {
                                Color_ARGB_Cielo = Color.FromArgb(255, ((Color_ARGB_Noche.R * (949 - Diferencia)) / 949) + ((Color_ARGB_Amanecer.R * Diferencia) / 949), ((Color_ARGB_Noche.G * (949 - Diferencia)) / 949) + ((Color_ARGB_Amanecer.G * Diferencia) / 949), ((Color_ARGB_Noche.B * (949 - Diferencia)) / 949) + ((Color_ARGB_Amanecer.B * Diferencia) / 949));
                            }
                            else
                            {
                                Diferencia -= 949;
                                Color_ARGB_Cielo = Color.FromArgb(255, ((Color_ARGB_Amanecer.R * (948 - Diferencia)) / 948) + ((Color_ARGB_Día.R * Diferencia) / 948), ((Color_ARGB_Amanecer.G * (948 - Diferencia)) / 948) + ((Color_ARGB_Día.G * Diferencia) / 948), ((Color_ARGB_Amanecer.B * (948 - Diferencia)) / 948) + ((Color_ARGB_Día.B * Diferencia) / 948));
                            }
                            //Índice_Color = (int)Math.Round(((double)Diferencia * 30d) / 1899d, MidpointRounding.AwayFromZero);
                        }
                        else if (Daytick >= 11616 && Daytick < 13800) // Sunset: 2184 ticks.
                        {
                            int Diferencia = Daytick;
                            Diferencia -= 11616;
                            Color_ARGB_Sol = Color.FromArgb(232 - ((Diferencia * 232) / 2183), 255, 255, 255);
                            Color_ARGB_Luna = Color.FromArgb((Diferencia * 72) / 2183, 255, 255, 255);
                            Color_ARGB_Cielo = Color.FromArgb(255, ((Color_ARGB_Día.R * (2183 - Diferencia)) / 2183) + ((Color_ARGB_Noche.R * Diferencia) / 2183), ((Color_ARGB_Día.G * (2183 - Diferencia)) / 2183) + ((Color_ARGB_Noche.G * Diferencia) / 2183), ((Color_ARGB_Día.B * (2183 - Diferencia)) / 2183) + ((Color_ARGB_Noche.B * Diferencia) / 2183));
                        }
                        else if (Daytick >= 13800 && Daytick < 22550) // Night
                        {
                            Color_ARGB_Sol = Color.FromArgb(0, 255, 255, 255);
                            Color_ARGB_Luna = Color.FromArgb(72, 255, 255, 255);
                            Color_ARGB_Cielo = Color_ARGB_Noche;
                        }

                        if (Variable_Oscurecer_Cielo && Clima != Climas.Clear) // Darken the sky, Sun and Moon if the weather is not clear.
                        {
                            //int Brillo = ((Color_ARGB_Cielo.R + Color_ARGB_Cielo.G + Color_ARGB_Cielo.B) / 3) / 4;
                            //int Brillo = 32;
                            Color_ARGB_Cielo = Color.FromArgb(Color_ARGB_Cielo.A, Color_ARGB_Cielo.R / 2, Color_ARGB_Cielo.G / 2, Color_ARGB_Cielo.B / 2);
                            //if (Color_ARGB_Sol.A > 0) Color_ARGB_Sol = Color.FromArgb((Color_ARGB_Sol.A + Brillo) / 2, Color_ARGB_Sol.R, Color_ARGB_Sol.G, Color_ARGB_Sol.B);
                            //else Color_ARGB_Sol = Color.FromArgb(255 - Brillo, 0, 0, 0);
                            //if (Color_ARGB_Luna.A > 0) Color_ARGB_Luna = Color.FromArgb(((Color_ARGB_Luna.A) + Brillo) / 2, Color_ARGB_Luna.R, Color_ARGB_Luna.G, Color_ARGB_Luna.B);
                            //else Color_ARGB_Luna = Color.FromArgb(255 - Brillo, 0, 0, 0);
                            if (Color_ARGB_Sol.A > 0) Color_ARGB_Sol = Color.FromArgb(Color_ARGB_Sol.A, 128, 128, 128);
                            if (Color_ARGB_Luna.A > 0) Color_ARGB_Luna = Color.FromArgb(Color_ARGB_Luna.A * 2, 0, 0, 0);
                        }
                        if (Color_ARGB_Cielo.R > 0 || Color_ARGB_Cielo.G > 0 || Color_ARGB_Cielo.B > 0)
                        {
                            //SolidBrush Pincel_Cielo = new SolidBrush(Matriz_Colores_Cielo[Índice_Color]);
                            SolidBrush Pincel_Cielo = new SolidBrush(Color_ARGB_Cielo);
                            if (Variable_Rotar_Cielo_Azul)
                            {
                                Pintar.FillRectangle(Pincel_Cielo, -Máximo_Ancho_Alto, -Máximo_Ancho_Alto, Máximo_Ancho_Alto * 2, Máximo_Ancho_Alto);
                            }
                            else Pintar.FillRectangle(Pincel_Cielo, -Máximo_Ancho_Alto, -Máximo_Ancho_Alto, Máximo_Ancho_Alto * 2, Máximo_Ancho_Alto);
                            //else Pintar.FillRectangle(Pincel_Cielo, 0, 0, Ancho_Cliente, Alto_Cliente / 2);
                            Pincel_Cielo.Dispose();
                            Pincel_Cielo = null;
                        }
                        Pintar.SmoothingMode = SmoothingMode.None;
                        Pintar.ResetTransform();
                    }

                    // Avoid the Sun and Moon clipping outside of the screen.
                    double Ancho_Sol_Luna = ((double)Ancho_Cliente / 2d) - (10d * Multiplicador_Tierra_Sol_Luna);
                    double Alto_Sol_Luna = ((double)Alto_Cliente / 2d) - (10d * Multiplicador_Tierra_Sol_Luna);

                    // Calculate the Math.Sin (horizontal coordinate) and Math.Cos (vertical coordinate) of the Sun and Moon to draw them following an ellipse:
                    double Seno_Sol_X = Ancho_Sol_Luna * Math.Sin((Ángulo_Sol * Math.PI) / 180d);
                    double Coseno_Sol_Y = Alto_Sol_Luna * Math.Cos(((Ángulo_Sol + 180d) * Math.PI) / 180d); // The "+ 180d" is used to invert vertically the Y coordinate.

                    double Seno_Luna_X = Ancho_Sol_Luna * Math.Sin((Ángulo_Luna * Math.PI) / 180d);
                    double Coseno_Luna_Y = Alto_Sol_Luna * Math.Cos(((Ángulo_Luna + 180d) * Math.PI) / 180d); // The "+ 180d" is used to invert vertically the Y coordinate.

                    // Avoid out of boundaries drawings:
                    if (Seno_Sol_X < 0) Seno_Sol_X += 0;
                    if (Seno_Sol_X >= Ancho_Cliente) Seno_Sol_X = Ancho_Cliente - 1;
                    if (Coseno_Sol_Y < 0) Coseno_Sol_Y += 0;
                    if (Coseno_Sol_Y >= Alto_Cliente) Coseno_Sol_Y = Alto_Cliente - 1;

                    if (Seno_Luna_X < 0) Seno_Luna_X += 0;
                    if (Seno_Luna_X >= Ancho_Cliente) Seno_Luna_X = Ancho_Cliente - 1;
                    if (Coseno_Luna_Y < 0) Coseno_Luna_Y += 0;
                    if (Coseno_Luna_Y >= Alto_Cliente) Coseno_Luna_Y = Alto_Cliente - 1;

                    //this.Text = Texto_Título + " - [Tick: " + Gametick.ToString() + ", Day Tick: " + Gametime.ToString() + ", Day: " + Gameday.ToString() + ", Sun Angle: " + Program.Traducir_Número_Decimales_Redondear(Ángulo_Sol, 2) + ", Moon Angle: " + Program.Traducir_Número_Decimales_Redondear(Ángulo_Luna, 2) + "]";
                    this.Text = Texto_Título + " - [Day: " + Program.Traducir_Número(Gameday) + ", Daytick: " + Program.Traducir_Número(Daytick) + ", Sun: " + Program.Traducir_Número_Decimales_Redondear(Ángulo_Sol, 2) + " º, Moon: " + Program.Traducir_Número_Decimales_Redondear(Ángulo_Luna, 2) + " º]";

                    // Draw the Minecraft clock with it's real time.
                    int Índice_Reloj = (int)Math.Round((Ángulo_Sol * 64d) / 360d, MidpointRounding.AwayFromZero);
                    if (Índice_Reloj < 0) Índice_Reloj = 0;
                    if (Índice_Reloj >= 64) Índice_Reloj = 63;
                    string Texto_Reloj = Índice_Reloj.ToString();
                    while (Texto_Reloj.Length < 2) Texto_Reloj = '0' + Texto_Reloj;
                    //int Ancho_Alto_Cliente = Math.Max(Math.Min(Picture.ClientSize.Width - ((20 * (int)Multiplicador_Sol_Luna) * 2), Picture.ClientSize.Height - ((20 * (int)Multiplicador_Sol_Luna) * 2)), 1);
                    //int Zoom;
                    //Bitmap Imagen_Reloj = Program.Obtener_Imagen_Autozoom(Minecraft.Obtener_Textura_Recursos("minecraft_clock_" + Texto_Reloj), Ancho_Alto_Cliente, Ancho_Alto_Cliente, true, CheckState.Unchecked, out Zoom);

                    int Sensor_Luz_Diurna = 0;
                    if (Clima == Climas.Clear)
                    {
                        if (Daytick >= 4300 && Daytick < 7720) Sensor_Luz_Diurna = 15;
                        else if (Daytick >= 3180 && Daytick < 8840) Sensor_Luz_Diurna = 14;
                        else if (Daytick >= 2460 && Daytick < 9560) Sensor_Luz_Diurna = 13;
                        else if (Daytick >= 1880 && Daytick < 10140) Sensor_Luz_Diurna = 12;
                        else if (Daytick >= 1380 && Daytick < 10640) Sensor_Luz_Diurna = 11;
                        else if (Daytick >= 940 && Daytick < 11080) Sensor_Luz_Diurna = 10;
                        else if (Daytick >= 540 && Daytick < 11480) Sensor_Luz_Diurna = 9;
                        else if (Daytick >= 180 && Daytick < 11840) Sensor_Luz_Diurna = 8;
                        else if (Daytick >= 23960 || Daytick < 12040) Sensor_Luz_Diurna = 7;
                        else if (Daytick >= 23780 || Daytick < 12240) Sensor_Luz_Diurna = 6;
                        else if (Daytick >= 23540 || Daytick < 12480) Sensor_Luz_Diurna = 5;
                        else if (Daytick >= 23300 || Daytick < 12720) Sensor_Luz_Diurna = 4;
                        else if (Daytick >= 23080 || Daytick < 12940) Sensor_Luz_Diurna = 3;
                        else if (Daytick >= 22800 || Daytick < 13220) Sensor_Luz_Diurna = 2;
                        else if (Daytick >= 22340 || Daytick < 13680) Sensor_Luz_Diurna = 1;
                    }
                    else if (Clima == Climas.Rain || Clima == Climas.Snow)
                    {
                        if (Daytick >= 4120 && Daytick < 7900) Sensor_Luz_Diurna = 12;
                        else if (Daytick >= 2880 && Daytick < 9140) Sensor_Luz_Diurna = 11;
                        else if (Daytick >= 2080 && Daytick < 9940) Sensor_Luz_Diurna = 10;
                        else if (Daytick >= 1440 && Daytick < 10580) Sensor_Luz_Diurna = 9;
                        else if (Daytick >= 900 && Daytick < 11120) Sensor_Luz_Diurna = 8;
                        else if (Daytick >= 400 && Daytick < 11620) Sensor_Luz_Diurna = 7;
                        else if (Daytick >= 0 && Daytick < 12020) Sensor_Luz_Diurna = 6;
                        else if (Daytick >= 23760 || Daytick < 12260) Sensor_Luz_Diurna = 5;
                        else if (Daytick >= 23520 || Daytick < 12500) Sensor_Luz_Diurna = 4;
                        else if (Daytick >= 23240 || Daytick < 12780) Sensor_Luz_Diurna = 3;
                        else if (Daytick >= 22800 || Daytick < 13220) Sensor_Luz_Diurna = 2;
                        else if (Daytick >= 22340 || Daytick < 13680) Sensor_Luz_Diurna = 1;
                    }
                    else if (Clima == Climas.Thunder)
                    {
                        if (Daytick >= 3960 && Daytick < 8060) Sensor_Luz_Diurna = 10;
                        else if (Daytick >= 2620 && Daytick < 9400) Sensor_Luz_Diurna = 9;
                        else if (Daytick >= 1740 && Daytick < 10280) Sensor_Luz_Diurna = 8;
                        else if (Daytick >= 1040 && Daytick < 10980) Sensor_Luz_Diurna = 7;
                        else if (Daytick >= 460 && Daytick < 11560) Sensor_Luz_Diurna = 6;
                        else if (Daytick >= 60 && Daytick < 11940) Sensor_Luz_Diurna = 5;
                        else if (Daytick >= 23700 || Daytick < 12300) Sensor_Luz_Diurna = 4;
                        else if (Daytick >= 23360 || Daytick < 12660) Sensor_Luz_Diurna = 3;
                        else if (Daytick >= 22960 || Daytick < 13060) Sensor_Luz_Diurna = 2;
                        else if (Daytick >= 22340 || Daytick < 13680) Sensor_Luz_Diurna = 1;
                    }
                    Barra_Estado_Etiqueta_Sensor_Luz_Diurna.Text = "Daylight sensor: " + Sensor_Luz_Diurna.ToString();

                    int Sensor_Luz_Diurna_Invertido = 15;
                    if (Clima == Climas.Clear)
                    {
                        if (Daytick >= 4300 && Daytick < 7720) Sensor_Luz_Diurna_Invertido = 0;
                        else if (Daytick >= 3180 && Daytick < 8840) Sensor_Luz_Diurna_Invertido = 1;
                        else if (Daytick >= 2460 && Daytick < 9560) Sensor_Luz_Diurna_Invertido = 2;
                        else if (Daytick >= 1880 && Daytick < 10140) Sensor_Luz_Diurna_Invertido = 3;
                        else if (Daytick >= 1380 && Daytick < 10640) Sensor_Luz_Diurna_Invertido = 4;
                        else if (Daytick >= 940 && Daytick < 11080) Sensor_Luz_Diurna_Invertido = 5;
                        else if (Daytick >= 540 && Daytick < 11480) Sensor_Luz_Diurna_Invertido = 6;
                        else if (Daytick >= 180 && Daytick < 11840) Sensor_Luz_Diurna_Invertido = 7;
                        else if (Daytick >= 23960 || Daytick < 12040) Sensor_Luz_Diurna_Invertido = 8;
                        else if (Daytick >= 23780 || Daytick < 12240) Sensor_Luz_Diurna_Invertido = 9;
                        else if (Daytick >= 23540 || Daytick < 12480) Sensor_Luz_Diurna_Invertido = 10;
                        else if (Daytick >= 23300 || Daytick < 12720) Sensor_Luz_Diurna_Invertido = 11;
                        else if (Daytick >= 23080 || Daytick < 12940) Sensor_Luz_Diurna_Invertido = 12;
                        else if (Daytick >= 22800 || Daytick < 13220) Sensor_Luz_Diurna_Invertido = 13;
                        else if (Daytick >= 22340 || Daytick < 13680) Sensor_Luz_Diurna_Invertido = 14;
                    }
                    else if (Clima == Climas.Rain || Clima == Climas.Snow)
                    {
                        if (Daytick >= 4120 && Daytick < 7900) Sensor_Luz_Diurna_Invertido = 3;
                        else if (Daytick >= 2880 && Daytick < 9140) Sensor_Luz_Diurna_Invertido = 4;
                        else if (Daytick >= 2080 && Daytick < 9940) Sensor_Luz_Diurna_Invertido = 5;
                        else if (Daytick >= 1440 && Daytick < 10580) Sensor_Luz_Diurna_Invertido = 6;
                        else if (Daytick >= 900 && Daytick < 11120) Sensor_Luz_Diurna_Invertido = 7;
                        else if (Daytick >= 400 && Daytick < 11620) Sensor_Luz_Diurna_Invertido = 8;
                        else if (Daytick >= 0 && Daytick < 12020) Sensor_Luz_Diurna_Invertido = 9;
                        else if (Daytick >= 23760 || Daytick < 12260) Sensor_Luz_Diurna_Invertido = 10;
                        else if (Daytick >= 23520 || Daytick < 12500) Sensor_Luz_Diurna_Invertido = 11;
                        else if (Daytick >= 23240 || Daytick < 12780) Sensor_Luz_Diurna_Invertido = 12;
                        else if (Daytick >= 22800 || Daytick < 13220) Sensor_Luz_Diurna_Invertido = 13;
                        else if (Daytick >= 22340 || Daytick < 13680) Sensor_Luz_Diurna_Invertido = 14;
                    }
                    else if (Clima == Climas.Thunder)
                    {
                        if (Daytick >= 3960 && Daytick < 8060) Sensor_Luz_Diurna_Invertido = 5;
                        else if (Daytick >= 2620 && Daytick < 9400) Sensor_Luz_Diurna_Invertido = 6;
                        else if (Daytick >= 1740 && Daytick < 10280) Sensor_Luz_Diurna_Invertido = 7;
                        else if (Daytick >= 1040 || Daytick < 10980) Sensor_Luz_Diurna_Invertido = 8;
                        else if (Daytick >= 460 || Daytick < 11560) Sensor_Luz_Diurna_Invertido = 9;
                        else if (Daytick >= 60 || Daytick < 11940) Sensor_Luz_Diurna_Invertido = 10;
                        else if (Daytick >= 23700 || Daytick < 12300) Sensor_Luz_Diurna_Invertido = 11;
                        else if (Daytick >= 23360 || Daytick < 12660) Sensor_Luz_Diurna_Invertido = 12;
                        else if (Daytick >= 22960 || Daytick < 13060) Sensor_Luz_Diurna_Invertido = 13;
                        else if (Daytick >= 22340 || Daytick < 13680) Sensor_Luz_Diurna_Invertido = 14;
                    }
                    Barra_Estado_Etiqueta_Sensor_Luz_Diurna_Invertido.Text = "Inverted sensor: " + Sensor_Luz_Diurna_Invertido.ToString();

                    //Barra_Estado_Etiqueta_Luna.Image = Program.Obtener_Imagen_Miniatura(Resources.Lunas.Clone(new Rectangle(((Fase_Lunar % 4) * 32) + 12, (Fase_Lunar < 4 ? 0 : 32) + 12, 8, 8), PixelFormat.Format32bppArgb), 16, 16, true, false);

                    int Milisegundos = Daytick + 6000;
                    //if (Milisegundos < 0) Milisegundos += 24000;
                    if (Milisegundos >= 24000) Milisegundos -= 24000;
                    Milisegundos = (int)(((long)Milisegundos * 86400000L) / 24000L);
                    TimeSpan Intervalo = new TimeSpan(0, 0, 0, 0, Milisegundos);

                    string Hora = Intervalo.Hours.ToString();
                    string Minuto = Intervalo.Minutes.ToString();
                    string Segundo = Intervalo.Seconds.ToString();
                    //string Milisegundo = Intervalo.Milliseconds.ToString();

                    while (Hora.Length < 2) Hora = '0' + Hora;
                    while (Minuto.Length < 2) Minuto = '0' + Minuto;
                    while (Segundo.Length < 2) Segundo = '0' + Segundo;
                    //while (Milisegundo.Length < 3) Milisegundo = '0' + Milisegundo;

                    Barra_Estado_Etiqueta_Reloj.Image = Program.Obtener_Imagen_Recursos("minecraft_clock_" + Texto_Reloj);
                    Barra_Estado_Etiqueta_Reloj.Text = "Clock: " + Hora + ':' + Minuto + ':' + Segundo/* + '.' + Milisegundo*/;
                    /*int Ancho_Alto = 16 * Zoom;
                    //Pintar.ResetTransform();
                    Pintar.DrawImage(Imagen_Reloj, new Rectangle((Picture.ClientSize.Width / 2) - (Ancho_Alto / 2), (Picture.ClientSize.Height / 2) - (Ancho_Alto / 2), Ancho_Alto, Ancho_Alto), new Rectangle(0, 0, Ancho_Alto, Ancho_Alto), GraphicsUnit.Pixel);
                    Imagen_Reloj.Dispose();
                    Imagen_Reloj = null;*/

                    if (Variable_Dibujar_Tierra_Sol_Luna)
                    {
                        int Ancho_Alto_Tierra_Sol_Luna = 8 * (int)Multiplicador_Tierra_Sol_Luna;

                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        // Draw the Sun:
                        //Pintar.TranslateTransform((float)(Seno_Sol_X - (4d * Multiplicador_Sol_Luna)), (float)(Coseno_Sol_Y - (4d * Multiplicador_Sol_Luna)));
                        Pintar.TranslateTransform((float)(((double)Ancho_Cliente / 2d) + Seno_Sol_X), (float)(((double)Alto_Cliente / 2d) + Coseno_Sol_Y));
                        if (Variable_Rotar_Tierra_Sol_Luna) Pintar.RotateTransform((float)Ángulo_Sol);
                        //Pintar.TranslateTransform((float)(((double)Ancho_Cliente / 2d) + Seno_Sol_X), (float)(((double)Alto_Cliente / 2d) + Coseno_Sol_Y));
                        //Pintar.DrawImage(Resources.Sol, new RectangleF((float)(-16d * Multiplicador_Sol_Luna), (float)(-16d * Multiplicador_Sol_Luna), (float)(32d * Multiplicador_Sol_Luna), (float)(32d * Multiplicador_Sol_Luna)), new RectangleF(0f, 0f, 32f, 32f), GraphicsUnit.Pixel);
                        //Pintar.DrawImage(Resources.Sol, new RectangleF((float)(-4d * Multiplicador_Sol_Luna), (float)(-4d * Multiplicador_Sol_Luna), (float)(8d * Multiplicador_Sol_Luna), (float)(8d * Multiplicador_Sol_Luna)), new RectangleF(12f, 12f, 8f, 8f), GraphicsUnit.Pixel);
                        Bitmap Imagen_Sol = Resources.Sol.Clone(new Rectangle(12, 12, 8, 8), PixelFormat.Format32bppArgb);
                        if (Variable_Dibujar_Cielo_Azul && Color_ARGB_Sol.A > 0)
                        {
                            Graphics Pintar_Sol = Graphics.FromImage(Imagen_Sol);
                            SolidBrush Pincel_Sol = new SolidBrush(Color_ARGB_Sol);
                            Pintar_Sol.FillRectangle(Pincel_Sol, 0, 0, 8, 8);
                            Pincel_Sol.Dispose();
                            Pincel_Sol = null;
                            Pintar_Sol.Dispose();
                            Pintar_Sol = null;
                        }
                        Imagen_Sol = Program.Obtener_Imagen_Miniatura(Imagen_Sol, Ancho_Alto_Tierra_Sol_Luna * 2, Ancho_Alto_Tierra_Sol_Luna * 2, true, false, CheckState.Checked);
                        Pintar.DrawImage(Imagen_Sol, new Rectangle(-Ancho_Alto_Tierra_Sol_Luna, -Ancho_Alto_Tierra_Sol_Luna, Ancho_Alto_Tierra_Sol_Luna * 2, Ancho_Alto_Tierra_Sol_Luna * 2), new Rectangle(0, 0, Ancho_Alto_Tierra_Sol_Luna * 2, Ancho_Alto_Tierra_Sol_Luna * 2), GraphicsUnit.Pixel);
                        Imagen_Sol.Dispose();
                        Imagen_Sol = null;
                        Pintar.ResetTransform();

                        // Draw the Moon:
                        Pintar.TranslateTransform((float)(((double)Ancho_Cliente / 2d) + Seno_Luna_X), (float)(((double)Alto_Cliente / 2d) + Coseno_Luna_Y));
                        if (Variable_Rotar_Tierra_Sol_Luna) Pintar.RotateTransform((float)Ángulo_Luna);
                        Bitmap Imagen_Luna = Resources.Lunas.Clone(new Rectangle(((Fase_Lunar % 4) * 32) + 12, (Fase_Lunar < 4 ? 0 : 32) + 12, 8, 8), PixelFormat.Format32bppArgb);
                        Barra_Estado_Etiqueta_Fase_Lunar.Image = Program.Obtener_Imagen_Miniatura(Imagen_Luna, 16, 16, true, false, CheckState.Checked);
                        if (Variable_Dibujar_Cielo_Azul && Color_ARGB_Luna.A > 0)
                        {
                            Graphics Pintar_Luna = Graphics.FromImage(Imagen_Luna);
                            SolidBrush Pincel_Luna = new SolidBrush(Color_ARGB_Luna);
                            Pintar_Luna.FillRectangle(Pincel_Luna, 0, 0, 8, 8);
                            Pincel_Luna.Dispose();
                            Pincel_Luna = null;
                            Pintar_Luna.Dispose();
                            Pintar_Luna = null;
                        }
                        if (Variable_Rotar_Tierra_Sol_Luna) Imagen_Luna.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        Imagen_Luna = Program.Obtener_Imagen_Miniatura(Imagen_Luna, Ancho_Alto_Tierra_Sol_Luna, Ancho_Alto_Tierra_Sol_Luna, true, false, CheckState.Checked);
                        //Pintar.DrawImage(Imagen_Luna, new RectangleF((float)(-16d * Multiplicador_Sol_Luna), (float)(-16d * Multiplicador_Sol_Luna), (float)(32d * Multiplicador_Sol_Luna), (float)(32d * Multiplicador_Sol_Luna)), new RectangleF(0f, 0f, 32f, 32f), GraphicsUnit.Pixel);
                        Pintar.DrawImage(Imagen_Luna, new Rectangle(-(Ancho_Alto_Tierra_Sol_Luna / 2), -(Ancho_Alto_Tierra_Sol_Luna / 2), Ancho_Alto_Tierra_Sol_Luna, Ancho_Alto_Tierra_Sol_Luna), new RectangleF(0, 0, Ancho_Alto_Tierra_Sol_Luna, Ancho_Alto_Tierra_Sol_Luna), GraphicsUnit.Pixel);
                        Imagen_Luna.Dispose();
                        Imagen_Luna = null;
                        Pintar.ResetTransform();

                        // Draw the Earth:
                        Pintar.TranslateTransform((float)((double)Ancho_Cliente / 2d), (float)((double)Alto_Cliente / 2d));
                        if (Variable_Rotar_Tierra_Sol_Luna) Pintar.RotateTransform((float)Ángulo_Luna);
                        Bitmap Imagen_Tierra = Resources.Tierra;
                        /*if (Variable_Dibujar_Cielo_Azul && Color_ARGB_Luna.A > 0)
                        {
                            Graphics Pintar_Luna = Graphics.FromImage(Imagen_Tierra);
                            SolidBrush Pincel_Luna = new SolidBrush(Color_ARGB_Luna);
                            Pintar_Luna.FillRectangle(Pincel_Luna, 0, 0, 8, 8);
                            Pincel_Luna.Dispose();
                            Pincel_Luna = null;
                            Pintar_Luna.Dispose();
                            Pintar_Luna = null;
                        }*/
                        if (Variable_Rotar_Tierra_Sol_Luna) Imagen_Tierra.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        Imagen_Tierra = Program.Obtener_Imagen_Miniatura(Imagen_Tierra, Ancho_Alto_Tierra_Sol_Luna * 2, Ancho_Alto_Tierra_Sol_Luna * 2, true, false, CheckState.Checked);
                        //Pintar.DrawImage(Imagen_Luna, new RectangleF((float)(-16d * Multiplicador_Sol_Luna), (float)(-16d * Multiplicador_Sol_Luna), (float)(32d * Multiplicador_Sol_Luna), (float)(32d * Multiplicador_Sol_Luna)), new RectangleF(0f, 0f, 32f, 32f), GraphicsUnit.Pixel);
                        Pintar.DrawImage(Imagen_Tierra, new Rectangle(-Ancho_Alto_Tierra_Sol_Luna, -Ancho_Alto_Tierra_Sol_Luna, Ancho_Alto_Tierra_Sol_Luna * 2, Ancho_Alto_Tierra_Sol_Luna * 2), new RectangleF(0, 0, Ancho_Alto_Tierra_Sol_Luna * 2, Ancho_Alto_Tierra_Sol_Luna * 2), GraphicsUnit.Pixel);
                        Imagen_Tierra.Dispose();
                        Imagen_Tierra = null;
                        Pintar.ResetTransform();

                        Pintar.CompositingQuality = CompositingQuality.Default;
                        Pintar.PixelOffsetMode = PixelOffsetMode.Default;
                    }
                    if (Variable_Dibujar_Efectos_Climáticos && Clima != Climas.Clear) // Draw the weather effects.
                    {
                        Pintar.TranslateTransform((float)((double)Ancho_Cliente / 2d), (float)((double)Alto_Cliente / 2d));
                        if (Variable_Rotar_Efectos_Climáticos) Pintar.RotateTransform((float)Ángulo_Sol);
                        //Pintar.CompositingQuality = CompositingQuality.AssumeLinear;
                        //Pintar.PixelOffsetMode = PixelOffsetMode.None;
                        /*// Reset all the Graphics values to it's defaults.
                        Pintar.CompositingMode = CompositingMode.SourceOver;
                        Pintar.CompositingQuality = CompositingQuality.Default;
                        Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                        Pintar.PixelOffsetMode = PixelOffsetMode.Default;
                        Pintar.SmoothingMode = SmoothingMode.None;*/

                        if (Clima == Climas.Rain)
                        {
                            Bitmap Imagen_Lluvia = Resources.Lluvia;

                            int Divisor_Lluvia = 2; // Higher values means slower rain.
                            int Divisor_Lluvia_Horizontal = 50; // Higher values means slower rain.

                            TextureBrush Pincel_Lluvia = new TextureBrush(Imagen_Lluvia, WrapMode.Tile, new Rectangle(63 - ((Ticks / Divisor_Lluvia_Horizontal) % 63), 255 - ((Ticks / Divisor_Lluvia) % 256), 64, 256));
                            Pintar.FillRectangle(Pincel_Lluvia, -Máximo_Ancho_Alto, -Máximo_Ancho_Alto, Máximo_Ancho_Alto * 2, Máximo_Ancho_Alto * 2);
                            Pincel_Lluvia.Dispose();
                            Pincel_Lluvia = null;

                            //Imagen_Lluvia.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            /*Pincel_Lluvia = new TextureBrush(Imagen_Lluvia, WrapMode.Tile, new Rectangle(((Ticks / Divisor_Lluvia_Horizontal) % 63), 255 - ((Ticks / Divisor_Lluvia) % 256), 64, 256));
                            Pintar.FillRectangle(Pincel_Lluvia, -Máximo_Ancho_Alto, -Máximo_Ancho_Alto, Máximo_Ancho_Alto * 2, Máximo_Ancho_Alto);
                            Pincel_Lluvia.Dispose();
                            Pincel_Lluvia = null;
                            
                            //Imagen_Lluvia.RotateFlip(RotateFlipType.RotateNoneFlipY);
                            /*Pincel_Lluvia = new TextureBrush(Imagen_Lluvia, WrapMode.Tile, new Rectangle(((Ticks / Divisor_Lluvia_Horizontal) % 63), 255 - ((Ticks / Divisor_Lluvia) % 256), 64, 256));
                            Pintar.FillRectangle(Pincel_Lluvia, -Máximo_Ancho_Alto, -Máximo_Ancho_Alto, Máximo_Ancho_Alto * 2, Máximo_Ancho_Alto);
                            Pincel_Lluvia.Dispose();
                            Pincel_Lluvia = null;
                            /*
                            Imagen_Lluvia.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            Pincel_Lluvia = new TextureBrush(Imagen_Lluvia, WrapMode.Tile, new Rectangle(((Ticks / Divisor_Lluvia) % 63), 255 - ((Ticks / Divisor_Lluvia) % 256), 64, 256));
                            Pintar.FillRectangle(Pincel_Lluvia, -Máximo_Ancho_Alto, -Máximo_Ancho_Alto, Máximo_Ancho_Alto * 2, Máximo_Ancho_Alto);
                            Pincel_Lluvia.Dispose();
                            Pincel_Lluvia = null;
                            */
                            Imagen_Lluvia.Dispose();
                            Imagen_Lluvia = null;
                        }
                        else if (Clima == Climas.Snow)
                        {
                            Bitmap Imagen_Nieve = Resources.Nieve;

                            int Divisor_Nieve = 30; // Higher values means slower snow.

                            TextureBrush Pincel_Nieve = new TextureBrush(Imagen_Nieve, WrapMode.Tile, new Rectangle(63 - ((Ticks / Divisor_Nieve) % 63), 255 - ((Ticks / Divisor_Nieve) % 256), 64, 256));
                            Pintar.FillRectangle(Pincel_Nieve, -Máximo_Ancho_Alto, -Máximo_Ancho_Alto, Máximo_Ancho_Alto * 2, Máximo_Ancho_Alto * 2);
                            Pincel_Nieve.Dispose();
                            Pincel_Nieve = null;

                            Imagen_Nieve.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            Pincel_Nieve = new TextureBrush(Imagen_Nieve, WrapMode.Tile, new Rectangle(255 - ((Ticks / Divisor_Nieve) % 256), 63 - ((Ticks / Divisor_Nieve) % 63), 256, 64));
                            Pintar.FillRectangle(Pincel_Nieve, -Máximo_Ancho_Alto, -Máximo_Ancho_Alto, Máximo_Ancho_Alto * 2, Máximo_Ancho_Alto * 2);
                            Pincel_Nieve.Dispose();
                            Pincel_Nieve = null;

                            Imagen_Nieve.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            Pincel_Nieve = new TextureBrush(Imagen_Nieve, WrapMode.Tile, new Rectangle(((Ticks / Divisor_Nieve) % 63), 255 - ((Ticks / Divisor_Nieve) % 256), 64, 256));
                            Pintar.FillRectangle(Pincel_Nieve, -Máximo_Ancho_Alto, -Máximo_Ancho_Alto, Máximo_Ancho_Alto * 2, Máximo_Ancho_Alto * 2);
                            Pincel_Nieve.Dispose();
                            Pincel_Nieve = null;

                            Imagen_Nieve.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            Pincel_Nieve = new TextureBrush(Imagen_Nieve, WrapMode.Tile, new Rectangle(((Ticks / Divisor_Nieve) % 256), 63 - ((Ticks / Divisor_Nieve) % 63), 256, 64));
                            Pintar.FillRectangle(Pincel_Nieve, -Máximo_Ancho_Alto, -Máximo_Ancho_Alto, Máximo_Ancho_Alto * 2, Máximo_Ancho_Alto * 2);
                            Pincel_Nieve.Dispose();
                            Pincel_Nieve = null;

                            Imagen_Nieve.Dispose();
                            Imagen_Nieve = null;
                        }
                        else if (Clima == Climas.Thunder)
                        {
                            Bitmap Imagen_Lluvia = Resources.Lluvia;

                            int Divisor_Lluvia = 2; // Higher values means slower rain.
                            int Divisor_Lluvia_Horizontal = 50; // Higher values means slower rain.

                            TextureBrush Pincel_Lluvia = new TextureBrush(Imagen_Lluvia, WrapMode.Tile, new Rectangle(63 - ((Ticks / Divisor_Lluvia_Horizontal) % 63), 255 - ((Ticks / 1) % 256), 64, 256));
                            Pintar.FillRectangle(Pincel_Lluvia, -Máximo_Ancho_Alto, -Máximo_Ancho_Alto, Máximo_Ancho_Alto * 2, Máximo_Ancho_Alto * 2);
                            Pincel_Lluvia.Dispose();
                            Pincel_Lluvia = null;

                            int Lluvia_X = (63 - ((Ticks / Divisor_Lluvia_Horizontal) % 63)) + 16;
                            int Lluvia_Y = (255 - ((Ticks / (Divisor_Lluvia - 1)) % 256)) + 64;
                            if (Lluvia_X >= 64) Lluvia_X -= 64;
                            if (Lluvia_Y >= 256) Lluvia_Y -= 256;
                            Imagen_Lluvia.RotateFlip(RotateFlipType.RotateNoneFlipY); // Invert Y
                            Pincel_Lluvia = new TextureBrush(Imagen_Lluvia, WrapMode.Tile, new Rectangle(Lluvia_X, Lluvia_Y, 64, 256));
                            Pintar.FillRectangle(Pincel_Lluvia, -Máximo_Ancho_Alto, -Máximo_Ancho_Alto, Máximo_Ancho_Alto * 2, Máximo_Ancho_Alto * 2);
                            Pincel_Lluvia.Dispose();
                            Pincel_Lluvia = null;

                            Lluvia_X = (63 - ((Ticks / Divisor_Lluvia_Horizontal) % 63)) + 32;
                            Lluvia_Y = (255 - ((Ticks / (Divisor_Lluvia - 1)) % 256)) + 128;
                            if (Lluvia_X >= 64) Lluvia_X -= 64;
                            if (Lluvia_Y >= 256) Lluvia_Y -= 256;
                            Imagen_Lluvia.RotateFlip(RotateFlipType.RotateNoneFlipY); // Invert none
                            Pincel_Lluvia = new TextureBrush(Imagen_Lluvia, WrapMode.Tile, new Rectangle(Lluvia_X, Lluvia_Y, 64, 256));
                            Pintar.FillRectangle(Pincel_Lluvia, -Máximo_Ancho_Alto, -Máximo_Ancho_Alto, Máximo_Ancho_Alto * 2, Máximo_Ancho_Alto * 2);
                            Pincel_Lluvia.Dispose();
                            Pincel_Lluvia = null;

                            Lluvia_X = (63 - ((Ticks / Divisor_Lluvia_Horizontal) % 63)) + 48;
                            Lluvia_Y = (255 - ((Ticks / (Divisor_Lluvia)) % 256)) + 192;
                            if (Lluvia_X >= 64) Lluvia_X -= 64;
                            if (Lluvia_Y >= 256) Lluvia_Y -= 256;
                            Imagen_Lluvia.RotateFlip(RotateFlipType.RotateNoneFlipY); // Invert Y
                            Pincel_Lluvia = new TextureBrush(Imagen_Lluvia, WrapMode.Tile, new Rectangle(Lluvia_X, Lluvia_Y, 64, 256));
                            Pintar.FillRectangle(Pincel_Lluvia, -Máximo_Ancho_Alto, -Máximo_Ancho_Alto, Máximo_Ancho_Alto * 2, Máximo_Ancho_Alto * 2);
                            Pincel_Lluvia.Dispose();
                            Pincel_Lluvia = null;

                            Imagen_Lluvia.Dispose();
                            Imagen_Lluvia = null;

                            if (Program.Rand.Next(0, 100) < 1) // Generate a random lightning at 1 % of drawings
                            {
                                double Porcentaje_Volumen = 25d + (Program.Rand.NextDouble() * 75d);
                                double Ángulo_Rayo = (Program.Rand.NextDouble() * 360d) - 90d;
                                if (Ángulo_Rayo < 0d) Ángulo_Rayo += 360;
                                if (Variable_Reproducir_Sonidos_Efectos_Climáticos)
                                {
                                    Reproductor = Reproductor_Sonidos.Cargar_Sonido("Ambient\\Weather\\thunder" + Program.Rand.Next(1, 4), 100d, Porcentaje_Volumen, Ángulo_Rayo);
                                    if (Reproductor != null) Reproductor.Play();
                                }
                                // The gradient isn't properly finished or centered, but it should work fine for now:
                                LinearGradientBrush Pincel_Rayo = new LinearGradientBrush(new Rectangle(-Ancho_Cliente, -Alto_Cliente, Ancho_Cliente * 2, Alto_Cliente * 2), Color.White, Color.Transparent, (float)Ángulo_Rayo);
                                Pintar.FillRectangle(Pincel_Rayo, -Ancho_Cliente, -Alto_Cliente, Ancho_Cliente * 2, Alto_Cliente * 2);
                                Pincel_Rayo.Dispose();
                                Pincel_Rayo = null;
                            }
                        }

                        /*Pintar.CompositingMode = CompositingMode.SourceOver;
                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar.SmoothingMode = SmoothingMode.None;*/
                        //Pintar.CompositingQuality = CompositingQuality.Default;
                        //Pintar.PixelOffsetMode = PixelOffsetMode.Default;
                    }

                    if (Pendiente_Generar_Rayo) // Force a new random lightning just for fun.
                    {
                        Pintar.ResetTransform();
                        Pendiente_Generar_Rayo = false;
                        double Porcentaje_Volumen = 25d + (Program.Rand.NextDouble() * 75d);
                        double Ángulo_Rayo = Pendiente_Generar_Rayo_Ángulo > -1d ? Pendiente_Generar_Rayo_Ángulo - 90d : (Program.Rand.NextDouble() * 360d) - 90d;
                        Pendiente_Generar_Rayo_Ángulo = -1d;

                        //Pintar.FillPie(Brushes.Red, (Ancho_Cliente / 2) - 200, (Alto_Cliente / 2) - 200, 400, 400, (float)(Ángulo_Rayo - 1d), 2f); // Debug
                        //this.Text = Ángulo_Rayo.ToString(); // Debug
                        Ángulo_Rayo = (360d - Ángulo_Rayo) - 180d;
                        while (Ángulo_Rayo < 0d) Ángulo_Rayo += 360;
                        while (Ángulo_Rayo >= 360d) Ángulo_Rayo -= 360;
                        if (Variable_Reproducir_Sonidos_Efectos_Climáticos)
                        {
                            Reproductor = Reproductor_Sonidos.Cargar_Sonido("Ambient\\Weather\\thunder" + Program.Rand.Next(1, 4), 100d, Porcentaje_Volumen, Ángulo_Rayo);
                            if (Reproductor != null) Reproductor.Play();
                        }
                        // The gradient isn't properly finished or centered, but it should work fine for now:
                        LinearGradientBrush Pincel_Rayo = new LinearGradientBrush(new Rectangle(-Ancho_Cliente, -Alto_Cliente, Ancho_Cliente * 2, Alto_Cliente * 2), Color.White, Color.Transparent, (float)(360d - Ángulo_Rayo));
                        Pintar.FillRectangle(Pincel_Rayo, -Ancho_Cliente, -Alto_Cliente, Ancho_Cliente * 2, Alto_Cliente * 2);
                        Pincel_Rayo.Dispose();
                        Pincel_Rayo = null;
                    }

                    //Pintar.SmoothingMode = SmoothingMode.None;

                    /*// Generate the moving clouds in real time.
                    Bitmap Imagen_Nubes = new Bitmap(256, 256, PixelFormat.Format32bppArgb);
                    Graphics Pintar_Nubes = Graphics.FromImage(Imagen_Nubes);
                    Pintar_Nubes.CompositingMode = CompositingMode.SourceCopy;
                    int Divisor_Nubes = 1; // Increase to slow the clouds
                    int Ancho_Nubes = (Gametime / Divisor_Nubes) % 256;
                    Pintar_Nubes.DrawImage(Resources.Nubes, new Rectangle(0, 0, 256 - Ancho_Nubes, 256), new Rectangle(Ancho_Nubes, 0, 256 - Ancho_Nubes, 256), GraphicsUnit.Pixel);
                    Pintar_Nubes.DrawImage(Resources.Nubes, new Rectangle(256 - Ancho_Nubes, 0, Ancho_Nubes, 256), new Rectangle(0, 0, Ancho_Nubes, 256), GraphicsUnit.Pixel);
                    Pintar_Nubes.Dispose();
                    Pintar_Nubes = null;

                    /*TextureBrush Pincel_Nubes = new TextureBrush(Imagen_Nubes, WrapMode.Tile);
                    TextureBrush Pincel = new TextureBrush((Bitmap)Resources.Animación_Agua.Clone(new Rectangle(0, Frametime * 16, 16, 16), Imagen.PixelFormat), WrapMode.Tile);
                    TextureBrush Pincel_Fluyendo = new TextureBrush((Bitmap)Resources.Animación_Agua_Fluyendo.Clone(new Rectangle(0, Frametime * 32, 32, 32), Imagen.PixelFormat), WrapMode.Tile);

                    //Pintar.FillRectangle(Pincel_Nubes, 0, (Alto / 4) - 8, Ancho, 16);
                    //Pintar.FillRectangle(Pincel, 0, Alto / 2, Ancho, 16);
                    //Pintar.FillRectangle(Pincel_Fluyendo, 0, (Alto / 2) + 16, Ancho, (int)Math.Round((double)Alto / 2d, MidpointRounding.AwayFromZero) - 16);

                    Pincel_Nubes.Dispose();
                    Pincel_Nubes = null;
                    Pincel_Fluyendo.Dispose();
                    Pincel_Fluyendo = null;
                    Pincel.Dispose();
                    Pincel = null;*/
                    Pintar.Dispose();
                    Pintar = null;
                    Picture.Image = Imagen;
                    Picture.Refresh();
                    //Gametick_Anterior = Gametick;
                    /*Gametime++;
                    //if (Gametime % 360 == 0 && Gametime > 0) //Gametime >= 18000)
                    if (Gametime >= 18000)
                    {
                        Gametime -= 18000;
                        //Gametime -= 360;
                        Gameday++;
                        if (Gameday >= 8) Gameday = 0;
                    }
                    //this.Text = Gametime.ToString();*/

                    /*if (Variable_doDaylightCycle) // Auto increase the gametick for the next drawing.
                    {
                        long Milisegundos = Cronómetro_Gametick.ElapsedMilliseconds;
                        if (Milisegundos_Anterior_Cronómetro_Gametick > -1L)
                        {

                        }
                        //else // Starting now the counter.
                        {
                            Milisegundos_Anterior_Cronómetro_Gametick = Milisegundos;
                        }


                    }*/

                    // Chances for slimes to spawn in swamps for every moon phase (Minecraft 1.5.2).
                    //float[] spawnChances = new float[] { 1.0F, 0.75F, 0.5F, 0.25F, 0.0F, 0.25F, 0.5F, 0.75F };

                    string Texto_Fase_Lunar = null;
                    if (Fase_Lunar == 0) Texto_Fase_Lunar = "Full moon";
                    else if (Fase_Lunar == 1) Texto_Fase_Lunar = "Waning gibbous";
                    else if (Fase_Lunar == 2) Texto_Fase_Lunar = "Last quarter";
                    else if (Fase_Lunar == 3) Texto_Fase_Lunar = "Waning crescent";
                    else if (Fase_Lunar == 4) Texto_Fase_Lunar = "New moon";
                    else if (Fase_Lunar == 5) Texto_Fase_Lunar = "Waxing crescent";
                    else if (Fase_Lunar == 6) Texto_Fase_Lunar = "First quarter";
                    else if (Fase_Lunar == 7) Texto_Fase_Lunar = "Waxing gibbous";
                    Barra_Estado_Etiqueta_Fase_Lunar.Text = "Moon phase: " + Texto_Fase_Lunar;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Tickspeed_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    ComboBox_Tickspeed.SelectedIndex = ComboBox_Tickspeed.Items.IndexOf((Variable_Tickspeed * -1L).ToString() + "x");
                    ComboBox_Tickspeed.Select();
                    ComboBox_Tickspeed.Focus();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left) CheckBox_doDaylightCycle.Checked = !CheckBox_doDaylightCycle.Checked;
                else if (e.Button == MouseButtons.Middle)
                {
                    ComboBox_Tickspeed.SelectedIndex = ComboBox_Tickspeed.Items.IndexOf((Variable_Tickspeed * -1L).ToString() + "x");
                    ComboBox_Tickspeed.Select();
                    ComboBox_Tickspeed.Focus();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Clima_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Clima.SelectedIndex > -1)
                {
                    Variable_Clima = (Climas)ComboBox_Clima.SelectedIndex;
                    Registro_Guardar_Opciones();
                    Actualizar_Clima();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Enumeration to identify the different weather effects.
        /// </summary>
        internal enum Climas : int
        {
            Clear = 0,
            Rain,
            Snow,
            Thunder,
            Automatic
        }

        /// <summary>
        /// Dinamically changes the weather based on the selected preset.
        /// </summary>
        private void Actualizar_Clima()
        {
            try
            {
                if (Variable_Clima == Climas.Automatic)
                {
                    List<Climas> Lista_Climas = new List<Climas>(new Climas[] { Climas.Clear, Climas.Clear, Climas.Clear, Climas.Rain, Climas.Snow, Climas.Thunder });
                    //Lista_Climas.Remove(Variable_Clima);
                    Variable_Clima_Automático = Lista_Climas[Program.Rand.Next(0, Lista_Climas.Count)];
                    Lista_Climas = null;
                    Variable_Clima_Ticks = (300 + Program.Rand.Next(600)) * 20; // Between 5 and 15 minutes of that weather (source code adapted from Minecraft 1.5.2).
                    //Variable_Clima_Ticks = (30 + Program.Rand.Next(60)) * 20; // Between 5 and 15 minutes of that weather (source code adapted from Minecraft 1.5.2). // Testing.
                    if (Variable_Clima_Automático == Climas.Clear) Barra_Estado_Etiqueta_Clima.Image = Program.Obtener_Imagen_Miniatura(Resources.Sol.Clone(new Rectangle(12, 12, 8, 8), PixelFormat.Format32bppArgb), 16, 16, true, false, CheckState.Checked);
                    else if (Variable_Clima_Automático == Climas.Rain) Barra_Estado_Etiqueta_Clima.Image = Resources.minecraft_flowing_water;
                    else if (Variable_Clima_Automático == Climas.Snow) Barra_Estado_Etiqueta_Clima.Image = Resources.minecraft_snow_block;
                    else if (Variable_Clima_Automático == Climas.Thunder) Barra_Estado_Etiqueta_Clima.Image = Resources.minecraft_fire;
                    Barra_Estado_Etiqueta_Clima.Text = "Weather: " + Variable_Clima_Automático.ToString().ToLowerInvariant() + " (" + Traducir_Tick_Intervalo(Variable_Clima_Ticks) + ")";
                }
                else
                {
                    if (Variable_Clima == Climas.Clear) Barra_Estado_Etiqueta_Clima.Image = Program.Obtener_Imagen_Miniatura(Resources.Sol.Clone(new Rectangle(12, 12, 8, 8), PixelFormat.Format32bppArgb), 16, 16, true, false, CheckState.Checked);
                    else if (Variable_Clima == Climas.Rain) Barra_Estado_Etiqueta_Clima.Image = Resources.minecraft_flowing_water;
                    else if (Variable_Clima == Climas.Snow) Barra_Estado_Etiqueta_Clima.Image = Resources.minecraft_snow_block;
                    else if (Variable_Clima == Climas.Thunder) Barra_Estado_Etiqueta_Clima.Image = Resources.minecraft_fire;
                    Barra_Estado_Etiqueta_Clima.Text = "Weather: " + Variable_Clima.ToString().ToLowerInvariant();
                    Variable_Clima_Ticks = int.MaxValue;
                }
                if (!Variable_doDaylightCycle || Variable_Tickspeed == 0L) Actualizar_Ciclo_Día_Noche(Variable_Gametick);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.Real_time_Minecraft_day_night_clock;
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
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes);
                Program.Ejecutar_Ruta(Program.Ruta_Guardado_Imágenes, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Variable_doDaylightCycle || Variable_Tickspeed == 0L) Actualizar_Ciclo_Día_Noche(Variable_Gametick);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Dibujar_Cielo_Azul_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Dibujar_Cielo_Azul = Menú_Contextual_Dibujar_Cielo_Azul.Checked;
                Registro_Guardar_Opciones();
                if (!Variable_doDaylightCycle || Variable_Tickspeed == 0L) Actualizar_Ciclo_Día_Noche(Variable_Gametick);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Dibujar_Tierra_Sol_Luna_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Dibujar_Tierra_Sol_Luna = Menú_Contextual_Dibujar_Tierra_Sol_Luna.Checked;
                Registro_Guardar_Opciones();
                if (!Variable_doDaylightCycle || Variable_Tickspeed == 0L) Actualizar_Ciclo_Día_Noche(Variable_Gametick);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Dibujar_Efectos_Climáticos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Dibujar_Efectos_Climáticos = Menú_Contextual_Dibujar_Efectos_Climáticos.Checked;
                Registro_Guardar_Opciones();
                if (!Variable_doDaylightCycle || Variable_Tickspeed == 0L) Actualizar_Ciclo_Día_Noche(Variable_Gametick);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Rotar_Cielo_Azul_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Rotar_Cielo_Azul = Menú_Contextual_Rotar_Cielo_Azul.Checked;
                Registro_Guardar_Opciones();
                if (!Variable_doDaylightCycle || Variable_Tickspeed == 0L) Actualizar_Ciclo_Día_Noche(Variable_Gametick);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Rotar_Tierra_Sol_Luna_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Rotar_Tierra_Sol_Luna = Menú_Contextual_Rotar_Tierra_Sol_Luna.Checked;
                Registro_Guardar_Opciones();
                if (!Variable_doDaylightCycle || Variable_Tickspeed == 0L) Actualizar_Ciclo_Día_Noche(Variable_Gametick);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Rotar_Efectos_Climáticos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Rotar_Efectos_Climáticos = Menú_Contextual_Rotar_Efectos_Climáticos.Checked;
                Registro_Guardar_Opciones();
                if (!Variable_doDaylightCycle || Variable_Tickspeed == 0L) Actualizar_Ciclo_Día_Noche(Variable_Gametick);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Oscurecer_Cielo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Oscurecer_Cielo = Menú_Contextual_Oscurecer_Cielo.Checked;
                Registro_Guardar_Opciones();
                if (!Variable_doDaylightCycle || Variable_Tickspeed == 0L) Actualizar_Ciclo_Día_Noche(Variable_Gametick);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Reproducir_Sonidos_Efectos_Climáticos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Reproducir_Sonidos_Efectos_Climáticos = Menú_Contextual_Reproducir_Sonidos_Efectos_Climáticos.Checked;
                Registro_Guardar_Opciones();
                if (!Variable_doDaylightCycle || Variable_Tickspeed == 0L) Actualizar_Ciclo_Día_Noche(Variable_Gametick);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
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
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Reloj_Minecraft_Tiempo_Real);
                    Picture.Image.Save(Program.Ruta_Guardado_Imágenes_Reloj_Minecraft_Tiempo_Real + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " Tick " + Variable_Gametick + ".png", ImageFormat.Png);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Registro_Cargar_Opciones()
        {
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Real Time Minecraft Clock");

                try { Variable_doDaylightCycle = bool.Parse((string)Clave.GetValue("doDaylightCycle", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_doDaylightCycle = true; }
                try { Variable_Tickspeed = (int)Clave.GetValue("Tickspeed", 1); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Tickspeed = 1; }
                try { Variable_Clima = (Climas)Clave.GetValue("Weather", (int)Climas.Clear); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Clima = Climas.Clear; }

                try { Variable_Dibujar_Cielo_Azul = bool.Parse((string)Clave.GetValue("Draw_Blue_Sky", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Dibujar_Cielo_Azul = true; }
                try { Variable_Dibujar_Tierra_Sol_Luna = bool.Parse((string)Clave.GetValue("Draw_Earth_Sun_Moon", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Dibujar_Tierra_Sol_Luna = true; }
                try { Variable_Dibujar_Efectos_Climáticos = bool.Parse((string)Clave.GetValue("Draw_Weather_Effects", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Dibujar_Efectos_Climáticos = true; }

                try { Variable_Rotar_Cielo_Azul = bool.Parse((string)Clave.GetValue("Rotate_Blue_Sky", bool.FalseString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Rotar_Cielo_Azul = false; }
                try { Variable_Rotar_Tierra_Sol_Luna = bool.Parse((string)Clave.GetValue("Rotate_Earth_Sun_Moon", bool.FalseString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Rotar_Tierra_Sol_Luna = false; }
                try { Variable_Rotar_Efectos_Climáticos = bool.Parse((string)Clave.GetValue("Rotate_Weather_Effects", bool.FalseString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Rotar_Efectos_Climáticos = false; }
                try { Variable_Oscurecer_Cielo = bool.Parse((string)Clave.GetValue("Darken_Blue_Sky", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Oscurecer_Cielo = true; }

                try { Variable_Reproducir_Sonidos_Efectos_Climáticos = bool.Parse((string)Clave.GetValue("Play_Weather_Effects_Sounds", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Reproducir_Sonidos_Efectos_Climáticos = true; }

                // Correct any bad value after loading:
                if (!ComboBox_Tickspeed.Items.Contains(Variable_Tickspeed.ToString() + "x")) Variable_Tickspeed = 1L;
                if ((int)Variable_Clima < 0 || (int)Variable_Clima > (int)Climas.Automatic) Variable_Clima = Climas.Clear;

                // Apply all the loaded values:
                CheckBox_doDaylightCycle.Checked = Variable_doDaylightCycle;
                ComboBox_Tickspeed.SelectedIndex = ComboBox_Tickspeed.Items.IndexOf(Variable_Tickspeed.ToString() + "x");
                ComboBox_Clima.SelectedIndex = (int)Variable_Clima;

                Menú_Contextual_Dibujar_Cielo_Azul.Checked = Variable_Dibujar_Cielo_Azul;
                Menú_Contextual_Dibujar_Tierra_Sol_Luna.Checked = Variable_Dibujar_Tierra_Sol_Luna;
                Menú_Contextual_Dibujar_Efectos_Climáticos.Checked = Variable_Dibujar_Efectos_Climáticos;

                Menú_Contextual_Rotar_Cielo_Azul.Checked = Variable_Rotar_Cielo_Azul;
                Menú_Contextual_Rotar_Tierra_Sol_Luna.Checked = Variable_Rotar_Tierra_Sol_Luna;
                Menú_Contextual_Rotar_Efectos_Climáticos.Checked = Variable_Rotar_Efectos_Climáticos;
                Menú_Contextual_Oscurecer_Cielo.Checked = Variable_Oscurecer_Cielo;

                Menú_Contextual_Reproducir_Sonidos_Efectos_Climáticos.Checked = Variable_Reproducir_Sonidos_Efectos_Climáticos;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Registro_Guardar_Opciones()
        {
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Real Time Minecraft Clock");
                string[] Matriz_Nombres = Clave.GetValueNames();
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                    {
                        Clave.DeleteValue(Matriz_Nombres[Índice]);
                    }
                }
                Matriz_Nombres = null;

                try { Clave.SetValue("doDaylightCycle", Variable_doDaylightCycle.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                try { Clave.SetValue("Tickspeed", (int)Variable_Tickspeed, RegistryValueKind.DWord); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                try { Clave.SetValue("Weather", (int)Variable_Clima, RegistryValueKind.DWord); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                try { Clave.SetValue("Draw_Blue_Sky", Variable_Dibujar_Cielo_Azul.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                try { Clave.SetValue("Draw_Earth_Sun_Moon", Variable_Dibujar_Tierra_Sol_Luna.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                try { Clave.SetValue("Draw_Weather_Effects", Variable_Dibujar_Efectos_Climáticos.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                try { Clave.SetValue("Rotate_Blue_Sky", Variable_Rotar_Cielo_Azul.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                try { Clave.SetValue("Rotate_Earth_Sun_Moon", Variable_Rotar_Tierra_Sol_Luna.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                try { Clave.SetValue("Rotate_Weather_Effects", Variable_Rotar_Efectos_Climáticos.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                try { Clave.SetValue("Darken_Blue_Sky", Variable_Oscurecer_Cielo.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                try { Clave.SetValue("Play_Weather_Effects_Sounds", Variable_Reproducir_Sonidos_Efectos_Climáticos.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Registro_Restablecer_Opciones()
        {
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Real Time Minecraft Clock");
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
                Clave = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Clima_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    ComboBox_Clima.SelectedIndex = Program.Rand.Next(0, ComboBox_Clima.Items.Count);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1.Refresh();
            Pendiente_Generar_Rayo_Ángulo = (double)numericUpDown1.Value;
            Pendiente_Generar_Rayo = true;
            if (!Variable_doDaylightCycle || Variable_Tickspeed == 0L) Actualizar_Ciclo_Día_Noche(Variable_Gametick);
        }
    }
}
