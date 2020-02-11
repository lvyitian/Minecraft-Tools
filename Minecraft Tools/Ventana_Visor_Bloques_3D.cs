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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Visor_Bloques_3D : Form
    {
        public Ventana_Visor_Bloques_3D()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "3D Block Viewer by Jupisoft for " + Program.Texto_Usuario;
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

        /// <summary>
        /// A fully transparent image. Used as a temporary fix when no image is used.
        /// </summary>
        internal static readonly Bitmap Imagen_Transparente = new Bitmap(16, 16, PixelFormat.Format32bppArgb);

        internal /*static */bool Variable_Habilitar_Bloque = true;
        internal static int Variable_Bloque = 0;
        internal static bool Variable_Habilitar_Dimensiones = false;
        internal static int Variable_Dimensiones = 256;
        internal static CheckState Variable_Suavizado = CheckState.Unchecked;
        internal static bool Variable_Habilitar_Rotación = true;
        internal static double Variable_Rotación = 0d;

        internal static CheckState Variable_Fondo = CheckState.Unchecked;

        internal static bool Variable_Habilitar_Ruta_1 = true;
        internal static string Variable_Ruta_1 = null;
        internal static bool Variable_Habilitar_Rotación_1 = true;
        internal static int Variable_Rotación_1 = (int)RotateFlipType.Rotate90FlipY;
        internal static bool Variable_Habilitar_Sombra_1 = true;
        internal static int Variable_Tipo_Sombra_1 = 1;
        internal static int Variable_Sombra_1 = 160;

        internal static bool Variable_Habilitar_Ruta_2 = true;
        internal static string Variable_Ruta_2 = null;
        internal static bool Variable_Habilitar_Rotación_2 = true;
        internal static int Variable_Rotación_2 = (int)RotateFlipType.RotateNoneFlipX;
        internal static bool Variable_Habilitar_Sombra_2 = true;
        internal static int Variable_Tipo_Sombra_2 = 0;
        internal static int Variable_Sombra_2 = 0;

        internal static bool Variable_Habilitar_Ruta_3 = true;
        internal static string Variable_Ruta_3 = null;
        internal static bool Variable_Habilitar_Rotación_3 = true;
        internal static int Variable_Rotación_3 = (int)RotateFlipType.RotateNoneFlipX;
        internal static bool Variable_Habilitar_Sombra_3 = true;
        internal static int Variable_Tipo_Sombra_3 = 0;
        internal static int Variable_Sombra_3 = 16;

        internal static bool Variable_Habilitar_Ruta_4 = true;
        internal static string Variable_Ruta_4 = null;
        internal static bool Variable_Habilitar_Rotación_4 = true;
        internal static int Variable_Rotación_4 = (int)RotateFlipType.RotateNoneFlipY;
        internal static bool Variable_Habilitar_Sombra_4 = true;
        internal static int Variable_Tipo_Sombra_4 = 0;
        internal static int Variable_Sombra_4 = 16;

        internal static bool Variable_Habilitar_Ruta_5 = true;
        internal static string Variable_Ruta_5 = null;
        internal static bool Variable_Habilitar_Rotación_5 = true;
        internal static int Variable_Rotación_5 = (int)RotateFlipType.RotateNoneFlipY;
        internal static bool Variable_Habilitar_Sombra_5 = true;
        internal static int Variable_Tipo_Sombra_5 = 0;
        internal static int Variable_Sombra_5 = 0;

        internal static bool Variable_Habilitar_Ruta_6 = true;
        internal static string Variable_Ruta_6 = null;
        internal static bool Variable_Habilitar_Rotación_6 = true;
        internal static int Variable_Rotación_6 = (int)RotateFlipType.Rotate90FlipX;
        internal static bool Variable_Habilitar_Sombra_6 = true;
        internal static int Variable_Tipo_Sombra_6 = 1;
        internal static int Variable_Sombra_6 = 160;
        
        internal Bitmap Imagen_Bloque = new Bitmap(16, 16, PixelFormat.Format32bppArgb);
        internal Bitmap Imagen_1_Fondo_Arriba_Izquierda = new Bitmap(16, 16, PixelFormat.Format32bppArgb);
        internal Bitmap Imagen_2_Fondo_Arriba_Derecha = new Bitmap(16, 16, PixelFormat.Format32bppArgb);
        internal Bitmap Imagen_3_Fondo_Abajo = new Bitmap(16, 16, PixelFormat.Format32bppArgb);
        internal Bitmap Imagen_4_Frente_Arriba = new Bitmap(16, 16, PixelFormat.Format32bppArgb);
        internal Bitmap Imagen_5_Frente_Abajo_Izquierda = new Bitmap(16, 16, PixelFormat.Format32bppArgb);
        internal Bitmap Imagen_6_Frente_Abajo_Derecha = new Bitmap(16, 16, PixelFormat.Format32bppArgb);

        private void Ventana_Visor_Bloques_3D_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título + " - [Drag and drop up to 6 images to generate your custom 3D blocks]";
                this.WindowState = FormWindowState.Maximized;
                Grupo_1.AllowDrop = true;
                Grupo_2.AllowDrop = true;
                Grupo_3.AllowDrop = true;
                Grupo_4.AllowDrop = true;
                Grupo_5.AllowDrop = true;
                Grupo_6.AllowDrop = true;
                Ocupado = true;
                Variable_Dimensiones = Math.Min(Picture.ClientSize.Width, Picture.ClientSize.Height);
                Registro_Cargar_Opciones();
                foreach (Minecraft.Bloques Bloque in Minecraft.Bloques.Matriz_Bloques)
                {
                    ComboBox_Bloque.Items.Add(Bloque.Nombre);
                }

                CheckBox_Bloque.Checked = Variable_Habilitar_Bloque;
                ComboBox_Bloque.SelectedIndex = Variable_Bloque;
                ComboBox_Bloque.Text = "Diamond ore";
                CheckBox_Dimensiones.Checked = Variable_Habilitar_Dimensiones;
                NumericUpDown_Dimensiones.Value = Variable_Dimensiones;
                CheckBox_Suavizado.CheckState = Variable_Suavizado;
                CheckBox_Rotación.Checked = Variable_Habilitar_Rotación;
                NumericUpDown_Rotación.Value = (decimal)Variable_Rotación;

                Picture.BackColor = Variable_Fondo == CheckState.Unchecked ? Color.Gray : Variable_Fondo == CheckState.Checked ? Color.White : Color.Black;

                CheckBox_Ruta_1.Checked = Variable_Habilitar_Ruta_1;
                CheckBox_Ruta_2.Checked = Variable_Habilitar_Ruta_2;
                CheckBox_Ruta_3.Checked = Variable_Habilitar_Ruta_3;
                CheckBox_Ruta_4.Checked = Variable_Habilitar_Ruta_4;
                CheckBox_Ruta_5.Checked = Variable_Habilitar_Ruta_5;
                CheckBox_Ruta_6.Checked = Variable_Habilitar_Ruta_6;

                CheckBox_Rotación_1.Checked = Variable_Habilitar_Rotación_1;
                CheckBox_Rotación_2.Checked = Variable_Habilitar_Rotación_2;
                CheckBox_Rotación_3.Checked = Variable_Habilitar_Rotación_3;
                CheckBox_Rotación_4.Checked = Variable_Habilitar_Rotación_4;
                CheckBox_Rotación_5.Checked = Variable_Habilitar_Rotación_5;
                CheckBox_Rotación_6.Checked = Variable_Habilitar_Rotación_6;

                ComboBox_Rotación_1.SelectedIndex = Variable_Rotación_1;
                ComboBox_Rotación_2.SelectedIndex = Variable_Rotación_2;
                ComboBox_Rotación_3.SelectedIndex = Variable_Rotación_3;
                ComboBox_Rotación_4.SelectedIndex = Variable_Rotación_4;
                ComboBox_Rotación_5.SelectedIndex = Variable_Rotación_5;
                ComboBox_Rotación_6.SelectedIndex = Variable_Rotación_6;

                CheckBox_Sombra_1.Checked = Variable_Habilitar_Sombra_1;
                CheckBox_Sombra_2.Checked = Variable_Habilitar_Sombra_2;
                CheckBox_Sombra_3.Checked = Variable_Habilitar_Sombra_3;
                CheckBox_Sombra_4.Checked = Variable_Habilitar_Sombra_4;
                CheckBox_Sombra_5.Checked = Variable_Habilitar_Sombra_5;
                CheckBox_Sombra_6.Checked = Variable_Habilitar_Sombra_6;

                ComboBox_Tipo_Sombra_1.SelectedIndex = Variable_Tipo_Sombra_1;
                ComboBox_Tipo_Sombra_2.SelectedIndex = Variable_Tipo_Sombra_2;
                ComboBox_Tipo_Sombra_3.SelectedIndex = Variable_Tipo_Sombra_3;
                ComboBox_Tipo_Sombra_4.SelectedIndex = Variable_Tipo_Sombra_4;
                ComboBox_Tipo_Sombra_5.SelectedIndex = Variable_Tipo_Sombra_5;
                ComboBox_Tipo_Sombra_6.SelectedIndex = Variable_Tipo_Sombra_6;

                NumericUpDown_Sombra_1.Value = Variable_Sombra_1;
                NumericUpDown_Sombra_2.Value = Variable_Sombra_2;
                NumericUpDown_Sombra_3.Value = Variable_Sombra_3;
                NumericUpDown_Sombra_4.Value = Variable_Sombra_4;
                NumericUpDown_Sombra_5.Value = Variable_Sombra_5;
                NumericUpDown_Sombra_6.Value = Variable_Sombra_6;
                
                Ocupado = false;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Bloques_3D_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Bloques_3D_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Bloques_3D_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Bloques_3D_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Bloques_3D_KeyDown(object sender, KeyEventArgs e)
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

        private void Ventana_Visor_Bloques_3D_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Visor_Bloques_3D_DragDrop(object sender, DragEventArgs e)
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
                                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Checked);
                                    if (Imagen != null) // Load the 6 side images at once as the same.
                                    {
                                        Ocupado = true;
                                        Imagen_1_Fondo_Arriba_Izquierda = Imagen.Clone() as Bitmap;
                                        Imagen_2_Fondo_Arriba_Derecha = Imagen.Clone() as Bitmap;
                                        Imagen_3_Fondo_Abajo = Imagen.Clone() as Bitmap;
                                        Imagen_4_Frente_Arriba = Imagen.Clone() as Bitmap;
                                        Imagen_5_Frente_Abajo_Izquierda = Imagen.Clone() as Bitmap;
                                        Imagen_6_Frente_Abajo_Derecha = Imagen.Clone() as Bitmap;
                                        TextBox_Ruta_1.Text = Ruta;
                                        TextBox_Ruta_2.Text = Ruta;
                                        TextBox_Ruta_3.Text = Ruta;
                                        TextBox_Ruta_4.Text = Ruta;
                                        TextBox_Ruta_5.Text = Ruta;
                                        TextBox_Ruta_6.Text = Ruta;
                                        Picture_1.Image = Program.Obtener_Imagen_Miniatura(Imagen, 18, 18, true, true, CheckState.Checked);
                                        Picture_2.Image = Program.Obtener_Imagen_Miniatura(Imagen, 18, 18, true, true, CheckState.Checked);
                                        Picture_3.Image = Program.Obtener_Imagen_Miniatura(Imagen, 18, 18, true, true, CheckState.Checked);
                                        Picture_4.Image = Program.Obtener_Imagen_Miniatura(Imagen, 18, 18, true, true, CheckState.Checked);
                                        Picture_5.Image = Program.Obtener_Imagen_Miniatura(Imagen, 18, 18, true, true, CheckState.Checked);
                                        Picture_6.Image = Program.Obtener_Imagen_Miniatura(Imagen, 18, 18, true, true, CheckState.Checked);
                                        CheckBox_Bloque.Checked = false;
                                        Ocupado = false;
                                        Imagen.Dispose();
                                        Imagen = null;
                                        Actualizar_Bloque_3D();
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

        private void Grupo_1_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Grupo_1_DragDrop(object sender, DragEventArgs e)
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
                                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Checked);
                                    if (Imagen != null)
                                    {
                                        Ocupado = true;
                                        Imagen_1_Fondo_Arriba_Izquierda = Imagen;
                                        TextBox_Ruta_1.Text = Ruta;
                                        Picture_1.Image = Program.Obtener_Imagen_Miniatura(Imagen, 18, 18, true, true, CheckState.Checked);
                                        CheckBox_Bloque.Checked = false;
                                        Ocupado = false;
                                        Actualizar_Bloque_3D();
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

        private void Grupo_2_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Grupo_2_DragDrop(object sender, DragEventArgs e)
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
                                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Checked);
                                    if (Imagen != null)
                                    {
                                        Ocupado = true;
                                        Imagen_2_Fondo_Arriba_Derecha = Imagen;
                                        TextBox_Ruta_2.Text = Ruta;
                                        Picture_2.Image = Program.Obtener_Imagen_Miniatura(Imagen, 18, 18, true, true, CheckState.Checked);
                                        CheckBox_Bloque.Checked = false;
                                        Ocupado = false;
                                        Actualizar_Bloque_3D();
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

        private void Grupo_3_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Grupo_3_DragDrop(object sender, DragEventArgs e)
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
                                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Checked);
                                    if (Imagen != null)
                                    {
                                        Ocupado = true;
                                        Imagen_3_Fondo_Abajo = Imagen;
                                        TextBox_Ruta_3.Text = Ruta;
                                        Picture_3.Image = Program.Obtener_Imagen_Miniatura(Imagen, 18, 18, true, true, CheckState.Checked);
                                        CheckBox_Bloque.Checked = false;
                                        Ocupado = false;
                                        Actualizar_Bloque_3D();
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

        private void Grupo_4_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Grupo_4_DragDrop(object sender, DragEventArgs e)
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
                                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Checked);
                                    if (Imagen != null)
                                    {
                                        Ocupado = true;
                                        Imagen_4_Frente_Arriba = Imagen;
                                        TextBox_Ruta_4.Text = Ruta;
                                        Picture_4.Image = Program.Obtener_Imagen_Miniatura(Imagen, 18, 18, true, true, CheckState.Checked);
                                        CheckBox_Bloque.Checked = false;
                                        Ocupado = false;
                                        Actualizar_Bloque_3D();
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

        private void Grupo_5_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Grupo_5_DragDrop(object sender, DragEventArgs e)
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
                                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Checked);
                                    if (Imagen != null)
                                    {
                                        Ocupado = true;
                                        Imagen_5_Frente_Abajo_Izquierda = Imagen;
                                        TextBox_Ruta_5.Text = Ruta;
                                        Picture_5.Image = Program.Obtener_Imagen_Miniatura(Imagen, 18, 18, true, true, CheckState.Checked);
                                        CheckBox_Bloque.Checked = false;
                                        Ocupado = false;
                                        Actualizar_Bloque_3D();
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

        private void Grupo_6_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Grupo_6_DragDrop(object sender, DragEventArgs e)
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
                                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Checked);
                                    if (Imagen != null)
                                    {
                                        Ocupado = true;
                                        Imagen_6_Frente_Abajo_Derecha = Imagen;
                                        TextBox_Ruta_6.Text = Ruta;
                                        Picture_6.Image = Program.Obtener_Imagen_Miniatura(Imagen, 18, 18, true, true, CheckState.Checked);
                                        CheckBox_Bloque.Checked = false;
                                        Ocupado = false;
                                        Actualizar_Bloque_3D();
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

        /// <summary>
        /// Updates the 3D block based on the selected settings. This function saves a lot of code each time it's called.
        /// </summary>
        internal void Actualizar_Bloque_3D()
        {
            try
            {
                if (!Ocupado)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        Picture.Image = Program.Obtener_Imagen_Bloque_3D(
                        Variable_Habilitar_Dimensiones ? Variable_Dimensiones : Math.Min(Picture.ClientSize.Width, Picture.ClientSize.Height),
                        Variable_Habilitar_Rotación ? Variable_Rotación : 0d,
                        Variable_Habilitar_Ruta_1 ? (Variable_Habilitar_Bloque ? Imagen_Bloque.Clone() as Bitmap : Imagen_1_Fondo_Arriba_Izquierda.Clone() as Bitmap) : Imagen_Transparente.Clone() as Bitmap,
                        Variable_Habilitar_Ruta_2 ? (Variable_Habilitar_Bloque ? Imagen_Bloque.Clone() as Bitmap : Imagen_2_Fondo_Arriba_Derecha.Clone() as Bitmap) : Imagen_Transparente.Clone() as Bitmap,
                        Variable_Habilitar_Ruta_3 ? (Variable_Habilitar_Bloque ? Imagen_Bloque.Clone() as Bitmap : Imagen_3_Fondo_Abajo.Clone() as Bitmap) : Imagen_Transparente.Clone() as Bitmap,
                        Variable_Habilitar_Ruta_4 ? (Variable_Habilitar_Bloque ? Imagen_Bloque.Clone() as Bitmap : Imagen_4_Frente_Arriba.Clone() as Bitmap) : Imagen_Transparente.Clone() as Bitmap,
                        Variable_Habilitar_Ruta_5 ? (Variable_Habilitar_Bloque ? Imagen_Bloque.Clone() as Bitmap : Imagen_5_Frente_Abajo_Izquierda.Clone() as Bitmap) : Imagen_Transparente.Clone() as Bitmap,
                        Variable_Habilitar_Ruta_6 ? (Variable_Habilitar_Bloque ? Imagen_Bloque.Clone() as Bitmap : Imagen_6_Frente_Abajo_Derecha.Clone() as Bitmap) : Imagen_Transparente.Clone() as Bitmap,
                        Variable_Habilitar_Rotación_1 ? (RotateFlipType)Variable_Rotación_1 : RotateFlipType.RotateNoneFlipNone,
                        Variable_Habilitar_Rotación_2 ? (RotateFlipType)Variable_Rotación_2 : RotateFlipType.RotateNoneFlipNone,
                        Variable_Habilitar_Rotación_3 ? (RotateFlipType)Variable_Rotación_3 : RotateFlipType.RotateNoneFlipNone,
                        Variable_Habilitar_Rotación_4 ? (RotateFlipType)Variable_Rotación_4 : RotateFlipType.RotateNoneFlipNone,
                        Variable_Habilitar_Rotación_5 ? (RotateFlipType)Variable_Rotación_5 : RotateFlipType.RotateNoneFlipNone,
                        Variable_Habilitar_Rotación_6 ? (RotateFlipType)Variable_Rotación_6 : RotateFlipType.RotateNoneFlipNone,
                        !Variable_Habilitar_Sombra_1 || Variable_Tipo_Sombra_1 == 0 ? false : true,
                        !Variable_Habilitar_Sombra_2 || Variable_Tipo_Sombra_2 == 0 ? false : true,
                        !Variable_Habilitar_Sombra_3 || Variable_Tipo_Sombra_3 == 0 ? false : true,
                        !Variable_Habilitar_Sombra_4 || Variable_Tipo_Sombra_4 == 0 ? false : true,
                        !Variable_Habilitar_Sombra_5 || Variable_Tipo_Sombra_5 == 0 ? false : true,
                        !Variable_Habilitar_Sombra_6 || Variable_Tipo_Sombra_6 == 0 ? false : true,
                        Variable_Habilitar_Sombra_1 ? Variable_Sombra_1 : 0,
                        Variable_Habilitar_Sombra_2 ? Variable_Sombra_2 : 0,
                        Variable_Habilitar_Sombra_3 ? Variable_Sombra_3 : 0,
                        Variable_Habilitar_Sombra_4 ? Variable_Sombra_4 : 0,
                        Variable_Habilitar_Sombra_5 ? Variable_Sombra_5 : 0,
                        Variable_Habilitar_Sombra_6 ? Variable_Sombra_6 : 0,
                        Variable_Suavizado);
                        Picture.Invalidate();
                        Picture.Update();
                    }
                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                    finally { this.Cursor = Cursors.Default; }
                }
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
                Program.Ejecutar_Ruta(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Actualizar_Bloque_3D();
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
                    string Ruta = Program.Obtener_Ruta_Temporal_Escritorio() + " 3D block.png";
                    Picture.Image.Save(Ruta, ImageFormat.Png);
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

        private void CheckBox_Bloque_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Bloque = CheckBox_Bloque.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Bloque_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Bloque.SelectedIndex > -1)
                {
                    Variable_Bloque = ComboBox_Bloque.SelectedIndex;
                    Imagen_Bloque = Program.Obtener_Imagen_Recursos(Minecraft.Bloques.Matriz_Bloques[Variable_Bloque].Recurso);
                    Actualizar_Bloque_3D();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Dimensiones_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Dimensiones = CheckBox_Dimensiones.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Dimensiones_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Dimensiones = (int)NumericUpDown_Dimensiones.Value;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Suavizado_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Suavizado = CheckBox_Suavizado.CheckState;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Rotación_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Rotación = CheckBox_Rotación.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Rotación_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Rotación = (double)NumericUpDown_Rotación.Value;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Ruta_1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Ruta_1 = CheckBox_Ruta_1.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_Ruta_1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string Ruta = TextBox_Ruta_1.Text;
                if (!Ocupado && !string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                {
                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Checked);
                    if (Imagen != null)
                    {
                        Imagen_1_Fondo_Arriba_Izquierda = Imagen;
                        TextBox_Ruta_1.Text = Ruta;
                        Picture_1.Image = Program.Obtener_Imagen_Miniatura(Imagen, 18, 18, true, true, CheckState.Checked);
                        Ocupado = true;
                        CheckBox_Bloque.Checked = false;
                        Ocupado = false;
                        Actualizar_Bloque_3D();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Rotación_1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Rotación_1 = CheckBox_Rotación_1.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Rotación_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Rotación_1 = ComboBox_Rotación_1.SelectedIndex;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Sombra_1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Sombra_1 = CheckBox_Sombra_1.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Tipo_Sombra_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Tipo_Sombra_1 = ComboBox_Tipo_Sombra_1.SelectedIndex;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Sombra_1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Sombra_1 = (int)NumericUpDown_Sombra_1.Value;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Ruta_2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Ruta_2 = CheckBox_Ruta_2.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_Ruta_2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string Ruta = TextBox_Ruta_2.Text;
                if (!Ocupado && !string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                {
                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Checked);
                    if (Imagen != null)
                    {
                        Imagen_2_Fondo_Arriba_Derecha = Imagen;
                        TextBox_Ruta_2.Text = Ruta;
                        Picture_2.Image = Program.Obtener_Imagen_Miniatura(Imagen, 18, 18, true, true, CheckState.Checked);
                        Ocupado = true;
                        CheckBox_Bloque.Checked = false;
                        Ocupado = false;
                        Actualizar_Bloque_3D();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Rotación_2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Rotación_2 = CheckBox_Rotación_2.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Rotación_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Rotación_2 = ComboBox_Rotación_2.SelectedIndex;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Sombra_2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Sombra_2 = CheckBox_Sombra_2.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Tipo_Sombra_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Tipo_Sombra_2 = ComboBox_Tipo_Sombra_2.SelectedIndex;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Sombra_2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Sombra_2 = (int)NumericUpDown_Sombra_2.Value;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Ruta_3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Ruta_3 = CheckBox_Ruta_3.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_Ruta_3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string Ruta = TextBox_Ruta_3.Text;
                if (!Ocupado && !string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                {
                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Checked);
                    if (Imagen != null)
                    {
                        Imagen_3_Fondo_Abajo = Imagen;
                        TextBox_Ruta_3.Text = Ruta;
                        Picture_3.Image = Program.Obtener_Imagen_Miniatura(Imagen, 18, 18, true, true, CheckState.Checked);
                        Ocupado = true;
                        CheckBox_Bloque.Checked = false;
                        Ocupado = false;
                        Actualizar_Bloque_3D();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Rotación_3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Rotación_3 = CheckBox_Rotación_3.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Rotación_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Rotación_3 = ComboBox_Rotación_3.SelectedIndex;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Sombra_3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Sombra_3 = CheckBox_Sombra_3.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Tipo_Sombra_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Tipo_Sombra_3 = ComboBox_Tipo_Sombra_3.SelectedIndex;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Sombra_3_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Sombra_3 = (int)NumericUpDown_Sombra_3.Value;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Ruta_4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Ruta_4 = CheckBox_Ruta_4.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_Ruta_4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string Ruta = TextBox_Ruta_4.Text;
                if (!Ocupado && !string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                {
                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Checked);
                    if (Imagen != null)
                    {
                        Imagen_4_Frente_Arriba = Imagen;
                        TextBox_Ruta_4.Text = Ruta;
                        Picture_4.Image = Program.Obtener_Imagen_Miniatura(Imagen, 18, 18, true, true, CheckState.Checked);
                        Ocupado = true;
                        CheckBox_Bloque.Checked = false;
                        Ocupado = false;
                        Actualizar_Bloque_3D();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Rotación_4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Rotación_4 = CheckBox_Rotación_4.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Rotación_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Rotación_4 = ComboBox_Rotación_4.SelectedIndex;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Sombra_4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Sombra_4 = CheckBox_Sombra_4.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Tipo_Sombra_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Tipo_Sombra_4 = ComboBox_Tipo_Sombra_4.SelectedIndex;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Sombra_4_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Sombra_4 = (int)NumericUpDown_Sombra_4.Value;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Ruta_5_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Ruta_5 = CheckBox_Ruta_5.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_Ruta_5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string Ruta = TextBox_Ruta_5.Text;
                if (!Ocupado && !string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                {
                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Checked);
                    if (Imagen != null)
                    {
                        Imagen_5_Frente_Abajo_Izquierda = Imagen;
                        TextBox_Ruta_5.Text = Ruta;
                        Picture_5.Image = Program.Obtener_Imagen_Miniatura(Imagen, 18, 18, true, true, CheckState.Checked);
                        Ocupado = true;
                        CheckBox_Bloque.Checked = false;
                        Ocupado = false;
                        Actualizar_Bloque_3D();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Rotación_5_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Rotación_5 = CheckBox_Rotación_5.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Rotación_5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Rotación_5 = ComboBox_Rotación_5.SelectedIndex;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Sombra_5_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Sombra_5 = CheckBox_Sombra_5.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Tipo_Sombra_5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Tipo_Sombra_5 = ComboBox_Tipo_Sombra_5.SelectedIndex;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Sombra_5_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Sombra_5 = (int)NumericUpDown_Sombra_5.Value;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Ruta_6_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Ruta_6 = CheckBox_Ruta_6.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_Ruta_6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string Ruta = TextBox_Ruta_6.Text;
                if (!Ocupado && !string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                {
                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Checked);
                    if (Imagen != null)
                    {
                        Imagen_6_Frente_Abajo_Derecha = Imagen;
                        TextBox_Ruta_6.Text = Ruta;
                        Picture_6.Image = Program.Obtener_Imagen_Miniatura(Imagen, 18, 18, true, true, CheckState.Checked);
                        Ocupado = true;
                        CheckBox_Bloque.Checked = false;
                        Ocupado = false;
                        Actualizar_Bloque_3D();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Rotación_6_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Rotación_6 = CheckBox_Rotación_6.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Rotación_6_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Rotación_6 = ComboBox_Rotación_6.SelectedIndex;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Sombra_6_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Habilitar_Sombra_6 = CheckBox_Sombra_6.Checked;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Tipo_Sombra_6_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Tipo_Sombra_6 = ComboBox_Tipo_Sombra_6.SelectedIndex;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Sombra_6_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Sombra_6 = (int)NumericUpDown_Sombra_6.Value;
                Actualizar_Bloque_3D();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    if (Variable_Fondo == CheckState.Unchecked) Variable_Fondo = CheckState.Checked;
                    else if (Variable_Fondo == CheckState.Checked) Variable_Fondo = CheckState.Indeterminate;
                    else Variable_Fondo = CheckState.Unchecked;
                    Picture.BackColor = Variable_Fondo == CheckState.Unchecked ? Color.Gray : Variable_Fondo == CheckState.Checked ? Color.White : Color.Black;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
