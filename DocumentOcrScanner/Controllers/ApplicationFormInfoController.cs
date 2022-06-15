using DocumentOcrScanner.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocumentOcrScanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationFormInfoController : ControllerBase
    {
        private readonly IDocumentInfoReaderService _service;
        public ApplicationFormInfoController(IDocumentInfoReaderService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task ReadRgDocumentInfoAsync(IFormFile file)
        {

        }
    }
}
