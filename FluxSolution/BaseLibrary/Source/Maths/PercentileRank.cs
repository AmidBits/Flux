namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Compute the percentile rank of the percentile by means of nearest.</summary>
    public static int PercentileNearestRank(int count, double percentile)
      => (percentile > 0 && percentile <= 100
      ? (int)System.Math.Ceiling(percentile / 100 * count)
      : throw new System.ArgumentOutOfRangeException(nameof(percentile));
    /// <summary>Compute the percentile rank of the percentile by means of nearest.</summary>
    public static int PercentileNearestRank(long count, double percentile)
      => (percentile > 0 && percentile <= 100
      ? (int)System.Math.Ceiling(percentile / 100 * count)
      : throw new System.ArgumentOutOfRangeException(nameof(percentile));

    /// <summary>Compute the percentile rank of the percentile by means of linear interpolation.</summary>
    public static double PercentileRankLerp(int count, double percentile)
      => percentile >= 0 && percentile <= 100
      ? percentile / 100 * (count - 1) + 1
      : throw new System.ArgumentOutOfRangeException(nameof(percentile));
    /// <summary>Compute the percentile rank of the percentile by means of linear interpolation.</summary>
    public static double PercentileRankLerp(long count, double percentile)
      => percentile >= 0 && percentile <= 100
      ? percentile / 100 * (count - 1) + 1
      : throw new System.ArgumentOutOfRangeException(nameof(percentile));
  }
}
