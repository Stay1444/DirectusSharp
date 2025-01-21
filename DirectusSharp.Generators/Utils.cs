namespace DirectusSharp.Generators;

internal static class Utils
{
    public static string ToCamelCase(this string s)
    {
        return char.ToLowerInvariant(s[0]) + s.Substring(1);
    }
}