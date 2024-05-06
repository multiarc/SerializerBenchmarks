using SerializersBenchmark;
using SerializersBenchmark.Models;
using SerializersBenchmark.Serializers;
using Xunit;

namespace SerializerBenchmarks.UnitTests;

public sealed class AsyncBenchmarkTests : IDisposable
{
    private static int _portNumber = 27003;
    private static int GetNextPort()
    {
        Interlocked.Increment(ref _portNumber);
        return _portNumber;
    }

    private readonly AsyncBenchmarks _benchmark = new()
    {
        N = 1,
        QueueLength = 7,
        BlackHolePort = GetNextPort(),
        WhiteHolePort = GetNextPort()
    };

    [Theory(Timeout = 1000)]
    [InlineData(typeof(Ceras<DataItem>))]
    [InlineData(typeof(Utf8JsonSerializer<DataItem>))]
    [InlineData(typeof(MessagePack<DataItem>))]
    [InlineData(typeof(GroBuf<DataItem>))]
    [InlineData(typeof(Bois<DataItem>))]
    [InlineData(typeof(BoisLz4<DataItem>))]
    [InlineData(typeof(Jil<DataItem>))]
    [InlineData(typeof(ProtobufNet<DataItem>))]
    [InlineData(typeof(GoogleProtobuf<ProtobufDataItem>))]
    [InlineData(typeof(ServiceStack<DataItem>))]
    [InlineData(typeof(FastJson<DataItem>))]
    [InlineData(typeof(DataContractBinaryXml<DataItem>))]
    [InlineData(typeof(DataContract<DataItem>))]
    [InlineData(typeof(XmlSerializer<DataItem>))]
    [InlineData(typeof(NewtonsoftJson<DataItem>))]
    [InlineData(typeof(MsgPackCli<DataItem>))]
    [InlineData(typeof(SystemTextJson<DataItem>))]
#if (NET6_0_OR_GREATER)
    [InlineData(typeof(MemoryPack<DataItem>))]
    [InlineData(typeof(BinaryPack<DataItem>))]
    [InlineData(typeof(SpanJson<DataItem>))]
    [InlineData(typeof(SystemTextJsonSourceGen<DataItem>))]
#endif
#if NET48
    [InlineData(typeof(BinaryFormatter<DataItem>))]
#endif
    public async Task SerializeAsyncTest(Type serializerType)
    {
        _benchmark.SerializerType = serializerType;
        await _benchmark.SetupAsync();
        await _benchmark.SerializeAsync();
    }
    
    [Theory(Timeout = 1000)]
    [InlineData(typeof(Ceras<DataItem>))]
    [InlineData(typeof(Utf8JsonSerializer<DataItem>))]
    [InlineData(typeof(MessagePack<DataItem>))]
    [InlineData(typeof(GroBuf<DataItem>))]
    [InlineData(typeof(Bois<DataItem>))]
    [InlineData(typeof(BoisLz4<DataItem>))]
    [InlineData(typeof(Jil<DataItem>))]
    [InlineData(typeof(ProtobufNet<DataItem>))]
    [InlineData(typeof(GoogleProtobuf<ProtobufDataItem>))]
    [InlineData(typeof(ServiceStack<DataItem>))]
    [InlineData(typeof(FastJson<DataItem>))]
    [InlineData(typeof(DataContractBinaryXml<DataItem>))]
    [InlineData(typeof(DataContract<DataItem>))]
    [InlineData(typeof(XmlSerializer<DataItem>))]
    [InlineData(typeof(NewtonsoftJson<DataItem>))]
    [InlineData(typeof(MsgPackCli<DataItem>))]
    [InlineData(typeof(SystemTextJson<DataItem>))]
#if (NET6_0_OR_GREATER)
    [InlineData(typeof(MemoryPack<DataItem>))]
    [InlineData(typeof(BinaryPack<DataItem>))]
    [InlineData(typeof(SpanJson<DataItem>))]
    [InlineData(typeof(SystemTextJsonSourceGen<DataItem>))]
#endif
#if NET48
    [InlineData(typeof(BinaryFormatter<DataItem>))]
#endif
    public async Task BufferedSerializeAsyncTest(Type serializerType)
    {
        _benchmark.SerializerType = serializerType;
        _benchmark.UseBuffer = true; 
        await _benchmark.SetupAsync();
        await _benchmark.SerializeAsync();
    }
    
