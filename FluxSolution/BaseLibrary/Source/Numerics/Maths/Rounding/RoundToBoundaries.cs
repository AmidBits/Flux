namespace Flux
{
  public static partial class Maths
  {
    /// <summary>
    /// <para>Rounds the <paramref name="source"/> to the nearest boundary according to the strategy <paramref name="mode"/>. The mode specifies how to round when halfway between two boundaries.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TBound"></typeparam>
    /// <typeparam name="TDistance"></typeparam>
    /// <param name="source"></param>
    /// <param name="mode"></param>
    /// <param name="boundaryTowardsZero"></param>
    /// <param name="boundaryAwayFromZero"></param>
    /// <param name="distanceTowardsZero"></param>
    /// <param name="distanceAwayFromZero"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TBound RoundToBoundaries<TValue, TBound, TDistance>(this TValue source, RoundingMode mode, TBound boundaryTowardsZero, TBound boundaryAwayFromZero, TDistance distanceTowardsZero, TDistance distanceAwayFromZero)
      where TValue : System.Numerics.INumber<TValue>
      where TBound : System.Numerics.INumber<TBound>
      where TDistance : System.Numerics.INumber<TDistance>
      => mode switch
      {
        // First we take care of the direct rounding cases.
        RoundingMode.AwayFromZero => boundaryAwayFromZero,
        RoundingMode.TowardsZero => boundaryTowardsZero,
        RoundingMode.ToNegativeInfinity => TValue.IsNegative(source) ? boundaryAwayFromZero : boundaryTowardsZero,
        RoundingMode.ToPositiveInfinity => TValue.IsNegative(source) ? boundaryTowardsZero : boundaryAwayFromZero,
        // If not applicable, and since we're comparing a value against two boundaries, if the distances from the value to the two boundaries are not equal, we can avoid halfway checks.
        _ => (distanceTowardsZero < distanceAwayFromZero) ? boundaryTowardsZero // A clear win for towards-zero.
          : (distanceTowardsZero > distanceAwayFromZero) ? boundaryAwayFromZero // A clear win for away-from-zero.
          : mode switch // If the distances are equal, i.e. exactly halfway, we use the appropriate rounding strategy to resolve a winner.
          {
            RoundingMode.HalfToEven => TBound.IsEvenInteger(boundaryTowardsZero) ? boundaryTowardsZero : boundaryAwayFromZero,
            RoundingMode.HalfAwayFromZero => boundaryAwayFromZero,
            RoundingMode.HalfTowardZero => boundaryTowardsZero,
            RoundingMode.HalfToNegativeInfinity => TValue.IsNegative(source) ? boundaryAwayFromZero : boundaryTowardsZero,
            RoundingMode.HalfToPositiveInfinity => TValue.IsNegative(source) ? boundaryTowardsZero : boundaryAwayFromZero,
            RoundingMode.HalfToOdd => TBound.IsOddInteger(boundaryAwayFromZero) ? boundaryAwayFromZero : boundaryTowardsZero,
            _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
          }
      };

    /// <summary>
    /// <para>Rounds the <paramref name="source"/> to either of <paramref name="boundaryTowardsZero"/> and <paramref name="boundaryAwayFromZero"/> according to the strategy <paramref name="mode"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="source"></param>
    /// <param name="mode"></param>
    /// <param name="boundaryTowardsZero"></param>
    /// <param name="boundaryAwayFromZero"></param>
    /// <returns></returns>
    public static TBound RoundToBoundaries<TValue, TBound>(this TValue source, RoundingMode mode, TBound boundaryTowardsZero, TBound boundaryAwayFromZero)
      where TValue : System.Numerics.INumber<TValue>
      where TBound : System.Numerics.INumber<TBound>
    {
      ComputeDistanceToBoundaries(source, boundaryTowardsZero, boundaryAwayFromZero, out TValue distanceTowardsZero, out TValue distanceAwayFromZero);

      return RoundToBoundaries(source, mode, boundaryTowardsZero, boundaryAwayFromZero, distanceTowardsZero, distanceAwayFromZero);
    }

    /// <summary>
    /// <para>Computes the distances <paramref name="distanceTowardsZero"/>/<paramref name="distanceAwayFromZero"/> as out parameters, which are the distances between <paramref name="value"/> and <paramref name="boundaryTowardsZero"/>/<paramref name="boundaryAwayFromZero"/> respectively.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TBound"></typeparam>
    /// <typeparam name="TDistance"></typeparam>
    /// <param name="value">The value to consider, in relation to the boundaries.</param>
    /// <param name="boundaryTowardsZero">The boundary closer to zero (positive or negative).</param>
    /// <param name="boundaryAwayFromZero">The boundary farther from zero (positive or negative).</param>
    /// <param name="distanceTowardsZero">Out parameter with the distance between <paramref name="value"/> and <paramref name="boundaryTowardsZero"/>.</param>
    /// <param name="distanceAwayFromZero">Out parameter with the distance between <paramref name="value"/> to <paramref name="boundaryAwayFromZero"/>.</param>
    /// <returns>Whether <paramref name="distanceTowardsZero"/> or <paramref name="distanceAwayFromZero"/> are within <paramref name="maxDistanceToBoundaries"/>.</returns>
    public static void ComputeDistanceToBoundaries<TValue, TBound, TDistance>(this TValue value, TBound boundaryTowardsZero, TBound boundaryAwayFromZero, out TDistance distanceTowardsZero, out TDistance distanceAwayFromZero)
      where TValue : System.Numerics.INumber<TValue>
      where TBound : System.Numerics.INumber<TBound>
      where TDistance : System.Numerics.INumber<TDistance>
    {
      var origin = TDistance.CreateChecked(value);

      distanceTowardsZero = TDistance.Abs(origin - TDistance.CreateChecked(boundaryTowardsZero)); // Distance from value to the boundary towardsZero.
      distanceAwayFromZero = TDistance.Abs(TDistance.CreateChecked(boundaryAwayFromZero) - origin); // Distance from value to the boundary awayFromZero;
    }
  }
}
