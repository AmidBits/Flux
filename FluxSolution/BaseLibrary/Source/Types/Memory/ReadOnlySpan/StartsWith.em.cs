namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Indicates whether the sequence ends with the other sequence. Uses the specified comparer.</summary>
    public static bool StartsWith<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      var targetLength = target.Length;

      if (source.Length < targetLength) return false;

      for (var index = 0; index < targetLength; index++)
        if (!comparer.Equals(source[index], target[index]))
          return false;

      return true;
    }
    /// <summary>Indicates whether the sequence ends with the other sequence. Uses the default comparer.</summary>
    public static bool StartsWith<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
       => EndsWith(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
