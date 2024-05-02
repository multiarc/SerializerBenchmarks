using System.Xml.Serialization;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class XmlSerializer<T> : TestBase<T>
{
    public XmlSerializer(Func<int, T> testDataStrategy) : base(testDataStrategy)
    {
    }

    private XmlSerializer Serializer { get; } = new(typeof(T));

    public override MemoryStream Serialize(object obj)
    {
        var stream = new MemoryStream();
        Serializer.Serialize(stream, obj);
        return stream;
    }

    public override object Deserialize(MemoryStream stream)
    {
        return (T) Serializer.Deserialize(stream);
    }
}