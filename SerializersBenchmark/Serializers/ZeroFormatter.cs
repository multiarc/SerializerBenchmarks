using SerializersBenchmark.Base;
using ZeroFormatter;

namespace SerializersBenchmark.Serializers;

public class ZeroFormatter<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
{
    protected override T Deserialize(MemoryStream stream)
    {
        return ZeroFormatterSerializer.Deserialize<T>(stream);
    }

    protected override void Serialize(T obj, MemoryStream stream)
    {
        ZeroFormatterSerializer.Serialize(stream, obj);
    }
}