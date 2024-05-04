using ProtoBuf.Meta;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class ProtobufNet<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    where T : class
{
    private RuntimeTypeModel Serializer { get; } = RuntimeTypeModel.Create();

    public override MemoryStream Serialize(object obj)
    {
        var stream = new MemoryStream();
        Serializer.Serialize(stream, obj);
        return stream;
    }

    public override object Deserialize(MemoryStream stream)
    {
        return Serializer.Deserialize(stream, null, typeof(T));
    }
    
    //no actual async implementation exists, using default
}