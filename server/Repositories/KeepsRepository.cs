

namespace keeper.Repositories;

public class KeepsRepository
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal List<Keep> GetAllKeeps()
    {
        string sql = @"
        SELECT 
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON accounts.id = keeps.creatorId;";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, JoinCreator).ToList();
        return keeps;
    }

    internal Keep PostKeep(Keep keepData)
    {
        string sql = @"
        INSERT INTO 
        keeps(name, description, img, creatorId)
        VALUES(@name, @description, @img, @creatorId);

        SELECT 
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON accounts.id = keeps.creatorId
        WHERE keeps.id = LAST_INSERT_ID();";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, JoinCreator, keepData).FirstOrDefault();
        return keep;
    }

    private Keep JoinCreator(Keep keep, Profile profile)
    {
        keep.creator = profile;
        return (keep);
    }
}

