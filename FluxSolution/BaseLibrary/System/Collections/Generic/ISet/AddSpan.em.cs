namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Adds all elements from <paramref name="other"/> to the current <see cref="System.Collections.Generic.ISet{T}"/> and returns the number of elements that were successfully added.</summary>
    public static void AddSpan<T>(this System.Collections.Generic.ISet<T> source, System.ReadOnlySpan<T> other, out int count)
    {
      count = 0;

      for (var index = 0; index < other.Length; index++)
        if (source.Add(other[index]))
          count++;
    }

    /// <summary>Adds all elements from <paramref name="other"/> to the current <see cref="System.Collections.Generic.ISet{T}"/> and returns the number of elements that were successfully added.</summary>
    public static void AddSpan<T>(this System.Collections.Generic.ISet<T> source, System.ReadOnlySpan<T> other)
    {
      for (var index = 0; index < other.Length; index++)
        source.Add(other[index]);
    }
  }
}
