namespace Minecraft_Tools
{
    partial class Ventana_Visor_Densidades_Bloques
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ventana_Visor_Densidades_Bloques));
            this.DataGridView_Principal = new System.Windows.Forms.DataGridView();
            this.Columna_Icono = new System.Windows.Forms.DataGridViewImageColumn();
            this.Columna_Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Densidad_Porcentaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Densidad_Cada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Mejores_Niveles_Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Niveles_Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Línea_X_Media = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Línea_Y_Media = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Línea_Z_Media = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.Columna_Icono,
            this.Columna_Nombre,
            this.Columna_Total,
            this.Columna_Densidad_Porcentaje,
            this.Columna_Densidad_Cada,
            this.Columna_Mejores_Niveles_Y,
            this.Columna_Niveles_Y,
            this.Columna_Línea_X_Media,
            this.Columna_Línea_Y_Media,
            this.Columna_Línea_Z_Media});
            this.DataGridView_Principal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DataGridView_Principal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridView_Principal.Location = new System.Drawing.Point(0, 0);
            this.DataGridView_Principal.MultiSelect = false;
            this.DataGridView_Principal.Name = "DataGridView_Principal";
            this.DataGridView_Principal.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView_Principal.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridView_Principal.RowHeadersVisible = false;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView_Principal.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.DataGridView_Principal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView_Principal.Size = new System.Drawing.Size(884, 461);
            this.DataGridView_Principal.TabIndex = 0;
            this.DataGridView_Principal.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGridView_Principal_DataError);
            this.DataGridView_Principal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Visor_Densidades_Bloques_KeyDown);
            // 
            // Columna_Icono
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle2.NullValue")));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(2);
            this.Columna_Icono.DefaultCellStyle = dataGridViewCellStyle2;
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
            // Columna_Total
            // 
            this.Columna_Total.HeaderText = "Count";
            this.Columna_Total.Name = "Columna_Total";
            this.Columna_Total.ReadOnly = true;
            // 
            // Columna_Densidad_Porcentaje
            // 
            this.Columna_Densidad_Porcentaje.HeaderText = "Density (%)";
            this.Columna_Densidad_Porcentaje.Name = "Columna_Densidad_Porcentaje";
            this.Columna_Densidad_Porcentaje.ReadOnly = true;
            // 
            // Columna_Densidad_Cada
            // 
            this.Columna_Densidad_Cada.HeaderText = "Density (1 each)";
            this.Columna_Densidad_Cada.Name = "Columna_Densidad_Cada";
            this.Columna_Densidad_Cada.ReadOnly = true;
            // 
            // Columna_Mejores_Niveles_Y
            // 
            this.Columna_Mejores_Niveles_Y.HeaderText = "Best Y levels";
            this.Columna_Mejores_Niveles_Y.Name = "Columna_Mejores_Niveles_Y";
            this.Columna_Mejores_Niveles_Y.ReadOnly = true;
            // 
            // Columna_Niveles_Y
            // 
            this.Columna_Niveles_Y.HeaderText = "Y levels";
            this.Columna_Niveles_Y.Name = "Columna_Niveles_Y";
            this.Columna_Niveles_Y.ReadOnly = true;
            // 
            // Columna_Línea_X_Media
            // 
            this.Columna_Línea_X_Media.HeaderText = "Average X line";
            this.Columna_Línea_X_Media.Name = "Columna_Línea_X_Media";
            this.Columna_Línea_X_Media.ReadOnly = true;
            // 
            // Columna_Línea_Y_Media
            // 
            this.Columna_Línea_Y_Media.HeaderText = "Average Y line";
            this.Columna_Línea_Y_Media.Name = "Columna_Línea_Y_Media";
            this.Columna_Línea_Y_Media.ReadOnly = true;
            // 
            // Columna_Línea_Z_Media
            // 
            this.Columna_Línea_Z_Media.HeaderText = "Average Z line";
            this.Columna_Línea_Z_Media.Name = "Columna_Línea_Z_Media";
            this.Columna_Línea_Z_Media.ReadOnly = true;
            // 
            // Ventana_Visor_Densidades_Bloques
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.Controls.Add(this.DataGridView_Principal);
            this.DoubleBuffered = true;
            this.Name = "Ventana_Visor_Densidades_Bloques";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Block Densities and Y Levels Viewer by Jupisoft";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventana_Visor_Densidades_Bloques_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventana_Visor_Densidades_Bloques_FormClosed);
            this.Load += new System.EventHandler(this.Ventana_Visor_Densidades_Bloques_Load);
            this.Shown += new System.EventHandler(this.Ventana_Visor_Densidades_Bloques_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Visor_Densidades_Bloques_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Principal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGridView_Principal;
        private System.Windows.Forms.DataGridViewImageColumn Columna_Icono;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Densidad_Porcentaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Densidad_Cada;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Mejores_Niveles_Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Niveles_Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Línea_X_Media;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Línea_Y_Media;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Línea_Z_Media;
    }
}