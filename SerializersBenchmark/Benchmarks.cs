using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using SerializersBenchmark.Base;
using SerializersBenchmark.Models;
using SerializersBenchmark.Serializers;
[assembly:InternalsVisibleTo("SerializerBenchmarks.UnitTests")]

namespace SerializersBenchmark;

[RPlotExporter]
[MemoryDiagnoser]
[ExceptionDiagnoser]
public class Benchmarks
{
    [Params(1, 100, 10_000, 100_000, 1_000_000)]
    public int N { get; set; }

    [Params(
        typeof(Ceras<DataItem>),
        typeof(Utf8JsonSerializer<DataItem>),
        typeof(MessagePack<DataItem>),
        typeof(GroBuf<DataItem>),
        typeof(Bois<DataItem>),
        typeof(BoisLz4<DataItem>),
        typeof(Jil<DataItem>),
        typeof(ProtobufNet<DataItem>),
        typeof(GoogleProtobuf<ProtobufDataItem>),
        typeof(ServiceStack<DataItem>),
        typeof(FastJson<DataItem>),
        typeof(DataContractBinaryXml<DataItem>),
        typeof(DataContract<DataItem>),
        typeof(XmlSerializer<DataItem>),
        typeof(NewtonsoftJson<DataItem>),
        typeof(MsgPackCli<DataItem>),
        typeof(SystemTextJson<DataItem>)
#if (NET6_0_OR_GREATER)
        ,typeof(MemoryPack<DataItem>),
        typeof(BinaryPack<DataItem>),
        typeof(SpanJson<DataItem>)
#endif
#if NET8_0
        ,typeof(SystemTextJsonSourceGen<DataItem>)
#endif
#if NET48
        ,typeof(BinaryFormatter<DataItem>)
#endif
    )]
    public Type SerializerType { get; set; }

    internal ISerializerTest SerializerTest => _serializer;
    internal MemoryStream SerializedValue => _serializedValue;
    
    private ISerializerTest _serializer;
    private MemoryStream _serializedValue;
    
    [GlobalSetup]
    public void Setup()
    {
        if (SerializerType != typeof(GoogleProtobuf<ProtobufDataItem>))
        {
            _serializer = (ISerializerTest) Activator.CreateInstance(SerializerType,
                (Func<int, DataItem>) CreateDataExtensions.Data);
        }
        else
        {
            _serializer = (ISerializerTest) Activator.CreateInstance(SerializerType,
                (Func<int, ProtobufDataItem>) CreateDataExtensions.ProtobufData);
        }
        _serializedValue = _serializer!.Setup(N);
    }

    [Benchmark]
    public MemoryStream TestSerialize()
    {
        return _serializer.Serialize(_serializer.TestDataObject);
    }

    [Benchmark]
    public object TestDeserialize()
    {
        _serializedValue.Position = 0;
        return _serializer.Deserialize(_serializedValue);
    }
    
    [Benchmark]
    public object EndToEnd()
    {
        var serializedValue = _serializer.Serialize(_serializer.TestDataObject);
        serializedValue.Position = 0;
        return _serializer.Deserialize(serializedValue);
    }
}