#if NET6_0_OR_GREATER
using SerializersBenchmark.Base;
using SpanJson;

namespace SerializersBenchmark.Serializers
{
    public class SpanJson<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    {
        public override MemoryStream Serialize(object obj)
        {
            var stream = new MemoryStream();
            stream.Write(JsonSerializer.Generic.Utf8.Serialize((T)obj));
            return stream;
        }

        public override object Deserialize(MemoryStream stream)
        {
            return JsonSerializer.Generic.Utf8.Deserialize<T>(stream.ToArray());
        }

        public override async Task SerializeAsync(object obj, Stream stream)
        {
            await JsonSerializer.Generic.Utf8.SerializeAsync((T) obj, stream);
        }

        public override async Task<object> DeserializeAsync(Stream stream)
        {
            return await JsonSerializer.Generic.Utf8.DeserializeAsync<T>(stream);
        }
    }
}

#endif