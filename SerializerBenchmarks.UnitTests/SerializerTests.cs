using FluentAssertions;
using SerializersBenchmark.Base;
using SerializersBenchmark.Models;
using SerializersBenchmark.Serializers;
using Xunit;

namespace SerializerBenchmarks.UnitTests;

public class SerializerTests
{
    [Theory]
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
    [InlineData(typeof(MsgPackCliDefaultAsync<DataItem>))]
    [InlineData(typeof(SystemTextJson<DataItem>))]
#if (NET6_0_OR_GREATER)
    [InlineData(typeof(MemoryPack<DataItemMemoryPack>))]
    [InlineData(typeof(BinaryPack<DataItem>))]
    [InlineData(typeof(SpanJson<DataItem>))]
    [InlineData(typeof(SystemTextJsonSourceGen<DataItem>))]
#endif
#if NET48
    [InlineData(typeof(BinaryFormatter<DataItem>))]
#endif
    public void SerializeTest(Type serializerType)
    {
        var serializer = serializerType.CreateSerializerInstance();

        Assert.NotNull(serializer);
        serializer.Setup(1);
        var result = serializer.Serialize(serializer.TestDataObject);
        Assert.NotNull(result);
        Assert.NotEqual(0, result.Length);
    }
    
    [Theory]
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
    [InlineData(typeof(MsgPackCliDefaultAsync<DataItem>))]
    [InlineData(typeof(SystemTextJson<DataItem>))]
#if (NET6_0_OR_GREATER)
    [InlineData(typeof(MemoryPack<DataItemMemoryPack>))]
    [InlineData(typeof(BinaryPack<DataItem>))]
    [InlineData(typeof(SpanJson<DataItem>))]
    [InlineData(typeof(SystemTextJsonSourceGen<DataItem>))]
#endif
#if NET48
    [InlineData(typeof(BinaryFormatter<DataItem>))]
#endif
    public void DeserializeTest(Type serializerType)
    {
        var serializer = serializerType.CreateSerializerInstance();

        Assert.NotNull(serializer);
        serializer.Setup(1);
        var stream = serializer.Serialize(serializer.TestDataObject);
        stream.Position = 0;
        var result = serializer.Deserialize(stream);
        Assert.NotNull(result);
        result.Should().BeEquivalentTo(serializer.TestDataObject);
    }
}