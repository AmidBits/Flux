namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Adds all elements from <paramref name="other"/> to the current <see cref="System.Collections.Generic.List{T}"/>.</summary>
    public static void AddRange<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> other)
    {
      foreach (var item in other)
        source.Add(item);
    }
  }
}
