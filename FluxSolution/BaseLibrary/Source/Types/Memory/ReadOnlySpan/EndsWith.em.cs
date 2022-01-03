namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Indicates whether the sequence ends with the other sequence. Uses the specified comparer.</summary>
    public static bool EndsWith<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      var sourceIndex = source.Length;
      var targetIndex = target.Length;

      if (sourceIndex < targetIndex)
        return false;

      while (--sourceIndex >= 0 && --targetIndex >= 0)
        if (!comparer.Equals(source[sourceIndex], target[targetIndex]))
          return false;

      return true;
    }
    /// <summary>Indicates whether the sequence ends with the other sequence. Uses the default comparer.</summary>
    public static bool EndsWith<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => EndsWith(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
