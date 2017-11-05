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
    public partial class Form4 : Form
    {
        MySqlCommand comm = new MySqlCommand();
        MySqlConnection conn;

        String[] depts = new String[] { "Aero", "Auto", "BioMed", "BioTech", "Chem", "Civil", "CSE", "ECE", "EEE", "ICE", "ICT", "Mech", "MechTron", "P&M" };


        public Form4()
        {
            InitializeComponent();
            comboBox3.Items.AddRange(depts);
            this.FormClosed += new FormClosedEventHandler(Form4_FormClosed);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String uname = textBox1.Text.ToString();
            String name = textBox2.Text.ToString();
            String pass = textBox3.Text.ToString();

            if (string.IsNullOrEmpty(uname) || !uname.EndsWith("@manipal.edu"))
                MessageBox.Show("Enter a valid email id");
            else if (string.IsNullOrEmpty(name))
                MessageBox.Show("Enter your name");
            else if (comboBox3.SelectedItem == null)
                MessageBox.Show("Select your department");
            else if (pass.Length <= 6)
                MessageBox.Show("Password must be atleast 6 characters");

            else
            {
                String dept = comboBox3.SelectedItem.ToString();
                try
                {
                    connect();
                    comm.CommandText = "insert into teacher(email_id, name, dept, pass) values('"
                        + uname + "', '" + name + "', '"
                        + dept + "', '" + pass + "')";
                    comm.ExecuteNonQuery();

                    Form6 form6 = new Form6(uname);
                    form6.Show();
                    Form[] forms = Application.OpenForms.Cast<Form>().ToArray();
                    foreach (Form thisForm in forms)
                    {
                        if (!thisForm.Name.Equals(form6.Name))
                        {
                            thisForm.Close();
                        }
                    }
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

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0)
                Application.Exit();
        }
    }
}
