using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private static void UpdateEntities(DbContext? dbContext)
        {
            if (dbContext == null)
                return;

            foreach (var entry in dbContext.ChangeTracker.Entries<IEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOnUtc = DateTime.UtcNow;
                        entry.Entity.UpdatedOnUtc = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedOnUtc = DateTime.UtcNow;
                        break;

                    case EntityState.Detached:
                    case EntityState.Unchanged:
                    case EntityState.Deleted:
                    default:
                        break;
                }
            }
        }
    }
}
