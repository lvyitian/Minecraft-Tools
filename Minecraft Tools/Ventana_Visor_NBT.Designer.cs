namespace Minecraft_Tools
{
    partial class Ventana_Visor_NBT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ventana_Visor_NBT));
            this.TreeView_NBT = new System.Windows.Forms.TreeView();
            this.Barra_Estado = new System.Windows.Forms.ToolStrip();
            this.Barra_Estado_Botón_Excepción = new System.Windows.Forms.ToolStripButton();
            this.Barra_Estado_Separador_1 = new System.Windows.Forms.ToolStripSeparator();
            this.Barra_Estado_Etiqueta_Memoria = new System.Windows.Forms.ToolStripLabel();
            this.Barra_Estado_Separador_2 = new System.Windows.Forms.ToolStripSeparator();
            this.Barra_Estado_Etiqueta_Sugerencia = new System.Windows.Forms.ToolStripLabel();
            this.Barra_Estado_Separador_3 = new System.Windows.Forms.ToolStripSeparator();
            this.Temporizador_Principal = new System.Windows.Forms.Timer(this.components);
            this.Menú_Contextual = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Menú_Contextual_Visor_Ayuda = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Acerca = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Depurador_Excepciones = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Abrir_Carpeta = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_1 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Actualizar = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Mostrar_Tipos_NBT = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Expandir_Nodo = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Expandir_Nodo_Subnodos = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_2 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Mostrar_Valores = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Mostrar_Valores_Original = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Mostrar_Valores_Bits = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Mostrar_Valores_Bytes = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Mostrar_Valores_Hexadecimal = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Mostrar_Valores_Seleccionados = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Signo = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Sin_Signo = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Separador_1 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Little_Endian = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_3 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Copiar = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Copiar_Valores_Texto = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_4 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Guardar = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Guardar_Valores_Texto = new System.Windows.Forms.ToolStripMenuItem();
            this.RichTextBox_Valor = new System.Windows.Forms.RichTextBox();
            this.Picture_Separador = new System.Windows.Forms.PictureBox();
            this.TextBox_Valor = new System.Windows.Forms.TextBox();
            this.Barra_Estado.SuspendLayout();
            this.Menú_Contextual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Separador)).BeginInit();
            this.SuspendLayout();
            // 
            // TreeView_NBT
            // 
            this.TreeView_NBT.BackColor = System.Drawing.Color.White;
            this.TreeView_NBT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeView_NBT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TreeView_NBT.Dock = System.Windows.Forms.DockStyle.Left;
            this.TreeView_NBT.FullRowSelect = true;
            this.TreeView_NBT.HideSelection = false;
            this.TreeView_NBT.LineColor = System.Drawing.Color.Red;
            this.TreeView_NBT.Location = new System.Drawing.Point(0, 0);
            this.TreeView_NBT.Name = "TreeView_NBT";
            this.TreeView_NBT.Size = new System.Drawing.Size(510, 436);
            this.TreeView_NBT.TabIndex = 0;
            this.TreeView_NBT.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_NBT_AfterSelect);
            this.TreeView_NBT.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NBT_NodeMouseClick);
            this.TreeView_NBT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Visor_NBT_KeyDown);
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
            this.Barra_Estado_Etiqueta_Sugerencia.Size = new System.Drawing.Size(1517, 16);
            this.Barra_Estado_Etiqueta_Sugerencia.Text = resources.GetString("Barra_Estado_Etiqueta_Sugerencia.Text");
            this.Barra_Estado_Etiqueta_Sugerencia.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Barra_Estado_Etiqueta_Sugerencia_MouseDown);
            // 
            // Barra_Estado_Separador_3
            // 
            this.Barra_Estado_Separador_3.Name = "Barra_Estado_Separador_3";
            this.Barra_Estado_Separador_3.Size = new System.Drawing.Size(6, 25);
            // 
            // Temporizador_Principal
            // 
            this.Temporizador_Principal.Interval = 1;
            this.Temporizador_Principal.Tick += new System.EventHandler(this.Temporizador_Principal_Tick);
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
            this.Menú_Contextual_Mostrar_Tipos_NBT,
            this.Menú_Contextual_Expandir_Nodo,
            this.Menú_Contextual_Expandir_Nodo_Subnodos,
            this.Menú_Contextual_Separador_2,
            this.Menú_Contextual_Mostrar_Valores,
            this.Menú_Contextual_Mostrar_Valores_Seleccionados,
            this.Menú_Contextual_Separador_3,
            this.Menú_Contextual_Copiar,
            this.Menú_Contextual_Copiar_Valores_Texto,
            this.Menú_Contextual_Separador_4,
            this.Menú_Contextual_Guardar,
            this.Menú_Contextual_Guardar_Valores_Texto});
            this.Menú_Contextual.Name = "Menú_Contextual";
            this.Menú_Contextual.Size = new System.Drawing.Size(411, 336);
            // 
            // Menú_Contextual_Visor_Ayuda
            // 
            this.Menú_Contextual_Visor_Ayuda.Image = global::Minecraft_Tools.Properties.Resources.Ayuda;
            this.Menú_Contextual_Visor_Ayuda.Name = "Menú_Contextual_Visor_Ayuda";
            this.Menú_Contextual_Visor_Ayuda.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.Menú_Contextual_Visor_Ayuda.Size = new System.Drawing.Size(410, 22);
            this.Menú_Contextual_Visor_Ayuda.Text = "Help viewer...";
            this.Menú_Contextual_Visor_Ayuda.Click += new System.EventHandler(this.Menú_Contextual_Visor_Ayuda_Click);
            // 
            // Menú_Contextual_Acerca
            // 
            this.Menú_Contextual_Acerca.Image = global::Minecraft_Tools.Properties.Resources.Jupisoft_16;
            this.Menú_Contextual_Acerca.Name = "Menú_Contextual_Acerca";
            this.Menú_Contextual_Acerca.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.Menú_Contextual_Acerca.Size = new System.Drawing.Size(410, 22);
            this.Menú_Contextual_Acerca.Text = "About...";
            this.Menú_Contextual_Acerca.Click += new System.EventHandler(this.Menú_Contextual_Acerca_Click);
            // 
            // Menú_Contextual_Depurador_Excepciones
            // 
            this.Menú_Contextual_Depurador_Excepciones.Image = global::Minecraft_Tools.Properties.Resources.Excepción;
            this.Menú_Contextual_Depurador_Excepciones.Name = "Menú_Contextual_Depurador_Excepciones";
            this.Menú_Contextual_Depurador_Excepciones.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.Menú_Contextual_Depurador_Excepciones.Size = new System.Drawing.Size(410, 22);
            this.Menú_Contextual_Depurador_Excepciones.Text = "Exception debugger...";
            this.Menú_Contextual_Depurador_Excepciones.Click += new System.EventHandler(this.Menú_Contextual_Depurador_Excepciones_Click);
            // 
            // Menú_Contextual_Abrir_Carpeta
            // 
            this.Menú_Contextual_Abrir_Carpeta.Image = global::Minecraft_Tools.Properties.Resources.Ejecutar;
            this.Menú_Contextual_Abrir_Carpeta.Name = "Menú_Contextual_Abrir_Carpeta";
            this.Menú_Contextual_Abrir_Carpeta.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.Menú_Contextual_Abrir_Carpeta.Size = new System.Drawing.Size(410, 22);
            this.Menú_Contextual_Abrir_Carpeta.Text = "Open the default save folder...";
            this.Menú_Contextual_Abrir_Carpeta.Click += new System.EventHandler(this.Menú_Contextual_Abrir_Carpeta_Click);
            // 
            // Menú_Contextual_Separador_1
            // 
            this.Menú_Contextual_Separador_1.Name = "Menú_Contextual_Separador_1";
            this.Menú_Contextual_Separador_1.Size = new System.Drawing.Size(407, 6);
            // 
            // Menú_Contextual_Actualizar
            // 
            this.Menú_Contextual_Actualizar.Image = global::Minecraft_Tools.Properties.Resources.Actualizar;
            this.Menú_Contextual_Actualizar.Name = "Menú_Contextual_Actualizar";
            this.Menú_Contextual_Actualizar.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.Menú_Contextual_Actualizar.Size = new System.Drawing.Size(410, 22);
            this.Menú_Contextual_Actualizar.Text = "Refresh";
            this.Menú_Contextual_Actualizar.Click += new System.EventHandler(this.Menú_Contextual_Actualizar_Click);
            // 
            // Menú_Contextual_Mostrar_Tipos_NBT
            // 
            this.Menú_Contextual_Mostrar_Tipos_NBT.Checked = true;
            this.Menú_Contextual_Mostrar_Tipos_NBT.CheckOnClick = true;
            this.Menú_Contextual_Mostrar_Tipos_NBT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menú_Contextual_Mostrar_Tipos_NBT.Name = "Menú_Contextual_Mostrar_Tipos_NBT";
            this.Menú_Contextual_Mostrar_Tipos_NBT.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.Menú_Contextual_Mostrar_Tipos_NBT.Size = new System.Drawing.Size(410, 22);
            this.Menú_Contextual_Mostrar_Tipos_NBT.Text = "Show the NBT types and compressions used";
            this.Menú_Contextual_Mostrar_Tipos_NBT.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Mostrar_Tipos_NBT_CheckedChanged);
            // 
            // Menú_Contextual_Expandir_Nodo
            // 
            this.Menú_Contextual_Expandir_Nodo.Image = global::Minecraft_Tools.Properties.Resources.Abajo;
            this.Menú_Contextual_Expandir_Nodo.Name = "Menú_Contextual_Expandir_Nodo";
            this.Menú_Contextual_Expandir_Nodo.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.Menú_Contextual_Expandir_Nodo.Size = new System.Drawing.Size(410, 22);
            this.Menú_Contextual_Expandir_Nodo.Text = "Expand only the selected node";
            this.Menú_Contextual_Expandir_Nodo.Click += new System.EventHandler(this.Menú_Contextual_Expandir_Nodo_Click);
            // 
            // Menú_Contextual_Expandir_Nodo_Subnodos
            // 
            this.Menú_Contextual_Expandir_Nodo_Subnodos.Image = global::Minecraft_Tools.Properties.Resources.Lista;
            this.Menú_Contextual_Expandir_Nodo_Subnodos.Name = "Menú_Contextual_Expandir_Nodo_Subnodos";
            this.Menú_Contextual_Expandir_Nodo_Subnodos.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.Menú_Contextual_Expandir_Nodo_Subnodos.Size = new System.Drawing.Size(410, 22);
            this.Menú_Contextual_Expandir_Nodo_Subnodos.Text = "Expand the selected node and all of it\'s subnodes";
            this.Menú_Contextual_Expandir_Nodo_Subnodos.Click += new System.EventHandler(this.Menú_Contextual_Expandir_Nodo_Subnodos_Click);
            // 
            // Menú_Contextual_Separador_2
            // 
            this.Menú_Contextual_Separador_2.Name = "Menú_Contextual_Separador_2";
            this.Menú_Contextual_Separador_2.Size = new System.Drawing.Size(407, 6);
            // 
            // Menú_Contextual_Mostrar_Valores
            // 
            this.Menú_Contextual_Mostrar_Valores.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menú_Contextual_Mostrar_Valores_Original,
            this.Menú_Contextual_Mostrar_Valores_Bits,
            this.Menú_Contextual_Mostrar_Valores_Bytes,
            this.Menú_Contextual_Mostrar_Valores_Hexadecimal});
            this.Menú_Contextual_Mostrar_Valores.Image = global::Minecraft_Tools.Properties.Resources.Controles_TextBox;
            this.Menú_Contextual_Mostrar_Valores.Name = "Menú_Contextual_Mostrar_Valores";
            this.Menú_Contextual_Mostrar_Valores.Size = new System.Drawing.Size(410, 22);
            this.Menú_Contextual_Mostrar_Valores.Text = "Show the NBT (Named Binary Tag) values as";
            // 
            // Menú_Contextual_Mostrar_Valores_Original
            // 
            this.Menú_Contextual_Mostrar_Valores_Original.Checked = true;
            this.Menú_Contextual_Mostrar_Valores_Original.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menú_Contextual_Mostrar_Valores_Original.Enabled = false;
            this.Menú_Contextual_Mostrar_Valores_Original.Name = "Menú_Contextual_Mostrar_Valores_Original";
            this.Menú_Contextual_Mostrar_Valores_Original.Size = new System.Drawing.Size(217, 22);
            this.Menú_Contextual_Mostrar_Valores_Original.Text = "Original";
            // 
            // Menú_Contextual_Mostrar_Valores_Bits
            // 
            this.Menú_Contextual_Mostrar_Valores_Bits.Enabled = false;
            this.Menú_Contextual_Mostrar_Valores_Bits.Name = "Menú_Contextual_Mostrar_Valores_Bits";
            this.Menú_Contextual_Mostrar_Valores_Bits.Size = new System.Drawing.Size(217, 22);
            this.Menú_Contextual_Mostrar_Valores_Bits.Text = "Bits (00000000 to 11111111)";
            // 
            // Menú_Contextual_Mostrar_Valores_Bytes
            // 
            this.Menú_Contextual_Mostrar_Valores_Bytes.Enabled = false;
            this.Menú_Contextual_Mostrar_Valores_Bytes.Name = "Menú_Contextual_Mostrar_Valores_Bytes";
            this.Menú_Contextual_Mostrar_Valores_Bytes.Size = new System.Drawing.Size(217, 22);
            this.Menú_Contextual_Mostrar_Valores_Bytes.Text = "Bytes (0 to 255)";
            // 
            // Menú_Contextual_Mostrar_Valores_Hexadecimal
            // 
            this.Menú_Contextual_Mostrar_Valores_Hexadecimal.Enabled = false;
            this.Menú_Contextual_Mostrar_Valores_Hexadecimal.Name = "Menú_Contextual_Mostrar_Valores_Hexadecimal";
            this.Menú_Contextual_Mostrar_Valores_Hexadecimal.Size = new System.Drawing.Size(217, 22);
            this.Menú_Contextual_Mostrar_Valores_Hexadecimal.Text = "Hexadecimal (00 to FF)";
            // 
            // Menú_Contextual_Mostrar_Valores_Seleccionados
            // 
            this.Menú_Contextual_Mostrar_Valores_Seleccionados.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Signo,
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Sin_Signo,
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Separador_1,
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Little_Endian});
            this.Menú_Contextual_Mostrar_Valores_Seleccionados.Image = global::Minecraft_Tools.Properties.Resources.Seleccionar_Todo;
            this.Menú_Contextual_Mostrar_Valores_Seleccionados.Name = "Menú_Contextual_Mostrar_Valores_Seleccionados";
            this.Menú_Contextual_Mostrar_Valores_Seleccionados.Size = new System.Drawing.Size(410, 22);
            this.Menú_Contextual_Mostrar_Valores_Seleccionados.Text = "Show the selected NBT values as";
            // 
            // Menú_Contextual_Mostrar_Valores_Seleccionados_Signo
            // 
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Signo.Checked = true;
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Signo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Signo.Enabled = false;
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Signo.Name = "Menú_Contextual_Mostrar_Valores_Seleccionados_Signo";
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Signo.Size = new System.Drawing.Size(236, 22);
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Signo.Text = "Signed values of infinite bits";
            // 
            // Menú_Contextual_Mostrar_Valores_Seleccionados_Sin_Signo
            // 
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Sin_Signo.Enabled = false;
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Sin_Signo.Name = "Menú_Contextual_Mostrar_Valores_Seleccionados_Sin_Signo";
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Sin_Signo.Size = new System.Drawing.Size(236, 22);
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Sin_Signo.Text = "Unsigned values of infinite bits";
            // 
            // Menú_Contextual_Mostrar_Valores_Seleccionados_Separador_1
            // 
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Separador_1.Name = "Menú_Contextual_Mostrar_Valores_Seleccionados_Separador_1";
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Separador_1.Size = new System.Drawing.Size(233, 6);
            // 
            // Menú_Contextual_Mostrar_Valores_Seleccionados_Little_Endian
            // 
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Little_Endian.Checked = true;
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Little_Endian.CheckOnClick = true;
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Little_Endian.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Little_Endian.Enabled = false;
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Little_Endian.Name = "Menú_Contextual_Mostrar_Valores_Seleccionados_Little_Endian";
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Little_Endian.Size = new System.Drawing.Size(236, 22);
            this.Menú_Contextual_Mostrar_Valores_Seleccionados_Little_Endian.Text = "Use little endian as byte order";
            // 
            // Menú_Contextual_Separador_3
            // 
            this.Menú_Contextual_Separador_3.Name = "Menú_Contextual_Separador_3";
            this.Menú_Contextual_Separador_3.Size = new System.Drawing.Size(407, 6);
            // 
            // Menú_Contextual_Copiar
            // 
            this.Menú_Contextual_Copiar.Image = global::Minecraft_Tools.Properties.Resources.Copiar;
            this.Menú_Contextual_Copiar.Name = "Menú_Contextual_Copiar";
            this.Menú_Contextual_Copiar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.Menú_Contextual_Copiar.Size = new System.Drawing.Size(410, 22);
            this.Menú_Contextual_Copiar.Text = "Copy the value of the selected node";
            this.Menú_Contextual_Copiar.Click += new System.EventHandler(this.Menú_Contextual_Copiar_Click);
            // 
            // Menú_Contextual_Copiar_Valores_Texto
            // 
            this.Menú_Contextual_Copiar_Valores_Texto.Image = global::Minecraft_Tools.Properties.Resources.Copiar;
            this.Menú_Contextual_Copiar_Valores_Texto.Name = "Menú_Contextual_Copiar_Valores_Texto";
            this.Menú_Contextual_Copiar_Valores_Texto.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.Menú_Contextual_Copiar_Valores_Texto.Size = new System.Drawing.Size(410, 22);
            this.Menú_Contextual_Copiar_Valores_Texto.Text = "Copy the sorted values of all the strings as text";
            this.Menú_Contextual_Copiar_Valores_Texto.Click += new System.EventHandler(this.Menú_Contextual_Copiar_Valores_Texto_Click);
            // 
            // Menú_Contextual_Separador_4
            // 
            this.Menú_Contextual_Separador_4.Name = "Menú_Contextual_Separador_4";
            this.Menú_Contextual_Separador_4.Size = new System.Drawing.Size(407, 6);
            // 
            // Menú_Contextual_Guardar
            // 
            this.Menú_Contextual_Guardar.Image = global::Minecraft_Tools.Properties.Resources.Guardar;
            this.Menú_Contextual_Guardar.Name = "Menú_Contextual_Guardar";
            this.Menú_Contextual_Guardar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.Menú_Contextual_Guardar.Size = new System.Drawing.Size(410, 22);
            this.Menú_Contextual_Guardar.Text = "Save the values of all the nodes (no need to expand first)";
            this.Menú_Contextual_Guardar.Click += new System.EventHandler(this.Menú_Contextual_Guardar_Click);
            // 
            // Menú_Contextual_Guardar_Valores_Texto
            // 
            this.Menú_Contextual_Guardar_Valores_Texto.Image = global::Minecraft_Tools.Properties.Resources.Guardar;
            this.Menú_Contextual_Guardar_Valores_Texto.Name = "Menú_Contextual_Guardar_Valores_Texto";
            this.Menú_Contextual_Guardar_Valores_Texto.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.Menú_Contextual_Guardar_Valores_Texto.Size = new System.Drawing.Size(410, 22);
            this.Menú_Contextual_Guardar_Valores_Texto.Text = "Save the sorted values of all the strings as text";
            this.Menú_Contextual_Guardar_Valores_Texto.Click += new System.EventHandler(this.Menú_Contextual_Guardar_Valores_Texto_Click);
            // 
            // RichTextBox_Valor
            // 
            this.RichTextBox_Valor.BackColor = System.Drawing.Color.White;
            this.RichTextBox_Valor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichTextBox_Valor.ContextMenuStrip = this.Menú_Contextual;
            this.RichTextBox_Valor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RichTextBox_Valor.Enabled = false;
            this.RichTextBox_Valor.Location = new System.Drawing.Point(511, 0);
            this.RichTextBox_Valor.Name = "RichTextBox_Valor";
            this.RichTextBox_Valor.ReadOnly = true;
            this.RichTextBox_Valor.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RichTextBox_Valor.ShowSelectionMargin = true;
            this.RichTextBox_Valor.Size = new System.Drawing.Size(373, 436);
            this.RichTextBox_Valor.TabIndex = 3;
            this.RichTextBox_Valor.Text = "";
            this.RichTextBox_Valor.Visible = false;
            this.RichTextBox_Valor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Visor_NBT_KeyDown);
            // 
            // Picture_Separador
            // 
            this.Picture_Separador.BackColor = System.Drawing.SystemColors.Control;
            this.Picture_Separador.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Picture_Separador.Dock = System.Windows.Forms.DockStyle.Left;
            this.Picture_Separador.Enabled = false;
            this.Picture_Separador.InitialImage = null;
            this.Picture_Separador.Location = new System.Drawing.Point(510, 0);
            this.Picture_Separador.Name = "Picture_Separador";
            this.Picture_Separador.Size = new System.Drawing.Size(1, 436);
            this.Picture_Separador.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Picture_Separador.TabIndex = 3;
            this.Picture_Separador.TabStop = false;
            // 
            // TextBox_Valor
            // 
            this.TextBox_Valor.BackColor = System.Drawing.Color.White;
            this.TextBox_Valor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBox_Valor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox_Valor.Location = new System.Drawing.Point(511, 0);
            this.TextBox_Valor.Multiline = true;
            this.TextBox_Valor.Name = "TextBox_Valor";
            this.TextBox_Valor.ReadOnly = true;
            this.TextBox_Valor.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox_Valor.Size = new System.Drawing.Size(373, 436);
            this.TextBox_Valor.TabIndex = 1;
            this.TextBox_Valor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Visor_NBT_KeyDown);
            // 
            // Ventana_Visor_NBT
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.ContextMenuStrip = this.Menú_Contextual;
            this.Controls.Add(this.TextBox_Valor);
            this.Controls.Add(this.RichTextBox_Valor);
            this.Controls.Add(this.Picture_Separador);
            this.Controls.Add(this.TreeView_NBT);
            this.Controls.Add(this.Barra_Estado);
            this.DoubleBuffered = true;
            this.Name = "Ventana_Visor_NBT";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NBT Viewer by Jupisoft - [Drag and drop any NBT file to open it as read-only...]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventana_Visor_NBT_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventana_Visor_NBT_FormClosed);
            this.Load += new System.EventHandler(this.Ventana_Visor_NBT_Load);
            this.Shown += new System.EventHandler(this.Ventana_Visor_NBT_Shown);
            this.SizeChanged += new System.EventHandler(this.Ventana_Visor_NBT_SizeChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Ventana_Visor_NBT_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Ventana_Visor_NBT_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Visor_NBT_KeyDown);
            this.Barra_Estado.ResumeLayout(false);
            this.Barra_Estado.PerformLayout();
            this.Menú_Contextual.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Separador)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView TreeView_NBT;
        private System.Windows.Forms.ToolStrip Barra_Estado;
        private System.Windows.Forms.ToolStripLabel Barra_Estado_Etiqueta_Memoria;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_1;
        private System.Windows.Forms.ToolStripLabel Barra_Estado_Etiqueta_Sugerencia;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_2;
        private System.Windows.Forms.Timer Temporizador_Principal;
        private System.Windows.Forms.ContextMenuStrip Menú_Contextual;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Visor_Ayuda;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Acerca;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Depurador_Excepciones;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Abrir_Carpeta;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_1;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Actualizar;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_2;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Copiar;
        private System.Windows.Forms.ToolStripButton Barra_Estado_Botón_Excepción;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_3;
        private System.Windows.Forms.RichTextBox RichTextBox_Valor;
        private System.Windows.Forms.PictureBox Picture_Separador;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Mostrar_Tipos_NBT;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Mostrar_Valores;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Mostrar_Valores_Original;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Mostrar_Valores_Bits;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Mostrar_Valores_Bytes;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Mostrar_Valores_Hexadecimal;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Mostrar_Valores_Seleccionados;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Mostrar_Valores_Seleccionados_Signo;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Mostrar_Valores_Seleccionados_Sin_Signo;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Mostrar_Valores_Seleccionados_Little_Endian;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Mostrar_Valores_Seleccionados_Separador_1;
        private System.Windows.Forms.TextBox TextBox_Valor;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_3;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Expandir_Nodo;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Expandir_Nodo_Subnodos;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Guardar;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_4;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Copiar_Valores_Texto;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Guardar_Valores_Texto;
    }
}