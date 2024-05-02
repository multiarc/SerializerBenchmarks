#if NET6_0_OR_GREATER
using SerializersBenchmark.Base;
using SpanJson;

namespace SerializersBenchmark.Serializers
{
    public class SpanJson<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    {
        protected override void Serialize(T obj, MemoryStream stream)
        {
            stream.Write(JsonSerializer.Generic.Utf8.Serialize(obj)); 
        }

        protected override T Deserialize(MemoryStream stream)
        {
            return JsonSerializer.Generic.Utf8.Deserialize<T>(stream.GetBuffer());
        }
    }
}

#endif
