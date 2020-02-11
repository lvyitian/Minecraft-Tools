using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    internal static class Invocación
    {
        internal delegate void Delegado_ContextMenuStrip_Enabled(ContextMenuStrip Menú, bool Habilitar);
        internal delegate void Delegado_Control_BackColor(Control Control, Color Color_ARGB);
        internal delegate void Delegado_Control_Cursor(Control Control, Cursor Cursor);
        internal delegate void Delegado_Control_Enabled(Control Control, bool Habilitar);
        internal delegate void Delegado_Control_Focus(Control Control);
        internal delegate void Delegado_Control_Invalidate(Control Control);
        internal delegate void Delegado_Control_Invalidate_Rectangle(Control Control, Rectangle Rectángulo);
        internal delegate void Delegado_Control_Invalidate_Update(Control Control);
        internal delegate void Delegado_Control_Select(Control Control);
        internal delegate void Delegado_Control_Text(Control Control, string Texto);
        internal delegate void Delegado_Control_Update(Control Control);
        internal delegate void Delegado_Form_Close(Form Ventana);
        internal delegate void Delegado_Control_Visible(Control Control, bool Visible);
        internal delegate DialogResult Delegado_IWin32Window_MessageBox(IWin32Window Control, string Texto, string Título, MessageBoxButtons Botones, MessageBoxIcon Icono);
        internal delegate void Delegado_NumericUpDown_Value(NumericUpDown Control, decimal Valor);
        internal delegate void Delegado_PictureBox_Image(PictureBox Picture, Image Imagen);
        internal delegate void Delegado_PictureBox_SizeMode(PictureBox Picture, PictureBoxSizeMode Zoom);
        internal delegate void Delegado_ProgressBar_Maximum(ProgressBar Control, int Valor);
        internal delegate void Delegado_ProgressBar_Minimum(ProgressBar Control, int Valor);
        internal delegate void Delegado_ProgressBar_Value(ProgressBar Control, int Valor);
        internal delegate void Delegado_TextBox_SelectionLength(TextBox Control, int Longitud);
        internal delegate void Delegado_TextBox_SelectionStart(TextBox Control, int Índice);
        internal delegate void Delegado_ToolStripLabel_Text(ToolStripLabel Control, string Texto);
        internal delegate void Delegado_ToolTip_SetToolTip(ToolTip Información_Contextual, Control Control, string Texto);
        internal delegate void Delegado_TreeView_Nodes_Add(TreeView Árbol, int Índice, TreeNode Nodo);

        internal static void Ejecutar_Delegado_ContextMenuStrip_Enabled(ContextMenuStrip Menú, bool Habilitar)
        {
            try
            {
                Menú.Enabled = Habilitar;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_Control_BackColor(Control Control, Color Color_ARGB)
        {
            try
            {
                Control.BackColor = Color_ARGB;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_Control_Cursor(Control Control, Cursor Cursor)
        {
            try
            {
                Control.Cursor = Cursor;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_Control_Enabled(Control Control, bool Habilitar)
        {
            try
            {
                Control.Enabled = Habilitar;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_Control_Focus(Control Control)
        {
            try
            {
                Control.Focus();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_Control_Invalidate(Control Control)
        {
            try
            {
                Control.Invalidate();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_Control_Invalidate_Rectangle(Control Control, Rectangle Rectángulo)
        {
            try
            {
                Control.Invalidate(Rectángulo);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_Control_Invalidate_Update(Control Control)
        {
            try
            {
                Control.Invalidate();
                Control.Update();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_Control_Select(Control Control)
        {
            try
            {
                Control.Select();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_Control_Text(Control Control, string Texto)
        {
            try
            {
                Control.Text = Texto;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_Control_Update(Control Control)
        {
            try
            {
                Control.Update();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_Form_Close(Form Ventana)
        {
            try
            {
                Ventana.Close();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_Control_Visible(Control Control, bool Visible)
        {
            try
            {
                Control.Visible = Visible;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static DialogResult Ejecutar_Delegado_IWin32Window_MessageBox(IWin32Window Ventana, string Texto, string Título, MessageBoxButtons Botones, MessageBoxIcon Icono)
        {
            try
            {
                return MessageBox.Show(Ventana, Texto, Título, Botones, Icono);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return DialogResult.Cancel;
        }

        internal static void Ejecutar_Delegado_NumericUpDown_Value(NumericUpDown Control, decimal Valor)
        {
            try
            {
                Control.Value = Valor;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_PictureBox_Image(PictureBox Control, Image Imagen)
        {
            try
            {
                Control.Image = Imagen;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }
        
        internal static void Ejecutar_Delegado_PictureBox_SizeMode(PictureBox Control, PictureBoxSizeMode Zoom)
        {
            try
            {
                Control.SizeMode = Zoom;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_ProgressBar_Maximum(ProgressBar Control, int Valor)
        {
            try
            {
                Control.Maximum = Valor;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_ProgressBar_Minimum(ProgressBar Control, int Valor)
        {
            try
            {
                Control.Minimum = Valor;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_ProgressBar_Value(ProgressBar Control, int Valor)
        {
            try
            {
                Control.Value = Math.Min(Control.Maximum, Math.Max(Control.Minimum, Valor));
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_TextBox_SelectionLength(TextBox Control, int Longitud)
        {
            try
            {
                Control.SelectionLength = Longitud;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_TextBox_SelectionStart(TextBox Control, int Índice)
        {
            try
            {
                Control.SelectionStart = Índice;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_ToolStripLabel_Text(ToolStripLabel Control, string Texto)
        {
            try
            {
                Control.Text = Texto;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_ToolTip_SetToolTip(ToolTip Información_Contextual, Control Control, string Texto)
        {
            try
            {
                Información_Contextual.SetToolTip(Control, Texto);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal static void Ejecutar_Delegado_TreeView_Nodes_Add(TreeView Árbol, int Índice, TreeNode Nodo)
        {
            try
            {
                Árbol.Nodes[Índice].Nodes.Add(Nodo);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }
    }
}
