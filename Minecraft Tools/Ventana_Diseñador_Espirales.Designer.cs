namespace Minecraft_Tools
{
    partial class Ventana_Diseñador_Espirales
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
            this.Menú_Contextual_Copiar = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_2 = new System.Windows.Forms.ToolStripSeparator();
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Picture = new System.Windows.Forms.PictureBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.Etiqueta_Ancho = new System.Windows.Forms.Label();
            this.NumericUpDown_Ancho = new System.Windows.Forms.NumericUpDown();
            this.NumericUpDown_Alto = new System.Windows.Forms.NumericUpDown();
            this.Etiqueta_Alto = new System.Windows.Forms.Label();
            this.NumericUpDown_Escalones = new System.Windows.Forms.NumericUpDown();
            this.Etiqueta_Escalones = new System.Windows.Forms.Label();
            this.CheckBox_Simetría = new System.Windows.Forms.CheckBox();
            this.Etiqueta_Porcentaje_Espacio = new System.Windows.Forms.Label();
            this.NumericUpDown_Porcentaje_Espacio = new System.Windows.Forms.NumericUpDown();
            this.Etiqueta_Porcentaje = new System.Windows.Forms.Label();
            this.CheckBox_Borde = new System.Windows.Forms.CheckBox();
            this.Menú_Contextual.SuspendLayout();
            this.Barra_Estado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Ancho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Alto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Escalones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Porcentaje_Espacio)).BeginInit();
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
            this.Menú_Contextual_Copiar,
            this.Menú_Contextual_Separador_2,
            this.Menú_Contextual_Guardar});
            this.Menú_Contextual.Name = "Menú_Contextual";
            this.Menú_Contextual.Size = new System.Drawing.Size(252, 148);
            // 
            // Menú_Contextual_Visor_Ayuda
            // 
            this.Menú_Contextual_Visor_Ayuda.Image = global::Minecraft_Tools.Properties.Resources.Ayuda;
            this.Menú_Contextual_Visor_Ayuda.Name = "Menú_Contextual_Visor_Ayuda";
            this.Menú_Contextual_Visor_Ayuda.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.Menú_Contextual_Visor_Ayuda.Size = new System.Drawing.Size(251, 22);
            this.Menú_Contextual_Visor_Ayuda.Text = "Help viewer...";
            this.Menú_Contextual_Visor_Ayuda.Click += new System.EventHandler(this.Menú_Contextual_Visor_Ayuda_Click);
            // 
            // Menú_Contextual_Acerca
            // 
            this.Menú_Contextual_Acerca.Image = global::Minecraft_Tools.Properties.Resources.Jupisoft_16;
            this.Menú_Contextual_Acerca.Name = "Menú_Contextual_Acerca";
            this.Menú_Contextual_Acerca.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.Menú_Contextual_Acerca.Size = new System.Drawing.Size(251, 22);
            this.Menú_Contextual_Acerca.Text = "About...";
            this.Menú_Contextual_Acerca.Click += new System.EventHandler(this.Menú_Contextual_Acerca_Click);
            // 
            // Menú_Contextual_Depurador_Excepciones
            // 
            this.Menú_Contextual_Depurador_Excepciones.Image = global::Minecraft_Tools.Properties.Resources.Excepción;
            this.Menú_Contextual_Depurador_Excepciones.Name = "Menú_Contextual_Depurador_Excepciones";
            this.Menú_Contextual_Depurador_Excepciones.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.Menú_Contextual_Depurador_Excepciones.Size = new System.Drawing.Size(251, 22);
            this.Menú_Contextual_Depurador_Excepciones.Text = "Exception debugger...";
            this.Menú_Contextual_Depurador_Excepciones.Click += new System.EventHandler(this.Menú_Contextual_Depurador_Excepciones_Click);
            // 
            // Menú_Contextual_Abrir_Carpeta
            // 
            this.Menú_Contextual_Abrir_Carpeta.Image = global::Minecraft_Tools.Properties.Resources.Ejecutar;
            this.Menú_Contextual_Abrir_Carpeta.Name = "Menú_Contextual_Abrir_Carpeta";
            this.Menú_Contextual_Abrir_Carpeta.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.Menú_Contextual_Abrir_Carpeta.Size = new System.Drawing.Size(251, 22);
            this.Menú_Contextual_Abrir_Carpeta.Text = "Open the default save folder...";
            this.Menú_Contextual_Abrir_Carpeta.Click += new System.EventHandler(this.Menú_Contextual_Abrir_Carpeta_Click);
            // 
            // Menú_Contextual_Separador_1
            // 
            this.Menú_Contextual_Separador_1.Name = "Menú_Contextual_Separador_1";
            this.Menú_Contextual_Separador_1.Size = new System.Drawing.Size(248, 6);
            // 
            // Menú_Contextual_Copiar
            // 
            this.Menú_Contextual_Copiar.Image = global::Minecraft_Tools.Properties.Resources.Copiar;
            this.Menú_Contextual_Copiar.Name = "Menú_Contextual_Copiar";
            this.Menú_Contextual_Copiar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.Menú_Contextual_Copiar.Size = new System.Drawing.Size(251, 22);
            this.Menú_Contextual_Copiar.Text = "Copy";
            this.Menú_Contextual_Copiar.Click += new System.EventHandler(this.Menú_Contextual_Copiar_Click);
            // 
            // Menú_Contextual_Separador_2
            // 
            this.Menú_Contextual_Separador_2.Name = "Menú_Contextual_Separador_2";
            this.Menú_Contextual_Separador_2.Size = new System.Drawing.Size(248, 6);
            // 
            // Menú_Contextual_Guardar
            // 
            this.Menú_Contextual_Guardar.Image = global::Minecraft_Tools.Properties.Resources.Guardar;
            this.Menú_Contextual_Guardar.Name = "Menú_Contextual_Guardar";
            this.Menú_Contextual_Guardar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.Menú_Contextual_Guardar.Size = new System.Drawing.Size(251, 22);
            this.Menú_Contextual_Guardar.Text = "Save";
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
            this.Barra_Estado.Size = new System.Drawing.Size(624, 25);
            this.Barra_Estado.TabIndex = 6;
            this.Barra_Estado.Text = "Status bar";
            // 
            // Barra_Estado_Botón_Excepción
            // 
            this.Barra_Estado_Botón_Excepción.AutoToolTip = false;
            this.Barra_Estado_Botón_Excepción.Image = global::Minecraft_Tools.Properties.Resources.Excepción_Gris;
            this.Barra_Estado_Botón_Excepción.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.Barra_Estado_Botón_Excepción.Name = "Barra_Estado_Botón_Excepción";
            this.Barra_Estado_Botón_Excepción.Size = new System.Drawing.Size(96, 22);
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
            this.Barra_Estado_Etiqueta_Sugerencia.Size = new System.Drawing.Size(597, 16);
            this.Barra_Estado_Etiqueta_Sugerencia.Text = "Tip: each color is a new height (half block upper or lower), so build it with sla" +
    "bs to turn it into real spiral stairs.";
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
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(592, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(20, 20);
            this.textBox1.TabIndex = 12;
            this.textBox1.Visible = false;
            // 
            // Picture
            // 
            this.Picture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Picture.BackColor = System.Drawing.Color.White;
            this.Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Picture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Picture.InitialImage = null;
            this.Picture.Location = new System.Drawing.Point(12, 116);
            this.Picture.Name = "Picture";
            this.Picture.Size = new System.Drawing.Size(600, 317);
            this.Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Picture.TabIndex = 5;
            this.Picture.TabStop = false;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(592, 38);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(20, 20);
            this.textBox2.TabIndex = 13;
            this.textBox2.Visible = false;
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.BackColor = System.Drawing.Color.White;
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(592, 64);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(20, 20);
            this.textBox3.TabIndex = 14;
            this.textBox3.Visible = false;
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox4.BackColor = System.Drawing.Color.White;
            this.textBox4.Enabled = false;
            this.textBox4.Location = new System.Drawing.Point(592, 90);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(20, 20);
            this.textBox4.TabIndex = 15;
            this.textBox4.Visible = false;
            // 
            // textBox5
            // 
            this.textBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox5.BackColor = System.Drawing.Color.White;
            this.textBox5.Enabled = false;
            this.textBox5.Location = new System.Drawing.Point(592, 116);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(20, 20);
            this.textBox5.TabIndex = 16;
            this.textBox5.Visible = false;
            // 
            // Etiqueta_Ancho
            // 
            this.Etiqueta_Ancho.AutoSize = true;
            this.Etiqueta_Ancho.Location = new System.Drawing.Point(12, 15);
            this.Etiqueta_Ancho.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.Etiqueta_Ancho.Name = "Etiqueta_Ancho";
            this.Etiqueta_Ancho.Size = new System.Drawing.Size(38, 13);
            this.Etiqueta_Ancho.TabIndex = 7;
            this.Etiqueta_Ancho.Text = "Width:";
            // 
            // NumericUpDown_Ancho
            // 
            this.NumericUpDown_Ancho.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NumericUpDown_Ancho.BackColor = System.Drawing.Color.White;
            this.NumericUpDown_Ancho.Location = new System.Drawing.Point(56, 12);
            this.NumericUpDown_Ancho.Maximum = new decimal(new int[] {
            16384,
            0,
            0,
            0});
            this.NumericUpDown_Ancho.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.NumericUpDown_Ancho.Name = "NumericUpDown_Ancho";
            this.NumericUpDown_Ancho.Size = new System.Drawing.Size(556, 20);
            this.NumericUpDown_Ancho.TabIndex = 8;
            this.NumericUpDown_Ancho.ThousandsSeparator = true;
            this.NumericUpDown_Ancho.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.NumericUpDown_Ancho.ValueChanged += new System.EventHandler(this.NumericUpDown_Ancho_ValueChanged);
            this.NumericUpDown_Ancho.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Plantilla_KeyDown);
            // 
            // NumericUpDown_Alto
            // 
            this.NumericUpDown_Alto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NumericUpDown_Alto.BackColor = System.Drawing.Color.White;
            this.NumericUpDown_Alto.Location = new System.Drawing.Point(56, 38);
            this.NumericUpDown_Alto.Maximum = new decimal(new int[] {
            16384,
            0,
            0,
            0});
            this.NumericUpDown_Alto.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.NumericUpDown_Alto.Name = "NumericUpDown_Alto";
            this.NumericUpDown_Alto.Size = new System.Drawing.Size(556, 20);
            this.NumericUpDown_Alto.TabIndex = 10;
            this.NumericUpDown_Alto.ThousandsSeparator = true;
            this.NumericUpDown_Alto.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.NumericUpDown_Alto.ValueChanged += new System.EventHandler(this.NumericUpDown_Alto_ValueChanged);
            this.NumericUpDown_Alto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Plantilla_KeyDown);
            // 
            // Etiqueta_Alto
            // 
            this.Etiqueta_Alto.AutoSize = true;
            this.Etiqueta_Alto.Location = new System.Drawing.Point(12, 41);
            this.Etiqueta_Alto.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.Etiqueta_Alto.Name = "Etiqueta_Alto";
            this.Etiqueta_Alto.Size = new System.Drawing.Size(41, 13);
            this.Etiqueta_Alto.TabIndex = 9;
            this.Etiqueta_Alto.Text = "Height:";
            // 
            // NumericUpDown_Escalones
            // 
            this.NumericUpDown_Escalones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NumericUpDown_Escalones.BackColor = System.Drawing.Color.White;
            this.NumericUpDown_Escalones.Location = new System.Drawing.Point(56, 64);
            this.NumericUpDown_Escalones.Maximum = new decimal(new int[] {
            16384,
            0,
            0,
            0});
            this.NumericUpDown_Escalones.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericUpDown_Escalones.Name = "NumericUpDown_Escalones";
            this.NumericUpDown_Escalones.Size = new System.Drawing.Size(442, 20);
            this.NumericUpDown_Escalones.TabIndex = 0;
            this.NumericUpDown_Escalones.ThousandsSeparator = true;
            this.NumericUpDown_Escalones.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.NumericUpDown_Escalones.ValueChanged += new System.EventHandler(this.NumericUpDown_Escalones_ValueChanged);
            this.NumericUpDown_Escalones.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Plantilla_KeyDown);
            // 
            // Etiqueta_Escalones
            // 
            this.Etiqueta_Escalones.AutoSize = true;
            this.Etiqueta_Escalones.Location = new System.Drawing.Point(12, 67);
            this.Etiqueta_Escalones.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.Etiqueta_Escalones.Name = "Etiqueta_Escalones";
            this.Etiqueta_Escalones.Size = new System.Drawing.Size(37, 13);
            this.Etiqueta_Escalones.TabIndex = 11;
            this.Etiqueta_Escalones.Text = "Steps:";
            // 
            // CheckBox_Simetría
            // 
            this.CheckBox_Simetría.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox_Simetría.AutoSize = true;
            this.CheckBox_Simetría.Checked = true;
            this.CheckBox_Simetría.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox_Simetría.Location = new System.Drawing.Point(507, 66);
            this.CheckBox_Simetría.Name = "CheckBox_Simetría";
            this.CheckBox_Simetría.Size = new System.Drawing.Size(105, 17);
            this.CheckBox_Simetría.TabIndex = 1;
            this.CheckBox_Simetría.Text = "4 sides symmetry";
            this.CheckBox_Simetría.ThreeState = true;
            this.CheckBox_Simetría.UseVisualStyleBackColor = true;
            this.CheckBox_Simetría.CheckStateChanged += new System.EventHandler(this.CheckBox_Simetría_CheckStateChanged);
            this.CheckBox_Simetría.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Plantilla_KeyDown);
            // 
            // Etiqueta_Porcentaje_Espacio
            // 
            this.Etiqueta_Porcentaje_Espacio.AutoSize = true;
            this.Etiqueta_Porcentaje_Espacio.Location = new System.Drawing.Point(12, 93);
            this.Etiqueta_Porcentaje_Espacio.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.Etiqueta_Porcentaje_Espacio.Name = "Etiqueta_Porcentaje_Espacio";
            this.Etiqueta_Porcentaje_Espacio.Size = new System.Drawing.Size(41, 13);
            this.Etiqueta_Porcentaje_Espacio.TabIndex = 2;
            this.Etiqueta_Porcentaje_Espacio.Text = "Space:";
            // 
            // NumericUpDown_Porcentaje_Espacio
            // 
            this.NumericUpDown_Porcentaje_Espacio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NumericUpDown_Porcentaje_Espacio.BackColor = System.Drawing.Color.White;
            this.NumericUpDown_Porcentaje_Espacio.Location = new System.Drawing.Point(56, 90);
            this.NumericUpDown_Porcentaje_Espacio.Name = "NumericUpDown_Porcentaje_Espacio";
            this.NumericUpDown_Porcentaje_Espacio.Size = new System.Drawing.Size(442, 20);
            this.NumericUpDown_Porcentaje_Espacio.TabIndex = 3;
            this.NumericUpDown_Porcentaje_Espacio.ThousandsSeparator = true;
            this.NumericUpDown_Porcentaje_Espacio.Value = new decimal(new int[] {
            65,
            0,
            0,
            0});
            this.NumericUpDown_Porcentaje_Espacio.ValueChanged += new System.EventHandler(this.NumericUpDown_Porcentaje_Espacio_ValueChanged);
            this.NumericUpDown_Porcentaje_Espacio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Plantilla_KeyDown);
            // 
            // Etiqueta_Porcentaje
            // 
            this.Etiqueta_Porcentaje.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Etiqueta_Porcentaje.AutoSize = true;
            this.Etiqueta_Porcentaje.Location = new System.Drawing.Point(501, 93);
            this.Etiqueta_Porcentaje.Margin = new System.Windows.Forms.Padding(0);
            this.Etiqueta_Porcentaje.Name = "Etiqueta_Porcentaje";
            this.Etiqueta_Porcentaje.Size = new System.Drawing.Size(15, 13);
            this.Etiqueta_Porcentaje.TabIndex = 4;
            this.Etiqueta_Porcentaje.Text = "%";
            // 
            // CheckBox_Borde
            // 
            this.CheckBox_Borde.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox_Borde.AutoSize = true;
            this.CheckBox_Borde.Checked = true;
            this.CheckBox_Borde.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox_Borde.Location = new System.Drawing.Point(519, 92);
            this.CheckBox_Borde.Name = "CheckBox_Borde";
            this.CheckBox_Borde.Size = new System.Drawing.Size(93, 17);
            this.CheckBox_Borde.TabIndex = 5;
            this.CheckBox_Borde.Text = "Draw a border";
            this.CheckBox_Borde.UseVisualStyleBackColor = true;
            this.CheckBox_Borde.CheckedChanged += new System.EventHandler(this.CheckBox_Borde_CheckedChanged);
            // 
            // Ventana_Diseñador_Espirales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 461);
            this.ContextMenuStrip = this.Menú_Contextual;
            this.Controls.Add(this.CheckBox_Simetría);
            this.Controls.Add(this.CheckBox_Borde);
            this.Controls.Add(this.Etiqueta_Porcentaje);
            this.Controls.Add(this.Etiqueta_Porcentaje_Espacio);
            this.Controls.Add(this.NumericUpDown_Porcentaje_Espacio);
            this.Controls.Add(this.Etiqueta_Escalones);
            this.Controls.Add(this.Etiqueta_Alto);
            this.Controls.Add(this.Etiqueta_Ancho);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Barra_Estado);
            this.Controls.Add(this.NumericUpDown_Escalones);
            this.Controls.Add(this.NumericUpDown_Alto);
            this.Controls.Add(this.NumericUpDown_Ancho);
            this.Controls.Add(this.Picture);
            this.DoubleBuffered = true;
            this.Name = "Ventana_Diseñador_Espirales";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spiral Designer by Jupisoft";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventana_Plantilla_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventana_Plantilla_FormClosed);
            this.Load += new System.EventHandler(this.Ventana_Plantilla_Load);
            this.Shown += new System.EventHandler(this.Ventana_Plantilla_Shown);
            this.SizeChanged += new System.EventHandler(this.Ventana_Plantilla_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Plantilla_KeyDown);
            this.Menú_Contextual.ResumeLayout(false);
            this.Barra_Estado.ResumeLayout(false);
            this.Barra_Estado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Ancho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Alto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Escalones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Porcentaje_Espacio)).EndInit();
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
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_2;
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label Etiqueta_Ancho;
        private System.Windows.Forms.NumericUpDown NumericUpDown_Ancho;
        private System.Windows.Forms.NumericUpDown NumericUpDown_Alto;
        private System.Windows.Forms.Label Etiqueta_Alto;
        private System.Windows.Forms.NumericUpDown NumericUpDown_Escalones;
        private System.Windows.Forms.Label Etiqueta_Escalones;
        private System.Windows.Forms.CheckBox CheckBox_Simetría;
        private System.Windows.Forms.Label Etiqueta_Porcentaje_Espacio;
        private System.Windows.Forms.NumericUpDown NumericUpDown_Porcentaje_Espacio;
        private System.Windows.Forms.Label Etiqueta_Porcentaje;
        private System.Windows.Forms.CheckBox CheckBox_Borde;
    }
}