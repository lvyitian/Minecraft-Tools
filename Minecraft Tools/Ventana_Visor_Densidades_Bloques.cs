using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Visor_Densidades_Bloques : Form
    {
        public Ventana_Visor_Densidades_Bloques()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Block Densities and Y Levels Viewer by Jupisoft for " + Program.Texto_Usuario;
        internal bool Variable_Siempre_Visible = false;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;

        internal long Total_Bloques = 0L;
        internal Dictionary<short, long> Diccionario_Densidades = new Dictionary<short, long>();
        internal Dictionary<short, List<long>> Diccionario_Niveles_Y = new Dictionary<short, List<long>>();

        private void Ventana_Visor_Densidades_Bloques_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                if (Total_Bloques > 0 && Diccionario_Densidades.Count > 0 && Diccionario_Niveles_Y.Count > 0)
                {
                    foreach (KeyValuePair<short, long> Entrada in Diccionario_Densidades)
                    {
                        string Texto_Mejores_Niveles_Y = null;
                        string Texto_Niveles_Y = null;
                        long Máximo = long.MinValue;
                        for (int Índice_Y = 0; Índice_Y < 256; Índice_Y++)
                        {
                            if (Diccionario_Niveles_Y[Entrada.Key][Índice_Y] > Máximo) Máximo = Diccionario_Niveles_Y[Entrada.Key][Índice_Y];
                            if (Diccionario_Niveles_Y[Entrada.Key][Índice_Y] > 0L) Texto_Niveles_Y += Índice_Y.ToString() + ", ";
                        }
                        for (int Índice_Y = 0; Índice_Y < 256; Índice_Y++)
                        {
                            if (Diccionario_Niveles_Y[Entrada.Key][Índice_Y] >= Máximo)
                            {
                                Texto_Mejores_Niveles_Y += Índice_Y.ToString() + ", ";
                            }
                        }
                        if (!string.IsNullOrEmpty(Texto_Mejores_Niveles_Y)) Texto_Mejores_Niveles_Y = Texto_Mejores_Niveles_Y.TrimEnd(", ".ToCharArray());
                        if (!string.IsNullOrEmpty(Texto_Niveles_Y)) Texto_Niveles_Y = Texto_Niveles_Y.TrimEnd(", ".ToCharArray());
                        DataGridView_Principal.Rows.Add(new object[]
                        {
                            Program.Obtener_Imagen_Recursos(Minecraft.Bloques.Diccionario_Índice_Nombre[Entrada.Key]),
                            Program.Traducir_Texto_Mayúsculas_Minúsculas_Automáticamente(Minecraft.Bloques.Diccionario_Índice_Nombre[Entrada.Key].Substring(10).Replace('_', ' ')),
                            Entrada.Value,
                            Math.Round(((decimal)Entrada.Value * 100m) / (decimal)Total_Bloques, 10, MidpointRounding.AwayFromZero),
                            Math.Round((decimal)Total_Bloques / (decimal)Entrada.Value, 10, MidpointRounding.AwayFromZero),
                            Texto_Mejores_Niveles_Y,
                            Texto_Niveles_Y,
                            0L,
                            0L,
                            0L
                        });
                    }
                    DataGridView_Principal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    DataGridView_Principal.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    DataGridView_Principal.Sort(Columna_Nombre, ListSortDirection.Ascending);
                    if (DataGridView_Principal.Rows.Count > 0)
                    {
                        this.Text = Texto_Título + " - [Unique blocks found: " + Program.Traducir_Número(DataGridView_Principal.Rows.Count) + "]";
                        DataGridView_Principal.CurrentCell = DataGridView_Principal[0, 0];
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Densidades_Bloques_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                //Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Densidades_Bloques_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Densidades_Bloques_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Densidades_Bloques_KeyDown(object sender, KeyEventArgs e)
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

        private void DataGridView_Principal_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                Depurador.Escribir_Excepción(e.Exception != null ? e.Exception.ToString() : null);
                e.ThrowException = false;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
