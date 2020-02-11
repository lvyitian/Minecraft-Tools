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
    public partial class Ventana_Contador_FPS : Form
    {
        public Ventana_Contador_FPS()
        {
            InitializeComponent();
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        internal readonly string Texto_Título = "FPS Counter by Jupisoft for " + Program.Texto_Usuario;
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
        
        internal Rectangle Rectángulo_Pantalla = Screen.PrimaryScreen.Bounds;

        internal List<int> Lista_Milisegundos = new List<int>();
        internal List<bool> Lista_Cambios = new List<bool>();

        private void Ventana_Plantilla_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                //this.WindowState = FormWindowState.Maximized;
                Combo_Modo_Captura.SelectedIndex = 0;
                Numérico_X.Maximum = Rectángulo_Pantalla.Width - 1;
                Numérico_Y.Maximum = Rectángulo_Pantalla.Height - 1;
                Numérico_Ancho.Maximum = Rectángulo_Pantalla.Width;
                Numérico_Alto.Maximum = Rectángulo_Pantalla.Height;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Plantilla_DragDrop(object sender, DragEventArgs e)
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

        private void Ventana_Plantilla_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Normal) this.WindowState = FormWindowState.Maximized;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal bool Pendiente_Rectángulo = false;
        internal Point Posición_Pendiente = new Point(0, 0);
        internal Rectangle Rectángulo_Captura = new Rectangle(0, 0, 1, 1);
        internal byte[] Matriz_Bytes_Anterior = null;

        private void Ventana_Plantilla_KeyDown(object sender, KeyEventArgs e)
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
                    else if (e.KeyCode == Keys.Insert)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        SystemSounds.Asterisk.Play();
                        if (!Pendiente_Rectángulo)
                        {
                            Posición_Pendiente = Control.MousePosition;
                            Numérico_X.Value = Posición_Pendiente.X;
                            Numérico_Y.Value = Posición_Pendiente.Y;
                            Pendiente_Rectángulo = true;
                        }
                        else
                        {
                            Point Posición = Control.MousePosition;
                            Rectángulo_Captura = new Rectangle(Posición_Pendiente.X, Posición_Pendiente.Y, Math.Abs(Posición.X - Posición_Pendiente.X) + 1, Math.Abs(Posición.Y - Posición_Pendiente.Y) + 1);
                            Numérico_Ancho.Value = Rectángulo_Captura.Width;
                            Numérico_Alto.Value = Rectángulo_Captura.Height;
                            Posición_Pendiente = new Point(0, 0);
                            Pendiente_Rectángulo = false;
                        }
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

                bool Aumentar_FPS = false;
                if (CheckBox_Modo_Captura.Checked)
                {
                    Rectangle Rectángulo = Rectángulo_Captura;
                    if (Rectángulo.X < 0) Rectángulo.X = 0;
                    else if (Rectángulo.X >= Rectángulo_Pantalla.Width) Rectángulo.X = Rectángulo_Pantalla.Width - 1;

                    if (Rectángulo.Y < 0) Rectángulo.Y = 0;
                    else if (Rectángulo.Y >= Rectángulo_Pantalla.Height) Rectángulo.Y = Rectángulo_Pantalla.Height - 1;

                    if (Rectángulo.Width < 1) Rectángulo.Width = 1;
                    else if (Rectángulo.Width > Rectángulo_Pantalla.Width) Rectángulo.Width = Rectángulo_Pantalla.Width - 1;

                    if (Rectángulo.Height < 1) Rectángulo.Height = 1;
                    else if (Rectángulo.Height > Rectángulo_Pantalla.Height) Rectángulo.Height = Rectángulo_Pantalla.Width - 1;

                    int Ancho = Rectángulo.Width;
                    int Alto = Rectángulo.Height;

                    Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format24bppRgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    //Pintar.CopyFromScreen(Rectángulo.X, Rectángulo.Y, 0, 0, Imagen.Size, Filtro != Filtros.Negativo ? CopyPixelOperation.SourceCopy : CopyPixelOperation.NotSourceCopy);
                    using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero)) // Desktop window?
                    {
                        IntPtr hSrcDC = gsrc.GetHdc();
                        IntPtr hDC = Pintar.GetHdc();
                        int retval = BitBlt(hDC, 0, 0, Ancho, Alto, hSrcDC, Rectángulo.X, Rectángulo.Y, (int)CopyPixelOperation.SourceCopy);
                        Pintar.ReleaseHdc();
                        gsrc.ReleaseHdc();
                    }
                    Pintar.Dispose();
                    Pintar = null;

                    BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                    byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                    int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                    int Bytes_Ancho = Math.Abs(Bitmap_Data.Stride);
                    int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);

                    byte[] Matriz_Bytes_Original = (byte[])Matriz_Bytes.Clone();
                    
                    if (Matriz_Bytes_Anterior != null && Matriz_Bytes_Anterior.Length == Matriz_Bytes.Length)
                    {
                        for (int Índice_Y = 0, Índice = 0, Subíndice = 0; Índice_Y < Alto; Índice_Y++, Índice += Bytes_Diferencia)
                        {
                            for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice += Bytes_Aumento)
                            {
                                if (!Aumentar_FPS &&
                                    (Matriz_Bytes[Índice + 2] != Matriz_Bytes_Anterior[Índice + 2] ||
                                    Matriz_Bytes[Índice + 1] != Matriz_Bytes_Anterior[Índice + 1] ||
                                    Matriz_Bytes[Índice] != Matriz_Bytes_Anterior[Índice])) // +1 FPS.
                                {
                                    Aumentar_FPS = true;
                                    //break;
                                }

                                byte Rojo = Matriz_Bytes_Original[Índice + 2];
                                byte Verde = Matriz_Bytes_Original[Índice + 1];
                                byte Azul = Matriz_Bytes_Original[Índice];
                                int Valor_R = 255, Valor_G = 255, Valor_B = 255;
                                int Valor_R_Temporal = 0, Valor_G_Temporal = 0, Valor_B_Temporal = 0;
                                for (int Subíndice_Y = -1, X = 0; Subíndice_Y <= 1; Subíndice_Y++, X++)
                                {
                                    for (int Subíndice_X = -1, Y = 0; Subíndice_X <= 1; Subíndice_X++, Y++)
                                    {
                                        if ((Subíndice_X != 0 || Subíndice_Y != 0) && Índice_X + Subíndice_X > -1 && Índice_X + Subíndice_X < Ancho && Índice_Y + Subíndice_Y > -1 && Índice_Y + Subíndice_Y < Alto)
                                        {
                                            Subíndice = (Bytes_Ancho * (Índice_Y + Subíndice_Y)) + ((Índice_X + Subíndice_X) * Bytes_Aumento);
                                            Valor_R_Temporal = Math.Abs(Rojo - Matriz_Bytes_Original[Subíndice + 2]);
                                            Valor_G_Temporal = Math.Abs(Verde - Matriz_Bytes_Original[Subíndice + 1]);
                                            Valor_B_Temporal = Math.Abs(Azul - Matriz_Bytes_Original[Subíndice]);
                                            if (Valor_R_Temporal < Valor_R && Valor_R_Temporal > 0 && Matriz_Bytes_Original[Subíndice + 2] < Rojo) Valor_R = Valor_R_Temporal;
                                            if (Valor_G_Temporal < Valor_G && Valor_G_Temporal > 0 && Matriz_Bytes_Original[Subíndice + 1] < Verde) Valor_G = Valor_G_Temporal;
                                            if (Valor_B_Temporal < Valor_B && Valor_B_Temporal > 0 && Matriz_Bytes_Original[Subíndice] < Azul) Valor_B = Valor_B_Temporal;
                                            //Valor_R += Matriz_Bytes_Original[Subíndice + 2];
                                            //Valor_G += Matriz_Bytes_Original[Subíndice + 1];
                                            //Valor_B += Matriz_Bytes_Original[Subíndice];
                                            //Divisor++;
                                        }
                                    }
                                }
                                //Valor_R /= Divisor;
                                //Valor_G /= Divisor;
                                //Valor_B /= Divisor;
                                Matriz_Bytes[Índice + 2] = (byte)(255 - Valor_R);
                                Matriz_Bytes[Índice + 1] = (byte)(255 - Valor_G);
                                Matriz_Bytes[Índice] = (byte)(255 - Valor_B);
                            }
                            //if (Aumentar_FPS) break;
                        }
                        if (Aumentar_FPS) FPS++;
                    }
                    Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                    Imagen.UnlockBits(Bitmap_Data);
                    Bitmap_Data = null;
                    //Imagen.Dispose();
                    //Imagen = null;
                    //this.Text = Texto_Título + " - [" + Program.Traducir_Número(Ancho) + " x " + Program.Traducir_Número(Alto) + (Ancho * Alto != 1 ? " pixels" : " pixel") + "]";
                    this.BackgroundImage = Imagen;
                    Matriz_Bytes_Anterior = Matriz_Bytes_Original;
                }

                long FPS_Milisegundo = FPS_Cronómetro.ElapsedMilliseconds;
                long FPS_Segundo = FPS_Milisegundo / 1000L;
                if (FPS_Segundo != FPS_Segundo_Anterior)
                {
                    FPS_Segundo_Anterior = FPS_Segundo;

                    // ...

                    //Etiqueta_FPS.Text = Program.Traducir_Número(FPS) + " FPS";
                    Lista_FPS_Anteriores.Add(FPS);
                    if (Lista_FPS_Anteriores.Count > 10) Lista_FPS_Anteriores.RemoveRange(0, Lista_FPS_Anteriores.Count - 10);
                    double Media_FPS = 0d;
                    foreach (int FPS_Anterior in Lista_FPS_Anteriores)
                    {
                        Media_FPS += FPS_Anterior;
                    }
                    Media_FPS /= (double)Lista_FPS_Anteriores.Count;
                    Etiqueta_FPS.Text = Program.Traducir_Número(FPS) + ", " + Program.Traducir_Número_Decimales_Redondear(Media_FPS, 4); //" FPS";
                    FPS = 0;

                    // ...

                    FPS_Real = FPS_Temporal;
                    Barra_Estado_Etiqueta_FPS.Text = FPS_Real.ToString() + " FPS";
                    FPS_Temporal = 0L;

                    // ...

                    Bitmap Imagen = new Bitmap(1000, 18, PixelFormat.Format32bppArgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    for (int Índice = 0; Índice < Lista_Milisegundos.Count; Índice++)
                    {
                        Pintar.FillRectangle(Lista_Cambios[Índice] ? Brushes.Lime : Brushes.Red, Lista_Milisegundos[Índice], 0, 1, 18);
                    }
                    Pintar.Dispose();
                    Pintar = null;
                    Picture_FPS.Image = Imagen;

                    Lista_Milisegundos.Clear();
                    Lista_Cambios.Clear();

                    Lista_Milisegundos.Add((int)(FPS_Milisegundo % 1000L));
                    Lista_Cambios.Add(Aumentar_FPS);
                }
                else
                {
                    Lista_Milisegundos.Add((int)(FPS_Milisegundo % 1000L));
                    Lista_Cambios.Add(Aumentar_FPS);
                }
                FPS_Temporal++;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal int FPS = 0;
        internal List<int> Lista_FPS_Anteriores = new List<int>();

        private void Numérico_X_ValueChanged(object sender, EventArgs e)
        {
            Rectángulo_Captura.X = (int)Numérico_X.Value;
        }

        private void Numérico_Y_ValueChanged(object sender, EventArgs e)
        {
            Rectángulo_Captura.Y = (int)Numérico_Y.Value;
        }

        private void Numérico_Ancho_ValueChanged(object sender, EventArgs e)
        {
            Rectángulo_Captura.Width = (int)Numérico_Ancho.Value;
        }

        private void Numérico_Alto_ValueChanged(object sender, EventArgs e)
        {
            Rectángulo_Captura.Height = (int)Numérico_Alto.Value;
        }
    }
}
