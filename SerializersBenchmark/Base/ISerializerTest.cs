namespace SerializersBenchmark.Base;

public interface ISerializerTest
{
    MemoryStream Serialize(object value);
    object Deserialize(MemoryStream stream);
    MemoryStream Setup(int numberOfObjects);
    object TestDataObject { get; }
}

public interface ISerializerTestAsync: ISerializerTest
{
    Task SerializeAsync(object obj, Stream stream);
    Task<object> DeserializeAsync(Stream stream);
}