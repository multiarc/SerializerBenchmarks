﻿using SerializersBenchmark;
using SerializersBenchmark.Models;
using SerializersBenchmark.Serializers;
using Xunit;

namespace SerializerBenchmarks.UnitTests;

public class BenchmarkTests: IDisposable
{
    private readonly Benchmarks _benchmark = new()
    {
        N = 1
    };

    [Fact]
    public async Task SetupTest()
    {
        _benchmark.SerializerType = typeof(MessagePack<DataItem>);
        _benchmark.BlackHolePort = 47000;
        _benchmark.WhiteHolePort = 47001;
        await _benchmark.SetupAsync();
        Assert.NotNull(_benchmark.SerializerTest);
        Assert.NotNull(_benchmark.SerializedValue);
        Assert.NotEqual(0, _benchmark.SerializedValue.Length);
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
    public async Task SerializeAsyncTest(Type serializerType)
    {
        _benchmark.SerializerType = serializerType;
        _benchmark.BlackHolePort = 47002;
        _benchmark.WhiteHolePort = 47003;
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
        _benchmark.BlackHolePort = 47004;
        _benchmark.WhiteHolePort = 47005;
        await _benchmark.SetupAsync();
        await _benchmark.DeserializeAsync();
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
    public async Task SerializeTest(Type serializerType)
    {
        _benchmark.SerializerType = serializerType;
        _benchmark.BlackHolePort = 47006;
        _benchmark.WhiteHolePort = 47007;
        await _benchmark.SetupAsync();
        var stream = _benchmark.Serialize();
        Assert.NotNull(stream);
        Assert.NotEqual(0, stream.Length);
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
    public async Task DeserializeTest(Type serializerType)
    {
        _benchmark.SerializerType = serializerType;
        _benchmark.BlackHolePort = 47008;
        _benchmark.WhiteHolePort = 47009;
        await _benchmark.SetupAsync();
        var value = _benchmark.Deserialize();
        Assert.NotNull(value);
    }

    public void Dispose()
    {
        _benchmark.Cleanup();
    }
}