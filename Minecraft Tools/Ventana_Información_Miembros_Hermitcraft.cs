using Minecraft_Tools.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Información_Miembros_Hermitcraft : Form
    {
        public Ventana_Información_Miembros_Hermitcraft()
        {
            InitializeComponent();
        }

        internal static bool Variable_Dibujar_Pelo = true;
        internal static bool Variable_Dibujar_Chaqueta = true;
        internal static bool Variable_Dibujar_Brazos_Chaqueta = true;
        internal static bool Variable_Dibujar_Pantalones = true;

        internal readonly string Texto_Título = "Hermitcraft Members Information by Jupisoft for " + Program.Texto_Usuario;
        internal bool Variable_Siempre_Visible = false;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;
        internal static Stopwatch Cronómetro_FPS = Stopwatch.StartNew();
        internal long Segundo_FPS_Anterior = 0L;
        internal long FPS_Temporal = 0L;
        internal long FPS_Real = 0L;
        internal DateTime Fecha_Nacimiento = DateTime.MinValue;
        internal bool Aleatorizar_Inicio = false;

        private void Ventana_Hermitcraft_Información_Miembros_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                Numérico_Edad.Minimum = decimal.MinValue;
                Numérico_Edad.Maximum = decimal.MaxValue;
                ComboBox_Edad.SelectedIndex = ComboBox_Edad.Items.Count - 1;
                if (Hermitcraft.Hermits.Matriz_Hermits != null && Hermitcraft.Hermits.Matriz_Hermits.Length > 0)
                {
                    foreach (Hermitcraft.Hermits Hermit in Hermitcraft.Hermits.Matriz_Hermits)
                    {
                        ComboBox_Hermit.Items.Add(Hermit.Nombre);
                    }
                    if (ComboBox_Hermit.Items.Count > 0) ComboBox_Hermit.SelectedIndex = !Aleatorizar_Inicio ? 0 : Program.Rand.Next(0, ComboBox_Hermit.Items.Count);
                    this.Text = Texto_Título + " - [Hermits known: " + Program.Traducir_Número(Hermitcraft.Hermits.Matriz_Hermits.Length) + "]";
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Hermitcraft_Información_Miembros_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Hermitcraft_Información_Miembros_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Hermitcraft_Información_Miembros_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Hermitcraft_Información_Miembros_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Hermit.SelectedIndex > -1) Picture_Fotografía.Image = Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos("Hermitcraft_Picture_" + Hermitcraft.Hermits.Matriz_Hermits[ComboBox_Hermit.SelectedIndex].Lista_Perfiles_Minecraft[0]), Picture_Fotografía.ClientSize.Width, Picture_Fotografía.ClientSize.Height, true, true, CheckState.Unchecked);
                if (Picture_Fotografía.Image == null) Picture_Fotografía.Image = Resources.Cancelar;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Hermitcraft_Información_Miembros_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Alt && !e.Control && !e.Shift)
                {
                    if (e.KeyCode == Keys.Escape) this.Close();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Hermit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Picture_Hermit_256.Image = Resources.Cancelar;
                Picture_Skin.Image = Resources.Cancelar;
                Picture_Fotografía.Image = Resources.Cancelar;
                Picture_Hermit.Image = Resources.Cancelar;
                TextBox_Nombres.Text = null;
                TextBox_Nombre_Real.Text = null;
                TextBox_Fecha_Nacimiento.Text = null;
                TextBox_País.Text = null;
                ComboBox_Perfiles_Minecraft.Items.Clear();
                ComboBox_Youtube.Items.Clear();
                ComboBox_Twitch.Items.Clear();
                ComboBox_Twitter.Items.Clear();
                ComboBox_Discord.Items.Clear();
                ComboBox_Patreon.Items.Clear();
                ComboBox_Mixer.Items.Clear();
                ComboBox_Facebook.Items.Clear();
                ComboBox_Instagram.Items.Clear();
                ComboBox_Sitio_Web.Items.Clear();
                ComboBox_Correo.Items.Clear();
                ComboBox_Reddit.Items.Clear();
                Etiqueta_Otros.Text = "Others:";
                Picture_Otros.Image = null;
                ComboBox_Otros.Items.Clear();
                if (ComboBox_Hermit.SelectedIndex > -1)
                {
                    Hermitcraft.Hermits Hermit = Hermitcraft.Hermits.Matriz_Hermits[ComboBox_Hermit.SelectedIndex];

                    Picture_Hermit_256.Image = Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos("Hermitcraft_" + Hermit.Lista_Perfiles_Minecraft[0]), 256, 256, true, false, CheckState.Checked);
                    Picture_Fotografía.Image = Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Application.StartupPath + "\\Hermitcraft\\" + "Hermitcraft_Picture_" + Hermitcraft.Hermits.Matriz_Hermits[ComboBox_Hermit.SelectedIndex].Lista_Perfiles_Minecraft[0], CheckState.Checked), Picture_Fotografía.ClientSize.Width, Picture_Fotografía.ClientSize.Height, true, true, CheckState.Checked);
                    Picture_Hermit.Image = Program.Obtener_Imagen_Recursos("Hermitcraft_" + Hermit.Lista_Perfiles_Minecraft[0]);

                    TextBox_Nombres.Text = Hermit.Nombres;
                    TextBox_Nombre_Real.Text = Hermit.Nombre_Real;
                    Fecha_Nacimiento = Hermit.Fecha_Nacimiento.Date;
                    TextBox_Fecha_Nacimiento.Text = Fecha_Nacimiento.ToString();
                    TextBox_País.Text = Hermit.País;

                    if (Hermit.Lista_Perfiles_Minecraft != null && Hermit.Lista_Perfiles_Minecraft.Count > 0)
                    {
                        foreach (string Línea in Hermit.Lista_Perfiles_Minecraft)
                        {
                            if (!string.IsNullOrEmpty(Línea)) ComboBox_Perfiles_Minecraft.Items.Add(Línea);
                        }
                        if (ComboBox_Perfiles_Minecraft.Items.Count > 0) ComboBox_Perfiles_Minecraft.SelectedIndex = 0;
                    }
                    if (Hermit.Lista_URL_Youtube != null && Hermit.Lista_URL_Youtube.Count > 0)
                    {
                        foreach (string Línea in Hermit.Lista_URL_Youtube)
                        {
                            if (!string.IsNullOrEmpty(Línea)) ComboBox_Youtube.Items.Add(Línea);
                        }
                        if (ComboBox_Youtube.Items.Count > 0) ComboBox_Youtube.SelectedIndex = 0;
                    }
                    if (Hermit.Lista_URL_Twitch != null && Hermit.Lista_URL_Twitch.Count > 0)
                    {
                        foreach (string Línea in Hermit.Lista_URL_Twitch)
                        {
                            if (!string.IsNullOrEmpty(Línea)) ComboBox_Twitch.Items.Add(Línea);
                        }
                        if (ComboBox_Twitch.Items.Count > 0) ComboBox_Twitch.SelectedIndex = 0;
                    }
                    if (Hermit.Lista_URL_Twitter != null && Hermit.Lista_URL_Twitter.Count > 0)
                    {
                        foreach (string Línea in Hermit.Lista_URL_Twitter)
                        {
                            if (!string.IsNullOrEmpty(Línea)) ComboBox_Twitter.Items.Add(Línea);
                        }
                        if (ComboBox_Twitter.Items.Count > 0) ComboBox_Twitter.SelectedIndex = 0;
                    }
                    if (Hermit.Lista_URL_Discord != null && Hermit.Lista_URL_Discord.Count > 0)
                    {
                        foreach (string Línea in Hermit.Lista_URL_Discord)
                        {
                            if (!string.IsNullOrEmpty(Línea)) ComboBox_Discord.Items.Add(Línea);
                        }
                        if (ComboBox_Discord.Items.Count > 0) ComboBox_Discord.SelectedIndex = 0;
                    }
                    if (Hermit.Lista_URL_Patreon != null && Hermit.Lista_URL_Patreon.Count > 0)
                    {
                        foreach (string Línea in Hermit.Lista_URL_Patreon)
                        {
                            if (!string.IsNullOrEmpty(Línea)) ComboBox_Patreon.Items.Add(Línea);
                        }
                        if (ComboBox_Patreon.Items.Count > 0) ComboBox_Patreon.SelectedIndex = 0;
                    }
                    if (Hermit.Lista_URL_Mixer != null && Hermit.Lista_URL_Mixer.Count > 0)
                    {
                        foreach (string Línea in Hermit.Lista_URL_Mixer)
                        {
                            if (!string.IsNullOrEmpty(Línea)) ComboBox_Mixer.Items.Add(Línea);
                        }
                        if (ComboBox_Mixer.Items.Count > 0) ComboBox_Mixer.SelectedIndex = 0;
                    }
                    if (Hermit.Lista_URL_Sito_Web != null && Hermit.Lista_URL_Sito_Web.Count > 0)
                    {
                        foreach (string Línea in Hermit.Lista_URL_Sito_Web)
                        {
                            if (!string.IsNullOrEmpty(Línea)) ComboBox_Sitio_Web.Items.Add(Línea);
                        }
                        if (ComboBox_Sitio_Web.Items.Count > 0) ComboBox_Sitio_Web.SelectedIndex = 0;
                    }
                    if (Hermit.Lista_URL_Correo != null && Hermit.Lista_URL_Correo.Count > 0)
                    {
                        foreach (string Línea in Hermit.Lista_URL_Correo)
                        {
                            if (!string.IsNullOrEmpty(Línea)) ComboBox_Correo.Items.Add(Línea);
                        }
                        if (ComboBox_Correo.Items.Count > 0) ComboBox_Correo.SelectedIndex = 0;
                    }
                    if (Hermit.Lista_URL_Facebook != null && Hermit.Lista_URL_Facebook.Count > 0)
                    {
                        foreach (string Línea in Hermit.Lista_URL_Facebook)
                        {
                            if (!string.IsNullOrEmpty(Línea)) ComboBox_Facebook.Items.Add(Línea);
                        }
                        if (ComboBox_Facebook.Items.Count > 0) ComboBox_Facebook.SelectedIndex = 0;
                    }
                    if (Hermit.Lista_URL_Instagram != null && Hermit.Lista_URL_Instagram.Count > 0)
                    {
                        foreach (string Línea in Hermit.Lista_URL_Instagram)
                        {
                            if (!string.IsNullOrEmpty(Línea)) ComboBox_Instagram.Items.Add(Línea);
                        }
                        if (ComboBox_Instagram.Items.Count > 0) ComboBox_Instagram.SelectedIndex = 0;
                    }
                    if (Hermit.Lista_URL_Reddit != null && Hermit.Lista_URL_Reddit.Count > 0)
                    {
                        foreach (string Línea in Hermit.Lista_URL_Reddit)
                        {
                            if (!string.IsNullOrEmpty(Línea)) ComboBox_Reddit.Items.Add(Línea);
                        }
                        if (ComboBox_Reddit.Items.Count > 0) ComboBox_Reddit.SelectedIndex = 0;
                    }
                    if (Hermit.Lista_URL_Otros != null && Hermit.Lista_URL_Otros.Count > 0)
                    {
                        foreach (string Línea in Hermit.Lista_URL_Otros)
                        {
                            if (!string.IsNullOrEmpty(Línea)) ComboBox_Otros.Items.Add(Línea);
                        }
                        if (ComboBox_Otros.Items.Count > 0) ComboBox_Otros.SelectedIndex = 0;
                    }
                    TextBox_Biografía.Text = Hermit.Biografía;
                }
                else Fecha_Nacimiento = DateTime.MinValue;
                if (Picture_Hermit_256.Image == null) Picture_Hermit_256.Image = Resources.Cancelar;
                if (Picture_Fotografía.Image == null) Picture_Fotografía.Image = Resources.Cancelar;
                if (Picture_Hermit.Image == null) Picture_Hermit.Image = Resources.Cancelar;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Perfiles_Minecraft_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Perfiles_Minecraft.SelectedIndex > -1)
                {
                    Picture_Hermit_256.Image = Program.Obtener_Imagen_Skin_2D(Program.Obtener_Imagen_Recursos("Hermitcraft_Skin_" + Hermitcraft.Hermits.Matriz_Hermits[ComboBox_Hermit.SelectedIndex].Lista_Perfiles_Minecraft[ComboBox_Perfiles_Minecraft.SelectedIndex]), Variable_Dibujar_Pelo, Variable_Dibujar_Chaqueta, Variable_Dibujar_Brazos_Chaqueta, Variable_Dibujar_Pantalones);
                    Picture_Skin.Image = Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos("Hermitcraft_Skin_" + Hermitcraft.Hermits.Matriz_Hermits[ComboBox_Hermit.SelectedIndex].Lista_Perfiles_Minecraft[ComboBox_Perfiles_Minecraft.SelectedIndex]), 256, 256, true, false, CheckState.Checked);
                }
                else
                {
                    Picture_Hermit_256.Image = Resources.Cancelar;
                    Picture_Skin.Image = Resources.Cancelar;
                }
                if (Picture_Hermit_256.Image == null) Picture_Hermit_256.Image = Resources.Cancelar;
                if (Picture_Skin.Image == null) Picture_Skin.Image = Resources.Cancelar;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Otros_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Otros.SelectedIndex > -1 && !string.IsNullOrEmpty(ComboBox_Otros.Text))
                {
                    string URL = ComboBox_Otros.Text.ToLowerInvariant();
                    if (ComboBox_Otros.Text.Contains("beam"))
                    {
                        Etiqueta_Otros.Text = "Beam pro:";
                        Picture_Otros.Image = Resources.Beam_Pro;
                    }
                    else if (ComboBox_Otros.Text.Contains("deviantart"))
                    {
                        Etiqueta_Otros.Text = "Deviant art:";
                        Picture_Otros.Image = Resources.Deviant_Art;
                    }
                    else if(ComboBox_Otros.Text.Contains("plus.google"))
                    {
                        Etiqueta_Otros.Text = "Google+:";
                        Picture_Otros.Image = Resources.Google_Plus;
                    }
                    else if(ComboBox_Otros.Text.Contains("spreadshirt"))
                    {
                        Etiqueta_Otros.Text = "Spread shirt:";
                        Picture_Otros.Image = Resources.Spread_Shirt;
                    }
                    else if(ComboBox_Otros.Text.Contains("steamcommunity"))
                    {
                        Etiqueta_Otros.Text = "Steam:";
                        Picture_Otros.Image = Resources.Steam;
                    }
                    else if(ComboBox_Otros.Text.Contains("tumblr"))
                    {
                        Etiqueta_Otros.Text = "Tumblr:";
                        Picture_Otros.Image = Resources.Tumblr;
                    }
                    else
                    {
                        Etiqueta_Otros.Text = "Others:";
                        Picture_Otros.Image = null;
                    }
                    URL = null;
                }
                else
                {
                    Etiqueta_Otros.Text = "Others:";
                    Picture_Otros.Image = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBoxes_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    ComboBox Combo = sender as ComboBox;
                    if (Combo != null)
                    {
                        if (!string.IsNullOrEmpty(Combo.Text))
                        {
                            Clipboard.SetText(Combo.Text);
                            SystemSounds.Asterisk.Play();
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Hermit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ComboBox_Perfiles_Minecraft.Text))
                {
                    SystemSounds.Beep.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Perfiles_Minecraft_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ComboBox_Perfiles_Minecraft.Text))
                {
                    SystemSounds.Beep.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Youtube_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ComboBox_Youtube.Text))
                {
                    Program.Ejecutar_Ruta(ComboBox_Youtube.Text, ProcessWindowStyle.Normal);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Twitch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ComboBox_Twitch.Text))
                {
                    Program.Ejecutar_Ruta(ComboBox_Twitch.Text, ProcessWindowStyle.Normal);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Twitter_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ComboBox_Twitter.Text))
                {
                    Program.Ejecutar_Ruta(ComboBox_Twitter.Text, ProcessWindowStyle.Normal);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Discord_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ComboBox_Discord.Text))
                {
                    Program.Ejecutar_Ruta(ComboBox_Discord.Text, ProcessWindowStyle.Normal);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Patreon_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ComboBox_Patreon.Text))
                {
                    Program.Ejecutar_Ruta(ComboBox_Patreon.Text, ProcessWindowStyle.Normal);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Mixer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ComboBox_Mixer.Text))
                {
                    Program.Ejecutar_Ruta(ComboBox_Mixer.Text, ProcessWindowStyle.Normal);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Facebook_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ComboBox_Facebook.Text))
                {
                    Program.Ejecutar_Ruta(ComboBox_Facebook.Text, ProcessWindowStyle.Normal);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Instagram_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ComboBox_Instagram.Text))
                {
                    Program.Ejecutar_Ruta(ComboBox_Instagram.Text, ProcessWindowStyle.Normal);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Sitio_Web_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ComboBox_Sitio_Web.Text))
                {
                    Program.Ejecutar_Ruta(ComboBox_Sitio_Web.Text, ProcessWindowStyle.Normal);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Correo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ComboBox_Correo.Text))
                {
                    Program.Ejecutar_Ruta("mailto:" + ComboBox_Correo.Text, ProcessWindowStyle.Normal);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Reddit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ComboBox_Reddit.Text))
                {
                    Program.Ejecutar_Ruta(ComboBox_Reddit.Text, ProcessWindowStyle.Normal);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Otros_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ComboBox_Otros.Text))
                {
                    Program.Ejecutar_Ruta(ComboBox_Otros.Text, ProcessWindowStyle.Normal);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.Hermitcraft_members_information;
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
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Información_Miembros_Hermitcraft);
                Program.Ejecutar_Ruta(Program.Ruta_Guardado_Imágenes_Información_Miembros_Hermitcraft, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Dibujar_Pelo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Dibujar_Pelo = Menú_Contextual_Dibujar_Pelo.Checked;
                ComboBox_Perfiles_Minecraft_SelectedIndexChanged(ComboBox_Perfiles_Minecraft, EventArgs.Empty);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Dibujar_Chaqueta_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Dibujar_Chaqueta = Menú_Contextual_Dibujar_Chaqueta.Checked;
                ComboBox_Perfiles_Minecraft_SelectedIndexChanged(ComboBox_Perfiles_Minecraft, EventArgs.Empty);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Dibujar_Brazos_Chaqueta_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Dibujar_Brazos_Chaqueta = Menú_Contextual_Dibujar_Brazos_Chaqueta.Checked;
                ComboBox_Perfiles_Minecraft_SelectedIndexChanged(ComboBox_Perfiles_Minecraft, EventArgs.Empty);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Dibujar_Pantalones_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Dibujar_Pantalones = Menú_Contextual_Dibujar_Pantalones.Checked;
                ComboBox_Perfiles_Minecraft_SelectedIndexChanged(ComboBox_Perfiles_Minecraft, EventArgs.Empty);
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
                try
                {
                    TimeSpan Intervalo = DateTime.Now - Fecha_Nacimiento;
                    decimal Valor = (decimal)Intervalo.TotalMilliseconds;
                    if (ComboBox_Edad.SelectedIndex > 0)
                    {
                        if (ComboBox_Edad.SelectedIndex == 1) Valor /= 1000m; // Seconds
                        else if (ComboBox_Edad.SelectedIndex == 2) Valor /= 1000m * 60m; // Minutes
                        else if (ComboBox_Edad.SelectedIndex == 3) Valor /= 1000m * 60m * 60m; // Hours
                        else if (ComboBox_Edad.SelectedIndex == 4) Valor /= 1000m * 60m * 60m * 24m; // Days
                        else if (ComboBox_Edad.SelectedIndex == 5) Valor /= 1000m * 60m * 60m * 24m * 7m; // Weeks
                        else if (ComboBox_Edad.SelectedIndex == 6) Valor /= 1000m * 60m * 60m * 24m * (365.25m / 12m); // Months
                        else if (ComboBox_Edad.SelectedIndex == 7) Valor /= 1000m * 60m * 60m * 24m * 365.25m; // Years
                    }
                    Numérico_Edad.Value = Valor;
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
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

        private void Picture_Skin_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    if (ComboBox_Hermit.SelectedIndex > -1 && ComboBox_Perfiles_Minecraft.SelectedIndex > -1)
                    {
                        Bitmap Imagen = Program.Obtener_Imagen_Recursos("Hermitcraft_Skin_" + Hermitcraft.Hermits.Matriz_Hermits[ComboBox_Hermit.SelectedIndex].Lista_Perfiles_Minecraft[ComboBox_Perfiles_Minecraft.SelectedIndex]);
                        if (Imagen != null)
                        {
                            Clipboard.SetImage(Imagen);
                            SystemSounds.Asterisk.Play();
                            Imagen.Dispose();
                            Imagen = null;
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_Hermit_256_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    if (ComboBox_Hermit.SelectedIndex > -1 && ComboBox_Perfiles_Minecraft.SelectedIndex > -1)
                    {
                        Bitmap Imagen = Program.Obtener_Imagen_Skin_2D(Program.Obtener_Imagen_Recursos("Hermitcraft_Skin_" + Hermitcraft.Hermits.Matriz_Hermits[ComboBox_Hermit.SelectedIndex].Lista_Perfiles_Minecraft[ComboBox_Perfiles_Minecraft.SelectedIndex]), Variable_Dibujar_Pelo, Variable_Dibujar_Chaqueta, Variable_Dibujar_Brazos_Chaqueta, Variable_Dibujar_Pantalones);
                        if (Imagen != null)
                        {
                            Clipboard.SetImage(Imagen);
                            SystemSounds.Asterisk.Play();
                            Imagen.Dispose();
                            Imagen = null;
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_Fotografía_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    if (ComboBox_Hermit.SelectedIndex > -1 && ComboBox_Perfiles_Minecraft.Items.Count > 0)
                    {
                        Bitmap Imagen = Program.Obtener_Imagen_Recursos("Hermitcraft_Picture_" + Hermitcraft.Hermits.Matriz_Hermits[ComboBox_Hermit.SelectedIndex].Lista_Perfiles_Minecraft[0]);
                        if (Imagen != null)
                        {
                            Clipboard.SetImage(Imagen);
                            SystemSounds.Asterisk.Play();
                            Imagen.Dispose();
                            Imagen = null;
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TextBox_Biografía.Text))
                {
                    Clipboard.SetText(TextBox_Biografía.Text);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Skin_Click(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Hermit.SelectedIndex > -1 && ComboBox_Perfiles_Minecraft.Items.Count > 0)
                {
                    Bitmap Imagen = Program.Obtener_Imagen_Recursos("Hermitcraft_Skin_" + Hermitcraft.Hermits.Matriz_Hermits[ComboBox_Hermit.SelectedIndex].Lista_Perfiles_Minecraft[ComboBox_Perfiles_Minecraft.SelectedIndex]);
                    if (Imagen != null)
                    {
                        Clipboard.SetImage(Imagen);
                        SystemSounds.Asterisk.Play();
                        Imagen.Dispose();
                        Imagen = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Skin_Frontal_Click(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Hermit.SelectedIndex > -1 && ComboBox_Perfiles_Minecraft.SelectedIndex > -1)
                {
                    Bitmap Imagen = Program.Obtener_Imagen_Skin_2D(Program.Obtener_Imagen_Recursos("Hermitcraft_Skin_" + Hermitcraft.Hermits.Matriz_Hermits[ComboBox_Hermit.SelectedIndex].Lista_Perfiles_Minecraft[ComboBox_Perfiles_Minecraft.SelectedIndex]), Variable_Dibujar_Pelo, Variable_Dibujar_Chaqueta, Variable_Dibujar_Brazos_Chaqueta, Variable_Dibujar_Pantalones);
                    if (Imagen != null)
                    {
                        Clipboard.SetImage(Imagen);
                        SystemSounds.Asterisk.Play();
                        Imagen.Dispose();
                        Imagen = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Fotografía_Click(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Hermit.SelectedIndex > -1 && ComboBox_Perfiles_Minecraft.Items.Count > 0)
                {
                    Bitmap Imagen = Program.Obtener_Imagen_Recursos("Hermitcraft_Picture_" + Hermitcraft.Hermits.Matriz_Hermits[ComboBox_Hermit.SelectedIndex].Lista_Perfiles_Minecraft[0]);
                    if (Imagen != null)
                    {
                        Clipboard.SetImage(Imagen);
                        SystemSounds.Asterisk.Play();
                        Imagen.Dispose();
                        Imagen = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TextBox_Biografía.Text))
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Información_Miembros_Hermitcraft);
                    File.WriteAllText(Program.Ruta_Guardado_Imágenes_Información_Miembros_Hermitcraft + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " Biography.txt", TextBox_Biografía.Text, Encoding.Unicode);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Skin_Click(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Hermit.SelectedIndex > -1 && ComboBox_Perfiles_Minecraft.Items.Count > 0)
                {
                    Bitmap Imagen = Program.Obtener_Imagen_Recursos("Hermitcraft_Skin_" + Hermitcraft.Hermits.Matriz_Hermits[ComboBox_Hermit.SelectedIndex].Lista_Perfiles_Minecraft[ComboBox_Perfiles_Minecraft.SelectedIndex]);
                    if (Imagen != null)
                    {
                        Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Información_Miembros_Hermitcraft);
                        Imagen.Save(Program.Ruta_Guardado_Imágenes_Información_Miembros_Hermitcraft + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " Skin.png", ImageFormat.Png);
                        SystemSounds.Asterisk.Play();
                        Imagen.Dispose();
                        Imagen = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Skin_Frontal_Click(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Hermit.SelectedIndex > -1 && ComboBox_Perfiles_Minecraft.SelectedIndex > -1)
                {
                    Bitmap Imagen = Program.Obtener_Imagen_Skin_2D(Program.Obtener_Imagen_Recursos("Hermitcraft_Skin_" + Hermitcraft.Hermits.Matriz_Hermits[ComboBox_Hermit.SelectedIndex].Lista_Perfiles_Minecraft[ComboBox_Perfiles_Minecraft.SelectedIndex]), Variable_Dibujar_Pelo, Variable_Dibujar_Chaqueta, Variable_Dibujar_Brazos_Chaqueta, Variable_Dibujar_Pantalones);
                    if (Imagen != null)
                    {
                        Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Información_Miembros_Hermitcraft);
                        Imagen.Save(Program.Ruta_Guardado_Imágenes_Información_Miembros_Hermitcraft + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " Front skin.png", ImageFormat.Png);
                        SystemSounds.Asterisk.Play();
                        Imagen.Dispose();
                        Imagen = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Fotografía_Click(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Hermit.SelectedIndex > -1 && ComboBox_Perfiles_Minecraft.Items.Count > 0)
                {
                    Bitmap Imagen = Program.Obtener_Imagen_Recursos("Hermitcraft_Picture_" + Hermitcraft.Hermits.Matriz_Hermits[ComboBox_Hermit.SelectedIndex].Lista_Perfiles_Minecraft[0]);
                    if (Imagen != null)
                    {
                        Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Información_Miembros_Hermitcraft);
                        Imagen.Save(Program.Ruta_Guardado_Imágenes_Información_Miembros_Hermitcraft + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " Picture.png", ImageFormat.Png);
                        SystemSounds.Asterisk.Play();
                        Imagen.Dispose();
                        Imagen = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Información_Miembros_Hermitcraft_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    this.Close(); // Since it can be accidentally started from the main window, allow for a fast closing without using the keyboard.
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
