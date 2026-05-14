using Abdul.Brevo.Abstractions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Abdul.Brevo.Conversations;

/// <summary>
/// Extension methods for registering the Brevo Conversations SDK with dependency injection.
/// </summary>
public static class BrevoConversationsServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Brevo Conversations SDK services, binding options from the
    /// <c>Brevo</c> configuration section.
    /// </summary>
    public static IServiceCollection AddBrevoConversations(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddBrevoHttpClient<BrevoConversationsOptions, BrevoConversationsHttpClient>(
            configuration,
            BrevoConversationsOptions.SectionName);

        return services.AddBrevoConversationsClients();
    }

    /// <summary>
    /// Adds the Brevo Conversations SDK services with manual options configuration.
    /// </summary>
    public static IServiceCollection AddBrevoConversations(
        this IServiceCollection services,
        Action<BrevoConversationsOptions> configure)
    {
        services.AddBrevoHttpClient<BrevoConversationsOptions, BrevoConversationsHttpClient>(
            configure);

        return services.AddBrevoConversationsClients();
    }

    private static IServiceCollection AddBrevoConversationsClients(
        this IServiceCollection services)
    {
        services.AddScoped<IBrevoConversationMessagesClient, BrevoConversationMessagesClient>();
        services.AddScoped<IBrevoAutomatedMessagesClient, BrevoAutomatedMessagesClient>();
        services.AddScoped<IBrevoConversationStatusClient, BrevoConversationStatusClient>();
        services.AddScoped<IBrevoConversationVisitorsClient, BrevoConversationVisitorsClient>();

        return services;
    }
}
