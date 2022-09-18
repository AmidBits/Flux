#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Number
  {
    /// <summary>PREVIEW! Find the numbers closest to the number and divisible by another number.</summary>
    public static (TSelf larger, TSelf smaller) ClosestToAndDivisibleBy<TSelf>(this TSelf closestTo, TSelf divisibleBy)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      var quotient = closestTo / divisibleBy;

      var n1 = divisibleBy * quotient; // 1st possible closest number
      var n2 = (closestTo * divisibleBy) > TSelf.Zero ? (divisibleBy * (quotient + TSelf.One)) : (divisibleBy * (quotient - TSelf.One)); // 2nd possible closest number

      return (TSelf.Abs(closestTo - n1) < TSelf.Abs(closestTo - n2)) ? (n1, n2) : (n2, n1);
    }
  }
}
#endif
