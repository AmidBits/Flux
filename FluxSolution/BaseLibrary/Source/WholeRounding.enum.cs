namespace Flux
{
  /// <summary>
  /// <para>The strategy of rounding to a number, when a number is anywhere between two others.</para>
  /// <para><seealso href="http://www.jackleitch.net/2010/06/adventures-in-net-rounding-part-2-exotic-rounding-algorithms/"/></para>
  /// <para><seealso href="http://www.cplusplus.com/articles/1UCRko23/"/></para>
  /// </summary>
  public enum WholeRounding
  {
    ToEven = 100, // There is no built-in rounding of this kind.

    /// <summary>
    /// <para>Round to the number away-from-zero.</para>
    /// </summary>
    /// <remarks>
    /// <para>This mode is the opposite of truncating.</para>
    /// <para>Symmetric rounding: round up, bias: away from zero.</para>
    /// </remarks>
    AwayFromZero = 101,

    /// <summary>
    /// <para>Round to the number towards-zero.</para>
    /// </summary>
    /// <remarks>
    /// <para>This is the same as Math.Truncate of the number.</para>
    /// <para>Symmetric rounding: round down, bias: towards zero.</para>
    /// </remarks>
    TowardZero = 102,

    /// <summary>
    /// <para>Round down to the number less-than-or-equal to value.</para>
    /// </summary>
    /// <remarks>
    /// <para>This is the same as Math.Floor of the number.</para>
    /// <para>Common rounding: round down, bias: negative infinity.</para>
    /// </remarks>
    ToNegativeInfinity = 103,

    /// <summary>
    /// <para>Round up to the number greater-than-or-equal to value.</para>
    /// </summary>
    /// <remarks>
    /// <para>This is the same as Math.Ceiling of the number.</para>
    /// <para>Common rounding: round up, bias: positive infinity.</para>
    /// </remarks>
    ToPositiveInfinity = 104,

    /// <summary>
    /// <para>Round to down or up randomly.</para>
    /// </summary>
    /// <remarks>
    /// <para>Random rounding: down or up, bias: none (though the RNG might be).</para>
    /// </remarks>
    ToRandom = 108, // There is no built-in rounding of this kind.

    Alternating = 109, // There is no built-in rounding of this kind.

    ToOdd = 110, // There is no built-in rounding of this kind.
  }
}
