namespace Flux
{
  public static partial class Percentiles
  {
    public enum LerpVariant
    {
      ExcelExc,
      ExcelInc,
    }

    /// <summary>Compute the ordinal index (rank) of the P-th percentile by means of nearest rank.</summary>
    public static int PercentNearestOrdinalRank(double percentile, int count)
      => percentile < 0 || percentile > 1
      ? throw new System.ArgumentOutOfRangeException(nameof(percentile))
      : count < 0
      ? throw new System.ArgumentOutOfRangeException(nameof(count))
      : System.Convert.ToInt32(System.Math.Ceiling(percentile * count));
    /// <summary>Compute the ordinal index (rank) of the P-th percentile by means of nearest rank.</summary>
    public static int PercentNearestOrdinalRank(int percentile, int count)
      => PercentNearestOrdinalRank(percentile / 100.0, count);

    public static double PercentRank(double percentile, int count, LerpVariant variant)
      => variant switch
      {
        LerpVariant.ExcelExc => PercentRankExcelExc(percentile, count),
        LerpVariant.ExcelInc => PercentRankExcelInc(percentile, count),
        _ => throw new System.ArgumentOutOfRangeException(nameof(variant))
      };
    public static double PercentRank(int percentile, int count, LerpVariant variant)
      => variant switch
      {
        LerpVariant.ExcelExc => PercentRankExcelExc(percentile, count),
        LerpVariant.ExcelInc => PercentRankExcelInc(percentile, count),
        _ => throw new System.ArgumentOutOfRangeException(nameof(variant))
      };

    /// <summary>Computes the percentile value of the percentile within the source sequence.</summary>
    public static double PercentileValue(this System.Collections.Generic.IEnumerable<double> source, double percentile, LerpVariant variant)
      => variant switch
      {
        LerpVariant.ExcelExc => PercentileValueExcelExc(source, percentile),
        LerpVariant.ExcelInc => PercentileValueExcelInc(source, percentile),
        _ => throw new System.ArgumentOutOfRangeException(nameof(variant))
      };

    /// <summary>Excel '.EXC' percent rank. Primary variant recommended by NIST.</summary>
    public static double PercentRankExcelExc(double percentile, int count)
      => percentile < 0 || percentile > 1
      ? throw new System.ArgumentOutOfRangeException(nameof(percentile))
      : count < 0
      ? throw new System.ArgumentOutOfRangeException(nameof(count))
      : percentile * (count + 1);
    /// <summary>Excel '.EXC' percent rank. Primary variant recommended by NIST.</summary>
    public static double PercentRankExcelExc(int percentile, int count)
      => PercentRankExcelExc(percentile / 100.0, count);

    /// <summary>Excel '.INC' percent rank. Noted as an alternative by NIST.</summary>
    public static double PercentRankExcelInc(double percentile, int count)
      => percentile < 0 || percentile > 1
      ? throw new System.ArgumentOutOfRangeException(nameof(percentile))
      : count < 0
      ? throw new System.ArgumentOutOfRangeException(nameof(count))
      : percentile * (count - 1) + 1;
    /// <summary>Excel '.INC' percent rank. Noted as an alternative by NIST.</summary>
    public static double PercentRankExcelInc(int percentile, int count)
      => PercentRankExcelInc(percentile / 100.0, count);

    /// <summary>Excel percentile value (exc). The primary variant recommended by NIST.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Percentile#Third_variant,_C_=_0"/>
    public static double PercentileValueExcelExc(this System.Collections.Generic.IEnumerable<double> source, double percentile)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (percentile < 0 || percentile > 1) throw new System.ArgumentOutOfRangeException(nameof(percentile));

      var values = source.ToList();
      var valuesCount = values.Count;

      var x = PercentRankExcelExc(percentile, valuesCount);
      var m = x % 1;

      var i = System.Convert.ToInt32(System.Math.Floor(x));

      var v3 = values[System.Math.Clamp(i, 0, valuesCount - 1)];
      var v2 = values[System.Math.Clamp(i - 1, 0, valuesCount - 1)];

      return v2 + m * (v3 - v2);
    }

    /// <summary>Excel percentile value (inc). Noted as an alternative by NIST.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Percentile#Second_variant,_C_=_1"/>
    public static double PercentileValueExcelInc(this System.Collections.Generic.IEnumerable<double> source, double percentile)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (percentile < 0 || percentile > 1) throw new System.ArgumentOutOfRangeException(nameof(percentile));

      var values = source.ToList();
      var valuesCount = values.Count;

      var x = PercentRankExcelInc(percentile, valuesCount);
      var m = x % 1;

      var i = System.Convert.ToInt32(System.Math.Floor(x));

      var v3 = values[System.Math.Clamp(i, 0, valuesCount - 1)];
      var v2 = values[System.Math.Clamp(i - 1, 0, valuesCount - 1)];

      return v2 + m * (v3 - v2);
    }

    ///// <summary>Matlab percent rank at the specified index.</summary>
    //public static double PercentRankMatlab(int index, int count)
    //  => 100.0 / count * (index - 0.5) / 100.0;

    ///// <summary>Matlab percentile value (prctile).</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Percentile#First_variant,_C_=_1/2"/>
    //public static System.Collections.Generic.SortedDictionary<double, double> PercentileRanksMatlab(this System.Collections.Generic.IEnumerable<double> source)
    //{
    //  if (source is null) throw new System.ArgumentNullException(nameof(source));

    //  var keys = source.ToOrderedHashSet();

    //  var index = 1;

    //  var pv = new System.Collections.Generic.SortedDictionary<double, double>();

    //  foreach (var key in keys)
    //  {
    //    pv[key] = PercentRankMatlab(index, keys.Count);

    //    index++;
    //  }

    //  return pv;
    //}

    //public static double PercentileValuesMatlab(this System.Collections.Generic.IEnumerable<double> source, double percentile)
    //{
    //  var prs = PercentileRanksMatlab(source);

    //  if (prs.First() is var plt && percentile < plt.Value)
    //    return plt.Key;
    //  if (prs.Last() is var pgt && percentile > pgt.Value)
    //    return pgt.Key;

    //  if (prs.ContainsValue(percentile))
    //    foreach (var kvp in prs)
    //      if (kvp.Value == percentile)
    //        return kvp.Key;

    //  var extrema = prs.ExtremaClosestToKey(kvp => kvp.Value, percentile);

    //  return extrema.elementLt.Key + prs.Count * (percentile - extrema.elementLt.Value) * (extrema.elementGt.Key - extrema.elementLt.Key);
    //}
  }
}
