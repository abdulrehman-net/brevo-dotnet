using Abdul.Brevo.Abstractions.RateLimiting;
using FluentAssertions;
using Xunit;

namespace Abdul.Brevo.Abstractions.Tests;

public class BrevoRateLimitInfoTests
{
    [Fact]
    public void FromResponse_ShouldParseHeaders_WhenPresent()
    {
        // Arrange
        var response = new HttpResponseMessage();
        response.Headers.Add("x-sib-ratelimit-limit", "100");
        response.Headers.Add("x-sib-ratelimit-remaining", "95");
        response.Headers.Add("x-sib-ratelimit-reset", "10");

        // Act
        var info = BrevoRateLimitInfo.FromResponse(response);

        // Assert
        info.Should().NotBeNull();
        info!.Limit.Should().Be(100);
        info.Remaining.Should().Be(95);
        info.ResetSeconds.Should().Be(10);
    }

    [Fact]
    public void FromResponse_ShouldReturnNull_WhenHeadersMissing()
    {
        // Arrange
        var response = new HttpResponseMessage();

        // Act
        var info = BrevoRateLimitInfo.FromResponse(response);

        // Assert
        info.Should().BeNull();
    }
}
