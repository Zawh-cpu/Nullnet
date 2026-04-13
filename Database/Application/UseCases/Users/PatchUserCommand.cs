using AutoMapper;
using Database.Application.Extensions;
using Database.Application.Abstractions.Persistence;
using Database.Application.DTO.PatchEntities;
using MediatR;

namespace Database.Application.UseCases.Users;

public sealed record PatchUserRequest(
    OptionalField<string> UserName,
    OptionalField<bool> IsActive
);

public sealed record PatchUserCommand(
    Guid UserId,
    PatchUserRequest Patch
) : IRequest<Guid>;

public sealed class PatchUserCommandHandler : IRequestHandler<PatchUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public async Task<Guid> Handle(PatchUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            throw new Exception("User not found");

        await _userRepository.UpdateAsync(
            user.Id,
            _mapper.Map<UserPatchDto>(request.Patch),
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return user.Id;
    }

    public PatchUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
}