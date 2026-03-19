using MassTransit;

namespace XMS.Integration.OneC
{
    public interface IOneCConsumer<TEvent> : IConsumer<TEvent> where TEvent : class
    {
    }
}
