using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyedServicesWithBreaker.Models.Entities
{
    public class ProviderScore
    {
        public string Provider { get; init; } = default!;
        public double SuccessRate { get; init; }
    }
}