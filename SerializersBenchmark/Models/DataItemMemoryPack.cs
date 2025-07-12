#if NET6_0_OR_GREATER
using System.Runtime.Serialization;
using MemoryPack;
using MessagePack;
using ProtoBuf;

namespace SerializersBenchmark.Models;

[MemoryPackable]
public partial class DataItemMemoryPack
{
    [DataMember, ProtoMember(1), Key(0)]
    public List<ChildDataItemMemoryPack> Children { get; set; }

    [DataMember, ProtoMember(2), Key(1)]
    private string _privateMember;

    public DataItemMemoryPack(string privateMember)
    {
        _privateMember = privateMember;
    }
    [MemoryPackConstructor]
    public DataItemMemoryPack()
    {
        
    }
}

[MemoryPackable]
public partial class ChildDataItemMemoryPack
{
    
    [DataMember, ProtoMember(1), Key(0)]
    public string Title;

    [DataMember, ProtoMember(2), Key(1)]
    public int Id;

    [DataMember, ProtoMember(3), Key(2)]
    public byte[] ArbitraryData;
}
#endif