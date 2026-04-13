using Database.Application.Abstractions.Persistence;
using Database.Application.DTO.Entities;
using Database.Domain.Exceptions;
using MediatR;

namespace Database.Application.UseCases.Users;

public sealed record GetUserQuery(
    Guid UserId, Guid ResourceId, Guid RoleId
) : IRequest<UserDto?>;

public sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto?>
{
    private readonly IRoleAssignmentRepository _roleAssignmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetUserQueryHandler(
        IRoleAssignmentRepository roleAssignmentRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _roleAssignmentRepository = roleAssignmentRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var res = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (res is null) throw new UserNotFoundException(request.UserId);
        
        return new UserDto(res.Id, res.UserName, res.IsActive);
    }
}