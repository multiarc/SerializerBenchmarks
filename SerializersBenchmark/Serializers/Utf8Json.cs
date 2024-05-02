using SerializersBenchmark.Base;
using Utf8Json;

namespace SerializersBenchmark.Serializers;

public class Utf8JsonSerializer<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
{
	protected override void Serialize(T obj, MemoryStream stream)
	{
		JsonSerializer.Serialize(stream, obj);
	}

	protected override T Deserialize(MemoryStream stream)
	{
		return JsonSerializer.Deserialize<T>(stream);
	}
}