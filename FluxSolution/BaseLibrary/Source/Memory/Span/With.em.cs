namespace Flux
{
  public static partial class XtensionsSpan
  {
    public static bool EndsWith<T>(this System.Span<T> source, System.Span<T> value, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      if (source.Length < value.Length) return false;

      for (int sourceIndex = source.Length - 1, valueIndex = value.Length - 1; sourceIndex >= 0 && valueIndex >= 0; sourceIndex--, valueIndex--)
        if (!comparer.Equals(source[sourceIndex], value[valueIndex]))
          return false;

      return true;
    }
    public static bool EndsWith<T>(this System.Span<T> source, System.Span<T> value)
      => EndsWith(source, value, System.Collections.Generic.EqualityComparer<T>.Default);

    public static bool StartsWith<T>(this System.Span<T> source, System.Span<T> value, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      if (source.Length < value.Length) return false;

      for (var index = 0; index < value.Length; index++)
        if (!comparer.Equals(source[index], value[index]))
          return false;

      return true;
    }
    public static bool StartsWith<T>(this System.Span<T> source, System.Span<T> value)
       => EndsWith(source, value, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
