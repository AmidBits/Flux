namespace Flux
{
  public static partial class ExtensionMethodsStringBuilder
  {
    /// <summary>Yields the number of characters that the source and the target have in common from the specified respective indices. Uses the specified comparer.</summary>
    public static int CountEqualAt(this System.Text.StringBuilder source, int sourceStartIndex, System.ReadOnlySpan<char> target, int targetStartIndex, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> equalityComparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var sourceLength = source.Length;
      var targetLength = target.Length;

      if (sourceStartIndex < 0 || sourceStartIndex >= sourceLength) throw new System.ArgumentOutOfRangeException(nameof(sourceStartIndex));
      if (targetStartIndex < 0 || targetStartIndex >= targetLength) throw new System.ArgumentOutOfRangeException(nameof(targetStartIndex));
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var minLength = System.Math.Min(sourceLength - sourceStartIndex, targetLength - targetStartIndex);

      var count = 0;
      while (count < minLength && equalityComparer.Equals(source[sourceStartIndex++], target[targetStartIndex++]))
        count++;
      return count;
    }
    /// <summary>Yields the number of characters that the source and the target have in common from the specified respective indices. Uses the default comparer.</summary>
    public static int CountEqualAt(this System.Text.StringBuilder source, int sourceStartIndex, System.ReadOnlySpan<char> target, int targetStartIndex)
      => CountEqualAt(source, sourceStartIndex, target, targetStartIndex, System.Collections.Generic.EqualityComparer<char>.Default);
  }
}
