using System.ComponentModel.DataAnnotations;

namespace Database.Presentation.Api.v1.Requests;

public sealed class CreateUserRequest
{
    [Required] public string UserName { get; set; } = null!;
    [Required] public bool IsVerified { get; set; }
    [Required] public bool IsActive { get; set; }
}