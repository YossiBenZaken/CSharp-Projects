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
    public partial class Users : Form
    {
        int i = 0;
        Quest q = new Quest();
        double grade = 0, point;
        int test = 0,userid;
        public Users(int userid2)
        {
            InitializeComponent();
            this.userid = userid2;
        }
        private static string strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Users.accdb;" + "Persist Security Info=False";
        OleDbConnection conn = new OleDbConnection(strDb);
        OleDbDataReader dr;
        OleDbCommand cmd;
        private void Users_Load(object sender, EventArgs e)
        {
            Shuffle();
            conn.Open();
            cmd = new OleDbCommand("Select * from NameofTest", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["testname"].ToString());
            }
            dr.Close();
            conn.Close();
            conn.Open();
            cmd = new OleDbCommand("Select * from Users where id= " + userid + "", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            this.Text = "ברוך הבא "+dr["FName"] + " " + dr["LName"];
            this.RightToLeft = RightToLeft.Yes;
            dr.Close();
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            test = comboBox1.SelectedIndex;
            conn.Open();
            cmd = new OleDbCommand("Select * from grades where idtest = "+test+" and iduser= "+userid+"", conn);
            dr = cmd.ExecuteReader();
            int j = 0;
            while (dr.Read())
            {
                j++;
            }
            dr.Close();
            conn.Close();
            if (j == 0)
            {
                q.QuestNumber("Users", "Tests", i, test);
                q.Count("Users", "Tests", test);
                point = (float)100 / q.GetCount();
                label1.Text = q.GetQuest();
                radioButton1.Visible = radioButton2.Visible = radioButton3.Visible = radioButton4.Visible = button1.Visible = true;
                radioButton1.Text = q.GetAns1();
                radioButton2.Text = q.GetAns2();
                radioButton3.Text = q.GetAns3();
                radioButton4.Text = q.GetAns4();
                button2.Visible = comboBox1.Visible = label2.Visible = false;
            }
            else MessageBox.Show("עשית כבר את המבחן הזה!");
        }
        /// <summary>
        /// מערבב את התשובות
        /// </summary>
        public void Shuffle()
        {
            var rand = new Random();
            var radioButtons = new[] { radioButton1, radioButton2, radioButton3, radioButton4 };
            List<Point> list = new List<Point> { radioButton1.Location, radioButton2.Location, radioButton3.Location, radioButton4.Location };
            var shuffledLocation = list.OrderBy(x => rand.Next(list.Count)).ToList();
            for (int i = 0; i < radioButtons.Length; i++)
                radioButtons[i].Location = shuffledLocation[i];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Shuffle();
            if (radioButton1.Checked == true && q.GetCorrect() == 1)
                grade += point;
            else if (radioButton2.Checked == true && q.GetCorrect() == 2)
                grade += point;
            else if (radioButton3.Checked == true && q.GetCorrect() == 3)
                grade += point;
            else if (radioButton4.Checked == true && q.GetCorrect() == 4)
                grade += point;
            i++;
            if (i < q.GetCount())
            {
                q.QuestNumber("Users", "Tests", i,test);
                label1.Text = q.GetQuest();
                radioButton1.Text = q.GetAns1();
                radioButton1.Checked = false;
                radioButton2.Text = q.GetAns2();
                radioButton2.Checked = false;
                radioButton3.Text = q.GetAns3();
                radioButton3.Checked = false;
                radioButton4.Text = q.GetAns4();
                radioButton4.Checked = false;
            }
            else
            {
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                radioButton4.Visible = false;
                label1.Text = "Grade: " + grade.ToString();
                button3.Visible = true;
                try
                {
                    conn.Open();
                    Login login = new Login();
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "insert into grades values (" + test + ","+userid+","+grade+");";
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch
                {

                }
            }
        }
    }
}
