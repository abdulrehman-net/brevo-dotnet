using System.Text.Json.Serialization;
using Abdul.Brevo.Abstractions.Pagination;

namespace Abdul.Brevo.Core.Models;

/// <summary>
/// Represents a contact in Brevo.
/// </summary>
public sealed class BrevoContact
{
    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("emailBlacklisted")]
    public bool EmailBlacklisted { get; init; }

    [JsonPropertyName("smsBlacklisted")]
    public bool SmsBlacklisted { get; init; }

    [JsonPropertyName("createdAt")]
    public DateTimeOffset CreatedAt { get; init; }

    [JsonPropertyName("modifiedAt")]
    public DateTimeOffset ModifiedAt { get; init; }

    [JsonPropertyName("listIds")]
    public IReadOnlyList<long>? ListIds { get; init; }

    [JsonPropertyName("attributes")]
    public Dictionary<string, object>? Attributes { get; init; }
}

public sealed class GetContactsRequest : BrevoPagedRequest
{
    /// <summary>
    /// Filter contacts by modified status. Example: 'modifiedSince=2020-09-20T19:20:30+01:00'
    /// </summary>
    public DateTimeOffset? ModifiedSince { get; set; }

    /// <summary>
    /// Sort the results in the ascending/descending order of record creation. Default is desc.
    /// </summary>
    public string? Sort { get; set; }

    public new string AppendTo(string path)
    {
        var url = base.AppendTo(path);
        
        if (ModifiedSince.HasValue)
        {
            var formattedDate = Uri.EscapeDataString(ModifiedSince.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            url += $"&modifiedSince={formattedDate}";
        }

        if (!string.IsNullOrWhiteSpace(Sort))
        {
            url += $"&sort={Sort.ToLowerInvariant()}";
        }

        return url;
    }
}

public sealed class CreateContactRequest
{
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("attributes")]
    public Dictionary<string, object>? Attributes { get; set; }

    [JsonPropertyName("emailBlacklisted")]
    public bool? EmailBlacklisted { get; set; }

    [JsonPropertyName("smsBlacklisted")]
    public bool? SmsBlacklisted { get; set; }

    [JsonPropertyName("listIds")]
    public IReadOnlyList<long>? ListIds { get; set; }

    [JsonPropertyName("updateEnabled")]
    public bool? UpdateEnabled { get; set; }

    [JsonPropertyName("smtpBlacklistSender")]
    public IReadOnlyList<string>? SmtpBlacklistSender { get; set; }
}

public sealed class UpdateContactRequest
{
    [JsonPropertyName("attributes")]
    public Dictionary<string, object>? Attributes { get; set; }

    [JsonPropertyName("emailBlacklisted")]
    public bool? EmailBlacklisted { get; set; }

    [JsonPropertyName("smsBlacklisted")]
    public bool? SmsBlacklisted { get; set; }

    [JsonPropertyName("listIds")]
    public IReadOnlyList<long>? ListIds { get; set; }

    [JsonPropertyName("unlinkListIds")]
    public IReadOnlyList<long>? UnlinkListIds { get; set; }

    [JsonPropertyName("smtpBlacklistSender")]
    public IReadOnlyList<string>? SmtpBlacklistSender { get; set; }
}

public sealed class CreateDoubleOptInContactRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("attributes")]
    public Dictionary<string, object>? Attributes { get; set; }

    [JsonPropertyName("includeListIds")]
    public IReadOnlyList<long> IncludeListIds { get; set; } = [];

    [JsonPropertyName("excludeListIds")]
    public IReadOnlyList<long>? ExcludeListIds { get; set; }

    [JsonPropertyName("templateId")]
    public long TemplateId { get; set; }

    [JsonPropertyName("redirectionUrl")]
    public string RedirectionUrl { get; set; } = string.Empty;
}
