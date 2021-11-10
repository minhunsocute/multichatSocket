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
using SimpleTcp;

namespace Client
{
    public partial class MainForm : Form
    {
        SimpleTcpClient client;
        string userNamebetween = "";
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e){
            client = new SimpleTcpClient(Sign_In.ip2Form);
            userNamebetween = Sign_In.userName2Form;
            client.Events.Disconnected += Event_Disconnected;
            client.Events.DataReceived += Event_DataRaceived;
            try {
                client.Connect();
                client.Send($"4{Sign_In.userName2Form}");
            }
            catch { }
        }

        private void Event_DataRaceived(object sender, DataReceivedEventArgs e){
            string s = Encoding.UTF8.GetString(e.Data);
            guna2TextBox1.Invoke(new Action(()=>{
                guna2TextBox1.Text = string.Empty;
                guna2TextBox1.Text = s;
            }));    
            if(s== "Disconnect") {
                MessageBox.Show("Server is Disconnected", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Invoke(new Action(() => { this.Close(); }));
            }
        }
        private void Event_Disconnected(object sender, ClientDisconnectedEventArgs e){
            //MessageBox.Show("Server is Disconnected","Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
            //this.Close();
        }

        private void circularBtn4_Click(object sender, EventArgs e)
        {
            client.Send(guna2TextBox1.Text);
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            client.Send($"3{userNamebetween}"); client.Disconnect();
            client.Disconnect();
        }
    }
}
    