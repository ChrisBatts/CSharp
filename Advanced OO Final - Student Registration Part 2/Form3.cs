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

namespace Final_2
{
    public partial class Form3 : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\chris\\OneDrive\\Desktop\\Final_2\\data\\Database1.mdf;Integrated Security=True");
        private Boolean status;
        String oldID;
        public Form3()
        {
            InitializeComponent();
        }

        private void addBut_Click(object sender, EventArgs e)
        {
            if ((corIDTextBox.Text == "") || (credTextBox.Text == "") || (termTextBox.Text == "") || (yearTextBox.Text == ""))
            {
                MessageBox.Show("Please fill all fields", "Empty field");
                return;
            }
            SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\chris\\OneDrive\\Desktop\\Final_2\\data\\Database1.mdf;Integrated Security=True");
            SqlCommand sqlCommand = new SqlCommand("insert into Courses values ('" + corIDTextBox.Text + "', " + credTextBox.Text + ", '" + termTextBox.Text + "', " + yearTextBox.Text + ");", connection);
            connection.Open();
            int num = (int)MessageBox.Show(sqlCommand.ExecuteNonQuery().ToString() + " course added successfully ", "Course added");
            connection.Close();
            this.Close();
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            status = Form2.instance.status;
            if (status)
            {
                addBut.Enabled = false;
                updateBut.Enabled = true;
            }
            else
            {
                addBut.Enabled = true;
                updateBut.Enabled = false;
            }
        }

        private void updateBut_Click(object sender, EventArgs e)
        {
            if ((corIDTextBox.Text == "") || (credTextBox.Text == "") || (termTextBox.Text == "") || (yearTextBox.Text == ""))
            {
                MessageBox.Show("Please fill all fields", "Empty field");
                return;
            }
            oldID = Form2.instance.oldID;
            SqlCommand sqlCommand = new SqlCommand("update Courses set CourseID = '" + corIDTextBox.Text + "', Credits = " + credTextBox.Text + ", Term = '" + termTextBox.Text + "', Year = " + yearTextBox.Text + " where CourseID = '" + oldID + "';", connection);
            connection.Open();
            int num1 = (int)MessageBox.Show(sqlCommand.ExecuteNonQuery().ToString() + " course updated successfully", "Course updated");
            connection.Close();
            this.Close();
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void cancelBut_Click(object sender, EventArgs e)
        {
            this.Close();
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
