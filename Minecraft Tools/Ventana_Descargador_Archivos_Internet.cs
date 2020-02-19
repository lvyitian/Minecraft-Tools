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

namespace Minecraft_Tools
{
    public partial class Ventana_Descargador_Archivos_Internet : Form
    {
        public Ventana_Descargador_Archivos_Internet()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Online Files Downloader by Jupisoft for " + Program.Texto_Usuario;
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

        internal static readonly string Ruta_Descargas = Application.StartupPath + "\\Downloads";
        internal static string Variable_URL = null;
        internal static string Variable_Filtros = ".bmp;.dds;.jpg;.jpeg;.gif;.png;.tif;.tiff";
        internal WebBrowser Navegador_Web = null;
        internal List<string> Lista_Vínculos = new List<string>();
        internal SortedDictionary<string, string> Diccionario_Vínculos = new SortedDictionary<string, string>();
        internal int Vínculos_Válidos = 0;

        private void Ventana_Descargador_Archivos_Internet_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                ListView_Resultados.CheckBoxes = false;
                ListView_Resultados.Columns[0].Width = ListView_Resultados.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
                Navegador_Web = new WebBrowser();
                Navegador_Web.AllowNavigation = true;
                Navegador_Web.AllowWebBrowserDrop = false;
                Navegador_Web.IsWebBrowserContextMenuEnabled = true;
                Navegador_Web.ScriptErrorsSuppressed = true;
                Navegador_Web.ScrollBarsEnabled = true;
                Navegador_Web.WebBrowserShortcutsEnabled = false;
                Navegador_Web.DocumentCompleted += Navegador_Web_DocumentCompleted;
                Navegador_Web.Navigated += Navegador_Web_Navigated;
                Navegador_Web.Navigating += Navegador_Web_Navigating;
                Navegador_Web.NewWindow += Navegador_Web_NewWindow;
                Navegador_Web.ProgressChanged += Navegador_Web_ProgressChanged;
                TextBox_URL.Text = Variable_URL;
                TextBox_Filtros.Text = Variable_Filtros;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Descargador_Archivos_Internet_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Descargador_Archivos_Internet_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Descargador_Archivos_Internet_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Descargador_Archivos_Internet_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Descargador_Archivos_Internet_DragDrop(object sender, DragEventArgs e)
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
                                if (!string.IsNullOrEmpty(Ruta))
                                {
                                    TextBox_URL.Text = Ruta;
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

        private void Ventana_Descargador_Archivos_Internet_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Descargador_Archivos_Internet_KeyDown(object sender, KeyEventArgs e)
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
                Program.Crear_Carpetas(Ruta_Descargas);
                Program.Ejecutar_Ruta(Ruta_Descargas, ProcessWindowStyle.Maximized);
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

        private void TextBox_URL_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_URL = TextBox_URL.Text;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Buscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Variable_URL))
                {
                    Vínculos_Válidos = 0;
                    Lista_Vínculos.Clear();
                    Diccionario_Vínculos.Clear();
                    ListView_Resultados.Items.Clear();
                    Navegador_Web.Navigate(Variable_URL);
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_Filtros_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Filtros = TextBox_Filtros.Text;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Descargar_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ListView_Resultados_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    ListViewHitTestInfo Info = ListView_Resultados.HitTest(e.Location);
                    if (Info != null)
                    {
                        ListViewItem Objeto = Info.Item;
                        if (Objeto != null && !string.IsNullOrEmpty(Objeto.Text))
                        {
                            if (ListView_Resultados.SelectedIndices != null && ListView_Resultados.SelectedIndices.Count > 0)
                            {
                                foreach (int Índice_Objeto in ListView_Resultados.SelectedIndices)
                                {
                                    ListView_Resultados.Items[Índice_Objeto].Selected = false; // Deselect all.
                                }
                            }
                            Objeto.Selected = true; // Select current.
                            if (!Program.Ejecutar_Ruta(Objeto.Text, ProcessWindowStyle.Normal))
                            {
                                // Try the regular disk path format instead?
                                //Program.Ejecutar_Ruta(Objeto.Text.Replace('/', '\\'), ProcessWindowStyle.Normal);
                                //Clipboard.SetText(Objeto.Text.Replace('/', '\\'));
                            }
                            //else Clipboard.SetText(Objeto.Text);
                            //SystemSounds.Asterisk.Play();
                        }
                        else SystemSounds.Beep.Play();
                        Info = null;
                    }
                    else SystemSounds.Beep.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Navegador_Web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                string Ruta_Base = e.Url.ToString();
                if (!string.IsNullOrEmpty(Ruta_Base) && string.Compare(Ruta_Base, "about:blank", true) != 0)
                {
                    bool Entropía = Diccionario_Vínculos.Count <= 0; // Should add external links below?
                    try { Ruta_Base = Path.GetDirectoryName(Ruta_Base).Replace('\\', '/'); } // For "src=" internal links.
                    catch { }
                    HtmlDocument Documento = Navegador_Web.Document;
                    if (Documento.Images != null && Documento.Images.Count > 0)
                    {
                        foreach (HtmlElement Elemento in Documento.Images)
                        {
                            try
                            {
                                if (Elemento != null)
                                {
                                    string Texto = Elemento.OuterHtml;
                                    if (!string.IsNullOrEmpty(Texto))
                                    {
                                        List<int> Lista_Índices_HTML = new List<int>();
                                        for (int Índice_Caracter = 0; Índice_Caracter < Texto.Length;)
                                        {
                                            try
                                            {
                                                int Índice_HTML = Texto.IndexOf("href=", Índice_Caracter);
                                                if (Índice_HTML > -1)
                                                {
                                                    Lista_Índices_HTML.Add(Índice_HTML);
                                                    Índice_Caracter = Índice_HTML + 1;
                                                }
                                                else break;
                                            }
                                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                        }
                                        List<int> Lista_Índices_SRC = new List<int>();
                                        for (int Índice_Caracter = 0; Índice_Caracter < Texto.Length;)
                                        {
                                            try
                                            {
                                                int Índice_SRC = Texto.IndexOf("src=", Índice_Caracter);
                                                if (Índice_SRC > -1)
                                                {
                                                    Lista_Índices_SRC.Add(Índice_SRC);
                                                    Índice_Caracter = Índice_SRC + 1;
                                                }
                                                else break;
                                            }
                                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                        }
                                        if (Lista_Índices_HTML != null && Lista_Índices_HTML.Count > 0 && Entropía)
                                        {
                                            foreach (int Índice_HTML in Lista_Índices_HTML)
                                            {
                                                try
                                                {
                                                    for (int Índice_Caracter = Índice_HTML, Índice_Inicio = -1; Índice_Caracter < Texto.Length; Índice_Caracter++)
                                                    {
                                                        try
                                                        {
                                                            if (Texto[Índice_Caracter] == '\"')
                                                            {
                                                                if (Índice_Inicio < 0) Índice_Inicio = Índice_Caracter + 1;
                                                                else
                                                                {
                                                                    string Vínculo = new Uri(Texto.Substring(Índice_Inicio, Índice_Caracter - Índice_Inicio), UriKind.RelativeOrAbsolute).ToString().Replace('\\', '/');
                                                                    Lista_Vínculos.Add(Vínculo);
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                    }
                                                }
                                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                            }
                                        }
                                        if (Lista_Índices_SRC != null && Lista_Índices_SRC.Count > 0)
                                        {
                                            foreach (int Índice_SRC in Lista_Índices_SRC)
                                            {
                                                try
                                                {
                                                    for (int Índice_Caracter = Índice_SRC, Índice_Inicio = -1; Índice_Caracter < Texto.Length; Índice_Caracter++)
                                                    {
                                                        try
                                                        {
                                                            if (Texto[Índice_Caracter] == '\"')
                                                            {
                                                                if (Índice_Inicio < 0) Índice_Inicio = Índice_Caracter + 1;
                                                                else
                                                                {
                                                                    string Vínculo = Ruta_Base + "/" + new Uri(Texto.Substring(Índice_Inicio, Índice_Caracter - Índice_Inicio), UriKind.Relative).ToString().Replace('\\', '/');
                                                                    Lista_Vínculos.Add(Vínculo);
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                    }
                                                }
                                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                        }
                    }
                    if (Documento.Links != null && Documento.Links.Count > 0)
                    {
                        foreach (HtmlElement Elemento in Documento.Links)
                        {
                            try
                            {
                                if (Elemento != null)
                                {
                                    string Texto = Elemento.OuterHtml;
                                    if (!string.IsNullOrEmpty(Texto))
                                    {
                                        List<int> Lista_Índices_HTML = new List<int>();
                                        for (int Índice_Caracter = 0; Índice_Caracter < Texto.Length;)
                                        {
                                            try
                                            {
                                                int Índice_HTML = Texto.IndexOf("href=", Índice_Caracter);
                                                if (Índice_HTML > -1)
                                                {
                                                    Lista_Índices_HTML.Add(Índice_HTML);
                                                    Índice_Caracter = Índice_HTML + 1;
                                                }
                                                else break;
                                            }
                                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                        }
                                        List<int> Lista_Índices_SRC = new List<int>();
                                        for (int Índice_Caracter = 0; Índice_Caracter < Texto.Length;)
                                        {
                                            try
                                            {
                                                int Índice_SRC = Texto.IndexOf("src=", Índice_Caracter);
                                                if (Índice_SRC > -1)
                                                {
                                                    Lista_Índices_SRC.Add(Índice_SRC);
                                                    Índice_Caracter = Índice_SRC + 1;
                                                }
                                                else break;
                                            }
                                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                        }
                                        if (Lista_Índices_HTML != null && Lista_Índices_HTML.Count > 0 && Entropía)
                                        {
                                            foreach (int Índice_HTML in Lista_Índices_HTML)
                                            {
                                                try
                                                {
                                                    for (int Índice_Caracter = Índice_HTML, Índice_Inicio = -1; Índice_Caracter < Texto.Length; Índice_Caracter++)
                                                    {
                                                        try
                                                        {
                                                            if (Texto[Índice_Caracter] == '\"')
                                                            {
                                                                if (Índice_Inicio < 0) Índice_Inicio = Índice_Caracter + 1;
                                                                else
                                                                {
                                                                    string Vínculo = new Uri(Texto.Substring(Índice_Inicio, Índice_Caracter - Índice_Inicio), UriKind.RelativeOrAbsolute).ToString().Replace('\\', '/');
                                                                    Lista_Vínculos.Add(Vínculo);
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                    }
                                                }
                                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                            }
                                        }
                                        if (Lista_Índices_SRC != null && Lista_Índices_SRC.Count > 0)
                                        {
                                            foreach (int Índice_SRC in Lista_Índices_SRC)
                                            {
                                                try
                                                {
                                                    for (int Índice_Caracter = Índice_SRC, Índice_Inicio = -1; Índice_Caracter < Texto.Length; Índice_Caracter++)
                                                    {
                                                        try
                                                        {
                                                            if (Texto[Índice_Caracter] == '\"')
                                                            {
                                                                if (Índice_Inicio < 0) Índice_Inicio = Índice_Caracter + 1;
                                                                else
                                                                {
                                                                    string Vínculo = Ruta_Base + "/" + new Uri(Texto.Substring(Índice_Inicio, Índice_Caracter - Índice_Inicio), UriKind.Relative).ToString().Replace('\\', '/');
                                                                    Lista_Vínculos.Add(Vínculo);
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                    }
                                                }
                                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                        }
                        if (Lista_Vínculos != null && Lista_Vínculos.Count > 0)
                        {
                            //if (Lista_Vínculos.Count > 1) Lista_Vínculos.Sort();
                            foreach (string Vínculo in Lista_Vínculos)
                            {
                                try
                                {
                                    if (!Diccionario_Vínculos.ContainsKey(Vínculo))
                                    {
                                        Diccionario_Vínculos.Add(Vínculo, null);
                                        //ListViewItem Objeto = new ListViewItem(Vínculo);
                                        //Objeto.BackColor = Color.Lime;
                                        //ListView_Resultados.Items.Add(Objeto);
                                        Navegador_Web.Navigate(Vínculo);
                                    }
                                }
                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                            }
                        }
                        if (Entropía && Diccionario_Vínculos.Count > 0) // We are still on the first call.
                        {
                            //MessageBox.Show(this, "Done!");
                            string Nombre_Temporal = Program.Obtener_Nombre_Temporal();
                            string Ruta_Salida = Ruta_Descargas + "\\" + Nombre_Temporal;
                            Program.Crear_Carpetas(Ruta_Salida);
                            FileStream Lector_Descargas = new FileStream(Ruta_Descargas + "\\Downloads.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                            Lector_Descargas.Seek(Lector_Descargas.Length, SeekOrigin.Begin); // Append text.
                            StreamWriter Lector_Texto_Descargas = new StreamWriter(Lector_Descargas, Encoding.UTF8);
                            Lector_Texto_Descargas.WriteLine("[" + Nombre_Temporal + "] Links: " + Program.Traducir_Número(Diccionario_Vínculos.Count) + ".");
                            Lector_Texto_Descargas.WriteLine();
                            Lector_Texto_Descargas.Flush();
                            FileStream Lector_Vínculos = new FileStream(Ruta_Descargas + "\\Links.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                            Lector_Vínculos.Seek(Lector_Vínculos.Length, SeekOrigin.Begin); // Append text.
                            StreamWriter Lector_Texto_Vínculos = new StreamWriter(Lector_Vínculos, Encoding.UTF8);
                            Lector_Texto_Vínculos.WriteLine("[" + Nombre_Temporal + "] Links: " + Program.Traducir_Número(Diccionario_Vínculos.Count) + ".");
                            Lector_Texto_Vínculos.WriteLine();
                            Lector_Texto_Vínculos.Flush();
                            foreach (KeyValuePair<string, string> Entrada in Diccionario_Vínculos)
                            {
                                try
                                {
                                    Lector_Texto_Vínculos.WriteLine(Entrada.Key); // Always add.
                                    Lector_Texto_Vínculos.Flush();
                                    byte[] Matriz_Bytes = Program.Descargar_Archivo_Completo(Entrada.Key, 6, 10); // Up to 1 minute per file.
                                    if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                                    {
                                        uint CRC_32 = Program.Calcular_CRC32(Matriz_Bytes);
                                        string Texto_CRC_32 = Convert.ToString(CRC_32, 16).ToUpperInvariant(); // To base 16.
                                        while (Texto_CRC_32.Length < 8) Texto_CRC_32 = '0' + Texto_CRC_32;
                                        Lector_Texto_Descargas.WriteLine("[" + Texto_CRC_32 + "] " + Entrada.Key);
                                        Lector_Texto_Descargas.Flush();
                                        string Extensión = null;
                                        try { Extensión = Path.GetExtension(Entrada.Key).ToLowerInvariant(); }
                                        catch { Extensión = null; }
                                        if (!string.IsNullOrEmpty(Extensión))
                                        {
                                            char[] Matriz_Caracteres = Extensión.ToCharArray();
                                            for (int Índice_Caracter = 0; Índice_Caracter < Matriz_Caracteres.Length; Índice_Caracter++)
                                            {
                                                if (Program.Lista_Caracteres_Prohibidos.Contains(Matriz_Caracteres[Índice_Caracter]))
                                                {
                                                    Matriz_Caracteres[Índice_Caracter] = '_'; // Replace invalid characters.
                                                }
                                            }
                                            Extensión = new string(Matriz_Caracteres);
                                            Matriz_Caracteres = null;
                                        }
                                        string Ruta = Ruta_Salida + "\\" + Texto_CRC_32;
                                        while (File.Exists(Ruta + Extensión)) Ruta += '_';
                                        FileStream Lector_Salida = new FileStream(Ruta + Extensión, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                        //Lector_Salida.SetLength(0L); // Reset the file?
                                        Lector_Salida.Seek(0L, SeekOrigin.Begin);
                                        Lector_Salida.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                        Lector_Salida.Flush();
                                        Lector_Salida.Close();
                                        Lector_Salida.Dispose();
                                        Lector_Salida = null;
                                        Vínculos_Válidos++;
                                    }
                                }
                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                            }
                            Lector_Texto_Descargas.WriteLine();
                            Lector_Texto_Descargas.WriteLine();
                            Lector_Texto_Descargas.Flush();
                            Lector_Texto_Vínculos.WriteLine();
                            Lector_Texto_Vínculos.WriteLine();
                            Lector_Texto_Vínculos.Flush();
                            Lector_Texto_Vínculos.Close();
                            Lector_Texto_Vínculos.Dispose();
                            Lector_Texto_Vínculos = null;
                            Lector_Vínculos.Close();
                            Lector_Vínculos.Dispose();
                            Lector_Vínculos = null;
                            Lector_Texto_Descargas.Close();
                            Lector_Texto_Descargas.Dispose();
                            Lector_Texto_Descargas = null;
                            Lector_Descargas.Close();
                            Lector_Descargas.Dispose();
                            Lector_Descargas = null;
                            this.Text = Texto_Título + " - [Found and valid links: " + Program.Traducir_Número(Documento.Links.Count.ToString()) + ", " + Program.Traducir_Número(Vínculos_Válidos) + "]";
                            Program.Ejecutar_Ruta(Ruta_Salida, ProcessWindowStyle.Maximized);
                            SystemSounds.Asterisk.Play();
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Navegador_Web_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Navegador_Web_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            try
            {
                
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Navegador_Web_NewWindow(object sender, CancelEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Navegador_Web_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            try
            {
                Barra_Progreso.Maximum = Math.Max((int)e.MaximumProgress, 0);
                Barra_Progreso.Value = Math.Max((int)e.CurrentProgress, 0);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
