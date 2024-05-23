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
    public partial class BookingDetails : Form
    {
        public BookingDetails()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count - 1)
            {
               
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string bookingId = selectedRow.Cells["b_id"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home hm = new Home();
            BusinessLogicLayer.BLL.showWindow(hm, this, MDI.ActiveForm);
        }

        private void BookingDetails_Load(object sender, EventArgs e)
        {
            DataTable dt = BusinessLogicLayer.BLL.ShowBookingDetails();
            if (dt != null)
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to load booking details.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
               
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

     
                string bookingId = selectedRow.Cells["b_id"].Value.ToString();

                
                BusinessLogicLayer.BLL.DeleteBookingDetail(bookingId, dataGridView1);
            }
            else
            {
                MessageBox.Show("Please select a row to delete");
            }
        }
    }
}
