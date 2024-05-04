using DataAccessLayer;
namespace BusinessLogicLayer
{
    public class bll_Student
    {
        public void InsertStudent()
        {
            DAL obj = new DAL();
            obj.OpenConnection();
        }
            
    }
}
