using Minecraft_Tools.Properties;
using SevenZip;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Comparador_Versiones_JAR : Form
    {
        public Ventana_Comparador_Versiones_JAR()
        {
            InitializeComponent();
        }

        internal static readonly int Ancho_Árboles = 300;

        internal ArchiveFileInfo[] Matriz_Archivos_1 = null;
        internal ArchiveFileInfo[] Matriz_Archivos_2 = null;
        internal SortedDictionary<string, string> Diccionario_Rutas_1 = new SortedDictionary<string, string>();
        internal SortedDictionary<string, string> Diccionario_Rutas_2 = new SortedDictionary<string, string>();
        internal string Ruta_Buscar = null;

        internal readonly string Texto_Título = "JAR Versions Comparer by Jupisoft for " + Program.Texto_Usuario;

        internal bool Variable_Siempre_Visible = false;
        internal bool Ocupado = false;

        private void Ventana_Comparador_Versiones_JAR_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                Panel_Árbol_1.Width = Ancho_Árboles;
                Panel_Árbol_2.Width = Ancho_Árboles;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Comparador_Versiones_JAR_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Comparador_Versiones_JAR_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Comparador_Versiones_JAR_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Comparador_Versiones_JAR_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal void Obtener_Carpetas(ArchiveFileInfo[] Matriz_Archivos, string Ruta_JAR, bool Cargar_2)
        {
            try
            {
                if (!Ocupado)
                {
                    Ocupado = true;
                    if (!Cargar_2) TreeView_1.Nodes.Clear();
                    else TreeView_2.Nodes.Clear();
                    if (Matriz_Archivos != null && Matriz_Archivos.Length > 0)
                    {
                        SortedDictionary<string, string> Diccionario_Rutas = !Cargar_2 ? Diccionario_Rutas_1 : Diccionario_Rutas_2;
                        Diccionario_Rutas.Clear();
                        foreach (ArchiveFileInfo Archivo in Matriz_Archivos)
                        {
                            if (Archivo.FileName.Contains('\\'))
                            {
                                int Índice = Archivo.FileName.LastIndexOf('\\');
                                if (Índice > -1)
                                {
                                    string Texto_Nodo = Archivo.FileName.Substring(0, Índice);
                                    if (!Diccionario_Rutas.ContainsKey(Texto_Nodo.ToLowerInvariant())) Diccionario_Rutas.Add(Texto_Nodo.ToLowerInvariant(), Texto_Nodo);
                                }
                            }
                        }
                        //MessageBox.Show(Extractor_7_Zip.FileName);
                        TreeNode Nodo = !Cargar_2 ? TreeView_1.Nodes.Add(Ruta_JAR, Ruta_JAR) : TreeView_2.Nodes.Add(Ruta_JAR, Ruta_JAR);
                        if (Diccionario_Rutas.Count > 0)
                        {
                            if (!Cargar_2) TreeView_1.BeginUpdate();
                            else TreeView_2.BeginUpdate();
                            foreach (KeyValuePair<string, string> Entrada in Diccionario_Rutas)
                            {
                                //MessageBox.Show(Entrada.Value);
                                //TreeView_1.Nodes.Add(Entrada.Value/*.Replace('\\', '/')*/);
                                string[] Matriz_Carpetas = Entrada.Value.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                                Nodo = !Cargar_2 ? TreeView_1.Nodes[Ruta_JAR] : TreeView_2.Nodes[Ruta_JAR];
                                for (int Índice = 0; Índice < Matriz_Carpetas.Length; Índice++)
                                {
                                    //if (Nodo == null) MessageBox.Show("!Nodo");
                                    //if (Nodo.Nodes == null) MessageBox.Show("!Nodos");
                                    if (!Nodo.Nodes.ContainsKey(Matriz_Carpetas[Índice]))
                                    {
                                        Nodo = Nodo.Nodes.Add(Matriz_Carpetas[Índice], Matriz_Carpetas[Índice]);
                                    }
                                    else
                                    {
                                        Nodo = Nodo.Nodes[Matriz_Carpetas[Índice]];
                                    }
                                }
                            }
                            if (!Cargar_2)
                            {
                                TreeView_1.EndUpdate();
                                TreeView_1.ExpandAll();
                                string Ruta_Buscar_Temporal = Ruta_Buscar;
                                TreeView_1.SelectedNode = TreeView_1.Nodes[Ruta_JAR];
                                Ruta_Buscar = Ruta_Buscar_Temporal;
                            }
                            else
                            {
                                TreeView_2.EndUpdate();
                                TreeView_2.ExpandAll();
                                string Ruta_Buscar_Temporal = Ruta_Buscar;
                                TreeView_2.SelectedNode = TreeView_2.Nodes[Ruta_JAR];
                                Ruta_Buscar = Ruta_Buscar_Temporal;
                            }
                        }
                        /*foreach (ArchiveFileInfo Archivo in Extractor_7_Zip.ArchiveFileData)
                        {
                            //if (Archivo.IsDirectory)
                            {
                                //MessageBox.Show(Archivo.FileName);
                                //TreeView_1.Nodes.Add(Archivo.FileName);
                            }
                            //Extractor_7_Zip.ArchiveFileData[0]


                            if (!Cargar_2)
                            {

                            }
                            else
                            {

                            }
                        }*/
                        //MessageBox.Show(Extractor_7_Zip.FilesCount.ToString());
                    }
                    //else MessageBox.Show("null...");
                    Ocupado = false;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Comparador_Versiones_JAR_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
                {
                    string[] Matriz_Rutas = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                    if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                    {
                        bool Cargar_2 = Control.ModifierKeys != Keys.None;
                        if (Matriz_Archivos_1 != null && Matriz_Archivos_2 == null && !Cargar_2) Cargar_2 = true;
                        else if (Matriz_Archivos_1 == null && Matriz_Archivos_2 != null && Cargar_2) Cargar_2 = false;
                        foreach (string Ruta in Matriz_Rutas)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(Ruta))
                                {
                                    if (File.Exists(Ruta))
                                    {
                                        Ocupado = true;
                                        SevenZipExtractor Extractor_7_Zip = null;
                                        FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                                        try { Extractor_7_Zip = new SevenZipExtractor(Lector); }
                                        catch (Exception Excepción)
                                        {
                                            Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                            Extractor_7_Zip = null;
                                        }
                                        if (Extractor_7_Zip != null && Extractor_7_Zip.FilesCount > 0)
                                        {
                                            if (!Cargar_2)
                                            {
                                                Matriz_Archivos_1 = new ArchiveFileInfo[Extractor_7_Zip.ArchiveFileData.Count];
                                                Extractor_7_Zip.ArchiveFileData.CopyTo(Matriz_Archivos_1, 0);
                                                Ocupado = false;
                                                Obtener_Carpetas(Matriz_Archivos_1, Path.GetFileName(Ruta), Cargar_2);
                                            }
                                            else
                                            {
                                                Matriz_Archivos_2 = new ArchiveFileInfo[Extractor_7_Zip.ArchiveFileData.Count];
                                                Extractor_7_Zip.ArchiveFileData.CopyTo(Matriz_Archivos_2, 0);
                                                Ocupado = false;
                                                Obtener_Carpetas(Matriz_Archivos_2, Path.GetFileName(Ruta), Cargar_2);
                                            }
                                            Extractor_7_Zip.Dispose();
                                            Extractor_7_Zip = null;
                                            Comparar();
                                        }
                                        //else MessageBox.Show("NULL...");
                                        Lector.Close();
                                        Lector.Dispose();
                                        Lector = null;
                                    }
                                    else if (Directory.Exists(Ruta))
                                    {

                                    }
                                    /*if (File.Exists(Ruta))
                                    {
                                        string Extensión = Path.GetExtension(Ruta);
                                        if (string.Compare(Extensión, ".png", true) == 0)
                                        {
                                            Color Color_ARGB = Minecraft.Obtener_Color_Único_Imagen(Minecraft.Cargar_Imagen(Ruta, true));
                                            string Texto_Color = Color_ARGB.A.ToString() + ", " + Color_ARGB.R.ToString() + ", " + Color_ARGB.G.ToString() + ", " + Color_ARGB.B.ToString();
                                            Clipboard.SetText("Color.FromArgb(" + Texto_Color + ")");
                                            MessageBox.Show(this, "The average ARGB color from the image is: " + Texto_Color + ".");
                                            return;
                                        }
                                    }
                                    string Ruta_Carpeta = Directory.Exists(Ruta) ? Ruta : Path.GetDirectoryName(Ruta);
                                    if (Directory.Exists(Ruta_Carpeta + "\\region") || Directory.Exists(Ruta_Carpeta + "\\DIM-1") || Directory.Exists(Ruta_Carpeta + "\\DIM1"))
                                    {
                                        Ruta_Pendiente_Abrir = Ruta_Carpeta;
                                        //this.Cursor = Cursors.WaitCursor; // En espera para abrir la ruta
                                    }*/
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
            finally { Ocupado = false; }
        }

        private void Ventana_Comparador_Versiones_JAR_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Alt && !e.Control && !e.Shift)
                {
                    if (e.KeyCode == Keys.Escape) this.Close();
                    else if (e.KeyCode == Keys.Enter)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void TreeView_1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    Panel_Árbol_1.Width = Panel_Árbol_1.Width <= 0 ? Ancho_Árboles : 0;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void TreeView_2_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    Panel_Árbol_2.Width = Panel_Árbol_2.Width <= 0 ? Ancho_Árboles : 0;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void DataGridView_Principal_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    if (Panel_Árbol_1.Width <= 0 || Panel_Árbol_2.Width <= 0)
                    {
                        Panel_Árbol_1.Width = Ancho_Árboles;
                        Panel_Árbol_2.Width = Ancho_Árboles;
                    }
                    else
                    {
                        Panel_Árbol_1.Width = 0;
                        Panel_Árbol_2.Width = 0;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void TreeView_1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (TreeView_1.Nodes != null && TreeView_1.Nodes.Count > 0 && TreeView_1.Nodes[0].Nodes != null && TreeView_1.Nodes[0].Nodes.Count > 0 && TreeView_1.SelectedNode != null) // Hay un nodo con subnodos
                {
                    Ruta_Buscar = TreeView_1.Nodes[0].Name.Length + 1 < TreeView_1.SelectedNode.FullPath.Length ? TreeView_1.SelectedNode.FullPath.Substring(TreeView_1.Nodes[0].Name.Length + 1).ToLowerInvariant() : null;
                    Comparar();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void TreeView_2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (TreeView_2.Nodes != null && TreeView_2.Nodes.Count > 0 && TreeView_2.Nodes[0].Nodes != null && TreeView_2.Nodes[0].Nodes.Count > 0 && TreeView_2.SelectedNode != null) // Hay un nodo con subnodos
                {
                    Ruta_Buscar = TreeView_2.Nodes[0].Name.Length + 1 < TreeView_2.SelectedNode.FullPath.Length ? TreeView_2.SelectedNode.FullPath.Substring(TreeView_2.Nodes[0].Name.Length + 1).ToLowerInvariant() : null;
                    Comparar();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal string Archivo_Existe(ArchiveFileInfo[] Matriz_Archivos, string Ruta, out uint CRC32)
        {
            CRC32 = 0;
            try
            {
                foreach (ArchiveFileInfo Archivo in Matriz_Archivos)
                {
                    if (string.Compare(Archivo.FileName, Ruta, true) == 0)
                    {
                        CRC32 = Archivo.Crc;
                        return Path.GetFileName(Archivo.FileName);
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return string.Empty;
        }

        internal void Comparar()
        {
            try
            {
                if (!Ocupado && TreeView_1.Nodes.Count > 0 && TreeView_2.Nodes.Count > 0)
                {
                    Ocupado = true;
                    this.Cursor = Cursors.WaitCursor;
                    // 1º colorear los árboles...

                    if (Matriz_Archivos_1 != null && Matriz_Archivos_2 != null && Matriz_Archivos_1.Length > 0 && Matriz_Archivos_2.Length > 0)
                    {
                        this.Text = Texto_Título + " - [Comparing the files, please wait up to a few minutes...]";
                        SortedDictionary<string, string> Diccionario_Comparación = new SortedDictionary<string, string>();
                        foreach (ArchiveFileInfo Archivo in Matriz_Archivos_1)
                        {
                            string Ruta = Archivo.FileName.ToLowerInvariant();
                            if (string.IsNullOrEmpty(Ruta_Buscar) || Ruta.StartsWith(Ruta_Buscar))
                            {
                                if (!Diccionario_Comparación.ContainsKey(Ruta))
                                {
                                    Diccionario_Comparación.Add(Ruta, Archivo.FileName);
                                }
                            }
                        }
                        foreach (ArchiveFileInfo Archivo in Matriz_Archivos_2)
                        {
                            string Ruta = Archivo.FileName.ToLowerInvariant();
                            if (string.IsNullOrEmpty(Ruta_Buscar) || Ruta.StartsWith(Ruta_Buscar))
                            {
                                if (!Diccionario_Comparación.ContainsKey(Ruta))
                                {
                                    Diccionario_Comparación.Add(Ruta, Archivo.FileName);
                                }
                            }
                        }
                        if (Diccionario_Comparación.Count > 0)
                        {
                            Bitmap Imagen_Verde = Resources.Aceptar;
                            Bitmap Imagen_Amarillo = Resources.Restablecer;
                            Bitmap Imagen_Rojo = Resources.Cancelar;
                            DataGridView_Principal.Rows.Clear();
                            DataGridView_Principal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                            DataGridView_Principal.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                            //List<DataGridViewRow> Lista_Filas = new List<DataGridViewRow>();
                            int Índice_Fila = 0;
                            DataGridViewRow[] Matriz_Filas = new DataGridViewRow[Diccionario_Comparación.Count];
                            foreach (KeyValuePair<string, string> Entrada in Diccionario_Comparación)
                            {
                                DataGridViewRow Fila = new DataGridViewRow();
                                uint CRC32_1 = 0;
                                uint CRC32_2 = 0;
                                string Nombre_1 = Archivo_Existe(Matriz_Archivos_1, Entrada.Key, out CRC32_1);
                                string Nombre_2 = Archivo_Existe(Matriz_Archivos_2, Entrada.Key, out CRC32_2);
                                Bitmap Imagen = null;
                                string Comparación = string.Empty;
                                Color Color_ARGB = Color.White;
                                if (!string.IsNullOrEmpty(Nombre_1) && !string.IsNullOrEmpty(Nombre_2)) // Both files exist
                                {
                                    if (CRC32_1 == CRC32_2) // Both files are the same
                                    {
                                        Imagen = Imagen_Verde;
                                        Comparación = "Equal: both files are identical";
                                        Color_ARGB = Color.FromArgb(255, 192, 255, 192); // Green
                                    }
                                    else
                                    {
                                        Imagen = Imagen_Amarillo;
                                        Comparación = "Different: the CRC's don't match";
                                        Color_ARGB = Color.FromArgb(255, 255, 255, 192); // Yellow
                                        //Color_ARGB = Color.FromArgb(255, 232, 232, 255); // Blue?
                                    }
                                }
                                else
                                {
                                    Imagen = Imagen_Rojo;
                                    Comparación = "Different: only one file exists";
                                    Color_ARGB = Color.FromArgb(255, 255, 192, 192); // Red
                                }
                                Fila.CreateCells(DataGridView_Principal, new object[] { Imagen, Comparación, Nombre_1, Nombre_2, CRC32_1, CRC32_2, Entrada.Value });
                                Fila.DefaultCellStyle.BackColor = Color_ARGB;
                                //Lista_Filas.Add(Fila);
                                //DataGridView_Principal.Rows.Add(Fila);
                                Matriz_Filas[Índice_Fila] = Fila;
                                Índice_Fila++;
                            }
                            //DataGridView_Principal.Rows.AddRange(Lista_Filas.ToArray());
                            DataGridView_Principal.Rows.AddRange(Matriz_Filas);
                            DataGridView_Principal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                            DataGridView_Principal.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                            DataGridView_Principal.Sort(Columna_Comparación, ListSortDirection.Ascending);
                            this.Text = Texto_Título + " - [Files compared: " + Program.Traducir_Número(DataGridView_Principal.Rows.Count) + "]";
                            Ocupado = false;
                            this.Cursor = Cursors.WaitCursor;
                        }
                    }
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridView_Principal.Rows.Count > 0)
                {
                    this.Controls.Remove(DataGridView_Principal);
                    int Ancho = DataGridView_Principal.RowHeadersVisible ? DataGridView_Principal.RowHeadersWidth + 2 : 3;
                    int Alto = DataGridView_Principal.ColumnHeadersVisible ? DataGridView_Principal.ColumnHeadersHeight + 2 : 3;
                    foreach (DataGridViewColumn Columna in DataGridView_Principal.Columns) Ancho += Columna.Width;
                    foreach (DataGridViewRow Fila in DataGridView_Principal.Rows) Alto += Fila.Height;
                    DataGridView_Principal.Size = new Size(Ancho, Alto);
                    Bitmap Imagen = new Bitmap(DataGridView_Principal.Width, DataGridView_Principal.Height, PixelFormat.Format24bppRgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.Clear(Color.Fuchsia);
                    Pintar.Dispose();
                    Pintar = null;
                    DataGridView_Principal.DrawToBitmap(Imagen, new Rectangle(0, 0, Imagen.Width, Imagen.Height));
                    if (!Directory.Exists(Program.Ruta_Guardado_Imágenes)) Directory.CreateDirectory(Program.Ruta_Guardado_Imágenes);
                    Clipboard.SetImage(Imagen);
                    Imagen.Dispose();
                    Imagen = null;
                    this.Controls.Add(DataGridView_Principal);
                    DataGridView_Principal.BringToFront();
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridView_Principal.Rows.Count > 0)
                {
                    this.Controls.Remove(DataGridView_Principal);
                    int Ancho = DataGridView_Principal.RowHeadersVisible ? DataGridView_Principal.RowHeadersWidth + 2 : 3;
                    int Alto = DataGridView_Principal.ColumnHeadersVisible ? DataGridView_Principal.ColumnHeadersHeight + 2 : 3;
                    foreach (DataGridViewColumn Columna in DataGridView_Principal.Columns) Ancho += Columna.Width;
                    foreach (DataGridViewRow Fila in DataGridView_Principal.Rows) Alto += Fila.Height;
                    DataGridView_Principal.Size = new Size(Ancho, Alto);
                    Bitmap Imagen = new Bitmap(DataGridView_Principal.Width, DataGridView_Principal.Height, PixelFormat.Format24bppRgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.Clear(Color.Fuchsia);
                    Pintar.Dispose();
                    Pintar = null;
                    DataGridView_Principal.DrawToBitmap(Imagen, new Rectangle(0, 0, Imagen.Width, Imagen.Height));
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Buscador_JAR);
                    Imagen.Save(Program.Ruta_Guardado_Imágenes_Buscador_JAR + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " Jar (" + Program.Traducir_Número(DataGridView_Principal.Rows.Count) + (DataGridView_Principal.Rows.Count != 1 ? " files" : " file") + ").png", ImageFormat.Png);
                    Imagen.Dispose();
                    Imagen = null;
                    this.Controls.Add(DataGridView_Principal);
                    DataGridView_Principal.BringToFront();
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.Finder_of_differences_between_JAR_versions;
                Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
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
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Depurador_Excepciones_Click(object sender, EventArgs e)
        {
            try
            {
                /*Variable_Excepción = false;
                Variable_Excepción_Imagen = false;
                Variable_Excepción_Total = 0;
                Barra_Estado_Botón_Excepción.Visible = false;
                Barra_Estado_Separador_1.Visible = false;
                Barra_Estado_Botón_Excepción.Image = Resources.Excepción_Gris;
                Barra_Estado_Botón_Excepción.ForeColor = Color.Black;
                Barra_Estado_Botón_Excepción.Text = "Exceptions: 0";*/
                Ventana_Depurador_Excepciones Ventana = new Ventana_Depurador_Excepciones();
                Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Abrir_Carpeta_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Buscador_JAR);
                Program.Ejecutar_Ruta(Program.Ruta_Guardado_Imágenes_Buscador_JAR, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Comparar();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void DataGridView_Principal_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                Depurador.Escribir_Excepción(e.Exception != null ? e.Exception.ToString() : null);
                e.ThrowException = false;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }
    }
}
