using System.Net;
using System.Net.Sockets;

namespace SerializersBenchmark.Network.Abstractions;

public abstract class TcpServer(int port) : IServer
{
    private readonly TcpListener _tcpListener = new(IPAddress.Parse("127.0.0.1"), port);
    private TcpClient _connectedClient;
    protected bool TeardownStarted;

    public void Start()
    {
        _tcpListener.Start();
        
        //listen asynchronously
        Task.Run(async () =>
        {
            try
            {
                while (true)
                {
                    _connectedClient = await _tcpListener.AcceptTcpClientAsync();
                    _connectedClient.Client.NoDelay = true;//disable Nagle's algorithm for low latency
                    await OnClientConnected(_connectedClient);
                }
            }
            catch when (TeardownStarted)
            {
                //skip
            }
        });
    }

    protected abstract Task OnClientConnected(TcpClient client);


    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            TeardownStarted = true;
            _tcpListener.Stop();
            _connectedClient?.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}