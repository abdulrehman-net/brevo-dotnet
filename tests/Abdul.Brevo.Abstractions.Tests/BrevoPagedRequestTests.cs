using Abdul.Brevo.Abstractions.Pagination;
using FluentAssertions;
using Xunit;

namespace Abdul.Brevo.Abstractions.Tests;

public class BrevoPagedRequestTests
{
    [Fact]
    public void AppendTo_ShouldAddQueryParams_WhenPathHasNoQuery()
    {
        // Arrange
        var request = new BrevoPagedRequest { Limit = 10, Offset = 20 };
        var path = "companies";

        // Act
        var result = request.AppendTo(path);

        // Assert
        result.Should().Be("companies?limit=10&offset=20");
    }

    [Fact]
    public void AppendTo_ShouldAddQueryParams_WhenPathHasExistingQuery()
    {
        // Arrange
        var request = new BrevoPagedRequest { Limit = 5, Offset = 0 };
        var path = "deals?sort=desc";

        // Act
        var result = request.AppendTo(path);

        // Assert
        result.Should().Be("deals?sort=desc&limit=5&offset=0");
    }
}
