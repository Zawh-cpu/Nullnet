namespace Database.Presentation.Api.v1.Requests;

public sealed record CreateRoleRequest(
    string Code, string Name, bool IsDefault
);