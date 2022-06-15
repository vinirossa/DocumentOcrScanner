using DocumentOcrScanner.Models;
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

        [HttpPost("file")]
        public async Task<IActionResult> ReadRgDocumentInfoAsync(IFormFile file)
        {
            try
            {
                if (file is null)
                    return BadRequest();

                await _service.ReadRgDocumentInfoAsync(file);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message});
            }
        }

        [HttpPost("teste")]
        public async Task<IActionResult> TestAsync(ApplicationFormInfo model)
        {
            try
            {
                if (model is null)
                    return BadRequest();

                await _service.TesteAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }
    }
}
