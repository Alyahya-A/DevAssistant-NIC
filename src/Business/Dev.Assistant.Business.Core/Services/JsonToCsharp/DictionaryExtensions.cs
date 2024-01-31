namespace Dev.Assistant.Business.Core.Services.JsonToCsharp;

internal static class DictionaryExtensions
{
    public static void Deconstruct<TKey, TValue>(
        this KeyValuePair<TKey, TValue> keyValuePair, out TKey key, out TValue value)
        => (key, value) = (keyValuePair.Key, keyValuePair.Value);
}