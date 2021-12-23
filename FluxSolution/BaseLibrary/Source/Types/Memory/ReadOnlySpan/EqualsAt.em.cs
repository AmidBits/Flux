namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Indicates whether the specified part of the target is found at the specified index in the source, using the specified comparer.</summary>
    public static bool EqualsAt<T>(this System.ReadOnlySpan<T> source, int sourceIndex, System.ReadOnlySpan<T> target, int targetIndex, int length, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      if (sourceIndex < 0 || targetIndex < 0 || length <= 0 || sourceIndex + length > source.Length || targetIndex + length > target.Length) return false;

      while (length-- > 0)
        if (!comparer.Equals(source[sourceIndex++], target[targetIndex++]))
          return false;

      return true;
    }
    /// <summary>Indicates whether the specified part of the target is found at the specified index in the source, using the default comparer.</summary>
    public static bool EqualsAt<T>(this System.ReadOnlySpan<T> source, int sourceIndex, System.ReadOnlySpan<T> target, int targetIndex, int length)
      => EqualsAt(source, sourceIndex, target, targetIndex, length, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Indicates whether the specified target is found at the specified index in the source, using the specified comparer.</summary>
    public static bool EqualsAt<T>(this System.ReadOnlySpan<T> source, int sourceIndex, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => EqualsAt(source, sourceIndex, target, 0, target.Length, comparer);
    /// <summary>Indicates whether the specified target is found at the specified index in the source, using the default comparer.</summary>
    public static bool EqualsAt<T>(this System.ReadOnlySpan<T> source, int sourceIndex, System.ReadOnlySpan<T> target)
      => EqualsAt(source, sourceIndex, target, 0, target.Length, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
