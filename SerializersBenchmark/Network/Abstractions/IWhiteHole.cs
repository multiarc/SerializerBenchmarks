namespace SerializersBenchmark.Network.Abstractions;

public interface IWhiteHole : IDisposable
{
    void SpawnNext(Queue<byte[]> value);
}