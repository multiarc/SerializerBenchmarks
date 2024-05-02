using MsgPack.Serialization;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class MsgPackCli<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    where T : class
{
    private MessagePackSerializer Formatter { get; } = MessagePackSerializer.Get<T>();

    protected override void Serialize(T obj, MemoryStream stream)
    {
        Formatter.Pack(stream, obj);
    }

    protected override T Deserialize(MemoryStream stream)
    {
        return (T) Formatter.Unpack(stream);
    }
}