using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Airespring.Web.Context;
using Airespring.Web.Entities;
using Airespring.Web.Contracts;
using Airespring.Web.Dto;

namespace Airespring.Web.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly IEmployeeRepository _employee;

        public CreateModel(IEmployeeRepository employee)
        {
            _employee = employee;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public EmployeeDto Employee { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            await _employee.CreateEmployee(Employee);

            return RedirectToPage("./Index");
        }
    }
}
