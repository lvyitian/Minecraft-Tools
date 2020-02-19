using Minecraft_Tools.Properties;
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
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Visor_Cuadros : Form
    {
        public Ventana_Visor_Cuadros()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Structure that holds up all the information about a painting.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Cuadros
        {
            internal string Nombre;
            internal string Nombre_Real;
            internal string Versión;
            internal string Descripción;
            internal string Recurso;
            internal string Recurso_Faithful;
            internal string Recurso_Real;
            internal Rectangle Rectángulo;

            internal Cuadros(string Nombre, string Nombre_Real, string Versión, string Descripción, string Recurso, string Recurso_Faithful, string Recurso_Real, Rectangle Rectángulo)
            {
                this.Nombre = Nombre;
                this.Nombre_Real = Nombre_Real;
                this.Versión = Versión;
                this.Descripción = Descripción;
                this.Recurso = Recurso;
                this.Recurso_Faithful = Recurso_Faithful;
                this.Recurso_Real = Recurso_Real;
                this.Rectángulo = Rectángulo;
            }

            internal static readonly Cuadros[] Matriz_Cuadros = new Cuadros[]
            {
                new Cuadros
                (
                    "Alban",
                    "Albanian",
                    "Indev",
                    "A man wearing a fez in a desert-type land stood next to a house and a bush. As the name of the painting suggests, it may be a landscape in Albania. However Albania is mostly snowy mountains and there are no \"deserts\", therefore making it impossible to be located in Albania.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Alban",
                    new Rectangle(32, 0, 16, 16)
                ),
                new Cuadros
                (
                    "Aztec",
                    "de_aztec",
                    "Indev",
                    "Free-look perspective of the map de_aztec from the video game Counter-Strike.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Aztec",
                    new Rectangle(16, 0, 16, 16)
                ),
                new Cuadros
                (
                    "Aztec2",
                    "de_aztec",
                    "Indev",
                    "Free-look perspective of the map de_aztec from the video game Counter-Strike.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Aztec2",
                    new Rectangle(48, 0, 16, 16)
                ),
                new Cuadros
                (
                    "Bomb",
                    "Target successfully bombed",
                    "Indev",
                    "Painting of the Counter-Strike map de_dust2, named \"target successfully bombed\" in reference to the game.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Bomb",
                    new Rectangle(64, 0, 16, 16)
                ),
                new Cuadros
                (
                    "Burning skull",
                    "Skull on Fire",
                    "Beta 1.2_01",
                    "A Skull on pixelated fire; in the background there is a moon in a clear night sky.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_BurningSkull",
                    new Rectangle(128, 192, 64, 64)
                ),
                new Cuadros
                (
                    "Bust",
                    "Bust",
                    "Indev",
                    "Painting of a statue bust surrounded by pixelated fire.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Bust",
                    new Rectangle(32, 128, 32, 32)
                ),
                new Cuadros
                (
                    "Courbet",
                    "Bonjour monsieur Courbet",
                    "Indev",
                    "Two hikers with pointy beards seemingly greeting each other. This painting is based on the realist painter Gustave Courbet's 1854 painting of the same title.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Courbet",
                    new Rectangle(32, 32, 32, 16)
                ),
                new Cuadros
                (
                    "Creebet",
                    "Seaside",
                    "Alpha 1.1.1",
                    "Painting of a view of mountains and a lake, with a small photo of a mountain and a creeper looking at the viewer through a window.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Sea",
                    new Rectangle(128, 32, 32, 16)
                ),
                new Cuadros
                (
                    "Donkey Kong",
                    "Kong",
                    "Alpha 1.1.1",
                    "A paper-looking screenshot of the level 100 m. from the original \"Donkey Kong\" arcade game.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_DonkeyKong",
                    new Rectangle(192, 112, 64, 48)
                ),
                new Cuadros
                (
                    "Fighters",
                    "Fighters",
                    "Indev",
                    "Two pixelated men poised to fight. Paper versions of fighters from the game \"International Karate+\".",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Fighters",
                    new Rectangle(0, 96, 64, 32)
                ),
                new Cuadros
                (
                    "Graham",
                    "Graham",
                    "Alpha 1.1.1",
                    "A small picture of King Graham, the player character in the King's Quest series.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Graham",
                    new Rectangle(16, 64, 16, 32)
                ),
                new Cuadros
                (
                    "Kebab",
                    "Kebab med tre pepperoni",
                    "Indev",
                    "A kebab with three green chili peppers.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Kebab",
                    new Rectangle(0, 0, 16, 16)
                ),
                new Cuadros
                (
                    "Match",
                    "Match",
                    "Indev",
                    "A hand holding a match, causing pixelated fire on a white cubic gas fireplace.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Match",
                    new Rectangle(0, 128, 32, 32)
                ),
                new Cuadros
                (
                    "Pigscene",
                    "RGB",
                    "Alpha 1.1.1",
                    "Painting of a girl that is pointing to a pig on a canvas. In the original version, the canvas shows red, green and blue blocks, representing the three colors of the RGB color model that is typically used by computer displays.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Pigscene",
                    new Rectangle(64, 192, 64, 64)
                ),
                new Cuadros
                (
                    "Plant",
                    "Paradisträd",
                    "Indev",
                    "Still life painting of two plants in pots. \"Paradisträd\" is Swedish for \"Paradise tree\", which is a common name for the depicted species in Scandinavia.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Plant",
                    new Rectangle(80, 0, 16, 16)
                ),
                new Cuadros
                (
                    "Pointer",
                    "Pointer",
                    "Indev",
                    "A painting of the main character of the classic Atari game International Karate (the Karateka character had white hair, this one clearly has black hair) fighting a large hand. It could also be interpreted as the two hands touching as seen in Michelangelo's Sistine Chapel painting.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Pointer",
                    new Rectangle(0, 192, 64, 64)
                ),
                new Cuadros
                (
                    "Pool",
                    "The pool",
                    "Indev",
                    "Some men and women skinny-dipping in a pool over a cube of sorts. Also there is an old man resting in the lower-right edge.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Pool",
                    new Rectangle(0, 32, 32, 16)
                ),
                new Cuadros
                (
                    "Sea",
                    "Seaside",
                    "Indev",
                    "Painting of a view of mountains and a lake, with a small photo of a mountain and a dull-colored plant on the window ledge. Note: In Alpha 1.1.1, this painting was replaced by the next image, featuring a more colorful plant.",
                    "Cuadros_Indev",
                    "Cuadros_Faithful",
                    "Cuadros_Sea",
                    new Rectangle(64, 32, 32, 16)
                ),
                new Cuadros
                (
                    "Sea",
                    "Seaside",
                    "Alpha 1.1.1",
                    "Painting of a view of mountains and a lake, with a small photo of a mountain and a dull-colored plant on the window ledge. Note: In Alpha 1.1.1, this painting replaced the previous image, featuring a less colorful plant.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Sea",
                    new Rectangle(64, 32, 32, 16)
                ),
                new Cuadros
                (
                    "Skeleton",
                    "Mortal Coil",
                    "Alpha 1.1.1",
                    "A painting of the \"Mean Midget\" from the adventure game Grim Fandango.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Skeleton",
                    new Rectangle(192, 64, 64, 48)
                ),
                new Cuadros
                (
                    "Skull and roses",
                    "Moonlight Installation",
                    "Indev",
                    "Painting of a skeleton at night with red flowers in the foreground. The original painting is very different, depicting a woman sitting in a couch, while the skull is in the middle of a body of glacial water of sorts.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_SkullAndRoses",
                    new Rectangle(128, 128, 32, 32)
                ),
                new Cuadros
                (
                    "Stage",
                    "The stage is set",
                    "Indev",
                    "Painting of scenery from Space Quest I, with the character Graham from King's Quest. Note: In Alpha 1.1.1, this painting was replaced by the next image, featuring a larger spider.",
                    "Cuadros_Indev",
                    "Cuadros_Faithful",
                    "Cuadros_Stage",
                    new Rectangle(64, 128, 32, 32)
                ),
                new Cuadros
                (
                    "Stage",
                    "The stage is set",
                    "Alpha 1.1.1",
                    "Painting of scenery from Space Quest I, with the character Graham from King's Quest. Note: In Alpha 1.1.1, this painting replaced the previous image, featuring a smaller spider.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Stage",
                    new Rectangle(64, 128, 32, 32)
                ),
                new Cuadros
                (
                    "Sunset",
                    "sunset_dense",
                    "Indev",
                    "Painting of a view of mountains at sunset.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Sunset",
                    new Rectangle(96, 32, 32, 16)
                ),
                new Cuadros
                (
                    "Void",
                    "The Void",
                    "Indev",
                    "Painting of an angel praying into what appears to be a void with pixelated fire below.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Void",
                    new Rectangle(96, 128, 32, 32)
                ),
                new Cuadros
                (
                    "Wanderer",
                    "Wanderer",
                    "Indev",
                    "A low-resolution version of Caspar David Friedrich's famous painting Wanderer above the Sea of Fog.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Wanderer",
                    new Rectangle(0, 64, 16, 32)
                ),
                new Cuadros
                (
                    "Wasteland",
                    "Wasteland",
                    "Indev",
                    "Painting of a view of some wastelands; a small animal (presumably a rabbit) is sitting on the window ledge.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Wasteland",
                    new Rectangle(96, 0, 16, 16)
                ),
                new Cuadros
                (
                    "Wither",
                    "Wither", // "-",
                    "1.4.2 (Snapshot 12w36a)",
                    "Painting depicting the creation of a wither. This is the only painting not based on a real painting.",
                    "Cuadros",
                    "Cuadros_Faithful",
                    "Cuadros_Wither",
                    new Rectangle(160, 128, 32, 32)
                )
            };
        }

        internal static CheckState Variable_HD = CheckState.Indeterminate;
        internal static int Variable_Cuadro = 0;
        internal static CheckState Variable_Antialiasing = CheckState.Unchecked;
        internal static bool Variable_Autozoom = true;
        internal static bool Variable_Filtro_Negativo = false;
        internal static bool Variable_Filtro_Raíz_Cuadrada = false;
        internal static bool Variable_Filtro_Logaritmo = false;

        internal static readonly string Ruta_Recursos_Externos_Paintings = Application.StartupPath + "\\Paintings";
        internal readonly string Texto_Título = "Paintings Viewer by Jupisoft for " + Program.Texto_Usuario;
        internal bool Variable_Siempre_Visible = false;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;
        
        internal static byte[] Variable_Matriz_Bytes_Filtro = null;

        private void Ventana_Visor_Cuadros_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                for (int Índice_Cuadro = 0; Índice_Cuadro < Cuadros.Matriz_Cuadros.Length; Índice_Cuadro++)
                {
                    ComboBox_Cuadro.Items.Add(Cuadros.Matriz_Cuadros[Índice_Cuadro].Nombre + " (" + Cuadros.Matriz_Cuadros[Índice_Cuadro].Nombre_Real + ") [" + Cuadros.Matriz_Cuadros[Índice_Cuadro].Versión + "]");
                }
                CheckBox_Cuadro_HD.CheckState = Variable_HD;
                CheckBox_Antialiasing.CheckState = Variable_Antialiasing;
                CheckBox_Autozoom.Checked = Variable_Autozoom;
                Menú_Contextual_Filtro_Negativo.Checked = Variable_Filtro_Negativo;
                Menú_Contextual_Filtro_Raíz_Cuadrada.Checked = Variable_Filtro_Raíz_Cuadrada;
                Menú_Contextual_Filtro_Logaritmo.Checked = Variable_Filtro_Logaritmo;
                if (ComboBox_Cuadro.Items.Count > 0) ComboBox_Cuadro.SelectedIndex = 0;
                TextBox_Descripción.Font = new Font("Calibri", 10f);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Cuadros_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Cuadros_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Cuadros_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Cuadros_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                Establecer_Cuadro();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Cuadros_KeyDown(object sender, KeyEventArgs e)
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
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    Menú_Contextual_Filtro_Logaritmo.PerformClick();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Cuadro_HD_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_HD = CheckBox_Cuadro_HD.CheckState;
                Establecer_Cuadro();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Cuadro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Cuadro.SelectedIndex > -1)
                {
                    Variable_Cuadro = ComboBox_Cuadro.SelectedIndex;
                    Establecer_Cuadro();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Antialiasing_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Antialiasing = CheckBox_Antialiasing.CheckState;
                Establecer_Cuadro();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Autozoom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Autozoom = CheckBox_Autozoom.Checked;
                Establecer_Cuadro();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.Paintings_viewer;
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
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Visor_Cuadros);
                Program.Ejecutar_Ruta(Program.Ruta_Guardado_Imágenes_Visor_Cuadros, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Establecer_Cuadro();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Filtro_Negativo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Filtro_Negativo = Menú_Contextual_Filtro_Negativo.Checked;
                Variable_Matriz_Bytes_Filtro = Program.Combinar_Matrices_Bytes_Filtros(new byte[][]
                {
                    Variable_Filtro_Negativo ? Program.Matriz_Bytes_Filtro_Negativo : null,
                    Variable_Filtro_Raíz_Cuadrada ? Program.Matriz_Bytes_Filtro_Raíz_Cuadrada : null,
                    Variable_Filtro_Logaritmo ? Program.Matriz_Bytes_Filtro_Logaritmo : null
                });
                Establecer_Cuadro();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Filtro_Raíz_Cuadrada_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Filtro_Raíz_Cuadrada = Menú_Contextual_Filtro_Raíz_Cuadrada.Checked;
                Variable_Matriz_Bytes_Filtro = Program.Combinar_Matrices_Bytes_Filtros(new byte[][]
                {
                    Variable_Filtro_Negativo ? Program.Matriz_Bytes_Filtro_Negativo : null,
                    Variable_Filtro_Raíz_Cuadrada ? Program.Matriz_Bytes_Filtro_Raíz_Cuadrada : null,
                    Variable_Filtro_Logaritmo ? Program.Matriz_Bytes_Filtro_Logaritmo : null
                });
                Establecer_Cuadro();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Filtro_Logaritmo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Filtro_Logaritmo = Menú_Contextual_Filtro_Logaritmo.Checked;
                Variable_Matriz_Bytes_Filtro = Program.Combinar_Matrices_Bytes_Filtros(new byte[][]
                {
                    Variable_Filtro_Negativo ? Program.Matriz_Bytes_Filtro_Negativo : null,
                    Variable_Filtro_Raíz_Cuadrada ? Program.Matriz_Bytes_Filtro_Raíz_Cuadrada : null,
                    Variable_Filtro_Logaritmo ? Program.Matriz_Bytes_Filtro_Logaritmo : null
                });
                Establecer_Cuadro();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.Image != null)
                {
                    Clipboard.SetImage(Picture.Image);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Picture.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Visor_Cuadros);
                    Picture.Image.Save(Program.Ruta_Guardado_Imágenes_Visor_Cuadros + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + " " + Cuadros.Matriz_Cuadros[Variable_Cuadro].Nombre + (Variable_HD == CheckState.Unchecked ? null : (Variable_HD == CheckState.Unchecked ? " HD" : " Real")) + ".png", ImageFormat.Png);
                    SystemSounds.Asterisk.Play();
                }
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
                catch { Barra_Estado_Etiqueta_Memoria.Text = "RAM: ? MB (? GB)"; }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Establecer_Cuadro()
        {
            try
            {
                Bitmap Imagen = null;
                if (Variable_Cuadro > -1)
                {
                    if (Variable_HD == CheckState.Unchecked) // Minecraft painting.
                    {
                        Imagen = Program.Obtener_Imagen_Recursos(Cuadros.Matriz_Cuadros[Variable_Cuadro].Recurso);
                        if (Imagen != null) Imagen = Imagen.Clone(Cuadros.Matriz_Cuadros[Variable_Cuadro].Rectángulo, PixelFormat.Format32bppArgb);
                    }
                    else if (Variable_HD == CheckState.Checked) // Faithful painting.
                    {
                        Imagen = Program.Obtener_Imagen_Recursos(Cuadros.Matriz_Cuadros[Variable_Cuadro].Recurso_Faithful);
                        if (Imagen != null) Imagen = Imagen.Clone(new Rectangle(Cuadros.Matriz_Cuadros[Variable_Cuadro].Rectángulo.X * 2, Cuadros.Matriz_Cuadros[Variable_Cuadro].Rectángulo.Y * 2, Cuadros.Matriz_Cuadros[Variable_Cuadro].Rectángulo.Width * 2, Cuadros.Matriz_Cuadros[Variable_Cuadro].Rectángulo.Height * 2), PixelFormat.Format32bppArgb);
                    }
                    else // Real painting.
                    {
                        Imagen = Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\" + Cuadros.Matriz_Cuadros[Variable_Cuadro].Recurso_Real, CheckState.Checked);
                    }
                    if (Imagen != null)
                    {
                        int Ancho = Imagen.Width;
                        int Alto = Imagen.Height;
                        if (Variable_Filtro_Negativo || Variable_Filtro_Raíz_Cuadrada || Variable_Filtro_Logaritmo)
                        {
                            Imagen = Program.Obtener_Imagen_Filtrada(Imagen, Variable_Matriz_Bytes_Filtro);
                        }
                        int Zoom = 1;
                        if (Variable_Autozoom) Imagen = Program.Obtener_Imagen_Autozoom(Imagen, Picture.ClientSize.Width, Picture.ClientSize.Height, true, Variable_Antialiasing, out Zoom);
                        this.Text = Texto_Título + " - [Minecraft " + Cuadros.Matriz_Cuadros[Variable_Cuadro].Versión + ", Dimensions: " + Program.Traducir_Número(Ancho) + " x " + Program.Traducir_Número(Alto) + (Ancho + Alto != 1 ? " pixels" : " pixel") + ", Autozoom: " + Program.Traducir_Número(Zoom) + "x]";
                    }
                    else this.Text = Texto_Título + " - [Minecraft " + Cuadros.Matriz_Cuadros[Variable_Cuadro].Versión + ", Dimensions: ? x ? pixels, Autozoom: ?x]";
                }
                else this.Text = Texto_Título + " - [Minecraft " + Cuadros.Matriz_Cuadros[Variable_Cuadro].Versión + ", Dimensions: ? x ? pixels, Autozoom: ?x]";
                Picture.Image = Imagen;
                Picture.Refresh();
                TextBox_Descripción.Text = "\"" + Cuadros.Matriz_Cuadros[Variable_Cuadro].Nombre_Real + "\": " + Cuadros.Matriz_Cuadros[Variable_Cuadro].Descripción;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Generates a new resource pack for the specified pack format containing the real HD paintings in a 8.192 x 8.192 texture. Note: the pack number will be the only difference in the pack, so it should load correctly on any Minecraft version (in theory).
        /// </summary>
        /// <param name="Pack">A number between 1 and 4 (Minecraft 1.13+).</param>
        internal void Exportar_Pack_Recursos_Cuadros(int Pack, bool Exportar_JPEG, int Calidad_JPEG)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Program.Crear_Carpetas(Program.Ruta_Guardado_Imágenes_Visor_Cuadros);
                if (Directory.Exists(Program.Ruta_Guardado_Imágenes_Visor_Cuadros))
                {
                    Bitmap Imagen = new Bitmap(8192, 8192, PixelFormat.Format24bppRgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.None;
                    Pintar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    SolidBrush Pincel_Fondo = new SolidBrush(Color.FromArgb(214, 127, 255));
                    SolidBrush Pincel_Bordes = new SolidBrush(Color.FromArgb(107, 63, 127));
                    for (int Índice_Y = 0; Índice_Y < 8192; Índice_Y += 512)
                    {
                        for (int Índice_X = 0; Índice_X < 8192; Índice_X += 512)
                        {
                            Pintar.FillRectangle(Pincel_Bordes, Índice_X, Índice_Y, 512, 512);
                            Pintar.FillRectangle(Pincel_Fondo, Índice_X + 1, Índice_Y + 1, 510, 510);
                        }
                    }
                    Pincel_Fondo.Dispose();
                    Pincel_Bordes.Dispose();
                    Pincel_Fondo = null;
                    Pincel_Bordes = null;

                    // Draw the background texture of the paintings, here like the original.
                    TextureBrush Pincel_Madera = new TextureBrush(Resources.Cuadros_Madera, WrapMode.Tile);
                    Pintar.FillRectangle(Pincel_Madera, 6144, 0, 2048, 2048);
                    Pincel_Madera.Dispose();
                    Pincel_Madera = null;

                    // Draw the real HD paintings.
                    /*string Ruta_Cuadros = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + "\\__Monster High\\Paintings";

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Kebab.png", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(0, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Aztec.png", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(512, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Alban.png", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(1024, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Aztec2.png", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(1536, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Bomb.png", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(2048, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Plant.png", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(2560, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Wasteland.png", CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(3072, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Pool.png", CheckState.Unchecked), 1024, 512, true, true, CheckState.Unchecked), new Rectangle(0, 1024, 1024, 512), new Rectangle(0, 0, 1024, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Courbet.png", CheckState.Unchecked), 1024, 512, false, true, CheckState.Unchecked), new Rectangle(1024, 1024, 1024, 512), new Rectangle(0, 0, 1024, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Sea.png", CheckState.Unchecked), 1024, 512, false, true, CheckState.Unchecked), new Rectangle(2048, 1024, 1024, 512), new Rectangle(0, 0, 1024, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Sunset.png", CheckState.Unchecked), 1024, 512, false, true, CheckState.Unchecked), new Rectangle(3072, 1024, 1024, 512), new Rectangle(0, 0, 1024, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Sea.png", CheckState.Unchecked), 1024, 512, false, true, CheckState.Unchecked), new Rectangle(4096, 1024, 1024, 512), new Rectangle(0, 0, 1024, 512), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Wanderer.png", CheckState.Unchecked), 512, 1024, false, true, CheckState.Unchecked), new Rectangle(0, 2048, 512, 1024), new Rectangle(0, 0, 512, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Graham.png", CheckState.Unchecked), 512, 1024, false, true, CheckState.Unchecked), new Rectangle(512, 2048, 512, 1024), new Rectangle(0, 0, 512, 1024), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Fighters.png", CheckState.Unchecked), 2048, 1024, false, true, CheckState.Unchecked), new Rectangle(0, 3072, 2048, 1024), new Rectangle(0, 0, 2048, 1024), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Match.png", CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(0, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Bust.png", CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(1024, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Stage.png", CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(2048, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Void.png", CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(3072, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_SkullAndRoses.png", CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(4096, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Wither.png", CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(5120, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Pointer.png", CheckState.Unchecked), 2048, 2048, false, true, CheckState.Unchecked), new Rectangle(0, 6144, 2048, 2048), new Rectangle(0, 0, 2048, 2048), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Pigscene.png", CheckState.Unchecked), 2048, 2048, false, true, CheckState.Unchecked), new Rectangle(2048, 6144, 2048, 2048), new Rectangle(0, 0, 2048, 2048), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_BurningSkull.png", CheckState.Unchecked), 2048, 2048, false, true, CheckState.Unchecked), new Rectangle(4096, 6144, 2048, 2048), new Rectangle(0, 0, 2048, 2048), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_Skeleton.png", CheckState.Unchecked), 2048, 1536, false, true, CheckState.Unchecked), new Rectangle(6144, 2048, 2048, 1536), new Rectangle(0, 0, 2048, 1536), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Cargar_Imagen_Ruta(Ruta_Cuadros + "\\Cuadros_DonkeyKong.png", CheckState.Unchecked), 2048, 1536, false, true, CheckState.Unchecked), new Rectangle(6144, 3584, 2048, 1536), new Rectangle(0, 0, 2048, 1536), GraphicsUnit.Pixel);*/

                    // Do the same but from the resources (the images won't be perfectly centered).
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Kebab", CheckState.Checked), 1, 0, 394, 394, CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(0, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Aztec", CheckState.Checked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(512, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Alban", CheckState.Checked), 0, 55, 339, 339, CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(1024, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Aztec2", CheckState.Checked), 0, 0, 403, 403, CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(1536, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Bomb", CheckState.Checked), 75, 0, 411, 411, CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(2048, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Plant", CheckState.Checked), 2, 0, 266, 266, CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(2560, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Wasteland", CheckState.Checked), 61, 0, 576, 576, CheckState.Unchecked), 512, 512, false, true, CheckState.Unchecked), new Rectangle(3072, 0, 512, 512), new Rectangle(0, 0, 512, 512), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Pool", CheckState.Checked), 0, 88, 800, 400, CheckState.Unchecked), 1024, 512, true, true, CheckState.Unchecked), new Rectangle(0, 1024, 1024, 512), new Rectangle(0, 0, 1024, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Courbet", CheckState.Checked), 0, 98, 640, 320, CheckState.Unchecked), 1024, 512, false, true, CheckState.Unchecked), new Rectangle(1024, 1024, 1024, 512), new Rectangle(0, 0, 1024, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Sea", CheckState.Checked), 0, 137, 700, 350, CheckState.Unchecked), 1024, 512, false, true, CheckState.Unchecked), new Rectangle(2048, 1024, 1024, 512), new Rectangle(0, 0, 1024, 512), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Sunset", CheckState.Checked), 0, 50, 600, 300, CheckState.Unchecked), 1024, 512, false, true, CheckState.Unchecked), new Rectangle(3072, 1024, 1024, 512), new Rectangle(0, 0, 1024, 512), GraphicsUnit.Pixel);

                    // Image used to draw a huge Creeper face like the Minecraft painting.
                    Bitmap Imagen_Creebet = Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Sea", CheckState.Checked), 0, 137, 700, 350, CheckState.Unchecked), 1024, 512, false, true, CheckState.Unchecked);
                    Graphics Pintar_Creebet = Graphics.FromImage(Imagen_Creebet);
                    Pintar_Creebet.CompositingMode = CompositingMode.SourceCopy;
                    Pintar_Creebet.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar_Creebet.InterpolationMode = InterpolationMode.NearestNeighbor;
                    Pintar_Creebet.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar_Creebet.SmoothingMode = SmoothingMode.None;
                    Pintar_Creebet.TextRenderingHint = TextRenderingHint.AntiAlias;
                    int Ancho_Alto_Creeper = 384;
                    Pintar_Creebet.DrawImage(Resources.minecraft_creeper_head, new Rectangle(512 + 10, ((512 - Ancho_Alto_Creeper) / 2) - 2, Ancho_Alto_Creeper, Ancho_Alto_Creeper), new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                    Pintar_Creebet.Dispose();
                    Pintar_Creebet = null;
                    Pintar.DrawImage(Imagen_Creebet, new Rectangle(4096, 1024, 1024, 512), new Rectangle(0, 0, 1024, 512), GraphicsUnit.Pixel);
                    Imagen_Creebet.Dispose();
                    Imagen_Creebet = null;

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Wanderer", CheckState.Checked), 10, 73, 320, 640, CheckState.Unchecked), 512, 1024, false, true, CheckState.Unchecked), new Rectangle(0, 2048, 512, 1024), new Rectangle(0, 0, 512, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Graham", CheckState.Checked), 214, 0, 211, 422, CheckState.Unchecked), 512, 1024, false, true, CheckState.Unchecked), new Rectangle(512, 2048, 512, 1024), new Rectangle(0, 0, 512, 1024), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Fighters", CheckState.Checked), 0, 319, 740, 370, CheckState.Unchecked), 2048, 1024, false, true, CheckState.Unchecked), new Rectangle(0, 3072, 2048, 1024), new Rectangle(0, 0, 2048, 1024), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Match", CheckState.Checked), 0, 0, 700, 700, CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(0, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Bust", CheckState.Checked), 0, 2, 640, 640, CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(1024, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Stage", CheckState.Checked), 45, 0, 532, 532, CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(2048, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Void", CheckState.Checked), 0, 0, 631, 631, CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(3072, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_SkullAndRoses", CheckState.Checked), 0, 0, 697, 697, CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(4096, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Wither", CheckState.Checked), 2, 2, 60, 60, CheckState.Unchecked), 1024, 1024, false, true, CheckState.Unchecked), new Rectangle(5120, 4096, 1024, 1024), new Rectangle(0, 0, 1024, 1024), GraphicsUnit.Pixel);
                    
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Pointer", CheckState.Checked), 8, 0, 742, 742, CheckState.Unchecked), 2048, 2048, false, true, CheckState.Unchecked), new Rectangle(0, 6144, 2048, 2048), new Rectangle(0, 0, 2048, 2048), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Pigscene", CheckState.Checked), 0, 2, 640, 640, CheckState.Unchecked), 2048, 2048, false, true, CheckState.Unchecked), new Rectangle(2048, 6144, 2048, 2048), new Rectangle(0, 0, 2048, 2048), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_BurningSkull", CheckState.Checked), 0, 0, 697, 697, CheckState.Unchecked), 2048, 2048, false, true, CheckState.Unchecked), new Rectangle(4096, 6144, 2048, 2048), new Rectangle(0, 0, 2048, 2048), GraphicsUnit.Pixel);

                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_Skeleton", CheckState.Checked), 0, 114, 500, 345, CheckState.Unchecked), 2048, 1536, false, true, CheckState.Unchecked), new Rectangle(6144, 2048, 2048, 1536), new Rectangle(0, 0, 2048, 1536), GraphicsUnit.Pixel);
                    Pintar.DrawImage(Program.Obtener_Imagen_Miniatura(Program.Obtener_Imagen_Recortada(Program.Obtener_Imagen_Recursos_Externos(Ruta_Recursos_Externos_Paintings + "\\Cuadros_DonkeyKong", CheckState.Checked), 0, 207, 500, 375, CheckState.Unchecked), 2048, 1536, false, true, CheckState.Unchecked), new Rectangle(6144, 3584, 2048, 1536), new Rectangle(0, 0, 2048, 1536), GraphicsUnit.Pixel);
                    
                    Pintar.Dispose();
                    Pintar = null;

                    // Allow even post support to all the available image filters.
                    if (Variable_Filtro_Negativo || Variable_Filtro_Raíz_Cuadrada || Variable_Filtro_Logaritmo)
                    {
                        Imagen = Program.Obtener_Imagen_Filtrada(Imagen, Variable_Matriz_Bytes_Filtro);
                    }

                    // Start a new ZIP file to store the resource pack.
                    string Ruta = Program.Ruta_Guardado_Imágenes_Visor_Cuadros + "\\Real Paintings [" + (Pack < 4 ? "1.12.2-" : "1.13+") + "] [" + (!Exportar_JPEG ? "PNG" : "JPEG") + "] " + Program.Obtener_Nombre_Temporal() + ".zip";
                    FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector.SetLength(0L);
                    Lector.Seek(0L, SeekOrigin.Begin);
                    ICSharpCode.SharpZipLib.Zip.ZipFile Archivo_ZIP = ICSharpCode.SharpZipLib.Zip.ZipFile.Create(Lector);

                    // Write the "pack.mcmeta".
                    string Ruta_Pack_MCMETA = Program.Ruta_Guardado_Imágenes_Visor_Cuadros + "\\" + Program.Obtener_Nombre_Temporal() + " pack.mcmeta";
                    FileStream Lector_Pack_MCMETA = new FileStream(Ruta_Pack_MCMETA, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector_Pack_MCMETA.SetLength(0L);
                    Lector_Pack_MCMETA.Seek(0L, SeekOrigin.Begin);
                    StreamWriter Lector_Texto_Pack_MCMETA = new StreamWriter(Lector_Pack_MCMETA, Encoding.UTF8);
                    Lector_Texto_Pack_MCMETA.WriteLine("{");
                    Lector_Texto_Pack_MCMETA.WriteLine("  \"pack\": {");
                    Lector_Texto_Pack_MCMETA.WriteLine("    \"pack_format\": " + Pack.ToString() + ",");
                    Lector_Texto_Pack_MCMETA.WriteLine("    \"description\": \"§fReal Paintings§r for §fJava Edition\\n§6Authors:§r §cXisumavoid & Jupisoft\"");
                    Lector_Texto_Pack_MCMETA.WriteLine("  }");
                    Lector_Texto_Pack_MCMETA.WriteLine("}");
                    Lector_Texto_Pack_MCMETA.Close();
                    Lector_Texto_Pack_MCMETA.Dispose();
                    Lector_Texto_Pack_MCMETA = null;
                    Lector_Pack_MCMETA.Close();
                    Lector_Pack_MCMETA.Dispose();
                    Lector_Pack_MCMETA = null;

                    // Write the "pack.png".
                    string Ruta_Pack_PNG = Program.Ruta_Guardado_Imágenes_Visor_Cuadros + "\\" + Program.Obtener_Nombre_Temporal() + " pack.png";
                    Resources.Paintings.Save(Ruta_Pack_PNG, ImageFormat.Png); // Jupisoft_256

                    // Write the "paintings_kristoffer_zetterstrand.png".
                    string Ruta_Paintings_Kristoffer_Zetterstrand_PNG = Program.Ruta_Guardado_Imágenes_Visor_Cuadros + "\\" + Program.Obtener_Nombre_Temporal() + " paintings_kristoffer_zetterstrand" + (!Exportar_JPEG ? ".png" : ".jpg");
                    if (!Exportar_JPEG) // Save as a PNG image (~ 62 MB).
                    {
                        Imagen.Save(Ruta_Paintings_Kristoffer_Zetterstrand_PNG, ImageFormat.Png);
                    }
                    else // Save as a JPEG image (Quality 90: ~ 6 MB, Quality 100: ~ 18 MB).
                    {
                        ImageCodecInfo Codificador = Program.Obtener_Imagen_Codificador_Guid(ImageFormat.Jpeg.Guid);
                        if (Codificador != null) // We can choose a any JPEG compression.
                        {
                            EncoderParameters Parámetros = new EncoderParameters(1);
                            Parámetros.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)Calidad_JPEG);
                            Imagen.Save(Ruta_Paintings_Kristoffer_Zetterstrand_PNG, Codificador, Parámetros);
                            Parámetros.Dispose();
                            Parámetros = null;
                            Codificador = null;
                        }
                        else Imagen.Save(Ruta_Paintings_Kristoffer_Zetterstrand_PNG, ImageFormat.Jpeg); // Only default compression.
                    }

                    // Once all the files for the ZIP archive have been saved, add them to the ZIP itself.
                    Archivo_ZIP.BeginUpdate();
                    Archivo_ZIP.Add(Ruta_Pack_MCMETA, "pack.mcmeta");
                    Archivo_ZIP.Add(Ruta_Pack_PNG, "pack.png");
                    Archivo_ZIP.AddDirectory("assets\\minecraft\\textures\\painting");
                    Archivo_ZIP.Add(Ruta_Paintings_Kristoffer_Zetterstrand_PNG, "assets\\minecraft\\textures\\painting\\paintings_kristoffer_zetterstrand.png"); // Note: if the original image is a JPEG, it will have a false extension, but it should still work normally.
                    Archivo_ZIP.CommitUpdate();
                    Archivo_ZIP.Close();
                    Archivo_ZIP = null;
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                    Imagen.Dispose();
                    Imagen = null;

                    // Tries to delete the files already added to the ZIP file.
                    Program.Eliminar_Archivo_Carpeta(Ruta_Pack_MCMETA);
                    Program.Eliminar_Archivo_Carpeta(Ruta_Pack_PNG);
                    Program.Eliminar_Archivo_Carpeta(Ruta_Paintings_Kristoffer_Zetterstrand_PNG);

                    // Done.
                    Program.Ejecutar_Ruta(Program.Ruta_Guardado_Imágenes_Visor_Cuadros, ProcessWindowStyle.Maximized);
                    SystemSounds.Asterisk.Play();
                }
                else SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_JPEG_Click(object sender, EventArgs e)
        {
            try
            {
                Exportar_Pack_Recursos_Cuadros(3, true, 95);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_PNG_Click(object sender, EventArgs e)
        {
            try
            {
                Exportar_Pack_Recursos_Cuadros(3, false, 0);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_JPEG_Click(object sender, EventArgs e)
        {
            try
            {
                Exportar_Pack_Recursos_Cuadros(4, true, 95);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_PNG_Click(object sender, EventArgs e)
        {
            try
            {
                Exportar_Pack_Recursos_Cuadros(4, false, 0);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
