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
    public partial class Form2 : Form
    {
        private SqlConnection connect;
        public Boolean status;
        public static Form2 instance = new Form2();
        public String oldID;
        private String sql = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\chris\\OneDrive\\Desktop\\Final_2\\data\\Database1.mdf;Integrated Security=True";
        public Form2()
        {
            InitializeComponent();
            instance = this;
        }

        private void addCoursebut_Click(object sender, EventArgs e)
        {
            status = false;
            Form3 form3 = new Form3();
            this.Hide();
            form3.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            connect = new SqlConnection(sql);
            SqlCommand sqlCommand = new SqlCommand("select CourseID from Courses", connect);
            connect.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader[0]);
            }
                connect.Close();
        }

        private void delCorBut_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                int i = (int)MessageBox.Show("You have not selected a course to delete.", "Select a course");
            }
            else
            {
                String removeCourse = (String)listBox1.SelectedItem;
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                SqlCommand sql1 = new SqlCommand("delete from Courses where CourseID = '" + removeCourse + "';", connect);
                connect.Open();
                int num1 = sql1.ExecuteNonQuery();
                connect.Close();
                searchList.Text = "";
                int num2 = (int)MessageBox.Show(num1.ToString() + " course was deleted successfully", "Course deleted");
            }
        }

        private void upCorBut_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                int i = (int)MessageBox.Show("You have not selected a course to update.", "Select a course");
            }
            else
            {
                int selectedIndex = this.listBox1.SelectedIndex;
                oldID = this.listBox1.SelectedItem.ToString();
                status = true;
                Form3 form3 = new Form3();
                form3.Show();
                this.Hide();
            }
        }

        private void searchBut_Click(object sender, EventArgs e)
        {
            string str = serTextBox.Text;
            if (serTextBox.Text == "")
            {
                int num = (int)MessageBox.Show("You have to enter a year to search", "Enter year");
            }

            else
            {
                SqlCommand sqlCommand = new SqlCommand("select * from Courses where Year = " + str + ";", connect);
                connect.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read() == false)
                {
                    searchList.Text = "No courses found";
                }
                else
                {
                    searchList.Text = "(Course Details)\n";
                    do
                    {
                        searchList.Text = searchList.Text + sqlDataReader["CourseID"].ToString() + " (" + sqlDataReader["Credits"].ToString() + ") " + sqlDataReader["Term"].ToString() + " " + sqlDataReader["Year"].ToString() + "\n";
                    }
                    while (sqlDataReader.Read());
                }
                sqlDataReader.Close();
                connect.Close();
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex == -1)
            {
                return;
            }
            searchList.Text = "(Course Details)\n";
            string select = listBox1.SelectedItem.ToString();
            SqlCommand sqlCommand = new SqlCommand("select * from Courses where CourseID = '" + select + "';", this.connect);
            connect.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                searchList.Text = searchList.Text + sqlDataReader["CourseID"].ToString() + " (" + sqlDataReader["Credits"].ToString() + ")  " + sqlDataReader["Term"].ToString() + "  " + sqlDataReader["Year"].ToString() + "\n";
            }
            sqlDataReader.Close();
            connect.Close();
        }

        private void exitBut_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
