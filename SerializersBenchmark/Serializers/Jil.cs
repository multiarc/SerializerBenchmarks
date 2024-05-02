using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class Jil<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    where T : class
{
    public override MemoryStream Serialize(object obj)
    {
        var stream = new MemoryStream();
        var text = new StreamWriter(stream);
        Jil.JSON.Serialize(obj, text);
        text.Flush();
        return stream;
    }

    public override object Deserialize(MemoryStream stream)
    {
        TextReader text = new StreamReader(stream);
        return Jil.JSON.Deserialize<T>(text);
    }
}