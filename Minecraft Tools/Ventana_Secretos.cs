using Minecraft_Tools.Properties;
using SevenZip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Secretos : Form
    {
        public Ventana_Secretos()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Secrets by Jupisoft for " + Program.Texto_Usuario;
        internal bool Variable_Siempre_Visible = false;
        internal bool Variable_Excepción = false;
        //internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        //internal bool Variable_Memoria = false;
        //internal static Stopwatch Cronómetro_FPS = Stopwatch.StartNew();
        //internal long Segundo_FPS_Anterior = 0L;
        //internal long FPS_Temporal = 0L;
        //internal long FPS_Real = 0L;

        private void Ventana_Secretos_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título + " - [Follow the instructions below if you want to enable all the secret files]";
                Botón_Jupisoft_Cuadros_HD_1_12.Image = Program.Obtener_Imagen_Miniatura(Resources.Paintings, 16, 16, true, true, CheckState.Checked);
                Botón_Jupisoft_Cuadros_HD_1_13.Image = Program.Obtener_Imagen_Miniatura(Resources.Paintings, 16, 16, true, true, CheckState.Checked);
                Botón_Minecraft_Versión_April_Fools_Red.Image = Program.Obtener_Imagen_Color(Color.FromArgb(255, 0, 0));
                Botón_Minecraft_Versión_April_Fools_Purple.Image = Program.Obtener_Imagen_Color(Color.FromArgb(160, 0, 255));
                //this.WindowState = FormWindowState.Maximized;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Secretos_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                //Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Secretos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Secretos_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Secretos_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Secretos_KeyDown(object sender, KeyEventArgs e)
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

        private void TextBox_Nombre_Usuario_KeyDown(object sender, KeyEventArgs e)
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
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Nombre_Usuario_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Botón_Mostrar_Secretos.Enabled = CheckBox_Nombre_Usuario.Checked && !string.IsNullOrEmpty(TextBox_Nombre_Usuario.Text) && string.Compare(TextBox_Nombre_Usuario.Text, Program.Texto_Usuario, true) == 0;
                if (CheckBox_Nombre_Usuario.Enabled) Botón_Mostrar_Secretos.Image = Botón_Mostrar_Secretos.Enabled ? Resources.Ojo : Resources.Ojo_Ciego;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_Nombre_Usuario_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Botón_Mostrar_Secretos.Enabled = CheckBox_Nombre_Usuario.Checked && !string.IsNullOrEmpty(TextBox_Nombre_Usuario.Text) && string.Compare(TextBox_Nombre_Usuario.Text, Program.Texto_Usuario, true) == 0;
                if (TextBox_Nombre_Usuario.Enabled) Botón_Mostrar_Secretos.Image = Botón_Mostrar_Secretos.Enabled ? Resources.Ojo : Resources.Ojo_Ciego;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Nombre_Usuario_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    CheckBox_Nombre_Usuario.Checked = true;
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_Nombre_Usuario_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    TextBox_Nombre_Usuario.Text = Program.Texto_Usuario;
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Mostrar_Secretos_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    Botón_Mostrar_Secretos.PerformClick();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Mostrar_Secretos_Click(object sender, EventArgs e)
        {
            try
            {
                CheckBox_Nombre_Usuario.Enabled = false;
                TextBox_Nombre_Usuario.Enabled = false;
                Botón_Mostrar_Secretos.Enabled = false;
                CheckBox_Nombre_Usuario.Visible = false;
                Etiqueta_Advertencia.Visible = true;

                // Enable the secret files buttons:

                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\B12FE96A")) Botón_Packs_Skins_1st_Birthday_Skin_Pack.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\6CFC63B7")) Botón_Packs_Skins_2nd_Birthday_Skin_Pack.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\437C4660")) Botón_Packs_Skins_3rd_Birthday_Skin_Pack.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\8003522")) Botón_Packs_Skins_4th_Birthday_Skin_Pack.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\3CBE35FB")) Botón_Packs_Skins_Adventure_Time.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\E0F86053")) Botón_Packs_Skins_Battle_Beasts.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\35EC2BE3")) Botón_Packs_Skins_Battle_Beasts_2.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\2BB7998E")) Botón_Packs_Skins_Biome_Settlers_Pack_1.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\A4AEE16C")) Botón_Packs_Skins_Campfire_Tales_Skin_Pack.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\BC278B06")) Botón_Packs_Skins_Chinese_Mythology.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\FE7F1F10")) Botón_Packs_Skins_Doctor_Who_Skins_Volume_I.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\65B5D112")) Botón_Packs_Skins_Doctor_Who_Skins_Volume_II.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\A7C1F72C")) Botón_Packs_Skins_Fallout.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\DED3A288")) Botón_Packs_Skins_Festive.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\30FF6565")) Botón_Packs_Skins_Festive_Skin_Pack.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\67ACC6FF")) Botón_Packs_Skins_Greek_Mythology.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\B198CD49")) Botón_Packs_Skins_Halloween_2015.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\FCBDB984")) Botón_Packs_Skins_Halloween_Charity_Skin_Pack.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\2F12D85B")) Botón_Packs_Skins_Halo.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\622D9DAB")) Botón_Packs_Skins_Magic_The_Gathering_Skin_Pack.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\688711E4")) Botón_Packs_Skins_Marvel_Avengers.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\89F83E2D")) Botón_Packs_Skins_Marvel_Guardians_of_the_Galaxy.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\FC5E8B6E")) Botón_Packs_Skins_Marvel_Spider_Man.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\BEFB1F9F")) Botón_Packs_Skins_Mass_Effect.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\A53ECB15")) Botón_Packs_Skins_Minecon_2015_Skin_Pack.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\C2189632")) Botón_Packs_Skins_Mini_Game_Masters_Skin_Pack.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\AFB257C2")) Botón_Packs_Skins_Power_Rangers_Skin_Pack.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\3B9CAE45")) Botón_Packs_Skins_Redstone_Specialists_Skin_Pack.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\C7D5C98A")) Botón_Packs_Skins_Skin_Pack_1.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\9FF4FCEE")) Botón_Packs_Skins_Skin_Pack_2.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\8C2D7039")) Botón_Packs_Skins_Skin_Pack_3.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\14D1BD29")) Botón_Packs_Skins_Skin_Pack_4.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\1B0C2589")) Botón_Packs_Skins_Skin_Pack_5.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\DE93409")) Botón_Packs_Skins_Skin_Pack_6.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\EFA4B512")) Botón_Packs_Skins_Skyrim.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\18F1442D")) Botón_Packs_Skins_Star_Wars_Classic_Skin_Pack.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\7025C634")) Botón_Packs_Skins_Star_Wars_Prequel_Skin_Pack.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\5341A607")) Botón_Packs_Skins_Star_Wars_Rebels_Skin_Pack.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\5987E9B2")) Botón_Packs_Skins_Story_Mode_Skin_Pack.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\9871C4F3")) Botón_Packs_Skins_Summer_of_Arcade_Skin_Pack.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\F2B4919C")) Botón_Packs_Skins_The_Simpsons.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\E7C858B1")) Botón_Packs_Skins_Villains_Skin_Pack.Enabled = true;

                if (File.Exists(Application.StartupPath + "\\Secrets\\Resources\\269BB7A4")) Botón_Packs_Recursos_Adventure_Time.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Resources\\89483C6D")) Botón_Packs_Recursos_Candy.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Resources\\CF7FB669")) Botón_Packs_Recursos_Cartoon.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Resources\\C57D709B")) Botón_Packs_Recursos_Chinese_Mythology.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Resources\\34C2F8A8")) Botón_Packs_Recursos_City.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Resources\\F512D9CB")) Botón_Packs_Recursos_Fallout.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Resources\\42E12AC6")) Botón_Packs_Recursos_Fantasy.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Resources\\E86DD8D1")) Botón_Packs_Recursos_Festive.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Resources\\D5F7FC0B")) Botón_Packs_Recursos_Greek_Mythology.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Resources\\E9D6C865")) Botón_Packs_Recursos_Halloween.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Resources\\FF4896C0")) Botón_Packs_Recursos_Halloween_2015.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Resources\\EB1255D9")) Botón_Packs_Recursos_Halo.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Resources\\72D44EAC")) Botón_Packs_Recursos_Mass_Effect.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Resources\\118FA1D5")) Botón_Packs_Recursos_Natural.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Resources\\75955242")) Botón_Packs_Recursos_Pattern.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Resources\\47D86650")) Botón_Packs_Recursos_Plastic.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Resources\\680A9114")) Botón_Packs_Recursos_Skyrim.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Resources\\CBA3979D")) Botón_Packs_Recursos_Steampunk.Enabled = true;

                //Botón_Packs_Recursos_Faithful.Enabled = true;
                //Botón_Packs_Recursos_X_Ray.Enabled = true;

                if (File.Exists(Application.StartupPath + "\\Secrets\\Clients\\C560F3F7")) Botón_Minecraft_Cliente_1_0_0.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Clients\\85DE36A1")) Botón_Minecraft_Cliente_1_1_0.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Clients\\E09534B2")) Botón_Minecraft_Cliente_1_2_5.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Clients\\FC727F44")) Botón_Minecraft_Cliente_1_3_2.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Clients\\555EDD3B")) Botón_Minecraft_Cliente_1_4_7.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Clients\\25642598")) Botón_Minecraft_Cliente_1_5_2.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Clients\\46477526")) Botón_Minecraft_Cliente_1_6_4.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Clients\\6BBD3EA2")) Botón_Minecraft_Cliente_1_7_10.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Clients\\CCD6E5FA")) Botón_Minecraft_Cliente_1_8_8.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Clients\\65EAB3B8")) Botón_Minecraft_Cliente_1_9_4.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Clients\\76949A07")) Botón_Minecraft_Cliente_1_10.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Clients\\C3242F08")) Botón_Minecraft_Cliente_1_11_2.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Clients\\FF9D499D")) Botón_Minecraft_Cliente_1_12.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Clients\\9DB33EED")) Botón_Minecraft_Cliente_1_13_2.Enabled = true;
                //if (File.Exists(Application.StartupPath + "\\Secrets\\Clients\\")) Botón_Minecraft_Cliente_1_14.Enabled = true;
                //if (File.Exists(Application.StartupPath + "\\Secrets\\Clients\\")) Botón_Minecraft_Cliente_1_15.Enabled = true;
                //if (File.Exists(Application.StartupPath + "\\Secrets\\Clients\\")) Botón_Minecraft_Cliente_1_16.Enabled = true;

                //if (File.Exists()) Botón_Minecraft_Servidor_1_0_0.Enabled = true;
                //if (File.Exists()) Botón_Minecraft_Servidor_1_1_0.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Servers\\DA957474")) Botón_Minecraft_Servidor_1_2_5.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Servers\\E3B2CBC9")) Botón_Minecraft_Servidor_1_3_2.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Servers\\97090A20")) Botón_Minecraft_Servidor_1_4_7.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Servers\\72B6E195")) Botón_Minecraft_Servidor_1_5_2.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Servers\\A7C604EC")) Botón_Minecraft_Servidor_1_6_4.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Servers\\734E9050")) Botón_Minecraft_Servidor_1_7_10.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Servers\\12AD1E23")) Botón_Minecraft_Servidor_1_8_8.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Servers\\32A8A5E0")) Botón_Minecraft_Servidor_1_9_4.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Servers\\E91FE11B")) Botón_Minecraft_Servidor_1_10.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Servers\\D6E7D9B7")) Botón_Minecraft_Servidor_1_11_2.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Servers\\AE3062D5")) Botón_Minecraft_Servidor_1_12.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Servers\\ED9779A8")) Botón_Minecraft_Servidor_1_13_2.Enabled = true;
                //if (File.Exists(Application.StartupPath + "\\Secrets\\Servers\\")) Botón_Minecraft_Servidor_1_14.Enabled = true;
                //if (File.Exists(Application.StartupPath + "\\Secrets\\Servers\\")) Botón_Minecraft_Servidor_1_15.Enabled = true;
                //if (File.Exists(Application.StartupPath + "\\Secrets\\Servers\\")) Botón_Minecraft_Servidor_1_16.Enabled = true;

                if (File.Exists(Application.StartupPath + "\\Secrets\\Sources\\EC22248B")) Botón_Minecraft_1_0_0.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Sources\\810486E4")) Botón_Minecraft_1_1_0.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Sources\\98F3C532")) Botón_Minecraft_1_2_5.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Sources\\ED9DE6E4")) Botón_Minecraft_1_3_2.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Sources\\9B053B77")) Botón_Minecraft_1_4_7.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Sources\\62B6F330")) Botón_Minecraft_1_5_2.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Sources\\7B7F95E6")) Botón_Minecraft_1_6_4.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Sources\\54BD5A59")) Botón_Minecraft_1_7_10.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Sources\\72B08AC6")) Botón_Minecraft_1_8_8.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Sources\\7CCDACF0")) Botón_Minecraft_1_9_4.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Sources\\D058CD75")) Botón_Minecraft_1_10.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Sources\\526FA4DF")) Botón_Minecraft_1_11_2.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Sources\\7C6FC8D9")) Botón_Minecraft_1_12.Enabled = true;
                //if (File.Exists()) Botón_Minecraft_1_13_1.Enabled = true;
                //if (File.Exists()) Botón_Minecraft_1_14.Enabled = true;
                //if (File.Exists()) Botón_Minecraft_1_15.Enabled = true;
                //if (File.Exists()) Botón_Minecraft_1_16.Enabled = true;

                if (File.Exists(Application.StartupPath + "\\Secrets\\Optifine\\63771667")) Botón_Optifine_1_7_10.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Optifine\\D1EA855C")) Botón_Optifine_1_8_0.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Optifine\\476B3C0")) Botón_Optifine_1_8_8.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Optifine\\A6A8F3A5")) Botón_Optifine_1_8_9.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Optifine\\2829A67F")) Botón_Optifine_1_9_0.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Optifine\\1F69E697")) Botón_Optifine_1_9_2.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Optifine\\963E2CB0")) Botón_Optifine_1_9_4.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Optifine\\90778F0A")) Botón_Optifine_1_10.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Optifine\\B7671E79")) Botón_Optifine_1_10_2.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Optifine\\E90364C0")) Botón_Optifine_1_11.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Optifine\\6144A41B")) Botón_Optifine_1_11_2.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Optifine\\A98DC412")) Botón_Optifine_1_12.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Optifine\\BC7EC82C")) Botón_Optifine_1_12_1.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Optifine\\314977B8")) Botón_Optifine_1_12_2.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Optifine\\3CA92E19")) Botón_Optifine_1_13.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Optifine\\547DF024")) Botón_Optifine_1_13_1.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Optifine\\E41A62F2")) Botón_Optifine_1_13_2.Enabled = true;

                if (File.Exists(Application.StartupPath + "\\Secrets\\Versions\\9227EF64")) Botón_Minecraft_Versión_Indev_InfDev.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Versions\\B65219A5")) Botón_Minecraft_Versión_April_Fools_Red.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Versions\\E7ABB0B0")) Botón_Minecraft_Versión_April_Fools_Purple.Enabled = true;

                if (File.Exists(Application.StartupPath + "\\Secrets\\Skins\\A24938DB")) Botón_Extras_26601_Skins.Enabled = true;
                if (File.Exists(Application.StartupPath + "\\Secrets\\Others\\DD08ECF8")) Botón_Extras_Fuente_Alfabeto_Galáctico.Enabled = true;
                Botón_Extras_URL_26601_Skins.Enabled = true;
                //Botón_Extras_Eclipse.Enabled = true;
                //Botón_Extras_Java_SDK.Enabled = true;
                //Botón_Extras_MCP.Enabled = true;
                //Botón_Extras_Xbox_360_Tutoriales.Enabled = true;

                Botón_Packs_Skins_1st_Birthday_Skin_Pack.Select();
                Botón_Packs_Skins_1st_Birthday_Skin_Pack.Focus();

                //SystemSounds.Asterisk.Play();
                if (MessageBox.Show(this, "Warning: the secret files are not included by default now.\r\nClick \"Yes\" to download them from mediafire as a zip file.\r\n\r\nOnce downloaded please extract the subfolders of the zip file near the application, in a folder called \"Secrets\", this one should contain the rest of subfolders inside it.\r\n\r\nIf done well you'll be able to extract the secret files from this window, and if it tells you it couldn't find a file, then they will be extracted on a wrong place or maybe inside 2 \"Secrets\" folders, which WinRar sometimes does. Please correct that and try again if that's the case.\r\n\r\nDo you want to download all the secret files at once from mediafire?\r\n(Please select \"No\" if you have already downloaded the secret files).", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Program.Ejecutar_Ruta("http://www.mediafire.com/file/081rafg4wo6git7/Secrets.zip/file", ProcessWindowStyle.Normal);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Function designed to decrypt and export any existing file.
        /// WARNING: using any part of this code to decrypt any of the "secret" files
        /// on your own will be like if you gave your consent to all of the
        /// requirements needed in order to be able to export the files
        /// using regularly the application, so again... YOU'VE BEEN WARNED!!!
        /// </summary>
        /// <param name="Ruta_Secreto">Any valid and existing file path.</param>
        internal void Desencriptar_Exportar_Archivo_Secreto(string Ruta_Secreto, string Nombre_Salida, string Extensión_Salida)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(Ruta_Secreto) && File.Exists(Ruta_Secreto))
                {
                    List<string> Lista_Rutas = new List<string>(new string[] { Ruta_Secreto });
                    for (int Índice = 1; Índice <= 256; Índice++)
                    {
                        string Ruta = Ruta_Secreto + new string('_', Índice);
                        if (File.Exists(Ruta)) Lista_Rutas.Add(Ruta);
                        //else break;
                    }
                    if (string.IsNullOrEmpty(Nombre_Salida)) Nombre_Salida = Program.Obtener_Nombre_Temporal(); // Defaults to zip file.
                    if (string.IsNullOrEmpty(Extensión_Salida)) Extensión_Salida = ".zip"; // Defaults to zip file.
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Secretos);
                    string Ruta_Extensión = Program.Ruta_Guardado_Imágenes_Secretos + "\\" + Nombre_Salida;
                    while (File.Exists(Ruta_Extensión + Extensión_Salida)) Ruta_Extensión += "_"; // Be sure the file doesn't exist.
                    Ruta_Extensión += Extensión_Salida;
                    FileStream Lector_Salida = new FileStream(Ruta_Extensión, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector_Salida.SetLength(0L); // Be sure the file is empty.
                    Lector_Salida.Seek(0L, SeekOrigin.Begin);
                    byte[] Matriz_Bytes = new byte[4096];
                    foreach (string Ruta in Lista_Rutas)
                    {
                        Program.Quitar_Atributo_Sólo_Lectura(Ruta);
                        FileStream Lector = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        if (Lector != null)
                        {
                            if (Lector.Length > 0L)
                            {
                                Lector.Seek(0L, SeekOrigin.Begin);
                                bool Desencriptar = true;
                                try
                                {
                                    // Verify if the file can be loaded as a zip file, meaning it isn't encrypted.
                                    SevenZipExtractor Extractor_7_Zip = new SevenZipExtractor(Lector);
                                    if (Extractor_7_Zip != null && Extractor_7_Zip.FilesCount > 0 && Extractor_7_Zip.Check())
                                    {
                                        Desencriptar = false; // The file somehow isn't encrypted.
                                    }
                                    Extractor_7_Zip = null; // Don't dispose this or it'll close the input file.
                                }
                                catch { Desencriptar = true; }
                                Lector.Seek(0L, SeekOrigin.Begin); // The zip reader will change this.
                                for (long Índice_Bloque = 0L; Índice_Bloque < Lector.Length; Índice_Bloque += 4096L)
                                {
                                    int Longitud = Lector.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                    if (Longitud > 0)
                                    {
                                        if (Desencriptar) Matriz_Bytes = Jupisoft_Encrypting_Decrypting.Desencriptar_Matriz_Bytes(Matriz_Bytes, true, false, true, false);
                                        Lector_Salida.Write(Matriz_Bytes, 0, Longitud); // Write the decrypted byte array.
                                    }
                                    else break;
                                }
                            }
                            Lector.Close(); // Close this input file.
                            Lector.Dispose();
                            Lector = null;
                        }
                    }
                    Matriz_Bytes = null;
                    Lector_Salida.Close(); // Close the output file.
                    Lector_Salida.Dispose();
                    Lector_Salida = null;
                    Program.Ejecutar_Ruta(Program.Ruta_Guardado_Imágenes_Secretos, ProcessWindowStyle.Maximized);
                }
                else MessageBox.Show(this, "The desired encrypted secret file couldn't be found.\r\nMake sure you have downloaded the \"secrets\" version of this application (the one which includes the optional files, including the secret ones).", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Botón_Packs_Skins_1st_Birthday_Skin_Pack_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\B12FE96A", "1st Birthday Skin Pack", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_2nd_Birthday_Skin_Pack_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\6CFC63B7", "2nd Birthday Skin Pack", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_3rd_Birthday_Skin_Pack_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\437C4660", "3rd Birthday Skin Pack", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_4th_Birthday_Skin_Pack_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\8003522", "4th Birthday Skin Pack", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Adventure_Time_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\3CBE35FB", "Adventure Time", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Battle_Beasts_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\E0F86053", "Battle & Beasts", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Battle_Beasts_2_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\35EC2BE3", "Battle & Beasts 2", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Biome_Settlers_Pack_1_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\2BB7998E", "Biome Settlers Pack 1", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Campfire_Tales_Skin_Pack_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\A4AEE16C", "Campfire Tales Skin Pack", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Chinese_Mythology_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\BC278B06", "Chinese Mythology", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Doctor_Who_Skins_Volume_I_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\FE7F1F10", "Doctor Who Skins Volume I", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Doctor_Who_Skins_Volume_II_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\65B5D112", "Doctor Who Skins Volume II", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Fallout_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\A7C1F72C", "Fallout", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Festive_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\DED3A288", "Festive", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Festive_Skin_Pack_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\30FF6565", "Festive Skin Pack", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Greek_Mythology_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\67ACC6FF", "Greek Mythology", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Halloween_2015_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\B198CD49", "Halloween 2015", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Halloween_Charity_Skin_Pack_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\FCBDB984", "Halloween Charity Skin Pack", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Halo_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\2F12D85B", "Halo", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Magic_The_Gathering_Skin_Pack_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\622D9DAB", "Magic The Gathering Skin Pack", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Marvel_Avengers_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\688711E4", "Marvel Avengers", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Marvel_Guardians_of_the_Galaxy_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\89F83E2D", "Marvel Guardians of the Galaxy", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Marvel_Spider_Man_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\FC5E8B6E", "Marvel Spider-Man", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Mass_Effect_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\BEFB1F9F", "Mass Effect", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Minecon_2015_Skin_Pack_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\A53ECB15", "Minecon 2015 Skin Pack", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Mini_Game_Masters_Skin_Pack_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\C2189632", "Mini Game Masters Skin Pack", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Power_Rangers_Skin_Pack_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\AFB257C2", "Power Rangers Skin Pack", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Redstone_Specialists_Skin_Pack_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\3B9CAE45", "Redstone Specialists Skin Pack", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Skin_Pack_1_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\C7D5C98A", "Skin Pack 1", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Skin_Pack_2_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\9FF4FCEE", "Skin Pack 2", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Skin_Pack_3_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\8C2D7039", "Skin Pack 3", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Skin_Pack_4_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\14D1BD29", "Skin Pack 4", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Skin_Pack_5_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\1B0C2589", "Skin Pack 5", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Skin_Pack_6_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\DE93409", "Skin Pack 6", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Skyrim_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\EFA4B512", "Skyrim", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Star_Wars_Classic_Skin_Pack_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\18F1442D", "Star Wars Classic Skin Pack", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Star_Wars_Prequel_Skin_Pack_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\7025C634", "Star Wars Prequel Skin Pack", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Star_Wars_Rebels_Skin_Pack_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\5341A607", "Star Wars Rebels Skin Pack", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Story_Mode_Skin_Pack_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\5987E9B2", "Story Mode Skin Pack", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Summer_of_Arcade_Skin_Pack_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\9871C4F3", "Summer of Arcade Skin Pack", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_The_Simpsons_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\F2B4919C", "The Simpsons", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Skins_Villains_Skin_Pack_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\E7C858B1", "Villains Skin Pack", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Recursos_Adventure_Time_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Resources\\269BB7A4", "Console Adventure Time", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Recursos_Candy_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Resources\\89483C6D", "Console Candy", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Recursos_Cartoon_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Resources\\CF7FB669", "Console Cartoon", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Recursos_Chinese_Mythology_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Resources\\C57D709B", "Console Chinese Mythology", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Recursos_City_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Resources\\34C2F8A8", "Console City", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Recursos_Fallout_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Resources\\F512D9CB", "Console Fallout", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Recursos_Fantasy_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Resources\\42E12AC6", "Console Fantasy", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Recursos_Festive_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Resources\\E86DD8D1", "Console Festive", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Recursos_Greek_Mythology_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Resources\\D5F7FC0B", "Console Greek Mythology", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Recursos_Halloween_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Resources\\E9D6C865", "Console Halloween", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Recursos_Halloween_2015_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Resources\\FF4896C0", "Console Halloween 2015", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Recursos_Halo_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Resources\\EB1255D9", "Console Halo", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Recursos_Mass_Effect_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Resources\\72D44EAC", "Console Mass Effect", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Recursos_Natural_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Resources\\118FA1D5", "Console Natural", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Recursos_Pattern_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Resources\\75955242", "Console Pattern", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Recursos_Plastic_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Resources\\47D86650", "Console Plastic", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Recursos_Skyrim_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Resources\\680A9114", "Console Skyrim", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Recursos_Steampunk_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Resources\\CBA3979D", "Console Steampunk", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Recursos_Faithful_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://minecraft.curseforge.com/projects/faithful-32x", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Packs_Recursos_X_Ray_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://minecraft.curseforge.com/projects/xray-ultimate-1-11-compatible", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Cliente_1_0_0_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Clients\\C560F3F7", "1.0", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Cliente_1_1_0_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Clients\\85DE36A1", "1.1", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Cliente_1_2_5_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Clients\\E09534B2", "1.2.5", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Cliente_1_3_2_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Clients\\FC727F44", "1.3.2", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Cliente_1_4_7_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Clients\\555EDD3B", "1.4.7", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Cliente_1_5_2_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Clients\\25642598", "1.5.2", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Cliente_1_6_4_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Clients\\46477526", "1.6.4", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Cliente_1_7_10_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Clients\\6BBD3EA2", "1.7.10", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Cliente_1_8_8_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Clients\\CCD6E5FA", "1.8.8", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Cliente_1_9_4_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Clients\\65EAB3B8", "1.9.4", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Cliente_1_10_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Clients\\76949A07", "1.10", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Cliente_1_11_2_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Clients\\C3242F08", "1.11.2", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Cliente_1_12_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Clients\\FF9D499D", "1.12", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Cliente_1_13_2_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Clients\\9DB33EED", "1.13.2", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Cliente_1_14_Click(object sender, EventArgs e)
        {
            try
            {
                //Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Servers\\00000000", "1.14", ".jar");
                SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Cliente_1_15_Click(object sender, EventArgs e)
        {
            try
            {
                //Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Servers\\00000000", "1.15", ".jar");
                SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Cliente_1_16_Click(object sender, EventArgs e)
        {
            try
            {
                //Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Servers\\00000000", "1.16", ".jar");
                SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Servidor_1_0_0_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Servers\\00000000", "minecraft_server.1.0.0", ".jar"); // Couldn't find anywhere the official 1.0.0 server jar.
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Servidor_1_1_0_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Servers\\00000000", "minecraft_server.1.1.0", ".jar"); // Couldn't find anywhere the official 1.1.0 server jar.
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Servidor_1_2_5_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Servers\\DA957474", "minecraft_server.1.2.5", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Servidor_1_3_2_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Servers\\E3B2CBC9", "minecraft_server.1.3.2", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Servidor_1_4_7_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Servers\\97090A20", "minecraft_server.1.4.7", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Servidor_1_5_2_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Servers\\72B6E195", "minecraft_server.1.5.2", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Servidor_1_6_4_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Servers\\A7C604EC", "minecraft_server.1.6.4", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Servidor_1_7_10_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Servers\\734E9050", "minecraft_server.1.7.10", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Servidor_1_8_8_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Servers\\12AD1E23", "minecraft_server.1.8.8", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Servidor_1_9_4_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Servers\\32A8A5E0", "minecraft_server.1.9.4", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Servidor_1_10_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Servers\\E91FE11B", "minecraft_server.1.10", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Servidor_1_11_2_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Servers\\D6E7D9B7", "minecraft_server.1.11.2", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Servidor_1_12_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Servers\\AE3062D5", "minecraft_server.1.12", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Servidor_1_13_2_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Servers\\ED9779A8", "minecraft_server.1.13.2", ".jar");
                SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Servidor_1_14_Click(object sender, EventArgs e)
        {
            try
            {
                //Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Servers\\00000000", "minecraft_server.1.14", ".jar");
                SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Servidor_1_15_Click(object sender, EventArgs e)
        {
            try
            {
                //Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Servers\\00000000", "minecraft_server.1.15", ".jar");
                SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Servidor_1_16_Click(object sender, EventArgs e)
        {
            try
            {
                //Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Servers\\00000000", "minecraft_server.1.16", ".jar");
                SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_1_0_0_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Sources\\EC22248B", "Minecraft 1.0.0 source", ".zip"); // Couldn't find the Minecraft 1.0.0 server jar file.
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_1_1_0_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Sources\\810486E4", "Minecraft 1.1.0 source", ".zip"); // Couldn't find the Minecraft 1.1.0 server jar file.
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_1_2_5_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Sources\\98F3C532", "Minecraft 1.2.5 source", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_1_3_2_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Sources\\ED9DE6E4", "Minecraft 1.3.2 source", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_1_4_7_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Sources\\9B053B77", "Minecraft 1.4.7 source", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_1_5_2_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Sources\\62B6F330", "Minecraft 1.5.2 source", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_1_6_4_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Sources\\7B7F95E6", "Minecraft 1.6.4 source", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_1_7_10_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Sources\\54BD5A59", "Minecraft 1.7.10 source", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_1_8_8_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Sources\\72B08AC6", "Minecraft 1.8.8 source", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_1_9_4_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Sources\\7CCDACF0", "Minecraft 1.9.4 source", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_1_10_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Sources\\D058CD75", "Minecraft 1.10 source", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_1_11_2_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Sources\\526FA4DF", "Minecraft 1.11.2 source", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_1_12_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Sources\\7C6FC8D9", "Minecraft 1.12 source", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_1_13_2_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Sources\\00000000", "Minecraft 1.13 source", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_1_14_Click(object sender, EventArgs e)
        {
            try
            {
                //Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Sources\\00000000", "Minecraft 1.14 source", ".zip");
                SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_1_15_Click(object sender, EventArgs e)
        {
            try
            {
                //Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Sources\\00000000", "Minecraft 1.15 source", ".zip");
                SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_1_16_Click(object sender, EventArgs e)
        {
            try
            {
                //Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Sources\\00000000", "Minecraft 1.16 source", ".zip");
                SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Jupisoft_Cuadros_HD_1_12_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.mediafire.com/file/rd8eeuu5eh25v2d/Jupisoft_HD_Paintings_%255B1.12%255D.zip/file", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Jupisoft_Cuadros_HD_1_13_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.mediafire.com/file/cc7s5wpwwqs5hda/Jupisoft_HD_Paintings_%255B1.13%255D.zip/file", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Jupisoft_Monster_High_HD_1_12_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.mediafire.com/file/0jtqep7p0h7b8ax/Jupisoft_MH_Paintings_%255B1.12%255D.zip/file", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Jupisoft_Monster_High_HD_1_13_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.mediafire.com/file/p2lit4j8id2doic/Jupisoft_MH_Paintings_%255B1.13%255D.zip/file", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Jupisoft_Cielo_HD_1_12_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.mediafire.com/file/25qcuu5tydktrcd/Jupisoft_Optifine_HD_Sky_%255B1.12%255D.zip/file", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Jupisoft_Cielo_HD_1_13_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.mediafire.com/file/ykd3d3v3v34dd2g/Jupisoft_Optifine_HD_Sky_%255B1.13%255D.zip/file", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Optifine_1_7_10_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Optifine\\63771667", "OptiFine_1.7.10_HD_U_E7", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Optifine_1_8_0_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Optifine\\D1EA855C", "OptiFine_1.8.0_HD_U_I7", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Optifine_1_8_8_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Optifine\\476B3C0", "OptiFine_1.8.8_HD_U_I7", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Optifine_1_8_9_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Optifine\\A6A8F3A5", "OptiFine_1.8.9_HD_U_I7", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Optifine_1_9_0_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Optifine\\2829A67F", "OptiFine_1.9.0_HD_U_E7", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Optifine_1_9_2_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Optifine\\1F69E697", "OptiFine_1.9.2_HD_U_E3", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Optifine_1_9_4_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Optifine\\963E2CB0", "OptiFine_1.9.4_HD_U_E7", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Optifine_1_10_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Optifine\\90778F0A", "OptiFine_1.10_HD_U_E7", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Optifine_1_10_2_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Optifine\\B7671E79", "OptiFine_1.10.2_HD_U_E7", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Optifine_1_11_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Optifine\\E90364C0", "OptiFine_1.11_HD_U_C7", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Optifine_1_11_2_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Optifine\\6144A41B", "OptiFine_1.11.2_HD_U_C7", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Optifine_1_12_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Optifine\\A98DC412", "OptiFine_1.12_HD_U_C7", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Optifine_1_12_1_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Optifine\\BC7EC82C", "OptiFine_1.12.1_HD_U_C7", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Optifine_1_12_2_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Optifine\\314977B8", "OptiFine_1.12.2_HD_U_E3", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Optifine_1_13_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Optifine\\3CA92E19", "OptiFine_1.13_HD_U_E4", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Optifine_1_13_1_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Optifine\\547DF024", "OptiFine_1.13.1_HD_U_E4", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Optifine_1_13_2_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Optifine\\E41A62F2", "OptiFine_1.13.2_HD_U_E7", ".jar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Versión_Indev_InfDev_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Versions\\9227EF64", "Minecraft Indev (please open Matts ReadMe.txt!)", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Versión_April_Fools_Red_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Versions\\B65219A5", "minecraft_2point0_red", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Minecraft_Versión_April_Fools_Purple_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Versions\\E7ABB0B0", "minecraft_2point0_purple", ".zip");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Descargar_Minecraft_1_0_0_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.mediafire.com/?s7dyeugk867no9j", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Descargar_Minecraft_1_1_0_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.mediafire.com/?wu9gfhy73m4k6a4", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Descargar_Minecraft_1_2_5_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.mediafire.com/?c6liau295225253", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Descargar_Minecraft_1_3_2_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.mediafire.com/?38vjh7hrpprrw1b", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Descargar_Minecraft_1_4_7_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.mediafire.com/?07d59w314ewjfth", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Descargar_Minecraft_1_5_2_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.mediafire.com/?95vlzp1a4n4wjqw", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Descargar_Minecraft_1_6_4_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.mediafire.com/?96mrmeo57cdf6zv", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Descargar_Minecraft_1_7_10_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.modcoderpack.com/files/mcp908.zip", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Descargar_Minecraft_1_8_8_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.modcoderpack.com/files/mcp918.zip", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Descargar_Minecraft_1_9_4_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.modcoderpack.com/files/mcp928.zip", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Descargar_Minecraft_1_10_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.modcoderpack.com/files/mcp931.zip", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Descargar_Minecraft_1_11_2_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.modcoderpack.com/files/mcp937.zip", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Descargar_Minecraft_1_12_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.modcoderpack.com/files/mcp940.zip", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Extras_26601_Skins_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Skins\\A24938DB", "26601 Minecraft Skins", ".rar");
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Extras_Fuente_Alfabeto_Galáctico_Click(object sender, EventArgs e)
        {
            try
            {
                Desencriptar_Exportar_Archivo_Secreto(Application.StartupPath + "\\Secrets\\Others\\DD08ECF8", "Standard Galactic Alphabet font", ".zip");
                // Program.Ejecutar_Ruta("http://legacy.3drealms.com/stuff/sga_ttf.zip", ProcessWindowStyle.Normal);
                // Program.Ejecutar_Ruta("https://www.minecraftforum.net/forums/minecraft-java-edition/discussion/157963-the-standard-galactic-alphabet", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Extras_URL_26601_Skins_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.mediafire.com/file/rhbf9vd9e002170/26601+Minecraft+Skins.rar", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Extras_Eclipse_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.eclipse.org/downloads/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Extras_Java_SDK_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.oracle.com/technetwork/java/javase/downloads/index.html", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Extras_MCP_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.modcoderpack.com/website/releases", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Extras_Xbox_360_Tutoriales_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.minecraftforum.net/forums/mapping-and-modding-java-edition/maps/maps-discussion/1557054-xbox-360-edition-tutorial-world-maps-for-pc", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
