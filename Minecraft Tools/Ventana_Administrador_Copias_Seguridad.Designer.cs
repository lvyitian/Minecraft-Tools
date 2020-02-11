namespace Minecraft_Tools
{
    partial class Ventana_Administrador_Copias_Seguridad
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
            this.Columna_Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Herramienta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_CRC_32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Icono = new System.Windows.Forms.DataGridViewImageColumn();
            this.Columna_Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Ruta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Principal)).BeginInit();
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
            this.Columna_Fecha,
            this.Columna_Herramienta,
            this.Columna_CRC_32,
            this.Columna_Icono,
            this.Columna_Nombre,
            this.Columna_Tipo,
            this.Columna_Ruta});
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
            this.DataGridView_Principal.Size = new System.Drawing.Size(884, 461);
            this.DataGridView_Principal.TabIndex = 15;
            // 
            // Columna_Fecha
            // 
            this.Columna_Fecha.HeaderText = "Date";
            this.Columna_Fecha.Name = "Columna_Fecha";
            this.Columna_Fecha.ReadOnly = true;
            // 
            // Columna_Herramienta
            // 
            this.Columna_Herramienta.HeaderText = "Tool";
            this.Columna_Herramienta.Name = "Columna_Herramienta";
            this.Columna_Herramienta.ReadOnly = true;
            // 
            // Columna_CRC_32
            // 
            this.Columna_CRC_32.HeaderText = "CRC 32";
            this.Columna_CRC_32.Name = "Columna_CRC_32";
            this.Columna_CRC_32.ReadOnly = true;
            // 
            // Columna_Icono
            // 
            this.Columna_Icono.HeaderText = "";
            this.Columna_Icono.Name = "Columna_Icono";
            this.Columna_Icono.ReadOnly = true;
            this.Columna_Icono.Width = 16;
            // 
            // Columna_Nombre
            // 
            this.Columna_Nombre.HeaderText = "Name";
            this.Columna_Nombre.Name = "Columna_Nombre";
            this.Columna_Nombre.ReadOnly = true;
            // 
            // Columna_Tipo
            // 
            this.Columna_Tipo.HeaderText = "Type";
            this.Columna_Tipo.Name = "Columna_Tipo";
            this.Columna_Tipo.ReadOnly = true;
            // 
            // Columna_Ruta
            // 
            this.Columna_Ruta.HeaderText = "Path";
            this.Columna_Ruta.Name = "Columna_Ruta";
            this.Columna_Ruta.ReadOnly = true;
            // 
            // Ventana_Administrador_Copias_Seguridad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.Controls.Add(this.DataGridView_Principal);
            this.DoubleBuffered = true;
            this.Name = "Ventana_Administrador_Copias_Seguridad";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backups Manager by Jupisoft";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventana_Administrador_Copias_Seguridad_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventana_Administrador_Copias_Seguridad_FormClosed);
            this.Load += new System.EventHandler(this.Ventana_Administrador_Copias_Seguridad_Load);
            this.Shown += new System.EventHandler(this.Ventana_Administrador_Copias_Seguridad_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Administrador_Copias_Seguridad_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Principal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGridView_Principal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Herramienta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_CRC_32;
        private System.Windows.Forms.DataGridViewImageColumn Columna_Icono;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Ruta;
    }
}