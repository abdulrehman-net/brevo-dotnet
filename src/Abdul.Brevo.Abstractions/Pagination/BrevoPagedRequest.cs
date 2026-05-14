namespace Abdul.Brevo.Abstractions.Pagination;

/// <summary>
/// Standard Brevo pagination request parameters using <c>limit</c> and <c>offset</c>.
/// </summary>
public class BrevoPagedRequest
{
    /// <summary>
    /// Number of items to return per page. Defaults to 50.
    /// </summary>
    public int Limit { get; set; } = 50;

    /// <summary>
    /// Index of the first item to retrieve (zero-based). Defaults to 0.
    /// </summary>
    public int Offset { get; set; }

    /// <summary>
    /// Appends <c>?limit=X&amp;offset=Y</c> (or <c>&amp;limit=X&amp;offset=Y</c>)
    /// to the given path.
    /// </summary>
    public string AppendTo(string path)
    {
        var separator = path.Contains('?') ? '&' : '?';
        return $"{path}{separator}limit={Limit}&offset={Offset}";
    }
}
