using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.UI.MobileControls;
using System.Windows;
using System.Windows.Forms;
//using System.Windows.Forms;

namespace DataAccessLayer
{
    public class DAL
    {
        private static System.Collections.Hashtable SqlparamCache = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());

        private SqlCommand DbCommand = new SqlCommand();
        private SqlDataAdapter DtAdapter = new SqlDataAdapter();
        private DataSet SqlDataSet = new DataSet();
        private DataTable SqlTable = new System.Data.DataTable();
        private static string path;
        private static string connectionString()
        {
            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\connectiondetails";
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else
            {
                return "";
            }
        }
        private static string connection;
        public static SqlConnection con = new SqlConnection(connectionString());
        /* public static void saveconnection(string servertxt, string database, Form openwindow, Form closedwindow, Form parentwindow, string userid = null, string password = null)
         {
               connection = "Data Source=" + servertxt + ";Initial Catalog=" + database + ";User ID=" + userid + ";Password=" + password + ";MultipleActiveResultSets = true;";
                 string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\connectiondetails";
                 File.WriteAllText(path, connection);

             con.Open();
             MessageBox.Show("Successfully Connected");
             closedwindow.Close();
             openwindow.MdiParent = parentwindow;
             openwindow.WindowState = FormWindowState.Maximized;
             openwindow.Show();

         }*/


        //public static void saveconnection(string servertxt, string database, Form openwindow, Form closedwindow, Form parentwindow, string userid = null, string password = null)
        //{
        //    string connection = "Data Source=" + servertxt + ";Initial Catalog=" + database + ";";

        //    // Add UserID and Password if provided
        //    if (!string.IsNullOrEmpty(userid) && !string.IsNullOrEmpty(password))
        //    {
        //        connection += "User ID=" + userid + ";Password=" + password + ";";
        //    }

        //    // Add MultipleActiveResultSets option
        //    connection += "MultipleActiveResultSets=true;";

        //    try
        //    {
        //        // Write connection string to file
        //        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\connectiondetails";
        //        File.WriteAllText(path, connection);

        //        // Instantiate and open the SqlConnection
        //        using (SqlConnection con = new SqlConnection(connection))
        //        {
        //            con.Open();
        //            MessageBox.Show("Successfully Connected");

        //            // Close the window
        //            closedwindow.Close();

        //            // Set open window as MDI child
        //            openwindow.MdiParent = parentwindow;
        //            openwindow.WindowState = FormWindowState.Maximized;
        //            openwindow.Show();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle connection error
        //        MessageBox.Show("Connection failed: " + ex.Message);
        //    }
        //}

        public static void saveconnection(string servertxt, string database, Form openwindow, Form closedwindow, Form parentwindow, string userid = null, string password = null)
        {
            
                connection = "Data Source=" + servertxt + ";Initial Catalog=" + database + ";User ID=" + userid + ";Password=" + password + ";MultipleActiveResultSets = true;";
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\connectiondetails";
                File.WriteAllText(path, connection);
                using (con)
                {
                try
                {

                    con.Open();
                    MessageBox.Show("Connection Successfully");
                    //DialogResult dr = MessageBox.Show("Connection Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //if (dr == DialogResult.OK)
                    //{
                    //    closedwindow.Close();
                    //    openwindow.MdiParent = parentwindow;
                    //    openwindow.WindowState = FormWindowState.Maximized;
                    //    openwindow.Show();

                    //}
                }
                catch (Exception ex)
                    {
                        File.Delete(path);
                        MessageBox.Show(ex.Message, "Error");
                    }
              
            }
         


        }


        public void UnLoadSpParameters()
        {
            DbCommand.Parameters.Clear();
        }

        public void LoadSpParameters(string SpName, params object[] ParaValues)
        {
            SqlParameter[] TheParameters = (SqlParameter[])SqlparamCache[SpName];
            DbCommand.Parameters.Clear();
            if (TheParameters == null)
            {
                DbCommand.CommandType = CommandType.StoredProcedure;
                DbCommand.CommandText = SpName;
                SqlCommandBuilder.DeriveParameters(DbCommand);
                TheParameters = new SqlParameter[DbCommand.Parameters.Count];

                DbCommand.Parameters.CopyTo(TheParameters, 0);
                SqlparamCache[SpName] = TheParameters;

            }
            else
            {
                short i;
                SqlParameter SqPr;
                DbCommand.CommandType = CommandType.StoredProcedure;
                DbCommand.CommandText = SpName;
                for (i = 0; i < TheParameters.Length; i++)
                {
                    SqPr = (SqlParameter)((System.ICloneable)(TheParameters[i])).Clone();
                    DbCommand.Parameters.Add(SqPr);
                }

            }
            MoveSqlParameters(ParaValues);

        }

        private void MoveSqlParameters(object[] Paras)
        {
            short ic;
            SqlParameter sqlPara;
            if (Paras.Length >= 0)
            {
                for (ic = 0; ic < Paras.Length; ic++)
                {
                    sqlPara = DbCommand.Parameters[ic + 1];
                    string s = sqlPara.ParameterName;
                    sqlPara.Value = Paras[ic];
                }
            }
        }

        public SqlParameter Parameters(int P)
        {
            return DbCommand.Parameters[P];
        }



        public bool OpenConnection()
        {
            try
            {
                if (con.State == ConnectionState.Open) return true;
                con = new SqlConnection();
                con.ConnectionString = connectionString();
                con.Open();
                if (con.State == ConnectionState.Open)
                {
                    DbCommand.Connection = con;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ee)
            {
                throw new System.Exception("Database:OpenConnection:" + ee.Message);
            }
        }

        public void CloseConnection()
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
                DbCommand.Dispose();
                DbCommand = null;
                DtAdapter.Dispose();
                DtAdapter = null;
                SqlDataSet.Dispose();
                SqlDataSet = null;
                SqlTable.Dispose();
                SqlTable = null;
            }
        }

        public SqlDataReader GetDataReader()
        {
            return DbCommand.ExecuteReader();

        }

        public int ExecuteQuery()
        {
            return DbCommand.ExecuteNonQuery();
        }

        public object ExecuteValue()
        {
            return DbCommand.ExecuteScalar();
        }

        public object ExecuteValue(string SQLStatement)
        {
            DbCommand.CommandType = CommandType.Text;
            DbCommand.CommandText = SQLStatement;
            return DbCommand.ExecuteScalar();
        }


        public string ReturnValue(string _PName)
        {
            DbCommand.ExecuteNonQuery();
            return (string)DbCommand.Parameters[_PName].Value.ToString();

        }

        public DataTable GetDataTable()
        {
            DtAdapter.SelectCommand = DbCommand;
            DtAdapter.Fill(SqlTable);
            return SqlTable;
        }

        public SqlConnection ConnectionObject
        {
            get { return con; }
        }
    }
}
