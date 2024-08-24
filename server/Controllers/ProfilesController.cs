using System.Diagnostics;

namespace keeper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly ProfilesService _profilesService;
    private readonly Auth0Provider _auth0Provider;

    public ProfilesController(ProfilesService profilesService, Auth0Provider auth0Provider)
    {
        _profilesService = profilesService;
        _auth0Provider = auth0Provider;
    }

    [HttpGet("{profileId}")]

    public ActionResult<Profile> GetProfile(string profileId)
    {
        try
        {
            Profile profile = _profilesService.GetProfile(profileId);
            return profile;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{profileId}/keeps")]

    public ActionResult<List<Keep>> GetKeepsByProfile(string profileId)
    {
        try
        {
            List<Keep> keeps = _profilesService.GetKeepsByProfile(profileId);
            return keeps;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{profileId}/vaults")]

    public ActionResult<List<Vault>> GetVaultsByProfile(string profileId)
    {
        try
        {
            List<Vault> vaults = _profilesService.GetVaultsByProfile(profileId);
            return vaults;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
