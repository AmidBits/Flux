namespace Flux
{
  public enum OptionTakeEvery
  {
    First,
    Last
  }

  public static partial class Enumerable
  {
    /// <summary>Creates a new sequence by taking the <paramref name="option"/> at every <paramref name="interval"/> starting at the specified <paramref name="offset"/>.</summary>
    public static System.Collections.Generic.IEnumerable<T> TakeEvery<T>(this System.Collections.Generic.IEnumerable<T> source, int interval, int offset, OptionTakeEvery option)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (interval <= 0) throw new System.ArgumentOutOfRangeException(nameof(interval));
      if (offset < 0) throw new System.ArgumentOutOfRangeException(nameof(offset));

      var takeIndex = option switch
      {
        OptionTakeEvery.First => 0,
        OptionTakeEvery.Last => interval - 1,
        _ => throw new System.ArgumentOutOfRangeException(nameof(option))
      };

      return source.Skip(offset).Where((e, i) => i % interval == takeIndex);
    }
  }
}
