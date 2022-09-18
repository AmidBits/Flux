#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class FloatingPoint
  {
    /// <summary>PREVIEW! Returns the normalized form sinc of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sinc_function"/>
    public static TNumber ConvertTo<TSelf, TNumber>(this TSelf number, out TNumber result)
      where TSelf : System.Numerics.INumber<TSelf>
      where TNumber : System.Numerics.INumber<TNumber>
      => result = TNumber.CreateChecked(number);
  }
}
#endif
