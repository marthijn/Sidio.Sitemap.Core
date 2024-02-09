using Microsoft.Extensions.DependencyInjection;
using Sitemap.Core.Serialization;

namespace Sitemap.Core.Services;

/// <summary>
/// The service configuration extensions.
/// </summary>
public static class ServiceConfigurationExtensions
{
    /// <summary>
    /// Adds the sitemap services to the service collection with the default <see cref="XmlSerializer"/>.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddDefaultSitemapServices(this IServiceCollection services)
    {
        services.AddScoped<ISitemapSerializer, XmlSerializer>(s => new XmlSerializer(s.GetService<IBaseUrlProvider>()));
        services.AddScoped<ISitemapService, SitemapService>();
        services.AddScoped<ISitemapIndexService, SitemapIndexService>();
        return services;
    }

    /// <summary>
    /// Adds the sitemap services to the service collection without any implementation of <see cref="ISitemapSerializer"/>.
    /// Call <see cref="AddSitemapSerializer{T}(IServiceCollection)"/> to add a serializer.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddSitemapServices(this IServiceCollection services)
    {
        services.AddScoped<ISitemapService, SitemapService>(x => new SitemapService(x.GetRequiredService<ISitemapSerializer>()));
        services.AddScoped<ISitemapIndexService, SitemapIndexService>(x => new SitemapIndexService(x.GetRequiredService<ISitemapSerializer>()));
        return services;
    }

    /// <summary>
    /// Adds the sitemap serializer to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <typeparam name="T">The sitemap serializer type.</typeparam>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddSitemapSerializer<T>(this IServiceCollection services)
        where T : class, ISitemapSerializer
    {
        services.AddScoped<ISitemapSerializer, T>();
        return services;
    }

    /// <summary>
    /// Adds the sitemap serializer to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="implementationFactory">The implementation factory.</param>
    /// <typeparam name="T">The sitemap serializer type.</typeparam>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddSitemapSerializer<T>(this IServiceCollection services, Func<IServiceProvider, T> implementationFactory)
        where T : class, ISitemapSerializer
    {
        services.AddScoped<ISitemapSerializer, T>(implementationFactory);
        return services;
    }

    /// <summary>
    /// Adds the base URL provider to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <typeparam name="T">The base URL provider implementation type.</typeparam>
    /// <returns>The <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddBaseUrlProvider<T>(this IServiceCollection services)
        where T : class, IBaseUrlProvider
    {
        services.AddScoped<IBaseUrlProvider, T>();
        return services;
    }

}