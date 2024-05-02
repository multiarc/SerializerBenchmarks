using Newtonsoft.Json;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class NewtonsoftJson<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    where T : class
{
    private JsonSerializer Formatter { get; } = JsonSerializer.Create(new JsonSerializerSettings
        {PreserveReferencesHandling = PreserveReferencesHandling.None});

    protected override void Serialize(T obj, MemoryStream stream)
    {
        var text = new StreamWriter(stream);
        Formatter.Serialize(text, obj);
        text.Flush();
    }

    protected override T Deserialize(MemoryStream stream)
    {
        TextReader text = new StreamReader(stream);
        return (T)Formatter.Deserialize(text, typeof(T));
    }
}