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
public class RecursiveBenchmarks
{
    [Params(1, 100, 500, 10_000, 200_000)]
    public int N { get; set; }

    [Params(
        typeof(Ceras<RecursiveDataItem>),
        typeof(Utf8JsonSerializer<RecursiveDataItem>),
        typeof(MessagePack<RecursiveDataItem>),
        typeof(GroBuf<RecursiveDataItem>),
        typeof(Bois<RecursiveDataItem>),
        typeof(BoisLz4<RecursiveDataItem>),
        typeof(Jil<RecursiveDataItem>),
        typeof(ProtobufNet<RecursiveDataItem>),
        typeof(GoogleProtobuf<ProtobufRecursiveDataItem>),
        typeof(ServiceStack<RecursiveDataItem>),
        typeof(FastJson<RecursiveDataItem>),
        typeof(DataContractBinaryXml<RecursiveDataItem>),
        typeof(DataContract<RecursiveDataItem>),
        typeof(XmlSerializer<RecursiveDataItem>),
        typeof(NewtonsoftJson<RecursiveDataItem>),
        typeof(MsgPackCli<RecursiveDataItem>),
        typeof(SystemTextJson<RecursiveDataItem>)
#if (NET6_0_OR_GREATER)
        ,typeof(MemoryPack<RecursiveDataItem>),
        //typeof(BinaryPack<RecursiveDataItem>), - doesn't work with recursive type references
        typeof(SpanJson<RecursiveDataItem>),
        typeof(SystemTextJsonSourceGen<RecursiveDataItem>)
#endif
#if NET48
        ,typeof(BinaryFormatter<RecursiveDataItem>)
#endif
    )]
    public Type SerializerType { get; set; }

    internal ISerializerTest SerializerTest => _serializer;
    internal MemoryStream SerializedValue => _serializedValue;
    
    private ISerializerTestAsync _serializer;
    private MemoryStream _serializedValue;
    
    
    [GlobalSetup]
    public void Setup()
    {
        if (SerializerType != typeof(GoogleProtobuf<ProtobufRecursiveDataItem>))
        {
            _serializer = (ISerializerTestAsync) Activator.CreateInstance(SerializerType,
                (Func<int, RecursiveDataItem>) CreateDataExtensions.RecursiveData);
        }
        else
        {
            _serializer = (ISerializerTestAsync) Activator.CreateInstance(SerializerType,
                (Func<int, ProtobufRecursiveDataItem>) CreateDataExtensions.ProtobufRecursiveData);
        }
        _serializedValue = _serializer!.Setup(N);
    }

    [Benchmark]
    public MemoryStream Serialize()
    {
        return _serializer.Serialize(_serializer.TestDataObject);
    }

    [Benchmark]
    public object Deserialize()
    {
        _serializedValue.Position = 0;
        return _serializer.Deserialize(_serializedValue);
    }
}