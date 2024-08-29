namespace keeper.Services;

public class UploadService
{
    private readonly UploadRepository _repo;

    public UploadService(UploadRepository repo)
    {
        _repo = repo;
    }
}
