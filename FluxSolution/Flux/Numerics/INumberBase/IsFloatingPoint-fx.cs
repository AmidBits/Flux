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
      => numberBase
      is System.Decimal
      or System.Double
      or System.Half
      or System.Runtime.InteropServices.NFloat
      or System.Single
      // Or type is-assignable-to System.Numerics.IBinaryInteger<>.
      || typeof(TNumberBase).IsAssignableToGenericType(typeof(System.Numerics.IFloatingPoint<>));
  }
}
