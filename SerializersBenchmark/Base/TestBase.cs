using System.Runtime.CompilerServices;

namespace SerializersBenchmark.Base;

public abstract class TestBase<T> : ISerializerTest
{
    private readonly Func<int, T> _createTestDataStrategy;

    protected T TestDataObject;
    
    protected TestBase(Func<int, T> testDataStrategy)
    {
        _createTestDataStrategy = testDataStrategy;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    protected abstract void Serialize(T obj, MemoryStream stream);

    [MethodImpl(MethodImplOptions.NoInlining)]
    protected abstract T Deserialize(MemoryStream stream);

    protected virtual Func<MemoryStream, T> CustomDeserialize => Deserialize;

    protected virtual Action<MemoryStream> CustomSerialize => s => Serialize(TestDataObject, s);

    MemoryStream _serializedValue;

    protected MemoryStream GetMemoryStream()
    {
        if (_serializedValue == null)
        {
            _serializedValue = new MemoryStream();
        }
        _serializedValue.Position = 0;
        return _serializedValue;
    }
        
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Serialize()
    {
        var dataStream = GetMemoryStream();
        CustomSerialize(dataStream);
    }
        
    [MethodImpl(MethodImplOptions.NoInlining)]
    public object Deserialize()
    {
        var dataStream = GetMemoryStream();
        return CustomDeserialize(dataStream);
    }

    public void Setup(int numberOfObjects)
    {
        TestDataObject = _createTestDataStrategy(numberOfObjects);
        //fill in memory stream to a size with serialized value
        Serialize();
    }
}