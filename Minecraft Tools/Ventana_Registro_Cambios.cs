using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Registro_Cambios : Form
    {
        public Ventana_Registro_Cambios()
        {
            InitializeComponent();
        }

        

        internal readonly string Texto_Título = "Change Log of " + Program.Texto_Título_Versión + " for " + Program.Texto_Usuario;
        //internal Ayudas Ayuda = Ayudas.Main_window;
        internal float Variable_Zoom = 1f;
        //internal Stopwatch Cronómetro_Memoria = new Stopwatch(); // Turn the text red when over 4 GB
        internal bool Variable_Siempre_Visible = false;
        internal int Total_Cambios = 0;

        private void Ventana_Registro_Cambios_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.WindowState = FormWindowState.Maximized;
                float Zoom = Variable_Zoom;
                if (Registro_Cambios.Cambios.Matriz_Cambios != null && Registro_Cambios.Cambios.Matriz_Cambios.Length > 0)
                {
                    string Texto_Cambios = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang3082{\\fonttbl{\\f0\\fnil\\fcharset0 " + Barra_Estado_Etiqueta_Sugerencia.Font.Name + ";}{\\f1\\fnil\\fcharset0 Calibri;}}\r\n{\\*\\generator Riched20 6.3.9600}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs" + (10 * 2).ToString() + " ";
                    for (int Índice = Registro_Cambios.Cambios.Matriz_Cambios.Length - 1; Índice >= 0; Índice--)
                    {
                        Texto_Cambios += "\\ul \\b [" + Program.Traducir_Fecha_Inglés(Registro_Cambios.Cambios.Matriz_Cambios[Índice].Fecha) + "]\\b0 \\ulnone \\par";
                        if (Registro_Cambios.Cambios.Matriz_Cambios[Índice].Matriz_Líneas != null && Registro_Cambios.Cambios.Matriz_Cambios[Índice].Matriz_Líneas.Length > 0)
                        {
                            foreach (string Línea in Registro_Cambios.Cambios.Matriz_Cambios[Índice].Matriz_Líneas)
                            {
                                if (!string.IsNullOrEmpty(Línea))
                                {
                                    Texto_Cambios += " - " + Línea + "\\par";
                                }
                            }
                            if (Índice < Registro_Cambios.Cambios.Matriz_Cambios.Length - 1) Texto_Cambios += "\\par";
                            Total_Cambios += Registro_Cambios.Cambios.Matriz_Cambios[Índice].Matriz_Líneas.Length;
                        }
                    }
                    Texto_Cambios += "\\pard\\sa200\\sl276\\slmult1\\f1\\fs22\\lang10\\par}";
                    RichTextBox_Cambios.Rtf = Texto_Cambios;
                    RichTextBox_Cambios.ZoomFactor = Zoom != 1.5f ? 1.5f : 2.5f;
                    RichTextBox_Cambios.ZoomFactor = Zoom;
                }
                this.Text = Texto_Título + " - [Updates and changes registered: " + Program.Traducir_Número(Registro_Cambios.Cambios.Matriz_Cambios.Length) + " and " + Program.Traducir_Número(Total_Cambios) + ", Zoom: " + Program.Traducir_Número(Variable_Zoom) + "x]";
                RichTextBox_Cambios.SelectionLength = 0; // Select the end of the text.
                RichTextBox_Cambios.SelectionStart = RichTextBox_Cambios.Text.Length;
                RichTextBox_Cambios.ScrollToCaret(); // Navigate to the bottom of the text.
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Registro_Cambios_Shown(object sender, EventArgs e)
        {
            try
            {
                Temporizador_Principal.Start();
                this.Activate();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Registro_Cambios_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Registro_Cambios_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Registro_Cambios_KeyDown(object sender, KeyEventArgs e)
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
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(RichTextBox_Cambios.Text))
                {
                    RichTextBox_Cambios.Copy();
                    //Clipboard.SetText();
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(RichTextBox_Cambios.Text))
                {
                    RichTextBox_Cambios.SaveFile(Application.StartupPath + "\\Change log " + Program.Obtener_Nombre_Temporal_Sin_Guiones() + ".txt", RichTextBoxStreamType.PlainText);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Guardar_RTF_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(RichTextBox_Cambios.Text))
                {
                    RichTextBox_Cambios.SaveFile(Application.StartupPath + "\\Change log " + Program.Obtener_Nombre_Temporal_Sin_Guiones() + ".rtf", RichTextBoxStreamType.RichText);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Temporizador_Principal_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Variable_Zoom != RichTextBox_Cambios.ZoomFactor)
                {
                    Variable_Zoom = RichTextBox_Cambios.ZoomFactor;
                    //Registro_Guardar_Opciones();
                    this.Text = Texto_Título + " - [Dates and changes registered: " + Program.Traducir_Número(Registro_Cambios.Cambios.Matriz_Cambios.Length) + " and " + Program.Traducir_Número(Total_Cambios) + ", Zoom: " + Program.Traducir_Número(Variable_Zoom) + "x]";
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }
    }
}
