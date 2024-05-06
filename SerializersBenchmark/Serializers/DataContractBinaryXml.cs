using System.Runtime.Serialization;
using System.Xml;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class DataContractBinaryXml<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
{
    private DataContractSerializer Serializer { get; } = new(typeof(T));

    public override MemoryStream Serialize(object obj)
    {
        var stream = new MemoryStream();
        var binaryWriter = XmlDictionaryWriter.CreateBinaryWriter(stream);
        Serializer.WriteObject(binaryWriter, TestDataObject);
        binaryWriter.Flush();
        return stream;
    }

    public override object Deserialize(MemoryStream stream)
    {
        var binaryReader = XmlDictionaryReader.CreateBinaryReader(stream, XmlDictionaryReaderQuotas.Max);
        return Serializer.ReadObject(binaryReader);
    }
    
    //no actual async implementation exists, using default
}