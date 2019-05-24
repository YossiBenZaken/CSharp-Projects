using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ProjectWithAccess
{
    public partial class Students : Form
    {
        string strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Users.accdb;" + "Persist Security Info=False";
        OleDbConnection con;
        OleDbDataReader dr;
        OleDbCommand cmd;
        public Students()
        {
            InitializeComponent();
        }

        private void Students_Load(object sender, EventArgs e)
        {
            con = new OleDbConnection(strDb);
            con.Open();
            cmd = new OleDbCommand("Select * from Users;", con);
            dr = cmd.ExecuteReader();
            label4.Text = label5.Text= label6.Text= "";
            while (dr.Read())
            {
                label4.Text += dr["Username"]+"\n";
                label5.Text += dr["LName"] + "\n";
                label6.Text += dr["FName"] + "\n";
            }
            dr.Close();
            con.Close();
        }

        private void ציוניםToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
