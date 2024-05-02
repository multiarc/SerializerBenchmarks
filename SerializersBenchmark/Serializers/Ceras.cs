using Ceras;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class Ceras<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
{
    private CerasSerializer Formatter { get; } = new();

    protected override void Serialize(T obj, MemoryStream stream)
    {
        var array = Formatter.Serialize(obj);
        stream.Write(array, 0, array.Length);
    }

    protected override T Deserialize(MemoryStream stream)
    {
        T ret = default;
        int offset = 0;
        Formatter.Deserialize(ref ret, stream.GetBuffer(), ref offset, (int)stream.Length);
        return ret;
    }
}