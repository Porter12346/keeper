namespace keeper.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class KeepsController : ControllerBase
{
    private readonly Auth0Provider _auth0Provider;
    private readonly KeepsService _keepsService;

    public KeepsController(Auth0Provider auth0Provider, KeepsService keepsService)
    {
        _auth0Provider = auth0Provider;
        _keepsService = keepsService;

    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Keep>> PostKeep([FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            keepData.creatorId = userInfo.Id;
            Keep keep = _keepsService.PostKeep(keepData);
            return keep;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet]

    public ActionResult<List<Keep>> GetAllKeeps()
    {
        try
        {
            List<Keep> keeps = _keepsService.GetAllKeeps();
            return keeps;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{keepId}")]

    public ActionResult<Keep> GetKeepById(int keepId)
    {
        try
        {
            Keep keep = _keepsService.incrementViews(keepId);
            return keep;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{keepId}")]
    [Authorize]

    public async Task<ActionResult<Keep>> EditKeep([FromBody] Keep keepData, int keepId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            keepData.creatorId = userInfo.Id;
            keepData.Id = keepId;
            Keep keep = _keepsService.EditKeep(keepData);
            return keep;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{keepId}")]
    [Authorize]

    public async Task<ActionResult<String>> DeleteKeep(int keepId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            _keepsService.DeleteKeep(keepId, userInfo.Id);
            return ("Keep Deleted");
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
