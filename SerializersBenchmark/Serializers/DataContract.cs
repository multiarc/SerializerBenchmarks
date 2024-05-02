using System.Runtime.Serialization;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class DataContract<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
{
    private DataContractSerializer Formatter { get; } = new(typeof(T));

    protected override void Serialize(T obj, MemoryStream stream)
    {
        Formatter.WriteObject(stream, obj);
    }

    protected override T Deserialize(MemoryStream stream)
    {
        return (T)Formatter.ReadObject(stream);
    }
}