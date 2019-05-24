using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectWithAccess
{
    public partial class AdminLogin : Form
    {
        int userid,flag;
        public AdminLogin(int user)
        {
            InitializeComponent();
            this.userid = user;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin(userid);
            admin.Show();
        }

        private void AdminLogin_Load(object sender, EventArgs e)
        {
            flag = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Grades g = new Grades();
            g.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Students s = new Students();
            s.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditTests et = new EditTests();
            et.Show();
        }

        private void ציוניםToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AdminLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (flag == 0)
            {
                Login f1 = new Login();
                f1.Show();
                flag = 1;
            }
        }
    }
}
