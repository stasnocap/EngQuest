using System.Data;
using System.Runtime.CompilerServices;
using EngQuest.Application.Abstractions.Data;
using EngQuest.Application.Abstractions.Messaging;
using EngQuest.Application.Abstractions.Repositories;
using EngQuest.Domain.Abstractions;
using MediatR;

namespace EngQuest.Application.Users.GetUsers;

public class GetUsersQueryHandler(ISqlConnectionFactory _sqlConnectionFactory, IUserRepository _userRepository) : IRequestHandler<GetUsersQuery, IReadOnlyList<UserResponse>>
{
    public async Task<IReadOnlyList<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection dbConnection = _sqlConnectionFactory.CreateConnection();

        List<UserResponse> users =  await _userRepository.GetAllAsync(dbConnection, cancellationToken);

        return users;
    }
}
