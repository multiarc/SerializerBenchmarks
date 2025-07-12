using System.Runtime.Serialization;
#if NET6_0_OR_GREATER
using BinaryPack.Attributes;
using BinaryPack.Enums;
using MemoryPack;
#endif
using MessagePack;
using ProtoBuf;

namespace SerializersBenchmark.Models;

[Serializable, DataContract, ProtoContract, MessagePackObject(AllowPrivate = true)
#if NET6_0_OR_GREATER
 , BinarySerialization(SerializationMode.Properties | SerializationMode.NonPublicMembers)
#endif
]
public partial class DataItem
{
    [DataMember, ProtoMember(1), Key(0)]
    public List<ChildDataItem> Children { get; set; }

    [DataMember, ProtoMember(2), Key(1)]
    private string _privateMember;

    public DataItem(string privateMember)
    {
        _privateMember = privateMember;
    }
    public DataItem()
    {
        
    }
}

[Serializable, DataContract, ProtoContract, MessagePackObject
#if NET6_0_OR_GREATER
 , MemoryPackable, BinarySerialization(SerializationMode.AllMembers)
#endif
]
public partial class ChildDataItem
{
    
    [DataMember, ProtoMember(1), Key(0)]
    public string Title;

    [DataMember, ProtoMember(2), Key(1)]
    public int Id;

    [DataMember, ProtoMember(3), Key(2)]
    public byte[] ArbitraryData;
}