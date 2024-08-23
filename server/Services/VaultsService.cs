

namespace keeper.Services;

public class VaultsService
{
    private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
        _repo = repo;
    }

    internal Vault GetVaultById(int vaultId, string userId)
    {
        Vault vault = _repo.GetVaultById(vaultId);
        if (vault.isPrivate)
        {
            if (vault.creatorId != userId) throw new Exception("No vault found with given id");
        }
        if (vault == null) throw new Exception("No vault found with given id");
        return vault;
    }

    internal Vault PostVault(Vault vaultData)
    {
        Vault vault = _repo.PostVault(vaultData);
        return vault;
    }
}
