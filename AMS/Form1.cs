using BusinessLogicLayer;
namespace AMS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bll_Student newStud = new bll_Student();
            newStud.InsertStudent();
        }
    }
}
