Sidio.Sitemap.Core
=============
Sidio.Sitemap.Core is a lightweight .NET library for generating [sitemaps](https://www.sitemaps.org/). It supports sitemap index files and can be used in any .NET application. It is written in C# and is available via NuGet.

[![NuGet Version](https://img.shields.io/nuget/v/Sidio.Sitemap.Core)](https://www.nuget.org/packages/Sidio.Sitemap.Core/)

# Versions

|            | [Sidio.Sitemap.Core](https://github.com/marthijn/Sidio.Sitemap.Core)| [Sidio.Sitemap.AspNetCore](https://github.com/marthijn/Sidio.Sitemap.AspNetCore)                                                                                                                                                               | [Sidio.Sitemap.Blazor](https://github.com/marthijn/Sidio.Sitemap.Blazor)                                                                                                                                                           |
|------------|---------------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| *NuGet*    | [![NuGet Version](https://img.shields.io/nuget/v/Sidio.Sitemap.Core)](https://www.nuget.org/packages/Sidio.Sitemap.Core/) | [![NuGet Version](https://img.shields.io/nuget/v/Sidio.Sitemap.AspNetCore)](https://www.nuget.org/packages/Sidio.Sitemap.AspNetCore/)                                                      | [![NuGet Version](https://img.shields.io/nuget/v/Sidio.Sitemap.Blazor)](https://www.nuget.org/packages/Sidio.Sitemap.Blazor/)                                                      |
| *Build*    | [![build](https://github.com/marthijn/Sidio.Sitemap.Core/actions/workflows/build.yml/badge.svg)](https://github.com/marthijn/Sidio.Sitemap.Core/actions/workflows/build.yml)| [![build](https://github.com/marthijn/Sidio.Sitemap.AspNetCore/actions/workflows/build.yml/badge.svg)](https://github.com/marthijn/Sidio.Sitemap.AspNetCore/actions/workflows/build.yml)   | [![build](https://github.com/marthijn/Sidio.Sitemap.Blazor/actions/workflows/build.yml/badge.svg)](https://github.com/marthijn/Sidio.Sitemap.Blazor/actions/workflows/build.yml)   |
| *Coverage* | [![Coverage Status](https://coveralls.io/repos/github/marthijn/Sidio.Sitemap.Core/badge.svg?branch=main)](https://coveralls.io/github/marthijn/Sidio.Sitemap.Core?branch=main)| [![Coverage Status](https://coveralls.io/repos/github/marthijn/Sidio.Sitemap.AspNetCore/badge.svg?branch=main)](https://coveralls.io/github/marthijn/Sidio.Sitemap.AspNetCore?branch=main) | [![Coverage Status](https://coveralls.io/repos/github/marthijn/Sidio.Sitemap.Blazor/badge.svg?branch=main)](https://coveralls.io/github/marthijn/Sidio.Sitemap.Blazor?branch=main) |
| *Requirements*|.NET Standard, .NET 8+, | .NET 8+, AspNetCore|.NET 8+, AspNetCore, Blazor server|


# Installation
Add [the package](https://www.nuget.org/packages/Sidio.Sitemap.Core/) to your project.

# Usage
_Looking for ASP.NET Core integration, see [Sidio.Sitemap.AspNetCore](https://github.com/marthijn/Sidio.Sitemap.AspNetCore). For Blazor integration, see [Sidio.Sitemap.Blazor](https://github.com/marthijn/Sidio.Sitemap.Blazor)._
## Sitemap
```csharp
var nodes = new List<SitemapNode> { new ("https://example.com/page.html") };
var sitemap = new Sitemap(nodes);
var service = new SitemapService(new XmlSerializer());
var xmlResult = service.Serialize();
```

## Sitemap index
```csharp
var sitemapIndexNodes = new List<SitemapIndexNode> { new("https://example.com/sitemap-1.xml") };
var sitemapIndex = new SitemapIndex(sitemapIndexNodes);
var service = new SitemapIndexService(new XmlSerializer());
var xmlResult = service.Serialize();
```

## Dependency injection
```csharp
// DI setup
services.AddDefaultSitemapServices();

// implementation
public class MyClass()
{
    public MyClass(ISitemapService service)
    {
        var nodes = new List<SitemapNode> { new ("https://example.com/page.html") };
        var sitemap = new Sitemap(nodes);
        var xmlResult = service.Serialize();
    }
}    
```

## Working with relative URLs
```csharp
public class MyBaseUrlProvider : IBaseUrlProvider
{
    public Uri BaseUrl => new ("https://example.com", UriKind.Absolute);
}

// DI setup
services.AddBaseUrlProvider<MyBaseUrlProvider>();
services.AddDefaultSitemapServices();
// or in one function:
services.AddDefaultSitemapServices<MyBaseUrlProvider>();

// regular setup
var serializer = new XmlSerializer(new MyBaseUrlProvider());
var service = new SitemapService(serializer);

// nodes, relative urls
var nodes = new List<SitemapNode> { new ("page.html") };
```

## Extensions
### Image sitemaps
```csharp
var sitemap = new Sitemap();
sitemap.Add(new SitemapImageNode("https://example.com/page.html", new ImageLocation("https://example.com/image.png")));
```
[Extension documentation on Google Search Central](https://developers.google.com/search/docs/crawling-indexing/sitemaps/image-sitemaps)

### News sitemaps
```csharp
var sitemap = new Sitemap();
sitemap.Add(new SitemapNewsNode("https://example.com/page.html", "title", "name", "EN", DateTimeOffset.UtcNow));
```
[Extension documentation on Google Search Central](https://developers.google.com/search/docs/crawling-indexing/sitemaps/news-sitemap)

### Video sitemaps
```csharp
var video = new VideoContent("https://example.com/thumbnail.png", "title", "description", "https://example.com/video.mp4", null);
var sitemap = new Sitemap();
sitemap.Add(new SitemapVideoNode("https://example.com/page.html", video));
```
[Extension documentation on Google Search Central](https://developers.google.com/search/docs/crawling-indexing/sitemaps/video-sitemaps)

## Stylesheets
XSLT stylesheets for sitemaps and sitemap indexes are supported. The stylesheet can be added to the Sitemap or SitemapIndex object:
```csharp
var sitemap = new Sitemap(nodes, "my-stylesheet.xslt");
```
For more information, see [Sitemap Style](https://www.sitemap.style/).

# Deserialization
It is possible to load existing XML and deserialize it into a sitemap object:
```csharp
var xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset> ....";
var serializer = services.GetRequiredService<ISitemapSerializer>();
var sitemap = serializer.Deserialize(xml);
```

# Benchmarks XmlSerializer sync/async (Sitemap)
```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4460/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.100
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

```
| Method         | Job      | Runtime  | NumberOfNodes | Mean          | Error       | StdDev      | Ratio | RatioSD |
|--------------- |--------- |--------- |-------------- |--------------:|------------:|------------:|------:|--------:|
| Serialize      | .NET 8.0 | .NET 8.0 | 10            |      5.153 us |   0.0971 us |   0.0758 us |  1.00 |    0.02 |
| Serialize      | .NET 9.0 | .NET 9.0 | 10            |      4.585 us |   0.0894 us |   0.0792 us |  0.89 |    0.02 |
|                |          |          |               |               |             |             |       |         |
| SerializeAsync | .NET 8.0 | .NET 8.0 | 10            |      6.312 us |   0.0733 us |   0.0650 us |  1.00 |    0.01 |
| SerializeAsync | .NET 9.0 | .NET 9.0 | 10            |      5.482 us |   0.0189 us |   0.0167 us |  0.87 |    0.01 |
|                |          |          |               |               |             |             |       |         |
| Serialize      | .NET 8.0 | .NET 8.0 | 100           |     41.446 us |   0.4271 us |   0.3995 us |  1.00 |    0.01 |
| Serialize      | .NET 9.0 | .NET 9.0 | 100           |     38.711 us |   0.3524 us |   0.3124 us |  0.93 |    0.01 |
|                |          |          |               |               |             |             |       |         |
| SerializeAsync | .NET 8.0 | .NET 8.0 | 100           |     51.229 us |   0.4338 us |   0.4057 us |  1.00 |    0.01 |
| SerializeAsync | .NET 9.0 | .NET 9.0 | 100           |     46.347 us |   0.7401 us |   0.6923 us |  0.90 |    0.01 |
|                |          |          |               |               |             |             |       |         |
| Serialize      | .NET 8.0 | .NET 8.0 | 40000         | 23,239.956 us | 428.2431 us | 400.5788 us |  1.00 |    0.02 |
| Serialize      | .NET 9.0 | .NET 9.0 | 40000         | 23,396.317 us | 334.0125 us | 312.4355 us |  1.01 |    0.02 |
|                |          |          |               |               |             |             |       |         |
| SerializeAsync | .NET 8.0 | .NET 8.0 | 40000         | 23,490.278 us | 251.5840 us | 223.0227 us |  1.00 |    0.01 |
| SerializeAsync | .NET 9.0 | .NET 9.0 | 40000         | 23,334.005 us | 253.3734 us | 237.0057 us |  0.99 |    0.01 |


# References
- [Sitemap protocol](https://www.sitemaps.org/protocol.html)
- [Sitemaps on Google Search Central](https://developers.google.com/search/docs/crawling-indexing/sitemaps/overview)