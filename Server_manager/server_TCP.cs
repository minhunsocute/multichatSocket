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

namespace Server_manager
{
    public partial class server_TCP : Form
    {
        private string conStr = @"Data Source=LAPTOP-DI57MUOG;Initial Catalog=socket_account;Integrated Security=True";
        private SqlConnection conn;
        private SqlDataAdapter myAdapter;
        private SqlCommand comm;
        private DataSet ds;
        private DataTable dt;

        public server_TCP()
        {
            InitializeComponent();
        }
        SimpleTcpServer server;
        // xu ly tao open server
        private void BtnConnect_Click(object sender, EventArgs e)
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
            server.Stop();
            btnOUT.Enabled = false;
            BtnConnect.Enabled = true;
        }
        // kiem tra string de thuc hien cho dung
        private void checkString(string s, DataReceivedEventArgs e)
        {
            if (s[0] == '1')
            {

            }
        }
        // Nhan thong tin tu client
        //e la 1 client nhan
        private void Events_DataRecceived(object sender, DataReceivedEventArgs e)
        {
            string s = Encoding.UTF8.GetString(e.Data);
            checkString(s, e);
        }
        // reload lai data gridview khi mot cliend disconnect
        private void Events_ClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate {
                string s = "";
                int check = 0;
                for (int i = 0; i < e.IpPort.Length; i++)
                {
                    if (check == -1)
                        s += e.IpPort[i];
                    if (e.IpPort[i] == ':')
                        check = -1;
                }
                string sqlString = "";
                reLoadgridview(s, sqlString);
            });   
        }
        //reload lai datagridviw khi mot client connect
        private void Events_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            string s = "";
            int check = 0;
            for(int i = 0; i < e.IpPort.Length; i++) {
                if (check == -1)
                    s += e.IpPort[i];
                if (e.IpPort[i] == ':')
                    check = -1;
            }
            string sqlString = "";
            reLoadgridview(s, sqlString);
        }

        private void reLoadgridview(string s,string sql) {
            conn = new SqlConnection(conStr);
            string sqlString = sql;
            myAdapter = new SqlDataAdapter(sqlString, conn);
            ds = new DataSet();
            myAdapter.Fill(ds, "id");
            dt = ds.Tables["id"];
            guna2DataGridView1.DataSource = dt;
            conn.Close();
        }
    }
}
