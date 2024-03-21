namespace FloralHub.Arch.Api.Extensions;

/// <summary>
/// Методы расширения для <see cref="IServiceCollection"/>
/// </summary>
internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddMiddlewares(this IServiceCollection services) =>
        services.AddScoped<ExceptionHandlingMiddleware>();

    internal static IServiceCollection Decorate<TInterface, TDecorator>(this IServiceCollection services)
        where TInterface : class
        where TDecorator : class, TInterface
    {
        Type interfaceType = typeof(TInterface);

        List<ServiceDescriptor> wrappedDescriptors = services
            .Where(t => t.ServiceType == interfaceType)
            .ToList();

        if (!wrappedDescriptors.Any())
        {
            throw new NotImplementedException();
        }

        foreach (ServiceDescriptor wrappedDescriptor in wrappedDescriptors)
        {
            ObjectFactory objectFactory = ActivatorUtilities.CreateFactory(
                typeof(TDecorator),
                new[] { interfaceType });

            services.Replace(ServiceDescriptor.Describe(
                interfaceType,
                serviceProvider =>
                    objectFactory(serviceProvider, new[] { serviceProvider.CreateInstance(wrappedDescriptor) }),
                wrappedDescriptor.Lifetime));
        }

        return services;
    }

    private static object CreateInstance(this IServiceProvider serviceProvider, ServiceDescriptor descriptor)
    {
        if (descriptor.ImplementationInstance is not null)
        {
            return descriptor.ImplementationInstance;
        }

        if (descriptor.ImplementationFactory is not null)
        {
            return descriptor.ImplementationFactory(serviceProvider);
        }

        if (descriptor.ImplementationType is null)
        {
            throw new NotImplementedException();
        }

        return ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, descriptor.ImplementationType);
    }
}
