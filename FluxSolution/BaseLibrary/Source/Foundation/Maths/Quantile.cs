namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Nine algorithms (estimate types and interpolation schemes(.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public enum QuantileType
    {
      R1,
      R2,
      R3,
      R4,
      R5,
      R6,
      R7,
      R8,
      R9,
    }

    /// <summary>Compute the ordinal index (rank) of the P-th percentile by means of nearest rank.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile"/>
    public static double Quantile(System.Collections.Generic.List<double> x, double p, QuantileType type)
    {
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      var N = x.Count;

      return type switch
      {
        QuantileType.R1 when N * p is var h => x[System.Convert.ToInt32(System.Math.Ceiling(h))],
        QuantileType.R2 when N * p + 0.5 is var h => (x[System.Convert.ToInt32(System.Math.Ceiling(h - 0.5))] + x[System.Convert.ToInt32(System.Math.Ceiling(h + 0.5))]) / 2,
        QuantileType.R3 when N * p - 0.5 is var h => x[System.Convert.ToInt32(System.Math.Round(h, System.MidpointRounding.ToEven))],
        QuantileType.R4 when N * p is var h => EmpiricalDistributionFunction(h),
        QuantileType.R5 when N * p + 0.5 is var h => EmpiricalDistributionFunction(h),
        QuantileType.R6 when (N + 1) * p is var h => EmpiricalDistributionFunction(h),
        QuantileType.R7 when (N - 1) * p + 1 is var h => EmpiricalDistributionFunction(h),
        QuantileType.R8 when (N + 1 / 3) * p + 1 / 3 is var h => EmpiricalDistributionFunction(h),
        QuantileType.R9 when (N + 1 / 4) * p + 3 / 8 is var h => EmpiricalDistributionFunction(h),
        _ => throw new System.ArgumentOutOfRangeException(nameof(type)),
      };

      double EmpiricalDistributionFunction(double h)
      {
        var lo = System.Convert.ToInt32(System.Math.Floor(h));
        var hi = System.Convert.ToInt32(System.Math.Ceiling(h));

        return x[lo] + (h - lo) * (x[hi] - x[lo]);
      }
    }
  }
}
