namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the specified comparer.</summary>
    public static int CountEqualOnBothSides(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer)
      => CountEqualAtStart(source, target, comparer, out _) + CountEqualAtEnd(source, target, comparer, out _);
    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the default comparer.</summary>
    public static int CountEqualOnBothSides(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target)
      => CountEqualOnBothSides(source, target, System.Collections.Generic.EqualityComparer<char>.Default);

    /// <summary>Yields the number of characters that the source and the target have in common at the end.</summary>
    public static int CountEqualAtEnd(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer, out int minLength)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var sourceIndex = source.Length;
      var targetIndex = target.Length;

      minLength = System.Math.Min(sourceIndex, targetIndex);

      for (var atEnd = 0; --sourceIndex >= 0 && --targetIndex >= 0; atEnd++)
        if (!comparer.Equals(source[sourceIndex], target[targetIndex]))
          return atEnd;

      return minLength;
    }
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the default comparer.</summary>
    public static int CountEqualAtEnd(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, out int minLength)
      => CountEqualAtEnd(source, target, System.Collections.Generic.EqualityComparer<char>.Default, out minLength);
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the specified comparer.</summary>
    public static int CountEqualAtEnd(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer)
      => CountEqualAtEnd(source, target, comparer, out var _);
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the default comparer.</summary>
    public static int CountEqualAtEnd(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target)
      => CountEqualAtEnd(source, target, System.Collections.Generic.EqualityComparer<char>.Default, out var _);

    /// <summary>Yields the number of characters that the source and the target have in common from the start.</summary>
    public static int CountEqualAtStart(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer, out int minLength)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      minLength = System.Math.Min(source.Length, target.Length);

      var index = 0;
      while (index < minLength && comparer.Equals(source[index], target[index]))
        index++;
      return index;
    }
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
    public static int CountEqualAtStart(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, out int minLength)
      => CountEqualAtStart(source, target, System.Collections.Generic.EqualityComparer<char>.Default, out minLength);
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the specified comparer.</summary>
    public static int CountEqualAtStart(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer)
      => CountEqualAtStart(source, target, comparer, out var _);
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
    public static int CountEqualAtStart(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target)
      => CountEqualAtStart(source, target, System.Collections.Generic.EqualityComparer<char>.Default, out _);
  }
}
