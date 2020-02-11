namespace Minecraft_Tools
{
    partial class Ventana_Gracias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ventana_Gracias));
            this.DataGridView_Principal = new System.Windows.Forms.DataGridView();
            this.Columna_Icono = new System.Windows.Forms.DataGridViewImageColumn();
            this.Columna_Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_URL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Origen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_URL_Origen = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.Columna_URL,
            this.Columna_Fecha,
            this.Columna_Origen,
            this.Columna_URL_Origen});
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
            this.DataGridView_Principal.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_Principal_CellMouseDown);
            this.DataGridView_Principal.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGridView_Principal_DataError);
            this.DataGridView_Principal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Gracias_KeyDown);
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
            // Columna_URL
            // 
            this.Columna_URL.HeaderText = "URL";
            this.Columna_URL.Name = "Columna_URL";
            this.Columna_URL.ReadOnly = true;
            // 
            // Columna_Fecha
            // 
            this.Columna_Fecha.HeaderText = "Contact date";
            this.Columna_Fecha.Name = "Columna_Fecha";
            this.Columna_Fecha.ReadOnly = true;
            // 
            // Columna_Origen
            // 
            this.Columna_Origen.HeaderText = "Contact origin";
            this.Columna_Origen.Name = "Columna_Origen";
            this.Columna_Origen.ReadOnly = true;
            // 
            // Columna_URL_Origen
            // 
            this.Columna_URL_Origen.HeaderText = "Contact origin URL";
            this.Columna_URL_Origen.Name = "Columna_URL_Origen";
            this.Columna_URL_Origen.ReadOnly = true;
            // 
            // Ventana_Gracias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.Controls.Add(this.DataGridView_Principal);
            this.DoubleBuffered = true;
            this.Name = "Ventana_Gracias";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thank You by Jupisoft";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventana_Gracias_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventana_Gracias_FormClosed);
            this.Load += new System.EventHandler(this.Ventana_Gracias_Load);
            this.Shown += new System.EventHandler(this.Ventana_Gracias_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Gracias_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Principal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGridView_Principal;
        private System.Windows.Forms.DataGridViewImageColumn Columna_Icono;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_URL;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Origen;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_URL_Origen;
    }
}