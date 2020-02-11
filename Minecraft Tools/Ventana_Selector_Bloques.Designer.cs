namespace Minecraft_Tools
{
    partial class Ventana_Selector_Bloques
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DataGridView_Principal = new System.Windows.Forms.DataGridView();
            this.Columna_Seleccionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Columna_Icono = new System.Windows.Forms.DataGridViewImageColumn();
            this.Columna_Bloque = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Icono_Invertido = new System.Windows.Forms.DataGridViewImageColumn();
            this.Columna_Bloque_Invertido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Imagen_Alfa = new System.Windows.Forms.DataGridViewImageColumn();
            this.Columna_Alfa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Imagen_Rojo = new System.Windows.Forms.DataGridViewImageColumn();
            this.Columna_Rojo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Imagen_Verde = new System.Windows.Forms.DataGridViewImageColumn();
            this.Columna_Verde = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Imagen_Azul = new System.Windows.Forms.DataGridViewImageColumn();
            this.Columna_Azul = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Imagen_Código_Hash = new System.Windows.Forms.DataGridViewImageColumn();
            this.Columna_Códgo_Hash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_CRC_32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.Botón_Importar = new System.Windows.Forms.Button();
            this.Botón_Exportar = new System.Windows.Forms.Button();
            this.ComboBox_Paleta = new System.Windows.Forms.ComboBox();
            this.Etiqueta_Paleta = new System.Windows.Forms.Label();
            this.Botón_Aceptar = new System.Windows.Forms.Button();
            this.Botón_Cancelar = new System.Windows.Forms.Button();
            this.Botón_Restablecer = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Principal)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGridView_Principal
            // 
            this.DataGridView_Principal.AllowUserToAddRows = false;
            this.DataGridView_Principal.AllowUserToDeleteRows = false;
            this.DataGridView_Principal.AllowUserToResizeColumns = false;
            this.DataGridView_Principal.AllowUserToResizeRows = false;
            this.DataGridView_Principal.BackgroundColor = System.Drawing.Color.White;
            this.DataGridView_Principal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView_Principal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView_Principal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_Principal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Columna_Seleccionar,
            this.Columna_Icono,
            this.Columna_Bloque,
            this.Columna_Icono_Invertido,
            this.Columna_Bloque_Invertido,
            this.Columna_Imagen_Alfa,
            this.Columna_Alfa,
            this.Columna_Imagen_Rojo,
            this.Columna_Rojo,
            this.Columna_Imagen_Verde,
            this.Columna_Verde,
            this.Columna_Imagen_Azul,
            this.Columna_Azul,
            this.Columna_Imagen_Código_Hash,
            this.Columna_Códgo_Hash,
            this.Columna_Tipo,
            this.Columna_Nombre,
            this.Columna_CRC_32});
            this.DataGridView_Principal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DataGridView_Principal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridView_Principal.Location = new System.Drawing.Point(0, 0);
            this.DataGridView_Principal.Name = "DataGridView_Principal";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView_Principal.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView_Principal.RowHeadersVisible = false;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView_Principal.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridView_Principal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView_Principal.Size = new System.Drawing.Size(959, 544);
            this.DataGridView_Principal.TabIndex = 14;
            this.DataGridView_Principal.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGridView_Principal_DataError);
            // 
            // Columna_Seleccionar
            // 
            this.Columna_Seleccionar.HeaderText = "";
            this.Columna_Seleccionar.Name = "Columna_Seleccionar";
            this.Columna_Seleccionar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Columna_Icono
            // 
            this.Columna_Icono.HeaderText = "";
            this.Columna_Icono.Name = "Columna_Icono";
            this.Columna_Icono.ReadOnly = true;
            this.Columna_Icono.Width = 16;
            // 
            // Columna_Bloque
            // 
            this.Columna_Bloque.HeaderText = "Block name";
            this.Columna_Bloque.Name = "Columna_Bloque";
            this.Columna_Bloque.ReadOnly = true;
            // 
            // Columna_Icono_Invertido
            // 
            this.Columna_Icono_Invertido.HeaderText = "";
            this.Columna_Icono_Invertido.Name = "Columna_Icono_Invertido";
            this.Columna_Icono_Invertido.ReadOnly = true;
            // 
            // Columna_Bloque_Invertido
            // 
            this.Columna_Bloque_Invertido.HeaderText = "Inverted block name";
            this.Columna_Bloque_Invertido.Name = "Columna_Bloque_Invertido";
            this.Columna_Bloque_Invertido.ReadOnly = true;
            // 
            // Columna_Imagen_Alfa
            // 
            this.Columna_Imagen_Alfa.HeaderText = "";
            this.Columna_Imagen_Alfa.Name = "Columna_Imagen_Alfa";
            this.Columna_Imagen_Alfa.ReadOnly = true;
            // 
            // Columna_Alfa
            // 
            this.Columna_Alfa.HeaderText = "Alpha";
            this.Columna_Alfa.Name = "Columna_Alfa";
            this.Columna_Alfa.ReadOnly = true;
            // 
            // Columna_Imagen_Rojo
            // 
            this.Columna_Imagen_Rojo.HeaderText = "";
            this.Columna_Imagen_Rojo.Name = "Columna_Imagen_Rojo";
            this.Columna_Imagen_Rojo.ReadOnly = true;
            // 
            // Columna_Rojo
            // 
            this.Columna_Rojo.HeaderText = "Red";
            this.Columna_Rojo.Name = "Columna_Rojo";
            this.Columna_Rojo.ReadOnly = true;
            // 
            // Columna_Imagen_Verde
            // 
            this.Columna_Imagen_Verde.HeaderText = "";
            this.Columna_Imagen_Verde.Name = "Columna_Imagen_Verde";
            this.Columna_Imagen_Verde.ReadOnly = true;
            // 
            // Columna_Verde
            // 
            this.Columna_Verde.HeaderText = "Green";
            this.Columna_Verde.Name = "Columna_Verde";
            this.Columna_Verde.ReadOnly = true;
            // 
            // Columna_Imagen_Azul
            // 
            this.Columna_Imagen_Azul.HeaderText = "";
            this.Columna_Imagen_Azul.Name = "Columna_Imagen_Azul";
            this.Columna_Imagen_Azul.ReadOnly = true;
            // 
            // Columna_Azul
            // 
            this.Columna_Azul.HeaderText = "Blue";
            this.Columna_Azul.Name = "Columna_Azul";
            this.Columna_Azul.ReadOnly = true;
            // 
            // Columna_Imagen_Código_Hash
            // 
            this.Columna_Imagen_Código_Hash.HeaderText = "";
            this.Columna_Imagen_Código_Hash.Name = "Columna_Imagen_Código_Hash";
            this.Columna_Imagen_Código_Hash.ReadOnly = true;
            // 
            // Columna_Códgo_Hash
            // 
            this.Columna_Códgo_Hash.HeaderText = "Hash code";
            this.Columna_Códgo_Hash.Name = "Columna_Códgo_Hash";
            this.Columna_Códgo_Hash.ReadOnly = true;
            // 
            // Columna_Tipo
            // 
            this.Columna_Tipo.HeaderText = "Type";
            this.Columna_Tipo.Name = "Columna_Tipo";
            this.Columna_Tipo.ReadOnly = true;
            // 
            // Columna_Nombre
            // 
            this.Columna_Nombre.HeaderText = "Name";
            this.Columna_Nombre.Name = "Columna_Nombre";
            this.Columna_Nombre.ReadOnly = true;
            this.Columna_Nombre.Visible = false;
            // 
            // Columna_CRC_32
            // 
            this.Columna_CRC_32.HeaderText = "CRC 32";
            this.Columna_CRC_32.Name = "Columna_CRC_32";
            this.Columna_CRC_32.ReadOnly = true;
            this.Columna_CRC_32.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox9);
            this.panel1.Controls.Add(this.Botón_Importar);
            this.panel1.Controls.Add(this.Botón_Exportar);
            this.panel1.Controls.Add(this.ComboBox_Paleta);
            this.panel1.Controls.Add(this.Etiqueta_Paleta);
            this.panel1.Controls.Add(this.Botón_Aceptar);
            this.panel1.Controls.Add(this.Botón_Cancelar);
            this.panel1.Controls.Add(this.Botón_Restablecer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 544);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(959, 42);
            this.panel1.TabIndex = 15;
            // 
            // textBox9
            // 
            this.textBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox9.BackColor = System.Drawing.Color.White;
            this.textBox9.Enabled = false;
            this.textBox9.Location = new System.Drawing.Point(936, 10);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(20, 20);
            this.textBox9.TabIndex = 16;
            this.textBox9.Visible = false;
            // 
            // Botón_Importar
            // 
            this.Botón_Importar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Botón_Importar.Image = global::Minecraft_Tools.Properties.Resources.Abrir;
            this.Botón_Importar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Botón_Importar.Location = new System.Drawing.Point(604, 6);
            this.Botón_Importar.Name = "Botón_Importar";
            this.Botón_Importar.Size = new System.Drawing.Size(100, 24);
            this.Botón_Importar.TabIndex = 22;
            this.Botón_Importar.Text = " Import... ";
            this.Botón_Importar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Botón_Importar.UseVisualStyleBackColor = true;
            this.Botón_Importar.Click += new System.EventHandler(this.Botón_Importar_Click);
            this.Botón_Importar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Selector_Bloques_KeyDown);
            // 
            // Botón_Exportar
            // 
            this.Botón_Exportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Botón_Exportar.Image = global::Minecraft_Tools.Properties.Resources.Guardar;
            this.Botón_Exportar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Botón_Exportar.Location = new System.Drawing.Point(498, 6);
            this.Botón_Exportar.Name = "Botón_Exportar";
            this.Botón_Exportar.Size = new System.Drawing.Size(100, 24);
            this.Botón_Exportar.TabIndex = 21;
            this.Botón_Exportar.Text = " Export... ";
            this.Botón_Exportar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Botón_Exportar.UseVisualStyleBackColor = true;
            this.Botón_Exportar.Click += new System.EventHandler(this.Botón_Exportar_Click);
            this.Botón_Exportar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Selector_Bloques_KeyDown);
            // 
            // ComboBox_Paleta
            // 
            this.ComboBox_Paleta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBox_Paleta.BackColor = System.Drawing.Color.White;
            this.ComboBox_Paleta.FormattingEnabled = true;
            this.ComboBox_Paleta.Items.AddRange(new object[] {
            "Default",
            "Empty",
            "Full",
            "Survival for Minecraft 1.12.2-",
            "Survival for Minecraft 1.13+",
            "Creative for Minecraft 1.12.2-",
            "Creative for Minecraft 1.13+",
            "Wool",
            "Concrete"});
            this.ComboBox_Paleta.Location = new System.Drawing.Point(167, 10);
            this.ComboBox_Paleta.Name = "ComboBox_Paleta";
            this.ComboBox_Paleta.Size = new System.Drawing.Size(325, 21);
            this.ComboBox_Paleta.TabIndex = 20;
            this.ComboBox_Paleta.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Paleta_SelectedIndexChanged);
            this.ComboBox_Paleta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Selector_Bloques_KeyDown);
            // 
            // Etiqueta_Paleta
            // 
            this.Etiqueta_Paleta.AutoSize = true;
            this.Etiqueta_Paleta.Location = new System.Drawing.Point(118, 13);
            this.Etiqueta_Paleta.Name = "Etiqueta_Paleta";
            this.Etiqueta_Paleta.Size = new System.Drawing.Size(43, 13);
            this.Etiqueta_Paleta.TabIndex = 19;
            this.Etiqueta_Paleta.Text = "Palette:";
            // 
            // Botón_Aceptar
            // 
            this.Botón_Aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Botón_Aceptar.Image = global::Minecraft_Tools.Properties.Resources.Aceptar;
            this.Botón_Aceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Botón_Aceptar.Location = new System.Drawing.Point(741, 6);
            this.Botón_Aceptar.Name = "Botón_Aceptar";
            this.Botón_Aceptar.Size = new System.Drawing.Size(100, 24);
            this.Botón_Aceptar.TabIndex = 17;
            this.Botón_Aceptar.Text = " Accept ";
            this.Botón_Aceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Botón_Aceptar.UseVisualStyleBackColor = true;
            this.Botón_Aceptar.Click += new System.EventHandler(this.Botón_Aceptar_Click);
            this.Botón_Aceptar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Selector_Bloques_KeyDown);
            // 
            // Botón_Cancelar
            // 
            this.Botón_Cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Botón_Cancelar.Image = global::Minecraft_Tools.Properties.Resources.Cancelar;
            this.Botón_Cancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Botón_Cancelar.Location = new System.Drawing.Point(847, 6);
            this.Botón_Cancelar.Name = "Botón_Cancelar";
            this.Botón_Cancelar.Size = new System.Drawing.Size(100, 24);
            this.Botón_Cancelar.TabIndex = 18;
            this.Botón_Cancelar.Text = " Cancel ";
            this.Botón_Cancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Botón_Cancelar.UseVisualStyleBackColor = true;
            this.Botón_Cancelar.Click += new System.EventHandler(this.Botón_Cancelar_Click);
            this.Botón_Cancelar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Selector_Bloques_KeyDown);
            // 
            // Botón_Restablecer
            // 
            this.Botón_Restablecer.Image = global::Minecraft_Tools.Properties.Resources.Restablecer;
            this.Botón_Restablecer.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Botón_Restablecer.Location = new System.Drawing.Point(12, 6);
            this.Botón_Restablecer.Name = "Botón_Restablecer";
            this.Botón_Restablecer.Size = new System.Drawing.Size(100, 24);
            this.Botón_Restablecer.TabIndex = 16;
            this.Botón_Restablecer.Text = " Restore ";
            this.Botón_Restablecer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Botón_Restablecer.UseVisualStyleBackColor = true;
            this.Botón_Restablecer.Click += new System.EventHandler(this.Botón_Restablecer_Click);
            this.Botón_Restablecer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Selector_Bloques_KeyDown);
            // 
            // Ventana_Selector_Bloques
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 586);
            this.Controls.Add(this.DataGridView_Principal);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "Ventana_Selector_Bloques";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Block Selector by Jupisoft";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventana_Selector_Bloques_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventana_Selector_Bloques_FormClosed);
            this.Load += new System.EventHandler(this.Ventana_Selector_Bloques_Load);
            this.Shown += new System.EventHandler(this.Ventana_Selector_Bloques_Shown);
            this.SizeChanged += new System.EventHandler(this.Ventana_Selector_Bloques_SizeChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Ventana_Selector_Bloques_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Ventana_Selector_Bloques_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Selector_Bloques_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Principal)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGridView_Principal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Botón_Restablecer;
        private System.Windows.Forms.Button Botón_Aceptar;
        private System.Windows.Forms.Button Botón_Cancelar;
        private System.Windows.Forms.Label Etiqueta_Paleta;
        private System.Windows.Forms.ComboBox ComboBox_Paleta;
        private System.Windows.Forms.Button Botón_Importar;
        private System.Windows.Forms.Button Botón_Exportar;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Columna_Seleccionar;
        private System.Windows.Forms.DataGridViewImageColumn Columna_Icono;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Bloque;
        private System.Windows.Forms.DataGridViewImageColumn Columna_Icono_Invertido;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Bloque_Invertido;
        private System.Windows.Forms.DataGridViewImageColumn Columna_Imagen_Alfa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Alfa;
        private System.Windows.Forms.DataGridViewImageColumn Columna_Imagen_Rojo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Rojo;
        private System.Windows.Forms.DataGridViewImageColumn Columna_Imagen_Verde;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Verde;
        private System.Windows.Forms.DataGridViewImageColumn Columna_Imagen_Azul;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Azul;
        private System.Windows.Forms.DataGridViewImageColumn Columna_Imagen_Código_Hash;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Códgo_Hash;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_CRC_32;
    }
}