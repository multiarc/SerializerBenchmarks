using BenchmarkDotNet.Attributes;
using SerializersBenchmark.Base;
using SerializersBenchmark.Models;
using SerializersBenchmark.Serializers;

namespace SerializersBenchmark;

[RPlotExporter]
[MemoryDiagnoser]
[ExceptionDiagnoser]
public class Benchmarks
{
    [Params(1, 10, 100, 500, 1000, 10_000, 50_000, 100_000, 200_000, 500_000, 800_000, 1_000_000)]
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
        typeof(BinaryFormatter<DataItem>),
        typeof(SystemTextJson<DataItem>)
#if (NET6_0_OR_GREATER)
        ,typeof(MemoryPack<DataItem>),
        typeof(BinaryPack<DataItem>),
        typeof(SpanJson<DataItem>)
#endif
#if NET8_0
        ,typeof(SystemTextJsonSourceGen<DataItem>)
#endif
    )]
    public Type SerializerType { get; set; }

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
    public void TestSerialize()
    {
        _serializer.Serialize(_serializer.TestDataObject);
    }

    [Benchmark]
    public object TestDeserialize()
    {
        return _serializer.Deserialize(_serializedValue);
    }
    
    [Benchmark]
    public object EndToEnd()
    {
        _serializer.Serialize(_serializer.TestDataObject);
        return _serializer.Deserialize(_serializedValue);
    }
}