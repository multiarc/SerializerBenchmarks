using SerializersBenchmark;
using SerializersBenchmark.Models;
using SerializersBenchmark.Serializers;
using Xunit;

namespace SerializerBenchmarks.UnitTests;

public class RecursiveBenchmarkTests
{
    [Fact]
    public void SetupTest()
    {
        RecursiveBenchmarks benchmark = new()
        {
            N = 1,
            SerializerType = typeof(MessagePack<RecursiveDataItem>)
        };
        benchmark.Setup();
        Assert.NotNull(benchmark.SerializerTest);
        Assert.NotNull(benchmark.SerializedValue);
        Assert.NotEqual(0, benchmark.SerializedValue.Length);
    }

    [Theory]
    [InlineData(typeof(Ceras<RecursiveDataItem>))]
    [InlineData(typeof(Utf8JsonSerializer<RecursiveDataItem>))]
    [InlineData(typeof(MessagePack<RecursiveDataItem>))]
    [InlineData(typeof(GroBuf<RecursiveDataItem>))]
    [InlineData(typeof(Bois<RecursiveDataItem>))]
    [InlineData(typeof(BoisLz4<RecursiveDataItem>))]
    [InlineData(typeof(Jil<RecursiveDataItem>))]
    [InlineData(typeof(ProtobufNet<RecursiveDataItem>))]
    [InlineData(typeof(GoogleProtobuf<ProtobufRecursiveDataItem>))]
    [InlineData(typeof(ServiceStack<RecursiveDataItem>))]
    [InlineData(typeof(FastJson<RecursiveDataItem>))]
    [InlineData(typeof(DataContractBinaryXml<RecursiveDataItem>))]
    [InlineData(typeof(DataContract<RecursiveDataItem>))]
    [InlineData(typeof(XmlSerializer<RecursiveDataItem>))]
    [InlineData(typeof(NewtonsoftJson<RecursiveDataItem>))]
    [InlineData(typeof(MsgPackCli<RecursiveDataItem>))]
    [InlineData(typeof(MsgPackCliDefaultAsync<RecursiveDataItem>))]
    [InlineData(typeof(SystemTextJson<RecursiveDataItem>))]
#if (NET6_0_OR_GREATER)
    [InlineData(typeof(MemoryPack<RecursiveDataItem>))]
    //[InlineData(typeof(BinaryPack<RecursiveDataItem>))] - doesn't work with recursive type references
    [InlineData(typeof(SpanJson<RecursiveDataItem>))]
    [InlineData(typeof(SystemTextJsonSourceGen<RecursiveDataItem>))]
#endif
#if NET48
    [InlineData(typeof(BinaryFormatter<RecursiveDataItem>))]
#endif
    public void SerializeTest(Type serializerType)
    {
        RecursiveBenchmarks benchmark = new()
        {
            N = 1,
            SerializerType = serializerType,
        };
        benchmark.Setup();
        var stream = benchmark.Serialize();
        Assert.NotNull(stream);
        Assert.NotEqual(0, stream.Length);
    }

    [Theory]
    [InlineData(typeof(Ceras<RecursiveDataItem>))]
    [InlineData(typeof(Utf8JsonSerializer<RecursiveDataItem>))]
    [InlineData(typeof(MessagePack<RecursiveDataItem>))]
    [InlineData(typeof(GroBuf<RecursiveDataItem>))]
    [InlineData(typeof(Bois<RecursiveDataItem>))]
    [InlineData(typeof(BoisLz4<RecursiveDataItem>))]
    [InlineData(typeof(Jil<RecursiveDataItem>))]
    [InlineData(typeof(ProtobufNet<RecursiveDataItem>))]
    [InlineData(typeof(GoogleProtobuf<ProtobufRecursiveDataItem>))]
    [InlineData(typeof(ServiceStack<RecursiveDataItem>))]
    [InlineData(typeof(FastJson<RecursiveDataItem>))]
    [InlineData(typeof(DataContractBinaryXml<RecursiveDataItem>))]
    [InlineData(typeof(DataContract<RecursiveDataItem>))]
    [InlineData(typeof(XmlSerializer<RecursiveDataItem>))]
    [InlineData(typeof(NewtonsoftJson<RecursiveDataItem>))]
    [InlineData(typeof(MsgPackCli<RecursiveDataItem>))]
    [InlineData(typeof(MsgPackCliDefaultAsync<RecursiveDataItem>))]
    [InlineData(typeof(SystemTextJson<RecursiveDataItem>))]
#if (NET6_0_OR_GREATER)
    [InlineData(typeof(MemoryPack<RecursiveDataItem>))]
    //[InlineData(typeof(BinaryPack<RecursiveDataItem>))] - doesn't work with recursive type references
    [InlineData(typeof(SpanJson<RecursiveDataItem>))]
    [InlineData(typeof(SystemTextJsonSourceGen<RecursiveDataItem>))]
#endif
#if NET48
    [InlineData(typeof(BinaryFormatter<RecursiveDataItem>))]
#endif
    public void DeserializeTest(Type serializerType)
    {
        RecursiveBenchmarks benchmark = new()
        {
            N = 1,
            SerializerType = serializerType,
        };
        benchmark.Setup();
        var value = benchmark.Deserialize();
        Assert.NotNull(value);
    }
}