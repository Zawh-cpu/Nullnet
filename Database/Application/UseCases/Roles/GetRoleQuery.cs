using AutoMapper;
using Database.Application.Abstractions.Persistence;
using Database.Application.DTO.Entities;
using Database.Domain.Exceptions;
using MediatR;

namespace Database.Application.UseCases.Roles;

public sealed record GetRoleQuery(
    Guid RoleId
) : IRequest<RoleDto>;

public sealed class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, RoleDto>
{
    private readonly IMapper _mapper;
    private readonly IRoleRepository _roleRepository;

    public GetRoleQueryHandler(
        IMapper mapper,
        IRoleRepository roleRepository
    )
    {
        _mapper = mapper;
        _roleRepository = roleRepository;
    }

    public async Task<RoleDto> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.RoleId, cancellationToken);
        if (role == null)
            throw new RoleNotFoundException(request.RoleId);
        
        return _mapper.Map<RoleDto>(role);
    }
}