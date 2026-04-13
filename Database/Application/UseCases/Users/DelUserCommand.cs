using Database.Application.Abstractions.Persistence;
using Database.Application.DTO.PatchEntities;
using Database.Domain.Exceptions;
using MediatR;

namespace Database.Application.UseCases.Users;

public sealed record DelUserCommand(
    Guid UserId
) : IRequest;

public sealed class DelUserCommandHandler : IRequestHandler<DelUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public async Task Handle(DelUserCommand request, CancellationToken cancellationToken)
    {
        var processed = await _userRepository.DelByIdAsync(request.UserId, cancellationToken);

        if (processed == 0)
            throw new UserNotFoundException(request.UserId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public DelUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
}