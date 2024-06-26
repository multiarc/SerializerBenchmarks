﻿using System.Net.Sockets;
using SerializersBenchmark.Network.Abstractions;

namespace SerializersBenchmark.Network;

/// <summary>
/// TCP based server using loopback to pass-through values for deserialization asynchronously
/// </summary>
public class WhiteHoleServer : TcpServer, IWhiteHole
{
    private readonly bool _useBufferedStream;
    private TaskCompletionSource<Queue<byte[]>> _dataIsReady;

    public WhiteHoleServer(int port, bool useBufferedStream = false) : base(port)
    {
        _useBufferedStream = useBufferedStream;
        ResetState();
    }

    public void SpawnNext(Queue<byte[]> value)
    {
        _dataIsReady.TrySetResult(value);
    }

    protected override async Task OnClientConnected(TcpClient client)
    {
        using Stream networkStream = client.GetStream();
        var stream = _useBufferedStream ? new BufferedStream(networkStream) : networkStream;

        try
        {
            while (true)
            {
                var queue = await _dataIsReady.Task;
                ResetState();

                while (queue.Count > 0)
                {
                    var data = queue.Dequeue();
                    await stream.WriteAsync(data, 0, data.Length);
                }
                await stream.FlushAsync();
            }
        }
        catch when (TeardownStarted)
        {
            //skip
        }
    }

    private void ResetState()
    {
        _dataIsReady = new TaskCompletionSource<Queue<byte[]>>(TaskCreationOptions.RunContinuationsAsynchronously);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            _dataIsReady.TrySetResult(null);
        }
    }
}