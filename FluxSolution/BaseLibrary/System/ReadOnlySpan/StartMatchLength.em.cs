namespace Flux
{
  public static partial class Fx
  {
    public static int StartMatchLength<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(predicate);

      var length = 0;
      while (length < source.Length && predicate(source[length]))
        length++;
      return length;
    }

    public static int StartMatchLength<T>(this System.ReadOnlySpan<T> source, T target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var length = 0;
      while (length < source.Length && equalityComparer.Equals(source[length], target))
        length++;
      return length;
    }

    public static int StartMatchLength<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var minLength = int.Min(source.Length, target.Length);

      var length = 0;
      while (length < minLength && equalityComparer.Equals(source[length], target[length]))
        length++;
      return length;
    }
  }
}
