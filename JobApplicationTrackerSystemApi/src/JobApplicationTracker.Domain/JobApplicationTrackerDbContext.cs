using JobApplicationTracker.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTracker.Domain;

public class JobApplicationTrackerDbContext : DbContext
{
    public DbSet<Application> Applications { get; set; }

    public JobApplicationTrackerDbContext(DbContextOptions<JobApplicationTrackerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Application>()
            .HasKey(application => application.Id);
    }
}