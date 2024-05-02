using Google.Protobuf;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class GoogleProtobuf<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    where T : class, IMessage<T>, new()
{
    protected override T Deserialize(MemoryStream stream)
    {
        var obj = new T();
        obj.MergeFrom(stream);
        return obj;
    }

    protected override void Serialize(T obj, MemoryStream stream)
    {
        obj.WriteTo(stream);
    }
}