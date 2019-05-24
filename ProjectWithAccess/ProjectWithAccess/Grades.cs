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
    public partial class Grades : Form
    {
        private static string strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Users.accdb;" + "Persist Security Info=False";
        OleDbConnection conn = new OleDbConnection(strDb);
        OleDbDataReader dr;
        OleDbCommand cmd;
        public Grades()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                label2.Text = label3.Text = "";
                conn.Open();
                cmd = new OleDbCommand("SELECT Users.Username, grades.grade FROM Users INNER JOIN(NameofTest INNER JOIN grades ON NameofTest.testid = grades.idtest) ON Users.id = grades.iduser WHERE(((NameofTest.testid) ="+comboBox1.SelectedIndex+"));", conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    label2.Text += "תלמיד: " + dr[0].ToString()+"\n";
                    label3.Text += "ציון: " + dr[1].ToString() + "\n";
                }
                dr.Close();
                conn.Close();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Grades_Load(object sender, EventArgs e)
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

        private void ציוניםToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
