using Microsoft.EntityFrameworkCore;
using VKprofileTaskAPI.Models;

namespace VKprofileTaskAPI.DbContexts;

public class UsersDbContext : DbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<UserState> UserStates { get; set; }
    public DbSet<UserGroupVariations> UserVariations { get; set; }
    public DbSet<UserStateVariations> UserStatesVariations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<UserGroupVariations>()

            .HasData(
                new UserGroupVariations { CodeId = 1, Type = "Admin" },
                new UserGroupVariations { CodeId = 2, Type = "User" }
            );
        modelBuilder
            .Entity<UserStateVariations>()

            .HasData(
                new UserStateVariations { CodeId = 1, Type = "Active" },
                new UserStateVariations { CodeId = 2, Type = "Blocked" }
            );
    }

}
