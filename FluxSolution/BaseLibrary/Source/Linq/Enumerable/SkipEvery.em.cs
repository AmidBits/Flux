namespace Flux
{
  public enum OptionSkipEvery
  {
    First,
    Last
  }

  public static partial class Enumerable
  {
    /// <summary>Creates a new sequence by skipping the <paramref name="option"/> at every <paramref name="interval"/> starting at the specified <paramref name="offset"/>.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<T> SkipEvery<T>(this System.Collections.Generic.IEnumerable<T> source, int interval, int offset, OptionSkipEvery option)
    {
      if (interval <= 0) throw new System.ArgumentOutOfRangeException(nameof(interval));
      if (offset < 0) throw new System.ArgumentOutOfRangeException(nameof(offset));

      var skipIndex = option switch
      {
        OptionSkipEvery.First => 0,
        OptionSkipEvery.Last => interval - 1,
        _ => throw new System.ArgumentOutOfRangeException(nameof(option)),
      };

      return source.Skip(offset).Where((e, i) => i % interval != skipIndex);
    }
  }
}
