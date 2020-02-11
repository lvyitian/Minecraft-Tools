using Minecraft_Tools.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class Ventana_Selector_Bloques : Form
    {
        public Ventana_Selector_Bloques()
        {
            InitializeComponent();
        }

        internal static readonly string Ruta_Guardado_Paletas = Program.Ruta_Guardado_Imágenes + "\\Palettes";

        internal List<short> Lista_Paleta_Original = null;
        internal List<short> Lista_Paleta = null;
        internal bool Variable_Siempre_Visible = false;

        private void Ventana_Selector_Bloques_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text += " - [Sorry, but any change to the palette won't be saved for now...]";
                //this.WindowState = FormWindowState.Maximized;
                if (Lista_Paleta == null)
                {
                    Lista_Paleta = new List<short>();
                    foreach (KeyValuePair<short, string> Entrada in Minecraft.Diccionario_Bloques_Índices_Nombres)
                    {
                        if (!Minecraft.Diccionario_Texturas_Transparentes.ContainsKey(Entrada.Key) && !Minecraft.Diccionario_Bloques_Altura_Diferente.ContainsKey(Entrada.Key) && !Minecraft.Diccionario_Bloques_Minecraft_1_13.ContainsKey(Entrada.Key))
                        {
                            Lista_Paleta.Add(Entrada.Key);
                        }
                    }
                }
                DataGridView_Principal.Rows.Clear();

                //this.WindowState = FormWindowState.Maximized;
                //List<byte> Lista_Bytes_CRC_32 = new List<byte>();

                int Índice_Fila = 0;
                DataGridViewRow[] Matriz_Filas = new DataGridViewRow[Minecraft.Diccionario_Bloques_Índices_Colores.Count];
                foreach (KeyValuePair<short, Color> Entrada in Minecraft.Diccionario_Bloques_Índices_Colores)
                {
                    string Nombre = Minecraft.Diccionario_Bloques_Índices_Nombres[Entrada.Key].Substring(10);
                    /*double Valor_CRC_32 = 0d;
                    foreach (char Caracter in Nombre)
                    {
                        Valor_CRC_32 += (double)Caracter;
                    }
                    //"".GetHashCode();
                    byte[] Matriz_Bytes = Encoding.UTF8.GetBytes(Nombre);
                    uint CRC_32 = Program.Calcular_CRC32(Matriz_Bytes);
                    byte CRC_8 = (byte)CRC_32;
                    //CRC_8 = (byte)(CRC_32 >> 24);
                    CRC_8 = (byte)(CRC_32 % 255);
                    CRC_8 = (byte)(Valor_CRC_32 % 255);
                    CRC_8 = (byte)(CRC_32 | (uint)Valor_CRC_32);
                    CRC_8 = (byte)(((double)CRC_32 + Valor_CRC_32) % 255);
                    CRC_8 = (byte)(Nombre.GetHashCode() % 255);
                    CRC_8 = (byte)Nombre.GetHashCode();
                    if (!Lista_Bytes_CRC_32.Contains(CRC_8)) Lista_Bytes_CRC_32.Add(CRC_8);*/
                    string Nombre_Invertido = null;
                    string[] Matriz_Palabras = Nombre.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int Índice_Palabra = Matriz_Palabras.Length - 1; Índice_Palabra >= 0; Índice_Palabra--)
                    {
                        Nombre_Invertido += Matriz_Palabras[Índice_Palabra] + (Índice_Palabra > 0 ? "_" : null);
                    }
                    Nombre = Nombre.Substring(0, 1).ToUpperInvariant() + Nombre.Substring(1).Replace('_', ' ');
                    Nombre_Invertido = Nombre_Invertido.Substring(0, 1).ToUpperInvariant() + Nombre_Invertido.Substring(1).Replace('_', ' ');
                    string Tipo = null;
                    if (Minecraft.Diccionario_Texturas_Píxeles_Completamente_Transparentes.ContainsKey(Entrada.Key)) Tipo = "Fully transparent pixels";
                    else if (Minecraft.Diccionario_Texturas_Píxeles_Parcialmente_Transparentes.ContainsKey(Entrada.Key)) Tipo = "Partially transparent pixels";
                    else Tipo = "Solid block";
                    DataGridViewRow Fila = new DataGridViewRow();
                    Fila.CreateCells(DataGridView_Principal, new object[] { Lista_Paleta.Contains(Entrada.Key), Minecraft.Diccionario_Texturas[Entrada.Key], Nombre, Minecraft.Diccionario_Texturas[Entrada.Key], Nombre_Invertido, Program.Obtener_Imagen_Color(Color.FromArgb(Entrada.Value.A, 255, 255, 255)), Entrada.Value.A, Program.Obtener_Imagen_Color(Color.FromArgb(255, Entrada.Value.R, 0, 0)), Entrada.Value.R, Program.Obtener_Imagen_Color(Color.FromArgb(255, 0, Entrada.Value.G, 0)), Entrada.Value.G, Program.Obtener_Imagen_Color(Color.FromArgb(255, 0, 0, Entrada.Value.B)), Entrada.Value.B, Program.Obtener_Imagen_Color(Entrada.Value), /*CRC_8, */Entrada.Value.GetHashCode(), Tipo, Minecraft.Diccionario_Bloques_Índices_Nombres[Entrada.Key] });
                    /*Fila.Cells[Columna_Alfa.Index].Style.BackColor = Color.FromArgb(255, Entrada.Value.A, Entrada.Value.A, Entrada.Value.A);
                    Fila.Cells[Columna_Rojo.Index].Style.BackColor = Color.FromArgb(255, Entrada.Value.R, 0, 0);
                    Fila.Cells[Columna_Verde.Index].Style.BackColor = Color.FromArgb(255, 0, Entrada.Value.G, 0);
                    Fila.Cells[Columna_Azul.Index].Style.BackColor = Color.FromArgb(255, 0, 0, Entrada.Value.B);
                    Fila.Cells[Columna_Códgo_Hash.Index].Style.BackColor = Color.FromArgb(255, Entrada.Value.R, Entrada.Value.G, Entrada.Value.B);
                    if (Entrada.Value.A < 128) Fila.Cells[Columna_Alfa.Index].Style.ForeColor = Color.White;
                    if (Entrada.Value.R < 128) Fila.Cells[Columna_Rojo.Index].Style.ForeColor = Color.White;
                    if (Entrada.Value.G < 128) Fila.Cells[Columna_Verde.Index].Style.ForeColor = Color.White;
                    if (Entrada.Value.B < 128) Fila.Cells[Columna_Azul.Index].Style.ForeColor = Color.White;
                    if (((Entrada.Value.R + Entrada.Value.G + Entrada.Value.B) / 3) < 128) Fila.Cells[Columna_Códgo_Hash.Index].Style.ForeColor = Color.White;*/
                    Matriz_Filas[Índice_Fila] = Fila;
                    Índice_Fila++;
                }
                DataGridView_Principal.Rows.AddRange(Matriz_Filas);
                DataGridView_Principal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                DataGridView_Principal.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                //MessageBox.Show(Lista_Bytes_CRC_32.Count.ToString());
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Selector_Bloques_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Selector_Bloques_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Selector_Bloques_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Selector_Bloques_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Selector_Bloques_DragDrop(object sender, DragEventArgs e)
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
                                    FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                                    
                                    Lector.Close();
                                    Lector.Dispose();
                                    Lector = null;
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

        private void Ventana_Selector_Bloques_SizeChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Selector_Bloques_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Alt && !e.Control && !e.Shift)
                {
                    if (e.KeyCode == Keys.Escape)
                    {
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Restablecer_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lista_Paleta_Original != null)
                {
                    Lista_Paleta.Clear();
                    if (Lista_Paleta_Original.Count > 0) Lista_Paleta = Lista_Paleta_Original.GetRange(0, Lista_Paleta_Original.Count);

                    // Establecer checkboxes...
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void ComboBox_Paleta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Exportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lista_Paleta.Count > 0)
                {
                    if (!Directory.Exists(Ruta_Guardado_Paletas)) Directory.CreateDirectory(Ruta_Guardado_Paletas);
                    string Ruta = Ruta_Guardado_Paletas + "\\Block palette " + Program.Obtener_Nombre_Temporal() + ".bin";
                    FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    if (Lector.Length > 0L) Lector.SetLength(0L);
                    Lector.Seek(0L, SeekOrigin.Begin);
                    BinaryWriter Lector_Binario = new BinaryWriter(Lector, Encoding.Unicode);
                    foreach (short ID in Lista_Paleta)
                    {
                        string Nombre = Minecraft.Diccionario_Bloques_Índices_Nombres[ID];
                        byte[] Matriz_Bytes = Encoding.Unicode.GetBytes(Nombre);
                        if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                        {
                            Lector_Binario.Write(Matriz_Bytes.Length);
                            Lector_Binario.Write(Matriz_Bytes);
                        }
                        Matriz_Bytes = null;
                    }
                    SystemSounds.Asterisk.Play();
                    Lector_Binario.Close();
                    Lector_Binario.Dispose();
                    Lector_Binario = null;
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                }
                else MessageBox.Show("The selected palette doesn't contain any block.\r\nEnable at least one block before exporting it...", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Importar_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("To import a palette just do a drag and drop on the window, or move the palette file to the following location, press F5 and select it from the combobox below: \"" + Ruta_Guardado_Paletas + "\".", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Cancelar_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
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
