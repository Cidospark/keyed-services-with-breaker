using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyedServicesWithBreaker.Services;

namespace KeyedServicesWithBreaker.Shared
{
    public abstract class PaymentProviderBase : IPaymentService
    {
        protected readonly HttpClient Http;
        protected PaymentProviderBase(HttpClient http)
        {
            Http = http;
        }

        public abstract string Name { get; }

        public async Task<bool> IsHealthyAsync()
        {
            var response = await Http.GetAsync("/health");
            return response.IsSuccessStatusCode;
        }

        public abstract Task ProcessAsync(decimal amount);
    }
}