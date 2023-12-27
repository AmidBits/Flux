namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Yields the number of characters that the source and the target have in common from the specified respective indices. Uses the specified comparer.</summary>
    public static int CountEqualAt(this System.Text.StringBuilder source, int sourceStartIndex, System.ReadOnlySpan<char> target, int targetStartIndex, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var sourceLength = source.Length;
      var targetLength = target.Length;

      if (sourceStartIndex < 0 || sourceStartIndex >= sourceLength) throw new System.ArgumentOutOfRangeException(nameof(sourceStartIndex));
      if (targetStartIndex < 0 || targetStartIndex >= targetLength) throw new System.ArgumentOutOfRangeException(nameof(targetStartIndex));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var minLength = System.Math.Min(sourceLength - sourceStartIndex, targetLength - targetStartIndex);

      var count = 0;
      while (count < minLength && equalityComparer.Equals(source[sourceStartIndex++], target[targetStartIndex++]))
        count++;
      return count;
    }
  }
}
