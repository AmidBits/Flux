namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Yields the number of characters that the source and the target have in common at the end.</summary>
    public static int CountEqualAtEnd(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var sourceIndex = source.Length;
      var targetIndex = target.Length;

      var minLength = System.Math.Min(sourceIndex, targetIndex);

      for (var atEnd = 0; --sourceIndex >= 0 && --targetIndex >= 0; atEnd++)
        if (!equalityComparer.Equals(source[sourceIndex], target[targetIndex]))
          return atEnd;

      return minLength;
    }
  }
}
