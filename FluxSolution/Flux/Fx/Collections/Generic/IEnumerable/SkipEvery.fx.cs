using System.Linq;

namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new sequence by skipping the <paramref name="option"/> at every <paramref name="interval"/> from <paramref name="source"/>.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<T> SkipEvery<T>(this System.Collections.Generic.IEnumerable<T> source, int interval, OptionEvery option)
    {
      if (interval <= 0) throw new System.ArgumentOutOfRangeException(nameof(interval));

      var skipIndex = option switch
      {
        OptionEvery.First => 0,
        OptionEvery.Last => interval - 1,
        _ => throw new System.ArgumentOutOfRangeException(nameof(option)),
      };

      return source.Where((e, i) => i % interval != skipIndex);
    }
  }
}
