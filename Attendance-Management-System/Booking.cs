using BusinessLogicLayer;
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
    public partial class Booking : template
    {
        public Booking()
        {
            InitializeComponent();
            this.carDD.SelectedIndexChanged += new System.EventHandler(this.carDD_SelectedIndexChanged);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
        }

        string selectedCarName;
        public DateTime selectedDate = DateTime.Today;
        public string carRent;
        public int selectedCarId;

        private void Booking_Load(object sender, EventArgs e)
        {
            BLL.Dropdown_Load("select * from carsDetails", carDD, "c_carname", "c_id");
            carDD.SelectedIndex = -1;
        }
        private void carDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (carDD.SelectedIndex != -1)
            {
                selectedCarName = carDD.Text;
                selectedCarId = (int)carDD.SelectedValue;
                carRent = BLL.FetchCarRent(selectedCarId);
                textBox6.Text = carRent;
            }
          //  MessageBox.Show("My car rent is "+carRent);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            selectedDate = dateTimePicker1.Value;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        public override void addbtn_Click(object sender, EventArgs e)
        {
            
            if(textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "")
            {
                MessageBox.Show("All fields are required");
            }
            else
            {
                string bookDate = selectedDate.ToString();
                BLL.AddBooking(textBox1.Text, selectedCarId, textBox2.Text, textBox3.Text, textBox4.Text, selectedCarName, textBox5.Text, textBox6.Text, bookDate);
            }
            
          
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home hm = new Home();
            BusinessLogicLayer.BLL.showWindow(hm, this, MDI.ActiveForm);
        }
    }
}
