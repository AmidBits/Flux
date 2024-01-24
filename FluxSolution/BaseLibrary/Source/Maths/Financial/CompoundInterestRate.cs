namespace Flux
{
  public static partial class Financial
  {
    /// <summary>Compound interest is the addition of interest to the principal sum of a loan or deposit, or in other words, interest on interest.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Compound_interest"/>
#if NET7_0_OR_GREATER
    public static TSelf CompoundInterestRate<TSelf>(this TSelf nominalInterestRate, TSelf compoundingPeriodsPerYear, TSelf numberOfYears)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => TSelf.Pow(TSelf.One + nominalInterestRate / compoundingPeriodsPerYear, compoundingPeriodsPerYear * numberOfYears);
#else
    public static double CompoundInterestRate(this double nominalInterestRate, double compoundingPeriodsPerYear, double numberOfYears)
      => System.Math.Pow(1 + nominalInterestRate / compoundingPeriodsPerYear, compoundingPeriodsPerYear * numberOfYears);
#endif
  }
}
