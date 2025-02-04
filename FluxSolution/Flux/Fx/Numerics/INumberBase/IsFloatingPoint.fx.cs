namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="numberBase"/> number is a floating point.</para>
    /// <para><seealso href="https://stackoverflow.com/a/13609383/3178666"/></para>
    /// </summary>
    public static bool IsFloatingPoint<TNumberBase>(this TNumberBase numberBase)
      where TNumberBase : System.Numerics.INumberBase<TNumberBase>
      => numberBase is System.Decimal
      || numberBase is System.Double
      || numberBase is System.Half
      || numberBase is System.Runtime.InteropServices.NFloat
      || numberBase is System.Single
      || typeof(TNumberBase).IsAssignableToGenericType(typeof(System.Numerics.IFloatingPoint<>)); // By being assignable to System.Numerics.IBinaryInteger<>.
  }
}
