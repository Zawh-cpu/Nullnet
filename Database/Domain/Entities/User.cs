namespace Database.Domain.Entities;

public sealed class User(Guid id, string userName, bool isVerified, bool isActive, DateTime createdAt)
{
    public Guid Id { get; set; } = id;
    public string UserName { get; set; } = userName;
    public bool IsVerified { get; set; } = isVerified;
    public bool IsActive { get; set; } = isActive;
    public DateTime CreatedAt { get; set; } = createdAt;
}