using Minecraft_Tools.Properties;
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
    public partial class Ventana_Visor_Formato_Chunks_1_13 : Form
    {
        public Ventana_Visor_Formato_Chunks_1_13()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Minecraft 1.13+ Chunk Format Information Viewer by Jupisoft for " + Program.Texto_Usuario;
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

        private void Ventana_Visor_Formato_Chunks_1_13_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título + " - [Zoom: " + Program.Traducir_Número(Variable_Zoom) + "x]"; ;
                this.WindowState = FormWindowState.Maximized;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
                float Zoom = Variable_Zoom;
                string Texto_Información = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang3082{\\fonttbl{\\f0\\fnil\\fcharset0 " + Barra_Estado_Etiqueta_Sugerencia.Font.Name + ";}{\\f1\\fnil\\fcharset0 Calibri;}}\r\n{\\*\\generator Riched20 6.3.9600}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs" + (10 * 2).ToString() + " "; // Open RTF.
                Texto_Información +=
                "\\ul \\b [OVERVIEW OF THIS GUIDE]\\b0 \\ulnone \\par\\par\r\n" +
                "\\b - \"What is this\":\\b0  this is a complete guide that explains in detail how the Minecraft 1.13+ new chunk format works. It is mainly designed thinking of anyone that knows how to program, and wants to understand and implement full support for the Minecraft 1.13+ versions. This would mean at least being able to decode and fully read any region file with it's chunks and sections and being able to tell which block type is present at any XYZ coordinate, including all of it's possible block properties like orientation, power state, etc.\\par\\par\r\n" +

                "\\par\\ul \\b [REGION FILES AND IT'S CONTENTS]\\b0 \\ulnone \\par\\par\r\n" +
                "\\b - \"Region file\":\\b0  they are the files located inside any Minecraft save game folder. They are located in a subfolder called \"region\" and they are also present for the Nether and The End dimensions (and also in any other dimension when playing modded Minecraft). They have the extension \".mca\" (\"Minecraft Anvil\", although some time ago they had the extension \".mcr\" or \"Minecraft Region\" with another file format, but the Minecraft wiki should have more information about this). Each of these files has inside 1.024 chunks, divided in a 32 x 32 grid.\\par\\par\r\n" +
                "\\b - \"Chunk\":\\b0  each chunk has 65.536 blocks, divided in a 16 x 256 x 16 grid (XYZ). But also each 16 x 16 x 16 part of a chunk is called a section, multiple of 4 KB in size, and usually compressed to save space with several methods. Each chunk has some other NBT metadata like the biome for each XZ coordinate of that chunk, the lighting of it's blocks, height maps and other useful informations about the chunk that will be ignored in this guide, since it's objective is to read only the block types information of each section and it's possible block properties.\\par\\par\r\n" +
                "\\b - \"Section\":\\b0  one part of the 16 any chunk has. It has 16 x 16 x 16 blocks in size (16^3 or 4.096 blocks in total). This means that reading a block inside a chunk at 8, 15, 8 (XYZ) and then reading another at 8, 16, 8 (XYZ) inside the same chunk will need to load and decode 2 of it's multiple sections (if they are present). Since a Y value from 0 to 15 will be the lower section (bedrock), and from 16 to 31 will be the second one, there will be a lot more work when programming than one might expect at first, because when playing Minecraft those blocks will seem to be \"together\", but they will be saved in different parts of the same region file, each one possibly compressed and separated from the other. Although this will help a lot to make the new chunk format more efficient as we will see below.\\par\\par\r\n" +

                "\\par\\ul \\b [OVERVIEW OF THE NEW CHUNK FORMAT]\\b0 \\ulnone \\par\\par\r\n" +
                "\\b - \"How an indexed BMP image works\":\\b0  those familiar with indexed images will know that to save space in disk, some time ago most images were limited to 2, 16 or 256 variable colors. The image format GIF still saves it's frames with a maximum of 8 bits or 256 colors. While most images are usually stored as JPEG or PNG. But let's now center on the old BMP format, which stored the images uncompressed. If a BMP image had 24 bits (or up to 16.777.216 colors), to store a screen capture (for example) with a size of 1.024 x 768 it will need 2,25 MB, but it the quantity of colors was reduced to 256 it will only need 0,75 MB. Since 1 byte (or 8 bits) can have 256 different values, then storing first a \"color palette\" with 256 colors and then 1.024 x 768 bytes where each of these bytes will be an index of the palette will save a lot of space. For this to work the colors in the palette will be stored in 24 bits, but will only be 256 of them. So for each pixel in the image there will be a unique byte, representing an index of that palette. So if a pixel has an index of \"7\" (for example), then you'll know that you need to read the 8th color of the palette (since the first color is the number zero and not one in programmation). And this full description is exactly what the new Minecraft chunk format does. In the previous 1.12.2- versions it had a byte (from 0 to 255) with a block ID and another 4 bits (from 0 to 15) with block extra properties. But almost all of the possible IDs and properties were already in use, so Mojang couldn't add new block types without redoing the full chunk saving system., and basically that's what happened in Minecraft 1.13+, they added support for almost infinite block types with this new format, which should be very useful in a near future when playing modded Minecraft. But sadly it broke all of the applications that could read or write Minecraft worlds before, leaving them obsolete at least for a long while.\\par\\par\r\n" +

                "\\par\\ul \\b [NBT VALUES OF THE NEW CHUNK FORMAT]\\b0 \\ulnone \\par\\par\r\n" +
                "\\b - \"How to know if a chunk is in 1.12.2- or 1.13+ format\":\\b0  each chunk in the 1.13+ format now has a NBT string value called \"Status\", so if this value is present in theory the chunk should be in 1.13+ format, and if it's not present it should be in 1.12.2- format. This application uses this trick to quickly identify the version to decode.\\par\\par\r\n" +
                "\\b - \"BlockStates\":\\b0  each of the previously mentioned sections of any chunk should now have a NBT property called \"BlockStates\", which is an array of long values (several numbers of 64 bits). Those are the indexes of the \"block palette\" to save space and avoid repeating each time the full block name, which now is a full string instead of an ID. By the looks of it, this array of long values shouldn't be interpreted as a long array, but like groups (array) of numbers with variable bits (not always 64 bits). To understand this with more detail let's imagine we have a block palette with only 2 block types (for example bedrock and air), then a full section made of 4.096 blocks should in theory have a single bit per block or 512 bytes, but in reality Minecraft seems to always split or round up the existing values based on fixed increments. This means that the minimum number of values in the long array should be 256 if the number of unique blocks in that section is less or equal than 16. But the equation to know how many bits should be assigned to each index of the block palette is this: since a section can only have 4.096 blocks, and the long array has values of 64 bits, then divide 4.096 between 64 bits and you'll get the number of bits per index of the palette. If for example you have 320 long values in the array, then 320 / 64 = 5 bits, which means that the current palette should have between 17 and 32 unique block types. So this is the full conversion to avoid future confusions: an array of 64 long values = 1 bit per index, 128 longs = 2 bits, 192 longs = 3 bits, 256 longs = 4 bits, 320 longs = 5 bits, 384 longs = 6 bits, 448 longs = 7 bits, 512 longs = 8 bits, 576 longs = 9 bits, 640 longs = 10 bits, 704 longs = 11 bits and 768 longs = 12 bits. Please note that with 12 bits one can have a maximum of 4.096 unique values, the number of blocks of each section. And although this full calculations are useful, remember that you can also directly divide the number of longs in the array by 64 to get number of bits per index. There is also a several problem with this format and to see this let's imagine we have 5 bits per palette index, so splitting numbers of 64 bits between groups of 5 bits isn't exact, which means that the whole array should be loaded as bits if possible, and then split it in groups of 5 bits for example, so each of this numbers of 5 bits will be a number without sign between 0 and 31 (in this example), and this basically means using slow classes like BitArray in C#, but reading directly all as an array of bytes and then for each byte check if it has each bit on or off and add a quantity based on the part of the 5 bits number (in this example), we're reading should be a lot faster and should avoid using the BitArray class. In a near future this application will have this faster method implemented.\\par\\par\r\n" +
                "\\b - \"Palette\":\\b0  a group of block types with variable length. You should first load in your own matrix or dictionary all this blocks, and when reading the groups of bits of the previous array, you could simply access to it by a numeric index, which should be a lot faster than reading the NBT data each time. The blocks in the palette have subvalues like \"Name\", which contains strings like \"minecraft:air\", the first word should be the mod name or \"minecraft\" if it's vanilla, and the second the block name. Some blocks will also have a subvalue called \"Properties\", which might describe things like the block orientation, age, power, etc. This means that the palette might have several blocks with the same name but different properties values, so always check if that value exists and what contents has.\\par\\par\r\n" +

                "\\par\\ul \\b [PARTIAL TUTORIAL]\\b0 \\ulnone \\par\\par\r\n" +
                "\\b - \"Ignored steps\":\\b0  for the sake of simplicity several steps were omitted from this guide, and to be specific, all the common steps when loading and decoding any region file like it was done in Minecraft 1.12.2- versions will be ignored. But at least a brief explanation on how to proceed will be descripted in the next lines to anyone unfamiliar with this procedure. First a region file, included in any Minecraft world should be opened (they are located inside the save game folder of your world, in the subfolder called \"region\", although they are also present in the Nether and The End dimensions). Once the region file is loaded the classes present inside this application should be called, the ones inside the folder called \"Substrate_Jupisoft\" support the Minecraft 1.13+ new chunk format, since they are able to read the new NBT tag with ID 12 for long arrays (you can check the Minecraft wiki about the NBT format if you need more info). Finally use the function \"Cargar_Región(...)\" from the main \"Minecraft.cs\" class of this application to decode, uncompress and read the chunks, in 1.12.2- or 1.13+ format.\\par\\par\r\n" +

                "\\par\\ul \\b [CONCLUSIONS]\\b0 \\ulnone \\par\\par\r\n" +
                "\\b - \"Extra help\":\\b0  if you still have any question about any part of the new chunk format you can send me an e-mail at Jupitermauro@gmail.com and I'll be more than happy to try to help you with your problem, implementation, etc. But please forgive me because I don't have access to internet at home so I can only connect once a week from another town, so if I take a couple of days to answer back should be because of that, sorry about that. Thanks a lot for your cooperation and have a nice day!\\par\r\n" +

                "\\pard\\sa200\\sl276\\slmult1\\f1\\fs22\\lang10\\par\r\n}"; // Close RTF.
                RichTextBox_Información.Rtf = Texto_Información;
                RichTextBox_Información.ZoomFactor = Zoom != 1.5f ? 1.5f : 2.5f;
                RichTextBox_Información.ZoomFactor = Zoom;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Formato_Chunks_1_13_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Formato_Chunks_1_13_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Formato_Chunks_1_13_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Formato_Chunks_1_13_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Visor_Formato_Chunks_1_13_DragDrop(object sender, DragEventArgs e)
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

        private void Ventana_Visor_Formato_Chunks_1_13_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Normal) this.WindowState = FormWindowState.Maximized;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Formato_Chunks_1_13_KeyDown(object sender, KeyEventArgs e)
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
                try
                {
                    if (Variable_Zoom != RichTextBox_Información.ZoomFactor)
                    {
                        Variable_Zoom = RichTextBox_Información.ZoomFactor;
                        Registro_Guardar_Opciones();
                        this.Text = Texto_Título + " - [Zoom: " + Program.Traducir_Número(Variable_Zoom) + "x]";
                    }
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
    }
}
