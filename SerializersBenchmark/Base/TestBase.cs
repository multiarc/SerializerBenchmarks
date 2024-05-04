using System.Runtime.CompilerServices;
using SerializersBenchmark.Network;

namespace SerializersBenchmark.Base;

public abstract class TestBase<T> : ISerializerTestAsync
{
    private readonly Func<int, T> _createTestDataStrategy;
    public object TestDataObject { get; private set; }
    
    protected TestBase(Func<int, T> testDataStrategy)
    {
        _createTestDataStrategy = testDataStrategy;
    }

    public abstract MemoryStream Serialize(object obj);

    public abstract object Deserialize(MemoryStream stream);

    public MemoryStream Setup(int numberOfObjects)
    {
        TestDataObject = _createTestDataStrategy(numberOfObjects);
        return Serialize(TestDataObject);
    }
    
    public virtual async Task SerializeAsync(object obj, Stream stream)
    {
        var memory = Serialize(obj);
        memory.Position = 0;
        await memory.CopyToAsync(stream);
    }

    public virtual async Task<object> DeserializeAsync(Stream stream)
    {
        var memory = new MemoryStream();
        await stream.CopyToAsync(memory);
        memory.Position = 0;
        return Deserialize(memory);
    }
}