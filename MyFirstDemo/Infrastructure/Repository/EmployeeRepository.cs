using Dapper;
using DomainStandard.Interface;
using DomainStandard.Model;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    [MapTo(typeof(IEmployeeRepository), ServiceLifetime.Singleton)]
    public class EmployeeRepository: IEmployeeRepository
    {
        public static string SqlConnectionString = "server=10.101.42.39;Database = Northwind;User ID=sa;Password=Foxconn99";
        public async Task<IList<EmployeeModel>> GetList(string firstName, string lastName, DataPage dataPage = null)
        {
            var customerList = new List<EmployeeModel>();
            using (IDbConnection conn = GetSqlConnection(SqlConnectionString))
            {
                string query = @"select * from [Northwind].[dbo].[Employees]";
                var result = await conn.QueryAsync<EmployeeModel>(query);
                return result.ToList();
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
