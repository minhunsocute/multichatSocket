﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Client
{
    public partial class client_TCP : Form
    {
        public client_TCP()
        {
            InitializeComponent();
        }

        private void client_TCP_Load(object sender, EventArgs e)
        {
            
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            add_server f = new add_server();
            f.Show();
        }

        private void BtnChange_Click(object sender, EventArgs e)
        {

        }
    }
}
