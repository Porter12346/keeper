

namespace keeper.Services;

public class ProfilesService
{
    private readonly ProfilesRepository _repo;

    public ProfilesService(ProfilesRepository repo)
    {
        _repo = repo;
    }

    internal List<Keep> GetKeepsByProfile(string profileId)
    {
        List<Keep> keeps = _repo.GetKeepsByProfile(profileId);
        return keeps;
    }

    internal List<Vault> GetVaultsByProfile(string profileId)
    {
        List<Vault> vaults = _repo.GetVaultsByProfile(profileId);
        return vaults;
    }

    internal Profile GetProfile(string profileId)
    {
        Profile profile = _repo.GetProfile(profileId);
        return profile;
    }
}
