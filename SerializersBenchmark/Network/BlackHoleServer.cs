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
            while (true)
            {
                await SwallowChunk(stream, 81920);
            }
        }
        catch when (TeardownStarted)
        {
            //skip
        }
    }
    
    private static async ValueTask SwallowChunk(Stream source, int bytesToSwallow)
    {
        var buffer = ArrayPool<byte>.Shared.Rent(81920);

        try
        {
            var bytesRead = 0;

            while (bytesRead < bytesToSwallow)
            {
                var bytesToRead = Math.Min(buffer.Length, bytesToSwallow - bytesRead);
                var read = await source.ReadAsync(buffer, 0, bytesToRead);
                if (read == 0)
                {
                    throw new EndOfStreamException();
                }

                bytesRead += read;
            }
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }
}