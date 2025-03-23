namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="numberBase"/> number is a floating point.</para>
    /// <para><seealso href="https://stackoverflow.com/a/13609383/3178666"/></para>
    /// </summary>
    /// <typeparam name="TNumberBase"></typeparam>
    /// <param name="numberBase"></param>
    /// <returns></returns>
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
