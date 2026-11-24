namespace Flux
{
  public static partial class Decimal128Extensions
  {
    #region Pow10Reciprocal table

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
      #region Try/FastPow10ReciprocalMagnitude

      /// <summary>
      /// <para>Fast-path power-of-10 reciprocals from precomputed values. Supported exponent magnitudes are in the range [0, 28], corresponding to powers from 1 through 10^-28.</para>
      /// </summary>
      /// <param name="exponent">The exponent magnitude in the range [0, 28].</param>
      /// <returns>10^-<paramref name="exponent"/></returns>
      public static System.Numerics.Decimal128 FastPow10ReciprocalMagnitude(int exponent)
      {
        var values = Pow10Reciprocal;

        var absExponent = int.Abs(exponent);

        if (absExponent >= values.Length) throw new System.ArgumentOutOfRangeException(nameof(exponent));

        return values[absExponent];
      }

      /// <summary>
      /// <para>Attempts fast-path power-of-10 reciprocals from precomputed values. Supported exponent magnitudes are in the range [0, 28], corresponding to powers from 1 through 10^-28.</para>
      /// </summary>
      /// <param name="exponent">The exponent magnitude in the range [0, 28].</param>
      /// <param name="reciprocal">10^-<paramref name="exponent"/></param>
      /// <returns>true if the value was computed successfully; otherwise, false.</returns>
      public static bool TryFastPow10ReciprocalMagnitude(int exponent, out System.Numerics.Decimal128 reciprocal)
      {
        var values = Pow10Reciprocal;

        var absExponent = int.Abs(exponent);

        if (absExponent >= values.Length)
        {
          reciprocal = System.Numerics.Decimal128.Zero;
          return false;
        }

        reciprocal = values[absExponent];
        return true;
      }

      #endregion
    }
  }
}
