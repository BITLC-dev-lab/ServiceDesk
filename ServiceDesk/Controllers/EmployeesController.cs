using Microsoft.AspNetCore.Mvc;
using ServiceDesk.Data;

namespace ServiceDesk.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext _context;
        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
