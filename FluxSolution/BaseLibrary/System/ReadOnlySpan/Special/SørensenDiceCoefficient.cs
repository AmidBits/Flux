namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>The S�rensen�Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</para>
    /// <see href="https://en.wikipedia.org/wiki/S�rensen�Dice_coefficient"/>
    /// </summary>
    public static double S�rensenDiceCoefficient<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => 2d * (double)source.Intersect(target, equalityComparer).Count / (double)(source.Length + target.Length);
  }
}
