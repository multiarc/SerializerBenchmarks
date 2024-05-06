using System.Runtime.Serialization;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class DataContract<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
{
    private DataContractSerializer Serializer { get; } = new(typeof(T));

    public override MemoryStream Serialize(object obj)
    {
        var stream = new MemoryStream();
        Serializer.WriteObject(stream, obj);
        return stream;
    }

    public override object Deserialize(MemoryStream stream)
    {
        return (T) Serializer.ReadObject(stream);
    }
    
    //no actual async implementation exists, using default
}