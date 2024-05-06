namespace SerializersBenchmark.Network.Abstractions;

public interface IRabbit: IDisposable
{
    Task SendAsync(object value, int repeatCount);
    Task<object> ReceiveAsync(int expectedSize);
}