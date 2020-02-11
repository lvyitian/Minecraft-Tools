using ImageMagick;
using Minecraft_Tools.Properties;
using Paloma;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Extractor_Recursos_Minecraft_Xbox_360 : Form
    {
        public Ventana_Extractor_Recursos_Minecraft_Xbox_360()
        {
            InitializeComponent();
        }

        // Byte arrays that store copies for the default files for block animations in Minecraft 1.14.4.
        internal static readonly byte[] Matriz_Bytes_blast_furnace_front_on_png_mcmeta = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_campfire_fire_png_mcmeta = new byte[44] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 50, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_campfire_log_lit_png_mcmeta = new byte[82] { 123, 10, 32, 32, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 50, 48, 10, 32, 32, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_chain_command_block_back_png_mcmeta = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_chain_command_block_conditional_png_mcmeta = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_chain_command_block_front_png_mcmeta = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_chain_command_block_side_png_mcmeta = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_command_block_back_png_mcmeta = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_command_block_conditional_png_mcmeta = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_command_block_front_png_mcmeta = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_command_block_side_png_mcmeta = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_fire_0_png_mcmeta = new byte[356] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 49, 54, 44, 10, 32, 32, 32, 32, 32, 32, 49, 55, 44, 10, 32, 32, 32, 32, 32, 32, 49, 56, 44, 10, 32, 32, 32, 32, 32, 32, 49, 57, 44, 10, 32, 32, 32, 32, 32, 32, 50, 48, 44, 10, 32, 32, 32, 32, 32, 32, 50, 49, 44, 10, 32, 32, 32, 32, 32, 32, 50, 50, 44, 10, 32, 32, 32, 32, 32, 32, 50, 51, 44, 10, 32, 32, 32, 32, 32, 32, 50, 52, 44, 10, 32, 32, 32, 32, 32, 32, 50, 53, 44, 10, 32, 32, 32, 32, 32, 32, 50, 54, 44, 10, 32, 32, 32, 32, 32, 32, 50, 55, 44, 10, 32, 32, 32, 32, 32, 32, 50, 56, 44, 10, 32, 32, 32, 32, 32, 32, 50, 57, 44, 10, 32, 32, 32, 32, 32, 32, 51, 48, 44, 10, 32, 32, 32, 32, 32, 32, 51, 49, 44, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 32, 32, 32, 32, 32, 32, 49, 44, 10, 32, 32, 32, 32, 32, 32, 50, 44, 10, 32, 32, 32, 32, 32, 32, 51, 44, 10, 32, 32, 32, 32, 32, 32, 52, 44, 10, 32, 32, 32, 32, 32, 32, 53, 44, 10, 32, 32, 32, 32, 32, 32, 54, 44, 10, 32, 32, 32, 32, 32, 32, 55, 44, 10, 32, 32, 32, 32, 32, 32, 56, 44, 10, 32, 32, 32, 32, 32, 32, 57, 44, 10, 32, 32, 32, 32, 32, 32, 49, 48, 44, 10, 32, 32, 32, 32, 32, 32, 49, 49, 44, 10, 32, 32, 32, 32, 32, 32, 49, 50, 44, 10, 32, 32, 32, 32, 32, 32, 49, 51, 44, 10, 32, 32, 32, 32, 32, 32, 49, 52, 44, 10, 32, 32, 32, 32, 32, 32, 49, 53, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_fire_1_png_mcmeta = new byte[21] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 125, 10, 125 };
        internal static readonly byte[] Matriz_Bytes_kelp_png_mcmeta = new byte[44] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 50, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_kelp_plant_png_mcmeta = new byte[44] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 50, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_lantern_png_mcmeta = new byte[43] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 56, 10, 32, 32, 125, 10, 125 };
        internal static readonly byte[] Matriz_Bytes_lava_flow_png_mcmeta = new byte[44] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 51, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_lava_still_png_mcmeta = new byte[426] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 50, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 32, 32, 32, 32, 32, 32, 49, 44, 10, 32, 32, 32, 32, 32, 32, 50, 44, 10, 32, 32, 32, 32, 32, 32, 51, 44, 10, 32, 32, 32, 32, 32, 32, 52, 44, 10, 32, 32, 32, 32, 32, 32, 53, 44, 10, 32, 32, 32, 32, 32, 32, 54, 44, 10, 32, 32, 32, 32, 32, 32, 55, 44, 10, 32, 32, 32, 32, 32, 32, 56, 44, 10, 32, 32, 32, 32, 32, 32, 57, 44, 10, 32, 32, 32, 32, 32, 32, 49, 48, 44, 10, 32, 32, 32, 32, 32, 32, 49, 49, 44, 10, 32, 32, 32, 32, 32, 32, 49, 50, 44, 10, 32, 32, 32, 32, 32, 32, 49, 51, 44, 10, 32, 32, 32, 32, 32, 32, 49, 52, 44, 10, 32, 32, 32, 32, 32, 32, 49, 53, 44, 10, 32, 32, 32, 32, 32, 32, 49, 54, 44, 10, 32, 32, 32, 32, 32, 32, 49, 55, 44, 10, 32, 32, 32, 32, 32, 32, 49, 56, 44, 10, 32, 32, 32, 32, 32, 32, 49, 57, 44, 10, 32, 32, 32, 32, 32, 32, 49, 56, 44, 10, 32, 32, 32, 32, 32, 32, 49, 55, 44, 10, 32, 32, 32, 32, 32, 32, 49, 54, 44, 10, 32, 32, 32, 32, 32, 32, 49, 53, 44, 10, 32, 32, 32, 32, 32, 32, 49, 52, 44, 10, 32, 32, 32, 32, 32, 32, 49, 51, 44, 10, 32, 32, 32, 32, 32, 32, 49, 50, 44, 10, 32, 32, 32, 32, 32, 32, 49, 49, 44, 10, 32, 32, 32, 32, 32, 32, 49, 48, 44, 10, 32, 32, 32, 32, 32, 32, 57, 44, 10, 32, 32, 32, 32, 32, 32, 56, 44, 10, 32, 32, 32, 32, 32, 32, 55, 44, 10, 32, 32, 32, 32, 32, 32, 54, 44, 10, 32, 32, 32, 32, 32, 32, 53, 44, 10, 32, 32, 32, 32, 32, 32, 52, 44, 10, 32, 32, 32, 32, 32, 32, 51, 44, 10, 32, 32, 32, 32, 32, 32, 50, 44, 10, 32, 32, 32, 32, 32, 32, 49, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125 };
        internal static readonly byte[] Matriz_Bytes_magma_png_mcmeta = new byte[118] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 56, 44, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 32, 32, 32, 32, 32, 32, 49, 44, 10, 32, 32, 32, 32, 32, 32, 50, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_nether_portal_png_mcmeta = new byte[21] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 125, 10, 125 };
        internal static readonly byte[] Matriz_Bytes_prismarine_png_mcmeta = new byte[291] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 51, 48, 48, 44, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 115, 34, 58, 32, 91, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 32, 32, 32, 32, 32, 32, 49, 44, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 32, 32, 32, 32, 32, 32, 50, 44, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 32, 32, 32, 32, 32, 32, 51, 44, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 32, 32, 32, 32, 32, 32, 49, 44, 10, 32, 32, 32, 32, 32, 32, 50, 44, 10, 32, 32, 32, 32, 32, 32, 49, 44, 10, 32, 32, 32, 32, 32, 32, 51, 44, 10, 32, 32, 32, 32, 32, 32, 49, 44, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 32, 32, 32, 32, 32, 32, 50, 44, 10, 32, 32, 32, 32, 32, 32, 49, 44, 10, 32, 32, 32, 32, 32, 32, 50, 44, 10, 32, 32, 32, 32, 32, 32, 51, 44, 10, 32, 32, 32, 32, 32, 32, 50, 44, 10, 32, 32, 32, 32, 32, 32, 48, 44, 10, 32, 32, 32, 32, 32, 32, 51, 44, 10, 32, 32, 32, 32, 32, 32, 49, 44, 10, 32, 32, 32, 32, 32, 32, 51, 10, 32, 32, 32, 32, 93, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_repeating_command_block_back_png_mcmeta = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_repeating_command_block_conditional_png_mcmeta = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_repeating_command_block_front_png_mcmeta = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_repeating_command_block_side_png_mcmeta = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 116, 114, 117, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 48, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_sea_lantern_png_mcmeta = new byte[52] { 123, 10, 32, 32, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 53, 10, 32, 32, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_seagrass_png_mcmeta = new byte[52] { 123, 10, 32, 32, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 50, 10, 32, 32, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_smoker_front_on_png_mcmeta = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 102, 97, 108, 115, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 52, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_stonecutter_saw_png_mcmeta = new byte[70] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 105, 110, 116, 101, 114, 112, 111, 108, 97, 116, 101, 34, 58, 32, 102, 97, 108, 115, 101, 44, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 49, 10, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_tall_seagrass_bottom_png_mcmeta = new byte[52] { 123, 10, 32, 32, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 50, 10, 32, 32, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_tall_seagrass_top_png_mcmeta = new byte[52] { 123, 10, 32, 32, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 50, 10, 32, 32, 32, 32, 125, 10, 125, 10 };
        internal static readonly byte[] Matriz_Bytes_water_flow_png_mcmeta = new byte[21] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 125, 10, 125 };
        internal static readonly byte[] Matriz_Bytes_water_still_png_mcmeta = new byte[44] { 123, 10, 32, 32, 34, 97, 110, 105, 109, 97, 116, 105, 111, 110, 34, 58, 32, 123, 10, 32, 32, 32, 32, 34, 102, 114, 97, 109, 101, 116, 105, 109, 101, 34, 58, 32, 50, 10, 32, 32, 125, 10, 125, 10 };

        internal static Dictionary<string, Rectangle> Diccionario_Cuadros_Rectángulos = null;

        internal static readonly long Tamaño_100_MB = 100L * 1024L * 1024L; // 100 bytes to KB and to MB.

        internal static readonly string Ruta_Xbox_360 = Application.StartupPath + "\\Xbox";
        internal static readonly string Ruta_Xbox_360_DLC = Application.StartupPath + "\\Xbox\\res\\DLC";
        internal static readonly string Ruta_PC = Application.StartupPath + "\\PC";

        internal readonly string Texto_Título = "Minecraft Xbox 360 Edition Resources Extractor by Jupisoft for " + Program.Texto_Usuario;
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

        internal Thread Subproceso = null;
        internal bool Pendiente_Subproceso_Abortar = false;
        internal bool Subproceso_Abortado = false;
        internal bool Subproceso_Activo = false;

        private void Ventana_Extractor_Recursos_Minecraft_Xbox_360_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                if (Diccionario_Cuadros_Rectángulos == null)
                {
                    Diccionario_Cuadros_Rectángulos = new Dictionary<string, Rectangle>();

                    Diccionario_Cuadros_Rectángulos.Add("kebab", new Rectangle(0, 0, 1, 1));
                    Diccionario_Cuadros_Rectángulos.Add("aztec", new Rectangle(1, 0, 1, 1));
                    Diccionario_Cuadros_Rectángulos.Add("alban", new Rectangle(2, 0, 1, 1));
                    Diccionario_Cuadros_Rectángulos.Add("aztec2", new Rectangle(3, 0, 1, 1));
                    Diccionario_Cuadros_Rectángulos.Add("bomb", new Rectangle(4, 0, 1, 1));
                    Diccionario_Cuadros_Rectángulos.Add("plant", new Rectangle(5, 0, 1, 1));
                    Diccionario_Cuadros_Rectángulos.Add("wasteland", new Rectangle(6, 0, 1, 1));
                    
                    Diccionario_Cuadros_Rectángulos.Add("_kebab_", new Rectangle(0, 1, 1, 1));
                    Diccionario_Cuadros_Rectángulos.Add("_aztec_", new Rectangle(1, 1, 1, 1));
                    Diccionario_Cuadros_Rectángulos.Add("_alban_", new Rectangle(2, 1, 1, 1));
                    Diccionario_Cuadros_Rectángulos.Add("_aztec2_", new Rectangle(3, 1, 1, 1));
                    Diccionario_Cuadros_Rectángulos.Add("_bomb_", new Rectangle(4, 1, 1, 1));

                    Diccionario_Cuadros_Rectángulos.Add("pool", new Rectangle(0, 2, 2, 1));
                    Diccionario_Cuadros_Rectángulos.Add("courbet", new Rectangle(2, 2, 2, 1));
                    Diccionario_Cuadros_Rectángulos.Add("sea", new Rectangle(4, 2, 2, 1));
                    Diccionario_Cuadros_Rectángulos.Add("sunset", new Rectangle(6, 2, 2, 1));
                    Diccionario_Cuadros_Rectángulos.Add("creebet", new Rectangle(8, 2, 2, 1));

                    Diccionario_Cuadros_Rectángulos.Add("wanderer", new Rectangle(0, 4, 1, 2));
                    Diccionario_Cuadros_Rectángulos.Add("graham", new Rectangle(1, 4, 1, 2));

                    Diccionario_Cuadros_Rectángulos.Add("match", new Rectangle(0, 8, 2, 2));
                    Diccionario_Cuadros_Rectángulos.Add("bust", new Rectangle(2, 8, 2, 2));
                    Diccionario_Cuadros_Rectángulos.Add("stage", new Rectangle(4, 8, 2, 2));
                    Diccionario_Cuadros_Rectángulos.Add("void", new Rectangle(6, 8, 2, 2));
                    Diccionario_Cuadros_Rectángulos.Add("skull_and_roses", new Rectangle(8, 8, 2, 2));
                    Diccionario_Cuadros_Rectángulos.Add("wither", new Rectangle(10, 8, 2, 2));

                    Diccionario_Cuadros_Rectángulos.Add("fighters", new Rectangle(0, 6, 4, 2));

                    Diccionario_Cuadros_Rectángulos.Add("skeleton", new Rectangle(12, 4, 4, 3));
                    Diccionario_Cuadros_Rectángulos.Add("donkey_kong", new Rectangle(12, 7, 4, 3));

                    Diccionario_Cuadros_Rectángulos.Add("pointer", new Rectangle(0, 12, 4, 4));
                    Diccionario_Cuadros_Rectángulos.Add("pigscene", new Rectangle(4, 12, 4, 4));
                    Diccionario_Cuadros_Rectángulos.Add("burning_skull", new Rectangle(8, 12, 4, 4));

                    Diccionario_Cuadros_Rectángulos.Add("back", new Rectangle(12, 0, 1, 1)); // MC 1.14+.
                }
                Ocupado = true;
                Program.Crear_Carpetas(Ruta_Xbox_360_DLC);
                TextBox_Ruta.Text = Ruta_Xbox_360_DLC;
                Picture_Mass_Effect.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Mass_Effect, 64, 64, true, false, CheckState.Checked);
                Registro_Cargar_Opciones();
                Ocupado = false;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Extractor_Recursos_Minecraft_Xbox_360_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
                /*// For Optifine's rainbow sky light try (experimental and fun):
                // Result: the light moves to fast on transitions and then too slow. Useless?
                Bitmap Imagen = new Bitmap(64, 64, PixelFormat.Format32bppArgb);
                Graphics Pintar = Graphics.FromImage(Imagen);
                Pintar.CompositingMode = CompositingMode.SourceCopy;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.HighQuality;
                Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                for (int Índice = 0; Índice < 64; Índice++)
                {
                    int Índice_1530 = (Índice * 1529) / 64; // Skip the last to space it.
                    SolidBrush Pincel = new SolidBrush(Program.Obtener_Color_Puro_1530(Índice_1530));
                    Pintar.FillRectangle(Pincel, Índice, 0, 64, 16);
                    Pincel.Dispose();
                    Pincel = null;
                }
                Pintar.Dispose();
                Pintar = null;
                Program.Guardar_Imagen_Temporal(Imagen);*/
                /*// For Optifine's rainbow torch light try (experimental and fun):
                // Result: it worked as expected, more or less, it might be very useful.
                Bitmap Imagen = new Bitmap(16, 16, PixelFormat.Format32bppArgb);
                Graphics Pintar = Graphics.FromImage(Imagen);
                Pintar.CompositingMode = CompositingMode.SourceCopy;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.HighQuality;
                Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                for (int Índice = 0; Índice < 16; Índice++)
                {
                    int Índice_1530 = (Índice * 1529) / 16; // Skip the last to space it.
                    SolidBrush Pincel = new SolidBrush(Program.Obtener_Color_Puro_1530(Índice_1530));
                    Pintar.FillRectangle(Pincel, 0, Índice, 16, 1);
                    Pincel.Dispose();
                    Pincel = null;
                }
                Pintar.Dispose();
                Pintar = null;
                Program.Guardar_Imagen_Temporal(Imagen);*/
                /*bool[][] Matriz_XY = new bool[6][] // Forager puzzle solver.
                {
                    new bool[6]{ false, false, false, false, false, true },
                    new bool[6]{ false, false, false, false, false, false },
                    new bool[6]{ false, false, false, false, false, false },
                    new bool[6]{ false, false, false, false, false, false },
                    new bool[6]{ false, false, false, false, true, false },
                    new bool[6]{ false, false, false, false, false, false },
                };*/
                /*for (int Índice_Iteración = 0; Índice_Iteración < 1000000; Índice_Iteración++)
                {
                    bool[][] Matriz_Temporal = new bool[6][] // Forager puzzle solver.
                    {
                        new bool[6]{ false, true, false, false, true, true },
                        new bool[6]{ false, false, true, true, false, false },
                        new bool[6]{ false, true, true, true, false, true },
                        new bool[6]{ true, false, true, true, true, true },
                        new bool[6]{ false, true, true, false, false, false },
                        new bool[6]{ false, false, false, true, false, false },
                    };
                    List<Point> Lista_Posiciones = new List<Point>();
                    bool Terminado = true;
                    for (int Índice_Paso = 0; Índice_Paso < 100; Índice_Paso++)
                    {
                        Point Posición = new Point(Program.Rand.Next(0, 6), Program.Rand.Next(0, 6));
                        Lista_Posiciones.Add(Posición);
                        if (Posición.X - 1 > -1) Matriz_Temporal[Posición.Y][Posición.X - 1] = !Matriz_Temporal[Posición.Y][Posición.X - 1]; // Invert left.
                        if (Posición.X + 1 < 6) Matriz_Temporal[Posición.Y][Posición.X + 1] = !Matriz_Temporal[Posición.Y][Posición.X + 1]; // Invert right.
                        Matriz_Temporal[Posición.Y][Posición.X] = !Matriz_Temporal[Posición.Y][Posición.X]; // Always invert center.
                        if (Posición.Y - 1 > -1) Matriz_Temporal[Posición.Y - 1][Posición.X] = !Matriz_Temporal[Posición.Y - 1][Posición.X]; // Invert top.
                        if (Posición.Y + 1 < 6) Matriz_Temporal[Posición.Y + 1][Posición.X] = !Matriz_Temporal[Posición.Y + 1][Posición.X]; // Invert bottom.

                        // Check if all are off.
                        Terminado = true;
                        for (int Índice_Y = 0; Índice_Y < 6; Índice_Y++)
                        {
                            for (int Índice_X = 0; Índice_X < 6; Índice_X++)
                            {
                                if (Matriz_Temporal[Índice_Y][Índice_X])
                                {
                                    Terminado = false;
                                    break;
                                }
                                if (!Terminado) break;
                            }
                        }
                        // Check if all are on.
                        Terminado = true;
                        for (int Índice_Y = 0; Índice_Y < 6; Índice_Y++)
                        {
                            for (int Índice_X = 0; Índice_X < 6; Índice_X++)
                            {
                                if (!Matriz_Temporal[Índice_Y][Índice_X])
                                {
                                    Terminado = false;
                                    break;
                                }
                                if (!Terminado) break;
                            }
                        }
                        if (Terminado) break;
                    }
                    if (Terminado)
                    {
                        string Texto = null;
                        foreach (Point Posición in Lista_Posiciones)
                        {
                            Texto += Posición.ToString() + "\r\n";
                        }
                        Clipboard.SetText(Texto);
                        MessageBox.Show("Fin");
                        return;
                    }
                }
                MessageBox.Show("?");*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Extractor_Recursos_Minecraft_Xbox_360_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //Temporizador_Principal.Stop();
                if (Subproceso_Activo)
                {
                    if (MessageBox.Show(this, "Currently there is a resource extraction in progress.\r\nDo you want to cancel it, but saving what has been done?", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes && Subproceso_Activo) // Since a message can stay on top for infinite time, double check if it's still converting.
                    {
                        Pendiente_Subproceso_Abortar = true;
                    }
                    e.Cancel = true;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Extractor_Recursos_Minecraft_Xbox_360_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Extractor_Recursos_Minecraft_Xbox_360_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Extractor_Recursos_Minecraft_Xbox_360_DragDrop(object sender, DragEventArgs e)
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
                                if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                                {
                                    byte[] Matriz_Bytes = Program.Obtener_Matriz_Bytes_Archivo(Ruta);
                                    if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                                    {
                                        string Texto = "internal static readonly byte[] Matriz_Bytes_" + Path.GetFileName(Ruta).Replace('.', '_') + " = new byte[" + Matriz_Bytes.Length.ToString() + "] { ";
                                        foreach (byte Valor in Matriz_Bytes)
                                        {
                                            Texto += Valor.ToString() + ", ";
                                        }
                                        Texto = Texto.TrimEnd(", ".ToCharArray()) + " };";
                                        Clipboard.SetText(Texto);
                                        SystemSounds.Asterisk.Play();
                                        Matriz_Bytes = null;
                                        break;
                                    }
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

        private void Ventana_Extractor_Recursos_Minecraft_Xbox_360_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Extractor_Recursos_Minecraft_Xbox_360_KeyDown(object sender, KeyEventArgs e)
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

        //internal BackgroundWorker Subproceso = null;

        internal static readonly List<KeyValuePair<string, Color>> Lista_Nombres_Colores = new List<KeyValuePair<string, Color>>(new KeyValuePair<string, Color>[]
        {
            new KeyValuePair<string, Color>("Foliage_Evergreen", Color.FromArgb(255, 97, 153, 97)),
            new KeyValuePair<string, Color>("Foliage_Birch", Color.FromArgb(255, 128, 167, 85)),
            new KeyValuePair<string, Color>("Foliage_Default", Color.FromArgb(255, 72, 181, 24)),
            new KeyValuePair<string, Color>("Foliage_Mesa", Color.FromArgb(255, 158, 129, 77)),
            new KeyValuePair<string, Color>("Foliage_Swampland", Color.FromArgb(255, 106, 112, 57)),
            new KeyValuePair<string, Color>("Grass_Common", Color.FromArgb(255, 124, 189, 107)),
            new KeyValuePair<string, Color>("Grass_Mesa", Color.FromArgb(255, 144, 129, 77)),
            new KeyValuePair<string, Color>("Grass_Swamp1", Color.FromArgb(255, 76, 118, 60)),
            new KeyValuePair<string, Color>("Grass_Swamp2", Color.FromArgb(255, 106, 112, 57)),
            new KeyValuePair<string, Color>("Grass_Roofed_Forest_Modifier", Color.FromArgb(255, 40, 53, 11)),
            new KeyValuePair<string, Color>("Water_Ocean", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_Plains", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_Desert", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_ExtremeHills", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_Forest", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_Taiga", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_Swampland", Color.FromArgb(255, 224, 255, 174)),
            new KeyValuePair<string, Color>("Water_River", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_Hell", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_Sky", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_FrozenOcean", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_FrozenRiver", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_IcePlains", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_IceMountains", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_MushroomIsland", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_MushroomIslandShore", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_Beach", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_DesertHills", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_ForestHills", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_TaigaHills", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_ExtremeHillsEdge", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_Jungle", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_JungleHills", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_JungleEdge", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_DeepOcean", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_StoneBeach", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_ColdBeach", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_BirchForest", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_BirchForestHills", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_RoofedForest", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_ColdTaiga", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_ColdTaigaHills", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_MegaTaiga", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_MegaTaigaHills", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_ExtremeHillsPlus", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_Savanna", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_SavannaPlateau", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_Mesa", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_MesaPlateauF", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Water_MesaPlateau", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Sky_Ocean", Color.FromArgb(255, 123, 165, 255)),
            new KeyValuePair<string, Color>("Sky_Plains", Color.FromArgb(255, 121, 167, 255)),
            new KeyValuePair<string, Color>("Sky_Desert", Color.FromArgb(255, 111, 178, 255)),
            new KeyValuePair<string, Color>("Sky_ExtremeHills", Color.FromArgb(255, 126, 163, 255)),
            new KeyValuePair<string, Color>("Sky_Forest", Color.FromArgb(255, 122, 166, 255)),
            new KeyValuePair<string, Color>("Sky_Taiga", Color.FromArgb(255, 127, 162, 255)),
            new KeyValuePair<string, Color>("Sky_Swampland", Color.FromArgb(255, 121, 167, 255)),
            new KeyValuePair<string, Color>("Sky_River", Color.FromArgb(255, 123, 165, 255)),
            new KeyValuePair<string, Color>("Sky_Hell", Color.FromArgb(255, 111, 178, 255)),
            new KeyValuePair<string, Color>("Sky_Sky", Color.FromArgb(255, 0, 0, 0)),
            new KeyValuePair<string, Color>("Sky_FrozenOcean", Color.FromArgb(255, 128, 161, 255)),
            new KeyValuePair<string, Color>("Sky_FrozenRiver", Color.FromArgb(255, 128, 161, 255)),
            new KeyValuePair<string, Color>("Sky_IcePlains", Color.FromArgb(255, 128, 161, 255)),
            new KeyValuePair<string, Color>("Sky_IceMountains", Color.FromArgb(255, 128, 161, 255)),
            new KeyValuePair<string, Color>("Sky_MushroomIsland", Color.FromArgb(255, 120, 168, 255)),
            new KeyValuePair<string, Color>("Sky_MushroomIslandShore", Color.FromArgb(255, 120, 168, 255)),
            new KeyValuePair<string, Color>("Sky_Beach", Color.FromArgb(255, 121, 167, 255)),
            new KeyValuePair<string, Color>("Sky_DesertHills", Color.FromArgb(255, 111, 178, 255)),
            new KeyValuePair<string, Color>("Sky_ForestHills", Color.FromArgb(255, 122, 166, 255)),
            new KeyValuePair<string, Color>("Sky_TaigaHills", Color.FromArgb(255, 127, 162, 255)),
            new KeyValuePair<string, Color>("Sky_ExtremeHillsEdge", Color.FromArgb(255, 126, 163, 255)),
            new KeyValuePair<string, Color>("Sky_Jungle", Color.FromArgb(255, 117, 171, 255)),
            new KeyValuePair<string, Color>("Sky_JungleHills", Color.FromArgb(255, 117, 171, 255)),
            new KeyValuePair<string, Color>("Sky_JungleEdge", Color.FromArgb(255, 117, 171, 255)),
            new KeyValuePair<string, Color>("Sky_DeepOcean", Color.FromArgb(255, 123, 165, 255)),
            new KeyValuePair<string, Color>("Sky_StoneBeach", Color.FromArgb(255, 126, 163, 255)),
            new KeyValuePair<string, Color>("Sky_ColdBeach", Color.FromArgb(255, 128, 161, 255)),
            new KeyValuePair<string, Color>("Sky_BirchForest", Color.FromArgb(255, 122, 165, 255)),
            new KeyValuePair<string, Color>("Sky_BirchForestHills", Color.FromArgb(255, 122, 165, 255)),
            new KeyValuePair<string, Color>("Sky_RoofedForest", Color.FromArgb(255, 122, 166, 255)),
            new KeyValuePair<string, Color>("Sky_ColdTaiga", Color.FromArgb(255, 131, 158, 255)),
            new KeyValuePair<string, Color>("Sky_ColdTaigaHills", Color.FromArgb(255, 131, 158, 255)),
            new KeyValuePair<string, Color>("Sky_MegaTaiga", Color.FromArgb(255, 127, 162, 255)),
            new KeyValuePair<string, Color>("Sky_MegaTaigaHills", Color.FromArgb(255, 127, 162, 255)),
            new KeyValuePair<string, Color>("Sky_ExtremeHillsPlus", Color.FromArgb(255, 126, 163, 255)),
            new KeyValuePair<string, Color>("Sky_Savanna", Color.FromArgb(255, 117, 170, 255)),
            new KeyValuePair<string, Color>("Sky_SavannaPlateau", Color.FromArgb(255, 118, 168, 255)),
            new KeyValuePair<string, Color>("Sky_Mesa", Color.FromArgb(255, 111, 178, 255)),
            new KeyValuePair<string, Color>("Sky_MesaPlateauF", Color.FromArgb(255, 111, 178, 255)),
            new KeyValuePair<string, Color>("Sky_MesaPlateau", Color.FromArgb(255, 111, 178, 255)),
            new KeyValuePair<string, Color>("Sky_Dawn_Dark", Color.FromArgb(255, 178, 51, 51)),
            new KeyValuePair<string, Color>("Sky_Dawn_Bright", Color.FromArgb(255, 255, 229, 51)),
            new KeyValuePair<string, Color>("Preview_Ocean", Color.FromArgb(255, 36, 50, 137)),
            new KeyValuePair<string, Color>("Preview_Plains", Color.FromArgb(255, 141, 179, 96)),
            new KeyValuePair<string, Color>("Preview_Desert", Color.FromArgb(255, 225, 225, 78)),
            new KeyValuePair<string, Color>("Preview_ExtremeHills", Color.FromArgb(255, 96, 96, 96)),
            new KeyValuePair<string, Color>("Preview_Forest", Color.FromArgb(255, 5, 102, 33)),
            new KeyValuePair<string, Color>("Preview_Taiga", Color.FromArgb(255, 11, 102, 89)),
            new KeyValuePair<string, Color>("Preview_Swampland", Color.FromArgb(255, 53, 54, 45)),
            new KeyValuePair<string, Color>("Preview_River", Color.FromArgb(255, 81, 97, 255)),
            new KeyValuePair<string, Color>("Preview_Hell", Color.FromArgb(255, 255, 0, 0)),
            new KeyValuePair<string, Color>("Preview_Sky", Color.FromArgb(255, 128, 128, 255)),
            new KeyValuePair<string, Color>("Preview_FrozenOcean", Color.FromArgb(255, 145, 164, 197)),
            new KeyValuePair<string, Color>("Preview_FrozenRiver", Color.FromArgb(255, 160, 160, 255)),
            new KeyValuePair<string, Color>("Preview_IcePlains", Color.FromArgb(255, 204, 208, 238)),
            new KeyValuePair<string, Color>("Preview_IceMountains", Color.FromArgb(255, 169, 174, 198)),
            new KeyValuePair<string, Color>("Preview_MushroomIsland", Color.FromArgb(255, 165, 45, 96)),
            new KeyValuePair<string, Color>("Preview_MushroomIslandShore", Color.FromArgb(255, 165, 45, 96)),
            new KeyValuePair<string, Color>("Preview_Beach", Color.FromArgb(255, 203, 196, 91)),
            new KeyValuePair<string, Color>("Preview_DesertHills", Color.FromArgb(255, 213, 213, 74)),
            new KeyValuePair<string, Color>("Preview_ForestHills", Color.FromArgb(255, 34, 85, 28)),
            new KeyValuePair<string, Color>("Preview_TaigaHills", Color.FromArgb(255, 36, 63, 54)),
            new KeyValuePair<string, Color>("Preview_ExtremeHillsEdge", Color.FromArgb(255, 84, 96, 80)),
            new KeyValuePair<string, Color>("Preview_Jungle", Color.FromArgb(255, 83, 123, 9)),
            new KeyValuePair<string, Color>("Preview_JungleHills", Color.FromArgb(255, 61, 91, 7)),
            new KeyValuePair<string, Color>("Preview_JungleEdge", Color.FromArgb(255, 83, 123, 9)),
            new KeyValuePair<string, Color>("Preview_DeepOcean", Color.FromArgb(255, 29, 41, 112)),
            new KeyValuePair<string, Color>("Preview_StoneBeach", Color.FromArgb(255, 162, 162, 132)),
            new KeyValuePair<string, Color>("Preview_ColdBeach", Color.FromArgb(255, 236, 250, 210)),
            new KeyValuePair<string, Color>("Preview_BirchForest", Color.FromArgb(255, 48, 116, 68)),
            new KeyValuePair<string, Color>("Preview_BirchForestHills", Color.FromArgb(255, 31, 95, 50)),
            new KeyValuePair<string, Color>("Preview_RoofedForest", Color.FromArgb(255, 64, 81, 26)),
            new KeyValuePair<string, Color>("Preview_ColdTaiga", Color.FromArgb(255, 49, 85, 74)),
            new KeyValuePair<string, Color>("Preview_ColdTaigaHills", Color.FromArgb(255, 36, 63, 54)),
            new KeyValuePair<string, Color>("Preview_MegaTaiga", Color.FromArgb(255, 89, 102, 81)),
            new KeyValuePair<string, Color>("Preview_MegaTaigaHills", Color.FromArgb(255, 69, 79, 62)),
            new KeyValuePair<string, Color>("Preview_ExtremeHillsPlus", Color.FromArgb(255, 74, 96, 73)),
            new KeyValuePair<string, Color>("Preview_Savanna", Color.FromArgb(255, 188, 211, 96)),
            new KeyValuePair<string, Color>("Preview_SavannaPlateau", Color.FromArgb(255, 170, 191, 87)),
            new KeyValuePair<string, Color>("Preview_Mesa", Color.FromArgb(255, 217, 100, 43)),
            new KeyValuePair<string, Color>("Preview_MesaPlateauF", Color.FromArgb(255, 217, 151, 80)),
            new KeyValuePair<string, Color>("Preview_MesaPlateau", Color.FromArgb(255, 217, 167, 88)),
            new KeyValuePair<string, Color>("Preview_Plains_Mutated", Color.FromArgb(255, 141, 179, 96)),
            new KeyValuePair<string, Color>("Preview_Desert_Mutated", Color.FromArgb(255, 215, 154, 35)),
            new KeyValuePair<string, Color>("Preview_ExtremeHills_Mutated", Color.FromArgb(255, 96, 96, 96)),
            new KeyValuePair<string, Color>("Preview_Forest_Mutated", Color.FromArgb(255, 5, 102, 33)),
            new KeyValuePair<string, Color>("Preview_Taiga_Mutated", Color.FromArgb(255, 11, 102, 89)),
            new KeyValuePair<string, Color>("Preview_Swampland_Mutated", Color.FromArgb(255, 53, 54, 45)),
            new KeyValuePair<string, Color>("Preview_IcePlains_Mutated", Color.FromArgb(255, 204, 208, 238)),
            new KeyValuePair<string, Color>("Preview_Jungle_Mutated", Color.FromArgb(255, 83, 123, 9)),
            new KeyValuePair<string, Color>("Preview_JungleEdge_Mutated", Color.FromArgb(255, 83, 123, 9)),
            new KeyValuePair<string, Color>("Preview_BirchForest_Mutated", Color.FromArgb(255, 48, 116, 68)),
            new KeyValuePair<string, Color>("Preview_BirchForestHills_Mutated", Color.FromArgb(255, 31, 95, 50)),
            new KeyValuePair<string, Color>("Preview_RoofedForest_Mutated", Color.FromArgb(255, 64, 81, 26)),
            new KeyValuePair<string, Color>("Preview_ColdTaiga_Mutated", Color.FromArgb(255, 49, 85, 74)),
            new KeyValuePair<string, Color>("Preview_ColdTaigaHills_Mutated", Color.FromArgb(255, 36, 63, 54)),
            new KeyValuePair<string, Color>("Preview_MegaTaiga_Mutated", Color.FromArgb(255, 89, 102, 81)),
            new KeyValuePair<string, Color>("Preview_MegaTaigaHills_Mutated", Color.FromArgb(255, 69, 79, 62)),
            new KeyValuePair<string, Color>("Preview_ExtremeHillsPlus_Mutated", Color.FromArgb(255, 80, 112, 80)),
            new KeyValuePair<string, Color>("Preview_Savanna_Mutated", Color.FromArgb(255, 189, 178, 95)),
            new KeyValuePair<string, Color>("Preview_SavannaPlateau_Mutated", Color.FromArgb(255, 167, 157, 100)),
            new KeyValuePair<string, Color>("Preview_Mesa_Mutated", Color.FromArgb(255, 217, 100, 43)),
            new KeyValuePair<string, Color>("Preview_MesaPlateauF_Mutated", Color.FromArgb(255, 217, 151, 80)),
            new KeyValuePair<string, Color>("Preview_MesaPlateau_Mutated", Color.FromArgb(255, 217, 167, 88)),
            new KeyValuePair<string, Color>("Tile_RedstoneDust", Color.FromArgb(255, 255, 0, 0)),
            new KeyValuePair<string, Color>("Tile_RedstoneDustUnlit", Color.FromArgb(255, 76, 0, 0)),
            new KeyValuePair<string, Color>("Tile_RedstoneDustLitMin", Color.FromArgb(255, 112, 0, 0)),
            new KeyValuePair<string, Color>("Tile_RedstoneDustLitMax", Color.FromArgb(255, 255, 50, 0)),
            new KeyValuePair<string, Color>("Tile_StemMin", Color.FromArgb(255, 0, 255, 0)),
            new KeyValuePair<string, Color>("Tile_StemMax", Color.FromArgb(255, 224, 199, 28)),
            new KeyValuePair<string, Color>("Tile_WaterLily", Color.FromArgb(255, 32, 128, 48)),
            new KeyValuePair<string, Color>("Material_None", Color.FromArgb(255, 0, 0, 0)),
            new KeyValuePair<string, Color>("Material_Grass", Color.FromArgb(255, 127, 178, 56)),
            new KeyValuePair<string, Color>("Material_Sand", Color.FromArgb(255, 247, 233, 163)),
            new KeyValuePair<string, Color>("Material_Cloth", Color.FromArgb(255, 167, 167, 167)),
            new KeyValuePair<string, Color>("Material_Fire", Color.FromArgb(255, 255, 0, 0)),
            new KeyValuePair<string, Color>("Material_Ice", Color.FromArgb(255, 160, 160, 255)),
            new KeyValuePair<string, Color>("Material_Plant", Color.FromArgb(255, 0, 124, 0)),
            new KeyValuePair<string, Color>("Material_Metal", Color.FromArgb(255, 167, 167, 167)),
            new KeyValuePair<string, Color>("Material_Snow", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Material_Clay", Color.FromArgb(255, 164, 168, 184)),
            new KeyValuePair<string, Color>("Material_Dirt", Color.FromArgb(255, 183, 106, 47)),
            new KeyValuePair<string, Color>("Material_Stone", Color.FromArgb(255, 112, 112, 112)),
            new KeyValuePair<string, Color>("Material_Water", Color.FromArgb(255, 64, 64, 255)),
            new KeyValuePair<string, Color>("Material_Wood", Color.FromArgb(255, 104, 83, 50)),
            new KeyValuePair<string, Color>("Material_Emerald", Color.FromArgb(255, 0, 217, 58)),
            new KeyValuePair<string, Color>("Material_Quartz", Color.FromArgb(255, 255, 252, 245)),
            new KeyValuePair<string, Color>("Material_Gold", Color.FromArgb(255, 250, 238, 77)),
            new KeyValuePair<string, Color>("Material_Diamond", Color.FromArgb(255, 92, 219, 213)),
            new KeyValuePair<string, Color>("Material_Lapis", Color.FromArgb(255, 74, 128, 255)),
            new KeyValuePair<string, Color>("Material_Podzol", Color.FromArgb(255, 129, 86, 49)),
            new KeyValuePair<string, Color>("Material_Nether", Color.FromArgb(255, 112, 2, 0)),
            new KeyValuePair<string, Color>("Material_Color_Orange", Color.FromArgb(255, 216, 127, 51)),
            new KeyValuePair<string, Color>("Material_Color_Magenta", Color.FromArgb(255, 178, 76, 216)),
            new KeyValuePair<string, Color>("Material_Color_Light_Blue", Color.FromArgb(255, 102, 153, 216)),
            new KeyValuePair<string, Color>("Material_Color_Yellow", Color.FromArgb(255, 229, 229, 51)),
            new KeyValuePair<string, Color>("Material_Color_Light_Green", Color.FromArgb(255, 127, 204, 25)),
            new KeyValuePair<string, Color>("Material_Color_Pink", Color.FromArgb(255, 242, 127, 165)),
            new KeyValuePair<string, Color>("Material_Color_Grey", Color.FromArgb(255, 76, 76, 76)),
            new KeyValuePair<string, Color>("Material_Color_Silver", Color.FromArgb(255, 153, 153, 153)),
            new KeyValuePair<string, Color>("Material_Color_Cyan", Color.FromArgb(255, 76, 127, 153)),
            new KeyValuePair<string, Color>("Material_Color_Purple", Color.FromArgb(255, 127, 63, 178)),
            new KeyValuePair<string, Color>("Material_Color_Blue", Color.FromArgb(255, 51, 76, 178)),
            new KeyValuePair<string, Color>("Material_Color_Brown", Color.FromArgb(255, 102, 76, 51)),
            new KeyValuePair<string, Color>("Material_Color_Green", Color.FromArgb(255, 102, 127, 51)),
            new KeyValuePair<string, Color>("Material_Color_Red", Color.FromArgb(255, 153, 51, 51)),
            new KeyValuePair<string, Color>("Material_Color_Black", Color.FromArgb(255, 25, 25, 25)),
            new KeyValuePair<string, Color>("Material_Terracotta_White", Color.FromArgb(255, 209, 177, 161)),
            new KeyValuePair<string, Color>("Material_Terracotta_Orange", Color.FromArgb(255, 159, 82, 36)),
            new KeyValuePair<string, Color>("Material_Terracotta_Magenta", Color.FromArgb(255, 149, 87, 108)),
            new KeyValuePair<string, Color>("Material_Terracotta_Light_Blue", Color.FromArgb(255, 112, 108, 138)),
            new KeyValuePair<string, Color>("Material_Terracotta_Yellow", Color.FromArgb(255, 186, 133, 36)),
            new KeyValuePair<string, Color>("Material_Terracotta_Light_Green", Color.FromArgb(255, 103, 117, 53)),
            new KeyValuePair<string, Color>("Material_Terracotta_Pink", Color.FromArgb(255, 160, 77, 78)),
            new KeyValuePair<string, Color>("Material_Terracotta_Grey", Color.FromArgb(255, 57, 41, 35)),
            new KeyValuePair<string, Color>("Material_Terracotta_Silver", Color.FromArgb(255, 135, 107, 98)),
            new KeyValuePair<string, Color>("Material_Terracotta_Cyan", Color.FromArgb(255, 87, 92, 92)),
            new KeyValuePair<string, Color>("Material_Terracotta_Purple", Color.FromArgb(255, 122, 73, 88)),
            new KeyValuePair<string, Color>("Material_Terracotta_Blue", Color.FromArgb(255, 76, 62, 92)),
            new KeyValuePair<string, Color>("Material_Terracotta_Brown", Color.FromArgb(255, 76, 50, 35)),
            new KeyValuePair<string, Color>("Material_Terracotta_Green", Color.FromArgb(255, 76, 82, 42)),
            new KeyValuePair<string, Color>("Material_Terracotta_Red", Color.FromArgb(255, 142, 60, 46)),
            new KeyValuePair<string, Color>("Material_Terracotta_Black", Color.FromArgb(255, 37, 22, 16)),
            new KeyValuePair<string, Color>("Particle_Note_00", Color.FromArgb(255, 89, 232, 0)),
            new KeyValuePair<string, Color>("Particle_Note_01", Color.FromArgb(255, 132, 206, 0)),
            new KeyValuePair<string, Color>("Particle_Note_02", Color.FromArgb(255, 172, 172, 0)),
            new KeyValuePair<string, Color>("Particle_Note_03", Color.FromArgb(255, 206, 132, 0)),
            new KeyValuePair<string, Color>("Particle_Note_04", Color.FromArgb(255, 232, 89, 0)),
            new KeyValuePair<string, Color>("Particle_Note_05", Color.FromArgb(255, 249, 46, 0)),
            new KeyValuePair<string, Color>("Particle_Note_06", Color.FromArgb(255, 255, 6, 6)),
            new KeyValuePair<string, Color>("Particle_Note_07", Color.FromArgb(255, 249, 0, 46)),
            new KeyValuePair<string, Color>("Particle_Note_08", Color.FromArgb(255, 232, 0, 89)),
            new KeyValuePair<string, Color>("Particle_Note_09", Color.FromArgb(255, 206, 0, 132)),
            new KeyValuePair<string, Color>("Particle_Note_10", Color.FromArgb(255, 172, 0, 172)),
            new KeyValuePair<string, Color>("Particle_Note_11", Color.FromArgb(255, 132, 0, 206)),
            new KeyValuePair<string, Color>("Particle_Note_12", Color.FromArgb(255, 89, 0, 232)),
            new KeyValuePair<string, Color>("Particle_Note_13", Color.FromArgb(255, 46, 0, 249)),
            new KeyValuePair<string, Color>("Particle_Note_14", Color.FromArgb(255, 6, 6, 254)),
            new KeyValuePair<string, Color>("Particle_Note_15", Color.FromArgb(255, 0, 46, 249)),
            new KeyValuePair<string, Color>("Particle_Note_16", Color.FromArgb(255, 0, 89, 232)),
            new KeyValuePair<string, Color>("Particle_Note_17", Color.FromArgb(255, 0, 132, 206)),
            new KeyValuePair<string, Color>("Particle_Note_18", Color.FromArgb(255, 0, 172, 172)),
            new KeyValuePair<string, Color>("Particle_Note_19", Color.FromArgb(255, 0, 206, 132)),
            new KeyValuePair<string, Color>("Particle_Note_20", Color.FromArgb(255, 0, 232, 89)),
            new KeyValuePair<string, Color>("Particle_Note_21", Color.FromArgb(255, 0, 249, 46)),
            new KeyValuePair<string, Color>("Particle_Note_22", Color.FromArgb(255, 6, 254, 6)),
            new KeyValuePair<string, Color>("Particle_Note_23", Color.FromArgb(255, 46, 249, 0)),
            new KeyValuePair<string, Color>("Particle_Note_24", Color.FromArgb(255, 89, 232, 0)),
            new KeyValuePair<string, Color>("Particle_NetherPortal", Color.FromArgb(255, 230, 77, 255)),
            new KeyValuePair<string, Color>("Particle_EnderPortal", Color.FromArgb(255, 76, 76, 76)),
            new KeyValuePair<string, Color>("Particle_Smoke", Color.FromArgb(255, 76, 76, 76)),
            new KeyValuePair<string, Color>("Particle_Ender", Color.FromArgb(255, 229, 76, 255)),
            new KeyValuePair<string, Color>("Particle_Explode", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Particle_HugeExplosion", Color.FromArgb(255, 153, 153, 153)),
            new KeyValuePair<string, Color>("Particle_DripWater", Color.FromArgb(255, 51, 76, 255)),
            new KeyValuePair<string, Color>("Particle_DripLavaStart", Color.FromArgb(255, 255, 255, 127)),
            new KeyValuePair<string, Color>("Particle_DripLavaEnd", Color.FromArgb(255, 255, 72, 21)),
            new KeyValuePair<string, Color>("Particle_EnchantmentTable", Color.FromArgb(255, 229, 229, 255)),
            new KeyValuePair<string, Color>("Particle_DragonBreathMin", Color.FromArgb(255, 183, 0, 210)),
            new KeyValuePair<string, Color>("Particle_DragonBreathMax", Color.FromArgb(255, 223, 0, 249)),
            new KeyValuePair<string, Color>("Particle_Suspend", Color.FromArgb(255, 102, 102, 178)),
            new KeyValuePair<string, Color>("Particle_CritStart", Color.FromArgb(255, 153, 153, 153)),
            new KeyValuePair<string, Color>("Particle_CritEnd", Color.FromArgb(255, 229, 229, 229)),
            new KeyValuePair<string, Color>("Effect_MovementSpeed", Color.FromArgb(255, 124, 175, 198)),
            new KeyValuePair<string, Color>("Effect_MovementSlowDown", Color.FromArgb(255, 90, 108, 129)),
            new KeyValuePair<string, Color>("Effect_DigSpeed", Color.FromArgb(255, 217, 192, 67)),
            new KeyValuePair<string, Color>("Effect_DigSlowdown", Color.FromArgb(255, 74, 66, 23)),
            new KeyValuePair<string, Color>("Effect_DamageBoost", Color.FromArgb(255, 147, 36, 35)),
            new KeyValuePair<string, Color>("Effect_Heal", Color.FromArgb(255, 248, 36, 35)),
            new KeyValuePair<string, Color>("Effect_Harm", Color.FromArgb(255, 67, 10, 9)),
            new KeyValuePair<string, Color>("Effect_Jump", Color.FromArgb(255, 34, 255, 76)),
            new KeyValuePair<string, Color>("Effect_Confusion", Color.FromArgb(255, 85, 29, 74)),
            new KeyValuePair<string, Color>("Effect_Regeneration", Color.FromArgb(255, 205, 92, 171)),
            new KeyValuePair<string, Color>("Effect_DamageResistance", Color.FromArgb(255, 153, 69, 58)),
            new KeyValuePair<string, Color>("Effect_FireResistance", Color.FromArgb(255, 228, 154, 58)),
            new KeyValuePair<string, Color>("Effect_WaterBreathing", Color.FromArgb(255, 46, 82, 153)),
            new KeyValuePair<string, Color>("Effect_Invisiblity", Color.FromArgb(255, 127, 131, 146)),
            new KeyValuePair<string, Color>("Effect_Blindness", Color.FromArgb(255, 31, 31, 35)),
            new KeyValuePair<string, Color>("Effect_NightVision", Color.FromArgb(255, 31, 31, 161)),
            new KeyValuePair<string, Color>("Effect_Hunger", Color.FromArgb(255, 88, 118, 83)),
            new KeyValuePair<string, Color>("Effect_Weakness", Color.FromArgb(255, 72, 77, 72)),
            new KeyValuePair<string, Color>("Effect_Poison", Color.FromArgb(255, 78, 147, 49)),
            new KeyValuePair<string, Color>("Effect_Wither", Color.FromArgb(255, 53, 42, 39)),
            new KeyValuePair<string, Color>("Effect_HealthBoost", Color.FromArgb(255, 248, 125, 35)),
            new KeyValuePair<string, Color>("Effect_Absorption", Color.FromArgb(255, 37, 82, 165)),
            new KeyValuePair<string, Color>("Effect_Saturation", Color.FromArgb(255, 248, 36, 35)),
            new KeyValuePair<string, Color>("Effect_Levitation", Color.FromArgb(255, 206, 255, 255)),
            new KeyValuePair<string, Color>("Effect_Luck", Color.FromArgb(255, 51, 153, 0)),
            new KeyValuePair<string, Color>("Effect_BadLuck", Color.FromArgb(255, 192, 164, 77)),
            new KeyValuePair<string, Color>("Effect_TurtleMaster", Color.FromArgb(255, 117, 91, 99)),
            new KeyValuePair<string, Color>("Effect_SlowFall", Color.FromArgb(255, 255, 239, 209)),
            new KeyValuePair<string, Color>("Effect_ConduitPower", Color.FromArgb(255, 124, 175, 198)),
            new KeyValuePair<string, Color>("Potion_BaseColour", Color.FromArgb(255, 56, 93, 198)),
            new KeyValuePair<string, Color>("Mob_Creeper_Colour1", Color.FromArgb(255, 13, 167, 11)),
            new KeyValuePair<string, Color>("Mob_Creeper_Colour2", Color.FromArgb(255, 0, 0, 0)),
            new KeyValuePair<string, Color>("Mob_Skeleton_Colour1", Color.FromArgb(255, 193, 193, 193)),
            new KeyValuePair<string, Color>("Mob_Skeleton_Colour2", Color.FromArgb(255, 73, 73, 73)),
            new KeyValuePair<string, Color>("Mob_Spider_Colour1", Color.FromArgb(255, 52, 45, 39)),
            new KeyValuePair<string, Color>("Mob_Spider_Colour2", Color.FromArgb(255, 168, 14, 14)),
            new KeyValuePair<string, Color>("Mob_Zombie_Colour1", Color.FromArgb(255, 0, 175, 175)),
            new KeyValuePair<string, Color>("Mob_Zombie_Colour2", Color.FromArgb(255, 121, 156, 101)),
            new KeyValuePair<string, Color>("Mob_Slime_Colour1", Color.FromArgb(255, 81, 160, 62)),
            new KeyValuePair<string, Color>("Mob_Slime_Colour2", Color.FromArgb(255, 126, 191, 110)),
            new KeyValuePair<string, Color>("Mob_Ghast_Colour1", Color.FromArgb(255, 249, 249, 249)),
            new KeyValuePair<string, Color>("Mob_Ghast_Colour2", Color.FromArgb(255, 188, 188, 188)),
            new KeyValuePair<string, Color>("Mob_PigZombie_Colour1", Color.FromArgb(255, 234, 147, 147)),
            new KeyValuePair<string, Color>("Mob_PigZombie_Colour2", Color.FromArgb(255, 76, 113, 41)),
            new KeyValuePair<string, Color>("Mob_Enderman_Colour1", Color.FromArgb(255, 22, 22, 22)),
            new KeyValuePair<string, Color>("Mob_Enderman_Colour2", Color.FromArgb(255, 0, 0, 0)),
            new KeyValuePair<string, Color>("Mob_CaveSpider_Colour1", Color.FromArgb(255, 12, 66, 78)),
            new KeyValuePair<string, Color>("Mob_CaveSpider_Colour2", Color.FromArgb(255, 168, 14, 14)),
            new KeyValuePair<string, Color>("Mob_Silverfish_Colour1", Color.FromArgb(255, 110, 110, 110)),
            new KeyValuePair<string, Color>("Mob_Silverfish_Colour2", Color.FromArgb(255, 48, 48, 48)),
            new KeyValuePair<string, Color>("Mob_Blaze_Colour1", Color.FromArgb(255, 246, 178, 1)),
            new KeyValuePair<string, Color>("Mob_Blaze_Colour2", Color.FromArgb(255, 255, 248, 126)),
            new KeyValuePair<string, Color>("Mob_LavaSlime_Colour1", Color.FromArgb(255, 52, 0, 0)),
            new KeyValuePair<string, Color>("Mob_LavaSlime_Colour2", Color.FromArgb(255, 252, 252, 0)),
            new KeyValuePair<string, Color>("Mob_Pig_Colour1", Color.FromArgb(255, 240, 165, 162)),
            new KeyValuePair<string, Color>("Mob_Pig_Colour2", Color.FromArgb(255, 219, 99, 95)),
            new KeyValuePair<string, Color>("Mob_Sheep_Colour1", Color.FromArgb(255, 231, 231, 231)),
            new KeyValuePair<string, Color>("Mob_Sheep_Colour2", Color.FromArgb(255, 255, 181, 181)),
            new KeyValuePair<string, Color>("Mob_Cow_Colour1", Color.FromArgb(255, 68, 54, 38)),
            new KeyValuePair<string, Color>("Mob_Cow_Colour2", Color.FromArgb(255, 161, 161, 161)),
            new KeyValuePair<string, Color>("Mob_Chicken_Colour1", Color.FromArgb(255, 161, 161, 161)),
            new KeyValuePair<string, Color>("Mob_Chicken_Colour2", Color.FromArgb(255, 255, 0, 0)),
            new KeyValuePair<string, Color>("Mob_Squid_Colour1", Color.FromArgb(255, 34, 59, 77)),
            new KeyValuePair<string, Color>("Mob_Squid_Colour2", Color.FromArgb(255, 112, 136, 153)),
            new KeyValuePair<string, Color>("Mob_Wolf_Colour1", Color.FromArgb(255, 215, 211, 211)),
            new KeyValuePair<string, Color>("Mob_Wolf_Colour2", Color.FromArgb(255, 206, 175, 150)),
            new KeyValuePair<string, Color>("Mob_MushroomCow_Colour1", Color.FromArgb(255, 160, 15, 16)),
            new KeyValuePair<string, Color>("Mob_MushroomCow_Colour2", Color.FromArgb(255, 183, 183, 183)),
            new KeyValuePair<string, Color>("Mob_Ocelot_Colour1", Color.FromArgb(255, 239, 222, 125)),
            new KeyValuePair<string, Color>("Mob_Ocelot_Colour2", Color.FromArgb(255, 86, 68, 52)),
            new KeyValuePair<string, Color>("Mob_Villager_Colour1", Color.FromArgb(255, 86, 60, 51)),
            new KeyValuePair<string, Color>("Mob_Villager_Colour2", Color.FromArgb(255, 189, 139, 114)),
            new KeyValuePair<string, Color>("Mob_Bat_Colour1", Color.FromArgb(255, 76, 62, 48)),
            new KeyValuePair<string, Color>("Mob_Bat_Colour2", Color.FromArgb(255, 15, 15, 15)),
            new KeyValuePair<string, Color>("Mob_Witch_Colour1", Color.FromArgb(255, 52, 0, 0)),
            new KeyValuePair<string, Color>("Mob_Witch_Colour2", Color.FromArgb(255, 81, 160, 62)),
            new KeyValuePair<string, Color>("Mob_Horse_Colour1", Color.FromArgb(255, 192, 158, 125)),
            new KeyValuePair<string, Color>("Mob_Horse_Colour2", Color.FromArgb(255, 238, 229, 0)),
            new KeyValuePair<string, Color>("Mob_Endermite_Color1", Color.FromArgb(255, 22, 22, 22)),
            new KeyValuePair<string, Color>("Mob_Endermite_Color2", Color.FromArgb(255, 110, 110, 110)),
            new KeyValuePair<string, Color>("Mob_Guardian_Color1", Color.FromArgb(255, 90, 130, 114)),
            new KeyValuePair<string, Color>("Mob_Guardian_Color2", Color.FromArgb(255, 241, 125, 48)),
            new KeyValuePair<string, Color>("Mob_Rabbit_Colour1", Color.FromArgb(255, 153, 95, 64)),
            new KeyValuePair<string, Color>("Mob_Rabbit_Colour2", Color.FromArgb(255, 115, 72, 49)),
            new KeyValuePair<string, Color>("Mob_PolarBear_Colour1", Color.FromArgb(255, 242, 242, 242)),
            new KeyValuePair<string, Color>("Mob_PolarBear_Colour2", Color.FromArgb(255, 149, 149, 144)),
            new KeyValuePair<string, Color>("Mob_Shulker_Colour1", Color.FromArgb(255, 148, 103, 148)),
            new KeyValuePair<string, Color>("Mob_Shulker_Colour2", Color.FromArgb(255, 77, 56, 82)),
            new KeyValuePair<string, Color>("Mob_Elder_Guardian_Colour1", Color.FromArgb(255, 206, 204, 186)),
            new KeyValuePair<string, Color>("Mob_Elder_Guardian_Colour2", Color.FromArgb(255, 116, 118, 147)),
            new KeyValuePair<string, Color>("Mob_Evocation_Illager_Colour1", Color.FromArgb(255, 149, 155, 155)),
            new KeyValuePair<string, Color>("Mob_Evocation_Illager_Colour2", Color.FromArgb(255, 30, 28, 26)),
            new KeyValuePair<string, Color>("Mob_Llama_Colour1", Color.FromArgb(255, 192, 158, 125)),
            new KeyValuePair<string, Color>("Mob_Llama_Colour2", Color.FromArgb(255, 153, 95, 64)),
            new KeyValuePair<string, Color>("Mob_Donkey_Colour1", Color.FromArgb(255, 83, 69, 57)),
            new KeyValuePair<string, Color>("Mob_Donkey_Colour2", Color.FromArgb(255, 134, 117, 102)),
            new KeyValuePair<string, Color>("Mob_Skeleton_Horse_Colour1", Color.FromArgb(255, 104, 104, 79)),
            new KeyValuePair<string, Color>("Mob_Skeleton_Horse_Colour2", Color.FromArgb(255, 229, 229, 216)),
            new KeyValuePair<string, Color>("Mob_Zombie_Horse_Colour1", Color.FromArgb(255, 49, 82, 52)),
            new KeyValuePair<string, Color>("Mob_Zombie_Horse_Colour2", Color.FromArgb(255, 151, 194, 132)),
            new KeyValuePair<string, Color>("Mob_Mule_Colour1", Color.FromArgb(255, 27, 2, 0)),
            new KeyValuePair<string, Color>("Mob_Mule_Colour2", Color.FromArgb(255, 81, 51, 29)),
            new KeyValuePair<string, Color>("Mob_Stray_Colour1", Color.FromArgb(255, 97, 118, 119)),
            new KeyValuePair<string, Color>("Mob_Stray_Colour2", Color.FromArgb(255, 221, 234, 234)),
            new KeyValuePair<string, Color>("Mob_Husk_Colour1", Color.FromArgb(255, 121, 112, 97)),
            new KeyValuePair<string, Color>("Mob_Husk_Colour2", Color.FromArgb(255, 230, 204, 148)),
            new KeyValuePair<string, Color>("Mob_Vex_Colour1", Color.FromArgb(255, 122, 144, 164)),
            new KeyValuePair<string, Color>("Mob_Vex_Colour2", Color.FromArgb(255, 232, 237, 241)),
            new KeyValuePair<string, Color>("Mob_Vindication_Illager_Colour1", Color.FromArgb(255, 149, 155, 155)),
            new KeyValuePair<string, Color>("Mob_Vindication_Illager_Colour2", Color.FromArgb(255, 39, 94, 97)),
            new KeyValuePair<string, Color>("Mob_Zombie_Villager_Colour1", Color.FromArgb(255, 86, 60, 51)),
            new KeyValuePair<string, Color>("Mob_Zombie_Villager_Colour2", Color.FromArgb(255, 121, 156, 101)),
            new KeyValuePair<string, Color>("Mob_Parrot_Colour1", Color.FromArgb(255, 13, 167, 11)),
            new KeyValuePair<string, Color>("Mob_Parrot_Colour2", Color.FromArgb(255, 255, 0, 0)),
            new KeyValuePair<string, Color>("Mob_Wither_Skeleton_Colour1", Color.FromArgb(255, 20, 20, 20)),
            new KeyValuePair<string, Color>("Mob_Wither_Skeleton_Colour2", Color.FromArgb(255, 71, 77, 77)),
            new KeyValuePair<string, Color>("Mob_Turtle_Colour1", Color.FromArgb(255, 231, 231, 231)),
            new KeyValuePair<string, Color>("Mob_Turtle_Colour2", Color.FromArgb(255, 0, 175, 175)),
            new KeyValuePair<string, Color>("Mob_Tropical_Colour1", Color.FromArgb(255, 239, 105, 21)),
            new KeyValuePair<string, Color>("Mob_Tropical_Colour2", Color.FromArgb(255, 255, 249, 239)),
            new KeyValuePair<string, Color>("Mob_Cod_Colour1", Color.FromArgb(255, 193, 167, 106)),
            new KeyValuePair<string, Color>("Mob_Cod_Colour2", Color.FromArgb(255, 229, 196, 139)),
            new KeyValuePair<string, Color>("Mob_Pufferfish_Colour1", Color.FromArgb(255, 246, 178, 1)),
            new KeyValuePair<string, Color>("Mob_Pufferfish_Colour2", Color.FromArgb(255, 55, 195, 242)),
            new KeyValuePair<string, Color>("Mob_Salmon_Colour1", Color.FromArgb(255, 160, 15, 16)),
            new KeyValuePair<string, Color>("Mob_Salmon_Colour2", Color.FromArgb(255, 14, 132, 116)),
            new KeyValuePair<string, Color>("Mob_Drowned_Colour1", Color.FromArgb(255, 143, 241, 215)),
            new KeyValuePair<string, Color>("Mob_Drowned_Colour2", Color.FromArgb(255, 121, 156, 101)),
            new KeyValuePair<string, Color>("Mob_Dolphin_Colour1", Color.FromArgb(255, 34, 59, 77)),
            new KeyValuePair<string, Color>("Mob_Dolphin_Colour2", Color.FromArgb(255, 249, 249, 249)),
            new KeyValuePair<string, Color>("Mob_Phantom_Colour1", Color.FromArgb(255, 67, 81, 138)),
            new KeyValuePair<string, Color>("Mob_Phantom_Colour2", Color.FromArgb(255, 136, 255, 0)),
            new KeyValuePair<string, Color>("Armour_Default_Leather_Colour", Color.FromArgb(255, 160, 101, 64)),
            new KeyValuePair<string, Color>("Under_Water_Clear_Colour", Color.FromArgb(255, 5, 5, 51)),
            new KeyValuePair<string, Color>("Under_Lava_Clear_Colour", Color.FromArgb(255, 153, 25, 0)),
            new KeyValuePair<string, Color>("In_Cloud_Base_Colour", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Under_Water_Fog_Colour", Color.FromArgb(255, 102, 102, 230)),
            new KeyValuePair<string, Color>("Under_Lava_Fog_Colour", Color.FromArgb(255, 102, 77, 77)),
            new KeyValuePair<string, Color>("In_Cloud_Fog_Colour", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Default_Fog_Colour", Color.FromArgb(255, 192, 216, 255)),
            new KeyValuePair<string, Color>("Nether_Fog_Colour", Color.FromArgb(255, 51, 8, 8)),
            new KeyValuePair<string, Color>("End_Fog_Colour", Color.FromArgb(255, 160, 128, 160)),
            new KeyValuePair<string, Color>("Sign_Text", Color.FromArgb(255, 0, 0, 0)),
            new KeyValuePair<string, Color>("Map_Text", Color.FromArgb(255, 0, 0, 0)),
            new KeyValuePair<string, Color>("Leash_Light_Colour", Color.FromArgb(255, 128, 102, 77)),
            new KeyValuePair<string, Color>("Leash_Dark_Colour", Color.FromArgb(255, 90, 71, 54)),
            new KeyValuePair<string, Color>("Fire_Overlay", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("BlockLight_OverworldDimension", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("BlockLight_HellDimension", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("BlockLight_EndDimension", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Banner_White", Color.FromArgb(255, 249, 255, 254)),
            new KeyValuePair<string, Color>("Banner_Orange", Color.FromArgb(255, 249, 128, 29)),
            new KeyValuePair<string, Color>("Banner_Magenta", Color.FromArgb(255, 199, 78, 189)),
            new KeyValuePair<string, Color>("Banner_Light_Blue", Color.FromArgb(255, 58, 179, 218)),
            new KeyValuePair<string, Color>("Banner_Yellow", Color.FromArgb(255, 254, 216, 61)),
            new KeyValuePair<string, Color>("Banner_Lime", Color.FromArgb(255, 128, 199, 31)),
            new KeyValuePair<string, Color>("Banner_Pink", Color.FromArgb(255, 243, 139, 170)),
            new KeyValuePair<string, Color>("Banner_Gray", Color.FromArgb(255, 71, 79, 82)),
            new KeyValuePair<string, Color>("Banner_Silver", Color.FromArgb(255, 157, 157, 151)),
            new KeyValuePair<string, Color>("Banner_Cyan", Color.FromArgb(255, 22, 156, 156)),
            new KeyValuePair<string, Color>("Banner_Purple", Color.FromArgb(255, 137, 50, 184)),
            new KeyValuePair<string, Color>("Banner_Blue", Color.FromArgb(255, 60, 68, 170)),
            new KeyValuePair<string, Color>("Banner_Brown", Color.FromArgb(255, 131, 84, 50)),
            new KeyValuePair<string, Color>("Banner_Green", Color.FromArgb(255, 94, 124, 22)),
            new KeyValuePair<string, Color>("Banner_Red", Color.FromArgb(255, 176, 46, 38)),
            new KeyValuePair<string, Color>("Banner_Black", Color.FromArgb(255, 29, 29, 33)),
            new KeyValuePair<string, Color>("Shulker_Box_Black", Color.FromArgb(255, 42, 42, 47)),
            new KeyValuePair<string, Color>("Shulker_Box_Blue", Color.FromArgb(255, 53, 55, 164)),
            new KeyValuePair<string, Color>("Shulker_Box_Brown", Color.FromArgb(255, 124, 76, 41)),
            new KeyValuePair<string, Color>("Shulker_Box_Cyan", Color.FromArgb(255, 34, 145, 156)),
            new KeyValuePair<string, Color>("Shulker_Box_Grey", Color.FromArgb(255, 75, 79, 82)),
            new KeyValuePair<string, Color>("Shulker_Box_Green", Color.FromArgb(255, 95, 121, 40)),
            new KeyValuePair<string, Color>("Shulker_Box_Light_Blue", Color.FromArgb(255, 49, 169, 226)),
            new KeyValuePair<string, Color>("Shulker_Box_Light_Green", Color.FromArgb(255, 116, 194, 24)),
            new KeyValuePair<string, Color>("Shulker_Box_Magenta", Color.FromArgb(255, 195, 65, 182)),
            new KeyValuePair<string, Color>("Shulker_Box_Orange", Color.FromArgb(255, 255, 118, 11)),
            new KeyValuePair<string, Color>("Shulker_Box_Pink", Color.FromArgb(255, 245, 137, 169)),
            new KeyValuePair<string, Color>("Shulker_Box_Purple", Color.FromArgb(255, 155, 110, 155)),
            new KeyValuePair<string, Color>("Shulker_Box_Red", Color.FromArgb(255, 168, 21, 19)),
            new KeyValuePair<string, Color>("Shulker_Box_Silver", Color.FromArgb(255, 153, 153, 145)),
            new KeyValuePair<string, Color>("Shulker_Box_White", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Shulker_Box_Yellow", Color.FromArgb(255, 255, 192, 43)),
            new KeyValuePair<string, Color>("Thermal_Lift", Color.FromArgb(255, 108, 122, 135)),
            new KeyValuePair<string, Color>("Thermal_Constant", Color.FromArgb(255, 161, 172, 161)),
            new KeyValuePair<string, Color>("Thermal_ConstantToHeight", Color.FromArgb(255, 186, 199, 207)),
            new KeyValuePair<string, Color>("Thermal_Speedboost", Color.FromArgb(255, 255, 210, 0)),
            new KeyValuePair<string, Color>("Thermal_Speedboost2", Color.FromArgb(255, 255, 133, 0)),
            new KeyValuePair<string, Color>("Particle_ScoreRing_Large", Color.FromArgb(255, 73, 214, 113)),
            new KeyValuePair<string, Color>("Particle_ScoreRing_Medium", Color.FromArgb(255, 223, 186, 37)),
            new KeyValuePair<string, Color>("Particle_ScoreRing_Small", Color.FromArgb(255, 99, 206, 202)),
            new KeyValuePair<string, Color>("Glide_Ghost_Slow", Color.FromArgb(255, 243, 21, 21)),
            new KeyValuePair<string, Color>("Glide_Ghost_Medium", Color.FromArgb(255, 255, 120, 0)),
            new KeyValuePair<string, Color>("Glide_Ghost_Fast", Color.FromArgb(255, 255, 222, 0)),
            new KeyValuePair<string, Color>("Bed_Black", Color.FromArgb(255, 43, 43, 47)),
            new KeyValuePair<string, Color>("Bed_Blue", Color.FromArgb(255, 61, 73, 175)),
            new KeyValuePair<string, Color>("Bed_Brown", Color.FromArgb(255, 120, 75, 43)),
            new KeyValuePair<string, Color>("Bed_Cyan", Color.FromArgb(255, 22, 141, 148)),
            new KeyValuePair<string, Color>("Bed_Grey", Color.FromArgb(255, 64, 69, 72)),
            new KeyValuePair<string, Color>("Bed_Green", Color.FromArgb(255, 86, 112, 26)),
            new KeyValuePair<string, Color>("Bed_Light_Blue", Color.FromArgb(255, 97, 204, 255)),
            new KeyValuePair<string, Color>("Bed_Lime", Color.FromArgb(255, 123, 218, 0)),
            new KeyValuePair<string, Color>("Bed_Magenta", Color.FromArgb(255, 236, 89, 223)),
            new KeyValuePair<string, Color>("Bed_Orange", Color.FromArgb(255, 255, 123, 20)),
            new KeyValuePair<string, Color>("Bed_Pink", Color.FromArgb(255, 255, 149, 182)),
            new KeyValuePair<string, Color>("Bed_Purple", Color.FromArgb(255, 155, 64, 215)),
            new KeyValuePair<string, Color>("Bed_Red", Color.FromArgb(255, 178, 33, 31)),
            new KeyValuePair<string, Color>("Bed_Silver", Color.FromArgb(255, 160, 160, 152)),
            new KeyValuePair<string, Color>("Bed_White", Color.FromArgb(255, 255, 255, 255)),
            new KeyValuePair<string, Color>("Bed_Yellow", Color.FromArgb(255, 255, 201, 68)),
            new KeyValuePair<string, Color>("Cauldron_Water", Color.FromArgb(255, 39, 53, 209)),
            new KeyValuePair<string, Color>("default", Color.FromArgb(255, 68, 175, 245)),
            new KeyValuePair<string, Color>("ice_plains", Color.FromArgb(255, 20, 85, 155)),
            new KeyValuePair<string, Color>("extreme_hills_mutated", Color.FromArgb(255, 14, 99, 171)),
            new KeyValuePair<string, Color>("mesa", Color.FromArgb(255, 78, 127, 129)),
            new KeyValuePair<string, Color>("mushroom_island_shore", Color.FromArgb(255, 129, 129, 147)),
            new KeyValuePair<string, Color>("lukewarm_ocean", Color.FromArgb(255, 13, 150, 219)),
            new KeyValuePair<string, Color>("taiga_mutated", Color.FromArgb(255, 30, 107, 130)),
            new KeyValuePair<string, Color>("savanna_plateau", Color.FromArgb(255, 37, 144, 168)),
            new KeyValuePair<string, Color>("deep_lukewarm_ocean", Color.FromArgb(255, 13, 150, 219)),
            new KeyValuePair<string, Color>("frozen_river", Color.FromArgb(255, 24, 83, 144)),
            new KeyValuePair<string, Color>("cold_beach", Color.FromArgb(255, 20, 99, 165)),
            new KeyValuePair<string, Color>("savanna", Color.FromArgb(255, 44, 139, 156)),
            new KeyValuePair<string, Color>("taiga", Color.FromArgb(255, 40, 112, 130)),
            new KeyValuePair<string, Color>("taiga_hills", Color.FromArgb(255, 35, 101, 131)),
            new KeyValuePair<string, Color>("warm_ocean", Color.FromArgb(255, 2, 176, 229)),
            new KeyValuePair<string, Color>("the_end", Color.FromArgb(255, 98, 82, 158)),
            new KeyValuePair<string, Color>("jungle_hills", Color.FromArgb(255, 27, 158, 216)),
            new KeyValuePair<string, Color>("mesa_plateau", Color.FromArgb(255, 85, 128, 158)),
            new KeyValuePair<string, Color>("birch_forest", Color.FromArgb(255, 6, 119, 206)),
            new KeyValuePair<string, Color>("plains", Color.FromArgb(255, 68, 175, 245)),
            new KeyValuePair<string, Color>("extreme_hills_plus_trees", Color.FromArgb(255, 14, 99, 171)),
            new KeyValuePair<string, Color>("flower_forest", Color.FromArgb(255, 32, 163, 204)),
            new KeyValuePair<string, Color>("deep_cold_ocean", Color.FromArgb(255, 32, 128, 201)),
            new KeyValuePair<string, Color>("extreme_hills", Color.FromArgb(255, 0, 123, 247)),
            new KeyValuePair<string, Color>("savanna_mutated", Color.FromArgb(255, 37, 144, 168)),
            new KeyValuePair<string, Color>("river", Color.FromArgb(255, 0, 132, 255)),
            new KeyValuePair<string, Color>("hell", Color.FromArgb(255, 144, 89, 87)),
            new KeyValuePair<string, Color>("cold_taiga_mutated", Color.FromArgb(255, 32, 94, 131)),
            new KeyValuePair<string, Color>("desert_hills", Color.FromArgb(255, 26, 122, 161)),
            new KeyValuePair<string, Color>("cold_taiga", Color.FromArgb(255, 32, 94, 131)),
            new KeyValuePair<string, Color>("forest", Color.FromArgb(255, 30, 151, 242)),
            new KeyValuePair<string, Color>("birch_forest_hills", Color.FromArgb(255, 10, 116, 196)),
            new KeyValuePair<string, Color>("cold_ocean", Color.FromArgb(255, 32, 128, 201)),
            new KeyValuePair<string, Color>("ice_plains_spikes", Color.FromArgb(255, 20, 85, 155)),
            new KeyValuePair<string, Color>("mesa_bryce", Color.FromArgb(255, 73, 127, 153)),
            new KeyValuePair<string, Color>("beach", Color.FromArgb(255, 21, 124, 171)),
            new KeyValuePair<string, Color>("swampland", Color.FromArgb(255, 76, 101, 89)),
            new KeyValuePair<string, Color>("jungle_edge", Color.FromArgb(255, 13, 138, 227)),
            new KeyValuePair<string, Color>("roofed_forest", Color.FromArgb(255, 59, 108, 209)),
            new KeyValuePair<string, Color>("ocean", Color.FromArgb(255, 23, 135, 212)),
            new KeyValuePair<string, Color>("forest_hills", Color.FromArgb(255, 5, 107, 209)),
            new KeyValuePair<string, Color>("mega_taiga", Color.FromArgb(255, 45, 109, 119)),
            new KeyValuePair<string, Color>("sunflower_plains", Color.FromArgb(255, 68, 175, 245)),
            new KeyValuePair<string, Color>("default", Color.FromArgb(255, 0, 137, 202)),
            new KeyValuePair<string, Color>("swampland_mutated", Color.FromArgb(255, 76, 97, 86)),
            new KeyValuePair<string, Color>("mushroom_island", Color.FromArgb(255, 138, 137, 151)),
            new KeyValuePair<string, Color>("stone_beach", Color.FromArgb(255, 13, 103, 187)),
            new KeyValuePair<string, Color>("jungle_mutated", Color.FromArgb(255, 27, 158, 216)),
            new KeyValuePair<string, Color>("desert", Color.FromArgb(255, 50, 165, 152)),
            new KeyValuePair<string, Color>("frozen_ocean", Color.FromArgb(255, 37, 112, 181)),
            new KeyValuePair<string, Color>("ice_mountains", Color.FromArgb(255, 17, 86, 167)),
            new KeyValuePair<string, Color>("mesa_plateau_stone", Color.FromArgb(255, 85, 128, 158)),
            new KeyValuePair<string, Color>("jungle", Color.FromArgb(255, 20, 162, 197)),
            new KeyValuePair<string, Color>("extreme_hills_plus_trees_mutated", Color.FromArgb(255, 14, 99, 171)),
            new KeyValuePair<string, Color>("deep_warm_ocean", Color.FromArgb(255, 2, 176, 229)),
            new KeyValuePair<string, Color>("deep_frozen_ocean", Color.FromArgb(255, 37, 112, 181)),
            new KeyValuePair<string, Color>("extreme_hills_edge", Color.FromArgb(255, 4, 92, 213)),
            new KeyValuePair<string, Color>("deep_ocean", Color.FromArgb(255, 23, 135, 212)),
            new KeyValuePair<string, Color>("cold_taiga_hills", Color.FromArgb(255, 36, 91, 120)),
            new KeyValuePair<string, Color>("mega_spruce_taiga", Color.FromArgb(255, 45, 109, 119)),
            new KeyValuePair<string, Color>("mega_taiga_hills", Color.FromArgb(255, 40, 99, 120))
        });


        private void Botón_Colores_Click(object sender, EventArgs e)
        {
            try
            {
                // Step 0 result: perfect.
                string Ruta_colours_col = Path.GetDirectoryName(Ruta_Xbox_360_DLC) + "\\colours.col";
                if (File.Exists(Ruta_colours_col))
                {
                    byte[] Matriz_Bytes = Program.Obtener_Matriz_Bytes_Archivo(Ruta_colours_col);
                    if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                    {
                        List<KeyValuePair<string, Color>> Lista_Nombres_Colores = new List<KeyValuePair<string, Color>>();
                        for (int Índice_Byte = 9, Bytes_Extra = 0; Índice_Byte < Matriz_Bytes.Length;)
                        {
                            try
                            {
                                byte Longitud = Matriz_Bytes[Índice_Byte];
                                if (Índice_Byte + Longitud >= Matriz_Bytes.Length) break; // End of file.
                                Índice_Byte++; // Skip the name length.
                                string Nombre = null;
                                for (int Índice_Caracter = 0; Índice_Caracter < Longitud; Índice_Caracter++)
                                {
                                    Nombre += (char)Matriz_Bytes[Índice_Byte + Índice_Caracter];
                                }
                                Índice_Byte += Longitud + 1;
                                Color Color_ARGB = Color.FromArgb(255, Matriz_Bytes[Índice_Byte + 0], Matriz_Bytes[Índice_Byte + 1], Matriz_Bytes[Índice_Byte + 2]);
                                Índice_Byte += 4;
                                if (string.Compare(Nombre, "Cauldron_Water", true) == 0)
                                {
                                    Bytes_Extra = 4; // Temporary fix for 4 unknown bytes only for this entry.
                                }
                                else if (string.Compare(Nombre, "default", true) == 0)
                                {
                                    Bytes_Extra = 8; // Temporary fix for 8 unknown bytes after each entry until the end.
                                }
                                Índice_Byte += Bytes_Extra;
                                Lista_Nombres_Colores.Add(new KeyValuePair<string, Color>(Nombre, Color_ARGB));
                            }
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                        }
                        if (Lista_Nombres_Colores.Count > 0)
                        {
                            string Texto = new string(' ', 8) + "internal static readonly List<KeyValuePair<string, Color>> Lista_Nombres_Colores = new List<KeyValuePair<string, Color>>(new KeyValuePair<string, Color>[]\r\n" + new string(' ', 8) + "{\r\n";
                            foreach (KeyValuePair<string, Color> Entrada in Lista_Nombres_Colores)
                            {
                                try
                                {
                                    Texto += new string(' ', 12) + "new KeyValuePair<string, Color>(\"" + Entrada.Key + "\", Color.FromArgb(255, " + Entrada.Value.R + ", " + Entrada.Value.G + ", " + Entrada.Value.B + ")),\r\n";
                                }
                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                            }
                            Texto += new string(' ', 8) + "});\r\n";
                            Clipboard.SetText(Texto);
                            Texto = null;
                        }
                        Matriz_Bytes = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Extraer_Click(object sender, EventArgs e)
        {
            try
            {
                Extraer_Recursos_Xbox_360_Minecraft_1_13(Ruta_Xbox_360_DLC); // Step 1 result: perfect.
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Indizar_Click(object sender, EventArgs e)
        {
            try
            {
                Generar_Diccionario_Recursos_Xbox_360(Ruta_PC + "\\Xbox_360_Minecraft_1_13\\Paths.txt"); // Step 2 result: perfect.
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Convertir_Click(object sender, EventArgs e)
        {
            try
            {
                //Extraer_Recursos_Xbox_360_Minecraft_1_13(Ruta_Xbox_360_DLC); // Step 1 result: perfect.
                //Generar_Diccionario_Recursos_Xbox_360(Ruta_PC + "\\Xbox_360_Minecraft_1_13\\Paths.txt"); // Step 2 result: perfect.
                Generar_Packs_Recursos_Xbox_360(Ruta_PC + "\\Xbox_360_Minecraft_1_13", Ruta_PC + "\\Resource Packs\\" + Program.Obtener_Nombre_Temporal()); // Step 3 result: ?.
                /*Botón_Extraer.Enabled = false;
                TextBox_Ruta.Enabled = false;
                Subproceso = new Thread(new ThreadStart(Subproceso_DoWork));
                Subproceso.IsBackground = true;
                Subproceso.Priority = ThreadPriority.Normal;
                Subproceso.Start();
                /*Subproceso = new BackgroundWorker();
                Subproceso.DoWork += Subproceso_DoWork;
                Subproceso.RunWorkerCompleted += Subproceso_RunWorkerCompleted;
                Subproceso.WorkerReportsProgress = false;
                Subproceso.WorkerSupportsCancellation = true;
                Subproceso.RunWorkerAsync();*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal int Extraer_Recursos_Recursivos(string Ruta, string Ruta_Salida, ref int Índice_Recurso, SortedDictionary<long, string> Diccionario_Índices_Rutas)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                {
                    FileStream Lector = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    if (Lector.Length >= 4L && Lector.Length < 2147483648L) // 2 GB.
                    {
                        byte[] Matriz_Bytes = new byte[Lector.Length]; // Load the whole file into memory.
                        Lector.Seek(0L, SeekOrigin.Begin);
                        int Longitud = Lector.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);
                        //if (Longitud < Matriz_Bytes.Length) Array.Resize(ref Matriz_Bytes, Longitud);
                        Lector.Close();
                        Lector.Dispose();
                        Lector = null;
                        if (Pendiente_Subproceso_Abortar) return 0; // Cancel safely before time.
                        int Total_Recursos = Extraer_Recursos_Máximos(Matriz_Bytes, Ruta_Salida, ref Índice_Recurso, 1, Diccionario_Índices_Rutas);
                        //int Total_Recursos = Extraer_Recursos_Recursivos(Matriz_Bytes, Ruta_Salida, ref Índice_Recurso, 1);
                        Matriz_Bytes = null;
                        GC.Collect(); // Recover RAM memory at the end.
                        GC.GetTotalMemory(true);
                        return Total_Recursos;
                    }
                    else // Ignore empty or too big files.
                    {
                        Lector.Close();
                        Lector.Dispose();
                        Lector = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return 0;
        }

        internal static bool Variable_Extraer_Tamaño_Máximo = false;

        /// <summary>
        /// Converts an image to a new bitmap, used to "delete" the possible excess of bytes from any
        /// found resource, and also make sure that the new image is only 24 or 32 bits with alpha.
        /// It can also "repair" some images that seem to have the red and blue colors swapped.
        /// </summary>
        /// <param name="Imagen_Original">Any valid image.</param>
        /// <param name="Invertir_RB">True if the red and blue colors have to be swapped again. False to keep the original colors.</param>
        /// <returns>Returns a new bitmap with the same contents of the original image, but with a common format. Returns null on any error.</returns>
        internal Bitmap Convertir_Imagen_Original(Image Imagen_Original, bool Invertir_RB)
        {
            try
            {
                if (Imagen_Original != null)
                {
                    int Ancho = Imagen_Original.Width;
                    int Alto = Imagen_Original.Height;
                    Bitmap Imagen = new Bitmap(Ancho, Alto, Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.HighQuality;
                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                    Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                    Pintar.Dispose();
                    Pintar = null;
                    // This new copy should have removed any excess bytes from the original image.
                    //if (Ruta_Salida.ToLowerInvariant().EndsWith("media")) // Some Xbox 360 images have R and B swapped.
                    if (Invertir_RB)
                    {
                        BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                        byte[] Matriz_Bytes_ARGB = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                        Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes_ARGB, 0, Matriz_Bytes_ARGB.Length);
                        int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                        int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                        byte Valor = 0; // Temporary value used to avoid overwriting the pixel values.
                        for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                        {
                            for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento) // Swap red and blue colors.
                            {
                                Valor = Matriz_Bytes_ARGB[Índice]; // Temporary value is blue.
                                Matriz_Bytes_ARGB[Índice] = Matriz_Bytes_ARGB[Índice + 2]; // Blue is red.
                                Matriz_Bytes_ARGB[Índice + 2] = Valor; // Red is temporary value (blue).
                            }
                        }
                        Marshal.Copy(Matriz_Bytes_ARGB, 0, Bitmap_Data.Scan0, Matriz_Bytes_ARGB.Length);
                        Imagen.UnlockBits(Bitmap_Data);
                        Bitmap_Data = null;
                        Matriz_Bytes_ARGB = null;
                    }
                    return Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        /// <summary>
        /// List with the known resource pack names of the Xbox 360 for Minecraft 1.13.
        /// </summary>
        internal static readonly List<string> Lista_Packs_Recursos = new List<string>(new string[]
        {
            "Adventure Time",
            "Candy",
            "Cartoon",
            "Chinese Mythology",
            "City",
            "Egyptian Mythology",
            "Fallout",
            "Fantasy",
            "Festive",
            "Greek Mythology",
            "Halloween",
            "Halloween 2015",
            "Halo",
            "Mass Effect",
            "Natural",
            "Norse Mythology",
            "Pattern",
            "Pirates Of The Caribbean",
            "Plastic",
            "Skyrim",
            "Steampunk",
            "Super Cute Texture Pack",
            "The Nightmare Before Christmas"
        });

        /// <summary>
        /// [2019_09_21_22_46_27_914] to [2019_09_23_00_32_48_774].
        /// New function capable of extracting the full names and the resources, now even
        /// images in TGA format, and this is specially difficult since those images
        /// don't contain a common header or "magic number". But this code (after several
        /// months of testings a lot of new functions disposed) ended up working as intended.
        /// Note: this code with a little modification to ignore the resource paths might also
        /// be used to extract images from any file like a database with thumbnails, etc.
        /// But for TGA images it shouldn't be used since it will only waste CPU. Luckily for
        /// the Xbox 360 resource files, I managed to get the resource names, and based on the
        /// resource pack names I know what resolution has each pack (Vanilla is 16x, and Natural
        /// is 32x for example), so I found that the Phantom and Drowned were saved in TGA format
        /// although the rest of the textures were in regular PNG, so I don't get why 4J Studios
        /// did this, but in those 2 examples the Vanilla textures would be 64 x 64 and in the
        /// Natural pack would be 128 x 128, so I made a code that "finds" the possible header of
        /// TGA images by looking it's dimensions of width and height. And it finally worked fine.
        /// But note that this code only works up to 255 of width or height, it would need to read
        /// 2 bytes to get a short value if the size is bigger than that. And note that the lower
        /// byte goes first in this case, so for "128" you should look for 2 bytes "128" and "0".
        /// </summary>
        /// <param name="Ruta_Entrada">Any valid file path that should contain a Xbox 360 packed resource file.</param>
        /// <param name="Ruta_Salida">Any valid directory path to extract the found resources in it.</param>
        /// <param name="Modo_Pack_Skins">If true the input file should be a "special" resource file with Minecraft skins inside.</param>
        /// <param name="Pack_Recursos_Skins">If true the input file should be a "full" resource pack file with Minecraft skins inside.</param>
        /// <returns>Returns the number of extracted resources or -1 on any error.</returns>
        internal int Extraer_Recursos_Archivo(string Ruta_Entrada, string Ruta_Salida, bool Modo_Pack_Skins, bool Pack_Recursos_Skins, string Nombre_Pack_Recursos)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta_Entrada) && File.Exists(Ruta_Entrada) && !string.IsNullOrEmpty(Ruta_Salida) && !Directory.Exists(Ruta_Salida))
                {
                    Program.Crear_Carpetas(Ruta_Salida);
                    FileStream Lector = new FileStream(Ruta_Entrada, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    // Start a file to keep track of any resources found in the Xbox 360 packed files.
                    FileStream Lector_Salida = new FileStream(Ruta_PC + "\\Resources.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    //Lector_Salida.SetLength(0L); // Reset each time?
                    Lector_Salida.Seek(Lector_Salida.Length, SeekOrigin.Begin); // Seek the end of the file.
                    StreamWriter Lector_Salida_Texto = new StreamWriter(Lector_Salida, Encoding.UTF8);

                    // Write the path of the current resource file.
                    Lector_Salida_Texto.Write('[' + Ruta_Entrada + ']');
                    Lector_Salida_Texto.WriteLine();

                    // Read the full resources file and load it into memory.
                    byte[] Matriz_Bytes = new byte[Lector.Length];
                    int Longitud_Total = Lector.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);
                    if (Matriz_Bytes.Length != Longitud_Total) Array.Resize(ref Matriz_Bytes, Longitud_Total);
                    Lector.Seek(0L, SeekOrigin.Begin);
                    int Longitud_Total_Máxima = Matriz_Bytes.Length.ToString().Length;

                    if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                    {
                        // Auto-detect the resolution of the possible current resource pack, to extract any
                        // possible TGA image "without" header inside the file with success.
                        int Resolución = -1; // Start by default the mode to ignore the TGA images.
                        string Nombre_Entrada = Path.GetFileNameWithoutExtension(Ruta_Entrada);
                        for (int Índice_Resolución = 16; Índice_Resolución <= 1024; Índice_Resolución *= 2)
                        {
                            if (string.Compare(Nombre_Entrada, "x" + Índice_Resolución.ToString() + "Data", true) == 0)
                            {
                                Resolución = Índice_Resolución;
                                break;
                            }
                        }

                        if (!Modo_Pack_Skins)
                        {
                            // Add each character that is not a control one into a new list.
                            List<char> Lista_Caracteres = new List<char>();
                            foreach (byte Valor in Matriz_Bytes)
                            {
                                char Caracter = (char)Valor;
                                if (!char.IsControl(Caracter)) // && (char.IsLetterOrDigit(Caracter) || char.IsPunctuation(Caracter) || char.IsSeparator(Caracter))) //|| Caracter == '\0'
                                {
                                    Lista_Caracteres.Add(Caracter);
                                }
                            }
                            //Matriz_Bytes = null;

                            // Convert the list of characters to a new big string.
                            string Texto = new string(Lista_Caracteres.ToArray());
                            Lista_Caracteres = null;

                            // Find any resource path to "res/".
                            List<int> Lista_Índices = new List<int>();
                            //foreach (char Caracter in Lista_Caracteres)
                            for (int Índice_Caracter = 0; ;)
                            {
                                int Índice = Texto.IndexOf("res/", Índice_Caracter);
                                if (Índice > -1)
                                {
                                    Lista_Índices.Add(Índice);
                                    Índice_Caracter = Índice + "res/".Length;
                                }
                                else break;
                            }

                            // Find any resource path to "textures/" to avoid errors.
                            List<int> Lista_Índices_Textures = new List<int>();
                            for (int Índice_Caracter = 0; ;)
                            {
                                int Índice = Texto.IndexOf("textures/", Índice_Caracter);
                                if (Índice > -1)
                                {
                                    Lista_Índices_Textures.Add(Índice);
                                    Índice_Caracter = Índice + "textures/".Length;
                                }
                                else break;
                            }

                            // Remove any index of "textures/", which ends like "res/" to avoid errors.
                            foreach (int Índice in Lista_Índices_Textures)
                            {
                                int Índice_Temporal = Índice + 5;
                                if (Lista_Índices.Contains(Índice_Temporal)) Lista_Índices.Remove(Índice_Temporal);
                            }

                            // Now add to a new list the full resource paths found.
                            List<string> Lista_Líneas = new List<string>();
                            if (Lista_Índices.Count > 0)
                            {
                                for (int Índice = 0; Índice < Lista_Índices.Count; Índice++)
                                {
                                    int Índice_Extensión = Texto.IndexOf('.', Lista_Índices[Índice]);
                                    if (Índice_Extensión > -1)
                                    {
                                        Índice_Extensión += 3; // ".png" or ".tga".
                                    }
                                    string Línea = Índice_Extensión < Texto.Length ? Texto.Substring(Lista_Índices[Índice], (Índice_Extensión - Lista_Índices[Índice]) + 1) : Texto.Substring(Lista_Índices[Índice]);
                                    if (!string.IsNullOrEmpty(Línea))
                                    {
                                        if (Lector_Salida.Length <= 104857600L) // 100 MB, avoid memory excess.
                                        {
                                            Lista_Líneas.Add(Línea);
                                            Lector_Salida_Texto.WriteLine(Línea);
                                            Lector.Flush();
                                        }
                                        else break;
                                    }
                                    /*string Línea = Índice + 1 < Lista_Índices.Count ? Texto.Substring(Lista_Índices[Índice], (Lista_Índices[Índice + 1] - Lista_Índices[Índice]) + 1) : Texto.Substring(Lista_Índices[Índice]);
                                    if (!string.IsNullOrEmpty(Línea))
                                    {
                                        int Índice_Extensión = Línea.IndexOf('.');
                                        if (Índice_Extensión > -1 && Índice_Extensión + 3 < Línea.Length)
                                        {
                                            Línea = Línea.Substring(0, Índice_Extensión + 3);
                                        }
                                        Lista_Líneas.Add(Línea);
                                    }*/
                                }
                                /*if (Lista_Líneas.Count > 0)
                                {
                                    foreach (string Línea in Lista_Líneas)
                                    {
                                        Lector_Salida_Texto.WriteLine(Línea);
                                        Lector.Flush();
                                    }
                                }*/
                            }
                            //Lista_Líneas = null;

                            // Add the unique resource paths found.
                            foreach (string Línea in Lista_Líneas)
                            {
                                if (!Lista_Global_Rutas_Recursos.Contains(Línea))
                                {
                                    Lista_Global_Rutas_Recursos.Add(Línea);
                                }
                            }

                            // First convert the TGA sizes into arrays of bytes to quickly find the possible TGA headers.
                            List<byte[]> Lista_Matrices_Bytes_TGA = null;
                            if (Resolución >= 16)
                            {
                                int Multiplicador = Resolución / 16;
                                List<Size> Lista_Dimensiones_TGA = new List<Size>();
                                foreach (string Línea in Lista_Líneas)
                                {
                                    string Nombre = Path.GetFileNameWithoutExtension(Línea);
                                    if (string.Compare(Nombre, "cavespider", true) == 0 ||
                                        string.Compare(Nombre, "enderman", true) == 0 ||
                                        string.Compare(Nombre, "fire", true) == 0 ||
                                        string.Compare(Nombre, "ghast_fire", true) == 0 ||
                                        string.Compare(Nombre, "lava", true) == 0 ||
                                        string.Compare(Nombre, "sheep", true) == 0 ||
                                        string.Compare(Nombre, "spider", true) == 0 ||
                                        string.Compare(Nombre, "wolf_tame", true) == 0)
                                    {
                                        Size Dimensiones = new Size(64 * Multiplicador, 32 * Multiplicador);
                                        if (!Lista_Dimensiones_TGA.Contains(Dimensiones))
                                        {
                                            Lista_Dimensiones_TGA.Add(Dimensiones); // 64 x 32.
                                        }
                                    }
                                    else if (string.Compare(Nombre, "phantom", true) == 0)
                                    {
                                        Size Dimensiones = new Size(64 * Multiplicador, 64 * Multiplicador);
                                        if (!Lista_Dimensiones_TGA.Contains(Dimensiones))
                                        {
                                            Lista_Dimensiones_TGA.Add(Dimensiones); // 64 x 64.
                                        }
                                    }
                                    else if (string.Compare(Nombre, "endergolem", true) == 0)
                                    {
                                        Size Dimensiones = new Size(64 * Multiplicador, 128 * Multiplicador);
                                        if (!Lista_Dimensiones_TGA.Contains(Dimensiones))
                                        {
                                            Lista_Dimensiones_TGA.Add(Dimensiones); // 64 x 128.
                                        }
                                    }
                                    else if (string.Compare(Nombre, "ender", true) == 0)
                                    {
                                        Size Dimensiones = new Size(256 * Multiplicador, 256 * Multiplicador);
                                        if (!Lista_Dimensiones_TGA.Contains(Dimensiones))
                                        {
                                            Lista_Dimensiones_TGA.Add(Dimensiones); // 256 x 256.
                                        }
                                    }
                                }
                                if (Lista_Dimensiones_TGA.Count > 0)
                                {
                                    Lista_Matrices_Bytes_TGA = new List<byte[]>();
                                    foreach (Size Dimensiones in Lista_Dimensiones_TGA)
                                    {
                                        byte[] Matriz_4_Bytes = new byte[4];
                                        BitConverter.GetBytes((short)Dimensiones.Width).CopyTo(Matriz_4_Bytes, 0);
                                        BitConverter.GetBytes((short)Dimensiones.Height).CopyTo(Matriz_4_Bytes, 2);
                                        Lista_Matrices_Bytes_TGA.Add(Matriz_4_Bytes);
                                    }
                                }
                            }

                            // There are resource paths found and the resource file length is 4 or more bytes.
                            if (/*Lista_Líneas.Count > 0 && */Matriz_Bytes != null && Matriz_Bytes.Length >= 9) // At least 9 bytes for the headers.
                            {
                                // TODO: add support for more "magic numbers" or headers here.
                                List<int> Lista_Marcadores_GIF_Apertura = new List<int>();
                                List<int> Lista_Marcadores_JPG_Apertura = new List<int>();
                                List<int> Lista_Marcadores_PNG_Apertura = new List<int>();
                                List<int> Lista_Marcadores_TGA_Apertura = new List<int>(); // This doesn't have a header.
                                for (int Índice_Byte = 0; Índice_Byte < Matriz_Bytes.Length; Índice_Byte++)
                                {
                                    if (Índice_Byte + 3 < Matriz_Bytes.Length &&
                                        Matriz_Bytes[Índice_Byte] == 71 &&
                                        Matriz_Bytes[Índice_Byte + 1] == 73 &&
                                        Matriz_Bytes[Índice_Byte + 2] == 70 &&
                                        Matriz_Bytes[Índice_Byte + 3] == 56 &&
                                        !Lista_Marcadores_GIF_Apertura.Contains(Índice_Byte))
                                    {
                                        Lista_Marcadores_GIF_Apertura.Add(Índice_Byte);
                                    }
                                    if (Índice_Byte + 2/*3*/ < Matriz_Bytes.Length &&
                                        Matriz_Bytes[Índice_Byte] == 255 &&
                                        Matriz_Bytes[Índice_Byte + 1] == 216 &&
                                        Matriz_Bytes[Índice_Byte + 2] == 255 &&
                                        /*Matriz_Bytes_Búfer[Índice_Byte + 3] >= 128
                                        /*224 && Matriz_Bytes_Búfer[Índice_Byte + 3] <= 239 && */
                                        !Lista_Marcadores_JPG_Apertura.Contains(Índice_Byte))
                                    {
                                        Lista_Marcadores_JPG_Apertura.Add(Índice_Byte);
                                    }
                                    if (Índice_Byte + 3 < Matriz_Bytes.Length &&
                                        Matriz_Bytes[Índice_Byte] == 137 &&
                                        Matriz_Bytes[Índice_Byte + 1] == 80 &&
                                        Matriz_Bytes[Índice_Byte + 2] == 78 &&
                                        Matriz_Bytes[Índice_Byte + 3] == 71 &&
                                        !Lista_Marcadores_PNG_Apertura.Contains(Índice_Byte))
                                    {
                                        Lista_Marcadores_PNG_Apertura.Add(Índice_Byte);
                                    }
                                    // Trick to find the start of a TGA file, then subtract 8 bytes to get the actual start.
                                    if (Lista_Matrices_Bytes_TGA != null && Lista_Matrices_Bytes_TGA.Count > 0)
                                    {
                                        foreach (byte[] Matriz_4_Bytes in Lista_Matrices_Bytes_TGA)
                                        {
                                            // Search for 4 consecutive bytes that are set to zero, that
                                            // are the X and Y positions in the TGa image, then search for
                                            // anotehr 4 bytes that store one of the actual width and height
                                            // dimensions and finally look for another byte that should be
                                            // either 8, 16, 24 or 32, the pixel depths supported by the
                                            // Targa library used in this application. Hopefully this will
                                            // help a lot to find only valid TGA headers, while avoiding any
                                            // "false positive" findings, which would infinitely slow the whole
                                            // process of trying to load the TGA images to save them later on.
                                            if (Índice_Byte + 8 < Matriz_Bytes.Length &&
                                                Matriz_Bytes[Índice_Byte] == 0 &&
                                                Matriz_Bytes[Índice_Byte + 1] == 0 &&
                                                Matriz_Bytes[Índice_Byte + 2] == 0 &&
                                                Matriz_Bytes[Índice_Byte + 3] == 0 &&
                                                Matriz_Bytes[Índice_Byte + 4] == Matriz_4_Bytes[0] &&
                                                Matriz_Bytes[Índice_Byte + 5] == Matriz_4_Bytes[1] &&
                                                Matriz_Bytes[Índice_Byte + 6] == Matriz_4_Bytes[2] &&
                                                Matriz_Bytes[Índice_Byte + 7] == Matriz_4_Bytes[3] &&
                                                (Matriz_Bytes[Índice_Byte + 8] == 8 ||
                                                Matriz_Bytes[Índice_Byte + 8] == 16 ||
                                                Matriz_Bytes[Índice_Byte + 8] == 24 ||
                                                Matriz_Bytes[Índice_Byte + 8] == 32) &&
                                                !Lista_Marcadores_TGA_Apertura.Contains(Índice_Byte - 8))
                                            {
                                                Lista_Marcadores_TGA_Apertura.Add(Índice_Byte - 8);
                                            }
                                        }
                                    }
                                }

                                List<int> Lista_Índices_Apertura = new List<int>();
                                if (Ruta_Entrada.Contains("\\media")) // Here try to export everything.
                                {
                                    Lista_Índices_Apertura.AddRange(Lista_Marcadores_GIF_Apertura); // 2020_02_07_21_13_34_685.
                                    Lista_Índices_Apertura.AddRange(Lista_Marcadores_JPG_Apertura); // 2020_02_07_21_13_38_509.
                                }
                                Lista_Índices_Apertura.AddRange(Lista_Marcadores_PNG_Apertura);
                                //Lista_Índices_Apertura.AddRange(Lista_Marcadores_TGA_Apertura);
                                Lista_Índices_Apertura.Sort(); // Sort to avoid errors with the paths list.

                                // First get the file paths with a TGA extension and move them to a new list.
                                List<string> Lista_Líneas_TGA = new List<string>();
                                for (int Índice_Línea = Lista_Líneas.Count - 1; Índice_Línea >= 0; Índice_Línea--)
                                {
                                    if (string.Compare(Path.GetExtension(Lista_Líneas[Índice_Línea]), ".tga", true) == 0)
                                    {
                                        Lista_Líneas_TGA.Insert(0, Lista_Líneas[Índice_Línea]);
                                        Lista_Líneas.RemoveAt(Índice_Línea);
                                    }
                                }

                                // Now extract all the found resources except for the TGA ones.
                                //bool Variable_Extraer_Tamaño_Máximo = false;
                                for (int Índice_Apertura = 0; Índice_Apertura < Lista_Índices_Apertura.Count; Índice_Apertura++)
                                {
                                    byte[] Matriz_Bytes_Recurso = null;
                                    Image Imagen_Original = null;
                                    int Índice_Formato_Cierre = -1;
                                    if (Índice_Apertura + 1 < Lista_Índices_Apertura.Count)
                                    {
                                        Índice_Formato_Cierre = Lista_Índices_Apertura[Índice_Apertura + 1];
                                    }
                                    else Índice_Formato_Cierre = Matriz_Bytes.Length;
                                    //Matriz_Bytes_Recurso = new byte[!Variable_Extraer_Tamaño_Máximo ? Índice_Formato_Cierre - Lista_Índices_Apertura[Índice_Apertura] : Matriz_Bytes.Length - Lista_Índices_Apertura[Índice_Apertura]];
                                    //Array.Copy(Matriz_Bytes, Lista_Índices_Apertura[Índice_Apertura], Matriz_Bytes_Recurso, 0, Matriz_Bytes_Recurso.Length);
                                    Matriz_Bytes_Recurso = new byte[Math.Min(Matriz_Bytes.Length - Lista_Índices_Apertura[Índice_Apertura], Tamaño_100_MB)];
                                    Array.Copy(Matriz_Bytes, Lista_Índices_Apertura[Índice_Apertura], Matriz_Bytes_Recurso, 0, Matriz_Bytes_Recurso.Length);
                                    try { Imagen_Original = Image.FromStream(new MemoryStream(Matriz_Bytes_Recurso, true), false, false); }
                                    catch { Imagen_Original = null; }
                                    if (Imagen_Original != null)
                                    {
                                        bool Invertir_RB = false;
                                        try
                                        {
                                            if (Ruta_Entrada.Contains("\\media")) // Here try to export everything.
                                            {
                                                ImageFormat Formato_Original = Imagen_Original.RawFormat;
                                                if (Formato_Original == ImageFormat.Bmp) Invertir_RB = false;
                                                else if (Formato_Original == ImageFormat.Emf) Invertir_RB = false;
                                                else if (Formato_Original == ImageFormat.Exif) Invertir_RB = false;
                                                else if (Formato_Original == ImageFormat.Gif) Invertir_RB = false;
                                                else if (Formato_Original == ImageFormat.Icon) Invertir_RB = false;
                                                else if (Formato_Original == ImageFormat.Jpeg) Invertir_RB = false;
                                                else if (Formato_Original == ImageFormat.MemoryBmp) Invertir_RB = false;
                                                else if (Formato_Original == ImageFormat.Png) Invertir_RB = true; // R and B channels need to be swapped.
                                                else if (Formato_Original == ImageFormat.Tiff) Invertir_RB = false;
                                                else if (Formato_Original == ImageFormat.Wmf) Invertir_RB = false;
                                                else // Unknown image format, so try to guess it here.
                                                {
                                                    ImageCodecInfo Codificador = Program.Obtener_Imagen_Codificador_Guid(Formato_Original.Guid);
                                                    if (Codificador != null)
                                                    {
                                                        string Texto_Formato = Codificador.FormatDescription;
                                                        if (string.Compare(Texto_Formato, "JPEG", true) == 0) Invertir_RB = false;
                                                        else if (string.Compare(Texto_Formato, "PNG", true) == 0) Invertir_RB = true; // R and B channels need to be swapped.
                                                        Codificador = null;
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Invertir_RB = false; }
                                        Bitmap Imagen = Convertir_Imagen_Original(Imagen_Original, Invertir_RB/*string.Compare(Path.GetFileNameWithoutExtension(Ruta_Entrada), "media", true) == 0*/);
                                        if (Imagen != null)
                                        {
                                            string Número = Lista_Índices_Apertura[Índice_Apertura].ToString();
                                            while (Número.Length < Longitud_Total_Máxima) Número = '0' + Número;
                                            string Ruta = Ruta_Salida + "\\" + (Índice_Apertura < Lista_Líneas.Count ? Path.GetFileNameWithoutExtension(Lista_Líneas[Índice_Apertura]) : Número);
                                            while (File.Exists(Ruta + ".png")) Ruta += '_'; // Avoid overwriting the files.
                                            Imagen.Save(Ruta + ".png", ImageFormat.Png);
                                            Imagen.Dispose();
                                            Imagen = null;
                                        }
                                        Imagen_Original.Dispose();
                                        Imagen_Original = null;
                                    }
                                }

                                // Finally try to load, convert and export any TGA images.
                                // This was very hard to think of, since TGa images don't have a common header...
                                // So this code works based on the known image sizes for each texture and resource pack.
                                if (Lista_Marcadores_TGA_Apertura.Count > 0)
                                {
                                    // Now try to extract the possible TGA images from the byte array.
                                    for (int Índice_TGA = 0; Índice_TGA < Lista_Marcadores_TGA_Apertura.Count; Índice_TGA++)
                                    {
                                        int Índice_Máximo = -1;
                                        for (int Índice_PNG = 0; Índice_PNG < Lista_Índices_Apertura.Count; Índice_PNG++)
                                        {
                                            if (Lista_Índices_Apertura[Índice_PNG] > Lista_Marcadores_TGA_Apertura[Índice_TGA])
                                            {
                                                Índice_Máximo = Lista_Índices_Apertura[Índice_PNG];
                                                break;
                                            }
                                        }
                                        if (Índice_Máximo > -1)
                                        {
                                            int Índice_Inicio = Lista_Marcadores_TGA_Apertura[Índice_TGA]; // Already subtracted 8 when adding this index.
                                            for (int Índice_Fin = Índice_Máximo; Índice_Fin > Índice_Inicio; Índice_Fin--)
                                            {
                                                byte[] Matriz_Bytes_Recurso = new byte[Índice_Fin - Índice_Inicio];
                                                Array.Copy(Matriz_Bytes, Índice_Inicio, Matriz_Bytes_Recurso, 0, Matriz_Bytes_Recurso.Length);
                                                MemoryStream Lector_Memoria = new MemoryStream(Matriz_Bytes_Recurso, true);
                                                Image Imagen_Original = null;
                                                try { Imagen_Original = TargaImage.LoadTargaImage(Lector_Memoria); } // Try to load as TGA image.
                                                catch { Imagen_Original = null; }
                                                if (Imagen_Original != null && Imagen_Original.Width > 0 && Imagen_Original.Height > 0 && Imagen_Original.Width <= 1024 && Imagen_Original.Height <= 1024)
                                                {
                                                    // New TGA resource found.
                                                    Bitmap Imagen = Convertir_Imagen_Original(Imagen_Original, string.Compare(Path.GetFileNameWithoutExtension(Ruta_Entrada), "media", true) == 0);
                                                    if (Imagen != null)
                                                    {
                                                        string Número = Lista_Marcadores_TGA_Apertura[Índice_TGA].ToString();
                                                        while (Número.Length < Longitud_Total_Máxima) Número = '0' + Número;
                                                        string Ruta = Ruta_Salida + "\\" + (Índice_TGA < Lista_Líneas_TGA.Count ? Path.GetFileNameWithoutExtension(Lista_Líneas_TGA[Índice_TGA]) : Número);
                                                        while (File.Exists(Ruta + ".png")) Ruta += '_'; // Avoid overwriting the files.
                                                        Imagen.Save(Ruta + ".png", ImageFormat.Png);
                                                        Imagen.Dispose();
                                                        Imagen = null;
                                                    }
                                                    Imagen_Original.Dispose();
                                                    Imagen_Original = null;
                                                    Lector_Memoria.Close();
                                                    Lector_Memoria.Dispose();
                                                    Lector_Memoria = null;
                                                    break;
                                                }
                                                Lector_Memoria.Close();
                                                Lector_Memoria.Dispose();
                                                Lector_Memoria = null;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else// if (Modo_Pack_Skins) // Try to extract skins from this packed resource file.
                        {
                            // Test to extract all the skin names in English or "en-EN":
                            if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                            {
                                // I managed to fully decode the Xbox 360 skin packed files.
                                List<string> Lista_Nombres_en_EN = new List<string>();
                                List<int> Lista_Índices_en_EN = Buscar_Matriz_Bytes_Archivo(Matriz_Bytes, new byte[5] { (byte)'e', (byte)'n', (byte)'-', (byte)'E', (byte)'N' });
                                List<int> Lista_Índices_en_GB = Buscar_Matriz_Bytes_Archivo(Matriz_Bytes, new byte[5] { (byte)'e', (byte)'n', (byte)'-', (byte)'G', (byte)'B' });
                                if (Lista_Índices_en_EN != null && Lista_Índices_en_GB != null && Lista_Índices_en_EN.Count > 0 && Lista_Índices_en_GB.Count > 0) // It should have 2 matches for each one.
                                {
                                    int Índice_Inicio = Lista_Índices_en_EN[Lista_Índices_en_EN.Count - 1]; // Use the last one.
                                    int Índice_Fin = Lista_Índices_en_GB[Lista_Índices_en_GB.Count - 1]; // Use the last one.
                                    // Skip 5 bytes for "en-EN" and then 4 bytes possibly for total length.
                                    byte[] Matriz_2_Bytes = new byte[2];
                                    for (int Índice_Byte = Índice_Inicio + 5 + 4; Índice_Byte < Índice_Fin;)
                                    {
                                        // First read the next 2 bytes to get the length of the next stored string.
                                        Matriz_2_Bytes[0] = Matriz_Bytes[Índice_Byte + 1]; // Use swapped byte order.
                                        Matriz_2_Bytes[1] = Matriz_Bytes[Índice_Byte];
                                        short Longitud = BitConverter.ToInt16(Matriz_2_Bytes, 0);
                                        Índice_Byte += 2; // Increase the byte index by 2.
                                        if (Longitud > 0 && Índice_Byte + Longitud <= Índice_Fin)
                                        {
                                            byte[] Matriz_Bytes_Nombre = new byte[Longitud];
                                            Array.Copy(Matriz_Bytes, Índice_Byte, Matriz_Bytes_Nombre, 0, Matriz_Bytes_Nombre.Length);
                                            string Nombre = Encoding.UTF8.GetString(Matriz_Bytes_Nombre);
                                            Lista_Nombres_en_EN.Add(Nombre);
                                            Matriz_Bytes_Nombre = null;
                                            /*List<char> Lista_Caracteres = new List<char>();
                                            for (int Índice_Caracter = 0; Índice_Caracter < Longitud; Índice_Caracter++)
                                            {
                                                Lista_Caracteres.Add((char)Matriz_Bytes[Índice_Byte + Índice_Caracter]);
                                            }
                                            Lista_Nombres.Add(new string(Lista_Caracteres.ToArray()));
                                            Lista_Caracteres = null;*/
                                            Índice_Byte += Longitud; // Increase the byte index by the string length.
                                        }
                                    }
                                    /*if (Lista_Nombres_en_EN.Count > 0)
                                    {
                                        // Test results: I got all the names with 100 % of success, but sadly the names
                                        // don't have the same order as the images and also there are several strings
                                        // that describe some copyrights of the skins or the "category" of them.
                                        // So I'll have to look after each full image by searching "‰PNG" and before the
                                        // next one to get the actual name of the skins. This will be tricky to get right.
                                        // Update: I finished the below functions to get all the names, so now I'm using this
                                        // to get the names in the right order once the skins have been saved by renaming them.
                                    }
                                    /*List<char> Lista_Caracteres = new List<char>();
                                    for (int Índice_Byte = Índice_Inicio; Índice_Byte < Índice_Fin; Índice_Byte++)
                                    {
                                        char Caracter = (char)Matriz_Bytes[Índice_Byte];
                                        if (!char.IsControl(Caracter) || Caracter == '\0')
                                        {
                                            Lista_Caracteres.Add(Caracter);
                                        }
                                    }
                                    if (Lista_Caracteres.Count > 0)
                                    {
                                        string Texto = new string(Lista_Caracteres.ToArray());
                                        string[] Matriz_Nombres = Texto.Split("\0".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                        if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                                        {
                                            List<string> Lista_Nombres = new List<string>();
                                            for (int Índice_Nombre = 0; Índice_Nombre < Matriz_Nombres.Length; Índice_Nombre += 2)
                                            {
                                                Lista_Nombres.Add(Matriz_Nombres[Índice_Nombre]);
                                            }
                                            if (Lista_Nombres.Count > 0)
                                            {
                                                ;
                                            }
                                        }
                                        Matriz_Nombres = null;
                                    }
                                    Lista_Caracteres = null;*/
                                }
                                Lista_Índices_en_EN = null;
                                Lista_Índices_en_GB = null;

                                // So this in now the second try to get the actual skin names with the same order.
                                // Try to search for 2 Unicode strings that seem to be after each PNG image.
                                // It looks that (almost) all names have before them this string "IEND®B`‚".
                                //List<int> Lista_Índices_IEND_B__ = Buscar_Matriz_Bytes_Archivo(Matriz_Bytes, new byte[8] { 73, 69, 78, 68, 174, 66, 96, 130 });
                                //List<int> Lista_Índices_DISPLAYNAME = Buscar_Matriz_Bytes_Archivo(Matriz_Bytes, new byte[22] { (byte)'D', 0, (byte)'I', 0, (byte)'S', 0, (byte)'P', 0, (byte)'L', 0, (byte)'A', 0, (byte)'Y', 0, (byte)'N', 0, (byte)'A', 0, (byte)'M', 0, (byte)'E', 0 });
                                List<int> Lista_Índices__PNG = Buscar_Matriz_Bytes_Archivo(Matriz_Bytes, new byte[4] { 137, 80, 78, 71 });
                                // This list is for single skin packs.
                                List<int> Lista_Índices_IDS_DLCSKIN = Buscar_Matriz_Bytes_Archivo(Matriz_Bytes, new byte[23] { 31, 0, (byte)'I', 0, (byte)'D', 0, (byte)'S', 0, (byte)'_', 0, (byte)'D', 0, (byte)'L', 0, (byte)'C', 0, (byte)'S', 0, (byte)'K', 0, (byte)'I', 0, (byte)'N'/*, 0, (byte)'0', 0, (byte)'0', 0, (byte)'0', 0, (byte)'0'*/ });
                                // This list is for skin packs that are a part of a full resource pack.
                                List<int> Lista_Índices_IDS_SKINS_DLCSKIN = Buscar_Matriz_Bytes_Archivo(Matriz_Bytes, new byte[35] { 37, 0, (byte)'I', 0, (byte)'D', 0, (byte)'S', 0, (byte)'_', 0, (byte)'S', 0, (byte)'K', 0, (byte)'I', 0, (byte)'N', 0, (byte)'S', 0, (byte)'_', 0, (byte)'D', 0, (byte)'L', 0, (byte)'C', 0, (byte)'S', 0, (byte)'K', 0, (byte)'I', 0, (byte)'N'/*, 0, (byte)'0', 0, (byte)'0', 0, (byte)'0', 0, (byte)'0'*/ });

                                List<int> Lista_Índices_skin_FromTheShadowsSkinPack_ = Buscar_Matriz_Bytes_Archivo(Matriz_Bytes, new byte[55] { (byte)'s', 0, (byte)'k', 0, (byte)'i', 0, (byte)'n', 0, (byte)'.', 0, (byte)'F', 0, (byte)'r', 0, (byte)'o', 0, (byte)'m', 0, (byte)'T', 0, (byte)'h', 0, (byte)'e', 0, (byte)'S', 0, (byte)'h', 0, (byte)'a', 0, (byte)'d', 0, (byte)'o', 0, (byte)'w', 0, (byte)'s', 0, (byte)'S', 0, (byte)'k', 0, (byte)'i', 0, (byte)'n', 0, (byte)'P', 0, (byte)'a', 0, (byte)'c', 0, (byte)'k', 0, (byte)'.' });

                                List<int> Lista_Índices_skin_TheNightmareBeforeChristmas_ = Buscar_Matriz_Bytes_Archivo(Matriz_Bytes, new byte[65] { (byte)'s', 0, (byte)'k', 0, (byte)'i', 0, (byte)'n', 0, (byte)'.', 0, (byte)'T', 0, (byte)'h', 0, (byte)'e', 0, (byte)'N', 0, (byte)'i', 0, (byte)'g', 0, (byte)'h', 0, (byte)'t', 0, (byte)'m', 0, (byte)'a', 0, (byte)'r', 0, (byte)'e', 0, (byte)'B', 0, (byte)'e', 0, (byte)'f', 0, (byte)'o', 0, (byte)'r', 0, (byte)'e', 0, (byte)'C', 0, (byte)'h', 0, (byte)'r', 0, (byte)'i', 0, (byte)'s', 0, (byte)'t', 0, (byte)'m', 0, (byte)'a', 0, (byte)'s', 0, (byte)'.' });

                                // WARNING: "From The Shadows Skin Pack" is failing to get the names.
                                // And also "The Nightmare Before Christmas". So this should fix it.
                                bool Modo_skin_FromTheShadowsSkinPack_ = false; // Add 55 bytes.
                                bool Modo_skin_TheNightmareBeforeChristmas_ = false; // Add 65 bytes.

                                if (Lista_Índices_IDS_DLCSKIN == null || Lista_Índices_IDS_DLCSKIN.Count <= 0) // Replace the list.
                                {
                                    if (Lista_Índices_IDS_SKINS_DLCSKIN != null && Lista_Índices_IDS_SKINS_DLCSKIN.Count > 0)
                                    {
                                        Lista_Índices_IDS_DLCSKIN = Lista_Índices_IDS_SKINS_DLCSKIN;
                                    }
                                    else if (Lista_Índices_skin_FromTheShadowsSkinPack_ != null && Lista_Índices_skin_FromTheShadowsSkinPack_.Count > 0)
                                    {
                                        Lista_Índices_IDS_DLCSKIN = Lista_Índices_skin_FromTheShadowsSkinPack_;
                                        Modo_skin_FromTheShadowsSkinPack_ = true;
                                    }
                                    else if (Lista_Índices_skin_TheNightmareBeforeChristmas_ != null && Lista_Índices_skin_TheNightmareBeforeChristmas_.Count > 0)
                                    {
                                        Lista_Índices_IDS_DLCSKIN = Lista_Índices_skin_TheNightmareBeforeChristmas_;
                                        Modo_skin_TheNightmareBeforeChristmas_ = true;
                                    }
                                }
                                int Modo_Pack_Recursos_Skins = 0;
                                if (Pack_Recursos_Skins) // This makes sure it's always correct from outside this function.
                                {
                                    // Assume we have a skin pack that is part of a "full" resource pack.
                                    Modo_Pack_Recursos_Skins = -2; // Used to subtract 2 images in the skin index.
                                    if (!string.IsNullOrEmpty(Nombre_Pack_Recursos) &&
                                        string.Compare(Nombre_Pack_Recursos, "Chinese Mythology", true) == 0)
                                    {
                                        Modo_Pack_Recursos_Skins--; // This pack has an extra image, so keep in sync.
                                    }
                                }
                                // Tested with "Skin Pack 6".
                                // The first test results, it should work, but sometimes there are images with
                                // capes or other things that don't seem to have a name, so first we should
                                // count how many PNG headers there are, and start to give names only after
                                // excluding the rest from the beginning that shouldn't have a skin name.
                                // I'm not really sure if the data is stored in little or big endian, so
                                // hopefully my workaround code will always work even if it's not the right endian.
                                if (Lista_Índices__PNG != null && Lista_Índices__PNG.Count > 0) // We found PNG images.
                                {
                                    List<string> Lista_Nombres = new List<string>();
                                    if (Lista_Índices_IDS_DLCSKIN != null && Lista_Índices_IDS_DLCSKIN.Count > 0) // Try to get the skin names.
                                    {
                                        byte[] Matriz_2_Bytes = new byte[2];
                                        foreach (int Índice_IDS_DLCSKIN in Lista_Índices_IDS_DLCSKIN)
                                        {
                                            if (!Modo_skin_FromTheShadowsSkinPack_ && !Modo_skin_TheNightmareBeforeChristmas_)
                                            {
                                                int Índice_Inicio = -1;
                                                int Total_Ceros_Seguidos = 0; // Total of zero bytes found.
                                                for (int Índice_Byte = Índice_IDS_DLCSKIN - 13; Índice_Byte >= 0; Índice_Byte--)
                                                {
                                                    if (Matriz_Bytes[Índice_Byte] == 0) Total_Ceros_Seguidos++; // Increase on a zero.
                                                    else Total_Ceros_Seguidos = 0; // Reset on a non zero byte.
                                                    if (Total_Ceros_Seguidos >= 2) // We have 2 consecutive zero bytes.
                                                    {
                                                        Índice_Inicio = Índice_Byte + 1;
                                                        break;
                                                    }
                                                }
                                                if (Índice_Inicio > -1)
                                                {
                                                    Matriz_2_Bytes[0] = Matriz_Bytes[Índice_Inicio + 1]; // Use swapped byte order?
                                                    Matriz_2_Bytes[1] = Matriz_Bytes[Índice_Inicio];
                                                    short Longitud = BitConverter.ToInt16(Matriz_2_Bytes, 0);
                                                    if (Longitud > 0)
                                                    {
                                                        Longitud *= 2; // Unicode length, so double the byte length to read.
                                                        byte[] Matriz_Bytes_Nombre = new byte[Longitud];
                                                        Array.Copy(Matriz_Bytes, Índice_Inicio + 2, Matriz_Bytes_Nombre, 0, Matriz_Bytes_Nombre.Length);
                                                        string Nombre = Encoding.BigEndianUnicode.GetString(Matriz_Bytes_Nombre);
                                                        Lista_Nombres.Add(Nombre);
                                                        Matriz_Bytes_Nombre = null;
                                                    }
                                                }
                                            }
                                            else // [2019_09_23_12_49_01_159] Test result: it worked for both skin packs, so now 100 % working.
                                            {
                                                int Diferencia = Modo_skin_FromTheShadowsSkinPack_ ? 55 : 65;
                                                int Índice_Fin = -1;
                                                int Total_Ceros_Seguidos = 0; // Total of zero bytes found.
                                                for (int Índice_Byte = Índice_IDS_DLCSKIN + Diferencia; Índice_Byte < Matriz_Bytes.Length; Índice_Byte++)
                                                {
                                                    if (Matriz_Bytes[Índice_Byte] == 0) Total_Ceros_Seguidos++; // Increase on a zero.
                                                    else Total_Ceros_Seguidos = 0; // Reset on a non zero byte.
                                                    if (Total_Ceros_Seguidos >= 2) // We have 2 consecutive zero bytes.
                                                    {
                                                        Índice_Fin = Índice_Byte - 1;
                                                        break;
                                                    }
                                                }
                                                if (Índice_Fin > -1)
                                                {
                                                    byte[] Matriz_Bytes_Nombre = new byte[Índice_Fin - (Índice_IDS_DLCSKIN + Diferencia)];
                                                    Array.Copy(Matriz_Bytes, Índice_IDS_DLCSKIN + Diferencia, Matriz_Bytes_Nombre, 0, Matriz_Bytes_Nombre.Length);
                                                    string Nombre = Encoding.BigEndianUnicode.GetString(Matriz_Bytes_Nombre);
                                                    Lista_Nombres.Add(Nombre);
                                                    Matriz_Bytes_Nombre = null;
                                                }
                                            }
                                        }
                                    }
                                    // Temporary fix to keep the original skin names.
                                    //List<string> Lista_Nombres_Original = Lista_Nombres.GetRange(0, Lista_Nombres.Count);

                                    // This code worked perfectly except to get the first skin name if it's not a cape.
                                    /*// I just found out that some resource files seem to have the skin names around 12 bytes
                                    // after what I was expecting, so I'm not sure how to fully fix this...
                                    // But I just saw that all the names that failed were "1", so I could fix this in theory.
                                    List<string> Lista_Nombres = new List<string>();
                                    byte[] Matriz_2_Bytes = new byte[2];
                                    foreach (int Índice_IEND_B__ in Lista_Índices_IEND_B__)
                                    {
                                        // First read the next 2 bytes to get the length of the next stored string.
                                        // This array with 2 bytes should be used to quickly set the right endian order.
                                        Matriz_2_Bytes[0] = Matriz_Bytes[Índice_IEND_B__ + 19]; // Use swapped byte order?
                                        Matriz_2_Bytes[1] = Matriz_Bytes[Índice_IEND_B__ + 18];
                                        short Longitud = BitConverter.ToInt16(Matriz_2_Bytes, 0);
                                        // This code below should also always work (hopefully).
                                        //short Longitud = BitConverter.ToInt16(Matriz_Bytes, Índice_IEND_B__ + 19);
                                        if (Longitud > 0)
                                        {
                                            // It seems that the Xbox 360 only stores the string length in characters and not bytes.
                                            Longitud *= 2; // Unicode length, so double the byte length to read.
                                            // I believe there aren't any null character terminators for any string, so ignore them.
                                            byte[] Matriz_Bytes_Nombre = new byte[Longitud];
                                            // 2 Options that should work (hopefully), since I don't know which endian is used:
                                            // First, use Índice_IEND_B__ + 20 and Encoding.BigEndianUnicode.GetString();
                                            // Second, use Índice_IEND_B__ + 21 and Encoding.Unicode.GetString();
                                            Array.Copy(Matriz_Bytes, Índice_IEND_B__ + 20, Matriz_Bytes_Nombre, 0, Matriz_Bytes_Nombre.Length);
                                            string Nombre = Encoding.BigEndianUnicode.GetString(Matriz_Bytes_Nombre);
                                            if (string.Compare(Nombre, "1", true) != 0) // I'm not sure why sometimes is missaligned.
                                            {
                                                Lista_Nombres.Add(Nombre);
                                            }
                                            else // The first code failed, so try again with a new adapted code. Result: it worked!
                                            {
                                                // Skip all the bytes that are zero after the first incorrect name.
                                                int Índice_Inicio = -1;
                                                for (int Índice_Byte = Índice_IEND_B__ + 20 + Longitud; Índice_Byte < Matriz_Bytes.Length; Índice_Byte++)
                                                {
                                                    if (Matriz_Bytes[Índice_Byte] != 0)
                                                    {
                                                        Índice_Inicio = Índice_Byte - 1; // Start at the previous zero.
                                                        break;
                                                    }
                                                }
                                                if (Índice_Inicio > -1)
                                                {
                                                    Matriz_2_Bytes[0] = Matriz_Bytes[Índice_Inicio + 1]; // Use swapped byte order?
                                                    Matriz_2_Bytes[1] = Matriz_Bytes[Índice_Inicio];
                                                    Longitud = BitConverter.ToInt16(Matriz_2_Bytes, 0);
                                                    if (Longitud > 0)
                                                    {
                                                        Longitud *= 2; // Unicode length, so double the byte length to read.
                                                        Matriz_Bytes_Nombre = new byte[Longitud];
                                                        Array.Copy(Matriz_Bytes, Índice_Inicio + 2, Matriz_Bytes_Nombre, 0, Matriz_Bytes_Nombre.Length);
                                                        Nombre = Encoding.BigEndianUnicode.GetString(Matriz_Bytes_Nombre);
                                                        if (string.Compare(Nombre, "1", true) != 0) // Second chance to get the real name.
                                                        {
                                                            Lista_Nombres.Add(Nombre);
                                                        }
                                                        else // This should never happen (hopefully).
                                                        {
                                                            // I don't really know what to do here for now...
                                                            MessageBox.Show(this, "Unknown name found or unable to retrieve the original name.");
                                                        }
                                                    }
                                                }
                                            }
                                            Matriz_Bytes_Nombre = null;
                                        }
                                    }*/
                                    if (Lista_Índices__PNG.Count > 0)
                                    {
                                        // Just add empty names at the beginning until both lists have the same length.
                                        while (Lista_Nombres.Count < Lista_Índices__PNG.Count)
                                        {
                                            Lista_Nombres.Insert(0, ""); // This should mean that there are capes, etc.
                                            // Or in the worse case scenario, we have failed and got no skin names. Or is another file type?
                                        }
                                        // Now just add a numeric index in front of each skin name to keep them sorted.
                                        int Longitud_Número_Máximo = Lista_Nombres.Count.ToString().Length;
                                        for (int Índice_Nombre = 0; Índice_Nombre < Lista_Nombres.Count; Índice_Nombre++)
                                        {
                                            // Change any non valid file name character to "_" to avoid saving errors.
                                            char[] Matriz_Caracteres = Lista_Nombres[Índice_Nombre].ToCharArray();
                                            for (int Índice_Caracter = 0; Índice_Caracter < Matriz_Caracteres.Length; Índice_Caracter++)
                                            {
                                                if (Program.Lista_Caracteres_Prohibidos.Contains(Lista_Nombres[Índice_Nombre][Índice_Caracter]))
                                                {
                                                    Matriz_Caracteres[Índice_Caracter] = '_';
                                                }
                                            }
                                            Lista_Nombres[Índice_Nombre] = new string(Matriz_Caracteres);
                                            // Now add the skin number before the skin name.
                                            string Número = ((Índice_Nombre + 1) + Modo_Pack_Recursos_Skins).ToString(); // Start at 1.
                                            if (Modo_Pack_Recursos_Skins == 0 || ((Índice_Nombre + 1) + Modo_Pack_Recursos_Skins > 0)) // Add zeros to the left only to the actual skins.
                                            {
                                                while (Número.Length < Longitud_Número_Máximo) Número = '0' + Número;
                                            }
                                            //if (Lista_Nombres_en_EN == null || Lista_Nombres_en_EN.Count <= 0 || string.IsNullOrEmpty(Lista_Nombres[Índice_Nombre]))
                                            {
                                                Lista_Nombres[Índice_Nombre] = Número + (!string.IsNullOrEmpty(Lista_Nombres[Índice_Nombre]) ? ' ' + Lista_Nombres[Índice_Nombre] : null);
                                            }
                                        }
                                        // Finally we should be ready to extract all the PNG images and give them names.
                                        for (int Índice_Apertura = 0; Índice_Apertura < Lista_Índices__PNG.Count; Índice_Apertura++)
                                        {
                                            byte[] Matriz_Bytes_Recurso = null;
                                            Image Imagen_Original = null;
                                            int Índice_Formato_Cierre = -1;
                                            if (Índice_Apertura + 1 < Lista_Índices__PNG.Count)
                                            {
                                                Índice_Formato_Cierre = Lista_Índices__PNG[Índice_Apertura + 1];
                                            }
                                            else Índice_Formato_Cierre = Matriz_Bytes.Length;
                                            Matriz_Bytes_Recurso = new byte[!Variable_Extraer_Tamaño_Máximo ? Índice_Formato_Cierre - Lista_Índices__PNG[Índice_Apertura] : Matriz_Bytes.Length - Lista_Índices__PNG[Índice_Apertura]];
                                            Array.Copy(Matriz_Bytes, Lista_Índices__PNG[Índice_Apertura], Matriz_Bytes_Recurso, 0, Matriz_Bytes_Recurso.Length);
                                            try { Imagen_Original = Image.FromStream(new MemoryStream(Matriz_Bytes_Recurso, true), false, false); }
                                            catch { Imagen_Original = null; }
                                            if (Imagen_Original != null)
                                            {
                                                Bitmap Imagen = Convertir_Imagen_Original(Imagen_Original, string.Compare(Path.GetFileNameWithoutExtension(Ruta_Entrada), "media", true) == 0);
                                                if (Imagen != null)
                                                {
                                                    string Número = Lista_Índices__PNG[Índice_Apertura].ToString();
                                                    while (Número.Length < Longitud_Total_Máxima) Número = '0' + Número;
                                                    string Ruta = Ruta_Salida + "\\" + (Índice_Apertura < Lista_Nombres.Count ? Path.GetFileNameWithoutExtension(Lista_Nombres[Índice_Apertura]) : Número);
                                                    while (File.Exists(Ruta + ".png")) Ruta += '_'; // Avoid overwriting the files.
                                                    Imagen.Save(Ruta + ".png", ImageFormat.Png);
                                                    Imagen.Dispose();
                                                    Imagen = null;
                                                }
                                                Imagen_Original.Dispose();
                                                Imagen_Original = null;
                                            }
                                        }
                                        // I believe this will never work at 100 % since there are several skins
                                        // on the same pack that have the same name, so I'll never be able to
                                        // know which one is mentioned first in the initial ordered list.
                                        // But if fully finished, this code should almost always work, in theory.
                                        /*// Finally try to sort the skins and rename the saved files.
                                        if (Lista_Nombres_en_EN != null && Lista_Nombres_en_EN.Count > 0)
                                        {
                                            int Índice_Skin = 1; // Start at 1.
                                            // Skip the null or empty skin names.
                                            foreach (string Nombre in Lista_Nombres_Original)
                                            {
                                                if (string.IsNullOrEmpty(Nombre))
                                                {
                                                    Índice_Skin++;
                                                }
                                            }
                                            // Add the existing names by it's new order.
                                            foreach (string Nombre in Lista_Nombres_en_EN)
                                            {
                                                // Skip any empty skin name.
                                                if (!string.IsNullOrEmpty(Nombre) && Lista_Nombres_Original.Contains(Nombre))
                                                {
                                                    string Ruta = Ruta_Salida + "\\" + Path.GetFileNameWithoutExtension(Nombre) + ".png";
                                                    if (File.Exists(Ruta))
                                                    {
                                                        string Número = Índice_Skin.ToString();
                                                        while (Número.Length < Longitud_Número_Máximo) Número = '0' + Número;
                                                        File.Move(Ruta, Path.GetDirectoryName(Ruta) + "\\" + Número + ' ' + Path.GetFileName(Ruta));
                                                    }
                                                    Índice_Skin++; // We found another skin.
                                                }
                                            }
                                        }*/
                                    }
                                }
                                Lista_Índices__PNG = null;
                                Lista_Índices_IDS_DLCSKIN = null;
                                Lista_Índices_IDS_SKINS_DLCSKIN = null;
                            }
                        }
                        // Extra code: once we are done with the PNG and TGA images, try to locate
                        // and export other types like JPEG, GIF, etc.

                        //int Marcadores_Android = 0; // 0, 114, 101, 115
                        //int Marcadores_BlackBerry = 0; // 0, 47
                        //int Marcadores_BMP_Apertura = 0; // 66, 77, 54 = BM6
                        //int Marcadores_BMP_Cierre = 0; // ?
                        //int Marcadores_GIF_Apertura = 0; // 71, 73, 70, 56 = GIF8
                        //int Marcadores_GIF_Cierre = 0; // 0, 59
                        //int Marcadores_JPG_Apertura = 0; // 255, 216, 255, 224~239 = ÿØÿ~
                        //int Marcadores_JPG_Cierre = 0; // 255, 217
                        //int Marcadores_PNG_Apertura = 0; // 137, 80, 78, 71 = ~PNG
                        //int Marcadores_PNG_Cierre = 0; // 174, 66, 96, 130
                        //int Marcadores_TGA_Apertura = 0; // 0, 0, 2, 0
                        //int Marcadores_TGA_Cierre = 0; // ?
                        //int Marcadores_TIF_Apertura = 0; // 73, 73, 42, 0
                        //int Marcadores_TIF_Cierre = 0; // ?

                        List<int> Lista_Índices_BMP = Buscar_Matriz_Bytes_Archivo(Matriz_Bytes, new byte[3] { 66, 77, 54 }); // "BM6"
                        //List<int> Lista_Índices_DDS = Buscar_Matriz_Bytes_Archivo(Matriz_Bytes, new byte[3] { 68, 68, 83/*, 32*/ }); // "DDS "
                        List<int> Lista_Índices_GIF = Buscar_Matriz_Bytes_Archivo(Matriz_Bytes, new byte[3] { 71, 73, 70/*, 56*/ }); // "GIF89a"
                        List<int> Lista_Índices_JPG = Buscar_Matriz_Bytes_Archivo(Matriz_Bytes, new byte[3] { 255, 216, 255 }); // "ÿØÿ"
                        List<int> Lista_Índices_TIF = Buscar_Matriz_Bytes_Archivo(Matriz_Bytes, new byte[3] { 73, 73, 42/*, 0*/ }); // "II*"

                        // Now add all the found indexes to a single list and sort it.
                        List<int> Lista_Índices_Apertura_Extra = new List<int>();
                        if (Lista_Índices_BMP != null && Lista_Índices_BMP.Count > 0) // We have ".bmp".
                        {
                            Lista_Índices_Apertura_Extra.AddRange(Lista_Índices_BMP);
                        }
                        else Lista_Índices_BMP = new List<int>(); // Avoid null lists.

                        /*if (Lista_Índices_DDS != null && Lista_Índices_DDS.Count > 0) // We have ".dds".
                        {
                            Lista_Índices_Apertura_Extra.AddRange(Lista_Índices_DDS); // This is not supported yet.
                        }
                        else Lista_Índices_DDS = new List<int>(); // Avoid null lists.*/

                        if (Lista_Índices_GIF != null && Lista_Índices_GIF.Count > 0) // We have ".gif".
                        {
                            Lista_Índices_Apertura_Extra.AddRange(Lista_Índices_GIF);
                        }
                        else Lista_Índices_GIF = new List<int>(); // Avoid null lists.

                        if (Lista_Índices_JPG != null && Lista_Índices_JPG.Count > 0) // We have ".jpg".
                        {
                            Lista_Índices_Apertura_Extra.AddRange(Lista_Índices_JPG);
                        }
                        else Lista_Índices_JPG = new List<int>(); // Avoid null lists.

                        if (Lista_Índices_TIF != null && Lista_Índices_TIF.Count > 0) // We have ".tif".
                        {
                            Lista_Índices_Apertura_Extra.AddRange(Lista_Índices_TIF);
                        }
                        else Lista_Índices_BMP = new List<int>(); // Avoid null lists.

                        if (Lista_Índices_Apertura_Extra.Count > 0)
                        {
                            if (Lista_Índices_Apertura_Extra.Count > 1) Lista_Índices_Apertura_Extra.Sort();
                            for (int Índice_Apertura_Extra = 0; Índice_Apertura_Extra < Lista_Índices_Apertura_Extra.Count; Índice_Apertura_Extra++)
                            {
                                byte[] Matriz_Bytes_Recurso = null;
                                Image Imagen_Original = null;
                                int Índice_Formato_Cierre = -1;
                                if (Índice_Apertura_Extra + 1 < Lista_Índices_Apertura_Extra.Count)
                                {
                                    Índice_Formato_Cierre = Lista_Índices_Apertura_Extra[Índice_Apertura_Extra + 1];
                                }
                                else Índice_Formato_Cierre = Matriz_Bytes.Length;
                                Matriz_Bytes_Recurso = new byte[!Variable_Extraer_Tamaño_Máximo ? Índice_Formato_Cierre - Lista_Índices_Apertura_Extra[Índice_Apertura_Extra] : Matriz_Bytes.Length - Lista_Índices_Apertura_Extra[Índice_Apertura_Extra]];
                                Array.Copy(Matriz_Bytes, Lista_Índices_Apertura_Extra[Índice_Apertura_Extra], Matriz_Bytes_Recurso, 0, Matriz_Bytes_Recurso.Length);
                                try { Imagen_Original = Image.FromStream(new MemoryStream(Matriz_Bytes_Recurso, true), false, false); }
                                catch { Imagen_Original = null; }
                                if (Imagen_Original != null)
                                {
                                    Bitmap Imagen = Convertir_Imagen_Original(Imagen_Original, false/*string.Compare(Path.GetFileNameWithoutExtension(Ruta_Entrada), "media", true) == 0*/);
                                    if (Imagen != null)
                                    {
                                        string Nombre = null;
                                        if (Lista_Índices_BMP.Contains(Lista_Índices_Apertura_Extra[Índice_Apertura_Extra]))
                                        {
                                            Nombre = "BMP";
                                        }
                                        /*else if (Lista_Índices_DDS.Contains(Lista_Índices_Apertura_Extra[Índice_Apertura]))
                                        {
                                            Nombre = "DDS";
                                        }*/
                                        else if (Lista_Índices_GIF.Contains(Lista_Índices_Apertura_Extra[Índice_Apertura_Extra]))
                                        {
                                            Nombre = "GIF";
                                        }
                                        else if (Lista_Índices_JPG.Contains(Lista_Índices_Apertura_Extra[Índice_Apertura_Extra]))
                                        {
                                            Nombre = "JPG";
                                        }
                                        else if (Lista_Índices_TIF.Contains(Lista_Índices_Apertura_Extra[Índice_Apertura_Extra]))
                                        {
                                            Nombre = "TIF";
                                        }
                                        string Número = (Índice_Apertura_Extra + 1).ToString();
                                        while (Número.Length < Longitud_Total_Máxima) Número = '0' + Número;
                                        string Ruta = Ruta_Salida + "\\_" + Nombre + ' ' + Número;
                                        while (File.Exists(Ruta + ".png")) Ruta += '_'; // Avoid overwriting the files.
                                        Imagen.Save(Ruta + ".png", ImageFormat.Png);
                                        Imagen.Dispose();
                                        Imagen = null;
                                    }
                                    Imagen_Original.Dispose();
                                    Imagen_Original = null;
                                }
                                Matriz_Bytes_Recurso = null;
                            }
                        }
                        Lista_Índices_Apertura_Extra = null;
                        Lista_Índices_BMP = null;
                        //Lista_Índices_DDS = null;
                        Lista_Índices_GIF = null;
                        Lista_Índices_JPG = null;
                        Lista_Índices_TIF = null;
                    }
                    Matriz_Bytes = null;
                    Lector_Salida_Texto.WriteLine();
                    Lector_Salida_Texto.Flush();
                    Lector_Salida_Texto.Close();
                    Lector_Salida_Texto.Dispose();
                    Lector_Salida_Texto = null;
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                    string[] Matriz_Archivos = Directory.GetFiles(Ruta_Salida);
                    // Check code to remove any empty folder.
                    if (Matriz_Archivos == null || Matriz_Archivos.Length <= 0)
                    {
                        // Assume we didn't extract any resource here and delete this empty folder.
                        Program.Eliminar_Archivo_Carpeta(Ruta_Salida);
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return 0;
        }

        internal List<string> Lista_Global_Rutas_Recursos = new List<string>();

        /// <summary>
        /// Function designed to search the full contents of a byte array to find any full
        /// matches with a second byte array.
        /// </summary>
        /// <param name="Matriz_Bytes">Any valid byte array to search inside of it the contents
        /// of the second byte array.</param>
        /// <param name="Matriz_Bytes_Buscar">Any valid byte array with less or equal length than
        /// the first byte array, the full contents of this array will be searched in the first.</param>
        /// <returns>Returns a list with all the indexes found within the first byte array. Returns null on any error or if no matches were found.</returns>
        internal List<int> Buscar_Matriz_Bytes_Archivo(byte[] Matriz_Bytes, byte[] Matriz_Bytes_Buscar)
        {
            try
            {
                if (Matriz_Bytes != null && Matriz_Bytes_Buscar != null && Matriz_Bytes.Length >= Matriz_Bytes_Buscar.Length)
                {
                    List<int> Lista_Índices = new List<int>();
                    for (int Índice_Byte = 0; Índice_Byte < Matriz_Bytes.Length - Matriz_Bytes_Buscar.Length; Índice_Byte++)
                    {
                        bool Encontrado = true;
                        for (int Índice_Buscar = 0; Índice_Buscar < Matriz_Bytes_Buscar.Length; Índice_Buscar++)
                        {
                            if (Matriz_Bytes[Índice_Byte + Índice_Buscar] != Matriz_Bytes_Buscar[Índice_Buscar])
                            {
                                Encontrado = false;
                                break;
                            }
                        }
                        if (Encontrado)
                        {
                            Lista_Índices.Add(Índice_Byte);
                        }
                    }
                    if (Lista_Índices.Count > 0) return Lista_Índices;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        /// <summary>
        /// [LEGAL NOTE] I'm not sure if it's legal to extract and convert the resource packs and
        /// skin files to PC format, but since I've payed all the packs myself on my old Xbox 360,
        /// and played for years with the Natural pack, among others, when I moved to PC I was missing
        /// too much the Xbox 360 packs and skins, so I used Horizon to extract some of the files
        /// from the latest Xbox 360 Minecraft update (Minecraft 1.13 equivalent), but those files
        /// didn't contain any image nor I had any tool to decrypt or extract the resources from them.
        /// So after spending a lot of weeks programming and trying codes, I ended up creating this
        /// new functions that are working 100 %, even for TGA images, and they can detect and extract
        /// all skin names and even block and item names. This only should be used as a "nostalgia" tool,
        /// and NEVER to make any kind of profit, either monetary or popular by getting likes, etc.
        /// YOU DON'T HAVE PERMISSION TO USE ANY PART OF THIS CODE TO MAKE ANYTHING ILLEGAL WITH ANY
        /// OF THE FILES FROM THE XBOX 360. YOU'VE BEEN WARNED!
        /// Function that extracts all the resources found inside the Xbox 360 Minecraft DLC files.
        /// WARNING: I found out that the Xbox 360 packs have up to 5 extra paintings without known name.
        /// TEST: success it created almost 25.000 files in less than a minute, even all TGA files.
        /// </summary>
        /// <param name="Ruta_DLC">Any valid directory path to the "DLC", usually at "Xbox\res\DLC"</param>
        internal void Extraer_Recursos_Xbox_360_Minecraft_1_13(string Ruta_DLC)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta_DLC) && Directory.Exists(Ruta_DLC))
                {
                    // List with all the Xbox 360 resource packs for Minecraft 1.13.
                    // I looked for them for years on the web and I never found them updated or complete.
                    // So I made this tool myself to achieve this and convert the packs to PC format.
                    Lista_Global_Rutas_Recursos = new List<string>(); // Reset each time.
                    string[] Matriz_Carpetas = Directory.GetDirectories(Ruta_DLC, "*", SearchOption.TopDirectoryOnly);
                    if (Matriz_Carpetas != null && Matriz_Carpetas.Length > 0)
                    {
                        foreach (string Carpeta in Matriz_Carpetas)
                        {
                            string Nombre = Path.GetFileName(Carpeta);
                            string[] Matriz_Archivos = Directory.GetFiles(Carpeta, "*", SearchOption.TopDirectoryOnly);
                            string[] Matriz_Subcarpetas = Directory.GetDirectories(Carpeta, "*", SearchOption.AllDirectories);
                            List<string> Lista_Archivos = new List<string>();
                            foreach (string Subcarpeta in Matriz_Subcarpetas)
                            {
                                string[] Matriz_Subarchivos = Directory.GetFiles(Subcarpeta, "*", SearchOption.AllDirectories);
                                if (Matriz_Subarchivos != null && Matriz_Subarchivos.Length > 0) Lista_Archivos.AddRange(Matriz_Subarchivos);
                            }
                            // Now we should have for the current main folder an array with the top
                            // files in it and in a list all the files from it's possible subfolders.
                            // Assume that the top files contain skins and the rest resource packs.
                            foreach (string Ruta in Matriz_Archivos) // Top files, assume skin files.
                            {
                                // Note: the skins will be sorted by the order in which they are found.
                                // Although there is a first code that gets a full list with the sorted names,
                                // but sadly among other varied strings, so it might be possible to do a real
                                // sorting like it is supposed to be seen on the Xbox 360, but it's not tested.
                                Extraer_Recursos_Archivo(Ruta, Ruta_PC + "\\" + Nombre, true, Lista_Packs_Recursos.Contains(Nombre), Nombre);
                            }
                            foreach (string Ruta in Lista_Archivos) // Subfiles, assume resource pack files.
                            {
                                // Now it auto-detects the JPEG and PNG types and swaps the R and B colors
                                // only for the correct type on the "media" folders.
                                Extraer_Recursos_Archivo(Ruta, Ruta_PC + "\\" + Nombre + "\\" + Path.GetFileNameWithoutExtension(Ruta), false, false/*Lista_Packs_Recursos.Contains(Nombre)*/, Nombre);
                            }
                            Lista_Archivos = null;
                            Matriz_Subcarpetas = null;
                            Matriz_Archivos = null;
                        }
                    }
                    Matriz_Carpetas = null;
                    // Finally save the list of unique resource paths.
                    if (Lista_Global_Rutas_Recursos.Count > 0)
                    {
                        if (Lista_Global_Rutas_Recursos.Count > 1) Lista_Global_Rutas_Recursos.Sort();
                        FileStream Lector = new FileStream(Ruta_PC + "\\Paths.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                        Lector.SetLength(0L); // Reset each time.
                        Lector.Seek(0L, SeekOrigin.Begin);
                        StreamWriter Lector_Texto = new StreamWriter(Lector, Encoding.UTF8);
                        foreach (string Ruta in Lista_Global_Rutas_Recursos)
                        {
                            Lector_Texto.Write(Ruta);
                            Lector_Texto.WriteLine();
                            Lector_Texto.Flush();
                        }
                        Lector_Texto.Close();
                        Lector_Texto.Dispose();
                        Lector_Texto = null;
                        Lector.Close();
                        Lector.Dispose();
                        Lector = null;

                        // Now generate the final resource packs in the Minecraft 1.14.4 format.
                        //Dictionary<string, string> Diccionario_Rutas_Recursos = new Dictionary<string, string>();






                        if (Lista_Packs_Recursos != null && Lista_Packs_Recursos.Count > 0)
                        {
                            string Ruta_Salida = Ruta_PC + "\\_Resource Packs";
                            Program.Crear_Carpetas(Ruta_Salida);
                            foreach (string Nombre in Lista_Packs_Recursos)
                            {

                            }
                        }
                    }
                    SystemSounds.Asterisk.Play();
                    // Skin packs investigation [2019_09_22_18_15_57_042]:
                    // Each ".pck" file seems to have the names of the skins in several languages
                    // so better search for "en-EN" for a second time in each file to get closer
                    // to the names of the images. This test failed but a better one was created.
                    /*List<string> Lista_Packs_Recursos = new List<string>(new string[]
                    {
                        "Adventure Time",
                        "Candy",
                        "Cartoon",
                        "Chinese Mythology",
                        "City",
                        "Egyptian Mythology",
                        "Fallout",
                        "Fantasy",
                        "Festive",
                        "Greek Mythology",
                        "Halloween",
                        "Halloween 2015",
                        "Halo",
                        "Mass Effect",
                        "Natural",
                        "Norse Mythology",
                        "Pattern",
                        "Pirates Of The Caribbean",
                        "Plastic",
                        "Skyrim",
                        "Steampunk",
                        "Super Cute Texture Pack",
                        "The Nightmare Before Christmas"
                    });
                    if (Lista_Packs_Recursos != null && Lista_Packs_Recursos.Count > 0)
                    {
                        foreach (string Nombre in Lista_Packs_Recursos)
                        {
                            //string Ruta = Ruta_PC + "\\" + Nombre;
                            string Ruta_Media = Ruta_DLC + "\\" + Nombre + "\\Data\\media.arc";
                            string Ruta_x16Data = Ruta_DLC + "\\" + Nombre + "\\Data\\x16Data.pck";
                            string Ruta_x32Data = Ruta_DLC + "\\" + Nombre + "\\Data\\x32Data.pck";

                            if (!string.IsNullOrEmpty(Ruta_Media) && File.Exists(Ruta_Media))
                            {
                                Extraer_Recursos_Archivo(Ruta_Media, Ruta_PC + "\\" + Nombre + "\\media", -1); // Unknown TGA dimensions!
                            }
                            for (int Índice_Resolución = 16; Índice_Resolución <= 1024; Índice_Resolución *= 2)
                            {
                                string Ruta_x00Data = Ruta_DLC + "\\" + Nombre + "\\Data\\x" + Índice_Resolución.ToString() + "Data.pck";
                                if (!string.IsNullOrEmpty(Ruta_x00Data) && File.Exists(Ruta_x00Data))
                                {
                                    Extraer_Recursos_Archivo(Ruta_x00Data, Ruta_PC + "\\" + Nombre + "\\x" + Índice_Resolución.ToString() + "Data", Índice_Resolución);
                                }
                            }
                        }
                    }*/
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Function to generate new C# code after translating the "Paths.txt" into a new strings dictionary.
        /// </summary>
        /// <param name="Ruta">Any valid file path. It should be the file "Paths.txt" generated after
        /// extracting all the resources from the DLCs of the Minecraft Xbox 360 Edition.</param>
        internal void Generar_Diccionario_Recursos_Xbox_360(string Ruta)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                {
                    List<string> Lista_Líneas = Program.Obtener_Lista_Líneas_Archivo(Ruta);
                    if (Lista_Líneas != null && Lista_Líneas.Count > 0)
                    {
                        if (Lista_Líneas.Count > 1) Lista_Líneas.Sort();
                        string Texto = null; // To copy the results to the clipboard.
                        //Dictionary<string, string> Diccionario_Rutas_Recursos = new Dictionary<string, string>();
                        foreach (string Línea in Lista_Líneas)
                        {
                            if (!string.IsNullOrEmpty(Línea))
                            {
                                string Ruta_Minecraft_1_14_4 = "00000000";
                                string Nombre = Path.GetFileNameWithoutExtension(Línea);
                                /*if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";
                                else if (string.Compare(Nombre, "", true) == 0) Ruta_Minecraft_1_14_4 = "";*/
                                Texto += "Diccionario_Rutas_Recursos.Add(\"" + Línea + "\", \"" + Ruta_Minecraft_1_14_4 + ".png\");\r\n";
                            }
                        }
                        if (!string.IsNullOrEmpty(Texto))
                        {
                            Clipboard.SetText(Texto);
                            SystemSounds.Asterisk.Play();
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Function that starts a resource dictionary to generate all the Xbox 360 resource packs
        /// with the Minecraft 1.14.4 PC format. It needs at least the "step 1" function to have all
        /// the resources available, otherwise this will never work properly. This is the "step 3".
        /// </summary>
        /// <param name="Ruta">Any valid directory path, to where the "step 1" function generated all
        /// the Xbox 360 resources, usually under the "PC\\..." folder near the main application.</param>
        internal void Generar_Packs_Recursos_Xbox_360(string Ruta_Entrada, string Ruta_Salida)
        {
            try
            {
                // WARNING: a lot of entity textures from the "Egyptian Mythology" pack don't
                // fit and look totally bad and incomplete, also some other packs are failing
                // too. But for a first conversion it's not totally bad at the moment, the
                // blocks and items look mostly as intended, even the animated ones. So with a
                // bit more time this will be updated and properly converted in a near future.
                // How to quick fix it: rename the folder "entity" to "entity_" to use the
                // default entity textures, the rest should look complete, hopefully.
                if (!string.IsNullOrEmpty(Ruta_Entrada) && Directory.Exists(Ruta_Entrada) && !string.IsNullOrEmpty(Ruta_Salida) && !Directory.Exists(Ruta_Salida))
                {
                    // This dictionary code was auto-generated with the "step 2" function to save a lot of time.
                    // Then the right "00000000.png" string was translated to the final 1.14.4 resource path.
                    // Note: the "unknown" textures will be stored on a folder with that name under "textures".
                    Dictionary<string, string> Diccionario_Rutas_Recursos = new Dictionary<string, string>();
                    Diccionario_Rutas_Recursos.Add("res/armor/chain_1.png", "assets\\minecraft\\textures\\models\\armor\\chainmail_layer_1.png");
                    Diccionario_Rutas_Recursos.Add("res/armor/chain_2.png", "assets\\minecraft\\textures\\models\\armor\\chainmail_layer_2.png");
                    Diccionario_Rutas_Recursos.Add("res/armor/cloth_1.png", "assets\\minecraft\\textures\\models\\armor\\leather_layer_1.png");
                    Diccionario_Rutas_Recursos.Add("res/armor/cloth_1_b.png", "assets\\minecraft\\textures\\models\\armor\\leather_layer_1_overlay.png");
                    Diccionario_Rutas_Recursos.Add("res/armor/cloth_2.png", "assets\\minecraft\\textures\\models\\armor\\leather_layer_2.png");
                    Diccionario_Rutas_Recursos.Add("res/armor/cloth_2_b.png", "assets\\minecraft\\textures\\models\\armor\\leather_layer_2_overlay.png");
                    Diccionario_Rutas_Recursos.Add("res/armor/diamond_1.png", "assets\\minecraft\\textures\\models\\armor\\diamond_layer_1.png");
                    Diccionario_Rutas_Recursos.Add("res/armor/diamond_2.png", "assets\\minecraft\\textures\\models\\armor\\diamond_layer_2.png");
                    Diccionario_Rutas_Recursos.Add("res/armor/gold_1.png", "assets\\minecraft\\textures\\models\\armor\\gold_layer_1.png");
                    Diccionario_Rutas_Recursos.Add("res/armor/gold_2.png", "assets\\minecraft\\textures\\models\\armor\\gold_layer_2.png");
                    Diccionario_Rutas_Recursos.Add("res/armor/iron_1.png", "assets\\minecraft\\textures\\models\\armor\\iron_layer_1.png");
                    Diccionario_Rutas_Recursos.Add("res/armor/iron_2.png", "assets\\minecraft\\textures\\models\\armor\\iron_layer_2.png");
                    Diccionario_Rutas_Recursos.Add("res/armor/power.png", "assets\\minecraft\\textures\\entity\\creeper\\creeper_armor.png");
                    Diccionario_Rutas_Recursos.Add("res/armor/turtle_1.png", "assets\\minecraft\\textures\\models\\armor\\turtle_layer_1.png");
                    Diccionario_Rutas_Recursos.Add("res/art/kz.png", "assets\\minecraft\\textures\\painting\\paintings_kristoffer_zetterstrand.png");
                    Diccionario_Rutas_Recursos.Add("res/environment/clouds.png", "assets\\minecraft\\textures\\environment\\clouds.png");
                    Diccionario_Rutas_Recursos.Add("res/environment/rain.png", "assets\\minecraft\\textures\\environment\\rain.png");
                    Diccionario_Rutas_Recursos.Add("res/environment/snow.png", "assets\\minecraft\\textures\\environment\\snow.png");
                    Diccionario_Rutas_Recursos.Add("res/item/armorstand/wood.png", "assets\\minecraft\\textures\\entity\\armorstand\\wood.png");
                    Diccionario_Rutas_Recursos.Add("res/item/arrows.png", "assets\\minecraft\\textures\\entity\\arrow.png");
                    Diccionario_Rutas_Recursos.Add("res/item/banner/banner_base.png", "assets\\minecraft\\textures\\entity\\banner_base.png");
                    Diccionario_Rutas_Recursos.Add("res/item/bed.png", "assets\\minecraft\\textures\\entity\\bed\\bed.png");
                    Diccionario_Rutas_Recursos.Add("res/item/boat/boat_acacia.png", "assets\\minecraft\\textures\\entity\\boat\\acacia.png");
                    Diccionario_Rutas_Recursos.Add("res/item/boat/boat_birch.png", "assets\\minecraft\\textures\\entity\\boat\\birch.png");
                    Diccionario_Rutas_Recursos.Add("res/item/boat/boat_darkoak.png", "assets\\minecraft\\textures\\entity\\boat\\dark_oak.png");
                    Diccionario_Rutas_Recursos.Add("res/item/boat/boat_jungle.png", "assets\\minecraft\\textures\\entity\\boat\\jungle.png");
                    Diccionario_Rutas_Recursos.Add("res/item/boat/boat_oak.png", "assets\\minecraft\\textures\\entity\\boat\\oak.png");
                    Diccionario_Rutas_Recursos.Add("res/item/boat/boat_spruce.png", "assets\\minecraft\\textures\\entity\\boat\\spruce.png");
                    Diccionario_Rutas_Recursos.Add("res/item/book.png", "assets\\minecraft\\textures\\entity\\enchanting_table_book.png");
                    Diccionario_Rutas_Recursos.Add("res/item/cart.png", "assets\\minecraft\\textures\\entity\\minecart.png");
                    Diccionario_Rutas_Recursos.Add("res/item/chest.png", "assets\\minecraft\\textures\\entity\\chest\\normal.png");
                    Diccionario_Rutas_Recursos.Add("res/item/conduit/conduit_base.png", "assets\\minecraft\\textures\\entity\\conduit\\base.png");
                    Diccionario_Rutas_Recursos.Add("res/item/conduit/conduit_cage.png", "assets\\minecraft\\textures\\entity\\conduit\\cage.png");
                    Diccionario_Rutas_Recursos.Add("res/item/conduit/conduit_closed.png", "assets\\minecraft\\textures\\entity\\conduit\\closed_eye.png");
                    Diccionario_Rutas_Recursos.Add("res/item/conduit/conduit_open.png", "assets\\minecraft\\textures\\entity\\conduit\\open_eye.png");
                    Diccionario_Rutas_Recursos.Add("res/item/conduit/conduit_wind_horizontal.png", "assets\\minecraft\\textures\\entity\\conduit\\wind.png");
                    Diccionario_Rutas_Recursos.Add("res/item/conduit/conduit_wind_vertical.png", "assets\\minecraft\\textures\\entity\\conduit\\wind_vertical.png");
                    Diccionario_Rutas_Recursos.Add("res/item/elytra.png", "assets\\minecraft\\textures\\entity\\elytra.png");
                    Diccionario_Rutas_Recursos.Add("res/item/enderchest.png", "assets\\minecraft\\textures\\entity\\chest\\ender.png");
                    Diccionario_Rutas_Recursos.Add("res/item/firework.png", "assets\\minecraft\\textures\\entity\\firework.png");
                    Diccionario_Rutas_Recursos.Add("res/item/largechest.png", "assets\\minecraft\\textures\\entity\\chest\\normal_double.png");
                    Diccionario_Rutas_Recursos.Add("res/item/lead_knot.png", "assets\\minecraft\\textures\\entity\\lead_knot.png");
                    Diccionario_Rutas_Recursos.Add("res/item/sign.png", "assets\\minecraft\\textures\\entity\\signs\\oak.png");
                    Diccionario_Rutas_Recursos.Add("res/item/tipped_arrow.png", "assets\\minecraft\\textures\\entity\\tipped_arrow.png");
                    Diccionario_Rutas_Recursos.Add("res/item/trapped.png", "assets\\minecraft\\textures\\entity\\chest\\trapped.png");
                    Diccionario_Rutas_Recursos.Add("res/item/trapped_double.png", "assets\\minecraft\\textures\\entity\\chest\\trapped_double.png");
                    Diccionario_Rutas_Recursos.Add("res/item/trident.png", "assets\\minecraft\\textures\\entity\\trident.png");
                    Diccionario_Rutas_Recursos.Add("res/item/trident_riptide.png", "assets\\minecraft\\textures\\entity\\trident_riptide.png");
                    Diccionario_Rutas_Recursos.Add("res/items.png", "assets\\minecraft\\textures\\item\\items.png");
                    Diccionario_Rutas_Recursos.Add("res/itemsMipMapLevel2.png", "assets\\minecraft\\textures\\item\\itemsMipMapLevel2.png");
                    Diccionario_Rutas_Recursos.Add("res/misc/additionalmapicons.png", "assets\\minecraft\\textures\\misc\\mapicons.png");
                    Diccionario_Rutas_Recursos.Add("res/misc/beacon_beam.png", "assets\\minecraft\\textures\\entity\\beacon_beam.png");
                    Diccionario_Rutas_Recursos.Add("res/misc/dial.png", "assets\\minecraft\\textures\\misc\\dial.png");
                    Diccionario_Rutas_Recursos.Add("res/misc/end_gateway_beam.png", "assets\\minecraft\\textures\\entity\\end_gateway_beam.png");
                    Diccionario_Rutas_Recursos.Add("res/misc/foliagecolor.png", "assets\\minecraft\\textures\\colormap\\foliage.png");
                    Diccionario_Rutas_Recursos.Add("res/misc/grasscolor.png", "assets\\minecraft\\textures\\colormap\\grass.png");
                    Diccionario_Rutas_Recursos.Add("res/misc/haloRing.png", "assets\\minecraft\\textures\\misc\\_haloRing.png");
                    Diccionario_Rutas_Recursos.Add("res/misc/haloRingMipMapLevel2.png", "assets\\minecraft\\textures\\misc\\_haloRingMipMapLevel2.png");
                    Diccionario_Rutas_Recursos.Add("res/misc/haloRingMipMapLevel3.png", "assets\\minecraft\\textures\\misc\\_haloRingMipMapLevel3.png");
                    Diccionario_Rutas_Recursos.Add("res/misc/mapbg.png", "assets\\minecraft\\textures\\map\\map_background.png");
                    Diccionario_Rutas_Recursos.Add("res/misc/mapicons.png", "assets\\minecraft\\textures\\map\\map_icons.png");
                    Diccionario_Rutas_Recursos.Add("res/misc/particlefield.png", "assets\\minecraft\\textures\\misc\\particlefield.png");
                    Diccionario_Rutas_Recursos.Add("res/misc/pumpkinblur.png", "assets\\minecraft\\textures\\misc\\pumpkinblur.png");
                    Diccionario_Rutas_Recursos.Add("res/misc/tunnel.png", "assets\\minecraft\\textures\\misc\\tunnel.png");
                    Diccionario_Rutas_Recursos.Add("res/misc/water.png", "assets\\minecraft\\textures\\block\\_water.png");
                    Diccionario_Rutas_Recursos.Add("res/misc/watercolor.png", "assets\\minecraft\\textures\\misc\\watercolor.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/bat.png", "assets\\minecraft\\textures\\entity\\bat.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/bear/polarbear.png", "assets\\minecraft\\textures\\entity\\bear\\polarbear.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/cat_black.png", "assets\\minecraft\\textures\\entity\\cat\\black.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/cat_red.png", "assets\\minecraft\\textures\\entity\\cat\\red.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/cat_siamese.png", "assets\\minecraft\\textures\\entity\\cat\\siamese.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/cavespider.png", "assets\\minecraft\\textures\\entity\\spider\\cave_spider.png");
                    //Diccionario_Rutas_Recursos.Add("res/mob/cavespider.tga", "assets\\minecraft\\textures\\entity\\spider\\cave_spider.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/char.png", "assets\\minecraft\\textures\\entity\\steve.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/chicken.png", "assets\\minecraft\\textures\\entity\\chicken.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/cow.png", "assets\\minecraft\\textures\\entity\\cow\\cow.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/creeper.png", "assets\\minecraft\\textures\\entity\\creeper\\creeper.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/dolphin.png", "assets\\minecraft\\textures\\entity\\dolphin.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/enderdragon/beam.png", "assets\\minecraft\\textures\\entity\\end_crystal\\end_crystal_beam.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/enderdragon/crystal.png", "assets\\minecraft\\textures\\entity\\end_crystal\\end_crystal.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/enderdragon/dragon_fireball.png", "assets\\minecraft\\textures\\entity\\enderdragon\\dragon_fireball.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/enderdragon/ender.png", "assets\\minecraft\\textures\\entity\\enderdragon\\dragon.png");
                    //Diccionario_Rutas_Recursos.Add("res/mob/enderdragon/ender.tga", "assets\\minecraft\\textures\\entity\\enderdragon\\dragon.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/enderdragon/ender_eyes.png", "assets\\minecraft\\textures\\entity\\enderdragon\\dragon_eyes.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/enderdragon/shuffle.png", "assets\\minecraft\\textures\\entity\\enderdragon\\dragon_exploding.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/enderman.png", "assets\\minecraft\\textures\\entity\\enderman\\enderman.png");
                    //Diccionario_Rutas_Recursos.Add("res/mob/enderman.tga", "assets\\minecraft\\textures\\entity\\enderman\\enderman.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/enderman_eyes.png", "assets\\minecraft\\textures\\entity\\enderman\\enderman_eyes.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/endermite.png", "assets\\minecraft\\textures\\entity\\endermite.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/fire.png", "assets\\minecraft\\textures\\entity\\blaze.png");
                    //Diccionario_Rutas_Recursos.Add("res/mob/fire.tga", "assets\\minecraft\\textures\\entity\\blaze.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/fish/cod.png", "assets\\minecraft\\textures\\entity\\fish\\cod.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/fish/pufferfish.png", "assets\\minecraft\\textures\\entity\\fish\\pufferfish.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/fish/salmon.png", "assets\\minecraft\\textures\\entity\\fish\\salmon.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/fish/tropical_a.png", "assets\\minecraft\\textures\\entity\\fish\\tropical_a.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/fish/tropical_a_pattern_1.png", "assets\\minecraft\\textures\\entity\\fish\\tropical_a_pattern_1.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/fish/tropical_a_pattern_2.png", "assets\\minecraft\\textures\\entity\\fish\\tropical_a_pattern_2.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/fish/tropical_a_pattern_3.png", "assets\\minecraft\\textures\\entity\\fish\\tropical_a_pattern_3.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/fish/tropical_a_pattern_4.png", "assets\\minecraft\\textures\\entity\\fish\\tropical_a_pattern_4.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/fish/tropical_a_pattern_5.png", "assets\\minecraft\\textures\\entity\\fish\\tropical_a_pattern_5.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/fish/tropical_a_pattern_6.png", "assets\\minecraft\\textures\\entity\\fish\\tropical_a_pattern_6.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/fish/tropical_b.png", "assets\\minecraft\\textures\\entity\\fish\\tropical_b.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/fish/tropical_b_pattern_1.png", "assets\\minecraft\\textures\\entity\\fish\\tropical_b_pattern_1.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/fish/tropical_b_pattern_2.png", "assets\\minecraft\\textures\\entity\\fish\\tropical_b_pattern_2.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/fish/tropical_b_pattern_3.png", "assets\\minecraft\\textures\\entity\\fish\\tropical_b_pattern_3.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/fish/tropical_b_pattern_4.png", "assets\\minecraft\\textures\\entity\\fish\\tropical_b_pattern_4.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/fish/tropical_b_pattern_5.png", "assets\\minecraft\\textures\\entity\\fish\\tropical_b_pattern_5.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/fish/tropical_b_pattern_6.png", "assets\\minecraft\\textures\\entity\\fish\\tropical_b_pattern_6.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/ghast.png", "assets\\minecraft\\textures\\entity\\ghast\\ghast.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/ghast_fire.png", "assets\\minecraft\\textures\\entity\\ghast\\ghast_shooting.png");
                    //Diccionario_Rutas_Recursos.Add("res/mob/ghast_fire.tga", "assets\\minecraft\\textures\\entity\\ghast\\ghast_shooting.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/guardian.png", "assets\\minecraft\\textures\\entity\\guardian.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/guardian_beam.png", "assets\\minecraft\\textures\\entity\\guardian_beam.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/guardian_elder.png", "assets\\minecraft\\textures\\entity\\guardian_elder.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/horse/armor/horse_armor_diamond.png", "assets\\minecraft\\textures\\entity\\horse\\armor\\horse_armor_diamond.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/horse/armor/horse_armor_gold.png", "assets\\minecraft\\textures\\entity\\horse\\armor\\horse_armor_gold.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/horse/armor/horse_armor_iron.png", "assets\\minecraft\\textures\\entity\\horse\\armor\\horse_armor_iron.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/horse/armor/horse_armor_leather_1.png", "assets\\minecraft\\textures\\entity\\horse\\armor\\horse_armor_leather.png");
                    //Diccionario_Rutas_Recursos.Add("res/mob/horse/armor/horse_armor_leather_1_b.png", "assets\\minecraft\\textures\\entity\\horse\\armor\\horse_armor_leather_b.png"); // This has to be fused with the previous one.
                    Diccionario_Rutas_Recursos.Add("res/mob/horse/donkey.png", "assets\\minecraft\\textures\\entity\\horse\\donkey.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/horse/horse_black.png", "assets\\minecraft\\textures\\entity\\horse\\horse_black.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/horse/horse_brown.png", "assets\\minecraft\\textures\\entity\\horse\\horse_brown.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/horse/horse_chestnut.png", "assets\\minecraft\\textures\\entity\\horse\\horse_chestnut.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/horse/horse_creamy.png", "assets\\minecraft\\textures\\entity\\horse\\horse_creamy.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/horse/horse_darkbrown.png", "assets\\minecraft\\textures\\entity\\horse\\horse_darkbrown.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/horse/horse_gray.png", "assets\\minecraft\\textures\\entity\\horse\\horse_gray.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/horse/horse_markings_blackdots.png", "assets\\minecraft\\textures\\entity\\horse\\horse_markings_blackdots.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/horse/horse_markings_white.png", "assets\\minecraft\\textures\\entity\\horse\\horse_markings_white.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/horse/horse_markings_whitedots.png", "assets\\minecraft\\textures\\entity\\horse\\horse_markings_whitedots.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/horse/horse_markings_whitefield.png", "assets\\minecraft\\textures\\entity\\horse\\horse_markings_whitefield.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/horse/horse_skeleton.png", "assets\\minecraft\\textures\\entity\\horse\\horse_skeleton.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/horse/horse_white.png", "assets\\minecraft\\textures\\entity\\horse\\horse_white.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/horse/horse_zombie.png", "assets\\minecraft\\textures\\entity\\horse\\horse_zombie.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/horse/mule.png", "assets\\minecraft\\textures\\entity\\horse\\mule.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/illager/evoker.png", "assets\\minecraft\\textures\\entity\\illager\\evoker.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/illager/fangs.png", "assets\\minecraft\\textures\\entity\\illager\\evoker_fangs.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/illager/vex.png", "assets\\minecraft\\textures\\entity\\illager\\vex.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/illager/vex_charging.png", "assets\\minecraft\\textures\\entity\\illager\\vex_charging.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/illager/vindicator.png", "assets\\minecraft\\textures\\entity\\illager\\vindicator.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/lava_.png", "assets\\minecraft\\textures\\entity\\slime\\magmacube.png");
                    //Diccionario_Rutas_Recursos.Add("res/mob/lava_.tga", "assets\\minecraft\\textures\\entity\\slime\\magmacube.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/decor/decor_black.png", "assets\\minecraft\\textures\\entity\\llama\\decor\\black.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/decor/decor_blue.png", "assets\\minecraft\\textures\\entity\\llama\\decor\\blue.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/decor/decor_brown.png", "assets\\minecraft\\textures\\entity\\llama\\decor\\brown.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/decor/decor_cyan.png", "assets\\minecraft\\textures\\entity\\llama\\decor\\cyan.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/decor/decor_gray.png", "assets\\minecraft\\textures\\entity\\llama\\decor\\gray.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/decor/decor_green.png", "assets\\minecraft\\textures\\entity\\llama\\decor\\green.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/decor/decor_light_blue.png", "assets\\minecraft\\textures\\entity\\llama\\decor\\light_blue.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/decor/decor_lime.png", "assets\\minecraft\\textures\\entity\\llama\\decor\\lime.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/decor/decor_magenta.png", "assets\\minecraft\\textures\\entity\\llama\\decor\\magenta.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/decor/decor_orange.png", "assets\\minecraft\\textures\\entity\\llama\\decor\\orange.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/decor/decor_pink.png", "assets\\minecraft\\textures\\entity\\llama\\decor\\pink.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/decor/decor_purple.png", "assets\\minecraft\\textures\\entity\\llama\\decor\\purple.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/decor/decor_red.png", "assets\\minecraft\\textures\\entity\\llama\\decor\\red.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/decor/decor_silver.png", "assets\\minecraft\\textures\\entity\\llama\\decor\\light_gray.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/decor/decor_white.png", "assets\\minecraft\\textures\\entity\\llama\\decor\\white.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/decor/decor_yellow.png", "assets\\minecraft\\textures\\entity\\llama\\decor\\yellow.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/llama.png", "assets\\minecraft\\textures\\entity\\llama\\llama.png"); // Unknown.
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/llama_brown.png", "assets\\minecraft\\textures\\entity\\llama\\brown.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/llama_creamy.png", "assets\\minecraft\\textures\\entity\\llama\\creamy.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/llama_gray.png", "assets\\minecraft\\textures\\entity\\llama\\gray.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/llama_white.png", "assets\\minecraft\\textures\\entity\\llama\\white.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/llama/spit.png", "assets\\minecraft\\textures\\entity\\llama\\spit.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/ozelot.png", "assets\\minecraft\\textures\\entity\\cat\\ocelot.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/parrot/parrot_blue.png", "assets\\minecraft\\textures\\entity\\parrot\\parrot_blue.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/parrot/parrot_green.png", "assets\\minecraft\\textures\\entity\\parrot\\parrot_green.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/parrot/parrot_grey.png", "assets\\minecraft\\textures\\entity\\parrot\\parrot_grey.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/parrot/parrot_red_blue.png", "assets\\minecraft\\textures\\entity\\parrot\\parrot_red_blue.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/parrot/parrot_yellow_blue.png", "assets\\minecraft\\textures\\entity\\parrot\\parrot_yellow_blue.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/phantom.png", "assets\\minecraft\\textures\\entity\\phantom.png");
                    //Diccionario_Rutas_Recursos.Add("res/mob/phantom.tga", "assets\\minecraft\\textures\\entity\\phantom.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/pig.png", "assets\\minecraft\\textures\\entity\\pig\\pig.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/pigzombie.png", "assets\\minecraft\\textures\\entity\\zombie_pigman.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/rabbit/black.png", "assets\\minecraft\\textures\\entity\\rabbit\\black.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/rabbit/brown.png", "assets\\minecraft\\textures\\entity\\rabbit\\brown.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/rabbit/caerbannog.png", "assets\\minecraft\\textures\\entity\\rabbit\\caerbannog.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/rabbit/gold.png", "assets\\minecraft\\textures\\entity\\rabbit\\gold.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/rabbit/salt.png", "assets\\minecraft\\textures\\entity\\rabbit\\salt.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/rabbit/toast.png", "assets\\minecraft\\textures\\entity\\rabbit\\toast.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/rabbit/white.png", "assets\\minecraft\\textures\\entity\\rabbit\\white.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/rabbit/white_splotched.png", "assets\\minecraft\\textures\\entity\\rabbit\\white_splotched.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/redcow.png", "assets\\minecraft\\textures\\entity\\cow\\red_mooshroom.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/saddle.png", "assets\\minecraft\\textures\\entity\\pig\\pig_saddle.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/sea_turtle.png", "assets\\minecraft\\textures\\entity\\turtle\\big_sea_turtle.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/sheep.png", "assets\\minecraft\\textures\\entity\\sheep\\sheep.png");
                    //Diccionario_Rutas_Recursos.Add("res/mob/sheep.tga", "assets\\minecraft\\textures\\entity\\sheep\\sheep.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/sheep_fur.png", "assets\\minecraft\\textures\\entity\\sheep\\sheep_fur.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/shulker/endergolem.png", "assets\\minecraft\\textures\\entity\\shulker\\shulker.png");
                    //Diccionario_Rutas_Recursos.Add("res/mob/shulker/endergolem.tga", "assets\\minecraft\\textures\\entity\\shulker\\shulker.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/shulker/spark.png", "assets\\minecraft\\textures\\entity\\shulker\\spark.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/silverfish.png", "assets\\minecraft\\textures\\entity\\silverfish.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/skeleton.png", "assets\\minecraft\\textures\\entity\\skeleton\\skeleton.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/skeleton/stray.png", "assets\\minecraft\\textures\\entity\\skeleton\\stray.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/skeleton/stray_overlay.png", "assets\\minecraft\\textures\\entity\\skeleton\\stray_overlay.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/skeleton_wither.png", "assets\\minecraft\\textures\\entity\\skeleton\\wither_skeleton.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/slime.png", "assets\\minecraft\\textures\\entity\\slime\\slime.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/snowman.png", "assets\\minecraft\\textures\\entity\\snow_golem.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/spider.png", "assets\\minecraft\\textures\\entity\\spider\\spider.png");
                    //Diccionario_Rutas_Recursos.Add("res/mob/spider.tga", "assets\\minecraft\\textures\\entity\\spider\\spider.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/spider_eyes.png", "assets\\minecraft\\textures\\entity\\spider\\spider_eyes.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/squid.png", "assets\\minecraft\\textures\\entity\\squid.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/steve.png", "assets\\minecraft\\textures\\entity\\steve.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/villager/butcher.png", "assets\\minecraft\\textures\\entity\\villager\\profession\\butcher.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/villager/farmer.png", "assets\\minecraft\\textures\\entity\\villager\\profession\\farmer.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/villager/librarian.png", "assets\\minecraft\\textures\\entity\\villager\\profession\\librarian.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/villager/priest.png", "assets\\minecraft\\textures\\entity\\villager\\profession\\cleric.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/villager/smith.png", "assets\\minecraft\\textures\\entity\\villager\\profession\\weaponsmith.png"); // ...
                    Diccionario_Rutas_Recursos.Add("res/mob/villager/villager.png", "assets\\minecraft\\textures\\entity\\villager\\villager.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/villager_golem.png", "assets\\minecraft\\textures\\entity\\iron_golem.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/witch.png", "assets\\minecraft\\textures\\entity\\witch.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/wither/wither.png", "assets\\minecraft\\textures\\entity\\wither\\wither.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/wither/wither_armor.png", "assets\\minecraft\\textures\\entity\\wither\\wither_armor.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/wither/wither_invulnerable.png", "assets\\minecraft\\textures\\entity\\wither\\wither_invulnerable.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/wolf.png", "assets\\minecraft\\textures\\entity\\wolf\\wolf.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/wolf_angry.png", "assets\\minecraft\\textures\\entity\\wolf\\wolf_angry.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/wolf_collar.png", "assets\\minecraft\\textures\\entity\\wolf\\wolf_collar.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/wolf_tame.png", "assets\\minecraft\\textures\\entity\\wolf\\wolf_tame.png");
                    //Diccionario_Rutas_Recursos.Add("res/mob/wolf_tame.tga", "assets\\minecraft\\textures\\entity\\wolf\\wolf_tame.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/zombie.png", "assets\\minecraft\\textures\\entity\\zombie\\zombie.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/zombie/drowned.png", "assets\\minecraft\\textures\\entity\\zombie\\drowned.png");
                    //Diccionario_Rutas_Recursos.Add("res/mob/zombie/drowned.tga", "assets\\minecraft\\textures\\entity\\zombie\\drowned.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/zombie/husk.png", "assets\\minecraft\\textures\\entity\\zombie\\husk.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/zombie_villager/zombie_butcher.png", "assets\\minecraft\\textures\\entity\\zombie_villager\\profession\\butcher.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/zombie_villager/zombie_farmer.png", "assets\\minecraft\\textures\\entity\\zombie_villager\\profession\\farmer.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/zombie_villager/zombie_librarian.png", "assets\\minecraft\\textures\\entity\\zombie_villager\\profession\\librarian.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/zombie_villager/zombie_priest.png", "assets\\minecraft\\textures\\entity\\zombie_villager\\profession\\cleric.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/zombie_villager/zombie_smith.png", "assets\\minecraft\\textures\\entity\\zombie_villager\\profession\\weaponsmith.png");
                    Diccionario_Rutas_Recursos.Add("res/mob/zombie_villager/zombie_villager.png", "assets\\minecraft\\textures\\entity\\zombie_villager\\zombie_villager.png");
                    Diccionario_Rutas_Recursos.Add("res/particles.png", "assets\\minecraft\\textures\\unknown\\particles.png");
                    Diccionario_Rutas_Recursos.Add("res/terrain.png", "assets\\minecraft\\textures\\unknown\\terrain.png");
                    Diccionario_Rutas_Recursos.Add("res/terrain/moon.png", "assets\\minecraft\\textures\\environment\\moon.png");
                    Diccionario_Rutas_Recursos.Add("res/terrain/moon_phases.png", "assets\\minecraft\\textures\\environment\\moon_phases.png");
                    Diccionario_Rutas_Recursos.Add("res/terrain/sun.png", "assets\\minecraft\\textures\\environment\\sun.png");
                    Diccionario_Rutas_Recursos.Add("res/terrainMipMapLevel2.png", "assets\\minecraft\\textures\\block\\_terrainMipMapLevel2.png");
                    Diccionario_Rutas_Recursos.Add("res/terrainMipMapLevel3.png", "assets\\minecraft\\textures\\block\\_terrainMipMapLevel3.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/cactus_side.png", "assets\\minecraft\\textures\\block\\cactus_side.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/cauldron_water.png", "assets\\minecraft\\textures\\block\\_cauldron_water.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/deadbush.png", "assets\\minecraft\\textures\\block\\dead_bush.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/diamond_block.png", "assets\\minecraft\\textures\\block\\diamond_block.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/diamond_ore.png", "assets\\minecraft\\textures\\block\\diamond_ore.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/double_plant_sunflower_front.png", "assets\\minecraft\\textures\\block\\sunflower_front.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/emerald_block.png", "assets\\minecraft\\textures\\block\\emerald_block.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/emerald_ore.png", "assets\\minecraft\\textures\\block\\emerald_ore.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/endframe_eye.png", "assets\\minecraft\\textures\\block\\end_portal_frame_eye.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/fire_0.png", "assets\\minecraft\\textures\\block\\fire_0.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/fire_1.png", "assets\\minecraft\\textures\\block\\fire_1.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/fire_1MipMapLevel2.png", "assets\\minecraft\\textures\\block\\_fire_1MipMapLevel2.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/fire_1MipMapLevel3.png", "assets\\minecraft\\textures\\block\\_fire_1MipMapLevel3.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/flower_dandelion.png", "assets\\minecraft\\textures\\block\\dandelion.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/flower_rose.png", "assets\\minecraft\\textures\\block\\poppy.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/furnace_front_lit.png", "assets\\minecraft\\textures\\block\\furnace_front_on.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/glowstone.png", "assets\\minecraft\\textures\\block\\glowstone.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/kelp_a.png", "assets\\minecraft\\textures\\block\\kelp_plant.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/kelp_top_a.png", "assets\\minecraft\\textures\\block\\kelp.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/lava.png", "assets\\minecraft\\textures\\block\\lava_still.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/lava_flow.png", "assets\\minecraft\\textures\\block\\lava_flow.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/lava_flowMipMapLevel2.png", "assets\\minecraft\\textures\\block\\_lava_flowMipMapLevel2.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/lava_flowMipMapLevel3.png", "assets\\minecraft\\textures\\block\\_lava_flowMipMapLevel3.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/lavaMipMapLevel2.png", "assets\\minecraft\\textures\\block\\_lavaMipMapLevel2.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/lavaMipMapLevel3.png", "assets\\minecraft\\textures\\block\\_lavaMipMapLevel3.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/log_jungle.png", "assets\\minecraft\\textures\\block\\jungle_log.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/log_spruce.png", "assets\\minecraft\\textures\\block\\spruce_log.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/magma.png", "assets\\minecraft\\textures\\block\\magma.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/mushroom_red.png", "assets\\minecraft\\textures\\block\\red_mushroom.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/obsidian.png", "assets\\minecraft\\textures\\block\\obsidian.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/portal.png", "assets\\minecraft\\textures\\block\\nether_portal.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/portalMipMapLevel2.png", "assets\\minecraft\\textures\\block\\_portalMipMapLevel2.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/portalMipMapLevel3.png", "assets\\minecraft\\textures\\block\\_portalMipMapLevel3.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/prismarine_rough.png", "assets\\minecraft\\textures\\block\\prismarine.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/pumpkin_face_on.png", "assets\\minecraft\\textures\\block\\jack_o_lantern.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/redstone_ore.png", "assets\\minecraft\\textures\\block\\redstone_ore.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/redstoneLight_lit.png", "assets\\minecraft\\textures\\block\\redstone_lamp_on.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/sea_lantern.png", "assets\\minecraft\\textures\\block\\sea_lantern.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/seagrass.png", "assets\\minecraft\\textures\\block\\seagrass.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/seagrass_doubletall_bottom.png", "assets\\minecraft\\textures\\block\\tall_seagrass_bottom.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/seagrass_doubletall_bottom_a.png", "assets\\minecraft\\textures\\block\\tall_seagrass_bottom.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/seagrass_doubletall_top.png", "assets\\minecraft\\textures\\block\\tall_seagrass_top.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/seagrass_doubletall_top_a.png", "assets\\minecraft\\textures\\block\\tall_seagrass_top.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/soul_sand.png", "assets\\minecraft\\textures\\block\\soul_sand.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/vine.png", "assets\\minecraft\\textures\\block\\vine.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/vineMipMapLevel2.png", "assets\\minecraft\\textures\\block\\_vineMipMapLevel2.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/vineMipMapLevel3.png", "assets\\minecraft\\textures\\block\\_vineMipMapLevel3.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/water.png", "assets\\minecraft\\textures\\block\\water_still.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/water_flow.png", "assets\\minecraft\\textures\\block\\water_flow.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/water_flowMipMapLevel2.png", "assets\\minecraft\\textures\\block\\_water_flowMipMapLevel2.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/water_flowMipMapLevel3.png", "assets\\minecraft\\textures\\block\\_water_flowMipMapLevel3.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/waterMipMapLevel2.png", "assets\\minecraft\\textures\\block\\_waterMipMapLevel2.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/waterMipMapLevel3.png", "assets\\minecraft\\textures\\block\\_waterMipMapLevel3.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/wool_colored_green.png", "assets\\minecraft\\textures\\block\\green_wool.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/blocks/wool_colored_pink.png", "assets\\minecraft\\textures\\block\\pink_wool.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/destroy_stage_0.png", "assets\\minecraft\\textures\\block\\destroy_stage_0.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/destroy_stage_1.png", "assets\\minecraft\\textures\\block\\destroy_stage_1.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/destroy_stage_2.png", "assets\\minecraft\\textures\\block\\destroy_stage_2.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/destroy_stage_3.png", "assets\\minecraft\\textures\\block\\destroy_stage_3.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/destroy_stage_4.png", "assets\\minecraft\\textures\\block\\destroy_stage_4.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/destroy_stage_5.png", "assets\\minecraft\\textures\\block\\destroy_stage_5.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/destroy_stage_6.png", "assets\\minecraft\\textures\\block\\destroy_stage_6.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/destroy_stage_7.png", "assets\\minecraft\\textures\\block\\destroy_stage_7.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/destroy_stage_8.png", "assets\\minecraft\\textures\\block\\destroy_stage_8.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/destroy_stage_9.png", "assets\\minecraft\\textures\\block\\destroy_stage_9.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/items/clock.png", "assets\\minecraft\\textures\\item\\clock_"); // Post-process.
                    Diccionario_Rutas_Recursos.Add("res/textures/items/clockMipMapLevel2.png", "assets\\minecraft\\textures\\item\\_clockMipMapLevel2.png");
                    Diccionario_Rutas_Recursos.Add("res/textures/items/compass.png", "assets\\minecraft\\textures\\item\\compass_"); // Post-process.
                    Diccionario_Rutas_Recursos.Add("res/textures/items/compassMipMapLevel2.png", "assets\\minecraft\\textures\\item\\_compassMipMapLevel2.png");

                    // Check for any repeated names. Result: "lava" as magmacube and "lava" liquid block.
                    /*string Errores = null;
                    int ii = 0;
                    foreach (KeyValuePair<string, string> Entrada in Diccionario_Rutas_Recursos)
                    {
                        int jj = 0;
                        foreach (KeyValuePair<string, string> Subentrada in Diccionario_Rutas_Recursos)
                        {
                            if (ii != jj)
                            {
                                if (string.Compare(Entrada.Key, Subentrada.Key, true) == 0)
                                {
                                    Errores += Entrada.Key;
                                    break;
                                }
                            }
                            jj++;
                        }
                        ii++;
                    }
                    if (!string.IsNullOrEmpty(Errores)) MessageBox.Show(this, Errores);
                    return;*/

                    // Now for each known resource pack name generate the full pack.
                    if (Lista_Packs_Recursos != null && Lista_Packs_Recursos.Count > 0)
                    {
                        foreach (string Nombre_Pack in Lista_Packs_Recursos)
                        {
                            string Ruta_Pack_Entrada = Ruta_Entrada + "\\" + Nombre_Pack + "\\";
                            for (int Índice_Resolución = 16; Índice_Resolución <= 1024; Índice_Resolución *= 2)
                            {
                                if (Directory.Exists(Ruta_Pack_Entrada + "x" + Índice_Resolución.ToString() + "Data"))
                                {
                                    Ruta_Pack_Entrada += "x" + Índice_Resolución.ToString() + "Data";
                                    break;
                                }
                            }
                            if (Directory.Exists(Ruta_Pack_Entrada))
                            {
                                string Ruta_Pack_Salida = Ruta_Salida + "\\" + Nombre_Pack;
                                Program.Crear_Carpetas(Ruta_Pack_Salida);
                                string[] Matriz_Rutas = Directory.GetFiles(Ruta_Pack_Entrada, "*.png", SearchOption.TopDirectoryOnly);
                                if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                                {
                                    if (Matriz_Rutas.Length > 1) Array.Sort(Matriz_Rutas);
                                    // First move at the start of the array images like "terrain" and "items" to
                                    // overwrite some of those textures at the end, since there are some animated.
                                    List<string> Lista_Temporal = new List<string>(Matriz_Rutas);
                                    List<string> Lista_Inicial = new List<string>(Matriz_Rutas);
                                    for (int Índice_Temporal = Lista_Temporal.Count - 1; Índice_Temporal >= 0; Índice_Temporal--)
                                    {
                                        if (string.Compare(Path.GetFileNameWithoutExtension(Lista_Temporal[Índice_Temporal]), "items", true) == 0 ||
                                            string.Compare(Path.GetFileNameWithoutExtension(Lista_Temporal[Índice_Temporal]), "particles", true) == 0 ||
                                            string.Compare(Path.GetFileNameWithoutExtension(Lista_Temporal[Índice_Temporal]), "terrain", true) == 0)
                                        {
                                            Lista_Inicial.Add(Lista_Temporal[Índice_Temporal]);
                                            Lista_Temporal.RemoveAt(Índice_Temporal);
                                        }
                                    }
                                    Lista_Temporal.InsertRange(0, Lista_Inicial);
                                    Matriz_Rutas = Lista_Temporal.ToArray(); // This is the newly "sorted" array.
                                    Lista_Inicial = null;
                                    Lista_Temporal = null;
                                    foreach (string Ruta_Recurso_Entrada in Matriz_Rutas)
                                    {
                                        string Nombre_Ruta = Path.GetFileNameWithoutExtension(Ruta_Recurso_Entrada);
                                        foreach (KeyValuePair<string, string> Entrada in Diccionario_Rutas_Recursos)
                                        {
                                            string Nombre_Recurso = Path.GetFileNameWithoutExtension(Entrada.Key);
                                            string Ruta_Recurso_Salida = null; // Used below.
                                            if (string.Compare(Nombre_Recurso, Nombre_Ruta, true) == 0)
                                            {
                                                // Now see if this is one of the images that need post-processing.
                                                if (string.Compare(Nombre_Recurso, "banner_base", true) == 0)
                                                {
                                                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta_Recurso_Entrada, CheckState.Indeterminate);
                                                    if (Imagen != null)
                                                    {
                                                        int Ancho = Imagen.Width;
                                                        int Alto = Imagen.Height;
                                                        int Multiplicador = Ancho / 64;
                                                        BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Ancho), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                                                        byte[] Matriz_Bytes_ARGB = new byte[Math.Abs(Bitmap_Data.Stride) * Ancho];
                                                        Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes_ARGB, 0, Matriz_Bytes_ARGB.Length);
                                                        int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                                                        int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                                                        for (int Y = 0, Índice = 0; Y < Ancho; Y++, Índice += Bytes_Diferencia)
                                                        {
                                                            for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                                                            {
                                                                if (X < 42 * Multiplicador && Y < 42 * Multiplicador)
                                                                {
                                                                    // Turn some banner pixels into gray scale so they can be recolored in game.
                                                                    Matriz_Bytes_ARGB[Índice] = (byte)((Matriz_Bytes_ARGB[Índice + 2] + Matriz_Bytes_ARGB[Índice + 1] + Matriz_Bytes_ARGB[Índice]) / 3);
                                                                    Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                                                    Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];
                                                                }
                                                            }
                                                        }
                                                        Marshal.Copy(Matriz_Bytes_ARGB, 0, Bitmap_Data.Scan0, Matriz_Bytes_ARGB.Length);
                                                        Imagen.UnlockBits(Bitmap_Data);
                                                        Bitmap_Data = null;
                                                        Matriz_Bytes_ARGB = null;

                                                        // Now save this new gray scale image.
                                                        Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\" + Path.GetDirectoryName(Entrada.Value) + "\\" + Path.GetFileNameWithoutExtension(Entrada.Value) + ".png";
                                                        Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                        Imagen.Save(Ruta_Recurso_Salida, ImageFormat.Png);
                                                        Imagen.Dispose();
                                                        Imagen = null;
                                                    }
                                                }
                                                else if (string.Compare(Nombre_Recurso, "beacon_beam", true) == 0 ||
                                                    string.Compare(Nombre_Recurso, "end_gateway_beam", true) == 0)
                                                {
                                                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta_Recurso_Entrada, CheckState.Indeterminate);
                                                    if (Imagen != null)
                                                    {
                                                        int Ancho = Imagen.Width;
                                                        int Alto = Imagen.Height;
                                                        BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Ancho), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                                                        byte[] Matriz_Bytes_ARGB = new byte[Math.Abs(Bitmap_Data.Stride) * Ancho];
                                                        Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes_ARGB, 0, Matriz_Bytes_ARGB.Length);
                                                        int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                                                        int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                                                        for (int Y = 0, Índice = 0; Y < Ancho; Y++, Índice += Bytes_Diferencia)
                                                        {
                                                            for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                                                            {
                                                                // Turn all the pixels into gray scale so they can be recolored in game.
                                                                Matriz_Bytes_ARGB[Índice] = (byte)((Matriz_Bytes_ARGB[Índice + 2] + Matriz_Bytes_ARGB[Índice + 1] + Matriz_Bytes_ARGB[Índice]) / 3);
                                                                Matriz_Bytes_ARGB[Índice + 1] = Matriz_Bytes_ARGB[Índice];
                                                                Matriz_Bytes_ARGB[Índice + 2] = Matriz_Bytes_ARGB[Índice];
                                                            }
                                                        }
                                                        Marshal.Copy(Matriz_Bytes_ARGB, 0, Bitmap_Data.Scan0, Matriz_Bytes_ARGB.Length);
                                                        Imagen.UnlockBits(Bitmap_Data);
                                                        Bitmap_Data = null;
                                                        Matriz_Bytes_ARGB = null;

                                                        // Now save this new gray scale image.
                                                        Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\" + Path.GetDirectoryName(Entrada.Value) + "\\" + Path.GetFileNameWithoutExtension(Entrada.Value) + ".png";
                                                        Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                        Imagen.Save(Ruta_Recurso_Salida, ImageFormat.Png);
                                                        Imagen.Dispose();
                                                        Imagen = null;
                                                    }
                                                }
                                                else if (string.Compare(Nombre_Recurso, "clock", true) == 0 || string.Compare(Nombre_Recurso, "compass", true) == 0)
                                                {
                                                    // Split the large image into multiple unique images.
                                                    Bitmap Imagen_Original = Program.Cargar_Imagen_Ruta(Ruta_Recurso_Entrada, CheckState.Indeterminate);
                                                    if (Imagen_Original != null)
                                                    {
                                                        int Ancho = Imagen_Original.Width;
                                                        int Alto = Imagen_Original.Height;
                                                        for (int Índice_Y = 0, Índice = 0; Índice_Y < Alto; Índice_Y += Ancho, Índice++)
                                                        {
                                                            Bitmap Imagen = new Bitmap(Ancho, Ancho, Imagen_Original.PixelFormat);
                                                            Graphics Pintar = Graphics.FromImage(Imagen);
                                                            Pintar.CompositingMode = CompositingMode.SourceCopy;
                                                            Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                                            Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                                                            Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                                            Pintar.SmoothingMode = SmoothingMode.None;
                                                            Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                                                            Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Ancho), new Rectangle(0, Índice_Y, Ancho, Ancho), GraphicsUnit.Pixel);
                                                            Pintar.Dispose();
                                                            Pintar = null;
                                                            string Número = Índice.ToString();
                                                            while (Número.Length < 2) Número = '0' + Número;
                                                            Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\" + Entrada.Value;
                                                            Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                            Imagen.Save(Ruta_Recurso_Salida + Número + ".png", ImageFormat.Png);
                                                            Imagen.Dispose();
                                                            Imagen = null;
                                                        }
                                                        Imagen_Original.Dispose();
                                                        Imagen_Original = null;
                                                    }
                                                }
                                                /*else if (string.Compare(Nombre, "compass", true) == 0)
                                                {
                                                    // Split the image into 64 unique compasses.

                                                }*/
                                                /*else if (string.Compare(Nombre_Recurso, "dolphin", true) == 0)
                                                {
                                                    // This texture seems to have all parts in wrong places, so try to correct that.
                                                    Bitmap Imagen_Original = Program.Cargar_Imagen_Ruta(Ruta_Recurso_Entrada, CheckState.Indeterminate);
                                                    if (Imagen_Original != null)
                                                    {
                                                        int Ancho = Imagen_Original.Width;
                                                        int Alto = Imagen_Original.Height;
                                                        int Resolución = Ancho / 64; // Get the resolution of this resource pack.
                                                        Bitmap Imagen = new Bitmap(Ancho, Alto, Imagen_Original.PixelFormat);
                                                        Graphics Pintar = Graphics.FromImage(Imagen);
                                                        Pintar.CompositingMode = CompositingMode.SourceOver; // Mix the rectangle parts.
                                                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                                        Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                                                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                                        Pintar.SmoothingMode = SmoothingMode.None;
                                                        Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                                                        // Now copy all the different parts on it's original places, like in Minecraft 1.14.4.
                                                        Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, 28 * Resolución, 13 * Resolución), new Rectangle(0, 0, 28 * Resolución, 13 * Resolución), GraphicsUnit.Pixel);
                                                        Pintar.DrawImage(Imagen_Original, new Rectangle(0, 13 * Resolución, 13 * Resolución, 6 * Resolución), new Rectangle(0, 13 * Resolución, 13 * Resolución, 6 * Resolución), GraphicsUnit.Pixel);
                                                        Pintar.DrawImage(Imagen_Original, new Rectangle(0, 19 * Resolución, 30 * Resolución, 16 * Resolución), new Rectangle(0, 33 * Resolución, 30 * Resolución, 16 * Resolución), GraphicsUnit.Pixel);
                                                        Pintar.DrawImage(Imagen_Original, new Rectangle(19 * Resolución, 20 * Resolución, 30 * Resolución, 6 * Resolución), new Rectangle(0, 49 * Resolución, 30 * Resolución, 6 * Resolución), GraphicsUnit.Pixel);
                                                        Pintar.DrawImage(Imagen_Original, new Rectangle(19 * Resolución, 26 * Resolución, 32 * Resolución, 1 * Resolución), new Rectangle(0, 55 * Resolución, 32 * Resolución, 1 * Resolución), GraphicsUnit.Pixel);
                                                        Pintar.DrawImage(Imagen_Original, new Rectangle(35 * Resolución, 0, 16 * Resolución, 13 * Resolución), new Rectangle(13 * Resolución, 13 * Resolución, 16 * Resolución, 13 * Resolución), GraphicsUnit.Pixel);
                                                        Pintar.DrawImage(Imagen_Original, new Rectangle(22 * Resolución, 13 * Resolución, 42 * Resolución, 7 * Resolución), new Rectangle(0, 26 * Resolución, 42 * Resolución, 7 * Resolución), GraphicsUnit.Pixel);
                                                        // Note: 2 texture parts are different on each image and won't fit well.
                                                        // After test it, they seem to be missing on the top fin of the dolphin.
                                                        // Also the 2 lateral fins were fully missing.
                                                        // But even with that still looks a lot better than without doing nothing.
                                                        //Pintar.DrawImage(Imagen_Original, new Rectangle(51 * Resolución, 0, 12 * Resolución, 9 * Resolución), new Rectangle(28 * Resolución, 0, 12 * Resolución, 9 * Resolución), GraphicsUnit.Pixel); // Doesn't fit.
                                                        // This 5 lines of code fixed the missing part from the top fin.
                                                        Pintar.DrawImage(Imagen_Original, new Rectangle(51 * Resolución, 5 * Resolución, 4 * Resolución, 4 * Resolución), new Rectangle(29 * Resolución, 5 * Resolución, 4 * Resolución, 4 * Resolución), GraphicsUnit.Pixel); // Left part.
                                                        Pintar.DrawImage(Imagen_Original, new Rectangle(59 * Resolución, 5 * Resolución, 4 * Resolución, 4 * Resolución), new Rectangle(35 * Resolución, 5 * Resolución, 4 * Resolución, 4 * Resolución), GraphicsUnit.Pixel); // Right part.
                                                        Pintar.DrawImage(Imagen_Original, new Rectangle(56 * Resolución, 0, 2 * Resolución, 9 * Resolución), new Rectangle(33 * Resolución, 0, 2 * Resolución, 9 * Resolución), GraphicsUnit.Pixel); // Center.
                                                        Pintar.DrawImage(Imagen_Original, new Rectangle(55 * Resolución, 5 * Resolución, 1 * Resolución, 4 * Resolución), new Rectangle(32 * Resolución, 5 * Resolución, 1 * Resolución, 4 * Resolución), GraphicsUnit.Pixel); // Left bit.
                                                        Pintar.DrawImage(Imagen_Original, new Rectangle(58 * Resolución, 5 * Resolución, 1 * Resolución, 4 * Resolución), new Rectangle(35 * Resolución, 5 * Resolución, 1 * Resolución, 4 * Resolución), GraphicsUnit.Pixel); // right bit.
                                                        // So the other 2 missing must be the lateral fins, also fix this.
                                                        Pintar.DrawImage(Imagen_Original, new Rectangle(48 * Resolución, 27 * Resolución, 16 * Resolución, 4 * Resolución), new Rectangle(44 * Resolución, 0, 16 * Resolución, 4 * Resolución), GraphicsUnit.Pixel);
                                                        Pintar.DrawImage(Imagen_Original, new Rectangle(55 * Resolución, 20 * Resolución, 2 * Resolución, 5 * Resolución), new Rectangle(51 * Resolución, 0, 2 * Resolución, 5 * Resolución), GraphicsUnit.Pixel);
                                                        Pintar.DrawImage(Imagen_Original, new Rectangle(55 * Resolución, 25 * Resolución, 2 * Resolución, 1 * Resolución), new Rectangle(51 * Resolución, 4 * Resolución, 2 * Resolución, 1 * Resolución), GraphicsUnit.Pixel);
                                                        Pintar.DrawImage(Imagen_Original, new Rectangle(55 * Resolución, 26 * Resolución, 2 * Resolución, 1 * Resolución), new Rectangle(51 * Resolución, 4 * Resolución, 2 * Resolución, 1 * Resolución), GraphicsUnit.Pixel);
                                                        // Results: the dolphin texture seems to be fully repaired.
                                                        Pintar.Dispose();
                                                        Pintar = null;
                                                        Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\" + Entrada.Value;
                                                        Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                        Imagen.Save(Ruta_Recurso_Salida, ImageFormat.Png);
                                                        Imagen.Dispose();
                                                        Imagen = null;
                                                    }
                                                }*/
                                                else if (string.Compare(Nombre_Recurso, "endergolem", true) == 0) // Shulker textures.
                                                {
                                                    Bitmap Imagen_Original = Program.Cargar_Imagen_Ruta(Ruta_Recurso_Entrada, CheckState.Indeterminate);
                                                    if (Imagen_Original != null)
                                                    {
                                                        int Ancho = Imagen_Original.Width;
                                                        int Alto = Imagen_Original.Height;
                                                        if (Ancho != Alto) // Large texture, split it, recolor it and mix the 2 parts.
                                                        {
                                                            // Start the 16 default dye colors and also add the default shulker color (more or less).
                                                            List<Color> Lista_Colores = new List<Color>(Minecraft_Source.Matriz_Colores_Tintes);
                                                            List<string> Lista_Nombres = new List<string>(Minecraft_Source.Matriz_Nombres_Tintes);
                                                            Lista_Colores.Add(Minecraft_Source.Color_Shulker);
                                                            Lista_Nombres.Add(null);
                                                            // Now make 16 copies of the image and recolor them with the original Minecraft dye colors.
                                                            for (int Índice_Tinte = 0; Índice_Tinte < Lista_Colores.Count; Índice_Tinte++)
                                                            {
                                                                Color Color_ARGB = Lista_Colores[Índice_Tinte];
                                                                Bitmap Imagen = Imagen_Original.Clone(new Rectangle(0, 0, Ancho, Ancho), Imagen_Original.PixelFormat); // It should already have alpha.
                                                                if (Imagen != null)
                                                                {
                                                                    BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Ancho), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                                                                    byte[] Matriz_Bytes_ARGB = new byte[Math.Abs(Bitmap_Data.Stride) * Ancho];
                                                                    Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes_ARGB, 0, Matriz_Bytes_ARGB.Length);
                                                                    int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                                                                    int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                                                                    for (int Y = 0, Índice = 0; Y < Ancho; Y++, Índice += Bytes_Diferencia)
                                                                    {
                                                                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                                                                        {
                                                                            if (Matriz_Bytes_ARGB[Índice + 3] > 0) // Not fully transparent.
                                                                            {
                                                                                int Mínimo = Math.Min(Matriz_Bytes_ARGB[Índice + 2], Math.Min(Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]));
                                                                                int Máximo = Math.Max(Matriz_Bytes_ARGB[Índice + 2], Math.Max(Matriz_Bytes_ARGB[Índice + 1], Matriz_Bytes_ARGB[Índice]));
                                                                                int Diferencia_Máxima = Máximo - Mínimo;

                                                                                if (Diferencia_Máxima < 16) // Assume it might not fully be gray scale.
                                                                                {
                                                                                    // Recolor the pixel based on it's gray value.
                                                                                    int Gris = (Matriz_Bytes_ARGB[Índice + 2] + Matriz_Bytes_ARGB[Índice + 1] + Matriz_Bytes_ARGB[Índice]) / 3; // Average gray color (mask).
                                                                                    int Rojo = (Color_ARGB.R * Gris) / 255;
                                                                                    int Verde = (Color_ARGB.G * Gris) / 255;
                                                                                    int Azul = (Color_ARGB.B * Gris) / 255;

                                                                                    if (Rojo < 0) Rojo = 0;
                                                                                    else if (Rojo > 255) Rojo = 255;
                                                                                    if (Verde < 0) Verde = 0;
                                                                                    else if (Verde > 255) Verde = 255;
                                                                                    if (Azul < 0) Azul = 0;
                                                                                    else if (Azul > 255) Azul = 255;

                                                                                    Matriz_Bytes_ARGB[Índice + 2] = (byte)Rojo;
                                                                                    Matriz_Bytes_ARGB[Índice + 1] = (byte)Verde;
                                                                                    Matriz_Bytes_ARGB[Índice] = (byte)Azul;
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    Marshal.Copy(Matriz_Bytes_ARGB, 0, Bitmap_Data.Scan0, Matriz_Bytes_ARGB.Length);
                                                                    Imagen.UnlockBits(Bitmap_Data);
                                                                    Bitmap_Data = null;
                                                                    Matriz_Bytes_ARGB = null;

                                                                    // Now draw over the top image the bottom half of the original image.
                                                                    Graphics Pintar = Graphics.FromImage(Imagen);
                                                                    Pintar.CompositingMode = CompositingMode.SourceOver;
                                                                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                                                    Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                                                                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                                                    Pintar.SmoothingMode = SmoothingMode.None;
                                                                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                                                                    Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto / 2), new Rectangle(0, Alto / 2, Ancho, Alto / 2), GraphicsUnit.Pixel);
                                                                    Pintar.Dispose();
                                                                    Pintar = null;

                                                                    // Now save this recolored and mixed copy of the image.
                                                                    Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\" + Path.GetDirectoryName(Entrada.Value) + "\\" + Path.GetFileNameWithoutExtension(Entrada.Value) + (!string.IsNullOrEmpty(Lista_Nombres[Índice_Tinte]) ? "_" + Lista_Nombres[Índice_Tinte].ToLowerInvariant() : null) + ".png";
                                                                    Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                                    Imagen.Save(Ruta_Recurso_Salida, ImageFormat.Png);
                                                                    Imagen.Dispose();
                                                                    Imagen = null;
                                                                }
                                                            }
                                                            Lista_Colores = null;
                                                            Lista_Nombres = null;
                                                        }
                                                        Imagen_Original.Dispose();
                                                        Imagen_Original = null;
                                                    }
                                                }
                                                else if (string.Compare(Nombre_Recurso, "black", true) == 0 ||
                                                    string.Compare(Nombre_Recurso, "brown", true) == 0 ||
                                                    string.Compare(Nombre_Recurso, "caerbannog", true) == 0 ||
                                                    string.Compare(Nombre_Recurso, "creeper", true) == 0 ||
                                                    string.Compare(Nombre_Recurso, "ghast", true) == 0 ||
                                                    string.Compare(Nombre_Recurso, "ghast_fire", true) == 0 ||
                                                    string.Compare(Nombre_Recurso, "gold", true) == 0 ||
                                                    string.Compare(Nombre_Recurso, "pig", true) == 0 ||
                                                    string.Compare(Nombre_Recurso, "salt", true) == 0 ||
                                                    string.Compare(Nombre_Recurso, "toast", true) == 0 ||
                                                    string.Compare(Nombre_Recurso, "white", true) == 0 ||
                                                    string.Compare(Nombre_Recurso, "white_splotched", true) == 0)
                                                {
                                                    // TODO: creeper, dolphin, etc, need a full re-edit...



                                                    Bitmap Imagen_Original = Program.Cargar_Imagen_Ruta(Ruta_Recurso_Entrada, CheckState.Indeterminate);
                                                    if (Imagen_Original != null)
                                                    {
                                                        int Ancho = Imagen_Original.Width;
                                                        int Alto = Imagen_Original.Height;
                                                        if (Ancho == Alto) // Too large texture, cut it's height by 2.
                                                        {
                                                            Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\" + Entrada.Value;
                                                            Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                            Bitmap Imagen = Imagen_Original.Clone(new Rectangle(0, 0, Ancho, Alto / 2), Imagen_Original.PixelFormat);
                                                            Imagen.Save(Ruta_Recurso_Salida, ImageFormat.Png);
                                                            Imagen.Dispose();
                                                            Imagen = null;
                                                        }
                                                        else // Valid texture, copy it.
                                                        {
                                                            byte[] Matriz_Bytes = Program.Obtener_Matriz_Bytes_Archivo(Ruta_Recurso_Entrada);
                                                            if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                                                            {
                                                                Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\" + Entrada.Value;
                                                                Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                                FileStream Lector = new FileStream(Ruta_Recurso_Salida, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                                                Lector.SetLength(0L);
                                                                Lector.Seek(0L, SeekOrigin.Begin);
                                                                Lector.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                                                Lector.Close();
                                                                Lector.Dispose();
                                                                Lector = null;
                                                                Matriz_Bytes = null;
                                                            }
                                                        }
                                                        Imagen_Original.Dispose();
                                                        Imagen_Original = null;
                                                    }
                                                }
                                                else if (string.Compare(Nombre_Recurso, "conduit_base", true) == 0)
                                                {
                                                    Bitmap Imagen_Original = Program.Cargar_Imagen_Ruta(Ruta_Recurso_Entrada, CheckState.Indeterminate);
                                                    if (Imagen_Original != null)
                                                    {
                                                        int Ancho = Imagen_Original.Width;
                                                        int Alto = Imagen_Original.Height;
                                                        int Resolución = Ancho / 24;
                                                        if (Ancho != Alto) // Short texture, double it's height.
                                                        {
                                                            Bitmap Imagen = new Bitmap(32 * Resolución, 16 * Resolución, Imagen_Original.PixelFormat);
                                                            Graphics Pintar = Graphics.FromImage(Imagen);
                                                            Pintar.CompositingMode = CompositingMode.SourceCopy;
                                                            Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                                            Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                                                            Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                                            Pintar.SmoothingMode = SmoothingMode.None;
                                                            Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                                                            Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, 24 * Resolución, 12 * Resolución), new Rectangle(0, 0, 24 * Resolución, 12 * Resolución), GraphicsUnit.Pixel);
                                                            Pintar.Dispose();
                                                            Pintar = null;
                                                            Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\" + Entrada.Value;
                                                            Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                            Imagen.Save(Ruta_Recurso_Salida, ImageFormat.Png);
                                                            Imagen.Dispose();
                                                            Imagen = null;
                                                        }
                                                        /*else // Equal texture, copy it.
                                                        {
                                                            byte[] Matriz_Bytes = Program.Obtener_Matriz_Bytes_Archivo(Ruta_Recurso_Entrada);
                                                            if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                                                            {
                                                                Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\" + Entrada.Value;
                                                                Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                                FileStream Lector = new FileStream(Ruta_Recurso_Salida, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                                                Lector.SetLength(0L);
                                                                Lector.Seek(0L, SeekOrigin.Begin);
                                                                Lector.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                                                Lector.Close();
                                                                Lector.Dispose();
                                                                Lector = null;
                                                                Matriz_Bytes = null;
                                                            }
                                                        }*/
                                                        Imagen_Original.Dispose();
                                                        Imagen_Original = null;
                                                    }
                                                }
                                                else if (string.Compare(Nombre_Recurso, "husk", true) == 0 ||
                                                    string.Compare(Nombre_Recurso, "pigzombie", true) == 0 ||
                                                    string.Compare(Nombre_Recurso, "zombie", true) == 0)
                                                {
                                                    Bitmap Imagen_Original = Program.Cargar_Imagen_Ruta(Ruta_Recurso_Entrada, CheckState.Indeterminate);
                                                    if (Imagen_Original != null)
                                                    {
                                                        int Ancho = Imagen_Original.Width;
                                                        int Alto = Imagen_Original.Height;
                                                        if (Ancho != Alto) // Short texture, double it's height.
                                                        {
                                                            Bitmap Imagen = new Bitmap(Ancho, Ancho, Imagen_Original.PixelFormat);
                                                            Graphics Pintar = Graphics.FromImage(Imagen);
                                                            Pintar.CompositingMode = CompositingMode.SourceCopy;
                                                            Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                                            Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                                                            Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                                            Pintar.SmoothingMode = SmoothingMode.None;
                                                            Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                                                            Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                                            Pintar.Dispose();
                                                            Pintar = null;
                                                            Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\" + Entrada.Value;
                                                            Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                            Imagen.Save(Ruta_Recurso_Salida, ImageFormat.Png);
                                                            Imagen.Dispose();
                                                            Imagen = null;
                                                        }
                                                        else // Equal texture, copy it.
                                                        {
                                                            byte[] Matriz_Bytes = Program.Obtener_Matriz_Bytes_Archivo(Ruta_Recurso_Entrada);
                                                            if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                                                            {
                                                                Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\" + Entrada.Value;
                                                                Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                                FileStream Lector = new FileStream(Ruta_Recurso_Salida, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                                                Lector.SetLength(0L);
                                                                Lector.Seek(0L, SeekOrigin.Begin);
                                                                Lector.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                                                Lector.Close();
                                                                Lector.Dispose();
                                                                Lector = null;
                                                                Matriz_Bytes = null;
                                                            }
                                                        }
                                                        Imagen_Original.Dispose();
                                                        Imagen_Original = null;
                                                    }
                                                }
                                                else if (string.Compare(Nombre_Recurso, "horse_armor_leather_1", true) == 0)
                                                {
                                                    // Fuse the 2 horse leather armor images.
                                                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta_Recurso_Entrada, CheckState.Indeterminate);
                                                    Bitmap Imagen_Original = Program.Cargar_Imagen_Ruta(Path.GetDirectoryName(Ruta_Recurso_Entrada) + "\\" + Path.GetFileNameWithoutExtension(Ruta_Recurso_Entrada) + "_b" + Path.GetExtension(Ruta_Recurso_Entrada), CheckState.Indeterminate);
                                                    if (Imagen != null && Imagen_Original != null)
                                                    {
                                                        Graphics Pintar = Graphics.FromImage(Imagen);
                                                        Pintar.CompositingMode = CompositingMode.SourceOver; // Mix the images.
                                                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                                        Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                                                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                                        Pintar.SmoothingMode = SmoothingMode.None;
                                                        Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                                                        Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Imagen.Width, Imagen.Height), new Rectangle(0, 0, Imagen_Original.Width, Imagen_Original.Height), GraphicsUnit.Pixel);
                                                        Pintar.Dispose();
                                                        Pintar = null;
                                                        Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\" + Entrada.Value;
                                                        Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                        Imagen.Save(Ruta_Recurso_Salida, ImageFormat.Png);
                                                        Imagen_Original.Dispose();
                                                        Imagen_Original = null;
                                                        Imagen.Dispose();
                                                        Imagen = null;
                                                    }
                                                }
                                                else if (string.Compare(Nombre_Recurso, "items", true) == 0)
                                                {
                                                    // Making those arrays by hand took a full day of work... but it was worth it at the end.
                                                    string[,] Matriz_Items = new string[16, 17];

                                                    Matriz_Items[0, 0] = "assets\\minecraft\\textures\\item\\leather_helmet.png";
                                                    Matriz_Items[1, 0] = "assets\\minecraft\\textures\\item\\chainmail_helmet.png";
                                                    Matriz_Items[2, 0] = "assets\\minecraft\\textures\\item\\iron_helmet.png";
                                                    Matriz_Items[3, 0] = "assets\\minecraft\\textures\\item\\diamond_helmet.png";
                                                    Matriz_Items[4, 0] = "assets\\minecraft\\textures\\item\\golden_helmet.png";
                                                    Matriz_Items[5, 0] = "assets\\minecraft\\textures\\item\\flint_and_steel.png";
                                                    Matriz_Items[6, 0] = "assets\\minecraft\\textures\\item\\flint.png";
                                                    Matriz_Items[7, 0] = "assets\\minecraft\\textures\\item\\coal.png";
                                                    Matriz_Items[8, 0] = "assets\\minecraft\\textures\\item\\string.png";
                                                    Matriz_Items[9, 0] = "assets\\minecraft\\textures\\item\\wheat_seeds.png";
                                                    Matriz_Items[10, 0] = "assets\\minecraft\\textures\\item\\apple.png";
                                                    Matriz_Items[11, 0] = "assets\\minecraft\\textures\\item\\golden_apple.png";
                                                    Matriz_Items[12, 0] = "assets\\minecraft\\textures\\item\\egg.png";
                                                    Matriz_Items[13, 0] = "assets\\minecraft\\textures\\item\\sugar.png";
                                                    Matriz_Items[14, 0] = "assets\\minecraft\\textures\\item\\snowball.png";
                                                    Matriz_Items[15, 0] = "assets\\minecraft\\textures\\item\\elytra.png";

                                                    Matriz_Items[0, 1] = "assets\\minecraft\\textures\\item\\leather_chestplate.png";
                                                    Matriz_Items[1, 1] = "assets\\minecraft\\textures\\item\\chainmail_chestplate.png";
                                                    Matriz_Items[2, 1] = "assets\\minecraft\\textures\\item\\iron_chestplate.png";
                                                    Matriz_Items[3, 1] = "assets\\minecraft\\textures\\item\\diamond_chestplate.png";
                                                    Matriz_Items[4, 1] = "assets\\minecraft\\textures\\item\\golden_chestplate.png";
                                                    Matriz_Items[5, 1] = "assets\\minecraft\\textures\\item\\bow.png";
                                                    Matriz_Items[6, 1] = "assets\\minecraft\\textures\\item\\brick.png";
                                                    Matriz_Items[7, 1] = "assets\\minecraft\\textures\\item\\iron_ingot.png";
                                                    Matriz_Items[8, 1] = "assets\\minecraft\\textures\\item\\feather.png";
                                                    Matriz_Items[9, 1] = "assets\\minecraft\\textures\\item\\wheat.png";
                                                    Matriz_Items[10, 1] = "assets\\minecraft\\textures\\item\\painting.png";
                                                    Matriz_Items[11, 1] = "assets\\minecraft\\textures\\item\\sugar_cane.png";
                                                    Matriz_Items[12, 1] = "assets\\minecraft\\textures\\item\\bone.png";
                                                    Matriz_Items[13, 1] = "assets\\minecraft\\textures\\item\\cake.png";
                                                    Matriz_Items[14, 1] = "assets\\minecraft\\textures\\item\\slime_ball.png";
                                                    Matriz_Items[15, 1] = "assets\\minecraft\\textures\\item\\broken_elytra.png";

                                                    Matriz_Items[0, 2] = "assets\\minecraft\\textures\\item\\leather_leggings.png";
                                                    Matriz_Items[1, 2] = "assets\\minecraft\\textures\\item\\chainmail_leggings.png";
                                                    Matriz_Items[2, 2] = "assets\\minecraft\\textures\\item\\iron_leggings.png";
                                                    Matriz_Items[3, 2] = "assets\\minecraft\\textures\\item\\diamond_leggings.png";
                                                    Matriz_Items[4, 2] = "assets\\minecraft\\textures\\item\\golden_leggings.png";
                                                    Matriz_Items[5, 2] = "assets\\minecraft\\textures\\item\\arrow.png";
                                                    Matriz_Items[6, 2] = "assets\\minecraft\\textures\\item\\end_crystal.png";
                                                    Matriz_Items[7, 2] = "assets\\minecraft\\textures\\item\\gold_ingot.png";
                                                    Matriz_Items[8, 2] = "assets\\minecraft\\textures\\item\\gunpowder.png";
                                                    Matriz_Items[9, 2] = "assets\\minecraft\\textures\\item\\bread.png";
                                                    Matriz_Items[10, 2] = "assets\\minecraft\\textures\\item\\oak_sign.png";
                                                    Matriz_Items[11, 2] = "assets\\minecraft\\textures\\item\\oak_door.png";
                                                    Matriz_Items[12, 2] = "assets\\minecraft\\textures\\item\\iron_door.png";
                                                    Matriz_Items[13, 2] = "assets\\minecraft\\textures\\item\\_bed.png"; // Unknown.
                                                    Matriz_Items[14, 2] = "assets\\minecraft\\textures\\item\\fire_charge.png";
                                                    Matriz_Items[15, 2] = "assets\\minecraft\\textures\\item\\chorus_fruit.png";

                                                    Matriz_Items[0, 3] = "assets\\minecraft\\textures\\item\\leather_boots.png";
                                                    Matriz_Items[1, 3] = "assets\\minecraft\\textures\\item\\chainmail_boots.png";
                                                    Matriz_Items[2, 3] = "assets\\minecraft\\textures\\item\\iron_boots.png";
                                                    Matriz_Items[3, 3] = "assets\\minecraft\\textures\\item\\diamond_boots.png";
                                                    Matriz_Items[4, 3] = "assets\\minecraft\\textures\\item\\golden_boots.png";
                                                    Matriz_Items[5, 3] = "assets\\minecraft\\textures\\item\\stick.png";
                                                    Matriz_Items[6, 3] = "assets\\minecraft\\textures\\item\\_compass.png"; // Unknown.
                                                    Matriz_Items[7, 3] = "assets\\minecraft\\textures\\item\\diamond.png";
                                                    Matriz_Items[8, 3] = "assets\\minecraft\\textures\\item\\redstone.png";
                                                    Matriz_Items[9, 3] = "assets\\minecraft\\textures\\item\\clay_ball.png";
                                                    Matriz_Items[10, 3] = "assets\\minecraft\\textures\\item\\paper.png";
                                                    Matriz_Items[11, 3] = "assets\\minecraft\\textures\\item\\book.png";
                                                    Matriz_Items[12, 3] = "assets\\minecraft\\textures\\item\\map.png";
                                                    Matriz_Items[13, 3] = "assets\\minecraft\\textures\\item\\pumpkin_seeds.png";
                                                    Matriz_Items[14, 3] = "assets\\minecraft\\textures\\item\\melon_seeds.png";
                                                    Matriz_Items[15, 3] = "assets\\minecraft\\textures\\item\\popped_chorus_fruit.png";

                                                    Matriz_Items[0, 4] = "assets\\minecraft\\textures\\item\\wooden_sword.png";
                                                    Matriz_Items[1, 4] = "assets\\minecraft\\textures\\item\\stone_sword.png";
                                                    Matriz_Items[2, 4] = "assets\\minecraft\\textures\\item\\iron_sword.png";
                                                    Matriz_Items[3, 4] = "assets\\minecraft\\textures\\item\\diamond_sword.png";
                                                    Matriz_Items[4, 4] = "assets\\minecraft\\textures\\item\\golden_sword.png";
                                                    Matriz_Items[5, 4] = "assets\\minecraft\\textures\\item\\fishing_rod.png";
                                                    Matriz_Items[6, 4] = "assets\\minecraft\\textures\\item\\_clock.png"; // Unknown.
                                                    Matriz_Items[7, 4] = "assets\\minecraft\\textures\\item\\bowl.png";
                                                    Matriz_Items[8, 4] = "assets\\minecraft\\textures\\item\\mushroom_stew.png";
                                                    Matriz_Items[9, 4] = "assets\\minecraft\\textures\\item\\glowstone_dust.png";
                                                    Matriz_Items[10, 4] = "assets\\minecraft\\textures\\item\\bucket.png";
                                                    Matriz_Items[11, 4] = "assets\\minecraft\\textures\\item\\water_bucket.png";
                                                    Matriz_Items[12, 4] = "assets\\minecraft\\textures\\item\\lava_bucket.png";
                                                    Matriz_Items[13, 4] = "assets\\minecraft\\textures\\item\\milk_bucket.png";
                                                    Matriz_Items[14, 4] = "assets\\minecraft\\textures\\item\\ink_sac.png";
                                                    Matriz_Items[15, 4] = "assets\\minecraft\\textures\\item\\gray_dye.png";

                                                    Matriz_Items[0, 5] = "assets\\minecraft\\textures\\item\\wooden_shovel.png";
                                                    Matriz_Items[1, 5] = "assets\\minecraft\\textures\\item\\stone_shovel.png";
                                                    Matriz_Items[2, 5] = "assets\\minecraft\\textures\\item\\iron_shovel.png";
                                                    Matriz_Items[3, 5] = "assets\\minecraft\\textures\\item\\diamond_shovel.png";
                                                    Matriz_Items[4, 5] = "assets\\minecraft\\textures\\item\\golden_shovel.png";
                                                    Matriz_Items[5, 5] = "assets\\minecraft\\textures\\item\\fishing_rod_cast.png";
                                                    Matriz_Items[6, 5] = "assets\\minecraft\\textures\\item\\repeater.png";
                                                    Matriz_Items[7, 5] = "assets\\minecraft\\textures\\item\\porkchop.png";
                                                    Matriz_Items[8, 5] = "assets\\minecraft\\textures\\item\\cooked_porkchop.png";
                                                    Matriz_Items[9, 5] = "assets\\minecraft\\textures\\item\\cod.png";
                                                    Matriz_Items[10, 5] = "assets\\minecraft\\textures\\item\\cooked_cod.png";
                                                    Matriz_Items[11, 5] = "assets\\minecraft\\textures\\item\\rotten_flesh.png";
                                                    Matriz_Items[12, 5] = "assets\\minecraft\\textures\\item\\cookie.png";
                                                    Matriz_Items[13, 5] = "assets\\minecraft\\textures\\item\\shears.png";
                                                    Matriz_Items[14, 5] = "assets\\minecraft\\textures\\item\\red_dye.png";
                                                    Matriz_Items[15, 5] = "assets\\minecraft\\textures\\item\\pink_dye.png";

                                                    Matriz_Items[0, 6] = "assets\\minecraft\\textures\\item\\wooden_pickaxe.png";
                                                    Matriz_Items[1, 6] = "assets\\minecraft\\textures\\item\\stone_pickaxe.png";
                                                    Matriz_Items[2, 6] = "assets\\minecraft\\textures\\item\\iron_pickaxe.png";
                                                    Matriz_Items[3, 6] = "assets\\minecraft\\textures\\item\\diamond_pickaxe.png";
                                                    Matriz_Items[4, 6] = "assets\\minecraft\\textures\\item\\golden_pickaxe.png";
                                                    Matriz_Items[5, 6] = "assets\\minecraft\\textures\\item\\bow_pulling_0.png";
                                                    Matriz_Items[6, 6] = "assets\\minecraft\\textures\\item\\carrot_on_a_stick.png";
                                                    Matriz_Items[7, 6] = "assets\\minecraft\\textures\\item\\leather.png";
                                                    Matriz_Items[8, 6] = "assets\\minecraft\\textures\\item\\saddle.png";
                                                    Matriz_Items[9, 6] = "assets\\minecraft\\textures\\item\\beef.png";
                                                    Matriz_Items[10, 6] = "assets\\minecraft\\textures\\item\\cooked_beef.png";
                                                    Matriz_Items[11, 6] = "assets\\minecraft\\textures\\item\\ender_pearl.png";
                                                    Matriz_Items[12, 6] = "assets\\minecraft\\textures\\item\\blaze_rod.png";
                                                    Matriz_Items[13, 6] = "assets\\minecraft\\textures\\item\\melon_slice.png";
                                                    Matriz_Items[14, 6] = "assets\\minecraft\\textures\\item\\green_dye.png";
                                                    Matriz_Items[15, 6] = "assets\\minecraft\\textures\\item\\lime_dye.png";

                                                    Matriz_Items[0, 7] = "assets\\minecraft\\textures\\item\\wooden_axe.png";
                                                    Matriz_Items[1, 7] = "assets\\minecraft\\textures\\item\\stone_axe.png";
                                                    Matriz_Items[2, 7] = "assets\\minecraft\\textures\\item\\iron_axe.png";
                                                    Matriz_Items[3, 7] = "assets\\minecraft\\textures\\item\\diamond_axe.png";
                                                    Matriz_Items[4, 7] = "assets\\minecraft\\textures\\item\\golden_axe.png";
                                                    Matriz_Items[5, 7] = "assets\\minecraft\\textures\\item\\bow_pulling_1.png";
                                                    Matriz_Items[6, 7] = "assets\\minecraft\\textures\\item\\baked_potato.png";
                                                    Matriz_Items[7, 7] = "assets\\minecraft\\textures\\item\\potato.png";
                                                    Matriz_Items[8, 7] = "assets\\minecraft\\textures\\item\\carrot.png";
                                                    Matriz_Items[9, 7] = "assets\\minecraft\\textures\\item\\chicken.png";
                                                    Matriz_Items[10, 7] = "assets\\minecraft\\textures\\item\\cooked_chicken.png";
                                                    Matriz_Items[11, 7] = "assets\\minecraft\\textures\\item\\ghast_tear.png";
                                                    Matriz_Items[12, 7] = "assets\\minecraft\\textures\\item\\gold_nugget.png";
                                                    Matriz_Items[13, 7] = "assets\\minecraft\\textures\\item\\nether_wart.png";
                                                    Matriz_Items[14, 7] = "assets\\minecraft\\textures\\item\\cocoa_beans.png";
                                                    Matriz_Items[15, 7] = "assets\\minecraft\\textures\\item\\yellow_dye.png";

                                                    Matriz_Items[0, 8] = "assets\\minecraft\\textures\\item\\wooden_hoe.png";
                                                    Matriz_Items[1, 8] = "assets\\minecraft\\textures\\item\\stone_hoe.png";
                                                    Matriz_Items[2, 8] = "assets\\minecraft\\textures\\item\\iron_hoe.png";
                                                    Matriz_Items[3, 8] = "assets\\minecraft\\textures\\item\\diamond_hoe.png";
                                                    Matriz_Items[4, 8] = "assets\\minecraft\\textures\\item\\golden_hoe.png";
                                                    Matriz_Items[5, 8] = "assets\\minecraft\\textures\\item\\bow_pulling_2.png";
                                                    Matriz_Items[6, 8] = "assets\\minecraft\\textures\\item\\poisonous_potato.png";
                                                    Matriz_Items[7, 8] = "assets\\minecraft\\textures\\item\\minecart.png";
                                                    Matriz_Items[8, 8] = "assets\\minecraft\\textures\\item\\oak_boat.png";
                                                    Matriz_Items[9, 8] = "assets\\minecraft\\textures\\item\\glistering_melon_slice.png";
                                                    Matriz_Items[10, 8] = "assets\\minecraft\\textures\\item\\fermented_spider_eye.png";
                                                    Matriz_Items[11, 8] = "assets\\minecraft\\textures\\item\\spider_eye.png";
                                                    Matriz_Items[12, 8] = "assets\\minecraft\\textures\\item\\potion.png";
                                                    Matriz_Items[13, 8] = "assets\\minecraft\\textures\\item\\potion_overlay.png";
                                                    Matriz_Items[14, 8] = "assets\\minecraft\\textures\\item\\lapis_lazuli.png";
                                                    Matriz_Items[15, 8] = "assets\\minecraft\\textures\\item\\light_blue_dye.png";

                                                    Matriz_Items[0, 9] = "assets\\minecraft\\textures\\item\\leather_helmet_overlay.png";
                                                    Matriz_Items[1, 9] = "assets\\minecraft\\textures\\item\\spectral_arrow.png";
                                                    Matriz_Items[2, 9] = "assets\\minecraft\\textures\\item\\iron_horse_armor.png";
                                                    Matriz_Items[3, 9] = "assets\\minecraft\\textures\\item\\diamond_horse_armor.png";
                                                    Matriz_Items[4, 9] = "assets\\minecraft\\textures\\item\\golden_horse_armor.png";
                                                    Matriz_Items[5, 9] = "assets\\minecraft\\textures\\item\\comparator.png";
                                                    Matriz_Items[6, 9] = "assets\\minecraft\\textures\\item\\golden_carrot.png";
                                                    Matriz_Items[7, 9] = "assets\\minecraft\\textures\\item\\chest_minecart.png";
                                                    Matriz_Items[8, 9] = "assets\\minecraft\\textures\\item\\pumpkin_pie.png";
                                                    Matriz_Items[9, 9] = "assets\\minecraft\\textures\\item\\spawn_egg.png";
                                                    Matriz_Items[10, 9] = "assets\\minecraft\\textures\\item\\splash_potion.png";
                                                    Matriz_Items[11, 9] = "assets\\minecraft\\textures\\item\\ender_eye.png";
                                                    Matriz_Items[12, 9] = "assets\\minecraft\\textures\\item\\cauldron.png";
                                                    Matriz_Items[13, 9] = "assets\\minecraft\\textures\\item\\blaze_powder.png";
                                                    Matriz_Items[14, 9] = "assets\\minecraft\\textures\\item\\purple_dye.png";
                                                    Matriz_Items[15, 9] = "assets\\minecraft\\textures\\item\\magenta_dye.png";

                                                    Matriz_Items[0, 10] = "assets\\minecraft\\textures\\item\\leather_chestplate_overlay.png";
                                                    Matriz_Items[1, 10] = "assets\\minecraft\\textures\\item\\tipped_arrow_base.png";
                                                    Matriz_Items[2, 10] = "assets\\minecraft\\textures\\item\\dragon_breath.png";
                                                    Matriz_Items[3, 10] = "assets\\minecraft\\textures\\item\\name_tag.png";
                                                    Matriz_Items[4, 10] = "assets\\minecraft\\textures\\item\\lead.png";
                                                    Matriz_Items[5, 10] = "assets\\minecraft\\textures\\item\\nether_brick.png";
                                                    Matriz_Items[6, 10] = "assets\\minecraft\\textures\\item\\tropical_fish.png";
                                                    Matriz_Items[7, 10] = "assets\\minecraft\\textures\\item\\furnace_minecart.png";
                                                    Matriz_Items[8, 10] = "assets\\minecraft\\textures\\item\\charcoal.png";
                                                    Matriz_Items[9, 10] = "assets\\minecraft\\textures\\item\\spawn_egg_overlay.png";
                                                    Matriz_Items[10, 10] = "assets\\minecraft\\textures\\item\\_bed_.png"; // Unknown.
                                                    Matriz_Items[11, 10] = "assets\\minecraft\\textures\\item\\experience_bottle.png";
                                                    Matriz_Items[12, 10] = "assets\\minecraft\\textures\\item\\brewing_stand.png";
                                                    Matriz_Items[13, 10] = "assets\\minecraft\\textures\\item\\magma_cream.png";
                                                    Matriz_Items[14, 10] = "assets\\minecraft\\textures\\item\\cyan_dye.png";
                                                    Matriz_Items[15, 10] = "assets\\minecraft\\textures\\item\\orange_dye.png";

                                                    Matriz_Items[0, 11] = "assets\\minecraft\\textures\\item\\leather_leggings_overlay.png";
                                                    Matriz_Items[1, 11] = "assets\\minecraft\\textures\\item\\tipped_arrow_head.png";
                                                    Matriz_Items[2, 11] = "assets\\minecraft\\textures\\item\\lingering_potion.png";
                                                    Matriz_Items[3, 11] = "assets\\minecraft\\textures\\item\\barrier.png";
                                                    Matriz_Items[4, 11] = "assets\\minecraft\\textures\\item\\mutton.png";
                                                    Matriz_Items[5, 11] = "assets\\minecraft\\textures\\item\\rabbit.png";
                                                    Matriz_Items[6, 11] = "assets\\minecraft\\textures\\item\\pufferfish.png";
                                                    Matriz_Items[7, 11] = "assets\\minecraft\\textures\\item\\hopper_minecart.png";
                                                    Matriz_Items[8, 11] = "assets\\minecraft\\textures\\item\\hopper.png";
                                                    Matriz_Items[9, 11] = "assets\\minecraft\\textures\\item\\nether_star.png";
                                                    Matriz_Items[10, 11] = "assets\\minecraft\\textures\\item\\emerald.png";
                                                    Matriz_Items[11, 11] = "assets\\minecraft\\textures\\item\\writable_book.png";
                                                    Matriz_Items[12, 11] = "assets\\minecraft\\textures\\item\\book.png";
                                                    Matriz_Items[13, 11] = "assets\\minecraft\\textures\\item\\flower_pot.png";
                                                    Matriz_Items[14, 11] = "assets\\minecraft\\textures\\item\\light_gray_dye.png";
                                                    Matriz_Items[15, 11] = "assets\\minecraft\\textures\\item\\bone_meal.png";

                                                    Matriz_Items[0, 12] = "assets\\minecraft\\textures\\item\\leather_boots_overlay.png";
                                                    Matriz_Items[1, 12] = "assets\\minecraft\\textures\\item\\beetroot.png";
                                                    Matriz_Items[2, 12] = "assets\\minecraft\\textures\\item\\beetroot_seeds.png";
                                                    Matriz_Items[3, 12] = "assets\\minecraft\\textures\\item\\beetroot_soup.png";
                                                    Matriz_Items[4, 12] = "assets\\minecraft\\textures\\item\\cooked_mutton.png";
                                                    Matriz_Items[5, 12] = "assets\\minecraft\\textures\\item\\cooked_rabbit.png";
                                                    Matriz_Items[6, 12] = "assets\\minecraft\\textures\\item\\salmon.png";
                                                    Matriz_Items[7, 12] = "assets\\minecraft\\textures\\item\\tnt_minecart.png";
                                                    Matriz_Items[8, 12] = "assets\\minecraft\\textures\\item\\armor_stand.png";
                                                    Matriz_Items[9, 12] = "assets\\minecraft\\textures\\item\\firework_rocket.png";
                                                    Matriz_Items[10, 12] = "assets\\minecraft\\textures\\item\\firework_star.png";
                                                    Matriz_Items[11, 12] = "assets\\minecraft\\textures\\item\\firework_star_overlay.png";
                                                    Matriz_Items[12, 12] = "assets\\minecraft\\textures\\item\\quartz.png";
                                                    Matriz_Items[13, 12] = "assets\\minecraft\\textures\\item\\filled_map.png";
                                                    Matriz_Items[14, 12] = "assets\\minecraft\\textures\\item\\item_frame.png";
                                                    Matriz_Items[15, 12] = "assets\\minecraft\\textures\\item\\enchanted_book.png";

                                                    Matriz_Items[0, 13] = "assets\\minecraft\\textures\\item\\acacia_door.png";
                                                    Matriz_Items[1, 13] = "assets\\minecraft\\textures\\item\\birch_door.png";
                                                    Matriz_Items[2, 13] = "assets\\minecraft\\textures\\item\\dark_oak_door.png";
                                                    Matriz_Items[3, 13] = "assets\\minecraft\\textures\\item\\jungle_door.png";
                                                    Matriz_Items[4, 13] = "assets\\minecraft\\textures\\item\\spruce_door.png";
                                                    Matriz_Items[5, 13] = "assets\\minecraft\\textures\\item\\rabbit_stew.png";
                                                    Matriz_Items[6, 13] = "assets\\minecraft\\textures\\item\\cooked_salmon.png";
                                                    Matriz_Items[7, 13] = "assets\\minecraft\\textures\\item\\command_block_minecart.png";
                                                    Matriz_Items[8, 13] = "assets\\minecraft\\textures\\item\\acacia_boat.png";
                                                    Matriz_Items[9, 13] = "assets\\minecraft\\textures\\item\\birch_boat.png";
                                                    Matriz_Items[10, 13] = "assets\\minecraft\\textures\\item\\dark_oak_boat.png";
                                                    Matriz_Items[11, 13] = "assets\\minecraft\\textures\\item\\jungle_boat.png";
                                                    Matriz_Items[12, 13] = "assets\\minecraft\\textures\\item\\spruce_boat.png";
                                                    Matriz_Items[13, 13] = "assets\\minecraft\\textures\\item\\prismarine_shard.png";
                                                    Matriz_Items[14, 13] = "assets\\minecraft\\textures\\item\\prismarine_crystals.png";
                                                    Matriz_Items[15, 13] = "assets\\minecraft\\textures\\item\\_leather_horse_armor.png"; // Unknown.

                                                    Matriz_Items[0, 14] = "assets\\minecraft\\textures\\item\\structure_void.png";
                                                    Matriz_Items[1, 14] = "assets\\minecraft\\textures\\item\\filled_map_markings.png";
                                                    Matriz_Items[2, 14] = "assets\\minecraft\\textures\\item\\totem_of_undying.png";
                                                    Matriz_Items[3, 14] = "assets\\minecraft\\textures\\item\\shulker_shell.png";
                                                    Matriz_Items[4, 14] = "assets\\minecraft\\textures\\item\\iron_nugget.png";
                                                    Matriz_Items[5, 14] = "assets\\minecraft\\textures\\item\\rabbit_foot.png";
                                                    Matriz_Items[6, 14] = "assets\\minecraft\\textures\\item\\rabbit_hide.png";
                                                    Matriz_Items[7, 14] = "assets\\minecraft\\textures\\item\\_item_7_14.png"; // Empty?
                                                    Matriz_Items[8, 14] = "assets\\minecraft\\textures\\item\\_item_8_14.png"; // Empty?
                                                    Matriz_Items[9, 14] = "assets\\minecraft\\textures\\item\\_item_9_14.png"; // Empty?
                                                    Matriz_Items[10, 14] = "assets\\minecraft\\textures\\item\\_item_10_14.png"; // Empty?
                                                    Matriz_Items[11, 14] = "assets\\minecraft\\textures\\item\\_item_11_14.png"; // Empty?
                                                    Matriz_Items[12, 14] = "assets\\minecraft\\textures\\item\\_item_12_14.png"; // Empty?
                                                    Matriz_Items[13, 14] = "assets\\minecraft\\textures\\item\\_item_13_14.png"; // Empty?
                                                    Matriz_Items[14, 14] = "assets\\minecraft\\textures\\item\\_item_14_14.png"; // Empty?
                                                    Matriz_Items[15, 14] = "assets\\minecraft\\textures\\entity\\enderdragon\\dragon_fireball.png";

                                                    Matriz_Items[0, 15] = "assets\\minecraft\\textures\\item\\music_disc_13.png";
                                                    Matriz_Items[1, 15] = "assets\\minecraft\\textures\\item\\music_disc_cat.png";
                                                    Matriz_Items[2, 15] = "assets\\minecraft\\textures\\item\\music_disc_blocks.png";
                                                    Matriz_Items[3, 15] = "assets\\minecraft\\textures\\item\\music_disc_chirp.png";
                                                    Matriz_Items[4, 15] = "assets\\minecraft\\textures\\item\\music_disc_far.png";
                                                    Matriz_Items[5, 15] = "assets\\minecraft\\textures\\item\\music_disc_mall.png";
                                                    Matriz_Items[6, 15] = "assets\\minecraft\\textures\\item\\music_disc_mellohi.png";
                                                    Matriz_Items[7, 15] = "assets\\minecraft\\textures\\item\\music_disc_stal.png";
                                                    Matriz_Items[8, 15] = "assets\\minecraft\\textures\\item\\music_disc_strad.png";
                                                    Matriz_Items[9, 15] = "assets\\minecraft\\textures\\item\\music_disc_ward.png";
                                                    Matriz_Items[10, 15] = "assets\\minecraft\\textures\\item\\music_disc_11.png";
                                                    Matriz_Items[11, 15] = "assets\\minecraft\\textures\\item\\music_disc_wait.png";
                                                    Matriz_Items[12, 15] = "assets\\minecraft\\textures\\item\\cod_bucket.png";
                                                    Matriz_Items[13, 15] = "assets\\minecraft\\textures\\item\\salmon_bucket.png";
                                                    Matriz_Items[14, 15] = "assets\\minecraft\\textures\\item\\pufferfish_bucket.png";
                                                    Matriz_Items[15, 15] = "assets\\minecraft\\textures\\item\\tropical_fish_bucket.png";

                                                    Matriz_Items[0, 16] = "assets\\minecraft\\textures\\item\\_leather_horse_armor_.png"; // Unknown.
                                                    Matriz_Items[1, 16] = "assets\\minecraft\\textures\\item\\_item_1_16.png"; // Empty?
                                                    Matriz_Items[2, 16] = "assets\\minecraft\\textures\\item\\_item_2_16.png"; // Empty?
                                                    Matriz_Items[3, 16] = "assets\\minecraft\\textures\\item\\_item_3_16.png"; // Empty?
                                                    Matriz_Items[4, 16] = "assets\\minecraft\\textures\\item\\_item_4_16.png"; // Empty?
                                                    Matriz_Items[5, 16] = "assets\\minecraft\\textures\\item\\_item_5_16.png"; // Empty?
                                                    Matriz_Items[6, 16] = "assets\\minecraft\\textures\\item\\_item_6_16.png"; // Empty?
                                                    Matriz_Items[7, 16] = "assets\\minecraft\\textures\\item\\kelp.png";
                                                    Matriz_Items[8, 16] = "assets\\minecraft\\textures\\item\\dried_kelp.png";
                                                    Matriz_Items[9, 16] = "assets\\minecraft\\textures\\item\\sea_pickle.png";
                                                    Matriz_Items[10, 16] = "assets\\minecraft\\textures\\item\\nautilus_shell.png";
                                                    Matriz_Items[11, 16] = "assets\\minecraft\\textures\\item\\heart_of_the_sea.png";
                                                    Matriz_Items[12, 16] = "assets\\minecraft\\textures\\item\\turtle_helmet.png";
                                                    Matriz_Items[13, 16] = "assets\\minecraft\\textures\\item\\scute.png";
                                                    Matriz_Items[14, 16] = "assets\\minecraft\\textures\\item\\trident.png";
                                                    Matriz_Items[15, 16] = "assets\\minecraft\\textures\\item\\phantom_membrane.png";

                                                    // First copy the full image bytes to include it in the resource packs.
                                                    byte[] Matriz_Bytes = Program.Obtener_Matriz_Bytes_Archivo(Ruta_Recurso_Entrada);
                                                    if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                                                    {
                                                        Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\assets\\minecraft\\textures\\item\\_items.png";
                                                        Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                        FileStream Lector = new FileStream(Ruta_Recurso_Salida, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                                        Lector.SetLength(0L);
                                                        Lector.Seek(0L, SeekOrigin.Begin);
                                                        Lector.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                                        Lector.Close();
                                                        Lector.Dispose();
                                                        Lector = null;
                                                        Matriz_Bytes = null;
                                                    }

                                                    // Now load the file as an image and split it into multiple images.
                                                    Bitmap Imagen_Original = Program.Cargar_Imagen_Ruta(Ruta_Recurso_Entrada, CheckState.Indeterminate);
                                                    if (Imagen_Original != null)
                                                    {
                                                        int Ancho = Imagen_Original.Width;
                                                        int Alto = Imagen_Original.Height;
                                                        int Ancho_Alto = Imagen_Original.Width / 16; // Texture dimensions.
                                                        for (int Índice_Y = 0; Índice_Y < Alto / Ancho_Alto; Índice_Y++)
                                                        {
                                                            for (int Índice_X = 0; Índice_X < Ancho / Ancho_Alto; Índice_X++)
                                                            {
                                                                Bitmap Imagen = Imagen_Original.Clone(new Rectangle(Índice_X * Ancho_Alto, Índice_Y * Ancho_Alto, Ancho_Alto, Ancho_Alto), Imagen_Original.PixelFormat) as Bitmap;
                                                                if (Imagen != null)
                                                                {
                                                                    Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\" + (!string.IsNullOrEmpty(Matriz_Items[Índice_X, Índice_Y]) ? Matriz_Items[Índice_X, Índice_Y] : "assets\\minecraft\\textures\\item\\_item_" + Índice_X.ToString() + "_" + Índice_Y.ToString() + ".png");
                                                                    Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                                    Imagen.Save(Ruta_Recurso_Salida, ImageFormat.Png);
                                                                    Imagen.Dispose();
                                                                    Imagen = null;
                                                                }
                                                            }
                                                        }
                                                        Imagen_Original.Dispose();
                                                        Imagen_Original = null;
                                                    }
                                                }
                                                else if (string.Compare(Nombre_Recurso, "kz", true) == 0)
                                                {
                                                    // Note: it seeems that the Xbox 360 has up to 5 extra paintings.
                                                    Bitmap Imagen_Original = Program.Cargar_Imagen_Ruta(Ruta_Recurso_Entrada, CheckState.Indeterminate);
                                                    if (Imagen_Original != null)
                                                    {
                                                        int Ancho = Imagen_Original.Width;
                                                        int Alto = Imagen_Original.Height;
                                                        int Ancho_Cuadro = Ancho / 16;
                                                        int Alto_Cuadro = Alto / 16;
                                                        foreach (KeyValuePair<string, Rectangle> Entrada_Cuadro in Diccionario_Cuadros_Rectángulos)
                                                        {
                                                            try
                                                            {
                                                                Bitmap Imagen = Imagen_Original.Clone(new Rectangle(Entrada_Cuadro.Value.X * Ancho_Cuadro, Entrada_Cuadro.Value.Y * Alto_Cuadro, Entrada_Cuadro.Value.Width * Ancho_Cuadro, Entrada_Cuadro.Value.Height * Alto_Cuadro), Imagen_Original.PixelFormat);
                                                                Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\assets\\minecraft\\textures\\painting\\" + Entrada_Cuadro.Key + ".png";
                                                                Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                                Imagen.Save(Ruta_Recurso_Salida, ImageFormat.Png);
                                                                Imagen.Dispose();
                                                                Imagen = null;
                                                            }
                                                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                        }
                                                        Imagen_Original.Dispose();
                                                        Imagen_Original = null;
                                                    }
                                                }
                                                else if (string.Compare(Nombre_Recurso, "particles", true) == 0)
                                                {
                                                    Bitmap Imagen_Original = Program.Cargar_Imagen_Ruta(Ruta_Recurso_Entrada, CheckState.Indeterminate);
                                                    if (Imagen_Original != null)
                                                    {
                                                        // Start a dictionary with the rectangles of the textures and it's names.
                                                        Dictionary<Rectangle, string> Diccionario_Rectángulos_Texturas = new Dictionary<Rectangle, string>();
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(0, 0, 8, 8), "assets\\minecraft\\textures\\particle\\generic_0.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(8, 0, 8, 8), "assets\\minecraft\\textures\\particle\\generic_1.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(16, 0, 8, 8), "assets\\minecraft\\textures\\particle\\generic_2.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(24, 0, 8, 8), "assets\\minecraft\\textures\\particle\\generic_3.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(32, 0, 8, 8), "assets\\minecraft\\textures\\particle\\generic_4.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(40, 0, 8, 8), "assets\\minecraft\\textures\\particle\\generic_5.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(48, 0, 8, 8), "assets\\minecraft\\textures\\particle\\generic_6.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(56, 0, 8, 8), "assets\\minecraft\\textures\\particle\\generic_7.png");

                                                        //Diccionario_Rectángulos_Texturas.Add(new Rectangle(0, 8, 8, 8), "assets\\minecraft\\textures\\particle\\00000000.png");
                                                        //Diccionario_Rectángulos_Texturas.Add(new Rectangle(8, 8, 8, 8), "assets\\minecraft\\textures\\particle\\00000000.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(24, 8, 8, 8), "assets\\minecraft\\textures\\particle\\splash_0.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(32, 8, 8, 8), "assets\\minecraft\\textures\\particle\\splash_1.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(40, 8, 8, 8), "assets\\minecraft\\textures\\particle\\splash_2.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(48, 8, 8, 8), "assets\\minecraft\\textures\\particle\\splash_3.png");

                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(0, 16, 8, 8), "assets\\minecraft\\textures\\particle\\bubble.png");
                                                        //Diccionario_Rectángulos_Texturas.Add(new Rectangle(8, 16, 8, 8), "assets\\minecraft\\textures\\particle\\00000000.png");
                                                        //Diccionario_Rectángulos_Texturas.Add(new Rectangle(16, 16, 8, 8), "assets\\minecraft\\textures\\particle\\00000000.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(32, 16, 32, 32), "assets\\minecraft\\textures\\particle\\flash.png");

                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(0, 24, 8, 8), "assets\\minecraft\\textures\\particle\\flame.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(8, 24, 8, 8), "assets\\minecraft\\textures\\particle\\lava.png");

                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(0, 32, 8, 8), "assets\\minecraft\\textures\\particle\\note.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(8, 32, 8, 8), "assets\\minecraft\\textures\\particle\\critical_hit.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(16, 32, 8, 8), "assets\\minecraft\\textures\\particle\\enchanted_hit.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(24, 32, 8, 8), "assets\\minecraft\\textures\\particle\\damage.png");

                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(0, 40, 8, 8), "assets\\minecraft\\textures\\particle\\heart.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(8, 40, 8, 8), "assets\\minecraft\\textures\\particle\\angry.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(16, 40, 8, 8), "assets\\minecraft\\textures\\particle\\glint.png");
                                                        //Diccionario_Rectángulos_Texturas.Add(new Rectangle(24, 40, 8, 8), "assets\\minecraft\\textures\\particle\\00000000.png");

                                                        //Diccionario_Rectángulos_Texturas.Add(new Rectangle(0, 48, 8, 8), "assets\\minecraft\\textures\\particle\\00000000.png");
                                                        //Diccionario_Rectángulos_Texturas.Add(new Rectangle(8, 48, 8, 8), "assets\\minecraft\\textures\\particle\\00000000.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(48, 48, 16, 16), "assets\\minecraft\\textures\\particle\\bubble_pop_0.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(64, 48, 16, 16), "assets\\minecraft\\textures\\particle\\bubble_pop_1.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(80, 48, 16, 16), "assets\\minecraft\\textures\\particle\\bubble_pop_2.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(96, 48, 16, 16), "assets\\minecraft\\textures\\particle\\bubble_pop_3.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(112, 48, 16, 16), "assets\\minecraft\\textures\\particle\\bubble_pop_4.png");

                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(0, 56, 8, 8), "assets\\minecraft\\textures\\particle\\drip_hang.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(8, 56, 8, 8), "assets\\minecraft\\textures\\particle\\drip_fall.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(16, 56, 8, 8), "assets\\minecraft\\textures\\particle\\drip_land.png");

                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(0, 64, 8, 8), "assets\\minecraft\\textures\\particle\\effect_0.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(8, 64, 8, 8), "assets\\minecraft\\textures\\particle\\effect_1.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(16, 64, 8, 8), "assets\\minecraft\\textures\\particle\\effect_2.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(24, 64, 8, 8), "assets\\minecraft\\textures\\particle\\effect_3.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(32, 64, 8, 8), "assets\\minecraft\\textures\\particle\\effect_4.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(40, 64, 8, 8), "assets\\minecraft\\textures\\particle\\effect_5.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(48, 64, 8, 8), "assets\\minecraft\\textures\\particle\\effect_6.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(56, 64, 8, 8), "assets\\minecraft\\textures\\particle\\effect_7.png");

                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(0, 72, 8, 8), "assets\\minecraft\\textures\\particle\\spell_0.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(8, 72, 8, 8), "assets\\minecraft\\textures\\particle\\spell_1.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(16, 72, 8, 8), "assets\\minecraft\\textures\\particle\\spell_2.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(24, 72, 8, 8), "assets\\minecraft\\textures\\particle\\spell_3.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(32, 72, 8, 8), "assets\\minecraft\\textures\\particle\\spell_4.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(40, 72, 8, 8), "assets\\minecraft\\textures\\particle\\spell_5.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(48, 72, 8, 8), "assets\\minecraft\\textures\\particle\\spell_6.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(56, 72, 8, 8), "assets\\minecraft\\textures\\particle\\spell_7.png");

                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(0, 80, 8, 8), "assets\\minecraft\\textures\\particle\\spark_0.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(8, 80, 8, 8), "assets\\minecraft\\textures\\particle\\spark_1.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(16, 80, 8, 8), "assets\\minecraft\\textures\\particle\\spark_2.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(24, 80, 8, 8), "assets\\minecraft\\textures\\particle\\spark_3.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(32, 80, 8, 8), "assets\\minecraft\\textures\\particle\\spark_4.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(40, 80, 8, 8), "assets\\minecraft\\textures\\particle\\spark_5.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(48, 80, 8, 8), "assets\\minecraft\\textures\\particle\\spark_6.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(56, 80, 8, 8), "assets\\minecraft\\textures\\particle\\spark_7.png");

                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(0, 88, 8, 8), "assets\\minecraft\\textures\\particle\\glitter_0.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(8, 88, 8, 8), "assets\\minecraft\\textures\\particle\\glitter_1.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(16, 88, 8, 8), "assets\\minecraft\\textures\\particle\\glitter_2.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(24, 88, 8, 8), "assets\\minecraft\\textures\\particle\\glitter_3.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(32, 88, 8, 8), "assets\\minecraft\\textures\\particle\\glitter_4.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(40, 88, 8, 8), "assets\\minecraft\\textures\\particle\\glitter_5.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(48, 88, 8, 8), "assets\\minecraft\\textures\\particle\\glitter_6.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(56, 88, 8, 8), "assets\\minecraft\\textures\\particle\\glitter_7.png");

                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(0, 96, 8, 8), "assets\\minecraft\\textures\\particle\\explosion_0.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(8, 96, 8, 8), "assets\\minecraft\\textures\\particle\\explosion_1.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(16, 96, 8, 8), "assets\\minecraft\\textures\\particle\\explosion_2.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(24, 96, 8, 8), "assets\\minecraft\\textures\\particle\\explosion_3.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(32, 96, 8, 8), "assets\\minecraft\\textures\\particle\\explosion_4.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(40, 96, 8, 8), "assets\\minecraft\\textures\\particle\\explosion_5.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(48, 96, 8, 8), "assets\\minecraft\\textures\\particle\\explosion_6.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(56, 96, 8, 8), "assets\\minecraft\\textures\\particle\\explosion_7.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(64, 96, 8, 8), "assets\\minecraft\\textures\\particle\\explosion_8.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(72, 96, 8, 8), "assets\\minecraft\\textures\\particle\\explosion_9.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(80, 96, 8, 8), "assets\\minecraft\\textures\\particle\\explosion_10.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(88, 96, 8, 8), "assets\\minecraft\\textures\\particle\\explosion_11.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(96, 96, 8, 8), "assets\\minecraft\\textures\\particle\\explosion_12.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(104, 96, 8, 8), "assets\\minecraft\\textures\\particle\\explosion_13.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(112, 96, 8, 8), "assets\\minecraft\\textures\\particle\\explosion_14.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(120, 96, 8, 8), "assets\\minecraft\\textures\\particle\\explosion_15.png");

                                                        //Diccionario_Rectángulos_Texturas.Add(new Rectangle(0, 104, 8, 8), "assets\\minecraft\\textures\\particle\\00000000.png");
                                                        //Diccionario_Rectángulos_Texturas.Add(new Rectangle(8, 104, 8, 8), "assets\\minecraft\\textures\\particle\\00000000.png");
                                                        //Diccionario_Rectángulos_Texturas.Add(new Rectangle(16, 104, 8, 8), "assets\\minecraft\\textures\\particle\\00000000.png");
                                                        //Diccionario_Rectángulos_Texturas.Add(new Rectangle(24, 104, 8, 8), "assets\\minecraft\\textures\\particle\\00000000.png");
                                                        //Diccionario_Rectángulos_Texturas.Add(new Rectangle(32, 104, 8, 8), "assets\\minecraft\\textures\\particle\\00000000.png");
                                                        //Diccionario_Rectángulos_Texturas.Add(new Rectangle(40, 104, 8, 8), "assets\\minecraft\\textures\\particle\\00000000.png");
                                                        //Diccionario_Rectángulos_Texturas.Add(new Rectangle(48, 104, 8, 8), "assets\\minecraft\\textures\\particle\\00000000.png");
                                                        //Diccionario_Rectángulos_Texturas.Add(new Rectangle(56, 104, 8, 8), "assets\\minecraft\\textures\\particle\\00000000.png");
                                                        //Diccionario_Rectángulos_Texturas.Add(new Rectangle(64, 104, 8, 8), "assets\\minecraft\\textures\\particle\\00000000.png");
                                                        //Diccionario_Rectángulos_Texturas.Add(new Rectangle(72, 104, 8, 8), "assets\\minecraft\\textures\\particle\\00000000.png");
                                                        //Diccionario_Rectángulos_Texturas.Add(new Rectangle(80, 104, 8, 8), "assets\\minecraft\\textures\\particle\\00000000.png");
                                                        //Diccionario_Rectángulos_Texturas.Add(new Rectangle(88, 104, 8, 8), "assets\\minecraft\\textures\\particle\\00000000.png");

                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(8, 112, 8, 8), "assets\\minecraft\\textures\\particle\\sga_a.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(16, 112, 8, 8), "assets\\minecraft\\textures\\particle\\sga_b.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(24, 112, 8, 8), "assets\\minecraft\\textures\\particle\\sga_c.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(32, 112, 8, 8), "assets\\minecraft\\textures\\particle\\sga_d.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(40, 112, 8, 8), "assets\\minecraft\\textures\\particle\\sga_e.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(48, 112, 8, 8), "assets\\minecraft\\textures\\particle\\sga_f.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(56, 112, 8, 8), "assets\\minecraft\\textures\\particle\\sga_g.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(64, 112, 8, 8), "assets\\minecraft\\textures\\particle\\sga_h.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(72, 112, 8, 8), "assets\\minecraft\\textures\\particle\\sga_i.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(80, 112, 8, 8), "assets\\minecraft\\textures\\particle\\sga_j.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(88, 112, 8, 8), "assets\\minecraft\\textures\\particle\\sga_k.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(96, 112, 8, 8), "assets\\minecraft\\textures\\particle\\sga_l.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(104, 112, 8, 8), "assets\\minecraft\\textures\\particle\\sga_m.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(112, 112, 8, 8), "assets\\minecraft\\textures\\particle\\sga_n.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(120, 112, 8, 8), "assets\\minecraft\\textures\\particle\\sga_o.png");

                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(0, 120, 8, 8), "assets\\minecraft\\textures\\particle\\sga_p.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(8, 120, 8, 8), "assets\\minecraft\\textures\\particle\\sga_q.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(16, 120, 8, 8), "assets\\minecraft\\textures\\particle\\sga_r.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(24, 120, 8, 8), "assets\\minecraft\\textures\\particle\\sga_s.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(32, 120, 8, 8), "assets\\minecraft\\textures\\particle\\sga_t.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(40, 120, 8, 8), "assets\\minecraft\\textures\\particle\\sga_u.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(48, 120, 8, 8), "assets\\minecraft\\textures\\particle\\sga_v.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(56, 120, 8, 8), "assets\\minecraft\\textures\\particle\\sga_w.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(64, 120, 8, 8), "assets\\minecraft\\textures\\particle\\sga_x.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(72, 120, 8, 8), "assets\\minecraft\\textures\\particle\\sga_y.png");
                                                        Diccionario_Rectángulos_Texturas.Add(new Rectangle(80, 120, 8, 8), "assets\\minecraft\\textures\\particle\\sga_z.png");

                                                        // Now split the original image in to multiple textures.
                                                        int Ancho = Imagen_Original.Width;
                                                        int Alto = Imagen_Original.Height;
                                                        int Resolución = Ancho / 128;
                                                        if (Diccionario_Rectángulos_Texturas.Count > 0)
                                                        {
                                                            foreach (KeyValuePair<Rectangle, string> Entrada_Textura in Diccionario_Rectángulos_Texturas)
                                                            {
                                                                Bitmap Imagen = Imagen_Original.Clone(new Rectangle(Entrada_Textura.Key.X * Resolución, Entrada_Textura.Key.Y * Resolución, Entrada_Textura.Key.Width * Resolución, Entrada_Textura.Key.Height * Resolución), Imagen_Original.PixelFormat) as Bitmap;
                                                                if (Imagen != null)
                                                                {
                                                                    Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\" + Entrada_Textura.Value;
                                                                    Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                                    Imagen.Save(Ruta_Recurso_Salida, ImageFormat.Png);
                                                                    Imagen.Dispose();
                                                                    Imagen = null;
                                                                }
                                                            }
                                                        }
                                                        Diccionario_Rectángulos_Texturas = null; // TODO: don't reset each time.
                                                        Imagen_Original.Dispose();
                                                        Imagen_Original = null;
                                                    }
                                                }
                                                else if (string.Compare(Nombre_Recurso, "terrain", true) == 0)
                                                {
                                                    // Making those arrays by hand took a full day of work... but it was worth it at the end.
                                                    string[,] Matriz_Terrain = new string[16, 34];

                                                    Matriz_Terrain[0, 0] = "assets\\minecraft\\textures\\block\\grass_block_top.png";
                                                    Matriz_Terrain[1, 0] = "assets\\minecraft\\textures\\block\\stone.png";
                                                    Matriz_Terrain[2, 0] = "assets\\minecraft\\textures\\block\\dirt.png";
                                                    Matriz_Terrain[3, 0] = "assets\\minecraft\\textures\\block\\grass_block_side.png";
                                                    Matriz_Terrain[4, 0] = "assets\\minecraft\\textures\\block\\oak_planks.png";
                                                    Matriz_Terrain[5, 0] = "assets\\minecraft\\textures\\block\\smooth_stone_slab_side.png";
                                                    Matriz_Terrain[6, 0] = "assets\\minecraft\\textures\\block\\smooth_stone.png";
                                                    Matriz_Terrain[7, 0] = "assets\\minecraft\\textures\\block\\bricks.png";
                                                    Matriz_Terrain[8, 0] = "assets\\minecraft\\textures\\block\\tnt_side.png";
                                                    Matriz_Terrain[9, 0] = "assets\\minecraft\\textures\\block\\tnt_top.png";
                                                    Matriz_Terrain[10, 0] = "assets\\minecraft\\textures\\block\\tnt_bottom.png";
                                                    Matriz_Terrain[11, 0] = "assets\\minecraft\\textures\\block\\cobweb.png";
                                                    Matriz_Terrain[12, 0] = "assets\\minecraft\\textures\\block\\poppy.png";
                                                    Matriz_Terrain[13, 0] = "assets\\minecraft\\textures\\block\\dandelion.png";
                                                    Matriz_Terrain[14, 0] = "assets\\minecraft\\textures\\block\\_terrain_14_0.png"; // Unknown.
                                                    Matriz_Terrain[15, 0] = "assets\\minecraft\\textures\\block\\oak_sapling.png";

                                                    Matriz_Terrain[0, 1] = "assets\\minecraft\\textures\\block\\cobblestone.png";
                                                    Matriz_Terrain[1, 1] = "assets\\minecraft\\textures\\block\\bedrock.png";
                                                    Matriz_Terrain[2, 1] = "assets\\minecraft\\textures\\block\\sand.png";
                                                    Matriz_Terrain[3, 1] = "assets\\minecraft\\textures\\block\\gravel.png";
                                                    Matriz_Terrain[4, 1] = "assets\\minecraft\\textures\\block\\oak_log.png";
                                                    Matriz_Terrain[5, 1] = "assets\\minecraft\\textures\\block\\oak_log_top.png";
                                                    Matriz_Terrain[6, 1] = "assets\\minecraft\\textures\\block\\iron_block.png";
                                                    Matriz_Terrain[7, 1] = "assets\\minecraft\\textures\\block\\gold_block.png";
                                                    Matriz_Terrain[8, 1] = "assets\\minecraft\\textures\\block\\diamond_block.png";
                                                    Matriz_Terrain[9, 1] = "assets\\minecraft\\textures\\block\\emerald_block.png";
                                                    Matriz_Terrain[10, 1] = "assets\\minecraft\\textures\\block\\redstone_block.png";
                                                    Matriz_Terrain[11, 1] = "assets\\minecraft\\textures\\block\\dropper_front.png";
                                                    Matriz_Terrain[12, 1] = "assets\\minecraft\\textures\\block\\red_mushroom.png";
                                                    Matriz_Terrain[13, 1] = "assets\\minecraft\\textures\\block\\brown_mushroom.png";
                                                    Matriz_Terrain[14, 1] = "assets\\minecraft\\textures\\block\\jungle_sapling.png";
                                                    Matriz_Terrain[15, 1] = "assets\\minecraft\\textures\\block\\_terrain_15_1.png"; // Unknown.

                                                    Matriz_Terrain[0, 2] = "assets\\minecraft\\textures\\block\\gold_ore.png";
                                                    Matriz_Terrain[1, 2] = "assets\\minecraft\\textures\\block\\iron_ore.png";
                                                    Matriz_Terrain[2, 2] = "assets\\minecraft\\textures\\block\\coal_ore.png";
                                                    Matriz_Terrain[3, 2] = "assets\\minecraft\\textures\\block\\bookshelf.png";
                                                    Matriz_Terrain[4, 2] = "assets\\minecraft\\textures\\block\\mossy_cobblestone.png";
                                                    Matriz_Terrain[5, 2] = "assets\\minecraft\\textures\\block\\obsidian.png";
                                                    Matriz_Terrain[6, 2] = "assets\\minecraft\\textures\\block\\grass_block_side_overlay.png";
                                                    Matriz_Terrain[7, 2] = "assets\\minecraft\\textures\\block\\grass.png";
                                                    Matriz_Terrain[8, 2] = "assets\\minecraft\\textures\\block\\dispenser_front_vertical.png";
                                                    Matriz_Terrain[9, 2] = "assets\\minecraft\\textures\\block\\beacon.png";
                                                    Matriz_Terrain[10, 2] = "assets\\minecraft\\textures\\block\\dropper_front_vertical.png";
                                                    Matriz_Terrain[11, 2] = "assets\\minecraft\\textures\\block\\crafting_table_top.png";
                                                    Matriz_Terrain[12, 2] = "assets\\minecraft\\textures\\block\\furnace_front.png";
                                                    Matriz_Terrain[13, 2] = "assets\\minecraft\\textures\\block\\furnace_side.png";
                                                    Matriz_Terrain[14, 2] = "assets\\minecraft\\textures\\block\\dispenser_front.png";
                                                    Matriz_Terrain[15, 2] = "assets\\minecraft\\textures\\block\\_terrain_15_2.png"; // Unknown.

                                                    Matriz_Terrain[0, 3] = "assets\\minecraft\\textures\\block\\sponge.png";
                                                    Matriz_Terrain[1, 3] = "assets\\minecraft\\textures\\block\\glass.png";
                                                    Matriz_Terrain[2, 3] = "assets\\minecraft\\textures\\block\\diamond_ore.png";
                                                    Matriz_Terrain[3, 3] = "assets\\minecraft\\textures\\block\\redstone_ore.png";
                                                    Matriz_Terrain[4, 3] = "assets\\minecraft\\textures\\block\\oak_leaves.png";
                                                    Matriz_Terrain[5, 3] = "assets\\minecraft\\textures\\block\\_oak_leaves_.png"; // Unknown.
                                                    Matriz_Terrain[6, 3] = "assets\\minecraft\\textures\\block\\stone_bricks.png";
                                                    Matriz_Terrain[7, 3] = "assets\\minecraft\\textures\\block\\dead_bush.png";
                                                    Matriz_Terrain[8, 3] = "assets\\minecraft\\textures\\block\\fern.png";
                                                    Matriz_Terrain[9, 3] = "assets\\minecraft\\textures\\block\\daylight_detector_top.png";
                                                    Matriz_Terrain[10, 3] = "assets\\minecraft\\textures\\block\\daylight_detector_side.png";
                                                    Matriz_Terrain[11, 3] = "assets\\minecraft\\textures\\block\\crafting_table_side.png";
                                                    Matriz_Terrain[12, 3] = "assets\\minecraft\\textures\\block\\crafting_table_front.png";
                                                    Matriz_Terrain[13, 3] = "assets\\minecraft\\textures\\block\\furnace_front_on.png";
                                                    Matriz_Terrain[14, 3] = "assets\\minecraft\\textures\\block\\furnace_top.png";
                                                    Matriz_Terrain[15, 3] = "assets\\minecraft\\textures\\block\\spruce_sapling.png";

                                                    Matriz_Terrain[0, 4] = "assets\\minecraft\\textures\\block\\white_wool.png";
                                                    Matriz_Terrain[1, 4] = "assets\\minecraft\\textures\\block\\spawner.png";
                                                    Matriz_Terrain[2, 4] = "assets\\minecraft\\textures\\block\\snow.png";
                                                    Matriz_Terrain[3, 4] = "assets\\minecraft\\textures\\block\\ice.png";
                                                    Matriz_Terrain[4, 4] = "assets\\minecraft\\textures\\block\\grass_block_snow.png";
                                                    Matriz_Terrain[5, 4] = "assets\\minecraft\\textures\\block\\cactus_top.png";
                                                    Matriz_Terrain[6, 4] = "assets\\minecraft\\textures\\block\\cactus_side.png";
                                                    Matriz_Terrain[7, 4] = "assets\\minecraft\\textures\\block\\cactus_bottom.png";
                                                    Matriz_Terrain[8, 4] = "assets\\minecraft\\textures\\block\\clay.png";
                                                    Matriz_Terrain[9, 4] = "assets\\minecraft\\textures\\block\\sugar_cane.png";
                                                    Matriz_Terrain[10, 4] = "assets\\minecraft\\textures\\block\\jukebox_side.png";
                                                    Matriz_Terrain[11, 4] = "assets\\minecraft\\textures\\block\\jukebox_top.png";
                                                    Matriz_Terrain[12, 4] = "assets\\minecraft\\textures\\block\\lily_pad.png";
                                                    Matriz_Terrain[13, 4] = "assets\\minecraft\\textures\\block\\mycelium_side.png";
                                                    Matriz_Terrain[14, 4] = "assets\\minecraft\\textures\\block\\mycelium_top.png";
                                                    Matriz_Terrain[15, 4] = "assets\\minecraft\\textures\\block\\birch_sapling.png";

                                                    Matriz_Terrain[0, 5] = "assets\\minecraft\\textures\\block\\torch.png";
                                                    Matriz_Terrain[1, 5] = "assets\\minecraft\\textures\\block\\oak_door_top.png";
                                                    Matriz_Terrain[2, 5] = "assets\\minecraft\\textures\\block\\iron_door_top.png";
                                                    Matriz_Terrain[3, 5] = "assets\\minecraft\\textures\\block\\ladder.png";
                                                    Matriz_Terrain[4, 5] = "assets\\minecraft\\textures\\block\\oak_trapdoor.png";
                                                    Matriz_Terrain[5, 5] = "assets\\minecraft\\textures\\block\\iron_bars.png";
                                                    Matriz_Terrain[6, 5] = "assets\\minecraft\\textures\\block\\farmland_moist.png";
                                                    Matriz_Terrain[7, 5] = "assets\\minecraft\\textures\\block\\farmland.png";
                                                    Matriz_Terrain[8, 5] = "assets\\minecraft\\textures\\block\\wheat_stage0.png";
                                                    Matriz_Terrain[9, 5] = "assets\\minecraft\\textures\\block\\wheat_stage1.png";
                                                    Matriz_Terrain[10, 5] = "assets\\minecraft\\textures\\block\\wheat_stage2.png";
                                                    Matriz_Terrain[11, 5] = "assets\\minecraft\\textures\\block\\wheat_stage3.png";
                                                    Matriz_Terrain[12, 5] = "assets\\minecraft\\textures\\block\\wheat_stage4.png";
                                                    Matriz_Terrain[13, 5] = "assets\\minecraft\\textures\\block\\wheat_stage5.png";
                                                    Matriz_Terrain[14, 5] = "assets\\minecraft\\textures\\block\\wheat_stage6.png";
                                                    Matriz_Terrain[15, 5] = "assets\\minecraft\\textures\\block\\wheat_stage7.png";

                                                    Matriz_Terrain[0, 6] = "assets\\minecraft\\textures\\block\\lever.png";
                                                    Matriz_Terrain[1, 6] = "assets\\minecraft\\textures\\block\\oak_door_bottom.png";
                                                    Matriz_Terrain[2, 6] = "assets\\minecraft\\textures\\block\\iron_door_bottom.png";
                                                    Matriz_Terrain[3, 6] = "assets\\minecraft\\textures\\block\\redstone_torch.png";
                                                    Matriz_Terrain[4, 6] = "assets\\minecraft\\textures\\block\\mossy_stone_bricks.png";
                                                    Matriz_Terrain[5, 6] = "assets\\minecraft\\textures\\block\\cracked_stone_bricks.png";
                                                    Matriz_Terrain[6, 6] = "assets\\minecraft\\textures\\block\\pumpkin_top.png";
                                                    Matriz_Terrain[7, 6] = "assets\\minecraft\\textures\\block\\netherrack.png";
                                                    Matriz_Terrain[8, 6] = "assets\\minecraft\\textures\\block\\soul_sand.png";
                                                    Matriz_Terrain[9, 6] = "assets\\minecraft\\textures\\block\\glowstone.png";
                                                    Matriz_Terrain[10, 6] = "assets\\minecraft\\textures\\block\\piston_top_sticky.png";
                                                    Matriz_Terrain[11, 6] = "assets\\minecraft\\textures\\block\\piston_top.png";
                                                    Matriz_Terrain[12, 6] = "assets\\minecraft\\textures\\block\\piston_side.png";
                                                    Matriz_Terrain[13, 6] = "assets\\minecraft\\textures\\block\\piston_bottom.png";
                                                    Matriz_Terrain[14, 6] = "assets\\minecraft\\textures\\block\\piston_inner.png";
                                                    Matriz_Terrain[15, 6] = "assets\\minecraft\\textures\\block\\pumpkin_stem.png";

                                                    Matriz_Terrain[0, 7] = "assets\\minecraft\\textures\\block\\rail_corner.png";
                                                    Matriz_Terrain[1, 7] = "assets\\minecraft\\textures\\block\\black_wool.png";
                                                    Matriz_Terrain[2, 7] = "assets\\minecraft\\textures\\block\\gray_wool.png";
                                                    Matriz_Terrain[3, 7] = "assets\\minecraft\\textures\\block\\redstone_torch_off.png";
                                                    Matriz_Terrain[4, 7] = "assets\\minecraft\\textures\\block\\spruce_log.png";
                                                    Matriz_Terrain[5, 7] = "assets\\minecraft\\textures\\block\\birch_log.png";
                                                    Matriz_Terrain[6, 7] = "assets\\minecraft\\textures\\block\\pumpkin_side.png";
                                                    Matriz_Terrain[7, 7] = "assets\\minecraft\\textures\\block\\carved_pumpkin.png";
                                                    Matriz_Terrain[8, 7] = "assets\\minecraft\\textures\\block\\jack_o_lantern.png";
                                                    Matriz_Terrain[9, 7] = "assets\\minecraft\\textures\\block\\cake_top.png";
                                                    Matriz_Terrain[10, 7] = "assets\\minecraft\\textures\\block\\cake_side.png";
                                                    Matriz_Terrain[11, 7] = "assets\\minecraft\\textures\\block\\cake_inner.png";
                                                    Matriz_Terrain[12, 7] = "assets\\minecraft\\textures\\block\\cake_bottom.png";
                                                    Matriz_Terrain[13, 7] = "assets\\minecraft\\textures\\block\\red_mushroom_block.png";
                                                    Matriz_Terrain[14, 7] = "assets\\minecraft\\textures\\block\\brown_mushroom_block.png";
                                                    Matriz_Terrain[15, 7] = "assets\\minecraft\\textures\\block\\attached_pumpkin_stem.png";

                                                    Matriz_Terrain[0, 8] = "assets\\minecraft\\textures\\block\\rail.png";
                                                    Matriz_Terrain[1, 8] = "assets\\minecraft\\textures\\block\\red_wool.png";
                                                    Matriz_Terrain[2, 8] = "assets\\minecraft\\textures\\block\\pink_wool.png";
                                                    Matriz_Terrain[3, 8] = "assets\\minecraft\\textures\\block\\repeater.png";
                                                    Matriz_Terrain[4, 8] = "assets\\minecraft\\textures\\block\\spruce_leaves.png";
                                                    Matriz_Terrain[5, 8] = "assets\\minecraft\\textures\\block\\_spruce_leaves_.png"; // Unknown.
                                                    Matriz_Terrain[6, 8] = "assets\\minecraft\\textures\\block\\conduit.png";
                                                    Matriz_Terrain[7, 8] = "assets\\minecraft\\textures\\block\\turtle_egg.png";
                                                    Matriz_Terrain[8, 8] = "assets\\minecraft\\textures\\block\\melon_side.png";
                                                    Matriz_Terrain[9, 8] = "assets\\minecraft\\textures\\block\\melon_top.png";
                                                    Matriz_Terrain[10, 8] = "assets\\minecraft\\textures\\block\\cauldron_top.png";
                                                    Matriz_Terrain[11, 8] = "assets\\minecraft\\textures\\block\\cauldron_inner.png";
                                                    Matriz_Terrain[12, 8] = "assets\\minecraft\\textures\\block\\wet_sponge.png";
                                                    Matriz_Terrain[13, 8] = "assets\\minecraft\\textures\\block\\mushroom_stem.png";
                                                    Matriz_Terrain[14, 8] = "assets\\minecraft\\textures\\block\\mushroom_block_inside.png";
                                                    Matriz_Terrain[15, 8] = "assets\\minecraft\\textures\\block\\vine.png";

                                                    Matriz_Terrain[0, 9] = "assets\\minecraft\\textures\\block\\lapis_block.png";
                                                    Matriz_Terrain[1, 9] = "assets\\minecraft\\textures\\block\\green_wool.png";
                                                    Matriz_Terrain[2, 9] = "assets\\minecraft\\textures\\block\\lime_wool.png";
                                                    Matriz_Terrain[3, 9] = "assets\\minecraft\\textures\\block\\repeater_on.png";
                                                    Matriz_Terrain[4, 9] = "assets\\minecraft\\textures\\block\\glass_pane_top.png";
                                                    Matriz_Terrain[5, 9] = "assets\\minecraft\\textures\\block\\_terrain_5_9.png"; // Unknown.
                                                    Matriz_Terrain[6, 9] = "assets\\minecraft\\textures\\block\\_terrain_6_9.png"; // Unknown.
                                                    Matriz_Terrain[7, 9] = "assets\\minecraft\\textures\\block\\turtle_egg_slightly_cracked.png";
                                                    Matriz_Terrain[8, 9] = "assets\\minecraft\\textures\\block\\turtle_egg_very_cracked.png";
                                                    Matriz_Terrain[9, 9] = "assets\\minecraft\\textures\\block\\jungle_log.png";
                                                    Matriz_Terrain[10, 9] = "assets\\minecraft\\textures\\block\\cauldron_side.png";
                                                    Matriz_Terrain[11, 9] = "assets\\minecraft\\textures\\block\\cauldron_bottom.png";
                                                    Matriz_Terrain[12, 9] = "assets\\minecraft\\textures\\block\\brewing_stand_base.png";
                                                    Matriz_Terrain[13, 9] = "assets\\minecraft\\textures\\block\\brewing_stand.png";
                                                    Matriz_Terrain[14, 9] = "assets\\minecraft\\textures\\block\\end_portal_frame_top.png";
                                                    Matriz_Terrain[15, 9] = "assets\\minecraft\\textures\\block\\end_portal_frame_side.png";

                                                    Matriz_Terrain[0, 10] = "assets\\minecraft\\textures\\block\\lapis_ore.png";
                                                    Matriz_Terrain[1, 10] = "assets\\minecraft\\textures\\block\\brown_wool.png";
                                                    Matriz_Terrain[2, 10] = "assets\\minecraft\\textures\\block\\yellow_wool.png";
                                                    Matriz_Terrain[3, 10] = "assets\\minecraft\\textures\\block\\powered_rail.png";
                                                    Matriz_Terrain[4, 10] = "assets\\minecraft\\textures\\block\\redstone_dust_dot.png"; // Correct?
                                                    Matriz_Terrain[5, 10] = "assets\\minecraft\\textures\\block\\redstone_dust_line0.png"; // Correct?
                                                    Matriz_Terrain[6, 10] = "assets\\minecraft\\textures\\block\\enchanting_table_top.png";
                                                    Matriz_Terrain[7, 10] = "assets\\minecraft\\textures\\block\\dragon_egg.png";
                                                    Matriz_Terrain[8, 10] = "assets\\minecraft\\textures\\block\\cocoa_stage2.png";
                                                    Matriz_Terrain[9, 10] = "assets\\minecraft\\textures\\block\\cocoa_stage1.png";
                                                    Matriz_Terrain[10, 10] = "assets\\minecraft\\textures\\block\\cocoa_stage0.png";
                                                    Matriz_Terrain[11, 10] = "assets\\minecraft\\textures\\block\\emerald_ore.png";
                                                    Matriz_Terrain[12, 10] = "assets\\minecraft\\textures\\block\\tripwire_hook.png";
                                                    Matriz_Terrain[13, 10] = "assets\\minecraft\\textures\\block\\tripwire.png";
                                                    Matriz_Terrain[14, 10] = "assets\\minecraft\\textures\\block\\end_portal_frame_eye.png";
                                                    Matriz_Terrain[15, 10] = "assets\\minecraft\\textures\\block\\end_stone.png";

                                                    Matriz_Terrain[0, 11] = "assets\\minecraft\\textures\\block\\sandstone_top.png";
                                                    Matriz_Terrain[1, 11] = "assets\\minecraft\\textures\\block\\blue_wool.png";
                                                    Matriz_Terrain[2, 11] = "assets\\minecraft\\textures\\block\\light_blue_wool.png";
                                                    Matriz_Terrain[3, 11] = "assets\\minecraft\\textures\\block\\powered_rail_on.png";
                                                    Matriz_Terrain[4, 11] = "assets\\minecraft\\textures\\block\\_terrain_4_11.png"; // Empty?
                                                    Matriz_Terrain[5, 11] = "assets\\minecraft\\textures\\block\\_terrain_5_11.png"; // Empty?
                                                    Matriz_Terrain[6, 11] = "assets\\minecraft\\textures\\block\\enchanting_table_side.png";
                                                    Matriz_Terrain[7, 11] = "assets\\minecraft\\textures\\block\\enchanting_table_bottom.png";
                                                    Matriz_Terrain[8, 11] = "assets\\minecraft\\textures\\block\\_terrain_8_11.png"; // Unknown.
                                                    Matriz_Terrain[9, 11] = "assets\\minecraft\\textures\\block\\item_frame.png";
                                                    Matriz_Terrain[10, 11] = "assets\\minecraft\\textures\\block\\flower_pot.png";
                                                    Matriz_Terrain[11, 11] = "assets\\minecraft\\textures\\block\\comparator.png";
                                                    Matriz_Terrain[12, 11] = "assets\\minecraft\\textures\\block\\comparator_on.png";
                                                    Matriz_Terrain[13, 11] = "assets\\minecraft\\textures\\block\\activator_rail.png";
                                                    Matriz_Terrain[14, 11] = "assets\\minecraft\\textures\\block\\activator_rail_on.png";
                                                    Matriz_Terrain[15, 11] = "assets\\minecraft\\textures\\block\\nether_quartz_ore.png";

                                                    Matriz_Terrain[0, 12] = "assets\\minecraft\\textures\\block\\sandstone.png";
                                                    Matriz_Terrain[1, 12] = "assets\\minecraft\\textures\\block\\purple_wool.png";
                                                    Matriz_Terrain[2, 12] = "assets\\minecraft\\textures\\block\\magenta_wool.png";
                                                    Matriz_Terrain[3, 12] = "assets\\minecraft\\textures\\block\\detector_rail.png";
                                                    Matriz_Terrain[4, 12] = "assets\\minecraft\\textures\\block\\jungle_leaves.png";
                                                    Matriz_Terrain[5, 12] = "assets\\minecraft\\textures\\block\\_jungle_leaves_.png"; // Unknown.
                                                    Matriz_Terrain[6, 12] = "assets\\minecraft\\textures\\block\\spruce_planks.png";
                                                    Matriz_Terrain[7, 12] = "assets\\minecraft\\textures\\block\\jungle_planks.png";
                                                    Matriz_Terrain[8, 12] = "assets\\minecraft\\textures\\block\\carrots_stage0.png";
                                                    Matriz_Terrain[9, 12] = "assets\\minecraft\\textures\\block\\carrots_stage1.png";
                                                    Matriz_Terrain[10, 12] = "assets\\minecraft\\textures\\block\\carrots_stage2.png";
                                                    Matriz_Terrain[11, 12] = "assets\\minecraft\\textures\\block\\carrots_stage3.png";
                                                    Matriz_Terrain[12, 12] = "assets\\minecraft\\textures\\block\\slime_block.png";
                                                    Matriz_Terrain[13, 12] = "assets\\minecraft\\textures\\block\\_terrain_13_12.png"; // Unknown.
                                                    Matriz_Terrain[14, 12] = "assets\\minecraft\\textures\\block\\_terrain_14_12.png"; // Unknown.
                                                    Matriz_Terrain[15, 12] = "assets\\minecraft\\textures\\block\\_terrain_15_12.png"; // Unknown.

                                                    Matriz_Terrain[0, 13] = "assets\\minecraft\\textures\\block\\sandstone_bottom.png";
                                                    Matriz_Terrain[1, 13] = "assets\\minecraft\\textures\\block\\cyan_wool.png";
                                                    Matriz_Terrain[2, 13] = "assets\\minecraft\\textures\\block\\orange_wool.png";
                                                    Matriz_Terrain[3, 13] = "assets\\minecraft\\textures\\block\\redstone_lamp.png";
                                                    Matriz_Terrain[4, 13] = "assets\\minecraft\\textures\\block\\redstone_lamp_on.png";
                                                    Matriz_Terrain[5, 13] = "assets\\minecraft\\textures\\block\\chiseled_stone_bricks.png";
                                                    Matriz_Terrain[6, 13] = "assets\\minecraft\\textures\\block\\birch_planks.png";
                                                    Matriz_Terrain[7, 13] = "assets\\minecraft\\textures\\block\\anvil.png";
                                                    Matriz_Terrain[8, 13] = "assets\\minecraft\\textures\\block\\chipped_anvil_top.png";
                                                    Matriz_Terrain[9, 13] = "assets\\minecraft\\textures\\block\\chiseled_quartz_block_top.png";
                                                    Matriz_Terrain[10, 13] = "assets\\minecraft\\textures\\block\\quartz_pillar_top.png";
                                                    Matriz_Terrain[11, 13] = "assets\\minecraft\\textures\\block\\quartz_block_top.png";
                                                    Matriz_Terrain[12, 13] = "assets\\minecraft\\textures\\block\\hopper_outside.png";
                                                    Matriz_Terrain[13, 13] = "assets\\minecraft\\textures\\block\\detector_rail_on.png";
                                                    Matriz_Terrain[14, 13] = "assets\\minecraft\\textures\\block\\_terrain_14_13.png"; // Unknown.
                                                    Matriz_Terrain[15, 13] = "assets\\minecraft\\textures\\block\\_terrain_15_13.png"; // Unknown.

                                                    Matriz_Terrain[0, 14] = "assets\\minecraft\\textures\\block\\nether_bricks.png";
                                                    Matriz_Terrain[1, 14] = "assets\\minecraft\\textures\\block\\light_gray_wool.png";
                                                    Matriz_Terrain[2, 14] = "assets\\minecraft\\textures\\block\\nether_wart_stage0.png";
                                                    Matriz_Terrain[3, 14] = "assets\\minecraft\\textures\\block\\nether_wart_stage1.png";
                                                    Matriz_Terrain[4, 14] = "assets\\minecraft\\textures\\block\\nether_wart_stage2.png";
                                                    Matriz_Terrain[5, 14] = "assets\\minecraft\\textures\\block\\chiseled_sandstone.png";
                                                    Matriz_Terrain[6, 14] = "assets\\minecraft\\textures\\block\\cut_sandstone.png";
                                                    Matriz_Terrain[7, 14] = "assets\\minecraft\\textures\\block\\anvil_top.png";
                                                    Matriz_Terrain[8, 14] = "assets\\minecraft\\textures\\block\\damaged_anvil_top.png";
                                                    Matriz_Terrain[9, 14] = "assets\\minecraft\\textures\\block\\chiseled_quartz_block.png";
                                                    Matriz_Terrain[10, 14] = "assets\\minecraft\\textures\\block\\quartz_pillar.png";
                                                    Matriz_Terrain[11, 14] = "assets\\minecraft\\textures\\block\\quartz_block_side.png";
                                                    Matriz_Terrain[12, 14] = "assets\\minecraft\\textures\\block\\hopper_inside.png";
                                                    Matriz_Terrain[13, 14] = "assets\\minecraft\\textures\\block\\_terrain_13_14.png"; // Unknown.
                                                    Matriz_Terrain[14, 14] = "assets\\minecraft\\textures\\block\\_terrain_14_14.png"; // Unknown.
                                                    Matriz_Terrain[15, 14] = "assets\\minecraft\\textures\\block\\_terrain_15_14.png"; // Unknown.

                                                    Matriz_Terrain[0, 15] = "assets\\minecraft\\textures\\block\\destroy_stage_0.png";
                                                    Matriz_Terrain[1, 15] = "assets\\minecraft\\textures\\block\\destroy_stage_1.png";
                                                    Matriz_Terrain[2, 15] = "assets\\minecraft\\textures\\block\\destroy_stage_2.png";
                                                    Matriz_Terrain[3, 15] = "assets\\minecraft\\textures\\block\\destroy_stage_3.png";
                                                    Matriz_Terrain[4, 15] = "assets\\minecraft\\textures\\block\\destroy_stage_4.png";
                                                    Matriz_Terrain[5, 15] = "assets\\minecraft\\textures\\block\\destroy_stage_5.png";
                                                    Matriz_Terrain[6, 15] = "assets\\minecraft\\textures\\block\\destroy_stage_6.png";
                                                    Matriz_Terrain[7, 15] = "assets\\minecraft\\textures\\block\\destroy_stage_7.png";
                                                    Matriz_Terrain[8, 15] = "assets\\minecraft\\textures\\block\\destroy_stage_8.png";
                                                    Matriz_Terrain[9, 15] = "assets\\minecraft\\textures\\block\\destroy_stage_9.png";
                                                    Matriz_Terrain[10, 15] = "assets\\minecraft\\textures\\block\\hay_block_side.png";
                                                    Matriz_Terrain[11, 15] = "assets\\minecraft\\textures\\block\\quartz_block_bottom.png";
                                                    Matriz_Terrain[12, 15] = "assets\\minecraft\\textures\\block\\hopper_top.png";
                                                    Matriz_Terrain[13, 15] = "assets\\minecraft\\textures\\block\\hay_block_top.png";
                                                    Matriz_Terrain[14, 15] = "assets\\minecraft\\textures\\block\\_terrain_14_15.png"; // Unknown.
                                                    Matriz_Terrain[15, 15] = "assets\\minecraft\\textures\\block\\_terrain_15_15.png"; // Unknown.

                                                    Matriz_Terrain[0, 16] = "assets\\minecraft\\textures\\block\\coal_block.png";
                                                    Matriz_Terrain[1, 16] = "assets\\minecraft\\textures\\block\\terracotta.png";
                                                    Matriz_Terrain[2, 16] = "assets\\minecraft\\textures\\block\\note_block.png";
                                                    Matriz_Terrain[3, 16] = "assets\\minecraft\\textures\\block\\andesite.png";
                                                    Matriz_Terrain[4, 16] = "assets\\minecraft\\textures\\block\\polished_andesite.png";
                                                    Matriz_Terrain[5, 16] = "assets\\minecraft\\textures\\block\\diorite.png";
                                                    Matriz_Terrain[6, 16] = "assets\\minecraft\\textures\\block\\polished_diorite.png";
                                                    Matriz_Terrain[7, 16] = "assets\\minecraft\\textures\\block\\granite.png";
                                                    Matriz_Terrain[8, 16] = "assets\\minecraft\\textures\\block\\polished_granite.png";
                                                    Matriz_Terrain[9, 16] = "assets\\minecraft\\textures\\block\\potatoes_stage0.png";
                                                    Matriz_Terrain[10, 16] = "assets\\minecraft\\textures\\block\\potatoes_stage1.png";
                                                    Matriz_Terrain[11, 16] = "assets\\minecraft\\textures\\block\\potatoes_stage2.png";
                                                    Matriz_Terrain[12, 16] = "assets\\minecraft\\textures\\block\\potatoes_stage3.png";
                                                    Matriz_Terrain[13, 16] = "assets\\minecraft\\textures\\block\\spruce_log_top.png";
                                                    Matriz_Terrain[14, 16] = "assets\\minecraft\\textures\\block\\jungle_log_top.png";
                                                    Matriz_Terrain[15, 16] = "assets\\minecraft\\textures\\block\\birch_log_top.png";

                                                    Matriz_Terrain[0, 17] = "assets\\minecraft\\textures\\block\\black_terracotta.png";
                                                    Matriz_Terrain[1, 17] = "assets\\minecraft\\textures\\block\\blue_terracotta.png";
                                                    Matriz_Terrain[2, 17] = "assets\\minecraft\\textures\\block\\brown_terracotta.png";
                                                    Matriz_Terrain[3, 17] = "assets\\minecraft\\textures\\block\\cyan_terracotta.png";
                                                    Matriz_Terrain[4, 17] = "assets\\minecraft\\textures\\block\\gray_terracotta.png";
                                                    Matriz_Terrain[5, 17] = "assets\\minecraft\\textures\\block\\green_terracotta.png";
                                                    Matriz_Terrain[6, 17] = "assets\\minecraft\\textures\\block\\light_blue_terracotta.png";
                                                    Matriz_Terrain[7, 17] = "assets\\minecraft\\textures\\block\\lime_terracotta.png";
                                                    Matriz_Terrain[8, 17] = "assets\\minecraft\\textures\\block\\magenta_terracotta.png";
                                                    Matriz_Terrain[9, 17] = "assets\\minecraft\\textures\\block\\orange_terracotta.png";
                                                    Matriz_Terrain[10, 17] = "assets\\minecraft\\textures\\block\\pink_terracotta.png";
                                                    Matriz_Terrain[11, 17] = "assets\\minecraft\\textures\\block\\purple_terracotta.png";
                                                    Matriz_Terrain[12, 17] = "assets\\minecraft\\textures\\block\\red_terracotta.png";
                                                    Matriz_Terrain[13, 17] = "assets\\minecraft\\textures\\block\\light_gray_terracotta.png";
                                                    Matriz_Terrain[14, 17] = "assets\\minecraft\\textures\\block\\white_terracotta.png";
                                                    Matriz_Terrain[15, 17] = "assets\\minecraft\\textures\\block\\yellow_terracotta.png";

                                                    Matriz_Terrain[0, 18] = "assets\\minecraft\\textures\\block\\black_stained_glass.png";
                                                    Matriz_Terrain[1, 18] = "assets\\minecraft\\textures\\block\\blue_stained_glass.png";
                                                    Matriz_Terrain[2, 18] = "assets\\minecraft\\textures\\block\\brown_stained_glass.png";
                                                    Matriz_Terrain[3, 18] = "assets\\minecraft\\textures\\block\\cyan_stained_glass.png";
                                                    Matriz_Terrain[4, 18] = "assets\\minecraft\\textures\\block\\gray_stained_glass.png";
                                                    Matriz_Terrain[5, 18] = "assets\\minecraft\\textures\\block\\green_stained_glass.png";
                                                    Matriz_Terrain[6, 18] = "assets\\minecraft\\textures\\block\\light_blue_stained_glass.png";
                                                    Matriz_Terrain[7, 18] = "assets\\minecraft\\textures\\block\\lime_stained_glass.png";
                                                    Matriz_Terrain[8, 18] = "assets\\minecraft\\textures\\block\\magenta_stained_glass.png";
                                                    Matriz_Terrain[9, 18] = "assets\\minecraft\\textures\\block\\orange_stained_glass.png";
                                                    Matriz_Terrain[10, 18] = "assets\\minecraft\\textures\\block\\pink_stained_glass.png";
                                                    Matriz_Terrain[11, 18] = "assets\\minecraft\\textures\\block\\purple_stained_glass.png";
                                                    Matriz_Terrain[12, 18] = "assets\\minecraft\\textures\\block\\red_stained_glass.png";
                                                    Matriz_Terrain[13, 18] = "assets\\minecraft\\textures\\block\\light_gray_stained_glass.png";
                                                    Matriz_Terrain[14, 18] = "assets\\minecraft\\textures\\block\\white_stained_glass.png";
                                                    Matriz_Terrain[15, 18] = "assets\\minecraft\\textures\\block\\yellow_stained_glass.png";

                                                    Matriz_Terrain[0, 19] = "assets\\minecraft\\textures\\block\\black_stained_glass_pane_top.png";
                                                    Matriz_Terrain[1, 19] = "assets\\minecraft\\textures\\block\\blue_stained_glass_pane_top.png";
                                                    Matriz_Terrain[2, 19] = "assets\\minecraft\\textures\\block\\brown_stained_glass_pane_top.png";
                                                    Matriz_Terrain[3, 19] = "assets\\minecraft\\textures\\block\\cyan_stained_glass_pane_top.png";
                                                    Matriz_Terrain[4, 19] = "assets\\minecraft\\textures\\block\\gray_stained_glass_pane_top.png";
                                                    Matriz_Terrain[5, 19] = "assets\\minecraft\\textures\\block\\green_stained_glass_pane_top.png";
                                                    Matriz_Terrain[6, 19] = "assets\\minecraft\\textures\\block\\light_blue_stained_glass_pane_top.png";
                                                    Matriz_Terrain[7, 19] = "assets\\minecraft\\textures\\block\\lime_stained_glass_pane_top.png";
                                                    Matriz_Terrain[8, 19] = "assets\\minecraft\\textures\\block\\magenta_stained_glass_pane_top.png";
                                                    Matriz_Terrain[9, 19] = "assets\\minecraft\\textures\\block\\orange_stained_glass_pane_top.png";
                                                    Matriz_Terrain[10, 19] = "assets\\minecraft\\textures\\block\\pink_stained_glass_pane_top.png";
                                                    Matriz_Terrain[11, 19] = "assets\\minecraft\\textures\\block\\purple_stained_glass_pane_top.png";
                                                    Matriz_Terrain[12, 19] = "assets\\minecraft\\textures\\block\\red_stained_glass_pane_top.png";
                                                    Matriz_Terrain[13, 19] = "assets\\minecraft\\textures\\block\\light_gray_stained_glass_pane_top.png";
                                                    Matriz_Terrain[14, 19] = "assets\\minecraft\\textures\\block\\white_stained_glass_pane_top.png";
                                                    Matriz_Terrain[15, 19] = "assets\\minecraft\\textures\\block\\yellow_stained_glass_pane_top.png";

                                                    Matriz_Terrain[0, 20] = "assets\\minecraft\\textures\\block\\large_fern_top.png";
                                                    Matriz_Terrain[1, 20] = "assets\\minecraft\\textures\\block\\tall_grass_top.png";
                                                    Matriz_Terrain[2, 20] = "assets\\minecraft\\textures\\block\\peony_top.png";
                                                    Matriz_Terrain[3, 20] = "assets\\minecraft\\textures\\block\\rose_bush_top.png";
                                                    Matriz_Terrain[4, 20] = "assets\\minecraft\\textures\\block\\lilac_top.png";
                                                    Matriz_Terrain[5, 20] = "assets\\minecraft\\textures\\block\\orange_tulip.png";
                                                    Matriz_Terrain[6, 20] = "assets\\minecraft\\textures\\block\\sunflower_top.png";
                                                    Matriz_Terrain[7, 20] = "assets\\minecraft\\textures\\block\\sunflower_front.png";
                                                    Matriz_Terrain[8, 20] = "assets\\minecraft\\textures\\block\\acacia_log.png";
                                                    Matriz_Terrain[9, 20] = "assets\\minecraft\\textures\\block\\acacia_log_top.png";
                                                    Matriz_Terrain[10, 20] = "assets\\minecraft\\textures\\block\\acacia_planks.png";
                                                    Matriz_Terrain[11, 20] = "assets\\minecraft\\textures\\block\\acacia_leaves.png";
                                                    Matriz_Terrain[12, 20] = "assets\\minecraft\\textures\\block\\_acacia_leaves_.png"; // Unknown.
                                                    Matriz_Terrain[13, 20] = "assets\\minecraft\\textures\\block\\prismarine_bricks.png";
                                                    Matriz_Terrain[14, 20] = "assets\\minecraft\\textures\\block\\red_sand.png";
                                                    Matriz_Terrain[15, 20] = "assets\\minecraft\\textures\\block\\red_sandstone_top.png";

                                                    Matriz_Terrain[0, 21] = "assets\\minecraft\\textures\\block\\large_fern_bottom.png";
                                                    Matriz_Terrain[1, 21] = "assets\\minecraft\\textures\\block\\tall_grass_bottom.png";
                                                    Matriz_Terrain[2, 21] = "assets\\minecraft\\textures\\block\\peony_bottom.png";
                                                    Matriz_Terrain[3, 21] = "assets\\minecraft\\textures\\block\\rose_bush_bottom.png";
                                                    Matriz_Terrain[4, 21] = "assets\\minecraft\\textures\\block\\lilac_bottom.png";
                                                    Matriz_Terrain[5, 21] = "assets\\minecraft\\textures\\block\\pink_tulip.png";
                                                    Matriz_Terrain[6, 21] = "assets\\minecraft\\textures\\block\\sunflower_bottom.png";
                                                    Matriz_Terrain[7, 21] = "assets\\minecraft\\textures\\block\\sunflower_back.png";
                                                    Matriz_Terrain[8, 21] = "assets\\minecraft\\textures\\block\\dark_oak_log.png";
                                                    Matriz_Terrain[9, 21] = "assets\\minecraft\\textures\\block\\dark_oak_log_top.png";
                                                    Matriz_Terrain[10, 21] = "assets\\minecraft\\textures\\block\\dark_oak_planks.png";
                                                    Matriz_Terrain[11, 21] = "assets\\minecraft\\textures\\block\\dark_oak_leaves.png";
                                                    Matriz_Terrain[12, 21] = "assets\\minecraft\\textures\\block\\_dark_oak_leaves_.png"; // Unknown.
                                                    Matriz_Terrain[13, 21] = "assets\\minecraft\\textures\\block\\dark_prismarine.png";
                                                    Matriz_Terrain[14, 21] = "assets\\minecraft\\textures\\block\\red_sandstone_bottom.png";
                                                    Matriz_Terrain[15, 21] = "assets\\minecraft\\textures\\block\\red_sandstone.png";

                                                    Matriz_Terrain[0, 22] = "assets\\minecraft\\textures\\block\\allium.png";
                                                    Matriz_Terrain[1, 22] = "assets\\minecraft\\textures\\block\\blue_orchid.png";
                                                    Matriz_Terrain[2, 22] = "assets\\minecraft\\textures\\block\\azure_bluet.png";
                                                    Matriz_Terrain[3, 22] = "assets\\minecraft\\textures\\block\\oxeye_daisy.png";
                                                    Matriz_Terrain[4, 22] = "assets\\minecraft\\textures\\block\\red_tulip.png";
                                                    Matriz_Terrain[5, 22] = "assets\\minecraft\\textures\\block\\white_tulip.png";
                                                    Matriz_Terrain[6, 22] = "assets\\minecraft\\textures\\block\\acacia_sapling.png";
                                                    Matriz_Terrain[7, 22] = "assets\\minecraft\\textures\\block\\dark_oak_sapling.png";
                                                    Matriz_Terrain[8, 22] = "assets\\minecraft\\textures\\block\\coarse_dirt.png";
                                                    Matriz_Terrain[9, 22] = "assets\\minecraft\\textures\\block\\podzol_side.png";
                                                    Matriz_Terrain[10, 22] = "assets\\minecraft\\textures\\block\\podzol_top.png";
                                                    Matriz_Terrain[11, 22] = "assets\\minecraft\\textures\\block\\birch_leaves.png";
                                                    Matriz_Terrain[12, 22] = "assets\\minecraft\\textures\\block\\_birch_leaves_.png"; // Unknown.
                                                    Matriz_Terrain[13, 22] = "assets\\minecraft\\textures\\block\\prismarine.png";
                                                    Matriz_Terrain[14, 22] = "assets\\minecraft\\textures\\block\\chiseled_red_sandstone.png";
                                                    Matriz_Terrain[15, 22] = "assets\\minecraft\\textures\\block\\cut_red_sandstone.png";

                                                    Matriz_Terrain[0, 23] = "assets\\minecraft\\textures\\block\\acacia_door_top.png";
                                                    Matriz_Terrain[1, 23] = "assets\\minecraft\\textures\\block\\birch_door_top.png";
                                                    Matriz_Terrain[2, 23] = "assets\\minecraft\\textures\\block\\dark_oak_door_top.png";
                                                    Matriz_Terrain[3, 23] = "assets\\minecraft\\textures\\block\\jungle_door_top.png";
                                                    Matriz_Terrain[4, 23] = "assets\\minecraft\\textures\\block\\spruce_door_top.png";
                                                    Matriz_Terrain[5, 23] = "assets\\minecraft\\textures\\block\\chorus_flower.png";
                                                    Matriz_Terrain[6, 23] = "assets\\minecraft\\textures\\block\\chorus_flower_dead.png";
                                                    Matriz_Terrain[7, 23] = "assets\\minecraft\\textures\\block\\chorus_plant.png";
                                                    Matriz_Terrain[8, 23] = "assets\\minecraft\\textures\\block\\end_stone_bricks.png";
                                                    Matriz_Terrain[9, 23] = "assets\\minecraft\\textures\\block\\grass_path_side.png";
                                                    Matriz_Terrain[10, 23] = "assets\\minecraft\\textures\\block\\grass_path_top.png";
                                                    Matriz_Terrain[11, 23] = "assets\\minecraft\\textures\\block\\barrier.png";
                                                    Matriz_Terrain[12, 23] = "assets\\minecraft\\textures\\block\\packed_ice.png";
                                                    Matriz_Terrain[13, 23] = "assets\\minecraft\\textures\\block\\sea_lantern.png";
                                                    Matriz_Terrain[14, 23] = "assets\\minecraft\\textures\\block\\daylight_detector_inverted_top.png";
                                                    Matriz_Terrain[15, 23] = "assets\\minecraft\\textures\\block\\iron_trapdoor.png";

                                                    Matriz_Terrain[0, 24] = "assets\\minecraft\\textures\\block\\acacia_door_bottom.png";
                                                    Matriz_Terrain[1, 24] = "assets\\minecraft\\textures\\block\\birch_door_bottom.png";
                                                    Matriz_Terrain[2, 24] = "assets\\minecraft\\textures\\block\\dark_oak_door_bottom.png";
                                                    Matriz_Terrain[3, 24] = "assets\\minecraft\\textures\\block\\jungle_door_bottom.png";
                                                    Matriz_Terrain[4, 24] = "assets\\minecraft\\textures\\block\\spruce_door_bottom.png";
                                                    Matriz_Terrain[5, 24] = "assets\\minecraft\\textures\\block\\purpur_block.png";
                                                    Matriz_Terrain[6, 24] = "assets\\minecraft\\textures\\block\\purpur_pillar.png";
                                                    Matriz_Terrain[7, 24] = "assets\\minecraft\\textures\\block\\purpur_pillar_top.png";
                                                    Matriz_Terrain[8, 24] = "assets\\minecraft\\textures\\block\\end_rod.png";
                                                    Matriz_Terrain[9, 24] = "assets\\minecraft\\textures\\block\\magma.png";
                                                    Matriz_Terrain[10, 24] = "assets\\minecraft\\textures\\block\\nether_wart_block.png";
                                                    Matriz_Terrain[11, 24] = "assets\\minecraft\\textures\\block\\red_nether_bricks.png";
                                                    Matriz_Terrain[12, 24] = "assets\\minecraft\\textures\\block\\frosted_ice_0.png";
                                                    Matriz_Terrain[13, 24] = "assets\\minecraft\\textures\\block\\frosted_ice_1.png";
                                                    Matriz_Terrain[14, 24] = "assets\\minecraft\\textures\\block\\frosted_ice_2.png";
                                                    Matriz_Terrain[15, 24] = "assets\\minecraft\\textures\\block\\frosted_ice_3.png";

                                                    Matriz_Terrain[0, 25] = "assets\\minecraft\\textures\\block\\beetroots_stage0.png";
                                                    Matriz_Terrain[1, 25] = "assets\\minecraft\\textures\\block\\beetroots_stage1.png";
                                                    Matriz_Terrain[2, 25] = "assets\\minecraft\\textures\\block\\beetroots_stage2.png";
                                                    Matriz_Terrain[3, 25] = "assets\\minecraft\\textures\\block\\beetroots_stage3.png";
                                                    Matriz_Terrain[4, 25] = "assets\\minecraft\\textures\\block\\chain_command_block_back.png";
                                                    Matriz_Terrain[5, 25] = "assets\\minecraft\\textures\\block\\chain_command_block_conditional.png";
                                                    Matriz_Terrain[6, 25] = "assets\\minecraft\\textures\\block\\chain_command_block_front.png";
                                                    Matriz_Terrain[7, 25] = "assets\\minecraft\\textures\\block\\chain_command_block_side.png";
                                                    Matriz_Terrain[8, 25] = "assets\\minecraft\\textures\\block\\command_block_back.png";
                                                    Matriz_Terrain[9, 25] = "assets\\minecraft\\textures\\block\\command_block_conditional.png";
                                                    Matriz_Terrain[10, 25] = "assets\\minecraft\\textures\\block\\command_block_front.png";
                                                    Matriz_Terrain[11, 25] = "assets\\minecraft\\textures\\block\\command_block_side.png";
                                                    Matriz_Terrain[12, 25] = "assets\\minecraft\\textures\\block\\repeating_command_block_back.png";
                                                    Matriz_Terrain[13, 25] = "assets\\minecraft\\textures\\block\\repeating_command_block_conditional.png";
                                                    Matriz_Terrain[14, 25] = "assets\\minecraft\\textures\\block\\repeating_command_block_front.png";
                                                    Matriz_Terrain[15, 25] = "assets\\minecraft\\textures\\block\\repeating_command_block_side.png";

                                                    Matriz_Terrain[0, 26] = "assets\\minecraft\\textures\\block\\bone_block_side.png";
                                                    Matriz_Terrain[1, 26] = "assets\\minecraft\\textures\\block\\bone_block_top.png";
                                                    Matriz_Terrain[2, 26] = "assets\\minecraft\\textures\\block\\melon_stem.png";
                                                    Matriz_Terrain[3, 26] = "assets\\minecraft\\textures\\block\\attached_melon_stem.png";
                                                    Matriz_Terrain[4, 26] = "assets\\minecraft\\textures\\block\\observer_front.png";
                                                    Matriz_Terrain[5, 26] = "assets\\minecraft\\textures\\block\\observer_side.png";
                                                    Matriz_Terrain[6, 26] = "assets\\minecraft\\textures\\block\\observer_back.png";
                                                    Matriz_Terrain[7, 26] = "assets\\minecraft\\textures\\block\\observer_back_on.png";
                                                    Matriz_Terrain[8, 26] = "assets\\minecraft\\textures\\block\\observer_top.png";
                                                    Matriz_Terrain[9, 26] = "assets\\minecraft\\textures\\block\\_terrain_9_26.png"; // Unknown.
                                                    Matriz_Terrain[10, 26] = "assets\\minecraft\\textures\\block\\_terrain_10_26.png"; // Unknown.
                                                    Matriz_Terrain[11, 26] = "assets\\minecraft\\textures\\block\\structure_block.png";
                                                    Matriz_Terrain[12, 26] = "assets\\minecraft\\textures\\block\\structure_block_corner.png";
                                                    Matriz_Terrain[13, 26] = "assets\\minecraft\\textures\\block\\structure_block_data.png";
                                                    Matriz_Terrain[14, 26] = "assets\\minecraft\\textures\\block\\structure_block_load.png";
                                                    Matriz_Terrain[15, 26] = "assets\\minecraft\\textures\\block\\structure_block_save.png";

                                                    Matriz_Terrain[0, 27] = "assets\\minecraft\\textures\\block\\black_concrete.png";
                                                    Matriz_Terrain[1, 27] = "assets\\minecraft\\textures\\block\\blue_concrete.png";
                                                    Matriz_Terrain[2, 27] = "assets\\minecraft\\textures\\block\\brown_concrete.png";
                                                    Matriz_Terrain[3, 27] = "assets\\minecraft\\textures\\block\\cyan_concrete.png";
                                                    Matriz_Terrain[4, 27] = "assets\\minecraft\\textures\\block\\gray_concrete.png";
                                                    Matriz_Terrain[5, 27] = "assets\\minecraft\\textures\\block\\green_concrete.png";
                                                    Matriz_Terrain[6, 27] = "assets\\minecraft\\textures\\block\\light_blue_concrete.png";
                                                    Matriz_Terrain[7, 27] = "assets\\minecraft\\textures\\block\\lime_concrete.png";
                                                    Matriz_Terrain[8, 27] = "assets\\minecraft\\textures\\block\\magenta_concrete.png";
                                                    Matriz_Terrain[9, 27] = "assets\\minecraft\\textures\\block\\orange_concrete.png";
                                                    Matriz_Terrain[10, 27] = "assets\\minecraft\\textures\\block\\pink_concrete.png";
                                                    Matriz_Terrain[11, 27] = "assets\\minecraft\\textures\\block\\purple_concrete.png";
                                                    Matriz_Terrain[12, 27] = "assets\\minecraft\\textures\\block\\red_concrete.png";
                                                    Matriz_Terrain[13, 27] = "assets\\minecraft\\textures\\block\\light_gray_concrete.png";
                                                    Matriz_Terrain[14, 27] = "assets\\minecraft\\textures\\block\\white_concrete.png";
                                                    Matriz_Terrain[15, 27] = "assets\\minecraft\\textures\\block\\yellow_concrete.png";

                                                    Matriz_Terrain[0, 28] = "assets\\minecraft\\textures\\block\\black_concrete_powder.png";
                                                    Matriz_Terrain[1, 28] = "assets\\minecraft\\textures\\block\\blue_concrete_powder.png";
                                                    Matriz_Terrain[2, 28] = "assets\\minecraft\\textures\\block\\brown_concrete_powder.png";
                                                    Matriz_Terrain[3, 28] = "assets\\minecraft\\textures\\block\\cyan_concrete_powder.png";
                                                    Matriz_Terrain[4, 28] = "assets\\minecraft\\textures\\block\\gray_concrete_powder.png";
                                                    Matriz_Terrain[5, 28] = "assets\\minecraft\\textures\\block\\green_concrete_powder.png";
                                                    Matriz_Terrain[6, 28] = "assets\\minecraft\\textures\\block\\light_blue_concrete_powder.png";
                                                    Matriz_Terrain[7, 28] = "assets\\minecraft\\textures\\block\\lime_concrete_powder.png";
                                                    Matriz_Terrain[8, 28] = "assets\\minecraft\\textures\\block\\magenta_concrete_powder.png";
                                                    Matriz_Terrain[9, 28] = "assets\\minecraft\\textures\\block\\orange_concrete_powder.png";
                                                    Matriz_Terrain[10, 28] = "assets\\minecraft\\textures\\block\\pink_concrete_powder.png";
                                                    Matriz_Terrain[11, 28] = "assets\\minecraft\\textures\\block\\purple_concrete_powder.png";
                                                    Matriz_Terrain[12, 28] = "assets\\minecraft\\textures\\block\\red_concrete_powder.png";
                                                    Matriz_Terrain[13, 28] = "assets\\minecraft\\textures\\block\\light_gray_concrete_powder.png";
                                                    Matriz_Terrain[14, 28] = "assets\\minecraft\\textures\\block\\white_concrete_powder.png";
                                                    Matriz_Terrain[15, 28] = "assets\\minecraft\\textures\\block\\yellow_concrete_powder.png";

                                                    Matriz_Terrain[0, 29] = "assets\\minecraft\\textures\\block\\black_glazed_terracotta.png";
                                                    Matriz_Terrain[1, 29] = "assets\\minecraft\\textures\\block\\blue_glazed_terracotta.png";
                                                    Matriz_Terrain[2, 29] = "assets\\minecraft\\textures\\block\\brown_glazed_terracotta.png";
                                                    Matriz_Terrain[3, 29] = "assets\\minecraft\\textures\\block\\cyan_glazed_terracotta.png";
                                                    Matriz_Terrain[4, 29] = "assets\\minecraft\\textures\\block\\gray_glazed_terracotta.png";
                                                    Matriz_Terrain[5, 29] = "assets\\minecraft\\textures\\block\\green_glazed_terracotta.png";
                                                    Matriz_Terrain[6, 29] = "assets\\minecraft\\textures\\block\\light_blue_glazed_terracotta.png";
                                                    Matriz_Terrain[7, 29] = "assets\\minecraft\\textures\\block\\lime_glazed_terracotta.png";
                                                    Matriz_Terrain[8, 29] = "assets\\minecraft\\textures\\block\\magenta_glazed_terracotta.png";
                                                    Matriz_Terrain[9, 29] = "assets\\minecraft\\textures\\block\\orange_glazed_terracotta.png";
                                                    Matriz_Terrain[10, 29] = "assets\\minecraft\\textures\\block\\pink_glazed_terracotta.png";
                                                    Matriz_Terrain[11, 29] = "assets\\minecraft\\textures\\block\\purple_glazed_terracotta.png";
                                                    Matriz_Terrain[12, 29] = "assets\\minecraft\\textures\\block\\red_glazed_terracotta.png";
                                                    Matriz_Terrain[13, 29] = "assets\\minecraft\\textures\\block\\light_gray_glazed_terracotta.png";
                                                    Matriz_Terrain[14, 29] = "assets\\minecraft\\textures\\block\\white_glazed_terracotta.png";
                                                    Matriz_Terrain[15, 29] = "assets\\minecraft\\textures\\block\\yellow_glazed_terracotta.png";

                                                    Matriz_Terrain[0, 30] = "assets\\minecraft\\textures\\block\\_white_shulker_box.png"; // Correct?
                                                    Matriz_Terrain[1, 30] = "assets\\minecraft\\textures\\block\\_terrain_1_30.png"; // Empty?
                                                    Matriz_Terrain[2, 30] = "assets\\minecraft\\textures\\block\\_terrain_2_30.png"; // Water?
                                                    Matriz_Terrain[3, 30] = "assets\\minecraft\\textures\\block\\tall_seagrass_top.png";
                                                    Matriz_Terrain[4, 30] = "assets\\minecraft\\textures\\block\\tube_coral_block.png";
                                                    Matriz_Terrain[5, 30] = "assets\\minecraft\\textures\\block\\bubble_coral_block.png";
                                                    Matriz_Terrain[6, 30] = "assets\\minecraft\\textures\\block\\brain_coral_block.png";
                                                    Matriz_Terrain[7, 30] = "assets\\minecraft\\textures\\block\\fire_coral_block.png";
                                                    Matriz_Terrain[8, 30] = "assets\\minecraft\\textures\\block\\horn_coral_block.png";
                                                    Matriz_Terrain[9, 30] = "assets\\minecraft\\textures\\block\\tube_coral.png";
                                                    Matriz_Terrain[10, 30] = "assets\\minecraft\\textures\\block\\bubble_coral.png";
                                                    Matriz_Terrain[11, 30] = "assets\\minecraft\\textures\\block\\brain_coral.png";
                                                    Matriz_Terrain[12, 30] = "assets\\minecraft\\textures\\block\\fire_coral.png";
                                                    Matriz_Terrain[13, 30] = "assets\\minecraft\\textures\\block\\horn_coral.png";
                                                    Matriz_Terrain[14, 30] = "assets\\minecraft\\textures\\block\\sea_pickle.png";
                                                    Matriz_Terrain[15, 30] = "assets\\minecraft\\textures\\block\\blue_ice.png";

                                                    Matriz_Terrain[0, 31] = "assets\\minecraft\\textures\\block\\dried_kelp_top.png";
                                                    Matriz_Terrain[1, 31] = "assets\\minecraft\\textures\\block\\dried_kelp_side.png";
                                                    Matriz_Terrain[2, 31] = "assets\\minecraft\\textures\\block\\seagrass.png";
                                                    Matriz_Terrain[3, 31] = "assets\\minecraft\\textures\\block\\tall_seagrass_bottom.png";
                                                    Matriz_Terrain[4, 31] = "assets\\minecraft\\textures\\block\\dead_tube_coral_block.png";
                                                    Matriz_Terrain[5, 31] = "assets\\minecraft\\textures\\block\\dead_bubble_coral_block.png";
                                                    Matriz_Terrain[6, 31] = "assets\\minecraft\\textures\\block\\dead_brain_coral_block.png";
                                                    Matriz_Terrain[7, 31] = "assets\\minecraft\\textures\\block\\dead_fire_coral_block.png";
                                                    Matriz_Terrain[8, 31] = "assets\\minecraft\\textures\\block\\dead_horn_coral_block.png";
                                                    Matriz_Terrain[9, 31] = "assets\\minecraft\\textures\\block\\tube_coral_fan.png";
                                                    Matriz_Terrain[10, 31] = "assets\\minecraft\\textures\\block\\bubble_coral_fan.png";
                                                    Matriz_Terrain[11, 31] = "assets\\minecraft\\textures\\block\\brain_coral_fan.png";
                                                    Matriz_Terrain[12, 31] = "assets\\minecraft\\textures\\block\\fire_coral_fan.png";
                                                    Matriz_Terrain[13, 31] = "assets\\minecraft\\textures\\block\\horn_coral_fan.png";
                                                    Matriz_Terrain[14, 31] = "assets\\minecraft\\textures\\block\\_terrain_14_31.png"; // Empty?
                                                    Matriz_Terrain[15, 31] = "assets\\minecraft\\textures\\block\\_terrain_15_31.png"; // Empty?

                                                    Matriz_Terrain[0, 32] = "assets\\minecraft\\textures\\block\\_kelp_plant.png"; // Unknown.
                                                    Matriz_Terrain[1, 32] = "assets\\minecraft\\textures\\block\\_kelp_plant_.png"; // Unknown.
                                                    Matriz_Terrain[2, 32] = "assets\\minecraft\\textures\\block\\_kelp_plant__.png"; // Unknown.
                                                    Matriz_Terrain[3, 32] = "assets\\minecraft\\textures\\block\\_kelp_plant___.png"; // Unknown.
                                                    Matriz_Terrain[4, 32] = "assets\\minecraft\\textures\\block\\_kelp.png"; // Unknown.
                                                    Matriz_Terrain[5, 32] = "assets\\minecraft\\textures\\block\\_kelp_.png"; // Unknown.
                                                    Matriz_Terrain[6, 32] = "assets\\minecraft\\textures\\block\\_kelp__.png"; // Unknown.
                                                    Matriz_Terrain[7, 32] = "assets\\minecraft\\textures\\block\\_kelp___.png"; // Unknown.
                                                    Matriz_Terrain[8, 32] = "assets\\minecraft\\textures\\block\\_seagrass_.png"; // Repeated?
                                                    Matriz_Terrain[9, 32] = "assets\\minecraft\\textures\\block\\dead_tube_coral_fan.png";
                                                    Matriz_Terrain[10, 32] = "assets\\minecraft\\textures\\block\\dead_bubble_coral_fan.png";
                                                    Matriz_Terrain[11, 32] = "assets\\minecraft\\textures\\block\\dead_brain_coral_fan.png";
                                                    Matriz_Terrain[12, 32] = "assets\\minecraft\\textures\\block\\dead_fire_coral_fan.png";
                                                    Matriz_Terrain[13, 32] = "assets\\minecraft\\textures\\block\\dead_horn_coral_fan.png";
                                                    Matriz_Terrain[14, 32] = "assets\\minecraft\\textures\\block\\_terrain_14_32.png"; // Empty?
                                                    Matriz_Terrain[15, 32] = "assets\\minecraft\\textures\\block\\spruce_trapdoor.png";

                                                    Matriz_Terrain[0, 33] = "assets\\minecraft\\textures\\block\\stripped_oak_log.png";
                                                    Matriz_Terrain[1, 33] = "assets\\minecraft\\textures\\block\\stripped_oak_log_top.png";
                                                    Matriz_Terrain[2, 33] = "assets\\minecraft\\textures\\block\\stripped_acacia_log.png";
                                                    Matriz_Terrain[3, 33] = "assets\\minecraft\\textures\\block\\stripped_acacia_log_top.png";
                                                    Matriz_Terrain[4, 33] = "assets\\minecraft\\textures\\block\\stripped_birch_log.png";
                                                    Matriz_Terrain[5, 33] = "assets\\minecraft\\textures\\block\\stripped_birch_log_top.png";
                                                    Matriz_Terrain[6, 33] = "assets\\minecraft\\textures\\block\\stripped_dark_oak_log.png";
                                                    Matriz_Terrain[7, 33] = "assets\\minecraft\\textures\\block\\stripped_dark_oak_log_top.png";
                                                    Matriz_Terrain[8, 33] = "assets\\minecraft\\textures\\block\\stripped_jungle_log.png";
                                                    Matriz_Terrain[9, 33] = "assets\\minecraft\\textures\\block\\stripped_jungle_log_top.png";
                                                    Matriz_Terrain[10, 33] = "assets\\minecraft\\textures\\block\\stripped_spruce_log.png";
                                                    Matriz_Terrain[11, 33] = "assets\\minecraft\\textures\\block\\stripped_spruce_log_top.png";
                                                    Matriz_Terrain[12, 33] = "assets\\minecraft\\textures\\block\\acacia_trapdoor.png";
                                                    Matriz_Terrain[13, 33] = "assets\\minecraft\\textures\\block\\birch_trapdoor.png";
                                                    Matriz_Terrain[14, 33] = "assets\\minecraft\\textures\\block\\dark_oak_trapdoor.png";
                                                    Matriz_Terrain[15, 33] = "assets\\minecraft\\textures\\block\\jungle_trapdoor.png";

                                                    // First copy the full image bytes to include it in the resource packs.
                                                    byte[] Matriz_Bytes = Program.Obtener_Matriz_Bytes_Archivo(Ruta_Recurso_Entrada);
                                                    if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                                                    {
                                                        Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\assets\\minecraft\\textures\\block\\_terrain.png";
                                                        Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                        FileStream Lector = new FileStream(Ruta_Recurso_Salida, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                                        Lector.SetLength(0L);
                                                        Lector.Seek(0L, SeekOrigin.Begin);
                                                        Lector.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                                        Lector.Close();
                                                        Lector.Dispose();
                                                        Lector = null;
                                                        Matriz_Bytes = null;
                                                    }

                                                    // Now load the file as an image and split it into multiple images.
                                                    Bitmap Imagen_Original = Program.Cargar_Imagen_Ruta(Ruta_Recurso_Entrada, CheckState.Indeterminate);
                                                    if (Imagen_Original != null)
                                                    {
                                                        int Ancho = Imagen_Original.Width;
                                                        int Alto = Imagen_Original.Height;
                                                        int Ancho_Alto = Imagen_Original.Width / 16; // Texture dimensions.
                                                        for (int Índice_Y = 0; Índice_Y < Alto / Ancho_Alto; Índice_Y++)
                                                        {
                                                            for (int Índice_X = 0; Índice_X < Ancho / Ancho_Alto; Índice_X++)
                                                            {
                                                                Bitmap Imagen = Imagen_Original.Clone(new Rectangle(Índice_X * Ancho_Alto, Índice_Y * Ancho_Alto, Ancho_Alto, Ancho_Alto), Imagen_Original.PixelFormat) as Bitmap;
                                                                if (Imagen != null)
                                                                {
                                                                    Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\" + (!string.IsNullOrEmpty(Matriz_Terrain[Índice_X, Índice_Y]) ? Matriz_Terrain[Índice_X, Índice_Y] : "assets\\minecraft\\textures\\block\\_terrain_" + Índice_X.ToString() + "_" + Índice_Y.ToString() + ".png");
                                                                    Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                                    Imagen.Save(Ruta_Recurso_Salida, ImageFormat.Png);
                                                                    Imagen.Dispose();
                                                                    Imagen = null;
                                                                }
                                                            }
                                                        }
                                                        Imagen_Original.Dispose();
                                                        Imagen_Original = null;
                                                    }
                                                }
                                                else if (string.Compare(Nombre_Recurso, "sea_turtle", true) == 0)
                                                {
                                                    // This seems to be moved to the left 1 pixel at 16x and 2 at 32x, so reverse that.
                                                    Bitmap Imagen_Original = Program.Cargar_Imagen_Ruta(Ruta_Recurso_Entrada, CheckState.Indeterminate);
                                                    if (Imagen_Original != null)
                                                    {
                                                        int Ancho = Imagen_Original.Width;
                                                        int Alto = Imagen_Original.Height;
                                                        int Resolución = Ancho / 128; // Pixels to move to the right.
                                                        Bitmap Imagen = new Bitmap(Ancho, Alto, Imagen_Original.PixelFormat);
                                                        Graphics Pintar = Graphics.FromImage(Imagen);
                                                        Pintar.CompositingMode = CompositingMode.SourceCopy;
                                                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                                        Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                                                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                                        Pintar.SmoothingMode = SmoothingMode.None;
                                                        Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                                                        Pintar.DrawImage(Imagen_Original, new Rectangle(Resolución, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                                        Pintar.Dispose();
                                                        Pintar = null;
                                                        Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\" + Entrada.Value;
                                                        Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                        Imagen.Save(Ruta_Recurso_Salida, ImageFormat.Png);
                                                        Imagen.Dispose();
                                                        Imagen = null;
                                                    }
                                                }
                                                else if (string.Compare(Nombre_Recurso, "sheep", true) == 0)
                                                {
                                                    Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\" + Entrada.Value;
                                                    Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                    Bitmap Imagen_Original = Program.Cargar_Imagen_Ruta(Ruta_Recurso_Entrada, CheckState.Indeterminate);
                                                    if (Imagen_Original != null)
                                                    {
                                                        int Ancho = Imagen_Original.Width;
                                                        int Alto = Imagen_Original.Height;
                                                        if (Ancho != Alto) // Single sheep texture, copy.
                                                        {
                                                            byte[] Matriz_Bytes = Program.Obtener_Matriz_Bytes_Archivo(Ruta_Recurso_Entrada);
                                                            if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                                                            {
                                                                FileStream Lector = new FileStream(Ruta_Recurso_Salida, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                                                Lector.SetLength(0L);
                                                                Lector.Seek(0L, SeekOrigin.Begin);
                                                                Lector.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                                                Lector.Close();
                                                                Lector.Dispose();
                                                                Lector = null;
                                                                Matriz_Bytes = null;
                                                            }
                                                        }
                                                        else // Double sheep texture, split.
                                                        {
                                                            Bitmap Imagen = Imagen_Original.Clone(new Rectangle(0, 0, Ancho, Alto / 2), Imagen_Original.PixelFormat);
                                                            Imagen.Save(Ruta_Recurso_Salida, ImageFormat.Png);
                                                            Imagen.Dispose();
                                                            Imagen = null;
                                                            Imagen = Imagen_Original.Clone(new Rectangle(0, Alto / 2, Ancho, Alto / 2), Imagen_Original.PixelFormat);
                                                            Imagen.Save(Ruta_Pack_Salida + "\\assets\\minecraft\\textures\\entity\\sheep\\sheep_fur.png", ImageFormat.Png);
                                                            Imagen.Dispose();
                                                            Imagen = null;
                                                        }
                                                        Imagen_Original.Dispose();
                                                        Imagen_Original = null;
                                                    }
                                                }
                                                else // Regular resource ready to copy.
                                                {
                                                    // The files shouldn't be too large, so load them at once.
                                                    byte[] Matriz_Bytes = Program.Obtener_Matriz_Bytes_Archivo(Ruta_Recurso_Entrada);
                                                    if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                                                    {
                                                        Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\" + Entrada.Value;
                                                        Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                                        FileStream Lector = new FileStream(Ruta_Recurso_Salida, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                                        Lector.SetLength(0L);
                                                        Lector.Seek(0L, SeekOrigin.Begin);
                                                        Lector.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                                        Lector.Close();
                                                        Lector.Dispose();
                                                        Lector = null;
                                                        // Now load the image to see if it should be animated based on it's dimensions.
                                                        Bitmap Imagen_Original = Program.Cargar_Imagen_Ruta(Ruta_Recurso_Entrada, CheckState.Indeterminate);
                                                        if (Imagen_Original != null)
                                                        {
                                                            int Ancho = Imagen_Original.Width;
                                                            int Alto = Imagen_Original.Height;
                                                            int Total_Cuadros = Alto / Ancho;
                                                            Imagen_Original.Dispose();
                                                            Imagen_Original = null;
                                                            if (Total_Cuadros >= 3) // Assume that images with at least a triple height are animated.
                                                            {
                                                                // Try to guess if it's a known block and copy it's custom animation times.
                                                                string Nombre_Recurso_Salida = Path.GetFileNameWithoutExtension(Ruta_Recurso_Salida);
                                                                string Ruta_Recurso_Salida_Mcmeta = Ruta_Pack_Salida + "\\" + Entrada.Value + ".mcmeta";
                                                                Lector = new FileStream(Ruta_Recurso_Salida_Mcmeta, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                                                Lector.SetLength(0L);
                                                                Lector.Seek(0L, SeekOrigin.Begin);
                                                                if (string.Compare(Nombre_Recurso_Salida, "blast_furnace_front_on", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_blast_furnace_front_on_png_mcmeta, 0, Matriz_Bytes_blast_furnace_front_on_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "campfire_fire", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_campfire_fire_png_mcmeta, 0, Matriz_Bytes_campfire_fire_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "campfire_log_lit", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_campfire_log_lit_png_mcmeta, 0, Matriz_Bytes_campfire_log_lit_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "chain_command_block_back", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_chain_command_block_back_png_mcmeta, 0, Matriz_Bytes_blast_furnace_front_on_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "chain_command_block_conditional", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_chain_command_block_conditional_png_mcmeta, 0, Matriz_Bytes_chain_command_block_conditional_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "chain_command_block_front", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_chain_command_block_front_png_mcmeta, 0, Matriz_Bytes_chain_command_block_front_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "chain_command_block_side", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_chain_command_block_side_png_mcmeta, 0, Matriz_Bytes_chain_command_block_side_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "command_block_back", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_command_block_back_png_mcmeta, 0, Matriz_Bytes_command_block_back_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "command_block_conditional", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_command_block_conditional_png_mcmeta, 0, Matriz_Bytes_command_block_conditional_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "command_block_front", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_command_block_front_png_mcmeta, 0, Matriz_Bytes_command_block_front_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "command_block_side", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_command_block_side_png_mcmeta, 0, Matriz_Bytes_command_block_side_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "fire_0", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_fire_0_png_mcmeta, 0, Matriz_Bytes_fire_0_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "fire_1", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_fire_1_png_mcmeta, 0, Matriz_Bytes_fire_1_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "kelp", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_kelp_png_mcmeta, 0, Matriz_Bytes_kelp_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "kelp_plant", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_kelp_plant_png_mcmeta, 0, Matriz_Bytes_kelp_plant_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "lantern", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_lantern_png_mcmeta, 0, Matriz_Bytes_lantern_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "lava_flow", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_lava_flow_png_mcmeta, 0, Matriz_Bytes_lava_flow_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "lava_still", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_lava_still_png_mcmeta, 0, Matriz_Bytes_lava_still_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "magma", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_magma_png_mcmeta, 0, Matriz_Bytes_magma_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "nether_portal", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_nether_portal_png_mcmeta, 0, Matriz_Bytes_nether_portal_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "prismarine", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_prismarine_png_mcmeta, 0, Matriz_Bytes_prismarine_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "repeating_command_block_back", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_repeating_command_block_back_png_mcmeta, 0, Matriz_Bytes_repeating_command_block_back_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "repeating_command_block_conditional", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_repeating_command_block_conditional_png_mcmeta, 0, Matriz_Bytes_repeating_command_block_conditional_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "repeating_command_block_front", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_repeating_command_block_front_png_mcmeta, 0, Matriz_Bytes_repeating_command_block_front_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "repeating_command_block_side", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_repeating_command_block_side_png_mcmeta, 0, Matriz_Bytes_repeating_command_block_side_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "sea_lantern", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_sea_lantern_png_mcmeta, 0, Matriz_Bytes_sea_lantern_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "seagrass", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_seagrass_png_mcmeta, 0, Matriz_Bytes_seagrass_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "smoker_front_on", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_smoker_front_on_png_mcmeta, 0, Matriz_Bytes_smoker_front_on_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "stonecutter_saw", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_stonecutter_saw_png_mcmeta, 0, Matriz_Bytes_stonecutter_saw_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "tall_seagrass_bottom", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_tall_seagrass_bottom_png_mcmeta, 0, Matriz_Bytes_tall_seagrass_bottom_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "tall_seagrass_top", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_tall_seagrass_top_png_mcmeta, 0, Matriz_Bytes_tall_seagrass_top_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "water_flow", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_water_flow_png_mcmeta, 0, Matriz_Bytes_water_flow_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else if (string.Compare(Nombre_Recurso_Salida, "water_still", true) == 0)
                                                                {
                                                                    Lector.Write(Matriz_Bytes_water_still_png_mcmeta, 0, Matriz_Bytes_water_still_png_mcmeta.Length);
                                                                    Lector.Flush();
                                                                }
                                                                else // Default animation time for unknown animated blocks, might not be perfect.
                                                                {
                                                                    // Build a custom animation file, without this won't be animations at all.
                                                                    StreamWriter Lector_Texto = new StreamWriter(Lector, Encoding.UTF8);
                                                                    bool Modo_Animación_Doble_Reflejada = true; // true = more realistic?
                                                                    if (!Modo_Animación_Doble_Reflejada) // Simple looped animation file.
                                                                    {
                                                                        Lector_Texto.WriteLine("{");
                                                                        Lector_Texto.WriteLine("    \"animation\": {");
                                                                        // Change the number below to any desired value, half second = "10", 1 second = "20", ten seconds = "200".
                                                                        Lector_Texto.WriteLine("        \"frametime\": 10"); // Time between frames.
                                                                        Lector_Texto.WriteLine("    }");
                                                                        Lector_Texto.Write("}");
                                                                        Lector_Texto.Flush();
                                                                    }
                                                                    else // Double animation, simple and reversed, all looped.
                                                                    {
                                                                        Lector_Texto.WriteLine("{");
                                                                        Lector_Texto.WriteLine("  \"animation\": {");
                                                                        // Change the number below to any desired value, half second = "10", 1 second = "20", ten seconds = "200".
                                                                        Lector_Texto.WriteLine("    \"frametime\": 10,"); // Time between frames.
                                                                        //Lector_Texto.WriteLine("    \"interpolate\": true,"); // It looked too blurry.
                                                                        Lector_Texto.WriteLine("    \"frames\": [");
                                                                        for (int Índice_Cuadro = 0; Índice_Cuadro < Total_Cuadros; Índice_Cuadro++) // Regular animation.
                                                                        {
                                                                            Lector_Texto.WriteLine("      " + Índice_Cuadro.ToString() + ",");
                                                                        }
                                                                        // Avoid again the last and first frames since it's going to be looped.
                                                                        // Warning: don't add a "," at the last frame index or it will fail.
                                                                        for (int Índice_Cuadro = Total_Cuadros - 2; Índice_Cuadro > 0; Índice_Cuadro--) // Reverse animation.
                                                                        {
                                                                            Lector_Texto.WriteLine("      " + Índice_Cuadro.ToString() + (Índice_Cuadro > 1 ? "," : null));
                                                                        }
                                                                        Lector_Texto.WriteLine("    ]");
                                                                        Lector_Texto.WriteLine("  }");
                                                                        Lector_Texto.Write("}");
                                                                        Lector_Texto.Flush();
                                                                    }
                                                                    Lector_Texto.Close();
                                                                    Lector_Texto.Dispose();
                                                                    Lector_Texto = null;
                                                                }
                                                                Lector.Close();
                                                                Lector.Dispose();
                                                                Lector = null;
                                                                Ruta_Recurso_Salida_Mcmeta = null;
                                                            }
                                                        }
                                                    }
                                                    Matriz_Bytes = null;
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    // Finally add the "pack.mcmeta" description file and "pack.png".
                                    string Ruta_Pack_Mcmeta = Ruta_Pack_Salida + "\\pack.mcmeta";
                                    FileStream Lector_Pack_Mcmeta = new FileStream(Ruta_Pack_Mcmeta, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                    Lector_Pack_Mcmeta.SetLength(0L);
                                    Lector_Pack_Mcmeta.Seek(0L, SeekOrigin.Begin);
                                    StreamWriter Lector_Texto_Pack_Mcmeta = new StreamWriter(Lector_Pack_Mcmeta, Encoding.UTF8);
                                    Lector_Texto_Pack_Mcmeta.WriteLine("{");
                                    Lector_Texto_Pack_Mcmeta.WriteLine("  \"pack\": {");
                                    Lector_Texto_Pack_Mcmeta.WriteLine("    \"pack_format\": 4,");
                                    Lector_Texto_Pack_Mcmeta.WriteLine("    \"description\": \"§f" + Nombre_Pack + "§r\\n§6Converted By:§r §cJupisoft§r\"");
                                    Lector_Texto_Pack_Mcmeta.WriteLine("  }");
                                    Lector_Texto_Pack_Mcmeta.Write("}");
                                    Lector_Texto_Pack_Mcmeta.Flush();
                                    Lector_Texto_Pack_Mcmeta.Close();
                                    Lector_Texto_Pack_Mcmeta.Dispose();
                                    Lector_Texto_Pack_Mcmeta = null;
                                    Lector_Pack_Mcmeta.Close();
                                    Lector_Pack_Mcmeta.Dispose();
                                    Lector_Pack_Mcmeta = null;
                                    Ruta_Pack_Mcmeta = null;

                                    string Ruta_Pack_Png = Path.GetDirectoryName(Ruta_Pack_Entrada) + "\\-1.png";
                                    if (File.Exists(Ruta_Pack_Png))
                                    {
                                        byte[] Matriz_Bytes = Program.Obtener_Matriz_Bytes_Archivo(Ruta_Pack_Png);
                                        if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                                        {
                                            string Ruta_Recurso_Salida = Ruta_Pack_Salida + "\\pack.png";
                                            Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_Recurso_Salida));
                                            FileStream Lector = new FileStream(Ruta_Recurso_Salida, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                            Lector.SetLength(0L);
                                            Lector.Seek(0L, SeekOrigin.Begin);
                                            Lector.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                            Lector.Close();
                                            Lector.Dispose();
                                            Lector = null;
                                        }
                                        Matriz_Bytes = null;
                                    }
                                    Ruta_Pack_Png = null;
                                }
                                Matriz_Rutas = null;
                            }
                        }
                        SystemSounds.Asterisk.Play();
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Recursively extracts and exports any known image found within the specified byte array, until all the possible images have been saved.
        /// </summary>
        /// <param name="Matriz_Bytes">Any valid byte array with at least 4 bytes.</param>
        /// <param name="Ruta_Salida">The output folder path.</param>
        /// <param name="Índice_Recurso_Global">The global extracted resources index.</param>
        /// <returns>Returns the number of extracted images. Returns 0 on any error.</returns>
        internal int Extraer_Recursos_Recursivos(byte[] Matriz_Bytes, string Ruta_Salida, ref int Índice_Recurso_Global, int Iteración, SortedDictionary<long, string> Diccionario_Índices_Rutas)
        {
            try
            {
                int Total_Recursos = 0;
                if (Matriz_Bytes != null && Matriz_Bytes.Length >= 4) // 4 bytes for the headers.
                {
                    //int Marcadores_Android = 0; // 0, 114, 101, 115
                    //int Marcadores_BlackBerry = 0; // 0, 47
                    //int Marcadores_BMP_Apertura = 0; // 66, 77, 54 = BM6
                    //int Marcadores_BMP_Cierre = 0; // ?
                    //int Marcadores_GIF_Apertura = 0; // 71, 73, 70, 56 = GIF8
                    //int Marcadores_GIF_Cierre = 0; // 0, 59
                    //int Marcadores_JPG_Apertura = 0; // 255, 216, 255, 224~239 = ÿØÿ~
                    //int Marcadores_JPG_Cierre = 0; // 255, 217
                    //int Marcadores_PNG_Apertura = 0; // 137, 80, 78, 71 = ~PNG
                    //int Marcadores_PNG_Cierre = 0; // 174, 66, 96, 130
                    //int Marcadores_TGA_Apertura = 0; // 0, 0, 2, 0
                    //int Marcadores_TGA_Cierre = 0; // ?
                    //int Marcadores_TIF_Apertura = 0; // 73, 73, 42, 0
                    //int Marcadores_TIF_Cierre = 0; // ?

                    //List<int> Lista_Marcadores_Android = new List<int>();
                    //List<int> Lista_Marcadores_BlackBerry = new List<int>();
                    //List<int> Lista_Marcadores_BMP_Apertura = new List<int>();
                    //List<int> Lista_Marcadores_BMP_Cierre = new List<int>();
                    List<int> Lista_Marcadores_GIF_Apertura = new List<int>();
                    //List<int> Lista_Marcadores_GIF_Cierre = new List<int>();
                    List<int> Lista_Marcadores_JPG_Apertura = new List<int>();
                    //List<int> Lista_Marcadores_JPG_Cierre = new List<int>();
                    List<int> Lista_Marcadores_PNG_Apertura = new List<int>();
                    //List<int> Lista_Marcadores_PNG_Cierre = new List<int>();
                    List<int> Lista_Marcadores_TGA_Apertura = new List<int>();
                    //List<int> Lista_Marcadores_TGA_Cierre = new List<int>();
                    //List<int> Lista_Marcadores_TIF_Apertura = new List<int>();
                    //List<int> Lista_Marcadores_TIF_Cierre = new List<int>();

                    int Longitud = Matriz_Bytes.Length; // Quicker access to the length?
                    // This looks for known image header bytes, and remember it's positions.
                    for (int Índice_Byte = 0; Índice_Byte < Longitud; Índice_Byte++)
                    {
                        if (Pendiente_Subproceso_Abortar) return 0; // Cancel safely before time.
                        //if (Índice_Byte + 3 < Longitud && Matriz_Bytes[Índice_Byte] == 0 && Matriz_Bytes[Índice_Byte + 1] == 114 && Matriz_Bytes[Índice_Byte + 2] == 101 && Matriz_Bytes[Índice_Byte + 3] == 115 && !Lista_Marcadores_Android.Contains(Índice_Byte)) Lista_Marcadores_Android.Add(Índice_Byte + 1);

                        //if (Matriz_Bytes[Índice_Byte] == 0 && Matriz_Bytes[Índice_Byte + 1] == 47 && !Lista_Marcadores_BlackBerry.Contains(Índice_Byte + 2)) Lista_Marcadores_BlackBerry.Add(Índice_Byte + 2);

                        //if (Índice_Byte + 2 < Longitud && Matriz_Bytes[Índice_Byte] == 66 && Matriz_Bytes[Índice_Byte + 1] == 77 && Matriz_Bytes[Índice_Byte + 2] == 54 && !Lista_Marcadores_BMP_Apertura.Contains(Índice_Byte)) Lista_Marcadores_BMP_Apertura.Add(Índice_Byte);

                        if (Índice_Byte + 3 < Longitud && Matriz_Bytes[Índice_Byte] == 71 && Matriz_Bytes[Índice_Byte + 1] == 73 && Matriz_Bytes[Índice_Byte + 2] == 70 && Matriz_Bytes[Índice_Byte + 3] == 56 && !Lista_Marcadores_GIF_Apertura.Contains(Índice_Byte)) Lista_Marcadores_GIF_Apertura.Add(Índice_Byte);
                        //if (Índice_Byte + 1 < Longitud && Matriz_Bytes[Índice_Byte] == 0 && Matriz_Bytes[Índice_Byte + 1] == 59 && !Lista_Marcadores_GIF_Cierre.Contains(Índice_Byte + 2)) Lista_Marcadores_GIF_Cierre.Add(Índice_Byte + 2);

                        if (Índice_Byte + 2/*3*/ < Longitud && Matriz_Bytes[Índice_Byte] == 255 && Matriz_Bytes[Índice_Byte + 1] == 216 && Matriz_Bytes[Índice_Byte + 2] == 255 && /*Matriz_Bytes_Búfer[Índice_Byte + 3] >= 128/*224 && Matriz_Bytes_Búfer[Índice_Byte + 3] <= 239 && */!Lista_Marcadores_JPG_Apertura.Contains(Índice_Byte)) Lista_Marcadores_JPG_Apertura.Add(Índice_Byte);
                        //if (Índice_Byte + 1 < Longitud && Matriz_Bytes[Índice_Byte] == 255 && Matriz_Bytes[Índice_Byte + 1] == 217 && !Lista_Marcadores_JPG_Cierre.Contains(Índice_Byte + 2)) Lista_Marcadores_JPG_Cierre.Add(Índice_Byte + 2);

                        if (Índice_Byte + 3 < Longitud && Matriz_Bytes[Índice_Byte] == 137 && Matriz_Bytes[Índice_Byte + 1] == 80 && Matriz_Bytes[Índice_Byte + 2] == 78 && Matriz_Bytes[Índice_Byte + 3] == 71 && !Lista_Marcadores_PNG_Apertura.Contains(Índice_Byte)) Lista_Marcadores_PNG_Apertura.Add(Índice_Byte);
                        //if (Índice_Byte + 1 < Longitud && Matriz_Bytes[Índice_Byte] == 96 && Matriz_Bytes[Índice_Byte + 1] == 130 && !Lista_Marcadores_PNG_Cierre.Contains(Índice_Byte + 2)) Lista_Marcadores_PNG_Cierre.Add(Índice_Byte + 2);

                        if (Índice_Byte + 3 < Longitud && Matriz_Bytes[Índice_Byte] == 0 && Matriz_Bytes[Índice_Byte + 1] == 128 && Matriz_Bytes[Índice_Byte + 2] == 0 && Matriz_Bytes[Índice_Byte + 3] == 128 && !Lista_Marcadores_TGA_Apertura.Contains(Índice_Byte)) Lista_Marcadores_TGA_Apertura.Add(Índice_Byte);
                        //if (Índice_Búfer + 3 < Longitud && Matriz_Bytes_Búfer[Índice_Byte] == 0 && Matriz_Bytes_Búfer[Índice_Byte + 1] == 0 && Matriz_Bytes_Búfer[Índice_Byte + 2] == 2 && Matriz_Bytes_Búfer[Índice_Byte + 3] == 0 && !Lista_Marcadores_TGA_Apertura.Contains(Índice_Byte)) Lista_Marcadores_TGA_Apertura.Add(Índice_Byte);

                        //if (Índice_Byte + 3 < Longitud && Matriz_Bytes[Índice_Byte] == 73 && Matriz_Bytes[Índice_Byte + 1] == 73 && Matriz_Bytes[Índice_Byte + 2] == 42 && Matriz_Bytes[Índice_Byte + 3] == 0 && !Lista_Marcadores_TIF_Apertura.Contains(Índice_Byte)) Lista_Marcadores_TIF_Apertura.Add(Índice_Byte);
                    }

                    //List<string> Lista_Extensiones = new List<string>(new string[] { ".gif", ".jpg", ".png" });
                    List<List<int>> Lista_Listas_Apertura = new List<List<int>>();
                    //List<List<int>> Lista_Listas_Cierre = new List<List<int>>();

                    Lista_Listas_Apertura.Add(new List<int>());
                    Lista_Listas_Apertura[0].AddRange(Lista_Marcadores_GIF_Apertura);
                    Lista_Listas_Apertura[0].AddRange(Lista_Marcadores_JPG_Apertura);
                    Lista_Listas_Apertura[0].AddRange(Lista_Marcadores_PNG_Apertura);
                    Lista_Listas_Apertura[0].Sort();

                    //Lista_Listas_Cierre.Add(Lista_Marcadores_GIF_Cierre);
                    //Lista_Listas_Cierre.Add(Lista_Marcadores_JPG_Cierre);
                    //Lista_Listas_Cierre.Add(Lista_Marcadores_PNG_Cierre);

                    for (int Índice_Formato = 0; Índice_Formato < Lista_Listas_Apertura.Count; Índice_Formato++) // 3 known image formats.
                    {
                        for (int Índice_Apertura = 0; Índice_Apertura < Lista_Listas_Apertura[Índice_Formato].Count; Índice_Apertura++)
                        {
                            if (Pendiente_Subproceso_Abortar) return 0; // Cancel safely before time.
                            int Índice_Formato_Cierre = -1;
                            if (Índice_Apertura + 1 < Lista_Listas_Apertura[Índice_Formato].Count)
                            {
                                Índice_Formato_Cierre = Lista_Listas_Apertura[Índice_Formato][Índice_Apertura + 1];
                            }
                            else Índice_Formato_Cierre = Longitud;
                            /*if (Lista_Listas_Cierre[Índice_Formato].Count > 0)
                            {
                                for (int Índice_Cierre = 0; Índice_Cierre < Lista_Listas_Cierre[Índice_Formato].Count; Índice_Cierre++)
                                {
                                    if (Lista_Listas_Cierre[Índice_Formato][Índice_Cierre] > Lista_Listas_Apertura[Índice_Formato][Índice_Apertura])
                                    {
                                        Índice_Formato_Cierre = Lista_Listas_Cierre[Índice_Formato][Índice_Cierre];
                                        break;
                                    }
                                }
                            }
                            else Índice_Formato_Cierre = Longitud;*/
                            /*if (Índice_Apertura + 1 < Lista_Listas_Apertura[Índice_Formato].Count)
                            {
                                if (Lista_Listas_Cierre[Índice_Formato][0] < Lista_Listas_Apertura[Índice_Formato][Índice_Apertura + 1])
                                {
                                    for (int Subíndice = Lista_Listas_Cierre[Índice_Formato].Count - 1; Subíndice >= 0; Subíndice--)
                                    {
                                        if (Pendiente_Subproceso_Abortar) return 0; // Cancel safely before time.
                                        if (Lista_Listas_Cierre[Índice_Formato][Subíndice] < Lista_Listas_Apertura[Índice_Formato][Índice_Apertura + 1])
                                        {
                                            if (Lista_Listas_Cierre[Índice_Formato][Subíndice] > Lista_Listas_Apertura[Índice_Formato][Índice_Apertura]) Índice_Formato_Cierre = Lista_Listas_Cierre[Índice_Formato][Subíndice];
                                            else Índice_Formato_Cierre = Lista_Listas_Apertura[Índice_Formato][Índice_Apertura + 1];
                                            break;
                                        }
                                    }
                                }
                                else Índice_Formato_Cierre = Lista_Listas_Apertura[Índice_Formato][Índice_Apertura + 1];
                            }
                            else if (Lista_Listas_Cierre[Índice_Formato].Count > 0 && Lista_Listas_Cierre[Índice_Formato][Lista_Listas_Cierre[Índice_Formato].Count - 1] > Lista_Listas_Apertura[Índice_Formato][Índice_Apertura]) Índice_Formato_Cierre = Lista_Listas_Cierre[Índice_Formato][Lista_Listas_Cierre[Índice_Formato].Count - 1];
                            else Índice_Formato_Cierre = Longitud;*/

                            byte[] Matriz_Bytes_Recurso = new byte[!Variable_Extraer_Tamaño_Máximo ? Índice_Formato_Cierre - Lista_Listas_Apertura[Índice_Formato][Índice_Apertura] : Longitud - Lista_Listas_Apertura[Índice_Formato][Índice_Apertura]];
                            //Lector.Seek(Lista_Listas_Apertura[Índice_Formato][Índice], SeekOrigin.Begin);
                            //Lector.Read(Matriz_Bytes_Recurso, 0, Matriz_Bytes_Recurso.Length);
                            Array.Copy(Matriz_Bytes, Lista_Listas_Apertura[Índice_Formato][Índice_Apertura], Matriz_Bytes_Recurso, 0, Matriz_Bytes_Recurso.Length);

                            bool Recurso_Válido = false;
                            try
                            {
                                MemoryStream Lector_Memoria = new MemoryStream(Matriz_Bytes_Recurso, true);
                                if (Lector_Memoria != null)
                                {
                                    Image Imagen_Original = Image.FromStream(Lector_Memoria, false, false);
                                    if (Imagen_Original != null) // A new resource has been found.
                                    {
                                        Recurso_Válido = true;
                                        Imagen_Original.Dispose();
                                        Imagen_Original = null;
                                    }
                                    Lector_Memoria.Close();
                                    Lector_Memoria.Dispose();
                                    Lector_Memoria = null;
                                }
                            }
                            catch { Recurso_Válido = false; }
                            /*finally
                            {
                                GC.Collect(); // Recover RAM memory at the end.
                                GC.GetTotalMemory(true);
                            }*/
                            //catch { Recurso_Válido = false; }
                            if (Recurso_Válido)
                            {
                                MemoryStream Lector_Memoria = new MemoryStream(Matriz_Bytes_Recurso);
                                if (Lector_Memoria != null)
                                {
                                    Image Imagen_Original = Image.FromStream(Lector_Memoria, false, false);
                                    if (Imagen_Original != null)
                                    {
                                        int Ancho = Imagen_Original.Width;
                                        int Alto = Imagen_Original.Height;
                                        Bitmap Imagen = new Bitmap(Ancho, Alto, /*!Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? PixelFormat.Format24bppRgb : */PixelFormat.Format32bppArgb);
                                        Graphics Pintar = Graphics.FromImage(Imagen);
                                        Pintar.CompositingMode = CompositingMode.SourceCopy;
                                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                        Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                        Pintar.SmoothingMode = SmoothingMode.HighQuality;
                                        Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                                        Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                        Pintar.Dispose();
                                        Pintar = null; // This copy should remove unused bytes from the resource.
                                        if (Ruta_Salida.ToLowerInvariant().EndsWith("media")) // Some images have R and B inverted.
                                        {
                                            BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                                            byte[] Matriz_Bytes_ARGB = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                                            Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes_ARGB, 0, Matriz_Bytes_ARGB.Length);
                                            int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                                            int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                                            byte Valor = 0;
                                            for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                                            {
                                                for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento) // Swap Red and Blue.
                                                {
                                                    Valor = Matriz_Bytes_ARGB[Índice];
                                                    Matriz_Bytes_ARGB[Índice] = Matriz_Bytes_ARGB[Índice + 2];
                                                    Matriz_Bytes_ARGB[Índice + 2] = Valor;
                                                }
                                            }
                                            Marshal.Copy(Matriz_Bytes_ARGB, 0, Bitmap_Data.Scan0, Matriz_Bytes_ARGB.Length);
                                            Imagen.UnlockBits(Bitmap_Data);
                                            Bitmap_Data = null;
                                            Matriz_Bytes_ARGB = null;
                                        }
                                        Program.Crear_Carpetas(Ruta_Salida);
                                        string Ruta = Total_Recursos.ToString(); // Use a numeric file name.
                                        while (Ruta.Length < 8) Ruta = '0' + Ruta; // Set file name length to 8.
                                        Ruta = Ruta_Salida + "\\" + Ruta; // Add the full path.
                                        while (File.Exists(Ruta + ".png")) Ruta += '_'; // Avoid overwriting.
                                        Ruta += ".png"; // Add the extension.
                                        Imagen.Save(Ruta, ImageFormat.Png); // Save the resource.
                                        Total_Recursos++; // Resource saved without errors.
                                        Índice_Recurso_Global++; // Increase the counter of saved resources.
                                        this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [Resources extracted: " + Program.Traducir_Número(Índice_Recurso_Global + 1) + "]" });
                                        Ruta = null; // Free the rest of variables.
                                        Imagen.Dispose();
                                        Imagen = null;
                                        Imagen_Original.Dispose();
                                        Imagen_Original = null;
                                    }
                                    Lector_Memoria.Close();
                                    Lector_Memoria.Dispose();
                                    Lector_Memoria = null;
                                }
                                /*if (Iteración <= 1 && Matriz_Bytes_Recurso.Length > 4) // Look a second time for more resources inside this one.
                                {
                                    Array.Copy(Matriz_Bytes_Recurso, 4, Matriz_Bytes_Recurso, 0, Matriz_Bytes_Recurso.Length - 4); // Move 4 bytes to the left.
                                    Array.Resize(ref Matriz_Bytes_Recurso, Matriz_Bytes_Recurso.Length - 4); // Remove the first byte and resize.
                                    Extraer_Recursos_Recursivos(Matriz_Bytes_Recurso, Ruta_Salida, ref Índice_Recurso, Iteración + 1);
                                }*/
                                /*else
                                {
                                    // Ignore it, it was already saved from the function above, which is also this one.
                                }*/
                            }
                            else continue; // Ignore.
                        }
                    }
                }
                return Total_Recursos;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return 0;
        }

        /// <summary>
        /// Recursively extracts and exports any known image found within the specified byte array, until all the possible images have been saved.
        /// </summary>
        /// <param name="Matriz_Bytes">Any valid byte array with at least 4 bytes.</param>
        /// <param name="Ruta_Salida">The output folder path.</param>
        /// <param name="Índice_Recurso_Global">The global extracted resources index.</param>
        /// <returns>Returns the number of extracted images. Returns 0 on any error.</returns>
        internal int Extraer_Recursos_Máximos(byte[] Matriz_Bytes, string Ruta_Salida, ref int Índice_Recurso_Global, int Iteración, SortedDictionary<long, string> Diccionario_Índices_Rutas)
        {
            try
            {
                int Total_Recursos = 0;
                if (Matriz_Bytes != null && Matriz_Bytes.Length >= 0)
                {
                    bool Modo_TGA = false; // It needs the CPU intense and extremely slow TGA mode?
                    int Índice_Siguiente = -1;
                    /*if (Diccionario_Índices_Rutas != null && Diccionario_Índices_Rutas.Count > 0)
                    {
                        foreach (KeyValuePair<long, string> Entrada in Diccionario_Índices_Rutas)
                        {
                            if (Entrada.Key >= Índice_Byte) // We are here.
                            {
                                if (!string.IsNullOrEmpty(Entrada.Value) && Entrada.Value.EndsWith(".tga"))
                                {
                                    Modo_TGA = true; // It should be a TGA image.
                                    break;
                                }
                            }
                        }
                    }*/
                    if (!Modo_TGA)
                    {
                        // Try to export all the images inside, but it's the slower mode.
                        int Longitud = Matriz_Bytes.Length; // Quicker access to the length?
                        this.Invoke(new Invocación.Delegado_ProgressBar_Maximum(Invocación.Ejecutar_Delegado_ProgressBar_Maximum), new object[] { Barra_Progreso_Archivo, Longitud });
                        Dictionary<uint, object> Diccionario_CRC_32 = new Dictionary<uint, object>();
                        for (int Índice_Byte = 0; Índice_Byte < Longitud; Índice_Byte++)
                        {
                            try
                            {
                                if (Pendiente_Subproceso_Abortar) return 0; // Cancel safely before time.
                                this.Invoke(new Invocación.Delegado_ProgressBar_Value(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Archivo, Índice_Byte });
                                byte[] Matriz_Bytes_Temporal = new byte[Longitud - Índice_Byte];
                                Array.Copy(Matriz_Bytes, Índice_Byte, Matriz_Bytes_Temporal, 0, Matriz_Bytes_Temporal.Length);
                                MemoryStream Lector_Memoria = new MemoryStream(Matriz_Bytes_Temporal);
                                if (Lector_Memoria != null)
                                {
                                    Image Imagen_Original = null;
                                    try
                                    {
                                        MagickReadSettings MRS = new MagickReadSettings();
                                        MRS.Format = MagickFormat.Tga;
                                        MagickImage Imagen_Magick = new MagickImage(Matriz_Bytes_Temporal, MRS);
                                        Imagen_Original = Imagen_Magick.ToBitmap();
                                        //Imagen_Original = Imagen_Magick.ToBitmap(ImageFormat.Png);
                                    }
                                    catch { Imagen_Original = null; }
                                    if (Imagen_Original == null)
                                    {
                                        try { Imagen_Original = Image.FromStream(Lector_Memoria, false, false); }
                                        catch { Imagen_Original = null; }
                                    }
                                    if (Imagen_Original != null)
                                    {
                                        int Ancho = Imagen_Original.Width;
                                        int Alto = Imagen_Original.Height;
                                        Bitmap Imagen = new Bitmap(Ancho, Alto, /*!Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? PixelFormat.Format24bppRgb : */PixelFormat.Format32bppArgb);
                                        Graphics Pintar = Graphics.FromImage(Imagen);
                                        Pintar.CompositingMode = CompositingMode.SourceCopy;
                                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                        Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                        Pintar.SmoothingMode = SmoothingMode.HighQuality;
                                        Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                                        Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                        Pintar.Dispose();
                                        Pintar = null; // This copy should remove unused bytes from the resource.
                                        if (Ruta_Salida.ToLowerInvariant().EndsWith("media")) // Some images have R and B inverted.
                                        {
                                            BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                                            byte[] Matriz_Bytes_ARGB = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                                            Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes_ARGB, 0, Matriz_Bytes_ARGB.Length);
                                            int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                                            int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                                            byte Valor = 0;
                                            for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                                            {
                                                for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento) // Swap Red and Blue.
                                                {
                                                    Valor = Matriz_Bytes_ARGB[Índice];
                                                    Matriz_Bytes_ARGB[Índice] = Matriz_Bytes_ARGB[Índice + 2];
                                                    Matriz_Bytes_ARGB[Índice + 2] = Valor;
                                                }
                                            }
                                            Marshal.Copy(Matriz_Bytes_ARGB, 0, Bitmap_Data.Scan0, Matriz_Bytes_ARGB.Length);
                                            Imagen.UnlockBits(Bitmap_Data);
                                            Bitmap_Data = null;
                                            Matriz_Bytes_ARGB = null;
                                        }
                                        Program.Crear_Carpetas(Ruta_Salida);
                                        MemoryStream Lector_Memoria_Salida = new MemoryStream();
                                        Imagen.Save(Lector_Memoria_Salida, ImageFormat.Png); // Save the resource.
                                        byte[] Matriz_Bytes_Salida = Lector_Memoria_Salida.ToArray();
                                        uint CRC_32 = Program.Calcular_CRC32(Matriz_Bytes_Salida);
                                        if (!Diccionario_CRC_32.ContainsKey(CRC_32))
                                        {
                                            Diccionario_CRC_32.Add(CRC_32, null);
                                            string Ruta = Total_Recursos.ToString(); // Use a numeric file name.
                                            while (Ruta.Length < 8) Ruta = '0' + Ruta; // Set file name length to 8.
                                            Ruta = Ruta_Salida + "\\" + Ruta; // Add the full path.
                                            while (File.Exists(Ruta + ".png")) Ruta += '_'; // Avoid overwriting.
                                            Ruta += ".png"; // Add the extension.
                                            FileStream Lector_Salida = new FileStream(Ruta, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
                                            Lector_Salida.SetLength(0L);
                                            Lector_Salida.Seek(0L, SeekOrigin.Begin);
                                            Lector_Salida.Write(Matriz_Bytes_Salida, 0, Matriz_Bytes_Salida.Length);
                                            Lector_Salida.Close();
                                            Lector_Salida.Dispose();
                                            Lector_Salida = null;
                                            Total_Recursos++; // Resource saved without errors.
                                            Índice_Recurso_Global++; // Increase the counter of saved resources.
                                            this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [Resources extracted: " + Program.Traducir_Número(Índice_Recurso_Global + 1) + "]" });
                                            Ruta = null; // Free the rest of variables.
                                        }
                                        Matriz_Bytes_Salida = null;
                                        Lector_Memoria_Salida.Close();
                                        Lector_Memoria_Salida.Dispose();
                                        Lector_Memoria_Salida = null;
                                        Imagen.Dispose();
                                        Imagen = null;
                                        Imagen_Original.Dispose();
                                        Imagen_Original = null;
                                    }
                                    Lector_Memoria.Close();
                                    Lector_Memoria.Dispose();
                                    Lector_Memoria = null;
                                }
                                Matriz_Bytes_Temporal = null;
                            }
                            catch { continue; }
                            //catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                        }
                    }
                    else
                    {
                        // Try to export all the images inside, but it's the slower mode.
                        int Longitud = Matriz_Bytes.Length; // Quicker access to the length?
                        this.Invoke(new Invocación.Delegado_ProgressBar_Maximum(Invocación.Ejecutar_Delegado_ProgressBar_Maximum), new object[] { Barra_Progreso_Archivo, Longitud });
                        Dictionary<uint, object> Diccionario_CRC_32 = new Dictionary<uint, object>();
                        for (int Índice_Byte = 0; Índice_Byte < Longitud; Índice_Byte++)
                        {
                            try
                            {
                                if (Pendiente_Subproceso_Abortar) return 0; // Cancel safely before time.
                                this.Invoke(new Invocación.Delegado_ProgressBar_Value(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso_Archivo, Índice_Byte });
                                byte[] Matriz_Bytes_Temporal = new byte[Longitud - Índice_Byte];
                                Array.Copy(Matriz_Bytes, Índice_Byte, Matriz_Bytes_Temporal, 0, Matriz_Bytes_Temporal.Length);
                                MemoryStream Lector_Memoria = new MemoryStream(Matriz_Bytes_Temporal);
                                if (Lector_Memoria != null)
                                {
                                    Image Imagen_Original = null;
                                    try
                                    {
                                        MagickReadSettings MRS = new MagickReadSettings();
                                        MRS.Format = MagickFormat.Tga;
                                        MagickImage Imagen_Magick = new MagickImage(Matriz_Bytes_Temporal, MRS);
                                        Imagen_Original = Imagen_Magick.ToBitmap();
                                        //Imagen_Original = Imagen_Magick.ToBitmap(ImageFormat.Png);
                                    }
                                    catch { Imagen_Original = null; }
                                    if (Imagen_Original == null)
                                    {
                                        try { Imagen_Original = Image.FromStream(Lector_Memoria, false, false); }
                                        catch { Imagen_Original = null; }
                                    }
                                    if (Imagen_Original != null)
                                    {
                                        int Ancho = Imagen_Original.Width;
                                        int Alto = Imagen_Original.Height;
                                        Bitmap Imagen = new Bitmap(Ancho, Alto, /*!Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? PixelFormat.Format24bppRgb : */PixelFormat.Format32bppArgb);
                                        Graphics Pintar = Graphics.FromImage(Imagen);
                                        Pintar.CompositingMode = CompositingMode.SourceCopy;
                                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                        Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                        Pintar.SmoothingMode = SmoothingMode.HighQuality;
                                        Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                                        Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                        Pintar.Dispose();
                                        Pintar = null; // This copy should remove unused bytes from the resource.
                                        if (Ruta_Salida.ToLowerInvariant().EndsWith("media")) // Some images have R and B inverted.
                                        {
                                            BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                                            byte[] Matriz_Bytes_ARGB = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                                            Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes_ARGB, 0, Matriz_Bytes_ARGB.Length);
                                            int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                                            int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                                            byte Valor = 0;
                                            for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                                            {
                                                for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento) // Swap Red and Blue.
                                                {
                                                    Valor = Matriz_Bytes_ARGB[Índice];
                                                    Matriz_Bytes_ARGB[Índice] = Matriz_Bytes_ARGB[Índice + 2];
                                                    Matriz_Bytes_ARGB[Índice + 2] = Valor;
                                                }
                                            }
                                            Marshal.Copy(Matriz_Bytes_ARGB, 0, Bitmap_Data.Scan0, Matriz_Bytes_ARGB.Length);
                                            Imagen.UnlockBits(Bitmap_Data);
                                            Bitmap_Data = null;
                                            Matriz_Bytes_ARGB = null;
                                        }
                                        Program.Crear_Carpetas(Ruta_Salida);
                                        MemoryStream Lector_Memoria_Salida = new MemoryStream();
                                        Imagen.Save(Lector_Memoria_Salida, ImageFormat.Png); // Save the resource.
                                        byte[] Matriz_Bytes_Salida = Lector_Memoria_Salida.ToArray();
                                        uint CRC_32 = Program.Calcular_CRC32(Matriz_Bytes_Salida);
                                        if (!Diccionario_CRC_32.ContainsKey(CRC_32))
                                        {
                                            Diccionario_CRC_32.Add(CRC_32, null);
                                            string Ruta = Total_Recursos.ToString(); // Use a numeric file name.
                                            while (Ruta.Length < 8) Ruta = '0' + Ruta; // Set file name length to 8.
                                            Ruta = Ruta_Salida + "\\" + Ruta; // Add the full path.
                                            while (File.Exists(Ruta + ".png")) Ruta += '_'; // Avoid overwriting.
                                            Ruta += ".png"; // Add the extension.
                                            FileStream Lector_Salida = new FileStream(Ruta, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
                                            Lector_Salida.SetLength(0L);
                                            Lector_Salida.Seek(0L, SeekOrigin.Begin);
                                            Lector_Salida.Write(Matriz_Bytes_Salida, 0, Matriz_Bytes_Salida.Length);
                                            Lector_Salida.Close();
                                            Lector_Salida.Dispose();
                                            Lector_Salida = null;
                                            Total_Recursos++; // Resource saved without errors.
                                            Índice_Recurso_Global++; // Increase the counter of saved resources.
                                            this.Invoke(new Invocación.Delegado_Control_Text(Invocación.Ejecutar_Delegado_Control_Text), new object[] { this, Texto_Título + " - [Resources extracted: " + Program.Traducir_Número(Índice_Recurso_Global + 1) + "]" });
                                            Ruta = null; // Free the rest of variables.
                                        }
                                        Matriz_Bytes_Salida = null;
                                        Lector_Memoria_Salida.Close();
                                        Lector_Memoria_Salida.Dispose();
                                        Lector_Memoria_Salida = null;
                                        Imagen.Dispose();
                                        Imagen = null;
                                        Imagen_Original.Dispose();
                                        Imagen_Original = null;
                                    }
                                    Lector_Memoria.Close();
                                    Lector_Memoria.Dispose();
                                    Lector_Memoria = null;
                                }
                                Matriz_Bytes_Temporal = null;
                            }
                            catch { continue; }
                            //catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                        }
                    }
                }
                return Total_Recursos;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return 0;
        }

        /// <summary>
        /// Thread function that extracts all the available Minecraft Xbox 360 Edition resources.
        /// </summary>
        internal void Subproceso_DoWork()
        {
            Subproceso_Abortado = false; // Used to know if the window must be closed.
            try
            {
                Subproceso_Activo = true;
                this.Invoke(new Invocación.Delegado_Control_Cursor(Invocación.Ejecutar_Delegado_Control_Cursor), new object[] { this, Cursors.WaitCursor });
                string Ruta_Entrada = TextBox_Ruta.Text;
                if (!string.IsNullOrEmpty(Ruta_Entrada) && Directory.Exists(Ruta_Entrada))
                {
                    string[] Matriz_Rutas = Directory.GetFiles(Ruta_Entrada, "*", SearchOption.AllDirectories);
                    if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                    {
                        if (Matriz_Rutas.Length > 1) Array.Sort(Matriz_Rutas);
                        this.Invoke(new Invocación.Delegado_ProgressBar_Value(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso, 0 });
                        this.Invoke(new Invocación.Delegado_ProgressBar_Maximum(Invocación.Ejecutar_Delegado_ProgressBar_Maximum), new object[] { Barra_Progreso, Matriz_Rutas.Length * 2 });
                        Program.Crear_Carpetas(Ruta_PC);
                        int Índice_Ruta = 0;
                        int Índice_Recurso = 0;
                        foreach (string Ruta in Matriz_Rutas)
                        {
                            try
                            {
                                if (Pendiente_Subproceso_Abortar) return; // Cancel safely before time.
                                Índice_Ruta++;
                                this.Invoke(new Invocación.Delegado_ProgressBar_Value(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso, Índice_Ruta });
                                Program.Crear_Carpetas(Path.GetDirectoryName(Ruta_PC + Ruta.Substring(Ruta_Entrada.Length)) + "\\" + Path.GetFileNameWithoutExtension(Ruta));
                                SortedDictionary<long, string> Diccionario_Índices_Rutas = Xbox_360.Obtener_Nombres_Packs_Recursos(Ruta, Path.GetDirectoryName(Ruta_PC + Ruta.Substring(Ruta_Entrada.Length)) + "\\" + Path.GetFileNameWithoutExtension(Ruta) + "\\_" + Path.GetFileNameWithoutExtension(Ruta) + "_.txt", false);
                                //Extraer_Recursos_Recursivos(Ruta, Path.GetDirectoryName(Ruta_PC + Ruta.Substring(Ruta_Entrada.Length))/* + "\\" + Path.GetFileNameWithoutExtension(Ruta)*/, ref Índice_Recurso);
                                Extraer_Recursos_Recursivos(Ruta, Path.GetDirectoryName(Ruta_PC + Ruta.Substring(Ruta_Entrada.Length)) + "\\" + Path.GetFileNameWithoutExtension(Ruta), ref Índice_Recurso, Diccionario_Índices_Rutas);
                            }
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                        }
                        SystemSounds.Asterisk.Play(); // Done.
                        Matriz_Rutas = null;
                    }
                }
            }
            catch (ThreadAbortException) { Subproceso_Abortado = true; } // Aborted, ignore this exception.
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally
            {
                try
                {
                    Subproceso_Activo = false;
                    Subproceso = null;
                    GC.Collect(); // Recover RAM memory at the end.
                    GC.GetTotalMemory(true);
                    if (!Subproceso_Abortado && !Pendiente_Subproceso_Abortar)
                    {
                        Pendiente_Subproceso_Abortar = false;
                        this.Invoke(new Invocación.Delegado_ProgressBar_Value(Invocación.Ejecutar_Delegado_ProgressBar_Value), new object[] { Barra_Progreso, 0 });
                        this.Invoke(new Invocación.Delegado_ProgressBar_Maximum(Invocación.Ejecutar_Delegado_ProgressBar_Maximum), new object[] { Barra_Progreso, 100 });
                        this.Invoke(new Invocación.Delegado_Control_Cursor(Invocación.Ejecutar_Delegado_Control_Cursor), new object[] { this, Cursors.Default });
                        this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { TextBox_Ruta, true });
                        this.Invoke(new Invocación.Delegado_Control_Enabled(Invocación.Ejecutar_Delegado_Control_Enabled), new object[] { Botón_Convertir, true });
                        this.Invoke(new Invocación.Delegado_Control_Select(Invocación.Ejecutar_Delegado_Control_Select), new object[] { Botón_Convertir });
                        this.Invoke(new Invocación.Delegado_Control_Focus(Invocación.Ejecutar_Delegado_Control_Focus), new object[] { Botón_Convertir });
                    }
                    else
                    {
                        Temporizador_Principal.Stop();
                        this.Invoke(new Invocación.Delegado_Form_Close(Invocación.Ejecutar_Delegado_Form_Close), new object[] { this }); // Close the window.
                    }
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            }
        }

        private void Pictures_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                PictureBox Picture = sender as PictureBox;
                if (Picture != null)
                {
                    if (Picture == Picture_Adventure_Time) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Adventure_Time_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Candy) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Candy_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Cartoon) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Cartoon_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Chinese_Mythology) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Chinese_Mythology_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_City) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_City_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Egyptian_Mythology) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Egyptian_Mythology_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Fallout) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Fallout_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Fantasy) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Fantasy_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Festive) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Festive_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Greek_Mythology) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Greek_Mythology_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Halloween) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Halloween_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Halloween_2015) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Halloween_2015_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Halo) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Halo_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Mass_Effect) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Mass_Effect_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Natural) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Natural_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Norse_Mythology) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Norse_Mythology_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Pattern) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Pattern_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Pirates_Of_The_Caribbean) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Pirates_Of_The_Caribbean_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Plastic) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Plastic_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Skyrim) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Skyrim_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Steampunk) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Steampunk_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Super_Cute_Texture_Pack) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_Super_Cute_Texture_Pack_Preview, 580, 288, true, false, CheckState.Checked);
                    else if (Picture == Picture_Xbox_360_The_Nightmare_Before_Christmas) Picture_Vista_Previa.Image = Program.Obtener_Imagen_Miniatura(Resources.Xbox_360_The_Nightmare_Before_Christmas_Preview, 580, 288, true, false, CheckState.Checked);
                    else Picture_Vista_Previa.Image = null;
                }
                else Picture_Vista_Previa.Image = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Pictures_MouseLeave(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
