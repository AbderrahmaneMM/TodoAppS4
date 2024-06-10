using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace TodoServer
{
    public class Tasks
    {
        public int Id;
        public string Title;
        public string Description;
        public DateTime DueDate;
        public int IsAvailable(int Id)
        {
            connection c = new connection();
            string cn = c.ConnectionString();
            int TaskID = 0;
            try
            {
                SqlDataReader reader;
                using (SqlConnection connection = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("SELECT Id FROM Task where Id=@Id ");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@Id", Id);

                    connection.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        TaskID = reader.GetInt32(0);

                    }

                    reader.Close();
                    connection.Close();
                }

            }
            catch (Exception ex)
            {

            }

            return TaskID;
        }
    }
}