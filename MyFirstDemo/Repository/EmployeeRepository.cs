using Dapper;
using DBModel;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Repository
{
    public class EmployeeRepository
    {
        public static string SqlConnectionString = "server=10.101.42.39;Database = Northwind;User ID=sa;Password=Foxconn99";
        public IList<EmployeeModel> GetEmployees()
        {
            var customerList = new List<EmployeeModel>();
            using (IDbConnection conn = GetSqlConnection(SqlConnectionString))
            {
                string query = @"select * from [Northwind].[dbo].[Employees]";
                return conn.Query<EmployeeModel>(query).ToList();
            }
        }

        private static SqlConnection GetSqlConnection(string sqlConnectionString)
        {
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            conn.Open();
            return conn;
        }

        public int Add(EmployeeModel employeeModel)
        {
            int result = 0;
            string sql = "insert into Employees (LastName,FirstName) values (@LastName,@FirstName)";
            using (IDbConnection conn = GetSqlConnection(SqlConnectionString))
            {
                result = conn.Execute(sql, employeeModel);
            }
            return result;
        }

        public void Delete(int id)
        {
            string sql = "delete from Employees where EmployeeID = @EmployeeID";
            using (IDbConnection conn = GetSqlConnection(SqlConnectionString))
            {
                conn.Execute(sql, new { EmployeeID = id });
            }
        }

        public void Update(EmployeeModel employeeModel)
        {
            string sql = "update Employees set LastName=@LastName,FirstName=@FirstName where EmployeeID=@EmployeeID";
            using (IDbConnection conn = GetSqlConnection(SqlConnectionString))
            {
                var result = conn.Execute(sql, employeeModel);
            }
        }
    }
}
