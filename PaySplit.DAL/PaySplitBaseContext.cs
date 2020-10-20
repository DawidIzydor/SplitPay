using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaySplit.DAL.Models;

namespace PaySplit.DAL
{
    public abstract class PaySplitBaseContext : DbContext
    {
        protected PaySplitBaseContext()
        {
        }

        protected PaySplitBaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DbPerson> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        #region SaveChanges

        /// <inheritdoc cref="DbContext" />
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateException">
        ///     An error is encountered while saving to the
        ///     database.
        /// </exception>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        /// <inheritdoc cref="DbContext" />
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateException">
        ///     An error is encountered while saving to the
        ///     database.
        /// </exception>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <inheritdoc cref="DbContext" />
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateException">
        ///     An error is encountered while saving to the
        ///     database.
        /// </exception>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc cref="DbContext" />
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateException">
        ///     An error is encountered while saving to the
        ///     database.
        /// </exception>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        #endregion

        #region TimestampsUpdate

        private void UpdateTimestamps()
        {
            UpdateCreated();
            UpdateLastModified();
        }

        private void UpdateCreated()
        {
            foreach (var entityEntry in ChangeTracker.Entries())
            {
                if (entityEntry.State == EntityState.Added && entityEntry.Entity.GetType().GetProperties()
                    .Any(p => p.Name == nameof(ICreatedTimestamp.Created)))
                {
                    entityEntry.CurrentValues[nameof(ICreatedTimestamp.Created)] = DateTimeOffset.Now;
                }
            }
        }

        private void UpdateLastModified()
        {
            foreach (var entityEntry in ChangeTracker.Entries())
            {
                if ((entityEntry.State == EntityState.Modified || entityEntry.State == EntityState.Added) && entityEntry
                    .Entity.GetType().GetProperties()
                    .Any(p => p.Name == nameof(ILastModifiedTimestamp.LastModified)))
                {
                    entityEntry.CurrentValues[nameof(ILastModifiedTimestamp.LastModified)] = DateTimeOffset.Now;
                }
            }
        }

        #endregion
    }
}