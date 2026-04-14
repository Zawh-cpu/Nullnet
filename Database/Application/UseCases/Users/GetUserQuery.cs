using Database.Application.Abstractions.Persistence;
using Database.Application.DTO.Entities;
using Database.Domain.Exceptions;
using MediatR;

namespace Database.Application.UseCases.Users;

public sealed record GetUserQuery(
    Guid UserId
) : IRequest<UserDto?>;

public sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto?>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(
        IUserRepository userRepository
    )
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var res = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (res is null) throw new UserNotFoundException(request.UserId);
        
        return new UserDto(res.Id, res.UserName, res.IsActive);
    }
}