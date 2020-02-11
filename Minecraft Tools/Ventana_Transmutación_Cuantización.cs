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
    public partial class Ventana_Transmutación_Cuantización : Form
    {
        public Ventana_Transmutación_Cuantización()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título_Transmutación = "Transmutation for " + Program.Texto_Usuario + " by Jupisoft";
        internal readonly string Texto_Título_Cuantización = "Quantization for " + Program.Texto_Usuario + " by Jupisoft";
        internal bool Variable_Siempre_Visible = false;
        /// <summary>
        /// Used to know if a transmutation or quantization already exists. Currently not used.
        /// </summary>
        internal SortedDictionary<string, string> Diccionario_Transmutación_Cuantización = new SortedDictionary<string, string>();
        /// <summary>
        /// The returned transmutation or quantization selected by the user.
        /// </summary>
        internal KeyValuePair<string, string> Transmutación_Cuantización = new KeyValuePair<string, string>(null, null);
        /// <summary>
        /// Used to know if the user must select a transmutation (false) or quantization (true).
        /// </summary>
        internal bool Cuantización = false;

        private void Ventana_Transmutación_Cuantización_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = (!Cuantización ? Texto_Título_Transmutación : Texto_Título_Cuantización) + " - [Blocks: " + Program.Traducir_Número(Minecraft.Bloques.Matriz_Bloques.Length) + "]";
                foreach (Minecraft.Bloques Bloque in Minecraft.Bloques.Matriz_Bloques)
                {
                    ComboBox_Entrada.Items.Add(Bloque.Nombre);
                    if (!Cuantización) ComboBox_Salida.Items.Add(Bloque.Nombre);
                }
                if (ComboBox_Entrada.Items.Count > 0) ComboBox_Entrada.SelectedIndex = Program.Rand.Next(0, ComboBox_Entrada.Items.Count);
                if (ComboBox_Salida.Items.Count > 0) ComboBox_Salida.SelectedIndex = Program.Rand.Next(0, ComboBox_Salida.Items.Count);
                if (Cuantización) // Adapt the window to the quantization selection mode.
                {
                    Etiqueta_Entrada.Text = "Block:";
                    Etiqueta_Salida.Visible = false;
                    Picture_Salida.Visible = false;
                    ComboBox_Salida.Visible = false;
                    Etiqueta_Salida.Enabled = false;
                    Picture_Salida.Enabled = false;
                    ComboBox_Salida.Enabled = false;
                    this.Height -= 26;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Transmutación_Cuantización_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Transmutación_Cuantización_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Transmutación_Cuantización_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Transmutación_Cuantización_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Transmutación_Cuantización_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Alt && !e.Control && !e.Shift)
                {
                    if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Delete)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Botón_Cancelar.PerformClick();
                    }
                    else if (e.KeyCode == Keys.Enter)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Botón_Aceptar.PerformClick();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void ComboBox_Entrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Entrada.SelectedIndex > -1)
                {
                    Picture_Entrada.Image = Minecraft.Bloques.Matriz_Bloques[ComboBox_Entrada.SelectedIndex].Imagen_Textura;
                    Transmutación_Cuantización = new KeyValuePair<string, string>(Minecraft.Bloques.Matriz_Bloques[ComboBox_Entrada.SelectedIndex].Nombre_1_13, !Cuantización ? Minecraft.Bloques.Matriz_Bloques[ComboBox_Salida.SelectedIndex].Nombre_1_13 : " ");
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void ComboBox_Salida_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Salida.SelectedIndex > -1)
                {
                    Picture_Salida.Image = Minecraft.Bloques.Matriz_Bloques[ComboBox_Salida.SelectedIndex].Imagen_Textura;
                    Transmutación_Cuantización = new KeyValuePair<string, string>(Minecraft.Bloques.Matriz_Bloques[ComboBox_Entrada.SelectedIndex].Nombre_1_13, !Cuantización ? Minecraft.Bloques.Matriz_Bloques[ComboBox_Salida.SelectedIndex].Nombre_1_13 : " ");
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void ComboBox_Entrada_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    ComboBox_Entrada.SelectedIndex = Program.Rand.Next(0, ComboBox_Entrada.Items.Count);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void ComboBox_Salida_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    if (!Cuantización) ComboBox_Salida.SelectedIndex = Program.Rand.Next(0, ComboBox_Salida.Items.Count);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Restablecer_Click(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Entrada.SelectedIndex = 0;
                if (!Cuantización) ComboBox_Salida.SelectedIndex = 0;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Aleatorizar_Click(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Entrada.SelectedIndex = Program.Rand.Next(0, ComboBox_Entrada.Items.Count);
                if (!Cuantización) ComboBox_Salida.SelectedIndex = Program.Rand.Next(0, ComboBox_Salida.Items.Count);
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
    }
}
