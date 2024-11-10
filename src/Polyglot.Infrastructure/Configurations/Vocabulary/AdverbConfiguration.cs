﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Polyglot.Domain.Shared;
using Polyglot.Domain.Vocabulary.Adverbs;

namespace Polyglot.Infrastructure.Configurations.Vocabulary;

public class AdverbConfiguration : IEntityTypeConfiguration<Adverb>
{
    public void Configure(EntityTypeBuilder<Adverb> builder)
    {
        builder.ToTable("adverbs");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Text)
            .HasMaxLength(50)
            .HasConversion(text => text.Value, value => new Text(value));

        builder.Property(a => a.Type);
    }
}