using MsgPack.Serialization;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class MsgPackCliDefaultAsync<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    where T : class
{
    private MessagePackSerializer Serializer { get; } = MessagePackSerializer.Get<T>();

    public override MemoryStream Serialize(object obj)
    {
        var stream = new MemoryStream();
        Serializer.Pack(stream, obj);
        return stream;
    }

    public override object Deserialize(MemoryStream stream)
    {
        return (T) Serializer.Unpack(stream);
    }
}