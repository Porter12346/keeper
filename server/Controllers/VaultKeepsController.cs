namespace keeper.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class VaultKeepsController : ControllerBase
{
    private readonly Auth0Provider _auth0Provider;
    private readonly VaultKeepsService _vaultKeepsService;

    public VaultKeepsController(Auth0Provider auth0Provider, VaultKeepsService vaultKeepsService)
    {
        _auth0Provider = auth0Provider;
        _vaultKeepsService = vaultKeepsService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> PostVaultKeep([FromBody] VaultKeep vaultKeepData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            vaultKeepData.creatorId = userInfo.Id;
            VaultKeep vaultKeep = _vaultKeepsService.PostVaultKeep(vaultKeepData);
            return vaultKeep;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{vaultKeepId}")]
    [Authorize]
    public async Task<ActionResult<String>> DeleteVaultKeep(int vaultKeepId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            _vaultKeepsService.DeleteVaultKeep(vaultKeepId, userInfo?.Id);
            return "vault keep Deleted";
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

    }
}
