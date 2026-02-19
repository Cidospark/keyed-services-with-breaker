using KeyedServicesWithBreaker.Models;
using KeyedServicesWithBreaker.Services;
using Microsoft.AspNetCore.Mvc;

namespace KeyedServicesWithBreaker.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly ProcessPaymentCommandHandler _handler;

        public PaymentController(ILogger<PaymentController> logger, ProcessPaymentCommandHandler handler)
        {
            _logger = logger;
            _handler = handler;
        }

        [HttpPost]   
        public async Task<IActionResult> Post(decimal amount)
        {
            await _handler.HandleAsync(new ProcessPaymentCommand(amount));
            return Ok("Done!");
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}