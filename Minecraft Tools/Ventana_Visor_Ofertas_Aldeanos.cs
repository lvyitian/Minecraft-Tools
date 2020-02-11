using Minecraft_Tools.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Visor_Ofertas_Aldeanos : Form
    {
        public Ventana_Visor_Ofertas_Aldeanos()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Matrix that holds up the 16 basic colors, designed by Jupisoft.
        /// </summary>
        internal static readonly Color[] Matriz_Colores = new Color[]
        {
            Color.FromArgb(0, 0, 0),
            Color.FromArgb(128, 128, 128),
            Color.FromArgb(192, 192, 192),
            Color.FromArgb(255, 255, 255),

            Color.FromArgb(255, 0, 0),
            Color.FromArgb(255, 160, 0),
            Color.FromArgb(255, 255, 0),
            Color.FromArgb(160, 255, 0),

            Color.FromArgb(0, 255, 0),
            Color.FromArgb(0, 255, 160),
            Color.FromArgb(0, 255, 255),
            Color.FromArgb(0, 160, 255),

            Color.FromArgb(0, 0, 255),
            Color.FromArgb(160, 0, 255),
            Color.FromArgb(255, 0, 255),
            Color.FromArgb(255, 0, 160),

            Color.FromArgb(56, 56, 56)
        };

        internal readonly string Texto_Título = "Villager Tradings Viewer by Jupisoft for " + Program.Texto_Usuario;
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

        private void Ventana_Visor_Ofertas_Aldeanos_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.WindowState = FormWindowState.Maximized;
                this.Text = Texto_Título + " - [Original image extracted from the Minecraft wiki]";
                Panel_Picture.KeyDown += Ventana_Visor_Ofertas_Aldeanos_KeyDown;
                for (int Índice = 0; Índice <= 16; Índice++)
                {
                    Menú_Contextual.Items["Menú_Contextual_Fondo_" + Índice.ToString()].Image = Program.Obtener_Imagen_Color(Matriz_Colores[Índice]);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Panel_Picture_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Ventana_Visor_Ofertas_Aldeanos_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Panel_Picture.Select();
                Panel_Picture.Focus();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Ofertas_Aldeanos_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Ventana_Visor_Ofertas_Aldeanos_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Ventana_Visor_Ofertas_Aldeanos_KeyDown(object sender, KeyEventArgs e)
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

        private void Menú_Contextual_Fondos_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem Menú = sender as ToolStripMenuItem;
                if (Menú != null)
                {
                    int Índice = int.Parse(Menú.Name.Replace("Menú_Contextual_Fondo_", null));
                    Panel_Picture.BackColor = Matriz_Colores[Índice];
                    Picture.BackColor = Matriz_Colores[Índice];
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Actual_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Visor_Ofertas_Aldeanos);
                    Bitmap Imagen = new Bitmap(Picture.Width, Picture.Height, PixelFormat.Format24bppRgb);
                    Picture.DrawToBitmap(Imagen, new Rectangle(0, 0, Imagen.Width, Imagen.Height));
                    Imagen.Save(Program.Ruta_Guardado_Imágenes_Visor_Ofertas_Aldeanos + "\\VillagerTradeChart " + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " " + ".png", ImageFormat.Png);
                    Imagen.Dispose();
                    Imagen = null;
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Transparente_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Visor_Ofertas_Aldeanos);
                    Resources.VillagerTradeChart.Save(Program.Ruta_Guardado_Imágenes_Visor_Ofertas_Aldeanos + "\\VillagerTradeChart " + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " " + ".png", ImageFormat.Png);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
