


using Microsoft.AspNetCore.SignalR;

namespace keeper.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repo;
    private readonly VaultsService _vaultsService;

    public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService)
    {
        _repo = repo;
        _vaultsService = vaultsService;
    }

    internal void DeleteVaultKeep(int vaultKeepId, string userId)
    {
        VaultKeep vaultKeepToDelete = GetVaultKeepById(vaultKeepId, userId);
        if(vaultKeepToDelete.creatorId != userId)throw new Exception("You cannot delete this VaultKeep");
        _repo.DeleteVaultKeep(vaultKeepId);
    }

    internal List<VaultedKeep> GetVaultKeeps(int vaultId, string userId)
    {
        Vault vaultContainingKeeps = _vaultsService.GetVaultById(vaultId, userId);
        List<VaultedKeep> keeps = _repo.GetVaultKeeps(vaultId);
        return keeps;
    }

    internal VaultKeep GetVaultKeepById(int id, string userId)
    {
        VaultKeep vaultKeep = _repo.GetVaultKeepById(id) ?? throw new Exception("No vaultKeep found with given id");
        if (vaultKeep.creatorId != userId) throw new Exception("No vaultKeep found with given id");
        return vaultKeep;
    }

    internal VaultKeep PostVaultKeep(VaultKeep vaultKeepData)
    {
        Vault vaultToAddTo = _vaultsService.GetVaultById(vaultKeepData.vaultId, vaultKeepData.creatorId);
        if(vaultToAddTo.creatorId != vaultKeepData.creatorId)throw new Exception("you don't own this vault");
        VaultKeep vaultKeep = _repo.PostVaultKeep(vaultKeepData);
        return vaultKeep;
    }
}
