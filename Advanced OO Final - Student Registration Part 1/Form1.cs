using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_1
{
    public partial class Form1 : Form
    {
        public static Form1 instance;
        public String course="";
        public ListBox list = new ListBox();
        Form2 form = new Form2();


        public Form1()
        {
            InitializeComponent();

            instance = this;
        }

        private void openForm2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
 
            form.Show();
            this.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void clearOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
        }

        private void resetOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
        }

        private void clearTheCourseEnteredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void addTheCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            course = textBox1.Text;

            if (course == "")
            {
                int i = (int)MessageBox.Show("No course entered, please enter.", "No course");
            }
            else if (checkBox1.Checked == false && course.Substring(0, 3) == "CIS")
            {
                int i = (int)MessageBox.Show("You must check CIS to add this course.", "CIS course");

            }
            else if (checkBox2.Checked == false && course.Substring(0, 3) == "CNT")
            {
                int i = (int)MessageBox.Show("You must check CNT to add this course.", "CNT course");

            }
            else if (checkBox3.Checked == false && course.Substring(0, 3) == "COP")
            {
                int i = (int)MessageBox.Show("You must check COP to add this course.", "COP course");

            }
            else if (checkBox4.Checked == false && (course.Substring(0, 3) != "CIS" || course.Substring(0, 3) != "CNT" || course.Substring(0, 3) != "COP"))
            {
                int i = (int)MessageBox.Show("You must check others to add this course.", "Other course");

            }
            else
            {
                if (radioButton1.Checked == true)
                {
                    textBox1.Text = "";
                    form.F2AddCourse();
                    form.update();
                    form.Show();
                    this.Hide();
                }
                else if (radioButton2.Checked == true)
                {
                    course = course.ToUpper();
                    textBox1.Text = "";
                    form.F2AddCourse();
                    form.update();
                    form.Show();
                    this.Hide();
                }
                else if (radioButton3.Checked == true)
                {
                    course = course.ToLower();
                    textBox1.Text = "";
                    form.F2AddCourse();
                    form.update();
                    form.Show();
                    this.Hide();
                }

            }
        }


    }
}
