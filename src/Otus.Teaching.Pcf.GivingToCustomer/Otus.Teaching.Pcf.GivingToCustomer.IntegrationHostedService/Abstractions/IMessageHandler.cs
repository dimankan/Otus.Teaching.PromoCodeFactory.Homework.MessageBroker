namespace Otus.Teaching.Pcf.GivingToCustomer.IntegrationHostedService.Abstractions
{
    public interface IMessageHandler<T>
    {
        Task HandleAsync(T message);
    }
}
