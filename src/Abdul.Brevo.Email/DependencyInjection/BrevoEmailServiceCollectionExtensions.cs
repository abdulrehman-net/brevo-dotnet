using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Abdul.Brevo.Email;

/// <summary>
/// Extension methods for registering the Brevo Email SDK with dependency injection.
/// </summary>
public static class BrevoEmailServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Brevo Email SDK services, binding options from the <c>BrevoEmail</c> configuration section.
    /// </summary>
    public static IServiceCollection AddBrevoEmail(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddOptions<BrevoEmailOptions>()
            .Bind(configuration.GetSection(BrevoEmailOptions.SectionName))
            .Validate(options => !string.IsNullOrWhiteSpace(options.ApiKey),
                "Brevo Email API key is required.")
            .ValidateOnStart();

        return services.AddBrevoEmailCore();
    }

    /// <summary>
    /// Adds the Brevo Email SDK services with manual options configuration.
    /// </summary>
    public static IServiceCollection AddBrevoEmail(
        this IServiceCollection services,
        Action<BrevoEmailOptions> configure)
    {
        services
            .AddOptions<BrevoEmailOptions>()
            .Configure(configure)
            .Validate(options => !string.IsNullOrWhiteSpace(options.ApiKey),
                "Brevo Email API key is required.")
            .ValidateOnStart();

        return services.AddBrevoEmailCore();
    }

    private static IServiceCollection AddBrevoEmailCore(
        this IServiceCollection services)
    {
        services.AddHttpClient<BrevoEmailHttpClient>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<BrevoEmailOptions>>().Value;

            client.BaseAddress = new Uri(options.BaseUrl);
            client.Timeout = options.Timeout;
        });

        services.AddScoped<IBrevoTransactionalEmailClient, BrevoTransactionalEmailClient>();
        services.AddScoped<IBrevoScheduledEmailClient, BrevoScheduledEmailClient>();
        services.AddScoped<IBrevoHardBounceClient, BrevoHardBounceClient>();

        return services;
    }
}
