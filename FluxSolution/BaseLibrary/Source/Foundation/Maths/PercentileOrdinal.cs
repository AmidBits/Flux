namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Compute the ordinal index (rank) of the P-th percentile by means of nearest rank.</summary>
    public static int PercentileOrdinalNearest(int percentile, int count)
      => count < 0
      ? throw new System.ArgumentOutOfRangeException(nameof(count))
      : percentile < 0 || percentile > 100
      ? throw new System.ArgumentOutOfRangeException(nameof(percentile))
      : (int)System.Math.Ceiling(percentile / 100.0 * count);

    /// <summary>Compute the ordinal index (rank) of the P-th percentile by means of linear interpolation.</summary>
    /// <param name="count">The number of items in the set.</param>
    /// <param name="percentile">The percentile to find the index for.</param>
    public static double PercentileOrdinalLerp(double percentile, int count)
      => count < 0
      ? throw new System.ArgumentOutOfRangeException(nameof(count))
      : percentile < 0 || percentile > 100
      ? throw new System.ArgumentOutOfRangeException(nameof(percentile))
      : percentile / 100.0 * (count - 1) + 1;
  }
}