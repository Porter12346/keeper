using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;


namespace keeper.Controllers;

[ApiController]
[Route("api/[Controller]")]

public class UploadController : ControllerBase
{
    private readonly IAmazonS3 _s3Client;
    private readonly UploadService _uploadService;
    private readonly Auth0Provider _auth0Provider;

    public UploadController(UploadService uploadService, IAmazonS3 s3Client, Auth0Provider auth0Provider)
    {
        _uploadService = uploadService;
        _s3Client = s3Client;
        _auth0Provider = auth0Provider;
    }

    [HttpPost]
    [Authorize]

    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            var request = new PutObjectRequest()
            {
                BucketName = "porteryoungbucket",
                Key = $"{userInfo.Id?.TrimEnd('/')}/{file.FileName.Substring(0, file.FileName.LastIndexOf('.'))}",
                InputStream = file.OpenReadStream()
            };
            request.Metadata.Add("Content-Type", file.ContentType);
            var response = await _s3Client.PutObjectAsync(request);
            return Ok($"https://porteryoungbucket.s3.us-west-2.amazonaws.com/{userInfo.Id}/{file.FileName.Substring(0, file.FileName.LastIndexOf('.'))}");
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
