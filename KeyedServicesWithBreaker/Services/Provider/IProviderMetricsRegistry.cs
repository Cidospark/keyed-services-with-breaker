using KeyedServicesWithBreaker.Models.Entities;

namespace KeyedServicesWithBreaker.Services.Provider
{
    public interface IProviderMetricsRegistry
    {
        ProviderMetricsModel Get(string providerName);
        IReadOnlyList<ProviderScore> GetProviderScores();
    }
}