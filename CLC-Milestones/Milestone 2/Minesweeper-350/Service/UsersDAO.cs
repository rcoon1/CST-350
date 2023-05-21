using Minesweeper_350.Models;
using System;
using System.Data.SqlClient;

namespace Minesweeper_350.Service
{
    public class UsersDAO
    {

        bool success = false;

        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=minesweeper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool FindByUsernameAndPassword(UserModel user)
        {
            string sqlStatement = "SELECT * FROM dbo.users WHERE USERNAME = @username AND PASSWORD = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar, 40).Value = user.username;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar, 40).Value = user.password;

                try
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        success = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return success;

        }
        public bool RegisterUser(UserModel user)
        {
            string sqlStatement = "INSERT INTO dbo.users (first_name, last_name, sex, age, state, email, username, password) VALUES (@firstName, @lastName, @sex, @age, @state, @email, @username, @password)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@firstName", System.Data.SqlDbType.NVarChar, 40).Value = user.firstName;
                command.Parameters.Add("@lastName", System.Data.SqlDbType.NVarChar, 40).Value = user.lastName;
                command.Parameters.Add("@sex", System.Data.SqlDbType.NVarChar, 40).Value = user.sex;
                command.Parameters.Add("@age", System.Data.SqlDbType.Int).Value = user.age;
                command.Parameters.Add("@state", System.Data.SqlDbType.NVarChar, 40).Value = user.state;
                command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar, 40).Value = user.email;
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar, 40).Value = user.username;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar, 40).Value = user.password;

                try
                {
                    //start db connection
                    connection.Open();
                    Console.WriteLine(sqlStatement);
                    int j = command.ExecuteNonQuery();
                    if(j != 0)
                    {
                        success= true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return success;

        }

    }
}
