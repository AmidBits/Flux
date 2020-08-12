namespace Flux
{
  public static partial class XtensionsString
  {
    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the specified comparer.</summary>
    public static int CountEqualOnBothEnds(this string source, string target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer)
      => source.CountEqualOnLeft(target, comparer, out _) + source.CountEqualOnRight(target, comparer, out _);
    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the default comparer.</summary>
    public static int CountEqualOnBothEnds(this string source, string target)
      => CountEqualOnBothEnds(source, target, System.Collections.Generic.EqualityComparer<char>.Default);

    /// <summary>Determines whether the beginning of the first sequence is equivalent to the second sequence, using the specified element equality comparer.</summary>
    public static int CountEqualOnLeft(this string source, string target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer, out int minLength)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var index = 0;

      minLength = source.Length < target.Length ? source.Length : target.Length;

      while (index < minLength && comparer.Equals(source[index], target[index])) index++;

      return index;
    }
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
    public static int CountEqualOnLeft(this string source, string target, out int minLength)
      => CountEqualOnLeft(source, target, System.Collections.Generic.EqualityComparer<char>.Default, out minLength);
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the specified comparer.</summary>
    public static int CountEqualOnLeft(this string source, string target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer)
      => CountEqualOnLeft(source, target, comparer, out var _);
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
    public static int CountEqualOnLeft(this string source, string target)
      => CountEqualOnLeft(source, target, System.Collections.Generic.EqualityComparer<char>.Default, out _);

    public static int CountEqualOnRight(this string source, string target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer, out int minLength)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var sourceIndex = source.Length;
      var targetIndex = target.Length;

      minLength = sourceIndex < targetIndex ? sourceIndex : targetIndex;

      for (var atEnd = 0; --sourceIndex >= 0 && --targetIndex >= 0; atEnd++)
        if (!comparer.Equals(source[sourceIndex], target[targetIndex]))
          return atEnd;

      return minLength;
    }
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the default comparer.</summary>
    public static int CountEqualOnRight(this string source, string target, out int minLength)
      => CountEqualOnRight(source, target, System.Collections.Generic.EqualityComparer<char>.Default, out minLength);
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the specified comparer.</summary>
    public static int CountEqualOnRight(this string source, string target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer)
      => CountEqualOnRight(source, target, comparer, out var _);
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the default comparer.</summary>
    public static int CountEqualOnRight(this string source, string target)
      => CountEqualOnRight(source, target, System.Collections.Generic.EqualityComparer<char>.Default, out var _);
  }
}
