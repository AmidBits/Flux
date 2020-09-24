namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Attempts to get the next enum item of the specified enum type T.</summary>
    public static bool TryGetNext<T>(this T source, out T result)
      where T : System.Enum
      => (result = typeof(T).IsEnum ? ((T[])System.Enum.GetValues(source.GetType()) is var a && System.Array.IndexOf(a, source) + 1 is var i && i == a.Length ? a[0] : a[i]) : default!) is var _;
    /// <summary>Attempts to get the next enum item of the specified enum type T.</summary>
    public static bool TryGetPrevious<T>(this T source, out T result)
      where T : System.Enum
      => (result = typeof(T).IsEnum ? ((T[])System.Enum.GetValues(source.GetType()) is var a && System.Array.IndexOf(a, source) - 1 is var i && i < 0 ? a[a.Length - 1] : a[i]) : default!) is var _;

    /// <summary>Attempts to convert the source name string to an enum type (T).</summary>
    public static T ToEnumByName<T>(this string source)
      where T : System.Enum
      => typeof(T).IsEnum && System.Enum.TryParse(typeof(T), source, true, out object? result)
      ? (T)result ?? throw new System.ArgumentOutOfRangeException(nameof(source))
      : throw new System.Exception($"Could not convert \"{source}\" to enum {typeof(T).Name}.");

    /// <summary>Attempts to convert the source value type to an enum type (T).</summary>
    public static T ToEnumByValue<T>(this System.ValueType source)
      where T : System.Enum
      => (typeof(T).IsEnum && System.Enum.IsDefined(typeof(T), source))
      ? (T)System.Enum.ToObject(typeof(T), source)
      : throw new System.Exception($"Could not convert ({source}) to enum {typeof(T).Name}.");

  }
}
