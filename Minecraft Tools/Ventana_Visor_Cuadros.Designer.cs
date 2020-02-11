namespace Minecraft_Tools
{
    partial class Ventana_Visor_Cuadros
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
            this.Tabla_Principal = new System.Windows.Forms.TableLayoutPanel();
            this.CheckBox_Autozoom = new System.Windows.Forms.CheckBox();
            this.ComboBox_Cuadro = new System.Windows.Forms.ComboBox();
            this.CheckBox_Cuadro_HD = new System.Windows.Forms.CheckBox();
            this.CheckBox_Antialiasing = new System.Windows.Forms.CheckBox();
            this.TextBox_Descripción = new System.Windows.Forms.TextBox();
            this.Temporizador_Principal = new System.Windows.Forms.Timer(this.components);
            this.Barra_Estado = new System.Windows.Forms.ToolStrip();
            this.Barra_Estado_Botón_Excepción = new System.Windows.Forms.ToolStripButton();
            this.Barra_Estado_Separador_1 = new System.Windows.Forms.ToolStripSeparator();
            this.Barra_Estado_Etiqueta_Memoria = new System.Windows.Forms.ToolStripLabel();
            this.Barra_Estado_Separador_2 = new System.Windows.Forms.ToolStripSeparator();
            this.Barra_Estado_Etiqueta_Sugerencia = new System.Windows.Forms.ToolStripLabel();
            this.Barra_Estado_Separador_3 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Menú_Contextual_Visor_Ayuda = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Acerca = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Depurador_Excepciones = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Abrir_Carpeta = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_1 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Actualizar = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Filtro_Negativo = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Filtro_Raíz_Cuadrada = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Filtro_Logaritmo = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_2 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_JPEG = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_PNG = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_JPEG = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_PNG = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_3 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Copiar = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_4 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Guardar = new System.Windows.Forms.ToolStripMenuItem();
            this.Picture = new System.Windows.Forms.PictureBox();
            this.Tabla_Principal.SuspendLayout();
            this.Barra_Estado.SuspendLayout();
            this.Menú_Contextual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            this.SuspendLayout();
            // 
            // Tabla_Principal
            // 
            this.Tabla_Principal.ColumnCount = 4;
            this.Tabla_Principal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89F));
            this.Tabla_Principal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Tabla_Principal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.Tabla_Principal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.Tabla_Principal.Controls.Add(this.CheckBox_Autozoom, 3, 0);
            this.Tabla_Principal.Controls.Add(this.ComboBox_Cuadro, 1, 0);
            this.Tabla_Principal.Controls.Add(this.CheckBox_Cuadro_HD, 0, 0);
            this.Tabla_Principal.Controls.Add(this.CheckBox_Antialiasing, 2, 0);
            this.Tabla_Principal.Dock = System.Windows.Forms.DockStyle.Top;
            this.Tabla_Principal.Location = new System.Drawing.Point(0, 0);
            this.Tabla_Principal.Name = "Tabla_Principal";
            this.Tabla_Principal.RowCount = 1;
            this.Tabla_Principal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.Tabla_Principal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.Tabla_Principal.Size = new System.Drawing.Size(884, 20);
            this.Tabla_Principal.TabIndex = 0;
            // 
            // CheckBox_Autozoom
            // 
            this.CheckBox_Autozoom.AutoSize = true;
            this.CheckBox_Autozoom.Checked = true;
            this.CheckBox_Autozoom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox_Autozoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CheckBox_Autozoom.Location = new System.Drawing.Point(811, 3);
            this.CheckBox_Autozoom.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.CheckBox_Autozoom.Name = "CheckBox_Autozoom";
            this.CheckBox_Autozoom.Size = new System.Drawing.Size(73, 17);
            this.CheckBox_Autozoom.TabIndex = 2;
            this.CheckBox_Autozoom.Text = "Autozoom";
            this.CheckBox_Autozoom.UseVisualStyleBackColor = true;
            this.CheckBox_Autozoom.CheckedChanged += new System.EventHandler(this.CheckBox_Autozoom_CheckedChanged);
            this.CheckBox_Autozoom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Visor_Cuadros_KeyDown);
            // 
            // ComboBox_Cuadro
            // 
            this.ComboBox_Cuadro.BackColor = System.Drawing.Color.White;
            this.ComboBox_Cuadro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComboBox_Cuadro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Cuadro.FormattingEnabled = true;
            this.ComboBox_Cuadro.Location = new System.Drawing.Point(89, 0);
            this.ComboBox_Cuadro.Margin = new System.Windows.Forms.Padding(0);
            this.ComboBox_Cuadro.Name = "ComboBox_Cuadro";
            this.ComboBox_Cuadro.Size = new System.Drawing.Size(637, 21);
            this.ComboBox_Cuadro.TabIndex = 0;
            this.ComboBox_Cuadro.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Cuadro_SelectedIndexChanged);
            this.ComboBox_Cuadro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Visor_Cuadros_KeyDown);
            // 
            // CheckBox_Cuadro_HD
            // 
            this.CheckBox_Cuadro_HD.AutoSize = true;
            this.CheckBox_Cuadro_HD.Checked = true;
            this.CheckBox_Cuadro_HD.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.CheckBox_Cuadro_HD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CheckBox_Cuadro_HD.Location = new System.Drawing.Point(3, 3);
            this.CheckBox_Cuadro_HD.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.CheckBox_Cuadro_HD.Name = "CheckBox_Cuadro_HD";
            this.CheckBox_Cuadro_HD.Size = new System.Drawing.Size(86, 17);
            this.CheckBox_Cuadro_HD.TabIndex = 3;
            this.CheckBox_Cuadro_HD.Text = "HD Painting:";
            this.CheckBox_Cuadro_HD.ThreeState = true;
            this.CheckBox_Cuadro_HD.UseVisualStyleBackColor = true;
            this.CheckBox_Cuadro_HD.CheckStateChanged += new System.EventHandler(this.CheckBox_Cuadro_HD_CheckStateChanged);
            this.CheckBox_Cuadro_HD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Visor_Cuadros_KeyDown);
            // 
            // CheckBox_Antialiasing
            // 
            this.CheckBox_Antialiasing.AutoSize = true;
            this.CheckBox_Antialiasing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CheckBox_Antialiasing.Location = new System.Drawing.Point(729, 3);
            this.CheckBox_Antialiasing.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.CheckBox_Antialiasing.Name = "CheckBox_Antialiasing";
            this.CheckBox_Antialiasing.Size = new System.Drawing.Size(79, 17);
            this.CheckBox_Antialiasing.TabIndex = 1;
            this.CheckBox_Antialiasing.Text = "Antialiasing";
            this.CheckBox_Antialiasing.ThreeState = true;
            this.CheckBox_Antialiasing.UseVisualStyleBackColor = true;
            this.CheckBox_Antialiasing.CheckStateChanged += new System.EventHandler(this.CheckBox_Antialiasing_CheckStateChanged);
            this.CheckBox_Antialiasing.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Visor_Cuadros_KeyDown);
            // 
            // TextBox_Descripción
            // 
            this.TextBox_Descripción.BackColor = System.Drawing.Color.White;
            this.TextBox_Descripción.Dock = System.Windows.Forms.DockStyle.Top;
            this.TextBox_Descripción.Location = new System.Drawing.Point(0, 20);
            this.TextBox_Descripción.Margin = new System.Windows.Forms.Padding(0);
            this.TextBox_Descripción.Multiline = true;
            this.TextBox_Descripción.Name = "TextBox_Descripción";
            this.TextBox_Descripción.ReadOnly = true;
            this.TextBox_Descripción.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox_Descripción.Size = new System.Drawing.Size(884, 44);
            this.TextBox_Descripción.TabIndex = 1;
            this.TextBox_Descripción.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Visor_Cuadros_KeyDown);
            // 
            // Temporizador_Principal
            // 
            this.Temporizador_Principal.Interval = 1;
            this.Temporizador_Principal.Tick += new System.EventHandler(this.Temporizador_Principal_Tick);
            // 
            // Barra_Estado
            // 
            this.Barra_Estado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Barra_Estado.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.Barra_Estado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Barra_Estado_Botón_Excepción,
            this.Barra_Estado_Separador_1,
            this.Barra_Estado_Etiqueta_Memoria,
            this.Barra_Estado_Separador_2,
            this.Barra_Estado_Etiqueta_Sugerencia,
            this.Barra_Estado_Separador_3});
            this.Barra_Estado.Location = new System.Drawing.Point(0, 436);
            this.Barra_Estado.Name = "Barra_Estado";
            this.Barra_Estado.Size = new System.Drawing.Size(884, 25);
            this.Barra_Estado.TabIndex = 2;
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
            // Barra_Estado_Etiqueta_Memoria
            // 
            this.Barra_Estado_Etiqueta_Memoria.Image = global::Minecraft_Tools.Properties.Resources.Memoria;
            this.Barra_Estado_Etiqueta_Memoria.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.Barra_Estado_Etiqueta_Memoria.Name = "Barra_Estado_Etiqueta_Memoria";
            this.Barra_Estado_Etiqueta_Memoria.Size = new System.Drawing.Size(82, 22);
            this.Barra_Estado_Etiqueta_Memoria.Text = "RAM: 0 MB";
            // 
            // Barra_Estado_Separador_2
            // 
            this.Barra_Estado_Separador_2.Name = "Barra_Estado_Separador_2";
            this.Barra_Estado_Separador_2.Size = new System.Drawing.Size(6, 25);
            // 
            // Barra_Estado_Etiqueta_Sugerencia
            // 
            this.Barra_Estado_Etiqueta_Sugerencia.Image = global::Minecraft_Tools.Properties.Resources.Ayuda;
            this.Barra_Estado_Etiqueta_Sugerencia.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.Barra_Estado_Etiqueta_Sugerencia.Name = "Barra_Estado_Etiqueta_Sugerencia";
            this.Barra_Estado_Etiqueta_Sugerencia.Size = new System.Drawing.Size(807, 16);
            this.Barra_Estado_Etiqueta_Sugerencia.Text = "Tip: all the Minecraft paintings are painted by the talented Kristoffer Zetterstr" +
    "and, and the HD paintings are from the awesome Faithful resource pack.";
            // 
            // Barra_Estado_Separador_3
            // 
            this.Barra_Estado_Separador_3.Name = "Barra_Estado_Separador_3";
            this.Barra_Estado_Separador_3.Size = new System.Drawing.Size(6, 25);
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
            this.Menú_Contextual_Filtro_Negativo,
            this.Menú_Contextual_Filtro_Raíz_Cuadrada,
            this.Menú_Contextual_Filtro_Logaritmo,
            this.Menú_Contextual_Separador_2,
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_JPEG,
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_PNG,
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_JPEG,
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_PNG,
            this.Menú_Contextual_Separador_3,
            this.Menú_Contextual_Copiar,
            this.Menú_Contextual_Separador_4,
            this.Menú_Contextual_Guardar});
            this.Menú_Contextual.Name = "Menú_Contextual";
            this.Menú_Contextual.Size = new System.Drawing.Size(434, 336);
            // 
            // Menú_Contextual_Visor_Ayuda
            // 
            this.Menú_Contextual_Visor_Ayuda.Image = global::Minecraft_Tools.Properties.Resources.Ayuda;
            this.Menú_Contextual_Visor_Ayuda.Name = "Menú_Contextual_Visor_Ayuda";
            this.Menú_Contextual_Visor_Ayuda.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.Menú_Contextual_Visor_Ayuda.Size = new System.Drawing.Size(433, 22);
            this.Menú_Contextual_Visor_Ayuda.Text = "Help viewer...";
            this.Menú_Contextual_Visor_Ayuda.Click += new System.EventHandler(this.Menú_Contextual_Visor_Ayuda_Click);
            // 
            // Menú_Contextual_Acerca
            // 
            this.Menú_Contextual_Acerca.Image = global::Minecraft_Tools.Properties.Resources.Jupisoft_16;
            this.Menú_Contextual_Acerca.Name = "Menú_Contextual_Acerca";
            this.Menú_Contextual_Acerca.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.Menú_Contextual_Acerca.Size = new System.Drawing.Size(433, 22);
            this.Menú_Contextual_Acerca.Text = "About...";
            this.Menú_Contextual_Acerca.Click += new System.EventHandler(this.Menú_Contextual_Acerca_Click);
            // 
            // Menú_Contextual_Depurador_Excepciones
            // 
            this.Menú_Contextual_Depurador_Excepciones.Image = global::Minecraft_Tools.Properties.Resources.Excepción;
            this.Menú_Contextual_Depurador_Excepciones.Name = "Menú_Contextual_Depurador_Excepciones";
            this.Menú_Contextual_Depurador_Excepciones.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.Menú_Contextual_Depurador_Excepciones.Size = new System.Drawing.Size(433, 22);
            this.Menú_Contextual_Depurador_Excepciones.Text = "Exception debugger...";
            this.Menú_Contextual_Depurador_Excepciones.Click += new System.EventHandler(this.Menú_Contextual_Depurador_Excepciones_Click);
            // 
            // Menú_Contextual_Abrir_Carpeta
            // 
            this.Menú_Contextual_Abrir_Carpeta.Image = global::Minecraft_Tools.Properties.Resources.Ejecutar;
            this.Menú_Contextual_Abrir_Carpeta.Name = "Menú_Contextual_Abrir_Carpeta";
            this.Menú_Contextual_Abrir_Carpeta.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.Menú_Contextual_Abrir_Carpeta.Size = new System.Drawing.Size(433, 22);
            this.Menú_Contextual_Abrir_Carpeta.Text = "Open the default save folder...";
            this.Menú_Contextual_Abrir_Carpeta.Click += new System.EventHandler(this.Menú_Contextual_Abrir_Carpeta_Click);
            // 
            // Menú_Contextual_Separador_1
            // 
            this.Menú_Contextual_Separador_1.Name = "Menú_Contextual_Separador_1";
            this.Menú_Contextual_Separador_1.Size = new System.Drawing.Size(430, 6);
            // 
            // Menú_Contextual_Actualizar
            // 
            this.Menú_Contextual_Actualizar.Image = global::Minecraft_Tools.Properties.Resources.Actualizar;
            this.Menú_Contextual_Actualizar.Name = "Menú_Contextual_Actualizar";
            this.Menú_Contextual_Actualizar.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.Menú_Contextual_Actualizar.Size = new System.Drawing.Size(433, 22);
            this.Menú_Contextual_Actualizar.Text = "Refresh";
            this.Menú_Contextual_Actualizar.Click += new System.EventHandler(this.Menú_Contextual_Actualizar_Click);
            // 
            // Menú_Contextual_Filtro_Negativo
            // 
            this.Menú_Contextual_Filtro_Negativo.CheckOnClick = true;
            this.Menú_Contextual_Filtro_Negativo.Name = "Menú_Contextual_Filtro_Negativo";
            this.Menú_Contextual_Filtro_Negativo.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.Menú_Contextual_Filtro_Negativo.Size = new System.Drawing.Size(433, 22);
            this.Menú_Contextual_Filtro_Negativo.Text = "Apply a negative filter";
            this.Menú_Contextual_Filtro_Negativo.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Filtro_Negativo_CheckedChanged);
            // 
            // Menú_Contextual_Filtro_Raíz_Cuadrada
            // 
            this.Menú_Contextual_Filtro_Raíz_Cuadrada.CheckOnClick = true;
            this.Menú_Contextual_Filtro_Raíz_Cuadrada.Name = "Menú_Contextual_Filtro_Raíz_Cuadrada";
            this.Menú_Contextual_Filtro_Raíz_Cuadrada.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.Menú_Contextual_Filtro_Raíz_Cuadrada.Size = new System.Drawing.Size(433, 22);
            this.Menú_Contextual_Filtro_Raíz_Cuadrada.Text = "Apply a square root filter";
            this.Menú_Contextual_Filtro_Raíz_Cuadrada.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Filtro_Raíz_Cuadrada_CheckedChanged);
            // 
            // Menú_Contextual_Filtro_Logaritmo
            // 
            this.Menú_Contextual_Filtro_Logaritmo.CheckOnClick = true;
            this.Menú_Contextual_Filtro_Logaritmo.Name = "Menú_Contextual_Filtro_Logaritmo";
            this.Menú_Contextual_Filtro_Logaritmo.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.Menú_Contextual_Filtro_Logaritmo.Size = new System.Drawing.Size(433, 22);
            this.Menú_Contextual_Filtro_Logaritmo.Text = "Apply a logarithm filter";
            this.Menú_Contextual_Filtro_Logaritmo.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Filtro_Logaritmo_CheckedChanged);
            // 
            // Menú_Contextual_Separador_2
            // 
            this.Menú_Contextual_Separador_2.Name = "Menú_Contextual_Separador_2";
            this.Menú_Contextual_Separador_2.Size = new System.Drawing.Size(430, 6);
            // 
            // Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_JPEG
            // 
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_JPEG.Image = global::Minecraft_Tools.Properties.Resources.Pool;
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_JPEG.Name = "Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_JPEG";
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_JPEG.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_JPEG.Size = new System.Drawing.Size(433, 22);
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_JPEG.Text = "Export a resource pack with the HD real paintings (1.12.2-, JPEG)";
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_JPEG.Click += new System.EventHandler(this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_JPEG_Click);
            // 
            // Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_PNG
            // 
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_PNG.Image = global::Minecraft_Tools.Properties.Resources.Pool;
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_PNG.Name = "Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_PNG";
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_PNG.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_PNG.Size = new System.Drawing.Size(433, 22);
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_PNG.Text = "Export a resource pack with the HD real paintings (1.12.2-, PNG)";
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_PNG.Click += new System.EventHandler(this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_PNG_Click);
            // 
            // Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_JPEG
            // 
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_JPEG.Image = global::Minecraft_Tools.Properties.Resources.Pool;
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_JPEG.Name = "Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_JPEG";
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_JPEG.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_JPEG.Size = new System.Drawing.Size(433, 22);
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_JPEG.Text = "Export a resource pack with the HD real paintings (1.13+, JPEG)";
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_JPEG.Click += new System.EventHandler(this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_JPEG_Click);
            // 
            // Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_PNG
            // 
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_PNG.Image = global::Minecraft_Tools.Properties.Resources.Pool;
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_PNG.Name = "Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_PNG";
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_PNG.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_PNG.Size = new System.Drawing.Size(433, 22);
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_PNG.Text = "Export a resource pack with the HD real paintings (1.13+, PNG)";
            this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_PNG.Click += new System.EventHandler(this.Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_PNG_Click);
            // 
            // Menú_Contextual_Separador_3
            // 
            this.Menú_Contextual_Separador_3.Name = "Menú_Contextual_Separador_3";
            this.Menú_Contextual_Separador_3.Size = new System.Drawing.Size(430, 6);
            // 
            // Menú_Contextual_Copiar
            // 
            this.Menú_Contextual_Copiar.Image = global::Minecraft_Tools.Properties.Resources.Copiar;
            this.Menú_Contextual_Copiar.Name = "Menú_Contextual_Copiar";
            this.Menú_Contextual_Copiar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.Menú_Contextual_Copiar.Size = new System.Drawing.Size(433, 22);
            this.Menú_Contextual_Copiar.Text = "Copy the painting";
            this.Menú_Contextual_Copiar.Click += new System.EventHandler(this.Menú_Contextual_Copiar_Click);
            // 
            // Menú_Contextual_Separador_4
            // 
            this.Menú_Contextual_Separador_4.Name = "Menú_Contextual_Separador_4";
            this.Menú_Contextual_Separador_4.Size = new System.Drawing.Size(430, 6);
            // 
            // Menú_Contextual_Guardar
            // 
            this.Menú_Contextual_Guardar.Image = global::Minecraft_Tools.Properties.Resources.Guardar;
            this.Menú_Contextual_Guardar.Name = "Menú_Contextual_Guardar";
            this.Menú_Contextual_Guardar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.Menú_Contextual_Guardar.Size = new System.Drawing.Size(433, 22);
            this.Menú_Contextual_Guardar.Text = "Save the painting";
            this.Menú_Contextual_Guardar.Click += new System.EventHandler(this.Menú_Contextual_Guardar_Click);
            // 
            // Picture
            // 
            this.Picture.BackColor = System.Drawing.Color.Gray;
            this.Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Picture.InitialImage = null;
            this.Picture.Location = new System.Drawing.Point(0, 64);
            this.Picture.Name = "Picture";
            this.Picture.Size = new System.Drawing.Size(884, 372);
            this.Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Picture.TabIndex = 0;
            this.Picture.TabStop = false;
            this.Picture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_MouseDown);
            // 
            // Ventana_Visor_Cuadros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.ContextMenuStrip = this.Menú_Contextual;
            this.Controls.Add(this.Picture);
            this.Controls.Add(this.TextBox_Descripción);
            this.Controls.Add(this.Barra_Estado);
            this.Controls.Add(this.Tabla_Principal);
            this.DoubleBuffered = true;
            this.Name = "Ventana_Visor_Cuadros";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Paintings Viewer by Jupisoft";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventana_Visor_Cuadros_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventana_Visor_Cuadros_FormClosed);
            this.Load += new System.EventHandler(this.Ventana_Visor_Cuadros_Load);
            this.Shown += new System.EventHandler(this.Ventana_Visor_Cuadros_Shown);
            this.SizeChanged += new System.EventHandler(this.Ventana_Visor_Cuadros_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Visor_Cuadros_KeyDown);
            this.Tabla_Principal.ResumeLayout(false);
            this.Tabla_Principal.PerformLayout();
            this.Barra_Estado.ResumeLayout(false);
            this.Barra_Estado.PerformLayout();
            this.Menú_Contextual.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Picture;
        private System.Windows.Forms.TableLayoutPanel Tabla_Principal;
        private System.Windows.Forms.ComboBox ComboBox_Cuadro;
        private System.Windows.Forms.CheckBox CheckBox_Cuadro_HD;
        private System.Windows.Forms.CheckBox CheckBox_Antialiasing;
        private System.Windows.Forms.CheckBox CheckBox_Autozoom;
        private System.Windows.Forms.Timer Temporizador_Principal;
        private System.Windows.Forms.ToolStrip Barra_Estado;
        private System.Windows.Forms.ToolStripButton Barra_Estado_Botón_Excepción;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_1;
        private System.Windows.Forms.ToolStripLabel Barra_Estado_Etiqueta_Memoria;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_2;
        private System.Windows.Forms.ToolStripLabel Barra_Estado_Etiqueta_Sugerencia;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_3;
        private System.Windows.Forms.ContextMenuStrip Menú_Contextual;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Visor_Ayuda;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Acerca;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Depurador_Excepciones;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Abrir_Carpeta;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_1;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Actualizar;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_2;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_3;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Copiar;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Guardar;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Filtro_Logaritmo;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Filtro_Raíz_Cuadrada;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Filtro_Negativo;
        private System.Windows.Forms.TextBox TextBox_Descripción;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_4;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_PNG;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_PNG;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_JPEG;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_JPEG;
    }
}