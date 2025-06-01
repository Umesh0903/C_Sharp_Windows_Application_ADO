using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;
using SqlCommand = Microsoft.Data.SqlClient.SqlCommand;
using SqlDataAdapter = Microsoft.Data.SqlClient.SqlDataAdapter;


namespace DatabaseConnectivity1
{
    public partial class Form1: Form
    {
        Microsoft.Data.SqlClient.SqlConnection con = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void addrecord_Click(object sender, EventArgs e)
        {
            AddData();
            getData();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
          rollnotxt.Text = nametxt.Text = citytxt.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*string connectionString = "Data Source=DESKTOP-RJ7FDJE\\SQLEXPRESS;Initial Catalog=mydb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            string query = "select * from student";
            con = new SqlConnection(connectionString);
            con.Open();

            SqlDataAdapter sda = new SqlDataAdapter(query, connectionString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "student");

            dataGridView1.DataSource = ds.Tables["student"].DefaultView;
            con.Close();*/
            getData();
          

        }

        /*public SqlConnection GetCon()
        {
            return con;
        }*/

         public void getData()
         {
            string connectionString = "Data Source=DESKTOP-RJ7FDJE\\SQLEXPRESS;Initial Catalog=mydb;Integrated Security=True;Trust Server Certificate=True";
            string query = "select * from student";
            con = new Microsoft.Data.SqlClient.SqlConnection(connectionString);
            con.Open();

            SqlDataAdapter sda = new SqlDataAdapter(query, connectionString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "student");

            dataGridView1.DataSource = ds.Tables["student"].DefaultView;

            con.Close();
         }

        public void AddData()    
        {
            string rollno, name, city;
            rollno = rollnotxt.Text;
            name = nametxt.Text;
            city = citytxt.Text;
            int rno = Convert.ToInt32(rollno);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into student values(@rollno,@name,@city)", con);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@rollno", rno);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@city", city);

            int count = cmd.ExecuteNonQuery();
            MessageBox.Show(count + " Record added successfully", "Record added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
        }

        public void UpdateData()
        {
            string rollno, name, city;
            rollno = rollnotxt.Text;
            name = nametxt.Text;
            city = citytxt.Text;
            int rno = Convert.ToInt32(rollno);
            con.Open();
            SqlCommand cmd = new SqlCommand("update student set name=@name, city=@city where rollno=@rollno", con);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@rollno", rno);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@city", city);

            int count = cmd.ExecuteNonQuery();
            MessageBox.Show(count + " Record updated successfully", "Record updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();

        }

        public void DeleteData()
        {
            con.Open();
            int rollno = Convert.ToInt32(rollnotxt.Text);
            SqlCommand cmd = new SqlCommand("delete from student where rollno=@rollno", con);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@rollno", rollno);

            int count = cmd.ExecuteNonQuery();
            MessageBox.Show(count + " Record deleted successfully", "Record deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void updatebtn_Click(object sender, EventArgs e)  //update button
        {
            UpdateData();
        }

        private void deletebtn_Click(object sender, EventArgs e)    //delete button
        {
            DeleteData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rollnotxt.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            nametxt.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            citytxt.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
            rollnotxt.ReadOnly = true;

        }
    }
}
