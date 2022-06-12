namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Creates a new sequence with every n-th element from the specified offset.</summary>
    public static System.Collections.Generic.IEnumerable<T> TakeEvery<T>(this System.Collections.Generic.IEnumerable<T> source, int interval, int initialOffset)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (interval <= 0) throw new System.ArgumentOutOfRangeException(nameof(interval));
      if (initialOffset <= 0) throw new System.ArgumentOutOfRangeException(nameof(initialOffset));

      return source.Skip(initialOffset).Where((e, i) => i % interval == 0);
    }
  }
}
