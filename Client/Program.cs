using System;
using Core;
using Grpc.Core;
using Grpc.Net.Client;

namespace Client;

internal static class Program {
    private static Customer.CustomerClient _customerClient;

    private static async Task Main(string[] args) {
        var channel = GrpcChannel.ForAddress("http://localhost:5000", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        _customerClient = new Customer.CustomerClient(channel);
        for (var i = 1; i <= 3; i++) {
            await OutputCustomer(i);
        }
    }

    private static async Task OutputCustomer(int id) {
        var request = new CustomerLockup { Id = id };
        var reply = await _customerClient.GetCustomersAsync(request);
        Console.WriteLine($"{reply.Firstname}, {reply.Lastname}");
    }
}