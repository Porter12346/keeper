



using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace keeper.Services;

public class KeepsService
{
    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
        _repo = repo;
    }

    internal Keep EditKeep(Keep keepData)
    {
        Trace.WriteLine(keepData.Id);
        Keep keepToEdit = GetKeepById(keepData.Id);
        if(keepToEdit.creatorId != keepData.creatorId)throw new Exception("You did not make this keep");
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

    internal Keep PostKeep(Keep keepData)
    {
        Keep keep = _repo.PostKeep(keepData);
        return keep;
    }
}
