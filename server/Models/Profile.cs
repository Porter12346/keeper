namespace keeper.Models;

public class Profile : RepoItem<String>
{
    public string Name { get; set; }
    public string Picture { get; set; }
    public string coverImg { get; set; }
}
