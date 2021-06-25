namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the specified comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualAtEnd<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer, out int minLength)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var sourceIndex = source.Length;
      var targetIndex = target.Length;

      minLength = System.Math.Min(sourceIndex, targetIndex);

      for (var atEnd = 0; --sourceIndex >= 0 && --targetIndex >= 0; atEnd++)
        if (!equalityComparer.Equals(source[sourceIndex], target[targetIndex]))
          return atEnd;

      return minLength;
    }
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the default comparer.</summary>
    public static int CountEqualAtEnd<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out int minLength)
      => CountEqualAtEnd(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out minLength);

    /// <summary>Reports the count of elements equal at the end of the sequences. Using the specified comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualAtEnd(this string source, string target, System.Collections.Generic.IEqualityComparer<char> equalityComparer, out int minLength)
      => CountEqualAtEnd(source, target, equalityComparer, out minLength);
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the default comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualAtEnd(this string source, string target, out int minLength)
      => CountEqualAtEnd(source, target, System.Collections.Generic.EqualityComparer<char>.Default, out minLength);
  }
}
