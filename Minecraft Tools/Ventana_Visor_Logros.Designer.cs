namespace Minecraft_Tools
{
    partial class Ventana_Visor_Logros
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ventana_Visor_Logros));
            this.Menú_Contextual = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Menú_Contextual_Separador_1 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Separador_2 = new System.Windows.Forms.ToolStripSeparator();
            this.Barra_Estado = new System.Windows.Forms.ToolStrip();
            this.Barra_Estado_Separador_1 = new System.Windows.Forms.ToolStripSeparator();
            this.Barra_Estado_Separador_2 = new System.Windows.Forms.ToolStripSeparator();
            this.Barra_Estado_Separador_3 = new System.Windows.Forms.ToolStripSeparator();
            this.Barra_Estado_Separador_4 = new System.Windows.Forms.ToolStripSeparator();
            this.Barra_Estado_Separador_5 = new System.Windows.Forms.ToolStripSeparator();
            this.Temporizador_Principal = new System.Windows.Forms.Timer(this.components);
            this.Etiqueta_Jugador = new System.Windows.Forms.Label();
            this.Etiqueta_Mundo = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ComboBox_Jugador = new System.Windows.Forms.ComboBox();
            this.ComboBox_Mundo = new System.Windows.Forms.ComboBox();
            this.DataGridView_Principal = new System.Windows.Forms.DataGridView();
            this.Columna_Icono = new System.Windows.Forms.DataGridViewImageColumn();
            this.Columna_Título = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Categoría = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Marco = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Oculto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Completado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Descripción = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Ruta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Menú_Contextual_Donar = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Visor_Ayuda = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Acerca = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Depurador_Excepciones = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Abrir_Carpeta_Guardado = new System.Windows.Forms.ToolStripMenuItem();
            this.Picture_FPS = new System.Windows.Forms.PictureBox();
            this.Barra_Estado_Botón_Excepción = new System.Windows.Forms.ToolStripButton();
            this.Barra_Estado_Etiqueta_CPU = new System.Windows.Forms.ToolStripLabel();
            this.Barra_Estado_Etiqueta_Memoria = new System.Windows.Forms.ToolStripLabel();
            this.Barra_Estado_Etiqueta_FPS = new System.Windows.Forms.ToolStripLabel();
            this.Barra_Estado_Etiqueta_Sugerencia = new System.Windows.Forms.ToolStripLabel();
            this.Menú_Contextual_Mostrar_Todos = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Dibujar_Fondo = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Dibujar_Marco = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Dibujar_Icono = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual.SuspendLayout();
            this.Barra_Estado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Principal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_FPS)).BeginInit();
            this.SuspendLayout();
            // 
            // Menú_Contextual
            // 
            this.Menú_Contextual.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menú_Contextual_Donar,
            this.Menú_Contextual_Separador_1,
            this.Menú_Contextual_Visor_Ayuda,
            this.Menú_Contextual_Acerca,
            this.Menú_Contextual_Depurador_Excepciones,
            this.Menú_Contextual_Abrir_Carpeta_Guardado,
            this.Menú_Contextual_Separador_2,
            this.Menú_Contextual_Mostrar_Todos,
            this.Menú_Contextual_Dibujar_Fondo,
            this.Menú_Contextual_Dibujar_Marco,
            this.Menú_Contextual_Dibujar_Icono});
            this.Menú_Contextual.Name = "Menú_Contextual";
            this.Menú_Contextual.Size = new System.Drawing.Size(268, 236);
            this.Menú_Contextual.Opening += new System.ComponentModel.CancelEventHandler(this.Menú_Contextual_Opening);
            // 
            // Menú_Contextual_Separador_1
            // 
            this.Menú_Contextual_Separador_1.Name = "Menú_Contextual_Separador_1";
            this.Menú_Contextual_Separador_1.Size = new System.Drawing.Size(264, 6);
            // 
            // Menú_Contextual_Separador_2
            // 
            this.Menú_Contextual_Separador_2.Name = "Menú_Contextual_Separador_2";
            this.Menú_Contextual_Separador_2.Size = new System.Drawing.Size(264, 6);
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
            this.Barra_Estado.Location = new System.Drawing.Point(0, 416);
            this.Barra_Estado.Name = "Barra_Estado";
            this.Barra_Estado.Size = new System.Drawing.Size(624, 25);
            this.Barra_Estado.TabIndex = 5;
            this.Barra_Estado.Text = "Status bar";
            // 
            // Barra_Estado_Separador_1
            // 
            this.Barra_Estado_Separador_1.Name = "Barra_Estado_Separador_1";
            this.Barra_Estado_Separador_1.Size = new System.Drawing.Size(6, 25);
            this.Barra_Estado_Separador_1.Visible = false;
            // 
            // Barra_Estado_Separador_2
            // 
            this.Barra_Estado_Separador_2.Name = "Barra_Estado_Separador_2";
            this.Barra_Estado_Separador_2.Size = new System.Drawing.Size(6, 25);
            // 
            // Barra_Estado_Separador_3
            // 
            this.Barra_Estado_Separador_3.Name = "Barra_Estado_Separador_3";
            this.Barra_Estado_Separador_3.Size = new System.Drawing.Size(6, 25);
            // 
            // Barra_Estado_Separador_4
            // 
            this.Barra_Estado_Separador_4.Name = "Barra_Estado_Separador_4";
            this.Barra_Estado_Separador_4.Size = new System.Drawing.Size(6, 25);
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
            // Etiqueta_Jugador
            // 
            this.Etiqueta_Jugador.AutoSize = true;
            this.Etiqueta_Jugador.Location = new System.Drawing.Point(12, 41);
            this.Etiqueta_Jugador.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.Etiqueta_Jugador.Name = "Etiqueta_Jugador";
            this.Etiqueta_Jugador.Size = new System.Drawing.Size(39, 13);
            this.Etiqueta_Jugador.TabIndex = 2;
            this.Etiqueta_Jugador.Text = "Player:";
            // 
            // Etiqueta_Mundo
            // 
            this.Etiqueta_Mundo.AutoSize = true;
            this.Etiqueta_Mundo.Location = new System.Drawing.Point(12, 15);
            this.Etiqueta_Mundo.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.Etiqueta_Mundo.Name = "Etiqueta_Mundo";
            this.Etiqueta_Mundo.Size = new System.Drawing.Size(38, 13);
            this.Etiqueta_Mundo.TabIndex = 0;
            this.Etiqueta_Mundo.Text = "World:";
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
            this.textBox2.TabIndex = 7;
            this.textBox2.Visible = false;
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
            this.textBox1.TabIndex = 6;
            this.textBox1.Visible = false;
            // 
            // ComboBox_Jugador
            // 
            this.ComboBox_Jugador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBox_Jugador.BackColor = System.Drawing.Color.White;
            this.ComboBox_Jugador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Jugador.FormattingEnabled = true;
            this.ComboBox_Jugador.Location = new System.Drawing.Point(54, 37);
            this.ComboBox_Jugador.Name = "ComboBox_Jugador";
            this.ComboBox_Jugador.Size = new System.Drawing.Size(558, 21);
            this.ComboBox_Jugador.TabIndex = 3;
            this.ComboBox_Jugador.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Jugador_SelectedIndexChanged);
            this.ComboBox_Jugador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Plantilla_KeyDown);
            // 
            // ComboBox_Mundo
            // 
            this.ComboBox_Mundo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBox_Mundo.BackColor = System.Drawing.Color.White;
            this.ComboBox_Mundo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Mundo.FormattingEnabled = true;
            this.ComboBox_Mundo.Location = new System.Drawing.Point(54, 11);
            this.ComboBox_Mundo.Name = "ComboBox_Mundo";
            this.ComboBox_Mundo.Size = new System.Drawing.Size(558, 21);
            this.ComboBox_Mundo.TabIndex = 1;
            this.ComboBox_Mundo.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Mundo_SelectedIndexChanged);
            this.ComboBox_Mundo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Plantilla_KeyDown);
            // 
            // DataGridView_Principal
            // 
            this.DataGridView_Principal.AllowUserToAddRows = false;
            this.DataGridView_Principal.AllowUserToDeleteRows = false;
            this.DataGridView_Principal.AllowUserToResizeColumns = false;
            this.DataGridView_Principal.AllowUserToResizeRows = false;
            this.DataGridView_Principal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView_Principal.BackgroundColor = System.Drawing.Color.White;
            this.DataGridView_Principal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle29.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle29.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle29.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView_Principal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle29;
            this.DataGridView_Principal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_Principal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Columna_Icono,
            this.Columna_Título,
            this.Columna_Categoría,
            this.Columna_Marco,
            this.Columna_Oculto,
            this.Columna_Fecha,
            this.Columna_Completado,
            this.Columna_Descripción,
            this.Columna_Ruta});
            this.DataGridView_Principal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DataGridView_Principal.Location = new System.Drawing.Point(0, 64);
            this.DataGridView_Principal.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.DataGridView_Principal.MultiSelect = false;
            this.DataGridView_Principal.Name = "DataGridView_Principal";
            this.DataGridView_Principal.ReadOnly = true;
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle31.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle31.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView_Principal.RowHeadersDefaultCellStyle = dataGridViewCellStyle31;
            this.DataGridView_Principal.RowHeadersVisible = false;
            dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView_Principal.RowsDefaultCellStyle = dataGridViewCellStyle32;
            this.DataGridView_Principal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView_Principal.Size = new System.Drawing.Size(624, 344);
            this.DataGridView_Principal.TabIndex = 4;
            this.DataGridView_Principal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Plantilla_KeyDown);
            // 
            // Columna_Icono
            // 
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle30.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle30.NullValue")));
            dataGridViewCellStyle30.Padding = new System.Windows.Forms.Padding(2);
            this.Columna_Icono.DefaultCellStyle = dataGridViewCellStyle30;
            this.Columna_Icono.HeaderText = "";
            this.Columna_Icono.Name = "Columna_Icono";
            this.Columna_Icono.ReadOnly = true;
            this.Columna_Icono.Width = 16;
            // 
            // Columna_Título
            // 
            this.Columna_Título.HeaderText = "Title";
            this.Columna_Título.Name = "Columna_Título";
            this.Columna_Título.ReadOnly = true;
            // 
            // Columna_Categoría
            // 
            this.Columna_Categoría.HeaderText = "Category";
            this.Columna_Categoría.Name = "Columna_Categoría";
            this.Columna_Categoría.ReadOnly = true;
            // 
            // Columna_Marco
            // 
            this.Columna_Marco.HeaderText = "Frame";
            this.Columna_Marco.Name = "Columna_Marco";
            this.Columna_Marco.ReadOnly = true;
            // 
            // Columna_Oculto
            // 
            this.Columna_Oculto.HeaderText = "Hidden";
            this.Columna_Oculto.Name = "Columna_Oculto";
            this.Columna_Oculto.ReadOnly = true;
            // 
            // Columna_Fecha
            // 
            this.Columna_Fecha.HeaderText = "Date";
            this.Columna_Fecha.Name = "Columna_Fecha";
            this.Columna_Fecha.ReadOnly = true;
            // 
            // Columna_Completado
            // 
            this.Columna_Completado.HeaderText = "Completed";
            this.Columna_Completado.Name = "Columna_Completado";
            this.Columna_Completado.ReadOnly = true;
            // 
            // Columna_Descripción
            // 
            this.Columna_Descripción.HeaderText = "Description";
            this.Columna_Descripción.Name = "Columna_Descripción";
            this.Columna_Descripción.ReadOnly = true;
            // 
            // Columna_Ruta
            // 
            this.Columna_Ruta.HeaderText = "Path";
            this.Columna_Ruta.Name = "Columna_Ruta";
            this.Columna_Ruta.ReadOnly = true;
            // 
            // Menú_Contextual_Donar
            // 
            this.Menú_Contextual_Donar.Image = global::Minecraft_Tools.Properties.Resources.Donar;
            this.Menú_Contextual_Donar.Name = "Menú_Contextual_Donar";
            this.Menú_Contextual_Donar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.Menú_Contextual_Donar.Size = new System.Drawing.Size(267, 22);
            this.Menú_Contextual_Donar.Text = "Donate to Jupisoft (PayPal)...";
            this.Menú_Contextual_Donar.Click += new System.EventHandler(this.Menú_Contextual_Donar_Click);
            // 
            // Menú_Contextual_Visor_Ayuda
            // 
            this.Menú_Contextual_Visor_Ayuda.Image = global::Minecraft_Tools.Properties.Resources.Ayuda;
            this.Menú_Contextual_Visor_Ayuda.Name = "Menú_Contextual_Visor_Ayuda";
            this.Menú_Contextual_Visor_Ayuda.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.Menú_Contextual_Visor_Ayuda.Size = new System.Drawing.Size(267, 22);
            this.Menú_Contextual_Visor_Ayuda.Text = "Help viewer...";
            this.Menú_Contextual_Visor_Ayuda.Click += new System.EventHandler(this.Menú_Contextual_Visor_Ayuda_Click);
            // 
            // Menú_Contextual_Acerca
            // 
            this.Menú_Contextual_Acerca.Image = global::Minecraft_Tools.Properties.Resources.Jupisoft_16;
            this.Menú_Contextual_Acerca.Name = "Menú_Contextual_Acerca";
            this.Menú_Contextual_Acerca.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.Menú_Contextual_Acerca.Size = new System.Drawing.Size(267, 22);
            this.Menú_Contextual_Acerca.Text = "About...";
            this.Menú_Contextual_Acerca.Click += new System.EventHandler(this.Menú_Contextual_Acerca_Click);
            // 
            // Menú_Contextual_Depurador_Excepciones
            // 
            this.Menú_Contextual_Depurador_Excepciones.Image = global::Minecraft_Tools.Properties.Resources.Excepción;
            this.Menú_Contextual_Depurador_Excepciones.Name = "Menú_Contextual_Depurador_Excepciones";
            this.Menú_Contextual_Depurador_Excepciones.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.Menú_Contextual_Depurador_Excepciones.Size = new System.Drawing.Size(267, 22);
            this.Menú_Contextual_Depurador_Excepciones.Text = "Exception debugger...";
            this.Menú_Contextual_Depurador_Excepciones.Click += new System.EventHandler(this.Menú_Contextual_Depurador_Excepciones_Click);
            // 
            // Menú_Contextual_Abrir_Carpeta_Guardado
            // 
            this.Menú_Contextual_Abrir_Carpeta_Guardado.Image = global::Minecraft_Tools.Properties.Resources.Ejecutar;
            this.Menú_Contextual_Abrir_Carpeta_Guardado.Name = "Menú_Contextual_Abrir_Carpeta_Guardado";
            this.Menú_Contextual_Abrir_Carpeta_Guardado.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.Menú_Contextual_Abrir_Carpeta_Guardado.Size = new System.Drawing.Size(267, 22);
            this.Menú_Contextual_Abrir_Carpeta_Guardado.Text = "Open the default save folder...";
            this.Menú_Contextual_Abrir_Carpeta_Guardado.Click += new System.EventHandler(this.Menú_Contextual_Abrir_Carpeta_Guardado_Click);
            // 
            // Picture_FPS
            // 
            this.Picture_FPS.BackColor = System.Drawing.Color.Transparent;
            this.Picture_FPS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Picture_FPS.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Picture_FPS.InitialImage = null;
            this.Picture_FPS.Location = new System.Drawing.Point(0, 408);
            this.Picture_FPS.Margin = new System.Windows.Forms.Padding(0);
            this.Picture_FPS.Name = "Picture_FPS";
            this.Picture_FPS.Size = new System.Drawing.Size(624, 8);
            this.Picture_FPS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Picture_FPS.TabIndex = 10;
            this.Picture_FPS.TabStop = false;
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
            // Barra_Estado_Etiqueta_CPU
            // 
            this.Barra_Estado_Etiqueta_CPU.Image = global::Minecraft_Tools.Properties.Resources.CPU;
            this.Barra_Estado_Etiqueta_CPU.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.Barra_Estado_Etiqueta_CPU.Name = "Barra_Estado_Etiqueta_CPU";
            this.Barra_Estado_Etiqueta_CPU.Size = new System.Drawing.Size(71, 22);
            this.Barra_Estado_Etiqueta_CPU.Text = "CPU: 0 %";
            // 
            // Barra_Estado_Etiqueta_Memoria
            // 
            this.Barra_Estado_Etiqueta_Memoria.Image = global::Minecraft_Tools.Properties.Resources.RAM;
            this.Barra_Estado_Etiqueta_Memoria.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.Barra_Estado_Etiqueta_Memoria.Name = "Barra_Estado_Etiqueta_Memoria";
            this.Barra_Estado_Etiqueta_Memoria.Size = new System.Drawing.Size(82, 22);
            this.Barra_Estado_Etiqueta_Memoria.Text = "RAM: 0 MB";
            // 
            // Barra_Estado_Etiqueta_FPS
            // 
            this.Barra_Estado_Etiqueta_FPS.Image = global::Minecraft_Tools.Properties.Resources.FPS;
            this.Barra_Estado_Etiqueta_FPS.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.Barra_Estado_Etiqueta_FPS.Name = "Barra_Estado_Etiqueta_FPS";
            this.Barra_Estado_Etiqueta_FPS.Size = new System.Drawing.Size(54, 22);
            this.Barra_Estado_Etiqueta_FPS.Text = "FPS: 0";
            // 
            // Barra_Estado_Etiqueta_Sugerencia
            // 
            this.Barra_Estado_Etiqueta_Sugerencia.Image = global::Minecraft_Tools.Properties.Resources.Ayuda;
            this.Barra_Estado_Etiqueta_Sugerencia.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.Barra_Estado_Etiqueta_Sugerencia.Name = "Barra_Estado_Etiqueta_Sugerencia";
            this.Barra_Estado_Etiqueta_Sugerencia.Size = new System.Drawing.Size(336, 22);
            this.Barra_Estado_Etiqueta_Sugerencia.Text = "Tip: shows all the advancements and recipes of each player.";
            // 
            // Menú_Contextual_Mostrar_Todos
            // 
            this.Menú_Contextual_Mostrar_Todos.CheckOnClick = true;
            this.Menú_Contextual_Mostrar_Todos.Name = "Menú_Contextual_Mostrar_Todos";
            this.Menú_Contextual_Mostrar_Todos.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.Menú_Contextual_Mostrar_Todos.Size = new System.Drawing.Size(267, 22);
            this.Menú_Contextual_Mostrar_Todos.Text = "Show the known advancements";
            this.Menú_Contextual_Mostrar_Todos.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Mostrar_Todos_CheckedChanged);
            // 
            // Menú_Contextual_Dibujar_Fondo
            // 
            this.Menú_Contextual_Dibujar_Fondo.Checked = true;
            this.Menú_Contextual_Dibujar_Fondo.CheckOnClick = true;
            this.Menú_Contextual_Dibujar_Fondo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menú_Contextual_Dibujar_Fondo.Name = "Menú_Contextual_Dibujar_Fondo";
            this.Menú_Contextual_Dibujar_Fondo.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.Menú_Contextual_Dibujar_Fondo.Size = new System.Drawing.Size(267, 22);
            this.Menú_Contextual_Dibujar_Fondo.Text = "Draw the icon background";
            this.Menú_Contextual_Dibujar_Fondo.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Dibujar_Fondo_CheckedChanged);
            // 
            // Menú_Contextual_Dibujar_Marco
            // 
            this.Menú_Contextual_Dibujar_Marco.Checked = true;
            this.Menú_Contextual_Dibujar_Marco.CheckOnClick = true;
            this.Menú_Contextual_Dibujar_Marco.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menú_Contextual_Dibujar_Marco.Name = "Menú_Contextual_Dibujar_Marco";
            this.Menú_Contextual_Dibujar_Marco.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.Menú_Contextual_Dibujar_Marco.Size = new System.Drawing.Size(267, 22);
            this.Menú_Contextual_Dibujar_Marco.Text = "Draw the icon frame";
            this.Menú_Contextual_Dibujar_Marco.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Dibujar_Marco_CheckedChanged);
            // 
            // Menú_Contextual_Dibujar_Icono
            // 
            this.Menú_Contextual_Dibujar_Icono.Checked = true;
            this.Menú_Contextual_Dibujar_Icono.CheckOnClick = true;
            this.Menú_Contextual_Dibujar_Icono.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menú_Contextual_Dibujar_Icono.Name = "Menú_Contextual_Dibujar_Icono";
            this.Menú_Contextual_Dibujar_Icono.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.Menú_Contextual_Dibujar_Icono.Size = new System.Drawing.Size(267, 22);
            this.Menú_Contextual_Dibujar_Icono.Text = "Draw the icon";
            this.Menú_Contextual_Dibujar_Icono.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Dibujar_Icono_CheckedChanged);
            // 
            // Ventana_Visor_Logros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.ContextMenuStrip = this.Menú_Contextual;
            this.Controls.Add(this.DataGridView_Principal);
            this.Controls.Add(this.Etiqueta_Jugador);
            this.Controls.Add(this.Etiqueta_Mundo);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ComboBox_Jugador);
            this.Controls.Add(this.ComboBox_Mundo);
            this.Controls.Add(this.Picture_FPS);
            this.Controls.Add(this.Barra_Estado);
            this.DoubleBuffered = true;
            this.Name = "Ventana_Visor_Logros";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advancements Viewer by Jupisoft";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventana_Plantilla_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventana_Plantilla_FormClosed);
            this.Load += new System.EventHandler(this.Ventana_Plantilla_Load);
            this.Shown += new System.EventHandler(this.Ventana_Plantilla_Shown);
            this.SizeChanged += new System.EventHandler(this.Ventana_Plantilla_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Plantilla_KeyDown);
            this.Menú_Contextual.ResumeLayout(false);
            this.Barra_Estado.ResumeLayout(false);
            this.Barra_Estado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Principal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_FPS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip Menú_Contextual;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Donar;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_1;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Visor_Ayuda;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Acerca;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Depurador_Excepciones;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Abrir_Carpeta_Guardado;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_2;
        private System.Windows.Forms.ToolStrip Barra_Estado;
        private System.Windows.Forms.ToolStripButton Barra_Estado_Botón_Excepción;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_1;
        private System.Windows.Forms.ToolStripLabel Barra_Estado_Etiqueta_CPU;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_2;
        private System.Windows.Forms.ToolStripLabel Barra_Estado_Etiqueta_Memoria;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_3;
        private System.Windows.Forms.ToolStripLabel Barra_Estado_Etiqueta_FPS;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_4;
        private System.Windows.Forms.ToolStripLabel Barra_Estado_Etiqueta_Sugerencia;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_5;
        private System.Windows.Forms.Timer Temporizador_Principal;
        private System.Windows.Forms.PictureBox Picture_FPS;
        private System.Windows.Forms.Label Etiqueta_Jugador;
        private System.Windows.Forms.Label Etiqueta_Mundo;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox ComboBox_Jugador;
        private System.Windows.Forms.ComboBox ComboBox_Mundo;
        private System.Windows.Forms.DataGridView DataGridView_Principal;
        private System.Windows.Forms.DataGridViewImageColumn Columna_Icono;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Título;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Categoría;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Marco;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Oculto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Completado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Descripción;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Ruta;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Mostrar_Todos;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Dibujar_Fondo;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Dibujar_Marco;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Dibujar_Icono;
    }
}

