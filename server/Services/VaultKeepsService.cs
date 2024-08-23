
namespace keeper.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repo;

    public VaultKeepsService(VaultKeepsRepository repo)
    {
        _repo = repo;
    }

    internal VaultKeep PostVaultKeep(VaultKeep vaultKeepData)
    {
        VaultKeep vaultKeep = _repo.PostVaultKeep(vaultKeepData);
        return vaultKeep;
    }
}
