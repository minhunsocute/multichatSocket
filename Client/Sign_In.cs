using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using SimpleTcp;

namespace Client
{
    public partial class Sign_In : Form
    {
        public static SimpleTcpClient client;

        public Sign_In()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Sign_Up f = new Sign_Up();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textIP.Text)&&!string.IsNullOrEmpty(textUsername.Text) && !string.IsNullOrEmpty(textPass.Text)) {
                client = new SimpleTcpClient($"{textIP.Text}");
                client.Events.Connected += Events_Connected;
                client.Events.Disconnected += Event_DisConnected;
                client.Events.DataReceived += Event_DataReceived;
                try {
                    client.Connect();
                    string send = $"1{textUsername.Text}@{textPass.Text}";
                    client.Send(send);
                }
                catch { }
            }
            else
            {
                MessageBox.Show("INPUT IS EMPTY", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void checkString(string s) {
            if (s=="success") {
                MainForm f = new MainForm();
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else if (s=="unsuccess") {
                //client.Disconnect();
                MessageBox.Show("Connect is unsuceesfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Event_DataReceived(object sender, DataReceivedEventArgs e){
            string receive = Encoding.UTF8.GetString(e.Data);
            checkString(receive);
        }

        private void Event_DisConnected(object sender, ClientDisconnectedEventArgs e)
        {
        }

        private void Events_Connected(object sender, ClientConnectedEventArgs e){ 
        }
    }
}
