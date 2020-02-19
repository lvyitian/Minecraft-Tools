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
    public partial class Ventana_Salvapantallas_Bloques : Form
    {
        public Ventana_Salvapantallas_Bloques()
        {
            InitializeComponent();
        }

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr Handle_Objeto);

        internal static CheckState Argumento_Salvapantallas = CheckState.Unchecked;

        internal readonly string Texto_Título = "Blocks Screen Saver by Jupisoft for " + Program.Texto_Usuario;
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
        internal Point Posición_Cursor = Control.MousePosition;

        internal Size Dimensiones_Cliente;
        internal Graphics Pintar = null;

        internal static bool Variable_Bloques_3D = true;

        internal static int Variable_Rotación = -1;
        internal static int Variable_Dimensiones = -1;
        internal static int Variable_Velocidad = 1;

        internal static bool Variable_Escala_Grises = false;
        internal static bool Variable_Interpolación = false;
        internal static bool Variable_Transparencia = true;

        internal static bool Variable_Cuadrícula = false;
        internal static bool Variable_Cursor_Bloques = false;
        internal static bool Variable_Pantalla_Completa = false;
        internal static bool Variable_Barra_Estado = true;

        private void Ventana_Salvapantallas_Bloques_Load(object sender, EventArgs e)
        {
            try
            {
                if (Program.Icono_Jupisoft == null) Program.Icono_Jupisoft = this.Icon.Clone() as Icon;
                Picture.MouseDown += Ventana_Salvapantallas_Bloques_MouseDown;
                Picture.MouseMove += Ventana_Salvapantallas_Bloques_MouseMove;
                this.MouseWheel += Ventana_Salvapantallas_Bloques_MouseWheel;
                Picture.MouseWheel += Ventana_Salvapantallas_Bloques_MouseWheel;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
                if (Argumento_Salvapantallas != CheckState.Unchecked)
                {
                    Menú_Contextual_Instalar.Enabled = false;
                    Menú_Contextual_Pantalla_Completa.Checked = true;
                    Menú_Contextual.Enabled = false;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Salvapantallas_Bloques_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Reiniciar_Picture();
                //if (Argumento_Salvapantallas == CheckState.Indeterminate) Menú_Contextual.Show(new Point(0, 0));
                Temporizador_Principal_Tick(Temporizador_Principal, EventArgs.Empty);
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Salvapantallas_Bloques_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
                if (Variable_Pantalla_Completa) Cursor.Show();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Salvapantallas_Bloques_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Salvapantallas_Bloques_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                Reiniciar_Picture();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Salvapantallas_Bloques_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Argumento_Salvapantallas == CheckState.Unchecked)
                {
                    if (!e.Alt && !e.Control && !e.Shift)
                    {
                        if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Delete || e.KeyCode == Keys.Space)
                        {
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            this.Close();
                        }
                        else if (e.KeyCode == Keys.Back)
                        {
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            Menú_Contextual_Actualizar.PerformClick();
                        }
                        else if (e.KeyCode == Keys.Enter)
                        {
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            Menú_Contextual_Pantalla_Completa.PerformClick();
                        }
                    }
                }
                else this.Close();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Salvapantallas_Bloques_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.None)
                {
                    if (Argumento_Salvapantallas != CheckState.Unchecked) this.Close();
                    else if (e.Button == MouseButtons.Middle)
                    {
                        Menú_Contextual_Pantalla_Completa.PerformClick();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Salvapantallas_Bloques_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (Argumento_Salvapantallas != CheckState.Unchecked)
                {
                    Point Posición = Control.MousePosition;
                    if (Math.Abs(Posición.X - Posición_Cursor.X) > 16 || Math.Abs(Posición.Y - Posición_Cursor.Y) > 16)
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Salvapantallas_Bloques_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta != 0) this.Close();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Registro_Cargar_Opciones()
        {
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Blocks screen saver");

                try { Variable_Bloques_3D = bool.Parse((string)Clave.GetValue("Blocks_3D", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Bloques_3D = true; }

                try { Variable_Rotación = (int)Clave.GetValue("Rotation", -1); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Rotación = -1; }

                try { Variable_Dimensiones = (int)Clave.GetValue("Size", -1); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Dimensiones = -1; }

                try { Variable_Velocidad = (int)Clave.GetValue("Speed", 1); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Velocidad = 1; }

                try { Variable_Escala_Grises = bool.Parse((string)Clave.GetValue("Gray_Scale", bool.FalseString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Escala_Grises = false; }

                try { Variable_Interpolación = bool.Parse((string)Clave.GetValue("Interpolation", bool.FalseString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Interpolación = false; }

                try { Variable_Transparencia = bool.Parse((string)Clave.GetValue("Transparency", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Transparencia = true; }

                try { Variable_Cuadrícula = bool.Parse((string)Clave.GetValue("Virtual_Grid", bool.FalseString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Cuadrícula = false; }

                try { Variable_Cursor_Bloques = bool.Parse((string)Clave.GetValue("Cursor_Blocks", bool.FalseString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Cursor_Bloques = false; }

                try { Variable_Pantalla_Completa = bool.Parse((string)Clave.GetValue("Full_Screen", bool.FalseString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Pantalla_Completa = false; }

                try { Variable_Barra_Estado = bool.Parse((string)Clave.GetValue("Status_Bar", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_Barra_Estado = true; }
                
                // Correct any bad value after loading:
                if (Variable_Rotación != -1 && Variable_Rotación != 0 && Variable_Rotación != 90 && Variable_Rotación != 180 && Variable_Rotación != 270) Variable_Rotación = -1;
                if (Variable_Dimensiones != -1 && Variable_Dimensiones != 1 && Variable_Dimensiones != 2 && Variable_Dimensiones != 3 && Variable_Dimensiones != 4 && Variable_Dimensiones != 5 && Variable_Dimensiones != 6 && Variable_Dimensiones != 7 && Variable_Dimensiones != 8 && Variable_Dimensiones != 9 && Variable_Dimensiones != 10 && Variable_Dimensiones != 11 && Variable_Dimensiones != 12 && Variable_Dimensiones != 13 && Variable_Dimensiones != 14 && Variable_Dimensiones != 15 && Variable_Dimensiones != 16) Variable_Dimensiones = -1;
                if (Variable_Velocidad != 0 && Variable_Velocidad != 1 && Variable_Velocidad != 16 && Variable_Velocidad != 17 && Variable_Velocidad != 32 && Variable_Velocidad != 47 && Variable_Velocidad != 48 && Variable_Velocidad != 78 && Variable_Velocidad != 94 && Variable_Velocidad != 110 && Variable_Velocidad != 188 && Variable_Velocidad != 235 && Variable_Velocidad != 316 && Variable_Velocidad != 485 && Variable_Velocidad != 970 && Variable_Velocidad != 2000 && Variable_Velocidad != 4000) Variable_Velocidad = 1;

                // Apply all the loaded values:
                Menú_Contextual_Bloques_3D.Checked = Variable_Bloques_3D;
                Menú_Contextual_Rotación.DropDownItems["Menú_Contextual_Rotación_" + Variable_Rotación.ToString().Replace("-", "_")].PerformClick();
                Menú_Contextual_Dimensiones.DropDownItems["Menú_Contextual_Dimensiones_" + Variable_Dimensiones.ToString().Replace("-", "_")].PerformClick();
                Menú_Contextual_Velocidad.DropDownItems["Menú_Contextual_Velocidad_" + Variable_Velocidad.ToString().Replace("-", "_")].PerformClick();

                Menú_Contextual_Escala_Grises.Checked = Variable_Escala_Grises;
                Menú_Contextual_Interpolación.Checked = Variable_Interpolación;
                Menú_Contextual_Transparencia.Checked = Variable_Transparencia;
                Menú_Contextual_Cuadrícula.Checked = Variable_Cuadrícula;
                Menú_Contextual_Cursor_Bloques.Checked = Variable_Cursor_Bloques;
                Menú_Contextual_Pantalla_Completa.Checked = Variable_Pantalla_Completa;
                Menú_Contextual_Barra_Estado.Checked = Variable_Barra_Estado;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Registro_Guardar_Opciones()
        {
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Blocks screen saver");
                string[] Matriz_Nombres = Clave.GetValueNames();
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                    {
                        Clave.DeleteValue(Matriz_Nombres[Índice]);
                    }
                }
                Matriz_Nombres = null;

                try { Clave.SetValue("Blocks_3D", Variable_Bloques_3D.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                try { Clave.SetValue("Rotation", Variable_Rotación, RegistryValueKind.DWord); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                try { Clave.SetValue("Size", Variable_Dimensiones, RegistryValueKind.DWord); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                try { Clave.SetValue("Speed", Variable_Velocidad, RegistryValueKind.DWord); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                try { Clave.SetValue("Gray_Scale", Variable_Escala_Grises.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                try { Clave.SetValue("Interpolation", Variable_Interpolación.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                try { Clave.SetValue("Transparency", Variable_Transparencia.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                try { Clave.SetValue("Virtual_Grid", Variable_Cuadrícula.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                try { Clave.SetValue("Cursor_Blocks", Variable_Cursor_Bloques.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                try { Clave.SetValue("Full_Screen", Variable_Pantalla_Completa.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                try { Clave.SetValue("Status_Bar", Variable_Barra_Estado.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Registro_Restablecer_Opciones()
        {
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Blocks screen saver");
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

        private void Menú_Contextual_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                if (Variable_Pantalla_Completa) Cursor.Show();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            try
            {
                if (Variable_Pantalla_Completa) Cursor.Hide();
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

        private void Menú_Contextual_Siempre_Visible_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Siempre_Visible = Menú_Contextual_Siempre_Visible.Checked;
                this.TopMost = Variable_Siempre_Visible;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Reiniciar_Picture();
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
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Salvapantallas_Bloques);
                    Picture.Image.Save(Program.Ruta_Guardado_Imágenes_Salvapantallas_Bloques + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + ".png", ImageFormat.Png);
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
                catch (Exception Excepción)
                {
                    Temporizador_Principal.Stop();
                    Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                }
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
                catch (Exception Excepción)
                {
                    Temporizador_Principal.Stop();
                    Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                }
                try
                {
                    string Recurso = Minecraft.Bloques.Matriz_Bloques[Program.Rand.Next(0, Minecraft.Bloques.Matriz_Bloques.Length)].Recurso;
                    Bitmap Imagen_Recurso = Program.Obtener_Imagen_Recursos(Recurso);
                    if (Imagen_Recurso != null)
                    {
                        int Ancho = Imagen_Recurso.Width;
                        int Alto = Imagen_Recurso.Height;
                        if (Ancho != 16 || Alto != 16)
                        {
                            Imagen_Recurso = Program.Obtener_Imagen_Miniatura(Imagen_Recurso, 16, 16, true, false, CheckState.Checked);
                            //MessageBox.Show(Recurso); // This should never pop-up.
                        }

                        if (Variable_Cursor_Bloques) // Create a cursor made of Minecraft blocks for fun.
                        {
                            if (!Variable_Pantalla_Completa) // Ignore it at full screen even with the cursor shown.
                            {
                                Bitmap Imagen_Cursor = Program.Obtener_Imagen_Miniatura(Imagen_Recurso, 32, 32, true, false, CheckState.Checked);
                                IntPtr Handle_Icono = Imagen_Cursor.GetHicon();
                                Cursor Cursor = new Cursor(Handle_Icono);
                                this.Cursor = Cursor;
                                //DeleteObject(Handle_Icono); // Why .NET doesn't seem to close the GDI+ handle?
                                // I believe that's what make it crash eventually, but I'm not sure how to fix it.
                                Cursor.Dispose();
                                Cursor = null;
                                Imagen_Cursor.Dispose();
                                Imagen_Cursor = null;
                            }
                        }

                        if (Variable_Escala_Grises)
                        {
                            Imagen_Recurso = Program.Obtener_Imagen_Desaturada(Imagen_Recurso, true);
                        }
                        double Zoom = Variable_Dimensiones != -1 ? Variable_Dimensiones : (1d + (Program.Rand.NextDouble() * (!Variable_Bloques_3D ? 7d : 11d)));
                        if (Variable_Velocidad == 0)
                        {
                            Temporizador_Principal.Interval = Math.Max((int)(Zoom * 15d), 1);
                        }
                        double Ancho_Zoom = (double)Ancho * Zoom;
                        double Alto_Zoom = (double)Alto * Zoom;
                        double Ancho_Cliente_Centro = (double)Dimensiones_Cliente.Width / 2d;
                        double Alto_Cliente_Centro = (double)Dimensiones_Cliente.Height / 2d;
                        double X = Program.Rand.Next(0, Dimensiones_Cliente.Width);
                        double Y = Program.Rand.Next(0, Dimensiones_Cliente.Height);
                        if (Variable_Cuadrícula)
                        {
                            X -= X % Ancho_Zoom;
                            Y -= Y % Alto_Zoom;
                            if (X < 0) X += Ancho_Zoom;
                            if (Y < 0) Y += Alto_Zoom;
                        }
                        RectangleF Rectángulo_Zoom = new RectangleF((float)(X - Ancho_Cliente_Centro), (float)(Y - Alto_Cliente_Centro), (float)Ancho_Zoom, (float)Alto_Zoom);
                        Pintar.TranslateTransform((float)Ancho_Cliente_Centro, (float)Alto_Cliente_Centro);
                        if (Variable_Rotación != 0)
                        {
                            double Rotación = Variable_Rotación != -1 ? Variable_Rotación : Program.Rand.NextDouble() * 360d;
                            Pintar.RotateTransform((float)Rotación);
                            if (Variable_Rotación == 90 || Variable_Rotación == 270)
                            {
                                float Valor = Rectángulo_Zoom.X;
                                Rectángulo_Zoom.X = Rectángulo_Zoom.Y;
                                Rectángulo_Zoom.Y = Valor;
                            }
                        }
                        if (!Variable_Bloques_3D) Pintar.DrawImage(Imagen_Recurso, Rectángulo_Zoom, new RectangleF(0f, 0f, (float)Ancho, (float)Alto), GraphicsUnit.Pixel);
                        else
                        {
                            int Ancho_Alto = (int)Math.Max(Ancho_Zoom, Alto_Zoom);
                            Pintar.DrawImage(Program.Obtener_Imagen_Bloque_3D(Ancho_Alto, 0d, Imagen_Recurso, !Variable_Interpolación ? CheckState.Unchecked : CheckState.Indeterminate), new RectangleF(Rectángulo_Zoom.X, Rectángulo_Zoom.Y, (float)Ancho_Alto, (float)Ancho_Alto), new RectangleF(0f, 0f, (float)Ancho_Alto, (float)Ancho_Alto), GraphicsUnit.Pixel);
                        }
                        Pintar.ResetTransform();
                        Picture.Invalidate();
                        Picture.Update();
                    }
                }
                catch (Exception Excepción)
                {
                    Temporizador_Principal.Stop();
                    Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                }
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
            catch (Exception Excepción)
            {
                Temporizador_Principal.Stop();
                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
            }
        }

        private void Menú_Contextual_Interpolación_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Interpolación = Menú_Contextual_Interpolación.Checked;
                Reiniciar_Graphics();
                Registro_Guardar_Opciones();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Transparencia_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Transparencia = Menú_Contextual_Transparencia.Checked;
                Reiniciar_Graphics();
                Registro_Guardar_Opciones();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Pantalla_Completa_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Pantalla_Completa = Menú_Contextual_Pantalla_Completa.Checked;
                Menú_Contextual_Siempre_Visible.Enabled = !Variable_Pantalla_Completa;
                Menú_Contextual_Barra_Estado.Enabled = !Variable_Pantalla_Completa;
                Barra_Estado.Visible = !Variable_Pantalla_Completa ? Variable_Barra_Estado : false;
                if (!Variable_Pantalla_Completa)
                {
                    this.TopMost = Variable_Siempre_Visible;
                    this.WindowState = FormWindowState.Normal;
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.WindowState = FormWindowState.Maximized;
                    Cursor.Show();
                }
                else
                {
                    Cursor.Hide();
                    this.WindowState = FormWindowState.Normal;
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                    this.TopMost = true;
                }
                Registro_Guardar_Opciones();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Escala_Grises_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Escala_Grises = Menú_Contextual_Escala_Grises.Checked;
                Reiniciar_Graphics();
                Registro_Guardar_Opciones();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Reiniciar_Graphics()
        {
            try
            {
                Temporizador_Principal.Stop();
                if (Pintar != null)
                {
                    Pintar.Dispose();
                    Pintar = null;
                }
                Pintar = Graphics.FromImage(Picture.Image);
                Pintar.CompositingMode = !Variable_Transparencia ? CompositingMode.SourceCopy : CompositingMode.SourceOver;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = !Variable_Interpolación ? InterpolationMode.NearestNeighbor : InterpolationMode.HighQualityBicubic;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.HighQuality;
                Pintar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                Temporizador_Principal_Tick(Temporizador_Principal, EventArgs.Empty);
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Reiniciar_Picture()
        {
            try
            {
                Temporizador_Principal.Stop();
                Dimensiones_Cliente = Picture.ClientSize;
                Bitmap Imagen = new Bitmap(Dimensiones_Cliente.Width, Dimensiones_Cliente.Height, PixelFormat.Format24bppRgb);
                Picture.Image = Imagen;
                Pintar = Graphics.FromImage(Picture.Image);
                Pintar.CompositingMode = !Variable_Transparencia ? CompositingMode.SourceCopy : CompositingMode.SourceOver;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = !Variable_Interpolación ? InterpolationMode.NearestNeighbor : InterpolationMode.HighQualityBicubic;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.HighQuality;
                Pintar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                Pintar.Clear(Color.Black);
                Temporizador_Principal_Tick(Temporizador_Principal, EventArgs.Empty);
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Instalar_Click(object sender, EventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
                string Ruta_Escritorio = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string Ruta_System32 = Environment.GetFolderPath(Environment.SpecialFolder.System);

                string Ruta_Aplicación = Application.ExecutablePath;
                string Ruta_Magick_NET = Application.StartupPath + "\\Magick.NET-Q16-AnyCPU.dll";
                string Ruta_Seven_Zip_Sharp = Application.StartupPath + "\\SevenZipSharp.dll";

                Program.Quitar_Atributo_Sólo_Lectura(Ruta_Escritorio + "\\Jupisoft - Minecraft Tools.scr");
                Program.Quitar_Atributo_Sólo_Lectura(Ruta_Escritorio + "\\Magick.NET-Q16-AnyCPU.dll");
                Program.Quitar_Atributo_Sólo_Lectura(Ruta_Escritorio + "\\SevenZipSharp.dll");

                File.Copy(Ruta_Aplicación, Ruta_Escritorio + "\\Jupisoft - Minecraft Tools.scr", true);
                File.Copy(Ruta_Magick_NET, Ruta_Escritorio + "\\Magick.NET-Q16-AnyCPU.dll", true);
                File.Copy(Ruta_Seven_Zip_Sharp, Ruta_Escritorio + "\\SevenZipSharp.dll", true);

                try
                {
                    Program.Quitar_Atributo_Sólo_Lectura(Ruta_System32 + "\\Jupisoft - Minecraft Tools.scr");
                    Program.Quitar_Atributo_Sólo_Lectura(Ruta_System32 + "\\Magick.NET-Q16-AnyCPU.dll");
                    Program.Quitar_Atributo_Sólo_Lectura(Ruta_System32 + "\\SevenZipSharp.dll");

                    File.Move(Ruta_Escritorio + "\\Jupisoft - Minecraft Tools.scr", Ruta_System32 + "\\Jupisoft - Minecraft Tools.scr");
                    File.Move(Ruta_Escritorio + "\\Magick.NET-Q16-AnyCPU.dll", Ruta_System32 + "\\Magick.NET-Q16-AnyCPU.dll");
                    File.Move(Ruta_Escritorio + "\\SevenZipSharp.dll", Ruta_System32 + "\\SevenZipSharp.dll");

                    SystemSounds.Asterisk.Play();
                }
                catch (UnauthorizedAccessException Excepción)
                {
                    Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                    MessageBox.Show(this, "The application saved the screen saver and it's 2 libraries to your desktop, but it couldn't move them to your System32 folder due to a lack of privileges. Please move the files \"Jupisoft - Minecraft Tools.scr\", \"Magick.NET-Q16-AnyCPU.dll\" and \"SevenZipSharp.dll\" manually to \"" + Ruta_System32 + "\". Thanks, and sorry for the inconvenience.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                Temporizador_Principal_Tick(Temporizador_Principal, EventArgs.Empty);
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Bloques_3D_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Bloques_3D = Menú_Contextual_Bloques_3D.Checked;
                Registro_Guardar_Opciones();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Rotación_Click(object sender, EventArgs e)
        {
            try
            {
                for (int Índice = 0; Índice < Menú_Contextual_Rotación.DropDownItems.Count; Índice++) if (Menú_Contextual_Rotación.DropDownItems[Índice].GetType() == typeof(ToolStripMenuItem)) ((ToolStripMenuItem)Menú_Contextual_Rotación.DropDownItems[Índice]).Checked = false;
                ToolStripMenuItem Menú = (ToolStripMenuItem)sender;
                Menú.Checked = true;
                Variable_Rotación = int.Parse(Menú.Name.Replace("Menú_Contextual_Rotación_", null).Replace('_', '-'));
                Registro_Guardar_Opciones();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Dimensiones_Click(object sender, EventArgs e)
        {
            try
            {
                for (int Índice = 0; Índice < Menú_Contextual_Dimensiones.DropDownItems.Count; Índice++) if (Menú_Contextual_Dimensiones.DropDownItems[Índice].GetType() == typeof(ToolStripMenuItem)) ((ToolStripMenuItem)Menú_Contextual_Dimensiones.DropDownItems[Índice]).Checked = false;
                ToolStripMenuItem Menú = (ToolStripMenuItem)sender;
                Menú.Checked = true;
                Variable_Dimensiones = int.Parse(Menú.Name.Replace("Menú_Contextual_Dimensiones_", null).Replace('_', '-'));
                Registro_Guardar_Opciones();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Velocidad_Click(object sender, EventArgs e)
        {
            try
            {
                for (int Índice = 0; Índice < Menú_Contextual_Velocidad.DropDownItems.Count; Índice++) if (Menú_Contextual_Velocidad.DropDownItems[Índice].GetType() == typeof(ToolStripMenuItem)) ((ToolStripMenuItem)Menú_Contextual_Velocidad.DropDownItems[Índice]).Checked = false;
                ToolStripMenuItem Menú = (ToolStripMenuItem)sender;
                Menú.Checked = true;
                Variable_Velocidad = int.Parse(Menú.Name.Replace("Menú_Contextual_Velocidad_", null).Replace('_', '-'));
                Temporizador_Principal.Interval = Variable_Velocidad != 0 ? Variable_Velocidad : 1;
                Registro_Guardar_Opciones();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Cuadrícula_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Cuadrícula = Menú_Contextual_Cuadrícula.Checked;
                Reiniciar_Graphics();
                Registro_Guardar_Opciones();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Cursor_Bloques_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
                Variable_Cursor_Bloques = Menú_Contextual_Cursor_Bloques.Checked;
                if (!Variable_Cursor_Bloques) this.Cursor = Cursors.Default;
                Temporizador_Principal_Tick(Temporizador_Principal, EventArgs.Empty);
                Temporizador_Principal.Start();
                Reiniciar_Graphics();
                Registro_Guardar_Opciones();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Barra_Estado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Barra_Estado = Menú_Contextual_Barra_Estado.Checked;
                Barra_Estado.Visible = Variable_Barra_Estado;
                Reiniciar_Picture();
                Registro_Guardar_Opciones();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Restablecer_Click(object sender, EventArgs e)
        {
            try
            {
                if (Variable_Pantalla_Completa) Cursor.Show();
                if (MessageBox.Show(this, "Do you want to restore the settings to it's default values?", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (Variable_Pantalla_Completa) Cursor.Hide();
                    Registro_Restablecer_Opciones(); // Delete all the settings
                    Registro_Cargar_Opciones(); // Load the default settings
                    Registro_Guardar_Opciones(); // Save the defautl settings
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
