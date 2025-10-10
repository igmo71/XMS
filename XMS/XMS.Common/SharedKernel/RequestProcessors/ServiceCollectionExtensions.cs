using Microsoft.Extensions.DependencyInjection;
using XMS.Common.SharedKernel.Abstractions;

namespace XMS.Common.SharedKernel.RequestProcessors;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLoggingProcessor(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRequestProcessor<,>), typeof(LoggingProcessor<,>));

        return services;
    }

    public static IServiceCollection AddLValidationProcessor(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRequestProcessor<,>), typeof(ValidationProcessor<,>));

        return services;
    }

    public static IServiceCollection AddTransactionProcessor(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRequestProcessor<,>), typeof(TransactionProcessor<,>));

        return services;
    }
}
