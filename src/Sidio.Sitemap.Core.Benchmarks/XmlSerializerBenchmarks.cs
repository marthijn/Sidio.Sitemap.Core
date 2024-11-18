using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Sidio.Sitemap.Core.Serialization;

namespace Sidio.Sitemap.Core.Benchmarks;

[ExcludeFromCodeCoverage]
[SimpleJob(RuntimeMoniker.Net80, baseline: true)]
[SimpleJob(RuntimeMoniker.Net90)]
public class XmlSerializerBenchmarks
{
    [Params(10, 100, 40000)]
    public int NumberOfNodes;

    private Sitemap? _sitemap;

    private readonly XmlSerializer _serializer = new ();

    [GlobalSetup]
    public void Setup()
    {
        _sitemap = new Sitemap(Enumerable.Range(0, NumberOfNodes).Select(x => new SitemapNode($"https://www.example.com/{x}")));
    }

    [Benchmark]
    public string Serialize()
    {
        return _serializer.Serialize(_sitemap!);
    }

    [Benchmark]
    public async Task<string> SerializeAsync()
    {
        return await _serializer.SerializeAsync(_sitemap!);
    }
}