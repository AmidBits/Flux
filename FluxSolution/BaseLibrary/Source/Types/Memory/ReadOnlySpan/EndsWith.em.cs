namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Indicates whether the sequence ends with the other sequence. Uses the specified comparer (null for default).</summary>
    public static bool EndsWith<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceIndex = source.Length;
      var targetIndex = target.Length;

      if (sourceIndex < targetIndex) return false;

      while (--sourceIndex >= 0 && --targetIndex >= 0)
        if (!equalityComparer.Equals(source[sourceIndex], target[targetIndex]))
          return false;

      return true;
    }
  }
}
