#if NET6_0_OR_GREATER
using BinaryPack;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers
{
    public class BinaryPack<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
        where T : class, new()
    {
        public override MemoryStream Serialize(object obj)
        {
            var stream = new MemoryStream();
            BinaryConverter.Serialize((T)obj, stream);
            return stream;
        }

        public override object Deserialize(MemoryStream stream)
        {
            return BinaryConverter.Deserialize<T>(stream);
        }
        
        //no actual async implementation exists, using default
    }
}
#endif
