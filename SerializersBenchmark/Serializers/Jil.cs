using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class Jil<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    where T : class
{
    protected override void Serialize(T obj, MemoryStream stream)
    {
        var text = new StreamWriter(stream);
        Jil.JSON.Serialize(obj, text);
        text.Flush();
    }

    protected override T Deserialize(MemoryStream stream)
    {
        TextReader text = new StreamReader(stream);
        return Jil.JSON.Deserialize<T>(text);
    }
}