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
            MemoryPackSerializer.Serialize(writer, obj, options: null);
            stream.Write(writer.GetMemory().Span);
            return stream;
        }

        public override object Deserialize(MemoryStream stream)
        {
            return MemoryPackSerializer.Deserialize<T>(stream.ToArray());
        }
    }
}
#endif