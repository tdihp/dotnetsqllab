using System;
using System.Data;
using System.Diagnostics.Tracing;
using Microsoft.Data.SqlClient;

namespace HelloWorld
{
    class Program
    {
        static void Main()
        {
            string connStr = System.Environment.GetEnvironmentVariable("CONNSTR");
            string qs = "SELECT @@version;";
            while (true)
            {
                Console.WriteLine("CreateCommand");
                CreateCommand(qs, connStr);
                System.Threading.Thread.Sleep(60000);
            }
        }

        private static void CreateCommand(string queryString,
            string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(
                       connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandTimeout = 15;
                command.CommandType = CommandType.Text;
                command.CommandText = queryString;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(String.Format("{0}", reader[0]));
                }
            }
        }
    }
}
