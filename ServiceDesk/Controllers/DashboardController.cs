using Microsoft.AspNetCore.Mvc;
using ServiceDesk.Models.Enums;
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
        public async Task<IActionResult> Index(TicketStatus? status, TicketPriority? priority)
        {
            var viewModel = await _dashboardService.GetDashboardViewModelAsync(status, priority);
            return View(viewModel);
        }
    }
}
