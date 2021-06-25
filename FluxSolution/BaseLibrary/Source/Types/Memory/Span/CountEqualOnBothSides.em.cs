namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the specified comparer.</summary>
    public static int CountEqualOnBothSides<T>(this System.Span<T> source, System.Span<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => source.CountEqualAtStart(target, comparer, out _) + source.CountEqualAtEnd(target, comparer, out _);
    /// <summary>Reports the length (or count) of equality at both the start and the end of the sequence. Using the default comparer.</summary>
    public static int CountEqualOnBothSides<T>(this System.Span<T> source, System.Span<T> target)
      => CountEqualOnBothSides(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
