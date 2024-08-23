namespace keeper.Models;

public class RepoItem<T>
{
    public T Id { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
}
