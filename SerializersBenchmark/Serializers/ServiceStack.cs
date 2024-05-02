using SerializersBenchmark.Base;
using ServiceStack.Text;

namespace SerializersBenchmark.Serializers;

public class ServiceStack<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    where T : class
{
    protected override void Serialize(T obj, MemoryStream stream)
    {
        JsonSerializer.SerializeToStream(obj, stream);
    }

    protected override T Deserialize(MemoryStream stream)
    {
        return JsonSerializer.DeserializeFromStream<T>(stream);
    }
}