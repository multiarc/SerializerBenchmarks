using MessagePack;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class MessagePack<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    where T : class
{
    protected override void Serialize(T obj, MemoryStream stream)
    {
        MessagePackSerializer.Serialize(typeof(T), stream, obj);
    }

    protected override T Deserialize(MemoryStream stream)
    {
        return MessagePackSerializer.Deserialize<T>(stream);
    }
}