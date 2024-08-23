

using System.Diagnostics;

namespace keeper.Services;

public class VaultsService
{
    private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
        _repo = repo;
    }

    internal void DeleteVault(int vaultId, string userId)
    {
        Vault vaultToDelete = GetVaultById(vaultId, userId);
        if(vaultToDelete.creatorId != userId)throw new Exception("You cannot delete this vault");
        _repo.DeleteVault(vaultId);
    }

    internal Vault EditVault(Vault vaultData)
    {
        Vault vaultToEdit = GetVaultById(vaultData.Id, vaultData.creatorId);
        if (vaultToEdit.creatorId != vaultData.creatorId) throw new Exception("You do not own this vault");
        Vault vault = _repo.EditVault(vaultData);
        return vault;
    }

    internal Vault GetVaultById(int vaultId, string userId)
    {
        Vault vault = _repo.GetVaultById(vaultId) ?? throw new Exception("No vault found with given id");
        if (vault.isPrivate)
        {
            if (vault.creatorId != userId) throw new Exception("No vault found with given id");
        }
        return vault;
    }

    internal Vault PostVault(Vault vaultData)
    {
        Vault vault = _repo.PostVault(vaultData);
        return vault;
    }
}
