using Microsoft.Win32;
using Minecraft_Tools.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Salvapantallas_El_Fin : Form
    {
        public Ventana_Salvapantallas_El_Fin()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "The End Screensaver by Jupisoft for " + Program.Texto_Usuario;
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

        internal int Ancho = 0;
        internal int Alto = 0;
        internal Graphics Pintar = null;
        internal Bitmap[] Matriz_Imágenes = new Bitmap[8];
        internal int[] Matriz_Ancho_Alto = new int[8];

        private void Ventana_Plantilla_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                //this.WindowState = FormWindowState.Maximized;

                if (!Variable_Siempre_Visible) this.TopMost = true;
                //Ventana_Maximizada = this.WindowState == FormWindowState.Maximized;
                Cursor.Hide();
                this.WindowState = FormWindowState.Normal;
                //this.TopMost = true;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;

                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;

                //Program.Guardar_Imagen_Temporal(Program.Obtener_Imagen_Alfa_Brillo(Resources.Salvapantallas_particlefield), "Salvapantallas_particlefield");
                //Program.Guardar_Imagen_Temporal(Program.Obtener_Imagen_Alfa_Brillo(Resources.Salvapantallas_tunnel), "Salvapantallas_tunnel");

                double Multiplicador = 0.6d;
                for (int Índice = 0; Índice < 8; Índice++)
                {
                    int Ancho_Alto = (int)Math.Round(512d * Multiplicador, MidpointRounding.AwayFromZero);
                    if (Ancho_Alto % 2 != 0) Ancho_Alto++;
                    Matriz_Imágenes[Índice] = Program.Obtener_Imagen_Miniatura(Resources.Salvapantallas_particlefield, Ancho_Alto, Ancho_Alto, true, false, CheckState.Checked);
                    Matriz_Ancho_Alto[Índice] = Ancho_Alto;
                    Multiplicador += 0.2d;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Ancho = Picture.ClientSize.Width;
                Alto = Picture.ClientSize.Height;
                Picture.Image = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                Pintar = Graphics.FromImage(Picture.Image);
                Pintar.CompositingMode = CompositingMode.SourceOver;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.None;
                Pintar.TranslateTransform((float)((double)Ancho / 2d), (float)((double)Alto / 2d));
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Cursor.Show();
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Plantilla_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
                {
                    string[] Matriz_Rutas = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                    if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                    {
                        foreach (string Ruta in Matriz_Rutas)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(Ruta) && (File.Exists(Ruta) || Directory.Exists(Ruta)))
                                {
                                    //Minecraft.Información_Niveles Información_Nivel = Minecraft.Información_Niveles.Obtener_Información_Nivel(Ruta);
                                    SystemSounds.Beep.Play();
                                    break;
                                }
                            }
                            catch (Exception Excepción)
                            {
                                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                continue;
                            }
                        }
                        Matriz_Rutas = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Plantilla_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Normal) this.WindowState = FormWindowState.Maximized;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Plantilla_KeyDown(object sender, KeyEventArgs e)
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
                    else if (e.KeyCode == Keys.Enter)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                }
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

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                /*if (Picture.Image != null)
                {
                    Clipboard.SetImage(Picture.Image);
                    SystemSounds.Asterisk.Play();
                }*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                /*if (Picture.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Minecraft);
                    Picture.Image.Save(Program.Ruta_Minecraft + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + ".png", ImageFormat.Png);
                    SystemSounds.Asterisk.Play();
                }*/
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

        internal double[] Matriz_Desviaciones = new double[8];

        private void Temporizador_Principal_Tick(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (Pintar != null)
                    {
                        /*double Multiplicador = 0.6d;
                        for (int Índice = 0; Índice < 8; Índice++)
                        {
                            int Ancho_Alto = (int)Math.Round(512d * Multiplicador, MidpointRounding.AwayFromZero);
                            Matriz_Imágenes[Índice] = Program.Obtener_Imagen_Miniatura(Resources.Salvapantallas_particlefield, Ancho_Alto, Ancho_Alto, true, false);
                            Matriz_Ancho_Alto[Índice] = Ancho_Alto;
                            Multiplicador += 0.2d;
                        }*/
                        Pintar.Clear(Color.Transparent);
                        double Multiplicador = 0.6d;
                        for (int Índice = 0; Índice < 8; Índice++)
                        {
                            //Pintar.ResetTransform();
                            //Pintar.TranslateTransform((float)((double)Ancho / 2d), (float)((double)Alto / 2d));
                            //Pintar.RotateTransform((float)((Índice * 360d) / 15d));
                            int Desviación = Índice % 4;
                            //TextureBrush Pincel = new TextureBrush(Program.Obtener_Imagen_Miniatura(Resources.Salvapantallas_particlefield.Clone(new RectangleF(Desviación < 2 ? (float)Matriz_Desviaciones[(int)Índice] : 0f, Desviación >= 2 ? (float)Matriz_Desviaciones[(int)Índice] : 0f, 256f, 256f), PixelFormat.Format32bppArgb), (int)Math.Round(256d * Multiplicador, MidpointRounding.AwayFromZero), (int)Math.Round(256d * Multiplicador, MidpointRounding.AwayFromZero), true, false), WrapMode.Tile);
                            int XY = (int)Math.Round(((double)(Matriz_Ancho_Alto[Índice] / 2) * Math.Abs(Matriz_Desviaciones[Índice])) / 100d, MidpointRounding.AwayFromZero);
                            if (XY < 0) XY = 0;
                            else if (XY >= Matriz_Ancho_Alto[Índice]) XY = Matriz_Ancho_Alto[Índice] - 1;
                            TextureBrush Pincel = new TextureBrush(Matriz_Imágenes[Índice].Clone(new RectangleF(Desviación < 2 ? (float)XY : 0f, Desviación >= 2 ? (float)XY : 0f, (float)(Matriz_Ancho_Alto[Índice] / 2), (float)(Matriz_Ancho_Alto[Índice] / 2)), PixelFormat.Format32bppArgb), WrapMode.Tile);
                            Pintar.FillRectangle(Pincel, (float)((double)-Ancho), (float)((double)-Alto), (float)((double)Ancho * 2d), (float)((double)Alto * 2d));
                            //Pintar.FillRectangle(Pincel, (float)((double)-Ancho + (Desviación < 2 ? Matriz_Desviaciones[(int)Índice] : 0)), (float)((double)-Alto + (Desviación >= 2 ? Matriz_Desviaciones[(int)Índice] : 0)), (float)((double)Ancho * 2d), (float)((double)Alto * 2d));
                            Pincel.Dispose();
                            Pincel = null;
                            if (Desviación % 2 == 0)
                            {
                                Matriz_Desviaciones[Índice] -= Multiplicador;
                                while (Matriz_Desviaciones[Índice] < 0d) Matriz_Desviaciones[Índice] += 100d;
                            }
                            else
                            {
                                Matriz_Desviaciones[Índice] += Multiplicador;
                                while (Matriz_Desviaciones[Índice] >= 100d) Matriz_Desviaciones[Índice] -= 100d;
                            }
                            Multiplicador += 0.2d;
                        }
                        Picture.Invalidate();
                    }

                    /*float f1 = (float)this.tileEntityRenderer.playerX;
                    float f2 = (float)this.tileEntityRenderer.playerY;
                    float f3 = (float)this.tileEntityRenderer.playerZ;
                    //GL11.glDisable(GL11.GL_LIGHTING);
                    Random random = new Random(31100L);
                    float f4 = 0.75F;

                    for (int i = 0; i < 16; ++i)
                    {
                        //GL11.glPushMatrix();
                        float f5 = (float)(16 - i);
                        float f6 = 0.0625F;
                        float f7 = 1.0F / (f5 + 1.0F);

                        if (i == 0)
                        {
                            this.bindTextureByName(Resources.Salvapantallas_tunnel);
                            f7 = 0.1F;
                            f5 = 65.0F;
                            f6 = 0.125F;
                            GL11.glEnable(GL11.GL_BLEND);
                            GL11.glBlendFunc(GL11.GL_SRC_ALPHA, GL11.GL_ONE_MINUS_SRC_ALPHA);
                        }

                        if (i == 1)
                        {
                            this.bindTextureByName(Resources.Salvapantallas_particlefield);
                            //GL11.glEnable(GL11.GL_BLEND);
                            //GL11.glBlendFunc(GL11.GL_ONE, GL11.GL_ONE);
                            f6 = 0.5F;
                        }

                        float f8 = (float)(-(par4 + (double)f4));
                        float f9 = f8 + ActiveRenderInfo.objectY;
                        float f10 = f8 + f5 + ActiveRenderInfo.objectY;
                        float f11 = f9 / f10;
                        f11 += (float)(par4 + (double)f4);
                        GL11.glTranslatef(f1, f11, f3);
                        GL11.glTexGeni(GL11.GL_S, GL11.GL_TEXTURE_GEN_MODE, GL11.GL_OBJECT_LINEAR);
                        GL11.glTexGeni(GL11.GL_T, GL11.GL_TEXTURE_GEN_MODE, GL11.GL_OBJECT_LINEAR);
                        GL11.glTexGeni(GL11.GL_R, GL11.GL_TEXTURE_GEN_MODE, GL11.GL_OBJECT_LINEAR);
                        GL11.glTexGeni(GL11.GL_Q, GL11.GL_TEXTURE_GEN_MODE, GL11.GL_EYE_LINEAR);
                        GL11.glTexGen(GL11.GL_S, GL11.GL_OBJECT_PLANE, this.func_76907_a(1.0F, 0.0F, 0.0F, 0.0F));
                        GL11.glTexGen(GL11.GL_T, GL11.GL_OBJECT_PLANE, this.func_76907_a(0.0F, 0.0F, 1.0F, 0.0F));
                        GL11.glTexGen(GL11.GL_R, GL11.GL_OBJECT_PLANE, this.func_76907_a(0.0F, 0.0F, 0.0F, 1.0F));
                        GL11.glTexGen(GL11.GL_Q, GL11.GL_EYE_PLANE, this.func_76907_a(0.0F, 1.0F, 0.0F, 0.0F));
                        GL11.glEnable(GL11.GL_TEXTURE_GEN_S);
                        GL11.glEnable(GL11.GL_TEXTURE_GEN_T);
                        GL11.glEnable(GL11.GL_TEXTURE_GEN_R);
                        GL11.glEnable(GL11.GL_TEXTURE_GEN_Q);
                        GL11.glPopMatrix();
                        GL11.glMatrixMode(GL11.GL_TEXTURE);
                        GL11.glPushMatrix();
                        GL11.glLoadIdentity();
                        GL11.glTranslatef(0.0F, (float)((long)Environment.TickCount % 700000L) / 700000.0F, 0.0F);
                        GL11.glScalef(f6, f6, f6);
                        GL11.glTranslatef(0.5F, 0.5F, 0.0F);
                        GL11.glRotatef((float)(i * i * 4321 + i * 9) * 2.0F, 0.0F, 0.0F, 1.0F);
                        GL11.glTranslatef(-0.5F, -0.5F, 0.0F);
                        GL11.glTranslatef(-f1, -f3, -f2);
                        f9 = f8 + ActiveRenderInfo.objectY;
                        GL11.glTranslatef(ActiveRenderInfo.objectX * f5 / f9, ActiveRenderInfo.objectZ * f5 / f9, -f2);
                        Tessellator tessellator = Tessellator.instance;
                        tessellator.startDrawingQuads();
                        f11 = random.nextFloat() * 0.5F + 0.1F;
                        float f12 = random.nextFloat() * 0.5F + 0.4F;
                        float f13 = random.nextFloat() * 0.5F + 0.5F;

                        if (i == 0)
                        {
                            f13 = 1.0F;
                            f12 = 1.0F;
                            f11 = 1.0F;
                        }

                        tessellator.setColorRGBA_F(f11 * f7, f12 * f7, f13 * f7, 1.0F);
                        tessellator.addVertex(par2, par4 + (double)f4, par6);
                        tessellator.addVertex(par2, par4 + (double)f4, par6 + 1.0D);
                        tessellator.addVertex(par2 + 1.0D, par4 + (double)f4, par6 + 1.0D);
                        tessellator.addVertex(par2 + 1.0D, par4 + (double)f4, par6);
                        tessellator.draw();
                        //GL11.glPopMatrix();
                        //GL11.glMatrixMode(GL11.GL_MODELVIEW);
                    }

                    //GL11.glDisable(GL11.GL_BLEND);
                    //GL11.glDisable(GL11.GL_TEXTURE_GEN_S);
                    //GL11.glDisable(GL11.GL_TEXTURE_GEN_T);
                    //GL11.glDisable(GL11.GL_TEXTURE_GEN_R);
                    //GL11.glDisable(GL11.GL_TEXTURE_GEN_Q);
                    //GL11.glEnable(GL11.GL_LIGHTING);*/
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
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Right)
                {
                    this.Close();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
