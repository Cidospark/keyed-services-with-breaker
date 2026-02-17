using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyedServicesWithBreaker.Models
{
    public class PaymentProviderOptions
    {
        public string[] PriorityOrder { get; set; } = [];
    }
}