    [Theory(Timeout = 1000)]
    [InlineData(typeof(Ceras<DataItem>))]
    [InlineData(typeof(Utf8JsonSerializer<DataItem>))]
    [InlineData(typeof(MessagePack<DataItem>))]
    [InlineData(typeof(GroBuf<DataItem>))]
    [InlineData(typeof(Bois<DataItem>))]
    [InlineData(typeof(BoisLz4<DataItem>))]
    [InlineData(typeof(Jil<DataItem>))]
    [InlineData(typeof(ProtobufNet<DataItem>))]
    [InlineData(typeof(GoogleProtobuf<ProtobufDataItem>))]
    [InlineData(typeof(ServiceStack<DataItem>))]
    [InlineData(typeof(FastJson<DataItem>))]
    [InlineData(typeof(DataContractBinaryXml<DataItem>))]
    [InlineData(typeof(DataContract<DataItem>))]
    [InlineData(typeof(XmlSerializer<DataItem>))]
    [InlineData(typeof(NewtonsoftJson<DataItem>))]
    [InlineData(typeof(MsgPackCli<DataItem>))]
    [InlineData(typeof(SystemTextJson<DataItem>))]
#if (NET6_0_OR_GREATER)
    [InlineData(typeof(MemoryPack<DataItem>))]
    [InlineData(typeof(BinaryPack<DataItem>))]
    [InlineData(typeof(SpanJson<DataItem>))]
    [InlineData(typeof(SystemTextJsonSourceGen<DataItem>))]
#endif
#if NET48
    [InlineData(typeof(BinaryFormatter<DataItem>))]
#endif
    public async Task DeserializeAsyncTest(Type serializerType)
    {
        _benchmark.SerializerType = serializerType;
        await _benchmark.SetupAsync();
        await _benchmark.DeserializeAsync();
    }
    
    [Theory(Timeout = 1000)]
    [InlineData(typeof(Ceras<DataItem>))]
    [InlineData(typeof(Utf8JsonSerializer<DataItem>))]
    [InlineData(typeof(MessagePack<DataItem>))]
    [InlineData(typeof(GroBuf<DataItem>))]
    [InlineData(typeof(Bois<DataItem>))]
    [InlineData(typeof(BoisLz4<DataItem>))]
    [InlineData(typeof(Jil<DataItem>))]
    [InlineData(typeof(ProtobufNet<DataItem>))]
    [InlineData(typeof(GoogleProtobuf<ProtobufDataItem>))]
    [InlineData(typeof(ServiceStack<DataItem>))]
    [InlineData(typeof(FastJson<DataItem>))]
    [InlineData(typeof(DataContractBinaryXml<DataItem>))]
    [InlineData(typeof(DataContract<DataItem>))]
    [InlineData(typeof(XmlSerializer<DataItem>))]
    [InlineData(typeof(NewtonsoftJson<DataItem>))]
    [InlineData(typeof(MsgPackCli<DataItem>))]
    [InlineData(typeof(SystemTextJson<DataItem>))]
#if (NET6_0_OR_GREATER)
    [InlineData(typeof(MemoryPack<DataItem>))]
    [InlineData(typeof(BinaryPack<DataItem>))]
    [InlineData(typeof(SpanJson<DataItem>))]
    [InlineData(typeof(SystemTextJsonSourceGen<DataItem>))]
#endif
#if NET48
    [InlineData(typeof(BinaryFormatter<DataItem>))]
#endif
    public async Task BufferedDeserializeAsyncTest(Type serializerType)
    {
        _benchmark.SerializerType = serializerType;
        _benchmark.UseBuffer = true;
        await _benchmark.SetupAsync();
        await _benchmark.DeserializeAsync();
    }
    
    public void Dispose()
    {
        _benchmark.Cleanup();
    }
}