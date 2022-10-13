using Airespring.Web.Dto;
using Airespring.Web.Entities;

namespace Airespring.Web.Contracts
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees(string sortOrder);
        Task<IEnumerable<Employee>> GetEmployees(string sortOrder, string searchString);
        Task<Employee> GetEmployee(int Id);

        Task CreateEmployee(EmployeeDto employee);
    }
}
