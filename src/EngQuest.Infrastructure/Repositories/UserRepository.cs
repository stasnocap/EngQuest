﻿using EngQuest.Domain.Users;

namespace EngQuest.Infrastructure.Repositories;

internal sealed class UserRepository(ApplicationDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    public override void Add(User user)
    {
        foreach (Role role in user.Roles)
        {
            DbContext.Attach(role);
        }

        DbContext.Add(user);
    }
}
