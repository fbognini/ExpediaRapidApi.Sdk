namespace ExpediaRapidApi.Sdk.Shared;

/// <summary>
/// Options carrying the Rapid <c>Test</c> header, which makes the test environment return a canned scenario
/// (<c>standard</c>, <c>sold_out</c>, <c>price_mismatch</c>, <c>service_unavailable</c>, ...) instead of a live
/// response. Ignored in production.
/// </summary>
internal interface IHasTestHeaderOptions
{
    string? Test { get; }
}
