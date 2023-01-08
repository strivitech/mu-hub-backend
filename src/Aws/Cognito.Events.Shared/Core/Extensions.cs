namespace Cognito.Events.Shared.Core;

internal static class Extensions
{
    public static bool AnyBaseType(this Type type, Func<Type, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.BaseTypes().Any(predicate);
    }

    public static bool IsParticularGeneric(this Type type, Type generic)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type.IsGenericType && type.GetGenericTypeDefinition() == generic;
    }
    
    private static IEnumerable<Type> BaseTypes(this Type type)
    {
        var t = type;
        while (true)
        {
            t = t.BaseType;
            if (t == null) break;
            yield return t;
        }
    }
}
