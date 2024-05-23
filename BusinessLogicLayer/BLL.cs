using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccessLayer;
using iTextSharp.text.pdf;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using PdfDocument = PdfSharp.Pdf.PdfDocument;
using PdfPage = PdfSharp.Pdf.PdfPage;

namespace BusinessLogicLayer
{
    public class BLL
    {
        public static void showWindow(Form openWin, Form closeWin, Form mdii)
        {
            closeWin.Close();
            openWin.MdiParent = mdii;
            openWin.WindowState = FormWindowState.Maximized;
            openWin.Show();
        }

        //public static bool isAuthenticate(string user, string pass)
        //{
        //    bool status = false;
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("select u.u_Username,u.u_Password from UserDetails u where u.u_Username=@user and u.u_Password=@pass", DataAccessLayer.DAL.con);
        //        cmd.Parameters.AddWithValue("@user", user);
        //        cmd.Parameters.AddWithValue("@pass", pass);
        //        DataAccessLayer.DAL.con.Open();
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            status = true;
        //                }
        //       // status = (result > 0);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception (logging, rethrowing, etc.)
        //        Console.WriteLine("An error occurred: " + ex.Message);
        //    }
        //    finally
        //    {
        //        if (DAL.con != null && DAL.con.State == System.Data.ConnectionState.Open)
        //        {
        //            DAL.con.Close();
        //        }
        //    }
        //    return status;
        //}
        private static string username;
        public static string GET_USERNAME()
        {
            return username;
        }
        private static void SET_USERNAME(string user)
        {
            username = user;
        }
        public static bool isAuthenticate(string user, string pass)
        {
            bool status = false;
            try
            {
                SqlCommand cmd = new SqlCommand("Select * from userDetails Where u_Username = @username and u_Pasword=@password", DAL.con);      
                cmd.Parameters.AddWithValue("@username", user);
                cmd.Parameters.AddWithValue("@password", pass);
                DAL.con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while(dr.Read())
                        {
                        SET_USERNAME(dr["u_Username"].ToString());
                        status = true;
                    }
                  
                    
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                    status = false;
                }
                DAL.con.Close();
            }
            catch (Exception ex)
            {
                DAL.con.Close();
                MessageBox.Show(ex.Message);
            }
            return status;
        }
        public static void Dropdown_Load(string proc, ComboBox cb, string displayMember, string valueMember)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(proc, DAL.con);
                //cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cb.DisplayMember = displayMember;
                cb.ValueMember = valueMember;
                cb.DataSource = dt;

            }
            catch (Exception ex)
            {

               MessageBox.Show(ex.Message);
            }
        }

        public static void AddBooking(string name, int carID, string address, string mobile, string nic, string carname, string carnum, string rent, string bookingdate)
        {
            try
            {
                // Your existing code to insert booking into the database
                SqlCommand cmd = new SqlCommand("insert into Booking (b_customername, b_cardetailsID, b_address, b_number, b_nic, b_carname, b_carnum, b_rent, b_bookingdate) values (@name, @carID, @address, @number, @nic, @carname, @carnum, @rent, @bookingdate)", DAL.con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@carID", carID);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@number", mobile);
                cmd.Parameters.AddWithValue("@nic", nic);
                cmd.Parameters.AddWithValue("@carname", carname);
                cmd.Parameters.AddWithValue("@carnum", carnum);
                cmd.Parameters.AddWithValue("@rent", rent);
                cmd.Parameters.AddWithValue("@bookingdate", bookingdate);

                DAL.con.Open();
                cmd.ExecuteNonQuery();
                DAL.con.Close();
                MessageBox.Show("Booking added Successfully");

                // Generate PDF
                GeneratePDF(name, carID, address, mobile, nic, carname, carnum, rent, bookingdate);
            }
            catch (Exception ex)
            {
                DAL.con.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public static void GeneratePDF(string name, int carID, string address, string mobile, string nic, string carname, string carnum, string rent, string bookingdate)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            // Add a page to the document
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 12, XFontStyle.Regular);

            // Define the content for the PDF
            string content = $"Booking Details\n\nName: {name}\nCar ID: {carID}\nAddress: {address}\nMobile: {mobile}\nNIC: {nic}\nCar Name: {carname}\nCar Number: {carnum}\nRent: {rent}\nBooking Date: {bookingdate}";

            // Draw the content on the page
            XRect rect = new XRect(40, 40, page.Width - 80, page.Height - 80);
            gfx.DrawString(content, font, XBrushes.Black, rect, XStringFormats.TopLeft);

            // Save the PDF to a MemoryStream
            MemoryStream memoryStream = new MemoryStream();
            document.Save(memoryStream);
            document.Close();

            // Convert the MemoryStream to byte array
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();

            // Write the byte array to a file or open it directly
            string filePath = "Booking_" + name + ".pdf";
            File.WriteAllBytes(filePath, bytes);

            // Open or prompt user to download the PDF
            Process.Start(filePath);
        }

        public static void AddCar(string cname, string model, string color, string rent, string carnum)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("insert into carsDetails (c_carname, c_model, c_color, c_carnumber, c_rent) VALUES (@c_name, @c_model, @c_color, @c_carnum, @c_rent)", DAL.con);
                cmd.Parameters.AddWithValue("@c_name", cname);
                cmd.Parameters.AddWithValue("@c_model", model);
                cmd.Parameters.AddWithValue("@c_color", color);
                cmd.Parameters.AddWithValue("@c_rent", rent);
                cmd.Parameters.AddWithValue("@c_carnum", carnum);
                
                DAL.con.Open();
                cmd.ExecuteNonQuery();
                DAL.con.Close();
                MessageBox.Show("Car Added Successfully");

            }
            catch (Exception ex)
            {
                DAL.con.Close();
                MessageBox.Show("Error");
            }
            
        }

        public static string FetchCarRent(int carId)
        {
            try
            {
                string carRent = ""; // Initialize car rent to an empty string
                using (SqlCommand cmd = new SqlCommand("SELECT c_rent FROM carsDetails WHERE c_id = @CarId", DAL.con))
                {
                    cmd.Parameters.AddWithValue("@CarId", carId);
                    DAL.con.Open();
                    object result = cmd.ExecuteScalar();
                    DAL.con.Close();

                    if (result != null && result != DBNull.Value)
                    {
                        decimal carRentDecimal = Convert.ToDecimal(result);
                        carRent = carRentDecimal.ToString(); // Convert decimal to string
                    }
                    else
                    {
                  //      MessageBox.Show("Specified car is not valid or car rent is not available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return carRent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DAL.con.Close();
                return ""; // Return an empty string in case of an error
            }
        }

        public static DataTable ShowCarDetails()
        {
            try
            {
                // Create a SQL query to fetch data from the carsDetails table
                string query = "SELECT c_id, c_carname, c_model, c_color, c_carnumber, c_rent FROM carsDetails";

                // Create a DataTable to store the retrieved data
                DataTable dt = new DataTable();

                // Create a SqlDataAdapter to fetch data from the database
                using (SqlDataAdapter da = new SqlDataAdapter(query, DAL.con))
                {
                    // Fill the DataTable with data from the database
                    da.Fill(dt);
                }

                // Return the populated DataTable
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null; // Return null in case of an error
            }
        }

        public static void DeleteCarDetail(string carId, DataGridView dataGridView)
        {
            string deleteQuery = "DELETE FROM carsDetails WHERE c_id = @carId";

            using (SqlCommand cmd = new SqlCommand(deleteQuery, DAL.con))
            {
                cmd.Parameters.AddWithValue("@carId", carId);

                try
                {
                    DAL.con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    DAL.con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record deleted successfully");

                        // Refresh the DataGridView to reflect the changes
                        DataTable dt = ShowCarDetails();
                        dataGridView.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No record deleted. Record not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    DAL.con.Close();
                }
            }
        }


        public static DataTable ShowBookingDetails()
        {
            try
            {
              
                string query = "SELECT b_id, b_customername, b_address, b_number, b_nic, b_carname, b_carnum, b_rent, b_bookingdate FROM Booking";

              
                DataTable dt = new DataTable();

              
                using (SqlDataAdapter da = new SqlDataAdapter(query, DAL.con))
                {
           
                    da.Fill(dt);
                }

               
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null; 
            }
        }


        public static void DeleteBookingDetail(string bookingId, DataGridView dataGridView)
        {
            string deleteQuery = "DELETE FROM Booking WHERE b_id = @bookingId";

            using (SqlCommand cmd = new SqlCommand(deleteQuery, DAL.con))
            {
                cmd.Parameters.AddWithValue("@bookingId", bookingId);

                try
                {
                    DAL.con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    DAL.con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record deleted successfully");

                      
                        DataTable dt = ShowBookingDetails();
                        dataGridView.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No record deleted. Record not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    DAL.con.Close();
                }
            }
        }


    }
}
