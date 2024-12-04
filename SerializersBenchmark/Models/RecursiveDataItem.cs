using System.Runtime.Serialization;
#if NET6_0_OR_GREATER
using BinaryPack.Attributes;
using BinaryPack.Enums;
using MemoryPack;
#endif
using MessagePack;
using ProtoBuf;

namespace SerializersBenchmark.Models;

[Serializable, DataContract, ProtoContract, MessagePackObject
#if NET6_0_OR_GREATER
 , MemoryPackable, BinarySerialization(SerializationMode.Properties | SerializationMode.NonPublicMembers)
#endif
]
public partial class RecursiveDataItem
{
    [DataMember, ProtoMember(1), Key(0)]
    public RecursiveDataItem Next { get; set; }

    [DataMember, ProtoMember(2), Key(1)]
    public string Text { get; set; }
    
#if NET6_0_OR_GREATER
    [MemoryPackConstructor]
#endif
    public RecursiveDataItem()
    {
        
    }
}