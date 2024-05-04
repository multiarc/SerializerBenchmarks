using System.Text;
using Newtonsoft.Json;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class NewtonsoftJson<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    where T : class
{
    private JsonSerializer Serializer { get; } = JsonSerializer.Create(new JsonSerializerSettings
        {PreserveReferencesHandling = PreserveReferencesHandling.None});

    public override MemoryStream Serialize(object obj)
    {
        var stream = new MemoryStream();
        using var writer = new StreamWriter(stream, Encoding.UTF8, bufferSize: 1024, leaveOpen: true);
        Serializer.Serialize(writer, obj);
        writer.Flush();
        return stream;
    }

    public override object Deserialize(MemoryStream stream)
    {
        using TextReader reader = new StreamReader(stream, Encoding.UTF8, bufferSize: 1024,
            detectEncodingFromByteOrderMarks: false, leaveOpen: true);
        return (T) Serializer.Deserialize(reader, typeof(T));
    }
}