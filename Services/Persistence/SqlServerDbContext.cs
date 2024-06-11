using LogApi.Interfaces;
using LogApi.Models;
using LogApi.Services.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace LogApi.Services.Persistence;

public class SqlServerDbContext(DbContextOptions options) : DbContext(options), SqlServerDbContextInterface
{
    public DbSet<UserModel> Users => Set<UserModel>();
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(true, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}