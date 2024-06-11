using LogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LogApi.Interfaces;

public interface SqlServerDbContextInterface
{
    DbSet<UserModel> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}