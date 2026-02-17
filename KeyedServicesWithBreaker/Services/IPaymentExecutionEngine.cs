namespace KeyedServicesWithBreaker.Services
{
    public interface IPaymentExecutionEngine
    {
        Task ExecuteAsync(IPaymentService provider, decimal amount);
    }
}