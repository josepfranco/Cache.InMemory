using Abstractions.Cache;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Cache.InMemory.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the in-memory cache implementation
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInMemoryCache(this IServiceCollection services)
        {
            services.TryAddSingleton<ICache, InMemoryCache>();
            return services;
        }
    }
}