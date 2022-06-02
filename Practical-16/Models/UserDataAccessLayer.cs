using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Practical_16.Models
{
    public class UserDataAccessLayer
    {
        public static IConfiguration Configuration { get; set; }

        //To Read ConnectionString from appsettings.json file  
        private static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            string connectionString = Configuration["ConnectionStrings:StudentDBContext"];

            return connectionString;

        }

        string connectionString = GetConnectionString();

        //To Register a new user   
        public string RegisterUser(User user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spRegisterUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@UserID", user.UserID);
                cmd.Parameters.AddWithValue("@UserPassword", user.Password);

                con.Open();
                string result = cmd.ExecuteScalar().ToString();
                con.Close();

                return result;
            }
        }

        //To Validate the login  
        public string ValidateLogin(User user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spValidateUserLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LoginID", user.UserID);
                cmd.Parameters.AddWithValue("@LoginPassword", user.Password);

                con.Open();
                string result = cmd.ExecuteScalar().ToString();
                con.Close();

                return result;
            }
        }
    }
}
