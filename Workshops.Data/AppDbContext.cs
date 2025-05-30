using Microsoft.EntityFrameworkCore;
using Workshops.Data.Entities;

namespace Workshops.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
}