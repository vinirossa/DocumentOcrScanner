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
        public async Task<IActionResult> ReadRgDocumentInfoAsync(IFormFile file)
        {
            if (file is null)
                return BadRequest();

            await _service.ReadRgDocumentInfoAsync(file);

            return Ok();
        }
    }
}
