namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Indicates whether the sequence starts with the other sequence. Uses the specified comparer.</summary>
    public static bool StartsWith<T>(this System.Span<T> source, System.ReadOnlySpan<T> other, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      if (source.Length < other.Length) return false;

      for (var index = 0; index < other.Length; index++)
        if (!comparer.Equals(source[index], other[index]))
          return false;

      return true;
    }
    /// <summary>Indicates whether the sequence starts with the other sequence. Uses the default comparer.</summary>
    public static bool StartsWith<T>(this System.Span<T> source, System.ReadOnlySpan<T> other)
      => StartsWith(source, other, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}