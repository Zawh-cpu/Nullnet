namespace Database.Application.DTO.Entities;

public class UserDto
{
    public Guid Id { get; }
    public string UserName { get; }
    public bool IsActive { get; }

    public UserDto(Guid id, string userName, bool isActive = true)
    {
        Id = id;
        UserName = userName;
        IsActive = isActive;
    }
}