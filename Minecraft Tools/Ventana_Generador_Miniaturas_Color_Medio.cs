using ImageMagick;
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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Generador_Miniaturas_Color_Medio : Form
    {
        public Ventana_Generador_Miniaturas_Color_Medio()
        {
            InitializeComponent();
        }


        internal static bool Variable_Mantener_Dimensiones = false;
        internal string Variable_Nombre_Imagen = null;

        internal readonly string Texto_Título = "Thumbnails and Average Color Generator by Jupisoft for " + Program.Texto_Usuario;
        internal bool Variable_Siempre_Visible = false;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;
        internal static Stopwatch Cronómetro_FPS = Stopwatch.StartNew();
        internal long Segundo_FPS_Anterior = 0L;
        internal long FPS_Temporal = 0L;
        internal long FPS_Real = 0L;
        internal string Texto_Portapapeles = null;

        internal bool Ocupado = false;
        internal Bitmap Imagen_Original = null;
        internal int Ancho_Original = 0;
        internal int Alto_Original = 0;
        internal Bitmap Imagen_Miniatura = null;
        internal int Ancho_Miniatura = 0;
        internal int Alto_Miniatura = 0;

        private void Ventana_Generador_Miniaturas_Color_Medio_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                Ocupado = true;
                ComboBox_Relación_Aspecto.SelectedIndex = 0;
                Menú_Contextual_Preconfiguración_Alta_Calidad.PerformClick();
                Ocupado = false;
                Establecer_Color_Medio_Imagen(Color.White, 0d, 0d, 100d);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Miniaturas_Color_Medio_Shown(object sender, EventArgs e)
        {
            try
            {
                // Used to decode the night vision potion color to generate a custom icon.
                // Is it R = 31, G = 31, B = 161?
                /*        REGISTRY.register(1, new ResourceLocation("speed"), (new Potion(false, 8171462)).setPotionName("effect.moveSpeed").setIconIndex(0, 0).registerPotionAttributeModifier(SharedMonsterAttributes.MOVEMENT_SPEED, "91AEAA56-376B-4498-935B-2F7F68070635", 0.20000000298023224D, 2).setBeneficial());
        REGISTRY.register(2, new ResourceLocation("slowness"), (new Potion(true, 5926017)).setPotionName("effect.moveSlowdown").setIconIndex(1, 0).registerPotionAttributeModifier(SharedMonsterAttributes.MOVEMENT_SPEED, "7107DE5E-7CE8-4030-940E-514C1F160890", -0.15000000596046448D, 2));
        REGISTRY.register(3, new ResourceLocation("haste"), (new Potion(false, 14270531)).setPotionName("effect.digSpeed").setIconIndex(2, 0).setEffectiveness(1.5D).setBeneficial().registerPotionAttributeModifier(SharedMonsterAttributes.ATTACK_SPEED, "AF8B6E3F-3328-4C0A-AA36-5BA2BB9DBEF3", 0.10000000149011612D, 2));
        REGISTRY.register(4, new ResourceLocation("mining_fatigue"), (new Potion(true, 4866583)).setPotionName("effect.digSlowDown").setIconIndex(3, 0).registerPotionAttributeModifier(SharedMonsterAttributes.ATTACK_SPEED, "55FCED67-E92A-486E-9800-B47F202C4386", -0.10000000149011612D, 2));
        REGISTRY.register(5, new ResourceLocation("strength"), (new PotionAttackDamage(false, 9643043, 3.0D)).setPotionName("effect.damageBoost").setIconIndex(4, 0).registerPotionAttributeModifier(SharedMonsterAttributes.ATTACK_DAMAGE, "648D7064-6A60-4F59-8ABE-C2C23A6DD7A9", 0.0D, 0).setBeneficial());
        REGISTRY.register(6, new ResourceLocation("instant_health"), (new PotionHealth(false, 16262179)).setPotionName("effect.heal").setBeneficial());
        REGISTRY.register(7, new ResourceLocation("instant_damage"), (new PotionHealth(true, 4393481)).setPotionName("effect.harm").setBeneficial());
        REGISTRY.register(8, new ResourceLocation("jump_boost"), (new Potion(false, 2293580)).setPotionName("effect.jump").setIconIndex(2, 1).setBeneficial());
        REGISTRY.register(9, new ResourceLocation("nausea"), (new Potion(true, 5578058)).setPotionName("effect.confusion").setIconIndex(3, 1).setEffectiveness(0.25D));
        REGISTRY.register(10, new ResourceLocation("regeneration"), (new Potion(false, 13458603)).setPotionName("effect.regeneration").setIconIndex(7, 0).setEffectiveness(0.25D).setBeneficial());
        REGISTRY.register(11, new ResourceLocation("resistance"), (new Potion(false, 10044730)).setPotionName("effect.resistance").setIconIndex(6, 1).setBeneficial());
        REGISTRY.register(12, new ResourceLocation("fire_resistance"), (new Potion(false, 14981690)).setPotionName("effect.fireResistance").setIconIndex(7, 1).setBeneficial());
        REGISTRY.register(13, new ResourceLocation("water_breathing"), (new Potion(false, 3035801)).setPotionName("effect.waterBreathing").setIconIndex(0, 2).setBeneficial());
        REGISTRY.register(14, new ResourceLocation("invisibility"), (new Potion(false, 8356754)).setPotionName("effect.invisibility").setIconIndex(0, 1).setBeneficial());
        REGISTRY.register(15, new ResourceLocation("blindness"), (new Potion(true, 2039587)).setPotionName("effect.blindness").setIconIndex(5, 1).setEffectiveness(0.25D));
        REGISTRY.register(16, new ResourceLocation("night_vision"), (new Potion(false, 2039713)).setPotionName("effect.nightVision").setIconIndex(4, 1).setBeneficial());
        REGISTRY.register(17, new ResourceLocation("hunger"), (new Potion(true, 5797459)).setPotionName("effect.hunger").setIconIndex(1, 1));
        REGISTRY.register(18, new ResourceLocation("weakness"), (new PotionAttackDamage(true, 4738376, -4.0D)).setPotionName("effect.weakness").setIconIndex(5, 0).registerPotionAttributeModifier(SharedMonsterAttributes.ATTACK_DAMAGE, "22653B89-116E-49DC-9B6B-9971489B5BE5", 0.0D, 0));
        REGISTRY.register(19, new ResourceLocation("poison"), (new Potion(true, 5149489)).setPotionName("effect.poison").setIconIndex(6, 0).setEffectiveness(0.25D));
        REGISTRY.register(20, new ResourceLocation("wither"), (new Potion(true, 3484199)).setPotionName("effect.wither").setIconIndex(1, 2).setEffectiveness(0.25D));
        REGISTRY.register(21, new ResourceLocation("health_boost"), (new PotionHealthBoost(false, 16284963)).setPotionName("effect.healthBoost").setIconIndex(7, 2).registerPotionAttributeModifier(SharedMonsterAttributes.MAX_HEALTH, "5D6F0BA2-1186-46AC-B896-C61C5CEE99CC", 4.0D, 0).setBeneficial());
        REGISTRY.register(22, new ResourceLocation("absorption"), (new PotionAbsorption(false, 2445989)).setPotionName("effect.absorption").setIconIndex(2, 2).setBeneficial());
        REGISTRY.register(23, new ResourceLocation("saturation"), (new PotionHealth(false, 16262179)).setPotionName("effect.saturation").setBeneficial());
        REGISTRY.register(24, new ResourceLocation("glowing"), (new Potion(false, 9740385)).setPotionName("effect.glowing").setIconIndex(4, 2));
        REGISTRY.register(25, new ResourceLocation("levitation"), (new Potion(true, 13565951)).setPotionName("effect.levitation").setIconIndex(3, 2));
        REGISTRY.register(26, new ResourceLocation("luck"), (new Potion(false, 3381504)).setPotionName("effect.luck").setIconIndex(5, 2).setBeneficial().registerPotionAttributeModifier(SharedMonsterAttributes.LUCK, "03C3C89D-7037-4B42-869F-B146BCB64D2E", 1.0D, 0));
        REGISTRY.register(27, new ResourceLocation("unluck"), (new Potion(true, 12624973)).setPotionName("effect.unluck").setIconIndex(6, 2).registerPotionAttributeModifier(SharedMonsterAttributes.LUCK, "CC5AF142-2BD2-4215-B636-2605AED11727", -1.0D, 0));*/
                /*//"00000000 00011111 00011111 10100001";
                int Valor_Color = 2039713;
                //int R = Valor_Color >> 16;
                //int G = Valor_Color >> 16;
                //int B = Valor_Color >> 16;
                int R = 31;
                int G = 31;
                int B = 161;*/
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Miniaturas_Color_Medio_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Miniaturas_Color_Medio_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Miniaturas_Color_Medio_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Miniaturas_Color_Medio_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
                {
                    string[] Matriz_Rutas = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                    if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                    {
                        foreach (string Ruta in Matriz_Rutas)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                                {
                                    FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                                    Image Imagen_Original = null;
                                    try { Imagen_Original = Image.FromStream(Lector, false, false); }
                                    catch { Imagen_Original = null; }
                                    if (Imagen_Original != null)
                                    {
                                        Ancho_Original = Imagen_Original.Width;
                                        Alto_Original = Imagen_Original.Height;
                                        Ocupado = true;
                                        if (!Variable_Mantener_Dimensiones)
                                        {
                                            Numérico_Ancho.Value = Ancho_Original;
                                            Numérico_Alto.Value = Alto_Original;
                                        }
                                        Ocupado = false;
                                        Bitmap Imagen = new Bitmap(Ancho_Original, Alto_Original, PixelFormat.Format32bppArgb);
                                        Graphics Pintar = Graphics.FromImage(Imagen);
                                        Pintar.CompositingMode = CompositingMode.SourceCopy;
                                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                        Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                        Pintar.SmoothingMode = SmoothingMode.None;
                                        Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho_Original, Alto_Original), new Rectangle(0, 0, Ancho_Original, Alto_Original), GraphicsUnit.Pixel);
                                        Pintar.Dispose();
                                        Pintar = null;
                                        this.Imagen_Original = (Bitmap)Imagen.Clone();
                                        int Píxeles = Ancho_Original * Alto_Original;
                                        this.Text = Texto_Título + " - [Dimensions: " + Program.Traducir_Número(Ancho_Original) + " x " + Program.Traducir_Número(Alto_Original) + (Píxeles != 1 ? " pixels" : " pixel") + " (" + Program.Traducir_Número_Decimales_Redondear((double)Píxeles / 1000000d, 4) + " MP)]";
                                        Picture.Image = Imagen;
                                        Picture.Refresh();
                                        if (Menú_Contextual_Calcular_Color_Medio.Checked)
                                        {
                                            Color Color_ARGB = Obtener_Color_Medio_Imagen(Imagen);
                                            double Matiz, Saturación, Luminosidad;
                                            Program.HSL.From_RGB(Color_ARGB.R, Color_ARGB.G, Color_ARGB.B, out Matiz, out Saturación, out Luminosidad);
                                            Establecer_Color_Medio_Imagen(Color_ARGB, Matiz, Saturación, Luminosidad);
                                        }
                                        else
                                        {
                                            Establecer_Color_Medio_Imagen(Color.Black, 0d, 0d, 0d);
                                        }
                                        Lector.Close();
                                        Lector.Dispose();
                                        Lector = null;
                                        Variable_Nombre_Imagen = Path.GetFileNameWithoutExtension(Ruta);
                                        if (Variable_Mantener_Dimensiones) Generar_Miniatura();
                                        return;
                                    }
                                }
                            }
                            catch (Exception Excepción)
                            {
                                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                this.Text = Texto_Título;
                                Picture.Image = null;
                                Picture.Refresh();
                                Establecer_Color_Medio_Imagen(Color.White, 0d, 0d, 100d);
                                continue;
                            }
                            finally
                            {
                                GC.Collect();
                                GC.GetTotalMemory(true);
                            }
                        }
                        // If none on the files were valid images, save it's bytes as text files:
                        if (Matriz_Rutas.Length > 1) Array.Sort(Matriz_Rutas); // Sort the array of paths.
                        string Ruta_Matriz_Bytes = Program.Ruta_Guardado_Imágenes_Generador_Miniaturas_Color_Medio + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " Byte array.txt";
                        FileStream Lector_Matriz_Bytes = null;
                        StreamWriter Lector_Texto_Matriz_Bytes = null;
                        foreach (string Ruta in Matriz_Rutas)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                                {
                                    FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                                    if (Lector.Length > 0L && Lector.Length <= 104857600L) // Save to the default folder the dropped files if they aren't images, and only for those less or equal than 100 MB, as byte arrays in C# source code, useful for having internally files like original Minecraft structures.
                                    {
                                        if (Lector_Matriz_Bytes == null || Lector_Texto_Matriz_Bytes == null) // Start the text exporters
                                        {
                                            Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Generador_Miniaturas_Color_Medio);
                                            Lector_Matriz_Bytes = new FileStream(Ruta_Matriz_Bytes, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                            Lector_Matriz_Bytes.SetLength(0L);
                                            Lector_Matriz_Bytes.Seek(0L, SeekOrigin.Begin);
                                            Lector_Texto_Matriz_Bytes = new StreamWriter(Lector_Matriz_Bytes, Encoding.Unicode);
                                        }
                                        Lector_Texto_Matriz_Bytes.WriteLine(/*new string(' ', 8) + */"internal static readonly byte[] Matriz_Bytes_" + Path.GetFileNameWithoutExtension(Ruta).Replace(' ', '_') + " = new byte[" + Lector.Length.ToString() + "]");
                                        Lector_Texto_Matriz_Bytes.WriteLine(new string(' ', 8) + "{");
                                        Lector_Texto_Matriz_Bytes.Flush();
                                        byte[] Matriz_Bytes = new byte[4096];
                                        string Texto_Bytes = null;
                                        Lector.Seek(0L, SeekOrigin.Begin);
                                        for (long Índice_Bloque = 0L; Índice_Bloque < Lector.Length; Índice_Bloque += 4096L)
                                        {
                                            int Longitud = Lector.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                            for (int Índice_Byte = 0; Índice_Byte < Longitud; Índice_Byte++)
                                            {
                                                Texto_Bytes += Matriz_Bytes[Índice_Byte].ToString() + ", ";
                                                if (Texto_Bytes.Length >= 64)
                                                {
                                                    Lector_Texto_Matriz_Bytes.WriteLine(new string(' ', 12) + Texto_Bytes.Trim());
                                                    Lector_Texto_Matriz_Bytes.Flush();
                                                    Texto_Bytes = null;
                                                }
                                            }
                                        }
                                        if (Texto_Bytes.Length > 0)
                                        {
                                            Lector_Texto_Matriz_Bytes.WriteLine(new string(' ', 12) + Texto_Bytes.Trim().TrimEnd(",".ToCharArray()));
                                            Lector_Texto_Matriz_Bytes.Flush();
                                            Texto_Bytes = null;
                                        }
                                        Lector_Texto_Matriz_Bytes.WriteLine(new string(' ', 8) + "};");
                                        Lector_Texto_Matriz_Bytes.WriteLine();
                                        Lector_Texto_Matriz_Bytes.Flush();
                                        Matriz_Bytes = null;
                                    }
                                    Lector.Close();
                                    Lector.Dispose();
                                    Lector = null;
                                }
                            }
                            catch (Exception Excepción)
                            {
                                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                                continue;
                            }
                            finally
                            {
                                GC.Collect();
                                GC.GetTotalMemory(true);
                            }
                        }
                        // After all the valid files were exported in several byte arrays in a single file:
                        if (Lector_Matriz_Bytes != null && Lector_Texto_Matriz_Bytes != null) // Start the text exporters
                        {
                            SystemSounds.Asterisk.Play();
                            try
                            {
                                Lector_Texto_Matriz_Bytes.Close();
                                Lector_Texto_Matriz_Bytes.Dispose();
                                Lector_Texto_Matriz_Bytes = null;
                            }
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                            try
                            {
                                Lector_Matriz_Bytes.Close();
                                Lector_Matriz_Bytes.Dispose();
                                Lector_Matriz_Bytes = null;
                            }
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                        }
                        Matriz_Rutas = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally
            {
                Ocupado = false;
                this.Cursor = Cursors.Default;
            }
        }

        internal string Obtener_Color_Hexadecimal(Color Color_ARGB)
        {
            try
            {
                string Texto_R = Convert.ToString(Color_ARGB.R, 16);
                while (Texto_R.Length < 2) Texto_R = '0' + Texto_R;

                string Texto_G = Convert.ToString(Color_ARGB.G, 16);
                while (Texto_G.Length < 2) Texto_G = '0' + Texto_G;

                string Texto_B = Convert.ToString(Color_ARGB.B, 16);
                while (Texto_B.Length < 2) Texto_B = '0' + Texto_B;

                return ("#" + Texto_R + Texto_G + Texto_B).ToUpperInvariant();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return "?";
        }

        internal void Establecer_Color_Medio_Imagen(Color Color_ARGB, double Matiz, double Saturación, double Luminosidad)
        {
            try
            {

                if (Color_ARGB != Color.Empty)
                {
                    string Texto_Hexadecimal = Obtener_Color_Hexadecimal(Color_ARGB);

                    Barra_Estado_Etiqueta_Alfa.Image = Program.Obtener_Imagen_Color(Color.FromArgb(Color_ARGB.A, 255, 255, 255));
                    Barra_Estado_Etiqueta_Alfa.Text = "Alpha: " + Program.Traducir_Número(Color_ARGB.A);

                    Barra_Estado_Etiqueta_Rojo.Image = Program.Obtener_Imagen_Color(Color.FromArgb(255, Color_ARGB.R, 0, 0));
                    Barra_Estado_Etiqueta_Rojo.Text = "Red: " + Program.Traducir_Número(Color_ARGB.R);

                    Barra_Estado_Etiqueta_Verde.Image = Program.Obtener_Imagen_Color(Color.FromArgb(255, 0, Color_ARGB.G, 0));
                    Barra_Estado_Etiqueta_Verde.Text = "Green: " + Program.Traducir_Número(Color_ARGB.G);

                    Barra_Estado_Etiqueta_Azul.Image = Program.Obtener_Imagen_Color(Color.FromArgb(255, 0, 0, Color_ARGB.B));
                    Barra_Estado_Etiqueta_Azul.Text = "Blue: " + Program.Traducir_Número(Color_ARGB.B);

                    byte Rojo, Verde, Azul;
                    Program.HSL.To_RGB(Matiz, 100d, 50d, out Rojo, out Verde, out Azul);
                    Barra_Estado_Etiqueta_Matiz.Image = Program.Obtener_Imagen_Color(Color.FromArgb(255, Rojo, Verde, Azul));
                    Barra_Estado_Etiqueta_Matiz.Text = "Hue: " + Program.Traducir_Número(Math.Round(Matiz, MidpointRounding.AwayFromZero));
                    
                    Program.HSL.To_RGB(Matiz, Saturación, 50d, out Rojo, out Verde, out Azul);
                    Barra_Estado_Etiqueta_Saturación.Image = Program.Obtener_Imagen_Color(Color.FromArgb(255, Rojo, Verde, Azul));
                    Program.HSL.To_RGB(0d, 0d, Saturación, out Rojo, out Verde, out Azul);
                    Barra_Estado_Etiqueta_Saturación.Text = "Saturation: " + Program.Traducir_Número(Rojo);

                    Program.HSL.To_RGB(0d, 0d, Luminosidad, out Rojo, out Verde, out Azul);
                    Barra_Estado_Etiqueta_Luminosidad.Image = Program.Obtener_Imagen_Color(Color.FromArgb(255, Rojo, Verde, Azul));
                    Barra_Estado_Etiqueta_Luminosidad.Text = "Lightness: " + Program.Traducir_Número(Rojo);

                    int Gris = (int)Math.Round((double)(Color_ARGB.R + Color_ARGB.G + Color_ARGB.B) / 3d, MidpointRounding.AwayFromZero);
                    Barra_Estado_Etiqueta_Gris.Image = Program.Obtener_Imagen_Color(Color.FromArgb(255, Gris, Gris, Gris));
                    Barra_Estado_Etiqueta_Gris.Text = "Gray: " + Program.Traducir_Número(Gris);

                    Barra_Estado_Etiqueta_Hexadecimal.Image = Program.Obtener_Imagen_Color(Color.FromArgb(255, Color_ARGB.R, Color_ARGB.G, Color_ARGB.B));
                    Barra_Estado_Etiqueta_Hexadecimal.Text = "Hexadecimal: " + Texto_Hexadecimal;

                    Barra_Estado_Etiqueta_Código_Hash.Image = Program.Obtener_Imagen_Color(Color_ARGB);
                    Barra_Estado_Etiqueta_Código_Hash.Text = "Hash code: " + Program.Traducir_Número(Color_ARGB.GetHashCode());

                    Barra_Estado_Etiqueta_C_Sharp.Image = Program.Obtener_Imagen_Color(Color_ARGB);
                    Barra_Estado_Etiqueta_C_Sharp.Text = "C#: Color.FromArgb(" + Color_ARGB.A.ToString() + ", " + Color_ARGB.R.ToString() + ", " + Color_ARGB.G.ToString() + ", " + Color_ARGB.B.ToString() + ")";

                    Texto_Portapapeles = "ARGB: " + Color_ARGB.A.ToString() + ", " + Color_ARGB.R.ToString() + ", " + Color_ARGB.G.ToString() + ", " + Color_ARGB.B.ToString() + "; Hexadecimal: " + Texto_Hexadecimal + "; Hash code: " + Color_ARGB.GetHashCode().ToString() + ";";
                }
                else Establecer_Color_Medio_Imagen(Color.White, 0d, 0d, 100d);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Miniaturas_Color_Medio_KeyDown(object sender, KeyEventArgs e)
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
                        Generar_Miniatura();
                    }
                }
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

        internal Color Obtener_Color_Medio_Imagen(Bitmap Imagen_Original)
        {
            try
            {
                if (Imagen_Original != null)
                {
                    int Ancho = Imagen_Original.Width;
                    int Alto = Imagen_Original.Height;
                    byte Alfa_Máximo = 0;
                    BitmapData Bitmap_Data = Imagen_Original.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadOnly, Imagen_Original.PixelFormat);
                    byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Imagen_Original.Height];
                    int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Imagen_Original.Width * Image.GetPixelFormatSize(Imagen_Original.PixelFormat)) / 8);
                    int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? 4 : 3;
                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Imagen_Original.UnlockBits(Bitmap_Data);
                    if (Bytes_Aumento == 4)
                    {
                        for (int Índice_Y = 0, Índice_Bytes = 0; Índice_Y < Alto; Índice_Y++, Índice_Bytes += Bytes_Diferencia)
                        {
                            for (int Índice_X = 0; Índice_X < Ancho; Índice_X++, Índice_Bytes += Bytes_Aumento)
                            {
                                if (Matriz_Bytes[Índice_Bytes + 3] > Alfa_Máximo) Alfa_Máximo = Matriz_Bytes[Índice_Bytes + 3];
                            }
                        }
                    }
                    else Alfa_Máximo = 255;
                    //int Píxeles_Alfa_Mínimo;
                    //int Píxeles_Alfa_Máximo;
                    //Obtener_Alfa_Mínimo_Máximo(Imagen_Original, out Alfa_Mínimo, out Alfa_Máximo, out Píxeles_Alfa_Mínimo, out Píxeles_Alfa_Máximo);
                    //if (Alfa_Máximo <= 0) return Color_Transparente;
                    //if (!Píxeles_Alfa && Alfa_Máximo > 0 && Píxeles_Alfa_Máximo < Píxeles) return Color_Transparente;
                    //if (!Píxeles_Alfa && Alfa_Mínimo <= 0) return Color_Transparente;
                    int Ancho_Triple = Ancho * 3;
                    int Alto_Triple = Alto * 3;
                    Bitmap Imagen_Triple = new Bitmap(Ancho_Triple, Alto_Triple, PixelFormat.Format32bppArgb);
                    Graphics Pintar_Triple = Graphics.FromImage(Imagen_Triple);
                    Pintar_Triple.CompositingMode = CompositingMode.SourceCopy;
                    Pintar_Triple.DrawImage(Imagen_Original, new Rectangle(Ancho, Alto, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                    Pintar_Triple.Dispose();
                    Pintar_Triple = null;
                    QuantizeSettings Ajustes_Cuantización = new QuantizeSettings();
                    Ajustes_Cuantización.Colors = 2;
                    //Ajustes_Cuantización.ColorSpace = ColorSpace.RGB;
                    //Ajustes_Cuantización.DitherMethod = !FloydSteinbergDitherMethod.FloydSteinberg;
                    //Ajustes_Cuantización.MeasureErrors = false;
                    //Ajustes_Cuantización.TreeDepth = 8;
                    MagickImage Imagen_Cuantizada = new MagickImage(Imagen_Triple);
                    Imagen_Cuantizada.Quantize(Ajustes_Cuantización);
                    Bitmap Imagen = Imagen_Cuantizada.ToBitmap(ImageFormat.Png);
                    Imagen_Cuantizada.Dispose();
                    Imagen_Cuantizada = null;
                    Imagen_Triple.Dispose();
                    Imagen_Triple = null;
                    /*Bitmap Imagen = new Bitmap(Ancho * 3, Alto * 3, PixelFormat.Format32bppArgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.DrawImage(Imagen_Magick.ToBitmap(ImageFormat.Png), new Rectangle(0, 0, Imagen.Width, Imagen.Height), new Rectangle(0, 0, Imagen.Width, Imagen.Height), GraphicsUnit.Pixel);
                    Pintar.Dispose();
                    Pintar = null;*/
                    //Imagen.Save(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\abc\\" + Índice_XY.ToString() + ".png", ImageFormat.Png);
                    Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho_Triple, Alto_Triple), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                    Matriz_Bytes = new Byte[Math.Abs(Bitmap_Data.Stride) * Imagen.Height];
                    Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Imagen.Width * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                    Imagen.UnlockBits(Bitmap_Data);
                    Imagen.Dispose();
                    Imagen = null;
                    Dictionary<int, Color> Diccionario_Colores = new Dictionary<int, Color>();
                    //Dictionary<int, int> Diccionario_Píxeles = new Dictionary<int, int>();
                    int Código_Hash = 0;
                    for (int Índice_Y = 0, Índice_Bytes = 0; Índice_Y < Alto_Triple; Índice_Y++, Índice_Bytes += Bytes_Diferencia)
                    {
                        for (int Índice_X = 0; Índice_X < Ancho_Triple; Índice_X++, Índice_Bytes += 4)
                        {
                            Color Color_ARGB = Color.FromArgb(Matriz_Bytes[Índice_Bytes + 3], Matriz_Bytes[Índice_Bytes + 2], Matriz_Bytes[Índice_Bytes + 1], Matriz_Bytes[Índice_Bytes]);
                            Código_Hash = Color_ARGB.GetHashCode();
                            if (!Diccionario_Colores.ContainsKey(Código_Hash))
                            {
                                Diccionario_Colores.Add(Código_Hash, Color_ARGB);
                                //Diccionario_Píxeles.Add(Código_Hash, 1);
                            }
                            //else Diccionario_Píxeles[Código_Hash]++;
                        }
                    }
                    Color Color_ARGB_Opaco = Color.Transparent;
                    foreach (KeyValuePair<int, Color> Entrada in Diccionario_Colores)
                    {
                        if (Entrada.Value.A > Color_ARGB_Opaco.A)
                        {
                            Código_Hash = Entrada.Key;
                            Color_ARGB_Opaco = Entrada.Value;
                        }
                    }
                    Diccionario_Colores = null;
                    if (Color_ARGB_Opaco.A != Alfa_Máximo) Color_ARGB_Opaco = Color.FromArgb(Alfa_Máximo, Color_ARGB_Opaco.R, Color_ARGB_Opaco.G, Color_ARGB_Opaco.B);
                    //if (Alfa_Máximo < 255 && !Devolver_Alfa) Color_ARGB_Opaco = Color.FromArgb(255, Color_ARGB_Opaco.R, Color_ARGB_Opaco.G, Color_ARGB_Opaco.B);
                    //if (!Píxeles_Alfa && Color_ARGB_Opaco.A > 0 && Diccionario_Píxeles[Código_Hash] < Píxeles) Color_ARGB_Opaco = Color_Transparente;
                    //Diccionario_Píxeles = null;
                    /*if (Math.Abs(Alfa_Máximo - Color_ARGB_Opaco.A) >= 128)
                    {
                        MessageBox.Show(Alfa_Máximo.ToString(), Color_ARGB_Opaco.A.ToString());
                    }*/
                    return Color_ARGB_Opaco;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return Color.Transparent;
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.Image != null)
                {
                    Clipboard.SetImage(Picture.Image);
                    SystemSounds.Asterisk.Play();
                }
                /*
                if (!string.IsNullOrEmpty(Texto_Portapapeles))
                {
                    Clipboard.SetText(Texto_Portapapeles);
                    SystemSounds.Asterisk.Play();
                }*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Texto_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.Image != null)
                {
                    string Texto = null;
                    Texto += Barra_Estado_Etiqueta_Alfa + "\r\n";
                    Texto += Barra_Estado_Etiqueta_Rojo + "\r\n";
                    Texto += Barra_Estado_Etiqueta_Verde + "\r\n";
                    Texto += Barra_Estado_Etiqueta_Azul + "\r\n";
                    Texto += Barra_Estado_Etiqueta_Matiz + "\r\n";
                    Texto += Barra_Estado_Etiqueta_Saturación + "\r\n";
                    Texto += Barra_Estado_Etiqueta_Luminosidad + "\r\n";
                    Texto += Barra_Estado_Etiqueta_Gris + "\r\n";
                    Texto += Barra_Estado_Etiqueta_Hexadecimal + "\r\n";
                    Texto += Barra_Estado_Etiqueta_C_Sharp + "\r\n";
                    Texto += Barra_Estado_Etiqueta_Código_Hash + "\r\n";
                    Clipboard.SetText(Texto);
                    SystemSounds.Asterisk.Play();
                    Texto = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_JPEG_90_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Generador_Miniaturas_Color_Medio);
                    int Ancho = Picture.Image.Width;
                    int Alto = Picture.Image.Height;
                    ImageCodecInfo Codificador = Program.Obtener_Imagen_Codificador_Guid(ImageFormat.Jpeg.Guid);
                    if (Codificador != null) // We can choose any JPEG compression.
                    {
                        int Calidad_JPEG = 90; // 0 = Minimum, 100 = Maximum quality.
                        EncoderParameters Parámetros = new EncoderParameters(1);
                        Parámetros.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)Calidad_JPEG);
                        Picture.Image.Save(Program.Ruta_Guardado_Imágenes_Generador_Miniaturas_Color_Medio + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " " + Ancho.ToString() + " x " + Alto.ToString() + (Ancho * Alto != 1 ? " pixels" : " pixel") + (!string.IsNullOrEmpty(Variable_Nombre_Imagen) ? " " + Variable_Nombre_Imagen : null) + ".jpg", Codificador, Parámetros);
                        Parámetros.Dispose();
                        Parámetros = null;
                        Codificador = null;
                    }
                    else Picture.Image.Save(Program.Ruta_Guardado_Imágenes_Generador_Miniaturas_Color_Medio + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " " + Ancho.ToString() + " x " + Alto.ToString() + (Ancho * Alto != 1 ? " pixels" : " pixel") + (!string.IsNullOrEmpty(Variable_Nombre_Imagen) ? " " + Variable_Nombre_Imagen : null) + ".jpg", ImageFormat.Jpeg); // Default compression.
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_JPEG_100_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Generador_Miniaturas_Color_Medio);
                    int Ancho = Picture.Image.Width;
                    int Alto = Picture.Image.Height;
                    ImageCodecInfo Codificador = Program.Obtener_Imagen_Codificador_Guid(ImageFormat.Jpeg.Guid);
                    if (Codificador != null) // We can choose any JPEG compression.
                    {
                        int Calidad_JPEG = 100; // 0 = Minimum, 100 = Maximum quality.
                        EncoderParameters Parámetros = new EncoderParameters(1);
                        Parámetros.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)Calidad_JPEG);
                        Picture.Image.Save(Program.Ruta_Guardado_Imágenes_Generador_Miniaturas_Color_Medio + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " " + Ancho.ToString() + " x " + Alto.ToString() + (Ancho * Alto != 1 ? " pixels" : " pixel") + (!string.IsNullOrEmpty(Variable_Nombre_Imagen) ? " " + Variable_Nombre_Imagen : null) + ".jpg", Codificador, Parámetros);
                        Parámetros.Dispose();
                        Parámetros = null;
                        Codificador = null;
                    }
                    else Picture.Image.Save(Program.Ruta_Guardado_Imágenes_Generador_Miniaturas_Color_Medio + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " " + Ancho.ToString() + " x " + Alto.ToString() + (Ancho * Alto != 1 ? " pixels" : " pixel") + (!string.IsNullOrEmpty(Variable_Nombre_Imagen) ? " " + Variable_Nombre_Imagen : null) + ".jpg", ImageFormat.Jpeg); // Default compression.
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Generador_Miniaturas_Color_Medio);
                    int Ancho = Picture.Image.Width;
                    int Alto = Picture.Image.Height;
                    Picture.Image.Save(Program.Ruta_Guardado_Imágenes_Generador_Miniaturas_Color_Medio + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " " + Ancho.ToString() + " x " + Alto.ToString() + (Ancho * Alto != 1 ? " pixels" : " pixel") + (!string.IsNullOrEmpty(Variable_Nombre_Imagen) ? " " + Variable_Nombre_Imagen : null) + ".png", ImageFormat.Png);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Texto_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Generador_Miniaturas_Color_Medio);
                    string Texto = null;
                    Texto += Barra_Estado_Etiqueta_Alfa + "\r\n";
                    Texto += Barra_Estado_Etiqueta_Rojo + "\r\n";
                    Texto += Barra_Estado_Etiqueta_Verde + "\r\n";
                    Texto += Barra_Estado_Etiqueta_Azul + "\r\n";
                    Texto += Barra_Estado_Etiqueta_Matiz + "\r\n";
                    Texto += Barra_Estado_Etiqueta_Saturación + "\r\n";
                    Texto += Barra_Estado_Etiqueta_Luminosidad + "\r\n";
                    Texto += Barra_Estado_Etiqueta_Gris + "\r\n";
                    Texto += Barra_Estado_Etiqueta_Hexadecimal + "\r\n";
                    Texto += Barra_Estado_Etiqueta_C_Sharp + "\r\n";
                    Texto += Barra_Estado_Etiqueta_Código_Hash + "\r\n";
                    File.WriteAllText(Program.Ruta_Guardado_Imágenes_Generador_Miniaturas_Color_Medio + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + ".txt", Texto, Encoding.Default);
                    SystemSounds.Asterisk.Play();
                    Texto = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Barra_Estado_Etiqueta_Alfa_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
                {
                    Clipboard.SetText(Barra_Estado_Etiqueta_Alfa.Text.Substring(7));
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Barra_Estado_Etiqueta_Rojo_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
                {
                    Clipboard.SetText(Barra_Estado_Etiqueta_Rojo.Text.Substring(5));
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Barra_Estado_Etiqueta_Verde_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
                {
                    Clipboard.SetText(Barra_Estado_Etiqueta_Verde.Text.Substring(7));
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Barra_Estado_Etiqueta_Azul_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
                {
                    Clipboard.SetText(Barra_Estado_Etiqueta_Azul.Text.Substring(6));
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Barra_Estado_Etiqueta_Hexadecimal_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
                {
                    Clipboard.SetText(Barra_Estado_Etiqueta_Hexadecimal.Text.Substring(13));
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Barra_Estado_Etiqueta_C_Sharp_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
                {
                    Clipboard.SetText(Barra_Estado_Etiqueta_C_Sharp.Text.Substring(4));
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Barra_Estado_Etiqueta_Código_Hash_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
                {
                    Clipboard.SetText(Barra_Estado_Etiqueta_Código_Hash.Text.Substring(11));
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.Thumbnails_and_average_color_generator;
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
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Generador_Miniaturas_Color_Medio);
                Program.Ejecutar_Ruta(Program.Ruta_Guardado_Imágenes_Generador_Miniaturas_Color_Medio, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Imagen_Original != null)
                {
                    int Píxeles = Ancho_Original * Alto_Original;
                    this.Text = Texto_Título + " - [Dimensions: " + Program.Traducir_Número(Ancho_Original) + " x " + Program.Traducir_Número(Alto_Original) + (Píxeles != 1 ? " pixels" : " pixel") + " (" + Program.Traducir_Número_Decimales_Redondear((double)Píxeles / 1000000d, 4) + " MP)]";
                    Ocupado = true;
                    if (!Variable_Mantener_Dimensiones)
                    {
                        Numérico_Ancho.Value = Ancho_Original;
                        Numérico_Alto.Value = Alto_Original;
                    }
                    Ocupado = false;
                    Picture.Image = (Bitmap)this.Imagen_Original.Clone();
                    Picture.Refresh();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Preconfiguración_Calidad_Predeterminada_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                ComboBox_Composición.SelectedIndex = 0;
                ComboBox_Interpolación.SelectedIndex = 0;
                ComboBox_Desplazamiento_Píxeles.SelectedIndex = 0;
                ComboBox_Suavizado.SelectedIndex = 0;
                Ocupado = false;
                Generar_Miniatura();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Preconfiguración_Alta_Calidad_Pixelada_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                ComboBox_Composición.SelectedIndex = 2;
                ComboBox_Interpolación.SelectedIndex = 5;
                ComboBox_Desplazamiento_Píxeles.SelectedIndex = 2;
                ComboBox_Suavizado.SelectedIndex = 2;
                Ocupado = false;
                Generar_Miniatura();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Preconfiguración_Alta_Calidad_Click(object sender, EventArgs e)
        {
            try
            {
                Ocupado = true;
                ComboBox_Composición.SelectedIndex = 2;
                ComboBox_Interpolación.SelectedIndex = 7;
                ComboBox_Desplazamiento_Píxeles.SelectedIndex = 2;
                ComboBox_Suavizado.SelectedIndex = 2;
                Ocupado = false;
                Generar_Miniatura();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Numérico_Ancho_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Generar_Miniatura();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Numérico_Alto_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Generar_Miniatura();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Numérico_Ancho_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle) Numérico_Ancho.Value = 16m;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Numérico_Alto_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle) Numérico_Alto.Value = 16m;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Relación_Aspecto_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle) ComboBox_Relación_Aspecto.SelectedIndex = 0;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Composición_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle) ComboBox_Composición.SelectedIndex = 2;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Interpolación_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle) ComboBox_Interpolación.SelectedIndex = 7;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Desplazamiento_Píxeles_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle) ComboBox_Desplazamiento_Píxeles.SelectedIndex = 2;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Suavizado_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle) ComboBox_Suavizado.SelectedIndex = 2;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Relación_Aspecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Generar_Miniatura();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Composición_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Generar_Miniatura();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Interpolación_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Generar_Miniatura();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Desplazamiento_Píxeles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Generar_Miniatura();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Suavizado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Generar_Miniatura();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Generar_Miniatura()
        {
            try
            {
                if (!Ocupado && Imagen_Original != null)
                {
                    try
                    {
                        Ocupado = true;
                        //int Ancho_Original = Imagen_Original.Width;
                        //int Alto_Original = Imagen_Original.Height;
                        Ancho_Miniatura = (int)Numérico_Ancho.Value;
                        Alto_Miniatura = (int)Numérico_Alto.Value;
                        int Ancho = Ancho_Miniatura;
                        int Alto = Alto_Miniatura;
                        if (ComboBox_Relación_Aspecto.SelectedIndex == 0) // Keep the original aspect ratio.
                        {
                            Ancho = (Alto_Miniatura * Ancho_Original) / Alto_Original;
                            Alto = (Ancho_Miniatura * Alto_Original) / Ancho_Original;
                            if (Ancho <= Ancho_Miniatura) Alto = Alto_Miniatura;
                            else if (Alto <= Alto_Miniatura) Ancho = Ancho_Miniatura;
                            Ancho_Miniatura = Ancho;
                            Alto_Miniatura = Alto;
                        }
                        Bitmap Imagen = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                        Graphics Pintar = Graphics.FromImage(Imagen);
                        Pintar.CompositingMode = CompositingMode.SourceCopy;

                        if (ComboBox_Composición.SelectedIndex == 0) Pintar.CompositingQuality = CompositingQuality.Default;
                        else if (ComboBox_Composición.SelectedIndex == 1) Pintar.CompositingQuality = CompositingQuality.HighSpeed;
                        else if (ComboBox_Composición.SelectedIndex == 2) Pintar.CompositingQuality = CompositingQuality.HighQuality;
                        else if (ComboBox_Composición.SelectedIndex == 3) Pintar.CompositingQuality = CompositingQuality.GammaCorrected;
                        else if (ComboBox_Composición.SelectedIndex == 4) Pintar.CompositingQuality = CompositingQuality.AssumeLinear;

                        if (ComboBox_Interpolación.SelectedIndex == 0) Pintar.InterpolationMode = InterpolationMode.Default;
                        else if (ComboBox_Interpolación.SelectedIndex == 1) Pintar.InterpolationMode = InterpolationMode.Low;
                        else if (ComboBox_Interpolación.SelectedIndex == 2) Pintar.InterpolationMode = InterpolationMode.High;
                        else if (ComboBox_Interpolación.SelectedIndex == 3) Pintar.InterpolationMode = InterpolationMode.Bilinear;
                        else if (ComboBox_Interpolación.SelectedIndex == 4) Pintar.InterpolationMode = InterpolationMode.Bicubic;
                        else if (ComboBox_Interpolación.SelectedIndex == 5) Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                        else if (ComboBox_Interpolación.SelectedIndex == 6) Pintar.InterpolationMode = InterpolationMode.HighQualityBilinear;
                        else if (ComboBox_Interpolación.SelectedIndex == 7) Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        if (ComboBox_Desplazamiento_Píxeles.SelectedIndex == 0) Pintar.PixelOffsetMode = PixelOffsetMode.Default;
                        else if (ComboBox_Desplazamiento_Píxeles.SelectedIndex == 1) Pintar.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                        else if (ComboBox_Desplazamiento_Píxeles.SelectedIndex == 2) Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        else if (ComboBox_Desplazamiento_Píxeles.SelectedIndex == 3) Pintar.PixelOffsetMode = PixelOffsetMode.None;
                        else if (ComboBox_Desplazamiento_Píxeles.SelectedIndex == 4) Pintar.PixelOffsetMode = PixelOffsetMode.Half;

                        if (ComboBox_Suavizado.SelectedIndex == 0) Pintar.SmoothingMode = SmoothingMode.Default;
                        else if (ComboBox_Suavizado.SelectedIndex == 1) Pintar.SmoothingMode = SmoothingMode.HighSpeed;
                        else if (ComboBox_Suavizado.SelectedIndex == 2) Pintar.SmoothingMode = SmoothingMode.HighQuality;
                        else if (ComboBox_Suavizado.SelectedIndex == 3) Pintar.SmoothingMode = SmoothingMode.None;
                        else if (ComboBox_Suavizado.SelectedIndex == 4) Pintar.SmoothingMode = SmoothingMode.AntiAlias;

                        Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho_Original, Alto_Original), GraphicsUnit.Pixel);
                        Pintar.Dispose();
                        Pintar = null;
                        this.Imagen_Miniatura = (Bitmap)Imagen.Clone();
                        int Píxeles = Ancho_Miniatura * Alto_Miniatura;
                        this.Text = Texto_Título + " - [Dimensions: " + Program.Traducir_Número(Ancho_Miniatura) + " x " + Program.Traducir_Número(Alto_Miniatura) + (Píxeles != 1 ? " pixels" : " pixel") + " (" + Program.Traducir_Número_Decimales_Redondear((double)Píxeles / 1000000d, 4) + " MP)]";
                        Picture.Image = Imagen;
                        Picture.Refresh();
                        Ocupado = false;
                    }
                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                    finally { Ocupado = false; }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Mantener_Dimensiones_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Mantener_Dimensiones = Menú_Contextual_Mantener_Dimensiones.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Exportar_2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.Image != null && Program.Exportar_Imagen_Indizada(Picture.Image as Bitmap, 2, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)))
                {
                    SystemSounds.Asterisk.Play();
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Exportar_16_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.Image != null && Program.Exportar_Imagen_Indizada(Picture.Image as Bitmap, 16, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)))
                {
                    SystemSounds.Asterisk.Play();
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Exportar_256_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.Image != null && Program.Exportar_Imagen_Indizada(Picture.Image as Bitmap, 256, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)))
                {
                    SystemSounds.Asterisk.Play();
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Generar_Personaje_Skin_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.Image != null)
                {
                    bool Variable_Dibujar_Pelo = true;
                    bool Variable_Dibujar_Chaqueta = true;
                    bool Variable_Dibujar_Brazos_Chaqueta = true;
                    bool Variable_Dibujar_Pantalones = true;
                    // Imagen = Program.Obtener_Imagen_Miniatura(Picture.Image.Clone() as Image, 256, 256, true, false, CheckState.Checked);
                    Bitmap Imagen = Program.Obtener_Imagen_Skin_2D(Picture.Image.Clone() as Bitmap, Variable_Dibujar_Pelo, Variable_Dibujar_Chaqueta, Variable_Dibujar_Brazos_Chaqueta, Variable_Dibujar_Pantalones);
                    Program.Guardar_Imagen_Temporal(Imagen);
                    SystemSounds.Asterisk.Play();
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
