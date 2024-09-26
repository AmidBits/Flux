namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Yields the number of characters that the source and the target have in common from the start.</para>
    /// </summary>
    public static int CommonPrefixLength(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var minLength = System.Math.Min(source.Length, target.Length);

      var length = 0;
      while (length < minLength && equalityComparer.Equals(source[length], target[length]))
        length++;
      return length;
    }
  }
}
