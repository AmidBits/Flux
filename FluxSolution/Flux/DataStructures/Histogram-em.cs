namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Computes a frequency histogram from the elements in the sequence into a dictionary. This version can be used for semi-aggregate data sources, hence the frequency selector. Uses the specified comparer.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <see href="https://en.wikipedia.org/wiki/Histogram"/>
    /// <seealso href="http://www.greenteapress.com/thinkstats/thinkstats.pdf"/>
    public static DataStructures.Histogram<TKey, TFrequency> ToHistogram<TSource, TKey, TFrequency>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, TFrequency> frequencySelector)
      where TKey : System.Numerics.INumber<TKey>
      where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
      => new DataStructures.Histogram<TKey, TFrequency>().AddRange(source, keySelector, frequencySelector);
  }
}
