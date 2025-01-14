using System.Data;
using EngQuest.Application.Abstractions.Authentication;
using EngQuest.Application.Abstractions.Data;
using EngQuest.Application.Abstractions.Messaging;
using EngQuest.Application.Abstractions.Repositories;
using EngQuest.Domain.Abstractions;
using EngQuest.Domain.Users;

namespace EngQuest.Application.Users.LogInUser;

internal sealed class LogInUserCommandHandler(IJwtService _jwtService, ISqlConnectionFactory _sqlConnectionFactory, IUserRepository _userRepository) : ICommandHandler<LogInUserCommand, LogInResponse>
{
    public async Task<Result<LogInResponse>> Handle(
        LogInUserCommand request,
        CancellationToken cancellationToken)
    {
        Result<string> result = await _jwtService.GetAccessTokenAsync(
            request.Email,
            request.Password,
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<LogInResponse>(UserErrors.InvalidCredentials);
        }

        using IDbConnection dbConnection = _sqlConnectionFactory.CreateConnection();

        LogInResponse logIn = await _userRepository.GetLoginResponseAsync(request, dbConnection);

        return logIn;
    }
}
