using SerializersBenchmark.Models;
using SerializersBenchmark.Serializers;

namespace SerializersBenchmark.Base;

public static class SerializerFactoryExtensions
{
    public static ISerializerTestAsync CreateSerializerInstance(this Type serializerType) {
        ISerializerTestAsync serializer;
        if (serializerType == typeof(GoogleProtobuf<ProtobufDataItem>)) {
            serializer = (ISerializerTestAsync) Activator.CreateInstance(serializerType,
                (Func<int, ProtobufDataItem>) CreateDataExtensions.ProtobufData);
        }
#if NET6_0_OR_GREATER        
        else if (serializerType == typeof(MemoryPack<DataItemMemoryPack>)) {
            serializer = (ISerializerTestAsync) Activator.CreateInstance(serializerType,
                (Func<int, DataItemMemoryPack>) CreateDataExtensions.DataMemoryPack);
        }
#endif
        else {
            serializer = (ISerializerTestAsync) Activator.CreateInstance(serializerType,
                (Func<int, DataItem>) CreateDataExtensions.Data);
        }

        return serializer;
    }
}