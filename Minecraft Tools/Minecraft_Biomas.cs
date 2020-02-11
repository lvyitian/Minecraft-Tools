using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Tools
{
    internal static class Minecraft_Biomas
    {
        /// <summary>
        /// Structure that holds up all the information about a thanked person or organization.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Biomas
        {
            /// <summary>
            /// The biome index.
            /// </summary>
            internal int Índice;
            /// <summary>
            /// The original biome name.
            /// </summary>
            internal string Nombre_Original;
            /// <summary>
            /// The biome name.
            /// </summary>
            internal string Nombre;
            /// <summary>
            /// The biome height base.
            /// </summary>
            internal double Altura_Base;
            /// <summary>
            /// The biome height variation.
            /// </summary>
            internal double Altura_Variación;
            /// <summary>
            /// The biome temperature.
            /// </summary>
            internal double Temperatura;
            /// <summary>
            /// The biome humidity.
            /// </summary>
            internal double Humedad;
            /// <summary>
            /// The water color as a 32 bits BGRA value.
            /// </summary>
            internal int Color_Agua;
            /// <summary>
            /// The water color as a Color RGBA value.
            /// </summary>
            internal Color Color_ARGB_Agua;
            /// <summary>
            /// If it's true, it can rain on this biome.
            /// </summary>
            internal bool Lluvia;
            /// <summary>
            /// If it's true, it can snow on this biome.
            /// </summary>
            internal bool Nieve;
            /// <summary>
            /// If the biome index is over 127 then it's a mutation.
            /// </summary>
            internal bool Mutación;
            /// <summary>
            /// The biome color.
            /// </summary>
            internal Color Color_ARGB;

            internal Biomas(int Índice, string Nombre_Original, string Nombre, Color Color_ARGB, double Altura_Base, double Altura_Variación, double Temperatura, double Humedad, int Color_Agua, bool Lluvia, bool Nieve)
            {
                this.Índice = Índice;
                this.Nombre_Original = Nombre_Original;
                this.Nombre = Nombre;
                this.Color_ARGB = Color_ARGB;
                this.Altura_Base = Altura_Base;
                this.Altura_Variación = Altura_Variación;
                this.Temperatura = Temperatura;
                this.Humedad = Humedad;
                this.Color_Agua = Color_Agua;
                this.Color_ARGB_Agua = Obtener_Color_ARGB(Color_Agua);
                this.Lluvia = Lluvia;
                this.Nieve = Nieve;
                this.Mutación = Índice > 127;
            }

            internal static readonly Biomas[] Matriz_Biomas = new Biomas[]
            {
                new Biomas(0, "ocean", "Ocean", Color.FromArgb(0, 0, 112), -1.0, 0.1, 0.5, 0.5, 16777215, true, false), // .................................................................................................
                new Biomas(1, "plains", "Plains", Color.FromArgb(141, 179, 96), 0.125, 0.05, 0.8, 0.4, 16777215, true, false),
                new Biomas(2, "desert", "Desert", Color.FromArgb(250, 148, 24), 0.125, 0.05, 2.0, 0.0, 16777215, false, false),
                new Biomas(3, "extreme_hills", "Extreme Hills", Color.FromArgb(96, 96, 96), 1.0, 0.5, 0.2, 0.3, 16777215, true, false),
                new Biomas(4, "forest", "Forest", Color.FromArgb(5, 102, 33), 0.1, 0.2, 0.7, 0.8, 16777215, true, false),
                new Biomas(5, "taiga", "Taiga", Color.FromArgb(11, 102, 89), 0.2, 0.2, 0.25, 0.8, 16777215, true, false),
                new Biomas(6, "swampland", "Swampland", Color.FromArgb(7, 249, 178), -0.2, 0.1, 0.8, 0.9, 14745518, true, false),
                new Biomas(7, "river", "River", Color.FromArgb(0, 0, 255), -0.5, 0.0, 0.5, 0.5, 16777215, true, false),
                new Biomas(8, "hell", "Hell", Color.FromArgb(255, 0, 0), 0.1, 0.2, 2.0, 0.0, 16777215, false, false),
                new Biomas(9, "sky", "The End", Color.FromArgb(128, 128, 255), 0.1, 0.2, 0.5, 0.5, 16777215, false, false),
                new Biomas(10, "frozen_ocean", "FrozenOcean", Color.FromArgb(144, 144, 160), -1.0, 0.1, 0.0, 0.5, 16777215, true, true),
                new Biomas(11, "frozen_river", "FrozenRiver", Color.FromArgb(160, 160, 255), -0.5, 0.0, 0.0, 0.5, 16777215, true, true),
                new Biomas(12, "ice_flats", "Ice Plains", Color.FromArgb(255, 255, 255), 0.125, 0.05, 0.0, 0.5, 16777215, true, true),
                new Biomas(13, "ice_mountains", "Ice Mountains", Color.FromArgb(160, 160, 160), 0.45, 0.3, 0.0, 0.5, 16777215, true, true),
                new Biomas(14, "mushroom_island", "MushroomIsland", Color.FromArgb(255, 0, 255), 0.2, 0.3, 0.9, 1.0, 16777215, true, false),
                new Biomas(15, "mushroom_island_shore", "MushroomIslandShore", Color.FromArgb(160, 0, 255), 0.0, 0.025, 0.9, 1.0, 16777215, true, false),
                new Biomas(16, "beaches", "Beach", Color.FromArgb(250, 222, 85), 0.0, 0.025, 0.8, 0.4, 16777215, true, false),
                new Biomas(17, "desert_hills", "DesertHills", Color.FromArgb(210, 95, 18), 0.45, 0.3, 2.0, 0.0, 16777215, false, false),
                new Biomas(18, "forest_hills", "ForestHills", Color.FromArgb(34, 85, 28), 0.45, 0.3, 0.7, 0.8, 16777215, true, false),
                new Biomas(19, "taiga_hills", "TaigaHills", Color.FromArgb(22, 57, 51), 0.45, 0.3, 0.25, 0.8, 16777215, true, false),
                new Biomas(20, "smaller_extreme_hills", "Extreme Hills Edge", Color.FromArgb(114, 120, 154), 0.8, 0.3, 0.2, 0.3, 16777215, true, false),
                new Biomas(21, "jungle", "Jungle", Color.FromArgb(83, 123, 9), 0.1, 0.2, 0.95, 0.9, 16777215, true, false),
                new Biomas(22, "jungle_hills", "JungleHills", Color.FromArgb(44, 66, 5), 0.45, 0.3, 0.95, 0.9, 16777215, true, false),
                new Biomas(23, "jungle_edge", "JungleEdge", Color.FromArgb(98, 139, 23), 0.1, 0.2, 0.95, 0.8, 16777215, true, false),
                new Biomas(24, "deep_ocean", "Deep Ocean", Color.FromArgb(0, 0, 48), -1.8, 0.1, 0.5, 0.5, 16777215, true, false),
                new Biomas(25, "stone_beach", "Stone Beach", Color.FromArgb(162, 162, 132), 0.1, 0.8, 0.2, 0.3, 16777215, true, false),
                new Biomas(26, "cold_beach", "Cold Beach", Color.FromArgb(250, 240, 192), 0.0, 0.025, 0.05, 0.3, 16777215, true, true),
                new Biomas(27, "birch_forest", "Birch Forest", Color.FromArgb(48, 116, 68), 0.1, 0.2, 0.6, 0.6, 16777215, true, false),
                new Biomas(28, "birch_forest_hills", "Birch Forest Hills", Color.FromArgb(31, 95, 50), 0.45, 0.3, 0.6, 0.6, 16777215, true, false),
                new Biomas(29, "roofed_forest", "Roofed Forest", Color.FromArgb(64, 81, 26), 0.1, 0.2, 0.7, 0.8, 16777215, true, false),
                new Biomas(30, "taiga_cold", "Cold Taiga", Color.FromArgb(49, 85, 74), 0.2, 0.2, -0.5, 0.4, 16777215, true, true),
                new Biomas(31, "taiga_cold_hills", "Cold Taiga Hills", Color.FromArgb(36, 63, 54), 0.45, 0.3, -0.5, 0.4, 16777215, true, true),
                new Biomas(32, "redwood_taiga", "Mega Taiga", Color.FromArgb(89, 102, 81), 0.2, 0.2, 0.3, 0.8, 16777215, true, false),
                new Biomas(33, "redwood_taiga_hills", "Mega Taiga Hills", Color.FromArgb(69, 79, 62), 0.45, 0.3, 0.3, 0.8, 16777215, true, false),
                new Biomas(34, "extreme_hills_with_trees", "Extreme Hills+", Color.FromArgb(80, 112, 80), 1.0, 0.5, 0.2, 0.3, 16777215, true, false),
                new Biomas(35, "savanna", "Savanna", Color.FromArgb(189, 178, 95), 0.125, 0.05, 1.2, 0.0, 16777215, false, false),
                new Biomas(36, "savanna_rock", "Savanna Plateau", Color.FromArgb(167, 157, 100), 1.5, 0.025, 1.0, 0.0, 16777215, false, false),
                new Biomas(37, "mesa", "Mesa", Color.FromArgb(217, 69, 21), 0.1, 0.2, 2.0, 0.0, 16777215, false, false),
                new Biomas(38, "mesa_rock", "Mesa Plateau F", Color.FromArgb(176, 151, 101), 1.5, 0.025, 2.0, 0.0, 16777215, false, false),
                new Biomas(39, "mesa_clear_rock", "Mesa Plateau", Color.FromArgb(202, 140, 101), 1.5, 0.025, 2.0, 0.0, 16777215, false, false),
                //new Biomas(127, "void", "The Void", , 0.1, 0.2, 0.5, 0.5, 16777215, false, false),
                new Biomas(129, "mutated_plains", "Sunflower Plains", Color.FromArgb(141, 179, 96), 0.125, 0.05, 0.8, 0.4, 16777215, true, false),
                new Biomas(130, "mutated_desert", "Desert M", Color.FromArgb(250, 148, 24), 0.225, 0.25, 2.0, 0.0, 16777215, false, false),
                new Biomas(131, "mutated_extreme_hills", "Extreme Hills M", Color.FromArgb(96, 96, 96), 1.0, 0.5, 0.2, 0.3, 16777215, true, false),
                new Biomas(132, "mutated_forest", "Flower Forest", Color.FromArgb(5, 102, 33), 0.1, 0.4, 0.7, 0.8, 16777215, true, false),
                new Biomas(133, "mutated_taiga", "Taiga M", Color.FromArgb(11, 102, 89), 0.3, 0.4, 0.25, 0.8, 16777215, true, false),
                new Biomas(134, "mutated_swampland", "Swampland M", Color.FromArgb(7, 249, 178), -0.1, 0.3, 0.8, 0.9, 14745518, true, false),
                new Biomas(140, "mutated_ice_flats", "Ice Plains Spikes", Color.FromArgb(140, 180, 180), 0.425, 0.45000002, 0.0, 0.5, 16777215, true, true),
                new Biomas(149, "mutated_jungle", "Jungle M", Color.FromArgb(83, 123, 9), 0.2, 0.4, 0.95, 0.9, 16777215, true, false),
                new Biomas(151, "mutated_jungle_edge", "JungleEdge M", Color.FromArgb(98, 139, 23), 0.2, 0.4, 0.95, 0.8, 16777215, true, false),
                new Biomas(155, "mutated_birch_forest", "Birch Forest M", Color.FromArgb(48, 116, 68), 0.2, 0.4, 0.6, 0.6, 16777215, true, false),
                new Biomas(156, "mutated_birch_forest_hills", "Birch Forest Hills M", Color.FromArgb(31, 95, 50), 0.55, 0.5, 0.6, 0.6, 16777215, true, false),
                new Biomas(157, "mutated_roofed_forest", "Roofed Forest M", Color.FromArgb(64, 81, 26), 0.2, 0.4, 0.7, 0.8, 16777215, true, false),
                new Biomas(158, "mutated_taiga_cold", "Cold Taiga M", Color.FromArgb(49, 85, 74), 0.3, 0.4, -0.5, 0.4, 16777215, true, true),
                new Biomas(160, "mutated_redwood_taiga", "Mega Spruce Taiga", Color.FromArgb(89, 102, 81), 0.2, 0.2, 0.25, 0.8, 16777215, true, false),
                new Biomas(161, "mutated_redwood_taiga_hills", "Redwood Taiga Hills M", Color.FromArgb(69, 79, 62), 0.2, 0.2, 0.25, 0.8, 16777215, true, false),
                new Biomas(162, "mutated_extreme_hills_with_trees", "Extreme Hills+ M", Color.FromArgb(80, 112, 80), 1.0, 0.5, 0.2, 0.3, 16777215, true, false),
                new Biomas(163, "mutated_savanna", "Savanna M", Color.FromArgb(189, 178, 95), 0.3625, 1.225, 1.1, 0.0, 16777215, false, false),
                new Biomas(164, "mutated_savanna_rock", "Savanna Plateau M", Color.FromArgb(167, 157, 100), 1.05, 1.2125001, 1.0, 0.0, 16777215, false, false),
                new Biomas(165, "mutated_mesa", "Mesa (Bryce)", Color.FromArgb(217, 69, 21), 0.1, 0.2, 2.0, 0.0, 16777215, false, false),
                new Biomas(166, "mutated_mesa_rock", "Mesa Plateau F M", Color.FromArgb(176, 151, 101), 0.45, 0.3, 2.0, 0.0, 16777215, false, false),
                new Biomas(167, "mutated_mesa_clear_rock", "Mesa Plateau M", Color.FromArgb(202, 140, 101), 0.45, 0.3, 2.0, 0.0, 16777215, false, false),
                
                //new Biomas(, "", "", , 0.1, 0.2, 0.5, 0.5, 16777215, true, false),
            };
        }

        // These filenames are known to be restricted on one or more OS's.
        //internal static readonly string[] DISALLOWED_FILENAMES = new string[] {"CON", "COM", "PRN", "AUX", "CLOCK$", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"};

        internal static Color Obtener_Color_ARGB(int Color_BGRA)
        {
            try
            {
                byte Alfa = (byte)((Color_BGRA >> 24) & 0xFF); // -16777216 (byte 255).
                byte Rojo = (byte)((Color_BGRA >> 16) & 0xFF);
                byte Verde = (byte)((Color_BGRA >> 8) & 0xFF);
                byte Azul = (byte)(Color_BGRA & 0xFF);
                return Color.FromArgb(Alfa, Rojo, Verde, Azul);
                /*byte Alfa = (byte)(Color_BGRA & 0xFF);
                byte Rojo = (byte)((Color_BGRA >> 8) & 0xFF);
                byte Verde = (byte)((Color_BGRA >> 16) & 0xFF);
                byte Azul = (byte)((Color_BGRA >> 24) & 0xFF);*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return Color.Transparent;
        }
    }
}
