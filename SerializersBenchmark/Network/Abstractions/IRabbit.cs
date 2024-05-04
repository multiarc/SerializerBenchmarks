namespace SerializersBenchmark.Network.Abstractions;

public interface IRabbit: IDisposable
{
    Task SendAsync(object value);
    Task<object> ReceiveAsync(int expectedSize);
}