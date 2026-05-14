using Abdul.Brevo.Abstractions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Abdul.Brevo.Core;

/// <summary>
/// Extension methods for registering the Brevo Core SDK with dependency injection.
/// </summary>
public static class BrevoCoreServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Brevo Core SDK services, binding options from the <c>Brevo</c> configuration section.
    /// </summary>
    public static IServiceCollection AddBrevoCore(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddBrevoHttpClient<BrevoCoreOptions, BrevoCoreHttpClient>(
            configuration,
            BrevoCoreOptions.SectionName);

        return services.AddBrevoCoreClients();
    }

    /// <summary>
    /// Adds the Brevo Core SDK services with manual options configuration.
    /// </summary>
    public static IServiceCollection AddBrevoCore(
        this IServiceCollection services,
        Action<BrevoCoreOptions> configure)
    {
        services.AddBrevoHttpClient<BrevoCoreOptions, BrevoCoreHttpClient>(configure);

        return services.AddBrevoCoreClients();
    }

    private static IServiceCollection AddBrevoCoreClients(
        this IServiceCollection services)
    {
        services.AddScoped<IBrevoAccountClient, BrevoAccountClient>();
        services.AddScoped<IBrevoContactsClient, BrevoContactsClient>();
        services.AddScoped<IBrevoFoldersClient, BrevoFoldersClient>();
        services.AddScoped<IBrevoListsClient, BrevoListsClient>();
        services.AddScoped<IBrevoWebhooksClient, BrevoWebhooksClient>();

        return services;
    }
}
