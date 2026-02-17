using KeyedServicesWithBreaker.Services.Provider;
using Polly;
using Polly.Wrap;

namespace KeyedServicesWithBreaker.Services
{
    public class PaymentExecutionEngine : IPaymentExecutionEngine
    {
        private readonly IProviderPolicyRegistry _policyRegistry;
        public PaymentExecutionEngine(IProviderPolicyRegistry policyRegistry)
        {
            _policyRegistry = policyRegistry;
        }
        public async Task ExecuteAsync(IPaymentService provider, decimal amount)
        {
            var policy = _policyRegistry.GetPolicy(provider.Name);
            await policy.ExecuteAsync(() => provider.ProcessAsync(amount) );
        }
    }
}