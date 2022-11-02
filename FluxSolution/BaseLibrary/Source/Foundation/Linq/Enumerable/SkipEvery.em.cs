namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Creates a new sequence by skipping every <paramref name="interval"/> element from the specified <paramref name="offset"/>.</summary>
    public static System.Collections.Generic.IEnumerable<T> SkipEvery<T>(this System.Collections.Generic.IEnumerable<T> source, int interval, int offset)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (interval <= 0) throw new System.ArgumentOutOfRangeException(nameof(interval));
      if (offset < 0) throw new System.ArgumentOutOfRangeException(nameof(offset));

      return source.Skip(offset).Where((e, i) => i % interval != 0);
    }
  }
}
