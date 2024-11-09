﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Polyglot.Domain.Users;

namespace Polyglot.Infrastructure.Configurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");

        builder.HasKey(role => role.Id);

        builder.HasMany(role => role.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();

        builder.HasData(Role.Registered);
    }
}
