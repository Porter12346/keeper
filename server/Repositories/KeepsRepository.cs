namespace keeper.Repositories;

public class KeepsRepository
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal void deleteKeep(int keepId)
    {
        string sql = "DELETE FROM keeps WHERE id = @keepId LIMIT 1;";

        int rowsAffected = _db.Execute(sql, new { keepId });
        if (rowsAffected == 0) throw new Exception("none deleted");
        if (rowsAffected > 1) throw new Exception("multiple deleted!!");
    }

    internal Keep EditKeep(Keep keepData)
    {
        string sql = @"
        UPDATE
        keeps
        SET
        name = @name,
        description = @description,
        views = @views
        WHERE keeps.id = @id;
        
        SELECT 
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON accounts.id = keeps.creatorId
        WHERE keeps.id = @id;";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, JoinCreator, keepData).FirstOrDefault();
        return keep;
    }

    internal List<Keep> GetAllKeeps()
    {
        string sql = @"
        SELECT 
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON accounts.id = keeps.creatorId 
        ORDER BY keeps.id;";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, JoinCreator).ToList();
        return keeps;
    }

    internal Keep GetKeepById(int keepId)
    {
        string sql = @"
        SELECT 
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON accounts.id = keeps.creatorId
        WHERE keeps.id = @keepId;";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, JoinCreator, new { keepId }).FirstOrDefault();
        return keep;
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

