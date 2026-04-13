using Database.Application.Abstractions.Persistence;
using Database.Domain.Entities;
using Database.Domain.Exceptions;
using MediatR;

namespace Database.Application.UseCases.RoleAssignments;

public sealed record DivestRoleCommand(
    Guid UserId, Guid ResourceId, Guid RoleId
) : IRequest;

public sealed class DivestRoleCommandHandler : IRequestHandler<DivestRoleCommand>
{
    private readonly IRoleAssignmentRepository _roleAssignmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DivestRoleCommandHandler(
        IRoleAssignmentRepository roleAssignmentRepository,
        IUnitOfWork unitOfWork)
    {
        _roleAssignmentRepository = roleAssignmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DivestRoleCommand request, CancellationToken cancellationToken)
    {
        // var role = Domain.Entities.Role.Create(request.Code, request.Name, request.IsDefault);
        var res = await _roleAssignmentRepository.DivestIfExistsAsync(request.UserId,
            request.RoleId,
            request.ResourceId,
            cancellationToken);
        
        if (res is false) throw new RoleAssignmentNotFound(request.UserId, request.ResourceId, request.RoleId);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}