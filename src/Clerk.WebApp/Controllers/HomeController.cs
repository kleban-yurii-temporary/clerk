using Clerk.Repositories;
using Clerk.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Clerk.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InfoRepository _infoRepository;
        private readonly DepartmentRepository _departmentRepository;

        public HomeController(
            ILogger<HomeController> logger,
            InfoRepository infoRepository,
            DepartmentRepository departmentRepository)
        {
            _logger = logger;
            _infoRepository = infoRepository;
            _departmentRepository = departmentRepository;
        }

        public IActionResult Index(int avgFilter = 0, int maxFilter = 0)
        {
            return View(_infoRepository.GetInfo(avgFilter, maxFilter));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Structure()
        {
            var dis = _departmentRepository.GetStructure();
            return View(dis);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}