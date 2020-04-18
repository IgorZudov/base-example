using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ziv.CodeExample.Abstractions
{
    public interface IDbRepository
    {
        Task<T> Find<T>(long id) where T : class;
        Task Create<T>(T entity) where T : class;

        Task CreateRange<T>(ICollection<T> entities) where T : class;

        void Update<T>(T entity) where T : class;

        void UpdateRange<T>(ICollection<T> entities) where T : class;

        void Delete<T>(T entity, bool loadNavigation = true) where T : class;

        void DeleteRange<T>(ICollection<T> entity) where T : class;

        IQueryable<T> Query<T>() where T : class;

        IQueryable<T> QueryAll<T>() where T : class;

        Task Commit();

        Task<bool> IsExistByMember<T>(Expression<Func<T, bool>> selector) where T : class;

        Task ResetChangeTracking();
    }
}