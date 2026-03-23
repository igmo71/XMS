using MassTransit;

namespace XMS.Integration.OneC.Abstractions
{
    public interface IOneCConsumer<TEvent> : IConsumer<TEvent> where TEvent : class
    {
    }
}
