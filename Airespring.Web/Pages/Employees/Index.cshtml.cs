using Microsoft.AspNetCore.Mvc.RazorPages;
using Airespring.Web.Entities;
using Airespring.Web.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Airespring.Web.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly IEmployeeRepository _employee;

        public IndexModel(IEmployeeRepository employee)
        {
            _employee = employee;
        }
       
        public IEnumerable<Employee> Employee { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder,string currentFilter, string searchString)
        {
            CurrentSort = sortOrder;
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            if (string.IsNullOrEmpty(searchString))
                searchString = currentFilter;

            CurrentFilter = searchString;

            if (string.IsNullOrEmpty(searchString))
            {
                Employee = await _employee.GetEmployees(sortOrder);                
            }
            else
            {
                Employee = await _employee.GetEmployees(sortOrder,searchString);
            }

            
        }
    }
}
