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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Analizador_Imágenes_Archivos : Form
    {
        public Ventana_Analizador_Imágenes_Archivos()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Files and Images Analyzer by Jupisoft for " + Program.Texto_Usuario;
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

        private void Ventana_Analizador_Imágenes_Archivos_Load(object sender, EventArgs e)
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

        private void Ventana_Analizador_Imágenes_Archivos_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Analizador_Imágenes_Archivos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Analizador_Imágenes_Archivos_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Analizador_Imágenes_Archivos_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Analizador_Imágenes_Archivos_DragDrop(object sender, DragEventArgs e)
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
                                    TextBox_Ruta.Text = Ruta;
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

        private void Ventana_Analizador_Imágenes_Archivos_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Analizador_Imágenes_Archivos_KeyDown(object sender, KeyEventArgs e)
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

        private void TextBox_Ruta_TextChanged(object sender, EventArgs e)
        {
            try
            {
                long Tamaño = 0L;
                if (!string.IsNullOrEmpty(TextBox_Ruta.Text) && File.Exists(TextBox_Ruta.Text))
                {
                    Tamaño = new FileInfo(TextBox_Ruta.Text).Length;
                    TextBox_Tamaño.Text = Program.Traducir_Número(Tamaño) + (Tamaño != 1L ? " bytes" : " byte") + (Tamaño > 1024L ? " (" + Program.Traducir_Tamaño_Bytes_Automático(Tamaño, 4, true) + ")" : null);
                }
                else TextBox_Tamaño.Text = TextBox_Tamaño.Text = Program.Traducir_Número(Tamaño) + (Tamaño != 1L ? " bytes" : " byte") + (Tamaño > 1024L ? " (" + Program.Traducir_Tamaño_Bytes_Automático(Tamaño, 4, true) + ")" : null);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Calcular_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Calcular_Hashes();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        /// <summary>
        /// Function that converts all the bytes in an array into a single hexadecimal (base 16) string. Warning: don't use this function with large byte arrays since it was originally designed to use with hashes like SHA1, etc.
        /// </summary>
        /// <param name="Matriz_Bytes">Any valid byte array with at least 1 byte.</param>
        /// <returns>Returns the hexadecimal string with all the bytes in the array. Returns null on any error.</returns>
        internal string Convertir_Matriz_Bytes_Hexadecimal(byte[] Matriz_Bytes, CharacterCasing Mayúsculas)
        {
            try
            {
                if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                {
                    string Texto_Hexadecimal = null;
                    foreach (byte Valor in Matriz_Bytes)
                    {
                        try
                        {
                            string Texto_Byte = Convert.ToString(Valor, 16);
                            if (Texto_Byte.Length < 2) Texto_Byte = new string('0', 2 - Texto_Byte.Length) + Texto_Byte;
                            Texto_Hexadecimal += Texto_Byte;
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                    }
                    if (Mayúsculas == CharacterCasing.Lower) Texto_Hexadecimal = Texto_Hexadecimal.ToLowerInvariant();
                    else if (Mayúsculas == CharacterCasing.Upper) Texto_Hexadecimal = Texto_Hexadecimal.ToUpperInvariant();
                    return Texto_Hexadecimal;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        /// <summary>
        /// Function that generates several hashes from any file and even from the pixels of any valid image.
        /// </summary>
        internal void Calcular_Hashes()
        {
            string Texto_Hashes = "[Hashes from the file bytes]\r\n";
            try
            {
                if (!string.IsNullOrEmpty(TextBox_Ruta.Text) && File.Exists(TextBox_Ruta.Text))
                {
                    FileStream Lector = new FileStream(TextBox_Ruta.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    if (Lector != null)
                    {
                        TextBox_Tamaño.Text = Program.Traducir_Número(Lector.Length) + (Lector.Length != 1L ? " bytes" : " byte") + (Lector.Length > 1024L ? " (" + Program.Traducir_Tamaño_Bytes_Automático(Lector.Length, 4, true) + ")" : null);
                        if (Lector.Length > 0L)
                        {
                            uint CRC_32 = 0;
                            Lector.Seek(0L, SeekOrigin.Begin);
                            byte[] Matriz_Bytes = new byte[4096];
                            for (long Índice_Bloque = 0L; Índice_Bloque < Lector.Length; Índice_Bloque += 4096L)
                            {
                                int Longitud = Lector.Read(Matriz_Bytes, 0, 4096);
                                if (Longitud > 0)
                                {
                                    CRC_32 = Program.Calcular_CRC32(Matriz_Bytes, Longitud, CRC_32);
                                }
                                else break;
                            }
                            CharacterCasing Mayúsculas = CharacterCasing.Upper;
                            HashAlgorithm Lector_Hash = null;
                            byte[] Matriz_Bytes_Hash = null;
                            string Texto_GUID = null;
                            //string Texto_UUID = null;

                            string Texto_Hexadecimal = Convert.ToString(CRC_32, 16);
                            if (Texto_Hexadecimal.Length < 8) Texto_Hexadecimal = new string('0', 8 - Texto_Hexadecimal.Length) + Texto_Hexadecimal;
                            if (Mayúsculas == CharacterCasing.Lower) Texto_Hexadecimal = Texto_Hexadecimal.ToLowerInvariant();
                            else if (Mayúsculas == CharacterCasing.Upper) Texto_Hexadecimal = Texto_Hexadecimal.ToUpperInvariant();
                            Texto_Hashes += "CRC32: " + Texto_Hexadecimal + "\r\n";

                            //Texto_Hashes += "CRC64: Soon...\r\n";

                            Lector_Hash = MD5.Create();
                            Lector.Seek(0L, SeekOrigin.Begin);
                            Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Lector);
                            Lector_Hash.Dispose();
                            Lector_Hash = null;
                            
                            // Use the 16 bytes from the MD5 hash to get the GUID hash.
                            Texto_GUID = new Guid(Matriz_Bytes_Hash).ToString();
                            if (Mayúsculas == CharacterCasing.Lower) Texto_GUID = Texto_GUID.ToLowerInvariant();
                            else if (Mayúsculas == CharacterCasing.Upper) Texto_GUID = Texto_GUID.ToUpperInvariant();
                            Texto_Hashes += "GUID: " + Texto_GUID + "\r\n";
                            Texto_GUID = null;
                            
                            Texto_Hashes += "MD5: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                            Matriz_Bytes_Hash = null;

                            Lector_Hash = RIPEMD160Managed.Create();
                            Lector.Seek(0L, SeekOrigin.Begin);
                            Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Lector);
                            Lector_Hash.Dispose();
                            Lector_Hash = null;
                            Texto_Hashes += "RIPEMD160: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                            Matriz_Bytes_Hash = null;

                            Lector_Hash = SHA1Managed.Create();
                            Lector.Seek(0L, SeekOrigin.Begin);
                            Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Lector);
                            Lector_Hash.Dispose();
                            Lector_Hash = null;
                            Texto_Hashes += "SHA1: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                            Matriz_Bytes_Hash = null;

                            Lector_Hash = SHA256Managed.Create();
                            Lector.Seek(0L, SeekOrigin.Begin);
                            Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Lector);
                            Lector_Hash.Dispose();
                            Lector_Hash = null;
                            Texto_Hashes += "SHA256: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                            Matriz_Bytes_Hash = null;

                            Lector_Hash = SHA384Managed.Create();
                            Lector.Seek(0L, SeekOrigin.Begin);
                            Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Lector);
                            Lector_Hash.Dispose();
                            Lector_Hash = null;
                            Texto_Hashes += "SHA384: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                            Matriz_Bytes_Hash = null;

                            Lector_Hash = SHA512Managed.Create();
                            Lector.Seek(0L, SeekOrigin.Begin);
                            Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Lector);
                            Lector_Hash.Dispose();
                            Lector_Hash = null;
                            Texto_Hashes += "SHA512: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                            Matriz_Bytes_Hash = null;

                            /*Texto_UUID = UUID.NameUUIDFromBytes(Matriz_Bytes_ARGB).ToString();
                            if (Mayúsculas == CharacterCasing.Lower) Texto_UUID = Texto_UUID.ToLowerInvariant();
                            else if (Mayúsculas == CharacterCasing.Upper) Texto_UUID = Texto_UUID.ToUpperInvariant();
                            Texto_Hashes += "UUID: " + Texto_UUID + "\r\n";
                            Texto_UUID = null;*/
                            //Texto_Hashes += "UUID: Soon...\r\n";

                            Lector.Seek(0L, SeekOrigin.Begin);
                            Bitmap Imagen = Program.Cargar_Imagen_Lector(Lector, CheckState.Indeterminate);
                            if (Imagen != null)
                            {
                                int Ancho = Imagen.Width;
                                int Alto = Imagen.Height;
                                BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                                int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                                bool Imagen_Alfa = Image.IsAlphaPixelFormat(Imagen.PixelFormat);
                                int Bytes_Aumento = !Imagen_Alfa ? 3 : 4;
                                int Bytes_Diferencia = Ancho_Stride - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                                byte[] Matriz_Bytes_Píxeles = new byte[Ancho_Stride * Alto];
                                Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes_Píxeles, 0, Matriz_Bytes_Píxeles.Length);
                                Imagen.UnlockBits(Bitmap_Data);
                                Imagen.Dispose();
                                Imagen = null;
                                byte[] Matriz_Bytes_ARGB = null;
                                byte[] Matriz_Bytes_RGB = null;
                                byte[] Matriz_Bytes_A = null;
                                byte[] Matriz_Bytes_R = null;
                                byte[] Matriz_Bytes_G = null;
                                byte[] Matriz_Bytes_B = null;
                                if (!Imagen_Alfa)
                                {
                                    // Calculate the RGB channels.
                                    Matriz_Bytes_RGB = new byte[(Ancho * Alto) * 3];
                                    for (int Y = 0, Índice = 0, Índice_RGB = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                                    {
                                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento, Índice_RGB += 3)
                                        {
                                            // Use RGB as the exact byte order, with 3 bytes per pixel and no extra stride bytes.
                                            Matriz_Bytes_RGB[Índice_RGB] = Matriz_Bytes_Píxeles[Índice + 2];
                                            Matriz_Bytes_RGB[Índice_RGB + 1] = Matriz_Bytes_Píxeles[Índice + 1];
                                            Matriz_Bytes_RGB[Índice_RGB + 2] = Matriz_Bytes_Píxeles[Índice];
                                        }
                                    }

                                    // Only calculate the red channel.
                                    Matriz_Bytes_R = new byte[Ancho * Alto];
                                    for (int Y = 0, Índice = 0, Índice_Rojo = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                                    {
                                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento, Índice_Rojo++)
                                        {
                                            Matriz_Bytes_R[Índice_Rojo] = Matriz_Bytes_Píxeles[Índice + 2];
                                        }
                                    }

                                    // Only calculate the green channel.
                                    Matriz_Bytes_G = new byte[Ancho * Alto];
                                    for (int Y = 0, Índice = 0, Índice_Verde = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                                    {
                                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento, Índice_Verde++)
                                        {
                                            Matriz_Bytes_G[Índice_Verde] = Matriz_Bytes_Píxeles[Índice + 1];
                                        }
                                    }

                                    // Only calculate the blue channel.
                                    Matriz_Bytes_B = new byte[Ancho * Alto];
                                    for (int Y = 0, Índice = 0, Índice_Azul = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                                    {
                                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento, Índice_Azul++)
                                        {
                                            Matriz_Bytes_B[Índice_Azul] = Matriz_Bytes_Píxeles[Índice];
                                        }
                                    }
                                }
                                else
                                {
                                    // Calculate the ARGB channels.
                                    Matriz_Bytes_ARGB = new byte[(Ancho * Alto) * 4];
                                    for (int Y = 0, Índice = 0, Índice_ARGB = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                                    {
                                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento, Índice_ARGB += 4)
                                        {
                                            // Use ARGB as the exact byte order, with 4 bytes per pixel and no extra stride bytes.
                                            Matriz_Bytes_ARGB[Índice_ARGB] = Matriz_Bytes_Píxeles[Índice + 3];
                                            Matriz_Bytes_ARGB[Índice_ARGB + 1] = Matriz_Bytes_Píxeles[Índice + 2];
                                            Matriz_Bytes_ARGB[Índice_ARGB + 2] = Matriz_Bytes_Píxeles[Índice + 1];
                                            Matriz_Bytes_ARGB[Índice_ARGB + 3] = Matriz_Bytes_Píxeles[Índice];
                                        }
                                    }

                                    // Calculate the RGB channels.
                                    Matriz_Bytes_RGB = new byte[(Ancho * Alto) * 3];
                                    for (int Y = 0, Índice = 0, Índice_RGB = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                                    {
                                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento, Índice_RGB += 3)
                                        {
                                            // Use RGB as the exact byte order, with 3 bytes per pixel and no extra stride bytes.
                                            Matriz_Bytes_RGB[Índice_RGB] = Matriz_Bytes_Píxeles[Índice + 2];
                                            Matriz_Bytes_RGB[Índice_RGB + 1] = Matriz_Bytes_Píxeles[Índice + 1];
                                            Matriz_Bytes_RGB[Índice_RGB + 2] = Matriz_Bytes_Píxeles[Índice];
                                        }
                                    }

                                    // Only calculate the alpha channel.
                                    Matriz_Bytes_A = new byte[Ancho * Alto];
                                    for (int Y = 0, Índice = 0, Índice_Alfa = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                                    {
                                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento, Índice_Alfa++)
                                        {
                                            Matriz_Bytes_A[Índice_Alfa] = Matriz_Bytes_Píxeles[Índice + 3];
                                        }
                                    }

                                    // Only calculate the red channel.
                                    Matriz_Bytes_R = new byte[Ancho * Alto];
                                    for (int Y = 0, Índice = 0, Índice_Rojo = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                                    {
                                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento, Índice_Rojo++)
                                        {
                                            Matriz_Bytes_R[Índice_Rojo] = Matriz_Bytes_Píxeles[Índice + 2];
                                        }
                                    }

                                    // Only calculate the green channel.
                                    Matriz_Bytes_G = new byte[Ancho * Alto];
                                    for (int Y = 0, Índice = 0, Índice_Verde = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                                    {
                                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento, Índice_Verde++)
                                        {
                                            Matriz_Bytes_G[Índice_Verde] = Matriz_Bytes_Píxeles[Índice + 1];
                                        }
                                    }

                                    // Only calculate the blue channel.
                                    Matriz_Bytes_B = new byte[Ancho * Alto];
                                    for (int Y = 0, Índice = 0, Índice_Azul = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                                    {
                                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento, Índice_Azul++)
                                        {
                                            Matriz_Bytes_B[Índice_Azul] = Matriz_Bytes_Píxeles[Índice];
                                        }
                                    }
                                }
                                Matriz_Bytes_Píxeles = null;

                                if (Matriz_Bytes_ARGB != null && Matriz_Bytes_ARGB.Length > 0)
                                {
                                    Texto_Hashes += "\r\n[Hashes from the image ARGB pixels]\r\n";

                                    CRC_32 = Program.Calcular_CRC32(Matriz_Bytes_ARGB);
                                    Texto_Hexadecimal = Convert.ToString(CRC_32, 16);
                                    if (Texto_Hexadecimal.Length < 8) Texto_Hexadecimal = new string('0', 8 - Texto_Hexadecimal.Length) + Texto_Hexadecimal;
                                    if (Mayúsculas == CharacterCasing.Lower) Texto_Hexadecimal = Texto_Hexadecimal.ToLowerInvariant();
                                    else if (Mayúsculas == CharacterCasing.Upper) Texto_Hexadecimal = Texto_Hexadecimal.ToUpperInvariant();
                                    Texto_Hashes += "CRC32: " + Texto_Hexadecimal + "\r\n";

                                    //Texto_Hashes += "CRC64: Soon...\r\n";

                                    Lector_Hash = MD5.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_ARGB);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;

                                    // Use the 16 bytes from the MD5 hash to get the GUID hash.
                                    Texto_GUID = new Guid(Matriz_Bytes_Hash).ToString();
                                    if (Mayúsculas == CharacterCasing.Lower) Texto_GUID = Texto_GUID.ToLowerInvariant();
                                    else if (Mayúsculas == CharacterCasing.Upper) Texto_GUID = Texto_GUID.ToUpperInvariant();
                                    Texto_Hashes += "GUID: " + Texto_GUID + "\r\n";
                                    Texto_GUID = null;

                                    Texto_Hashes += "MD5: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = RIPEMD160Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_ARGB);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "RIPEMD160: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA1Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_ARGB);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA1: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA256Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_ARGB);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA256: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA384Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_ARGB);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA384: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA512Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_ARGB);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA512: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    /*Texto_UUID = UUID.NameUUIDFromBytes(Matriz_Bytes_ARGB).ToString();
                                    if (Mayúsculas == CharacterCasing.Lower) Texto_UUID = Texto_UUID.ToLowerInvariant();
                                    else if (Mayúsculas == CharacterCasing.Upper) Texto_UUID = Texto_UUID.ToUpperInvariant();
                                    Texto_Hashes += "UUID: " + Texto_UUID + "\r\n";
                                    Texto_UUID = null;*/

                                    Matriz_Bytes_ARGB = null;
                                }
                                if (Matriz_Bytes_RGB != null && Matriz_Bytes_RGB.Length > 0)
                                {
                                    Texto_Hashes += "\r\n[Hashes from the image RGB pixels]\r\n";

                                    CRC_32 = Program.Calcular_CRC32(Matriz_Bytes_RGB);
                                    Texto_Hexadecimal = Convert.ToString(CRC_32, 16);
                                    if (Texto_Hexadecimal.Length < 8) Texto_Hexadecimal = new string('0', 8 - Texto_Hexadecimal.Length) + Texto_Hexadecimal;
                                    if (Mayúsculas == CharacterCasing.Lower) Texto_Hexadecimal = Texto_Hexadecimal.ToLowerInvariant();
                                    else if (Mayúsculas == CharacterCasing.Upper) Texto_Hexadecimal = Texto_Hexadecimal.ToUpperInvariant();
                                    Texto_Hashes += "CRC32: " + Texto_Hexadecimal + "\r\n";

                                    //Texto_Hashes += "CRC64: Soon...\r\n";

                                    Lector_Hash = MD5.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_RGB);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;

                                    // Use the 16 bytes from the MD5 hash to get the GUID hash.
                                    Texto_GUID = new Guid(Matriz_Bytes_Hash).ToString();
                                    if (Mayúsculas == CharacterCasing.Lower) Texto_GUID = Texto_GUID.ToLowerInvariant();
                                    else if (Mayúsculas == CharacterCasing.Upper) Texto_GUID = Texto_GUID.ToUpperInvariant();
                                    Texto_Hashes += "GUID: " + Texto_GUID + "\r\n";
                                    Texto_GUID = null;

                                    Texto_Hashes += "MD5: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = RIPEMD160Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_RGB);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "RIPEMD160: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA1Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_RGB);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA1: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA256Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_RGB);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA256: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA384Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_RGB);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA384: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA512Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_RGB);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA512: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    /*Texto_UUID = UUID.NameUUIDFromBytes(Matriz_Bytes_RGB).ToString();
                                    if (Mayúsculas == CharacterCasing.Lower) Texto_UUID = Texto_UUID.ToLowerInvariant();
                                    else if (Mayúsculas == CharacterCasing.Upper) Texto_UUID = Texto_UUID.ToUpperInvariant();
                                    Texto_Hashes += "UUID: " + Texto_UUID + "\r\n";
                                    Texto_UUID = null;*/

                                    Matriz_Bytes_RGB = null;
                                }
                                if (Matriz_Bytes_A != null && Matriz_Bytes_A.Length > 0)
                                {
                                    Texto_Hashes += "\r\n[Hashes from the image alpha pixels]\r\n";

                                    CRC_32 = Program.Calcular_CRC32(Matriz_Bytes_A);
                                    Texto_Hexadecimal = Convert.ToString(CRC_32, 16);
                                    if (Texto_Hexadecimal.Length < 8) Texto_Hexadecimal = new string('0', 8 - Texto_Hexadecimal.Length) + Texto_Hexadecimal;
                                    if (Mayúsculas == CharacterCasing.Lower) Texto_Hexadecimal = Texto_Hexadecimal.ToLowerInvariant();
                                    else if (Mayúsculas == CharacterCasing.Upper) Texto_Hexadecimal = Texto_Hexadecimal.ToUpperInvariant();
                                    Texto_Hashes += "CRC32: " + Texto_Hexadecimal + "\r\n";

                                    //Texto_Hashes += "CRC64: Soon...\r\n";

                                    Lector_Hash = MD5.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_A);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;

                                    // Use the 16 bytes from the MD5 hash to get the GUID hash.
                                    Texto_GUID = new Guid(Matriz_Bytes_Hash).ToString();
                                    if (Mayúsculas == CharacterCasing.Lower) Texto_GUID = Texto_GUID.ToLowerInvariant();
                                    else if (Mayúsculas == CharacterCasing.Upper) Texto_GUID = Texto_GUID.ToUpperInvariant();
                                    Texto_Hashes += "GUID: " + Texto_GUID + "\r\n";
                                    Texto_GUID = null;

                                    Texto_Hashes += "MD5: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = RIPEMD160Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_A);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "RIPEMD160: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA1Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_A);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA1: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA256Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_A);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA256: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA384Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_A);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA384: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA512Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_A);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA512: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    /*Texto_UUID = UUID.NameUUIDFromBytes(Matriz_Bytes_A).ToString();
                                    if (Mayúsculas == CharacterCasing.Lower) Texto_UUID = Texto_UUID.ToLowerInvariant();
                                    else if (Mayúsculas == CharacterCasing.Upper) Texto_UUID = Texto_UUID.ToUpperInvariant();
                                    Texto_Hashes += "UUID: " + Texto_UUID + "\r\n";
                                    Texto_UUID = null;*/

                                    Matriz_Bytes_A = null;
                                }
                                if (Matriz_Bytes_R != null && Matriz_Bytes_R.Length > 0)
                                {
                                    Texto_Hashes += "\r\n[Hashes from the image red pixels]\r\n";

                                    CRC_32 = Program.Calcular_CRC32(Matriz_Bytes_R);
                                    Texto_Hexadecimal = Convert.ToString(CRC_32, 16);
                                    if (Texto_Hexadecimal.Length < 8) Texto_Hexadecimal = new string('0', 8 - Texto_Hexadecimal.Length) + Texto_Hexadecimal;
                                    if (Mayúsculas == CharacterCasing.Lower) Texto_Hexadecimal = Texto_Hexadecimal.ToLowerInvariant();
                                    else if (Mayúsculas == CharacterCasing.Upper) Texto_Hexadecimal = Texto_Hexadecimal.ToUpperInvariant();
                                    Texto_Hashes += "CRC32: " + Texto_Hexadecimal + "\r\n";

                                    //Texto_Hashes += "CRC64: Soon...\r\n";

                                    Lector_Hash = MD5.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_R);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;

                                    // Use the 16 bytes from the MD5 hash to get the GUID hash.
                                    Texto_GUID = new Guid(Matriz_Bytes_Hash).ToString();
                                    if (Mayúsculas == CharacterCasing.Lower) Texto_GUID = Texto_GUID.ToLowerInvariant();
                                    else if (Mayúsculas == CharacterCasing.Upper) Texto_GUID = Texto_GUID.ToUpperInvariant();
                                    Texto_Hashes += "GUID: " + Texto_GUID + "\r\n";
                                    Texto_GUID = null;

                                    Texto_Hashes += "MD5: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = RIPEMD160Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_R);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "RIPEMD160: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA1Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_R);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA1: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA256Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_R);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA256: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA384Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_R);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA384: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA512Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_R);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA512: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    /*Texto_UUID = UUID.NameUUIDFromBytes(Matriz_Bytes_R).ToString();
                                    if (Mayúsculas == CharacterCasing.Lower) Texto_UUID = Texto_UUID.ToLowerInvariant();
                                    else if (Mayúsculas == CharacterCasing.Upper) Texto_UUID = Texto_UUID.ToUpperInvariant();
                                    Texto_Hashes += "UUID: " + Texto_UUID + "\r\n";
                                    Texto_UUID = null;*/

                                    Matriz_Bytes_R = null;
                                }
                                if (Matriz_Bytes_G != null && Matriz_Bytes_G.Length > 0)
                                {
                                    Texto_Hashes += "\r\n[Hashes from the image green pixels]\r\n";

                                    CRC_32 = Program.Calcular_CRC32(Matriz_Bytes_G);
                                    Texto_Hexadecimal = Convert.ToString(CRC_32, 16);
                                    if (Texto_Hexadecimal.Length < 8) Texto_Hexadecimal = new string('0', 8 - Texto_Hexadecimal.Length) + Texto_Hexadecimal;
                                    if (Mayúsculas == CharacterCasing.Lower) Texto_Hexadecimal = Texto_Hexadecimal.ToLowerInvariant();
                                    else if (Mayúsculas == CharacterCasing.Upper) Texto_Hexadecimal = Texto_Hexadecimal.ToUpperInvariant();
                                    Texto_Hashes += "CRC32: " + Texto_Hexadecimal + "\r\n";

                                    //Texto_Hashes += "CRC64: Soon...\r\n";

                                    Lector_Hash = MD5.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_G);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;

                                    // Use the 16 bytes from the MD5 hash to get the GUID hash.
                                    Texto_GUID = new Guid(Matriz_Bytes_Hash).ToString();
                                    if (Mayúsculas == CharacterCasing.Lower) Texto_GUID = Texto_GUID.ToLowerInvariant();
                                    else if (Mayúsculas == CharacterCasing.Upper) Texto_GUID = Texto_GUID.ToUpperInvariant();
                                    Texto_Hashes += "GUID: " + Texto_GUID + "\r\n";
                                    Texto_GUID = null;

                                    Texto_Hashes += "MD5: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = RIPEMD160Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_G);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "RIPEMD160: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA1Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_G);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA1: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA256Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_G);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA256: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA384Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_G);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA384: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA512Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_G);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA512: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    /*Texto_UUID = UUID.NameUUIDFromBytes(Matriz_Bytes_G).ToString();
                                    if (Mayúsculas == CharacterCasing.Lower) Texto_UUID = Texto_UUID.ToLowerInvariant();
                                    else if (Mayúsculas == CharacterCasing.Upper) Texto_UUID = Texto_UUID.ToUpperInvariant();
                                    Texto_Hashes += "UUID: " + Texto_UUID + "\r\n";
                                    Texto_UUID = null;*/

                                    Matriz_Bytes_G = null;
                                }
                                if (Matriz_Bytes_B != null && Matriz_Bytes_B.Length > 0)
                                {
                                    Texto_Hashes += "\r\n[Hashes from the image blue pixels]\r\n";

                                    CRC_32 = Program.Calcular_CRC32(Matriz_Bytes_B);
                                    Texto_Hexadecimal = Convert.ToString(CRC_32, 16);
                                    if (Texto_Hexadecimal.Length < 8) Texto_Hexadecimal = new string('0', 8 - Texto_Hexadecimal.Length) + Texto_Hexadecimal;
                                    if (Mayúsculas == CharacterCasing.Lower) Texto_Hexadecimal = Texto_Hexadecimal.ToLowerInvariant();
                                    else if (Mayúsculas == CharacterCasing.Upper) Texto_Hexadecimal = Texto_Hexadecimal.ToUpperInvariant();
                                    Texto_Hashes += "CRC32: " + Texto_Hexadecimal + "\r\n";

                                    //Texto_Hashes += "CRC64: Soon...\r\n";

                                    Lector_Hash = MD5.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_B);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;

                                    // Use the 16 bytes from the MD5 hash to get the GUID hash.
                                    Texto_GUID = new Guid(Matriz_Bytes_Hash).ToString();
                                    if (Mayúsculas == CharacterCasing.Lower) Texto_GUID = Texto_GUID.ToLowerInvariant();
                                    else if (Mayúsculas == CharacterCasing.Upper) Texto_GUID = Texto_GUID.ToUpperInvariant();
                                    Texto_Hashes += "GUID: " + Texto_GUID + "\r\n";
                                    Texto_GUID = null;

                                    Texto_Hashes += "MD5: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = RIPEMD160Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_B);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "RIPEMD160: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA1Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_B);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA1: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA256Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_B);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA256: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA384Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_B);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA384: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    Lector_Hash = SHA512Managed.Create();
                                    Matriz_Bytes_Hash = Lector_Hash.ComputeHash(Matriz_Bytes_B);
                                    Lector_Hash.Dispose();
                                    Lector_Hash = null;
                                    Texto_Hashes += "SHA512: " + Convertir_Matriz_Bytes_Hexadecimal(Matriz_Bytes_Hash, Mayúsculas) + "\r\n";
                                    Matriz_Bytes_Hash = null;

                                    /*Texto_UUID = UUID.NameUUIDFromBytes(Matriz_Bytes_B).ToString();
                                    if (Mayúsculas == CharacterCasing.Lower) Texto_UUID = Texto_UUID.ToLowerInvariant();
                                    else if (Mayúsculas == CharacterCasing.Upper) Texto_UUID = Texto_UUID.ToUpperInvariant();
                                    Texto_Hashes += "UUID: " + Texto_UUID + "\r\n";
                                    Texto_UUID = null;*/

                                    Matriz_Bytes_B = null;
                                }
                            }
                            else
                            {
                                Texto_Hashes += "\r\n[Hashes from the image pixels]\r\n";
                                Texto_Hashes += "The file can't be loaded as an image";
                            }
                        }
                        else Texto_Hashes = "The file is empty";
                        Lector.Close();
                        Lector.Dispose();
                        Lector = null;
                    }
                    else Texto_Hashes += "The file can't be loaded";
                }
                else if (!string.IsNullOrEmpty(TextBox_Ruta.Text))
                {
                    Texto_Hashes += "The file doesn't exist";
                }
                else
                {
                    Texto_Hashes += "Drag and drop any file or image to calculate it's hashes";
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { TextBox_Hashes.Text = Texto_Hashes; }
        }

        private void TextBox_Tamaño_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox_Tamaño.Refresh();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_Hashes_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox_Hashes.Refresh();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
