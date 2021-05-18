namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Compute the ordinal rank of the P-th percentile by means of nearest rank.</summary>
    public static int PercentileNearestRank(int percentile, int count)
      => percentile > 0 && percentile <= 100
      ? (int)System.Math.Ceiling(percentile / 100.0 * count)
      : throw new System.ArgumentOutOfRangeException(nameof(percentile));

    /// <summary>Compute the ordinal rank of the P-th percentile by means of linear interpolation.</summary>
    public static double PercentileRankLerp(double percentile, int count)
      => percentile >= 0 && percentile <= 100
      ? percentile / 100.0 * (count - 1) + 1
      : throw new System.ArgumentOutOfRangeException(nameof(percentile));
  }
}
