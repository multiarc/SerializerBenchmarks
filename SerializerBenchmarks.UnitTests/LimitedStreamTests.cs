using SerializersBenchmark.Network.Abstractions;
using Xunit;

namespace SerializerBenchmarks.UnitTests;

public class LimitedStreamTests
{
    [Fact]
    public void LimitedStreamReadOverTest()
    {
        var buffer = new byte[37];
        const int maxRead = 100;
        var memory = new MemoryStream(new byte[100]);
        var limitedStream = new LimitedStreamReader(memory, maxRead);
        int read = 0;
        while (read < maxRead)
        {
            read += limitedStream.Read(buffer, 0, buffer.Length);
        }

        read += limitedStream.Read(buffer, 0, buffer.Length);

        Assert.Equal(maxRead, read);
    }
    
#if NET6_0_OR_GREATER    
    [Fact]
    public void LimitedStreamReadSpanOverTest()
    {
        var buffer = new byte[37];
        const int maxRead = 100;
        var memory = new MemoryStream(new byte[100]);
        var limitedStream = new LimitedStreamReader(memory, maxRead);
        int read = 0;
        while (read < maxRead)
        {
            read += limitedStream.Read(buffer.AsSpan());
        }

        read += limitedStream.Read(buffer.AsSpan());

        Assert.Equal(maxRead, read);
    }
#endif
    
    [Fact]
    public async Task LimitedStreamReadAsyncOverTest()
    {
        var buffer = new byte[37];
        const int maxRead = 100;
        var memory = new MemoryStream(new byte[100]);
        var limitedStream = new LimitedStreamReader(memory, maxRead);
        int read = 0;
        while (read < maxRead)
        {
            read += await limitedStream.ReadAsync(buffer, 0, buffer.Length);
        }

        read += await limitedStream.ReadAsync(buffer, 0, buffer.Length);

        Assert.Equal(maxRead, read);
    }
    
#if NET6_0_OR_GREATER    
    [Fact]
    public async Task LimitedStreamReadMemoryAsyncOverTest()
    {
        var buffer = new byte[37];
        const int maxRead = 100;
        var memory = new MemoryStream(new byte[100]);
        var limitedStream = new LimitedStreamReader(memory, maxRead);
        int read = 0;
        while (read < maxRead)
        {
            read += await limitedStream.ReadAsync(buffer.AsMemory());
        }

        read += await limitedStream.ReadAsync(buffer.AsMemory());

        Assert.Equal(maxRead, read);
    }
#endif
    
    [Fact]
    public void LimitedStreamReadByteOverTest()
    {
        var buffer = new byte[37];
        const int maxRead = 100;
        var memory = new MemoryStream(new byte[100]);
        var limitedStream = new LimitedStreamReader(memory, maxRead);
        int read = 0;
        while (read < maxRead)
        {
            read += limitedStream.Read(buffer, 0, buffer.Length);
        }

        var byteReadOver = limitedStream.ReadByte();
        
        Assert.Equal(-1, byteReadOver);
        Assert.Equal(maxRead, read);
    }
    
}