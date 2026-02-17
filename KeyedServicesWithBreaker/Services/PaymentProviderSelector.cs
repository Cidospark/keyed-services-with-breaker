using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyedServicesWithBreaker.Models;
using KeyedServicesWithBreaker.Services.Provider;
using Microsoft.Extensions.Options;

namespace KeyedServicesWithBreaker.Services
{
    public class PaymentProviderSelector
    {
        private readonly Func<string, IPaymentService> _resolver;
        private readonly IProviderMetricsRegistry _metrics;

        public PaymentProviderSelector(Func<string, IPaymentService> resolver, IProviderMetricsRegistry metrics)
        {
            _resolver = resolver;
            _metrics = metrics;
        }
        
        public async Task<IPaymentService> GetHealthyProviderAsync()
        {
            var orderedProviderScores = _metrics.GetProviderScores();
            foreach (var ps in orderedProviderScores)
            {
                var provider = _resolver(ps.Provider);
                if (await provider.IsHealthyAsync())
                {
                    return provider;
                }
            }
            throw new InvalidOperationException("No healthy payment providers available");
        }
    }
}