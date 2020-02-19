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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Comparador_Imágenes : Form
    {
        public Ventana_Comparador_Imágenes()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Image Comparator by Jupisoft for " + Program.Texto_Usuario;
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

        private void Ventana_Comparador_Imágenes_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                //this.WindowState = FormWindowState.Maximized;
                GroupBox_Imagen_1.AllowDrop = true;
                GroupBox_Imagen_2.AllowDrop = true;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Comparador_Imágenes_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
                /*// This tool was actually born to check if the textures I used for my new Mars resource
                // pack were the same on Minecraft 1.14.4 and 1.15.1, so this is the test function I
                // used to automatically check everything, just ignore it and enjoy the rest of the tool.
                string Ruta_Base = @"C:\Users\Jupisoft\AppData\Roaming\.minecraft\resourcepacks\_MARS\Jupisoft Mars Full Pack [Vanilla] [1.14+]\assets\minecraft";
                string Ruta_1_14_4 = @"C:\Users\Jupisoft\AppData\Roaming\.minecraft\resourcepacks\Jupisoft Planet Mars [1.13+]\assets\minecraft\textures orig\1.14.4\assets\minecraft";
                string Ruta_1_15_1 = @"C:\Users\Jupisoft\AppData\Roaming\.minecraft\resourcepacks\Jupisoft Planet Mars [1.13+]\assets\minecraft\textures orig\1.15.1\assets\minecraft";
                string[] Matriz_Rutas_Base = Directory.GetFiles(Ruta_Base, "*.png", SearchOption.AllDirectories);
                if (Matriz_Rutas_Base != null && Matriz_Rutas_Base.Length > 0)
                {
                    string Texto_Iguales = null;
                    string Texto_Diferentes = null;
                    string Texto_Inválidos = null;
                    if (Matriz_Rutas_Base.Length > 1) Array.Sort(Matriz_Rutas_Base);
                    int Longitud_Base = Ruta_Base.Length + 1;
                    foreach (string Ruta in Matriz_Rutas_Base)
                    {
                        try
                        {
                            string Ruta_1 = Ruta_1_14_4 + "\\" + Ruta.Substring(Longitud_Base);
                            string Ruta_2 = Ruta_1_15_1 + "\\" + Ruta.Substring(Longitud_Base);
                            if (File.Exists(Ruta_1) && File.Exists(Ruta_2))
                            {
                                double Porcentaje_Iguales = Program.Comparar_Píxeles_Imágenes(Ruta_1, Ruta_2);
                                if (Porcentaje_Iguales >= 100d)
                                {
                                    Texto_Iguales += Ruta + "\r\n";
                                }
                                else
                                {
                                    Texto_Diferentes += Ruta + "\r\n";
                                }
                            }
                            else
                            {
                                Texto_Inválidos += Ruta + "\r\n";
                            }
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                    }
                    Matriz_Rutas_Base = null;
                    Clipboard.SetText("[Iguales]\r\n" + Texto_Iguales + "\r\n\r\n[Diferentes]\r\n" + Texto_Diferentes + "\r\n\r\n[Inválidos]\r\n" + Texto_Inválidos);
                }*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Comparador_Imágenes_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Comparador_Imágenes_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Comparador_Imágenes_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Comparador_Imágenes_KeyDown(object sender, KeyEventArgs e)
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

        private void GroupBox_Imagen_1_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void GroupBox_Imagen_1_DragDrop(object sender, DragEventArgs e)
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
                                if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                                {
                                    TextBox_Ruta_1.Text = Ruta;
                                    TextBox_Ruta_1.SelectionLength = 0;
                                    TextBox_Ruta_1.SelectionStart = TextBox_Ruta_1.TextLength;
                                    if (CheckBox_Automático.Checked) Botón_Comparar.PerformClick();
                                    break;
                                }
                            }
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                        }
                        Matriz_Rutas = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void GroupBox_Imagen_2_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void GroupBox_Imagen_2_DragDrop(object sender, DragEventArgs e)
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
                                if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                                {
                                    TextBox_Ruta_2.Text = Ruta;
                                    TextBox_Ruta_2.SelectionLength = 0;
                                    TextBox_Ruta_2.SelectionStart = TextBox_Ruta_2.TextLength;
                                    if (CheckBox_Automático.Checked) Botón_Comparar.PerformClick();
                                    break;
                                }
                            }
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                        }
                        Matriz_Rutas = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Comparar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                GroupBox_Resultado.Text = "Result of the comparison";
                Picture_Resultado.Image = Resources.Aleatorio;
                TextBox_Resultado.Text = "Comparison in progress...";
                this.Refresh();
                double Porcentaje_Iguales = Program.Comparar_Píxeles_Imágenes(TextBox_Ruta_1.Text, TextBox_Ruta_2.Text);
                double Porcentaje_Diferentes = 100d - Porcentaje_Iguales;
                Picture_1.Image = Program.Cargar_Imagen_Ruta(TextBox_Ruta_1.Text, CheckState.Checked);
                Picture_2.Image = Program.Cargar_Imagen_Ruta(TextBox_Ruta_2.Text, CheckState.Checked);
                if (Porcentaje_Iguales < 0d)
                {
                    GroupBox_Resultado.Text = "Result of the comparison - [DIFFERENT]";
                    Picture_Resultado.Image = Resources.Cancelar;
                    TextBox_Resultado.Text = "One or more file path or image is invalid or empty, so they are different.";
                }
                else if (Porcentaje_Iguales >= 100d) // 100 % equals.
                {
                    GroupBox_Resultado.Text = "Result of the comparison - [EQUAL]";
                    Picture_Resultado.Image = Resources.Aceptar;
                    if (string.Compare(TextBox_Ruta_1.Text, TextBox_Ruta_2.Text, true) != 0)
                    {
                        TextBox_Resultado.Text = "The images are equal by " + Program.Traducir_Número_Decimales_Redondear(Porcentaje_Iguales, 4) + " %, so they are the same one.";
                    }
                    else
                    {
                        TextBox_Resultado.Text = "Both image file paths are the same, so they are the same one.";
                    }
                }
                else if (Porcentaje_Iguales >= 50d) // Mostly equal.
                {
                    GroupBox_Resultado.Text = "Result of the comparison - [DIFFERENT]";
                    Picture_Resultado.Image = Resources.Agregar;
                    TextBox_Resultado.Text = "The images are equal by " + Program.Traducir_Número_Decimales_Redondear(Porcentaje_Iguales, 4) + " % and different by " + Program.Traducir_Número_Decimales_Redondear(Porcentaje_Diferentes, 4) + " %, so they are different.";
                }
                else if (Porcentaje_Iguales > 0d) // Mostly different.
                {
                    GroupBox_Resultado.Text = "Result of the comparison - [DIFFERENT]";
                    Picture_Resultado.Image = Resources.Quitar;
                    TextBox_Resultado.Text = "The images are different by " + Program.Traducir_Número_Decimales_Redondear(Porcentaje_Diferentes, 4) + " % and equal by " + Program.Traducir_Número_Decimales_Redondear(Porcentaje_Iguales, 4) + " %, so they are different.";
                }
                else // 100 % different.
                {
                    GroupBox_Resultado.Text = "Result of the comparison - [DIFFERENT]";
                    Picture_Resultado.Image = Resources.Cancelar;
                    TextBox_Resultado.Text = "The images are different by " + Program.Traducir_Número_Decimales_Redondear(Porcentaje_Diferentes, 4) + " %, so they are different.";
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
