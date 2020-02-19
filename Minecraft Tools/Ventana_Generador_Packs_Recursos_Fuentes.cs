using ICSharpCode.SharpZipLib.Zip;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Generador_Packs_Recursos_Fuentes : Form
    {
        public Ventana_Generador_Packs_Recursos_Fuentes()
        {
            InitializeComponent();
        }

        internal static readonly List<int> Lista_Páginas_Ignoradas = new List<int>(new int[] { 8, 216, 217, 218, 219, 220, 221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 240, 241, 242, 243, 244, 245, 246, 247, 248 });

        /// <summary>
        /// minecraft:font/accented.png.
        /// </summary>
        internal static readonly string Texto_Accented =
        "\u00c0\u00c1\u00c2\u00c3\u00c4\u00c5\u00c6\u00c7\u00c8\u00c9\u00ca\u00cb\u00cc\u00cd\u00ce\u00cf" +
        "\u00d0\u00d1\u00d2\u00d3\u00d4\u00d5\u00d6\u00d9\u00da\u00db\u00dc\u00dd\u00e0\u00e1\u00e2\u00e3" +
        "\u00e4\u00e5\u00e6\u00e7\u00ec\u00ed\u00ee\u00ef\u00f1\u00f2\u00f3\u00f4\u00f5\u00f6\u00f9\u00fa" +
        "\u00fb\u00fc\u00fd\u00ff\u0100\u0101\u0102\u0103\u0104\u0105\u0106\u0107\u0108\u0109\u010a\u010b" +
        "\u010c\u010d\u010e\u010f\u0110\u0111\u0112\u0113\u0114\u0115\u0116\u0117\u0118\u0119\u011a\u011b" +
        "\u011c\u011d\u1e20\u1e21\u011e\u011f\u0120\u0121\u0122\u0123\u0124\u0125\u0126\u0127\u0128\u0129" +
        "\u012a\u012b\u012c\u012d\u012e\u012f\u0130\u0131\u0134\u0135\u0136\u0137\u0139\u013a\u013b\u013c" +
        "\u013d\u013e\u013f\u0140\u0141\u0142\u0143\u0144\u0145\u0146\u0147\u0148\u014a\u014b\u014c\u014d" +
        "\u014e\u014f\u0150\u0151\u0152\u0153\u0154\u0155\u0156\u0157\u0158\u0159\u015a\u015b\u015c\u015d" +
        "\u015e\u015f\u0160\u0161\u0162\u0163\u0164\u0165\u0166\u0167\u0168\u0169\u016a\u016b\u016c\u016d" +
        "\u016e\u016f\u0170\u0171\u0172\u0173\u0174\u0175\u0176\u0177\u0178\u0179\u017a\u017b\u017c\u017d" +
        "\u017e\u01fc\u01fd\u01fe\u01ff\u0218\u0219\u021a\u021b\u0386\u0388\u0389\u038a\u038c\u038e\u038f" +
        "\u0390\u03aa\u03ab\u03ac\u03ad\u03ae\u03af\u03b0\u03ca\u03cb\u03cc\u03cd\u03ce\u0400\u0401\u0403" +
        "\u0407\u040c\u040d\u040e\u0419\u0439\u0450\u0451\u0452\u0453\u0457\u045b\u045c\u045d\u045e\u045f" +
        "\u0490\u0491\u1e02\u1e03\u1e0a\u1e0b\u1e1e\u1e1f\u1e22\u1e23\u1e30\u1e31\u1e40\u1e41\u1e56\u1e57" +
        "\u1e60\u1e61\u1e6a\u1e6b\u1e80\u1e81\u1e82\u1e83\u1e84\u1e85\u1ef2\u1ef3\u00e8\u00e9\u00ea\u00eb" +
        "\u0149\u01e7\u01eb\u040f\u1e0d\u1e25\u1e5b\u1e6d\u1e92\u1eca\u1ecb\u1ecc\u1ecd\u1ee4\u1ee5\u2116" +
        "\u0207\u0194\u0263\u0283\u0000\u0000\u0000\u0000\u0000\u0000\u0000\u0000\u0000\u0000\u0000\u0000";

        /// <summary>
        /// minecraft:font/ascii.png.
        /// </summary>
        internal static readonly string Texto_ASCII =
        "\u00c0\u00c1\u00c2\u00c8\u00ca\u00cb\u00cd\u00d3\u00d4\u00d5\u00da\u00df\u00e3\u00f5\u011f\u0130" +
        "\u0131\u0152\u0153\u015e\u015f\u0174\u0175\u017e\u0207\u0000\u0000\u0000\u0000\u0000\u0000\u0000" +
        "\u0020\u0021\"\u0023\u0024\u0025\u0026\u0027\u0028\u0029\u002a\u002b\u002c\u002d\u002e\u002f" +
        "\u0030\u0031\u0032\u0033\u0034\u0035\u0036\u0037\u0038\u0039\u003a\u003b\u003c\u003d\u003e\u003f" +
        "\u0040\u0041\u0042\u0043\u0044\u0045\u0046\u0047\u0048\u0049\u004a\u004b\u004c\u004d\u004e\u004f" +
        "\u0050\u0051\u0052\u0053\u0054\u0055\u0056\u0057\u0058\u0059\u005a\u005b\\\u005d\u005e\u005f" +
        "\u0060\u0061\u0062\u0063\u0064\u0065\u0066\u0067\u0068\u0069\u006a\u006b\u006c\u006d\u006e\u006f" +
        "\u0070\u0071\u0072\u0073\u0074\u0075\u0076\u0077\u0078\u0079\u007a\u007b\u007c\u007d\u007e\u0000" +
        "\u00c7\u00fc\u00e9\u00e2\u00e4\u00e0\u00e5\u00e7\u00ea\u00eb\u00e8\u00ef\u00ee\u00ec\u00c4\u00c5" +
        "\u00c9\u00e6\u00c6\u00f4\u00f6\u00f2\u00fb\u00f9\u00ff\u00d6\u00dc\u00f8\u00a3\u00d8\u00d7\u0192" +
        "\u00e1\u00ed\u00f3\u00fa\u00f1\u00d1\u00aa\u00ba\u00bf\u00ae\u00ac\u00bd\u00bc\u00a1\u00ab\u00bb" +
        "\u2591\u2592\u2593\u2502\u2524\u2561\u2562\u2556\u2555\u2563\u2551\u2557\u255d\u255c\u255b\u2510" +
        "\u2514\u2534\u252c\u251c\u2500\u253c\u255e\u255f\u255a\u2554\u2569\u2566\u2560\u2550\u256c\u2567" +
        "\u2568\u2564\u2565\u2559\u2558\u2552\u2553\u256b\u256a\u2518\u250c\u2588\u2584\u258c\u2590\u2580" +
        "\u03b1\u03b2\u0393\u03c0\u03a3\u03c3\u03bc\u03c4\u03a6\u0398\u03a9\u03b4\u221e\u2205\u2208\u2229" +
        "\u2261\u00b1\u2265\u2264\u2320\u2321\u00f7\u2248\u00b0\u2219\u00b7\u221a\u207f\u00b2\u25a0\u0000";

        /// <summary>
        /// minecraft:font/nonlatin_european.png.
        /// </summary>
        internal static readonly string Texto_Nonlatin_European =
        "\u00a1\u2030\u00ad\u00b7\u20b4\u2260\u00bf\u00d7\u00d8\u00de\u00df\u00f0\u00f8\u00fe\u0391\u0392" +
        "\u0393\u0394\u0395\u0396\u0397\u0398\u0399\u039a\u039b\u039c\u039d\u039e\u039f\u03a0\u03a1\u03a3" +
        "\u03a4\u03a5\u03a6\u03a7\u03a8\u03a9\u03b1\u03b2\u03b3\u03b4\u03b5\u03b6\u03b7\u03b8\u03b9\u03ba" +
        "\u03bb\u03bc\u03bd\u03be\u03bf\u03c0\u03c1\u03c2\u03c3\u03c4\u03c5\u03c6\u03c7\u03c8\u03c9\u0402" +
        "\u0405\u0406\u0408\u0409\u040a\u040b\u0410\u0411\u0412\u0413\u0414\u0415\u0416\u0417\u0418\u041a" +
        "\u041b\u041c\u041d\u041e\u041f\u0420\u0421\u0422\u0423\u0424\u0425\u0426\u0427\u0428\u0429\u042a" +
        "\u042b\u042c\u042d\u042e\u042f\u0430\u0431\u0432\u0433\u0434\u0435\u0436\u0437\u0438\u043a\u043b" +
        "\u043c\u043d\u043e\u043f\u0440\u0441\u0442\u0443\u0444\u0445\u0446\u0447\u0448\u0449\u044a\u044b" +
        "\u044c\u044d\u044e\u044f\u0454\u0455\u0456\u0458\u0459\u045a\u2013\u2014\u2018\u2019\u201c\u201d" +
        "\u201e\u2026\u204a\u2190\u2191\u2192\u2193\u21c4\uff0b\u018f\u0259\u025b\u026a\u04ae\u04af\u04e8" +
        "\u04e9\u02bb\u02cc\u037e\u0138\u1e9e\u00df\u20bd\u20ac\u0462\u0463\u0474\u0475\u0406\u0472\u0473" +
        "\u2070\u00b9\u00b3\u2074\u2075\u2076\u2077\u2078\u2079\u207a\u207b\u207c\u207d\u207e\u2071\u2122" +
        "\u0294\u0295\u29c8\u2694\u2620\u0000\u0000\u0000\u0000\u0000\u0000\u0000\u0000\u0000\u0000\u0000";

        internal readonly string Texto_Título = "Fonts Resource Packs Generator by Jupisoft for " + Program.Texto_Usuario;
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

        private void Ventana_Generador_Packs_Recursos_Fuentes_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título + " - [The resource packs will be saved on your desktop]";
                this.WindowState = FormWindowState.Maximized;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
                //FontFamily.Families
                //SystemFonts.
                //Font.
                TextBox_Vista_Previa.Text = "abcdefghijklmnopqrstuvwxyz\r\nABCDEFGHIJKLMNOPQRSTUVWXYZ\r\n1234567890\r\n.:,;\'\"(!?) +-*/=\r\n\r\nEl veloz murciélago hindú comía feliz cardillo y kiwi. La cigüeña tocaba el saxofón detrás del palenque de paja.";
                FontFamily[] Matriz_Fuentes = FontFamily.Families;
                if (Matriz_Fuentes != null && Matriz_Fuentes.Length > 0)
                {
                    foreach (FontFamily Fuente in Matriz_Fuentes)
                    {
                        ComboBox_Fuente.Items.Add(Fuente.Name);
                    }
                    Matriz_Fuentes = null;
                    //if (ComboBox_Fuente.Items.Count > 0) ComboBox_Fuente.SelectedIndex = 0;
                    ComboBox_Fuente.Text = ComboBox_Fuente.Font.Name;
                }
                ComboBox_Formato_Pack.SelectedIndex = 3;
                ComboBox_Renderizado.SelectedIndex = (int)TextRenderingHint.AntiAlias;
                //CheckBox_Bold.Font = new Font(CheckBox_Bold.Font.FontFamily, CheckBox_Bold.Font.Size, FontStyle.Bold);
                //CheckBox_Italic.Font = new Font(CheckBox_Italic.Font.FontFamily, CheckBox_Italic.Font.Size, FontStyle.Italic);
                //CheckBox_Underline.Font = new Font(CheckBox_Underline.Font.FontFamily, CheckBox_Underline.Font.Size, FontStyle.Underline);
                //CheckBox_Strikeout.Font = new Font(CheckBox_Strikeout.Font.FontFamily, CheckBox_Strikeout.Font.Size, FontStyle.Strikeout);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Packs_Recursos_Fuentes_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();

                // Extarct the strings in the old Minecraft source codes to learn the sounds used.
                /*string[] Matriz_Archivos = Directory.GetFiles(@"C:\Users\Jupisoft\Desktop\SRC inf-20100627", "*.java", SearchOption.AllDirectories);
                if (Matriz_Archivos != null && Matriz_Archivos.Length > 0)
                {
                    FileStream Lector_Salida = new FileStream(Program.Obtener_Ruta_Temporal_Escritorio() + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector_Salida.SetLength(0L);
                    Lector_Salida.Seek(0L, SeekOrigin.Begin);
                    StreamWriter Lector_Salida_Texto = new StreamWriter(Lector_Salida, Encoding.UTF8);
                    foreach (string Ruta in Matriz_Archivos)
                    {
                        Lector_Salida_Texto.WriteLine("[\"" + Ruta + "\"]");
                        Lector_Salida_Texto.WriteLine();
                        Lector_Salida_Texto.Flush();
                        FileStream Lector_Entrada = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                        if (Lector_Entrada != null && Lector_Entrada.Length > 0L)
                        {
                            Lector_Entrada.Seek(0L, SeekOrigin.Begin);
                            StreamReader Lector_Entrada_Texto = new StreamReader(Lector_Entrada, Encoding.UTF8, true);
                            if (Lector_Entrada_Texto != null)
                            {
                                string Texto = Lector_Entrada_Texto.ReadToEnd();
                                if (!string.IsNullOrEmpty(Texto))
                                {
                                    List<int> Lista_Índices = new List<int>();
                                    for (int Índice_Caracter = 0; Índice_Caracter < Texto.Length; Índice_Caracter++)
                                    {
                                        char Caracter = Texto[Índice_Caracter];
                                        if (Caracter == '\"') Lista_Índices.Add(Índice_Caracter);
                                    }
                                    if (Lista_Índices.Count > 0)
                                    {
                                        for (int Índice = 0; Índice < (Lista_Índices.Count % 2 == 0 ? Lista_Índices.Count : Lista_Índices.Count - 1); Índice += 2)
                                        {
                                            string Línea = Texto.Substring(Lista_Índices[Índice], (Lista_Índices[Índice + 1] - Lista_Índices[Índice]) + 1);
                                            Lector_Salida_Texto.WriteLine(Línea);
                                            Lector_Salida_Texto.Flush();
                                        }
                                    }
                                }
                                Texto = null;
                                Lector_Entrada_Texto.Close();
                                Lector_Entrada_Texto.Dispose();
                                Lector_Entrada_Texto = null;
                            }
                            Lector_Entrada.Close();
                            Lector_Entrada.Dispose();
                            Lector_Entrada = null;
                        }
                        Lector_Salida_Texto.WriteLine();
                        Lector_Salida_Texto.WriteLine();
                        Lector_Salida_Texto.Flush();
                    }
                    Lector_Salida_Texto.Close();
                    Lector_Salida_Texto.Dispose();
                    Lector_Salida_Texto = null;
                    Lector_Salida.Close();
                    Lector_Salida.Dispose();
                    Lector_Salida = null;
                    SystemSounds.Asterisk.Play();
                }*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Packs_Recursos_Fuentes_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Packs_Recursos_Fuentes_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Packs_Recursos_Fuentes_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Generador_Packs_Recursos_Fuentes_DragDrop(object sender, DragEventArgs e)
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
                                if (!string.IsNullOrEmpty(Ruta) && (File.Exists(Ruta) || Directory.Exists(Ruta)))
                                {
                                    //Minecraft.Información_Niveles Información_Nivel = Minecraft.Información_Niveles.Obtener_Información_Nivel(Ruta);
                                    SystemSounds.Beep.Play();
                                    break;
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

        private void Ventana_Generador_Packs_Recursos_Fuentes_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Generador_Packs_Recursos_Fuentes_KeyDown(object sender, KeyEventArgs e)
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

        internal void Actualizar_Vista_Previa()
        {
            try
            {
                FontStyle Estilo = FontStyle.Regular;
                if (CheckBox_Bold.Checked) Estilo |= FontStyle.Bold;
                if (CheckBox_Italic.Checked) Estilo |= FontStyle.Italic;
                if (CheckBox_Underline.Checked) Estilo |= FontStyle.Underline;
                if (CheckBox_Strikeout.Checked) Estilo |= FontStyle.Strikeout;
                TextBox_Vista_Previa.Font = new Font(ComboBox_Fuente.SelectedIndex > -1 && !string.IsNullOrEmpty(ComboBox_Fuente.Text) ? ComboBox_Fuente.Text : TextBox_Vista_Previa.Font.Name, (float)NumericUpDown_Tamaño.Value, Estilo);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Fuente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Actualizar_Vista_Previa();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Tamaño_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Actualizar_Vista_Previa();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Bold_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Actualizar_Vista_Previa();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Italic_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Actualizar_Vista_Previa();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Underline_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Actualizar_Vista_Previa();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Strikeout_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Actualizar_Vista_Previa();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Generar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //int Dimensiones = (int)NumericUpDown_Tamaño.Value;
                float Tamaño = (float)NumericUpDown_Tamaño.Value;
                if (ComboBox_Fuente.SelectedIndex > -1 && !string.IsNullOrEmpty(ComboBox_Fuente.Text))
                {
                    /*string Ruta = "C:\\Users\\Jupisoft\\Pictures\\Jupisoft\\Minecraft Tools\\Secrets\\1.13.2\\assets\\minecraft\\textures\\font";
                    string Texto_No = null;
                    for (int Índice_Página = 0; Índice_Página < 256; Índice_Página++)
                    {
                        string Texto = Convert.ToString(Índice_Página, 16).ToLowerInvariant();
                        while (Texto.Length < 2) Texto = '0' + Texto;
                        if (!File.Exists(Ruta + "\\unicode_page_" + Texto + ".png"))
                        {
                            Texto_No += Índice_Página.ToString() + ", ";
                        }
                    }
                    Clipboard.SetText(Texto_No);
                    return;*/
                    string Nombre = ComboBox_Fuente.Text;
                    List<char> Lista_Caracteres_Inválidos = new List<char>(Path.GetInvalidFileNameChars());
                    string Nombre_Válido = null;
                    foreach (char Caracter in Nombre)
                    {
                        if (!Lista_Caracteres_Inválidos.Contains(Caracter)) Nombre_Válido += Caracter;
                    }
                    Lista_Caracteres_Inválidos = null;
                    FontStyle Estilo = FontStyle.Regular;
                    if (CheckBox_Bold.Checked) Estilo |= FontStyle.Bold;
                    if (CheckBox_Italic.Checked) Estilo |= FontStyle.Italic;
                    if (CheckBox_Underline.Checked) Estilo |= FontStyle.Underline;
                    if (CheckBox_Strikeout.Checked) Estilo |= FontStyle.Strikeout;
                    Font Fuente = new Font(Nombre, Tamaño, Estilo);
                    if (Fuente != null)
                    {
                        int Formato_Pack = ComboBox_Formato_Pack.SelectedIndex + 1;
                        string Ruta_ZIP = Program.Obtener_Ruta_Temporal_Escritorio() + " Font " + Nombre_Válido + " [Size " + Tamaño.ToString() + "] [" + (Formato_Pack == 1 ? "1.6+" : Formato_Pack == 2 ? "1.9+" : Formato_Pack == 3 ? "1.11+" : "1.13+") + "].zip";
                        FileStream Lector = new FileStream(Ruta_ZIP, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                        Lector.SetLength(0L);
                        Lector.Seek(0L, SeekOrigin.Begin);
                        ZipOutputStream Archivo_ZIP = new ZipOutputStream(Lector); // Start a new zip file.
                        MemoryStream Lector_Memoria = null;
                        byte[] Matriz_Bytes = null;

                        Rectangle Rectángulo_Máximo = new Rectangle(int.MaxValue, int.MaxValue, int.MinValue, int.MinValue);
                        Bitmap[] Matriz_Imágenes_Caracteres = new Bitmap[65536];
                        Rectangle[] Matriz_Rectángulos_Caracteres = new Rectangle[65536];
                        for (int Índice_Caracter = 0, Índice_Página = 0; Índice_Caracter < 65536; Índice_Página++)
                        {
                            for (int Índice_Y = 0; Índice_Y < 16; Índice_Y++)
                            {
                                for (int Índice_X = 0; Índice_X < 16; Índice_X++, Índice_Caracter++)
                                {
                                    Bitmap Imagen_Caracter = Program.Obtener_Imagen_Texto(((char)Índice_Caracter).ToString(), Fuente, Color.Empty, Color.White, (TextRenderingHint)ComboBox_Renderizado.SelectedIndex);
                                    Rectangle Rectángulo = Program.Buscar_Zona_Recorte_Imagen(Imagen_Caracter, Color.Empty);
                                    Matriz_Imágenes_Caracteres[Índice_Caracter] = Imagen_Caracter;
                                    Matriz_Rectángulos_Caracteres[Índice_Caracter] = Rectángulo;
                                    if (!Lista_Páginas_Ignoradas.Contains(Índice_Página)) // Skip the pages ignored by Minecraft.
                                    {
                                        if (Rectángulo.X > -1 && Rectángulo.Y > -1 && Rectángulo.X < int.MaxValue && Rectángulo.Y < int.MaxValue && Rectángulo.Width > 0 && Rectángulo.Height > 0)
                                        {
                                            if (Rectángulo.Left < Rectángulo_Máximo.X) Rectángulo_Máximo.X = Rectángulo.Left;
                                            if (Rectángulo.Top < Rectángulo_Máximo.Y) Rectángulo_Máximo.Y = Rectángulo.Top;
                                            if (Rectángulo.Right > Rectángulo_Máximo.Width) Rectángulo_Máximo.Width = Rectángulo.Right;
                                            if (Rectángulo.Bottom > Rectángulo_Máximo.Height) Rectángulo_Máximo.Height = Rectángulo.Bottom;
                                        }
                                        else Matriz_Rectángulos_Caracteres[Índice_Caracter] = new Rectangle(0, 0, 0, 0);
                                    }
                                    else
                                    {
                                        if (Rectángulo.X > -1 && Rectángulo.Y > -1 && Rectángulo.X < int.MaxValue && Rectángulo.Y < int.MaxValue && Rectángulo.Width > 0 && Rectángulo.Height > 0)
                                        {

                                        }
                                        else Matriz_Rectángulos_Caracteres[Índice_Caracter] = new Rectangle(0, 0, 0, 0);
                                    }
                                }
                            }
                        }
                        Rectángulo_Máximo = Rectangle.FromLTRB(Rectángulo_Máximo.X, Rectángulo_Máximo.Y, Rectángulo_Máximo.Width, Rectángulo_Máximo.Height);
                        int Ancho_Alto_Caracter = Math.Max(Rectángulo_Máximo.Width, Rectángulo_Máximo.Height);

                        //Matriz_Imágenes_Caracteres[103].Save(Program.Obtener_Ruta_Temporal_Escritorio() + ", " + Matriz_Rectángulos_Caracteres[103].ToString() + ".png", ImageFormat.Png);

                        Rectangle Rectángulo_Máximo_Accented = new Rectangle(int.MaxValue, int.MaxValue, int.MinValue, int.MinValue);
                        for (int Índice_Y = 0, Índice_Texto = 0; Índice_Y < 18; Índice_Y++)
                        {
                            for (int Índice_X = 0; Índice_X < 16; Índice_X++, Índice_Texto++)
                            {
                                int Índice_Caracter = (int)Texto_Accented[Índice_Texto];
                                if (Matriz_Rectángulos_Caracteres[Índice_Caracter].Width > 0 && Matriz_Rectángulos_Caracteres[Índice_Caracter].Height > 0)
                                {
                                    if (Matriz_Rectángulos_Caracteres[Índice_Caracter].Left < Rectángulo_Máximo_Accented.X) Rectángulo_Máximo_Accented.X = Matriz_Rectángulos_Caracteres[Índice_Caracter].Left;
                                    if (Matriz_Rectángulos_Caracteres[Índice_Caracter].Top < Rectángulo_Máximo_Accented.Y) Rectángulo_Máximo_Accented.Y = Matriz_Rectángulos_Caracteres[Índice_Caracter].Top;
                                    if (Matriz_Rectángulos_Caracteres[Índice_Caracter].Right > Rectángulo_Máximo_Accented.Width) Rectángulo_Máximo_Accented.Width = Matriz_Rectángulos_Caracteres[Índice_Caracter].Right;
                                    if (Matriz_Rectángulos_Caracteres[Índice_Caracter].Bottom > Rectángulo_Máximo_Accented.Height) Rectángulo_Máximo_Accented.Height = Matriz_Rectángulos_Caracteres[Índice_Caracter].Bottom;
                                }
                            }
                        }

                        Rectangle Rectángulo_Máximo_ASCII = new Rectangle(int.MaxValue, int.MaxValue, int.MinValue, int.MinValue);
                        for (int Índice_Y = 0, Índice_Texto = 0; Índice_Y < 16; Índice_Y++)
                        {
                            for (int Índice_X = 0; Índice_X < 16; Índice_X++, Índice_Texto++)
                            {
                                int Índice_Caracter = (int)Texto_ASCII[Índice_Texto];
                                if (Matriz_Rectángulos_Caracteres[Índice_Caracter].Width > 0 && Matriz_Rectángulos_Caracteres[Índice_Caracter].Height > 0)
                                {
                                    if (Matriz_Rectángulos_Caracteres[Índice_Caracter].Left < Rectángulo_Máximo_ASCII.X) Rectángulo_Máximo_ASCII.X = Matriz_Rectángulos_Caracteres[Índice_Caracter].Left;
                                    if (Matriz_Rectángulos_Caracteres[Índice_Caracter].Top < Rectángulo_Máximo_ASCII.Y) Rectángulo_Máximo_ASCII.Y = Matriz_Rectángulos_Caracteres[Índice_Caracter].Top;
                                    if (Matriz_Rectángulos_Caracteres[Índice_Caracter].Right > Rectángulo_Máximo_ASCII.Width) Rectángulo_Máximo_ASCII.Width = Matriz_Rectángulos_Caracteres[Índice_Caracter].Right;
                                    if (Matriz_Rectángulos_Caracteres[Índice_Caracter].Bottom > Rectángulo_Máximo_ASCII.Height) Rectángulo_Máximo_ASCII.Height = Matriz_Rectángulos_Caracteres[Índice_Caracter].Bottom;
                                }
                            }
                        }

                        Rectangle Rectángulo_Máximo_Nonlatin_European = new Rectangle(int.MaxValue, int.MaxValue, int.MinValue, int.MinValue);
                        for (int Índice_Y = 0, Índice_Texto = 0; Índice_Y < 13; Índice_Y++)
                        {
                            for (int Índice_X = 0; Índice_X < 16; Índice_X++, Índice_Texto++)
                            {
                                int Índice_Caracter = (int)Texto_Nonlatin_European[Índice_Texto];
                                if (Matriz_Rectángulos_Caracteres[Índice_Caracter].Width > 0 && Matriz_Rectángulos_Caracteres[Índice_Caracter].Height > 0)
                                {
                                    if (Matriz_Rectángulos_Caracteres[Índice_Caracter].Left < Rectángulo_Máximo_Nonlatin_European.X) Rectángulo_Máximo_Nonlatin_European.X = Matriz_Rectángulos_Caracteres[Índice_Caracter].Left;
                                    if (Matriz_Rectángulos_Caracteres[Índice_Caracter].Top < Rectángulo_Máximo_Nonlatin_European.Y) Rectángulo_Máximo_Nonlatin_European.Y = Matriz_Rectángulos_Caracteres[Índice_Caracter].Top;
                                    if (Matriz_Rectángulos_Caracteres[Índice_Caracter].Right > Rectángulo_Máximo_Nonlatin_European.Width) Rectángulo_Máximo_Nonlatin_European.Width = Matriz_Rectángulos_Caracteres[Índice_Caracter].Right;
                                    if (Matriz_Rectángulos_Caracteres[Índice_Caracter].Bottom > Rectángulo_Máximo_Nonlatin_European.Height) Rectángulo_Máximo_Nonlatin_European.Height = Matriz_Rectángulos_Caracteres[Índice_Caracter].Bottom;
                                }
                            }
                        }

                        Rectángulo_Máximo_Accented = Rectangle.FromLTRB(
                        Math.Min(Math.Min(Rectángulo_Máximo_Accented.X, Rectángulo_Máximo_ASCII.X), Rectángulo_Máximo_Nonlatin_European.X),
                        Math.Min(Math.Min(Rectángulo_Máximo_Accented.Y, Rectángulo_Máximo_ASCII.Y), Rectángulo_Máximo_Nonlatin_European.Y),
                        Math.Max(Math.Max(Rectángulo_Máximo_Accented.Width, Rectángulo_Máximo_ASCII.Width), Rectángulo_Máximo_Nonlatin_European.Width),
                        Math.Max(Math.Max(Rectángulo_Máximo_Accented.Height, Rectángulo_Máximo_ASCII.Height), Rectángulo_Máximo_Nonlatin_European.Height));
                        Rectángulo_Máximo_ASCII = Rectángulo_Máximo_Accented;
                        Rectángulo_Máximo_Nonlatin_European = Rectángulo_Máximo_Accented;

                        //Rectángulo_Máximo_Accented = Rectangle.FromLTRB(Rectángulo_Máximo_Accented.X, Rectángulo_Máximo_Accented.Y, Rectángulo_Máximo_Accented.Width, Rectángulo_Máximo_Accented.Height);
                        //Rectángulo_Máximo_ASCII = Rectangle.FromLTRB(Rectángulo_Máximo_ASCII.X, Rectángulo_Máximo_ASCII.Y, Rectángulo_Máximo_ASCII.Width, Rectángulo_Máximo_ASCII.Height);
                        //Rectángulo_Máximo_Nonlatin_European = Rectangle.FromLTRB(Rectángulo_Máximo_Nonlatin_European.X, Rectángulo_Máximo_Nonlatin_European.Y, Rectángulo_Máximo_Nonlatin_European.Width, Rectángulo_Máximo_Nonlatin_European.Height);



                        int Ancho_Alto_Caracter_Accented = Math.Max(Rectángulo_Máximo_Accented.Width, Rectángulo_Máximo_Accented.Height);
                        Bitmap Imagen_Página_Accented = new Bitmap(Ancho_Alto_Caracter_Accented * 16, Ancho_Alto_Caracter_Accented * 18, PixelFormat.Format32bppArgb);
                        Graphics Pintar_Página_Accented = Graphics.FromImage(Imagen_Página_Accented);
                        Pintar_Página_Accented.CompositingMode = CompositingMode.SourceOver;
                        Pintar_Página_Accented.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar_Página_Accented.InterpolationMode = InterpolationMode.NearestNeighbor;
                        Pintar_Página_Accented.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar_Página_Accented.SmoothingMode = SmoothingMode.HighQuality;
                        Pintar_Página_Accented.TextRenderingHint = TextRenderingHint.AntiAlias;
                        for (int Índice_Y = 0, Índice_Texto = 0; Índice_Y < 18; Índice_Y++)
                        {
                            for (int Índice_X = 0; Índice_X < 16; Índice_X++, Índice_Texto++)
                            {
                                int Índice_Caracter = (int)Texto_Accented[Índice_Texto];
                                int X = Índice_X * Ancho_Alto_Caracter_Accented;
                                int Y = Índice_Y * Ancho_Alto_Caracter_Accented;
                                if (Matriz_Rectángulos_Caracteres[Índice_Caracter].Width > 0 && Matriz_Rectángulos_Caracteres[Índice_Caracter].Height > 0)
                                {
                                    Pintar_Página_Accented.DrawImage(Matriz_Imágenes_Caracteres[Índice_Caracter], new Rectangle(X, Y, Rectángulo_Máximo_Accented.Width, Rectángulo_Máximo_Accented.Height), Rectángulo_Máximo_Accented, GraphicsUnit.Pixel);
                                }
                            }
                        }
                        Pintar_Página_Accented.Dispose();
                        Pintar_Página_Accented = null;
                        Lector_Memoria = new MemoryStream(); // Save the image as PNG in the memory.
                        Imagen_Página_Accented.Save(Lector_Memoria, ImageFormat.Png);
                        Matriz_Bytes = Lector_Memoria.ToArray(); // Get the bytes of the saved image.
                        Lector_Memoria.Close();
                        Lector_Memoria.Dispose();
                        Lector_Memoria = null;
                        Imagen_Página_Accented.Dispose();
                        Imagen_Página_Accented = null;
                        Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/textures/font/accented.png"));
                        Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                        Archivo_ZIP.CloseEntry();
                        Matriz_Bytes = null;

                        //Matriz_Imágenes_Caracteres[103].Save(Program.Obtener_Ruta_Temporal_Escritorio() + ", " + Matriz_Rectángulos_Caracteres[103].ToString() + ", " + Rectángulo_Máximo_ASCII.ToString() + ".png", ImageFormat.Png);
                        int Ancho_Alto_Caracter_ASCII = Math.Max(Rectángulo_Máximo_ASCII.Width, Rectángulo_Máximo_ASCII.Height);
                        Bitmap Imagen_Página_ASCII = new Bitmap(Ancho_Alto_Caracter_ASCII * 16, Ancho_Alto_Caracter_ASCII * 16, PixelFormat.Format32bppArgb);
                        Graphics Pintar_Página_ASCII = Graphics.FromImage(Imagen_Página_ASCII);
                        Pintar_Página_ASCII.CompositingMode = CompositingMode.SourceOver;
                        Pintar_Página_ASCII.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar_Página_ASCII.InterpolationMode = InterpolationMode.NearestNeighbor;
                        Pintar_Página_ASCII.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar_Página_ASCII.SmoothingMode = SmoothingMode.HighQuality;
                        Pintar_Página_ASCII.TextRenderingHint = TextRenderingHint.AntiAlias;
                        for (int Índice_Y = 0, Índice_Texto = 0; Índice_Y < 16; Índice_Y++)
                        {
                            for (int Índice_X = 0; Índice_X < 16; Índice_X++, Índice_Texto++)
                            {
                                int Índice_Caracter = (int)Texto_ASCII[Índice_Texto];
                                int X = Índice_X * Ancho_Alto_Caracter_ASCII;
                                int Y = Índice_Y * Ancho_Alto_Caracter_ASCII;
                                if (Matriz_Rectángulos_Caracteres[Índice_Caracter].Width > 0 && Matriz_Rectángulos_Caracteres[Índice_Caracter].Height > 0)
                                {
                                    Pintar_Página_ASCII.DrawImage(Matriz_Imágenes_Caracteres[Índice_Caracter], new Rectangle(X, Y, Rectángulo_Máximo_ASCII.Width, Rectángulo_Máximo_ASCII.Height), Rectángulo_Máximo_ASCII, GraphicsUnit.Pixel);
                                }
                            }
                        }
                        Pintar_Página_ASCII.Dispose();
                        Pintar_Página_ASCII = null;
                        Lector_Memoria = new MemoryStream(); // Save the image as PNG in the memory.
                        Imagen_Página_ASCII.Save(Lector_Memoria, ImageFormat.Png);
                        Matriz_Bytes = Lector_Memoria.ToArray(); // Get the bytes of the saved image.
                        Lector_Memoria.Close();
                        Lector_Memoria.Dispose();
                        Lector_Memoria = null;
                        Imagen_Página_ASCII.Dispose();
                        Imagen_Página_ASCII = null;
                        Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/textures/font/ascii.png"));
                        Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                        Archivo_ZIP.CloseEntry();
                        Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/textures/font/ascii_sga.png"));
                        Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                        Archivo_ZIP.CloseEntry();
                        Matriz_Bytes = null;

                        int Ancho_Alto_Caracter_Nonlatin_European = Math.Max(Rectángulo_Máximo_Nonlatin_European.Width, Rectángulo_Máximo_Nonlatin_European.Height);
                        Bitmap Imagen_Página_Nonlatin_European = new Bitmap(Ancho_Alto_Caracter_Nonlatin_European * 16, Ancho_Alto_Caracter_Nonlatin_European * 13, PixelFormat.Format32bppArgb);
                        Graphics Pintar_Página_Nonlatin_European = Graphics.FromImage(Imagen_Página_Nonlatin_European);
                        Pintar_Página_Nonlatin_European.CompositingMode = CompositingMode.SourceOver;
                        Pintar_Página_Nonlatin_European.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar_Página_Nonlatin_European.InterpolationMode = InterpolationMode.NearestNeighbor;
                        Pintar_Página_Nonlatin_European.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar_Página_Nonlatin_European.SmoothingMode = SmoothingMode.HighQuality;
                        Pintar_Página_Nonlatin_European.TextRenderingHint = TextRenderingHint.AntiAlias;
                        for (int Índice_Y = 0, Índice_Texto = 0; Índice_Y < 13; Índice_Y++)
                        {
                            for (int Índice_X = 0; Índice_X < 16; Índice_X++, Índice_Texto++)
                            {
                                int Índice_Caracter = (int)Texto_Nonlatin_European[Índice_Texto];
                                int X = Índice_X * Ancho_Alto_Caracter_Nonlatin_European;
                                int Y = Índice_Y * Ancho_Alto_Caracter_Nonlatin_European;
                                if (Matriz_Rectángulos_Caracteres[Índice_Caracter].Width > 0 && Matriz_Rectángulos_Caracteres[Índice_Caracter].Height > 0)
                                {
                                    Pintar_Página_Nonlatin_European.DrawImage(Matriz_Imágenes_Caracteres[Índice_Caracter], new Rectangle(X, Y, Rectángulo_Máximo_Nonlatin_European.Width, Rectángulo_Máximo_Nonlatin_European.Height), Rectángulo_Máximo_Nonlatin_European, GraphicsUnit.Pixel);
                                }
                            }
                        }
                        Pintar_Página_Nonlatin_European.Dispose();
                        Pintar_Página_Nonlatin_European = null;
                        Lector_Memoria = new MemoryStream(); // Save the image as PNG in the memory.
                        Imagen_Página_Nonlatin_European.Save(Lector_Memoria, ImageFormat.Png);
                        Matriz_Bytes = Lector_Memoria.ToArray(); // Get the bytes of the saved image.
                        Lector_Memoria.Close();
                        Lector_Memoria.Dispose();
                        Lector_Memoria = null;
                        Imagen_Página_Nonlatin_European.Dispose();
                        Imagen_Página_Nonlatin_European = null;
                        Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/textures/font/nonlatin_european.png"));
                        Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                        Archivo_ZIP.CloseEntry();
                        Matriz_Bytes = null;

                        for (int Índice_Caracter = 0, Índice_Página  = 0; Índice_Caracter < 65536; Índice_Página++)
                        {
                            if (!Lista_Páginas_Ignoradas.Contains(Índice_Página)) // Skip the pages ignored by Minecraft.
                            {
                                Bitmap Imagen_Página = new Bitmap(Ancho_Alto_Caracter * 16, Ancho_Alto_Caracter * 16, PixelFormat.Format32bppArgb);
                                Graphics Pintar_Página = Graphics.FromImage(Imagen_Página);
                                Pintar_Página.CompositingMode = CompositingMode.SourceOver;
                                Pintar_Página.CompositingQuality = CompositingQuality.HighQuality;
                                Pintar_Página.InterpolationMode = InterpolationMode.NearestNeighbor;
                                Pintar_Página.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                Pintar_Página.SmoothingMode = SmoothingMode.HighQuality;
                                Pintar_Página.TextRenderingHint = TextRenderingHint.AntiAlias;
                                for (int Índice_Y = 0; Índice_Y < 16; Índice_Y++)
                                {
                                    for (int Índice_X = 0; Índice_X < 16; Índice_X++, Índice_Caracter++)
                                    {
                                        int X = Índice_X * Ancho_Alto_Caracter;
                                        int Y = Índice_Y * Ancho_Alto_Caracter;
                                        if (Matriz_Rectángulos_Caracteres[Índice_Caracter].Width > 0 && Matriz_Rectángulos_Caracteres[Índice_Caracter].Height > 0)
                                        {
                                            Pintar_Página.DrawImage(Matriz_Imágenes_Caracteres[Índice_Caracter], new Rectangle(X, Y, Rectángulo_Máximo.Width, Rectángulo_Máximo.Height), Rectángulo_Máximo, GraphicsUnit.Pixel);
                                        }
                                    }
                                }
                                Pintar_Página.Dispose();
                                Pintar_Página = null;
                                string Texto_Página = Convert.ToString(Índice_Página, 16).ToLowerInvariant();
                                while (Texto_Página.Length < 2) Texto_Página = '0' + Texto_Página;

                                Lector_Memoria = new MemoryStream(); // Save the image as PNG in the memory.
                                Imagen_Página.Save(Lector_Memoria, ImageFormat.Png);
                                Matriz_Bytes = Lector_Memoria.ToArray(); // Get the bytes of the saved image.
                                Lector_Memoria.Close();
                                Lector_Memoria.Dispose();
                                Lector_Memoria = null;
                                Imagen_Página.Dispose();
                                Imagen_Página = null;

                                Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/textures/font/unicode_page_" + Texto_Página + ".png"));
                                Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                Archivo_ZIP.CloseEntry();
                                Matriz_Bytes = null;
                            }
                        }

                        Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/font/alt.json"));
                        Matriz_Bytes = Encoding.UTF8.GetBytes(
                        "{\r\n" +
                        "    \"providers\": [\r\n" +
                        "        {\r\n" +
                        "            \"type\": \"bitmap\",\r\n" +
                        "            \"file\": \"minecraft:font/ascii_sga.png\",\r\n" +
                        "            \"ascent\": " + Rectángulo_Máximo_ASCII.Width.ToString() + ",\r\n" +
                        "            \"chars\": [\r\n" +
                        "                \"\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\",\r\n" +
                        "                \"\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\",\r\n" +
                        "                \"\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\",\r\n" +
                        "                \"\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\",\r\n" +
                        "                \"\\u0000\\u0041\\u0042\\u0043\\u0044\\u0045\\u0046\\u0047\\u0048\\u0049\\u004A\\u004B\\u004C\\u004D\\u004E\\u004F\",\r\n" +
                        "                \"\\u0050\\u0051\\u0052\\u0053\\u0054\\u0055\\u0056\\u0057\\u0058\\u0059\\u005A\\u0000\\u0000\\u0000\\u0000\\u0000\",\r\n" +
                        "                \"\\u0000\\u0061\\u0062\\u0063\\u0064\\u0065\\u0066\\u0067\\u0068\\u0069\\u006A\\u006B\\u006C\\u006D\\u006E\\u006F\",\r\n" +
                        "                \"\\u0070\\u0071\\u0072\\u0073\\u0074\\u0075\\u0076\\u0077\\u0078\\u0079\\u007A\\u0000\\u0000\\u0000\\u0000\\u0000\",\r\n" +
                        "                \"\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\",\r\n" +
                        "                \"\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\",\r\n" +
                        "                \"\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\",\r\n" +
                        "                \"\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\",\r\n" +
                        "                \"\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\",\r\n" +
                        "                \"\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\",\r\n" +
                        "                \"\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\",\r\n" +
                        "                \"\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\"\r\n" +
                        "            ]\r\n" +
                        "        }\r\n" +
                        "    ]\r\n" +
                        "}");
                        Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                        Archivo_ZIP.CloseEntry();
                        Matriz_Bytes = null;

                        Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/font/default.json"));
                        Matriz_Bytes = Encoding.UTF8.GetBytes(
                        "{\r\n" +
                        "    \"providers\": [\r\n" +
                        "        {\r\n" +
                        "            \"type\": \"bitmap\",\r\n" +
                        "            \"file\": \"minecraft:font/nonlatin_european.png\",\r\n" +
                        "            \"ascent\": " + Rectángulo_Máximo_Nonlatin_European.Width.ToString() + ",\r\n" +
                        "            \"chars\": [\r\n" +
                        "                \"\\u00a1\\u2030\\u00ad\\u00b7\\u20b4\\u2260\\u00bf\\u00d7\\u00d8\\u00de\\u00df\\u00f0\\u00f8\\u00fe\\u0391\\u0392\",\r\n" +
                        "                \"\\u0393\\u0394\\u0395\\u0396\\u0397\\u0398\\u0399\\u039a\\u039b\\u039c\\u039d\\u039e\\u039f\\u03a0\\u03a1\\u03a3\",\r\n" +
                        "                \"\\u03a4\\u03a5\\u03a6\\u03a7\\u03a8\\u03a9\\u03b1\\u03b2\\u03b3\\u03b4\\u03b5\\u03b6\\u03b7\\u03b8\\u03b9\\u03ba\",\r\n" +
                        "                \"\\u03bb\\u03bc\\u03bd\\u03be\\u03bf\\u03c0\\u03c1\\u03c2\\u03c3\\u03c4\\u03c5\\u03c6\\u03c7\\u03c8\\u03c9\\u0402\",\r\n" +
                        "                \"\\u0405\\u0406\\u0408\\u0409\\u040a\\u040b\\u0410\\u0411\\u0412\\u0413\\u0414\\u0415\\u0416\\u0417\\u0418\\u041a\",\r\n" +
                        "                \"\\u041b\\u041c\\u041d\\u041e\\u041f\\u0420\\u0421\\u0422\\u0423\\u0424\\u0425\\u0426\\u0427\\u0428\\u0429\\u042a\",\r\n" +
                        "                \"\\u042b\\u042c\\u042d\\u042e\\u042f\\u0430\\u0431\\u0432\\u0433\\u0434\\u0435\\u0436\\u0437\\u0438\\u043a\\u043b\",\r\n" +
                        "                \"\\u043c\\u043d\\u043e\\u043f\\u0440\\u0441\\u0442\\u0443\\u0444\\u0445\\u0446\\u0447\\u0448\\u0449\\u044a\\u044b\",\r\n" +
                        "                \"\\u044c\\u044d\\u044e\\u044f\\u0454\\u0455\\u0456\\u0458\\u0459\\u045a\\u2013\\u2014\\u2018\\u2019\\u201c\\u201d\",\r\n" +
                        "                \"\\u201e\\u2026\\u204a\\u2190\\u2191\\u2192\\u2193\\u21c4\\uff0b\\u018f\\u0259\\u025b\\u026a\\u04ae\\u04af\\u04e8\",\r\n" +
                        "                \"\\u04e9\\u02bb\\u02cc\\u037e\\u0138\\u1e9e\\u00df\\u20bd\\u20ac\\u0462\\u0463\\u0474\\u0475\\u0406\\u0472\\u0473\",\r\n" +
                        "                \"\\u2070\\u00b9\\u00b3\\u2074\\u2075\\u2076\\u2077\\u2078\\u2079\\u207a\\u207b\\u207c\\u207d\\u207e\\u2071\\u2122\",\r\n" +
                        "                \"\\u0294\\u0295\\u29c8\\u2694\\u2620\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\"\r\n" +
                        "            ]\r\n" +
                        "        },\r\n" +
                        "        {\r\n" +
                        "            \"type\": \"bitmap\",\r\n" +
                        "            \"file\": \"minecraft:font/accented.png\",\r\n" +
                        "            \"height\": " + Rectángulo_Máximo_Accented.Height.ToString() + ",\r\n" +
                        "            \"ascent\": " + Rectángulo_Máximo_Accented.Width.ToString() + ",\r\n" +
                        "            \"chars\": [\r\n" +
                        "                \"\\u00c0\\u00c1\\u00c2\\u00c3\\u00c4\\u00c5\\u00c6\\u00c7\\u00c8\\u00c9\\u00ca\\u00cb\\u00cc\\u00cd\\u00ce\\u00cf\",\r\n" +
                        "                \"\\u00d0\\u00d1\\u00d2\\u00d3\\u00d4\\u00d5\\u00d6\\u00d9\\u00da\\u00db\\u00dc\\u00dd\\u00e0\\u00e1\\u00e2\\u00e3\",\r\n" +
                        "                \"\\u00e4\\u00e5\\u00e6\\u00e7\\u00ec\\u00ed\\u00ee\\u00ef\\u00f1\\u00f2\\u00f3\\u00f4\\u00f5\\u00f6\\u00f9\\u00fa\",\r\n" +
                        "                \"\\u00fb\\u00fc\\u00fd\\u00ff\\u0100\\u0101\\u0102\\u0103\\u0104\\u0105\\u0106\\u0107\\u0108\\u0109\\u010a\\u010b\",\r\n" +
                        "                \"\\u010c\\u010d\\u010e\\u010f\\u0110\\u0111\\u0112\\u0113\\u0114\\u0115\\u0116\\u0117\\u0118\\u0119\\u011a\\u011b\",\r\n" +
                        "                \"\\u011c\\u011d\\u1e20\\u1e21\\u011e\\u011f\\u0120\\u0121\\u0122\\u0123\\u0124\\u0125\\u0126\\u0127\\u0128\\u0129\",\r\n" +
                        "                \"\\u012a\\u012b\\u012c\\u012d\\u012e\\u012f\\u0130\\u0131\\u0134\\u0135\\u0136\\u0137\\u0139\\u013a\\u013b\\u013c\",\r\n" +
                        "                \"\\u013d\\u013e\\u013f\\u0140\\u0141\\u0142\\u0143\\u0144\\u0145\\u0146\\u0147\\u0148\\u014a\\u014b\\u014c\\u014d\",\r\n" +
                        "                \"\\u014e\\u014f\\u0150\\u0151\\u0152\\u0153\\u0154\\u0155\\u0156\\u0157\\u0158\\u0159\\u015a\\u015b\\u015c\\u015d\",\r\n" +
                        "                \"\\u015e\\u015f\\u0160\\u0161\\u0162\\u0163\\u0164\\u0165\\u0166\\u0167\\u0168\\u0169\\u016a\\u016b\\u016c\\u016d\",\r\n" +
                        "                \"\\u016e\\u016f\\u0170\\u0171\\u0172\\u0173\\u0174\\u0175\\u0176\\u0177\\u0178\\u0179\\u017a\\u017b\\u017c\\u017d\",\r\n" +
                        "                \"\\u017e\\u01fc\\u01fd\\u01fe\\u01ff\\u0218\\u0219\\u021a\\u021b\\u0386\\u0388\\u0389\\u038a\\u038c\\u038e\\u038f\",\r\n" +
                        "                \"\\u0390\\u03aa\\u03ab\\u03ac\\u03ad\\u03ae\\u03af\\u03b0\\u03ca\\u03cb\\u03cc\\u03cd\\u03ce\\u0400\\u0401\\u0403\",\r\n" +
                        "                \"\\u0407\\u040c\\u040d\\u040e\\u0419\\u0439\\u0450\\u0451\\u0452\\u0453\\u0457\\u045b\\u045c\\u045d\\u045e\\u045f\",\r\n" +
                        "                \"\\u0490\\u0491\\u1e02\\u1e03\\u1e0a\\u1e0b\\u1e1e\\u1e1f\\u1e22\\u1e23\\u1e30\\u1e31\\u1e40\\u1e41\\u1e56\\u1e57\",\r\n" +
                        "                \"\\u1e60\\u1e61\\u1e6a\\u1e6b\\u1e80\\u1e81\\u1e82\\u1e83\\u1e84\\u1e85\\u1ef2\\u1ef3\\u00e8\\u00e9\\u00ea\\u00eb\",\r\n" +
                        "                \"\\u0149\\u01e7\\u01eb\\u040f\\u1e0d\\u1e25\\u1e5b\\u1e6d\\u1e92\\u1eca\\u1ecb\\u1ecc\\u1ecd\\u1ee4\\u1ee5\\u2116\",\r\n" +
                        "                \"\\u0207\\u0194\\u0263\\u0283\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\"\r\n" +
                        "            ]\r\n" +
                        "        },\r\n" +
                        "        {\r\n" +
                        "            \"type\": \"bitmap\",\r\n" +
                        "            \"file\": \"minecraft:font/ascii.png\",\r\n" +
                        "            \"ascent\": " + Rectángulo_Máximo_ASCII.Width.ToString() + ",\r\n" +
                        "            \"chars\": [\r\n" +
                        "                \"\\u00c0\\u00c1\\u00c2\\u00c8\\u00ca\\u00cb\\u00cd\\u00d3\\u00d4\\u00d5\\u00da\\u00df\\u00e3\\u00f5\\u011f\\u0130\",\r\n" +
                        "                \"\\u0131\\u0152\\u0153\\u015e\\u015f\\u0174\\u0175\\u017e\\u0207\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\",\r\n" +
                        "                \"\\u0020\\u0021\\\"\\u0023\\u0024\\u0025\\u0026\\u0027\\u0028\\u0029\\u002a\\u002b\\u002c\\u002d\\u002e\\u002f\",\r\n" +
                        "                \"\\u0030\\u0031\\u0032\\u0033\\u0034\\u0035\\u0036\\u0037\\u0038\\u0039\\u003a\\u003b\\u003c\\u003d\\u003e\\u003f\",\r\n" +
                        "                \"\\u0040\\u0041\\u0042\\u0043\\u0044\\u0045\\u0046\\u0047\\u0048\\u0049\\u004a\\u004b\\u004c\\u004d\\u004e\\u004f\",\r\n" +
                        "                \"\\u0050\\u0051\\u0052\\u0053\\u0054\\u0055\\u0056\\u0057\\u0058\\u0059\\u005a\\u005b\\\\\\u005d\\u005e\\u005f\",\r\n" +
                        "                \"\\u0060\\u0061\\u0062\\u0063\\u0064\\u0065\\u0066\\u0067\\u0068\\u0069\\u006a\\u006b\\u006c\\u006d\\u006e\\u006f\",\r\n" +
                        "                \"\\u0070\\u0071\\u0072\\u0073\\u0074\\u0075\\u0076\\u0077\\u0078\\u0079\\u007a\\u007b\\u007c\\u007d\\u007e\\u0000\",\r\n" +
                        "                \"\\u00c7\\u00fc\\u00e9\\u00e2\\u00e4\\u00e0\\u00e5\\u00e7\\u00ea\\u00eb\\u00e8\\u00ef\\u00ee\\u00ec\\u00c4\\u00c5\",\r\n" +
                        "                \"\\u00c9\\u00e6\\u00c6\\u00f4\\u00f6\\u00f2\\u00fb\\u00f9\\u00ff\\u00d6\\u00dc\\u00f8\\u00a3\\u00d8\\u00d7\\u0192\",\r\n" +
                        "                \"\\u00e1\\u00ed\\u00f3\\u00fa\\u00f1\\u00d1\\u00aa\\u00ba\\u00bf\\u00ae\\u00ac\\u00bd\\u00bc\\u00a1\\u00ab\\u00bb\",\r\n" +
                        "                \"\\u2591\\u2592\\u2593\\u2502\\u2524\\u2561\\u2562\\u2556\\u2555\\u2563\\u2551\\u2557\\u255d\\u255c\\u255b\\u2510\",\r\n" +
                        "                \"\\u2514\\u2534\\u252c\\u251c\\u2500\\u253c\\u255e\\u255f\\u255a\\u2554\\u2569\\u2566\\u2560\\u2550\\u256c\\u2567\",\r\n" +
                        "                \"\\u2568\\u2564\\u2565\\u2559\\u2558\\u2552\\u2553\\u256b\\u256a\\u2518\\u250c\\u2588\\u2584\\u258c\\u2590\\u2580\",\r\n" +
                        "                \"\\u03b1\\u03b2\\u0393\\u03c0\\u03a3\\u03c3\\u03bc\\u03c4\\u03a6\\u0398\\u03a9\\u03b4\\u221e\\u2205\\u2208\\u2229\",\r\n" +
                        "                \"\\u2261\\u00b1\\u2265\\u2264\\u2320\\u2321\\u00f7\\u2248\\u00b0\\u2219\\u00b7\\u221a\\u207f\\u00b2\\u25a0\\u0000\"\r\n" +
                        "            ]\r\n" +
                        "        },\r\n" +
                        "        {\r\n" +
                        "            \"type\": \"legacy_unicode\",\r\n" +
                        "            \"sizes\": \"minecraft:font/glyph_sizes.bin\",\r\n" +
                        "            \"template\": \"minecraft:font/unicode_page_%s.png\"\r\n" +
                        "        }\r\n" +
                        "    ]\r\n" +
                        "}");
                        Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                        Archivo_ZIP.CloseEntry();
                        Matriz_Bytes = null;

                        byte[] Matriz_Bytes_Anchos = new byte[65536];
                        for (int Índice_Caracter = 0; Índice_Caracter < 65536; Índice_Caracter++)
                        {
                            Matriz_Bytes_Anchos[Índice_Caracter] = (byte)Math.Min(Matriz_Rectángulos_Caracteres[Índice_Caracter].Width, 255);
                        }
                        Archivo_ZIP.PutNextEntry(new ZipEntry("assets/minecraft/font/glyph_sizes.bin"));
                        Archivo_ZIP.Write(Matriz_Bytes_Anchos, 0, Matriz_Bytes_Anchos.Length);
                        Archivo_ZIP.CloseEntry();
                        Matriz_Bytes_Anchos = null;

                        // package net.minecraft.util.text:
                        /*BLACK("BLACK", '0', 0),
                        DARK_BLUE("DARK_BLUE", '1', 1),
                        DARK_GREEN("DARK_GREEN", '2', 2),
                        DARK_AQUA("DARK_AQUA", '3', 3),
                        DARK_RED("DARK_RED", '4', 4),
                        DARK_PURPLE("DARK_PURPLE", '5', 5),
                        GOLD("GOLD", '6', 6),
                        GRAY("GRAY", '7', 7),
                        DARK_GRAY("DARK_GRAY", '8', 8),
                        BLUE("BLUE", '9', 9),
                        GREEN("GREEN", 'a', 10),
                        AQUA("AQUA", 'b', 11),
                        RED("RED", 'c', 12),
                        LIGHT_PURPLE("LIGHT_PURPLE", 'd', 13),
                        YELLOW("YELLOW", 'e', 14),
                        WHITE("WHITE", 'f', 15),
                        OBFUSCATED("OBFUSCATED", 'k', true),
                        BOLD("BOLD", 'l', true),
                        STRIKETHROUGH("STRIKETHROUGH", 'm', true),
                        UNDERLINE("UNDERLINE", 'n', true),
                        ITALIC("ITALIC", 'o', true),
                        RESET("RESET", 'r', -1);*/

                        Archivo_ZIP.PutNextEntry(new ZipEntry("pack.mcmeta"));
                        Matriz_Bytes = Encoding.UTF8.GetBytes("{\r\n  \"pack\": {\r\n    \"pack_format\": " + Formato_Pack.ToString() + ",\r\n    \"description\": \"§fFont§r §c" + Nombre + "§r §f[Size " + Tamaño.ToString() + "]§r\\n§fBy:§r §9" + Program.Texto_Usuario + "§r\"\r\n  }\r\n}\r\n");
                        Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                        Archivo_ZIP.CloseEntry();
                        Matriz_Bytes = null;

                        Bitmap Imagen = Resources.Codificación; //Program.Obtener_Imagen_Miniatura(Resources.Codificación, 64, 64, true, false, CheckState.Checked);
                        Lector_Memoria = new MemoryStream(); // Save the image as PNG in the memory.
                        Imagen.Save(Lector_Memoria, ImageFormat.Png);
                        Matriz_Bytes = Lector_Memoria.ToArray(); // Get the bytes of the saved image.
                        Lector_Memoria.Close();
                        Lector_Memoria.Dispose();
                        Lector_Memoria = null;
                        Imagen.Dispose();
                        Imagen = null;

                        Archivo_ZIP.PutNextEntry(new ZipEntry("pack.png"));
                        Archivo_ZIP.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                        Archivo_ZIP.CloseEntry();
                        Matriz_Bytes = null;

                        Archivo_ZIP.Finish();
                        Archivo_ZIP.Close();
                        Archivo_ZIP.Dispose();
                        Archivo_ZIP = null;
                        Matriz_Bytes = null;
                        SystemSounds.Asterisk.Play();
                        GC.Collect();
                        GC.GetTotalMemory(true);
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
