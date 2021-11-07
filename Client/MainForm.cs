using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using SimpleTcp;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class MainForm : Form
    {
        SimpleTcpServer server;
        public MainForm()
        {
            InitializeComponent();
        }
    }
}
