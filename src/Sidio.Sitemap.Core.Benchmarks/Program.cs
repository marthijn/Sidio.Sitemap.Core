﻿using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Running;

namespace Sidio.Sitemap.Core.Benchmarks;

[ExcludeFromCodeCoverage]
public sealed class Program
{
    public static void Main(string[] args)
    {
        _ = BenchmarkRunner.Run<XmlSerializerBenchmarks>();
    }
}