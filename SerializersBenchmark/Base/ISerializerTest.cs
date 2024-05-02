namespace SerializersBenchmark.Base;

public interface ISerializerTest
{
    void Serialize();
    object Deserialize();
    void Setup(int numberOfObjects);
}