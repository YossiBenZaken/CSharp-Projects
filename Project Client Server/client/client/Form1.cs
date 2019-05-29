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
        int id;
        IPAddress addressserver;
        Int32 port;
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Connect()
        {
            if (IPAddressText.Text != "" && PortText.Text != "")
            {
                addressserver = System.Net.IPAddress.Parse(IPAddressText.Text);
                port = int.Parse(PortText.Text);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Connect();
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
                    listView1.Items.Clear();
                    for (int i = 0; i < str.Length-1; i++)
                    {
                        string[] str2 = str[i].Split('$');
                        ListViewItem item = new ListViewItem(new[]{ str2[0],str2[1],str2[2] });
                        listView1.Items.Add(item);
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
                    listView1.Items.Clear();
                    for (int i = 0; i < str.Length-1; i++)
                    {
                        string[] str2 = str[i].Split('$');
                        ListViewItem item = new ListViewItem(new[] { str2[0], str2[1], str2[2] });
                        listView1.Items.Add(item);
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

        private void Button2_Click(object sender, EventArgs e)
        {
            Connect();
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
            Connect();
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
            Connect();
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
            Connect();
            try
            {
                TcpClient client = new TcpClient(addressserver.ToString(), port);

                string message = "SearchByName##";

                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);

                string messagefromserver = "";
                data = new byte[256];
                int bytes = stream.Read(data, 0, data.Length);
                messagefromserver = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                string[] str = messagefromserver.Split('#');
                listView1.Items.Clear();
                for (int i = 0; i < str.Length-1; i++)
                {
                    string[] str2 = str[i].Split('$');
                    ListViewItem item = new ListViewItem(new[] { str2[0], str2[1], str2[2] });
                    listView1.Items.Add(item);
                }

                stream.Close();
                client.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Connect();
            if (comboBox1.SelectedIndex == 0)
            {
                try
                {
                    TcpClient client = new TcpClient(addressserver.ToString(), port);

                    string message = "SortListName#";

                    byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                    NetworkStream stream = client.GetStream();
                    stream.Write(data, 0, data.Length);

                    string messagefromserver = "";
                    data = new byte[256];
                    int bytes = stream.Read(data, 0, data.Length);
                    messagefromserver = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    string[] str = messagefromserver.Split('#');
                    listView1.Items.Clear();
                    for (int i = 0; i < str.Length-1; i++)
                    {
                        string[] str2 = str[i].Split('$');
                        ListViewItem item = new ListViewItem(new[] { str2[0], str2[1], str2[2] });
                        listView1.Items.Add(item);
                    }

                    stream.Close();
                    client.Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                try
                {
                    TcpClient client = new TcpClient(addressserver.ToString(), port);

                    string message = "SortListPhone#";

                    byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                    NetworkStream stream = client.GetStream();
                    stream.Write(data, 0, data.Length);

                    string messagefromserver = "";
                    data = new byte[256];
                    int bytes = stream.Read(data, 0, data.Length);
                    messagefromserver = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    string[] str = messagefromserver.Split('#');
                    listView1.Items.Clear();
                    for (int i = 0; i < str.Length-1; i++)
                    {
                        string[] str2 = str[i].Split('$');
                        ListViewItem item = new ListViewItem(new[] { str2[0], str2[1], str2[2] });
                        listView1.Items.Add(item);
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem index = listView1.SelectedItems[0];
            textBox1.Text = index.SubItems[2].Text;
            textBox2.Text = index.SubItems[1].Text;
            id = int.Parse(index.SubItems[0].Text);
        }
    }
}
