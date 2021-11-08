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
        public int returnNo(string username,string pass) {
            int check = 0;
            conn = new SqlConnection(conStr);
            conn.Open();
            string sqlString = $"SELECT COUNT(*) FROM CLIENT WHERE USERNAME ='{username}' AND PASSWORD='{pass}'";
            comm = new SqlCommand(sqlString,conn);
            Int32 count = (Int32)comm.ExecuteScalar();
            conn.Close();
            if (count != 0) 
                return -1;
            return check;
        }
    }
}
