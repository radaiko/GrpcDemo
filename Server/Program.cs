using Server.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

// Configure Kestrel to allow HTTP/2 without TLS
builder.WebHost.ConfigureKestrel(options =>
{
  options.ListenLocalhost(5000, o => o.Protocols = HttpProtocols.Http2);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<CustomersService>();
app.Run();