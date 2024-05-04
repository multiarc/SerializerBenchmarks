﻿using System.Net.Sockets;
using SerializersBenchmark.Network.Abstractions;

namespace SerializersBenchmark.Network;

/// <summary>
/// TCP based server using loopback to pass-through values for deserialization asynchronously
/// </summary>
public class WhiteHoleServer : TcpServer, IWhiteHole
{
    private TaskCompletionSource<byte[]> _dataIsReady;

    public WhiteHoleServer(int port) : base(port)
    {
        ResetState();
    }

    public void SpawnNext(byte[] value)
    {
        _dataIsReady.SetResult(value);
    }

    protected override async Task OnClientConnected(TcpClient client)
    {
        using var stream = client.GetStream();

        try
        {
            while (true)
            {
                var data = await _dataIsReady.Task;
                ResetState();

                await stream.WriteAsync(data, 0, data.Length);
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
        _dataIsReady = new TaskCompletionSource<byte[]>(TaskCreationOptions.RunContinuationsAsynchronously);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            _dataIsReady.SetResult(null);
        }
    }
}