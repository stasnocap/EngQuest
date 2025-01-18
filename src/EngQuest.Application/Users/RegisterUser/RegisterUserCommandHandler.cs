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
        if (!string.IsNullOrWhiteSpace(request.IdentityId) && await _userRepository.ExistsByIdentityIdAsync(request.IdentityId, cancellationToken))
        {
            return Result.Success<LogInResponse>(null!);
        }

        Result<Level> createLevelResult = Level.Create(request.Level, request.Experience);

        if (createLevelResult.IsFailure)
        {
            return Result.Failure<LogInResponse>(createLevelResult.Error);
        }

        var user = User.Create(
            new FirstName(request.FirstName),
            new LastName(request.LastName),
            new Email(request.Email));

        if (string.IsNullOrWhiteSpace(request.IdentityId))
        {
            if (string.IsNullOrWhiteSpace(request.Password))
            {
                return Result.Failure<LogInResponse>(new Error("User.EmptyPassword", "Пароль не может быть пустым."));
            }

            if (request.Password.Length < 5)
            {
                return Result.Failure<LogInResponse>(new Error("User.PasswordMinimumLength", "Пароль не может быть меньше 5 символов."));
            }

            string identityId = await _authenticationService.RegisterAsync(
                user,
                request.Password,
                cancellationToken);

            user.SetIdentityId(identityId);
        }
        else
        {
            user.SetIdentityId(request.IdentityId);
        }

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
