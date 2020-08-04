namespace Flux
{
  public static partial class Math
  {
    /// <summary>Compute the percentile rank of the percentile by means of nearest.</summary>
    public static int PercentileNearestRank(int count, double percentile)
      => (int)System.Math.Ceiling((percentile > 0 && percentile <= 100 ? percentile : throw new System.ArgumentOutOfRangeException(nameof(percentile))) / 100 * count);
    /// <summary>Compute the percentile rank of the percentile by means of nearest.</summary>
    public static int PercentileNearestRank(long count, double percentile)
      => (int)System.Math.Ceiling((percentile > 0 && percentile <= 100 ? percentile : throw new System.ArgumentOutOfRangeException(nameof(percentile))) / 100 * count);

    /// <summary>Compute the percentile rank of the percentile by means of linear interpolation.</summary>
    public static double PercentileRankLerp(int count, double percentile)
      => (percentile >= 0 && percentile <= 100 ? percentile : throw new System.ArgumentOutOfRangeException(nameof(percentile))) / 100 * (count - 1) + 1;
    /// <summary>Compute the percentile rank of the percentile by means of linear interpolation.</summary>
    public static double PercentileRankLerp(long count, double percentile)
      => (percentile >= 0 && percentile <= 100 ? percentile : throw new System.ArgumentOutOfRangeException(nameof(percentile))) / 100 * (count - 1) + 1;
  }
}
