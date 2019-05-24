using System;
//
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;


namespace client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        int id;
        Int32 port = 13000;
        IPAddress addressserver = IPAddress.Parse("127.0.0.1");
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                try
                {
                    TcpClient client = new TcpClient(addressserver.ToString(), port);

                    string message = "SearchByName#" + nametext.Text + "#";

                    byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                    NetworkStream stream = client.GetStream();
                    stream.Write(data, 0, data.Length);

                    string messagefromserver = "";
                    data = new byte[256];
                    int bytes = stream.Read(data, 0, data.Length);
                    messagefromserver = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    string[] str = messagefromserver.Split('#');
                    listBox1.Items.Clear();
                    for (int i = 0; i < str.Length; i++)
                    {
                        listBox1.Items.Add(str[i].Replace('$', ' '));
                    }

                    stream.Close();
                    client.Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            else if(comboBox1.SelectedIndex == 1)
            {
                try
                {
                    TcpClient client = new TcpClient(addressserver.ToString(), port);

                    string message = "SearchByPhone#" + phonetext.Text + "#";

                    byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                    NetworkStream stream = client.GetStream();
                    stream.Write(data, 0, data.Length);

                    string messagefromserver = "";
                    data = new byte[256];
                    int bytes = stream.Read(data, 0, data.Length);
                    messagefromserver = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    string[] str = messagefromserver.Split('#');
                    listBox1.Items.Clear();
                    for (int i = 0; i < str.Length; i++)
                    {
                        listBox1.Items.Add(str[i].Replace('$', ' '));
                    }

                    stream.Close();
                    client.Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            else
            {
                MessageBox.Show("You must select from combo box!");
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] str = listBox1.SelectedItem.ToString().Split(' ');
            textBox1.Text = str[2];
            textBox2.Text = str[1];
            id = int.Parse(str[0]);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient(addressserver.ToString(), port);

                string message = "AddContact#" + textBox1.Text + "#"+textBox2.Text+"#";

                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
                client.Close();
                MessageBox.Show(textBox1.Text + " with the number: " + textBox2.Text + " add to Phonebook!");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient(addressserver.ToString(), port);

                string message = "DelContact#" + textBox1.Text + "#" + textBox2.Text + "#";

                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
                client.Close();
                MessageBox.Show(textBox1.Text + " with the number: " + textBox2.Text + " remove from Phonebook!");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient(addressserver.ToString(), port);

                string message = "UpdateContact#" +id+"#"+ textBox1.Text + "#" + textBox2.Text + "#";

                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
                client.Close();
                MessageBox.Show(textBox1.Text + " with the number: " + textBox2.Text + " updated!");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient(addressserver.ToString(), port);

                string message = "SearchByName#" + nametext.Text + "#";

                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);

                string messagefromserver = "";
                data = new byte[256];
                int bytes = stream.Read(data, 0, data.Length);
                messagefromserver = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                string[] str = messagefromserver.Split('#');
                listBox1.Items.Clear();
                for (int i = 0; i < str.Length; i++)
                {
                    listBox1.Items.Add(str[i].Replace('$', ' '));
                }

                stream.Close();
                client.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
