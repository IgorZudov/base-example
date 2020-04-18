using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ziv.CodeExample.Abstractions;

namespace Ziv.CodeExample.Database
{
    public class DbRepository : IDbRepository
    {
        private readonly AppDbContext _context;

        public DbRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<T> Find<T>(long id) where T : class =>
            _context.Set<T>().FindAsync(id);

        public async Task Create<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public Task CreateRange<T>(ICollection<T> entities) where T : class =>
            _context.Set<T>().AddRangeAsync(entities);

        public void Update<T>(T entity) where T : class =>
            _context.Update(entity);

        public void UpdateRange<T>(ICollection<T> entities) where T : class =>
            _context.UpdateRange(entities);

        public void Delete<T>(T entity, bool loadNavigation = true) where T : class =>
            _context.Remove(entity);


        public void DeleteRange<T>(ICollection<T> entities) where T : class
        {
            var set = _context.Set<T>();
            foreach (var entity in entities)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                    set.Attach(entity);
            }

            set.RemoveRange(entities);
        }

        public IQueryable<T> Query<T>() where T : class => _context.Set<T>().AsNoTracking();

        public IQueryable<T> QueryAll<T>() where T : class => _context.Set<T>();

        public Task Commit() => _context.SaveChangesAsync();

        public Task<bool> IsExistByMember<T>(Expression<Func<T, bool>> selector)
            where T : class => _context.Set<T>().AsNoTracking().AnyAsync(selector);

        public Task ResetChangeTracking()
        {
            var changedEntriesCopy = _context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
            return Task.CompletedTask;
        }
    }
}