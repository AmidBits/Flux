namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Yields the number of characters that the source and the target have in common from the start.</summary>
    public static int CountEqualAtStart(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var minLength = System.Math.Min(source.Length, target.Length);

      var index = 0;
      while (index < minLength && equalityComparer.Equals(source[index], target[index]))
        index++;
      return index;
    }
  }
}
