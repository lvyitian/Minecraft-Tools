using Minecraft_Tools.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Analizador_Matemático_Multidimensional : Form
    {
        public Ventana_Analizador_Matemático_Multidimensional()
        {
            InitializeComponent();
        }

        internal enum Filtros : int
        {
            Original = 0,
            Negative,
            Bit_inversion_in_base_2,
            Bit_inversion_in_base_4,
            Bit_inversion_in_base_16,
            Bit_inversion_in_bases_2_and_4,
            Bit_inversion_in_bases_2_and_16,
            Bit_inversion_in_bases_4_and_16,
            Bit_inversion_in_bases_2___4_and_16,
        }

        internal int[][] Matriz_Guarismos_Invertidos_Base = null;
        internal Dictionary<int, int[][]> Diccionario_Bases_Longitudes_Valores = new Dictionary<int, int[][]>();

        internal static Filtros Filtro = Filtros.Bit_inversion_in_bases_2_and_16;
        //internal static byte[] Matriz_Bytes_Personalizado_Cifrado = null;
        //internal static byte[] Matriz_Bytes_Personalizado_Descifrado = null;
        //internal static byte[] Matriz_Bytes_Personalizado_2_Cifrado = null;
        //internal static byte[] Matriz_Bytes_Personalizado_2_Descifrado = null;
        internal Label[] Matriz_Etiquetas = null;
        internal Label[][] Matriz_Etiquetas_XY = null;
        internal static int[] Matriz_Valores_Actual = null;
        internal bool Tecla_Alt_Presionada = false;
        internal bool Tecla_Control_Presionada = false;
        internal bool Tecla_Mayúsculas_Presionada = false;

        internal readonly string Texto_Título = "Multidimensional Mathematical Analyzer by Jupisoft for " + Program.Texto_Usuario;
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

        internal static int Variable_Dibujo = 0;

        internal static int Límite_Valores = 256; // 256; // 640;
        internal static int Límite_Valores_1 = Límite_Valores - 1;
        internal static int Límite_Real_Valores = Límite_Valores;
        internal static int Límite_Real_Valores_1 = Límite_Valores_1;
        internal static int Y = 640 - Límite_Valores;

        private void Ventana_Analizador_Matemático_Multidimensional_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título  + " - [Try the bases 2, 4 and 16 mixed at 256 to get fractals]";
                //this.WindowState = FormWindowState.Maximized;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;

                Matriz_Etiquetas = new Label[256];
                Matriz_Etiquetas_XY = new Label[16][];
                for (int X = 0, Índice = 0; X < 16; X++)
                {
                    Matriz_Etiquetas_XY[X] = new Label[16];
                    for (int Y = 0; Y < 16; Y++, Índice++)
                    {
                        Label Etiqueta = new Label();
                        //Etiqueta.AutoEllipsis = true;
                        Etiqueta.AutoSize = false;
                        Etiqueta.BackColor = Color.FromArgb(176, 255, 176);
                        Etiqueta.BorderStyle = BorderStyle.FixedSingle;
                        //Etiqueta.Font = new Font(Etiqueta.Font.FontFamily, 6f); // 8.25f.
                        Etiqueta.Font = new Font("Arial", 6.25f); // 8.25f.
                        Etiqueta.Location = new Point(X * 22,  Y * 22);
                        Etiqueta.Margin = Padding.Empty;
                        Etiqueta.Name = "Etiqueta_" + Índice.ToString();
                        Etiqueta.Padding = Padding.Empty;
                        Etiqueta.Size = new Size(23, 23);
                        Etiqueta.TabIndex = Índice;
                        Etiqueta.Text = Índice.ToString();
                        Etiqueta.TextAlign = ContentAlignment.MiddleCenter;
                        Etiqueta.UseMnemonic = false;
                        //Etiqueta.UseCompatibleTextRendering = true;
                        //Tabla_Principal.Controls.Add(Etiqueta, X, Y);
                        this.Controls.Add(Etiqueta);
                        //Etiqueta.Dock = DockStyle.Fill;
                        Matriz_Etiquetas[Índice] = Etiqueta;
                        Matriz_Etiquetas_XY[X][Y] = Etiqueta;
                    }
                }
                /*//Tabla_Principal.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                int Ancho_Alto = 21;
                Tabla_Principal.Size = new Size((Ancho_Alto * 16) + 17, (Ancho_Alto * 16) + 17);
                for (int X = 0; X < 16; X++)
                {
                    Tabla_Principal.ColumnStyles[X].Width = Ancho_Alto;
                    // .NET bug, why it gains 1 pixel wide after adding the labels at the end?
                }
                for (int Y = 0; Y < 16; Y++)
                {
                    Tabla_Principal.RowStyles[Y].Height = Ancho_Alto;
                }*/

                /*string[] Matriz_Nombres = Enum.GetNames(typeof(Filtros));
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    foreach (string Nombre in Matriz_Nombres)
                    {
                        Combo_Filtro.Items.Add(Nombre.Replace("__", ",").Replace("_", " "));
                    }
                }
                Matriz_Nombres = null;
                //if (Combo_Filtro.Items.Count > 0) Combo_Filtro.SelectedIndex = 0;
                if (Combo_Filtro.Items.Count > 0)
                {
                    Combo_Filtro.SelectedIndex = (int)Filtros.Bit_inversion_in_bases_2_and_16;
                }*/

                Reiniciar_Valores();
                ComboBox_Dibujo.SelectedIndex = Variable_Dibujo;

                CheckedListBox_Bases.SetItemChecked(0, true);
                CheckedListBox_Bases.SetItemChecked(14, true);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        // Editor de Imágenes:
        // internal static Byte[] Obtener_Matriz_Reasignación(Reasignaciones Reasignación)
        /*if (Reasignación == Reasignaciones.Invertir_Bits)
        {
            bool[] Matriz_Booleans = new bool[8];
            for (int Contador = 0; Contador < 256; Contador++)
            {
                new BitArray(new Byte[] { (Byte)Contador }).CopyTo(Matriz_Booleans, 0);
                Array.Reverse(Matriz_Booleans);
                new BitArray(Matriz_Booleans).CopyTo(Matriz_Reasignación, Contador);
            }
            return Matriz_Reasignación;
        }*/
        // Inversión de guarismos en base X con proporción de raíz cuadrada.

        private void Ventana_Analizador_Matemático_Multidimensional_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
                /*string Texto = null;
                for (double Valor = 1d; Valor <= 529d; Valor++)
                {
                    double Sq = Math.Sqrt(Valor);
                    if (Sq - Math.Truncate(Sq) == 0d)
                    {
                        Texto += Valor.ToString() + " = " + Sq.ToString() + ",\r\n";
                    }
                }
                if (!string.IsNullOrEmpty(Texto))
                {
                    MessageBox.Show(this, Texto);
                }*/
                //Abc();

                // The biggest square image that can be created with .NET has 26.754 x 26.754 pixels.

                // Just an experiment with maximum image sizes.
                //Bitmap Imagen = new Bitmap(65536, 65536, PixelFormat.Format24bppRgb); // Error.
                //Bitmap Imagen = new Bitmap(32768, 32768, PixelFormat.Format24bppRgb); // Error.
                //Bitmap Imagen = new Bitmap(30000, 30000, PixelFormat.Format24bppRgb); // Error.
                //Bitmap Imagen = new Bitmap(28000, 28000, PixelFormat.Format24bppRgb); // Error.
                //Bitmap Imagen = new Bitmap(27000, 27000, PixelFormat.Format24bppRgb); // Error.
                //Bitmap Imagen = new Bitmap(26900, 26900, PixelFormat.Format24bppRgb); // Error.
                //Bitmap Imagen = new Bitmap(26850, 26850, PixelFormat.Format24bppRgb); // Error.
                //Bitmap Imagen = new Bitmap(26800, 26800, PixelFormat.Format24bppRgb); // Error.
                //Bitmap Imagen = new Bitmap(26775, 26775, PixelFormat.Format24bppRgb); // Error.
                //Bitmap Imagen = new Bitmap(26760, 26760, PixelFormat.Format24bppRgb); // Error.
                //Bitmap Imagen = new Bitmap(26755, 26755, PixelFormat.Format24bppRgb); // Error.
                //Bitmap Imagen = new Bitmap(26754, 26754, PixelFormat.Format24bppRgb); // OK: Maximum.
                //Bitmap Imagen = new Bitmap(2672, 26752, PixelFormat.Format24bppRgb); // OK.
                //Bitmap Imagen = new Bitmap(26750, 26750, PixelFormat.Format24bppRgb); // OK.
                //Bitmap Imagen = new Bitmap(26500, 26500, PixelFormat.Format24bppRgb); // OK.
                //Bitmap Imagen = new Bitmap(26000, 26000, PixelFormat.Format24bppRgb); // OK.
                //Bitmap Imagen = new Bitmap(16384, 16384, PixelFormat.Format24bppRgb); // OK.
                Exportar_Imagen_Máxima(2, true, false, true, false);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Analizador_Matemático_Multidimensional_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Analizador_Matemático_Multidimensional_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Analizador_Matemático_Multidimensional_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Analizador_Matemático_Multidimensional_DragDrop(object sender, DragEventArgs e)
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

        private void Ventana_Analizador_Matemático_Multidimensional_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Analizador_Matemático_Multidimensional_KeyDown(object sender, KeyEventArgs e)
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
                if (Program.Edición_Aplicación != CheckState.Indeterminate)
                {
                    Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                    Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.Main_window;
                    Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                    Ventana.ShowDialog(this);
                    Ventana.Dispose();
                    Ventana = null;
                }
                else MessageBox.Show(this, "Esta ventana es capaz de combinar números en múltiples bases y dimensiones, generando en el proceso figuras geométricas complejas (incluyendo círculos perfectos), aunque sin razón aparente en la mayoría de casos.\r\n\r\nLo \"único\" que hace el programa es coger por ejemplo números del 0 (cero) al 255, lo que sería el rango de un byte (8 bits), convertirlos a base 2 (binaria), 4 o 16 (hexadecimal), poner ceros a la izquierda hasta alcanzar la longitud deseada de 256 valores (255 en la práctica, pues empieza en cero), así en base la longitud de cada número será de 8 guarismos (ceros y unos) y en hexadeciaml será de 2 guarismos (de cero a 15, letra F), una vez alcanzada dicha longitud, simplemente invierte horizontalmente esos guarismos (de izuierda a derecha), o sea que 10010011 (147) pasará a ser 11001001 (201). Y eso de por sí ya produce una cálculo inteligente capaz de codificar figuras geométricas automáticas de forma inteligente y aparentemente inexplicable.\r\n\r\nA continuación se dividen los 256 números en su raíz cuadrada (16) y se muestran así en 2 dimensiones dentro de una cuadrícula de 16 x 16, donde los números que tras ser invertidos siguen siendo los mismos se muestran de color verde, los diferentes en rojo y los repetidos en amarillo.\r\n\r\nLuego se dibujan en una gráfica 2D diagonal y el resultado es incomprensible y perfectamente geométrico. Es capaz de generar círculos perfectos sin haber usado el número Pi, funciones de seno, coseno, etc. Por lo que es como si el mismo orden de los números ya incluyera un código inteligente en ellos que tiende a su perfecto orden.\r\n\r\nEste es el resumen de mis descubrimientos: la dimensión cero es un único punto infinito, la primera dimensión es una línea diagonal, la segunda es un rombo diagonal, la tercera es un círculo perfecto. Cada dimensión superior incluye las anteriores en su propia forma geométrica única. No he podido llegar a la cuarta dimensión del todo, pues requiere una imagen de 65.536 x 65.536 píxeles, que hoy en día no se puede generar en PNG, pero en porciones he logrado ver una parte y puede que fallase porque era una copia de la tercera gigante y repetida 16 veces en 4 x 4 zonas, a lo que no le vi el sentido y pensé que me equivoqué por verla en trozos incompletos, pero cabe la posibilidad de que realmente sea así. La quinta requiere una de 4.294.967.296 x 4.294.967.296 píxeles (algo imposible para un PC normal).\r\n\r\nLa tercera dimensión es literalmente un ojo (o un planeta con un anillo alrededor y un rombo en su centro), aunque a mi me recuerda más al ojo de un reptil por ser su centro un rombo y no un círculo (¿será ese el esquema que genera los cuerpos de muchos seres del planeta y otras disposiciones atómicas?). Es como si a medida que se suman dimensiones se empezase a construir un \"objeto\" o cuerpo gigante, y fuese adquiriendo forma progresivamente, lo que me asusta un poco, porque no sé lo que se podría llegar a hacer con este descubrimiento. Parece ser que siempre hay en verde la raíz cuadrada del total de valores usados (para 256, 16 en verde).\r\n\r\nHe descompuesto las figuras de las dimensiones y puede ser solo casualidad, pero me recuerdan al artilugio que llevaban muchos dioses sumerios con forma de aro y vara fusionados, ¿es como si representase los principios para generar cosas en todos los planos y formas?. Lo más raro es que al unir los puntos de la gráfica se dejan de ver dichas figuras fractales. Y sólo inventé esa inversión de guarismos para crear un encriptado en base X, y la ventana para ver cuántos valores seguían aún sin haberse modificado (para ver lo seguro que era como cifrado de archivos), aunque el resultado fue inesperado. También soporta 81 valores en base 3, 9, y demás. O sea que el total de valores debe ser proporcional a las bases usadas o sus raíces cuadradas. ¿Puede que usar solo la base 2 con 256 valores muestre en la gráfica la 4ª dimensión, con el dibujo de la 3ª ampliado y en 4 x 4 veces?\r\n\r\nSi aún tienes dudas sobre cualquier función del programa, por favor envía un correo a Jupitermauro@gmail.com, muchísimas gracias.", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Question);
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

        internal void Actualizar_Valores()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int[] Matriz_Valores = new int[Límite_Real_Valores];
                for (int Índice_Valor = 0; Índice_Valor < Límite_Real_Valores; Índice_Valor++)
                {
                    Matriz_Valores[Índice_Valor] = Índice_Valor;
                }
                if (Lista_Bases.Count > 0)
                {
                    for (int Índice_Base = 0; Índice_Base < Lista_Bases.Count; Índice_Base++)
                    {
                        for (int Índice_Valor = 0; Índice_Valor < Límite_Real_Valores; Índice_Valor++)
                        {
                            if (Índice_Valor < 640 && Matriz_Valores[Índice_Valor] < Límite_Real_Valores)
                            {
                                Matriz_Valores[Índice_Valor] = Matriz_Guarismos_Invertidos_Base[Lista_Bases[Índice_Base]][Matriz_Valores[Índice_Valor]];
                                if (Matriz_Valores[Índice_Valor] > 639) Matriz_Valores[Índice_Valor] = 639;
                            }
                            else Matriz_Valores[Índice_Valor] = 639;
                        }
                    }
                    for (int Índice_Valor = 0; Índice_Valor < Límite_Real_Valores; Índice_Valor++)
                    {
                        if (Matriz_Valores[Índice_Valor] > 639) Matriz_Valores[Índice_Valor] = 639;
                    }
                }
                Graphics Pintar = Graphics.FromImage(Picture.Image);
                Pintar.CompositingMode = CompositingMode.SourceCopy;
                Pintar.Clear(Color.Transparent);
                if (Variable_Dibujo == 0)
                {
                    for (int Índice_Valor = 0; Índice_Valor < Límite_Real_Valores; Índice_Valor++)
                    {
                        Pintar.FillRectangle(Brushes.Black, Índice_Valor, Matriz_Valores[Índice_Valor], 1, 1);
                    }
                }
                else if (Variable_Dibujo == 1)
                {
                    List<Point> Lista_Posiciones = new List<Point>();
                    for (int Índice_Valor = 0; Índice_Valor < Límite_Real_Valores; Índice_Valor++)
                    {
                        Lista_Posiciones.Add(new Point(Índice_Valor, Matriz_Valores[Índice_Valor]));
                    }
                    Pintar.DrawLines(Pens.Black, Lista_Posiciones.ToArray());
                    Lista_Posiciones = null;
                }
                else if (Variable_Dibujo == 2)
                {
                    List<Point> Lista_Posiciones = new List<Point>();
                    for (int Índice_Valor = 0; Índice_Valor < Límite_Real_Valores; Índice_Valor++)
                    {
                        Lista_Posiciones.Add(new Point(Índice_Valor, Matriz_Valores[Índice_Valor]));
                    }
                    Pintar.FillPolygon(Brushes.Black, Lista_Posiciones.ToArray());
                    Lista_Posiciones = null;
                }
                Pintar.Dispose();
                Pintar = null;
                Picture.Invalidate();
                Picture.Update();

                Dictionary<int, int> Diccionario = new Dictionary<int, int>();
                for (int Índice = 0; Índice < Límite_Real_Valores; Índice++)
                {
                    if (!Diccionario.ContainsKey(Matriz_Valores[Índice])) Diccionario.Add(Matriz_Valores[Índice], 1);
                    else Diccionario[Matriz_Valores[Índice]]++;
                }
                //this.Text = Texto_Título + " - [Unique bytes: " + Diccionario.Count.ToString() + "]";

                for (int Índice = 0; Índice < Matriz_Etiquetas.Length; Índice++) // Reset.
                {
                    //Matriz_Etiquetas[Índice].BackColor = Color.FromArgb(176, 255, 176);
                    //Matriz_Etiquetas[Índice].BackColor = Color.White;
                    Matriz_Etiquetas[Índice].BackColor = Color.FromArgb(236, 233, 216);
                    Matriz_Etiquetas[Índice].Text = Índice.ToString();
                }
                int Ancho_Alto = 16; //(int)Math.Sqrt(Límite_Valores);
                for (int Y = 0, Índice = 0; Y < Ancho_Alto; Y++)
                {
                    for (int X = 0; X < Ancho_Alto; X++, Índice++)
                    {
                        if (Índice < Límite_Real_Valores)
                        {
                            Matriz_Etiquetas_XY[X][Y].Text = Matriz_Valores[Índice].ToString();
                            if (Matriz_Valores[Índice] == Índice || Índice >= Límite_Real_Valores) // Original position.
                            {
                                //Bytes_Verdes++;
                                Matriz_Etiquetas_XY[X][Y].BackColor = Color.FromArgb(176, 255, 176);
                            }
                            else if (Diccionario[Matriz_Valores[Índice]] < 2) // Different position, not repeated.
                            {
                                //Bytes_Rojos++;
                                Matriz_Etiquetas_XY[X][Y].BackColor = Color.FromArgb(255, 176, 176);
                            }
                            else // Repeated different position.
                            {
                                //Bytes_Amarillos++;
                                Matriz_Etiquetas_XY[X][Y].BackColor = Color.FromArgb(255, 255, 176);
                            }
                        }
                    }
                }
                for (int Índice = 0; Índice < Matriz_Etiquetas.Length; Índice++)
                {
                    Matriz_Etiquetas[Índice].Invalidate();
                    Matriz_Etiquetas[Índice].Update();
                }

                /*for (int Índice = 0; Índice < Matriz_Etiquetas.Length; Índice++)
                {
                    if (Matriz_Valores[Índice] == Índice || Índice >= Límite_Real_Valores) // Original position.
                    {
                        //Bytes_Verdes++;
                        Matriz_Etiquetas[Índice].BackColor = Color.FromArgb(176, 255, 176);
                    }
                    else if (Diccionario[Matriz_Valores[Índice]] < 2) // Different position, not repeated.
                    {
                        //Bytes_Rojos++;
                        Matriz_Etiquetas[Índice].BackColor = Color.FromArgb(255, 176, 176);
                    }
                    else // Repeated different position.
                    {
                        //Bytes_Amarillos++;
                        Matriz_Etiquetas[Índice].BackColor = Color.FromArgb(255, 255, 176);
                    }
                }*/

                /*if (Matriz_Valores_Cifrado == null || Matriz_Valores_Cifrado.Length < 640)
                {
                    Matriz_Valores_Cifrado = new int[640];
                    for (int Índice = 0; Índice < Matriz_Valores_Cifrado.Length; Índice++)
                    {
                        try { Matriz_Valores_Cifrado[Índice] = Índice; }
                        catch { continue; }
                    }
                }
                if (Matriz_Valores_Descifrado == null || Matriz_Valores_Descifrado.Length < 640)
                {
                    Matriz_Valores_Descifrado = new int[640];
                    for (int Índice = 0; Índice < Matriz_Valores_Descifrado.Length; Índice++)
                    {
                        try { Matriz_Valores_Descifrado[Índice] = Índice; }
                        catch { continue; }
                    }
                }

                int[] Matriz_Valores = new int[640];
                if (!Tecla_Alt_Presionada && !Tecla_Control_Presionada && !Tecla_Mayúsculas_Presionada)
                {
                    for (int Índice = 0; Índice < 640; Índice++) Matriz_Valores[Índice] = Matriz_Valores_Cifrado[Índice];
                }
                else if (Tecla_Alt_Presionada)
                {
                    for (int Índice = 0; Índice < 640; Índice++) Matriz_Valores[Índice] = Matriz_Valores_Descifrado[Índice];
                }
                else if (Tecla_Control_Presionada)
                {
                    for (int Índice = 0; Índice < 640; Índice++) Matriz_Valores[Índice] = Matriz_Valores_Descifrado[Matriz_Valores_Cifrado[Índice]];
                }
                else if (Tecla_Mayúsculas_Presionada)
                {
                    for (int Índice = 0; Índice < 640; Índice++) Matriz_Valores[Índice] = Índice;
                }
                Dictionary<int, int> Diccionario = new Dictionary<int, int>();
                for (int Índice = 0; Índice < 640; Índice++)
                {
                    if (!Diccionario.ContainsKey(Matriz_Valores[Índice])) Diccionario.Add(Matriz_Valores[Índice], 1);
                    else Diccionario[Matriz_Valores[Índice]]++;
                }
                this.Text = Texto_Título + " - [Unique bytes: " + Diccionario.Count.ToString() + "]";
                Matriz_Valores_Actual = Matriz_Valores;
                int Bytes_Verdes = 0, Bytes_Rojos = 0, Bytes_Amarillos = 0;
                using (Graphics Pintar = Graphics.FromImage(Picture.Image))
                {
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.Clear(Color.Transparent);
                    Point Posición_Anterior = new Point(0, 639 - Matriz_Valores[0]);
                    for (int Índice = 0; Índice < Matriz_Etiquetas.Length; Índice++)
                    {
                        try
                        {
                            try
                            {
                                if (Matriz_Valores[Índice] == Índice) // Original position.
                                {
                                    Bytes_Verdes++;
                                    Matriz_Etiquetas[Índice].BackColor = Color.FromArgb(176, 255, 176);
                                }
                                else if (Diccionario[Matriz_Valores[Índice]] < 2) // Different position, not repeated.
                                {
                                    Bytes_Rojos++;
                                    Matriz_Etiquetas[Índice].BackColor = Color.FromArgb(255, 176, 176);
                                }
                                else // Repeated different position.
                                {
                                    Bytes_Amarillos++;
                                    Matriz_Etiquetas[Índice].BackColor = Color.FromArgb(255, 255, 176);
                                }
                            }
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                            try { Matriz_Etiquetas[Índice].Text = Matriz_Valores[Índice].ToString(); }
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                            try
                            {
                                Point Posición = new Point(Índice, 639 - Matriz_Valores[Índice]);
                                //Pintar.DrawLine(Pens.Red, Posición_Anterior, Posición);
                                Posición_Anterior = Posición;
                                //Pintar.FillRectangle(Brushes.Black, Índice, 255 - Matriz_Bytes[Índice], 1, 1);
                            }
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                    }
                    for (int Índice = 0; Índice < Matriz_Etiquetas.Length; Índice++)
                    {
                        Pintar.FillRectangle(Brushes.Black , Índice, 639 - Matriz_Valores[Índice], 1, 1);
                    }
                }
                Etiqueta_Verde.Text = "Green color: original " + Program.Traducir_Número(Bytes_Verdes) + (Bytes_Verdes != 1 ? " bytes" : " byte");
                Etiqueta_Rojo.Text = "Red color: modified " + Program.Traducir_Número(Bytes_Rojos) + (Bytes_Rojos != 1 ? " bytes" : " byte");
                Etiqueta_Azul.Text = "Yellow color: duplicated " + Program.Traducir_Número(Bytes_Amarillos) + (Bytes_Amarillos != 1 ? " bytes" : " byte");
                Picture.Refresh();
                Etiqueta_Verde.Refresh();
                Etiqueta_Rojo.Refresh();
                Etiqueta_Azul.Refresh();*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Combo_Filtro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                return;
                if (Combo_Filtro.SelectedIndex > -1)
                {
                    /*Filtro = (Filtros)Combo_Filtro.SelectedIndex;
                    int[] Matriz_Bytes_Original = new int[640];
                    for (int Índice = 0; Índice < 640; Índice++)
                    {
                        Matriz_Bytes_Original[Índice] = (byte)Índice;
                    }
                    int[] Matriz_Bytes_Filtro = Matriz_Bytes_Original.Clone() as int[];
                    if (Filtro == Filtros.Original)
                    {
                        Establecer_Valores(Matriz_Bytes_Filtro.Clone() as int[], Matriz_Bytes_Filtro.Clone() as int[]);
                    }
                    else if (Filtro == Filtros.Negative)
                    {
                        for (int Índice = 0; Índice < 640; Índice++)
                        {
                            Matriz_Bytes_Filtro[Índice] = 639 - Índice;
                        }
                        Establecer_Valores(Matriz_Bytes_Filtro.Clone() as int[], Matriz_Bytes_Filtro.Clone() as int[]);
                    }
                    else if (Filtro == Filtros.Bit_inversion_in_base_2)
                    {
                        for (int Índice = 0; Índice < 640; Índice++)
                        {
                            Matriz_Bytes_Filtro[Índice] = Program.Matriz_Bytes_Filtro_Invertir_Bits_Base_2[Matriz_Bytes_Filtro[Índice]];
                        }
                        Establecer_Valores(Matriz_Bytes_Filtro.Clone() as int[], Matriz_Bytes_Filtro.Clone() as int[]);
                    }
                    else if (Filtro == Filtros.Bit_inversion_in_base_4)
                    {
                        for (int Índice = 0; Índice < 256; Índice++)
                        {
                            Matriz_Bytes_Filtro[Índice] = Program.Matriz_Bytes_Filtro_Invertir_Bits_Base_4[Matriz_Bytes_Filtro[Índice]];
                        }
                        Establecer_Valores(Matriz_Bytes_Filtro.Clone() as byte[], Matriz_Bytes_Filtro.Clone() as byte[]);
                    }
                    else if (Filtro == Filtros.Bit_inversion_in_base_16)
                    {
                        for (int Índice = 0; Índice < 256; Índice++)
                        {
                            Matriz_Bytes_Filtro[Índice] = Program.Matriz_Bytes_Filtro_Invertir_Bits_Base_16[Matriz_Bytes_Filtro[Índice]];
                        }
                        Establecer_Valores(Matriz_Bytes_Filtro.Clone() as byte[], Matriz_Bytes_Filtro.Clone() as byte[]);
                    }
                    else if (Filtro == Filtros.Bit_inversion_in_bases_2_and_4)
                    {
                        for (int Índice = 0; Índice < 256; Índice++)
                        {
                            Matriz_Bytes_Filtro[Índice] = Program.Matriz_Bytes_Filtro_Invertir_Bits_Base_4[Program.Matriz_Bytes_Filtro_Invertir_Bits_Base_2[Matriz_Bytes_Filtro[Índice]]];
                        }
                        Establecer_Valores(Matriz_Bytes_Filtro.Clone() as byte[], Matriz_Bytes_Filtro.Clone() as byte[]);
                    }
                    else if (Filtro == Filtros.Bit_inversion_in_bases_2_and_16)
                    {
                        for (int Índice = 0; Índice < 256; Índice++)
                        {
                            Matriz_Bytes_Filtro[Índice] = Program.Matriz_Bytes_Filtro_Invertir_Bits_Base_16[Program.Matriz_Bytes_Filtro_Invertir_Bits_Base_2[Matriz_Bytes_Filtro[Índice]]];
                        }
                        Establecer_Valores(Matriz_Bytes_Filtro.Clone() as byte[], Matriz_Bytes_Filtro.Clone() as byte[]);
                    }
                    else if (Filtro == Filtros.Bit_inversion_in_bases_4_and_16)
                    {
                        for (int Índice = 0; Índice < 256; Índice++)
                        {
                            Matriz_Bytes_Filtro[Índice] = Program.Matriz_Bytes_Filtro_Invertir_Bits_Base_16[Program.Matriz_Bytes_Filtro_Invertir_Bits_Base_4[Matriz_Bytes_Filtro[Índice]]];
                        }
                        Establecer_Valores(Matriz_Bytes_Filtro.Clone() as byte[], Matriz_Bytes_Filtro.Clone() as byte[]);
                    }
                    else if (Filtro == Filtros.Bit_inversion_in_bases_2___4_and_16)
                    {
                        for (int Índice = 0; Índice < 256; Índice++)
                        {
                            Matriz_Bytes_Filtro[Índice] = Program.Matriz_Bytes_Filtro_Invertir_Bits_Base_16[Program.Matriz_Bytes_Filtro_Invertir_Bits_Base_4[Program.Matriz_Bytes_Filtro_Invertir_Bits_Base_2[Matriz_Bytes_Filtro[Índice]]]];
                        }
                        Establecer_Valores(Matriz_Bytes_Filtro.Clone() as byte[], Matriz_Bytes_Filtro.Clone() as byte[]);
                    }
                    else // Unknown filter.
                    {
                        Establecer_Valores(Matriz_Bytes_Filtro.Clone() as byte[], Matriz_Bytes_Filtro.Clone() as byte[]);
                    }*/
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Right)
                {
                    this.Close();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckedListBox_Bases_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    for (int Índice = 0; Índice < CheckedListBox_Bases.Items.Count; Índice++)
                    {
                        CheckedListBox_Bases.SetItemChecked(Índice, false);
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ListView_Bases_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    for (int Índice = 0; Índice < ListView_Bases.Items.Count; Índice++)
                    {
                        ListView_Bases.Items[Índice].Checked = false;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ListView_Bases_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    ListViewHitTestInfo Info = ListView_Bases.HitTest(e.Location);
                    ListViewItem Objeto = Info.Item;
                    if (Objeto != null)
                    {
                        Objeto.Checked = !Objeto.Checked;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// List with the currently selected bases. Note: only includes the power X ^ 1.
        /// </summary>
        internal List<int> Lista_Bases = new List<int>();
        /// <summary>
        /// List with the currently selected powers. Note: X ^ 1 is included if that base is contained inside "Lista_Bases".
        /// </summary>
        internal List<int> Lista_Potencias = new List<int>();
        /// <summary>
        /// List with the length in figures of each power in any base. [index] = base number, starting by X ^ 1. Seems to be the same length for every base.
        /// </summary>
        internal List<int> Lista_Potencias_Longitudes = new List<int>(new int[12] { /*17*/16, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 });
        //internal List<int> Lista_Potencias_Longitudes = new List<int>(new int[12] { /*17*/9, 9, 9, 5, 5, 5, 5, 5, 5, 5, 5, 5 });
        //internal List<int> Lista_Potencias_Longitudes = new List<int>(new int[12] { 2, 3, 5, 9, 17, 33, 65, 129, 257, 513, 1025, 2049 });

        internal List<int> Lista_Bases_Longitudes = new List<int>(new int[12] { 65536, 6561, 65536, 625, 1296, 2401, 4096, 6561, 10000, 14641, 20736, 28561 });

        private void CheckedListBox_Bases_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                int Índice = e.Index;
                if (Índice < 16) // Bases.
                {
                    if (e.NewValue == CheckState.Unchecked)
                    {
                        if (Lista_Bases.Contains(Índice)) Lista_Bases.Remove(Índice);
                    }
                    else if (!Lista_Bases.Contains(Índice)) Lista_Bases.Add(Índice);
                }
                else // Powers.
                {
                    Índice = Índice - 15; // From 1 to 16.
                    if (e.NewValue == CheckState.Unchecked)
                    {
                        if (Lista_Potencias.Contains(Índice)) Lista_Potencias.Remove(Índice);
                    }
                    else if (!Lista_Potencias.Contains(Índice)) Lista_Potencias.Add(Índice);
                }
                Reiniciar_Fondo();
                Actualizar_Valores();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Límite_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown_Límite.Refresh();
                Límite_Valores = (int)NumericUpDown_Límite.Value;
                Límite_Valores_1 = Límite_Valores - 1;
                Límite_Real_Valores = Math.Min(Límite_Valores, 640);
                Límite_Real_Valores_1 = Math.Min(Límite_Valores_1, 639);
                Y = 640 - Límite_Valores;
                Reiniciar_Valores();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Reiniciar_Valores()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Matriz_Guarismos_Invertidos_Base = new int[16][];
                for (int Índice_Base = 0; Índice_Base < 16; Índice_Base++)
                {
                    Matriz_Guarismos_Invertidos_Base[Índice_Base] = new int[Límite_Real_Valores];
                    List<int> Lista_Base = Calculadora_Infinita.Traducir_Número((Índice_Base + 2).ToString());
                    for (int Índice_Valor = 0; Índice_Valor < Límite_Real_Valores; Índice_Valor++)
                    {
                        int Longitud_Base = Calculadora_Infinita.Traducir_Número_Base(Calculadora_Infinita.Operación_Convertir_a_Base(Calculadora_Infinita.Traducir_Número(Límite_Real_Valores_1.ToString()), Lista_Base), Lista_Base).Length;
                        List<int> Lista_Valor = Calculadora_Infinita.Traducir_Número(Índice_Valor.ToString());
                        List<List<int>> Lista_Guarismos = Calculadora_Infinita.Operación_Convertir_a_Base(Lista_Valor, Lista_Base);
                        //while (Lista_Guarismos.Count < Longitud_Base) Lista_Guarismos.Insert(0, new List<int>(new int[] { 0 }));
                        /*for (int Índice_Lista = 0; Índice_Lista < Lista_Guarismos.Count; Índice_Lista++)
                        {
                            for (int Índice_Guarismo = 0; Índice_Guarismo < Lista_Guarismos[Índice_Lista].Count; Índice_Guarismo++)
                            {
                                Lista_Guarismos[Índice_Lista][Índice_Guarismo] = Índice_Base - Lista_Guarismos[Índice_Lista][Índice_Guarismo];
                            }
                            //if (Lista_Guarismos[Índice_Lista].Count > 1) Lista_Guarismos[Índice_Lista].Reverse();
                        }*/
                        if (Lista_Guarismos.Count > 1) Lista_Guarismos.Reverse();
                        while (Lista_Guarismos.Count < Longitud_Base) Lista_Guarismos.Add(new List<int>(new int[] { 0 }));
                        //if (Lista_Guarismos.Count < Longitud_Base) Lista_Guarismos.AddRange(new int[Longitud_Base - Lista_Guarismos.Count]);
                        int Valor = int.Parse(Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Calculadora_Infinita.Operación_Convertir_desde_Base(Lista_Guarismos, Lista_Base)));
                        Matriz_Guarismos_Invertidos_Base[Índice_Base][Índice_Valor] = Valor < Límite_Valores ? Valor : Índice_Valor;
                    }
                }
                Reiniciar_Fondo();
                Picture.Image = new Bitmap(640, 640, PixelFormat.Format32bppArgb); // Overlay.
                Actualizar_Valores();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        internal void Reiniciar_Fondo()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Bitmap Imagen_Fondo = new Bitmap(640, 640, PixelFormat.Format32bppArgb);
                Graphics Pintar = Graphics.FromImage(Imagen_Fondo);
                //Pintar.Clear(Color.White);
                Pintar.CompositingMode = CompositingMode.SourceOver;
                /*ImageList Lista_Imágenes = new ImageList();
                Lista_Imágenes.ColorDepth = ColorDepth.Depth32Bit;
                Lista_Imágenes.ImageSize = new Size(13, 13);
                Lista_Imágenes.TransparentColor = Color.Empty;
                Bitmap Imagen_Base = null;
                Graphics Pintar_Base = null;
                SolidBrush Pincel_Base = null;*/

                Pen Lápiz = new Pen(Color.FromArgb(236, 233, 216));

                // Image for the diagonal line.
                /*Imagen_Base = new Bitmap(13, 13, PixelFormat.Format32bppArgb);
                Pintar_Base = Graphics.FromImage(Imagen_Base);
                Pintar_Base.Clear(Color.Black);
                Pintar_Base.CompositingMode = CompositingMode.SourceCopy;
                Pincel_Base = new SolidBrush(Color.FromArgb(236, 233, 216));
                //Pintar_Base.FillRectangle(Brushes.White, 1, 1, 11, 11);
                //Pintar_Base.DrawLine(Lápiz, 1, 11, 11, 1);
                Pintar_Base.FillRectangle(Pincel_Base, 1, 1, 11, 11);
                Pincel_Base.Dispose();
                Pincel_Base = null;
                Pintar_Base.Dispose();
                Pintar_Base = null;
                Lista_Imágenes.Images.Add(Imagen_Base.Clone() as Bitmap);
                Imagen_Base.Dispose();
                Imagen_Base = null;

                ListView_Bases.SmallImageList = Lista_Imágenes;
                ListView_Bases.LargeImageList = Lista_Imágenes;*/
                //ListView_Bases.Columns[0].Width = 206;
                //ListView_Bases.Columns[1].Width = 206;
                Pintar.DrawLine(Lápiz, 0, 0, 639, 639);
                Lápiz.Dispose();
                Lápiz = null;

                SolidBrush Pincel_Límite = new SolidBrush(Color.FromArgb(236, 233, 216));
                Pintar.FillRectangle(Pincel_Límite, 0, Límite_Valores_1, 640, 1); // X.
                Pintar.FillRectangle(Pincel_Límite, Límite_Valores_1, 0, 1, 640); // Y.
                Pincel_Límite.Dispose();
                Pincel_Límite = null;

                for (int Índice_Base = 0; Índice_Base < 16; Índice_Base++)
                {
                    if (Lista_Bases.Contains(Índice_Base))
                    {
                        int Base = ((Índice_Base + 2) * (Índice_Base + 2)) - 1;
                        //SolidBrush Pincel = new SolidBrush(Color.FromArgb(224, 224, 224));
                        //SolidBrush Pincel = new SolidBrush(Program.Obtener_Color_Puro_1530((Base * 1529) / 639));
                        Color Color_ARGB = Program.Obtener_Color_Puro_1530((Base * 1529) / 639);

                        /*Imagen_Base = new Bitmap(13, 13, PixelFormat.Format32bppArgb);
                        Pintar_Base = Graphics.FromImage(Imagen_Base);
                        Pintar_Base.Clear(Color.Black);
                        Pintar_Base.CompositingMode = CompositingMode.SourceCopy;
                        Pincel_Base = new SolidBrush(Color_ARGB);
                        Pintar_Base.FillRectangle(Pincel_Base, 1, 1, 11, 11);
                        Pincel_Base.Dispose();
                        Pincel_Base = null;
                        Pintar_Base.Dispose();
                        Pintar_Base = null;
                        Lista_Imágenes.Images.Add(Imagen_Base.Clone() as Bitmap);
                        Imagen_Base.Dispose();
                        Imagen_Base = null;*/

                        //ListView_Bases.Items[Índice - 1].BackColor = Color_ARGB;
                        //ListView_Bases.Items[Índice - 1].ForeColor = Color_ARGB;
                        //Color_ARGB = Color.FromArgb(255, Color_ARGB.R, Color_ARGB.G, Color_ARGB.B);
                        
                        HatchBrush Pincel = new HatchBrush(HatchStyle.Percent50, Color_ARGB, Color.Transparent);
                        Pintar.FillRectangle(Pincel, 0, Base, 640, 1); // X.
                        Pintar.FillRectangle(Pincel, Base, 0, 1, 640); // Y.

                        //Pintar.FillRectangle(Pincel, 0, 127, 256, 1);
                        //Pintar.FillRectangle(Pincel, 0, 191, 256, 1);

                        //Pintar.FillRectangle(Pincel, 128, 0, 1, 256);
                        //Pintar.FillRectangle(Pincel, 192, 0, 1, 256);

                        Pincel.Dispose();
                        Pincel = null;
                    }
                }
                // Image for the rainbow lines.
                /*Imagen_Base = new Bitmap(13, 13, PixelFormat.Format32bppArgb);
                Pintar_Base = Graphics.FromImage(Imagen_Base);
                Pintar_Base.Clear(Color.Black);
                Pintar_Base.CompositingMode = CompositingMode.SourceCopy;
                //Pintar_Base.FillRectangle(Brushes.White, 1, 1, 11, 11);
                for (int Índice = 0; Índice < 11; Índice++)
                {
                    Pincel_Base = new SolidBrush(Program.Obtener_Color_Puro_1530((Índice * 1529) / 10));
                    Pintar_Base.FillRectangle(Pincel_Base, 1, 1 + (10 - Índice), 11, 1); // X.
                    Pintar_Base.FillRectangle(Pincel_Base, 1 + Índice, 1, 1, 11); // Y.
                    Pincel_Base.Dispose();
                    Pincel_Base = null;
                }
                Pintar_Base.Dispose();
                Pintar_Base = null;
                Lista_Imágenes.Images.Add(Imagen_Base.Clone() as Bitmap);
                Imagen_Base.Dispose();
                Imagen_Base = null;*/

                Pintar.Dispose();
                Pintar = null;
                Picture.BackgroundImage = Imagen_Fondo; // Background.
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        internal Thread Subproceso = null;

        internal void Subproceso_DoWork()
        {
            try
            {
                //int Índice_Base = 2;

                FileStream Lector = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Auto base.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamWriter Lector_Texto = new StreamWriter(Lector, Encoding.Default);

                //Matriz_Guarismos_Invertidos_Base = new int[12][];
                for (int Índice_Base = 2; Índice_Base <= 13; Índice_Base++)
                {
                    Límite_Valores = Lista_Bases_Longitudes[Índice_Base - 2];
                    Límite_Valores_1 = Límite_Valores - 1;
                    //Límite_Real_Valores = Math.Min(Límite_Valores, 640);
                    Límite_Real_Valores = 640;
                    Límite_Real_Valores_1 = Límite_Real_Valores - 1;
                    Y = 640 - Límite_Valores;

                    Lector_Texto.WriteLine();
                    Lector_Texto.WriteLine("[Base " + Índice_Base.ToString() + "]");
                    Lector_Texto.WriteLine();
                    Lector_Texto.Flush();
                    for (int Índice_Potencia = 1; Índice_Potencia <= 10; Índice_Potencia++)
                    {
                        //Lector_Texto.WriteLine("{Power " + Índice_Potencia.ToString() + "}");
                        Lector_Texto.Write("new int[" + Límite_Real_Valores.ToString() + "] { ");
                        Lector_Texto.Flush();
                        //Matriz_Guarismos_Invertidos_Base[Índice_Base] = new int[Límite_Real_Valores];
                        List<int> Lista_Base = Calculadora_Infinita.Traducir_Número(Índice_Base.ToString());
                        for (int Índice_Valor = 0; Índice_Valor < Límite_Real_Valores; Índice_Valor++)
                        {
                            //this.Text = "Base " + Índice_Base.ToString() + ", Power " + Índice_Potencia.ToString() + ", Value " + Índice_Valor.ToString();
                            int Longitud_Base = Lista_Potencias_Longitudes[Índice_Base - 2]; //Calculadora_Infinita.Traducir_Número_Base(Calculadora_Infinita.Operación_Convertir_a_Base(Calculadora_Infinita.Traducir_Número(Límite_Real_Valores_1.ToString()), Lista_Base), Lista_Base).Length;
                            //int Longitud_Base = Lista_Potencias_Longitudes[Índice_Potencia - 1]; //Calculadora_Infinita.Traducir_Número_Base(Calculadora_Infinita.Operación_Convertir_a_Base(Calculadora_Infinita.Traducir_Número(Límite_Real_Valores_1.ToString()), Lista_Base), Lista_Base).Length;
                            List<int> Lista_Valor = Calculadora_Infinita.Traducir_Número(Índice_Valor.ToString());
                            List<List<int>> Lista_Guarismos = Calculadora_Infinita.Operación_Convertir_a_Base(Lista_Valor, Lista_Base);
                            while (Lista_Guarismos.Count < Longitud_Base) Lista_Guarismos.Insert(0, new List<int>(new int[] { 0 }));
                            if (Lista_Guarismos.Count > 1) Lista_Guarismos.Reverse(); // Fractal.
                            int Valor = int.Parse(Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Calculadora_Infinita.Operación_Convertir_desde_Base(Lista_Guarismos, Lista_Base)));
                            //Valor = Límite_Valores_1 - Valor; // Negative.
                            //if (Valor > Límite_Real_Valores_1) Valor = Límite_Real_Valores_1;
                            Lector_Texto.Write(Valor.ToString() + (Índice_Valor < Límite_Real_Valores - 1 ? ", " : null));
                            Lector_Texto.Flush();
                            //Matriz_Guarismos_Invertidos_Base[Índice_Base][Índice_Valor] = Valor < Límite_Valores ? Valor : Índice_Valor;
                        }
                        Lector_Texto.Write("},");
                        Lector_Texto.WriteLine();
                        //Lector_Texto.WriteLine();
                        Lector_Texto.Flush();
                    }
                }
                Lector_Texto.Close();
                Lector_Texto.Dispose();
                Lector_Texto = null;
                Lector.Close();
                Lector.Dispose();
                Lector = null;

                /*FileStream Lector2 = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Base numbers.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamWriter Lector_Texto2 = new StreamWriter(Lector2, Encoding.Default);
                FileStream Lector3 = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Base figures.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamWriter Lector_Texto3 = new StreamWriter(Lector3, Encoding.Default);
                List<int> Lista_Potencia = Calculadora_Infinita.Traducir_Número("2");
                for (int Índice_Base = 2; Índice_Base <= 13; Índice_Base++)
                {
                    List<int> Lista_Base = Calculadora_Infinita.Traducir_Número(Índice_Base.ToString());
                    List<int> Lista_Valor = Calculadora_Infinita.Traducir_Número(Índice_Base.ToString());
                    Lector_Texto.WriteLine("[Base " + Índice_Base.ToString() + "]");
                    Lector_Texto.Flush();
                    Lector_Texto2.WriteLine("[Base " + Índice_Base.ToString() + "]");
                    Lector_Texto2.Flush();
                    Lector_Texto3.WriteLine("[Base " + Índice_Base.ToString() + "]");
                    Lector_Texto3.Flush();
                    for (int Índice_Potencia = 1; Índice_Potencia <= 10; Índice_Potencia++)
                    {
                        this.Text = "Base " + Índice_Base.ToString() + ", Power " + Índice_Potencia.ToString();
                        string Texto_Base = Calculadora_Infinita.Traducir_Número_Base(Calculadora_Infinita.Operación_Convertir_a_Base(Lista_Valor, Lista_Base), Lista_Base);
                        int Longitud_Base = Texto_Base.Length;
                        Lector_Texto3.WriteLine(Texto_Base);
                        Lector_Texto3.WriteLine();
                        Lector_Texto3.Flush();
                        Texto_Base = null;
                        Lector_Texto.Write(Longitud_Base.ToString() + ", ");
                        Lector_Texto.Flush();
                        string Texto_Número = Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Lista_Valor);
                        Lector_Texto2.WriteLine(Texto_Número);
                        Lector_Texto2.WriteLine();
                        Lector_Texto2.Flush();
                        Texto_Número = null;
                        this.Text = "Base " + Índice_Base.ToString() + ", Power " + Índice_Potencia.ToString() + ", Next...";
                        Lista_Valor = Calculadora_Infinita.Operación_Potencia(Lista_Valor, Lista_Potencia);
                        this.Text = "Base " + Índice_Base.ToString() + ", Power " + Índice_Potencia.ToString() + ", Free...";
                        GC.Collect();
                        GC.GetTotalMemory(true);
                    }
                    //Texto = Texto.TrimEnd(", ".ToCharArray()) + "\r\n\r\n";
                    Lector_Texto.WriteLine();
                    Lector_Texto.WriteLine();
                    Lector_Texto.Flush();
                    Lector_Texto2.WriteLine();
                    Lector_Texto2.WriteLine();
                    Lector_Texto2.Flush();
                    Lector_Texto3.WriteLine();
                    Lector_Texto3.WriteLine();
                    Lector_Texto3.Flush();
                    //break;
                }
                Lector_Texto.Close();
                Lector_Texto.Dispose();
                Lector_Texto = null;
                Lector.Close();
                Lector.Dispose();
                Lector = null;
                Lector_Texto2.Close();
                Lector_Texto2.Dispose();
                Lector_Texto2 = null;
                Lector2.Close();
                Lector2.Dispose();
                Lector2 = null;
                Lector_Texto3.Close();
                Lector_Texto3.Dispose();
                Lector_Texto3 = null;
                Lector3.Close();
                Lector3.Dispose();
                Lector3 = null;*/
                this.Activate();
                MessageBox.Show(this, "Done!");
            }
            catch (ThreadAbortException Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Abc()
        {
            try
            {
                Temporizador_Principal.Stop();
                Subproceso = new Thread(new ThreadStart(Subproceso_DoWork));
                Subproceso.IsBackground = true;
                Subproceso.Priority = ThreadPriority.Normal;
                Subproceso.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Límite_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    NumericUpDown_Límite.Value = 256m;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Exportar_Imagen_Máxima(int Base, bool Potencia_2, bool Potencia_3, bool Potencia_4, bool Potencia_5)
        {
            try
            {
                AAA();
                return;

                int Límite_Máximo = 0;
                List<int> Lista_Base = Calculadora_Infinita.Traducir_Número(Base.ToString());
                int[] Matriz_Potencia_2 = null;
                int[] Matriz_Potencia_3 = null;
                int[] Matriz_Potencia_4 = null;
                int[] Matriz_Potencia_5 = null;
                string Texto_Potencias = null;
                if (Potencia_2)
                {
                    Texto_Potencias += "2, ";
                    int Límite = Base;
                    for (int Índice_Potencia = 1; Índice_Potencia < 2; Índice_Potencia++)
                    {
                        Límite = (int)Math.Pow((double)Límite, 2d);
                    }
                    if (Límite > Límite_Máximo) Límite_Máximo = Límite;
                    int Límite_1 = Límite - 1;
                    Matriz_Potencia_2 = new int[Límite];
                    int Longitud_Base = Calculadora_Infinita.Traducir_Número_Base(Calculadora_Infinita.Operación_Convertir_a_Base(Calculadora_Infinita.Traducir_Número(Límite_1.ToString()), Lista_Base), Lista_Base).Length;
                    for (int Índice_Valor = 0; Índice_Valor < Límite; Índice_Valor++)
                    {
                        List<int> Lista_Valor = Calculadora_Infinita.Traducir_Número(Índice_Valor.ToString());
                        List<List<int>> Lista_Guarismos = Calculadora_Infinita.Operación_Convertir_a_Base(Lista_Valor, Lista_Base);
                        Lista_Valor = null;
                        while (Lista_Guarismos.Count < Longitud_Base) Lista_Guarismos.Insert(0, new List<int>(new int[] { 0 }));
                        if (Lista_Guarismos.Count > 1) Lista_Guarismos.Reverse();
                        int Valor = int.Parse(Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Calculadora_Infinita.Operación_Convertir_desde_Base(Lista_Guarismos, Lista_Base)));
                        Lista_Guarismos = null;
                        Matriz_Potencia_2[Índice_Valor] = Valor < Límite ? Valor : Índice_Valor;
                    }
                }
                if (Potencia_3)
                {
                    Texto_Potencias += "3, ";
                    int Límite = Base;
                    for (int Índice_Potencia = 1; Índice_Potencia < 3; Índice_Potencia++)
                    {
                        Límite = (int)Math.Pow((double)Límite, 2d);
                    }
                    if (Límite > Límite_Máximo) Límite_Máximo = Límite;
                    int Límite_1 = Límite - 1;
                    Matriz_Potencia_3 = new int[Límite];
                    int Longitud_Base = Calculadora_Infinita.Traducir_Número_Base(Calculadora_Infinita.Operación_Convertir_a_Base(Calculadora_Infinita.Traducir_Número(Límite_1.ToString()), Lista_Base), Lista_Base).Length;
                    for (int Índice_Valor = 0; Índice_Valor < Límite; Índice_Valor++)
                    {
                        List<int> Lista_Valor = Calculadora_Infinita.Traducir_Número(Índice_Valor.ToString());
                        List<List<int>> Lista_Guarismos = Calculadora_Infinita.Operación_Convertir_a_Base(Lista_Valor, Lista_Base);
                        Lista_Valor = null;
                        while (Lista_Guarismos.Count < Longitud_Base) Lista_Guarismos.Insert(0, new List<int>(new int[] { 0 }));
                        if (Lista_Guarismos.Count > 1) Lista_Guarismos.Reverse();
                        int Valor = int.Parse(Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Calculadora_Infinita.Operación_Convertir_desde_Base(Lista_Guarismos, Lista_Base)));
                        Lista_Guarismos = null;
                        Matriz_Potencia_3[Índice_Valor] = Valor < Límite ? Valor : Índice_Valor;
                    }
                }
                if (Potencia_4)
                {
                    Texto_Potencias += "4, ";
                    int Límite = Base;
                    for (int Índice_Potencia = 1; Índice_Potencia < 4; Índice_Potencia++)
                    {
                        Límite = (int)Math.Pow((double)Límite, 2d);
                    }
                    if (Límite > Límite_Máximo) Límite_Máximo = Límite;
                    int Límite_1 = Límite - 1;
                    Matriz_Potencia_4 = new int[Límite];
                    int Longitud_Base = Calculadora_Infinita.Traducir_Número_Base(Calculadora_Infinita.Operación_Convertir_a_Base(Calculadora_Infinita.Traducir_Número(Límite_1.ToString()), Lista_Base), Lista_Base).Length;
                    for (int Índice_Valor = 0; Índice_Valor < Límite; Índice_Valor++)
                    {
                        List<int> Lista_Valor = Calculadora_Infinita.Traducir_Número(Índice_Valor.ToString());
                        List<List<int>> Lista_Guarismos = Calculadora_Infinita.Operación_Convertir_a_Base(Lista_Valor, Lista_Base);
                        Lista_Valor = null;
                        while (Lista_Guarismos.Count < Longitud_Base) Lista_Guarismos.Insert(0, new List<int>(new int[] { 0 }));
                        if (Lista_Guarismos.Count > 1) Lista_Guarismos.Reverse();
                        int Valor = int.Parse(Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Calculadora_Infinita.Operación_Convertir_desde_Base(Lista_Guarismos, Lista_Base)));
                        Lista_Guarismos = null;
                        Matriz_Potencia_4[Índice_Valor] = Valor < Límite ? Valor : Índice_Valor;
                    }
                }
                if (Potencia_5)
                {
                    Texto_Potencias += "5, ";
                    int Límite = Base;
                    for (int Índice_Potencia = 1; Índice_Potencia < 5; Índice_Potencia++)
                    {
                        Límite = (int)Math.Pow((double)Límite, 2d);
                    }
                    if (Límite > Límite_Máximo) Límite_Máximo = Límite;
                    int Límite_1 = Límite - 1;
                    Matriz_Potencia_5 = new int[Límite];
                    int Longitud_Base = Calculadora_Infinita.Traducir_Número_Base(Calculadora_Infinita.Operación_Convertir_a_Base(Calculadora_Infinita.Traducir_Número(Límite_1.ToString()), Lista_Base), Lista_Base).Length;
                    for (int Índice_Valor = 0; Índice_Valor < Límite; Índice_Valor++)
                    {
                        List<int> Lista_Valor = Calculadora_Infinita.Traducir_Número(Índice_Valor.ToString());
                        List<List<int>> Lista_Guarismos = Calculadora_Infinita.Operación_Convertir_a_Base(Lista_Valor, Lista_Base);
                        Lista_Valor = null;
                        while (Lista_Guarismos.Count < Longitud_Base) Lista_Guarismos.Insert(0, new List<int>(new int[] { 0 }));
                        if (Lista_Guarismos.Count > 1) Lista_Guarismos.Reverse();
                        int Valor = int.Parse(Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Calculadora_Infinita.Operación_Convertir_desde_Base(Lista_Guarismos, Lista_Base)));
                        Lista_Guarismos = null;
                        Matriz_Potencia_5[Índice_Valor] = Valor < Límite ? Valor : Índice_Valor;
                    }
                }
                int Ancho_Alto = Math.Min(Límite_Máximo, Program.Ancho_Alto_Máximo_Imagen);
                Bitmap Imagen = new Bitmap(Ancho_Alto, Ancho_Alto, PixelFormat.Format24bppRgb);
                Graphics Pintar = Graphics.FromImage(Imagen);
                Pintar.Clear(Color.Black);
                Pintar.CompositingMode = CompositingMode.SourceCopy;
                int[] Matriz_Valores = new int[Límite_Máximo];
                for (int Índice_Valor = 0; Índice_Valor < Límite_Máximo; Índice_Valor++)
                {
                    Matriz_Valores[Índice_Valor] = Índice_Valor;
                }
                for (int Índice_Valor = 0; Índice_Valor < Límite_Máximo; Índice_Valor++)
                {
                    if (Potencia_2 && Índice_Valor < Matriz_Potencia_2.Length)
                    {
                        Matriz_Valores[Índice_Valor] = Matriz_Potencia_2[Matriz_Valores[Índice_Valor]];
                    }
                    if (Potencia_3 && Índice_Valor < Matriz_Potencia_3.Length)
                    {
                        Matriz_Valores[Índice_Valor] = Matriz_Potencia_3[Matriz_Valores[Índice_Valor]];
                    }
                    if (Potencia_4 && Índice_Valor < Matriz_Potencia_4.Length)
                    {
                        Matriz_Valores[Índice_Valor] = Matriz_Potencia_4[Matriz_Valores[Índice_Valor]];
                    }
                    if (Potencia_5 && Índice_Valor < Matriz_Potencia_5.Length)
                    {
                        Matriz_Valores[Índice_Valor] = Matriz_Potencia_5[Matriz_Valores[Índice_Valor]];
                    }
                }
                for (int Índice_Valor = 0; Índice_Valor < Límite_Máximo; Índice_Valor++)
                {
                    Pintar.FillRectangle(Brushes.White, Índice_Valor, (Ancho_Alto - 1) - Matriz_Valores[Índice_Valor], 1, 1);
                }
                Pintar.Dispose();
                Pintar = null;
                Imagen.Save(Program.Obtener_Ruta_Temporal_Escritorio() + ", " + Base.ToString() + ", " + Texto_Potencias.TrimEnd(", ".ToCharArray()).ToString() + ".png", ImageFormat.Png);
                Imagen.Dispose();
                Imagen = null;
                SystemSounds.Asterisk.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void AAA()
        {
            try
            {
                ZZZ();
                return;

                int Base = 3;
                int Potencia = 5;
                int Límite = Base;
                for (int Índice_Potencia = 1; Índice_Potencia < Potencia; Índice_Potencia++)
                {
                    Límite = (int)Math.Pow((double)Límite, 2d);
                }
                int Límite_1 = Límite - 1;
                int Ancho_Alto = (int)Math.Sqrt((double)Límite);
                int[] Matriz_Valores = new int[Límite];
                List<int> Lista_Base = Calculadora_Infinita.Traducir_Número(Base.ToString());
                int Longitud_Base = Calculadora_Infinita.Traducir_Número_Base(Calculadora_Infinita.Operación_Convertir_a_Base(Calculadora_Infinita.Traducir_Número(Límite_1.ToString()), Lista_Base), Lista_Base).Length;
                for (int Índice_Valor = 0; Índice_Valor < Límite; Índice_Valor++)
                {
                    List<int> Lista_Valor = Calculadora_Infinita.Traducir_Número(Índice_Valor.ToString());
                    List<List<int>> Lista_Guarismos = Calculadora_Infinita.Operación_Convertir_a_Base(Lista_Valor, Lista_Base);
                    Lista_Valor = null;
                    while (Lista_Guarismos.Count < Longitud_Base) Lista_Guarismos.Insert(0, new List<int>(new int[] { 0 }));
                    if (Lista_Guarismos.Count > 1) Lista_Guarismos.Reverse();
                    int Valor = int.Parse(Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Calculadora_Infinita.Operación_Convertir_desde_Base(Lista_Guarismos, Lista_Base)));
                    Lista_Guarismos = null;
                    //Matriz_Valores[Índice_Valor] = Valor < Límite ? Valor : Índice_Valor;
                    Matriz_Valores[Índice_Valor] = Valor;
                }
                Bitmap Imagen = new Bitmap(Ancho_Alto, Ancho_Alto, PixelFormat.Format24bppRgb);
                Graphics Pintar = Graphics.FromImage(Imagen);
                Pintar.Clear(Color.Black);
                Pintar.CompositingMode = CompositingMode.SourceCopy;
                for (int Y = 0, Índice_Valor = 0; Y < Ancho_Alto; Y++)
                {
                    for (int X = 0; X < Ancho_Alto; X++, Índice_Valor++)
                    {
                        if (Matriz_Valores[Índice_Valor] == Índice_Valor)
                        {
                            Pintar.FillRectangle(new SolidBrush(Color.FromArgb(176, 255, 176)), X, Y, 1, 1);
                        }
                    }
                }
                Pintar.Dispose();
                Pintar = null;
                Imagen.Save(Program.Obtener_Ruta_Temporal_Escritorio() + ", " + Base.ToString() + ", " + Potencia + " OK.png", ImageFormat.Png);
                Imagen.Dispose();
                Imagen = null;
                SystemSounds.Asterisk.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void ZZZ()
        {
            try
            {
                return;

                int Base = 10;
                int Potencia = 4;
                int Límite = Base;
                for (int Índice_Potencia = 1; Índice_Potencia < Potencia; Índice_Potencia++)
                {
                    Límite *= Límite;
                }
                int Límite_1 = Límite - 1;
                int Ancho_Alto = (int)Math.Sqrt((double)Límite);
                //int[] Matriz_Valores = new int[Límite];
                //List<int> Lista_Base = Calculadora_Infinita.Traducir_Número(Base.ToString());
                int Longitud_Base = Límite_1.ToString().Length;
                /*for (int Índice_Valor = 0; Índice_Valor < Límite; Índice_Valor++)
                {
                    List<int> Lista_Valor = Calculadora_Infinita.Traducir_Número(Índice_Valor.ToString());
                    //List<List<int>> Lista_Guarismos = Calculadora_Infinita.Operación_Convertir_a_Base(Lista_Valor, Lista_Base);
                    //Lista_Valor = null;
                    while (Lista_Valor.Count < Longitud_Base) Lista_Valor.Insert(0, 0);
                    if (Lista_Valor.Count > 1) Lista_Valor.Reverse();
                    int Valor = int.Parse(Calculadora_Infinita.Traducir_Número_Sin_Puntuación(Lista_Valor));
                    //Matriz_Valores[Índice_Valor] = Valor < Límite ? Valor : Índice_Valor;
                    Matriz_Valores[Índice_Valor] = Valor;
                }*/
                Bitmap Imagen = new Bitmap(Ancho_Alto, Ancho_Alto, PixelFormat.Format24bppRgb);
                Graphics Pintar = Graphics.FromImage(Imagen);
                Pintar.Clear(Color.Black);
                Pintar.CompositingMode = CompositingMode.SourceCopy;
                for (int Y = 0, Índice_Valor = 0; Y < Ancho_Alto; Y++)
                {
                    for (int X = 0; X < Ancho_Alto; X++, Índice_Valor++)
                    {
                        List<int> Lista_Valor = Calculadora_Infinita.Traducir_Número(Índice_Valor.ToString());


                        /*string Texto = Índice_Valor.ToString();
                        string Texto2 = Texto;
                        if (Texto.Length < Longitud_Base) Texto = new string('0', Longitud_Base - Texto.Length) + Texto;
                        //int Valor = int.Parse(new string(Texto.Reverse().ToArray()));
                        //Texto = null;
                        //if (Valor == Índice_Valor)
                        if (string.Compare(new string(Texto.Reverse().ToArray()), Texto2) == 0)
                        {
                            Pintar.FillRectangle(new SolidBrush(Color.FromArgb(176, 255, 176)), X, Y, 1, 1);
                        }*/
                    }
                    this.Text = Y.ToString();
                    Application.DoEvents();
                }
                Pintar.Dispose();
                Pintar = null;
                Imagen.Save(Program.Obtener_Ruta_Temporal_Escritorio() + ", " + Base.ToString() + ", " + Potencia + " OK.png", ImageFormat.Png);
                Imagen.Dispose();
                Imagen = null;
                SystemSounds.Asterisk.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Dibujo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox_Dibujo.SelectedIndex > -1)
                {
                    Variable_Dibujo = ComboBox_Dibujo.SelectedIndex;
                    Actualizar_Valores();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
