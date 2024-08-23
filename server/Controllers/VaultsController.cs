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

}
