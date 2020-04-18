using Ziv.CodeExample.Abstractions;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using System;

namespace Ziv.CodeExample.Database
{
    public static class Entry
    {
        public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, Action<DbContextOptionsBuilder> optionsAction = null)
        {
            serviceCollection.AddDbContext<AppDbContext>(optionsAction);
            return serviceCollection.AddScoped<IDbRepository, DbRepository>();
        }
    }
}