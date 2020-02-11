namespace Minecraft_Tools
{
    partial class Ventana_Adivinación_Número_Mágico
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
            this.Menú_Contextual_Actualizar = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Montón_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Montón_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Montón_3 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_2 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Mostrar_Ayuda = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Aleatorizar_Cartas = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Repartir_Menos_Velocidad = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Restablecer = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_3 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Copiar = new System.Windows.Forms.ToolStripMenuItem();
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
            this.Tabla_Principal = new System.Windows.Forms.TableLayoutPanel();
            this.Picture_Progreso = new System.Windows.Forms.PictureBox();
            this.Picture_3 = new System.Windows.Forms.PictureBox();
            this.Picture_1 = new System.Windows.Forms.PictureBox();
            this.Picture_2 = new System.Windows.Forms.PictureBox();
            this.RichTextBox_Ayuda = new System.Windows.Forms.RichTextBox();
            this.Temporizador_Cartas = new System.Windows.Forms.Timer(this.components);
            this.Menú_Contextual.SuspendLayout();
            this.Barra_Estado.SuspendLayout();
            this.Tabla_Principal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Progreso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_2)).BeginInit();
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
            this.Menú_Contextual_Actualizar,
            this.Menú_Contextual_Montón_1,
            this.Menú_Contextual_Montón_2,
            this.Menú_Contextual_Montón_3,
            this.Menú_Contextual_Separador_2,
            this.Menú_Contextual_Mostrar_Ayuda,
            this.Menú_Contextual_Aleatorizar_Cartas,
            this.Menú_Contextual_Repartir_Menos_Velocidad,
            this.Menú_Contextual_Restablecer,
            this.Menú_Contextual_Separador_3,
            this.Menú_Contextual_Copiar});
            this.Menú_Contextual.Name = "Menú_Contextual";
            this.Menú_Contextual.Size = new System.Drawing.Size(335, 308);
            // 
            // Menú_Contextual_Visor_Ayuda
            // 
            this.Menú_Contextual_Visor_Ayuda.Image = global::Minecraft_Tools.Properties.Resources.Ayuda;
            this.Menú_Contextual_Visor_Ayuda.Name = "Menú_Contextual_Visor_Ayuda";
            this.Menú_Contextual_Visor_Ayuda.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.Menú_Contextual_Visor_Ayuda.Size = new System.Drawing.Size(334, 22);
            this.Menú_Contextual_Visor_Ayuda.Text = "Help viewer...";
            this.Menú_Contextual_Visor_Ayuda.Click += new System.EventHandler(this.Menú_Contextual_Visor_Ayuda_Click);
            // 
            // Menú_Contextual_Acerca
            // 
            this.Menú_Contextual_Acerca.Image = global::Minecraft_Tools.Properties.Resources.Jupisoft_16;
            this.Menú_Contextual_Acerca.Name = "Menú_Contextual_Acerca";
            this.Menú_Contextual_Acerca.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.Menú_Contextual_Acerca.Size = new System.Drawing.Size(334, 22);
            this.Menú_Contextual_Acerca.Text = "About...";
            this.Menú_Contextual_Acerca.Click += new System.EventHandler(this.Menú_Contextual_Acerca_Click);
            // 
            // Menú_Contextual_Depurador_Excepciones
            // 
            this.Menú_Contextual_Depurador_Excepciones.Image = global::Minecraft_Tools.Properties.Resources.Excepción;
            this.Menú_Contextual_Depurador_Excepciones.Name = "Menú_Contextual_Depurador_Excepciones";
            this.Menú_Contextual_Depurador_Excepciones.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.Menú_Contextual_Depurador_Excepciones.Size = new System.Drawing.Size(334, 22);
            this.Menú_Contextual_Depurador_Excepciones.Text = "Exception debugger...";
            this.Menú_Contextual_Depurador_Excepciones.Click += new System.EventHandler(this.Menú_Contextual_Depurador_Excepciones_Click);
            // 
            // Menú_Contextual_Abrir_Carpeta
            // 
            this.Menú_Contextual_Abrir_Carpeta.Image = global::Minecraft_Tools.Properties.Resources.Ejecutar;
            this.Menú_Contextual_Abrir_Carpeta.Name = "Menú_Contextual_Abrir_Carpeta";
            this.Menú_Contextual_Abrir_Carpeta.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.Menú_Contextual_Abrir_Carpeta.Size = new System.Drawing.Size(334, 22);
            this.Menú_Contextual_Abrir_Carpeta.Text = "Open the default save folder...";
            this.Menú_Contextual_Abrir_Carpeta.Click += new System.EventHandler(this.Menú_Contextual_Abrir_Carpeta_Click);
            // 
            // Menú_Contextual_Separador_1
            // 
            this.Menú_Contextual_Separador_1.Name = "Menú_Contextual_Separador_1";
            this.Menú_Contextual_Separador_1.Size = new System.Drawing.Size(331, 6);
            // 
            // Menú_Contextual_Actualizar
            // 
            this.Menú_Contextual_Actualizar.CheckOnClick = true;
            this.Menú_Contextual_Actualizar.Image = global::Minecraft_Tools.Properties.Resources.Actualizar;
            this.Menú_Contextual_Actualizar.Name = "Menú_Contextual_Actualizar";
            this.Menú_Contextual_Actualizar.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.Menú_Contextual_Actualizar.Size = new System.Drawing.Size(334, 22);
            this.Menú_Contextual_Actualizar.Text = "Start a new game (then click on 3 piles)";
            this.Menú_Contextual_Actualizar.Click += new System.EventHandler(this.Menú_Contextual_Actualizar_Click);
            // 
            // Menú_Contextual_Montón_1
            // 
            this.Menú_Contextual_Montón_1.Image = global::Minecraft_Tools.Properties.Resources.Montón_Izquierdo;
            this.Menú_Contextual_Montón_1.Name = "Menú_Contextual_Montón_1";
            this.Menú_Contextual_Montón_1.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.Menú_Contextual_Montón_1.Size = new System.Drawing.Size(334, 22);
            this.Menú_Contextual_Montón_1.Text = "The card I thought was on the left pile";
            this.Menú_Contextual_Montón_1.Click += new System.EventHandler(this.Menú_Contextual_Montón_1_Click);
            // 
            // Menú_Contextual_Montón_2
            // 
            this.Menú_Contextual_Montón_2.Image = global::Minecraft_Tools.Properties.Resources.Montón_Centro;
            this.Menú_Contextual_Montón_2.Name = "Menú_Contextual_Montón_2";
            this.Menú_Contextual_Montón_2.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.Menú_Contextual_Montón_2.Size = new System.Drawing.Size(334, 22);
            this.Menú_Contextual_Montón_2.Text = "The card I thought was on the center pile";
            this.Menú_Contextual_Montón_2.Click += new System.EventHandler(this.Menú_Contextual_Montón_2_Click);
            // 
            // Menú_Contextual_Montón_3
            // 
            this.Menú_Contextual_Montón_3.Image = global::Minecraft_Tools.Properties.Resources.Montón_Derecho;
            this.Menú_Contextual_Montón_3.Name = "Menú_Contextual_Montón_3";
            this.Menú_Contextual_Montón_3.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.Menú_Contextual_Montón_3.Size = new System.Drawing.Size(334, 22);
            this.Menú_Contextual_Montón_3.Text = "The card I thought was on the right pile";
            this.Menú_Contextual_Montón_3.Click += new System.EventHandler(this.Menú_Contextual_Montón_3_Click);
            // 
            // Menú_Contextual_Separador_2
            // 
            this.Menú_Contextual_Separador_2.Name = "Menú_Contextual_Separador_2";
            this.Menú_Contextual_Separador_2.Size = new System.Drawing.Size(331, 6);
            // 
            // Menú_Contextual_Mostrar_Ayuda
            // 
            this.Menú_Contextual_Mostrar_Ayuda.CheckOnClick = true;
            this.Menú_Contextual_Mostrar_Ayuda.Name = "Menú_Contextual_Mostrar_Ayuda";
            this.Menú_Contextual_Mostrar_Ayuda.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.Menú_Contextual_Mostrar_Ayuda.Size = new System.Drawing.Size(334, 22);
            this.Menú_Contextual_Mostrar_Ayuda.Text = "Show a full guide about this magick trick";
            this.Menú_Contextual_Mostrar_Ayuda.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Mostrar_Ayuda_CheckedChanged);
            // 
            // Menú_Contextual_Aleatorizar_Cartas
            // 
            this.Menú_Contextual_Aleatorizar_Cartas.CheckOnClick = true;
            this.Menú_Contextual_Aleatorizar_Cartas.Name = "Menú_Contextual_Aleatorizar_Cartas";
            this.Menú_Contextual_Aleatorizar_Cartas.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.Menú_Contextual_Aleatorizar_Cartas.Size = new System.Drawing.Size(334, 22);
            this.Menú_Contextual_Aleatorizar_Cartas.Text = "Use 21 random cards of the 78 each time";
            this.Menú_Contextual_Aleatorizar_Cartas.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Aleatorizar_Cartas_CheckedChanged);
            // 
            // Menú_Contextual_Repartir_Menos_Velocidad
            // 
            this.Menú_Contextual_Repartir_Menos_Velocidad.CheckOnClick = true;
            this.Menú_Contextual_Repartir_Menos_Velocidad.Name = "Menú_Contextual_Repartir_Menos_Velocidad";
            this.Menú_Contextual_Repartir_Menos_Velocidad.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.Menú_Contextual_Repartir_Menos_Velocidad.Size = new System.Drawing.Size(334, 22);
            this.Menú_Contextual_Repartir_Menos_Velocidad.Text = "Draw the numbers in 12 seconds instead of 6";
            this.Menú_Contextual_Repartir_Menos_Velocidad.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Repartir_Menos_Velocidad_CheckedChanged);
            // 
            // Menú_Contextual_Restablecer
            // 
            this.Menú_Contextual_Restablecer.Image = global::Minecraft_Tools.Properties.Resources.Restablecer;
            this.Menú_Contextual_Restablecer.Name = "Menú_Contextual_Restablecer";
            this.Menú_Contextual_Restablecer.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.Menú_Contextual_Restablecer.Size = new System.Drawing.Size(334, 22);
            this.Menú_Contextual_Restablecer.Text = "Quit any current game";
            this.Menú_Contextual_Restablecer.Click += new System.EventHandler(this.Menú_Contextual_Restablecer_Click);
            // 
            // Menú_Contextual_Separador_3
            // 
            this.Menú_Contextual_Separador_3.Name = "Menú_Contextual_Separador_3";
            this.Menú_Contextual_Separador_3.Size = new System.Drawing.Size(331, 6);
            // 
            // Menú_Contextual_Copiar
            // 
            this.Menú_Contextual_Copiar.Image = global::Minecraft_Tools.Properties.Resources.Copiar;
            this.Menú_Contextual_Copiar.Name = "Menú_Contextual_Copiar";
            this.Menú_Contextual_Copiar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.Menú_Contextual_Copiar.Size = new System.Drawing.Size(334, 22);
            this.Menú_Contextual_Copiar.Text = "Copy the help text";
            this.Menú_Contextual_Copiar.Click += new System.EventHandler(this.Menú_Contextual_Copiar_Click);
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
            this.Barra_Estado_Etiqueta_Sugerencia.Size = new System.Drawing.Size(652, 16);
            this.Barra_Estado_Etiqueta_Sugerencia.Text = "Tip: think of a number between 1 and 21 and tell me 3 times on which pile it was." +
    " Click to start the game or select a pile.";
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
            // Tabla_Principal
            // 
            this.Tabla_Principal.ColumnCount = 3;
            this.Tabla_Principal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.Tabla_Principal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.Tabla_Principal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.Tabla_Principal.Controls.Add(this.Picture_Progreso, 0, 1);
            this.Tabla_Principal.Controls.Add(this.Picture_3, 2, 0);
            this.Tabla_Principal.Controls.Add(this.Picture_1, 0, 0);
            this.Tabla_Principal.Controls.Add(this.Picture_2, 1, 0);
            this.Tabla_Principal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabla_Principal.Location = new System.Drawing.Point(0, 0);
            this.Tabla_Principal.Name = "Tabla_Principal";
            this.Tabla_Principal.RowCount = 2;
            this.Tabla_Principal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Tabla_Principal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.Tabla_Principal.Size = new System.Drawing.Size(884, 436);
            this.Tabla_Principal.TabIndex = 0;
            // 
            // Picture_Progreso
            // 
            this.Picture_Progreso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Picture_Progreso.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Picture_Progreso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Tabla_Principal.SetColumnSpan(this.Picture_Progreso, 3);
            this.Picture_Progreso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Picture_Progreso.InitialImage = null;
            this.Picture_Progreso.Location = new System.Drawing.Point(0, 416);
            this.Picture_Progreso.Margin = new System.Windows.Forms.Padding(0);
            this.Picture_Progreso.Name = "Picture_Progreso";
            this.Picture_Progreso.Size = new System.Drawing.Size(884, 20);
            this.Picture_Progreso.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Picture_Progreso.TabIndex = 9;
            this.Picture_Progreso.TabStop = false;
            // 
            // Picture_3
            // 
            this.Picture_3.BackColor = System.Drawing.Color.White;
            this.Picture_3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Picture_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Picture_3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Picture_3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Picture_3.InitialImage = null;
            this.Picture_3.Location = new System.Drawing.Point(588, 0);
            this.Picture_3.Margin = new System.Windows.Forms.Padding(0);
            this.Picture_3.Name = "Picture_3";
            this.Picture_3.Size = new System.Drawing.Size(296, 416);
            this.Picture_3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Picture_3.TabIndex = 10;
            this.Picture_3.TabStop = false;
            this.Picture_3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_3_MouseDown);
            // 
            // Picture_1
            // 
            this.Picture_1.BackColor = System.Drawing.Color.White;
            this.Picture_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Picture_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Picture_1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Picture_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Picture_1.InitialImage = null;
            this.Picture_1.Location = new System.Drawing.Point(0, 0);
            this.Picture_1.Margin = new System.Windows.Forms.Padding(0);
            this.Picture_1.Name = "Picture_1";
            this.Picture_1.Size = new System.Drawing.Size(294, 416);
            this.Picture_1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Picture_1.TabIndex = 8;
            this.Picture_1.TabStop = false;
            this.Picture_1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_1_MouseDown);
            // 
            // Picture_2
            // 
            this.Picture_2.BackColor = System.Drawing.Color.White;
            this.Picture_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Picture_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Picture_2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Picture_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Picture_2.InitialImage = null;
            this.Picture_2.Location = new System.Drawing.Point(294, 0);
            this.Picture_2.Margin = new System.Windows.Forms.Padding(0);
            this.Picture_2.Name = "Picture_2";
            this.Picture_2.Size = new System.Drawing.Size(294, 416);
            this.Picture_2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Picture_2.TabIndex = 9;
            this.Picture_2.TabStop = false;
            this.Picture_2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_2_MouseDown);
            // 
            // RichTextBox_Ayuda
            // 
            this.RichTextBox_Ayuda.BackColor = System.Drawing.Color.White;
            this.RichTextBox_Ayuda.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichTextBox_Ayuda.ContextMenuStrip = this.Menú_Contextual;
            this.RichTextBox_Ayuda.DetectUrls = false;
            this.RichTextBox_Ayuda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RichTextBox_Ayuda.Location = new System.Drawing.Point(0, 0);
            this.RichTextBox_Ayuda.Margin = new System.Windows.Forms.Padding(0);
            this.RichTextBox_Ayuda.Name = "RichTextBox_Ayuda";
            this.RichTextBox_Ayuda.ReadOnly = true;
            this.RichTextBox_Ayuda.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RichTextBox_Ayuda.ShowSelectionMargin = true;
            this.RichTextBox_Ayuda.Size = new System.Drawing.Size(884, 436);
            this.RichTextBox_Ayuda.TabIndex = 8;
            this.RichTextBox_Ayuda.Text = "";
            this.RichTextBox_Ayuda.Visible = false;
            this.RichTextBox_Ayuda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Adivinación_Número_Mágico_KeyDown);
            // 
            // Temporizador_Cartas
            // 
            this.Temporizador_Cartas.Interval = 285;
            this.Temporizador_Cartas.Tick += new System.EventHandler(this.Temporizador_Cartas_Tick);
            // 
            // Ventana_Adivinación_Número_Mágico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.ContextMenuStrip = this.Menú_Contextual;
            this.Controls.Add(this.Tabla_Principal);
            this.Controls.Add(this.RichTextBox_Ayuda);
            this.Controls.Add(this.Barra_Estado);
            this.DoubleBuffered = true;
            this.Name = "Ventana_Adivinación_Número_Mágico";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Magic Card Guessing by Jupisoft";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventana_Adivinación_Número_Mágico_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventana_Adivinación_Número_Mágico_FormClosed);
            this.Load += new System.EventHandler(this.Ventana_Adivinación_Número_Mágico_Load);
            this.Shown += new System.EventHandler(this.Ventana_Adivinación_Número_Mágico_Shown);
            this.SizeChanged += new System.EventHandler(this.Ventana_Adivinación_Número_Mágico_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Adivinación_Número_Mágico_KeyDown);
            this.Menú_Contextual.ResumeLayout(false);
            this.Barra_Estado.ResumeLayout(false);
            this.Barra_Estado.PerformLayout();
            this.Tabla_Principal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Progreso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_2)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Actualizar;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Montón_1;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Montón_2;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Montón_3;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_2;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Mostrar_Ayuda;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Aleatorizar_Cartas;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Repartir_Menos_Velocidad;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Restablecer;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_3;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Copiar;
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
        private System.Windows.Forms.TableLayoutPanel Tabla_Principal;
        private System.Windows.Forms.PictureBox Picture_1;
        private System.Windows.Forms.PictureBox Picture_2;
        private System.Windows.Forms.PictureBox Picture_3;
        private System.Windows.Forms.RichTextBox RichTextBox_Ayuda;
        private System.Windows.Forms.Timer Temporizador_Cartas;
        private System.Windows.Forms.PictureBox Picture_Progreso;
    }
}