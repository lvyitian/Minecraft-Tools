namespace Minecraft_Tools
{
    partial class Ventana_Selector_Herramientas
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
            this.Panel_Inferior = new System.Windows.Forms.Panel();
            this.Etiqueta_Descripción = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Botón_Aceptar = new System.Windows.Forms.Button();
            this.Botón_Cancelar = new System.Windows.Forms.Button();
            this.Botón_Restablecer = new System.Windows.Forms.Button();
            this.Botón_Aleatorizar = new System.Windows.Forms.Button();
            this.CheckBox_Gris = new System.Windows.Forms.CheckBox();
            this.CheckBox_Amarillo = new System.Windows.Forms.CheckBox();
            this.CheckBox_Rojo = new System.Windows.Forms.CheckBox();
            this.CheckBox_Azul = new System.Windows.Forms.CheckBox();
            this.CheckBox_Negro = new System.Windows.Forms.CheckBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.Panel_Separador_Superior = new System.Windows.Forms.Panel();
            this.ListView_Principal = new System.Windows.Forms.ListView();
            this.Lista_Imágenes_16 = new System.Windows.Forms.ImageList(this.components);
            this.Temporizador_Principal = new System.Windows.Forms.Timer(this.components);
            this.Panel_Inferior.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Inferior
            // 
            this.Panel_Inferior.BackColor = System.Drawing.Color.White;
            this.Panel_Inferior.Controls.Add(this.Etiqueta_Descripción);
            this.Panel_Inferior.Controls.Add(this.panel1);
            this.Panel_Inferior.Controls.Add(this.Botón_Aceptar);
            this.Panel_Inferior.Controls.Add(this.Botón_Cancelar);
            this.Panel_Inferior.Controls.Add(this.Botón_Restablecer);
            this.Panel_Inferior.Controls.Add(this.Botón_Aleatorizar);
            this.Panel_Inferior.Controls.Add(this.CheckBox_Gris);
            this.Panel_Inferior.Controls.Add(this.CheckBox_Amarillo);
            this.Panel_Inferior.Controls.Add(this.CheckBox_Rojo);
            this.Panel_Inferior.Controls.Add(this.CheckBox_Azul);
            this.Panel_Inferior.Controls.Add(this.CheckBox_Negro);
            this.Panel_Inferior.Controls.Add(this.textBox9);
            this.Panel_Inferior.Controls.Add(this.Panel_Separador_Superior);
            this.Panel_Inferior.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel_Inferior.Location = new System.Drawing.Point(0, 542);
            this.Panel_Inferior.Name = "Panel_Inferior";
            this.Panel_Inferior.Size = new System.Drawing.Size(984, 69);
            this.Panel_Inferior.TabIndex = 1;
            // 
            // Etiqueta_Descripción
            // 
            this.Etiqueta_Descripción.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Etiqueta_Descripción.Image = global::Minecraft_Tools.Properties.Resources.Controles_Cursor;
            this.Etiqueta_Descripción.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Etiqueta_Descripción.Location = new System.Drawing.Point(0, 1);
            this.Etiqueta_Descripción.Margin = new System.Windows.Forms.Padding(0);
            this.Etiqueta_Descripción.Name = "Etiqueta_Descripción";
            this.Etiqueta_Descripción.Size = new System.Drawing.Size(984, 19);
            this.Etiqueta_Descripción.TabIndex = 3;
            this.Etiqueta_Descripción.Text = "Hover over any tool in the list above to see it\'s description here...";
            this.Etiqueta_Descripción.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Etiqueta_Descripción.UseMnemonic = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(0, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 1);
            this.panel1.TabIndex = 4;
            // 
            // Botón_Aceptar
            // 
            this.Botón_Aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Botón_Aceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Botón_Aceptar.Image = global::Minecraft_Tools.Properties.Resources.Aceptar;
            this.Botón_Aceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Botón_Aceptar.Location = new System.Drawing.Point(766, 33);
            this.Botón_Aceptar.Margin = new System.Windows.Forms.Padding(3, 12, 3, 12);
            this.Botón_Aceptar.Name = "Botón_Aceptar";
            this.Botón_Aceptar.Size = new System.Drawing.Size(100, 24);
            this.Botón_Aceptar.TabIndex = 0;
            this.Botón_Aceptar.Text = " Accept ";
            this.Botón_Aceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Botón_Aceptar.UseVisualStyleBackColor = true;
            this.Botón_Aceptar.Click += new System.EventHandler(this.Botón_Aceptar_Click);
            this.Botón_Aceptar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Selector_Herramientas_KeyDown);
            // 
            // Botón_Cancelar
            // 
            this.Botón_Cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Botón_Cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Botón_Cancelar.Image = global::Minecraft_Tools.Properties.Resources.Cancelar;
            this.Botón_Cancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Botón_Cancelar.Location = new System.Drawing.Point(872, 33);
            this.Botón_Cancelar.Margin = new System.Windows.Forms.Padding(3, 12, 12, 12);
            this.Botón_Cancelar.Name = "Botón_Cancelar";
            this.Botón_Cancelar.Size = new System.Drawing.Size(100, 24);
            this.Botón_Cancelar.TabIndex = 1;
            this.Botón_Cancelar.Text = " Cancel ";
            this.Botón_Cancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Botón_Cancelar.UseVisualStyleBackColor = true;
            this.Botón_Cancelar.Click += new System.EventHandler(this.Botón_Cancelar_Click);
            this.Botón_Cancelar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Selector_Herramientas_KeyDown);
            // 
            // Botón_Restablecer
            // 
            this.Botón_Restablecer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Botón_Restablecer.Image = global::Minecraft_Tools.Properties.Resources.Restablecer;
            this.Botón_Restablecer.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Botón_Restablecer.Location = new System.Drawing.Point(12, 33);
            this.Botón_Restablecer.Margin = new System.Windows.Forms.Padding(12, 12, 3, 12);
            this.Botón_Restablecer.Name = "Botón_Restablecer";
            this.Botón_Restablecer.Size = new System.Drawing.Size(100, 24);
            this.Botón_Restablecer.TabIndex = 5;
            this.Botón_Restablecer.Text = " Restore ";
            this.Botón_Restablecer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Botón_Restablecer.UseVisualStyleBackColor = true;
            this.Botón_Restablecer.Click += new System.EventHandler(this.Botón_Restablecer_Click);
            this.Botón_Restablecer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Selector_Herramientas_KeyDown);
            // 
            // Botón_Aleatorizar
            // 
            this.Botón_Aleatorizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Botón_Aleatorizar.Image = global::Minecraft_Tools.Properties.Resources.Aleatorio;
            this.Botón_Aleatorizar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Botón_Aleatorizar.Location = new System.Drawing.Point(118, 33);
            this.Botón_Aleatorizar.Margin = new System.Windows.Forms.Padding(3, 12, 3, 12);
            this.Botón_Aleatorizar.Name = "Botón_Aleatorizar";
            this.Botón_Aleatorizar.Size = new System.Drawing.Size(100, 24);
            this.Botón_Aleatorizar.TabIndex = 6;
            this.Botón_Aleatorizar.Text = " Randomize ";
            this.Botón_Aleatorizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Botón_Aleatorizar.UseVisualStyleBackColor = true;
            this.Botón_Aleatorizar.Click += new System.EventHandler(this.Botón_Aleatorizar_Click);
            this.Botón_Aleatorizar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Selector_Herramientas_KeyDown);
            // 
            // CheckBox_Gris
            // 
            this.CheckBox_Gris.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBox_Gris.AutoSize = true;
            this.CheckBox_Gris.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(255)))), ((int)(((byte)(208)))));
            this.CheckBox_Gris.Checked = true;
            this.CheckBox_Gris.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox_Gris.Cursor = System.Windows.Forms.Cursors.Help;
            this.CheckBox_Gris.ForeColor = System.Drawing.Color.Black;
            this.CheckBox_Gris.Location = new System.Drawing.Point(648, 37);
            this.CheckBox_Gris.Name = "CheckBox_Gris";
            this.CheckBox_Gris.Size = new System.Drawing.Size(110, 17);
            this.CheckBox_Gris.TabIndex = 11;
            this.CheckBox_Gris.Text = "Tool newly added";
            this.CheckBox_Gris.ThreeState = true;
            this.CheckBox_Gris.UseVisualStyleBackColor = false;
            this.CheckBox_Gris.CheckStateChanged += new System.EventHandler(this.CheckBox_Gris_CheckStateChanged);
            // 
            // CheckBox_Amarillo
            // 
            this.CheckBox_Amarillo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBox_Amarillo.AutoSize = true;
            this.CheckBox_Amarillo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(144)))));
            this.CheckBox_Amarillo.Checked = true;
            this.CheckBox_Amarillo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox_Amarillo.Cursor = System.Windows.Forms.Cursors.Help;
            this.CheckBox_Amarillo.ForeColor = System.Drawing.Color.Black;
            this.CheckBox_Amarillo.Location = new System.Drawing.Point(542, 37);
            this.CheckBox_Amarillo.Name = "CheckBox_Amarillo";
            this.CheckBox_Amarillo.Size = new System.Drawing.Size(106, 17);
            this.CheckBox_Amarillo.TabIndex = 10;
            this.CheckBox_Amarillo.Text = "Tool with secrets";
            this.CheckBox_Amarillo.ThreeState = true;
            this.CheckBox_Amarillo.UseVisualStyleBackColor = false;
            this.CheckBox_Amarillo.CheckStateChanged += new System.EventHandler(this.CheckBox_Amarillo_CheckStateChanged);
            // 
            // CheckBox_Rojo
            // 
            this.CheckBox_Rojo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBox_Rojo.AutoSize = true;
            this.CheckBox_Rojo.Cursor = System.Windows.Forms.Cursors.Help;
            this.CheckBox_Rojo.ForeColor = System.Drawing.Color.Red;
            this.CheckBox_Rojo.Location = new System.Drawing.Point(436, 37);
            this.CheckBox_Rojo.Name = "CheckBox_Rojo";
            this.CheckBox_Rojo.Size = new System.Drawing.Size(101, 17);
            this.CheckBox_Rojo.TabIndex = 9;
            this.CheckBox_Rojo.Text = "Tool won\'t work";
            this.CheckBox_Rojo.ThreeState = true;
            this.CheckBox_Rojo.UseVisualStyleBackColor = true;
            this.CheckBox_Rojo.CheckStateChanged += new System.EventHandler(this.CheckBox_Rojo_CheckStateChanged);
            this.CheckBox_Rojo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Selector_Herramientas_KeyDown);
            // 
            // CheckBox_Azul
            // 
            this.CheckBox_Azul.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBox_Azul.AutoSize = true;
            this.CheckBox_Azul.Checked = true;
            this.CheckBox_Azul.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.CheckBox_Azul.Cursor = System.Windows.Forms.Cursors.Help;
            this.CheckBox_Azul.ForeColor = System.Drawing.Color.Blue;
            this.CheckBox_Azul.Location = new System.Drawing.Point(330, 37);
            this.CheckBox_Azul.Name = "CheckBox_Azul";
            this.CheckBox_Azul.Size = new System.Drawing.Size(101, 17);
            this.CheckBox_Azul.TabIndex = 8;
            this.CheckBox_Azul.Text = "Tool might work";
            this.CheckBox_Azul.ThreeState = true;
            this.CheckBox_Azul.UseVisualStyleBackColor = false;
            this.CheckBox_Azul.CheckStateChanged += new System.EventHandler(this.CheckBox_Azul_CheckStateChanged);
            this.CheckBox_Azul.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Selector_Herramientas_KeyDown);
            // 
            // CheckBox_Negro
            // 
            this.CheckBox_Negro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBox_Negro.AutoSize = true;
            this.CheckBox_Negro.Checked = true;
            this.CheckBox_Negro.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox_Negro.Cursor = System.Windows.Forms.Cursors.Help;
            this.CheckBox_Negro.Location = new System.Drawing.Point(224, 37);
            this.CheckBox_Negro.Name = "CheckBox_Negro";
            this.CheckBox_Negro.Size = new System.Drawing.Size(98, 17);
            this.CheckBox_Negro.TabIndex = 7;
            this.CheckBox_Negro.Text = "Tool works fine";
            this.CheckBox_Negro.ThreeState = true;
            this.CheckBox_Negro.UseVisualStyleBackColor = false;
            this.CheckBox_Negro.CheckStateChanged += new System.EventHandler(this.CheckBox_Negro_CheckStateChanged);
            this.CheckBox_Negro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Selector_Herramientas_KeyDown);
            // 
            // textBox9
            // 
            this.textBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox9.BackColor = System.Drawing.Color.White;
            this.textBox9.Enabled = false;
            this.textBox9.Location = new System.Drawing.Point(952, 15);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(20, 20);
            this.textBox9.TabIndex = 10;
            this.textBox9.Visible = false;
            // 
            // Panel_Separador_Superior
            // 
            this.Panel_Separador_Superior.BackColor = System.Drawing.Color.Black;
            this.Panel_Separador_Superior.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Separador_Superior.Location = new System.Drawing.Point(0, 0);
            this.Panel_Separador_Superior.Name = "Panel_Separador_Superior";
            this.Panel_Separador_Superior.Size = new System.Drawing.Size(984, 1);
            this.Panel_Separador_Superior.TabIndex = 2;
            // 
            // ListView_Principal
            // 
            this.ListView_Principal.BackColor = System.Drawing.Color.White;
            this.ListView_Principal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListView_Principal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ListView_Principal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListView_Principal.FullRowSelect = true;
            this.ListView_Principal.HideSelection = false;
            this.ListView_Principal.Location = new System.Drawing.Point(0, 0);
            this.ListView_Principal.Margin = new System.Windows.Forms.Padding(0);
            this.ListView_Principal.MultiSelect = false;
            this.ListView_Principal.Name = "ListView_Principal";
            this.ListView_Principal.Size = new System.Drawing.Size(984, 542);
            this.ListView_Principal.TabIndex = 0;
            this.ListView_Principal.UseCompatibleStateImageBehavior = false;
            this.ListView_Principal.View = System.Windows.Forms.View.Tile;
            this.ListView_Principal.SelectedIndexChanged += new System.EventHandler(this.ListView_Principal_SelectedIndexChanged);
            this.ListView_Principal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Selector_Herramientas_KeyDown);
            this.ListView_Principal.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListView_Principal_MouseClick);
            this.ListView_Principal.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView_Principal_MouseDoubleClick);
            this.ListView_Principal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListView_Principal_MouseDown);
            this.ListView_Principal.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ListView_Principal_MouseMove);
            // 
            // Lista_Imágenes_16
            // 
            this.Lista_Imágenes_16.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.Lista_Imágenes_16.ImageSize = new System.Drawing.Size(16, 16);
            this.Lista_Imágenes_16.TransparentColor = System.Drawing.Color.Empty;
            // 
            // Temporizador_Principal
            // 
            this.Temporizador_Principal.Tick += new System.EventHandler(this.Temporizador_Principal_Tick);
            // 
            // Ventana_Selector_Herramientas
            // 
            this.AcceptButton = this.Botón_Aceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Botón_Cancelar;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.ListView_Principal);
            this.Controls.Add(this.Panel_Inferior);
            this.DoubleBuffered = true;
            this.Name = "Ventana_Selector_Herramientas";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tool Selector by Jupisoft";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventana_Selector_Herramientas_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventana_Selector_Herramientas_FormClosed);
            this.Load += new System.EventHandler(this.Ventana_Selector_Herramientas_Load);
            this.Shown += new System.EventHandler(this.Ventana_Selector_Herramientas_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Selector_Herramientas_KeyDown);
            this.Panel_Inferior.ResumeLayout(false);
            this.Panel_Inferior.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Botón_Aceptar;
        private System.Windows.Forms.Button Botón_Cancelar;
        private System.Windows.Forms.Button Botón_Restablecer;
        private System.Windows.Forms.Panel Panel_Inferior;
        private System.Windows.Forms.ListView ListView_Principal;
        private System.Windows.Forms.Panel Panel_Separador_Superior;
        private System.Windows.Forms.ImageList Lista_Imágenes_16;
        private System.Windows.Forms.CheckBox CheckBox_Negro;
        private System.Windows.Forms.CheckBox CheckBox_Rojo;
        private System.Windows.Forms.CheckBox CheckBox_Azul;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Button Botón_Aleatorizar;
        private System.Windows.Forms.Timer Temporizador_Principal;
        private System.Windows.Forms.CheckBox CheckBox_Amarillo;
        private System.Windows.Forms.CheckBox CheckBox_Gris;
        private System.Windows.Forms.Label Etiqueta_Descripción;
        private System.Windows.Forms.Panel panel1;
    }
}