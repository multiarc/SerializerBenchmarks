# SerializerBenchmarks
.NET Serializer testing benchmark

The project compiles to .NET 8, .NET 6 and .NET 4.8

![build](https://github.com/multiarc/SerializerBenchmarks/actions/workflows/dotnet.yml/badge.svg)

Implemented benchmarks for the following serializers:
- System.Runtime.Serialization.DataContract
- System.Text.JsonSerializer
- System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
- System.Xml.Serialization.XmlSerializer
- Bois (https://github.com/salarcode/Bois)
- Ceras (https://github.com/rikimaru0345/Ceras)
- FastJson ( https://github.com/mgholam/fastJSON/)
- GroBuf (https://github.com/skbkontur/GroBuf)
- JIL (https://github.com/kevin-montrose/Jil)
- Json.NET (http://www.newtonsoft.com/json)
- MemoryPack (https://github.com/Cysharp/MemoryPack)
- MessagePackSharp (https://github.com/MessagePack-CSharp/MessagePack-CSharp)
- MsgPack.Cli (https://github.com/msgpack/msgpack-cli)
- Protobuf.NET (https://github.com/mgravell/protobuf-net)
- Google Protobuf (https://github.com/protocolbuffers/protobuf)
- SerivceStack (https://github.com/ServiceStack/ServiceStack.Text)
- SpanJson (https://github.com/Tornhoof/SpanJson)
- UTF8Json (https://github.com/neuecc/Utf8Json)

#### Legacy and no longer supported Serializers
- BinaryFormatter
- BinaryPack (https://github.com/Sergio0694/BinaryPack)
