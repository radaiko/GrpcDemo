Needs no extra nugets when dotnet gprc service template

---

Every service must inherits from the base class which is generated

---

Needs reference to shared proto project

---
New services has to added in program.cs like that
```csharp
app.MapGrpcService<CustomersService>();
```