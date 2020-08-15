namespace Flux
{
  public static partial class XtendSpan
  {
    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the specified comparer.</summary>
    public static int CountEqualOnBothEnds<T>(this System.Span<T> source, System.Span<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => source.CountEqualOnLeft(target, comparer, out _) + source.CountEqualOnRight(target, comparer, out _);
    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the default comparer.</summary>
    public static int CountEqualOnBothEnds<T>(this System.Span<T> source, System.Span<T> target)
      => CountEqualOnBothEnds(source, target, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Reports the length (or count) of equality at the start of the sequence. Using the default comparer.</summary>
    public static int CountEqualOnLeft<T>(this System.Span<T> source, System.Span<T> target, System.Collections.Generic.IEqualityComparer<T> comparer, out int minLength)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceLength = source.Length;
      var targetLength = target.Length;

      minLength = sourceLength < targetLength ? sourceLength : targetLength;

      for (var index = 0; index < minLength; index++)
        if (!comparer.Equals(source[index], target[index]))
          return index;

      return minLength;
    }
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
    public static int CountEqualOnLeft<T>(this System.Span<T> source, System.Span<T> target, out int minLength)
      => CountEqualOnLeft(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out minLength);
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the specified comparer.</summary>
    public static int CountEqualOnLeft<T>(this System.Span<T> source, System.Span<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => CountEqualOnLeft(source, target, comparer, out var _);
    /// <summary>Reports the length (or count) of equality at the start of the sequence. Using the default comparer.</summary>
    public static int CountEqualOnLeft<T>(this System.Span<T> source, System.Span<T> target)
      => CountEqualOnLeft(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out _);

    /// <summary>Reports the length (or count) of equality at the end of the sequence. Using the specified comparer.</summary>
    public static int CountEqualOnRight<T>(this System.Span<T> source, System.Span<T> target, System.Collections.Generic.IEqualityComparer<T> comparer, out int minLength)
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
    /// <summary>Reports the length (or count) of equality at the end of the sequence. Using the default comparer.</summary>
    public static int CountEqualOnRight<T>(this System.Span<T> source, System.Span<T> target, out int minLength)
      => CountEqualOnRight(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out minLength);
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the specified comparer.</summary>
    public static int CountEqualOnRight<T>(this System.Span<T> source, System.Span<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => CountEqualOnRight(source, target, comparer, out var _);
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the default comparer.</summary>
    public static int CountEqualOnRight<T>(this System.Span<T> source, System.Span<T> target)
      => CountEqualOnRight(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out var _);
  }
}
