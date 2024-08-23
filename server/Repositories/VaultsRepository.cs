


namespace keeper.Repositories;

public class VaultsRepository
{
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Vault EditVault(Vault vaultData)
    {
        string sql = @"
        UPDATE
        vaults
        SET
        name = @name,
        isPrivate = @isPrivate
        WHERE vaults.id = @id;
        
        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.id = vaults.creatorId
        WHERE vaults.id = @id;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, JoinCreator, vaultData).FirstOrDefault();
        return vault;
    }

    internal Vault GetVaultById(int vaultId)
    {
        string sql = @"
        SELECT 
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.id = vaults.creatorId
        WHERE vaults.id = @vaultId;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, JoinCreator, new { vaultId }).FirstOrDefault();
        return vault;
    }

    internal Vault PostVault(Vault vaultData)
    {
        string sql = @"
        INSERT INTO
        vaults(name, description, img, isPrivate, creatorId)
        VALUES(@name, @description, @img, @isPrivate, @creatorId);
        
        SELECT 
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.id = vaults.creatorId
        WHERE vaults.id = LAST_INSERT_ID();";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, JoinCreator, vaultData).FirstOrDefault();
        return vault;
    }

    private Vault JoinCreator(Vault vault, Profile profile)
    {
        vault.creator = profile;
        return (vault);
    }
}

