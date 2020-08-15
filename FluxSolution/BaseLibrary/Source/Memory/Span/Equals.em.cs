namespace Flux
{
  public static partial class XtendSpan
  {
    /// <summary>Returns whether the specified part of the target is found at the specified index in the string, using the specified comparer.</summary>
    public static bool Equals<T>(this System.Span<T> source, int sourceIndex, System.ReadOnlySpan<T> target, int targetIndex, int length, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      if (sourceIndex < 0 || targetIndex < 0 || length <= 0 || sourceIndex + length > source.Length || targetIndex + length > target.Length) return false;

      while (length-- > 0)
        if (!comparer.Equals(source[sourceIndex++], target[targetIndex++]))
          return false;

      return true;
    }
    /// <summary>Returns whether the specified part of the target is found at the specified index in the string, using the default comparer.</summary>
    public static bool Equals<T>(this System.Span<T> source, int sourceIndex, System.ReadOnlySpan<T> target, int targetIndex, int length)
      => Equals(source, sourceIndex, target, targetIndex, length, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Returns whether the specified target is found at the specified index in the string, using the specified comparer.</summary>
    public static bool Equals<T>(this System.Span<T> source, int sourceIndex, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => Equals(source, sourceIndex, target, 0, target.Length, comparer);

    /// <summary>Returns whether the specified target is found at the specified index in the string, using the default comparer.</summary>
    public static bool Equals<T>(this System.Span<T> source, int sourceIndex, System.ReadOnlySpan<T> target)
      => Equals(source, sourceIndex, target, 0, target.Length, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
