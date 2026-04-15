using Database.Application.Extensions;

namespace Database.Presentation.Api.v1.Requests;

public sealed record PatchUserRequest(
    OptionalField<string> UserName,
    OptionalField<bool> IsVerified,
    OptionalField<bool> IsActive
);