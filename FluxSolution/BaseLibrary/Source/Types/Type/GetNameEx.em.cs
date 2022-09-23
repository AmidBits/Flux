namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Returns the name with various extended functionalities, e.g. "<T1, T2, T3>" for generics instead of "`3".</summary>
    public static string GetNameEx(this System.Type source)
    {
      if (source.Name.Split('`') is var genericName && genericName.Length == 2)
        if (int.TryParse(genericName[1], System.Globalization.NumberStyles.Integer, null, out var count))
          return $"{genericName[0]}<{string.Join(", ", System.Linq.Enumerable.Range(1, count).Select(i => $"T{i}"))}>";

      return source.Name;
    }
  }
}
