namespace Flux
{
  public static partial class Fx
  {
    public static void RemoveSpan<T>(this System.Collections.Generic.ISet<T> source, System.ReadOnlySpan<T> other, out int count)
    {
      count = 0;

      for (var index = 0; index < other.Length; index++)
        if (source.Remove(other[index]))
          count++;
    }

    public static void RemoveSpan<T>(this System.Collections.Generic.ISet<T> source, System.ReadOnlySpan<T> other)
    {
      for (var index = 0; index < other.Length; index++)
        source.Remove(other[index]);
    }
  }
}
