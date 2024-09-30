namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Yields the number of characters that the source and the target have in common at the end.</para>
    /// </summary>
    public static int CommonSuffixLength(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var sourceIndex = source.Length;
      var targetIndex = target.Length;

      var length = 0;
      while (--sourceIndex >= 0 && --targetIndex >= 0 && equalityComparer.Equals(source[sourceIndex], target[targetIndex]))
        length++;
      return length;
    }
  }
}
