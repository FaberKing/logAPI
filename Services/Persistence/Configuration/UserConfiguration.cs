using LogApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serilog;

namespace LogApi.Services.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);
        builder.Property(e => e.Email).HasColumnType("Nvarchar").HasMaxLength(100);
        builder.Property(e => e.Password).HasColumnType("Nvarchar(max)");
    }
}