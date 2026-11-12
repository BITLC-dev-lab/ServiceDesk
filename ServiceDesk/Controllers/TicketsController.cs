using ServiceDesk.Data;
using Microsoft.AspNetCore.Mvc;

namespace ServiceDesk.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext _context;
        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
