using GroBuf;
using GroBuf.DataMembersExtracters;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class GroBuf<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    where T : class
{
    private Serializer Serializer { get; } =
        new(new AllFieldsExtractor(), options: GroBufOptions.WriteEmptyObjects);

    public override MemoryStream Serialize(object obj)
    {
        var stream = new MemoryStream();
        //double copy due to streams not supported
        var bytes = Serializer.Serialize((T)obj);
        stream.Write(bytes, 0, bytes.Length);
        return stream;
    }

    public override object Deserialize(MemoryStream stream)
    {
        //double copy due to streams not supported
        return Serializer.Deserialize<T>(stream.ToArray(), (int) stream.Length);
    }
    
    //no actual async implementation exists, using default
}