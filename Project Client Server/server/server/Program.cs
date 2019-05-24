using System;
using System.Data.OleDb;
using System.Net;
using System.Net.Sockets;

namespace server
{
    class Program
    {
        static string strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Phonebook.accdb;" + "Persist Security Info=False";
        static OleDbConnection conn = new OleDbConnection(strDb);
        static OleDbDataReader dr;
        static OleDbCommand cmd;
        public static string Check(string data)
        {
            string[] str = data.Split('#');
            switch (str[0])
            {
                case "SearchByName":
                    return SearchByName(str[1]);
                case "SearchByPhone":
                    return SearchByPhone(str[1]);
                case "AddContact":
                    AddContact(str[1],str[2]);
                    break;
                case "DelContact":
                    DelContact(str[1], str[2]);
                    break;
                case "UpdateContact":
                    UpdateContact(int.Parse(str[1]), str[2],str[3]);
                    break;
            }
            return null;
        }
        public static void UpdateContact(int id,string name, string phone)
        {
            conn.Open();
            cmd = new OleDbCommand("update PhoneBook set number_phone = '" + phone + "',fname = '" + name + "' where id = "+id+"", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public static void AddContact(string name,string phone)
        {
            conn.Open();
            cmd = new OleDbCommand("Insert into PhoneBook (number_phone,fname) values ('"+phone+"','"+name+"')", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public static void DelContact(string name, string phone)
        {
            conn.Open();
            cmd = new OleDbCommand("delete from PhoneBook where number_phone = '"+phone+"' and fname = '" + name + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public static string SearchByName(string name)
        {
            string str = "";
            conn.Open();
            cmd = new OleDbCommand("Select * from PhoneBook where fname like '" + name + "%'", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                str += dr[0] + "$" + dr[1] + "$" + dr[2] + "#";
            }
            dr.Close();
            conn.Close();
            return str;
        }
        public static string SearchByPhone(string phone)
        {
            string str = "";
            conn.Open();
            cmd = new OleDbCommand("Select * from PhoneBook where number_phone = '" + phone + "'", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                str += dr[0] + "$" + dr[1] + "$" + dr[2] + "#";
            }
            dr.Close();
            conn.Close();
            return str;
        }
        static void Main(string[] args)
        {
            try
            {
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                TcpListener server = new TcpListener(localAddr, port);
                server.Start();

                byte[] bytes = new byte[256];
                string data = null;
                while (true)
                {
                    Console.WriteLine("waiting to connection...");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("connected ....");
                    Console.WriteLine(client.Client.RemoteEndPoint.ToString());
                    Console.WriteLine(DateTime.Now.ToString());

                    data = null;
                    NetworkStream stream = client.GetStream();
                    int i;
                    i = stream.Read(bytes, 0, bytes.Length);
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine(data);
                    string data1 = Check(data);
                    if (data1 != null)
                    {
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data1);
                        stream.Write(msg, 0, msg.Length);
                    }
                    client.Close();


                }

            }
            catch (Exception arr)
            {
                Console.WriteLine(arr.Message);
            }
        }
    }
}
