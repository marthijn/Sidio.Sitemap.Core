Sitemap.Core
=============
Sitemap.Core is a lightweight .NET library for generating [sitemaps](https://www.sitemaps.org/). It supports sitemap index files and can be used in any .NET application. It is written in C# and is available via NuGet.

[![build](https://github.com/marthijn/Sitemap.Core/actions/workflows/build.yml/badge.svg)](https://github.com/marthijn/Sitemap.Core/actions/workflows/build.yml)
![NuGet Version](https://img.shields.io/nuget/v/Sitemap.Core)

# Installation

# Usage

# Benchmarks XmlSerializer sync/async
```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3007/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


```
| Method         | NumberOfNodes | Mean         | Error      | StdDev     | Median       |
|--------------- |-------------- |-------------:|-----------:|-----------:|-------------:|
| **Serialize**      | **10**            |     **2.116 μs** |  **0.0423 μs** |  **0.1186 μs** |     **2.069 μs** |
| SerializeAsync | 10            |     3.084 μs |  0.0617 μs |  0.1478 μs |     3.005 μs |
| **Serialize**      | **100**           |    **14.535 μs** |  **0.2853 μs** |  **0.2669 μs** |    **14.539 μs** |
| SerializeAsync | 100           |    23.972 μs |  0.4581 μs |  0.4704 μs |    23.928 μs |
| **Serialize**      | **40000**         | **6,638.247 μs** | **81.9893 μs** | **72.6814 μs** | **6,621.218 μs** |
| SerializeAsync | 40000         | 6,786.160 μs | 41.9065 μs | 37.1491 μs | 6,788.726 μs |



# References
- [Sitemap protocol](https://www.sitemaps.org/protocol.html)
- [Sitemaps on Google Search Central](https://developers.google.com/search/docs/crawling-indexing/sitemaps/overview)