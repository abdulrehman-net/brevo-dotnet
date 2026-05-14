using Abdul.Brevo.Abstractions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Abdul.Brevo.Email;

/// <summary>
/// Extension methods for registering the Brevo Email SDK with dependency injection.
/// </summary>
public static class BrevoEmailServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Brevo Email SDK services, binding options from the <c>Brevo</c> configuration section.
    /// </summary>
    public static IServiceCollection AddBrevoEmail(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddBrevoHttpClient<BrevoEmailOptions, BrevoEmailHttpClient>(
            configuration,
            BrevoEmailOptions.SectionName);

        return services.AddBrevoEmailClients();
    }

    /// <summary>
    /// Adds the Brevo Email SDK services with manual options configuration.
    /// </summary>
    public static IServiceCollection AddBrevoEmail(
        this IServiceCollection services,
        Action<BrevoEmailOptions> configure)
    {
        services.AddBrevoHttpClient<BrevoEmailOptions, BrevoEmailHttpClient>(configure);

        return services.AddBrevoEmailClients();
    }

    private static IServiceCollection AddBrevoEmailClients(
        this IServiceCollection services)
    {
        services.AddScoped<IBrevoTransactionalEmailClient, BrevoTransactionalEmailClient>();
        services.AddScoped<IBrevoScheduledEmailClient, BrevoScheduledEmailClient>();
        services.AddScoped<IBrevoHardBounceClient, BrevoHardBounceClient>();

        return services;
    }
}
