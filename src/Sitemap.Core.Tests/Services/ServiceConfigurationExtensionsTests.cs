using Microsoft.Extensions.DependencyInjection;
using Sitemap.Core.Serialization;
using Sitemap.Core.Services;

namespace Sitemap.Core.Tests.Services;

public sealed class ServiceConfigurationExtensionsTests
{
    [Fact]
    public void AddDefaultSitemapServices_ServicesAdded()
    {
        // arrange
        var services = new ServiceCollection();

        // act
        var result = services.AddDefaultSitemapServices();

        // assert
        result.Should().ContainSingle(x => x.ServiceType == typeof(ISitemapSerializer) && x.ImplementationFactory != null);
        result.Should().ContainSingle(x => x.ServiceType == typeof(ISitemapService) && x.ImplementationType == typeof(SitemapService));
        result.Should().ContainSingle(x => x.ServiceType == typeof(ISitemapIndexService) && x.ImplementationType == typeof(SitemapIndexService));
    }

    [Fact]
    public void AddDefaultSitemapServices_WithBaseUrlProvider_ServicesAdded()
    {
        // arrange
        var services = new ServiceCollection();

        // act
        var result = services.AddDefaultSitemapServices<MyBaseUrlProvider>();

        // assert
        result.Should().ContainSingle(x => x.ServiceType == typeof(ISitemapSerializer) && x.ImplementationFactory != null);
        result.Should().ContainSingle(x => x.ServiceType == typeof(ISitemapService) && x.ImplementationType == typeof(SitemapService));
        result.Should().ContainSingle(x => x.ServiceType == typeof(ISitemapIndexService) && x.ImplementationType == typeof(SitemapIndexService));
        result.Should().ContainSingle(x => x.ServiceType == typeof(IBaseUrlProvider) && x.ImplementationType == typeof(MyBaseUrlProvider));
    }

    [Fact]
    public void AddSitemapServices_ServicesAdded()
    {
        // arrange
        var services = new ServiceCollection();

        // act
        var result = services.AddSitemapServices();

        // assert
        result.Should().NotContain(x => x.ServiceType == typeof(ISitemapSerializer));
        result.Should().ContainSingle(x => x.ServiceType == typeof(ISitemapService) && x.ImplementationFactory != null);
        result.Should().ContainSingle(x => x.ServiceType == typeof(ISitemapIndexService)  && x.ImplementationFactory != null);
    }

    [Fact]
    public void AddSitemapSerializer_ServicesAdded()
    {
        // arrange
        var services = new ServiceCollection();

        // act
        var result = services.AddSitemapSerializer<XmlSerializer>();

        // assert
        result.Should().ContainSingle(x => x.ServiceType == typeof(ISitemapSerializer) && x.ImplementationType == typeof(XmlSerializer));
    }

    [Fact]
    public void AddSitemapSerializer_WithFactory_ServicesAdded()
    {
        // arrange
        var services = new ServiceCollection();

        // act
        var result = services.AddSitemapSerializer<XmlSerializer>(_ => new XmlSerializer());

        // assert
        result.Should().ContainSingle(x => x.ServiceType == typeof(ISitemapSerializer) && x.ImplementationFactory != null);
    }

    [Fact]
    public void AddBaseUrlProvider_ServicesAdded()
    {
        // arrange
        var services = new ServiceCollection();

        // act
        var result = services.AddBaseUrlProvider<MyBaseUrlProvider>();

        // assert
        result.Should().ContainSingle(x => x.ServiceType == typeof(IBaseUrlProvider) && x.ImplementationType == typeof(MyBaseUrlProvider));
    }

    private sealed class MyBaseUrlProvider : IBaseUrlProvider
    {
        public Uri BaseUrl => new ("https://example.com");
    }
}