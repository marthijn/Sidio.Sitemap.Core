using BenchmarkDotNet.Running;

namespace Sitemap.Core.Benchmarks;

public sealed class Program
{
    public static void Main(string[] args)
    {
        _ = BenchmarkRunner.Run<XmlSerializerBenchmarks>();
    }
}