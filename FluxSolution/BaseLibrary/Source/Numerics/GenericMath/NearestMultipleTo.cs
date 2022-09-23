#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Find the numbers closest to the number and divisible by another number.</summary>
    public static TSelf NearestMultipleTo<TSelf>(this TSelf value, TSelf multiple, out TSelf smaller, out TSelf larger)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      var quotient = value / multiple;

      smaller = multiple * quotient; // 1st possible closest number
      larger = (value * multiple) > TSelf.Zero ? (multiple * (quotient + TSelf.One)) : (multiple * (quotient - TSelf.One)); // 2nd possible closest number

      return (TSelf.Abs(value - smaller) < TSelf.Abs(value - larger)) ? smaller : larger;
    }
  }
}
#endif
