using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance_Management_System
{
    public partial class AddCars : Form
    {
        public AddCars()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home hom = new Home();
            BusinessLogicLayer.BLL.showWindow(hom, this, MDI.ActiveForm);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "")
            {
                MessageBox.Show("All fields are required");
            }
            else
            {
                BusinessLogicLayer.BLL.AddCar(textBox1.Text,textBox2.Text,textBox3.Text,textBox4.Text,textBox5.Text);
            }
        }
    }
}
