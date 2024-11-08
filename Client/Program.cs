﻿using System;
using Core;
using Grpc.Core;
using Grpc.Net.Client;

namespace Client;

internal static class Program {
    private static Customer.CustomerClient _customerClient;

    private static async Task Main(string[] args){
        // ---------------------------------------------------------------------
        var channel = GrpcChannel.ForAddress("http://localhost:5000", new GrpcChannelOptions {
            Credentials = ChannelCredentials.Insecure
        });
        _customerClient = new Customer.CustomerClient(channel);
        
        // ---------------------------------------------------------------------
        Console.WriteLine();
        Console.WriteLine("Output existing customer");
        for (var i = 1; i <= 3; i++) {
            await OutputCustomer(i);
        }
        
        // ---------------------------------------------------------------------
        Console.WriteLine();
        Console.WriteLine("Output new customers from stream");
        using (var stream = _customerClient.GetNewCustomersAsStream(new Empty())) {
            while (await stream.ResponseStream.MoveNext()) {
                var customer = stream.ResponseStream.Current;
                Console.WriteLine($"{customer.Firstname}, {customer.Lastname}");
            }
        }
        
        // ---------------------------------------------------------------------
        Console.WriteLine();
        Console.WriteLine("Output new customers from arary");
        var newCustomerArray = await _customerClient.GetNewCustomersAsArrayAsync(new Empty());
        foreach (var newCustomer in newCustomerArray.Customers) {
            Console.WriteLine($"{newCustomer.Firstname}, {newCustomer.Lastname}");
        }
    }

    private static async Task OutputCustomer(int id) {
        var request = new CustomerLockup { Id = id };
        var reply = await _customerClient.GetCustomersAsync(request);
        Console.WriteLine($"{reply.Firstname}, {reply.Lastname}");
    }
}