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
        private readonly string[] _priorityOrder;

        public PaymentProviderSelector(Func<string, IPaymentService> resolver, IProviderMetricsRegistry metrics, IOptions<PaymentProviderOptions> options)
        {
            _resolver = resolver;
            _metrics = metrics;
            _priorityOrder = options.Value.PriorityOrder;
        }
        
        public async Task<IPaymentService> GetHealthyProviderAsync()
        {
            var orderedProviderScores = _metrics.GetScores();
            string[] providers = [];
            providers = orderedProviderScores.Count > 0 ? orderedProviderScores.Select(x => x.Provider).ToArray() : _priorityOrder;
             
            foreach (var p in providers)
            {
                var provider = _resolver(p);
                if (await provider.IsHealthyAsync())
                {
                    return provider;
                }
            }
            throw new InvalidOperationException("No healthy payment providers available");
        }
    }
}