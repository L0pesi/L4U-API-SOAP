using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using L4U_API_SOAP.SoapModels;
using System.Web.Services;
using System.Threading.Tasks;

namespace L4U_API_SOAP.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor"
    // menu to change the class name "Users" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service,
    // please select Users.svc or Users.svc.cs at the Solution Explorer and start debugging.
    public class Users : IUsers
    {
        
        //Declaration of Connection String
        string connectString = "Server=l4u.database.windows.net;Database=L4U;"+
            "User Id=supergrupoadmin;Password=supergrupo+2022";


        /// <summary>
        /// Adds a new User
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public bool AddNewUser(User user)
        {

            if (user == null || string.IsNullOrEmpty(user.Email)) return false; //checks if obj is null

            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    //conn.Open();
                    string addUser = "INSERT INTO Users " +
                        "(FirstName, LastName, Email, Password) " +
                        "VALUES " +
                        "(@FirstName,@LastName,@Email,@Password)";
                    using (SqlCommand cmd = new SqlCommand(addUser))
                    {

                        //cmd.CommandType = CommandType.Text;

                        cmd.Connection = conn;
                        /*
                        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", user.LastName);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        */
                        cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = user.FirstName;
                        cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = user.LastName;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                        cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;

                        conn.Open();
                        int result = cmd.ExecuteNonQuery();
                        conn.Close();

                        return result.Equals(1);

                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

        }


         


        /// <summary>
        /// Lists All Users in the Database
        /// </summary>
        /// <returns></returns>        
        public List<User> GetAllUsers()
        {

            try
            {
                SqlConnection conn = new SqlConnection(connectString);

                string updateLocker = "SELECT * FROM users";
                SqlCommand cmd = new SqlCommand(updateLocker);


                cmd.Connection = conn;
                conn.Open();
                // Execute the command and get the data
                SqlDataReader reader = cmd.ExecuteReader();
                List<User> users = new List<User>();
                while (reader.Read())
                {
                    User user = new User();
                    user.Id = reader.GetString(0);
                    user.FirstName = reader.GetString(1);
                    user.LastName = reader.GetString(2);
                    user.Email = reader.GetString(3);
                    user.Password = reader.GetString(4);
                    users.Add(user);
                }

                return users;

            }
            catch (Exception)
            {
                return null;
            }

        }



        /// <summary>
        /// Method that deletes a user from the db
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(User user)
        {

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                string deleteUser = "Delete from users where id=@Id";
                using (SqlCommand cmd = new SqlCommand(deleteUser))
                {
                    cmd.Connection = conn;

                    cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = user.Id;
                    
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();
                    

                }
            }
        }
            
         


    }
}
