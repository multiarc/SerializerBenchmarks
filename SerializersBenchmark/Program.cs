using BenchmarkDotNet.Running;

namespace SerializersBenchmark;

public class Program
{
    static void Main(string[] args)
    {
        BenchmarkRunner.Run<Benchmarks>();
    }
}