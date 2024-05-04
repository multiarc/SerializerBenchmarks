using MsgPack.Serialization;
using SerializersBenchmark.Base;

namespace SerializersBenchmark.Serializers;

public class MsgPackCli<T>(Func<int, T> testDataStrategy) : TestBase<T>(testDataStrategy)
    where T : class
{
    private MessagePackSerializer Serializer { get; } = MessagePackSerializer.Get<T>();

    public override MemoryStream Serialize(object obj)
    {
        var stream = new MemoryStream();
        Serializer.Pack(stream, obj);
        return stream;
    }

    public override object Deserialize(MemoryStream stream)
    {
        return (T) Serializer.Unpack(stream);
    }

    public override async Task SerializeAsync(object obj, Stream stream)
    {
        var packer = MsgPack.Packer.Create(stream);
        await Serializer.PackToAsync(packer, obj, CancellationToken.None);
    }

    public override async Task<object> DeserializeAsync(Stream stream)
    {
        var unpacker = MsgPack.Unpacker.Create(stream);
        return await Serializer.UnpackFromAsync(unpacker, CancellationToken.None);
    }
}