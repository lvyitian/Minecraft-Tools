using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Tools
{
    internal static class PInvoke
    {
        internal static class Gdi32
        {
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr Handle_Objeto);
        }

        internal static class Shell32
        {
            /// <summary>
            /// this enum used on conjunction with getobjectproperties
            /// </summary>
            public enum SHObjectPropertiesFlags
            {
                PrinterName = 1, // lpObject points to a printer friendly name
                FilePath = 2, // lpObject points to a fully qualified path + file name
                VolumeGuid = 4 // lpObject points to a Volume GUID
            }

            /// <summary>
            /// This function invokes the Properties context menu command on a Shell object.
            /// Ejemplo: "CallPropDialog(this.Handle, GetProperties.SHOP_FILEPATH, Ruta, null)";
            /// </summary>
            /// <param name="hwnd">[in] The HWND of the window that will be the parent of the dialog box.</param>
            /// <param name="dwType">enum to what to call</param>
            /// <param name="szObject">[in] A NULL-terminated Unicode string that contains the object name.
            /// The contents of the string are determinated by which of
            /// the first three flags are set in dwType.</param>
            /// <param name="szPage">[in] A NULL-terminated Unicode string that contains the name of
            /// the property sheet page to be initally opened.
            /// Set this parameter to NULL to specifiy the default page.
            /// General, Summary, Security, etc. Pero en Español: "Resumen" (09-01-2010)...</param>
            /// <returns>Returns TRUE if the Properties command is successfully invoked, or FALSE otherwise.</returns>
            [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern bool SHObjectProperties(IntPtr Handle_Ventana, SHObjectPropertiesFlags Flags, string Ruta, string Página);
        }

        internal static class User32
        {
            [Flags]
            internal enum MouseEventF : uint
            {
                Move = 1,
                LeftDown = 2,
                LeftUp = 4,
                RightDown = 8,
                RightUp = 16,
                MiddleDown = 32,
                MiddleUp = 64,
                XDown = 128,
                XUp = 256,
                Wheel = 2048,
                //Scroll = 2048, // dwData = -120 ó +120
                VirtualDesk = 16384,
                Absolute = 32768
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            internal struct CursorInfo
            {
                internal int cbSize;
                internal uint Flags;
                internal IntPtr Handle_Cursor;
                internal Point Posición;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            internal struct IconInfo
            {
                internal bool fIcon;
                internal uint Hotspot_X;
                internal uint Hotspot_Y;
                internal IntPtr Handle_Máscara;
                internal IntPtr Handle_Imagen;
            }

            /*[StructLayout(LayoutKind.Sequential)]
            public struct ICONINFO
            {
                /// <summary>
                /// Specifies whether this structure defines an icon or a cursor. A value of TRUE specifies...
                /// </summary>
                public bool fIcon;
                /// <summary>
                /// Specifies the x-coordinate of a cursor's hot spot. If this structure defines an icon, the hot...
                /// </summary>
                public Int32 xHotspot;
                /// <summary>
                /// Specifies the y-coordinate of the cursor's hot spot. If this structure defines an icon, the hot...
                /// </summary>
                public Int32 yHotspot;
                /// <summary>
                /// (HBITMAP) Specifies the icon bitmask bitmap. If this structure defines a black and white icon...
                /// </summary>
                public IntPtr hbmMask;
                /// <summary>
                /// (HBITMAP) Handle to the icon color bitmap. This member can be optional if this...
                /// </summary>
                public IntPtr hbmColor;
            }*/

            [DllImport("User32.dll")]
            public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

            //[DllImport("user32.dll")]
            //public static extern IntPtr CreateIconIndirect(ref ICONINFO icon);

            [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern bool DestroyCursor(IntPtr Handle_Cursor);

            [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern bool DestroyIcon(IntPtr Handle_Icono);

            [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern bool GetCursorInfo(ref CursorInfo Info);

            [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern bool GetIconInfo(IntPtr Handle_Icono, ref IconInfo Info);

            //[DllImport("user32.dll", EntryPoint = "GetIconInfo")]
            //public static extern bool GetIconInfo(IntPtr hIcon, ref ICONINFO piconinfo);

            [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern void mouse_event(MouseEventF Flags, int x, int y, int Data, int ExtraInfo);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern bool SetCursorPos(int X, int Y);
        }
    }
}
