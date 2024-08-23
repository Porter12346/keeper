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

    [HttpGet("{vaultId}")]
    public async Task<ActionResult<Vault>> GetVaultById(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultsService.GetVaultById(vaultId, userInfo.Id);
            return vault;

        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<Vault>> EditVault([FromBody] Vault vaultData, int vaultId)
    {
        try
        {
            vaultData.Id = vaultId;
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            vaultData.creatorId = userInfo.Id;
            Vault vault = _vaultsService.EditVault(vaultData);
            return vault;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

}
