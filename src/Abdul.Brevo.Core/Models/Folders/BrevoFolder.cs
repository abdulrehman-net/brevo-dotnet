using System.Text.Json.Serialization;
using Abdul.Brevo.Abstractions.Pagination;

namespace Abdul.Brevo.Core.Models;

/// <summary>
/// Represents a folder in Brevo used to organize contact lists.
/// </summary>
public sealed class BrevoFolder
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
}

public sealed class GetFoldersRequest : BrevoPagedRequest
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

public sealed class CreateFolderRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

public sealed class UpdateFolderRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}
