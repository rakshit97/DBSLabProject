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
            //splitContainer1.Panel2.Hide();
            panel1.Show();

            try
            {
                connect();
                comm.CommandText = "select name as Teacher, student_review.rating as Rating, review as Review from student_review, teacher where reg_no = " + reg_no + " and teacher.email_id=student_review.email_id";
                adapter = new MySqlDataAdapter(comm.CommandText, conn);
                adapter.Fill(ds, "reviews");
                table = ds.Tables["reviews"];
                int rowCount = table.Rows.Count;

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

                    dispTable.RowCount++;
                }
                dispTable.Size = new Size(dispTable.Size.Width, dispTable.RowCount * 30);
                conn.Close();
            }
            catch(Exception err)
            {
                MessageBox.Show("Error: " + err.Message);
            }
        }
    }
}
