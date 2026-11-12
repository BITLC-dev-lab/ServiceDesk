using Microsoft.AspNetCore.Mvc;
using ServiceDesk.Data;

namespace ServiceDesk.Controllers
{
    public class CategoriesController : Controller
    {
        private ApplicationDbContext _context;
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
