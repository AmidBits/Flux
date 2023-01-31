namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Reports the index in <paramref name="source"/> of the first occurence that satisfy the <paramref name="predicate"/>.</summary>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var sourceLength = source.Length;

      for (var index = 0; index < sourceLength; index++)
        if (predicate(source[index], index))
          return index;

      return -1;
    }

    /// <summary>Reports the first index of the specified <paramref name="value"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var index = 0; index < source.Length; index++)
        if (equalityComparer.Equals(source[index], value))
          return index;

      return -1;
    }

    /// <summary>Returns the first index of the specified <paramref name="target"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var targetLength = target.Length;

      var maxLength = source.Length - targetLength;

      for (var index = 0; index < maxLength; index++)
        if (EqualsAt(source, index, target, 0, targetLength, equalityComparer))
          return index;

      return -1;
    }
  }
}
