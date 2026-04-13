namespace Database.Domain.Entities;

public sealed class Role
{
    public Guid Id { get; }
    public string Code { get; }
    public string Name { get; }
    public bool IsDefault { get; }

    public Role(Guid id, string code, string name, bool isDefault)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Role id cannot be empty.", nameof(id));

        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Role code cannot be empty.", nameof(code));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name cannot be empty.", nameof(name));

        Id = id;
        Code = code;
        Name = name;
        IsDefault = isDefault;
    }

    public static Role Create(string code, string name, bool isDefault = false)
        => new(Guid.NewGuid(), code, name, isDefault);
}