namespace Minecraft_Tools
{
    partial class Ventana_Calculadora_Infinita_Semillas_Mundos
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
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.Etiqueta_Texto = new System.Windows.Forms.Label();
            this.TextBox_Semilla = new System.Windows.Forms.TextBox();
            this.Etiqueta_Semilla = new System.Windows.Forms.Label();
            this.Etiqueta_Base = new System.Windows.Forms.Label();
            this.Numérico_Base = new System.Windows.Forms.NumericUpDown();
            this.Etiqueta_Base_Nota = new System.Windows.Forms.Label();
            this.TextBox_Resultados = new System.Windows.Forms.TextBox();
            this.Botón_Copiar_Texto = new System.Windows.Forms.Button();
            this.Botón_Copiar_Semilla = new System.Windows.Forms.Button();
            this.Etiqueta_Cálculos = new System.Windows.Forms.Label();
            this.Picture_Base = new System.Windows.Forms.PictureBox();
            this.ComboBox_Texto = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.Numérico_Base)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Base)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox9
            // 
            this.textBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox9.BackColor = System.Drawing.Color.White;
            this.textBox9.Enabled = false;
            this.textBox9.Location = new System.Drawing.Point(852, 12);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(20, 20);
            this.textBox9.TabIndex = 11;
            this.textBox9.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(852, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(20, 20);
            this.textBox1.TabIndex = 12;
            this.textBox1.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(852, 64);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(20, 20);
            this.textBox2.TabIndex = 13;
            this.textBox2.Visible = false;
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.BackColor = System.Drawing.Color.White;
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(852, 90);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(20, 20);
            this.textBox3.TabIndex = 14;
            this.textBox3.Visible = false;
            // 
            // Etiqueta_Texto
            // 
            this.Etiqueta_Texto.AutoSize = true;
            this.Etiqueta_Texto.Location = new System.Drawing.Point(9, 15);
            this.Etiqueta_Texto.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.Etiqueta_Texto.Name = "Etiqueta_Texto";
            this.Etiqueta_Texto.Size = new System.Drawing.Size(31, 13);
            this.Etiqueta_Texto.TabIndex = 0;
            this.Etiqueta_Texto.Text = "Text:";
            // 
            // TextBox_Semilla
            // 
            this.TextBox_Semilla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_Semilla.BackColor = System.Drawing.Color.White;
            this.TextBox_Semilla.Location = new System.Drawing.Point(55, 38);
            this.TextBox_Semilla.Name = "TextBox_Semilla";
            this.TextBox_Semilla.ReadOnly = true;
            this.TextBox_Semilla.Size = new System.Drawing.Size(787, 20);
            this.TextBox_Semilla.TabIndex = 4;
            this.TextBox_Semilla.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Calculadora_Infinita_Semillas_Mundos_KeyDown);
            // 
            // Etiqueta_Semilla
            // 
            this.Etiqueta_Semilla.AutoSize = true;
            this.Etiqueta_Semilla.Location = new System.Drawing.Point(9, 41);
            this.Etiqueta_Semilla.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.Etiqueta_Semilla.Name = "Etiqueta_Semilla";
            this.Etiqueta_Semilla.Size = new System.Drawing.Size(35, 13);
            this.Etiqueta_Semilla.TabIndex = 3;
            this.Etiqueta_Semilla.Text = "Seed:";
            // 
            // Etiqueta_Base
            // 
            this.Etiqueta_Base.AutoSize = true;
            this.Etiqueta_Base.Location = new System.Drawing.Point(9, 67);
            this.Etiqueta_Base.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.Etiqueta_Base.Name = "Etiqueta_Base";
            this.Etiqueta_Base.Size = new System.Drawing.Size(34, 13);
            this.Etiqueta_Base.TabIndex = 6;
            this.Etiqueta_Base.Text = "Base:";
            // 
            // Numérico_Base
            // 
            this.Numérico_Base.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Numérico_Base.BackColor = System.Drawing.Color.White;
            this.Numérico_Base.Location = new System.Drawing.Point(55, 64);
            this.Numérico_Base.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.Numérico_Base.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.Numérico_Base.Name = "Numérico_Base";
            this.Numérico_Base.Size = new System.Drawing.Size(270, 20);
            this.Numérico_Base.TabIndex = 7;
            this.Numérico_Base.ThousandsSeparator = true;
            this.Numérico_Base.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.Numérico_Base.ValueChanged += new System.EventHandler(this.Numérico_Base_ValueChanged);
            this.Numérico_Base.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Calculadora_Infinita_Semillas_Mundos_KeyDown);
            this.Numérico_Base.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Numérico_Base_MouseDown);
            // 
            // Etiqueta_Base_Nota
            // 
            this.Etiqueta_Base_Nota.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Etiqueta_Base_Nota.AutoSize = true;
            this.Etiqueta_Base_Nota.Location = new System.Drawing.Point(354, 67);
            this.Etiqueta_Base_Nota.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.Etiqueta_Base_Nota.Name = "Etiqueta_Base_Nota";
            this.Etiqueta_Base_Nota.Size = new System.Drawing.Size(518, 13);
            this.Etiqueta_Base_Nota.TabIndex = 8;
            this.Etiqueta_Base_Nota.Text = "Note: Minecraft only uses the base 31 when it calculates seeds, and the resulting" +
    " seed will only have 32 bits.";
            // 
            // TextBox_Resultados
            // 
            this.TextBox_Resultados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_Resultados.BackColor = System.Drawing.Color.White;
            this.TextBox_Resultados.Location = new System.Drawing.Point(55, 90);
            this.TextBox_Resultados.Multiline = true;
            this.TextBox_Resultados.Name = "TextBox_Resultados";
            this.TextBox_Resultados.ReadOnly = true;
            this.TextBox_Resultados.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextBox_Resultados.Size = new System.Drawing.Size(817, 359);
            this.TextBox_Resultados.TabIndex = 10;
            this.TextBox_Resultados.WordWrap = false;
            this.TextBox_Resultados.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Calculadora_Infinita_Semillas_Mundos_KeyDown);
            // 
            // Botón_Copiar_Texto
            // 
            this.Botón_Copiar_Texto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Botón_Copiar_Texto.Image = global::Minecraft_Tools.Properties.Resources.Copiar;
            this.Botón_Copiar_Texto.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Botón_Copiar_Texto.Location = new System.Drawing.Point(848, 10);
            this.Botón_Copiar_Texto.Name = "Botón_Copiar_Texto";
            this.Botón_Copiar_Texto.Size = new System.Drawing.Size(24, 24);
            this.Botón_Copiar_Texto.TabIndex = 2;
            this.Botón_Copiar_Texto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Botón_Copiar_Texto.UseVisualStyleBackColor = true;
            this.Botón_Copiar_Texto.Click += new System.EventHandler(this.Botón_Copiar_Texto_Click);
            this.Botón_Copiar_Texto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Calculadora_Infinita_Semillas_Mundos_KeyDown);
            // 
            // Botón_Copiar_Semilla
            // 
            this.Botón_Copiar_Semilla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Botón_Copiar_Semilla.Image = global::Minecraft_Tools.Properties.Resources.Copiar;
            this.Botón_Copiar_Semilla.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Botón_Copiar_Semilla.Location = new System.Drawing.Point(848, 36);
            this.Botón_Copiar_Semilla.Name = "Botón_Copiar_Semilla";
            this.Botón_Copiar_Semilla.Size = new System.Drawing.Size(24, 24);
            this.Botón_Copiar_Semilla.TabIndex = 5;
            this.Botón_Copiar_Semilla.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Botón_Copiar_Semilla.UseVisualStyleBackColor = true;
            this.Botón_Copiar_Semilla.Click += new System.EventHandler(this.Botón_Copiar_Semilla_Click);
            this.Botón_Copiar_Semilla.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Calculadora_Infinita_Semillas_Mundos_KeyDown);
            // 
            // Etiqueta_Cálculos
            // 
            this.Etiqueta_Cálculos.AutoSize = true;
            this.Etiqueta_Cálculos.Location = new System.Drawing.Point(9, 93);
            this.Etiqueta_Cálculos.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.Etiqueta_Cálculos.Name = "Etiqueta_Cálculos";
            this.Etiqueta_Cálculos.Size = new System.Drawing.Size(40, 13);
            this.Etiqueta_Cálculos.TabIndex = 9;
            this.Etiqueta_Cálculos.Text = "Result:";
            // 
            // Picture_Base
            // 
            this.Picture_Base.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Picture_Base.BackColor = System.Drawing.Color.White;
            this.Picture_Base.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Picture_Base.Image = global::Minecraft_Tools.Properties.Resources.Minecraft;
            this.Picture_Base.InitialImage = null;
            this.Picture_Base.Location = new System.Drawing.Point(331, 64);
            this.Picture_Base.Name = "Picture_Base";
            this.Picture_Base.Size = new System.Drawing.Size(20, 20);
            this.Picture_Base.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Picture_Base.TabIndex = 30;
            this.Picture_Base.TabStop = false;
            // 
            // ComboBox_Texto
            // 
            this.ComboBox_Texto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBox_Texto.BackColor = System.Drawing.Color.White;
            this.ComboBox_Texto.FormattingEnabled = true;
            this.ComboBox_Texto.Items.AddRange(new object[] {
            "A person who never yodelled an apology, never preened vocalizing transsexuals.",
            "amusement & hemophilias",
            "bequirtle zorillo",
            "BLEACHINGFEMININELY.NET",
            "chronogrammic schtoff",
            "constitutionalunstableness.net",
            "contusive cloisterlike",
            "creashaks organzine",
            "drumwood boulderhead",
            "electroanalytic exercisable",
            "electrolysissweeteners.net",
            "favosely nonconstruable",
            "grinnerslaphappier.org",
            "Incentively, my dear, I don\'t tessellate a derangement.",
            "Microcomputers: the unredeemed lollipop...",
            "pollinating sandboxes",
            "schoolworks = perversive",
            "WWW.BUMRACEGOERS.ORG",
            "WWW.RACCOONPRUDENTIALS.NET"});
            this.ComboBox_Texto.Location = new System.Drawing.Point(55, 11);
            this.ComboBox_Texto.Name = "ComboBox_Texto";
            this.ComboBox_Texto.Size = new System.Drawing.Size(787, 21);
            this.ComboBox_Texto.TabIndex = 1;
            this.ComboBox_Texto.TextChanged += new System.EventHandler(this.TextBox_Texto_TextChanged);
            this.ComboBox_Texto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Calculadora_Infinita_Semillas_Mundos_KeyDown);
            this.ComboBox_Texto.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ComboBox_Texto_MouseDown);
            // 
            // Ventana_Calculadora_Infinita_Semillas_Mundos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.Controls.Add(this.ComboBox_Texto);
            this.Controls.Add(this.Picture_Base);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.Botón_Copiar_Semilla);
            this.Controls.Add(this.Etiqueta_Cálculos);
            this.Controls.Add(this.TextBox_Resultados);
            this.Controls.Add(this.Etiqueta_Base_Nota);
            this.Controls.Add(this.Numérico_Base);
            this.Controls.Add(this.Etiqueta_Base);
            this.Controls.Add(this.Etiqueta_Semilla);
            this.Controls.Add(this.Etiqueta_Texto);
            this.Controls.Add(this.TextBox_Semilla);
            this.Controls.Add(this.Botón_Copiar_Texto);
            this.DoubleBuffered = true;
            this.Name = "Ventana_Calculadora_Infinita_Semillas_Mundos";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Worlds Seeds Infinite Calculator by Jupisoft";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventana_Calculadora_Infinita_Semillas_Mundos_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventana_Calculadora_Infinita_Semillas_Mundos_FormClosed);
            this.Load += new System.EventHandler(this.Ventana_Calculadora_Infinita_Semillas_Mundos_Load);
            this.Shown += new System.EventHandler(this.Ventana_Calculadora_Infinita_Semillas_Mundos_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Calculadora_Infinita_Semillas_Mundos_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Numérico_Base)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Base)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label Etiqueta_Texto;
        private System.Windows.Forms.TextBox TextBox_Semilla;
        private System.Windows.Forms.Label Etiqueta_Semilla;
        private System.Windows.Forms.Label Etiqueta_Base;
        private System.Windows.Forms.NumericUpDown Numérico_Base;
        private System.Windows.Forms.Label Etiqueta_Base_Nota;
        private System.Windows.Forms.TextBox TextBox_Resultados;
        private System.Windows.Forms.Button Botón_Copiar_Texto;
        private System.Windows.Forms.Button Botón_Copiar_Semilla;
        private System.Windows.Forms.Label Etiqueta_Cálculos;
        private System.Windows.Forms.PictureBox Picture_Base;
        private System.Windows.Forms.ComboBox ComboBox_Texto;
    }
}