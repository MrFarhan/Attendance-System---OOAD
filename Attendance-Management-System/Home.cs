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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Booking b = new Booking();
            BusinessLogicLayer.BLL.showWindow(b,this,MDI.ActiveForm);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {
            label2.Text = BusinessLogicLayer.BLL.GET_USERNAME();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddCars adc = new AddCars();
           // Form1 frm = new Form1();
            BusinessLogicLayer.BLL.showWindow(adc, this, MDI.ActiveForm);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Car_details cardet = new Car_details();
            BusinessLogicLayer.BLL.showWindow(cardet, this, MDI.ActiveForm);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            BookingDetails bookdet = new BookingDetails();
            BusinessLogicLayer.BLL.showWindow(bookdet, this, MDI.ActiveForm);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
