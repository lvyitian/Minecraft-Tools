using Minecraft_Tools.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Visor_Información_Bloques : Form
    {
        public Ventana_Visor_Información_Bloques()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Block Information Viewer by Jupisoft for " + Program.Texto_Usuario;
        internal bool Variable_Siempre_Visible = false;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;
        internal static Stopwatch Cronómetro_FPS = Stopwatch.StartNew();
        internal long Segundo_FPS_Anterior = 0L;
        internal long FPS_Temporal = 0L;
        internal long FPS_Real = 0L;
        internal bool Ocupado = false;

        private void Ventana_Visor_Información_Bloques_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                DataGridView_Principal.Sort(Columna_Nombre, ListSortDirection.Ascending);
                Menú_Contextual_Filtrar_Todos.PerformClick();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Información_Bloques_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Información_Bloques_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Información_Bloques_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Información_Bloques_KeyDown(object sender, KeyEventArgs e)
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
                    else if ((e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z) || (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9))
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        string Tecla = e.KeyCode.ToString();
                        string Letra = Tecla[Tecla.Length - 1].ToString().ToUpperInvariant();
                        int Índice_Columna_Actual = DataGridView_Principal.CurrentCell != null ? DataGridView_Principal.CurrentCell.ColumnIndex : Columna_Nombre.Index;
                        int Índice_Fila_Actual = DataGridView_Principal.CurrentCell != null ? DataGridView_Principal.CurrentCell.RowIndex : -1;
                        for (int Índice_Fila = 0; Índice_Fila < DataGridView_Principal.Rows.Count; Índice_Fila++)
                        {
                            string Nombre = null;
                            try { Nombre = DataGridView_Principal[Índice_Columna_Actual, Índice_Fila].Value.ToString(); }
                            catch { Nombre = null; }
                            if (!string.IsNullOrEmpty(Nombre) && Nombre.StartsWith(Letra))
                            {
                                if (Índice_Fila != Índice_Fila_Actual)
                                {
                                    DataGridView_Principal.CurrentCell = DataGridView_Principal[Índice_Columna_Actual, Índice_Fila];
                                    return;
                                }
                                else
                                {
                                    Índice_Fila_Actual = int.MaxValue;
                                    break;
                                }
                            }
                        }
                        if (Índice_Fila_Actual >= int.MaxValue) // Do an inverted search.
                        {
                            for (int Índice_Fila = DataGridView_Principal.Rows.Count - 1; Índice_Fila >= 0; Índice_Fila--)
                            {
                                string Nombre = null;
                                try { Nombre = DataGridView_Principal[Índice_Columna_Actual, Índice_Fila].Value.ToString(); }
                                catch { Nombre = null; }
                                if (!string.IsNullOrEmpty(Nombre) && Nombre.StartsWith(Letra))
                                {
                                    DataGridView_Principal.CurrentCell = DataGridView_Principal[Índice_Columna_Actual, Índice_Fila];
                                    return;
                                }
                            }
                        }
                        SystemSounds.Beep.Play(); // Nothing found starting with the pressed letter.
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
                long Milisegundo_FPS = Cronómetro_FPS.ElapsedMilliseconds;
                long Segundo_FPS = Milisegundo_FPS / 1000L;
                if (Segundo_FPS != Segundo_FPS_Anterior)
                {
                    Segundo_FPS_Anterior = Segundo_FPS;
                    FPS_Real = FPS_Temporal;
                    Barra_Estado_Etiqueta_FPS.Text = FPS_Real.ToString() + " FPS";
                    FPS_Temporal = 0L;
                }
                FPS_Temporal++;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void DataGridView_Principal_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                Depurador.Escribir_Excepción(e.Exception != null ? e.Exception.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                e.ThrowException = false;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void DataGridView_Principal_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    if (e.ColumnIndex > -1 && e.ColumnIndex < DataGridView_Principal.Columns.Count && e.RowIndex > -1 && e.RowIndex < DataGridView_Principal.Rows.Count)
                    {
                        string Nombre = DataGridView_Principal[e.ColumnIndex, e.RowIndex].Value.ToString();
                        if (!string.IsNullOrEmpty(Nombre))
                        {
                            Clipboard.SetText(Nombre);
                            DataGridView_Principal.CurrentCell = DataGridView_Principal[Columna_Nombre_1_13.Index, e.RowIndex];
                            SystemSounds.Asterisk.Play();
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void DataGridView_Principal_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Right)
                {
                    DataGridView.HitTestInfo Info = DataGridView_Principal.HitTest(e.X, e.Y);
                    if (Info.Type == DataGridViewHitTestType.None)
                    {
                        DataGridView_Principal.ClearSelection();
                        DataGridView_Principal.CurrentCell = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.Block_information_viewer;
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

        internal void Filtrar_Bloques()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!Ocupado)
                {
                    Ocupado = true;
                    DataGridView_Principal.Rows.Clear(); // Clear the filtered blocks.
                    if (Minecraft.Bloques.Matriz_Bloques != null && Minecraft.Bloques.Matriz_Bloques.Length > 0)
                    {
                        DataGridView_Principal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                        DataGridView_Principal.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                        foreach (Minecraft.Bloques Bloque in Minecraft.Bloques.Matriz_Bloques)
                        {
                            if ((!Menú_Contextual_Filtrar_No_Obsoletos.Checked || (!Bloque.Obsoleto)) &&
                                (!Menú_Contextual_Filtrar_1_12_2.Checked || (Bloque.Lista_ID != null && Bloque.Lista_ID.Count > 0)) &&
                                (!Menú_Contextual_Filtrar_1_13.Checked || (Bloque.Lista_ID == null || Bloque.Lista_ID.Count <= 0)) &&
                                (!Menú_Contextual_Filtrar_Parciales.Checked || (Bloque.Altura_Diferente)) &&
                                (!Menú_Contextual_Filtrar_Completos.Checked || (!Bloque.Altura_Diferente)) &&
                                (!Menú_Contextual_Filtrar_Transparentes.Checked || (Bloque.Transparencia != Minecraft.Transparencias.Solid)) &&
                                (!Menú_Contextual_Filtrar_Sólidos.Checked || (Bloque.Transparencia == Minecraft.Transparencias.Solid)))
                            {
                                DataGridView_Principal.Rows.Add(new object[]
                                {
                                    Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos(Bloque.Recurso), 32, 32, true, false, CheckState.Checked),
                                    Bloque.Nombre,
                                    Program.Obtener_Nombre_Invertido(Bloque.Nombre),
                                    Bloque.Nombre_1_13,
                                    Bloque.Lista_ID != null && Bloque.Lista_ID.Count > 0 ? (int)Bloque.Lista_ID[0] : -1,
                                    Program.Traducir_Lista_Variables(Bloque.Lista_Data),
                                    Bloque.Color_ARGB,
                                    Bloque.Código_Hash_Color,
                                    Bloque.Altura_Diferente,
                                    Bloque.Transparencia,
                                    Bloque.Obsoleto,
                                    Bloque.Obtención
                               });
                            }
                        }
                        DataGridView_Principal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        DataGridView_Principal.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                        this.Text = Texto_Título + " - [Minecraft blocks known: " + Program.Traducir_Número(DataGridView_Principal.Rows.Count) + "]";
                        if (DataGridView_Principal.Rows.Count > 0)
                        {
                            DataGridView_Principal.Sort(DataGridView_Principal.SortedColumn, DataGridView_Principal.SortOrder != SortOrder.Descending ? ListSortDirection.Ascending : ListSortDirection.Descending);
                            DataGridView_Principal.CurrentCell = DataGridView_Principal[Columna_Nombre.Index, 0];
                        }
                    }
                    Ocupado = false;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Filtrar_Todos_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                Menú_Contextual_Filtrar_No_Obsoletos.Checked = false;
                Menú_Contextual_Filtrar_1_12_2.Checked = false;
                Menú_Contextual_Filtrar_1_13.Checked = false;
                Menú_Contextual_Filtrar_Parciales.Checked = false;
                Menú_Contextual_Filtrar_Completos.Checked = false;
                Menú_Contextual_Filtrar_Transparentes.Checked = false;
                Menú_Contextual_Filtrar_Sólidos.Checked = false;
                Ocupado = false;
                Filtrar_Bloques();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Filtrar_No_Obsoletos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Filtrar_Bloques();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Filtrar_1_12_2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Filtrar_Bloques();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Filtrar_1_13_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Filtrar_Bloques();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Filtrar_Parciales_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Filtrar_Bloques();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Filtrar_Completos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Filtrar_Bloques();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Filtrar_Transparentes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Filtrar_Bloques();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Filtrar_Sólidos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Filtrar_Bloques();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridView_Principal.Rows != null && DataGridView_Principal.Rows.Count > 0)
                {
                    string Texto = null;
                    foreach (DataGridViewRow Fila in DataGridView_Principal.Rows)
                    {
                        Texto += Fila.Cells[Columna_Nombre_1_13.Index].Value as string + "\r\n";
                    }
                    if (!string.IsNullOrEmpty(Texto))
                    {
                        Clipboard.SetText(Texto.TrimEnd("\r\n".ToCharArray()));
                        SystemSounds.Asterisk.Play();
                    }
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Código_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridView_Principal.Rows != null && DataGridView_Principal.Rows.Count > 0)
                {
                    string Texto = null;
                    foreach (DataGridViewRow Fila in DataGridView_Principal.Rows)
                    {
                        string Nombre = Fila.Cells[Columna_Nombre_1_13.Index].Value as string;
                        if (!string.IsNullOrEmpty(Nombre))
                        {
                            foreach (Minecraft.Bloques Bloque in Minecraft.Bloques.Matriz_Bloques)
                            {
                                if (string.Compare(Nombre, Bloque.Nombre_1_13, true) == 0)
                                {
                                    if (Bloque.Lista_ID != null && Bloque.Lista_ID.Count > 0)
                                    {
                                        Texto += "Lista_Paleta_ID.Add(" + Bloque.Lista_ID[0].ToString() + "); Lista_Paleta_Data.Add(" + (Bloque.Lista_Data != null && Bloque.Lista_Data.Count > 0 ? Bloque.Lista_Data[0].ToString() : "0") + "); // " + Nombre + ".\r\n";
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(Texto))
                    {
                        Clipboard.SetText(Texto.TrimEnd("\r\n".ToCharArray()));
                        SystemSounds.Asterisk.Play();
                    }
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
