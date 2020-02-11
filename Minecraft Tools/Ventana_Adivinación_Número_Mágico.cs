using Minecraft_Tools.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Adivinación_Número_Mágico : Form
    {
        public Ventana_Adivinación_Número_Mágico()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Magic Card Guessing by Jupisoft for " + Program.Texto_Usuario;
        internal bool Variable_Siempre_Visible = false;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;
        internal static Stopwatch FPS_Cronómetro = Stopwatch.StartNew();
        internal long FPS_Segundo_Anterior = 0L;
        internal long FPS_Temporal = 0L;
        internal long FPS_Real = 0L;
        internal bool Ocupado = false;
        internal float Variable_Zoom = 1f;
        internal int Ancho_Progreso = 0;
        internal SolidBrush Pincel_Progreso = new SolidBrush(Color.FromArgb(255, 6, 176, 37)); // Default progress bar color in Windows 8.1.
        internal Graphics Pintar_Progreso = null; // Simulated progress bar, a lot quicker.
        internal List<int> Lista_21_Cartas_Usadas = null; // Variables used in the game.
        internal List<int> Lista_21_Cartas = null;
        internal bool Variable_Aleatorizar_Cartas = false;
        internal int Ronda = 0;
        internal List<int> Lista_Montón_Izquierdo = null;
        internal List<int> Lista_Montón_Centro = null;
        internal List<int> Lista_Montón_Derecho = null;
        internal int Índice_Reparto = 0;

        private void Ventana_Adivinación_Número_Mágico_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título + " - [Zoom: " + Program.Traducir_Número(Variable_Zoom) + "x]";
                this.WindowState = FormWindowState.Maximized;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
                float Zoom = Variable_Zoom;
                string Texto_Ayuda = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang3082{\\fonttbl{\\f0\\fnil\\fcharset0 " + Barra_Estado_Etiqueta_Sugerencia.Font.Name + ";}{\\f1\\fnil\\fcharset0 Calibri;}}\r\n{\\*\\generator Riched20 6.3.9600}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs" + (10 * 2).ToString() + " " +
                "\\ul \\b [How does this magick card trick works?]\\b0 \\ulnone \\par\\par\r\n" +
                "\\b - \"Can I use this trick in real life with my own cards?\":\\b0  absolutely. In fact this trick comes from there and now is inside an application, possibly for the first time.\\par\\par\r\n" +
                "\\b - \"How to make it work\":\\b0  pick 21 cards of your choice, but make sure that all are different or it won't work. Then if you want you can mix your cards, but it's not necessary. After that you need another person in front of you, then start placing one by one the cards on 3 different piles, with it's face showing to the other person. This step is crucial and you always need to reproduce the same order, whichever you choose, so you can start by first placing a card on the left pile, then the center and right. Or you might start at the right pile, then center and left. But always one card at a time and in the same pile order, so each of the next 3 cards will go on a different pile but always in your picked order. While you place your 21 cards divided in 3 piles (7 cards per pile), ask your partner to look at them and choose a favorite card, but tell to not reveal it yet. Once finished the 21 cards in 3 piles, ask to your partner for the pile that contains the favorite card. But you shouldn't know that card yet. Once you know the selected pile, pick that 7 cards and then place the other 2 piles without mixing any the cards, one pile on top of the picked pile and another on the bottom. So it will be like making a sandwich, always with the picked pile on the center. After that repeat the whole process another 2 times, making a total of 3 iterations. So start placing again the cards one by one in 3 piles and ask again to your partner to look at the previous favorite card, once you know in which pile it's located, leave that pile in the center and put the rest of piles on top and bottom. Then once again everything, and once you have done 3 iterations and you have all your cards ordered into a single pile of 21 cards, take either from the top or bottom, but just from one of those sides, 7 cards, which equals to one of the piles, then remove another 3 cards, which will let you with the center card of the 21. Pick that card and show it to your partner. If done well you'll have guessed the favorite card, leaving your partner amazed, so congratulations, you're a true wizard now! Also you can try the trick just by yourself to practice and learn how do it properly first. Also I know that other numbers of cards might work also, but the easiest one seems to be 21.\\par\\par\r\n" +
                "\\b - \"The mathemathics behind the trick\":\\b0  since you make 3 iterations of the 21 cards, divided in 3 piles of 7 cards, then pick the favorite pile and placing it in the center, what's doing each time is to discard 2 cards, one from the top and another from the bottom, so basically in the whole process 6 cards are being moved or discarded, so you end up always with the favorite card in the center of the 21 cards or the pile of 7 cards. It's kinda cool to be able to learn the mathemathical secrets behind that trick. So now you know the secret, now start to amaze everyone, they'll love this trick. Thanks for reading, and if you still have questions, please send an e-mail to Jupitermauro@gmail.com.\\par\\par\r\n" +
                "\\b - \"Image copyright\":\\b0  the copyright of the 79 cards shown on this tool is by Heraclio Fournier, from Vitoria, Spain. They are only shown as a demonstration of the magick card trick, so please also search for them because they are amongst the best cards in the world. Also these cards come from \"Tarot\" and are also used in another tool of this application, so for the sake of the application size they were used again on this tool.\\par\r\n" +
                "\\pard\\sa200\\sl276\\slmult1\\f1\\fs22\\lang10\\par\r\n}";
                RichTextBox_Ayuda.Rtf = Texto_Ayuda;
                RichTextBox_Ayuda.ZoomFactor = Zoom != 1.5f ? 1.5f : 2.5f;
                RichTextBox_Ayuda.ZoomFactor = Zoom;
                if (Program.Matriz_Cartas_79 == null/* && Directory.Exists(Program.Ruta_Cartas_79)*/)
                {
                    Program.Matriz_Cartas_79 = new Bitmap[79];
                    Program.Matriz_Cartas_79[0] = new Bitmap(1, 1, PixelFormat.Format32bppArgb); // Empty.
                    for (int Índice = 1; Índice < 79; Índice++)
                    {
                        try
                        {
                            Bitmap Imagen_Texto = Program.Obtener_Imagen_Texto(Índice.ToString(), new Font("Arial", 128f, FontStyle.Bold), Color.Empty, Color.Black, TextRenderingHint.AntiAlias);
                            Rectangle Rectángulo = Program.Buscar_Zona_Recorte_Imagen(Imagen_Texto, Color.Empty);
                            if (Rectángulo.X > -1 && Rectángulo.Y > -1 && Rectángulo.X < int.MaxValue && Rectángulo.Y < int.MaxValue && Rectángulo.Width > 0 && Rectángulo.Height > 0)
                            {
                                Imagen_Texto = Imagen_Texto.Clone(Rectángulo, PixelFormat.Format32bppArgb);
                                Program.Matriz_Cartas_79[Índice] = Imagen_Texto;
                            }
                            //Bitmap Imagen = Program.Obtener_Imagen_Ruta(Program.Ruta_Cartas_79 + "\\Card_" + Índice.ToString() + ".png");
                            //if (Imagen != null) Program.Matriz_Cartas_79[Índice] = Imagen;
                            //else Program.Matriz_Cartas_79[Índice] = new Bitmap(224, 400, PixelFormat.Format24bppRgb);
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Program.Matriz_Cartas_79[Índice] = new Bitmap(224, 400, PixelFormat.Format32bppArgb); continue; }
                    }
                }
                if (Program.Matriz_Cartas_79 != null)
                {
                    Picture_1.Image = Program.Matriz_Cartas_79[0].Clone() as Bitmap;
                    Picture_2.Image = Program.Matriz_Cartas_79[0].Clone() as Bitmap;
                    Picture_3.Image = Program.Matriz_Cartas_79[0].Clone() as Bitmap;
                }
                Ancho_Progreso = Picture_Progreso.ClientSize.Width;
                Picture_Progreso.Image = new Bitmap(Ancho_Progreso, 18, PixelFormat.Format32bppArgb);
                Pintar_Progreso = Graphics.FromImage(Picture_Progreso.Image);
                Pintar_Progreso.CompositingMode = CompositingMode.SourceCopy;
                Pintar_Progreso.CompositingQuality = CompositingQuality.HighQuality;
                Pintar_Progreso.InterpolationMode = InterpolationMode.NearestNeighbor;
                Pintar_Progreso.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar_Progreso.SmoothingMode = SmoothingMode.HighQuality;
                Pintar_Progreso.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Adivinación_Número_Mágico_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Adivinación_Número_Mágico_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Adivinación_Número_Mágico_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Adivinación_Número_Mágico_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Adivinación_Número_Mágico_KeyDown(object sender, KeyEventArgs e)
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
                    else if (e.KeyCode == Keys.Back)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Jugar(-1);
                    }
                    else if (e.KeyCode == Keys.Enter)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Jugar(0);
                    }
                    else if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Jugar(1);
                    }
                    else if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Jugar(2);
                    }
                    else if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Jugar(3);
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    Jugar(1);
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    Jugar(0);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_2_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    Jugar(2);
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    Jugar(0);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_3_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    Jugar(3);
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    Jugar(0);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.Main_window;
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
                Program.Crear_Carpetas(Program.Ruta_Minecraft);
                Program.Ejecutar_Ruta(Program.Ruta_Guardado_Minecraft, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Jugar(0);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Montón_1_Click(object sender, EventArgs e)
        {
            try
            {
                Jugar(1);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Montón_2_Click(object sender, EventArgs e)
        {
            try
            {
                Jugar(2);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Montón_3_Click(object sender, EventArgs e)
        {
            try
            {
                Jugar(3);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Mostrar_Ayuda_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Tabla_Principal.Visible = !Menú_Contextual_Mostrar_Ayuda.Checked;
                RichTextBox_Ayuda.Visible = Menú_Contextual_Mostrar_Ayuda.Checked;
                RichTextBox_Ayuda.Select();
                RichTextBox_Ayuda.Focus();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Aleatorizar_Cartas_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Aleatorizar_Cartas = Menú_Contextual_Aleatorizar_Cartas.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Repartir_Menos_Velocidad_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Temporizador_Cartas.Interval = (!Menú_Contextual_Repartir_Menos_Velocidad.Checked ? 6000 : 12000) / 21; // X (milli)seconds divided by 21 cards = (milli)seconds per card.
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Restablecer_Click(object sender, EventArgs e)
        {
            try
            {
                Jugar(-1);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(RichTextBox_Ayuda.Text))
                {
                    RichTextBox_Ayuda.Copy();
                    SystemSounds.Asterisk.Play();
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Registro_Cargar_Opciones()
        {
            try
            {
                /*RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Template");

                // bool
                try { Variable_ = bool.Parse((string)Clave.GetValue("Variable_", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_ = true; }

                // int
                try { Variable_ = (int)Clave.GetValue("Variable_", 0); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_ = 0; }
                
                // Correct any bad value after loading:
                if ((int)Variable_ < 0 || (int)Variable_ > (int)Variables.Variable) Variable_ = Variables.Variable;

                // Apply all the loaded values:
                ComboBox_Variable_.SelectedIndex = (int)Variable_;

                Menú_Contextual_Variable_.Checked = Variable_;*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Registro_Guardar_Opciones()
        {
            try
            {
                /*RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Template");
                string[] Matriz_Nombres = Clave.GetValueNames();
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                    {
                        Clave.DeleteValue(Matriz_Nombres[Índice]);
                    }
                }
                Matriz_Nombres = null;
                
                // bool
                try { Clave.SetValue("Variable_", Variable_doDaylightCycle.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                // int
                try { Clave.SetValue("Tickspeed", (int)Variable_, RegistryValueKind.DWord); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Registro_Restablecer_Opciones()
        {
            try
            {
                /*RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Template");
                string[] Matriz_Nombres = Clave.GetValueNames();
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                    {
                        try { Clave.DeleteValue(Matriz_Nombres[Índice]); }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                    }
                    Matriz_Nombres = null;
                }
                Clave.Close();
                Clave = null;*/
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
                long FPS_Milisegundo = FPS_Cronómetro.ElapsedMilliseconds;
                long FPS_Segundo = FPS_Milisegundo / 1000L;
                if (FPS_Segundo != FPS_Segundo_Anterior)
                {
                    FPS_Segundo_Anterior = FPS_Segundo;
                    FPS_Real = FPS_Temporal;
                    Barra_Estado_Etiqueta_FPS.Text = FPS_Real.ToString() + " FPS";
                    FPS_Temporal = 0L;
                }
                FPS_Temporal++;
                try
                {
                    if (Variable_Zoom != RichTextBox_Ayuda.ZoomFactor)
                    {
                        Variable_Zoom = RichTextBox_Ayuda.ZoomFactor;
                        Registro_Guardar_Opciones();
                        this.Text = Texto_Título + " - [Zoom: " + Program.Traducir_Número(Variable_Zoom) + "x]";
                    }
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Temporizador_Cartas_Tick(object sender, EventArgs e)
        {
            try
            {
                int Resto = Índice_Reparto % 3;
                int Iteración = Índice_Reparto / 3;
                if (Resto == 0)
                {
                    Picture_1.Image = Program.Matriz_Cartas_79[Lista_Montón_Izquierdo[Iteración]];
                    Picture_1.Invalidate();
                    Picture_1.Update();
                }
                else if (Resto == 1)
                {
                    Picture_2.Image = Program.Matriz_Cartas_79[Lista_Montón_Centro[Iteración]];
                    Picture_2.Invalidate();
                    Picture_2.Update();
                }
                else if (Resto == 2)
                {
                    Picture_3.Image = Program.Matriz_Cartas_79[Lista_Montón_Derecho[Iteración]];
                    Picture_3.Invalidate();
                    Picture_3.Update();
                }
                Índice_Reparto++;
                Pintar_Progreso.FillRectangle(Pincel_Progreso, 0, 0, (Índice_Reparto * Ancho_Progreso) / 21, 18);
                Picture_Progreso.Invalidate();
                Picture_Progreso.Update();
                if (Índice_Reparto >= 21) // Reset.
                {
                    Temporizador_Cartas.Stop();
                    Índice_Reparto = 0;
                    SystemSounds.Beep.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Function that starts a new game or continues to the next round of a current game.
        /// </summary>
        /// <param name="Montón">A number between 1 (left) to 3 (right) to select the pile that contains the desired card. Use zero to start a new game.</param>
        internal void Jugar(int Montón)
        {
            try
            {
                Temporizador_Cartas.Stop(); // Always stop the card timer.
                if (Montón < 0) // Quit and reset the game.
                {
                    Ronda = 0;
                    Índice_Reparto = 0;
                    Picture_1.Image = Program.Matriz_Cartas_79[0];
                    Picture_2.Image = Program.Matriz_Cartas_79[0];
                    Picture_3.Image = Program.Matriz_Cartas_79[0];
                    Pintar_Progreso.Clear(Color.Transparent);
                    Picture_1.Invalidate();
                    Picture_2.Invalidate();
                    Picture_3.Invalidate();
                    Picture_Progreso.Invalidate();
                    Picture_1.Update();
                    Picture_2.Update();
                    Picture_3.Update();
                    Picture_Progreso.Update();
                    Menú_Contextual_Mostrar_Ayuda.Enabled = true;
                    Menú_Contextual_Aleatorizar_Cartas.Enabled = true;
                    SystemSounds.Asterisk.Play(); // After another click the game will be restarted.
                }
                else if (Montón < 1 || Montón > 3 || Ronda <= 0) // Start a new game.
                {
                    if (Menú_Contextual_Mostrar_Ayuda.Checked) Menú_Contextual_Mostrar_Ayuda.PerformClick();
                    Menú_Contextual_Mostrar_Ayuda.Enabled = false;
                    Menú_Contextual_Aleatorizar_Cartas.Enabled = false;
                    if (!Variable_Aleatorizar_Cartas) // Use the 21 default cards by Jupisoft.
                    {
                        //List<int> Lista_Temporal = new List<int>(new int[] { 23, 24, 25, 26, 27, 28, 29, 37, 38, 39, 40, 41, 42, 43, 65, 66, 67, 68, 69, 70, 71 }); // Default 21 cards.
                        List<int> Lista_Temporal = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 }); // Default 21 cards.
                        Lista_21_Cartas_Usadas = new List<int>();
                        Lista_Montón_Izquierdo = new List<int>();
                        Lista_Montón_Centro = new List<int>();
                        Lista_Montón_Derecho = new List<int>();
                        for (int Índice = 21; Índice >= 1; Índice--) // Randomize the card order.
                        {
                            int Índice_Aleatorio = Program.Rand.Next(0, Lista_Temporal.Count);
                            Lista_21_Cartas_Usadas.Add(Lista_Temporal[Índice_Aleatorio]);
                            if (Índice % 3 == 0) Lista_Montón_Izquierdo.Add(Lista_Temporal[Índice_Aleatorio]);
                            else if (Índice % 3 == 2) Lista_Montón_Centro.Add(Lista_Temporal[Índice_Aleatorio]);
                            else if (Índice % 3 == 1) Lista_Montón_Derecho.Add(Lista_Temporal[Índice_Aleatorio]);
                            Lista_Temporal.RemoveAt(Índice_Aleatorio);
                            Lista_21_Cartas = Lista_21_Cartas_Usadas.GetRange(0, Lista_21_Cartas_Usadas.Count);
                        }
                        Lista_Temporal = null;
                    }
                    else // Use 21 random cards on each game, so don't expect to pick always the same!
                    {
                        List<int> Lista_Temporal = new List<int>();
                        for (int Índice = 1; Índice <= 78; Índice++) // Add the 78 cards.
                        {
                            Lista_Temporal.Add(Índice);
                        }
                        Lista_21_Cartas_Usadas = new List<int>();
                        Lista_Montón_Izquierdo = new List<int>();
                        Lista_Montón_Centro = new List<int>();
                        Lista_Montón_Derecho = new List<int>();
                        for (int Índice = 21; Índice >= 1; Índice--) // Randomly pick 21 cards.
                        {
                            int Índice_Aleatorio = Program.Rand.Next(0, Lista_Temporal.Count);
                            Lista_21_Cartas_Usadas.Add(Lista_Temporal[Índice_Aleatorio]);
                            if (Índice % 3 == 0) Lista_Montón_Izquierdo.Add(Lista_Temporal[Índice_Aleatorio]);
                            else if (Índice % 3 == 2) Lista_Montón_Centro.Add(Lista_Temporal[Índice_Aleatorio]);
                            else if (Índice % 3 == 1) Lista_Montón_Derecho.Add(Lista_Temporal[Índice_Aleatorio]);
                            Lista_Temporal.RemoveAt(Índice_Aleatorio);
                            Lista_21_Cartas = Lista_21_Cartas_Usadas.GetRange(0, Lista_21_Cartas_Usadas.Count);
                        }
                        Lista_Temporal = null;
                    }
                    Ronda = 1; // Get ready to show the cards in their current order.
                    Índice_Reparto = 0; // Reset all variables and controls.
                    Picture_1.Image = Program.Matriz_Cartas_79[0];
                    Picture_2.Image = Program.Matriz_Cartas_79[0];
                    Picture_3.Image = Program.Matriz_Cartas_79[0];
                    Pintar_Progreso.Clear(Color.Transparent);
                    Picture_1.Invalidate();
                    Picture_2.Invalidate();
                    Picture_3.Invalidate();
                    Picture_Progreso.Invalidate();
                    Picture_1.Update();
                    Picture_2.Update();
                    Picture_3.Update();
                    Picture_Progreso.Update();
                    //Temporizador_Cartas_Tick(Temporizador_Cartas, EventArgs.Empty); // Start at once.
                    Temporizador_Cartas.Start(); // Start a new game.
                }
                else if (Montón == 1) // Left.
                {
                    Lista_21_Cartas.Clear(); // Make the "sandwich" with the 3 piles.
                    Lista_21_Cartas.AddRange(Lista_Montón_Centro.GetRange(0, Lista_Montón_Centro.Count));
                    Lista_21_Cartas.AddRange(Lista_Montón_Izquierdo.GetRange(0, Lista_Montón_Izquierdo.Count));
                    Lista_21_Cartas.AddRange(Lista_Montón_Derecho.GetRange(0, Lista_Montón_Derecho.Count));
                    Lista_Montón_Izquierdo.Clear(); // Rest the 3 piles.
                    Lista_Montón_Centro.Clear();
                    Lista_Montón_Derecho.Clear();
                    for (int Índice = 0; Índice < 21; Índice += 3) // Redo the 3 piles of 7 cards.
                    {
                        Lista_Montón_Izquierdo.Add(Lista_21_Cartas[Índice]);
                        Lista_Montón_Centro.Add(Lista_21_Cartas[Índice + 1]);
                        Lista_Montón_Derecho.Add(Lista_21_Cartas[Índice + 2]);
                    }
                    Ronda++;
                    if (Ronda <= 3)
                    {
                        Índice_Reparto = 0;
                        Picture_1.Image = Program.Matriz_Cartas_79[0];
                        Picture_2.Image = Program.Matriz_Cartas_79[0];
                        Picture_3.Image = Program.Matriz_Cartas_79[0];
                        Pintar_Progreso.Clear(Color.Transparent);
                        Picture_1.Invalidate();
                        Picture_2.Invalidate();
                        Picture_3.Invalidate();
                        Picture_Progreso.Invalidate();
                        Picture_1.Update();
                        Picture_2.Update();
                        Picture_3.Update();
                        Picture_Progreso.Update();
                        //Temporizador_Cartas_Tick(Temporizador_Cartas, EventArgs.Empty); // Start at once.
                        Temporizador_Cartas.Start();
                    }
                }
                else if (Montón == 2) // Center.
                {
                    Lista_21_Cartas.Clear(); // Make the "sandwich" with the 3 piles.
                    Lista_21_Cartas.AddRange(Lista_Montón_Izquierdo.GetRange(0, Lista_Montón_Izquierdo.Count));
                    Lista_21_Cartas.AddRange(Lista_Montón_Centro.GetRange(0, Lista_Montón_Centro.Count));
                    Lista_21_Cartas.AddRange(Lista_Montón_Derecho.GetRange(0, Lista_Montón_Derecho.Count));
                    Lista_Montón_Izquierdo.Clear(); // Rest the 3 piles.
                    Lista_Montón_Centro.Clear();
                    Lista_Montón_Derecho.Clear();
                    for (int Índice = 0; Índice < 21; Índice += 3) // Redo the 3 piles of 7 cards.
                    {
                        Lista_Montón_Izquierdo.Add(Lista_21_Cartas[Índice]);
                        Lista_Montón_Centro.Add(Lista_21_Cartas[Índice + 1]);
                        Lista_Montón_Derecho.Add(Lista_21_Cartas[Índice + 2]);
                    }
                    Ronda++;
                    if (Ronda <= 3)
                    {
                        Índice_Reparto = 0;
                        Picture_1.Image = Program.Matriz_Cartas_79[0];
                        Picture_2.Image = Program.Matriz_Cartas_79[0];
                        Picture_3.Image = Program.Matriz_Cartas_79[0];
                        Pintar_Progreso.Clear(Color.Transparent);
                        Picture_1.Invalidate();
                        Picture_2.Invalidate();
                        Picture_3.Invalidate();
                        Picture_Progreso.Invalidate();
                        Picture_1.Update();
                        Picture_2.Update();
                        Picture_3.Update();
                        Picture_Progreso.Update();
                        //Temporizador_Cartas_Tick(Temporizador_Cartas, EventArgs.Empty); // Start at once.
                        Temporizador_Cartas.Start();
                    }
                }
                else if (Montón == 3) // Right.
                {
                    Lista_21_Cartas.Clear(); // Make the "sandwich" with the 3 piles.
                    Lista_21_Cartas.AddRange(Lista_Montón_Izquierdo.GetRange(0, Lista_Montón_Izquierdo.Count));
                    Lista_21_Cartas.AddRange(Lista_Montón_Derecho.GetRange(0, Lista_Montón_Derecho.Count));
                    Lista_21_Cartas.AddRange(Lista_Montón_Centro.GetRange(0, Lista_Montón_Centro.Count));
                    Lista_Montón_Izquierdo.Clear(); // Rest the 3 piles.
                    Lista_Montón_Centro.Clear();
                    Lista_Montón_Derecho.Clear();
                    for (int Índice = 0; Índice < 21; Índice += 3) // Redo the 3 piles of 7 cards.
                    {
                        Lista_Montón_Izquierdo.Add(Lista_21_Cartas[Índice]);
                        Lista_Montón_Centro.Add(Lista_21_Cartas[Índice + 1]);
                        Lista_Montón_Derecho.Add(Lista_21_Cartas[Índice + 2]);
                    }
                    Ronda++;
                    if (Ronda <= 3)
                    {
                        Índice_Reparto = 0;
                        Picture_1.Image = Program.Matriz_Cartas_79[0];
                        Picture_2.Image = Program.Matriz_Cartas_79[0];
                        Picture_3.Image = Program.Matriz_Cartas_79[0];
                        Pintar_Progreso.Clear(Color.Transparent);
                        Picture_1.Invalidate();
                        Picture_2.Invalidate();
                        Picture_3.Invalidate();
                        Picture_Progreso.Invalidate();
                        Picture_1.Update();
                        Picture_2.Update();
                        Picture_3.Update();
                        Picture_Progreso.Update();
                        //Temporizador_Cartas_Tick(Temporizador_Cartas, EventArgs.Empty); // Start at once.
                        Temporizador_Cartas.Start();
                    }
                }
                if (Ronda > 3) // Game finished after 3 rounds.
                {
                    Ronda = 0;
                    Índice_Reparto = 0;
                    Picture_1.Image = Program.Matriz_Cartas_79[0];
                    Picture_2.Image = Program.Matriz_Cartas_79[Lista_21_Cartas[10]]; // This is the thought card.
                    Picture_3.Image = Program.Matriz_Cartas_79[0];
                    Pintar_Progreso.Clear(Color.Transparent);
                    Picture_1.Invalidate();
                    Picture_2.Invalidate();
                    Picture_3.Invalidate();
                    Picture_Progreso.Invalidate();
                    Picture_1.Update();
                    Picture_2.Update();
                    Picture_3.Update();
                    Picture_Progreso.Update();
                    Menú_Contextual_Mostrar_Ayuda.Enabled = true;
                    Menú_Contextual_Aleatorizar_Cartas.Enabled = true;
                    SystemSounds.Asterisk.Play(); // After another click the game will be restarted.
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
