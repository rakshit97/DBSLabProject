using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.Types;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace DBS_LAB
{
    public partial class Form1 : Form
    {
        MySqlCommand comm = new MySqlCommand();
        MySqlConnection conn;
        MySqlDataAdapter adapter;

        public Form1()
        {
            InitializeComponent();
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            string uname = u_text.Text.ToString();
            string pass = p_text.Text.ToString();
            if(string.IsNullOrEmpty(uname))
            {
                MessageBox.Show("Enter username");
            }
            else if (string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Enter password");
            }
            else
            {
                try
                {
                    connect();
                    if (uname.Length == 9 && Regex.IsMatch(uname, @"^\d+$"))
                    {
                        comm.CommandText = "select pass from student where reg_no = " + uname;
                    }
                    else
                    {
                        comm.CommandText = "select pass from teacher where email_id = '" + uname + "'";
                    }

                    string stored_pass = (string)comm.ExecuteScalar();
                    if (pass == stored_pass)
                        MessageBox.Show("Success");
                    else
                        MessageBox.Show("Incorrect username or password");
                }
                catch(Exception err)
                {
                    MessageBox.Show("Error: " + err.Message);
                }
            }
        }

        public void connect()
        {
            string connString = "server = localhost; user id = root; password = Rakshit@97; persistsecurityinfo = True; database = test;";
            conn = new MySqlConnection(connString);
            conn.Open();
            comm.CommandType = CommandType.Text;
            comm.Connection = conn;
        }

        private void register_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
