using Database.Application.Abstractions.Persistence;
using Database.Domain.Entities;
using MediatR;

namespace Database.Application.UseCases.RoleAssignments;

public sealed record AssignRoleCommand(
    Guid UserId, Guid ResourceId, Guid RoleId
) : IRequest<Guid>;

public sealed class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, Guid>
{
    private readonly IRoleAssignmentRepository _roleAssignmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AssignRoleCommandHandler(
        IRoleAssignmentRepository roleAssignmentRepository,
        IUnitOfWork unitOfWork)
    {
        _roleAssignmentRepository = roleAssignmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        // var role = Domain.Entities.Role.Create(request.Code, request.Name, request.IsDefault);
        var data = RoleAssignment.Create(request.UserId, request.ResourceId, request.RoleId);
        await _roleAssignmentRepository.AddAsync(data, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return data.Id;
    }
}