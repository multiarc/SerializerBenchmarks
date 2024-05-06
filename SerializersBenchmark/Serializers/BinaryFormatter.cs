using System.Runtime.Serialization.Formatters.Binary;
using SerializersBenchmark.Base;
using SerializersBenchmark.Network;

namespace SerializersBenchmark.Serializers;
#pragma warning disable SYSLIB0011 // Type or member is obsolete
public class BinaryFormatter<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    where T : class
{
    private BinaryFormatter Serializer { get; } = new();

    public override MemoryStream Serialize(object obj)
    {
        var result = new MemoryStream();
        Serializer.Serialize(result, obj);
        return result;
    }

    public override object Deserialize(MemoryStream stream)
    {
        return Serializer.Deserialize(stream);
    }
    
    //no actual async implementation exists, using default
}

#pragma warning restore SYSLIB0011 // Type or member is obsolete