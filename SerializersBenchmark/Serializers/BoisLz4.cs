using K4os.Compression.LZ4;
using Salar.Bois.LZ4;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class BoisLz4<T>(Func<int, T> testData) : TestBase<T>(testData)
{
    private BoisLz4Serializer Serializer { get; } = new();

    public override MemoryStream Serialize(object obj)
    {
        var stream = new MemoryStream();
        Serializer.Pickle((T)obj, stream, LZ4Level.L00_FAST);
        return stream;
    }

    public override object Deserialize(MemoryStream stream)
    {
        return Serializer.Unpickle<T>(stream);
    }
}