using ProtoBuf.Meta;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class ProtobufNet<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    where T : class
{
    private RuntimeTypeModel Formatter { get; } = RuntimeTypeModel.Create(); 

    protected override void Serialize(T obj, MemoryStream stream)
    {
        Formatter.Serialize(stream, obj);
    }

    protected override T Deserialize(MemoryStream stream)
    {
        return (T)Formatter.Deserialize(stream, null, typeof(T));
    }
}