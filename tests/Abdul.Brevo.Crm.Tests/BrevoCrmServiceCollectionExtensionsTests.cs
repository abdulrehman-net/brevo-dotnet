using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Abdul.Brevo.Crm.Tests;

public sealed class BrevoCrmServiceCollectionExtensionsTests
{
    [Fact]
    public void AddBrevoCrm_RegistersAllServices()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["BrevoCrm:ApiKey"] = "test-key"
            })
            .Build();

        var services = new ServiceCollection();
        services.AddBrevoCrm(configuration);

        var provider = services.BuildServiceProvider();

        provider.GetService<IBrevoCrmCompaniesClient>().Should().NotBeNull();
        provider.GetService<IBrevoCrmDealsClient>().Should().NotBeNull();
        provider.GetService<IBrevoCrmPipelineClient>().Should().NotBeNull();
        provider.GetService<IBrevoCrmTasksClient>().Should().NotBeNull();
        provider.GetService<IBrevoCrmNotesClient>().Should().NotBeNull();
        provider.GetService<IBrevoCrmFilesClient>().Should().NotBeNull();
        provider.GetService<IBrevoCrmAttributesClient>().Should().NotBeNull();
    }
}
