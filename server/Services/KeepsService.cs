namespace keeper.Services;

public class KeepsService
{
    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
        _repo = repo;
    }

    internal void DeleteKeep(int keepId, string userId)
    {
        Keep keepToDelete = _repo.GetKeepById(keepId);
        if (keepToDelete.creatorId != userId) throw new Exception("You cannot delete this keep");
        _repo.deleteKeep(keepId);
    }

    internal Keep EditKeep(Keep keepData)
    {
        Keep keepToEdit = GetKeepById(keepData.Id);
        if (keepToEdit.creatorId != keepData.creatorId) throw new Exception("You did not make this keep");
        Keep keep = _repo.EditKeep(keepData);
        return keep;
    }

    internal List<Keep> GetAllKeeps()
    {
        List<Keep> keeps = _repo.GetAllKeeps();
        return keeps;
    }

    internal Keep GetKeepById(int keepId)
    {
        Keep keep = _repo.GetKeepById(keepId) ?? throw new Exception("No Keep Found");
        return keep;
    }

    internal Keep incrementViews(int keepId)
    {
        Keep keep = _repo.GetKeepById(keepId) ?? throw new Exception("No Keep Found");
        keep.views++;
        _repo.EditKeep(keep);
        return keep;
    }

    internal Keep PostKeep(Keep keepData)
    {
        Keep keep = _repo.PostKeep(keepData);
        return keep;
    }
}
