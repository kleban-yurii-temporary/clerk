using Clerk.Fake;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clerk.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakeDataController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly FakeDataInitRepository _fakeDataInitRepository;
        public FakeDataController(IWebHostEnvironment webHostEnvironment,
            FakeDataInitRepository fakeDataInitRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _fakeDataInitRepository = fakeDataInitRepository;
        }

        [HttpGet]
        [Route("/api/[controller]/path")]
        public async Task<string> WebPath()
        {            
            return _webHostEnvironment.WebRootPath;
        }

        [HttpGet]
        [Route("/api/[controller]/init")]
        public async Task<string> InitAsync()
        {
            var dataPath = Path.Combine(_webHostEnvironment.WebRootPath, "data", "fake.xlsx");
            await _fakeDataInitRepository.StartInit(dataPath);
            return "Ok!";
        }
    }
}
