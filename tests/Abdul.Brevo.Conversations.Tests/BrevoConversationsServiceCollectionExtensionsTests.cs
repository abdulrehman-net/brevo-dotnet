using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Abdul.Brevo.Conversations.Tests;

public class BrevoConversationsServiceCollectionExtensionsTests
{
    [Fact]
    public void AddBrevoConversations_WithConfiguration_RegistersServices()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "Brevo:ApiKey", "test-key" },
                { "Brevo:BaseUrl", "https://api.test.com" }
            })
            .Build();

        // Act
        services.AddBrevoConversations(configuration);
        var provider = services.BuildServiceProvider();

        // Assert
        var options = provider.GetRequiredService<IOptions<BrevoConversationsOptions>>().Value;
        options.ApiKey.Should().Be("test-key");
        options.BaseUrl.Should().Be("https://api.test.com");

        provider.GetRequiredService<IBrevoConversationMessagesClient>().Should().NotBeNull();
        provider.GetRequiredService<IBrevoAutomatedMessagesClient>().Should().NotBeNull();
        provider.GetRequiredService<IBrevoConversationStatusClient>().Should().NotBeNull();
        provider.GetRequiredService<IBrevoConversationVisitorsClient>().Should().NotBeNull();
    }

    [Fact]
    public void AddBrevoConversations_WithAction_RegistersServices()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddBrevoConversations(options =>
        {
            options.ApiKey = "test-key-2";
            options.BaseUrl = "https://api2.test.com";
        });
        var provider = services.BuildServiceProvider();

        // Assert
        var options = provider.GetRequiredService<IOptions<BrevoConversationsOptions>>().Value;
        options.ApiKey.Should().Be("test-key-2");
        options.BaseUrl.Should().Be("https://api2.test.com");

        provider.GetRequiredService<IBrevoConversationMessagesClient>().Should().NotBeNull();
        provider.GetRequiredService<IBrevoAutomatedMessagesClient>().Should().NotBeNull();
        provider.GetRequiredService<IBrevoConversationStatusClient>().Should().NotBeNull();
        provider.GetRequiredService<IBrevoConversationVisitorsClient>().Should().NotBeNull();
    }

    [Fact]
    public void AddBrevoConversations_MissingApiKey_ThrowsOptionsValidationException()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddBrevoConversations(options =>
        {
            options.ApiKey = "";
        });
        var provider = services.BuildServiceProvider();

        // Assert
        var act = () => provider.GetRequiredService<IBrevoConversationMessagesClient>();
        act.Should().Throw<OptionsValidationException>();
    }
}
