namespace Database.Application.DTO.Entities;

public class RoleDto(Guid id, string code, string name, bool isDefault)
{
    public Guid Id { get; } = id;
    public string Code { get; } = code;
    public string Name { get; } = name;
    public bool IsDefault { get; } = isDefault;
}