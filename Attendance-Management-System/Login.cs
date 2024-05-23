using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogicLayer;

namespace Attendance_Management_System
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (usernameTxt.Text != "" && passwordTxt.Text != "")
            {
                if (BLL.isAuthenticate(usernameTxt.Text, passwordTxt.Text))
                {
                    Home hom = new Home();
                    BLL.showWindow(hom, this, MDI.ActiveForm);
                }
                else
                {
                    MessageBox.Show("Username or Password is incorrect");
                }
            }

            else
            {
                MessageBox.Show("All fields are required");
            }
          
        }
    }
}
