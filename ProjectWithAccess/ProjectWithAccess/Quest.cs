using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace ProjectWithAccess
{
    class Quest
    {
        OleDbConnection con;
        OleDbDataReader dr;
        OleDbCommand cmd;
        private int idquest;
        private string Question;
        private string Ans1;
        private string Ans2;
        private string Ans3;
        private string Ans4;
        private int Correct;
        private int count = 0;
        public Quest()
        {
            
        }
        /// <summary>
        /// מעדכן את השאלה
        /// </summary>
        public void UpdateQuest(string file,string table,int test,int num,string quest,string ans1,string ans2,string ans3,string ans4,int correct)
        {
            string strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file + ".accdb;" + "Persist Security Info=False";
            con = new OleDbConnection(strDb);
            con.Open();
            cmd = new OleDbCommand("UPDATE "+table+" set Quest = '"+quest+ "' ,Ans1 = '" + ans1 + "',Ans2 = '" + ans2 + "',Ans3 = '" + ans3 + "',Ans4 = '" + ans4 + "',CurrectAns="+correct+" where idtest = "+test+" and questnum = "+num+"", con);
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// מבצע ספירה של מספר השאלות במבחן
        /// </summary>
        public void Count(string file, string table,int testid)
        {
            string strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file + ".accdb;" + "Persist Security Info=False";
            con = new OleDbConnection(strDb);
            con.Open();
            cmd = new OleDbCommand("Select * from " + table + " where idtest="+testid+";", con);
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                count++;
            }
            dr.Close();
            con.Close();
        }
        /// <summary>
        /// מקבל את הפרטים של השאלה
        /// </summary>
        public void QuestNumber(string file,string table,int num,int testid)
        {
            try
            {
                string strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file + ".accdb;" + "Persist Security Info=False";
                con = new OleDbConnection(strDb);
                con.Open();
                cmd = new OleDbCommand("Select * from " + table + " where questnum=" + num + " and idtest="+testid+";", con);
                dr = cmd.ExecuteReader();
                dr.Read();
                idquest = int.Parse(dr["questnum"].ToString());
                Question = dr["Quest"].ToString();
                Ans1 = dr["Ans1"].ToString();
                Ans2 = dr["Ans2"].ToString();
                Ans3 = dr["Ans3"].ToString();
                Ans4 = dr["Ans4"].ToString();
                Correct = int.Parse(dr["CurrectAns"].ToString());
                dr.Close();
                con.Close();
            }
            catch
            {
                
            }
        }
        /// <summary>
        /// מחזיר את השאלה
        /// </summary>
        public string GetQuest()
        {
            return Question;
        }
        /// <summary>
        /// מחזיר את התשובה
        /// </summary>
        public string GetAns1()
        {
            return Ans1;
        }
        /// <summary>
        /// מחזיר את התשובה
        /// </summary>
        public string GetAns2()
        {
            return Ans2;
        }
        /// <summary>
        /// מחזיר את התשובה
        /// </summary>
        public string GetAns3()
        {
            return Ans3;
        }
        /// <summary>
        /// מחזיר את התשובה
        /// </summary>
        public string GetAns4()
        {
            return Ans4;
        }
        /// <summary>
        /// מחזיר את התשובה הנכונה
        /// </summary>
        public int GetCorrect()
        {
            return Correct;
        }
        /// <summary>
        /// מחזיר את הספירה
        /// </summary>
        public int GetCount()
        {
            return count;
        }
    }
}
