using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Tools
{
    /// <summary>
    /// Class designed to convert any known 1.13+ block to a 1.12.2- block with ID and Data bytes. Generated with a modified copy of the class "Ventana_Conversor_Mundos_1_13_a_1_12_2" (of this same application).
    /// </summary>
    internal static class Minecraft_Bloques_1_13_a_1_12
    {
        // Temporary variable fix.
        internal static int Variable_Dimensión_Overworld = 0;
        internal static int Variable_Dimensión_Nether = 1;
        internal static int Variable_Dimensión_The_End = 2;
        internal static int Variable_Biomas = 0;
        internal static int Variable_Luz = 16;
        internal static bool Variable_Mundo_Invertido = false;
        internal static bool Variable_Mundo_Invertido_Suelo = false;
        internal static bool Variable_Auto_Destrucción = false;
        internal static bool Variable_Mundo_Agua = false;
        internal static bool Variable_Mundo_Lava = false;

        /// <summary>
        /// Dictionary used to change block types into another block types (like lava into water or netherrack to diamond ore). Any block conversion is valid with this, even converting different block types to the same one, like all the ores to stone.
        /// </summary>
        internal static SortedDictionary<string, string> Diccionario_1_13_a_1_12_2 = null;

        /*internal static byte Traducir_Bloque_1_13_a_1_12(string Nombre_1_13, List<string> Lista_Propiedades, out string Nombre_1_12_2, out byte Data)
        {
            Nombre_1_12_2 = Nombre_1_13;
            Data = 0; // Default block state (ranges from 0 to 15).
            try
            {
                // Avoid null or unknown 1.13 block names or properties.
                if (string.IsNullOrEmpty(Nombre_1_13) || !Minecraft.Diccionario_Bloques_Nombres_Índices.ContainsKey(Nombre_1_13)) Nombre_1_13 = "minecraft:air"; // Never will be unknown.
                if (Lista_Propiedades == null) Lista_Propiedades = new List<string>(); // Never will be null.

                byte ID = 0; // Defaults to air.

                // First replace the 1.13+ missing blocks on 1.12.2- with relatively similar blocks.
                Reiniciar_Diccionario_1_13_a_1_12_2();
                if (Diccionario_1_13_a_1_12_2.ContainsKey(Nombre_1_13))
                {
                    Nombre_1_12_2 = Diccionario_1_13_a_1_12_2[Nombre_1_13];
                }
                //Nombre_1_12 = Reemplazar_Bloques_Minecraft_1_13(Nombre_1_13);

                if (string.Compare(Nombre_1_13, Nombre_1_12_2, true) != 0)
                {
                    Lista_Propiedades.Clear(); // Avoid setting wrong properties if the block has changed.
                }

                // Now convert the adapted 1.13+ Minecraft name to an internal index of this program.
                short ID_Minecraft_1_13 = Minecraft.Bloques.Diccionario_Nombre_Índice[Nombre_1_12_2];

                // Then search if that index can be converted to 1.12.2- ID and Data values.
                foreach (KeyValuePair<short, short> Entrada in Minecraft.Diccionario_Bloques_Índices_1_12_2_a_Índices_1_13)
                {
                    if (Entrada.Value == ID_Minecraft_1_13)
                    {
                        ID = Minecraft.Obtener_Valores_ID_Data(Entrada.Key, out Data);
                        //if (ID != 43 && ID != 44 && ID != 125 && ID != 126 && ID != 181 && ID != 182) // Ignore all the slabs.
                        {
                            break;
                        }
                    }
                }

                //if (Lista_Propiedades != null && Lista_Propiedades.Count > 0) // Always check it.
                {
                    // Finally adapt the Data value with it's found properties, so it can rotated, etc.
                    ID = Obtener_ID_Data_Bloque_Ajustados(Nombre_1_12_2, Lista_Propiedades, ID, Data, out Data);
                }

                return ID; //Return the ID and Data values generated after being fully adapted.

                // If we are here, something went wrong and Air will replace a full type of block.
                // So if you expected a block and see Air, the conversion might have failed in here.



            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return 0; // Defaults to air.
        }*/

        internal static void Reiniciar_Diccionario_1_13_a_1_12_2()
        {
            try
            {
                /*string pp = null;
                foreach (Minecraft.Bloques Bloque in Minecraft.Bloques.Matriz_Bloques)
                {
                    if (Bloque.Lista_ID == null || Bloque.Lista_ID.Count <= 0)
                    {
                        pp += "Diccionario_1_13_a_1_12_2.Add(\"" + Bloque.Nombre_1_13 + "\", \"\");\r\n";
                    }
                }
                Clipboard.SetText(pp);
                return;*/

                if (Diccionario_1_13_a_1_12_2 == null || Diccionario_1_13_a_1_12_2.Count <= 0)
                {
                    Diccionario_1_13_a_1_12_2 = new SortedDictionary<string, string>();
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_bark", "");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_button", "minecraft:oak_button");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_pressure_plate", "minecraft:oak_pressure_plate");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_trapdoor", "minecraft:oak_trapdoor");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_wood", "minecraft:acacia_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:attached_melon_stem", "minecraft:melon_stem");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:attached_pumpkin_stem", "minecraft:pumpkin_stem");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:banner", "minecraft:white_banner");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:birch_bark", "");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:birch_button", "minecraft:oak_button");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:birch_pressure_plate", "minecraft:oak_pressure_plate");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:birch_trapdoor", "minecraft:oak_trapdoor");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:birch_wood", "minecraft:birch_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:black_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:blue_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:blue_coral", "minecraft:blue_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:blue_coral_fan", "minecraft:blue_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:blue_coral_plant", "minecraft:blue_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:blue_dead_coral", "minecraft:light_gray_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:blue_ice", "minecraft:light_blue_concrete");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:brain_coral", "minecraft:pink_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:brain_coral_block", "minecraft:pink_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:brain_coral_fan", "minecraft:pink_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:brain_coral_wall_fan", "minecraft:pink_stained_glass_pane");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:brown_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bubble_column", "minecraft:water");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bubble_coral", "minecraft:purple_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bubble_coral_block", "minecraft:purple_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bubble_coral_fan", "minecraft:purple_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bubble_coral_wall_fan", "minecraft:purple_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:cave_air", "minecraft:air");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:conduit", "minecraft:beacon");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:creeper_wall_head", "minecraft:creeper_head");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:cyan_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_bark", "");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_button", "minecraft:oak_button");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_pressure_plate", "minecraft:oak_pressure_plate");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_trapdoor", "minecraft:oak_trapdoor");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_wood", "minecraft:dark_oak_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_prismarine_slab", "minecraft:nether_brick_slab");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dark_prismarine_stairs", "minecraft:nether_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_brain_coral", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_brain_coral_block", "minecraft:light_gray_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_brain_coral_fan", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_brain_coral_wall_fan", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_bubble_coral", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_bubble_coral_block", "minecraft:light_gray_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_bubble_coral_fan", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_bubble_coral_wall_fan", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_fire_coral", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_fire_coral_block", "minecraft:light_gray_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_fire_coral_fan", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_fire_coral_wall_fan", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_horn_coral", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_horn_coral_block", "minecraft:light_gray_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_horn_coral_fan", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_horn_coral_wall_fan", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_tube_coral", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_tube_coral_block", "minecraft:light_gray_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_tube_coral_fan", "minecraft:light_gray_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dead_tube_coral_wall_fan", "minecraft:light_gray_stained_glass_pane");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:dragon_wall_head", "minecraft:dragon_head");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dried_kelp_block", "minecraft:green_concrete");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:fire_coral", "minecraft:red_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:fire_coral_block", "minecraft:red_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:fire_coral_fan", "minecraft:red_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:fire_coral_wall_fan", "minecraft:red_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:four_turtle_eggs", "minecraft:white_stained_glass_pane");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:gray_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:green_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:horn_coral", "minecraft:yellow_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:horn_coral_block", "minecraft:yellow_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:horn_coral_fan", "minecraft:yellow_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:horn_coral_wall_fan", "minecraft:yellow_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:item_frame", "minecraft:brown_stained_glass_pane");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_bark", "");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_button", "minecraft:oak_button");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_pressure_plate", "minecraft:oak_pressure_plate");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_trapdoor", "minecraft:oak_trapdoor");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_wood", "minecraft:jungle_planks");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:kelp", "minecraft:lime_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:kelp_plant", "minecraft:lime_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:kelp_top", "minecraft:lime_stained_glass_pane");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:light_blue_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:light_gray_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:lime_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:magenta_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:melon_block", "minecraft:melon");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:mob_spawner", "minecraft:spawner");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:mushroom_stem", "minecraft:brown_mushroom_block");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:oak_bark", "");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:oak_wood", "minecraft:oak_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:orange_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:pink_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:pink_coral", "minecraft:pink_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:pink_coral_fan", "minecraft:pink_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:pink_coral_plant", "minecraft:pink_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:pink_dead_coral", "minecraft:light_gray_stained_glass");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:player_wall_head", "minecraft:player_head");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:portal", "minecraft:nether_portal");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_brick_slab", "minecraft:stone_brick_slab");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_brick_stairs", "minecraft:stone_brick_stairs");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_bricks_slab", "minecraft:stone_brick_slab");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_bricks_stairs", "minecraft:stone_brick_stairs");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_slab", "minecraft:cobblestone_slab");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_stairs", "minecraft:cobblestone_stairs");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:pumpkin", "minecraft:carved_pumpkin");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:purple_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:purple_coral", "minecraft:purple_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:purple_coral_fan", "minecraft:purple_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:purple_coral_plant", "minecraft:purple_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:purple_dead_coral", "minecraft:light_gray_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_coral", "minecraft:red_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_coral_fan", "minecraft:red_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_coral_plant", "minecraft:red_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_dead_coral", "minecraft:light_gray_stained_glass");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:redstone_wall_torch", "minecraft:redstone_torch");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:sea_grass", "minecraft:green_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:sea_pickle", "minecraft:sea_lantern");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:seagrass", "minecraft:green_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:shulker_box", "minecraft:purple_shulker_box");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:skeleton_wall_skull", "minecraft:skeleton_skull");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_bark", "");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_button", "minecraft:oak_button");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_pressure_plate", "minecraft:oak_pressure_plate");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_trapdoor", "minecraft:oak_trapdoor");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_wood", "minecraft:spruce_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_acacia_log", "minecraft:acacia_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_acacia_wood", "minecraft:acacia_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_birch_log", "minecraft:birch_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_birch_wood", "minecraft:birch_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_dark_oak_log", "minecraft:dark_oak_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_dark_oak_wood", "minecraft:dark_oak_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_jungle_log", "minecraft:jungle_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_jungle_wood", "minecraft:jungle_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_oak_log", "minecraft:oak_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_oak_wood", "minecraft:oak_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_spruce_log", "minecraft:spruce_planks");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:stripped_spruce_wood", "minecraft:spruce_planks");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:tall_sea_grass", "minecraft:green_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:tall_seagrass", "minecraft:green_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:three_turtle_eggs", "minecraft:white_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:tube_coral", "minecraft:blue_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:tube_coral_block", "minecraft:blue_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:tube_coral_fan", "minecraft:blue_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:tube_coral_wall_fan", "minecraft:blue_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:turtle_egg", "minecraft:white_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:two_turtle_eggs", "minecraft:white_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:void_air", "minecraft:air");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:wall_banner", "minecraft:white_banner");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:wall_torch", "minecraft:torch");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:white_bed", "minecraft:red_bed");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:wither_skeleton_wall_skull", "minecraft:wither_skeleton_skull");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:yellow_bed", "minecraft:red_bed");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:yellow_coral", "minecraft:yellow_stained_glass");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:yellow_coral_fan", "minecraft:yellow_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:yellow_coral_plant", "minecraft:yellow_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:yellow_dead_coral", "minecraft:light_gray_stained_glass");
                    //Diccionario_1_13_a_1_12_2.Add("minecraft:zombie_wall_head", "minecraft:zombie_head");

                    // Add support for all the new Minecraft 1.14 (Snapshot 18w43c) new block types:
                    Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_sign", "minecraft:sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:acacia_wall_sign", "minecraft:wall_sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:andesite_slab", "minecraft:cobblestone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:andesite_stairs", "minecraft:cobblestone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:andesite_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bamboo", "minecraft:lime_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bamboo_sapling", "minecraft:green_stained_glass_pane");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:birch_sign", "minecraft:sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:birch_wall_sign", "minecraft:wall_sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:brick_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:cornflower", "minecraft:blue_orchid");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_sign", "minecraft:sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:dark_oak_wall_sign", "minecraft:wall_sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:diorite_slab", "minecraft:cobblestone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:diorite_stairs", "minecraft:cobblestone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:diorite_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:end_stone_brick_slab", "minecraft:cobblestone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:end_stone_brick_stairs", "minecraft:cobblestone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:end_stone_brick_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:granite_slab", "minecraft:cobblestone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:granite_stairs", "minecraft:cobblestone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:granite_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_sign", "minecraft:sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:jungle_wall_sign", "minecraft:wall_sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:lily_of_the_valley", "minecraft:oxeye_daisy");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:loom", "minecraft:crafting_table");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:mossy_cobblestone_slab", "minecraft:cobblestone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:mossy_cobblestone_stairs", "minecraft:cobblestone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:mossy_stone_brick_slab", "minecraft:stone_brick_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:mossy_stone_brick_stairs", "minecraft:stone_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:mossy_stone_brick_wall", "minecraft:mossy_cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:nether_brick_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:oak_sign", "minecraft:sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:oak_wall_sign", "minecraft:wall_sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:polished_andesite_slab", "minecraft:stone_brick_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:polished_andesite_stairs", "minecraft:stone_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:polished_diorite_slab", "minecraft:stone_brick_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:polished_diorite_stairs", "minecraft:stone_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:polished_granite_slab", "minecraft:stone_brick_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:polished_granite_stairs", "minecraft:stone_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_acacia_sapling", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_allium", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_azure_bluet", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_bamboo", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_birch_sapling", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_blue_orchid", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_brown_mushroom", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_cactus", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_cornflower", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_dandelion", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_dark_oak_sapling", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_dead_bush", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_fern", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_jungle_sapling", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_lily_of_the_valley", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_oak_sapling", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_orange_tulip", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_oxeye_daisy", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_pink_tulip", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_poppy", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_red_mushroom", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_red_tulip", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_spruce_sapling", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_white_tulip", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:potted_wither_rose", "minecraft:flower_pot");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:prismarine_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_nether_brick_slab", "minecraft:nether_brick_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_nether_brick_stairs", "minecraft:nether_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_nether_brick_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:red_sandstone_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:sandstone_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_quartz_slab", "minecraft:quartz_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_quartz_stairs", "minecraft:quartz_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_red_sandstone_slab", "minecraft:red_sandstone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_red_sandstone_stairs", "minecraft:red_sandstone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_sandstone_slab", "minecraft:sandstone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_sandstone_stairs", "minecraft:sandstone_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smooth_stone_slab", "minecraft:stone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_sign", "minecraft:sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:spruce_wall_sign", "minecraft:wall_sign");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:stone_brick_wall", "minecraft:cobblestone_wall");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:stone_stairs", "minecraft:stone_brick_stairs");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:wither_rose", "minecraft:allium");

                    // Minecraft 1.14 (Snapshot 18w44a).
                    Diccionario_1_13_a_1_12_2.Add("minecraft:barrel", "minecraft:cauldron");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bell", "minecraft:note_block");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:blast_furnace", "minecraft:furnace");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:cartography_table", "minecraft:crafting_table");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:fletching_table", "minecraft:crafting_table");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:grindstone", "minecraft:anvil");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:lectern", "minecraft:bookshelf");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smithing_table", "minecraft:crafting_table");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:smoker", "minecraft:furnace");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:stonecutter", "minecraft:anvil");

                    // Minecraft 1.14 (Snapshot 18w49a).
                    Diccionario_1_13_a_1_12_2.Add("minecraft:jigsaw", "minecraft:coal_block");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:lantern", "minecraft:glowstone");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:scaffolding", "minecraft:dirt");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:sweet_berry_bush", "minecraft:lime_stained_glass_pane");

                    // Minecraft 1.14 (Snapshot 19w06a).
                    Diccionario_1_13_a_1_12_2.Add("minecraft:campfire", "minecraft:magma_block");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:composter", "minecraft:cauldron");

                    // Minecraft 1.14.
                    Diccionario_1_13_a_1_12_2.Add("minecraft:cut_red_sandstone_slab", "minecraft:red_sandstone_slab");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:cut_sandstone_slab", "minecraft:sandstone_slab");

                    // New blocks from the Minecraft 1.15 snapshot 19w35a:
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bee_hive", "minecraft:cauldron");
                    Diccionario_1_13_a_1_12_2.Add("minecraft:bee_nest", "minecraft:cauldron");
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        /// <summary>
        /// Obtains a custom Data value for 1.12.2- based on the list of properties of a block. The page "Java Edition data values" on the official Minecraft wiki was key to help to program it correctly, so please check it out also if you're interested in this topic.
        /// </summary>
        /// <param name="Nombre_1_13">A valid Minecraft 1.13+ block name.</param>
        /// <param name="Lista_Propiedades">A list with the NBT properties of a block. It can be null or empty, which will give a default Data value.</param>
        /// <param name="ID_Original">The Minecraft 1.12.2- block ID based on the block name.</param>
        /// <param name="Data_Original">The Minecraft 1.12.2- block Data based on the block name.</param>
        /// <param name="Data">A value between 0 and 15. It's default is 0.</param>
        /// <returns>Returns an adapted Data value between 0 and 15. If it doesn't need to be adapted it will return the original Data value passed.</returns>
        internal static byte Obtener_ID_Data_Bloque_Ajustados(string Nombre_1_13, List<string> Lista_Propiedades, out byte Data)
        {
            Data = 0; // Default.
            try
            {
                byte ID = 0; // Air.
                if (string.IsNullOrEmpty(Nombre_1_13) || !Minecraft.Bloques.Diccionario_Nombre_Índice.ContainsKey(Nombre_1_13))
                {
                    Nombre_1_13 = "minecraft:glass"; // "minecraft:air"; // Unknown block, so replace it with air.
                }
                if (Lista_Propiedades == null) Lista_Propiedades = new List<string>(); // This can be empty, but never null.

                // First replace the 1.13+ missing blocks on 1.12.2- with relatively similar blocks.
                Reiniciar_Diccionario_1_13_a_1_12_2(); // Make sure the 1.13+ dictionary is started.
                if (Diccionario_1_13_a_1_12_2.ContainsKey(Nombre_1_13))
                {
                    if (string.Compare(Nombre_1_13, Diccionario_1_13_a_1_12_2[Nombre_1_13], true) != 0) // Different.
                    {
                        Nombre_1_13 = Diccionario_1_13_a_1_12_2[Nombre_1_13]; // Use a similar block instead.
                        string Nombre_Minúsculas = Nombre_1_13.ToLowerInvariant();
                        if (!Nombre_Minúsculas.Contains("_slab") && !Nombre_Minúsculas.Contains("_stairs"))
                        {
                            Lista_Propiedades.Clear(); // Avoid setting wrong properties if the block has changed.
                        }
                        Nombre_Minúsculas = null;
                    }
                }

                short ID_Minecraft_Tools = Minecraft.Bloques.Diccionario_Nombre_Índice[Nombre_1_13];
                foreach (KeyValuePair<short, short> Entrada in Minecraft.Diccionario_Bloques_Índices_1_12_2_a_Índices_1_13)
                {
                    if (Entrada.Value == ID_Minecraft_Tools)
                    {
                        ID = Minecraft.Obtener_Valores_ID_Data(Entrada.Key, out Data);
                        break;
                    }
                }
                //ID = Minecraft.Buscar_ID_Data_Minecraft_1_12_2(Minecraft.Bloques.Diccionario_Nombre_Índice[Nombre_1_13], out Data);

                // Change some IDs based on the properties, since my program ignores
                // for example if a repeater is on or off, because on it's name that
                // value never appears, so now analyze it's properties to find it out
                // and properly convert the blocks and in some cases it's ID values.

                if (string.Compare(Nombre_1_13, "minecraft:stone_slab", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:sandstone_slab", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:petrified_oak_slab", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:cobblestone_slab", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:brick_slab", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:stone_brick_slab", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:quartz_slab", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:nether_brick_slab", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:oak_slab", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:spruce_slab", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:birch_slab", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:jungle_slab", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:acacia_slab", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:dark_oak_slab", true) == 0) // ID: 44, 126.
                { // Using also "half: X" because Mojang uses it on it's NBT structure files (it's a bug).
                    if (!Buscar_Propiedad("type: double", Lista_Propiedades) && !Buscar_Propiedad("half: double", Lista_Propiedades)) // Regular slab.
                    {
                        if (Buscar_Propiedad("type: top", Lista_Propiedades) || Buscar_Propiedad("half: top", Lista_Propiedades)) Data |= !Variable_Mundo_Invertido ? (byte)8 : (byte)0; // Slab is upside-down, occupying the top half of its voxel.
                        else Data |= !Variable_Mundo_Invertido ? (byte)0 : (byte)8;
                    }
                    else // Double slab, replace with a full block (temporary fix).
                    {
                        if (string.Compare(Nombre_1_13, "minecraft:stone_slab", true) == 0)
                        {
                            ID = 1;
                            Data = 0;
                        }
                        else if (string.Compare(Nombre_1_13, "minecraft:sandstone_slab", true) == 0)
                        {
                            ID = 24;
                            Data = 0;
                        }
                        else if (string.Compare(Nombre_1_13, "minecraft:petrified_oak_slab", true) == 0)
                        {
                            ID = 5;
                            Data = 0;
                        }
                        else if (string.Compare(Nombre_1_13, "minecraft:cobblestone_slab", true) == 0)
                        {
                            ID = 4;
                            Data = 0;
                        }
                        else if (string.Compare(Nombre_1_13, "minecraft:brick_slab", true) == 0)
                        {
                            ID = 45;
                            Data = 0;
                        }
                        else if (string.Compare(Nombre_1_13, "minecraft:stone_brick_slab", true) == 0)
                        {
                            ID = 98;
                            Data = 0;
                        }
                        else if (string.Compare(Nombre_1_13, "minecraft:quartz_slab", true) == 0)
                        {
                            ID = 155;
                            Data = 0;
                        }
                        else if (string.Compare(Nombre_1_13, "minecraft:nether_brick_slab", true) == 0)
                        {
                            ID = 112;
                            Data = 0;
                        }
                        else if (string.Compare(Nombre_1_13, "minecraft:oak_slab", true) == 0)
                        {
                            ID = 5;
                            Data = 0;
                        }
                        else if (string.Compare(Nombre_1_13, "minecraft:spruce_slab", true) == 0)
                        {
                            ID = 5;
                            Data = 1;
                        }
                        else if (string.Compare(Nombre_1_13, "minecraft:birch_slab", true) == 0)
                        {
                            ID = 5;
                            Data = 2;
                        }
                        else if (string.Compare(Nombre_1_13, "minecraft:jungle_slab", true) == 0)
                        {
                            ID = 5;
                            Data = 3;
                        }
                        else if (string.Compare(Nombre_1_13, "minecraft:acacia_slab", true) == 0)
                        {
                            ID = 5;
                            Data = 4;
                        }
                        else if (string.Compare(Nombre_1_13, "minecraft:dark_oak_slab", true) == 0)
                        {
                            ID = 5;
                            Data = 5;
                        }
                        //Data = 15; // ?
                        //if (Buscar_Propiedad("type: top", Lista_Propiedades)) Data |= 8; // Slab is upside-down, occupying the top half of its voxel.
                    }
                }
                else if (string.Compare(Nombre_1_13, "minecraft:water", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:flowing_water", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:lava", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:flowing_lava", true) == 0) // ID: 8 ~ 11.
                {
                    if (Buscar_Propiedad("level: 8", Lista_Propiedades)) Data = 8; // Level 8.
                    else if (Buscar_Propiedad("level: 7", Lista_Propiedades)) Data = 7; // Level 7.
                    else if (Buscar_Propiedad("level: 6", Lista_Propiedades)) Data = 6; // Level 6.
                    else if (Buscar_Propiedad("level: 5", Lista_Propiedades)) Data = 5; // Level 5.
                    else if (Buscar_Propiedad("level: 4", Lista_Propiedades)) Data = 4; // Level 4.
                    else if (Buscar_Propiedad("level: 3", Lista_Propiedades)) Data = 3; // Level 3.
                    else if (Buscar_Propiedad("level: 2", Lista_Propiedades)) Data = 2; // Level 2.
                    else if (Buscar_Propiedad("level: 1", Lista_Propiedades)) Data = 1; // Level 1.
                    else if (Buscar_Propiedad("level: 0", Lista_Propiedades)) Data = 0; // Level 0.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:oak_log", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:stripped_oak_log", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:stripped_oak_wood", true) == 0) // ID: 17, 0.
                {
                    Data = 0; // Oak Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    ID = 17; // change the ID to log.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:oak_bark", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:oak_wood", true) == 0) // ID: 17, 0.
                {
                    Data = 0; // Oak Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    Data |= 12; // Only bark.

                    ID = 17; // change the ID to log.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:spruce_log", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:stripped_spruce_log", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:stripped_spruce_wood", true) == 0) // ID: 17, 1.
                {
                    Data = 1; // Spruce Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    ID = 17; // change the ID to log.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:spruce_bark", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:spruce_wood", true) == 0) // ID: 17, 1.
                {
                    Data = 1; // Spruce Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    Data |= 12; // Only bark.

                    ID = 17; // change the ID to log.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:birch_log", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:stripped_birch_log", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:stripped_birch_wood", true) == 0) // ID: 17, 2.
                {
                    Data = 2; // Birch Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    ID = 17; // change the ID to log.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:birch_bark", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:birch_wood", true) == 0) // ID: 17, 2.
                {
                    Data = 2; // Birch Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    Data |= 12; // Only bark.

                    ID = 17; // change the ID to log.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:jungle_log", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:stripped_jungle_log", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:stripped_jungle_wood", true) == 0) // ID: 17, 3.
                {
                    Data = 3; // Jungle Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    ID = 17; // change the ID to log.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:jungle_bark", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:jungle_wood", true) == 0) // ID: 17, 3.
                {
                    Data = 3; // Jungle Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    Data |= 12; // Only bark.

                    ID = 17; // change the ID to log.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:oak_leaves", true) == 0) // ID: 18, 0.
                {
                    if (Buscar_Propiedad("persistent: true", Lista_Propiedades)) Data = 12; // Oak Leaves (no decay and check decay).
                    else Data = 0; // Oak Leaves.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:spruce_leaves", true) == 0) // ID: 18, 1.
                {
                    if (Buscar_Propiedad("persistent: true", Lista_Propiedades)) Data = 13; // Spruce Leaves (no decay and check decay).
                    else Data = 1; // Spruce Leaves.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:birch_leaves", true) == 0) // ID: 18, 2.
                {
                    if (Buscar_Propiedad("persistent: true", Lista_Propiedades)) Data = 14; // Birch Leaves (no decay and check decay).
                    else Data = 2; // Birch Leaves.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:jungle_leaves", true) == 0) // ID: 18, 3.
                {
                    if (Buscar_Propiedad("persistent: true", Lista_Propiedades)) Data = 15; // Jungle Leaves (no decay and check decay).
                    else Data = 3; // Jungle Leaves.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:dispenser", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:dropper", true) == 0) // ID: 23 - 158.
                {
                    if (Buscar_Propiedad("facing: down", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)0 : (byte)1; // Dropper facing down.
                    else if (Buscar_Propiedad("facing: up", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)1 : (byte)0; // Dropper facing up.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Dropper facing north.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // Dropper facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // Dropper facing west.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // Dropper facing east.

                    if (Buscar_Propiedad("triggered: true", Lista_Propiedades)) Data |= 8; // Set if it's activated.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:bed", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:black_bed", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:blue_bed", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:brown_bed", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:cyan_bed", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:gray_bed", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:green_bed", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:light_blue_bed", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:light_gray_bed", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:lime_bed", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:magenta_bed", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:orange_bed", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:pink_bed", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:purple_bed", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:red_bed", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:white_bed", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:yellow_bed", true) == 0) // ID: 26.
                {
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // Head facing South.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // Head facing West.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Head facing North.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Head facing East.

                    if (Buscar_Propiedad("occupied: true", Lista_Propiedades)) Data |= 4; // The bed is occupied.

                    if (Buscar_Propiedad("part: head", Lista_Propiedades)) Data |= 8; // The head of the bed.

                    ID = 26; // Needs a change of ID.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:activator_rail", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:detector_rail", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:powered_rail", true) == 0) // ID: 27 ~ 28 - 157.
                {
                    if (Buscar_Propiedad("shape: north_south", Lista_Propiedades)) Data = 0; // flat track going north-south.
                    else if (Buscar_Propiedad("shape: east_west", Lista_Propiedades)) Data = 1; // flat track going west-east.
                    else if (Buscar_Propiedad("shape: ascending_east", Lista_Propiedades)) Data = 2; // sloped track ascending to the east.
                    else if (Buscar_Propiedad("shape: ascending_west", Lista_Propiedades)) Data = 3; // sloped track ascending to the west.
                    else if (Buscar_Propiedad("shape: ascending_north", Lista_Propiedades)) Data = 4; // sloped track ascending to the north.
                    else if (Buscar_Propiedad("shape: ascending_south", Lista_Propiedades)) Data = 5; // sloped track ascending to the south.

                    if (Buscar_Propiedad("powered: true", Lista_Propiedades)) Data |= 8; // Set if rail is active.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:piston", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:sticky_piston", true) == 0) // ID: 29 ~ 33.
                {
                    if (Buscar_Propiedad("facing: down", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)0 : (byte)1; // Down.
                    else if (Buscar_Propiedad("facing: up", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)1 : (byte)0; // Up.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // north.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // west.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // east.

                    if (Buscar_Propiedad("extended: true", Lista_Propiedades)) Data |= 8; // 1 for pushed out.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:piston_head", true) == 0) // ID: 34.
                {
                    // What is the "short: true" property in the piston heads?...
                    // Here will be ignored until I find what it is, so please tell me.

                    if (Buscar_Propiedad("facing: down", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)0 : (byte)1; // Down.
                    else if (Buscar_Propiedad("facing: up", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)1 : (byte)0; // Up.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // north.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // west.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // east.

                    if (Buscar_Propiedad("type: sticky", Lista_Propiedades)) Data |= 8; // 1 is sticky.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:torch", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:wall_torch", true) == 0) // ID: 50.
                {
                    if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 1; // Facing east (attached to a block to its west).
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 2; // Facing west (attached to a block to its east).
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // Facing south (attached to a block to its north).
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 4; // Facing north (attached to a block to its south).
                    else Data = 5; // Facing up (attached to a block beneath it).
                }
                else if (string.Compare(Nombre_1_13, "minecraft:fire", true) == 0) // ID: 51.
                {
                    if (Buscar_Propiedad("age: 0", Lista_Propiedades)) Data = 0; // Age 0.
                    else if (Buscar_Propiedad("age: 1", Lista_Propiedades)) Data = 1; // Age 1.
                    else if (Buscar_Propiedad("age: 2", Lista_Propiedades)) Data = 2; // Age 2.
                    else if (Buscar_Propiedad("age: 3", Lista_Propiedades)) Data = 3; // Age 3.
                    else if (Buscar_Propiedad("age: 4", Lista_Propiedades)) Data = 4; // Age 4.
                    else if (Buscar_Propiedad("age: 5", Lista_Propiedades)) Data = 5; // Age 5.
                    else if (Buscar_Propiedad("age: 6", Lista_Propiedades)) Data = 6; // Age 6.
                    else if (Buscar_Propiedad("age: 7", Lista_Propiedades)) Data = 7; // Age 7.
                    else if (Buscar_Propiedad("age: 8", Lista_Propiedades)) Data = 8; // Age 8.
                    else if (Buscar_Propiedad("age: 9", Lista_Propiedades)) Data = 9; // Age 9.
                    else if (Buscar_Propiedad("age: 10", Lista_Propiedades)) Data = 10; // Age 10.
                    else if (Buscar_Propiedad("age: 11", Lista_Propiedades)) Data = 11; // Age 11.
                    else if (Buscar_Propiedad("age: 12", Lista_Propiedades)) Data = 12; // Age 12.
                    else if (Buscar_Propiedad("age: 13", Lista_Propiedades)) Data = 13; // Age 13.
                    else if (Buscar_Propiedad("age: 14", Lista_Propiedades)) Data = 14; // Age 14.
                    else if (Buscar_Propiedad("age: 15", Lista_Propiedades)) Data = 15; // Age 15.
                    // 0x0 is a placed or spread fire. Once it reaches 0xF the eternal fire-trick will work since there will be no further updates of the block.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:acacia_stairs", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:birch_stairs", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:brick_stairs", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:cobblestone_stairs", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:dark_oak_stairs", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:jungle_stairs", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:nether_brick_stairs", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:oak_stairs", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:purpur_stairs", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:quartz_stairs", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:red_sandstone_stairs", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:sandstone_stairs", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:spruce_stairs", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:stone_brick_stairs", true) == 0) // ID: 53 ~ 67 ~ 108 ~ 109 ~ 114 ~ 128 ~ 134 ~ 135 ~ 136 ~ 156 ~ 163 ~ 164 ~ 180 ~ 203.
                {
                    if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 0; // East.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // West.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 2; // South.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 3; // North.

                    if (Buscar_Propiedad("half: top", Lista_Propiedades)) Data |= !Variable_Mundo_Invertido ? (byte)4 : (byte)0; // Set if stairs are upside-down.
                    else Data |= !Variable_Mundo_Invertido ? (byte)0 : (byte)4;
                }
                else if (string.Compare(Nombre_1_13, "minecraft:chest", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:ladder", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:trapped_chest", true) == 0) // ID: 54 ~ 65 ~ 146.
                {
                    if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // facing north.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // facing west.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // facing east.

                    if (Data < 2 || Data > 5) Data = 2; // Invalid values default to 2.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:redstone_wire", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:light_weighted_pressure_plate", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:heavy_weighted_pressure_plate", true) == 0) // ID: 55 ~ 147-148.
                {
                    if (Buscar_Propiedad("power: 0", Lista_Propiedades)) Data = 0; // Power 0.
                    else if (Buscar_Propiedad("power: 1", Lista_Propiedades)) Data = 1; // Power 1.
                    else if (Buscar_Propiedad("power: 2", Lista_Propiedades)) Data = 2; // Power 2.
                    else if (Buscar_Propiedad("power: 3", Lista_Propiedades)) Data = 3; // Power 3.
                    else if (Buscar_Propiedad("power: 4", Lista_Propiedades)) Data = 4; // Power 4.
                    else if (Buscar_Propiedad("power: 5", Lista_Propiedades)) Data = 5; // Power 5.
                    else if (Buscar_Propiedad("power: 6", Lista_Propiedades)) Data = 6; // Power 6.
                    else if (Buscar_Propiedad("power: 7", Lista_Propiedades)) Data = 7; // Power 7.
                    else if (Buscar_Propiedad("power: 8", Lista_Propiedades)) Data = 8; // Power 8.
                    else if (Buscar_Propiedad("power: 9", Lista_Propiedades)) Data = 9; // Power 9.
                    else if (Buscar_Propiedad("power: 10", Lista_Propiedades)) Data = 10; // Power 10.
                    else if (Buscar_Propiedad("power: 11", Lista_Propiedades)) Data = 11; // Power 11.
                    else if (Buscar_Propiedad("power: 12", Lista_Propiedades)) Data = 12; // Power 12.
                    else if (Buscar_Propiedad("power: 13", Lista_Propiedades)) Data = 13; // Power 13.
                    else if (Buscar_Propiedad("power: 14", Lista_Propiedades)) Data = 14; // Power 14.
                    else if (Buscar_Propiedad("power: 15", Lista_Propiedades)) Data = 15; // Power 15.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:wheat", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:carrots", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:potatoes", true) == 0) // ID: 59 ~ 141-142.
                {
                    if (Buscar_Propiedad("age: 0", Lista_Propiedades)) Data = 0; // Age 0.
                    else if (Buscar_Propiedad("age: 1", Lista_Propiedades)) Data = 1; // Age 1.
                    else if (Buscar_Propiedad("age: 2", Lista_Propiedades)) Data = 2; // Age 2.
                    else if (Buscar_Propiedad("age: 3", Lista_Propiedades)) Data = 3; // Age 3.
                    else if (Buscar_Propiedad("age: 4", Lista_Propiedades)) Data = 4; // Age 4.
                    else if (Buscar_Propiedad("age: 5", Lista_Propiedades)) Data = 5; // Age 5.
                    else if (Buscar_Propiedad("age: 6", Lista_Propiedades)) Data = 6; // Age 6.
                    else if (Buscar_Propiedad("age: 7", Lista_Propiedades)) Data = 7; // Age 7.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:farmland", true) == 0) // ID: 60.
                {
                    if (Buscar_Propiedad("moisture: 0", Lista_Propiedades)) Data = 0; // Moisture 0.
                    else if (Buscar_Propiedad("moisture: 1", Lista_Propiedades)) Data = 1; // Moisture 1.
                    else if (Buscar_Propiedad("moisture: 2", Lista_Propiedades)) Data = 2; // Moisture 2.
                    else if (Buscar_Propiedad("moisture: 3", Lista_Propiedades)) Data = 3; // Moisture 3.
                    else if (Buscar_Propiedad("moisture: 4", Lista_Propiedades)) Data = 4; // Moisture 4.
                    else if (Buscar_Propiedad("moisture: 5", Lista_Propiedades)) Data = 5; // Moisture 5.
                    else if (Buscar_Propiedad("moisture: 6", Lista_Propiedades)) Data = 6; // Moisture 6.
                    else if (Buscar_Propiedad("moisture: 7", Lista_Propiedades)) Data = 7; // Moisture 7.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:furnace", true) == 0) // ID: 61-62.
                {
                    if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // facing north.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // facing west.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // facing east.

                    if (Data < 2 || Data > 5) Data = 2; // Invalid values default to 2.

                    if (Buscar_Propiedad("lit: true", Lista_Propiedades)) ID = 62; // Lit needs a change of ID.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:sign", true) == 0) // ID: 63.
                {
                    if (Buscar_Propiedad("rotation: 0", Lista_Propiedades)) Data = 0; // south.
                    else if (Buscar_Propiedad("rotation: 1", Lista_Propiedades)) Data = 1; // south-southwest.
                    else if (Buscar_Propiedad("rotation: 2", Lista_Propiedades)) Data = 2; // southwest.
                    else if (Buscar_Propiedad("rotation: 3", Lista_Propiedades)) Data = 3; // west-southwest.
                    else if (Buscar_Propiedad("rotation: 4", Lista_Propiedades)) Data = 4; // west.
                    else if (Buscar_Propiedad("rotation: 5", Lista_Propiedades)) Data = 5; // west-northwest.
                    else if (Buscar_Propiedad("rotation: 6", Lista_Propiedades)) Data = 6; // northwest.
                    else if (Buscar_Propiedad("rotation: 7", Lista_Propiedades)) Data = 7; // north-northwest.
                    else if (Buscar_Propiedad("rotation: 8", Lista_Propiedades)) Data = 8; // north.
                    else if (Buscar_Propiedad("rotation: 9", Lista_Propiedades)) Data = 9; // north-northeast.
                    else if (Buscar_Propiedad("rotation: 10", Lista_Propiedades)) Data = 10; // northeast.
                    else if (Buscar_Propiedad("rotation: 11", Lista_Propiedades)) Data = 11; // east-northeast.
                    else if (Buscar_Propiedad("rotation: 12", Lista_Propiedades)) Data = 12; // east.
                    else if (Buscar_Propiedad("rotation: 13", Lista_Propiedades)) Data = 13; // east-southeast.
                    else if (Buscar_Propiedad("rotation: 14", Lista_Propiedades)) Data = 14; // southeast.
                    else if (Buscar_Propiedad("rotation: 15", Lista_Propiedades)) Data = 15; // south-southeast.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:acacia_door", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:birch_door", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:dark_oak_door", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:iron_door", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:jungle_door", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:oak_door", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:spruce_door", true) == 0) // ID: 64 ~ 193-197.
                {
                    if (Buscar_Propiedad("half: upper", Lista_Propiedades))
                    {
                        Data = 8; // This is the top half of a door.

                        if (Buscar_Propiedad("hinge: right", Lista_Propiedades)) Data |= 1; // Hinge is on the left.

                        if (Buscar_Propiedad("powered: true", Lista_Propiedades)) Data |= 2; // Door is Powered.
                    }
                    else // half: lower
                    {
                        if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 0; // Facing east.
                        else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 1; // Facing south.
                        else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 2; // Facing west.
                        else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 3; // Facing north.

                        if (Buscar_Propiedad("open: true", Lista_Propiedades)) Data |= 4; // Open.
                    }
                }
                else if (string.Compare(Nombre_1_13, "minecraft:rail", true) == 0) // ID: 66.
                {
                    if (Buscar_Propiedad("shape: north_south", Lista_Propiedades)) Data = 0; // Straight rail connecting to the north and south.
                    else if (Buscar_Propiedad("shape: east_west", Lista_Propiedades)) Data = 1; // Straight rail connecting to the east and west.
                    else if (Buscar_Propiedad("shape: ascending_east", Lista_Propiedades)) Data = 2; // Sloped rail ascending to the east.
                    else if (Buscar_Propiedad("shape: ascending_west", Lista_Propiedades)) Data = 3; // Sloped rail ascending to the west.
                    else if (Buscar_Propiedad("shape: ascending_north", Lista_Propiedades)) Data = 4; // Sloped rail ascending to the north.
                    else if (Buscar_Propiedad("shape: ascending_south", Lista_Propiedades)) Data = 5; // Sloped rail ascending to the south.
                    else if (Buscar_Propiedad("shape: south_east", Lista_Propiedades)) Data = 6; // Curved rail connecting to the south and east.
                    else if (Buscar_Propiedad("shape: south_west", Lista_Propiedades)) Data = 7; // Curved rail connecting to the south and west.
                    else if (Buscar_Propiedad("shape: north_west", Lista_Propiedades)) Data = 8; // Curved rail connecting to the north and west.
                    else if (Buscar_Propiedad("shape: north_east", Lista_Propiedades)) Data = 9; // Curved rail connecting to the north and east.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:prismarine_stairs", true) == 0) // ID: 67.
                {
                    if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 0; // East.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // West.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 2; // South.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 3; // North.

                    if (Buscar_Propiedad("half: top", Lista_Propiedades)) Data |= !Variable_Mundo_Invertido ? (byte)4 : (byte)0; // Set if stairs are upside-down.
                    else Data |= !Variable_Mundo_Invertido ? (byte)0 : (byte)4;

                    ID = 67; // change the ID to cobblestone stairs.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:wall_sign", true) == 0) // ID: 68.
                {
                    if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // north.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // west.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // east.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:lever", true) == 0) // ID: 69.
                {
                    if (Buscar_Propiedad("face: wall", Lista_Propiedades)) // Lever on block side.
                    {
                        if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 1; // Lever on block side facing east.
                        else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 2; // Lever on block side facing west.
                        else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // Lever on block side facing south.
                        else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 4; // Lever on block side facing north.
                    }
                    else if (Buscar_Propiedad("face: floor", Lista_Propiedades)) // Lever on block top.
                    {
                        if (Buscar_Propiedad("facing: south", Lista_Propiedades) || Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)5 : (byte)0; // Lever on block top points south when off.
                        else if (Buscar_Propiedad("facing: east", Lista_Propiedades) || Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)6 : (byte)7; // Lever on block top points east when off.
                    }
                    else if (Buscar_Propiedad("face: ceiling", Lista_Propiedades)) // Lever on block bottom.
                    {
                        if (Buscar_Propiedad("facing: east", Lista_Propiedades) || Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)0 : (byte)5; // Lever on block bottom points east when off.
                        else if (Buscar_Propiedad("facing: south", Lista_Propiedades) || Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)7 : (byte)6; // Lever on block bottom points south when off.
                    }

                    if (Buscar_Propiedad("powered: true", Lista_Propiedades)) Data |= 8; // Set if activated/disabled.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:acacia_pressure_plate", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:birch_pressure_plate", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:dark_oak_pressure_plate", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:jungle_pressure_plate", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:oak_pressure_plate", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:spruce_pressure_plate", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:stone_pressure_plate", true) == 0) // ID: 70 ~ 72.
                {
                    if (Buscar_Propiedad("powered: true", Lista_Propiedades)) Data = 1; // If this bit is set, the pressure plate is active.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:redstone_ore", true) == 0) // ID: 73 ~ 74.
                {
                    if (Buscar_Propiedad("lit: true", Lista_Propiedades)) ID = 74; // Lit.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:redstone_torch", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:redstone_wall_torch", true) == 0) // ID: 75 ~ 76.
                {
                    if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 1; // Facing east (attached to a block to its west).
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 2; // Facing west (attached to a block to its east).
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // Facing south (attached to a block to its north).
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 4; // Facing north (attached to a block to its south).
                    else Data = 5; // Facing up (attached to a block beneath it).

                    if (Buscar_Propiedad("lit: true", Lista_Propiedades)) ID = 76; // Lit.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:acacia_button", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:birch_button", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:dark_oak_button", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:jungle_button", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:oak_button", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:spruce_button", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:stone_button", true) == 0) // ID: 77 ~ 143.
                {
                    if (Buscar_Propiedad("face: ceiling", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)0 : (byte)5; // Button on block bottom facing down.
                    else if (Buscar_Propiedad("face: wall", Lista_Propiedades)) // Button on block side.
                    {
                        if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 1; // Button on block side facing east.
                        else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 2; // Button on block side facing west.
                        else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // Button on block side facing south.
                        else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 4; // Button on block side facing north.
                    }
                    else if (Buscar_Propiedad("face: floor", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)5 : (byte)0; // Button on block top facing up.

                    if (Buscar_Propiedad("powered: true", Lista_Propiedades)) Data |= 8; // If this bit is set, the button is currently active.

                    ID = 143; // Change the ID to oak button.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:snow", true) == 0) // ID: 78.
                {
                    if (Buscar_Propiedad("layers: 1", Lista_Propiedades)) Data = 0; // One layer, 2 pixels thick.
                    else if (Buscar_Propiedad("layers: 2", Lista_Propiedades)) Data = 1; // Two layers, 4 pixels thick.
                    else if (Buscar_Propiedad("layers: 3", Lista_Propiedades)) Data = 2; // Three layers, 6 pixels thick.
                    else if (Buscar_Propiedad("layers: 4", Lista_Propiedades)) Data = 3; // Four layers, 8 pixels thick.
                    else if (Buscar_Propiedad("layers: 5", Lista_Propiedades)) Data = 4; // Five layers, 10 pixels thick.
                    else if (Buscar_Propiedad("layers: 6", Lista_Propiedades)) Data = 5; // Six layers, 12 pixels thick.
                    else if (Buscar_Propiedad("layers: 7", Lista_Propiedades)) Data = 6; // Seven layers, 14 pixels thick.
                    else if (Buscar_Propiedad("layers: 8", Lista_Propiedades)) Data = 7; // Eight layers, 16 pixels thick.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:cactus", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:sugar_cane", true) == 0) // ID: 81 ~ 83.
                {
                    if (Buscar_Propiedad("age: 0", Lista_Propiedades)) Data = 0; // Age 0.
                    else if (Buscar_Propiedad("age: 1", Lista_Propiedades)) Data = 1; // Age 1.
                    else if (Buscar_Propiedad("age: 2", Lista_Propiedades)) Data = 2; // Age 2.
                    else if (Buscar_Propiedad("age: 3", Lista_Propiedades)) Data = 3; // Age 3.
                    else if (Buscar_Propiedad("age: 4", Lista_Propiedades)) Data = 4; // Age 4.
                    else if (Buscar_Propiedad("age: 5", Lista_Propiedades)) Data = 5; // Age 5.
                    else if (Buscar_Propiedad("age: 6", Lista_Propiedades)) Data = 6; // Age 6.
                    else if (Buscar_Propiedad("age: 7", Lista_Propiedades)) Data = 7; // Age 7.
                    else if (Buscar_Propiedad("age: 8", Lista_Propiedades)) Data = 8; // Age 8.
                    else if (Buscar_Propiedad("age: 9", Lista_Propiedades)) Data = 9; // Age 9.
                    else if (Buscar_Propiedad("age: 10", Lista_Propiedades)) Data = 10; // Age 10.
                    else if (Buscar_Propiedad("age: 11", Lista_Propiedades)) Data = 11; // Age 11.
                    else if (Buscar_Propiedad("age: 12", Lista_Propiedades)) Data = 12; // Age 12.
                    else if (Buscar_Propiedad("age: 13", Lista_Propiedades)) Data = 13; // Age 13.
                    else if (Buscar_Propiedad("age: 14", Lista_Propiedades)) Data = 14; // Age 14.
                    else if (Buscar_Propiedad("age: 15", Lista_Propiedades)) Data = 15; // Age 15.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:jukebox", true) == 0) // ID: 84.
                {
                    if (Buscar_Propiedad("has_record: true", Lista_Propiedades)) Data = 1; // Contains a disc.
                    // The associated block entity is used to identify which record has been inserted.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:carved_pumpkin", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:jack_o_lantern", true) == 0) // ID: 86 ~ 91.
                {
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // Pumpkin facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // Pumpkin facing west.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Pumpkin facing north.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Pumpkin facing east.

                    //Data = 4; // Jack o'lantern without face. Will this work?
                }
                else if (string.Compare(Nombre_1_13, "minecraft:pumpkin", true) == 0) // ID: 86.
                {
                    ID = 86; // Convert the ID to a pumpkin with face.

                    Data = (byte)Program.Rand.Next(0, 4); // Randomize the facing direction  if the pumpkin.

                    //Data = 4; // Pumpkin without face according to the wiki, but on my tests it dissapeared.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:cake", true) == 0) // ID: 92.
                {
                    if (Buscar_Propiedad("bites: 0", Lista_Propiedades)) Data = 0; // 0 pieces eaten.
                    else if (Buscar_Propiedad("bites: 1", Lista_Propiedades)) Data = 1; // 1 piece eaten.
                    else if (Buscar_Propiedad("bites: 2", Lista_Propiedades)) Data = 2; // 2 pieces eaten.
                    else if (Buscar_Propiedad("bites: 3", Lista_Propiedades)) Data = 3; // 3 pieces eaten.
                    else if (Buscar_Propiedad("bites: 4", Lista_Propiedades)) Data = 4; // 4 pieces eaten.
                    else if (Buscar_Propiedad("bites: 5", Lista_Propiedades)) Data = 5; // 5 pieces eaten.
                    else if (Buscar_Propiedad("bites: 6", Lista_Propiedades)) Data = 6; // 6 pieces eaten.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:repeater", true) == 0) // ID: 93 ~ 94.
                {
                    // Note that the arrow or triangle points in the opposite direction, which is confusing.
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // Facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // Facing west.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Facing north.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Facing east.

                    if (Buscar_Propiedad("delay: 1", Lista_Propiedades)) Data |= 0; // Delay of 1 redstone tick.
                    else if (Buscar_Propiedad("delay: 2", Lista_Propiedades)) Data |= 4; // Delay of 2 redstone ticks.
                    else if (Buscar_Propiedad("delay: 3", Lista_Propiedades)) Data |= 8; // Delay of 3 redstone ticks.
                    else if (Buscar_Propiedad("delay: 4", Lista_Propiedades)) Data |= 12; // Delay of 4 redstone ticks.

                    if (Buscar_Propiedad("powered: true", Lista_Propiedades)) ID = 94; // Powered needs a change of ID.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:acacia_trapdoor", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:birch_trapdoor", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:dark_oak_trapdoor", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:jungle_trapdoor", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:oak_trapdoor", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:spruce_trapdoor", true) == 0) // ID: 96.
                {
                    // The directions are inverted, which is confusing.
                    if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 0; // Trapdoor on the south side of a block.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 1; // Trapdoor on the north side of a block.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 2; // Trapdoor on the east side of a block.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Trapdoor on the west side of a block.

                    if (Buscar_Propiedad("open: true", Lista_Propiedades)) Data |= 4; // If this bit is set, the trapdoor is open.

                    if (Buscar_Propiedad("half: top", Lista_Propiedades)) Data |= !Variable_Mundo_Invertido ? (byte)8 : (byte)0; // If this bit is set, the trapdoor is on the top half of a block. Otherwise, it is on the bottom half.
                    else Data |= !Variable_Mundo_Invertido ? (byte)0 : (byte)8;

                    ID = 96; // Change the ID to oak trapdoor.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:iron_trapdoor", true) == 0) // ID: 167.
                {
                    // The directions are inverted, which is confusing.
                    if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 0; // Trapdoor on the south side of a block.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 1; // Trapdoor on the north side of a block.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 2; // Trapdoor on the east side of a block.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Trapdoor on the west side of a block.

                    if (Buscar_Propiedad("open: true", Lista_Propiedades)) Data |= 4; // If this bit is set, the trapdoor is open.

                    if (Buscar_Propiedad("half: top", Lista_Propiedades)) Data |= !Variable_Mundo_Invertido ? (byte)8 : (byte)0; // If this bit is set, the trapdoor is on the top half of a block. Otherwise, it is on the bottom half.
                    else Data |= !Variable_Mundo_Invertido ? (byte)0 : (byte)8;
                }
                else if (string.Compare(Nombre_1_13, "minecraft:mushroom_stem", true) == 0) // ID: 99.
                {
                    // Ignore the top part or the conversion will fail sometimes.
                    if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: false", Lista_Propiedades) &&
                        Buscar_Propiedad("north: false", Lista_Propiedades) &&
                        Buscar_Propiedad("south: false", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: false", Lista_Propiedades) &&
                        Buscar_Propiedad("west: false", Lista_Propiedades)) Data = 0; // Pores on all sides.
                    else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: true", Lista_Propiedades) &&
                        Buscar_Propiedad("north: true", Lista_Propiedades) &&
                        Buscar_Propiedad("south: true", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: false", Lista_Propiedades) &&
                        Buscar_Propiedad("west: true", Lista_Propiedades)) Data = 10; // Stem texture on all four sides, pores on top and bottom.
                    else if (Buscar_Propiedad("down: true", Lista_Propiedades) &&
                        Buscar_Propiedad("east: true", Lista_Propiedades) &&
                        Buscar_Propiedad("north: true", Lista_Propiedades) &&
                        Buscar_Propiedad("south: true", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: true", Lista_Propiedades) &&
                        Buscar_Propiedad("west: true", Lista_Propiedades)) Data = 15; // Stem texture on all six sides.
                    else Data = 0;

                    ID = 99; // Change the block ID to "minecraft:brown_mushroom_block".
                }
                else if (string.Compare(Nombre_1_13, "minecraft:brown_mushroom_block", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:red_mushroom_block", true) == 0) // ID: 99 ~ 100.
                {
                    // Ignore the top part or the conversion will fail sometimes.
                    if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: false", Lista_Propiedades) &&
                        Buscar_Propiedad("north: false", Lista_Propiedades) &&
                        Buscar_Propiedad("south: false", Lista_Propiedades) &&
                        Buscar_Propiedad("up: false", Lista_Propiedades) &&
                        Buscar_Propiedad("west: false", Lista_Propiedades)) Data = 0; // Pores on all sides.
                    else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: false", Lista_Propiedades) &&
                        Buscar_Propiedad("north: true", Lista_Propiedades) &&
                        Buscar_Propiedad("south: false", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: true", Lista_Propiedades) &&
                        Buscar_Propiedad("west: true", Lista_Propiedades)) Data = 1; // Cap texture on top, west and north.
                    else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: false", Lista_Propiedades) &&
                        Buscar_Propiedad("north: true", Lista_Propiedades) &&
                        Buscar_Propiedad("south: false", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: true", Lista_Propiedades) &&
                        Buscar_Propiedad("west: false", Lista_Propiedades)) Data = 2; // Cap texture on top and north.
                    else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: true", Lista_Propiedades) &&
                        Buscar_Propiedad("north: true", Lista_Propiedades) &&
                        Buscar_Propiedad("south: false", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: true", Lista_Propiedades) &&
                        Buscar_Propiedad("west: false", Lista_Propiedades)) Data = 3; // Cap texture on top, north and east.
                    else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: false", Lista_Propiedades) &&
                        Buscar_Propiedad("north: false", Lista_Propiedades) &&
                        Buscar_Propiedad("south: false", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: true", Lista_Propiedades) &&
                        Buscar_Propiedad("west: true", Lista_Propiedades)) Data = 4; // Cap texture on top and west.
                    else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: false", Lista_Propiedades) &&
                        Buscar_Propiedad("north: false", Lista_Propiedades) &&
                        Buscar_Propiedad("south: false", Lista_Propiedades) &&
                        Buscar_Propiedad("up: true", Lista_Propiedades) && // Don't ignore it here.
                        Buscar_Propiedad("west: false", Lista_Propiedades)) Data = 5; // Cap texture on top.
                    else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: true", Lista_Propiedades) &&
                        Buscar_Propiedad("north: false", Lista_Propiedades) &&
                        Buscar_Propiedad("south: false", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: true", Lista_Propiedades) &&
                        Buscar_Propiedad("west: false", Lista_Propiedades)) Data = 6; // Cap texture on top and east.
                    else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: false", Lista_Propiedades) &&
                        Buscar_Propiedad("north: false", Lista_Propiedades) &&
                        Buscar_Propiedad("south: true", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: true", Lista_Propiedades) &&
                        Buscar_Propiedad("west: true", Lista_Propiedades)) Data = 7; // Cap texture on top, south and west.
                    else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: false", Lista_Propiedades) &&
                        Buscar_Propiedad("north: false", Lista_Propiedades) &&
                        Buscar_Propiedad("south: true", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: true", Lista_Propiedades) &&
                        Buscar_Propiedad("west: false", Lista_Propiedades)) Data = 8; // Cap texture on top and south.
                    else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                        Buscar_Propiedad("east: true", Lista_Propiedades) &&
                        Buscar_Propiedad("north: false", Lista_Propiedades) &&
                        Buscar_Propiedad("south: true", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: true", Lista_Propiedades) &&
                        Buscar_Propiedad("west: false", Lista_Propiedades)) Data = 9; // Cap texture on top, east and south.
                                                                                      //else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                                                                                      //Buscar_Propiedad("east: false", Lista_Propiedades) &&
                                                                                      //Buscar_Propiedad("north: false", Lista_Propiedades) &&
                                                                                      //Buscar_Propiedad("south: false", Lista_Propiedades) &&
                                                                                      //Buscar_Propiedad("up: false", Lista_Propiedades) &&
                                                                                      //Buscar_Propiedad("west: false", Lista_Propiedades)) Data = 10; // Stem texture on all four sides, pores on top and bottom.
                    else if (Buscar_Propiedad("down: true", Lista_Propiedades) &&
                        Buscar_Propiedad("east: true", Lista_Propiedades) &&
                        Buscar_Propiedad("north: true", Lista_Propiedades) &&
                        Buscar_Propiedad("south: true", Lista_Propiedades) &&
                        //Buscar_Propiedad("up: true", Lista_Propiedades) &&
                        Buscar_Propiedad("west: true", Lista_Propiedades)) Data = 14; // Cap texture on all six sides.
                                                                                      //else if (Buscar_Propiedad("down: false", Lista_Propiedades) &&
                                                                                      //Buscar_Propiedad("east: false", Lista_Propiedades) &&
                                                                                      //Buscar_Propiedad("north: false", Lista_Propiedades) &&
                                                                                      //Buscar_Propiedad("south: false", Lista_Propiedades) &&
                                                                                      //Buscar_Propiedad("up: false", Lista_Propiedades) &&
                                                                                      //Buscar_Propiedad("west: false", Lista_Propiedades)) Data = 15; // Stem texture on all six sides.
                    else Data = 0;
                }
                else if (string.Compare(Nombre_1_13, "minecraft:pumpkin_stem", true) == 0) // ID: 104.
                {
                    if (Buscar_Propiedad("age: 0", Lista_Propiedades)) Data = 0; // Freshly planted stem.
                    else if (Buscar_Propiedad("age: 1", Lista_Propiedades)) Data = 1; // First stage of growth.
                    else if (Buscar_Propiedad("age: 2", Lista_Propiedades)) Data = 2; // Second stage of growth.
                    else if (Buscar_Propiedad("age: 3", Lista_Propiedades)) Data = 3; // Third stage of growth.
                    else if (Buscar_Propiedad("age: 4", Lista_Propiedades)) Data = 4; // Fourth stage of growth.
                    else if (Buscar_Propiedad("age: 5", Lista_Propiedades)) Data = 5; // Fifth stage of growth.
                    else if (Buscar_Propiedad("age: 6", Lista_Propiedades)) Data = 6; // Sixth stage of growth.
                    else if (Buscar_Propiedad("age: 7", Lista_Propiedades)) Data = 7; // Seventh stage of growth.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:attached_pumpkin_stem", true) == 0) // ID: 104.
                {
                    Data = 7; // Seventh stage of growth.

                    ID = 104; // Change the ID to pumpkin stem.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:melon_stem", true) == 0) // ID: 105.
                {
                    if (Buscar_Propiedad("age: 0", Lista_Propiedades)) Data = 0; // Freshly planted stem.
                    else if (Buscar_Propiedad("age: 1", Lista_Propiedades)) Data = 1; // First stage of growth.
                    else if (Buscar_Propiedad("age: 2", Lista_Propiedades)) Data = 2; // Second stage of growth.
                    else if (Buscar_Propiedad("age: 3", Lista_Propiedades)) Data = 3; // Third stage of growth.
                    else if (Buscar_Propiedad("age: 4", Lista_Propiedades)) Data = 4; // Fourth stage of growth.
                    else if (Buscar_Propiedad("age: 5", Lista_Propiedades)) Data = 5; // Fifth stage of growth.
                    else if (Buscar_Propiedad("age: 6", Lista_Propiedades)) Data = 6; // Sixth stage of growth.
                    else if (Buscar_Propiedad("age: 7", Lista_Propiedades)) Data = 7; // Seventh stage of growth.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:attached_melon_stem", true) == 0) // ID: 105.
                {
                    Data = 7; // Seventh stage of growth.

                    ID = 105; // Change the ID to melon stem.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:vine", true) == 0) // ID: 106.
                {
                    if (Buscar_Propiedad("south: true", Lista_Propiedades)) Data |= 1; // south.
                    if (Buscar_Propiedad("west: true", Lista_Propiedades)) Data |= 2; // west.
                    if (Buscar_Propiedad("north: true", Lista_Propiedades)) Data |= 4; // north.
                    if (Buscar_Propiedad("east: true", Lista_Propiedades)) Data |= 8; // east.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:acacia_fence_gate", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:birch_fence_gate", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:dark_oak_fence_gate", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:jungle_fence_gate", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:oak_fence_gate", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:spruce_fence_gate", true) == 0) // ID: 107.
                {
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // Facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // Facing west.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Facing north.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Facing east.

                    if (Buscar_Propiedad("open: true", Lista_Propiedades)) Data |= 4; // 0 if the gate is closed, 1 if open.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:prismarine_brick_stairs", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:prismarine_bricks_stairs", true) == 0) // ID: 109.
                {
                    if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 0; // East.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // West.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 2; // South.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 3; // North.

                    if (Buscar_Propiedad("half: top", Lista_Propiedades)) Data |= !Variable_Mundo_Invertido ? (byte)4 : (byte)0; // Set if stairs are upside-down.
                    else Data |= !Variable_Mundo_Invertido ? (byte)0 : (byte)4;

                    ID = 109; // Change the ID to stone brick stairs.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:dark_prismarine_stairs", true) == 0) // ID: 114.
                {
                    if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 0; // East.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // West.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 2; // South.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 3; // North.

                    if (Buscar_Propiedad("half: top", Lista_Propiedades)) Data |= !Variable_Mundo_Invertido ? (byte)4 : (byte)0; // Set if stairs are upside-down.
                    else Data |= !Variable_Mundo_Invertido ? (byte)0 : (byte)4;

                    ID = 114; // Change the ID to nether brick stairs.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:nether_wart", true) == 0) // ID: 115.
                {
                    if (Buscar_Propiedad("age: 0", Lista_Propiedades)) Data = 0; // Age 0.
                    else if (Buscar_Propiedad("age: 1", Lista_Propiedades)) Data = 1; // Age 1.
                    else if (Buscar_Propiedad("age: 2", Lista_Propiedades)) Data = 2; // Age 2.
                    else if (Buscar_Propiedad("age: 3", Lista_Propiedades)) Data = 3; // Age 3.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:brewing_stand", true) == 0) // ID: 117.
                {
                    if (Buscar_Propiedad("has_bottle_0: true", Lista_Propiedades)) Data |= 1; // The slot pointing east.
                    if (Buscar_Propiedad("has_bottle_2: true", Lista_Propiedades)) Data |= 2; // The slot pointing southwest.
                    if (Buscar_Propiedad("has_bottle_1: true", Lista_Propiedades)) Data |= 4; // The slot pointing northwest.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:cauldron", true) == 0) // ID: 118.
                {
                    if (Buscar_Propiedad("level: 0", Lista_Propiedades)) Data = 0; // Empty.
                    else if (Buscar_Propiedad("level: 1", Lista_Propiedades)) Data = 1; // ⅓ filled.
                    else if (Buscar_Propiedad("level: 2", Lista_Propiedades)) Data = 2; // ⅔ filled.
                    else if (Buscar_Propiedad("level: 3", Lista_Propiedades)) Data = 3; // Fully filled.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:end_portal_frame", true) == 0) // ID: 120.
                {
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // To the south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // To the west.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // To the north.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // To the east.

                    if (Buscar_Propiedad("eye: true", Lista_Propiedades)) Data |= 4; // 0x4 is a bit flag: 0 is an "empty" frame block, 1 is a block with an Eye of Ender inserted.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:redstone_lamp", true) == 0) // ID: 123 ~ 124.
                {
                    if (Buscar_Propiedad("lit: true", Lista_Propiedades)) ID = 124; // Lit.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:cocoa", true) == 0) // ID: 127.
                {
                    // The directions are inverted, which is confusing.
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // Attached to the north.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // Attached to the east.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Attached to the south.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Attached to the west.

                    if (Buscar_Propiedad("age: 0", Lista_Propiedades)) Data |= 0; // First stage.
                    else if (Buscar_Propiedad("age: 1", Lista_Propiedades)) Data |= 4; // Second stage.
                    else if (Buscar_Propiedad("age: 2", Lista_Propiedades)) Data |= 8; // Final stage.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:tripwire_hook", true) == 0) // ID: 131.
                {
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // Tripwire hook on block side facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // Tripwire hook on block side facing west.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Tripwire hook on block side facing north.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Tripwire hook on block side facing east.

                    if (Buscar_Propiedad("attached: true", Lista_Propiedades)) Data |= 4; // If set, the tripwire hook is connected and ready to trip ("middle" position).

                    if (Buscar_Propiedad("powered: true", Lista_Propiedades)) Data |= 8; // If set, the tripwire hook is currently activated ("down" position).
                }
                else if (string.Compare(Nombre_1_13, "minecraft:tripwire", true) == 0) // ID: 132.
                {
                    if (Buscar_Propiedad("powered: true", Lista_Propiedades)) Data |= 1; // Set if tripwire is activated (an entity is intersecting its collision mask).
                    //if (Buscar_Propiedad("", Lista_Propiedades)) Data |= 2; // Unused.
                    if (Buscar_Propiedad("attached: true", Lista_Propiedades)) Data |= 4; // Set if tripwire is attached to a valid tripwire circuit.
                    if (Buscar_Propiedad("disarmed: true", Lista_Propiedades)) Data |= 8; // Set if tripwire is disarmed.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:creeper_head", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:dragon_head", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:player_head", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:skeleton_skull", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:wither_skeleton_skull", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:zombie_head", true) == 0) // ID: 144.
                {
                    Data = 1; // On the floor (rotation is stored in the tile entity).
                }
                else if (string.Compare(Nombre_1_13, "minecraft:creeper_wall_head", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:dragon_wall_head", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:player_wall_head", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:skeleton_wall_skull", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:wither_skeleton_wall_skull", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:zombie_wall_head", true) == 0) // ID: 144.
                {
                    if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // On a wall, facing north.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // On a wall, facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // On a wall, facing east.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // On a wall, facing west.

                    ID = 144; // Needs a change of ID.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:anvil", true) == 0) // ID: 145.
                {
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // Anvil facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // Anvil facing west.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Anvil facing north.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Anvil facing east.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:chipped_anvil", true) == 0) // ID: 145.
                {
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // Anvil facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // Anvil facing west.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Anvil facing north.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Anvil facing east.

                    Data |= 4; // Slightly Damaged Anvil.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:damaged_anvil", true) == 0) // ID: 145.
                {
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // Anvil facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // Anvil facing west.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Anvil facing north.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Anvil facing east.

                    Data |= 8; // Very Damaged Anvil.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:comparator", true) == 0) // ID: 149 ~ 150.
                {
                    // If it was an item in a hopper, after the conversion it will be gone,
                    // and then the comparator will still be on, and after putting back a
                    // new item in the hopper, it will be off forever, and finally needing
                    // to remove it and place it back for it work again as expected...
                    // I'm not sure how I could fix this bug, so any suggestion is welcome.
                    /*if (Buscar_Propiedad("powered: true", Lista_Propiedades))
                    {
                        // Note that the arrow or triangle points in the opposite direction, which is confusing.
                        if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 0; // Facing north.
                        else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 1; // Facing east.
                        else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 2; // Facing south.
                        else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 3; // Facing west.

                        if (Buscar_Propiedad("mode: subtract", Lista_Propiedades)) Data |= 4; // Set if in subtraction mode (front torch up and powered).

                        Data |= 8; // Set if powered (at any power level).

                        ID = 150; // Powered needs a change of ID.
                    }
                    else*/
                    {
                        if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // Facing south.
                        else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // Facing west.
                        else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Facing north.
                        else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // Facing east.

                        if (Buscar_Propiedad("mode: subtract", Lista_Propiedades)) Data |= 4; // Set if in subtraction mode (front torch up and powered).
                    }
                }
                else if (string.Compare(Nombre_1_13, "minecraft:daylight_detector", true) == 0) // ID: 151 ~ 178.
                {
                    if (Buscar_Propiedad("power: 0", Lista_Propiedades)) Data = 0; // Power 0.
                    else if (Buscar_Propiedad("power: 1", Lista_Propiedades)) Data = 1; // Power 1.
                    else if (Buscar_Propiedad("power: 2", Lista_Propiedades)) Data = 2; // Power 2.
                    else if (Buscar_Propiedad("power: 3", Lista_Propiedades)) Data = 3; // Power 3.
                    else if (Buscar_Propiedad("power: 4", Lista_Propiedades)) Data = 4; // Power 4.
                    else if (Buscar_Propiedad("power: 5", Lista_Propiedades)) Data = 5; // Power 5.
                    else if (Buscar_Propiedad("power: 6", Lista_Propiedades)) Data = 6; // Power 6.
                    else if (Buscar_Propiedad("power: 7", Lista_Propiedades)) Data = 7; // Power 7.
                    else if (Buscar_Propiedad("power: 8", Lista_Propiedades)) Data = 8; // Power 8.
                    else if (Buscar_Propiedad("power: 9", Lista_Propiedades)) Data = 9; // Power 9.
                    else if (Buscar_Propiedad("power: 10", Lista_Propiedades)) Data = 10; // Power 10.
                    else if (Buscar_Propiedad("power: 11", Lista_Propiedades)) Data = 11; // Power 11.
                    else if (Buscar_Propiedad("power: 12", Lista_Propiedades)) Data = 12; // Power 12.
                    else if (Buscar_Propiedad("power: 13", Lista_Propiedades)) Data = 13; // Power 13.
                    else if (Buscar_Propiedad("power: 14", Lista_Propiedades)) Data = 14; // Power 14.
                    else if (Buscar_Propiedad("power: 15", Lista_Propiedades)) Data = 15; // Power 15.

                    if (Buscar_Propiedad("inverted: true", Lista_Propiedades)) ID = 178; // Inverted needs a change of ID.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:hopper", true) == 0) // ID: 154.
                {
                    if (Buscar_Propiedad("facing: down", Lista_Propiedades)) Data = 0; // Output facing down.
                                                                                       //else if (Buscar_Propiedad("facing: up", Lista_Propiedades)) Data = 1; // (unused). But, why Mojang didn't add hoppers going up?
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Output facing north.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // Output facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // Output facing west.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // Output facing east.

                    if (Buscar_Propiedad("enabled: false", Lista_Propiedades)) Data |= 8; // Set if activated/disabled.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:acacia_leaves", true) == 0) // ID: 161, 0.
                {
                    if (Buscar_Propiedad("persistent: true", Lista_Propiedades)) Data = 12; // Acacia Leaves (no decay and check decay).
                    else Data = 0; // Acacia Leaves.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:dark_oak_leaves", true) == 0) // ID: 161, 1.
                {
                    if (Buscar_Propiedad("persistent: true", Lista_Propiedades)) Data = 13; // Dark Oak Leaves (no decay and check decay).
                    else Data = 1; // Dark Oak Leaves.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:acacia_log", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:stripped_acacia_log", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:stripped_acacia_wood", true) == 0) // ID: 162, 0.
                {
                    Data = 0; // Acacia Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    ID = 162; // change the ID to log.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:acacia_bark", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:acacia_wood", true) == 0) // ID: 162, 0.
                {
                    Data = 0; // Acacia Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    Data |= 12; // Only bark.

                    ID = 162; // change the ID to log.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:dark_oak_log", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:stripped_dark_oak_log", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:stripped_dark_oak_wood", true) == 0) // ID: 162, 1.
                {
                    Data = 1; // Dark Oak Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    ID = 162; // change the ID to log.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:dark_oak_bark", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:dark_oak_wood", true) == 0) // ID: 162, 1.
                {
                    Data = 1; // Dark Oak Wood.

                    if (Buscar_Propiedad("axis: y", Lista_Propiedades)) Data |= 0; // Facing Up/Down.
                    else if (Buscar_Propiedad("axis: x", Lista_Propiedades)) Data |= 4; // Facing East/West.
                    else if (Buscar_Propiedad("axis: z", Lista_Propiedades)) Data |= 8; // Facing North/South.

                    Data |= 12; // Only bark.

                    ID = 162; // change the ID to log.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:large_fern", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:lilac", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:peony", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:rose_bush", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:sunflower", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:tall_grass", true) == 0) // ID: 175.
                {
                    if (Buscar_Propiedad("half: upper", Lista_Propiedades)) Data = 8; // Top Half of any Large Plant; low three bits 0x7 are derived from the block below.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:black_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:blue_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:brown_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:cyan_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:gray_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:green_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:light_blue_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:light_gray_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:lime_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:magenta_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:orange_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:pink_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:purple_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:red_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:white_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:yellow_banner", true) == 0) // ID: 176.
                {
                    if (Buscar_Propiedad("rotation: 0", Lista_Propiedades)) Data = 0; // Rotation 0.
                    else if (Buscar_Propiedad("rotation: 1", Lista_Propiedades)) Data = 1; // Rotation 1.
                    else if (Buscar_Propiedad("rotation: 2", Lista_Propiedades)) Data = 2; // Rotation 2.
                    else if (Buscar_Propiedad("rotation: 3", Lista_Propiedades)) Data = 3; // Rotation 3.
                    else if (Buscar_Propiedad("rotation: 4", Lista_Propiedades)) Data = 4; // Rotation 4.
                    else if (Buscar_Propiedad("rotation: 5", Lista_Propiedades)) Data = 5; // Rotation 5.
                    else if (Buscar_Propiedad("rotation: 6", Lista_Propiedades)) Data = 6; // Rotation 6.
                    else if (Buscar_Propiedad("rotation: 7", Lista_Propiedades)) Data = 7; // Rotation 7.
                    else if (Buscar_Propiedad("rotation: 8", Lista_Propiedades)) Data = 8; // Rotation 8.
                    else if (Buscar_Propiedad("rotation: 9", Lista_Propiedades)) Data = 9; // Rotation 9.
                    else if (Buscar_Propiedad("rotation: 10", Lista_Propiedades)) Data = 10; // Rotation 10.
                    else if (Buscar_Propiedad("rotation: 11", Lista_Propiedades)) Data = 11; // Rotation 11.
                    else if (Buscar_Propiedad("rotation: 12", Lista_Propiedades)) Data = 12; // Rotation 12.
                    else if (Buscar_Propiedad("rotation: 13", Lista_Propiedades)) Data = 13; // Rotation 13.
                    else if (Buscar_Propiedad("rotation: 14", Lista_Propiedades)) Data = 14; // Rotation 14.
                    else if (Buscar_Propiedad("rotation: 15", Lista_Propiedades)) Data = 15; // Rotation 15.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:black_wall_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:blue_wall_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:brown_wall_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:cyan_wall_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:gray_wall_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:green_wall_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:light_blue_wall_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:light_gray_wall_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:lime_wall_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:magenta_wall_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:orange_wall_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:pink_wall_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:purple_wall_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:red_wall_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:wall_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:white_wall_banner", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:yellow_wall_banner", true) == 0) // ID: 177.
                {
                    if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // north.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // west.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // east.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:end_rod", true) == 0) // ID: 198.
                {
                    if (Buscar_Propiedad("facing: down", Lista_Propiedades)) Data = 0; // Facing down.
                    else if (Buscar_Propiedad("facing: up", Lista_Propiedades)) Data = 1; // Facing up.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Facing north.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // Facing south.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // Facing west.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // Facing east.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:chorus_flower", true) == 0) // ID: 200.
                {
                    if (Buscar_Propiedad("age: 0", Lista_Propiedades)) Data = 0000; // Age 0.
                    else if (Buscar_Propiedad("age: 1", Lista_Propiedades)) Data = 0000; // Age 1.
                    else if (Buscar_Propiedad("age: 2", Lista_Propiedades)) Data = 0000; // Age 2.
                    else if (Buscar_Propiedad("age: 3", Lista_Propiedades)) Data = 0000; // Age 3.
                    else if (Buscar_Propiedad("age: 4", Lista_Propiedades)) Data = 0000; // Age 4.
                    else if (Buscar_Propiedad("age: 5", Lista_Propiedades)) Data = 0000; // Age 5, the data value denotes its age, it will not grow anymore when data value is 0x5.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:beetroots", true) == 0) // ID: 207.
                {
                    if (Buscar_Propiedad("age: 0", Lista_Propiedades)) Data = 0; // Age 0.
                    else if (Buscar_Propiedad("age: 1", Lista_Propiedades)) Data = 1; // Age 1.
                    else if (Buscar_Propiedad("age: 2", Lista_Propiedades)) Data = 2; // Age 2.
                    else if (Buscar_Propiedad("age: 3", Lista_Propiedades)) Data = 3; // Age 3.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:observer", true) == 0) // ID: 218.
                {
                    // The directions seem to be inverted, which is confusing.
                    if (Buscar_Propiedad("facing: down", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)0 : (byte)1; // Facing down.
                    else if (Buscar_Propiedad("facing: up", Lista_Propiedades)) Data = !Variable_Mundo_Invertido ? (byte)1 : (byte)0; // Facing up.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // Facing south.
                    else if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 3; // Facing north.
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 4; // Facing east.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 5; // Facing west.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:black_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:blue_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:brown_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:cyan_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:gray_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:green_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:light_blue_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:light_gray_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:lime_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:magenta_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:orange_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:pink_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:purple_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:red_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:white_glazed_terracotta", true) == 0 ||
                    string.Compare(Nombre_1_13, "minecraft:yellow_glazed_terracotta", true) == 0) // ID: 235 ~ 250.
                {
                    if (Buscar_Propiedad("facing: south", Lista_Propiedades)) Data = 0; // south (the player was facing north when this block was placed).
                    else if (Buscar_Propiedad("facing: west", Lista_Propiedades)) Data = 1; // west.
                    else if (Buscar_Propiedad("facing: north", Lista_Propiedades)) Data = 2; // north.
                    else if (Buscar_Propiedad("facing: east", Lista_Propiedades)) Data = 3; // east.
                }
                else if (string.Compare(Nombre_1_13, "minecraft:structure_block", true) == 0) // ID: 255.
                {
                    if (Buscar_Propiedad("mode: save", Lista_Propiedades)) Data = 0; // Save.
                    else if (Buscar_Propiedad("mode: load", Lista_Propiedades)) Data = 1; // Load.
                    else if (Buscar_Propiedad("mode: corner", Lista_Propiedades)) Data = 2; // Corner.
                    else if (Buscar_Propiedad("mode: data", Lista_Propiedades)) Data = 3; // Data.
                }
                return ID;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            Data = 0; // Default since something went wrong.
            return 0; // Air.
        }

        /// <summary>
        /// Searches for a property value inside the list of properties, ignoring the letter case.
        /// </summary>
        /// <param name="Propiedad">A text string with the property to search for.</param>
        /// <param name="Lista_Propiedades">A list with the NBT properties of a Minecraft 1.13+ block in the form of text strings.</param>
        /// <returns>Returns true if the list contains the selected property. Returns false toherwise.</returns>
        internal static bool Buscar_Propiedad(string Propiedad, List<string> Lista_Propiedades)
        {
            try
            {
                //if (Lista_Propiedades != null && Lista_Propiedades.Count > 0) // Checked before.
                {
                    foreach (string Texto_Propiedad in Lista_Propiedades)
                    {
                        if (string.Compare(Texto_Propiedad, Propiedad, true) == 0)
                        {
                            return true; // Found the wanted property.
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return false; // Wanted property not found.
        }

        /// <summary>
        /// The list with all the possible properties of the blocks in Minecraft 1.13.1, extracted directly from a debug world (see the secrets on the help viewer by pressing F1 to know how to create one), which should've created all the possible values at once. Here the values have been sorted and all include at least one block name that uses every property for a better understanding. This is also very useful to know which properties and what values can be expected, and also for converting between 1.12.2- and 1.13+. But note that maybe not all the blocks that share a common value like "age" might have the same value range between them, so be aware of that.
        /// </summary>
        internal static readonly List<string> Lista_Propiedades_Únicas = new List<string>(new string[]
        {
            "age: 0", // sugar_cane.
            "age: 1", // sugar_cane.
            "age: 2", // fire.
            "age: 3", // sugar_cane.
            "age: 4", // sugar_cane.
            "age: 5", // fire.
            "age: 6", // potatoes.
            "age: 7", // potatoes.
            "age: 8", // fire.
            "age: 9", // sugar_cane.
            "age: 10", // sugar_cane.
            "age: 11", // fire.
            "age: 12", // sugar_cane.
            "age: 13", // fire.
            "age: 14", // fire.
            "age: 15", // cactus.
            "age: 16", // kelp.
            "age: 17", // kelp.
            "age: 18", // kelp.
            "age: 19", // kelp.
            "age: 20", // kelp.
            "age: 21", // kelp.
            "age: 22", // kelp.
            "age: 23", // kelp.
            "age: 24", // kelp.
            "age: 25", // kelp.
            "attached: false", // tripwire.
            "attached: true", // tripwire_hook.
            "axis: x", // stripped_birch_log.
            "axis: y", // stripped_birch_log.
            "axis: z", // stripped_birch_log.
            "bites: 0", // cake.
            "bites: 1", // cake.
            "bites: 2", // cake.
            "bites: 3", // cake.
            "bites: 4", // cake.
            "bites: 5", // cake.
            "bites: 6", // cake.
            "conditional: false", // chain_command_block.
            "conditional: true", // command_block.
            "delay: 1", // repeater.
            "delay: 2", // repeater.
            "delay: 3", // repeater.
            "delay: 4", // repeater.
            "disarmed: false", // tripwire.
            "disarmed: true", // tripwire.
            "distance: 1", // jungle_leaves.
            "distance: 2", // jungle_leaves.
            "distance: 3", // jungle_leaves.
            "distance: 4", // jungle_leaves.
            "distance: 5", // jungle_leaves.
            "distance: 6", // jungle_leaves.
            "distance: 7", // jungle_leaves.
            "down: false", // red_mushroom_block.
            "down: true", // brown_mushroom_block.
            "drag: false", // bubble_column.
            "drag: true", // bubble_column.
            "east: false", // fire.
            "east: none", // redstone_wire.
            "east: side", // redstone_wire.
            "east: true", // fire.
            "east: up", // redstone_wire.
            "eggs: 1", // turtle_egg.
            "eggs: 2", // turtle_egg.
            "eggs: 3", // turtle_egg.
            "eggs: 4", // turtle_egg.
            "enabled: false", // hopper.
            "enabled: true", // hopper.
            "extended: false", // sticky_piston.
            "extended: true", // sticky_piston.
            "eye: false", // end_portal_frame.
            "eye: true", // end_portal_frame.
            "face: ceiling", // jungle_button.
            "face: floor", // oak_button.
            "face: wall", // oak_button.
            "facing: down", // end_rod.
            "facing: east", // lime_bed.
            "facing: north", // white_bed.
            "facing: south", // blue_bed.
            "facing: up", // end_rod.
            "facing: west", // lime_bed.
            "half: bottom", // oak_stairs.
            "half: lower", // iron_door.
            "half: top", // oak_stairs.
            "half: upper", // oak_door.
            "has_bottle_0: false", // brewing_stand.
            "has_bottle_0: true", // brewing_stand.
            "has_bottle_1: false", // brewing_stand.
            "has_bottle_1: true", // brewing_stand.
            "has_bottle_2: false", // brewing_stand.
            "has_bottle_2: true", // brewing_stand.
            "has_record: false", // jukebox.
            "has_record: true", // jukebox.
            "hatch: 0", // turtle_egg.
            "hatch: 1", // turtle_egg.
            "hatch: 2", // turtle_egg.
            "hinge: left", // oak_door.
            "hinge: right", // oak_door.
            "in_wall: false", // jungle_fence_gate.
            "in_wall: true", // jungle_fence_gate.
            "instrument: basedrum", // note_block.
            "instrument: bass", // note_block.
            "instrument: bell", // note_block.
            "instrument: chime", // note_block.
            "instrument: flute", // note_block.
            "instrument: guitar", // note_block.
            "instrument: harp", // note_block.
            "instrument: hat", // note_block.
            "instrument: snare", // note_block.
            "instrument: xylophone", // note_block.
            "inverted: false", // daylight_detector.
            "inverted: true", // daylight_detector.
            "layers: 1", // snow.
            "layers: 2", // snow.
            "layers: 3", // snow.
            "layers: 4", // snow.
            "layers: 5", // snow.
            "layers: 6", // snow.
            "layers: 7", // snow.
            "layers: 8", // snow.
            "level: 0", // water.
            "level: 1", // water.
            "level: 2", // water.
            "level: 3", // water.
            "level: 4", // water.
            "level: 5", // water.
            "level: 6", // water.
            "level: 7", // water.
            "level: 8", // water.
            "level: 9", // water.
            "level: 10", // water.
            "level: 11", // water.
            "level: 12", // water.
            "level: 13", // water.
            "level: 14", // water.
            "level: 15", // water.
            "lit: false", // furnace.
            "lit: true", // furnace.
            "locked: false", // repeater.
            "locked: true", // repeater.
            "mode: compare", // comparator.
            "mode: corner", // structure_block.
            "mode: data", // structure_block.
            "mode: load", // structure_block.
            "mode: save", // structure_block.
            "mode: subtract", // comparator.
            "moisture: 0", // farmland.
            "moisture: 1", // farmland.
            "moisture: 2", // farmland.
            "moisture: 3", // farmland.
            "moisture: 4", // farmland.
            "moisture: 5", // farmland.
            "moisture: 6", // farmland.
            "moisture: 7", // farmland.
            "north: false", // fire.
            "north: none", // redstone_wire.
            "north: side", // redstone_wire.
            "north: true", // fire.
            "north: up", // redstone_wire.
            "note: 0", // note_block.
            "note: 1", // note_block.
            "note: 2", // note_block.
            "note: 3", // note_block.
            "note: 4", // note_block.
            "note: 5", // note_block.
            "note: 6", // note_block.
            "note: 7", // note_block.
            "note: 8", // note_block.
            "note: 9", // note_block.
            "note: 10", // note_block.
            "note: 11", // note_block.
            "note: 12", // note_block.
            "note: 13", // note_block.
            "note: 14", // note_block.
            "note: 15", // note_block.
            "note: 16", // note_block.
            "note: 17", // note_block.
            "note: 18", // note_block.
            "note: 19", // note_block.
            "note: 20", // note_block.
            "note: 21", // note_block.
            "note: 22", // note_block.
            "note: 23", // note_block.
            "note: 24", // note_block.
            "occupied: false", // blue_bed.
            "occupied: true", // lime_bed.
            "open: false", // oak_door.
            "open: true", // iron_door.
            "part: foot", // lime_bed.
            "part: head", // blue_bed.
            "persistent: false", // jungle_leaves.
            "persistent: true", // jungle_leaves.
            "pickles: 1", // sea_pickle.
            "pickles: 2", // sea_pickle.
            "pickles: 3", // sea_pickle.
            "pickles: 4", // sea_pickle.
            "power: 0", // redstone_wire.
            "power: 1", // redstone_wire.
            "power: 2", // redstone_wire.
            "power: 3", // redstone_wire.
            "power: 4", // redstone_wire.
            "power: 5", // redstone_wire.
            "power: 6", // redstone_wire.
            "power: 7", // redstone_wire.
            "power: 8", // redstone_wire.
            "power: 9", // redstone_wire.
            "power: 10", // redstone_wire.
            "power: 11", // redstone_wire.
            "power: 12", // redstone_wire.
            "power: 13", // redstone_wire.
            "power: 14", // redstone_wire.
            "power: 15", // redstone_wire.
            "powered: false", // note_block.
            "powered: true", // note_block.
            "rotation: 0", // sign.
            "rotation: 1", // zombie_head.
            "rotation: 2", // zombie_head.
            "rotation: 3", // sign.
            "rotation: 4", // sign.
            "rotation: 5", // green_banner.
            "rotation: 6", // green_banner.
            "rotation: 7", // green_banner.
            "rotation: 8", // gray_banner.
            "rotation: 9", // gray_banner.
            "rotation: 10", // gray_banner.
            "rotation: 11", // orange_banner.
            "rotation: 12", // orange_banner.
            "rotation: 13", // orange_banner.
            "rotation: 14", // orange_banner.
            "rotation: 15", // wither_skeleton_skull.
            "shape: ascending_east", // detector_rail.
            "shape: ascending_north", // detector_rail.
            "shape: ascending_south", // detector_rail.
            "shape: ascending_west", // detector_rail.
            "shape: east_west", // detector_rail.
            "shape: inner_left", // oak_stairs.
            "shape: inner_right", // oak_stairs.
            "shape: north_east", // rail.
            "shape: north_south", // activator_rail.
            "shape: north_west", // rail.
            "shape: outer_left", // oak_stairs.
            "shape: outer_right", // oak_stairs.
            "shape: south_east", // rail.
            "shape: south_west", // rail.
            "shape: straight", // oak_stairs.
            "short: false", // piston_head.
            "short: true", // piston_head.
            "signal_fire: false",
            "signal_fire: true",
            "snowy: false", // grass_block.
            "snowy: true", // grass_block.
            "south: false", // fire.
            "south: none", // redstone_wire.
            "south: side", // redstone_wire.
            "south: true", // fire.
            "south: up", // redstone_wire.
            "stage: 0", // oak_sapling.
            "stage: 1", // oak_sapling.
            "triggered: false", // dropper.
            "triggered: true", // dropper.
            "type: bottom", // purpur_slab.
            "type: double", // red_sandstone_slab.
            "type: left", // trapped_chest.
            "type: normal", // piston_head.
            "type: right", // trapped_chest.
            "type: single", // trapped_chest.
            "type: sticky", // piston_head.
            "type: top", // purpur_slab.
            "unstable: false", // tnt.
            "unstable: true", // tnt.
            "up: false", // fire.
            "up: true", // fire.
            "waterlogged: false", // oak_stairs.
            "waterlogged: true", // oak_stairs.
            "west: false", // fire.
            "west: none", // redstone_wire.
            "west: side", // redstone_wire.
            "west: true", // fire.
            "west: up" // redstone_wire.
        });
    }
}
