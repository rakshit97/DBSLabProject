using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.Types;
using MySql.Data.MySqlClient;

namespace DBS_LAB
{
    public partial class Form3 : Form
    {
        MySqlCommand comm = new MySqlCommand();
        MySqlConnection conn;
        MySqlDataAdapter adapter;

        String[] sections = new String[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        String[] sems = new String[] { "1", "2", "3", "4", "5", "6", "7", "8" };
        String[] branches = new String[] { "Aero", "Auto", "BioMed", "BioTech", "Chem", "Civil", "CCE", "CSE", "ECE", "EEE", "ICE", "IP", "IT", "Mech", "MechTron", "P&M" };


        public Form3()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(sections);
            comboBox2.Items.AddRange(sems);
            comboBox3.Items.AddRange(branches);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String uname = textBox1.Text.ToString();
            String name = textBox2.Text.ToString();

            if (uname.Length != 9 || !Regex.IsMatch(uname, @"^\d+$"))
                MessageBox.Show("Enter a valid registration number");
            else if (string.IsNullOrEmpty(name))
                MessageBox.Show("Enter your name");
            else if (comboBox1.SelectedItem == null)
                MessageBox.Show("Select your section");
            else if (comboBox2.SelectedItem == null)
                MessageBox.Show("Select your semester");
            else if (comboBox3.SelectedItem == null)
                MessageBox.Show("Select your branch");

            else
            {
                String section = comboBox1.SelectedItem.ToString();
                String sem = comboBox2.SelectedItem.ToString();
                String branch = comboBox3.SelectedItem.ToString();
                try
                {
                    connect();
                    comm.CommandText = "insert into student(reg_no, name, section, sem, branch) values("
                        + uname + ", '" + name + "', '"
                        + section + "', " + sem + ", '"
                        + branch + "')";
                    comm.ExecuteNonQuery();

                }
                catch (Exception err)
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
    }
}
