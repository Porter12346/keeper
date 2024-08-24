namespace keeper.Models;

public class VaultKeep : RepoItem<int>
{
    public int keepId { get; set; }
    public int vaultId { get; set; }
    public string creatorId { get; set; }
}


public class VaultedKeep : Keep
{
    public int VaultKeepId { get; set; }
}