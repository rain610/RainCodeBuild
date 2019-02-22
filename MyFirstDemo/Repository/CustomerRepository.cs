using DBModel;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace Repository
{
    public class CustomerRepository
    {
        public static string SqlConnectionString = "Data Source=10.101.42.39;Database = Northwind;Persist Security Info=True;User ID=sa;Password=Foxconn99";
        public IList<CustomerModel> GetCustomers()
        {
            var customerList = new List<CustomerModel>();
            using (IDbConnection conn = GetSqlConnection(SqlConnectionString))
            {
                string query = @"select * from Customers";
                return conn.Query<CustomerModel>(query).ToList();
            }
        }

        private static SqlConnection GetSqlConnection(string sqlConnectionString)
        {
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            conn.Open();
            return conn;
        }
    }
}
