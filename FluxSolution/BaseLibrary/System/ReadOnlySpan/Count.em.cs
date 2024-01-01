namespace Flux
{
  public static partial class Fx
  {
#if NET7_0_OR_GREATER
    public static int Count<T>(this System.ReadOnlySpan<T> source, T target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var count = 0;

      for (var i = 0; i < source.Length; i++)
        if (equalityComparer.Equals(source[i], target))
          count++;

      return count;
    }
#endif
  }
}
