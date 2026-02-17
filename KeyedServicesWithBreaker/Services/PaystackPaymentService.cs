using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyedServicesWithBreaker.Shared;

namespace KeyedServicesWithBreaker.Services
{
    public class PaystackPaymentService : PaymentProviderBase
    { 
        public override string Name => "paystack";

        public PaystackPaymentService(HttpClient http) : base(http) { }

        
        public override async Task ProcessAsync(decimal amount)
        {
            var response = await Http.PostAsJsonAsync(
            "/transaction/initialize",
            new { amount });

            response.EnsureSuccessStatusCode();
        }
    }
}