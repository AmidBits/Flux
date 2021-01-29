namespace Flux
{
  public static partial class SystemMemoryReadOnlySpanEm
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

    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the specified comparer.</summary>
    public static int CountEqualAtStart<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer, out int minLength)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      minLength = System.Math.Min(source.Length, target.Length);

      var index = 0;
      while (index < minLength && equalityComparer.Equals(source[index], target[index]))
        index++;
      return index;
    }
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
    public static int CountEqualAtStart<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out int minLength)
      => CountEqualAtStart(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out minLength);

    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the specified comparer.</summary>
    public static int CountEqualOnBothSides<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => CountEqualAtStart(source, target, equalityComparer, out _) + CountEqualAtEnd(source, target, equalityComparer, out _);
    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the default comparer.</summary>
    public static int CountEqualOnBothSides<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => CountEqualAtStart(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out _) + CountEqualAtEnd(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out _);

    /// <summary>Reports the count of elements equal at the end of the sequences. Using the specified comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualAtEnd(this string source, string target, System.Collections.Generic.IEqualityComparer<char> equalityComparer, out int minLength)
      => CountEqualAtEnd(source, target, equalityComparer, out minLength);
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the default comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualAtEnd(this string source, string target, out int minLength)
      => CountEqualAtEnd(source, target, System.Collections.Generic.EqualityComparer<char>.Default, out minLength);

    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the specified comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualAtStart(this string source, string target, System.Collections.Generic.IEqualityComparer<char> equalityComparer, out int minLength)
      => CountEqualAtStart(source, target, equalityComparer, out minLength);
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualAtStart(this string source, string target, out int minLength)
      => CountEqualAtStart(source, target, System.Collections.Generic.EqualityComparer<char>.Default, out minLength);

    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the specified comparer.</summary>
    public static int CountEqualOnBothSides(this string source, string target, System.Collections.Generic.IEqualityComparer<char> equalityComparer)
      => CountEqualOnBothSides(source, target, equalityComparer);
    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the default comparer.</summary>
    public static int CountEqualOnBothSides(this string source, string target)
      => CountEqualOnBothSides(source, target, System.Collections.Generic.EqualityComparer<char>.Default);
  }
}
