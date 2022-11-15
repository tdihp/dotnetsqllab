using System;
using System.Data;
using System.Diagnostics.Tracing;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace HelloWorld
{
    class Program
    {
        static void Main()
        {
            string connStr = System.Environment.GetEnvironmentVariable("CONNSTR");
            int numConns = Int32.Parse(System.Environment.GetEnvironmentVariable("NUMCONNS"));
            string bootstrapQueryStr = System.Environment.GetEnvironmentVariable("BOOTSTRAPQUERYSTR");
            string queryStr = System.Environment.GetEnvironmentVariable("QUERYSTR");
            if (numConns > 1)
            {
                Console.WriteLine("bootstrapping");
                Parallel.For(0, numConns, i =>
                {
                    CreateCommand(bootstrapQueryStr, connStr);
                });
                Console.WriteLine("... done");
                System.Threading.Thread.Sleep(60000);
            }
            while (true)
            {
                Console.WriteLine("CreateCommand");
                // CreateCommand(queryStr, connStr);
                Parallel.For(0, numConns, i =>
                {
                    CreateCommand(queryStr, connStr);
                });
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
