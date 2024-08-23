


namespace keeper.Services;

public class KeepsService
{
    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
        _repo = repo;
    }

    internal List<Keep> GetAllKeeps()
    {
        List<Keep> keeps = _repo.GetAllKeeps();
        return keeps;
    }

    internal Keep GetKeepById(int keepId)
    {
        Keep keep = _repo.GetKeepById(keepId);
        return keep;
    }

    internal Keep PostKeep(Keep keepData)
    {
        Keep keep = _repo.PostKeep(keepData);
        return keep;
    }
}
