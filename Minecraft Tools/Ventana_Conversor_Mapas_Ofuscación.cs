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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Conversor_Mapas_Ofuscación : Form
    {
        public Ventana_Conversor_Mapas_Ofuscación()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Read-only path to the ProGuard GUI executable, used to actually deobfuscate the mappings converted with this tool.
        /// </summary>
        internal static readonly string Ruta_ProGuard = Application.StartupPath + "\\ProGuard 6.1.1\\bin\\proguardgui.bat";
        /// <summary>
        /// Path to temporary download the obfuscation mappings along with the client and server jars, it's where the deobfuscation will take place.
        /// </summary>
        internal static readonly string Ruta_Desofuscación = Application.StartupPath + "\\Deobfuscation";

        internal readonly string Texto_Título = "Obfuscation Mappings Converter by Jupisoft for " + Program.Texto_Usuario;
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

        internal BackgroundWorker Subproceso = null;

        internal static string Variable_Versión_Minecraft = "19w36a";
        internal static bool Variable_Cargar_Archivo = true;
        internal static string Variable_URL_Cliente_JAR = null;
        internal static string Variable_URL_Cliente_TXT = null;
        internal static string Variable_URL_Servidor_JAR = null;
        internal static string Variable_URL_Servidor_TXT = null;
        internal static string Variable_Ruta_Cliente_JAR = Ruta_Desofuscación + "\\client.jar";
        internal static string Variable_Ruta_Cliente_TXT = Ruta_Desofuscación + "\\client.txt";
        internal static string Variable_Ruta_Servidor_JAR = Ruta_Desofuscación + "\\server.jar";
        internal static string Variable_Ruta_Servidor_TXT = Ruta_Desofuscación + "\\server.txt";
        internal static string Variable_Ruta_Salida_Cliente_JAR = Ruta_Desofuscación + "\\test_client.jar";
        internal static string Variable_Ruta_Salida_Cliente_TXT = Ruta_Desofuscación + "\\test_client.txt";
        internal static string Variable_Ruta_Salida_Servidor_JAR = Ruta_Desofuscación + "\\test_server.jar";
        internal static string Variable_Ruta_Salida_Servidor_TXT = Ruta_Desofuscación + "\\test_server.txt";
        internal static int Variable_Archivos_Convertir = 0;

        private void Ventana_Conversor_Mapas_Ofuscación_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título + " - [You can also drop any JSON version file here]";
                //this.WindowState = FormWindowState.Maximized;
                Subproceso = new BackgroundWorker();
                Subproceso.DoWork += Subproceso_DoWork;
                Subproceso.ProgressChanged += Subproceso_ProgressChanged;
                Subproceso.RunWorkerCompleted += Subproceso_RunWorkerCompleted;
                if (Directory.Exists(Program.Ruta_Minecraft + "\\versions"))
                {
                    string[] Matriz_Carpetas = Directory.GetDirectories(Program.Ruta_Minecraft + "\\versions", "*", SearchOption.TopDirectoryOnly);
                    if (Matriz_Carpetas != null && Matriz_Carpetas.Length > 0)
                    {
                        foreach (string Carpeta in Matriz_Carpetas)
                        {
                            if (File.Exists(Carpeta + "\\" + Path.GetFileName(Carpeta) + ".json"))
                            {
                                ComboBox_Versión_Minecraft.Items.Add(Path.GetFileName(Carpeta));
                            }
                        }
                    }
                    Matriz_Carpetas = null;
                }
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
                if (ComboBox_Versión_Minecraft.Items.Contains(Variable_Versión_Minecraft)) ComboBox_Versión_Minecraft.Text = Variable_Versión_Minecraft;
                CheckBox_Cargar_Archivo.Checked = Variable_Cargar_Archivo;
                TextBox_URL_Cliente_JAR.Text = Variable_URL_Cliente_JAR;
                TextBox_URL_Cliente_TXT.Text = Variable_URL_Cliente_TXT;
                TextBox_URL_Servidor_JAR.Text = Variable_URL_Servidor_JAR;
                TextBox_URL_Servidor_TXT.Text = Variable_URL_Servidor_TXT;
                TextBox_Ruta_Cliente_JAR.Text = Variable_Ruta_Cliente_JAR;
                TextBox_Ruta_Cliente_TXT.Text = Variable_Ruta_Cliente_TXT;
                TextBox_Ruta_Servidor_JAR.Text = Variable_Ruta_Servidor_JAR;
                TextBox_Ruta_Servidor_TXT.Text = Variable_Ruta_Servidor_TXT;
                ComboBox_Archivos_Convertir.SelectedIndex = Variable_Archivos_Convertir;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Mapas_Ofuscación_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
                //Test_RAM();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Function that tries to copy part of the memory of the old Minecraft "inf-20100227", the one
        /// with the brick pyramids and the obsidian walls, since I believe it can't save those worlds.
        /// Test result: application crashed even with try-catch and also on the debugger. Failure.
        /// </summary>
        internal void Test_RAM()
        {
            try
            {
                Process[] Matriz_Procesos = Process.GetProcesses();
                if (Matriz_Procesos != null && Matriz_Procesos.Length > 0)
                {
                    foreach (Process Proceso in Matriz_Procesos)
                    {
                        try
                        {
                            string Nombre = "java"; // This will fail if there are more than one java app at once.
                            if (string.Compare(Proceso.ProcessName, Nombre, true) == 0)
                            {
                                if (Proceso.Threads != null && Proceso.Threads.Count > 0)
                                {
                                    foreach (ProcessThread Subproceso in Proceso.Threads)
                                    {
                                        try
                                        {
                                            if (Subproceso.Id == 11260) // This needs to change each time.
                                            {
                                                FileStream Lector = new FileStream(Application.StartupPath + "\\zTest_RAM.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                                Lector.SetLength(0L);
                                                Lector.Seek(0L, SeekOrigin.Begin);
                                                IntPtr Puntero = Subproceso.StartAddress;
                                                byte[] Matriz_Bytes = new byte[4096];
                                                for (long Índice_Byte = 0L; Índice_Byte < (long)256 * 1048576L; Índice_Byte += 4096L)
                                                {
                                                    try
                                                    {
                                                        Marshal.Copy(new IntPtr(Puntero.ToInt64() + Índice_Byte), Matriz_Bytes, 0, Matriz_Bytes.Length);
                                                        Lector.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                                        Lector.Flush();
                                                    }
                                                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                }
                                                Lector.Close();
                                                Lector.Dispose();
                                                Lector = null;
                                                SystemSounds.Asterisk.Play();
                                                return;
                                                //break;
                                            }
                                        }
                                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                    }
                                }
                                break;
                            }
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                    }
                }
                Matriz_Procesos = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Mapas_Ofuscación_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Mapas_Ofuscación_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Mapas_Ofuscación_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Conversor_Mapas_Ofuscación_DragDrop(object sender, DragEventArgs e)
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
                                    if (File.Exists(Ruta) || File.Exists(Ruta + "\\" + Path.GetFileName(Ruta) + ".json"))
                                    {
                                        if (Cargar_Archivo(Ruta))
                                        {
                                            Matriz_Rutas = null;
                                            return;
                                        }
                                    }
                                }
                            }
                            catch (Exception Excepción)
                            {
                                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                continue;
                            }
                        }
                        Matriz_Rutas = null;
                        SystemSounds.Beep.Play();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Conversor_Mapas_Ofuscación_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Conversor_Mapas_Ofuscación_KeyDown(object sender, KeyEventArgs e)
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

        private void ComboBox_Versión_Minecraft_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Versión_Minecraft = ComboBox_Versión_Minecraft.Text;
                if (Variable_Cargar_Archivo) Botón_Cargar_Archivo.PerformClick();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Cargar_Archivo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Cargar_Archivo = CheckBox_Cargar_Archivo.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Loads a JSON Minecraft version file into the memory and extracts it's possible URLs for the obfuscation mappings and JAR files.
        /// </summary>
        /// <param name="Ruta">Any valid file path to a JSON Minecraft version file.</param>
        internal bool Cargar_Archivo(string Ruta)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta) && new FileInfo(Ruta).Length > 0L)
                {
                    FileStream Lector_Entrada = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    Lector_Entrada.Seek(0L, SeekOrigin.Begin);
                    StreamReader Lector_Entrada_Texto = new StreamReader(Lector_Entrada, Encoding.UTF8, true);
                    string Texto_Versión = Lector_Entrada_Texto.ReadToEnd();
                    Lector_Entrada_Texto.Close();
                    Lector_Entrada_Texto.Dispose();
                    Lector_Entrada_Texto = null;
                    Lector_Entrada.Close();
                    Lector_Entrada.Dispose();
                    Lector_Entrada = null;
                    if (!string.IsNullOrEmpty(Texto_Versión))
                    {
                        // Search the content between these 2 groups?
                        //int Índice_Assets = Texto_Versión.IndexOf("assets");
                        //int Índice_Libraries = Texto_Versión.IndexOf("libraries");

                        // Hopefully this search trick will always work as expected.
                        string Texto_Cliente_JAR = "client.jar";
                        string Texto_Cliente_TXT = "client.txt";
                        string Texto_Servidor_JAR = "server.jar";
                        string Texto_Servidor_TXT = "server.txt";

                        int Índice_Cliente_JAR = Texto_Versión.IndexOf(Texto_Cliente_JAR);
                        int Índice_Cliente_TXT = Texto_Versión.IndexOf(Texto_Cliente_TXT);
                        int Índice_Servidor_JAR = Texto_Versión.IndexOf(Texto_Servidor_JAR);
                        int Índice_Servidor_TXT = Texto_Versión.IndexOf(Texto_Servidor_TXT);

                        if (Índice_Cliente_JAR > -1)
                        {
                            int Índice_Inicio = -1;
                            for (int Índice_Caracter = Índice_Cliente_JAR; Índice_Caracter >= 0; Índice_Caracter--)
                            {
                                if (Texto_Versión[Índice_Caracter] == '\"')
                                {
                                    Índice_Inicio = Índice_Caracter + 1; // Skip the start of the line.
                                    break;
                                }
                            }
                            int Índice_Fin = -1;
                            for (int Índice_Caracter = Índice_Cliente_JAR + Texto_Cliente_JAR.Length; Índice_Caracter < Texto_Versión.Length; Índice_Caracter++)
                            {
                                if (Texto_Versión[Índice_Caracter] == '\"')
                                {
                                    Índice_Fin = Índice_Caracter - 1; // Skip the end of the line.
                                    break;
                                }
                            }
                            if (Índice_Inicio > -1 && Índice_Fin > -1 && Índice_Inicio < Índice_Fin)
                            {
                                Variable_URL_Cliente_JAR = Texto_Versión.Substring(Índice_Inicio, (Índice_Fin - Índice_Inicio) + 1);
                            }
                            else Variable_URL_Cliente_JAR = null;
                        }
                        else Variable_URL_Cliente_JAR = null;

                        if (Índice_Cliente_TXT > -1)
                        {
                            int Índice_Inicio = -1;
                            for (int Índice_Caracter = Índice_Cliente_TXT; Índice_Caracter >= 0; Índice_Caracter--)
                            {
                                if (Texto_Versión[Índice_Caracter] == '\"')
                                {
                                    Índice_Inicio = Índice_Caracter + 1; // Skip the start of the line.
                                    break;
                                }
                            }

                            int Índice_Fin = -1;
                            for (int Índice_Caracter = Índice_Cliente_TXT + Texto_Cliente_TXT.Length; Índice_Caracter < Texto_Versión.Length; Índice_Caracter++)
                            {
                                if (Texto_Versión[Índice_Caracter] == '\"')
                                {
                                    Índice_Fin = Índice_Caracter - 1; // Skip the end of the line.
                                    break;
                                }
                            }

                            if (Índice_Inicio > -1 && Índice_Fin > -1 && Índice_Inicio < Índice_Fin)
                            {
                                Variable_URL_Cliente_TXT = Texto_Versión.Substring(Índice_Inicio, (Índice_Fin - Índice_Inicio) + 1);
                            }
                            else Variable_URL_Cliente_TXT = null;
                        }
                        else Variable_URL_Cliente_TXT = null;

                        if (Índice_Servidor_JAR > -1)
                        {
                            int Índice_Inicio = -1;
                            for (int Índice_Caracter = Índice_Servidor_JAR; Índice_Caracter >= 0; Índice_Caracter--)
                            {
                                if (Texto_Versión[Índice_Caracter] == '\"')
                                {
                                    Índice_Inicio = Índice_Caracter + 1; // Skip the start of the line.
                                    break;
                                }
                            }

                            int Índice_Fin = -1;
                            for (int Índice_Caracter = Índice_Servidor_JAR + Texto_Servidor_JAR.Length; Índice_Caracter < Texto_Versión.Length; Índice_Caracter++)
                            {
                                if (Texto_Versión[Índice_Caracter] == '\"')
                                {
                                    Índice_Fin = Índice_Caracter - 1; // Skip the end of the line.
                                    break;
                                }
                            }

                            if (Índice_Inicio > -1 && Índice_Fin > -1 && Índice_Inicio < Índice_Fin)
                            {
                                Variable_URL_Servidor_JAR = Texto_Versión.Substring(Índice_Inicio, (Índice_Fin - Índice_Inicio) + 1);
                            }
                            else Variable_URL_Servidor_JAR = null;
                        }
                        else Variable_URL_Servidor_JAR = null;

                        if (Índice_Servidor_TXT > -1)
                        {
                            int Índice_Inicio = -1;
                            for (int Índice_Caracter = Índice_Servidor_TXT; Índice_Caracter >= 0; Índice_Caracter--)
                            {
                                if (Texto_Versión[Índice_Caracter] == '\"')
                                {
                                    Índice_Inicio = Índice_Caracter + 1; // Skip the start of the line.
                                    break;
                                }
                            }

                            int Índice_Fin = -1;
                            for (int Índice_Caracter = Índice_Servidor_TXT + Texto_Servidor_TXT.Length; Índice_Caracter < Texto_Versión.Length; Índice_Caracter++)
                            {
                                if (Texto_Versión[Índice_Caracter] == '\"')
                                {
                                    Índice_Fin = Índice_Caracter - 1; // Skip the end of the line.
                                    break;
                                }
                            }

                            if (Índice_Inicio > -1 && Índice_Fin > -1 && Índice_Inicio < Índice_Fin)
                            {
                                Variable_URL_Servidor_TXT = Texto_Versión.Substring(Índice_Inicio, (Índice_Fin - Índice_Inicio) + 1);
                            }
                            else Variable_URL_Servidor_TXT = null;
                        }
                        else Variable_URL_Servidor_TXT = null;

                        TextBox_URL_Cliente_JAR.Text = Variable_URL_Cliente_JAR;
                        TextBox_URL_Cliente_TXT.Text = Variable_URL_Cliente_TXT;
                        TextBox_URL_Servidor_JAR.Text = Variable_URL_Servidor_JAR;
                        TextBox_URL_Servidor_TXT.Text = Variable_URL_Servidor_TXT;
                        Texto_Versión = null;
                        return true;
                    }
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return false;
        }

        private void Botón_Cargar_Archivo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (ComboBox_Versión_Minecraft.SelectedIndex > -1 && !string.IsNullOrEmpty(Variable_Versión_Minecraft))
                {
                    Cargar_Archivo(Program.Ruta_Minecraft + "\\versions\\" + Variable_Versión_Minecraft + "\\" + Variable_Versión_Minecraft + ".json");
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Botón_Descargar_Click(object sender, EventArgs e)
        {
            try
            {
                // This should be up to 10 minutes in total, since there are 4 files.
                int Intentos = 5; // Tries to download each file.
                int Segundos_Intento = 30; // Wait time to get the whole file for each try. // Use -1 to use the default value. // Default of 100.000?
                byte[] Matriz_Bytes_Cliente_JAR = Program.Descargar_Archivo_Completo(Variable_URL_Cliente_JAR, Intentos, Segundos_Intento);
                byte[] Matriz_Bytes_Cliente_TXT = Program.Descargar_Archivo_Completo(Variable_URL_Cliente_TXT, Intentos, Segundos_Intento);
                byte[] Matriz_Bytes_Servidor_JAR = Program.Descargar_Archivo_Completo(Variable_URL_Servidor_JAR, Intentos, Segundos_Intento);
                byte[] Matriz_Bytes_Servidor_TXT = Program.Descargar_Archivo_Completo(Variable_URL_Servidor_TXT, Intentos, Segundos_Intento);

                Barra_Progreso.Value = 25;
                if (Matriz_Bytes_Cliente_JAR != null && Matriz_Bytes_Cliente_JAR.Length > 0)
                {
                    string Ruta = Ruta_Desofuscación + "\\" + Program.Obtener_Nombre_Temporal() + " client " + Variable_Versión_Minecraft.Replace('.', '_') + ".jar";
                    FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector.SetLength(0L);
                    Lector.Seek(0L, SeekOrigin.Begin);
                    Lector.Write(Matriz_Bytes_Cliente_JAR, 0, Matriz_Bytes_Cliente_JAR.Length);
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                    TextBox_Ruta_Cliente_JAR.Text = Ruta; // Update the path to the downloaded file.
                }
                else TextBox_Ruta_Cliente_JAR.Text = null; // Update the path to a null file.

                Barra_Progreso.Value = 50;
                if (Matriz_Bytes_Cliente_TXT != null && Matriz_Bytes_Cliente_TXT.Length > 0)
                {
                    string Ruta = Ruta_Desofuscación + "\\" + Program.Obtener_Nombre_Temporal() + " client " + Variable_Versión_Minecraft.Replace('.', '_') + ".txt";
                    FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector.SetLength(0L);
                    Lector.Seek(0L, SeekOrigin.Begin);
                    Lector.Write(Matriz_Bytes_Cliente_TXT, 0, Matriz_Bytes_Cliente_TXT.Length);
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                    TextBox_Ruta_Cliente_TXT.Text = Ruta; // Update the path to the downloaded file.
                }
                else TextBox_Ruta_Cliente_TXT.Text = null; // Update the path to a null file.

                Barra_Progreso.Value = 75;
                if (Matriz_Bytes_Servidor_JAR != null && Matriz_Bytes_Servidor_JAR.Length > 0)
                {
                    string Ruta = Ruta_Desofuscación + "\\" + Program.Obtener_Nombre_Temporal() + " server " + Variable_Versión_Minecraft.Replace('.', '_') + ".jar";
                    FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector.SetLength(0L);
                    Lector.Seek(0L, SeekOrigin.Begin);
                    Lector.Write(Matriz_Bytes_Servidor_JAR, 0, Matriz_Bytes_Servidor_JAR.Length);
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                    TextBox_Ruta_Servidor_JAR.Text = Ruta; // Update the path to the downloaded file.
                }
                else TextBox_Ruta_Servidor_JAR.Text = null; // Update the path to a null file.

                Barra_Progreso.Value = 100;
                if (Matriz_Bytes_Servidor_TXT != null && Matriz_Bytes_Servidor_TXT.Length > 0)
                {
                    string Ruta = Ruta_Desofuscación + "\\" + Program.Obtener_Nombre_Temporal() + " server " + Variable_Versión_Minecraft.Replace('.', '_') + ".txt";
                    FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector.SetLength(0L);
                    Lector.Seek(0L, SeekOrigin.Begin);
                    Lector.Write(Matriz_Bytes_Servidor_TXT, 0, Matriz_Bytes_Servidor_TXT.Length);
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                    TextBox_Ruta_Servidor_TXT.Text = Ruta; // Update the path to the downloaded file.
                }
                else TextBox_Ruta_Servidor_TXT.Text = null; // Update the path to a null file.

                Matriz_Bytes_Cliente_JAR = null;
                Matriz_Bytes_Cliente_TXT = null;
                Matriz_Bytes_Servidor_JAR = null;
                Matriz_Bytes_Servidor_TXT = null;
                Barra_Progreso.Value = 0;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_URL_Cliente_JAR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_URL_Cliente_JAR = TextBox_URL_Cliente_JAR.Text;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Copiar_URL_Cliente_JAR_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Variable_URL_Cliente_JAR))
                {
                    Clipboard.SetText(Variable_URL_Cliente_JAR);
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Navegar_URL_Cliente_JAR_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Variable_URL_Cliente_JAR))
                {
                    Program.Ejecutar_Ruta(Variable_URL_Cliente_JAR, ProcessWindowStyle.Normal);
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_URL_Cliente_TXT_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_URL_Cliente_TXT = TextBox_URL_Cliente_TXT.Text;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Copiar_URL_Cliente_TXT_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Variable_URL_Cliente_TXT))
                {
                    Clipboard.SetText(Variable_URL_Cliente_TXT);
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Navegar_URL_Cliente_TXT_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Variable_URL_Cliente_TXT))
                {
                    Program.Ejecutar_Ruta(Variable_URL_Cliente_TXT, ProcessWindowStyle.Normal);
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_URL_Servidor_JAR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_URL_Servidor_JAR = TextBox_URL_Servidor_JAR.Text;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Copiar_URL_Servidor_JAR_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Variable_URL_Servidor_JAR))
                {
                    Clipboard.SetText(Variable_URL_Servidor_JAR);
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Navegar_URL_Servidor_JAR_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Variable_URL_Servidor_JAR))
                {
                    Program.Ejecutar_Ruta(Variable_URL_Servidor_JAR, ProcessWindowStyle.Normal);
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_URL_Servidor_TXT_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_URL_Servidor_TXT = TextBox_URL_Servidor_TXT.Text;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Copiar_URL_Servidor_TXT_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Variable_URL_Servidor_TXT))
                {
                    Clipboard.SetText(Variable_URL_Servidor_TXT);
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Navegar_URL_Servidor_TXT_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Variable_URL_Servidor_TXT))
                {
                    Program.Ejecutar_Ruta(Variable_URL_Servidor_TXT, ProcessWindowStyle.Normal);
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_Ruta_Cliente_JAR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Ruta_Cliente_JAR = TextBox_Ruta_Cliente_JAR.Text;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_Ruta_Cliente_TXT_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Ruta_Cliente_TXT = TextBox_Ruta_Cliente_TXT.Text;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_Ruta_Servidor_JAR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Ruta_Servidor_JAR = TextBox_Ruta_Servidor_JAR.Text;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_Ruta_Servidor_TXT_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Ruta_Servidor_TXT = TextBox_Ruta_Servidor_TXT.Text;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Archivos_Convertir_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Archivos_Convertir = ComboBox_Archivos_Convertir.SelectedIndex;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_ProGuard_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta(Ruta_ProGuard, ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Archivos_Convertir_Click(object sender, EventArgs e)
        {
            try
            {
                //Subproceso.RunWorkerAsync();
                Traducir_Código_Fuente();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Subproceso_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                Barra_Progreso.Value = 0;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Subproceso_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Parses the given line with a class member mapping and processes the results
        /// with the given mapping processor.
        /// </summary>
        internal string Invertir_Línea_ProGuard(string Línea)
        {
            try
            {
                string className = "?"; // Test.

                // See if we can parse one of
                //     ___ ___ -> ___
                //     ___:___:___ ___(___) -> ___
                //     ___:___:___ ___(___):___ -> ___
                //     ___:___:___ ___(___):___:___ -> ___
                // containing the optional line numbers, the return type, the original
                // field/method name, optional arguments, the optional original line
                // numbers, and the new field/method name. The original field/method
                // name may contain an original class name "___.___".

                int colonIndex1 = Línea.IndexOf(':');
                int colonIndex2 = colonIndex1 < 0 ? -1 : Línea.IndexOf(':', colonIndex1 + 1);
                int spaceIndex = Línea.IndexOf(' ', colonIndex2 + 2);
                int argumentIndex1 = Línea.IndexOf('(', spaceIndex + 1);
                int argumentIndex2 = argumentIndex1 < 0 ? -1 : Línea.IndexOf(')', argumentIndex1 + 1);
                int colonIndex3 = argumentIndex2 < 0 ? -1 : Línea.IndexOf(':', argumentIndex2 + 1);
                int colonIndex4 = colonIndex3 < 0 ? -1 : Línea.IndexOf(':', colonIndex3 + 1);
                int arrowIndex = Línea.IndexOf("->", (colonIndex4 >= 0 ? colonIndex4 :
                                                                                   colonIndex3 >= 0 ? colonIndex3 :
                                                                                   argumentIndex2 >= 0 ? argumentIndex2 :
                                                                                                         spaceIndex) + 1);

                if (spaceIndex < 0 ||
                    arrowIndex < 0)
                {
                    return null;
                }

                // Extract the elements.
                string type = Línea.Substring(colonIndex2 + 1, spaceIndex).Trim();
                string name = Línea.Substring(spaceIndex + 1, argumentIndex1 >= 0 ? argumentIndex1 : arrowIndex).Trim();
                string newName = Línea.Substring(arrowIndex + 2).Trim();

                // Does the method name contain an explicit original class name?
                string newClassName = className;
                int dotIndex = name.LastIndexOf('.');
                if (dotIndex >= 0)
                {
                    className = name.Substring(0, dotIndex);
                    name = name.Substring(dotIndex + 1);
                }

                // Process this class member mapping.
                if (type.Length > 0 &&
                    name.Length > 0 &&
                    newName.Length > 0)
                {
                    // Is it a field or a method?
                    if (argumentIndex2 < 0)
                    {
                        /*mappingProcessor.processFieldMapping(className,
                                                             type,
                                                             name,
                                                             newClassName,
                                                             newName);*/
                    }
                    else
                    {
                        int firstLineNumber = 0;
                        int lastLineNumber = 0;
                        int newFirstLineNumber = 0;
                        int newLastLineNumber = 0;

                        if (colonIndex2 >= 0)
                        {
                            firstLineNumber = newFirstLineNumber = int.Parse(Línea.Substring(0, colonIndex1).Trim());
                            lastLineNumber = newLastLineNumber = int.Parse(Línea.Substring(colonIndex1 + 1, colonIndex2).Trim());
                        }

                        if (colonIndex3 >= 0)
                        {
                            firstLineNumber = int.Parse(Línea.Substring(colonIndex3 + 1, colonIndex4 > 0 ? colonIndex4 : arrowIndex).Trim());
                            lastLineNumber = colonIndex4 < 0 ? firstLineNumber :
                                              int.Parse(Línea.Substring(colonIndex4 + 1, arrowIndex).Trim());
                        }

                        string arguments = Línea.Substring(argumentIndex1 + 1, argumentIndex2).Trim();

                        /*mappingProcessor.processMethodMapping(className,
                                                              firstLineNumber,
                                                              lastLineNumber,
                                                              type,
                                                              name,
                                                              arguments,
                                                              newClassName,
                                                              newFirstLineNumber,
                                                              newLastLineNumber,
                                                              newName);*/
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        internal string Invertir_Línea(string Línea)
        {
            try
            {
                //return Invertir_Línea_ProGuard(Línea);
                if (!string.IsNullOrEmpty(Línea) && !Línea.StartsWith("#") && Línea.Contains(" -> "))
                {
                    int Índice_Desofuscado_Inicial = -1; // Start of the deobfuscated name. Unknown yet.
                    int Índice_Separador = Línea.IndexOf(" -> "); // The separator between obfuscated names.
                    if (char.IsLetter(Línea[0])) Índice_Desofuscado_Inicial = 0; // Assume it's the name of a class file.
                    else // Search the first letter, but ignoring the first full "word" if there are numbers.
                    {
                        //return null; // Test.
                        bool Caracteres_Números = char.IsDigit(Línea[0]); // We found a number or a ":".
                        bool Caracteres_Letras = false; // We found a letter.
                        bool Caracteres_Espacio = false; // We found a space.
                        //bool Caracteres_Espacio_Final = false;
                        for (int Índice_Caracter = 0; Índice_Caracter < Índice_Separador; Índice_Caracter++) // Only look at the first "half".
                        {
                            if (!Caracteres_Números) // Are we on a number or a ":"?
                            {
                                if (char.IsNumber(Línea[Índice_Caracter]) || Línea[Índice_Caracter] == ':')
                                {
                                    //Caracteres_Números = true; // We found a number or a ":".
                                    Índice_Desofuscado_Inicial = Índice_Caracter; // We found a letter.
                                    break; // This line should be the name of a class file.
                                }
                            }

                            //return null; // Test.
                            if (!Caracteres_Números)
                            {
                                if (char.IsLetter(Línea[Índice_Caracter]))
                                {
                                    Índice_Desofuscado_Inicial = Índice_Caracter; // We found a letter.
                                    break; // This line should be the name of a class file.
                                }
                            }
                            else // Long mode, it might also contain line and character numbers with a type.
                            {
                                if (!Caracteres_Letras)
                                {
                                    if (char.IsLetter(Línea[Índice_Caracter]))
                                    {
                                        //Caracteres_Letras = true; // We found a letter.
                                        Índice_Desofuscado_Inicial = Índice_Caracter; // We found a letter.
                                        break; // This line shouldn't be the name of a class file, but it includes line and character numbers as well as the name of a type.
                                    }
                                }
                                /*else
                                {
                                    if (!Caracteres_Espacio)
                                    {
                                        if (char.IsWhiteSpace(Línea[Índice_Caracter]))
                                        {
                                            Caracteres_Espacio = true; // We found a space after the numbers and letters, meaning the type name should be over.
                                        }
                                    }
                                    else
                                    {
                                        if (char.IsLetter(Línea[Índice_Caracter]))
                                        {
                                            Índice_Desofuscado_Inicial = Índice_Caracter; // We found a letter.
                                            break; // This line shouldn't be the name of a class file, but it includes line and character numbers as well as the name of a type.
                                        }
                                    }
                                }*/
                            }
                        }
                    }
                    if (Índice_Desofuscado_Inicial > -1) // We finally got the start of the deobfuscated name.
                    {
                        // Now get the other 3 indexes that are still missing.

                        // Get the end of the deobfuscated name.
                        int Índice_Desofuscado_Final = -1;
                        for (int Índice_Caracter = Índice_Separador - 1; Índice_Caracter >= Índice_Desofuscado_Inicial; Índice_Caracter--) // Only look at the first "half".
                        {
                            if (!char.IsWhiteSpace(Línea[Índice_Caracter]))
                            {
                                Índice_Desofuscado_Final = Índice_Caracter; // Add 1 to the length when getting the full name.
                                break;
                            }
                        }

                        // Get the start of the obfuscated name.
                        int Índice_Ofuscado_Inicial = -1;
                        for (int Índice_Caracter = Índice_Separador + 4; Índice_Caracter < Línea.Length; Índice_Caracter++) // Only look at the second "half".
                        {
                            if (!char.IsWhiteSpace(Línea[Índice_Caracter]))
                            {
                                Índice_Ofuscado_Inicial = Índice_Caracter;
                                break;
                            }
                        }

                        // Get the end of the obfuscated name.
                        int Índice_Ofuscado_Final = -1;
                        for (int Índice_Caracter = Línea.Length - 1; Índice_Caracter >= Índice_Ofuscado_Inicial; Índice_Caracter--) // Only look at the second "half".
                        {
                            if (!char.IsWhiteSpace(Línea[Índice_Caracter]))
                            {
                                Índice_Ofuscado_Final = Índice_Caracter; // Add 1 to the length when getting the full name.
                                break;
                            }
                        }

                        // We should be ready to convert the current line.
                        if (Índice_Desofuscado_Inicial > -1 && Índice_Desofuscado_Final > -1 &&
                            Índice_Ofuscado_Inicial > -1 && Índice_Ofuscado_Final > -1 &&
                            Índice_Desofuscado_Inicial <= Índice_Desofuscado_Final && // Is this check really needed?
                            Índice_Ofuscado_Inicial <= Índice_Ofuscado_Final) // Is this check really needed?
                        {
                            string Texto_Desofuscado_Inicial = null; // Not always used.
                            string Texto_Desofuscado_Nombre = null; // Deobfusctaed name.
                            string Texto_Desofuscado_Final = null; // Not always used.
                            string Texto_Separador = " -> "; // Separator.
                            string Texto_Ofuscado_Inicial = null; // Not always used.
                            string Texto_Ofuscado_Nombre = null; // Obfuscated name.
                            string Texto_Ofuscado_Final = null; // Not always used.

                            // First "half":
                            if (Índice_Desofuscado_Inicial > 0)
                            {
                                Texto_Desofuscado_Inicial = Línea.Substring(0, Índice_Desofuscado_Inicial);
                            }
                            Texto_Desofuscado_Nombre = Línea.Substring(Índice_Desofuscado_Inicial, (Índice_Desofuscado_Final - Índice_Desofuscado_Inicial) + 1);
                            /*if (Índice_Desofuscado_Final + 1 < Índice_Separador)
                            {
                                Texto_Desofuscado_Final = Línea.Substring(Índice_Desofuscado_Final + 1, (Índice_Separador - (Índice_Desofuscado_Final + 1)) + 1);
                            }*/

                            // Second "half":
                            if (Índice_Ofuscado_Inicial > Índice_Separador + 4)
                            {
                                Texto_Ofuscado_Inicial = Línea.Substring(Índice_Separador + 4, (Índice_Ofuscado_Inicial - (Índice_Separador + 4)) + 1);
                            }
                            Texto_Ofuscado_Nombre = Línea.Substring(Índice_Ofuscado_Inicial, (Índice_Ofuscado_Final - Índice_Ofuscado_Inicial) + 1);
                            /*if (Índice_Ofuscado_Final + 1 < Línea.Length)
                            {
                                Texto_Ofuscado_Final = Línea.Substring(Índice_Ofuscado_Final + 1, (Línea.Length - (Índice_Ofuscado_Final + 1)) + 1);
                            }*/

                            if (Texto_Ofuscado_Nombre.EndsWith(":")) // Quick fix out of place.
                            {
                                Texto_Ofuscado_Nombre = Texto_Ofuscado_Nombre.TrimEnd(":".ToCharArray());
                                Texto_Desofuscado_Nombre += ':';
                            }

                            // We are finally done, so return the inverted line.
                            return Texto_Desofuscado_Inicial +
                                Texto_Ofuscado_Nombre + // Swap this.
                                Texto_Desofuscado_Final +
                                Texto_Separador +
                                Texto_Ofuscado_Inicial +
                                Texto_Desofuscado_Nombre + // Swap this.
                                Texto_Ofuscado_Final;
                            /*return Texto_Desofuscado_Inicial +
                                Texto_Desofuscado_Nombre + // Swap this.
                                Texto_Desofuscado_Final +
                                Texto_Separador +
                                Texto_Ofuscado_Inicial +
                                Texto_Ofuscado_Nombre + // Swap this.
                                Texto_Ofuscado_Final;*/
                        }
                    }
                }
                else return Línea;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        private void Subproceso_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //Barra_Progreso.Value = 100;
                if (!string.IsNullOrEmpty(Variable_Ruta_Cliente_TXT) && File.Exists(Variable_Ruta_Cliente_TXT) && new FileInfo(Variable_Ruta_Cliente_TXT).Length > 0L)
                {
                    FileStream Lector_Entrada = new FileStream(Variable_Ruta_Cliente_TXT, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    Lector_Entrada.Seek(0L, SeekOrigin.Begin);
                    StreamReader Lector_Entrada_Texto = new StreamReader(Lector_Entrada, Encoding.UTF8, true);

                    FileStream Lector_Salida = new FileStream(Variable_Ruta_Salida_Cliente_TXT, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector_Salida.SetLength(0L);
                    Lector_Salida.Seek(0L, SeekOrigin.Begin);
                    StreamWriter Lector_Salida_Texto = new StreamWriter(Lector_Salida, Encoding.UTF8);

                    //List<string> Lista_Líneas = new List<string>();
                    while (!Lector_Entrada_Texto.EndOfStream)
                    {
                        string Línea = Lector_Entrada_Texto.ReadLine();
                        if (!string.IsNullOrEmpty(Línea))
                        {
                            Línea = Invertir_Línea(Línea);
                            if (!string.IsNullOrEmpty(Línea)) Lector_Salida_Texto.WriteLine(Línea);
                            //else Lector_Salida_Texto.WriteLine("# INTERNAL ERROR #"); // "// INTERNAL ERROR //".
                        }
                        else Lector_Salida_Texto.WriteLine(string.Empty); // ?.
                        Lector_Salida_Texto.Flush();
                    }
                    Lector_Salida_Texto.Close();
                    Lector_Salida_Texto.Dispose();
                    Lector_Salida_Texto = null;
                    Lector_Salida.Close();
                    Lector_Salida.Dispose();
                    Lector_Salida = null;

                    Lector_Entrada_Texto.Close();
                    Lector_Entrada_Texto.Dispose();
                    Lector_Entrada_Texto = null;
                    Lector_Entrada.Close();
                    Lector_Entrada.Dispose();
                    Lector_Entrada = null;

                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal SortedDictionary<string, Ofuscaciones> Traducir_Mapa_Ofuscación(string Ruta_Mapa_Ofuscación)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta_Mapa_Ofuscación) && File.Exists(Ruta_Mapa_Ofuscación) && new FileInfo(Ruta_Mapa_Ofuscación).Length > 0L)
                {
                    FileStream Lector_Entrada = new FileStream(Ruta_Mapa_Ofuscación, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    Lector_Entrada.Seek(0L, SeekOrigin.Begin);
                    StreamReader Lector_Entrada_Texto = new StreamReader(Lector_Entrada, Encoding.UTF8, true);
                    SortedDictionary<string, Ofuscaciones> Diccionario_Ofuscaciones = new SortedDictionary<string, Ofuscaciones>();
                    KeyValuePair<string, string> Entrada_Clase = new KeyValuePair<string, string>(null, null);
                    // Obfuscation maps reader converted from the ProGuard source code from Java to C# by Jupisoft.
                    // Note: the "string.Substring();" function in Java is different and the second int value in
                    // Java is another index in the string while in C# is the length of the substring, so just
                    // subtract to the value in Java the start index to make it work again as expected. This
                    // actually took me a while to find since the code wasn't working properly and threw exceptios.

                    Ofuscaciones Ofuscación_Clase = Ofuscaciones.Nueva_Clase();
                    List<Ofuscaciones> Lista_Ofuscaciones_Temporal = new List<Ofuscaciones>();
                    string Nombre_Clase = null;
                    string className = null;
                    while (!Lector_Entrada_Texto.EndOfStream)
                    {
                        string Línea = Lector_Entrada_Texto.ReadLine();
                        if (!string.IsNullOrEmpty(Línea))
                        {
                            Línea = Línea.Trim();

                            // Is it a non-comment line?
                            if (!Línea.StartsWith("#"))
                            {
                                // Is it a class mapping or a class member mapping?
                                if (Línea.EndsWith(":"))
                                {
                                    // It's a new class, so add the previous if it's not empty.
                                    if (!string.IsNullOrEmpty(Nombre_Clase) && string.Compare(Ofuscación_Clase.Nombre_Ofuscado, Nombre_Clase, false) == 0)
                                    {
                                        Lista_Ofuscaciones_Temporal.Add(Ofuscación_Clase); // Add itself at the end.
                                        Ofuscación_Clase.Lista_Ofuscaciones = Lista_Ofuscaciones_Temporal.GetRange(0, Lista_Ofuscaciones_Temporal.Count);
                                        Diccionario_Ofuscaciones.Add(Nombre_Clase, Ofuscación_Clase);
                                        Lista_Ofuscaciones_Temporal.Clear();
                                    }
                                    // Always reset the class structure.
                                    Ofuscación_Clase = Ofuscaciones.Nueva_Clase();

                                    // Process the class mapping and remember the class's
                                    // old name.
                                    //className = processClassMapping(Línea, mappingProcessor);

                                    // Parses the given line with a class mapping and processes the
                                    // results with the given mapping processor. Returns the old class name,
                                    // or null if any subsequent class member lines can be ignored.

                                    // See if we can parse "___ -> ___:", containing the original
                                    // class name and the new class name.

                                    int arrowIndex = Línea.IndexOf("->");
                                    if (arrowIndex < 0)
                                    {
                                        //return null;
                                        continue;
                                    }

                                    int colonIndex = Línea.IndexOf(':', arrowIndex + 2);
                                    if (colonIndex < 0)
                                    {
                                        //return null;
                                        continue;
                                    }

                                    // Extract the elements.
                                    /*string */
                                    className = Línea.Substring(0, arrowIndex).Trim();
                                    string newClassName = Línea.Substring(arrowIndex + 2, colonIndex - (arrowIndex + 2)).Trim();
                                    Nombre_Clase = newClassName; // Used to know on which class we are right now.

                                    Ofuscación_Clase.Nombre_Ofuscado = newClassName;
                                    Ofuscación_Clase.Nombre_Desofuscado = className;
                                    Ofuscación_Clase.Nombre_Clase_Ofuscado = newClassName; // Needed later on.
                                    Ofuscación_Clase.Nombre_Clase_Desofuscado = className;
                                    Ofuscación_Clase.Nombre_Completo_Ofuscado = newClassName; // Needed later on.
                                    Ofuscación_Clase.Nombre_Completo_Desofuscado = className;

                                    // Process this class name mapping.
                                    //bool interested = mappingProcessor.processClassMapping(className, newClassName);

                                    //return interested ? className : null;

                                    /*Entrada_Clase = new KeyValuePair<string, string>(newClassName, className);
                                    if (!Diccionario_Ofuscaciones.ContainsKey(Entrada_Clase))
                                    {
                                        Diccionario_Ofuscaciones.Add(Entrada_Clase, new List<KeyValuePair<string, string>>());
                                    }*/
                                    //Ofuscaciones Ofuscación = new Ofuscaciones(newClassName, className);
                                }
                                else if (className != null)
                                {
                                    // Process the class member mapping, in the context of
                                    // the current old class name.
                                    //processClassMemberMapping(className, Línea, mappingProcessor);

                                    // Parses the given line with a class member mapping and processes the
                                    // results with the given mapping processor.

                                    // See if we can parse one of
                                    //     ___ ___ -> ___
                                    //     ___:___:___ ___(___) -> ___
                                    //     ___:___:___ ___(___):___ -> ___
                                    //     ___:___:___ ___(___):___:___ -> ___
                                    // containing the optional line numbers, the return type, the original
                                    // field/method name, optional arguments, the optional original line
                                    // numbers, and the new field/method name. The original field/method
                                    // name may contain an original class name "___.___".

                                    int colonIndex1 = Línea.IndexOf(':');
                                    int colonIndex2 = colonIndex1 < 0 ? -1 : Línea.IndexOf(':', colonIndex1 + 1);
                                    int spaceIndex = Línea.IndexOf(' ', colonIndex2 + 2);
                                    int argumentIndex1 = Línea.IndexOf('(', spaceIndex + 1);
                                    int argumentIndex2 = argumentIndex1 < 0 ? -1 : Línea.IndexOf(')', argumentIndex1 + 1);
                                    int colonIndex3 = argumentIndex2 < 0 ? -1 : Línea.IndexOf(':', argumentIndex2 + 1);
                                    int colonIndex4 = colonIndex3 < 0 ? -1 : Línea.IndexOf(':', colonIndex3 + 1);
                                    int arrowIndex = Línea.IndexOf("->", (colonIndex4 >= 0 ? colonIndex4 : (colonIndex3 >= 0 ? colonIndex3 : (argumentIndex2 >= 0 ? argumentIndex2 : spaceIndex))) + 1);

                                    if (spaceIndex < 0 ||
                                        arrowIndex < 0)
                                    {
                                        //return;
                                        continue;
                                    }

                                    // Extract the elements.
                                    string type = Línea.Substring(colonIndex2 + 1, spaceIndex - (colonIndex2 + 1)).Trim();
                                    string name = Línea.Substring(spaceIndex + 1, argumentIndex1 >= 0 ? argumentIndex1 - (spaceIndex + 1) : arrowIndex - (spaceIndex + 1)).Trim();
                                    string newName = Línea.Substring(arrowIndex + 2).Trim();

                                    // Does the method name contain an explicit original class name?
                                    string newClassName = className;
                                    int dotIndex = name.LastIndexOf('.');
                                    if (dotIndex >= 0)
                                    {
                                        className = name.Substring(0, dotIndex);
                                        name = name.Substring(dotIndex + 1);
                                    }

                                    // Process this class member mapping.
                                    if (type.Length > 0 &&
                                        name.Length > 0 &&
                                        newName.Length > 0)
                                    {
                                        // Is it a field or a method?
                                        if (argumentIndex2 < 0)
                                        {
                                            //mappingProcessor.processFieldMapping(className, type, name, newClassName, newName);

                                            Lista_Ofuscaciones_Temporal.Add(new Ofuscaciones(newName, name, type, Ofuscación_Clase.Nombre_Ofuscado, Ofuscación_Clase.Nombre_Desofuscado));
                                        }
                                        else
                                        {
                                            int firstLineNumber = 0;
                                            int lastLineNumber = 0;
                                            int newFirstLineNumber = 0;
                                            int newLastLineNumber = 0;

                                            if (colonIndex2 >= 0)
                                            {
                                                firstLineNumber = newFirstLineNumber = int.Parse(Línea.Substring(0, colonIndex1).Trim());
                                                lastLineNumber = newLastLineNumber = int.Parse(Línea.Substring(colonIndex1 + 1, colonIndex2 - (colonIndex1 + 1)).Trim());
                                            }

                                            if (colonIndex3 >= 0)
                                            {
                                                firstLineNumber = int.Parse(Línea.Substring(colonIndex3 + 1, colonIndex4 > 0 ? colonIndex4 - (colonIndex3 + 1) : arrowIndex - (colonIndex3 + 1)).Trim());
                                                lastLineNumber = colonIndex4 < 0 ? firstLineNumber : int.Parse(Línea.Substring(colonIndex4 + 1, arrowIndex - (colonIndex4 + 1)).Trim());
                                            }

                                            string arguments = Línea.Substring(argumentIndex1 + 1, argumentIndex2 - (argumentIndex1 + 1)).Trim();

                                            /*if (!string.IsNullOrEmpty(arguments) && arguments.Contains(","))
                                            {
                                                ; // Test to know if multiple arguments could be detected here. It worked!
                                            }*/
                                            string[] Matriz_Argumentos = arguments.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                            /*if (!string.IsNullOrEmpty(arguments))
                                            {
                                                if (!arguments.Contains(","))
                                                {
                                                    string arg2 = arguments.Replace("[]", "_array").Replace("$", "_S_").Replace('.', '_');
                                                    if (!Lista_Global.Contains(arg2)) Lista_Global.Add(arg2);
                                                }
                                                else
                                                {
                                                    string[] args = arguments.Split(",".ToCharArray());
                                                    foreach (string arg in args)
                                                    {
                                                        string arg2 = arg.Replace("[]", "_array").Replace("$", "_S_").Replace('.', '_');
                                                        if (!Lista_Global.Contains(arg2)) Lista_Global.Add(arg2);
                                                    }
                                                }
                                            }*/

                                            //mappingProcessor.processMethodMapping(className, firstLineNumber, lastLineNumber, type, name, arguments, newClassName, newFirstLineNumber, newLastLineNumber, newName);

                                            Lista_Ofuscaciones_Temporal.Add(new Ofuscaciones(newName, name, type, firstLineNumber, lastLineNumber, Matriz_Argumentos, Ofuscación_Clase.Nombre_Ofuscado, Ofuscación_Clase.Nombre_Desofuscado));
                                        }
                                        /*if (Diccionario_Ofuscaciones.ContainsKey(Entrada_Clase))
                                        {
                                            Diccionario_Ofuscaciones[Entrada_Clase].Add(new KeyValuePair<string, string>(newName, name));
                                        }*/
                                    }
                                }
                            }

                            //Línea = Invertir_Línea(Línea);
                            //if (!string.IsNullOrEmpty(Línea)) Lector_Salida_Texto.WriteLine(Línea);
                            //else Lector_Salida_Texto.WriteLine("# INTERNAL ERROR #"); // "// INTERNAL ERROR //".
                        }
                        //else Lector_Salida_Texto.WriteLine(string.Empty); // ?.
                    }
                    // If the last class isn't empty and wasn't added, add it now.
                    if (!string.IsNullOrEmpty(Nombre_Clase) && string.Compare(Ofuscación_Clase.Nombre_Ofuscado, Nombre_Clase, false) == 0)
                    {
                        Lista_Ofuscaciones_Temporal.Add(Ofuscación_Clase); // Add itself at the end.
                        Ofuscación_Clase.Lista_Ofuscaciones = Lista_Ofuscaciones_Temporal.GetRange(0, Lista_Ofuscaciones_Temporal.Count);
                        Diccionario_Ofuscaciones.Add(Nombre_Clase, Ofuscación_Clase);
                        Lista_Ofuscaciones_Temporal.Clear();
                    }
                    Lista_Ofuscaciones_Temporal = null;
                    Lector_Entrada_Texto.Close();
                    Lector_Entrada_Texto.Dispose();
                    Lector_Entrada_Texto = null;
                    Lector_Entrada.Close();
                    Lector_Entrada.Dispose();
                    Lector_Entrada = null;
                    if (Diccionario_Ofuscaciones != null && Diccionario_Ofuscaciones.Count > 0) return Diccionario_Ofuscaciones;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        //internal static List<string> Lista_Global = new List<string>();

        /// <summary>
        /// Enumeration that contains all the different types of obfuscation done by ProGuard.
        /// </summary>
        internal enum Categorías : int
        {
            /// <summary>
            /// It's a class.
            /// </summary>
            Clase,
            /// <summary>
            /// It's a field.
            /// </summary>
            Campo,
            /// <summary>
            /// It's a method.
            /// </summary>
            Método,
        }

        /// <summary>
        /// Structure that holds up all the information about a single obfuscation done by ProGuard.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Ofuscaciones
        {
            internal string Nombre_Ofuscado;
            internal string Nombre_Desofuscado;
            internal Categorías Categoría;
            internal string Tipo;
            internal int Índice_Línea_Inicial;
            internal int Índice_Línea_Final;
            internal string[] Matriz_Argumentos;
            internal int Argumentos;
            internal string Nombre_Clase_Ofuscado;
            internal string Nombre_Clase_Desofuscado;
            internal string Nombre_Completo_Ofuscado;
            internal string Nombre_Completo_Desofuscado;
            internal List<Ofuscaciones> Lista_Ofuscaciones;

            /// <summary>
            /// New structure, used to reset the global variable.
            /// </summary>
            internal static Ofuscaciones Nueva_Clase()
            {
                Ofuscaciones Ofuscación = new Ofuscaciones();
                Ofuscación.Nombre_Ofuscado = null;
                Ofuscación.Nombre_Desofuscado = null;
                Ofuscación.Categoría = Categorías.Clase;
                Ofuscación.Tipo = null;
                Ofuscación.Índice_Línea_Inicial = -1;
                Ofuscación.Índice_Línea_Final = -1;
                Ofuscación.Matriz_Argumentos = null;
                Ofuscación.Argumentos = 0;
                Ofuscación.Nombre_Clase_Ofuscado = null;
                Ofuscación.Nombre_Clase_Desofuscado = null;
                Ofuscación.Nombre_Completo_Ofuscado = null;
                Ofuscación.Nombre_Completo_Desofuscado = null;
                Ofuscación.Lista_Ofuscaciones = new List<Ofuscaciones>();
                return Ofuscación;
            }

            /// <summary>
            /// Class.
            /// </summary>
            internal Ofuscaciones(string Nombre_Ofuscado, string Nombre_Desofuscado, List<Ofuscaciones> Lista_Ofuscaciones)
            {
                this.Nombre_Ofuscado = Nombre_Ofuscado;
                this.Nombre_Desofuscado = Nombre_Desofuscado;
                this.Categoría = Categorías.Clase;
                this.Tipo = null;
                this.Índice_Línea_Inicial = -1;
                this.Índice_Línea_Final = -1;
                this.Matriz_Argumentos = null;
                this.Argumentos = 0;
                this.Nombre_Clase_Ofuscado = Nombre_Ofuscado;
                this.Nombre_Clase_Desofuscado = Nombre_Desofuscado;
                this.Nombre_Completo_Ofuscado = Nombre_Ofuscado;
                this.Nombre_Completo_Desofuscado = Nombre_Desofuscado;
                this.Lista_Ofuscaciones = Lista_Ofuscaciones;
            }

            /// <summary>
            /// Field.
            /// </summary>
            internal Ofuscaciones(string Nombre_Ofuscado, string Nombre_Desofuscado, string Tipo, string Nombre_Clase_Ofuscado, string Nombre_Clase_Desofuscado)
            {
                this.Nombre_Ofuscado = Nombre_Ofuscado;
                this.Nombre_Desofuscado = Nombre_Desofuscado;
                this.Categoría = Categorías.Campo;
                this.Tipo = Tipo;
                this.Índice_Línea_Inicial = -1;
                this.Índice_Línea_Final = -1;
                this.Matriz_Argumentos = null;
                this.Argumentos = 0;
                this.Nombre_Clase_Ofuscado = Nombre_Clase_Ofuscado;
                this.Nombre_Clase_Desofuscado = Nombre_Clase_Desofuscado;
                this.Nombre_Completo_Ofuscado = Nombre_Clase_Ofuscado + '.' + Nombre_Ofuscado;
                this.Nombre_Completo_Desofuscado = Nombre_Clase_Desofuscado + '.' + Nombre_Desofuscado;
                this.Lista_Ofuscaciones = null;
            }

            /// <summary>
            /// Method.
            /// </summary>
            internal Ofuscaciones(string Nombre_Ofuscado, string Nombre_Desofuscado, string Tipo, int Índice_Línea_Inicial, int Índice_Línea_Final, string[] Matriz_Argumentos, string Nombre_Clase_Ofuscado, string Nombre_Clase_Desofuscado)
            {
                this.Nombre_Ofuscado = Nombre_Ofuscado;
                this.Nombre_Desofuscado = Nombre_Desofuscado;
                this.Categoría = Categorías.Método;
                this.Tipo = Tipo;
                this.Índice_Línea_Inicial = Índice_Línea_Inicial;
                this.Índice_Línea_Final = Índice_Línea_Final;
                this.Matriz_Argumentos = Matriz_Argumentos;
                this.Argumentos = Matriz_Argumentos != null ? Matriz_Argumentos.Length : 0;
                this.Nombre_Clase_Ofuscado = Nombre_Clase_Ofuscado;
                this.Nombre_Clase_Desofuscado = Nombre_Clase_Desofuscado;
                this.Nombre_Completo_Ofuscado = Nombre_Clase_Ofuscado + '.' + Nombre_Ofuscado;
                this.Nombre_Completo_Desofuscado = Nombre_Clase_Desofuscado + '.' + Nombre_Desofuscado;
                this.Lista_Ofuscaciones = null;
            }
        }

        internal enum Argumentos : int
        {
            /// <summary>
            /// Unknown.
            /// </summary>
            Desconocido = 0, // Should start at 1, and increase in powers of base 2?
            Boolean,
            Number,
            //Number_Decimal,
            Character,
            String,

            /*boolean,
            boolean_array,
            byte_,
            byte_array,
            byte_array_array,
            char_,
            char_array,
            double_,
            double_array,
            double_array_array,
            float_,
            float_array,
            int_,
            int_array,
            int_array_array,
            long_,
            long_array,
            short_,
            short_array,*/
        }

        internal void Traducir_Código_Fuente()
        {
            try
            {
                string Ruta_Mapa_Ofuscación = Ruta_Desofuscación + "\\2019_09_13_03_34_07_332 client 1_14_4.txt";
                //Lista_Global.Clear();
                SortedDictionary<string, Ofuscaciones> Diccionario_Ofuscaciones = Traducir_Mapa_Ofuscación(Ruta_Mapa_Ofuscación);
                /*Lista_Global.Sort();
                string TP = null;
                foreach (string l in Lista_Global)
                {
                    TP += l + ",\r\n";
                }
                Clipboard.SetText(TP);
                SystemSounds.Asterisk.Play();*/
                //return;
                string Ruta_Código_Fuente = Program.Ruta_Guardado_Imágenes_Secretos + "\\1.14.4.jar.src";
                if (Diccionario_Ofuscaciones != null && Diccionario_Ofuscaciones.Count > 0 && !string.IsNullOrEmpty(Ruta_Código_Fuente) && Directory.Exists(Ruta_Código_Fuente))
                {
                    string[] Matriz_Rutas = Directory.GetFiles(Ruta_Código_Fuente, "*.java", SearchOption.TopDirectoryOnly);
                    if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                    {
                        string Ruta_Salida_Base = Ruta_Desofuscación + "\\try"/* + Program.Obtener_Nombre_Temporal()*/;
                        Program.Crear_Carpetas(Ruta_Salida_Base);
                        foreach (string Ruta in Matriz_Rutas)
                        {
                            try
                            {
                                string Nombre_Ofuscado = Path.GetFileNameWithoutExtension(Ruta);
                                FileStream Lector_Entrada = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                                Lector_Entrada.Seek(0L, SeekOrigin.Begin);
                                StreamReader Lector_Entrada_Texto = new StreamReader(Lector_Entrada, Encoding.UTF8, true);

                                // Find the obfuscated class name in the current dictionary.
                                string Nombre_Desofuscado = Path.GetFileNameWithoutExtension(Ruta);
                                KeyValuePair<string, string> Entrada_Clase = new KeyValuePair<string, string>(null, null);
                                List<KeyValuePair<string, string>> Lista_Traducciones = new List<KeyValuePair<string, string>>();
                                foreach (KeyValuePair<string, Ofuscaciones> Entrada in Diccionario_Ofuscaciones)
                                {
                                    if (string.Compare(Entrada.Key, Nombre_Ofuscado, true) == 0)
                                    {
                                        Nombre_Desofuscado = Entrada.Value.Nombre_Desofuscado;
                                        break;
                                    }
                                }

                                // Now start the already deobfuscated class name as a new file path.
                                string Ruta_Salida = Ruta_Salida_Base + "\\" + Nombre_Desofuscado + "_" + Nombre_Ofuscado + ".java";
                                FileStream Lector_Salida = new FileStream(Ruta_Salida, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                Lector_Salida.SetLength(0L);
                                Lector_Salida.Seek(0L, SeekOrigin.Begin);
                                StreamWriter Lector_Salida_Texto = new StreamWriter(Lector_Salida, Encoding.UTF8);
                                // Now fully deobfuscate each source code file here.
                                /*List<Ofuscaciones> Lista_Ofuscaciones = new List<Ofuscaciones>();
                                foreach (KeyValuePair<string, Ofuscaciones> Entrada in Diccionario_Ofuscaciones)
                                {
                                    Lista_Ofuscaciones.Add(Entrada.Value);
                                    if (Entrada.Value.Lista_Ofuscaciones != null && Entrada.Value.Lista_Ofuscaciones.Count > 0)
                                    {
                                        foreach (Ofuscaciones Ofuscación in Entrada.Value.Lista_Ofuscaciones)
                                        {

                                        }
                                    }
                                }*/
                                int Índice_Línea = 0; // Start at 1 after this, since ProGuard seems to do this.
                                while (!Lector_Entrada_Texto.EndOfStream)
                                {
                                    string Línea = Lector_Entrada_Texto.ReadLine();
                                    Índice_Línea++;
                                    if (!string.IsNullOrEmpty(Línea) && !Línea.StartsWith("/* Location:"))
                                    {
                                        // First try to replace the rest of classes, fields and methods invoked in this one.
                                        foreach (KeyValuePair<string, Ofuscaciones> Entrada in Diccionario_Ofuscaciones)
                                        {
                                            if (string.Compare(Entrada.Key, Nombre_Ofuscado, false) != 0) // First avoid the self class.
                                            {
                                                if (Entrada.Value.Lista_Ofuscaciones != null && Entrada.Value.Lista_Ofuscaciones.Count > 0)
                                                {
                                                    foreach (Ofuscaciones Ofuscación in Entrada.Value.Lista_Ofuscaciones)
                                                    {
                                                        if (string.Compare(Ofuscación.Nombre_Completo_Ofuscado, Entrada.Value.Lista_Ofuscaciones[Entrada.Value.Lista_Ofuscaciones.Count - 1].Nombre_Completo_Ofuscado, false) != 0) // Avoid the single class name at the end?
                                                        {
                                                            List<int> Lista_Índices = new List<int>();
                                                            for (int Índice_Caracter = 0; ;)
                                                            {
                                                                /*if (Nombre_Ofuscado == "ctp" && Lista_Índices.Count > 0)
                                                                {
                                                                    ;
                                                                }*/

                                                                int Índice = Línea.IndexOf(Ofuscación.Nombre_Completo_Ofuscado, Índice_Caracter);
                                                                if (Índice > -1)
                                                                {
                                                                    // Now make sure that is a full "word" to avoid errors.
                                                                    if (Índice - 2 > -1 &&
                                                                        Índice + Ofuscación.Nombre_Completo_Ofuscado.Length < Línea.Length &&
                                                                        !char.IsLetterOrDigit(Línea[Índice - 2]) && // Avoid this.X.etc.
                                                                        !char.IsLetterOrDigit(Línea[Índice - 1]) &&
                                                                        !char.IsLetterOrDigit(Línea[Índice + Ofuscación.Nombre_Completo_Ofuscado.Length]))
                                                                    {
                                                                        Lista_Índices.Add(Índice);
                                                                        /*if (!Diccionario_Índices.ContainsKey(Ofuscación.Nombre_Completo_Ofuscado))
                                                                        {
                                                                            Diccionario_Índices.Add(Ofuscación.Nombre_Completo_Ofuscado, new List<int>(new int[] { Índice }));
                                                                        }
                                                                        else
                                                                        {
                                                                            Diccionario_Índices[Ofuscación.Nombre_Completo_Ofuscado].Add(Índice);
                                                                        }*/
                                                                    }
                                                                    Índice_Caracter = Índice + Ofuscación.Nombre_Completo_Ofuscado.Length;
                                                                }
                                                                else break;
                                                            }

                                                            // Now translate the full line here in reverse order every time for each found class, etc.
                                                            if (Lista_Índices != null && Lista_Índices.Count > 0)
                                                            {
                                                                if (Lista_Índices.Count > 1) Lista_Índices.Sort(); // Go by inverted sorted order.

                                                                // Start replacing at the end so the indexes won't change of place.
                                                                for (int Índice = Lista_Índices.Count - 1; Índice >= 0; Índice--)
                                                                {
                                                                    List<Ofuscaciones> Lista_Ofuscaciones_Temporal = new List<Ofuscaciones>();
                                                                    for (int Índice_Ofuscación = 0; Índice_Ofuscación < Entrada.Value.Lista_Ofuscaciones.Count; Índice_Ofuscación++)
                                                                    {
                                                                        if (string.Compare(Entrada.Value.Lista_Ofuscaciones[Índice_Ofuscación].Nombre_Completo_Ofuscado, Ofuscación.Nombre_Completo_Ofuscado, false) == 0)
                                                                        {
                                                                            Lista_Ofuscaciones_Temporal.Add(Entrada.Value.Lista_Ofuscaciones[Índice_Ofuscación]);
                                                                        }
                                                                    }

                                                                    // We have multiple valid options, so search the one it should be.
                                                                    if (Lista_Ofuscaciones_Temporal.Count > 1)
                                                                    {
                                                                        // Now try to analyze the source code in detail, and if it's a method how many arguments has.
                                                                        if (Línea[Lista_Índices[Índice] + Ofuscación.Nombre_Completo_Ofuscado.Length] == '(')
                                                                        {
                                                                            // We should have found a class or method, so count it's possible arguments.
                                                                            // Let's hope that the full method invocation is in this same line of code.
                                                                            int Total_Argumentos = 0;
                                                                            if (Línea[Lista_Índices[Índice] + Ofuscación.Nombre_Completo_Ofuscado.Length + 1] != ')')
                                                                            {
                                                                                Total_Argumentos++; // It should be one here at the start if it's not already closed.
                                                                                int Paréntesis_Abiertos = 0; // Ignore the method start bracket.
                                                                                for (int Índice_Caracter = Lista_Índices[Índice] + Ofuscación.Nombre_Completo_Ofuscado.Length + 1; Índice_Caracter < Línea.Length; Índice_Caracter++)
                                                                                {
                                                                                    if (Línea[Índice_Caracter] == '(') // A new unexpected bracket has been opened.
                                                                                    {
                                                                                        Paréntesis_Abiertos++;
                                                                                    }
                                                                                    else if (Línea[Índice_Caracter] == ')') // One of the brackets has been closed.
                                                                                    {
                                                                                        Paréntesis_Abiertos--;
                                                                                    }
                                                                                    else if (Paréntesis_Abiertos == 0 && Línea[Índice_Caracter] == ',')
                                                                                    {
                                                                                        // Only while we don't have any pending brackets opened (or closed?).
                                                                                        Total_Argumentos++; // We found a new argument separator
                                                                                    }
                                                                                }
                                                                            }
                                                                            // Else we have an empty class or method without any argument.

                                                                            // Now remove from the findings list the methods with improper argument quantity.
                                                                            for (int Índice_Ofuscación = Lista_Ofuscaciones_Temporal.Count - 1; Índice_Ofuscación >= 0; Índice_Ofuscación--)
                                                                            {
                                                                                if (Lista_Ofuscaciones_Temporal[Índice_Ofuscación].Argumentos != Total_Argumentos)
                                                                                {
                                                                                    Lista_Ofuscaciones_Temporal.RemoveAt(Índice_Ofuscación);
                                                                                }
                                                                            }

                                                                            if (Lista_Ofuscaciones_Temporal.Count > 1) // We still have found too many matches.
                                                                            {
                                                                                // Now try to "learn" the type of the arguments in real time...
                                                                                // This will likely fail multiple times, it needs a better "AI".
                                                                                Argumentos Argumento = Argumentos.Desconocido;
                                                                                char Caracter = Línea[Lista_Índices[Índice] + Ofuscación.Nombre_Completo_Ofuscado.Length + 1];
                                                                                if (Caracter == '\"') Argumento = Argumentos.String;
                                                                                else if (Caracter == '\'') Argumento = Argumentos.Character;
                                                                                else if (char.IsDigit(Caracter)) Argumento = Argumentos.Number; // Might still be a decimal.
                                                                                                                                                //else if (char.Is(?)) Argumento = Argumentos.?; // ...

                                                                                for (int Índice_Ofuscación = Lista_Ofuscaciones_Temporal.Count - 1; Índice_Ofuscación >= 0; Índice_Ofuscación--)
                                                                                {
                                                                                    if (Lista_Ofuscaciones_Temporal[Índice_Ofuscación].Matriz_Argumentos != null && Lista_Ofuscaciones_Temporal[Índice_Ofuscación].Matriz_Argumentos.Length > 0)
                                                                                    {
                                                                                        if (Argumento == Argumentos.String &&
                                                                                            string.Compare(Lista_Ofuscaciones_Temporal[Índice_Ofuscación].Matriz_Argumentos[0], "java.lang.String", false) == 0)
                                                                                        {
                                                                                            // Actually do nothing here.
                                                                                        }
                                                                                        else if (Argumento == Argumentos.Character &&
                                                                                            (string.Compare(Lista_Ofuscaciones_Temporal[Índice_Ofuscación].Matriz_Argumentos[0], "char", false) == 0 ||
                                                                                            string.Compare(Lista_Ofuscaciones_Temporal[Índice_Ofuscación].Matriz_Argumentos[0], "java.lang.Character", false) == 0))
                                                                                        {
                                                                                            // Actually do nothing here.
                                                                                        }
                                                                                        else if (Argumento == Argumentos.Number &&
                                                                                            (string.Compare(Lista_Ofuscaciones_Temporal[Índice_Ofuscación].Matriz_Argumentos[0], "byte", false) == 0 ||
                                                                                            string.Compare(Lista_Ofuscaciones_Temporal[Índice_Ofuscación].Matriz_Argumentos[0], "double", false) == 0 ||
                                                                                            string.Compare(Lista_Ofuscaciones_Temporal[Índice_Ofuscación].Matriz_Argumentos[0], "float", false) == 0 ||
                                                                                            string.Compare(Lista_Ofuscaciones_Temporal[Índice_Ofuscación].Matriz_Argumentos[0], "int", false) == 0 ||
                                                                                            string.Compare(Lista_Ofuscaciones_Temporal[Índice_Ofuscación].Matriz_Argumentos[0], "long", false) == 0 ||
                                                                                            string.Compare(Lista_Ofuscaciones_Temporal[Índice_Ofuscación].Matriz_Argumentos[0], "short", false) == 0))
                                                                                        {
                                                                                            // Actually do nothing here.
                                                                                        }
                                                                                        else // Unknown types, so delete this possible match?
                                                                                        {
                                                                                            Lista_Ofuscaciones_Temporal.RemoveAt(Índice_Ofuscación);
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        else // It should be a class or field, not a class or method.
                                                                        {
                                                                            // Now remove from the findings list any methods and leave the fields.
                                                                            for (int Índice_Ofuscación = Lista_Ofuscaciones_Temporal.Count - 1; Índice_Ofuscación >= 0; Índice_Ofuscación--)
                                                                            {
                                                                                if (Lista_Ofuscaciones_Temporal[Índice_Ofuscación].Categoría == Categorías.Método)
                                                                                {
                                                                                    Lista_Ofuscaciones_Temporal.RemoveAt(Índice_Ofuscación);
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    else if (Lista_Ofuscaciones_Temporal.Count <= 0) continue; // We have lost all the matches?
                                                                                                                               // So here we don't have any matches remaining? Something went wrong...

                                                                    // We have one or multiple matches, so always pick the first one.
                                                                    if (Lista_Ofuscaciones_Temporal.Count > 0)
                                                                    {
                                                                        //if (Lista_Ofuscaciones.Count == 1) // Only apply if 100 % certain of the "method" name?
                                                                        {
                                                                            // Get in reverse order the first deobfuscated remaining name.
                                                                            string Nombre = Lista_Ofuscaciones_Temporal[Lista_Ofuscaciones_Temporal.Count - 1].Nombre_Completo_Desofuscado;

                                                                            // Finally apply the choosen match and deobfuscate the source code.
                                                                            Línea =
                                                                            (Lista_Índices[Índice] > 0 ? Línea.Substring(0, Lista_Índices[Índice]) : null) +
                                                                            Nombre + // The filtered name to make sure it's the correct match (still might not work).
                                                                                     //Ofuscación.Nombre_Completo_Desofuscado + // The default or first match. Will fail...
                                                                            (Lista_Índices[Índice] + Ofuscación.Nombre_Completo_Ofuscado.Length < Línea.Length ? Línea.Substring(Lista_Índices[Índice] + Ofuscación.Nombre_Completo_Ofuscado.Length) : null);
                                                                        }
                                                                    }
                                                                    else continue; // Just skip this finding, so sadly it's still obfuscated.
                                                                }
                                                                /*// Start replacing at the end so the indexes won't change of place.
                                                                for (int Índice = Lista_Índices.Count - 1; Índice >= 0; Índice--)
                                                                {
                                                                    Línea =
                                                                        (Lista_Índices[Índice] > 0 ? Línea.Substring(0, Lista_Índices[Índice]) : null) +
                                                                        Ofuscación.Nombre_Completo_Desofuscado +
                                                                        (Lista_Índices[Índice] + Ofuscación.Nombre_Completo_Ofuscado.Length < Línea.Length ? Línea.Substring(Lista_Índices[Índice] + Ofuscación.Nombre_Completo_Ofuscado.Length) : null);
                                                                }*/
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        // Second pass to deobfuscate the class, field and method names of this class.
                                        foreach (KeyValuePair<string, Ofuscaciones> Entrada in Diccionario_Ofuscaciones)
                                        {
                                            if (string.Compare(Entrada.Key, Nombre_Ofuscado, false) == 0) // First include the self class.
                                            {
                                                if (Entrada.Value.Lista_Ofuscaciones != null && Entrada.Value.Lista_Ofuscaciones.Count > 0)
                                                {
                                                    foreach (Ofuscaciones Ofuscación in Entrada.Value.Lista_Ofuscaciones)
                                                    {
                                                        List<int> Lista_Índices = new List<int>();
                                                        for (int Índice_Caracter = 0; ;)
                                                        {
                                                            // TODO: check if this starts with "this.", if not and starts with "." just
                                                            // ignor eit since it should reference an unknown class, method, etc from
                                                            // another class file. This will require a lot more code to "learn" which is.
                                                            int Índice = Línea.IndexOf(Ofuscación.Nombre_Ofuscado, Índice_Caracter);
                                                            if (Índice > -1)
                                                            {
                                                                // Now make sure that is a full "word" to avoid errors.
                                                                if (Ofuscación.Categoría != Categorías.Clase)
                                                                {
                                                                    if (Índice - 1 > -1 &&
                                                                    Índice + Ofuscación.Nombre_Ofuscado.Length < Línea.Length &&
                                                                    !char.IsLetterOrDigit(Línea[Índice - 1]) &&
                                                                    !char.IsLetterOrDigit(Línea[Índice + Ofuscación.Nombre_Ofuscado.Length]))
                                                                    {
                                                                        Lista_Índices.Add(Índice);
                                                                    }
                                                                }
                                                                else // Ignore the character after the obfuscated name since it shouldn't exist.
                                                                {
                                                                    if (Índice - 1 > -1 &&
                                                                    !char.IsLetterOrDigit(Línea[Índice - 1]))
                                                                    {
                                                                        Lista_Índices.Add(Índice);
                                                                    }
                                                                }
                                                                Índice_Caracter = Índice + Ofuscación.Nombre_Ofuscado.Length;
                                                            }
                                                            else break;
                                                        }

                                                        // Now translate the full line here in reverse order every time for each found class, etc.
                                                        if (Lista_Índices != null && Lista_Índices.Count > 0)
                                                        {
                                                            if (Lista_Índices.Count > 1) Lista_Índices.Sort(); // Go by inverted sorted order.

                                                            // Start replacing at the end so the indexes won't change of place.
                                                            for (int Índice = Lista_Índices.Count - 1; Índice >= 0; Índice--)
                                                            {
                                                                List<Ofuscaciones> Lista_Ofuscaciones_Temporal = new List<Ofuscaciones>();
                                                                List<Ofuscaciones> Lista_Ofuscaciones_Líneas_Temporal = new List<Ofuscaciones>();
                                                                for (int Índice_Ofuscación = 0; Índice_Ofuscación < Entrada.Value.Lista_Ofuscaciones.Count; Índice_Ofuscación++)
                                                                {
                                                                    if (string.Compare(Entrada.Value.Lista_Ofuscaciones[Índice_Ofuscación].Nombre_Ofuscado, Ofuscación.Nombre_Ofuscado, false) == 0)
                                                                    {
                                                                        if (Entrada.Value.Lista_Ofuscaciones[Índice_Ofuscación].Índice_Línea_Inicial > -1 && Entrada.Value.Lista_Ofuscaciones[Índice_Ofuscación].Índice_Línea_Final > -1)
                                                                        {
                                                                            Lista_Ofuscaciones_Líneas_Temporal.Add(Entrada.Value.Lista_Ofuscaciones[Índice_Ofuscación]);
                                                                        }
                                                                        else Lista_Ofuscaciones_Temporal.Add(Entrada.Value.Lista_Ofuscaciones[Índice_Ofuscación]);
                                                                    }
                                                                }

                                                                if (Lista_Ofuscaciones_Líneas_Temporal.Count > 0)
                                                                {
                                                                    for (int Índice_Ofuscación = Lista_Ofuscaciones_Líneas_Temporal.Count - 1; Índice_Ofuscación >= 0; Índice_Ofuscación--)
                                                                    {
                                                                        // Remove the matches not included between the specified lines of code.
                                                                        // Subtract and add 1 to the full range to include the method definition line or it will fail.
                                                                        // Might need to subtract 2 if there is the line of code and also an open bracket below it.
                                                                        /*if (Lista_Ofuscaciones_Líneas_Temporal[Índice_Ofuscación].Categoría == Categorías.Método)
                                                                        {
                                                                            if (Lista_Ofuscaciones_Líneas_Temporal[Índice_Ofuscación].Índice_Línea_Inicial - 1 == Índice_Línea) // Is valid.
                                                                            {
                                                                                // Actually do nothing here.
                                                                            }
                                                                            else // Out of range, remove it.
                                                                            {
                                                                                Lista_Ofuscaciones_Líneas_Temporal.RemoveAt(Índice_Ofuscación);
                                                                            }
                                                                        }
                                                                        else // Class or field.*/
                                                                        {
                                                                            if (Lista_Ofuscaciones_Líneas_Temporal[Índice_Ofuscación].Índice_Línea_Inicial - 1 <= Índice_Línea &&
                                                                                Lista_Ofuscaciones_Líneas_Temporal[Índice_Ofuscación].Índice_Línea_Final + 1 >= Índice_Línea) // Is valid.
                                                                            {
                                                                                // Actually do nothing here.
                                                                            }
                                                                            else // Out of range, remove it.
                                                                            {
                                                                                Lista_Ofuscaciones_Líneas_Temporal.RemoveAt(Índice_Ofuscación);
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                // Won't remove any match without a specified line of code, so pick the first one if needed.

                                                                if (Lista_Ofuscaciones_Líneas_Temporal.Count > 0) // Line numbers based matches.
                                                                {
                                                                    // Finally apply the choosen match and deobfuscate the source code.
                                                                    Línea =
                                                                    (Lista_Índices[Índice] > 0 ? Línea.Substring(0, Lista_Índices[Índice]) : null) +
                                                                    Lista_Ofuscaciones_Líneas_Temporal[0].Nombre_Desofuscado +
                                                                    (Lista_Índices[Índice] + Ofuscación.Nombre_Ofuscado.Length < Línea.Length ? Línea.Substring(Lista_Índices[Índice] + Ofuscación.Nombre_Ofuscado.Length) : null);
                                                                }
                                                                else if (Lista_Ofuscaciones_Temporal.Count > 0) // Second chance with global matches.
                                                                {
                                                                    // Finally apply the choosen match and deobfuscate the source code.
                                                                    Línea =
                                                                    (Lista_Índices[Índice] > 0 ? Línea.Substring(0, Lista_Índices[Índice]) : null) +
                                                                    Lista_Ofuscaciones_Temporal[0].Nombre_Desofuscado +
                                                                    (Lista_Índices[Índice] + Ofuscación.Nombre_Ofuscado.Length < Línea.Length ? Línea.Substring(Lista_Índices[Índice] + Ofuscación.Nombre_Ofuscado.Length) : null);
                                                                }
                                                                else continue; // Just skip this finding, so sadly it's still obfuscated.
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        // A third pass is needed for single class names invoked outside of their own file.
                                        foreach (KeyValuePair<string, Ofuscaciones> Entrada in Diccionario_Ofuscaciones)
                                        {
                                            //if (string.Compare(Entrada.Key, Nombre_Ofuscado, false) != 0) // First exclude the self class.
                                            {
                                                if (Entrada.Value.Lista_Ofuscaciones != null && Entrada.Value.Lista_Ofuscaciones.Count > 0)
                                                {
                                                    // Only get the single class name.
                                                    Ofuscaciones Ofuscación = Entrada.Value.Lista_Ofuscaciones[Entrada.Value.Lista_Ofuscaciones.Count - 1];
                                                    List<int> Lista_Índices = new List<int>();
                                                    for (int Índice_Caracter = 0; ;)
                                                    {
                                                        int Índice = Línea.IndexOf(Ofuscación.Nombre_Ofuscado, Índice_Caracter);
                                                        if (Índice > -1)
                                                        {
                                                            // Now make sure that is a full "word" to avoid errors.
                                                            if (Índice - 1/*2*/ > -1 &&
                                                                Índice + Ofuscación.Nombre_Ofuscado.Length < Línea.Length &&
                                                                //!char.IsLetterOrDigit(Línea[Índice - 2]) && // Avoid this.X.etc.
                                                                Línea[Índice - 1] != '.' &&
                                                                Línea[Índice + Ofuscación.Nombre_Ofuscado.Length] != '.' &&
                                                                /*(!char.IsWhiteSpace(Línea[Índice - 1]) ||
                                                                !char.IsWhiteSpace(Línea[Índice + Ofuscación.Nombre_Ofuscado.Length])) &&*/
                                                                !char.IsLetterOrDigit(Línea[Índice - 1]) &&
                                                                !char.IsLetterOrDigit(Línea[Índice + Ofuscación.Nombre_Ofuscado.Length]))
                                                            {
                                                                // Check to see if the current "name" is not a in a char or string.
                                                                bool Agregar = true;
                                                                bool Char_Abiertos = false;
                                                                bool String_Abiertos = false;
                                                                for (int Índice_Temporal = 0; Índice_Temporal <= Índice; Índice_Temporal++)
                                                                {
                                                                    if (Línea[Índice_Temporal] == '\'') Char_Abiertos = !Char_Abiertos;
                                                                    else if (Línea[Índice_Temporal] == '\"') String_Abiertos = !String_Abiertos;
                                                                    if (Índice_Temporal == Índice)
                                                                    {
                                                                        if (Char_Abiertos || String_Abiertos) Agregar = false; // Ignore.
                                                                        break;
                                                                    }
                                                                }
                                                                if (Agregar) Lista_Índices.Add(Índice);
                                                            }
                                                            /*if (Índice - 1 > -1 &&
                                                                 !char.IsLetterOrDigit(Línea[Índice - 1]))
                                                                {
                                                                    Lista_Índices.Add(Índice);
                                                                }*/
                                                            Índice_Caracter = Índice + Ofuscación.Nombre_Ofuscado.Length;
                                                        }
                                                        else break;
                                                    }

                                                    // Now translate the full line here in reverse order every time for each found class, etc.
                                                    if (Lista_Índices != null && Lista_Índices.Count > 0)
                                                    {
                                                        if (Lista_Índices.Count > 1) Lista_Índices.Sort(); // Go by inverted sorted order.

                                                        // Start replacing at the end so the indexes won't change of place.
                                                        for (int Índice = Lista_Índices.Count - 1; Índice >= 0; Índice--)
                                                        {
                                                            List<Ofuscaciones> Lista_Ofuscaciones_Temporal = new List<Ofuscaciones>();
                                                            List<Ofuscaciones> Lista_Ofuscaciones_Líneas_Temporal = new List<Ofuscaciones>();
                                                            for (int Índice_Ofuscación = 0; Índice_Ofuscación < Entrada.Value.Lista_Ofuscaciones.Count; Índice_Ofuscación++)
                                                            {
                                                                if (string.Compare(Entrada.Value.Lista_Ofuscaciones[Índice_Ofuscación].Nombre_Ofuscado, Ofuscación.Nombre_Ofuscado, false) == 0)
                                                                {
                                                                    if (Entrada.Value.Lista_Ofuscaciones[Índice_Ofuscación].Índice_Línea_Inicial > -1 && Entrada.Value.Lista_Ofuscaciones[Índice_Ofuscación].Índice_Línea_Final > -1)
                                                                    {
                                                                        Lista_Ofuscaciones_Líneas_Temporal.Add(Entrada.Value.Lista_Ofuscaciones[Índice_Ofuscación]);
                                                                    }
                                                                    else Lista_Ofuscaciones_Temporal.Add(Entrada.Value.Lista_Ofuscaciones[Índice_Ofuscación]);
                                                                }
                                                            }

                                                            if (Lista_Ofuscaciones_Líneas_Temporal.Count > 0)
                                                            {
                                                                for (int Índice_Ofuscación = Lista_Ofuscaciones_Líneas_Temporal.Count - 1; Índice_Ofuscación >= 0; Índice_Ofuscación--)
                                                                {
                                                                    // Remove the matches not included between the specified lines of code.
                                                                    // Subtract and add 1 to the full range to include the method definition line or it will fail.
                                                                    // Might need to subtract 2 if there is the line of code and also an open bracket below it.
                                                                    if (Lista_Ofuscaciones_Líneas_Temporal[Índice_Ofuscación].Índice_Línea_Inicial - 1 <= Índice_Línea &&
                                                                        Lista_Ofuscaciones_Líneas_Temporal[Índice_Ofuscación].Índice_Línea_Final + 1 >= Índice_Línea) // Is valid.
                                                                    {
                                                                        // Actually do nothing here.
                                                                    }
                                                                    else // Out of range, remove it.
                                                                    {
                                                                        Lista_Ofuscaciones_Líneas_Temporal.RemoveAt(Índice_Ofuscación);
                                                                    }
                                                                }
                                                            }
                                                            // Won't remove any match without a specified line of code, so pick the first one if needed.

                                                            if (Lista_Ofuscaciones_Líneas_Temporal.Count > 0) // Line numbers based matches.
                                                            {
                                                                // Finally apply the choosen match and deobfuscate the source code.
                                                                Línea =
                                                                (Lista_Índices[Índice] > 0 ? Línea.Substring(0, Lista_Índices[Índice]) : null) +
                                                                Lista_Ofuscaciones_Líneas_Temporal[0].Nombre_Desofuscado +
                                                                (Lista_Índices[Índice] + Ofuscación.Nombre_Ofuscado.Length < Línea.Length ? Línea.Substring(Lista_Índices[Índice] + Ofuscación.Nombre_Ofuscado.Length) : null);
                                                            }
                                                            else if (Lista_Ofuscaciones_Temporal.Count > 0) // Second chance with global matches.
                                                            {
                                                                // Finally apply the choosen match and deobfuscate the source code.
                                                                Línea =
                                                                (Lista_Índices[Índice] > 0 ? Línea.Substring(0, Lista_Índices[Índice]) : null) +
                                                                Lista_Ofuscaciones_Temporal[0].Nombre_Desofuscado +
                                                                (Lista_Índices[Índice] + Ofuscación.Nombre_Ofuscado.Length < Línea.Length ? Línea.Substring(Lista_Índices[Índice] + Ofuscación.Nombre_Ofuscado.Length) : null);
                                                            }
                                                            else continue; // Just skip this finding, so sadly it's still obfuscated.
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        // Write the deobfuscated line of code.
                                        Lector_Salida_Texto.WriteLine(Línea);
                                        Lector_Salida_Texto.Flush();
                                        /*foreach (KeyValuePair<string, string> Entrada in Lista_Traducciones)
                                        {
                                            List<int> Lista_Índices = new List<int>();
                                            for (int Índice = 0; Índice < Línea.Length; Índice++)
                                            {
                                                int Índice_Temporal = Línea.IndexOf(Entrada.Key, Índice);
                                                if (Índice_Temporal > -1)
                                                {
                                                    if (Índice_Temporal - 1 > -1 &&
                                                        !char.IsLetterOrDigit(Línea[Índice_Temporal - 1]) &&
                                                        Índice_Temporal + Entrada.Key.Length < Línea.Length &&
                                                        !char.IsLetterOrDigit(Línea[Índice_Temporal + Entrada.Key.Length]))
                                                    {
                                                        // We might have found a new match to translate later on.

                                                    }
                                                }
                                                else break;
                                            }
                                        }*/
                                    }
                                    else
                                    {
                                        Lector_Salida_Texto.WriteLine(Línea);
                                        Lector_Salida_Texto.Flush();
                                    }
                                }
                                Lector_Salida_Texto.Close();
                                Lector_Salida_Texto.Dispose();
                                Lector_Salida_Texto = null;
                                Lector_Salida.Close();
                                Lector_Salida.Dispose();
                                Lector_Salida = null;
                                Lector_Entrada_Texto.Close();
                                Lector_Entrada_Texto.Dispose();
                                Lector_Entrada_Texto = null;
                                Lector_Entrada.Close();
                                Lector_Entrada.Dispose();
                                Lector_Entrada = null;
                            }
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;/* continue;*/ }
                        }
                        Program.Ejecutar_Ruta(Ruta_Salida_Base, ProcessWindowStyle.Maximized);
                        SystemSounds.Asterisk.Play();
                        Matriz_Rutas = null;
                    }
                    Diccionario_Ofuscaciones = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
