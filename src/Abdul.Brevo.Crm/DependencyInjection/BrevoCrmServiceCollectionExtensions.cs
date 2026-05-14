using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Abdul.Brevo.Crm;

/// <summary>
/// Extension methods for registering the Brevo CRM SDK with dependency injection.
/// </summary>
public static class BrevoCrmServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Brevo CRM SDK services, binding options from the <c>BrevoCrm</c> configuration section.
    /// </summary>
    public static IServiceCollection AddBrevoCrm(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddOptions<BrevoCrmOptions>()
            .Bind(configuration.GetSection(BrevoCrmOptions.SectionName))
            .Validate(options => !string.IsNullOrWhiteSpace(options.ApiKey),
                "Brevo CRM API key is required.")
            .ValidateOnStart();

        return services.AddBrevoCrmCore();
    }

    /// <summary>
    /// Adds the Brevo CRM SDK services with manual options configuration.
    /// </summary>
    public static IServiceCollection AddBrevoCrm(
        this IServiceCollection services,
        Action<BrevoCrmOptions> configure)
    {
        services
            .AddOptions<BrevoCrmOptions>()
            .Configure(configure)
            .Validate(options => !string.IsNullOrWhiteSpace(options.ApiKey),
                "Brevo CRM API key is required.")
            .ValidateOnStart();

        return services.AddBrevoCrmCore();
    }

    private static IServiceCollection AddBrevoCrmCore(
        this IServiceCollection services)
    {
        services.AddHttpClient<BrevoCrmHttpClient>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<BrevoCrmOptions>>().Value;

            client.BaseAddress = new Uri(options.BaseUrl);
            client.Timeout = options.Timeout;
        });

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
