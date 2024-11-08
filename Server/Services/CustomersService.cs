using Core;
using Grpc.Core;

namespace Server.Services;

public class CustomersService : Customer.CustomerBase {
  private readonly ILogger<CustomersService> _logger;
  public CustomersService(ILogger<CustomersService> logger) {
    _logger = logger;
  }

  public override Task<CustomerModel> GetCustomers(CustomerLockup request, ServerCallContext context) {
    CustomerModel output = new();

    if (request.Id == 1) {
      output.Firstname = "Jamie";
      output.Lastname = "Smith";
    }
    else if (request.Id == 2) {
      output.Firstname = "Alex";
      output.Lastname = "Johnson";
    }
    else {
      output.Firstname = "Greg";
      output.Lastname = "Thomas";
    }

    return Task.FromResult(output);
  }

  public override Task<CustomersModel> GetNewCustomersAsArray(Empty request, ServerCallContext context) {
    List<CustomerModel> newCustomers = [
      new() { Firstname = "a", Lastname = "b" }, 
      new() { Firstname = "c", Lastname = "d" }, 
      new() { Firstname = "e", Lastname = "f" }
    ];

    CustomersModel output = new();
    output.Customers.AddRange(newCustomers);
    
    return Task.FromResult(output);
  }

  public override async Task GetNewCustomersAsStream(Empty request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context) {
    List<CustomerModel> newCustomers = [
      new() { Firstname = "a", Lastname = "b" }, 
      new() { Firstname = "c", Lastname = "d" }, 
      new() { Firstname = "e", Lastname = "f" }
    ];

    foreach (var newCustomer in newCustomers) {
      await responseStream.WriteAsync(newCustomer);
    }
    
  }
}