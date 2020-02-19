using Minecraft_Tools.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Gracias : Form
    {
        public Ventana_Gracias()
        {
            InitializeComponent();
        }

        internal class Comparador_Agradecimientos : IComparer<Agradecimientos>
        {
            public int Compare(Agradecimientos X, Agradecimientos Y)
            {
                if (string.Compare(X.Nombre, Program.Texto_Usuario, false) == 0)
                {
                    return -1;
                }
                else if (string.Compare(Y.Nombre, Program.Texto_Usuario, false) == 0)
                {
                    return 1;
                }
                else return string.Compare(X.Nombre, Y.Nombre);
            }
        }

        /// <summary>
        /// Structure that holds up all the information about a thanked person or organization.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Agradecimientos
        {
            internal string Nombre;
            internal string Recurso;
            internal string URL;
            internal DateTime Fecha;
            internal string Origen;
            internal string URL_Origen; // Vínculos a mis álbumes en el caso de música electrónica y a los foros en el resto.

            internal Agradecimientos(string Nombre, string URL, DateTime Fecha, string Origen, string URL_Origen)
            {
                this.Nombre = Nombre;
                this.Recurso = Nombre.Replace(' ', '_').Replace('~', '_').Replace('=', '_').Replace('+', '_').Replace('-', '_').Replace('\\', '_').Replace('/', '_').Replace(':', '_').Replace('*', '_').Replace('?', '_').Replace('\"', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_').Replace('.', '_');
                this.URL = URL;
                this.Fecha = Fecha;
                this.Origen = Origen;
                this.URL_Origen = URL_Origen;
            }

            internal static List<Agradecimientos> Lista_Agradecimientos = new List<Agradecimientos>(new Agradecimientos[]
            {
                new Agradecimientos
                (
                    "Alexander",
                    "",
                    new DateTime(2018, 10, 3),
                    "Minecraft Tools",
                    "https://www.minecraftforum.net/forums/minecraft-java-edition/recent-updates-and-snapshots/2894808-minecraft-1-13-new-chunk-format-fully-decoded-read"
                ),
                new Agradecimientos
                (
                    "BigBadLoser",
                    "https://www.minecraftforum.net/members/BigBadLoser",
                    new DateTime(2018, 3, 17),
                    "Minecraft Forum",
                    "https://www.minecraftforum.net/forums/minecraft-java-edition/recent-updates-and-snapshots/2894808-minecraft-1-13-new-chunk-format-fully-decoded-read?comment=2"
                ),
                new Agradecimientos
                (
                    "Brad Breeck",
                    "http://www.bradbreeck.com",
                    new DateTime(2017, 4, 23),
                    "Electronic music",
                    "http://jupisoft.x10host.com/html/resurrection.html"
                ),
                new Agradecimientos
                (
                    "bugmancx",
                    "https://www.minecraftforum.net/members/bugmancx",
                    new DateTime(2018, 6, 25),
                    "Minecraft Forum",
                    "https://www.minecraftforum.net/forums/minecraft-java-edition/recent-updates-and-snapshots/2894808-minecraft-1-13-new-chunk-format-fully-decoded-read?comment=22"
                ),
                new Agradecimientos
                (
                    "C418",
                    "http://www.bandcamp.com/c418",
                    new DateTime(1, 1, 1),
                    "Electronic music",
                    "http://jupisoft.x10host.com/html/minecraft_10_subwoofer_lullaby.html"
                ),
                new Agradecimientos
                (
                    "Corinna John",
                    "https://www.codeproject.com/Members/Corinna-John",
                    new DateTime(2016, 11, 16),
                    "Code Project",
                    "https://www.codeproject.com/Articles/5390/Steganography-V-Hiding-Messages-in-MIDI-Songs"
                ),
                new Agradecimientos
                (
                    "David Maeso",
                    "http://www.davidmaeso.com",
                    new DateTime(2016, 10, 1),
                    "Electronic music",
                    "http://jupisoft.x10host.com/html/david_maeso_01_lullaby_for_an_artist.html"
                ),
                new Agradecimientos
                (
                    "DenBob",
                    "https://www.minecraftforum.net/members/DenBob",
                    new DateTime(2018, 4, 27),
                    "Minecraft Forum",
                    "https://www.minecraftforum.net/forums/minecraft-java-edition/recent-updates-and-snapshots/2894808-minecraft-1-13-new-chunk-format-fully-decoded-read?comment=13"
                ),
                new Agradecimientos
                (
                    "Fratelli Stellari",
                    "http://www.messaggidallestelle.altervista.org",
                    new DateTime(2017, 12, 30),
                    "Electronic music",
                    "http://jupisoft.x10host.com/html/remixes_01_cosmic_messages_instrumental.html"
                ),
                new Agradecimientos
                (
                    "iaraUM",
                    "https://github.com/iaraUM",
                    new DateTime(2019, 7, 7),
                    "GitHub",
                    "https://github.com/Jupisoft111/Minecraft-Tools/issues/1"
                ),
                new Agradecimientos
                (
                    "ISpectre23",
                    "https://www.youtube.com/channel/UCuvzTPOhPtXK_knOx73E34w",
                    new DateTime(2002, 3, 30),
                    "Electronic music",
                    "http://jupisoft.x10host.com/html/thank_you_03_ispectre23.html"
                ),
                new Agradecimientos
                (
                    "kpqvz2",
                    "https://www.minecraftforum.net/members/kpqvz2",
                    new DateTime(2019, 6, 15),
                    "Minecraft Forum",
                    "https://www.minecraftforum.net/members/kpqvz2"
                ),
                new Agradecimientos
                (
                    "Mattel",
                    "http://play.mattel.com",
                    new DateTime(2017, 11, 21),
                    "Monster High",
                    "http://jupisoft.x10host.com/html/monster_high.html"
                ),
                new Agradecimientos
                (
                    "Minecraft",
                    "https://www.minecraft.net",
                    new DateTime(1, 1, 1),
                    "ISpectre23",
                    "http://jupisoft.x10host.com/html/minecraft.html"
                ),
                new Agradecimientos
                (
                    "Minecraft Forum",
                    "https://www.minecraftforum.net",
                    new DateTime(2018, 3, 17),
                    "Minecraft",
                    "https://www.minecraftforum.net/forums/minecraft-java-edition/recent-updates-and-snapshots/2894808-minecraft-1-13-new-chunk-format-fully-decoded-read"
                ),
                new Agradecimientos
                (
                    "minepowaa",
                    "https://www.minecraftforum.net",
                    new DateTime(2018, 5, 1),
                    "Minecraft",
                    "https://www.minecraftforum.net/members/minepowaa"
                ),
                new Agradecimientos
                (
                    "Mojang",
                    "https://mojang.com",
                    new DateTime(1, 1, 1),
                    "Minecraft",
                    "http://jupisoft.x10host.com/html/minecraft.html"
                ),
                new Agradecimientos
                (
                    "Monster High",
                    "http://play.monsterhigh.com/en-us/index.html",
                    new DateTime(2017, 11, 21),
                    "Electronic music",
                    "http://jupisoft.x10host.com/html/monster_high.html"
                ),
                new Agradecimientos
                (
                    "MrLesk",
                    "https://www.minecraftforum.net/members/MrLesk",
                    new DateTime(2018, 4, 12),
                    "Minecraft Forum",
                    "https://www.minecraftforum.net/forums/minecraft-java-edition/recent-updates-and-snapshots/2894808-minecraft-1-13-new-chunk-format-fully-decoded-read?comment=7"
                ),
                new Agradecimientos
                (
                    "MSpaceDev",
                    "https://www.minecraftforum.net/members/MSpaceDev",
                    new DateTime(2018, 3, 17),
                    "Minecraft Forum",
                    "https://www.minecraftforum.net/forums/minecraft-java-edition/recent-updates-and-snapshots/2894808-minecraft-1-13-new-chunk-format-fully-decoded-read?comment=3"
                ),
                new Agradecimientos
                (
                    "oPryzeLP",
                    "https://www.minecraftforum.net/members/oPryzeLP",
                    new DateTime(2018, 8, 18),
                    "Minecraft Forum",
                    "https://www.minecraftforum.net/forums/minecraft-java-edition/recent-updates-and-snapshots/2894808-minecraft-1-13-new-chunk-format-fully-decoded-read?comment=27"
                ),
                new Agradecimientos
                (
                    "PM_ME_YOUR_HOOMAN",
                    "https://www.reddit.com/user/PM_ME_YOUR_HOOMAN",
                    new DateTime(2018, 3, 17),
                    "Reddit",
                    "https://www.reddit.com/r/Minecraft/comments/852eoc/minecraft_113_chunk_format_fully_decoded/"
                ),
                new Agradecimientos
                (
                    "ScotsMiser",
                    "https://www.minecraftforum.net/members/ScotsMiser",
                    new DateTime(2018, 4, 19),
                    "Minecraft Forum",
                    "https://www.minecraftforum.net/forums/minecraft-java-edition/recent-updates-and-snapshots/2894808-minecraft-1-13-new-chunk-format-fully-decoded-read?comment=11"
                ),
                new Agradecimientos
                (
                    "SharpSeeEr",
                    "https://www.minecraftforum.net/members/SharpSeeEr",
                    new DateTime(2018, 7, 31),
                    "Minecraft Forum",
                    "https://www.minecraftforum.net/forums/minecraft-java-edition/recent-updates-and-snapshots/2894808-minecraft-1-13-new-chunk-format-fully-decoded-read?comment=26"
                ),
                new Agradecimientos
                (
                    "swagmiter",
                    "https://www.minecraftforum.net/members/swagmiter",
                    new DateTime(2018, 4, 17),
                    "Minecraft Forum",
                    "https://www.minecraftforum.net/forums/minecraft-java-edition/recent-updates-and-snapshots/2894808-minecraft-1-13-new-chunk-format-fully-decoded-read?comment=9"
                ),
                new Agradecimientos
                (
                    "The FLipside Forum",
                    "http://www.theflipsideforum.com/index.php",
                    new DateTime(1, 1, 1),
                    "Electronic music",
                    "http://jupisoft.x10host.com/html/resurrection.html"
                ),
                new Agradecimientos
                (
                    "TheMasterCaver",
                    "https://www.minecraftforum.net/members/TheMasterCaver",
                    new DateTime(2018, 7, 26),
                    "Minecraft Forum",
                    "https://www.minecraftforum.net/forums/minecraft-java-edition/recent-updates-and-snapshots/2894808-minecraft-1-13-new-chunk-format-fully-decoded-read?comment=25"
                ),
                new Agradecimientos
                (
                    "Videogamer555",
                    "https://www.minecraftforum.net/members/Videogamer555",
                    new DateTime(2018, 7, 25),
                    "Minecraft Forum",
                    "https://www.minecraftforum.net/forums/minecraft-java-edition/recent-updates-and-snapshots/2894808-minecraft-1-13-new-chunk-format-fully-decoded-read?comment=23"
                ),
                new Agradecimientos
                (
                    "Xisumavoid",
                    "https://youtube.com/xisumavoid",
                    new DateTime(2016, 9, 10),
                    "Hermitcraft",
                    "http://jupisoft.x10host.com/html/thank_you_10_xisumavoid.html"
                ),
                /*new Agradecimientos
                (
                    "Hermitcraft",
                    "YouTube",
                    "",
                    ""
                ),*/
                /*new Agradecimientos
                (
                    "",
                    "",
                    "",
                    ""
                ),
                new Agradecimientos
                (
                    "",
                    "",
                    "",
                    ""
                ),*/
            });
        }

        internal readonly string Texto_Título = "Thank You by Jupisoft for " + Program.Texto_Usuario;
        internal bool Variable_Siempre_Visible = false;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;

        private void Ventana_Gracias_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                if (Agradecimientos.Lista_Agradecimientos != null && Agradecimientos.Lista_Agradecimientos.Count > 0)
                {
                    //Agradecimientos Agradecimiento = new Agradecimientos();
                    for (int Índice_Agradecimiento = 0; Índice_Agradecimiento < Agradecimientos.Lista_Agradecimientos.Count; Índice_Agradecimiento++)
                    {
                        Bitmap Imagen = Program.Obtener_Imagen_Recursos("Thanks_" + Agradecimientos.Lista_Agradecimientos[Índice_Agradecimiento].Recurso);
                        DataGridView_Principal.Rows.Add(new object[]
                        {
                            Imagen != null ? Imagen : Resources.Jupisoft_48,//Resources.Thanks_Default,
                            Agradecimientos.Lista_Agradecimientos[Índice_Agradecimiento].Nombre,
                            Agradecimientos.Lista_Agradecimientos[Índice_Agradecimiento].URL,
                            Agradecimientos.Lista_Agradecimientos[Índice_Agradecimiento].Fecha,
                            Agradecimientos.Lista_Agradecimientos[Índice_Agradecimiento].Origen,
                            Agradecimientos.Lista_Agradecimientos[Índice_Agradecimiento].URL_Origen
                        });
                        /*Bitmap Imagen = Minecraft.Obtener_Textura_Recursos("Thanks_" + Agradecimientos.Matriz_Agradecimientos[Índice_Agradecimiento].Recurso);
                        if (Imagen != null)
                        {
                            Rectangle Rectángulo = Program.Buscar_Zona_Recorte_Imagen(Imagen);
                            if (Rectángulo.X > -1 && Rectángulo.Y > -1 && Rectángulo.X < int.MaxValue && Rectángulo.Y < int.MaxValue && Rectángulo.Width > 0 && Rectángulo.Height > 0)
                            {
                                Imagen = Imagen.Clone(Rectángulo, PixelFormat.Format32bppArgb);
                            }
                            int Ancho = Imagen.Width;
                            int Alto = Imagen.Height;
                            if (Ancho != Alto)
                            {
                                int Máximo = Math.Max(Ancho, Alto);
                                int X = (Máximo - Ancho) / 2;
                                int Y = (Máximo - Alto) / 2;
                                Bitmap Imagen_Temporal = new Bitmap(Máximo, Máximo, PixelFormat.Format32bppArgb);
                                Graphics Pintar_Temporal = Graphics.FromImage(Imagen_Temporal);
                                Pintar_Temporal.CompositingMode = CompositingMode.SourceCopy;
                                Pintar_Temporal.CompositingQuality = CompositingQuality.HighQuality;
                                Pintar_Temporal.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                Pintar_Temporal.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                Pintar_Temporal.SmoothingMode = SmoothingMode.None;
                                Pintar_Temporal.DrawImage(Imagen, new Rectangle(X, Y, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                                Pintar_Temporal.Dispose();
                                Pintar_Temporal = null;
                                Imagen = Imagen_Temporal;
                                Ancho = Máximo;
                                Alto = Máximo;
                            }
                            List<string> Lista_Antialiasing = new List<string>(new string[] { "BigBadLoser", "Mojang", "MrLesk", "ScotsMiser", "Xisumavoid" });
                            bool Antialiasing = !Lista_Antialiasing.Contains(Agradecimientos.Matriz_Agradecimientos[Índice_Agradecimiento].Recurso);
                            Bitmap Imagen_Miniatura = null;

                            if (Ancho != 16 || Alto != 16) Imagen_Miniatura = Program.Obtener_Imagen_Miniatura((Bitmap)Imagen.Clone(), 16, 16, true, Antialiasing);
                            else Imagen_Miniatura = (Bitmap)Imagen.Clone();
                            Program.Guardar_Imagen_Temporal(Imagen_Miniatura, "16_Thanks_" + Agradecimientos.Matriz_Agradecimientos[Índice_Agradecimiento].Recurso);
                            Imagen_Miniatura.Dispose();
                            Imagen_Miniatura = null;

                            if (Ancho != 24 || Alto != 24) Imagen_Miniatura = Program.Obtener_Imagen_Miniatura((Bitmap)Imagen.Clone(), 24, 24, true, Antialiasing);
                            else Imagen_Miniatura = (Bitmap)Imagen.Clone();
                            Program.Guardar_Imagen_Temporal(Imagen_Miniatura, "24_Thanks_" + Agradecimientos.Matriz_Agradecimientos[Índice_Agradecimiento].Recurso);
                            Imagen_Miniatura.Dispose();
                            Imagen_Miniatura = null;

                            if (Ancho != 32 || Alto != 32) Imagen_Miniatura = Program.Obtener_Imagen_Miniatura((Bitmap)Imagen.Clone(), 32, 32, true, Antialiasing);
                            else Imagen_Miniatura = (Bitmap)Imagen.Clone();
                            Program.Guardar_Imagen_Temporal(Imagen_Miniatura, "32_Thanks_" + Agradecimientos.Matriz_Agradecimientos[Índice_Agradecimiento].Recurso);
                            Imagen_Miniatura.Dispose();
                            Imagen_Miniatura = null;

                            if (Ancho != 48 || Alto != 48) Imagen_Miniatura = Program.Obtener_Imagen_Miniatura((Bitmap)Imagen.Clone(), 48, 48, true, Antialiasing);
                            else Imagen_Miniatura = (Bitmap)Imagen.Clone();
                            Program.Guardar_Imagen_Temporal(Imagen_Miniatura, "Thanks_" + Agradecimientos.Matriz_Agradecimientos[Índice_Agradecimiento].Recurso);
                            Imagen_Miniatura.Dispose();
                            Imagen_Miniatura = null;

                            Imagen.Dispose();
                            Imagen = null;
                        }*/
                    }
                    DataGridView_Principal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    DataGridView_Principal.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    //DataGridView_Principal.Sort(Columna_Nombre, ListSortDirection.Ascending);
                    if (DataGridView_Principal.Rows.Count > 0)
                    {
                        this.Text = Texto_Título + " - [People and organizations thanked: " + Program.Traducir_Número(DataGridView_Principal.Rows.Count) + "]";
                        DataGridView_Principal.CurrentCell = DataGridView_Principal[0, 0];
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Gracias_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                //Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Gracias_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Gracias_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Gracias_KeyDown(object sender, KeyEventArgs e)
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

        private void DataGridView_Principal_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    if (e.ColumnIndex > -1 && e.ColumnIndex < DataGridView_Principal.Columns.Count && e.RowIndex > -1 && e.RowIndex < DataGridView_Principal.Rows.Count)
                    {
                        if (e.ColumnIndex <= Columna_URL.Index)
                        {
                            string URL = DataGridView_Principal[Columna_URL.Index, e.RowIndex].Value as string;
                            if (!string.IsNullOrEmpty(URL)) Program.Ejecutar_Ruta(URL, ProcessWindowStyle.Normal);
                            else SystemSounds.Beep.Play();
                            URL = null;
                        }
                        else
                        {
                            string URL = DataGridView_Principal[Columna_URL_Origen.Index, e.RowIndex].Value as string;
                            if (!string.IsNullOrEmpty(URL)) Program.Ejecutar_Ruta(URL, ProcessWindowStyle.Normal);
                            else SystemSounds.Beep.Play();
                            URL = null;
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void DataGridView_Principal_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                Depurador.Escribir_Excepción(e.Exception != null ? e.Exception.ToString() : null);
                e.ThrowException = false;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
