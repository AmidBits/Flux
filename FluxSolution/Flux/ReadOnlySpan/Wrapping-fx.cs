namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    /// <summary>Indicates whether the source is wrapped in the specified characters. E.g. brackets, or parenthesis.</summary>
    public static bool IsWrapped<T>(this System.ReadOnlySpan<T> source, T left, T right, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return source.Length >= 2 && equalityComparer.Equals(source[0], left) && equalityComparer.Equals(source[^1], right);
    }

    /// <summary>Indicates whether the source is wrapped in the specified left and right strings. If either the strings are null, a false is returned.</summary>
    public static bool IsWrapped<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> left, System.ReadOnlySpan<T> right, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return source.Length >= (left.Length + right.Length) && source.IsCommonPrefix(left, equalityComparer) && source.IsCommonSuffix(right, equalityComparer);
    }
  }
}
