namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Creates a new sequence with every n-th element from the sequence.</summary>
    public static System.Collections.Generic.IEnumerable<T> TakeEvery<T>(this System.Collections.Generic.IEnumerable<T> source, int nth, int startAt)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (nth <= 0) throw new System.ArgumentOutOfRangeException(nameof(nth));

      return System.Linq.Enumerable.Where(System.Linq.Enumerable.Skip(source, startAt), (e, i) => i % nth == 0);
    }
  }
}
