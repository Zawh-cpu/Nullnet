using Database.Application.Extensions;

namespace Database.Application.DTO.PatchEntities;

public class UserPatchDto(OptionalField<string> userName, OptionalField<bool> isVerified, OptionalField<bool> isActive)
{
    public OptionalField<string> UserName { get; } = userName;
    public OptionalField<bool> IsVerified { get; } = isVerified;
    public OptionalField<bool> IsActive { get; } = isActive;
}