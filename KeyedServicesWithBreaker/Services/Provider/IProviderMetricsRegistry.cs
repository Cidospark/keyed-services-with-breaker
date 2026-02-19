using KeyedServicesWithBreaker.Models.Entities;

namespace KeyedServicesWithBreaker.Services.Provider
{
    public interface IProviderMetricsRegistry
    {
        ProviderMetricsModel GetMetrics(string providerName);
        IReadOnlyList<ProviderScore> GetScores();
    }
}