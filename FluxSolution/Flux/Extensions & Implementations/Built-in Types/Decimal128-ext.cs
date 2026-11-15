namespace Flux
{
  public static partial class Decimal128Extensions
  {
    #region Pow10Reciprocal Table

    /// <summary>
    /// <para>Table of Pow10 reciprocals up to 10^33.</para>
    /// </summary>
    internal static readonly System.Numerics.Decimal128[] Pow10Reciprocal =
    [
      1m,
      0.1m,
      0.01m,
      0.001m,
      0.0001m,
      0.00001m,
      0.000001m,
      0.0000001m,
      0.00000001m,
      0.000000001m,
      0.0000000001m,
      0.00000000001m,
      0.000000000001m,
      0.0000000000001m,
      0.00000000000001m,
      0.000000000000001m,
      0.0000000000000001m,
      0.00000000000000001m,
      0.000000000000000001m,
      0.0000000000000000001m,
      0.00000000000000000001m,
      0.000000000000000000001m,
      0.0000000000000000000001m,
      0.00000000000000000000001m,
      0.000000000000000000000001m,
      0.0000000000000000000000001m,
      0.00000000000000000000000001m,
      0.000000000000000000000000001m,
      0.0000000000000000000000000001m,
      0.00000000000000000000000000001m,
      0.000000000000000000000000000001m,
      0.0000000000000000000000000000001m,
      0.00000000000000000000000000000001m,
      0.000000000000000000000000000000001m,
    ];

    #endregion

    extension(System.Numerics.Decimal128)
    {
      /// <summary>
      /// <para>Fast-path computation of power-of-10 reciprocals using precomputed values. Supported exponents are in the range [-28, 0], corresponding to powers from 10^-28 through 1.</para>
      /// </summary>
      /// <param name="negativeExponent">The power-of-10 exponent, in the range [-28, 0].</param>
      /// <returns></returns>
      public static System.Numerics.Decimal128 FastPow10ReciprocalMagnitude(int negativeExponent)
      {
        var values = Pow10Reciprocal;

        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(negativeExponent, -values.Length);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(negativeExponent, 0);

        return values[int.Abs(negativeExponent)];
      }

      /// <summary>
      /// <para>Attempts fast-path computation of power-of-10 reciprocals from precomputed values. Supported exponents are in the range [-28, 0], corresponding to powers from 10^-28 through 1.</para>
      /// </summary>
      /// <param name="negativeExponent">The power-of-10 exponent, in the range [-28, 0].</param>
      /// <param name="pow10Reciprocal"></param>
      /// <returns>true if the value was computed successfully; otherwise, false.</returns>
      public static bool TryFastPow10ReciprocalMagnitude(int negativeExponent, out System.Numerics.Decimal128 pow10Reciprocal)
      {
        var values = Pow10Reciprocal;

        if (negativeExponent <= -values.Length || negativeExponent > 0)
        {
          pow10Reciprocal = System.Numerics.Decimal128.Zero;
          return false;
        }

        pow10Reciprocal = values[int.Abs(negativeExponent)];
        return true;
      }
    }
  }
}
