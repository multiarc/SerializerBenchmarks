namespace SerializersBenchmark.Network.Abstractions;

public interface ITcpClient
{
    Task ConnectAsync();
}