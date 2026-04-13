using Database.Application.Abstractions.Persistence;
using Database.Application.DTO.Entities;
using Database.Domain.Entities;
using Database.Domain.Exceptions;
using MediatR;

namespace Database.Application.UseCases.RoleAssignments;

public sealed record GetRoleAssignmentQuery(
    Guid UserId, Guid ResourceId, Guid RoleId
) : IRequest<RoleAssignmentDto>;

public sealed class GetRoleAssignmentQueryHandler : IRequestHandler<GetRoleAssignmentQuery, RoleAssignmentDto>
{
    private readonly IRoleAssignmentRepository _roleAssignmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetRoleAssignmentQueryHandler(
        IRoleAssignmentRepository roleAssignmentRepository,
        IUnitOfWork unitOfWork)
    {
        _roleAssignmentRepository = roleAssignmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RoleAssignmentDto> Handle(GetRoleAssignmentQuery request, CancellationToken cancellationToken)
    {
        // var role = Domain.Entities.Role.Create(request.Code, request.Name, request.IsDefault);
        var res = await _roleAssignmentRepository.GetAsync(request.UserId, request.ResourceId, cancellationToken);
        if (res is null) throw new RoleAssignmentNotFound(request.UserId, request.ResourceId, request.RoleId);
        
        return new RoleAssignmentDto(res.Id, res.UserId, res.ResourceId, res.RoleId);
    }
}