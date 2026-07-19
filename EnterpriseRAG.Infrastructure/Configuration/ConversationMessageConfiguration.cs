using EnterpriseRAG.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Infrastructure.Configuration
{
    public class ConversationMessageConfiguration
    : IEntityTypeConfiguration<ConversationMessage>
    {
        public void Configure(
            EntityTypeBuilder<ConversationMessage> builder)
        {
            builder.ToTable("ConversationMessages");
            builder.HasKey(x => x.MessageId);
            builder.Property(x => x.Role)
                   .HasMaxLength(20)
                   .IsRequired();
            builder.Property(x => x.Message)
                   .IsRequired();

            builder.Property(x => x.CreatedOn)
                   .IsRequired();
        }
    }

}
