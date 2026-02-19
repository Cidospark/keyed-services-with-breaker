using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyedServicesWithBreaker.Models;
using KeyedServicesWithBreaker.Services.Provider;

namespace KeyedServicesWithBreaker.Services
{
    public class ProcessPaymentCommandHandler
    {
        private readonly PaymentProviderSelector _selector;
        private readonly IPaymentExecutionEngine _engine;
        private readonly IProviderMetricsRegistry _metrics;

        public ProcessPaymentCommandHandler(PaymentProviderSelector selector,
                                            IPaymentExecutionEngine engine,
                                            IProviderMetricsRegistry metrics
                                         )
        {
             _selector = selector;
            _engine = engine;
            _metrics = metrics;
        }

        public async Task HandleAsync(ProcessPaymentCommand command)
        {
            var provider = await _selector.GetHealthyProviderAsync();
            var metrics = _metrics.GetMetrics(provider.Name);

            try
            {
                await _engine.ExecuteAsync(provider, command.Amount);
                metrics.RecordSuccess();
            }
            catch
            {
                metrics.RecordFailure();
                throw;
            }
        }
    }
}