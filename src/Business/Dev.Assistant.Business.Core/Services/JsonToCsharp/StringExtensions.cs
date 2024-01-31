namespace Dev.Assistant.Business.Core.Services.JsonToCsharp;

internal static class StringExtensions
{
    internal static string SnakeToUpperCamel(this string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        return input
            .Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => char.ToUpperInvariant(s[0]) + s[1..])
            .Aggregate(string.Empty, (s1, s2) => s1 + s2)
        ;
    }
}