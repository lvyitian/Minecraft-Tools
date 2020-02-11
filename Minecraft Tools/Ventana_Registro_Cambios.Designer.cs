namespace Minecraft_Tools
{
    partial class Ventana_Registro_Cambios
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
            this.Temporizador_Principal = new System.Windows.Forms.Timer(this.components);
            this.Barra_Estado = new System.Windows.Forms.ToolStrip();
            this.Barra_Estado_Etiqueta_Sugerencia = new System.Windows.Forms.ToolStripLabel();
            this.Barra_Estado_Separador_2 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Menú_Contextual_Copiar = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Guardar = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Guardar_RTF = new System.Windows.Forms.ToolStripMenuItem();
            this.RichTextBox_Cambios = new System.Windows.Forms.RichTextBox();
            this.Barra_Estado.SuspendLayout();
            this.Menú_Contextual.SuspendLayout();
            this.SuspendLayout();
            // 
            // Temporizador_Principal
            // 
            this.Temporizador_Principal.Interval = 1;
            this.Temporizador_Principal.Tick += new System.EventHandler(this.Temporizador_Principal_Tick);
            // 
            // Barra_Estado
            // 
            this.Barra_Estado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Barra_Estado.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.Barra_Estado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Barra_Estado_Etiqueta_Sugerencia,
            this.Barra_Estado_Separador_2});
            this.Barra_Estado.Location = new System.Drawing.Point(0, 461);
            this.Barra_Estado.Name = "Barra_Estado";
            this.Barra_Estado.Size = new System.Drawing.Size(659, 25);
            this.Barra_Estado.TabIndex = 1;
            this.Barra_Estado.Text = "Status bar";
            // 
            // Barra_Estado_Etiqueta_Sugerencia
            // 
            this.Barra_Estado_Etiqueta_Sugerencia.Image = global::Minecraft_Tools.Properties.Resources.Ayuda;
            this.Barra_Estado_Etiqueta_Sugerencia.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.Barra_Estado_Etiqueta_Sugerencia.Name = "Barra_Estado_Etiqueta_Sugerencia";
            this.Barra_Estado_Etiqueta_Sugerencia.Size = new System.Drawing.Size(933, 16);
            this.Barra_Estado_Etiqueta_Sugerencia.Text = "Tip: left click on the main text display to give it focus, then hold control and " +
    "scroll your mouse wheel to change the zoom of the text, which you\'ll see on the " +
    "top right corner.";
            // 
            // Barra_Estado_Separador_2
            // 
            this.Barra_Estado_Separador_2.Name = "Barra_Estado_Separador_2";
            this.Barra_Estado_Separador_2.Size = new System.Drawing.Size(6, 25);
            // 
            // Menú_Contextual
            // 
            this.Menú_Contextual.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menú_Contextual_Copiar,
            this.Menú_Contextual_Guardar,
            this.Menú_Contextual_Guardar_RTF});
            this.Menú_Contextual.Name = "Menú_Contextual";
            this.Menú_Contextual.Size = new System.Drawing.Size(236, 70);
            // 
            // Menú_Contextual_Copiar
            // 
            this.Menú_Contextual_Copiar.Image = global::Minecraft_Tools.Properties.Resources.Copiar;
            this.Menú_Contextual_Copiar.Name = "Menú_Contextual_Copiar";
            this.Menú_Contextual_Copiar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.Menú_Contextual_Copiar.Size = new System.Drawing.Size(235, 22);
            this.Menú_Contextual_Copiar.Text = "Copy the help";
            this.Menú_Contextual_Copiar.Click += new System.EventHandler(this.Menú_Contextual_Copiar_Click);
            // 
            // Menú_Contextual_Guardar
            // 
            this.Menú_Contextual_Guardar.Image = global::Minecraft_Tools.Properties.Resources.Guardar;
            this.Menú_Contextual_Guardar.Name = "Menú_Contextual_Guardar";
            this.Menú_Contextual_Guardar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.Menú_Contextual_Guardar.Size = new System.Drawing.Size(235, 22);
            this.Menú_Contextual_Guardar.Text = "Save as a TXT file";
            this.Menú_Contextual_Guardar.Click += new System.EventHandler(this.Menú_Contextual_Guardar_Click);
            // 
            // Menú_Contextual_Guardar_RTF
            // 
            this.Menú_Contextual_Guardar_RTF.Image = global::Minecraft_Tools.Properties.Resources.Guardar;
            this.Menú_Contextual_Guardar_RTF.Name = "Menú_Contextual_Guardar_RTF";
            this.Menú_Contextual_Guardar_RTF.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.Menú_Contextual_Guardar_RTF.Size = new System.Drawing.Size(235, 22);
            this.Menú_Contextual_Guardar_RTF.Text = "Save as a RTF file";
            this.Menú_Contextual_Guardar_RTF.Click += new System.EventHandler(this.Menú_Contextual_Guardar_RTF_Click);
            // 
            // RichTextBox_Cambios
            // 
            this.RichTextBox_Cambios.BackColor = System.Drawing.Color.White;
            this.RichTextBox_Cambios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichTextBox_Cambios.ContextMenuStrip = this.Menú_Contextual;
            this.RichTextBox_Cambios.DetectUrls = false;
            this.RichTextBox_Cambios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RichTextBox_Cambios.Location = new System.Drawing.Point(0, 0);
            this.RichTextBox_Cambios.Name = "RichTextBox_Cambios";
            this.RichTextBox_Cambios.ReadOnly = true;
            this.RichTextBox_Cambios.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RichTextBox_Cambios.ShowSelectionMargin = true;
            this.RichTextBox_Cambios.Size = new System.Drawing.Size(659, 461);
            this.RichTextBox_Cambios.TabIndex = 0;
            this.RichTextBox_Cambios.Text = "";
            this.RichTextBox_Cambios.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Registro_Cambios_KeyDown);
            // 
            // Ventana_Registro_Cambios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 486);
            this.ContextMenuStrip = this.Menú_Contextual;
            this.Controls.Add(this.RichTextBox_Cambios);
            this.Controls.Add(this.Barra_Estado);
            this.DoubleBuffered = true;
            this.Name = "Ventana_Registro_Cambios";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Log by Jupisoft";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventana_Registro_Cambios_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventana_Registro_Cambios_FormClosed);
            this.Load += new System.EventHandler(this.Ventana_Registro_Cambios_Load);
            this.Shown += new System.EventHandler(this.Ventana_Registro_Cambios_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Registro_Cambios_KeyDown);
            this.Barra_Estado.ResumeLayout(false);
            this.Barra_Estado.PerformLayout();
            this.Menú_Contextual.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer Temporizador_Principal;
        private System.Windows.Forms.ToolStrip Barra_Estado;
        private System.Windows.Forms.ToolStripLabel Barra_Estado_Etiqueta_Sugerencia;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_2;
        private System.Windows.Forms.ContextMenuStrip Menú_Contextual;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Copiar;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Guardar;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Guardar_RTF;
        private System.Windows.Forms.RichTextBox RichTextBox_Cambios;
    }
}