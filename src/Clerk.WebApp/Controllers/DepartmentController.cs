using Clerk.Repositories;
using Microsoft.AspNetCore.Mvc;

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
            return View(_departmentRepository.GetAll());
        }
    }
}
