Sidio.Sitemap.Core
=============
Sidio.Sitemap.Core is a lightweight .NET library for generating [sitemaps](https://www.sitemaps.org/). It supports sitemap index files and can be used in any .NET application. It is written in C# and is available via NuGet.

[![build](https://github.com/marthijn/Sidio.Sitemap.Core/actions/workflows/build.yml/badge.svg)](https://github.com/marthijn/Sidio.Sitemap.Core/actions/workflows/build.yml)
[![NuGet Version](https://img.shields.io/nuget/v/Sidio.Sitemap.Core)](https://www.nuget.org/packages/Sidio.Sitemap.Core/)

# Installation
Add [the package](https://www.nuget.org/packages/Sidio.Sitemap.Core/) to your project.

# Usage
_Looking for ASP.NET Core integration, see [Sidio.Sitemap.AspNetCore](https://github.com/marthijn/Sidio.Sitemap.AspNetCore)._
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

### News sitemaps
```csharp
var sitemap = new Sitemap();
sitemap.Add(new SitemapNewsNode("https://example.com/page.html", "title", "name", "EN", DateTimeOffset.UtcNow));
```

# Benchmarks XmlSerializer sync/async (Sitemap)
```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3007/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


```
| Method         | NumberOfNodes | Mean          | Error       | StdDev      |
|--------------- |-------------- |--------------:|------------:|------------:|
| **Serialize**      | **10**            |      **4.316 μs** |   **0.0825 μs** |   **0.0772 μs** |
| SerializeAsync | 10            |      5.367 μs |   0.0769 μs |   0.0681 μs |
| **Serialize**      | **100**           |     **33.616 μs** |   **0.1583 μs** |   **0.1480 μs** |
| SerializeAsync | 100           |     41.328 μs |   0.3361 μs |   0.3144 μs |
| **Serialize**      | **40000**         | **19,396.188 μs** | **380.0968 μs** | **568.9109 μs** |
| SerializeAsync | 40000         | 20,183.385 μs | 399.3607 μs | 644.8931 μs |

# References
- [Sitemap protocol](https://www.sitemaps.org/protocol.html)
- [Sitemaps on Google Search Central](https://developers.google.com/search/docs/crawling-indexing/sitemaps/overview)