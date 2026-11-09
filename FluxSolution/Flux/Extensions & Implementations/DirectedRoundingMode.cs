namespace Flux
{
  /// <summary>
  /// <para>Specifies the direction toward which values are rounded.</para>
  /// </summary>
  public enum DirectedRoundingMode
  {
    TowardNegativeInfinity = System.MidpointRounding.ToNegativeInfinity,
    TowardPositiveInfinity = System.MidpointRounding.ToPositiveInfinity,
    TowardZero = System.MidpointRounding.ToZero,
    AwayFromZero = System.MidpointRounding.AwayFromZero,
    Random = 23,
    Stochastic = 24
  }
}
