using SerializersBenchmark.Base;
using Utf8Json;

namespace SerializersBenchmark.Serializers;

public class Utf8JsonSerializer<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
{
	public override MemoryStream Serialize(object obj)
	{
		var stream = new MemoryStream();
		JsonSerializer.Serialize(stream, (T)obj);
		return stream;
	}

	public override object Deserialize(MemoryStream stream)
	{
		return JsonSerializer.Deserialize<T>(stream);
	}

	public override async Task SerializeAsync(object obj, Stream stream)
	{
		await JsonSerializer.SerializeAsync(stream, (T)obj);
	}

	public override async Task<object> DeserializeAsync(Stream stream)
	{
		return await JsonSerializer.DeserializeAsync<T>(stream);
	}
}