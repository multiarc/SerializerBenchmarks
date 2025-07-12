using Google.Protobuf;

namespace SerializersBenchmark.Models;

public static class CreateDataExtensions
{
    public static DataItem Data(int itemsToCreate)
    {
        return new DataItem("private value")
        {
            Children = Enumerable.Range(1, itemsToCreate).Select(i => new ChildDataItem
                {
                    Id = i,
                    Title = $"Child {i}",
                    ArbitraryData = CreateAndFillByteBuffer(),
                }
            ).ToList()
        };
    }
    
#if NET6_0_OR_GREATER    
    public static DataItemMemoryPack DataMemoryPack(int itemsToCreate)
    {
        return new DataItemMemoryPack("private value")
        {
            Children = Enumerable.Range(1, itemsToCreate).Select(i => new ChildDataItemMemoryPack
                {
                    Id = i,
                    Title = $"Child {i}",
                    ArbitraryData = CreateAndFillByteBuffer(),
                }
            ).ToList()
        };
    }
#endif
    
    public static ProtobufDataItem ProtobufData(int nToCreate)
    {
        var childDataItems = Enumerable.Range(1, nToCreate).Select(i =>
            new ProtobufChildDataItem
            {
                Id = i,
                Title = $"Child {i}",
                ArbitraryData = ByteString.CopyFrom(CreateAndFillByteBuffer())
            }).ToList();

        var result = new ProtobufDataItem();
        result.Children.AddRange(childDataItems);
        return result;
    }

    private static byte[] CreateAndFillByteBuffer()
    {
        byte[] optionalPayload = new byte[100];

        for (int j = 0; j < optionalPayload.Length; j++)
        {
            optionalPayload[j] = (byte) (j % 26 + 'a');
        }

        return optionalPayload;
    }
}