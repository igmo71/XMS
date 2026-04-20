using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using XMS.Application.Abstractions.Integration.OneC;

namespace XMS.Application.Integration.OneC.Common;

public abstract class ODataClient<TConfig>(HttpClient httpClient, IOptions<TConfig> options, ILogger<ODataClient<TConfig>> logger)
    where TConfig : ODataClientConfig
{
    protected readonly TConfig _clientConfig = options.Value;
    protected readonly HttpClient _httpClient = httpClient;
    protected readonly JsonSerializerOptions _serializerOptions = new()
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        //Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };

    public Task<TValue?> GetValueFromJsonAsync<TValue>(string? uri, CancellationToken ct = default)
    {
        return _httpClient.GetFromJsonAsync<TValue>($"{_clientConfig.ServiceUri}/{uri}", ct);
    }

    public async Task<TValue?> GetValueAsync<TValue>(string? uri, CancellationToken ct = default)
    {
        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Start {Uri}", nameof(GetValueAsync), uri);

        using var response = await _httpClient.GetAsync($"{_clientConfig.ServiceUri}/{uri}", ct);

        var content = await response.Content.ReadAsStringAsync(ct);

        if (!response.IsSuccessStatusCode)
        {
            var error = JsonSerializer.Deserialize<OneCError>(content);
            logger.LogError("{Source} {Uri} {@Error} {content}", nameof(GetValueAsync), uri, error, content);
            return default;
        }

        var result = JsonSerializer.Deserialize<TValue>(content);

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Ok {Uri} {@Result}", nameof(GetValueAsync), uri, result);

        return result;
    }

    public async Task<TValue?> PostValueAsync<TValue>(TValue value, string? uri, CancellationToken ct = default)
    {
        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Start {Uri} {@Value}", nameof(PostValueAsync), uri, value);

        var jsonString = JsonSerializer.Serialize(value, _serializerOptions);

        using var stringContent = new StringContent(jsonString, Encoding.UTF8, MediaTypeNames.Application.Json);

        using var response = await _httpClient.PostAsync($"{_clientConfig.ServiceUri}/{uri}", stringContent, ct);

        var content = await response.Content.ReadAsStringAsync(ct);

        if (!response.IsSuccessStatusCode)
        {
            var error = JsonSerializer.Deserialize<OneCError>(content);
            logger.LogError("{Source} {Uri} {JsonString} {@Value} {@Error}", nameof(PostValueAsync), uri, jsonString, value, error);
            return default;
        }

        var result = JsonSerializer.Deserialize<TValue>(content);

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Ok {Uri} {JsonString} {@Value} {@Result}", nameof(PostValueAsync), uri, jsonString, value, result);

        return result;
    }

    public async Task<TValue?> PatchValueAsync<TValue>(TValue value, string? uri, CancellationToken ct = default) where TValue : class, IOdataEntity
    {
        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Start {Uri} {@Value}", nameof(PatchValueAsync), uri, value);

        var jsonString = JsonSerializer.Serialize(value, _serializerOptions);

        using var stringContent = new StringContent(jsonString, Encoding.UTF8, MediaTypeNames.Application.Json);

        using var response = await _httpClient.PatchAsync($"{_clientConfig.ServiceUri}/{uri}(guid'{value.Ref_Key}')", stringContent, ct);

        var content = await response.Content.ReadAsStringAsync(ct);

        if (!response.IsSuccessStatusCode)
        {
            var error = JsonSerializer.Deserialize<OneCError>(content);
            logger.LogError("{Source} {Uri} {JsonString} {@Value} {@Error}", nameof(PatchValueAsync), uri, jsonString, value, error);
            return default;
        }

        var result = JsonSerializer.Deserialize<TValue>(content);

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Ok {Uri} {JsonString} {@Value} {@Result}", nameof(PatchValueAsync), uri, jsonString, value, result);

        return result;
    }

    public async Task<T?> FetchByRefKeyAsync<T>(Guid refKey, CancellationToken ct) where T : class, ISelectable
    {
        var uri = IntegrationHelper.GetUriByRefKey<T>(refKey);

        var rootObject = await GetValueAsync<RootObject<T>>(uri, ct);

        var result = rootObject?.Value?.FirstOrDefault();

        return result;
    }
}
