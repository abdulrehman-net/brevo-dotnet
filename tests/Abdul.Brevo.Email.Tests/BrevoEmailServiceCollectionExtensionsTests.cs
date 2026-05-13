using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Abdul.Brevo.Email.Tests;

public sealed class BrevoEmailServiceCollectionExtensionsTests
{
    [Fact]
    public void AddBrevoEmail_WithConfiguration_RegistersAllServices()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["BrevoEmail:ApiKey"] = "test-key"
            })
            .Build();

        var services = new ServiceCollection();
        services.AddBrevoEmail(configuration);

        var provider = services.BuildServiceProvider();

        provider.GetService<IBrevoTransactionalEmailClient>().Should().NotBeNull();
        provider.GetService<IBrevoScheduledEmailClient>().Should().NotBeNull();
        provider.GetService<IBrevoHardBounceClient>().Should().NotBeNull();
    }

    [Fact]
    public void AddBrevoEmail_WithAction_RegistersAllServices()
    {
        var services = new ServiceCollection();
        services.AddBrevoEmail(options =>
        {
            options.ApiKey = "test-key";
        });

        var provider = services.BuildServiceProvider();

        provider.GetService<IBrevoTransactionalEmailClient>().Should().NotBeNull();
        provider.GetService<IBrevoScheduledEmailClient>().Should().NotBeNull();
        provider.GetService<IBrevoHardBounceClient>().Should().NotBeNull();
    }

    [Fact]
    public void AddBrevoEmail_WithConfiguration_BindsOptions()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["BrevoEmail:ApiKey"] = "my-api-key",
                ["BrevoEmail:BaseUrl"] = "https://custom.api.com"
            })
            .Build();

        var services = new ServiceCollection();
        services.AddBrevoEmail(configuration);

        var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<BrevoEmailOptions>>().Value;

        options.ApiKey.Should().Be("my-api-key");
        options.BaseUrl.Should().Be("https://custom.api.com");
    }

    [Fact]
    public void AddBrevoEmail_WithAction_ConfiguresOptions()
    {
        var services = new ServiceCollection();
        services.AddBrevoEmail(options =>
        {
            options.ApiKey = "action-key";
            options.Timeout = TimeSpan.FromSeconds(60);
        });

        var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<BrevoEmailOptions>>().Value;

        options.ApiKey.Should().Be("action-key");
        options.Timeout.Should().Be(TimeSpan.FromSeconds(60));
    }
}
