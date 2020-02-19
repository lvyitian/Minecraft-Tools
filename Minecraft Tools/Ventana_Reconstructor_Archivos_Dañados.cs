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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Reconstructor_Archivos_Dañados : Form
    {
        public Ventana_Reconstructor_Archivos_Dañados()
        {
            InitializeComponent();
        }

        internal class Comparador_Extensiones : IComparer<KeyValuePair<string, byte>>
        {
            public int Compare(KeyValuePair<string, byte> X, KeyValuePair<string, byte> Y)
            {
                if (X.Value > Y.Value) return -1;
                else if (Y.Value > X.Value) return 1;
                else return string.Compare(X.Key, Y.Key);
            }
        }

        internal class Comparador_Tamaños_Rutas : IComparer<string>
        {
            public int Compare(string Ruta_1, string Ruta_2)
            {
                long Tamaño_1 = new FileInfo(Ruta_1).Length;
                long Tamaño_2 = new FileInfo(Ruta_2).Length;
                if (Tamaño_1 > Tamaño_2) return -1;
                else if (Tamaño_2 > Tamaño_1) return 1;
                else
                {
                    if (Lista_CheckedListBox_Rutas != null && Lista_CheckedListBox_Rutas.Count > 0)
                    {
                        int Índice_1 = Lista_CheckedListBox_Rutas.IndexOf(Ruta_1);
                        int Índice_2 = Lista_CheckedListBox_Rutas.IndexOf(Ruta_2);
                        if (Índice_1 < Índice_2) return -1;
                        else if (Índice_1 > Índice_2) return 1;
                        else return 0;
                    }
                    else return string.Compare(Ruta_1, Ruta_2);
                }
            }
        }

        internal List<bool> Lista_CheckedListBox_Habilitados = null;
        private static List<string> Lista_CheckedListBox_Ordenada = null;
        private static List<string> Lista_CheckedListBox_Rutas = null;

        internal static bool[] Matriz_Booleans_Errores = new bool[100];
        internal static bool Ventana_Siempre_Visible = false;
        internal ComboBox[] Matriz_Combos_Bloques_Errores = null;
        internal Graphics Pintar_1 = null;
        internal Graphics Pintar_2 = null;
        internal Graphics Pintar_3 = null;
        internal Graphics Pintar_4 = null;
        internal Graphics Pintar_5 = null;
        internal Graphics Pintar = null;
        internal Thread Subproceso = null;

        internal readonly string Texto_Título = "Damaged Files Rebuilder by Jupisoft for " + Program.Texto_Usuario;
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

        private void Ventana_Reconstructor_Dañados_Archivos_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título + " - [Please don't recombine different files as one]";
                //this.WindowState = FormWindowState.Maximized;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
                Matriz_Combos_Bloques_Errores = new ComboBox[100];
                Panel[] Matriz_Paneles = new Panel[4] { Panel_Bloques_25, Panel_Bloques_50, Panel_Bloques_75, Panel_Bloques_100 };
                for (int Índice_Panel = 0, Índice_Bloque = 0; Índice_Panel < 4; Índice_Panel++)
                {
                    for (int Índice_Y = 0; Índice_Y < 5; Índice_Y++)
                    {
                        for (int Índice_X = 0; Índice_X < 5; Índice_X++, Índice_Bloque++)
                        {
                            ComboBox Combo = new ComboBox();
                            Combo.BackColor = Color.White;
                            Combo.ContextMenuStrip = Menú_Contextual;
                            Combo.DropDownStyle = ComboBoxStyle.DropDownList;
                            Combo.Items.Add("1º");
                            Combo.Items.Add("2º");
                            Combo.KeyDown += new KeyEventHandler(Ventana_Reconstructor_Archivos_Dañados_KeyDown);
                            Combo.MouseDown += new MouseEventHandler(Combos_MouseDown);
                            Combo.Name = "Combo_Bloque_" + Índice_Bloque.ToString();
                            Combo.Margin = new Padding(0, 0, 0, 0);
                            Combo.RightToLeft = !Matriz_Booleans_Errores[Índice_Bloque] ? RightToLeft.No : RightToLeft.Yes;
                            Combo.SelectedIndex = !Matriz_Booleans_Errores[Índice_Bloque] ? 0 : 1;
                            Combo.SelectedIndexChanged += new EventHandler(Combo_SelectedIndexChanged);
                            Matriz_Combos_Bloques_Errores[Índice_Bloque] = Combo;
                            Matriz_Paneles[Índice_Panel].Controls.Add(Combo);
                            Combo.Location = new Point(1 + (Índice_X * 37), 1 + (Índice_Y * 20));
                            Combo.Size = new Size(38, 21);
                            Combo.TabIndex = Índice_X + (Índice_Y * 5);
                            Información_Contextual.SetToolTip(Combo, "Error block number " + (Índice_Bloque + 1).ToString() + ".\r\nOn errors read the file...");
                        }
                    }
                }
                Matriz_Paneles = null;
                Picture_1.Image = new Bitmap(768, 6, PixelFormat.Format24bppRgb);
                Picture_2.Image = new Bitmap(768, 6, PixelFormat.Format24bppRgb);
                Picture_3.Image = new Bitmap(768, 6, PixelFormat.Format24bppRgb);
                Picture_4.Image = new Bitmap(768, 6, PixelFormat.Format24bppRgb);
                Picture_5.Image = new Bitmap(768, 6, PixelFormat.Format24bppRgb);
                Picture.Image = new Bitmap(768, 34, PixelFormat.Format24bppRgb);
                Pintar_1 = Graphics.FromImage(Picture_1.Image);
                Pintar_2 = Graphics.FromImage(Picture_2.Image);
                Pintar_3 = Graphics.FromImage(Picture_3.Image);
                Pintar_4 = Graphics.FromImage(Picture_4.Image);
                Pintar_5 = Graphics.FromImage(Picture_5.Image);
                Pintar = Graphics.FromImage(Picture.Image);
                Pintar_1.Clear(Color.White);
                Pintar_2.Clear(Color.White);
                Pintar_3.Clear(Color.White);
                Pintar_4.Clear(Color.White);
                Pintar_5.Clear(Color.White);
                Pintar.Clear(Color.White);
                Picture_1.Invalidate();
                Picture_2.Invalidate();
                Picture_3.Invalidate();
                Picture_4.Invalidate();
                Picture_5.Invalidate();
                Picture.Invalidate();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Reconstructor_Archivos_Dañados_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Reconstructor_Archivos_Dañados_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Reconstructor_Archivos_Dañados_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Reconstructor_Archivos_Dañados_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Reconstructor_Archivos_Dañados_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
                {
                    string[] Matriz_Rutas = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                    if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                    {
                        int Elementos = CheckedListBox_Principal.Items.Count;
                        foreach (string Ruta in Matriz_Rutas)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                                {
                                    CheckedListBox_Principal.Items.Add(Ruta, true);
                                }
                            }
                            catch (Exception Excepción)
                            {
                                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                continue;
                            }
                        }
                        Matriz_Rutas = null;
                        if (CheckedListBox_Principal.Items.Count > Elementos)
                        {
                            Lista_CheckedListBox_Habilitados = new List<bool>();
                            Lista_CheckedListBox_Ordenada = new List<string>();
                            Lista_CheckedListBox_Rutas = new List<string>();
                            for (int Índice = 0; Índice < CheckedListBox_Principal.Items.Count; Índice++)
                            {
                                Lista_CheckedListBox_Habilitados.Add(CheckedListBox_Principal.GetItemChecked(Índice));
                                Lista_CheckedListBox_Ordenada.Add(CheckedListBox_Principal.Items[Índice] as string);
                                Lista_CheckedListBox_Rutas.Add(CheckedListBox_Principal.Items[Índice] as string);
                            }
                            Lista_CheckedListBox_Ordenada.Sort(new Comparador_Tamaños_Rutas());
                            CheckedListBox_Principal.Items.Clear();
                            for (int Índice = 0; Índice < Lista_CheckedListBox_Ordenada.Count; Índice++) CheckedListBox_Principal.Items.Add(Lista_CheckedListBox_Ordenada[Índice], Lista_CheckedListBox_Habilitados[Lista_CheckedListBox_Rutas.IndexOf(Lista_CheckedListBox_Ordenada[Índice])]);
                            Lista_CheckedListBox_Habilitados = null;
                            Lista_CheckedListBox_Ordenada = null;
                            Lista_CheckedListBox_Rutas = null;
                        }
                        else SystemSounds.Beep.Play();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Reconstructor_Archivos_Dañados_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Reconstructor_Archivos_Dañados_KeyDown(object sender, KeyEventArgs e)
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
                        if (Botón_Reconstruir.Enabled) Botón_Reconstruir_Click(Botón_Reconstruir, EventArgs.Empty);
                        else SystemSounds.Beep.Play();
                    }
                    else if (e.KeyCode == Keys.Delete)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        if (Botón_Reconstruir.Enabled)
                        {
                            this.Text = Texto_Título + " - [Please don't recombine different files as one]";
                            CheckedListBox_Principal.Items.Clear();
                            Pintar_1.Clear(Color.White);
                            Pintar_2.Clear(Color.White);
                            Pintar_3.Clear(Color.White);
                            Pintar_4.Clear(Color.White);
                            Pintar_5.Clear(Color.White);
                            Pintar.Clear(Color.White);
                            Picture_1.Invalidate();
                            Picture_2.Invalidate();
                            Picture_3.Invalidate();
                            Picture_4.Invalidate();
                            Picture_5.Invalidate();
                            Picture.Invalidate();
                            Información_Contextual.SetToolTip(Picture, "This picture represents the rebuilding progress.");
                            Etiqueta_Aguamarina.Text = "Perfect bits: 0,0000 %";
                            Etiqueta_Aguamarina_Tamaño.Text = "Size: 0 Bits";
                            Etiqueta_Azul.Text = "Correct bits: 0,0000 %";
                            Etiqueta_Azul_Tamaño.Text = "Size: 0 Bits";
                            Etiqueta_Rojo.Text = "Damaged bits: 0,0000 %";
                            Etiqueta_Rojo_Tamaño.Text = "Size: 0 Bits";
                            Etiqueta_Verde.Text = "Undefined bits: 0,0000 %";
                            Etiqueta_Verde_Tamaño.Text = "Size: 0 Bits";
                            Etiqueta_Blanco.Text = "Unread bits: 100,0000 %";
                            Etiqueta_Blanco_Tamaño.Text = "Size: 0 Bits";
                        }
                        else SystemSounds.Beep.Play();
                    }
                    else if (e.KeyCode == Keys.Back)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        if (Botón_Reconstruir.Enabled) for (int Índice = 0; Índice < CheckedListBox_Principal.Items.Count; Índice++) CheckedListBox_Principal.SetItemChecked(Índice, true);
                        else SystemSounds.Beep.Play();
                    }
                    else if (e.KeyCode == Keys.Insert)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        if (Botón_Reconstruir.Enabled) for (int Índice = 0; Índice < CheckedListBox_Principal.Items.Count; Índice++) CheckedListBox_Principal.SetItemChecked(Índice, !CheckedListBox_Principal.GetItemChecked(Índice));
                        else SystemSounds.Beep.Play();
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

        private void CheckedListBox_Principal_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                CheckedListBox_Principal.SelectedIndex = -1;
                if (Botón_Reconstruir.Enabled)
                {
                    Botón_Reconstruir.Select();
                    Botón_Reconstruir.Focus();
                }
            }
        }

        private void Botón_Reconstruir_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> Lista_Rutas = new List<string>();
                int Rutas_Ignoradas = 0;
                for (int Índice = 0; Índice < CheckedListBox_Principal.Items.Count; Índice++)
                {
                    if (CheckedListBox_Principal.GetItemChecked(Índice))
                    {
                        if (Lista_Rutas.Count < 255) Lista_Rutas.Add(CheckedListBox_Principal.Items[Índice] as String);
                        else Rutas_Ignoradas++;
                    }
                }
                if (Lista_Rutas.Count > 0)
                {
                    Botón_Reconstruir.Enabled = false;
                    CheckedListBox_Principal.Enabled = false;
                    this.Text = Texto_Título + " - [Please don't recombine different files as one]";
                    Pintar_1.Clear(Color.White);
                    Pintar_2.Clear(Lista_Rutas.Count > 1 ? Color.White : Color.Gray);
                    Pintar_3.Clear(Lista_Rutas.Count > 2 ? Color.White : Color.Gray);
                    Pintar_4.Clear(Lista_Rutas.Count > 3 ? Color.White : Color.Gray);
                    Pintar_5.Clear(Lista_Rutas.Count > 4 ? Color.White : Color.Gray);
                    Pintar.Clear(Color.White);
                    Picture_1.Invalidate();
                    Picture_2.Invalidate();
                    Picture_3.Invalidate();
                    Picture_4.Invalidate();
                    Picture_5.Invalidate();
                    Picture.Invalidate();
                    Información_Contextual.SetToolTip(Picture, "This picture represents the rebuilding progress.");
                    Etiqueta_Aguamarina.Text = "Perfect bits: 0,0000 %";
                    Etiqueta_Aguamarina_Tamaño.Text = "Size: 0 Bits";
                    Etiqueta_Azul.Text = "Correct bits: 0,0000 %";
                    Etiqueta_Azul_Tamaño.Text = "Size: 0 Bits";
                    Etiqueta_Rojo.Text = "Damaged bits: 0,0000 %";
                    Etiqueta_Rojo_Tamaño.Text = "Size: 0 Bits";
                    Etiqueta_Verde.Text = "Undefined bits: 0,0000 %";
                    Etiqueta_Verde_Tamaño.Text = "Size: 0 Bits";
                    Etiqueta_Blanco.Text = "Unread bits: 100,0000 %";
                    Etiqueta_Blanco_Tamaño.Text = "Size: 0 Bits";
                    if (Rutas_Ignoradas > 0) MessageBox.Show(this, "This tool can't read more than 255 files at once.\r\nIgnored files in the rebuilding process: " + Program.Traducir_Número(Rutas_Ignoradas) + ".", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Subproceso = new Thread(new ParameterizedThreadStart(Subproceso_DoWork));
                    Subproceso.IsBackground = true;
                    Subproceso.Priority = ThreadPriority.BelowNormal;
                    Subproceso.Start(Lista_Rutas);
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción)
            {
                CheckedListBox_Principal.Enabled = true;
                Botón_Reconstruir.Enabled = true;
                Application.OnThreadException(Excepción);
            }
        }

        private void Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox Combo = sender as ComboBox;
            if (Combo != null)
            {
                Matriz_Booleans_Errores[int.Parse(Combo.Name.Replace("Combo_Bloque_", null))] = Combo.SelectedIndex != 1 ? false : true;
                Combo.RightToLeft = Combo.SelectedIndex != 1 ? RightToLeft.No : RightToLeft.Yes;
            }
        }

        private void Combos_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                ComboBox Combo = sender as ComboBox;
                if (Combo != null)
                {
                    Combo.SelectedIndex = Combo.SelectedIndex == 1 ? 0 : 1;
                }
            }
        }

        internal void Subproceso_DoWork(object Objeto)
        {
            FileStream Lector = null;
            try
            {
                this.Invoke(new Invocación.Delegado_Control_Cursor(Invocación.Ejecutar_Delegado_Control_Cursor), new Object[] { this, Cursors.WaitCursor });
                List<string> Lista_Rutas = Objeto as List<string>;
                if (Lista_Rutas != null && Lista_Rutas.Count > 0)
                {
                    SortedDictionary<string, byte> Diccionario_Extensiones = new SortedDictionary<string, byte>();
                    List<FileStream> Lista_Lectores = new List<FileStream>();
                    long Longitud_Total_Bits = 0L, Longitud_Total_Bytes = 0L;
                    for (int Índice = 0; Índice < Lista_Rutas.Count; Índice++)
                    {
                        string Extensión = Path.GetExtension(Lista_Rutas[Índice]).ToLowerInvariant();
                        if (!Diccionario_Extensiones.ContainsKey(Extensión)) Diccionario_Extensiones.Add(Extensión, 1);
                        else Diccionario_Extensiones[Extensión]++;
                        FileStream Lector_Lista = new FileStream(Lista_Rutas[Índice], FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        if (Lector_Lista.Length > 0L)
                        {
                            if (Lector_Lista.Length > Longitud_Total_Bytes) Longitud_Total_Bytes = Lector_Lista.Length;
                            Lista_Lectores.Add(Lector_Lista);
                        }
                        else { Lector_Lista.Close(); Lector_Lista.Dispose(); }
                    }
                    if (Lista_Lectores.Count > 0)
                    {
                        if (Diccionario_Extensiones.Count <= 0) Diccionario_Extensiones.Add(string.Empty, 1);
                        KeyValuePair<string, byte>[] Matriz_Extensiones = new KeyValuePair<string, byte>[Diccionario_Extensiones.Count];
                        Diccionario_Extensiones.CopyTo(Matriz_Extensiones, 0);
                        if (Matriz_Extensiones.Length > 1) Array.Sort(Matriz_Extensiones, new Comparador_Extensiones());
                        Lector = new FileStream(Program.Obtener_Ruta_Temporal_Escritorio() + Matriz_Extensiones[0].Key, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite);
                        Lector.SetLength(Longitud_Total_Bytes);
                        Lector.Seek(0L, SeekOrigin.Begin);
                        string Texto_Tamaño = Program.Traducir_Tamaño_Bytes(Longitud_Total_Bytes, 4, true);
                        Longitud_Total_Bits = Longitud_Total_Bytes * 8L;
                        Picture.Invoke(new Invocación.Delegado_ToolTip_SetToolTip(Invocación.Ejecutar_Delegado_ToolTip_SetToolTip), new object[] { Información_Contextual, Picture, "This picture represents the rebuilding progress.\r\nA pixel represents " + Program.Traducir_Tamaño_Bits((double)Longitud_Total_Bits / 768d, 4, true) + " of " + Texto_Tamaño + "." });
                        List<int> Lista_Longitudes = new List<int>(new int[Lista_Lectores.Count]);
                        List<byte[]> Lista_Matrices = new List<byte[]>(new byte[Lista_Lectores.Count][]);
                        long Bits_Aguamarina = 0L, Bits_Azules = 0L, Bits_Rojos = 0L, Bits_Verdes = 0L, Bits_Blancos = Longitud_Total_Bits;
                        long Índice_Error_Anterior = -1L, Longitud_Bloque_Errores_Bits = 32768L;
                        byte Índice_Bloque_Erorres = 0/*, Píxel_Actual_Ambiguo = 2*/;
                        //int Píxel_Anterior_Anterior = 0, Píxel_Anterior = 0;

                        bool[] Matriz_Triple_Archivos = new bool[3] { Lista_Lectores.Count > 0, Lista_Lectores.Count > 1, Lista_Lectores.Count > 2 };
                        bool[] Matriz_Triple_Estados_Error = new bool[3] { false, false, false };
                        List<int> Lista_Triple_Índices = new List<int>(new int[3] { 0, 1, 2 });
                        PictureBox[] Matriz_Triple_Pictures = new PictureBox[3] { Picture_1, Picture_2, Picture_3 };
                        Graphics[] Matriz_Triple_Pintar = new Graphics[3] { Pintar_1, Pintar_2, Pintar_3 };
                        //int Píxel_Anterior_Anterior_1 = 0, Píxel_Anterior_1 = 0;
                        //int Píxel_Anterior_Anterior_2 = 0, Píxel_Anterior_2 = 0;
                        //int Píxel_Anterior_Anterior_3 = 0, Píxel_Anterior_3 = 0;

                        long Índice_Total_Bits = 0L, Índice_Total_Bytes = 0L;
                        long Cronómetro_Milisegundos_Anterior = 0L, Cronómetro_Milisegundos = 0L;
                        Stopwatch Cronómetro = Stopwatch.StartNew();

                        bool Dibujar_Archivo_2 = Lista_Lectores.Count < 2 ? false : true, Dibujar_Archivo_3 = Lista_Lectores.Count < 3 ? false : true, Dibujar_Archivo_4 = Lista_Lectores.Count < 4 ? false : true, Dibujar_Archivo_5 = Lista_Lectores.Count < 5 ? false : true;
                        bool Actualizar_Picture_1 = false, Actualizar_Picture_2 = false, Actualizar_Picture_3 = false, Actualizar_Picture_4 = false, Actualizar_Picture_5 = false, Actualizar_Picture = false;
                        bool Píxel_Rojo_1 = false, Píxel_Rojo_2 = false, Píxel_Rojo_3 = false, Píxel_Rojo_4 = false, Píxel_Rojo_5 = false, Píxel_Rojo = false, Píxel_Azul = false;
                        int Píxel_Índice_Siguiente = -1;
                        //int Píxel_Índice_Anterior = -1;
                        int Píxel_Ancho = Math.Max((int)(768L / Longitud_Total_Bits), 1);
                        int Píxeles_Totales = 768 / Píxel_Ancho;
                        long[] Matriz_Píxeles_Bits_Índices = new long[Píxeles_Totales];
                        int[] Matriz_Píxeles_Índices = new int[Píxeles_Totales];
                        for (long Índice = 0L; Índice < Matriz_Píxeles_Índices.Length; Índice++)
                        {
                            Matriz_Píxeles_Bits_Índices[Índice] = (Índice * Longitud_Total_Bits) / (long)Píxeles_Totales;
                            Matriz_Píxeles_Índices[Índice] = (int)((Índice * 768L) / (long)Píxeles_Totales);
                        }
                        //Matriz_Píxeles_Índices[Matriz_Píxeles_Índices.Length - 1] = long.MaxValue;

                        /*if (Píxel_Índice_Siguiente < Matriz_Píxeles_Índices.Length && Índice_Total_Bits >= Matriz_Píxeles_Índices[Píxel_Índice_Siguiente])
                        {
                            Pintar.FillRectangle(Brushes.Cyan, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                            Picture.Invoke(new Invocación.Delegado_Control_Invalidate(Invocación.Ejecutar_Delegado_Control_Invalidate), new Object[] { Picture });

                            //Píxel_Índice_Anterior = Píxel_Índice_Actual;
                            Píxel_Rojo = false;
                            Píxel_Azul = false;
                            Píxel_Índice_Siguiente++;
                        }*/
                        //MessageBox.Show(Matriz_Píxeles_Índices.Length.ToString() + ", " + Píxel_Ancho.ToString());
                        //return;

                        for (; Índice_Total_Bytes < Longitud_Total_Bytes; Índice_Total_Bytes += 4096L)
                        {
                            if (Lista_Lectores.Count > 1)
                            {
                                int Longitud_Bloque = 0;
                                for (int Índice_Lector = Lista_Lectores.Count - 1; Índice_Lector >= 0; Índice_Lector--)
                                {
                                    byte[] Matriz_Bytes = new byte[4096];
                                    int Longitud = Lista_Lectores[Índice_Lector].Read(Matriz_Bytes, 0, 4096);
                                    if (Longitud > 0)
                                    {
                                        if (Longitud > Longitud_Bloque) Longitud_Bloque = Longitud;
                                        Lista_Longitudes[Índice_Lector] = Longitud;
                                        Lista_Matrices[Índice_Lector] = Matriz_Bytes;
                                    }
                                    else
                                    {
                                        if (Índice_Lector == 1)
                                        {
                                            Dibujar_Archivo_2 = false;
                                            Pintar_2.FillRectangle(Brushes.Gray, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, 768, 6);
                                            Actualizar_Picture_2 = true;
                                        }
                                        else if (Índice_Lector == 2)
                                        {
                                            Dibujar_Archivo_3 = false;
                                            Pintar_3.FillRectangle(Brushes.Gray, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, 768, 6);
                                            Actualizar_Picture_3 = true;
                                        }
                                        else if (Índice_Lector == 3)
                                        {
                                            Dibujar_Archivo_4 = false;
                                            Pintar_4.FillRectangle(Brushes.Gray, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, 768, 6);
                                            Actualizar_Picture_4 = true;
                                        }
                                        else if (Índice_Lector == 4)
                                        {
                                            Dibujar_Archivo_5 = false;
                                            Pintar_5.FillRectangle(Brushes.Gray, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, 768, 6);
                                            Actualizar_Picture_5 = true;
                                        }
                                        Lista_Lectores[Índice_Lector].Close();
                                        Lista_Lectores[Índice_Lector].Dispose();
                                        Lista_Lectores.RemoveAt(Índice_Lector);
                                        Lista_Longitudes.RemoveAt(Índice_Lector);
                                        Lista_Matrices.RemoveAt(Índice_Lector);
                                    }
                                }
                                if (Longitud_Bloque > 0)
                                {
                                    byte[] Matriz_Bytes = new byte[Longitud_Bloque];
                                    for (int Índice_Byte = 0; Índice_Byte < Longitud_Bloque; Índice_Byte++)
                                    {
                                        for (int Índice_Bit = 0; Índice_Bit < 8; Índice_Bit++, Índice_Total_Bits++)
                                        {
                                            if (Píxel_Índice_Siguiente + 1 < Píxeles_Totales && Índice_Total_Bits >= Matriz_Píxeles_Bits_Índices[Píxel_Índice_Siguiente + 1])
                                            {
                                                Píxel_Índice_Siguiente++;

                                                Pintar_1.FillRectangle(Brushes.Cyan, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                                Actualizar_Picture_1 = true;
                                                if (Dibujar_Archivo_2)
                                                {
                                                    Pintar_2.FillRectangle(Brushes.Cyan, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                                    Actualizar_Picture_2 = true;
                                                }
                                                if (Dibujar_Archivo_3)
                                                {
                                                    Pintar_3.FillRectangle(Brushes.Cyan, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                                    Actualizar_Picture_3 = true;
                                                }
                                                if (Dibujar_Archivo_4)
                                                {
                                                    Pintar_4.FillRectangle(Brushes.Cyan, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                                    Actualizar_Picture_4 = true;
                                                }
                                                if (Dibujar_Archivo_5)
                                                {
                                                    Pintar_5.FillRectangle(Brushes.Cyan, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                                    Actualizar_Picture_5 = true;
                                                }
                                                Pintar.FillRectangle(Brushes.Cyan, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 34);
                                                Actualizar_Picture = true;

                                                Píxel_Rojo_1 = false; Píxel_Rojo_2 = false; Píxel_Rojo_3 = false; Píxel_Rojo_4 = false; Píxel_Rojo_5 = false; Píxel_Rojo = false; Píxel_Azul = false;
                                                //MessageBox.Show(Índice_Total_Bits.ToString(), "Píxel:");
                                            }

                                            //MessageBox.Show(Índice_Total_Bits.ToString() + ", " + Píxel_Índice_Siguiente.ToString());

                                            //Índice_Total_Bits++;
                                            byte Bits_False = 0, Bits_True = 0, Byte_Correcto = 0;

                                            for (int Índice_Triple = 0; Índice_Triple < 3; Índice_Triple++)
                                            {
                                                if (Matriz_Triple_Archivos[Índice_Triple])
                                                {
                                                    Matriz_Triple_Estados_Error[Índice_Triple] = false;
                                                }
                                            }

                                            for (int Índice_Lector = 0; Índice_Lector < Lista_Lectores.Count; Índice_Lector++)
                                            {
                                                if (Índice_Byte < Lista_Longitudes[Índice_Lector])
                                                {
                                                    if ((Lista_Matrices[Índice_Lector][Índice_Byte] & Program.Matriz_Potencias_Base_2[Índice_Bit]) == 0) Bits_False++;
                                                    else Bits_True++;
                                                }
                                            }

                                            if (Bits_False == Bits_True)
                                            {
                                                if (Índice_Error_Anterior > -1L && Índice_Total_Bits - Índice_Error_Anterior >= Longitud_Bloque_Errores_Bits)
                                                {
                                                    Índice_Bloque_Erorres++;
                                                    if (Índice_Bloque_Erorres >= 100) Índice_Bloque_Erorres = 0;
                                                }
                                                Índice_Error_Anterior = Índice_Total_Bits;
                                                Byte_Correcto = (byte)(Lista_Matrices[!Matriz_Booleans_Errores[Índice_Bloque_Erorres] ? 0 : 1][Índice_Byte] & Program.Matriz_Potencias_Base_2[Índice_Bit]);
                                                Matriz_Bytes[Índice_Byte] += Byte_Correcto;
                                                
                                                if (!Píxel_Rojo_1 && (Lista_Matrices[0][Índice_Byte] & Program.Matriz_Potencias_Base_2[Índice_Bit]) != Byte_Correcto)
                                                {
                                                    Pintar_1.FillRectangle(Brushes.Red, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                                    Actualizar_Picture_1 = true;
                                                    Píxel_Rojo_1 = true;
                                                }
                                                if (Dibujar_Archivo_2 && !Píxel_Rojo_2 && (Lista_Matrices[1][Índice_Byte] & Program.Matriz_Potencias_Base_2[Índice_Bit]) != Byte_Correcto)
                                                {
                                                    Pintar_2.FillRectangle(Brushes.Red, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                                    Actualizar_Picture_2 = true;
                                                    Píxel_Rojo_2 = true;
                                                }
                                                if (Dibujar_Archivo_3 && !Píxel_Rojo_3 && (Lista_Matrices[2][Índice_Byte] & Program.Matriz_Potencias_Base_2[Índice_Bit]) != Byte_Correcto)
                                                {
                                                    Pintar_3.FillRectangle(Brushes.Red, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                                    Actualizar_Picture_3 = true;
                                                    Píxel_Rojo_3 = true;
                                                }
                                                if (Dibujar_Archivo_4 && !Píxel_Rojo_4 && (Lista_Matrices[3][Índice_Byte] & Program.Matriz_Potencias_Base_2[Índice_Bit]) != Byte_Correcto)
                                                {
                                                    Pintar_4.FillRectangle(Brushes.Red, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                                    Actualizar_Picture_4 = true;
                                                    Píxel_Rojo_4 = true;
                                                }
                                                if (Dibujar_Archivo_5 && !Píxel_Rojo_5 && (Lista_Matrices[4][Índice_Byte] & Program.Matriz_Potencias_Base_2[Índice_Bit]) != Byte_Correcto)
                                                {
                                                    Pintar_5.FillRectangle(Brushes.Red, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                                    Actualizar_Picture_5 = true;
                                                    Píxel_Rojo_5 = true;
                                                }
                                                if (!Píxel_Rojo)
                                                {
                                                    Pintar.FillRectangle(Brushes.Red, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 34);
                                                    Actualizar_Picture = true;
                                                    Píxel_Rojo = true;
                                                }
                                                Bits_Rojos++;
                                            }
                                            else if (Bits_False > Bits_True)
                                            {
                                                if (Bits_True == 0) Bits_Aguamarina++;
                                                else
                                                {
                                                    if (!Píxel_Rojo_1 && (Lista_Matrices[0][Índice_Byte] & Program.Matriz_Potencias_Base_2[Índice_Bit]) != Byte_Correcto)
                                                    {
                                                        Pintar_1.FillRectangle(Brushes.Red, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                                        Actualizar_Picture_1 = true;
                                                        Píxel_Rojo_1 = true;
                                                    }
                                                    if (Dibujar_Archivo_2 && !Píxel_Rojo_2 && (Lista_Matrices[1][Índice_Byte] & Program.Matriz_Potencias_Base_2[Índice_Bit]) != Byte_Correcto)
                                                    {
                                                        Pintar_2.FillRectangle(Brushes.Red, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                                        Actualizar_Picture_2 = true;
                                                        Píxel_Rojo_2 = true;
                                                    }
                                                    if (Dibujar_Archivo_3 && !Píxel_Rojo_3 && (Lista_Matrices[2][Índice_Byte] & Program.Matriz_Potencias_Base_2[Índice_Bit]) != Byte_Correcto)
                                                    {
                                                        Pintar_3.FillRectangle(Brushes.Red, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                                        Actualizar_Picture_3 = true;
                                                        Píxel_Rojo_3 = true;
                                                    }
                                                    if (Dibujar_Archivo_4 && !Píxel_Rojo_4 && (Lista_Matrices[3][Índice_Byte] & Program.Matriz_Potencias_Base_2[Índice_Bit]) != Byte_Correcto)
                                                    {
                                                        Pintar_4.FillRectangle(Brushes.Red, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                                        Actualizar_Picture_4 = true;
                                                        Píxel_Rojo_4 = true;
                                                    }
                                                    if (Dibujar_Archivo_5 && !Píxel_Rojo_5 && (Lista_Matrices[4][Índice_Byte] & Program.Matriz_Potencias_Base_2[Índice_Bit]) != Byte_Correcto)
                                                    {
                                                        Pintar_5.FillRectangle(Brushes.Red, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                                        Actualizar_Picture_5 = true;
                                                        Píxel_Rojo_5 = true;
                                                    }
                                                    if (!Píxel_Rojo && !Píxel_Azul)
                                                    {
                                                        Pintar.FillRectangle(Brushes.Blue, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 34);
                                                        Actualizar_Picture = true;
                                                        Píxel_Azul = true;
                                                    }
                                                    Bits_Azules++;
                                                }
                                            }
                                            else if (Bits_True > Bits_False)
                                            {
                                                Byte_Correcto = Program.Matriz_Potencias_Base_2[Índice_Bit];
                                                Matriz_Bytes[Índice_Byte] += Byte_Correcto;
                                                if (Bits_False == 0) Bits_Aguamarina++;
                                                else
                                                {
                                                    if (!Píxel_Rojo_1 && (Lista_Matrices[0][Índice_Byte] & Program.Matriz_Potencias_Base_2[Índice_Bit]) != Byte_Correcto)
                                                    {
                                                        Pintar_1.FillRectangle(Brushes.Red, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                                        Actualizar_Picture_1 = true;
                                                        Píxel_Rojo_1 = true;
                                                    }
                                                    if (Dibujar_Archivo_2 && !Píxel_Rojo_2 && (Lista_Matrices[1][Índice_Byte] & Program.Matriz_Potencias_Base_2[Índice_Bit]) != Byte_Correcto)
                                                    {
                                                        Pintar_2.FillRectangle(Brushes.Red, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                                        Actualizar_Picture_2 = true;
                                                        Píxel_Rojo_2 = true;
                                                    }
                                                    if (Dibujar_Archivo_3 && !Píxel_Rojo_3 && (Lista_Matrices[2][Índice_Byte] & Program.Matriz_Potencias_Base_2[Índice_Bit]) != Byte_Correcto)
                                                    {
                                                        Pintar_3.FillRectangle(Brushes.Red, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                                        Actualizar_Picture_3 = true;
                                                        Píxel_Rojo_3 = true;
                                                    }
                                                    if (Dibujar_Archivo_4 && !Píxel_Rojo_4 && (Lista_Matrices[3][Índice_Byte] & Program.Matriz_Potencias_Base_2[Índice_Bit]) != Byte_Correcto)
                                                    {
                                                        Pintar_4.FillRectangle(Brushes.Red, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                                        Actualizar_Picture_4 = true;
                                                        Píxel_Rojo_4 = true;
                                                    }
                                                    if (Dibujar_Archivo_5 && !Píxel_Rojo_5 && (Lista_Matrices[4][Índice_Byte] & Program.Matriz_Potencias_Base_2[Índice_Bit]) != Byte_Correcto)
                                                    {
                                                        Pintar_5.FillRectangle(Brushes.Red, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                                        Actualizar_Picture_5 = true;
                                                        Píxel_Rojo_5 = true;
                                                    }
                                                    if (!Píxel_Rojo && !Píxel_Azul)
                                                    {
                                                        Pintar.FillRectangle(Brushes.Blue, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 34);
                                                        Actualizar_Picture = true;
                                                        Píxel_Azul = true;
                                                    }
                                                    Bits_Azules++;
                                                }
                                            }
                                            Bits_Blancos--;
                                        }
                                    }
                                    Lector.Write(Matriz_Bytes, 0, Longitud_Bloque);
                                }
                            }
                            else if (Lista_Lectores.Count > 0)
                            {
                                byte[] Matriz_Bytes = new byte[4096];
                                int Longitud_Bytes = Lista_Lectores[0].Read(Matriz_Bytes, 0, 4096);
                                if (Longitud_Bytes > 0)
                                {
                                    Lector.Write(Matriz_Bytes, 0, Longitud_Bytes);
                                    int Longitud_Bits = Longitud_Bytes * 8;
                                    Bits_Verdes += Longitud_Bits;
                                    Bits_Blancos -= Longitud_Bits;
                                    for (int Índice_Bit = 0; Índice_Bit < Longitud_Bits; Índice_Bit++, Índice_Total_Bits++)
                                    {
                                        if (Píxel_Índice_Siguiente + 1 < Píxeles_Totales && Índice_Total_Bits >= Matriz_Píxeles_Bits_Índices[Píxel_Índice_Siguiente + 1])
                                        {
                                            Píxel_Índice_Siguiente++;
                                            Pintar_1.FillRectangle(Brushes.Lime, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 6);
                                            Actualizar_Picture_1 = true;
                                            Pintar.FillRectangle(Brushes.Lime, Matriz_Píxeles_Índices[Píxel_Índice_Siguiente], 0, Píxel_Ancho, 34);
                                            Actualizar_Picture = true;
                                        }
                                    }
                                }
                                else break;
                            }
                            else
                            {
                                MessageBox.Show(this, "0 readers.");
                                break;
                            }
                            Cronómetro_Milisegundos = Cronómetro.ElapsedMilliseconds;
                            if (Cronómetro_Milisegundos - Cronómetro_Milisegundos_Anterior >= 100L)
                            {
                                Cronómetro_Milisegundos_Anterior = Cronómetro_Milisegundos;
                                this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [Rebuild at " + Program.Traducir_Número_Decimales(Math.Round(((double)Índice_Total_Bits * 100d) / (double)Longitud_Total_Bits, 4, MidpointRounding.AwayFromZero), 4) + " % (" + Program.Traducir_Tamaño_Bits(Índice_Total_Bits, 4, true) + " of " + Texto_Tamaño + ")]" });
                                if (Actualizar_Picture_1)
                                {
                                    Picture_1.Invoke(new Invocación.Delegado_Control_Invalidate(Invocación.Ejecutar_Delegado_Control_Invalidate), new object[] { Picture_1 });
                                    Actualizar_Picture_1 = false;
                                }
                                if (Actualizar_Picture_2)
                                {
                                    Picture_2.Invoke(new Invocación.Delegado_Control_Invalidate(Invocación.Ejecutar_Delegado_Control_Invalidate), new object[] { Picture_2 });
                                    Actualizar_Picture_2 = false;
                                }
                                if (Actualizar_Picture_3)
                                {
                                    Picture_3.Invoke(new Invocación.Delegado_Control_Invalidate(Invocación.Ejecutar_Delegado_Control_Invalidate), new object[] { Picture_3 });
                                    Actualizar_Picture_3 = false;
                                }
                                if (Actualizar_Picture_4)
                                {
                                    Picture_4.Invoke(new Invocación.Delegado_Control_Invalidate(Invocación.Ejecutar_Delegado_Control_Invalidate), new object[] { Picture_4 });
                                    Actualizar_Picture_4 = false;
                                }
                                if (Actualizar_Picture_5)
                                {
                                    Picture_5.Invoke(new Invocación.Delegado_Control_Invalidate(Invocación.Ejecutar_Delegado_Control_Invalidate), new object[] { Picture_5 });
                                    Actualizar_Picture_5 = false;
                                }
                                if (Actualizar_Picture)
                                {
                                    Picture.Invoke(new Invocación.Delegado_Control_Invalidate(Invocación.Ejecutar_Delegado_Control_Invalidate), new object[] { Picture });
                                    Actualizar_Picture = false;
                                }
                                Etiqueta_Aguamarina.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { Etiqueta_Aguamarina, "Perfect bits: " + Program.Traducir_Número_Decimales(Math.Round(((double)Bits_Aguamarina * 100d) / (double)Longitud_Total_Bits, 4, MidpointRounding.AwayFromZero), 4) + " %" });
                                Etiqueta_Aguamarina_Tamaño.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { Etiqueta_Aguamarina_Tamaño, "Size: " + Program.Traducir_Tamaño_Bits(Bits_Aguamarina, 4, true) });
                                Etiqueta_Azul.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { Etiqueta_Azul, "Correct bits: " + Program.Traducir_Número_Decimales(Math.Round(((double)Bits_Azules * 100d) / (double)Longitud_Total_Bits, 4, MidpointRounding.AwayFromZero), 4) + " %" });
                                Etiqueta_Azul_Tamaño.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { Etiqueta_Azul_Tamaño, "Size: " + Program.Traducir_Tamaño_Bits(Bits_Azules, 4, true) });
                                Etiqueta_Rojo.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { Etiqueta_Rojo, "Damaged bits: " + Program.Traducir_Número_Decimales(Math.Round(((double)Bits_Rojos * 100d) / (double)Longitud_Total_Bits, 4, MidpointRounding.AwayFromZero), 4) + " %" });
                                Etiqueta_Rojo_Tamaño.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { Etiqueta_Rojo_Tamaño, "Size: " + Program.Traducir_Tamaño_Bits(Bits_Rojos, 4, true) });
                                Etiqueta_Verde.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { Etiqueta_Verde, "Undefined bits: " + Program.Traducir_Número_Decimales(Math.Round(((double)Bits_Verdes * 100d) / (double)Longitud_Total_Bits, 4, MidpointRounding.AwayFromZero), 4) + " %" });
                                Etiqueta_Verde_Tamaño.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { Etiqueta_Verde_Tamaño, "Size: " + Program.Traducir_Tamaño_Bits(Bits_Verdes, 4, true) });
                                Etiqueta_Blanco.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { Etiqueta_Blanco, "Unread bits: " + Program.Traducir_Número_Decimales(Math.Round(((double)Bits_Blancos * 100d) / (double)Longitud_Total_Bits, 4, MidpointRounding.AwayFromZero), 4) + " %" });
                                Etiqueta_Blanco_Tamaño.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { Etiqueta_Blanco_Tamaño, "Size: " + Program.Traducir_Tamaño_Bits(Bits_Blancos, 4, true) });
                            }
                        }
                        Cronómetro.Stop();
                        for (int Índice_Lector = Lista_Lectores.Count - 1; Índice_Lector >= 0; Índice_Lector--)
                        {
                            Lista_Lectores[Índice_Lector].Close();
                            Lista_Lectores[Índice_Lector].Dispose();
                            Lista_Lectores.RemoveAt(Índice_Lector);
                            Lista_Longitudes.RemoveAt(Índice_Lector);
                            Lista_Matrices.RemoveAt(Índice_Lector);
                        }
                        this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + "- [Rebuilt at 100,0000 % (" + Texto_Tamaño + " of " + Texto_Tamaño + ")]" });
                        Picture_1.Invoke(new Invocación.Delegado_Control_Invalidate(Invocación.Ejecutar_Delegado_Control_Invalidate), new object[] { Picture_1 });
                        Picture_2.Invoke(new Invocación.Delegado_Control_Invalidate(Invocación.Ejecutar_Delegado_Control_Invalidate), new object[] { Picture_2 });
                        Picture_3.Invoke(new Invocación.Delegado_Control_Invalidate(Invocación.Ejecutar_Delegado_Control_Invalidate), new object[] { Picture_3 });
                        Picture_4.Invoke(new Invocación.Delegado_Control_Invalidate(Invocación.Ejecutar_Delegado_Control_Invalidate), new object[] { Picture_4 });
                        Picture_5.Invoke(new Invocación.Delegado_Control_Invalidate(Invocación.Ejecutar_Delegado_Control_Invalidate), new object[] { Picture_5 });
                        Picture.Invoke(new Invocación.Delegado_Control_Invalidate(Invocación.Ejecutar_Delegado_Control_Invalidate), new object[] { Picture });
                        Etiqueta_Aguamarina.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { Etiqueta_Aguamarina, "Perfect bits: " + Program.Traducir_Número_Decimales(Math.Round(((double)Bits_Aguamarina * 100d) / (double)Longitud_Total_Bits, 4, MidpointRounding.AwayFromZero), 4) + " %" });
                        Etiqueta_Aguamarina_Tamaño.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { Etiqueta_Aguamarina_Tamaño, "Size: " + Program.Traducir_Tamaño_Bits(Bits_Aguamarina, 4, true) });
                        Etiqueta_Azul.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { Etiqueta_Azul, "Correct bits: " + Program.Traducir_Número_Decimales(Math.Round(((double)Bits_Azules * 100d) / (double)Longitud_Total_Bits, 4, MidpointRounding.AwayFromZero), 4) + " %" });
                        Etiqueta_Azul_Tamaño.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { Etiqueta_Azul_Tamaño, "Size: " + Program.Traducir_Tamaño_Bits(Bits_Azules, 4, true) });
                        Etiqueta_Rojo.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { Etiqueta_Rojo, "Damaged bits: " + Program.Traducir_Número_Decimales(Math.Round(((double)Bits_Rojos * 100d) / (double)Longitud_Total_Bits, 4, MidpointRounding.AwayFromZero), 4) + " %" });
                        Etiqueta_Rojo_Tamaño.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { Etiqueta_Rojo_Tamaño, "Size: " + Program.Traducir_Tamaño_Bits(Bits_Rojos, 4, true) });
                        Etiqueta_Verde.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { Etiqueta_Verde, "Undefined bits: " + Program.Traducir_Número_Decimales(Math.Round(((double)Bits_Verdes * 100d) / (double)Longitud_Total_Bits, 4, MidpointRounding.AwayFromZero), 4) + " %" });
                        Etiqueta_Verde_Tamaño.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { Etiqueta_Verde_Tamaño, "Size: " + Program.Traducir_Tamaño_Bits(Bits_Verdes, 4, true) });
                        Etiqueta_Blanco.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { Etiqueta_Blanco, "Unread bits: " + Program.Traducir_Número_Decimales(Math.Round(((double)Bits_Blancos * 100d) / (double)Longitud_Total_Bits, 4, MidpointRounding.AwayFromZero), 4) + " %" });
                        Etiqueta_Blanco_Tamaño.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { Etiqueta_Blanco_Tamaño, "Size: " + Program.Traducir_Tamaño_Bits(Bits_Blancos, 4, true) });
                        try { if (Lector != null) { Lector.Close(); Lector.Dispose(); } }
                        catch { }
                        this.Invoke(new Invocación.Delegado_IWin32Window_MessageBox(Invocación.Ejecutar_Delegado_IWin32Window_MessageBox), new object[] { this, "The rebuilding has finished apparently " + (Bits_Rojos <= 0 ? "with success" : "with errors") + ".\r\nTime and speed: " + Program.Traducir_Intervalo(Cronómetro.Elapsed, false, false, true) + " at " + Program.Traducir_Tamaño_Bits_Segundo(Longitud_Total_Bits, Cronómetro.Elapsed.TotalSeconds, 4, true) + ".\r\n\r\nYou'll find the rebuilt file on your desktop, and please be cautious with it since it might still contain errors. Although using this tool on damaged files due to bad internet connection always worked perfectly.", Program.Texto_Título_Versión + " - [" + Program.Traducir_Número_Decimales_Redondear(((double)(Bits_Aguamarina + Bits_Azules + Bits_Verdes) * 100d) / (double)Longitud_Total_Bits, 4) + " %]", MessageBoxButtons.OK, Bits_Rojos <= 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning });
                    }
                    else SystemSounds.Beep.Play();
                }
                this.Invoke(new Invocación.Delegado_Control_Cursor(Invocación.Ejecutar_Delegado_Control_Cursor), new object[] { this, Cursors.Default });
                CheckedListBox_Principal.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { CheckedListBox_Principal, true });
                Botón_Reconstruir.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { Botón_Reconstruir, true });
                Botón_Reconstruir.Invoke(new Invocación.Delegado_Control_Select(Invocación.Ejecutar_Delegado_Control_Select), new object[] { Botón_Reconstruir });
                Botón_Reconstruir.Invoke(new Invocación.Delegado_Control_Focus(Invocación.Ejecutar_Delegado_Control_Focus), new object[] { Botón_Reconstruir });
            }
            catch (ThreadAbortException) { try { if (Lector != null) { Lector.Close(); Lector.Dispose(); } } catch { } }
            catch (Exception Excepción)
            {
                try { if (Lector != null) { Lector.Close(); Lector.Dispose(); } } catch { }
                Application.OnThreadException(Excepción);
                this.Invoke(new Invocación.Delegado_Control_Cursor(Invocación.Ejecutar_Delegado_Control_Cursor), new object[] { this, Cursors.Default });
                CheckedListBox_Principal.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { CheckedListBox_Principal, true });
                Botón_Reconstruir.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { Botón_Reconstruir, true });
                Botón_Reconstruir.Invoke(new Invocación.Delegado_Control_Select(Invocación.Ejecutar_Delegado_Control_Select), new object[] { Botón_Reconstruir });
                Botón_Reconstruir.Invoke(new Invocación.Delegado_Control_Focus(Invocación.Ejecutar_Delegado_Control_Focus), new object[] { Botón_Reconstruir });
            }
        }
    }
}
