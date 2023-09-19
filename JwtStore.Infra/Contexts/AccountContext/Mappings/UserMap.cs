using JwtStore.Core.Contexts.AccountContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtStore.Infra.Contexts.AccountContext.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.Image)
                .HasColumnName("Image")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255)
                .IsRequired();

            builder.OwnsOne(x => x.Email)
                .Property(x => x.Address)
                .HasColumnName("Email")
                .IsRequired();

            builder.OwnsOne(x => x.Email)
                .OwnsOne(x => x.VerificationCode)
                .Property(x => x.Code)
                .HasColumnName("EmailVerificationCode")
                .IsRequired();

            builder.OwnsOne(x => x.Email)
                .OwnsOne(x => x.VerificationCode)
                .Property(x => x.ExpiresAt)
                .HasColumnName("EmailVerificationCodeExpiresAt")
                .IsRequired(false);

            builder.OwnsOne(x => x.Email)
               .OwnsOne(x => x.VerificationCode)
               .Property(x => x.VerifiedAt)
               .HasColumnName("EmailVerificationCodeVerifiedAt")
               .IsRequired(false);

            builder.OwnsOne(x => x.Email)
              .OwnsOne(x => x.VerificationCode)
              .Ignore(x => x.IsActive);

            builder.OwnsOne(x => x.Password)
              .Property(x => x.Hash)
              .HasColumnName("PasswordHash").IsRequired();

            builder.OwnsOne(x => x.Password)
              .Property(x => x.ResetCode)
              .HasColumnName("PasswordResetCode")
              .IsRequired();
        }
    }
}