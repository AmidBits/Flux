namespace Flux
{
  public static partial class XtendReadOnlySpan
  {
    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the specified comparer.</summary>
    public static int CountEqualOnBothEnds<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => source.CountEqualOnLeft(target, comparer, out _) + source.CountEqualOnRight(target, comparer, out _);
    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the default comparer.</summary>
    public static int CountEqualOnBothEnds<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => CountEqualOnBothEnds(source, target, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the specified comparer.</summary>
    public static int CountEqualOnLeft<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer, out int minLength)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceLength = source.Length;
      var targetLength = target.Length;

      minLength = sourceLength < targetLength ? sourceLength : targetLength;

      var index = 0;
      while (index < minLength && comparer.Equals(source[index], target[index]))
        index++;
      return index;
    }
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
    public static int CountEqualOnLeft<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out int minLength)
      => CountEqualOnLeft(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out minLength);
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the specified comparer.</summary>
    public static int CountEqualOnLeft<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => CountEqualOnLeft(source, target, comparer, out var _);
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
    public static int CountEqualOnLeft<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => CountEqualOnLeft(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out _);

    /// <summary>Reports the count of elements equal at the end of the sequences. Using the specified comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CountEqualOnRight<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer, out int minLength)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceIndex = source.Length;
      var targetIndex = target.Length;

      minLength = sourceIndex < targetIndex ? sourceIndex : targetIndex;

      for (var atEnd = 0; --sourceIndex >= 0 && --targetIndex >= 0; atEnd++)
        if (!comparer.Equals(source[sourceIndex], target[targetIndex]))
          return atEnd;

      return minLength;
    }
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the default comparer.</summary>
    public static int CountEqualOnRight<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out int minLength)
      => CountEqualOnRight(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out minLength);
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the specified comparer.</summary>
    public static int CountEqualOnRight<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => CountEqualOnRight(source, target, comparer, out var _);
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the default comparer.</summary>
    public static int CountEqualOnRight<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => CountEqualOnRight(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out var _);
  }
}
