using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace DBS_LAB
{
    public partial class Form6 : Form
    {
        String email_id = "";
        MySqlCommand comm = new MySqlCommand();
        MySqlConnection conn;
        MySqlDataAdapter adapter;
        DataSet ds = new DataSet();
        DataTable table;
        DataRow row;

        String[] sections = new String[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        String[] sems = new String[] { "1", "2", "3", "4", "5", "6", "7", "8" };
        String[] branches = new String[] { "Aero", "Auto", "BioMed", "BioTech", "Chem", "Civil", "CCE", "CSE", "ECE", "EEE", "ICE", "IP", "IT", "Mech", "MechTron", "P&M" };

        public Form6()
        {
            InitializeComponent();
        }

        public Form6(String email_id)
        {
            InitializeComponent();
            this.email_id = email_id;
            this.FormClosed += new FormClosedEventHandler(Form6_FormClosed);
            panel1.Hide();
            panel2.Hide();
            panel4.Hide();
            panel3.Show();

            comboBox1.Items.AddRange(sections);
            comboBox2.Items.AddRange(sems);
            comboBox3.Items.AddRange(branches);

            try
            {
                connect();
                comm.CommandText = "select email_id, name, dept, rating from teacher where email_id='" + email_id + "'";
                adapter = new MySqlDataAdapter(comm.CommandText, conn);
                adapter.Fill(ds, "student_info");
                table = ds.Tables["student_info"];
                row = table.Rows[0];
                label_name.Text = (string)row[1];
                label_reg.Text = row[0].ToString();
                label_bsa.Text = (string)row[2];
                if (!string.IsNullOrEmpty(row[3].ToString()))
                    label_rate.Text = row[3].ToString();
                else
                    label_rate.Text = "N.A.";
                conn.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show("Error: " + err.Message);
            }

            getClasses();
        }

        private void Form6_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0)
                Application.Exit();
        }

        private void connect()
        {
            string connString = "server = localhost; user id = root; password = Rakshit@97; persistsecurityinfo = True; database = test;";
            conn = new MySqlConnection(connString);
            conn.Open();
            comm.CommandType = CommandType.Text;
            comm.Connection = conn;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
            panel1.Show();

            try
            {
                connect();
                comm.CommandText = "select name, teacher_review.rating, review from teacher_review, student where email_id = '" + email_id + "' and student.reg_no=teacher_review.reg_no";
                adapter = new MySqlDataAdapter(comm.CommandText, conn);
                ds = new DataSet();
                adapter.Fill(ds, "reviews");
                table = ds.Tables["reviews"];
                int rowCount = table.Rows.Count;
                dispTable.Controls.Clear();

                for (int i = 0; i < rowCount; i++)
                {
                    row = table.Rows[i];

                    Label l1 = new Label();
                    l1.TextAlign = ContentAlignment.MiddleCenter;
                    l1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                    l1.Text = (string)row[0];
                    dispTable.Controls.Add(l1, 0, i);

                    Label l2 = new Label();
                    l2.TextAlign = ContentAlignment.MiddleCenter;
                    l2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                    l2.Text = (string)row[2];
                    dispTable.Controls.Add(l2, 1, i);

                    Label l3 = new Label();
                    l3.TextAlign = ContentAlignment.MiddleCenter;
                    l3.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                    l3.Text = row[1].ToString();
                    dispTable.Controls.Add(l3, 2, i);

                    //dispTable.RowCount++;
                }
                //dispTable.Size = new Size(dispTable.Size.Width, dispTable.RowCount * 30);
                conn.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show("Error: " + err.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
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
                    comm.CommandText = "insert into section values('"
                        + section + "', " + sem + ", '"
                        + branch + "', '" + email_id + "')";
                    comm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Class added!");
                }
                catch (Exception err)
                {
                    MessageBox.Show("Error: " + err.Message);
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            panel3.Hide();
            panel4.Hide();
            panel2.Show();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            panel2.Hide();
            panel4.Hide();
            panel3.Show();
            getClasses();
        }

        private void getClasses()
        {
            try
            {
                connect();
                comm.CommandText = "select branch, sem, section from section where email_id = '" + email_id + "'";
                adapter = new MySqlDataAdapter(comm.CommandText, conn);
                ds = new DataSet();
                adapter.Fill(ds, "classes");
                table = ds.Tables["classes"];
                int rowCount = table.Rows.Count;
                classTable.Controls.Clear();

                for (int i = 0; i < rowCount; i++)
                {
                    row = table.Rows[i];

                    Label l1 = new Label();
                    l1.TextAlign = ContentAlignment.MiddleCenter;
                    l1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                    l1.Text = (string)row[0];
                    classTable.Controls.Add(l1, 0, i);

                    Label l2 = new Label();
                    l2.TextAlign = ContentAlignment.MiddleCenter;
                    l2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                    l2.Text = row[1].ToString();
                    classTable.Controls.Add(l2, 1, i);

                    Label l3 = new Label();
                    l3.TextAlign = ContentAlignment.MiddleCenter;
                    l3.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                    l3.Text = (string)row[2];
                    classTable.Controls.Add(l3, 2, i);

                    //dispTable.RowCount++;
                }
                //dispTable.Size = new Size(dispTable.Size.Width, dispTable.RowCount * 30);
                conn.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show("Error: " + err.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            panel2.Hide();
            panel3.Hide();
            panel4.Show();

            try
            {
                connect();
                comm.CommandText = "select reg_no, name from student, section where email_id = '" + email_id + "' and student.section=section.section and student.sem=section.sem and student.branch=section.branch";
                adapter = new MySqlDataAdapter(comm.CommandText, conn);
                adapter.Fill(ds, "students");
                comboBox4.DisplayMember = "name";
                comboBox4.ValueMember = "reg_no";
                comboBox4.DataSource = ds.Tables["students"];
            }
            catch (Exception err)
            {
                MessageBox.Show("Error: " + err.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedValue== null)
                MessageBox.Show("Select a student");
            else
            {
                try
                {
                    connect();
                    comm.CommandText = "insert into teacher_review values('"
                        + email_id + "', " + comboBox4.SelectedValue + ", "
                        + numUD.Value + ", '" + text_review.Text.ToString() + "')";
                    comm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Review added!");
                }
                catch (Exception err)
                {
                    MessageBox.Show("Error: " + err.Message);
                }
            }
        }
    }
}
