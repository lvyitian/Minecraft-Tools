using Microsoft.Win32;
using Minecraft_Tools.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Buscador_Archivos_Duplicados : Form
    {
        public Ventana_Buscador_Archivos_Duplicados()
        {
            InitializeComponent();
        }

        internal class Comparador_String : IComparer<string>
        {
            public int Compare(string X, string Y)
            {
                return string.Compare(X, Y);
            }
        }

        internal class Comparador_FileInfo_Nombre : IComparer<FileInfo>
        {
            public int Compare(FileInfo X, FileInfo Y)
            {
                return string.Compare(X.Name, Y.Name);
            }
        }

        internal class Comparador_DirectoryInfo_Nombre : IComparer<DirectoryInfo>
        {
            public int Compare(DirectoryInfo X, DirectoryInfo Y)
            {
                return string.Compare(X.Name, Y.Name);
            }
        }

        internal class Comparador_FileInfo : IComparer<FileInfo>
        {
            public int Compare(FileInfo X, FileInfo Y)
            {
                if (X.Length < Y.Length) return -1;
                else if (X.Length > Y.Length) return 1;
                else
                {
                    if (X.Name.Length < Y.Name.Length) return -1;
                    else if (X.Name.Length > Y.Name.Length) return 1;
                    else return string.Compare(X.Name, Y.Name);
                }
            }
        }

        internal readonly string Texto_Título = "Duplicated Files Finder by Jupisoft for " + Program.Texto_Usuario;
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

        private void Ventana_Buscador_Archivos_Duplicados_Load(object sender, EventArgs e)
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

        private void Ventana_Buscador_Archivos_Duplicados_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Buscador_Archivos_Duplicados_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Buscador_Archivos_Duplicados_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Buscador_Archivos_Duplicados_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Buscador_Archivos_Duplicados_DragDrop(object sender, DragEventArgs e)
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
                                    TextBox_Ruta.Text = Directory.Exists(Ruta) ? Ruta : Path.GetDirectoryName(Ruta);
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

        private void Ventana_Buscador_Archivos_Duplicados_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Normal) this.WindowState = FormWindowState.Maximized;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Buscador_Archivos_Duplicados_KeyDown(object sender, KeyEventArgs e)
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

        internal string[] Buscar_Carpetas(string Ruta)
        {
            try
            {
                return Directory.GetDirectories(Ruta, "*", SearchOption.TopDirectoryOnly);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        internal FileInfo[] Buscar_Archivos(string Ruta)
        {
            try
            {
                return new DirectoryInfo(Ruta).GetFiles("*", SearchOption.TopDirectoryOnly);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        internal List<string> Buscar_Archivos_Duplicados(FileInfo[] Matriz_Archivos)
        {
            try
            {
                Lector = new FileStream(Application.StartupPath + "\\" + Program.Obtener_Nombre_Temporal() + " Duplicates.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                Lector_Texto = new StreamWriter(Lector, Encoding.Unicode);
                Lector.Seek(Lector.Length, SeekOrigin.Begin);
                if (Matriz_Archivos != null && Matriz_Archivos.Length > 1)
                {
                    Array.Sort(Matriz_Archivos, new Comparador_FileInfo());
                    Dictionary<long, List<string>> Diccionario_Tamaños_Lista_Archivos = new Dictionary<long, List<string>>();
                    for (int Índice = 0; Índice < Matriz_Archivos.Length - 1; Índice++)
                    {
                        for (int Subíndice = Índice + 1; Subíndice < Matriz_Archivos.Length; Subíndice++)
                        {
                            if (Índice != Subíndice && Matriz_Archivos[Índice].Length == Matriz_Archivos[Subíndice].Length && Matriz_Archivos[Índice].Length > 0L)
                            {
                                if (!Diccionario_Tamaños_Lista_Archivos.ContainsKey(Matriz_Archivos[Índice].Length))
                                {
                                    Diccionario_Tamaños_Lista_Archivos.Add(Matriz_Archivos[Índice].Length, new List<string>());
                                    if (!Diccionario_Tamaños_Lista_Archivos[Matriz_Archivos[Índice].Length].Contains(Matriz_Archivos[Índice].FullName))
                                    {
                                        Diccionario_Tamaños_Lista_Archivos[Matriz_Archivos[Índice].Length].Add(Matriz_Archivos[Índice].FullName);
                                    }
                                    if (!Diccionario_Tamaños_Lista_Archivos[Matriz_Archivos[Subíndice].Length].Contains(Matriz_Archivos[Subíndice].FullName))
                                    {
                                        Diccionario_Tamaños_Lista_Archivos[Matriz_Archivos[Subíndice].Length].Add(Matriz_Archivos[Subíndice].FullName);
                                    }
                                }
                            }
                        }
                    }
                    if (Diccionario_Tamaños_Lista_Archivos.Count > 0)
                    {
                        List<string> Lista_Archivos_Duplicados = new List<string>();
                        foreach (KeyValuePair<long, List<string>> Entrada in Diccionario_Tamaños_Lista_Archivos)
                        {
                            List<uint> Lista_CRC_32 = new List<uint>();
                            for (int Índice = 0; Índice < Entrada.Value.Count; Índice++)
                            {
                                uint CRC_32 = Program.Obtener_CRC_32(Entrada.Value[Índice]);
                                if (!Lista_CRC_32.Contains(CRC_32)) Lista_CRC_32.Add(CRC_32);
                                else Lista_Archivos_Duplicados.Add(Entrada.Value[Índice]);
                            }
                        }
                        if (Lista_Archivos_Duplicados.Count > 0)
                        {
                            string Ruta_Salida = Path.GetDirectoryName(Lista_Archivos_Duplicados[0]) + "\\" + Program.Obtener_Nombre_Temporal() + " Duplicates";
                            if (CheckBox_Mover_Archivos.Checked) Program.Crear_Carpetas(Ruta_Salida);
                            string Texto = null;
                            foreach (string Ruta in Lista_Archivos_Duplicados)
                            {
                                Texto += Ruta + "\r\n";
                                if (CheckBox_Mover_Archivos.Checked)
                                {
                                    FileAttributes Atributos = Program.Quitar_Atributo_Sólo_Lectura(Ruta);
                                    File.Move(Ruta, Ruta_Salida + "\\" + Path.GetFileName(Ruta));
                                    try { new FileInfo(Ruta).Attributes = Atributos; } // Restore attributes.
                                    catch { }
                                }
                            }
                            TextBox_Archivos.Text = Texto.TrimEnd("\r\n".ToCharArray());
                        }
                    }
                }
                if (Lector_Texto != null)
                {
                    Lector_Texto.Flush();
                    Lector_Texto.Close();
                    Lector_Texto.Dispose();
                    Lector_Texto = null;
                }
                if (Lector != null)
                {
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        internal void Buscar_FL_Studio_9()
        {
            try
            {
                string Ruta_C = @"C:\Program Files (x86)\Image-Line\FL Studio 9";
                string Ruta_F = @"F:\FL Studio 9";

                List<string> Lista_Rutas_C = new List<string>();
                List<string> Lista_Rutas_F = new List<string>();

                Lista_Rutas_C.AddRange(Directory.GetDirectories(Ruta_C, "*", SearchOption.AllDirectories));
                Lista_Rutas_C.AddRange(Directory.GetFiles(Ruta_C, "*", SearchOption.AllDirectories));

                Lista_Rutas_F.AddRange(Directory.GetDirectories(Ruta_F, "*", SearchOption.AllDirectories));
                Lista_Rutas_F.AddRange(Directory.GetFiles(Ruta_F, "*", SearchOption.AllDirectories));

                Lista_Rutas_C.Sort(new Comparador_String());
                Lista_Rutas_F.Sort(new Comparador_String());

                List<string> Lista_Rutas_Largas_Desaparecidas = new List<string>();
                string Texto = null;
                foreach (string Ruta in Lista_Rutas_C)
                {
                    string Ruta_Temporal = Ruta.Replace(Ruta_C, Ruta_F).Replace(" \\ ", "\\").Replace("\\ ", "\\").Trim();
                    if (!Lista_Rutas_F.Contains(Ruta_Temporal))
                    {
                        Lista_Rutas_Largas_Desaparecidas.Add(Ruta_Temporal);
                        Texto += (Ruta.Length < 256 ? null : "[256] ") + Ruta_Temporal + "\r\n";
                    }
                }
                if (Lista_Rutas_Largas_Desaparecidas.Count > 0)
                {
                    Clipboard.SetText(Texto);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Comparar_CRC_32_Bluestacks()
        {
            try
            {
                string Ruta_C = @"C:\Users\All Users\BlueStacks";
                string Ruta_F = @"C:\ProgramData\BlueStacks";

                List<string> Lista_Rutas_C = new List<string>();
                List<string> Lista_Rutas_F = new List<string>();

                //Lista_Rutas_C.AddRange(Directory.GetDirectories(Ruta_C, "*", SearchOption.AllDirectories));
                Lista_Rutas_C.AddRange(Directory.GetFiles(Ruta_C, "*", SearchOption.AllDirectories));

                //Lista_Rutas_F.AddRange(Directory.GetDirectories(Ruta_F, "*", SearchOption.AllDirectories));
                Lista_Rutas_F.AddRange(Directory.GetFiles(Ruta_F, "*", SearchOption.AllDirectories));

                Lista_Rutas_C.Sort(new Comparador_String());
                Lista_Rutas_F.Sort(new Comparador_String());

                List<string> Lista_Rutas_Largas_Desaparecidas = new List<string>();
                string Texto = null;
                foreach (string Ruta in Lista_Rutas_C)
                {
                    string Ruta_2 = Ruta.Replace(Ruta_C, Ruta_F);
                    uint CRC_32_C = Program.Obtener_CRC_32(Ruta);
                    uint CRC_32_F = Program.Obtener_CRC_32(Ruta_2);
                    if (CRC_32_C != CRC_32_F)
                    {
                        Texto += "[" + Program.Traducir_Número(CRC_32_C) + "] " + Ruta + "\r\n";
                        Texto += "[" + Program.Traducir_Número(CRC_32_F) + "] " + Ruta_2 + "\r\n\r\n";
                    }
                }
                if (string.IsNullOrEmpty(Texto)) Texto = "100 % equal...";
                if (!string.IsNullOrEmpty(Texto))
                {
                    Clipboard.SetText(Texto);
                    this.Activate();
                    MessageBox.Show(this, "CRC done!");
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Buscar_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(TextBox_Ruta.Text) && Directory.Exists(TextBox_Ruta.Text))
                {
                    Buscar_Archivos_Duplicados(Buscar_Archivos(TextBox_Ruta.Text));
                    if (CheckBox_Subcarpetas.Checked)
                    {
                        //Buscar_Carpetas(
                    }
                    /*Buscar_FL_Studio_9();
                    /*Comparar_CRC_32_Bluestacks();
                    /*this.Cursor = Cursors.WaitCursor;
                    Subproceso = new Thread(new ThreadStart(Subproceso_DoWork));
                    Subproceso.IsBackground = true;
                    Subproceso.Priority = ThreadPriority.Normal;
                    Subproceso.Start();
                    /*DirectoryInfo Carpeta = new DirectoryInfo(TextBox_Ruta.Text);
                    try
                    {
                        try { Lector_Texto.WriteLine(Carpeta.FullName); }
                        catch { Lector_Texto.WriteLine(); }

                        try { Lector_Texto.WriteLine("[Folder]"); }
                        catch { Lector_Texto.WriteLine(); }

                        try { Lector_Texto.WriteLine(Carpeta.Attributes.ToString()); }
                        catch { Lector_Texto.WriteLine(); }

                        try { Lector_Texto.WriteLine(Carpeta.CreationTimeUtc.Ticks.ToString()); }
                        catch { Lector_Texto.WriteLine(); }

                        try { Lector_Texto.WriteLine(Carpeta.LastWriteTimeUtc.Ticks.ToString()); }
                        catch { Lector_Texto.WriteLine(); }

                        try { Lector_Texto.WriteLine(Carpeta.LastAccessTimeUtc.Ticks.ToString()); }
                        catch { Lector_Texto.WriteLine(); }

                        Lector_Texto.Flush();
                    }
                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                    Buscar_Archivos_Carpetas(Carpeta, CheckBox_Subcarpetas.Checked);*/
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal Stopwatch Cronómetro = new Stopwatch();
        internal FileStream Lector = null;
        internal StreamWriter Lector_Texto = null;

        internal string[] Buscar_Archivos_Carpetas(DirectoryInfo Carpeta_Entrada, bool Subcarpetas)
        {
            try
            {
                try
                {
                    FileInfo[] Matriz_Archivos = Carpeta_Entrada.GetFiles("*", SearchOption.TopDirectoryOnly);
                    if (Matriz_Archivos != null && Matriz_Archivos.Length > 0)
                    {
                        if (Matriz_Archivos.Length > 1) Array.Sort(Matriz_Archivos, new Comparador_FileInfo_Nombre());
                        foreach (FileInfo Archivo in Matriz_Archivos)
                        {
                            try
                            {
                                try { Lector_Texto.WriteLine(Archivo.FullName); }
                                catch { Lector_Texto.WriteLine(); }

                                try { Lector_Texto.WriteLine(Archivo.Length.ToString()); }
                                catch { Lector_Texto.WriteLine(); }

                                try { Lector_Texto.WriteLine(Archivo.Attributes.ToString()); }
                                catch { Lector_Texto.WriteLine(); }

                                try { Lector_Texto.WriteLine(Archivo.CreationTimeUtc.Ticks.ToString()); }
                                catch { Lector_Texto.WriteLine(); }

                                try { Lector_Texto.WriteLine(Archivo.LastWriteTimeUtc.Ticks.ToString()); }
                                catch { Lector_Texto.WriteLine(); }

                                try { Lector_Texto.WriteLine(Archivo.LastAccessTimeUtc.Ticks.ToString()); }
                                catch { Lector_Texto.WriteLine(); }

                                /*try { Lector_Texto.WriteLine(Archivo); }
                                catch { Lector_Texto.WriteLine(); }*/

                                Lector_Texto.Flush();
                            }
                            catch (Exception Excepción)
                            {
                                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                                continue;
                            }
                        }
                    }
                    Matriz_Archivos = null;
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                try
                {
                    DirectoryInfo[] Matriz_Carpetas = Carpeta_Entrada.GetDirectories("*", SearchOption.TopDirectoryOnly);
                    if (Matriz_Carpetas != null && Matriz_Carpetas.Length > 0)
                    {
                        if (Matriz_Carpetas.Length > 1) Array.Sort(Matriz_Carpetas, new Comparador_DirectoryInfo_Nombre());
                        foreach (DirectoryInfo Carpeta in Matriz_Carpetas)
                        {
                            try
                            {
                                try { Lector_Texto.WriteLine(Carpeta.FullName); }
                                catch { Lector_Texto.WriteLine(); }

                                try { Lector_Texto.WriteLine("[Folder]"); }
                                catch { Lector_Texto.WriteLine(); }

                                try { Lector_Texto.WriteLine(Carpeta.Attributes.ToString()); }
                                catch { Lector_Texto.WriteLine(); }

                                try { Lector_Texto.WriteLine(Carpeta.CreationTimeUtc.Ticks.ToString()); }
                                catch { Lector_Texto.WriteLine(); }

                                try { Lector_Texto.WriteLine(Carpeta.LastWriteTimeUtc.Ticks.ToString()); }
                                catch { Lector_Texto.WriteLine(); }

                                try { Lector_Texto.WriteLine(Carpeta.LastAccessTimeUtc.Ticks.ToString()); }
                                catch { Lector_Texto.WriteLine(); }

                                /*try { Lector_Texto.WriteLine(Archivo); }
                                catch { Lector_Texto.WriteLine(); }*/

                                Lector_Texto.Flush();

                                if (Subcarpetas)
                                {
                                    Buscar_Archivos_Carpetas(Carpeta, Subcarpetas);
                                }
                            }
                            catch (Exception Excepción)
                            {
                                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                                continue;
                            }
                        }
                    }
                    Matriz_Carpetas = null;
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        internal Thread Subproceso = null;
        internal bool Subproceso_Activo = false;

        internal void Subproceso_DoWork()
        {
            try
            {
                string Letra_Unidad = Application.StartupPath[0].ToString().ToUpperInvariant();
                DriveInfo[] Matriz_Unidades = DriveInfo.GetDrives();
                if (Matriz_Unidades != null && Matriz_Unidades.Length > 0)
                {
                    foreach (DriveInfo Unidad in Matriz_Unidades)
                    {
                        if (Unidad != null && string.Compare(Unidad.RootDirectory.FullName[0].ToString(), Letra_Unidad, true) == 0)
                        {
                            long Espacio_Disponible = Math.Min(Unidad.AvailableFreeSpace, Unidad.TotalFreeSpace);
                            FileStream Lector = new FileStream(Application.StartupPath + "\\Memory", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                            for (;;)
                            {
                                try
                                {
                                    Lector.SetLength(Espacio_Disponible);
                                    break;
                                }
                                catch (Exception Excepción)
                                {
                                    Espacio_Disponible -= 1048576L; // 1 MB.
                                    Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                                    Lector.SetLength(0L); // Reset.
                                    continue;
                                }
                            }
                            Lector.Seek(0L, SeekOrigin.Begin);
                            byte[] Matriz_Bytes_256 = new byte[256];
                            for (int Índice = 0; Índice < 256; Índice++)
                            {
                                Matriz_Bytes_256[Índice] = (byte)Índice;
                            }
                            Matriz_Bytes_256 = Jupisoft_Encrypting_Decrypting.Encriptar_Matriz_Bytes(Matriz_Bytes_256, true, false, true, false);
                            byte[] Matriz_Bytes = new byte[1048576]; // 1 MB.
                            for (int Índice = 0; Índice < Matriz_Bytes.Length; Índice += Matriz_Bytes_256.Length)
                            {
                                Array.Copy(Matriz_Bytes_256, 0, Matriz_Bytes, Índice, Matriz_Bytes_256.Length);
                            }
                            Matriz_Bytes_256 = null;
                            long Errores_Bloque = 0L;
                            int Porcentaje_Anterior = 0;
                            for (long Índice_Bloque = 0L; Índice_Bloque <= Espacio_Disponible; Índice_Bloque += Matriz_Bytes.LongLength)
                            {
                                try
                                {
                                    Lector.Seek(Índice_Bloque, SeekOrigin.Begin);
                                    int Porcentaje = (int)((Índice_Bloque * 1000000L) / Espacio_Disponible);
                                    if (Porcentaje != Porcentaje_Anterior)
                                    {
                                        Porcentaje_Anterior = Porcentaje;
                                        Barra_Progreso.Invoke(new Invocación.Delegado_ProgressBar_Value(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso, Porcentaje });
                                    }
                                    Lector.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                }
                                catch (Exception Excepción)
                                {
                                    Errores_Bloque++;
                                    Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                                    continue;
                                }
                            }
                            //if (Errores_Bloque > 0)
                            {
                                this.Invoke(new Invocación.Delegado_IWin32Window_MessageBox(Invocación.Ejecutar_Delegado_IWin32Window_MessageBox), new object[] { this, "Bloques con errores: " + Program.Traducir_Número(Errores_Bloque) + " en " + Program.Traducir_Número(Espacio_Disponible) + " bytes.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information });
                            }
                            Lector.Close();
                            Lector.Dispose();
                            Lector = null;
                        }
                    }
                }
            }
            catch (ThreadAbortException) { } // Cancelado, no registrar el error
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally
            {
                this.Invoke(new Invocación.Delegado_Control_Cursor(Invocación.Ejecutar_Delegado_Control_Cursor), new object[] { this, Cursors.Default });
                Subproceso_Activo = false;
                Subproceso = null;
            }
        }
    }
}
