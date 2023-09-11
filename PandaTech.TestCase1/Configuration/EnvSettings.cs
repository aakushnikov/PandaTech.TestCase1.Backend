namespace PandaTech.TestCase1.Configuration;

using System;

public static class EnvSettings
{
    private static readonly Type[] SupportedTypes =
        { typeof(string), typeof(int), typeof(long), typeof(double), typeof(bool) };
    
    public static readonly string Namespace;


    static EnvSettings()
    {
        Namespace = typeof(EnvSettings).Namespace ?? string.Empty;
    }

    private static T InitializeEnvAssert<T>(this T? def, string name, T? defValue = default)
    {
        var val = def.InitializeEnv(name, defValue);

        if (val == null) throw new ArgumentException(name);
        
        if (SupportedTypes.All(t => t != typeof(T)))
            throw new ArgumentOutOfRangeException();

        return val;
    }

    private static T? InitializeEnv<T>(this T? def, string name, T? defValue = default)
    {
        var stringValue = Environment.GetEnvironmentVariable($"{Namespace}.{name}");

        if (string.IsNullOrEmpty(stringValue)) return defValue;

        return (T)Convert.ChangeType(stringValue, typeof(T));
    }

    private static T[] InitializeArrayEnv<T>(this T[] def, string name, T? defValue = default)
    {
        var stringValue = Environment.GetEnvironmentVariable($"{Namespace}.{name}");

        if (string.IsNullOrEmpty(stringValue)) return Array.Empty<T>();

        return stringValue.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(v => (T)Convert.ChangeType(v, typeof(T))).ToArray();
    }

    private static T[] InitializeArrayEnvAssert<T>(this T[] def, string name, T? defValue = default)
    {
        var val = InitializeArrayEnv(def, name, defValue);
        
        if (val == null) throw new ArgumentException(name);

        if (!SupportedTypes.Contains(typeof(T)))
            throw new ArgumentOutOfRangeException();

        return val;
    }
}
