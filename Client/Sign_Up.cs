using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleTcp;

namespace Client
{
    public partial class Sign_Up : Form
    {
        SimpleTcpClient client;
        public Sign_Up()
        {
            InitializeComponent();
            LoadConnect();
        }
        private void LoadConnect() {
            Sign_In f = new Sign_In();
            client = new SimpleTcpClient(f.textIP.Text);
            client.Events.DataReceived += Event_DataReceived;
            try {
                client.Connect();
            }
            catch { }
        }

        private void Event_DataReceived(object sender, DataReceivedEventArgs e)
        {
            if (Encoding.UTF8.GetString(e.Data) == "success") {
                client.Disconnect();
                this.Invoke(new Action(() => { this.Close(); }));
            }
            else
                MessageBox.Show("Account already exists", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void BtnConnect_Click(object sender, EventArgs e){
            if (!string.IsNullOrEmpty(textName.Text)&&!string.IsNullOrEmpty(textUsername.Text) && !string.IsNullOrEmpty(textPass.Text) && !string.IsNullOrEmpty(textRepass.Text)) { 
                if(textPass.Text == textRepass.Text) {
                    client.Send($"2{textUsername.Text}@{textPass.Text}@{textName.Text}");                
                }
                else {
                    MessageBox.Show("RePass is Invalid", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Input is empty","Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e){
            client.Disconnect();
        }
    }
}
