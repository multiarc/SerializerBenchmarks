using Google.Protobuf;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class GoogleProtobuf<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    where T : class, IMessage<T>, new()
{
    public override object Deserialize(MemoryStream stream)
    {
        var obj = new T();
        obj.MergeFrom(stream);
        return obj;
    }

    public override MemoryStream Serialize(object obj)
    {
        var stream = new MemoryStream();
        ((T)obj).WriteTo(stream);
        return stream;
    }
}