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
    public partial class EditTests : Form
    {
        private static string strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Users.accdb;" + "Persist Security Info=False";
        OleDbConnection conn = new OleDbConnection(strDb);
        OleDbDataReader dr;
        OleDbCommand cmd;
        Quest q = new Quest();
        int test, i,correct;
        public EditTests()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) correct = 1;
            else if (radioButton2.Checked == true) correct = 2;
            else if (radioButton3.Checked == true) correct = 3;
            else if (radioButton4.Checked == true) correct = 4;
            q.UpdateQuest("Users", "Tests", test, i, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, correct);
            i++;
            q.QuestNumber("Users", "Tests", i, test);
            q.Count("Users", "Tests", test);
            if (q.GetCorrect() == 1) radioButton1.Checked = true;
            else if (q.GetCorrect() == 2) radioButton2.Checked = true;
            else if (q.GetCorrect() == 3) radioButton2.Checked = true;
            else if (q.GetCorrect() == 4) radioButton2.Checked = true;
            textBox1.Text = q.GetQuest();
            textBox2.Text = q.GetAns1();
            textBox3.Text = q.GetAns2();
            textBox4.Text = q.GetAns3();
            textBox5.Text = q.GetAns4();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditTests_Load(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new OleDbCommand("Select * from NameofTest", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["testname"].ToString());
            }
            dr.Close();
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            i = 0;
            test = comboBox1.SelectedIndex;
            q.QuestNumber("Users", "Tests", i, test);
            q.Count("Users", "Tests", test);
            if (q.GetCorrect() == 1) radioButton1.Checked = true;
            else if (q.GetCorrect() == 2) radioButton2.Checked = true;
            else if (q.GetCorrect() == 3) radioButton2.Checked = true;
            else if (q.GetCorrect() == 4) radioButton2.Checked = true;
            textBox1.Text = q.GetQuest();
            textBox2.Text = q.GetAns1();
            textBox3.Text = q.GetAns2();
            textBox4.Text = q.GetAns3();
            textBox5.Text = q.GetAns4();
            button2.Enabled = true;
        }
    }
}
