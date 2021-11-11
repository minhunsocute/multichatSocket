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
        public static int checkServerDisConnect = 0;
        public static string userName2Form = "";
        public static string ip2Form = "";
        public Sign_In()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textIP.Text)) {
                Sign_Up f = new Sign_Up();
                this.Invoke(new Action(() => { this.Hide(); }));
                f.ShowDialog();
                this.Invoke(new Action(() => { this.Show(); }));
            }
            else {
                MessageBox.Show("IP:Port is Empty", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                Main_face f = new Main_face();
                //Cross-thread operation not valid: xu ly loi nay
                client.Disconnect();
                userName2Form = string.Empty;userName2Form = textUsername.Text;
                ip2Form = string.Empty;ip2Form = textIP.Text;
                this.Invoke(new Action(()=>{this.Hide();}));
                f.ShowDialog();
                this.Invoke(new Action(() => { this.Show(); }));
                /*if (checkServerDisConnect == 0) { 
                    client.Send($"3{textUsername.Text}");client.Disconnect();
                }*/
                textUsername.Invoke(new Action(() => { textUsername.Text = string.Empty; }));
                textPass.Invoke(new Action(() => { textPass.Text = string.Empty; }));
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

        private void Event_DisConnected(object sender, ClientDisconnectedEventArgs e){
        
        }

        private void Events_Connected(object sender, ClientConnectedEventArgs e){ 
        
        }
    }
}
