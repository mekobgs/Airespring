using Airespring.Web.Context;
using Airespring.Web.Contracts;
using Airespring.Web.Dto;
using Airespring.Web.Entities;
using Dapper;
using System.Data;

namespace Airespring.Web.Repository
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly DapperContext _context;
        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateEmployee(EmployeeDto employee)
        {
            var query = "INSERT INTO EMPLOYEE (LastName, FirstName, Phone, Zip, HiredDate) VALUES (@LastName, @FirstName,'(' + LEFT(@Phone, 3) + ')' + SUBSTRING(@Phone, 4, 3) + '-' + SUBSTRING(@Phone, 7, 4), @Zip,  convert(varchar, @HiredDate, 1))";
            var parameters = new DynamicParameters();
            parameters.Add("LastName", employee.LastName, DbType.String);
            parameters.Add("FirstName", employee.FirstName, DbType.String);
            parameters.Add("Phone",  employee.Phone, DbType.String);
            parameters.Add("Zip", employee.Zip, DbType.String);
            parameters.Add("HiredDate", employee.HiredDate, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<Employee> GetEmployee(int Id)
        {
            var query = "SELECT LastName, FirstName, Phone, Zip, Convert(varchar, HiredDate,1) as HiredDate FROM EMPLOYEE WHERE ID = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Employee>(query, new { Id });
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployees(string sortOrder)
        {
            var query = "SELECT LastName, FirstName, Phone, Zip, Convert(varchar, HiredDate,1) as HiredDate FROM EMPLOYEE";
            switch (sortOrder)
            {
                case "Date":
                    query += " Order By HiredDate";
                    break;
                case "date_desc":
                    query += " Order by HiredDate Desc";
                    break;
            }

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Employee>(query);
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployees(string sortOrder, string searchString)
        {
            var query = "SELECT LastName, FirstName, Phone, Zip, Convert(varchar, HiredDate,1) as HiredDate FROM EMPLOYEE WHERE LastName like CONCAT('%',@searchString,'%')  or Phone like CONCAT('%',@searchString,'%')";

            switch (sortOrder)
            {
                case "Date":
                    query += " Order By HiredDate";
                    break;
                case "date_desc":
                    query += " Order by HiredDate Desc";
                    break;
            }
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Employee>(query, new { searchString });
            }
        }
    }
}
