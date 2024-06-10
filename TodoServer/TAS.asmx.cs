using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.Common;
using System.Collections.ObjectModel;
using System.Data.Odbc;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Security.Policy;
using System.Drawing;

namespace TodoServer
{
    /// <summary>
    /// Summary description for TAS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TAS : System.Web.Services.WebService
    {
      //sing up method
  [WebMethod(MessageName = "Register", Description = "Register new account")]
        [System.Xml.Serialization.XmlInclude(typeof(ReturnData))]
        public ReturnData Register(int Id, string mail, string password, string username)  
        {

            string connectionString = "Data Source=DESKTOP-3QTD5NT;Initial Catalog=todolist;Integrated Security=True;TrustServerCertificate=True";

            int IsAdded = 1;
            string Message = "";

            // check if we have this account already
            Users myUsers = new Users();
            if (myUsers.IsAvailable(username, mail) == 0)
            {
                //  saving into db
                try
                {
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
                    Message = "your account is created succefully";
                }
                catch (Exception ex)
                {
                    IsAdded = 0;
                    Message = ex.Message;// "Cannot add your inforamtion";
                }
            }
            else
            {
                IsAdded = 0;
                Message = "User name or email is reserved";
            }
            ReturnData rt = new ReturnData();
            rt.Message = Message;
            rt.Id = IsAdded;

            return rt;
        }

        //Add Task Mathode
        [WebMethod(MessageName = "Add Task", Description = "Add new Task ")]
        [System.Xml.Serialization.XmlInclude(typeof(ReturnData))]
        public ReturnData Addtask (int Id, string Title, string Description, DateTime DueDate)
        {
            string connectionString = "Data Source=DESKTOP-3QTD5NT;Initial Catalog=todolist;Integrated Security=True;TrustServerCertificate=True";

            int IsAdded = 1;
            string Message = "new task";
            Tasks task = new Tasks();
           
                //  saving into db
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO Task (Id,Title,Description,DueDate) VALUES (@Id,@Title,@Description,@DueDate)");
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.Parameters.AddWithValue("@Title", Title);
                        cmd.Parameters.AddWithValue("@Description", Description);
                        cmd.Parameters.AddWithValue("@DueDate", DueDate);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();

                    }
                    Message = "your Task is created succefully";
                }
                catch (Exception ex)
                {
                    IsAdded = 0;
                    Message = ex.Message;// "Cannot add your inforamtion";
                }
        
            ReturnData rt = new ReturnData();
            rt.Message = Message;
            rt.Id = IsAdded;

            return rt;
        }

        [WebMethod]
        public void Adduser() 
        {
         Crud crud = new Crud();
            crud.Signup(7,"tyui","748","uio");
        }

        //Add collection Mathode
        [WebMethod(MessageName = "Add collection", Description = "Add new Collection")]
        [System.Xml.Serialization.XmlInclude(typeof(ReturnData))]
        public ReturnData AddCollection(int Id, string name, int  idtask)
        {
            string connectionString = "Data Source=DESKTOP-3QTD5NT;Initial Catalog=todolist;Integrated Security=True";

            int IsAdded = 1;
            string Message = "new collection";
            Tasks task = new Tasks();
            if (task.IsAvailable(Id) == 0)
            {
                //  saving into db
                try
                {
                  using (SqlConnection connection = new SqlConnection(connectionString))
                  {
                    SqlCommand cmd = new SqlCommand("INSERT INTO collection (Id,name,idtask) VALUES (@Id,@name,@idtask)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@idtask", idtask);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                  }
                  Message = "your collection is add succefully";
                  }


                  catch (Exception ex)
                    {
                    IsAdded = 0;
                    Message = ex.Message;// "Cannot add your inforamtion";
                }

            }
            else
            {
                IsAdded = 0;
                Message = "Task doesn't exist";
            }
            ReturnData rt = new ReturnData();
            rt.Message = Message;
            rt.Id = IsAdded;

            return rt;
        }

        // login method :
        [WebMethod(MessageName = "Login", Description = "Login new user")]
        [System.Xml.Serialization.XmlInclude(typeof(ReturnData))]
        public ReturnData Login(string username, string password) 
        {
            string connectionString = "Data Source=DESKTOP-3QTD5NT;Initial Catalog=todolist;Integrated Security=True;TrustServerCertificate=True";
            int UserID = 0;
            string Message = "";

            try
            {
                SqlDataReader reader;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT Id,username,mail FROM usere where username=@username and password=@password ");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    connection.Open();

                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserID = reader.GetInt32(0);

                    }
                    if (UserID == 0)
                    {
                        Message = " user name or password is in correct";
                    }
                    reader.Close();

                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                Message = " cannot access to the data";
            }


            ReturnData rt = new ReturnData();
            rt.Message = Message;
            rt.Id = UserID;

            return rt;
        }

        [WebMethod(MessageName = "LoginNotify", Description = "Login Notify new user")]

        [System.Xml.Serialization.XmlInclude(typeof(ReturnData))]
        public ReturnData LoginNotify(int Id)  /// get list of notes
        {

            string connectionString = "Data Source=DESKTOP-3QTD5NT;Initial Catalog=todolist;Integrated Security=True;TrustServerCertificate=True";
            string Message = "";

            try
            {
                SqlDataReader reader;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT username,Id FROM usere where Id>@Id and Id=(SELECT MAX(Id) FROM[usere])");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    connection.Open();

                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Message = reader.GetString(0);
                        Id = reader.GetInt32(1);
                    }
                    if (Message.Length == 0)
                    {
                       Id = 0;
                        Message = "NO NEW USER";
                    }
                    reader.Close();

                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                Message = " cannot access to the data";
            }


            ReturnData rt = new ReturnData();
            rt.Message = Message;
            rt.Id = Id;

            return rt;
        }

        //tESTING CONNECTION METHOD:
        [WebMethod (Description = "tESTING CONNECTION" )]
        public DataSet GetData()
        {
            string connectionString = "Data Source=DESKTOP-3QTD5NT;Initial Catalog=todolist;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Task";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
            }
        }
    }
}
