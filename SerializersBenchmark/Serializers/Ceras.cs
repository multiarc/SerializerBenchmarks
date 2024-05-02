using Ceras;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class Ceras<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
{
    private CerasSerializer Serializer { get; } = new();

    public override MemoryStream Serialize(object obj)
    {
        var stream = new MemoryStream();
        var array = Serializer.Serialize(obj);
        //this is not optimal in this case,
        //but to keep Stream usage pattern similar to all serializers we have to write instead of construct of MemoryStream in this case
        stream.Write(array, 0, array.Length);
        return stream;
    }

    public override object Deserialize(MemoryStream stream)
    {
        T ret = default;
        int offset = 0;
        //double copy involved,
        //but since serializer doesn't support streams natively we would have to extract an array anyways
        Serializer.Deserialize(ref ret, stream.ToArray(), ref offset, (int) stream.Length);
        return ret;
    }
}