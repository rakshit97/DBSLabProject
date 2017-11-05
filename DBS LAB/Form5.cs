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
    public partial class Form5 : Form
    {
        String reg_no = "";
        MySqlCommand comm = new MySqlCommand();
        MySqlConnection conn;
        MySqlDataAdapter adapter;
        DataSet ds = new DataSet();
        DataTable table;
        DataRow row;

        public Form5()
        {
            InitializeComponent();
        }

        public Form5(String reg_no)
        {
            InitializeComponent();
            this.reg_no = reg_no;
            this.FormClosed += new FormClosedEventHandler(Form5_FormClosed);
            panel1.Hide();
            panel3.Hide();
            panel2.Show();

            try
            {
                connect();
                comm.CommandText = "select reg_no, name, branch, sem, section, rating from student where reg_no=" + reg_no;
                adapter = new MySqlDataAdapter(comm.CommandText, conn);
                adapter.Fill(ds, "student_info");
                table = ds.Tables["student_info"];
                row = table.Rows[0];
                label_name.Text = (string)row[1];
                label_reg.Text = row[0].ToString();
                label_bsa.Text = (string)row[2] + ", Semester " + row[3].ToString() + ", Section " + (string)row[4];
                if (!string.IsNullOrEmpty(row[5].ToString()))
                    label_rate.Text = row[5].ToString();
                else
                    label_rate.Text = "N.A.";
            }
            catch(Exception err)
            {
                MessageBox.Show("Error: " + err.Message);
            }
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
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
            panel1.Show();

            try
            {
                connect();
                comm.CommandText = "select name as Teacher, student_review.rating as Rating, review as Review from student_review, teacher where reg_no = " + reg_no + " and teacher.email_id=student_review.email_id";
                adapter = new MySqlDataAdapter(comm.CommandText, conn);
                ds = new DataSet();
                adapter.Fill(ds, "reviews");
                table = ds.Tables["reviews"];
                int rowCount = table.Rows.Count;
                dispTable.Controls.Clear();

                for(int i=0;i<rowCount;i++)
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
                }
                conn.Close();
            }
            catch(Exception err)
            {
                MessageBox.Show("Error: " + err.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            panel3.Hide();
            panel2.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            panel2.Hide();
            panel3.Show();

            try
            {
                connect();
                comm.CommandText = "select name, email_id from teacher where email_id in(select email_id from student, section where reg_no = " + reg_no + " and student.section=section.section and student.sem=section.sem and student.branch=section.branch)";
                adapter = new MySqlDataAdapter(comm.CommandText, conn);
                adapter.Fill(ds, "teachers");
                comboBox4.DisplayMember = "name";
                comboBox4.ValueMember = "email_id";
                comboBox4.DataSource = ds.Tables["teachers"];
            }
            catch (Exception err)
            {
                MessageBox.Show("Error: " + err.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedValue == null)
                MessageBox.Show("Select a teacher");
            else
            {
                try
                {
                    connect();
                    comm.CommandText = "insert into student_review values("
                        + reg_no + ", '" + comboBox4.SelectedValue + "', "
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
