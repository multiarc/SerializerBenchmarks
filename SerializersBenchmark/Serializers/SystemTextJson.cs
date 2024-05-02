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
            JsonSerializer.Serialize(stream, obj, _serializerOptions);
            return stream;
        }

        public override object Deserialize(MemoryStream stream)
        {
            return (T) JsonSerializer.Deserialize(stream, typeof(T), _serializerOptions);
        }
    }
}