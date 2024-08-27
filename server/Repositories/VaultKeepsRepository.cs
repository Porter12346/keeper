




namespace keeper.Repositories;

public class VaultKeepsRepository
{
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal List<VaultedKeep> GetVaultKeeps(int vaultId)
    {
        string sql = @"
        SELECT
        vk.*,
        k.*,
        COUNT(vkc.id) AS kept,
        a.*
        FROM vaultKeeps vk
        JOIN keeps k ON k.id = vk.keepId
        JOIN accounts a ON a.id = k.creatorId
        LEFT JOIN vaultKeeps vkc ON vkc.keepId = k.id
        WHERE vk.vaultId = @vaultId
        GROUP BY vk.id, k.id, a.id ;";

        List<VaultedKeep> keeps = _db.Query<VaultKeep, VaultedKeep, Profile, VaultedKeep>(sql, JoinVirtuals, new { vaultId }).ToList();
        return keeps;
    }

    private VaultedKeep JoinVirtuals(VaultKeep vaultKeep, VaultedKeep vaultedKeep, Profile profile)
    {
        vaultedKeep.VaultKeepId = vaultKeep.Id;
        vaultedKeep.creator = profile;
        return vaultedKeep;
    }

    internal VaultKeep PostVaultKeep(VaultKeep vaultKeepData)
    {
        string sql = @"
        INSERT INTO
        vaultKeeps(keepId, vaultId, creatorId)
        VALUES(@keepId, @vaultId, @creatorId);
        
        SELECT
        *
        FROM
        vaultKeeps
        WHERE id = LAST_INSERT_ID();";

        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, vaultKeepData).FirstOrDefault();
        return vaultKeep;
    }

    internal VaultKeep GetVaultKeepById(int id)
    {
        string sql = @"
        SELECT
        *
        FROM
        vaultKeeps
        WHERE id = @id;";

        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { id }).FirstOrDefault();
        return vaultKeep;
    }

    internal void DeleteVaultKeep(int vaultKeepId)
    {
        string sql = "DELETE FROM vaultKeeps WHERE id = @vaultKeepId LIMIT 1;";
        int rowsAffected = _db.Execute(sql, new { vaultKeepId });
        if (rowsAffected == 0) throw new Exception("none deleted");
        if (rowsAffected > 1) throw new Exception("multiple deleted!!");
    }
}

