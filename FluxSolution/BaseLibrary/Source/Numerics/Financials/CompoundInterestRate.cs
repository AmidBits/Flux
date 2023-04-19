namespace Flux
{
  public static partial class Financials
  {
    /// <summary>Compound interest is the addition of interest to the principal sum of a loan or deposit, or in other words, interest on interest.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Compound_interest"/>
    static public double CompoundInterestRate(double nominalInterestRate, double compoundingPeriodsPerYear, int numberOfYears)
      => System.Math.Pow(1 + nominalInterestRate / compoundingPeriodsPerYear, compoundingPeriodsPerYear * numberOfYears);
  }
}
