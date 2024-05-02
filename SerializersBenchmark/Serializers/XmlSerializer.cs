using System.Xml.Serialization;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class XmlSerializer<T> : TestBase<T>
{
    public XmlSerializer(Func<int, T> testDataStrategy) : base(testDataStrategy)
    {
    }

    private XmlSerializer Formatter { get; } = new(typeof(T));

    protected override void Serialize(T obj, MemoryStream stream)
    {
        Formatter.Serialize(stream, obj);
    }

    protected override T Deserialize(MemoryStream stream)
    {
        return (T)Formatter.Deserialize(stream);
    }
}