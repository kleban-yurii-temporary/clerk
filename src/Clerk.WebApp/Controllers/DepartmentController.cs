using Clerk.Core;
using Clerk.Repositories;
using Clerk.Repositories.Models;
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

        [HttpPost]
        public async Task<int> HasPositions(int id)
        {
            return await _departmentRepository.PositionsCountAsync(id);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _departmentRepository.DeleteAsync(id);
            return Ok("ok!");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new Department());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department model)
        {
            if (ModelState.IsValid)
            {
                await _departmentRepository.CreateAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _departmentRepository.GetInfoAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentInfo model)
        {
            if (ModelState.IsValid)
            {
                await _departmentRepository.EditAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
