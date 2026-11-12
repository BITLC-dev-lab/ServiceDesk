using Microsoft.AspNetCore.Mvc;
using ServiceDesk.Data;
using Microsoft.EntityFrameworkCore;

namespace ServiceDesk.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext _context;
        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.ToListAsync();
            return View(employees);
        }
    }
}
