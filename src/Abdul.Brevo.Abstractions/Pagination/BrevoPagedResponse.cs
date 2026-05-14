namespace Abdul.Brevo.Abstractions.Pagination;

/// <summary>
/// Standard Brevo paginated response wrapper.
/// </summary>
/// <typeparam name="T">The type of items in the paginated result.</typeparam>
public class BrevoPagedResponse<T>
{
    /// <summary>
    /// The items returned in this page.
    /// </summary>
    public IReadOnlyList<T> Items { get; init; } = [];

    /// <summary>
    /// The total number of items matching the query (across all pages).
    /// </summary>
    public int Count { get; init; }
}
