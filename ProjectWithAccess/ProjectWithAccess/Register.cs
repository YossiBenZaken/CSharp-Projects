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
    public partial class Register : Form
    {
        private static string strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Users.accdb;" + "Persist Security Info=False";
        OleDbConnection conn = new OleDbConnection(strDb);
        OleDbDataReader dr;
        OleDbCommand cmd;
        private int flag = 0;
        public Register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                Sha sha = new Sha(password.Text);
                cmd = new OleDbCommand("Select * from Users", conn);
                dr = cmd.ExecuteReader();
                int str = 0;
                while (dr.Read())
                {
                    str = int.Parse(dr[0].ToString());
                    str++;
                }
                dr.Close();
                cmd = conn.CreateCommand();
                cmd.CommandText = "insert into Users values ("+str+",'" + username.Text + "','" + sha.GetPassword() + "',0,'" + fname.Text + "','" + lname.Text + "');";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Create User Success!");
                conn.Close();
            }
            catch(OleDbException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Register_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (flag == 0)
            {
                Opening f1 = new Opening();
                f1.Show();
                flag = 1;
            }
        }

        private void Register_Load(object sender, EventArgs e)
        {
            flag = 0;
        }
    }
}
