
using System.Diagnostics;

namespace keeper.Repositories;

public class ProfilesRepository
{
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal List<Keep> GetKeepsByProfile(string profileId)
    {
        string sql = @"
        SELECT
        *
        FROM
        keeps
        WHERE creatorId = @profileId;";

        List<Keep> keeps = _db.Query<Keep>(sql, new { profileId }).ToList();
        return keeps;
    }
    internal List<Vault> GetVaultsByProfile(string profileId)
    {
        string sql = @"
        SELECT
        *
        FROM
        vaults
        WHERE creatorId = @profileId AND isPrivate = false;";

        List<Vault> vaults = _db.Query<Vault>(sql, new { profileId }).ToList();
        return vaults;
    }

    internal Profile GetProfile(string profileId)
    {
        string sql = @"
        SELECT
        *
        FROM
        accounts
        WHERE id = @profileId;";

        Profile profile = _db.Query<Profile>(sql, new { profileId }).FirstOrDefault();
        return profile;
    }
}

