using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Polly;

namespace KeyedServicesWithBreaker.Services.Provider
{
    public interface IProviderPolicyRegistry
    {
        IAsyncPolicy GetPolicy(string providerName);
    }
}