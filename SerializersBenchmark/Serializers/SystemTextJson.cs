using System.Text.Json;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers
{
    public class SystemTextJson<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    {
        private readonly JsonSerializerOptions _serializerOptions = new() {IncludeFields = true};

        public override MemoryStream Serialize(object obj)
        {
            var stream = new MemoryStream();
            JsonSerializer.Serialize(stream, (T)obj, _serializerOptions);
            return stream;
        }

        public override object Deserialize(MemoryStream stream)
        {
            return JsonSerializer.Deserialize<T>(stream, _serializerOptions);
        }

        public override async Task SerializeAsync(object obj, Stream stream)
        {
            await JsonSerializer.SerializeAsync(stream, (T)obj, _serializerOptions);
        }

        public override async Task<object> DeserializeAsync(Stream stream)
        {
            return await JsonSerializer.DeserializeAsync<T>(stream, _serializerOptions);
        }
    }
}