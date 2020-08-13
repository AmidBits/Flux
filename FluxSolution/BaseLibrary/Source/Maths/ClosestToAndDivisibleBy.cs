namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Find the number closest to a number and divisible by another number.</summary>
    public static System.Numerics.BigInteger ClosestToAndDivisibleBy(System.Numerics.BigInteger closestTo, System.Numerics.BigInteger divisibleBy)
    {
      var quotient = closestTo / divisibleBy;

      var n1 = divisibleBy * quotient; // 1st possible closest number

      var n2 = (closestTo * divisibleBy) > 0 ? (divisibleBy * (quotient + 1)) : (divisibleBy * (quotient - 1)); // 2nd possible closest number

      return (System.Numerics.BigInteger.Abs(closestTo - n1) < System.Numerics.BigInteger.Abs(closestTo - n2)) ? n1 : n2;
    }

    /// <summary>Find the number closest to a number and divisible by another number.</summary>
    public static int ClosestToAndDivisibleBy(int closestTo, int divisibleBy)
    {
      var quotient = closestTo / divisibleBy;

      var n1 = divisibleBy * quotient; // 1st possible closest number

      var n2 = (closestTo * divisibleBy) > 0 ? (divisibleBy * (quotient + 1)) : (divisibleBy * (quotient - 1)); // 2nd possible closest number

      return (System.Math.Abs(closestTo - n1) < System.Math.Abs(closestTo - n2)) ? n1 : n2;
    }
    /// <summary>Find the number closest to a number and divisible by another number.</summary>
    public static long ClosestToAndDivisibleBy(long closestTo, long divisibleBy)
    {
      var quotient = closestTo / divisibleBy;

      var n1 = divisibleBy * quotient; // 1st possible closest number

      var n2 = (closestTo * divisibleBy) > 0 ? (divisibleBy * (quotient + 1)) : (divisibleBy * (quotient - 1)); // 2nd possible closest number

      return (System.Math.Abs(closestTo - n1) < System.Math.Abs(closestTo - n2)) ? n1 : n2;
    }
  }
}
