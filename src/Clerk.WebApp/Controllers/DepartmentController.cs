using Clerk.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Clerk.WebApp.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DepartmentRepository _departmentRepository;
        public DepartmentController(DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;                
        }
        
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            ViewBag.Count = departments.Count();
            return View(departments);
        }
    }
}
