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
            string name = parseName(file.FileName, userInfo.Id);

            // TODO integrate sharp

            var request = new PutObjectRequest()
            {
                BucketName = "porteryoungbucket",
                Key = name,
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType
            };
            request.Metadata.Add("Content-Type", file.ContentType);
            var response = await _s3Client.PutObjectAsync(request);
            return Ok($"https://porteryoungbucket.s3.us-west-2.amazonaws.com/{name}");
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    private static string parseName(string source, string userId)
    {
        string[] s = source.Split(' ');
        string n = string.Join("_", s);
        return $"{userId.TrimEnd('/')}/{n.Substring(0, n.LastIndexOf('.'))}";

        // foreach (int c in source)
        //     if(source[c] == ' ')source = source.Remove(c, 1);
        // return source;
    }
}
