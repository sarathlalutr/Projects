using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using UserDetails;
namespace Libraries
{
    public class UserManager
    {
        public UserEntity getUserDetails(string userName, string password)
        {
            UserEntity authUser = null;

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName", userName)
                , new SqlParameter("@Password", password)
            };
            //Lets get the user
            using (
                DataTable table = DatabaseManager.ExecuteSelectCommand("usp_UserLoginSelect",
                    CommandType.StoredProcedure, parameters))

                //check if any record exist or not
                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];

                    authUser = new UserEntity();

                    //Now lets populate the user details  

                    authUser.username = row["UserName"].ToString();
                    authUser.password = row["Password"].ToString();
                    authUser.role = row["Role"].ToString();
                    

                    return authUser;
                }
                else
                    return null;
        }

        public bool authenticateUser(string userName, string password)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName", userName)
                , new SqlParameter("@Password", password)
            };
            //Lets get the user
            using (
                DataTable table = DatabaseManager.ExecuteSelectCommand("usp_UserLoginSelect",
                    CommandType.StoredProcedure, parameters))

                //check if any record exist or not
                if (table.Rows.Count == 1)
                {
                    return true;
                }
                else
                    return false;
        }

        public bool updatePassword(string userName, string currPassword, string newPassword)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName", userName)
                , new SqlParameter("@CurrPassword", currPassword)
                , new SqlParameter("@NewPassword", newPassword)
            };

            return DatabaseManager.ExecuteNonQuery("usp_ChangePasswordUpdate", CommandType.StoredProcedure, parameters);
        }
    }
}
