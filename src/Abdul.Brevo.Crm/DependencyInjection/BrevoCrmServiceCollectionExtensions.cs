using Abdul.Brevo.Abstractions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Abdul.Brevo.Crm;

/// <summary>
/// Extension methods for registering the Brevo CRM SDK with dependency injection.
/// </summary>
public static class BrevoCrmServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Brevo CRM SDK services, binding options from the <c>Brevo</c> configuration section.
    /// </summary>
    public static IServiceCollection AddBrevoCrm(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddBrevoHttpClient<BrevoCrmOptions, BrevoCrmHttpClient>(
            configuration,
            BrevoCrmOptions.SectionName);

        return services.AddBrevoCrmClients();
    }

    /// <summary>
    /// Adds the Brevo CRM SDK services with manual options configuration.
    /// </summary>
    public static IServiceCollection AddBrevoCrm(
        this IServiceCollection services,
        Action<BrevoCrmOptions> configure)
    {
        services.AddBrevoHttpClient<BrevoCrmOptions, BrevoCrmHttpClient>(configure);

        return services.AddBrevoCrmClients();
    }

    private static IServiceCollection AddBrevoCrmClients(
        this IServiceCollection services)
    {
        services.AddScoped<IBrevoCrmCompaniesClient, BrevoCrmCompaniesClient>();
        services.AddScoped<IBrevoCrmDealsClient, BrevoCrmDealsClient>();
        services.AddScoped<IBrevoCrmPipelineClient, BrevoCrmPipelineClient>();
        services.AddScoped<IBrevoCrmTasksClient, BrevoCrmTasksClient>();
        services.AddScoped<IBrevoCrmNotesClient, BrevoCrmNotesClient>();
        services.AddScoped<IBrevoCrmFilesClient, BrevoCrmFilesClient>();
        services.AddScoped<IBrevoCrmAttributesClient, BrevoCrmAttributesClient>();

        return services;
    }
}
