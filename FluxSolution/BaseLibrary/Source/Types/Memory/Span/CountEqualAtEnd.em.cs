namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Reports the length (or count) of equality at the end of the sequence. Using the specified comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualAtEnd<T>(this System.Span<T> source, System.Span<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer, out int minLength)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      var sourceIndex = source.Length;
      var targetIndex = target.Length;

      minLength = System.Math.Min(sourceIndex, targetIndex);

      for (var atEnd = 0; --sourceIndex >= 0 && --targetIndex >= 0; atEnd++)
        if (!comparer.Equals(source[sourceIndex], target[targetIndex]))
          return atEnd;

      return minLength;
    }
    /// <summary>Reports the length (or count) of equality at the end of the sequence. Using the default comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualAtEnd<T>(this System.Span<T> source, System.Span<T> target, out int minLength)
      => CountEqualAtEnd(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out minLength);
  }
}
