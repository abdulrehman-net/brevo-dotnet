using System.Text.Json;
using System.Text.Json.Serialization;

namespace Abdul.Brevo.Abstractions.Http;

/// <summary>
/// Shared <see cref="JsonSerializerOptions"/> used by all Brevo SDK HTTP clients.
/// </summary>
public static class BrevoJsonDefaults
{
    /// <summary>
    /// Default JSON serialization options: camelCase naming with null properties omitted.
    /// </summary>
    public static JsonSerializerOptions Options { get; } = new(JsonSerializerDefaults.Web)
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
}
