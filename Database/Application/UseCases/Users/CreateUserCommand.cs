using Database.Application.Abstractions.Persistence;
using MediatR;

namespace Database.Application.UseCases.Users;

public sealed record CreateUserCommand(
    string UserName
) : IRequest<Guid>;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IRoleAssignmentRepository _roleAssignmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IRoleAssignmentRepository roleAssignmentRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _roleAssignmentRepository = roleAssignmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = Domain.Entities.User.Create(
            request.UserName
        );

        await _userRepository.AddAsync(user, cancellationToken);

        var defaultRole = await _roleRepository.GetDefaultRoleAsync(cancellationToken);
        if (defaultRole is null)
            throw new InvalidOperationException("Default role not found.");

        var assignment = Domain.Entities.RoleAssignment.Create(
            user.Id,
            Domain.Entities.Resource.GlobalId,
            defaultRole.Id
        );

        await _roleAssignmentRepository.AddAsync(assignment, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}