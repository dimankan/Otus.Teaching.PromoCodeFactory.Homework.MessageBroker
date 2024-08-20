namespace Otus.Teaching.Pcf.Administration.IntegrationHostedService.Abstractions
{
    public interface IMessageHandler<T>
    {
        Task HandleAsync(T message);
    }
}
