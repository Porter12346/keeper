namespace keeper.Models;

public class Keep : RepoItem<int>
{
    public string name { get; set; }
    public string description { get; set; }
    public string img { get; set; }
    public int views { get; set; }
    public string creatorId { get; set; }
    public Profile creator { get; set; }
    public int kept { get; set; }
}
