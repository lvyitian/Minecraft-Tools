namespace Minecraft_Tools
{
    partial class Ventana_Filtros_Tiempo_Real
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
            this.Menú_Contextual_Siempre_Visible = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_1 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Negativo = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Negativo_Posterior = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Desaturado_Anterior = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Desaturado_Posterior = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_2 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Mover_Cursor_Centro = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Seguir_Cursor = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Mantener_Cursor_Centrado = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Zoom_Suave = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_3 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Dibujar_Cursor = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Pantalla_Completa = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_GitHub = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_4 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Copiar = new System.Windows.Forms.ToolStripMenuItem();
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
            this.Tabla_Principal = new System.Windows.Forms.TableLayoutPanel();
            this.ComboBox_Zoom = new System.Windows.Forms.ComboBox();
            this.ComboBox_Filtro = new System.Windows.Forms.ComboBox();
            this.Picture = new System.Windows.Forms.PictureBox();
            this.Menú_Contextual.SuspendLayout();
            this.Barra_Estado.SuspendLayout();
            this.Tabla_Principal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            this.SuspendLayout();
            // 
            // Menú_Contextual
            // 
            this.Menú_Contextual.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menú_Contextual_Visor_Ayuda,
            this.Menú_Contextual_Acerca,
            this.Menú_Contextual_Depurador_Excepciones,
            this.Menú_Contextual_Siempre_Visible,
            this.Menú_Contextual_Separador_1,
            this.Menú_Contextual_Negativo,
            this.Menú_Contextual_Negativo_Posterior,
            this.Menú_Contextual_Desaturado_Anterior,
            this.Menú_Contextual_Desaturado_Posterior,
            this.Menú_Contextual_Separador_2,
            this.Menú_Contextual_Mover_Cursor_Centro,
            this.Menú_Contextual_Seguir_Cursor,
            this.Menú_Contextual_Mantener_Cursor_Centrado,
            this.Menú_Contextual_Zoom_Suave,
            this.Menú_Contextual_Separador_3,
            this.Menú_Contextual_Dibujar_Cursor,
            this.Menú_Contextual_Pantalla_Completa,
            this.Menú_Contextual_GitHub,
            this.Menú_Contextual_Separador_4,
            this.Menú_Contextual_Copiar,
            this.Menú_Contextual_Guardar});
            this.Menú_Contextual.Name = "Menú_Contextual";
            this.Menú_Contextual.Size = new System.Drawing.Size(292, 402);
            // 
            // Menú_Contextual_Visor_Ayuda
            // 
            this.Menú_Contextual_Visor_Ayuda.Image = global::Minecraft_Tools.Properties.Resources.Ayuda;
            this.Menú_Contextual_Visor_Ayuda.Name = "Menú_Contextual_Visor_Ayuda";
            this.Menú_Contextual_Visor_Ayuda.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.Menú_Contextual_Visor_Ayuda.Size = new System.Drawing.Size(291, 22);
            this.Menú_Contextual_Visor_Ayuda.Text = "Help viewer...";
            this.Menú_Contextual_Visor_Ayuda.Click += new System.EventHandler(this.Menú_Contextual_Visor_Ayuda_Click);
            // 
            // Menú_Contextual_Acerca
            // 
            this.Menú_Contextual_Acerca.Image = global::Minecraft_Tools.Properties.Resources.Jupisoft_16;
            this.Menú_Contextual_Acerca.Name = "Menú_Contextual_Acerca";
            this.Menú_Contextual_Acerca.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.Menú_Contextual_Acerca.Size = new System.Drawing.Size(291, 22);
            this.Menú_Contextual_Acerca.Text = "About...";
            this.Menú_Contextual_Acerca.Click += new System.EventHandler(this.Menú_Contextual_Acerca_Click);
            // 
            // Menú_Contextual_Depurador_Excepciones
            // 
            this.Menú_Contextual_Depurador_Excepciones.Image = global::Minecraft_Tools.Properties.Resources.Excepción;
            this.Menú_Contextual_Depurador_Excepciones.Name = "Menú_Contextual_Depurador_Excepciones";
            this.Menú_Contextual_Depurador_Excepciones.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.Menú_Contextual_Depurador_Excepciones.Size = new System.Drawing.Size(291, 22);
            this.Menú_Contextual_Depurador_Excepciones.Text = "Exception debugger...";
            this.Menú_Contextual_Depurador_Excepciones.Click += new System.EventHandler(this.Menú_Contextual_Depurador_Excepciones_Click);
            // 
            // Menú_Contextual_Siempre_Visible
            // 
            this.Menú_Contextual_Siempre_Visible.Checked = true;
            this.Menú_Contextual_Siempre_Visible.CheckOnClick = true;
            this.Menú_Contextual_Siempre_Visible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menú_Contextual_Siempre_Visible.Name = "Menú_Contextual_Siempre_Visible";
            this.Menú_Contextual_Siempre_Visible.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.Menú_Contextual_Siempre_Visible.Size = new System.Drawing.Size(291, 22);
            this.Menú_Contextual_Siempre_Visible.Text = "Always on top";
            this.Menú_Contextual_Siempre_Visible.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Siempre_Visible_CheckedChanged);
            // 
            // Menú_Contextual_Separador_1
            // 
            this.Menú_Contextual_Separador_1.Name = "Menú_Contextual_Separador_1";
            this.Menú_Contextual_Separador_1.Size = new System.Drawing.Size(288, 6);
            // 
            // Menú_Contextual_Negativo
            // 
            this.Menú_Contextual_Negativo.CheckOnClick = true;
            this.Menú_Contextual_Negativo.Name = "Menú_Contextual_Negativo";
            this.Menú_Contextual_Negativo.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.Menú_Contextual_Negativo.Size = new System.Drawing.Size(291, 22);
            this.Menú_Contextual_Negativo.Text = "Pre negative filter";
            this.Menú_Contextual_Negativo.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Negativo_CheckedChanged);
            // 
            // Menú_Contextual_Negativo_Posterior
            // 
            this.Menú_Contextual_Negativo_Posterior.CheckOnClick = true;
            this.Menú_Contextual_Negativo_Posterior.Name = "Menú_Contextual_Negativo_Posterior";
            this.Menú_Contextual_Negativo_Posterior.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.Menú_Contextual_Negativo_Posterior.Size = new System.Drawing.Size(291, 22);
            this.Menú_Contextual_Negativo_Posterior.Text = "Post negative filter";
            this.Menú_Contextual_Negativo_Posterior.Click += new System.EventHandler(this.Menú_Contextual_Negativo_Posterior_Click);
            // 
            // Menú_Contextual_Desaturado_Anterior
            // 
            this.Menú_Contextual_Desaturado_Anterior.CheckOnClick = true;
            this.Menú_Contextual_Desaturado_Anterior.Name = "Menú_Contextual_Desaturado_Anterior";
            this.Menú_Contextual_Desaturado_Anterior.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.Menú_Contextual_Desaturado_Anterior.Size = new System.Drawing.Size(291, 22);
            this.Menú_Contextual_Desaturado_Anterior.Text = "Pre desaturate filter";
            this.Menú_Contextual_Desaturado_Anterior.Click += new System.EventHandler(this.Menú_Contextual_Escala_Grises_Anterior_Click);
            // 
            // Menú_Contextual_Desaturado_Posterior
            // 
            this.Menú_Contextual_Desaturado_Posterior.CheckOnClick = true;
            this.Menú_Contextual_Desaturado_Posterior.Name = "Menú_Contextual_Desaturado_Posterior";
            this.Menú_Contextual_Desaturado_Posterior.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.Menú_Contextual_Desaturado_Posterior.Size = new System.Drawing.Size(291, 22);
            this.Menú_Contextual_Desaturado_Posterior.Text = "Post desaturate filter";
            this.Menú_Contextual_Desaturado_Posterior.Click += new System.EventHandler(this.Menú_Contextual_Desaturado_Posterior_Click);
            // 
            // Menú_Contextual_Separador_2
            // 
            this.Menú_Contextual_Separador_2.Name = "Menú_Contextual_Separador_2";
            this.Menú_Contextual_Separador_2.Size = new System.Drawing.Size(288, 6);
            // 
            // Menú_Contextual_Mover_Cursor_Centro
            // 
            this.Menú_Contextual_Mover_Cursor_Centro.Image = global::Minecraft_Tools.Properties.Resources.Posición;
            this.Menú_Contextual_Mover_Cursor_Centro.Name = "Menú_Contextual_Mover_Cursor_Centro";
            this.Menú_Contextual_Mover_Cursor_Centro.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.Menú_Contextual_Mover_Cursor_Centro.Size = new System.Drawing.Size(291, 22);
            this.Menú_Contextual_Mover_Cursor_Centro.Text = "Move the cursor to the center";
            this.Menú_Contextual_Mover_Cursor_Centro.Click += new System.EventHandler(this.Menú_Contextual_Mover_Cursor_Centro_Click);
            // 
            // Menú_Contextual_Seguir_Cursor
            // 
            this.Menú_Contextual_Seguir_Cursor.Checked = true;
            this.Menú_Contextual_Seguir_Cursor.CheckOnClick = true;
            this.Menú_Contextual_Seguir_Cursor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menú_Contextual_Seguir_Cursor.Name = "Menú_Contextual_Seguir_Cursor";
            this.Menú_Contextual_Seguir_Cursor.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.Menú_Contextual_Seguir_Cursor.Size = new System.Drawing.Size(291, 22);
            this.Menú_Contextual_Seguir_Cursor.Text = "Follow cursor";
            this.Menú_Contextual_Seguir_Cursor.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Seguir_Cursor_CheckedChanged);
            // 
            // Menú_Contextual_Mantener_Cursor_Centrado
            // 
            this.Menú_Contextual_Mantener_Cursor_Centrado.CheckOnClick = true;
            this.Menú_Contextual_Mantener_Cursor_Centrado.Name = "Menú_Contextual_Mantener_Cursor_Centrado";
            this.Menú_Contextual_Mantener_Cursor_Centrado.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.Menú_Contextual_Mantener_Cursor_Centrado.Size = new System.Drawing.Size(291, 22);
            this.Menú_Contextual_Mantener_Cursor_Centrado.Text = "Keep the cursor centered";
            this.Menú_Contextual_Mantener_Cursor_Centrado.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Mantener_Cursor_Centrado_CheckedChanged);
            // 
            // Menú_Contextual_Zoom_Suave
            // 
            this.Menú_Contextual_Zoom_Suave.CheckOnClick = true;
            this.Menú_Contextual_Zoom_Suave.Name = "Menú_Contextual_Zoom_Suave";
            this.Menú_Contextual_Zoom_Suave.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.Menú_Contextual_Zoom_Suave.Size = new System.Drawing.Size(291, 22);
            this.Menú_Contextual_Zoom_Suave.Text = "Smooth zoom";
            this.Menú_Contextual_Zoom_Suave.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Zoom_Suave_CheckedChanged);
            // 
            // Menú_Contextual_Separador_3
            // 
            this.Menú_Contextual_Separador_3.Name = "Menú_Contextual_Separador_3";
            this.Menú_Contextual_Separador_3.Size = new System.Drawing.Size(288, 6);
            // 
            // Menú_Contextual_Dibujar_Cursor
            // 
            this.Menú_Contextual_Dibujar_Cursor.CheckOnClick = true;
            this.Menú_Contextual_Dibujar_Cursor.Name = "Menú_Contextual_Dibujar_Cursor";
            this.Menú_Contextual_Dibujar_Cursor.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F9)));
            this.Menú_Contextual_Dibujar_Cursor.Size = new System.Drawing.Size(291, 22);
            this.Menú_Contextual_Dibujar_Cursor.Text = "Draw the current cursor";
            // 
            // Menú_Contextual_Pantalla_Completa
            // 
            this.Menú_Contextual_Pantalla_Completa.CheckOnClick = true;
            this.Menú_Contextual_Pantalla_Completa.Name = "Menú_Contextual_Pantalla_Completa";
            this.Menú_Contextual_Pantalla_Completa.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F11)));
            this.Menú_Contextual_Pantalla_Completa.Size = new System.Drawing.Size(291, 22);
            this.Menú_Contextual_Pantalla_Completa.Text = "Full screen";
            this.Menú_Contextual_Pantalla_Completa.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Pantalla_Completa_CheckedChanged);
            // 
            // Menú_Contextual_GitHub
            // 
            this.Menú_Contextual_GitHub.CheckOnClick = true;
            this.Menú_Contextual_GitHub.Enabled = false;
            this.Menú_Contextual_GitHub.Name = "Menú_Contextual_GitHub";
            this.Menú_Contextual_GitHub.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F12)));
            this.Menú_Contextual_GitHub.Size = new System.Drawing.Size(291, 22);
            this.Menú_Contextual_GitHub.Text = "Commit changes to GitHub";
            this.Menú_Contextual_GitHub.CheckedChanged += new System.EventHandler(this.Menú_Contextual_GitHub_CheckedChanged);
            // 
            // Menú_Contextual_Separador_4
            // 
            this.Menú_Contextual_Separador_4.Name = "Menú_Contextual_Separador_4";
            this.Menú_Contextual_Separador_4.Size = new System.Drawing.Size(288, 6);
            // 
            // Menú_Contextual_Copiar
            // 
            this.Menú_Contextual_Copiar.Image = global::Minecraft_Tools.Properties.Resources.Copiar;
            this.Menú_Contextual_Copiar.Name = "Menú_Contextual_Copiar";
            this.Menú_Contextual_Copiar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.Menú_Contextual_Copiar.Size = new System.Drawing.Size(291, 22);
            this.Menú_Contextual_Copiar.Text = "Copy";
            this.Menú_Contextual_Copiar.Click += new System.EventHandler(this.Menú_Contextual_Copiar_Click);
            // 
            // Menú_Contextual_Guardar
            // 
            this.Menú_Contextual_Guardar.Image = global::Minecraft_Tools.Properties.Resources.Guardar;
            this.Menú_Contextual_Guardar.Name = "Menú_Contextual_Guardar";
            this.Menú_Contextual_Guardar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.Menú_Contextual_Guardar.Size = new System.Drawing.Size(291, 22);
            this.Menú_Contextual_Guardar.Text = "Save";
            this.Menú_Contextual_Guardar.Click += new System.EventHandler(this.Menú_Contextual_Guardar_Click);
            // 
            // Barra_Estado
            // 
            this.Barra_Estado.BackColor = System.Drawing.SystemColors.Control;
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
            this.Barra_Estado.Location = new System.Drawing.Point(0, 277);
            this.Barra_Estado.Name = "Barra_Estado";
            this.Barra_Estado.Size = new System.Drawing.Size(256, 25);
            this.Barra_Estado.TabIndex = 0;
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
            this.Barra_Estado_Etiqueta_Sugerencia.Size = new System.Drawing.Size(215, 16);
            this.Barra_Estado_Etiqueta_Sugerencia.Text = "Tip: drop an image to save it filtered.";
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
            this.Tabla_Principal.BackColor = System.Drawing.SystemColors.Control;
            this.Tabla_Principal.ColumnCount = 2;
            this.Tabla_Principal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Tabla_Principal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.Tabla_Principal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.Tabla_Principal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.Tabla_Principal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.Tabla_Principal.Controls.Add(this.ComboBox_Zoom, 1, 0);
            this.Tabla_Principal.Controls.Add(this.ComboBox_Filtro, 0, 0);
            this.Tabla_Principal.Dock = System.Windows.Forms.DockStyle.Top;
            this.Tabla_Principal.Location = new System.Drawing.Point(0, 0);
            this.Tabla_Principal.Name = "Tabla_Principal";
            this.Tabla_Principal.RowCount = 1;
            this.Tabla_Principal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Tabla_Principal.Size = new System.Drawing.Size(256, 21);
            this.Tabla_Principal.TabIndex = 0;
            // 
            // ComboBox_Zoom
            // 
            this.ComboBox_Zoom.BackColor = System.Drawing.Color.White;
            this.ComboBox_Zoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComboBox_Zoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Zoom.FormattingEnabled = true;
            this.ComboBox_Zoom.Items.AddRange(new object[] {
            "1x",
            "2x",
            "4x",
            "8x",
            "16x",
            "32x",
            "64x",
            "128x",
            "256x"});
            this.ComboBox_Zoom.Location = new System.Drawing.Point(206, 0);
            this.ComboBox_Zoom.Margin = new System.Windows.Forms.Padding(0);
            this.ComboBox_Zoom.Name = "ComboBox_Zoom";
            this.ComboBox_Zoom.Size = new System.Drawing.Size(50, 21);
            this.ComboBox_Zoom.TabIndex = 1;
            this.ComboBox_Zoom.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Zoom_SelectedIndexChanged);
            this.ComboBox_Zoom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Filtros_Tiempo_Real_KeyDown);
            // 
            // ComboBox_Filtro
            // 
            this.ComboBox_Filtro.BackColor = System.Drawing.Color.White;
            this.ComboBox_Filtro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComboBox_Filtro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Filtro.FormattingEnabled = true;
            this.ComboBox_Filtro.Location = new System.Drawing.Point(0, 0);
            this.ComboBox_Filtro.Margin = new System.Windows.Forms.Padding(0);
            this.ComboBox_Filtro.Name = "ComboBox_Filtro";
            this.ComboBox_Filtro.Size = new System.Drawing.Size(206, 21);
            this.ComboBox_Filtro.TabIndex = 0;
            this.ComboBox_Filtro.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Filtro_SelectedIndexChanged);
            this.ComboBox_Filtro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Filtros_Tiempo_Real_KeyDown);
            // 
            // Picture
            // 
            this.Picture.BackColor = System.Drawing.Color.White;
            this.Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Picture.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Picture.InitialImage = null;
            this.Picture.Location = new System.Drawing.Point(0, 21);
            this.Picture.Name = "Picture";
            this.Picture.Size = new System.Drawing.Size(256, 256);
            this.Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Picture.TabIndex = 4;
            this.Picture.TabStop = false;
            this.Picture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_MouseDown);
            // 
            // Ventana_Filtros_Tiempo_Real
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(256, 302);
            this.ContextMenuStrip = this.Menú_Contextual;
            this.Controls.Add(this.Picture);
            this.Controls.Add(this.Tabla_Principal);
            this.Controls.Add(this.Barra_Estado);
            this.DoubleBuffered = true;
            this.Name = "Ventana_Filtros_Tiempo_Real";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Real Time Filters by Jupisoft";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventana_Filtros_Tiempo_Real_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventana_Filtros_Tiempo_Real_FormClosed);
            this.Load += new System.EventHandler(this.Ventana_Filtros_Tiempo_Real_Load);
            this.Shown += new System.EventHandler(this.Ventana_Filtros_Tiempo_Real_Shown);
            this.SizeChanged += new System.EventHandler(this.Ventana_Filtros_Tiempo_Real_SizeChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Ventana_Filtros_Pantalla_Tiempo_Real_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Ventana_Filtros_Pantalla_Tiempo_Real_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Filtros_Tiempo_Real_KeyDown);
            this.Menú_Contextual.ResumeLayout(false);
            this.Barra_Estado.ResumeLayout(false);
            this.Barra_Estado.PerformLayout();
            this.Tabla_Principal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip Menú_Contextual;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Visor_Ayuda;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Acerca;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Depurador_Excepciones;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Siempre_Visible;
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
        private System.Windows.Forms.TableLayoutPanel Tabla_Principal;
        private System.Windows.Forms.ComboBox ComboBox_Filtro;
        private System.Windows.Forms.PictureBox Picture;
        private System.Windows.Forms.ComboBox ComboBox_Zoom;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Pantalla_Completa;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_3;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_GitHub;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Mover_Cursor_Centro;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Mantener_Cursor_Centrado;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Negativo;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_4;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Zoom_Suave;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Seguir_Cursor;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Negativo_Posterior;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Desaturado_Anterior;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Desaturado_Posterior;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Dibujar_Cursor;
    }
}