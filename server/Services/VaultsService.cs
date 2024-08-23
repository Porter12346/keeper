
namespace keeper.Services;

public class VaultsService
{
    private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
        _repo = repo;
    }

    internal Vault PostVault(Vault vaultData)
    {
        Vault vault = _repo.PostVault(vaultData);
        return vault;
    }
}
