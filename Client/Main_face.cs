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
        List<ListClietnActi> listClient;
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
            //LoadListClientActi();
        }

        private void Event_DataReceived(object sender, DataReceivedEventArgs e)
        {
            // throw new NotImplementedException();
            string s = Encoding.UTF8.GetString(e.Data);
            this.Invoke(new Action(() =>
            {
                
                if (s[0] == '1') {
                    MessageBox.Show("Server is Disconnected", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Invoke(new Action(() => { this.Close(); }));
                }
                else if (s[0] == '6') {
                    guna2TextBox1.Text += $"{s}{Environment.NewLine}";
                    string clietString = "";
                    listClient = new List<ListClietnActi>();
                    for(int i = 1; i < s.Length; i++) {
                        if (s[i] != '@')
                            clietString += s[i];
                        else if (s[i] == '@') {
                            ListClietnActi f = new ListClietnActi();
                            f.nameText.Text= clietString;
                            f.Tag = clietString;
                            f.Click += lable_click;
                            clietString = "";
                            //flowLayoutPanel1.Controls.Add(f);                        
                            listClient.Add(f);
                        }
                    }
                    foreach(ListClietnActi item in listClient) {
                        flowLayoutPanel1.Controls.Add(item);
                    }
                }
            }));
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
        private void LoadListClientActi() {
            client.Send("6"+textNameF.Text);
            /*for(int i = 0; i < 20; i++) {
                ListClietnActi f = new ListClietnActi();
                f.Click += lable_click;
                flowLayoutPanel1.Controls.Add(f);
            }*/
        }

        private void lable_click(object sender, EventArgs e){
            label2.Text = (sender as ListClietnActi).Tag as string;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void circularPicture1_Click(object sender, EventArgs e)
        {

        }
        // xy ly trang chu
    }

}
