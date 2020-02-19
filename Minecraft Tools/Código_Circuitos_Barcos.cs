// This code can generate some sort of "boat racing" tracks made of ice and grass (not intended). [2019-08-31].
/*using Minecraft_Tools.Properties;
using Substrate;
using Substrate.Core;
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
    public partial class Ventana_Generador_Mundos_Aleatorios : Form
    {
        public Ventana_Generador_Mundos_Aleatorios()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Random Worlds Generator by Jupisoft for " + Program.Texto_Usuario;
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

        internal static long Variable_Semilla = 0L;
        internal static int Variable_Dimensiones_X = 512;

        internal Thread Subproceso = null;
        internal bool Pendiente_Subproceso_Abortar = false;
        internal bool Subproceso_Activo = false;

        private void Ventana_Generador_Mundos_Aleatorios_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                NumericUpDown_Semilla.Minimum = long.MinValue;
                NumericUpDown_Semilla.Maximum = long.MaxValue;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
                NumericUpDown_Semilla.Value = Variable_Semilla;
                NumericUpDown_Dimensiones_X.Value = Variable_Dimensiones_X;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Mundos_Aleatorios_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Mundos_Aleatorios_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Subproceso_Activo)
                {
                    if (MessageBox.Show(this, "Currently there is a world conversion in progress.\r\nDo you want to cancel it, but saving what has been done?", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes && Subproceso_Activo) // Since a message can stay on top for infinite time, double check if it's still converting.
                    {
                        Pendiente_Subproceso_Abortar = true;
                    }
                    e.Cancel = true;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Mundos_Aleatorios_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Mundos_Aleatorios_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Generador_Mundos_Aleatorios_DragDrop(object sender, DragEventArgs e)
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
                                    //Minecraft.Información_Niveles Información_Nivel = Minecraft.Información_Niveles.Obtener_Información_Nivel(Ruta);
                                    SystemSounds.Beep.Play();
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

        private void Ventana_Generador_Mundos_Aleatorios_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Mundos_Aleatorios_KeyDown(object sender, KeyEventArgs e)
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

                Menú_Contextual_Variable_.Checked = Variable_;*//*
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
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }*//*
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
                Clave = null;*//*
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
                }*//*
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
                }*//*
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

        private void NumericUpDown_Semilla_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Semilla = (long)NumericUpDown_Semilla.Value;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Dimensiones_X_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Dimensiones_X = (int)NumericUpDown_Dimensiones_X.Value;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Generar_Click(object sender, EventArgs e)
        {
            try
            {
                Subproceso = new Thread(new ThreadStart(Subproceso_DoWork));
                Subproceso.IsBackground = true;
                Subproceso.Priority = ThreadPriority.Normal;
                Subproceso.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal enum Biomas : byte
        {
            Ocean = 0,
            Plains = 1,
            Desert = 2,
            Mountains = 3,
            Ice = 12,
            Frozen_Ocean = 10
        }

        /// <summary>
        /// Thread function that generates a new Minecraft 1.12.2- randomized world.
        /// </summary>
        internal void Subproceso_DoWork()
        {
            bool Subproceso_Abortado = false; // Used to know if the window must be closed.
            try
            {
                Subproceso_Activo = true;
                Stopwatch Cronómetro_Total = Stopwatch.StartNew();
                this.Invoke(new Invocación.Delegado_Control_Cursor(Invocación.Ejecutar_Delegado_Control_Cursor), new object[] { this, Cursors.WaitCursor });
                string Ruta = Program.Ruta_Guardado_Minecraft + "\\" + Program.Obtener_Nombre_Temporal() + " Random world";
                if (Directory.Exists(Ruta))
                {
                    this.Invoke(new Invocación.Delegado_IWin32Window_MessageBox(Invocación.Ejecutar_Delegado_IWin32Window_MessageBox), new object[] { this, "Somehow the directory name for the new Minecraft map already exists.\r\nPlease try it again if the system clock is running properly.\r\nPath: \"" + Ruta + "\".", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning });
                    Ruta = null;
                    return;
                }
                Program.Crear_Carpetas(Ruta);
                AnvilWorld Mundo = AnvilWorld.Create(Ruta);
                Mundo.Level.LevelName = Path.GetFileName(Ruta);
                Mundo.Level.UseMapFeatures = true; // ?
                //Mundo.Level.GeneratorOptions = "1;minecraft:bedrock"; // Not used for now.
                Mundo.Level.GameType = GameType.CREATIVE;
                Mundo.Level.Spawn = new SpawnPoint(0, 255, 0);
                Mundo.Level.AllowCommands = true; // Allow cheats.
                Mundo.Level.GameRules.DoMobSpawning = true; // Spawn mobs.
                Mundo.Level.GameRules.DoFireTick = false; // Prevent the new level to burn out.
                Mundo.Level.GameRules.MobGriefing = false; // Prevent the mobs to destroy anything.
                Mundo.Level.GameRules.KeepInventory = true; // Keep the player inventory.
                Mundo.Level.RainTime = 0;
                Mundo.Level.IsRaining = false;
                Mundo.Level.Player = new Player();
                Mundo.Level.Player.Dimension = 0; // 0 = Overworld, -1 = Nether, +1 = The End.
                Mundo.Level.Player.Position = new Vector3();
                Mundo.Level.Player.Position.X = 0d; // Try to spawn where the player was.
                Mundo.Level.Player.Position.Y = 255d;
                Mundo.Level.Player.Position.Z = 0d;
                Substrate.Orientation Orientación = new Substrate.Orientation();
                Orientación.Pitch = 45d; // -90º a +90º // 45º = Camera centered (looking into the horizon).
                Orientación.Yaw = -45d; // -180º a +180º // -45º = Camera rotation (looking at the southeast).
                Mundo.Level.Player.Rotation = Orientación;
                Mundo.Level.Player.Spawn = new SpawnPoint(0, 255, 0);
                Mundo.Level.Player.Abilities.Flying = true; // Start with creative flight enabled.
                Mundo.Level.RandomSeed = Variable_Semilla; // Copy the original seed.
                Mundo.Level.ThunderTime = 0;
                Mundo.Level.IsThundering = false;

                // Start the multiple dimensions at once to work with them.
                IChunkManager Chunks = Mundo.GetChunkManager(0);
                BlockManager Bloques = Mundo.GetBlockManager(0);
                //IChunkManager Chunks_Nether = Mundo.GetChunkManager(-1);
                //BlockManager Bloques_Nether = Mundo.GetBlockManager(-1);
                //IChunkManager Chunks_The_End = Mundo.GetChunkManager(1);
                //BlockManager Bloques_The_End = Mundo.GetBlockManager(1);

                //int Inicio_X = -(Variable_Dimensiones_X / 2);
                //int Inicio_Z = -(Variable_Dimensiones_X / 2);

                byte[,] Matriz_Altura_Y = new byte[Variable_Dimensiones_X, Variable_Dimensiones_X];
                byte[,] Matriz_Biomas = new byte[Variable_Dimensiones_X, Variable_Dimensiones_X];
                for (int Índice_Z = 0; Índice_Z < Variable_Dimensiones_X; Índice_Z++)
                {
                    for (int Índice_X = 0; Índice_X < Variable_Dimensiones_X; Índice_X++)
                    {
                        try
                        {
                            if (Pendiente_Subproceso_Abortar)
                            {
                                Pendiente_Subproceso_Abortar = false;
                                Chunks.Save(); // Save the part of the chunks already generated.
                                Chunks = null;
                                Bloques = null;
                                Mundo.Save(); // Save the part of the world already generated.
                                Mundo = null;
                                Subproceso_Abortado = true;
                                return; // Cancel safely before time.
                            }
                            //float X = (Variable_Semilla + Índice_X);
                            //float Z = (Variable_Semilla + Índice_Z);
                            //byte Y = (byte)(UnityEngine.Mathf.PerlinNoise(X, Z) * 100f);
                            //byte Bioma = (byte)(UnityEngine.Mathf.PerlinNoise(X, Z) * (float)Biomas.Total);
                            byte Y = (byte)(40d + (Program.Rand_Xoroshiro128p.GetInt32(0, 48)));
                            //byte Bioma = (byte)Program.Rand_Xoroshiro128p.GetInt32(0, (int)Biomas.Total);
                            Matriz_Altura_Y[Índice_X, Índice_Z] = Y; // For generating decoration later on.
                            //Matriz_Biomas[Índice_X, Índice_Z] = Bioma; // For generating similar zones.
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                    }
                }

                // Super slow, average all the heights in 2D.
                byte[,] Matriz_Altura_Y_Original = Matriz_Altura_Y.Clone() as byte[,];
                for (int Índice_Z = 0; Índice_Z < Variable_Dimensiones_X; Índice_Z++)
                {
                    for (int Índice_X = 0; Índice_X < Variable_Dimensiones_X; Índice_X++)
                    {
                        int Total = 0;
                        int Divisores = 0;
                        int Rango = 5; // 1. // 3. // Higher values means a lot slower.
                        for (int Z = -Rango; Z <= Rango; Z++)
                        {
                            for (int X = -Rango; X <= Rango; X++)
                            {
                                int XX = Índice_X + X;
                                int ZZ = Índice_Z + Z;
                                if (XX > -1 && XX < Variable_Dimensiones_X && ZZ > -1 && ZZ < Variable_Dimensiones_X)
                                {
                                    Total += Matriz_Altura_Y_Original[XX, ZZ];
                                    Divisores++;
                                }
                            }
                        }
                        byte Altura = (byte)(Total / Divisores);
                        Matriz_Altura_Y[Índice_X, Índice_Z] = Altura;
                        if (Altura < 48) Matriz_Biomas[Índice_X, Índice_Z] = (byte)Biomas.Ocean;
                        else if (Altura < 56) Matriz_Biomas[Índice_X, Índice_Z] = (byte)Biomas.Frozen_Ocean;
                        else if (Altura < 64) Matriz_Biomas[Índice_X, Índice_Z] = (byte)Biomas.Ice;
                        else if (Altura < 72) Matriz_Biomas[Índice_X, Índice_Z] = (byte)Biomas.Plains;
                        else if (Altura < 80) Matriz_Biomas[Índice_X, Índice_Z] = (byte)Biomas.Desert;
                        else /*if (Altura < 88) *//*Matriz_Biomas[Índice_X, Índice_Z] = (byte)Biomas.Mountains;
                    }
                }
                Matriz_Altura_Y_Original = null;

                for (int Índice_Z = 0; Índice_Z < Variable_Dimensiones_X; Índice_Z += 16)
                {
                    for (int Índice_X = 0; Índice_X < Variable_Dimensiones_X; Índice_X += 16)
                    {
                        if (Pendiente_Subproceso_Abortar)
                        {
                            Pendiente_Subproceso_Abortar = false;
                            Chunks.Save(); // Save the part of the chunks already generated.
                            Chunks = null;
                            Bloques = null;
                            Mundo.Save(); // Save the part of the world already generated.
                            Mundo = null;
                            Subproceso_Abortado = true;
                            return; // Cancel safely before time.
                        }
                        // Actually use real Minecraft biomes but with custom content.
                        AnvilChunk.Biomes_Jupisoft = new ZXByteArray(16, 16);
                        for (int Índice_Z_16 = 0; Índice_Z_16 < 16; Índice_Z_16++)
                        {
                            for (int Índice_X_16 = 0; Índice_X_16 < 16; Índice_X_16++)
                            {
                                AnvilChunk.Biomes_Jupisoft[Índice_X_16, Índice_Z_16] = Matriz_Biomas[Índice_X + Índice_X_16, Índice_Z + Índice_Z_16];
                            }
                        }
                        ChunkRef Chunk = Chunks.CreateChunk(Índice_X / 16, Índice_Z / 16);
                        Chunk.IsLightPopulated = true; // For 1.13+ conversion support.
                        Chunk.IsTerrainPopulated = true;
                        Chunk.Blocks.AutoLight = false;

                        //Chunk.Blocks.RebuildHeightMap(); // Automatic height map.
                        //Chunk.Blocks.RebuildBlockLight(); // Automatic block light.
                        //Chunk.Blocks.RebuildSkyLight(); // Automatic sky light.
                    }
                }
                for (int Índice_Z = 0; Índice_Z < Variable_Dimensiones_X; Índice_Z++)
                {
                    for (int Índice_X = 0; Índice_X < Variable_Dimensiones_X; Índice_X++)
                    {
                        if (Pendiente_Subproceso_Abortar)
                        {
                            Pendiente_Subproceso_Abortar = false;
                            Chunks.Save(); // Save the part of the chunks already generated.
                            Chunks = null;
                            Bloques = null;
                            Mundo.Save(); // Save the part of the world already generated.
                            Mundo = null;
                            Subproceso_Abortado = true;
                            return; // Cancel safely before time.
                        }
                        Biomas Bioma = (Biomas)Matriz_Biomas[Índice_X, Índice_Z];
                        int Altura = Matriz_Altura_Y[Índice_X, Índice_Z];
                        int Altura_0 = (Altura * 0) / 100; // Bedrock.
                        int Altura_1 = (Altura * 12) / 100; // Lava.
                        int Altura_2 = (Altura * 14) / 100; // Stone.
                        int Altura_3 = (Altura * 12) / 100; // Gravel.
                        int Altura_4 = (Altura * 12) / 100; // Sand.
                        int Altura_5 = (Altura * 12) / 100; // Gravel.
                        int Altura_6 = (Altura * 14) / 100; // Stone.
                        int Altura_7 = (Altura * 12) / 100; // Gravel.
                        int Altura_8 = (Altura * 12) / 100; // Dirt.
                        int Altura_9 = (Altura * 0) / 100; // Grass.

                        // Force a minimum of 1 block per "layer" or block type based on a biome.
                        if (Altura_0 < 1) Altura_0 = 1;
                        if (Altura_1 < 1) Altura_1 = 1;
                        if (Altura_2 < 1) Altura_2 = 1;
                        if (Altura_3 < 1) Altura_3 = 1;
                        if (Altura_4 < 1) Altura_4 = 1;
                        if (Altura_5 < 1) Altura_5 = 1;
                        if (Altura_6 < 1) Altura_6 = 1;
                        if (Altura_7 < 1) Altura_7 = 1;
                        if (Altura_8 < 1) Altura_8 = 1;
                        if (Altura_9 < 1) Altura_9 = 1;

                        int ID = 0;
                        int Data = 0;
                        int Altura_Actual = 0;

                        if (Bioma == Biomas.Ocean) { ID = 7; Data = 0; } // Bedrock.
                        else if (Bioma == Biomas.Plains) { ID = 7; Data = 0; } // Bedrock.
                        else if (Bioma == Biomas.Desert) { ID = 7; Data = 0; } // Bedrock.
                        else if (Bioma == Biomas.Mountains) { ID = 7; Data = 0; } // Bedrock.
                        else if (Bioma == Biomas.Ice) { ID = 7; Data = 0; } // Bedrock.
                        else if (Bioma == Biomas.Frozen_Ocean) { ID = 7; Data = 0; } // Bedrock.
                        for (int Índice_Y = Altura_Actual; Índice_Y < Altura_Actual + Altura_0; Índice_Y++) // Bedrock.
                        {
                            Bloques.SetBlock(Índice_X, Índice_Y, Índice_Z, new AlphaBlock(ID, Data));
                        }
                        Altura_Actual += Altura_0;

                        if (Bioma == Biomas.Ocean) { ID = 11; Data = 0; } // Lava.
                        else if (Bioma == Biomas.Plains) { ID = 11; Data = 0; } // Lava.
                        else if (Bioma == Biomas.Desert) { ID = 11; Data = 0; } // Lava.
                        else if (Bioma == Biomas.Mountains) { ID = 11; Data = 0; } // Lava.
                        else if (Bioma == Biomas.Ice) { ID = 11; Data = 0; } // Lava.
                        else if (Bioma == Biomas.Frozen_Ocean) { ID = 11; Data = 0; } // Lava.
                        for (int Índice_Y = Altura_Actual; Índice_Y < Altura_Actual + Altura_1; Índice_Y++) // Lava.
                        {
                            Bloques.SetBlock(Índice_X, Índice_Y, Índice_Z, new AlphaBlock(ID, Data));
                        }
                        Altura_Actual += Altura_1;

                        if (Bioma == Biomas.Ocean) { ID = 1; Data = 0; } // Water.
                        else if (Bioma == Biomas.Plains) { ID = 1; Data = 0; } // Stone.
                        else if (Bioma == Biomas.Desert) { ID = 1; Data = 0; } // Stone.
                        else if (Bioma == Biomas.Mountains) { ID = 1; Data = 0; } // Stone.
                        else if (Bioma == Biomas.Ice) { ID = 1; Data = 0; } // Stone.
                        else if (Bioma == Biomas.Frozen_Ocean) { ID = 1; Data = 0; } // Stone.
                        for (int Índice_Y = Altura_Actual; Índice_Y < Altura_Actual + Altura_2; Índice_Y++) // Stone.
                        {
                            Bloques.SetBlock(Índice_X, Índice_Y, Índice_Z, new AlphaBlock(ID, Data));
                        }
                        Altura_Actual += Altura_2;

                        if (Bioma == Biomas.Ocean) { ID = 13; Data = 0; } // Gravel.
                        else if (Bioma == Biomas.Plains) { ID = 13; Data = 0; } // Gravel.
                        else if (Bioma == Biomas.Desert) { ID = 13; Data = 0; } // Gravel.
                        else if (Bioma == Biomas.Mountains) { ID = 13; Data = 0; } // Gravel.
                        else if (Bioma == Biomas.Ice) { ID = 13; Data = 0; } // Gravel.
                        else if (Bioma == Biomas.Frozen_Ocean) { ID = 13; Data = 0; } // Gravel.
                        for (int Índice_Y = Altura_Actual; Índice_Y < Altura_Actual + Altura_3; Índice_Y++) // Gravel.
                        {
                            Bloques.SetBlock(Índice_X, Índice_Y, Índice_Z, new AlphaBlock(ID, Data));
                        }
                        Altura_Actual += Altura_3;

                        if (Bioma == Biomas.Ocean) { ID = 9; Data = 0; } // Water.
                        else if (Bioma == Biomas.Plains) { ID = 12; Data = 0; } // Sand.
                        else if (Bioma == Biomas.Desert) { ID = 12; Data = 0; } // Sand.
                        else if (Bioma == Biomas.Mountains) { ID = 12; Data = 0; } // Sand.
                        else if (Bioma == Biomas.Ice) { ID = 12; Data = 0; } // Sand.
                        else if (Bioma == Biomas.Frozen_Ocean) { ID = 9; Data = 0; } // Water.
                        for (int Índice_Y = Altura_Actual; Índice_Y < Altura_Actual + Altura_4; Índice_Y++) // Sand.
                        {
                            Bloques.SetBlock(Índice_X, Índice_Y, Índice_Z, new AlphaBlock(ID, Data));
                        }
                        Altura_Actual += Altura_4;

                        if (Bioma == Biomas.Ocean) { ID = 9; Data = 0; } // Water.
                        else if (Bioma == Biomas.Plains) { ID = 13; Data = 0; } // Gravel.
                        else if (Bioma == Biomas.Desert) { ID = 13; Data = 0; } // Gravel.
                        else if (Bioma == Biomas.Mountains) { ID = 13; Data = 0; } // Gravel.
                        else if (Bioma == Biomas.Ice) { ID = 13; Data = 0; } // Gravel.
                        else if (Bioma == Biomas.Frozen_Ocean) { ID = 9; Data = 0; } // Water.
                        for (int Índice_Y = Altura_Actual; Índice_Y < Altura_Actual + Altura_5; Índice_Y++) // Gravel.
                        {
                            Bloques.SetBlock(Índice_X, Índice_Y, Índice_Z, new AlphaBlock(ID, Data));
                        }
                        Altura_Actual += Altura_5;

                        if (Bioma == Biomas.Ocean) { ID = 9; Data = 0; } // Water.
                        else if (Bioma == Biomas.Plains) { ID = 1; Data = 0; } // Stone.
                        else if (Bioma == Biomas.Desert) { ID = 43; Data = 9; } // Smooth sandstone.
                        else if (Bioma == Biomas.Mountains) { ID = 1; Data = 1; } // Granite.
                        else if (Bioma == Biomas.Ice) { ID = 1; Data = 0; } // Stone.
                        else if (Bioma == Biomas.Frozen_Ocean) { ID = 9; Data = 0; } // Water.
                        for (int Índice_Y = Altura_Actual; Índice_Y < Altura_Actual + Altura_6; Índice_Y++) // Stone.
                        {
                            Bloques.SetBlock(Índice_X, Índice_Y, Índice_Z, new AlphaBlock(ID, Data));
                        }
                        Altura_Actual += Altura_6;

                        if (Bioma == Biomas.Ocean) { ID = 9; Data = 0; } // Water.
                        else if (Bioma == Biomas.Plains) { ID = 13; Data = 0; } // Gravel.
                        else if (Bioma == Biomas.Desert) { ID = 24; Data = 0; } // Sandstone.
                        else if (Bioma == Biomas.Mountains) { ID = 1; Data = 0; } // Stone.
                        else if (Bioma == Biomas.Ice) { ID = 80; Data = 0; } // Snow block.
                        else if (Bioma == Biomas.Frozen_Ocean) { ID = 9; Data = 0; } // Water.
                        for (int Índice_Y = Altura_Actual; Índice_Y < Altura_Actual + Altura_7; Índice_Y++) // Gravel.
                        {
                            Bloques.SetBlock(Índice_X, Índice_Y, Índice_Z, new AlphaBlock(ID, Data));
                        }
                        Altura_Actual += Altura_7;

                        if (Bioma == Biomas.Ocean) { ID = 9; Data = 0; } // Water.
                        else if (Bioma == Biomas.Plains) { ID = 3; Data = 0; } // Dirt.
                        else if (Bioma == Biomas.Desert) { ID = 12; Data = 0; } // Sand.
                        else if (Bioma == Biomas.Mountains) { ID = 1; Data = 3; } // Diorite.
                        else if (Bioma == Biomas.Ice) { ID = 174; Data = 0; } // Packed ice.
                        else if (Bioma == Biomas.Frozen_Ocean) { ID = 9; Data = 0; } // Water.
                        for (int Índice_Y = Altura_Actual; Índice_Y < Altura_Actual + Altura_8; Índice_Y++) // Dirt.
                        {
                            Bloques.SetBlock(Índice_X, Índice_Y, Índice_Z, new AlphaBlock(ID, Data));
                        }
                        Altura_Actual += Altura_8;

                        if (Bioma == Biomas.Ocean) { ID = 9; Data = 0; } // Water.
                        else if (Bioma == Biomas.Plains) { ID = 2; Data = 0; } // Grass.
                        else if (Bioma == Biomas.Desert) { ID = 12; Data = 0; } // Sand.
                        else if (Bioma == Biomas.Mountains) { ID = 1; Data = 5; } // Andesite.
                        else if (Bioma == Biomas.Ice) { ID = 79; Data = 0; } // Ice.
                        else if (Bioma == Biomas.Frozen_Ocean) { ID = 174; Data = 0; } // Packed ice.
                        for (int Índice_Y = Altura_Actual; Índice_Y < Altura_Actual + Altura_9; Índice_Y++) // Grass.
                        {
                            Bloques.SetBlock(Índice_X, Índice_Y, Índice_Z, new AlphaBlock(ID, Data));
                        }
                        Altura_Actual += Altura_9;
                    }
                    Chunks.Save(); // Save the chunks of the new region to save RAM memory.
                    //GC.Collect(); // Recover RAM memory after every full chunk?
                    //GC.GetTotalMemory(true);
                }

                // Finally add the decoration like grass, flowers, trees, etc.
                // Note: this separate last pass should avoid terrain overwriting plants, etc.
                for (int Índice_Z = 0; Índice_Z < Variable_Dimensiones_X; Índice_Z++)
                {
                    for (int Índice_X = 0; Índice_X < Variable_Dimensiones_X; Índice_X++)
                    {
                        if (Pendiente_Subproceso_Abortar)
                        {
                            Pendiente_Subproceso_Abortar = false;
                            Chunks.Save(); // Save the part of the chunks already generated.
                            Chunks = null;
                            Bloques = null;
                            Mundo.Save(); // Save the part of the world already generated.
                            Mundo = null;
                            Subproceso_Abortado = true;
                            return; // Cancel safely before time.
                        }
                        Biomas Bioma = (Biomas)Matriz_Biomas[Índice_X, Índice_Z];
                        int Altura = Matriz_Altura_Y[Índice_X, Índice_Z];
                        if (Bioma == Biomas.Plains) // Add vegetation, but all kinds mixed.
                        {
                            if (Program.Rand_Xoroshiro128p.GetInt32(1, 101) > 75) // 25 % chance.
                            {
                                int Desviación = Program.Rand_Xoroshiro128p.GetInt32(0, 15);
                                if (Desviación == 0) Bloques.SetBlock(Índice_X, Altura, Índice_Z, new AlphaBlock(31, 1)); // Grass.
                                else if (Desviación == 1) Bloques.SetBlock(Índice_X, Altura, Índice_Z, new AlphaBlock(31, 2)); // Fern.
                                else if (Desviación == 2) Bloques.SetBlock(Índice_X, Altura, Índice_Z, new AlphaBlock(32, 0)); // Dead bush.
                                else if (Desviación == 3) Bloques.SetBlock(Índice_X, Altura, Índice_Z, new AlphaBlock(37, 0)); // Dandelion.
                                else if (Desviación == 4) Bloques.SetBlock(Índice_X, Altura, Índice_Z, new AlphaBlock(38, 2)); // Allium.
                                else if (Desviación == 5) Bloques.SetBlock(Índice_X, Altura, Índice_Z, new AlphaBlock(38, 3)); // Azure bluet.
                                else if (Desviación == 6) Bloques.SetBlock(Índice_X, Altura, Índice_Z, new AlphaBlock(38, 6)); // White tulip.
                                else if (Desviación == 7) Bloques.SetBlock(Índice_X, Altura, Índice_Z, new AlphaBlock(38, 5)); // Orange tulip.
                                else if (Desviación == 8) Bloques.SetBlock(Índice_X, Altura, Índice_Z, new AlphaBlock(38, 7)); // Pink tulip.
                                else if (Desviación == 9) Bloques.SetBlock(Índice_X, Altura, Índice_Z, new AlphaBlock(38, 0)); // Poppy.
                                else if (Desviación == 10) Bloques.SetBlock(Índice_X, Altura, Índice_Z, new AlphaBlock(38, 1)); // Blue orchid.
                                else if (Desviación == 11) Bloques.SetBlock(Índice_X, Altura, Índice_Z, new AlphaBlock(38, 8)); // Oxeye daisy.
                                else if (Desviación == 12) Bloques.SetBlock(Índice_X, Altura, Índice_Z, new AlphaBlock(38, 4)); // Red tulip.
                                else if (Desviación == 13) Bloques.SetBlock(Índice_X, Altura, Índice_Z, new AlphaBlock(39, 0)); // Brown mushroom.
                                else if (Desviación == 14) Bloques.SetBlock(Índice_X, Altura, Índice_Z, new AlphaBlock(40, 0)); // Red mushroom.
                            }
                        }
                        if (Program.Rand_Xoroshiro128p.GetInt32(1, 1001) > 995) // 0,5 % chance.
                        {
                            if (Bioma == Biomas.Ocean) // Add a sponge cluster.
                            {
                                int Desviación = Altura - Program.Rand_Xoroshiro128p.GetInt32(-10, -3);
                                Bloques.SetBlock(Índice_X, Desviación, Índice_Z, new AlphaBlock(19, 0)); // Sponge.
                                Bloques.SetBlock(Índice_X, Desviación - 1, Índice_Z, new AlphaBlock(19, 1)); // Wet sponge.
                                Bloques.SetBlock(Índice_X, Desviación + 1, Índice_Z, new AlphaBlock(19, 1)); // Wet sponge.
                                Bloques.SetBlock(Índice_X - 1, Desviación, Índice_Z, new AlphaBlock(19, 1)); // Wet sponge.
                                Bloques.SetBlock(Índice_X + 1, Desviación, Índice_Z, new AlphaBlock(19, 1)); // Wet sponge.
                                Bloques.SetBlock(Índice_X, Desviación, Índice_Z - 1, new AlphaBlock(19, 1)); // Wet sponge.
                                Bloques.SetBlock(Índice_X, Desviación, Índice_Z + 1, new AlphaBlock(19, 1)); // Wet sponge.
                            }
                            else if (Bioma == Biomas.Plains) // Add real trees, but randomly mixed.
                            {
                                int Desviación = Altura + Program.Rand_Xoroshiro128p.GetInt32(2, 8);
                                int Tipo_Madera = Program.Rand_Xoroshiro128p.GetInt32(0, 6);
                                int Tipo_Hojas = Program.Rand_Xoroshiro128p.GetInt32(0, 6);
                                AlphaBlock Bloque_Madera;
                                if (Tipo_Madera == 0) Bloque_Madera = new AlphaBlock(17, 0); // Oak log.
                                else if (Tipo_Madera == 1) Bloque_Madera = new AlphaBlock(17, 1); // Spruce log.
                                else if (Tipo_Madera == 2) Bloque_Madera = new AlphaBlock(17, 2); // Birch log.
                                else if (Tipo_Madera == 3) Bloque_Madera = new AlphaBlock(17, 3); // Jungle log.
                                else if (Tipo_Madera == 4) Bloque_Madera = new AlphaBlock(162, 0); // Acacia log.
                                else Bloque_Madera = new AlphaBlock(162, 1); // Dark oak log.
                                AlphaBlock Bloque_Hojas;
                                if (Tipo_Hojas == 0) Bloque_Hojas = new AlphaBlock(18, 0); // Oak leaves.
                                else if (Tipo_Hojas == 1) Bloque_Hojas = new AlphaBlock(18, 1); // Spruce leaves.
                                else if (Tipo_Hojas == 2) Bloque_Hojas = new AlphaBlock(18, 2); // Birch leaves.
                                else if (Tipo_Hojas == 3) Bloque_Hojas = new AlphaBlock(18, 3); // Jungle leaves.
                                else if (Tipo_Hojas == 4) Bloque_Hojas = new AlphaBlock(161, 0); // Acacia leaves.
                                else Bloque_Hojas = new AlphaBlock(161, 1); // Dark oak leaves.
                                for (int Y = 0; Y <= 1; Y++)
                                {
                                    for (int Z = -2; Z <= 2; Z++)
                                    {
                                        for (int X = -2; X <= 2; X++)
                                        {
                                            if (X != 0 || Z != 0)
                                            {
                                                Bloques.SetBlock(Índice_X + X, Desviación + Y, Índice_Z + Z, Bloque_Hojas);
                                            }
                                        }
                                    }
                                }
                                for (int Y = 2; Y <= 3; Y++)
                                {
                                    for (int X = -1; X <= 1; X++)
                                    {
                                        if (X != 0 || Y == 4)
                                        {
                                            Bloques.SetBlock(Índice_X + X, Desviación + Y, Índice_Z, Bloque_Hojas);
                                        }
                                    }
                                }
                                for (int Y = 2; Y <= 3; Y++)
                                {
                                    for (int Z = -1; Z <= 1; Z++)
                                    {
                                        if (Z != 0 || Y == 4)
                                        {
                                            Bloques.SetBlock(Índice_X, Desviación + Y, Índice_Z + Z, Bloque_Hojas);
                                        }
                                    }
                                }
                                for (int Índice_Y = Altura; Índice_Y < Desviación + 3; Índice_Y++)
                                {
                                    Bloques.SetBlock(Índice_X, Índice_Y, Índice_Z, Bloque_Madera);
                                }
                            }
                            else if (Bioma == Biomas.Desert) // Add obelisks.
                            {
                                int Desviación = Altura + Program.Rand_Xoroshiro128p.GetInt32(4, 12);
                                AlphaBlock Bloque_Pilar = new AlphaBlock(24, 1); // Chiseled sandstone.
                                for (int Índice_Y = Altura; Índice_Y < Desviación; Índice_Y++)
                                {
                                    Bloques.SetBlock(Índice_X, Índice_Y, Índice_Z, Bloque_Pilar);
                                }
                                Bloques.SetBlock(Índice_X, Altura + Desviación, Índice_Z, new AlphaBlock(189, 0)); // Birch fence.
                            }
                            else if (Bioma == Biomas.Mountains)
                            {
                                // Add a stone cluster with a secret skull as a random treasure.
                                int Desviación = Program.Rand_Xoroshiro128p.GetInt32(0, 6);
                                if (Desviación == 0) Bloques.SetBlock(Índice_X, Altura - 1, Índice_Z, new AlphaBlock(144, 0)); // Skeleton skull.
                                else if (Desviación == 1) Bloques.SetBlock(Índice_X, Altura - 1, Índice_Z, new AlphaBlock(144, 1)); // Wither skeleton skull.
                                else if (Desviación == 2) Bloques.SetBlock(Índice_X, Altura - 1, Índice_Z, new AlphaBlock(144, 2)); // Zombie head.
                                else if (Desviación == 3) Bloques.SetBlock(Índice_X, Altura - 1, Índice_Z, new AlphaBlock(144, 3)); // Player head.
                                else if (Desviación == 4) Bloques.SetBlock(Índice_X, Altura - 1, Índice_Z, new AlphaBlock(144, 4)); // Creeper head.
                                else Bloques.SetBlock(Índice_X, Altura - 1, Índice_Z, new AlphaBlock(144, 5)); // Dragon head.
                                Bloques.SetBlock(Índice_X, Altura - 2, Índice_Z, new AlphaBlock(1, 6)); // Polished andesite.
                                Bloques.SetBlock(Índice_X, Altura, Índice_Z, new AlphaBlock(1, 6)); // Polished andesite.
                                Bloques.SetBlock(Índice_X - 1, Altura - 1, Índice_Z, new AlphaBlock(1, 6)); // Polished andesite.
                                Bloques.SetBlock(Índice_X + 1, Altura - 1, Índice_Z, new AlphaBlock(1, 6)); // Polished andesite.
                                Bloques.SetBlock(Índice_X, Altura - 1, Índice_Z - 1, new AlphaBlock(1, 6)); // Polished andesite.
                                Bloques.SetBlock(Índice_X, Altura - 1, Índice_Z + 1, new AlphaBlock(1, 6)); // Polished andesite.
                            }
                            else if (Bioma == Biomas.Ice) // Packed ice pillar.
                            {
                                AlphaBlock Bloque_Hielo = new AlphaBlock(174, 0); // Packed ice.
                                for (int Índice_Repetición = 0, Diámetro = 5; Índice_Repetición <= 2; Índice_Repetición++)
                                {
                                    for (int Y = 0; Y <= 2; Y++)
                                    {
                                        for (int Z = -(Diámetro / 2); Z <= Diámetro / 2; Z++)
                                        {
                                            for (int X = -(Diámetro / 2); X <= Diámetro / 2; X++)
                                            {
                                                Bloques.SetBlock(Índice_X + X, Altura + (3 * Índice_Repetición) + Y, Índice_Z + Z, Bloque_Hielo);
                                            }
                                        }
                                    }
                                    Diámetro -= 2;
                                    if (Diámetro < 1) Diámetro = 1; // Should never happen.
                                }
                            }
                            else if (Bioma == Biomas.Frozen_Ocean)
                            {
                                AlphaBlock Bloque_Hielo = new AlphaBlock(174, 0); // Packed ice.
                                for (int Índice_Repetición = 0, Diámetro = 5; Índice_Repetición <= 2; Índice_Repetición++)
                                {
                                    for (int Y = 0; Y <= 2; Y++)
                                    {
                                        for (int Z = -(Diámetro / 2); Z <= Diámetro / 2; Z++)
                                        {
                                            for (int X = -(Diámetro / 2); X <= Diámetro / 2; X++)
                                            {
                                                Bloques.SetBlock(Índice_X + X, Altura + (3 * Índice_Repetición) + Y, Índice_Z + Z, Bloque_Hielo);
                                            }
                                        }
                                    }
                                    Diámetro -= 2;
                                    if (Diámetro < 1) Diámetro = 1; // Should never happen.
                                }
                            }
                        }
                    }
                }
                Chunks.Save();
                Chunks = null;
                Bloques = null;
                Mundo.Save();
                Mundo = null;
                SystemSounds.Asterisk.Play();
            }
            catch (ThreadAbortException) { Subproceso_Abortado = true; } // Aborted, ignore this exception.
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally
            {
                try
                {
                    //ImageMagick.NoiseType = ImageMagick.NoiseType.Uniform;
                    AnvilChunk.Biomes_Jupisoft = null; // Always reset the temporary biome array.
                    Pendiente_Subproceso_Abortar = false;
                    Subproceso_Activo = false;
                    Subproceso = null;
                    GC.Collect(); // Recover RAM memory at the end.
                    GC.GetTotalMemory(true);
                    if (!Subproceso_Abortado)
                    {
                        this.Invoke(new Invocación.Delegado_Control_Cursor(Invocación.Ejecutar_Delegado_Control_Cursor), new object[] { this, Cursors.Default });
                        // Reset all the progress bars.
                        //this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [The original world files will never be modified]" });
                        //this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Región, "Region progress: 0,0000 % (0 of 1.024 chunks)" });
                        //this.Invoke(new Invocación.Delegado_ToolStripLabel_Text(Invocación.Ejecutar_Delegado_ToolStripLabel_Text), new object[] { Barra_Estado_Etiqueta_Progreso_Total, "Total progress: 0,0000 % (0 of 0 regions)" });
                        //this.Invoke(new Invocación.Delegado_ProgressBar_Value(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Chunk, 0 });
                        //this.Invoke(new Invocación.Delegado_ProgressBar_Value(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Región, 0 });
                        //this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { Grupo_Ajustes, true });
                        //this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { Tabla_Bloques, true });
                        //this.Invoke(new Invocación.Delegado_ContextMenuStrip_Enabled(Invocación.Ejecutar_Delegado_ContextMenuStrip_Enabled), new object[] { Menú_Contextual, true });
                        //this.Invoke(new Invocación.Delegado_Control_Select(Invocación.Ejecutar_Delegado_Control_Select), new object[] { Botón_Convertir });
                        //this.Invoke(new Invocación.Delegado_Control_Focus(Invocación.Ejecutar_Delegado_Control_Focus), new object[] { Botón_Convertir });
                    }
                    else this.Invoke(new Invocación.Delegado_Form_Close(Invocación.Ejecutar_Delegado_Form_Close), new object[] { this }); // Close the window.
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            }
        }
    }
}*/