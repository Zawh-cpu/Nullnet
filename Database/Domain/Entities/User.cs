namespace Database.Domain.Entities;

public sealed class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public bool IsActive { get; set; }

    public User(Guid id, string userName, bool isActive = true)
    {
        Id = id;
        UserName = userName;
        IsActive = isActive;
    }

    public static User Create(string userName, bool isActive = true)
    {
        return new User(Guid.NewGuid(), userName, isActive);
    }
}