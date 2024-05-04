using System.Text;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class FastJson<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
{
    public override MemoryStream Serialize(object obj)
    {
        var stream = new MemoryStream();
        var text = new StreamWriter(stream, Encoding.UTF8);
        var jsonString = fastJSON.JSON.ToJSON(obj);
        text.WriteLine(jsonString);
        text.Flush();
        return stream;
    }

    public override object Deserialize(MemoryStream stream)
    {
        var reader = new StreamReader(stream, Encoding.UTF8);
        return fastJSON.JSON.ToObject<T>(reader.ReadToEnd());
    }
    
    //no actual async implementation exists, using default
}