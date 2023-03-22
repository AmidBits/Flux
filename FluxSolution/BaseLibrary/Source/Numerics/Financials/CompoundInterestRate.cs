namespace Flux
{
  public static partial class Financials
  {
    /// <summary>Compound interest is the addition of interest to the principal sum of a loan or deposit, or in other words, interest on interest.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Compound_interest"/>
    static public TSelf CompoundInterestRate<TSelf>(TSelf nominalInterestRateAsDecimal, TSelf compoundingPeriodsPerYear, TSelf numberOfYears)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => TSelf.Pow(TSelf.One + nominalInterestRateAsDecimal / compoundingPeriodsPerYear, compoundingPeriodsPerYear * numberOfYears);
  }
}
