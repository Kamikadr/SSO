using Microsoft.EntityFrameworkCore;
using SSO.Entities;

namespace SSO.Database;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<AuthData> AuthDatas { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<GroupRole> GroupRoles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasAlternateKey(u => u.Email);
        modelBuilder.Entity<Group>().HasAlternateKey(g => g.Name);
        modelBuilder.Entity<Role>().HasAlternateKey(r => r.Name);
        modelBuilder.Entity<Service>().HasAlternateKey(s => s.Name);

        modelBuilder.Entity<Group>()
            .HasMany(g => g.Roles)
            .WithMany(s => s.Groups)
            .UsingEntity<GroupRole>();
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Services)
            .WithMany(d => d.Users)
            .UsingEntity<AuthData>();

        modelBuilder.Entity<User>()
            .HasMany(u => u.Groups)
            .WithMany(p => p.Users)
            .UsingEntity<UserGroup>(
                j => j
                    .HasOne(pt => pt.Group)
                    .WithMany(t => t.UserGroups)
                    .HasForeignKey(pt => pt.GroupId),
                j => j
                    .HasOne(pt => pt.User)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(pt => pt.UserId),
                j => j.HasKey(t => new { t.GroupId, t.UserId }));
        
        modelBuilder.Entity<UserGroup>()
            .HasOne(ug => ug.InvitedByUser)
            .WithMany()
            .HasForeignKey(ug => ug.InvitedByUserId);
        
        modelBuilder.Entity<Role>()
            .HasMany(r => r.Permissions)
            .WithMany(p => p.Roles)
            .UsingEntity<RolePermission>();
        
    }

}