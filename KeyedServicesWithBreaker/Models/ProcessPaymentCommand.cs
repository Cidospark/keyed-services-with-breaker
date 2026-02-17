using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyedServicesWithBreaker.Models
{
    public record ProcessPaymentCommand(decimal Amount);
}