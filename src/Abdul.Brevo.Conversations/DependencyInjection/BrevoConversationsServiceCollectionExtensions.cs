using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Abdul.Brevo.Conversations;

public static class BrevoConversationsServiceCollectionExtensions
{
    public static IServiceCollection AddBrevoConversations(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddOptions<BrevoConversationsOptions>()
            .Bind(configuration.GetSection(BrevoConversationsOptions.SectionName))
            .Validate(options => !string.IsNullOrWhiteSpace(options.ApiKey),
                "Brevo Conversations API key is required.")
            .ValidateOnStart();

        return services.AddBrevoConversationsCore();
    }

    public static IServiceCollection AddBrevoConversations(
        this IServiceCollection services,
        Action<BrevoConversationsOptions> configure)
    {
        services
            .AddOptions<BrevoConversationsOptions>()
            .Configure(configure)
            .Validate(options => !string.IsNullOrWhiteSpace(options.ApiKey),
                "Brevo Conversations API key is required.")
            .ValidateOnStart();

        return services.AddBrevoConversationsCore();
    }

    private static IServiceCollection AddBrevoConversationsCore(
        this IServiceCollection services)
    {
        services.AddHttpClient<BrevoConversationsHttpClient>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<BrevoConversationsOptions>>().Value;

            client.BaseAddress = new Uri(options.BaseUrl);
            client.Timeout = options.Timeout;
        });

        services.AddScoped<IBrevoConversationMessagesClient, BrevoConversationMessagesClient>();
        services.AddScoped<IBrevoAutomatedMessagesClient, BrevoAutomatedMessagesClient>();
        services.AddScoped<IBrevoConversationStatusClient, BrevoConversationStatusClient>();
        services.AddScoped<IBrevoConversationVisitorsClient, BrevoConversationVisitorsClient>();

        return services;
    }
}
