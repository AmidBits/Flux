namespace Flux
{
  /// <summary>
  /// <para>The strategy of rounding to the nearest number, and either when a number is halfway between two others or directly to one of two others.</para>
  /// <para><seealso href="http://www.jackleitch.net/2010/06/adventures-in-net-rounding-part-2-exotic-rounding-algorithms/"/></para>
  /// <para><seealso href="http://www.cplusplus.com/articles/1UCRko23/"/></para>
  /// </summary>
  public enum DistanceBasedRounding
  {
    ToEven = System.MidpointRounding.ToEven,
    AwayFromZero = System.MidpointRounding.AwayFromZero,
    ToZero = System.MidpointRounding.ToZero,
    ToNegativeInfinity = System.MidpointRounding.ToNegativeInfinity,
    ToPositiveInfinity = System.MidpointRounding.ToPositiveInfinity,
    ToOdd,
  }
}
