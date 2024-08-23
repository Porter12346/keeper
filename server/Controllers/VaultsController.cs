namespace keeper.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class VaultsController : ControllerBase
{
    private readonly Auth0Provider _auth0Provider;
    private readonly VaultsService _vaultsService;

    public VaultsController(Auth0Provider auth0Provider, VaultsService vaultsService)
    {
        _auth0Provider = auth0Provider;
        _vaultsService = vaultsService;
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Vault>> PostVault([FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            vaultData.creatorId = userInfo.Id;
            Vault vault = _vaultsService.PostVault(vaultData);
            return vault;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

}
