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
    public partial class Admin : Form
    {
        private static string strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Users.accdb;" + "Persist Security Info=False";
        OleDbConnection conn = new OleDbConnection(strDb);
        OleDbDataReader dr;
        OleDbCommand cmd;
        int userid;
        public Admin(int user)
        {
            InitializeComponent();
            this.userid = user;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                cmd = new OleDbCommand("Select * from NameofTest", conn);
                dr = cmd.ExecuteReader();
                int str = 0;
                while (dr.Read())
                {
                    str = int.Parse(dr[0].ToString());
                    str++;
                }
                dr.Close();
                cmd = conn.CreateCommand();
                cmd.CommandText = "insert into NameofTest values (" + str + ",'" + textBox1.Text + "');";
                cmd.ExecuteNonQuery();
                label1.Text = "שאלה:";
                textBox1.Text = "";
                button2.Visible = button3.Visible = radioButton1.Visible = radioButton2.Visible = radioButton3.Visible = radioButton4.Visible = textBox2.Visible = textBox3.Visible = textBox4.Visible = textBox5.Visible = true;
                button1.Visible = false;
                conn.Close();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new OleDbCommand("Select * from Users where id= " + userid + "", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            this.Text = "ברוך הבא " + dr["FName"] + " " + dr["LName"];
            dr.Close();
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                cmd = new OleDbCommand("Select * from NameofTest", conn);
                dr = cmd.ExecuteReader();
                int str2 = 0;
                while (dr.Read())
                {
                    str2 = int.Parse(dr[0].ToString());
                    str2++;
                }
                dr.Close();
                str2--;
                cmd = new OleDbCommand("Select * from Tests where idtest="+str2+"", conn);
                dr = cmd.ExecuteReader();
                int str = 0;
                while (dr.Read())
                {
                    str = int.Parse(dr[1].ToString());
                    str++;
                }
                dr.Close();
                int correct=0;
                if (radioButton1.Checked) correct = 1;
                else if (radioButton2.Checked) correct = 2;
                else if (radioButton3.Checked) correct = 3;
                else if (radioButton4.Checked) correct = 4;
                cmd = conn.CreateCommand();
                textBox1.Text = FixStr(textBox1.Text);
                cmd.CommandText = "insert into Tests values ("+str2+"," + str + ",'" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "',"+correct+");";
                cmd.ExecuteNonQuery();
                conn.Close();
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text =textBox5.Text= "";
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private string FixStr(string str)
        {
            string str_new ="";
            int i = 0;
            while(i<str.Length)
            {
                while (i < str.Length&&str[i] != ' ')
                    str_new += str[i++];
                if(i < str.Length) str_new += str[i];
                while (i < str.Length&&str[i] == ' ')
                    i++;
            }
            return str_new;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
