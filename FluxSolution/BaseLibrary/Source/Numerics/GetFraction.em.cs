namespace Flux
{
  public static partial class ExtensionMethodsNumerics
  {
    /// <summary>Strips the integer part of the floating point value, resulting in only the fractional part.</summary>
    public static TSelf GetFraction<TSelf, TInteger>(this TSelf source, out TInteger integer)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var whole = TSelf.Truncate(source);

      integer = TInteger.CreateChecked(whole);

      return source - whole;
    }
  }
}
