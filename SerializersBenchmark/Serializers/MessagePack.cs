using MessagePack;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class MessagePack<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    where T : class
{
    public override MemoryStream Serialize(object obj)
    {
        var stream = new MemoryStream();
        MessagePackSerializer.Serialize(typeof(T), stream, obj);
        return stream;
    }

    public override object Deserialize(MemoryStream stream)
    {
        return MessagePackSerializer.Deserialize<T>(stream);
    }

    public override async Task SerializeAsync(object obj, Stream stream)
    {
        await MessagePackSerializer.SerializeAsync(stream, (T) obj);
    }
    public override async Task<object> DeserializeAsync(Stream stream)
    {
        return await MessagePackSerializer.DeserializeAsync<T>(stream);
    }
}