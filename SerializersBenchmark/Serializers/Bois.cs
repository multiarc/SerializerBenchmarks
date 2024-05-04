using Salar.Bois;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class Bois<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
{
    private BoisSerializer Serializer { get; } = new();

    public override MemoryStream Serialize(object obj)
    {
        var stream = new MemoryStream();
        Serializer.Serialize((T)obj, stream);
        return stream;
    }

    public override object Deserialize(MemoryStream stream)
    {
        return Serializer.Deserialize<T>(stream);
    }
}