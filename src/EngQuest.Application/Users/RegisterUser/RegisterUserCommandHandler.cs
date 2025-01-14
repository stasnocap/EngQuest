using EngQuest.Application.Abstractions.Authentication;
using EngQuest.Application.Abstractions.Messaging;
using EngQuest.Application.Abstractions.Repositories;
using EngQuest.Application.Users.LogInUser;
using EngQuest.Domain.Abstractions;
using EngQuest.Domain.Levels;
using EngQuest.Domain.Users;

namespace EngQuest.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IAuthenticationService _authenticationService,
    IUserRepository _userRepository,
    ILevelRepository _levelRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<RegisterUserCommand, LogInResponse>
{
    public async Task<Result<LogInResponse>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        Result<Level> createLevelResult = Level.Create(request.Level, request.Experience);

        if (createLevelResult.IsFailure)
        {
            return Result.Failure<LogInResponse>(createLevelResult.Error);
        }

        var user = User.Create(
            new FirstName(request.FirstName),
            new LastName(request.LastName),
            new Email(request.Email));

        string identityId = await _authenticationService.RegisterAsync(
            user,
            request.Password,
            cancellationToken);

        user.SetIdentityId(identityId);

        _userRepository.Add(user);

        Level level = createLevelResult.Value;

        level.SetUserId(user.Id);

        _levelRepository.Add(level);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new LogInResponse()
        {
            Email = user.Email.Value,
            FirstName = user.FirstName.Value,
            LastName = user.LastName.Value,
            Roles = user.Roles.Select(x => x.Name).ToList(),
            IdentityId = user.IdentityId,
            UserId = user.Id,
        };
    }
}
