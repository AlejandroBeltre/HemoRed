using Microsoft.EntityFrameworkCore;
using backend.Context;
using backend.Models;

namespace Backend.Tests
{
    public class TestHemoRedContext : HemoRedContext
    {
        public TestHemoRedContext(DbContextOptions<HemoRedContext> options) : base(options)
        {
        }
        
        // Override OnConfiguring to prevent the MySQL connection setup in the base class
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Intentionally left empty to avoid calling base.OnConfiguring
            // which would attempt to configure MySQL
        }
        
        // If necessary, override OnModelCreating to simplify configuration for tests
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call the base implementation to get the basic entity configurations
            base.OnModelCreating(modelBuilder);
            
            // Remove any MySQL-specific configurations that might cause issues
            // This is only needed if the base OnModelCreating has MySQL-specific configurations
        }
    }
}