namespace Minecraft_Tools
{
    partial class Ventana_Monster_High
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
            this.ComboBox_Personaje = new System.Windows.Forms.ComboBox();
            this.Picture_Header = new System.Windows.Forms.PictureBox();
            this.RichTextBox_Personaje = new System.Windows.Forms.RichTextBox();
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
            this.Menú_Contextual_Ajustar_Imágenes = new System.Windows.Forms.ToolStripMenuItem();
            this.Tabla_Principal = new System.Windows.Forms.TableLayoutPanel();
            this.Picture_Thumb = new System.Windows.Forms.PictureBox();
            this.Picture_Grid = new System.Windows.Forms.PictureBox();
            this.Picture_Hero = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Header)).BeginInit();
            this.Menú_Contextual.SuspendLayout();
            this.Tabla_Principal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Thumb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Hero)).BeginInit();
            this.SuspendLayout();
            // 
            // ComboBox_Personaje
            // 
            this.ComboBox_Personaje.BackColor = System.Drawing.Color.White;
            this.ComboBox_Personaje.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComboBox_Personaje.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Personaje.FormattingEnabled = true;
            this.ComboBox_Personaje.Location = new System.Drawing.Point(360, 309);
            this.ComboBox_Personaje.Margin = new System.Windows.Forms.Padding(0);
            this.ComboBox_Personaje.Name = "ComboBox_Personaje";
            this.ComboBox_Personaje.Size = new System.Drawing.Size(618, 21);
            this.ComboBox_Personaje.TabIndex = 0;
            this.ComboBox_Personaje.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Personaje_SelectedIndexChanged);
            this.ComboBox_Personaje.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Monster_High_KeyDown);
            this.ComboBox_Personaje.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ComboBox_Personaje_MouseDown);
            // 
            // Picture_Header
            // 
            this.Picture_Header.BackColor = System.Drawing.Color.Black;
            this.Tabla_Principal.SetColumnSpan(this.Picture_Header, 2);
            this.Picture_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Picture_Header.InitialImage = null;
            this.Picture_Header.Location = new System.Drawing.Point(185, 0);
            this.Picture_Header.Margin = new System.Windows.Forms.Padding(0);
            this.Picture_Header.Name = "Picture_Header";
            this.Tabla_Principal.SetRowSpan(this.Picture_Header, 2);
            this.Picture_Header.Size = new System.Drawing.Size(793, 309);
            this.Picture_Header.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Picture_Header.TabIndex = 15;
            this.Picture_Header.TabStop = false;
            this.Picture_Header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pictures_MouseDown);
            // 
            // RichTextBox_Personaje
            // 
            this.RichTextBox_Personaje.BackColor = System.Drawing.Color.White;
            this.RichTextBox_Personaje.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichTextBox_Personaje.ContextMenuStrip = this.Menú_Contextual;
            this.RichTextBox_Personaje.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RichTextBox_Personaje.Location = new System.Drawing.Point(360, 330);
            this.RichTextBox_Personaje.Margin = new System.Windows.Forms.Padding(0);
            this.RichTextBox_Personaje.Name = "RichTextBox_Personaje";
            this.RichTextBox_Personaje.ReadOnly = true;
            this.RichTextBox_Personaje.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RichTextBox_Personaje.ShowSelectionMargin = true;
            this.RichTextBox_Personaje.Size = new System.Drawing.Size(618, 331);
            this.RichTextBox_Personaje.TabIndex = 1;
            this.RichTextBox_Personaje.Text = "";
            this.RichTextBox_Personaje.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Monster_High_KeyDown);
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
            this.Menú_Contextual_Ajustar_Imágenes});
            this.Menú_Contextual.Name = "Menú_Contextual";
            this.Menú_Contextual.Size = new System.Drawing.Size(434, 308);
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
            // Menú_Contextual_Ajustar_Imágenes
            // 
            this.Menú_Contextual_Ajustar_Imágenes.Checked = true;
            this.Menú_Contextual_Ajustar_Imágenes.CheckOnClick = true;
            this.Menú_Contextual_Ajustar_Imágenes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menú_Contextual_Ajustar_Imágenes.Name = "Menú_Contextual_Ajustar_Imágenes";
            this.Menú_Contextual_Ajustar_Imágenes.Size = new System.Drawing.Size(433, 22);
            this.Menú_Contextual_Ajustar_Imágenes.Text = "Adjust the images if they won\'t fit";
            this.Menú_Contextual_Ajustar_Imágenes.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Ajustar_Imágenes_CheckedChanged);
            // 
            // Tabla_Principal
            // 
            this.Tabla_Principal.ColumnCount = 3;
            this.Tabla_Principal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.Tabla_Principal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.Tabla_Principal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Tabla_Principal.Controls.Add(this.Picture_Thumb, 0, 0);
            this.Tabla_Principal.Controls.Add(this.RichTextBox_Personaje, 2, 3);
            this.Tabla_Principal.Controls.Add(this.Picture_Grid, 0, 1);
            this.Tabla_Principal.Controls.Add(this.Picture_Hero, 0, 2);
            this.Tabla_Principal.Controls.Add(this.Picture_Header, 1, 0);
            this.Tabla_Principal.Controls.Add(this.ComboBox_Personaje, 2, 2);
            this.Tabla_Principal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabla_Principal.Location = new System.Drawing.Point(0, 0);
            this.Tabla_Principal.Name = "Tabla_Principal";
            this.Tabla_Principal.RowCount = 4;
            this.Tabla_Principal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.Tabla_Principal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 124F));
            this.Tabla_Principal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.Tabla_Principal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Tabla_Principal.Size = new System.Drawing.Size(978, 661);
            this.Tabla_Principal.TabIndex = 0;
            // 
            // Picture_Thumb
            // 
            this.Picture_Thumb.BackColor = System.Drawing.Color.Black;
            this.Picture_Thumb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Picture_Thumb.InitialImage = null;
            this.Picture_Thumb.Location = new System.Drawing.Point(0, 0);
            this.Picture_Thumb.Margin = new System.Windows.Forms.Padding(0);
            this.Picture_Thumb.Name = "Picture_Thumb";
            this.Picture_Thumb.Size = new System.Drawing.Size(185, 185);
            this.Picture_Thumb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Picture_Thumb.TabIndex = 17;
            this.Picture_Thumb.TabStop = false;
            this.Picture_Thumb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pictures_MouseDown);
            // 
            // Picture_Grid
            // 
            this.Picture_Grid.BackColor = System.Drawing.Color.Black;
            this.Picture_Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Picture_Grid.InitialImage = null;
            this.Picture_Grid.Location = new System.Drawing.Point(0, 185);
            this.Picture_Grid.Margin = new System.Windows.Forms.Padding(0);
            this.Picture_Grid.Name = "Picture_Grid";
            this.Picture_Grid.Size = new System.Drawing.Size(185, 124);
            this.Picture_Grid.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Picture_Grid.TabIndex = 18;
            this.Picture_Grid.TabStop = false;
            this.Picture_Grid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pictures_MouseDown);
            // 
            // Picture_Hero
            // 
            this.Picture_Hero.BackColor = System.Drawing.Color.Black;
            this.Tabla_Principal.SetColumnSpan(this.Picture_Hero, 2);
            this.Picture_Hero.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Picture_Hero.InitialImage = null;
            this.Picture_Hero.Location = new System.Drawing.Point(0, 309);
            this.Picture_Hero.Margin = new System.Windows.Forms.Padding(0);
            this.Picture_Hero.Name = "Picture_Hero";
            this.Tabla_Principal.SetRowSpan(this.Picture_Hero, 2);
            this.Picture_Hero.Size = new System.Drawing.Size(360, 352);
            this.Picture_Hero.TabIndex = 16;
            this.Picture_Hero.TabStop = false;
            this.Picture_Hero.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pictures_MouseDown);
            // 
            // Ventana_Monster_High
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 661);
            this.ContextMenuStrip = this.Menú_Contextual;
            this.Controls.Add(this.Tabla_Principal);
            this.DoubleBuffered = true;
            this.Name = "Ventana_Monster_High";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monster High Characters by Jupisoft";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventana_Monster_High_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventana_Monster_High_FormClosed);
            this.Load += new System.EventHandler(this.Ventana_Monster_High_Load);
            this.Shown += new System.EventHandler(this.Ventana_Monster_High_Shown);
            this.SizeChanged += new System.EventHandler(this.Ventana_Monster_High_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Monster_High_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Header)).EndInit();
            this.Menú_Contextual.ResumeLayout(false);
            this.Tabla_Principal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Thumb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Hero)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox ComboBox_Personaje;
        private System.Windows.Forms.PictureBox Picture_Header;
        private System.Windows.Forms.RichTextBox RichTextBox_Personaje;
        private System.Windows.Forms.TableLayoutPanel Tabla_Principal;
        private System.Windows.Forms.PictureBox Picture_Hero;
        private System.Windows.Forms.PictureBox Picture_Thumb;
        private System.Windows.Forms.PictureBox Picture_Grid;
        private System.Windows.Forms.ContextMenuStrip Menú_Contextual;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Ajustar_Imágenes;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_JPEG;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_12_2_PNG;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_JPEG;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Exportar_Pack_Recursos_Cuadros_1_13_PNG;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_1;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_2;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Visor_Ayuda;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Acerca;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Depurador_Excepciones;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Abrir_Carpeta;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Actualizar;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Filtro_Negativo;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Filtro_Raíz_Cuadrada;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Filtro_Logaritmo;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_3;
    }
}