using System.Net.Sockets;
using SerializersBenchmark.Base;
using SerializersBenchmark.Network.Abstractions;

namespace SerializersBenchmark.Network;

public sealed class Rabbit(ISerializerTestAsync serializer, int port) : IRabbit, ITcpClient
{
    private readonly TcpClient _tcpClient = new();
    private NetworkStream _networkStream;

    public async Task ConnectAsync()
    {
        await _tcpClient.ConnectAsync("127.0.0.1", port);
        _networkStream = _tcpClient.GetStream();
    }
    
    public async Task SendAsync(object value)
    {
        await serializer.SerializeAsync(value, _networkStream).ConfigureAwait(false);
        await _networkStream.FlushAsync();
    }

    public async Task<object> ReceiveAsync(int expectedSize)
    {
        return await serializer.DeserializeAsync(new LimitedStreamReader(_networkStream, expectedSize)).ConfigureAwait(false);
    }

    public void Dispose()
    {
        _networkStream?.Dispose();
        _tcpClient?.Dispose();
    }
}