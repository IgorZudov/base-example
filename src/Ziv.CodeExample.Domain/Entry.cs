using Ziv.CodeExample.Abstractions;
using Ziv.CodeExample.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ziv.CodeExample
{
    public static class Entry
    {
        public static IServiceCollection AddDomain(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddScoped<IUserService, UserService>()
                .AddScoped<ICommentService, CommentService>();
        }
    }
}