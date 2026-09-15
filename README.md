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
var xmlResult = service.Serialize(sitemap);
```

## Sitemap index
```csharp
var sitemapIndexNodes = new List<SitemapIndexNode> { new("https://example.com/sitemap-1.xml") };
var sitemapIndex = new SitemapIndex(sitemapIndexNodes);
var service = new SitemapIndexService(new XmlSerializer());
var xmlResult = service.Serialize(sitemapIndex);
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
        var xmlResult = service.Serialize(sitemap);
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

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.9445/25H2/2025Update/HudsonValley2)
AMD Ryzen 7 5800H with Radeon Graphics 3.20GHz, 1 CPU, 16 logical and 8 physical cores                                                                                                                                                                                           
.NET SDK 10.0.302                                                                                                                                                                                                                                                                
  [Host]    : .NET 10.0.12 (10.0.12, 10.0.1226.42308), X64 RyuJIT x86-64-v3                                                                                                                                                                                                      
  .NET 10.0 : .NET 10.0.12 (10.0.12, 10.0.1226.42308), X64 RyuJIT x86-64-v3                                                                                                                                                                                                      
  .NET 8.0  : .NET 8.0.31 (8.0.31, 8.0.3126.42015), X64 RyuJIT x86-64-v3                                                                                                                                                                                                         
  .NET 9.0  : .NET 9.0.18 (9.0.18, 9.0.1826.31522), X64 RyuJIT x86-64-v3 

```
| Method         | Job       | Runtime   | NumberOfNodes | Mean          | Error       | StdDev        | Ratio | RatioSD |
|--------------- |---------- |---------- |-------------- |--------------:|------------:|--------------:|------:|--------:|
| Serialize      | .NET 10.0 | .NET 10.0 | 10            |      4.940 us |   0.0981 us |     0.0918 us |  0.84 |    0.04 |                                                                                                                                                       
| Serialize      | .NET 8.0  | .NET 8.0  | 10            |      5.865 us |   0.1168 us |     0.2360 us |  1.00 |    0.06 |
| Serialize      | .NET 9.0  | .NET 9.0  | 10            |      5.354 us |   0.0611 us |     0.0571 us |  0.91 |    0.04 |
|                |           |           |               |               |             |               |       |         |
| SerializeAsync | .NET 10.0 | .NET 10.0 | 10            |      6.328 us |   0.1233 us |     0.2192 us |  0.80 |    0.03 |
| SerializeAsync | .NET 8.0  | .NET 8.0  | 10            |      7.930 us |   0.1081 us |     0.1011 us |  1.00 |    0.02 |
| SerializeAsync | .NET 9.0  | .NET 9.0  | 10            |      6.902 us |   0.1270 us |     0.2051 us |  0.87 |    0.03 |
|                |           |           |               |               |             |               |       |         |
| Serialize      | .NET 10.0 | .NET 10.0 | 100           |     37.636 us |   0.3136 us |     0.2619 us |  0.79 |    0.01 |
| Serialize      | .NET 8.0  | .NET 8.0  | 100           |     47.350 us |   0.4471 us |     0.3964 us |  1.00 |    0.01 |
| Serialize      | .NET 9.0  | .NET 9.0  | 100           |     41.813 us |   0.4453 us |     0.3948 us |  0.88 |    0.01 |
|                |           |           |               |               |             |               |       |         |
| SerializeAsync | .NET 10.0 | .NET 10.0 | 100           |     45.750 us |   0.2481 us |     0.2072 us |  0.82 |    0.01 |
| SerializeAsync | .NET 8.0  | .NET 8.0  | 100           |     55.988 us |   0.3038 us |     0.2693 us |  1.00 |    0.01 |
| SerializeAsync | .NET 9.0  | .NET 9.0  | 100           |     51.098 us |   0.3159 us |     0.2638 us |  0.91 |    0.01 |
|                |           |           |               |               |             |               |       |         |
| Serialize      | .NET 10.0 | .NET 10.0 | 40000         | 21,965.477 us | 184.3410 us |   163.4135 us |  0.88 |    0.04 |
| Serialize      | .NET 8.0  | .NET 8.0  | 40000         | 24,991.718 us | 464.2925 us | 1,278.7964 us |  1.00 |    0.07 |
| Serialize      | .NET 9.0  | .NET 9.0  | 40000         | 23,005.318 us | 344.1842 us |   305.1103 us |  0.92 |    0.05 |
|                |           |           |               |               |             |               |       |         |
| SerializeAsync | .NET 10.0 | .NET 10.0 | 40000         | 22,025.450 us | 232.3676 us |   194.0375 us |  0.86 |    0.02 |
| SerializeAsync | .NET 8.0  | .NET 8.0  | 40000         | 25,542.654 us | 498.9985 us |   715.6489 us |  1.00 |    0.04 |
| SerializeAsync | .NET 9.0  | .NET 9.0  | 40000         | 24,630.967 us | 486.8769 us |   633.0775 us |  0.97 |    0.04 |

```
dotnet run -c Release -- --job short --runtimes net8.0 net9.0 net10.0   
```

# References
- [Sitemap protocol](https://www.sitemaps.org/protocol.html)
- [Sitemaps on Google Search Central](https://developers.google.com/search/docs/crawling-indexing/sitemaps/overview)