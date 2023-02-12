using System.ComponentModel.DataAnnotations;

namespace CachingExample.ApplicationCore.Services;

/// <summary>
/// Options POCO for configuring the cacghe
/// </summary>
public class CacheOptions
{
    /// <summary>
    /// Length of time items stay alive in cache
    /// </summary>
    [Required]
    public TimeSpan ExpiryTime { get; init; }
}