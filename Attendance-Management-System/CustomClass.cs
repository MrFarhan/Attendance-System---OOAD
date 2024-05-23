using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance_Management_System
{
    class CustomClass
    {
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static void showWindow(Form openWin, Form closeWin, Form MDIi)
        {
            closeWin.Close();
            openWin.WindowState = FormWindowState.Maximized;
            openWin.MdiParent = MDIi;
            openWin.Show();
        }
        public static DialogResult ShowMSG(string msg, string heading, string type)
        {
            if (type == "Sucecess")
            {
                return MessageBox.Show(msg, heading, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                return MessageBox.Show(msg, heading, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void disable_reset(Panel panel)
        {
            foreach (Control C in panel.Controls)
            {
                if (C is TextBox)
                {
                    TextBox t = (TextBox)C;
                    t.Enabled = false;
                    t.Text = "";
                }
                if (C is ComboBox)
                {
                    ComboBox cb = (ComboBox)C;
                    cb.Enabled = false;
                    cb.SelectedIndex = -1;
                }
                if (C is RadioButton)
                {
                    RadioButton rb = (RadioButton)C;
                    rb.Enabled = false;
                    rb.Checked = false;
                }
                if (C is CheckBox)
                {
                    CheckBox cb = (CheckBox)C;
                    cb.Enabled = false;
                    cb.Checked = false;
                }
                if (C is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)C;
                    dtp.Enabled = false;
                    dtp.Value = DateTime.Now;
                }
                if (C is Button)
                {
                    Button b = (Button)C;
                    b.Enabled = false;
                }
            }
        }

        public static void disable(Panel p)
        {
            foreach (Control C in p.Controls)
            {
                if (C is TextBox)
                {
                    TextBox t = (TextBox)C;
                    t.Enabled = false;
                }
                if (C is ComboBox)
                {
                    ComboBox cb = (ComboBox)C;
                    cb.Enabled = false;
                }
                if (C is RadioButton)
                {
                    RadioButton rb = (RadioButton)C;
                    rb.Enabled = false;
                }
                if (C is CheckBox)
                {
                    CheckBox cb = (CheckBox)C;
                    cb.Enabled = false;
                }
                if (C is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)C;
                    dtp.Enabled = false;
                }
                if (C is Button)
                {
                    Button b = (Button)C;
                    b.Enabled = false;
                }
            }
        }

        public static void enable_reset(Panel p)
        {
            foreach (Control C in p.Controls)
            {
                if (C is TextBox)
                {
                    TextBox t = (TextBox)C;
                    t.Enabled = true;
                    t.Text = "";
                }
                if (C is ComboBox)
                {
                    ComboBox cb = (ComboBox)C;
                    cb.Enabled = true;
                    cb.SelectedIndex = -1;
                }
                if (C is RadioButton)
                {
                    RadioButton rb = (RadioButton)C;
                    rb.Enabled = true;
                    rb.Checked = false;
                }
                if (C is CheckBox)
                {
                    CheckBox cb = (CheckBox)C;
                    cb.Enabled = true;
                    cb.Checked = false;
                }
                if (C is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)C;
                    dtp.Enabled = true;
                    dtp.Value = DateTime.Now;
                }
                if (C is Button)
                {
                    Button b = (Button)C;
                    b.Enabled = true;
                }
            }
        }

        public static void enable_reset(ComboBox gb)
        {
            foreach (Control C in gb.Controls)
            {
                if (C is TextBox)
                {
                    TextBox t = (TextBox)C;
                    t.Enabled = true;
                    t.Text = "";
                }
                if (C is ComboBox)
                {
                    ComboBox cb = (ComboBox)C;
                    cb.Enabled = true;
                    cb.SelectedIndex = -1;
                }
                if (C is RadioButton)
                {
                    RadioButton rb = (RadioButton)C;
                    rb.Enabled = true;
                    rb.Checked = false;
                }
                if (C is CheckBox)
                {
                    CheckBox cb = (CheckBox)C;
                    cb.Enabled = true;
                    cb.Checked = false;
                }
                if (C is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)C;
                    dtp.Enabled = true;
                    dtp.Value = DateTime.Now;
                }
                if (C is Button)
                {
                    Button b = (Button)C;
                    b.Enabled = true;
                }
            }
        }

        public static void enable(Panel p)
        {
            foreach (Control C in p.Controls)
            {
                if (C is TextBox)
                {
                    TextBox t = (TextBox)C;
                    t.Enabled = true;
                }
                if (C is ComboBox)
                {
                    ComboBox cb = (ComboBox)C;
                    cb.Enabled = true;
                }
                if (C is RadioButton)
                {
                    RadioButton rb = (RadioButton)C;
                    rb.Enabled = true;
                }
                if (C is CheckBox)
                {
                    CheckBox cb = (CheckBox)C;
                    cb.Enabled = true;
                }
                if (C is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)C;
                    dtp.Enabled = true;
                }
                if (C is Button)
                {
                    Button b = (Button)C;
                    b.Enabled = true;
                }
            }
        }

    }
}
