namespace Flux
{
  public static partial class XtensionsReadOnlySpan
  {    /// <summary>Reports the length (or count) of equal, using the specified comparer.</summary>
    public static bool EndsWith<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => CountEqualOnRight(source, target, comparer, out var _) == target.Length;
    /// <summary>Reports the length (or count) of equal, using the default comparer.</summary>
    public static bool EndsWith<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => CountEqualOnRight(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out var _) == target.Length;

    /// <summary>Determines whether the syaty of the ReadOnlySpan matches a specified target ReadOnlySpan, using the specified comparer.</summary>
    public static bool StartsWith<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => CountEqualOnLeft(source, target, comparer, out var _) == target.Length;
    /// <summary>Determines whether the syaty of the ReadOnlySpan matches a specified target ReadOnlySpan, using the default comparer.</summary>
    public static bool StartsWith<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => CountEqualOnLeft(source, target, System.Collections.Generic.EqualityComparer<T>.Default, out var _) == target.Length;
  }
}
