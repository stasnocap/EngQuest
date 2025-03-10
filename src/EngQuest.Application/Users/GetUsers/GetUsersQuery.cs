using EngQuest.Application.Abstractions.Messaging;
using EngQuest.Domain.Users;
using MediatR;

namespace EngQuest.Application.Users.GetUsers;

public record GetUsersQuery : IRequest<IReadOnlyList<UserResponse>>;