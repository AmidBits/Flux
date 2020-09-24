namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returns the number of equal elements at the end of the sequences. Uses the specified equality comparer.</summary>
    public static int CountEqualAtEnd<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer, out int minLength)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (target is null) throw new System.ArgumentNullException(nameof(target));

      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceIndex = source.Count;
      var targetIndex = target.Count;

      minLength = sourceIndex < targetIndex ? sourceIndex : targetIndex;

      for (var atEnd = 0; --sourceIndex >= 0 && --targetIndex >= 0; atEnd++)
        if (!comparer.Equals(source[sourceIndex], target[targetIndex]))
          return atEnd;

      return minLength;
    }
    /// <summary>Returns the number of equal elements at the end of the sequences. Uses the default equality comparer.</summary>
    public static int CountEqualAtEnd<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, out int minLength)
      => CountEqualAtEnd(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out minLength);
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the specified comparer.</summary>
    public static int CountEqualAtEnd<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => CountEqualAtEnd(source, target, comparer, out var _);
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the default comparer.</summary>
    public static int CountEqualAtEnd<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target)
      => CountEqualAtEnd(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out var _);

    /// <summary>Returns the number of equal elements in the sequences at the start. Using the specified equality comparer.</summary>
    public static int CountEqualAtStart<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer, out int minLength)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (target is null) throw new System.ArgumentNullException(nameof(target));

      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceCount = source.Count;
      var targetCount = target.Count;

      minLength = sourceCount < targetCount ? sourceCount : targetCount;

      for (var atStart = 0; atStart < minLength; atStart++)
        if (!comparer.Equals(source[atStart], target[atStart]))
          return atStart;

      return minLength;
    }
    /// <summary>Returns the number of equal elements in the sequences at the start. Using the specified equality comparer.</summary>
    public static int CountEqualAtStart<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, out int minLength)
      => CountEqualAtStart(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out minLength);
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the specified comparer.</summary>
    public static int CountEqualAtStart<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => CountEqualAtStart(source, target, comparer, out var _);
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
    public static int CountEqualAtStart<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target)
      => CountEqualAtStart(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out _);

    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the specified comparer.</summary>
    public static int CountEqualOnBothSides<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => CountEqualAtStart(source, target, comparer, out _) + CountEqualAtEnd(source, target, comparer, out _);
    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the default comparer.</summary>
    public static int CountEqualOnBothSides<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target)
      => CountEqualOnBothSides(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
