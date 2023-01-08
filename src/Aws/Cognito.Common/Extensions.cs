namespace Cognito.Common;

public static class Extensions
{
    public static bool IsCognitoApplicationLinked(this string? applicationLinkedValue)
    {
        var isConverted = bool.TryParse(applicationLinkedValue, out var isLinked);
        return isConverted && isLinked;
    }
}
