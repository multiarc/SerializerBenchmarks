using BenchmarkDotNet.Running;

namespace SerializersBenchmark;

public class Program
{
    static void Main(string[] args)
    {
        //dotnet run -c Release -f net8.0 -- --job default --runtimes net8.0 --filter *AsyncBenchmarks*
        //dotnet run -c Release -f net6.0 -- --job default --runtimes net6.0 --filter *
        //dotnet run -c Release -f net48 -- --job default --runtimes net48 --filter *
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}