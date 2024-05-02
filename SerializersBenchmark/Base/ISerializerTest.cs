namespace SerializersBenchmark.Base;

public interface ISerializerTest
{
    MemoryStream Serialize(object value);
    object Deserialize(MemoryStream stream);
    MemoryStream Setup(int numberOfObjects);
    object TestDataObject { get; }
}