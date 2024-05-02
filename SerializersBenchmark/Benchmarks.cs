using BenchmarkDotNet.Attributes;
using SerializersBenchmark.Base;
using SerializersBenchmark.Models;
using SerializersBenchmark.Serializers;

namespace SerializersBenchmark;

[RPlotExporter]
public class Benchmarks
{
    [Params(1, 10, 100, 500, 1000, 10_000, 50_000, 100_000, 200_000, 500_000, 800_000, 1000_000)]
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
        typeof(BinaryFormatter<DataItem>)
#if (NET6_0_OR_GREATER)
        ,typeof(MemoryPack<DataItem>),
        typeof(BinaryPack<DataItem>),
        typeof(SystemTextJson<DataItem>),
        typeof(SpanJson<DataItem>)
#endif
#if NET8_0
        ,typeof(SystemTextJsonSourceGen<DataItem>)
#endif
    )]
    public Type SerializerType { get; set; }

    public ISerializerTest Serializer { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        if (SerializerType != typeof(GoogleProtobuf<ProtobufDataItem>))
        {
            Serializer = (ISerializerTest) Activator.CreateInstance(SerializerType,
                (Func<int, DataItem>) CreateDataExtensions.Data);
        }
        else
        {
            Serializer = (ISerializerTest) Activator.CreateInstance(SerializerType,
                (Func<int, ProtobufDataItem>) CreateDataExtensions.ProtobufData);
        }

        Serializer.Setup(N);
    }

    [Benchmark]
    public void TestSerialize()
    {
        Serializer.Serialize();
    }

    [Benchmark]
    public object TestDeserialize()
    {
        return Serializer.Deserialize();
    }

    
}