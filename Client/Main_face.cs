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
    public partial class Main_face : Form
    {
        SimpleTcpClient client;
        string userNamebetween = "";
        public Main_face()
        {
            InitializeComponent();
        }

        private void Main_face_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient(Sign_In.ip2Form);
            userNamebetween = Sign_In.userName2Form;
            client.Events.Disconnected += Event_Disconnected;
            client.Events.DataReceived += Event_DataReceived;
            try {
                client.Connect();
                client.Send($"4{Sign_In.userName2Form}");
            }
            catch { }
            textNameF.Text = Sign_In.userName2Form;
        }

        private void Event_DataReceived(object sender, DataReceivedEventArgs e)
        {
            // throw new NotImplementedException();
            string s = Encoding.UTF8.GetString(e.Data);
            this.Invoke(new Action(() =>
            {
                guna2TextBox1.Text = string.Empty;
                guna2TextBox1.Text = s;
            }));
            if (s == "Disconnect") {
                MessageBox.Show("Server is Disconnected", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Invoke(new Action(() => { this.Close(); }));
            }
        }

        private void Event_Disconnected(object sender, ClientDisconnectedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void circularBtn9_Click(object sender, EventArgs e)
        {
            client.Send(messageText.Text);
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            client.Send($"3{userNamebetween}");client.Disconnect();
        }
        // xu ly messenger

        // xy ly trang chu
    }

}
