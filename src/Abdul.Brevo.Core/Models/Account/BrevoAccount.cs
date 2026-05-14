using System.Text.Json.Serialization;

namespace Abdul.Brevo.Core.Models;

/// <summary>
/// Represents the account information returned by Brevo.
/// </summary>
public sealed class BrevoAccount
{
    /// <summary>
    /// Login email associated with the account.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; init; }

    /// <summary>
    /// Name of the company.
    /// </summary>
    [JsonPropertyName("companyName")]
    public string? CompanyName { get; init; }

    /// <summary>
    /// Address information for the account.
    /// </summary>
    [JsonPropertyName("address")]
    public BrevoAccountAddress? Address { get; init; }

    /// <summary>
    /// Information about the account's plan(s).
    /// </summary>
    [JsonPropertyName("plan")]
    public IReadOnlyList<BrevoAccountPlan>? Plan { get; init; }

    /// <summary>
    /// Information about the SMTP relay setup.
    /// </summary>
    [JsonPropertyName("relay")]
    public BrevoAccountRelay? Relay { get; init; }

    /// <summary>
    /// Status of the marketing automation feature.
    /// </summary>
    [JsonPropertyName("marketingAutomation")]
    public BrevoAccountMarketingAutomation? MarketingAutomation { get; init; }
}

public sealed class BrevoAccountAddress
{
    [JsonPropertyName("street")]
    public string? Street { get; init; }

    [JsonPropertyName("city")]
    public string? City { get; init; }

    [JsonPropertyName("zipCode")]
    public string? ZipCode { get; init; }

    [JsonPropertyName("country")]
    public string? Country { get; init; }
}

public sealed class BrevoAccountPlan
{
    /// <summary>
    /// Displays the plan type (free, lite, premium, enterprise).
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>
    /// This is the type of the credit: 'Send Limit' or 'Send'.
    /// </summary>
    [JsonPropertyName("creditsType")]
    public string? CreditsType { get; init; }

    /// <summary>
    /// Remaining credits of the user.
    /// </summary>
    [JsonPropertyName("credits")]
    public float? Credits { get; init; }

    [JsonPropertyName("startDate")]
    public DateTimeOffset? StartDate { get; init; }

    [JsonPropertyName("endDate")]
    public DateTimeOffset? EndDate { get; init; }

    [JsonPropertyName("userLimit")]
    public int? UserLimit { get; init; }
}

public sealed class BrevoAccountRelay
{
    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }

    [JsonPropertyName("port")]
    public int? Port { get; init; }
}

public sealed class BrevoAccountMarketingAutomation
{
    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }
}
