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
    public partial class Login : Form
    {
        private static string strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Users.accdb;" + "Persist Security Info=False";
        OleDbConnection conn = new OleDbConnection(strDb);
        OleDbDataReader dr;
        OleDbCommand cmd;
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                cmd = new OleDbCommand("Select * from Users where Username='" + username.Text + "'", conn);
                dr = cmd.ExecuteReader();
                Sha sha = new Sha(password.Text);
                dr.Read();
                if (dr["Password"].ToString() == sha.GetPassword())
                {
                    if (int.Parse(dr["Permission"].ToString())==1)
                    {
                        AdminLogin admin = new AdminLogin(int.Parse(dr["id"].ToString()));
                        admin.Show();
                        this.Close();
                    }
                    else
                    {
                        Users user = new Users(int.Parse(dr["id"].ToString()));
                        user.Show();
                        this.Close();
                    }
                }
                else
                    MessageBox.Show("Error!");
                dr.Close();
                conn.Close();
            }
            catch(OleDbException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
