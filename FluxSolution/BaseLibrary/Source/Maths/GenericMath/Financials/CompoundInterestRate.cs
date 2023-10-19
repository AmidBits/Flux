namespace Flux
{
  public static partial class Financials
  {
    /// <summary>Compound interest is the addition of interest to the principal sum of a loan or deposit, or in other words, interest on interest.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Compound_interest"/>
    static public TSelf CompoundInterestRate<TSelf>(this TSelf nominalInterestRate, TSelf compoundingPeriodsPerYear, TSelf numberOfYears)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => TSelf.Pow(TSelf.One + nominalInterestRate / compoundingPeriodsPerYear, compoundingPeriodsPerYear * numberOfYears);
  }
}
