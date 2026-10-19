using Microsoft.EntityFrameworkCore;

namespace ClientMonitoring.Web.Data
{
    public class ReadApplicationDbContext : ApplicationDbContext
    {
        public ReadApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            // Ensure no tracking for all queries from this context
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        // Override SaveChanges to prevent writes
        public override int SaveChanges()
        {
            throw new InvalidOperationException("This is a read-only context. Write operations are not allowed.");
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            throw new InvalidOperationException("This is a read-only context. Write operations are not allowed.");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new InvalidOperationException("This is a read-only context. Write operations are not allowed.");
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            throw new InvalidOperationException("This is a read-only context. Write operations are not allowed.");
        }
        
    }


}
