namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="source"/> number is a floating point.</para>
    /// <para><seealso href="https://stackoverflow.com/a/13609383/3178666"/></para>
    /// </summary>
    public static bool IsFloatingPoint<TSelf>(this TSelf source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source is System.Decimal
      || source is System.Double
      || source is System.Half
      || source is System.Runtime.InteropServices.NFloat
      || source is System.Single
      || typeof(TSelf).IsAssignableToGenericType(typeof(System.Numerics.IFloatingPoint<>)); // By being assignable to System.Numerics.IBinaryInteger<>.
  }
}
