using FluentAssertions;
using SerializersBenchmark.Base;
using SerializersBenchmark.Models;
using SerializersBenchmark.Serializers;
using Xunit;

namespace SerializerBenchmarks.UnitTests;

public class AsyncSerializerTests
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
    public async Task SerializeAsyncTest(Type serializerType)
    {
        var serializer = serializerType.CreateSerializerInstance();

        Assert.NotNull(serializer);
        serializer.Setup(1);
        var memory = new MemoryStream();
        await serializer.SerializeAsync(serializer.TestDataObject, memory);
        Assert.NotEqual(0, memory.Length);
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
    public async Task DeserializeAsyncTest(Type serializerType)
    {
        var serializer = serializerType.CreateSerializerInstance();

        Assert.NotNull(serializer);
        serializer.Setup(1);
        var stream = new MemoryStream();
        await serializer.SerializeAsync(serializer.TestDataObject, stream);
        stream.Position = 0;
        var result = await serializer.DeserializeAsync(stream);
        Assert.NotNull(result);
        result.Should().BeEquivalentTo(serializer.TestDataObject);
    }
}