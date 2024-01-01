namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Indicates whether the sequence ends with the other sequence. Uses the specified comparer.</summary>
    public static bool StartsWith<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var targetIndex = target.Length;

      if (source.Length < targetIndex) return false;

      while (--targetIndex >= 0)
        if (!equalityComparer.Equals(source[targetIndex], target[targetIndex]))
          return false;

      return true;
    }
  }
}
