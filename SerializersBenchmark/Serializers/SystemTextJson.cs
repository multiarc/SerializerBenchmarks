#if NET6_0_OR_GREATER

using System.Text.Json;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers
{
    public class SystemTextJson<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    {
        private readonly JsonSerializerOptions _serializerOptions = new() { IncludeFields = true };

        protected override void Serialize(T obj, MemoryStream stream)
        {
            JsonSerializer.Serialize(stream, obj, _serializerOptions);
        }

        protected override T Deserialize(MemoryStream stream)
        {
            return (T) JsonSerializer.Deserialize(stream, typeof(T), _serializerOptions);
        }
    }
}

#endif
