using Microsoft.AspNetCore.Mvc;
using ServiceDesk.Services;


namespace ServiceDesk.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardService _dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = await _dashboardService.GetDashboardViewModelAsync();
            return View(viewModel);
        }
    }
}
