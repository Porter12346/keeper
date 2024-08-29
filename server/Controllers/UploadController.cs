using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace keeper.Controllers;

[ApiController]
[Route("api/[Controller]")]

public class UploadController : ControllerBase
{
    private readonly UploadService _uploadService;

    public UploadController(UploadService uploadService)
    {
        _uploadService = uploadService;
    }

    [HttpPost]

    public async Task<IActionResult> UploadFile(List<IFormFile> dogg)
    {
        try
        {
            return Ok(dogg);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
