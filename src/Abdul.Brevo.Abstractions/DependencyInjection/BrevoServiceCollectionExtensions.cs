using Abdul.Brevo.Abstractions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Abdul.Brevo.Abstractions.DependencyInjection;

/// <summary>
/// Shared extension methods that wire up Brevo options + typed HttpClient
/// for any SDK module, eliminating the duplicated DI registration boilerplate.
/// </summary>
public static class BrevoServiceCollectionExtensions
{
    /// <summary>
    /// Registers <typeparamref name="TOptions"/> from the given configuration section
    /// and a typed <see cref="HttpClient"/> for <typeparamref name="THttpClient"/>.
    /// </summary>
    /// <returns>
    /// An <see cref="IHttpClientBuilder"/> so callers can chain Polly resilience
    /// policies, additional handlers, etc.
    /// </returns>
    public static IHttpClientBuilder AddBrevoHttpClient<TOptions, THttpClient>(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName)
        where TOptions : BrevoOptionsBase, new()
        where THttpClient : BrevoHttpClientBase
    {
        services
            .AddOptions<TOptions>()
            .Bind(configuration.GetSection(sectionName))
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.ApiKey),
                $"Brevo API key is required (section: {sectionName}).")
            .ValidateOnStart();

        return services.AddBrevoHttpClientCore<TOptions, THttpClient>();
    }

    /// <summary>
    /// Registers <typeparamref name="TOptions"/> via an <see cref="Action{T}"/>
    /// and a typed <see cref="HttpClient"/> for <typeparamref name="THttpClient"/>.
    /// </summary>
    public static IHttpClientBuilder AddBrevoHttpClient<TOptions, THttpClient>(
        this IServiceCollection services,
        Action<TOptions> configure)
        where TOptions : BrevoOptionsBase, new()
        where THttpClient : BrevoHttpClientBase
    {
        services
            .AddOptions<TOptions>()
            .Configure(configure)
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.ApiKey),
                "Brevo API key is required.")
            .ValidateOnStart();

        return services.AddBrevoHttpClientCore<TOptions, THttpClient>();
    }

    private static IHttpClientBuilder AddBrevoHttpClientCore<TOptions, THttpClient>(
        this IServiceCollection services)
        where TOptions : BrevoOptionsBase, new()
        where THttpClient : BrevoHttpClientBase
    {
        return services.AddHttpClient<THttpClient>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<TOptions>>().Value;

            client.BaseAddress = new Uri(options.BaseUrl);
            client.Timeout = options.Timeout;
        });
    }
}
