namespace Minecraft_Tools
{
    partial class Ventana_Depurador_Excepciones
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
            this.Editor_RTF = new System.Windows.Forms.RichTextBox();
            this.Menú_Contextual = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Menú_Contextual_Localizar_Debugger = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_1 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Invertir_Orden = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_2 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Ordenar_0 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Ordenar_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Ordenar_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Ordenar_3 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Ordenar_4 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Enviar_Correo = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_3 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual.SuspendLayout();
            this.SuspendLayout();
            // 
            // Editor_RTF
            // 
            this.Editor_RTF.BackColor = System.Drawing.Color.White;
            this.Editor_RTF.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Editor_RTF.ContextMenuStrip = this.Menú_Contextual;
            this.Editor_RTF.DetectUrls = false;
            this.Editor_RTF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Editor_RTF.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Editor_RTF.Location = new System.Drawing.Point(0, 0);
            this.Editor_RTF.Name = "Editor_RTF";
            this.Editor_RTF.ReadOnly = true;
            this.Editor_RTF.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.Editor_RTF.ShowSelectionMargin = true;
            this.Editor_RTF.Size = new System.Drawing.Size(624, 441);
            this.Editor_RTF.TabIndex = 0;
            this.Editor_RTF.Text = "";
            this.Editor_RTF.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Depurador_Excepciones_KeyDown);
            // 
            // Menú_Contextual
            // 
            this.Menú_Contextual.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menú_Contextual_Enviar_Correo,
            this.Menú_Contextual_Separador_1,
            this.Menú_Contextual_Localizar_Debugger,
            this.Menú_Contextual_Separador_2,
            this.Menú_Contextual_Invertir_Orden,
            this.Menú_Contextual_Separador_3,
            this.Menú_Contextual_Ordenar_0,
            this.Menú_Contextual_Ordenar_1,
            this.Menú_Contextual_Ordenar_2,
            this.Menú_Contextual_Ordenar_3,
            this.Menú_Contextual_Ordenar_4});
            this.Menú_Contextual.Name = "Menú_Contextual";
            this.Menú_Contextual.Size = new System.Drawing.Size(319, 198);
            // 
            // Menú_Contextual_Localizar_Debugger
            // 
            this.Menú_Contextual_Localizar_Debugger.Image = global::Minecraft_Tools.Properties.Resources.Ejecutar;
            this.Menú_Contextual_Localizar_Debugger.Name = "Menú_Contextual_Localizar_Debugger";
            this.Menú_Contextual_Localizar_Debugger.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.Menú_Contextual_Localizar_Debugger.Size = new System.Drawing.Size(318, 22);
            this.Menú_Contextual_Localizar_Debugger.Text = "Locate the \"Debugger\" file (and attach it)...";
            this.Menú_Contextual_Localizar_Debugger.Click += new System.EventHandler(this.Menú_Contextual_Localizar_Debugger_Click);
            // 
            // Menú_Contextual_Separador_1
            // 
            this.Menú_Contextual_Separador_1.Name = "Menú_Contextual_Separador_1";
            this.Menú_Contextual_Separador_1.Size = new System.Drawing.Size(308, 6);
            // 
            // Menú_Contextual_Invertir_Orden
            // 
            this.Menú_Contextual_Invertir_Orden.Checked = true;
            this.Menú_Contextual_Invertir_Orden.CheckOnClick = true;
            this.Menú_Contextual_Invertir_Orden.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menú_Contextual_Invertir_Orden.Name = "Menú_Contextual_Invertir_Orden";
            this.Menú_Contextual_Invertir_Orden.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.Menú_Contextual_Invertir_Orden.Size = new System.Drawing.Size(311, 22);
            this.Menú_Contextual_Invertir_Orden.Text = "Invert the order";
            this.Menú_Contextual_Invertir_Orden.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Invertir_Orden_CheckedChanged);
            // 
            // Menú_Contextual_Separador_2
            // 
            this.Menú_Contextual_Separador_2.Name = "Menú_Contextual_Separador_2";
            this.Menú_Contextual_Separador_2.Size = new System.Drawing.Size(308, 6);
            // 
            // Menú_Contextual_Ordenar_0
            // 
            this.Menú_Contextual_Ordenar_0.Name = "Menú_Contextual_Ordenar_0";
            this.Menú_Contextual_Ordenar_0.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.Menú_Contextual_Ordenar_0.Size = new System.Drawing.Size(311, 22);
            this.Menú_Contextual_Ordenar_0.Text = "Order by CRC-32";
            this.Menú_Contextual_Ordenar_0.Click += new System.EventHandler(this.Menú_Contextual_Ordenar_Click);
            // 
            // Menú_Contextual_Ordenar_1
            // 
            this.Menú_Contextual_Ordenar_1.Name = "Menú_Contextual_Ordenar_1";
            this.Menú_Contextual_Ordenar_1.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.Menú_Contextual_Ordenar_1.Size = new System.Drawing.Size(311, 22);
            this.Menú_Contextual_Ordenar_1.Text = "Order by first date";
            this.Menú_Contextual_Ordenar_1.Click += new System.EventHandler(this.Menú_Contextual_Ordenar_Click);
            // 
            // Menú_Contextual_Ordenar_2
            // 
            this.Menú_Contextual_Ordenar_2.Checked = true;
            this.Menú_Contextual_Ordenar_2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menú_Contextual_Ordenar_2.Name = "Menú_Contextual_Ordenar_2";
            this.Menú_Contextual_Ordenar_2.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.Menú_Contextual_Ordenar_2.Size = new System.Drawing.Size(311, 22);
            this.Menú_Contextual_Ordenar_2.Text = "Order by last date";
            this.Menú_Contextual_Ordenar_2.Click += new System.EventHandler(this.Menú_Contextual_Ordenar_Click);
            // 
            // Menú_Contextual_Ordenar_3
            // 
            this.Menú_Contextual_Ordenar_3.Name = "Menú_Contextual_Ordenar_3";
            this.Menú_Contextual_Ordenar_3.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.Menú_Contextual_Ordenar_3.Size = new System.Drawing.Size(311, 22);
            this.Menú_Contextual_Ordenar_3.Text = "Order by repetitions";
            this.Menú_Contextual_Ordenar_3.Click += new System.EventHandler(this.Menú_Contextual_Ordenar_Click);
            // 
            // Menú_Contextual_Ordenar_4
            // 
            this.Menú_Contextual_Ordenar_4.Name = "Menú_Contextual_Ordenar_4";
            this.Menú_Contextual_Ordenar_4.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.Menú_Contextual_Ordenar_4.Size = new System.Drawing.Size(311, 22);
            this.Menú_Contextual_Ordenar_4.Text = "Order by messages";
            this.Menú_Contextual_Ordenar_4.Click += new System.EventHandler(this.Menú_Contextual_Ordenar_Click);
            // 
            // Menú_Contextual_Enviar_Correo
            // 
            this.Menú_Contextual_Enviar_Correo.Image = global::Minecraft_Tools.Properties.Resources.Correo;
            this.Menú_Contextual_Enviar_Correo.Name = "Menú_Contextual_Enviar_Correo";
            this.Menú_Contextual_Enviar_Correo.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.Menú_Contextual_Enviar_Correo.Size = new System.Drawing.Size(316, 22);
            this.Menú_Contextual_Enviar_Correo.Text = "Send an e-mail (add the \"Debugger\" file)...";
            this.Menú_Contextual_Enviar_Correo.Click += new System.EventHandler(this.Menú_Contextual_Enviar_Correo_Click);
            // 
            // Menú_Contextual_Separador_3
            // 
            this.Menú_Contextual_Separador_3.Name = "Menú_Contextual_Separador_3";
            this.Menú_Contextual_Separador_3.Size = new System.Drawing.Size(308, 6);
            // 
            // Ventana_Depurador_Excepciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.ContextMenuStrip = this.Menú_Contextual;
            this.Controls.Add(this.Editor_RTF);
            this.DoubleBuffered = true;
            this.MinimizeBox = false;
            this.Name = "Ventana_Depurador_Excepciones";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exception Debugger by Jupisoft";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventana_Depurador_Excepciones_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventana_Depurador_Excepciones_FormClosed);
            this.Load += new System.EventHandler(this.Ventana_Depurador_Excepciones_Load);
            this.Shown += new System.EventHandler(this.Ventana_Depurador_Excepciones_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Depurador_Excepciones_KeyDown);
            this.Menú_Contextual.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox Editor_RTF;
        private System.Windows.Forms.ContextMenuStrip Menú_Contextual;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Ordenar_0;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_1;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Invertir_Orden;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Ordenar_1;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Ordenar_2;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Ordenar_3;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Ordenar_4;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_2;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Localizar_Debugger;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Enviar_Correo;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_3;
    }
}