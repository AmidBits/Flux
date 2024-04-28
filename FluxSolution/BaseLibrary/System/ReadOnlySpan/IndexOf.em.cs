namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Reports the first index of the specified <paramref name="value"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var index = 0; index < source.Length; index++)
        if (equalityComparer.Equals(source[index], value))
          return index;

      return -1;
    }

    /// <summary>Returns the first index of the specified <paramref name="value"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var maxIndex = source.Length - value.Length;

      for (var index = 0; index < maxIndex; index++)
        if (EqualsAt(source, index, value, 0, value.Length, equalityComparer))
          return index;

      return -1;
    }
  }
}
