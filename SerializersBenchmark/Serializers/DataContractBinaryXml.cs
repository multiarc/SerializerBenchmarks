using System.Runtime.Serialization;
using System.Xml;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class DataContractBinaryXml<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
{
    private DataContractSerializer Formatter { get; } = new(typeof(T));

    protected override Action<MemoryStream> CustomSerialize
    {
        get
        {
            return stream =>
            {
                var binaryWriter = XmlDictionaryWriter.CreateBinaryWriter(stream);
                Formatter.WriteObject(binaryWriter, TestDataObject);
                binaryWriter.Flush();
            };
        }
    }

    protected override Func<MemoryStream, T> CustomDeserialize
    {
        get
        {
            return stream =>
            {
                var binaryReader = XmlDictionaryReader.CreateBinaryReader(stream, XmlDictionaryReaderQuotas.Max);
                T deserialized = (T) Formatter.ReadObject(binaryReader);
                return deserialized;
            };
        }
    }

    protected override void Serialize(T obj, MemoryStream stream)
    {
        throw new NotSupportedException();
    }

    protected override T Deserialize(MemoryStream stream)
    {
        throw new NotSupportedException();
    }
}