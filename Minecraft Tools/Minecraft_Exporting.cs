using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    /// <summary>
    /// This class is designed to export to the desktop directory a zip file containing the whole source code for the selected Minecraft version. This file should never be distributed and only used to learn how certain parts of Minecraft worked back on it's older versions.
    /// </summary>
    internal static class Minecraft_Exporting
    {
        internal enum Minecraft_Versions : int
        {
            /// <summary>
            /// Minecraft 1.4.7 (MCP).
            /// </summary>
            Minecraft_1_4_7 = 0,
            /// <summary>
            /// Minecraft 1.5.2 (MCP).
            /// </summary>
            Minecraft_1_5_2,
            /// <summary>
            /// Minecraft 1.7.10 (JD-GUI).
            /// </summary>
            Minecraft_1_7_10,
            /// <summary>
            /// Minecraft 1.10.2 (JD-GUI).
            /// </summary>
            Minecraft_1_10_2,
            /// <summary>
            /// Minecraft 18w15a (JD-GUI).
            /// </summary>
            Minecraft_18w15a,
            /// <summary>
            /// Minecraft 18w15a (JAD).
            /// </summary>
            Minecraft_18w15a_JAD
        }

        internal static void Exportar_Código_Fuente_Minecraft(Minecraft_Versions Versión)
        {
            /*if (Versión == Minecraft_Versions.Minecraft_1_4_7)
            {
                string Ruta = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Minecraft_1_4_7_Source_Code.zip";
                if (Minecraft_1_4_7_Zip.Matriz_Bytes != null && Minecraft_1_4_7_Zip.Matriz_Bytes.Length > 0 && !string.IsNullOrEmpty(Ruta) && !File.Exists(Ruta))
                {
                    if (MessageBox.Show("Do you want to save to desktop a file with the Minecraft 1.4.7 source?\r\nIf you do, don't distribute that file, and use it only for learning purposes.", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        File.WriteAllBytes(Ruta, Minecraft_1_4_7_Zip.Matriz_Bytes);
                        MessageBox.Show("The Minecraft 1.4.7 source has been saved in your desktop in a zip file.\r\nTo see the code use applications like the Eclipse IDE or Notepad++.\r\nRemember to don't distribute that file, you've been warned, thank you.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else SystemSounds.Beep.Play();
            }
            else if (Versión == Minecraft_Versions.Minecraft_1_5_2)
            {
                string Ruta = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Minecraft_1_5_2_Source_Code.zip";
                if (Secret_Setting_Minecraft_1_5_2_Zip.Matriz_Bytes != null && Secret_Setting_Minecraft_1_5_2_Zip.Matriz_Bytes.Length > 0 && !string.IsNullOrEmpty(Ruta) && !File.Exists(Ruta))
                {
                    if (MessageBox.Show("Do you want to save to desktop a file with the Minecraft 1.5.2 source?\r\nIf you do, don't distribute that file, and use it only for learning purposes.", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        File.WriteAllBytes(Ruta, Secret_Setting_Minecraft_1_5_2_Zip.Matriz_Bytes);
                        MessageBox.Show("The Minecraft 1.5.2 source has been saved in your desktop in a zip file.\r\nTo see the code use applications like the Eclipse IDE or Notepad++.\r\nRemember to don't distribute that file, you've been warned, thank you.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else SystemSounds.Beep.Play();
            }
            else if (Versión == Minecraft_Versions.Minecraft_1_7_10)
            {
                string Ruta = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Minecraft_1_7_10_Source_Code.zip";
                if (Minecraft_1_7_10_Zip.Matriz_Bytes != null && Minecraft_1_7_10_Zip.Matriz_Bytes.Length > 0 && !string.IsNullOrEmpty(Ruta) && !File.Exists(Ruta))
                {
                    if (MessageBox.Show("Do you want to save to desktop a file with the Minecraft 1.7.10 source?\r\nIf you do, don't distribute that file, and use it only for learning purposes.", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        File.WriteAllBytes(Ruta, Minecraft_1_7_10_Zip.Matriz_Bytes);
                        MessageBox.Show("The Minecraft 1.7.10 source has been saved in your desktop in a zip file.\r\nTo see the code use applications like the Eclipse IDE or Notepad++.\r\nRemember to don't distribute that file, you've been warned, thank you.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else SystemSounds.Beep.Play();
            }
            else if (Versión == Minecraft_Versions.Minecraft_1_10_2)
            {
                string Ruta = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Minecraft_1_10_2_Source_Code.zip";
                if (Minecraft_1_10_2_Zip.Matriz_Bytes != null && Minecraft_1_10_2_Zip.Matriz_Bytes.Length > 0 && !string.IsNullOrEmpty(Ruta) && !File.Exists(Ruta))
                {
                    if (MessageBox.Show("Do you want to save to desktop a file with the Minecraft 1.10.2 source?\r\nIf you do, don't distribute that file, and use it only for learning purposes.", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        File.WriteAllBytes(Ruta, Minecraft_1_10_2_Zip.Matriz_Bytes);
                        MessageBox.Show("The Minecraft 1.10.2 source has been saved in your desktop in a zip file.\r\nTo see the code use applications like the Eclipse IDE or Notepad++.\r\nRemember to don't distribute that file, you've been warned, thank you.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else SystemSounds.Beep.Play();
            }
            else if (Versión == Minecraft_Versions.Minecraft_18w15a)
            {
                string Ruta = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Minecraft_18w15a_Source_Code.zip";
                if (Secret_Setting_Minecraft_18w15a_Zip.Matriz_Bytes != null && Secret_Setting_Minecraft_18w15a_Zip.Matriz_Bytes.Length > 0 && !string.IsNullOrEmpty(Ruta) && !File.Exists(Ruta))
                {
                    if (MessageBox.Show("Do you want to save to desktop a file with the Minecraft 18w15a source?\r\nIf you do, don't distribute that file, and use it only for learning purposes.", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        File.WriteAllBytes(Ruta, Secret_Setting_Minecraft_18w15a_Zip.Matriz_Bytes);
                        MessageBox.Show("The Minecraft 18w15a source has been saved in your desktop in a zip file.\r\nTo see the code use applications like the Eclipse IDE or Notepad++.\r\nRemember to don't distribute that file, you've been warned, thank you.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else SystemSounds.Beep.Play();
            }
            else if (Versión == Minecraft_Versions.Minecraft_18w15a_JAD)
            {
                string Ruta = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Minecraft_18w15a_JAD_Source_Code.zip";
                if (Minecraft_18w15a_JAD_Zip.Matriz_Bytes != null && Minecraft_18w15a_JAD_Zip.Matriz_Bytes.Length > 0 && !string.IsNullOrEmpty(Ruta) && !File.Exists(Ruta))
                {
                    if (MessageBox.Show("Do you want to save to desktop a file with the Minecraft 18w15a source?\r\nIf you do, don't distribute that file, and use it only for learning purposes.", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        File.WriteAllBytes(Ruta, Minecraft_18w15a_JAD_Zip.Matriz_Bytes);
                        MessageBox.Show("The Minecraft 18w15a source has been saved in your desktop in a zip file.\r\nTo see the code use applications like the Eclipse IDE or Notepad++.\r\nRemember to don't distribute that file, you've been warned, thank you.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else SystemSounds.Beep.Play();
            }*/
        }
    }
}
