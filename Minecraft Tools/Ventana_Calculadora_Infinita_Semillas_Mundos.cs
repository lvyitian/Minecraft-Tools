using Minecraft_Tools.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Calculadora_Infinita_Semillas_Mundos : Form
    {
        public Ventana_Calculadora_Infinita_Semillas_Mundos()
        {
            InitializeComponent();
        }

        internal static readonly List<List<int>> Listas_Bits_64 = new List<List<int>>(new List<int>[64]
        {
            new List<int>(new int[]{ 1 }),
            new List<int>(new int[]{ 2 }),
            new List<int>(new int[]{ 4 }),
            new List<int>(new int[]{ 8 }),
            new List<int>(new int[]{ 1, 6 }),
            new List<int>(new int[]{ 3, 2 }),
            new List<int>(new int[]{ 6, 4 }),
            new List<int>(new int[]{ 1, 2, 8 }),
            new List<int>(new int[]{ 2, 5, 6 }), // 8
            new List<int>(new int[]{ 5, 1, 2 }),
            new List<int>(new int[]{ 1, 0, 2, 4 }),
            new List<int>(new int[]{ 2, 0, 4, 8 }),
            new List<int>(new int[]{ 4, 0, 9, 6 }),
            new List<int>(new int[]{ 8, 1, 9, 2 }),
            new List<int>(new int[]{ 1, 6, 3, 8, 4 }),
            new List<int>(new int[]{ 3, 2, 7, 6, 8 }),
            new List<int>(new int[]{ 6, 5, 5, 3, 6 }), // 16
            new List<int>(new int[]{ 1, 3, 1, 0, 7, 2 }),
            new List<int>(new int[]{ 2, 6, 2, 1, 4, 4 }),
            new List<int>(new int[]{ 5, 2, 4, 2, 8, 8 }),
            new List<int>(new int[]{ 1, 0, 4, 8, 5, 7, 6 }),
            new List<int>(new int[]{ 2, 0, 9, 7, 1, 5, 2 }),
            new List<int>(new int[]{ 4, 1, 9, 4, 3, 0, 4 }),
            new List<int>(new int[]{ 8, 3, 8, 8, 6, 0, 8 }),
            new List<int>(new int[]{ 1, 6, 7, 7, 7, 2, 1, 6 }), // 24
            new List<int>(new int[]{ 3, 3, 5, 5, 4, 4, 3, 2 }),
            new List<int>(new int[]{ 6, 7, 1, 0, 8, 8, 6, 4 }),
            new List<int>(new int[]{ 1, 3, 4, 2, 1, 7, 7, 2, 8 }),
            new List<int>(new int[]{ 2, 6, 8, 4, 3, 5, 4, 5, 6 }),
            new List<int>(new int[]{ 5, 3, 6, 8, 7, 0, 9, 1, 2 }),
            new List<int>(new int[]{ 1, 0, 7, 3, 7, 4, 1, 8, 2, 4 }),
            new List<int>(new int[]{ 2, 1, 4, 7, 4, 8, 3, 6, 4, 8 }),
            new List<int>(new int[]{ 4, 2, 9, 4, 9, 6, 7, 2, 9, 6 }), // 32
            new List<int>(new int[]{ 8, 5, 8, 9, 9, 3, 4, 5, 9, 2 }),
            new List<int>(new int[]{ 1, 7, 1, 7, 9, 8, 6, 9, 1, 8, 4 }),
            new List<int>(new int[]{ 3, 4, 3, 5, 9, 7, 3, 8, 3, 6, 8 }),
            new List<int>(new int[]{ 6, 8, 7, 1, 9, 4, 7, 6, 7, 3, 6 }),
            new List<int>(new int[]{ 1, 3, 7, 4, 3, 8, 9, 5, 3, 4, 7, 2 }),
            new List<int>(new int[]{ 2, 7, 4, 8, 7, 7, 9, 0, 6, 9, 4, 4 }),
            new List<int>(new int[]{ 5, 4, 9, 7, 5, 5, 8, 1, 3, 8, 8, 8 }),
            new List<int>(new int[]{ 1, 0, 9, 9, 5, 1, 1, 6, 2, 7, 7, 7, 6 }), // 40
            new List<int>(new int[]{ 2, 1, 9, 9, 0, 2, 3, 2, 5, 5, 5, 5, 2 }),
            new List<int>(new int[]{ 4, 3, 9, 8, 0, 4, 6, 5, 1, 1, 1, 0, 4 }),
            new List<int>(new int[]{ 8, 7, 9, 6, 0, 9, 3, 0, 2, 2, 2, 0, 8 }),
            new List<int>(new int[]{ 1, 7, 5, 9, 2, 1, 8, 6, 0, 4, 4, 4, 1, 6 }),
            new List<int>(new int[]{ 3, 5, 1, 8, 4, 3, 7, 2, 0, 8, 8, 8, 3, 2 }),
            new List<int>(new int[]{ 7, 0, 3, 6, 8, 7, 4, 4, 1, 7, 7, 6, 6, 4 }),
            new List<int>(new int[]{ 1, 4, 0, 7, 3, 7, 4, 8, 8, 3, 5, 5, 3, 2, 8 }),
            new List<int>(new int[]{ 2, 8, 1, 4, 7, 4, 9, 7, 6, 7, 1, 0, 6, 5, 6 }), // 48
            new List<int>(new int[]{ 5, 6, 2, 9, 4, 9, 9, 5, 3, 4, 2, 1, 3, 1, 2 }),
            new List<int>(new int[]{ 1, 1, 2, 5, 8, 9, 9, 9, 0, 6, 8, 4, 2, 6, 2, 4 }),
            new List<int>(new int[]{ 2, 2, 5, 1, 7, 9, 9, 8, 1, 3, 6, 8, 5, 2, 4, 8 }),
            new List<int>(new int[]{ 4, 5, 0, 3, 5, 9, 9, 6, 2, 7, 3, 7, 0, 4, 9, 6 }),
            new List<int>(new int[]{ 9, 0, 0, 7, 1, 9, 9, 2, 5, 4, 7, 4, 0, 9, 9, 2 }),
            new List<int>(new int[]{ 1, 8, 0, 1, 4, 3, 9, 8, 5, 0, 9, 4, 8, 1, 9, 8, 4 }),
            new List<int>(new int[]{ 3, 6, 0, 2, 8, 7, 9, 7, 0, 1, 8, 9, 6, 3, 9, 6, 8 }),
            new List<int>(new int[]{ 7, 2, 0, 5, 7, 5, 9, 4, 0, 3, 7, 9, 2, 7, 9, 3, 6 }), // 56
            new List<int>(new int[]{ 1, 4, 4, 1, 1, 5, 1, 8, 8, 0, 7, 5, 8, 5, 5, 8, 7, 2 }),
            new List<int>(new int[]{ 2, 8, 8, 2, 3, 0, 3, 7, 6, 1, 5, 1, 7, 1, 1, 7, 4, 4 }),
            new List<int>(new int[]{ 5, 7, 6, 4, 6, 0, 7, 5, 2, 3, 0, 3, 4, 2, 3, 4, 8, 8 }),
            new List<int>(new int[]{ 1, 1, 5, 2, 9, 2, 1, 5, 0, 4, 6, 0, 6, 8, 4, 6, 9, 7, 6 }),
            new List<int>(new int[]{ 2, 3, 0, 5, 8, 4, 3, 0, 0, 9, 2, 1, 3, 6, 9, 3, 9, 5, 2 }),
            new List<int>(new int[]{ 4, 6, 1, 1, 6, 8, 6, 0, 1, 8, 4, 2, 7, 3, 8, 7, 9, 0, 4 }),
            new List<int>(new int[]{ 9, 2, 2, 3, 3, 7, 2, 0, 3, 6, 8, 5, 4, 7, 7, 5, 8, 0, 8 }),
            //new List<int>(new int[]{ 1, 8, 4, 4, 6, 7, 4, 4, 0, 7, 3, 7, 0, 9, 5, 5, 1, 6, 1, 6 }), // 64
        });

        internal readonly string Texto_Título = "Worlds Seeds Infinite Calculator by Jupisoft for " + Program.Texto_Usuario;

        internal bool Variable_Siempre_Visible = false;

        private void Ventana_Calculadora_Infinita_Semillas_Mundos_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título + " - [It simulates Java hashcode from a string and even better]";
                this.WindowState = FormWindowState.Maximized;
                //Numérico_Base.Minimum = decimal.MinValue;
                //Numérico_Base.Maximum = decimal.MaxValue;
                ComboBox_Texto.Items.Insert(0, Program.Texto_Usuario);
                ComboBox_Texto.Text = Program.Texto_Usuario;
                ComboBox_Texto.Select();
                ComboBox_Texto.Focus();
                ComboBox_Texto.SelectAll();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Calculadora_Infinita_Semillas_Mundos_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Calculadora_Infinita_Semillas_Mundos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Calculadora_Infinita_Semillas_Mundos_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Calculadora_Infinita_Semillas_Mundos_KeyDown(object sender, KeyEventArgs e)
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

        private void TextBox_Texto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Calcular_Semilla(ComboBox_Texto.Text, (int)Numérico_Base.Value);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_Base_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Picture_Base.Image = Numérico_Base.Value == 31m ? Resources.Minecraft : Resources.Memoria;
                Picture_Base.Refresh();
                Calcular_Semilla(ComboBox_Texto.Text, (int)Numérico_Base.Value);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Copiar_Texto_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ComboBox_Texto.Text))
                {
                    Clipboard.SetText(ComboBox_Texto.Text);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Copiar_Semilla_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TextBox_Semilla.Text))
                {
                    Clipboard.SetText(TextBox_Semilla.Text);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void ComboBox_Texto_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    string Texto = ComboBox_Texto.Text;
                    if (!string.IsNullOrEmpty(Texto))
                    {
                        bool Todos_Caracteres_Minúsculas = true;
                        bool Todos_Caracteres_Mayúsculas = true;
                        for (int Índice_Caracter = 0; Índice_Caracter < Texto.Length; Índice_Caracter++)
                        {
                            if (char.IsLetter(Texto[Índice_Caracter]) && (!char.IsLower(Texto[Índice_Caracter]) || !char.IsUpper(Texto[Índice_Caracter])))
                            {
                                if (char.IsLower(Texto[Índice_Caracter])) Todos_Caracteres_Mayúsculas = false;
                                else Todos_Caracteres_Minúsculas = false;
                            }
                        }
                        if (Todos_Caracteres_Minúsculas) // "xisumavoid" to "Xisumavoid"
                        {
                            Texto = Texto.Substring(0, 1).ToUpperInvariant() + (Texto.Length > 1 ? Texto.Substring(1).ToLowerInvariant() : null);
                        }
                        else if (Todos_Caracteres_Mayúsculas) // "XISUMAVOID" to "xisumavoid"
                        {
                            Texto = Texto.ToLowerInvariant();
                        }
                        else // "?" to "XISUMAVOID"
                        {
                            Texto = Texto.ToUpperInvariant();
                        }
                        ComboBox_Texto.Text = Texto;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Numérico_Base_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    Numérico_Base.Value = 31m;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal decimal Calcular_Semilla(string Texto, int Base)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                TextBox_Semilla.Text = null;
                TextBox_Resultados.Text = null;
                if (!string.IsNullOrEmpty(Texto) && Base >= 1)// && Base <= 1024)
                {
                    string Texto_Operaciones = null;
                    List<int> Lista_Base = Calculadora_Infinita.Traducir_Número(Base.ToString());
                    List<int> Lista_Multiplicador = new List<int>(new int[] { 1 });
                    List<int> Lista_Valor = new List<int>(new int[] { 0 });
                    for (int Índice = Texto.Length - 1, Índice_Potencia = 0; Índice >= 0; Índice--, Índice_Potencia++)
                    {
                        try
                        {
                            int Valor_Caracter = (int)Texto[Índice];
                            List<int> Lista_Multiplicación = Calculadora_Infinita.Operación_Multiplicación(Calculadora_Infinita.Traducir_Número(Valor_Caracter.ToString()), Lista_Multiplicador);
                            Lista_Valor = Calculadora_Infinita.Operación_Suma(Lista_Valor, Lista_Multiplicación);
                            if (Índice > 0) Lista_Multiplicador = Calculadora_Infinita.Operación_Multiplicación(Lista_Multiplicador, Lista_Base);
                            Texto_Operaciones += Texto[Índice] + " = " + Valor_Caracter.ToString() + " * (" + Program.Traducir_Número(Base) + "^" + Índice_Potencia.ToString() + ") = " + /*Valor_Caracter.ToString() + " * " + Calculadora_Infinita.Traducir_Número(Lista_Multiplicador) + " = " + */Calculadora_Infinita.Traducir_Número(Lista_Multiplicación) + "\r\n";
                        }
                        catch { break; }
                    }
                    Texto_Operaciones += "\r\nAdding all the previous results will give the full seed.";
                    List<List<int>> Lista_Valor_Binario = Calculadora_Infinita.Operación_Convertir_a_Base(Lista_Valor, new List<int>(new int[] { 2 }));

                    Texto_Operaciones += "\r\n\r\n";
                    //Texto_Operaciones += "Minecraft seed (in ): " + Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Lista_Valor) + "\r\n\r\n";
                    Texto_Operaciones += "Full seed of infinite bits: " + Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Lista_Valor) + "\r\n\r\n";
                    for (int Índice = 64, Índice_1 = 63; Índice >= 1; Índice--, Índice_1--)
                    {
                        Texto_Operaciones += Índice.ToString() + (Índice != 1 ? " bits seed = " : " bit seed = ") + Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Calculadora_Infinita.Operación_Resta(Calculadora_Infinita.Operación_Convertir_desde_Base(Lista_Valor_Binario.GetRange(Math.Max(0, Lista_Valor_Binario.Count - Índice_1), Math.Min(Índice_1, Lista_Valor_Binario.Count)), new List<int>(new int[] { 2 })), Lista_Valor_Binario.Count >= Índice && Lista_Valor_Binario[Lista_Valor_Binario.Count - Índice][0] != 0 ? Listas_Bits_64[Índice_1] : new List<int>(new int[] { 0 }))) + (Índice > 1 ? "\r\n" : null);
                    }
                    
                    TextBox_Semilla.Text = Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Calculadora_Infinita.Operación_Resta(Calculadora_Infinita.Operación_Convertir_desde_Base(Lista_Valor_Binario.GetRange(Math.Max(0, Lista_Valor_Binario.Count - 31), Math.Min(31, Lista_Valor_Binario.Count)), new List<int>(new int[] { 2 })), Lista_Valor_Binario.Count > 31 && Lista_Valor_Binario[Lista_Valor_Binario.Count - 32][0] != 0 ? new List<int>(new int[] { 2, 1, 4, 7, 4, 8, 3, 6, 4, 8 }) : new List<int>(new int[] { 0 })));

                    //Descodificar_Semilla_Numérica(TextBox_Semilla.Text); // 28-04-2018.
                    //Obtener_Texto_Semilla(TextBox_Semilla.Text); // 13-03-2019.

                    TextBox_Resultados.Text = Texto_Operaciones;

                    /*Text_Semilla_Personalizada.Text = Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Calculadora_Infinita.Operación_Resta(Calculadora_Infinita.Operación_Convertir_desde_Base(Lista_Valor_Binario.GetRange(Math.Max(0, Lista_Valor_Binario.Count - (Bits_Global - 1)), Math.Min(Bits_Global - 1, Lista_Valor_Binario.Count)), new List<int>(new int[] { 2 })), Lista_Valor_Binario.Count >= Bits_Global && Lista_Valor_Binario[Lista_Valor_Binario.Count - Bits_Global][0] != 0 ? Calculadora_Infinita.Operación_Potencia(new List<int>(new int[] { 2 }), Calculadora_Infinita.Traducir_Número((Bits_Global - 1).ToString())) : new List<int>(new int[] { 0 })));


                    Text_Semilla_8_Bits.Text = Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Calculadora_Infinita.Operación_Resta(Calculadora_Infinita.Operación_Convertir_desde_Base(Lista_Valor_Binario.GetRange(Math.Max(0, Lista_Valor_Binario.Count - 7), Math.Min(7, Lista_Valor_Binario.Count)), new List<int>(new int[] { 2 })), Lista_Valor_Binario.Count > 7 && Lista_Valor_Binario[Lista_Valor_Binario.Count - 8][0] != 0 ? new List<int>(new int[] { 1, 2, 8 }) : new List<int>(new int[] { 0 })));
                    Text_Semilla_16_Bits.Text = Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Calculadora_Infinita.Operación_Resta(Calculadora_Infinita.Operación_Convertir_desde_Base(Lista_Valor_Binario.GetRange(Math.Max(0, Lista_Valor_Binario.Count - 15), Math.Min(15, Lista_Valor_Binario.Count)), new List<int>(new int[] { 2 })), Lista_Valor_Binario.Count > 15 && Lista_Valor_Binario[Lista_Valor_Binario.Count - 16][0] != 0 ? new List<int>(new int[] { 3, 2, 7, 6, 8 }) : new List<int>(new int[] { 0 })));
                    Text_Semilla_32_Bits.Text = Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Calculadora_Infinita.Operación_Resta(Calculadora_Infinita.Operación_Convertir_desde_Base(Lista_Valor_Binario.GetRange(Math.Max(0, Lista_Valor_Binario.Count - 31), Math.Min(31, Lista_Valor_Binario.Count)), new List<int>(new int[] { 2 })), Lista_Valor_Binario.Count > 31 && Lista_Valor_Binario[Lista_Valor_Binario.Count - 32][0] != 0 ? new List<int>(new int[] { 2, 1, 4, 7, 4, 8, 3, 6, 4, 8 }) : new List<int>(new int[] { 0 })));
                    Text_Semilla_64_Bits.Text = Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Calculadora_Infinita.Operación_Resta(Calculadora_Infinita.Operación_Convertir_desde_Base(Lista_Valor_Binario.GetRange(Math.Max(0, Lista_Valor_Binario.Count - 63), Math.Min(63, Lista_Valor_Binario.Count)), new List<int>(new int[] { 2 })), Lista_Valor_Binario.Count > 63 && Lista_Valor_Binario[Lista_Valor_Binario.Count - 64][0] != 0 ? new List<int>(new int[] { 9, 2, 2, 3, 3, 7, 2, 0, 3, 6, 8, 5, 4, 7, 7, 5, 8, 0, 8 }) : new List<int>(new int[] { 0 })));
                    //Text_Semilla_128_Bits.Text = Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Calculadora_Infinita.Operación_Resta(Calculadora_Infinita.Operación_Convertir_desde_Base(Lista_Valor_Binario.GetRange(Math.Max(0, Lista_Valor_Binario.Count - 127), Math.Min(127, Lista_Valor_Binario.Count)), new List<int>(new int[] { 2 })), Lista_Valor_Binario.Count > 127 && Lista_Valor_Binario[Lista_Valor_Binario.Count - 128][0] != 0 ? Ventana_Calculadora_Infinita.Traducir_Número("170141183460469231731687303715884105728") : new List<int>(new int[] { 0 }))); ;
                    //Text_Semilla_256_Bits.Text = Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Calculadora_Infinita.Operación_Resta(Calculadora_Infinita.Operación_Convertir_desde_Base(Lista_Valor_Binario.GetRange(Math.Max(0, Lista_Valor_Binario.Count - 255), Math.Min(255, Lista_Valor_Binario.Count)), new List<int>(new int[] { 2 })), Lista_Valor_Binario.Count > 255 && Lista_Valor_Binario[Lista_Valor_Binario.Count - 256][0] != 0 ? Ventana_Calculadora_Infinita.Traducir_Número("170141183460469231731687303715884105728") : new List<int>(new int[] { 0 }))); ;
                    Text_Semilla_Completa.Text = Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Lista_Valor);*/

                    Lista_Valor_Binario = null;
                    Lista_Valor = null;
                    Lista_Multiplicador = null;
                    Lista_Base = null;
                }
                /*Text_Operaciones.Refresh();
                Text_Semilla_8_Bits.Refresh();
                Text_Semilla_16_Bits.Refresh();
                Text_Semilla_32_Bits.Refresh();
                Text_Semilla_64_Bits.Refresh();
                Text_Semilla_Completa.Refresh();*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            finally { this.Cursor = Cursors.Default; }
            return 0m;
        }

        /// <summary>
        /// 2019_03_13_01_03_25_669.
        /// </summary>
        internal static void Obtener_Texto_Semilla(string Texto_Semilla)
        {
            try
            {
                return; // Needs more time to be finished...
                double Semilla = (double)long.Parse(Texto_Semilla);
                if (Semilla < 0d) Semilla += int.MaxValue + 1d;
                List<double> Lista_Valores = new List<double>();
                for (int Índice_Caracter = 0; Índice_Caracter < 256; Índice_Caracter++)
                {
                    char Caracter = (char)Índice_Caracter;
                    //if ((Caracter >= 'A' && Caracter <= 'Z') || (Caracter >= 'a' && Caracter <= 'z'))
                    if (!char.IsControl(Caracter))
                    {
                        Lista_Valores.Add((double)Índice_Caracter);
                    }
                }
                double Potencia_Máxima = 0d;
                for (;;)
                {
                    if (Lista_Valores[0] * Math.Pow(31, Potencia_Máxima) <= Semilla)
                    {
                        Potencia_Máxima++;
                    }
                    else break;
                }
                //MessageBox.Show(Potencia_Máxima.ToString());
                //if (Lista_Caracteres.Count > 1) Lista_Caracteres.Sort();
                List<char> Lista_Caracteres = new List<char>();
                double Valor = 0d;
                for (double Índice_Potencia = Potencia_Máxima - 0d; Índice_Potencia >= 0d; Índice_Potencia--)
                {
                    double Valor_Temporal = Valor;
                    for (int Índice_Caracter = Lista_Valores.Count - 1; Índice_Caracter >= 0; Índice_Caracter--)
                    //for (int Índice_Caracter = 0; Índice_Caracter < Lista_Valores.Count; Índice_Caracter++)
                    {
                        double Potencia = Math.Pow(31, Índice_Potencia);
                        double Valor_Actual = Lista_Valores[Índice_Caracter] * Potencia;
                        Valor_Temporal += Valor_Actual;
                        if (Valor_Temporal <= Semilla)
                        {
                            Lista_Caracteres.Add((char)Lista_Valores[Índice_Caracter]);
                            Valor += Valor_Actual;
                            break;
                        }
                    }
                    //if (Valor == Semilla) break;
                }
                MessageBox.Show("\"" + new string(Lista_Caracteres.ToArray()) + "\"", Valor.ToString());
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static decimal Calcular_Semilla(string Texto)
        {
            try
            {
                if (!string.IsNullOrEmpty(Texto))
                {
                    int Base = 31;
                    List<int> Lista_Base = Calculadora_Infinita.Traducir_Número(Base.ToString());
                    List<int> Lista_Multiplicador = new List<int>(new int[] { 1 });
                    List<int> Lista_Valor = new List<int>(new int[] { 0 });
                    for (int Índice = Texto.Length - 1, Índice_Potencia = 0; Índice >= 0; Índice--, Índice_Potencia++)
                    {
                        try
                        {
                            int Valor_Caracter = (int)Texto[Índice];
                            List<int> Lista_Multiplicación = Calculadora_Infinita.Operación_Multiplicación(Calculadora_Infinita.Traducir_Número(Valor_Caracter.ToString()), Lista_Multiplicador);
                            Lista_Valor = Calculadora_Infinita.Operación_Suma(Lista_Valor, Lista_Multiplicación);
                            if (Índice > 0) Lista_Multiplicador = Calculadora_Infinita.Operación_Multiplicación(Lista_Multiplicador, Lista_Base);
                        }
                        catch { break; }
                    }
                    List<List<int>> Lista_Valor_Binario = Calculadora_Infinita.Operación_Convertir_a_Base(Lista_Valor, new List<int>(new int[] { 2 }));
                    decimal Semilla = decimal.Parse(Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Calculadora_Infinita.Operación_Resta(Calculadora_Infinita.Operación_Convertir_desde_Base(Lista_Valor_Binario.GetRange(Math.Max(0, Lista_Valor_Binario.Count - 31), Math.Min(31, Lista_Valor_Binario.Count)), new List<int>(new int[] { 2 })), Lista_Valor_Binario.Count > 31 && Lista_Valor_Binario[Lista_Valor_Binario.Count - 32][0] != 0 ? new List<int>(new int[] { 2, 1, 4, 7, 4, 8, 3, 6, 4, 8 }) : new List<int>(new int[] { 0 }))));
                    Lista_Valor_Binario = null;
                    Lista_Valor = null;
                    Lista_Multiplicador = null;
                    Lista_Base = null;
                    return Semilla;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return 0m;
        }

        /// <summary>
        /// Numerical seed decoder (28-04-2018) [EXPERIMENTAL]. Note: since Minecraft (or Java) trims the numerical seeds to 32 bits. In the seed "Xisumavoid" the first 5 characters will be lost forever, but the last 5 ones should be recovered from the 32 bits seed "1614748699".
        /// </summary>
        /// <param name="Texto_Semilla"></param>
        internal void Descodificar_Semilla_Numérica(string Texto_Semilla)
        {
            try
            {
                return;
                Texto_Semilla = "1614748699"; // Xisumavoid
                decimal Semilla = decimal.Parse(Texto_Semilla);
                List<char> Lista_Caracteres = new List<char>("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());
                Lista_Caracteres.Sort();
                List<string> Lista_Semillas = new List<string>();
                double Base = 31d;
                int Longitud_Mínima = 10;
                int Longitud_Máxima = 10;
                for (int Índice_Longitud = Longitud_Máxima; Índice_Longitud >= Longitud_Mínima; Índice_Longitud--)
                {
                    string Texto = null;
                    decimal Valor_Actual = Semilla;
                    for (int Índice_Posición = Longitud_Máxima - 1; Índice_Posición >= 0; Índice_Posición--)
                    {
                        decimal Valor_Base = (decimal)Math.Pow(Base, (double)Índice_Posición);
                        for (int Índice_Caracter = 0; Índice_Caracter < Lista_Caracteres.Count; Índice_Caracter++)
                        {
                            decimal Valor = (decimal)Lista_Caracteres[Índice_Caracter] * Valor_Base;
                            if (Valor < Valor_Actual)
                            {
                                Valor_Actual -= Valor;
                                Texto = Lista_Caracteres[Índice_Caracter] + Texto;
                                break;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(Texto) && !Lista_Semillas.Contains(Texto)) Lista_Semillas.Add(Texto);
                }
                if (Lista_Semillas.Count > 0)
                {
                    string Texto = null;
                    for (int Índice_Semilla = 0; Índice_Semilla < Lista_Semillas.Count; Índice_Semilla++)
                    {
                        Texto += Lista_Semillas[Índice_Semilla];
                    }
                    Clipboard.SetText(Texto);
                    MessageBox.Show(Texto);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }

        }
    }
}
