//namespace Flux.Statistics.Quantile
//{
//  /// <summary>Nine algorithms (estimate types and interpolation schemes).</summary>
//  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
//  public enum QuantileAlgorithm
//  {
//    /// <summary>Quantile Empirical Distribution Function.</summary>
//    EDF,
//    /// <summary>Inverse of empirical distribution function.</summary>
//    R1,
//    /// <summary>The same as R1, but with averaging at discontinuities.</summary>
//    R2,
//    /// <summary>The observation numbered closest to Np. Rounding to the nearest integer, choosing the even integer in the case of a tie.</summary>
//    R3,
//    /// <summary>Linear interpolation of the empirical distribution function.</summary>
//    R4,
//    /// <summary>Piecewise linear function where the knots are the values midway through the steps of the empirical distribution function.</summary>
//    R5,
//    /// <summary>Linear interpolation of the expectations for the order statistics for the uniform distribution on [0,1]. That is, it is the linear interpolation between points (ph, xh), where ph = h/(N+1) is the probability that the last of (N+1) randomly drawn values will not exceed the h-th smallest of the first N randomly drawn values.</summary>
//    R6,
//    /// <summary>Linear interpolation of the modes for the order statistics for the uniform distribution on [0,1].</summary>
//    R7,
//    /// <summary>Linear interpolation of the approximate medians for order statistics.</summary>
//    R8,
//    /// <summary>The resulting quantile estimates are approximately unbiased for the expected order statistics if x is normally distributed.</summary>
//    R9,
//  }
//}
