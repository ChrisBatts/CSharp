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
    public partial class Form1 : Form
    {
        public static Form1 instance;
        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\chris\\OneDrive\\Desktop\\Final_2\\data\\Database1.mdf;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
            instance = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("select count(*) from Courses", connection);
            connection.Open();
            courseCountLab.Text = sqlCommand.ExecuteScalar().ToString();
            connection.Close();
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

    }
}
