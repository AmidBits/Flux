namespace Flux
{
  public static partial class Reflection
  {
    private static string InternalGenericToFriendly(string? name)
      => name is null
      ? throw new System.ArgumentNullException(nameof(name))
      : name.Split('`') is var names && names.Length == 2 && int.TryParse(names[1], System.Globalization.NumberStyles.Integer, null, out var count)
      ? $"{names[0]}<{(count > 1 ? string.Join(", ", System.Linq.Enumerable.Range(1, count).Select(i => $"T{i}")) : "T")}>"
      : name;

    /// <summary>Returns the name with various extended functionalities, e.g. "<T1, T2, T3>" for generics instead of "`3".</summary>
    public static string GetFullNameEx(this System.Type source)
      => InternalGenericToFriendly(source?.FullName ?? null);

    /// <summary>Returns the name with various extended functionalities, e.g. "<T1, T2, T3>" for generics instead of "`3".</summary>
    public static string GetNameEx(this System.Type source)
      => InternalGenericToFriendly(source?.Name ?? null);
  }
}
