using GroBuf;
using GroBuf.DataMembersExtracters;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class GroBuf<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    where T : class
{
    private Serializer Formatter { get; } =
        new(new AllFieldsExtractor(), options: GroBufOptions.WriteEmptyObjects);

    protected override void Serialize(T obj, MemoryStream stream)
    {
        var bytes = Formatter.Serialize(obj);
        stream.Write(bytes, 0, bytes.Length);
    }

    protected override T Deserialize(MemoryStream stream)
    {
        return Formatter.Deserialize<T>(stream.GetBuffer(), (int) stream.Length);
    }
}