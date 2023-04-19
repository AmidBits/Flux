namespace Flux
{
  public static partial class Financials
  {
    /// <summary>The interest rate on a loan or financial product restated from the nominal interest rate as an interest rate with annual compound interest payable in arrears.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Effective_interest_rate"/>
    static public double EffectiveInterestRate(double nominalRate, double compoundingPeriods)
      => System.Math.Pow(1 + nominalRate / compoundingPeriods, compoundingPeriods) - 1;
  }
}
