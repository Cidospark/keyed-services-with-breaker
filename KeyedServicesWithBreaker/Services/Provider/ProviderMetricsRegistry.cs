using System.Collections.Concurrent;
using KeyedServicesWithBreaker.Models.Entities;

namespace KeyedServicesWithBreaker.Services.Provider
{
    public class ProviderMetricsRegistry : IProviderMetricsRegistry
    {
        private readonly ConcurrentDictionary<string, ProviderMetricsModel> _metrics = new();
        public ProviderMetricsModel GetMetrics(string providerName)
        {
            return _metrics.GetOrAdd(providerName, _ => new ProviderMetricsModel());
        }

        public IReadOnlyList<ProviderScore> GetScores()
        {
            return _metrics.Select(kvp =>
            {
                var total = kvp.Value.SuccessCount + kvp.Value.FailureCount;
                var rate = total == 0 ? 1 : (double)kvp.Value.SuccessCount / total;

                return new ProviderScore
                {
                    Provider = kvp.Key,
                    SuccessRate = rate
                };
            }).OrderByDescending(x => x.SuccessRate).ToList();
        }
    }
}