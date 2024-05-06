using SerializersBenchmark.Base;
using ServiceStack.Text;

namespace SerializersBenchmark.Serializers;

public class ServiceStack<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    where T : class
{
    public override MemoryStream Serialize(object obj)
    {
        var stream = new MemoryStream();
        JsonSerializer.SerializeToStream(obj, stream);
        return stream;
    }

    public override object Deserialize(MemoryStream stream)
    {
        return JsonSerializer.DeserializeFromStream<T>(stream);
    }

    public override async Task SerializeAsync(object obj, Stream stream)
    {
        await JsonSerializer.SerializeToStreamAsync(obj, stream);
    }

    public override async Task<object> DeserializeAsync(Stream stream)
    {
        return await JsonSerializer.DeserializeFromStreamAsync<T>(stream);
    }
}