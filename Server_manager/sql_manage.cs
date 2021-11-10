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
using Guna;

namespace Server_manager
{
    public class sql_manage
    {
        private string conStr = @"Data Source=LAPTOP-DI57MUOG;Initial Catalog=MULTICHAT;Integrated Security=True";
        private SqlConnection conn;
        private SqlDataAdapter myAdapter;
        private SqlCommand comm;
        private DataSet ds;
        private DataTable dt;
        
        //reload datagridview when data change
        public void reLoadgridview(string s, string sql,DataGridView table)
        {
            conn = new SqlConnection(conStr);
            string sqlString = sql;
            myAdapter = new SqlDataAdapter(sqlString, conn);
            ds = new DataSet();
            myAdapter.Fill(ds, "id");
            dt = ds.Tables["id"];
            table.DataSource = dt;
            conn.Close();
        }
        // count number 
        public int returnNo(string username,string pass,int type) {
            int check = 0;
            conn = new SqlConnection(conStr);
            conn.Open();
            string sqlString="";
            if(type == 1) { 
                sqlString = $"SELECT COUNT(*) FROM CLIENT WHERE USERNAME ='{username}' AND PASSWORD='{pass}'";
            }
            else { 
                sqlString = $"SELECT COUNT(*) FROM CLIENT WHERE USERNAME ='{username}'";
            }
            comm = new SqlCommand(sqlString,conn);
            Int32 count = (Int32)comm.ExecuteScalar();
            conn.Close();
            if (count != 0) 
                return -1;
            return check;
        }
        public void inserAccount(string username,string pass,string name) {
            conn = new SqlConnection(conStr);
            conn.Open();
            try { 
                string sqlString = $"INSERT INTO CLIENT (USERNAME,PASSWORD,NAME_INMESSAGE,TYPE_ACTI) VALUES('{username}'," +
                                $"'{pass}',N'{name}',0)";
                comm = new SqlCommand(sqlString, conn);
                comm.ExecuteNonQuery();
            }
            catch { }
            conn.Close();
        }
        public void updateActi(string userName,int type) {
            conn = new SqlConnection(conStr);
            conn.Open();
            string sqlString = "";
            if (type == 1) { 
                sqlString = $"UPDATE CLIENT SET TYPE_ACTI = 1 WHERE USERNAME = '{userName}'";
            }
            else {
                sqlString = $"UPDATE CLIENT SET TYPE_ACTI = 0 WHERE USERNAME = '{userName}'";
            }
            comm = new SqlCommand(sqlString, conn);
            comm.ExecuteNonQuery();
            conn.Close();
        }
        public void Loaddata(DataGridView table,string ipPort,string userName,int type) {
            if (type == 0) { 
                Client c = new Client(userName,ipPort);
                server_TCP.listCList.Add(c);
            }
            foreach(Client item in server_TCP.listCList) {
                DataGridViewRow row = (DataGridViewRow)table.Rows[0].Clone();
                row.Cells[0].Value = item.Name;
                row.Cells[1].Value = item.IpPort;
                table.Rows.Add(row);
            }
        }
    }
}
