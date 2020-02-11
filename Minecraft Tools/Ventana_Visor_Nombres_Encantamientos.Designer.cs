namespace Minecraft_Tools
{
    partial class Ventana_Visor_Nombres_Encantamientos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Menú_Contextual = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Menú_Contextual_Visor_Ayuda = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Acerca = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Depurador_Excepciones = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Abrir_Carpeta = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_1 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Aleatorizar = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Seleccionar_Color_Fondo = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Seleccionar_Color_Fuente = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Ajuste_Línea = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_2 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Copiar_SGA = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Copiar = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_3 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Guardar_SGA = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Guardar = new System.Windows.Forms.ToolStripMenuItem();
            this.Barra_Estado = new System.Windows.Forms.ToolStrip();
            this.Barra_Estado_Botón_Excepción = new System.Windows.Forms.ToolStripButton();
            this.Barra_Estado_Separador_1 = new System.Windows.Forms.ToolStripSeparator();
            this.Barra_Estado_Etiqueta_CPU = new System.Windows.Forms.ToolStripLabel();
            this.Barra_Estado_Separador_2 = new System.Windows.Forms.ToolStripSeparator();
            this.Barra_Estado_Etiqueta_Memoria = new System.Windows.Forms.ToolStripLabel();
            this.Barra_Estado_Separador_3 = new System.Windows.Forms.ToolStripSeparator();
            this.Barra_Estado_Etiqueta_FPS = new System.Windows.Forms.ToolStripLabel();
            this.Barra_Estado_Separador_4 = new System.Windows.Forms.ToolStripSeparator();
            this.Barra_Estado_Etiqueta_Sugerencia = new System.Windows.Forms.ToolStripLabel();
            this.Barra_Estado_Separador_5 = new System.Windows.Forms.ToolStripSeparator();
            this.Temporizador_Principal = new System.Windows.Forms.Timer(this.components);
            this.ComboBox_Palabras = new System.Windows.Forms.ComboBox();
            this.Etiqueta_Palabras = new System.Windows.Forms.Label();
            this.Picture = new System.Windows.Forms.PictureBox();
            this.Tabla_Principal = new System.Windows.Forms.TableLayoutPanel();
            this.Numérico_Zoom = new System.Windows.Forms.NumericUpDown();
            this.Picture_SGA = new System.Windows.Forms.PictureBox();
            this.Etiqueta_Zoom = new System.Windows.Forms.Label();
            this.Menú_Contextual.SuspendLayout();
            this.Barra_Estado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            this.Tabla_Principal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numérico_Zoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_SGA)).BeginInit();
            this.SuspendLayout();
            // 
            // Menú_Contextual
            // 
            this.Menú_Contextual.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menú_Contextual_Visor_Ayuda,
            this.Menú_Contextual_Acerca,
            this.Menú_Contextual_Depurador_Excepciones,
            this.Menú_Contextual_Abrir_Carpeta,
            this.Menú_Contextual_Separador_1,
            this.Menú_Contextual_Aleatorizar,
            this.Menú_Contextual_Seleccionar_Color_Fondo,
            this.Menú_Contextual_Seleccionar_Color_Fuente,
            this.Menú_Contextual_Ajuste_Línea,
            this.Menú_Contextual_Separador_2,
            this.Menú_Contextual_Copiar_SGA,
            this.Menú_Contextual_Copiar,
            this.Menú_Contextual_Separador_3,
            this.Menú_Contextual_Guardar_SGA,
            this.Menú_Contextual_Guardar});
            this.Menú_Contextual.Name = "Menú_Contextual";
            this.Menú_Contextual.Size = new System.Drawing.Size(452, 286);
            // 
            // Menú_Contextual_Visor_Ayuda
            // 
            this.Menú_Contextual_Visor_Ayuda.Image = global::Minecraft_Tools.Properties.Resources.Ayuda;
            this.Menú_Contextual_Visor_Ayuda.Name = "Menú_Contextual_Visor_Ayuda";
            this.Menú_Contextual_Visor_Ayuda.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.Menú_Contextual_Visor_Ayuda.Size = new System.Drawing.Size(451, 22);
            this.Menú_Contextual_Visor_Ayuda.Text = "Help viewer...";
            this.Menú_Contextual_Visor_Ayuda.Click += new System.EventHandler(this.Menú_Contextual_Visor_Ayuda_Click);
            // 
            // Menú_Contextual_Acerca
            // 
            this.Menú_Contextual_Acerca.Image = global::Minecraft_Tools.Properties.Resources.Jupisoft_16;
            this.Menú_Contextual_Acerca.Name = "Menú_Contextual_Acerca";
            this.Menú_Contextual_Acerca.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.Menú_Contextual_Acerca.Size = new System.Drawing.Size(451, 22);
            this.Menú_Contextual_Acerca.Text = "About...";
            this.Menú_Contextual_Acerca.Click += new System.EventHandler(this.Menú_Contextual_Acerca_Click);
            // 
            // Menú_Contextual_Depurador_Excepciones
            // 
            this.Menú_Contextual_Depurador_Excepciones.Image = global::Minecraft_Tools.Properties.Resources.Excepción;
            this.Menú_Contextual_Depurador_Excepciones.Name = "Menú_Contextual_Depurador_Excepciones";
            this.Menú_Contextual_Depurador_Excepciones.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.Menú_Contextual_Depurador_Excepciones.Size = new System.Drawing.Size(451, 22);
            this.Menú_Contextual_Depurador_Excepciones.Text = "Exception debugger...";
            this.Menú_Contextual_Depurador_Excepciones.Click += new System.EventHandler(this.Menú_Contextual_Depurador_Excepciones_Click);
            // 
            // Menú_Contextual_Abrir_Carpeta
            // 
            this.Menú_Contextual_Abrir_Carpeta.Image = global::Minecraft_Tools.Properties.Resources.Ejecutar;
            this.Menú_Contextual_Abrir_Carpeta.Name = "Menú_Contextual_Abrir_Carpeta";
            this.Menú_Contextual_Abrir_Carpeta.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.Menú_Contextual_Abrir_Carpeta.Size = new System.Drawing.Size(451, 22);
            this.Menú_Contextual_Abrir_Carpeta.Text = "Open the default save folder...";
            this.Menú_Contextual_Abrir_Carpeta.Click += new System.EventHandler(this.Menú_Contextual_Abrir_Carpeta_Click);
            // 
            // Menú_Contextual_Separador_1
            // 
            this.Menú_Contextual_Separador_1.Name = "Menú_Contextual_Separador_1";
            this.Menú_Contextual_Separador_1.Size = new System.Drawing.Size(448, 6);
            // 
            // Menú_Contextual_Aleatorizar
            // 
            this.Menú_Contextual_Aleatorizar.Image = global::Minecraft_Tools.Properties.Resources.Aleatorio;
            this.Menú_Contextual_Aleatorizar.Name = "Menú_Contextual_Aleatorizar";
            this.Menú_Contextual_Aleatorizar.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.Menú_Contextual_Aleatorizar.Size = new System.Drawing.Size(451, 22);
            this.Menú_Contextual_Aleatorizar.Text = "Randomize the text using the Minecraft source code";
            this.Menú_Contextual_Aleatorizar.Click += new System.EventHandler(this.Menú_Contextual_Aleatorizar_Click);
            // 
            // Menú_Contextual_Seleccionar_Color_Fondo
            // 
            this.Menú_Contextual_Seleccionar_Color_Fondo.Image = global::Minecraft_Tools.Properties.Resources.Selector_Color_Windows;
            this.Menú_Contextual_Seleccionar_Color_Fondo.Name = "Menú_Contextual_Seleccionar_Color_Fondo";
            this.Menú_Contextual_Seleccionar_Color_Fondo.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.Menú_Contextual_Seleccionar_Color_Fondo.Size = new System.Drawing.Size(451, 22);
            this.Menú_Contextual_Seleccionar_Color_Fondo.Text = "Select the background color...";
            this.Menú_Contextual_Seleccionar_Color_Fondo.Click += new System.EventHandler(this.Menú_Contextual_Seleccionar_Color_Fondo_Click);
            // 
            // Menú_Contextual_Seleccionar_Color_Fuente
            // 
            this.Menú_Contextual_Seleccionar_Color_Fuente.Image = global::Minecraft_Tools.Properties.Resources.Selector_Color_Windows;
            this.Menú_Contextual_Seleccionar_Color_Fuente.Name = "Menú_Contextual_Seleccionar_Color_Fuente";
            this.Menú_Contextual_Seleccionar_Color_Fuente.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.Menú_Contextual_Seleccionar_Color_Fuente.Size = new System.Drawing.Size(451, 22);
            this.Menú_Contextual_Seleccionar_Color_Fuente.Text = "Select the foreground color...";
            this.Menú_Contextual_Seleccionar_Color_Fuente.Click += new System.EventHandler(this.Menú_Contextual_Seleccionar_Color_Fuente_Click);
            // 
            // Menú_Contextual_Ajuste_Línea
            // 
            this.Menú_Contextual_Ajuste_Línea.Checked = true;
            this.Menú_Contextual_Ajuste_Línea.CheckOnClick = true;
            this.Menú_Contextual_Ajuste_Línea.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menú_Contextual_Ajuste_Línea.Name = "Menú_Contextual_Ajuste_Línea";
            this.Menú_Contextual_Ajuste_Línea.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.Menú_Contextual_Ajuste_Línea.Size = new System.Drawing.Size(451, 22);
            this.Menú_Contextual_Ajuste_Línea.Text = "Word wrap";
            this.Menú_Contextual_Ajuste_Línea.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Ajuste_Línea_CheckedChanged);
            // 
            // Menú_Contextual_Separador_2
            // 
            this.Menú_Contextual_Separador_2.Name = "Menú_Contextual_Separador_2";
            this.Menú_Contextual_Separador_2.Size = new System.Drawing.Size(448, 6);
            // 
            // Menú_Contextual_Copiar_SGA
            // 
            this.Menú_Contextual_Copiar_SGA.Image = global::Minecraft_Tools.Properties.Resources.Copiar;
            this.Menú_Contextual_Copiar_SGA.Name = "Menú_Contextual_Copiar_SGA";
            this.Menú_Contextual_Copiar_SGA.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.Menú_Contextual_Copiar_SGA.Size = new System.Drawing.Size(451, 22);
            this.Menú_Contextual_Copiar_SGA.Text = "Copy the Standard Galactic Alphabet (SGA) text as a PNG image";
            this.Menú_Contextual_Copiar_SGA.Click += new System.EventHandler(this.Menú_Contextual_Copiar_SGA_Click);
            // 
            // Menú_Contextual_Copiar
            // 
            this.Menú_Contextual_Copiar.Image = global::Minecraft_Tools.Properties.Resources.Copiar;
            this.Menú_Contextual_Copiar.Name = "Menú_Contextual_Copiar";
            this.Menú_Contextual_Copiar.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.Menú_Contextual_Copiar.Size = new System.Drawing.Size(451, 22);
            this.Menú_Contextual_Copiar.Text = "Copy the regular Minecraft text as a PNG image";
            this.Menú_Contextual_Copiar.Click += new System.EventHandler(this.Menú_Contextual_Copiar_Click);
            // 
            // Menú_Contextual_Separador_3
            // 
            this.Menú_Contextual_Separador_3.Name = "Menú_Contextual_Separador_3";
            this.Menú_Contextual_Separador_3.Size = new System.Drawing.Size(448, 6);
            // 
            // Menú_Contextual_Guardar_SGA
            // 
            this.Menú_Contextual_Guardar_SGA.Image = global::Minecraft_Tools.Properties.Resources.Guardar;
            this.Menú_Contextual_Guardar_SGA.Name = "Menú_Contextual_Guardar_SGA";
            this.Menú_Contextual_Guardar_SGA.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.Menú_Contextual_Guardar_SGA.Size = new System.Drawing.Size(451, 22);
            this.Menú_Contextual_Guardar_SGA.Text = "Save the Standard Galactic Alphabet (SGA) text as a PNG image";
            this.Menú_Contextual_Guardar_SGA.Click += new System.EventHandler(this.Menú_Contextual_Guardar_SGA_Click);
            // 
            // Menú_Contextual_Guardar
            // 
            this.Menú_Contextual_Guardar.Image = global::Minecraft_Tools.Properties.Resources.Guardar;
            this.Menú_Contextual_Guardar.Name = "Menú_Contextual_Guardar";
            this.Menú_Contextual_Guardar.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.Menú_Contextual_Guardar.Size = new System.Drawing.Size(451, 22);
            this.Menú_Contextual_Guardar.Text = "Save the regular Minecraft text as a PNG image";
            this.Menú_Contextual_Guardar.Click += new System.EventHandler(this.Menú_Contextual_Guardar_Click);
            // 
            // Barra_Estado
            // 
            this.Barra_Estado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Barra_Estado.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.Barra_Estado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Barra_Estado_Botón_Excepción,
            this.Barra_Estado_Separador_1,
            this.Barra_Estado_Etiqueta_CPU,
            this.Barra_Estado_Separador_2,
            this.Barra_Estado_Etiqueta_Memoria,
            this.Barra_Estado_Separador_3,
            this.Barra_Estado_Etiqueta_FPS,
            this.Barra_Estado_Separador_4,
            this.Barra_Estado_Etiqueta_Sugerencia,
            this.Barra_Estado_Separador_5});
            this.Barra_Estado.Location = new System.Drawing.Point(0, 436);
            this.Barra_Estado.Name = "Barra_Estado";
            this.Barra_Estado.Size = new System.Drawing.Size(884, 25);
            this.Barra_Estado.TabIndex = 1;
            this.Barra_Estado.Text = "Status bar";
            // 
            // Barra_Estado_Botón_Excepción
            // 
            this.Barra_Estado_Botón_Excepción.AutoToolTip = false;
            this.Barra_Estado_Botón_Excepción.Image = global::Minecraft_Tools.Properties.Resources.Excepción_Gris;
            this.Barra_Estado_Botón_Excepción.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.Barra_Estado_Botón_Excepción.Name = "Barra_Estado_Botón_Excepción";
            this.Barra_Estado_Botón_Excepción.Size = new System.Drawing.Size(95, 22);
            this.Barra_Estado_Botón_Excepción.Text = "Exceptions: 0";
            this.Barra_Estado_Botón_Excepción.Visible = false;
            this.Barra_Estado_Botón_Excepción.Click += new System.EventHandler(this.Barra_Estado_Botón_Excepción_Click);
            // 
            // Barra_Estado_Separador_1
            // 
            this.Barra_Estado_Separador_1.Name = "Barra_Estado_Separador_1";
            this.Barra_Estado_Separador_1.Size = new System.Drawing.Size(6, 25);
            this.Barra_Estado_Separador_1.Visible = false;
            // 
            // Barra_Estado_Etiqueta_CPU
            // 
            this.Barra_Estado_Etiqueta_CPU.Image = global::Minecraft_Tools.Properties.Resources.Procesador;
            this.Barra_Estado_Etiqueta_CPU.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.Barra_Estado_Etiqueta_CPU.Name = "Barra_Estado_Etiqueta_CPU";
            this.Barra_Estado_Etiqueta_CPU.Size = new System.Drawing.Size(71, 22);
            this.Barra_Estado_Etiqueta_CPU.Text = "CPU: 0 %";
            // 
            // Barra_Estado_Separador_2
            // 
            this.Barra_Estado_Separador_2.Name = "Barra_Estado_Separador_2";
            this.Barra_Estado_Separador_2.Size = new System.Drawing.Size(6, 25);
            // 
            // Barra_Estado_Etiqueta_Memoria
            // 
            this.Barra_Estado_Etiqueta_Memoria.Image = global::Minecraft_Tools.Properties.Resources.Memoria;
            this.Barra_Estado_Etiqueta_Memoria.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.Barra_Estado_Etiqueta_Memoria.Name = "Barra_Estado_Etiqueta_Memoria";
            this.Barra_Estado_Etiqueta_Memoria.Size = new System.Drawing.Size(82, 22);
            this.Barra_Estado_Etiqueta_Memoria.Text = "RAM: 0 MB";
            // 
            // Barra_Estado_Separador_3
            // 
            this.Barra_Estado_Separador_3.Name = "Barra_Estado_Separador_3";
            this.Barra_Estado_Separador_3.Size = new System.Drawing.Size(6, 25);
            // 
            // Barra_Estado_Etiqueta_FPS
            // 
            this.Barra_Estado_Etiqueta_FPS.Image = global::Minecraft_Tools.Properties.Resources.Temporizador;
            this.Barra_Estado_Etiqueta_FPS.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.Barra_Estado_Etiqueta_FPS.Name = "Barra_Estado_Etiqueta_FPS";
            this.Barra_Estado_Etiqueta_FPS.Size = new System.Drawing.Size(54, 22);
            this.Barra_Estado_Etiqueta_FPS.Text = "FPS: 0";
            // 
            // Barra_Estado_Separador_4
            // 
            this.Barra_Estado_Separador_4.Name = "Barra_Estado_Separador_4";
            this.Barra_Estado_Separador_4.Size = new System.Drawing.Size(6, 25);
            // 
            // Barra_Estado_Etiqueta_Sugerencia
            // 
            this.Barra_Estado_Etiqueta_Sugerencia.Image = global::Minecraft_Tools.Properties.Resources.Ayuda;
            this.Barra_Estado_Etiqueta_Sugerencia.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.Barra_Estado_Etiqueta_Sugerencia.Name = "Barra_Estado_Etiqueta_Sugerencia";
            this.Barra_Estado_Etiqueta_Sugerencia.Size = new System.Drawing.Size(709, 16);
            this.Barra_Estado_Etiqueta_Sugerencia.Text = "Tip: Minecraft uses the Standard Galactic Alphabet on the enchanting table, but t" +
    "he names shown are randomized and not related.";
            // 
            // Barra_Estado_Separador_5
            // 
            this.Barra_Estado_Separador_5.Name = "Barra_Estado_Separador_5";
            this.Barra_Estado_Separador_5.Size = new System.Drawing.Size(6, 25);
            // 
            // Temporizador_Principal
            // 
            this.Temporizador_Principal.Interval = 1;
            this.Temporizador_Principal.Tick += new System.EventHandler(this.Temporizador_Principal_Tick);
            // 
            // ComboBox_Palabras
            // 
            this.ComboBox_Palabras.BackColor = System.Drawing.Color.White;
            this.ComboBox_Palabras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComboBox_Palabras.FormattingEnabled = true;
            this.ComboBox_Palabras.Location = new System.Drawing.Point(47, 0);
            this.ComboBox_Palabras.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.ComboBox_Palabras.Name = "ComboBox_Palabras";
            this.ComboBox_Palabras.Size = new System.Drawing.Size(694, 21);
            this.ComboBox_Palabras.TabIndex = 1;
            this.ComboBox_Palabras.TextChanged += new System.EventHandler(this.ComboBox_Palabras_TextChanged);
            this.ComboBox_Palabras.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Visor_Nombres_Encantamientos_KeyDown);
            this.ComboBox_Palabras.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ComboBox_Palabras_MouseDown);
            // 
            // Etiqueta_Palabras
            // 
            this.Etiqueta_Palabras.AutoSize = true;
            this.Etiqueta_Palabras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Etiqueta_Palabras.Location = new System.Drawing.Point(3, 4);
            this.Etiqueta_Palabras.Margin = new System.Windows.Forms.Padding(3, 4, 0, 0);
            this.Etiqueta_Palabras.Name = "Etiqueta_Palabras";
            this.Etiqueta_Palabras.Size = new System.Drawing.Size(41, 17);
            this.Etiqueta_Palabras.TabIndex = 0;
            this.Etiqueta_Palabras.Text = "Words:";
            // 
            // Picture
            // 
            this.Picture.BackColor = System.Drawing.Color.White;
            this.Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Tabla_Principal.SetColumnSpan(this.Picture, 4);
            this.Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Picture.InitialImage = null;
            this.Picture.Location = new System.Drawing.Point(0, 230);
            this.Picture.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.Picture.Name = "Picture";
            this.Picture.Size = new System.Drawing.Size(884, 206);
            this.Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Picture.TabIndex = 4;
            this.Picture.TabStop = false;
            this.Picture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_MouseDown);
            // 
            // Tabla_Principal
            // 
            this.Tabla_Principal.ColumnCount = 4;
            this.Tabla_Principal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.Tabla_Principal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Tabla_Principal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.Tabla_Principal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.Tabla_Principal.Controls.Add(this.Picture, 0, 2);
            this.Tabla_Principal.Controls.Add(this.Numérico_Zoom, 3, 0);
            this.Tabla_Principal.Controls.Add(this.Picture_SGA, 0, 1);
            this.Tabla_Principal.Controls.Add(this.Etiqueta_Palabras, 0, 0);
            this.Tabla_Principal.Controls.Add(this.ComboBox_Palabras, 1, 0);
            this.Tabla_Principal.Controls.Add(this.Etiqueta_Zoom, 2, 0);
            this.Tabla_Principal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabla_Principal.Location = new System.Drawing.Point(0, 0);
            this.Tabla_Principal.Margin = new System.Windows.Forms.Padding(0);
            this.Tabla_Principal.Name = "Tabla_Principal";
            this.Tabla_Principal.RowCount = 3;
            this.Tabla_Principal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.Tabla_Principal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Tabla_Principal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Tabla_Principal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.Tabla_Principal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.Tabla_Principal.Size = new System.Drawing.Size(884, 436);
            this.Tabla_Principal.TabIndex = 0;
            // 
            // Numérico_Zoom
            // 
            this.Numérico_Zoom.BackColor = System.Drawing.Color.White;
            this.Numérico_Zoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Numérico_Zoom.Location = new System.Drawing.Point(787, 0);
            this.Numérico_Zoom.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.Numérico_Zoom.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.Numérico_Zoom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Numérico_Zoom.Name = "Numérico_Zoom";
            this.Numérico_Zoom.Size = new System.Drawing.Size(94, 20);
            this.Numérico_Zoom.TabIndex = 3;
            this.Numérico_Zoom.ThousandsSeparator = true;
            this.Numérico_Zoom.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.Numérico_Zoom.ValueChanged += new System.EventHandler(this.Numérico_Zoom_ValueChanged);
            this.Numérico_Zoom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Visor_Nombres_Encantamientos_KeyDown);
            // 
            // Picture_SGA
            // 
            this.Picture_SGA.BackColor = System.Drawing.Color.White;
            this.Picture_SGA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Tabla_Principal.SetColumnSpan(this.Picture_SGA, 4);
            this.Picture_SGA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Picture_SGA.InitialImage = null;
            this.Picture_SGA.Location = new System.Drawing.Point(0, 21);
            this.Picture_SGA.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.Picture_SGA.Name = "Picture_SGA";
            this.Picture_SGA.Size = new System.Drawing.Size(884, 205);
            this.Picture_SGA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Picture_SGA.TabIndex = 5;
            this.Picture_SGA.TabStop = false;
            this.Picture_SGA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_SGA_MouseDown);
            // 
            // Etiqueta_Zoom
            // 
            this.Etiqueta_Zoom.AutoSize = true;
            this.Etiqueta_Zoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Etiqueta_Zoom.Location = new System.Drawing.Point(747, 4);
            this.Etiqueta_Zoom.Margin = new System.Windows.Forms.Padding(3, 4, 0, 0);
            this.Etiqueta_Zoom.Name = "Etiqueta_Zoom";
            this.Etiqueta_Zoom.Size = new System.Drawing.Size(37, 17);
            this.Etiqueta_Zoom.TabIndex = 2;
            this.Etiqueta_Zoom.Text = "Zoom:";
            // 
            // Ventana_Visor_Nombres_Encantamientos
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.ContextMenuStrip = this.Menú_Contextual;
            this.Controls.Add(this.Tabla_Principal);
            this.Controls.Add(this.Barra_Estado);
            this.DoubleBuffered = true;
            this.Name = "Ventana_Visor_Nombres_Encantamientos";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enchantment Names Viewer by Jupisoft";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventana_Visor_Nombres_Encantamientos_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventana_Visor_Nombres_Encantamientos_FormClosed);
            this.Load += new System.EventHandler(this.Ventana_Visor_Nombres_Encantamientos_Load);
            this.Shown += new System.EventHandler(this.Ventana_Visor_Nombres_Encantamientos_Shown);
            this.SizeChanged += new System.EventHandler(this.Ventana_Visor_Nombres_Encantamientos_SizeChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Ventana_Visor_Nombres_Encantamientos_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Ventana_Visor_Nombres_Encantamientos_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Visor_Nombres_Encantamientos_KeyDown);
            this.Menú_Contextual.ResumeLayout(false);
            this.Barra_Estado.ResumeLayout(false);
            this.Barra_Estado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            this.Tabla_Principal.ResumeLayout(false);
            this.Tabla_Principal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numérico_Zoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_SGA)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip Menú_Contextual;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Visor_Ayuda;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Acerca;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Depurador_Excepciones;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Abrir_Carpeta;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_1;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Aleatorizar;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Ajuste_Línea;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Seleccionar_Color_Fondo;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_2;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_3;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Copiar;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Guardar;
        private System.Windows.Forms.ToolStrip Barra_Estado;
        private System.Windows.Forms.ToolStripButton Barra_Estado_Botón_Excepción;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_1;
        private System.Windows.Forms.ToolStripLabel Barra_Estado_Etiqueta_CPU;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_2;
        private System.Windows.Forms.ToolStripLabel Barra_Estado_Etiqueta_Memoria;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_3;
        private System.Windows.Forms.ToolStripLabel Barra_Estado_Etiqueta_FPS;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_4;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_5;
        private System.Windows.Forms.ToolStripLabel Barra_Estado_Etiqueta_Sugerencia;
        private System.Windows.Forms.Timer Temporizador_Principal;
        private System.Windows.Forms.PictureBox Picture;
        private System.Windows.Forms.ComboBox ComboBox_Palabras;
        private System.Windows.Forms.Label Etiqueta_Palabras;
        private System.Windows.Forms.TableLayoutPanel Tabla_Principal;
        private System.Windows.Forms.PictureBox Picture_SGA;
        private System.Windows.Forms.Label Etiqueta_Zoom;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Copiar_SGA;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Guardar_SGA;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Seleccionar_Color_Fuente;
        private System.Windows.Forms.NumericUpDown Numérico_Zoom;
    }
}