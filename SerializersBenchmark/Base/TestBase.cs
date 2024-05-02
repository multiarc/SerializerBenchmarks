using System.Runtime.CompilerServices;

namespace SerializersBenchmark.Base;

public abstract class TestBase<T> : ISerializerTest
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
}