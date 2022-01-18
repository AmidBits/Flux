namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Reports the index of the first occurence that satisfy the predicate.</summary>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var sourceLength = source.Length;

      for (var index = 0; index < sourceLength; index++)
        if (predicate(source[index], index))
          return index;

      return -1;
    }

    /// <summary>Reports the first index of the specified target within the source, or -1 if not found. Uses the specified comparer.</summary>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, T value, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      for (var index = 0; index < source.Length; index++)
        if (equalityComparer.Equals(source[index], value))
          return index;

      return -1;
    }
    /// <summary>Reports the first index of the specified target within the source, or -1 if not found. Uses the default comparer.</summary>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, T value)
      => IndexOf(source, value, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Returns the first index of the specified target within the source, or -1 if not found. Uses the specified comparer.</summary>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var targetLength = target.Length;
      
      var maxLength = source.Length - targetLength;

      for (var index = 0; index < maxLength; index++)
        if (EqualsAt(source, index, target, 0, targetLength, equalityComparer))
          return index;

      return -1;
    }
    /// <summary>Reports the first index of the specified target within the source, or -1 if not found. Uses the default comparer.</summary>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> value)
      => IndexOf(source, value, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
