using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace TodoServer
{
    public class Crud
    {
        public void Signup(int Id, string mail, string password, string username)
        {
            string connectionString = "Data Source=DESKTOP-3QTD5NT;Initial Catalog=todolist;Integrated Security=True;TrustServerCertificate=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO usere(Id, mail, password,username) VALUES (@Id,@mail,@password,@username)");
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.Parameters.AddWithValue("@mail", mail);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@username", username);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
        }
        public void Read(string t , int Id)
        {
            string connectionString = "Data Source=DESKTOP-3QTD5NT;Initial Catalog=todolist;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM {t} WHERE Id=@Id");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void Delete(string t,int Id)
        {
            string connectionString = "Data Source=DESKTOP-3QTD5NT;Initial Catalog=todolist;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM {t} WHERE Id=@Id");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}