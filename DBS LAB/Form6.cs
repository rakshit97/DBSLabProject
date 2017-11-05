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
            }
            catch (Exception err)
            {
                MessageBox.Show("Error: " + err.Message);
            }
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
            //splitContainer1.Panel2.Hide();
            panel1.Show();

            try
            {
                connect();
                comm.CommandText = "select name as Student, teacher_review.rating as Rating, review as Review from teacher_review, student where email_id = '" + email_id + "' and student.reg_no=teacher_review.reg_no";
                adapter = new MySqlDataAdapter(comm.CommandText, conn);
                adapter.Fill(ds, "reviews");
                table = ds.Tables["reviews"];
                int rowCount = table.Rows.Count;

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

                    dispTable.RowCount++;
                }
                dispTable.Size = new Size(dispTable.Size.Width, dispTable.RowCount * 30);
                conn.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show("Error: " + err.Message);
            }
        }
    }
}
