using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using SerializersBenchmark.Base;
using SerializersBenchmark.Models;
using SerializersBenchmark.Network;
using SerializersBenchmark.Network.Abstractions;
using SerializersBenchmark.Serializers;
[assembly:InternalsVisibleTo("SerializerBenchmarks.UnitTests")]

namespace SerializersBenchmark;

[RPlotExporter]
[MemoryDiagnoser]
[ExceptionDiagnoser]
[SimpleJob]
public class Benchmarks
{
    internal int BlackHolePort { get; set; } = 37001;
    internal int WhiteHolePort { get; set; } = 37000;
    
    [Params(1, 100, 10_000, 200_000)]
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
        typeof(SpanJson<DataItem>),
        typeof(SystemTextJsonSourceGen<DataItem>)
#endif
#if NET48
        ,typeof(BinaryFormatter<DataItem>)
#endif
    )]
    public Type SerializerType { get; set; }

    internal ISerializerTest SerializerTest => _serializer;
    internal MemoryStream SerializedValue => _serializedValue;
    
    private ISerializerTestAsync _serializer;
    private MemoryStream _serializedValue;
    private byte[] _serializedArray;
    private IWhiteHole _whiteHole;
    private IBlackHole _blackHole;
    private IRabbit _rabbitToSerialize;
    private IRabbit _rabbitToDeserialize;
    
    
    [GlobalSetup]
    public async Task SetupAsync()
    {
        if (SerializerType != typeof(GoogleProtobuf<ProtobufDataItem>))
        {
            _serializer = (ISerializerTestAsync) Activator.CreateInstance(SerializerType,
                (Func<int, DataItem>) CreateDataExtensions.Data);
        }
        else
        {
            _serializer = (ISerializerTestAsync) Activator.CreateInstance(SerializerType,
                (Func<int, ProtobufDataItem>) CreateDataExtensions.ProtobufData);
        }
        _serializedValue = _serializer!.Setup(N);
        _serializedArray = _serializedValue.ToArray();
        var whiteHole = new WhiteHoleServer(WhiteHolePort);
        var blackHole = new BlackHoleServer(BlackHolePort);
        whiteHole.Start();
        blackHole.Start();
        
        //allow servers to spin up before connecting
        await Task.Delay(100);
        
        var rabbitToDeserialize = new Rabbit(_serializer, WhiteHolePort);
        var rabbitToSerialize = new Rabbit(_serializer, BlackHolePort);
        await rabbitToSerialize.ConnectAsync();
        await rabbitToDeserialize.ConnectAsync();
        
        _rabbitToSerialize = rabbitToSerialize;
        _rabbitToDeserialize = rabbitToDeserialize;
        _whiteHole = whiteHole;
        _blackHole = blackHole;
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _rabbitToDeserialize.Dispose();
        _rabbitToSerialize.Dispose();
        _whiteHole.Dispose();
        _blackHole.Dispose();
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

    [Benchmark]
    public async Task SerializeAsync()
    {
        await _rabbitToSerialize.SendAsync(_serializer.TestDataObject);
    }

    [Benchmark]
    public async Task DeserializeAsync()
    {
        _whiteHole.SpawnNext(_serializedArray);
        await _rabbitToDeserialize.ReceiveAsync(_serializedArray.Length);
    }
}