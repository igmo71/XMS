using Microsoft.Extensions.DependencyInjection;
using XMS.Common.SharedKernel.Abstractions;

namespace XMS.Common.SharedKernel;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRequestHandler<TRequest, TResult, TRequestHandler>(this IServiceCollection services)
        where TRequest : IRequest<TResult>
        where TResult : IResult
        where TRequestHandler : class, IRequestHandler<TRequest, TResult>
    {
        services.AddScoped<IRequestHandler<TRequest, TResult>, TRequestHandler>();

        return services;
    }

    public static IServiceCollection AddRequestProcessor<TRequest, TResult, TRequestProcessor>(this IServiceCollection services)
        where TRequest : IRequest<TResult>
        where TResult : IResult
        where TRequestProcessor : class, IRequestProcessor<TRequest, TResult>
    {
        services.AddScoped<IRequestProcessor<TRequest, TResult>, TRequestProcessor>();

        return services;
    }

    public static IServiceCollection AddEventHandler<TEvent, TEventHandler>(this IServiceCollection services)
        where TEvent : IEvent
        where TEventHandler : class, IEventHandler<TEvent>
    {
        services.AddScoped<IEventHandler<TEvent>, TEventHandler>();

        return services;
    }
}
