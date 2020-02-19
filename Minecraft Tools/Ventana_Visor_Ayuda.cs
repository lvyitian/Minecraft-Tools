using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Visor_Ayuda : Form
    {
        public Ventana_Visor_Ayuda()
        {
            InitializeComponent();
        }
        
        internal enum Ayudas : int
        {
            Main_window = 0,

            Realistic_world_viewer_in_2D,
            Slime_chunks_finder,
            Animated_3D_skin_viewer,
            World_seeds_infinite_calculator,

            Pixel_art_generator_with_world_exporter,
            Painted_structures_exporter,
            Custom_structure_generator,
            Thumbnails_and_average_color_generator,

            NBT_viewer,
            Finder_of_differences_between_JAR_versions,
            Redstone_designer,
            Structure_generator_through_commands,

            Real_time_Minecraft_day_night_clock,
            Block_information_viewer,
            Backups_manager,
            Automatic_skin_downloader,

            Paintings_viewer,
            Note_blocks_tuner,
            Banner_and_shield_designer,
            Villager_tradings_viewer,

            Entities_information_viewer,
            Resource_files_structure_rebuilder,
            Minecraft_internal_structures_exporter,
            Enchantment_names_viewer,

            Start_a_random_tool_every_time,

            Help_viewer,
            Internal_debugger,
            Change_user_name,
            About,

            Hermitcraft_members_information,

            Secrets,

            Total // Don't use
        }

        internal readonly string Texto_Título = "Help Viewer by Jupisoft for " + Program.Texto_Usuario;
        internal Ayudas Ayuda = Ayudas.Main_window;
        internal float Variable_Zoom = 1f;
        internal Stopwatch Cronómetro_Memoria = new Stopwatch(); // Turn the text red when over 4 GB
        internal bool Variable_Siempre_Visible = false;

        private void Ventana_Visor_Ayuda_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título + " - [Help files: " + Program.Traducir_Número((int)Ayudas.Total) + ", Zoom: " + Program.Traducir_Número(Variable_Zoom) + "x]";
                this.WindowState = FormWindowState.Maximized;
                string[] Matriz_Nombres = Enum.GetNames(typeof(Ayudas));
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Nombres.Length - 1; Índice++) ComboBox_Ayuda.Items.Add(" " + Matriz_Nombres[Índice].Substring(0, 1).ToUpperInvariant() + Matriz_Nombres[Índice].Substring(1).Replace('_', ' '));
                    Matriz_Nombres = null;
                }
                //RichTextBox_Ayuda.Font = new Font("Courier New", 11f, FontStyle.Regular);
                //RichTextBox_Ayuda.Font = Barra_Estado_Etiqueta_Memoria.Font;
                //RichTextBox_Ayuda.Font = new Font(Barra_Estado_Etiqueta_Memoria.Font.Name, 31f, FontStyle.Regular);
                Registro_Cargar_Opciones();
                if (Ayuda >= 0 && (int)Ayuda < ComboBox_Ayuda.Items.Count) ComboBox_Ayuda.SelectedIndex = (int)Ayuda;
                else if (ComboBox_Ayuda.Items.Count > 0) ComboBox_Ayuda.SelectedIndex = 0;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Visor_Ayuda_Shown(object sender, EventArgs e)
        {
            try
            {
                Temporizador_Principal.Start();
                this.Activate();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Visor_Ayuda_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
                Cronómetro_Memoria.Reset();
                Cronómetro_Memoria = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Visor_Ayuda_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Visor_Ayuda_KeyDown(object sender, KeyEventArgs e)
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

        /// <summary>
        /// [RTF format info]:
        /// 
        /// "\b ": bold start.
        /// "\b0 ": bold end.
        /// 
        /// "\i ": italic start.
        /// "\i0 ": italic end.
        /// 
        /// "\ul ": underline start.
        /// "\ulnone ": underline end.
        /// 
        /// "\strike ": strike start.
        /// "\strike0 ": strike end.
        /// 
        /// "\sub ": subtext start.
        /// "\nosupersub ": subtext end.
        /// 
        /// "\super ": supertext start.
        /// "\nosupersub ": supertext end.
        /// 
        /// "\highlight1 ": highlight start.
        /// "\highlight0 ": highlight end.
        /// 
        /// "\cf1 ": colored font.
        /// "\cf0 ": default colored font.
        /// 
        /// "\pard\qc ": center text.
        /// "\pard ": left text.
        /// "\pard\qr ": right text.
        /// "\pard\qj ": justified text.
        /// 
        /// "\fs18 ": font size 9.
        /// "\fs20 ": font size 10.
        /// "\fs22 ": font size 11.
        /// "\fs24 ": font size 12.
        /// "\fs160 ": font size 80.
        /// Note: the font size seems to be the double on the source code than the actual value.
        /// </summary>
        private void ComboBox_Ayuda_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Ayuda.SelectedIndex > -1)
                {
                    float Zoom = Variable_Zoom;
                    Ayudas Ayuda = (Ayudas)ComboBox_Ayuda.SelectedIndex;
                    string Texto_Ayuda = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang3082{\\fonttbl{\\f0\\fnil\\fcharset0 " + Barra_Estado_Etiqueta_Sugerencia.Font.Name + ";}{\\f1\\fnil\\fcharset0 Calibri;}}\r\n{\\*\\generator Riched20 6.3.9600}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs" + (10 * 2).ToString() + " ";
                    if (Ayuda == Ayudas.Main_window)
                    {
                        Texto_Ayuda +=
                        "\\ul \\b [File menu]\\b0 \\ulnone \\par\r\n" +
                        "\\b - \"Delete all the saved options from the registry...\":\\b0  deletes all the application settings stored in the Windows registry, useful for making a full \"uninstall\".\\par\r\n" +
                        "\\b - \"Exit\":\\b0  closes the application.\\par\r\n" +

                        "\\par\\ul \\b [View menu]\\b0 \\ulnone \\par\r\n" +
                        "\\b - \"Always on top\":\\b0  puts the windows of the application on top of other windows of the system.\\par\r\n" +

                        "\\par\\ul \\b [Tools menu]\\b0 \\ulnone \\par\r\n" +
                        "\\b - \"Open the default tool now...\":\\b0  starts the default tool, if it was some selected.\\par\r\n" +
                        "\\b - \"Default tool every time the program loads\":\\b0  let's you select the tool that will open every time the program starts.\\par\r\n" +
                        "\\b - \"Realistic world viewer in 2D...\":\\b0  tool that loads a Minecraft world ranging 1.12.2- to 1.13+, reading all of it's possible dimensions (it even supports modpacks worlds) and then draws several useful maps to view those worlds and dimensions in lots of new ways.\\par\r\n" +
                        "\\b - \"Slime chunks finder...\":\\b0  tool that displays where are located in the world the chunks (16 x 16 blocks) where slimes will be able to spawn underground, and for that you can input a text seed or a numeric one directly.\\par\r\n" +
                        "\\b - \"Animated 3D skin viewer...\":\\b0  tool that draws any player skin as an animated 3D model, allowing to fully select it's movement, and it also comes with several exclusive filters to enhance the viewing of the skin.\\par\r\n" +
                        "\\b - \"World seeds infinite calculator...\":\\b0  tool that calculates a world numeric seed based on a text seed, but it also supports hundreds of bases and literally infinite numbers.\\par\r\n" +
                        "\\b - \"Finder of differences between JAR versions...\":\\b0  tool that tells you what has changed between 2 versions of the .jar files of Minecraft, very useful when a new snapshot is released.\\par\r\n" +
                        "\\b - \"Pixel art generator with world exporter...\":\\b0  tool that loads any image and converts it to Minecraft blocks from a selected palette, it supports horizontal and vertical new Minecraft world exporting.\\par\r\n" +
                        "\\b - \"Custom structure generator...\":\\b0  tool that exports new Minecraft worlds containing customized massive structures like labyrinths, pyramids, spheres, etc, including the default Minecraft structures but allowing to change it's default block types.\\par\r\n" +
                        "\\b - \"Average color from any image...\":\\b0  tool that displays the average ARGB colors form any image, useful for creating tools like a pixel art generator, where each pixel of any image must be converted to an average color from any Minecraft texture.\\par\r\n" +
                        "\\b - \"Redstone designer (soon)...\":\\b0  tool that allows the design of redstone circuits very easily, with an option to export them as new Minecraft worlds.\\par\r\n" +
                        "\\b - \"Structure generator through commands (soon)...\":\\b0  tool that generates massive structures in real time by writing commands inside the game, but without any mods. Note: this tool might be very dangerous if the game can't keep up the text inputs, and this might result in a world being destroyed forever, so please before using this tool make backup copies of the worlds you're going to load while usign the tool.\\par\r\n" +
                        "\\b - \"Other cool Minecraft tools made by other awesome people\":\\b0  displays a list of other interesting and free Minecraft utilities, some of which are part of this application, while others have similar tools to the ones contained in this program.\\par\r\n" +
                        "\\b - \"Open the Twitch (Curse) default modpacks folder...\":\\b0  opens the default modpacks folder from Twitch (Curse).\\par\r\n" +
                        "\\b - \"Open the Minecraft default save folder...\":\\b0  opens the default Minecraft save folder.\\par\r\n" +

                        "\\par\\ul \\b [Help menu]\\b0 \\ulnone \\par\r\n" +
                        "\\b - \"Help viewer...\":\\b0  opens the tool you're using right now to read this text.\\par\r\n" +
                        "\\b - \"Exception debugger...\":\\b0  opens a tool that will show you any exception that happened while the application was running even from long ago, and will sort them by it's date.\\par\r\n" +
                        "\\b - \"Website...\":\\b0  navigates to the Jupisoft's website, currently only containing free music albums composed by Júpiter Mauro.\\par\r\n" +
                        "\\b - \"Send an e-mail...\":\\b0  sends an e-mail to Jupisoft, feel free to send any bug you've found or tell any suggestion you have.\\par\r\n" +
                        "\\b - \"Donate...\":\\b0  navigates to the donation site, to give any quantity you want to Jupisoft, so it keeps improving tools like this and developing new ones.\\par\r\n" +
                        "\\b - \"About...\":\\b0  opens the about window, that contains more detailed information about this application and some other interesting features.\\par\r\n" +

                        "\\par\\ul \\b [Status bar]\\b0 \\ulnone \\par\r\n" +
                        "\\b - \"Exceptions\":\\b0  if this label is visible, that means at least one exception has been registered in the application. Click on that label to open the exception debugger and see what happened.\\par\r\n" +
                        "\\b - \"Memory\":\\b0  this label displays the current memory use by the whole application. And by just measuring that memory use, it might decrease or increase a bit this value.\\par\r\n" +
                        "\\b - \"Tip\":\\b0  this label is designed to kindly welcome the user to the application and to suggest the start of one of it's multiple tools.\\par\r\n" +

                        "\\par\\ul \\b [Controls]\\b0 \\ulnone \\par\r\n" +
                        "\\b - \"Drag and drop\":\\b0  this window doesn't support any drag and drop operations.\\par\r\n" +
                        "\\b - \"Escape or Delete\":\\b0  pressing any of those keys will close the window and quit the program.";
                    }
                    else if (Ayuda == Ayudas.Realistic_world_viewer_in_2D)
                    {
                        Texto_Ayuda +=
                        "\\ul \\b [Main toolbar]\\b0 \\ulnone \\par\r\n" +
                        "\\b - \"Map\":\\b0  allows you to select a map type to view the loaded dimension in different ways.\\par\r\n" +
                        "\\b - \"In 3D\":\\b0  allows the use different brightness on the current map depending on the Y value of each block, representing lower blocks with darker areas and higher blocks with brighter areas.\\par\r\n" +
                        "\\b - \"Dimension\":\\b0  allows you to to choose between the available dimensions in the current world.\\par\r\n" +
                        "\\b - \"Zoom\":\\b0  allows to zoom in or out the map to be able to see the blocks with more or less details.\\par\r\n" +
                        "\\b - \"Block\":\\b0  allows you to choose the block type to filter when the current map type is set to \"Search block\".\\par\r\n" +
                        "\\b - \"X\":\\b0  allows you to change the current X (horizontal, from west to east) coordinate.\\par\r\n" +
                        "\\b - \"Y\":\\b0  allows you to change the current Y (vertical, from bottom to top) coordinate.\\par\r\n" +
                        "\\b - \"Z\":\\b0  allows you to change the current Z (depth, from north to south) coordinate.\\par\r\n" +

                        "\\par\\ul \\b [Status bar]\\b0 \\ulnone \\par\r\n" +
                        "\\b - \"Memory\":\\b0  displays the quantity of memory the application is currently using.\\par\r\n" +
                        "\\b - \"Minecraft\":\\b0  displays the Minecraft version detected after loading any dimension from a world.\\par\r\n" +
                        "\\b - \"Regions\":\\b0  displays the number of regions available for the current dimension.\\par\r\n" +
                        "\\b - \"Min. XZ\":\\b0  displays the minimum X and Z (northwest) coordinates that exists within the available regions.\\par\r\n" +
                        "\\b - \"Max. XZ\":\\b0  displays the maximum X and Z (southeast) coordinates that exists within the available regions.\\par\r\n" +
                        "\\b - \"Blocks\":\\b0  displays the size in blocks of the current map.\\par\r\n" +
                        "\\b - \"Visible\":\\b0  displays the number of visible blocks in the current map.\\par\r\n" +

                        "\\par\\ul \\b [Context menu]\\b0 \\ulnone \\par\r\n" +
                        "\\b - \"Help viewer...\":\\b0  opens the tool you're using right now to read this text.\\par\r\n" +
                        "\\b - \"About...\":\\b0  opens the about window, that contains more detailed information about this application and some other interesting features.\\par\r\n" +
                        "\\b - \"Exception debugger...\":\\b0  opens the exception debugger, a tool that keeps a registry of all the exceptions that have occured in the program even from long ago.\\par\r\n" +
                        "\\b - \"Open the default save folder for the maps...\":\\b0  opens the default folder used to save the maps, located inside the My Pictures folder.\\par\r\n" +
                        "\\b - \"Refresh and redraw the full map\":\\b0  forces a redraw of the full map.\\par\r\n" +
                        "\\b - \"Clear the current dimension cache after loading any region\":\\b0  if checked, before drawing the map all the regions from the current dimension will deleted from the cache to recover memory.\\par\r\n" +
                        "\\b - \"Clear the other dimensions cache when loading a new one\":\\b0  if checked, before drawing the map all the regions from the other dimensions will deleted from the cache to recover memory.\\par\r\n" +
                        "\\b - \"Clear from all the caches the regions outside of the screen\":\\b0  if checked, before drawing the map all the regions from any dimension that aren't going to be drawed right now will deleted from the cache to recover memory.\\par\r\n" +
                        "\\b - \"Randomize the region and chunk draw order\":\\b0  if checked, before drawing the map the order of the current regions and chunks will be randomized, creating a nicer pattern when drawing.\\par\r\n" +
                        "\\b - \"Show the randomized rainbow chunk grid\":\\b0  if checked, a semi-transparent grid made of randomized and fully saturated colors will be drawn around every chunk border of the current map.\\par\r\n" +
                        "\\b - \"Full screen mode\":\\b0  if checked, the window will change to full screen mode, allowing for more screen space to be used.\\par\r\n" +
                        "\\b - \"Select the background color...\":\\b0  opens a color selector dialog to be able to change the background color of the map.\\par\r\n" +
                        "\\b - \"Clear now the whole cache to recover more memory\":\\b0  forces a full cache deletion in all the loaded dimensions to recover memory, and this means that the next time the map is drawn all the regions will have to be loaded again from the hard drive, thus slowing the drawing of the map at least the next time.\\par\r\n" +
                        "\\b - \"Save the current map as a PNG image...\":\\b0  exports the current map as a .png image inside the My Pictures folder.\\par\r\n" +
                        "\\b - \"Copy the current map to the clipboard\":\\b0  copies the current map to the clipboard to be able to paste it into any other program.\\par\r\n" +
                        "\\b - \"Copy the whole form (useful in fullscreen)\":\\b0  copies the whole form to the clipboard to be able to paste it into any other program.\\par\r\n" +
                        "\\b - \"Restore all this settings to it's default values...\":\\b0  deletes all the settings of the current tool stored in the Windows registry.\\par\r\n" +

                        "\\par\\ul \\b [Controls]\\b0 \\ulnone \\par\r\n" +
                        "\\b - \"Drag and drop\":\\b0  to load an existing Minecraft world drag it's folder (or any file inside it) and drop it onto the window.\\par\r\n" +
                        "\\b - \"Escape\":\\b0  pressing that key will close the window and will return to the main window.\\par\r\n" +
                        "\\b - \"Mouse middle click\":\\b0  pressing that button will have different effects based on the control that is clicked on, like copy to the clipboard some image or text, reset the control value to it's default one, randomize that value and other interesting effects.";
                    }
                    else if (Ayuda == Ayudas.Slime_chunks_finder)
                    {
                        Texto_Ayuda +=
                        "\\ul \\b [Main toolbar]\\b0 \\ulnone \\par\r\n" +
                        "\\b - \"Seed\":\\b0  allows you to input a numeric seed or a text seed that will be converted to a numeric one, and based on that seed the map will show you (the green rectangles) where in a Minecraft world with that seed slimes will spawn below Y = 40. Note: Minecraft usually turns a seed 0 to 48, so be aware of that.\\par\r\n" +
                        "\\b - \"Zoom\":\\b0  allows you to change the zoom level of the map to be able to see it better or to see more terrain at once.\\par\r\n" +
                        "\\b - \"Block X\":\\b0  allows you to change the block X coordinate to move around the world and find slime chunks at a specific position.\\par\r\n" +
                        "\\b - \"Block Z\":\\b0  allows you to change the block Z coordinate to move around the world and find slime chunks at a specific position.\\par\r\n" +
                        "\\b - \"Chunk X\":\\b0  allows you to change the chunk X coordinate to move around the world and find slime chunks at a specific position.\\par\r\n" +
                        "\\b - \"Chunk Z\":\\b0  allows you to change the chunk Z coordinate to move around the world and find slime chunks at a specific position.\\par\r\n" +
                        "\\b - \"Region X\":\\b0  allows you to change the region X coordinate to move around the world and find slime chunks at a specific position.\\par\r\n" +
                        "\\b - \"Region Z\":\\b0  allows you to change the region Z coordinate to move around the world and find slime chunks at a specific position.\\par\r\n" +

                        "\\par\\ul \\b [Status bar]\\b0 \\ulnone \\par\r\n" +
                        "\\b - \"Memory\":\\b0  displays the quantity of memory the application is currently using.\\par\r\n" +
                        "\\b - \"Dimensions\":\\b0  displays the dimensions of the current map measured in chunks (16 x 16 blocks).\\par\r\n" +
                        "\\b - \"Visible chunks\":\\b0  displays how many chunks are visible in the whole map.\\par\r\n" +
                        "\\b - \"Slime chunks\":\\b0  displays how many of the visible chunks are valid for spawning slimes below Y = 40.\\par\r\n" +

                        "\\par\\ul \\b [Context menu]\\b0 \\ulnone \\par\r\n" +
                        "\\b - \"Help viewer...\":\\b0  opens the tool you're using right now to read this text.\\par\r\n" +
                        "\\b - \"About...\":\\b0  opens the about window, that contains more detailed information about this application and some other interesting features.\\par\r\n" +
                        "\\b - \"Exception debugger...\":\\b0  opens the exception debugger, a tool that keeps a registry of all the exceptions that have occured in the program even from long ago.\\par\r\n" +
                        "\\b - \"Open the default save folder for the maps...\":\\b0  opens the default folder used to save the maps, located inside the My Pictures folder.\\par\r\n" +
                        "\\b - \"Refresh\":\\b0  forces a redraw of the full map.\\par\r\n" +
                        "\\b - \"Show the rulers\":\\b0  if checked, the slime chunk map will display on it's borders some rulers for a better reading of the map.\\par\r\n" +
                        "\\b - \"Copy\":\\b0  copies the current map to the clipboard to be able to paste it into any other program.\\par\r\n" +
                        "\\b - \"Save as a PNG image\":\\b0  exports the current map as a .png image inside the My Pictures folder.\\par\r\n" +
                        "\\b - \"Restore all this settings to it's default values...\":\\b0  deletes all the settings of the current tool stored in the Windows registry.\\par\r\n" +

                        "\\par\\ul \\b [Controls]\\b0 \\ulnone \\par\r\n" +
                        "\\b - \"Drag and drop\":\\b0  to load an existing Minecraft world drag it's folder (or any file inside it) and drop it onto the window.\\par\r\n" +
                        "\\b - \"Escape\":\\b0  pressing that key will close the window and will return to the main window.\\par\r\n" +
                        "\\b - \"Mouse middle click\":\\b0  pressing that button will have different effects based on the control that is clicked on, like copy to the clipboard some image or text, reset the control value to it's default one, randomize that value and other interesting effects.";
                    }
                    else if (Ayuda == Ayudas.Animated_3D_skin_viewer)
                    {
                        Texto_Ayuda += "\\ul \\b [Coming soon...]\\b0 \\ulnone \\par\r\n";
                    }
                    else if (Ayuda == Ayudas.World_seeds_infinite_calculator)
                    {
                        Texto_Ayuda += "\\ul \\b [Coming soon...]\\b0 \\ulnone \\par\r\n";
                    }
                    else if (Ayuda == Ayudas.Finder_of_differences_between_JAR_versions)
                    {
                        Texto_Ayuda += "\\ul \\b [Coming soon...]\\b0 \\ulnone \\par\r\n";
                    }
                    else if (Ayuda == Ayudas.Pixel_art_generator_with_world_exporter)
                    {
                        Texto_Ayuda += "\\ul \\b [Coming soon...]\\b0 \\ulnone \\par\r\n";
                    }
                    else if (Ayuda == Ayudas.Custom_structure_generator)
                    {
                        Texto_Ayuda += "\\ul \\b [Coming soon...]\\b0 \\ulnone \\par\r\n";
                    }
                    else if (Ayuda == Ayudas.Thumbnails_and_average_color_generator)
                    {
                        Texto_Ayuda += "\\ul \\b [Coming soon...]\\b0 \\ulnone \\par\r\n";
                    }
                    else if (Ayuda == Ayudas.NBT_viewer)
                    {
                        Texto_Ayuda += "\\ul \\b [Coming soon...]\\b0 \\ulnone \\par\r\n";
                    }
                    else if (Ayuda == Ayudas.Redstone_designer)
                    {
                        Texto_Ayuda += "\\ul \\b [Coming soon...]\\b0 \\ulnone \\par\r\n";
                    }
                    else if (Ayuda == Ayudas.Structure_generator_through_commands)
                    {
                        Texto_Ayuda += "\\ul \\b [Coming soon...]\\b0 \\ulnone \\par\r\n";
                    }
                    else if (Ayuda == Ayudas.Help_viewer)
                    {
                        Texto_Ayuda += "\\ul \\b [Coming soon...]\\b0 \\ulnone \\par\r\n";
                    }
                    else if (Ayuda == Ayudas.About)
                    {
                        Texto_Ayuda += "\\ul \\b [Coming soon...]\\b0 \\ulnone \\par\r\n";
                    }
                    else if (Ayuda == Ayudas.Secrets)
                    {
                        Texto_Ayuda +=
                        "\\ul \\b [Minecraft secrets]\\b0 \\ulnone \\par\r\n" +
                        "\\b - \"Debug world\":\\b0  On Minecraft go to \"Create New World\", then click on \"More World Options...\". Now hold down the shift key (not the key you use to sneak on Minecraft, but the real shift key on your keyboard), and while holding it, click several times on \"World Type: Default\" until you see \"World Type: Debug Mode\", finally click on \"Create New World\" (and release the shift key). Now you'll be playing in the debug world used only by the Minecraft creators. There you'll see all the possible block combinations at around Y = 70, and a barrier at Y = 60. This world might be really useful for testing new resource packs for Minecraft. So have fun with it and tell everyone else about this cool secret.\\par\r\n" +

                        "\\par\\ul \\b [Minecraft Tools secrets]\\b0 \\ulnone \\par\r\n" +
                        "\\b - \"Secrets: hidden\":\\b0  if you are programming new tools for Minecraft and need to know how certain parts of the game work, you might take a look at the Minecraft wiki. Doing it that way might take you a lot of time and effort, so now there's a faster way since this application includes several Minecraft versions inside zip files. But they are encrypted by default, and since distributing them directly shouldn't be done, there's an option that lets you decrypt the zip files, but if you decide to do this, first you'll have to promise that you won't redistribute the source codes, that you'll only use them for learning purposes, like checking or updating the Minecraft wiki information (I found 2 bugs for Minecraft 1.12 Data values: the netherrack and quartz slabs have their values inverted and also the acacia and dark oak fence posts), or developing new cool tools for the community, and that you'll never do anything illegal with it (like playing without having purchased the game, etc). If you ever use it wrong, then you'll be the only responsible for that, because I gave it to you encrypted with my best intention and only for learning purposes (like the way I discovered how to load the Minecraft debug world thanks to it's source code). So please use it well, thanks a lot. And remember... you have been warned! To show the secret menus and decrypt options, hold shift while clicking the \"Secrets: hidden\" status bar button (or middle click it) and finally follow the new window instructions. Note: it also includes other cool stuff like an awesome font with the Standard Galactic Alphabet (SGA) for you to use in any text editor, and all the Xbox 360 resource packs and skins reconverted to PC.\\par\r\n";
                    }
                    else Texto_Ayuda += "The help file for the selected tool couldn't be found.";
                    Texto_Ayuda += "\\pard\\sa200\\sl276\\slmult1\\f1\\fs22\\lang10\\par\r\n}";
                    RichTextBox_Ayuda.Rtf = Texto_Ayuda;
                    RichTextBox_Ayuda.ZoomFactor = Zoom != 1.5f ? 1.5f : 2.5f;
                    RichTextBox_Ayuda.ZoomFactor = Zoom;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void ComboBox_Ayuda_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    RichTextBox_Ayuda.ZoomFactor = 1f;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Temporizador_Principal_Tick(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    Program.Proceso.Refresh();
                    long Memoria_Bytes = Program.Proceso.PagedMemorySize64;
                    Barra_Estado_Etiqueta_Memoria.Text = "RAM: " + Program.Traducir_Tamaño_Bytes_Automático(Memoria_Bytes, 2, true);
                    if (Memoria_Bytes >= 4294967296L && !Cronómetro_Memoria.IsRunning) Cronómetro_Memoria.Restart();
                    else if (Memoria_Bytes < 4294967296L && Cronómetro_Memoria.IsRunning)
                    {
                        Cronómetro_Memoria.Reset();
                        Barra_Estado_Etiqueta_Memoria.ForeColor = Color.Black;
                    }
                    if (Cronómetro_Memoria.IsRunning)
                    {
                        Barra_Estado_Etiqueta_Memoria.ForeColor = (Cronómetro_Memoria.ElapsedMilliseconds / 500L) % 2 == 0 ? Color.Black : Color.Red;
                    }
                }
                catch { Barra_Estado_Etiqueta_Memoria.Text = "RAM: ? MB (? GB)"; }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            try
            {
                if (Variable_Zoom != RichTextBox_Ayuda.ZoomFactor)
                {
                    Variable_Zoom = RichTextBox_Ayuda.ZoomFactor;
                    Registro_Guardar_Opciones();
                    this.Text = Texto_Título + " - [Helps: " + Program.Traducir_Número(ComboBox_Ayuda.Items.Count) + (ComboBox_Ayuda.Items.Count != 1 ? " files" : " file") + ", Zoom: " + Program.Traducir_Número(Variable_Zoom) + "x]";
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal void Registro_Cargar_Opciones()
        {
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Help Viewer");

                // Main options:
                try { Variable_Zoom = float.Parse((string)Clave.GetValue("Zoom", (1f).ToString())); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Zoom = 1f; }

                // Correct any bad value after loading:
                if (Variable_Zoom < 0.1f || Variable_Zoom > 5f) Variable_Zoom = 1f;

                // Apply all the loaded values:
                RichTextBox_Ayuda.ZoomFactor = Variable_Zoom;;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal void Registro_Guardar_Opciones()
        {
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Help Viewer");
                string[] Matriz_Nombres = Clave.GetValueNames();
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                    {
                        Clave.DeleteValue(Matriz_Nombres[Índice]);
                    }
                }
                Matriz_Nombres = null;

                // Main options:
                try { Clave.SetValue("Zoom", Variable_Zoom.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal void Registro_Restablecer_Opciones()
        {
            try
            {
                RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Help Viewer");
                string[] Matriz_Nombres = Clave.GetValueNames();
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                    {
                        try { Clave.DeleteValue(Matriz_Nombres[Índice]); }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    Matriz_Nombres = null;
                }
                Clave.Close();
                Clave = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(RichTextBox_Ayuda.Text))
                {
                    RichTextBox_Ayuda.Copy();
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
                if (!string.IsNullOrEmpty(RichTextBox_Ayuda.Text))
                {
                    RichTextBox_Ayuda.SaveFile(Application.StartupPath + "\\Help " + Program.Obtener_Nombre_Temporal_Sin_Guiones() + ".txt", RichTextBoxStreamType.PlainText);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Menú_Contextual_Guardar_RTF_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(RichTextBox_Ayuda.Text))
                {
                    RichTextBox_Ayuda.SaveFile(Application.StartupPath + "\\Help " + Program.Obtener_Nombre_Temporal_Sin_Guiones() + ".rtf", RichTextBoxStreamType.RichText);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }
    }
}
