Needs 3 nugets

- Grpc.Tools (.proto files are source generated to .cs files)
- Google.Protobuf (needed as reference for the source generated cs files)
- Grpc (needed as reference for the source generated cs files)
---
In .csproj file add the ItemGroup to auto generated the cs from proto files for server and client
```config
<ItemGroup>
  <Protobuf Include="Protos\customers.proto" GrpcService="Client and Server" />
</ItemGroup>
```
---