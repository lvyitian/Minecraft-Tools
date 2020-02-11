using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Tools
{
    public partial class Ventana_Información_Bloques : Form
    {
        public Ventana_Información_Bloques()
        {
            InitializeComponent();
        }

        internal bool Variable_Siempre_Visible = false;

        private void Ventana_Información_Bloques_Load(object sender, EventArgs e)
        {
            this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
        }

        private void Ventana_Información_Bloques_Shown(object sender, EventArgs e)
        {

        }

        private void Ventana_Información_Bloques_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Ventana_Información_Bloques_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Ventana_Información_Bloques_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
