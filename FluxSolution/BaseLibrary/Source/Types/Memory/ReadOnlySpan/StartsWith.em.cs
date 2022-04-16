namespace Flux
{
  public static partial class ReadOnlySpanEm
  {
    /// <summary>Indicates whether the sequence ends with the other sequence. Uses the specified comparer.</summary>
    public static bool StartsWith<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var targetLength = target.Length;

      if (source.Length < targetLength) return false;

      for (var index = 0; index < targetLength; index++)
        if (!equalityComparer.Equals(source[index], target[index]))
          return false;

      return true;
    }
    /// <summary>Indicates whether the sequence ends with the other sequence. Uses the default comparer.</summary>
    public static bool StartsWith<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
       => EndsWith(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
