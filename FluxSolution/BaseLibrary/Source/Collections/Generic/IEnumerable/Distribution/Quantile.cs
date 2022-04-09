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

    public static double Quartile(this System.Collections.Generic.List<double> source, double rthQuartile, double frequencyCount, double cumulativeFrequency)
    {
      return source[0] + ((rthQuartile * (source.Count / 4) - cumulativeFrequency) / frequencyCount) * (source[source.Count - 1] - source[0]);
    }

    /// <summary>Estimating quantiles from a sample.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile"/>
    public static double Quantile(this System.Collections.Generic.List<double> source, double p, QuantileType type)
      => type switch
      {
        QuantileType.R1 => QuantileR1(source, p),
        QuantileType.R2 => QuantileR2(source, p),
        QuantileType.R3 => QuantileR3(source, p),
        QuantileType.R4 => QuantileR4(source, p),
        QuantileType.R5 => QuantileR5(source, p),
        QuantileType.R6 => QuantileR6(source, p),
        QuantileType.R7 => QuantileR7(source, p),
        QuantileType.R8 => QuantileR8(source, p),
        QuantileType.R9 => QuantileR9(source, p),
        _ => throw new System.ArgumentOutOfRangeException(nameof(type)),
      };
    /// <summary>Estimating quantiles from a sample.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile"/>
    public static double Quantile(this System.Collections.Generic.List<int> source, double p, QuantileType type)
      => Quantile(System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(source, int32 => (double)int32)), p, type);

    private static double Quantile_Value(System.Collections.Generic.List<double> zeroBased, int oneBasedIndex)
      => zeroBased[System.Math.Clamp(oneBasedIndex - 1, 0, zeroBased.Count - 1)];

    private static double Quantile_EmpiricalDistributionFunction(System.Collections.Generic.List<double> x, double h)
    {
      var lo = System.Convert.ToInt32(System.Math.Floor(h));
      var hi = System.Convert.ToInt32(System.Math.Ceiling(h));

      return Quantile_Value(x, lo) + (h - lo) * (Quantile_Value(x, hi) - Quantile_Value(x, lo));
    }

    /// <summary>Inverse of empirical distribution function.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static double QuantileR1(this System.Collections.Generic.List<double> source, double p)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      return Quantile_Value(source, System.Convert.ToInt32(System.Math.Ceiling(source.Count * p)));
    }

    /// <summary>The same as R-1, but with averaging at discontinuities.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static double QuantileR2(this System.Collections.Generic.List<double> source, double p)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      var h = source.Count * p + 0.5;

      return (Quantile_Value(source, System.Convert.ToInt32(System.Math.Ceiling(h - 0.5))) + Quantile_Value(source, System.Convert.ToInt32(System.Math.Floor(h + 0.5)))) / 2;
    }

    /// <summary>The observation numbered closest to Np. Here, h indicates rounding to the nearest integer, choosing the even integer in the case of a tie.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static double QuantileR3(this System.Collections.Generic.List<double> source, double p)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      return Quantile_Value(source, System.Convert.ToInt32(System.Math.Round(source.Count * p - 0.5, System.MidpointRounding.ToEven)));
    }

    /// <summary>Linear interpolation of the empirical distribution function.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static double QuantileR4(this System.Collections.Generic.List<double> source, double p)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      return Quantile_EmpiricalDistributionFunction(source, source.Count * p);
    }

    /// <summary>Piecewise linear function where the knots are the values midway through the steps of the empirical distribution function.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static double QuantileR5(this System.Collections.Generic.List<double> source, double p)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      return Quantile_EmpiricalDistributionFunction(source, source.Count * p + 0.5);
    }

    /// <summary>Linear interpolation of the expectations for the order statistics for the uniform distribution on [0,1]. That is, it is the linear interpolation between points (ph, xh), where ph = h/(N+1) is the probability that the last of (N+1) randomly drawn values will not exceed the h-th smallest of the first N randomly drawn values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static double QuantileR6(this System.Collections.Generic.List<double> source, double p)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      return Quantile_EmpiricalDistributionFunction(source, (source.Count + 1) * p);
    }

    /// <summary>Linear interpolation of the modes for the order statistics for the uniform distribution on [0, 1].</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static double QuantileR7(this System.Collections.Generic.List<double> source, double p)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      return Quantile_EmpiricalDistributionFunction(source, (source.Count - 1) * p + 1);
    }

    /// <summary>Linear interpolation of the approximate medians for order statistics.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static double QuantileR8(this System.Collections.Generic.List<double> source, double p)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      return Quantile_EmpiricalDistributionFunction(source, (source.Count + 1 / 3) * p + 1 / 3);
    }

    /// <summary>The resulting quantile estimates are approximately unbiased for the expected order statistics if x is normally distributed.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static double QuantileR9(this System.Collections.Generic.List<double> source, double p)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      return Quantile_EmpiricalDistributionFunction(source, (source.Count + 1 / 4) * p + 3 / 8);
    }
  }
}
