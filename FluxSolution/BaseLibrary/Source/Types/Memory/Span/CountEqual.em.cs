namespace Flux
{
  public static partial class Xtensions
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

    /// <summary>Reports the length (or count) of equality at the start of the sequence. Using the default comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualAtStart<T>(this System.Span<T> source, System.Span<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer, out int minLength)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      minLength = System.Math.Min(source.Length, target.Length);

      var index = 0;
      while (index < minLength && comparer.Equals(source[index], target[index]))
        index++;
      return index;
    }
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualAtStart<T>(this System.Span<T> source, System.Span<T> target, out int minLength)
      => CountEqualAtStart(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out minLength);

    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the specified comparer.</summary>
    public static int CountEqualOnBothSides<T>(this System.Span<T> source, System.Span<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => source.CountEqualAtStart(target, comparer, out _) + source.CountEqualAtEnd(target, comparer, out _);
    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the default comparer.</summary>
    public static int CountEqualOnBothSides<T>(this System.Span<T> source, System.Span<T> target)
      => CountEqualOnBothSides(source, target, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Reports the count of elements equal at the end of the sequences. Using the specified comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualAtEnd<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer, out int minLength)
      => CountEqualAtEnd((System.Span<T>)(T[])source, (System.Span<T>)(T[])target, equalityComparer, out minLength);
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the default comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualAtEnd<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, out int minLength)
      => CountEqualAtEnd((System.Span<T>)(T[])source, (System.Span<T>)(T[])target, System.Collections.Generic.EqualityComparer<T>.Default, out minLength);

    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the specified comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualAtStart<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer, out int minLength)
      => CountEqualAtStart((System.Span<T>)(T[])source, (System.Span<T>)(T[])target, equalityComparer, out minLength);
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualAtStart<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, out int minLength)
      => CountEqualAtStart((System.Span<T>)(T[])source, (System.Span<T>)(T[])target, System.Collections.Generic.EqualityComparer<T>.Default, out minLength);

    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the specified comparer.</summary>
    public static int CountEqualOnBothSides<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => CountEqualOnBothSides((System.Span<T>)(T[])source, (System.Span<T>)(T[])target, equalityComparer);
    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the default comparer.</summary>
    public static int CountEqualOnBothSides<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target)
      => CountEqualOnBothSides((System.Span<T>)(T[])source, (System.Span<T>)(T[])target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
