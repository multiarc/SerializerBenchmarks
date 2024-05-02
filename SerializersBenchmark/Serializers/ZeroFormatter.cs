using SerializersBenchmark.Base;
using ZeroFormatter;

namespace SerializersBenchmark.Serializers;

public class ZeroFormatter<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
{
    public override object Deserialize(MemoryStream stream)
    {
        return ZeroFormatterSerializer.Deserialize<T>(stream);
    }

    public override MemoryStream Serialize(object obj)
    {
        var stream = new MemoryStream();
        ZeroFormatterSerializer.Serialize(stream, obj);
        return stream;
    }
}