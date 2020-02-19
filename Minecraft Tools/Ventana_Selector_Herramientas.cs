using Minecraft_Tools.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Selector_Herramientas : Form
    {
        public Ventana_Selector_Herramientas()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Enumeration that defines the current state of a tool.
        /// </summary>
        internal enum Estados : int
        {
            /// <summary>
            /// Not working.
            /// </summary>
            Inoperativo = 0,
            /// <summary>
            /// Might work.
            /// </summary>
            Intermedio,
            /// <summary>
            /// It's working.
            /// </summary>
            Funcional
        }

        internal static readonly ListViewGroup Grupo_Avanzado = new ListViewGroup("Advanced tools  (so far they are the most powerful and advanced tools in this application)");
        internal static readonly ListViewGroup Grupo_Minecraft = new ListViewGroup("Minecraft tools  (they are directly related to Minecraft and might be hard to use without it)");
        internal static readonly ListViewGroup Grupo_Universal = new ListViewGroup("Universal tools  (use them for other things besides Minecraft and you'll be surprised)");
        internal static readonly ListViewGroup Grupo_Conocimiento = new ListViewGroup("Knowledge tools  (they might give you advanced knowledge about a lot of secret things)");
        internal static readonly ListViewGroup Grupo_Jupisoft = new ListViewGroup("Application tools  (they let you see or change extra information about this application)");
        internal static readonly ListViewGroup Grupo_Incompleto = new ListViewGroup("Unfinished tools  (sorry, I didn't had enough time to finish them for now, but eventually I will)");

        /// <summary>
        /// Structure that holds up all the information about a tool of this application.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Herramientas
        {
            internal string Texto;
            internal Bitmap Imagen;
            internal Estados Estado;
            internal bool Secretos;
            internal ListViewGroup Grupo;
            internal Type Tipo;
            internal string Texto_Tipo;
            internal string Descripción;
            internal DateTime Fecha;

            internal Herramientas(string Texto, Bitmap Imagen, Estados Estado, bool Secretos, ListViewGroup Grupo, Type Tipo, string Descripción, DateTime Fecha)
            {
                this.Texto = Texto;
                this.Imagen = Imagen;
                this.Estado = Estado;
                this.Secretos = Secretos;
                this.Grupo = Grupo;
                this.Tipo = Tipo;
                this.Texto_Tipo = Tipo != null ? Tipo.FullName : string.Empty;
                this.Descripción = Descripción;
                this.Fecha = Fecha;
            }

            internal static readonly Herramientas[] Matriz_Herramientas = new Herramientas[]
            {
                //new Herramientas("Block information", Resources.Controles_TextBox, typeof(Ventana_Información_Bloques), CheckState.Checked),
                //new Herramientas("Start a random tool every time", Resources.Aleatorio, null, CheckState.Unchecked),
                new Herramientas("3D block viewer with generator and exporter", Resources.Minecraft, Estados.Funcional, false, Grupo_Universal, typeof(Ventana_Visor_Bloques_3D), "Tool capable of generating a simulated 3D block with 6 individual images, it's highly customizable and can export the resulting 3D images.", new DateTime(2019, 3, 7)),
                new Herramientas("About", Resources.Jupisoft_16, Estados.Funcional, false, Grupo_Jupisoft, typeof(Ventana_Acerca), "Tool capable of showing the about window of this application with several useful links and a lot of information.", new DateTime(2018, 3, 17)),
                new Herramientas("Advancements viewer", Resources.Item_golden_apple, Estados.Funcional, false, Grupo_Avanzado, typeof(Ventana_Visor_Logros), "Tool capable of loading any Minecraft world and showing all the advancements each player has.", new DateTime(2020, 2, 16)),
                new Herramientas("Animated 3D skin viewer", Resources.Visor_Skins_3D, Estados.Inoperativo, false, Grupo_Minecraft, typeof(Ventana_Visor_Skins_Animado_3D), "Tool capable of loading any Minecraft skin image and render it as a real 3D model with individual body parts with custom movement and edition.", new DateTime(2018, 3, 17)),
                new Herramientas("Automatic skins downloader", Resources.Ordenar, Estados.Inoperativo, false, Grupo_Minecraft, typeof(Ventana_Descargador_Skins_Automático), "Tool capable of bulk downloading millions of Minecraft skins from real player names using the desired charset to generate names by order.", new DateTime(2018, 3, 17)),
                new Herramientas("Backups manager", Resources.Copia_Seguridad, Estados.Inoperativo, false, Grupo_Jupisoft, typeof(Ventana_Administrador_Copias_Seguridad), "Tool capable of generating in real time backups of your Minecraft worlds before editing them. Note: unfinished because none of the current tools will ever edit your worlds.", new DateTime(2018, 3, 17)),
                new Herramientas("Banner and shield designer (outdated since 1.14+)", Resources.minecraft_red_banner, Estados.Funcional, false, Grupo_Avanzado, typeof(Ventana_Diseñador_Estandartes_Escudos), "Tool capable of generating custom banner and shield designs, either for survival or creative mode, with image exporter that includes the full recipe, it also supports loading the recipe images.", new DateTime(2018, 3, 17)),
                new Herramientas("Block densities counter", Resources.minecraft_diamond_block, Estados.Funcional, false, Grupo_Avanzado, typeof(Ventana_Contador_Densidades_Bloques), "Tool capable of counting all the block densities inside of a full region (1.024 chunks). It includes very detailed information and graphics.", new DateTime(2020, 2, 12)),
                new Herramientas("Block information viewer", Resources.Controles_TextBox, Estados.Funcional, false, Grupo_Minecraft, typeof(Ventana_Visor_Información_Bloques), "Tool capable of showing a full Minecraft block list with almost all the information of any possible block, even it's Minecraft 1.12.2- ID and Data values.", new DateTime(2018, 3, 17)),
                new Herramientas("Block selector (only for the pixel art generator)", Resources.minecraft_stone, Estados.Funcional, false, Grupo_Minecraft, typeof(Ventana_Selector_Bloques), "Tool capable of selecting Minecraft blocks.", new DateTime(2018, 3, 17)),
                new Herramientas("Blocks screen saver in 3D with Windows installer", Resources.minecraft_dirt, Estados.Funcional, false, Grupo_Minecraft, typeof(Ventana_Salvapantallas_Bloques), "Tool capable of generating a real time 2D or 3D blocks screen saver, it includes an installer as a regular Windows screen saver, very low CPU use.", new DateTime(2018, 3, 17)),
                new Herramientas("Change log", Resources.Registro_Cambios, Estados.Funcional, false, Grupo_Jupisoft, typeof(Ventana_Registro_Cambios), "Tool capable of showing all the changes done to this application over time with high accuracy dates and changes.", new DateTime(2018, 3, 17)),
                new Herramientas("Comparer of Minecraft versions and resource packs", Resources.WinRAR, Estados.Funcional, false, Grupo_Universal, typeof(Ventana_Comparador_Versiones_JAR), "Tool capable of comparing 2 JAR files (also ZIP, RAR, etc), and generate a list with the differences in the contents of those files to see what has changed.", new DateTime(2018, 3, 17)),
                new Herramientas("Custom light maps resource packs generator (Optifine)", Resources.Visión_Nocturna, Estados.Funcional, false, Grupo_Minecraft, typeof(Ventana_Generador_Packs_Recursos_Mapas_Luz), "Tool capable of generating new modular resource packs than can give you an infinite night (or blind) vision effect without particles or coloring the screen with your custom colors.", new DateTime(2019, 3, 26)),
                new Herramientas("Custom structures generator", Resources.minecraft_structure_block, Estados.Intermedio, false, Grupo_Minecraft, typeof(Ventana_Generador_Estructuras_Personalizadas), "Tool capable of generating new 1.12.2- Minecraft worlds with your custom structures in it, like several pyramids, polygons and even mazes or Minecraft structures.", new DateTime(2018, 3, 17)),
                new Herramientas("Damaged files rebuilder", Resources.Opciones, Estados.Funcional, false, Grupo_Universal, typeof(Ventana_Reconstructor_Archivos_Dañados), "Tool capable of loading 3 different damaged copies of a file (if it was downloaded damaged 3 times or so), and export a new copy without errors (unless the 3 were damaged at the same places).", new DateTime(2019, 2, 13)),
                new Herramientas("Duplicated files finder", Resources.Copiar, Estados.Funcional, false, Grupo_Universal, typeof(Ventana_Buscador_Archivos_Duplicados), "Tool capable of finding duplicated files inside your desired folders, it can also move outside the cloned files to identify them quickly.", new DateTime(2018, 10, 21)),
                new Herramientas("Enchantment names viewer", Resources.Item_enchanted_book, Estados.Funcional, false, Grupo_Minecraft, typeof(Ventana_Visor_Nombres_Encantamientos), "Tool capable of generating random enchantment names like Minecraft does and even to generate your custom messages in the Standard Galactic Alphabet (SGA).", new DateTime(2018, 3, 17)),
                new Herramientas("Entities information viewer", Resources.minecraft_player_head, Estados.Funcional, false, Grupo_Minecraft, typeof(Ventana_Visor_Información_Entidades), "Tool capable of showing a list of all the minecraft mobs and living entities, with all of their properties like health, damage, etc.", new DateTime(2018, 3, 17)),
                new Herramientas("Exception debugger", Resources.Excepción, Estados.Funcional, false, Grupo_Jupisoft, typeof(Ventana_Depurador_Excepciones), "Tool capable of showing a detailed list of all the exceptions that happened while running the application at any time.", new DateTime(2018, 3, 17)),
                new Herramientas("File encoder and decoder from Minecraft worlds", Resources.Llave, Estados.Intermedio, false, Grupo_Universal, typeof(Ventana_Codificador_Descodificador_Archivos), "Tool capable of generating new 1.12.2- Minecraft worlds with your desired file in them encoded with only 256 different blocks, supports passwords, etc.", new DateTime(2018, 3, 17)),
                new Herramientas("Files and images analyzer (CRC and hash generator)", Resources.Controles_TextBox, Estados.Funcional, false, Grupo_Universal, typeof(Ventana_Analizador_Imágenes_Archivos), "Tool capable of generating several hashes of any file and even from the pixels of any valid image.", new DateTime(2019, 10, 20)),
                new Herramientas("Fonts resource packs generator", Resources.Codificación, Estados.Funcional, false, Grupo_Minecraft, typeof(Ventana_Generador_Packs_Recursos_Fuentes), "Tool capable of generating new modular resource packs with your desired Windows font in them, to show it as the Minecraft font in game.", new DateTime(2019, 4, 3)),
                new Herramientas("FPS counter", Resources.Temporizador, Estados.Funcional, false, Grupo_Universal, typeof(Ventana_Contador_FPS), "Tool capable of counting the FPS of any portion of the screen in real time, but by counting how many times the selected region changes any of it's pixels.", new DateTime(2018, 10, 21)),
                new Herramientas("Help viewer", Resources.Ayuda, Estados.Funcional, false, Grupo_Jupisoft, typeof(Ventana_Visor_Ayuda), "Tool capable of showing a full help about this application and all of it's tools.", new DateTime(2018, 3, 17)),
                new Herramientas("Hermitcraft members viewer (outdated since season 6)", Resources.Xisumavoid, Estados.Funcional, false, Grupo_Minecraft, typeof(Ventana_Información_Miembros_Hermitcraft), "Tool capable of showing a full list of the Hermitcraft members with it's pictures, Minecraft skins, and a lot of their links and info.", new DateTime(2018, 3, 17)),
                new Herramientas("Image comparator (pixel by pixel)", Resources.Buscar, Estados.Funcional, false, Grupo_Universal, typeof(Ventana_Comparador_Imágenes), "Tool capable of comparing 2 images pixel by pixel to see if they actually are the same one or not.", new DateTime(2019, 12, 28)),
                new Herramientas("Image extractor (from any file)", Resources.Imagen, Estados.Funcional, false, Grupo_Universal, typeof(Ventana_Extractor_Imágenes), "Tool capable of detecting and extracting a lot of known image formats hidden within any file, it somehow uses \"smart\" brute force to extract them, but it's very quick.", new DateTime(2019, 10, 4)),
                new Herramientas("Infiniscope (top secret)", Resources.Ojo_Ciego, Estados.Funcional, true, Grupo_Conocimiento, typeof(Ventana_Infiniscopio), "Tool capable of showing a detailed description to build an old \"alien\" device that's like a telescope of thousands of kilometers, but with only a few centimeters.", new DateTime(2019, 2, 13)),
                new Herramientas("Júpiter Mauro free music site (all Creative Commons)", Resources.Jupisoft_16, Estados.Funcional, false, Grupo_Conocimiento, typeof(Ventana_Visor_Partituras_Júpiter_Mauro), "The old tool that used to be here was replaced with a link to the music site of Júpiter Mauro (Jupisoft) to save space in the whole application, sorry.", new DateTime(2018, 3, 17)),
                new Herramientas("Magic number guessing (real magic trick)", Resources.Montón_Centro, Estados.Funcional, false, Grupo_Conocimiento, typeof(Ventana_Adivinación_Número_Mágico), "Tool capable of playing a game where you think of a number between 1 and 21, and click 3 times on the pile it's shown, and after that the tool will tell you which number you thought of. It never fails.", new DateTime(2018, 3, 17)),
                new Herramientas("Map viewer", Resources.Item_map_filled, Estados.Funcional, false, Grupo_Avanzado, typeof(Ventana_Visor_Mapas), "Tool capable of finding, decoding and drawing all the maps stored inside each Minecraft world.", new DateTime(2020, 2, 14)),
                new Herramientas("Minecraft 1.13+ chunk format information viewer", Resources.Región, Estados.Funcional, true, Grupo_Conocimiento, typeof(Ventana_Visor_Formato_Chunks_1_13), "Tool capable of showing a very detailed explanation of the new Minecraft 1.13 chunk format and how to decode it, use this to update your own tools.", new DateTime(2019, 2, 13)),
                new Herramientas("Minecraft 1.13+ (or very old) to 1.12.2- world converter", Resources.Mundo, Estados.Funcional, false, Grupo_Avanzado, typeof(Ventana_Conversor_Mundos_1_13_a_1_12_2), "Tool capable of converting any Minecraft 1.13+ world to a new 1.12.2- world, but any entity or item will be lost. Now supports Indev, InfDev and all Minecraft 1.0 to 1.15.2 worlds.", new DateTime(2018, 3, 17)),
                new Herramientas("Minecraft internal structures exporter (includes PixARK)", Resources.PixARK, Estados.Funcional, false, Grupo_Avanzado, typeof(Ventana_Exportador_Estructuras_Internas), "Tool capable of generating a new 1.12.2- world that contains your desired NBT structures inside, like the end cities, shipwrecks, ocean ruins, villages, etc, and all in the same row (X+ axis).", new DateTime(2019, 04, 17/*2018, 3, 17*/)),
                new Herramientas("Minecraft Xbox 360 Edition resources extractor (1.13+)", Resources.Xbox, Estados.Funcional, false, Grupo_Minecraft, typeof(Ventana_Extractor_Recursos_Minecraft_Xbox_360), "Tool capable of decoding and extracting all the skin and resource packs from the latest Minecraft Xbox 360 Edition Title Update, and reconvert those to PC format.", new DateTime(2019, 04, 23)),
                new Herramientas("Multidimensional mathematical analyzer (top secret)", Resources.Fractal, Estados.Funcional, true, Grupo_Conocimiento, typeof(Ventana_Analizador_Matemático_Multidimensional), "Tool capable of analyzing multiple mathematic dimensions made by using several bases like 2 and 16 and limitng the values to 256 for example.", new DateTime(2019, 2, 13)),
                new Herramientas("Monster High characters", Resources.Monster_High, Estados.Funcional, false, Grupo_Conocimiento, typeof(Ventana_Monster_High), "Tool capable of showing a full list of characters of Monster High, with a very detailed biography and several pictures.", new DateTime(2018, 10, 21)),
                new Herramientas("Multiple structures finder", Resources.Estructura_Monumento.Clone(new Rectangle(2, 2, 16, 16), PixelFormat.Format32bppArgb), Estados.Funcional, false, Grupo_Minecraft, typeof(Ventana_Buscador_Estructuras_Dobles), "Tool capable of finding the coordinates where several structures of the same type might collide with each other, and thus creating something unexpected and sometimes interesting or very hard to find.", new DateTime(2019, 04, 20)),
                new Herramientas("NBT viewer", Resources.NBT_Byte, Estados.Funcional, false, Grupo_Avanzado, typeof(Ventana_Visor_NBT), "Tool capable of loading any NBT file and explore it's contents as read-only, so for now it can't edit any file or save it back, sorry.", new DateTime(2018, 3, 17)),
                new Herramientas("Note blocks tuner", Resources.minecraft_note_block, Estados.Intermedio, false, Grupo_Minecraft, typeof(Ventana_Afinador_Bloques_Nota), "Tool capable of playing most note block sounds with all of it's multiple notes.", new DateTime(2018, 3, 17)),
                new Herramientas("Obfuscation mappings converter", Resources.Mapas_Ofuscación, Estados.Funcional, false, Grupo_Minecraft, typeof(Ventana_Conversor_Mapas_Ofuscación), "Tool capable of generating inverting the direction of the conversion in the obfuscation maps since the 1.15 snapshot 19w36a. Used to deobfuscate the Minecraft source code.", new DateTime(2019, 9, 7)),
                new Herramientas("Online files downloader", Resources.Internet_Explorer, Estados.Intermedio, false, Grupo_Incompleto, typeof(Ventana_Descargador_Archivos_Internet), "Tool capable of recursively detecting all links from any webpage until it locates the wanted file types and downloads them (like images from Instagram, Minecraft skins, etc).", new DateTime(2019, 9, 27)),
                new Herramientas("Painted structures exporter", Resources.Paleta, Estados.Intermedio, false, Grupo_Incompleto, typeof(Ventana_Exportador_Estructuras_Pintadas), "Tool capable of generating new 1.12.2- Minecraft worlds with custom structures inside, but from images with painted blocks.", new DateTime(2018, 3, 17)),
                new Herramientas("Paintings images converter between 1.13.2- and 1.14+", Resources.Pool, Estados.Funcional, false, Grupo_Minecraft, typeof(Ventana_Conversor_Imagen_Cuadros), "Tool capable of converting the \"paintings_kristoffer_zetterstrand.png\" image (1.13.2-) to multiple images (1.14+) and viceversa.", new DateTime(2018, 3, 17)),
                new Herramientas("Paintings viewer", Resources.Pool, Estados.Funcional, false, Grupo_Minecraft, typeof(Ventana_Visor_Cuadros), "Tool capable of showing all the real HD paintings that Minecraft has, and it can even export them and show it's full description.", new DateTime(2018, 3, 17)),
                new Herramientas("Pixel art generator with world exporter", Resources.Pixel_Art, Estados.Funcional, false, Grupo_Avanzado, typeof(Ventana_Generador_Pixel_Art), "Tool capable of generating a new 1.12.2- Minecraft world with any selected image inside, but replacing any pixel with a similar Minecraft block.", new DateTime(2018, 3, 17)),
                new Herramientas("Prime numbers finder", Resources.Buscar, Estados.Funcional, false, Grupo_Universal, typeof(Ventana_Buscador_Números_Primos), "Tool capable of quickly searching prime numbers and store them to a binary data base where each 8 bytes is a 64 bits number.", new DateTime(2019, 10, 21)),
                new Herramientas("Random numbers generator", Resources.Aleatorio, Estados.Funcional, false, Grupo_Universal, typeof(Ventana_Generador_Números_Aleatorios), "Tool capable of generating random numbers over time, includes TRNG (True Random Number Generators).", new DateTime(2019, 8, 24)),
                new Herramientas("Random worlds generator", Resources.Mundo, Estados.Intermedio, false, Grupo_Incompleto, typeof(Ventana_Generador_Mundos_Aleatorios), "Tool capable of generating new random Minecraft worlds that contain customized terrain, uses the Perlin Noise function from Unity 3D.", new DateTime(2019, 8, 24)),
                new Herramientas("Real time Minecraft clock", Resources.Sol_Luna, Estados.Funcional, false, Grupo_Minecraft, typeof(Ventana_Reloj_Minecraft_Tiempo_Real), "Tool capable of simulating the time flow in Minecraft, with several information like the signals form a daylight sensor, etc.", new DateTime(2018, 3, 17)),
                new Herramientas("Real time screen filters", Resources.Pantalla, Estados.Funcional, true, Grupo_Universal, typeof(Ventana_Filtros_Tiempo_Real), "Tool capable of filtering any part of the screen in real time, with dozens of exclusive filters (even for video) and also with custom zoom levels.", new DateTime(2019, 1, 21)),
                new Herramientas("Realistic 2D world viewer", Resources.Visor_Mundos_2D, Estados.Funcional, false, Grupo_Avanzado, typeof(Ventana_Visor_Mundos_Realista_2D), "Tool capable of generating a 2D image from the top of any Minecraft world, with dozens of custom maps like block search, caves, ores, structures and slime chunks icons, etc.", new DateTime(2018, 3, 17)),
                new Herramientas("Recipes viewer", Resources.Menú_Contextual, Estados.Intermedio, false, Grupo_Incompleto, typeof(Ventana_Visor_Recetas), "Tool capable of showing a full list of the available crafting, smelting, etc, recipes in Minecraft.", new DateTime(2018, 3, 17)),
                new Herramientas("Redstone designer", Resources.minecraft_redstone_block, Estados.Inoperativo, false, Grupo_Minecraft, null, "Tool capable of generating new 1.12.2- Minecraft worlds with your painted redstone circuits and contraptions as images but with real blocks.", new DateTime(2018, 3, 17)),
                new Herramientas("Resource packs converter with zip and folder support", Resources.Pack_Recursos, Estados.Funcional, false, Grupo_Minecraft, typeof(Ventana_Conversor_Packs_Recursos), "Tool capable of converting resource packs between pack formats 1 to 9, it will always generate a new resource pack.", new DateTime(2019, 3, 11)),
                new Herramientas("Resource structure rebuilder", Resources.Controles_TreeView, Estados.Funcional, false, Grupo_Minecraft, typeof(Ventana_Reconstructor_Estructura_Archivos_Recursos), "Tool capable of reading the JSON files within the assets folder, and exporting to your desktop the Minecraft resources with their real names.", new DateTime(2018, 3, 17)),
                //new Herramientas("Score viewer in real time with full sound analysis", Resources.Ventana, Estados.Funcional, true, Grupo_Universal, typeof(Ventana_Visor_Partituras), "Tool capable of showing the score from any sound in real time", new DateTime(2019, 7, 20)), // Searh it on GitHub near this main application as "Score Viewer".
                new Herramientas("Secrets: hidden", Resources.Candado, Estados.Funcional, true, Grupo_Conocimiento, typeof(Ventana_Secretos), "Tool capable of enabling and exporting highly secret resource packs, and other cool stuff to help you understand better how Minecraft works.", new DateTime(2018, 3, 17)),
                new Herramientas("Sky box resource packs generator (Optifine)", Resources.Cielo, Estados.Funcional, false, Grupo_Minecraft, typeof(Ventana_Generador_Packs_Recursos_Cielos), "Tool capable of generating new modular resource packs with your desired sky box, made from your desired 6 images.", new DateTime(2019, 3, 24)),
                //new Herramientas("Sky simulator in 3D", Resources.Cielo, Estados.Inoperativo, false, Grupo_Minecraft, typeof(Ventana_Simulador_Cielo_3D), "", new DateTime(2019, 3, 3)),
                new Herramientas("Slime chunks and structures finder", Resources.minecraft_slime_block, Estados.Funcional, false, Grupo_Avanzado, typeof(Ventana_Buscador_Chunks_Limos_Estructuras), "Tool capable of predicting where will be able to spawn slimes (find slime chunks), and even to predict where other structures might be located if the biome matches in that coordinate.", new DateTime(2018, 3, 17)),
                new Herramientas("Spiral designer (use slabs to build them)", Resources.Espiral, Estados.Funcional, false, Grupo_Avanzado, typeof(Ventana_Diseñador_Espirales), "Tool capable of designing spirals with the desired dimensions and \"steps\", very useful to make them with slabs, where each color is a different height level.", new DateTime(2020, 2, 9)),
                new Herramientas("Structure generator through commands", Resources.minecraft_command_block, Estados.Inoperativo, false, Grupo_Minecraft, null, "Tool capable of writing commands in the Minecraft console in real time to build your desired structures, using the fill command and others. if it fails it might destroy your world forever!", new DateTime(2018, 3, 17)),
                new Herramientas("UUID generator like Minecraft", Resources.Controles_TextBox, Estados.Funcional, false, Grupo_Universal, typeof(Ventana_Generador_UUID), "Tool capable of generating an UUID from any text like Minecraft does based on the player names, also supports all files by dropping them. Compared with Minecraft the results were the same.", new DateTime(2020, 2, 17)),
                new Herramientas("Useful seeds registry", Resources.Item_wheat_seeds, Estados.Funcional, false, Grupo_Minecraft, typeof(Ventana_Registro_Semillas_Útiles), "Tool capable of showing a detailed list of Minecraft seeds with unique and cool features like \"Draculaura\" for Minecraft 1.13+.", new DateTime(2019, 3, 12)),
                new Herramientas("Thank you", Resources.Lista, Estados.Funcional, false, Grupo_Jupisoft, typeof(Ventana_Gracias), "Tool capable of showing a list of all the people and organizations to which I'm very thankful for all their help and support.", new DateTime(2018, 3, 17)),
                new Herramientas("The End screensaver (WIP)", Resources.minecraft_end_portal_frame, Estados.Intermedio, false, Grupo_Incompleto, typeof(Ventana_Salvapantallas_El_Fin), "Tool capable of simulating the old end portal animation of stars, but currently it's too slow.", new DateTime(2018, 3, 17)),
                new Herramientas("Thumbnails and average color generator", Resources.Ojo, Estados.Funcional, false, Grupo_Universal, typeof(Ventana_Generador_Miniaturas_Color_Medio), "Tool capable of reducing the colors from any image to only 1, so this will be the average color, like it's shown on my 2D overworld viewer.", new DateTime(2018, 3, 17)),
                new Herramientas("Villager tradings viewer (outdated since 1.14+)", Resources.minecraft_emerald_block, Estados.Funcional, false, Grupo_Minecraft, typeof(Ventana_Visor_Ofertas_Aldeanos), "Tool capable of showing a 1.13.2- villager trading picture, with custom background color and exporting.", new DateTime(2018, 3, 17)),
                new Herramientas("Virtual moon in real time", Resources.Luna, Estados.Intermedio, false, Grupo_Universal, typeof(Ventana_Luna_Virtual), "Tool capable of predicting the moon phases until the year 3000, and even all the eclipses with high accuracy times.", new DateTime(2019, 3, 3)),
                new Herramientas("World seeds infinite calculator", Resources.Calculadora, Estados.Funcional, false, Grupo_Avanzado, typeof(Ventana_Calculadora_Infinita_Semillas_Mundos), "Tool capable of converting any text to a Minecraft numerical seed, and it even extends the Java code to show the real seed with infinite bits (for real).", new DateTime(2018, 3, 17)),
                new Herramientas("XNA resources extractor (don't use it for illegal things)", Resources.XNA, Estados.Funcional, false, Grupo_Universal, typeof(Ventana_Extractor_Recursos_XNA), "Tool capable of extracting and saving the XNA resources from any game that uses XNA 4.0 or so, like Stardew Valley, Terraria, etc.", new DateTime(2019, 3, 23)),
                //new Herramientas("None (select it manually everytime)", Resources.Ejecutar, Estados.Funcional, Categorías.Normal, null, "", new DateTime(2018, 3, 17)),
            };

            internal static void Ejecutar_Herramienta(int Índice_Herramienta, bool Siempre_Visible, Ventana_Principal Ventana_Superior)
            {
                try
                {
                    if (Índice_Herramienta < 0 || Índice_Herramienta >= Matriz_Herramientas.Length)
                    {
                        Índice_Herramienta = Program.Rand.Next(0, Matriz_Herramientas.Length); // Select a random tool.
                    }
                    if (Índice_Herramienta > -1 && Índice_Herramienta < Matriz_Herramientas.Length)
                    {
                        string Texto_Tipo = Matriz_Herramientas[Índice_Herramienta].Texto_Tipo;
                        if (string.IsNullOrEmpty(Texto_Tipo)) // None
                        {
                            // Do nothing.
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Visor_Bloques_3D).FullName, true) == 0)
                        {
                            Ventana_Visor_Bloques_3D Ventana = new Ventana_Visor_Bloques_3D();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Acerca).FullName, true) == 0)
                        {
                            Ventana_Acerca Ventana = new Ventana_Acerca();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Administrador_Copias_Seguridad).FullName, true) == 0)
                        {
                            Ventana_Administrador_Copias_Seguridad Ventana = new Ventana_Administrador_Copias_Seguridad();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Afinador_Bloques_Nota).FullName, true) == 0)
                        {
                            Ventana_Afinador_Bloques_Nota Ventana = new Ventana_Afinador_Bloques_Nota();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Salvapantallas_Bloques).FullName, true) == 0)
                        {
                            Ventana_Salvapantallas_Bloques Ventana = new Ventana_Salvapantallas_Bloques();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Buscador_Chunks_Limos_Estructuras).FullName, true) == 0)
                        {
                            Ventana_Buscador_Chunks_Limos_Estructuras Ventana = new Ventana_Buscador_Chunks_Limos_Estructuras();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Calculadora_Infinita_Semillas_Mundos).FullName, true) == 0)
                        {
                            Ventana_Calculadora_Infinita_Semillas_Mundos Ventana = new Ventana_Calculadora_Infinita_Semillas_Mundos();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Diseñador_Espirales).FullName, true) == 0)
                        {
                            Ventana_Diseñador_Espirales Ventana = new Ventana_Diseñador_Espirales();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Extractor_Recursos_XNA).FullName, true) == 0)
                        {
                            Ventana_Extractor_Recursos_XNA Ventana = new Ventana_Extractor_Recursos_XNA();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Codificador_Descodificador_Archivos).FullName, true) == 0)
                        {
                            Ventana_Codificador_Descodificador_Archivos Ventana = new Ventana_Codificador_Descodificador_Archivos();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Analizador_Imágenes_Archivos).FullName, true) == 0)
                        {
                            Ventana_Analizador_Imágenes_Archivos Ventana = new Ventana_Analizador_Imágenes_Archivos();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Generador_Packs_Recursos_Fuentes).FullName, true) == 0)
                        {
                            Ventana_Generador_Packs_Recursos_Fuentes Ventana = new Ventana_Generador_Packs_Recursos_Fuentes();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Comparador_Versiones_JAR).FullName, true) == 0)
                        {
                            Ventana_Comparador_Versiones_JAR Ventana = new Ventana_Comparador_Versiones_JAR();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Generador_Packs_Recursos_Mapas_Luz).FullName, true) == 0)
                        {
                            Ventana_Generador_Packs_Recursos_Mapas_Luz Ventana = new Ventana_Generador_Packs_Recursos_Mapas_Luz();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Depurador_Excepciones).FullName, true) == 0)
                        {
                            Ventana_Depurador_Excepciones Ventana = new Ventana_Depurador_Excepciones();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Visor_Logros).FullName, true) == 0)
                        {
                            Ventana_Visor_Logros Ventana = new Ventana_Visor_Logros();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Visor_Skins_Animado_3D).FullName, true) == 0)
                        {
                            Ventana_Visor_Skins_Animado_3D Ventana = new Ventana_Visor_Skins_Animado_3D();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Descargador_Skins_Automático).FullName, true) == 0)
                        {
                            Ventana_Descargador_Skins_Automático Ventana = new Ventana_Descargador_Skins_Automático();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Diseñador_Estandartes_Escudos).FullName, true) == 0)
                        {
                            Ventana_Diseñador_Estandartes_Escudos Ventana = new Ventana_Diseñador_Estandartes_Escudos();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Exportador_Estructuras_Internas).FullName, true) == 0)
                        {
                            Ventana_Exportador_Estructuras_Internas Ventana = new Ventana_Exportador_Estructuras_Internas();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Extractor_Recursos_Minecraft_Xbox_360).FullName, true) == 0)
                        {
                            Ventana_Extractor_Recursos_Minecraft_Xbox_360 Ventana = new Ventana_Extractor_Recursos_Minecraft_Xbox_360();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Analizador_Matemático_Multidimensional).FullName, true) == 0)
                        {
                            Ventana_Analizador_Matemático_Multidimensional Ventana = new Ventana_Analizador_Matemático_Multidimensional();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Adivinación_Número_Mágico).FullName, true) == 0)
                        {
                            Ventana_Adivinación_Número_Mágico Ventana = new Ventana_Adivinación_Número_Mágico();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Visor_Mapas).FullName, true) == 0)
                        {
                            Ventana_Visor_Mapas Ventana = new Ventana_Visor_Mapas();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Visor_Formato_Chunks_1_13).FullName, true) == 0)
                        {
                            Ventana_Visor_Formato_Chunks_1_13 Ventana = new Ventana_Visor_Formato_Chunks_1_13();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Conversor_Mundos_1_13_a_1_12_2).FullName, true) == 0)
                        {
                            Ventana_Conversor_Mundos_1_13_a_1_12_2 Ventana = new Ventana_Conversor_Mundos_1_13_a_1_12_2();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Monster_High).FullName, true) == 0)
                        {
                            Ventana_Monster_High Ventana = new Ventana_Monster_High();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Buscador_Estructuras_Dobles).FullName, true) == 0)
                        {
                            Ventana_Buscador_Estructuras_Dobles Ventana = new Ventana_Buscador_Estructuras_Dobles();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Conversor_Mapas_Ofuscación).FullName, true) == 0)
                        {
                            Ventana_Conversor_Mapas_Ofuscación Ventana = new Ventana_Conversor_Mapas_Ofuscación();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Descargador_Archivos_Internet).FullName, true) == 0)
                        {
                            Ventana_Descargador_Archivos_Internet Ventana = new Ventana_Descargador_Archivos_Internet();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Exportador_Estructuras_Pintadas).FullName, true) == 0)
                        {
                            Ventana_Exportador_Estructuras_Pintadas Ventana = new Ventana_Exportador_Estructuras_Pintadas();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Filtros_Tiempo_Real).FullName, true) == 0)
                        {
                            Ventana_Superior.Visible = false;
                            Ventana_Filtros_Tiempo_Real Ventana = new Ventana_Filtros_Tiempo_Real();
                            //Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                            Ventana_Superior.Visible = true;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Generador_Estructuras_Personalizadas).FullName, true) == 0)
                        {
                            Ventana_Generador_Estructuras_Personalizadas Ventana = new Ventana_Generador_Estructuras_Personalizadas();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Generador_Miniaturas_Color_Medio).FullName, true) == 0)
                        {
                            Ventana_Generador_Miniaturas_Color_Medio Ventana = new Ventana_Generador_Miniaturas_Color_Medio();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Generador_Pixel_Art).FullName, true) == 0)
                        {
                            Ventana_Generador_Pixel_Art Ventana = new Ventana_Generador_Pixel_Art();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Buscador_Números_Primos).FullName, true) == 0)
                        {
                            Ventana_Buscador_Números_Primos Ventana = new Ventana_Buscador_Números_Primos();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Generador_UUID).FullName, true) == 0)
                        {
                            Ventana_Generador_UUID Ventana = new Ventana_Generador_UUID();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Registro_Semillas_Útiles).FullName, true) == 0)
                        {
                            Ventana_Registro_Semillas_Útiles Ventana = new Ventana_Registro_Semillas_Útiles();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Gracias).FullName, true) == 0)
                        {
                            Ventana_Gracias Ventana = new Ventana_Gracias();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Información_Bloques).FullName, true) == 0)
                        {
                            Ventana_Información_Bloques Ventana = new Ventana_Información_Bloques();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Registro_Cambios).FullName, true) == 0)
                        {
                            Ventana_Registro_Cambios Ventana = new Ventana_Registro_Cambios();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Reconstructor_Estructura_Archivos_Recursos).FullName, true) == 0)
                        {
                            Ventana_Reconstructor_Estructura_Archivos_Recursos Ventana = new Ventana_Reconstructor_Estructura_Archivos_Recursos();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        /*else if (string.Compare(Texto_Tipo, typeof(Ventana_Visor_Partituras).FullName, true) == 0)
                        {
                            Ventana_Superior.Visible = false;
                            Ventana_Visor_Partituras Ventana = new Ventana_Visor_Partituras();
                            //Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                            Ventana_Superior.Visible = true;
                        }*/
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Secretos).FullName, true) == 0)
                        {
                            Ventana_Secretos Ventana = new Ventana_Secretos();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Generador_Packs_Recursos_Cielos).FullName, true) == 0)
                        {
                            Ventana_Generador_Packs_Recursos_Cielos Ventana = new Ventana_Generador_Packs_Recursos_Cielos();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Simulador_Cielo_3D).FullName, true) == 0)
                        {
                            Ventana_Simulador_Cielo_3D Ventana = new Ventana_Simulador_Cielo_3D();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Generador_Números_Aleatorios).FullName, true) == 0)
                        {
                            Ventana_Generador_Números_Aleatorios Ventana = new Ventana_Generador_Números_Aleatorios();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Generador_Mundos_Aleatorios).FullName, true) == 0)
                        {
                            Ventana_Generador_Mundos_Aleatorios Ventana = new Ventana_Generador_Mundos_Aleatorios();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Reloj_Minecraft_Tiempo_Real).FullName, true) == 0)
                        {
                            Ventana_Reloj_Minecraft_Tiempo_Real Ventana = new Ventana_Reloj_Minecraft_Tiempo_Real();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Salvapantallas_El_Fin).FullName, true) == 0)
                        {
                            Ventana_Salvapantallas_El_Fin Ventana = new Ventana_Salvapantallas_El_Fin();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Selector_Bloques).FullName, true) == 0)
                        {
                            Ventana_Selector_Bloques Ventana = new Ventana_Selector_Bloques();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Contador_FPS).FullName, true) == 0)
                        {
                            Ventana_Superior.Visible = false;
                            Ventana_Contador_FPS Ventana = new Ventana_Contador_FPS();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                            Ventana_Superior.Visible = true;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Visor_Ayuda).FullName, true) == 0)
                        {
                            Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Información_Miembros_Hermitcraft).FullName, true) == 0)
                        {
                            Ventana_Información_Miembros_Hermitcraft Ventana = new Ventana_Información_Miembros_Hermitcraft();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Comparador_Imágenes).FullName, true) == 0)
                        {
                            Ventana_Superior.Visible = false;
                            Ventana_Comparador_Imágenes Ventana = new Ventana_Comparador_Imágenes();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                            Ventana_Superior.Visible = true;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Extractor_Imágenes).FullName, true) == 0)
                        {
                            Ventana_Extractor_Imágenes Ventana = new Ventana_Extractor_Imágenes();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Infiniscopio).FullName, true) == 0)
                        {
                            Ventana_Infiniscopio Ventana = new Ventana_Infiniscopio();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Visor_Partituras_Júpiter_Mauro).FullName, true) == 0)
                        {
                            Program.Ejecutar_Ruta("http://jupisoft.x10host.com/", ProcessWindowStyle.Normal);
                            /*Ventana_Visor_Partituras_Júpiter_Mauro Ventana = new Ventana_Visor_Partituras_Júpiter_Mauro();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;*/
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Conversor_Imagen_Cuadros).FullName, true) == 0)
                        {
                            Ventana_Conversor_Imagen_Cuadros Ventana = new Ventana_Conversor_Imagen_Cuadros();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Visor_Cuadros).FullName, true) == 0)
                        {
                            Ventana_Visor_Cuadros Ventana = new Ventana_Visor_Cuadros();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Contador_Densidades_Bloques).FullName, true) == 0)
                        {
                            Ventana_Contador_Densidades_Bloques Ventana = new Ventana_Contador_Densidades_Bloques();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Visor_Información_Bloques).FullName, true) == 0)
                        {
                            Ventana_Visor_Información_Bloques Ventana = new Ventana_Visor_Información_Bloques();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Visor_Información_Entidades).FullName, true) == 0)
                        {
                            Ventana_Visor_Información_Entidades Ventana = new Ventana_Visor_Información_Entidades();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Visor_Mundos_Realista_2D).FullName, true) == 0)
                        {
                            Ventana_Visor_Mundos_Realista_2D Ventana = new Ventana_Visor_Mundos_Realista_2D();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Visor_Recetas).FullName, true) == 0)
                        {
                            Ventana_Visor_Recetas Ventana = new Ventana_Visor_Recetas();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Visor_NBT).FullName, true) == 0)
                        {
                            Ventana_Visor_NBT Ventana = new Ventana_Visor_NBT();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Reconstructor_Archivos_Dañados).FullName, true) == 0)
                        {
                            Ventana_Reconstructor_Archivos_Dañados Ventana = new Ventana_Reconstructor_Archivos_Dañados();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Buscador_Archivos_Duplicados).FullName, true) == 0)
                        {
                            Ventana_Buscador_Archivos_Duplicados Ventana = new Ventana_Buscador_Archivos_Duplicados();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Visor_Nombres_Encantamientos).FullName, true) == 0)
                        {
                            Ventana_Visor_Nombres_Encantamientos Ventana = new Ventana_Visor_Nombres_Encantamientos();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Visor_Ofertas_Aldeanos).FullName, true) == 0)
                        {
                            Ventana_Visor_Ofertas_Aldeanos Ventana = new Ventana_Visor_Ofertas_Aldeanos();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Luna_Virtual).FullName, true) == 0)
                        {
                            Ventana_Luna_Virtual Ventana = new Ventana_Luna_Virtual();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else if (string.Compare(Texto_Tipo, typeof(Ventana_Conversor_Packs_Recursos).FullName, true) == 0)
                        {
                            Ventana_Conversor_Packs_Recursos Ventana = new Ventana_Conversor_Packs_Recursos();
                            Ventana.Variable_Siempre_Visible = Siempre_Visible;
                            Ventana.ShowDialog(Ventana_Superior);
                            Ventana.Dispose();
                            Ventana = null;
                        }
                        else MessageBox.Show(Ventana_Superior, "The selected tool couldn't be found or started yet.\nProbably it's under development and soon will be fully released.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else MessageBox.Show(Ventana_Superior, "The selected tool couldn't be found or started yet.\nProbably it's under development and soon will be fully released.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception Excepción)
                {
                    Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null);
                    MessageBox.Show(Ventana_Superior, "The selected tool couldn't be found or started yet.\nProbably it's under development and soon will be fully released.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        internal readonly string Texto_Título = "Tool Selector by Jupisoft for " + Program.Texto_Usuario;
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
        internal int Índice_Herramienta = -1;

        /// <summary>
        /// This variable is used to know if the user is selecting the default start tool.
        /// </summary>
        internal bool Seleccionar_Herramienta_Inicio = false;

        //internal int Matiz_Infiniscopio = 0;
        //internal ListViewItem Objeto_Infiniscopio = null;

        private void Ventana_Selector_Herramientas_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.WindowState = FormWindowState.Maximized;
                ListView_Principal.Groups.Add(Grupo_Avanzado);
                ListView_Principal.Groups.Add(Grupo_Minecraft);
                ListView_Principal.Groups.Add(Grupo_Universal);
                ListView_Principal.Groups.Add(Grupo_Conocimiento);
                ListView_Principal.Groups.Add(Grupo_Jupisoft);
                ListView_Principal.Groups.Add(Grupo_Incompleto);
                DateTime Fecha = DateTime.Now;
                TimeSpan Intervalo = TimeSpan.FromDays(365.25d / 2d); // Half year.
                for (int Índice = 0; Índice < Herramientas.Matriz_Herramientas.Length; Índice++)
                {
                    Lista_Imágenes_16.Images.Add(Herramientas.Matriz_Herramientas[Índice].Imagen);
                    ListViewItem Objeto = new ListViewItem(Herramientas.Matriz_Herramientas[Índice].Texto, Índice);

                    if (Herramientas.Matriz_Herramientas[Índice].Estado == Estados.Funcional)
                    {
                        Objeto.ForeColor = Color.Black;
                    }
                    else if (Herramientas.Matriz_Herramientas[Índice].Estado == Estados.Intermedio)
                    {
                        Objeto.ForeColor = Color.Blue;
                    }
                    else if (Herramientas.Matriz_Herramientas[Índice].Estado == Estados.Inoperativo)
                    {
                        Objeto.ForeColor = Color.Red;
                    }
                    else Objeto.ForeColor = Color.Gray; // Unknown.

                    if (Herramientas.Matriz_Herramientas[Índice].Secretos)
                    {
                        Objeto.BackColor = Color.FromArgb(255, 255, 255, 144); // Secret tool.
                    }
                    else if (Fecha - Herramientas.Matriz_Herramientas[Índice].Fecha <= Intervalo)
                    {
                        //Objeto.BackColor = Color.FromArgb(255, 240, 240, 240); // New tool.
                        //Objeto.BackColor = Color.FromArgb(255, 224, 224, 224); // New tool.
                        Objeto.BackColor = Color.FromArgb(255, 208, 255, 208); // New tool.
                    }

                    if (Herramientas.Matriz_Herramientas[Índice].Estado != Estados.Inoperativo)
                    {
                        Objeto.Group = Herramientas.Matriz_Herramientas[Índice].Grupo;
                    }
                    else
                    {
                        Objeto.Group = Grupo_Incompleto;
                    }

                    ListView_Principal.Items.Add(Objeto);
                    if (Program.Edición_Aplicación != CheckState.Unchecked)
                    {
                        if (Program.Edición_Aplicación == CheckState.Checked)
                        {
                            if (string.Compare(Herramientas.Matriz_Herramientas[Índice].Texto_Tipo, typeof(Ventana_Filtros_Tiempo_Real).FullName, true) == 0)
                            {
                                Objeto.Selected = true;
                            }
                        }
                        else if (Program.Edición_Aplicación == CheckState.Indeterminate)
                        {
                            if (string.Compare(Herramientas.Matriz_Herramientas[Índice].Texto_Tipo, typeof(Ventana_Analizador_Matemático_Multidimensional).FullName, true) == 0)
                            {
                                Objeto.Selected = true;
                            }
                        }
                    }
                }
                this.Text = Texto_Título + (!Seleccionar_Herramienta_Inicio ? " - [Registered tools: " + Program.Traducir_Número(Herramientas.Matriz_Herramientas.Length) + ", in " + Program.Traducir_Número(ListView_Principal.Groups.Count) + (ListView_Principal.Groups.Count != 1 ? " groups]" : " group]") : " - [Click accept without a selected tool to delete the stored one]");
                ListView_Principal.SmallImageList = Lista_Imágenes_16;
                ListView_Principal.LargeImageList = Lista_Imágenes_16;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Selector_Herramientas_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                //Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Selector_Herramientas_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Selector_Herramientas_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                /*ICSharpCode.SharpZipLib.Zip.Compression.Deflater Def = new ICSharpCode.SharpZipLib.Zip.Compression.Deflater(ICSharpCode.SharpZipLib.Zip.Compression.Deflater.DEFAULT_COMPRESSION);
                Def.Deflate(new byte[]);
                //Def.SetInput();
                ICSharpCode.SharpZipLib.Zip.ZipFile zip = ICSharpCode.SharpZipLib.Zip.ZipFile.Create();
                zip.Add("", ICSharpCode.SharpZipLib.Zip.CompressionMethod.Deflated);
                zip.Close();
                zip = null;*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Selector_Herramientas_KeyDown(object sender, KeyEventArgs e)
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

        private void CheckBox_Negro_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (CheckBox_Negro.CheckState != CheckState.Checked) CheckBox_Negro.CheckState = CheckState.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Azul_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (CheckBox_Azul.CheckState != CheckState.Indeterminate) CheckBox_Azul.CheckState = CheckState.Indeterminate;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Rojo_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (CheckBox_Rojo.CheckState != CheckState.Unchecked) CheckBox_Rojo.CheckState = CheckState.Unchecked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Amarillo_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (CheckBox_Amarillo.CheckState != CheckState.Checked) CheckBox_Amarillo.CheckState = CheckState.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Gris_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (CheckBox_Gris.CheckState != CheckState.Checked) CheckBox_Gris.CheckState = CheckState.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ListView_Principal_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    ListViewItem Objeto = ListView_Principal.HitTest(e.Location).Item;
                    if (Objeto != null)
                    {
                        Índice_Herramienta = Objeto.Index;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ListView_Principal_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                /*if (e.Button == MouseButtons.Left)
                {
                    ListViewItem Objeto = ListView_Principal.HitTest(e.Location).Item;
                    if (Objeto != null)
                    {
                        Índice_Herramienta = Objeto.Index;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Restablecer_Click(object sender, EventArgs e)
        {
            try
            {
                Índice_Herramienta = -1;
                // Finish this.
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Aleatorizar_Click(object sender, EventArgs e)
        {
            try
            {
                Índice_Herramienta = int.MaxValue;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Índice_Herramienta > -1)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Cancelar_Click(object sender, EventArgs e)
        {
            try
            {
                Índice_Herramienta = -1;
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ListView_Principal_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ListView_Principal.SelectedIndices != null && ListView_Principal.SelectedIndices.Count > 0)
                {
                    foreach (int Índice in ListView_Principal.SelectedIndices)
                    {
                        Índice_Herramienta = Índice;
                    }
                }
                else Índice_Herramienta = -1;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ListView_Principal_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    Índice_Herramienta = -1;
                    this.DialogResult = DialogResult.Cancel;
                    this.Close(); // Since it can be accidentally started from the main window, allow for a fast closing without using the keyboard.
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Temporizador_Principal_Tick(object sender, EventArgs e)
        {
            try
            {
                /*Temporizador_Principal.Stop();
                return;
                if (Objeto_Infiniscopio != null)
                {
                    ListView_Principal.BeginUpdate();
                    Objeto_Infiniscopio.ForeColor = Program.Obtener_Color_Puro_1530(Matiz_Infiniscopio);
                    Matiz_Infiniscopio += 1530 / 12;
                    if (Matiz_Infiniscopio >= 1530) Matiz_Infiniscopio = 0;
                    //ListView_Principal.Invalidate(Objeto_Infiniscopio.GetBounds(ItemBoundsPortion.Entire));
                    ListView_Principal.EndUpdate();
                    //ListView_Principal.Update();
                    //ListView_Principal.Refresh();
                }*/
            }
            catch (Exception Excepción)
            {
                Temporizador_Principal.Stop();
                Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
            }
        }

        private void ListView_Principal_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.None)
                {
                    ListViewItem Objeto = ListView_Principal.HitTest(e.Location).Item;
                    if (Objeto != null)
                    {
                        Índice_Herramienta = Objeto.Index;
                        Etiqueta_Descripción.Text = !string.IsNullOrEmpty(Herramientas.Matriz_Herramientas[Índice_Herramienta].Descripción) ? Herramientas.Matriz_Herramientas[Índice_Herramienta].Descripción : "Tool description not available yet, sorry...";
                    }
                    else Etiqueta_Descripción.Text = "Hover over any tool in the list above to see it's description here...";
                }
                else Etiqueta_Descripción.Text = "Hover over any tool in the list above to see it's description here...";
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
