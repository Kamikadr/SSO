using Microsoft.EntityFrameworkCore;
using SSO.Entities;

namespace SSO.Database;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}