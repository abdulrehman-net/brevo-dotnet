using System.Text.Json.Serialization;
using Abdul.Brevo.Abstractions.Pagination;

namespace Abdul.Brevo.Core.Models;

/// <summary>
/// Represents a contact list in Brevo.
/// </summary>
public sealed class BrevoList
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("totalBlacklisted")]
    public long TotalBlacklisted { get; init; }

    [JsonPropertyName("totalSubscribers")]
    public long TotalSubscribers { get; init; }

    [JsonPropertyName("uniqueSubscribers")]
    public long UniqueSubscribers { get; init; }
    
    [JsonPropertyName("folderId")]
    public long FolderId { get; init; }
}

public sealed class GetListsRequest : BrevoPagedRequest
{
    /// <summary>
    /// Sort the results in the ascending/descending order of record creation. Default is desc.
    /// </summary>
    public string? Sort { get; set; }

    public new string AppendTo(string path)
    {
        var url = base.AppendTo(path);
        
        if (!string.IsNullOrWhiteSpace(Sort))
        {
            url += $"&sort={Sort.ToLowerInvariant()}";
        }

        return url;
    }
}

public sealed class CreateListRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("folderId")]
    public long FolderId { get; set; }
}

public sealed class UpdateListRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("folderId")]
    public long? FolderId { get; set; }
}

public sealed class ModifyListContactsRequest
{
    [JsonPropertyName("emails")]
    public IReadOnlyList<string>? Emails { get; set; }

    [JsonPropertyName("ids")]
    public IReadOnlyList<long>? Ids { get; set; }
}
