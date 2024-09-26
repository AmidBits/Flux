namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Yields the number of characters that the source and the target have in common at the end.</summary>
    public static int CommonSuffixLength(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var length = 0;

      var sourceIndex = source.Length;
      var targetIndex = target.Length;

      while (--sourceIndex >= 0 && --targetIndex >= 0 && equalityComparer.Equals(source[sourceIndex], target[targetIndex]))
        length++;

      return length;
    }
  }
}
