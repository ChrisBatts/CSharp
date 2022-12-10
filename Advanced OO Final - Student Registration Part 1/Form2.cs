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
    public partial class Form2 : Form
    {
        public String course;
        public static Form2 instance;

        public Form2()
        {
            
            InitializeComponent();
            instance = this;

        }

        private void form1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1.instance.Show();
        }

        private void deleteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            label1.Text = listBox1.Items.Count.ToString() + " total courses are entered (in Form1):";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }
        public void update()
        {
            label1.Text = listBox1.Items.Count.ToString() + " total courses are entered (in Form1):";

        }
        public void F2AddCourse()
        {
            course = Form1.instance.course;
            listBox1.Items.Add(course);
        }

        private void sortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false)
            {
                listBox1.Sorted = true;
            }
            else
            {
                listBox1.Sorted = false;
            }
        }

        private void deleteCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                int i = (int)MessageBox.Show("You have not selected a course to delete.", "Select a course");
            }
            else
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                label1.Text = listBox1.Items.Count.ToString() + " total courses are entered (in Form1):";
            }
        }
    }
}
