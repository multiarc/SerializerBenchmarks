using System.Runtime.Serialization.Formatters.Binary;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;
#pragma warning disable SYSLIB0011 // Type or member is obsolete
public class BinaryFormatter<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    where T : class
{
    private BinaryFormatter Formatter { get; } = new();

    protected override void Serialize(T obj, MemoryStream stream)
    {
            Formatter.Serialize(stream, obj);
        }

    protected override T Deserialize(MemoryStream stream)
    {
            return (T)Formatter.Deserialize(stream);
        }
}

#pragma warning restore SYSLIB0011 // Type or member is obsolete