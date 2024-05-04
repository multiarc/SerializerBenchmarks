namespace SerializersBenchmark.Network.Abstractions;

public interface IWhiteHole : IDisposable
{
    void SpawnNext(byte[] value);
}