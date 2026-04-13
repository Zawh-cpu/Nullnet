using Database.Application.Extensions;

namespace Database.Application.DTO.PatchEntities;

public class UserPatchDto
{
    public OptionalField<string> UserName { get; }
    public OptionalField<bool> IsActive { get; }

    public UserPatchDto(OptionalField<string> userName, OptionalField<bool> isActive)
    {
        UserName = userName;
        IsActive = isActive;
    }
}