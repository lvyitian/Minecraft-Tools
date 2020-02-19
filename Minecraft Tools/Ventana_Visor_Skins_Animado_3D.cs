using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Visor_Skins_Animado_3D : Form
    {
        public Ventana_Visor_Skins_Animado_3D()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Animated 3D Skin Viewer by Jupisoft for " + Program.Texto_Usuario;
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

        internal static bool Variable_Mostrar_Cabeza = true;
        internal static bool Variable_Mostrar_Cuerpo = true;
        internal static bool Variable_Mostrar_Brazo_Izquierdo = true;
        internal static bool Variable_Mostrar_Brazo_Derecho = true;
        internal static bool Variable_Mostrar_Pierna_Izquierda = true;
        internal static bool Variable_Mostrar_Pierna_Derecha = true;
        internal static bool Variable_Mostrar_Capa = true;
        internal static bool Variable_Mostrar_Pelo = true;
        internal static bool Variable_Mostrar_Chaqueta = true;
        internal static bool Variable_Mostrar_Pantalones = true;
        internal static bool Variable_Mostrar_Desnudo = false;
        internal static bool Variable_Mostrar_Barbilla_Invertida = false;
        internal static bool Variable_Mostrar_Máscara_Cabeza = false;

        private void Ventana_Visor_Skins_Animado_3D_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título + " - [Drag and drop any image containing a Minecraft skin]";
                this.WindowState = FormWindowState.Maximized;
                Ocupado = true;
                CheckedListBox_Skin.SetItemChecked(0, Variable_Mostrar_Cabeza);
                CheckedListBox_Skin.SetItemChecked(1, Variable_Mostrar_Cuerpo);
                CheckedListBox_Skin.SetItemChecked(2, Variable_Mostrar_Brazo_Izquierdo);
                CheckedListBox_Skin.SetItemChecked(3, Variable_Mostrar_Brazo_Derecho);
                CheckedListBox_Skin.SetItemChecked(4, Variable_Mostrar_Pierna_Izquierda);
                CheckedListBox_Skin.SetItemChecked(5, Variable_Mostrar_Pierna_Derecha);
                CheckedListBox_Skin.SetItemChecked(6, Variable_Mostrar_Capa);
                CheckedListBox_Skin.SetItemChecked(7, Variable_Mostrar_Pelo);
                CheckedListBox_Skin.SetItemChecked(8, Variable_Mostrar_Chaqueta);
                CheckedListBox_Skin.SetItemChecked(9, Variable_Mostrar_Pantalones);
                CheckedListBox_Skin.SetItemChecked(10, Variable_Mostrar_Desnudo);
                CheckedListBox_Skin.SetItemChecked(11, Variable_Mostrar_Barbilla_Invertida);
                CheckedListBox_Skin.SetItemChecked(12, Variable_Mostrar_Máscara_Cabeza);
                /*for (int Índice = 0; Índice < CheckedListBox_Skin.Items.Count; Índice++)
                {
                    CheckedListBox_Skin.SetItemChecked(Índice, true);
                }*/
                Ocupado = false;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Skins_Animado_3D_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Skins_Animado_3D_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Skins_Animado_3D_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Skins_Animado_3D_KeyDown(object sender, KeyEventArgs e)
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
    }
}
