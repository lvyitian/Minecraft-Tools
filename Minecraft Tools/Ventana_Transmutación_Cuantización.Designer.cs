namespace Minecraft_Tools
{
    partial class Ventana_Transmutación_Cuantización
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
            this.textBox23 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Etiqueta_Entrada = new System.Windows.Forms.Label();
            this.Etiqueta_Salida = new System.Windows.Forms.Label();
            this.ComboBox_Entrada = new System.Windows.Forms.ComboBox();
            this.Picture_Entrada = new System.Windows.Forms.PictureBox();
            this.Picture_Salida = new System.Windows.Forms.PictureBox();
            this.ComboBox_Salida = new System.Windows.Forms.ComboBox();
            this.Botón_Cancelar = new System.Windows.Forms.Button();
            this.Botón_Aceptar = new System.Windows.Forms.Button();
            this.Botón_Restablecer = new System.Windows.Forms.Button();
            this.Botón_Aleatorizar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Entrada)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Salida)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox23
            // 
            this.textBox23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox23.BackColor = System.Drawing.Color.White;
            this.textBox23.Enabled = false;
            this.textBox23.Location = new System.Drawing.Point(452, 12);
            this.textBox23.Name = "textBox23";
            this.textBox23.ReadOnly = true;
            this.textBox23.Size = new System.Drawing.Size(20, 20);
            this.textBox23.TabIndex = 8;
            this.textBox23.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(452, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(20, 20);
            this.textBox1.TabIndex = 9;
            this.textBox1.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(452, 64);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(20, 20);
            this.textBox2.TabIndex = 10;
            this.textBox2.Visible = false;
            // 
            // Etiqueta_Entrada
            // 
            this.Etiqueta_Entrada.AutoSize = true;
            this.Etiqueta_Entrada.Location = new System.Drawing.Point(9, 15);
            this.Etiqueta_Entrada.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.Etiqueta_Entrada.Name = "Etiqueta_Entrada";
            this.Etiqueta_Entrada.Size = new System.Drawing.Size(33, 13);
            this.Etiqueta_Entrada.TabIndex = 0;
            this.Etiqueta_Entrada.Text = "From:";
            // 
            // Etiqueta_Salida
            // 
            this.Etiqueta_Salida.AutoSize = true;
            this.Etiqueta_Salida.Location = new System.Drawing.Point(9, 41);
            this.Etiqueta_Salida.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.Etiqueta_Salida.Name = "Etiqueta_Salida";
            this.Etiqueta_Salida.Size = new System.Drawing.Size(23, 13);
            this.Etiqueta_Salida.TabIndex = 2;
            this.Etiqueta_Salida.Text = "To:";
            // 
            // ComboBox_Entrada
            // 
            this.ComboBox_Entrada.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBox_Entrada.BackColor = System.Drawing.Color.White;
            this.ComboBox_Entrada.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Entrada.FormattingEnabled = true;
            this.ComboBox_Entrada.Location = new System.Drawing.Point(74, 11);
            this.ComboBox_Entrada.Name = "ComboBox_Entrada";
            this.ComboBox_Entrada.Size = new System.Drawing.Size(398, 21);
            this.ComboBox_Entrada.TabIndex = 1;
            this.ComboBox_Entrada.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Entrada_SelectedIndexChanged);
            this.ComboBox_Entrada.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Transmutación_Cuantización_KeyDown);
            this.ComboBox_Entrada.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ComboBox_Entrada_MouseDown);
            // 
            // Picture_Entrada
            // 
            this.Picture_Entrada.BackColor = System.Drawing.Color.White;
            this.Picture_Entrada.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Picture_Entrada.InitialImage = null;
            this.Picture_Entrada.Location = new System.Drawing.Point(48, 12);
            this.Picture_Entrada.Name = "Picture_Entrada";
            this.Picture_Entrada.Size = new System.Drawing.Size(20, 20);
            this.Picture_Entrada.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Picture_Entrada.TabIndex = 13;
            this.Picture_Entrada.TabStop = false;
            // 
            // Picture_Salida
            // 
            this.Picture_Salida.BackColor = System.Drawing.Color.White;
            this.Picture_Salida.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Picture_Salida.InitialImage = null;
            this.Picture_Salida.Location = new System.Drawing.Point(48, 38);
            this.Picture_Salida.Name = "Picture_Salida";
            this.Picture_Salida.Size = new System.Drawing.Size(20, 20);
            this.Picture_Salida.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Picture_Salida.TabIndex = 15;
            this.Picture_Salida.TabStop = false;
            // 
            // ComboBox_Salida
            // 
            this.ComboBox_Salida.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBox_Salida.BackColor = System.Drawing.Color.White;
            this.ComboBox_Salida.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Salida.FormattingEnabled = true;
            this.ComboBox_Salida.Location = new System.Drawing.Point(74, 37);
            this.ComboBox_Salida.Name = "ComboBox_Salida";
            this.ComboBox_Salida.Size = new System.Drawing.Size(398, 21);
            this.ComboBox_Salida.TabIndex = 3;
            this.ComboBox_Salida.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Salida_SelectedIndexChanged);
            this.ComboBox_Salida.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Transmutación_Cuantización_KeyDown);
            this.ComboBox_Salida.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ComboBox_Salida_MouseDown);
            // 
            // Botón_Cancelar
            // 
            this.Botón_Cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Botón_Cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Botón_Cancelar.Image = global::Minecraft_Tools.Properties.Resources.Cancelar;
            this.Botón_Cancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Botón_Cancelar.Location = new System.Drawing.Point(372, 62);
            this.Botón_Cancelar.Name = "Botón_Cancelar";
            this.Botón_Cancelar.Size = new System.Drawing.Size(100, 24);
            this.Botón_Cancelar.TabIndex = 7;
            this.Botón_Cancelar.Text = " Cancel ";
            this.Botón_Cancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Botón_Cancelar.UseVisualStyleBackColor = true;
            this.Botón_Cancelar.Click += new System.EventHandler(this.Botón_Cancelar_Click);
            this.Botón_Cancelar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Transmutación_Cuantización_KeyDown);
            // 
            // Botón_Aceptar
            // 
            this.Botón_Aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Botón_Aceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Botón_Aceptar.Image = global::Minecraft_Tools.Properties.Resources.Aceptar;
            this.Botón_Aceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Botón_Aceptar.Location = new System.Drawing.Point(266, 62);
            this.Botón_Aceptar.Name = "Botón_Aceptar";
            this.Botón_Aceptar.Size = new System.Drawing.Size(100, 24);
            this.Botón_Aceptar.TabIndex = 6;
            this.Botón_Aceptar.Text = " Accept ";
            this.Botón_Aceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Botón_Aceptar.UseVisualStyleBackColor = true;
            this.Botón_Aceptar.Click += new System.EventHandler(this.Botón_Aceptar_Click);
            this.Botón_Aceptar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Transmutación_Cuantización_KeyDown);
            // 
            // Botón_Restablecer
            // 
            this.Botón_Restablecer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Botón_Restablecer.Image = global::Minecraft_Tools.Properties.Resources.Restablecer;
            this.Botón_Restablecer.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Botón_Restablecer.Location = new System.Drawing.Point(12, 62);
            this.Botón_Restablecer.Name = "Botón_Restablecer";
            this.Botón_Restablecer.Size = new System.Drawing.Size(100, 24);
            this.Botón_Restablecer.TabIndex = 4;
            this.Botón_Restablecer.Text = " Restore ";
            this.Botón_Restablecer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Botón_Restablecer.UseVisualStyleBackColor = true;
            this.Botón_Restablecer.Click += new System.EventHandler(this.Botón_Restablecer_Click);
            this.Botón_Restablecer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Transmutación_Cuantización_KeyDown);
            // 
            // Botón_Aleatorizar
            // 
            this.Botón_Aleatorizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Botón_Aleatorizar.Image = global::Minecraft_Tools.Properties.Resources.Aleatorio;
            this.Botón_Aleatorizar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Botón_Aleatorizar.Location = new System.Drawing.Point(118, 62);
            this.Botón_Aleatorizar.Name = "Botón_Aleatorizar";
            this.Botón_Aleatorizar.Size = new System.Drawing.Size(100, 24);
            this.Botón_Aleatorizar.TabIndex = 5;
            this.Botón_Aleatorizar.Text = " Randomize ";
            this.Botón_Aleatorizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Botón_Aleatorizar.UseVisualStyleBackColor = true;
            this.Botón_Aleatorizar.Click += new System.EventHandler(this.Botón_Aleatorizar_Click);
            this.Botón_Aleatorizar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Transmutación_Cuantización_KeyDown);
            // 
            // Ventana_Transmutación_Cuantización
            // 
            this.AcceptButton = this.Botón_Aceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Botón_Cancelar;
            this.ClientSize = new System.Drawing.Size(484, 98);
            this.Controls.Add(this.Botón_Aleatorizar);
            this.Controls.Add(this.Botón_Restablecer);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox23);
            this.Controls.Add(this.ComboBox_Salida);
            this.Controls.Add(this.ComboBox_Entrada);
            this.Controls.Add(this.Picture_Salida);
            this.Controls.Add(this.Picture_Entrada);
            this.Controls.Add(this.Etiqueta_Salida);
            this.Controls.Add(this.Etiqueta_Entrada);
            this.Controls.Add(this.Botón_Aceptar);
            this.Controls.Add(this.Botón_Cancelar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ventana_Transmutación_Cuantización";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transmutation by Jupisoft";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventana_Transmutación_Cuantización_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventana_Transmutación_Cuantización_FormClosed);
            this.Load += new System.EventHandler(this.Ventana_Transmutación_Cuantización_Load);
            this.Shown += new System.EventHandler(this.Ventana_Transmutación_Cuantización_Shown);
            this.SizeChanged += new System.EventHandler(this.Ventana_Transmutación_Cuantización_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Transmutación_Cuantización_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Entrada)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Salida)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox23;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label Etiqueta_Entrada;
        private System.Windows.Forms.Label Etiqueta_Salida;
        private System.Windows.Forms.ComboBox ComboBox_Entrada;
        private System.Windows.Forms.PictureBox Picture_Entrada;
        private System.Windows.Forms.PictureBox Picture_Salida;
        private System.Windows.Forms.ComboBox ComboBox_Salida;
        private System.Windows.Forms.Button Botón_Cancelar;
        private System.Windows.Forms.Button Botón_Aceptar;
        private System.Windows.Forms.Button Botón_Restablecer;
        private System.Windows.Forms.Button Botón_Aleatorizar;
    }
}