using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Tools
{
    internal static class Minecraft_Source
    {
        /// <summary>
        /// Enumeration with the 16 original Minecraft 1.12 dye colors, use "Color.FromArgb((int)Colores.DesiredColor)" to get the actual ARGB color.
        /// </summary>
        internal enum Tintes : int
        {
            Black = 1908001,
            Blue = 3949738,
            Brown = 8606770,
            Cyan = 1481884,
            Gray = 4673362,
            Green = 6192150,
            Light_Blue = 3847130,
            /// <summary>
            /// Also called "Silver" in the older Minecraft versions.
            /// </summary>
            Light_Gray = 10329495,
            Lime = 8439583,
            Magenta = 13061821,
            Orange = 16351261,
            Pink = 15961002,
            Purple = 8991416,
            Red = 11546150,
            White = 16383998,
            Yellow = 16701501,
        }

        /// <summary>
        /// Default shulker box texture color. I couldn't find this, so since I had the purple color,
        /// I picked the more repeated color in the purple texture, and got the difference with the
        /// original purple dye color, then got the more repeated color in the default shulker box
        /// texture, and added the previous difference, so more or less I got the default color. It's
        /// not perfect, but it should be very close to the original default color, at least in theory.
        /// </summary>
        internal static Color Color_Shulker = Color.FromArgb(255, 155, 110, 155);
        //internal static Color Color_Shulker = Color.FromArgb(255, 172, 117, 166);

        internal static readonly string[] Matriz_Nombres_Tintes = new string[16]
        {
            "Black",
            "Blue",
            "Brown",
            "Cyan",
            "Gray",
            "Green",
            "Light_Blue",
            "Light_Gray",
            "Lime",
            "Magenta",
            "Orange",
            "Pink",
            "Purple",
            "Red",
            "White",
            "Yellow"
        };

        internal static readonly Color[] Matriz_Colores_Tintes = new Color[16]
        {
            Color.FromArgb((int)Tintes.Black),
            Color.FromArgb((int)Tintes.Blue),
            Color.FromArgb((int)Tintes.Brown),
            Color.FromArgb((int)Tintes.Cyan),
            Color.FromArgb((int)Tintes.Gray),
            Color.FromArgb((int)Tintes.Green),
            Color.FromArgb((int)Tintes.Light_Blue),
            Color.FromArgb((int)Tintes.Light_Gray),
            Color.FromArgb((int)Tintes.Lime),
            Color.FromArgb((int)Tintes.Magenta),
            Color.FromArgb((int)Tintes.Orange),
            Color.FromArgb((int)Tintes.Pink),
            Color.FromArgb((int)Tintes.Purple),
            Color.FromArgb((int)Tintes.Red),
            Color.FromArgb((int)Tintes.White),
            Color.FromArgb((int)Tintes.Yellow)
        };

        /// <summary>
        /// Function designed to convert any of the 16 dye colors to an ARGB color.
        /// </summary>
        /// <param name="Tinte">Any of the 16 valid dye colors.</param>
        /// <returns>Returns one of the 16 ARGB dye colors. Returns "Color.Empty" on any error.</returns>
        internal static Color Obtener_Color_Tinte(Tintes Tinte)
        {
            try
            {
                return Color.FromArgb((int)Tinte);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return Color.Empty;
        }
    }
}
