using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Tools
{
    internal static class Packs_Recursos
    {
        // pack_format: Pack version. If this number does not match the current required number,
        // the resource pack will display an error and required additional confirmation to load the
        // pack. Requires 1 for 1.6-1.9, 2 for 1.9 and 1.10, 3 for 1.11 and 1.12, and 4 for 1.13.

        /// <summary>
        /// Array that stores at once all the names of the folders and files of all the resource pack versions of Minecraft (since 1.6.4 to 1.13.2). With this will be possible to convert resource packs for one pack version to any other at will.
        /// </summary>
        internal static readonly Carpetas[] Matriz_Carpetas = new Carpetas[]
        {
            new Carpetas
            (
                "assets\\minecraft\\mcpatcher",
                "assets\\minecraft\\mcpatcher",
                "assets\\minecraft\\mcpatcher",
                "assets\\minecraft\\optifine",
                new Archivos[]
                {

                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\blocks",
                "assets\\minecraft\\textures\\blocks",
                "assets\\minecraft\\textures\\blocks",
                "assets\\minecraft\\textures\\block",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\door_acacia_lower.png",
                        "assets\\minecraft\\textures\\blocks\\door_acacia_lower.png",
                        "assets\\minecraft\\textures\\blocks\\door_acacia_lower.png",
                        "assets\\minecraft\\textures\\block\\acacia_door_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\door_acacia_upper.png",
                        "assets\\minecraft\\textures\\blocks\\door_acacia_upper.png",
                        "assets\\minecraft\\textures\\blocks\\door_acacia_upper.png",
                        "assets\\minecraft\\textures\\block\\acacia_door_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\leaves_acacia.png",
                        "assets\\minecraft\\textures\\blocks\\leaves_acacia.png",
                        "assets\\minecraft\\textures\\blocks\\leaves_acacia.png",
                        "assets\\minecraft\\textures\\block\\acacia_leaves.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\log_acacia.png",
                        "assets\\minecraft\\textures\\blocks\\log_acacia.png",
                        "assets\\minecraft\\textures\\blocks\\log_acacia.png",
                        "assets\\minecraft\\textures\\block\\acacia_log.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\log_acacia_top.png",
                        "assets\\minecraft\\textures\\blocks\\log_acacia_top.png",
                        "assets\\minecraft\\textures\\blocks\\log_acacia_top.png",
                        "assets\\minecraft\\textures\\block\\acacia_log_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\planks_acacia.png",
                        "assets\\minecraft\\textures\\blocks\\planks_acacia.png",
                        "assets\\minecraft\\textures\\blocks\\planks_acacia.png",
                        "assets\\minecraft\\textures\\block\\acacia_planks.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\sapling_acacia.png",
                        "assets\\minecraft\\textures\\blocks\\sapling_acacia.png",
                        "assets\\minecraft\\textures\\blocks\\sapling_acacia.png",
                        "assets\\minecraft\\textures\\block\\acacia_sapling.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\acacia_trapdoor.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\rail_activator.png",
                        "assets\\minecraft\\textures\\blocks\\rail_activator.png",
                        "assets\\minecraft\\textures\\blocks\\rail_activator.png",
                        "assets\\minecraft\\textures\\block\\activator_rail.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\rail_activator_powered.png",
                        "assets\\minecraft\\textures\\blocks\\rail_activator_powered.png",
                        "assets\\minecraft\\textures\\blocks\\rail_activator_powered.png",
                        "assets\\minecraft\\textures\\block\\activator_rail_on.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\flower_allium.png",
                        "assets\\minecraft\\textures\\blocks\\flower_allium.png",
                        "assets\\minecraft\\textures\\blocks\\flower_allium.png",
                        "assets\\minecraft\\textures\\block\\allium.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\stone_andesite.png",
                        "assets\\minecraft\\textures\\blocks\\stone_andesite.png",
                        "assets\\minecraft\\textures\\blocks\\stone_andesite.png",
                        "assets\\minecraft\\textures\\block\\andesite.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\anvil_base.png",
                        "assets\\minecraft\\textures\\blocks\\anvil_base.png",
                        "assets\\minecraft\\textures\\blocks\\anvil_base.png",
                        "assets\\minecraft\\textures\\block\\anvil.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\anvil_top_damaged_0.png",
                        "assets\\minecraft\\textures\\blocks\\anvil_top_damaged_0.png",
                        "assets\\minecraft\\textures\\blocks\\anvil_top_damaged_0.png",
                        "assets\\minecraft\\textures\\block\\anvil_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\melon_stem_connected.png",
                        "assets\\minecraft\\textures\\blocks\\melon_stem_connected.png",
                        "assets\\minecraft\\textures\\blocks\\melon_stem_connected.png",
                        "assets\\minecraft\\textures\\block\\attached_melon_stem.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\pumpkin_stem_connected.png",
                        "assets\\minecraft\\textures\\blocks\\pumpkin_stem_connected.png",
                        "assets\\minecraft\\textures\\blocks\\pumpkin_stem_connected.png",
                        "assets\\minecraft\\textures\\block\\attached_pumpkin_stem.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\flower_houstonia.png",
                        "assets\\minecraft\\textures\\blocks\\flower_houstonia.png",
                        "assets\\minecraft\\textures\\blocks\\flower_houstonia.png",
                        "assets\\minecraft\\textures\\block\\azure_bluet.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\beacon.png",
                        "assets\\minecraft\\textures\\blocks\\beacon.png",
                        "assets\\minecraft\\textures\\blocks\\beacon.png",
                        "assets\\minecraft\\textures\\block\\beacon.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\bed_feet_end.png",
                        "assets\\minecraft\\textures\\blocks\\bed_feet_end.png",
                        "",
                        ""
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\bed_feet_side.png",
                        "assets\\minecraft\\textures\\blocks\\bed_feet_side.png",
                        "",
                        ""
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\bed_feet_top.png",
                        "assets\\minecraft\\textures\\blocks\\bed_feet_top.png",
                        "",
                        ""
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\bed_head_end.png",
                        "assets\\minecraft\\textures\\blocks\\bed_head_end.png",
                        "",
                        ""
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\bed_head_side.png",
                        "assets\\minecraft\\textures\\blocks\\bed_head_side.png",
                        "",
                        ""
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\bed_head_top.png",
                        "assets\\minecraft\\textures\\blocks\\bed_head_top.png",
                        "",
                        ""
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\bedrock.png",
                        "assets\\minecraft\\textures\\blocks\\bedrock.png",
                        "assets\\minecraft\\textures\\blocks\\bedrock.png",
                        "assets\\minecraft\\textures\\block\\bedrock.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\beetroots_stage_0.png",
                        "assets\\minecraft\\textures\\blocks\\beetroots_stage_0.png",
                        "assets\\minecraft\\textures\\block\\beetroots_stage0.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\beetroots_stage_1.png",
                        "assets\\minecraft\\textures\\blocks\\beetroots_stage_1.png",
                        "assets\\minecraft\\textures\\block\\beetroots_stage1.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\beetroots_stage_2.png",
                        "assets\\minecraft\\textures\\blocks\\beetroots_stage_2.png",
                        "assets\\minecraft\\textures\\block\\beetroots_stage2.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\beetroots_stage_3.png",
                        "assets\\minecraft\\textures\\blocks\\beetroots_stage_3.png",
                        "assets\\minecraft\\textures\\block\\beetroots_stage3.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\door_birch_lower.png",
                        "assets\\minecraft\\textures\\blocks\\door_birch_lower.png",
                        "assets\\minecraft\\textures\\blocks\\door_birch_lower.png",
                        "assets\\minecraft\\textures\\block\\birch_door_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\door_birch_upper.png",
                        "assets\\minecraft\\textures\\blocks\\door_birch_upper.png",
                        "assets\\minecraft\\textures\\blocks\\door_birch_upper.png",
                        "assets\\minecraft\\textures\\block\\birch_door_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\leaves_birch.png",
                        "assets\\minecraft\\textures\\blocks\\leaves_birch.png",
                        "assets\\minecraft\\textures\\blocks\\leaves_birch.png",
                        "assets\\minecraft\\textures\\block\\birch_leaves.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\log_birch.png",
                        "assets\\minecraft\\textures\\blocks\\log_birch.png",
                        "assets\\minecraft\\textures\\blocks\\log_birch.png",
                        "assets\\minecraft\\textures\\block\\birch_log.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\log_birch_top.png",
                        "assets\\minecraft\\textures\\blocks\\log_birch_top.png",
                        "assets\\minecraft\\textures\\blocks\\log_birch_top.png",
                        "assets\\minecraft\\textures\\block\\birch_log_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\planks_birch.png",
                        "assets\\minecraft\\textures\\blocks\\planks_birch.png",
                        "assets\\minecraft\\textures\\blocks\\planks_birch.png",
                        "assets\\minecraft\\textures\\block\\birch_planks.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\sapling_birch.png",
                        "assets\\minecraft\\textures\\blocks\\sapling_birch.png",
                        "assets\\minecraft\\textures\\blocks\\sapling_birch.png",
                        "assets\\minecraft\\textures\\block\\birch_sapling.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\birch_trapdoor.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_black.png",
                        "assets\\minecraft\\textures\\block\\black_concrete.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_powder_black.png",
                        "assets\\minecraft\\textures\\block\\black_concrete_powder.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\glazed_terracotta_black.png",
                        "assets\\minecraft\\textures\\block\\black_glazed_terracotta.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\shulker_top_black.png",
                        "assets\\minecraft\\textures\\block\\black_shulker_box.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_black.png",
                        "assets\\minecraft\\textures\\blocks\\glass_black.png",
                        "assets\\minecraft\\textures\\blocks\\glass_black.png",
                        "assets\\minecraft\\textures\\block\\black_stained_glass.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_black.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_black.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_black.png",
                        "assets\\minecraft\\textures\\block\\black_stained_glass_pane_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_black.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_black.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_black.png",
                        "assets\\minecraft\\textures\\block\\black_terracotta.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wool_colored_black.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_black.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_black.png",
                        "assets\\minecraft\\textures\\block\\black_wool.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_blue.png",
                        "assets\\minecraft\\textures\\block\\blue_concrete.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_powder_blue.png",
                        "assets\\minecraft\\textures\\block\\blue_concrete_powder.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\glazed_terracotta_blue.png",
                        "assets\\minecraft\\textures\\block\\blue_glazed_terracotta.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\blue_ice.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\flower_blue_orchid.png",
                        "assets\\minecraft\\textures\\blocks\\flower_blue_orchid.png",
                        "assets\\minecraft\\textures\\blocks\\flower_blue_orchid.png",
                        "assets\\minecraft\\textures\\block\\blue_orchid.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\shulker_top_blue.png",
                        "assets\\minecraft\\textures\\block\\blue_shulker_box.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_blue.png",
                        "assets\\minecraft\\textures\\blocks\\glass_blue.png",
                        "assets\\minecraft\\textures\\blocks\\glass_blue.png",
                        "assets\\minecraft\\textures\\block\\blue_stained_glass.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_blue.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_blue.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_blue.png",
                        "assets\\minecraft\\textures\\block\\blue_stained_glass_pane_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_blue.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_blue.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_blue.png",
                        "assets\\minecraft\\textures\\block\\blue_terracotta.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wool_colored_blue.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_blue.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_blue.png",
                        "assets\\minecraft\\textures\\block\\blue_wool.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\bone_block_side.png",
                        "assets\\minecraft\\textures\\blocks\\bone_block_side.png",
                        "assets\\minecraft\\textures\\block\\bone_block_side.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\bone_block_top.png",
                        "assets\\minecraft\\textures\\blocks\\bone_block_top.png",
                        "assets\\minecraft\\textures\\block\\bone_block_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\bookshelf.png",
                        "assets\\minecraft\\textures\\blocks\\bookshelf.png",
                        "assets\\minecraft\\textures\\blocks\\bookshelf.png",
                        "assets\\minecraft\\textures\\block\\bookshelf.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\brain_coral.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\brain_coral_block.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\brain_coral_fan.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\brewing_stand.png",
                        "assets\\minecraft\\textures\\blocks\\brewing_stand.png",
                        "assets\\minecraft\\textures\\blocks\\brewing_stand.png",
                        "assets\\minecraft\\textures\\block\\brewing_stand.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\brewing_stand_base.png",
                        "assets\\minecraft\\textures\\blocks\\brewing_stand_base.png",
                        "assets\\minecraft\\textures\\blocks\\brewing_stand_base.png",
                        "assets\\minecraft\\textures\\block\\brewing_stand_base.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\brick.png",
                        "assets\\minecraft\\textures\\blocks\\brick.png",
                        "assets\\minecraft\\textures\\blocks\\brick.png",
                        "assets\\minecraft\\textures\\block\\bricks.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_brown.png",
                        "assets\\minecraft\\textures\\block\\brown_concrete.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_powder_brown.png",
                        "assets\\minecraft\\textures\\block\\brown_concrete_powder.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\glazed_terracotta_brown.png",
                        "assets\\minecraft\\textures\\block\\brown_glazed_terracotta.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\mushroom_brown.png",
                        "assets\\minecraft\\textures\\blocks\\mushroom_brown.png",
                        "assets\\minecraft\\textures\\blocks\\mushroom_brown.png",
                        "assets\\minecraft\\textures\\block\\brown_mushroom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\mushroom_block_skin_brown.png",
                        "assets\\minecraft\\textures\\blocks\\mushroom_block_skin_brown.png",
                        "assets\\minecraft\\textures\\blocks\\mushroom_block_skin_brown.png",
                        "assets\\minecraft\\textures\\block\\brown_mushroom_block.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\shulker_top_brown.png",
                        "assets\\minecraft\\textures\\block\\brown_shulker_box.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_brown.png",
                        "assets\\minecraft\\textures\\blocks\\glass_brown.png",
                        "assets\\minecraft\\textures\\blocks\\glass_brown.png",
                        "assets\\minecraft\\textures\\block\\brown_stained_glass.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_brown.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_brown.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_brown.png",
                        "assets\\minecraft\\textures\\block\\brown_stained_glass_pane_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_brown.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_brown.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_brown.png",
                        "assets\\minecraft\\textures\\block\\brown_terracotta.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wool_colored_brown.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_brown.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_brown.png",
                        "assets\\minecraft\\textures\\block\\brown_wool.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\bubble_coral.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\bubble_coral_block.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\bubble_coral_fan.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\cactus_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\cactus_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\cactus_bottom.png",
                        "assets\\minecraft\\textures\\block\\cactus_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\cactus_side.png",
                        "assets\\minecraft\\textures\\blocks\\cactus_side.png",
                        "assets\\minecraft\\textures\\blocks\\cactus_side.png",
                        "assets\\minecraft\\textures\\block\\cactus_side.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\cactus_top.png",
                        "assets\\minecraft\\textures\\blocks\\cactus_top.png",
                        "assets\\minecraft\\textures\\blocks\\cactus_top.png",
                        "assets\\minecraft\\textures\\block\\cactus_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\cake_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\cake_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\cake_bottom.png",
                        "assets\\minecraft\\textures\\block\\cake_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\cake_inner.png",
                        "assets\\minecraft\\textures\\blocks\\cake_inner.png",
                        "assets\\minecraft\\textures\\blocks\\cake_inner.png",
                        "assets\\minecraft\\textures\\block\\cake_inner.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\cake_side.png",
                        "assets\\minecraft\\textures\\blocks\\cake_side.png",
                        "assets\\minecraft\\textures\\blocks\\cake_side.png",
                        "assets\\minecraft\\textures\\block\\cake_side.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\cake_top.png",
                        "assets\\minecraft\\textures\\blocks\\cake_top.png",
                        "assets\\minecraft\\textures\\blocks\\cake_top.png",
                        "assets\\minecraft\\textures\\block\\cake_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\carrots_stage_0.png",
                        "assets\\minecraft\\textures\\blocks\\carrots_stage_0.png",
                        "assets\\minecraft\\textures\\blocks\\carrots_stage_0.png",
                        "assets\\minecraft\\textures\\block\\carrots_stage0.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\carrots_stage_1.png",
                        "assets\\minecraft\\textures\\blocks\\carrots_stage_1.png",
                        "assets\\minecraft\\textures\\blocks\\carrots_stage_1.png",
                        "assets\\minecraft\\textures\\block\\carrots_stage1.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\carrots_stage_2.png",
                        "assets\\minecraft\\textures\\blocks\\carrots_stage_2.png",
                        "assets\\minecraft\\textures\\blocks\\carrots_stage_2.png",
                        "assets\\minecraft\\textures\\block\\carrots_stage2.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\carrots_stage_3.png",
                        "assets\\minecraft\\textures\\blocks\\carrots_stage_3.png",
                        "assets\\minecraft\\textures\\blocks\\carrots_stage_3.png",
                        "assets\\minecraft\\textures\\block\\carrots_stage3.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\pumpkin_face_off.png",
                        "assets\\minecraft\\textures\\blocks\\pumpkin_face_off.png",
                        "assets\\minecraft\\textures\\blocks\\pumpkin_face_off.png",
                        "assets\\minecraft\\textures\\block\\carved_pumpkin.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\cauldron_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\cauldron_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\cauldron_bottom.png",
                        "assets\\minecraft\\textures\\block\\cauldron_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\cauldron_inner.png",
                        "assets\\minecraft\\textures\\blocks\\cauldron_inner.png",
                        "assets\\minecraft\\textures\\blocks\\cauldron_inner.png",
                        "assets\\minecraft\\textures\\block\\cauldron_inner.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\cauldron_side.png",
                        "assets\\minecraft\\textures\\blocks\\cauldron_side.png",
                        "assets\\minecraft\\textures\\blocks\\cauldron_side.png",
                        "assets\\minecraft\\textures\\block\\cauldron_side.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\cauldron_top.png",
                        "assets\\minecraft\\textures\\blocks\\cauldron_top.png",
                        "assets\\minecraft\\textures\\blocks\\cauldron_top.png",
                        "assets\\minecraft\\textures\\block\\cauldron_top.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\chain_command_block_back.png",
                        "assets\\minecraft\\textures\\blocks\\chain_command_block_back.png",
                        "assets\\minecraft\\textures\\block\\chain_command_block_back.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\chain_command_block_back.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\chain_command_block_back.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\chain_command_block_back.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\chain_command_block_conditional.png",
                        "assets\\minecraft\\textures\\blocks\\chain_command_block_conditional.png",
                        "assets\\minecraft\\textures\\block\\chain_command_block_conditional.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\chain_command_block_conditional.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\chain_command_block_conditional.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\chain_command_block_conditional.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\chain_command_block_front.png",
                        "assets\\minecraft\\textures\\blocks\\chain_command_block_front.png",
                        "assets\\minecraft\\textures\\block\\chain_command_block_front.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\chain_command_block_front.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\chain_command_block_front.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\chain_command_block_front.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\chain_command_block_side.png",
                        "assets\\minecraft\\textures\\blocks\\chain_command_block_side.png",
                        "assets\\minecraft\\textures\\block\\chain_command_block_side.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\chain_command_block_side.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\chain_command_block_side.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\chain_command_block_side.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\anvil_top_damaged_1.png",
                        "assets\\minecraft\\textures\\blocks\\anvil_top_damaged_1.png",
                        "assets\\minecraft\\textures\\blocks\\anvil_top_damaged_1.png",
                        "assets\\minecraft\\textures\\block\\chipped_anvil_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\quartz_block_chiseled.png",
                        "assets\\minecraft\\textures\\blocks\\quartz_block_chiseled.png",
                        "assets\\minecraft\\textures\\blocks\\quartz_block_chiseled.png",
                        "assets\\minecraft\\textures\\block\\chiseled_quartz_block.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\quartz_block_chiseled_top.png",
                        "assets\\minecraft\\textures\\blocks\\quartz_block_chiseled_top.png",
                        "assets\\minecraft\\textures\\blocks\\quartz_block_chiseled_top.png",
                        "assets\\minecraft\\textures\\block\\chiseled_quartz_block_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\red_sandstone_carved.png",
                        "assets\\minecraft\\textures\\blocks\\red_sandstone_carved.png",
                        "assets\\minecraft\\textures\\blocks\\red_sandstone_carved.png",
                        "assets\\minecraft\\textures\\block\\chiseled_red_sandstone.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\sandstone_carved.png",
                        "assets\\minecraft\\textures\\blocks\\sandstone_carved.png",
                        "assets\\minecraft\\textures\\blocks\\sandstone_carved.png",
                        "assets\\minecraft\\textures\\block\\chiseled_sandstone.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\stonebrick_carved.png",
                        "assets\\minecraft\\textures\\blocks\\stonebrick_carved.png",
                        "assets\\minecraft\\textures\\blocks\\stonebrick_carved.png",
                        "assets\\minecraft\\textures\\block\\chiseled_stone_bricks.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\chorus_flower.png",
                        "assets\\minecraft\\textures\\blocks\\chorus_flower.png",
                        "assets\\minecraft\\textures\\block\\chorus_flower.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\chorus_flower_dead.png",
                        "assets\\minecraft\\textures\\blocks\\chorus_flower_dead.png",
                        "assets\\minecraft\\textures\\block\\chorus_flower_dead.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\chorus_plant.png",
                        "assets\\minecraft\\textures\\blocks\\chorus_plant.png",
                        "assets\\minecraft\\textures\\block\\chorus_plant.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\clay.png",
                        "assets\\minecraft\\textures\\blocks\\clay.png",
                        "assets\\minecraft\\textures\\blocks\\clay.png",
                        "assets\\minecraft\\textures\\block\\clay.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\coal_block.png",
                        "assets\\minecraft\\textures\\blocks\\coal_block.png",
                        "assets\\minecraft\\textures\\blocks\\coal_block.png",
                        "assets\\minecraft\\textures\\block\\coal_block.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\coal_ore.png",
                        "assets\\minecraft\\textures\\blocks\\coal_ore.png",
                        "assets\\minecraft\\textures\\blocks\\coal_ore.png",
                        "assets\\minecraft\\textures\\block\\coal_ore.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\coarse_dirt.png",
                        "assets\\minecraft\\textures\\blocks\\coarse_dirt.png",
                        "assets\\minecraft\\textures\\blocks\\coarse_dirt.png",
                        "assets\\minecraft\\textures\\block\\coarse_dirt.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\cobblestone.png",
                        "assets\\minecraft\\textures\\blocks\\cobblestone.png",
                        "assets\\minecraft\\textures\\blocks\\cobblestone.png",
                        "assets\\minecraft\\textures\\block\\cobblestone.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\web.png",
                        "assets\\minecraft\\textures\\blocks\\web.png",
                        "assets\\minecraft\\textures\\blocks\\web.png",
                        "assets\\minecraft\\textures\\block\\cobweb.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\cocoa_stage_0.png",
                        "assets\\minecraft\\textures\\blocks\\cocoa_stage_0.png",
                        "assets\\minecraft\\textures\\blocks\\cocoa_stage_0.png",
                        "assets\\minecraft\\textures\\block\\cocoa_stage0.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\cocoa_stage_1.png",
                        "assets\\minecraft\\textures\\blocks\\cocoa_stage_1.png",
                        "assets\\minecraft\\textures\\blocks\\cocoa_stage_1.png",
                        "assets\\minecraft\\textures\\block\\cocoa_stage1.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\cocoa_stage_2.png",
                        "assets\\minecraft\\textures\\blocks\\cocoa_stage_2.png",
                        "assets\\minecraft\\textures\\blocks\\cocoa_stage_2.png",
                        "assets\\minecraft\\textures\\block\\cocoa_stage2.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\command_block_back.png",
                        "assets\\minecraft\\textures\\blocks\\command_block_back.png",
                        "assets\\minecraft\\textures\\block\\command_block_back.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\command_block_back.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\command_block_back.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\command_block_back.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\command_block_conditional.png",
                        "assets\\minecraft\\textures\\blocks\\command_block_conditional.png",
                        "assets\\minecraft\\textures\\block\\command_block_conditional.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\command_block_conditional.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\command_block_conditional.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\command_block_conditional.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\command_block.png",
                        "assets\\minecraft\\textures\\blocks\\command_block_front.png",
                        "assets\\minecraft\\textures\\blocks\\command_block_front.png",
                        "assets\\minecraft\\textures\\block\\command_block_front.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\command_block_front.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\command_block_front.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\command_block_front.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\command_block_side.png",
                        "assets\\minecraft\\textures\\blocks\\command_block_side.png",
                        "assets\\minecraft\\textures\\block\\command_block_side.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\command_block_side.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\command_block_side.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\command_block_side.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\comparator_off.png",
                        "assets\\minecraft\\textures\\blocks\\comparator_off.png",
                        "assets\\minecraft\\textures\\blocks\\comparator_off.png",
                        "assets\\minecraft\\textures\\block\\comparator.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\comparator_on.png",
                        "assets\\minecraft\\textures\\blocks\\comparator_on.png",
                        "assets\\minecraft\\textures\\blocks\\comparator_on.png",
                        "assets\\minecraft\\textures\\block\\comparator_on.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\conduit.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\stonebrick_cracked.png",
                        "assets\\minecraft\\textures\\blocks\\stonebrick_cracked.png",
                        "assets\\minecraft\\textures\\blocks\\stonebrick_cracked.png",
                        "assets\\minecraft\\textures\\block\\cracked_stone_bricks.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\crafting_table_front.png",
                        "assets\\minecraft\\textures\\blocks\\crafting_table_front.png",
                        "assets\\minecraft\\textures\\blocks\\crafting_table_front.png",
                        "assets\\minecraft\\textures\\block\\crafting_table_front.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\crafting_table_side.png",
                        "assets\\minecraft\\textures\\blocks\\crafting_table_side.png",
                        "assets\\minecraft\\textures\\blocks\\crafting_table_side.png",
                        "assets\\minecraft\\textures\\block\\crafting_table_side.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\crafting_table_top.png",
                        "assets\\minecraft\\textures\\blocks\\crafting_table_top.png",
                        "assets\\minecraft\\textures\\blocks\\crafting_table_top.png",
                        "assets\\minecraft\\textures\\block\\crafting_table_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\red_sandstone_smooth.png",
                        "assets\\minecraft\\textures\\blocks\\red_sandstone_smooth.png",
                        "assets\\minecraft\\textures\\blocks\\red_sandstone_smooth.png",
                        "assets\\minecraft\\textures\\block\\cut_red_sandstone.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\sandstone_smooth.png",
                        "assets\\minecraft\\textures\\blocks\\sandstone_smooth.png",
                        "assets\\minecraft\\textures\\blocks\\sandstone_smooth.png",
                        "assets\\minecraft\\textures\\block\\cut_sandstone.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_cyan.png",
                        "assets\\minecraft\\textures\\block\\cyan_concrete.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_powder_cyan.png",
                        "assets\\minecraft\\textures\\block\\cyan_concrete_powder.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\glazed_terracotta_cyan.png",
                        "assets\\minecraft\\textures\\block\\cyan_glazed_terracotta.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\shulker_top_cyan.png",
                        "assets\\minecraft\\textures\\block\\cyan_shulker_box.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_cyan.png",
                        "assets\\minecraft\\textures\\blocks\\glass_cyan.png",
                        "assets\\minecraft\\textures\\blocks\\glass_cyan.png",
                        "assets\\minecraft\\textures\\block\\cyan_stained_glass.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_cyan.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_cyan.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_cyan.png",
                        "assets\\minecraft\\textures\\block\\cyan_stained_glass_pane_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_cyan.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_cyan.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_cyan.png",
                        "assets\\minecraft\\textures\\block\\cyan_terracotta.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wool_colored_cyan.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_cyan.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_cyan.png",
                        "assets\\minecraft\\textures\\block\\cyan_wool.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\anvil_top_damaged_2.png",
                        "assets\\minecraft\\textures\\blocks\\anvil_top_damaged_2.png",
                        "assets\\minecraft\\textures\\blocks\\anvil_top_damaged_2.png",
                        "assets\\minecraft\\textures\\block\\damaged_anvil_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\flower_dandelion.png",
                        "assets\\minecraft\\textures\\blocks\\flower_dandelion.png",
                        "assets\\minecraft\\textures\\blocks\\flower_dandelion.png",
                        "assets\\minecraft\\textures\\block\\dandelion.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\door_dark_oak_lower.png",
                        "assets\\minecraft\\textures\\blocks\\door_dark_oak_lower.png",
                        "assets\\minecraft\\textures\\blocks\\door_dark_oak_lower.png",
                        "assets\\minecraft\\textures\\block\\dark_oak_door_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\door_dark_oak_upper.png",
                        "assets\\minecraft\\textures\\blocks\\door_dark_oak_upper.png",
                        "assets\\minecraft\\textures\\blocks\\door_dark_oak_upper.png",
                        "assets\\minecraft\\textures\\block\\dark_oak_door_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\leaves_big_oak.png",
                        "assets\\minecraft\\textures\\blocks\\leaves_big_oak.png",
                        "assets\\minecraft\\textures\\blocks\\leaves_big_oak.png",
                        "assets\\minecraft\\textures\\block\\dark_oak_leaves.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\log_big_oak.png",
                        "assets\\minecraft\\textures\\blocks\\log_big_oak.png",
                        "assets\\minecraft\\textures\\blocks\\log_big_oak.png",
                        "assets\\minecraft\\textures\\block\\dark_oak_log.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\log_big_oak_top.png",
                        "assets\\minecraft\\textures\\blocks\\log_big_oak_top.png",
                        "assets\\minecraft\\textures\\blocks\\log_big_oak_top.png",
                        "assets\\minecraft\\textures\\block\\dark_oak_log_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\planks_big_oak.png",
                        "assets\\minecraft\\textures\\blocks\\planks_big_oak.png",
                        "assets\\minecraft\\textures\\blocks\\planks_big_oak.png",
                        "assets\\minecraft\\textures\\block\\dark_oak_planks.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\sapling_roofed_oak.png",
                        "assets\\minecraft\\textures\\blocks\\sapling_roofed_oak.png",
                        "assets\\minecraft\\textures\\blocks\\sapling_roofed_oak.png",
                        "assets\\minecraft\\textures\\block\\dark_oak_sapling.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\dark_oak_trapdoor.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\prismarine_dark.png",
                        "assets\\minecraft\\textures\\blocks\\prismarine_dark.png",
                        "assets\\minecraft\\textures\\blocks\\prismarine_dark.png",
                        "assets\\minecraft\\textures\\block\\dark_prismarine.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\daylight_detector_inverted_top.png",
                        "assets\\minecraft\\textures\\blocks\\daylight_detector_inverted_top.png",
                        "assets\\minecraft\\textures\\blocks\\daylight_detector_inverted_top.png",
                        "assets\\minecraft\\textures\\block\\daylight_detector_inverted_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\daylight_detector_side.png",
                        "assets\\minecraft\\textures\\blocks\\daylight_detector_side.png",
                        "assets\\minecraft\\textures\\blocks\\daylight_detector_side.png",
                        "assets\\minecraft\\textures\\block\\daylight_detector_side.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\daylight_detector_top.png",
                        "assets\\minecraft\\textures\\blocks\\daylight_detector_top.png",
                        "assets\\minecraft\\textures\\blocks\\daylight_detector_top.png",
                        "assets\\minecraft\\textures\\block\\daylight_detector_top.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\dead_brain_coral.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\dead_brain_coral_block.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\dead_brain_coral_fan.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\dead_bubble_coral.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\dead_bubble_coral_block.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\dead_bubble_coral_fan.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\deadbush.png",
                        "assets\\minecraft\\textures\\blocks\\deadbush.png",
                        "assets\\minecraft\\textures\\blocks\\deadbush.png",
                        "assets\\minecraft\\textures\\block\\dead_bush.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\dead_fire_coral.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\dead_fire_coral_block.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\dead_fire_coral_fan.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\dead_horn_coral.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\dead_horn_coral_block.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\dead_horn_coral_fan.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\dead_tube_coral.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\dead_tube_coral_block.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\dead_tube_coral_fan.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\debug.png",
                        "assets\\minecraft\\textures\\blocks\\debug.png",
                        "assets\\minecraft\\textures\\block\\debug.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\debug2.png",
                        "assets\\minecraft\\textures\\blocks\\debug2.png",
                        "assets\\minecraft\\textures\\block\\debug2.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_0.png",
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_0.png",
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_0.png",
                        "assets\\minecraft\\textures\\block\\destroy_stage_0.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_1.png",
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_1.png",
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_1.png",
                        "assets\\minecraft\\textures\\block\\destroy_stage_1.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_2.png",
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_2.png",
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_2.png",
                        "assets\\minecraft\\textures\\block\\destroy_stage_2.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_3.png",
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_3.png",
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_3.png",
                        "assets\\minecraft\\textures\\block\\destroy_stage_3.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_4.png",
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_4.png",
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_4.png",
                        "assets\\minecraft\\textures\\block\\destroy_stage_4.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_5.png",
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_5.png",
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_5.png",
                        "assets\\minecraft\\textures\\block\\destroy_stage_5.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_6.png",
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_6.png",
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_6.png",
                        "assets\\minecraft\\textures\\block\\destroy_stage_6.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_7.png",
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_7.png",
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_7.png",
                        "assets\\minecraft\\textures\\block\\destroy_stage_7.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_8.png",
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_8.png",
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_8.png",
                        "assets\\minecraft\\textures\\block\\destroy_stage_8.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_9.png",
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_9.png",
                        "assets\\minecraft\\textures\\blocks\\destroy_stage_9.png",
                        "assets\\minecraft\\textures\\block\\destroy_stage_9.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\rail_detector.png",
                        "assets\\minecraft\\textures\\blocks\\rail_detector.png",
                        "assets\\minecraft\\textures\\blocks\\rail_detector.png",
                        "assets\\minecraft\\textures\\block\\detector_rail.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\rail_detector_powered.png",
                        "assets\\minecraft\\textures\\blocks\\rail_detector_powered.png",
                        "assets\\minecraft\\textures\\blocks\\rail_detector_powered.png",
                        "assets\\minecraft\\textures\\block\\detector_rail_on.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\diamond_block.png",
                        "assets\\minecraft\\textures\\blocks\\diamond_block.png",
                        "assets\\minecraft\\textures\\blocks\\diamond_block.png",
                        "assets\\minecraft\\textures\\block\\diamond_block.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\diamond_ore.png",
                        "assets\\minecraft\\textures\\blocks\\diamond_ore.png",
                        "assets\\minecraft\\textures\\blocks\\diamond_ore.png",
                        "assets\\minecraft\\textures\\block\\diamond_ore.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\stone_diorite.png",
                        "assets\\minecraft\\textures\\blocks\\stone_diorite.png",
                        "assets\\minecraft\\textures\\blocks\\stone_diorite.png",
                        "assets\\minecraft\\textures\\block\\diorite.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\dirt.png",
                        "assets\\minecraft\\textures\\blocks\\dirt.png",
                        "assets\\minecraft\\textures\\blocks\\dirt.png",
                        "assets\\minecraft\\textures\\block\\dirt.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\dispenser_front_horizontal.png",
                        "assets\\minecraft\\textures\\blocks\\dispenser_front_horizontal.png",
                        "assets\\minecraft\\textures\\blocks\\dispenser_front_horizontal.png",
                        "assets\\minecraft\\textures\\block\\dispenser_front.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\dispenser_front_vertical.png",
                        "assets\\minecraft\\textures\\blocks\\dispenser_front_vertical.png",
                        "assets\\minecraft\\textures\\blocks\\dispenser_front_vertical.png",
                        "assets\\minecraft\\textures\\block\\dispenser_front_vertical.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\dragon_egg.png",
                        "assets\\minecraft\\textures\\blocks\\dragon_egg.png",
                        "assets\\minecraft\\textures\\blocks\\dragon_egg.png",
                        "assets\\minecraft\\textures\\block\\dragon_egg.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\dried_kelp_bottom.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\dried_kelp_side.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\dried_kelp_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\dropper_front_horizontal.png",
                        "assets\\minecraft\\textures\\blocks\\dropper_front_horizontal.png",
                        "assets\\minecraft\\textures\\blocks\\dropper_front_horizontal.png",
                        "assets\\minecraft\\textures\\block\\dropper_front.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\dropper_front_vertical.png",
                        "assets\\minecraft\\textures\\blocks\\dropper_front_vertical.png",
                        "assets\\minecraft\\textures\\blocks\\dropper_front_vertical.png",
                        "assets\\minecraft\\textures\\block\\dropper_front_vertical.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\emerald_block.png",
                        "assets\\minecraft\\textures\\blocks\\emerald_block.png",
                        "assets\\minecraft\\textures\\blocks\\emerald_block.png",
                        "assets\\minecraft\\textures\\block\\emerald_block.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\emerald_ore.png",
                        "assets\\minecraft\\textures\\blocks\\emerald_ore.png",
                        "assets\\minecraft\\textures\\blocks\\emerald_ore.png",
                        "assets\\minecraft\\textures\\block\\emerald_ore.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\enchanting_table_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\enchanting_table_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\enchanting_table_bottom.png",
                        "assets\\minecraft\\textures\\block\\enchanting_table_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\enchanting_table_side.png",
                        "assets\\minecraft\\textures\\blocks\\enchanting_table_side.png",
                        "assets\\minecraft\\textures\\blocks\\enchanting_table_side.png",
                        "assets\\minecraft\\textures\\block\\enchanting_table_side.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\enchanting_table_top.png",
                        "assets\\minecraft\\textures\\blocks\\enchanting_table_top.png",
                        "assets\\minecraft\\textures\\blocks\\enchanting_table_top.png",
                        "assets\\minecraft\\textures\\block\\enchanting_table_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\endframe_eye.png",
                        "assets\\minecraft\\textures\\blocks\\endframe_eye.png",
                        "assets\\minecraft\\textures\\blocks\\endframe_eye.png",
                        "assets\\minecraft\\textures\\block\\end_portal_frame_eye.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\endframe_side.png",
                        "assets\\minecraft\\textures\\blocks\\endframe_side.png",
                        "assets\\minecraft\\textures\\blocks\\endframe_side.png",
                        "assets\\minecraft\\textures\\block\\end_portal_frame_side.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\endframe_top.png",
                        "assets\\minecraft\\textures\\blocks\\endframe_top.png",
                        "assets\\minecraft\\textures\\blocks\\endframe_top.png",
                        "assets\\minecraft\\textures\\block\\end_portal_frame_top.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\end_rod.png",
                        "assets\\minecraft\\textures\\blocks\\end_rod.png",
                        "assets\\minecraft\\textures\\block\\end_rod.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\end_stone.png",
                        "assets\\minecraft\\textures\\blocks\\end_stone.png",
                        "assets\\minecraft\\textures\\blocks\\end_stone.png",
                        "assets\\minecraft\\textures\\block\\end_stone.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\end_bricks.png",
                        "assets\\minecraft\\textures\\blocks\\end_bricks.png",
                        "assets\\minecraft\\textures\\block\\end_stone_bricks.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\farmland_dry.png",
                        "assets\\minecraft\\textures\\blocks\\farmland_dry.png",
                        "assets\\minecraft\\textures\\blocks\\farmland_dry.png",
                        "assets\\minecraft\\textures\\block\\farmland.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\farmland_wet.png",
                        "assets\\minecraft\\textures\\blocks\\farmland_wet.png",
                        "assets\\minecraft\\textures\\blocks\\farmland_wet.png",
                        "assets\\minecraft\\textures\\block\\farmland_moist.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\fern.png",
                        "assets\\minecraft\\textures\\blocks\\fern.png",
                        "assets\\minecraft\\textures\\blocks\\fern.png",
                        "assets\\minecraft\\textures\\block\\fern.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\fire_layer_0.png",
                        "assets\\minecraft\\textures\\blocks\\fire_layer_0.png",
                        "assets\\minecraft\\textures\\blocks\\fire_layer_0.png",
                        "assets\\minecraft\\textures\\block\\fire_0.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\fire_layer_0.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\fire_layer_0.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\fire_layer_0.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\fire_0.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\fire_layer_1.png",
                        "assets\\minecraft\\textures\\blocks\\fire_layer_1.png",
                        "assets\\minecraft\\textures\\blocks\\fire_layer_1.png",
                        "assets\\minecraft\\textures\\block\\fire_1.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\fire_layer_1.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\fire_layer_1.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\fire_layer_1.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\fire_1.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\fire_coral.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\fire_coral_block.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\fire_coral_fan.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\flower_paeonia.png",
                        "assets\\minecraft\\textures\\blocks\\flower_paeonia.png",
                        "assets\\minecraft\\textures\\blocks\\flower_paeonia.png",
                        ""
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\flower_pot.png",
                        "assets\\minecraft\\textures\\blocks\\flower_pot.png",
                        "assets\\minecraft\\textures\\blocks\\flower_pot.png",
                        "assets\\minecraft\\textures\\block\\flower_pot.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\frosted_ice_0.png",
                        "assets\\minecraft\\textures\\blocks\\frosted_ice_0.png",
                        "assets\\minecraft\\textures\\blocks\\frosted_ice_0.png",
                        "assets\\minecraft\\textures\\block\\frosted_ice_0.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\frosted_ice_1.png",
                        "assets\\minecraft\\textures\\blocks\\frosted_ice_1.png",
                        "assets\\minecraft\\textures\\block\\frosted_ice_1.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\frosted_ice_2.png",
                        "assets\\minecraft\\textures\\blocks\\frosted_ice_2.png",
                        "assets\\minecraft\\textures\\block\\frosted_ice_2.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\frosted_ice_3.png",
                        "assets\\minecraft\\textures\\blocks\\frosted_ice_3.png",
                        "assets\\minecraft\\textures\\block\\frosted_ice_3.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\furnace_front_off.png",
                        "assets\\minecraft\\textures\\blocks\\furnace_front_off.png",
                        "assets\\minecraft\\textures\\blocks\\furnace_front_off.png",
                        "assets\\minecraft\\textures\\block\\furnace_front.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\furnace_front_on.png",
                        "assets\\minecraft\\textures\\blocks\\furnace_front_on.png",
                        "assets\\minecraft\\textures\\blocks\\furnace_front_on.png",
                        "assets\\minecraft\\textures\\block\\furnace_front_on.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\furnace_side.png",
                        "assets\\minecraft\\textures\\blocks\\furnace_side.png",
                        "assets\\minecraft\\textures\\blocks\\furnace_side.png",
                        "assets\\minecraft\\textures\\block\\furnace_side.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\furnace_top.png",
                        "assets\\minecraft\\textures\\blocks\\furnace_top.png",
                        "assets\\minecraft\\textures\\blocks\\furnace_top.png",
                        "assets\\minecraft\\textures\\block\\furnace_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass.png",
                        "assets\\minecraft\\textures\\blocks\\glass.png",
                        "assets\\minecraft\\textures\\blocks\\glass.png",
                        "assets\\minecraft\\textures\\block\\glass.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top.png",
                        "assets\\minecraft\\textures\\block\\glass_pane_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glowstone.png",
                        "assets\\minecraft\\textures\\blocks\\glowstone.png",
                        "assets\\minecraft\\textures\\blocks\\glowstone.png",
                        "assets\\minecraft\\textures\\block\\glowstone.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\gold_block.png",
                        "assets\\minecraft\\textures\\blocks\\gold_block.png",
                        "assets\\minecraft\\textures\\blocks\\gold_block.png",
                        "assets\\minecraft\\textures\\block\\gold_block.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\gold_ore.png",
                        "assets\\minecraft\\textures\\blocks\\gold_ore.png",
                        "assets\\minecraft\\textures\\blocks\\gold_ore.png",
                        "assets\\minecraft\\textures\\block\\gold_ore.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\stone_granite.png",
                        "assets\\minecraft\\textures\\blocks\\stone_granite.png",
                        "assets\\minecraft\\textures\\blocks\\stone_granite.png",
                        "assets\\minecraft\\textures\\block\\granite.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\tallgrass.png",
                        "assets\\minecraft\\textures\\blocks\\tallgrass.png",
                        "assets\\minecraft\\textures\\blocks\\tallgrass.png",
                        "assets\\minecraft\\textures\\block\\grass.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\grass_side.png",
                        "assets\\minecraft\\textures\\blocks\\grass_side.png",
                        "assets\\minecraft\\textures\\blocks\\grass_side.png",
                        "assets\\minecraft\\textures\\block\\grass_block_side.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\grass_side_overlay.png",
                        "assets\\minecraft\\textures\\blocks\\grass_side_overlay.png",
                        "assets\\minecraft\\textures\\blocks\\grass_side_overlay.png",
                        "assets\\minecraft\\textures\\block\\grass_block_side_overlay.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\grass_side_snowed.png",
                        "assets\\minecraft\\textures\\blocks\\grass_side_snowed.png",
                        "assets\\minecraft\\textures\\blocks\\grass_side_snowed.png",
                        "assets\\minecraft\\textures\\block\\grass_block_snow.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\grass_top.png",
                        "assets\\minecraft\\textures\\blocks\\grass_top.png",
                        "assets\\minecraft\\textures\\blocks\\grass_top.png",
                        "assets\\minecraft\\textures\\block\\grass_block_top.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\grass_path_side.png",
                        "assets\\minecraft\\textures\\blocks\\grass_path_side.png",
                        "assets\\minecraft\\textures\\block\\grass_path_side.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\grass_path_top.png",
                        "assets\\minecraft\\textures\\blocks\\grass_path_top.png",
                        "assets\\minecraft\\textures\\block\\grass_path_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\gravel.png",
                        "assets\\minecraft\\textures\\blocks\\gravel.png",
                        "assets\\minecraft\\textures\\blocks\\gravel.png",
                        "assets\\minecraft\\textures\\block\\gravel.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_gray.png",
                        "assets\\minecraft\\textures\\block\\gray_concrete.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_powder_gray.png",
                        "assets\\minecraft\\textures\\block\\gray_concrete_powder.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\glazed_terracotta_gray.png",
                        "assets\\minecraft\\textures\\block\\gray_glazed_terracotta.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\shulker_top_gray.png",
                        "assets\\minecraft\\textures\\block\\gray_shulker_box.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_gray.png",
                        "assets\\minecraft\\textures\\blocks\\glass_gray.png",
                        "assets\\minecraft\\textures\\blocks\\glass_gray.png",
                        "assets\\minecraft\\textures\\block\\gray_stained_glass.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_gray.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_gray.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_gray.png",
                        "assets\\minecraft\\textures\\block\\gray_stained_glass_pane_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_gray.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_gray.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_gray.png",
                        "assets\\minecraft\\textures\\block\\gray_terracotta.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wool_colored_gray.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_gray.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_gray.png",
                        "assets\\minecraft\\textures\\block\\gray_wool.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_green.png",
                        "assets\\minecraft\\textures\\block\\green_concrete.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_powder_green.png",
                        "assets\\minecraft\\textures\\block\\green_concrete_powder.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\glazed_terracotta_green.png",
                        "assets\\minecraft\\textures\\block\\green_glazed_terracotta.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\shulker_top_green.png",
                        "assets\\minecraft\\textures\\block\\green_shulker_box.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_green.png",
                        "assets\\minecraft\\textures\\blocks\\glass_green.png",
                        "assets\\minecraft\\textures\\blocks\\glass_green.png",
                        "assets\\minecraft\\textures\\block\\green_stained_glass.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_green.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_green.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_green.png",
                        "assets\\minecraft\\textures\\block\\green_stained_glass_pane_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_green.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_green.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_green.png",
                        "assets\\minecraft\\textures\\block\\green_terracotta.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wool_colored_green.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_green.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_green.png",
                        "assets\\minecraft\\textures\\block\\green_wool.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hay_block_side.png",
                        "assets\\minecraft\\textures\\blocks\\hay_block_side.png",
                        "assets\\minecraft\\textures\\blocks\\hay_block_side.png",
                        "assets\\minecraft\\textures\\block\\hay_block_side.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hay_block_top.png",
                        "assets\\minecraft\\textures\\blocks\\hay_block_top.png",
                        "assets\\minecraft\\textures\\blocks\\hay_block_top.png",
                        "assets\\minecraft\\textures\\block\\hay_block_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hopper_inside.png",
                        "assets\\minecraft\\textures\\blocks\\hopper_inside.png",
                        "assets\\minecraft\\textures\\blocks\\hopper_inside.png",
                        "assets\\minecraft\\textures\\block\\hopper_inside.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hopper_outside.png",
                        "assets\\minecraft\\textures\\blocks\\hopper_outside.png",
                        "assets\\minecraft\\textures\\blocks\\hopper_outside.png",
                        "assets\\minecraft\\textures\\block\\hopper_outside.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hopper_top.png",
                        "assets\\minecraft\\textures\\blocks\\hopper_top.png",
                        "assets\\minecraft\\textures\\blocks\\hopper_top.png",
                        "assets\\minecraft\\textures\\block\\hopper_top.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\horn_coral.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\horn_coral_block.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\horn_coral_fan.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\ice.png",
                        "assets\\minecraft\\textures\\blocks\\ice.png",
                        "assets\\minecraft\\textures\\blocks\\ice.png",
                        "assets\\minecraft\\textures\\block\\ice.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\iron_bars.png",
                        "assets\\minecraft\\textures\\blocks\\iron_bars.png",
                        "assets\\minecraft\\textures\\blocks\\iron_bars.png",
                        "assets\\minecraft\\textures\\block\\iron_bars.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\iron_block.png",
                        "assets\\minecraft\\textures\\blocks\\iron_block.png",
                        "assets\\minecraft\\textures\\blocks\\iron_block.png",
                        "assets\\minecraft\\textures\\block\\iron_block.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\door_iron_lower.png",
                        "assets\\minecraft\\textures\\blocks\\door_iron_lower.png",
                        "assets\\minecraft\\textures\\blocks\\door_iron_lower.png",
                        "assets\\minecraft\\textures\\block\\iron_door_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\door_iron_upper.png",
                        "assets\\minecraft\\textures\\blocks\\door_iron_upper.png",
                        "assets\\minecraft\\textures\\blocks\\door_iron_upper.png",
                        "assets\\minecraft\\textures\\block\\iron_door_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\iron_ore.png",
                        "assets\\minecraft\\textures\\blocks\\iron_ore.png",
                        "assets\\minecraft\\textures\\blocks\\iron_ore.png",
                        "assets\\minecraft\\textures\\block\\iron_ore.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\iron_trapdoor.png",
                        "assets\\minecraft\\textures\\blocks\\iron_trapdoor.png",
                        "assets\\minecraft\\textures\\blocks\\iron_trapdoor.png",
                        "assets\\minecraft\\textures\\block\\iron_trapdoor.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\itemframe_background.png",
                        "assets\\minecraft\\textures\\blocks\\itemframe_background.png",
                        "assets\\minecraft\\textures\\blocks\\itemframe_background.png",
                        "assets\\minecraft\\textures\\block\\item_frame.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\pumpkin_face_on.png",
                        "assets\\minecraft\\textures\\blocks\\pumpkin_face_on.png",
                        "assets\\minecraft\\textures\\blocks\\pumpkin_face_on.png",
                        "assets\\minecraft\\textures\\block\\jack_o_lantern.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\jukebox_side.png",
                        "assets\\minecraft\\textures\\blocks\\jukebox_side.png",
                        "assets\\minecraft\\textures\\blocks\\jukebox_side.png",
                        "assets\\minecraft\\textures\\block\\jukebox_side.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\jukebox_top.png",
                        "assets\\minecraft\\textures\\blocks\\jukebox_top.png",
                        "assets\\minecraft\\textures\\blocks\\jukebox_top.png",
                        "assets\\minecraft\\textures\\block\\jukebox_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\door_jungle_lower.png",
                        "assets\\minecraft\\textures\\blocks\\door_jungle_lower.png",
                        "assets\\minecraft\\textures\\blocks\\door_jungle_lower.png",
                        "assets\\minecraft\\textures\\block\\jungle_door_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\door_jungle_upper.png",
                        "assets\\minecraft\\textures\\blocks\\door_jungle_upper.png",
                        "assets\\minecraft\\textures\\blocks\\door_jungle_upper.png",
                        "assets\\minecraft\\textures\\block\\jungle_door_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\leaves_jungle.png",
                        "assets\\minecraft\\textures\\blocks\\leaves_jungle.png",
                        "assets\\minecraft\\textures\\blocks\\leaves_jungle.png",
                        "assets\\minecraft\\textures\\block\\jungle_leaves.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\log_jungle.png",
                        "assets\\minecraft\\textures\\blocks\\log_jungle.png",
                        "assets\\minecraft\\textures\\blocks\\log_jungle.png",
                        "assets\\minecraft\\textures\\block\\jungle_log.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\log_jungle_top.png",
                        "assets\\minecraft\\textures\\blocks\\log_jungle_top.png",
                        "assets\\minecraft\\textures\\blocks\\log_jungle_top.png",
                        "assets\\minecraft\\textures\\block\\jungle_log_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\planks_jungle.png",
                        "assets\\minecraft\\textures\\blocks\\planks_jungle.png",
                        "assets\\minecraft\\textures\\blocks\\planks_jungle.png",
                        "assets\\minecraft\\textures\\block\\jungle_planks.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\sapling_jungle.png",
                        "assets\\minecraft\\textures\\blocks\\sapling_jungle.png",
                        "assets\\minecraft\\textures\\blocks\\sapling_jungle.png",
                        "assets\\minecraft\\textures\\block\\jungle_sapling.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\jungle_trapdoor.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\kelp.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\kelp.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\kelp_plant.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\kelp_plant.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\ladder.png",
                        "assets\\minecraft\\textures\\blocks\\ladder.png",
                        "assets\\minecraft\\textures\\blocks\\ladder.png",
                        "assets\\minecraft\\textures\\block\\ladder.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\lapis_block.png",
                        "assets\\minecraft\\textures\\blocks\\lapis_block.png",
                        "assets\\minecraft\\textures\\blocks\\lapis_block.png",
                        "assets\\minecraft\\textures\\block\\lapis_block.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\lapis_ore.png",
                        "assets\\minecraft\\textures\\blocks\\lapis_ore.png",
                        "assets\\minecraft\\textures\\blocks\\lapis_ore.png",
                        "assets\\minecraft\\textures\\block\\lapis_ore.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\double_plant_fern_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_fern_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_fern_bottom.png",
                        "assets\\minecraft\\textures\\block\\large_fern_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\double_plant_fern_top.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_fern_top.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_fern_top.png",
                        "assets\\minecraft\\textures\\block\\large_fern_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\lava_flow.png",
                        "assets\\minecraft\\textures\\blocks\\lava_flow.png",
                        "assets\\minecraft\\textures\\blocks\\lava_flow.png",
                        "assets\\minecraft\\textures\\block\\lava_flow.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\lava_flow.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\lava_flow.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\lava_flow.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\lava_flow.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\lava_still.png",
                        "assets\\minecraft\\textures\\blocks\\lava_still.png",
                        "assets\\minecraft\\textures\\blocks\\lava_still.png",
                        "assets\\minecraft\\textures\\block\\lava_still.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\lava_still.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\lava_still.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\lava_still.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\lava_still.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\lever.png",
                        "assets\\minecraft\\textures\\blocks\\lever.png",
                        "assets\\minecraft\\textures\\blocks\\lever.png",
                        "assets\\minecraft\\textures\\block\\lever.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_light_blue.png",
                        "assets\\minecraft\\textures\\block\\light_blue_concrete.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_powder_light_blue.png",
                        "assets\\minecraft\\textures\\block\\light_blue_concrete_powder.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\glazed_terracotta_light_blue.png",
                        "assets\\minecraft\\textures\\block\\light_blue_glazed_terracotta.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\shulker_top_light_blue.png",
                        "assets\\minecraft\\textures\\block\\light_blue_shulker_box.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_light_blue.png",
                        "assets\\minecraft\\textures\\blocks\\glass_light_blue.png",
                        "assets\\minecraft\\textures\\blocks\\glass_light_blue.png",
                        "assets\\minecraft\\textures\\block\\light_blue_stained_glass.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_light_blue.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_light_blue.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_light_blue.png",
                        "assets\\minecraft\\textures\\block\\light_blue_stained_glass_pane_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_light_blue.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_light_blue.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_light_blue.png",
                        "assets\\minecraft\\textures\\block\\light_blue_terracotta.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wool_colored_light_blue.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_light_blue.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_light_blue.png",
                        "assets\\minecraft\\textures\\block\\light_blue_wool.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_silver.png",
                        "assets\\minecraft\\textures\\block\\light_gray_concrete.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_powder_silver.png",
                        "assets\\minecraft\\textures\\block\\light_gray_concrete_powder.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\glazed_terracotta_silver.png",
                        "assets\\minecraft\\textures\\block\\light_gray_glazed_terracotta.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\shulker_top_silver.png",
                        "assets\\minecraft\\textures\\block\\light_gray_shulker_box.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_silver.png",
                        "assets\\minecraft\\textures\\blocks\\glass_silver.png",
                        "assets\\minecraft\\textures\\blocks\\glass_silver.png",
                        "assets\\minecraft\\textures\\block\\light_gray_stained_glass.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_silver.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_silver.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_silver.png",
                        "assets\\minecraft\\textures\\block\\light_gray_stained_glass_pane_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_silver.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_silver.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_silver.png",
                        "assets\\minecraft\\textures\\block\\light_gray_terracotta.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wool_colored_silver.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_silver.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_silver.png",
                        "assets\\minecraft\\textures\\block\\light_gray_wool.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\double_plant_syringa_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_syringa_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_syringa_bottom.png",
                        "assets\\minecraft\\textures\\block\\lilac_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\double_plant_syringa_top.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_syringa_top.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_syringa_top.png",
                        "assets\\minecraft\\textures\\block\\lilac_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\waterlily.png",
                        "assets\\minecraft\\textures\\blocks\\waterlily.png",
                        "assets\\minecraft\\textures\\blocks\\waterlily.png",
                        "assets\\minecraft\\textures\\block\\lily_pad.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_lime.png",
                        "assets\\minecraft\\textures\\block\\lime_concrete.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_powder_lime.png",
                        "assets\\minecraft\\textures\\block\\lime_concrete_powder.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\glazed_terracotta_lime.png",
                        "assets\\minecraft\\textures\\block\\lime_glazed_terracotta.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\shulker_top_lime.png",
                        "assets\\minecraft\\textures\\block\\lime_shulker_box.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_lime.png",
                        "assets\\minecraft\\textures\\blocks\\glass_lime.png",
                        "assets\\minecraft\\textures\\blocks\\glass_lime.png",
                        "assets\\minecraft\\textures\\block\\lime_stained_glass.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_lime.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_lime.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_lime.png",
                        "assets\\minecraft\\textures\\block\\lime_stained_glass_pane_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_lime.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_lime.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_lime.png",
                        "assets\\minecraft\\textures\\block\\lime_terracotta.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wool_colored_lime.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_lime.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_lime.png",
                        "assets\\minecraft\\textures\\block\\lime_wool.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_magenta.png",
                        "assets\\minecraft\\textures\\block\\magenta_concrete.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_powder_magenta.png",
                        "assets\\minecraft\\textures\\block\\magenta_concrete_powder.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\glazed_terracotta_magenta.png",
                        "assets\\minecraft\\textures\\block\\magenta_glazed_terracotta.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\shulker_top_magenta.png",
                        "assets\\minecraft\\textures\\block\\magenta_shulker_box.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_magenta.png",
                        "assets\\minecraft\\textures\\blocks\\glass_magenta.png",
                        "assets\\minecraft\\textures\\blocks\\glass_magenta.png",
                        "assets\\minecraft\\textures\\block\\magenta_stained_glass.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_magenta.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_magenta.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_magenta.png",
                        "assets\\minecraft\\textures\\block\\magenta_stained_glass_pane_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_magenta.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_magenta.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_magenta.png",
                        "assets\\minecraft\\textures\\block\\magenta_terracotta.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wool_colored_magenta.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_magenta.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_magenta.png",
                        "assets\\minecraft\\textures\\block\\magenta_wool.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\magma.png",
                        "assets\\minecraft\\textures\\blocks\\magma.png",
                        "assets\\minecraft\\textures\\block\\magma.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\magma.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\magma.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\magma.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\melon_side.png",
                        "assets\\minecraft\\textures\\blocks\\melon_side.png",
                        "assets\\minecraft\\textures\\blocks\\melon_side.png",
                        "assets\\minecraft\\textures\\block\\melon_side.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\melon_stem_disconnected.png",
                        "assets\\minecraft\\textures\\blocks\\melon_stem_disconnected.png",
                        "assets\\minecraft\\textures\\blocks\\melon_stem_disconnected.png",
                        "assets\\minecraft\\textures\\block\\melon_stem.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\melon_top.png",
                        "assets\\minecraft\\textures\\blocks\\melon_top.png",
                        "assets\\minecraft\\textures\\blocks\\melon_top.png",
                        "assets\\minecraft\\textures\\block\\melon_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\cobblestone_mossy.png",
                        "assets\\minecraft\\textures\\blocks\\cobblestone_mossy.png",
                        "assets\\minecraft\\textures\\blocks\\cobblestone_mossy.png",
                        "assets\\minecraft\\textures\\block\\mossy_cobblestone.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\stonebrick_mossy.png",
                        "assets\\minecraft\\textures\\blocks\\stonebrick_mossy.png",
                        "assets\\minecraft\\textures\\blocks\\stonebrick_mossy.png",
                        "assets\\minecraft\\textures\\block\\mossy_stone_bricks.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\mushroom_block_inside.png",
                        "assets\\minecraft\\textures\\blocks\\mushroom_block_inside.png",
                        "assets\\minecraft\\textures\\blocks\\mushroom_block_inside.png",
                        "assets\\minecraft\\textures\\block\\mushroom_block_inside.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\mushroom_block_skin_stem.png",
                        "assets\\minecraft\\textures\\blocks\\mushroom_block_skin_stem.png",
                        "assets\\minecraft\\textures\\blocks\\mushroom_block_skin_stem.png",
                        "assets\\minecraft\\textures\\block\\mushroom_stem.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\mycelium_side.png",
                        "assets\\minecraft\\textures\\blocks\\mycelium_side.png",
                        "assets\\minecraft\\textures\\blocks\\mycelium_side.png",
                        "assets\\minecraft\\textures\\block\\mycelium_side.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\mycelium_top.png",
                        "assets\\minecraft\\textures\\blocks\\mycelium_top.png",
                        "assets\\minecraft\\textures\\blocks\\mycelium_top.png",
                        "assets\\minecraft\\textures\\block\\mycelium_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\nether_brick.png",
                        "assets\\minecraft\\textures\\blocks\\nether_brick.png",
                        "assets\\minecraft\\textures\\blocks\\nether_brick.png",
                        "assets\\minecraft\\textures\\block\\nether_bricks.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\portal.png",
                        "assets\\minecraft\\textures\\blocks\\portal.png",
                        "assets\\minecraft\\textures\\blocks\\portal.png",
                        "assets\\minecraft\\textures\\block\\nether_portal.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\portal.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\portal.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\portal.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\nether_portal.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\quartz_ore.png",
                        "assets\\minecraft\\textures\\blocks\\quartz_ore.png",
                        "assets\\minecraft\\textures\\blocks\\quartz_ore.png",
                        "assets\\minecraft\\textures\\block\\nether_quartz_ore.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\nether_wart_block.png",
                        "assets\\minecraft\\textures\\blocks\\nether_wart_block.png",
                        "assets\\minecraft\\textures\\block\\nether_wart_block.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\nether_wart_stage_0.png",
                        "assets\\minecraft\\textures\\blocks\\nether_wart_stage_0.png",
                        "assets\\minecraft\\textures\\blocks\\nether_wart_stage_0.png",
                        "assets\\minecraft\\textures\\block\\nether_wart_stage0.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\nether_wart_stage_1.png",
                        "assets\\minecraft\\textures\\blocks\\nether_wart_stage_1.png",
                        "assets\\minecraft\\textures\\blocks\\nether_wart_stage_1.png",
                        "assets\\minecraft\\textures\\block\\nether_wart_stage1.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\nether_wart_stage_2.png",
                        "assets\\minecraft\\textures\\blocks\\nether_wart_stage_2.png",
                        "assets\\minecraft\\textures\\blocks\\nether_wart_stage_2.png",
                        "assets\\minecraft\\textures\\block\\nether_wart_stage2.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\netherrack.png",
                        "assets\\minecraft\\textures\\blocks\\netherrack.png",
                        "assets\\minecraft\\textures\\blocks\\netherrack.png",
                        "assets\\minecraft\\textures\\block\\netherrack.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\noteblock.png",
                        "assets\\minecraft\\textures\\blocks\\noteblock.png",
                        "assets\\minecraft\\textures\\blocks\\noteblock.png",
                        "assets\\minecraft\\textures\\block\\note_block.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\door_wood_lower.png",
                        "assets\\minecraft\\textures\\blocks\\door_wood_lower.png",
                        "assets\\minecraft\\textures\\blocks\\door_wood_lower.png",
                        "assets\\minecraft\\textures\\block\\oak_door_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\door_wood_upper.png",
                        "assets\\minecraft\\textures\\blocks\\door_wood_upper.png",
                        "assets\\minecraft\\textures\\blocks\\door_wood_upper.png",
                        "assets\\minecraft\\textures\\block\\oak_door_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\leaves_oak.png",
                        "assets\\minecraft\\textures\\blocks\\leaves_oak.png",
                        "assets\\minecraft\\textures\\blocks\\leaves_oak.png",
                        "assets\\minecraft\\textures\\block\\oak_leaves.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\log_oak.png",
                        "assets\\minecraft\\textures\\blocks\\log_oak.png",
                        "assets\\minecraft\\textures\\blocks\\log_oak.png",
                        "assets\\minecraft\\textures\\block\\oak_log.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\log_oak_top.png",
                        "assets\\minecraft\\textures\\blocks\\log_oak_top.png",
                        "assets\\minecraft\\textures\\blocks\\log_oak_top.png",
                        "assets\\minecraft\\textures\\block\\oak_log_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\planks_oak.png",
                        "assets\\minecraft\\textures\\blocks\\planks_oak.png",
                        "assets\\minecraft\\textures\\blocks\\planks_oak.png",
                        "assets\\minecraft\\textures\\block\\oak_planks.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\sapling_oak.png",
                        "assets\\minecraft\\textures\\blocks\\sapling_oak.png",
                        "assets\\minecraft\\textures\\blocks\\sapling_oak.png",
                        "assets\\minecraft\\textures\\block\\oak_sapling.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\trapdoor.png",
                        "assets\\minecraft\\textures\\blocks\\trapdoor.png",
                        "assets\\minecraft\\textures\\blocks\\trapdoor.png",
                        "assets\\minecraft\\textures\\block\\oak_trapdoor.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\observer_back.png",
                        "assets\\minecraft\\textures\\block\\observer_back.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\observer_back_lit.png",
                        "assets\\minecraft\\textures\\block\\observer_back_on.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\observer_front.png",
                        "assets\\minecraft\\textures\\block\\observer_front.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\observer_side.png",
                        "assets\\minecraft\\textures\\block\\observer_side.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\observer_top.png",
                        "assets\\minecraft\\textures\\block\\observer_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\obsidian.png",
                        "assets\\minecraft\\textures\\blocks\\obsidian.png",
                        "assets\\minecraft\\textures\\blocks\\obsidian.png",
                        "assets\\minecraft\\textures\\block\\obsidian.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_orange.png",
                        "assets\\minecraft\\textures\\block\\orange_concrete.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_powder_orange.png",
                        "assets\\minecraft\\textures\\block\\orange_concrete_powder.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\glazed_terracotta_orange.png",
                        "assets\\minecraft\\textures\\block\\orange_glazed_terracotta.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\shulker_top_orange.png",
                        "assets\\minecraft\\textures\\block\\orange_shulker_box.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_orange.png",
                        "assets\\minecraft\\textures\\blocks\\glass_orange.png",
                        "assets\\minecraft\\textures\\blocks\\glass_orange.png",
                        "assets\\minecraft\\textures\\block\\orange_stained_glass.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_orange.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_orange.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_orange.png",
                        "assets\\minecraft\\textures\\block\\orange_stained_glass_pane_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_orange.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_orange.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_orange.png",
                        "assets\\minecraft\\textures\\block\\orange_terracotta.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\flower_tulip_orange.png",
                        "assets\\minecraft\\textures\\blocks\\flower_tulip_orange.png",
                        "assets\\minecraft\\textures\\blocks\\flower_tulip_orange.png",
                        "assets\\minecraft\\textures\\block\\orange_tulip.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wool_colored_orange.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_orange.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_orange.png",
                        "assets\\minecraft\\textures\\block\\orange_wool.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\flower_oxeye_daisy.png",
                        "assets\\minecraft\\textures\\blocks\\flower_oxeye_daisy.png",
                        "assets\\minecraft\\textures\\blocks\\flower_oxeye_daisy.png",
                        "assets\\minecraft\\textures\\block\\oxeye_daisy.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\ice_packed.png",
                        "assets\\minecraft\\textures\\blocks\\ice_packed.png",
                        "assets\\minecraft\\textures\\blocks\\ice_packed.png",
                        "assets\\minecraft\\textures\\block\\packed_ice.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\double_plant_paeonia_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_paeonia_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_paeonia_bottom.png",
                        "assets\\minecraft\\textures\\block\\peony_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\double_plant_paeonia_top.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_paeonia_top.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_paeonia_top.png",
                        "assets\\minecraft\\textures\\block\\peony_top.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_pink.png",
                        "assets\\minecraft\\textures\\block\\pink_concrete.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_powder_pink.png",
                        "assets\\minecraft\\textures\\block\\pink_concrete_powder.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\glazed_terracotta_pink.png",
                        "assets\\minecraft\\textures\\block\\pink_glazed_terracotta.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\shulker_top_pink.png",
                        "assets\\minecraft\\textures\\block\\pink_shulker_box.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_pink.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pink.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pink.png",
                        "assets\\minecraft\\textures\\block\\pink_stained_glass.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_pink.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_pink.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_pink.png",
                        "assets\\minecraft\\textures\\block\\pink_stained_glass_pane_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_pink.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_pink.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_pink.png",
                        "assets\\minecraft\\textures\\block\\pink_terracotta.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\flower_tulip_pink.png",
                        "assets\\minecraft\\textures\\blocks\\flower_tulip_pink.png",
                        "assets\\minecraft\\textures\\blocks\\flower_tulip_pink.png",
                        "assets\\minecraft\\textures\\block\\pink_tulip.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wool_colored_pink.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_pink.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_pink.png",
                        "assets\\minecraft\\textures\\block\\pink_wool.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\piston_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\piston_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\piston_bottom.png",
                        "assets\\minecraft\\textures\\block\\piston_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\piston_inner.png",
                        "assets\\minecraft\\textures\\blocks\\piston_inner.png",
                        "assets\\minecraft\\textures\\blocks\\piston_inner.png",
                        "assets\\minecraft\\textures\\block\\piston_inner.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\piston_side.png",
                        "assets\\minecraft\\textures\\blocks\\piston_side.png",
                        "assets\\minecraft\\textures\\blocks\\piston_side.png",
                        "assets\\minecraft\\textures\\block\\piston_side.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\piston_top_normal.png",
                        "assets\\minecraft\\textures\\blocks\\piston_top_normal.png",
                        "assets\\minecraft\\textures\\blocks\\piston_top_normal.png",
                        "assets\\minecraft\\textures\\block\\piston_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\piston_top_sticky.png",
                        "assets\\minecraft\\textures\\blocks\\piston_top_sticky.png",
                        "assets\\minecraft\\textures\\blocks\\piston_top_sticky.png",
                        "assets\\minecraft\\textures\\block\\piston_top_sticky.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\dirt_podzol_side.png",
                        "assets\\minecraft\\textures\\blocks\\dirt_podzol_side.png",
                        "assets\\minecraft\\textures\\blocks\\dirt_podzol_side.png",
                        "assets\\minecraft\\textures\\block\\podzol_side.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\dirt_podzol_top.png",
                        "assets\\minecraft\\textures\\blocks\\dirt_podzol_top.png",
                        "assets\\minecraft\\textures\\blocks\\dirt_podzol_top.png",
                        "assets\\minecraft\\textures\\block\\podzol_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\stone_andesite_smooth.png",
                        "assets\\minecraft\\textures\\blocks\\stone_andesite_smooth.png",
                        "assets\\minecraft\\textures\\blocks\\stone_andesite_smooth.png",
                        "assets\\minecraft\\textures\\block\\polished_andesite.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\stone_diorite_smooth.png",
                        "assets\\minecraft\\textures\\blocks\\stone_diorite_smooth.png",
                        "assets\\minecraft\\textures\\blocks\\stone_diorite_smooth.png",
                        "assets\\minecraft\\textures\\block\\polished_diorite.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\stone_granite_smooth.png",
                        "assets\\minecraft\\textures\\blocks\\stone_granite_smooth.png",
                        "assets\\minecraft\\textures\\blocks\\stone_granite_smooth.png",
                        "assets\\minecraft\\textures\\block\\polished_granite.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\flower_rose.png",
                        "assets\\minecraft\\textures\\blocks\\flower_rose.png",
                        "assets\\minecraft\\textures\\blocks\\flower_rose.png",
                        "assets\\minecraft\\textures\\block\\poppy.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\potatoes_stage_0.png",
                        "assets\\minecraft\\textures\\blocks\\potatoes_stage_0.png",
                        "assets\\minecraft\\textures\\blocks\\potatoes_stage_0.png",
                        "assets\\minecraft\\textures\\block\\potatoes_stage0.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\potatoes_stage_1.png",
                        "assets\\minecraft\\textures\\blocks\\potatoes_stage_1.png",
                        "assets\\minecraft\\textures\\blocks\\potatoes_stage_1.png",
                        "assets\\minecraft\\textures\\block\\potatoes_stage1.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\potatoes_stage_2.png",
                        "assets\\minecraft\\textures\\blocks\\potatoes_stage_2.png",
                        "assets\\minecraft\\textures\\blocks\\potatoes_stage_2.png",
                        "assets\\minecraft\\textures\\block\\potatoes_stage2.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\potatoes_stage_3.png",
                        "assets\\minecraft\\textures\\blocks\\potatoes_stage_3.png",
                        "assets\\minecraft\\textures\\blocks\\potatoes_stage_3.png",
                        "assets\\minecraft\\textures\\block\\potatoes_stage3.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\rail_golden.png",
                        "assets\\minecraft\\textures\\blocks\\rail_golden.png",
                        "assets\\minecraft\\textures\\blocks\\rail_golden.png",
                        "assets\\minecraft\\textures\\block\\powered_rail.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\rail_golden_powered.png",
                        "assets\\minecraft\\textures\\blocks\\rail_golden_powered.png",
                        "assets\\minecraft\\textures\\blocks\\rail_golden_powered.png",
                        "assets\\minecraft\\textures\\block\\powered_rail_on.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\prismarine_rough.png",
                        "assets\\minecraft\\textures\\blocks\\prismarine_rough.png",
                        "assets\\minecraft\\textures\\blocks\\prismarine_rough.png",
                        "assets\\minecraft\\textures\\block\\prismarine.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\prismarine_rough.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\prismarine_rough.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\prismarine_rough.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\prismarine.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\prismarine_bricks.png",
                        "assets\\minecraft\\textures\\blocks\\prismarine_bricks.png",
                        "assets\\minecraft\\textures\\blocks\\prismarine_bricks.png",
                        "assets\\minecraft\\textures\\block\\prismarine_bricks.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\pumpkin_side.png",
                        "assets\\minecraft\\textures\\blocks\\pumpkin_side.png",
                        "assets\\minecraft\\textures\\blocks\\pumpkin_side.png",
                        "assets\\minecraft\\textures\\block\\pumpkin_side.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\pumpkin_stem_disconnected.png",
                        "assets\\minecraft\\textures\\blocks\\pumpkin_stem_disconnected.png",
                        "assets\\minecraft\\textures\\blocks\\pumpkin_stem_disconnected.png",
                        "assets\\minecraft\\textures\\block\\pumpkin_stem.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\pumpkin_top.png",
                        "assets\\minecraft\\textures\\blocks\\pumpkin_top.png",
                        "assets\\minecraft\\textures\\blocks\\pumpkin_top.png",
                        "assets\\minecraft\\textures\\block\\pumpkin_top.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_purple.png",
                        "assets\\minecraft\\textures\\block\\purple_concrete.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_powder_purple.png",
                        "assets\\minecraft\\textures\\block\\purple_concrete_powder.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\glazed_terracotta_purple.png",
                        "assets\\minecraft\\textures\\block\\purple_glazed_terracotta.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\shulker_top_purple.png",
                        "assets\\minecraft\\textures\\block\\purple_shulker_box.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_purple.png",
                        "assets\\minecraft\\textures\\blocks\\glass_purple.png",
                        "assets\\minecraft\\textures\\blocks\\glass_purple.png",
                        "assets\\minecraft\\textures\\block\\purple_stained_glass.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_purple.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_purple.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_purple.png",
                        "assets\\minecraft\\textures\\block\\purple_stained_glass_pane_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_purple.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_purple.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_purple.png",
                        "assets\\minecraft\\textures\\block\\purple_terracotta.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wool_colored_purple.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_purple.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_purple.png",
                        "assets\\minecraft\\textures\\block\\purple_wool.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\purpur_block.png",
                        "assets\\minecraft\\textures\\blocks\\purpur_block.png",
                        "assets\\minecraft\\textures\\block\\purpur_block.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\purpur_pillar.png",
                        "assets\\minecraft\\textures\\blocks\\purpur_pillar.png",
                        "assets\\minecraft\\textures\\block\\purpur_pillar.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\purpur_pillar_top.png",
                        "assets\\minecraft\\textures\\blocks\\purpur_pillar_top.png",
                        "assets\\minecraft\\textures\\block\\purpur_pillar_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\quartz_block_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\quartz_block_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\quartz_block_bottom.png",
                        "assets\\minecraft\\textures\\block\\quartz_block_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\quartz_block_side.png",
                        "assets\\minecraft\\textures\\blocks\\quartz_block_side.png",
                        "assets\\minecraft\\textures\\blocks\\quartz_block_side.png",
                        "assets\\minecraft\\textures\\block\\quartz_block_side.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\quartz_block_top.png",
                        "assets\\minecraft\\textures\\blocks\\quartz_block_top.png",
                        "assets\\minecraft\\textures\\blocks\\quartz_block_top.png",
                        "assets\\minecraft\\textures\\block\\quartz_block_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\quartz_block_lines.png",
                        "assets\\minecraft\\textures\\blocks\\quartz_block_lines.png",
                        "assets\\minecraft\\textures\\blocks\\quartz_block_lines.png",
                        "assets\\minecraft\\textures\\block\\quartz_pillar.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\quartz_block_lines_top.png",
                        "assets\\minecraft\\textures\\blocks\\quartz_block_lines_top.png",
                        "assets\\minecraft\\textures\\blocks\\quartz_block_lines_top.png",
                        "assets\\minecraft\\textures\\block\\quartz_pillar_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\rail_normal.png",
                        "assets\\minecraft\\textures\\blocks\\rail_normal.png",
                        "assets\\minecraft\\textures\\blocks\\rail_normal.png",
                        "assets\\minecraft\\textures\\block\\rail.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\rail_normal_turned.png",
                        "assets\\minecraft\\textures\\blocks\\rail_normal_turned.png",
                        "assets\\minecraft\\textures\\blocks\\rail_normal_turned.png",
                        "assets\\minecraft\\textures\\block\\rail_corner.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_red.png",
                        "assets\\minecraft\\textures\\block\\red_concrete.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_powder_red.png",
                        "assets\\minecraft\\textures\\block\\red_concrete_powder.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\glazed_terracotta_red.png",
                        "assets\\minecraft\\textures\\block\\red_glazed_terracotta.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\mushroom_red.png",
                        "assets\\minecraft\\textures\\blocks\\mushroom_red.png",
                        "assets\\minecraft\\textures\\blocks\\mushroom_red.png",
                        "assets\\minecraft\\textures\\block\\red_mushroom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\mushroom_block_skin_red.png",
                        "assets\\minecraft\\textures\\blocks\\mushroom_block_skin_red.png",
                        "assets\\minecraft\\textures\\blocks\\mushroom_block_skin_red.png",
                        "assets\\minecraft\\textures\\block\\red_mushroom_block.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\red_nether_brick.png",
                        "assets\\minecraft\\textures\\blocks\\red_nether_brick.png",
                        "assets\\minecraft\\textures\\block\\red_nether_bricks.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\red_sand.png",
                        "assets\\minecraft\\textures\\blocks\\red_sand.png",
                        "assets\\minecraft\\textures\\blocks\\red_sand.png",
                        "assets\\minecraft\\textures\\block\\red_sand.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\red_sandstone_normal.png",
                        "assets\\minecraft\\textures\\blocks\\red_sandstone_normal.png",
                        "assets\\minecraft\\textures\\blocks\\red_sandstone_normal.png",
                        "assets\\minecraft\\textures\\block\\red_sandstone.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\red_sandstone_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\red_sandstone_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\red_sandstone_bottom.png",
                        "assets\\minecraft\\textures\\block\\red_sandstone_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\red_sandstone_top.png",
                        "assets\\minecraft\\textures\\blocks\\red_sandstone_top.png",
                        "assets\\minecraft\\textures\\blocks\\red_sandstone_top.png",
                        "assets\\minecraft\\textures\\block\\red_sandstone_top.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\shulker_top_red.png",
                        "assets\\minecraft\\textures\\block\\red_shulker_box.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_red.png",
                        "assets\\minecraft\\textures\\blocks\\glass_red.png",
                        "assets\\minecraft\\textures\\blocks\\glass_red.png",
                        "assets\\minecraft\\textures\\block\\red_stained_glass.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_red.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_red.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_red.png",
                        "assets\\minecraft\\textures\\block\\red_stained_glass_pane_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_red.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_red.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_red.png",
                        "assets\\minecraft\\textures\\block\\red_terracotta.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\flower_tulip_red.png",
                        "assets\\minecraft\\textures\\blocks\\flower_tulip_red.png",
                        "assets\\minecraft\\textures\\blocks\\flower_tulip_red.png",
                        "assets\\minecraft\\textures\\block\\red_tulip.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wool_colored_red.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_red.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_red.png",
                        "assets\\minecraft\\textures\\block\\red_wool.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\redstone_block.png",
                        "assets\\minecraft\\textures\\blocks\\redstone_block.png",
                        "assets\\minecraft\\textures\\blocks\\redstone_block.png",
                        "assets\\minecraft\\textures\\block\\redstone_block.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\redstone_dust_cross.png",
                        "",
                        "",
                        ""
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\redstone_dust_cross_overlay.png",
                        "",
                        "",
                        ""
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\redstone_dust_dot.png",
                        "assets\\minecraft\\textures\\blocks\\redstone_dust_dot.png",
                        "assets\\minecraft\\textures\\block\\redstone_dust_dot.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\redstone_dust_line.png",
                        "",
                        "",
                        ""
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\redstone_dust_line_overlay.png",
                        "",
                        "",
                        ""
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\redstone_dust_line0.png",
                        "assets\\minecraft\\textures\\blocks\\redstone_dust_line0.png",
                        "assets\\minecraft\\textures\\block\\redstone_dust_line0.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\redstone_dust_line1.png",
                        "assets\\minecraft\\textures\\blocks\\redstone_dust_line1.png",
                        "assets\\minecraft\\textures\\block\\redstone_dust_line1.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\redstone_dust_overlay.png",
                        "assets\\minecraft\\textures\\blocks\\redstone_dust_overlay.png",
                        "assets\\minecraft\\textures\\block\\redstone_dust_overlay.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\redstone_lamp_off.png",
                        "assets\\minecraft\\textures\\blocks\\redstone_lamp_off.png",
                        "assets\\minecraft\\textures\\blocks\\redstone_lamp_off.png",
                        "assets\\minecraft\\textures\\block\\redstone_lamp.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\redstone_lamp_on.png",
                        "assets\\minecraft\\textures\\blocks\\redstone_lamp_on.png",
                        "assets\\minecraft\\textures\\blocks\\redstone_lamp_on.png",
                        "assets\\minecraft\\textures\\block\\redstone_lamp_on.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\redstone_ore.png",
                        "assets\\minecraft\\textures\\blocks\\redstone_ore.png",
                        "assets\\minecraft\\textures\\blocks\\redstone_ore.png",
                        "assets\\minecraft\\textures\\block\\redstone_ore.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\redstone_torch_on.png",
                        "assets\\minecraft\\textures\\blocks\\redstone_torch_on.png",
                        "assets\\minecraft\\textures\\blocks\\redstone_torch_on.png",
                        "assets\\minecraft\\textures\\block\\redstone_torch.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\redstone_torch_off.png",
                        "assets\\minecraft\\textures\\blocks\\redstone_torch_off.png",
                        "assets\\minecraft\\textures\\blocks\\redstone_torch_off.png",
                        "assets\\minecraft\\textures\\block\\redstone_torch_off.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\repeater_off.png",
                        "assets\\minecraft\\textures\\blocks\\repeater_off.png",
                        "assets\\minecraft\\textures\\blocks\\repeater_off.png",
                        "assets\\minecraft\\textures\\block\\repeater.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\repeater_on.png",
                        "assets\\minecraft\\textures\\blocks\\repeater_on.png",
                        "assets\\minecraft\\textures\\blocks\\repeater_on.png",
                        "assets\\minecraft\\textures\\block\\repeater_on.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\repeating_command_block_back.png",
                        "assets\\minecraft\\textures\\blocks\\repeating_command_block_back.png",
                        "assets\\minecraft\\textures\\block\\repeating_command_block_back.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\repeating_command_block_back.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\repeating_command_block_back.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\repeating_command_block_back.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\repeating_command_block_conditional.png",
                        "assets\\minecraft\\textures\\blocks\\repeating_command_block_conditional.png",
                        "assets\\minecraft\\textures\\block\\repeating_command_block_conditional.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\repeating_command_block_conditional.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\repeating_command_block_conditional.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\repeating_command_block_conditional.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\repeating_command_block_front.png",
                        "assets\\minecraft\\textures\\blocks\\repeating_command_block_front.png",
                        "assets\\minecraft\\textures\\block\\repeating_command_block_front.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\repeating_command_block_front.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\repeating_command_block_front.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\repeating_command_block_front.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\repeating_command_block_side.png",
                        "assets\\minecraft\\textures\\blocks\\repeating_command_block_side.png",
                        "assets\\minecraft\\textures\\block\\repeating_command_block_side.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\repeating_command_block_side.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\repeating_command_block_side.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\repeating_command_block_side.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\double_plant_rose_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_rose_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_rose_bottom.png",
                        "assets\\minecraft\\textures\\block\\rose_bush_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\double_plant_rose_top.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_rose_top.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_rose_top.png",
                        "assets\\minecraft\\textures\\block\\rose_bush_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\sand.png",
                        "assets\\minecraft\\textures\\blocks\\sand.png",
                        "assets\\minecraft\\textures\\blocks\\sand.png",
                        "assets\\minecraft\\textures\\block\\sand.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\sandstone_normal.png",
                        "assets\\minecraft\\textures\\blocks\\sandstone_normal.png",
                        "assets\\minecraft\\textures\\blocks\\sandstone_normal.png",
                        "assets\\minecraft\\textures\\block\\sandstone.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\sandstone_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\sandstone_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\sandstone_bottom.png",
                        "assets\\minecraft\\textures\\block\\sandstone_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\sandstone_top.png",
                        "assets\\minecraft\\textures\\blocks\\sandstone_top.png",
                        "assets\\minecraft\\textures\\blocks\\sandstone_top.png",
                        "assets\\minecraft\\textures\\block\\sandstone_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\sea_lantern.png",
                        "assets\\minecraft\\textures\\blocks\\sea_lantern.png",
                        "assets\\minecraft\\textures\\blocks\\sea_lantern.png",
                        "assets\\minecraft\\textures\\block\\sea_lantern.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\sea_lantern.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\sea_lantern.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\sea_lantern.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\sea_lantern.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\sea_pickle.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\seagrass.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\seagrass.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\shulker_box.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\slime.png",
                        "assets\\minecraft\\textures\\blocks\\slime.png",
                        "assets\\minecraft\\textures\\blocks\\slime.png",
                        "assets\\minecraft\\textures\\block\\slime_block.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\snow.png",
                        "assets\\minecraft\\textures\\blocks\\snow.png",
                        "assets\\minecraft\\textures\\blocks\\snow.png",
                        "assets\\minecraft\\textures\\block\\snow.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\soul_sand.png",
                        "assets\\minecraft\\textures\\blocks\\soul_sand.png",
                        "assets\\minecraft\\textures\\blocks\\soul_sand.png",
                        "assets\\minecraft\\textures\\block\\soul_sand.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\mob_spawner.png",
                        "assets\\minecraft\\textures\\blocks\\mob_spawner.png",
                        "assets\\minecraft\\textures\\blocks\\mob_spawner.png",
                        "assets\\minecraft\\textures\\block\\spawner.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\sponge.png",
                        "assets\\minecraft\\textures\\blocks\\sponge.png",
                        "assets\\minecraft\\textures\\blocks\\sponge.png",
                        "assets\\minecraft\\textures\\block\\sponge.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\door_spruce_lower.png",
                        "assets\\minecraft\\textures\\blocks\\door_spruce_lower.png",
                        "assets\\minecraft\\textures\\blocks\\door_spruce_lower.png",
                        "assets\\minecraft\\textures\\block\\spruce_door_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\door_spruce_upper.png",
                        "assets\\minecraft\\textures\\blocks\\door_spruce_upper.png",
                        "assets\\minecraft\\textures\\blocks\\door_spruce_upper.png",
                        "assets\\minecraft\\textures\\block\\spruce_door_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\leaves_spruce.png",
                        "assets\\minecraft\\textures\\blocks\\leaves_spruce.png",
                        "assets\\minecraft\\textures\\blocks\\leaves_spruce.png",
                        "assets\\minecraft\\textures\\block\\spruce_leaves.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\log_spruce.png",
                        "assets\\minecraft\\textures\\blocks\\log_spruce.png",
                        "assets\\minecraft\\textures\\blocks\\log_spruce.png",
                        "assets\\minecraft\\textures\\block\\spruce_log.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\log_spruce_top.png",
                        "assets\\minecraft\\textures\\blocks\\log_spruce_top.png",
                        "assets\\minecraft\\textures\\blocks\\log_spruce_top.png",
                        "assets\\minecraft\\textures\\block\\spruce_log_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\planks_spruce.png",
                        "assets\\minecraft\\textures\\blocks\\planks_spruce.png",
                        "assets\\minecraft\\textures\\blocks\\planks_spruce.png",
                        "assets\\minecraft\\textures\\block\\spruce_planks.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\sapling_spruce.png",
                        "assets\\minecraft\\textures\\blocks\\sapling_spruce.png",
                        "assets\\minecraft\\textures\\blocks\\sapling_spruce.png",
                        "assets\\minecraft\\textures\\block\\spruce_sapling.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\spruce_trapdoor.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\stone.png",
                        "assets\\minecraft\\textures\\blocks\\stone.png",
                        "assets\\minecraft\\textures\\blocks\\stone.png",
                        "assets\\minecraft\\textures\\block\\stone.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\stonebrick.png",
                        "assets\\minecraft\\textures\\blocks\\stonebrick.png",
                        "assets\\minecraft\\textures\\blocks\\stonebrick.png",
                        "assets\\minecraft\\textures\\block\\stone_bricks.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\stone_slab_side.png",
                        "assets\\minecraft\\textures\\blocks\\stone_slab_side.png",
                        "assets\\minecraft\\textures\\blocks\\stone_slab_side.png",
                        "assets\\minecraft\\textures\\block\\stone_slab_side.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\stone_slab_top.png",
                        "assets\\minecraft\\textures\\blocks\\stone_slab_top.png",
                        "assets\\minecraft\\textures\\blocks\\stone_slab_top.png",
                        "assets\\minecraft\\textures\\block\\stone_slab_top.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\stripped_acacia_log.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\stripped_acacia_log_top.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\stripped_birch_log.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\stripped_birch_log_top.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\stripped_dark_oak_log.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\stripped_dark_oak_log_top.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\stripped_jungle_log.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\stripped_jungle_log_top.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\stripped_oak_log.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\stripped_oak_log_top.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\stripped_spruce_log.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\stripped_spruce_log_top.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\structure_block.png",
                        "assets\\minecraft\\textures\\blocks\\structure_block.png",
                        "assets\\minecraft\\textures\\block\\structure_block.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\structure_block_corner.png",
                        "assets\\minecraft\\textures\\blocks\\structure_block_corner.png",
                        "assets\\minecraft\\textures\\block\\structure_block_corner.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\structure_block_data.png",
                        "assets\\minecraft\\textures\\blocks\\structure_block_data.png",
                        "assets\\minecraft\\textures\\block\\structure_block_data.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\structure_block_load.png",
                        "assets\\minecraft\\textures\\blocks\\structure_block_load.png",
                        "assets\\minecraft\\textures\\block\\structure_block_load.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\structure_block_save.png",
                        "assets\\minecraft\\textures\\blocks\\structure_block_save.png",
                        "assets\\minecraft\\textures\\block\\structure_block_save.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\reeds.png",
                        "assets\\minecraft\\textures\\blocks\\reeds.png",
                        "assets\\minecraft\\textures\\blocks\\reeds.png",
                        "assets\\minecraft\\textures\\block\\sugar_cane.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\double_plant_sunflower_back.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_sunflower_back.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_sunflower_back.png",
                        "assets\\minecraft\\textures\\block\\sunflower_back.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\double_plant_sunflower_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_sunflower_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_sunflower_bottom.png",
                        "assets\\minecraft\\textures\\block\\sunflower_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\double_plant_sunflower_front.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_sunflower_front.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_sunflower_front.png",
                        "assets\\minecraft\\textures\\block\\sunflower_front.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\double_plant_sunflower_top.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_sunflower_top.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_sunflower_top.png",
                        "assets\\minecraft\\textures\\block\\sunflower_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\double_plant_grass_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_grass_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_grass_bottom.png",
                        "assets\\minecraft\\textures\\block\\tall_grass_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\double_plant_grass_top.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_grass_top.png",
                        "assets\\minecraft\\textures\\blocks\\double_plant_grass_top.png",
                        "assets\\minecraft\\textures\\block\\tall_grass_top.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\tall_seagrass_bottom.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\tall_seagrass_bottom.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\tall_seagrass_top.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\tall_seagrass_top.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hardened_clay.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay.png",
                        "assets\\minecraft\\textures\\block\\terracotta.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\tnt_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\tnt_bottom.png",
                        "assets\\minecraft\\textures\\blocks\\tnt_bottom.png",
                        "assets\\minecraft\\textures\\block\\tnt_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\tnt_side.png",
                        "assets\\minecraft\\textures\\blocks\\tnt_side.png",
                        "assets\\minecraft\\textures\\blocks\\tnt_side.png",
                        "assets\\minecraft\\textures\\block\\tnt_side.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\tnt_top.png",
                        "assets\\minecraft\\textures\\blocks\\tnt_top.png",
                        "assets\\minecraft\\textures\\blocks\\tnt_top.png",
                        "assets\\minecraft\\textures\\block\\tnt_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\torch_on.png",
                        "assets\\minecraft\\textures\\blocks\\torch_on.png",
                        "assets\\minecraft\\textures\\blocks\\torch_on.png",
                        "assets\\minecraft\\textures\\block\\torch.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\trip_wire.png",
                        "assets\\minecraft\\textures\\blocks\\trip_wire.png",
                        "assets\\minecraft\\textures\\blocks\\trip_wire.png",
                        "assets\\minecraft\\textures\\block\\tripwire.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\trip_wire_source.png",
                        "assets\\minecraft\\textures\\blocks\\trip_wire_source.png",
                        "assets\\minecraft\\textures\\blocks\\trip_wire_source.png",
                        "assets\\minecraft\\textures\\block\\tripwire_hook.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\tube_coral.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\tube_coral_block.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\tube_coral_fan.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\turtle_egg.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\turtle_egg_slightly_cracked.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\block\\turtle_egg_very_cracked.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\vine.png",
                        "assets\\minecraft\\textures\\blocks\\vine.png",
                        "assets\\minecraft\\textures\\blocks\\vine.png",
                        "assets\\minecraft\\textures\\block\\vine.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\water_flow.png",
                        "assets\\minecraft\\textures\\blocks\\water_flow.png",
                        "assets\\minecraft\\textures\\blocks\\water_flow.png",
                        "assets\\minecraft\\textures\\block\\water_flow.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\water_flow.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\water_flow.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\water_flow.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\water_flow.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\blocks\\water_overlay.png",
                        "assets\\minecraft\\textures\\blocks\\water_overlay.png",
                        "assets\\minecraft\\textures\\block\\water_overlay.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\water_still.png",
                        "assets\\minecraft\\textures\\blocks\\water_still.png",
                        "assets\\minecraft\\textures\\blocks\\water_still.png",
                        "assets\\minecraft\\textures\\block\\water_still.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\water_still.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\water_still.png.mcmeta",
                        "assets\\minecraft\\textures\\blocks\\water_still.png.mcmeta",
                        "assets\\minecraft\\textures\\block\\water_still.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\sponge_wet.png",
                        "assets\\minecraft\\textures\\blocks\\sponge_wet.png",
                        "assets\\minecraft\\textures\\blocks\\sponge_wet.png",
                        "assets\\minecraft\\textures\\block\\wet_sponge.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_0.png",
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_0.png",
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_0.png",
                        "assets\\minecraft\\textures\\block\\wheat_stage0.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_1.png",
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_1.png",
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_1.png",
                        "assets\\minecraft\\textures\\block\\wheat_stage1.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_2.png",
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_2.png",
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_2.png",
                        "assets\\minecraft\\textures\\block\\wheat_stage2.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_3.png",
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_3.png",
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_3.png",
                        "assets\\minecraft\\textures\\block\\wheat_stage3.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_4.png",
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_4.png",
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_4.png",
                        "assets\\minecraft\\textures\\block\\wheat_stage4.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_5.png",
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_5.png",
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_5.png",
                        "assets\\minecraft\\textures\\block\\wheat_stage5.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_6.png",
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_6.png",
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_6.png",
                        "assets\\minecraft\\textures\\block\\wheat_stage6.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_7.png",
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_7.png",
                        "assets\\minecraft\\textures\\blocks\\wheat_stage_7.png",
                        "assets\\minecraft\\textures\\block\\wheat_stage7.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_white.png",
                        "assets\\minecraft\\textures\\block\\white_concrete.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_powder_white.png",
                        "assets\\minecraft\\textures\\block\\white_concrete_powder.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\glazed_terracotta_white.png",
                        "assets\\minecraft\\textures\\block\\white_glazed_terracotta.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\shulker_top_white.png",
                        "assets\\minecraft\\textures\\block\\white_shulker_box.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_white.png",
                        "assets\\minecraft\\textures\\blocks\\glass_white.png",
                        "assets\\minecraft\\textures\\blocks\\glass_white.png",
                        "assets\\minecraft\\textures\\block\\white_stained_glass.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_white.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_white.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_white.png",
                        "assets\\minecraft\\textures\\block\\white_stained_glass_pane_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_white.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_white.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_white.png",
                        "assets\\minecraft\\textures\\block\\white_terracotta.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\flower_tulip_white.png",
                        "assets\\minecraft\\textures\\blocks\\flower_tulip_white.png",
                        "assets\\minecraft\\textures\\blocks\\flower_tulip_white.png",
                        "assets\\minecraft\\textures\\block\\white_tulip.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wool_colored_white.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_white.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_white.png",
                        "assets\\minecraft\\textures\\block\\white_wool.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_yellow.png",
                        "assets\\minecraft\\textures\\block\\yellow_concrete.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\concrete_powder_yellow.png",
                        "assets\\minecraft\\textures\\block\\yellow_concrete_powder.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\glazed_terracotta_yellow.png",
                        "assets\\minecraft\\textures\\block\\yellow_glazed_terracotta.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\blocks\\shulker_top_yellow.png",
                        "assets\\minecraft\\textures\\block\\yellow_shulker_box.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_yellow.png",
                        "assets\\minecraft\\textures\\blocks\\glass_yellow.png",
                        "assets\\minecraft\\textures\\blocks\\glass_yellow.png",
                        "assets\\minecraft\\textures\\block\\yellow_stained_glass.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_yellow.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_yellow.png",
                        "assets\\minecraft\\textures\\blocks\\glass_pane_top_yellow.png",
                        "assets\\minecraft\\textures\\block\\yellow_stained_glass_pane_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_yellow.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_yellow.png",
                        "assets\\minecraft\\textures\\blocks\\hardened_clay_stained_yellow.png",
                        "assets\\minecraft\\textures\\block\\yellow_terracotta.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\blocks\\wool_colored_yellow.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_yellow.png",
                        "assets\\minecraft\\textures\\blocks\\wool_colored_yellow.png",
                        "assets\\minecraft\\textures\\block\\yellow_wool.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\colormap",
                "assets\\minecraft\\textures\\colormap",
                "assets\\minecraft\\textures\\colormap",
                "assets\\minecraft\\textures\\colormap",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\colormap\\foliage.png",
                        "assets\\minecraft\\textures\\colormap\\foliage.png",
                        "assets\\minecraft\\textures\\colormap\\foliage.png",
                        "assets\\minecraft\\textures\\colormap\\foliage.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\colormap\\grass.png",
                        "assets\\minecraft\\textures\\colormap\\grass.png",
                        "assets\\minecraft\\textures\\colormap\\grass.png",
                        "assets\\minecraft\\textures\\colormap\\grass.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\effect",
                "assets\\minecraft\\textures\\effect",
                "assets\\minecraft\\textures\\effect",
                "assets\\minecraft\\textures\\effect",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\effect\\dither.png",
                        "assets\\minecraft\\textures\\effect\\dither.png",
                        "assets\\minecraft\\textures\\effect\\dither.png",
                        "assets\\minecraft\\textures\\effect\\dither.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity",
                "assets\\minecraft\\textures\\entity",
                "assets\\minecraft\\textures\\entity",
                "assets\\minecraft\\textures\\entity",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\alex.png",
                        "assets\\minecraft\\textures\\entity\\alex.png",
                        "assets\\minecraft\\textures\\entity\\alex.png",
                        "assets\\minecraft\\textures\\entity\\alex.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\arrow.png",
                        "assets\\minecraft\\textures\\entity\\arrow.png",
                        "assets\\minecraft\\textures\\entity\\arrow.png",
                        "assets\\minecraft\\textures\\entity\\arrow.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner_base.png",
                        "assets\\minecraft\\textures\\entity\\banner_base.png",
                        "assets\\minecraft\\textures\\entity\\banner_base.png",
                        "assets\\minecraft\\textures\\entity\\banner_base.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\bat.png",
                        "assets\\minecraft\\textures\\entity\\bat.png",
                        "assets\\minecraft\\textures\\entity\\bat.png",
                        "assets\\minecraft\\textures\\entity\\bat.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\beacon_beam.png",
                        "assets\\minecraft\\textures\\entity\\beacon_beam.png",
                        "assets\\minecraft\\textures\\entity\\beacon_beam.png",
                        "assets\\minecraft\\textures\\entity\\beacon_beam.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\blaze.png",
                        "assets\\minecraft\\textures\\entity\\blaze.png",
                        "assets\\minecraft\\textures\\entity\\blaze.png",
                        "assets\\minecraft\\textures\\entity\\blaze.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\chicken.png",
                        "assets\\minecraft\\textures\\entity\\chicken.png",
                        "assets\\minecraft\\textures\\entity\\chicken.png",
                        "assets\\minecraft\\textures\\entity\\chicken.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\dolphin.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\elytra.png",
                        "assets\\minecraft\\textures\\entity\\elytra.png",
                        "assets\\minecraft\\textures\\entity\\elytra.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\enchanting_table_book.png",
                        "assets\\minecraft\\textures\\entity\\enchanting_table_book.png",
                        "assets\\minecraft\\textures\\entity\\enchanting_table_book.png",
                        "assets\\minecraft\\textures\\entity\\enchanting_table_book.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\end_gateway_beam.png",
                        "assets\\minecraft\\textures\\entity\\end_gateway_beam.png",
                        "assets\\minecraft\\textures\\entity\\end_gateway_beam.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\end_portal.png",
                        "assets\\minecraft\\textures\\entity\\end_portal.png",
                        "assets\\minecraft\\textures\\entity\\end_portal.png",
                        "assets\\minecraft\\textures\\entity\\end_portal.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\endermite.png",
                        "assets\\minecraft\\textures\\entity\\endermite.png",
                        "assets\\minecraft\\textures\\entity\\endermite.png",
                        "assets\\minecraft\\textures\\entity\\endermite.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\experience_orb.png",
                        "assets\\minecraft\\textures\\entity\\experience_orb.png",
                        "assets\\minecraft\\textures\\entity\\experience_orb.png",
                        "assets\\minecraft\\textures\\entity\\experience_orb.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\explosion.png",
                        "assets\\minecraft\\textures\\entity\\explosion.png",
                        "assets\\minecraft\\textures\\entity\\explosion.png",
                        "assets\\minecraft\\textures\\entity\\explosion.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\guardian.png",
                        "assets\\minecraft\\textures\\entity\\guardian.png",
                        "assets\\minecraft\\textures\\entity\\guardian.png",
                        "assets\\minecraft\\textures\\entity\\guardian.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\guardian_beam.png",
                        "assets\\minecraft\\textures\\entity\\guardian_beam.png",
                        "assets\\minecraft\\textures\\entity\\guardian_beam.png",
                        "assets\\minecraft\\textures\\entity\\guardian_beam.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\guardian_elder.png",
                        "assets\\minecraft\\textures\\entity\\guardian_elder.png",
                        "assets\\minecraft\\textures\\entity\\guardian_elder.png",
                        "assets\\minecraft\\textures\\entity\\guardian_elder.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\iron_golem.png",
                        "assets\\minecraft\\textures\\entity\\iron_golem.png",
                        "assets\\minecraft\\textures\\entity\\iron_golem.png",
                        "assets\\minecraft\\textures\\entity\\iron_golem.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\lead_knot.png",
                        "assets\\minecraft\\textures\\entity\\lead_knot.png",
                        "assets\\minecraft\\textures\\entity\\lead_knot.png",
                        "assets\\minecraft\\textures\\entity\\lead_knot.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\minecart.png",
                        "assets\\minecraft\\textures\\entity\\minecart.png",
                        "assets\\minecraft\\textures\\entity\\minecart.png",
                        "assets\\minecraft\\textures\\entity\\minecart.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\phantom.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\phantom_eyes.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield_base.png",
                        "assets\\minecraft\\textures\\entity\\shield_base.png",
                        "assets\\minecraft\\textures\\entity\\shield_base.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield_base_nopattern.png",
                        "assets\\minecraft\\textures\\entity\\shield_base_nopattern.png",
                        "assets\\minecraft\\textures\\entity\\shield_base_nopattern.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\sign.png",
                        "assets\\minecraft\\textures\\entity\\sign.png",
                        "assets\\minecraft\\textures\\entity\\sign.png",
                        "assets\\minecraft\\textures\\entity\\sign.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\silverfish.png",
                        "assets\\minecraft\\textures\\entity\\silverfish.png",
                        "assets\\minecraft\\textures\\entity\\silverfish.png",
                        "assets\\minecraft\\textures\\entity\\silverfish.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\snowman.png",
                        "assets\\minecraft\\textures\\entity\\snowman.png",
                        "assets\\minecraft\\textures\\entity\\snowman.png",
                        "assets\\minecraft\\textures\\entity\\snow_golem.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\spider_eyes.png",
                        "assets\\minecraft\\textures\\entity\\spider_eyes.png",
                        "assets\\minecraft\\textures\\entity\\spider_eyes.png",
                        "assets\\minecraft\\textures\\entity\\spider_eyes.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\squid.png",
                        "assets\\minecraft\\textures\\entity\\squid.png",
                        "assets\\minecraft\\textures\\entity\\squid.png",
                        "assets\\minecraft\\textures\\entity\\squid.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\steve.png",
                        "assets\\minecraft\\textures\\entity\\steve.png",
                        "assets\\minecraft\\textures\\entity\\steve.png",
                        "assets\\minecraft\\textures\\entity\\steve.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\sweep.png",
                        "assets\\minecraft\\textures\\entity\\sweep.png",
                        "assets\\minecraft\\textures\\entity\\sweep.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\trident.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\trident_riptide.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\witch.png",
                        "assets\\minecraft\\textures\\entity\\witch.png",
                        "assets\\minecraft\\textures\\entity\\witch.png",
                        "assets\\minecraft\\textures\\entity\\witch.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\zombie_pigman.png",
                        "assets\\minecraft\\textures\\entity\\zombie_pigman.png",
                        "assets\\minecraft\\textures\\entity\\zombie_pigman.png",
                        "assets\\minecraft\\textures\\entity\\zombie_pigman.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\armorstand",
                "assets\\minecraft\\textures\\entity\\armorstand",
                "assets\\minecraft\\textures\\entity\\armorstand",
                "assets\\minecraft\\textures\\entity\\armorstand",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\armorstand\\wood.png",
                        "assets\\minecraft\\textures\\entity\\armorstand\\wood.png",
                        "assets\\minecraft\\textures\\entity\\armorstand\\wood.png",
                        "assets\\minecraft\\textures\\entity\\armorstand\\wood.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\banner",
                "assets\\minecraft\\textures\\entity\\banner",
                "assets\\minecraft\\textures\\entity\\banner",
                "assets\\minecraft\\textures\\entity\\banner",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\base.png",
                        "assets\\minecraft\\textures\\entity\\banner\\base.png",
                        "assets\\minecraft\\textures\\entity\\banner\\base.png",
                        "assets\\minecraft\\textures\\entity\\banner\\base.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\border.png",
                        "assets\\minecraft\\textures\\entity\\banner\\border.png",
                        "assets\\minecraft\\textures\\entity\\banner\\border.png",
                        "assets\\minecraft\\textures\\entity\\banner\\border.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\bricks.png",
                        "assets\\minecraft\\textures\\entity\\banner\\bricks.png",
                        "assets\\minecraft\\textures\\entity\\banner\\bricks.png",
                        "assets\\minecraft\\textures\\entity\\banner\\bricks.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\circle.png",
                        "assets\\minecraft\\textures\\entity\\banner\\circle.png",
                        "assets\\minecraft\\textures\\entity\\banner\\circle.png",
                        "assets\\minecraft\\textures\\entity\\banner\\circle.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\creeper.png",
                        "assets\\minecraft\\textures\\entity\\banner\\creeper.png",
                        "assets\\minecraft\\textures\\entity\\banner\\creeper.png",
                        "assets\\minecraft\\textures\\entity\\banner\\creeper.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\cross.png",
                        "assets\\minecraft\\textures\\entity\\banner\\cross.png",
                        "assets\\minecraft\\textures\\entity\\banner\\cross.png",
                        "assets\\minecraft\\textures\\entity\\banner\\cross.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\curly_border.png",
                        "assets\\minecraft\\textures\\entity\\banner\\curly_border.png",
                        "assets\\minecraft\\textures\\entity\\banner\\curly_border.png",
                        "assets\\minecraft\\textures\\entity\\banner\\curly_border.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\diagonal_left.png",
                        "assets\\minecraft\\textures\\entity\\banner\\diagonal_left.png",
                        "assets\\minecraft\\textures\\entity\\banner\\diagonal_left.png",
                        "assets\\minecraft\\textures\\entity\\banner\\diagonal_left.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\diagonal_right.png",
                        "assets\\minecraft\\textures\\entity\\banner\\diagonal_right.png",
                        "assets\\minecraft\\textures\\entity\\banner\\diagonal_right.png",
                        "assets\\minecraft\\textures\\entity\\banner\\diagonal_right.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\diagonal_up_left.png",
                        "assets\\minecraft\\textures\\entity\\banner\\diagonal_up_left.png",
                        "assets\\minecraft\\textures\\entity\\banner\\diagonal_up_left.png",
                        "assets\\minecraft\\textures\\entity\\banner\\diagonal_up_left.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\diagonal_up_right.png",
                        "assets\\minecraft\\textures\\entity\\banner\\diagonal_up_right.png",
                        "assets\\minecraft\\textures\\entity\\banner\\diagonal_up_right.png",
                        "assets\\minecraft\\textures\\entity\\banner\\diagonal_up_right.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\flower.png",
                        "assets\\minecraft\\textures\\entity\\banner\\flower.png",
                        "assets\\minecraft\\textures\\entity\\banner\\flower.png",
                        "assets\\minecraft\\textures\\entity\\banner\\flower.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\gradient.png",
                        "assets\\minecraft\\textures\\entity\\banner\\gradient.png",
                        "assets\\minecraft\\textures\\entity\\banner\\gradient.png",
                        "assets\\minecraft\\textures\\entity\\banner\\gradient.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\gradient_up.png",
                        "assets\\minecraft\\textures\\entity\\banner\\gradient_up.png",
                        "assets\\minecraft\\textures\\entity\\banner\\gradient_up.png",
                        "assets\\minecraft\\textures\\entity\\banner\\gradient_up.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\half_horizontal.png",
                        "assets\\minecraft\\textures\\entity\\banner\\half_horizontal.png",
                        "assets\\minecraft\\textures\\entity\\banner\\half_horizontal.png",
                        "assets\\minecraft\\textures\\entity\\banner\\half_horizontal.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\half_horizontal_bottom.png",
                        "assets\\minecraft\\textures\\entity\\banner\\half_horizontal_bottom.png",
                        "assets\\minecraft\\textures\\entity\\banner\\half_horizontal_bottom.png",
                        "assets\\minecraft\\textures\\entity\\banner\\half_horizontal_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\half_vertical.png",
                        "assets\\minecraft\\textures\\entity\\banner\\half_vertical.png",
                        "assets\\minecraft\\textures\\entity\\banner\\half_vertical.png",
                        "assets\\minecraft\\textures\\entity\\banner\\half_vertical.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\half_vertical_right.png",
                        "assets\\minecraft\\textures\\entity\\banner\\half_vertical_right.png",
                        "assets\\minecraft\\textures\\entity\\banner\\half_vertical_right.png",
                        "assets\\minecraft\\textures\\entity\\banner\\half_vertical_right.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\mojang.png",
                        "assets\\minecraft\\textures\\entity\\banner\\mojang.png",
                        "assets\\minecraft\\textures\\entity\\banner\\mojang.png",
                        "assets\\minecraft\\textures\\entity\\banner\\mojang.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\rhombus.png",
                        "assets\\minecraft\\textures\\entity\\banner\\rhombus.png",
                        "assets\\minecraft\\textures\\entity\\banner\\rhombus.png",
                        "assets\\minecraft\\textures\\entity\\banner\\rhombus.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\skull.png",
                        "assets\\minecraft\\textures\\entity\\banner\\skull.png",
                        "assets\\minecraft\\textures\\entity\\banner\\skull.png",
                        "assets\\minecraft\\textures\\entity\\banner\\skull.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\small_stripes.png",
                        "assets\\minecraft\\textures\\entity\\banner\\small_stripes.png",
                        "assets\\minecraft\\textures\\entity\\banner\\small_stripes.png",
                        "assets\\minecraft\\textures\\entity\\banner\\small_stripes.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\square_bottom_left.png",
                        "assets\\minecraft\\textures\\entity\\banner\\square_bottom_left.png",
                        "assets\\minecraft\\textures\\entity\\banner\\square_bottom_left.png",
                        "assets\\minecraft\\textures\\entity\\banner\\square_bottom_left.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\square_bottom_right.png",
                        "assets\\minecraft\\textures\\entity\\banner\\square_bottom_right.png",
                        "assets\\minecraft\\textures\\entity\\banner\\square_bottom_right.png",
                        "assets\\minecraft\\textures\\entity\\banner\\square_bottom_right.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\square_top_left.png",
                        "assets\\minecraft\\textures\\entity\\banner\\square_top_left.png",
                        "assets\\minecraft\\textures\\entity\\banner\\square_top_left.png",
                        "assets\\minecraft\\textures\\entity\\banner\\square_top_left.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\square_top_right.png",
                        "assets\\minecraft\\textures\\entity\\banner\\square_top_right.png",
                        "assets\\minecraft\\textures\\entity\\banner\\square_top_right.png",
                        "assets\\minecraft\\textures\\entity\\banner\\square_top_right.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\straight_cross.png",
                        "assets\\minecraft\\textures\\entity\\banner\\straight_cross.png",
                        "assets\\minecraft\\textures\\entity\\banner\\straight_cross.png",
                        "assets\\minecraft\\textures\\entity\\banner\\straight_cross.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_bottom.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_bottom.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_bottom.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_center.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_center.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_center.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_center.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_downleft.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_downleft.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_downleft.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_downleft.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_downright.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_downright.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_downright.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_downright.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_left.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_left.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_left.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_left.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_middle.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_middle.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_middle.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_middle.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_right.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_right.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_right.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_right.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_top.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_top.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_top.png",
                        "assets\\minecraft\\textures\\entity\\banner\\stripe_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\triangle_bottom.png",
                        "assets\\minecraft\\textures\\entity\\banner\\triangle_bottom.png",
                        "assets\\minecraft\\textures\\entity\\banner\\triangle_bottom.png",
                        "assets\\minecraft\\textures\\entity\\banner\\triangle_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\triangle_top.png",
                        "assets\\minecraft\\textures\\entity\\banner\\triangle_top.png",
                        "assets\\minecraft\\textures\\entity\\banner\\triangle_top.png",
                        "assets\\minecraft\\textures\\entity\\banner\\triangle_top.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\triangles_bottom.png",
                        "assets\\minecraft\\textures\\entity\\banner\\triangles_bottom.png",
                        "assets\\minecraft\\textures\\entity\\banner\\triangles_bottom.png",
                        "assets\\minecraft\\textures\\entity\\banner\\triangles_bottom.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\banner\\triangles_top.png",
                        "assets\\minecraft\\textures\\entity\\banner\\triangles_top.png",
                        "assets\\minecraft\\textures\\entity\\banner\\triangles_top.png",
                        "assets\\minecraft\\textures\\entity\\banner\\triangles_top.png"
                    ),
                }
            ),
            new Carpetas
            (
                "",
                "assets\\minecraft\\textures\\entity\\bear",
                "assets\\minecraft\\textures\\entity\\bear",
                "assets\\minecraft\\textures\\entity\\bear",
                new Archivos[]
                {
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\bear\\polarbear.png",
                        "assets\\minecraft\\textures\\entity\\bear\\polarbear.png",
                        "assets\\minecraft\\textures\\entity\\bear\\polarbear.png"
                    ),
                }
            ),
            new Carpetas
            (
                "",
                "",
                "assets\\minecraft\\textures\\entity\\bed",
                "assets\\minecraft\\textures\\entity\\bed",
                new Archivos[]
                {
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\bed\\black.png",
                        "assets\\minecraft\\textures\\entity\\bed\\black.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\bed\\blue.png",
                        "assets\\minecraft\\textures\\entity\\bed\\blue.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\bed\\brown.png",
                        "assets\\minecraft\\textures\\entity\\bed\\brown.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\bed\\cyan.png",
                        "assets\\minecraft\\textures\\entity\\bed\\cyan.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\bed\\gray.png",
                        "assets\\minecraft\\textures\\entity\\bed\\gray.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\bed\\green.png",
                        "assets\\minecraft\\textures\\entity\\bed\\green.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\bed\\light_blue.png",
                        "assets\\minecraft\\textures\\entity\\bed\\light_blue.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\bed\\silver.png",
                        "assets\\minecraft\\textures\\entity\\bed\\light_gray.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\bed\\lime.png",
                        "assets\\minecraft\\textures\\entity\\bed\\lime.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\bed\\magenta.png",
                        "assets\\minecraft\\textures\\entity\\bed\\magenta.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\bed\\orange.png",
                        "assets\\minecraft\\textures\\entity\\bed\\orange.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\bed\\pink.png",
                        "assets\\minecraft\\textures\\entity\\bed\\pink.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\bed\\purple.png",
                        "assets\\minecraft\\textures\\entity\\bed\\purple.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\bed\\red.png",
                        "assets\\minecraft\\textures\\entity\\bed\\red.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\bed\\white.png",
                        "assets\\minecraft\\textures\\entity\\bed\\white.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\bed\\yellow.png",
                        "assets\\minecraft\\textures\\entity\\bed\\yellow.png"
                    ),
                }
            ),
            new Carpetas
            (
                "",
                "assets\\minecraft\\textures\\entity\\boat",
                "assets\\minecraft\\textures\\entity\\boat",
                "assets\\minecraft\\textures\\entity\\boat",
                new Archivos[]
                {
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\boat\\boat_acacia.png",
                        "assets\\minecraft\\textures\\entity\\boat\\boat_acacia.png",
                        "assets\\minecraft\\textures\\entity\\boat\\acacia.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\boat\\boat_birch.png",
                        "assets\\minecraft\\textures\\entity\\boat\\boat_birch.png",
                        "assets\\minecraft\\textures\\entity\\boat\\birch.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\boat\\boat_darkoak.png",
                        "assets\\minecraft\\textures\\entity\\boat\\boat_darkoak.png",
                        "assets\\minecraft\\textures\\entity\\boat\\dark_oak.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\boat\\boat_jungle.png",
                        "assets\\minecraft\\textures\\entity\\boat\\boat_jungle.png",
                        "assets\\minecraft\\textures\\entity\\boat\\jungle.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\boat.png",
                        "assets\\minecraft\\textures\\entity\\boat\\boat_oak.png",
                        "assets\\minecraft\\textures\\entity\\boat\\boat_oak.png",
                        "assets\\minecraft\\textures\\entity\\boat\\oak.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\boat\\boat_spruce.png",
                        "assets\\minecraft\\textures\\entity\\boat\\boat_spruce.png",
                        "assets\\minecraft\\textures\\entity\\boat\\spruce.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\cat",
                "assets\\minecraft\\textures\\entity\\cat",
                "assets\\minecraft\\textures\\entity\\cat",
                "assets\\minecraft\\textures\\entity\\cat",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\cat\\black.png",
                        "assets\\minecraft\\textures\\entity\\cat\\black.png",
                        "assets\\minecraft\\textures\\entity\\cat\\black.png",
                        "assets\\minecraft\\textures\\entity\\cat\\black.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\cat\\ocelot.png",
                        "assets\\minecraft\\textures\\entity\\cat\\ocelot.png",
                        "assets\\minecraft\\textures\\entity\\cat\\ocelot.png",
                        "assets\\minecraft\\textures\\entity\\cat\\ocelot.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\cat\\red.png",
                        "assets\\minecraft\\textures\\entity\\cat\\red.png",
                        "assets\\minecraft\\textures\\entity\\cat\\red.png",
                        "assets\\minecraft\\textures\\entity\\cat\\red.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\cat\\siamese.png",
                        "assets\\minecraft\\textures\\entity\\cat\\siamese.png",
                        "assets\\minecraft\\textures\\entity\\cat\\siamese.png",
                        "assets\\minecraft\\textures\\entity\\cat\\siamese.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\chest",
                "assets\\minecraft\\textures\\entity\\chest",
                "assets\\minecraft\\textures\\entity\\chest",
                "assets\\minecraft\\textures\\entity\\chest",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\chest\\christmas.png",
                        "assets\\minecraft\\textures\\entity\\chest\\christmas.png",
                        "assets\\minecraft\\textures\\entity\\chest\\christmas.png",
                        "assets\\minecraft\\textures\\entity\\chest\\christmas.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\chest\\christmas_double.png",
                        "assets\\minecraft\\textures\\entity\\chest\\christmas_double.png",
                        "assets\\minecraft\\textures\\entity\\chest\\christmas_double.png",
                        "assets\\minecraft\\textures\\entity\\chest\\christmas_double.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\chest\\ender.png",
                        "assets\\minecraft\\textures\\entity\\chest\\ender.png",
                        "assets\\minecraft\\textures\\entity\\chest\\ender.png",
                        "assets\\minecraft\\textures\\entity\\chest\\ender.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\chest\\normal.png",
                        "assets\\minecraft\\textures\\entity\\chest\\normal.png",
                        "assets\\minecraft\\textures\\entity\\chest\\normal.png",
                        "assets\\minecraft\\textures\\entity\\chest\\normal.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\chest\\normal_double.png",
                        "assets\\minecraft\\textures\\entity\\chest\\normal_double.png",
                        "assets\\minecraft\\textures\\entity\\chest\\normal_double.png",
                        "assets\\minecraft\\textures\\entity\\chest\\normal_double.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\chest\\trapped.png",
                        "assets\\minecraft\\textures\\entity\\chest\\trapped.png",
                        "assets\\minecraft\\textures\\entity\\chest\\trapped.png",
                        "assets\\minecraft\\textures\\entity\\chest\\trapped.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\chest\\trapped_double.png",
                        "assets\\minecraft\\textures\\entity\\chest\\trapped_double.png",
                        "assets\\minecraft\\textures\\entity\\chest\\trapped_double.png",
                        "assets\\minecraft\\textures\\entity\\chest\\trapped_double.png"
                    ),
                }
            ),
            new Carpetas
            (
                "",
                "",
                "",
                "assets\\minecraft\\textures\\entity\\conduit",
                new Archivos[]
                {
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\conduit\\base.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\conduit\\break_particle.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\conduit\\cage.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\conduit\\closed_eye.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\conduit\\open_eye.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\conduit\\wind.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\conduit\\wind_vertical.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\cow",
                "assets\\minecraft\\textures\\entity\\cow",
                "assets\\minecraft\\textures\\entity\\cow",
                "assets\\minecraft\\textures\\entity\\cow",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\cow\\cow.png",
                        "assets\\minecraft\\textures\\entity\\cow\\cow.png",
                        "assets\\minecraft\\textures\\entity\\cow\\cow.png",
                        "assets\\minecraft\\textures\\entity\\cow\\cow.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\cow\\mooshroom.png",
                        "assets\\minecraft\\textures\\entity\\cow\\mooshroom.png",
                        "assets\\minecraft\\textures\\entity\\cow\\mooshroom.png",
                        "assets\\minecraft\\textures\\entity\\cow\\mooshroom.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\creeper",
                "assets\\minecraft\\textures\\entity\\creeper",
                "assets\\minecraft\\textures\\entity\\creeper",
                "assets\\minecraft\\textures\\entity\\creeper",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\creeper\\creeper.png",
                        "assets\\minecraft\\textures\\entity\\creeper\\creeper.png",
                        "assets\\minecraft\\textures\\entity\\creeper\\creeper.png",
                        "assets\\minecraft\\textures\\entity\\creeper\\creeper.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\creeper\\creeper_armor.png",
                        "assets\\minecraft\\textures\\entity\\creeper\\creeper_armor.png",
                        "assets\\minecraft\\textures\\entity\\creeper\\creeper_armor.png",
                        "assets\\minecraft\\textures\\entity\\creeper\\creeper_armor.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\endercrystal",
                "assets\\minecraft\\textures\\entity\\endercrystal",
                "assets\\minecraft\\textures\\entity\\endercrystal",
                "assets\\minecraft\\textures\\entity\\end_crystal",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\endercrystal\\endercrystal.png",
                        "assets\\minecraft\\textures\\entity\\endercrystal\\endercrystal.png",
                        "assets\\minecraft\\textures\\entity\\endercrystal\\endercrystal.png",
                        "assets\\minecraft\\textures\\entity\\end_crystal\\end_crystal.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\endercrystal\\endercrystal_beam.png",
                        "assets\\minecraft\\textures\\entity\\endercrystal\\endercrystal_beam.png",
                        "assets\\minecraft\\textures\\entity\\endercrystal\\endercrystal_beam.png",
                        "assets\\minecraft\\textures\\entity\\end_crystal\\end_crystal_beam.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\enderdragon",
                "assets\\minecraft\\textures\\entity\\enderdragon",
                "assets\\minecraft\\textures\\entity\\enderdragon",
                "assets\\minecraft\\textures\\entity\\enderdragon",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\enderdragon\\dragon.png",
                        "assets\\minecraft\\textures\\entity\\enderdragon\\dragon.png",
                        "assets\\minecraft\\textures\\entity\\enderdragon\\dragon.png",
                        "assets\\minecraft\\textures\\entity\\enderdragon\\dragon.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\enderdragon\\dragon_exploding.png",
                        "assets\\minecraft\\textures\\entity\\enderdragon\\dragon_exploding.png",
                        "assets\\minecraft\\textures\\entity\\enderdragon\\dragon_exploding.png",
                        "assets\\minecraft\\textures\\entity\\enderdragon\\dragon_exploding.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\enderdragon\\dragon_eyes.png",
                        "assets\\minecraft\\textures\\entity\\enderdragon\\dragon_eyes.png",
                        "assets\\minecraft\\textures\\entity\\enderdragon\\dragon_eyes.png",
                        "assets\\minecraft\\textures\\entity\\enderdragon\\dragon_eyes.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\enderdragon\\dragon_fireball.png",
                        "assets\\minecraft\\textures\\entity\\enderdragon\\dragon_fireball.png",
                        "assets\\minecraft\\textures\\entity\\enderdragon\\dragon_fireball.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\enderman",
                "assets\\minecraft\\textures\\entity\\enderman",
                "assets\\minecraft\\textures\\entity\\enderman",
                "assets\\minecraft\\textures\\entity\\enderman",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\enderman\\enderman.png",
                        "assets\\minecraft\\textures\\entity\\enderman\\enderman.png",
                        "assets\\minecraft\\textures\\entity\\enderman\\enderman.png",
                        "assets\\minecraft\\textures\\entity\\enderman\\enderman.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\enderman\\enderman_eyes.png",
                        "assets\\minecraft\\textures\\entity\\enderman\\enderman_eyes.png",
                        "assets\\minecraft\\textures\\entity\\enderman\\enderman_eyes.png",
                        "assets\\minecraft\\textures\\entity\\enderman\\enderman_eyes.png"
                    ),
                }
            ),
            new Carpetas
            (
                "",
                "",
                "",
                "assets\\minecraft\\textures\\entity\\fish",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\fish_cod_raw.png",
                        "assets\\minecraft\\textures\\items\\fish_cod_raw.png",
                        "assets\\minecraft\\textures\\items\\fish_cod_raw.png",
                        "assets\\minecraft\\textures\\entity\\fish\\cod.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\fish\\pufferfish.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\fish\\salmon.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\fish\\tropical_a.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\fish\\tropical_a_pattern_1.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\fish\\tropical_a_pattern_2.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\fish\\tropical_a_pattern_3.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\fish\\tropical_a_pattern_4.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\fish\\tropical_a_pattern_5.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\fish\\tropical_a_pattern_6.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\fish\\tropical_b.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\fish\\tropical_b_pattern_1.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\fish\\tropical_b_pattern_2.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\fish\\tropical_b_pattern_3.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\fish\\tropical_b_pattern_4.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\fish\\tropical_b_pattern_5.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\fish\\tropical_b_pattern_6.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\ghast",
                "assets\\minecraft\\textures\\entity\\ghast",
                "assets\\minecraft\\textures\\entity\\ghast",
                "assets\\minecraft\\textures\\entity\\ghast",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\ghast\\ghast.png",
                        "assets\\minecraft\\textures\\entity\\ghast\\ghast.png",
                        "assets\\minecraft\\textures\\entity\\ghast\\ghast.png",
                        "assets\\minecraft\\textures\\entity\\ghast\\ghast.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\ghast\\ghast_shooting.png",
                        "assets\\minecraft\\textures\\entity\\ghast\\ghast_shooting.png",
                        "assets\\minecraft\\textures\\entity\\ghast\\ghast_shooting.png",
                        "assets\\minecraft\\textures\\entity\\ghast\\ghast_shooting.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\horse",
                "assets\\minecraft\\textures\\entity\\horse",
                "assets\\minecraft\\textures\\entity\\horse",
                "assets\\minecraft\\textures\\entity\\horse",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\horse\\donkey.png",
                        "assets\\minecraft\\textures\\entity\\horse\\donkey.png",
                        "assets\\minecraft\\textures\\entity\\horse\\donkey.png",
                        "assets\\minecraft\\textures\\entity\\horse\\donkey.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\horse\\horse_black.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_black.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_black.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_black.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\horse\\horse_brown.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_brown.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_brown.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_brown.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\horse\\horse_chestnut.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_chestnut.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_chestnut.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_chestnut.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\horse\\horse_creamy.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_creamy.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_creamy.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_creamy.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\horse\\horse_darkbrown.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_darkbrown.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_darkbrown.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_darkbrown.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\horse\\horse_gray.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_gray.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_gray.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_gray.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\horse\\horse_markings_blackdots.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_markings_blackdots.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_markings_blackdots.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_markings_blackdots.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\horse\\horse_markings_white.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_markings_white.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_markings_white.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_markings_white.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\horse\\horse_markings_whitedots.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_markings_whitedots.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_markings_whitedots.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_markings_whitedots.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\horse\\horse_markings_whitefield.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_markings_whitefield.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_markings_whitefield.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_markings_whitefield.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\horse\\horse_skeleton.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_skeleton.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_skeleton.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_skeleton.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\horse\\horse_white.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_white.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_white.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_white.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\horse\\horse_zombie.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_zombie.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_zombie.png",
                        "assets\\minecraft\\textures\\entity\\horse\\horse_zombie.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\horse\\mule.png",
                        "assets\\minecraft\\textures\\entity\\horse\\mule.png",
                        "assets\\minecraft\\textures\\entity\\horse\\mule.png",
                        "assets\\minecraft\\textures\\entity\\horse\\mule.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\horse\\armor",
                "assets\\minecraft\\textures\\entity\\horse\\armor",
                "assets\\minecraft\\textures\\entity\\horse\\armor",
                "assets\\minecraft\\textures\\entity\\horse\\armor",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\horse\\armor\\horse_armor_diamond.png",
                        "assets\\minecraft\\textures\\entity\\horse\\armor\\horse_armor_diamond.png",
                        "assets\\minecraft\\textures\\entity\\horse\\armor\\horse_armor_diamond.png",
                        "assets\\minecraft\\textures\\entity\\horse\\armor\\horse_armor_diamond.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\horse\\armor\\horse_armor_gold.png",
                        "assets\\minecraft\\textures\\entity\\horse\\armor\\horse_armor_gold.png",
                        "assets\\minecraft\\textures\\entity\\horse\\armor\\horse_armor_gold.png",
                        "assets\\minecraft\\textures\\entity\\horse\\armor\\horse_armor_gold.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\horse\\armor\\horse_armor_iron.png",
                        "assets\\minecraft\\textures\\entity\\horse\\armor\\horse_armor_iron.png",
                        "assets\\minecraft\\textures\\entity\\horse\\armor\\horse_armor_iron.png",
                        "assets\\minecraft\\textures\\entity\\horse\\armor\\horse_armor_iron.png"
                    ),
                }
            ),
            new Carpetas
            (
                "",
                "",
                "assets\\minecraft\\textures\\entity\\illager",
                "assets\\minecraft\\textures\\entity\\illager",
                new Archivos[]
                {
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\illager\\evoker.png",
                        "assets\\minecraft\\textures\\entity\\illager\\evoker.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\illager\\fangs.png",
                        "assets\\minecraft\\textures\\entity\\illager\\evoker_fangs.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\illager\\illusionist.png",
                        "assets\\minecraft\\textures\\entity\\illager\\illusioner.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\illager\\vex.png",
                        "assets\\minecraft\\textures\\entity\\illager\\vex.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\illager\\vex_charging.png",
                        "assets\\minecraft\\textures\\entity\\illager\\vex_charging.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\illager\\vindicator.png",
                        "assets\\minecraft\\textures\\entity\\illager\\vindicator.png"
                    ),
                }
            ),
            new Carpetas
            (
                "",
                "",
                "assets\\minecraft\\textures\\entity\\llama",
                "assets\\minecraft\\textures\\entity\\llama",
                new Archivos[]
                {
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\llama.png",
                        ""
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\llama_brown.png",
                        "assets\\minecraft\\textures\\entity\\llama\\brown.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\llama_creamy.png",
                        "assets\\minecraft\\textures\\entity\\llama\\creamy.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\llama_gray.png",
                        "assets\\minecraft\\textures\\entity\\llama\\gray.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\spit.png",
                        "assets\\minecraft\\textures\\entity\\llama\\spit.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\llama_white.png",
                        "assets\\minecraft\\textures\\entity\\llama\\white.png"
                    ),
                }
            ),
            new Carpetas
            (
                "",
                "",
                "assets\\minecraft\\textures\\entity\\llama\\decor",
                "assets\\minecraft\\textures\\entity\\llama\\decor",
                new Archivos[]
                {
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\decor_black.png",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\black.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\decor_blue.png",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\blue.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\decor_brown.png",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\brown.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\decor_cyan.png",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\cyan.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\decor_gray.png",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\gray.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\decor_green.png",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\green.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\decor_light_blue.png",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\light_blue.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\decor_silver.png",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\light_gray.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\decor_lime.png",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\lime.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\decor_magenta.png",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\magenta.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\decor_orange.png",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\orange.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\decor_pink.png",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\pink.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\decor_purple.png",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\purple.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\decor_red.png",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\red.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\decor_white.png",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\white.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\decor_yellow.png",
                        "assets\\minecraft\\textures\\entity\\llama\\decor\\yellow.png"
                    ),
                }
            ),
            new Carpetas
            (
                "",
                "",
                "assets\\minecraft\\textures\\entity\\parrot",
                "assets\\minecraft\\textures\\entity\\parrot",
                new Archivos[]
                {
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\parrot\\parrot_blue.png",
                        "assets\\minecraft\\textures\\entity\\parrot\\parrot_blue.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\parrot\\parrot_green.png",
                        "assets\\minecraft\\textures\\entity\\parrot\\parrot_green.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\parrot\\parrot_grey.png",
                        "assets\\minecraft\\textures\\entity\\parrot\\parrot_grey.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\parrot\\parrot_red_blue.png",
                        "assets\\minecraft\\textures\\entity\\parrot\\parrot_red_blue.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\parrot\\parrot_yellow_blue.png",
                        "assets\\minecraft\\textures\\entity\\parrot\\parrot_yellow_blue.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\pig",
                "assets\\minecraft\\textures\\entity\\pig",
                "assets\\minecraft\\textures\\entity\\pig",
                "assets\\minecraft\\textures\\entity\\pig",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\pig\\pig.png",
                        "assets\\minecraft\\textures\\entity\\pig\\pig.png",
                        "assets\\minecraft\\textures\\entity\\pig\\pig.png",
                        "assets\\minecraft\\textures\\entity\\pig\\pig.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\pig\\pig_saddle.png",
                        "assets\\minecraft\\textures\\entity\\pig\\pig_saddle.png",
                        "assets\\minecraft\\textures\\entity\\pig\\pig_saddle.png",
                        "assets\\minecraft\\textures\\entity\\pig\\pig_saddle.png"
                    ),
                }
            ),
            new Carpetas
            (
                "",
                "assets\\minecraft\\textures\\entity\\projectiles",
                "assets\\minecraft\\textures\\entity\\projectiles",
                "assets\\minecraft\\textures\\entity\\projectiles",
                new Archivos[]
                {
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\projectiles\\arrow.png",
                        "assets\\minecraft\\textures\\entity\\projectiles\\arrow.png",
                        "assets\\minecraft\\textures\\entity\\projectiles\\arrow.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\projectiles\\spectral_arrow.png",
                        "assets\\minecraft\\textures\\entity\\projectiles\\spectral_arrow.png",
                        "assets\\minecraft\\textures\\entity\\projectiles\\spectral_arrow.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\projectiles\\tipped_arrow.png",
                        "assets\\minecraft\\textures\\entity\\projectiles\\tipped_arrow.png",
                        "assets\\minecraft\\textures\\entity\\projectiles\\tipped_arrow.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\rabbit",
                "assets\\minecraft\\textures\\entity\\rabbit",
                "assets\\minecraft\\textures\\entity\\rabbit",
                "assets\\minecraft\\textures\\entity\\rabbit",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\rabbit\\black.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\black.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\black.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\black.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\rabbit\\brown.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\brown.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\brown.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\brown.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\rabbit\\caerbannog.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\caerbannog.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\caerbannog.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\caerbannog.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\rabbit\\gold.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\gold.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\gold.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\gold.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\rabbit\\salt.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\salt.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\salt.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\salt.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\rabbit\\toast.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\toast.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\toast.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\toast.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\rabbit\\white.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\white.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\white.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\white.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\rabbit\\white_splotched.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\white_splotched.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\white_splotched.png",
                        "assets\\minecraft\\textures\\entity\\rabbit\\white_splotched.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\sheep",
                "assets\\minecraft\\textures\\entity\\sheep",
                "assets\\minecraft\\textures\\entity\\sheep",
                "assets\\minecraft\\textures\\entity\\sheep",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\sheep\\sheep.png",
                        "assets\\minecraft\\textures\\entity\\sheep\\sheep.png",
                        "assets\\minecraft\\textures\\entity\\sheep\\sheep.png",
                        "assets\\minecraft\\textures\\entity\\sheep\\sheep.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\sheep\\sheep_fur.png",
                        "assets\\minecraft\\textures\\entity\\sheep\\sheep_fur.png",
                        "assets\\minecraft\\textures\\entity\\sheep\\sheep_fur.png",
                        "assets\\minecraft\\textures\\entity\\sheep\\sheep_fur.png"
                    ),
                }
            ),
            new Carpetas
            (
                "",
                "assets\\minecraft\\textures\\entity\\shield",
                "assets\\minecraft\\textures\\entity\\shield",
                "assets\\minecraft\\textures\\entity\\shield",
                new Archivos[]
                {
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\base.png",
                        "assets\\minecraft\\textures\\entity\\shield\\base.png",
                        "assets\\minecraft\\textures\\entity\\shield\\base.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\border.png",
                        "assets\\minecraft\\textures\\entity\\shield\\border.png",
                        "assets\\minecraft\\textures\\entity\\shield\\border.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\bricks.png",
                        "assets\\minecraft\\textures\\entity\\shield\\bricks.png",
                        "assets\\minecraft\\textures\\entity\\shield\\bricks.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\circle.png",
                        "assets\\minecraft\\textures\\entity\\shield\\circle.png",
                        "assets\\minecraft\\textures\\entity\\shield\\circle.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\creeper.png",
                        "assets\\minecraft\\textures\\entity\\shield\\creeper.png",
                        "assets\\minecraft\\textures\\entity\\shield\\creeper.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\cross.png",
                        "assets\\minecraft\\textures\\entity\\shield\\cross.png",
                        "assets\\minecraft\\textures\\entity\\shield\\cross.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\curly_border.png",
                        "assets\\minecraft\\textures\\entity\\shield\\curly_border.png",
                        "assets\\minecraft\\textures\\entity\\shield\\curly_border.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\diagonal_left.png",
                        "assets\\minecraft\\textures\\entity\\shield\\diagonal_left.png",
                        "assets\\minecraft\\textures\\entity\\shield\\diagonal_left.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\diagonal_right.png",
                        "assets\\minecraft\\textures\\entity\\shield\\diagonal_right.png",
                        "assets\\minecraft\\textures\\entity\\shield\\diagonal_right.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\diagonal_up_left.png",
                        "assets\\minecraft\\textures\\entity\\shield\\diagonal_up_left.png",
                        "assets\\minecraft\\textures\\entity\\shield\\diagonal_up_left.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\diagonal_up_right.png",
                        "assets\\minecraft\\textures\\entity\\shield\\diagonal_up_right.png",
                        "assets\\minecraft\\textures\\entity\\shield\\diagonal_up_right.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\flower.png",
                        "assets\\minecraft\\textures\\entity\\shield\\flower.png",
                        "assets\\minecraft\\textures\\entity\\shield\\flower.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\gradient.png",
                        "assets\\minecraft\\textures\\entity\\shield\\gradient.png",
                        "assets\\minecraft\\textures\\entity\\shield\\gradient.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\gradient_up.png",
                        "assets\\minecraft\\textures\\entity\\shield\\gradient_up.png",
                        "assets\\minecraft\\textures\\entity\\shield\\gradient_up.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\half_horizontal.png",
                        "assets\\minecraft\\textures\\entity\\shield\\half_horizontal.png",
                        "assets\\minecraft\\textures\\entity\\shield\\half_horizontal.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\half_horizontal_bottom.png",
                        "assets\\minecraft\\textures\\entity\\shield\\half_horizontal_bottom.png",
                        "assets\\minecraft\\textures\\entity\\shield\\half_horizontal_bottom.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\half_vertical.png",
                        "assets\\minecraft\\textures\\entity\\shield\\half_vertical.png",
                        "assets\\minecraft\\textures\\entity\\shield\\half_vertical.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\half_vertical_right.png",
                        "assets\\minecraft\\textures\\entity\\shield\\half_vertical_right.png",
                        "assets\\minecraft\\textures\\entity\\shield\\half_vertical_right.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\mojang.png",
                        "assets\\minecraft\\textures\\entity\\shield\\mojang.png",
                        "assets\\minecraft\\textures\\entity\\shield\\mojang.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\rhombus.png",
                        "assets\\minecraft\\textures\\entity\\shield\\rhombus.png",
                        "assets\\minecraft\\textures\\entity\\shield\\rhombus.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\skull.png",
                        "assets\\minecraft\\textures\\entity\\shield\\skull.png",
                        "assets\\minecraft\\textures\\entity\\shield\\skull.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\small_stripes.png",
                        "assets\\minecraft\\textures\\entity\\shield\\small_stripes.png",
                        "assets\\minecraft\\textures\\entity\\shield\\small_stripes.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\square_bottom_left.png",
                        "assets\\minecraft\\textures\\entity\\shield\\square_bottom_left.png",
                        "assets\\minecraft\\textures\\entity\\shield\\square_bottom_left.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\square_bottom_right.png",
                        "assets\\minecraft\\textures\\entity\\shield\\square_bottom_right.png",
                        "assets\\minecraft\\textures\\entity\\shield\\square_bottom_right.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\square_top_left.png",
                        "assets\\minecraft\\textures\\entity\\shield\\square_top_left.png",
                        "assets\\minecraft\\textures\\entity\\shield\\square_top_left.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\square_top_right.png",
                        "assets\\minecraft\\textures\\entity\\shield\\square_top_right.png",
                        "assets\\minecraft\\textures\\entity\\shield\\square_top_right.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\straight_cross.png",
                        "assets\\minecraft\\textures\\entity\\shield\\straight_cross.png",
                        "assets\\minecraft\\textures\\entity\\shield\\straight_cross.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_bottom.png",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_bottom.png",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_bottom.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_center.png",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_center.png",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_center.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_downleft.png",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_downleft.png",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_downleft.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_downright.png",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_downright.png",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_downright.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_left.png",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_left.png",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_left.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_middle.png",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_middle.png",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_middle.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_right.png",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_right.png",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_right.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_top.png",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_top.png",
                        "assets\\minecraft\\textures\\entity\\shield\\stripe_top.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\triangle_bottom.png",
                        "assets\\minecraft\\textures\\entity\\shield\\triangle_bottom.png",
                        "assets\\minecraft\\textures\\entity\\shield\\triangle_bottom.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\triangle_top.png",
                        "assets\\minecraft\\textures\\entity\\shield\\triangle_top.png",
                        "assets\\minecraft\\textures\\entity\\shield\\triangle_top.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\triangles_bottom.png",
                        "assets\\minecraft\\textures\\entity\\shield\\triangles_bottom.png",
                        "assets\\minecraft\\textures\\entity\\shield\\triangles_bottom.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shield\\triangles_top.png",
                        "assets\\minecraft\\textures\\entity\\shield\\triangles_top.png",
                        "assets\\minecraft\\textures\\entity\\shield\\triangles_top.png"
                    ),
                }
            ),
            new Carpetas
            (
                "",
                "assets\\minecraft\\textures\\entity\\shulker",
                "assets\\minecraft\\textures\\entity\\shulker",
                "assets\\minecraft\\textures\\entity\\shulker",
                new Archivos[]
                {
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shulker\\endergolem.png",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_purple.png",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_black.png",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_black.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_blue.png",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_blue.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_brown.png",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_brown.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_cyan.png",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_cyan.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_gray.png",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_gray.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_green.png",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_green.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_light_blue.png",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_light_blue.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_silver.png",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_light_gray.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_lime.png",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_lime.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_magenta.png",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_magenta.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_orange.png",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_orange.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_pink.png",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_pink.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_purple.png",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_purple.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_red.png",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_red.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_white.png",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_white.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_yellow.png",
                        "assets\\minecraft\\textures\\entity\\shulker\\shulker_yellow.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\shulker\\spark.png",
                        "assets\\minecraft\\textures\\entity\\shulker\\spark.png",
                        "assets\\minecraft\\textures\\entity\\shulker\\spark.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\skeleton",
                "assets\\minecraft\\textures\\entity\\skeleton",
                "assets\\minecraft\\textures\\entity\\skeleton",
                "assets\\minecraft\\textures\\entity\\skeleton",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\skeleton\\skeleton.png",
                        "assets\\minecraft\\textures\\entity\\skeleton\\skeleton.png",
                        "assets\\minecraft\\textures\\entity\\skeleton\\skeleton.png",
                        "assets\\minecraft\\textures\\entity\\skeleton\\skeleton.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\skeleton\\stray.png",
                        "assets\\minecraft\\textures\\entity\\skeleton\\stray.png",
                        "assets\\minecraft\\textures\\entity\\skeleton\\stray.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\skeleton\\stray_overlay.png",
                        "assets\\minecraft\\textures\\entity\\skeleton\\stray_overlay.png",
                        "assets\\minecraft\\textures\\entity\\skeleton\\stray_overlay.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\skeleton\\wither_skeleton.png",
                        "assets\\minecraft\\textures\\entity\\skeleton\\wither_skeleton.png",
                        "assets\\minecraft\\textures\\entity\\skeleton\\wither_skeleton.png",
                        "assets\\minecraft\\textures\\entity\\skeleton\\wither_skeleton.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\slime",
                "assets\\minecraft\\textures\\entity\\slime",
                "assets\\minecraft\\textures\\entity\\slime",
                "assets\\minecraft\\textures\\entity\\slime",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\slime\\magmacube.png",
                        "assets\\minecraft\\textures\\entity\\slime\\magmacube.png",
                        "assets\\minecraft\\textures\\entity\\slime\\magmacube.png",
                        "assets\\minecraft\\textures\\entity\\slime\\magmacube.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\slime\\slime.png",
                        "assets\\minecraft\\textures\\entity\\slime\\slime.png",
                        "assets\\minecraft\\textures\\entity\\slime\\slime.png",
                        "assets\\minecraft\\textures\\entity\\slime\\slime.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\spider",
                "assets\\minecraft\\textures\\entity\\spider",
                "assets\\minecraft\\textures\\entity\\spider",
                "assets\\minecraft\\textures\\entity\\spider",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\spider\\cave_spider.png",
                        "assets\\minecraft\\textures\\entity\\spider\\cave_spider.png",
                        "assets\\minecraft\\textures\\entity\\spider\\cave_spider.png",
                        "assets\\minecraft\\textures\\entity\\spider\\cave_spider.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\spider\\spider.png",
                        "assets\\minecraft\\textures\\entity\\spider\\spider.png",
                        "assets\\minecraft\\textures\\entity\\spider\\spider.png",
                        "assets\\minecraft\\textures\\entity\\spider\\spider.png"
                    ),
                }
            ),
            new Carpetas
            (
                "",
                "",
                "",
                "assets\\minecraft\\textures\\entity\\turtle",
                new Archivos[]
                {
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\turtle\\big_sea_turtle.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\villager",
                "assets\\minecraft\\textures\\entity\\villager",
                "assets\\minecraft\\textures\\entity\\villager",
                "assets\\minecraft\\textures\\entity\\villager",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\villager\\butcher.png",
                        "assets\\minecraft\\textures\\entity\\villager\\butcher.png",
                        "assets\\minecraft\\textures\\entity\\villager\\butcher.png",
                        "assets\\minecraft\\textures\\entity\\villager\\butcher.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\villager\\farmer.png",
                        "assets\\minecraft\\textures\\entity\\villager\\farmer.png",
                        "assets\\minecraft\\textures\\entity\\villager\\farmer.png",
                        "assets\\minecraft\\textures\\entity\\villager\\farmer.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\villager\\librarian.png",
                        "assets\\minecraft\\textures\\entity\\villager\\librarian.png",
                        "assets\\minecraft\\textures\\entity\\villager\\librarian.png",
                        "assets\\minecraft\\textures\\entity\\villager\\librarian.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\villager\\priest.png",
                        "assets\\minecraft\\textures\\entity\\villager\\priest.png",
                        "assets\\minecraft\\textures\\entity\\villager\\priest.png",
                        "assets\\minecraft\\textures\\entity\\villager\\priest.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\villager\\smith.png",
                        "assets\\minecraft\\textures\\entity\\villager\\smith.png",
                        "assets\\minecraft\\textures\\entity\\villager\\smith.png",
                        "assets\\minecraft\\textures\\entity\\villager\\smith.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\villager\\villager.png",
                        "assets\\minecraft\\textures\\entity\\villager\\villager.png",
                        "assets\\minecraft\\textures\\entity\\villager\\villager.png",
                        "assets\\minecraft\\textures\\entity\\villager\\villager.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\wither",
                "assets\\minecraft\\textures\\entity\\wither",
                "assets\\minecraft\\textures\\entity\\wither",
                "assets\\minecraft\\textures\\entity\\wither",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\wither\\wither.png",
                        "assets\\minecraft\\textures\\entity\\wither\\wither.png",
                        "assets\\minecraft\\textures\\entity\\wither\\wither.png",
                        "assets\\minecraft\\textures\\entity\\wither\\wither.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\wither\\wither_armor.png",
                        "assets\\minecraft\\textures\\entity\\wither\\wither_armor.png",
                        "assets\\minecraft\\textures\\entity\\wither\\wither_armor.png",
                        "assets\\minecraft\\textures\\entity\\wither\\wither_armor.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\wither\\wither_invulnerable.png",
                        "assets\\minecraft\\textures\\entity\\wither\\wither_invulnerable.png",
                        "assets\\minecraft\\textures\\entity\\wither\\wither_invulnerable.png",
                        "assets\\minecraft\\textures\\entity\\wither\\wither_invulnerable.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\wolf",
                "assets\\minecraft\\textures\\entity\\wolf",
                "assets\\minecraft\\textures\\entity\\wolf",
                "assets\\minecraft\\textures\\entity\\wolf",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\wolf\\wolf.png",
                        "assets\\minecraft\\textures\\entity\\wolf\\wolf.png",
                        "assets\\minecraft\\textures\\entity\\wolf\\wolf.png",
                        "assets\\minecraft\\textures\\entity\\wolf\\wolf.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\wolf\\wolf_angry.png",
                        "assets\\minecraft\\textures\\entity\\wolf\\wolf_angry.png",
                        "assets\\minecraft\\textures\\entity\\wolf\\wolf_angry.png",
                        "assets\\minecraft\\textures\\entity\\wolf\\wolf_angry.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\wolf\\wolf_collar.png",
                        "assets\\minecraft\\textures\\entity\\wolf\\wolf_collar.png",
                        "assets\\minecraft\\textures\\entity\\wolf\\wolf_collar.png",
                        "assets\\minecraft\\textures\\entity\\wolf\\wolf_collar.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\wolf\\wolf_tame.png",
                        "assets\\minecraft\\textures\\entity\\wolf\\wolf_tame.png",
                        "assets\\minecraft\\textures\\entity\\wolf\\wolf_tame.png",
                        "assets\\minecraft\\textures\\entity\\wolf\\wolf_tame.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\entity\\zombie",
                "assets\\minecraft\\textures\\entity\\zombie",
                "assets\\minecraft\\textures\\entity\\zombie",
                "assets\\minecraft\\textures\\entity\\zombie",
                new Archivos[]
                {
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\zombie\\drowned.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\entity\\zombie\\drowned_outer_layer.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\zombie\\husk.png",
                        "assets\\minecraft\\textures\\entity\\zombie\\husk.png",
                        "assets\\minecraft\\textures\\entity\\zombie\\husk.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\zombie\\zombie.png",
                        "assets\\minecraft\\textures\\entity\\zombie\\zombie.png",
                        "assets\\minecraft\\textures\\entity\\zombie\\zombie.png",
                        "assets\\minecraft\\textures\\entity\\zombie\\zombie.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\entity\\zombie\\zombie_villager.png",
                        "assets\\minecraft\\textures\\entity\\zombie\\zombie_villager.png",
                        "assets\\minecraft\\textures\\entity\\zombie\\zombie_villager.png",
                        "assets\\minecraft\\textures\\entity\\zombie\\zombie_villager.png"
                    ),
                }
            ),
            new Carpetas
            (
                "",
                "assets\\minecraft\\textures\\entity\\zombie_villager",
                "assets\\minecraft\\textures\\entity\\zombie_villager",
                "assets\\minecraft\\textures\\entity\\zombie_villager",
                new Archivos[]
                {
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\zombie_villager\\zombie_butcher.png",
                        "assets\\minecraft\\textures\\entity\\zombie_villager\\zombie_butcher.png",
                        "assets\\minecraft\\textures\\entity\\zombie_villager\\zombie_butcher.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\zombie_villager\\zombie_farmer.png",
                        "assets\\minecraft\\textures\\entity\\zombie_villager\\zombie_farmer.png",
                        "assets\\minecraft\\textures\\entity\\zombie_villager\\zombie_farmer.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\zombie_villager\\zombie_librarian.png",
                        "assets\\minecraft\\textures\\entity\\zombie_villager\\zombie_librarian.png",
                        "assets\\minecraft\\textures\\entity\\zombie_villager\\zombie_librarian.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\zombie_villager\\zombie_priest.png",
                        "assets\\minecraft\\textures\\entity\\zombie_villager\\zombie_priest.png",
                        "assets\\minecraft\\textures\\entity\\zombie_villager\\zombie_priest.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\zombie_villager\\zombie_smith.png",
                        "assets\\minecraft\\textures\\entity\\zombie_villager\\zombie_smith.png",
                        "assets\\minecraft\\textures\\entity\\zombie_villager\\zombie_smith.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\entity\\zombie_villager\\zombie_villager.png",
                        "assets\\minecraft\\textures\\entity\\zombie_villager\\zombie_villager.png",
                        "assets\\minecraft\\textures\\entity\\zombie_villager\\zombie_villager.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\environment",
                "assets\\minecraft\\textures\\environment",
                "assets\\minecraft\\textures\\environment",
                "assets\\minecraft\\textures\\environment",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\environment\\clouds.png",
                        "assets\\minecraft\\textures\\environment\\clouds.png",
                        "assets\\minecraft\\textures\\environment\\clouds.png",
                        "assets\\minecraft\\textures\\environment\\clouds.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\environment\\end_sky.png",
                        "assets\\minecraft\\textures\\environment\\end_sky.png",
                        "assets\\minecraft\\textures\\environment\\end_sky.png",
                        "assets\\minecraft\\textures\\environment\\end_sky.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\environment\\moon_phases.png",
                        "assets\\minecraft\\textures\\environment\\moon_phases.png",
                        "assets\\minecraft\\textures\\environment\\moon_phases.png",
                        "assets\\minecraft\\textures\\environment\\moon_phases.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\environment\\rain.png",
                        "assets\\minecraft\\textures\\environment\\rain.png",
                        "assets\\minecraft\\textures\\environment\\rain.png",
                        "assets\\minecraft\\textures\\environment\\rain.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\environment\\snow.png",
                        "assets\\minecraft\\textures\\environment\\snow.png",
                        "assets\\minecraft\\textures\\environment\\snow.png",
                        "assets\\minecraft\\textures\\environment\\snow.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\environment\\sun.png",
                        "assets\\minecraft\\textures\\environment\\sun.png",
                        "assets\\minecraft\\textures\\environment\\sun.png",
                        "assets\\minecraft\\textures\\environment\\sun.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\font",
                "assets\\minecraft\\textures\\font",
                "assets\\minecraft\\textures\\font",
                "assets\\minecraft\\textures\\font",
                new Archivos[]
                {
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\font\\accented.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\ascii.png",
                        "assets\\minecraft\\textures\\font\\ascii.png",
                        "assets\\minecraft\\textures\\font\\ascii.png",
                        "assets\\minecraft\\textures\\font\\ascii.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\ascii_sga.png",
                        "assets\\minecraft\\textures\\font\\ascii_sga.png",
                        "assets\\minecraft\\textures\\font\\ascii_sga.png",
                        "assets\\minecraft\\textures\\font\\ascii_sga.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\font\\nonlatin_european.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_00.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_00.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_00.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_00.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_01.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_01.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_01.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_01.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_02.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_02.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_02.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_02.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_03.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_03.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_03.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_03.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_04.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_04.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_04.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_04.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_05.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_05.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_05.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_05.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_06.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_06.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_06.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_06.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_07.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_07.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_07.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_07.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_09.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_09.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_09.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_09.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_0a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_0a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_0a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_0a.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_0b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_0b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_0b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_0b.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_0c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_0c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_0c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_0c.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_0d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_0d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_0d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_0d.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_0e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_0e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_0e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_0e.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_0f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_0f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_0f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_0f.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_10.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_10.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_10.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_10.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_11.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_11.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_11.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_11.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_12.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_12.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_12.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_12.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_13.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_13.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_13.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_13.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_14.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_14.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_14.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_14.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_15.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_15.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_15.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_15.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_16.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_16.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_16.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_16.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_17.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_17.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_17.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_17.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_18.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_18.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_18.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_18.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_19.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_19.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_19.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_19.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_1a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_1a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_1a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_1a.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_1b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_1b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_1b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_1b.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_1c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_1c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_1c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_1c.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_1d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_1d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_1d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_1d.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_1e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_1e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_1e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_1e.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_1f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_1f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_1f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_1f.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_20.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_20.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_20.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_20.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_21.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_21.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_21.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_21.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_22.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_22.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_22.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_22.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_23.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_23.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_23.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_23.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_24.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_24.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_24.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_24.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_25.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_25.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_25.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_25.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_26.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_26.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_26.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_26.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_27.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_27.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_27.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_27.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_28.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_28.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_28.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_28.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_29.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_29.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_29.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_29.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_2a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_2a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_2a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_2a.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_2b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_2b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_2b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_2b.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_2c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_2c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_2c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_2c.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_2d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_2d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_2d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_2d.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_2e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_2e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_2e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_2e.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_2f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_2f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_2f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_2f.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_30.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_30.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_30.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_30.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_31.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_31.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_31.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_31.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_32.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_32.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_32.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_32.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_33.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_33.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_33.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_33.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_34.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_34.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_34.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_34.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_35.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_35.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_35.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_35.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_36.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_36.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_36.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_36.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_37.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_37.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_37.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_37.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_38.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_38.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_38.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_38.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_39.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_39.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_39.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_39.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_3a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_3a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_3a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_3a.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_3b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_3b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_3b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_3b.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_3c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_3c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_3c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_3c.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_3d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_3d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_3d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_3d.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_3e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_3e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_3e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_3e.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_3f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_3f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_3f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_3f.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_40.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_40.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_40.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_40.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_41.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_41.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_41.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_41.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_42.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_42.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_42.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_42.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_43.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_43.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_43.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_43.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_44.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_44.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_44.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_44.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_45.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_45.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_45.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_45.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_46.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_46.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_46.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_46.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_47.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_47.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_47.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_47.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_48.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_48.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_48.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_48.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_49.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_49.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_49.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_49.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_4a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_4a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_4a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_4a.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_4b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_4b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_4b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_4b.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_4c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_4c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_4c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_4c.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_4d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_4d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_4d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_4d.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_4e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_4e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_4e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_4e.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_4f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_4f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_4f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_4f.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_50.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_50.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_50.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_50.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_51.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_51.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_51.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_51.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_52.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_52.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_52.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_52.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_53.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_53.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_53.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_53.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_54.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_54.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_54.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_54.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_55.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_55.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_55.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_55.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_56.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_56.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_56.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_56.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_57.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_57.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_57.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_57.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_58.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_58.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_58.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_58.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_59.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_59.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_59.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_59.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_5a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_5a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_5a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_5a.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_5b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_5b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_5b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_5b.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_5c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_5c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_5c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_5c.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_5d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_5d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_5d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_5d.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_5e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_5e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_5e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_5e.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_5f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_5f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_5f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_5f.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_60.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_60.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_60.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_60.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_61.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_61.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_61.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_61.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_62.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_62.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_62.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_62.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_63.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_63.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_63.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_63.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_64.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_64.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_64.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_64.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_65.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_65.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_65.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_65.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_66.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_66.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_66.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_66.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_67.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_67.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_67.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_67.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_68.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_68.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_68.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_68.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_69.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_69.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_69.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_69.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_6a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_6a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_6a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_6a.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_6b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_6b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_6b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_6b.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_6c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_6c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_6c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_6c.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_6d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_6d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_6d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_6d.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_6e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_6e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_6e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_6e.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_6f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_6f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_6f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_6f.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_70.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_70.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_70.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_70.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_71.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_71.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_71.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_71.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_72.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_72.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_72.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_72.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_73.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_73.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_73.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_73.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_74.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_74.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_74.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_74.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_75.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_75.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_75.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_75.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_76.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_76.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_76.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_76.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_77.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_77.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_77.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_77.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_78.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_78.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_78.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_78.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_79.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_79.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_79.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_79.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_7a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_7a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_7a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_7a.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_7b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_7b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_7b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_7b.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_7c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_7c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_7c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_7c.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_7d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_7d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_7d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_7d.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_7e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_7e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_7e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_7e.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_7f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_7f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_7f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_7f.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_80.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_80.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_80.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_80.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_81.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_81.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_81.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_81.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_82.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_82.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_82.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_82.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_83.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_83.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_83.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_83.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_84.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_84.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_84.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_84.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_85.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_85.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_85.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_85.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_86.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_86.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_86.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_86.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_87.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_87.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_87.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_87.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_88.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_88.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_88.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_88.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_89.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_89.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_89.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_89.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_8a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_8a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_8a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_8a.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_8b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_8b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_8b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_8b.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_8c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_8c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_8c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_8c.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_8d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_8d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_8d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_8d.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_8e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_8e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_8e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_8e.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_8f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_8f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_8f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_8f.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_90.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_90.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_90.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_90.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_91.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_91.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_91.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_91.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_92.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_92.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_92.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_92.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_93.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_93.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_93.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_93.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_94.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_94.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_94.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_94.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_95.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_95.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_95.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_95.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_96.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_96.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_96.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_96.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_97.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_97.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_97.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_97.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_98.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_98.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_98.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_98.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_99.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_99.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_99.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_99.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_9a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_9a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_9a.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_9a.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_9b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_9b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_9b.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_9b.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_9c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_9c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_9c.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_9c.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_9d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_9d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_9d.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_9d.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_9e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_9e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_9e.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_9e.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_9f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_9f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_9f.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_9f.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_a0.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a0.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a0.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a0.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_a1.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a1.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a1.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a1.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_a2.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a2.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a2.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a2.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_a3.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a3.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a3.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a3.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_a4.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a4.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a4.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a4.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_a5.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a5.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a5.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a5.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_a6.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a6.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a6.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a6.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_a7.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a7.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a7.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a7.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_a8.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a8.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a8.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a8.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_a9.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a9.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a9.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_a9.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_aa.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_aa.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_aa.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_aa.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_ab.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ab.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ab.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ab.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_ac.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ac.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ac.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ac.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_ad.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ad.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ad.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ad.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_ae.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ae.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ae.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ae.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_af.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_af.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_af.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_af.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_b0.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b0.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b0.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b0.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_b1.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b1.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b1.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b1.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_b2.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b2.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b2.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b2.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_b3.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b3.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b3.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b3.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_b4.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b4.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b4.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b4.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_b5.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b5.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b5.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b5.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_b6.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b6.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b6.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b6.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_b7.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b7.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b7.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b7.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_b8.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b8.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b8.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b8.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_b9.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b9.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b9.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_b9.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_ba.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ba.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ba.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ba.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_bb.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_bb.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_bb.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_bb.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_bc.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_bc.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_bc.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_bc.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_bd.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_bd.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_bd.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_bd.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_be.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_be.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_be.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_be.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_bf.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_bf.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_bf.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_bf.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_c0.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c0.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c0.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c0.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_c1.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c1.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c1.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c1.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_c2.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c2.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c2.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c2.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_c3.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c3.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c3.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c3.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_c4.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c4.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c4.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c4.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_c5.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c5.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c5.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c5.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_c6.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c6.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c6.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c6.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_c7.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c7.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c7.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c7.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_c8.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c8.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c8.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c8.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_c9.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c9.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c9.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_c9.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_ca.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ca.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ca.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ca.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_cb.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_cb.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_cb.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_cb.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_cc.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_cc.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_cc.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_cc.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_cd.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_cd.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_cd.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_cd.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_ce.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ce.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ce.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ce.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_cf.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_cf.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_cf.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_cf.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_d0.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d0.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d0.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d0.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_d1.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d1.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d1.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d1.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_d2.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d2.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d2.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d2.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_d3.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d3.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d3.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d3.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_d4.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d4.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d4.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d4.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_d5.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d5.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d5.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d5.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_d6.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d6.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d6.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d6.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_d7.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d7.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d7.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_d7.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_f9.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_f9.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_f9.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_f9.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_fa.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_fa.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_fa.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_fa.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_fb.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_fb.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_fb.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_fb.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_fc.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_fc.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_fc.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_fc.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_fd.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_fd.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_fd.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_fd.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_fe.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_fe.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_fe.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_fe.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\font\\unicode_page_ff.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ff.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ff.png",
                        "assets\\minecraft\\textures\\font\\unicode_page_ff.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\gui",
                "assets\\minecraft\\textures\\gui",
                "assets\\minecraft\\textures\\gui",
                "assets\\minecraft\\textures\\gui",
                new Archivos[]
                {
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\gui\\bars.png",
                        "assets\\minecraft\\textures\\gui\\bars.png",
                        "assets\\minecraft\\textures\\gui\\bars.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\book.png",
                        "assets\\minecraft\\textures\\gui\\book.png",
                        "assets\\minecraft\\textures\\gui\\book.png",
                        "assets\\minecraft\\textures\\gui\\book.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\demo_background.png",
                        "assets\\minecraft\\textures\\gui\\demo_background.png",
                        "assets\\minecraft\\textures\\gui\\demo_background.png",
                        "assets\\minecraft\\textures\\gui\\demo_background.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\icons.png",
                        "assets\\minecraft\\textures\\gui\\icons.png",
                        "assets\\minecraft\\textures\\gui\\icons.png",
                        "assets\\minecraft\\textures\\gui\\icons.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\options_background.png",
                        "assets\\minecraft\\textures\\gui\\options_background.png",
                        "assets\\minecraft\\textures\\gui\\options_background.png",
                        "assets\\minecraft\\textures\\gui\\options_background.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\gui\\recipe_book.png",
                        "assets\\minecraft\\textures\\gui\\recipe_book.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\gui\\recipe_button.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\resource_packs.png",
                        "assets\\minecraft\\textures\\gui\\resource_packs.png",
                        "assets\\minecraft\\textures\\gui\\resource_packs.png",
                        "assets\\minecraft\\textures\\gui\\resource_packs.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\server_selection.png",
                        "assets\\minecraft\\textures\\gui\\server_selection.png",
                        "assets\\minecraft\\textures\\gui\\server_selection.png",
                        "assets\\minecraft\\textures\\gui\\server_selection.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\spectator_widgets.png",
                        "assets\\minecraft\\textures\\gui\\spectator_widgets.png",
                        "assets\\minecraft\\textures\\gui\\spectator_widgets.png",
                        "assets\\minecraft\\textures\\gui\\spectator_widgets.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\stream_indicator.png",
                        "assets\\minecraft\\textures\\gui\\stream_indicator.png",
                        "assets\\minecraft\\textures\\gui\\stream_indicator.png",
                        "assets\\minecraft\\textures\\gui\\stream_indicator.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\gui\\toasts.png",
                        "assets\\minecraft\\textures\\gui\\toasts.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\widgets.png",
                        "assets\\minecraft\\textures\\gui\\widgets.png",
                        "assets\\minecraft\\textures\\gui\\widgets.png",
                        "assets\\minecraft\\textures\\gui\\widgets.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\gui\\world_selection.png",
                        "assets\\minecraft\\textures\\gui\\world_selection.png",
                        "assets\\minecraft\\textures\\gui\\world_selection.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\gui\\achievement",
                "assets\\minecraft\\textures\\gui\\achievement",
                "assets\\minecraft\\textures\\gui\\advancements",
                "assets\\minecraft\\textures\\gui\\advancements",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\achievement\\achievement_background.png",
                        "assets\\minecraft\\textures\\gui\\achievement\\achievement_background.png",
                        "",
                        ""
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\achievement\\achievement_icons.png",
                        "assets\\minecraft\\textures\\gui\\achievement\\achievement_icons.png",
                        "",
                        ""
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\gui\\advancements\\tabs.png",
                        "assets\\minecraft\\textures\\gui\\advancements\\tabs.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\gui\\advancements\\widgets.png",
                        "assets\\minecraft\\textures\\gui\\advancements\\widgets.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\gui\\advancements\\window.png",
                        "assets\\minecraft\\textures\\gui\\advancements\\window.png"
                    ),
                }
            ),
            new Carpetas
            (
                "",
                "",
                "assets\\minecraft\\textures\\gui\\advancements\\backgrounds",
                "assets\\minecraft\\textures\\gui\\advancements\\backgrounds",
                new Archivos[]
                {
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\gui\\advancements\\backgrounds\\adventure.png",
                        "assets\\minecraft\\textures\\gui\\advancements\\backgrounds\\adventure.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\gui\\advancements\\backgrounds\\end.png",
                        "assets\\minecraft\\textures\\gui\\advancements\\backgrounds\\end.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\gui\\advancements\\backgrounds\\husbandry.png",
                        "assets\\minecraft\\textures\\gui\\advancements\\backgrounds\\husbandry.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\gui\\advancements\\backgrounds\\nether.png",
                        "assets\\minecraft\\textures\\gui\\advancements\\backgrounds\\nether.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\gui\\advancements\\backgrounds\\stone.png",
                        "assets\\minecraft\\textures\\gui\\advancements\\backgrounds\\stone.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\gui\\container",
                "assets\\minecraft\\textures\\gui\\container",
                "assets\\minecraft\\textures\\gui\\container",
                "assets\\minecraft\\textures\\gui\\container",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\container\\anvil.png",
                        "assets\\minecraft\\textures\\gui\\container\\anvil.png",
                        "assets\\minecraft\\textures\\gui\\container\\anvil.png",
                        "assets\\minecraft\\textures\\gui\\container\\anvil.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\container\\beacon.png",
                        "assets\\minecraft\\textures\\gui\\container\\beacon.png",
                        "assets\\minecraft\\textures\\gui\\container\\beacon.png",
                        "assets\\minecraft\\textures\\gui\\container\\beacon.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\container\\brewing_stand.png",
                        "assets\\minecraft\\textures\\gui\\container\\brewing_stand.png",
                        "assets\\minecraft\\textures\\gui\\container\\brewing_stand.png",
                        "assets\\minecraft\\textures\\gui\\container\\brewing_stand.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\container\\crafting_table.png",
                        "assets\\minecraft\\textures\\gui\\container\\crafting_table.png",
                        "assets\\minecraft\\textures\\gui\\container\\crafting_table.png",
                        "assets\\minecraft\\textures\\gui\\container\\crafting_table.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\container\\dispenser.png",
                        "assets\\minecraft\\textures\\gui\\container\\dispenser.png",
                        "assets\\minecraft\\textures\\gui\\container\\dispenser.png",
                        "assets\\minecraft\\textures\\gui\\container\\dispenser.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\container\\enchanting_table.png",
                        "assets\\minecraft\\textures\\gui\\container\\enchanting_table.png",
                        "assets\\minecraft\\textures\\gui\\container\\enchanting_table.png",
                        "assets\\minecraft\\textures\\gui\\container\\enchanting_table.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\container\\furnace.png",
                        "assets\\minecraft\\textures\\gui\\container\\furnace.png",
                        "assets\\minecraft\\textures\\gui\\container\\furnace.png",
                        "assets\\minecraft\\textures\\gui\\container\\furnace.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\container\\generic_54.png",
                        "assets\\minecraft\\textures\\gui\\container\\generic_54.png",
                        "assets\\minecraft\\textures\\gui\\container\\generic_54.png",
                        "assets\\minecraft\\textures\\gui\\container\\generic_54.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\container\\hopper.png",
                        "assets\\minecraft\\textures\\gui\\container\\hopper.png",
                        "assets\\minecraft\\textures\\gui\\container\\hopper.png",
                        "assets\\minecraft\\textures\\gui\\container\\hopper.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\container\\horse.png",
                        "assets\\minecraft\\textures\\gui\\container\\horse.png",
                        "assets\\minecraft\\textures\\gui\\container\\horse.png",
                        "assets\\minecraft\\textures\\gui\\container\\horse.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\container\\inventory.png",
                        "assets\\minecraft\\textures\\gui\\container\\inventory.png",
                        "assets\\minecraft\\textures\\gui\\container\\inventory.png",
                        "assets\\minecraft\\textures\\gui\\container\\inventory.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\gui\\container\\recipe_background.png",
                        ""
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\gui\\container\\shulker_box.png",
                        "assets\\minecraft\\textures\\gui\\container\\shulker_box.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\container\\stats_icons.png",
                        "assets\\minecraft\\textures\\gui\\container\\stats_icons.png",
                        "assets\\minecraft\\textures\\gui\\container\\stats_icons.png",
                        "assets\\minecraft\\textures\\gui\\container\\stats_icons.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\container\\villager.png",
                        "assets\\minecraft\\textures\\gui\\container\\villager.png",
                        "assets\\minecraft\\textures\\gui\\container\\villager.png",
                        "assets\\minecraft\\textures\\gui\\container\\villager.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\gui\\container\\creative_inventory",
                "assets\\minecraft\\textures\\gui\\container\\creative_inventory",
                "assets\\minecraft\\textures\\gui\\container\\creative_inventory",
                "assets\\minecraft\\textures\\gui\\container\\creative_inventory",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\container\\creative_inventory\\tab_inventory.png",
                        "assets\\minecraft\\textures\\gui\\container\\creative_inventory\\tab_inventory.png",
                        "assets\\minecraft\\textures\\gui\\container\\creative_inventory\\tab_inventory.png",
                        "assets\\minecraft\\textures\\gui\\container\\creative_inventory\\tab_inventory.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\container\\creative_inventory\\tab_item_search.png",
                        "assets\\minecraft\\textures\\gui\\container\\creative_inventory\\tab_item_search.png",
                        "assets\\minecraft\\textures\\gui\\container\\creative_inventory\\tab_item_search.png",
                        "assets\\minecraft\\textures\\gui\\container\\creative_inventory\\tab_item_search.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\container\\creative_inventory\\tab_items.png",
                        "assets\\minecraft\\textures\\gui\\container\\creative_inventory\\tab_items.png",
                        "assets\\minecraft\\textures\\gui\\container\\creative_inventory\\tab_items.png",
                        "assets\\minecraft\\textures\\gui\\container\\creative_inventory\\tab_items.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\container\\creative_inventory\\tabs.png",
                        "assets\\minecraft\\textures\\gui\\container\\creative_inventory\\tabs.png",
                        "assets\\minecraft\\textures\\gui\\container\\creative_inventory\\tabs.png",
                        "assets\\minecraft\\textures\\gui\\container\\creative_inventory\\tabs.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\gui\\presets",
                "assets\\minecraft\\textures\\gui\\presets",
                "assets\\minecraft\\textures\\gui\\presets",
                "assets\\minecraft\\textures\\gui\\presets",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\presets\\chaos.png",
                        "assets\\minecraft\\textures\\gui\\presets\\chaos.png",
                        "assets\\minecraft\\textures\\gui\\presets\\chaos.png",
                        "assets\\minecraft\\textures\\gui\\presets\\chaos.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\presets\\delight.png",
                        "assets\\minecraft\\textures\\gui\\presets\\delight.png",
                        "assets\\minecraft\\textures\\gui\\presets\\delight.png",
                        "assets\\minecraft\\textures\\gui\\presets\\delight.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\presets\\drought.png",
                        "assets\\minecraft\\textures\\gui\\presets\\drought.png",
                        "assets\\minecraft\\textures\\gui\\presets\\drought.png",
                        "assets\\minecraft\\textures\\gui\\presets\\drought.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\presets\\isles.png",
                        "assets\\minecraft\\textures\\gui\\presets\\isles.png",
                        "assets\\minecraft\\textures\\gui\\presets\\isles.png",
                        "assets\\minecraft\\textures\\gui\\presets\\isles.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\presets\\luck.png",
                        "assets\\minecraft\\textures\\gui\\presets\\luck.png",
                        "assets\\minecraft\\textures\\gui\\presets\\luck.png",
                        "assets\\minecraft\\textures\\gui\\presets\\luck.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\presets\\madness.png",
                        "assets\\minecraft\\textures\\gui\\presets\\madness.png",
                        "assets\\minecraft\\textures\\gui\\presets\\madness.png",
                        "assets\\minecraft\\textures\\gui\\presets\\madness.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\presets\\water.png",
                        "assets\\minecraft\\textures\\gui\\presets\\water.png",
                        "assets\\minecraft\\textures\\gui\\presets\\water.png",
                        "assets\\minecraft\\textures\\gui\\presets\\water.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\gui\\title",
                "assets\\minecraft\\textures\\gui\\title",
                "assets\\minecraft\\textures\\gui\\title",
                "assets\\minecraft\\textures\\gui\\title",
                new Archivos[]
                {
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\gui\\title\\edition.png",
                        "assets\\minecraft\\textures\\gui\\title\\edition.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\title\\minecraft.png",
                        "assets\\minecraft\\textures\\gui\\title\\minecraft.png",
                        "assets\\minecraft\\textures\\gui\\title\\minecraft.png",
                        "assets\\minecraft\\textures\\gui\\title\\minecraft.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\title\\mojang.png",
                        "assets\\minecraft\\textures\\gui\\title\\mojang.png",
                        "assets\\minecraft\\textures\\gui\\title\\mojang.png",
                        "assets\\minecraft\\textures\\gui\\title\\mojang.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\gui\\title\\background",
                "assets\\minecraft\\textures\\gui\\title\\background",
                "assets\\minecraft\\textures\\gui\\title\\background",
                "assets\\minecraft\\textures\\gui\\title\\background",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_0.png",
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_0.png",
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_0.png",
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_0.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_1.png",
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_1.png",
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_1.png",
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_1.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_2.png",
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_2.png",
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_2.png",
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_2.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_3.png",
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_3.png",
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_3.png",
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_3.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_4.png",
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_4.png",
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_4.png",
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_4.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_5.png",
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_5.png",
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_5.png",
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_5.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\gui\\title\\background\\panorama_overlay.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\items",
                "assets\\minecraft\\textures\\items",
                "assets\\minecraft\\textures\\items",
                "assets\\minecraft\\textures\\item",
                new Archivos[]
                {
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\acacia_boat.png",
                        "assets\\minecraft\\textures\\items\\acacia_boat.png",
                        "assets\\minecraft\\textures\\item\\acacia_boat.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\door_acacia.png",
                        "assets\\minecraft\\textures\\items\\door_acacia.png",
                        "assets\\minecraft\\textures\\items\\door_acacia.png",
                        "assets\\minecraft\\textures\\item\\acacia_door.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\apple.png",
                        "assets\\minecraft\\textures\\items\\apple.png",
                        "assets\\minecraft\\textures\\items\\apple.png",
                        "assets\\minecraft\\textures\\item\\apple.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\wooden_armorstand.png",
                        "assets\\minecraft\\textures\\items\\wooden_armorstand.png",
                        "assets\\minecraft\\textures\\items\\wooden_armorstand.png",
                        "assets\\minecraft\\textures\\item\\armor_stand.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\arrow.png",
                        "assets\\minecraft\\textures\\items\\arrow.png",
                        "assets\\minecraft\\textures\\items\\arrow.png",
                        "assets\\minecraft\\textures\\item\\arrow.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\potato_baked.png",
                        "assets\\minecraft\\textures\\items\\potato_baked.png",
                        "assets\\minecraft\\textures\\items\\potato_baked.png",
                        "assets\\minecraft\\textures\\item\\baked_potato.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\banner_base.png",
                        "assets\\minecraft\\textures\\items\\banner_base.png",
                        "",
                        ""
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\banner_overlay.png",
                        "assets\\minecraft\\textures\\items\\banner_overlay.png",
                        "assets\\minecraft\\textures\\items\\banner_overlay.png",
                        ""
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\barrier.png",
                        "assets\\minecraft\\textures\\items\\barrier.png",
                        "assets\\minecraft\\textures\\items\\barrier.png",
                        "assets\\minecraft\\textures\\item\\barrier.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\bed.png",
                        "assets\\minecraft\\textures\\items\\bed.png",
                        "",
                        ""
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\beef_raw.png",
                        "assets\\minecraft\\textures\\items\\beef_raw.png",
                        "assets\\minecraft\\textures\\items\\beef_raw.png",
                        "assets\\minecraft\\textures\\item\\beef.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\beetroot.png",
                        "assets\\minecraft\\textures\\items\\beetroot.png",
                        "assets\\minecraft\\textures\\item\\beetroot.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\beetroot_seeds.png",
                        "assets\\minecraft\\textures\\items\\beetroot_seeds.png",
                        "assets\\minecraft\\textures\\item\\beetroot_seeds.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\beetroot_soup.png",
                        "assets\\minecraft\\textures\\items\\beetroot_soup.png",
                        "assets\\minecraft\\textures\\item\\beetroot_soup.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\birch_boat.png",
                        "assets\\minecraft\\textures\\items\\birch_boat.png",
                        "assets\\minecraft\\textures\\item\\birch_boat.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\door_birch.png",
                        "assets\\minecraft\\textures\\items\\door_birch.png",
                        "assets\\minecraft\\textures\\items\\door_birch.png",
                        "assets\\minecraft\\textures\\item\\birch_door.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\blaze_powder.png",
                        "assets\\minecraft\\textures\\items\\blaze_powder.png",
                        "assets\\minecraft\\textures\\items\\blaze_powder.png",
                        "assets\\minecraft\\textures\\item\\blaze_powder.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\blaze_rod.png",
                        "assets\\minecraft\\textures\\items\\blaze_rod.png",
                        "assets\\minecraft\\textures\\items\\blaze_rod.png",
                        "assets\\minecraft\\textures\\item\\blaze_rod.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\boat.png",
                        "",
                        "",
                        ""
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\bone.png",
                        "assets\\minecraft\\textures\\items\\bone.png",
                        "assets\\minecraft\\textures\\items\\bone.png",
                        "assets\\minecraft\\textures\\item\\bone.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\dye_powder_white.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_white.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_white.png",
                        "assets\\minecraft\\textures\\item\\bone_meal.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\book_normal.png",
                        "assets\\minecraft\\textures\\items\\book_normal.png",
                        "assets\\minecraft\\textures\\items\\book_normal.png",
                        "assets\\minecraft\\textures\\item\\book.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\bow_standby.png",
                        "assets\\minecraft\\textures\\items\\bow_standby.png",
                        "assets\\minecraft\\textures\\items\\bow_standby.png",
                        "assets\\minecraft\\textures\\item\\bow.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\bow_pulling_0.png",
                        "assets\\minecraft\\textures\\items\\bow_pulling_0.png",
                        "assets\\minecraft\\textures\\items\\bow_pulling_0.png",
                        "assets\\minecraft\\textures\\item\\bow_pulling_0.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\bow_pulling_1.png",
                        "assets\\minecraft\\textures\\items\\bow_pulling_1.png",
                        "assets\\minecraft\\textures\\items\\bow_pulling_1.png",
                        "assets\\minecraft\\textures\\item\\bow_pulling_1.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\bow_pulling_2.png",
                        "assets\\minecraft\\textures\\items\\bow_pulling_2.png",
                        "assets\\minecraft\\textures\\items\\bow_pulling_2.png",
                        "assets\\minecraft\\textures\\item\\bow_pulling_2.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\bowl.png",
                        "assets\\minecraft\\textures\\items\\bowl.png",
                        "assets\\minecraft\\textures\\items\\bowl.png",
                        "assets\\minecraft\\textures\\item\\bowl.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\bread.png",
                        "assets\\minecraft\\textures\\items\\bread.png",
                        "assets\\minecraft\\textures\\items\\bread.png",
                        "assets\\minecraft\\textures\\item\\bread.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\brewing_stand.png",
                        "assets\\minecraft\\textures\\items\\brewing_stand.png",
                        "assets\\minecraft\\textures\\items\\brewing_stand.png",
                        "assets\\minecraft\\textures\\item\\brewing_stand.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\brick.png",
                        "assets\\minecraft\\textures\\items\\brick.png",
                        "assets\\minecraft\\textures\\items\\brick.png",
                        "assets\\minecraft\\textures\\item\\brick.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\broken_elytra.png",
                        "assets\\minecraft\\textures\\items\\broken_elytra.png",
                        "assets\\minecraft\\textures\\item\\broken_elytra.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\bucket_empty.png",
                        "assets\\minecraft\\textures\\items\\bucket_empty.png",
                        "assets\\minecraft\\textures\\items\\bucket_empty.png",
                        "assets\\minecraft\\textures\\item\\bucket.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\dye_powder_green.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_green.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_green.png",
                        "assets\\minecraft\\textures\\item\\cactus_green.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\cake.png",
                        "assets\\minecraft\\textures\\items\\cake.png",
                        "assets\\minecraft\\textures\\items\\cake.png",
                        "assets\\minecraft\\textures\\item\\cake.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\carrot.png",
                        "assets\\minecraft\\textures\\items\\carrot.png",
                        "assets\\minecraft\\textures\\items\\carrot.png",
                        "assets\\minecraft\\textures\\item\\carrot.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\carrot_on_a_stick.png",
                        "assets\\minecraft\\textures\\items\\carrot_on_a_stick.png",
                        "assets\\minecraft\\textures\\items\\carrot_on_a_stick.png",
                        "assets\\minecraft\\textures\\item\\carrot_on_a_stick.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\cauldron.png",
                        "assets\\minecraft\\textures\\items\\cauldron.png",
                        "assets\\minecraft\\textures\\items\\cauldron.png",
                        "assets\\minecraft\\textures\\item\\cauldron.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\chainmail_boots.png",
                        "assets\\minecraft\\textures\\items\\chainmail_boots.png",
                        "assets\\minecraft\\textures\\items\\chainmail_boots.png",
                        "assets\\minecraft\\textures\\item\\chainmail_boots.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\chainmail_chestplate.png",
                        "assets\\minecraft\\textures\\items\\chainmail_chestplate.png",
                        "assets\\minecraft\\textures\\items\\chainmail_chestplate.png",
                        "assets\\minecraft\\textures\\item\\chainmail_chestplate.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\chainmail_helmet.png",
                        "assets\\minecraft\\textures\\items\\chainmail_helmet.png",
                        "assets\\minecraft\\textures\\items\\chainmail_helmet.png",
                        "assets\\minecraft\\textures\\item\\chainmail_helmet.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\chainmail_leggings.png",
                        "assets\\minecraft\\textures\\items\\chainmail_leggings.png",
                        "assets\\minecraft\\textures\\items\\chainmail_leggings.png",
                        "assets\\minecraft\\textures\\item\\chainmail_leggings.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\charcoal.png",
                        "assets\\minecraft\\textures\\items\\charcoal.png",
                        "assets\\minecraft\\textures\\items\\charcoal.png",
                        "assets\\minecraft\\textures\\item\\charcoal.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\minecart_chest.png",
                        "assets\\minecraft\\textures\\items\\minecart_chest.png",
                        "assets\\minecraft\\textures\\items\\minecart_chest.png",
                        "assets\\minecraft\\textures\\item\\chest_minecart.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\chicken_raw.png",
                        "assets\\minecraft\\textures\\items\\chicken_raw.png",
                        "assets\\minecraft\\textures\\items\\chicken_raw.png",
                        "assets\\minecraft\\textures\\item\\chicken.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\chorus_fruit.png",
                        "assets\\minecraft\\textures\\items\\chorus_fruit.png",
                        "assets\\minecraft\\textures\\item\\chorus_fruit.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\clay_ball.png",
                        "assets\\minecraft\\textures\\items\\clay_ball.png",
                        "assets\\minecraft\\textures\\items\\clay_ball.png",
                        "assets\\minecraft\\textures\\item\\clay_ball.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\clock.png",
                        "",
                        "",
                        ""
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\clock.png.mcmeta",
                        "",
                        "",
                        ""
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_00.png",
                        "assets\\minecraft\\textures\\items\\clock_00.png",
                        "assets\\minecraft\\textures\\item\\clock_00.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_01.png",
                        "assets\\minecraft\\textures\\items\\clock_01.png",
                        "assets\\minecraft\\textures\\item\\clock_01.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_02.png",
                        "assets\\minecraft\\textures\\items\\clock_02.png",
                        "assets\\minecraft\\textures\\item\\clock_02.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_03.png",
                        "assets\\minecraft\\textures\\items\\clock_03.png",
                        "assets\\minecraft\\textures\\item\\clock_03.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_04.png",
                        "assets\\minecraft\\textures\\items\\clock_04.png",
                        "assets\\minecraft\\textures\\item\\clock_04.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_05.png",
                        "assets\\minecraft\\textures\\items\\clock_05.png",
                        "assets\\minecraft\\textures\\item\\clock_05.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_06.png",
                        "assets\\minecraft\\textures\\items\\clock_06.png",
                        "assets\\minecraft\\textures\\item\\clock_06.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_07.png",
                        "assets\\minecraft\\textures\\items\\clock_07.png",
                        "assets\\minecraft\\textures\\item\\clock_07.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_08.png",
                        "assets\\minecraft\\textures\\items\\clock_08.png",
                        "assets\\minecraft\\textures\\item\\clock_08.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_09.png",
                        "assets\\minecraft\\textures\\items\\clock_09.png",
                        "assets\\minecraft\\textures\\item\\clock_09.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_10.png",
                        "assets\\minecraft\\textures\\items\\clock_10.png",
                        "assets\\minecraft\\textures\\item\\clock_10.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_11.png",
                        "assets\\minecraft\\textures\\items\\clock_11.png",
                        "assets\\minecraft\\textures\\item\\clock_11.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_12.png",
                        "assets\\minecraft\\textures\\items\\clock_12.png",
                        "assets\\minecraft\\textures\\item\\clock_12.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_13.png",
                        "assets\\minecraft\\textures\\items\\clock_13.png",
                        "assets\\minecraft\\textures\\item\\clock_13.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_14.png",
                        "assets\\minecraft\\textures\\items\\clock_14.png",
                        "assets\\minecraft\\textures\\item\\clock_14.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_15.png",
                        "assets\\minecraft\\textures\\items\\clock_15.png",
                        "assets\\minecraft\\textures\\item\\clock_15.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_16.png",
                        "assets\\minecraft\\textures\\items\\clock_16.png",
                        "assets\\minecraft\\textures\\item\\clock_16.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_17.png",
                        "assets\\minecraft\\textures\\items\\clock_17.png",
                        "assets\\minecraft\\textures\\item\\clock_17.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_18.png",
                        "assets\\minecraft\\textures\\items\\clock_18.png",
                        "assets\\minecraft\\textures\\item\\clock_18.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_19.png",
                        "assets\\minecraft\\textures\\items\\clock_19.png",
                        "assets\\minecraft\\textures\\item\\clock_19.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_20.png",
                        "assets\\minecraft\\textures\\items\\clock_20.png",
                        "assets\\minecraft\\textures\\item\\clock_20.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_21.png",
                        "assets\\minecraft\\textures\\items\\clock_21.png",
                        "assets\\minecraft\\textures\\item\\clock_21.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_22.png",
                        "assets\\minecraft\\textures\\items\\clock_22.png",
                        "assets\\minecraft\\textures\\item\\clock_22.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_23.png",
                        "assets\\minecraft\\textures\\items\\clock_23.png",
                        "assets\\minecraft\\textures\\item\\clock_23.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_24.png",
                        "assets\\minecraft\\textures\\items\\clock_24.png",
                        "assets\\minecraft\\textures\\item\\clock_24.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_25.png",
                        "assets\\minecraft\\textures\\items\\clock_25.png",
                        "assets\\minecraft\\textures\\item\\clock_25.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_26.png",
                        "assets\\minecraft\\textures\\items\\clock_26.png",
                        "assets\\minecraft\\textures\\item\\clock_26.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_27.png",
                        "assets\\minecraft\\textures\\items\\clock_27.png",
                        "assets\\minecraft\\textures\\item\\clock_27.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_28.png",
                        "assets\\minecraft\\textures\\items\\clock_28.png",
                        "assets\\minecraft\\textures\\item\\clock_28.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_29.png",
                        "assets\\minecraft\\textures\\items\\clock_29.png",
                        "assets\\minecraft\\textures\\item\\clock_29.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_30.png",
                        "assets\\minecraft\\textures\\items\\clock_30.png",
                        "assets\\minecraft\\textures\\item\\clock_30.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_31.png",
                        "assets\\minecraft\\textures\\items\\clock_31.png",
                        "assets\\minecraft\\textures\\item\\clock_31.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_32.png",
                        "assets\\minecraft\\textures\\items\\clock_32.png",
                        "assets\\minecraft\\textures\\item\\clock_32.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_33.png",
                        "assets\\minecraft\\textures\\items\\clock_33.png",
                        "assets\\minecraft\\textures\\item\\clock_33.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_34.png",
                        "assets\\minecraft\\textures\\items\\clock_34.png",
                        "assets\\minecraft\\textures\\item\\clock_34.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_35.png",
                        "assets\\minecraft\\textures\\items\\clock_35.png",
                        "assets\\minecraft\\textures\\item\\clock_35.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_36.png",
                        "assets\\minecraft\\textures\\items\\clock_36.png",
                        "assets\\minecraft\\textures\\item\\clock_36.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_37.png",
                        "assets\\minecraft\\textures\\items\\clock_37.png",
                        "assets\\minecraft\\textures\\item\\clock_37.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_38.png",
                        "assets\\minecraft\\textures\\items\\clock_38.png",
                        "assets\\minecraft\\textures\\item\\clock_38.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_39.png",
                        "assets\\minecraft\\textures\\items\\clock_39.png",
                        "assets\\minecraft\\textures\\item\\clock_39.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_40.png",
                        "assets\\minecraft\\textures\\items\\clock_40.png",
                        "assets\\minecraft\\textures\\item\\clock_40.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_41.png",
                        "assets\\minecraft\\textures\\items\\clock_41.png",
                        "assets\\minecraft\\textures\\item\\clock_41.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_42.png",
                        "assets\\minecraft\\textures\\items\\clock_42.png",
                        "assets\\minecraft\\textures\\item\\clock_42.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_43.png",
                        "assets\\minecraft\\textures\\items\\clock_43.png",
                        "assets\\minecraft\\textures\\item\\clock_43.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_44.png",
                        "assets\\minecraft\\textures\\items\\clock_44.png",
                        "assets\\minecraft\\textures\\item\\clock_44.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_45.png",
                        "assets\\minecraft\\textures\\items\\clock_45.png",
                        "assets\\minecraft\\textures\\item\\clock_45.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_46.png",
                        "assets\\minecraft\\textures\\items\\clock_46.png",
                        "assets\\minecraft\\textures\\item\\clock_46.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_47.png",
                        "assets\\minecraft\\textures\\items\\clock_47.png",
                        "assets\\minecraft\\textures\\item\\clock_47.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_48.png",
                        "assets\\minecraft\\textures\\items\\clock_48.png",
                        "assets\\minecraft\\textures\\item\\clock_48.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_49.png",
                        "assets\\minecraft\\textures\\items\\clock_49.png",
                        "assets\\minecraft\\textures\\item\\clock_49.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_50.png",
                        "assets\\minecraft\\textures\\items\\clock_50.png",
                        "assets\\minecraft\\textures\\item\\clock_50.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_51.png",
                        "assets\\minecraft\\textures\\items\\clock_51.png",
                        "assets\\minecraft\\textures\\item\\clock_51.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_52.png",
                        "assets\\minecraft\\textures\\items\\clock_52.png",
                        "assets\\minecraft\\textures\\item\\clock_52.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_53.png",
                        "assets\\minecraft\\textures\\items\\clock_53.png",
                        "assets\\minecraft\\textures\\item\\clock_53.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_54.png",
                        "assets\\minecraft\\textures\\items\\clock_54.png",
                        "assets\\minecraft\\textures\\item\\clock_54.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_55.png",
                        "assets\\minecraft\\textures\\items\\clock_55.png",
                        "assets\\minecraft\\textures\\item\\clock_55.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_56.png",
                        "assets\\minecraft\\textures\\items\\clock_56.png",
                        "assets\\minecraft\\textures\\item\\clock_56.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_57.png",
                        "assets\\minecraft\\textures\\items\\clock_57.png",
                        "assets\\minecraft\\textures\\item\\clock_57.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_58.png",
                        "assets\\minecraft\\textures\\items\\clock_58.png",
                        "assets\\minecraft\\textures\\item\\clock_58.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_59.png",
                        "assets\\minecraft\\textures\\items\\clock_59.png",
                        "assets\\minecraft\\textures\\item\\clock_59.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_60.png",
                        "assets\\minecraft\\textures\\items\\clock_60.png",
                        "assets\\minecraft\\textures\\item\\clock_60.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_61.png",
                        "assets\\minecraft\\textures\\items\\clock_61.png",
                        "assets\\minecraft\\textures\\item\\clock_61.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_62.png",
                        "assets\\minecraft\\textures\\items\\clock_62.png",
                        "assets\\minecraft\\textures\\item\\clock_62.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\clock_63.png",
                        "assets\\minecraft\\textures\\items\\clock_63.png",
                        "assets\\minecraft\\textures\\item\\clock_63.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\coal.png",
                        "assets\\minecraft\\textures\\items\\coal.png",
                        "assets\\minecraft\\textures\\items\\coal.png",
                        "assets\\minecraft\\textures\\item\\coal.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\dye_powder_brown.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_brown.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_brown.png",
                        "assets\\minecraft\\textures\\item\\cocoa_beans.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\item\\cod.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\item\\cod_bucket.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\minecart_command_block.png",
                        "assets\\minecraft\\textures\\items\\minecart_command_block.png",
                        "assets\\minecraft\\textures\\items\\minecart_command_block.png",
                        "assets\\minecraft\\textures\\item\\command_block_minecart.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\comparator.png",
                        "assets\\minecraft\\textures\\items\\comparator.png",
                        "assets\\minecraft\\textures\\items\\comparator.png",
                        "assets\\minecraft\\textures\\item\\comparator.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\compass.png",
                        "",
                        "",
                        ""
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\compass.png.mcmeta",
                        "",
                        "",
                        ""
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_00.png",
                        "assets\\minecraft\\textures\\items\\compass_00.png",
                        "assets\\minecraft\\textures\\item\\compass_00.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_01.png",
                        "assets\\minecraft\\textures\\items\\compass_01.png",
                        "assets\\minecraft\\textures\\item\\compass_01.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_02.png",
                        "assets\\minecraft\\textures\\items\\compass_02.png",
                        "assets\\minecraft\\textures\\item\\compass_02.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_03.png",
                        "assets\\minecraft\\textures\\items\\compass_03.png",
                        "assets\\minecraft\\textures\\item\\compass_03.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_04.png",
                        "assets\\minecraft\\textures\\items\\compass_04.png",
                        "assets\\minecraft\\textures\\item\\compass_04.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_05.png",
                        "assets\\minecraft\\textures\\items\\compass_05.png",
                        "assets\\minecraft\\textures\\item\\compass_05.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_06.png",
                        "assets\\minecraft\\textures\\items\\compass_06.png",
                        "assets\\minecraft\\textures\\item\\compass_06.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_07.png",
                        "assets\\minecraft\\textures\\items\\compass_07.png",
                        "assets\\minecraft\\textures\\item\\compass_07.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_08.png",
                        "assets\\minecraft\\textures\\items\\compass_08.png",
                        "assets\\minecraft\\textures\\item\\compass_08.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_09.png",
                        "assets\\minecraft\\textures\\items\\compass_09.png",
                        "assets\\minecraft\\textures\\item\\compass_09.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_10.png",
                        "assets\\minecraft\\textures\\items\\compass_10.png",
                        "assets\\minecraft\\textures\\item\\compass_10.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_11.png",
                        "assets\\minecraft\\textures\\items\\compass_11.png",
                        "assets\\minecraft\\textures\\item\\compass_11.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_12.png",
                        "assets\\minecraft\\textures\\items\\compass_12.png",
                        "assets\\minecraft\\textures\\item\\compass_12.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_13.png",
                        "assets\\minecraft\\textures\\items\\compass_13.png",
                        "assets\\minecraft\\textures\\item\\compass_13.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_14.png",
                        "assets\\minecraft\\textures\\items\\compass_14.png",
                        "assets\\minecraft\\textures\\item\\compass_14.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_15.png",
                        "assets\\minecraft\\textures\\items\\compass_15.png",
                        "assets\\minecraft\\textures\\item\\compass_15.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_16.png",
                        "assets\\minecraft\\textures\\items\\compass_16.png",
                        "assets\\minecraft\\textures\\item\\compass_16.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_17.png",
                        "assets\\minecraft\\textures\\items\\compass_17.png",
                        "assets\\minecraft\\textures\\item\\compass_17.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_18.png",
                        "assets\\minecraft\\textures\\items\\compass_18.png",
                        "assets\\minecraft\\textures\\item\\compass_18.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_19.png",
                        "assets\\minecraft\\textures\\items\\compass_19.png",
                        "assets\\minecraft\\textures\\item\\compass_19.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_20.png",
                        "assets\\minecraft\\textures\\items\\compass_20.png",
                        "assets\\minecraft\\textures\\item\\compass_20.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_21.png",
                        "assets\\minecraft\\textures\\items\\compass_21.png",
                        "assets\\minecraft\\textures\\item\\compass_21.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_22.png",
                        "assets\\minecraft\\textures\\items\\compass_22.png",
                        "assets\\minecraft\\textures\\item\\compass_22.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_23.png",
                        "assets\\minecraft\\textures\\items\\compass_23.png",
                        "assets\\minecraft\\textures\\item\\compass_23.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_24.png",
                        "assets\\minecraft\\textures\\items\\compass_24.png",
                        "assets\\minecraft\\textures\\item\\compass_24.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_25.png",
                        "assets\\minecraft\\textures\\items\\compass_25.png",
                        "assets\\minecraft\\textures\\item\\compass_25.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_26.png",
                        "assets\\minecraft\\textures\\items\\compass_26.png",
                        "assets\\minecraft\\textures\\item\\compass_26.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_27.png",
                        "assets\\minecraft\\textures\\items\\compass_27.png",
                        "assets\\minecraft\\textures\\item\\compass_27.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_28.png",
                        "assets\\minecraft\\textures\\items\\compass_28.png",
                        "assets\\minecraft\\textures\\item\\compass_28.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_29.png",
                        "assets\\minecraft\\textures\\items\\compass_29.png",
                        "assets\\minecraft\\textures\\item\\compass_29.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_30.png",
                        "assets\\minecraft\\textures\\items\\compass_30.png",
                        "assets\\minecraft\\textures\\item\\compass_30.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\compass_31.png",
                        "assets\\minecraft\\textures\\items\\compass_31.png",
                        "assets\\minecraft\\textures\\item\\compass_31.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\beef_cooked.png",
                        "assets\\minecraft\\textures\\items\\beef_cooked.png",
                        "assets\\minecraft\\textures\\items\\beef_cooked.png",
                        "assets\\minecraft\\textures\\item\\cooked_beef.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\chicken_cooked.png",
                        "assets\\minecraft\\textures\\items\\chicken_cooked.png",
                        "assets\\minecraft\\textures\\items\\chicken_cooked.png",
                        "assets\\minecraft\\textures\\item\\cooked_chicken.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\fish_cod_cooked.png",
                        "assets\\minecraft\\textures\\items\\fish_cod_cooked.png",
                        "assets\\minecraft\\textures\\items\\fish_cod_cooked.png",
                        "assets\\minecraft\\textures\\item\\cooked_cod.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\mutton_cooked.png",
                        "assets\\minecraft\\textures\\items\\mutton_cooked.png",
                        "assets\\minecraft\\textures\\items\\mutton_cooked.png",
                        "assets\\minecraft\\textures\\item\\cooked_mutton.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\porkchop_cooked.png",
                        "assets\\minecraft\\textures\\items\\porkchop_cooked.png",
                        "assets\\minecraft\\textures\\items\\porkchop_cooked.png",
                        "assets\\minecraft\\textures\\item\\cooked_porkchop.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\rabbit_cooked.png",
                        "assets\\minecraft\\textures\\items\\rabbit_cooked.png",
                        "assets\\minecraft\\textures\\items\\rabbit_cooked.png",
                        "assets\\minecraft\\textures\\item\\cooked_rabbit.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\fish_salmon_cooked.png",
                        "assets\\minecraft\\textures\\items\\fish_salmon_cooked.png",
                        "assets\\minecraft\\textures\\items\\fish_salmon_cooked.png",
                        "assets\\minecraft\\textures\\item\\cooked_salmon.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\cookie.png",
                        "assets\\minecraft\\textures\\items\\cookie.png",
                        "assets\\minecraft\\textures\\items\\cookie.png",
                        "assets\\minecraft\\textures\\item\\cookie.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\dye_powder_cyan.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_cyan.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_cyan.png",
                        "assets\\minecraft\\textures\\item\\cyan_dye.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\dye_powder_yellow.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_yellow.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_yellow.png",
                        "assets\\minecraft\\textures\\item\\dandelion_yellow.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\dark_oak_boat.png",
                        "assets\\minecraft\\textures\\items\\dark_oak_boat.png",
                        "assets\\minecraft\\textures\\item\\dark_oak_boat.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\door_dark_oak.png",
                        "assets\\minecraft\\textures\\items\\door_dark_oak.png",
                        "assets\\minecraft\\textures\\items\\door_dark_oak.png",
                        "assets\\minecraft\\textures\\item\\dark_oak_door.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\diamond.png",
                        "assets\\minecraft\\textures\\items\\diamond.png",
                        "assets\\minecraft\\textures\\items\\diamond.png",
                        "assets\\minecraft\\textures\\item\\diamond.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\diamond_axe.png",
                        "assets\\minecraft\\textures\\items\\diamond_axe.png",
                        "assets\\minecraft\\textures\\items\\diamond_axe.png",
                        "assets\\minecraft\\textures\\item\\diamond_axe.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\diamond_boots.png",
                        "assets\\minecraft\\textures\\items\\diamond_boots.png",
                        "assets\\minecraft\\textures\\items\\diamond_boots.png",
                        "assets\\minecraft\\textures\\item\\diamond_boots.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\diamond_chestplate.png",
                        "assets\\minecraft\\textures\\items\\diamond_chestplate.png",
                        "assets\\minecraft\\textures\\items\\diamond_chestplate.png",
                        "assets\\minecraft\\textures\\item\\diamond_chestplate.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\diamond_helmet.png",
                        "assets\\minecraft\\textures\\items\\diamond_helmet.png",
                        "assets\\minecraft\\textures\\items\\diamond_helmet.png",
                        "assets\\minecraft\\textures\\item\\diamond_helmet.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\diamond_hoe.png",
                        "assets\\minecraft\\textures\\items\\diamond_hoe.png",
                        "assets\\minecraft\\textures\\items\\diamond_hoe.png",
                        "assets\\minecraft\\textures\\item\\diamond_hoe.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\diamond_horse_armor.png",
                        "assets\\minecraft\\textures\\items\\diamond_horse_armor.png",
                        "assets\\minecraft\\textures\\items\\diamond_horse_armor.png",
                        "assets\\minecraft\\textures\\item\\diamond_horse_armor.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\diamond_leggings.png",
                        "assets\\minecraft\\textures\\items\\diamond_leggings.png",
                        "assets\\minecraft\\textures\\items\\diamond_leggings.png",
                        "assets\\minecraft\\textures\\item\\diamond_leggings.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\diamond_pickaxe.png",
                        "assets\\minecraft\\textures\\items\\diamond_pickaxe.png",
                        "assets\\minecraft\\textures\\items\\diamond_pickaxe.png",
                        "assets\\minecraft\\textures\\item\\diamond_pickaxe.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\diamond_shovel.png",
                        "assets\\minecraft\\textures\\items\\diamond_shovel.png",
                        "assets\\minecraft\\textures\\items\\diamond_shovel.png",
                        "assets\\minecraft\\textures\\item\\diamond_shovel.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\diamond_sword.png",
                        "assets\\minecraft\\textures\\items\\diamond_sword.png",
                        "assets\\minecraft\\textures\\items\\diamond_sword.png",
                        "assets\\minecraft\\textures\\item\\diamond_sword.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\dragon_breath.png",
                        "assets\\minecraft\\textures\\items\\dragon_breath.png",
                        "assets\\minecraft\\textures\\item\\dragon_breath.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\item\\dried_kelp.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\egg.png",
                        "assets\\minecraft\\textures\\items\\egg.png",
                        "assets\\minecraft\\textures\\items\\egg.png",
                        "assets\\minecraft\\textures\\item\\egg.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\elytra.png",
                        "assets\\minecraft\\textures\\items\\elytra.png",
                        "assets\\minecraft\\textures\\item\\elytra.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\emerald.png",
                        "assets\\minecraft\\textures\\items\\emerald.png",
                        "assets\\minecraft\\textures\\items\\emerald.png",
                        "assets\\minecraft\\textures\\item\\emerald.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\empty_armor_slot_boots.png",
                        "assets\\minecraft\\textures\\items\\empty_armor_slot_boots.png",
                        "assets\\minecraft\\textures\\items\\empty_armor_slot_boots.png",
                        "assets\\minecraft\\textures\\item\\empty_armor_slot_boots.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\empty_armor_slot_chestplate.png",
                        "assets\\minecraft\\textures\\items\\empty_armor_slot_chestplate.png",
                        "assets\\minecraft\\textures\\items\\empty_armor_slot_chestplate.png",
                        "assets\\minecraft\\textures\\item\\empty_armor_slot_chestplate.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\empty_armor_slot_helmet.png",
                        "assets\\minecraft\\textures\\items\\empty_armor_slot_helmet.png",
                        "assets\\minecraft\\textures\\items\\empty_armor_slot_helmet.png",
                        "assets\\minecraft\\textures\\item\\empty_armor_slot_helmet.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\empty_armor_slot_leggings.png",
                        "assets\\minecraft\\textures\\items\\empty_armor_slot_leggings.png",
                        "assets\\minecraft\\textures\\items\\empty_armor_slot_leggings.png",
                        "assets\\minecraft\\textures\\item\\empty_armor_slot_leggings.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\empty_armor_slot_shield.png",
                        "assets\\minecraft\\textures\\items\\empty_armor_slot_shield.png",
                        "assets\\minecraft\\textures\\item\\empty_armor_slot_shield.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\book_enchanted.png",
                        "assets\\minecraft\\textures\\items\\book_enchanted.png",
                        "assets\\minecraft\\textures\\items\\book_enchanted.png",
                        "assets\\minecraft\\textures\\item\\enchanted_book.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\end_crystal.png",
                        "assets\\minecraft\\textures\\items\\end_crystal.png",
                        "assets\\minecraft\\textures\\item\\end_crystal.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\ender_eye.png",
                        "assets\\minecraft\\textures\\items\\ender_eye.png",
                        "assets\\minecraft\\textures\\items\\ender_eye.png",
                        "assets\\minecraft\\textures\\item\\ender_eye.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\ender_pearl.png",
                        "assets\\minecraft\\textures\\items\\ender_pearl.png",
                        "assets\\minecraft\\textures\\items\\ender_pearl.png",
                        "assets\\minecraft\\textures\\item\\ender_pearl.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\experience_bottle.png",
                        "assets\\minecraft\\textures\\items\\experience_bottle.png",
                        "assets\\minecraft\\textures\\items\\experience_bottle.png",
                        "assets\\minecraft\\textures\\item\\experience_bottle.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\feather.png",
                        "assets\\minecraft\\textures\\items\\feather.png",
                        "assets\\minecraft\\textures\\items\\feather.png",
                        "assets\\minecraft\\textures\\item\\feather.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\spider_eye_fermented.png",
                        "assets\\minecraft\\textures\\items\\spider_eye_fermented.png",
                        "assets\\minecraft\\textures\\items\\spider_eye_fermented.png",
                        "assets\\minecraft\\textures\\item\\fermented_spider_eye.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\item\\filled_map.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\item\\filled_map_markings.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\fireball.png",
                        "assets\\minecraft\\textures\\items\\fireball.png",
                        "assets\\minecraft\\textures\\items\\fireball.png",
                        "assets\\minecraft\\textures\\item\\fire_charge.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\fireworks.png",
                        "assets\\minecraft\\textures\\items\\fireworks.png",
                        "assets\\minecraft\\textures\\items\\fireworks.png",
                        "assets\\minecraft\\textures\\item\\firework_rocket.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\fireworks_charge.png",
                        "assets\\minecraft\\textures\\items\\fireworks_charge.png",
                        "assets\\minecraft\\textures\\items\\fireworks_charge.png",
                        "assets\\minecraft\\textures\\item\\firework_star.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\fireworks_charge_overlay.png",
                        "assets\\minecraft\\textures\\items\\fireworks_charge_overlay.png",
                        "assets\\minecraft\\textures\\items\\fireworks_charge_overlay.png",
                        "assets\\minecraft\\textures\\item\\firework_star_overlay.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\fishing_rod_uncast.png",
                        "assets\\minecraft\\textures\\items\\fishing_rod_uncast.png",
                        "assets\\minecraft\\textures\\items\\fishing_rod_uncast.png",
                        "assets\\minecraft\\textures\\item\\fishing_rod.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\fishing_rod_cast.png",
                        "assets\\minecraft\\textures\\items\\fishing_rod_cast.png",
                        "assets\\minecraft\\textures\\items\\fishing_rod_cast.png",
                        "assets\\minecraft\\textures\\item\\fishing_rod_cast.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\flint.png",
                        "assets\\minecraft\\textures\\items\\flint.png",
                        "assets\\minecraft\\textures\\items\\flint.png",
                        "assets\\minecraft\\textures\\item\\flint.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\flint_and_steel.png",
                        "assets\\minecraft\\textures\\items\\flint_and_steel.png",
                        "assets\\minecraft\\textures\\items\\flint_and_steel.png",
                        "assets\\minecraft\\textures\\item\\flint_and_steel.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\flower_pot.png",
                        "assets\\minecraft\\textures\\items\\flower_pot.png",
                        "assets\\minecraft\\textures\\items\\flower_pot.png",
                        "assets\\minecraft\\textures\\item\\flower_pot.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\minecart_furnace.png",
                        "assets\\minecraft\\textures\\items\\minecart_furnace.png",
                        "assets\\minecraft\\textures\\items\\minecart_furnace.png",
                        "assets\\minecraft\\textures\\item\\furnace_minecart.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\ghast_tear.png",
                        "assets\\minecraft\\textures\\items\\ghast_tear.png",
                        "assets\\minecraft\\textures\\items\\ghast_tear.png",
                        "assets\\minecraft\\textures\\item\\ghast_tear.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\potion_bottle_empty.png",
                        "assets\\minecraft\\textures\\items\\potion_bottle_empty.png",
                        "assets\\minecraft\\textures\\items\\potion_bottle_empty.png",
                        "assets\\minecraft\\textures\\item\\glass_bottle.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\melon_speckled.png",
                        "assets\\minecraft\\textures\\items\\melon_speckled.png",
                        "assets\\minecraft\\textures\\items\\melon_speckled.png",
                        "assets\\minecraft\\textures\\item\\glistering_melon_slice.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\glowstone_dust.png",
                        "assets\\minecraft\\textures\\items\\glowstone_dust.png",
                        "assets\\minecraft\\textures\\items\\glowstone_dust.png",
                        "assets\\minecraft\\textures\\item\\glowstone_dust.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\gold_ingot.png",
                        "assets\\minecraft\\textures\\items\\gold_ingot.png",
                        "assets\\minecraft\\textures\\items\\gold_ingot.png",
                        "assets\\minecraft\\textures\\item\\gold_ingot.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\gold_nugget.png",
                        "assets\\minecraft\\textures\\items\\gold_nugget.png",
                        "assets\\minecraft\\textures\\items\\gold_nugget.png",
                        "assets\\minecraft\\textures\\item\\gold_nugget.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\apple_golden.png",
                        "assets\\minecraft\\textures\\items\\apple_golden.png",
                        "assets\\minecraft\\textures\\items\\apple_golden.png",
                        "assets\\minecraft\\textures\\item\\golden_apple.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\gold_axe.png",
                        "assets\\minecraft\\textures\\items\\gold_axe.png",
                        "assets\\minecraft\\textures\\items\\gold_axe.png",
                        "assets\\minecraft\\textures\\item\\golden_axe.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\gold_boots.png",
                        "assets\\minecraft\\textures\\items\\gold_boots.png",
                        "assets\\minecraft\\textures\\items\\gold_boots.png",
                        "assets\\minecraft\\textures\\item\\golden_boots.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\carrot_golden.png",
                        "assets\\minecraft\\textures\\items\\carrot_golden.png",
                        "assets\\minecraft\\textures\\items\\carrot_golden.png",
                        "assets\\minecraft\\textures\\item\\golden_carrot.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\gold_chestplate.png",
                        "assets\\minecraft\\textures\\items\\gold_chestplate.png",
                        "assets\\minecraft\\textures\\items\\gold_chestplate.png",
                        "assets\\minecraft\\textures\\item\\golden_chestplate.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\gold_helmet.png",
                        "assets\\minecraft\\textures\\items\\gold_helmet.png",
                        "assets\\minecraft\\textures\\items\\gold_helmet.png",
                        "assets\\minecraft\\textures\\item\\golden_helmet.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\gold_hoe.png",
                        "assets\\minecraft\\textures\\items\\gold_hoe.png",
                        "assets\\minecraft\\textures\\items\\gold_hoe.png",
                        "assets\\minecraft\\textures\\item\\golden_hoe.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\gold_horse_armor.png",
                        "assets\\minecraft\\textures\\items\\gold_horse_armor.png",
                        "assets\\minecraft\\textures\\items\\gold_horse_armor.png",
                        "assets\\minecraft\\textures\\item\\golden_horse_armor.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\gold_leggings.png",
                        "assets\\minecraft\\textures\\items\\gold_leggings.png",
                        "assets\\minecraft\\textures\\items\\gold_leggings.png",
                        "assets\\minecraft\\textures\\item\\golden_leggings.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\gold_pickaxe.png",
                        "assets\\minecraft\\textures\\items\\gold_pickaxe.png",
                        "assets\\minecraft\\textures\\items\\gold_pickaxe.png",
                        "assets\\minecraft\\textures\\item\\golden_pickaxe.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\gold_shovel.png",
                        "assets\\minecraft\\textures\\items\\gold_shovel.png",
                        "assets\\minecraft\\textures\\items\\gold_shovel.png",
                        "assets\\minecraft\\textures\\item\\golden_shovel.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\gold_sword.png",
                        "assets\\minecraft\\textures\\items\\gold_sword.png",
                        "assets\\minecraft\\textures\\items\\gold_sword.png",
                        "assets\\minecraft\\textures\\item\\golden_sword.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\dye_powder_gray.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_gray.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_gray.png",
                        "assets\\minecraft\\textures\\item\\gray_dye.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\gunpowder.png",
                        "assets\\minecraft\\textures\\items\\gunpowder.png",
                        "assets\\minecraft\\textures\\items\\gunpowder.png",
                        "assets\\minecraft\\textures\\item\\gunpowder.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\item\\heart_of_the_sea.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\hopper.png",
                        "assets\\minecraft\\textures\\items\\hopper.png",
                        "assets\\minecraft\\textures\\items\\hopper.png",
                        "assets\\minecraft\\textures\\item\\hopper.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\minecart_hopper.png",
                        "assets\\minecraft\\textures\\items\\minecart_hopper.png",
                        "assets\\minecraft\\textures\\items\\minecart_hopper.png",
                        "assets\\minecraft\\textures\\item\\hopper_minecart.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\dye_powder_black.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_black.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_black.png",
                        "assets\\minecraft\\textures\\item\\ink_sac.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\iron_axe.png",
                        "assets\\minecraft\\textures\\items\\iron_axe.png",
                        "assets\\minecraft\\textures\\items\\iron_axe.png",
                        "assets\\minecraft\\textures\\item\\iron_axe.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\iron_boots.png",
                        "assets\\minecraft\\textures\\items\\iron_boots.png",
                        "assets\\minecraft\\textures\\items\\iron_boots.png",
                        "assets\\minecraft\\textures\\item\\iron_boots.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\iron_chestplate.png",
                        "assets\\minecraft\\textures\\items\\iron_chestplate.png",
                        "assets\\minecraft\\textures\\items\\iron_chestplate.png",
                        "assets\\minecraft\\textures\\item\\iron_chestplate.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\door_iron.png",
                        "assets\\minecraft\\textures\\items\\door_iron.png",
                        "assets\\minecraft\\textures\\items\\door_iron.png",
                        "assets\\minecraft\\textures\\item\\iron_door.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\iron_helmet.png",
                        "assets\\minecraft\\textures\\items\\iron_helmet.png",
                        "assets\\minecraft\\textures\\items\\iron_helmet.png",
                        "assets\\minecraft\\textures\\item\\iron_helmet.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\iron_hoe.png",
                        "assets\\minecraft\\textures\\items\\iron_hoe.png",
                        "assets\\minecraft\\textures\\items\\iron_hoe.png",
                        "assets\\minecraft\\textures\\item\\iron_hoe.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\iron_horse_armor.png",
                        "assets\\minecraft\\textures\\items\\iron_horse_armor.png",
                        "assets\\minecraft\\textures\\items\\iron_horse_armor.png",
                        "assets\\minecraft\\textures\\item\\iron_horse_armor.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\iron_ingot.png",
                        "assets\\minecraft\\textures\\items\\iron_ingot.png",
                        "assets\\minecraft\\textures\\items\\iron_ingot.png",
                        "assets\\minecraft\\textures\\item\\iron_ingot.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\iron_leggings.png",
                        "assets\\minecraft\\textures\\items\\iron_leggings.png",
                        "assets\\minecraft\\textures\\items\\iron_leggings.png",
                        "assets\\minecraft\\textures\\item\\iron_leggings.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\items\\iron_nugget.png",
                        "assets\\minecraft\\textures\\item\\iron_nugget.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\iron_pickaxe.png",
                        "assets\\minecraft\\textures\\items\\iron_pickaxe.png",
                        "assets\\minecraft\\textures\\items\\iron_pickaxe.png",
                        "assets\\minecraft\\textures\\item\\iron_pickaxe.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\iron_shovel.png",
                        "assets\\minecraft\\textures\\items\\iron_shovel.png",
                        "assets\\minecraft\\textures\\items\\iron_shovel.png",
                        "assets\\minecraft\\textures\\item\\iron_shovel.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\iron_sword.png",
                        "assets\\minecraft\\textures\\items\\iron_sword.png",
                        "assets\\minecraft\\textures\\items\\iron_sword.png",
                        "assets\\minecraft\\textures\\item\\iron_sword.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\item_frame.png",
                        "assets\\minecraft\\textures\\items\\item_frame.png",
                        "assets\\minecraft\\textures\\items\\item_frame.png",
                        "assets\\minecraft\\textures\\item\\item_frame.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\jungle_boat.png",
                        "assets\\minecraft\\textures\\items\\jungle_boat.png",
                        "assets\\minecraft\\textures\\item\\jungle_boat.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\door_jungle.png",
                        "assets\\minecraft\\textures\\items\\door_jungle.png",
                        "assets\\minecraft\\textures\\items\\door_jungle.png",
                        "assets\\minecraft\\textures\\item\\jungle_door.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\item\\kelp.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\items\\knowledge_book.png",
                        "assets\\minecraft\\textures\\item\\knowledge_book.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\dye_powder_blue.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_blue.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_blue.png",
                        "assets\\minecraft\\textures\\item\\lapis_lazuli.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\bucket_lava.png",
                        "assets\\minecraft\\textures\\items\\bucket_lava.png",
                        "assets\\minecraft\\textures\\items\\bucket_lava.png",
                        "assets\\minecraft\\textures\\item\\lava_bucket.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\lead.png",
                        "assets\\minecraft\\textures\\items\\lead.png",
                        "assets\\minecraft\\textures\\items\\lead.png",
                        "assets\\minecraft\\textures\\item\\lead.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\leather.png",
                        "assets\\minecraft\\textures\\items\\leather.png",
                        "assets\\minecraft\\textures\\items\\leather.png",
                        "assets\\minecraft\\textures\\item\\leather.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\leather_boots.png",
                        "assets\\minecraft\\textures\\items\\leather_boots.png",
                        "assets\\minecraft\\textures\\items\\leather_boots.png",
                        "assets\\minecraft\\textures\\item\\leather_boots.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\leather_boots_overlay.png",
                        "assets\\minecraft\\textures\\items\\leather_boots_overlay.png",
                        "assets\\minecraft\\textures\\items\\leather_boots_overlay.png",
                        "assets\\minecraft\\textures\\item\\leather_boots_overlay.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\leather_chestplate.png",
                        "assets\\minecraft\\textures\\items\\leather_chestplate.png",
                        "assets\\minecraft\\textures\\items\\leather_chestplate.png",
                        "assets\\minecraft\\textures\\item\\leather_chestplate.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\leather_chestplate_overlay.png",
                        "assets\\minecraft\\textures\\items\\leather_chestplate_overlay.png",
                        "assets\\minecraft\\textures\\items\\leather_chestplate_overlay.png",
                        "assets\\minecraft\\textures\\item\\leather_chestplate_overlay.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\leather_helmet.png",
                        "assets\\minecraft\\textures\\items\\leather_helmet.png",
                        "assets\\minecraft\\textures\\items\\leather_helmet.png",
                        "assets\\minecraft\\textures\\item\\leather_helmet.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\leather_helmet_overlay.png",
                        "assets\\minecraft\\textures\\items\\leather_helmet_overlay.png",
                        "assets\\minecraft\\textures\\items\\leather_helmet_overlay.png",
                        "assets\\minecraft\\textures\\item\\leather_helmet_overlay.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\leather_leggings.png",
                        "assets\\minecraft\\textures\\items\\leather_leggings.png",
                        "assets\\minecraft\\textures\\items\\leather_leggings.png",
                        "assets\\minecraft\\textures\\item\\leather_leggings.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\leather_leggings_overlay.png",
                        "assets\\minecraft\\textures\\items\\leather_leggings_overlay.png",
                        "assets\\minecraft\\textures\\items\\leather_leggings_overlay.png",
                        "assets\\minecraft\\textures\\item\\leather_leggings_overlay.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\dye_powder_light_blue.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_light_blue.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_light_blue.png",
                        "assets\\minecraft\\textures\\item\\light_blue_dye.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\dye_powder_silver.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_silver.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_silver.png",
                        "assets\\minecraft\\textures\\item\\light_gray_dye.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\dye_powder_lime.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_lime.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_lime.png",
                        "assets\\minecraft\\textures\\item\\lime_dye.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\potion_bottle_lingering.png",
                        "assets\\minecraft\\textures\\items\\potion_bottle_lingering.png",
                        "assets\\minecraft\\textures\\item\\lingering_potion.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\dye_powder_magenta.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_magenta.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_magenta.png",
                        "assets\\minecraft\\textures\\item\\magenta_dye.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\magma_cream.png",
                        "assets\\minecraft\\textures\\items\\magma_cream.png",
                        "assets\\minecraft\\textures\\items\\magma_cream.png",
                        "assets\\minecraft\\textures\\item\\magma_cream.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\map_empty.png",
                        "assets\\minecraft\\textures\\items\\map_empty.png",
                        "assets\\minecraft\\textures\\items\\map_empty.png",
                        "assets\\minecraft\\textures\\item\\map.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\map_filled.png",
                        "assets\\minecraft\\textures\\items\\map_filled.png",
                        "assets\\minecraft\\textures\\items\\map_filled.png",
                        ""
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\items\\map_filled_markings.png",
                        ""
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\seeds_melon.png",
                        "assets\\minecraft\\textures\\items\\seeds_melon.png",
                        "assets\\minecraft\\textures\\items\\seeds_melon.png",
                        "assets\\minecraft\\textures\\item\\melon_seeds.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\melon.png",
                        "assets\\minecraft\\textures\\items\\melon.png",
                        "assets\\minecraft\\textures\\items\\melon.png",
                        "assets\\minecraft\\textures\\item\\melon_slice.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\bucket_milk.png",
                        "assets\\minecraft\\textures\\items\\bucket_milk.png",
                        "assets\\minecraft\\textures\\items\\bucket_milk.png",
                        "assets\\minecraft\\textures\\item\\milk_bucket.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\minecart_normal.png",
                        "assets\\minecraft\\textures\\items\\minecart_normal.png",
                        "assets\\minecraft\\textures\\items\\minecart_normal.png",
                        "assets\\minecraft\\textures\\item\\minecart.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\mushroom_stew.png",
                        "assets\\minecraft\\textures\\items\\mushroom_stew.png",
                        "assets\\minecraft\\textures\\items\\mushroom_stew.png",
                        "assets\\minecraft\\textures\\item\\mushroom_stew.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\record_11.png",
                        "assets\\minecraft\\textures\\items\\record_11.png",
                        "assets\\minecraft\\textures\\items\\record_11.png",
                        "assets\\minecraft\\textures\\item\\music_disc_11.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\record_13.png",
                        "assets\\minecraft\\textures\\items\\record_13.png",
                        "assets\\minecraft\\textures\\items\\record_13.png",
                        "assets\\minecraft\\textures\\item\\music_disc_13.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\record_blocks.png",
                        "assets\\minecraft\\textures\\items\\record_blocks.png",
                        "assets\\minecraft\\textures\\items\\record_blocks.png",
                        "assets\\minecraft\\textures\\item\\music_disc_blocks.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\record_cat.png",
                        "assets\\minecraft\\textures\\items\\record_cat.png",
                        "assets\\minecraft\\textures\\items\\record_cat.png",
                        "assets\\minecraft\\textures\\item\\music_disc_cat.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\record_chirp.png",
                        "assets\\minecraft\\textures\\items\\record_chirp.png",
                        "assets\\minecraft\\textures\\items\\record_chirp.png",
                        "assets\\minecraft\\textures\\item\\music_disc_chirp.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\record_far.png",
                        "assets\\minecraft\\textures\\items\\record_far.png",
                        "assets\\minecraft\\textures\\items\\record_far.png",
                        "assets\\minecraft\\textures\\item\\music_disc_far.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\record_mall.png",
                        "assets\\minecraft\\textures\\items\\record_mall.png",
                        "assets\\minecraft\\textures\\items\\record_mall.png",
                        "assets\\minecraft\\textures\\item\\music_disc_mall.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\record_mellohi.png",
                        "assets\\minecraft\\textures\\items\\record_mellohi.png",
                        "assets\\minecraft\\textures\\items\\record_mellohi.png",
                        "assets\\minecraft\\textures\\item\\music_disc_mellohi.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\record_stal.png",
                        "assets\\minecraft\\textures\\items\\record_stal.png",
                        "assets\\minecraft\\textures\\items\\record_stal.png",
                        "assets\\minecraft\\textures\\item\\music_disc_stal.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\record_strad.png",
                        "assets\\minecraft\\textures\\items\\record_strad.png",
                        "assets\\minecraft\\textures\\items\\record_strad.png",
                        "assets\\minecraft\\textures\\item\\music_disc_strad.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\record_wait.png",
                        "assets\\minecraft\\textures\\items\\record_wait.png",
                        "assets\\minecraft\\textures\\items\\record_wait.png",
                        "assets\\minecraft\\textures\\item\\music_disc_wait.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\record_ward.png",
                        "assets\\minecraft\\textures\\items\\record_ward.png",
                        "assets\\minecraft\\textures\\items\\record_ward.png",
                        "assets\\minecraft\\textures\\item\\music_disc_ward.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\mutton_raw.png",
                        "assets\\minecraft\\textures\\items\\mutton_raw.png",
                        "assets\\minecraft\\textures\\items\\mutton_raw.png",
                        "assets\\minecraft\\textures\\item\\mutton.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\name_tag.png",
                        "assets\\minecraft\\textures\\items\\name_tag.png",
                        "assets\\minecraft\\textures\\items\\name_tag.png",
                        "assets\\minecraft\\textures\\item\\name_tag.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\item\\nautilus_shell.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\netherbrick.png",
                        "assets\\minecraft\\textures\\items\\netherbrick.png",
                        "assets\\minecraft\\textures\\items\\netherbrick.png",
                        "assets\\minecraft\\textures\\item\\nether_brick.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\nether_star.png",
                        "assets\\minecraft\\textures\\items\\nether_star.png",
                        "assets\\minecraft\\textures\\items\\nether_star.png",
                        "assets\\minecraft\\textures\\item\\nether_star.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\nether_wart.png",
                        "assets\\minecraft\\textures\\items\\nether_wart.png",
                        "assets\\minecraft\\textures\\items\\nether_wart.png",
                        "assets\\minecraft\\textures\\item\\nether_wart.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\oak_boat.png",
                        "assets\\minecraft\\textures\\items\\oak_boat.png",
                        "assets\\minecraft\\textures\\item\\oak_boat.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\door_wood.png",
                        "assets\\minecraft\\textures\\items\\door_wood.png",
                        "assets\\minecraft\\textures\\items\\door_wood.png",
                        "assets\\minecraft\\textures\\item\\oak_door.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\dye_powder_orange.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_orange.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_orange.png",
                        "assets\\minecraft\\textures\\item\\orange_dye.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\painting.png",
                        "assets\\minecraft\\textures\\items\\painting.png",
                        "assets\\minecraft\\textures\\items\\painting.png",
                        "assets\\minecraft\\textures\\item\\painting.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\paper.png",
                        "assets\\minecraft\\textures\\items\\paper.png",
                        "assets\\minecraft\\textures\\items\\paper.png",
                        "assets\\minecraft\\textures\\item\\paper.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\item\\phantom_membrane.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\dye_powder_pink.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_pink.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_pink.png",
                        "assets\\minecraft\\textures\\item\\pink_dye.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\potato_poisonous.png",
                        "assets\\minecraft\\textures\\items\\potato_poisonous.png",
                        "assets\\minecraft\\textures\\items\\potato_poisonous.png",
                        "assets\\minecraft\\textures\\item\\poisonous_potato.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\chorus_fruit_popped.png",
                        "assets\\minecraft\\textures\\items\\chorus_fruit_popped.png",
                        "assets\\minecraft\\textures\\item\\popped_chorus_fruit.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\porkchop_raw.png",
                        "assets\\minecraft\\textures\\items\\porkchop_raw.png",
                        "assets\\minecraft\\textures\\items\\porkchop_raw.png",
                        "assets\\minecraft\\textures\\item\\porkchop.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\potato.png",
                        "assets\\minecraft\\textures\\items\\potato.png",
                        "assets\\minecraft\\textures\\items\\potato.png",
                        "assets\\minecraft\\textures\\item\\potato.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\potion_bottle_drinkable.png",
                        "assets\\minecraft\\textures\\items\\potion_bottle_drinkable.png",
                        "assets\\minecraft\\textures\\items\\potion_bottle_drinkable.png",
                        "assets\\minecraft\\textures\\item\\potion.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\potion_overlay.png",
                        "assets\\minecraft\\textures\\items\\potion_overlay.png",
                        "assets\\minecraft\\textures\\items\\potion_overlay.png",
                        "assets\\minecraft\\textures\\item\\potion_overlay.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\prismarine_crystals.png",
                        "assets\\minecraft\\textures\\items\\prismarine_crystals.png",
                        "assets\\minecraft\\textures\\items\\prismarine_crystals.png",
                        "assets\\minecraft\\textures\\item\\prismarine_crystals.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\prismarine_shard.png",
                        "assets\\minecraft\\textures\\items\\prismarine_shard.png",
                        "assets\\minecraft\\textures\\items\\prismarine_shard.png",
                        "assets\\minecraft\\textures\\item\\prismarine_shard.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\fish_pufferfish_raw.png",
                        "assets\\minecraft\\textures\\items\\fish_pufferfish_raw.png",
                        "assets\\minecraft\\textures\\items\\fish_pufferfish_raw.png",
                        "assets\\minecraft\\textures\\item\\pufferfish.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\item\\pufferfish_bucket.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\pumpkin_pie.png",
                        "assets\\minecraft\\textures\\items\\pumpkin_pie.png",
                        "assets\\minecraft\\textures\\items\\pumpkin_pie.png",
                        "assets\\minecraft\\textures\\item\\pumpkin_pie.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\seeds_pumpkin.png",
                        "assets\\minecraft\\textures\\items\\seeds_pumpkin.png",
                        "assets\\minecraft\\textures\\items\\seeds_pumpkin.png",
                        "assets\\minecraft\\textures\\item\\pumpkin_seeds.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\dye_powder_purple.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_purple.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_purple.png",
                        "assets\\minecraft\\textures\\item\\purple_dye.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\quartz.png",
                        "assets\\minecraft\\textures\\items\\quartz.png",
                        "assets\\minecraft\\textures\\items\\quartz.png",
                        "assets\\minecraft\\textures\\item\\quartz.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\quiver.png",
                        "",
                        "",
                        ""
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\rabbit_raw.png",
                        "assets\\minecraft\\textures\\items\\rabbit_raw.png",
                        "assets\\minecraft\\textures\\items\\rabbit_raw.png",
                        "assets\\minecraft\\textures\\item\\rabbit.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\rabbit_foot.png",
                        "assets\\minecraft\\textures\\items\\rabbit_foot.png",
                        "assets\\minecraft\\textures\\items\\rabbit_foot.png",
                        "assets\\minecraft\\textures\\item\\rabbit_foot.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\rabbit_hide.png",
                        "assets\\minecraft\\textures\\items\\rabbit_hide.png",
                        "assets\\minecraft\\textures\\items\\rabbit_hide.png",
                        "assets\\minecraft\\textures\\item\\rabbit_hide.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\rabbit_stew.png",
                        "assets\\minecraft\\textures\\items\\rabbit_stew.png",
                        "assets\\minecraft\\textures\\items\\rabbit_stew.png",
                        "assets\\minecraft\\textures\\item\\rabbit_stew.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\redstone_dust.png",
                        "assets\\minecraft\\textures\\items\\redstone_dust.png",
                        "assets\\minecraft\\textures\\items\\redstone_dust.png",
                        "assets\\minecraft\\textures\\item\\redstone.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\repeater.png",
                        "assets\\minecraft\\textures\\items\\repeater.png",
                        "assets\\minecraft\\textures\\items\\repeater.png",
                        "assets\\minecraft\\textures\\item\\repeater.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\dye_powder_red.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_red.png",
                        "assets\\minecraft\\textures\\items\\dye_powder_red.png",
                        "assets\\minecraft\\textures\\item\\rose_red.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\rotten_flesh.png",
                        "assets\\minecraft\\textures\\items\\rotten_flesh.png",
                        "assets\\minecraft\\textures\\items\\rotten_flesh.png",
                        "assets\\minecraft\\textures\\item\\rotten_flesh.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\ruby.png",
                        "assets\\minecraft\\textures\\items\\ruby.png",
                        "assets\\minecraft\\textures\\items\\ruby.png",
                        "assets\\minecraft\\textures\\item\\ruby.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\saddle.png",
                        "assets\\minecraft\\textures\\items\\saddle.png",
                        "assets\\minecraft\\textures\\items\\saddle.png",
                        "assets\\minecraft\\textures\\item\\saddle.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\fish_salmon_raw.png",
                        "assets\\minecraft\\textures\\items\\fish_salmon_raw.png",
                        "assets\\minecraft\\textures\\items\\fish_salmon_raw.png",
                        "assets\\minecraft\\textures\\item\\salmon.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\item\\salmon_bucket.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\item\\scute.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\item\\sea_pickle.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\item\\seagrass.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\shears.png",
                        "assets\\minecraft\\textures\\items\\shears.png",
                        "assets\\minecraft\\textures\\items\\shears.png",
                        "assets\\minecraft\\textures\\item\\shears.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\items\\shulker_shell.png",
                        "assets\\minecraft\\textures\\item\\shulker_shell.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\sign.png",
                        "assets\\minecraft\\textures\\items\\sign.png",
                        "assets\\minecraft\\textures\\items\\sign.png",
                        "assets\\minecraft\\textures\\item\\sign.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\slimeball.png",
                        "assets\\minecraft\\textures\\items\\slimeball.png",
                        "assets\\minecraft\\textures\\items\\slimeball.png",
                        "assets\\minecraft\\textures\\item\\slime_ball.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\snowball.png",
                        "assets\\minecraft\\textures\\items\\snowball.png",
                        "assets\\minecraft\\textures\\items\\snowball.png",
                        "assets\\minecraft\\textures\\item\\snowball.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\spawn_egg.png",
                        "assets\\minecraft\\textures\\items\\spawn_egg.png",
                        "assets\\minecraft\\textures\\items\\spawn_egg.png",
                        "assets\\minecraft\\textures\\item\\spawn_egg.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\spawn_egg_overlay.png",
                        "assets\\minecraft\\textures\\items\\spawn_egg_overlay.png",
                        "assets\\minecraft\\textures\\items\\spawn_egg_overlay.png",
                        "assets\\minecraft\\textures\\item\\spawn_egg_overlay.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\spectral_arrow.png",
                        "assets\\minecraft\\textures\\items\\spectral_arrow.png",
                        "assets\\minecraft\\textures\\item\\spectral_arrow.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\spider_eye.png",
                        "assets\\minecraft\\textures\\items\\spider_eye.png",
                        "assets\\minecraft\\textures\\items\\spider_eye.png",
                        "assets\\minecraft\\textures\\item\\spider_eye.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\potion_bottle_splash.png",
                        "assets\\minecraft\\textures\\items\\potion_bottle_splash.png",
                        "assets\\minecraft\\textures\\items\\potion_bottle_splash.png",
                        "assets\\minecraft\\textures\\item\\splash_potion.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\spruce_boat.png",
                        "assets\\minecraft\\textures\\items\\spruce_boat.png",
                        "assets\\minecraft\\textures\\item\\spruce_boat.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\door_spruce.png",
                        "assets\\minecraft\\textures\\items\\door_spruce.png",
                        "assets\\minecraft\\textures\\items\\door_spruce.png",
                        "assets\\minecraft\\textures\\item\\spruce_door.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\stick.png",
                        "assets\\minecraft\\textures\\items\\stick.png",
                        "assets\\minecraft\\textures\\items\\stick.png",
                        "assets\\minecraft\\textures\\item\\stick.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\stone_axe.png",
                        "assets\\minecraft\\textures\\items\\stone_axe.png",
                        "assets\\minecraft\\textures\\items\\stone_axe.png",
                        "assets\\minecraft\\textures\\item\\stone_axe.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\stone_hoe.png",
                        "assets\\minecraft\\textures\\items\\stone_hoe.png",
                        "assets\\minecraft\\textures\\items\\stone_hoe.png",
                        "assets\\minecraft\\textures\\item\\stone_hoe.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\stone_pickaxe.png",
                        "assets\\minecraft\\textures\\items\\stone_pickaxe.png",
                        "assets\\minecraft\\textures\\items\\stone_pickaxe.png",
                        "assets\\minecraft\\textures\\item\\stone_pickaxe.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\stone_shovel.png",
                        "assets\\minecraft\\textures\\items\\stone_shovel.png",
                        "assets\\minecraft\\textures\\items\\stone_shovel.png",
                        "assets\\minecraft\\textures\\item\\stone_shovel.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\stone_sword.png",
                        "assets\\minecraft\\textures\\items\\stone_sword.png",
                        "assets\\minecraft\\textures\\items\\stone_sword.png",
                        "assets\\minecraft\\textures\\item\\stone_sword.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\string.png",
                        "assets\\minecraft\\textures\\items\\string.png",
                        "assets\\minecraft\\textures\\items\\string.png",
                        "assets\\minecraft\\textures\\item\\string.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\structure_void.png",
                        "assets\\minecraft\\textures\\items\\structure_void.png",
                        "assets\\minecraft\\textures\\item\\structure_void.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\sugar.png",
                        "assets\\minecraft\\textures\\items\\sugar.png",
                        "assets\\minecraft\\textures\\items\\sugar.png",
                        "assets\\minecraft\\textures\\item\\sugar.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\reeds.png",
                        "assets\\minecraft\\textures\\items\\reeds.png",
                        "assets\\minecraft\\textures\\items\\reeds.png",
                        "assets\\minecraft\\textures\\item\\sugar_cane.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\tipped_arrow_base.png",
                        "assets\\minecraft\\textures\\items\\tipped_arrow_base.png",
                        "assets\\minecraft\\textures\\item\\tipped_arrow_base.png"
                    ),
                    new Archivos
                    (
                        "",
                        "assets\\minecraft\\textures\\items\\tipped_arrow_head.png",
                        "assets\\minecraft\\textures\\items\\tipped_arrow_head.png",
                        "assets\\minecraft\\textures\\item\\tipped_arrow_head.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\minecart_tnt.png",
                        "assets\\minecraft\\textures\\items\\minecart_tnt.png",
                        "assets\\minecraft\\textures\\items\\minecart_tnt.png",
                        "assets\\minecraft\\textures\\item\\tnt_minecart.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "assets\\minecraft\\textures\\items\\totem.png",
                        "assets\\minecraft\\textures\\item\\totem_of_undying.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\item\\trident.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\fish_clownfish_raw.png",
                        "assets\\minecraft\\textures\\items\\fish_clownfish_raw.png",
                        "assets\\minecraft\\textures\\items\\fish_clownfish_raw.png",
                        "assets\\minecraft\\textures\\item\\tropical_fish.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\item\\tropical_fish_bucket.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\item\\turtle_egg.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\item\\turtle_helmet.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\bucket_water.png",
                        "assets\\minecraft\\textures\\items\\bucket_water.png",
                        "assets\\minecraft\\textures\\items\\bucket_water.png",
                        "assets\\minecraft\\textures\\item\\water_bucket.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\wheat.png",
                        "assets\\minecraft\\textures\\items\\wheat.png",
                        "assets\\minecraft\\textures\\items\\wheat.png",
                        "assets\\minecraft\\textures\\item\\wheat.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\seeds_wheat.png",
                        "assets\\minecraft\\textures\\items\\seeds_wheat.png",
                        "assets\\minecraft\\textures\\items\\seeds_wheat.png",
                        "assets\\minecraft\\textures\\item\\wheat_seeds.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\wood_axe.png",
                        "assets\\minecraft\\textures\\items\\wood_axe.png",
                        "assets\\minecraft\\textures\\items\\wood_axe.png",
                        "assets\\minecraft\\textures\\item\\wooden_axe.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\wood_hoe.png",
                        "assets\\minecraft\\textures\\items\\wood_hoe.png",
                        "assets\\minecraft\\textures\\items\\wood_hoe.png",
                        "assets\\minecraft\\textures\\item\\wooden_hoe.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\wood_pickaxe.png",
                        "assets\\minecraft\\textures\\items\\wood_pickaxe.png",
                        "assets\\minecraft\\textures\\items\\wood_pickaxe.png",
                        "assets\\minecraft\\textures\\item\\wooden_pickaxe.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\wood_shovel.png",
                        "assets\\minecraft\\textures\\items\\wood_shovel.png",
                        "assets\\minecraft\\textures\\items\\wood_shovel.png",
                        "assets\\minecraft\\textures\\item\\wooden_shovel.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\wood_sword.png",
                        "assets\\minecraft\\textures\\items\\wood_sword.png",
                        "assets\\minecraft\\textures\\items\\wood_sword.png",
                        "assets\\minecraft\\textures\\item\\wooden_sword.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\book_writable.png",
                        "assets\\minecraft\\textures\\items\\book_writable.png",
                        "assets\\minecraft\\textures\\items\\book_writable.png",
                        "assets\\minecraft\\textures\\item\\writable_book.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\items\\book_written.png",
                        "assets\\minecraft\\textures\\items\\book_written.png",
                        "assets\\minecraft\\textures\\items\\book_written.png",
                        "assets\\minecraft\\textures\\item\\written_book.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\map",
                "assets\\minecraft\\textures\\map",
                "assets\\minecraft\\textures\\map",
                "assets\\minecraft\\textures\\map",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\map\\map_background.png",
                        "assets\\minecraft\\textures\\map\\map_background.png",
                        "assets\\minecraft\\textures\\map\\map_background.png",
                        "assets\\minecraft\\textures\\map\\map_background.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\map\\map_icons.png",
                        "assets\\minecraft\\textures\\map\\map_icons.png",
                        "assets\\minecraft\\textures\\map\\map_icons.png",
                        "assets\\minecraft\\textures\\map\\map_icons.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\misc",
                "assets\\minecraft\\textures\\misc",
                "assets\\minecraft\\textures\\misc",
                "assets\\minecraft\\textures\\misc",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\misc\\enchanted_item_glint.png",
                        "assets\\minecraft\\textures\\misc\\enchanted_item_glint.png",
                        "assets\\minecraft\\textures\\misc\\enchanted_item_glint.png",
                        "assets\\minecraft\\textures\\misc\\enchanted_item_glint.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\misc\\enchanted_item_glint.png.mcmeta",
                        "assets\\minecraft\\textures\\misc\\enchanted_item_glint.png.mcmeta",
                        "assets\\minecraft\\textures\\misc\\enchanted_item_glint.png.mcmeta",
                        "assets\\minecraft\\textures\\misc\\enchanted_item_glint.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\misc\\forcefield.png",
                        "assets\\minecraft\\textures\\misc\\forcefield.png",
                        "assets\\minecraft\\textures\\misc\\forcefield.png",
                        "assets\\minecraft\\textures\\misc\\forcefield.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\misc\\pumpkinblur.png",
                        "assets\\minecraft\\textures\\misc\\pumpkinblur.png",
                        "assets\\minecraft\\textures\\misc\\pumpkinblur.png",
                        "assets\\minecraft\\textures\\misc\\pumpkinblur.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\misc\\pumpkinblur.png.mcmeta",
                        "assets\\minecraft\\textures\\misc\\pumpkinblur.png.mcmeta",
                        "assets\\minecraft\\textures\\misc\\pumpkinblur.png.mcmeta",
                        "assets\\minecraft\\textures\\misc\\pumpkinblur.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\misc\\shadow.png",
                        "assets\\minecraft\\textures\\misc\\shadow.png",
                        "assets\\minecraft\\textures\\misc\\shadow.png",
                        "assets\\minecraft\\textures\\misc\\shadow.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\misc\\shadow.png.mcmeta",
                        "assets\\minecraft\\textures\\misc\\shadow.png.mcmeta",
                        "assets\\minecraft\\textures\\misc\\shadow.png.mcmeta",
                        "assets\\minecraft\\textures\\misc\\shadow.png.mcmeta"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\misc\\underwater.png",
                        "assets\\minecraft\\textures\\misc\\underwater.png",
                        "assets\\minecraft\\textures\\misc\\underwater.png",
                        "assets\\minecraft\\textures\\misc\\underwater.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\misc\\unknown_pack.png",
                        "assets\\minecraft\\textures\\misc\\unknown_pack.png",
                        "assets\\minecraft\\textures\\misc\\unknown_pack.png",
                        "assets\\minecraft\\textures\\misc\\unknown_pack.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\misc\\unknown_server.png",
                        "assets\\minecraft\\textures\\misc\\unknown_server.png",
                        "assets\\minecraft\\textures\\misc\\unknown_server.png",
                        "assets\\minecraft\\textures\\misc\\unknown_server.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\misc\\vignette.png",
                        "assets\\minecraft\\textures\\misc\\vignette.png",
                        "assets\\minecraft\\textures\\misc\\vignette.png",
                        "assets\\minecraft\\textures\\misc\\vignette.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\misc\\vignette.png.mcmeta",
                        "assets\\minecraft\\textures\\misc\\vignette.png.mcmeta",
                        "assets\\minecraft\\textures\\misc\\vignette.png.mcmeta",
                        "assets\\minecraft\\textures\\misc\\vignette.png.mcmeta"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\models",
                "assets\\minecraft\\textures\\models",
                "assets\\minecraft\\textures\\models",
                "assets\\minecraft\\textures\\models",
                new Archivos[]
                {

                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\models\\armor",
                "assets\\minecraft\\textures\\models\\armor",
                "assets\\minecraft\\textures\\models\\armor",
                "assets\\minecraft\\textures\\models\\armor",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\models\\armor\\chainmail_layer_1.png",
                        "assets\\minecraft\\textures\\models\\armor\\chainmail_layer_1.png",
                        "assets\\minecraft\\textures\\models\\armor\\chainmail_layer_1.png",
                        "assets\\minecraft\\textures\\models\\armor\\chainmail_layer_1.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\models\\armor\\chainmail_layer_2.png",
                        "assets\\minecraft\\textures\\models\\armor\\chainmail_layer_2.png",
                        "assets\\minecraft\\textures\\models\\armor\\chainmail_layer_2.png",
                        "assets\\minecraft\\textures\\models\\armor\\chainmail_layer_2.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\models\\armor\\diamond_layer_1.png",
                        "assets\\minecraft\\textures\\models\\armor\\diamond_layer_1.png",
                        "assets\\minecraft\\textures\\models\\armor\\diamond_layer_1.png",
                        "assets\\minecraft\\textures\\models\\armor\\diamond_layer_1.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\models\\armor\\diamond_layer_2.png",
                        "assets\\minecraft\\textures\\models\\armor\\diamond_layer_2.png",
                        "assets\\minecraft\\textures\\models\\armor\\diamond_layer_2.png",
                        "assets\\minecraft\\textures\\models\\armor\\diamond_layer_2.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\models\\armor\\gold_layer_1.png",
                        "assets\\minecraft\\textures\\models\\armor\\gold_layer_1.png",
                        "assets\\minecraft\\textures\\models\\armor\\gold_layer_1.png",
                        "assets\\minecraft\\textures\\models\\armor\\gold_layer_1.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\models\\armor\\gold_layer_2.png",
                        "assets\\minecraft\\textures\\models\\armor\\gold_layer_2.png",
                        "assets\\minecraft\\textures\\models\\armor\\gold_layer_2.png",
                        "assets\\minecraft\\textures\\models\\armor\\gold_layer_2.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\models\\armor\\iron_layer_1.png",
                        "assets\\minecraft\\textures\\models\\armor\\iron_layer_1.png",
                        "assets\\minecraft\\textures\\models\\armor\\iron_layer_1.png",
                        "assets\\minecraft\\textures\\models\\armor\\iron_layer_1.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\models\\armor\\iron_layer_2.png",
                        "assets\\minecraft\\textures\\models\\armor\\iron_layer_2.png",
                        "assets\\minecraft\\textures\\models\\armor\\iron_layer_2.png",
                        "assets\\minecraft\\textures\\models\\armor\\iron_layer_2.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\models\\armor\\leather_layer_1.png",
                        "assets\\minecraft\\textures\\models\\armor\\leather_layer_1.png",
                        "assets\\minecraft\\textures\\models\\armor\\leather_layer_1.png",
                        "assets\\minecraft\\textures\\models\\armor\\leather_layer_1.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\models\\armor\\leather_layer_1_overlay.png",
                        "assets\\minecraft\\textures\\models\\armor\\leather_layer_1_overlay.png",
                        "assets\\minecraft\\textures\\models\\armor\\leather_layer_1_overlay.png",
                        "assets\\minecraft\\textures\\models\\armor\\leather_layer_1_overlay.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\models\\armor\\leather_layer_2.png",
                        "assets\\minecraft\\textures\\models\\armor\\leather_layer_2.png",
                        "assets\\minecraft\\textures\\models\\armor\\leather_layer_2.png",
                        "assets\\minecraft\\textures\\models\\armor\\leather_layer_2.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\models\\armor\\leather_layer_2_overlay.png",
                        "assets\\minecraft\\textures\\models\\armor\\leather_layer_2_overlay.png",
                        "assets\\minecraft\\textures\\models\\armor\\leather_layer_2_overlay.png",
                        "assets\\minecraft\\textures\\models\\armor\\leather_layer_2_overlay.png"
                    ),
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        "assets\\minecraft\\textures\\models\\armor\\turtle_layer_1.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\painting",
                "assets\\minecraft\\textures\\painting",
                "assets\\minecraft\\textures\\painting",
                "assets\\minecraft\\textures\\painting",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\painting\\paintings_kristoffer_zetterstrand.png",
                        "assets\\minecraft\\textures\\painting\\paintings_kristoffer_zetterstrand.png",
                        "assets\\minecraft\\textures\\painting\\paintings_kristoffer_zetterstrand.png",
                        "assets\\minecraft\\textures\\painting\\paintings_kristoffer_zetterstrand.png"
                    ),
                }
            ),
            new Carpetas
            (
                "assets\\minecraft\\textures\\particle",
                "assets\\minecraft\\textures\\particle",
                "assets\\minecraft\\textures\\particle",
                "assets\\minecraft\\textures\\particle",
                new Archivos[]
                {
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\particle\\footprint.png",
                        "assets\\minecraft\\textures\\particle\\footprint.png",
                        "assets\\minecraft\\textures\\particle\\footprint.png",
                        "assets\\minecraft\\textures\\particle\\footprint.png"
                    ),
                    new Archivos
                    (
                        "assets\\minecraft\\textures\\particle\\particles.png",
                        "assets\\minecraft\\textures\\particle\\particles.png",
                        "assets\\minecraft\\textures\\particle\\particles.png",
                        "assets\\minecraft\\textures\\particle\\particles.png"
                    ),
                }
            ),
            /*new Carpetas
            (
                "",
                "",
                "",
                "",
                new Archivos[]
                {

                }
            ),*/
            /*new Carpetas
            (
                "",
                "",
                "",
                "",
                new Archivos[]
                {
                    new Archivos
                    (
                        "",
                        "",
                        "",
                        ""
                    ),
                }
            ),*/
        };

        /// <summary>
        /// Structure that holds up all the information about a resource folder from any Minecraft version, this includes all of it's resource files.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Carpetas
        {
            internal string Ruta_Pack_1;
            internal string Ruta_Pack_2;
            internal string Ruta_Pack_3;
            internal string Ruta_Pack_4;
            internal Archivos[] Matriz_Archivos;

            internal Carpetas(string Ruta_Pack_1, string Ruta_Pack_2, string Ruta_Pack_3, string Ruta_Pack_4, Archivos[] Matriz_Archivos)
            {
                this.Ruta_Pack_1 = Ruta_Pack_1;
                this.Ruta_Pack_2 = Ruta_Pack_2;
                this.Ruta_Pack_3 = Ruta_Pack_3;
                this.Ruta_Pack_4 = Ruta_Pack_4;
                this.Matriz_Archivos = Matriz_Archivos;
            }

            /*internal string Ruta_1_12_2;
            internal string Ruta_1_13_2;
            internal Recursos[] Matriz_Recursos;

            internal Carpetas(string Ruta_1_12_2, string Ruta_1_13_2, Recursos[] Matriz_Recursos)
            {
                this.Ruta_1_12_2 = Ruta_1_12_2;
                this.Ruta_1_13_2 = Ruta_1_13_2;
                this.Matriz_Recursos = Matriz_Recursos;
            }

            internal static readonly Carpetas[] Matriz_Carpetas = new Carpetas[]
            {
                //new Recursos(Resources.Ejecutar, "None (select it manually everytime)", null, CheckState.Checked),
                
            };*/
        }

        /// <summary>
        /// Structure that holds up all the information about a resource from any Minecraft version.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Archivos
        {
            internal string Ruta_Pack_1;
            internal string Ruta_Pack_2;
            internal string Ruta_Pack_3;
            internal string Ruta_Pack_4;

            internal Archivos(string Ruta_Pack_1, string Ruta_Pack_2, string Ruta_Pack_3, string Ruta_Pack_4)
            {
                this.Ruta_Pack_1 = Ruta_Pack_1;
                this.Ruta_Pack_2 = Ruta_Pack_2;
                this.Ruta_Pack_3 = Ruta_Pack_3;
                this.Ruta_Pack_4 = Ruta_Pack_4;
            }

            /*internal string Nombre_1_12_2;
            internal uint CRC_1_12_2;
            internal string Nombre_1_13_2;
            internal uint CRC_1_13_2;*//*

            /// <summary>
            /// A relative resource path for the 1.12.2 Minecraft version.
            /// </summary>
            internal Dictionary<string, string> Diccionario_Rutas;
            /// <summary>
            /// A relative resource path for the 1.13.2 Minecraft version.
            /// </summary>
            internal Dictionary<string, string> Diccionario_Nombres;
            /// <summary>
            /// A relative resource path for any version.
            /// </summary>
            internal string Ruta;
            /// <summary>
            /// A resource name with it's extension for any version.
            /// </summary>
            internal string Nombre;
            /// <summary>
            /// A useful value to know if 2 files are the same. Mostly these values will come from the 1.13.2 Minecraft version (the latest at this moment).
            /// </summary>
            internal uint CRC_32;
            /// <summary>
            /// A useful value to know from which version comes this resource and even if it's from Minecraft or Faithful.
            /// </summary>
            internal string Versión;

            internal Recursos(Dictionary<string, string> Diccionario_Rutas, Dictionary<string, string> Diccionario_Nombres, string Ruta, string Nombre, uint CRC_32, string Versión)
            {
                this.Diccionario_Rutas = Diccionario_Rutas;
                this.Diccionario_Nombres = Diccionario_Nombres;
                this.Ruta = Ruta;
                this.Nombre = Nombre;
                this.CRC_32 = CRC_32;
                this.Versión = Versión;
            }

            internal static readonly Recursos[] Matriz_Recursos = new Recursos[]
            {
                //new Recursos(Resources.Ejecutar, "None (select it manually everytime)", null, CheckState.Checked),
                
            };

            /// <summary>
            /// Searches for all the available resources in the specified folders and generates a unique list of relative paths used later for correctly convert resource packs between Minecraft versions. Note this function still requires a lot of manual work where the resource names are different between versions, but at least will be a list of the relative paths that haven't been automatically found, so they can be manually moved.
            /// </summary>
            internal void Buscar_Recursos()
            {
                try
                {
                    /*string Ruta_Minecraft_1_12_2 = TextBox_Minecraft_1_12_2.Text;
                    string Ruta_Faithful_1_12_2 = TextBox_Minecraft_1_12_2.Text;

                    string Ruta_Faithful_1_13_1 = @"C:\Users\Jupisoft\Videos\__DVDs copiados\Faithful+1.13.1-rv2";
                    string Ruta_Minecraft_1_13_1 = @"C:\Users\Jupisoft\Videos\__DVDs copiados\1.13.1";
                    *//*












                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            }*/
        }
    }
}
