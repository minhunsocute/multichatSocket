using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ListClietnActi : UserControl
    {
        public ListClietnActi()
        {
            InitializeComponent();
        }

        public ListClietnActi(string name) {
            this.Name1 = name;
           
        }
        private string name;
        public string Name1 { get => name; set => name = value;  }

        private void ListClietnActi_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = DefaultBackColor;
            nameText.BackColor = DefaultBackColor;
        }

        private void ListClietnActi_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Silver;
            nameText.BackColor = Color.Silver;
        }

        private void ListClietnActi_Click(object sender, EventArgs e)
        {
            Main_face f = new Main_face();
            
        }

        private void nameText_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Silver;
            nameText.BackColor = Color.Silver;
        }

        private void nameText_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = DefaultBackColor;
            nameText.BackColor = DefaultBackColor;
        }
    }
}
