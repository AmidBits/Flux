namespace Flux
{
  //public static partial class ExtensionMethods
  //{
  //  /// <summary>Computes a frequency histogram from the elements in the sequence into a dictionary. This version can be used for semi-aggregate data sources, hence the frequency selector. Uses the specified comparer.</summary>
  //  /// <exception cref="System.ArgumentNullException"/>
  //  /// <see cref="https://en.wikipedia.org/wiki/Histogram"/>
  //  public static DataStructures.ProbabilityMassFunction<TKey, TProbability> ToProbabilityMassFunction<TKey, TFrequency, TProbability>(this DataStructures.Histogram<TKey, TFrequency> source, TProbability factor)
  //    where TKey : notnull
  //    where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
  //    where TProbability : System.Numerics.IFloatingPoint<TProbability>
  //    => DataStructures.ProbabilityMassFunction<TKey, TProbability>.Create(source, factor);

  //  public static TProbability ToPmfProbability<TKey, TFrequency, TProbability>(this DataStructures.Histogram<TKey, TFrequency> source, TKey key, TProbability factor)
  //    where TKey : notnull
  //    where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
  //    where TProbability : System.Numerics.IFloatingPoint<TProbability>
  //    => DataStructures.ProbabilityMassFunction<TKey, TProbability>.ComputeProbability(source, key, factor);
  //}

  namespace DataStructures
  {
    public class ProbabilityMassFunction<TKey, TProbability>
      : System.Collections.Generic.IReadOnlyDictionary<TKey, TProbability>
      where TKey : notnull
      where TProbability : System.Numerics.IFloatingPoint<TProbability>
    {
      internal System.Collections.Generic.SortedDictionary<TKey, TProbability> m_data = new System.Collections.Generic.SortedDictionary<TKey, TProbability>();

      internal ProbabilityMassFunction() { }

      #region Static methods.
      /// <summary>The CDF is the function that maps values to their percentile rank, within the probability range [0, 1], in a distribution. Uses the specified comparer.</summary>
      /// <remarks>This function maps </remarks>
      /// <exception cref="System.ArgumentNullException"/>
      public static TProbability ComputeProbability<TFrequency>(Histogram<TKey, TFrequency> histogram, TKey key, TProbability factor)
        where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
        => TProbability.CreateChecked(histogram[key]) / TProbability.CreateChecked(histogram.TotalFrequency) * factor;
      #endregion Static methods.

      #region Implemented interfaces.
      public TProbability this[TKey key] => m_data.TryGetValue(key, out var probability) ? probability : TProbability.Zero;

      public System.Collections.Generic.IEnumerable<TKey> Keys => m_data.Keys;

      public System.Collections.Generic.IEnumerable<TProbability> Values => m_data.Values;

      public int Count => m_data.Count;

      public bool ContainsKey(TKey key) => m_data.ContainsKey(key);

      public System.Collections.Generic.IEnumerator<KeyValuePair<TKey, TProbability>> GetEnumerator() => m_data.GetEnumerator();

      public bool TryGetValue(TKey key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out TProbability value) => m_data.TryGetValue(key, out value);

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
      #endregion Implemented interfaces.
    }
  }
}
