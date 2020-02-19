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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Minecraft_Tools
{
    public partial class Ventana_Reconstructor_Estructura_Archivos_Recursos: Form
    {
        public Ventana_Reconstructor_Estructura_Archivos_Recursos()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Resource Files Structure Rebuilder by Jupisoft for " + Program.Texto_Usuario;

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

        internal static string Ruta_Minecraft = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft";
        internal static string Ruta_Salida = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        private void Ventana_Reconstructor_Estructura_Archivos_Recursos_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                Ocupado = true;
                TextBox_Ruta.Text = Ruta_Minecraft;
                TextBox_Ruta_Salida.Text = Ruta_Salida;
                //Registro_Cargar_Opciones();
                Ocupado = false;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Reconstructor_Estructura_Archivos_Recursos_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Reconstructor_Estructura_Archivos_Recursos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Reconstructor_Estructura_Archivos_Recursos_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Reconstructor_Estructura_Archivos_Recursos_KeyDown(object sender, KeyEventArgs e)
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

        private void TextBox_Ruta_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Ruta_Minecraft = TextBox_Ruta.Text;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_Ruta_Salida_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Ruta_Salida = TextBox_Ruta_Salida.Text;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Reconstruir_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string Ruta_Recursos = Ruta_Minecraft + "\\assets";
                if (!string.IsNullOrEmpty(Ruta_Recursos) && Directory.Exists(Ruta_Recursos))
                {
                    if (MessageBox.Show(this, "Do you wish to copy and rename all the resources found in the specified Minecraft assets folder to the output folder?\r\nYour original files won't be touched and no file will be overwritten.\r\nNote: it might take a couple of minutes to finish, so please wait...", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string Ruta_Índices = Ruta_Recursos + "\\indexes";
                        if (Directory.Exists(Ruta_Índices))
                        {
                            string[] Matriz_Rutas = Directory.GetFiles(Ruta_Índices, "*.json", SearchOption.TopDirectoryOnly);
                            if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                            {
                                if (Matriz_Rutas.Length > 1) Array.Sort(Matriz_Rutas);
                                Ruta_Índices = Matriz_Rutas[Matriz_Rutas.Length - 1];
                                Matriz_Rutas = null;
                                FileStream Lector = new FileStream(Ruta_Índices, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                                if (Lector != null && Lector.Length > 0L)
                                {
                                    bool Formato_1_14 = false;
                                    try
                                    {
                                        if (decimal.Parse(Path.GetFileNameWithoutExtension(Ruta_Índices)) >= 1.14m)
                                        {
                                            Formato_1_14 = true;
                                        }
                                    }
                                    catch { Formato_1_14 = false; }
                                    Formato_1_14 = true;
                                    Lector.Seek(0L, SeekOrigin.Begin);
                                    StreamReader Lector_Texto = new StreamReader(Lector, Encoding.UTF8);
                                    if (Lector_Texto != null)
                                    {
                                        List<string> Lista_Líneas = new List<string>();
                                        while (!Lector_Texto.EndOfStream)
                                        {
                                            string Línea = Lector_Texto.ReadLine();
                                            if (!string.IsNullOrEmpty(Línea))
                                            {
                                                Lista_Líneas.Add(Línea);
                                            }
                                        }
                                        Lector_Texto.Close();
                                        Lector_Texto.Dispose();
                                        Lector_Texto = null;
                                        Lector.Close();
                                        Lector.Dispose();
                                        Lector = null;
                                        if (Lista_Líneas.Count > 0)
                                        {
                                            bool Formato_Línea_Única = Lista_Líneas.Count == 1;
                                            Dictionary<string, string> Diccionario_Hash_Nombres = new Dictionary<string, string>();
                                            bool Objetos_Encontrados = false;
                                            string Texto_Hash = null;
                                            string Texto_Nombre = null;
                                            if (!Formato_Línea_Única)
                                            {
                                                foreach (string Línea in Lista_Líneas)
                                                {
                                                    if (!string.IsNullOrEmpty(Línea))
                                                    {
                                                        if (!Objetos_Encontrados)
                                                        {
                                                            if (Línea.ToLowerInvariant().Contains("\"objects\""))
                                                            {
                                                                Objetos_Encontrados = true;
                                                                continue;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (Línea.ToLowerInvariant().Contains("\"hash\""))
                                                            {
                                                                int Índice_Inicio = Línea.IndexOf('\"');
                                                                if (Índice_Inicio > -1) // Ignore the first '"'
                                                                {
                                                                    Índice_Inicio = Línea.IndexOf('\"', Índice_Inicio + 1);
                                                                    if (Índice_Inicio > -1) // Ignore the second '"'
                                                                    {
                                                                        Índice_Inicio = Línea.IndexOf('\"', Índice_Inicio + 1); // Find the third '"'
                                                                        if (Índice_Inicio > -1)
                                                                        {
                                                                            int Índice_Fin = Línea.IndexOf('\"', Índice_Inicio + 1);
                                                                            if (Índice_Fin > -1) // Find the fourth '"'
                                                                            {
                                                                                Texto_Hash = Línea.Substring(Índice_Inicio + 1, (Índice_Fin - Índice_Inicio) - 1);
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else if (Línea.ToLowerInvariant().Contains("\"size\""))
                                                            {
                                                                continue; // Ignore for now...
                                                            }
                                                            else if (Línea.Contains("\"") && Línea.Contains(":")/* && Línea.Contains("{")*/ && !Línea.Contains("}"))
                                                            {
                                                                int Índice_Inicio = Línea.IndexOf('\"');
                                                                if (Índice_Inicio > -1)
                                                                {
                                                                    int Índice_Fin = Línea.IndexOf('\"', Índice_Inicio + 1);
                                                                    if (Índice_Fin > -1)
                                                                    {
                                                                        Texto_Nombre = Línea.Substring(Índice_Inicio + 1, (Índice_Fin - Índice_Inicio) - 1);
                                                                    }
                                                                }
                                                            }

                                                            if (!string.IsNullOrEmpty(Texto_Hash) && !string.IsNullOrEmpty(Texto_Nombre))
                                                            {
                                                                if (!Diccionario_Hash_Nombres.ContainsKey(Texto_Hash)) Diccionario_Hash_Nombres.Add(Texto_Hash, Texto_Nombre);
                                                                //else MessageBox.Show(Texto_Hash, Texto_Nombre);
                                                                Texto_Hash = null;
                                                                Texto_Nombre = null;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else // Latest format with a single line of text.
                                            {
                                                foreach (string Línea in Lista_Líneas)
                                                {
                                                    if (!string.IsNullOrEmpty(Línea))
                                                    {/*if (!Objetos_Encontrados)
                                                    {
                                                        if (Línea.Contains("\"objects\""))
                                                        {
                                                            Objetos_Encontrados = true;
                                                        }
                                                    }*/
                                                        for (int Índice = 0; Índice < Línea.Length;)
                                                        {
                                                            int Índice_Objetos = Línea.IndexOf("\"objects\"", Índice);
                                                            if (Índice_Objetos > -1)
                                                            {
                                                                Índice = Índice_Objetos + 9;
                                                            }

                                                            int Índice_Nombre_Inicio = Línea.IndexOf('\"', Índice);
                                                            if (Índice_Nombre_Inicio > -1) // Name start.
                                                            {
                                                                int Índice_Nombre_Fin = Línea.IndexOf('\"', Índice_Nombre_Inicio + 1);
                                                                if (Índice_Nombre_Fin > -1) // Name end.
                                                                {
                                                                    Texto_Nombre = Línea.Substring(Índice_Nombre_Inicio + 1, (Índice_Nombre_Fin - Índice_Nombre_Inicio) - 1);
                                                                    Índice = Índice_Nombre_Fin + 1;
                                                                }
                                                            }

                                                            int Índice_Hash = Línea.IndexOf("\"hash\"", Índice);
                                                            if (Índice_Hash > -1)
                                                            {
                                                                int Índice_Hash_Inicio = Línea.IndexOf('\"', Índice_Hash + 6);
                                                                if (Índice_Hash_Inicio > -1) // Hash start.
                                                                {
                                                                    int Índice_Hash_Fin = Línea.IndexOf('\"', Índice_Hash_Inicio + 1);
                                                                    if (Índice_Hash_Fin > -1) // Hash end.
                                                                    {
                                                                        Texto_Hash = Línea.Substring(Índice_Hash_Inicio + 1, (Índice_Hash_Fin - Índice_Hash_Inicio) - 1);
                                                                        Índice = Índice_Hash_Fin + 1;
                                                                    }
                                                                }
                                                            }

                                                            int Índice_Size = Línea.IndexOf("\"size\"", Índice);
                                                            if (Índice_Size > -1)
                                                            {
                                                                // Just ignore the size for now.
                                                                Índice = Índice_Size + 6;
                                                            }

                                                            if (!string.IsNullOrEmpty(Texto_Hash) && !string.IsNullOrEmpty(Texto_Nombre))
                                                            {
                                                                if (!Diccionario_Hash_Nombres.ContainsKey(Texto_Hash)) Diccionario_Hash_Nombres.Add(Texto_Hash, Texto_Nombre);
                                                                Texto_Hash = null;
                                                                Texto_Nombre = null;
                                                            }
                                                            else break;
                                                        }
                                                    }
                                                }
                                            }
                                            Lista_Líneas = null;
                                            if (Diccionario_Hash_Nombres.Count > 0)
                                            {
                                                string Ruta = Ruta_Salida + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " Resource Structure";
                                                if (!Directory.Exists(Ruta))
                                                {
                                                    Program.Crear_Carpetas(Ruta);
                                                    if (Directory.Exists(Ruta))
                                                    {
                                                        string[] Matriz_Rutas_Recursos = Directory.GetFiles(Ruta_Recursos + "\\objects", "*", SearchOption.AllDirectories);
                                                        if (Matriz_Rutas_Recursos != null && Matriz_Rutas_Recursos.Length > 0)
                                                        {
                                                            foreach (KeyValuePair<string, string> Entrada in Diccionario_Hash_Nombres)
                                                            {
                                                                foreach (string Ruta_Recurso in Matriz_Rutas_Recursos)
                                                                {
                                                                    string Nombre = Path.GetFileNameWithoutExtension(Ruta_Recurso);
                                                                    if (string.Compare(Entrada.Key, Nombre, true) == 0) // Found the resource.
                                                                    {
                                                                        string Ruta_Salida_Recurso = Ruta + "\\" + Entrada.Value;
                                                                        if (!File.Exists(Ruta_Salida_Recurso))
                                                                        {
                                                                            Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Salida_Recurso));
                                                                            File.Copy(Ruta_Recurso, Ruta_Salida_Recurso, false);
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            Program.Ejecutar_Ruta(Ruta, ProcessWindowStyle.Maximized);
                                                            Matriz_Rutas_Recursos = null;
                                                            SystemSounds.Asterisk.Play();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else Lista_Líneas = null;
                                    }
                                }
                            }
                        }
                    }
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
