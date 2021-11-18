using SimpleTcp;
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
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;

namespace Server_manager
{
    public partial class server_TCP : Form
    {
        public server_TCP()
        {
            InitializeComponent();
            textIP.Text = getIPv4();
        }
        public static List<Client> listCList = new List<Client>();
        string ipPortConnect = "";
        SimpleTcpServer server;
        // xu ly tao open server
        private void BtnConnect_Click(object sender, EventArgs   e)
        {
            if (!string.IsNullOrEmpty(textName.Text) && !string.IsNullOrEmpty(textPort.Text)) {
                server = new SimpleTcpServer($"{textIP.Text}:{textPort.Text}");
                server.Events.ClientConnected += Events_ClientConnected;
                server.Events.ClientDisconnected += Events_ClientDisconnected;
                server.Events.DataReceived += Events_DataRecceived;
                server.Start();
                btnOUT.Enabled = true;
                BtnConnect.Enabled = false;
            }
            else 
                MessageBox.Show("NAME OR PORT TEXT IS NULL","MESSAGE",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
        // xu ly dung server
        private void btnOUT_Click(object sender, EventArgs e)
        {
            textName.Text = string.Empty;
            textPort.Text = string.Empty;
            //foreach (Client item in listCList) {
            //}
            foreach(Client item in listCList) {
                server.Send($"{textIP.Text}:{item.IpPort}", "1Disconnect");
            }
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView1.Refresh();
            server.Stop();
            btnOUT.Enabled = false;
            BtnConnect.Enabled = true;
            (new sql_manage()).refreshAllData();
        }
        private void LoadDataGrid(string username,sql_manage f) {
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView1.Refresh();
            f.updateActi(username,1);
            int inDex = ipPortConnect.IndexOf(':');
            f.Loaddata(guna2DataGridView1, ipPortConnect.Substring(inDex+1), username,0);
        }
        public void removeDataGrid(string username,sql_manage f) {
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView1.Refresh();
            int i = 0;
            foreach(Client item in listCList) {
                if (item.Name == username) break;
                i++;
            }
            listCList.RemoveAt(i);
            f.Loaddata(guna2DataGridView1, ipPortConnect, username, 1);
        }
        // kiem tra string de thuc hien cho dung
        private void checkString(string s, DataReceivedEventArgs e)
        {
            sql_manage f = new sql_manage();
            //guna2TextBox1.Text = string.Empty;
            //kiem tra dang nhap thanh cong
            if (s[0] == '1'){
                int i = 1;
                string username = "";string password = "";
                while (true) {
                    if(s[i]!='@')username += s[i];
                    if (s[i] == '@') {i++;break;}
                    i++;
                }
                while (i < s.Length) { 
                    password += s[i];i++;
                }
                int check = f.returnNo(username,password,1);
                if (check == -1) {
                    server.Send(e.IpPort, "success");
                }
                else
                    server.Send(e.IpPort, "unsuccess");
            }
            // kiem tra dang ky thanh cong
            else if (s[0] == '2') {
                int i = 1;
                string username = ""; string password = "";string name = "";
                while (true){
                    if (s[i] != '@') username += s[i];
                    if (s[i] == '@'){i++; break;}i++;
                }
                while (true)
                {
                    if (s[i] != '@') password += s[i];
                    if (s[i] == '@'){
                        i++; break;
                    }i++;
                }
                while (i < s.Length){
                    name += s[i]; i++;
                }
                int check = f.returnNo(username, password,2);
                if (check == 0) {
                    f.inserAccount(username, password, name);
                    server.Send(e.IpPort, "success");
                }
                else
                    server.Send(e.IpPort, "unsuccess");
            }
            //kiem tra nguoi dung out
            else if(s[0]=='3') {
                string userName = s.Substring(1);
                f.updateActi(userName, 0);
                removeDataGrid(userName, f);
                string sendString = f.getListClientActi(s.Substring(1));
                foreach (Client item in listCList)
                {
                    server.Send($"{textIP.Text}:{item.IpPort}", sendString);
                }
            }
            //kiemm tra MainForm duoc load
            else if (s[0] == '4') {
                string userName = "";
                for (int i = 1; i < s.Length; i++)
                    userName += s[i];
                LoadDataGrid(userName, f);
                string sendString = f.getListClientActi(s.Substring(1));
                foreach (Client item in listCList) {
                    server.Send($"{textIP.Text}:{item.IpPort}", sendString);
                }
            }
            else if (s[0] == '7') {
                int Index = s.IndexOf('@');
                f.updateAvt(s.Substring(1,Index-1),s.Substring(Index+1));
            }   
            else if (s[0] == '8') {
                int Index = s.IndexOf('@');
                string username = s.Substring(1, Index - 1);
                string port = e.IpPort.Substring(e.IpPort.IndexOf(':')+1);
                string nameSend = "";
                guna2TextBox1.Text = port;
                foreach(Client item in listCList) { 
                    if($"{textIP.Text}:{item.IpPort}" == e.IpPort) {
                        f.InsertMess(item.Name, username, s.Substring(Index + 1));
                        nameSend = item.Name;
                        break;
                    }
                }
                foreach(Client item in listCList) { 
                    if(username == item.Name) {
                        server.Send($"{textIP.Text}:{item.IpPort}", $"8{nameSend}@{s.Substring(Index+1)}");
                    }
                    else continue;
                }
            }
            else if (s[0] == '9') {
                int Index = s.IndexOf('@');
                string sendString = f.LoadMess(s.Substring(1, Index - 1), s.Substring(Index + 1));
                server.Send(e.IpPort, sendString);
            }
        }
        // Nhan thong tin tu client
        //e la 1 client nhan
        private void Events_DataRecceived(object sender, DataReceivedEventArgs e)
        {
            string s = Encoding.UTF8.GetString(e.Data);
            guna2TextBox1.Text = string.Empty;
            guna2TextBox1.Text = $"{e.IpPort}:{s}";
            checkString(s,e);
        }
        // reload lai data gridview khi mot cliend disconnect
        private void Events_ClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate {
                ipPortConnect = e.IpPort;
            });   
        }
        //reload lai datagridviw khi mot client connect
        private void Events_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            ipPortConnect = string.Empty;
            ipPortConnect = e.IpPort;
        }
        // get IPv4
        public string getIPv4(){
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostByName(hostName).AddressList[3].ToString();
            return myIP;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            server.Send(ipPortConnect, guna2TextBox1.Text);
        }

        // xu ly khi server dung dot ngot
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            foreach(Client item in listCList) {
                server.Send($"{textIP.Text}:{item.IpPort}", "1Disconnect");
            }
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView1.Refresh();
            server.Stop();
            (new sql_manage()).refreshAllData();
        }
    }
}
