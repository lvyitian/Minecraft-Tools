using Ionic.Zlib;
using Minecraft_Tools.Properties;
using Substrate_Jupisoft.Core;
using Substrate_Jupisoft.Nbt;
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
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Visor_NBT : Form
    {
        public Ventana_Visor_NBT()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "NBT Viewer by Jupisoft for " + Program.Texto_Usuario;

        internal static readonly int Longitud_Máxima_Valores_Árbol = 40; // 32 // 64 // 128

        internal bool Variable_Siempre_Visible = false;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;

        internal string Ruta_Entrada = null;
        internal int Total_Nodos = 0;
        internal Dictionary<int, object> Diccionario_NBT_Objetos = new Dictionary<int, object>();
        internal Dictionary<int, TagType> Diccionario_NBT_Tipos = new Dictionary<int, TagType>();
        internal SortedDictionary<string, object> Diccionario_Valores_Texto = new SortedDictionary<string, object>();

        private void Ventana_Visor_NBT_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                ImageList Lista_Imágenes = new ImageList();
                Lista_Imágenes.ColorDepth = ColorDepth.Depth32Bit;
                Lista_Imágenes.ImageSize = new Size(16, 16);
                Lista_Imágenes.TransparentColor = Color.Empty;
                Lista_Imágenes.Images.AddRange(new Image[] { Resources.NBT_End, Resources.NBT_Byte, Resources.NBT_Short, Resources.NBT_Int, Resources.NBT_Long, Resources.NBT_Float, Resources.NBT_Double, Resources.NBT_Byte_Array, Resources.NBT_String, Resources.NBT_List, Resources.NBT_Compound, Resources.NBT_Int_Array, Resources.NBT_Long_Array, Resources.NBT_Unknown, Resources.Carpeta, Resources.Región });
                TreeView_NBT.ImageList = Lista_Imágenes;
                TreeView_NBT.StateImageList = Lista_Imágenes;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_NBT_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_NBT_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_NBT_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_NBT_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_NBT_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
                {
                    string[] Matriz_Rutas = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                    if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                    {
                        Diccionario_Valores_Texto.Clear(); // Support for multi-folder string values exporting.
                        foreach (string Ruta in Matriz_Rutas)
                        {
                            if (!string.IsNullOrEmpty(Ruta) && (File.Exists(Ruta) || Directory.Exists(Ruta)))
                            {
                                if (File.Exists(Ruta) || File.Exists(Ruta + "\\level.dat"))
                                {
                                    this.Cursor = Cursors.WaitCursor;
                                    try { if (Abrir_Archivo_NBT(File.Exists(Ruta) ? Ruta : Ruta + "\\level.dat", true)) break; }
                                    catch (Exception Excepción)
                                    {
                                        Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                        Variable_Excepción_Total++;
                                        Variable_Excepción = true;
                                        continue;
                                    }
                                }
                                else if (Directory.Exists(Ruta))
                                {
                                    string[] Matriz_Archivos = Directory.GetFiles(Ruta, "*", SearchOption.TopDirectoryOnly);
                                    if (Matriz_Archivos != null && Matriz_Archivos.Length > 0)
                                    {
                                        for (int Índice_Archivo = 0; Índice_Archivo < Matriz_Archivos.Length; Índice_Archivo++)
                                        {
                                            try { Abrir_Archivo_NBT(Matriz_Archivos[Índice_Archivo], false); }
                                            catch (Exception Excepción)
                                            {
                                                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                                Variable_Excepción_Total++;
                                                Variable_Excepción = true;
                                                continue;
                                            }
                                        }
                                    }
                                    Matriz_Archivos = null;
                                }
                            }
                            Matriz_Rutas = null;
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_NBT_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (TreeView_NBT.Width != 511) TreeView_NBT.Width = 511;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_NBT_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Alt && !e.Control && !e.Shift)
                {
                    if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Delete)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        this.Close();
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
                catch { Barra_Estado_Etiqueta_Memoria.Text = "RAM: ? MB (? GB)"; }
            }
            catch (Exception Excepción) {  Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal bool Abrir_Archivo_NBT(string Ruta, bool Reiniciar_Diccionario_Valores_Texto)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                {
                    this.Text = Texto_Título + " - [Loading, please wait up to a few minutes...]";
                    Diccionario_NBT_Objetos.Clear();
                    Diccionario_NBT_Tipos.Clear();
                    if (Reiniciar_Diccionario_Valores_Texto) Diccionario_Valores_Texto.Clear();
                    TreeView_NBT.Nodes.Clear();
                    TextBox_Valor.Text = null;
                    Total_Nodos = 0;
                    Ruta_Entrada = null;
                    GC.Collect();
                    GC.GetTotalMemory(true);
                    this.Cursor = Cursors.WaitCursor;
                    string Nombre = Path.GetFileName(Ruta).ToLowerInvariant();
                    string Extensión = Path.GetExtension(Ruta);
                    //if (string.IsNullOrEmpty(Extensión)) Extensión = string.Empty;
                    //Extensión = Extensión.ToLowerInvariant();
                    if (string.Compare(Extensión, ".mca", true) == 0 || string.Compare(Extensión, ".mcr", true) == 0)
                    {
                        TreeNode Nodo_Archivo = TreeView_NBT.Nodes.Add(Total_Nodos.ToString(), Nombre, 15, 15);
                        Total_Nodos++;
                        RegionFile Archivo_Región = new RegionFile(Ruta);
                        if (Archivo_Región != null)
                        {
                            Ruta_Entrada = Ruta;
                            TreeView_NBT.BeginUpdate();
                            for (int Chunk_Z = 0; Chunk_Z < 32; Chunk_Z++)
                            {
                                for (int Chunk_X = 0; Chunk_X < 32; Chunk_X++)
                                {
                                    string Compresión;
                                    Stream Lector_Chunk = Archivo_Región.GetChunkDataInputStream(Chunk_X, Chunk_Z, out Compresión);
                                    if (Lector_Chunk != null)
                                    {
                                        string Chunk = (Variable_Mostrar_Tipos_NBT ? Compresión : null) + "Chunk " + Chunk_X.ToString() + ", " + Chunk_Z.ToString() + "";
                                        TreeNode Nodo_Chunk = Nodo_Archivo.Nodes.Add(Total_Nodos.ToString(), Chunk, 14, 14);
                                        Total_Nodos++;
                                        NbtTree Árbol = new NbtTree(Lector_Chunk);
                                        if (Árbol != null && Árbol.Root != null/* && Árbol.Root.ContainsKey("Level")*/)
                                        {
                                            Agregar_Lista_Compuesta(Nodo_Chunk, Árbol.Root);
                                        }
                                    }
                                }
                            }
                            this.Text = Texto_Título + " - [Nodes: " + Program.Traducir_Número(Total_Nodos) + "]";
                            TreeView_NBT.EndUpdate();
                            Archivo_Región.Close();
                            Archivo_Región.Dispose();
                            Archivo_Región = null;
                            //TreeView_NBT.ExpandAll();
                            foreach (TreeNode Nodo in TreeView_NBT.Nodes)
                            {
                                Nodo.Expand();
                                TreeView_NBT.SelectedNode = Nodo;
                                /*foreach (TreeNode Subnodo in TreeView_NBT.Nodes)
                                {
                                    Subnodo.ExpandAll();
                                    TreeView_NBT.SelectedNode = Subnodo;
                                    break;
                                }*/
                                break;
                            }
                            GC.Collect();
                            GC.GetTotalMemory(true);
                            return true;
                        }
                    }
                    else // Unknown file type, try to decode it with all the known ways
                    {
                        FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                        if (Lector != null && Lector.Length > 0L)
                        {
                            string Compresión = null;
                            NbtTree Árbol = null;
                            try
                            {
                                Lector.Seek(0L, SeekOrigin.Begin);
                                Árbol = new NbtTree();
                                Árbol.ReadFrom(Lector);
                                if (Árbol != null && Árbol.Root != null) Compresión = "[Uncompressed] ";
                            }
                            catch// (Exception Excepción)
                            {
                                //Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                Árbol = null;
                            }
                            if (Árbol == null || Árbol.Root == null)
                            {
                                try
                                {
                                    Lector.Seek(0L, SeekOrigin.Begin);
                                    Árbol = new NbtTree();
                                    Árbol.ReadFrom(new GZipStream(Lector, CompressionMode.Decompress));
                                    if (Árbol != null && Árbol.Root != null) Compresión = "[GZipStream] ";
                                }
                                catch// (Exception Excepción)
                                {
                                    //Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                    Árbol = null;
                                }
                            }
                            if (Árbol == null || Árbol.Root == null)
                            {
                                try
                                {
                                    Lector.Seek(0L, SeekOrigin.Begin);
                                    Árbol = new NbtTree();
                                    Árbol.ReadFrom(new ZlibStream(Lector, CompressionMode.Decompress));
                                    if (Árbol != null && Árbol.Root != null) Compresión = "[ZlibStream] ";
                                }
                                catch// (Exception Excepción)
                                {
                                    //Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                    Árbol = null;
                                }
                            }
                            if (Árbol == null || Árbol.Root == null)
                            {
                                try
                                {
                                    Lector.Seek(0L, SeekOrigin.Begin);
                                    Árbol = new NbtTree();
                                    Árbol.ReadFrom(new DeflateStream(Lector, CompressionMode.Decompress));
                                    if (Árbol != null && Árbol.Root != null) Compresión = "[DeflateStream] ";
                                }
                                catch// (Exception Excepción)
                                {
                                    //Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                    Árbol = null;
                                }
                            }
                            if (Árbol != null && Árbol.Root != null)
                            {
                                Ruta_Entrada = Ruta;
                                TreeNode Nodo_Archivo = TreeView_NBT.Nodes.Add(Total_Nodos.ToString(), (Variable_Mostrar_Tipos_NBT ? Compresión : null) + Nombre, 15, 15);
                                Total_Nodos++;
                                TreeView_NBT.BeginUpdate();
                                Agregar_Lista_Compuesta(Nodo_Archivo, Árbol.Root);
                                this.Text = Texto_Título + " - [Nodes: " + Program.Traducir_Número(Total_Nodos) + "]";
                                TreeView_NBT.EndUpdate();
                                //TreeView_NBT.ExpandAll();
                                foreach (TreeNode Nodo in TreeView_NBT.Nodes)
                                {
                                    Nodo.Expand();
                                    TreeView_NBT.SelectedNode = Nodo;
                                    /*foreach (TreeNode Subnodo in TreeView_NBT.Nodes)
                                    {
                                        Subnodo.ExpandAll();
                                        TreeView_NBT.SelectedNode = Subnodo;
                                        break;
                                    }*/
                                    break;
                                }
                                GC.Collect();
                                GC.GetTotalMemory(true);
                                return true;
                            }
                            else MessageBox.Show(this, "The file couldn't be opened, make sure it's a valid NBT file.\r\nAnd if it is, maybe this application is outdated, so sorry about that.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Question);
                        }
                        Lector.Close();
                        Lector.Dispose();
                        Lector = null;
                    }
                }
            }
            catch (Exception Excepción)
            {
                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                this.Text = Texto_Título;
                Diccionario_NBT_Objetos.Clear();
                Diccionario_NBT_Tipos.Clear();
                if (Reiniciar_Diccionario_Valores_Texto) Diccionario_Valores_Texto.Clear();
                TreeView_NBT.Nodes.Clear();
                TextBox_Valor.Text = null;
                Total_Nodos = 0;
                Ruta_Entrada = null;
            }
            finally { this.Cursor = Cursors.Default; }
            return false;
        }

        

        internal void Agregar_Lista_Compuesta(TreeNode Nodo_Árbol, TagNodeCompound Nodo_Lista_Compuesta)
        {
            try
            {
                if (Nodo_Lista_Compuesta != null)
                {
                    foreach (string Nombre in Nodo_Lista_Compuesta.Keys)
                    {
                        TagNode Nodo_Valor = Nodo_Lista_Compuesta[Nombre];
                        TagType Tipo = Nodo_Valor.GetTagType();
                        if (Tipo == TagType.TAG_COMPOUND)
                        {
                            TreeNode Subnodo_Árbol = Nodo_Árbol.Nodes.Add(Total_Nodos.ToString(), Traducir_Tipo(Tipo, Variable_Mostrar_Tipos_NBT) + Nombre + ": " + Traducir_Valor(Tipo, Nodo_Valor), (int)Tipo, (int)Tipo);
                            Diccionario_NBT_Objetos.Add(Total_Nodos, null);
                            Diccionario_NBT_Tipos.Add(Total_Nodos, Tipo);
                            Total_Nodos++;
                            Agregar_Lista_Compuesta(Subnodo_Árbol, Nodo_Valor as TagNodeCompound);
                        }
                        else if (Tipo == TagType.TAG_LIST)
                        {
                            TagNodeList Nodo_Lista = Nodo_Valor as TagNodeList;
                            TreeNode Subnodo_Árbol = Nodo_Árbol.Nodes.Add(Total_Nodos.ToString(), Traducir_Tipo(Tipo, Nodo_Lista.ValueType, Variable_Mostrar_Tipos_NBT) + Nombre + ": " + Traducir_Valor(Tipo, Nodo_Valor), (int)Tipo, (int)Tipo);
                            Diccionario_NBT_Objetos.Add(Total_Nodos, null);
                            Diccionario_NBT_Tipos.Add(Total_Nodos, Tipo);
                            Total_Nodos++;
                            Agregar_Lista(Subnodo_Árbol, Nodo_Lista);
                            /*TagNodeList Nodo_Lista = Nodo_Valor as TagNodeList;
                            if (Nodo_Lista != null)
                            {
                                Tipo = Nodo_Lista.ValueType;
                                for (int Índice = 0; Índice < Nodo_Lista.Count; Índice++)
                                {
                                    //string Nombre_Diccionario = Obtener_Nombre_Válido_Diccionario(Nodo_Árbol.FullPath + "\\" + (Índice + 1).ToString());
                                    Agregar_Valor(Subnodo_Árbol, (Índice + 1).ToString(), Tipo, Nodo_Lista[Índice]);
                                }
                            }*/
                        }
                        else Agregar_Valor(Nodo_Árbol, Nombre, Tipo, Nodo_Valor);
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Agregar_Lista(TreeNode Nodo_Árbol, TagNodeList Nodo_Lista)
        {
            try
            {
                TagType Tipo = Nodo_Lista.ValueType;
                for (int Índice = 0; Índice < Nodo_Lista.Count; Índice++)
                {
                    if (Tipo == TagType.TAG_COMPOUND)
                    {
                        TagNodeCompound Nodo_Compuesto = Nodo_Lista[Índice] as TagNodeCompound;
                        TreeNode Subnodo_Árbol = Nodo_Árbol.Nodes.Add(Total_Nodos.ToString(), Traducir_Tipo(Tipo, Variable_Mostrar_Tipos_NBT) + Program.Traducir_Número(Nodo_Compuesto.Count), (int)Tipo, (int)Tipo);
                        Diccionario_NBT_Objetos.Add(Total_Nodos, null);
                        Diccionario_NBT_Tipos.Add(Total_Nodos, Tipo);
                        Total_Nodos++;
                        Agregar_Valor(Subnodo_Árbol, (Índice + 1).ToString(), Tipo, Nodo_Lista[Índice]);
                    }
                    else if (Tipo == TagType.TAG_LIST)
                    {
                        TagNodeList Nodo_Sublista = Nodo_Lista[Índice] as TagNodeList;
                        TreeNode Subnodo_Árbol = Nodo_Árbol.Nodes.Add(Total_Nodos.ToString(), Traducir_Tipo(Tipo, Variable_Mostrar_Tipos_NBT) + Program.Traducir_Número(Nodo_Sublista.Count), (int)Tipo, (int)Tipo);
                        Diccionario_NBT_Objetos.Add(Total_Nodos, null);
                        Diccionario_NBT_Tipos.Add(Total_Nodos, Tipo);
                        Total_Nodos++;
                        Agregar_Valor(Subnodo_Árbol, (Índice + 1).ToString(), Tipo, Nodo_Lista[Índice]);
                    }
                    else Agregar_Valor(Nodo_Árbol, (Índice + 1).ToString(), Tipo, Nodo_Lista[Índice]);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Agregar_Valor(TreeNode Nodo_Árbol, string Nombre, TagType Tipo, TagNode Nodo_Valor)
        {
            try
            {
                if (Tipo == TagType.TAG_END)
                {
                    object Valor = null;
                    TreeNode Subnodo_Árbol = Nodo_Árbol.Nodes.Add(Total_Nodos.ToString(), Traducir_Tipo(Tipo, Variable_Mostrar_Tipos_NBT) + Nombre + ": " + Traducir_Valor(Tipo, Nodo_Valor), (int)Tipo, (int)Tipo);
                    Diccionario_NBT_Objetos.Add(Total_Nodos, Valor);
                    Diccionario_NBT_Tipos.Add(Total_Nodos, Tipo);
                    Total_Nodos++;
                }
                else if (Tipo == TagType.TAG_BYTE)
                {
                    byte Valor = Nodo_Valor.ToTagByte();
                    TreeNode Subnodo_Árbol = Nodo_Árbol.Nodes.Add(Total_Nodos.ToString(), Traducir_Tipo(Tipo, Variable_Mostrar_Tipos_NBT) + Nombre + ": " + Traducir_Valor(Tipo, Nodo_Valor), (int)Tipo, (int)Tipo);
                    Diccionario_NBT_Objetos.Add(Total_Nodos, Valor);
                    Diccionario_NBT_Tipos.Add(Total_Nodos, Tipo);
                    Total_Nodos++;
                }
                else if (Tipo == TagType.TAG_SHORT)
                {
                    short Valor = Nodo_Valor.ToTagShort();
                    TreeNode Subnodo_Árbol = Nodo_Árbol.Nodes.Add(Total_Nodos.ToString(), Traducir_Tipo(Tipo, Variable_Mostrar_Tipos_NBT) + Nombre + ": " + Traducir_Valor(Tipo, Nodo_Valor), (int)Tipo, (int)Tipo);
                    Diccionario_NBT_Objetos.Add(Total_Nodos, Valor);
                    Diccionario_NBT_Tipos.Add(Total_Nodos, Tipo);
                    Total_Nodos++;
                }
                else if (Tipo == TagType.TAG_INT)
                {
                    int Valor = Nodo_Valor.ToTagInt();
                    TreeNode Subnodo_Árbol = Nodo_Árbol.Nodes.Add(Total_Nodos.ToString(), Traducir_Tipo(Tipo, Variable_Mostrar_Tipos_NBT) + Nombre + ": " + Traducir_Valor(Tipo, Nodo_Valor), (int)Tipo, (int)Tipo);
                    Diccionario_NBT_Objetos.Add(Total_Nodos, Valor);
                    Diccionario_NBT_Tipos.Add(Total_Nodos, Tipo);
                    Total_Nodos++;
                }
                else if (Tipo == TagType.TAG_LONG)
                {
                    long Valor = Nodo_Valor.ToTagLong();
                    TreeNode Subnodo_Árbol = Nodo_Árbol.Nodes.Add(Total_Nodos.ToString(), Traducir_Tipo(Tipo, Variable_Mostrar_Tipos_NBT) + Nombre + ": " + Traducir_Valor(Tipo, Nodo_Valor), (int)Tipo, (int)Tipo);
                    Diccionario_NBT_Objetos.Add(Total_Nodos, Valor);
                    Diccionario_NBT_Tipos.Add(Total_Nodos, Tipo);
                    Total_Nodos++;
                }
                else if (Tipo == TagType.TAG_FLOAT)
                {
                    float Valor = Nodo_Valor.ToTagFloat();
                    TreeNode Subnodo_Árbol = Nodo_Árbol.Nodes.Add(Total_Nodos.ToString(), Traducir_Tipo(Tipo, Variable_Mostrar_Tipos_NBT) + Nombre + ": " + Traducir_Valor(Tipo, Nodo_Valor), (int)Tipo, (int)Tipo);
                    Diccionario_NBT_Objetos.Add(Total_Nodos, Valor);
                    Diccionario_NBT_Tipos.Add(Total_Nodos, Tipo);
                    Total_Nodos++;
                }
                else if (Tipo == TagType.TAG_DOUBLE)
                {
                    double Valor = Nodo_Valor.ToTagDouble();
                    TreeNode Subnodo_Árbol = Nodo_Árbol.Nodes.Add(Total_Nodos.ToString(), Traducir_Tipo(Tipo, Variable_Mostrar_Tipos_NBT) + Nombre + ": " + Traducir_Valor(Tipo, Nodo_Valor), (int)Tipo, (int)Tipo);
                    Diccionario_NBT_Objetos.Add(Total_Nodos, Valor);
                    Diccionario_NBT_Tipos.Add(Total_Nodos, Tipo);
                    Total_Nodos++;
                }
                else if (Tipo == TagType.TAG_BYTE_ARRAY)
                {
                    byte[] Valor = Nodo_Valor.ToTagByteArray();
                    TreeNode Subnodo_Árbol = Nodo_Árbol.Nodes.Add(Total_Nodos.ToString(), Traducir_Tipo(Tipo, Variable_Mostrar_Tipos_NBT) + Nombre + ": " + Traducir_Valor(Tipo, Nodo_Valor), (int)Tipo, (int)Tipo);
                    Diccionario_NBT_Objetos.Add(Total_Nodos, Valor);
                    Diccionario_NBT_Tipos.Add(Total_Nodos, Tipo);
                    Total_Nodos++;
                }
                else if (Tipo == TagType.TAG_STRING)
                {
                    string Valor = Nodo_Valor.ToTagString();
                    //if (!string.IsNullOrEmpty(Valor) && !Diccionario_Valores_Texto.ContainsKey(Valor)) Diccionario_Valores_Texto.Add(Valor, null);
                    if (!string.IsNullOrEmpty(Valor) && !Diccionario_Valores_Texto.ContainsKey("Diccionario_Índice_Tesoros.Add(Diccionario_Nombre_Índice[\"" + Valor + "\"], null);")) Diccionario_Valores_Texto.Add("Diccionario_Índice_Tesoros.Add(Diccionario_Nombre_Índice[\"" + Valor + "\"], null);", null);
                    TreeNode Subnodo_Árbol = Nodo_Árbol.Nodes.Add(Total_Nodos.ToString(), Traducir_Tipo(Tipo, Variable_Mostrar_Tipos_NBT) + Nombre + ": " + Traducir_Valor(Tipo, Nodo_Valor), (int)Tipo, (int)Tipo);
                    Diccionario_NBT_Objetos.Add(Total_Nodos, Valor);
                    Diccionario_NBT_Tipos.Add(Total_Nodos, Tipo);
                    Total_Nodos++;
                }
                else if (Tipo == TagType.TAG_LIST)
                {
                    Agregar_Lista(Nodo_Árbol, Nodo_Valor as TagNodeList);
                }
                else if (Tipo == TagType.TAG_COMPOUND)
                {
                    Agregar_Lista_Compuesta(Nodo_Árbol, Nodo_Valor as TagNodeCompound);
                }
                else if (Tipo == TagType.TAG_INT_ARRAY)
                {
                    int[] Valor = Nodo_Valor.ToTagIntArray();
                    TreeNode Subnodo_Árbol = Nodo_Árbol.Nodes.Add(Total_Nodos.ToString(), Traducir_Tipo(Tipo, Variable_Mostrar_Tipos_NBT) + Nombre + ": " + Traducir_Valor(Tipo, Nodo_Valor), (int)Tipo, (int)Tipo);
                    Diccionario_NBT_Objetos.Add(Total_Nodos, Valor);
                    Diccionario_NBT_Tipos.Add(Total_Nodos, Tipo);
                    Total_Nodos++;
                }
                else if (Tipo == TagType.TAG_LONG_ARRAY)
                {
                    long[] Valor = Nodo_Valor.ToTagLongArray();
                    TreeNode Subnodo_Árbol = Nodo_Árbol.Nodes.Add(Total_Nodos.ToString(), Traducir_Tipo(Tipo, Variable_Mostrar_Tipos_NBT) + Nombre + ": " + Traducir_Valor(Tipo, Nodo_Valor), (int)Tipo, (int)Tipo);
                    Diccionario_NBT_Objetos.Add(Total_Nodos, Valor);
                    Diccionario_NBT_Tipos.Add(Total_Nodos, Tipo);
                    Total_Nodos++;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal string Traducir_Tipo(TagType Tipo, bool Traducir)
        {
            try
            {
                if (Traducir)
                {
                    if (Tipo == TagType.TAG_END) return "[End] ";
                    else if (Tipo == TagType.TAG_BYTE) return "[Byte] ";
                    else if (Tipo == TagType.TAG_SHORT) return "[Short] ";
                    else if (Tipo == TagType.TAG_INT) return "[Int] ";
                    else if (Tipo == TagType.TAG_LONG) return "[Long] ";
                    else if (Tipo == TagType.TAG_FLOAT) return "[Float] ";
                    else if (Tipo == TagType.TAG_DOUBLE) return "[Double] ";
                    else if (Tipo == TagType.TAG_BYTE_ARRAY) return "[Byte Array] ";
                    else if (Tipo == TagType.TAG_STRING) return "[String] ";
                    else if (Tipo == TagType.TAG_LIST) return "[List] ";
                    else if (Tipo == TagType.TAG_COMPOUND) return "[Compound] ";
                    else if (Tipo == TagType.TAG_INT_ARRAY) return "[Int Array] ";
                    else if (Tipo == TagType.TAG_LONG_ARRAY) return "[Long Array] ";
                    else return "[Unknown] ";
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        internal string Traducir_Tipo(TagType Tipo, TagType Subtipo, bool Traducir)
        {
            try
            {
                if (Traducir)
                {
                    if (Tipo == TagType.TAG_END) return "[End] ";
                    else if (Tipo == TagType.TAG_BYTE) return "[Byte] ";
                    else if (Tipo == TagType.TAG_SHORT) return "[Short] ";
                    else if (Tipo == TagType.TAG_INT) return "[Int] ";
                    else if (Tipo == TagType.TAG_LONG) return "[Long] ";
                    else if (Tipo == TagType.TAG_FLOAT) return "[Float] ";
                    else if (Tipo == TagType.TAG_DOUBLE) return "[Double] ";
                    else if (Tipo == TagType.TAG_BYTE_ARRAY) return "[Byte Array] ";
                    else if (Tipo == TagType.TAG_STRING) return "[String] ";
                    else if (Tipo == TagType.TAG_LIST)
                    {
                        if (Subtipo == TagType.TAG_END) return "[List of End] ";
                        else if (Subtipo == TagType.TAG_BYTE) return "[List of Byte] ";
                        else if (Subtipo == TagType.TAG_SHORT) return "[List of Short] ";
                        else if (Subtipo == TagType.TAG_INT) return "[List of Int] ";
                        else if (Subtipo == TagType.TAG_LONG) return "[List of Long] ";
                        else if (Subtipo == TagType.TAG_FLOAT) return "[List of Float] ";
                        else if (Subtipo == TagType.TAG_DOUBLE) return "[List of Double] ";
                        else if (Subtipo == TagType.TAG_BYTE_ARRAY) return "[List of Byte Array] ";
                        else if (Subtipo == TagType.TAG_STRING) return "[List of String] ";
                        else if (Subtipo == TagType.TAG_LIST) return "[List of List] ";
                        else if (Subtipo == TagType.TAG_COMPOUND) return "[List of Compound] ";
                        else if (Subtipo == TagType.TAG_INT_ARRAY) return "[List of Int Array] ";
                        else if (Subtipo == TagType.TAG_LONG_ARRAY) return "[List of Long Array] ";
                        else return "[List of Unknown] ";
                    }
                    else if (Tipo == TagType.TAG_COMPOUND) return "[Compound] ";
                    else if (Tipo == TagType.TAG_INT_ARRAY) return "[Int Array] ";
                    else if (Tipo == TagType.TAG_LONG_ARRAY) return "[Long Array] ";
                    else return "[Unknown] ";
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        internal string Traducir_Valor(TagType Tipo, TagNode Nodo_Valor)
        {
            try
            {
                if (Tipo == TagType.TAG_END)
                {
                    return "null";
                }
                else if (Tipo == TagType.TAG_BYTE)
                {
                    string Texto = ((byte)Nodo_Valor.ToTagByte()).ToString();
                    if (Texto.Length >= Longitud_Máxima_Valores_Árbol) Texto = Texto.Substring(0, Longitud_Máxima_Valores_Árbol - 3) + "...";
                    return Texto;
                }
                else if (Tipo == TagType.TAG_SHORT)
                {
                    string Texto = ((short)Nodo_Valor.ToTagShort()).ToString();
                    if (Texto.Length >= Longitud_Máxima_Valores_Árbol) Texto = Texto.Substring(0, Longitud_Máxima_Valores_Árbol - 3) + "...";
                    return Texto;
                }
                else if (Tipo == TagType.TAG_INT)
                {
                    string Texto = ((int)Nodo_Valor.ToTagInt()).ToString();
                    if (Texto.Length >= Longitud_Máxima_Valores_Árbol) Texto = Texto.Substring(0, Longitud_Máxima_Valores_Árbol - 3) + "...";
                    return Texto;
                }
                else if (Tipo == TagType.TAG_LONG)
                {
                    string Texto = ((long)Nodo_Valor.ToTagLong()).ToString();
                    if (Texto.Length >= Longitud_Máxima_Valores_Árbol) Texto = Texto.Substring(0, Longitud_Máxima_Valores_Árbol - 3) + "...";
                    return Texto;
                }
                else if (Tipo == TagType.TAG_FLOAT)
                {
                    string Texto = ((float)Nodo_Valor.ToTagFloat()).ToString();
                    if (Texto.Length >= Longitud_Máxima_Valores_Árbol) Texto = Texto.Substring(0, Longitud_Máxima_Valores_Árbol - 3) + "...";
                    return Texto;
                }
                else if (Tipo == TagType.TAG_DOUBLE)
                {
                    string Texto = ((double)Nodo_Valor.ToTagDouble()).ToString();
                    if (Texto.Length >= Longitud_Máxima_Valores_Árbol) Texto = Texto.Substring(0, Longitud_Máxima_Valores_Árbol - 3) + "...";
                    return Texto;
                }
                else if (Tipo == TagType.TAG_BYTE_ARRAY)
                {
                    byte[] Matriz_Valores = Nodo_Valor.ToTagByteArray();
                    string Texto = Matriz_Valores.Length.ToString() + " { ";
                    foreach (long Valor in Matriz_Valores) Texto += Valor.ToString() + ", ";
                    Texto = Texto.TrimEnd(", ".ToCharArray());
                    if (Texto.Length >= Longitud_Máxima_Valores_Árbol - 2) Texto = Texto.Substring(0, Longitud_Máxima_Valores_Árbol - 5) + "...";
                    return Texto + " }";
                }
                else if (Tipo == TagType.TAG_STRING)
                {
                    string Texto = (string)Nodo_Valor.ToTagString();
                    if (Texto.Length >= Longitud_Máxima_Valores_Árbol) Texto = Texto.Substring(0, Longitud_Máxima_Valores_Árbol - 3) + "...";
                    return Texto;
                }
                else if (Tipo == TagType.TAG_LIST)
                {
                    string Texto = ((TagNodeList)Nodo_Valor).Count.ToString();
                    if (Texto.Length >= Longitud_Máxima_Valores_Árbol) Texto = Texto.Substring(0, Longitud_Máxima_Valores_Árbol - 3) + "...";
                    return Texto;
                }
                else if (Tipo == TagType.TAG_COMPOUND)
                {
                    string Texto = ((TagNodeCompound)Nodo_Valor).Count.ToString();
                    if (Texto.Length >= Longitud_Máxima_Valores_Árbol) Texto = Texto.Substring(0, Longitud_Máxima_Valores_Árbol - 3) + "...";
                    return Texto;
                }
                else if (Tipo == TagType.TAG_INT_ARRAY)
                {
                    int[] Matriz_Valores = Nodo_Valor.ToTagIntArray();
                    string Texto = Matriz_Valores.Length.ToString() + " { ";
                    foreach (long Valor in Matriz_Valores) Texto += Valor.ToString() + ", ";
                    Texto = Texto.TrimEnd(", ".ToCharArray());
                    if (Texto.Length >= Longitud_Máxima_Valores_Árbol - 2) Texto = Texto.Substring(0, Longitud_Máxima_Valores_Árbol - 5) + "...";
                    return Texto + " }";
                }
                else if (Tipo == TagType.TAG_LONG_ARRAY)
                {
                    long[] Matriz_Valores = Nodo_Valor.ToTagLongArray();
                    string Texto = Matriz_Valores.Length.ToString() + " { ";
                    foreach (long Valor in Matriz_Valores) Texto += Valor.ToString() + ", ";
                    Texto = Texto.TrimEnd(", ".ToCharArray());
                    if (Texto.Length >= Longitud_Máxima_Valores_Árbol - 2) Texto = Texto.Substring(0, Longitud_Máxima_Valores_Árbol - 5) + "...";
                    return Texto + " }";
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        internal string Traducir_Valor(TagType Tipo, object Objeto)
        {
            try
            {
                if (Tipo == TagType.TAG_END)
                {
                    return null;
                }
                else if (Tipo == TagType.TAG_BYTE)
                {
                    string Texto = ((byte)Objeto).ToString();
                    return Texto;
                }
                else if (Tipo == TagType.TAG_SHORT)
                {
                    string Texto = ((short)Objeto).ToString();
                    return Texto;
                }
                else if (Tipo == TagType.TAG_INT)
                {
                    string Texto = ((int)Objeto).ToString();
                    return Texto;
                }
                else if (Tipo == TagType.TAG_LONG)
                {
                    string Texto = ((long)Objeto).ToString();
                    return Texto;
                }
                else if (Tipo == TagType.TAG_FLOAT)
                {
                    string Texto = ((float)Objeto).ToString();
                    return Texto;
                }
                else if (Tipo == TagType.TAG_DOUBLE)
                {
                    string Texto = ((double)Objeto).ToString();
                    return Texto;
                }
                else if (Tipo == TagType.TAG_BYTE_ARRAY)
                {
                    byte[] Matriz_Valores = (byte[])Objeto;
                    string Texto = null;
                    foreach (long Valor in Matriz_Valores) Texto += Valor.ToString() + ", ";
                    Texto = Texto.TrimEnd(", ".ToCharArray());
                    return Texto;
                }
                else if (Tipo == TagType.TAG_STRING)
                {
                    string Texto = (string)Objeto;
                    return Texto;
                }
                else if (Tipo == TagType.TAG_LIST)
                {
                    return "List";
                }
                else if (Tipo == TagType.TAG_COMPOUND)
                {
                    return "Compound";
                }
                else if (Tipo == TagType.TAG_INT_ARRAY)
                {
                    int[] Matriz_Valores = (int[])Objeto;
                    string Texto = null;
                    foreach (long Valor in Matriz_Valores) Texto += Valor.ToString() + ", ";
                    Texto = Texto.TrimEnd(", ".ToCharArray());
                    return Texto;
                }
                else if (Tipo == TagType.TAG_LONG_ARRAY)
                {
                    long[] Matriz_Valores = (long[])Objeto;
                    string Texto = null;
                    foreach (long Valor in Matriz_Valores) Texto += Valor.ToString() + ", ";
                    Texto = Texto.TrimEnd(", ".ToCharArray());
                    return Texto;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.NBT_viewer;
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
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes);
                Program.Ejecutar_Ruta(Program.Ruta_Guardado_Imágenes, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Abrir_Archivo_NBT(Ruta_Entrada, true);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TextBox_Valor.Text))
                {
                    Clipboard.SetText(TextBox_Valor.Text);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal static bool Variable_Mostrar_Tipos_NBT = true;

        private void Menú_Contextual_Mostrar_Tipos_NBT_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Mostrar_Tipos_NBT = Menú_Contextual_Mostrar_Tipos_NBT.Checked;
                Abrir_Archivo_NBT(Ruta_Entrada, true);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TreeView_NBT_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    if (e.Node != null) // Why it doesn't work?...
                    {
                        e.Node.ExpandAll();
                        TreeView_NBT.SelectedNode = e.Node;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TreeView_NBT_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node != null)
                {
                    int Índice = int.Parse(e.Node.Name);
                    if (Diccionario_NBT_Objetos.ContainsKey(Índice) && Diccionario_NBT_Tipos.ContainsKey(Índice))
                    {
                        TextBox_Valor.Text = Traducir_Valor(Diccionario_NBT_Tipos[Índice], Diccionario_NBT_Objetos[Índice]);
                    }
                    else TextBox_Valor.Text = null;
                }
                else TextBox_Valor.Text = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Expandir_Nodo_Click(object sender, EventArgs e)
        {
            try
            {
                if (TreeView_NBT.SelectedNode != null)
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (!TreeView_NBT.SelectedNode.IsExpanded) TreeView_NBT.SelectedNode.Expand();
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Expandir_Nodo_Subnodos_Click(object sender, EventArgs e)
        {
            try
            {
                if (TreeView_NBT.SelectedNode != null)
                {
                    this.Cursor = Cursors.WaitCursor;
                    TreeView_NBT.SelectedNode.ExpandAll();
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Barra_Estado_Etiqueta_Sugerencia_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
                {
                    MessageBox.Show(this, "[Information about the NBT tags (Named Binary Tags)]\r\n\r\n" +
                        "ID 0, End: It's an empty ending tag also used for closing lists.\r\n" +
                        "ID 1, Byte: It's an unsigned 8 bits value (from 0 to 255).\r\n" +
                        "ID 2, Short: It's a signed 16 bits value (from -32768 to 32767).\r\n" +
                        "ID 3, Int: It's a signed 32 bits value (from -2147483648 to 2147483647).\r\n" +
                        "ID 4, Long: It's a signed 64 bits value (from -9223372036854775808 to 9223372036854775807).\r\n" +
                        "ID 5, Float: It's a signed decimal 32 bits value (from -3" + Program.Caracter_Coma_Decimal + "40282347E+38 to 3" + Program.Caracter_Coma_Decimal + "40282347E+38).\r\n" +
                        "ID 6, Double: It's a signed decimal 64 bits value (from -1" + Program.Caracter_Coma_Decimal + "7976931348623157E+308 to 1" + Program.Caracter_Coma_Decimal + "7976931348623157E+308).\r\n" +
                        "ID 7, Byte array: It's an array of variable length made of bytes.\r\n" +
                        "ID 8, String: It's a string of characters of variable length.\r\n" +
                        "ID 9, List: It's a list of variable length made of a single tag type.\r\n" +
                        "ID 10, Compound: It's a list of variable length that supports any mixed tag types.\r\n" +
                        "ID 11, Int array: It's an array of variable length made of ints.\r\n" +
                        "ID 12, Long array: It's an array of variable length made of longs.\r\n", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        //internal static readonly string Texto_Espaciador_Nodos = " "; // "\t"

        internal string Obtener_Espaciador_Nodos(int Nivel)
        {
            try
            {
                if (Nivel > 0) return new string(' ', Nivel * 4); // Add 4 spaces before any line for any depth in the tree node
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        internal void Obtener_Texto_Subnodos(TreeNodeCollection Nodos, int Nivel, StreamWriter Lector_Texto)
        {
            try
            {
                foreach (TreeNode Nodo in Nodos)
                {
                    if (Nodo != null)
                    {
                        Lector_Texto.WriteLine(Obtener_Espaciador_Nodos(Nivel) + Nodo.Text);
                        if (Nodo.Nodes != null) Obtener_Texto_Subnodos(Nodo.Nodes, Nivel + 1, Lector_Texto);
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /*internal string Obtener_Texto_Subnodo(TreeNodeCollection Nodos, int Nivel, string Texto)
        {
            try
            {
                foreach (TreeNode Subnodo in Nodo.Nodes)
                {

                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }*/

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (TreeView_NBT.Nodes != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Visor_NBT);
                    string Ruta = Program.Ruta_Guardado_Imágenes_Visor_NBT + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " NBT viewer (" + Program.Traducir_Número(Total_Nodos) + (Total_Nodos != 1 ? " nodes" : " node") + ").txt";
                    FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector.SetLength(0L);
                    Lector.Seek(0L, SeekOrigin.Begin);
                    StreamWriter Lector_Texto = new StreamWriter(Lector, Encoding.Unicode);
                    Obtener_Texto_Subnodos(TreeView_NBT.Nodes, 0, Lector_Texto);
                    SystemSounds.Asterisk.Play();
                    Lector_Texto.Close();
                    Lector_Texto.Dispose();
                    Lector_Texto = null;
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                    if (File.Exists(Ruta)) Program.Ejecutar_Ruta(Ruta, ProcessWindowStyle.Normal);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Copiar_Valores_Texto_Click(object sender, EventArgs e)
        {
            try
            {
                if (Diccionario_Valores_Texto != null && Diccionario_Valores_Texto.Count > 0)
                {
                    string Texto = null;
                    foreach (KeyValuePair<string, object> Entrada in Diccionario_Valores_Texto)
                    {
                        Texto += Entrada.Key + "\r\n";
                    }
                    Texto = Texto.TrimEnd("\r\n".ToCharArray());
                    Clipboard.SetText(Texto);
                    SystemSounds.Asterisk.Play();
                    Texto = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Valores_Texto_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (Diccionario_Valores_Texto != null && Diccionario_Valores_Texto.Count > 0)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Visor_NBT);
                    string Ruta = Program.Ruta_Guardado_Imágenes_Visor_NBT + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " NBT viewer (" + Program.Traducir_Número(Total_Nodos) + (Diccionario_Valores_Texto.Count != 1 ? " objects" : " object") + ").txt";
                    FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector.SetLength(0L);
                    Lector.Seek(0L, SeekOrigin.Begin);
                    StreamWriter Lector_Texto = new StreamWriter(Lector, Encoding.Unicode);
                    foreach (KeyValuePair<string, object> Entrada in Diccionario_Valores_Texto)
                    {
                        Lector_Texto.WriteLine(Entrada.Key);
                    }
                    SystemSounds.Asterisk.Play();
                    Lector_Texto.Close();
                    Lector_Texto.Dispose();
                    Lector_Texto = null;
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                    if (File.Exists(Ruta)) Program.Ejecutar_Ruta(Ruta, ProcessWindowStyle.Normal);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
