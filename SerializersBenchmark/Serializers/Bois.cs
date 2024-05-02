using Salar.Bois;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class Bois<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
{
    private BoisSerializer Formatter { get; } = new();

    protected override void Serialize(T obj, MemoryStream stream)
    {
            Formatter.Serialize(obj, stream);
        }

    protected override T Deserialize(MemoryStream stream)
    {
            return Formatter.Deserialize<T>(stream);
        }
}