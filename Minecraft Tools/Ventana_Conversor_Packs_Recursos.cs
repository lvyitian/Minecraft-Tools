using ICSharpCode.SharpZipLib.Zip;
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
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Conversor_Packs_Recursos : Form
    {
        public Ventana_Conversor_Packs_Recursos()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Resource Packs Converter by Jupisoft for " + Program.Texto_Usuario;
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
        internal Bitmap Imagen_Pack = null;

        // TODO: tweak all images that might need it between pack versions, like resizing them.

        private void Ventana_Conversor_Packs_Recursos_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título + " - [Drag and drop any resource pack as a zip file or as a folder]";
                this.WindowState = FormWindowState.Maximized;
                ComboBox_Pack.SelectedIndex = 3;
                NumericUpDown_Información_Formato_Pack.Minimum = int.MinValue;
                NumericUpDown_Información_Formato_Pack.Maximum = int.MaxValue;
                NumericUpDown_Información_Dimensiones.Minimum = int.MinValue;
                NumericUpDown_Información_Dimensiones.Maximum = int.MaxValue;
                NumericUpDown_Información_Total_Carpetas.Minimum = int.MinValue;
                NumericUpDown_Información_Total_Carpetas.Maximum = int.MaxValue;
                NumericUpDown_Información_Total_Archivos.Minimum = int.MinValue;
                NumericUpDown_Información_Total_Archivos.Maximum = int.MaxValue;
                NumericUpDown_Información_Carpetas_Desconocidas.Minimum = int.MinValue;
                NumericUpDown_Información_Carpetas_Desconocidas.Maximum = int.MaxValue;
                NumericUpDown_Información_Archivos_Desconocidos.Minimum = int.MinValue;
                NumericUpDown_Información_Archivos_Desconocidos.Maximum = int.MaxValue;
                NumericUpDown_Información_Carpetas_Ignoradas.Minimum = int.MinValue;
                NumericUpDown_Información_Carpetas_Ignoradas.Maximum = int.MaxValue;
                NumericUpDown_Información_Archivos_Ignorados.Minimum = int.MinValue;
                NumericUpDown_Información_Archivos_Ignorados.Maximum = int.MaxValue;
                NumericUpDown_Información_Carpetas_Conocidas.Minimum = int.MinValue;
                NumericUpDown_Información_Carpetas_Conocidas.Maximum = int.MaxValue;
                NumericUpDown_Información_Archivos_Conocidos.Minimum = int.MinValue;
                NumericUpDown_Información_Archivos_Conocidos.Maximum = int.MaxValue;
                NumericUpDown_Información_Carpetas_Convertibles.Minimum = int.MinValue;
                NumericUpDown_Información_Carpetas_Convertibles.Maximum = int.MaxValue;
                NumericUpDown_Información_Archivos_Convertibles.Minimum = int.MinValue;
                NumericUpDown_Información_Archivos_Convertibles.Maximum = int.MaxValue;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
                if (string.Compare(Environment.UserName, "Jupisoft", true) == 0)
                {
                    Botón_Buscar_Nombres.Enabled = true; // Only for self-programming.
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Packs_Recursos_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Packs_Recursos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Packs_Recursos_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Packs_Recursos_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Conversor_Packs_Recursos_DragDrop(object sender, DragEventArgs e)
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
                                    TextBox_Ruta.Text = Ruta;
                                    Convertir_Pack_Recursos(Ruta, ComboBox_Pack.SelectedIndex + 1, true);
                                    break;
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

        private void Ventana_Conversor_Packs_Recursos_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
                Picture.Image = Imagen_Pack != null ? Program.Obtener_Imagen_Miniatura(Imagen_Pack, Picture.ClientSize.Width, Picture.ClientSize.Height, true, false, CheckState.Checked) : null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Packs_Recursos_KeyDown(object sender, KeyEventArgs e)
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
                Convertir_Pack_Recursos(TextBox_Ruta.Text, ComboBox_Pack.SelectedIndex + 1, true);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Imagen_Pack != null)
                {
                    Clipboard.SetImage(Imagen_Pack);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Imagen_Pack != null)
                {
                    Imagen_Pack.Save(Program.Obtener_Ruta_Temporal_Escritorio() + " Resource pack.png", ImageFormat.Png);
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

        private void Botón_Buscar_Nombres_Click(object sender, EventArgs e)
        {
            try
            {
                /*foreach (Packs_Recursos.Carpetas Carpeta in Packs_Recursos.Matriz_Carpetas_Recursos)
                {
                    foreach (Packs_Recursos.Archivos Archivo in Carpeta.Matriz_Archivos)
                    {
                        if (!string.IsNullOrEmpty(Archivo.Ruta_Pack_2))
                        {
                            MessageBox.Show(Archivo.Ruta_Pack_2, Archivo.Ruta_Pack_4);
                        }
                    }
                }
                MessageBox.Show("OK!");*/

                string Ruta_Base = Program.Ruta_Guardado_Imágenes_Secretos;
                string Ruta_Pack_1 = Ruta_Base + "\\1.8.8\\assets\\minecraft\\textures";
                string Ruta_Pack_2 = Ruta_Base + "\\1.10.2\\assets\\minecraft\\textures";
                string Ruta_Pack_3 = Ruta_Base + "\\1.12.2\\assets\\minecraft\\textures";
                string Ruta_Pack_4 = Ruta_Base + "\\1.13.2\\assets\\minecraft\\textures";
                if (!string.IsNullOrEmpty(Ruta_Pack_1) && !string.IsNullOrEmpty(Ruta_Pack_2) && !string.IsNullOrEmpty(Ruta_Pack_3) && !string.IsNullOrEmpty(Ruta_Pack_4) && Directory.Exists(Ruta_Pack_1) && Directory.Exists(Ruta_Pack_2) && Directory.Exists(Ruta_Pack_3) && Directory.Exists(Ruta_Pack_4))
                {
                    string[] Matriz_Carpetas_Pack_1 = Directory.GetDirectories(Ruta_Pack_1, "*", SearchOption.AllDirectories);
                    string[] Matriz_Carpetas_Pack_2 = Directory.GetDirectories(Ruta_Pack_2, "*", SearchOption.AllDirectories);
                    string[] Matriz_Carpetas_Pack_3 = Directory.GetDirectories(Ruta_Pack_3, "*", SearchOption.AllDirectories);
                    string[] Matriz_Carpetas_Pack_4 = Directory.GetDirectories(Ruta_Pack_4, "*", SearchOption.AllDirectories);

                    string[] Matriz_Archivos_Pack_1 = Directory.GetFiles(Ruta_Pack_1, "*", SearchOption.AllDirectories);
                    string[] Matriz_Archivos_Pack_2 = Directory.GetFiles(Ruta_Pack_2, "*", SearchOption.AllDirectories);
                    string[] Matriz_Archivos_Pack_3 = Directory.GetFiles(Ruta_Pack_3, "*", SearchOption.AllDirectories);
                    string[] Matriz_Archivos_Pack_4 = Directory.GetFiles(Ruta_Pack_4, "*", SearchOption.AllDirectories);

                    // Get the relative paths for the folders and files.
                    for (int Índice = 0; Índice < Matriz_Carpetas_Pack_1.Length; Índice++)
                    {
                        Matriz_Carpetas_Pack_1[Índice] = Matriz_Carpetas_Pack_1[Índice].Substring(Ruta_Pack_1.Length + 1);
                    }
                    for (int Índice = 0; Índice < Matriz_Carpetas_Pack_2.Length; Índice++)
                    {
                        Matriz_Carpetas_Pack_2[Índice] = Matriz_Carpetas_Pack_2[Índice].Substring(Ruta_Pack_2.Length + 1);
                    }
                    for (int Índice = 0; Índice < Matriz_Carpetas_Pack_3.Length; Índice++)
                    {
                        Matriz_Carpetas_Pack_3[Índice] = Matriz_Carpetas_Pack_3[Índice].Substring(Ruta_Pack_3.Length + 1);
                    }
                    for (int Índice = 0; Índice < Matriz_Carpetas_Pack_4.Length; Índice++)
                    {
                        Matriz_Carpetas_Pack_4[Índice] = Matriz_Carpetas_Pack_4[Índice].Substring(Ruta_Pack_4.Length + 1);
                    }

                    for (int Índice = 0; Índice < Matriz_Archivos_Pack_1.Length; Índice++)
                    {
                        Matriz_Archivos_Pack_1[Índice] = Matriz_Archivos_Pack_1[Índice].Substring(Ruta_Pack_1.Length + 1);
                    }
                    for (int Índice = 0; Índice < Matriz_Archivos_Pack_2.Length; Índice++)
                    {
                        Matriz_Archivos_Pack_2[Índice] = Matriz_Archivos_Pack_2[Índice].Substring(Ruta_Pack_2.Length + 1);
                    }
                    for (int Índice = 0; Índice < Matriz_Archivos_Pack_3.Length; Índice++)
                    {
                        Matriz_Archivos_Pack_3[Índice] = Matriz_Archivos_Pack_3[Índice].Substring(Ruta_Pack_3.Length + 1);
                    }
                    for (int Índice = 0; Índice < Matriz_Archivos_Pack_4.Length; Índice++)
                    {
                        Matriz_Archivos_Pack_4[Índice] = Matriz_Archivos_Pack_4[Índice].Substring(Ruta_Pack_4.Length + 1);
                    }

                    // Sort the arrays of folders and files.
                    if (Matriz_Carpetas_Pack_1.Length > 1) Array.Sort(Matriz_Carpetas_Pack_1);
                    if (Matriz_Carpetas_Pack_2.Length > 1) Array.Sort(Matriz_Carpetas_Pack_2);
                    if (Matriz_Carpetas_Pack_3.Length > 1) Array.Sort(Matriz_Carpetas_Pack_3);
                    if (Matriz_Carpetas_Pack_4.Length > 1) Array.Sort(Matriz_Carpetas_Pack_4);

                    if (Matriz_Archivos_Pack_1.Length > 1) Array.Sort(Matriz_Archivos_Pack_1);
                    if (Matriz_Archivos_Pack_2.Length > 1) Array.Sort(Matriz_Archivos_Pack_2);
                    if (Matriz_Archivos_Pack_3.Length > 1) Array.Sort(Matriz_Archivos_Pack_3);
                    if (Matriz_Archivos_Pack_4.Length > 1) Array.Sort(Matriz_Archivos_Pack_4);

                    FileStream Lector = new FileStream(Program.Obtener_Ruta_Temporal_Escritorio() + " Folders.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector.SetLength(0L);
                    Lector.Seek(0L, SeekOrigin.Begin);
                    StreamWriter Lector_Texto = new StreamWriter(Lector, Encoding.Default);

                    foreach (Packs_Recursos.Carpetas Carpeta in Packs_Recursos.Matriz_Carpetas)
                    {
                        Lector_Texto.WriteLine(new string(' ', 12) + "new Carpetas");
                        Lector_Texto.WriteLine(new string(' ', 12) + "(");
                        Lector_Texto.WriteLine(new string(' ', 16) + "\"" + Carpeta.Ruta_Pack_1.Replace("\\", "\\\\") + "\",");
                        Lector_Texto.WriteLine(new string(' ', 16) + "\"" + Carpeta.Ruta_Pack_2.Replace("\\", "\\\\") + "\",");
                        Lector_Texto.WriteLine(new string(' ', 16) + "\"" + Carpeta.Ruta_Pack_3.Replace("\\", "\\\\") + "\",");
                        Lector_Texto.WriteLine(new string(' ', 16) + "\"" + Carpeta.Ruta_Pack_4.Replace("\\", "\\\\") + "\",");
                        Lector_Texto.WriteLine(new string(' ', 16) + "new Archivos[]");
                        Lector_Texto.WriteLine(new string(' ', 16) + "{");
                        if (Carpeta.Matriz_Archivos != null && Carpeta.Matriz_Archivos.Length > 0)
                        {
                            foreach (Packs_Recursos.Archivos Archivo in Carpeta.Matriz_Archivos)
                            {
                                Lector_Texto.WriteLine(new string(' ', 20) + "new Archivos");
                                Lector_Texto.WriteLine(new string(' ', 20) + "(");

                                string Ruta_1 = Archivo.Ruta_Pack_1;
                                if (string.IsNullOrEmpty(Ruta_1) && !string.IsNullOrEmpty(Archivo.Ruta_Pack_2))
                                {
                                    foreach (string Ruta in Matriz_Archivos_Pack_1)
                                    {
                                        if (string.Compare(Archivo.Ruta_Pack_2, "assets\\minecraft\\" + Ruta, true) == 0)
                                        {
                                            Ruta_1 = Archivo.Ruta_Pack_2;
                                            break;
                                        }
                                    }
                                }
                                if (string.IsNullOrEmpty(Ruta_1) && !string.IsNullOrEmpty(Archivo.Ruta_Pack_3))
                                {
                                    foreach (string Ruta in Matriz_Archivos_Pack_1)
                                    {
                                        if (string.Compare(Archivo.Ruta_Pack_3, "assets\\minecraft\\" + Ruta, true) == 0)
                                        {
                                            Ruta_1 = Archivo.Ruta_Pack_3;
                                            break;
                                        }
                                    }
                                }
                                if (string.IsNullOrEmpty(Ruta_1) && !string.IsNullOrEmpty(Archivo.Ruta_Pack_4))
                                {
                                    foreach (string Ruta in Matriz_Archivos_Pack_1)
                                    {
                                        if (string.Compare(Archivo.Ruta_Pack_4, "assets\\minecraft\\" + Ruta, true) == 0)
                                        {
                                            Ruta_1 = Archivo.Ruta_Pack_4;
                                            break;
                                        }
                                    }
                                }
                                Lector_Texto.WriteLine(new string(' ', 24) + "\"" + Ruta_1.Replace("\\", "\\\\") + "\",");

                                string Ruta_2 = Archivo.Ruta_Pack_2;
                                if (string.IsNullOrEmpty(Ruta_2) && !string.IsNullOrEmpty(Archivo.Ruta_Pack_1))
                                {
                                    foreach (string Ruta in Matriz_Archivos_Pack_2)
                                    {
                                        if (string.Compare(Archivo.Ruta_Pack_1, "assets\\minecraft\\" + Ruta, true) == 0)
                                        {
                                            Ruta_2 = Archivo.Ruta_Pack_1;
                                            break;
                                        }
                                    }
                                }
                                if (string.IsNullOrEmpty(Ruta_2) && !string.IsNullOrEmpty(Archivo.Ruta_Pack_3))
                                {
                                    foreach (string Ruta in Matriz_Archivos_Pack_2)
                                    {
                                        if (string.Compare(Archivo.Ruta_Pack_3, "assets\\minecraft\\" + Ruta, true) == 0)
                                        {
                                            Ruta_2 = Archivo.Ruta_Pack_3;
                                            break;
                                        }
                                    }
                                }
                                if (string.IsNullOrEmpty(Ruta_2) && !string.IsNullOrEmpty(Archivo.Ruta_Pack_4))
                                {
                                    foreach (string Ruta in Matriz_Archivos_Pack_2)
                                    {
                                        if (string.Compare(Archivo.Ruta_Pack_4, "assets\\minecraft\\" + Ruta, true) == 0)
                                        {
                                            Ruta_2 = Archivo.Ruta_Pack_4;
                                            break;
                                        }
                                    }
                                }
                                Lector_Texto.WriteLine(new string(' ', 24) + "\"" + Ruta_2.Replace("\\", "\\\\") + "\",");

                                string Ruta_3 = Archivo.Ruta_Pack_3;
                                if (string.IsNullOrEmpty(Ruta_3) && !string.IsNullOrEmpty(Archivo.Ruta_Pack_1))
                                {
                                    foreach (string Ruta in Matriz_Archivos_Pack_3)
                                    {
                                        if (string.Compare(Archivo.Ruta_Pack_1, "assets\\minecraft\\" + Ruta, true) == 0)
                                        {
                                            Ruta_3 = Archivo.Ruta_Pack_1;
                                            break;
                                        }
                                    }
                                }
                                if (string.IsNullOrEmpty(Ruta_3) && !string.IsNullOrEmpty(Archivo.Ruta_Pack_2))
                                {
                                    foreach (string Ruta in Matriz_Archivos_Pack_3)
                                    {
                                        if (string.Compare(Archivo.Ruta_Pack_2, "assets\\minecraft\\" + Ruta, true) == 0)
                                        {
                                            Ruta_3 = Archivo.Ruta_Pack_2;
                                            break;
                                        }
                                    }
                                }
                                if (string.IsNullOrEmpty(Ruta_3) && !string.IsNullOrEmpty(Archivo.Ruta_Pack_4))
                                {
                                    foreach (string Ruta in Matriz_Archivos_Pack_3)
                                    {
                                        if (string.Compare(Archivo.Ruta_Pack_4, "assets\\minecraft\\" + Ruta, true) == 0)
                                        {
                                            Ruta_3 = Archivo.Ruta_Pack_4;
                                            break;
                                        }
                                    }
                                }
                                Lector_Texto.WriteLine(new string(' ', 24) + "\"" + Ruta_3.Replace("\\", "\\\\") + "\",");

                                Lector_Texto.WriteLine(new string(' ', 24) + "\"" + Archivo.Ruta_Pack_4.Replace("\\", "\\\\") + "\"");
                                Lector_Texto.WriteLine(new string(' ', 20) + "),");
                            }
                        }
                        else Lector_Texto.WriteLine(new string(' ', 20));
                        Lector_Texto.WriteLine(new string(' ', 16) + "}");
                        Lector_Texto.WriteLine(new string(' ', 12) + "),");
                    }

                    Lector_Texto.WriteLine();
                    Lector_Texto.WriteLine();
                    Lector_Texto.WriteLine();
                    Lector_Texto.WriteLine();
                    Lector_Texto.WriteLine();
                    Lector_Texto.WriteLine();
                    Lector_Texto.WriteLine();
                    Lector_Texto.WriteLine();
                    Lector_Texto.WriteLine();
                    Lector_Texto.WriteLine();

                    Lector_Texto.WriteLine("[MISSING PACK 1]");
                    foreach (string Ruta in Matriz_Archivos_Pack_1)
                    {
                        bool Existente = false;
                        foreach (Packs_Recursos.Carpetas Carpeta in Packs_Recursos.Matriz_Carpetas)
                        {
                            foreach (Packs_Recursos.Archivos Archivo in Carpeta.Matriz_Archivos)
                            {
                                if (string.Compare(Archivo.Ruta_Pack_1, "assets\\minecraft\\" + Ruta, true) == 0)
                                {
                                    Existente = true;
                                    break;
                                }
                            }
                            if (Existente) break;
                        }
                        if (!Existente)
                        {
                            Lector_Texto.WriteLine(new string(' ', 20) + "new Archivos");
                            Lector_Texto.WriteLine(new string(' ', 20) + "(");
                            Lector_Texto.WriteLine(new string(' ', 24) + "\"" + ("assets\\minecraft\\" + Ruta).Replace("\\", "\\\\") + "\",");
                            Lector_Texto.WriteLine(new string(' ', 24) + "\"\",");
                            Lector_Texto.WriteLine(new string(' ', 24) + "\"\",");
                            Lector_Texto.WriteLine(new string(' ', 24) + "\"\",");
                            Lector_Texto.WriteLine(new string(' ', 20) + "),");
                        }
                    }
                    Lector_Texto.WriteLine();
                    Lector_Texto.WriteLine();

                    Lector_Texto.WriteLine("[MISSING PACK 2]");
                    foreach (string Ruta in Matriz_Archivos_Pack_2)
                    {
                        bool Existente = false;
                        foreach (Packs_Recursos.Carpetas Carpeta in Packs_Recursos.Matriz_Carpetas)
                        {
                            foreach (Packs_Recursos.Archivos Archivo in Carpeta.Matriz_Archivos)
                            {
                                if (string.Compare(Archivo.Ruta_Pack_2, "assets\\minecraft\\" + Ruta, true) == 0)
                                {
                                    Existente = true;
                                    break;
                                }
                            }
                            if (Existente) break;
                        }
                        if (!Existente)
                        {
                            Lector_Texto.WriteLine(new string(' ', 20) + "new Archivos");
                            Lector_Texto.WriteLine(new string(' ', 20) + "(");
                            Lector_Texto.WriteLine(new string(' ', 24) + "\"\"");
                            Lector_Texto.WriteLine(new string(' ', 24) + "\"" + ("assets\\minecraft\\" + Ruta).Replace("\\", "\\\\") + "\",");
                            Lector_Texto.WriteLine(new string(' ', 24) + "\"\"");
                            Lector_Texto.WriteLine(new string(' ', 24) + "\"\"");
                            Lector_Texto.WriteLine(new string(' ', 20) + "),");
                        }
                    }
                    Lector_Texto.WriteLine();
                    Lector_Texto.WriteLine();

                    Lector_Texto.WriteLine("[MISSING PACK 3]");
                    foreach (string Ruta in Matriz_Archivos_Pack_3)
                    {
                        bool Existente = false;
                        foreach (Packs_Recursos.Carpetas Carpeta in Packs_Recursos.Matriz_Carpetas)
                        {
                            foreach (Packs_Recursos.Archivos Archivo in Carpeta.Matriz_Archivos)
                            {
                                if (string.Compare(Archivo.Ruta_Pack_3, "assets\\minecraft\\" + Ruta, true) == 0)
                                {
                                    Existente = true;
                                    break;
                                }
                            }
                            if (Existente) break;
                        }
                        if (!Existente)
                        {
                            Lector_Texto.WriteLine(new string(' ', 20) + "new Archivos");
                            Lector_Texto.WriteLine(new string(' ', 20) + "(");
                            Lector_Texto.WriteLine(new string(' ', 24) + "\"\",");
                            Lector_Texto.WriteLine(new string(' ', 24) + "\"\",");
                            Lector_Texto.WriteLine(new string(' ', 24) + "\"" + ("assets\\minecraft\\" + Ruta).Replace("\\", "\\\\") + "\",");
                            Lector_Texto.WriteLine(new string(' ', 24) + "\"\",");
                            Lector_Texto.WriteLine(new string(' ', 20) + "),");
                        }
                    }
                    Lector_Texto.WriteLine();
                    Lector_Texto.WriteLine();

                    Lector_Texto.WriteLine("[MISSING PACK 4]");
                    foreach (string Ruta in Matriz_Archivos_Pack_4)
                    {
                        bool Existente = false;
                        foreach (Packs_Recursos.Carpetas Carpeta in Packs_Recursos.Matriz_Carpetas)
                        {
                            foreach (Packs_Recursos.Archivos Archivo in Carpeta.Matriz_Archivos)
                            {
                                if (string.Compare(Archivo.Ruta_Pack_4, "assets\\minecraft\\" + Ruta, true) == 0)
                                {
                                    Existente = true;
                                    break;
                                }
                            }
                            if (Existente) break;
                        }
                        if (!Existente)
                        {
                            Lector_Texto.WriteLine(new string(' ', 20) + "new Archivos");
                            Lector_Texto.WriteLine(new string(' ', 20) + "(");
                            Lector_Texto.WriteLine(new string(' ', 24) + "\"\",");
                            Lector_Texto.WriteLine(new string(' ', 24) + "\"\",");
                            Lector_Texto.WriteLine(new string(' ', 24) + "\"\",");
                            Lector_Texto.WriteLine(new string(' ', 24) + "\"" + ("assets\\minecraft\\" + Ruta).Replace("\\", "\\\\") + "\",");
                            Lector_Texto.WriteLine(new string(' ', 20) + "),");
                        }
                    }
                    Lector_Texto.WriteLine();
                    Lector_Texto.WriteLine();




                    //Dictionary<string, List<string>> Diccionario_

                    /*// Just write the folder lists, ignoring the CRC values.
                    Lector_Texto.WriteLine("[FOLDERS PACK 1]");
                    foreach (string Ruta in Matriz_Carpetas_Pack_1)
                    {
                        Lector_Texto.WriteLine(Ruta.Replace("\\", "\\\\"));
                    }
                    Lector_Texto.WriteLine();
                    Lector_Texto.WriteLine();

                    Lector_Texto.WriteLine("[FOLDERS PACK 2]");
                    foreach (string Ruta in Matriz_Carpetas_Pack_2)
                    {
                        Lector_Texto.WriteLine(Ruta.Replace("\\", "\\\\"));
                    }
                    Lector_Texto.WriteLine();
                    Lector_Texto.WriteLine();

                    Lector_Texto.WriteLine("[FOLDERS PACK 3]");
                    foreach (string Ruta in Matriz_Carpetas_Pack_3)
                    {
                        Lector_Texto.WriteLine(Ruta.Replace("\\", "\\\\"));
                    }
                    Lector_Texto.WriteLine();
                    Lector_Texto.WriteLine();

                    Lector_Texto.WriteLine("[FOLDERS PACK 4]");
                    foreach (string Ruta in Matriz_Carpetas_Pack_4)
                    {
                        Lector_Texto.WriteLine(Ruta.Replace("\\", "\\\\"));
                    }
                    Lector_Texto.WriteLine();
                    Lector_Texto.WriteLine();

                    Lector_Texto.WriteLine("[{(CRCs)}]");
                    Lector_Texto.WriteLine();
                    Lector_Texto.WriteLine();

                    // Now find the CRCs of all the files at once for each folder.
                    foreach (Packs_Recursos.Carpetas Carpeta in Packs_Recursos.Matriz_Carpetas)
                    {
                        Dictionary<uint, List<string>> Diccionario_CRCs_Lista_Rutas = new Dictionary<uint, List<string>>();
                        Lector_Texto.WriteLine("[CRCs " + Carpeta.Ruta_Pack_4 + "]");
                        Lector_Texto.WriteLine();
                        Lector_Texto.WriteLine();
                        foreach (string Ruta in Matriz_Archivos_Pack_1)
                        {
                            if (string.Compare(Carpeta.Ruta_Pack_1, Path.GetDirectoryName("assets\\minecraft\\" + Ruta), true) == 0)
                            {
                                uint CRC_32 = Program.Obtener_CRC_32(Ruta_Pack_1 + "\\" + Ruta);
                                if (!Diccionario_CRCs_Lista_Rutas.ContainsKey(CRC_32)) // New CRC.
                                {
                                    Diccionario_CRCs_Lista_Rutas.Add(CRC_32, new List<string>(new string[] { "[Pack 1]:", "assets\\minecraft\\" + Ruta })); // Add the new CRC and path.
                                }
                                else if (!Diccionario_CRCs_Lista_Rutas[CRC_32].Contains("assets\\minecraft\\" + Ruta)) // Ignore repeated paths.
                                {
                                    Diccionario_CRCs_Lista_Rutas[CRC_32].AddRange(new string[] { "[Pack 1]:", "assets\\minecraft\\" + Ruta }); // Add the path to the existing CRC.
                                }
                            }
                        }
                        foreach (string Ruta in Matriz_Archivos_Pack_2)
                        {
                            if (string.Compare(Carpeta.Ruta_Pack_2, Path.GetDirectoryName("assets\\minecraft\\" + Ruta), true) == 0)
                            {
                                uint CRC_32 = Program.Obtener_CRC_32(Ruta_Pack_2 + "\\" + Ruta);
                                if (!Diccionario_CRCs_Lista_Rutas.ContainsKey(CRC_32)) // New CRC.
                                {
                                    Diccionario_CRCs_Lista_Rutas.Add(CRC_32, new List<string>(new string[] { "[Pack 2]:", "assets\\minecraft\\" + Ruta })); // Add the new CRC and path.
                                }
                                else if (!Diccionario_CRCs_Lista_Rutas[CRC_32].Contains("assets\\minecraft\\" + Ruta)) // Ignore repeated paths.
                                {
                                    Diccionario_CRCs_Lista_Rutas[CRC_32].AddRange(new string[] { "[Pack 2]:", "assets\\minecraft\\" + Ruta }); // Add the path to the existing CRC.
                                }
                            }
                        }
                        foreach (string Ruta in Matriz_Archivos_Pack_3)
                        {
                            if (string.Compare(Carpeta.Ruta_Pack_3, Path.GetDirectoryName("assets\\minecraft\\" + Ruta), true) == 0)
                            {
                                uint CRC_32 = Program.Obtener_CRC_32(Ruta_Pack_3 + "\\" + Ruta);
                                if (!Diccionario_CRCs_Lista_Rutas.ContainsKey(CRC_32)) // New CRC.
                                {
                                    Diccionario_CRCs_Lista_Rutas.Add(CRC_32, new List<string>(new string[] { "[Pack 3]:", "assets\\minecraft\\" + Ruta })); // Add the new CRC and path.
                                }
                                else if (!Diccionario_CRCs_Lista_Rutas[CRC_32].Contains("assets\\minecraft\\" + Ruta)) // Ignore repeated paths.
                                {
                                    Diccionario_CRCs_Lista_Rutas[CRC_32].AddRange(new string[] { "[Pack 3]:", "assets\\minecraft\\" + Ruta }); // Add the path to the existing CRC.
                                }
                            }
                        }
                        foreach (string Ruta in Matriz_Archivos_Pack_4)
                        {
                            if (string.Compare(Carpeta.Ruta_Pack_4, Path.GetDirectoryName("assets\\minecraft\\" + Ruta), true) == 0)
                            {
                                uint CRC_32 = Program.Obtener_CRC_32(Ruta_Pack_4 + "\\" + Ruta);
                                if (!Diccionario_CRCs_Lista_Rutas.ContainsKey(CRC_32)) // New CRC.
                                {
                                    Diccionario_CRCs_Lista_Rutas.Add(CRC_32, new List<string>(new string[] { "[Pack 4]:", "assets\\minecraft\\" + Ruta })); // Add the new CRC and path.
                                }
                                else if (!Diccionario_CRCs_Lista_Rutas[CRC_32].Contains("assets\\minecraft\\" + Ruta)) // Ignore repeated paths.
                                {
                                    Diccionario_CRCs_Lista_Rutas[CRC_32].AddRange(new string[] { "[Pack 4]:", "assets\\minecraft\\" + Ruta }); // Add the path to the existing CRC.
                                }

                                // This code will self-program the application, in some parts.
                                /*Lector_Texto.WriteLine(new string(' ', 20) + "new Archivos");
                                Lector_Texto.WriteLine(new string(' ', 20) + "(");
                                Lector_Texto.WriteLine(new string(' ', 24) + "\"\",");
                                Lector_Texto.WriteLine(new string(' ', 24) + "\"\",");
                                Lector_Texto.WriteLine(new string(' ', 24) + "\"\",");
                                Lector_Texto.WriteLine(new string(' ', 24) + "\"" + Ruta.Replace("\\", "\\\\") + "\"");
                                Lector_Texto.WriteLine(new string(' ', 20) + "),");*//*
                            }
                        }

                        // Write the CRCs found and it's paths for each folder.
                        foreach (KeyValuePair<uint, List<string>> Entrada in Diccionario_CRCs_Lista_Rutas)
                        {
                            Lector_Texto.WriteLine(new string(' ', 4) + Entrada.Key.ToString());
                            Lector_Texto.WriteLine(new string(' ', 4) + "{");
                            foreach (string Ruta in Entrada.Value)
                            {
                                Lector_Texto.WriteLine((Ruta.StartsWith("[") ? new string(' ', 4) : null) + Ruta.Replace("\\", "\\\\"));
                            }
                            Lector_Texto.WriteLine(new string(' ', 4) + "},");
                        }
                        Lector_Texto.WriteLine();
                        Lector_Texto.WriteLine();
                        Diccionario_CRCs_Lista_Rutas = null;
                    }*/
                    Lector_Texto.Close();
                    Lector_Texto.Dispose();
                    Lector_Texto = null;
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                    Matriz_Carpetas_Pack_1 = null;
                    Matriz_Carpetas_Pack_2 = null;
                    Matriz_Carpetas_Pack_3 = null;
                    Matriz_Carpetas_Pack_4 = null;
                    Matriz_Archivos_Pack_1 = null;
                    Matriz_Archivos_Pack_2 = null;
                    Matriz_Archivos_Pack_3 = null;
                    Matriz_Archivos_Pack_4 = null;
                    SystemSounds.Asterisk.Play();
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Pack_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Convertir_Pack_Recursos(TextBox_Ruta.Text, ComboBox_Pack.SelectedIndex + 1, true);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Convertir_Click(object sender, EventArgs e)
        {
            try
            {
                Convertir_Pack_Recursos(TextBox_Ruta.Text, ComboBox_Pack.SelectedIndex + 1, false);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Converts any valid Minecraft resource pack (in a zip file or an uncompressed folder),
        /// to the specified pack format. The new resulting zip file will be stored on the user's desktop.
        /// </summary>
        /// <param name="Ruta">The path to the zip file or folder.</param>
        /// <param name="Formato_Pack">The destination pack format.</param>
        /// <param name="Analizar">If true the resource pack will only be analyzed and no real conversion will be done. Useful to see if it's ready to convert.</param>
        internal void Convertir_Pack_Recursos(string Ruta, int Formato_Pack, bool Analizar)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Imagen_Pack = null; // Reset.
                if (File.Exists(Ruta)) // ICSharpCode.SharpZipLib.Zip.ZipFile.
                {
                    FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                    Lector.Seek(0L, SeekOrigin.Begin);
                    if (Lector.Length > 0L)
                    {
                        FileStream Lector_Salida = null;
                        ZipOutputStream Archivo_ZIP_Salida = null;
                        if (!Analizar)
                        {
                            Lector_Salida = new FileStream(Program.Obtener_Ruta_Temporal_Escritorio() + " Resource pack [" + (Formato_Pack == 1 ? "1.6+" : Formato_Pack == 2 ? "1.9+" : Formato_Pack == 3 ? "1.11+" : "1.13+") + "].zip", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                            Lector_Salida.SetLength(0L);
                            Lector_Salida.Seek(0L, SeekOrigin.Begin);
                            Archivo_ZIP_Salida = new ZipOutputStream(Lector_Salida);
                        }
                        ZipFile Archivo_ZIP = new ZipFile(Ruta);
                        int Formato_Pack_ZIP = 0;
                        int Dimensiones = 0;
                        int Total_Carpetas = 0;
                        int Carpetas_Desconocidas = 0;
                        int Carpetas_Conocidas = 0;
                        int Carpetas_Ignoradas = 0;
                        int Carpetas_Convertibles = 0;
                        int Total_Archivos = 0;
                        int Archivos_Desconocidos = 0;
                        int Archivos_Conocidos = 0;
                        int Archivos_Ignorados = 0;
                        int Archivos_Convertibles = 0;
                        string Texto_Pack_Mcmeta = null;
                        Dictionary<string, object> Diccionario_Post_Procesar = new Dictionary<string, object>();
                        Diccionario_Post_Procesar.Add("assets/minecraft/textures/particle/particles.png", null);
                        ZipEntry Entrada_Pack_Mcmeta = Archivo_ZIP.GetEntry("pack.mcmeta");
                        if (Entrada_Pack_Mcmeta != null)
                        {
                            Stream Lector_Pack = Archivo_ZIP.GetInputStream(Entrada_Pack_Mcmeta);
                            if (Lector_Pack != null)
                            {
                                StreamReader Lector_Texto = new StreamReader(Lector_Pack, Encoding.Default);
                                Texto_Pack_Mcmeta = Lector_Texto.ReadToEnd();
                                if (!string.IsNullOrEmpty(Texto_Pack_Mcmeta))
                                {
                                    int Índice_Formato_Pack = Texto_Pack_Mcmeta.IndexOf("pack_format", StringComparison.InvariantCultureIgnoreCase);
                                    if (Índice_Formato_Pack > -1)
                                    {
                                        for (int Índice = Índice_Formato_Pack + 11; Índice < Texto_Pack_Mcmeta.Length; Índice++)
                                        {
                                            if (char.IsDigit(Texto_Pack_Mcmeta[Índice]))
                                            {
                                                Formato_Pack_ZIP = int.Parse(Texto_Pack_Mcmeta[Índice].ToString());
                                                if (Formato_Pack_ZIP != Formato_Pack) // Set the new pack format.
                                                {
                                                    char[] Matriz_Caracteres = Texto_Pack_Mcmeta.ToCharArray();
                                                    Matriz_Caracteres[Índice] = Formato_Pack.ToString()[0];
                                                    Texto_Pack_Mcmeta = new string(Matriz_Caracteres);
                                                    Matriz_Caracteres = null;
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                                Lector_Texto.Close();
                                Lector_Texto.Dispose();
                                Lector_Texto = null;
                                Lector_Pack.Close();
                                Lector_Pack.Dispose();
                                Lector_Pack = null;
                            }
                            Entrada_Pack_Mcmeta = null;
                        }
                        ZipEntry Entrada_Pack_Png = Archivo_ZIP.GetEntry("pack.png");
                        if (Entrada_Pack_Png != null)
                        {
                            Stream Lector_Pack = Archivo_ZIP.GetInputStream(Entrada_Pack_Png);
                            if (Lector_Pack != null)
                            {
                                Image Imagen_Original = null;
                                try { Imagen_Original = Image.FromStream(Lector_Pack, false, false); }
                                catch { Imagen_Original = null; }
                                if (Imagen_Original != null)
                                {
                                    int Ancho = Imagen_Original.Width;
                                    int Alto = Imagen_Original.Height;
                                    Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                                    Graphics Pintar = Graphics.FromImage(Imagen);
                                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                    Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                    Pintar.SmoothingMode = SmoothingMode.HighQuality;
                                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                                    Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                    Pintar.Dispose();
                                    Pintar = null;
                                    Imagen_Pack = Imagen;
                                    Imagen_Original.Dispose();
                                    Imagen_Original = null;
                                }
                                Lector_Pack.Close();
                                Lector_Pack.Dispose();
                                Lector_Pack = null;
                            }
                            Entrada_Pack_Png = null;
                        }
                        long Índice_Entrada_ZIP = 1;
                        long Total_Entradas_ZIP = Archivo_ZIP.Count;
                        foreach (ZipEntry Entrada in Archivo_ZIP)
                        {
                            Barra_Progreso.Value = (int)((Índice_Entrada_ZIP * 10000L) / Total_Entradas_ZIP);
                            Índice_Entrada_ZIP++;
                            bool Conocido = false;
                            string Nombre = Entrada.Name.Replace("/", "\\");
                            if (Entrada.IsDirectory)
                            {
                                Nombre = Nombre.TrimEnd("\\/".ToCharArray());
                                Total_Carpetas++;
                                foreach (Packs_Recursos.Carpetas Carpeta in Packs_Recursos.Matriz_Carpetas)
                                {
                                    if (string.Compare(Carpeta.Ruta_Pack_1, Nombre, true) == 0 ||
                                    string.Compare(Carpeta.Ruta_Pack_2, Nombre, true) == 0 ||
                                    string.Compare(Carpeta.Ruta_Pack_3, Nombre, true) == 0 ||
                                    string.Compare(Carpeta.Ruta_Pack_4, Nombre, true) == 0)
                                    {
                                        Conocido = true;
                                        if (Formato_Pack == 1 && !string.IsNullOrEmpty(Carpeta.Ruta_Pack_1))
                                        {
                                            Carpetas_Convertibles++;
                                            if (!Analizar)
                                            {
                                                Archivo_ZIP_Salida.PutNextEntry(new ZipEntry(Carpeta.Ruta_Pack_1.Replace("\\", "/") + "/"));
                                                Archivo_ZIP_Salida.CloseEntry();
                                            }
                                        }
                                        else if (Formato_Pack == 2 && !string.IsNullOrEmpty(Carpeta.Ruta_Pack_2))
                                        {
                                            Carpetas_Convertibles++;
                                            if (!Analizar)
                                            {
                                                Archivo_ZIP_Salida.PutNextEntry(new ZipEntry(Carpeta.Ruta_Pack_2.Replace("\\", "/") + "/"));
                                                Archivo_ZIP_Salida.CloseEntry();
                                            }
                                        }
                                        else if (Formato_Pack == 3 && !string.IsNullOrEmpty(Carpeta.Ruta_Pack_3))
                                        {
                                            Carpetas_Convertibles++;
                                            if (!Analizar)
                                            {
                                                Archivo_ZIP_Salida.PutNextEntry(new ZipEntry(Carpeta.Ruta_Pack_3.Replace("\\", "/") + "/"));
                                                Archivo_ZIP_Salida.CloseEntry();
                                            }
                                        }
                                        else if (Formato_Pack == 4 && !string.IsNullOrEmpty(Carpeta.Ruta_Pack_4))
                                        {
                                            Carpetas_Convertibles++;
                                            if (!Analizar)
                                            {
                                                Archivo_ZIP_Salida.PutNextEntry(new ZipEntry(Carpeta.Ruta_Pack_4.Replace("\\", "/") + "/"));
                                                Archivo_ZIP_Salida.CloseEntry();
                                            }
                                        }
                                        else
                                        {
                                            Carpetas_Ignoradas++;
                                            if (!Analizar)
                                            {
                                                Archivo_ZIP_Salida.PutNextEntry(new ZipEntry(Nombre.Replace("\\", "/") + "/"));
                                                Archivo_ZIP_Salida.CloseEntry();
                                            }
                                        }
                                        break;
                                    }
                                }
                                if (Conocido) Carpetas_Conocidas++;
                                else
                                {
                                    Carpetas_Desconocidas++;
                                    if (!Analizar)
                                    {
                                        Archivo_ZIP_Salida.PutNextEntry(new ZipEntry(Nombre.Replace("\\", "/") + "/"));
                                        Archivo_ZIP_Salida.CloseEntry();
                                    }
                                }
                            }
                            else if (!Diccionario_Post_Procesar.ContainsKey(Entrada.Name.ToLowerInvariant()))
                            {
                                string Ruta_Carpeta_Máxima = string.Empty;
                                string Ruta_Carpeta_Máxima_Nueva = string.Empty;
                                bool Archivo_Mcmeta = false;
                                if (Nombre.ToLowerInvariant().EndsWith(".mcmeta"))
                                {
                                    Archivo_Mcmeta = true;
                                    Nombre = Nombre.Substring(0, Nombre.Length - 7);
                                }
                                else
                                {
                                    if (string.Compare(Nombre, "assets\\minecraft\\textures\\blocks\\stone.png", true) == 0 ||
                                        string.Compare(Nombre, "assets\\minecraft\\textures\\blocks\\stone.png", true) == 0 ||
                                        string.Compare(Nombre, "assets\\minecraft\\textures\\blocks\\stone.png", true) == 0 ||
                                        string.Compare(Nombre, "assets\\minecraft\\textures\\block\\stone.png", true) == 0)
                                    {
                                        Stream Lector_Entrada = Archivo_ZIP.GetInputStream(Entrada);
                                        if (Lector_Entrada != null)
                                        {
                                            Image Imagen_Original = null;
                                            try { Imagen_Original = Image.FromStream(Lector_Entrada, false, false); }
                                            catch { Imagen_Original = null; }
                                            if (Imagen_Original != null)
                                            {
                                                Dimensiones = Math.Min(Imagen_Original.Width, Imagen_Original.Height);
                                                Imagen_Original.Dispose();
                                                Imagen_Original = null;
                                            }
                                            Lector_Entrada.Close();
                                            Lector_Entrada.Dispose();
                                            Lector_Entrada = null;
                                        }
                                    }
                                }
                                Total_Archivos++;
                                foreach (Packs_Recursos.Carpetas Carpeta in Packs_Recursos.Matriz_Carpetas)
                                {
                                    if (Nombre.StartsWith(Carpeta.Ruta_Pack_1, StringComparison.InvariantCultureIgnoreCase) &&
                                        Carpeta.Ruta_Pack_1.Length > Ruta_Carpeta_Máxima.Length)
                                    {
                                        Ruta_Carpeta_Máxima = Carpeta.Ruta_Pack_2;
                                        if (Formato_Pack == 1) Ruta_Carpeta_Máxima_Nueva = Carpeta.Ruta_Pack_1;
                                        else if (Formato_Pack == 2) Ruta_Carpeta_Máxima_Nueva = Carpeta.Ruta_Pack_2;
                                        else if (Formato_Pack == 3) Ruta_Carpeta_Máxima_Nueva = Carpeta.Ruta_Pack_3;
                                        else if (Formato_Pack == 4) Ruta_Carpeta_Máxima_Nueva = Carpeta.Ruta_Pack_4;
                                    }
                                    if (Nombre.StartsWith(Carpeta.Ruta_Pack_2, StringComparison.InvariantCultureIgnoreCase) &&
                                        Carpeta.Ruta_Pack_2.Length > Ruta_Carpeta_Máxima.Length)
                                    {
                                        Ruta_Carpeta_Máxima = Carpeta.Ruta_Pack_1;
                                        if (Formato_Pack == 1) Ruta_Carpeta_Máxima_Nueva = Carpeta.Ruta_Pack_1;
                                        else if (Formato_Pack == 2) Ruta_Carpeta_Máxima_Nueva = Carpeta.Ruta_Pack_2;
                                        else if (Formato_Pack == 3) Ruta_Carpeta_Máxima_Nueva = Carpeta.Ruta_Pack_3;
                                        else if (Formato_Pack == 4) Ruta_Carpeta_Máxima_Nueva = Carpeta.Ruta_Pack_4;
                                    }
                                    if (Nombre.StartsWith(Carpeta.Ruta_Pack_3, StringComparison.InvariantCultureIgnoreCase) &&
                                        Carpeta.Ruta_Pack_3.Length > Ruta_Carpeta_Máxima.Length)
                                    {
                                        Ruta_Carpeta_Máxima = Carpeta.Ruta_Pack_3;
                                        if (Formato_Pack == 1) Ruta_Carpeta_Máxima_Nueva = Carpeta.Ruta_Pack_1;
                                        else if (Formato_Pack == 2) Ruta_Carpeta_Máxima_Nueva = Carpeta.Ruta_Pack_2;
                                        else if (Formato_Pack == 3) Ruta_Carpeta_Máxima_Nueva = Carpeta.Ruta_Pack_3;
                                        else if (Formato_Pack == 4) Ruta_Carpeta_Máxima_Nueva = Carpeta.Ruta_Pack_4;
                                    }
                                    if (Nombre.StartsWith(Carpeta.Ruta_Pack_4, StringComparison.InvariantCultureIgnoreCase) &&
                                        Carpeta.Ruta_Pack_4.Length > Ruta_Carpeta_Máxima.Length)
                                    {
                                        Ruta_Carpeta_Máxima = Carpeta.Ruta_Pack_4;
                                        if (Formato_Pack == 1) Ruta_Carpeta_Máxima_Nueva = Carpeta.Ruta_Pack_1;
                                        else if (Formato_Pack == 2) Ruta_Carpeta_Máxima_Nueva = Carpeta.Ruta_Pack_2;
                                        else if (Formato_Pack == 3) Ruta_Carpeta_Máxima_Nueva = Carpeta.Ruta_Pack_3;
                                        else if (Formato_Pack == 4) Ruta_Carpeta_Máxima_Nueva = Carpeta.Ruta_Pack_4;
                                    }
                                    foreach (Packs_Recursos.Archivos Archivo in Carpeta.Matriz_Archivos)
                                    {
                                        if (string.Compare(Archivo.Ruta_Pack_1, Nombre, true) == 0 ||
                                        string.Compare(Archivo.Ruta_Pack_2, Nombre, true) == 0 ||
                                        string.Compare(Archivo.Ruta_Pack_3, Nombre, true) == 0 ||
                                        string.Compare(Archivo.Ruta_Pack_4, Nombre, true) == 0)
                                        {
                                            Conocido = true;
                                            if (Formato_Pack == 1 && !string.IsNullOrEmpty(Archivo.Ruta_Pack_1))
                                            {
                                                Archivos_Convertibles++;
                                                if (!Analizar)
                                                {
                                                    Archivo_ZIP_Salida.PutNextEntry(new ZipEntry(Archivo.Ruta_Pack_1.Replace("\\", "/") + (!Archivo_Mcmeta ? null : ".mcmeta")));
                                                    Stream Lector_Entrada = Archivo_ZIP.GetInputStream(Entrada);
                                                    byte[] Matriz_Bytes = new byte[Entrada.Size];
                                                    int Longitud = Lector_Entrada.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                                    Archivo_ZIP_Salida.Write(Matriz_Bytes, 0, Longitud);
                                                    Archivo_ZIP_Salida.CloseEntry();
                                                    Matriz_Bytes = null;
                                                    Lector_Entrada.Close();
                                                    Lector_Entrada.Dispose();
                                                    Lector_Entrada = null;
                                                }
                                            }
                                            else if (Formato_Pack == 2 && !string.IsNullOrEmpty(Archivo.Ruta_Pack_2))
                                            {
                                                Archivos_Convertibles++;
                                                if (!Analizar)
                                                {
                                                    Archivo_ZIP_Salida.PutNextEntry(new ZipEntry(Archivo.Ruta_Pack_2.Replace("\\", "/") + (!Archivo_Mcmeta ? null : ".mcmeta")));
                                                    Stream Lector_Entrada = Archivo_ZIP.GetInputStream(Entrada);
                                                    byte[] Matriz_Bytes = new byte[Entrada.Size];
                                                    int Longitud = Lector_Entrada.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                                    Archivo_ZIP_Salida.Write(Matriz_Bytes, 0, Longitud);
                                                    Archivo_ZIP_Salida.CloseEntry();
                                                    Matriz_Bytes = null;
                                                    Lector_Entrada.Close();
                                                    Lector_Entrada.Dispose();
                                                    Lector_Entrada = null;
                                                }
                                            }
                                            else if (Formato_Pack == 3 && !string.IsNullOrEmpty(Archivo.Ruta_Pack_3))
                                            {
                                                Archivos_Convertibles++;
                                                if (!Analizar)
                                                {
                                                    Archivo_ZIP_Salida.PutNextEntry(new ZipEntry(Archivo.Ruta_Pack_3.Replace("\\", "/") + (!Archivo_Mcmeta ? null : ".mcmeta")));
                                                    Stream Lector_Entrada = Archivo_ZIP.GetInputStream(Entrada);
                                                    byte[] Matriz_Bytes = new byte[Entrada.Size];
                                                    int Longitud = Lector_Entrada.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                                    Archivo_ZIP_Salida.Write(Matriz_Bytes, 0, Longitud);
                                                    Archivo_ZIP_Salida.CloseEntry();
                                                    Matriz_Bytes = null;
                                                    Lector_Entrada.Close();
                                                    Lector_Entrada.Dispose();
                                                    Lector_Entrada = null;
                                                }
                                            }
                                            else if (Formato_Pack == 4 && !string.IsNullOrEmpty(Archivo.Ruta_Pack_4))
                                            {
                                                Archivos_Convertibles++;
                                                if (!Analizar)
                                                {
                                                    Archivo_ZIP_Salida.PutNextEntry(new ZipEntry(Archivo.Ruta_Pack_4.Replace("\\", "/") + (!Archivo_Mcmeta ? null : ".mcmeta")));
                                                    Stream Lector_Entrada = Archivo_ZIP.GetInputStream(Entrada);
                                                    byte[] Matriz_Bytes = new byte[Entrada.Size];
                                                    int Longitud = Lector_Entrada.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);

                                                    if (string.Compare("", Archivo.Ruta_Pack_4, true) == 0 ||
                                                        string.Compare("assets\\minecraft\\textures\\items\\clock.png", Archivo.Ruta_Pack_4, true) == 0 ||
                                                        string.Compare("assets\\minecraft\\textures\\items\\compass.png", Archivo.Ruta_Pack_4, true) == 0 ||
                                                        string.Compare("", Archivo.Ruta_Pack_4, true) == 0 ||
                                                        string.Compare("", Archivo.Ruta_Pack_4, true) == 0)
                                                    { // Post process some resources.
                                                        // ...
                                                    }

                                                    Archivo_ZIP_Salida.Write(Matriz_Bytes, 0, Longitud);
                                                    Archivo_ZIP_Salida.CloseEntry();
                                                    Matriz_Bytes = null;
                                                    Lector_Entrada.Close();
                                                    Lector_Entrada.Dispose();
                                                    Lector_Entrada = null;
                                                }
                                            }
                                            else
                                            {
                                                Archivos_Ignorados++;
                                                if (!Analizar)
                                                {
                                                    if (!string.IsNullOrEmpty(Ruta_Carpeta_Máxima) && !string.IsNullOrEmpty(Ruta_Carpeta_Máxima_Nueva))
                                                    {
                                                        Nombre = Ruta_Carpeta_Máxima_Nueva + Nombre.Substring(Ruta_Carpeta_Máxima.Length);
                                                    } // Convert at least the known folders of unknown files.
                                                    Archivo_ZIP_Salida.PutNextEntry(new ZipEntry(Nombre.Replace("\\", "/") + (!Archivo_Mcmeta ? null : ".mcmeta")));
                                                    Stream Lector_Entrada = Archivo_ZIP.GetInputStream(Entrada);
                                                    byte[] Matriz_Bytes = new byte[Entrada.Size];
                                                    int Longitud = Lector_Entrada.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                                    Archivo_ZIP_Salida.Write(Matriz_Bytes, 0, Longitud);
                                                    Archivo_ZIP_Salida.CloseEntry();
                                                    Matriz_Bytes = null;
                                                    Lector_Entrada.Close();
                                                    Lector_Entrada.Dispose();
                                                    Lector_Entrada = null;
                                                }
                                            }
                                            break;
                                        }
                                    }
                                    if (Conocido) break;
                                }
                                if (Conocido) Archivos_Conocidos++;
                                else
                                {
                                    Archivos_Desconocidos++;
                                    if (!Analizar)
                                    {
                                        if (!string.IsNullOrEmpty(Ruta_Carpeta_Máxima) && !string.IsNullOrEmpty(Ruta_Carpeta_Máxima_Nueva))
                                        {
                                            Nombre = Ruta_Carpeta_Máxima_Nueva + Nombre.Substring(Ruta_Carpeta_Máxima.Length);
                                        } // Convert at least the known folders of unknown files.
                                        Archivo_ZIP_Salida.PutNextEntry(new ZipEntry(Nombre.Replace("\\", "/") + (!Archivo_Mcmeta ? null : ".mcmeta")));
                                        if (string.Compare(Entrada.Name, "pack.mcmeta", true) != 0)
                                        {
                                            Stream Lector_Entrada = Archivo_ZIP.GetInputStream(Entrada);
                                            byte[] Matriz_Bytes = new byte[Entrada.Size];
                                            int Longitud = Lector_Entrada.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                            Archivo_ZIP_Salida.Write(Matriz_Bytes, 0, Longitud);
                                            Archivo_ZIP_Salida.CloseEntry();
                                            Matriz_Bytes = null;
                                            Lector_Entrada.Close();
                                            Lector_Entrada.Dispose();
                                            Lector_Entrada = null;
                                        }
                                        else
                                        {
                                            byte[] Matriz_Bytes = Encoding.Default.GetBytes(Texto_Pack_Mcmeta);
                                            Archivo_ZIP_Salida.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                            Archivo_ZIP_Salida.CloseEntry();
                                            Matriz_Bytes = null;
                                            Texto_Pack_Mcmeta = null;
                                        }
                                    }
                                }
                            }
                        }
                        if (!Analizar) // Post process several resources at the end.
                        {
                            ZipEntry Entrada_Particles = Archivo_ZIP.GetEntry("assets/minecraft/textures/particle/particles.png");
                            if (Entrada_Particles != null)
                            {
                                Stream Lector_Particles = Archivo_ZIP.GetInputStream(Entrada_Particles);
                                if (Lector_Particles != null)
                                {
                                    Image Imagen_Original = null;
                                    try { Imagen_Original = Image.FromStream(Lector_Particles, false, false); }
                                    catch { Imagen_Original = null; }
                                    if (Imagen_Original != null)
                                    {
                                        int Ancho_Original = Imagen_Original.Width;
                                        int Alto_Original = Imagen_Original.Height;
                                        int Ancho = 0;
                                        int Alto = 0;
                                        if (Formato_Pack < 4)
                                        {
                                            Ancho = Dimensiones * 8;
                                            Alto = Dimensiones * 8;
                                        }
                                        else
                                        {
                                            Ancho = Dimensiones * 16;
                                            Alto = Dimensiones * 16;
                                        }
                                        if (Ancho_Original != Ancho || Alto_Original != Alto) // Readjust the dimensions.
                                        {
                                            Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                                            Graphics Pintar = Graphics.FromImage(Imagen);
                                            Pintar.CompositingMode = CompositingMode.SourceCopy;
                                            Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                            Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                            Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                            Pintar.SmoothingMode = SmoothingMode.HighQuality;
                                            Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                                            Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho_Original, Alto_Original), new Rectangle(0, 0, Ancho_Original, Alto_Original), GraphicsUnit.Pixel);
                                            Pintar.Dispose();
                                            Pintar = null;
                                            MemoryStream Lector_Memoria = new MemoryStream();
                                            Imagen.Save(Lector_Memoria, ImageFormat.Png);
                                            byte[] Matriz_Bytes = Lector_Memoria.ToArray();
                                            Archivo_ZIP_Salida.PutNextEntry(new ZipEntry("assets/minecraft/textures/particle/particles.png"));
                                            Archivo_ZIP_Salida.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                            Archivo_ZIP_Salida.CloseEntry();
                                            Matriz_Bytes = null;
                                            Lector_Memoria.Close();
                                            Lector_Memoria.Dispose();
                                            Lector_Memoria = null;
                                        }
                                        else // It's already corrected, so just make a direct copy of the file.
                                        {
                                            Lector_Particles.Close();
                                            Lector_Particles.Dispose();
                                            Lector_Particles = null;
                                            Lector_Particles = Archivo_ZIP.GetInputStream(Entrada_Particles); // Re-open.
                                            if (Lector_Particles != null)
                                            {
                                                byte[] Matriz_Bytes = new byte[Entrada_Particles.Size];
                                                int Longitud = Lector_Particles.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                                Archivo_ZIP_Salida.PutNextEntry(new ZipEntry("assets/minecraft/textures/particle/particles.png"));
                                                Archivo_ZIP_Salida.Write(Matriz_Bytes, 0, Longitud);
                                                Archivo_ZIP_Salida.CloseEntry();
                                                Matriz_Bytes = null;
                                            }
                                        }
                                        Imagen_Original.Dispose();
                                        Imagen_Original = null;
                                    }
                                    Lector_Particles.Close();
                                    Lector_Particles.Dispose();
                                    Lector_Particles = null;
                                }
                                Entrada_Particles = null;
                            }
                            //if (Formato_Pack == 1) Entrada_Particles = Archivo_ZIP.GetEntry("assets/minecraft/textures/particle/particles.png");
                            //else if (Formato_Pack == 2) Entrada_Particles = Archivo_ZIP.GetEntry("assets/minecraft/textures/particle/particles.png");
                            //else if (Formato_Pack == 3) Entrada_Particles = Archivo_ZIP.GetEntry("assets/minecraft/textures/particle/particles.png");
                            //else if (Formato_Pack == 4) Entrada_Particles = Archivo_ZIP.GetEntry("assets/minecraft/textures/particle/particles.png");
                        }
                        NumericUpDown_Información_Formato_Pack.Value = Formato_Pack_ZIP;
                        NumericUpDown_Información_Dimensiones.Value = Dimensiones;
                        NumericUpDown_Información_Total_Carpetas.Value = Total_Carpetas;
                        NumericUpDown_Información_Carpetas_Desconocidas.Value = Carpetas_Desconocidas;
                        NumericUpDown_Información_Carpetas_Conocidas.Value = Carpetas_Conocidas;
                        NumericUpDown_Información_Carpetas_Ignoradas.Value = Carpetas_Ignoradas;
                        NumericUpDown_Información_Carpetas_Convertibles.Value = Carpetas_Convertibles;
                        NumericUpDown_Información_Total_Archivos.Value = Total_Archivos;
                        NumericUpDown_Información_Archivos_Desconocidos.Value = Archivos_Desconocidos;
                        NumericUpDown_Información_Archivos_Conocidos.Value = Archivos_Conocidos;
                        NumericUpDown_Información_Archivos_Ignorados.Value = Archivos_Ignorados;
                        NumericUpDown_Información_Archivos_Convertibles.Value = Archivos_Convertibles;
                        Picture.Image = Imagen_Pack != null ? Program.Obtener_Imagen_Miniatura(Imagen_Pack, Picture.ClientSize.Width, Picture.ClientSize.Height, true, false, CheckState.Checked) : null;
                        if (!Analizar)
                        {
                            Archivo_ZIP_Salida.Finish();
                            Archivo_ZIP_Salida.Close();
                            Archivo_ZIP_Salida.Dispose();
                            Archivo_ZIP_Salida = null;
                            Lector_Salida.Close();
                            Lector_Salida.Dispose();
                            Lector_Salida = null;
                            SystemSounds.Asterisk.Play();
                        }
                        Archivo_ZIP.Close();
                        Archivo_ZIP = null;
                    }
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                }
                else if (Directory.Exists(Ruta))
                {
                    // Soon...
                }
                this.Activate();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally
            {
                this.Cursor = Cursors.Default;
                Barra_Progreso.Value = 0;
            }
        }
    }
}
