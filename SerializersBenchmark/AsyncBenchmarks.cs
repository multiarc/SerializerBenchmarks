using BenchmarkDotNet.Attributes;
using SerializersBenchmark.Base;
using SerializersBenchmark.Models;
using SerializersBenchmark.Network;
using SerializersBenchmark.Network.Abstractions;
using SerializersBenchmark.Serializers;

namespace SerializersBenchmark;

[RPlotExporter]
[MemoryDiagnoser]
[ExceptionDiagnoser]
public class AsyncBenchmarks
{
    internal int BlackHolePort { get; set; } = 27001;
    internal int WhiteHolePort { get; set; } = 27000;
    
    [Params(100)]
    public int N { get; set; }
    
    [Params(true, false)]
    public bool UseBuffer { get; set; }
    
    [Params(1, 4, 16, 64)]
    public int QueueLength { get; set; }

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
        typeof(MsgPackCliDefaultAsync<DataItem>),
        typeof(SystemTextJson<DataItem>)
#if (NET6_0_OR_GREATER)
        ,typeof(MemoryPack<DataItemMemoryPack>),
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
        _serializer = SerializerType.CreateSerializerInstance();
        _serializedValue = _serializer!.Setup(N);
        _serializedArray = _serializedValue.ToArray();
        
        _whiteHole = CreateWhiteHole();
        _blackHole = CreateBlackHole();
        
        //allow servers to spin up before connecting
        await Task.Delay(100);
        
        var rabbitToDeserialize = new Rabbit(_serializer, WhiteHolePort, UseBuffer);
        var rabbitToSerialize = new Rabbit(_serializer, BlackHolePort, UseBuffer);
        await rabbitToSerialize.ConnectAsync();
        await rabbitToDeserialize.ConnectAsync();
        
        _rabbitToSerialize = rabbitToSerialize;
        _rabbitToDeserialize = rabbitToDeserialize;

        var queue = new Queue<byte[]>();
        //keep queue busy
        for (var i = 0; i < QueueLength; i++)
        {
            queue.Enqueue(_serializedArray);
        }
        _whiteHole.SpawnNext(queue);
    }

    private IWhiteHole CreateWhiteHole()
    {
        while (true)
        {
            try
            {
                var whiteHole = new WhiteHoleServer(WhiteHolePort, UseBuffer);
                whiteHole.Start();
                return whiteHole;
            }
            catch
            {
                WhiteHolePort++;
            }
        }
    }
    
    private IBlackHole CreateBlackHole()
    {
        while (true)
        {
            try
            {
                var blackHole = new BlackHoleServer(BlackHolePort);
                blackHole.Start();
                return blackHole;
            }
            catch
            {
                BlackHolePort++;
            }
        }
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
    public async Task SerializeAsync()
    {
        await _rabbitToSerialize.SendAsync(_serializer.TestDataObject, QueueLength);
    }

    [Benchmark]
    public async Task DeserializeAsync()
    {
        for (var i = 0; i < QueueLength; i++)
        {
            await _rabbitToDeserialize.ReceiveAsync(_serializedArray.Length);
        }

        var queue = new Queue<byte[]>();
        //keep queue busy
        for (var i = 0; i < QueueLength; i++)
        {
            queue.Enqueue(_serializedArray);
        }
        _whiteHole.SpawnNext(queue);
    }
}