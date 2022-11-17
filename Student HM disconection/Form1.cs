using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;



namespace Student_HM_disconection
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
       
        public Form1()
        {
            InitializeComponent();
            string str = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            con = new SqlConnection(str);

        }
        public DataSet GetAllEmps()
        {
            da = new SqlDataAdapter("select * from student", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "std");
            return ds;

        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmps();
                DataRow row = ds.Tables["std"].NewRow();
                row["studentName"] = txtStudentName.Text;
                row["city"]=txtCity.Text;
                row["percentage"] = txtPercentage.Text;
                ds.Tables["std"].Rows.Add(row);
                int result = da.Update(ds.Tables["std"]);
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmps();
                DataRow row = ds.Tables["std"].Rows.Find(txtRollno.Text);
                if (row != null)
                {
                    row["studentName"] = txtStudentName.Text;
                    row["city"] = txtCity.Text;
                    row["percentage"] = txtPercentage.Text;

                    int result = da.Update(ds.Tables["std"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record updated");
                    }
                }
                else
                {
                    MessageBox.Show("Id not found to update");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmps();
                DataRow row = ds.Tables["std"].Rows.Find(txtRollno.Text);
                if (row != null)
                {
                    row.Delete();
                    int result = da.Update(ds.Tables["std"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record deleted");
                    }
                }
                else
                {
                    MessageBox.Show("Id not found to delete");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmps();
                DataRow row = ds.Tables["std"].Rows.Find(txtRollno.Text);
                if (row != null)
                {
                    txtStudentName.Text = row["studentName"].ToString();
                    txtCity.Text = row["city"].ToString();
                    txtPercentage.Text = row["percentage"].ToString();
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnShowallstudent_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmps();
                dataGridView1.DataSource = ds.Tables["std"]; ;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
