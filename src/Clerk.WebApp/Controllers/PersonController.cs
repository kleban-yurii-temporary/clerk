using Clerk.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Clerk.WebApp.Controllers
{
    public class PersonController : Controller
    {
        private readonly PersonRepository _personRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PersonController(PersonRepository personRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _personRepository = personRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _personRepository.GetAsync(id));
        }

        public async Task<IActionResult> Avatar(int id)
        {
            var mimeType = "";
            var filePath = await _personRepository.GetAvatarPathAsync(id);            
            new FileExtensionContentTypeProvider().TryGetContentType(filePath, out mimeType);
            return File(filePath, mimeType);
        }

        [HttpPost]
        public async Task<IActionResult> UploadAvatar(IFormFile avatar, int id)
        {
            if (avatar != null)
            {
                var fileInfo = new FileInfo(avatar.FileName);

                var serverPath = Path.Combine("uploads", "person", $"{Guid.NewGuid()}{fileInfo.Extension}");
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, serverPath);                

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    avatar.CopyTo(stream);
                }

                var person = await _personRepository.GetAsync(id);

                if(!string.IsNullOrEmpty(person.AvatarPath))
                {
                    var avatarPath = Path.Combine(_webHostEnvironment.WebRootPath, person.AvatarPath);
                    if (System.IO.File.Exists(avatarPath))
                        System.IO.File.Delete(avatarPath);
                }

                await _personRepository.UpdateAvatarAsync(id, serverPath);
            }

            return RedirectToAction(nameof(Details), new { id }); 
        }
    }
}
