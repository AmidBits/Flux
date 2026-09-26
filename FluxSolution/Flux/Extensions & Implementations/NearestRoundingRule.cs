namespace Flux
{
  /// <summary>
  /// <para>Specifies how values exactly halfway between two candidates are rounded.</para>
  /// </summary>
  public enum NearestRoundingRule
  {
    TowardNegativeInfinity = System.MidpointRounding.ToNegativeInfinity,
    TowardPositiveInfinity = System.MidpointRounding.ToPositiveInfinity,
    TowardZero = System.MidpointRounding.ToZero,
    AwayFromZero = System.MidpointRounding.AwayFromZero,
    ToEven = System.MidpointRounding.ToEven,
    ToOdd = 17,
    Random = 19
  }
}
