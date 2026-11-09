namespace Flux
{
  public static partial class DecimalFloatingPointIeee754
  {
#if NET11_0_OR_GREATER

    /// <summary>
    /// <para>Table of Pow10 reciprocals up to 10^33.</para>
    /// </summary>
    internal static readonly System.Numerics.Decimal128[] Pow10Reciprocal = //[.. Number.GeometricSequence(System.Numerics.Decimal128.One, 10m).Where(d => d < System.Numerics.Decimal128.MaxExactInteger).Select(d => 1m / d)];
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

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IDecimalFloatingPointIeee754<TFloat>
    {
    
    }

#endif
  }
}