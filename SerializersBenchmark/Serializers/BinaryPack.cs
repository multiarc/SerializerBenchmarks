#if NET6_0_OR_GREATER
using BinaryPack;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers
{
    public class BinaryPack<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
        where T : class, new()
    {
        protected override void Serialize(T obj, MemoryStream stream)
        {
            BinaryConverter.Serialize(obj, stream);
        }

        protected override T Deserialize(MemoryStream stream)
        {
            return BinaryConverter.Deserialize<T>(stream);
        }
    }
}
#endif
