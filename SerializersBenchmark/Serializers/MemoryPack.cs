#if !NET48
using System.Buffers;
using MemoryPack;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers
{
    public class MemoryPack<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
        where T : class
    {
        protected override void Serialize(T obj, MemoryStream stream)
        {
            var writer = new ArrayBufferWriter<byte>();
            MemoryPackSerializer.Serialize(writer, obj, options: null);
            stream.Write(writer.GetMemory().Span);
        }

        protected override T Deserialize(MemoryStream stream)
        {
            return MemoryPackSerializer.Deserialize<T>(stream.GetBuffer());
        }
    }
}
#endif