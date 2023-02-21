using BrskTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace BrskTestTask.Data;

public class AutoContext : DbContext
{
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Model> Models { get; set; }

    public AutoContext(DbContextOptions<AutoContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}
