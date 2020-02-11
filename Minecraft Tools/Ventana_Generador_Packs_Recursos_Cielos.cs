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
    public partial class Ventana_Generador_Packs_Recursos_Cielos : Form
    {
        public Ventana_Generador_Packs_Recursos_Cielos()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Sky Box Resource Packs Generator by Jupisoft for " + Program.Texto_Usuario;
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

        internal string Variable_Ruta_Textura_Izquierda = null;
        internal string Variable_Ruta_Textura_Derecha = null;
        internal string Variable_Ruta_Textura_Abajo = null;
        internal string Variable_Ruta_Textura_Arriba = null;
        internal string Variable_Ruta_Textura_Atrás = null;
        internal string Variable_Ruta_Textura_Delante = null;
        internal string Variable_Ruta_Textura_Exportar = null;

        internal Bitmap Variable_Imagen_Textura_Izquierda = Resources.Cielo_Izquierda;
        internal Bitmap Variable_Imagen_Textura_Derecha = Resources.Cielo_Derecha;
        internal Bitmap Variable_Imagen_Textura_Abajo = Resources.Cielo_Abajo;
        internal Bitmap Variable_Imagen_Textura_Arriba = Resources.Cielo_Arriba;
        internal Bitmap Variable_Imagen_Textura_Atrás = Resources.Cielo_Atrás;
        internal Bitmap Variable_Imagen_Textura_Delante = Resources.Cielo_Delante;
        internal Bitmap Variable_Imagen_Textura_Exportar = null;

        private void Ventana_Generador_Packs_Recursos_Cielos_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                Grupo_Textura_Izquierda.AllowDrop = true;
                Grupo_Textura_Derecha.AllowDrop = true;
                Grupo_Textura_Abajo.AllowDrop = true;
                Grupo_Textura_Arriba.AllowDrop = true;
                Grupo_Textura_Atrás.AllowDrop = true;
                Grupo_Textura_Delante.AllowDrop = true;
                Grupo_Exportar.AllowDrop = true;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
                ComboBox_Formato_Pack.SelectedIndex = 3;
                ComboBox_Textura_Izquierda_Rotación.SelectedIndex = 0;
                ComboBox_Textura_Derecha_Rotación.SelectedIndex = 0;
                ComboBox_Textura_Abajo_Rotación.SelectedIndex = 0;
                ComboBox_Textura_Arriba_Rotación.SelectedIndex = 0;
                ComboBox_Textura_Atrás_Rotación.SelectedIndex = 0;
                ComboBox_Textura_Delante_Rotación.SelectedIndex = 0;
                ComboBox_Exportar_Rotación.SelectedIndex = 0;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Packs_Recursos_Cielos_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Packs_Recursos_Cielos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Packs_Recursos_Cielos_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Packs_Recursos_Cielos_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Generador_Packs_Recursos_Cielos_DragDrop(object sender, DragEventArgs e)
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

        private void Ventana_Generador_Packs_Recursos_Cielos_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Packs_Recursos_Cielos_KeyDown(object sender, KeyEventArgs e)
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

        private void Menú_Contextual_Rotaciones_Predeterminadas_Click(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Textura_Izquierda_Rotación.SelectedIndex = 0;
                ComboBox_Textura_Derecha_Rotación.SelectedIndex = 0;
                ComboBox_Textura_Abajo_Rotación.SelectedIndex = 0;
                ComboBox_Textura_Arriba_Rotación.SelectedIndex = 0;
                ComboBox_Textura_Atrás_Rotación.SelectedIndex = 0;
                ComboBox_Textura_Delante_Rotación.SelectedIndex = 0;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Rotaciones_Cube_Sauerbraten_Click(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Textura_Izquierda_Rotación.SelectedIndex = 0;
                ComboBox_Textura_Derecha_Rotación.SelectedIndex = 0;
                ComboBox_Textura_Abajo_Rotación.SelectedIndex = 0;
                ComboBox_Textura_Arriba_Rotación.SelectedIndex = (int)RotateFlipType.Rotate270FlipNone;
                ComboBox_Textura_Atrás_Rotación.SelectedIndex = 0;
                ComboBox_Textura_Delante_Rotación.SelectedIndex = 0;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Rotaciones_Unity_Click(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Textura_Izquierda_Rotación.SelectedIndex = 0;
                ComboBox_Textura_Derecha_Rotación.SelectedIndex = 0;
                ComboBox_Textura_Abajo_Rotación.SelectedIndex = 0;
                ComboBox_Textura_Arriba_Rotación.SelectedIndex = (int)RotateFlipType.Rotate180FlipNone;
                ComboBox_Textura_Atrás_Rotación.SelectedIndex = 0;
                ComboBox_Textura_Delante_Rotación.SelectedIndex = 0;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Descargar_Texturas_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox_Textura_Izquierda_Ruta.Text = null;
                TextBox_Textura_Derecha_Ruta.Text = null;
                TextBox_Textura_Abajo_Ruta.Text = null;
                TextBox_Textura_Arriba_Ruta.Text = null;
                TextBox_Textura_Atrás_Ruta.Text = null;
                TextBox_Textura_Delante_Ruta.Text = null;
                TextBox_Exportar_Ruta.Text = null;
                Variable_Ruta_Textura_Izquierda = null;
                Variable_Ruta_Textura_Derecha = null;
                Variable_Ruta_Textura_Abajo = null;
                Variable_Ruta_Textura_Arriba = null;
                Variable_Ruta_Textura_Atrás = null;
                Variable_Ruta_Textura_Delante = null;
                Variable_Ruta_Textura_Exportar = null;
                Variable_Imagen_Textura_Izquierda = Resources.Cielo_Izquierda;
                Variable_Imagen_Textura_Derecha = Resources.Cielo_Derecha;
                Variable_Imagen_Textura_Abajo = Resources.Cielo_Abajo;
                Variable_Imagen_Textura_Arriba = Resources.Cielo_Arriba;
                Variable_Imagen_Textura_Atrás = Resources.Cielo_Atrás;
                Variable_Imagen_Textura_Delante = Resources.Cielo_Delante;
                if (Variable_Imagen_Textura_Exportar != null)
                {
                    Variable_Imagen_Textura_Exportar.Dispose();
                    Variable_Imagen_Textura_Exportar = null;
                }
                Picture_Textura_Izquierda.Image = Resources.Cielo_Izquierda;
                Picture_Textura_Derecha.Image = Resources.Cielo_Derecha;
                Picture_Textura_Abajo.Image = Resources.Cielo_Abajo;
                Picture_Textura_Arriba.Image = Resources.Cielo_Arriba;
                Picture_Textura_Atrás.Image = Resources.Cielo_Atrás;
                Picture_Textura_Delante.Image = Resources.Cielo_Delante;
                Picture_Exportar.Image = null;
                SystemSounds.Asterisk.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Cargar_Texturas_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (Variable_Imagen_Textura_Izquierda != null || Variable_Imagen_Textura_Derecha != null || Variable_Imagen_Textura_Abajo != null || Variable_Imagen_Textura_Arriba != null || Variable_Imagen_Textura_Atrás != null || Variable_Imagen_Textura_Delante != null)
                {
                    string Ruta_Base = null;
                    string Nombre = null;
                    string Extensión = null;
                    if (Variable_Imagen_Textura_Izquierda != null)
                    {
                        Ruta_Base = Path.GetDirectoryName(Variable_Ruta_Textura_Izquierda);
                        Nombre = Path.GetFileNameWithoutExtension(Variable_Ruta_Textura_Izquierda);
                        Extensión = Path.GetExtension(Variable_Ruta_Textura_Izquierda);
                    }
                    else if (Variable_Imagen_Textura_Derecha != null)
                    {
                        Ruta_Base = Path.GetDirectoryName(Variable_Ruta_Textura_Derecha);
                        Nombre = Path.GetFileNameWithoutExtension(Variable_Ruta_Textura_Derecha);
                        Extensión = Path.GetExtension(Variable_Ruta_Textura_Derecha);
                    }
                    else if (Variable_Imagen_Textura_Abajo != null)
                    {
                        Ruta_Base = Path.GetDirectoryName(Variable_Ruta_Textura_Abajo);
                        Nombre = Path.GetFileNameWithoutExtension(Variable_Ruta_Textura_Abajo);
                        Extensión = Path.GetExtension(Variable_Ruta_Textura_Abajo);
                    }
                    else if (Variable_Imagen_Textura_Arriba != null)
                    {
                        Ruta_Base = Path.GetDirectoryName(Variable_Ruta_Textura_Arriba);
                        Nombre = Path.GetFileNameWithoutExtension(Variable_Ruta_Textura_Arriba);
                        Extensión = Path.GetExtension(Variable_Ruta_Textura_Arriba);
                    }
                    else if (Variable_Imagen_Textura_Atrás != null)
                    {
                        Ruta_Base = Path.GetDirectoryName(Variable_Ruta_Textura_Atrás);
                        Nombre = Path.GetFileNameWithoutExtension(Variable_Ruta_Textura_Atrás);
                        Extensión = Path.GetExtension(Variable_Ruta_Textura_Atrás);
                    }
                    else if (Variable_Imagen_Textura_Delante != null)
                    {
                        Ruta_Base = Path.GetDirectoryName(Variable_Ruta_Textura_Delante);
                        Nombre = Path.GetFileNameWithoutExtension(Variable_Ruta_Textura_Delante);
                        Extensión = Path.GetExtension(Variable_Ruta_Textura_Delante);
                    }
                    if (!string.IsNullOrEmpty(Ruta_Base) && Directory.Exists(Ruta_Base))
                    {
                        string[] Matriz_Archivos = Directory.GetFiles(Ruta_Base, "*" + Extensión, SearchOption.TopDirectoryOnly);
                        if (Matriz_Archivos != null && Matriz_Archivos.Length >= 6)
                        {
                            Array.Sort(Matriz_Archivos);
                            if (Nombre.EndsWith("Left", StringComparison.InvariantCultureIgnoreCase) ||
                                Nombre.EndsWith("Right", StringComparison.InvariantCultureIgnoreCase) ||
                                Nombre.EndsWith("Bottom", StringComparison.InvariantCultureIgnoreCase) ||
                                Nombre.EndsWith("Top", StringComparison.InvariantCultureIgnoreCase) ||
                                Nombre.EndsWith("Back", StringComparison.InvariantCultureIgnoreCase) ||
                                Nombre.EndsWith("Front", StringComparison.InvariantCultureIgnoreCase))
                            { // Unity assets from "Day-Night Skyboxes" sky box file names.
                                if (Nombre.EndsWith("Left", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Nombre = Nombre.Substring(0, Nombre.Length - 4);
                                }
                                else if (Nombre.EndsWith("Right", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Nombre = Nombre.Substring(0, Nombre.Length - 5);
                                }
                                else if (Nombre.EndsWith("Bottom", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Nombre = Nombre.Substring(0, Nombre.Length - 6);
                                }
                                else if (Nombre.EndsWith("Top", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Nombre = Nombre.Substring(0, Nombre.Length - 3);
                                }
                                else if (Nombre.EndsWith("Back", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Nombre = Nombre.Substring(0, Nombre.Length - 4);
                                }
                                else if (Nombre.EndsWith("Front", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Nombre = Nombre.Substring(0, Nombre.Length - 5);
                                }
                                Grupo_Textura_Izquierda_DragDrop(Grupo_Textura_Izquierda, new DragEventArgs(new DataObject(DataFormats.FileDrop, new string[1] { Ruta_Base + "\\" + Nombre + "Left" + Extensión }), 0, 0, 0, DragDropEffects.All, DragDropEffects.Copy));
                                Grupo_Textura_Derecha_DragDrop(Grupo_Textura_Derecha, new DragEventArgs(new DataObject(DataFormats.FileDrop, new string[1] { Ruta_Base + "\\" + Nombre + "Right" + Extensión }), 0, 0, 0, DragDropEffects.All, DragDropEffects.Copy));
                                Grupo_Textura_Abajo_DragDrop(Grupo_Textura_Abajo, new DragEventArgs(new DataObject(DataFormats.FileDrop, new string[1] { Ruta_Base + "\\" + Nombre + "Bottom" + Extensión }), 0, 0, 0, DragDropEffects.All, DragDropEffects.Copy));
                                Grupo_Textura_Arriba_DragDrop(Grupo_Textura_Arriba, new DragEventArgs(new DataObject(DataFormats.FileDrop, new string[1] { Ruta_Base + "\\" + Nombre + "Top" + Extensión }), 0, 0, 0, DragDropEffects.All, DragDropEffects.Copy));
                                Grupo_Textura_Atrás_DragDrop(Grupo_Textura_Atrás, new DragEventArgs(new DataObject(DataFormats.FileDrop, new string[1] { Ruta_Base + "\\" + Nombre + "Back" + Extensión }), 0, 0, 0, DragDropEffects.All, DragDropEffects.Copy));
                                Grupo_Textura_Delante_DragDrop(Grupo_Textura_Delante, new DragEventArgs(new DataObject(DataFormats.FileDrop, new string[1] { Ruta_Base + "\\" + Nombre + "Front" + Extensión }), 0, 0, 0, DragDropEffects.All, DragDropEffects.Copy));
                                SystemSounds.Asterisk.Play();
                            }
                            else if (Nombre.StartsWith("left", StringComparison.InvariantCultureIgnoreCase) ||
                                Nombre.StartsWith("right", StringComparison.InvariantCultureIgnoreCase) ||
                                Nombre.StartsWith("down", StringComparison.InvariantCultureIgnoreCase) ||
                                Nombre.StartsWith("up", StringComparison.InvariantCultureIgnoreCase) ||
                                Nombre.StartsWith("back", StringComparison.InvariantCultureIgnoreCase) ||
                                Nombre.StartsWith("front", StringComparison.InvariantCultureIgnoreCase))
                            { // Unity assets from "SkyBox Volume 2" sky box file names.
                                if (Nombre.StartsWith("left", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Nombre = Nombre.Substring(4);
                                }
                                else if (Nombre.StartsWith("right", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Nombre = Nombre.Substring(5);
                                }
                                else if (Nombre.StartsWith("down", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Nombre = Nombre.Substring(4);
                                }
                                else if (Nombre.StartsWith("up", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Nombre = Nombre.Substring(2);
                                }
                                else if (Nombre.StartsWith("back", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Nombre = Nombre.Substring(4);
                                }
                                else if (Nombre.StartsWith("front", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Nombre = Nombre.Substring(5);
                                }
                                Grupo_Textura_Izquierda_DragDrop(Grupo_Textura_Izquierda, new DragEventArgs(new DataObject(DataFormats.FileDrop, new string[1] { Ruta_Base + "\\" + "left" + Nombre + Extensión }), 0, 0, 0, DragDropEffects.All, DragDropEffects.Copy));
                                Grupo_Textura_Derecha_DragDrop(Grupo_Textura_Derecha, new DragEventArgs(new DataObject(DataFormats.FileDrop, new string[1] { Ruta_Base + "\\" + "right" + Nombre + Extensión }), 0, 0, 0, DragDropEffects.All, DragDropEffects.Copy));
                                Grupo_Textura_Abajo_DragDrop(Grupo_Textura_Abajo, new DragEventArgs(new DataObject(DataFormats.FileDrop, new string[1] { Ruta_Base + "\\" + "down" + Nombre + Extensión }), 0, 0, 0, DragDropEffects.All, DragDropEffects.Copy));
                                Grupo_Textura_Arriba_DragDrop(Grupo_Textura_Arriba, new DragEventArgs(new DataObject(DataFormats.FileDrop, new string[1] { Ruta_Base + "\\" + "up" + Nombre + Extensión }), 0, 0, 0, DragDropEffects.All, DragDropEffects.Copy));
                                Grupo_Textura_Atrás_DragDrop(Grupo_Textura_Atrás, new DragEventArgs(new DataObject(DataFormats.FileDrop, new string[1] { Ruta_Base + "\\" + "back" + Nombre + Extensión }), 0, 0, 0, DragDropEffects.All, DragDropEffects.Copy));
                                Grupo_Textura_Delante_DragDrop(Grupo_Textura_Delante, new DragEventArgs(new DataObject(DataFormats.FileDrop, new string[1] { Ruta_Base + "\\" + "front" + Nombre + Extensión }), 0, 0, 0, DragDropEffects.All, DragDropEffects.Copy));
                                SystemSounds.Asterisk.Play();
                            }
                            else if (Nombre.EndsWith("lf", StringComparison.InvariantCultureIgnoreCase) ||
                                Nombre.EndsWith("rt", StringComparison.InvariantCultureIgnoreCase) ||
                                Nombre.EndsWith("dn", StringComparison.InvariantCultureIgnoreCase) ||
                                Nombre.EndsWith("up", StringComparison.InvariantCultureIgnoreCase) ||
                                Nombre.EndsWith("bk", StringComparison.InvariantCultureIgnoreCase) ||
                                Nombre.EndsWith("ft", StringComparison.InvariantCultureIgnoreCase))
                            { // "Cube" / "Cube 2 Sauerbraten" sky box file names.
                                Nombre = Nombre.Substring(0, Nombre.Length - 2);
                                Grupo_Textura_Izquierda_DragDrop(Grupo_Textura_Izquierda, new DragEventArgs(new DataObject(DataFormats.FileDrop, new string[1] { Ruta_Base + "\\" + Nombre + "lf" + Extensión }), 0, 0, 0, DragDropEffects.All, DragDropEffects.Copy));
                                Grupo_Textura_Derecha_DragDrop(Grupo_Textura_Derecha, new DragEventArgs(new DataObject(DataFormats.FileDrop, new string[1] { Ruta_Base + "\\" + Nombre + "rt" + Extensión }), 0, 0, 0, DragDropEffects.All, DragDropEffects.Copy));
                                Grupo_Textura_Abajo_DragDrop(Grupo_Textura_Abajo, new DragEventArgs(new DataObject(DataFormats.FileDrop, new string[1] { Ruta_Base + "\\" + Nombre + "dn" + Extensión }), 0, 0, 0, DragDropEffects.All, DragDropEffects.Copy));
                                Grupo_Textura_Arriba_DragDrop(Grupo_Textura_Arriba, new DragEventArgs(new DataObject(DataFormats.FileDrop, new string[1] { Ruta_Base + "\\" + Nombre + "up" + Extensión }), 0, 0, 0, DragDropEffects.All, DragDropEffects.Copy));
                                Grupo_Textura_Atrás_DragDrop(Grupo_Textura_Atrás, new DragEventArgs(new DataObject(DataFormats.FileDrop, new string[1] { Ruta_Base + "\\" + Nombre + "bk" + Extensión }), 0, 0, 0, DragDropEffects.All, DragDropEffects.Copy));
                                Grupo_Textura_Delante_DragDrop(Grupo_Textura_Delante, new DragEventArgs(new DataObject(DataFormats.FileDrop, new string[1] { Ruta_Base + "\\" + Nombre + "ft" + Extensión }), 0, 0, 0, DragDropEffects.All, DragDropEffects.Copy));
                                SystemSounds.Asterisk.Play();
                            }
                            else SystemSounds.Beep.Play(); // Unknown... load by file name order?
                        }
                        else SystemSounds.Beep.Play();
                    }
                    else SystemSounds.Beep.Play();
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
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

        private void Grupo_Textura_Izquierda_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Grupo_Textura_Izquierda_DragDrop(object sender, DragEventArgs e)
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
                                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Indeterminate);
                                    if (Imagen != null)
                                    {
                                        Variable_Ruta_Textura_Izquierda = Ruta;
                                        Variable_Imagen_Textura_Izquierda = Imagen;
                                        TextBox_Textura_Izquierda_Ruta.Text = Ruta;
                                        Picture_Textura_Izquierda.Image = Imagen;
                                        break;
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
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Grupo_Textura_Derecha_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Grupo_Textura_Derecha_DragDrop(object sender, DragEventArgs e)
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
                                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Indeterminate);
                                    if (Imagen != null)
                                    {
                                        Variable_Ruta_Textura_Derecha = Ruta;
                                        Variable_Imagen_Textura_Derecha = Imagen;
                                        TextBox_Textura_Derecha_Ruta.Text = Ruta;
                                        Picture_Textura_Derecha.Image = Imagen;
                                        break;
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
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Grupo_Textura_Abajo_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Grupo_Textura_Abajo_DragDrop(object sender, DragEventArgs e)
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
                                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Indeterminate);
                                    if (Imagen != null)
                                    {
                                        Variable_Ruta_Textura_Abajo = Ruta;
                                        Variable_Imagen_Textura_Abajo = Imagen;
                                        TextBox_Textura_Abajo_Ruta.Text = Ruta;
                                        Picture_Textura_Abajo.Image = Imagen;
                                        break;
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
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Grupo_Textura_Arriba_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Grupo_Textura_Arriba_DragDrop(object sender, DragEventArgs e)
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
                                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Indeterminate);
                                    if (Imagen != null)
                                    {
                                        Variable_Ruta_Textura_Arriba = Ruta;
                                        Variable_Imagen_Textura_Arriba = Imagen;
                                        TextBox_Textura_Arriba_Ruta.Text = Ruta;
                                        Picture_Textura_Arriba.Image = Imagen;
                                        break;
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
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Grupo_Textura_Atrás_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Grupo_Textura_Atrás_DragDrop(object sender, DragEventArgs e)
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
                                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Indeterminate);
                                    if (Imagen != null)
                                    {
                                        Variable_Ruta_Textura_Atrás = Ruta;
                                        Variable_Imagen_Textura_Atrás = Imagen;
                                        TextBox_Textura_Atrás_Ruta.Text = Ruta;
                                        Picture_Textura_Atrás.Image = Imagen;
                                        break;
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
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Grupo_Textura_Delante_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Grupo_Textura_Delante_DragDrop(object sender, DragEventArgs e)
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
                                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Indeterminate);
                                    if (Imagen != null)
                                    {
                                        Variable_Ruta_Textura_Derecha = Ruta;
                                        Variable_Imagen_Textura_Delante = Imagen;
                                        TextBox_Textura_Delante_Ruta.Text = Ruta;
                                        Picture_Textura_Delante.Image = Imagen;
                                        break;
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
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Grupo_Exportar_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Grupo_Exportar_DragDrop(object sender, DragEventArgs e)
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
                                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Indeterminate);
                                    if (Imagen != null)
                                    {
                                        Variable_Ruta_Textura_Exportar = Ruta;
                                        Variable_Imagen_Textura_Exportar = Imagen;
                                        TextBox_Exportar_Ruta.Text = Ruta;
                                        Picture_Exportar.Image = Imagen;
                                        break;
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
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Generar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (Variable_Imagen_Textura_Izquierda != null && Variable_Imagen_Textura_Derecha != null && Variable_Imagen_Textura_Abajo != null && Variable_Imagen_Textura_Arriba != null && Variable_Imagen_Textura_Atrás != null && Variable_Imagen_Textura_Delante != null)
                {
                    Graphics Pintar = null;
                    Bitmap Imagen_Textura_Izquierda = null;
                    Bitmap Imagen_Textura_Derecha = null;
                    Bitmap Imagen_Textura_Abajo = null;
                    Bitmap Imagen_Textura_Arriba = null;
                    Bitmap Imagen_Textura_Atrás = null;
                    Bitmap Imagen_Textura_Delante = null;
                    List<int> Lista_Ancho = new List<int>(new int[6] { Variable_Imagen_Textura_Izquierda.Width, Variable_Imagen_Textura_Derecha.Width, Variable_Imagen_Textura_Abajo.Width, Variable_Imagen_Textura_Arriba.Width, Variable_Imagen_Textura_Atrás.Width, Variable_Imagen_Textura_Delante.Width });
                    List<int> Lista_Alto = new List<int>(new int[6] { Variable_Imagen_Textura_Izquierda.Height, Variable_Imagen_Textura_Derecha.Height, Variable_Imagen_Textura_Abajo.Height, Variable_Imagen_Textura_Arriba.Height, Variable_Imagen_Textura_Atrás.Height, Variable_Imagen_Textura_Delante.Height });
                    Lista_Ancho.Sort();
                    Lista_Alto.Sort();
                    int Mínimo_Ancho_Alto = Math.Min(Lista_Ancho[0], Lista_Alto[0]); // Use the same width and height.
                    Lista_Ancho = null;
                    Lista_Alto = null;

                    int Ancho = Variable_Imagen_Textura_Izquierda.Width;
                    int Alto = Variable_Imagen_Textura_Izquierda.Height;
                    if (Ancho != Mínimo_Ancho_Alto || Alto != Mínimo_Ancho_Alto)
                    {
                        Imagen_Textura_Izquierda = new Bitmap(Mínimo_Ancho_Alto, Mínimo_Ancho_Alto, Variable_Imagen_Textura_Izquierda.PixelFormat);
                        Pintar = Graphics.FromImage(Imagen_Textura_Izquierda);
                        Pintar.CompositingMode = CompositingMode.SourceCopy;
                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar.SmoothingMode = SmoothingMode.None;
                        if (!CheckBox_Recortar_Imágenes.Checked) // Stretch the texture.
                        {
                            Pintar.DrawImage(Variable_Imagen_Textura_Izquierda, new Rectangle(0, 0, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                        }
                        else // Cut and zoom the texture keeping it's aspect ratio.
                        {
                            int Ancho_Alto = Math.Min(Ancho, Alto);
                            Pintar.DrawImage(Variable_Imagen_Textura_Izquierda, new Rectangle(0, 0, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), new Rectangle(0, 0, Ancho_Alto, Ancho_Alto), GraphicsUnit.Pixel);
                        }
                        Pintar.Dispose();
                        Pintar = null; // Now this texture should have equal width and height as the others.
                    }
                    else Imagen_Textura_Izquierda = (Bitmap)Variable_Imagen_Textura_Izquierda.Clone();

                    Ancho = Variable_Imagen_Textura_Derecha.Width;
                    Alto = Variable_Imagen_Textura_Derecha.Height;
                    if (Ancho != Mínimo_Ancho_Alto || Alto != Mínimo_Ancho_Alto)
                    {
                        Imagen_Textura_Derecha = new Bitmap(Mínimo_Ancho_Alto, Mínimo_Ancho_Alto, Variable_Imagen_Textura_Derecha.PixelFormat);
                        Pintar = Graphics.FromImage(Imagen_Textura_Derecha);
                        Pintar.CompositingMode = CompositingMode.SourceCopy;
                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar.SmoothingMode = SmoothingMode.HighQuality;
                        Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                        if (!CheckBox_Recortar_Imágenes.Checked) // Stretch the texture.
                        {
                            Pintar.DrawImage(Variable_Imagen_Textura_Derecha, new Rectangle(0, 0, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                        }
                        else // Cut and zoom the texture keeping it's aspect ratio.
                        {
                            int Ancho_Alto = Math.Min(Ancho, Alto);
                            Pintar.DrawImage(Variable_Imagen_Textura_Derecha, new Rectangle(0, 0, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), new Rectangle(0, 0, Ancho_Alto, Ancho_Alto), GraphicsUnit.Pixel);
                        }
                        Pintar.Dispose();
                        Pintar = null; // Now this texture should have equal width and height as the others.
                    }
                    else Imagen_Textura_Derecha = (Bitmap)Variable_Imagen_Textura_Derecha.Clone();

                    Ancho = Variable_Imagen_Textura_Abajo.Width;
                    Alto = Variable_Imagen_Textura_Abajo.Height;
                    if (Ancho != Mínimo_Ancho_Alto || Alto != Mínimo_Ancho_Alto)
                    {
                        Imagen_Textura_Abajo = new Bitmap(Mínimo_Ancho_Alto, Mínimo_Ancho_Alto, Variable_Imagen_Textura_Abajo.PixelFormat);
                        Pintar = Graphics.FromImage(Imagen_Textura_Abajo);
                        Pintar.CompositingMode = CompositingMode.SourceCopy;
                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar.SmoothingMode = SmoothingMode.HighQuality;
                        Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                        if (!CheckBox_Recortar_Imágenes.Checked) // Stretch the texture.
                        {
                            Pintar.DrawImage(Variable_Imagen_Textura_Abajo, new Rectangle(0, 0, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                        }
                        else // Cut and zoom the texture keeping it's aspect ratio.
                        {
                            int Ancho_Alto = Math.Min(Ancho, Alto);
                            Pintar.DrawImage(Variable_Imagen_Textura_Abajo, new Rectangle(0, 0, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), new Rectangle(0, 0, Ancho_Alto, Ancho_Alto), GraphicsUnit.Pixel);
                        }
                        Pintar.Dispose();
                        Pintar = null; // Now this texture should have equal width and height as the others.
                    }
                    else Imagen_Textura_Abajo = (Bitmap)Variable_Imagen_Textura_Abajo.Clone();

                    Ancho = Variable_Imagen_Textura_Arriba.Width;
                    Alto = Variable_Imagen_Textura_Arriba.Height;
                    if (Ancho != Mínimo_Ancho_Alto || Alto != Mínimo_Ancho_Alto)
                    {
                        Imagen_Textura_Arriba = new Bitmap(Mínimo_Ancho_Alto, Mínimo_Ancho_Alto, Variable_Imagen_Textura_Arriba.PixelFormat);
                        Pintar = Graphics.FromImage(Imagen_Textura_Arriba);
                        Pintar.CompositingMode = CompositingMode.SourceCopy;
                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar.SmoothingMode = SmoothingMode.HighQuality;
                        Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                        if (!CheckBox_Recortar_Imágenes.Checked) // Stretch the texture.
                        {
                            Pintar.DrawImage(Variable_Imagen_Textura_Arriba, new Rectangle(0, 0, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                        }
                        else // Cut and zoom the texture keeping it's aspect ratio.
                        {
                            int Ancho_Alto = Math.Min(Ancho, Alto);
                            Pintar.DrawImage(Variable_Imagen_Textura_Arriba, new Rectangle(0, 0, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), new Rectangle(0, 0, Ancho_Alto, Ancho_Alto), GraphicsUnit.Pixel);
                        }
                        Pintar.Dispose();
                        Pintar = null; // Now this texture should have equal width and height as the others.
                    }
                    else Imagen_Textura_Arriba = (Bitmap)Variable_Imagen_Textura_Arriba.Clone();

                    Ancho = Variable_Imagen_Textura_Atrás.Width;
                    Alto = Variable_Imagen_Textura_Atrás.Height;
                    if (Ancho != Mínimo_Ancho_Alto || Alto != Mínimo_Ancho_Alto)
                    {
                        Imagen_Textura_Atrás = new Bitmap(Mínimo_Ancho_Alto, Mínimo_Ancho_Alto, Variable_Imagen_Textura_Atrás.PixelFormat);
                        Pintar = Graphics.FromImage(Imagen_Textura_Atrás);
                        Pintar.CompositingMode = CompositingMode.SourceCopy;
                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar.SmoothingMode = SmoothingMode.HighQuality;
                        Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                        if (!CheckBox_Recortar_Imágenes.Checked) // Stretch the texture.
                        {
                            Pintar.DrawImage(Variable_Imagen_Textura_Atrás, new Rectangle(0, 0, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                        }
                        else // Cut and zoom the texture keeping it's aspect ratio.
                        {
                            int Ancho_Alto = Math.Min(Ancho, Alto);
                            Pintar.DrawImage(Variable_Imagen_Textura_Atrás, new Rectangle(0, 0, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), new Rectangle(0, 0, Ancho_Alto, Ancho_Alto), GraphicsUnit.Pixel);
                        }
                        Pintar.Dispose();
                        Pintar = null; // Now this texture should have equal width and height as the others.
                    }
                    else Imagen_Textura_Atrás = (Bitmap)Variable_Imagen_Textura_Atrás.Clone();

                    Ancho = Variable_Imagen_Textura_Delante.Width;
                    Alto = Variable_Imagen_Textura_Delante.Height;
                    if (Ancho != Mínimo_Ancho_Alto || Alto != Mínimo_Ancho_Alto)
                    {
                        Imagen_Textura_Delante = new Bitmap(Mínimo_Ancho_Alto, Mínimo_Ancho_Alto, Variable_Imagen_Textura_Delante.PixelFormat);
                        Pintar = Graphics.FromImage(Imagen_Textura_Delante);
                        Pintar.CompositingMode = CompositingMode.SourceCopy;
                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar.SmoothingMode = SmoothingMode.HighQuality;
                        Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                        if (!CheckBox_Recortar_Imágenes.Checked) // Stretch the texture.
                        {
                            Pintar.DrawImage(Variable_Imagen_Textura_Delante, new Rectangle(0, 0, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                        }
                        else // Cut and zoom the texture keeping it's aspect ratio.
                        {
                            int Ancho_Alto = Math.Min(Ancho, Alto);
                            Pintar.DrawImage(Variable_Imagen_Textura_Delante, new Rectangle(0, 0, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), new Rectangle(0, 0, Ancho_Alto, Ancho_Alto), GraphicsUnit.Pixel);
                        }
                        Pintar.Dispose();
                        Pintar = null; // Now this texture should have equal width and height as the others.
                    }
                    else Imagen_Textura_Delante = (Bitmap)Variable_Imagen_Textura_Delante.Clone();

                    if (ComboBox_Textura_Izquierda_Rotación.SelectedIndex > 0) Imagen_Textura_Izquierda.RotateFlip((RotateFlipType)ComboBox_Textura_Izquierda_Rotación.SelectedIndex);
                    if (ComboBox_Textura_Derecha_Rotación.SelectedIndex > 0) Imagen_Textura_Derecha.RotateFlip((RotateFlipType)ComboBox_Textura_Derecha_Rotación.SelectedIndex);
                    if (ComboBox_Textura_Abajo_Rotación.SelectedIndex > 0) Imagen_Textura_Abajo.RotateFlip((RotateFlipType)ComboBox_Textura_Abajo_Rotación.SelectedIndex);
                    if (ComboBox_Textura_Arriba_Rotación.SelectedIndex > 0) Imagen_Textura_Arriba.RotateFlip((RotateFlipType)ComboBox_Textura_Arriba_Rotación.SelectedIndex);
                    if (ComboBox_Textura_Atrás_Rotación.SelectedIndex > 0) Imagen_Textura_Atrás.RotateFlip((RotateFlipType)ComboBox_Textura_Atrás_Rotación.SelectedIndex);
                    if (ComboBox_Textura_Delante_Rotación.SelectedIndex > 0) Imagen_Textura_Delante.RotateFlip((RotateFlipType)ComboBox_Textura_Delante_Rotación.SelectedIndex);

                    Ancho = Mínimo_Ancho_Alto * 3;
                    Alto = Mínimo_Ancho_Alto * 2;
                    Bitmap Imagen = new Bitmap(Ancho, Alto, (!Image.IsAlphaPixelFormat(Imagen_Textura_Izquierda.PixelFormat) && !Image.IsAlphaPixelFormat(Imagen_Textura_Derecha.PixelFormat) && !Image.IsAlphaPixelFormat(Imagen_Textura_Abajo.PixelFormat) && !Image.IsAlphaPixelFormat(Imagen_Textura_Arriba.PixelFormat) && !Image.IsAlphaPixelFormat(Imagen_Textura_Atrás.PixelFormat) && !Image.IsAlphaPixelFormat(Imagen_Textura_Delante.PixelFormat)) ? PixelFormat.Format24bppRgb : PixelFormat.Format32bppArgb);
                    Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.HighQuality;
                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                    bool Invertir_X = Menú_Contextual_Invertir_X.Checked;
                    bool Invertir_Z = Menú_Contextual_Invertir_Z.Checked;
                    Pintar.DrawImage(Imagen_Textura_Izquierda, new Rectangle((!Invertir_X ? 0 : 2) * Mínimo_Ancho_Alto, 1 * Mínimo_Ancho_Alto, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), new Rectangle(0, 0, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Imagen_Textura_Derecha, new Rectangle((!Invertir_X ? 2 : 0) * Mínimo_Ancho_Alto, 1 * Mínimo_Ancho_Alto, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), new Rectangle(0, 0, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Imagen_Textura_Abajo, new Rectangle(0 * Mínimo_Ancho_Alto, 0 * Mínimo_Ancho_Alto, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), new Rectangle(0, 0, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Imagen_Textura_Arriba, new Rectangle(1 * Mínimo_Ancho_Alto, 0 * Mínimo_Ancho_Alto, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), new Rectangle(0, 0, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Imagen_Textura_Atrás, new Rectangle((!Invertir_Z ? 1 : 2) * Mínimo_Ancho_Alto, (!Invertir_Z ? 1 : 0) * Mínimo_Ancho_Alto, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), new Rectangle(0, 0, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Imagen_Textura_Delante, new Rectangle((!Invertir_Z ? 2 : 1) * Mínimo_Ancho_Alto, (!Invertir_Z ? 0 : 1) * Mínimo_Ancho_Alto, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), new Rectangle(0, 0, Mínimo_Ancho_Alto, Mínimo_Ancho_Alto), GraphicsUnit.Pixel);
                    Pintar.Dispose();
                    Pintar = null;

                    int Formato_Pack = ComboBox_Formato_Pack.SelectedIndex + 1;
                    string Ruta_ZIP = Program.Obtener_Ruta_Temporal_Escritorio() + " Sky pack [" + (Formato_Pack == 1 ? "1.6+" : Formato_Pack == 2 ? "1.9+" : Formato_Pack == 3 ? "1.11+" : "1.13+") + "].zip";
                    FileStream Lector = new FileStream(Ruta_ZIP, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector.SetLength(0L);
                    Lector.Seek(0L, SeekOrigin.Begin);
                    ZipOutputStream Archivo_ZIP = new ZipOutputStream(Lector); // Start a new zip file.

                    Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/" + (Formato_Pack < 4 ? "mcpatcher" : "optifine") + "/sky/world0/cloud1.png"));
                    MemoryStream Lector_Memoria = new MemoryStream();
                    Imagen.Save(Lector_Memoria, ImageFormat.Png);
                    byte[] Matriz_Bytes = Lector_Memoria.ToArray();
                    Lector_Memoria.Close();
                    Lector_Memoria.Dispose();
                    Lector_Memoria = null;
                    Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Archivo_ZIP.CloseEntry();
                    Matriz_Bytes = null;

                    Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/" + (Formato_Pack < 4 ? "mcpatcher" : "optifine") + "/sky/world0/sky1.properties"));
                    Matriz_Bytes = Encoding.UTF8.GetBytes("startFadeIn=18:00\r\nendFadeIn=18:45\r\nstartFadeOut=18:50\r\nendFadeOut=19:10\r\nblend=add\r\nrotate=true\r\naxis=0.0 -0.2 0.0\r\nsource=./cloud2.png");
                    Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Archivo_ZIP.CloseEntry();
                    Matriz_Bytes = null;

                    Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/" + (Formato_Pack < 4 ? "mcpatcher" : "optifine") + "/sky/world0/sky2.properties"));
                    Matriz_Bytes = Encoding.UTF8.GetBytes("startFadeIn=4:45\r\nendFadeIn=5:10\r\nstartFadeOut=5:20\r\nendFadeOut=6:05\r\nblend=add\r\nrotate=true\r\naxis=0.0 -0.2 0.0\r\nsource=./cloud2.png");
                    Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Archivo_ZIP.CloseEntry();
                    Matriz_Bytes = null;

                    Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/" + (Formato_Pack < 4 ? "mcpatcher" : "optifine") + "/sky/world0/sky3.properties"));
                    Matriz_Bytes = Encoding.UTF8.GetBytes("startFadeIn=5:30\r\nendFadeIn=6:00\r\nstartFadeOut=17:50\r\nendFadeOut=18:40\r\nblend=screen\r\nrotate=true\r\naxis=0.0 -0.2 0.0\r\nsource=./cloud1.png");
                    Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Archivo_ZIP.CloseEntry();
                    Matriz_Bytes = null;

                    Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/" + (Formato_Pack < 4 ? "mcpatcher" : "optifine") + "/sky/world0/sky4.properties"));
                    Matriz_Bytes = Encoding.UTF8.GetBytes("startFadeIn=17:30\r\nendFadeIn=20:00\r\nendFadeOut=6:10\r\nblend=add\r\nrotate=true\r\nsource=./starfield01.png\r\n");
                    Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Archivo_ZIP.CloseEntry();
                    Matriz_Bytes = null;

                    Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/" + (Formato_Pack < 4 ? "mcpatcher" : "optifine") + "/sky/world0/sky5.properties"));
                    Matriz_Bytes = Encoding.UTF8.GetBytes("startFadeIn=19:30\r\nendFadeIn=19:50\r\nendFadeOut=4:40\r\nblend=burn\r\nrotate=true\r\naxis=0.0 -0.2 0.0\r\nsource=./starfield02.png");
                    Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Archivo_ZIP.CloseEntry();
                    Matriz_Bytes = null;

                    Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/" + (Formato_Pack < 4 ? "mcpatcher" : "optifine") + "/sky/world0/sky6.properties"));
                    Matriz_Bytes = Encoding.UTF8.GetBytes("startFadeIn=18:30\r\nendFadeIn=18:45\r\nendFadeOut=5:25\r\nblend=add\r\nrotate=true\r\naxis=0.0 -0.2 0.0\r\nsource=./starfield03.png\r\n");
                    Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Archivo_ZIP.CloseEntry();
                    Matriz_Bytes = null;

                    Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/" + (Formato_Pack < 4 ? "mcpatcher" : "optifine") + "/sky/world0/sky7.properties"));
                    Matriz_Bytes = Encoding.UTF8.GetBytes("startFadeIn=17:50\r\nendFadeIn=18:30\r\nendFadeOut=19:20\r\nblend=add\r\nrotate=true\r\nsource=./sky_sunflare.png\r\n");
                    Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Archivo_ZIP.CloseEntry();
                    Matriz_Bytes = null;

                    Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/" + (Formato_Pack < 4 ? "mcpatcher" : "optifine") + "/sky/world0/sky8.properties"));
                    Matriz_Bytes = Encoding.UTF8.GetBytes("startFadeIn=4:40\r\nendFadeIn=5:00\r\nendFadeOut=5:50\r\nblend=add\r\nrotate=true\r\nsource=./sky_sunflare.png\r\n");
                    Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Archivo_ZIP.CloseEntry();
                    Matriz_Bytes = null;

                    Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/" + (Formato_Pack < 4 ? "mcpatcher" : "optifine") + "/sky/world0/cloud2.png"));
                    Bitmap Imagen_Cloud_2 = (Bitmap)Imagen.Clone();
                    BitmapData Bitmap_Data = Imagen_Cloud_2.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen_Cloud_2.PixelFormat);
                    Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                    int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen_Cloud_2.PixelFormat) ? 4 : 3;
                    int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen_Cloud_2.PixelFormat)) / 8);
                    int Rojo, Verde, Azul;
                    for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                    {
                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                        {
                            Rojo = Matriz_Bytes[Índice + 2] / 2;
                            Verde = Matriz_Bytes[Índice + 1] / 4;
                            Azul = Matriz_Bytes[Índice] / 4;
                            if (Rojo < 0) Rojo = 0;
                            else if (Rojo > 255) Rojo = 255;
                            if (Verde < 0) Verde = 0;
                            else if (Verde > 255) Verde = 255;
                            if (Azul < 0) Azul = 0;
                            else if (Azul > 255) Azul = 255;
                            Matriz_Bytes[Índice + 2] = (byte)Rojo;
                            Matriz_Bytes[Índice + 1] = (byte)Verde;
                            Matriz_Bytes[Índice] = (byte)Azul;

                            //Matriz_Bytes[Índice + 2] = Program.Matriz_Bytes_Filtro_Raíz_Cuadrada_Menos[Matriz_Bytes[Índice + 2]];
                            //Matriz_Bytes[Índice + 1] = Program.Matriz_Bytes_Filtro_Raíz_Cuadrada_Menos[Matriz_Bytes[Índice + 1]];
                            //Matriz_Bytes[Índice] = Program.Matriz_Bytes_Filtro_Raíz_Cuadrada_Menos[Matriz_Bytes[Índice]];
                        }
                    }
                    Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                    Imagen_Cloud_2.UnlockBits(Bitmap_Data);
                    Matriz_Bytes = null;
                    Lector_Memoria = new MemoryStream();
                    Imagen_Cloud_2.Save(Lector_Memoria, ImageFormat.Png);
                    Matriz_Bytes = Lector_Memoria.ToArray();
                    Lector_Memoria.Close();
                    Lector_Memoria.Dispose();
                    Lector_Memoria = null;
                    Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Archivo_ZIP.CloseEntry();
                    Matriz_Bytes = null;
                    Imagen_Cloud_2.Dispose();
                    Imagen_Cloud_2 = null;

                    Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/" + (Formato_Pack < 4 ? "mcpatcher" : "optifine") + "/sky/world0/starfield02.png"));
                    Bitmap Imagen_Starfield_02 = (Bitmap)Imagen.Clone();
                    Bitmap_Data = Imagen_Starfield_02.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen_Starfield_02.PixelFormat);
                    Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen_Starfield_02.PixelFormat) ? 4 : 3;
                    Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen_Starfield_02.PixelFormat)) / 8);
                    for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                    {
                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                        {
                            Rojo = Matriz_Bytes[Índice + 2] / 4; // 3;
                            Verde = Matriz_Bytes[Índice + 1] / 8;
                            Azul = Matriz_Bytes[Índice] / 8;
                            if (Rojo < 0) Rojo = 0;
                            else if (Rojo > 255) Rojo = 255;
                            if (Verde < 0) Verde = 0;
                            else if (Verde > 255) Verde = 255;
                            if (Azul < 0) Azul = 0;
                            else if (Azul > 255) Azul = 255;
                            Matriz_Bytes[Índice + 2] = (byte)Rojo;
                            Matriz_Bytes[Índice + 1] = (byte)Verde;
                            Matriz_Bytes[Índice] = (byte)Azul;

                            //Matriz_Bytes[Índice + 2] = Program.Matriz_Bytes_Filtro_Raíz_Cuadrada_Menos[Matriz_Bytes[Índice + 2]];
                            //Matriz_Bytes[Índice + 1] = Program.Matriz_Bytes_Filtro_Raíz_Cuadrada_Menos[Matriz_Bytes[Índice + 1]];
                            //Matriz_Bytes[Índice] = Program.Matriz_Bytes_Filtro_Raíz_Cuadrada_Menos[Matriz_Bytes[Índice]];
                        }
                    }
                    Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                    Imagen_Starfield_02.UnlockBits(Bitmap_Data);
                    Matriz_Bytes = null;
                    Lector_Memoria = new MemoryStream();
                    Imagen_Starfield_02.Save(Lector_Memoria, ImageFormat.Png);
                    Matriz_Bytes = Lector_Memoria.ToArray();
                    Lector_Memoria.Close();
                    Lector_Memoria.Dispose();
                    Lector_Memoria = null;
                    Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Archivo_ZIP.CloseEntry();
                    Matriz_Bytes = null;
                    Imagen_Starfield_02.Dispose();
                    Imagen_Starfield_02 = null;

                    Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/" + (Formato_Pack < 4 ? "mcpatcher" : "optifine") + "/sky/world0/starfield03.png"));
                    Bitmap Imagen_Starfield_03 = (Bitmap)Imagen.Clone();
                    Bitmap_Data = Imagen_Starfield_03.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen_Starfield_03.PixelFormat);
                    Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen_Starfield_03.PixelFormat) ? 4 : 3;
                    Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen_Starfield_03.PixelFormat)) / 8);
                    for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                    {
                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                        {
                            Rojo = Matriz_Bytes[Índice + 2] / 8;
                            Verde = Matriz_Bytes[Índice + 1] / 8;
                            Azul = Matriz_Bytes[Índice] / 4;
                            if (Rojo < 0) Rojo = 0;
                            else if (Rojo > 255) Rojo = 255;
                            if (Verde < 0) Verde = 0;
                            else if (Verde > 255) Verde = 255;
                            if (Azul < 0) Azul = 0;
                            else if (Azul > 255) Azul = 255;
                            Matriz_Bytes[Índice + 2] = (byte)Rojo;
                            Matriz_Bytes[Índice + 1] = (byte)Verde;
                            Matriz_Bytes[Índice] = (byte)Azul;

                            //Matriz_Bytes[Índice + 2] = Program.Matriz_Bytes_Filtro_Raíz_Cuadrada_Menos[Matriz_Bytes[Índice + 2]];
                            //Matriz_Bytes[Índice + 1] = Program.Matriz_Bytes_Filtro_Raíz_Cuadrada_Menos[Matriz_Bytes[Índice + 1]];
                            //Matriz_Bytes[Índice] = Program.Matriz_Bytes_Filtro_Raíz_Cuadrada_Menos[Matriz_Bytes[Índice]];
                        }
                    }
                    Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                    Imagen_Starfield_03.UnlockBits(Bitmap_Data);
                    Matriz_Bytes = null;
                    Lector_Memoria = new MemoryStream();
                    Imagen_Starfield_03.Save(Lector_Memoria, ImageFormat.Png);
                    Matriz_Bytes = Lector_Memoria.ToArray();
                    Lector_Memoria.Close();
                    Lector_Memoria.Dispose();
                    Lector_Memoria = null;
                    Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Archivo_ZIP.CloseEntry();
                    Matriz_Bytes = null;
                    Imagen_Starfield_03.Dispose();
                    Imagen_Starfield_03 = null;

                    Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/" + (Formato_Pack < 4 ? "mcpatcher" : "optifine") + "/sky/world0/starfield01.png"));
                    Lector_Memoria = new MemoryStream();
                    Resources.starfield01.Save(Lector_Memoria, ImageFormat.Png);
                    Matriz_Bytes = Lector_Memoria.ToArray();
                    Lector_Memoria.Close();
                    Lector_Memoria.Dispose();
                    Lector_Memoria = null;
                    Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Archivo_ZIP.CloseEntry();
                    Matriz_Bytes = null;

                    if (CheckBox_Incluir_Entorno.Checked)
                    {
                        Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/" + (Formato_Pack < 4 ? "mcpatcher" : "optifine") + "/sky/world0/sky_sunflare.png"));
                        Lector_Memoria = new MemoryStream();
                        Resources.sky_sunflare.Save(Lector_Memoria, ImageFormat.Png);
                        Matriz_Bytes = Lector_Memoria.ToArray();
                        Lector_Memoria.Close();
                        Lector_Memoria.Dispose();
                        Lector_Memoria = null;
                        Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                        Archivo_ZIP.CloseEntry();
                        Matriz_Bytes = null;

                        Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/textures/environment/moon_phases.png"));
                        Lector_Memoria = new MemoryStream();
                        Resources.moon_phases.Save(Lector_Memoria, ImageFormat.Png);
                        Matriz_Bytes = Lector_Memoria.ToArray();
                        Lector_Memoria.Close();
                        Lector_Memoria.Dispose();
                        Lector_Memoria = null;
                        Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                        Archivo_ZIP.CloseEntry();
                        Matriz_Bytes = null;

                        Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/textures/environment/sun.png"));
                        Lector_Memoria = new MemoryStream();
                        Resources.sun.Save(Lector_Memoria, ImageFormat.Png);
                        Matriz_Bytes = Lector_Memoria.ToArray();
                        Lector_Memoria.Close();
                        Lector_Memoria.Dispose();
                        Lector_Memoria = null;
                        Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                        Archivo_ZIP.CloseEntry();
                        Matriz_Bytes = null;
                    }

                    Archivo_ZIP.PutNextEntry(new ZipEntry("pack.mcmeta"));
                    Matriz_Bytes = Encoding.UTF8.GetBytes("{\r\n  \"pack\": {\r\n    \"pack_format\": " + Formato_Pack.ToString() + ",\r\n    \"description\": \"§f" + Program.Texto_Usuario + "'s Sky§r for §fJava Ed.\\n§6Author:§r §c" + Program.Texto_Usuario + "\"\r\n  }\r\n}\r\n");
                    Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Archivo_ZIP.CloseEntry();
                    Matriz_Bytes = null;

                    Archivo_ZIP.PutNextEntry(new ZipEntry("pack.png"));
                    Lector_Memoria = new MemoryStream();
                    Program.Obtener_Imagen_Miniatura(Imagen_Textura_Delante, 256, 256, false, true, CheckState.Indeterminate).Save(Lector_Memoria, ImageFormat.Png);
                    Matriz_Bytes = Lector_Memoria.ToArray();
                    Lector_Memoria.Close();
                    Lector_Memoria.Dispose();
                    Lector_Memoria = null;
                    Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Archivo_ZIP.CloseEntry();
                    Matriz_Bytes = null;

                    Archivo_ZIP.Finish();
                    Archivo_ZIP.Close();
                    Archivo_ZIP.Dispose();
                    Archivo_ZIP = null;
                    Imagen.Dispose();
                    Imagen = null;
                    Imagen_Textura_Izquierda.Dispose();
                    Imagen_Textura_Izquierda = null;
                    Imagen_Textura_Derecha.Dispose();
                    Imagen_Textura_Derecha = null;
                    Imagen_Textura_Abajo.Dispose();
                    Imagen_Textura_Abajo = null;
                    Imagen_Textura_Arriba.Dispose();
                    Imagen_Textura_Arriba = null;
                    Imagen_Textura_Atrás.Dispose();
                    Imagen_Textura_Atrás = null;
                    Imagen_Textura_Delante.Dispose();
                    Imagen_Textura_Delante = null;
                    SystemSounds.Asterisk.Play();
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Botón_Exportar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (Variable_Imagen_Textura_Exportar != null)
                {
                    int Ancho_Original = Variable_Imagen_Textura_Exportar.Width;
                    int Alto_Original = Variable_Imagen_Textura_Exportar.Height;
                    int Texturas_X = (int)NumericUpDown_Exportar_X.Value;
                    int Texturas_Y = (int)NumericUpDown_Exportar_Y.Value;
                    int Separador_Exterior = (int)NumericUpDown_Exportar_Bordes_Exteriores.Value;
                    int Separador_Interior = (int)NumericUpDown_Exportar_Bordes_Interiores.Value;
                    int Ancho = ((Ancho_Original - (Separador_Exterior * 2)) - (Separador_Interior * (Texturas_X - 1))) / Texturas_X;
                    int Alto = ((Alto_Original - (Separador_Exterior * 2)) - (Separador_Interior * (Texturas_Y - 1))) / Texturas_Y;
                    if (Ancho < 1) Ancho = 1;
                    if (Alto < 1) Alto = 1;

                    string Ruta = Program.Obtener_Ruta_Temporal_Escritorio();
                    Program.Crear_Carpetas(Ruta);
                    for (int Índice_Y = 0, Y = Separador_Exterior, Índice = 1; Índice_Y < Texturas_Y; Índice_Y++, Y += Alto)
                    {
                        for (int Índice_X = 0, X = Separador_Exterior; Índice_X < Texturas_X; Índice_X++, X += Ancho, Índice++)
                        {
                            Bitmap Imagen = null;
                            try { Imagen = Variable_Imagen_Textura_Exportar.Clone(new Rectangle(X, Y, Ancho, Alto), Variable_Imagen_Textura_Exportar.PixelFormat); }
                            catch { Imagen = null; }
                            if (Imagen != null)
                            {
                                Imagen.Save(Ruta + "\\" + Índice.ToString() + ".png", ImageFormat.Png);
                                Imagen.Dispose();
                                Imagen = null;
                            }
                            X += Separador_Interior;
                        }
                        Y += Separador_Interior;
                    }
                    Program.Ejecutar_Ruta(Ruta, ProcessWindowStyle.Maximized);
                    Ruta = null;
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
