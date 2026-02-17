using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Polly;

namespace KeyedServicesWithBreaker.Services.Provider
{
    public class ProviderPolicyRegistry : IProviderPolicyRegistry
    {
        private readonly ConcurrentDictionary<string, IAsyncPolicy> _policies = new();

        public IAsyncPolicy GetPolicy(string providerName)
        {
            return _policies.GetOrAdd(providerName, _ =>
            {
                return Policy
                    .Handle<Exception>()
                    .CircuitBreakerAsync(
                        exceptionsAllowedBeforeBreaking: 3,
                        durationOfBreak: TimeSpan.FromSeconds(30));
            });
        }
    }
}