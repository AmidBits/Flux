namespace Flux
{
  public static partial class Quantiles
  {
    /// <summary>Nine algorithms (estimate types and interpolation schemes(.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public enum QuantileType
    {
      /// <summary>Inverse of empirical distribution function.</summary>
      R1,
      /// <summary>The same as R-1, but with averaging at discontinuities.</summary>
      R2,
      /// <summary>The observation numbered closest to Np. Rounding to the nearest integer, choosing the even integer in the case of a tie.</summary>
      R3,
      /// <summary>Linear interpolation of the empirical distribution function.</summary>
      R4,
      /// <summary>Piecewise linear function where the knots are the values midway through the steps of the empirical distribution function.</summary>
      R5,
      /// <summary>Linear interpolation of the expectations for the order statistics for the uniform distribution on [0,1]. That is, it is the linear interpolation between points (ph, xh), where ph = h/(N+1) is the probability that the last of (N+1) randomly drawn values will not exceed the h-th smallest of the first N randomly drawn values.</summary>
      R6,
      /// <summary>Linear interpolation of the modes for the order statistics for the uniform distribution on [0,1].</summary>
      R7,
      /// <summary>Linear interpolation of the approximate medians for order statistics.</summary>
      R8,
      /// <summary>The resulting quantile estimates are approximately unbiased for the expected order statistics if x is normally distributed.</summary>
      R9,
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

    public static double EmpiricalDistributionFunction(System.Collections.Generic.List<double> source, double h)
    {
      var lo = System.Convert.ToInt32(System.Math.Floor(h));
      var hi = System.Convert.ToInt32(System.Math.Ceiling(h));

      var sourceCountM1 = source.Count - 1;

      var lov = source[System.Math.Clamp(lo - 1, 0, sourceCountM1)];
      var hiv = source[System.Math.Clamp(hi - 1, 0, sourceCountM1)];

      return lov + (h - lo) * (hiv - lov);
    }

    /// <summary>Inverse of empirical distribution function.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static double QuantileR1(this System.Collections.Generic.List<double> source, double p)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      var sourceCount = source.Count;
      var sourceCountM1 = sourceCount - 1;

      var index = System.Convert.ToInt32(System.Math.Ceiling(sourceCount * p)) - 1;

      return source[System.Math.Clamp(index, 0, sourceCountM1)];
    }

    /// <summary>The same as R-1, but with averaging at discontinuities.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static double QuantileR2(this System.Collections.Generic.List<double> source, double p)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      var sourceCount = source.Count;
      var sourceCountM1 = sourceCount - 1;

      var h = sourceCount * p + 0.5;

      var indexLo = System.Convert.ToInt32(System.Math.Ceiling(h - 0.5)) - 1;
      var indexHi = System.Convert.ToInt32(System.Math.Floor(h + 0.5)) - 1;

      return (source[System.Math.Clamp(indexLo, 0, sourceCountM1)] + source[System.Math.Clamp(indexHi, 0, sourceCountM1)]) / 2;
    }

    /// <summary>The observation numbered closest to Np. Here, h indicates rounding to the nearest integer, choosing the even integer in the case of a tie.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static double QuantileR3(this System.Collections.Generic.List<double> source, double p)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      var sourceCount = source.Count;
      var sourceCountM1 = sourceCount - 1;

      var index = System.Convert.ToInt32(System.Math.Round(sourceCount * p - 0.5, System.MidpointRounding.ToEven)) - 1;

      return source[System.Math.Clamp(index, 0, sourceCountM1)];
    }

    /// <summary>Linear interpolation of the empirical distribution function.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static double QuantileR4(this System.Collections.Generic.List<double> source, double p)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      return EmpiricalDistributionFunction(source, source.Count * p);
    }

    /// <summary>Piecewise linear function where the knots are the values midway through the steps of the empirical distribution function.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static double QuantileR5(this System.Collections.Generic.List<double> source, double p)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      return EmpiricalDistributionFunction(source, source.Count * p + 0.5);
    }

    /// <summary>Linear interpolation of the expectations for the order statistics for the uniform distribution on [0,1]. That is, it is the linear interpolation between points (ph, xh), where ph = h/(N+1) is the probability that the last of (N+1) randomly drawn values will not exceed the h-th smallest of the first N randomly drawn values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static double QuantileR6(this System.Collections.Generic.List<double> source, double p)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      return EmpiricalDistributionFunction(source, (source.Count + 1) * p);
    }

    /// <summary>Linear interpolation of the modes for the order statistics for the uniform distribution on [0, 1].</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static double QuantileR7(this System.Collections.Generic.List<double> source, double p)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      return EmpiricalDistributionFunction(source, (source.Count - 1) * p + 1);
    }

    /// <summary>Linear interpolation of the approximate medians for order statistics.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static double QuantileR8(this System.Collections.Generic.List<double> source, double p)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      return EmpiricalDistributionFunction(source, (source.Count + 1 / 3) * p + 1 / 3);
    }

    /// <summary>The resulting quantile estimates are approximately unbiased for the expected order statistics if x is normally distributed.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static double QuantileR9(this System.Collections.Generic.List<double> source, double p)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      return EmpiricalDistributionFunction(source, (source.Count + 1 / 4) * p + 3 / 8);
    }
  }
}
