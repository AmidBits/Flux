namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Indicates whether the source ends with value. Uses the specified comparer.</summary>
    public static bool EndsWith(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var sourceIndex = source.Length;
      var targetIndex = target.Length;

      if (sourceIndex < targetIndex)
        return false;

      while (--sourceIndex >= 0 && --targetIndex >= 0)
        if (!equalityComparer.Equals(source[sourceIndex], target[targetIndex]))
          return false;

      return true;
    }
  }
}
