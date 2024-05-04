using BenchmarkDotNet.Running;

namespace SerializersBenchmark;

public class Program
{
    static void Main(string[] args)
    {
        //dotnet run -c Release -f net8.0 -- --runtimes net48 net6.0 net8.0 --filter *Benchmarks*
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}