using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Tools
{
    internal static class Registro_Cambios
    {
        /// <summary>
        /// Structure that holds up all the information about a change log entry.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Cambios
        {
            /// <summary>
            /// The date at which a change in the application took place. The hour should be ignored, and the date should represent the exact day the changes were released to the public.
            /// </summary>
            internal DateTime Fecha;
            /// <summary>
            /// The version of the application at which a change took place. Minecraft itself uses strange version increment, since affter 1.9 it should have been 2.0, but instead it was 1.10, so keeping in mind this behavior, this application will be maintained as 1.0 at least until all of it's planned features are fully implemented. So please, use the dates in the change log to know which version is newer.
            /// </summary>
            internal string Versión;
            /// <summary>
            /// The lines of text that describes what changed in the application.
            /// </summary>
            internal string[] Matriz_Líneas;

            internal Cambios(DateTime Fecha, string Versión, string[] Matriz_Líneas)
            {
                this.Fecha = Fecha;
                this.Versión = Versión;
                this.Matriz_Líneas = Matriz_Líneas;
            }

            /// <summary>
            /// Array that contains a registry of the changes done in the application over time.
            /// </summary>
            internal static readonly Cambios[] Matriz_Cambios = new Cambios[]
            {
                /*new Cambios(new DateTime(), "1.0.0.0", new string[]
                {
                    "",
                    ""
                }),*/
                new Cambios(new DateTime(2020, 02, 19), "1.16.0.0", new string[]
                {
                    "Fixed a bug that was adding all the found Minecraft player names to the file \"Players\" each time the program was started.",
                }),
                new Cambios(new DateTime(2020, 02, 16), "1.16.0.0", new string[]
                {
                    "Added a new achievements viewer, and to add that first I fully decoded the Minecraft UUID source code, now ported into this application.",
                }),
                new Cambios(new DateTime(2020, 02, 15), "1.16.0.0", new string[]
                {
                    "I forgot to add the dictionary to convert 1.16+ blocks to 1.12.2- equivalent, so now it's added and world converter should work as expected.",
                    "After testing a full world conversion (20 minutes...), the leaves were despawning, so I changed it's data so they will never despawn.",
                }),
                new Cambios(new DateTime(2020, 02, 14), "1.16.0.0", new string[]
                {
                    "Added a lot of new maps and settings to the new block densities counter.",
                    "Added a new map viewer, still without knowing how to properly convert the 16.384 map bytes into colors.",
                    "Decoded the Minecraft 1.12 source code for reading maps and obtained the 64 colors used in the maps drawing.",
                    "At the end I decoded all the maps format, it was as simple as divide each byte number by 4 to get a value between 0 and 63, and it worked.",
                }),
                new Cambios(new DateTime(2020, 02, 13), "1.16.0.0", new string[]
                {
                    "Improved a lot more the block densities counter by adding rainbow graphs and more detailed information.",
                    "Found that the new Minecraft 1.16 has an unknown biome format for each chunk with 1.024 integers... how it's decoded now?",
                }),
                new Cambios(new DateTime(2020, 02, 12), "1.16.0.0", new string[]
                {
                    "Updated the whole application to fully support the first Minecraft 1.16 snapshot. Now it's the 1.16 version to keep in sync.",
                    "Tried to find the new ancient debris block with the block densities counter inside the realistic 2D world viewer, but it wasn't very good.",
                    "So added a new block densities counter tool, that can load a full region and shows graphically which are the best levels to find any block.",
                }),
                new Cambios(new DateTime(2020, 02, 10), "1.15.2.0", new string[]
                {
                    "Fully removed the tools for changing the username (now only the Windows user name will be used) and the donations received (none so far).",
                    "Improved the exceptions debugger, now it works better and it starts the file a \"JUPISOFT\" so it's never empty (used to keep on GitHub).",
                    "Also cleaned and improved the whole application and added a new group for the most advanced tools.",
                }),
                new Cambios(new DateTime(2020, 02, 09), "1.15.2.0", new string[]
                {
                    "Added a new spiral designer tool, very useful to generate awesome spirals made of slabs. Each color is a different height (half block).",
                }),
                new Cambios(new DateTime(2020, 02, 04), "1.15.2.0", new string[]
                {
                    "Changed the block densities counter so it only counts the Y selected block range and not all from 0 to 255.",
                }),
                new Cambios(new DateTime(2020, 01, 30), "1.15.2.0", new string[]
                {
                    "Added a new option to draw the current cursor on the real time screen filters.",
                    "Changed the window size of the real time screen filters so it's always square, and removing several drawing errors.",
                    "Added the option to copy the screen with an outside border up to 128 pixels, or half of the window, so the cursor is always centered.",
                }),
                new Cambios(new DateTime(2019, 12, 28), "1.15.0.0", new string[]
                {
                    "Finished several functions to generate a Mars simulation resource pack called \"Generar_Pack_Recursos_Marte()\", it worked as expected after several days.",
                    "Added a new image comparator tool that actually compares pixel by pixel a whole image on all it's ARGB channels to see if they are the same one or not.",
                }),
                new Cambios(new DateTime(2019, 11, 06), "1.15.0.0", new string[]
                {
                    "Fully decoded by myself how the PixARK structure files work, so now I'm trying to convert them to a Minecraft world format.",
                    "Added on the tool internal structures exporter the option to export all the existing PixARK structures, even from it's DLC SkyARK.",
                }),
                new Cambios(new DateTime(2019, 10, 22), "1.15.0.0", new string[]
                {
                    "Improved even more the prime numbers finder by adding the numbers and primes per second as well as the average numbers between the last primes.",
                    "Changed the code to only use int values instead of long, so now it's going almost 20 times faster than before.",
                }),
                new Cambios(new DateTime(2019, 10, 21), "1.15.0.0", new string[]
                {
                    "Finished the prime numbers finder, now it can resume the search at any time and it's using a thread.",
                    "Added a new option to draw an image that contains all the found prime numbers in a loop (it took half day to make it right).",
                    "Added several new color arrays with the 12 notes colors and the 8 basic colors and even the 12 regular ones.",
                    "By mistake I managed to color straight and diagonal lines in the primes image with the same color... I'm not sure how I did it, but it's working.",
                }),
                new Cambios(new DateTime(2019, 10, 20), "1.15.0.0", new string[]
                {
                    "Improved a lot the hash generator tool by also adding support to the pixels of the image (if it's a valid image).",
                    "Added a new tool to find prime numbers and store them as 64 bits values in a binary data base.",
                }),
                new Cambios(new DateTime(2019, 10, 19), "1.15.0.0", new string[]
                {
                    "Added the perlin noise generator \"SimplexNoise\" with it's class \"Noise.cs\", might be usfeul for the custom terrain generator.",
                    "Added 2 pre filters and 2 post filters for negative and desaturate, which might give new results to the rest of filters.",
                    "Added a new tool capable of generating several hashes of any file and even from the pixels of any valid image.",
                }),
                new Cambios(new DateTime(2019, 10, 18), "1.15.0.0", new string[]
                {
                    "Added several extra dozens of new awesome filters in real time, like convolutions for focus, blurs, edges, etc.",
                    "Added 2 pre filters and 2 post filters for negative and desaturate, which might give new results to the rest of filters.",
                }),
                new Cambios(new DateTime(2019, 10, 17), "1.15.0.0", new string[]
                {
                    "I've been working without rest in the new PixARK Tools these last weeks.",
                    "Added some new experimental filters to the real time screen filters, now renamed to real time filters.",
                    "Improved the real time filters by reducing the default capture zone size to increase the FPS.",
                    "Added support for windows resizing and maximizing to the real time filters, and moved some of it's controls to it's context menu.",
                    "Added several dozens of new awesome filters in real time, like random JPEG compression or sine waves oscillation.",
                }),
                new Cambios(new DateTime(2019, 10, 07), "1.15.0.0", new string[]
                {
                    "I just made a new tool called \"PixARK Tools\", a bit like this one but for the game PixARK.",
                    "Fixed a few hundreds of bugs where the exception buttons weren't hidden again after pressing F3.",
                    "Also fixed the top most option for the help and about windows shown from the context menus.",
                }),
                new Cambios(new DateTime(2019, 10, 06), "1.15.0.0", new string[]
                {
                    "A bit off-topic I discovered how PixARK world saves are stored, in SQLite 3 format.",
                    "Added arrays that hold the 256 colors also for \"Color\" and \"Pen\", useful for quick access to the common colors for 3 base types.",
                }),
                new Cambios(new DateTime(2019, 10, 04), "1.15.0.0", new string[]
                {
                    "Added a new tool to extract all the known image formats hidden within any file, supports 5 image formats and even full folders with it's subfolders.",
                    "I've just discovered I messed up when exporting the Xbox 360 files with JPEG format, because I was closing the memory stream too soon.",
                    "Changed from 240 to 224 the gray scale tone from the tool selector window for the newly added tools background color.",
                }),
                new Cambios(new DateTime(2019, 10, 02), "1.15.0.0", new string[]
                {
                    "Added the function to fully export the particles image for all the console edition resource packs into multiple textures.",
                    "Fixed the dimensions of the \"conduit_base\" texture.",
                }),
                new Cambios(new DateTime(2019, 09, 27), "1.15.0.0", new string[]
                {
                    "The entity textures are broken for several packs like \"Egyptian Mythology\", so I spent the last days fixing them with new code.",
                    "The dolphin texture was totally broken, but finally is fully repaired and looks as intented for all the packs (in theory).",
                    "Added a new Online file downloader to auto-detect links to the desired file types and download them, like images, sounds, etc.",
                }),
                new Cambios(new DateTime(2019, 09, 24), "1.15.0.0", new string[]
                {
                    "The very intense work from the last weeks (around 16 hours per day) was successfull, 100 % of the Xbox 360 resources were extracted and converted.",
                    "Added the final steps to generate all the resource packs with Minecraft 1.14.4 format, although the Xbox 360 contents were only for Minecraft 1.13.",
                }),
                new Cambios(new DateTime(2019, 09, 22), "1.15.0.0", new string[]
                {
                    "Finally invented a new function that can quickly identify and extract TGA images from the inside of any file, used for the Xbox 360 resources.",
                    "Implemented the new functions to fully extract all the Xbox 360 images from the Minecraft resource packs, even the TGA images... this took months.",
                }),
                new Cambios(new DateTime(2019, 09, 14), "1.15.0.0", new string[]
                {
                    "Added a real deobfuscator in C# for the Minecraft source code decompiled with JD-GUI, this will remap all with the new Mojang mappings.",
                    "The tool didn't work as expected since there are inter-references of other class files, leading to multiple unexpected translation errors.",
                }),
                new Cambios(new DateTime(2019, 09, 10), "1.15.0.0", new string[]
                {
                    "Added auto-loading of mapping URLs for any Minecraft version as well as an automatic download option from Mojang server.",
                }),
                new Cambios(new DateTime(2019, 09, 08), "1.15.0.0", new string[]
                {
                    "Sadly the white kitten has died tonight.",
                }),
                new Cambios(new DateTime(2019, 09, 07), "1.15.0.0", new string[]
                {
                    "Added a new obfuscation mappings converter that inverts the direction of the conversion in the files to deobfuscate the code with ProGuard.",
                    "Sadly the tool designed didn't work as expected, so for now I still don't know how to use Mojang's obfuscation maps.",
                }),
                new Cambios(new DateTime(2019, 09, 04), "1.15.0.0", new string[]
                {
                    "Generated almost 700 textures of 96 x 16 with 6 sides of a 3D cube for all the known blocks.",
                    "Added some chunks of the original 1.12 source code of Minecraft to correctly color the leaves and plants textures.",
                    "Added also the 16 colors for the banners and a function to generate stairs and slabs textures from any block.",
                }),
                new Cambios(new DateTime(2019, 09, 01), "1.15.0.0", new string[]
                {
                    "Finally finished the file encoder and decoder from Minecraft worlds. Now it has a default palette with 128 blocks, which avoid werid errors.",
                    "TODO: add the decoding function and test if it actually worked as expected.",
                }),
                new Cambios(new DateTime(2019, 08, 31), "1.15.0.0", new string[]
                {
                    "Improved a lot the 1.13+ to 1.12.2- world converter, now supports worlds with \".mcr\" regions, prior to MC 1.6.",
                    "Added a new reference for the Perlin Noise library from Unity 3D.",
                    "Added a new tool that can geenrate new 1.12.2- Minecraft worlds with customized terrain not found in Minecraft itself.",
                }),
                new Cambios(new DateTime(2019, 08, 27), "1.14.4.0", new string[]
                {
                    "Updated and moved to a new class all the music albums information about David Maeso, Fratelli Stellari and Jupisoft.",
                }),
                new Cambios(new DateTime(2019, 08, 26), "1.14.4.0", new string[]
                {
                    "Added a new paintings converter tool that can convert the single image called \"paintings_kristoffer_zetterstrand.png\" (1.13.2-) to multiple (1.14+) or viceversa.",
                    "Moved to the top the paintings description text in the paintings viewer tool.",
                }),
                new Cambios(new DateTime(2019, 08, 24), "1.14.4.0", new string[]
                {
                    "Improved the speed of the thumbnails generator by not generating the average color by default, unless activated from it's menu.",
                    "Added several TRNG (True Random Number Generators) in a new tool to see how random the numbers really are.",
                }),
                new Cambios(new DateTime(2019, 08, 11), "1.14.4.0", new string[]
                {
                    "Fixed a serious bug thanks to iaraUM, where the 1.13+ to 1.12.2- world converter was missing unlit redstone torches on any wall, and regular torches and banners as well.",
                    "Now won't ask for a user name the first time unless later manually changed from the help menu. The default name will now be the Windows user name.",
                    "Cleaned a lot the whole application by removing the less used features which includes very big files, like the seceret ones, so now it should be easier to download.",
                    "Fixed a bug where the change of the user name from the tool selector wasn't saved properly like it did from the help menu.",
                    "Converted the magic card guessing tool to a magic number guessing, so all the cards are now gone and the numbers are drawn in real time to save even more space.",
                    "Removed the newly added Score Viewer tool, since on 64 bits it was lagging a lot and it's on a full project on GitHub for 32 bits near this one now.",
                    "Removed also the Júpiter Mauro score viewer to save even more space, now instead of it it goes to the online site, which does all from before and even more cool things.",
                    "Also removed the file with all the scores from Jupisoft, so saved almost 1 MB of extra space.",
                }),
                new Cambios(new DateTime(2019, 08, 08), "1.14.4.0", new string[]
                {
                    "Added inside the thumbnails generator a new option to export the inmages as JPEG with quality 100 (maximum).",
                }),
                new Cambios(new DateTime(2019, 07, 29), "1.14.4.0", new string[]
                {
                    "Sadly the yellow kitten die tonight, so there are still 4 left.",
                    "The number of the octaves where too high by 1, so hopefully now it has been corrected even on the keyboard with 5 octaves.",
                }),
                new Cambios(new DateTime(2019, 07, 26), "1.14.4.0", new string[]
                {
                    "Added a mouse wheel functionality to change the visualizaton of the panel where the mouse cursor is standing, which is now shown on the status bar.",
                    "Updated the score viewer to 64 bits by using the corresponding FFTW library, but it's seems to perform a lot slower since then.",
                    "After a the too slow score viewer it has splitted into a new project on GitHub, and now it's made with 32 bits, so please use that instead.",
                }),
                new Cambios(new DateTime(2019, 07, 25), "1.14.3.0", new string[]
                {
                    "Added a lot of visualizations and settings to the score viewer, now it's even better, soon the settings will be remembered forever in the registry.",
                }),
                new Cambios(new DateTime(2019, 07, 24), "1.14.3.0", new string[]
                {
                    "Finished enough the first release of the score viewer tool, now it even supports a simulated audio input if it fails to record in real time.",
                }),
                new Cambios(new DateTime(2019, 07, 23), "1.14.3.0", new string[]
                {
                    "Added Lissajous curves to show the frequency changes, but it seems to use a lot of CPU.",
                    "Added a direct sample to pixel conversion, which might be used to show low frequencies like a \"kick\", which might be used to detect the tempo of a song.",
                    "2 more kittens have been born from the same mother, a white siamese and one fully yellow."
                }),
                new Cambios(new DateTime(2019, 07, 22), "1.14.3.0", new string[]
                {
                    "3 new kittens have been born, one with mixed black, white and yellow, one siamese a bit gray, and another siamese with mixed gray spots.",
                    "Added from scratch most of the visualizations for the score viewer tool.",
                }),
                new Cambios(new DateTime(2019, 07, 21), "1.14.3.0", new string[]
                {
                    "Added a new core viewer tool, which will be the most powerful audio tool ever made by Jupisoft, sadly for now it needs to be compiled as 32 bits due to the FFTW.",
                }),
                new Cambios(new DateTime(2019, 05, 26), "1.14.3.0", new string[]
                {
                    "TODO: the structure icons aren't loading properly due to changes inside the chunk's NBT structures. Soon this should be fixed forever.",
                    "Replaced the moon image with a new HD image at 640 x 640 pixels for the virtual moon tool.",
                    "Updated the code from the virtual moon tool, now it already works on real time until the year 2100.",
                }),
                new Cambios(new DateTime(2019, 05, 10), "1.14.0.0", new string[]
                {
                    "Replaced the custom light level resource packs generator with a custom light maps, which can even generate mob spawing detector or infinite night vision packs.",
                    "Added 6 new resource pack types, with awesome and unique effects that you probably have never seen before anywhere else.",
                    "Added a new cloudy sky option, which will turn the sky light darker during the day as if the sky was actually cloudy, so the torches are more visible in the day.",
                    "Updated the GitHub Desktop automatic updater, now it's working again as expected, although the external application works quite bad.",
                }),
                new Cambios(new DateTime(2019, 05, 05), "1.14.0.0", new string[]
                {
                    "Created new modular resource packs for Optifine with infinite night vision, mob spawning detection, inverted torch lighting, rainbow torch lighting, etc.",
                    "Fully updated the known blocks with the new Minecraft 1.14 blocks, in this case cut red sandstone slab and cut sandstone slab were still missing.",
                    "Marked as obsolete 11 previously known blocks for Minecraft 1.14.",
                    "Added properly support for drag and drop to the internal structures exporter, now it sets the folder path of the dropped folder or file.",
                    "The 1.14+ worlds were showing igloos structures on every chunk...",
                    "Added support for the new pillager outpost structure detection in the realistic 2D world viewer.",
                }),
                new Cambios(new DateTime(2019, 04, 29), "1.14.0.0", new string[]
                {
                    "Fixed several potential divide by zero exceptions in the Minecraft 1.13+ to 1.12.2- world converter, thanks to \"user-13553372\" from the Minecraft Forum.",
                    "Officially synced the application version with the current Minecraft version 1.14, now it should be easier to see what minimum version it supports.",
                }),
                new Cambios(new DateTime(2019, 04, 23), "1.0.0.0", new string[]
                {
                    "Downloaded the latest Minecraft Xbox 360 Edition Title Update, which is the equivalent for Minecraft 1.13.2 for Java Edition.",
                    "Added a new tool to decode and extract the resources from the Xbox 360 data extracted with Horizon. It will recreate the skin and resource packs for PC.",
                }),
                new Cambios(new DateTime(2019, 04, 21), "1.0.0.0", new string[]
                {
                    "Today is my birthday!",
                    "Improved a lot the double structure prediction tool, now it can copy up to 3 coordinates to teleport in game very quickly.",
                }),
                new Cambios(new DateTime(2019, 04, 20), "1.0.0.0", new string[]
                {
                    "Added a new tool that can predict where 2 structures of the same type might collide and mix as one weird thing.",
                }),
                new Cambios(new DateTime(2019, 04, 19), "1.0.0.0", new string[]
                {
                    "Fixed a Mojang bug where the NBT structure files define slabs as \"half: X\" instead of \"type: X\" like in game (1.13+), so added dual support for those formats.",
                    "Fixed a bug where the wood slabs weren't properly converted (they were replaced by air), and also replaced any double slab with a full regular block.",
                    "Finished the internal NBT structures exporter, it now supports even 1.14+ NBT structures, which are included by default now.",
                    "Added a new tool description text at the bottom of the tool selector, useful to remember what can do any of the tools of this application.",
                    "Fixed a bug where blocks from 1.13 or 1.14+ were replaced by 1.12.2- blocks, the slabs and stairs were loosing it's properties like orientation, etc.",
                }),
                new Cambios(new DateTime(2019, 04, 18), "1.0.0.0", new string[]
                {
                    "Improved a lot the internal NBT structures exporter.",
                    "Replaced the NBT structure files with the latest 1.14 snapshot files, that includes the new pillager outposts and village buildings.",
                }),
                new Cambios(new DateTime(2019, 04, 17), "1.0.0.0", new string[]
                {
                    "Added the 1.13+ to 1.12.2- block converter class for the tool that exports all the valid NBT structures in the \"Structures\" application subfolder as a new world.",
                    "Fixed a bug in the NBT viewer where a list that contains another list wasn't showing it's own node, thanks to the NBT structure files.",
                }),
                new Cambios(new DateTime(2019, 04, 06), "1.0.0.0", new string[]
                {
                    "Improved the resource structure rebuilder, now it can detect if the JSON files have multiple (old format) or a single line of text (new format) and decode it's names.",
                    "Added support for InfDev worlds to the 1.13+ to 1.12.2- world converter, but entities and inventories will still be lost (for now). Play in 1.12+ your old worlds!",
                }),
                new Cambios(new DateTime(2019, 04, 03), "1.0.0.0", new string[]
                {
                    "Added a fonts resource packs generator, capable of replacing the Minecraft fonts by your desired ones, like it was on Minecraft 1.13 pre-release 6.",
                }),
                new Cambios(new DateTime(2019, 03, 29), "1.0.0.0", new string[]
                {
                    "Finally fully understood the light maps for Optifine, the sky and torch colors are added together, so red and green will end up in yellow.",
                    "Idea for a resource pack that shows where mobs are able to spawn, ideal to place torches where needed and to avoid blind spots.",
                    "The grass color (and others) aren't visible at all since the recoloration is red, it's only barely visible, so applying the logarithm filter to all of them.",
                }),
                new Cambios(new DateTime(2019, 03, 27), "1.0.0.0", new string[]
                {
                    "Added 17 optifine versions into the secrets folder, from Minecraft 1.7.10 to 1.13.2, so now they'll be easier to find to anyone interested.",
                    "Added a texture splitter that even supports custom outer and inner borders, so basically it can split sky box textures into individual ones and any other images too.",
                    "Fixed a serious bug by adding the \"Light populated\" value when generating new worlds for 1.13+ support, before the chunks were lost when upgrading to 1.13+.",
                }),
                new Cambios(new DateTime(2019, 03, 26), "1.0.0.0", new string[]
                {
                    "Also discovered that if a custom color is used for the lightmap, then all the blocks have a filter of that color, this might be also useful for something.",
                    "Added a custom light level resource packs generator, with it you can force infinite night vision without particle effects or even pitch black darkness, it has 16 levels.",
                    "Added 16 gray colors used for the 16 light levels in Minecraft but also a custom color used to recolor all blocks in game to any color... the green lava looks cool.",
                }),
                new Cambios(new DateTime(2019, 03, 25), "1.0.0.0", new string[]
                {
                    "Finished the sky box resource packs generator with dozens of new options, like Cube / Sauerbraten and Unity assets rotations support and a smart auto texture loader.",
                    "Trying some settings in the newly generated sky boxes dicovered by chance that it's possible to force a custom light level, even in the caves or other dimensions.",
                }),
                new Cambios(new DateTime(2019, 03, 24), "1.0.0.0", new string[]
                {
                    "Improved a lot the XNA resource extractor tool, now it even supports variable dictionaries for Stardew Valley, and it can extract 2D textures, fonts, sounds, etc.",
                    "Added some libraries and external programs like \"xcompress32.dll\" or \"ffmpeg.exe\" for even better XNA resource extracting and even the XNA 4.0 redistributable.",
                    "Fully removed the sky simulator in 3D and replaced it with a sky box resource packs generator, a lot more useful and functional. It's packs will require Optifine.",
                }),
                new Cambios(new DateTime(2019, 03, 23), "1.0.0.0", new string[]
                {
                    "Upgraded this project to Microsoft Visual Studio 2017 instead of 2015, and now it comes with Unity, so hopefully will be useful for this tool in a future.",
                    "Added a new XNA resource extractor tool, that supports the resources from games like Terraria, Stardew Valley, etc. Don't use it for anything illegal!",
                }),
                new Cambios(new DateTime(2019, 03, 20), "1.0.0.0", new string[]
                {
                    "Added several blocks to ignore when drawing 2D underground cave maps like logs and mushroom blocks.",
                }),
                new Cambios(new DateTime(2019, 03, 13), "1.0.0.0", new string[]
                {
                    "Added a new option to draw the blocks in real 3D in the blocks screen saver, which at least for me uses 0 % of CPU, so it's lag free when the PC is idle.",
                    "Increased the maximum random size of the blocks by 4 when drawing in real 3D mode.",
                    "A 4th kitten was born more than 24 hours after the other 3, gray in color.",
                }),
                new Cambios(new DateTime(2019, 03, 12), "1.0.0.0", new string[]
                {
                    "Improved even more the resource pack converter, now it can adapt images like the particles one, etc.",
                    "Added a new useful seeds registry tool, designed to search a seed that adapts to your needs, it even includes a search function by key words like \"Jungle\" or \"Pyramid\".",
                    "3 new kittens have been born today, one gray, one with mixed white, black and yellow and one with white and black. Happy birthday!",
                    "Changed the draw order for the structure icons in the realistic 2D world viewer, now the spawn will always be on top, then players and other structures on the bottom.",
                    "Fixed a serious bug where if the X or Z where negative, the player or spawn icons were never drawn. Now the negative region and chunk indexes are properly calculated.",
                    "Moved all this changes to a separate class and inverted the array order to add the new changes always at the first line, so the read order now has been inverted.",
                    "Added a new structure icon in the realistic 2D world viewer to represent the center of the world at XZ 0, 0.",
                }),
                new Cambios(new DateTime(2019, 03, 11), "1.0.0.0", new string[]
                {
                    "Finally after a lot more work than expected the resource pack converter should be fully working, at least for the textures folders, this might be now one of the best tools.",
                    "Finished all the zip code for reading and writing in the same function. It worked perfectly at the first try and upgraded a pack from 1.12 to 1.13 without errors.",
                    "Some images in the pack might need a manual fix, because they're missing some new icons like in the particles, etc, but still is better than doing it manually.",
                    "Added theoric support for future pack formats up to level 9, at least one file will retain it's path in the future... hopefully, so it'll still help the users.",
                }),
                new Cambios(new DateTime(2019, 03, 10), "1.0.0.0", new string[]
                {
                    "It took days to do it manually, but it now has a full list of file names of all the pack formats. So the tool is almost ready to start working. I'm exhausted but happy.",
                }),
                new Cambios(new DateTime(2019, 03, 09), "1.0.0.0", new string[]
                {
                    "After hours spent thinking new C# codes, finally the resource pack converter tool has inside some names of all the pack versions (1 to 4), so it's almost ready to work.",
                }),
                new Cambios(new DateTime(2019, 03, 08), "1.0.0.0", new string[]
                {
                    "Finished the 3D block viewer, which now gives to the user full control of everything, from images, rotations, shadows, etc, and it even allows to export all as PNG images.",
                    "Added a few new splashes, and deleted an obsolete one.",
                }),
                new Cambios(new DateTime(2019, 03, 07), "1.0.0.0", new string[]
                {
                    "Added a new 3D block viewer tool that simulates how Minecraft shows most solid blocks in it's menus and hotbar, but it even allows to generate custom blocks in 3D.",
                }),
                new Cambios(new DateTime(2019, 03, 04), "1.0.0.0", new string[]
                {
                    "Added 3 new resource packs in the secrets tool, HD real paintings, HD Monster High paintings and HD real sky (based on another pack), all for MC 1.12.2- and 1.13+.",
                }),
                new Cambios(new DateTime(2019, 03, 03), "1.0.0.0", new string[]
                {
                    "Added a sky simulator in 3D, but since it only used CPU, it's source code was removed until it can be improved and go faster. Hopefully some day it will be back.",
                    "Added a new tool that guesses any card you thought, so it's a real magic trick but inside a virtual application... how cool is that. The trick is also explained in detail.",
                    "Changed a lot the tool selector, which now has a lot of groups, and even moved the unfinished tools to the bottom group, now it looks more professional and clear.",
                    "Added in the code a class called \"_Todo_\", whose summary includes a list of several future ideas for this application, useful to avoid forgetting things.",
                    "Added a new virtual moon tool, with phases and eclipse dates up to the year 3.000, but it onmly shows up to 2.100 to save memory and time, it's very accurate.",
                }),
                new Cambios(new DateTime(2019, 03, 02), "1.0.0.0", new string[]
                {
                    "Improved the tool selector, that now supports more colors to quickly identify the categories and states of all the tools.",
                    "Added 3 new options to export the images as PNG with 2, 16 or 256 colors inside the palette in the thumbnails and average color generator.",
                }),
                new Cambios(new DateTime(2019, 02, 26), "1.0.0.0", new string[]
                {
                    "Added support to drag and drop any image to filter it and auto-export a modified copy to the user's desktop in the real time screen filters tool, so now it's more useful.",
                }),
                new Cambios(new DateTime(2019, 02, 25), "1.0.0.0", new string[]
                {
                    "Discovered a lot of things about the multidimensional mathematical analyzer tool and why it creates fractals.",
                    "The first power (dimension) of any base number produces a diagonal line, the second a diagonal rhombus, the third a perfect circle, at least in base 2.",
                    "Each dimension seems to include the previous geometric figures mixed with a new one... what's beyond a circle in our 3rd dimension?",
                    "Tried to arrive to the 4th dimension, half-success on it, but it looked like a 4 x 4 modified copy of the 3rd dimension. I might be wrong though...",
                    "Technically the dimension or power 0 it's a single point, since any power of zero gives 1.",
                    "Couldn't fully arrive to 4th dimension because the larger image .NET can create only has 26.754 x 26.754 pixels, and it needs 65.536 x 65.536 pixels.",
                    "The 5h dimension would need an image of 4.294.967.296 x 4.294.967.296 pixels, which can't be achieved today sadly, but I'll think of some way to get there.",
                    "Combining the base 2 and 16 is like rotating only the base 2 and then applying to it a vertical flip, which can turn circles into squares and viceversa.",
                    "Discovered the \"quadration of the circle\" mentioned thousands of years ago, not sure if it was that, but for sure I've discovered that with bases 2 and 16.",
                    "Discovered that using reversed base figures, the numbers seem to have perfect geometry, even for perfect circles.",
                    "Using the power of 2 each time turns it in 2 square dimensions (2D), so it naturally has at least between 1/4 and 2/4 of natural symmetry in the same numbers.",
                    "Conclusions: this needs further investigation by experts, because it could generate perfect geometric figures (like circles), without decimal calculations, using Pi, etc.",
                    "Added a new filter for the real time screen filters called \"Differences over time\", which can show what pixels change over time, use it on video or as movement detector.",
                }),
                new Cambios(new DateTime(2019, 02, 24), "1.0.0.0", new string[]
                {
                    "Added 10 new filters to the real time screen filters tool called \"Minimum difference for JPEG\", designed to supress any JPEG or compression noise while watching videos.",
                    "Changed the filters in code to make it easier to add new filters and to auto-translate their names properly.",
                    "Changed the method that zooms the filtered images, so now the filters won't be affected by the selected zoom like before.",
                    "Added another smooth zoom option, as a middle way between the square and circle. Virtual lava lamp: variable termography filter, zoom at 128x and use smoothing.",
                    "Added 2 new ways of drawing the fractals in the multidimensional mathematical analyzer tool, but the fractals are hidden with them, which is very strange...",
                }),
                new Cambios(new DateTime(2019, 02, 18), "1.0.0.0", new string[]
                {
                    "Improved a lot the multidimensional mathematical analyzer tool, now it also includes the full source code to generate fractals.",
                    "Added a dark mode on the main window, inside it's view menu, with it you'll see everything with an inverted color scheme.",
                }),
                new Cambios(new DateTime(2019, 02, 13), "1.0.0.0", new string[]
                {
                    "A few days ago my Minecraft Forum account was deleted and all my posts are gone forever after reaching almost 20.000 visits. It got hacked or what happened here?",
                    "Now this application has a new thread and full support for all the Minecraft 1.14 snapshot 19w06a new block types and textures.",
                    "Added a new tool that fully describes the new Minecraft 1.13+ new chunk format. This should help others to update their applications to support Minecraft 1.13+ worlds.",
                    "Updated the tool able to reconstruct the resource structure with original file names from any Minecraft folder, now supports JSON files with a single line of code.",
                    "Added a new screen saver tool that randomly draws all the known Minecraft blocks by this application on the screen with random options, but also configurable.",
                    "Added an option to auto-install the screen saver near the other Windows screen savers, but first saves the files to the desktop and then tries to move them.",
                    "Now the screen saver remembers it's options when it's loaded, useful to set it up from the main application and then use the same options in the real screen saver.",
                    "Added a new Score viewer with all the scores composed by Júpiter Mauro (Jupisoft), also available for free on the music site at: http://jupisoft.x10host.com/.",
                    "Saved almost 11 MB of space by simply removing the Jupisoft icons from the designer of all the forms except the main and the blocks screen saver.",
                    "Added several images to draw over them in real time all the scores composed by Jupisoft and also implemented advanced FSC reading functions and custom classes.",
                    "Added all the scores composed by Jupisoft inside the application into custom classes for later use by some tools, but in a way that should save a lot of space.",
                    "Finished the score viewer tool, now the user can even rotate the notes in real time, and the result is better than my own music site, but without any space used.",
                    "Now the score viewer can play the scores as MIDI files, and it has real time positions and markers to follow along the playback with high accuracy.",
                    "Added a new \"infiniscope\" tool, with instructions to develop an ancient device better than a colossal telescope and microscope, please anyone interested get in contact.",
                    "Added a new tool capable of repairing damaged files, if you download any file that ends up corrupted 3 times at different places, you'll get a flawless version with this.",
                    "Added a new multidimensional mathematical analyzer tool, capable of generating fractals and other awesome effects, which needs further investigation by someone.",
                    "Added the secrets window to the tool selector window, making it less secret than before. Added a yellow color for tools with secrets like the infiniscope tool or secrets.",
                }),
                new Cambios(new DateTime(2019, 01, 21), "1.0.0.0", new string[]
                {
                    "Reduced a lot the total size of the application, and now the \"secret files\" won't be included by default to save even more space.",
                    "Added a new tool that allows to see the screen in real time but through several useful filters, including zoom, try it diving underwater inside a shipwreck in Minecraft.",
                }),
                new Cambios(new DateTime(2018, 10, 21), "1.0.0.0", new string[]
                {
                    "Added a global and much better \"pixel art\" world option for 1.13+ to 1.12.2-, so now the worlds will turn into wool, concrete, etc much faster than before.",
                    "Added a new function for custom block transmutations that will turn any block type into another one, allowing even for block type repetitions.",
                    "Changed the block textures for Ender dragon egg, end rods, sea pickles, flower pots and levers to make them look like they look in Minecraft.",
                    "Moved some obsolete blocks to the bottom of the blocks list and properly set the blocks with any dimensions different than 1 full block.",
                    "Improved a lot the 1.13+ to 1.12.2- world converter, now with block transmutations, quantizations, upside down worlds and even with self-destruct TNT columns.",
                    "Planned to add full 1.12.2- to 1.12.2- support in order to edit or filter the worlds and change it's blocks with transmutations, quantizations and other options.",
                    "Added quantization with support for blocks with partial (or full) transparent textures, like stained glass, etc.",
                    "Changed a bit some blocks average texture color in order to fully support qunatization with variable alpha values.",
                    "Received the first donation ever, thanks to Alexander, so now a received donations window has been fully implemented from the help menu.",
                    "Added a new window for selecting custom transmutations or quantizations with a lot of interesting options.",
                    "Adapted correctly the water and lava spreading level to it's Data values from 1.13+ to 1.12.2- and also corrected the orientation of all the anvils.",
                    "Improved this change log window, now it shows the updates in reversed order and it auto-scrolls to the bottom of the text when it starts.",
                    "Added a lot of new splashes.",
                    "Changed the tool selector window, so now saving your default start tool will work again as it used to do.",
                    "Readapted the code in the about window, since the previous update to alpha in all the images from the resources made a mess in the Jupisoft image there.",
                    "Finally started the development of the animated 3D skin viewer tool, although still will take a while until it's working properly.",
                    "Added the ability to export and import block transmutations and quantizations as plain text files in the 1.13+ to 1.12.2- world converter.",
                    "Corrected a lot of block ID and Data conversions like beds, buttons, observers, trapdoors, pressure plates, slabs, stairs, heads, all logs, etc.",
                    "Corrected also the Data values from the known vanilla blocks for bricks, nether bricks and quartz blocks. Before they were like it's slabs variants.",
                    "WARNING: the image \"DataValuesBeta\" from the wiki has a lot of wrong Data values, like dark and prismarine bricks (inverted), quartz and nether brick slabs, etc.",
                    "Some blocks that need a TileEntity (like banners, heads or shulker boxes) might be invisible after the conversion. Use \"pick block\" on their hit box to show them again.",
                    "Added some new functions in the paintings viewer to export resource pack with the real HD paintings inside with support for multiple pack formats.",
                    "Added a full internal wiki about all the Monster High characters with cool images, although some are missing (like the Dracubecca, Cupid, etc).",
                    "Almost added a new secret to export a Monster High resource pack with paintings at 512 x 512 pixels about Monster High.",
                    "Added a lot of structure markers for the Realistic 2D world viewer that show the players, the spawn point and even the subtypes of all the structures.",
                    "Improved the blocks viewer tool with new combinable filters, very useful to search block types.",
                    "Finished the encoder and decoder of files to (and from) Minecraft worlds, now with an improved 1.12.2- 256 blocks palette, where 1 block equals 1 byte of the file.",
                    "Added full support for all the new Minecraft 1.14 (Snapshot 18w43c) block types, including it's textures but seen in the old original Minecraft textures style.",
                    "Fixed a bug for the Realistic 2D world viewer where it started at 2 when counting the block densities, while it should only have added 1 for each block type.",
                    "Updated the 1.13+ to 1.12.2- world converter, which now has full 1.14+ new block types support.",
                    "Added the average conversion times for each chunk and region in the 1.13+ to 1.12.2- world converter.",
                    "Fixed a bug where the note block sounds, now saved in a folder near the application, weren't loaded correctly, so the tool was muted and useless with that bug.",
                    "Added the colors for the new 1.13 biomes, the difference will be shown on the realistic 2D world viewer.",
                    "Fixed a bug where the map wasn't fully drawn (if that option was selected) after loading a different world on the realistic 2D world viewer.",
                    "Expanded the slime chunks finder, now it can predict where will spawn all Minecraft structures, assuming the world had only one correct biome (use the world buffet).",
                    "Also added a biome list to exactly predict any structure and improved a lot the map colors and rulers, even it's numeric font, now with a better background blending.",
                    "Added an option in the context menu to invert the map colors in the new slime chunks and structures finder.",
                    "Created the first datapack by Jupisoft, with new recipes to craft any monster egg and even spawners (with 10+ nether stars, so it's very hard, but possible to get).",
                    "Added a new map type to detect floating blocks that should fall or be destroyed when updated, like generated floating sand or grass with air below.",
                    "Since the VBO switch is no longer included in the latest snapshots and my Intel HD Graphics makes Minecraft crash after loading any world, I can't play or test it.",
                    "I still can for some reason load a debug world and watch from very far the existing blocks to at least add theoric support for them, but without testing like before sadly.",
                    "Added support for the Minecraft 1.14 Snapshot 18w49a, at least in theory, so hopefully everything will work fine.",
                    "Added a new tool that can really count the FPS at which any program window is drawing on the screen, but it might not work on DirectX windows or so.",
                    "Added a new tool that can find duplicated files inside any folder and even move them inside new subfolders to save a lot of time.",
                    "Added inside the infiniscope tool a picture from Jonathan Swift's Gulliver's travels Laputa and from Steven Universe soundtrack vol. 1 that looks like Laputa.",
                    /*"",
                    ""*/
                }),
                new Cambios(new DateTime(2018, 10, 11), "1.0.0.0", new string[]
                {
                    "Added a lot of the old Minecraft splashes, even from it's other versions.",
                    "Added more than 500 custom splashes, making the Minecraft original ones to be hidden for now.",
                    "Added a new color and shadow to the splash texts.",
                    "Added a new tool for converting 1.13+ worlds to 1.12.2-, meaning some blocks won't be there yet, but might be very useful for several purposes.",
                    "Improved the block information viewer, now middle clicking will copy it's 1.13+ name and now it can sort the blocks with it's names inverted, also the ID now works.",
                    "Recolored the texture \"minecraft_melon_stem.png\" because by mistake it was on grayscale.",
                    "Now pressing a letter between A and Z will go to the first block starting with that letter on the block information viewer.",
                    "Added a lot of new functions to help in the conversion from 1.13+ to 1.12.2-. Also done the first successful conversion.",
                    "Added for each block it's Minecraft category tag from the creative inventory, so now they will be able to be sorted in the same groups like Minecraft.",
                    "Added a new window to see all the donations to Jupisoft, but at least for now no one has ever donated yet, so the new window is empty.",
                    "Added a list with all the possible NBT properties of all the Minecraft 1.13.1 blocks, very useful for the new 1.12.2- conversor or just for learning them.",
                    "Improved even more the block information viewer, now when pressing a letter it will navigate on the first or last block starting with it, but from the selected column.",
                    "Added full support for the 37 new blocks from Minecraft 1.13.1, including it's new textures and average colors, meaning the Realistic 2D world viewer will support them.",
                    "Found a few hundreds of images in the resources that weren't 32 bits with alpha, so now all have been reconverted properly into the specified format.",
                    "Finished a working version of the 1.13+ to 1.12.2- world converter, and is better than expected, even signs save it's messages.",
                    "Fixed dozens of incorrect block metadata (Data values), like orientation, state, etc. But it might be more blocks with incorrect conversion yet.",
                    "Added 4 test options to delete the original world water, lava, stones and dirt, which might be very interesting and dangerous because of the falling chain reactions.",
                    "The source code of the last tool could be used to port back levels from 1.13+ to Java edition, but also to other platforms with a bit of recoding and adapting.",
                    "Now the splash count text (without counting itself) will only show once at the start of the application instead of also randomly.",
                    "Added full support for the Nether and The End dimensions in the 1.12.2- converter and also a lot of awesome and cool test options, so check them out.",
                    "Added special world types like wool, concrete or ores for the 1.13+ to 1.12.2- converter, which finally is fully functional, but super slow.",
                }),
                new Cambios(new DateTime(2018, 10, 4), "1.0.0.0", new string[]
                {
                    "Deleted the old background image based on a Minecraft launcher file called \"bg.png\", which has inconsistency errors on the oversized pixel borders.",
                    "Added a new function called \"Crear_Imagen_Mosaico_Fondo()\" which generated a new background image to be displayed as a mosaic, based on the dirt block texture.",
                    "The new background image should be better mixed with the red background now and show colors nearer to red on most displays (before it was too orange).",
                    "Started a new resource pack converter tool, which will convert resource packs (or the old texture packs) from any Minecraft version to another.",
                    "Deleted all the tools from it's menu on the main window, now they'll be started from the new tool selector, which has been mostly finished.",
                    "Deleted about 300 png images from the resources containing ASCII characters, because they were repeated after the previous update.",
                    "Added a new function to delete a certain color of any image, used for making transparent the villager trade chart from the Minecraft wiki.",
                    "Finished (for now) the tool Villager tradings viewer, which has an enhanced version of the image from the Minecraft wiki, now with multiple background colors.",
                }),
                new Cambios(new DateTime(2018, 9, 27), "1.0.0.0", new string[]
                {
                    "The application will only be updated through GitHub from now on, although the Minecraft Forum will be updated with the new changes as well.",
                    "Added the ability to show which chunks are slime chunks for making farms on the Realistic 2D world viewer, shown as transparent black and pink diagonal lines.",
                    "Fixed a bug for the Realistic 2D world viewer where it failed when drawing a full world at once by missing several regions, now it has a full new code to do it right.",
                    "Planned a new tool to convert resource packs for 1.12+ to 1.13+, since several folders and files now have other names.",
                    "Finally finished the full code that shows all the ASCII characters that Minecraft supports, shown at the bottom of the main window as splash texts.",
                    "Added a new icon for the Realistic 2D world viewer combining the Minecraft 3D grass block with a semi-transparent magnifying glass.",
                    "The tool selector window now contains almost all the tools included, and although still can't start them, it's almost finished.",
                    "Fully redesigned the main window, which now shows several images from the Minecraft launcher itself and useful links to Minecraft and Mojang sites.",
                    "Added this change log as a new window, and it took several hours to recover all the previous dates and changes."
                }),
                new Cambios(new DateTime(2018, 9, 16), "1.0.0.0", new string[]
                {
                    "The application was first ever published on GitHub, after learning it can't upload files over 50 MB.",
                    "Added on the readme an external link on Mediafire for the 26.601 real Minecraft skins, since it can't be uploaded on GitHub.",
                    "Also it can't upload more than 100 files at once, so for the thousands of images and resources it took several days.",
                    "Most of the unfinished tools are now working, although there's a lot of work to do on them yet before saying they are fully finished."
                }),
                new Cambios(new DateTime(2018, 3, 17), "1.0.0.0", new string[]
                {
                    "The application was first ever published on the Minecraft Forum.",
                    "After the positive response anyone that said anything about the program was added to the Thank you window.",
                    "Also the good feedback helped a lot to inspire and develop it even more quickly.",
                    "During the next weeks several new unfinished tools were added as well as some were fully finished.",
                    "A few days later it's full source code was released for free.",
                    "A few months later it's development was very slow due to the lack of time to program it, so sorry about that."
                }),
                new Cambios(new DateTime(2018, 3, 8), "1.0.0.0", new string[]
                {
                    "After decoding the new Minecraft 1.13 chunk format this application was born.",
                    "Some kind of old 2D world viewer was created some years ago, but it was abandoned at the end, it worked up to Minecraft 1.12.2, but still was unfinished.",
                    "The new idea was to save that old program, improve it and make an all-in-one free tool.",
                    "The plan wasn't release it so quick, but after decoding the new chunk format it just happened.",
                    "The application was first ever shown to Xisumavoid, which made a post about it on reddit in the hope of spreading the new 1.13 chunk format decoding.",
                    "This should help the whole Minecraft community and help others to update their tools for Minecraft 1.13 faster.",
                    "It only included the Realistic 2D world viewer, but it was possibly the first in the world to fully support the new 1.13 chunk format.",
                    "Dozens of other full tools were planned but due to the fast and unexpected release, they were unfinished."
                })
            };
        }
    }
}
