namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Rounds the <paramref name="number"/> to the nearest boundary according to the strategy <paramref name="mode"/>.</para>
    /// <remark>Use .NET built-in functionality when possible.</remark>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TNearest"></typeparam>
    /// <param name="number"></param>
    /// <param name="mode"></param>
    /// <param name="nearestTowardZero"></param>
    /// <param name="nearestAwayFromZero"></param>
    /// <returns></returns>
    public static TNearest RoundToNearest<TNumber, TNearest>(this TNumber number, UniversalRounding mode, TNearest nearestTowardZero, TNearest nearestAwayFromZero)
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
        UniversalRounding.WholeToRandom => number.RoundToNearestRandom(nearestTowardZero, nearestAwayFromZero),
        UniversalRounding.WholeToEven => TNearest.IsEvenInteger(nearestTowardZero) ? nearestTowardZero : nearestAwayFromZero,
        UniversalRounding.WholeAlternating => number.RoundToNearestAlternating(nearestTowardZero, nearestAwayFromZero),
        UniversalRounding.WholeToOdd => TNearest.IsOddInteger(nearestTowardZero) ? nearestTowardZero : nearestAwayFromZero,
        // Second we delegate to halfway-rounding strategies.
        _ => TNumber.Abs(number - TNumber.CreateChecked(nearestTowardZero)) is var distanceTowardsZero
          && TNumber.Abs(TNumber.CreateChecked(nearestAwayFromZero) - number) is var distanceAwayFromZero
          && (distanceTowardsZero < distanceAwayFromZero) ? nearestTowardZero // A clear win for towards-zero, no halfway-rounding needed.
          : (distanceAwayFromZero < distanceTowardsZero) ? nearestAwayFromZero // A clear win for away-from-zero, no halfway-rounding needed.
          : mode switch // If the distances are equal, i.e. exactly halfway, we use the appropriate rounding strategy to resolve a winner.
          {
            UniversalRounding.HalfToEven => TNearest.IsEvenInteger(nearestTowardZero) ? nearestTowardZero : nearestAwayFromZero,
            UniversalRounding.HalfAwayFromZero => nearestAwayFromZero,
            UniversalRounding.HalfTowardZero => nearestTowardZero,
            UniversalRounding.HalfToNegativeInfinity => TNearest.Min(nearestTowardZero, nearestAwayFromZero),
            UniversalRounding.HalfToPositiveInfinity => TNearest.Max(nearestTowardZero, nearestAwayFromZero),
            UniversalRounding.HalfToRandom => number.RoundToNearestRandom(nearestTowardZero, nearestAwayFromZero),
            UniversalRounding.HalfAlternating => number.RoundToNearestAlternating(nearestTowardZero, nearestAwayFromZero),
            UniversalRounding.HalfToOdd => TNearest.IsOddInteger(nearestAwayFromZero) ? nearestAwayFromZero : nearestTowardZero,
            _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
          }
      };

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="alternateStore"></param>
    /// <returns></returns>
    public static TNearest RoundToNearestAlternating<TValue, TNearest>(this TValue value, TNearest nearestTowardZero, TNearest nearestAwayFromZero, ref bool alternateStore)
      where TValue : System.Numerics.INumber<TValue>
      where TNearest : System.Numerics.INumber<TNearest>
      => (alternateStore = !alternateStore)
      ? nearestTowardZero
      : nearestAwayFromZero;

    private static bool m_roundNearestAlternatingState; // This is a field used for the method below.

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TNearest RoundToNearestAlternating<TValue, TNearest>(this TValue value, TNearest nearestTowardZero, TNearest nearestAwayFromZero)
      where TValue : System.Numerics.INumber<TValue>
      where TNearest : System.Numerics.INumber<TNearest>
      => value.RoundToNearestAlternating(nearestTowardZero, nearestAwayFromZero, ref m_roundNearestAlternatingState);

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="HalfRounding.HalfToRandom"/></para>
    /// </remarks>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="rng"></param>
    /// <returns></returns>
    public static TNearest RoundToNearestRandom<TValue, TNearest>(this TValue value, TNearest nearestTowardZero, TNearest nearestAwayFromZero, System.Random? rng = null)
      where TValue : System.Numerics.INumber<TValue>
      where TNearest : System.Numerics.INumber<TNearest>
      => (rng ?? System.Random.Shared).NextDouble() < 0.5
      ? nearestTowardZero
      : nearestAwayFromZero;
  }
}
