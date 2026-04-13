namespace Database.Domain.Entities;

public enum PermissionScope
{
    Self = 1,
    Children = 2,
    All = 3,
    Owned = 4
}