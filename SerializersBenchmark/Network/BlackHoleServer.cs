using System.Buffers;
using System.Net;
using System.Net.Sockets;
using SerializersBenchmark.Base;
using SerializersBenchmark.Network.Abstractions;

namespace SerializersBenchmark.Network;

/// <summary>
/// TCP based server using loopback to consume infinite amount of serialized values with deserialization
/// </summary>
public class BlackHoleServer(int port) : TcpServer(port), IBlackHole
{
    protected override async Task OnClientConnected(TcpClient client)
    {
        using var stream = client.GetStream();

        try
        {
            await SwallowEverything(stream);
        }
        catch when (TeardownStarted)
        {
            //skip
        }
    }

    private static readonly byte[] Buffer = new byte[81920];

    private static async ValueTask SwallowEverything(Stream source)
    {
        while (true)
        {
            var read = await source.ReadAsync(Buffer, 0, Buffer.Length);
            if (read == 0)
            {
                throw new EndOfStreamException();
            }
        }
    }
}