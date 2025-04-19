namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Rounds the <paramref name="value"/> to the nearest boundary according to the strategy <paramref name="mode"/>.</para>
    /// <remark>Use .NET built-in functionality when possible.</remark>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TNearest"></typeparam>
    /// <param name="value"></param>
    /// <param name="mode"></param>
    /// <param name="nearestTowardZero"></param>
    /// <param name="nearestAwayFromZero"></param>
    /// <returns></returns>
    public static TNearest RoundToNearest<TNumber, TNearest>(this TNumber value, UniversalRounding mode, TNearest nearestTowardZero, TNearest nearestAwayFromZero)
      where TNumber : System.Numerics.INumber<TNumber>
      where TNearest : System.Numerics.INumber<TNearest>
      => nearestTowardZero == nearestAwayFromZero ? nearestTowardZero // If the two boundaries are equal, it's the one.
      : mode switch
      {
        // First we take care of the whole rounding cases.
        UniversalRounding.WholeAwayFromZero => nearestAwayFromZero,
        UniversalRounding.WholeTowardZero => nearestTowardZero,
        UniversalRounding.WholeToNegativeInfinity => TNearest.Min(nearestTowardZero, nearestAwayFromZero),
        UniversalRounding.WholeToPositiveInfinity => TNearest.Max(nearestTowardZero, nearestAwayFromZero),
        UniversalRounding.WholeToRandom => value.RoundToNearestRandom(nearestTowardZero, nearestAwayFromZero),
        UniversalRounding.WholeToEven => TNearest.IsEvenInteger(nearestTowardZero) ? nearestTowardZero : nearestAwayFromZero,
        UniversalRounding.WholeAlternating => value.RoundToNearestAlternating(nearestTowardZero, nearestAwayFromZero),
        UniversalRounding.WholeToOdd => TNearest.IsOddInteger(nearestTowardZero) ? nearestTowardZero : nearestAwayFromZero,
        // Second we delegate to halfway-rounding strategies.
        _ => TNumber.Abs(value - TNumber.CreateChecked(nearestTowardZero)) is var distanceTowardsZero
          && TNumber.Abs(TNumber.CreateChecked(nearestAwayFromZero) - value) is var distanceAwayFromZero
          && (distanceTowardsZero < distanceAwayFromZero) ? nearestTowardZero // A clear win for towards-zero, no halfway-rounding needed.
          : (distanceAwayFromZero < distanceTowardsZero) ? nearestAwayFromZero // A clear win for away-from-zero, no halfway-rounding needed.
          : mode switch // If the distances are equal, i.e. exactly halfway, we use the appropriate rounding strategy to resolve a winner.
          {
            UniversalRounding.HalfToEven => TNearest.IsEvenInteger(nearestTowardZero) ? nearestTowardZero : nearestAwayFromZero,
            UniversalRounding.HalfAwayFromZero => nearestAwayFromZero,
            UniversalRounding.HalfTowardZero => nearestTowardZero,
            UniversalRounding.HalfToNegativeInfinity => TNearest.Min(nearestTowardZero, nearestAwayFromZero),
            UniversalRounding.HalfToPositiveInfinity => TNearest.Max(nearestTowardZero, nearestAwayFromZero),
            UniversalRounding.HalfToRandom => value.RoundToNearestRandom(nearestTowardZero, nearestAwayFromZero),
            UniversalRounding.HalfAlternating => value.RoundToNearestAlternating(nearestTowardZero, nearestAwayFromZero),
            UniversalRounding.HalfToOdd => TNearest.IsOddInteger(nearestAwayFromZero) ? nearestAwayFromZero : nearestTowardZero,
            _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
          }
      };

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="customState"></param>
    /// <returns></returns>
    public static TNearest RoundToNearestAlternating<TNumber, TNearest>(this TNumber value, TNearest nearestTowardZero, TNearest nearestAwayFromZero, ref bool customState)
      where TNumber : System.Numerics.INumber<TNumber>
      where TNearest : System.Numerics.INumber<TNearest>
      => (customState = !customState)
      ? nearestTowardZero
      : nearestAwayFromZero;

    private static bool m_roundNearestAlternatingState; // This is a field used for the method below.

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TNearest RoundToNearestAlternating<TNumber, TNearest>(this TNumber value, TNearest nearestTowardZero, TNearest nearestAwayFromZero)
      where TNumber : System.Numerics.INumber<TNumber>
      where TNearest : System.Numerics.INumber<TNearest>
      => value.RoundToNearestAlternating(nearestTowardZero, nearestAwayFromZero, ref m_roundNearestAlternatingState);

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="HalfRounding.HalfToRandom"/></para>
    /// </remarks>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="rng"></param>
    /// <returns></returns>
    public static TNearest RoundToNearestRandom<TNumber, TNearest>(this TNumber value, TNearest nearestTowardZero, TNearest nearestAwayFromZero, System.Random? rng = null)
      where TNumber : System.Numerics.INumber<TNumber>
      where TNearest : System.Numerics.INumber<TNearest>
      => (rng ?? System.Random.Shared).NextDouble() < 0.5
      ? nearestTowardZero
      : nearestAwayFromZero;
  }
}
