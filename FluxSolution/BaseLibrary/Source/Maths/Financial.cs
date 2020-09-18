namespace Flux
{
  public static class Financial
  {
    /// <summary>Compound interest is the addition of interest to the principal sum of a loan or deposit, or in other words, interest on interest.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Compound_interest"/>
    static public double CompoundInterestRate(double nominalInterestRateAsDecimal, double compoundingPeriodsPerYear, double numberOfYears)
      => System.Math.Pow(1 + nominalInterestRateAsDecimal / compoundingPeriodsPerYear, compoundingPeriodsPerYear * numberOfYears);
    /// <summary>The interest rate on a loan or financial product restated from the nominal interest rate as an interest rate with annual compound interest payable in arrears.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Effective_interest_rate"/>
    static public double EffectiveInterestRate(double nominalRate, double compoundingPeriods)
      => System.Math.Pow(1.0 + nominalRate / compoundingPeriods, compoundingPeriods) - 1.0;
    /// <summary>An amortizing mortgage loan where the interest rate on the note remains the same through the term of the loan, as opposed to loans where the interest rate may adjust or "float".</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Fixed-rate_mortgage"/>
    static public double FixedRateMortgageMonthlyPayment(double loanAmount, double fixedYearlyNominalInterestRate, double numberOfYears)
    {
      var r = fixedYearlyNominalInterestRate / 100.0 / 12.0;

      return r / (1.0 - System.Math.Pow(1.0 + r, -numberOfYears * 12.0)) * loanAmount;
    }
  }
}
