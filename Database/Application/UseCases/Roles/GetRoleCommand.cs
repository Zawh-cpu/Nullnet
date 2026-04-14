using Database.Application.Abstractions.Persistence;
using MediatR;

namespace Database.Application.UseCases.Roles;

public sealed record GetRoleCommand(
    string Code, string Name, bool IsDefault
) : IRequest<Guid>;

public sealed class GetRoleCommandHandler : IRequestHandler<GetRoleCommand, Guid>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetRoleCommandHandler(
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(GetRoleCommand request, CancellationToken cancellationToken)
    {
        var role = Domain.Entities.Role.Create(request.Code, request.Name, request.IsDefault);

        await _roleRepository.AddAsync(role, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return role.Id;
    }
}