namespace Minecraft_Tools
{
    partial class Ventana_Generador_UUID
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
            this.Menú_Contextual = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Menú_Contextual_Donar = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_1 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Visor_Ayuda = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Acerca = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Depurador_Excepciones = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Abrir_Carpeta_Guardado = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_2 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Actualizar = new System.Windows.Forms.ToolStripMenuItem();
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
            this.Picture_FPS = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Etiqueta_Texto = new System.Windows.Forms.Label();
            this.TextBox_Texto = new System.Windows.Forms.TextBox();
            this.TextBox_UUID = new System.Windows.Forms.TextBox();
            this.Etiqueta_UUID = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.CheckBox_Offline = new System.Windows.Forms.CheckBox();
            this.Menú_Contextual.SuspendLayout();
            this.Barra_Estado.SuspendLayout();
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
            this.Menú_Contextual_Actualizar});
            this.Menú_Contextual.Name = "Menú_Contextual";
            this.Menú_Contextual.Size = new System.Drawing.Size(268, 148);
            this.Menú_Contextual.Opening += new System.ComponentModel.CancelEventHandler(this.Menú_Contextual_Opening);
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
            // Menú_Contextual_Separador_1
            // 
            this.Menú_Contextual_Separador_1.Name = "Menú_Contextual_Separador_1";
            this.Menú_Contextual_Separador_1.Size = new System.Drawing.Size(264, 6);
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
            // Menú_Contextual_Separador_2
            // 
            this.Menú_Contextual_Separador_2.Name = "Menú_Contextual_Separador_2";
            this.Menú_Contextual_Separador_2.Size = new System.Drawing.Size(264, 6);
            // 
            // Menú_Contextual_Actualizar
            // 
            this.Menú_Contextual_Actualizar.Image = global::Minecraft_Tools.Properties.Resources.Actualizar;
            this.Menú_Contextual_Actualizar.Name = "Menú_Contextual_Actualizar";
            this.Menú_Contextual_Actualizar.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.Menú_Contextual_Actualizar.Size = new System.Drawing.Size(267, 22);
            this.Menú_Contextual_Actualizar.Text = "Refresh";
            this.Menú_Contextual_Actualizar.Click += new System.EventHandler(this.Menú_Contextual_Actualizar_Click);
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
            this.Barra_Estado.Location = new System.Drawing.Point(0, 72);
            this.Barra_Estado.Name = "Barra_Estado";
            this.Barra_Estado.Size = new System.Drawing.Size(624, 25);
            this.Barra_Estado.TabIndex = 5;
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
            this.Barra_Estado_Etiqueta_CPU.Image = global::Minecraft_Tools.Properties.Resources.CPU;
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
            this.Barra_Estado_Etiqueta_Memoria.Image = global::Minecraft_Tools.Properties.Resources.RAM;
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
            this.Barra_Estado_Etiqueta_FPS.Image = global::Minecraft_Tools.Properties.Resources.FPS;
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
            this.Barra_Estado_Etiqueta_Sugerencia.Size = new System.Drawing.Size(626, 16);
            this.Barra_Estado_Etiqueta_Sugerencia.Text = "Tip: it works as expected, although the UUIDs supplied by the Mojang servers for " +
    "the player names will be different.";
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
            // Picture_FPS
            // 
            this.Picture_FPS.BackColor = System.Drawing.Color.Transparent;
            this.Picture_FPS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Picture_FPS.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Picture_FPS.InitialImage = null;
            this.Picture_FPS.Location = new System.Drawing.Point(0, 64);
            this.Picture_FPS.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.Picture_FPS.Name = "Picture_FPS";
            this.Picture_FPS.Size = new System.Drawing.Size(624, 8);
            this.Picture_FPS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Picture_FPS.TabIndex = 10;
            this.Picture_FPS.TabStop = false;
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
            // Etiqueta_Texto
            // 
            this.Etiqueta_Texto.AutoSize = true;
            this.Etiqueta_Texto.Location = new System.Drawing.Point(12, 15);
            this.Etiqueta_Texto.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.Etiqueta_Texto.Name = "Etiqueta_Texto";
            this.Etiqueta_Texto.Size = new System.Drawing.Size(31, 13);
            this.Etiqueta_Texto.TabIndex = 0;
            this.Etiqueta_Texto.Text = "Text:";
            // 
            // TextBox_Texto
            // 
            this.TextBox_Texto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_Texto.BackColor = System.Drawing.Color.White;
            this.TextBox_Texto.Location = new System.Drawing.Point(52, 12);
            this.TextBox_Texto.Name = "TextBox_Texto";
            this.TextBox_Texto.Size = new System.Drawing.Size(413, 20);
            this.TextBox_Texto.TabIndex = 1;
            this.TextBox_Texto.TextChanged += new System.EventHandler(this.TextBox_Texto_TextChanged);
            this.TextBox_Texto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Generador_UUID_KeyDown);
            // 
            // TextBox_UUID
            // 
            this.TextBox_UUID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_UUID.BackColor = System.Drawing.Color.White;
            this.TextBox_UUID.Location = new System.Drawing.Point(52, 38);
            this.TextBox_UUID.Name = "TextBox_UUID";
            this.TextBox_UUID.ReadOnly = true;
            this.TextBox_UUID.Size = new System.Drawing.Size(560, 20);
            this.TextBox_UUID.TabIndex = 4;
            this.TextBox_UUID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Generador_UUID_KeyDown);
            // 
            // Etiqueta_UUID
            // 
            this.Etiqueta_UUID.AutoSize = true;
            this.Etiqueta_UUID.Location = new System.Drawing.Point(12, 41);
            this.Etiqueta_UUID.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.Etiqueta_UUID.Name = "Etiqueta_UUID";
            this.Etiqueta_UUID.Size = new System.Drawing.Size(37, 13);
            this.Etiqueta_UUID.TabIndex = 3;
            this.Etiqueta_UUID.Text = "UUID:";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.BackColor = System.Drawing.Color.White;
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(592, 38);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(20, 20);
            this.textBox3.TabIndex = 7;
            this.textBox3.Visible = false;
            // 
            // CheckBox_Offline
            // 
            this.CheckBox_Offline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBox_Offline.AutoSize = true;
            this.CheckBox_Offline.Location = new System.Drawing.Point(471, 14);
            this.CheckBox_Offline.Name = "CheckBox_Offline";
            this.CheckBox_Offline.Size = new System.Drawing.Size(141, 17);
            this.CheckBox_Offline.TabIndex = 2;
            this.CheckBox_Offline.Text = "Emulate an offline player";
            this.CheckBox_Offline.UseVisualStyleBackColor = true;
            this.CheckBox_Offline.CheckedChanged += new System.EventHandler(this.CheckBox_Offline_CheckedChanged);
            // 
            // Ventana_Generador_UUID
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 97);
            this.ContextMenuStrip = this.Menú_Contextual;
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.TextBox_UUID);
            this.Controls.Add(this.Etiqueta_UUID);
            this.Controls.Add(this.TextBox_Texto);
            this.Controls.Add(this.Etiqueta_Texto);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Picture_FPS);
            this.Controls.Add(this.Barra_Estado);
            this.Controls.Add(this.CheckBox_Offline);
            this.DoubleBuffered = true;
            this.Name = "Ventana_Generador_UUID";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UUID Generator by Jupisoft";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventana_Generador_UUID_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventana_Generador_UUID_FormClosed);
            this.Load += new System.EventHandler(this.Ventana_Generador_UUID_Load);
            this.Shown += new System.EventHandler(this.Ventana_Generador_UUID_Shown);
            this.SizeChanged += new System.EventHandler(this.Ventana_Generador_UUID_SizeChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Ventana_Generador_UUID_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Ventana_Generador_UUID_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Generador_UUID_KeyDown);
            this.Menú_Contextual.ResumeLayout(false);
            this.Barra_Estado.ResumeLayout(false);
            this.Barra_Estado.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Actualizar;
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label Etiqueta_Texto;
        private System.Windows.Forms.TextBox TextBox_Texto;
        private System.Windows.Forms.TextBox TextBox_UUID;
        private System.Windows.Forms.Label Etiqueta_UUID;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.CheckBox CheckBox_Offline;
    }
}

