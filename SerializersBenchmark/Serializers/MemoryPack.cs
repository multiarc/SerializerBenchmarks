#if !NET48
using System.Buffers;
using MemoryPack;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers
{
    public class MemoryPack<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
        where T : class
    {
        public override MemoryStream Serialize(object obj)
        {
            var stream = new MemoryStream();
            var writer = new ArrayBufferWriter<byte>();
            MemoryPackSerializer.Serialize(writer, (T)obj, options: null);
            stream.Write(writer.WrittenSpan);
            return stream;
        }

        public override object Deserialize(MemoryStream stream)
        {
            return MemoryPackSerializer.Deserialize<T>(stream.ToArray());
        }

        public override async Task SerializeAsync(object obj, Stream stream)
        {
            await MemoryPackSerializer.SerializeAsync(stream, (T)obj, options: null);
        }

        public override async Task<object> DeserializeAsync(Stream stream)
        {
            return await MemoryPackSerializer.DeserializeAsync<T>(stream, options: null);
        }
    }
}
#endif