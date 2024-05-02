using System.Text;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class FastJson<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
{
    protected override void Serialize(T obj, MemoryStream stream)
    {
        var text = new StreamWriter(stream, Encoding.UTF8);
        var jsonString = fastJSON.JSON.ToJSON(obj);
        text.WriteLine(jsonString);
        text.Flush();
    }

    protected override T Deserialize(MemoryStream stream)
    {
        var reader = new StreamReader(stream, Encoding.UTF8);
        return fastJSON.JSON.ToObject<T>(reader.ReadToEnd());
    }
}