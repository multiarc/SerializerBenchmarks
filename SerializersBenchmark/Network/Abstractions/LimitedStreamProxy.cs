namespace SerializersBenchmark.Network.Abstractions;

public class LimitedStreamReader: Stream
{
    private readonly Stream _source;
    private int _pendingBytes;

    public LimitedStreamReader(Stream source, int pendingBytes)
    {
        _source = source;
        _pendingBytes = pendingBytes;
    }

    public override void Flush()
    {
        throw new NotSupportedException();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        if (_pendingBytes == 0)
            return 0;
        
        var bytesToRead = Math.Min(count, _pendingBytes);
        var bytesRead = _source.Read(buffer, offset, bytesToRead);
        _pendingBytes -= bytesRead;
        return bytesRead;
    }

#if NET6_0_OR_GREATER
    public override int Read(Span<byte> buffer)
    {
        if (_pendingBytes == 0)
            return 0;
        
        var bytesToRead = Math.Min(buffer.Length, _pendingBytes);
        var bytesRead = _source.Read(buffer[..bytesToRead]);
        _pendingBytes -= bytesRead;
        return bytesRead;
    }
#endif
    
    public override int ReadByte()
    {
        if (_pendingBytes == 0)
            return -1;

        var result = _source.ReadByte();
        if (_pendingBytes > 0)
        {
            _pendingBytes--;
        }

        return result;
    }

    public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        if (_pendingBytes == 0)
            return 0;
        
        var bytesToRead = Math.Min(count, _pendingBytes);
        var bytesRead = await _source.ReadAsync(buffer, offset, bytesToRead, cancellationToken);
        _pendingBytes -= bytesRead;
        return bytesRead;
    }

#if NET6_0_OR_GREATER
    public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = new CancellationToken())
    {
        if (_pendingBytes == 0)
            return 0;
        
        var bytesToRead = Math.Min(buffer.Length, _pendingBytes);
        var bytesRead = await _source.ReadAsync(buffer[..bytesToRead], cancellationToken);
        _pendingBytes -= bytesRead;
        return bytesRead;
    }
#endif

    public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
    {
        throw new NotSupportedException();
    }

    public override int EndRead(IAsyncResult asyncResult)
    {
        throw new NotSupportedException();
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotSupportedException();
    }

    public override void SetLength(long value)
    {
        throw new NotSupportedException();
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        throw new NotSupportedException();
    }

    public override bool CanRead { get; } = true;
    public override bool CanSeek { get; } = false;
    public override bool CanWrite { get; } = false;
    public override long Length => throw new NotSupportedException();
    public override long Position
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }
}