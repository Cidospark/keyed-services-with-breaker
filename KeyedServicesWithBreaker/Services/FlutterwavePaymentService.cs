using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyedServicesWithBreaker.Shared;

namespace KeyedServicesWithBreaker.Services
{
    public class FlutterwavePaymentService : PaymentProviderBase
    {
         public override string Name => "flutterwave";

         public FlutterwavePaymentService(HttpClient http) : base(http) { }

        public override Task ProcessAsync(decimal amount)
        {
            Console.WriteLine($"Processing ₦{amount} via Flutterwave");
            return Task.CompletedTask;
        }
    }
}