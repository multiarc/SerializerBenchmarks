using Salar.Bois.LZ4;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class BoisLz4<T>(Func<int, T> testData) : TestBase<T>(testData)
{
    private BoisLz4Serializer Formatter { get; } = new();

    protected override void Serialize(T obj, MemoryStream stream)
    {
        Formatter.Pickle(obj, stream);
    }

    protected override T Deserialize(MemoryStream stream)
    {
        return Formatter.Unpickle<T>(stream);
    }
}