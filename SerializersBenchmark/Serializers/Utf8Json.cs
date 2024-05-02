using SerializersBenchmark.Base;
using Utf8Json;

namespace SerializersBenchmark.Serializers;

public class Utf8JsonSerializer<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
{
	public override MemoryStream Serialize(object obj)
	{
		var stream = new MemoryStream();
		JsonSerializer.Serialize(stream, obj);
		return stream;
	}

	public override object Deserialize(MemoryStream stream)
	{
		return JsonSerializer.Deserialize<T>(stream);
	}
}