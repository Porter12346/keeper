namespace keeper.Models;

public class Vault : RepoItem<int>
{
    public string name { get; set; }
    public string description { get; set; }
    public string img { get; set; }
    public Boolean isPrivate { get; set; }
    public string creatorId { get; set; }
    public Profile creator { get; set; }
}
