using System.Net.Sockets;
using SerializersBenchmark.Base;
using SerializersBenchmark.Network.Abstractions;

namespace SerializersBenchmark.Network;

public sealed class Rabbit(ISerializerTestAsync serializer, int port, bool useBufferedStream = false) : IRabbit, ITcpClient
{
    private readonly TcpClient _tcpClient = new();
    private NetworkStream _networkStream;

    public async Task ConnectAsync()
    {
        await _tcpClient.ConnectAsync("127.0.0.1", port);
        _networkStream = _tcpClient.GetStream();
    }

    public async Task SendAsync(object value, int repeatCount)
    {
        Stream stream = _networkStream;
        if (useBufferedStream)
        {
            stream = new BufferedStream(stream);
        }

        for (var i = 0; i < repeatCount; i++)
        {
            await serializer.SerializeAsync(value, stream).ConfigureAwait(false);
        }

        await _networkStream.FlushAsync().ConfigureAwait(false);
    }

    public async Task<object> ReceiveAsync(int expectedSize)
    {
        Stream stream = new LimitedStreamReader(_networkStream, expectedSize);
        if (useBufferedStream)
        {
            stream = new BufferedStream(stream);
        }
        return await serializer.DeserializeAsync(stream).ConfigureAwait(false);
    }

    public void Dispose()
    {
        _networkStream?.Dispose();
        _tcpClient?.Dispose();
    }
}