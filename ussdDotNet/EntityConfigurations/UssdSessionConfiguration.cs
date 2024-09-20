using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ussdDotNet.Models;

namespace ussdDotNet.EntityConfigurations
{
    public class UssdSessionConfiguration : IEntityTypeConfiguration<UssdSession>
    {
        public void Configure(EntityTypeBuilder<UssdSession> builder)
        {
            builder.ToTable("UssdSessions");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.SessionId)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(e => e.Mobile)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.ServiceCode)
                .HasMaxLength(50);

            builder.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.MessageDescription)
               .HasMaxLength(255);

            builder.Property(e => e.Tag)
               .HasMaxLength(100);

            builder.Property(e => e.Operator)
               .HasMaxLength(50);

            builder.Property(e => e.CreatedAt)
               .HasColumnType("datetime");

            builder.Property(e => e.UpdatedAt)
               .HasColumnType("datetime");

            builder.Property(e => e.MerchantId);

            builder.Property(e => e.MerchantName)
               .HasMaxLength(100);

        }
    }
}
