namespace keeper.Controllers;

[ApiController]
[Route("api/[Controler]")]
public class KeepsController : ControllerBase
{
    private readonly Auth0Provider _auth0Provider;
    private readonly KeepsService _keepsService;

    public KeepsController(Auth0Provider auth0Provider, KeepsService keepsService)
    {
        _auth0Provider = auth0Provider;
        _keepsService = keepsService;

    }

}
