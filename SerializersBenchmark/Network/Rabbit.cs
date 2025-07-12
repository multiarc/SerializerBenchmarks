using System.Net.Sockets;
using SerializersBenchmark.Base;
using SerializersBenchmark.Network.Abstractions;

namespace SerializersBenchmark.Network;

public sealed class Rabbit : IRabbit, ITcpClient
{
    private readonly TcpClient _tcpClient;
    private NetworkStream _networkStream;
    private readonly ISerializerTestAsync _serializer;
    private readonly int _port;
    private readonly bool _useBufferedStream;
    public Rabbit(ISerializerTestAsync serializer, int port, bool useBufferedStream = false) {
        _serializer = serializer;
        _port = port;
        _useBufferedStream = useBufferedStream;
        _tcpClient = new TcpClient();
        _tcpClient.Client.NoDelay = true; //disable Nagle's algorithm for low latency
    }

    public async Task ConnectAsync() {
        await _tcpClient.ConnectAsync("127.0.0.1", _port);
        _networkStream = _tcpClient.GetStream();
    }

    public async Task SendAsync(object value, int repeatCount)
    {
        Stream stream = _networkStream;
        if (_useBufferedStream)
        {
            stream = new BufferedStream(stream);
        }

        for (var i = 0; i < repeatCount; i++)
        {
            await _serializer.SerializeAsync(value, stream).ConfigureAwait(false);
        }

        await stream.FlushAsync().ConfigureAwait(false);
    }

    public async Task<object> ReceiveAsync(int expectedSize)
    {
        Stream stream = new LimitedStreamReader(_networkStream, expectedSize);
        if (_useBufferedStream)
        {
            stream = new BufferedStream(stream);
        }
        return await _serializer.DeserializeAsync(stream).ConfigureAwait(false);
    }

    public void Dispose()
    {
        _networkStream?.Dispose();
        _tcpClient?.Dispose();
    }
}