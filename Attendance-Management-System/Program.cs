using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance_Management_System
{
      static class Program
      {
          /// <summary>
          /// The main entry point for the application.
          /// </summary>
          [STAThread]
          static void Main()
          {
            if (!isStillRunning())
            {
                SplashScreen splash = new SplashScreen();
                splash.Show();
                splash.Update();

                
                System.Threading.Thread.Sleep(3000); 
                splash.Close();
               // Application.EnableVisualStyles();
               // Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MDI());
            }
            else
            {
                MessageBox.Show("Application is already running",
                   "Application Halted", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
            }        
          }

        static bool isStillRunning()
        {
            string processName = Process.GetCurrentProcess().MainModule.ModuleName;
            ManagementObjectSearcher mos = new ManagementObjectSearcher();
            mos.Query.QueryString = @"SELECT * FROM Win32_Process WHERE Name = '" + processName + @"'";
            if (mos.Get().Count > 1)
            {
                return true;
            }
            else
                return false;
        }
    }

}
