using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace TodoServer
{
    public class Users
    {



        public int IsAvailable(string username, string mail)
        {
            connection c =new connection();
            string cn = c.ConnectionString();
            int UserID = 0;
            try
            { 
                SqlDataReader reader;
                using (SqlConnection connection = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("SELECT Id FROM usere where userame=@username or mail=@mail ");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@mail", mail);
                    connection.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        UserID = reader.GetInt32(0);

                    }

                    reader.Close();
                    connection.Close();
                }

            }
            catch (Exception ex)
            {

            }

            return UserID;
        }
    }
}