namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the specified comparer.</summary>
    public static int CountEqualOnBothSides<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => CountEqualAtStart(source, target, equalityComparer, out _) + CountEqualAtEnd(source, target, equalityComparer, out _);
    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the default comparer.</summary>
    public static int CountEqualOnBothSides<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => CountEqualAtStart(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out _) + CountEqualAtEnd(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out _);

    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the specified comparer.</summary>
    public static int CountEqualOnBothSides(this string source, string target, System.Collections.Generic.IEqualityComparer<char> equalityComparer)
      => CountEqualOnBothSides(source, target, equalityComparer);
    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the default comparer.</summary>
    public static int CountEqualOnBothSides(this string source, string target)
      => CountEqualOnBothSides(source, target, System.Collections.Generic.EqualityComparer<char>.Default);
  }
}
