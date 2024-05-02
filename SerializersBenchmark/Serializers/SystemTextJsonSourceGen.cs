#if NET8_0

using System.Text.Json;
using System.Text.Json.Serialization;
using SerializersBenchmark.Base;
using SerializersBenchmark.Models;

namespace SerializersBenchmark.Serializers
{
    [JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default, IncludeFields = true)]
    [JsonSerializable(typeof(DataItem))]
    internal partial class MyJsonContext : JsonSerializerContext
    {
    }

    public class SystemTextJsonSourceGen<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    {
        protected override void Serialize(T obj, MemoryStream stream)
        {
            JsonSerializer.Serialize(stream, obj, MyJsonContext.Default.Options);
        }

        protected override T Deserialize(MemoryStream stream)
        {
            return JsonSerializer.Deserialize<T>(stream, MyJsonContext.Default.Options);
        }
    }
}

#endif
